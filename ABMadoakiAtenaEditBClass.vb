'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        窓あき封筒用宛名編集クラス（ABMadoakiAtenaEditBClass.vb）
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2023/04/24　張　一帆
'* 
'* 著作権          （株）電算
'************************************************************************************************
#Region "修正履歴"
'* 修正履歴　　履歴番号　　修正内容
'* 2023/04/24  AB-0590-1   窓あき封筒用宛名編集機能 新規作成
'*
#End Region
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

Imports Densan.FrameWork
Imports Densan.FrameWork.Tools

#Region "窓あき封筒用宛名編集クラス"

'**
'* 窓あき封筒用宛名編集クラス
'*
'* @version 1.0    2023/04/24
'* @author 張　一帆
'*
Public Class ABMadoakiAtenaEditBClass

#Region "コンスタント定義"
    'クラス名
    Public Const THIS_CLASS_NAME As String = "ABMadoakiAtenaEditBClass"

    'オーバーフロー時編集パターン
    Public Enum WhenOverflow As Short
        Edit = 0                        '編集
        ReplaceOverflowChar = 1         'オーバーフロー文字置き換え
        Empty = 2                       '空白
    End Enum

    'メンバ変数
    Private m_cfLogClass As Tools.UFLogClass                                            'ログ出力クラス
    Private m_cfControlData As UFControlData                                            'コントロールデータ
    Private m_cfConfigData As UFConfigDataClass                                         'コンフィグデータ
    Private m_cMadoakiAtenaEditParamXClass As ABMadoakiAtenaEditParamXClass             '窓あき宛名編集パラメータ
    Private m_cMadoakiAtenaLengthParamXClass As ABMadoakiAtenaLengthParamXClass         '窓あき宛名文字数・行数指示パラメータ
    Private m_shtEditPaturnWhenOverflow As Short                                        'オーバーフロー時編集方法
    Private m_strOverflowChar As String                                                 'オーバーフロー文字
    Private m_blnOverflowFG As Boolean                                                  'オーバーフローフラグ
    Private m_strYubinNO As String                                                      '郵便番号
    Private m_strShichosonMeisho As String                                              '市町村名名称
    Private m_strJuSho As String                                                        '住所
    Private m_strKatagaki As String                                                     '方書
    Private m_strSofuGyoseiku As String                                                 '送付用行政区
    Private m_blnSofuGyoseikuOverFlowFG As Boolean                                      '送付用行政区オーバーフローフラグ
    Private m_strDaino_Or_SofuShimei_Array As String()                                  '代納人/送付先氏名配列
    Private m_blnDaino_Or_SofuShimeiOverflowFG As Boolean                               '代納人/送付先氏名オーバーフローフラグ
    Private m_shtDaino_Or_SofuShimeiFont As Short                                       '代納人/送付先氏名フォント
    Private m_strHonninShimei_Array As String()                                         '本人氏名配列
    Private m_blnHonninShimeiOverFlowFG As Boolean                                      '本人氏名オーバーフローフラグ
    Private m_shtHonninShimeiFont As Short                                              '本人氏名フォント
    Private m_blnKatagakiran_StaiNusMei_EditFG As Boolean                               '方書欄世帯主編集フラグ
    Private m_strSamakata As String                                                     '様方
    Private m_strJusho_Array As String()                                                '住所配列
    Private m_blnJushoOverFlowFG As Boolean                                             '住所オーバーフローフラグ
    Private m_strKatagaki_Array As String()                                             '方書配列
    Private m_blnkatagakiOverFlowFG As Boolean                                          '方書オーバーフローフラグ

    Public Property p_shtEditPaturnWhenOverflow As Short
        Get
            Return m_shtEditPaturnWhenOverflow
        End Get
        Set(value As Short)
            m_shtEditPaturnWhenOverflow = value
        End Set
    End Property

    Public Property p_strOverflowChar As String
        Get
            Return m_strOverflowChar
        End Get
        Set(value As String)
            m_strOverflowChar = value
        End Set
    End Property

    Public Property p_blnOverflowFG As Boolean
        Get
            Return m_blnOverflowFG
        End Get
        Set(value As Boolean)
            m_blnOverflowFG = value
        End Set
    End Property
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名     コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '*                               ByVal cfConfigDataClass As UFConfigDataClass)
    '* 
    '* 機能　　       初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '*                cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass)
        '引数をメンバ変数にセットする
        m_cfControlData = cfControlData
        m_cfConfigData = cfConfigDataClass

        'メンバ変数・オーバーフロー時編集パターンに0（編集）をセットする
        m_shtEditPaturnWhenOverflow = 0

        'メンバ変数・オーバーフロー文字に全角アスタリスクをセットする
        m_strOverflowChar = "＊"

    End Sub
#End Region

#Region "窓あき宛名編集"
    '************************************************************************************************
    '* メソッド名     窓あき宛名編集
    '* 
    '* 構文           Public Function EditMadoakiAtena(ByVal cMadoakiAtenaEditParamXClass As ABMadoakiAtenaEditParamXClass,
    '*                                ByVal cMadoakiAtenaLengthParamXClass As ABMadoakiAtenaLengthParamXClass) As ABMadoakiAtenaReturnXClass
    '*
    '* 
    '* 機能　　       窓あき宛名編集
    '* 
    '* 引数           cMadoakiAtenaEditParamXClass As ABMadoakiAtenaEditParamXClass      : 窓あき宛名編集パラメータ
    '*                cMadoakiAtenaLengthParamXClass As ABMadoakiAtenaLengthParamXClass  : 窓あき宛名文字数・行数指示パラメータ
    '* 
    '* 戻り値         窓あき宛名編集結果パラメータ
    '************************************************************************************************
    Public Function EditMadoakiAtena(ByVal cMadoakiAtenaEditParamXClass As ABMadoakiAtenaEditParamXClass,
                                     ByVal cMadoakiAtenaLengthParamXClass As ABMadoakiAtenaLengthParamXClass) As ABMadoakiAtenaReturnXClass

        Const THIS_METHOD_NAME As String = "EditMadoakiAtena"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass = New Tools.UFLogClass(m_cfConfigData, m_cfControlData.m_strBusinessId)
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '1 窓あき宛名編集結果パラメータのインスタンスを生成する
            Dim cMadoakiAtenaReturnXClass As New ABMadoakiAtenaReturnXClass()

            '2 引数をメンバ変数へセットする
            m_cMadoakiAtenaEditParamXClass = cMadoakiAtenaEditParamXClass                  '窓あき宛名編集パラメータ
            m_cMadoakiAtenaLengthParamXClass = cMadoakiAtenaLengthParamXClass              '窓あき宛名文字数・行数指示パラメータ

            '3 窓あき宛名編集パラメータの空白削除（TrimMadoakiAtenaEditParam）を呼び出す
            Me.TrimMadoakiAtenaEditParam()

            '4 窓あき宛名編集パラメータのチェック（CheckMadoakiAtenaEditParam）を呼び出す
            Me.CheckMadoakiAtenaEditParam()

            '5 窓あき宛名文字数・行数指示パラメータのチェック（CheckMadoakiAtenaLengthParam）を呼び出す
            Me.CheckMadoakiAtenaLengthParam()

            '6 方書欄世帯主名編集（EditKatagakiSetainushi)を呼び出す
            Me.EditKatagakiSetainushi()

            '7 郵便番号編集（GetYubinHenshu）を呼びだす
            Me.GetYubinHenshu()

            '8 市町村名編集（EditShichosonMeisho）を呼び出す
            Me.EditShichosonMeisho()

            '9 住所編集（EditJusho）を呼び出す
            Me.EditJusho()

            '10 連結住所編集(EditJoinJusho)を呼び出す
            Me.EditJoinJusho()

            '11 方書編集（EditKatagaki）を呼び出す
            Me.EditKatagaki()

            '12 送付用行政区の編集（EditSofuGyoseiku）を呼び出す
            Me.EditSofuGyoseiku()

            '13 代納人/送付先氏名の編集（EditDainoShimei）を呼び出す
            Me.EditDainoShimei()

            '14 本人氏名の編集を行う
            If (Not String.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei) AndAlso
                Not String.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei) AndAlso
                m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei <> m_cMadoakiAtenaEditParamXClass.p_strHonninShimei) Then
                '本人氏名編集（EditHonninShimei）を呼び出す
                Me.EditHonninShimei()
            Else
                m_blnHonninShimeiOverFlowFG = False
                m_shtHonninShimeiFont = 0
                '窓あき宛名文字数・行数指示パラメータ・氏名行数＝1の場合
                If (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount = 1) Then
                    'メンバ変数・本人氏名配列を最大インデックス0で再定義し、空白をセットする
                    ReDim m_strHonninShimei_Array(0)
                    m_strHonninShimei_Array(0) = ""
                Else
                    'メンバ変数・本人氏名配列を最大インデックス1で再定義し、各配列に空白をセットする
                    ReDim m_strHonninShimei_Array(1)
                    m_strHonninShimei_Array(0) = ""
                    m_strHonninShimei_Array(1) = ""
                End If
            End If

            '15 窓あき宛名編集結果編集（EditMadoakiAtenaReturn）を呼び出す																			
            cMadoakiAtenaReturnXClass = Me.EditMadoakiAtenaReturn(cMadoakiAtenaReturnXClass)

            '16 窓あき宛名編集結果パラメータを返却する
            Return cMadoakiAtenaReturnXClass

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Function

#End Region

#Region "窓あき宛名編集パラメータの空白カット"
    '************************************************************************************************
    '* メソッド名      窓あき宛名編集パラメータの空白カット
    '* 
    '* 構文           Private Sub TrimMadoakiAtenaEditParam()
    '* 
    '* 機能　　        メンバ変数の「窓あき宛名編集パラメータ」のString項目について後ろ空白を削除する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub TrimMadoakiAtenaEditParam()

        Const THIS_METHOD_NAME As String = "TrimMadoakiAtenaEditParam"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            With m_cMadoakiAtenaEditParamXClass
                .p_strYubinNo = .p_strYubinNo.TrimEnd()                                 '郵便番号
                .p_strKenmei = .p_strKenmei.TrimEnd()                                   '県名
                .p_strGunmei = .p_strGunmei.TrimEnd()                                   '郡名
                .p_strShichosonMei = .p_strShichosonMei.TrimEnd()                       '市町村名
                .p_strJusho = .p_strJusho.TrimEnd()                                     '住所
                .p_strBanchi = .p_strBanchi.TrimEnd()                                   '番地
                .p_strKatagaki = .p_strKatagaki.TrimEnd()                               '方書
                .p_strGyoseikumei = .p_strGyoseikumei.TrimEnd()                         '行政区名
                .p_strKannaiKangaiKB = .p_strKannaiKangaiKB.TrimEnd()                   '管内・管外区分
                .p_strJushoEditPaturn_KakkoL = .p_strJushoEditPaturn_KakkoL.TrimEnd()   '住所編集方法の括弧(左)
                .p_strJushoEditPaturn_KakkoR = .p_strJushoEditPaturn_KakkoR.TrimEnd()   '住所編集方法の括弧(右)
                .p_strCNS_Samakata = .p_strCNS_Samakata.TrimEnd()                       '様方コンスタント
                .p_strDaino_Or_Sofushimei = .p_strDaino_Or_Sofushimei.TrimEnd()         '代納人/送付先氏名
                .p_strHonninShimei = .p_strHonninShimei.TrimEnd()                       '本人氏名
                .p_strCNS_Sama = .p_strCNS_Sama.TrimEnd()                               '様コンスタント
                .p_strHonninShimei_KakkoL = .p_strHonninShimei_KakkoL.TrimEnd()         '本人氏名の括弧(左)
                .p_strHonninShimei_KakkoR = .p_strHonninShimei_KakkoR.TrimEnd()         '本人氏名の括弧(右)
                .p_strCNS_Samabun = .p_strCNS_Samabun.TrimEnd()                         '様分コンスタント
                .p_strDainoKBMeisho = .p_strDainoKBMeisho.TrimEnd()                     '代納区分名称
                .p_strDainoKBMeisho_KakkoL = .p_strDainoKBMeisho_KakkoL.TrimEnd()       '代納区分名称の括弧(左)
                .p_strDainoKBMeisho_KakkoR = .p_strDainoKBMeisho_KakkoR.TrimEnd()       '代納区分名称の括弧(右)
                .p_strSofugyoseiku_KakkoL = .p_strSofugyoseiku_KakkoL.TrimEnd()         '送付用行政区の括弧(左)
                .p_strSofugyoseiku_KakkoR = .p_strSofugyoseiku_KakkoR.TrimEnd()         '送付用行政区の括弧(右)
                .p_strGyoseikuCD = .p_strGyoseikuCD.TrimEnd()                           '行政区ｺｰﾄﾞ
                .p_strStaiNusmei = .p_strStaiNusmei.TrimEnd()                           '世帯主名
                .p_strJusho_Honnin = .p_strJusho_Honnin.TrimEnd()                       '本人・住所
                .p_strBanchi_Honnin = .p_strBanchi_Honnin.TrimEnd()                     '本人・番地
                .p_strGyoseikuMei_Honnin = .p_strGyoseikuMei_Honnin.TrimEnd()           '本人・行政区名
            End With

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub

#End Region

#Region "窓あき宛名編集パラメータのチェック"
    '************************************************************************************************
    '* メソッド名      窓あき宛名編集パラメータのチェック
    '* 
    '* 構文             Private Sub CheckMadoakiAtenaEditParam()
    '* 
    '* 機能　　         メンバ変数の窓あき宛名編集パラメータの内容をチェックする
    '* 
    '* 引数             なし
    '* 
    '* 戻り値           なし
    '************************************************************************************************
    Private Sub CheckMadoakiAtenaEditParam()

        Const THIS_METHOD_NAME As String = "CheckMadoakiAtenaEditParam"         'メソッド名

        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim cfErrorClass As UFErrorClass                  'エラー処理クラス

        Try
            cfErrorClass = New UFErrorClass
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'パラメータ・管内・管外区分＝’1’（管内）　OR　’2’（管外）でない場合
            If (Not (m_cMadoakiAtenaEditParamXClass.p_strKannaiKangaiKB = ABConstClass.KANNAIKB OrElse
                m_cMadoakiAtenaEditParamXClass.p_strKannaiKangaiKB = ABConstClass.KANGAIKB)) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：管内管外）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：管内管外", objErrorStruct.m_strErrorCode)

            End If

            ' パラメータ・市町村名称編集方法＜0（空白）　OR　パラメータ・市町村名称編集方法＞3（市町村名）
            If (m_cMadoakiAtenaEditParamXClass.p_shtShichosonMeishoEditPaturn < 0 OrElse
                m_cMadoakiAtenaEditParamXClass.p_shtShichosonMeishoEditPaturn > 3) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：市町村名称編集）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：市町村名称編集", objErrorStruct.m_strErrorCode)
            End If

            ' パラメータ・住所編集方法＜1（住所）　OR　パラメータ・住所編集方法＞6（番地のみ）の場合
            If (m_cMadoakiAtenaEditParamXClass.p_shtJushoEditPaturn < 1 OrElse
                m_cMadoakiAtenaEditParamXClass.p_shtJushoEditPaturn > 6) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：住所編集）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：住所編集", objErrorStruct.m_strErrorCode)
            End If

            ' パラメータ・様方編集方法＜0（空白）　or　パラメータ・様方編集方法＞2（末尾）の場合
            If (m_cMadoakiAtenaEditParamXClass.p_shtSamakataEditPaturn < 0 OrElse
                m_cMadoakiAtenaEditParamXClass.p_shtSamakataEditPaturn > 2) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：様方編集）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：様方編集", objErrorStruct.m_strErrorCode)
            End If

            ' パラメータ・様方編集方法≠0（空白）　AND　パラメータ・様方コンスタント＝空白の場合
            If (m_cMadoakiAtenaEditParamXClass.p_shtSamakataEditPaturn <> 0 AndAlso
                m_cMadoakiAtenaEditParamXClass.p_strCNS_Samakata = String.Empty) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：様方コンスタント）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：様方コンスタント", objErrorStruct.m_strErrorCode)
            End If

            ' パラメータ・様編集方法≠0（空白）　AND　パラメータ・様コンスタント＝空白の場合
            If (m_cMadoakiAtenaEditParamXClass.p_shtSamaEditPaturn <> 0 AndAlso
                m_cMadoakiAtenaEditParamXClass.p_strCNS_Sama = String.Empty) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：様コンスタント）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：様コンスタント", objErrorStruct.m_strErrorCode)
            End If

            ' パラメータ・様編集方法＜0（空白）　or　パラメータ・様編集方法＞2（末尾）の場合
            If (m_cMadoakiAtenaEditParamXClass.p_shtSamaEditPaturn < 0 OrElse
                m_cMadoakiAtenaEditParamXClass.p_shtSamaEditPaturn > 2) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：様編集）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：様編集", objErrorStruct.m_strErrorCode)
            End If

            ' パラメータ・送付用行政区編集方法＜0（空白）　OR　パラメータ・送付用行政区編集方法＞3（行政区コード括弧）の場合
            If (m_cMadoakiAtenaEditParamXClass.p_shtSofuGyoseikuEditPaturn < 0 OrElse
                m_cMadoakiAtenaEditParamXClass.p_shtSofuGyoseikuEditPaturn > 3) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：送付用行政区編集方法）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：送付用行政区編集方法", objErrorStruct.m_strErrorCode)
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub

#End Region

#Region "窓あき宛名文字数・行数指示パラメータのチェック"
    '************************************************************************************************
    '* メソッド名      窓あき宛名文字数・行数指示パラメータのチェック
    '* 
    '* 構文             Private Sub CheckMadoakiAtenaLengthParam()
    '* 
    '* 機能　　         メンバ変数の窓あき宛名文字数・行数指示パラメータの内容をチェックする
    '* 
    '* 引数             なし
    '* 
    '* 戻り値           なし
    '************************************************************************************************
    Private Sub CheckMadoakiAtenaLengthParam()

        Const THIS_METHOD_NAME As String = "CheckMadoakiAtenaLengthParam"         'メソッド名

        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim cfErrorClass As UFErrorClass                  'エラー処理クラス

        Try
            cfErrorClass = New UFErrorClass

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'パラメータ・住所行数＜1　OR　パラメータ・住所行数＞3　の場合
            If (m_cMadoakiAtenaLengthParamXClass.p_shtJushoLineCount < 1 OrElse
                m_cMadoakiAtenaLengthParamXClass.p_shtJushoLineCount > 3) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：住所行数）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：住所行数", objErrorStruct.m_strErrorCode)
            End If

            'パラメータ・住所1行あたりの文字数　<　1　Or　パラメータ住所1行あたりの文字数>1000の場合
            If (m_cMadoakiAtenaLengthParamXClass.p_shtJushoLengthEveryLine < 1 OrElse
                m_cMadoakiAtenaLengthParamXClass.p_shtJushoLengthEveryLine > 1000) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：住所文字数）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：住所文字数", objErrorStruct.m_strErrorCode)
            End If

            'パラメータ・方書行数＜1　OR　パラメータ・方書行数＞2　の場合
            If (m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount < 1 OrElse
                m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount > 2) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：方書行数）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：方書行数", objErrorStruct.m_strErrorCode)
            End If

            'パラメータ・方書1行あたりの文字数＜1　OR　パラメータ・方書1行あたりの文字数＞1000　の場合
            If (m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline < 1 OrElse
                m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline > 1000) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：方書文字数）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：方書文字数", objErrorStruct.m_strErrorCode)
            End If

            'パラメータ・氏名行数<1　Or　パラメータ・氏名行数>2　の場合
            If (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount < 1 OrElse
                m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount > 2) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：氏名行数）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：氏名行数", objErrorStruct.m_strErrorCode)
            End If

            'パラメータ・氏名1行あたりの文字数大フォント　<　1　Or　パラメータ・氏名1行当たりの文字数大フォント　>　80　の場合
            If (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont < 1 OrElse
                m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont > 80) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：氏名文字数大）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：氏名文字数大", objErrorStruct.m_strErrorCode)
            End If

            'パラメータ・氏名1行あたりの文字数小フォント　<　1　Or　パラメータ・氏名1行当たりの文字数小フォント　>　120　の場合
            If (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont < 1 OrElse
                m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont > 120) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：氏名文字数小）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：氏名文字数小", objErrorStruct.m_strErrorCode)
            End If

            'パラメータ・氏名1行あたりの文字数小フォント　<　パラメータ・氏名1行当たりの文字数大フォント　の場合
            If (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont < m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：氏名文字数大小逆転）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：氏名文字数大小逆転", objErrorStruct.m_strErrorCode)
            End If

            'パラメータ・送付用行政区文字数<0　Or　パラメータ・送付用行政区文字数>30の場合
            If (m_cMadoakiAtenaLengthParamXClass.p_shtSofuGyoseikuLength < 0 OrElse
                m_cMadoakiAtenaLengthParamXClass.p_shtSofuGyoseikuLength > 30) Then
                ' エラー定義を取得
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003438)
                ' 例外を生成（ABE003438　パラメータエラー：氏名文字数小）
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "：送付用行政区文字数", objErrorStruct.m_strErrorCode)
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub

#End Region

