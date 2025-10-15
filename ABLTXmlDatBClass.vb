'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         ＡＢｅＬＴＡＸ受信ＸＭＬマスタ(ABLTXmlDatBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付             2009/07/15
'*
'* 作成者           比嘉　計成
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2010/04/16   000001     VS2008対応（比嘉）
'* 2011/08/30   000002     eLTAX利用届出連携の削除機能追加に伴う改修（比嘉）
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

Public Class ABLTXmlDatBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス

    Private m_csDataSchma As DataSet                        ' スキーマ保管用データセット
    Private m_strInsertSQL As String
    Private m_strUpDateSQL As String
    Private m_strUpDateSQL_ConvertFG As String
    Private m_strUpDateSQL_SakujoFG As String
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE用パラメータコレクション
    Private m_cfUpdateConvertFGUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE用パラメータコレクション
    Private m_cfUpdateSakujoFGUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE用パラメータコレクション
    '*履歴番号 000002 2011/08/30 追加開始
    Private m_strDeleteSQL As String
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass  'DELETE用パラメータコレクション
    '*履歴番号 000002 2011/08/30 追加終了

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABLTXmlDatBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData As UFControlData, 
    '*                                ByVal cfConfigDataClass As UFConfigDataClass, 
    '*                                ByVal cfRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
    '*                 cfConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
    '*                 cfRdbClass As UFRdbClass               : ＲＤＢデータオブジェクト
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
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABLTXMLDatEntity.TABLE_NAME, ABLTXMLDatEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "メソッド"

#Region "eLTAX受信XMLデータ取得メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XML届出・申告データ取得
    '* 
    '* 構文         Public Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
    '* 
    '* 機能　　     eLTAX受信XMLマスタより該当データを取得する。
    '* 
    '* 引数         csABLTXmlDatParaX As ABLTXmlDatParaXClass   : eLTAX受信XMLパラメータクラス
    '* 
    '* 戻り値       取得したｅＬＴＡＸ受信ＸＭＬマスタの該当データ（DataSet）
    '*                 構造：csLtXMLDatEntity    
    '************************************************************************************************
    Public Overloads Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTXmlDat"

        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim csLtXMLDatEntity As DataSet                                 ' 利用届出受信マスタ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL文の作成
            strSQL.Append("SELECT * ")
            strSQL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME)

            ' WHERE句
            strSQL.Append(" WHERE ")

            ' 必須条件
            '* SHINKOKUSHINSEIKB = "R0" AND 
            strSQL.Append(ABLTXMLDatEntity.SHINKOKUSHINSEIKB).Append(" = ")
            strSQL.Append(ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ")
            strSQL.Append("'1'")


            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB
            cfUFParameterClass.Value = ABConstClass.ELTAX_RIYOTDKD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 税目区分
            If (csABLTXmlDatParaX.p_strTaxKB <> ABEnumDefine.ZeimokuCDType.Empty) Then
                strSQL.Append(" AND ")

                ' 税目区分が設定されている場合、抽出条件にする
                strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(" = ")
                strSQL.Append(ABLTXMLDatEntity.KEY_TAXKB)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(csABLTXmlDatParaX.p_strTaxKB)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If

            ' コンバートフラグ
            strSQL.Append(" AND ")
            If (csABLTXmlDatParaX.p_blnConvertFG = True) Then
                ' コンバートフラグがTrueの場合、"1"を取得する
                strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" = ")
                strSQL.Append("'1'")

            Else
                ' コンバートフラグがFalseの場合、"1"以外を取得する
                strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> ")
                strSQL.Append("'1'")

            End If

            ' 最大取得件数セット
            If (csABLTXmlDatParaX.p_intMaxCount <> 0) Then
                m_cfRdbClass.p_intMaxRows = csABLTXmlDatParaX.p_intMaxCount
            Else
            End If

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' 届出・申告データ取得
            csLtXMLDatEntity = m_csDataSchma.Clone()
            csLtXMLDatEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtXMLDatEntity, ABLTXMLDatEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


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

        Return csLtXMLDatEntity

    End Function
