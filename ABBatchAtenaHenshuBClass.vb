'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢバッチ用宛名編集クラス(ABBatchAtenaHenshuBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/08/22　滝沢　欽也
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

Public Class ABBatchAtenaHenshuBClass
    Inherits ABAtenaHenshuBClass        ' 宛名編集Ｂクラスを継承

    'パラメータのメンバ変数

    '　コンスタント定義
    Protected Shadows Const THIS_CLASS_NAME As String = "ABBatchAtenaHenshuBClass"      ' クラス名

    '* 履歴番号 000001 2004/08/27 追加開始（宮沢）
    Private m_cURKanriJohoB As URKANRIJOHOBClass              '管理情報取得クラス
    '* 履歴番号 000001 2004/08/27 追加終了

    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass,
    '*                               ByVal cfUFRdbClass as UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
    '*                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '*                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfUFControlData As UFControlData,
                   ByVal cfUFConfigDataClass As UFConfigDataClass,
                   ByVal cfUFRdbClass As UFRdbClass)
        MyBase.New(cfUFControlData, cfUFConfigDataClass, cfUFRdbClass)
    End Sub
    '* 履歴番号 000002 2005/01/25 追加開始（宮沢）
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass,
    '*                               ByVal cfUFRdbClass as UFRdbClass)
    '* 　　                          ByVal blnSelectAll as boolean)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
    '*                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '*                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
    '* 　　           ByVal blnSelectAll As Boolean           : Trueの場合全項目、Falseの場合簡易項目のみ取得
    '* 
    '* 戻り値          なし
    '************************************************************************************************
    Public Sub New(ByVal cfUFControlData As UFControlData,
                   ByVal cfUFConfigDataClass As UFConfigDataClass,
                   ByVal cfUFRdbClass As UFRdbClass,
                   ByVal blnSelectAll As ABEnumDefine.AtenaGetKB)
        MyBase.New(cfUFControlData, cfUFConfigDataClass, cfUFRdbClass, blnSelectAll)
    End Sub
    '* 履歴番号 000002 2005/01/25 追加終了（宮沢）

    '************************************************************************************************
    '* メソッド名     送付先住所行政区編集区分取得
    '* 
    '* 構文           Private Function GetSofuJushoGyoseikuType() As SofuJushoGyoseikuType
    '* 
    '* 機能　　    　　送付先住所行政区編集区分を取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         SofuJushoGyoseikuType
    '************************************************************************************************
    <SecuritySafeCritical>
    Protected Overrides Function GetSofuJushoGyoseikuType() As SofuJushoGyoseikuType
        Const THIS_METHOD_NAME As String = "GetSofuJushoGyoseikuType"
        '* 履歴番号 000001 2004/08/27 削除開始（宮沢）
        'Dim cURKanriJohoB As URKANRIJOHOBClass              '管理情報取得クラス
        '* 履歴番号 000001 2004/08/27 削除終了
        Dim cSofuJushoGyoseikuType As SofuJushoGyoseikuType

        Try
            ' デバッグログ出力
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '管理情報取得Ｂのインスタンス作成
            '* 履歴番号 000001 2004/08/27 更新開始（宮沢）
            'cURKanriJohoB = New URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            If (m_cURKanriJohoB Is Nothing) Then
                m_cURKanriJohoB = New URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            End If
            '* 履歴番号 000001 2004/08/27 更新終了

            '* 履歴番号 000002 2005/01/25 更新開始（宮沢）
            'cSofuJushoGyoseikuType = m_cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param
            If (m_bSofuJushoGyoseikuTypeFlg = False) Then
                m_cSofuJushoGyoseikuType = m_cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param
                m_bSofuJushoGyoseikuTypeFlg = True
            End If
            cSofuJushoGyoseikuType = m_cSofuJushoGyoseikuType
            '* 履歴番号 000002 2005/01/25 更新終了（宮沢）

            ' デバッグログ出力
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp
        Catch objExp As Exception
            ' エラーログ出力
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp
        End Try

        Return cSofuJushoGyoseikuType

    End Function

End Class
