'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        宛名管理情報ＤＡ(ABAtenaKanriJohoBClass)
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/14　山崎　敏生
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/03/17 000001     追加時、共通項目を設定する
'* 2003/04/14 000002     種別をキーに取得するメソッドを追加
'* 2003/05/21 000003     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/08/28 000004     RDBアクセスログの修正
'* 2005/01/17 000005     宛名管理情報の識別キーのデータ整合性チェックを修正(数字→英数字)
'* 2007/07/27 000006     同一人代表者取得メソッド追加(吉澤)
'* 2007/10/03 000007     更新時に「備考」は何もチェックしないように変更(吉澤)
'* 2008/02/13 000008     氏名括弧編集制御取得メソッド追加（比嘉）
'* 2010/04/16 000009     VS2008対応（比嘉）
'* 2010/05/12 000010     本籍筆頭者取得区分取得メソッド、外字フラグ取得区分取得メソッド追加（比嘉）
'* 2011/05/18 000011     本名・通称名優先設定制御パラメータ取得メソッドを追加（比嘉）
'* 2014/12/18 000012     【AB21040】番号制度　宛名取得　直近検索区分パラメーター取得メソッドを追加（石合）
'* 2015/01/05 000013     【AB21034】番号制度　法人番号利用開始日パラメーター取得メソッドを追加（石合）
'* 2015/03/05 000014     【AB21034】番号制度　法人番号利用開始日のエラーメッセージを変更（石合）
'* 2018/05/07 000015     【AB27002】備考管理（石合）
'* 2018/05/22 000016     【AB24011】連絡先管理項目追加（石合）
'* 2020/08/03 000017     【AB32008】代納・送付先備考管理（石合）
'* 2020/08/21 000018     【AB32006】代納・送付先メンテナンス（石合）
'* 2020/11/10 000019     【AB00189】利用届出複数納税者ID対応（須江）
'* 2023/12/22 000020     【AB-0970-1_2】宛名GET日付項目設定対応(下村)
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

