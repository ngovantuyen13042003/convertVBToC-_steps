'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        宛名空番取得(ABAkibanShutokuBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/20　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2004/04/19  000001      住民コード取得(宛名法人用)・(宛名共有用)処理追加 
'* 2007/02/05  000002      宛名更新エラーログ番号取得処理追加（内山(堅)）
'* 2007/04/02  000003      コード取得時の存在チェック処理を修正（比嘉）
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.Common
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Text

Public Class ABAkibanShutokuBClass

    ' メンバ変数の定義
    Private m_cfUFLogClass As UFLogClass            'ログ出力クラス
    Private m_cfUFControlData As UFControlData      'コントロールデータ

    'パラメータのメンバ変数
    Private m_strBango As String                    '取得番号

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAkibanShutokuBClass"

    '各メンバ変数のプロパティ定義
    Public ReadOnly Property p_strBango() As String
        Get
            Return m_strBango
        End Get
    End Property

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigData As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfUFControlData As UFControlData         : コントロールデータオブジェクト
    '*                 cfUFConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass)

        'メンバ変数セット
        m_cfUFControlData = cfControlData

        'ログ出力クラスのインスタンス化
        m_cfUFLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

        'パラメータのメンバ変数
        m_strBango = String.Empty
    End Sub

    '************************************************************************************************
    '* メソッド名      住民コード取得
    '* 
    '* 構文            Public Sub GetJuminCD()
    '* 
    '* 機能　　        空番を取得する。
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub GetJuminCD()
        Const THIS_METHOD_NAME As String = "GetJuminCD"             'このメソッド名

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '番号取得クラスコンストラクタセット
            Dim cuGetNum As New USSnumgetClass("AB", "0001", "0000")

            '*履歴番号 000003 2007/04/02 修正開始
            ' コード存在チェック
            AtenaDBChecKCD(cuGetNum, "0")

            ''住民コードを１件取得
            'cuGetNum.GetNum(m_cfUFControlData)

            ''取得番号をプロパティにセット
            'm_strBango = cuGetNum.p_strBango(0)
            '*履歴番号 000003 2007/04/02 修正終了

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名      住民コード取得（宛名用）
    '* 
    '* 構文            Public Sub GetAtenaJuminCD()
    '* 
    '* 機能　　        空番を取得する。
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub GetAtenaJuminCD()
        Const THIS_METHOD_NAME As String = "GetAtenaJuminCD"            'このメソッド名

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '番号取得クラスコンストラクタセット
            Dim cuGetNum As New USSnumgetClass("AB", "0002", "0000")

            '*履歴番号 000003 2007/04/02 修正開始
            AtenaDBChecKCD(cuGetNum, "0")

            ''住民コード（宛名用）を１件取得
            'cuGetNum.GetNum(m_cfUFControlData)

            ''取得番号をプロパティにセット
            'm_strBango = cuGetNum.p_strBango(0)
            '*履歴番号 000003 2007/04/02 修正終了

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名      世帯コード取得
    '* 
    '* 構文            Public Sub GetSetaiCD()
    '* 
    '* 機能　　        空番を取得する。
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub GetSetaiCD()
        Const THIS_METHOD_NAME As String = "GetSetaiCD"             'このメソッド名

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '番号取得クラスコンストラクタセット
            Dim cuGetNum As New USSnumgetClass("AB", "0003", "0000")

            '*履歴番号 000003 2007/04/02 修正開始
            AtenaDBChecKCD(cuGetNum, "1")

            ''世帯コードを１件取得
            'cuGetNum.GetNum(m_cfUFControlData)

            ''取得番号をプロパティにセット
            'm_strBango = cuGetNum.p_strBango(0)
            '*履歴番号 000003 2007/04/02 修正終了

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名      世帯コード取得（宛名用）
    '* 
    '* 構文            Public Sub GetAtenaSetaiCD()
    '* 
    '* 機能　　        空番を取得する。
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub GetAtenaSetaiCD()
        Const THIS_METHOD_NAME As String = "GetAtenaSetaiCD"        'このメソッド名

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '番号取得クラスコンストラクタセット
            Dim cuGetNum As New USSnumgetClass("AB", "0004", "0000")

            '*履歴番号 000003 2007/04/02 修正開始
            AtenaDBChecKCD(cuGetNum, "1")

            ''世帯コード（宛名用）を１件取得
            'cuGetNum.GetNum(m_cfUFControlData)

            ''取得番号をプロパティにセット
            'm_strBango = cuGetNum.p_strBango(0)
            '*履歴番号 000003 2007/04/02 修正終了

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名      共有者コード取得
    '* 
    '* 構文            Public Sub GetKyoyuCD()
    '* 
    '* 機能　　        空番を取得する。
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub GetKyoyuCD()
        Const THIS_METHOD_NAME As String = "GetKyoyuCD"             'このメソッド名

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '番号取得クラスコンストラクタセット
            Dim cuGetNum As New USSnumgetClass("AB", "0005", "0000")

            '*履歴番号 000003 2007/04/02 修正開始
            AtenaDBChecKCD(cuGetNum, "0")

            ''共有者コードを１件取得
            'cuGetNum.GetNum(m_cfUFControlData)

            ''取得番号をプロパティにセット
            'm_strBango = cuGetNum.p_strBango(0)
            '*履歴番号 000003 2007/04/02 修正終了

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp

        End Try
    End Sub

    '*履歴番号 000001 2004/04/19 追加開始
    '************************************************************************************************
    '* メソッド名      住民コード取得（宛名法人用）
    '* 
    '* 構文            Public Sub GetAtenaHojinCD()
    '* 
    '* 機能　　        空番を取得する。
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub GetAtenaHojinCD()
        Const THIS_METHOD_NAME As String = "GetAtenaHojinCD"            'このメソッド名

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '番号取得クラスコンストラクタセット
            Dim cuGetNum As New USSnumgetClass("AB", "0006", "0000")

            '*履歴番号 000003 2007/04/02 修正開始
            AtenaDBChecKCD(cuGetNum, "0")

            ''住民コード（宛名用）を１件取得
            'cuGetNum.GetNum(m_cfUFControlData)

            ''取得番号をプロパティにセット
            'm_strBango = cuGetNum.p_strBango(0)
            '*履歴番号 000003 2007/04/02 修正終了

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名      住民コード取得（宛名共有用）
    '* 
    '* 構文            Public Sub GetAtenaKyoyuCD()
    '* 
    '* 機能　　        空番を取得する。
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub GetAtenaKyoyuCD()
        Const THIS_METHOD_NAME As String = "GetAtenaKyoyuCD"            'このメソッド名

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '番号取得クラスコンストラクタセット
            Dim cuGetNum As New USSnumgetClass("AB", "0007", "0000")

            '*履歴番号 000003 2007/04/02 修正開始
            AtenaDBChecKCD(cuGetNum, "0")

            ''住民コード（宛名用）を１件取得
            'cuGetNum.GetNum(m_cfUFControlData)

            ''取得番号をプロパティにセット
            'm_strBango = cuGetNum.p_strBango(0)
            '*履歴番号 000003 2007/04/02 修正終了

            'デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw objExp

        End Try
    End Sub
    '*履歴番号 000001 2004/04/19 追加終了

    '*履歴番号 000003 2007/04/02 追加開始
    '************************************************************************************************
    '* メソッド名      コード取得時の存在チェック
    '* 
    '* 構文            Public Sub AtenaDBChecKCD(ByVal cuGetNum As USSnumgetClass, ByVal strChkCD As String)
    '* 
    '* 機能　　        取得したコードが宛名ＤＢ上に存在しないかチェックを行う。
    '* 
    '* 引数            cuGetNum As USSnumgetClass   :番号取得クラス 
    '*                 strChkCD As String           :コード取得判定フラグ
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub AtenaDBChecKCD(ByVal cuGetNum As USSnumgetClass, ByVal strChkCD As String)
        Const THIS_METHOD_NAME As String = "AtenaDBChecKCD"     ' メソッド名
        Dim cfRdb As UFRdbClass                                 ' RDBクラス
        Dim blnChkCD As Boolean = True                          ' コード存在チェックフラグ
        Dim csSB As StringBuilder
        Dim cfParamCollection As UFParameterCollectionClass     ' パラメータコレクションクラス
        Dim cfDataReder As UFDataReaderClass                    ' データリーダークラス

        Try
            'デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' インスタンス化
            csSB = New StringBuilder
            cfParamCollection = New UFParameterCollectionClass

            ' SQL作成
            '* SELECT JUMINCD FROM ABATENA WHERE JUMINCD = @JUMINCD
            '* SELECT JUMINCD FROM ABATENA WHERE STAICD = @STAICD
            csSB.Append("SELECT ").Append(ABAtenaEntity.JUMINCD)
            csSB.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME)
            If (strChkCD = "0") Then
                ' 住民コードの存在値チェック
                csSB.Append(" WHERE ").Append(ABAtenaEntity.JUMINCD)
                csSB.Append(" = ").Append(ABAtenaEntity.PARAM_JUMINCD)
            Else
                ' 世帯コードの存在値チェック
                csSB.Append(" WHERE ").Append(ABAtenaEntity.STAICD)
                csSB.Append(" = ").Append(ABAtenaEntity.PARAM_STAICD)
            End If

            ' RDBクラスのインスタンス作成
            cfRdb = New UFRdbClass(m_cfUFControlData.m_strBusinessId)
            ' RDB接続
            cfRdb.Connect()

            Try
                ' 空きコードが見つかるまで繰り返す
                While blnChkCD
                    ' 空番取得
                    cuGetNum.GetNum(m_cfUFControlData)

                    cfParamCollection.Clear()
                    ' 住民コードか世帯コードか判断
                    If (strChkCD = "0") Then
                        ' 住民コードの場合
                        cfParamCollection.Add(ABAtenaEntity.PARAM_JUMINCD, cuGetNum.p_strBango(0))
                    Else
                        ' 世帯コードの場合
                        cfParamCollection.Add(ABAtenaEntity.PARAM_STAICD, cuGetNum.p_strBango(0))
                    End If

                    cfDataReder = cfRdb.GetDataReader(csSB.ToString, cfParamCollection)
                    If (cfDataReder.Read = False) Then
                        ' コードが存在しない場合
                        ' チェックフラグをFalseにする
                        blnChkCD = False
                    End If
                    cfDataReder.Close()

                End While
            Catch
                ' エラーをそのままスロー
                Throw
            Finally
                ' RDBアクセスログ出力
                m_cfUFLogClass.RdbWrite(m_cfUFControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:Disconnect】")
                ' RDB切断
                cfRdb.Disconnect()
            End Try

            ' 取得番号をプロパティにセット
            m_strBango = cuGetNum.p_strBango(0)

            ' デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            'エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】")
            'エラーをそのままスローする
            Throw

        End Try
    End Sub
    '*履歴番号 000003 2007/04/02 追加終了

    '*履歴番号 000002 2007/02/05 追加開始
    '************************************************************************************************
    '* メソッド名      宛名更新エラーログ番号取得
    '* 
    '* 構文            Public Sub GetErrLogNo()
    '* 
    '* 機能　　        空番を取得する。
    '* 
    '* 引数            なし
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub GetErrLogNo()

        Const THIS_METHOD_NAME As String = "GetErrLogNo"          ' メソッド名

        Try
            ' デバッグ開始ログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 番号取得クラスコンストラクタセット
            Dim cuGetNum As New USSnumgetClass("AB", "2001", "0000")

            ' 宛名更新エラーログ番号を１件取得
            cuGetNum.GetNum(m_cfUFControlData)

            ' 取得番号をプロパティにセット
            m_strBango = cuGetNum.p_strBango(0)

            ' デバッグ終了ログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            ' エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "【クラス名:" + THIS_CLASS_NAME + "】【メソッド名:THIS_METHOD_NAME】【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp

        End Try

    End Sub
    '*履歴番号 000002 2007/02/05 追加終了

End Class
