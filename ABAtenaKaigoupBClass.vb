'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        宛名介護マスタ更新(ABAtenaNenkinupBClas)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/11/12　吉澤　行宣
'* 
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2004/03/26 000001     ビジネスIDの変更修正
'* 2005/12/01 000002     住基の個別事項更新結果を評価するかしないかの処理を追加
'* 2008/05/13 000003     ホスト連携処理を起動するワークフロー起動処理を追加（比嘉）
'* 2008/09/30 000004     住基の個別事項マスタ更新の制御機能を追加（吉澤）
'* 2022/12/16 000005    【AB-8010】住民コード世帯コード15桁対応(下村)
'* 2024/02/19 000006    【AB-9001_1】個別記載事項対応(下村)
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

Public Class ABAtenaKaigoupBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfABConfigDataClass As UFConfigDataClass        ' コンフィグデータAB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' コンフィグデータAA
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strRsBusiId As String

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaKaigoupBClass"
    Private Const AA_BUSSINESS_ID As String = "AA"                            ' 業務コード
    '*履歴番号 000003 2008/05/13 追加開始
    Private Const WORK_FLOW_NAME As String = "宛名介護個別事項"         ' ワークフロー名
    Private Const DATA_NAME As String = "介護個別"                      ' データ名
    '*履歴番号 000003 2008/05/13 追加終了
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

        '----------コンフィグデータの"AA"の環境情報を取得----------------------
        cfAAUFConfigClass = New UFConfigClass()
        cfAAUFConfigData = cfAAUFConfigClass.GetConfig(AA_BUSSINESS_ID)
        m_cfAAConfigDataClass = cfAAUFConfigData
        '----------コンフィグデータの"AA"の環境情報を取得----------------------

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
    '* メソッド名     宛名介護マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaKaigo(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty) As Integer
    '* 
    '* 機能　　    　  宛名介護マスタのデータを更新する。
    '* 
    '* 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaKaigo(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaKaigo"
        Dim intUpdCnt As Integer
        Dim cABAtenaKaigoBClass As ABAtenaKaigoBClass
        Dim cAAKOBETSUKAIGOParamClass() As localhost.AAKOBETSUKAIGOParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaKaigoEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()
        Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAppExp As UFAppException
        Dim intcnt As Integer
        '*履歴番号 000002 2005/12/01 追加開始
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '宛名管理情報ＤＡビジネスクラス
        Dim csAtenaKanriEntity As DataSet                   '宛名管理情報データセット
        Dim strJukiResult As String                         '住基の結果をチェックするかどうか(0:する 1:しない)
        '*履歴番号 000002 2005/12/01 追加終了

        Try

            '*履歴番号 000001 2004/03/26 追加開始
            '業務IDを宛名(AB)に変更
            m_cfControlData.m_strBusinessId = "AB"
            '*履歴番号 000001 2004/03/26 追加終了

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '市町村情報取得（市町村コード)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '宛名介護ＤＡクラスのインスタンス化
            cABAtenaKaigoBClass = New ABAtenaKaigoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '宛名介護マスタ抽出呼び出し
                csABAtenaKaigoEntity = cABAtenaKaigoBClass.GetAtenaKaigo(cABKobetsuProperty(intcnt).p_strJUMINCD)

                '追加・更新の判定
                If csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).Rows.Count = 0 Then

                    cDatRow = csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).NewRow()
                    '各項目をプロパティから取得
                    cDatRow.Item(ABAtenaKaigoEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaKaigoEntity.HIHOKENSHAGAITOKB) = String.Empty
                    cDatRow.Item(ABAtenaKaigoEntity.HIHKNSHANO) = cABKobetsuProperty(intcnt).p_strHIHKNSHANO
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB) = cABKobetsuProperty(intcnt).p_strSKAKHIHOKENSHAKB
                    cDatRow.Item(ABAtenaKaigoEntity.JUSHOCHITKRIKB) = cABKobetsuProperty(intcnt).p_strJUSHOCHITKRIKB
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUSHAKB) = cABKobetsuProperty(intcnt).p_strJUKYUSHAKB
                    cDatRow.Item(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD) = cABKobetsuProperty(intcnt).p_strYOKAIGJOTAIKBCD
                    cDatRow.Item(ABAtenaKaigoEntity.KAIGSKAKKB) = cABKobetsuProperty(intcnt).p_strKAIGSKAKKB
                    cDatRow.Item(ABAtenaKaigoEntity.NINTEIKAISHIYMD) = cABKobetsuProperty(intcnt).p_strNINTEIKAISHIYMD
                    cDatRow.Item(ABAtenaKaigoEntity.NINTEISHURYOYMD) = cABKobetsuProperty(intcnt).p_strNINTEISHURYOYMD
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEIYMD) = cABKobetsuProperty(intcnt).p_strJUKYUNINTEIYMD
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD) = cABKobetsuProperty(intcnt).p_strJUKYUNINTEITORIKESHIYMD

                    '市町村コード
                    cDatRow.Item(ABAtenaKaigoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '旧市町村コード
                    cDatRow.Item(ABAtenaKaigoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    'データの追加
                    'csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).Rows.Add(cDatRow)

                    '宛名介護マスタ追加メソッド呼び出し
                    intUpdCnt = cABAtenaKaigoBClass.InsertAtenaKaigo(cDatRow)
                Else

                    cDatRow = csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).Rows(0)
                    '各項目をプロパティから取得
                    cDatRow.Item(ABAtenaKaigoEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaKaigoEntity.HIHKNSHANO) = cABKobetsuProperty(intcnt).p_strHIHKNSHANO
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB) = cABKobetsuProperty(intcnt).p_strSKAKHIHOKENSHAKB
                    cDatRow.Item(ABAtenaKaigoEntity.JUSHOCHITKRIKB) = cABKobetsuProperty(intcnt).p_strJUSHOCHITKRIKB
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUSHAKB) = cABKobetsuProperty(intcnt).p_strJUKYUSHAKB
                    cDatRow.Item(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD) = cABKobetsuProperty(intcnt).p_strYOKAIGJOTAIKBCD
                    cDatRow.Item(ABAtenaKaigoEntity.KAIGSKAKKB) = cABKobetsuProperty(intcnt).p_strKAIGSKAKKB
                    cDatRow.Item(ABAtenaKaigoEntity.NINTEIKAISHIYMD) = cABKobetsuProperty(intcnt).p_strNINTEIKAISHIYMD
                    cDatRow.Item(ABAtenaKaigoEntity.NINTEISHURYOYMD) = cABKobetsuProperty(intcnt).p_strNINTEISHURYOYMD
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEIYMD) = cABKobetsuProperty(intcnt).p_strJUKYUNINTEIYMD
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD) = cABKobetsuProperty(intcnt).p_strJUKYUNINTEITORIKESHIYMD
                    '市町村コード
                    cDatRow.Item(ABAtenaNenkinEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '旧市町村コード
                    cDatRow.Item(ABAtenaNenkinEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '宛名介護マスタ更新メソッド呼び出し
                    intUpdCnt = cABAtenaKaigoBClass.UpdateAtenaKaigo(cDatRow)
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

            '*履歴番号 000004 2008/09/30 修正開始
            ' 宛名管理情報Ｂクラスのインスタンス作成
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            ' 住基個別事項マスタ更新制御情報の取得
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "17")

            '管理情報のレコード存在し、パラメータが "1" の場合のみ更新を行なわない。
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) AndAlso _
                    CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                '住基個別事項マスタの更新は行わない。
            Else

                '*履歴番号 000002 2005/12/01 追加開始
                '*履歴番号 000004 2008/09/30 削除開始
                '' 宛名管理情報Ｂクラスのインスタンス作成
                'cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
                '*履歴番号 000004 2008/09/30 削除終了

                ' 宛名管理情報の種別04識別キー25のデータを取得する(住基側の更新処理の結果を判断するかどうか)
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "25")
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
                '*履歴番号 000002 2005/12/01 追加終了

                'WebserviceのURLをWebConfigから取得して設定する
                cAACommonBSClass = New localhost.AACommonBSClass
                cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                ReDim cAAKOBETSUKAIGOParamClass(cABKobetsuProperty.Length - 1)

                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '個別介護パラメータのインスタンス化
                    cAAKOBETSUKAIGOParamClass(intcnt) = New localhost.AAKOBETSUKAIGOParamClass

                    '更新・追加した項目を取得
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strHIHKNSHANO = CStr(cABKobetsuProperty(intcnt).p_strHIHKNSHANO)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strSKAKSHUTKYMD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strSKAKSSHTSYMD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strSKAKHIHOKENSHAKB = CStr(cABKobetsuProperty(intcnt).p_strSKAKHIHOKENSHAKB)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUSHOCHITKRIKB = CStr(cABKobetsuProperty(intcnt).p_strJUSHOCHITKRIKB)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUKYUSHAKB = CStr(cABKobetsuProperty(intcnt).p_strJUKYUSHAKB)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strYOKAIGJOTAIKBCD = CStr(cABKobetsuProperty(intcnt).p_strYOKAIGJOTAIKBCD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strKAIGSKAKKB = CStr(cABKobetsuProperty(intcnt).p_strKAIGSKAKKB)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strNINTEIKAISHIYMD = CStr(cABKobetsuProperty(intcnt).p_strNINTEIKAISHIYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strNINTEISHURYOYMD = CStr(cABKobetsuProperty(intcnt).p_strNINTEISHURYOYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUKYUNINTEIYMD = CStr(cABKobetsuProperty(intcnt).p_strJUKYUNINTEIYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUKYUNINTEITORIKESHIYMD = CStr(cABKobetsuProperty(intcnt).p_strJUKYUNINTEITORIKESHIYMD)
                Next

                ' 住基個別介護更新メソッドを実行する
                strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                intUpdCnt = cAACommonBSClass.UpdateKBKAIGO(strControlData, cAAKOBETSUKAIGOParamClass)

                '*履歴番号 000002 2005/12/01 修正開始
                '''''追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                ''''If Not (intUpdCnt = cABKobetsuProperty.Length) Then

                ''''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                ''''    'エラー定義を取得
                ''''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                ''''    '例外を生成
                ''''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                ''''    Throw csAppExp

                ''''End If
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
                '*履歴番号 000002 2005/12/01 修正終了

            End If
            '*履歴番号 000004 2008/09/30 修正終了



            '*履歴番号 000003 2008/05/13 追加開始
            ' 宛名管理情報の種別04識別キー26のデータを取得する(上田市ﾎｽﾄとの連携をするかどうかの判定)
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "26")

            ' 管理情報のワークフローレコードが存在し、パラメータが"1"の時だけワークフロー処理を行う
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                    ' ワークフロー処理メソッドを呼ぶ
                    Me.WorkFlowSet(cABKobetsuProperty)
                End If
            End If
            '*履歴番号 000003 2008/05/13 追加終了

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

    '*履歴番号 000003 2008/05/13 追加開始
    '************************************************************************************************
    '* メソッド名     宛名介護ワークフロー
    '* 
    '* 構文           Public Sub WorkFlowSet(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty)
    '* 
    '* 機能　　    　 宛名介護データをワークフローへ渡す。
    '* 
    '* 引数           ByVal cDatRow As DataRow  :更新データ
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Public Sub WorkFlowSet(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty)
        Const THIS_METHOD_NAME As String = "WorkFlowSet"
        Dim csABKaigoEntity As New DataSet                  ' 個別事項介護データセット
        Dim csABKaigoTable As DataTable                     ' 個別事項介護データテーブル
        Dim csABKaigoRow As DataRow                         ' 個別事項介護データロウ
        Dim strNen As String                                ' 作成日時
        Dim intRecCnt As Integer                            ' 連番用カウンター
        Dim cuCityInfoClass As New USSCityInfoClass         ' 市町村管理情報クラス
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
            csABKaigoTable = Me.CreateColumnsData()
            csABKaigoTable.TableName = ABKobetsuKaigoEntity.TABLE_NAME
            ' データセットにテーブルセットの追加
            csABKaigoEntity.Tables.Add(csABKaigoTable)

            '*****
            '*　１行目～の編集
            '*
            '*****
            For intIdx = 0 To cABKobetsuProperty.Length - 1
                ' 新規レコードの作成
                csABKaigoRow = csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).NewRow
                ' 各項目にデータをセット
                csABKaigoRow.Item(ABKobetsuKaigoEntity.CITYCD) = strCityCD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SHIKIBETSUID) = "AA65"
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SAKUSEIYMD) = strNen
                csABKaigoRow.Item(ABKobetsuKaigoEntity.LASTRECKB) = ""
                csABKaigoRow.Item(ABKobetsuKaigoEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUMINCD) = cABKobetsuProperty(intIdx).p_strJUMINCD.RSubstring(3, 12)
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SHICHOSONCD) = strCityCD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.KYUSHICHOSONCD) = String.Empty
                csABKaigoRow.Item(ABKobetsuKaigoEntity.HIHKNSHANO) = cABKobetsuProperty(intIdx).p_strHIHKNSHANO
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intIdx).p_strSKAKSHUTKYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intIdx).p_strSKAKSSHTSYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKHIHOKENSHAKB) = cABKobetsuProperty(intIdx).p_strSKAKHIHOKENSHAKB
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUSHOCHITKRIKB) = cABKobetsuProperty(intIdx).p_strJUSHOCHITKRIKB
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUSHAKB) = cABKobetsuProperty(intIdx).p_strJUKYUSHAKB
                csABKaigoRow.Item(ABKobetsuKaigoEntity.YOKAIGJOTAIKBCD) = cABKobetsuProperty(intIdx).p_strYOKAIGJOTAIKBCD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.KAIGSKAKKB) = cABKobetsuProperty(intIdx).p_strKAIGSKAKKB
                csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEIKAISHIYMD) = cABKobetsuProperty(intIdx).p_strNINTEIKAISHIYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEISHURYOYMD) = cABKobetsuProperty(intIdx).p_strNINTEISHURYOYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEIYMD) = cABKobetsuProperty(intIdx).p_strJUKYUNINTEIYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEITORIKESHIYMD) = cABKobetsuProperty(intIdx).p_strJUKYUNINTEITORIKESHIYMD

                'データセットにレコードを追加
                csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).Rows.Add(csABKaigoRow)
                ' 連番用カウントアップ
                intRecCnt += 1
            Next intIdx

            '*****
            '*　最終行の編集
            '*
            '*****
            ' 新規レコードの作成
            csABKaigoRow = csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).NewRow
            ' 各項目にデータをセット
            csABKaigoRow.Item(ABKobetsuKaigoEntity.CITYCD) = strCityCD
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SHIKIBETSUID) = "AA65"
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SAKUSEIYMD) = strNen
            csABKaigoRow.Item(ABKobetsuKaigoEntity.LASTRECKB) = "E"
            csABKaigoRow.Item(ABKobetsuKaigoEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUMINCD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SHICHOSONCD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.KYUSHICHOSONCD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.HIHKNSHANO) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSHUTKYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSSHTSYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKHIHOKENSHAKB) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUSHOCHITKRIKB) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUSHAKB) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.YOKAIGJOTAIKBCD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.KAIGSKAKKB) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEIKAISHIYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEISHURYOYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEIYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEITORIKESHIYMD) = String.Empty
            ' データセットにレコードを追加
            csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).Rows.Add(csABKaigoRow)

            '*****
            '*　ワークフロー送信
            '*
            '*****
            ' データセット取得クラスのインスタンス化
            cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            ' ワークフロー送信処理呼び出し
            cABAtenaCnvBClass.WorkFlowExec(csABKaigoEntity, WORK_FLOW_NAME, DATA_NAME)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppExp As UFAppException                   ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                    "【ワーニングコード:" + exAppExp.p_strErrorCode + "】" + _
                                    "【ワーニング内容:" + exAppExp.Message + "】")
            Throw
        Catch exExp As Exception                           ' Exceptionをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                    "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                    "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                    "【エラー内容:" + exExp.Message + "】")
            Throw

        End Try

    End Sub

    '************************************************************************************************
    '* メソッド名      データカラム作成
    '* 
    '* 構文            Private Function CreateColumnsData() As DataTable
    '* 
    '* 機能　　        レプリカＤＢのカラム定義を作成する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          DataTable() 代納情報テーブル
    '************************************************************************************************
    Private Function CreateColumnsData() As DataTable
        Const THIS_METHOD_NAME As String = "CreateColumnsData"
        Dim csABKaigoTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 個別介護情報カラム定義
            csABKaigoTable = New DataTable
            csABKaigoTable.TableName = ABKobetsuKaigoEntity.TABLE_NAME
            ' 市町村コード
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.CITYCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            ' 識別ID
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SHIKIBETSUID, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            ' 処理日時
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SAKUSEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 14
            ' 最終行区分
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.LASTRECKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            ' 連番
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.RENBAN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            ' 住民コード
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            ' 市町村コード
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            ' 旧市町村コード
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            ' 被保険者番号
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.HIHKNSHANO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            ' 資格取得日
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SKAKSHUTKYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' 資格喪失日
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SKAKSSHTSYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' 資格被保険者区分
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SKAKHIHOKENSHAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            ' 住所地特例者区分
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUSHOCHITKRIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            ' 受給者区分
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUKYUSHAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            ' 要介護状態区分コード
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.YOKAIGJOTAIKBCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            ' 要介護状態区分
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.KAIGSKAKKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            ' 認定有効開始年月日
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.NINTEIKAISHIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' 認定有効終了年月日
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.NINTEISHURYOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' 受給認定年月日
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUKYUNINTEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' 受給認定取消年月日
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUKYUNINTEITORIKESHIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8

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

        Return csABKaigoTable

    End Function
    '*履歴番号 000003 2008/05/13 追加終了

End Class
