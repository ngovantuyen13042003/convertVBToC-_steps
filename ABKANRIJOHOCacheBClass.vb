'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        宛名管理情報キャッシュＤＡ(ABKANRIJOHOCacheBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2014/04/28　岩下 一美
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2014/04/28  000000      新規作成
'* 2014/06/11  000001      バッチ処理よりコールされた際のエラー修正（田中）
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
Imports System.Web

Public Class ABKANRIJOHOCacheBClass
    Inherits ABAtenaKanriJohoBClass

#Region "メンバ変数"
    '**
    '* クラスID定義
    '* 
    Private Const THIS_CLASS_NAME As String = "ABKANRIJOHOCacheBClass"

    ' メンバ変数の定義
    Private m_cfLog As URLogXClass                                     ' ログ出力クラス

    ' キャッシュクラス
    Private Const ABKANRIJOHO As String = "ABKANRIJOHO"
    Private Class CacheDataClass
        Public m_strUpdate As String
        Public m_csDS As DataSet
    End Class

    ' 宛名管理情報　種別キー・識別キー
    Private Const SHUBETSUKEY_KOJINJOHOSEIGYO As String = "20"         ' 種別キー:20：個人情報制御機能
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '*                               ByVal cfConfigData As UFConfigDataClass, 
    '*                               ByVal cfRdb As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData    : コントロールデータオブジェクト
    '*                cfConfigData As UFConfigDataClass : コンフィグデータオブジェクト
    '*                cfRdb As UFRdbClass               : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigData As UFConfigDataClass, _
                   ByVal cfRdb As UFRdbClass)

        MyBase.New(cfControlData, cfConfigData, cfRdb)

        ' ログ出力クラスインスタンス化
        m_cfLog = New URLogXClass(cfControlData, cfConfigData, Me.GetType.Name)

    End Sub
#End Region