#Region "方書欄世帯主名編集"
    '************************************************************************************************
    '* メソッド名       方書欄世帯主名編集
    '* 
    '* 構文             Private Sub EditKatagakiSetainushi()
    '* 
    '* 機能　　         世帯主名方書欄編集が指示された場合、方書欄に世帯主名を設定する
    '* 
    '* 引数             なし
    '* 
    '* 戻り値           なし
    '************************************************************************************************
    Private Sub EditKatagakiSetainushi()

        Const THIS_METHOD_NAME As String = "EditKatagakiSetainushi"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'メンバ変数・方書欄世帯主編集フラグにFalseをセットする
            m_blnKatagakiran_StaiNusMei_EditFG = False

            'メンバ変数・様方に空白をセットする
            m_strSamakata = String.Empty

            '窓あき宛名編集パラメータ・方書編集フラグ＝True　AND	
            '窓あき宛名編集パラメータ・世帯主名方書欄編集フラグ＝True　AND	
            '窓あき宛名編集パラメータ・方書＝空白　AND	
            '（窓あき宛名編集パラメータ・代納人/送付先氏名＝本人氏名　OR	
            '窓あき宛名編集パラメータ・代納人/送付先氏名＝空白）　AND	
            '窓あき宛名編集パラメータ・住所＝本人・住所　AND	
            '窓あき宛名編集パラメータ・番地＝本人・番地　AND	
            '窓あき宛名編集パラメータ・本人氏名≠世帯主名　AND	
            '窓あき宛名編集パラメータ・世帯主名≠空白　の場合	
            If (m_cMadoakiAtenaEditParamXClass.p_blnKatagakiFG AndAlso
                m_cMadoakiAtenaEditParamXClass.p_blnKatagakiran_StaiNusmei_EditFG AndAlso
                String.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strKatagaki) AndAlso
                (m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei = m_cMadoakiAtenaEditParamXClass.p_strHonninShimei Or
                String.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei)) AndAlso
                m_cMadoakiAtenaEditParamXClass.p_strJusho = m_cMadoakiAtenaEditParamXClass.p_strJusho_Honnin AndAlso
                m_cMadoakiAtenaEditParamXClass.p_strBanchi = m_cMadoakiAtenaEditParamXClass.p_strBanchi_Honnin AndAlso
                m_cMadoakiAtenaEditParamXClass.p_strHonninShimei <> m_cMadoakiAtenaEditParamXClass.p_strStaiNusmei AndAlso
                Not String.IsNullOrEmpty(m_cMadoakiAtenaEditParamXClass.p_strStaiNusmei)) Then
                '窓あき宛名編集パラメータ・管内・管外区分＝1（管内）　AND
                '窓あき宛名編集パラメータ・住所編集方法≧2（行政区）　AND
                '窓あき宛名編集パラメータ・住所編集方法≦5（行政区1空白）
                If (m_cMadoakiAtenaEditParamXClass.p_strKannaiKangaiKB = ABConstClass.KANNAIKB AndAlso
                    m_cMadoakiAtenaEditParamXClass.p_shtJushoEditPaturn >= 2 AndAlso
                    m_cMadoakiAtenaEditParamXClass.p_shtJushoEditPaturn <= 5) Then
                    '窓あき宛名編集パラメータ・行政区名＝窓あき宛名編集パラメータ・本人・行政区名の場合
                    If (m_cMadoakiAtenaEditParamXClass.p_strGyoseikumei = m_cMadoakiAtenaEditParamXClass.p_strGyoseikuMei_Honnin) Then
                        m_blnKatagakiran_StaiNusMei_EditFG = True
                        m_strSamakata = m_cMadoakiAtenaEditParamXClass.p_strCNS_Samakata
                        m_cMadoakiAtenaEditParamXClass.p_strKatagaki = m_cMadoakiAtenaEditParamXClass.p_strStaiNusmei
                    End If
                    '上記以外
                Else
                    m_blnKatagakiran_StaiNusMei_EditFG = True
                    m_strSamakata = m_cMadoakiAtenaEditParamXClass.p_strCNS_Samakata
                    m_cMadoakiAtenaEditParamXClass.p_strKatagaki = m_cMadoakiAtenaEditParamXClass.p_strStaiNusmei
                End If
            End If

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub
#End Region

#Region "郵便番号編集"
    '************************************************************************************************
    '* メソッド名      郵便番号編集
    '* 
    '* 構文            Private Sub GetYubinHenshu()
    '* 
    '* 機能　　        郵便番号を編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub GetYubinHenshu()

        Const THIS_METHOD_NAME As String = "GetYubinHenshu"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '窓あき宛名編集パラメータ・郵便番号の文字列長　≦　3の場合
            If (m_cMadoakiAtenaEditParamXClass.p_strYubinNo.Trim.RLength() <= 3) Then
                '窓あき宛名編集パラメータ・郵便番号をメンバ変数・郵便番号へセットする
                m_strYubinNO = m_cMadoakiAtenaEditParamXClass.p_strYubinNo.Trim
            Else
                '窓あき宛名編集パラメータ・郵便番号の先頭3桁+「-」+窓あき宛名編集パラメータ・郵便番号の4桁目以降をメンバ変数・郵便番号にセットする
                m_strYubinNO = m_cMadoakiAtenaEditParamXClass.p_strYubinNo.Trim.RSubstring(0, 3) + "-" + m_cMadoakiAtenaEditParamXClass.p_strYubinNo.Trim.RSubstring(3)
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub

#End Region

#Region "市町村名編集"
    '************************************************************************************************
    '* メソッド名      市町村名編集
    '* 
    '* 構文            Private Sub EditShichosonMeisho()
    '* 
    '* 機能　　        市町村名編集方法の指示に従い、県郡市町村名を編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub EditShichosonMeisho()

        Const THIS_METHOD_NAME As String = "EditShichosonMeisho"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            With m_cMadoakiAtenaEditParamXClass
                '窓あき宛名編集パラメータ・管内管外＝’1’（管内）の場合
                If (.p_strKannaiKangaiKB = ABConstClass.KANNAIKB) Then
                    '窓あき宛名編集パラメータ・市町村名称編集方法
                    Select Case .p_shtShichosonMeishoEditPaturn
                        Case .ShichosonMeishoEditPaturn.Empty               '0（空白）の場合
                            'メンバ変数・市町村名称は空白とする
                            m_strShichosonMeisho = String.Empty
                        Case .ShichosonMeishoEditPaturn.Kenmei              '1（県名）の場合
                            'メンバ変数・市町村名称は窓あき宛名編集パラメータ・県名+郡名+市町村名をセットする
                            m_strShichosonMeisho = .p_strKenmei + .p_strGunmei + .p_strShichosonMei
                        Case .ShichosonMeishoEditPaturn.Gunmei              '2（郡名）の場合
                            'メンバ変数・市町村名称は窓あき宛名編集パラメータ・郡名+市町村名をセットする
                            m_strShichosonMeisho = .p_strGunmei + .p_strShichosonMei
                        Case .ShichosonMeishoEditPaturn.ShichosonMei        '3（市町村名）の場合
                            'メンバ変数・市町村名称は窓あき宛名編集パラメータ市町村名をセットする
                            m_strShichosonMeisho = .p_strShichosonMei
                    End Select
                Else                                                         '上記以外の場合
                    'メンバ変数・市町村名称は空白とする
                    m_strShichosonMeisho = String.Empty
                End If
                '窓あき宛名編集パラメータ・郵便番号付加有無フラグ＝true　AND　メンバ変数・市町村名称の文字列長＞0の場合
                If (.p_blnYubinNoFG AndAlso m_strShichosonMeisho.RLength() > 0) Then
                    'メンバ変数・市町村名はメンバ変数・郵便番号+全角空白+メンバ変数・市町村名をセットする
                    m_strShichosonMeisho = m_strYubinNO + "　" + m_strShichosonMeisho
                End If
            End With

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub

#End Region

#Region "住所編集"
    '************************************************************************************************
    '* メソッド名      住所編集
    '* 
    '* 構文            Private Sub EditJusho()
    '* 
    '* 機能　　        住所編集方法の指示に従い、住所を編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub EditJusho()

        Const THIS_METHOD_NAME As String = "EditJusho"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            With m_cMadoakiAtenaEditParamXClass
                '窓あき宛名編集パラメータ・管内管外='1’（管内）の場合
                If (.p_strKannaiKangaiKB = ABConstClass.KANNAIKB) Then
                    Select Case .p_shtJushoEditPaturn                     '窓あき宛名編集パラメータ・住所編集方法
                        Case .JushoEditPaturn.Jusho                     '1（住所）の場合
                            m_strJuSho = .p_strJusho
                        Case .JushoEditPaturn.Gyoseiku                  '2（行政区）
                            '窓あき宛名編集パラメータ・行政区名≠空白の場合
                            If (.p_strGyoseikumei <> String.Empty) Then
                                'メンバ変数・住所は窓あき宛名編集パラメータ・行政区名をセットする
                                m_strJuSho = .p_strGyoseikumei
                            Else
                                'メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                                m_strJuSho = .p_strJusho
                            End If
                        Case .JushoEditPaturn.JushoKakkoGyoseiku        '3（住所（行政区））
                            '窓あき宛名編集パラメータ・行政区名≠空白　AND 窓あき宛名編集パラメータ・住所≠空白の場合
                            If (.p_strGyoseikumei.Trim <> String.Empty AndAlso
                               .p_strJusho.Trim <> String.Empty) Then
                                'メンバ変数・住所は窓あき宛名編集パラメータ・住所+住所編集方法の括弧(左)+行政区名+住所編集方法の括弧(右)をセットする
                                m_strJuSho =
                                   .p_strJusho + .p_strJushoEditPaturn_KakkoL + .p_strGyoseikumei + .p_strJushoEditPaturn_KakkoR
                            Else
                                'メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                                m_strJuSho = .p_strJusho
                            End If
                        Case .JushoEditPaturn.GyoseikuKakkoJusho        '4（行政区（住所））の場合  
                            '窓あき宛名編集パラメータ・行政区名≠空白　AND 窓あき宛名編集パラメータ・住所≠空白の場合
                            If (.p_strGyoseikumei.Trim <> String.Empty AndAlso
                                .p_strJusho.Trim <> String.Empty) Then
                                'メンバ変数・住所は窓あき宛名編集パラメータ・行政区名+住所編集方法の括弧(左)+住所+住所編集方法の括弧(右)をセットする
                                m_strJuSho =
                                    .p_strGyoseikumei + .p_strJushoEditPaturn_KakkoL + .p_strJusho + .p_strJushoEditPaturn_KakkoR
                            ElseIf .p_strJusho = String.Empty Then
                                '窓あき宛名編集パラメータ・住所＝空白　の場合
                                'メンバ変数・住所は窓あき宛名編集パラメータ・行政区名をセットする
                                m_strJuSho = .p_strGyoseikumei
                            Else
                                'メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                                m_strJuSho = .p_strJusho
                            End If
                        Case .JushoEditPaturn.GyoseikuOneBlanK          '5（行政区1空白）の場合
                            '窓あき宛名編集パラメータ・行政区名≠空白の場合
                            If (.p_strGyoseikumei <> String.Empty) Then
                                'メンバ変数・住所は窓あき宛名編集パラメータ・行政区名+全角空白をセットする
                                m_strJuSho = .p_strGyoseikumei + "　"
                            Else
                                'メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                                m_strJuSho = .p_strJusho
                            End If
                        Case .JushoEditPaturn.BanchiOnly                '6（番地のみ）の場合
                            'メンバ変数・住所は空白をセットする
                            m_strJuSho = String.Empty
                    End Select
                Else                                                    '上記以外の場合
                    'メンバ変数・住所は窓あき宛名編集パラメータ・住所をセットする
                    m_strJuSho = .p_strJusho
                End If

                '窓あき宛名編集パラメータ・郵便番号付加有無フラグ＝true　AND　メンバ変数・市町村名称の文字列長＝0の場合
                If (.p_blnYubinNoFG AndAlso m_strShichosonMeisho.RLength() = 0) Then
                    'メンバ変数・住所はメンバ変数・郵便番号+全角空白+メンバ変数・住所をセットする
                    m_strJuSho = m_strYubinNO + "　" + m_strJuSho
                End If
            End With

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub

#End Region

