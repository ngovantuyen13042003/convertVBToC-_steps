'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        宛名年金マスタ更新(ABAtenaNenkinupBClas)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/11/11　吉澤　行宣
'* 
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2004/03/26 000001     ビジネスIDの変更修正
'* 2005/12/01 000002     住基の個別事項更新結果を評価するかしないかの処理を追加
'* 2024/02/19 000003    【AB-9001_1】個別記載事項対応(下村)
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

Public Class ABAtenaNenkinupBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfABConfigDataClass As UFConfigDataClass        ' コンフィグデータAB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' コンフィグデータAA
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strRsBusiId As String

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaNenkinupBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード
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
    '* メソッド名     宛名年金マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaNenkin(ByVal cABKobetsuProperty As ABKobetsuNenkinProperty) As Integer
    '* 
    '* 機能　　    　  宛名年金マスタのデータを更新する。
    '* 
    '* 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaNenkin(ByVal cABKobetsuProperty() As ABKobetsuNenkinProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaNenkin"
        Dim intUpdCnt As Integer
        Dim cABAtenaNenkinBClass As ABAtenaNenkinBClass
        Dim cAAKOBETSUNENKINParamClass(0) As localhost.AAKOBETSUNENKINParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csAtenaNenkinEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()
        Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAppExp As UFAppException
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

            '宛名年金ＤＡクラスのインスタンス化
            cABAtenaNenkinBClass = New ABAtenaNenkinBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            Dim intcnt As Integer
            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '宛名年金マスタ抽出呼び出し
                csAtenaNenkinEntity = cABAtenaNenkinBClass.GetAtenaNenkin(CStr(cABKobetsuProperty(intcnt).p_strJUMINCD))

                '追加・更新の判定
                If csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Count = 0 Then

                    cDatRow = csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).NewRow()
                    '各項目をプロパティから取得
                    cDatRow.Item(ABAtenaNenkinEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaNenkinEntity.HIHOKENSHAGAITOKB) = String.Empty
                    cDatRow.Item(ABAtenaNenkinEntity.KSNENKNNO) = cABKobetsuProperty(intcnt).p_strKSNENKNNO
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKSHU) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKSHU
                    cDatRow.Item(ABAtenaNenkinEntity.SHUBETSUHENKOYMD) = String.Empty
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKRIYUCD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSRIYUCD
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO1) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO1) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU1) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN1) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB1) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO2) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO2) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU2) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN2) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB2) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO3) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO3) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU3) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN3) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB3) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB3
                    '市町村コード
                    cDatRow.Item(ABAtenaNenkinEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '旧市町村コード
                    cDatRow.Item(ABAtenaNenkinEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    'データの追加
                    'csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Add(cDatRow)

                    '宛名年金マスタ追加メソッド呼び出し
                    intUpdCnt = cABAtenaNenkinBClass.InsertAtenaNenkin(cDatRow)
                Else

                    cDatRow = csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows(0)
                    '各項目をプロパティから取得
                    cDatRow.Item(ABAtenaNenkinEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaNenkinEntity.KSNENKNNO) = cABKobetsuProperty(intcnt).p_strKSNENKNNO
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKSHU) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKSHU
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKRIYUCD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSRIYUCD
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO1) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO1) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU1) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN1) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB1) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO2) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO2) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU2) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN2) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB2) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO3) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO3) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU3) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN3) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB3) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB3

                    '市町村コード
                    cDatRow.Item(ABAtenaNenkinEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '旧市町村コード
                    cDatRow.Item(ABAtenaNenkinEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '宛名年金マスタ更新メソッド呼び出し
                    intUpdCnt = cABAtenaNenkinBClass.UpdateAtenaNenkin(cDatRow)
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

            '*履歴番号 000002 2005/12/01 追加開始
            ' 宛名管理情報Ｂクラスのインスタンス作成
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            ' 宛名管理情報の種別04識別キー23のデータを取得する(住基側の更新処理の結果を判断するかどうか)
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "23")
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
            cAACommonBSClass = New localhost.AACommonBSClass()
            'm_cfLogClass.WarningWrite(m_cfControlData, m_cfABConfigDataClass.p_strWebServerDomain + "Densan/Reams/AA/AA001BS/AACommonBSClass.asmx")
            cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"

            'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

            ReDim cAAKOBETSUNENKINParamClass(cABKobetsuProperty.Length - 1)

            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '個別年金パラメータのインスタンス化
                cAAKOBETSUNENKINParamClass(intcnt) = New localhost.AAKOBETSUNENKINParamClass()

                '更新・追加した項目を取得
                cAAKOBETSUNENKINParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strKSNENKNNO = CStr(cABKobetsuProperty(intcnt).p_strKSNENKNNO)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSHUTKYMD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSHUTKSHU = CStr(cABKobetsuProperty(intcnt).p_strSKAKSHUTKSHU)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSHUTKRIYUCD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSHUTKRIYUCD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSSHTSYMD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSSHTSRIYUCD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSSHTSRIYUCD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKIGO1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNNO1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNNO1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNSHU1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNSHU1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNEDABAN1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKB1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKB1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKIGO2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNNO2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNNO2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNSHU2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNSHU2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNEDABAN2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKB2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKB2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKIGO3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO3)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNNO3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNNO3)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNSHU3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNSHU3)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNEDABAN3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN3)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKB3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKB3)

            Next

            ' 住基個別年金更新メソッドを実行する
            strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
            intUpdCnt = cAACommonBSClass.UpdateKBNENKIN(strControlData, cAAKOBETSUNENKINParamClass)

            '*履歴番号 000002 2005/12/01 修正開始
            ''''''追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
            '''''If Not (intUpdCnt = cABKobetsuProperty.Length) Then

            '''''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
            '''''    'エラー定義を取得
            '''''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
            '''''    '例外を生成
            '''''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '''''    Throw csAppExp

            '''''End If

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
