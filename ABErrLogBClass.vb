'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        宛名更新エラーログＤＢ管理(ABErrLogBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2007/02/05　内山 堅太郎
'* 
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.Common
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Text
Imports System.Web

Public Class ABErrLogBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfConfigDataClass As UFConfigDataClass                      ' コンフィグデータ
    Private m_cfControlData As UFControlData                              ' コントロールデータ
    Private m_cfLogClass As UFLogClass                                    ' ログ出力クラス
    Private m_cfInsParamCollection As UFParameterCollectionClass          ' INSERT用パラメータコレクション
    Private m_strInsertSQL As String                                      ' INSERT用SQL
    Private m_strRsBusinId As String                                      ' ビジネスＩＤ保存用

    ' コンスタント定義
    Private Const TAISHOKBN_MIKAKUNIN As String = "0"                     ' 未確認
    Private Const TAISHOKBN_ZUMI As String = "1"                          ' 確認済
    Private Const JOKYOKBN_NORMAL As String = "0"                         ' 正常終了
    Private Const JOKYOKBN_ERR As String = "9"                            ' 異常終了
    Private Const SPACE As String = " "                                   ' SPACE

    Private Const THIS_CLASS_NAME As String = "ABErrLogBClass"            ' クラス名

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData As UFControlData,
    '* 　　                           ByVal cfConfigDataClass As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
    '* 　　            cfConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass)

        ' メンバ変数へセット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass

        ' ログ出力クラスインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' 受け取ったビジネスIDをメンバへ保存
        m_strRsBusinId = m_cfControlData.m_strBusinessId

        ' メンバ変数の初期化
        m_strInsertSQL = String.Empty
        m_cfInsParamCollection = Nothing

    End Sub

#End Region

#Region "エラーログ取得"
    '************************************************************************************************
    '* メソッド名      エラーログ取得
    '* 
    '* 構文            Public Function GetABErrLog() As String()
    '* 
    '* 機能            エラーログの取得を行なう
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          String()：エラー発生場所とエラーメッセージ
    '************************************************************************************************
    Public Function GetABErrLog() As String()

        Const THIS_METHOD_NAME As String = "GetABErrLog"
        Dim cfRdb As UFRdbClass
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csABErrLogEntity As DataSet
        Dim csDataRow As DataRow
        Dim intCnt As Integer
        Dim strGyomuMei As String
        Dim strErrMSG As String
        Dim strReturn() As String
        Dim strSQL As New StringBuilder

        Try
            ' 業務ＩＤを宛名(AB)に変更
            m_cfControlData.m_strBusinessId = "AB"

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                 "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                 "【実行メソッド名:Connect】")

            ' RDBクラスのインスタンス作成
            cfRdb = New UFRdbClass(m_cfControlData.m_strBusinessId)

            ' RDB接続
            cfRdb.Connect()

            ' SelectSQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABErrLogEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABErrLogEntity.TAISHOKB)
            strSQL.Append(" = ")
            strSQL.Append(ABErrLogEntity.KEY_TAISHOKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABErrLogEntity.JOKYOKB)
            strSQL.Append(" = ")
            strSQL.Append(ABErrLogEntity.KEY_JOKYOKB)
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABErrLogEntity.LOGNO)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TAISHOKB          ' 対処区分
            cfUFParameterClass.Value = TAISHOKBN_MIKAKUNIN
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_JOKYOKB           ' 状況区分
            cfUFParameterClass.Value = JOKYOKBN_ERR
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                 "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                 "【実行メソッド名:GetDataSet】" + _
                                 "【SQL内容:" + cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQL実行 DataSet取得
            csABErrLogEntity = cfRdb.GetDataSet(strSQL.ToString, ABErrLogEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' 戻り値編集用配列初期化
            Dim strRet(csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows.Count - 1) As String

            ' 戻り値編集
            'For intCnt = 0 To csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows.Count - 1
            '    csDataRow = csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows(intCnt)
            '    strGyomuMei = CType(csDataRow(ABErrLogEntity.MSG5), String).Trim
            '    strErrMSG = CType(csDataRow(ABErrLogEntity.MSG7), String).Trim
            '    strRet(intCnt) = strGyomuMei + "," + strErrMSG
            'Next intCnt

            intCnt = 0
            For Each csDataRow In csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows
                strGyomuMei = CType(csDataRow(ABErrLogEntity.MSG5), String).Trim          ' エラー発生場所
                strErrMSG = CType(csDataRow(ABErrLogEntity.MSG7), String).Trim            ' エラーメッセージ
                strRet(intCnt) = strGyomuMei + "," + strErrMSG
                intCnt += 1
            Next csDataRow

            ' 戻り値セット
            strReturn = strRet

        Catch objRdbExp As UFRdbException                          ' RdbExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニング内容:" + objRdbExp.Message + "】")
            ' ワーニングをスローする
            Throw objRdbExp

        Catch objRdbDeadLockExp As UFRdbDeadLockException          ' デッドロックをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニングコード:" + objRdbDeadLockExp.p_strErrorCode + "】" + _
                                     "【ワーニング内容:" + objRdbDeadLockExp.Message + "】")
            ' ワーニングをスローする
            Throw objRdbDeadLockExp

        Catch objUFRdbUniqueExp As UFRdbUniqueException            ' 一意制約違反をキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニングコード:" + objUFRdbUniqueExp.p_strErrorCode + "】" + _
                                     "【ワーニング内容:" + objUFRdbUniqueExp.Message + "】")
            ' ワーニングをスローする
            Throw objUFRdbUniqueExp

        Catch objRdbTimeOutExp As UFRdbTimeOutException            ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + _
                                     "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
            ' ワーニングをスローする
            Throw objRdbTimeOutExp

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                     "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception                             ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                   "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                   "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                   "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        Finally
            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                 "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                 "【実行メソッド名:Disconnect】")

            ' RDB切断
            cfRdb.Disconnect()

            ' 元のビジネスIDを入れる
            m_cfControlData.m_strBusinessId = m_strRsBusinId

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        End Try

        ' 戻り値設定
        Return strReturn

    End Function