#End Region

#Region "eLTAX受信XMLデータ取得メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XML届出・申告データ取得
    '* 
    '* 構文         Public Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass, _
    '*                                          ByRef intAllCount As Integer) As DataSet
    '* 
    '* 機能　　     eLTAX受信XMLマスタより該当データを取得する。
    '* 
    '* 引数         csABLTXmlDatParaX As ABLTXmlDatParaXClass   : eLTAX受信XMLパラメータクラス
    '*              intAllCount As Integer                      : 全データ件数
    '* 
    '* 戻り値       取得したｅＬＴＡＸ受信ＸＭＬマスタの該当データ（DataSet）
    '*                 構造：csLtXMLDatEntity    
    '************************************************************************************************
    Public Overloads Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass, ByRef intAllCount As Integer) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTXmlDat"
        Const COL_COUNT As String = "COUNT"
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim csLtXMLDatEntity As DataSet                                 ' 利用届出受信マスタ
        Dim csLtXmlDat_All As DataSet                                   ' 利用届出受信全件データ
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim strSQL_ALL As New StringBuilder                             ' SQL文全件取得文字列
        Dim strWhere As New StringBuilder                               ' WHERE文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL文の作成
            strSQL.Append("SELECT * ")
            strSQL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME)

            strSQL_ALL.Append("SELECT COUNT(*) AS ").Append(COL_COUNT)
            strSQL_ALL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME)

            ' WHERE句
            strWhere.Append(" WHERE ")

            ' 必須条件
            '* SHINKOKUSHINSEIKB = "R0" AND 
            strWhere.Append(ABLTXMLDatEntity.SHINKOKUSHINSEIKB).Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB)
            strWhere.Append(" AND ")

            '*履歴番号 000002 2011/08/30 修正開始
            If (csABLTXmlDatParaX.p_blnSakuJoFG = False) Then
                ' eLTAX受信XMLパラメータクラス:削除フラグ="False"の場合、削除データ以外を抽出
                strWhere.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ")
                strWhere.Append("'1'")
            Else
                ' eLTAX受信XMLパラメータクラス:削除フラグ="True"の場合、削除データを抽出
                strWhere.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" = ")
                strWhere.Append("'1'")
            End If
            'strWhere.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ")
            'strWhere.Append("'1'")
            '*履歴番号 000002 2011/08/30 修正終了


            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB
            cfUFParameterClass.Value = ABConstClass.ELTAX_RIYOTDKD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 税目区分
            If (csABLTXmlDatParaX.p_strTaxKB <> ABEnumDefine.ZeimokuCDType.Empty) Then
                strWhere.Append(" AND ")

                ' 税目区分が設定されている場合、抽出条件にする
                strWhere.Append(ABLTXMLDatEntity.TAXKB).Append(" = ")
                strWhere.Append(ABLTXMLDatEntity.KEY_TAXKB)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(csABLTXmlDatParaX.p_strTaxKB)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If

            ' コンバートフラグ
            strWhere.Append(" AND ")
            If (csABLTXmlDatParaX.p_blnConvertFG = True) Then
                ' コンバートフラグがTrueの場合、"1"を取得する
                strWhere.Append(ABLTXMLDatEntity.CONVERTFG).Append(" = ")
                strWhere.Append("'1'")

            Else
                ' コンバートフラグがFalseの場合、"1"以外を取得する
                strWhere.Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> ")
                strWhere.Append("'1'")

            End If

            ' 最大取得件数セット
            If (csABLTXmlDatParaX.p_intMaxCount <> 0) Then
                m_cfRdbClass.p_intMaxRows = csABLTXmlDatParaX.p_intMaxCount
            Else
            End If

            ' SQL文結合 
            strSQL.Append(strWhere.ToString)
            strSQL_ALL.Append(strWhere.ToString)

            ' 全件取得処理
            csLtXmlDat_All = m_cfRdbClass.GetDataSet(strSQL_ALL.ToString, cfUFParameterCollectionClass)

            intAllCount = CInt(csLtXmlDat_All.Tables(0).Rows(0)(COL_COUNT))


            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' 届出・申告データ取得
            csLtXMLDatEntity = m_csDataSchma.Clone()
            csLtXMLDatEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtXMLDatEntity, ABLTXMLDatEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


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

        Return csLtXMLDatEntity

    End Function
