'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        宛名選挙マスタ更新(ABAtenaSenkyoupBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/11/11　吉澤　行宣
'* 
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2004/03/26 000001     ビジネスIDの変更修正
'* 2005/02/17 000002     レスポンス改善：UpdateAtenaSenkyoでAtenaマスタ更新修正
'* 2006/03/17 000003     投票区コードの更新判定を修正
'* 2010/02/09 000004     管理情報により住基個別事項の更新を制御する
'* 2024/02/19 000005    【AB-9001_1】個別記載事項対応(下村)
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

Public Class ABAtenaSenkyoupBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfABConfigDataClass As UFConfigDataClass        ' コンフィグデータAB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' コンフィグデータAA
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strRsBusiId As String

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaSenkyoupBClass"
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
    '* メソッド名     宛名選挙マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaSenkyo(ByVal cABKobetsuProperty As ABKobetsuSenkyoProperty) As Integer
    '* 
    '* 機能　　    　  宛名選挙マスタのデータを更新する。
    '* 
    '* 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaSenkyo(ByVal cABKobetsuProperty() As ABKobetsuSenkyoProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaSenkyo"
        Dim intUpdCnt As Integer
        Dim cABAtenaSenkyoBClass As ABAtenaSenkyoBClass
        Dim cAAKOBETSUSENKYOParamClass(0) As localhost.AAKOBETSUSENKYOParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaSenkyoEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()

        Dim cABAtenaBClass As ABAtenaBClass
        Dim csABAtenaEntity As DataSet
        Dim cDatRowt As DataRow
        Dim cSearchKey As New ABAtenaSearchKey()            ' 宛名検索キー
        Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAppExp As UFAppException
        Dim intcnt As Integer

        '*履歴番号 000004 2010/02/09 追加開始
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '宛名管理情報ＤＡビジネスクラス
        Dim csAtenaKanriEntity As DataSet                   '宛名管理情報データセット
        '*履歴番号 000004 2010/02/09 追加終了

        Try

            '*履歴番号 000001 2004/03/26 追加開始
            '業務IDを宛名(AB)に変更
            m_cfControlData.m_strBusinessId = "AB"
            '*履歴番号 000001 2004/03/26 追加終了

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '市町村情報取得（市町村コード)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '宛名選挙ＤＡクラスのインスタンス化
            cABAtenaSenkyoBClass = New ABAtenaSenkyoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            '宛名ＤＡクラスのインスタンス化
            cABAtenaBClass = New ABAtenaBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            cSearchKey.p_strJuminYuseniKB = "1"

            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '宛名選挙マスタ抽出呼び出し
                csABAtenaSenkyoEntity = cABAtenaSenkyoBClass.GetAtenaSenkyo(cABKobetsuProperty(intcnt).p_strJUMINCD)

                '追加・更新の判定
                If csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).Rows.Count = 0 Then

                    cDatRow = csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).NewRow()
                    '各項目をプロパティから取得
                    cDatRow.Item(ABAtenaSenkyoEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB) = cABKobetsuProperty(intcnt).p_strSenkyoShikakuKB
                    cDatRow.Item(ABAtenaSenkyoEntity.TOROKUJOTAIKBN) = String.Empty

                    '市町村コード
                    cDatRow.Item(ABAtenaSenkyoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '旧市町村コード
                    cDatRow.Item(ABAtenaSenkyoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    'データの追加
                    'csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).Rows.Add(cDatRow)

                    '宛名選挙マスタ追加メソッド呼び出し
                    intUpdCnt = cABAtenaSenkyoBClass.InsertAtenaSenkyo(cDatRow)

                Else

                    cDatRow = csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).Rows(0)
                    '各項目をプロパティから取得
                    cDatRow.Item(ABAtenaSenkyoEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB) = cABKobetsuProperty(intcnt).p_strSenkyoShikakuKB

                    '市町村コード
                    cDatRow.Item(ABAtenaSenkyoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '旧市町村コード
                    cDatRow.Item(ABAtenaSenkyoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '宛名選挙マスタ更新メソッド呼び出し
                    intUpdCnt = cABAtenaSenkyoBClass.UpdateAtenaSenkyo(cDatRow)
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

                ' 宛名検索キーの設定をする
                cSearchKey.p_strJuminCD = cABKobetsuProperty(intcnt).p_strJUMINCD

                ' 宛名データを取得する
                csABAtenaEntity = cABAtenaBClass.GetAtenaBHoshu(1, cSearchKey)

                '追加・更新の判定
                If csABAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count = 0 Then
                    intUpdCnt = 0
                Else
                    '*履歴番号 000002 2005/02/17 修正開始　000003 2006/03/17 修正開始
                    'Rowを取得
                    cDatRowt = csABAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)

                    ' 宛名マスタの投票区コードと個別プロパティの投票区コードが同じだったら更新しない
                    If Not (CType(cDatRowt.Item(ABAtenaEntity.TOHYOKUCD), String) = cABKobetsuProperty(intcnt).p_strTohyokuCD) Then
                        '投票区CDをプロパティから取得
                        cDatRowt.Item(ABAtenaEntity.TOHYOKUCD) = cABKobetsuProperty(intcnt).p_strTohyokuCD

                        '宛名マスタ追加メソッド呼び出し
                        intUpdCnt = cABAtenaBClass.UpdateAtenaB(cDatRowt)
                    End If

                    'cDatRowt = csABAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)
                    ''投票区CDをプロパティから取得
                    'cDatRowt.Item(ABAtenaEntity.TOHYOKUCD) = cABKobetsuProperty(intcnt).p_strTohyokuCD

                    ''宛名マスタ追加メソッド呼び出し
                    'intUpdCnt = cABAtenaBClass.UpdateAtenaB(cDatRowt)
                    '*履歴番号 000002 2004/02/17 修正終了　000003 2006/03/17 修正開始
                End If

                '追加・更新件数が0件の時0を返す
                If intUpdCnt = 0 Then

                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    'エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
                    '例外を生成
                    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    Throw csAppExp
                End If

            Next

            '*履歴番号 000004 2010/02/09 修正開始
            ' 宛名管理情報Ｂクラスのインスタンス作成
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '  宛名管理情報の種別04識別キー01のデータを全件取得する
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "15")

            '管理情報の住基更新レコードが存在しない、または、パラメータが"0"の時だけ住基更新処理を行う
            If (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) OrElse _
                CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "0" Then

                'WebserviceのURLをWebConfigから取得して設定する
                cAACommonBSClass = New localhost.AACommonBSClass
                cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                ReDim cAAKOBETSUSENKYOParamClass(cABKobetsuProperty.Length - 1)

                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '個別選挙パラメータのインスタンス化
                    cAAKOBETSUSENKYOParamClass(intcnt) = New localhost.AAKOBETSUSENKYOParamClass

                    '更新・追加した項目を取得
                    cAAKOBETSUSENKYOParamClass(intcnt).m_strJuminCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                    cAAKOBETSUSENKYOParamClass(intcnt).m_strSenkyoShikakuKB = CStr(cABKobetsuProperty(intcnt).p_strSenkyoShikakuKB)
                    cAAKOBETSUSENKYOParamClass(intcnt).m_strTohyokuCD = CStr(cABKobetsuProperty(intcnt).p_strTohyokuCD)

                Next

                ' 住基個別選挙更新メソッドを実行する
                strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                intUpdCnt = cAACommonBSClass.UpdateKBSENKYO(strControlData, cAAKOBETSUSENKYOParamClass)

                '追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                If Not (intUpdCnt = cABKobetsuProperty.Length) Then

                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    'エラー定義を取得
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    '例外を生成
                    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    Throw csAppExp

                End If

            End If
            ''WebserviceのURLをWebConfigから取得して設定する
            'cAACommonBSClass = New localhost.AACommonBSClass
            'cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
            ''cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

            'ReDim cAAKOBETSUSENKYOParamClass(cABKobetsuProperty.Length - 1)

            'For intcnt = 0 To cABKobetsuProperty.Length - 1

            '    '個別選挙パラメータのインスタンス化
            '    cAAKOBETSUSENKYOParamClass(intcnt) = New localhost.AAKOBETSUSENKYOParamClass

            '    '更新・追加した項目を取得
            '    cAAKOBETSUSENKYOParamClass(intcnt).m_strJuminCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
            '    cAAKOBETSUSENKYOParamClass(intcnt).m_strSenkyoShikakuKB = CStr(cABKobetsuProperty(intcnt).p_strSenkyoShikakuKB)
            '    cAAKOBETSUSENKYOParamClass(intcnt).m_strTohyokuCD = CStr(cABKobetsuProperty(intcnt).p_strTohyokuCD)

            'Next

            '' 住基個別選挙更新メソッドを実行する
            'strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
            'intUpdCnt = cAACommonBSClass.UpdateKBSENKYO(strControlData, cAAKOBETSUSENKYOParamClass)

            ''追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
            'If Not (intUpdCnt = cABKobetsuProperty.Length) Then

            '    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
            '    'エラー定義を取得
            '    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
            '    '例外を生成
            '    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '    Throw csAppExp

            'End If
            '*履歴番号 000004 2010/02/09 修正終了

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

End Class