#End Region

#Region "エラーログ追加"
    '************************************************************************************************
    '* メソッド名      エラーログ追加
    '* 
    '* 構文            Public Function InsertABErrLog(ByVal cABErrLogXClass As ABErrLogXClass) As Integer
    '* 
    '* 機能            エラーログの追加を行なう
    '* 
    '* 引数            cABErrLogXClass As ABErrLogXClass : 追加データ
    '* 
    '* 戻り値          Integer ： 追加したデータの件数
    '************************************************************************************************
    Public Function InsertABErrLog(ByVal cABErrLogXClass As ABErrLogXClass) As Integer

        Const THIS_METHOD_NAME As String = "InsertABErrLog"
        Dim cABAkibanShutokuBClass As ABAkibanShutokuBClass          ' エラーログ番号空番取得
        Dim cfErrorClass As UFErrorClass                             ' エラークラス
        Dim cfErrorStruct As UFErrorStruct                           ' エラー定義構造体
        Dim cfRdb As UFRdbClass
        Dim cfUFParameterClass As UFParameterClass
        Dim intCheckCnt As Integer
        Dim intInsCnt As Integer
        Dim strErrLogNo As String
        Dim strSystemDateTime As String
        Dim strSystemDate As String
        Dim strSystemTime As String

        Try
            ' 業務ＩＤを宛名(AB)に変更
            m_cfControlData.m_strBusinessId = "AB"

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                 "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                 "【実行メソッド名:Connect】")

            ' RDBクラスのインスタンス作成
            cfRdb = New UFRdbClass(m_cfControlData.m_strBusinessId)

            ' RDB接続
            cfRdb.Connect()

            ' 引数チェック
            ' 空白チェック
            If (cABErrLogXClass.p_strShichosonCD.Trim = String.Empty) Then          ' 市町村コード
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' 例外を生成
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "市町村コード】", cfErrorStruct.m_strErrorCode)
            End If

            ' 文字数チェック
            If (cABErrLogXClass.p_strShichosonCD.RLength > 6) Then                   ' 市町村コード
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001030)
                ' 例外を生成
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "市町村コード】", cfErrorStruct.m_strErrorCode)
            End If

            ' 数値チェック
            For intCheckCnt = 1 To Len(cABErrLogXClass.p_strShichosonCD)            ' 市町村コード
                If Not Mid(cABErrLogXClass.p_strShichosonCD, intCheckCnt, 1) Like "[0-9]" Then
                    cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001014)
                    ' 例外を生成
                    Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "市町村コード】", cfErrorStruct.m_strErrorCode)
                End If
            Next intCheckCnt

            ' 空白チェック
            If (cABErrLogXClass.p_strShoriID.Trim = String.Empty) Then              ' 処理ＩＤ
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' 例外を生成
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "処理ＩＤ】", cfErrorStruct.m_strErrorCode)
            End If

            ' 文字数チェック
            If (cABErrLogXClass.p_strShoriID.RLength > 5) Then                       ' 処理ＩＤ
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001030)
                ' 例外を生成
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "処理ＩＤ】", cfErrorStruct.m_strErrorCode)
            End If

            ' 半角チェック
            For intCheckCnt = 1 To Len(cABErrLogXClass.p_strShoriID)                ' 処理ＩＤ
                If Not Mid(cABErrLogXClass.p_strShoriID, intCheckCnt, 1) Like "[0-9a-zA-Z]" Then
                    cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001080)
                    ' 例外を生成
                    Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "処理ＩＤ】", cfErrorStruct.m_strErrorCode)
                End If
            Next intCheckCnt

            ' 空白チェック
            If (cABErrLogXClass.p_strShoriShu.Trim = String.Empty) Then             ' 処理種別
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' 例外を生成
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "処理種別】", cfErrorStruct.m_strErrorCode)
            End If

            ' 文字数チェック
            If (cABErrLogXClass.p_strShoriShu.RLength > 4) Then                      ' 処理種別
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001030)
                ' 例外を生成
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "処理種別】", cfErrorStruct.m_strErrorCode)
            End If

            ' 半角チェック
            For intCheckCnt = 1 To Len(cABErrLogXClass.p_strShoriShu)               ' 処理種別
                If Not Mid(cABErrLogXClass.p_strShoriShu, intCheckCnt, 1) Like "[0-9a-zA-Z]" Then
                    cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001080)
                    ' 例外を生成
                    Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "処理種別】", cfErrorStruct.m_strErrorCode)
                End If
            Next intCheckCnt

            ' 空白チェック
            If (cABErrLogXClass.p_strMsg5.Trim = String.Empty) Then                 ' メッセージ５（エラー発生場所）
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' 例外を生成
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "エラー発生場所】", cfErrorStruct.m_strErrorCode)
            End If

            ' 空白チェック
            If (cABErrLogXClass.p_strMsg6.Trim = String.Empty) Then                 ' メッセージ６（住民コード）
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' 例外を生成
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "住民コード】", cfErrorStruct.m_strErrorCode)
            End If

            ' 空白チェック
            If (cABErrLogXClass.p_strMsg7.Trim = String.Empty) Then                 ' メッセージ７（エラーメッセージ）
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' 例外を生成
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n【" + cfErrorStruct.m_strErrorMessage + "エラーメッセージ】", cfErrorStruct.m_strErrorCode)
            End If

            ' InsertSQL文の雛形を作成
            Call CreateInsertSQL()

            ' 空番取得クラスのインスタンス化
            cABAkibanShutokuBClass = New ABAkibanShutokuBClass(m_cfControlData, m_cfConfigDataClass)
            cABAkibanShutokuBClass.GetErrLogNo()

            ' エラーログ番号を取得
            strErrLogNo = cABAkibanShutokuBClass.p_strBango

            ' ＤＢ日時の取得
            strSystemDateTime = cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff")          ' ＤＢ日時
            strSystemDate = cfRdb.GetSystemDate.ToString("yyyyMMdd")                         ' ＤＢ日付
            strSystemTime = cfRdb.GetSystemDate.ToString("HHmmss")                           ' ＤＢ時間

            ' パラメータコレクションオブジェクトを作成
            m_cfInsParamCollection = New UFParameterCollectionClass

            ' 項目の編集
            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGNO                   ' ログ番号
            cfUFParameterClass.Value = strErrLogNo
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_YMD                  ' 開始年月日
            cfUFParameterClass.Value = strSystemDate
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_TIME                 ' 開始時間
            cfUFParameterClass.Value = strSystemTime
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORIID                 ' 処理ＩＤ
            cfUFParameterClass.Value = cABErrLogXClass.p_strShoriID.Trim
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORISHU                ' 処理種別
            cfUFParameterClass.Value = cABErrLogXClass.p_strShoriShu.Trim
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TAISHOKB                ' 対処区分
            cfUFParameterClass.Value = TAISHOKBN_MIKAKUNIN
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_JOKYOKB                 ' 状況区分
            cfUFParameterClass.Value = JOKYOKBN_ERR
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHINCHOKURITSU          ' 進捗率
            cfUFParameterClass.Value = String.Empty
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS1                    ' ステータス１
            cfUFParameterClass.Value = String.Empty
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS2                    ' ステータス２
            cfUFParameterClass.Value = String.Empty
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_YMD                  ' 終了年月日
            cfUFParameterClass.Value = String.Empty
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_TIME                 ' 終了時間
            cfUFParameterClass.Value = String.Empty
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG1                    ' メッセージ１
            ' 文字数チェック
            If (cABErrLogXClass.p_strMsg1.RLength > 8) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg1.RSubstring(0, 8).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg1.Trim
            End If
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG2                    ' メッセージ２
            If (cABErrLogXClass.p_strMsg2.RLength > 8) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg2.RSubstring(0, 8).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg2.Trim
            End If
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG3                    ' メッセージ３
            If (cABErrLogXClass.p_strMsg3.RLength > 8) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg3.RSubstring(0, 8).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg3.Trim
            End If
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG4                    ' メッセージ４
            If (cABErrLogXClass.p_strMsg4.RLength > 8) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg4.RSubstring(0, 8).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg4.Trim
            End If
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG5                    ' メッセージ５
            If (cABErrLogXClass.p_strMsg5.RLength > 15) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg5.RSubstring(0, 15).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg5.Trim
            End If
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG6                    ' メッセージ６
            If (cABErrLogXClass.p_strMsg6.RLength > 40) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg6.RSubstring(0, 40).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg6.Trim
            End If
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG7                    ' メッセージ７
            If (cABErrLogXClass.p_strMsg7.RLength > 100) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg7.RSubstring(0, 100).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg7.Trim
            End If
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG8                    ' メッセージ８
            If (cABErrLogXClass.p_strMsg8.RLength > 120) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg8.RSubstring(0, 120).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg8.Trim
            End If
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGFILEMEI              ' ログファイル名
            cfUFParameterClass.Value = String.Empty
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHICHOSONCD             ' 市町村コード
            cfUFParameterClass.Value = cABErrLogXClass.p_strShichosonCD.Trim
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KYUSHICHOSONCD          ' 旧市町村コード
            cfUFParameterClass.Value = cABErrLogXClass.p_strShichosonCD.Trim
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_RESERVE30BYTE           ' リザーブ
            cfUFParameterClass.Value = String.Empty
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TANMATSUID              ' 端末ＩＤ
            cfUFParameterClass.Value = m_cfControlData.m_strClientId
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUJOFG                ' 削除フラグ
            cfUFParameterClass.Value = "0"
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINCOUNTER           ' 更新カウンタ
            cfUFParameterClass.Value = Decimal.Zero
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEINICHIJI          ' 作成日時
            cfUFParameterClass.Value = strSystemDateTime
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEIUSER             ' 作成ユーザー
            cfUFParameterClass.Value = m_cfControlData.m_strUserId
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINNICHIJI           ' 更新日時
            cfUFParameterClass.Value = strSystemDateTime
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' パラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINUSER              ' 更新ユーザー
            cfUFParameterClass.Value = m_cfControlData.m_strUserId
            ' パラメータコレクションオブジェクトにパラメータオブジェクトの追加
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                 "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                 "【実行メソッド名:INSERT】" + _
                                 "【SQL内容:" + cfRdb.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsParamCollection) + "】")

            ' SQL実行
            intInsCnt = cfRdb.ExecuteSQL(m_strInsertSQL, m_cfInsParamCollection)

        Catch objRdbExp As UFRdbException                          ' RdbExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニング内容:" + objRdbExp.Message + "】")
            ' ワーニングをスローする
            Throw objRdbExp

        Catch objRdbDeadLockExp As UFRdbDeadLockException          ' デッドロックをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニングコード:" + objRdbDeadLockExp.p_strErrorCode + "】" + _
                                     "【ワーニング内容:" + objRdbDeadLockExp.Message + "】")
            ' ワーニングをスローする
            Throw objRdbDeadLockExp

        Catch objUFRdbUniqueExp As UFRdbUniqueException            ' 一意制約違反をキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニングコード:" + objUFRdbUniqueExp.p_strErrorCode + "】" + _
                                     "【ワーニング内容:" + objUFRdbUniqueExp.Message + "】")
            ' ワーニングをスローする
            Throw objUFRdbUniqueExp

        Catch objRdbTimeOutExp As UFRdbTimeOutException            ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + _
                                     "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
            ' ワーニングをスローする
            Throw objRdbTimeOutExp

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                     "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception                             ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                   "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                   "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                   "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        Finally
            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                 "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                 "【実行メソッド名:Disconnect】")

            ' RDB切断
            cfRdb.Disconnect()

            ' 元のビジネスIDを入れる
            m_cfControlData.m_strBusinessId = m_strRsBusinId

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        End Try

        ' 戻り値設定
        Return intInsCnt

    End Function