#End Region

#Region "eLTAX受信XML届出・申告データ件数取得メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XML届出・申告データ件数取得
    '* 
    '* 構文         Public Function GetLTXmlCount(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
    '* 
    '* 機能　　     eLTAX受信XMLマスタより該当データの件数を取得する。
    '* 
    '* 引数         csABLTXmlDatParaX As ABLTXmlDatParaXClass   : eLTAX受信XMLパラメータクラス
    '* 
    '* 戻り値       取得したeLTAX受信データ件数データ（DataSet）
    '*                 構造：csLtXMLDatCountDS    
    '************************************************************************************************
    Public Function GetLTXmlCount(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTXmlCount"

        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim objErrorStruct As UFErrorStruct                             ' エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim csLtXMLDatCountDS As DataSet                                ' ABeLTAX受信DAT件数データセット
        Dim csDataSet As DataSet
        Dim strSQL As New StringBuilder                                 ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' パラメータコレクションクラス
        Dim csDataRow As DataRow
        Dim csNewRow As DataRow
        Dim intCount As Integer = 0

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL文の作成
            strSQL.Append("SELECT ")
            strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(", ")
            strSQL.Append(ABLTXMLDatEntity.PROCID).Append(", ")
            strSQL.Append("COUNT(*) AS COUNT")
            strSQL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME)

            ' WHERE句
            strSQL.Append(" WHERE ")

            ' 必須条件
            '* SHINKOKUSHINSEIKB = "T0" AND 
            strSQL.Append(ABLTXMLDatEntity.SHINKOKUSHINSEIKB).Append(" = ")
            strSQL.Append(ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ")
            strSQL.Append("'1'")

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB
            cfUFParameterClass.Value = ABConstClass.ELTAX_RIYOTDKD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 税目区分
            If (csABLTXmlDatParaX.p_strTaxKB <> ABEnumDefine.ZeimokuCDType.Empty) Then
                strSQL.Append(" AND ")

                ' 税目区分が設定されている場合、抽出条件にする
                strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(" = ")
                strSQL.Append(ABLTXMLDatEntity.KEY_TAXKB)

                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(csABLTXmlDatParaX.p_strTaxKB)

                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If

            ' コンバートフラグ
            strSQL.Append(" AND ")
            If (csABLTXmlDatParaX.p_blnConvertFG = True) Then
                ' コンバートフラグがTrueの場合、"1"を取得する
                strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" = ")
                strSQL.Append("'1'")

            Else
                ' コンバートフラグがFalseの場合、"1"以外を取得する
                strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> ")
                strSQL.Append("'1'")

            End If

            ' GROUP BY句
            strSQL.Append(" GROUP BY ")
            strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(", ")
            strSQL.Append(ABLTXMLDatEntity.PROCID)

            ' ORDER BY句
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(", ")
            strSQL.Append(ABLTXMLDatEntity.PROCID)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                  "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                  "【実行メソッド名:GetDataSet】" + _
                                  "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' データ取得
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABLTXMLDatEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


            ' eLTAX受信DAT件数データテーブル作成
            csLtXMLDatCountDS = CreateDataSet()


            ' ｅＬＴＡＸ受信ＸＭＬ届出・申告データ件数データセットにセット
            For Each csDataRow In csDataSet.Tables(ABLTXMLDatEntity.TABLE_NAME).Rows

                csNewRow = csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).NewRow

                csNewRow(ABLTXmlDatCountData.TAXKB) = csDataRow(ABLTXMLDatEntity.TAXKB)
                csNewRow(ABLTXmlDatCountData.PROCID) = csDataRow(ABLTXMLDatEntity.PROCID)
                csNewRow(ABLTXmlDatCountData.PROCRYAKUMEI) = GetProcRyakumei(CStr(csDataRow(ABLTXMLDatEntity.PROCID)))
                csNewRow(ABLTXmlDatCountData.COUNT) = csDataRow("COUNT")

                csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).Rows.Add(csNewRow)

            Next
            '----------------------------------------------------------------------------
            ' 合計行追加
            csNewRow = csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).NewRow

            ' 税目区分
            If (csABLTXmlDatParaX.p_strTaxKB <> ABEnumDefine.ZeimokuCDType.Empty) Then
                ' 空白以外
                csNewRow(ABLTXmlDatCountData.TAXKB) = CStr(csABLTXmlDatParaX.p_strTaxKB)
            Else
                ' 空白の場合
                csNewRow(ABLTXmlDatCountData.TAXKB) = String.Empty
            End If

            ' 手続ID
            csNewRow(ABLTXmlDatCountData.PROCID) = String.Empty

            ' 手続名
            csNewRow(ABLTXmlDatCountData.PROCRYAKUMEI) = String.Empty

            ' 件数
            For Each csDataRow In csDataSet.Tables(ABLTXMLDatEntity.TABLE_NAME).Rows
                intCount += CInt(csDataRow("COUNT"))
            Next
            csNewRow(ABLTXmlDatCountData.COUNT) = CStr(intCount)

            csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).Rows.Add(csNewRow)
            '----------------------------------------------------------------------------


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

        Return csLtXMLDatCountDS

    End Function
