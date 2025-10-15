'************************************************************************************************
'* 業務名           宛名管理システム
'* 
'* クラス名         連絡先マスタＤＡ(ABRenrakusaki2BClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け           2007/07/25
'*
'* 作成者　　　     比嘉　計成
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2007/08/27  000001      チェック方法の誤りを修正
'* 2010/04/16  000002      VS2008対応（比嘉）
'* 2024/01/11  000003     【AB-0860-1】連絡先管理項目追加
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Text

Public Class ABRenrakusaki2BClass

#Region "メンバ変数"
    'メンバ変数の定義
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_strInsertSQL As String                                                'INSERT用SQL
    Private m_strUpdateSQL As String                                                'UPDATE用SQL
    Private m_strDeleteSQL As String                                                'DELETE用SQL（物理）
    Private m_strDelRonriSQL As String                                              'DELETE用SQL（論理）
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE用パラメータコレクション
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass      'DELETE用パラメータコレクション（物理）
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    'DELETE用パラメータコレクション（論理）
    Private m_csDataSchma As DataSet   'スキーマ保管用データセット

    'コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABRenrakusaki2BClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' 業務コード
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfControlData As UFControlData, 
    '* 　　                          ByVal cfConfigDataClass As UFConfigDataClass, 
    '* 　　                          ByVal cfRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfControlData As UFControlData          : コントロールデータオブジェクト
    '* 　　           cfConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '* 　　           cfRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
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

        'メンバ変数の初期化
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing

        ' SQL文の作成
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     連絡先マスタ抽出
    '* 
    '* 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* 機能　　    　　連絡先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String  :住民コード
    '* 
    '* 戻り値         取得した連絡先マスタの該当データ（DataSet）
    '*                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       'このメソッド名
        Dim csRenrakusakiEntity As DataSet                              '連絡先マスタデータ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            ' ORDER文結合
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)
            strSQL.Append(" ASC")

            strSQL.Append(" , ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)
            strSQL.Append(" ASC")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + strSQL.ToString + "】")

            ' SQLの実行 DataSetの取得
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function

    '************************************************************************************************
    '* メソッド名     連絡先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　連絡先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String    :住民コード
    '*                blnSakujoFG As Boolean  :削除フラグ
    '* 
    '* 戻り値         取得した連絡先マスタの該当データ（DataSet）
    '*                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       'このメソッド名
        Dim csRenrakusakiEntity As DataSet                              '連絡先マスタデータ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABRenrakusakiEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            ' ORDER文結合
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)
            strSQL.Append(" ASC")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function

    '************************************************************************************************
    '* メソッド名     連絡先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String) As DataSet
    '* 
    '* 機能　　    　　連絡先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '* 
    '* 戻り値         取得した連絡先マスタの該当データ（DataSet）
    '*                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String, ByVal strGyomunaiSHUCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       'このメソッド名
        Dim csRenrakusakiEntity As DataSet                              '連絡先マスタデータ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim blnSakujo As Boolean = True                                 '削除データ読み込み

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)          '住民コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)          '業務コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)   '業務内種別コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            
            ' SQLの実行 DataSetの取得
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            
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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function

    '*履歴番号 000003 2024/01/11 追加開始
    '************************************************************************************************
    '* メソッド名     連絡先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD, 
    '*                                                        ByVal intTorokuRenban As String) As DataSet
    '* 
    '* 機能　　    　　連絡先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '*                strTorokuRenban As String     :登録連番
    '* 
    '* 戻り値         取得した連絡先マスタの該当データ（DataSet）
    '*                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String, ByVal strGyomunaiSHUCD As String, ByVal strTorokuRenban As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       'このメソッド名
        Dim csRenrakusakiEntity As DataSet                              '連絡先マスタデータ
        Dim strSQL As New StringBuilder()                               'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim blnSakujo As Boolean = True                                 '削除データ読み込み

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)          '住民コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)          '業務コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)   '業務内種別コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.TOROKURENBAN)     '登録連番
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_TOROKURENBAN)

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務内種別コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 登録連番
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_TOROKURENBAN
            cfUFParameterClass.Value = strTorokuRenban
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "【クラス名:" + Me.GetType.Name + "】" +
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" +
                                        "【実行メソッド名:GetDataSet】" +
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function
    '*履歴番号 000003 2024/01/11 追加終了

    '************************************************************************************************
    '* メソッド名     連絡先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　連絡先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '*                blnSakujoFG As Boolean        :削除フラグ
    '* 
    '* 戻り値         取得した連絡先マスタの該当データ（DataSet）
    '*                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String, ByVal strGyomunaiSHUCD As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       'このメソッド名
        Dim csRenrakusakiEntity As DataSet                              '連絡先マスタデータ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim blnSakujo As Boolean = True                                 '削除データ読み込み

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)          '住民コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)          '業務コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)   '業務内種別コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABRenrakusakiEntity.SAKUJOFG)     '削除フラグ
                strSQL.Append(" <> 1")
            End If

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            
            ' SQLの実行 DataSetの取得
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            
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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function

    '*履歴番号 000003 2024/01/11 追加開始
    '************************************************************************************************
    '* メソッド名     連絡先マスタ抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal strTorokuRenban As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* 機能　　    　　連絡先マスタより該当データを取得する。
    '* 
    '* 引数           strJuminCD As String          :住民コード
    '*                strGyomuCD As String          :業務コード
    '*                strGyomunaiSHUCD As String    :業務内種別コード
    '*                strTorokuRenban As String     :登録連番
    '*                blnSakujoFG As Boolean        :削除フラグ
    '* 
    '* 戻り値         取得した連絡先マスタの該当データ（DataSet）
    '*                   構造：csRenrakusakiEntity    インテリセンス：ABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String, ByVal strGyomunaiSHUCD As String, ByVal strTorokuRenban As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       'このメソッド名
        Dim csRenrakusakiEntity As DataSet                              '連絡先マスタデータ
        Dim strSQL As New StringBuilder()                               'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス
        Dim blnSakujo As Boolean = True                                 '削除データ読み込み

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)          '住民コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)          '業務コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)   '業務内種別コード
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.TOROKURENBAN)     '登録連番
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_TOROKURENBAN)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABRenrakusakiEntity.SAKUJOFG)     '削除フラグ
                strSQL.Append(" <> 1")
            End If

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' 検索条件のパラメータを作成
            ' 住民コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務内種別コード
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 登録連番
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_TOROKURENBAN
            cfUFParameterClass.Value = strTorokuRenban
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")

            ' SQLの実行 DataSetの取得
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function
    '*履歴番号 000003 2024/01/11 追加終了

    '************************************************************************************************
    '* メソッド名     連絡先マスタ追加
    '* 
    '* 構文           Public Function InsertRenrakusakiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  連絡先マスタにデータを追加する。
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertRenrakusakiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertRenrakusakiB"         'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer                                        '追加件数
        Dim strUpdateDateTime As String

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' 更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '作成日時

            ' 共通項目の編集を行う
            csDataRow(ABRenrakusakiEntity.TANMATSUID) = m_cfControlData.m_strClientId               '端末ＩＤ
            csDataRow(ABRenrakusakiEntity.SAKUJOFG) = "0"                                           '削除フラグ
            csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) = Decimal.Zero                             '更新カウンタ
            csDataRow(ABRenrakusakiEntity.SAKUSEINICHIJI) = strUpdateDateTime                       '作成日時
            csDataRow(ABRenrakusakiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                '作成ユーザー
            csDataRow(ABRenrakusakiEntity.KOSHINNICHIJI) = strUpdateDateTime                        '更新日時
            csDataRow(ABRenrakusakiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                 '更新ユーザー

            ' 当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                ' データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
            
            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* メソッド名     連絡先マスタ更新
    '* 
    '* 構文           Public Function UpdateRenrakusakiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  連絡先マスタのデータを更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateRenrakusakiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateRenrakusakiB"         'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim intUpdCnt As Integer                                        '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' 共通項目の編集を行う
            csDataRow(ABRenrakusakiEntity.TANMATSUID) = m_cfControlData.m_strClientId '端末ＩＤ
            csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER)) + 1   '更新カウンタ
            csDataRow(ABRenrakusakiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '更新日時
            csDataRow(ABRenrakusakiEntity.KOSHINUSER) = m_cfControlData.m_strUserId   '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABRenrakusakiEntity.PREFIX_KEY.RLength) = ABRenrakusakiEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")
            
            ' SQLの実行
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* メソッド名     連絡先マスタ削除（論理）
    '* 
    '* 構文           Public Function DeleteRenrakusakiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  連絡先マスタのデータを削除（論理）する。
    '* 
    '* 引数           csDataRow As DataRow  :削除データ
    '* 
    '* 戻り値         削除（論理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteRenrakusakiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteRenrakusakiB（論理）"  'このメソッド名
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim intDelCnt As Integer                                        '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or _
                m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' 共通項目の編集を行う
            csDataRow(ABRenrakusakiEntity.TANMATSUID) = m_cfControlData.m_strClientId '端末ＩＤ
            csDataRow(ABRenrakusakiEntity.SAKUJOFG) = 1                                 '削除フラグ
            csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER)) + 1   '更新カウンタ
            csDataRow(ABRenrakusakiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '更新日時
            csDataRow(ABRenrakusakiEntity.KOSHINUSER) = m_cfControlData.m_strUserId   '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABRenrakusakiEntity.PREFIX_KEY.RLength) = ABRenrakusakiEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "】")
            
            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

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
            Throw exAppException

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* メソッド名     連絡先マスタ削除（物理）
    '* 
    '* 構文           Public Overloads Function DeleteRenrakusakiB(ByVal csDataRow As DataRow, 
    '*                                                      ByVal strSakujoKB As String) As Integer
    '* 
    '* 機能　　    　  連絡先マスタのデータを削除（物理）する。
    '* 
    '* 引数           csDataRow As DataRow      :削除データ
    '*                strSakujoKB As String     :削除フラグ
    '* 
    '* 戻り値         削除（物理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteRenrakusakiB(ByVal csDataRow As DataRow, ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteRenrakusakiB（物理）"  'このメソッド名
        Dim objErrorStruct As UFErrorStruct                             'エラー定義構造体
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim intDelCnt As Integer                                        '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 引数の削除区分をチェック
            If (strSakujoKB <> "D") Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' エラー定義を取得
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_DELETE_SAKUJOKB)
                ' 例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' SQLが作成されていなければ作成
            If (m_strDeleteSQL Is Nothing Or m_strDeleteSQL = String.Empty Or _
                m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABRenrakusakiEntity.PREFIX_KEY.RLength) = ABRenrakusakiEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】")

            ' SQLの実行
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

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
            Throw exAppException

        Catch exException As Exception ' システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")

            ' システムエラーをスローする
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* メソッド名     SQL文の作成
    '* 
    '* 構文           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* 機能　　    　 INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
    '* 
    '* 引数           csDataRow As DataRow : 更新対象の行
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL"              'このメソッド名
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  'パラメータクラス
        Dim strInsertColumn As String                               '追加SQL文項目文字列
        Dim strInsertParam As String                                '追加SQL文パラメータ文字列
        Dim strDelRonriSQL As New StringBuilder                     '論理削除SQL文文字列
        Dim strDeleteSQL As New StringBuilder                       '物理削除SQL文文字列
        Dim strWhere As New StringBuilder                           '更新削除SQL文Where文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABRenrakusakiEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' 更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABRenrakusakiEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABRenrakusakiEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABRenrakusakiEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            strWhere.Append(ABRenrakusakiEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABRenrakusakiEntity.KEY_KOSHINCOUNTER)
            '*履歴番号 000003 2024/01/11 追加開始
            strWhere.Append(" AND ")
            strWhere.Append(ABRenrakusakiEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABRenrakusakiEntity.KEY_TOROKURENBAN)
            '*履歴番号 000003 2024/01/11 追加終了

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABRenrakusakiEntity.TABLE_NAME + " SET "

            ' DELETE（論理） SQL文の作成
            strDelRonriSQL.Append("UPDATE ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            strDelRonriSQL.Append(" SET ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.TANMATSUID)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_TANMATSUID)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.SAKUJOFG)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_SAKUJOFG)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.KOSHINCOUNTER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_KOSHINCOUNTER)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.KOSHINNICHIJI)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_KOSHINNICHIJI)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.KOSHINUSER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABRenrakusakiEntity.PARAM_KOSHINUSER)
            strDelRonriSQL.Append(strWhere.ToString)
            m_strDelRonriSQL = strDelRonriSQL.ToString

            ' DELETE（物理） SQL文の作成
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE（論理） パラメータコレクションのインスタンス化
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE（物理） パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL文の作成
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            ' INSERT SQL文のトリミング
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))
            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            ' UPDATE SQL文のトリミング
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL文にWHERE句の追加
            m_strUpdateSQL += strWhere.ToString

            ' UPDATE,DELETE(物理) コレクションにキー情報を追加
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000003 2024/01/11 追加開始
            ' 登録連番
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_TOROKURENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000003 2024/01/11 追加終了

            ' DELETE（論理） コレクションにパラメータを追加
            ' 端末ＩＤ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 削除フラグ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新日時
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新ユーザ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 住民コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務内種別コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000003 2024/01/11 追加開始
            ' 登録連番
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_TOROKURENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*履歴番号 000003 2024/01/11 追加終了

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
            Throw exAppException

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* 機能　　       連絡先マスタのデータ整合性チェックを行います。
    '* 
    '* 引数           strColumnName As String
    '*                strValue As String
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"       'このメソッド名
        Dim objErrorStruct As UFErrorStruct                         'エラー定義構造体

        Try
            'デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strColumnName.ToUpper()
                Case ABRenrakusakiEntity.JUMINCD                        '住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.SHICHOSONCD                    '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.KYUSHICHOSONCD                 '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.GYOMUCD                        '業務コード
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_GYOMUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.GYOMUNAISHU_CD                 '業務内種別コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_GYOMUNAISHU_CD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKIKB                  '連絡先区分
                    '* 履歴番号 000001 2007/08/27 修正開始
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        'If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000001 2007/08/27 修正終了
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKIMEI                 '連絡先名
                    '* 履歴番号 000001 2007/08/27 修正開始
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'If (Not UFStringClass.CheckNumber(strValue, m_cfConfigDataClass)) Then
                        '* 履歴番号 000001 2007/08/27 修正終了
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKIMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKI1                   '連絡先1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKI1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKI2                   '連絡先2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKI2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKI3                   '連絡先3
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKI3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RESERVE                        'リザーブ
                    '何もしない
                Case ABRenrakusakiEntity.TANMATSUID                     '端末ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.SAKUJOFG                       '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.KOSHINCOUNTER                  '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.SAKUSEINICHIJI                 '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.SAKUSEIUSER                    '作成ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.KOSHINNICHIJI                  '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.KOSHINUSER                     '更新ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KOSHINUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
            End Select

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
            Throw exAppException
        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw exException
        End Try
    End Sub
#End Region

End Class
