'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         連絡先区分コードマスタ取得(ABRenrakusakiKBGetBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付             2007/07/26
'*
'* 作成者           比嘉　計成
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
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

Public Class ABRenrakusakiKBGetBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
 
    Private m_csDataSchma As DataSet   'スキーマ保管用データセット

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABRenrakusakiKBGetBClass"

#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文            Public Sub New(ByVal cfControlData As UFControlData, 
    '*                              　ByVal cfConfigData As UFConfigDataClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数            cfControlData As UFControlData         : コントロールデータオブジェクト
    '*                 cfConfigData As UFConfigDataClass      : コンフィグデータオブジェクト 
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                      ByVal cfConfigDataClass As UFConfigDataClass, _
                      ByVal cfRdbClass As UFRdbClass)
        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ログ出力クラスのインスタンス化
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' SQL文の作成
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABRenrakusakiCDMstEntity.TABLE_NAME, ABRenrakusakiCDMstEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     連絡先区分コードマスタ抽出
    '* 
    '* 構文           Public Overloads Function GetRenrakusakiCD() As DataSet
    '* 
    '* 機能　　    　 連絡先区分コードマスタより該当データを取得する。
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         連絡先区分コードマスタデータ(全件)（DataSet）
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiCD() As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiCD"             ' このメソッド名
        Dim csRenrakusakiCDEntity As DataSet                                ' 異動理由マスタデータ
        Dim strSQL As New System.Text.StringBuilder                         ' SQL文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiCDMstEntity.TABLE_NAME)
            ' ORDER文結合
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csRenrakusakiCDEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiCDMstEntity.TABLE_NAME, False)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw 
        End Try

        Return csRenrakusakiCDEntity

    End Function

    '************************************************************************************************
    '* メソッド名     連絡先区分コードマスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String) As DataSet
    '* 
    '* 機能　　    　 連絡先区分コードより該当データを取得する。
    '* 
    '* 引数           strRenrakusakiCD As String     :連絡先区分
    '* 
    '* 戻り値         取得した連絡先区分コードマスタの該当データ（DataSet）
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiCD"             ' このメソッド名
        Dim csRenrakusakiCDEntity As DataSet                                ' 連絡先区分コードマスタデータ
        Dim strSQL As New System.Text.StringBuilder                         ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                          ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiCDMstEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiCDMstEntity.KEY_RENRAKUSAKIKB)
            ' ORDER文結合
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiCDMstEntity.KEY_RENRAKUSAKIKB
            cfUFParameterClass.Value = strRenrakusakiCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)


            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csRenrakusakiCDEntity = m_csDataSchma.Clone()
            csRenrakusakiCDEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiCDEntity, ABRenrakusakiCDMstEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return csRenrakusakiCDEntity

    End Function

    '************************************************************************************************
    '* メソッド名     連絡先区分コードマスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String, 
    '*                                                             ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　 連絡先区分コードより該当データを取得する。
    '* 
    '* 引数           strRenrakusakiCD As String     :連絡先区分
    '*                blnSakujoFG As Boolean         :削除フラグ
    '* 
    '* 戻り値         取得した連絡先区分コードマスタの該当データ（DataSet）
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiCD"             ' このメソッド名
        Dim csRenrakusakiCDEntity As DataSet                                ' 連絡先区分コードマスタデータ
        Dim strSQL As New System.Text.StringBuilder                         ' SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                          ' パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiCDMstEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiCDMstEntity.KEY_RENRAKUSAKIKB)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABRenrakusakiCDMstEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            ' ORDER文結合
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABIdoRiyuEntity.KEY_RIYUCD
            cfUFParameterClass.Value = strRenrakusakiCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)


            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csRenrakusakiCDEntity = m_csDataSchma.Clone()
            csRenrakusakiCDEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiCDEntity, ABRenrakusakiCDMstEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw 
        End Try

        Return csRenrakusakiCDEntity

    End Function
#End Region

End Class