#End Region

#Region "eLTAX受信XMLデータ追加メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XMLデータ追加メソッド
    '* 
    '* 構文         Public Function InsertLTXMLDat(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     eLTAX受信XMLマスタに新規データを追加する
    '* 
    '* 引数         csDataRow As DataRow   : 追加データ(ABeLTAXRiyoTdk)
    '* 
    '* 戻り値       追加件数(Integer)
    '************************************************************************************************
    Public Function InsertLTXMLDat(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertLTXMLDat"
        Dim cfParam As UFParameterClass                                 ' パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                                  ' データカラム
        '* corresponds to VS2008 End 2010/04/16 000001
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
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId              ' 端末ＩＤ
            csDataRow(ABLTXMLDatEntity.SAKUJOFG) = "0"                                          ' 削除フラグ
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = Decimal.Zero                            ' 更新カウンタ
            csDataRow(ABLTXMLDatEntity.SAKUSEINICHIJI) = strUpdateDateTime                      ' 作成日時
            csDataRow(ABLTXMLDatEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId               ' 作成ユーザー
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = strUpdateDateTime                       ' 更新日時
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                ' 更新ユーザー


            For Each cfParam In m_cfInsertUFParameterCollectionClass
                If (cfParam.ParameterName = ABLTXMLDatEntity.KEY_XMLDAT) Then
                    ' 項目:XMLDatの場合は、byte型のままセットする
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength))
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value =
                                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength)).ToString()
                End If
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

#Region "eLTAX受信XMLデータ更新メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XMLデータ更新メソッド
    '* 
    '* 構文         Public Function UpdateLTXMLDat(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     eLTAX受信XMLマスタのデータを更新する。
    '* 
    '* 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateLTXMLDat(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTXmlDat"
        Dim cfParam As UFParameterClass                         ' パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                          ' データカラム
        '* corresponds to VS2008 End 2010/04/16 000001
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
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' 端末ＩＤ
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' 更新カウンタ
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' 更新日時
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' 更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                ElseIf (cfParam.ParameterName = ABLTXMLDatEntity.KEY_XMLDAT) Then
                    ' 項目:XMLDatの場合は、byte型のままセットする
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current)
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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

