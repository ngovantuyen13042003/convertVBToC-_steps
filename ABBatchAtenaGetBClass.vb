'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢバッチ用宛名取得(ABBatchAtenaGetClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/08/21　滝沢　欽也
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2004/08/27 000001     速度改善：（宮沢）
'* 2005/01/25 000002     速度改善２：（宮沢）
'*
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports Densan.Common
Imports System.Data
Imports System.Text
Imports System.Security

Public Class ABBatchAtenaGetBClass
    Inherits ABAtenaGetBClass           ' 宛名取得Ｂクラスを継承

    ' パラメータのメンバ変数
    Protected Shadows m_cABAtenaHenshuB As ABBatchAtenaHenshuBClass             ' 宛名編集クラス(バッチ用)

    ' コンスタント定義
    Protected Shadows Const THIS_CLASS_NAME As String = "ABBatchAtenaGetBClass" ' クラス名

    '* 履歴番号 000001 2004/08/27 追加開始（宮沢）
    Private m_cfURAtenaKanriJoho As URAtenaKanriJohoBClass    '宛名管理情報Ｂクラス
    '* 履歴番号 000001 2004/08/27 追加終了


    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass)
        MyBase.New(cfControlData, cfConfigDataClass)
        m_blnBatch = True
        '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
        If Not (Me.m_cABAtenaBRef Is Nothing) Then
            Me.m_cABAtenaBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABAtenaRirekiBRef Is Nothing) Then
            Me.m_cABAtenaRirekiBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABDainoBRef Is Nothing) Then
            Me.m_cABDainoBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABSfskBRef Is Nothing) Then
            Me.m_cABSfskBRef.m_blnBatch = True
        End If
        '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
    End Sub
    '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* 　　                          ByVal blnSelectAll as boolean)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal blnSelectAll As Boolean)
        MyBase.New(cfControlData, cfConfigDataClass, blnSelectAll)
        m_blnBatch = True
        '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
        If Not (Me.m_cABAtenaBRef Is Nothing) Then
            Me.m_cABAtenaBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABAtenaRirekiBRef Is Nothing) Then
            Me.m_cABAtenaRirekiBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABDainoBRef Is Nothing) Then
            Me.m_cABDainoBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABSfskBRef Is Nothing) Then
            Me.m_cABSfskBRef.m_blnBatch = True
        End If
        '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
    End Sub
    '* 履歴番号 000002 2005/01/25 追加終了（宮沢）
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass)
        MyBase.New(cfControlData, cfConfigDataClass, cfRdbClass)
        m_blnBatch = True
        '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
        If Not (Me.m_cABAtenaBRef Is Nothing) Then
            Me.m_cABAtenaBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABAtenaRirekiBRef Is Nothing) Then
            Me.m_cABAtenaRirekiBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABDainoBRef Is Nothing) Then
            Me.m_cABDainoBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABSfskBRef Is Nothing) Then
            Me.m_cABSfskBRef.m_blnBatch = True
        End If
        '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
    End Sub

    '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* 　　                          ByVal blnSelectAll as boolean)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass,
                   ByVal blnSelectAll As Boolean)
        MyBase.New(cfControlData, cfConfigDataClass, cfRdbClass, blnSelectAll)
        m_blnBatch = True
        '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
        If Not (Me.m_cABAtenaBRef Is Nothing) Then
            Me.m_cABAtenaBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABAtenaRirekiBRef Is Nothing) Then
            Me.m_cABAtenaRirekiBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABDainoBRef Is Nothing) Then
            Me.m_cABDainoBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABSfskBRef Is Nothing) Then
            Me.m_cABSfskBRef.m_blnBatch = True
        End If
        '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
    End Sub
    '* 履歴番号 000002 2005/01/25 追加終了（宮沢）

    '************************************************************************************************
    '* メソッド名     管理情報取得（内部処理）
    '* 
    '* 構文           Private Function GetKanriJoho()
    '* 
    '* 機能　　    　　管理情報を取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    <SecuritySafeCritical>
    Protected Overrides Sub GetKanriJoho()
        Const THIS_METHOD_NAME As String = "GetKanriJoho"
        '* 履歴番号 000001 2004/08/27 削除開始（宮沢）
        'Dim cfURAtenaKanriJoho As URAtenaKanriJohoBClass    '宛名管理情報Ｂクラス
        '* 履歴番号 000001 2004/08/27 削除終了

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (m_blnKanriJoho) Then
                Exit Sub
            End If

            '管理情報クラスのインスタンス作成
            '* 履歴番号 000001 2004/08/27 更新開始（宮沢）
            'cfURAtenaKanriJoho = New URAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            If (m_cfURAtenaKanriJoho Is Nothing) Then
                m_cfURAtenaKanriJoho = New URAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            End If
            '* 履歴番号 000001 2004/08/27 更新終了

            m_intHyojiketaJuminCD = m_cfURAtenaKanriJoho.p_intHyojiketaJuminCD                '住民コード表示桁数
            m_intHyojiketaStaiCD = m_cfURAtenaKanriJoho.p_intHyojiketaSetaiCD                 '世帯コード表示桁数
            m_intHyojiketaJushoCD = m_cfURAtenaKanriJoho.p_intHyojiketaJushoCD                '住所コード表示桁数（管内のみ）
            m_intHyojiketaGyoseikuCD = m_cfURAtenaKanriJoho.p_intHyojiketaGyoseikuCD          '行政区コード表示桁数
            m_intHyojiketaChikuCD1 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD1              '地区コード１表示桁数
            m_intHyojiketaChikuCD2 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD2              '地区コード２表示桁数
            m_intHyojiketaChikuCD3 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD3              '地区コード３表示桁数
            m_strChikuCD1HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD1HyojiMeisho          '地区コード１表示名称
            m_strChikuCD2HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD2HyojiMeisho          '地区コード２表示名称
            m_strChikuCD3HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD3HyojiMeisho          '地区コード３表示名称
            m_strRenrakusaki1HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki1HyojiMeisho  '連絡先１表示名称
            m_strRenrakusaki2HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki2HyojiMeisho  '連絡先２表示名称

            ' 管理情報取得済みフラグ設定
            m_blnKanriJoho = True

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp

        End Try

    End Sub

End Class