Public Class ABAtenaKanriJohoBClass
#Region "メンバ変数"
    ' メンバ変数の定義
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_strInsertSQL As String                                            'INSERT用SQL
    Private m_strUpdateSQL As String                                            'UPDATE用SQL
    Private m_strDeleteSQL As String                                            'DELETE用SQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT用パラメータコレクション
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE用パラメータコレクション
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass  'DELETE用パラメータコレクション

    '*履歴番号 000006 2007/07/27 追加開始
    Private m_strDoitsuHantei_Param As String() = {"10", "07"}             '同一人代表者の取得判定
    '*履歴番号 000006 2007/07/27 追加終了
    '*履歴番号 000008 2008/02/13 追加開始
    Private m_strShimeiKakkoKB_Param As String() = {"10", "15"}            '氏名括弧編集制御
    '*履歴番号 000008 2008/02/13 追加終了
    '*履歴番号 000010 2010/05/12 追加開始
    Private m_strHonsekiKB_Param As String() = {"10", "18"}                '本籍取得区分
    Private m_strShoriTeishiKB_Param As String() = {"10", "19"}            '処理停止区分取得区分
    '*履歴番号 000010 2010/05/12 追加終了
    '*履歴番号 000011 2011/05/18 追加開始
    Private m_strHonmyoTsushomeiYusenKB_Param As String() = {"10", "23"}   '本名通称名優先区分取得区分
    '*履歴番号 000011 2011/05/18 追加終了
    '*履歴番号 000019 2020/11/10 追加開始
    Private m_strHenkyakuFuyoGyomuCD_Param As String() = {"10", "46"}      ' 独自処理　利用届出共通納税返却不要業務
    '*履歴番号 000019 2020/11/10 追加終了
    '*履歴番号 000012 2014/12/18 追加開始
    Private m_strMyNumberChokkinSearchKB_Param() As String = {"35", "29"}   ' 番号制度　宛名取得　直近検索区分
    '*履歴番号 000012 2014/12/18 追加終了
    '*履歴番号 000013 2015/01/05 追加開始
    Private m_strHojinBangoRiyoKaishiYMD_Param() As String = {"35", "30"}   ' 番号制度　法人番号利用開始日
    '*履歴番号 000013 2015/01/05 追加終了
    '*履歴番号 000015 2018/05/07 追加開始
    Private m_strJutogaiBikoUmu_Param() As String = {"40", "07"}            ' 次期Ｒｅａｍｓ　住登外備考有無
    '*履歴番号 000015 2018/05/07 追加終了
    '*履歴番号 000016 2018/05/22 追加開始
    Private m_strRenrakusakiKakuchoUmu_Param() As String = {"40", "08"}     ' 次期Ｒｅａｍｓ　連絡先拡張有無
    '*履歴番号 000016 2018/05/22 追加終了
    '*履歴番号 000017 2020/08/03 追加開始
    Private m_strDainoSfskBikoUmu_Param() As String = {"40", "15"}          ' 代納・送付先備考有無
    '*履歴番号 000017 2020/08/03 追加終了
    '*履歴番号 000018 2020/08/21 追加開始
    Private m_strZeimokuCDConvertTable_Param() As String = {"10", "40"}     ' 税目コード変換テーブル
    Private m_strDainoSfskMainteShiyoUmu_Param() As String = {"12", "25"}   ' 代納・送付先メンテナンス使用有無
    '*履歴番号 000018 2020/08/21 追加終了
    Private m_strUmareYMDHenkan_Param() As String = {"51", "01"}            ' 標準準拠対応宛名GET 歴上日変換日付（生年月日）
    Private m_strShojoIdobiHenkan_Param() As String = {"51", "02"}          ' 標準準拠対応宛名GET 歴上日変換日付（消除異動日）
    Private m_strCknIdobiHenkan_Param() As String = {"51", "03"}            ' 標準準拠対応宛名GET 歴上日変換日付（直近異動日）

    ' コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaKanriJohoBClass"
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

        ' メンバ変数の初期化
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     宛名管理情報抽出
    '* 
    '* 構文           Public Overloads Function GetKanriJohoHoshu() As DataSet
    '* 
    '* 機能　　    　　宛名管理情報より該当データを全件取得する。
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         取得した宛名管理情報の該当データ（DataSet）
    '*                   構造：csAtenaKanriJohoEntity    インテリセンス：ABAtenaKanriJohoEntity
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu() As DataSet
        Const THIS_METHOD_NAME As String = "GetKanriJohoHoshu"          'このメソッド名
        Dim csAtenaKanriJohoEntity As DataSet                           '宛名管理情報データ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKanriJohoEntity.KANRINENDO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            ' 管理年度
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO
            cfUFParameterClass.Value = "0000"
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = "AB"
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000004 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '*履歴番号 000004 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            csAtenaKanriJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKanriJohoEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csAtenaKanriJohoEntity

    End Function

    '************************************************************************************************
    '* メソッド名     宛名管理情報抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String, 
    '*                                                            ByVal strShikibetsuKey As String) As DataSet
    '* 
    '* 機能　　    　　宛名管理情報より該当データを全件取得する。
    '* 
    '* 引数           strSHUKey As String           :種別キー
    '*                strShikibetsuKey As String    :識別キー
    '* 
    '* 戻り値         取得した宛名管理情報の該当データ（DataSet）
    '*                   構造：csAtenaKanriJohoEntity    インテリセンス：ABAtenaKanriJohoEntity
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String, ByVal strShikibetsuKey As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetKanriJohoHoshu(Overloads)"          'このメソッド名
        Dim csAtenaKanriJohoEntity As DataSet                           '宛名管理情報データ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKanriJohoEntity.KANRINENDO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SHUKEY)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_SHUKEY)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SHIKIBETSUKEY)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            ' 管理年度
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO
            cfUFParameterClass.Value = "0000"
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = "AB"
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 種別キー
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHUKEY
            cfUFParameterClass.Value = strSHUKey
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 識別キー
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY
            cfUFParameterClass.Value = strShikibetsuKey
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000004 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '*履歴番号 000004 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            csAtenaKanriJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKanriJohoEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csAtenaKanriJohoEntity

    End Function

    '************************************************************************************************
    '* メソッド名     宛名管理情報抽出(ｵｰﾊﾞｰﾛｰﾄﾞ)
    '* 
    '* 構文           Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String) As DataSet
    '* 
    '* 機能　　    　　宛名管理情報より該当データを全件取得する。
    '* 
    '* 引数           strSHUKey As String           :種別キー
    '* 
    '* 戻り値         取得した宛名管理情報の該当データ（DataSet）
    '*                   構造：csAtenaKanriJohoEntity    インテリセンス：ABAtenaKanriJohoEntity
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetKanriJohoHoshu(Overloads)"          'このメソッド名
        Dim csAtenaKanriJohoEntity As DataSet                           '宛名管理情報データ
        Dim strSQL As New StringBuilder                                 'SQL文文字列
        Dim cfUFParameterClass As UFParameterClass                      'パラメータクラス
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  'パラメータコレクションクラス

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL文の作成
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME)
            ' WHERE文結合
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKanriJohoEntity.KANRINENDO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SHUKEY)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_SHUKEY)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' 検索条件のパラメータコレクションオブジェクトを作成
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' 検索条件のパラメータを作成
            ' 管理年度
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO
            cfUFParameterClass.Value = "0000"
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = "AB"
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 種別キー
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHUKEY
            cfUFParameterClass.Value = strSHUKey
            ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*履歴番号 000004 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + strSQL.ToString + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:GetDataSet】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "】")
            '*履歴番号 000004 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            csAtenaKanriJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKanriJohoEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csAtenaKanriJohoEntity

    End Function

    '************************************************************************************************
    '* メソッド名     宛名管理情報追加
    '* 
    '* 構文           Public Function InsertKanriJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  宛名管理情報にデータを追加する。
    '* 
    '* 引数           csDataRow As DataRow  :追加データ
    '* 
    '* 戻り値         追加件数(Integer)
    '************************************************************************************************
    Public Function InsertKanriJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertKanriJoho"            'このメソッド名
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
            csDataRow(ABAtenaKanriJohoEntity.TANMATSUID) = m_cfControlData.m_strClientId            '端末ＩＤ
            csDataRow(ABAtenaKanriJohoEntity.SAKUJOFG) = "0"                                        '削除フラグ
            csDataRow(ABAtenaKanriJohoEntity.KOSHINCOUNTER) = Decimal.Zero                          '更新カウンタ
            csDataRow(ABAtenaKanriJohoEntity.SAKUSEINICHIJI) = strUpdateDateTime                    '作成日時
            csDataRow(ABAtenaKanriJohoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId             '作成ユーザー
            csDataRow(ABAtenaKanriJohoEntity.KOSHINNICHIJI) = strUpdateDateTime                     '更新日時
            csDataRow(ABAtenaKanriJohoEntity.KOSHINUSER) = m_cfControlData.m_strUserId              '更新ユーザー

            ' 当クラスのデータ整合性チェックを行う
            For Each csDataColumn In csDataRow.Table.Columns
                ' データ整合性チェック
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*履歴番号 000004 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strInsertSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
            '*履歴番号 000004 2003/08/28 修正終了

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
    '* メソッド名     宛名管理情報更新
    '* 
    '* 構文           Public Function UpdateKanriJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  宛名管理情報のデータを更新する。
    '* 
    '* 引数           csDataRow As DataRow  :更新データ
    '* 
    '* 戻り値         更新件数(Integer)
    '************************************************************************************************
    Public Function UpdateKanriJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateKanriJoho"         'このメソッド名
        Dim cfParam As UFParameterClass                     'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000009
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000009
        Dim intUpdCnt As Integer                            '更新件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' 共通項目の編集を行う
            csDataRow(ABAtenaKanriJohoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '端末ＩＤ
            csDataRow(ABAtenaKanriJohoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaKanriJohoEntity.KOSHINCOUNTER)) + 1     '更新カウンタ
            csDataRow(ABAtenaKanriJohoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")    '更新日時
            csDataRow(ABAtenaKanriJohoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '更新ユーザー

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaKanriJohoEntity.PREFIX_KEY.RLength) = ABAtenaKanriJohoEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' データ整合性チェック
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    ' パラメータコレクションへ値の設定
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000004 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strUpdateSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "】")
            '*履歴番号 000004 2003/08/28 修正終了

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
    '* メソッド名     宛名管理情報削除（物理）
    '* 
    '* 構文           Public Overloads Function DeleteKanriJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　  宛名管理情報のデータを削除（物理）する。
    '* 
    '* 引数           csDataRow As DataRow      :削除データ
    '* 
    '* 戻り値         削除（物理）件数(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteKanriJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteKanriJoho（物理）"
        Dim cfParam As UFParameterClass                                 'パラメータクラス
        '* corresponds to VS2008 Start 2010/04/16 000009
        'Dim csDataColumn As DataColumn
        'Dim objErrorStruct As UFErrorStruct                             'エラー定義構造体
        '* corresponds to VS2008 End 2010/04/16 000009
        Dim intDelCnt As Integer                                        '削除件数

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strDeleteSQL Is Nothing Or m_strDeleteSQL = String.Empty Or _
                m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' 作成済みのパラメータへ更新行から値を設定する。
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' キー項目は更新前の値で設定
                If (cfParam.ParameterName.RSubstring(0, ABAtenaKanriJohoEntity.PREFIX_KEY.RLength) = ABAtenaKanriJohoEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' パラメータコレクションへ値の設定
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*履歴番号 000004 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                            "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_strDeleteSQL + "】")

            ' RDBアクセスログ出力
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "【クラス名:" + Me.GetType.Name + "】" + _
                                        "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
                                        "【実行メソッド名:ExecuteSQL】" + _
                                        "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "】")
            '*履歴番号 000004 2003/08/28 修正終了

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
    '* 機能　　    　　INSERT, UPDATE, DELETEの各SQLを作成、パラメータコレクションを作成する
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
        Dim strDeleteSQL As New StringBuilder                       '削除SQL文文字列
        Dim strWhere As New StringBuilder                           '更新削除SQL文Where文文字列

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABAtenaKanriJohoEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' 更新削除Where文作成
            strWhere.Append(" WHERE ")
            strWhere.Append(ABAtenaKanriJohoEntity.KANRINENDO)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO)
            strWhere.Append(" AND ")
            strWhere.Append(ABAtenaKanriJohoEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABAtenaKanriJohoEntity.SHUKEY)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_SHUKEY)
            strWhere.Append(" AND ")
            strWhere.Append(ABAtenaKanriJohoEntity.SHIKIBETSUKEY)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY)
            strWhere.Append(" AND ")
            strWhere.Append(ABAtenaKanriJohoEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_KOSHINCOUNTER)

            ' UPDATE SQL文の作成
            m_strUpdateSQL = "UPDATE " + ABAtenaKanriJohoEntity.TABLE_NAME + " SET "

            ' DELETE（物理） SQL文の作成
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            ' SELECT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE パラメータコレクションのインスタンス化
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE（物理） パラメータコレクションのインスタンス化
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL文の作成
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL文の作成
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            ' 管理年度
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 業務コード
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 種別キー
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHUKEY
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 識別キー
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' 更新カウンタ
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

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
    '* 機能　　       宛名管理情報のデータ整合性チェックを行います。
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
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strColumnName.ToUpper()
                Case ABAtenaKanriJohoEntity.SHICHOSONCD                 '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KYUSHICHOSONCD              '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KANRINENDO                  '管理年度
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KANRINENDO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.GYOMUCD                     '業務コード
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_GYOMUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SHUKEY                      '種別キー
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHUKEY)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SHIKIBETSUKEY               '識別キー
                    '*履歴番号 000005 2006/01/17 修正開始
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        'If (Not UFStringClass.CheckNumber(strValue)) Then
                        '*履歴番号 000005 2006/01/17 修正終了
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHIKIBETSUKEY)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SHUKEYMEISHO                '種別キー名称
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHUKEYMEISHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SHIKIBETSUKEYMEISHO         '識別キー名称
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHIKIBETSUKEYMEISHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.PARAMETER                   'パラメータ
                    '何もしない
                Case ABAtenaKanriJohoEntity.BIKO                        '備考
                    '*履歴番号 000007 2007/10/01 削除開始
                    'If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                    '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '    'エラー定義を取得
                    '    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_BIKO)
                    '    '例外を生成
                    '    Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    'End If
                    '*履歴番号 000007 2007/10/01 削除終了
                Case ABAtenaKanriJohoEntity.RESERVE                     'リザーブ
                    '何もしない
                Case ABAtenaKanriJohoEntity.TANMATSUID                  '端末ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SAKUJOFG                    '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KOSHINCOUNTER               '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SAKUSEINICHIJI              '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SAKUSEIUSER                 '作成ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KOSHINNICHIJI               '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KOSHINUSER                  '更新ユーザ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KOSHINUSER)
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

    '*履歴番号 000006 2007/07/27 追加開始
    '************************************************************************************************
    '* メソッド名     同一人代表者取得の判定パラメータ取得
    '* 
    '* 構文           Public Function GetDoitsuHantei_Param() As DataSet
    '* 
    '* 機能           同一人代表者取得の判定パラメータを取得する
    '* 
    '* 引数           strShichosonCD As String : 市町村コード
    '* 
    '* 戻り値         String : 
    '************************************************************************************************
    Public Function GetDoitsuHantei_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetDoitsuHantei_Param"

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDS = GetKanriJohoHoshu(m_strDoitsuHantei_Param(0), m_strDoitsuHantei_Param(1))

            '取得データのチェック
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                'レコードが存在しない場合は、本人情報の取得とする
                strRet = ABConstClass.PRM_HONNIN
            ElseIf CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = ABConstClass.PRM_DAIHYO Then
                'パラメータが同一人代表者取得の場合は、同一人代表者の取得とする
                strRet = ABConstClass.PRM_DAIHYO
            Else
                '上記以外は、本人情報の取得とする
                strRet = ABConstClass.PRM_HONNIN
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

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
    End Function
    '*履歴番号 000006 2007/07/27 追加終了
    '*履歴番号 000008 2008/02/13 追加開始
    '************************************************************************************************
    '* メソッド名     氏名括弧編集制御パラメータ取得
    '* 
    '* 構文           Public Function GetShimeiKakkoKB_Param() As DataSet
    '* 
    '* 機能           氏名括弧編集制御の判定パラメータを取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         String : 
    '************************************************************************************************
    Public Function GetShimeiKakkoKB_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetShimeiKakkoKB_Param"

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDS = GetKanriJohoHoshu(m_strShimeiKakkoKB_Param(0), m_strShimeiKakkoKB_Param(1))

            '取得データのチェック
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                'レコードが存在しない場合は、標準とする
                strRet = "0"
            Else
                'レコードが存在する場合は、管理情報をセットする
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

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
    End Function
    '*履歴番号 000008 2008/02/13 追加終了
    '*履歴番号 000010 2010/05/12 追加開始
    '************************************************************************************************
    '* メソッド名     本籍取得区分パラメータ取得
    '* 
    '* 構文           Public Function GetHonsekiKB_Param() As DataSet
    '* 
    '* 機能           本籍取得区分パラメータを取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         String 
    '************************************************************************************************
    Public Function GetHonsekiKB_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetHonsekiKB_Param"

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDS = GetKanriJohoHoshu(m_strHonsekiKB_Param(0), m_strHonsekiKB_Param(1))

            '取得データのチェック
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                'レコードが存在しない場合は、空白とする
                strRet = "0"
            Else
                'レコードが存在する場合は、管理情報をセットする
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

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
    End Function
    '************************************************************************************************
    '* メソッド名     処理停止区分取得区分パラメータ取得
    '* 
    '* 構文           Public Function GetShoriteishiKB_Param() As DataSet
    '* 
    '* 機能           処理停止区分取得区分パラメータを取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         String 
    '************************************************************************************************
    Public Function GetShoriteishiKB_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetShoriteishiKB_Param"

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDS = GetKanriJohoHoshu(m_strShoriTeishiKB_Param(0), m_strShoriTeishiKB_Param(1))

            '取得データのチェック
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                'レコードが存在しない場合は、空白とする
                strRet = "0"
            Else
                'レコードが存在する場合は、管理情報をセットする
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

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
    End Function
    '*履歴番号 000010 2010/05/12 追加終了

    '*履歴番号 000011 2011/05/18 追加開始
    '************************************************************************************************
    '* メソッド名     本名・通称名優先制御区分パラメータ取得
    '* 
    '* 構文           Public Function GetHonmyoTsushomeiYusenKB_Param() As String
    '* 
    '* 機能           本名・通称名優先制御区分パラメータを取得する
    '* 
    '* 引数           なし
    '* 
    '* 戻り値         String 
    '************************************************************************************************
    Public Function GetHonmyoTsushomeiYusenKB_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetHonmyoTsushomeiYusenKB_Param"

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDS = GetKanriJohoHoshu(m_strHonmyoTsushomeiYusenKB_Param(0), m_strHonmyoTsushomeiYusenKB_Param(1))

            '取得データのチェック
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                'レコードが存在しない場合は、空白とする
                strRet = "0"
            Else
                'レコードが存在する場合は、管理情報をセットする
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

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
    End Function
    '*履歴番号 000011 2011/05/18 追加終了

    '*履歴番号 000012 2014/12/18 追加開始