#End Region

#Region "InsertSQL文の雛形を作成"
    '************************************************************************************************
    '* メソッド名      InsertSQL文の雛形を作成
    '* 
    '* 構文            Private Sub CreateInsertSQL()
    '* 
    '* 機能　　    　　InsertSQLの雛型とパラメータコレクションを作成する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub CreateInsertSQL()

        Const THIS_METHOD_NAME As String = "CreateInsertSQL"
        Dim cfUFParameterClass As UFParameterClass
        Dim strInsertColumn As New StringBuilder
        Dim strInsertParam As New StringBuilder
        Dim strInsertSQL As New StringBuilder

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' InsertSQL文の作成
            strInsertSQL.Append("INSERT INTO ")
            strInsertSQL.Append(ABErrLogEntity.TABLE_NAME)
            strInsertSQL.Append(" ")

            ' INSERTパラメータコレクションクラスのインスタンス化
            m_cfInsParamCollection = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            cfUFParameterClass = New UFParameterClass

            ' InsertSQL文の作成
            strInsertColumn.Append(ABErrLogEntity.LOGNO)                   ' ログ番号
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.ST_YMD)                  ' 開始年月日
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.ST_TIME)                 ' 開始時間
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SHORIID)                 ' 処理ＩＤ
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SHORISHU)                ' 処理種別
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.TAISHOKB)                ' 対処区分
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.JOKYOKB)                 ' 状況区分
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SHINCHOKURITSU)          ' 進捗率
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.STS1)                    ' ステータス１
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.STS2)                    ' ステータス２
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.ED_YMD)                  ' 終了年月日
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.ED_TIME)                 ' 終了時間
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG1)                    ' メッセージ１
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG2)                    ' メッセージ２
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG3)                    ' メッセージ３
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG4)                    ' メッセージ４
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG5)                    ' メッセージ５
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG6)                    ' メッセージ６
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG7)                    ' メッセージ７
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG8)                    ' メッセージ８
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.LOGFILEMEI)              ' ログファイル名
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SHICHOSONCD)             ' 市町村コード
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.KYUSHICHOSONCD)          ' 旧市町村コード
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.RESERVE30BYTE)           ' リザーブ
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.TANMATSUID)              ' 端末ＩＤ
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SAKUJOFG)                ' 削除フラグ
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.KOSHINCOUNTER)           ' 更新カウンタ
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SAKUSEINICHIJI)          ' 作成日時
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SAKUSEIUSER)             ' 作成ユーザー
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.KOSHINNICHIJI)           ' 更新日時
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.KOSHINUSER)              ' 更新ユーザー

            strInsertParam.Append(ABErrLogEntity.KEY_LOGNO)                   ' ログ番号
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_ST_YMD)                  ' 開始年月日
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_ST_TIME)                 ' 開始時間
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SHORIID)                 ' 処理ＩＤ
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SHORISHU)                ' 処理種別
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_TAISHOKB)                ' 対処区分
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_JOKYOKB)                 ' 状況区分
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SHINCHOKURITSU)          ' 進捗率
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_STS1)                    ' ステータス１
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_STS2)                    ' ステータス２
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_ED_YMD)                  ' 終了年月日
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_ED_TIME)                 ' 終了時間
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG1)                    ' メッセージ１
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG2)                    ' メッセージ２
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG3)                    ' メッセージ３
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG4)                    ' メッセージ４
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG5)                    ' メッセージ５
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG6)                    ' メッセージ６
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG7)                    ' メッセージ７
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG8)                    ' メッセージ８
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_LOGFILEMEI)              ' ログファイル名
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SHICHOSONCD)             ' 市町村コード
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_KYUSHICHOSONCD)          ' 旧市町村コード
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_RESERVE30BYTE)           ' リザーブ
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_TANMATSUID)              ' 端末ＩＤ
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SAKUJOFG)                ' 削除フラグ
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_KOSHINCOUNTER)           ' 更新カウンタ
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SAKUSEINICHIJI)          ' 作成日時
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SAKUSEIUSER)             ' 作成ユーザー
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_KOSHINNICHIJI)           ' 更新日時
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_KOSHINUSER)              ' 更新ユーザー

            ' INSERTコレクションにパラメータを追加
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGNO                   ' ログ番号
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_YMD                  ' 開始年月日
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_TIME                 ' 開始時間
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORIID                 ' 処理ＩＤ
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORISHU                ' 処理種別
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TAISHOKB                ' 対処区分
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_JOKYOKB                 ' 状況区分
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHINCHOKURITSU          ' 進捗率
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS1                    ' ステータス１
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS2                    ' ステータス２
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_YMD                  ' 終了年月日
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_TIME                 ' 終了時間
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG1                    ' メッセージ１
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG2                    ' メッセージ２
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG3                    ' メッセージ３
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG4                    ' メッセージ４
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG5                    ' メッセージ５
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG6                    ' メッセージ６
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG7                    ' メッセージ７
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG8                    ' メッセージ８
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGFILEMEI              ' ログファイル名
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHICHOSONCD             ' 市町村コード
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KYUSHICHOSONCD          ' 旧市町村コード
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_RESERVE30BYTE           ' リザーブ
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TANMATSUID              ' 端末ＩＤ
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUJOFG                ' 削除フラグ
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINCOUNTER           ' 更新カウンタ
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEINICHIJI          ' 作成日時
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEIUSER             ' 作成ユーザー
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINNICHIJI           ' 更新日時
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINUSER              ' 更新ユーザー
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' InsertSQL文の結合
            strInsertSQL.Append("(")
            strInsertSQL.Append(strInsertColumn)
            strInsertSQL.Append(")")
            strInsertSQL.Append(" VALUES (")
            strInsertSQL.Append(strInsertParam)
            strInsertSQL.Append(")")

            ' String型に変換
            m_strInsertSQL = strInsertSQL.ToString

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                     "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                     "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                     "【ワーニング内容:" + exAppException.Message + "】")
            ' エラーをそのままスローする
            Throw exAppException

        Catch exException As Exception          ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                   "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                   "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                   "【エラー内容:" + exException.Message + "】")
            ' エラーをそのままスローする
            Throw exException

        Finally
            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        End Try

    End Sub

#End Region

End Class
