'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        宛名印鑑マスタ更新(ABAtenaInkanupBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/11/11　吉澤　行宣
'* 
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2004/03/26 000001     ビジネスIDの変更修正 
'* 2007/03/16 000002     エラーを取得する個所の変更とABLOGへ書き込む処理の追加(高原)
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports System.Text
Imports Densan.FrameWork.Tools
Imports Densan.Common


Public Class ABAtenaInkanupBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfABConfigDataClass As UFConfigDataClass        ' コンフィグデータAB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' コンフィグデータAA
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strRsBusiId As String
 
    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaInkanupBClass"
    Private Const AA_BUSSINESS_ID As String = "AA"            ' 業務コード

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData As UFControlData,
    '* 　　                           ByVal cfConfigDataClass As UFConfigDataClass,
    '* 　　                           ByVal cfRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
    '* 　　            cfConfigDataClass As UFConfigDataClass : コンフィグデータオブジェクト 
    '* 　　            cfRdbClass As UFRdbClass               : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        Dim cfAAUFConfigData As UFConfigDataClass
        Dim cfAAUFConfigClass As UFConfigClass
       
        'コンフィグデータの"AA"の環境情報を取得
        cfAAUFConfigClass = New UFConfigClass()
        cfAAUFConfigData = cfAAUFConfigClass.GetConfig(AA_BUSSINESS_ID)
        m_cfAAConfigDataClass = cfAAUFConfigData

        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfABConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfABConfigDataClass, m_cfControlData.m_strBusinessId)

        '受け取ったビジネスIDをメンバへ保存
        m_strRsBusiId = m_cfControlData.m_strBusinessId

        '*履歴番号 000001 2004/03/26 削除開始
        ''業務IDを宛名(AB)に変更
        'm_cfControlData.m_strBusinessId = "AB"
        '*履歴番号 000001 2004/03/26 削除終了

    End Sub

