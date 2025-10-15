'************************************************************************************************
'* 業務名　　　　   宛名管理システム
'* 
'* クラス名　　　   仮登録Bクラス
'* 
'* バージョン情報   Ver 1.0
'* 
'* 作成日付　　     2024/01/10
'*
'* 作成者　　　　   掛川　翔太
'* 
'* 著作権　　　　   （株）電算
'* 
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2024/01/10             【AB-0120-1】住民データ異動中の排他制御(掛川)
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

#Region "参照名前空間"

Imports Densan.Common
Imports Densan.FrameWork
#End Region

Public Class ABKariTorokuBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfControlData As UFControlData                        ' コントロールデータ
    Private m_cfConfigData As UFConfigDataClass                     ' コンフィグデータ
    Private m_cfRdb As UFRdbClass                                   ' ＲＤＢクラス
    Private m_cfError As UFErrorClass                               ' エラー処理クラス
    Private m_cABLogX As ABLogXClass                                ' ABログ出力Xクラス
    Private m_strKTorokuKBN As String                               ' 仮登録中区分
    Private m_cABKojinSeigyo As ABKojinSeigyoBClass                 ' 個人制御情報DA
    Private m_cABKojinSeigyoRireki As ABKojinSeigyoRirekiBClass     ' 個人制御情報履歴DA
    Private m_strShichosonCD As String                              ' 市町村コード
    Private m_strKTorokuMsg As String                               ' 仮登録中メッセージ
    Private m_strMsg As String                                      ' メッセージ
    Private m_strSystemYMD As String                                ' システム日付

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABKariTorokuBClass"
    Private Const ERR_MSG_SHORIKBN As String = "処理区分"               ' エラーメッセージ_処理区分
    Private Const ERR_MSG_JUMINCD As String = "住民コード"              ' エラーメッセージ_住民コード
    Private Const ERR_MSG_KOJINSEIGYO As String = "個人制御情報"        ' エラーメッセージ_個人制御情報
    Private Const ERR_MSG_KOJINSEIGYORIREKI As String = "個人制御情報"  ' エラーメッセージ_個人制御情報
    Private Const KTOROKU_MSG_TOROKUCHU As String = "仮登録中です。"    ' メッセージ_仮登録中
    Private Const KTOROKU_MSG_KOSHIN As String = "入力・更新中です。"   ' メッセージ_更新中
    Private Const SHUBETSU_KEY_20 As String = "20"                      ' 種別キー
    Private Const SHIKIBETSU_KEY_85 As String = "85"                    ' 識別キー
    Private Const ALL9_YMD As String = "99999999"                       ' 年月日オール９
#End Region
    
#Region "メソッド"

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
    '* 　　                          ByVal cfRdb As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           cfRdb as UFRdb                          : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigData As UFConfigDataClass, _
                   ByVal cfRdb As UFRdbClass)

        ' 変数の初期化
        m_cfControlData = New UFControlData()
        m_cfConfigData = New UFConfigDataClass()
        m_cfRdb = New UFRdbClass(ABConstClass.THIS_BUSINESSID)
        m_cfError = New UFErrorClass()
        m_cABLogX = New ABLogXClass(cfControlData, cfConfigData, THIS_CLASS_NAME)
        m_strKTorokuKBN = String.Empty
        m_cABKojinSeigyo = New ABKojinSeigyoBClass(cfControlData, cfConfigData, cfRdb)
        m_cABKojinSeigyoRireki = New ABKojinseigyoRirekiBClass(cfControlData, cfConfigData, cfRdb)
        m_strShichosonCD = String.Empty
        m_strKTorokuMsg = String.Empty 
        m_strMsg = String.Empty
        m_strSystemYMD = String.Empty

        ' メンバ変数セット
        m_cfConfigData = cfConfigData
        m_cfControlData = cfControlData
        m_cfRdb = cfRdb

    End Sub
#End Region

