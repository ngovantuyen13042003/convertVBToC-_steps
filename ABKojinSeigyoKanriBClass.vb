'************************************************************************************************
'* 業務名　　　　   宛名管理システム
'* 
'* クラス名　　　   ABKojinSeigyoKanriBClass：宛名個人情報管理Bクラス
'* 
'* バージョン情報   Ver 1.0
'* 
'* 作成日付　　     2012/07/19
'*
'* 作成者　　　　   2906 中嶋　秀文
'* 
'* 著作権　　　　   （株）電算
'* 
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

#Region "参照名前空間"

Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports Densan.Reams.AB.AB001BX
Imports System.Collections.Generic
Imports System.Text
#End Region

Public Class ABKojinSeigyoKanriBClass

#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigData As UFConfigDataClass             ' コンフィグデータ
    Private m_cfRdb As UFRdbClass                           ' ＲＤＢクラス
    Private m_cfError As UFErrorClass                       ' エラー処理クラス
    Private m_cABLogX As ABLogXClass                        ' ABログ出力Xクラス

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABKojinSeigyoKanriBClass"
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
        ' メンバ変数セット
        m_cfControlData = cfControlData
        m_cfConfigData = cfConfigData
        m_cfRdb = cfRdb

        ' ABログ出力クラスのインスタンス化
        m_cABLogX = New ABLogXClass(m_cfControlData, m_cfConfigData, THIS_CLASS_NAME)

    End Sub
#End Region

#Region "宛名個人制御管理マスタデータ取得"
    '************************************************************************************************
    '* メソッド名     宛名個人制御管理マスタデータ取得
    '* 
    '* 構文           Public Function GetABKojinSeigyoKanri(ByVal strGyomuCD As String, ByVal strGroupID As String, ByVal enKinoBunrui As ABKinoBunruiType) As DataSet
    '* 
    '* 機能　　    　 宛名個人制御管理マスタから引数条件でデータを取得する
    '* 
    '* 引数           strGyomuCD：業務コード strGruopID：グループID enKinoBunrui：機能分類
    '* 
    '* 戻り値         取得結果：DataSet
    '************************************************************************************************
    Public Function GetABKojinSeigyoKanri(ByVal strGyomuCD As String, ByVal a_strGroupID() As String, ByVal enKinoBunrui As ABKinoBunruiType) As DataSet
        Const THIS_METHOD_NAME As String = "GetABKojinSeigyo"           ' メソッド名
        Dim csSQL As New StringBuilder
        Dim cfParameterCollection As UFParameterCollectionClass         ' パラメータクラス
        Dim csReturn As DataSet
        Try
            ' デバッグログ出力
            m_cABLogX.DebugStartWrite(THIS_METHOD_NAME)

            cfParameterCollection = New UFParameterCollectionClass()

            With csSQL
                .AppendFormat("SELECT * FROM {0}", ABKojinSeigyoKanriMstEntity.TABLE_NAME)
                .Append(" WHERE")
                If (strGyomuCD.Trim.RLength > 0) Then
                    .AppendFormat(" {0} = {1} AND", ABKojinSeigyoKanriMstEntity.GYOMUCD, ABKojinSeigyoKanriMstEntity.KEY_GYOMUCD)
                    cfParameterCollection.Add(ABKojinSeigyoKanriMstEntity.KEY_GYOMUCD, strGyomuCD)
                Else
                    'そのまま
                End If
                If ((a_strGroupID IsNot Nothing) AndAlso (a_strGroupID.Length > 0)) Then
                    .AppendFormat(" {0} IN (", ABKojinSeigyoKanriMstEntity.GROUPID)
                    '引数のグループID分作成
                    For intIdx As Integer = 0 To a_strGroupID.Length - 1
                        .AppendFormat(" {0}_{1},", ABKojinSeigyoKanriMstEntity.KEY_GROUPID, intIdx.ToString)
                        cfParameterCollection.Add(String.Format("{0}_{1}", ABKojinSeigyoKanriMstEntity.KEY_GROUPID, intIdx.ToString), a_strGroupID(intIdx))
                    Next
                    '最後のカンマを取る
                    .RRemove(.RLength - 1, 1)
                    .Append(" ) AND")
                Else
                    'そのまま
                End If
                '機能分類は指定なしには出来ないので必ず付ける
                .AppendFormat(" {0} = {1} AND", ABKojinSeigyoKanriMstEntity.KINOBUNRUI, ABKojinSeigyoKanriMstEntity.KEY_KINOBUNRUI)
                cfParameterCollection.Add(ABKojinSeigyoKanriMstEntity.KEY_KINOBUNRUI, Convert.ToInt32(enKinoBunrui).ToString)

                .AppendFormat(" {0} <> '1'", ABKojinSeigyoKanriMstEntity.SAKUJOFG)

                .AppendFormat(" ORDER BY {0},{1},{2}", ABKojinSeigyoKanriMstEntity.GYOMUCD, _
                                                        ABKojinSeigyoKanriMstEntity.GROUPID, _
                                                        ABKojinSeigyoKanriMstEntity.KINOBUNRUI)
            End With


            ' RDBアクセスログ出力
            m_cABLogX.RdbWrite(System.Reflection.MethodBase.GetCurrentMethod.Name, m_cfRdb.GetDevelopmentSQLString(csSQL.ToString, cfParameterCollection))

            csReturn = Me.m_cfRdb.GetDataSet(csSQL.ToString, cfParameterCollection)

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

        Return csReturn

    End Function
#End Region
#End Region

End Class
