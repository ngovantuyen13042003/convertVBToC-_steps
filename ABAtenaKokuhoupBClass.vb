'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        宛名国保マスタ更新(ABAtenaKokuhoupBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/11/12　吉澤　行宣
'* 
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2004/02/26  000001     RⅢ連携（ワークフロー）処理を追加
'* 2004/03/08  000002     住基更新処理有無の判定を追加
'* 2004/03/26  000003     ビジネスIDの変更修正
'* 2005/12/01  000004     住基の個別事項更新結果を評価するかしないかの処理を追加
'* 2010/04/16  000005      VS2008対応（比嘉）
'* 2022/12/16  000006    【AB-8010】住民コード世帯コード15桁対応(下村)
'* 2024/02/19  000007    【AB-9001_1】個別記載事項対応(下村)
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
Imports Densan.WorkFlow.UWCommon

Public Class ABAtenaKokuhoupBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfABConfigDataClass As UFConfigDataClass        ' コンフィグデータAB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' コンフィグデータAA
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strRsBusiId As String

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaKokuhoupBClass"
    Private Const AA_BUSSINESS_ID As String = "AA"          ' 業務コード
    '*履歴番号 000001 2004/02/26 追加開始
    Private Const WORK_FLOW_NAME As String = "宛名国保個別事項"             ' ワークフロー名
    Private Const DATA_NAME As String = "国保個別"                      'データ名
    '*履歴番号 000001 2004/02/26 追加終了

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

        '*履歴番号 000003 2004/03/26 削除開始
        ''業務IDを宛名(AB)に変更
        'm_cfControlData.m_strBusinessId = "AB"
        '*履歴番号 000003 2004/03/26 削除終了

    End Sub

