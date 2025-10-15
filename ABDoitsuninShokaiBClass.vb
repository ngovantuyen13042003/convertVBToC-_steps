'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        同一人照会ＤＡ(ABDoitsuninShokaiBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/05/01　滝沢　欽也
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/08/28 000001     RDBアクセスログの修正
'* 2004/01/19 000002     旧市町村コードの追加に伴う修正     
'* 2007/05/22 000003     宛名データ種別の追加に伴う修正(生年月日統一化の判定用に追加)
'* 2007/07/10 000004     DB文字数拡張対応，文字数を拡張したDBに対応するためにカラム作成時のMaxLength値修正（中沢）
'* 2014/09/01 000005     【AB21010】個人番号制度対応（岩下）
'* 2022/12/16 000006    【AB-8010】住民コード世帯コード15桁対応(下村)
'* 2023/12/18 000007    【AB-7010-1】同一人設定情報取得対応(下村)
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
Imports Densan.Common

Public Class ABDoitsuninShokaiBClass
#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfLog As UFLogClass                           ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdb As UFRdbClass                           ' ＲＤＢクラス

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABDoitsuninShokaiBClass"
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfUFControlData As UFControlData,
    '*                                ByVal cfUFConfigDataClass As UFConfigDataClass,
    '*                                ByVal cfUFRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
    '*                 cfUFConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
    '*                 cfUFRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass, ByVal cfRdb As UFRdbClass)

        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigData
        m_cfRdb = cfRdb

        ' ログ出力クラスのインスタンス化
        m_cfLog = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     同一人グループ宛名抽出
    '* 
    '* 構文           Public Function GetDoitsuninAtena(ByVal strDoitsuninShikibetsuCD As String) As DataSet
    '* 
    '* 機能　　    　　合併同一人より該当データを全件取得する。
    '* 
    '* 引数           strDoitsuninShikibetsuCD As String      :同一人識別コード
    '* 
    '* 戻り値         取得した合併同一人の該当データ（DataSet）
    '*                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsuninAtena(ByVal strDoitsuninShikibetsuCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninAtena"          ' このメソッド名
        Dim csGappeiDoitsuninEntity As DataSet                          ' 合併同一人データ
        Dim strSQL As New StringBuilder()                               ' SQL文文字列
        Dim cfParameter As UFParameterClass                             ' パラメータクラス
        Dim cfParameterCollection As UFParameterCollectionClass         ' パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(".*,")
            strSQL.Append(ABAtenaEntity.TABLE_NAME)
            strSQL.Append(".* FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(" LEFT OUTER JOIN ")
            strSQL.Append(ABAtenaEntity.TABLE_NAME)
            strSQL.Append(" ON ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append("=")
            strSQL.Append(ABAtenaEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABAtenaEntity.JUMINCD)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            strSQL.Append("=")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABAtenaEntity.JUTOGAIYUSENKB)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaEntity.KEY_JUTOGAIYUSENKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfParameterCollection = New UFParameterCollectionClass()
            ' 検索条件のパラメータを作成

            ' 同一人識別コード
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
            cfParameter.Value = strDoitsuninShikibetsuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfParameterCollection.Add(cfParameter)

            ' 住登外優先区分
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABAtenaEntity.KEY_JUTOGAIYUSENKB
            cfParameter.Value = "1"
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfParameterCollection.Add(cfParameter)

            '*履歴番号 000001 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLog.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            m_cfLog.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfParameterCollection) + "】")
            '*履歴番号 000001 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            csGappeiDoitsuninEntity = m_cfRdb.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)


            ' デバッグログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csGappeiDoitsuninEntity

    End Function

    '************************************************************************************************
    '* メソッド名     同一人データスキーマ作成
    '* 
    '* 構文           Public Function GetSchemaDoitsuninData() As DataSet
    '* 
    '* 機能　　       同一人データのスキーマを作成する。
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         ABDoitsuninDataEntity(DataSet) : 同一人データ
    '************************************************************************************************
    Public Function GetSchemaDoitsuninData() As DataSet
        Const THIS_METHOD_NAME As String = "GetSchemaDoitsuninData"
        Dim csDoitsuninDataEntity As DataSet                ' 同一人データセット
        Dim csDoitsuninDataTable As DataTable               ' 同一人データテーブル
        Dim csDataColumn As DataColumn                      ' データカラム

        Try
            ' デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 合併同一人のテーブルスキーマを取得する
            csDoitsuninDataEntity = m_cfRdb.GetTableSchema(ABGappeiDoitsuninEntity.TABLE_NAME)

            ' テーブル名を変更する
            csDoitsuninDataTable = csDoitsuninDataEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME)
            csDoitsuninDataTable.TableName = ABDoitsuninDataEntity.TABLE_NAME

            '**
            '* 表示用カラムを追加する
            '*
            ' 表示用種別(住民種別)
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_HENSHUSHUBETSURYOKU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' 表示用生年月日
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_UMAREHYOJIWMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 11
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' 表示用性別
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_SEIBETSU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' 表示用氏名（名称）
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_HENSHUKANJISHIMEI, System.Type.GetType("System.String"))
            '* 履歴番号 000004 2007/07/10 修正開始
            csDataColumn.MaxLength = 240
            'csDataColumn.MaxLength = 40
            '* 履歴番号 000004 2007/07/10 修正終了
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' 表示用住所
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_HENSHUJUSHO, System.Type.GetType("System.String"))
            '* 履歴番号 000004 2007/07/10 修正開始
            csDataColumn.MaxLength = 160
            'csDataColumn.MaxLength = 60
            '* 履歴番号 000004 2007/07/10 修正終了
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' 表示用行政区
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_GYOSEIKUMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' 表示用世帯コード
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_STAICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            '*履歴番号 000002 2003/08/28 修正開始
            ' 表示用世帯コード
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            '*履歴番号 000002 2003/08/28 修正終了
            ' 表示用本人区分
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_HONNINKBMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDoitsuninDataTable.Columns.Add(csDataColumn)

            ' 履歴番号 000003 2007/05/22 追加開始
            ' 宛名データ種別
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.ATENADATASHU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' 履歴番号 000003 2007/05/22 追加終了

            ' 履歴番号 000005 2014/09/01 追加開始
            ' 個人番号
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.MYNUMBER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' 宛名データ区分
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.ATENADATAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' 履歴番号 000005 2014/09/01 追加終了

            ' デバッグ終了ログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
            ' UFAppExceptionをスローする
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp
        End Try

        Return csDoitsuninDataEntity

    End Function

