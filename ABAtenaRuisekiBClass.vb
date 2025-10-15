'************************************************************************************************
'* 業務名          宛名管理システム
'* 
'* クラス名        ＡＢ宛名累積マスタＤＡ
'* 
'* バージョン情報   Ver 1.0
'* 
'* 日付け　作成者   2003/01/15　滝沢　欽也
'*
'* 著作権          （株）電算
'************************************************************************************************
'* 修正履歴　　履歴番号　　修正内容
'* 2003/03/10 000001     住所ＣＤ等の整合性チェックに誤り
'* 2003/03/31 000002     整合性チェックをTrimした値でチェックする
'* 2003/04/16 000003     生和暦年月日の日付チェックを数値チェックに変更
'*                       検索用カナの半角カナチェックをＡＮＫチェックに変更
'* 2003/05/20 000004     エラー、日付クラスのｲﾝｽﾀﾝｽをｺﾝｽﾄﾗｸﾀに変更
'* 2003/08/28 000005     RDBアクセスログの修正
'* 2003/09/11 000006     端末ＩＤ整合性チェックをANKにする
'* 2003/10/09 000007     作成ユーザー・更新ユーザーチェックの変更
'* 2003/10/30 000008     仕様変更、カタカナチェックをANKチェックに変更
'* 2003/11/18 000009     仕様変更：項目追加
'* 2003/12/01 000010     仕様変更：項目名の変更(SYORINICHIJI->SHORINICHIJI)
'*                       仕様変更：項目名の変更(KOKUHOTIAHKHONHIKBMEISHO->KOKUHOTISHKHONHIKBMEISHO)
'* 2004/03/06 000011     仕様変更：国保保険証番号のチェックなしに変更
'* 2004/08/13 000012     仕様変更、地区コードチェックをANKチェックに変更
'* 2004/11/12 000013     データチェックを行なわない
'* 2005/12/26 000014     仕様変更：行政区ＣＤをANKチェックに変更(マルゴ村山)
'* 2010/04/16 000015     VS2008対応（比嘉）
'* 2011/10/24 000016     【AB17010】＜住基法改正対応＞宛名累積付随マスタ追加   (小松)
'* 2023/08/14 000017    【AB-0820-1】住登外管理項目追加(早崎)
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* 参照する名前空間
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Data
Imports System.Text

'************************************************************************************************
'*
'* 宛名累積マスタ取得時に使用するパラメータクラス
'*
'************************************************************************************************
Public Class ABAtenaRuisekiBClass
#Region "メンバ変数"
    'パラメータのメンバ変数
    Private m_cfLogClass As UFLogClass                      ' ログ出力クラス
    Private m_cfControlData As UFControlData                ' コントロールデータ
    Private m_cfConfigDataClass As UFConfigDataClass        ' コンフィグデータ
    Private m_cfRdbClass As UFRdbClass                      ' ＲＤＢクラス
    Private m_strInsertSQL As String                        ' INSERT用SQL
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT用パラメータコレクション
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT用パラメータコレクション
    Private m_cfErrorClass As UFErrorClass                  ' エラー処理クラス
    Private m_cfDateClass As UFDateClass                    ' 日付クラス

    '*履歴番号 000016 2011/10/24 追加開始
    Private m_csSekoYMDHanteiB As ABSekoYMDHanteiBClass             '施行日判定Bｸﾗｽ
    Private m_csAtenaRuisekiFZYB As ABAtenaRuisekiFZYBClass         '宛名累積付随マスタBｸﾗｽ
    Private m_blnJukihoKaiseiFG As Boolean = False
    Private m_strJukihoKaiseiKB As String                           '住基法改正区分
    '*履歴番号 000016 2011/10/24 追加終了

    '*履歴番号 000017 2023/08/14 追加開始
    Private m_csAtenaRuisekiHyojunB As ABAtenaRuiseki_HyojunBClass            '宛名累積_標準マスタBｸﾗｽ
    Private m_csAtenaRuisekiFZYHyojunB As ABAtenaRuisekiFZY_HyojunBClass      '宛名累積付随_標準マスタBｸﾗｽ
    '*履歴番号 000017 2023/08/14 追加終了

    '　コンスタント定義
    Private Const THIS_CLASS_NAME As String = "ABAtenaRuisekiBClass"                ' クラス名
    Private Const THIS_BUSINESSID As String = "AB"                                  ' 業務コード

    Private Const JUKIHOKAISEIKB_ON As String = "1"

#End Region

#Region "プロパティ"
    '*履歴番号 000016 2011/10/24 追加開始
    Public WriteOnly Property p_strJukihoKaiseiKB() As String      ' 住基法改正区分
        Set(ByVal Value As String)
            m_strJukihoKaiseiKB = Value
        End Set
    End Property
    '*履歴番号 000016 2011/10/24 追加終了
#End Region

#Region "コンストラクタ"
    '************************************************************************************************
    '* メソッド名      コンストラクタ
    '* 
    '* 構文           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass, 
    '*                               ByVal cfUFRdbClass As UFRdbClass)
    '* 
    '* 機能　　        初期化処理
    '* 
    '* 引数           cfUFControlData As UFControlData          : コントロールデータオブジェクト
    '*                cfUFConfigDataClass as UFConfigDataClass  : コンフィグデータオブジェクト
    '*                cfUFRdbClass as UFRdbClass                : データベースアクセス用オブジェクト
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
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' パラメータのメンバ変数
        m_strInsertSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing

        '*履歴番号 000016 2011/10/24 追加開始
        m_strJukihoKaiseiKB = String.Empty

        '住基法改正ﾌﾗｸﾞ取得
        Call GetJukihoKaiseiFG()
        '*履歴番号 000016 2011/10/24 追加終了
    End Sub
#End Region