#Region "eLTAX受信XMLデータ:コンバートフラグ更新メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XMLデータ:コンバートフラグ更新メソッド
    '* 
    '* 構文         Public Function UpdateLTXMLDat_ConvertFG(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     eLTAX受信XMLマスタのデータを更新する。
    '* 
    '* 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateLTXMLDat_ConvertFG(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTXMLDat_ConvertFG"
        Dim cfParam As UFParameterClass                         ' パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                          ' データカラム
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim intUpdCnt As Integer                                ' 更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpDateSQL_ConvertFG Is Nothing Or m_strUpDateSQL_ConvertFG = String.Empty Or _
                m_cfUpdateConvertFGUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL_UpDateConvertFG()
            Else
            End If

            ' 共通項目の編集を行う
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' 端末ＩＤ
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' 更新カウンタ
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' 更新日時
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' 更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateConvertFGUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfUpdateConvertFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateConvertFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                          csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current)
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL_ConvertFG, m_cfUpdateConvertFGUFParameterCollectionClass)

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

#Region "eLTAX受信XMLデータ:削除フラグ更新メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XMLデータ:削除フラグ更新メソッド
    '* 
    '* 構文         Public Overloads Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　     eLTAX受信XMLマスタのデータを更新する。
    '* 
    '* 引数         csDataRow As DataRow   : 利用届データ(ABeLTAXRiyoTdk)
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Overloads Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTXMLDat_SakujoFG"
        Dim cfParam As UFParameterClass                         ' パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                          ' データカラム
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim intUpdCnt As Integer                                ' 更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpDateSQL_SakujoFG Is Nothing Or m_strUpDateSQL_SakujoFG = String.Empty Or _
                m_cfUpdateSakujoFGUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL_UpDateSakujoFG()
            Else
            End If

            ' 共通項目の編集を行う
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' 端末ＩＤ
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' 更新カウンタ
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' 更新日時
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' 更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateSakujoFGUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                                csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL_SakujoFG, m_cfUpdateSakujoFGUFParameterCollectionClass)

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

#Region "eLTAX受信XMLデータ:削除フラグ更新メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XMLデータ:削除フラグ更新メソッド
    '* 
    '* 構文         Public Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow, _
    '*                                                      ByVal blnKoshinCounter As Boolean) As Integer
    '* 
    '* 機能　　     eLTAX受信XMLマスタのデータを更新する。
    '* 
    '* 引数         csDataRow As DataRow    : 利用届データ(ABeLTAXRiyoTdk)
    '*              blnKoshinCounter        : 更新カウンタ(True:条件に含む、False:含まない)
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Overloads Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow, _
                                                      ByVal blnKoshinCounter As Boolean) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTXMLDat_SakujoFG"
        Dim cfParam As UFParameterClass                         ' パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                          ' データカラム
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim intUpdCnt As Integer                                ' 更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpDateSQL_SakujoFG Is Nothing Or m_strUpDateSQL_SakujoFG = String.Empty Or _
                m_cfUpdateSakujoFGUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL_UpDateSakujoFG(blnKoshinCounter)
            Else
            End If

            ' 共通項目の編集を行う
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' 端末ＩＤ
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' 更新カウンタ
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' 更新日時
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' 更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateSakujoFGUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                                csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL_SakujoFG, m_cfUpdateSakujoFGUFParameterCollectionClass)

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

    '*履歴番号 000002 2011/08/30 追加開始