#Region "同一人取得"
    '************************************************************************************************
    '* メソッド名     同一人取得
    '* 
    '* 構文           Public Function GetDoitsuninData_JuminCD(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　住民コード指定で同一人を取得する。
    '* 
    '* 引数           strJuminCD As String      :住民コード
    '* 
    '* 戻り値         取得した合併同一人の該当データ（DataSet）
    '*                   構造：csGappeiDoitsuninEntity    インテリセンス：ABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsuninData_JuminCD(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninData_JuminCD"          ' このメソッド名
        Dim csGappeiDoitsuninEntity As DataSet                          ' 合併同一人データ
        Dim strSQL As New StringBuilder()                               ' SQL文文字列
        Dim cfParameter As UFParameterClass                             ' パラメータクラス
        Dim cfParameterCollection As UFParameterCollectionClass         ' パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            With strSQL
                .Append("SELECT * FROM ")
                .Append(ABGappeiDoitsuninEntity.TABLE_NAME)

                ' WHERE文結合
                .Append(" WHERE ")
                .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                .Append(" = (SELECT ")
                .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                .Append(" FROM ")
                .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABGappeiDoitsuninEntity.JUMINCD)
                .Append(" = ")
                .Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
                .Append(" AND ")
                .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                .Append(" <> '1')")
                .Append(" AND ")
                .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                .Append(" <> '1'")
                .Append(" ORDER BY ")
                .Append(ABGappeiDoitsuninEntity.JUMINCD)
            End With

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfParameterCollection = New UFParameterCollectionClass()
            ' 検索条件のパラメータを作成

            ' 同一人識別コード
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfParameter.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfParameterCollection.Add(cfParameter)

            ' RDBアクセスログ出力
            m_cfLog.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfParameterCollection) + "】")

            ' SQLの実行 DataSetの取得
            csGappeiDoitsuninEntity = m_cfRdb.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)


            ' デバッグログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csGappeiDoitsuninEntity

    End Function