#Region "番号制度　宛名取得　直近検索区分　パラメーター取得"

    ''' <summary>
    ''' 番号制度　宛名取得　直近検索区分　パラメーター取得
    ''' </summary>
    ''' <returns>番号制度　宛名取得　直近検索区分</returns>
    ''' <remarks></remarks>
    Public Function GetMyNumberChokkinSearchKB_Param() As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csDataSet As DataSet
        Dim strResult As String

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDataSet = GetKanriJohoHoshu(m_strMyNumberChokkinSearchKB_Param(0), m_strMyNumberChokkinSearchKB_Param(1))

            ' 取得データのチェック
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                strResult = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                Select Case strResult

                    Case ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode.ToString, _
                         ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode.ToString
                        ' noop
                    Case Else

                        ' 規定値以外（値なしを含む）の場合は、"2"（履歴を含めて検索）を設定する。
                        strResult = ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode.ToString

                End Select

            Else

                ' レコードが存在しない場合は、"2"（履歴を含めて検索）を設定する。
                strResult = ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode.ToString

            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return strResult

    End Function

#End Region
    '*履歴番号 000012 2014/12/18 追加終了

    '*履歴番号 000013 2015/01/05 追加開始
#Region "番号制度　法人番号利用開始日　パラメーター取得"

    ''' <summary>
    ''' 番号制度　法人番号利用開始日　パラメーター取得
    ''' </summary>
    ''' <returns>番号制度　法人番号利用開始日</returns>
    ''' <remarks></remarks>
    Public Function GetHojinBangoRiyoKaishiYMD_Param() As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csDataSet As DataSet
        Dim strResult As String
        Dim cfDate As UFDateClass                           ' 日付クラス
        Dim cfErrorClass As UFErrorClass                    ' エラークラス
        Dim cfErrorStruct As UFErrorStruct                  ' エラー定義構造体

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDataSet = GetKanriJohoHoshu(m_strHojinBangoRiyoKaishiYMD_Param(0), m_strHojinBangoRiyoKaishiYMD_Param(1))

            ' パラメーター値の取り出し
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then
                strResult = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString
            Else
                strResult = String.Empty
            End If

            ' 取得データのチェック
            cfDate = New UFDateClass(m_cfConfigDataClass, UFDateSeparator.None, UFDateFillType.Zero, UFEraType.Number, False, False)
            cfDate.p_strDateValue = strResult
            If (cfDate.CheckDate = True) Then
                strResult = cfDate.p_strSeirekiYMD
            Else

                ' 実在日以外の場合は、エラーとする。（業共の動きに準拠させる。）
                '*履歴番号 000014 2015/03/05 修正開始
                'cfErrorClass = New UFErrorClass
                'cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001053)
                'Throw New Exception(cfErrorStruct.m_strErrorMessage)
                cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                cfErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003144)
                Throw New Exception(String.Format("{0} 宛名管理情報 ： 種別キー【{1}】、識別キー【{2}】", _
                                                  cfErrorStruct.m_strErrorMessage, _
                                                  m_strHojinBangoRiyoKaishiYMD_Param(0), _
                                                  m_strHojinBangoRiyoKaishiYMD_Param(1)))
                '*履歴番号 000014 2015/03/05 修正終了

            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return strResult

    End Function