#Region "連結住所編集"
    '************************************************************************************************
    '* メソッド名      連結住所編集
    '* 
    '* 構文            Private Sub EditJoinJusho()
    '* 
    '* 機能　　        市町村名称、住所、番地より戻り値・住所を編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub EditJoinJusho()

        Const THIS_METHOD_NAME As String = "EditJoinJusho"         'メソッド名

        Dim strShichosonMeishoJusho As String       '変数・市町村名住所
        Dim strJushoZentai As String                '変数・住所全体

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'メンバ変数・オーバーフローフラグにFalseをセットする
            m_blnOverflowFG = False

            '変数・市町村名住所にメンバ変数・市町村名+メンバ変数・住所をセットする
            strShichosonMeishoJusho = m_strShichosonMeisho + m_strJuSho

            '変数・住所全体に変数・市町村名住所+窓あき宛名編集パラメータ・番地をセットする
            strJushoZentai = strShichosonMeishoJusho + m_cMadoakiAtenaEditParamXClass.p_strBanchi

            Select Case m_cMadoakiAtenaLengthParamXClass.p_shtJushoLineCount                      '窓あき宛名文字数・行数指示パラメータ・住所行数
                Case 1
                    '文字切れチェック(CheckOverflow)を呼び出し、先頭空白を削除してメンバ変数・住所配列(0)にセットする
                    m_strJusho_Array =
                        Me.CheckOverflow(strJushoZentai,
                                        m_cMadoakiAtenaLengthParamXClass.p_shtJushoLengthEveryLine,
                                        m_cMadoakiAtenaLengthParamXClass.p_shtJushoLineCount)
                    m_strJusho_Array(0) = m_strJusho_Array(0).TrimStart

                Case 2
                    'メンバ変数・住所配列を最大インデックス1で再定義する
                    ReDim m_strJusho_Array(1)

                    With m_cMadoakiAtenaLengthParamXClass
                        '変数・住所全体の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                        If (strJushoZentai.RLength() <= .p_shtJushoLengthEveryLine) Then
                            'メンバ変数・住所配列(0)に空白を設定する
                            m_strJusho_Array(0) = String.Empty

                            'メンバ変数・住所配列(1)に変数・住所全体を設定する
                            m_strJusho_Array(1) = strJushoZentai
                        Else
                            '変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                            '変数・市町村名住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　And
                            '窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                            If (strJushoZentai.RLength() > .p_shtJushoLengthEveryLine AndAlso
                               strShichosonMeishoJusho.RLength() <= .p_shtJushoLengthEveryLine AndAlso
                               m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= .p_shtJushoLengthEveryLine) Then
                                'メンバ変数・住所配列(0)に変数・市町村名住所を設定する
                                m_strJusho_Array(0) = strShichosonMeishoJusho

                                'メンバ変数・住所配列(1)に窓あき宛名編集パラメータ・番地を設定する
                                m_strJusho_Array(1) = m_cMadoakiAtenaEditParamXClass.p_strBanchi
                            Else
                                '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・住所配列にセットする
                                m_strJusho_Array =
                                    Me.CheckOverflow(strJushoZentai, .p_shtJushoLengthEveryLine, .p_shtJushoLineCount)
                            End If
                        End If
                    End With

                    'メンバ変数・住所配列の各配列の先頭空白を削除する
                    m_strJusho_Array(0) = m_strJusho_Array(0).TrimStart
                    m_strJusho_Array(1) = m_strJusho_Array(1).TrimStart

                Case 3
                    'メンバ変数・住所配列を最大インデックス2で再定義する
                    ReDim m_strJusho_Array(2)

                    With m_cMadoakiAtenaLengthParamXClass
                        '変数・住所全体の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                        If (strJushoZentai.RLength() <= .p_shtJushoLengthEveryLine) Then
                            'メンバ変数・住所配列(0)に空白を設定する
                            m_strJusho_Array(0) = String.Empty

                            'メンバ変数・住所配列(1)に変数・住所全体を設定する
                            m_strJusho_Array(1) = strJushoZentai

                            'メンバ変数・住所配列(2)に空白を設定する
                            m_strJusho_Array(2) = String.Empty
                        Else
                            '変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                            ' 変数・市町村名住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　And
                            '窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                            If (strJushoZentai.RLength() > .p_shtJushoLengthEveryLine AndAlso
                               strShichosonMeishoJusho.RLength() <= .p_shtJushoLengthEveryLine AndAlso
                               m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= .p_shtJushoLengthEveryLine) Then
                                'メンバ変数・住所配列(0)に空白を設定する
                                m_strJusho_Array(0) = String.Empty

                                'メンバ変数・住所配列(1)に変数・市町村名住所を設定する
                                m_strJusho_Array(1) = strShichosonMeishoJusho

                                'メンバ変数・住所配列(2)に窓あき宛名編集パラメータ・番地を設定する
                                m_strJusho_Array(2) = m_cMadoakiAtenaEditParamXClass.p_strBanchi
                            Else
                                '変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                '変数・市町村名住所の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                'メンバ変数・市町村名の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                'メンバ変数・住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                '窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                                If (strJushoZentai.RLength() > .p_shtJushoLengthEveryLine AndAlso
                                    strShichosonMeishoJusho.RLength() > .p_shtJushoLengthEveryLine AndAlso
                                    m_strShichosonMeisho.RLength() <= .p_shtJushoLengthEveryLine AndAlso
                                    m_strJuSho.RLength() <= .p_shtJushoLengthEveryLine AndAlso
                                    m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= .p_shtJushoLengthEveryLine) Then
                                    'メンバ変数・住所配列(0)にメンバ変数・市町村名を設定する
                                    m_strJusho_Array(0) = m_strShichosonMeisho

                                    'メンバ変数・住所配列(1)にメンバ変数・住所を設定する
                                    m_strJusho_Array(1) = m_strJuSho

                                    'メンバ変数・住所配列(2)に窓あき宛名編集パラメータ・番地を設定する
                                    m_strJusho_Array(2) = m_cMadoakiAtenaEditParamXClass.p_strBanchi
                                Else
                                    '変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                    '変数・市町村名住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数×2　AND
                                    '（メンバ変数・市町村名の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　OR
                                    'メンバ変数・住所の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数）　AND
                                    '窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　の場合
                                    If (strJushoZentai.RLength() > .p_shtJushoLengthEveryLine AndAlso
                                        strShichosonMeishoJusho.RLength() <= .p_shtJushoLengthEveryLine * 2 AndAlso
                                        (m_strShichosonMeisho.RLength() > .p_shtJushoLengthEveryLine OrElse
                                            m_strJuSho.RLength() > .p_shtJushoLengthEveryLine) AndAlso
                                        m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= .p_shtJushoLengthEveryLine) Then
                                        'メンバ変数・住所配列(0)に変数・市町村名住所の先頭から住所1行当たりの文字数分を設定する
                                        m_strJusho_Array(0) = strShichosonMeishoJusho.RSubstring(0, .p_shtJushoLengthEveryLine)

                                        'メンバ変数・住所配列(1)に変数・市町村名住所の住所1行当たりの文字数以降を設定する
                                        m_strJusho_Array(1) = strShichosonMeishoJusho.RSubstring(.p_shtJushoLengthEveryLine)

                                        'メンバ変数・住所配列(2)に窓あき宛名編集パラメータ・番地を設定する
                                        m_strJusho_Array(2) = m_cMadoakiAtenaEditParamXClass.p_strBanchi
                                    Else
                                        '変数・住所全体の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                        '変数・市町村名住所の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                        '窓あき宛名編集パラメータ・番地の文字列長　＞　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数　AND
                                        '窓あき宛名編集パラメータ・番地の文字列長　≦　窓あき宛名文字数・行数指示パラメータ・住所1行当たりの文字数×2　の場合
                                        If (strJushoZentai.RLength() > .p_shtJushoLengthEveryLine AndAlso
                                            strShichosonMeishoJusho.RLength() <= .p_shtJushoLengthEveryLine AndAlso
                                            m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() > .p_shtJushoLengthEveryLine AndAlso
                                            m_cMadoakiAtenaEditParamXClass.p_strBanchi.RLength() <= .p_shtJushoLengthEveryLine * 2) Then
                                            'メンバ変数・住所配列(0)に変数・市町村名住所を設定する
                                            m_strJusho_Array(0) = strShichosonMeishoJusho

                                            'メンバ変数・住所配列(1)に窓あき宛名編集パラメータ・番地の先頭から住所1行当たりの文字数分を設定する
                                            m_strJusho_Array(1) =
                                                             m_cMadoakiAtenaEditParamXClass.p_strBanchi.RSubstring(0, .p_shtJushoLengthEveryLine)

                                            'メンバ変数・住所配列(2)に窓あき宛名編集パラメータ・番地の住所1行当たりの文字数分以降を設定する
                                            m_strJusho_Array(2) =
                                                             m_cMadoakiAtenaEditParamXClass.p_strBanchi.RSubstring(.p_shtJushoLengthEveryLine)
                                        Else
                                            '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・住所配列にセットする
                                            m_strJusho_Array =
                                                Me.CheckOverflow(strJushoZentai, .p_shtJushoLengthEveryLine, .p_shtJushoLineCount)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End With

                    'メンバ変数・住所配列の各配列の先頭空白を削除する
                    m_strJusho_Array(0) = m_strJusho_Array(0).TrimStart
                    m_strJusho_Array(1) = m_strJusho_Array(1).TrimStart
                    m_strJusho_Array(2) = m_strJusho_Array(2).TrimStart

            End Select

            'メンバ変数・住所オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
            m_blnJushoOverFlowFG = m_blnOverflowFG

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub
#End Region