#Region "メソッド"
    '************************************************************************************************
    '* メソッド名     宛名累積マスタ抽出
    '* 
    '* 構文           Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
    '*                                                          ByVal strYusenKB As String) As DataSet
    '* 
    '* 機能　　    　　住登外マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD    : 住民コード
    '*                strYusenKB    : 優先区分
    '* 
    '* 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
                                              ByVal strYusenKB As String) As DataSet
        Return Me.GetAtenaRuiseki(strJuminCD, "", "", strYusenKB)
    End Function

    '************************************************************************************************
    '* メソッド名     宛名累積マスタ抽出
    '* 
    '* 構文           Public Overloads Function GetAtenaRuiseki(ByVal strKaishiNichiji As String, _
    '*                                                          ByVal strSyuryoNichiji As String, _
    '*                                                          ByVal strYusenKB As String) As DataSet
    '* 
    '* 機能　　    　　住登外マスタより該当データを取得する
    '* 
    '* 引数           strKaishiNichiji  : 開始日時
    '*                strSyuryoNichiji  : 終了日時
    '*                strYusenKB        : 優先区分
    '* 
    '* 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetAtenaRuiseki(ByVal strKaishiNichiji As String, _
                                              ByVal strSyuryoNichiji As String, _
                                              ByVal strYusenKB As String) As DataSet
        Return Me.GetAtenaRuiseki("", strKaishiNichiji, strSyuryoNichiji, strYusenKB)
    End Function

    '************************************************************************************************
    '* メソッド名     宛名累積マスタ抽出
    '* 
    '* 構文           Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
    '*                                                          ByVal strKaishiNichiji As String, _
    '*                                                          ByVal strSyuryoNichiji As String, _
    '*                                                          ByVal strYusenKB As String) As DataSet
    '* 
    '* 機能　　    　　住登外マスタより該当データを取得する
    '* 
    '* 引数           strJuminCD        : 住民コード
    '*                strKaishiNichiji  : 開始日時
    '*                strSyuryoNichiji  : 終了日時
    '*                strYusenKB        : 優先区分
    '* 
    '* 戻り値         DataSet : 取得した宛名履歴マスタの該当データ
    '************************************************************************************************
    Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
                                              ByVal strKaishiNichiji As String, _
                                              ByVal strSyuryoNichiji As String, _
                                              ByVal strYusenKB As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaRuiseki"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体
        Dim cfUFParameterClass As UFParameterClass          'パラメータクラス
        Dim csAtenaRuisekiEntity As DataSet                 '宛名累積DataSet
        Dim strKaishiNichiji2 As String                     '開始日時
        Dim strSyuryoNichiji2 As String                     '終了日時
        Dim strSQL As StringBuilder
        Dim strWHERE As StringBuilder
        Dim csDataSchema As DataSet

        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' パラメータコレクションのインスタンス化
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass()

            ' パラメータチェック
            ' 開始日時チェック
            If strKaishiNichiji.RLength = 17 Then
                strKaishiNichiji2 = strKaishiNichiji

            ElseIf strKaishiNichiji.RLength = 8 Then
                strKaishiNichiji2 = strKaishiNichiji + "000000000"

            ElseIf (strKaishiNichiji = String.Empty) And (strSyuryoNichiji = String.Empty) Then
                strKaishiNichiji2 = String.Empty
            Else
                'エラー定義を取得
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_KAISHINICHIJI)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            '終了日時チェック
            If strSyuryoNichiji.RLength = 17 Then
                strSyuryoNichiji2 = strSyuryoNichiji

            ElseIf strSyuryoNichiji.RLength = 8 Then
                strSyuryoNichiji2 = strSyuryoNichiji + "000000000"

            ElseIf strSyuryoNichiji = String.Empty Then
                strSyuryoNichiji2 = String.Empty
            Else
                'エラー定義を取得
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_SYURYONICHIJI)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            '優先区分
            If Not (strYusenKB = "1" Or strYusenKB = "2") Then
                'エラー定義を取得
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_YUSENKB)
                '例外を生成
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If


            ' SQL文の作成
            strSQL = New StringBuilder()
            '*履歴番号 000016 2011/10/24 修正開始
            'strSQL.Append("SELECT * FROM ")
            'strSQL.Append(ABAtenaRuisekiEntity.TABLE_NAME)
            '住基法改正以降は宛名累積付随マスタを付加
            If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                strSQL.AppendFormat("SELECT {0}.*", ABAtenaRuisekiEntity.TABLE_NAME)
                Call SetFZYEntity(strSQL)
                strSQL.AppendFormat(" FROM {0}", ABAtenaRuisekiEntity.TABLE_NAME)
                Call SetFZYJoin(strSQL)
            Else
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABAtenaRuisekiEntity.TABLE_NAME)
            End If
            '*履歴番号 000016 2011/10/24 修正終了

            '*履歴番号 000016 2011/10/24 追加開始
            csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRuisekiEntity.TABLE_NAME, False)
            '*履歴番号 000016 2011/10/24 追加終了


            strSQL.Append(" WHERE ")

            'WHERE句の作成
            strWHERE = New StringBuilder()
            '住民コード
            If Not (strJuminCD = String.Empty) Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                '*履歴番号 000016 2011/10/24 追加開始
                '住基法改正以降は宛名累積付随マスタを付加
                If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                    strWHERE.AppendFormat("{0}.", ABAtenaRuisekiEntity.TABLE_NAME)
                Else
                    '処理なし
                End If
                '*履歴番号 000016 2011/10/24 追加終了
                strWHERE.Append(ABAtenaRuisekiEntity.JUMINCD)
                strWHERE.Append(" = ")
                strWHERE.Append(ABAtenaRuisekiEntity.KEY_JUMINCD)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.KEY_JUMINCD
                cfUFParameterClass.Value = strJuminCD
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '開始日時
            If Not (strKaishiNichiji2 = String.Empty) Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                '*履歴番号 000016 2011/10/24 追加開始
                '住基法改正以降は宛名累積付随マスタを付加
                If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                    strWHERE.AppendFormat("{0}.", ABAtenaRuisekiEntity.TABLE_NAME)
                Else
                    '処理なし
                End If
                '*履歴番号 000016 2011/10/24 追加終了
                '*履歴番号 000010 2003/12/01 修正開始
                'strWHERE.Append(ABAtenaRuisekiEntity.SYORINICHIJI)
                strWHERE.Append(ABAtenaRuisekiEntity.SHORINICHIJI)
                '*履歴番号 000010 2003/12/01 修正終了
                strWHERE.Append(" >= ")
                strWHERE.Append(ABAtenaRuisekiEntity.KEY_SYORINICHIJI)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.KEY_SYORINICHIJI
                cfUFParameterClass.Value = strKaishiNichiji2
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '終了日時
            If Not (strSyuryoNichiji2 = String.Empty) Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                '*履歴番号 000016 2011/10/24 追加開始
                '住基法改正以降は宛名累積付随マスタを付加
                If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                    strWHERE.AppendFormat("{0}.", ABAtenaRuisekiEntity.TABLE_NAME)
                Else
                    '処理なし
                End If
                '*履歴番号 000016 2011/10/24 追加終了
                '*履歴番号 000010 2003/12/01 修正開始
                'strWHERE.Append(ABAtenaRuisekiEntity.SYORINICHIJI)
                strWHERE.Append(ABAtenaRuisekiEntity.SHORINICHIJI)
                '*履歴番号 000010 2003/12/01 修正終了
                strWHERE.Append(" <= ")
                strWHERE.Append(ABAtenaRuisekiEntity.PARAM_SYORINICHIJI)
                ' 検索条件のパラメータを作成
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.PARAM_SYORINICHIJI
                cfUFParameterClass.Value = strSyuryoNichiji2
                ' 検索条件のパラメータコレクションオブジェクトにパラメータオブジェクトの追加
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '優先区分
            If (strYusenKB = "1") Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                strWHERE.Append(ABAtenaRuisekiEntity.JUTOGAIYUSENKB)
                strWHERE.Append(" = '1'")
            End If
            If (strYusenKB = "2") Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                strWHERE.Append(ABAtenaRuisekiEntity.JUMINYUSENIKB)
                strWHERE.Append(" = '1'")
            End If


            'ORDER句を結合
            If strWHERE.RLength <> 0 Then
                strSQL.Append(strWHERE)
            End If


            '*履歴番号 000005 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                        "【実行メソッド名:GetDataSet】" + _
            '                        "【SQL内容:" + strSQL.ToString + "】")

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:GetDataSet】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "】")
            '*履歴番号 000005 2003/08/28 修正終了

            ' SQLの実行 DataSetの取得
            '*履歴番号 000016 2011/10/24 修正開始
            'csAtenaRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
            csAtenaRuisekiEntity = csDataSchema.Clone()
            csAtenaRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRuisekiEntity, ABAtenaRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '*履歴番号 000016 2011/10/24 修正終了


            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return csAtenaRuisekiEntity

    End Function


    '************************************************************************************************
    '* メソッド名     宛名履歴マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* 機能　　    　　宛名履歴マスタにデータを追加する
    '* 
    '* 引数           csDataRow As DataRow : 追加するデータの含まれるDataRowオブジェクト
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertAtenaRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csInstRow As DataRow
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim intInsCnt As Integer                            ' 追加件数
        Dim strUpdateDateTime As String

        Try

            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQLが作成されていなければ作成
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '更新日時の取得
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '作成日時

            '共通項目の編集を行う
            csDataRow(ABAtenaRuisekiEntity.TANMATSUID) = m_cfControlData.m_strClientId  ' 端末ＩＤ
            csDataRow(ABAtenaRuisekiEntity.SAKUJOFG) = "0"                              ' 削除フラグ
            csDataRow(ABAtenaRuisekiEntity.KOSHINCOUNTER) = Decimal.Zero                ' 更新カウンタ
            csDataRow(ABAtenaRuisekiEntity.SAKUSEINICHIJI) = strUpdateDateTime          ' 作成日時
            csDataRow(ABAtenaRuisekiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   ' 作成ユーザー
            csDataRow(ABAtenaRuisekiEntity.KOSHINNICHIJI) = strUpdateDateTime           ' 更新日時
            csDataRow(ABAtenaRuisekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId    ' 更新ユーザー

            '*履歴番号 000013 2004/11/12 修正開始
            '当クラスのデータ整合性チェックを行う
            'For Each csDataColumn In csDataRow.Table.Columns
            '    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString().Trim)
            'Next csDataColumn
            '*履歴番号 000016 2004/11/12 修正終了

            ' パラメータコレクションへ値の設定
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*履歴番号 000005 2003/08/28 修正開始
            '' RDBアクセスログ出力
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
            '                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
            '                        "【実行メソッド名:ExecuteSQL】" + _
            '                        "【SQL内容:" + m_strInsertSQL + "】")

            '' RDBアクセスログ出力（2024/04/18 DBアクセス速度改善のためコメントアウト）
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "【クラス名:" + Me.GetType.Name + "】" + _
            '                            "【メソッド名:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "】" + _
            '                            "【実行メソッド名:ExecuteSQL】" + _
            '                            "【SQL内容:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "】")
            '*履歴番号 000005 2003/08/28 修正終了

            ' SQLの実行
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return intInsCnt

    End Function
    '*履歴番号 000016 2011/10/24 追加開始
    '************************************************************************************************
    '* メソッド名     宛名累積マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaB() As Integer
    '* 
    '* 機能　　    　 宛名累積マスタにデータを追加する
    '* 
    '* 引数           csAtenaDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名累積）
    '* 　　           csAtenaFZYDr As DataRow : 追加するデータの含まれるDataRowオブジェクト（宛名累積付随）
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '宛名累積マスタ追加を実行
            intCnt = Me.InsertAtenaRB(csAtenaDr)

            '住基法改正以降のとき
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '宛名累積付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                If (IsNothing(m_csAtenaRuisekiFZYB)) Then
                    m_csAtenaRuisekiFZYB = New ABAtenaRuisekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '処理なし
                End If

                '作成日時、更新日時の同期
                csAtenaFZYDr(ABAtenaRuisekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI)
                csAtenaFZYDr(ABAtenaRuisekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI)

                '宛名累積付随マスタ追加を実行
                intCnt2 = m_csAtenaRuisekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr)
            Else
                '処理なし
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return intCnt

    End Function

    '*履歴番号 000017 2023/08/14 追加開始
    '************************************************************************************************
    '* メソッド名     宛名累積マスタ追加
    '* 
    '* 構文           Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
    '*                                              ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    '* 
    '* 機能　　    　 宛名累積マスタにデータを追加する
    '* 
    '* 引数           csAtenaDr As DataRow           : 追加するデータの含まれるDataRowオブジェクト（宛名累積）
    '*                csAtenaHyojunDr As DataRow     : 追加するデータの含まれるDataRowオブジェクト（宛名累積_標準）
    '* 　　           csAtenaFZYDr As DataRow        : 追加するデータの含まれるDataRowオブジェクト（宛名累積付随）
    '*                csAtenaFZYHyojunDr As DataRow  : 追加するデータの含まれるDataRowオブジェクト（宛名累積付随_標準）
    '* 
    '* 戻り値         Integer : 追加したデータの件数
    '************************************************************************************************
    Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
                                  ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0
        Dim intCnt3 As Integer = 0
        Dim intCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"

        Try

            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '宛名累積マスタ追加を実行
            intCnt = Me.InsertAtenaRB(csAtenaDr)

            If (Not IsNothing(csAtenaHyojunDr)) Then

                '宛名累積_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                If (IsNothing(m_csAtenaRuisekiHyojunB)) Then
                    m_csAtenaRuisekiHyojunB = New ABAtenaRuiseki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '処理なし
                End If

                '宛名累積標準の作成日時と更新日時に宛名累積Rowの作成日時と更新日時をセットする
                csAtenaHyojunDr(ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI)
                csAtenaHyojunDr(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI)

                '宛名累積_標準マスタ追加を実行
                intCnt2 = m_csAtenaRuisekiHyojunB.InsertAtenaRuisekiHyojunB(csAtenaHyojunDr)

            End If
            '住基法改正以降のとき
            If (m_blnJukihoKaiseiFG) Then

                '宛名累積付随Rowが存在する場合
                If (csAtenaFZYDr IsNot Nothing) Then

                    '宛名累積付随マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    If (IsNothing(m_csAtenaRuisekiFZYB)) Then
                        m_csAtenaRuisekiFZYB = New ABAtenaRuisekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '処理なし
                    End If

                    '作成日時、更新日時の同期
                    csAtenaFZYDr(ABAtenaRuisekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI)
                    csAtenaFZYDr(ABAtenaRuisekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI)

                    '宛名累積付随マスタ追加を実行
                    intCnt3 = m_csAtenaRuisekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr)

                End If

                '宛名累積付随_標準Rowが存在する場合
                If (csAtenaFZYHyojunDr IsNot Nothing) Then

                    '宛名累積付随_標準マスタBｸﾗｽのｲﾝｽﾀﾝｽ化
                    If (IsNothing(m_csAtenaRuisekiFZYHyojunB)) Then
                        m_csAtenaRuisekiFZYHyojunB = New ABAtenaRuisekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '処理なし
                    End If

                    '作成日時、更新日時の同期
                    csAtenaFZYHyojunDr(ABAtenaRuisekiFZYHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI)
                    csAtenaFZYHyojunDr(ABAtenaRuisekiFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI)

                    '宛名累積付随_標準マスタ追加を実行
                    intCnt4 = m_csAtenaRuisekiFZYHyojunB.InsertAtenaRuisekiFZYHyojunB(csAtenaFZYHyojunDr)

                End If

            Else
                '処理なし
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

        Return intCnt

    End Function
    '*履歴番号 000017 2023/08/14 追加終了

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

        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim csInsertColumn As StringBuilder                 'INSERT用カラム定義
        Dim csInsertParam As StringBuilder                  'INSERT用パラメータ定義


        Try
            ' デバッグ開始ログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL文の作成
            m_strInsertSQL = "INSERT INTO " + ABAtenaRuisekiEntity.TABLE_NAME + " "
            csInsertColumn = New StringBuilder()
            csInsertParam = New StringBuilder()

            ' INSERT パラメータコレクションクラスのインスタンス化
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()



            ' パラメータコレクションの作成
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL文の作成
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")

                csInsertParam.Append(ABAtenaRuisekiEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT コレクションにパラメータを追加
                cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)


            Next csDataColumn

            ' 最後のカンマを取り除いてINSERT文を作成
            m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + csInsertParam.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")"

            ' デバッグ終了ログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

    End Sub

    '*履歴番号 000016 2011/10/24 追加開始
    '************************************************************************************************
    '* メソッド名     宛名付随データ項目編集
    '* 
    '* 構文           Private SetFZYEntity()
    '* 
    '* 機能           宛名付随データの項目編集をします。
    '* 
    '* 引数           strAtenaSQLsb　：　宛名取得用SQL  
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub SetFZYEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TABLEINSERTKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.LINKNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUMINHYOJOTAIKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKYOCHITODOKEFLG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.HONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANAHONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANJIHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANJITSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KATAKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.UMAREFUSHOKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TSUSHOMEITOUROKUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUKIKANCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUKIKANMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUSHACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUSHAMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUCARDNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KOFUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KOFUYOTEISTYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KOFUYOTEIEDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.FRNSTAINUSMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.FRNSTAINUSKANAMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSKANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE5)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE6)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE7)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE8)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE9)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE10)

    End Sub
   '************************************************************************************************
    '* メソッド名     宛名付随テーブルJOIN句作成
    '* 
    '* 構文           Private SetFZYJoin()
    '* 
    '* 機能           宛名付随テーブルのJOIN句を作成します。
    '* 
    '* 引数           strAtenaSQLsb　：　宛名取得用SQL  
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub SetFZYJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaRuisekiFZYEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", _
                                    ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.JUMINCD, _
                                    ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUMINCD)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", _
                                    ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.RIREKINO, _
                                    ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RIREKINO)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", _
                                    ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.SHORINICHIJI, _
                                    ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.SHORINICHIJI)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", _
                                    ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.ZENGOKB, _
                                    ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZENGOKB)
    End Sub
    '*履歴番号 000016 2011/10/24 追加終了

    '************************************************************************************************
    '* メソッド名     データ整合性チェック
    '* 
    '* 構文           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
    '* 
    '* 機能           更新データの整合性をチェックする。
    '* 
    '* 引数           strColumnName As String : 宛名履歴マスタデータセットの項目名
    '*                strValue As String     : 項目に対応する値
    '* 
    '* 戻り値         なし
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)

        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Const TABLENAME As String = "宛名累積．"
        Dim objErrorStruct As UFErrorStruct                 'エラー定義構造体

        Try
            ' デバッグ開始ログ出力
            'm_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'")

            ' 日付クラスのインスタンス化
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' 日付クラスの必要な設定を行う
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()

                Case ABAtenaRuisekiEntity.JUMINCD            '住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHICHOSONCD        '市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KYUSHICHOSONCD     '旧市町村コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KYUSHICHOSONCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.RIREKINO           '履歴番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RIREKINO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                    '*履歴番号 000010 2003/12/01 修正開始
                    'Case ABAtenaRuisekiEntity.SYORINICHIJI      '処理日時
                Case ABAtenaRuisekiEntity.SHORINICHIJI      '処理日時
                    '*履歴番号 000010 2003/12/01 修正終了
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SYORINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaRuisekiEntity.ZENGOKB           '前後区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZENGOKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.RRKST_YMD          '履歴開始年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RRKST_YMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.RRKED_YMD          '履歴終了年月日
                    If Not (strValue = String.Empty Or strValue = "00000000" Or strValue = "99999999") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RRKED_YMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUMINJUTOGAIKB     '住民住登外区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINJUTOGAIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUMINYUSENIKB      '住民優先区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINYUSENIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUTOGAIYUSENKB     '住登外優先区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTOGAIYUSENKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ATENADATAKB        '宛名データ区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ATENADATAKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.STAICD             '世帯コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_STAICD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUMINHYOCD         '住民票コード
                    'チェックなし

                Case ABAtenaRuisekiEntity.SEIRINO            '整理番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEIRINO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ATENADATASHU       '宛名データ種別
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ATENADATASHU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HANYOKB1           '汎用区分1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HANYOKB1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KJNHJNKB           '個人法人区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KJNHJNKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HANYOKB2           '汎用区分2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HANYOKB2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANNAIKANGAIKB     '管内管外区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANNAIKANGAIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANAMEISHO1        'カナ名称1
                    '*履歴番号 000008 2003/10/30 修正開始
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*履歴番号 000008 2003/10/30 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANAMEISHO1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIMEISHO1       '漢字名称1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIMEISHO1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANAMEISHO2        'カナ名称2
                    '*履歴番号 000008 2003/10/30 修正開始
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*履歴番号 000008 2003/10/30 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANAMEISHO2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIMEISHO2       '漢字名称2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIMEISHO2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIHJNKEITAI     '漢字法人形態
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIHJNKEITAI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIHJNDAIHYOSHSHIMEI   '漢字法人代表者氏名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEARCHKANJIMEISHO  '検索用漢字名称
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEARCHKANJIMEISHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KYUSEI             '旧姓
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KYUSEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEARCHKANASEIMEI   '検索用カナ姓名
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ姓名", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEARCHKANASEI      '検索用カナ姓
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ姓", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEARCHKANAMEI      '検索用カナ名
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得(英数字・半角カナ項目入力の誤りです。：)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "検索用カナ名", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIRRKNO          '住基履歴番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIRRKNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                    'Case ABAtenaRuisekiEntity.UMAREYMD           '生年月日
                    '    If Not (strValue = String.Empty Or strValue = "00000000") Then
                    '        m_cfDateClass.p_strDateValue = strValue
                    '        If (Not m_cfDateClass.CheckDate()) Then
                    '            'エラー定義を取得
                    '            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_UMAREYMD)
                    '            '例外を生成
                    '            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '        End If
                    '    End If

                    'Case ABAtenaRuisekiEntity.UMAREWMD           '生和暦年月日
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        'エラー定義を取得(数字項目入力の誤りです。：)
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                    '        '例外を生成
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "生和暦年月日", objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaRuisekiEntity.SEIBETSUCD         '性別コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEIBETSUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEIBETSU           '性別
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEIBETSU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEKINO             '籍番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEKINO)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUMINHYOHYOJIJUN   '住民票表示順
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINHYOHYOJIJUN)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZOKUGARACD         '続柄コード
                    If (Not UFStringClass.CheckNumber(strValue.TrimEnd)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZOKUGARACD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZOKUGARA           '続柄
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZOKUGARA)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2JUMINHYOHYOJIJUN     '第２住民票表示順
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2JUMINHYOHYOJIJUN)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2ZOKUGARACD           '第２続柄コード
                    If (Not UFStringClass.CheckNumber(strValue.TrimEnd)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2ZOKUGARACD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2ZOKUGARA             '第２続柄
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2ZOKUGARA)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.STAINUSJUMINCD     '世帯主住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_STAINUSJUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.STAINUSMEI         '世帯主名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_STAINUSMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANASTAINUSMEI     'カナ世帯主名
                    '*履歴番号 000008 2003/10/30 修正開始
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*履歴番号 000008 2003/10/30 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANASTAINUSMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2STAINUSJUMINCD       '第２世帯主住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2STAINUSJUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2STAINUSMEI           '第２世帯主名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2STAINUSMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANADAI2STAINUSMEI       '第２カナ世帯主名
                    '*履歴番号 000008 2003/10/30 修正開始
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*履歴番号 000008 2003/10/30 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANADAI2STAINUSMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.YUBINNO            '郵便番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_YUBINNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUSHOCD            '住所コード
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUSHOCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUSHO              '住所
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BANCHICD1          '番地コード1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHICD1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BANCHICD2          '番地コード2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHICD2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BANCHICD3          '番地コード3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHICD3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BANCHI             '番地
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KATAGAKIFG         '方書フラグ
                    If (Not strValue.Trim = String.Empty) Then
                        If (Not UFStringClass.CheckNumber(strValue)) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KATAGAKIFG)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.KATAGAKICD         '方書コード
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KATAGAKICD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KATAGAKI           '方書
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KATAGAKI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.RENRAKUSAKI1       '連絡先1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RENRAKUSAKI1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.RENRAKUSAKI2       '連絡先2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RENRAKUSAKI2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HON_ZJUSHOCD       '本籍全国住所コード
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HON_ZJUSHOCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HON_JUSHO          '本籍住所
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HON_JUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HONSEKIBANCHI      '本籍番地
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HONSEKIBANCHI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HITTOSH            '筆頭者
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HITTOSH)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CKINIDOYMD         '直近異動年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINIDOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.CKINJIYUCD         '直近事由コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINJIYUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CKINJIYU           '直近事由
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINJIYU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CKINTDKDYMD        '直近届出年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINTDKDYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.CKINTDKDTUCIKB     '直近届出通知区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINTDKDTUCIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TOROKUIDOYMD       '登録異動年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUIDOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TOROKUIDOWMD       '登録異動和暦年月日
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUIDOWMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TOROKUJIYUCD       '登録事由コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUJIYUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TOROKUJIYU         '登録事由
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUJIYU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TOROKUTDKDYMD      '登録届出年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUTDKDYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TOROKUTDKDWMD      '登録届出和暦年月日
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUTDKDWMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TOROKUTDKDTUCIKB   '登録届出通知区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUTDKDTUCIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUTEIIDOYMD        '住定異動年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIIDOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUTEIIDOWMD        '住定異動和暦年月日
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIIDOWMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUTEIJIYUCD        '住定事由コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIJIYUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUTEIJIYU          '住定事由
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIJIYU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUTEITDKDYMD       '住定届出年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEITDKDYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUTEITDKDWMD       '住定届出和暦年月日
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEITDKDWMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUTEITDKDTUCIKB    '住定届出通知区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEITDKDTUCIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHOJOIDOYMD        '消除異動年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOIDOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.SHOJOJIYUCD        '消除事由コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOJIYUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHOJOJIYU          '消除事由
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOJIYU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHOJOTDKDYMD       '消除届出年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOTDKDYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.SHOJOTDKDTUCIKB    '消除届出通知区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOTDKDTUCIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIIDOYMD     '転出予定届出年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIIDOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIIDOYMD      '転出確定届出年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIIDOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTITSUCHIYMD   '転出確定通知年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTITSUCHIYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUNYURIYUCD       '転出入理由コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUNYURIYUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUNYURIYU         '転出入理由
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUNYURIYU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_YUBINNO         '転入前住所郵便番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_YUBINNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_ZJUSHOCD        '転入前住所全国住所コード
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_ZJUSHOCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_JUSHO           '転入前住所住所
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_JUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_BANCHI          '転入前住所番地
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_BANCHI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_KATAGAKI        '転入前住所方書
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_KATAGAKI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_STAINUSMEI      '転入前住所世帯主名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_STAINUSMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIYUBINNO    '転出予定郵便番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIYUBINNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIZJUSHOCD   '転出予定全国住所コード
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIZJUSHOCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIJUSHO      '転出予定住所
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIJUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIBANCHI     '転出予定番地
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIBANCHI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIKATAGAKI   '転出予定方書
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIKATAGAKI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEISTAINUSMEI '転出予定世帯主名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEISTAINUSMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIYUBINNO     '転出確定郵便番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIYUBINNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIZJUSHOCD    '転出確定全国住所コード
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIZJUSHOCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIJUSHO     '転出確定住所
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIJUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIBANCHI      '転出確定番地
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIBANCHI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIKATAGAKI    '転出確定方書
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIKATAGAKI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTISTAINUSMEI  '転出確定世帯主名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTISTAINUSMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIMITDKFG     '転出確定未届フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIMITDKFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BIKOYMD                  '備考年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKOYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.BIKO                     '備考
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BIKOTENSHUTSUKKTIJUSHOFG '備考転出確定住所フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKOTENSHUTSUKKTIJUSHOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HANNO                    '版番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HANNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KAISEIATOFG              '改製後フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAISEIATOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KAISEIMAEFG             '改製前フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAISEIMAEFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KAISEIYMD                '改製年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAISEIYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.GYOSEIKUCD               '行政区コード
                    '* 履歴番号 000014 2005/12/26 修正開始
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* 履歴番号 000014 2005/12/26 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_GYOSEIKUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.GYOSEIKUMEI              '行政区名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_GYOSEIKUMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUCD1                 '地区コード1
                    '*履歴番号 00012 2004/08/13 修正開始
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*履歴番号 00012 2004/08/13 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUCD1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUMEI1                '地区名1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUMEI1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUCD2                 '地区コード2
                    '*履歴番号 00012 2004/08/13 修正開始
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*履歴番号 00012 2004/08/13 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUCD2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUMEI2                '地区名2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUMEI2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUCD3                 '地区コード3
                    '*履歴番号 00012 2004/08/13 修正開始
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*履歴番号 00012 2004/08/13 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUCD3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUMEI3                '地区名3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUMEI3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TOHYOKUCD                '投票区コード
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOHYOKUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHOGAKKOKUCD             '小学校区コード
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOGAKKOKUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHUGAKKOKUCD             '中学校区コード
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHUGAKKOKUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HOGOSHAJUMINCD           '保護者住民コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HOGOSHAJUMINCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIHOGOSHAMEI          '漢字保護者名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIHOGOSHAMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANAHOGOSHAMEI           'カナ保護者名
                    '*履歴番号 000008 2003/10/30 修正開始
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*履歴番号 000008 2003/10/30 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANAHOGOSHAMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KIKAYMD                  '帰化年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KIKAYMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.KARIIDOKB                '仮異動区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KARIIDOKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHORITEISHIKB            '処理停止区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHORITEISHIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIYUBINNO              '住基郵便番号
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIYUBINNO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHORIYOKUSHIKB           '処理抑止区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHORIYOKUSHIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIJUSHOCD              '住基住所コード
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIJUSHOCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIJUSHO                '住基住所
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIJUSHO)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIBANCHICD1            '住基番地コード1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHICD1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIBANCHICD2            '住基番地コード2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHICD2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIBANCHICD3            '住基番地コード3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHICD3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIBANCHI               '住基番地
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIKATAGAKIFG           '住基方書フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIKATAGAKIFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIKATAGAKICD           '住基方書コード
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIKATAGAKICD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIKATAGAKI             '住基方書
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIKATAGAKI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIGYOSEIKUCD           '住基行政区コード
                    '* 履歴番号 000014 2005/12/26 修正開始
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* 履歴番号 000014 2005/12/26 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIGYOSEIKUCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIGYOSEIKUMEI          '住基行政区名
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIGYOSEIKUMEI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUCD1                    '住基地区コード1
                    '*履歴番号 00012 2004/08/13 修正開始
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*履歴番号 00012 2004/08/13 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUCD1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUMEI1            '住基地区名1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUMEI1)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUCD2             '住基地区コード2
                    '*履歴番号 00012 2004/08/13 修正開始
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*履歴番号 00012 2004/08/13 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUCD2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUMEI2            '住基地区名2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUMEI2)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUCD3             '住基地区コード3
                    '*履歴番号 00012 2004/08/13 修正開始
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*履歴番号 00012 2004/08/13 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUCD3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUMEI3            '住基地区名3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUMEI3)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KAOKUSHIKIKB             '家屋敷区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAOKUSHIKIKB)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BIKOZEIMOKU              '備考税目
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKOZEIMOKU)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOKUSEKICD               '国籍コード
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOKUSEKICD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOKUSEKI                 '国籍
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOKUSEKI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYUSKAKCD             '在留資格コード
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYUSKAKCD)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYUSKAK               '在留資格
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYUSKAK)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYUKIKAN              '在留期間
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYUKIKAN)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYU_ST_YMD            '在留開始年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYU_ST_YMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYU_ED_YMD            '在留終了年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYU_ED_YMD)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                    '*履歴番号 000009 2003/11/18 追加開始
                Case ABAtenaRuisekiEntity.KSNENKNNO, _
                        ABAtenaRuisekiEntity.JKYNENKNKIGO1, _
                        ABAtenaRuisekiEntity.JKYNENKNNO1, _
                        ABAtenaRuisekiEntity.JKYNENKNEDABAN1, _
                        ABAtenaRuisekiEntity.JKYNENKNKB1, _
                        ABAtenaRuisekiEntity.JKYNENKNKIGO2, _
                        ABAtenaRuisekiEntity.JKYNENKNNO2, _
                        ABAtenaRuisekiEntity.JKYNENKNEDABAN2, _
                        ABAtenaRuisekiEntity.JKYNENKNKB2, _
                        ABAtenaRuisekiEntity.JKYNENKNKIGO3, _
                        ABAtenaRuisekiEntity.JKYNENKNNO3, _
                        ABAtenaRuisekiEntity.JKYNENKNEDABAN3, _
                        ABAtenaRuisekiEntity.JKYNENKNKB3, _
                        ABAtenaRuisekiEntity.KOKUHOSHIKAKUKB
                    ' 基礎年金番号
                    ' 受給年金記号１
                    ' 受給年金番号１
                    ' 受給年金枝番１
                    ' 受給年金区分１
                    ' 受給年金記号２
                    ' 受給年金番号２
                    ' 受給年金枝番２
                    ' 受給年金区分２
                    ' 受給年金記号３
                    ' 受給年金番号３
                    ' 受給年金枝番３
                    ' 受給年金区分３
                    ' 国保資格区分
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.NENKNSKAKSHUTKYMD, _
                        ABAtenaRuisekiEntity.NENKNSKAKSSHTSYMD, _
                        ABAtenaRuisekiEntity.KOKUHOSHUTOKUYMD, _
                        ABAtenaRuisekiEntity.KOKUHOSOSHITSUYMD, _
                        ABAtenaRuisekiEntity.KOKUHOTISHKGAITOYMD, _
                        ABAtenaRuisekiEntity.KOKUHOTISHKHIGAITOYMD
                    ' 年金資格取得年月日
                    ' 年金資格喪失年月日
                    ' 国保取得年月日
                    ' 国保喪失年月日
                    ' 国保退職該当年月日
                    ' 国保退職非該当年月日
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            'エラー定義を取得
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002019)
                            '例外を生成
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.NENKNSKAKSHUTKSHU, _
                        ABAtenaRuisekiEntity.NENKNSKAKSHUTKRIYUCD, _
                        ABAtenaRuisekiEntity.NENKNSKAKSSHTSRIYUCD, _
                        ABAtenaRuisekiEntity.JKYNENKNSHU1, _
                        ABAtenaRuisekiEntity.JKYNENKNSHU2, _
                        ABAtenaRuisekiEntity.JKYNENKNSHU3, _
                        ABAtenaRuisekiEntity.KOKUHONO, _
                        ABAtenaRuisekiEntity.KOKUHOGAKUENKB, _
                        ABAtenaRuisekiEntity.KOKUHOTISHKKB, _
                        ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKB
                    ' 年金資格取得種別
                    ' 年金資格取得理由コード
                    ' 年金資格喪失理由コード
                    ' 受給年金種別１
                    ' 受給年金種別２
                    ' 受給年金種別３
                    ' 国保番号
                    ' 国保学遠区分
                    ' 国保退職区分
                    ' 国保退職本被区分
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode)
                    End If

                    '*履歴番号 000010 2003/12/01 修正開始
                    'Case ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBMEISHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBRYAKUSHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOGAKUENKBMEISHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOGAKUENKBRYAKUSHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOTISHKKBMEISHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOTISHKKBRYAKUSHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOTIAHKHONHIKBMEISHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBRYAKUSHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOHOKENSHOKIGO, _
                    '        ABAtenaRuisekiEntity.KOKUHOHOKENSHONO
                Case ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBMEISHO, _
                  ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBRYAKUSHO, _
                  ABAtenaRuisekiEntity.KOKUHOGAKUENKBMEISHO, _
                  ABAtenaRuisekiEntity.KOKUHOGAKUENKBRYAKUSHO, _
                  ABAtenaRuisekiEntity.KOKUHOTISHKKBMEISHO, _
                  ABAtenaRuisekiEntity.KOKUHOTISHKKBRYAKUSHO, _
                  ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBMEISHO, _
                  ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBRYAKUSHO, _
                  ABAtenaRuisekiEntity.KOKUHOHOKENSHOKIGO ', _
                    '*履歴番号 000011 2004/03/06 修正開始
                    'ABAtenaRuisekiEntity.KOKUHOHOKENSHONO
                    '*履歴番号 000011 2004/03/06 修正開始
                    '*履歴番号 000010 2003/12/01 修正終了
                    ' 国保資格区分正式名称
                    ' 国保資格区分略式名称
                    ' 国保学遠区分正式名称
                    ' 国保学遠区分略式名称
                    ' 国保退職区分正式名称
                    ' 国保退職区分略式名称
                    ' 国保退職本被区分正式名称
                    ' 国保退職本被区分略式名称
                    ' 国保保険証記号
                    ' 国保保険証番号
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002011)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode)
                    End If

                    '*履歴番号 000009 2003/11/18 追加終了

                Case ABAtenaRuisekiEntity.RESERCE                  'リザーブ
                    'チェックなし

                Case ABAtenaRuisekiEntity.TANMATSUID               '端末ＩＤ
                    '* 履歴番号 000006 2003/09/11 修正開始
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000006 2003/09/11 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TANMATSUID)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SAKUJOFG                 '削除フラグ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SAKUJOFG)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOSHINCOUNTER            '更新カウンタ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOSHINCOUNTER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SAKUSEINICHIJI           '作成日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SAKUSEINICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SAKUSEIUSER              '作成ユーザ
                    '* 履歴番号 000007 2003/10/09 修正開始
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000007 2003/10/09 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SAKUSEIUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOSHINNICHIJI            '更新日時
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOSHINNICHIJI)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOSHINUSER               '更新ユーザ
                    '* 履歴番号 000007 2003/10/09 修正開始
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* 履歴番号 000007 2003/10/09 修正終了
                        'エラー定義を取得
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOSHINUSER)
                        '例外を生成
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

            End Select

            ' デバッグ終了ログ出力
            'm_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' エラーをそのままスローする
            Throw objExp
        End Try

    End Sub

    '*履歴番号 000016 2011/10/24 追加開始
    '************************************************************************************************
    '* メソッド名       住基法改正ﾌﾗｸﾞ取得
    '* 
    '* 構文             Private Function GetJukihoKaiseiFG()
    '* 
    '* 機能　　    　   管理情報を取得する
    '* 
    '* 引数             なし
    '* 
    '* 戻り値           なし
    '************************************************************************************************
    Private Sub GetJukihoKaiseiFG()
        Const THIS_METHOD_NAME As String = "GetJukihoKaiseiFG"
        Try
            ' デバッグログ出力
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (IsNothing(m_csSekoYMDHanteiB)) Then
                '施行日判定Ｂｸﾗｽのｲﾝｽﾀﾝｽ化
                m_csSekoYMDHanteiB = New ABSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '住基法改正ﾌﾗｸﾞ＝施行日判定結果
                m_blnJukihoKaiseiFG = m_csSekoYMDHanteiB.CheckAfterSekoYMD
            Else
                '処理なし
            End If

            ' デバッグログ出力
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppExceptionをキャッチ
            ' ワーニングログ出力
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【ワーニングコード:" + objAppExp.p_strErrorCode + "】" + _
                                        "【ワーニング内容:" + objAppExp.Message + "】")
            ' エラーをそのままスローする
            Throw objAppExp

        Catch objExp As Exception
            ' エラーログ出力
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "【クラス名:" + THIS_CLASS_NAME + "】" + _
                                        "【メソッド名:" + THIS_METHOD_NAME + "】" + _
                                        "【エラー内容:" + objExp.Message + "】")
            ' システムエラーをスローする
            Throw objExp

        End Try
    End Sub
    '*履歴番号 000016 2011/10/24 追加終了

#End Region

End Class