#End Region
    '*履歴番号 000013 2015/01/05 追加終了

    '*履歴番号 000015 2018/05/07 追加開始
#Region "次期Ｒｅａｍｓ　住登外備考有無　パラメーター取得"

    ''' <summary>
    ''' 次期Ｒｅａｍｓ　住登外備考有無　パラメーター取得
    ''' </summary>
    ''' <returns>次期Ｒｅａｍｓ　住登外備考有無</returns>
    ''' <remarks></remarks>
    Public Function GetJutogaiBikoUmu_Param() As Boolean

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim blnResult As Boolean = False
        Dim csDataSet As DataSet
        Dim strParameter As String

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDataSet = GetKanriJohoHoshu(m_strJutogaiBikoUmu_Param(0), m_strJutogaiBikoUmu_Param(1))

            ' 取得データのチェック
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                If (strParameter.Trim = "1") Then
                    blnResult = True
                Else
                    blnResult = False
                End If

            Else
                blnResult = False
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return blnResult

    End Function

#End Region
    '*履歴番号 000015 2018/05/07 追加終了

    '*履歴番号 000016 2018/05/22 追加開始
#Region "次期Ｒｅａｍｓ　連絡先拡張有無　パラメーター取得"

    ''' <summary>
    ''' 次期Ｒｅａｍｓ　連絡先拡張有無　パラメーター取得
    ''' </summary>
    ''' <returns>次期Ｒｅａｍｓ　連絡先拡張有無</returns>
    ''' <remarks></remarks>
    Public Function GetRenrakusakiKakuchoUmu_Param() As Boolean

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim blnResult As Boolean = False
        Dim csDataSet As DataSet
        Dim strParameter As String

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDataSet = GetKanriJohoHoshu(m_strRenrakusakiKakuchoUmu_Param(0), m_strRenrakusakiKakuchoUmu_Param(1))

            ' 取得データのチェック
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                If (strParameter.Trim = "1") Then
                    blnResult = True
                Else
                    blnResult = False
                End If

            Else
                blnResult = False
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return blnResult

    End Function