#Region "個人制御情報更新"
    '************************************************************************************************
    '* メソッド名     個人制御情報更新
    '* 
    '* 構文           Public Function KojinSeigyoKoshin(ByVal cABKariTorokuPrm As ABKariTorokuParamXClass) As Integer
    '* 
    '* 機能　　    　 個人制御情報更新を更新する
    '* 
    '* 引数           cABKariTorokuPrm：仮登録パラメータ
    '* 
    '* 戻り値         更新件数：Integer
    '************************************************************************************************
    Public Function KojinSeigyoKoshin(ByVal cABKariTorokuPrm As ABKariTorokuParamXClass) As Integer
        Const THIS_METHOD_NAME As String = "KojinSeigyoKoshin"          ' メソッド名
        Dim cfErrorClass As UFErrorClass                    'エラー処理クラス
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim cUssCityInfo As USSCityInfoClass                '市町村情報
        Dim cABKanriInfo As ABKANRIJOHOCacheBClass          'AB管理情報
        Dim cCommonClass As ABCommonClass
        Dim csKojinSeigyoRow As DataRow
        Dim csKojinSeigyoRirekiRow As DataRow
        Dim csKTourokuDS As DataSet
        Dim csKojinSeigyoDS As DataSet
        Dim csKojinSeigyoRirekiDS As DataSet
        Dim csSortDataRow As DataRow()
        Dim intRirekiNo As Integer
        Dim blnInsertFlg As Boolean
        Dim intKojinSeigyoCnt As Integer
        Dim intKojinSeigyoRirekiCnt As Integer

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            'パラメータチェック
            With cABKariTorokuPrm
                '仮登録パラメータ・処理区分が「1」「2」以外の場合
                If (Not((.p_strShoriKBN.Trim = ABKariTorokuParamXClass.SHORIKBN_HAITA_KAISHI) OrElse (.p_strShoriKBN.Trim = ABKariTorokuParamXClass.SHORIKBN_HAITA_KAIJO))) Then

                    'エラー定義を取得
                    cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_SHORIKBN, objErrorStruct.m_strErrorCode)
                End If

                '仮登録パラメータ・住民コードが空白の場合
                If (.p_strJuminCD.Trim = String.Empty) Then

                    'エラー定義を取得
                    cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_JUMINCD, objErrorStruct.m_strErrorCode)
                End If

                'メンバ変数.仮登録中区分の設定
                m_strKTorokuKBN = .p_strKariTorokuKb.Trim
                '仮登録パラメータ・処理区分＝「1」(排他開始)　AND　仮登録パラメータ・仮登録中区分＝空白の場合
                If((.p_strShoriKBN.Trim = ABKariTorokuParamXClass.SHORIKBN_HAITA_KAISHI) AndAlso (.p_strKariTorokuKb.Trim = String.Empty)) Then
                    m_strKTorokuKBN = ABKariTorokuParamXClass.KTOROKUKBN_KTOROKUCHU
                End If
            End With

            'メンバ変数のセット
            'メンバ変数・個人制御情報DAがnothingの場合
            If(m_cABKojinSeigyo Is Nothing) Then
                m_cABKojinSeigyo = New ABKojinSeigyoBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            End If

            'メンバ変数・個人制御情報履歴DAがnothingの場合
            If(m_cABKojinSeigyoRireki Is Nothing) Then
                m_cABKojinSeigyoRireki = New ABKojinseigyoRirekiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            End If

            'メンバ変数・市町村コード＝空白の場合
            If(m_strShichosonCD.Trim = String.Empty) 
                cUssCityInfo = New USSCityInfoClass()
                cUssCityInfo.GetCityInfo(m_cfControlData)
                m_strShichosonCD = cUssCityInfo.p_strShichosonCD(0)
            End If

            'メンバ変数・仮登録中メッセージ＝空白の場合
            If(m_strKTorokuMsg.Trim = String.Empty) Then
                cABKanriInfo = New ABKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
                csKTourokuDS = cABKanriInfo.GetKanriJohoHoshu(SHUBETSU_KEY_20, SHIKIBETSU_KEY_85)
            
                '取得できた場合
                If (csKTourokuDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then
                    m_strKTorokuMsg = CStr(csKTourokuDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
                End If
            End If

            'メンバ変数・仮登録中区分＝「1」(仮登録中)の場合
            If(m_strKTorokuKBN.Trim = ABKariTorokuParamXClass.KTOROKUKBN_KTOROKUCHU) Then
                'メンバ変数・仮登録中メッセージ≠空白の場合
                If (m_strKTorokuMsg.Trim <> String.Empty) Then
                    m_strMsg = m_strKTorokuMsg
                Else
                    m_strMsg = KTOROKU_MSG_TOROKUCHU
                End If
            Else
                m_strMsg = KTOROKU_MSG_KOSHIN
            End If

            '個人制御情報を取得する。
            csKojinSeigyoDS = m_cABKojinSeigyo.GetABKojinSeigyo(cABKariTorokuPrm.p_strJuminCD)
            '個人制御情報が取得できなかった場合
            If (csKojinSeigyoDS.Tables(ABKojinseigyomstEntity.TABLE_NAME).Rows.Count = 0) Then

                blnInsertFlg = True

                '仮登録パラメータ・処理区分＝2(排他解除)の場合
                If (cABKariTorokuPrm.p_strShoriKBN.Trim = ABKariTorokuParamXClass.SHORIKBN_HAITA_KAIJO) Then
                    cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001039)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                End If
            Else
                blnInsertFlg = False
            End If

            'システム日付(YYYYMMDD)を取得し、メンバ変数にセットする。
            m_strSystemYMD = m_cfRdb.GetSystemDate().ToString("yyyyMMdd")


            '個人制御情報を編集する。
            cCommonClass = New ABCommonClass()
            If (blnInsertFlg = True) Then
                csKojinSeigyoRow = csKojinSeigyoDS.Tables(ABKojinseigyomstEntity.TABLE_NAME).NewRow
                csKojinSeigyoRow.BeginEdit()
                cCommonClass.InitColumnValue(csKojinSeigyoRow)
                csKojinSeigyoRow = EditKojinSeigyoInfo(csKojinSeigyoRow, cABKariTorokuPrm, blnInsertFlg)
                csKojinSeigyoRow.EndEdit()
            Else
                csKojinSeigyoRow = csKojinSeigyoDS.Tables(ABKojinseigyomstEntity.TABLE_NAME).Rows(0)
                csKojinSeigyoRow.BeginEdit()
                csKojinSeigyoRow = EditKojinSeigyoInfo(csKojinSeigyoRow, cABKariTorokuPrm, blnInsertFlg)
                csKojinSeigyoRow.EndEdit()
            End If

            '個人制御情報履歴を取得する。
            csKojinSeigyoRirekiDS = m_cABKojinSeigyoRireki.GetKojinseigyoRireki(cABKariTorokuPrm.p_strJuminCD)
            If (csKojinSeigyoRirekiDS.Tables(ABKojinseigyoRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
                intRirekiNo = 1
            Else
                csSortDataRow = csKojinSeigyoRirekiDS.Tables(ABKojinseigyoRirekiEntity.TABLE_NAME).Select(String.Empty, _
                                                                                          ABKojinseigyoRirekiEntity.RIREKINO + " DESC, " _
                                                                                          + ABKojinseigyoRirekiEntity.RIREKIEDABAN + " DESC ")
                
                intRirekiNo = CInt(csSortDataRow(0).Item(ABKojinseigyoRirekiEntity.RIREKINO).ToString()) + 1
            End If

            '個人制御情報履歴を編集する
            csKojinSeigyoRirekiRow = csKojinSeigyoRirekiDS.Tables(ABKojinseigyoRirekiEntity.TABLE_NAME).NewRow()
            csKojinSeigyoRirekiRow.BeginEdit()
            cCommonClass.InitColumnValue(csKojinSeigyoRirekiRow)
            csKojinSeigyoRirekiRow = EditKojinSeigyoRirekiInfo(csKojinSeigyoRirekiRow, csKojinSeigyoRow, intRirekiNo)
            csKojinSeigyoRirekiRow.EndEdit()

            '更新処理
            If (blnInsertFlg = True) Then
                intKojinSeigyoCnt = m_cABKojinSeigyo.InsertKojinSeigyo(csKojinSeigyoRow)
                If (intKojinSeigyoCnt = 0) Then
                    
                    'エラー定義を取得
                    cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYO, objErrorStruct.m_strErrorCode)
                End If
            Else
                intKojinSeigyoCnt = m_cABKojinSeigyo.UpdateKojinSeigyo(csKojinSeigyoRow)
                If (intKojinSeigyoCnt = 0) Then
                    
                    'エラー定義を取得
                    cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001048)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYO, objErrorStruct.m_strErrorCode)
                End If
            End If
            
            '個人制御情報履歴をInsertする
            intKojinSeigyoRirekiCnt = m_cABKojinSeigyoRireki.InsertKojinseigyoRireki(csKojinSeigyoRirekiRow)
            If (intKojinSeigyoRirekiCnt = 0) Then
                    
                'エラー定義を取得
                cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYORIREKI, objErrorStruct.m_strErrorCode)
            End If

            ' デバッグログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, objRdbTimeOutExp.p_strErrorCode, objRdbTimeOutExp.Message)
            ' UFAppExceptionをスローする
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message)
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message)
            ' システムエラーをスローする
            Throw exException

        End Try

        Return intKojinSeigyoCnt

    End Function