#End Region

    '************************************************************************************************
    '* メソッド名     宛名印鑑マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty As ABKobetsuInkanProperty) As Integer
    '* 
    '* 機能　　    　  宛名印鑑マスタのデータを更新する。
    '* 
    '* 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty() As ABKobetsuInkanProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaInkan"
        Dim intUpdCnt As Integer
        Dim cABAtenaInkanBClass As ABAtenaInkanBClass
        Dim cAAKOBETSUINKANParamClass() As localhost.AAKOBETSUINKANParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaInkanEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass
        Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAppExp As UFAppException
        Dim intcnt As Integer
       
        Try

            '*履歴番号 000001 2004/03/26 追加開始
            '業務IDを宛名(AB)に変更
            m_cfControlData.m_strBusinessId = "AB"
            '*履歴番号 000001 2004/03/26 追加終了

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '市町村情報取得（市町村コード)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '宛名印鑑ＤＡクラスのインスタンス化
            cABAtenaInkanBClass = New ABAtenaInkanBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            Try
                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '宛名印鑑マスタ抽出呼び出し
                    csABAtenaInkanEntity = cABAtenaInkanBClass.GetAtenaInkan(CStr(cABKobetsuProperty(intcnt).p_strJUMINCD))

                    '追加・更新の判定
                    If csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows.Count = 0 Then

                        cDatRow = csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).NewRow()
                        '各項目をプロパティから取得
                        cDatRow.Item(ABAtenaInkanEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                        cDatRow.Item(ABAtenaInkanEntity.INKANNO) = cABKobetsuProperty(intcnt).p_strINKANNO
                        cDatRow.Item(ABAtenaInkanEntity.INKANTOROKUKB) = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

                        '市町村コード
                        cDatRow.Item(ABAtenaInkanEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                        '旧市町村コード
                        cDatRow.Item(ABAtenaInkanEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                        'データの追加
                        'csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows.Add(cDatRow)

                        '宛名印鑑マスタ追加メソッド呼び出し
                        intUpdCnt = cABAtenaInkanBClass.InsertAtenaInkan(cDatRow)
                    Else

                        cDatRow = csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows(0)
                        '各項目をプロパティから取得
                        cDatRow.Item(ABAtenaInkanEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                        cDatRow.Item(ABAtenaInkanEntity.INKANNO) = cABKobetsuProperty(intcnt).p_strINKANNO
                        cDatRow.Item(ABAtenaInkanEntity.INKANTOROKUKB) = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

                        '市町村コード
                        cDatRow.Item(ABAtenaInkanEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                        '旧市町村コード
                        cDatRow.Item(ABAtenaInkanEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                        '宛名印鑑マスタ更新メソッド呼び出し
                        intUpdCnt = cABAtenaInkanBClass.UpdateAtenaInkan(cDatRow)
                    End If

                    '追加・更新件数が0件の時メッセージ"宛名の個別事項の更新は正常に行えませんでした"を返す
                    If intUpdCnt = 0 Then

                        cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                        'エラー定義を取得
                        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
                        '例外を生成
                        csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        Throw csAppExp
                    End If

                Next

                '*履歴番号 000002 2007/03/16 追加開始
            Catch exAppExp As UFAppException                   ' UFAppExceptionをキャッチ
                ' ※通常のエラーをログファイルに書き込み
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppExp.Message + "】")

                ' ※ログファイル書き込み後、連携エラー用メッセージを作成
                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                'エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
                ' ※ABLOGへ書き込み
                SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "個別記載更新（印鑑）", _
                         cABKobetsuProperty(intcnt).p_strJUMINCD, objErrorStruct.m_strErrorMessage)

                Throw exAppExp
            Catch exExp As Exception                           ' Exceptionをキャッチ
                ' ※通常のエラーをログファイルに書き込み
                ' エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exExp.Message + "】")
                ' ※ログファイル書き込み後、連携エラー用メッセージを作成
                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                'エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
                ' ※ABLOGへ書き込み
                SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "個別記載更新（印鑑）", _
                         cABKobetsuProperty(intcnt).p_strJUMINCD, objErrorStruct.m_strErrorMessage)

                Throw exExp
            End Try
            '*履歴番号 000002 2007/03/16 追加終了


            Try
                'WebserviceのURLをWebConfigから取得して設定する
                cAACommonBSClass = New localhost.AACommonBSClass
                cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                ReDim cAAKOBETSUINKANParamClass(cABKobetsuProperty.Length - 1)

                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '個別印鑑パラメータのインスタンス化
                    cAAKOBETSUINKANParamClass(intcnt) = New localhost.AAKOBETSUINKANParamClass

                    '更新・追加した項目を取得
                    cAAKOBETSUINKANParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                    cAAKOBETSUINKANParamClass(intcnt).m_strINKANNO = CStr(cABKobetsuProperty(intcnt).p_strINKANNO)
                    cAAKOBETSUINKANParamClass(intcnt).m_strINKANTOROKUKB = CStr(cABKobetsuProperty(intcnt).p_strINKANTOROKUKB)
                Next

                ' 住基個別印鑑更新メソッドを実行する
                strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                intUpdCnt = cAACommonBSClass.UpdateKBINKAN(strControlData, cAAKOBETSUINKANParamClass)

                '追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                If Not (intUpdCnt = cABKobetsuProperty.Length) Then

                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    'エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    '例外を生成
                    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    Throw csAppExp

                End If

            Catch objSoapExp As Web.Services.Protocols.SoapException             ' SoapExceptionをキャッチ
                ' OuterXmlにエラー内容が格納してある。
                Dim objExpTool As UFExceptionTool = New UFExceptionTool(objSoapExp.Detail.OuterXml)
                Dim objErr As UFErrorStruct

                ' アプリケーション例外かどうかの判定
                If (objExpTool.IsAppException = True) Then
                    ' ワーニングログ出力
                    m_cfLogClass.WarningWrite(m_cfControlData, _
                                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                            "【ワーニングコード:" + objExpTool.p_strErrorCode + "】" + _
                                            "【ワーニング内容:" + objExpTool.p_strErrorMessage + "】")

                    ' 付加メッセージを作成する
                    Dim strExtMsg As String = "<P>対象住民のリカバリ処理を行ってください。<BR>"

                    ' アプリケーション例外を作成する
                    Dim objAppExp As UFAppException
                    objAppExp = New UFAppException(objExpTool.p_strErrorMessage + strExtMsg, objExpTool.p_strErrorCode)

                    ' 拡張領域のメッセージにも付加（実際にはここのメッセージが表示される）
                    UFErrorToolClass.ErrorStructSetStr(objErr, objExpTool.p_strExt)
                    objErr.m_strErrorMessage += strExtMsg
                    objAppExp.p_strExt = UFErrorToolClass.ErrorStructGetStr(objErr)
                    ' メッセージを付加しない場合は以下
                    'objAppExp.p_strExt = objExpTool.p_strExt
                    '*履歴番号 000002 2007/03/16 追加開始
                    ' ※ログファイル書き込み後、連携エラー用メッセージを作成
                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    'エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    ' ※ABLOGへ書き込み
                    ' ※　注　※　引数で住民コードを渡す個所ですが、cABKobetsuPropertyが複数であっても
                    ' 　　　　　　ＡＡから戻ってきたエラーでは何番目で落ちたか判断できないので、以下固定でIndex(0)を渡します。
                    SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "個別記載更新（印鑑）", _
                             cABKobetsuProperty(0).p_strJUMINCD, objErrorStruct.m_strErrorMessage)
                    '*履歴番号 000002 2007/03/16 追加終了

                    Throw objAppExp
                Else
                    ' システム例外の場合
                    ' エラーログ出力
                    m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExpTool.p_strErrorMessage + "】")

                    '*履歴番号 000002 2007/03/16 追加開始
                    ' ※ログファイル書き込み後、連携エラー用メッセージを作成
                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    'エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    ' ※ABLOGへ書き込み
                    SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "個別記載更新（印鑑）", _
                             cABKobetsuProperty(0).p_strJUMINCD, objErrorStruct.m_strErrorMessage)
                    '*履歴番号 000002 2007/03/16 追加終了
                    Throw objSoapExp
                End If
            Catch exAppExp As UFAppException                   ' UFAppExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppExp.Message + "】")

                '*履歴番号 000002 2007/03/16 追加開始
                ' ※ログファイル書き込み後、連携エラー用メッセージを作成
                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                'エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                ' ※ABLOGへ書き込み
                SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "個別記載更新（印鑑）", _
                         cABKobetsuProperty(0).p_strJUMINCD, objErrorStruct.m_strErrorMessage)
                '*履歴番号 000002 2007/03/16 追加終了

                Throw exAppExp
            Catch exExp As Exception                           ' Exceptionをキャッチ
                ' エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exExp.Message + "】")

                '*履歴番号 000002 2007/03/16 追加開始
                ' ※ログファイル書き込み後、連携エラー用メッセージを作成
                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                'エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                ' ※ABLOGへ書き込み
                SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "個別記載更新（印鑑）", _
                         cABKobetsuProperty(0).p_strJUMINCD, objErrorStruct.m_strErrorMessage)
                '*履歴番号 000002 2007/03/16 追加終了

                Throw exExp
            End Try
        Catch
            Throw
        Finally
            '元のビジネスIDを入れる
            m_cfControlData.m_strBusinessId = m_strRsBusiId
            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        End Try

        Return intUpdCnt

    End Function

    '*履歴番号 000002 2007/03/16 追加開始