#Region "eLTAX受信XMLデータ:削除(物理)メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XMLデータ:削除(物理)メソッド
    '* 
    '* 構文         Public Overloads Function DeleteLTXMLDat() As Integer
    '* 
    '* 機能　　     eLTAX受信XMLマスタの該当データを物理削除する
    '* 
    '* 引数         なし
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteLTXMLDat(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteLTXMLDat"
        Dim cfParam As UFParameterClass                         ' パラメータクラス
        Dim intUpdCnt As Integer                                ' 更新件数
        Dim blnKoshinCounter As Boolean = False                 ' 更新カウンター

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpDateSQL_SakujoFG Is Nothing OrElse m_strUpDateSQL_SakujoFG = String.Empty OrElse _
                m_cfUpdateSakujoFGUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL_UpDateSakujoFG(blnKoshinCounter)
            Else
            End If

            ' 共通項目の編集を行う
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' 端末ＩＤ
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' 更新カウンタ
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' 更新日時
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' 更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                                csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

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

#Region "eLTAX受信XMLデータ:削除データ一括削除(物理)メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XMLデータ:削除データ一括削除(物理)メソッド
    '* 
    '* 構文         Public Overloads Function DeleteLTXMLDat_Sakujo() As Integer
    '* 
    '* 機能　　     eLTAX受信XMLマスタのデータの削除フラグ="1"のデータを一括削除する
    '* 
    '* 引数         なし
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteLTXMLDat_Sakujo() As Integer
        Const THIS_METHOD_NAME As String = "DeleteLTXMLDat_Sakujo"
        Dim csSQL As New StringBuilder
        Dim intUpdCnt As Integer                                ' 更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文作成
            csSQL.Append("DELETE ").Append(ABLTXMLDatEntity.TABLE_NAME)
            csSQL.Append(" WHERE ").Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> '1' ")
            csSQL.Append("AND ").Append(ABLTXMLDatEntity.SAKUJOFG).Append(" = '1'")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(csSQL.ToString) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(csSQL.ToString)

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

#Region "eLTAX受信XMLデータ:コンバート済み一括削除(物理)メソッド"
    '************************************************************************************************
    '* メソッド名   eLTAX受信XMLデータ:コンバート済み一括削除(物理)メソッド
    '* 
    '* 構文         Public Overloads Function DeleteLTXMLDat_Sakujo() As Integer
    '* 
    '* 機能　　     eLTAX受信XMLマスタのデータのコンバートフラグ="1"のデータを一括削除する
    '* 
    '* 引数         なし
    '* 
    '* 戻り値       更新件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteLTXMLDat_Convert() As Integer
        Const THIS_METHOD_NAME As String = "DeleteLTXMLDat_Convert"
        Dim csSQL As New StringBuilder
        Dim intUpdCnt As Integer                    ' 更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文作成
            csSQL.Append("DELETE ").Append(ABLTXMLDatEntity.TABLE_NAME)
            csSQL.Append(" WHERE ").Append(ABLTXMLDatEntity.CONVERTFG).Append(" = '1'")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(csSQL.ToString) + "】")

            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(csSQL.ToString)

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
    '*履歴番号 000002 2011/08/30 追加終了

#Region "SQL文の作成"
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
            m_strInsertSQL = "INSERT INTO " + ABLTXMLDatEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' UPDATE SQL文の作成
            m_strUpDateSQL = "UPDATE " + ABLTXMLDatEntity.TABLE_NAME + " SET "

            ' UPDATE Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER)

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' UPDATE SQL文の作成
                m_strUpDateSQL += csDataColumn.ColumnName + " = " + ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            ' 受信日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 受付番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ＸＭＬ連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 申告受付番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER
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

