'************************************************************************************************
'* 業務名          宛名システム
'* 
'* クラス名        コード存在チェックＢ(ABCodeUmuCheckBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/04/21　滝沢　欽也
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/05/22 000001     RDBのConnectはﾒｿｯﾄﾞの先頭に変更(仕様変更)
'* 2010/04/16  000002      VS2008対応（比嘉）
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools

Public Class ABCodeUmuCheckBClass

    ' パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_blnCodeUmu As Boolean                         ' コード有無

    '　コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABCodeUmuCheckBClass"            'クラス名
    Private Const THIS_BUSINESSID As String = "AB"                              '業務コード

    '************************************************************************************************
    '* 各メンバ変数のプロパティ定義
    '************************************************************************************************

    Public Property p_blnCodeUmu() As Boolean
        Get
            Return m_blnCodeUmu
        End Get
        Set(ByVal Value As Boolean)
            m_blnCodeUmu = Value
        End Set
    End Property

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControldata As UFControlData, 
    '*                                 ByVal cfConfigData As UFConfigDataClass,
    '*                                 ByVal cfRdb As UFRdbClass)
    '* 
    '* 機能           初期化処理
    '* 
    '* 引数           cfControlData As UFControlData        : コントロールデータオブジェクト
    '*                  cfConfigData As UFConfigDataClass     : コンフィグデータオブジェクト
    '*                  cfRdb As UFRdbClass                   : ＲＤＢオブジェクト
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Public Sub New(ByVal cfControldata As UFControlData, _
                   ByVal cfConfigData As UFConfigDataClass)
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Const THIS_METHOD_NAME As String = "New"            'メソッド名
        '* corresponds to VS2008 End 2010/04/16 000002

        ' メンバ変数セット
        m_cfControlData = cfControldata
        m_cfConfigDataClass = cfConfigData

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' RDBクラスのインスタンス作成
        m_cfRdbClass = New UFRdbClass(THIS_BUSINESSID)

        ' メンバ変数の初期化
        m_blnCodeUmu = False
    End Sub

    '************************************************************************************************
    '* メソッド名      住民コード有無チェック
    '* 
    '* 構文           Public Sub JuminCDUmuCheck(ByVal strJuminCD As String)
    '* 
    '* 機能　　        住民コードが存在するかチェックする。
    '* 
    '* 引数           strJuminCD As String          : 住民コード
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Public Sub JuminCDUmuCheck(ByVal strJuminCD As String)
        Const THIS_METHOD_NAME As String = "JuminCDUmuCheck"
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim cAtenaB As ABAtenaBClass                        '宛名ＤＡクラス
        Dim cAtenaSearchKey As New ABAtenaSearchKey()       '宛名検索キー
        Dim csAtenaEntity As DataSet                        '宛名Entity
        Dim intDataCount As Integer

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:Connect】")
            ' RDB接続
            m_cfRdbClass.Connect()

            Try
                ' 宛名取得インスタンス化
                cAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                cAtenaSearchKey.p_strJuminCD = strJuminCD

                ' 宛名ＤＡクラスの宛名取得メゾットを実行
                csAtenaEntity = cAtenaB.GetAtenaBHoshu(1, cAtenaSearchKey, True)

                intDataCount = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count()

                ' データが０件のときは
                If (intDataCount = 0) Then
                    m_blnCodeUmu = False
                Else
                    m_blnCodeUmu = True
                End If

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
                ' UFAppExceptionをスローする
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' エラーをそのままスロー
                Throw

            Finally
                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:Disconnect】")
                ' RDB切断
                m_cfRdbClass.Disconnect()
            End Try

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp

        End Try

    End Sub

    '************************************************************************************************
    '* メソッド名      世帯コード有無チェック
    '* 
    '* 構文           Public Sub StaiCDUmuCheck(ByVal strStaiCD As String)
    '* 
    '* 機能　　        世帯コードが存在するかチェックする。
    '* 
    '* 引数           strStaiCD As String          : 世帯コード
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Public Sub StaiCDUmuCheck(ByVal strStaiCD As String)
        Const THIS_METHOD_NAME As String = "StaiCDUmuCheck"
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim cAtenaB As ABAtenaBClass                        '宛名ＤＡクラス
        Dim cAtenaSearchKey As New ABAtenaSearchKey()       '宛名検索キー
        Dim csAtenaEntity As DataSet                        '宛名Entity
        Dim intDataCount As Integer

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:Connect】")
            ' RDB接続
            m_cfRdbClass.Connect()

            Try
                ' 宛名取得インスタンス化
                cAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                cAtenaSearchKey.p_strStaiCD = strStaiCD

                ' 宛名ＤＡクラスの宛名取得メゾットを実行
                csAtenaEntity = cAtenaB.GetAtenaBHoshu(1, cAtenaSearchKey, True)

                intDataCount = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count()

                ' データが０件のときは
                If (intDataCount = 0) Then
                    m_blnCodeUmu = False
                Else
                    m_blnCodeUmu = True
                End If

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutExceptionをキャッチ
                ' ワーニングログ出力
                m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objRdbTimeOutExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objRdbTimeOutExp.Message + "】")
                ' UFAppExceptionをスローする
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' エラーをそのままスロー
                Throw

            Finally
                ' RDBアクセスログ出力
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:Disconnect】")
                ' RDB切断
                m_cfRdbClass.Disconnect()
            End Try

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            'ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            'エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            'エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp

        End Try

    End Sub

End Class