#Region "方書編集"
    '************************************************************************************************
    '* メソッド名      方書編集
    '* 
    '* 構文            Private Sub EditKatagaki()
    '* 
    '* 機能　　        方書編集有無フラグの指示に従い、方書を編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub EditKatagaki()

        Const THIS_METHOD_NAME As String = "EditKatagaki"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            With m_cMadoakiAtenaEditParamXClass
                '窓あき宛名編集パラメータ・方書編集有無フラグ＝Falseの場合
                If (.p_blnKatagakiFG = False) Then
                    '変数・方書は空白をセットする
                    m_strKatagaki = String.Empty
                Else
                    '変数・方書は窓あき宛名編集パラメータ・方書をセットする
                    m_strKatagaki = .p_strKatagaki
                End If

                'メンバ変数・オーバーフローフラグにFalseをセットする
                m_blnOverflowFG = False

                '窓あき宛名文字数・行数指示パラメータ・方書行数＝1の場合
                If (m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount = 1) Then

                    'メンバ変数・方書配列を最大インデックス0で再定義する
                    ReDim m_strKatagaki_Array(0)

                    Select Case .p_shtSamakataEditPaturn                  '窓あき宛名編集パラメータ・様方編集方法

                        Case .SamakataEditPaturn.Empty                  '0（空白）の場合
                            '文字切れチェック(CheckOverflow)を呼び出し、先頭空白を削除してメンバ変数・方書配列(0)にセットする
                            m_strKatagaki_Array(0) = Me.CheckOverflow(m_strKatagaki,
                                                                     m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart
                        Case .SamakataEditPaturn.OneBlank               '1（1空白）の場合
                            '全角空白＆文字列付加（PadCharOneBlank）を呼び出し、先頭空白を削除してメンバ変数・方書配列(0)にセットする
                            m_strKatagaki_Array(0) = Me.PadCharOneBlank(m_strKatagaki,
                                                                    .p_strCNS_Samakata,
                                                                    m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart
                        Case Else                                       '上記以外
                            '最後尾文字列付加（PadCharLast）を呼び出し、先頭空白を削除して、メンバ変数・方書配列(0)にセットする
                            m_strKatagaki_Array(0) = Me.PadCharLast(m_strKatagaki,
                                                                .p_strCNS_Samakata,
                                                                m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart
                    End Select

                    'メンバ変数・方書オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                    m_blnkatagakiOverFlowFG = m_blnOverflowFG
                End If

                '窓あき宛名文字数・行数指示パラメータ・方書行数＝2の場合
                If (m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount = 2) Then
                    'メンバ変数・方書配列を最大インデックス1で再定義し、各配列に空白をセットする
                    ReDim m_strKatagaki_Array(1)
                    m_strKatagaki_Array(0) = String.Empty
                    m_strKatagaki_Array(1) = String.Empty

                    Select Case .p_shtSamakataEditPaturn                      '窓あき宛名編集パラメータ・様方編集方法
                        Case .SamakataEditPaturn.Empty                      '0（空白）の場合
                            '文字切れチェック(CheckOverflow)を呼び出し、先頭空白を削除してメンバ変数・方書配列にセットする
                            m_strKatagaki_Array = Me.CheckOverflow(m_strKatagaki,
                                                                    m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline,
                                                                    m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLineCount)
                            m_strKatagaki_Array(0) = m_strKatagaki_Array(0).TrimStart
                            m_strKatagaki_Array(1) = m_strKatagaki_Array(1).TrimStart
                            If (m_blnOverflowFG = False) Then
                                'メンバ変数・方書配列(0)をメンバ変数・方書配列(1)へセットする
                                m_strKatagaki_Array(1) = m_strKatagaki_Array(0)
                                'メンバ変数・方書配列(0)をクリアする
                                m_strKatagaki_Array(0) = String.Empty
                            End If

                        Case .SamakataEditPaturn.OneBlank                   '1（1空白）の場合
                            '全角空白＆文字列付加（PadCharOneBlank）を呼び出し、先頭空白を削除してメンバ変数・方書配列(1)にセットする
                            m_strKatagaki_Array(1) = Me.PadCharOneBlank(m_strKatagaki,
                                                                        .p_strCNS_Samakata,
                                                                        m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart
                        Case Else                                           '上記以外
                            '最後尾文字列付加（PadCharLast）を呼び出し、先頭空白を削除して、メンバ変数・方書配列(1)にセットする
                            m_strKatagaki_Array(1) = Me.PadCharLast(m_strKatagaki,
                                                                    .p_strCNS_Samakata,
                                                                    m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline).TrimStart
                    End Select

                    'メンバ変数・オーバーフローフラグ＝falseの場合
                    If (m_blnOverflowFG = False) Then
                        'メンバ変数・方書オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnkatagakiOverFlowFG = m_blnOverflowFG

                    Else
                        'メンバ変数・オーバーフローフラグにFalseをセットする
                        m_blnOverflowFG = False

                        'メンバ変数・方書配列に空白をセットする
                        m_strKatagaki_Array(0) = String.Empty
                        m_strKatagaki_Array(1) = String.Empty

                        Select Case .p_shtSamakataEditPaturn                      '窓あき宛名編集パラメータ・様方編集方法
                            Case .SamakataEditPaturn.Empty                      '0（空白）の場合
                                '文字切れチェック(CheckOverflow)を呼び出し、先頭空白を削除してメンバ変数・方書配列(0)にセットする
                                m_strKatagaki_Array(0) = Me.CheckOverflow(m_strKatagaki,
                                                                             m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline * 2S).TrimStart
                            Case .SamakataEditPaturn.OneBlank                   '1（1空白）の場合
                                '全角空白＆文字列付加（PadCharOneBlank）を呼び出し、先頭空白を削除してメンバ変数・方書配列(0)にセットする
                                m_strKatagaki_Array(0) = Me.PadCharOneBlank(m_strKatagaki,
                                                                            .p_strCNS_Samakata,
                                                                            m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline * 2S).TrimStart
                            Case Else                                           '上記以外
                                '最後尾文字列付加（PadCharLast）を呼び出し、先頭空白を削除して、メンバ変数・方書配列(0)にセットする
                                m_strKatagaki_Array(0) = Me.PadCharLast(m_strKatagaki,
                                                                        .p_strCNS_Samakata,
                                                                        m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline * 2S).TrimStart
                        End Select

                        'メンバ変数・方書配列(0)の文字列長＞0　AND
                        '窓あき宛名文字数・行数指示パラメータ・方書1行あたりの文字数>0　の場合
                        If (m_strKatagaki_Array(0).RLength() > 0 And
                        m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline > 0) Then
                            'メンバ変数・方書配列(1)にメンバ変数・方書配列(0)の窓あき宛名文字数・桁数指示パラメータ・方書1行あたりの文字数以降をセットする
                            m_strKatagaki_Array(1) = m_strKatagaki_Array(0).RSubstring(m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline)

                            'メンバ変数・方書配列(0)にメンバ変数・方書配列(0)の先頭から窓あき宛名文字数・桁数指示パラメータ・方書1行あたりの文字数分をセットする
                            m_strKatagaki_Array(0) = m_strKatagaki_Array(0).RSubstring(0, m_cMadoakiAtenaLengthParamXClass.p_shtKatagakiLengthEveryline)
                        End If
                    End If

                    'メンバ変数・方書オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                    m_blnkatagakiOverFlowFG = m_blnOverflowFG

                    'メンバ変数・方書配列の先頭空白を削除する
                    m_strKatagaki_Array(0) = m_strKatagaki_Array(0).TrimStart
                    m_strKatagaki_Array(1) = m_strKatagaki_Array(1).TrimStart
                End If
            End With
            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try

    End Sub

#End Region

#Region "送付用行政区編集"
    '************************************************************************************************
    '* メソッド名      送付用行政区編集
    '* 
    '* 構文            Private Sub EditSofuGyoseiku()
    '* 
    '* 機能　　        送付用行政区編集方法の指示に従い、送付用行政区を編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub EditSofuGyoseiku()

        Const THIS_METHOD_NAME As String = "EditSofuGyoseiku"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'メンバ変数・送付用行政区オーバーフローフラグ、オーバーフローフラグにFalseをセットする
            m_blnOverflowFG = False
            m_blnSofuGyoseikuOverFlowFG = False

            With m_cMadoakiAtenaEditParamXClass
                Select Case .p_shtSofuGyoseikuEditPaturn            '窓あき宛名編集パラメータ・送付用行政区編集方法
                    Case .SofuGyoseikuEditPaturn.Empty              '0（空白）の場合
                        'メンバ変数・送付用行政区は空白とする
                        m_strSofuGyoseiku = String.Empty
                    Case .SofuGyoseikuEditPaturn.Gyoseiku           '1（行政区）の場合
                        'メンバ変数・送付用行政区は窓あき宛名編集パラメータ・行政区名をセットする
                        m_strSofuGyoseiku = .p_strGyoseikumei
                    Case .SofuGyoseikuEditPaturn.GyoseikuKakko      '2（行政区括弧）の場合
                        '窓あき宛名編集パラメータ・行政区名≠空白の場合
                        If (.p_strGyoseikumei <> String.Empty) Then
                            'メンバ変数・送付用行政区は窓あき宛名編集パラメータ・送付用行政区の括弧(左)+行政区名+送付用行政区の括弧(右)をセットする
                            m_strSofuGyoseiku =
                              .p_strSofugyoseiku_KakkoL +
                              .p_strGyoseikumei + .p_strSofugyoseiku_KakkoR
                        Else
                            'メンバ変数・送付用行政区は空白とする
                            m_strSofuGyoseiku = String.Empty
                        End If
                    Case .SofuGyoseikuEditPaturn.GyoseikuCDKakko    '3（行政区コード括弧）の場合
                        '窓あき宛名編集パラメータ・行政区コード≠空白の場合
                        If (.p_strGyoseikuCD <> String.Empty) Then
                            'メンバ変数・送付用行政区は窓あき宛名編集パラメータ・送付用行政区の括弧(左)+行政区コード+送付用行政区の括弧(右)をセットする
                            m_strSofuGyoseiku =
                               .p_strSofugyoseiku_KakkoL +
                               .p_strGyoseikuCD + .p_strSofugyoseiku_KakkoR
                        Else
                            'メンバ変数・送付用行政区は空白とする
                            m_strSofuGyoseiku = String.Empty
                        End If
                End Select
            End With

            'メンバ変数・送付用行政区の文字列長＞0の場合
            If (m_strSofuGyoseiku.RLength() > 0) Then
                'メンバ変数・送付用行政区に文字切れチェック結果(CheckOverflow)の呼出結果をセットする
                m_strSofuGyoseiku = Me.CheckOverflow(m_strSofuGyoseiku, m_cMadoakiAtenaLengthParamXClass.p_shtSofuGyoseikuLength)
                'メンバ変数・送付用行政区オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                m_blnSofuGyoseikuOverFlowFG = m_blnOverflowFG
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub

#End Region

#Region "代納人/送付先氏名の編集"
    '************************************************************************************************
    '* メソッド名      代納人/送付先氏名の編集
    '* 
    '* 構文            Private Sub EditDainoShimei()
    '* 
    '* 機能　　        代納人/送付先氏名を指示された行数・文字数に編集し、様編集方法の指示に従い敬称を付与する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub EditDainoShimei()

        Const THIS_METHOD_NAME As String = "EditDainoShimei"         'メソッド名

        Dim strDainoShimei As String        '代納氏名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '窓あき宛名編集パラメータ・代納人/送付先氏名＝空白の場合
            If (m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei = String.Empty) Then
                '変数・代納氏名に窓あき宛名編集パラメータ・本人氏名をセットする
                strDainoShimei = m_cMadoakiAtenaEditParamXClass.p_strHonninShimei
            Else
                '変数・代納氏名に窓あき宛名編集パラメータ・代納人/送付先氏名をセットする
                strDainoShimei = m_cMadoakiAtenaEditParamXClass.p_strDaino_Or_Sofushimei
            End If

            'メンバ変数・オーバーフローフラグにFalseをセットする
            m_blnOverflowFG = False

            With m_cMadoakiAtenaEditParamXClass
                '窓あき宛名文字数・行数指示パラメータ・氏名行数＝1の場合
                If (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount = 1) Then
                    'メンバ変数・代納人/送付先氏名配列を最大インデックス0で再定義する
                    ReDim m_strDaino_Or_SofuShimei_Array(0)

                    'メンバ変数・代納人/送付先氏名フォントに2（大）をセットする
                    m_shtDaino_Or_SofuShimeiFont = 2

                    Select Case .p_shtSamaEditPaturn                '窓あき宛名編集パラメータ・様編集方法
                        Case .SamaEditPaturn.Empty                  '0（空白）の場合
                            '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                            '引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                            m_strDaino_Or_SofuShimei_Array(0) =
                                 Me.CheckOverflow(strDainoShimei, m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont)
                        Case .SamaEditPaturn.OneBlank               '1（1空白）の場合
                            '全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                            '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                            m_strDaino_Or_SofuShimei_Array(0) =
                                 Me.PadCharOneBlank(strDainoShimei, .p_strCNS_Sama,
                                 m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont)
                        Case .SamaEditPaturn.Last                   '2（末尾）の場合
                            '最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                            '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                            m_strDaino_Or_SofuShimei_Array(0) =
                                 Me.PadCharLast(strDainoShimei, .p_strCNS_Sama,
                                 m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont)
                    End Select

                    'メンバ変数オーバーフローフラグ＝True　AND　
                    '窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント<氏名1行あたりの文字数小フォントの場合
                    If (m_blnOverflowFG AndAlso
                       m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont <
                       m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont) Then
                        'メンバ変数・オーバーフローフラグにFalseをセットする
                        m_blnOverflowFG = False

                        'メンバ変数・代納人/送付先氏名フォントに1（小）をセットする
                        m_shtDaino_Or_SofuShimeiFont = 1

                        Select Case .p_shtSamaEditPaturn                '窓あき宛名編集パラメータ・様編集方法
                            Case .SamaEditPaturn.Empty                  '0（空白）の場合
                                '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                '引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                                m_strDaino_Or_SofuShimei_Array(0) =
                                     Me.CheckOverflow(strDainoShimei,
                                     m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont)
                            Case .SamaEditPaturn.OneBlank               '1（1空白）の場合
                                '全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                '③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                                m_strDaino_Or_SofuShimei_Array(0) =
                                     Me.PadCharOneBlank(strDainoShimei, .p_strCNS_Sama,
                                     m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont)
                            Case .SamaEditPaturn.Last                   '2（末尾）の場合
                                '最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                                '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                '③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                                m_strDaino_Or_SofuShimei_Array(0) =
                                     Me.PadCharLast(strDainoShimei, .p_strCNS_Sama,
                                     m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont)
                        End Select
                    End If

                    'メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                    m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG
                End If

                '窓あき宛名文字数・行数指示パラメータ・氏名行数＝2の場合
                If (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount = 2) Then
                    'メンバ変数・代納人/送付先氏名配列を最大インデックス1で再定義し、空白をセットする
                    ReDim m_strDaino_Or_SofuShimei_Array(1)
                    m_strDaino_Or_SofuShimei_Array(0) = String.Empty
                    m_strDaino_Or_SofuShimei_Array(1) = String.Empty

                    'メンバ変数・代納人/送付先氏名フォントに2（大）をセットする
                    m_shtDaino_Or_SofuShimeiFont = 2


                    'フォント大で1段編集が可能か判定を行う
                    Select Case .p_shtSamaEditPaturn                '窓あき宛名編集パラメータ・様編集方法
                        Case .SamaEditPaturn.Empty                  '0（空白）の場合
                            '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                            '引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                            m_strDaino_Or_SofuShimei_Array(1) =
                                 Me.CheckOverflow(strDainoShimei,
                                 m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont)
                        Case .SamaEditPaturn.OneBlank               '1（1空白）の場合
                            '全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                            '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                            m_strDaino_Or_SofuShimei_Array(1) =
                                 Me.PadCharOneBlank(strDainoShimei, .p_strCNS_Sama,
                                 m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont)
                        Case Else                                   '2（末尾）の場合
                            '最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                            '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                            m_strDaino_Or_SofuShimei_Array(1) =
                                 Me.PadCharLast(strDainoShimei, .p_strCNS_Sama,
                                 m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont)
                    End Select

                    'メンバ変数・オーバーフローフラグ＝false　の場合
                    If (Not m_blnOverflowFG) Then
                        'メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG
                    Else
                        'メンバ変数・オーバーフローフラグにFalseをセットする
                        m_blnOverflowFG = False

                        'フォント大で2段編集が可能か判定を行う
                        Select Case .p_shtSamaEditPaturn                '窓あき宛名編集パラメータ・様編集方法
                            Case .SamaEditPaturn.Empty                  '0（空白）の場合
                                '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                '引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント×2
                                m_strDaino_Or_SofuShimei_Array(1) =
                                     Me.CheckOverflow(strDainoShimei,
                                     m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont * 2S)
                            Case .SamaEditPaturn.OneBlank               '1（1空白）の場合
                                '全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                '③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント×2
                                m_strDaino_Or_SofuShimei_Array(1) =
                                     Me.PadCharOneBlank(strDainoShimei, .p_strCNS_Sama,
                                     m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont * 2S)
                            Case Else                                   '2（末尾）の場合
                                '最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                '③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント×2
                                m_strDaino_Or_SofuShimei_Array(1) =
                                     Me.PadCharLast(strDainoShimei, .p_strCNS_Sama,
                                     m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont * 2S)
                        End Select

                        'メンバ変数・オーバーフローフラグ＝True　AND
                        '窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント<氏名1行あたりの文字数小フォントの場合
                        If (m_blnOverflowFG AndAlso
                           m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont <
                           m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont) Then
                            '処理なし
                        Else
                            'メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                            m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG

                            '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                            '引数:①メンバ変数・代納人/送付先氏名配列(1)、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント、③氏名行数
                            m_strDaino_Or_SofuShimei_Array = Me.CheckOverflow(m_strDaino_Or_SofuShimei_Array(1),
                                                                m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont,
                                                                m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount)
                        End If
                    End If
                End If

                '窓あき宛名文字数・行数指示パラメータ・氏名行数＝2　AND　メンバ変数・オーバーフローフラグ＝True　AND
                '窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント<氏名1行あたりの文字数小フォントの場合
                If (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount = 2 AndAlso
                   m_blnOverflowFG AndAlso
                   m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_LargeFont <
                   m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont) Then
                    'メンバ変数・代納人/送付先氏名フォントに1（小）をセットする
                    m_shtDaino_Or_SofuShimeiFont = 1

                    'メンバ変数・代納人/送付先氏名配列を各配列に空白をセットする
                    m_strDaino_Or_SofuShimei_Array(0) = String.Empty
                    m_strDaino_Or_SofuShimei_Array(1) = String.Empty

                    'フォント小で1段編集が可能か判定を行う
                    Select Case .p_shtSamaEditPaturn                    '窓あき宛名編集パラメータ・様編集方法
                        Case .SamaEditPaturn.Empty                      '0（空白）の場合
                            '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                            '引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                            m_strDaino_Or_SofuShimei_Array(1) =
                                 Me.CheckOverflow(strDainoShimei,
                                 m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont)
                        Case .SamaEditPaturn.OneBlank                   '1（1空白）の場合
                            '全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                            '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント
                            '、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                            m_strDaino_Or_SofuShimei_Array(1) =
                                 Me.PadCharOneBlank(strDainoShimei, .p_strCNS_Sama,
                                 m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont)
                        Case Else                                       '2（末尾）の場合
                            '最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                            '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント'
                            '、③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                            m_strDaino_Or_SofuShimei_Array(1) =
                                 Me.PadCharLast(strDainoShimei, .p_strCNS_Sama,
                                 m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont)
                    End Select

                    'メンバ変数・オーバーフローフラグ＝false　の場合
                    If (Not m_blnOverflowFG) Then
                        'メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG
                    Else
                        'メンバ変数・オーバーフローフラグにFalseをセットする
                        m_blnOverflowFG = False
                        'フォント小で2段編集が可能か判定を行う
                        Select Case .p_shtSamaEditPaturn                    '窓あき宛名編集パラメータ・様編集方法
                            Case .SamaEditPaturn.Empty                      '0（空白）の場合
                                '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                '引数:①変数・代納氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント×2
                                m_strDaino_Or_SofuShimei_Array(1) =
                                     Me.CheckOverflow(strDainoShimei,
                                     m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont * 2S)
                            Case .SamaEditPaturn.OneBlank                   '1（1空白）の場合
                                '全角空白＆文字列付加（PadCharOneBlank）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                '③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント×2
                                m_strDaino_Or_SofuShimei_Array(1) =
                                     Me.PadCharOneBlank(strDainoShimei, .p_strCNS_Sama,
                                     m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont * 2S)
                            Case Else                                       '2（末尾）の場合
                                '最後尾文字列付加（PadCharLast）を呼び出し、メンバ変数・代納人/送付先氏名配列(1)にセットする
                                '引数:①変数・代納氏名、②窓あき宛名編集パラメータ・様コンスタント、
                                '③窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント×2
                                m_strDaino_Or_SofuShimei_Array(1) =
                                     Me.PadCharLast(strDainoShimei, .p_strCNS_Sama,
                                     m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont * 2S)
                        End Select

                        'メンバ変数・代納人/送付先オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnDaino_Or_SofuShimeiOverflowFG = m_blnOverflowFG

                        '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・代納人/送付先氏名配列にセットする
                        '引数:①メンバ変数・代納人/送付先氏名配列(1)、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント、③氏名行数
                        m_strDaino_Or_SofuShimei_Array = Me.CheckOverflow(m_strDaino_Or_SofuShimei_Array(1),
                                                            m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLengthEveryLine_SmallFont,
                                                            m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount)
                    End If
                End If
            End With

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub

#End Region

#Region "本人氏名編集"
    '************************************************************************************************
    '* メソッド名      本人氏名編集
    '* 
    '* 構文            Private Sub EditHonninShimei()
    '* 
    '* 機能　　        本人氏名を指示された行数・文字数に編集する
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Private Sub EditHonninShimei()

        Const THIS_METHOD_NAME As String = "EditHonninShimei"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'メンバ変数・オーバーフローフラグにfalseをセットする
            m_blnOverflowFG = False

            With m_cMadoakiAtenaLengthParamXClass
                '窓あき宛名文字数・行数指示パラメータの氏名行数が１の場合
                If (.p_shtShimeiLineCount = 1) Then
                    'メンバ変数・本人氏名配列を最大インデックス0で再定義する
                    ReDim m_strHonninShimei_Array(0)

                    'メンバ変数・本人氏名フォントに2（大）をセットする
                    m_shtHonninShimeiFont = 2

                    '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列にセットする
                    '引数:①窓あき宛名編集パラメータ・本人氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント
                    m_strHonninShimei_Array(0) =
                        Me.CheckOverflow(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei, .p_shtShimeiLengthEveryLine_LargeFont)

                    'メンバ変数・オーバーフローフラグ＝True　AND
                    '窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント<氏名1行あたりの文字数小フォントの場合
                    If (m_blnOverflowFG AndAlso
                       .p_shtShimeiLengthEveryLine_LargeFont <
                       .p_shtShimeiLengthEveryLine_SmallFont) Then
                        'メンバ変数・オーバーフローフラグにfalseをセットする
                        m_blnOverflowFG = False
                        'メンバ変数・本人氏名フォントに1（小）をセットする
                        m_shtHonninShimeiFont = 1
                        '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列にセットする
                        '引数:①窓あき宛名編集パラメータ・本人氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント
                        m_strHonninShimei_Array(0) =
                            Me.CheckOverflow(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei, .p_shtShimeiLengthEveryLine_SmallFont)
                    End If

                    'メンバ変数・本人氏名オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                    m_blnHonninShimeiOverFlowFG = m_blnOverflowFG
                End If

                '窓あき宛名文字数・行数指示パラメータの氏名行数が2の場合
                If (.p_shtShimeiLineCount = 2) Then
                    'メンバ変数・本人氏名配列を最大インデックス1で再定義する
                    ReDim m_strHonninShimei_Array(1)

                    'メンバ変数・本人氏名フォントに2（大）をセットする
                    m_shtHonninShimeiFont = 2

                    '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列(0)にセットする
                    '引数:①窓あき宛名編集パラメータ・本人氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント×氏名行数
                    m_strHonninShimei_Array(0) =
                             Me.CheckOverflow(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei,
                             .p_shtShimeiLengthEveryLine_LargeFont * .p_shtShimeiLineCount)

                    'メンバ変数・オーバーフローフラグ＝False　OR
                    '窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント=氏名1行あたりの文字数小フォントの場合
                    If (m_blnOverflowFG = False OrElse
                   .p_shtShimeiLengthEveryLine_LargeFont = .p_shtShimeiLengthEveryLine_SmallFont) Then
                        'メンバ変数・本人氏名オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnHonninShimeiOverFlowFG = m_blnOverflowFG

                        'メンバ変数・本人氏名配列(0)の文字列長≦窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォントの場合
                        If (m_strHonninShimei_Array(0).RLength() <= .p_shtShimeiLengthEveryLine_LargeFont) Then
                            'メンバ変数・本人氏名配列(1)にメンバ変数・本人氏名配列(0)をセットする
                            m_strHonninShimei_Array(1) = m_strHonninShimei_Array(0)

                            'メンバ変数・本人氏名配列(0)に空白をセットする
                            m_strHonninShimei_Array(0) = String.Empty
                        Else
                            '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列にセットする
                            '引数:①メンバ変数・本人氏名配列(0)、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント、③氏名行数
                            m_strHonninShimei_Array = Me.CheckOverflow(m_strHonninShimei_Array(0),
                                                                         .p_shtShimeiLengthEveryLine_LargeFont,
                                                                         .p_shtShimeiLineCount)
                        End If
                    Else
                        'メンバ変数・オーバーフローフラグにfalseをセットする
                        m_blnOverflowFG = False

                        'メンバ変数・本人氏名フォントに1（小）をセットする
                        m_shtHonninShimeiFont = 1

                        '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列(0)にセットする
                        '引数:①窓あき宛名編集パラメータ・本人氏名、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント×氏名行数
                        m_strHonninShimei_Array(0) =
                             Me.CheckOverflow(m_cMadoakiAtenaEditParamXClass.p_strHonninShimei,
                                                 .p_shtShimeiLengthEveryLine_SmallFont * .p_shtShimeiLineCount)

                        'メンバ変数・本人氏名オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                        m_blnHonninShimeiOverFlowFG = m_blnOverflowFG

                        'メンバ変数・本人氏名配列(0)の文字列長≦窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォントの場合
                        If (m_strHonninShimei_Array(0).RLength() <= .p_shtShimeiLengthEveryLine_SmallFont) Then
                            'メンバ変数・本人氏名配列(1)にメンバ変数・本人氏名配列(0)をセットする
                            m_strHonninShimei_Array(1) = m_strHonninShimei_Array(0)

                            'メンバ変数・本人氏名配列(0)に空白をセットする
                            m_strHonninShimei_Array(0) = String.Empty
                        Else
                            '文字切れチェック(CheckOverflow)を呼び出し、メンバ変数・本人氏名配列にセットする
                            '引数:①メンバ変数・本人氏名配列(0)、②窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数小フォント、③氏名行数
                            m_strHonninShimei_Array = Me.CheckOverflow(m_strHonninShimei_Array(0),
                                                                         .p_shtShimeiLengthEveryLine_SmallFont,
                                                                         .p_shtShimeiLineCount)
                        End If
                    End If
                End If


                'メンバ変数・本人氏名フォント＝2（大）　AND
                '窓あき宛名文字数・行数指示パラメータ・氏名1行あたりの文字数大フォント=氏名1行あたりの文字数小フォントの場合
                If (m_shtHonninShimeiFont = 2 AndAlso
               .p_shtShimeiLengthEveryLine_LargeFont = .p_shtShimeiLengthEveryLine_SmallFont) Then
                    'メンバ変数・本人氏名フォントに1（小）をセットする
                    m_shtHonninShimeiFont = 1
                End If
            End With

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try
    End Sub

#End Region

#Region "窓あき宛名編集結果編集"
    '************************************************************************************************
    '* メソッド名      窓あき宛名編集結果編集
    '* 
    '* 構文            Private Sub EditMadoakiAtenaReturn(ByVal cMadoakiAtenaReturnXClass As ABMadoakiAtenaReturnXClass) As ABMadoakiAtenaReturnXClass
    '* 
    '* 機能　　        メンバ変数を窓あき宛名編集結果パラメータにセットする
    '* 
    '* 引数            窓あき宛名編集結果パラメータ
    '* 
    '* 戻り値          窓あき宛名編集結果パラメータ
    '************************************************************************************************
    Private Function EditMadoakiAtenaReturn(ByVal cMadoakiAtenaReturnXClass As ABMadoakiAtenaReturnXClass) As ABMadoakiAtenaReturnXClass

        Const THIS_METHOD_NAME As String = "EditMadoakiAtenaReturn"         'メソッド名

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '窓あき宛名編集結果パラメータに編集元メンバ変数をセットする
            With cMadoakiAtenaReturnXClass
                .p_strYubinNo = m_strYubinNO                                                '郵便番号
                .p_strJusho_Array = m_strJusho_Array                                        '住所配列
                .p_strKatagaki_Array = m_strKatagaki_Array                                  '方書配列
                .p_blnJushoOverFlowFG = m_blnJushoOverFlowFG                                '住所オーバーフローフラグ
                .p_blnKatagakiOverFlowFG = m_blnkatagakiOverFlowFG                          '方書オーバーフローフラグ
                .p_strSofuGyoseiku = m_strSofuGyoseiku                                      '送付行政区
                .p_blnSofuGyoseikuOverflowFG = m_blnSofuGyoseikuOverFlowFG                  '送付用行政区オーバーフローフラグ
                .p_strDaino_Or_SofuShimei_Array = m_strDaino_Or_SofuShimei_Array            '代納人/送付先氏名配列
                .p_blnDaino_Or_SofuShimeiOverFlowFG = m_blnDaino_Or_SofuShimeiOverflowFG    '代納人/送付先氏名オーバーフローフラグ
                .p_shtDaino_Or_SofuShimeiFont = m_shtDaino_Or_SofuShimeiFont                '代納人/送付先氏名フォント
                .p_strHonninShimei_Array = m_strHonninShimei_Array                          '本人氏名配列
                .p_blnHonninShimeiOverflowFG = m_blnHonninShimeiOverFlowFG                  '本人氏名オーバーフローフラグ
                .p_shtHonninShimeiFont = m_shtHonninShimeiFont                              '本人氏名フォント
                .p_blnKatagakiran_StaiNusmei_EditFG = m_blnKatagakiran_StaiNusMei_EditFG    '方書欄世帯主編集フラグ
                .p_strSamakata = m_strSamakata                                              '様方


                '代納区分名称
                'メンバ変数・本人氏名フォント＝0（空白）　OR
                '窓あき宛名編集パラメータ・代納区分名称=空白の場合
                If (m_shtHonninShimeiFont = 0 OrElse
                    m_cMadoakiAtenaEditParamXClass.p_strDainoKBMeisho = String.Empty) Then
                    '窓あき宛名編集結果パラメータ・代納区分名称に空白をセットする
                    .p_strDainoKBMeisho = String.Empty
                Else
                    '窓あき宛名編集結果パラメータ・代納区分名称に窓あき宛名編集パラメータ・代納区分名称の括弧(左)　+　
                    '　代納区分名称　+代納区分名称の括弧(右)をセットする
                    .p_strDainoKBMeisho = m_cMadoakiAtenaEditParamXClass.p_strDainoKBMeisho_KakkoL +
                                            m_cMadoakiAtenaEditParamXClass.p_strDainoKBMeisho +
                                            m_cMadoakiAtenaEditParamXClass.p_strDainoKBMeisho_KakkoR
                End If

                '本人氏名括弧上段
                'メンバ変数・本人氏名フォント≠0（空白）　AND
                '窓あき宛名文字数・行数指示パラメータ・氏名行数=2　And
                'メンバ変数・本人氏名配列(0)の文字列長>0　And
                'メンバ変数・本人氏名配列(1)の文字列長>0　
                If (m_shtHonninShimeiFont <> 0 AndAlso
                    m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount = 2 AndAlso
                    m_strHonninShimei_Array(0).RLength > 0 AndAlso
                    m_strHonninShimei_Array(1).RLength > 0) Then
                    '窓あき宛名編集結果パラメータ・本人氏名括弧上段に窓あき宛名編集パラメータ・本人氏名の括弧(左)をセットする
                    .p_strHonninShimei_KakkoHigh = m_cMadoakiAtenaEditParamXClass.p_strHonninShimei_KakkoL
                Else
                    '窓あき宛名編集結果パラメータ・本人氏名括弧上段に空白をセットする
                    .p_strHonninShimei_KakkoHigh = String.Empty
                End If

                '本人氏名括弧下段
                'メンバ変数・本人氏名フォント≠0（空白）　AND
                '（窓あき宛名文字数・行数指示パラメータ・氏名行数＝2　AND
                'メンバ変数・本人氏名配列(0)の文字列長＝0　AND
                'メンバ変数・本人氏名配列(1)の文字列長＞0　）　OR
                '（窓あき宛名文字数・行数指示パラメータ・氏名行数＝1　AND
                'メンバ変数・本人氏名配列(0)の文字列長＞0）
                If (m_shtHonninShimeiFont <> 0 AndAlso
                    (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount = 2 AndAlso
                    m_strHonninShimei_Array(0).RLength = 0 AndAlso
                    m_strHonninShimei_Array(1).RLength > 0) OrElse
                    (m_cMadoakiAtenaLengthParamXClass.p_shtShimeiLineCount = 1 AndAlso
                    m_strHonninShimei_Array(0).RLength > 0)) Then
                    '窓あき宛名編集結果パラメータ・本人氏名括弧下段に窓あき宛名編集パラメータ・本人氏名の括弧(左)をセットする
                    .p_strHonninShimei_KakkoLow = m_cMadoakiAtenaEditParamXClass.p_strHonninShimei_KakkoL
                Else
                    '窓あき宛名編集結果パラメータ・本人氏名括弧下段に空白をセットする
                    .p_strHonninShimei_KakkoLow = String.Empty
                End If

                '様分
                '窓あき宛名編集結果パラメータ・本人氏名括弧上段の文字列長＞0　OR
                '窓あき宛名編集結果パラメータ・本人氏名括弧下段の文字列長>0　
                If (.p_strHonninShimei_KakkoHigh.RLength > 0 OrElse
                    .p_strHonninShimei_KakkoLow.RLength > 0) Then
                    '窓あき宛名編集結果パラメータ・様分に窓あき宛名編集パラメータ・様分コンスタント　+　本人氏名の括弧(右)をセットする
                    .p_strSamabun = m_cMadoakiAtenaEditParamXClass.p_strCNS_Samabun +
                                        m_cMadoakiAtenaEditParamXClass.p_strHonninShimei_KakkoR
                Else
                    '窓あき宛名編集結果パラメータ・様分に空白ををセットする
                    .p_strSamabun = String.Empty
                End If

                'メンバ変数・送付用行政区オーバーフローフラグ　＝True　OR
                'メンバ変数・代納人/送付先氏名オーバーフローフラグ　=True　Or
                'メンバ変数・本人氏名オーバーフローフラグ　=True　Or
                'メンバ変数・住所オーバーフローフラグ　=True　Or
                'メンバ変数・方書オーバーフローフラグ　=True　　の場合
                If ((m_blnSofuGyoseikuOverFlowFG OrElse
                    m_blnDaino_Or_SofuShimeiOverflowFG OrElse
                    m_blnHonninShimeiOverFlowFG OrElse
                    m_blnJushoOverFlowFG OrElse
                    m_blnkatagakiOverFlowFG)) Then
                    'メンバ変数・オーバーフローフラグにTrueをセットする
                    m_blnOverflowFG = True
                Else
                    'メンバ変数・オーバーフローフラグにFalseをセットする
                    m_blnOverflowFG = False
                End If

                '窓あき宛名編集結果パラメータ・オーバーフローフラグにメンバ変数・オーバーフローフラグをセットする
                .p_blnOverflowFG = m_blnOverflowFG

            End With
            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try

        Return cMadoakiAtenaReturnXClass

    End Function

#End Region

#Region "全角空白＆文字列付加"
    '************************************************************************************************
    '* メソッド名      全角空白＆文字列付加
    '* 
    '* 構文                Private Function PadCharOneBlank(ByVal strInputChar As String,
    '*                                                      ByVal strPadChar As String,
    '*                                                      ByVal shtMaxLength As Short) As String
    '* 
    '* 機能　　        入力文字列の後ろに全角空白+付加文字を付加する
    '*                  例）入力文字列電算■太郎　、付加文字：様、文字数：10の場合、電算■太郎■様となる（■は全角空白）
    '* 
    '* 引数            入力文字列（String）、付加文字（String）、文字数（Short）
    '* 
    '* 戻り値          編集後文字列(String)
    '************************************************************************************************
    Private Function PadCharOneBlank(ByVal strInputChar As String,
                                    ByVal strPadChar As String,
                                    ByVal shtMaxLength As Short) As String

        Const THIS_METHOD_NAME As String = "PadCharOneBlank"         'メソッド名

        Dim strEditInput As String          '入力文字列
        Dim strFukaMoji As String           '付加文字
        Dim strOutChar As String            '戻り値

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '引数・入力文字列、引数・付加文字の後ろ空白を削除し、変数にセットする
            strEditInput = strInputChar.TrimEnd
            strFukaMoji = strPadChar.TrimEnd

            '変数・戻り値に空白をセットする
            strOutChar = String.Empty

            'メンバ変数・オーバーフローフラグにFalseをセットする
            m_blnOverflowFG = False

            '変数・入力文字列の文字列長＞0の場合
            If (strEditInput.RLength() > 0) Then

                '文字切れチェック（CheckOverflow）を呼び出し、変数・戻り値に戻り値をセットする
                strOutChar = Me.CheckOverflow(strEditInput, shtMaxLength)

                'メンバ変数・オーバーフローフラグ＝False　AND　変数・付加文字の文字列長＞0の場合
                If (m_blnOverflowFG = False AndAlso strFukaMoji.RLength() > 0) Then

                    '文字切れチェック（CheckOverflow）を呼び出す
                    Me.CheckOverflow(strEditInput + strFukaMoji, shtMaxLength)

                    'メンバ変数・オーバーフローフラグ＝Trueの場合
                    If (m_blnOverflowFG) Then

                        '文字切れチェック（CheckOverflow）を呼び出し、変数・戻り値に戻り値をセットする
                        strOutChar = Me.CheckOverflow(strEditInput + "＊", CType(strEditInput.RLength(), Short))
                    Else
                        '変数・入力文字列+変数・付加文字の文字列長＝変数・文字数の場合
                        If (CType(strEditInput.RLength() + strFukaMoji.RLength(), Short) = shtMaxLength) Then
                            '変数・戻り値に変数・入力文字列+変数・付加文字をセットする
                            strOutChar = strEditInput + strFukaMoji
                        Else
                            '変数・戻り値に変数・入力文字列+全角空白+変数・付加文字をセットする
                            strOutChar = strEditInput + "　" + strFukaMoji
                        End If
                    End If
                End If
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try

        Return strOutChar

    End Function

#End Region

#Region "最後尾文字列付加"
    '************************************************************************************************
    '* メソッド名      最後尾文字列付加
    '* 
    '* 構文                Private Function PadCharLast(ByVal strInputChar As String,
    '*                                                      ByVal strPadChar As String,
    '*                                                      ByVal shtMaxLength As Short) As String
    '* 
    '* 機能　　        文字数の位置に付加文字を付加して返却する		
    '*                 例）		入力文字列：電算■太郎　、付加文字：様、文字数：10の場合、電算■太郎■■■■様となる（■は全角空白）
    '* 
    '* 引数            入力文字列（String）、付加文字（String）、文字数（Short）
    '* 
    '* 戻り値          編集後文字列(String)
    '************************************************************************************************
    Private Function PadCharLast(ByVal strInputChar As String,
                                    ByVal strPadChar As String,
                                    ByVal shtMaxLength As Short) As String

        Const THIS_METHOD_NAME As String = "PadCharLast"         'メソッド名

        Dim strEditInput As String          '入力文字列
        Dim strFukaMoji As String           '付加文字
        Dim strOutChar As String            '戻り値

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '引数・入力文字列、引数・付加文字の後ろ空白を削除し、変数にセットする
            strEditInput = strInputChar.TrimEnd
            strFukaMoji = strPadChar.TrimEnd

            '変数・戻り値に空白をセットする
            strOutChar = String.Empty

            'メンバ変数・オーバーフローフラグにFalseをセットする
            m_blnOverflowFG = False

            '変数・入力文字列の文字列長＞0の場合
            If (strEditInput.RLength() > 0) Then
                '文字切れチェック（CheckOverflow）を呼び出し、変数・戻り値に戻り値をセットする
                strOutChar = Me.CheckOverflow(strEditInput, shtMaxLength)

                'メンバ変数・オーバーフローフラグ＝False　AND　変数・付加文字の文字列長＞0の場合
                If (m_blnOverflowFG = False AndAlso strFukaMoji.RLength() > 0) Then
                    '文字切れチェック（CheckOverflow）を呼び出す
                    Me.CheckOverflow(strEditInput + strFukaMoji, shtMaxLength)

                    'メンバ変数・オーバーフローフラグ＝Trueの場合
                    If (m_blnOverflowFG) Then
                        '文字切れチェック（CheckOverflow）を呼び出し、変数・戻り値に戻り値をセットする
                        strOutChar = Me.CheckOverflow(strEditInput + "＊", CType(strEditInput.RLength(), Short))
                    Else
                        '変数・入力文字列の右を引数・文字数分、全角空白埋めを行い、変数・戻り値にセットする
                        '変数・戻り値の末尾を変数・付加文字に置き換え（変数・戻り値の文字列長-変数・付加文字の文字列長の位置に付加文字を挿入）後ろ空白を削除する
                        strOutChar = strEditInput.RPadRight(shtMaxLength, CType("　", Char))
                        strOutChar = strOutChar.RInsert(strOutChar.RLength() - strFukaMoji.RLength(), strFukaMoji).TrimEnd
                    End If
                End If
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try

        Return strOutChar

    End Function

#End Region

#Region "文字切れチェック"
    '************************************************************************************************
    '* メソッド名      文字切れチェック
    '* 
    '* 構文            Private Function CheckOverflow(ByVal strInputChar As String,
    '*                                               ByVal shtMaxLength As Short) As String
    '* 
    '* 機能　　        行数１を補完して、文字切れチェックを呼び出す
    '* 
    '* 引数            入力文字列（String）、文字数（Short)
    '* 
    '* 戻り値          文字列（String）
    '************************************************************************************************
    Private Function CheckOverflow(ByVal strInputChar As String,
                                  ByVal shtMaxLength As Short) As String

        Const THIS_METHOD_NAME As String = "CheckOverflow"         'メソッド名

        Dim strOutChar_Array As String()
        Dim strOutChar As String

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '文字切れチェック（CheckOverflow）を呼び出す。
            '引数は①引数・入力文字列、②引数・文字数、③1固定
            strOutChar_Array = Me.CheckOverflow(strInputChar, shtMaxLength, 1)

            '文字切れチェックの戻り値配列の先頭を返却する
            strOutChar = strOutChar_Array(0)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try

        Return strOutChar
    End Function

#End Region

#Region "文字切れチェック"
    '************************************************************************************************
    '* メソッド名      文字切れチェック
    '* 
    '* 構文                Private Function CheckOverflow(ByVal strInputChar As String,
    '*                             ByVal shtLengthEveryLine As Short,
    '*                             ByVal shtLineCount As Short) As String()
    '* 
    '* 機能　　        引数・入力文字列を引数・行数分、引数・文字数で分割してセットする
    '* 
    '* 引数            入力文字列（String）、文字数（Short)、行数（Short)
    '* 
    '* 戻り値          文字列配列（String()）
    '************************************************************************************************
    Private Function CheckOverflow(ByVal strInputChar As String,
                                  ByVal shtLengthEveryLine As Short,
                                  ByVal shtLineCount As Short) As String()

        Const THIS_METHOD_NAME As String = "CheckOverflow"         'メソッド名

        Dim strEditInput As String
        Dim strOutChar_Array As String()
        Dim intLine As Integer
        Dim intStartIndex As Integer
        Dim intEditLengthCurrentLine As Integer
        Dim intMaxLength As Integer

        Try
            'デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '引数・入力文字列の後ろ空白を削除し、変数へセットする
            strEditInput = strInputChar.TrimEnd

            'メンバ変数・オーバーフローフラグをFalseにする
            m_blnOverflowFG = False

            '引数・行数＜1　OR　引数・文字数＜1　の場合
            If (shtLengthEveryLine < 1 OrElse
                shtLineCount < 1) Then
                'メンバ変数・オーバーフローフラグをTrueにする
                m_blnOverflowFG = True

                '戻り値配列を最大インデックス0で再定義し、空白をセットする
                ReDim strOutChar_Array(0)
                strOutChar_Array(0) = String.Empty
            Else
                '変数・最大文字数　＝　引数・文字数　×　引数・行数
                intMaxLength = shtLengthEveryLine * shtLineCount

                '変数・入力文字列の文字列長　＞　変数・最大文字数　の場合
                If (strEditInput.RLength() > intMaxLength) Then
                    'メンバ変数・オーバーフローフラグをTrueにする
                    m_blnOverflowFG = True
                End If

                '戻り値配列を最大インデックス（引数・行数-1）で再定義し、各配列に空白をセットする
                ReDim strOutChar_Array(shtLineCount - 1)
                For intLine = 0 To strOutChar_Array.Length - 1
                    strOutChar_Array(intLine) = String.Empty
                Next

                'メンバ変数・オーバーフローフラグ=true　AND　メンバ変数・オーバーフロー時編集パターン＝1(オーバーバフロー文字置き換え）
                'And　メンバ変数・オーバーフロー文字の文字列長>0の場合
                If (m_blnOverflowFG AndAlso
                   m_shtEditPaturnWhenOverflow = WhenOverflow.ReplaceOverflowChar AndAlso
                   m_strOverflowChar.RLength() > 0) Then
                    '変数・入力文字列の（変数・最大文字数-1）の位置をメンバ変数・オーバーフロー文字の1文字目で置き換える
                    strEditInput = strEditInput.RInsert(intMaxLength - 1, m_strOverflowChar.RSubstring(0, 1))
                    strEditInput = strEditInput.RRemove(strEditInput.RLength() - 1, 1)
                End If

                'メンバ変数・オーバーフローフラグ=true　AND　メンバ変数・オーバーフロー時編集パターン＝2（空白）の場合
                If (m_blnOverflowFG AndAlso
                   m_shtEditPaturnWhenOverflow = WhenOverflow.Empty) Then
                    '処理なし
                Else
                    '引数・行数分、戻り値配列に変数入力文字列を分割してセットする
                    For intLine = 0 To shtLineCount - 1
                        '開始位置　＝　行カウント　×　文字数
                        intStartIndex = intLine * shtLengthEveryLine

                        '編集文字列長　＝　変数・入力文字列の文字列長　-　開始位置
                        intEditLengthCurrentLine = strEditInput.RLength() - intStartIndex

                        '編集文字列長　＞　引数・文字数の場合
                        If (intEditLengthCurrentLine > shtLengthEveryLine) Then
                            '編集文字列長に引数・文字数をセットする
                            intEditLengthCurrentLine = shtLengthEveryLine
                        End If

                        '編集文字列長　＜　1の場合
                        If (intEditLengthCurrentLine < 1) Then
                            '処理終了
                            Exit For
                        End If

                        '戻り値配列の該当行インデックスに　変数・入力文字列の開始位置から編集文字列長分substringしてセットする
                        strOutChar_Array(intLine) = strEditInput.RSubstring(intStartIndex, intEditLengthCurrentLine)
                    Next
                End If
            End If

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            'システムエラーをスローする
            Throw objExp
        End Try

        Return strOutChar_Array
    End Function

#End Region

End Class
#End Region