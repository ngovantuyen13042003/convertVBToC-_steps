'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        宛名児手マスタ更新(ABAtenaJiteupBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/11/11　吉澤　行宣
'* 
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2004/03/26 000001     ビジネスIDの変更修正 
'* 2005/10/13 000002     上田市ホスト連携（ワークフロー）処理を追加(マルゴ村山)
'* 2005/10/25 000003     上田市ホスト連携（ワークフロー）処理を修正(マルゴ村山)
'* 2005/12/01 000004     住基の個別事項更新結果を評価するかしないかの処理を追加
'* 2010/04/09 000005     管理情報により住基個別事項の更新を制御する（比嘉）
'* 2010/04/16 000006     VS2008対応（比嘉）
'* 2022/12/16 000007    【AB-8010】住民コード世帯コード15桁対応(下村)
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
'*履歴番号 000002 2005/10/13 追加開始
Imports Densan.WorkFlow.UWCommon
'*履歴番号 000002 2005/10/13 追加終了

Public Class ABAtenaJiteupBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfABConfigDataClass As UFConfigDataClass        ' コンフィグデータAB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' コンフィグデータAA
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strRsBusiId As String

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaJiteupBClass"
    Private Const AA_BUSSINESS_ID As String = "AA"                              ' 業務コード
    '*履歴番号 000002 2005/10/13 追加開始
    Private Const WORK_FLOW_NAME As String = "宛名児手個別事項"         ' ワークフロー名
    Private Const DATA_NAME As String = "児手個別"                      ' データ名
    '*履歴番号 000002 2005/10/13 追加終了
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
    '* メソッド名     宛名児手マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaJite(ByVal cABKobetsuProperty As ABKobetsuJiteProperty) As Integer
    '* 
    '* 機能　　    　  宛名児手マスタのデータを更新する。
    '* 
    '* 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaJite(ByVal cABKobetsuProperty() As ABKobetsuJiteProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaJite"
        Dim intUpdCnt As Integer
        Dim cABAtenaJiteBClass As ABAtenaJiteBClass
        Dim cAAKOBETSUJITEParamClass(0) As localhost.AAKOBETSUJITEParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaJiteEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()
        Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAppExp As UFAppException
        '*履歴番号 000002 2005/10/13 追加開始
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '宛名管理情報ＤＡビジネスクラス
        Dim csAtenaKanriEntity As DataSet                   '宛名管理情報データセット
        '*履歴番号 000002 2005/10/13 追加終了
        '*履歴番号 000004 2005/12/01 追加開始
        Dim strJukiResult As String                         '住基の結果をチェックするかどうか(0:する 1:しない)
        '*履歴番号 000004 2005/12/01 追加終了

        Try

            '*履歴番号 000001 2004/03/26 追加開始
            '業務IDを宛名(AB)に変更
            m_cfControlData.m_strBusinessId = "AB"
            '*履歴番号 000001 2004/03/26 追加終了

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '市町村情報取得（市町村コード)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '宛名児手ＤＡクラスのインスタンス化
            cABAtenaJiteBClass = New ABAtenaJiteBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            Dim intcnt As Integer
            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '宛名児手マスタ抽出呼び出し
                csABAtenaJiteEntity = cABAtenaJiteBClass.GetAtenaJite(cABKobetsuProperty(intcnt).p_strJUMINCD)

                '追加・更新の判定
                If csABAtenaJiteEntity.Tables(ABAtenaJiteEntity.TABLE_NAME).Rows.Count = 0 Then

                    cDatRow = csABAtenaJiteEntity.Tables(ABAtenaJiteEntity.TABLE_NAME).NewRow()
                    '各項目をプロパティから取得
                    cDatRow.Item(ABAtenaJiteEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATEHIYOKB) = cABKobetsuProperty(intcnt).p_strHIYOKB
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATESTYM) = cABKobetsuProperty(intcnt).p_strJIDOTEATESTYM
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATEEDYM) = cABKobetsuProperty(intcnt).p_strJIDOTEATEEDYM

                    '市町村コード
                    cDatRow.Item(ABAtenaJiteEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '旧市町村コード
                    cDatRow.Item(ABAtenaJiteEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    'データの追加
                    'csABAtenaJiteEntity.Tables(ABAtenaJiteEntity.TABLE_NAME).Rows.Add(cDatRow)

                    '宛名児手マスタ追加メソッド呼び出し
                    intUpdCnt = cABAtenaJiteBClass.InsertAtenaJite(cDatRow)
                Else

                    cDatRow = csABAtenaJiteEntity.Tables(ABAtenaJiteEntity.TABLE_NAME).Rows(0)
                    '各項目をプロパティから取得
                    cDatRow.Item(ABAtenaJiteEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATEHIYOKB) = cABKobetsuProperty(intcnt).p_strHIYOKB
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATESTYM) = cABKobetsuProperty(intcnt).p_strJIDOTEATESTYM
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATEEDYM) = cABKobetsuProperty(intcnt).p_strJIDOTEATEEDYM
                    '市町村コード
                    cDatRow.Item(ABAtenaJiteEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '旧市町村コード
                    cDatRow.Item(ABAtenaJiteEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '宛名児手マスタ更新メソッド呼び出し
                    intUpdCnt = cABAtenaJiteBClass.UpdateAtenaJite(cDatRow)
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

            '*履歴番号 000005 2010/04/09 修正開始
            ' 宛名管理情報Ｂクラスのインスタンス作成
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '  宛名管理情報の種別キー:04,識別キー:16のデータを取得する
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "16")

            '管理情報の住基更新レコードが存在しない、または、パラメータが"0"の時だけ住基更新処理を行う
            If (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) OrElse _
               (CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "0") Then

                ' 宛名管理情報の種別04識別キー24のデータを取得する(住基側の更新処理の結果を判断するかどうか)
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "24")
                ' 管理情報にレコードが存在し、パラメータが"1"の時はチェックしない
                If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                    If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                        ' ﾊﾟﾗﾒｰﾀが"1"のときはチェックしない
                        strJukiResult = "1"
                    Else
                        ' ﾊﾟﾗﾒｰﾀが"1"のときはチェックする
                        strJukiResult = "0"
                    End If
                Else
                    ' レコードがないときはチェックする
                    strJukiResult = "0"
                End If

                'WebserviceのURLをWebConfigから取得して設定する
                cAACommonBSClass = New localhost.AACommonBSClass
                cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                ReDim cAAKOBETSUJITEParamClass(cABKobetsuProperty.Length - 1)

                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '個別児手パラメータのインスタンス化
                    cAAKOBETSUJITEParamClass(intcnt) = New localhost.AAKOBETSUJITEParamClass

                    '更新・追加した項目を取得
                    cAAKOBETSUJITEParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                    cAAKOBETSUJITEParamClass(intcnt).m_strHIYOKB = CStr(cABKobetsuProperty(intcnt).p_strHIYOKB)
                    cAAKOBETSUJITEParamClass(intcnt).m_strJIDOTEATESTYM = CStr(cABKobetsuProperty(intcnt).p_strJIDOTEATESTYM)
                    cAAKOBETSUJITEParamClass(intcnt).m_strJIDOTEATEEDYM = CStr(cABKobetsuProperty(intcnt).p_strJIDOTEATEEDYM)

                Next

                ' 住基個別児手更新メソッドを実行する
                strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                intUpdCnt = cAACommonBSClass.UpdateKBJITE(strControlData, cAAKOBETSUJITEParamClass)

                If strJukiResult = "0" Then
                    ' 管理情報から取得した内容が"0"のときはチェックする
                    '追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                    If Not (intUpdCnt = cABKobetsuProperty.Length) Then

                        cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                        'エラー定義を取得
                        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                        '例外を生成
                        csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        Throw csAppExp

                    End If
                ElseIf strJukiResult = "1" Then
                    ' チェックしない
                Else
                    ' チェックしない
                End If
            Else
            End If

            ''*履歴番号 000004 2005/12/01 追加開始
            '' 宛名管理情報Ｂクラスのインスタンス作成
            'cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '' 宛名管理情報の種別04識別キー24のデータを取得する(住基側の更新処理の結果を判断するかどうか)
            'csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "24")
            '' 管理情報にレコードが存在し、パラメータが"1"の時はチェックしない
            'If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
            '    If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
            '        ' ﾊﾟﾗﾒｰﾀが"1"のときはチェックしない
            '        strJukiResult = "1"
            '    Else
            '        ' ﾊﾟﾗﾒｰﾀが"1"のときはチェックする
            '        strJukiResult = "0"
            '    End If
            'Else
            '    ' レコードがないときはチェックする
            '    strJukiResult = "0"
            'End If
            ''*履歴番号 000004 2005/12/01 追加終了

            ''WebserviceのURLをWebConfigから取得して設定する
            'cAACommonBSClass = New localhost.AACommonBSClass
            'cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
            ''cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

            'ReDim cAAKOBETSUJITEParamClass(cABKobetsuProperty.Length - 1)

            'For intcnt = 0 To cABKobetsuProperty.Length - 1

            '    '個別児手パラメータのインスタンス化
            '    cAAKOBETSUJITEParamClass(intcnt) = New localhost.AAKOBETSUJITEParamClass

            '    '更新・追加した項目を取得
            '    cAAKOBETSUJITEParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
            '    cAAKOBETSUJITEParamClass(intcnt).m_strHIYOKB = CStr(cABKobetsuProperty(intcnt).p_strHIYOKB)
            '    cAAKOBETSUJITEParamClass(intcnt).m_strJIDOTEATESTYM = CStr(cABKobetsuProperty(intcnt).p_strJIDOTEATESTYM)
            '    cAAKOBETSUJITEParamClass(intcnt).m_strJIDOTEATEEDYM = CStr(cABKobetsuProperty(intcnt).p_strJIDOTEATEEDYM)

            'Next

            '' 住基個別児手更新メソッドを実行する
            'strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
            'intUpdCnt = cAACommonBSClass.UpdateKBJITE(strControlData, cAAKOBETSUJITEParamClass)

            ''*履歴番号 000004 2005/12/01 修正開始
            '''''追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
            ''''If Not (intUpdCnt = cABKobetsuProperty.Length) Then

            ''''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
            ''''    'エラー定義を取得
            ''''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
            ''''    '例外を生成
            ''''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            ''''    Throw csAppExp

            ''''End If

            'If strJukiResult = "0" Then
            '    ' 管理情報から取得した内容が"0"のときはチェックする
            '    '追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
            '    If Not (intUpdCnt = cABKobetsuProperty.Length) Then

            '        cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
            '        'エラー定義を取得
            '        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
            '        '例外を生成
            '        csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '        Throw csAppExp

            '    End If
            'ElseIf strJukiResult = "1" Then
            '    ' チェックしない
            'Else
            '    ' チェックしない
            'End If
            ''*履歴番号 000004 2005/12/01 修正終了
            '*履歴番号 000005 2010/04/09 修正終了

            '*履歴番号 000002 2005/10/13 追加開始
            '*履歴番号 000004 2005/12/01 削除開始
            ' 上のほうで宛名管理情報を取得するので、そこでインスタンス作成する
            '''' 宛名管理情報Ｂクラスのインスタンス作成
            '* corresponds to VS2008 Start 2010/04/16 000006
            ''''cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '* corresponds to VS2008 End 2010/04/16 000006
            '*履歴番号 000004 2005/12/01 削除終了

            ' 宛名管理情報の種別04識別キー21のデータを取得する(上田市ﾎｽﾄとの連携をするかどうかの判定)
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "21")

            ' 管理情報のワークフローレコードが存在し、パラメータが"1"の時だけワークフロー処理を行う
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                    ' ワークフロー処理メソッドを呼ぶ
                    Me.WorkFlowSet(cABKobetsuProperty)
                End If
            End If
            '*履歴番号 000002 2005/10/13 追加終了

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

                Throw objAppExp
            Else
                ' システム例外の場合
                ' エラーログ出力
                m_cfLogClass.ErrorWrite(m_cfControlData, _
                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                    "【エラー内容:" + objExpTool.p_strErrorMessage + "】")
                Throw objSoapExp
            End If
        Catch exAppExp As UFAppException                   ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                    "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + _
                                    "【ワーニング内容:" + exAppExp.Message + "】")
            Throw exAppExp
        Catch exExp As Exception                           ' Exceptionをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                    "【エラー内容:" + exExp.Message + "】")
            Throw exExp
        Finally
            '元のビジネスIDを入れる
            m_cfControlData.m_strBusinessId = m_strRsBusiId
            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        End Try

        Return intUpdCnt

    End Function

    '*履歴番号 000002 2005/10/13 追加開始
    '************************************************************************************************
    '* メソッド名     宛名児童手当ワークフロー
    '* 
    '* 構文           Public Sub WorkFlowSet(ByVal cABKobetsuProperty() As ABKobetsuJiteProperty)
    '* 
    '* 機能　　    　 宛名児童手当データをワークフローへ渡す。
    '* 
    '* 引数           ByVal cDatRow As DataRow  :更新データ
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Public Sub WorkFlowSet(ByVal cABKobetsuProperty() As ABKobetsuJiteProperty)
        Const THIS_METHOD_NAME As String = "WorkFlowSet"
        Dim csABJiteEntity As New DataSet()                 ' 個別事項児手データセット
        Dim csABJiteTable As DataTable                      ' 個別事項児手データテーブル
        Dim csABJiteRow As DataRow                          ' 個別事項児手データロウ
        Dim strNen As String                                ' 作成日時
        Dim intRecCnt As Integer                            ' 連番用カウンター
        Dim cuCityInfoClass As New USSCityInfoClass()       ' 市町村管理情報クラス
        Dim strCityCD As String                             ' 市町村コード
        Dim cABAtenaCnvBClass As ABAtenaCnvBClass
        Dim intIdx As Integer

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 市町村管理情報の取得
            cuCityInfoClass.GetCityInfo(m_cfControlData)
            ' 市町村コードの取得
            strCityCD = cuCityInfoClass.p_strShichosonCD(0)
            ' 作成日時(14桁)の取得
            strNen = DateTime.Now.ToString("yyyyMMddHHmmss")
            ' 連番用カウンターの初期設定
            intRecCnt = 1

            ' テーブルセットの取得
            csABJiteTable = Me.CreateColumnsData()
            csABJiteTable.TableName = ABKobetsuJiteEntity.TABLE_NAME
            ' データセットにテーブルセットの追加
            csABJiteEntity.Tables.Add(csABJiteTable)

            '*****
            '*　１行目～の編集
            '*
            '*****
            For intIdx = 0 To cABKobetsuProperty.Length - 1
                ' 新規レコードの作成
                csABJiteRow = csABJiteEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME).NewRow
                ' 各項目にデータをセット
                csABJiteRow.Item(ABKobetsuJiteEntity.SHICHOSONCD) = strCityCD
                csABJiteRow.Item(ABKobetsuJiteEntity.SHIKIBETSUID) = "AA64"
                csABJiteRow.Item(ABKobetsuJiteEntity.LASTRECKB) = ""
                csABJiteRow.Item(ABKobetsuJiteEntity.SAKUSEIYMD) = strNen
                csABJiteRow.Item(ABKobetsuJiteEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
                csABJiteRow.Item(ABKobetsuJiteEntity.JUMINCD) = cABKobetsuProperty(intIdx).p_strJUMINCD
                '*履歴番号 000003 2005/10/25 追加開始
                csABJiteRow.Item(ABKobetsuJiteEntity.CITYCD) = strCityCD
                csABJiteRow.Item(ABKobetsuJiteEntity.KYUCITYCD) = String.Empty
                '*履歴番号 000003 2005/10/25 追加終了
                csABJiteRow.Item(ABKobetsuJiteEntity.HIYOKB) = cABKobetsuProperty(intIdx).p_strHIYOKB
                csABJiteRow.Item(ABKobetsuJiteEntity.JIDOTEATESTYM) = cABKobetsuProperty(intIdx).p_strJIDOTEATESTYM
                csABJiteRow.Item(ABKobetsuJiteEntity.JIDOTEATEEDYM) = cABKobetsuProperty(intIdx).p_strJIDOTEATEEDYM

                'データセットにレコードを追加
                csABJiteEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME).Rows.Add(csABJiteRow)
                ' 連番用カウントアップ
                intRecCnt += 1
            Next intIdx

            '*****
            '*　最終行の編集
            '*
            '*****
            ' 新規レコードの作成
            csABJiteRow = csABJiteEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME).NewRow
            ' 各項目にデータをセット
            csABJiteRow.Item(ABKobetsuJiteEntity.SHICHOSONCD) = strCityCD
            csABJiteRow.Item(ABKobetsuJiteEntity.SHIKIBETSUID) = "AA64"
            csABJiteRow.Item(ABKobetsuJiteEntity.LASTRECKB) = "E"
            csABJiteRow.Item(ABKobetsuJiteEntity.SAKUSEIYMD) = strNen
            csABJiteRow.Item(ABKobetsuJiteEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
            csABJiteRow.Item(ABKobetsuJiteEntity.JUMINCD) = String.Empty
            '*履歴番号 000003 2005/10/25 追加開始
            csABJiteRow.Item(ABKobetsuJiteEntity.CITYCD) = String.Empty
            csABJiteRow.Item(ABKobetsuJiteEntity.KYUCITYCD) = String.Empty
            csABJiteRow.Item(ABKobetsuJiteEntity.HIYOKB) = String.Empty
            csABJiteRow.Item(ABKobetsuJiteEntity.JIDOTEATESTYM) = String.Empty
            csABJiteRow.Item(ABKobetsuJiteEntity.JIDOTEATEEDYM) = String.Empty
            '*履歴番号 000003 2005/10/25 追加終了
            ' データセットにレコードを追加
            csABJiteEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME).Rows.Add(csABJiteRow)

            '*****
            '*　ワークフロー送信
            '*
            '*****
            ' データセット取得クラスのインスタンス化
            cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            ' ワークフロー送信処理呼び出し
            cABAtenaCnvBClass.WorkFlowExec(csABJiteEntity, WORK_FLOW_NAME, DATA_NAME)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppExp As UFAppException                   ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                    "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + _
                                    "【ワーニング内容:" + exAppExp.Message + "】")
            Throw exAppExp
        Catch exExp As Exception                           ' Exceptionをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                    "【エラー内容:" + exExp.Message + "】")
            Throw exExp

        End Try

    End Sub

    '************************************************************************************************
    '* メソッド名      データカラム作成
    '* 
    '* 構文            Private Function CreateColumnsData() As DataTable
    '* 
    '* 機能　　        レプリカＤＢのカラム定義を作成する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         DataTable() 代納情報テーブル
    '************************************************************************************************
    Private Function CreateColumnsData() As DataTable
        Const THIS_METHOD_NAME As String = "CreateColumnsData"
        Dim csABJiteTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 個別児手情報カラム定義
            csABJiteTable = New DataTable()
            csABJiteTable.TableName = ABKobetsuJiteEntity.TABLE_NAME
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.SHIKIBETSUID, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.SAKUSEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 14
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.LASTRECKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.RENBAN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            '*履歴番号 000003 2005/10/25 追加開始
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.CITYCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.KYUCITYCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            '*履歴番号 000003 2005/10/25 追加終了
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.HIYOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.JIDOTEATESTYM, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.JIDOTEATEEDYM, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return csABJiteTable

    End Function
    '*履歴番号 000002 2005/10/13 追加終了

End Class