#End Region

#Region "個人制御情報編集"
    '************************************************************************************************
    '* メソッド名     個人制御情報編集
    '* 
    '* 構文           Public Function EditKojinSeigyoInfo(ByVal csKojinSeigyoRow As DataRow,
    '*                                                    ByVal cABKariTorokuPrm As ABKariTorokuParamXClass,
    '*                                                    ByVal blnInsertFlg As Boolean) As DataRow
    '* 
    '* 機能　　    　 個人制御情報を編集する
    '* 
    '* 引数           csKojinSeigyoRow：個人制御情報
    '*                cABKariTorokuPrm ：仮登録パラメータ
    '*                blnInsertFlg：挿入フラグ
    '* 
    '* 戻り値         個人制御情報(編集後)：DataRow
    '************************************************************************************************
    Public Function EditKojinSeigyoInfo(ByVal csKojinSeigyoRow As DataRow, ByVal cABKariTorokuPrm As ABKariTorokuParamXClass, ByVal blnInsertFlg As Boolean) As DataRow
        Const THIS_METHOD_NAME As String = "EditKojinSeigyoInfo"          ' メソッド名

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)
            
            '追加の場合
            If(blnInsertFlg = True) Then
                csKojinSeigyoRow(ABKojinseigyomstEntity.JUMINCD) = cABKariTorokuPrm.p_strJuminCD        '住民コード
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHICHOSONCD) = m_strShichosonCD                 '市町村コード
                csKojinSeigyoRow(ABKojinseigyomstEntity.KYUSHICHOSONCD) = m_strShichosonCD              '旧市町村コード
                csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOKB) = String.Empty                      'ＤＶ対象区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOMSG) = String.Empty                     'ＤＶ対象メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOSHINSEIYMD) = String.Empty              'ＤＶ対象申請日
                csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOKAISHIYMD) = String.Empty               'ＤＶ対象開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOSHURYOYMD) = String.Empty               'ＤＶ対象終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.HAKKOTEISHIKB) = String.Empty                   '発行停止区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.HAKKOTEISHIMSG) = String.Empty                  '発行停止メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD) = String.Empty            '発行停止開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD) = String.Empty            '発行停止終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.JITTAICHOSAKB) = String.Empty                   '実態調査区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.JITTAICHOSAMSG) = String.Empty                  '実態調査メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD) = String.Empty            '実態調査開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD) = String.Empty            '実態調査終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENKB) = String.Empty                   '成年後見区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENMSG) = String.Empty                  '成年後見メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD) = String.Empty            '成年後見開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD) = String.Empty            '成年後見終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = String.Empty    '成年被後見人の審判確定日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD) = String.Empty         '成年被後見人の登記日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD) = String.Empty       '成年被後見人である旨を知った日
                csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUKB) = m_strKTorokuKBN                 '仮登録中区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUMSG) = m_strMsg                       '仮登録中メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUKAISHIYMD) = m_strSystemYMD           '仮登録中開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUSHURYOYMD) = ALL9_YMD                 '仮登録中終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUYOSHIKB) = String.Empty                '特別養子区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUYOSHIMSG) = String.Empty               '特別養子メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD) = String.Empty         '特別養子開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD) = String.Empty         '特別養子終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUJIJOKB) = String.Empty                 '特別事情区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUJIJOMSG) = String.Empty                '特別事情メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUJIJOKAISHIYMD) = String.Empty          '特別事情開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUJIJOSHURYOYMD) = String.Empty          '特別事情終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI1KB) = String.Empty                    '処理注意1区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI1MSG) = String.Empty                   '処理注意1メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD) = String.Empty             '処理注意1開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD) = String.Empty             '処理注意1終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI2KB) = String.Empty                    '処理注意2区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI2MSG) = String.Empty                   '処理注意2メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD) = String.Empty             '処理注意2開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD) = String.Empty             '処理注意2終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.GYOMUCD_CHUI) = String.Empty                    '業務コード注意
                csKojinSeigyoRow(ABKojinseigyomstEntity.GYOMUSHOSAICD_CHUI) = String.Empty              '業務詳細（税目）コード注意
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI3KB) = String.Empty                    '処理注意3区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI3MSG) = String.Empty                   '処理注意3メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD) = String.Empty             '処理注意3開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD) = String.Empty             '処理注意3終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORIHORYUKB) = String.Empty                    '処理保留区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORIHORYUMSG) = String.Empty                   '処理保留メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD) = String.Empty             '処理保留開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD) = String.Empty             '処理保留終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.GYOMUCD_HORYU) = String.Empty                   '業務コード保留
                csKojinSeigyoRow(ABKojinseigyomstEntity.GYOMUSHOSAICD_HORYU) = String.Empty             '業務詳細（税目）コード保留
                csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKAKB) = String.Empty                    '他業務不可区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKAMSG) = String.Empty                   '他業務不可メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD) = String.Empty             '他業務不可開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD) = String.Empty             '他業務不可終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKATOROKUGYOMUCD) = String.Empty         '登録業務コード
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA1KB) = String.Empty                       'その他１区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA1MSG) = String.Empty                      'その他１メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA1KAISHIYMD) = String.Empty                'その他１開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA1SHURYOYMD) = String.Empty                'その他１終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA2KB) = String.Empty                       'その他２区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA2MSG) = String.Empty                      'その他２メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA2KAISHIYMD) = String.Empty                'その他２開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA2SHURYOYMD) = String.Empty                'その他２終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA3KB) = String.Empty                       'その他３区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA3MSG) = String.Empty                      'その他３メッセージ
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA3KAISHIYMD) = String.Empty                'その他３開始日
                csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA3SHURYOYMD) = String.Empty                'その他３終了日
                csKojinSeigyoRow(ABKojinseigyomstEntity.KINSHIKAIJOKB) = String.Empty                   '禁止解除区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.SETAIYOKUSHIKB) = String.Empty                  '世帯抑止区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOSTYMD) = String.Empty                '一時解除開始年月日
                csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOSTTIME) = String.Empty               '一時解除開始時刻
                csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOEDYMD) = String.Empty                '一時解除終了年月日
                csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOEDTIME) = String.Empty               '一時解除終了時刻
                csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOUSER) = String.Empty                 '一時解除設定操作者ID
                csKojinSeigyoRow(ABKojinseigyomstEntity.KANRIKB) = String.Empty                         '管理区分
                csKojinSeigyoRow(ABKojinseigyomstEntity.BIKO) = String.Empty                            '備考
                csKojinSeigyoRow(ABKojinseigyomstEntity.RESERVE) = String.Empty                         'リザーブ

            '更新の場合
            Else
                '仮登録パラメータ・処理区分＝1(排他開始)の場合
                If (cABKariTorokuPrm.p_strShoriKBN.Trim = ABKariTorokuParamXClass.SHORIKBN_HAITA_KAISHI) Then
                    csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUKB) = m_strKTorokuKBN             '仮登録中区分
                    csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUMSG) = m_strMsg                   '仮登録中メッセージ
                    csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUKAISHIYMD) = m_strSystemYMD       '仮登録中開始日
                    csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUSHURYOYMD) = ALL9_YMD             '仮登録中終了日
                '仮登録パラメータ・処理区分＝2(排他終了)の場合
                Else If(cABKariTorokuPrm.p_strShoriKBN.Trim = ABKariTorokuParamXClass.SHORIKBN_HAITA_KAIJO) Then
                    csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUKB) = String.Empty                '仮登録中区分
                    csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUMSG) = String.Empty               '仮登録中メッセージ
                    csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUKAISHIYMD) = String.Empty         '仮登録中開始日
                    csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUSHURYOYMD) = String.Empty         '仮登録中終了日
                End If
            End If

            ' デバッグログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, objRdbTimeOutExp.p_strErrorCode, objRdbTimeOutExp.Message)
            ' UFAppExceptionをスローする
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message)
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message)
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csKojinSeigyoRow

    End Function