#End Region

#Region "同一人候補者取得"
    '************************************************************************************************
    '* メソッド名     同一人候補者取得
    '* 
    '* 構文           Public Function GetDoitsuninKohoshaData(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　住民コード指定で同一人候補者を取得する。
    '* 
    '* 引数           strJuminCD As String      :住民コード
    '* 
    '* 戻り値         取得した同一人候補者のデータ（DataSet）
    '*                   構造：csResultDS    インテリセンス：ABDoitsuninKohoshaEntity
    '************************************************************************************************
    Public Function GetDoitsuninKohoshaData(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninKohoshaData"          ' このメソッド名
        Dim csResultDS As DataSet                                       ' 同一人候補者データ
        Dim strSQL As New StringBuilder()                               ' SQL文文字列
        Dim cfParameter As UFParameterClass                             ' パラメータクラス
        Dim cfParameterCollection As UFParameterCollectionClass         ' パラメータコレクションクラス
        Dim cSearchKey As ABAtenaSearchKey
        Dim cABAtenaB As ABAtenaBClass
        Dim csDataSet As DataSet
        Dim csRow As DataRow
        Dim strUmareYMD As String
        Dim strSearchKanaShimei1 As String
        Dim strSearchKanaShimei2 As String
        Dim strSearchKanaShimei3 As String
        Dim strSearchKanaShimei4 As String
        Dim strSearchKanaShimei5 As String
        Dim strSeibetsuCd As String
        Dim intI As Integer = 0

        Try
            ' デバッグログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '対象者の検索
            cSearchKey = New ABAtenaSearchKey
            cSearchKey.p_strJuminCD = strJuminCD
            cSearchKey.p_strJutogaiYusenKB = "1"                                '住登外優先
            cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdb, ABEnumDefine.AtenaGetKB.SelectAll, True)
            cABAtenaB.m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun           '標準化対応
            csDataSet = cABAtenaB.GetAtenaBHoshu(1, cSearchKey)

            If (csDataSet Is Nothing) Then
                Return csResultDS
            Else
                If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count > 0) Then
                    csRow = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)
                    strUmareYMD = csRow.Item(ABAtenaEntity.UMAREYMD).ToString
                    strSearchKanaShimei1 = csRow.Item(ABAtenaEntity.SEARCHKANASEIMEI).ToString
                    If (csRow.Item(ABAtenaEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_HOJIN) Then
                        strSearchKanaShimei2 = csRow.Item(ABAtenaEntity.SEARCHKANASEI).ToString
                    Else
                        strSearchKanaShimei2 = String.Empty
                    End If
                    strSearchKanaShimei3 = csRow.Item(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI).ToString
                    strSearchKanaShimei4 = csRow.Item(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI).ToString
                    strSearchKanaShimei5 = csRow.Item(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI).ToString
                    strSeibetsuCd = csRow.Item(ABAtenaEntity.SEIBETSUCD).ToString
                Else
                    Return csResultDS
                End If
            End If

            ' SQL文の作成
            With strSQL
                .Append(CreateSelect)
                .Append(" FROM ")
                .Append(ABAtenaEntity.TABLE_NAME)
                .Append(" LEFT JOIN ")
                .Append(ABAtenaFZYEntity.TABLE_NAME)
                .AppendFormat(" ON {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                .AppendFormat(" = {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD)
                .AppendFormat(" AND {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB)
                .AppendFormat(" = {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINJUTOGAIKB)
                .Append(" LEFT JOIN ")
                .Append(ABAtenaFZYHyojunEntity.TABLE_NAME)
                .AppendFormat(" ON {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                .AppendFormat(" = {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD)
                .AppendFormat(" AND {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB)
                .AppendFormat(" = {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB)

                ' WHERE文結合
                .Append(" WHERE ")
                .AppendFormat("{0}.{1} = '1'", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUTOGAIYUSENKB)
                .AppendFormat(" AND {0}.{1} <> '1'", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SAKUJOFG)
                .AppendFormat(" AND {0}.{1} <> ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                .Append(ABAtenaEntity.KEY_JUMINCD)
                .AppendFormat(" AND {0}.{1} = ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.UMAREYMD)
                .Append(ABAtenaEntity.PARAM_UMAREYMD)
                .AppendFormat(" AND {0}.{1} = ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEIBETSUCD)
                .Append(ABAtenaEntity.PARAM_SEIBETSUCD)
                '検索カナ姓名
                .AppendFormat(" AND (( {0}.{1} <> '' AND ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEIMEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEIMEI)
                intI = 1
                .AppendFormat("{0},", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                '検索カナ姓
                .AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEI)
                intI = 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                '検索カナ外国人名
                .AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                intI = 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                '検索カナ通称名
                .AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                intI = 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                '検索カナ併記名
                .AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                intI = 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})))", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
            End With

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfParameterCollection = New UFParameterCollectionClass()
            ' 検索条件のパラメータを作成

            ' 住民コード
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABAtenaEntity.KEY_JUMINCD
            cfParameter.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfParameterCollection.Add(cfParameter)

            ' 生年月日
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABAtenaEntity.PARAM_UMAREYMD
            cfParameter.Value = strUmareYMD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfParameterCollection.Add(cfParameter)

            ' 性別コード
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABAtenaEntity.PARAM_SEIBETSUCD
            cfParameter.Value = strSeibetsuCd
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfParameterCollection.Add(cfParameter)

            ' 検索カナ姓名
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter)
            Next

            ' 検索カナ姓
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter)
            Next

            ' 検索カナ外国人名
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter)
            Next

            ' 検索カナ通称名
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter)
            Next

            ' 検索カナ併記名
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                cfParameterCollection.Add(cfParameter)
            Next

            ' RDBアクセスログ出力
            m_cfLog.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfParameterCollection) + "】")

            ' SQLの実行 DataSetの取得
            csResultDS = m_cfRdb.GetDataSet(strSQL.ToString, ABDoitsuninKohoshaEntity.TABLE_NAME, cfParameterCollection)

            ' デバッグログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csResultDS

    End Function

#End Region

#Region "SELECT句作成"
    '************************************************************************************************
    '* メソッド名     SELECT句の作成
    '* 
    '* 構文           Private Sub CreateSelect() As String
    '* 
    '* 機能　　    　 SELECT句を生成する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         String    :   SELECT句
    '************************************************************************************************
    Private Function CreateSelect() As String
        Const THIS_METHOD_NAME As String = "CreateSelect"
        Dim csSELECT As New StringBuilder

        Try
            ' デバッグログ出力
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT句の作成
            csSELECT.AppendFormat("SELECT {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.ATENADATAKB)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.ATENADATASHU)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANAMEISHO1)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANJIMEISHO1)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANAMEISHO2)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANJIMEISHO2)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJITSUSHOMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHONGOKUMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.HONGOKUMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJIHEIKIMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SHIMEIYUSENKB)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.UMAREYMD)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEIBETSUCD)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEIBETSU)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUSHO)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KATAGAKI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.YUBINNO)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUSHOCD)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHICD1)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHICD2)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHICD3)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KATAGAKICD)

            ' デバッグログ出力
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLog.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLog.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return csSELECT.ToString

    End Function
#End Region

#End Region

End Class