#Region "SQL文の作成(コンバートフラグ用)"
    '************************************************************************************************
    '* メソッド名   SQL文の作成(コンバートフラグ用)
    '* 
    '* 構文         Private Sub CreateSQL_UpDateConvertFG()
    '* 
    '* 機能　　     UPDATEの各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数         csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値       なし
    '************************************************************************************************
    Private Sub CreateSQL_UpDateConvertFG()
        Const THIS_METHOD_NAME As String = "CreateSQL_UpDateConvertFG"
        Dim cfUFParameterClass As UFParameterClass                  ' パラメータクラス
        Dim strWhere As New StringBuilder                           ' 更新SQL文Where文文字列
        Dim strSet As New StringBuilder                             ' 更新SQL文Set文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL文の作成
            m_strUpDateSQL_ConvertFG = "UPDATE " + ABLTXMLDatEntity.TABLE_NAME + " SET "

            ' UPDATE Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER)

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateConvertFGUFParameterCollectionClass = New UFParameterCollectionClass

            ' コンバートフラグ用UPDATE SQL文の作成
            m_strUpDateSQL_ConvertFG += ABLTXMLDatEntity.CONVERTFG + " = " + ABLTXMLDatEntity.KEY_CONVERTFG + ","

            ' 共通Set文
            strSet.Append(ABLTXMLDatEntity.TANMATSUID).Append(" = ").Append(ABLTXMLDatEntity.KEY_TANMATSUID).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINCOUNTER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINCOUNTER).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINNICHIJI).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINNICHIJI).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINUSER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINUSER)

            ' UPDATE SQL文にWHERE句の追加
            m_strUpDateSQL_ConvertFG += strSet.ToString + strWhere.ToString

            '*-------------------------------------------------------------------------*
            ' コンバートフラグ用UPDATE コレクションにパラメータを追加
            ' コンバートフラグ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_CONVERTFG
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            '*-------------------------------------------------------------------------*
            ' UPDATE コレクションにキー情報を追加
            ' 端末ＩＤ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TANMATSUID
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINCOUNTER
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINNICHIJI
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新ユーザ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINUSER
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 受信日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 受付番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ＸＭＬ連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 申告受付番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            '*-------------------------------------------------------------------------*

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

#Region "SQL文の作成(削除フラグ用)"
    '************************************************************************************************
    '* メソッド名   SQL文の作成(削除フラグ用)
    '* 
    '* 構文         Private Sub CreateSQL_UpDateSakujoFG()
    '* 
    '* 機能　　     UPDATEの各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数         csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値       なし
    '************************************************************************************************
    Private Sub CreateSQL_UpDateSakujoFG()

        Call CreateSQL_UpDateSakujoFG(True)

    End Sub
    Private Sub CreateSQL_UpDateSakujoFG(ByVal blnKoshinCounter As Boolean)
        Const THIS_METHOD_NAME As String = "CreateSQL_UpDateSakujoFG"
        Dim cfUFParameterClass As UFParameterClass                  ' パラメータクラス
        Dim strWhere As New StringBuilder                           ' 更新SQL文Where文文字列
        Dim strSet As New StringBuilder                             ' 更新SQL文Set文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL文の作成
            m_strUpDateSQL_SakujoFG = "UPDATE " + ABLTXMLDatEntity.TABLE_NAME + " SET "

            '*履歴番号 000002 2011/08/30 追加開始
            m_strDeleteSQL = "DELETE " + ABLTXMLDatEntity.TABLE_NAME
            '*履歴番号 000002 2011/08/30 追加終了

            ' UPDATE Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD)

            If (blnKoshinCounter = True) Then
                strWhere.Append(" AND ")
                strWhere.Append(ABLTXMLDatEntity.KOSHINCOUNTER)
                strWhere.Append(" = ")
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER)
            Else
            End If

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateSakujoFGUFParameterCollectionClass = New UFParameterCollectionClass

            ' 削除フラグ用UPDATE SQL文の作成
            m_strUpDateSQL_SakujoFG += ABLTXMLDatEntity.SAKUJOFG + " = " + ABLTXMLDatEntity.KEY_SAKUJOFG + ","

            ' 共通Set文
            strSet.Append(ABLTXMLDatEntity.TANMATSUID).Append(" = ").Append(ABLTXMLDatEntity.KEY_TANMATSUID).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINCOUNTER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINCOUNTER).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINNICHIJI).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINNICHIJI).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINUSER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINUSER)

            ' UPDATE SQL文にWHERE句の追加
            m_strUpDateSQL_SakujoFG += strSet.ToString + strWhere.ToString

            '*-------------------------------------------------------------------------*
            ' 削除フラグ用UPDATE コレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SAKUJOFG
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            '*-------------------------------------------------------------------------*
            ' UPDATE コレクションにキー情報を追加
            ' 端末ＩＤ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TANMATSUID
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINCOUNTER
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINNICHIJI
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新ユーザ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINUSER
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 受信日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 受付番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ＸＭＬ連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 申告受付番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            If (blnKoshinCounter = True) Then
                ' 更新カウンタ
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If
            '*-------------------------------------------------------------------------*

            '*履歴番号 000002 2011/08/30 追加開始
            ' DELETE パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE SQL文にWHERE句の追加
            m_strDeleteSQL += strWhere.ToString

            '*-------------------------------------------------------------------------*
            ' DELETE コレクションにキー情報を追加
            ' 受信日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 受付番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ＸＭＬ連番
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 申告受付番号
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            If (blnKoshinCounter = True) Then
                ' 更新カウンタ
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If
            '*-------------------------------------------------------------------------*
            '*履歴番号 000002 2011/08/30 追加終了

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