#End Region
    '*履歴番号 000016 2018/05/22 追加終了

    '*履歴番号 000017 2020/08/03 追加開始
#Region "代納・送付先備考有無　パラメーター取得"

    ''' <summary>
    ''' 代納・送付先備考有無　パラメーター取得
    ''' </summary>
    ''' <returns>代納・送付先備考有無</returns>
    ''' <remarks></remarks>
    Public Function GetDainoSfskBikoUmu_Param() As Boolean

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim blnResult As Boolean = False
        Dim csDataSet As DataSet
        Dim strParameter As String

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDataSet = GetKanriJohoHoshu(m_strDainoSfskBikoUmu_Param(0), m_strDainoSfskBikoUmu_Param(1))

            ' 取得データのチェック
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                If (strParameter.Trim = "1") Then
                    blnResult = True
                Else
                    blnResult = False
                End If

            Else
                blnResult = False
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try

        Return blnResult

    End Function

#End Region
    '*履歴番号 000017 2020/08/03 追加終了

    '*履歴番号 000018 2020/08/21 追加開始
#Region "税目コード変換テーブル　パラメーター取得"

    ''' <summary>
    ''' 税目コード変換テーブル　パラメーター取得
    ''' </summary>
    ''' <returns>税目コード変換テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetZeikokuCDConvertTable_Param() As Hashtable

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csResult As Hashtable
        Dim csDataSet As DataSet
        Dim strParameter As String
        Dim a_strParameter() As String
        Dim a_strValue() As String

        Const SEPARATOR_SLASH As Char = "/"c
        Const SEPARATOR_COMMA As Char = ","c

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 返信オブジェクトのインスタンス化
            csResult = New Hashtable

            ' 管理情報からデータを取得
            csDataSet = GetKanriJohoHoshu(m_strZeimokuCDConvertTable_Param(0), m_strZeimokuCDConvertTable_Param(1))

            ' 取得データのチェック
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                ' パラメーターを取得
                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                ' スラッシュで区切る
                a_strParameter = strParameter.Split(SEPARATOR_SLASH)

                ' 業務数分ループ
                For Each strValue As String In a_strParameter

                    ' カンマで区切る
                    a_strValue = strValue.Split(SEPARATOR_COMMA)

                    ' 項目数分ループ
                    If (a_strValue.Count > 1) Then

                        ' 重複チェックを行いながら、ハッシュへ追加する
                        If (csResult.ContainsKey(a_strValue(0)) = True) Then
                            ' noop
                        Else
                            csResult.Add(a_strValue(0), a_strValue(1))
                        End If

                    Else
                        ' noop
                    End If

                Next strValue

            Else
                ' noop
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

        Return csResult

    End Function

#End Region

#Region "代納・送付先メンテナンス使用有無　パラメーター取得"

    ''' <summary>
    ''' 代納・送付先メンテナンス使用有無　パラメーター取得
    ''' </summary>
    ''' <returns>代納・送付先メンテナンス使用有無</returns>
    ''' <remarks></remarks>
    Public Function GetDainoSfskMainteShiyoUmu_Param() As Boolean

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim blnResult As Boolean
        Dim csDataSet As DataSet
        Dim strParameter As String

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 返信オブジェクトの初期化
            blnResult = False

            ' 管理情報からデータを取得
            csDataSet = GetKanriJohoHoshu(m_strDainoSfskMainteShiyoUmu_Param(0), m_strDainoSfskMainteShiyoUmu_Param(1))

            ' 取得データのチェック
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                ' パラメーターを取得
                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                ' 取得結果を判定
                If (strParameter.Trim = "1") Then
                    blnResult = True
                Else
                    blnResult = False
                End If

            Else
                ' noop
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + cfAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + cfAppExp.Message + "】")

            ' ワーニングをスローする
            Throw

        Catch csExp As Exception

            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + csExp.Message + "】")

            ' システムエラーをスローする
            Throw

        End Try

        Return blnResult

    End Function