#End Region

#Region "個人制御履歴情報編集"
    '************************************************************************************************
    '* メソッド名     個人制御履歴情報編集
    '* 
    '* 構文           Public Function EditKojinSeigyoRirekiInfo(ByVal csKojinSeigyoRirekiRow As DataRow,
    '*                                                          ByVal csKojinSeigyoRow As DataRow ,
    '*                                                          ByVal intRirekiNo As Integer) As DataRow
    '* 
    '* 機能　　    　 個人制御履歴情報編集を編集する
    '* 
    '* 引数           csKojinSeigyoRirekiRow：個人制御履歴情報
    '*                csKojinSeigyoRow：個人制御情報
    '*                intRirekiNo：履歴番号
    '* 
    '* 戻り値         個人制御履歴情報編集(編集後)：DataRow
    '************************************************************************************************
    Public Function EditKojinSeigyoRirekiInfo(ByVal csKojinSeigyoRirekiRow As DataRow, ByVal csKojinSeigyoRow As DataRow , ByVal intRirekiNo As Integer) As DataRow
        Const THIS_METHOD_NAME As String = "EditKojinSeigyoRirekiInfo"          ' メソッド名

        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)
            
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.JUMINCD) = csKojinSeigyoRow(ABKojinseigyomstEntity.JUMINCD)                                            '住民コード
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHICHOSONCD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHICHOSONCD)                                    '市町村コード
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.KYUSHICHOSONCD) = csKojinSeigyoRow(ABKojinseigyomstEntity.KYUSHICHOSONCD)                              '旧市町村コード
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.RIREKINO) = CDec(intRirekiNo)                                                                          '履歴番号
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.RIREKIEDABAN) = Decimal.Zero                                                                           '履歴枝番
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.DVTAISHOKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOKB)                                      'ＤＶ対象区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.DVTAISHOMSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOMSG)                                    'ＤＶ対象メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.DVTAISHOSHINSEIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOSHINSEIYMD)                      'ＤＶ対象申請日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.DVTAISHOKAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOKAISHIYMD)                        'ＤＶ対象開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.DVTAISHOSHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.DVTAISHOSHURYOYMD)                        'ＤＶ対象終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.HAKKOTEISHIKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.HAKKOTEISHIKB)                                '発行停止区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.HAKKOTEISHIMSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.HAKKOTEISHIMSG)                              '発行停止メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.HAKKOTEISHIKAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD)                  '発行停止開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.HAKKOTEISHISHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD)                  '発行停止終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.JITTAICHOSAKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.JITTAICHOSAKB)                                '実態調査区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.JITTAICHOSAMSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.JITTAICHOSAMSG)                              '実態調査メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.JITTAICHOSAKAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD)                  '実態調査開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.JITTAICHOSASHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD)                  '実態調査終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SEINENKOKENKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENKB)                                '成年後見区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SEINENKOKENMSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENMSG)                              '成年後見メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SEINENKOKENKAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD)                  '成年後見開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SEINENKOKENSHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD)                  '成年後見終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD)  '成年被後見人の審判確定日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SEINENHIKOKENNINTOKIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD)            '成年被後見人の登記日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SEINENHIKOKENNINSHITTAYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD)        '成年被後見人である旨を知った日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.KARITOROKUKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUKB)                                  '仮登録中区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.KARITOROKUMSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUMSG)                                '仮登録中メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.KARITOROKUKAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUKAISHIYMD)                    '仮登録中開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.KARITOROKUSHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.KARITOROKUSHURYOYMD)                    '仮登録中終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUYOSHIKB)                          '特別養子区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIMSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUYOSHIMSG)                        '特別養子メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIKAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD)            '特別養子開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHISHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD)            '特別養子終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUJIJOKB)                            '特別事情区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOMSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUJIJOMSG)                          '特別事情メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOKAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUJIJOKAISHIYMD)              '特別事情開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOSHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.TOKUBETSUJIJOSHURYOYMD)              '特別事情終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI1KB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI1KB)                                  '処理注意1区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI1MSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI1MSG)                                '処理注意1メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI1KAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD)                    '処理注意1開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI1SHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD)                    '処理注意1終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI2KB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI2KB)                                  '処理注意2区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI2MSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI2MSG)                                '処理注意2メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI2KAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD)                    '処理注意2開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI2SHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD)                    '処理注意2終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.GYOMUCD_CHUI) = csKojinSeigyoRow(ABKojinseigyomstEntity.GYOMUCD_CHUI)                                  '業務コード注意
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.GYOMUSHOSAICD_CHUI) = csKojinSeigyoRow(ABKojinseigyomstEntity.GYOMUSHOSAICD_CHUI)                      '業務詳細（税目）コード注意
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI3KB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI3KB)                                  '処理注意3区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI3MSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI3MSG)                                '処理注意3メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI3KAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD)                    '処理注意3開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORICHUI3SHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD)                    '処理注意3終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORIHORYUKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORIHORYUKB)                                  '処理保留区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORIHORYUMSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORIHORYUMSG)                                '処理保留メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORIHORYUKAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD)                    '処理保留開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SHORIHORYUSHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD)                    '処理保留終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.GYOMUCD_HORYU) = csKojinSeigyoRow(ABKojinseigyomstEntity.GYOMUCD_HORYU)                                '業務コード保留
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.GYOMUSHOSAICD_HORYU) = csKojinSeigyoRow(ABKojinseigyomstEntity.GYOMUSHOSAICD_HORYU)                    '業務詳細（税目）コード保留
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SANSHOFUKAKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKAKB)                                  '他業務不可区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SANSHOFUKAMSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKAMSG)                                '他業務不可メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SANSHOFUKAKAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD)                    '他業務不可開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SANSHOFUKASHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD)                    '他業務不可終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SANSHOFUKATOROKUGYOMUCD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SANSHOFUKATOROKUGYOMUCD)            '登録業務コード
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA1KB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA1KB)                                        'その他１区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA1MSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA1MSG)                                      'その他１メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA1KAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA1KAISHIYMD)                          'その他１開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA1SHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA1SHURYOYMD)                          'その他１終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA2KB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA2KB)                                        'その他２区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA2MSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA2MSG)                                      'その他２メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA2KAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA2KAISHIYMD)                          'その他２開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA2SHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA2SHURYOYMD)                          'その他２終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA3KB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA3KB)                                        'その他３区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA3MSG) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA3MSG)                                      'その他３メッセージ
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA3KAISHIYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA3KAISHIYMD)                          'その他３開始日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SONOTA3SHURYOYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.SONOTA3SHURYOYMD)                          'その他３終了日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.KINSHIKAIJOKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.KINSHIKAIJOKB)                                '禁止解除区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.SETAIYOKUSHIKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.SETAIYOKUSHIKB)                              '世帯抑止区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.ICHIJIKAIJOSTYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOSTYMD)                          '一時解除開始年月日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.ICHIJIKAIJOSTTIME) = csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOSTTIME)                        '一時解除開始時刻
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.ICHIJIKAIJOEDYMD) = csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOEDYMD)                          '一時解除終了年月日
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.ICHIJIKAIJOEDTIME) = csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOEDTIME)                        '一時解除終了時刻
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.ICHIJIKAIJOUSER) = csKojinSeigyoRow(ABKojinseigyomstEntity.ICHIJIKAIJOUSER)                            '一時解除設定操作者ID
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.KANRIKB) = csKojinSeigyoRow(ABKojinseigyomstEntity.KANRIKB)                                            '管理区分
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.BIKO) = csKojinSeigyoRow(ABKojinseigyomstEntity.BIKO)                                                  '備考
            csKojinSeigyoRirekiRow(ABKojinseigyoRirekiEntity.RESERVE) = csKojinSeigyoRow(ABKojinseigyomstEntity.RESERVE)                                            'リザーブ

            ' デバッグログ出力
            m_cABLogX.DebugEndWrite(THIS_METHOD_NAME)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, objRdbTimeOutExp.p_strErrorCode, objRdbTimeOutExp.Message)
            ' UFAppExceptionをスローする
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cABLogX.WarningWrite(THIS_METHOD_NAME, exAppException.p_strErrorCode, exAppException.Message)
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cABLogX.ErrorWrite(THIS_METHOD_NAME, exException.Message)
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csKojinSeigyoRirekiRow

    End Function
#End Region
#End Region

End Class