#Region "データセット作成"
    '************************************************************************************************
    '* メソッド名   ｅＬＴＡＸ受信ＤＡＴ件数データセット作成
    '* 
    '* 構文         Private Function CreateDataSet() As DataSet
    '* 
    '* 機能　　     ｅＬＴＡＸ受信ＤＡＴ件数データセットを作成する
    '* 
    '* 引数         なし
    '* 
    '* 戻り値       作成したｅＬＴＡＸ受信ＤＡＴデータセット(DataSet)
    '************************************************************************************************
    Private Function CreateDataSet() As DataSet
        Const THIS_METHOD_NAME As String = "CreateDataSet"
        Dim csDataSet As DataSet                        ' データセット
        Dim csDataTable As DataTable                    ' テーブル
        Dim csDataColumn As DataColumn                  ' カラム

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' DataSetのインスタンス作成
            csDataSet = New DataSet

            ' データテーブル作成
            csDataTable = csDataSet.Tables.Add(ABLTXmlDatCountData.TABLE_NAME)

            ' カラム定義の作成
            ' 税目区分
            csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.TAXKB, System.Type.GetType("System.String"))
            ' 手続ID
            csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.PROCID, System.Type.GetType("System.String"))
            ' 手続名(略)
            csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.PROCRYAKUMEI, System.Type.GetType("System.String"))
            ' 件数
            csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.COUNT, System.Type.GetType("System.String"))

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

        Return csDataSet

    End Function
#End Region

#Region "手続名称(略)取得処理"
    '************************************************************************************************
    '* メソッド名   手続名称(略)取得処理
    '* 
    '* 構文         Private Function GetProcRyakumei(ByVal strProcId As String) As String
    '* 
    '* 機能　　     手続名称(略)を取得する
    '* 
    '* 引数         ByVal strProcId As String   ：手続ＩＤ
    '* 
    '* 戻り値       
    '************************************************************************************************
    Private Function GetProcRyakumei(ByVal strProcId As String) As String
        Const THIS_METHOD_NAME As String = "GetProcRyakumei"
        Dim strProcRyakumei As String = String.Empty

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strProcId
                Case ABConstClass.ELTAX_PROCID_SHINKI
                    ' 手続ＩＤ:T0999910，手続略称:届出新規
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_SHINKI

                Case ABConstClass.ELTAX_PROCID_HENKO_RIYOSHAJOHO
                    ' 手続ＩＤ:T0999920，手続略称:変更(利)
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_HENKO_RIYOSHAJOHO

                Case ABConstClass.ELTAX_PROCID_HENKO_SHINKOKUSAKITAXKB
                    ' 手続ＩＤ:T0999910，手続略称:変更(申)
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_HENKO_SHINKOKUSAKITAXKB

                Case ABConstClass.ELTAX_PROCID_HAISHI
                    ' 手続ＩＤ:T0999910，手続略称:廃止
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_HAISHI

                Case ABConstClass.ELTAX_PROCID_SHOMEISHOSASIKAE
                    ' 手続ＩＤ:T0999910，手続略称:証明差替
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_SHOMEISHOSASIKAE

                Case Else

            End Select


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

        Return strProcRyakumei

    End Function
#End Region

#End Region

End Class