#End Region
    '*履歴番号 000018 2020/08/21 追加終了

    '*履歴番号 000019 2020/11/10 追加開始
#Region "独自処理　利用届出共通納税返却不要業務　パラメーター取得"

    ''' <summary>
    ''' 独自処理　利用届出共通納税返却不要業務　パラメーター取得
    ''' </summary>
    ''' <returns>独自処理　利用届出共通納税返却不要業務</returns>
    ''' <remarks></remarks>
    Public Function GetHenkyakuFuyoGyomuCD_Param() As String

        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetHenkyakuFuyoGyomuCD_Param"


        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDS = GetKanriJohoHoshu(m_strHenkyakuFuyoGyomuCD_Param(0), m_strHenkyakuFuyoGyomuCD_Param(1))

            '取得データのチェック
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                'レコードが存在しない場合は、空白とする
                strRet = ""
            Else
                'レコードが存在する場合は、管理情報をセットする
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

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
    End Function

#End Region
    '*履歴番号 000019 2020/11/10 追加終了

#Region "標準準拠対応宛名GET　歴上日変換日付（生年月日）　パラメーター取得"

    ''' <summary>
    ''' 標準準拠対応宛名GET　歴上日変換日付（生年月日）　パラメーター取得
    ''' </summary>
    ''' <returns>標準準拠対応宛名GET　歴上日変換日付（生年月日）</returns>
    ''' <remarks></remarks>
    Public Function GetUmareYMDHenkanHizuke_Param() As String

        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetUmareYMDHenkanHizuke_Param"

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDS = GetKanriJohoHoshu(m_strUmareYMDHenkan_Param(0), m_strUmareYMDHenkan_Param(1))

            '取得データのチェック
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                'レコードが存在しない場合は、空白とする
                strRet = String.Empty
            Else
                'レコードが存在する場合は、管理情報をセットする
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try
    End Function