#End Region

    '************************************************************************************************
    '* メソッド名     宛名国保マスタ更新
    '* 
    '* 構文           Public Function UpdateAtenaKokuho(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty) As Integer
    '* 
    '* 機能　　    　  宛名国保マスタのデータを更新する。
    '* 
    '* 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaKokuho(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaKokuho"
        Dim intUpdCnt As Integer
        Dim cABAtenaKokuhoBClass As ABAtenaKokuhoBClass
        Dim cAAKOBETSUKOKUHOParamClass(0) As localhost.AAKOBETSUKOKUHOParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaKokuhoEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()
        Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim csAppExp As UFAppException
        '*履歴番号 000001 2004/02/26 追加開始
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '宛名管理情報ＤＡビジネスクラス
        Dim csAtenaKanriEntity As DataSet                   '宛名管理情報データセット
        '*履歴番号 000001 2004/02/26 追加終了
        '*履歴番号 000004 2005/12/01 追加開始
        Dim strJukiResult As String                         '住基の結果をチェックするかどうか(0:する 1:しない)
        '*履歴番号 000004 2005/12/01 追加終了

        Try

            '*履歴番号 000003 2004/03/26 追加開始
            '業務IDを宛名(AB)に変更
            m_cfControlData.m_strBusinessId = "AB"
            '*履歴番号 000003 2004/03/26 追加終了

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '市町村情報取得（市町村コード)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '宛名国保ＤＡクラスのインスタンス化
            cABAtenaKokuhoBClass = New ABAtenaKokuhoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            '宛名国保マスタ抽出呼び出し
            csABAtenaKokuhoEntity = cABAtenaKokuhoBClass.GetAtenaKokuho(cABKobetsuProperty.p_strJUMINCD)

            '追加・更新の判定
            If csABAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Count = 0 Then

                cDatRow = csABAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).NewRow()
                '各項目をプロパティから取得
                cDatRow.Item(ABAtenaKokuhoEntity.JUMINCD) = cABKobetsuProperty.p_strJUMINCD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHONO) = cABKobetsuProperty.p_strKOKUHONO
                cDatRow.Item(ABAtenaKokuhoEntity.HIHOKENSHAGAITOKB) = String.Empty
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKB) = cABKobetsuProperty.p_strKOKUHOGAKUENKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD) = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD) = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKB) = cABKobetsuProperty.p_strKOKUHOTISHKKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO) = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO   '*DB(ABATENAKOKUHO)に存在してない
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO) = cABKobetsuProperty.p_strKOKUHOHOKENSHONO

                '市町村コード
                cDatRow.Item(ABAtenaKokuhoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                '旧市町村コード
                cDatRow.Item(ABAtenaKokuhoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                'データの追加
                'csABAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Add(cDatRow)

                '宛名国保マスタ追加メソッド呼び出し
                intUpdCnt = cABAtenaKokuhoBClass.InsertAtenaKokuho(cDatRow)
            Else

                cDatRow = csABAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows(0)
                '各項目をプロパティから取得
                cDatRow.Item(ABAtenaKokuhoEntity.JUMINCD) = cABKobetsuProperty.p_strJUMINCD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHONO) = cABKobetsuProperty.p_strKOKUHONO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKB) = cABKobetsuProperty.p_strKOKUHOGAKUENKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD) = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD) = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKB) = cABKobetsuProperty.p_strKOKUHOTISHKKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO) = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO  '*DB(ABATENAKOKUHO)に存在してない
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO) = cABKobetsuProperty.p_strKOKUHOHOKENSHONO

                '市町村コード
                cDatRow.Item(ABAtenaKokuhoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                '旧市町村コード
                cDatRow.Item(ABAtenaKokuhoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                '宛名国保マスタ更新メソッド呼び出し
                intUpdCnt = cABAtenaKokuhoBClass.UpdateAtenaKokuho(cDatRow)
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


            '*履歴番号 000002 2004/03/08 追加開始
            ' 宛名管理情報Ｂクラスのインスタンス作成
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '  宛名管理情報の種別04識別キー01のデータを全件取得する
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "11")

            '管理情報の住基更新レコードが存在し、パラメータが"0"の時だけ住基更新処理を行う
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "0" Then

                    'WebserviceのURLをWebConfigから取得して設定する
                    cAACommonBSClass = New localhost.AACommonBSClass()
                    cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                    'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                    '個別国保パラメータのインスタンス化
                    cAAKOBETSUKOKUHOParamClass(0) = New localhost.AAKOBETSUKOKUHOParamClass()

                    '更新・追加した項目を取得
                    cAAKOBETSUKOKUHOParamClass(0).m_strJUMINCD = cABKobetsuProperty.p_strJUMINCD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHONO = cABKobetsuProperty.p_strKOKUHONO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSHIKAKUKB = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSHIKAKUKBMEISHO = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSHIKAKUKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOGAKUENKB = cABKobetsuProperty.p_strKOKUHOGAKUENKB
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOGAKUENKBMEISHO = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOGAKUENKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSHUTOKUYMD = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSOSHITSUYMD = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKKB = cABKobetsuProperty.p_strKOKUHOTISHKKB
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKKBMEISHO = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKHONHIKB = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKHONHIKBMEISHO = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO '＊国保退職本被区分正式名称英字項目名に間違いあり＊
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKHONHIKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKGAITOYMD = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKHIGAITOYMD = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOHOKENSHOKIGO = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOHOKENSHONO = cABKobetsuProperty.p_strKOKUHOHOKENSHONO

                    ' 住基個別国保更新メソッドを実行する
                    strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                    intUpdCnt = cAACommonBSClass.UpdateKBKOKUHO(strControlData, cAAKOBETSUKOKUHOParamClass)

                    '*履歴番号 000004 2005/12/01 追加開始
                    ' 宛名管理情報の種別04識別キー22のデータを取得する(住基側の更新処理の結果を判断するかどうか)
                    csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "22")
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
                    '*履歴番号 000004 2005/12/01 追加終了

                    '*履歴番号 000004 2005/12/01 修正開始
                    '* corresponds to VS2008 Start 2010/04/16 000005
                    ''''追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                    ''''If intUpdCnt = 0 Then
                    ''''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    ''''    'エラー定義を取得
                    ''''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    ''''    '例外を生成
                    ''''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    ''''    Throw csAppExp
                    ''''End If
                    '* corresponds to VS2008 End 2010/04/16 000005
                    If strJukiResult = "0" Then
                        ' 管理情報から取得した内容が"0"のときはチェックする
                        '追加・更新件数が0件の時メッセージ"住基の個別事項の更新は正常に行えませんでした"を返す
                        If intUpdCnt = 0 Then
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
                    '*履歴番号 000004 2005/12/01 修正終了
                End If
            End If
            '*履歴番号 000002 2004/03/08 追加開始

            '*履歴番号 000001 2004/02/26 追加開始
            '  宛名管理情報の種別04識別キー01のデータを全件取得する
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "02")

            '管理情報のワークフローレコードが存在し、パラメータが"1"の時だけワークフロー処理を行う
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                    'ワークフロー処理メソッドを呼ぶ
                    Me.WorkFlowSet(cABKobetsuProperty)
                End If
            End If
            '*履歴番号 000001 2004/02/26 追加終了

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

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


    '*履歴番号 000001 2004/02/26 追加開始
    '************************************************************************************************
    '* メソッド名     宛名国保ワークフロー
    '* 
    '* 構文           Public Function UpdateAtenaKokuho(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty) As Integer
    '* 
    '* 機能　　    　  宛名国保データをワークフローへ渡す。
    '* 
    '* 引数           cABKobetsuProperty As ABKobetsuProperty  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Sub WorkFlowSet(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty)
        Const THIS_METHOD_NAME As String = "WorkFlowSet"
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim cwMessage As UWMessageClass                     'ワークフロー起動クラス
        'Dim cwStartRetInfo As UWStartRetInfo                'ワークフロー開始クラス
        '* corresponds to VS2008 End 2010/04/16 000005
        'Dim cUWSerialGroupId(0) As UWSerialGroupId
        'Dim cUWSerialGroupIdTemp As UWSerialGroupId
        'Dim cwDataInfo As UWStartDataInfo                                              ' ワークフローデータ
        Dim strMethodName As String = Reflection.MethodBase.GetCurrentMethod.Name       ' ワークフローデータ
        Dim cUWStartDataInfoForDataSet(0) As UWStartDataInfoForDataSet
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000005
        Dim csABKokuhoEntity As New DataSet()               '個別事項国保データセット
        Dim csABKokuhoTable As DataTable                    '個別事項国保データテーブル
        Dim csABKokuhoRow As DataRow                        '個別事項国保データロウ
        Dim strNen As String                                '作成日時
        Dim intRecCnt As Integer                            '連番用カウンター
        Dim cuCityInfoClass As New USSCityInfoClass()       '市町村管理情報クラス
        Dim strCityCD As String                             '市町村コード
        Dim cABAtenaCnvBClass As ABAtenaCnvBClass

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '市町村管理情報の取得
            cuCityInfoClass.GetCityInfo(m_cfControlData)
            '市町村コードの取得
            strCityCD = cuCityInfoClass.p_strShichosonCD(0)
            ' 作成日時(14桁)の取得
            strNen = DateTime.Now.ToString("yyyyMMddHHmmss")
            '連番用カウンターの初期設定
            intRecCnt = 1

            ' テーブルセットの取得
            csABKokuhoTable = Me.CreateColumnsData()
            csABKokuhoTable.TableName = ABKobetsuKokuhoEntity.TABLE_NAME
            ' データセットにテーブルセットの追加
            csABKokuhoEntity.Tables.Add(csABKokuhoTable)

            '*****
            '*　１行目の編集
            '*
            '*****
            '新規レコードの作成
            csABKokuhoRow = csABKokuhoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME).NewRow
            '各項目にデータをセット
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SHICHOSONCD) = strCityCD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SHIKIBETSUID) = "AA60"
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.LASTRECKB) = ""
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SAKUSEIYMD) = strNen
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.JUMINCD) = cABKobetsuProperty.p_strJUMINCD.RSubstring(3, 12)
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHONO) = cABKobetsuProperty.p_strKOKUHONO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKB) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBMEISHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOGAKUENKB) = cABKobetsuProperty.p_strKOKUHOGAKUENKB
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBMEISHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSHUTOKUYMD) = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSOSHITSUYMD) = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKKB) = cABKobetsuProperty.p_strKOKUHOTISHKKB
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKB) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKHIGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOHOKENSHOKIGO) = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOHOKENSHONO) = cABKobetsuProperty.p_strKOKUHOHOKENSHONO
            'データセットにレコードを追加
            csABKokuhoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME).Rows.Add(csABKokuhoRow)

            '*****
            '*　最終行の編集
            '*
            '*****
            '連番用カウンタに１を足す
            intRecCnt += 1
            '新規レコードの作成
            csABKokuhoRow = csABKokuhoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME).NewRow
            '各項目にデータをセット
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SHICHOSONCD) = strCityCD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SHIKIBETSUID) = "AA60"
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.LASTRECKB) = "E"
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SAKUSEIYMD) = strNen
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
            'データセットにレコードを追加
            csABKokuhoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME).Rows.Add(csABKokuhoRow)

            '*****
            '*　ワークフロー送信
            '*
            '*****
            'データセット取得クラスのインスタンス化
            cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            'ワークフロー送信処理呼び出し
            cABAtenaCnvBClass.WorkFlowExec(csABKokuhoEntity, WORK_FLOW_NAME, DATA_NAME)

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
        Dim csABKokuhoTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 代納情報カラム定義
            csABKokuhoTable = New DataTable()
            csABKokuhoTable.TableName = ABKobetsuKokuhoEntity.TABLE_NAME
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.SHIKIBETSUID, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.SAKUSEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 14
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.LASTRECKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.RENBAN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHONO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 14
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 24
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOGAKUENKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 24
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHUTOKUYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSOSHITSUYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 24
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 24
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKGAITOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHIGAITOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOHOKENSHOKIGO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 32
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOHOKENSHONO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 32

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

        Return csABKokuhoTable

    End Function
    '*履歴番号 000001 2004/02/26 追加終了

End Class