#Region "宛名更新エラーログSET"
    '************************************************************************************************
    '* メソッド名     宛名更新エラーログSET処理
    '* 
    '* 構文           SetABLOG(ByVal strShichosonCD As String, _
    '* 　　                    ByVal strShoriID As String, _
    '* 　　                    ByVal strShoriShu As String, _
    '* 　　                    ByVal strBasho As String, _
    '* 　　                    ByVal strJuminCD As String, _
    '* 　　                    ByVal strErrMsg As String)
    '* 
    '* 機能           ABLOG用エラーメッセージをSETする
    '* 
    '* 引数           ByVal strShichosonCD As String : 市町村コード
    '* 　　           ByVal strShoriID as string     : 処理ＩＤ
    '* 　　           ByVal strShoriShu As String    : 処理種別
    '* 　　           ByVal strBasho As String       : エラー発生場所
    '* 　　           ByVal strJuminCD As String     : 該当住民コード
    '* 　　           ByVal strErrMsg As String      : エラーメッセージ
    '* 
    '* 戻り値         Dim intCnt As Integer          : エラー追加件数
    '************************************************************************************************
    Private Function SetABLOG(ByVal strShichosonCD As String, _
                              ByVal strShoriID As String, _
                              ByVal strShoriShu As String, _
                              ByVal strBasho As String, _
                              ByVal strJuminCD As String, _
                              ByVal strErrMsg As String) As Integer
        Dim cABErrLog As ABErrLogBClass
        Dim cABErrLogPrm As ABErrLogXClass
        Dim intCnt As Integer

        cABErrLog = New ABErrLogBClass(m_cfControlData, m_cfABConfigDataClass)
        cABErrLogPrm = New ABErrLogXClass

        ' 各種項目をパラメータにセット
        cABErrLogPrm.p_strShichosonCD = strShichosonCD
        cABErrLogPrm.p_strShoriID = strShoriID
        cABErrLogPrm.p_strShoriShu = strShoriShu
        cABErrLogPrm.p_strMsg5 = strBasho
        cABErrLogPrm.p_strMsg6 = strJuminCD
        cABErrLogPrm.p_strMsg7 = strErrMsg

        intCnt = cABErrLog.InsertABErrLog(cABErrLogPrm)

        Return intCnt

    End Function