#End Region

#Region "標準準拠対応宛名GET　歴上日変換日付（消除異動日）　パラメーター取得"

    ''' <summary>
    ''' 標準準拠対応宛名GET　歴上日変換日付（消除異動日）　パラメーター取得
    ''' </summary>
    ''' <returns>標準準拠対応宛名GET　歴上日変換日付（消除異動日）</returns>
    ''' <remarks></remarks>
    Public Function GetShojoIdobiHenkanHizuke_Param() As String

        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetShojoIdobiHenkanHizuke_Param"

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDS = GetKanriJohoHoshu(m_strShojoIdobiHenkan_Param(0), m_strShojoIdobiHenkan_Param(1))

            '取得データのチェック
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                'レコードが存在しない場合は、空白とする
                strRet = String.Empty
            Else
                'レコードが存在する場合は、管理情報をセットする
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try
    End Function

#End Region

#Region "標準準拠対応宛名GET　歴上日変換日付（直近異動日）　パラメーター取得"

    ''' <summary>
    ''' 標準準拠対応宛名GET　歴上日変換日付（直近異動日）　パラメーター取得
    ''' </summary>
    ''' <returns>標準準拠対応宛名GET　歴上日変換日付（直近異動日）</returns>
    ''' <remarks></remarks>
    Public Function GetCknIdobiHenkanHizuke_Param() As String

        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetCknIdobiHenkanHizuke_Param"

        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' 管理情報からデータを取得
            csDS = GetKanriJohoHoshu(m_strCknIdobiHenkan_Param(0), m_strCknIdobiHenkan_Param(1))

            '取得データのチェック
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                'レコードが存在しない場合は、空白とする
                strRet = String.Empty
            Else
                'レコードが存在する場合は、管理情報をセットする
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

        Catch exAppException As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【ワーニングコード:" + exAppException.p_strErrorCode + "】" +
                                        "【ワーニング内容:" + exAppException.Message + "】")
            ' ワーニングをスローする
            Throw

        Catch exException As Exception 'システムエラーをキャッチ
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "【クラス名:" + THIS_CLASS_NAME + "】" +
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" +
                                        "【エラー内容:" + exException.Message + "】")
            ' システムエラーをスローする
            Throw

        End Try
    End Function

#End Region
#End Region

End Class