#Region "メソッド"
#Region "管理情報マスタ抽出"
    '************************************************************************************************
    '* メソッド名     管理情報マスタ抽出
    '* 
    '* 構文           Private Function GetKanriJohoHoshu() As DataSet
    '* 
    '* 機能           指定された管理情報マスタを条件により該当データを取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         DataSet : 取得した管理情報マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu() As DataSet
        Return MyClass.GetKanriJohoHoshu(String.Empty, String.Empty)
    End Function

    '************************************************************************************************
    '* メソッド名     管理情報マスタ抽出
    '* 
    '* 構文           Private Function GetKanriJohoHoshu(ByVal strShuKEY As String) As DataSet
    '* 
    '* 機能           指定された管理情報マスタを条件により該当データを取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         DataSet : 取得した管理情報マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu(ByVal strShuKEY As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetKanriJohoHoshu"     'メソッド名
        Dim csRet As DataSet
        Dim strMethodName As String = Reflection.MethodBase.GetCurrentMethod.Name

        Try
            m_cfLog.DebugStartWrite(strMethodName)

            ' キャッシュからデータを取得
            csRet = GetKanriJohoHoshu(strShuKEY, String.Empty)

            m_cfLog.DebugEndWrite(strMethodName)

            Return csRet

        Catch objAppExp As UFAppException
            'ワーニングログ出力
            m_cfLog.WarningWrite("【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】", _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】", _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            Throw objAppExp
        Catch objExp As Exception
            'エラーログ出力
            m_cfLog.ErrorWrite("【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】", _
                                        "【エラー内容:" + objExp.Message + "】")
            Throw objExp
        End Try
    End Function

    '************************************************************************************************
    '* メソッド名     管理情報マスタ抽出
    '* 
    '* 構文           Private Function GetKanriJohoHoshu(ByVal strShuKEY As String, _
    '*                                                      ByVal strShikibetsuKEY As String) As DataSet
    '* 
    '* 機能           指定された管理情報マスタを条件により該当データを取得する
    '* 
    '* 引数           strShuKEY As String        : 種別キー（管理情報マスタ取得時のキー）
    '*                strShikibetsuKEY As String : 識別キー（管理情報マスタ取得時のキー）
    '* 
    '* 戻り値         DataSet : 取得した管理情報マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu(ByVal strShuKEY As String, _
                                                     ByVal strShikibetsuKEY As String) As DataSet
        Dim csDS As DataSet
        Dim csRetDS As DataSet
        Dim csDRs As DataRow()
        Dim intI As Integer
        Dim csRetDT As DataTable
        Dim csSB As StringBuilder = New StringBuilder()

        'キャッシュから管理情報の取得
        csDS = GetDataFromCache()

        'Filter条件の作成
        If (strShuKEY <> String.Empty) Then
            csSB.Append(ABAtenaKanriJohoEntity.SHUKEY).Append(" = '").Append(strShuKEY).Append("'")
            If (strShikibetsuKEY <> String.Empty) Then
                csSB.Append(" AND ")
            End If
        End If
        If (strShikibetsuKEY <> String.Empty) Then
            csSB.Append(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).Append(" = '").Append(strShikibetsuKEY).Append("'")
        End If
        If (csSB.RLength > 0) Then
            csDRs = csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Select(csSB.ToString)
        End If

        csRetDS = csDS.Clone
        csRetDT = csRetDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME)
        For intI = 0 To csDRs.Length - 1
            csRetDT.ImportRow(csDRs(intI))
        Next
        Return csRetDS
    End Function

    '************************************************************************************************
    '* メソッド名     管理情報マスタ取得
    '* 
    '* 構文           Private Function GetDataFromCache() As DataSet
    '* 
    '* 機能           管理情報マスタをキャッシュから取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         DataSet
    '************************************************************************************************
    Private Function GetDataFromCache() As DataSet
        Const THIS_METHOD_NAME As String = "GetDataFromCache"     'メソッド名
        Dim cCacheData As CacheDataClass
        Dim csRet As DataSet

        Try
            'デバッグ開始ログ出力
            m_cfLog.DebugStartWrite(THIS_METHOD_NAME)

            SyncLock GetType(HttpContext)
                Try
                    cCacheData = DirectCast(HttpContext.Current.Cache(ABKANRIJOHO), CacheDataClass)
                Catch
                End Try
                If cCacheData Is Nothing Then
                    '*履歴番号 000001 2014/06/11 修正開始
                    'm_cfLog.DebugWrite("キャッシュ作成(ABKANRIJOHO)")
                    'cCacheData = New CacheDataClass()
                    'cCacheData.m_csDS = MyBase.GetKanriJohoHoshu(SHUBETSUKEY_KOJINJOHOSEIGYO)
                    'cCacheData.m_strUpdate = String.Empty
                    'HttpContext.Current.Cache(ABKANRIJOHO) = cCacheData

                    csRet = MyBase.GetKanriJohoHoshu(SHUBETSUKEY_KOJINJOHOSEIGYO)

                    If Not (HttpContext.Current Is Nothing) Then
                        'HttpContext.CurrentがNothingでない場合
                        m_cfLog.DebugWrite("キャッシュ作成(ABKANRIJOHO)")
                        cCacheData = New CacheDataClass()
                        cCacheData.m_csDS = csRet
                        cCacheData.m_strUpdate = String.Empty
                        HttpContext.Current.Cache(ABKANRIJOHO) = cCacheData
                    Else
                        'それ以外の場合、処理なし
                    End If
                    '*履歴番号 000001 2014/06/11 修正終了
                Else
                    m_cfLog.DebugWrite("キャッシュ中にデータ有")
                    '*履歴番号 000001 2014/06/11 追加開始
                    csRet = cCacheData.m_csDS
                    '*履歴番号 000001 2014/06/11 追加終了
                End If
                '*履歴番号 000001 2014/06/11 削除開始
                'csRet = cCacheData.m_csDS
                '*履歴番号 000001 2014/06/11 削除終了

            End SyncLock

            m_cfLog.DebugEndWrite(THIS_METHOD_NAME)

            Return csRet

        Catch objAppExp As UFAppException
            'ワーニングログ出力
            m_cfLog.WarningWrite("【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】", _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】", _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            Throw objAppExp
        Catch objExp As Exception
            'エラーログ出力
            m_cfLog.ErrorWrite("【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】", _
                                        "【エラー内容:" + objExp.Message + "】")
            Throw objExp
        End Try
    End Function
#End Region
#End Region

End Class