#End Region
    '*履歴番号 000002 2007/03/16 追加終了

    '*履歴番号 000002 2007/03/16 削除開始
    ' ※Try-Catchの作りを大幅に変えるので旧ソースをそのまま残しておきます。
#Region "旧ソース UpdateAtenaInkan"
    ''************************************************************************************************
    ''* メソッド名     宛名印鑑マスタ更新
    ''* 
    ''* 構文           Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty As ABKobetsuInkanProperty) As Integer
    ''* 
    ''* 機能　　    　  宛名印鑑マスタのデータを更新する。
    ''* 
    ''* 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
    ''* 
    ''* 戻り値         更新件数(Integer)
    ''************************************************************************************************
    'Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty() As ABKobetsuInkanProperty) As Integer
    '    Const THIS_METHOD_NAME As String = "UpdateAtenaInkan"
    '    Dim intUpdCnt As Integer
    '    Dim cABAtenaInkanBClass As ABAtenaInkanBClass
    '    Dim cAAKOBETSUINKANParamClass() As localhost.AAKOBETSUINKANParamClass
    '    Dim cAACommonBSClass As localhost.AACommonBSClass
    '    Dim csABAtenaInkanEntity As DataSet
    '    Dim cDatRow As DataRow
    '    Dim strControlData As String
    '    Dim cUSSCItyInfo As New USSCityInfoClass()
    '    Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
    '    Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
    '    Dim csAppExp As UFAppException
    '    Dim intcnt As Integer

    '    Try

    '        '*履歴番号 000001 2004/03/26 追加開始
    '        '業務IDを宛名(AB)に変更
    '        m_cfControlData.m_strBusinessId = "AB"
    '        '*履歴番号 000001 2004/03/26 追加終了

    '        ' デバッグログ出力
    '        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        '市町村情報取得（市町村コード)
    '        cUSSCItyInfo.GetCityInfo(m_cfControlData)

    '        '宛名印鑑ＤＡクラスのインスタンス化
    '        cABAtenaInkanBClass = New ABAtenaInkanBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

    '        For intcnt = 0 To cABKobetsuProperty.Length - 1

    '            '宛名印鑑マスタ抽出呼び出し
    '            csABAtenaInkanEntity = cABAtenaInkanBClass.GetAtenaInkan(CStr(cABKobetsuProperty(intcnt).p_strJUMINCD))

    '            '追加・更新の判定
    '            If csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows.Count = 0 Then

    '                cDatRow = csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).NewRow()
    '                '各項目をプロパティから取得
    '                cDatRow.Item(ABAtenaInkanEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
    '                cDatRow.Item(ABAtenaInkanEntity.INKANNO) = cABKobetsuProperty(intcnt).p_strINKANNO
    '                cDatRow.Item(ABAtenaInkanEntity.INKANTOROKUKB) = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

    '                '市町村コード
    '                cDatRow.Item(ABAtenaInkanEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
    '                '旧市町村コード
    '                cDatRow.Item(ABAtenaInkanEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

    '                'データの追加
    '                'csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows.Add(cDatRow)

    '                '宛名印鑑マスタ追加メソッド呼び出し
    '                intUpdCnt = cABAtenaInkanBClass.InsertAtenaInkan(cDatRow)
    '            Else

    '                cDatRow = csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows(0)
    '                '各項目をプロパティから取得
    '                cDatRow.Item(ABAtenaInkanEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
    '                cDatRow.Item(ABAtenaInkanEntity.INKANNO) = cABKobetsuProperty(intcnt).p_strINKANNO
    '                cDatRow.Item(ABAtenaInkanEntity.INKANTOROKUKB) = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

    '                '市町村コード
    '                cDatRow.Item(ABAtenaInkanEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
    '                '旧市町村コード
    '                cDatRow.Item(ABAtenaInkanEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

    '                '宛名印鑑マスタ更新メソッド呼び出し
    '                intUpdCnt = cABAtenaInkanBClass.UpdateAtenaInkan(cDatRow)
    '            End If

    '            '追加・更新件数が0件の時メッセージ"宛名の個別事項の更新は正常に行えませんでした"を返す
    '            If intUpdCnt = 0 Then

    '                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
    '                'エラー定義を取得
    '                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
    '                '例外を生成
    '                csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
    '                Throw csAppExp
    '            End If

    '        Next

    '        'WebserviceのURLをWebConfigから取得して設定する
    '        cAACommonBSClass = New localhost.AACommonBSClass()
    '        cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
    '        'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

    '        ReDim cAAKOBETSUINKANParamClass(cABKobetsuProperty.Length - 1)

    '        For intcnt = 0 To cABKobetsuProperty.Length - 1

    '            '個別印鑑パラメータのインスタンス化
    '            cAAKOBETSUINKANParamClass(intcnt) = New localhost.AAKOBETSUINKANParamClass()

    '            '更新・追加した項目を取得
    '            cAAKOBETSUINKANParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
    '            cAAKOBETSUINKANParamClass(intcnt).m_strINKANNO = CStr(cABKobetsuProperty(intcnt).p_strINKANNO)
    '            cAAKOBETSUINKANParamClass(intcnt).m_strINKANTOROKUKB = CStr(cABKobetsuProperty(intcnt).p_strINKANTOROKUKB)
    '        Next

    '        ' 住基個別印鑑更新メソッドを実行する
    '        strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
    '        intUpdCnt = cAACommonBSClass.UpdateKBINKAN(strControlData, cAAKOBETSUINKANParamClass)

    '        '追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
    '        If Not (intUpdCnt = cABKobetsuProperty.Length) Then

    '            cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
    '            'エラー定義を取得
    '            objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
    '            '例外を生成
    '            csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
    '            Throw csAppExp

    '        End If

    '    Catch objSoapExp As Web.Services.Protocols.SoapException             ' SoapExceptionをキャッチ
    '        ' OuterXmlにエラー内容が格納してある。
    '        Dim objExpTool As UFExceptionTool = New UFExceptionTool(objSoapExp.Detail.OuterXml)
    '        Dim objErr As UFErrorStruct

    '        ' アプリケーション例外かどうかの判定
    '        If (objExpTool.IsAppException = True) Then
    '            ' ワーニングログ出力
    '            m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
    '                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    '                                    "【ワーニングコード:" + objExpTool.p_strErrorCode + "】" + _
    '                                    "【ワーニング内容:" + objExpTool.p_strErrorMessage + "】")

    '            ' 付加メッセージを作成する
    '            Dim strExtMsg As String = "<P>対象住民のリカバリ処理を行ってください。<BR>"

    '            ' アプリケーション例外を作成する
    '            Dim objAppExp As UFAppException
    '            objAppExp = New UFAppException(objExpTool.p_strErrorMessage + strExtMsg, objExpTool.p_strErrorCode)

    '            ' 拡張領域のメッセージにも付加（実際にはここのメッセージが表示される）
    '            UFErrorToolClass.ErrorStructSetStr(objErr, objExpTool.p_strExt)
    '            objErr.m_strErrorMessage += strExtMsg
    '            objAppExp.p_strExt = UFErrorToolClass.ErrorStructGetStr(objErr)
    '            ' メッセージを付加しない場合は以下
    '            'objAppExp.p_strExt = objExpTool.p_strExt

    '            Throw objAppExp
    '        Else
    '            ' システム例外の場合
    '            ' エラーログ出力
    '            m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
    '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    '                                "【エラー内容:" + objExpTool.p_strErrorMessage + "】")
    '            Throw objSoapExp
    '        End If
    '    Catch exAppExp As UFAppException                   ' UFAppExceptionをキャッチ
    '        ' ワーニングログ出力
    '        m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
    '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    '                                "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + _
    '                                "【ワーニング内容:" + exAppExp.Message + "】")
    '        Throw exAppExp
    '    Catch exExp As Exception                           ' Exceptionをキャッチ
    '        ' エラーログ出力
    '        m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                "【クラス名:" + THIS_CLASS_NAME + "】" + _
    '                                "【メソッド名:" + THIS_METHOD_NAME + "】" + _
    '                                "【エラー内容:" + exExp.Message + "】")
    '        Throw exExp
    '    Finally
    '        '元のビジネスIDを入れる
    '        m_cfControlData.m_strBusinessId = m_strRsBusiId
    '        ' デバッグログ出力
    '        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '    End Try

    '    Return intUpdCnt

    'End Function
#End Region
    '*履歴番号 000002 2007/03/16 削除終了
End Class
