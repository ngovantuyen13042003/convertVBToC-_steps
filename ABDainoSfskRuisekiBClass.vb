'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �`�a��[���t��ٓ��ݐσ}�X�^�c�`
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t��           2007/08/10
'*
'* �쐬��           ��Á@�v��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'*  �C�������@ ����ԍ��@�@�C�����e
'* 2010/02/26   000001     ���t��f�[�^�X�V�̏ꍇ�A��[���t��ݐσ}�X�^:��[�敪�ɢ40����Z�b�g����悤���C�i��Áj
'* 2010/04/16   000002     VS2008�Ή��i��Áj
'* 2023/10/25   000003    �yAB-0840-1�z���t��Ǘ����ڒǉ��i����j
'* 2023/12/05   000004    �yAB-0840-1�z���t��Ǘ����ڒǉ�_�ǉ��C���i�����j
'* 2024/03/07   000005    �yAB-0900-1�z�A�h���X�E�x�[�X�E���W�X�g���Ή�(����)
'* 2024/06/10   000006    �yAB-9902-1�z�s��Ή� 
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Data
Imports System.Text
'*����ԍ� 000003 2023/10/25 �ǉ��J�n
Imports System.CodeDom
Imports System.Web.UI.WebControls
Imports Densan.Reams.UR.UR002BB
Imports Densan.Reams.UR.UR002BX
'*����ԍ� 000003 2023/10/25 �ǉ��I��

'************************************************************************************************
'*
'* ��[���t��ٓ��ݐσ}�X�^�擾�A�X�V���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABDainoSfskRuisekiBClass

#Region "�����o�ϐ�"
    '�p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_strInsertSQL As String                        ' INSERT�pSQL
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X
    Private m_csDataSchma As DataSet                        ' �X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT�p�p�����[�^�R���N�V����
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT�p�p�����[�^�R���N�V����

    '�@�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABDainoSfskRuisekiBClass"            ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h
    Private Const STRING_D As String = "D"                                          ' ��[
    Private Const string_S As String = "S"                                          ' ���t��
    '*����ԍ� 000003 2023/10/25 �ǉ��J�n
    Private Const ZENGOKB_ZEN As String = "1"                                       '�O��敪�@�O
    Private Const ZENGOKB_GO As String = "2"                                        '�O��敪�@��
    Private Const SOUFU_TSUIKA As String = "S0"                                     '�����敪�@���t_�ǉ�
    Private Const SOUFU_SHUSEI As String = "S1"                                     '�����敪�@���t_�C��
    Private Const SOUFU_SAKUJO As String = "S2"                                     '�����敪�@���t_�폜
    Private Const DAINO_TSUIKA As String = "D0"                                     '�����敪�@��[_�ǉ�
    Private Const DAINO_SHUSEI As String = "D1"                                     '�����敪�@��[_�C��
    Private Const DAINO_SAKUJO As String = "D2"                                     '�����敪�@��[_�폜
    Private Const SAKUJO_ON As String = "1"                                         '�폜�t���O
    '*����ԍ� 000003 2023/10/25 �ǉ��I��
#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass, 
    '*                               ByVal cfUFRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfUFControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfUFConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                cfUFRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass)
        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' �p�����[�^�̃����o�ϐ�
        m_strInsertSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing

        ' AB��[���t��ݐσ}�X�^�̃X�L�[�}�擾
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABDainoSfskRuisekiEntity.TABLE_NAME, ABDainoSfskRuisekiEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "���\�b�h"

#Region "��[���t��ٓ��ݐσ}�X�^���o"
    ' �g�p���Ă��Ȃ����A������̂Ŏc���Ă���
    '''''************************************************************************************************
    '''''* ���\�b�h��     ��[���t��ٓ��ݐσ}�X�^���o
    '''''* 
    '''''* �\��           Public Overloads Function GetDainoSfsk(ByVal strJuminCD As String) As DataSet
    '''''* 
    '''''* �@�\�@�@    �@ ��[���t��ٓ��ݐσ}�X�^���f�[�^�𒊏o����
    '''''* 
    '''''* ����           strJuminCD        : �Z���R�[�h
    '''''* 
    '''''* �߂�l         DataSet : �擾������[���t��ٓ��ݐσ}�X�^�̊Y���f�[�^
    '''''************************************************************************************************
    ''''Public Overloads Function GetDainoSfsk(ByVal strJuminCD As String) As DataSet
    ''''    Const THIS_METHOD_NAME As String = "GetDainoSfsk"
    ''''    Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
    ''''    Dim cfUFParameterClass As UFParameterClass          ' �p�����[�^�N���X
    ''''    Dim csDainoSfskEntity As DataSet                    ' ��[���t��ݐ�DataSet
    ''''    Dim strSQL As StringBuilder
    ''''    Dim strWHERE As StringBuilder

    ''''    Try
    ''''        ' �f�o�b�O�J�n���O�o��
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''        ' �p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

    ''''        ' SQL���̍쐬
    ''''        strSQL = New StringBuilder
    ''''        strSQL.Append("SELECT * FROM ")
    ''''        strSQL.Append(ABDainoSfskRuisekiEntity.TABLE_NAME)
    ''''        strSQL.Append(" WHERE ")

    ''''        'WHERE��̍쐬
    ''''        strWHERE = New StringBuilder
    ''''        '�Z���R�[�h
    ''''        If Not (strJuminCD = String.Empty) Then
    ''''            strWHERE.Append(ABDainoSfskRuisekiEntity.JUMINCD)
    ''''            strWHERE.Append(" = ")
    ''''            strWHERE.Append(ABDainoSfskRuisekiEntity.KEY_JUMINCD)
    ''''            ' ���������̃p�����[�^���쐬
    ''''            cfUFParameterClass = New UFParameterClass
    ''''            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_JUMINCD
    ''''            cfUFParameterClass.Value = strJuminCD
    ''''            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
    ''''            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        End If

    ''''        'ORDER�������
    ''''        If (strWHERE.Length <> 0) Then
    ''''            strSQL.Append(strWHERE)
    ''''            strSQL.Append(" ORDER BY ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.SHORINICHIJI)
    ''''            strSQL.Append(" , ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.ZENGOKB)
    ''''        Else
    ''''            strSQL.Append(" ORDER BY ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.JUMINCD)
    ''''            strSQL.Append(", ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.SHORINICHIJI)
    ''''            strSQL.Append(", ")
    ''''            strSQL.Append(ABDainoSfskRuisekiEntity.ZENGOKB)
    ''''        End If

    ''''        ' RDB�A�N�Z�X���O�o��
    ''''        m_cfLogClass.RdbWrite(m_cfControlData, _
    ''''                                    "�y�N���X��:" + Me.GetType.Name + "�z" + _
    ''''                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
    ''''                                    "�y���s���\�b�h��:GetDataSet�z" + _
    ''''                                    "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "�z")

    ''''        ' SQL�̎��s DataSet�̎擾
    ''''        csDainoSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoSfskRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)


    ''''        ' �f�o�b�O�I�����O�o��
    ''''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''    Catch objAppExp As UFAppException
    ''''        ' ���[�j���O���O�o��
    ''''        m_cfLogClass.WarningWrite(m_cfControlData, _
    ''''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''''                                    "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
    ''''                                    "�y���[�j���O���e:" + objAppExp.Message + "�z")
    ''''        ' �G���[�����̂܂܃X���[����
    ''''        Throw

    ''''    Catch objExp As Exception
    ''''        ' �G���[���O�o��
    ''''        m_cfLogClass.ErrorWrite(m_cfControlData, _
    ''''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''''                                    "�y�G���[���e:" + objExp.Message + "�z")
    ''''        ' �G���[�����̂܂܃X���[����
    ''''        Throw
    ''''    End Try

    ''''    Return csDainoSfskEntity

    ''''End Function
#End Region

#Region "��[���t��ٓ��ݐσ}�X�^�ǉ�"
    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ٓ��ݐσ}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     �@��[���t��ٓ��ݐσ}�X�^�Ƀf�[�^��ǉ�
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertDainoSfskB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csInstRow As DataRow
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim intInsCnt As Integer                            ' �ǉ�����
        Dim strUpdateDateTime As String

        Try

            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '�X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '�쐬����

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABDainoSfskRuisekiEntity.TANMATSUID) = m_cfControlData.m_strClientId  ' �[���h�c
            'csDataRow(ABDainoSfskRuisekiEntity.SAKUJOFG) = "0"                              ' �폜�t���O
            csDataRow(ABDainoSfskRuisekiEntity.KOSHINCOUNTER) = Decimal.Zero                ' �X�V�J�E���^
            csDataRow(ABDainoSfskRuisekiEntity.SAKUSEINICHIJI) = strUpdateDateTime          ' �쐬����
            csDataRow(ABDainoSfskRuisekiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   ' �쐬���[�U�[
            csDataRow(ABDainoSfskRuisekiEntity.KOSHINNICHIJI) = strUpdateDateTime           ' �X�V����
            csDataRow(ABDainoSfskRuisekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId    ' �X�V���[�U�[

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoSfskRuisekiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:ExecuteSQL�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' �f�o�b�O�I�����O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw
        End Try

        Return intInsCnt

    End Function
#End Region

#Region "SQL���쐬"
    '************************************************************************************************
    '* ���\�b�h��     SQL���̍쐬
    '* 
    '* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\�@�@    �@ INSERT��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)

        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim csInsertColumn As StringBuilder                 'INSERT�p�J������`
        Dim csInsertParam As StringBuilder                  'INSERT�p�p�����[�^��`


        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABDainoSfskRuisekiEntity.TABLE_NAME + " "
            csInsertColumn = New StringBuilder
            csInsertParam = New StringBuilder

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass


            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")

                csInsertParam.Append(ABDainoSfskRuisekiEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)


            Next csDataColumn

            ' �Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + csInsertParam.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")"

            ' �f�o�b�O�I�����O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw
        End Try

    End Sub
#End Region

#Region "��[���t��ݐσf�[�^�쐬"
    '*����ԍ� 000003 2023/10/25 �C���J�n
    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐσf�[�^�쐬
    '* 
    '* �\��           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String) As Integer
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐσf�[�^���쐬����
    '* 
    '* ����           csDataRow As DataRow      : ��[���t��f�[�^
    '*                strShoriKB As String      : �����敪
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String) As Integer
        Dim strShoriNichiji As String = String.Empty

        Return CreateDainoSfskData(csDataRow, strShoriKB, Nothing, strShoriNichiji)

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐσf�[�^�쐬
    '* 
    '* �\��           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String, _
    '                                                     ByRef strShoriNichiji As String) As Integer
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐσf�[�^���쐬����
    '* 
    '* ����           csDataRow As DataRow      : ��[���t��f�[�^
    '*                strShoriKB As String      : �����敪
    '*                strShoriNichiji As String : ��������
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String, ByRef strShoriNichiji As String) As Integer

        Return CreateDainoSfskData(csDataRow, strShoriKB, Nothing, strShoriNichiji)

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐσf�[�^�쐬
    '* 
    '* �\��           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String, _
    '*                                                    ByVal csABSfskHyojunDataRow As DataRow, _
    '*                                                    ByRef strShoriNichiji As String) As Integer
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐσf�[�^���쐬����
    '* 
    '* ����           csDataRow As DataRow                : ��[���t��f�[�^
    '*                strShoriKB As String                : �����敪
    '*                csABSfskHyojunDataRow As DataRow    : AB���t��_�W���f�[�^�iDataRow�`���j
    '*                strShoriNichiji As String           : ��������
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************

    'Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String) As Integer
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String,
                                        ByVal csABSfskHyojunDataRow As DataRow, ByRef strShorinichiji As String) As Integer
        '*����ԍ� 000003 2023/10/25 �C���I��
        Const THIS_METHOD_NAME As String = "CreateDainoSfskData"
        Dim csDataSet As DataSet
        Dim csRuisekiDR As DataRow
        Dim csDataColumn As DataColumn
        Dim strSystemDate As String                 ' �V�X�e�����t
        Dim intInsCnt As Integer
        'Dim csDainoSfskRows() As DataRow
        'Dim csDainoSfskRow As DataRow
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csNewDainosfskRow As DataRow
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim csOriginalDR As DataRow
        'Dim csDainoSfskEntity As DataSet
        Dim intUpdataCount_zen As Integer
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")
            strShorinichiji = strSystemDate
            ' �X�L�[�}���擾
            csDataSet = m_csDataSchma.Clone

            '***
            '* ��[���t��ݐ�(�O)�ҏW����
            '*
            If (strShoriKB <> ABConstClass.DAINO_ADD AndAlso strShoriKB <> ABConstClass.SFSK_ADD) Then
                ' �����敪���ǉ��ȊO�̏ꍇ
                If (csDataRow.HasVersion(DataRowVersion.Original)) Then
                    ' �C���O��񂪎c���Ă���ꍇ

                    ' ��[���t��ݐσf�[�^���쐬
                    csOriginalDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow

                    For Each csDataColumn In csDataRow.Table.Columns
                        If Not (csDataColumn.ColumnName = ABDainoEntity.RESERVE) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskDataEntity.SFSKDATAKB) Then
                            csOriginalDR(csDataColumn.ColumnName) = csDataRow(csDataColumn.ColumnName, DataRowVersion.Original)
                        End If
                    Next

                    csOriginalDR(ABDainoSfskRuisekiEntity.SHORINICHIJI) = strSystemDate
                    csOriginalDR(ABDainoSfskRuisekiEntity.SHORIKB) = strShoriKB               ' �����敪
                    csOriginalDR(ABDainoSfskRuisekiEntity.ZENGOKB) = "1"                      ' �O��敪

                    '*����ԍ� 000001 2010/02/26 �C���J�n
                    ' -- �R�����g�C�� --
                    ''''' ���t��f�[�^�̏ꍇ�A���t��敪���[�敪�ɃZ�b�g����
                    ' ���t��f�[�^�̏ꍇ�A��[�敪�ɢ40����Z�b�g����B���t��f�[�^�͢40��Œ�̂��߁B
                    ' -- �R�����g�C�� --
                    If (strShoriKB.RSubstring(0, 1) = "S") Then
                        'csOriginalDR(ABDainoSfskRuisekiEntity.DAINOKB) = csDataRow(ABSfskEntity.SFSKDATAKB)
                        csOriginalDR(ABDainoSfskRuisekiEntity.DAINOKB) = "40"

                        '*����ԍ� 000003 2023/10/25 �ǉ��J�n
                        If ((Not IsNothing(csABSfskHyojunDataRow)) AndAlso (csABSfskHyojunDataRow.HasVersion(DataRowVersion.Original))) Then
                            ' ���t��_�W����Nothing�ȊO�ł��A�C���O��񂪎c���Ă���ꍇ
                            '���t��Ԓn�R�[�h�P
                            csOriginalDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD1, DataRowVersion.Original)
                            '���t��Ԓn�R�[�h�Q
                            csOriginalDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD2, DataRowVersion.Original)
                            '���t��Ԓn�R�[�h�R
                            csOriginalDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD3, DataRowVersion.Original)
                            '���t������R�[�h
                            csOriginalDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKATAGAKICD, DataRowVersion.Original)
                        End If
                        '*����ԍ� 000003 2023/10/25 �ǉ��I��

                    Else
                    End If
                    '*����ԍ� 000001 2010/02/26 �C���I��

                    ' �f�[�^�Z�b�g�ɏC���O����ǉ�
                    csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows.Add(csOriginalDR)

                    ' ��[���t��ݐ�(�O)�}�X�^�ǉ�����
                    intUpdataCount_zen = Me.InsertDainoSfskB(csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows(0))

                    ' �X�V�������P���ȊO�̏ꍇ�A�G���[�𔭐�������
                    If Not (intUpdataCount_zen = 1) Then
                        m_cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F��[���t��ݐρj
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��[���t��ݐ�", objErrorStruct.m_strErrorCode)
                    End If

                    ' �f�[�^�Z�b�g�̃N���A
                    csDataSet.Clear()
                Else

                End If
            Else

            End If


            '***
            '* ��[���t��ݐ�(��)�ҏW����
            '*
            ' ��[���t��ݐσf�[�^���쐬
            csRuisekiDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow

            For Each csDataColumn In csDataRow.Table.Columns
                If Not (csDataColumn.ColumnName = ABDainoEntity.RESERVE) AndAlso
                    Not (csDataColumn.ColumnName = ABSfskDataEntity.SFSKDATAKB) Then
                    csRuisekiDR(csDataColumn.ColumnName) = csDataRow(csDataColumn.ColumnName)
                End If
            Next

            ' ���ʍ��ڂ̃f�[�^�Z�b�g
            csRuisekiDR(ABDainoSfskRuisekiEntity.SHORINICHIJI) = strSystemDate              ' ��������
            csRuisekiDR(ABDainoSfskRuisekiEntity.SHORIKB) = strShoriKB                      ' �����敪
            csRuisekiDR(ABDainoSfskRuisekiEntity.ZENGOKB) = "2"                             ' �O��敪
            csRuisekiDR(ABDainoSfskRuisekiEntity.RESERVE1) = String.Empty                   ' ���U�[�u1
            csRuisekiDR(ABDainoSfskRuisekiEntity.RESERVE2) = String.Empty                   ' ���U�[�u2

            '*����ԍ� 000003 2023/10/25 �ǉ��J�n
            '��[�A���t��̏����敪���폜�̏ꍇ�A�폜�t���O�𗧂Ă�
            If (strShoriKB = ABConstClass.DAINO_DELETE OrElse strShoriKB = ABConstClass.SFSK_DELETE) Then
                csRuisekiDR(ABDainoSfskRuisekiEntity.SAKUJOFG) = SAKUJO_ON                  ' �폜�t���O

            End If
            '*����ԍ� 000003 2023/10/25 �ǉ��I��

            ' ��[�f�[�^�A���t��f�[�^�ʏ����̏ꍇ
            'If (CStr(csDataRow(ABDainoSfskRuisekiEntity.DAINOKB)) <> "40") Then
            If (strShoriKB.RSubstring(0, 1) = "D") Then
                ' ��[�f�[�^�̏ꍇ
                ' ��[�敪��"40"�ȊO�̏ꍇ
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB) = String.Empty     ' ���t��Ǔ��ǊO�敪
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKANAMEISHO) = String.Empty         ' ���t��J�i����
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKANJIMEISHO) = String.Empty        ' ���t�抿������
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKYUBINNO) = String.Empty            ' ���t��X�֔ԍ�
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKZJUSHOCD) = String.Empty           ' ���t��Z���R�[�h
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKJUSHO) = String.Empty              ' ���t��Z��
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) = String.Empty          ' ���t��Ԓn�R�[�h1
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) = String.Empty          ' ���t��Ԓn�R�[�h2
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) = String.Empty          ' ���t��Ԓn�R�[�h3
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHI) = String.Empty             ' ���t��Ԓn
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) = String.Empty         ' ���t������R�[�h
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKI) = String.Empty           ' ���t�����
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1) = String.Empty       ' ���t��A����1
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2) = String.Empty       ' ���t��A����2
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKGYOSEIKUCD) = String.Empty         ' ���t��s����R�[�h
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKGYOSEIKUMEI) = String.Empty        ' ���t��s���於
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUCD1) = String.Empty           ' ���t��n��R�[�h1
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI1) = String.Empty          ' ���t��n�於1
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUCD2) = String.Empty           ' ���t��n��R�[�h2
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI2) = String.Empty          ' ���t��n�於2
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUCD3) = String.Empty           ' ���t��n��R�[�h3
                csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI3) = String.Empty          ' ���t��n�於3
            Else
                ' ���t��f�[�^�̏ꍇ
                ' ��[�敪��"40"�̏ꍇ
                '*����ԍ� 000001 2010/02/26 �C���J�n
                '**�R�����g �F ���t��f�[�^�̏ꍇ�A��[�敪�ɢ40����Z�b�g�B���t��f�[�^�͢40��Œ�̂��߁B
                'csRuisekiDR(ABDainoSfskRuisekiEntity.DAINOKB) = csDataRow(ABSfskEntity.SFSKDATAKB)
                csRuisekiDR(ABDainoSfskRuisekiEntity.DAINOKB) = "40"
                '*����ԍ� 000001 2010/02/26 �C���I��
                '*����ԍ� 000003 2023/10/25 �C���J�n
                'csRuisekiDR(ABDainoSfskRuisekiEntity.DAINOJUMINCD) = String.Empty
                'csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) = String.Empty          ' ���t��Ԓn�R�[�h1
                'csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) = String.Empty          ' ���t��Ԓn�R�[�h2
                'csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) = String.Empty          ' ���t��Ԓn�R�[�h3
                'csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) = String.Empty         ' ���t������R�[�h
                If (Not IsNothing(csABSfskHyojunDataRow)) Then
                    ' ���t��_�W����Nothing�ȊO�̏ꍇ
                    '���t��Ԓn�R�[�h�P
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD1)
                    '���t��Ԓn�R�[�h�Q
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD2)
                    '���t��Ԓn�R�[�h�R
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKBANCHICD3)
                    '���t������R�[�h
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) =
                                csABSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKATAGAKICD)
                Else
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD1) = String.Empty          ' ���t��Ԓn�R�[�h1
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD2) = String.Empty          ' ���t��Ԓn�R�[�h2
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKBANCHICD3) = String.Empty          ' ���t��Ԓn�R�[�h3
                    csRuisekiDR(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD) = String.Empty         ' ���t������R�[�h

                End If
                '*����ԍ� 000003 2023/10/25 �C���I��
            End If

            csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows.Add(csRuisekiDR)

            '***
            '* ��[���t��ݐ�(��)�}�X�^�ǉ�����
            '*
            intInsCnt = InsertDainoSfskB(csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows(0))

            ' �f�o�b�O�I�����O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw
        End Try

        Return intInsCnt

    End Function
#End Region

    '*����ԍ� 000003 2023/10/25 �ǉ��J�n
#Region "��[���t��ݐσf�[�^���o"
    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐσf�[�^���o
    '* 
    '* �\��           PPublic Function GetABDainoSfskRuisekiData(ByVal strJuminCD As String,
    '*                                                           ByVal strGyomuCD As String,
    '*                                                           ByVal strGyomuNaiShubetsuCD As String,
    '*                                                           ByVal intTorokuRenban As Integer,
    '*                                                           ByVal strShoriKB As String) As DataRow()
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐσ}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD             : �Z���R�[�h 
    '*                strGyomuCD             : �Ɩ��R�[�h
    '*                strGyomuNaiShubetsuCD  : �Ɩ�����ʃR�[�h
    '*                intTorokuRenban        : �o�^�ԍ�
    '*                strShoriKB             : �����敪�@"D"�F��[�A"S"�F���t
    '* 
    '* �߂�l         DataSet : �擾������[���t��ݐσ}�X�^�̊Y���f�[�^(DataRow())
    '************************************************************************************************
    Public Function GetABDainoSfskRuisekiData(ByVal strJuminCD As String,
                                              ByVal strGyomuCD As String,
                                              ByVal strGyomuNaiShubetsuCD As String,
                                              ByVal intTorokuRenban As Integer,
                                              ByVal strShoriKB As String) As DataRow()

        Const THIS_METHOD_NAME As String = "GetABDainoSfskRuisekiData"
        Dim csDainoSfskRuisekiEntity As DataSet
        Dim csReturnDataRows As DataRow()
        Dim strSQL As New StringBuilder

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABDainoSfskRuisekiEntity.TABLE_NAME)
            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoSfskRuisekiEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            strSQL.Append(Me.CreateWhere(strJuminCD, strGyomuCD, strGyomuNaiShubetsuCD, intTorokuRenban.ToString(), strShoriKB, THIS_METHOD_NAME))

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
                                            strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDainoSfskRuisekiEntity = m_csDataSchma.Clone()
            csDainoSfskRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoSfskRuisekiEntity,
                                                    ABDainoSfskRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '�߂�l�p�Ƀf�[�^���i�[
            strSQL.Clear()
            strSQL.Append(ABDainoSfskRuisekiEntity.JUMINCD)
            strSQL.Append(" = '")
            strSQL.Append(strJuminCD)
            strSQL.Append("'")
            csReturnDataRows = csDainoSfskRuisekiEntity.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Select(strSQL.ToString)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csReturnDataRows

    End Function

    '************************************************************************************************
    '* ���\�b�h��     SELECT��̍쐬
    '* 
    '* �\��           Private Sub CreateSelect() As String
    '* 
    '* �@�\           SELECT��𐶐�����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         String    :   SELECT��
    '************************************************************************************************
    Private Function CreateSelect() As String
        Const THIS_METHOD_NAME As String = "CreateSelect"
        Dim strSELECT As New StringBuilder

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT��̍쐬
            strSELECT.AppendFormat("SELECT {0}", ABDainoSfskRuisekiEntity.JUMINCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SHICHOSONCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KYUSHICHOSONCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SHORINICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SHORIKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.ZENGOKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.GYOMUCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.TOROKURENBAN)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.STYMD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.EDYMD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.RRKNO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.DAINOKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.DAINOJUMINCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKANAMEISHO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKANJIMEISHO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKYUBINNO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKZJUSHOCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKJUSHO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHICD1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHICD2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHICD3)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKBANCHI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKATAGAKICD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKKATAGAKI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKGYOSEIKUCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKGYOSEIKUMEI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUCD1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUMEI1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUCD2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUMEI2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUCD3)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SFSKCHIKUMEI3)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.RESERVE1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.RESERVE2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.TANMATSUID)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SAKUJOFG)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KOSHINCOUNTER)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SAKUSEINICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.SAKUSEIUSER)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KOSHINNICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiEntity.KOSHINUSER)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return strSELECT.ToString

    End Function

    '************************************************************************************************
    '* ���\�b�h��     WHERE���̍쐬
    '* 
    '* �\��           Private Function CreateWhere(ByVal strJuminCD As String,
    '*                                             ByVal strGyomuCD As String,
    '*                                             ByVal strGyomuNaiShubetsuCD As String,
    '*                                             ByVal strTorokuRenban As String,
    '*                                             ByVal strShoriKB As String,
    '*                                             ByVal strMethodName As String) As String
    '* 
    '* �@�\�@�@    �@ WHERE�����쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           strJuminCD             : �Z���R�[�h 
    '*                strGyomuCD             : �Ɩ��R�[�h
    '*                strGyomuNaiShubetsuCD  : �Ɩ�����ʃR�[�h
    '*                strTorokuRenban        : �o�^�A��
    '*                strShoriKB             : �����敪�@"D"�F��[�A"S"�F���t
    '*                strMethodName          : �ďo�����֐���
    '*
    '* �߂�l         String    :   WHERE��
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String,
                                 ByVal strGyomuCD As String,
                                 ByVal strGyomuNaiShubetsuCD As String,
                                 ByVal strTorokuRenban As String,
                                 ByVal strShoriKB As String,
                                 ByVal strMethodName As String) As String

        Const THIS_METHOD_NAME As String = "CreateWhere"
        Const GET_MAX_TOROKURENBAN As String = "GetMaxTorokuRenban"

        Dim strWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT�p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' WHERE��̍쐬
            strWHERE = New StringBuilder(256)

            ' �Z���R�[�h
            strWHERE.AppendFormat("WHERE {0} = {1}", ABDainoSfskRuisekiEntity.JUMINCD, ABDainoSfskRuisekiEntity.KEY_JUMINCD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �Ɩ��R�[�h
            strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.GYOMUCD, ABDainoSfskRuisekiEntity.KEY_GYOMUCD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �Ɩ�����ʃR�[�h
            strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD, ABDainoSfskRuisekiEntity.KEY_GYOMUNAISHU_CD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomuNaiShubetsuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �o�^�A��
            If (Not (strTorokuRenban = String.Empty)) Then
                strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.TOROKURENBAN, ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN
                cfUFParameterClass.Value = strTorokuRenban
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�����敪
            Select Case strShoriKB
                Case string_S
                    '���t
                    strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB,
                                         ABConstClass.SFSK_ADD, ABConstClass.SFSK_SHUSEI, ABConstClass.SFSK_DELETE)

                Case STRING_D
                    '��[
                    '*����ԍ� 000004 2023/12/05 �C���J�n
                    'strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB,
                    '                     ABConstClass.DAINO_ADD, ABConstClass.DAINO_SHUSEI, ABConstClass.DAINO_SHUSEI)
                    strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB,
                                         ABConstClass.DAINO_ADD, ABConstClass.DAINO_SHUSEI, ABConstClass.DAINO_DELETE)
                    '*����ԍ� 000004 2023/12/05 �C���I��

            End Select

            '�O��敪
            strWHERE.AppendFormat(" AND {0} = '{1}'", ABDainoSfskRuisekiEntity.ZENGOKB, ZENGOKB_GO)

            '����ԍ��@�~�ԂŃ\�[�g�@
            If (strMethodName <> GET_MAX_TOROKURENBAN) Then
                strWHERE.AppendFormat(" ORDER BY {0} DESC", ABDainoSfskRuisekiEntity.RRKNO)
            End If


            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return strWHERE.ToString

    End Function
#End Region

#Region "�o�^�A�ԍő�l�擾����"
    '************************************************************************************************
    '* ���\�b�h��     �o�^�A�ԍő�l�擾����
    '* 
    '* �\��           Public Function GetMaxTorokuRenban(ByVal strJuminCD As String,
    '*                                                    ByVal strGyomuCD As String,
    '*                                                    ByVal strGyomuNaiShubetsuCD As String,
    '*                                                    ByVal strShoriKB As String) As Integer
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐσ}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD             : �Z���R�[�h 
    '*                strGyomuCD             : �Ɩ��R�[�h
    '*                strGyomuNaiShubetsuCD  : �Ɩ�����ʃR�[�h
    '*                strShoriKB             : �����敪�@"D"�F��[�A"S"�F���t
    '* 
    '* �߂�l         Integer : �擾�����o�^�A�Ԃ̍ő�
    '************************************************************************************************
    Public Function GetMaxTorokuRenban(ByVal strJuminCD As String,
                                       ByVal strGyomuCD As String,
                                       ByVal strGyomuNaiShubetsuCD As String,
                                       ByVal strShoriKB As String) As Integer

        Const THIS_METHOD_NAME As String = "GetMaxTorokuRenban"
        Dim csDainoSfskRuisekiEntity As DataSet
        Dim intMaxTorokuRenban As Integer = 0
        Dim strSQL As New StringBuilder()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT��̐���
            strSQL.AppendFormat("SELECT MAX({0}) AS MAXTOROKURENBAN ", ABDainoSfskRuisekiEntity.TOROKURENBAN)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABDainoSfskRuisekiEntity.TABLE_NAME)
            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoSfskRuisekiEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            strSQL.Append(Me.CreateWhere(strJuminCD, strGyomuCD, strGyomuNaiShubetsuCD, String.Empty, strShoriKB, THIS_METHOD_NAME))

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
                                            strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDainoSfskRuisekiEntity = m_csDataSchma.Clone()
            csDainoSfskRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoSfskRuisekiEntity,
                                                    Nothing, m_cfSelectUFParameterCollectionClass, False)

            If (0 < csDainoSfskRuisekiEntity.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).Rows.Count) Then
                '�f�[�^������ꍇ�͖߂�l�Ɋi�[����
                If (IsNumeric(csDainoSfskRuisekiEntity.Tables(0).Rows(0).Item(0))) Then
                    intMaxTorokuRenban = CInt(csDainoSfskRuisekiEntity.Tables(0).Rows(0).Item(0))
                Else
                    '�f�[�^�������ꍇ��0��߂�l�ɃZ�b�g
                    intMaxTorokuRenban = 0
                End If

            End If
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return intMaxTorokuRenban

    End Function
#End Region


#Region "��[���t��ݐσf�[�^���G���e�B�e�B�Ɋi�[����"

    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐςƔ��l�̃f�[�^���G���e�B�e�B�Ɋi�[����
    '* 
    '* �\��        Public Function SetDainoSfsfRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet,
    '*                                                    ByVal strShoriKB As String) As DataSet
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐσ}�X�^���Y���f�[�^���i�[����
    '* 
    '* ����           csDainoSfskRuisekiDataset As DataSet   �F��[���t��ݐσf�[�^�Z�b�g
    '*                strShoriKB As String                   �F�����敪�@"D"�F��[�A"S"�F���t��
    '* 
    '* �߂�l         DataSet : ��[�����ꗗ�\���p�̃f�[�^(DataSet)
    '************************************************************************************************
    Public Function SetDainoSfsfRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet, ByVal strShoriKB As String) As DataSet
        Const SHORIKB_SFSK As String = "S"
        Const SHORIKB_DAINO As String = "D"

        Dim csReturnDataset As DataSet

        If (strShoriKB = SHORIKB_SFSK) Then
            csReturnDataset = SetSfskRirekiData(csDainoSfskRuisekiDataset, strShoriKB)
        ElseIf (strShoriKB = SHORIKB_DAINO) Then
            csReturnDataset = SetDainoRirekiData(csDainoSfskRuisekiDataset, strShoriKB)
        End If

        Return csReturnDataset
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐςƔ��l�̃f�[�^���G���e�B�e�B�Ɋi�[����
    '* 
    '* �\��           Public Function SetSfskRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet,
    '*                                                  ByVal strShoriKB As String) As DataSet
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐσ}�X�^���Y���f�[�^���i�[����
    '* 
    '* ����           csDainoSfskRuisekiDataset As DataSet   �F��[���t��ݐσf�[�^�Z�b�g
    '*                strShoriKB As String                   �F�����敪�@"D"�F��[�A"S"�F���t��
    '* 
    '* �߂�l         DataSet : ��[�����ꗗ�\���p�̃f�[�^(DataSet)
    '************************************************************************************************
    Public Function SetSfskRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet, ByVal strShoriKB As String) As DataSet
        '�萔
        Const ALL9_YMD As String = "99999999"               '�N�����I�[���X
        Const SFSK As String = "���t��"                      '���t�敶��

        Dim csReturnDataset As DataSet
        Dim csDataRow As DataRow
        Dim csDataNewRow As DataRow
        Dim csDataColumn As DataColumn

        Dim blnIsDainoSfskBiko As Boolean = False
        Dim csBikoDataSet As DataSet
        '*����ԍ� 000004 2023/12/05 �C���J�n
        'Dim blnSakujoFG As Boolean = False
        Dim blnSakujoFG As Boolean = True
        '*����ԍ� 000004 2023/12/05 �C���I��

        Dim cGyomuCDMstB As URGyomuCDMstBClass              '�Ɩ��R�[�h�}�X�^�c�`
        Dim csGyomuCDMstEntity As DataSet                   '�Ɩ��R�[�h�}�X�^DataSet
        Dim cfDate As UFDateClass                           '���t�N���X
        Dim cDainoKankeiB As ABDainoKankeiBClass            '��[�֌W�擾�N���X
        Dim cAtenaGetB As ABAtenaGetBClass                  '�����擾�N���X
        Dim cAtenaHenshuB As ABAtenaHenshuBClass            '�����ҏW�a
        Dim cJuminShubetsuB As ABJuminShubetsuBClass        '�Z����ʖ��̎擾�N���X
        Dim cKannaiKangaiKBB As ABKannaiKangaiKBBClass      '�Ǔ��ǊO���̎擾�N���X
        Dim cABBikoB As ABBikoBClass

        Dim csDataTable As DataTable
        Dim cDainoSfskRuisekiB As ABDainoSfskRuisekiBClass               ' ��[���t��ݐςc�`�r�W�l�X�N���X
        Dim cDainoSfskRuisekiHyojunB As ABDainoSfskRuiseki_HyojunBClass  ' ��[���t��ݐ�_�W���c�`�r�W�l�X�N���X
        Dim csSfskRirekiDataRows As DataRow()
        Dim csSfskRirekiHyojunDataRow As DataRow
        Dim csSfskRirekiHyojunDataTable As New DataTable

        '�f�[�^���o�p�ϐ�
        Dim strJuminCd As String
        Dim strGyomuCD As String
        Dim strGyomuNaiShuCD As String
        Dim intTorokuRenban As Integer
        '*����ԍ� 000004 2023/12/05 �ǉ��J�n
        Dim strKannaiKangaiCD As String
        Dim strKannaiKangaiMeisho As String
        '*����ԍ� 000004 2023/12/05 �ǉ��I��

        Try

            Dim csDataRows As DataRow()

            csDataRows = csDainoSfskRuisekiDataset.Tables(ABSfskDataEntity.TABLE_NAME).Select(
                                                                    String.Format("{0} = 'True'", ABSfskDataEntity.CHECK))

            strJuminCd = csDataRows(0).Item(ABSfskDataEntity.JUMINCD).ToString
            strGyomuCD = csDataRows(0).Item(ABSfskDataEntity.GYOMUCD).ToString
            strGyomuNaiShuCD = csDataRows(0).Item(ABSfskDataEntity.GYOMUNAISHUCD).ToString
            intTorokuRenban = CInt(csDataRows(0).Item(ABSfskDataEntity.TOROKURENBAN))

            '��[���t��ݐσf�[�^�̎擾
            ' ��[���t��ݐςc�`�N���X�̃C���X�^���X��
            cDainoSfskRuisekiB = New ABDainoSfskRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            csSfskRirekiDataRows = cDainoSfskRuisekiB.GetABDainoSfskRuisekiData(strJuminCd,
                                                  strGyomuCD, strGyomuNaiShuCD, intTorokuRenban, strShoriKB)
            '��[���t��ݐ�_�W���f�[�^�̎擾
            cDainoSfskRuisekiHyojunB = New ABDainoSfskRuiseki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' �f�[�^�Z�b�g�C���X�^���X��
            csReturnDataset = New DataSet

            ' �e�[�u���Z�b�g�̎擾
            csDataTable = Me.CreateColumnsABSfskRirekiData()

            ' �f�[�^�Z�b�g�Ƀe�[�u���Z�b�g�̒ǉ�
            csReturnDataset.Tables.Add(csDataTable)

            ' ���t�N���X�̃C���X�^���X��
            cfDate = New UFDateClass(m_cfConfigDataClass)

            ' ��[�֌W�擾�C���X�^���X��
            cDainoKankeiB = New ABDainoKankeiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' �Ɩ��R�[�h�}�X�^�c�`�̃C���X�^���X�쐬
            cGyomuCDMstB = New URGyomuCDMstBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' �����ҏW�a�̃C���X�^���X�쐬
            cAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' �����擾�N���X�C���X�^���X��
            cAtenaGetB = New ABAtenaGetBClass(m_cfControlData, m_cfConfigDataClass)

            ' �Z����ʃN���X�C���X�^���X��
            cJuminShubetsuB = New ABJuminShubetsuBClass(m_cfControlData, m_cfConfigDataClass)

            ' �Ǔ��ǊO�N���X�C���X�^���X��
            cKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfControlData, m_cfConfigDataClass)

            ' ���l�N���X�̃C���X�^���X��
            cABBikoB = New ABBikoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            For Each csDataRow In csSfskRirekiDataRows

                csDataNewRow = csReturnDataset.Tables(ABSfskDataEntity.TABLE_NAME).NewRow

                ' �����l�̐ݒ�
                For Each csDataColumn In csDataNewRow.Table.Columns
                    If (csDataColumn.ColumnName = ABSfskDataEntity.KOSHINCOUNTER) Then
                        csDataNewRow(csDataColumn.ColumnName) = Decimal.Zero
                    Else
                        csDataNewRow(csDataColumn.ColumnName) = String.Empty
                    End If
                Next csDataColumn

                ' �Z���R�[�h
                csDataNewRow(ABSfskDataEntity.JUMINCD) = csDataRow(ABDainoSfskRuisekiEntity.JUMINCD)
                ' �s�����R�[�h
                csDataNewRow(ABSfskDataEntity.SHICHOSONCD) = csDataRow(ABDainoSfskRuisekiEntity.SHICHOSONCD)
                ' ���s�����R�[�h
                csDataNewRow(ABSfskDataEntity.KYUSHICHOSONCD) = csDataRow(ABDainoSfskRuisekiEntity.KYUSHICHOSONCD)
                ' �Ɩ��R�[�h
                csDataNewRow(ABSfskDataEntity.GYOMUCD) = csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD)

                ' �Ɩ��R�[�h�}�X�^���擾����
                strGyomuCD = CType(csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD), String)
                csGyomuCDMstEntity = cGyomuCDMstB.GetGyomuCDHoshu(strGyomuCD)

                If (csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows.Count = 0) Then
                    ' �Ɩ�����
                    csDataNewRow(ABSfskDataEntity.GYOMUMEISHO) = String.Empty
                    ' �Ɩ����̗�
                    csDataNewRow(ABSfskDataEntity.GYOMUMEISHORYAKU) = String.Empty
                Else
                    ' �Ɩ�����
                    csDataNewRow(ABSfskDataEntity.GYOMUMEISHO) = csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows(0).Item(URGyomuCDMstEntity.GYOMUMEI)
                    ' �Ɩ����̗�
                    csDataNewRow(ABSfskDataEntity.GYOMUMEISHORYAKU) = csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows(0).Item(URGyomuCDMstEntity.GYOMURYAKUSHO)
                End If

                ' �Ɩ�����ʃR�[�h
                csDataNewRow(ABSfskDataEntity.GYOMUNAISHUCD) = csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD)
                ' ��[�Z���R�[�h
                csDataNewRow(ABSfskDataEntity.DAINOJUMINCD) = csDataRow(ABDainoSfskRuisekiEntity.DAINOJUMINCD)
                ' �J�n�N��
                csDataNewRow(ABSfskDataEntity.STYMD) = csDataRow(ABDainoSfskRuisekiEntity.STYMD)
                ' �I���N��
                csDataNewRow(ABSfskDataEntity.EDYMD) = csDataRow(ABDainoSfskRuisekiEntity.EDYMD)

                ' �\���p�J�n�N��
                cfDate.p_strDateValue = CType(csDataRow(ABDainoSfskRuisekiEntity.STYMD), String)
                cfDate.p_enEraType = UFEraType.KanjiRyaku
                cfDate.p_enDateSeparator = UFDateSeparator.Period
                csDataNewRow(ABSfskDataEntity.DISP_STYMD) = cfDate.p_strWarekiYMD

                ' �\���p�I���N���i999999�̎��́A��\���j
                If (CType(csDataRow(ABDainoSfskRuisekiEntity.EDYMD), String) = ALL9_YMD) Then
                    csDataNewRow(ABSfskDataEntity.DISP_EDYMD) = String.Empty
                Else
                    cfDate.p_strDateValue = CType(csDataRow(ABDainoSfskRuisekiEntity.EDYMD), String)
                    csDataNewRow(ABSfskDataEntity.DISP_EDYMD) = cfDate.p_strWarekiYMD
                End If

                ' ���t��J�i����
                csDataNewRow(ABSfskDataEntity.SFSKKANAMEISHO) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKANAMEISHO)
                ' ���t�抿������
                csDataNewRow(ABSfskDataEntity.SFSKKANJIMEISHO) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKANJIMEISHO)

                ' ���t��Ǔ��ǊO�敪
                csDataNewRow(ABSfskDataEntity.SFSKKANNAiKANGAIKB) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB)
                '*����ԍ� 000004 2023/12/05 �ǉ��J�n
                ' �Ǔ��ǊO���̃L�[�Z�b�g
                strKannaiKangaiCD = CType(csDataRow(ABDainoSfskRuisekiEntity.SFSKKANNAIKANGAIKB), String)
                ' �Ǔ��ǊO���̎擾���]�b�g���s
                strKannaiKangaiMeisho = cKannaiKangaiKBB.GetKannaiKangai(strKannaiKangaiCD)
                ' �Ǔ��ǊO����
                csDataNewRow(ABSfskDataEntity.SFSKKANNAIKANGAIMEI) = strKannaiKangaiMeisho
                '*����ԍ� 000004 2023/12/05 �ǉ��I��
                ' ���t��X�֔ԍ�
                csDataNewRow(ABSfskDataEntity.SFSKYUBINNO) = csDataRow(ABDainoSfskRuisekiEntity.SFSKYUBINNO)
                ' ���t��Z���R�[�h
                csDataNewRow(ABSfskDataEntity.SFSKZJUSHOCD) = csDataRow(ABDainoSfskRuisekiEntity.SFSKZJUSHOCD)
                ' ���t��Z��
                csDataNewRow(ABSfskDataEntity.SFSKJUSHO) = csDataRow(ABDainoSfskRuisekiEntity.SFSKJUSHO)
                ' ���t��Ԓn
                csDataNewRow(ABSfskDataEntity.SFSKBANCHI) = csDataRow(ABDainoSfskRuisekiEntity.SFSKBANCHI)
                ' ���t��Ԓn�R�[�h1
                csDataNewRow(ABSfskDataEntity.BANCHICD1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKBANCHICD1)
                ' ���t��Ԓn�R�[�h2
                csDataNewRow(ABSfskDataEntity.BANCHICD2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKBANCHICD2)
                ' ���t��Ԓn�R�[�h3
                csDataNewRow(ABSfskDataEntity.BANCHICD3) = csDataRow(ABDainoSfskRuisekiEntity.SFSKBANCHICD3)
                ' ���t�����
                csDataNewRow(ABSfskDataEntity.SFSKKATAGAKI) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKATAGAKI)
                ' ���t��A����P
                csDataNewRow(ABSfskDataEntity.SFSKRENRAKUSAKI1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1)
                ' ���t��A����Q
                csDataNewRow(ABSfskDataEntity.SFSKRENRAKUSAKI2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2)
                ' �����R�[�h
                csDataNewRow.Item(ABSfskDataEntity.SFSKKATAGAKICD) = csDataRow(ABDainoSfskRuisekiEntity.SFSKKATAGAKICD)
                ' ���t��s����R�[�h
                csDataNewRow(ABSfskDataEntity.SFSKGYOSEIKUCD) = csDataRow(ABDainoSfskRuisekiEntity.SFSKGYOSEIKUCD)
                ' ���t��s���於
                ' �s����b�c�ɐ����ȊO�̂��̂��������Ă���ꍇ�͂��̂܂܍s���於�̂��Z�b�g
                csDataNewRow(ABSfskDataEntity.SFSKGYOSEIKUMEI) = csDataRow(ABDainoSfskRuisekiEntity.SFSKGYOSEIKUMEI)
                ' ���t��n��R�[�h�P
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUCD1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUCD1)
                ' ���t��n�於�P
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUMEI1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI1)
                ' ���t��n��R�[�h�Q
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUCD2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUCD2)
                ' ���t��n�於�Q
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUMEI2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI2)
                ' ���t��n��R�[�h�R
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUCD3) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUCD3)
                ' ���t��n�於�R
                csDataNewRow(ABSfskDataEntity.SFSKCHIKUMEI3) = csDataRow(ABDainoSfskRuisekiEntity.SFSKCHIKUMEI3)
                ' ���t��A����P
                csDataNewRow(ABSfskDataEntity.SFSKRENRAKUSAKI1) = csDataRow(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI1)
                ' ���t��A����Q
                csDataNewRow(ABSfskDataEntity.SFSKRENRAKUSAKI2) = csDataRow(ABDainoSfskRuisekiEntity.SFSKRENRAKUSAKI2)


                csSfskRirekiHyojunDataTable = cDainoSfskRuisekiHyojunB.GetABDainoSfskRuisekiData(strJuminCd,
                                                                                             strGyomuCD, strGyomuNaiShuCD, intTorokuRenban, strShoriKB)
                csSfskRirekiHyojunDataRow = csSfskRirekiHyojunDataTable.Select(String.Format("{0}='{1}'",
                                                                                ABDainoSfskRuisekiHyojunEntity.RRKNO,
                                                                                csDataRow(ABDainoSfskRuisekiEntity.RRKNO).ToString))(0)

                ' ���l�}�X�^���擾
                csBikoDataSet = cABBikoB.SelectByKey(
                                        ABBikoEntity.DEFAULT.BIKOKBN.SFSK,
                                        csDataRow(ABDainoSfskRuisekiEntity.JUMINCD).ToString(),
                                        csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD).ToString(),
                                        csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD).ToString(),
                                        csDataRow(ABDainoSfskRuisekiEntity.TOROKURENBAN).ToString(),
                                        csDataRow(ABDainoSfskRuisekiEntity.RRKNO).ToString(),
                                        blnSakujoFG)

                If (csBikoDataSet IsNot Nothing _
                        AndAlso 0 < csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows.Count) Then
                    ' �Z���R�[�h
                    csDataNewRow(ABSfskDataEntity.DAINOJUMINCD) = csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows(0).Item(ABBikoEntity.RESERVE)
                    csDataNewRow(ABSfskDataEntity.BIKO) = csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows(0).Item(ABBikoEntity.BIKO)
                Else
                    csDataNewRow(ABSfskDataEntity.BIKO) = String.Empty
                End If

                csDataNewRow(ABSfskDataEntity.CHECK) = False
                csDataNewRow(ABSfskDataEntity.JOTAI) = ABDainoSfskShoriMode.Empty.GetHashCode.ToString
                csDataNewRow(ABSfskDataEntity.DISP_JOTAI) = String.Empty
                csDataNewRow(ABSfskDataEntity.SEIGYOKB) = String.Empty

                csDataNewRow(ABSfskDataEntity.TOROKURENBAN) = csDataRow(ABDainoSfskRuisekiEntity.TOROKURENBAN)     '�o�^�A��
                csDataNewRow(ABSfskDataEntity.RRKNO) = csDataRow(ABDainoSfskRuisekiEntity.RRKNO)                   '����ԍ�
                csDataNewRow(ABSfskDataEntity.SHIKUCHOSONCD) = String.Empty                                        '�s�撬���R�[�g
                csDataNewRow(ABSfskDataEntity.MACHIAZACD) = String.Empty                                           '�����R�[�h
                csDataNewRow(ABSfskDataEntity.TODOFUKEN) = String.Empty                                            '�s���{��
                csDataNewRow(ABSfskDataEntity.SHIKUCHOSON) = String.Empty
                csDataNewRow(ABSfskDataEntity.MACHIAZA) = String.Empty

                '���t��敪
                csDataNewRow(ABSfskDataEntity.SFSKKBN) = csSfskRirekiHyojunDataRow.Item(ABDainoSfskRuisekiHyojunEntity.SFSKKBN).ToString()

                csDataNewRow(ABSfskDataEntity.DISP_DAINOKB) = SFSK

                ' �폜�t���O
                csDataNewRow(ABSfskDataEntity.SAKUJOFG) = csDataRow(ABDainoSfskRuisekiEntity.SAKUJOFG)

                ' �X�V���[�U
                csDataNewRow(ABSfskDataEntity.KOSHINUSER) = csDataRow(ABDainoSfskRuisekiEntity.KOSHINUSER)
                ' �X�V�J�E���^
                csDataNewRow(ABSfskDataEntity.KOSHINCOUNTER) = csDataRow(ABDainoSfskRuisekiEntity.KOSHINCOUNTER)

                csReturnDataset.Tables(ABSfskDataEntity.TABLE_NAME).Rows.Add(csDataNewRow)

            Next csDataRow
            csReturnDataset.AcceptChanges()


        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                    "�y���\�b�h��:" + Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                    "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                    "�y���[�j���O���e:" + cfAppExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                    "�y���\�b�h��:" + Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                    "�y�G���[���e:" + csExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        End Try

        Return csReturnDataset

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐςƔ��l�̃f�[�^���G���e�B�e�B�Ɋi�[����
    '* 
    '* �\��        Public Function SetDainoRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet,
    '*                                                    ByVal strShoriKB As String) As DataSet
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐσ}�X�^���Y���f�[�^���i�[����
    '* 
    '* ����           csDainoSfskRuisekiDataset As DataSet   �F��[���t��ݐσf�[�^�Z�b�g
    '*                strShoriKB As String                  : �����敪�@"D"�F��[�A"S"�F���t��
    '* 
    '* �߂�l         DataSet : ��[�����ꗗ�\���p�̃f�[�^(DataSet)
    '************************************************************************************************
    Public Function SetDainoRirekiData(ByVal csDainoSfskRuisekiDataset As DataSet, ByVal strShoriKB As String) As DataSet
        '�萔
        Const ALL9_YMD As String = "99999999"               '�N�����I�[���X
        Const JUSHOHENSHU1_PARA_ONE As String = "1"         '���ҏW1�@�p�����[�^��1
        Const GET_HONNINDATA As String = "1"                '�{�l�f�[�^�擾
        Const DATAKB_HOJIN As String = "20"                 '�f�[�^�敪�@�@�l
        Const DATASHU_FRN As String = "2"                   '�f�[�^��@�O���l

        Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����

        Dim csReturnDataset As DataSet
        Dim csDataRow As DataRow
        Dim csDataNewRow As DataRow
        Dim csDataColumn As DataColumn
        Dim csDainoKankeiDataSet As DataSet
        Dim csAtenaDataSet As DataSet
        Dim csAtenaRow As DataRow

        Dim strDainoKB As String
        Dim intRowCount As Integer
        Dim strDataKB As String
        Dim strDataShu As String
        Dim strMeisho As String
        Dim strKannaiKangaiCD As String
        Dim strKannaiKangaiMeisho As String
        Dim strKanjiShimei As String                        '��������
        Dim strKanaShimei As String                         '�J�i����
        Dim strYubinNO As String                            '�X�֔ԍ�
        Dim csBikoDataSet As DataSet
        '*����ԍ� 000004 2023/12/05 �C���J�n
        'Dim blnSakujoFG As Boolean = False
        Dim blnSakujoFG As Boolean = True
        '*����ԍ� 000004 2023/12/05 �C���I��

        Dim cGyomuCDMstB As URGyomuCDMstBClass              '�Ɩ��R�[�h�}�X�^�c�`
        Dim csGyomuCDMstEntity As DataSet                   '�Ɩ��R�[�h�}�X�^DataSet
        Dim cfDate As UFDateClass                           '���t�N���X
        Dim cDainoKankeiB As ABDainoKankeiBClass            '��[�֌W�擾�N���X
        Dim cAtenaGetB As ABAtenaGetBClass                  '�����擾�N���X
        Dim cAtenaGetPara1X As ABAtenaGetPara1XClass        '�����擾�p�����[�^�N���X
        Dim cAtenaHenshuB As ABAtenaHenshuBClass            '�����ҏW�a
        Dim csAtena1Entity As DataSet                       '�����f�[�^Entity
        Dim cJuminShubetsuB As ABJuminShubetsuBClass        '�Z����ʖ��̎擾�N���X
        Dim cKannaiKangaiKBB As ABKannaiKangaiKBBClass      '�Ǔ��ǊO���̎擾�N���X
        Dim cABBikoB As ABBikoBClass

        Dim csDataTable As DataTable
        Dim csDainoSfskRuisekiB As ABDainoSfskRuisekiBClass ' ��[���t��ݐςc�`�r�W�l�X�N���X
        Dim csDainoRirekiDataRows As DataRow()

        '�f�[�^���o�p�ϐ�
        Dim strJuminCd As String
        Dim strGyomuCD As String
        Dim strGyomuNaiShuCD As String
        Dim intTorokuRenban As Integer

        Try

            Dim csDataRows As DataRow()
            csDataRows = csDainoSfskRuisekiDataset.Tables(ABDainoDataEntity.TABLE_NAME).Select(
                                                                    String.Format("{0} = 'True'", ABDainoDataEntity.CHECK))

            strJuminCd = csDataRows(0).Item(ABDainoDataEntity.JUMINCD).ToString
            strGyomuCD = csDataRows(0).Item(ABDainoDataEntity.GYOMUCD).ToString
            strGyomuNaiShuCD = csDataRows(0).Item(ABDainoDataEntity.GYOMUNAISHUCD).ToString
            intTorokuRenban = CInt(csDataRows(0).Item(ABDainoDataEntity.TOROKURENBAN))


            '��[���t��ݐσf�[�^�̎擾
            ' ��[���t��ݐςc�`�N���X�̃C���X�^���X��
            csDainoSfskRuisekiB = New ABDainoSfskRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            csDainoRirekiDataRows = csDainoSfskRuisekiB.GetABDainoSfskRuisekiData(strJuminCd,
                                                  strGyomuCD, strGyomuNaiShuCD, intTorokuRenban, strShoriKB)

            ' �f�[�^�Z�b�g�C���X�^���X��
            csReturnDataset = New DataSet

            ' �e�[�u���Z�b�g�̎擾
            csDataTable = Me.CreateColumnsABDainoRirekiData()

            ' �f�[�^�Z�b�g�Ƀe�[�u���Z�b�g�̒ǉ�
            csReturnDataset.Tables.Add(csDataTable)

            ' ���t�N���X�̃C���X�^���X��
            cfDate = New UFDateClass(m_cfConfigDataClass)

            ' ��[�֌W�擾�C���X�^���X��
            cDainoKankeiB = New ABDainoKankeiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' �Ɩ��R�[�h�}�X�^�c�`�̃C���X�^���X�쐬
            cGyomuCDMstB = New URGyomuCDMstBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' �����ҏW�a�̃C���X�^���X�쐬
            cAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' �����擾�N���X�C���X�^���X��
            cAtenaGetB = New ABAtenaGetBClass(m_cfControlData, m_cfConfigDataClass)

            ' �Z����ʃN���X�C���X�^���X��
            cJuminShubetsuB = New ABJuminShubetsuBClass(m_cfControlData, m_cfConfigDataClass)

            ' �Ǔ��ǊO�N���X�C���X�^���X��
            cKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfControlData, m_cfConfigDataClass)

            ' ���l�N���X�̃C���X�^���X��
            cABBikoB = New ABBikoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            For Each csDataRow In csDainoRirekiDataRows
                csDataNewRow = csReturnDataset.Tables(ABDainoDataEntity.TABLE_NAME).NewRow

                ' �����l�̐ݒ�
                For Each csDataColumn In csDataNewRow.Table.Columns
                    If (csDataColumn.ColumnName = ABDainoDataEntity.KOSHINCOUNTER) Then
                        csDataNewRow(csDataColumn.ColumnName) = Decimal.Zero
                    Else
                        csDataNewRow(csDataColumn.ColumnName) = String.Empty
                    End If
                Next csDataColumn

                ' �Z���R�[�h
                csDataNewRow(ABDainoDataEntity.JUMINCD) = csDataRow(ABDainoSfskRuisekiEntity.JUMINCD)
                ' �s�����R�[�h
                csDataNewRow(ABDainoDataEntity.SHICHOSONCD) = csDataRow(ABDainoSfskRuisekiEntity.SHICHOSONCD)
                ' ���s�����R�[�h
                csDataNewRow(ABDainoDataEntity.KYUSHICHOSONCD) = csDataRow(ABDainoSfskRuisekiEntity.KYUSHICHOSONCD)
                ' �Ɩ��R�[�h
                csDataNewRow(ABDainoDataEntity.GYOMUCD) = csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD)

                ' �Ɩ��R�[�h�}�X�^���擾����
                strGyomuCD = CType(csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD), String)
                csGyomuCDMstEntity = cGyomuCDMstB.GetGyomuCDHoshu(strGyomuCD)

                If (csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows.Count = 0) Then
                    ' �Ɩ�����
                    csDataNewRow(ABHiDainoDataEntity.GYOMUMEISHO) = String.Empty
                    ' �Ɩ����̗�
                    csDataNewRow(ABHiDainoDataEntity.GYOMUMEISHORYAKU) = String.Empty
                Else
                    ' �Ɩ�����
                    csDataNewRow(ABHiDainoDataEntity.GYOMUMEISHO) = csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows(0).Item(URGyomuCDMstEntity.GYOMUMEI)
                    ' �Ɩ����̗�
                    csDataNewRow(ABHiDainoDataEntity.GYOMUMEISHORYAKU) = csGyomuCDMstEntity.Tables(URGyomuCDMstEntity.TABLE_NAME).Rows(0).Item(URGyomuCDMstEntity.GYOMURYAKUSHO)
                End If

                ' �Ɩ�����ʃR�[�h
                csDataNewRow(ABDainoDataEntity.GYOMUNAISHUCD) = csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD)
                ' ��[�Z���R�[�h
                csDataNewRow(ABDainoDataEntity.DAINOJUMINCD) = csDataRow(ABDainoSfskRuisekiEntity.DAINOJUMINCD)
                ' �J�n�N��
                csDataNewRow(ABDainoDataEntity.STYMD) = csDataRow(ABDainoSfskRuisekiEntity.STYMD)
                ' �I���N��
                csDataNewRow(ABDainoDataEntity.EDYMD) = csDataRow(ABDainoSfskRuisekiEntity.EDYMD)

                ' �\���p�J�n�N��
                cfDate.p_strDateValue = CType(csDataRow(ABDainoSfskRuisekiEntity.STYMD), String)
                cfDate.p_enEraType = UFEraType.KanjiRyaku
                cfDate.p_enDateSeparator = UFDateSeparator.Period
                csDataNewRow(ABDainoDataEntity.DISP_STYMD) = cfDate.p_strWarekiYMD

                ' �\���p�I���N���i999999�̎��́A��\���j
                If (CType(csDataRow(ABDainoSfskRuisekiEntity.EDYMD), String) = ALL9_YMD) Then
                    csDataNewRow(ABDainoDataEntity.DISP_EDYMD) = String.Empty
                Else
                    cfDate.p_strDateValue = CType(csDataRow(ABDainoSfskRuisekiEntity.EDYMD), String)
                    csDataNewRow(ABDainoDataEntity.DISP_EDYMD) = cfDate.p_strWarekiYMD
                End If

                ' ��[�敪
                csDataNewRow(ABDainoDataEntity.DAINOKB) = csDataRow(ABDainoSfskRuisekiEntity.DAINOKB)
                ' ��[�敪����
                strDainoKB = CType(csDataRow(ABDainoSfskRuisekiEntity.DAINOKB), String)
                csDainoKankeiDataSet = cDainoKankeiB.GetDainoKBHoshu(strDainoKB)
                intRowCount = csDainoKankeiDataSet.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows.Count
                If (Not (intRowCount = 0)) Then
                    csDataNewRow(ABDainoDataEntity.DAINOKBMEISHO) = CType(csDainoKankeiDataSet.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows(0).Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO), String)
                    csDataNewRow(ABDainoDataEntity.DAINOKBRYAKUMEI) = CType(csDainoKankeiDataSet.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows(0).Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI), String)
                End If

                ' �����擾�p�����[�^�C���X�^���X��
                cAtenaGetPara1X = New ABAtenaGetPara1XClass

                ' �������o�L�[�Z�b�g
                cAtenaGetPara1X.p_strJuminCD = CType(csDataRow(ABDainoSfskRuisekiEntity.DAINOJUMINCD), String)
                cAtenaGetPara1X.p_strJushoHenshu1 = JUSHOHENSHU1_PARA_ONE
                cAtenaGetPara1X.p_blnSakujoFG = True
                cAtenaGetPara1X.p_strDaihyoShaKB = GET_HONNINDATA       '*�{�l�f�[�^�擾
                '�l�ԍ��擾�p�����[�^��ݒ�
                cAtenaGetPara1X.p_strMyNumberKB = ABConstClass.MYNUMBER.MYNUMBERKB.ON

                Try
                    '�u�����擾�a�v�N���X�́u�����擾�Q�v���\�b�h�����s
                    csAtenaDataSet = cAtenaGetB.AtenaGet2(cAtenaGetPara1X)

                    intRowCount = csAtenaDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
                    If (Not (intRowCount = 1)) Then
                        '�G���[�N���X�̃C���X�^���X��
                        cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                        '�G���[��`���擾
                        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003078)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                    '�u�����ҏW�a�v�N���X�́u�����ҏW�v���\�b�h�����s����
                    csAtena1Entity = cAtenaHenshuB.AtenaHenshu(cAtenaGetPara1X, csAtenaDataSet)

                    csAtenaRow = csAtenaDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)

                    ' �Z�����̎擾�L�[�Z�b�g
                    strDataKB = CType(csAtenaRow(ABAtenaEntity.ATENADATAKB), String)
                    strDataShu = CType(csAtenaRow(ABAtenaEntity.ATENADATASHU), String)
                    ' �Z�����̎擾���]�b�g���s
                    cJuminShubetsuB.GetJuminshubetsu(strDataKB, strDataShu)
                    ' �Z����ʖ���
                    csDataNewRow(ABDainoDataEntity.JUMINSHUMEISHO) = cJuminShubetsuB.p_strHenshuShubetsu

                    ' �J�i��
                    strMeisho = CType(csAtenaRow(ABAtenaEntity.KANAMEISHO2), String)
                    If (strMeisho = String.Empty) Then
                        csDataNewRow(ABDainoDataEntity.KANASHIMEI) = csAtenaRow(ABAtenaEntity.KANAMEISHO1)
                    Else
                        '### �@�l�̎��̓J�i���̂P�ƃJ�i���̂Q�𔼊p�X�y�[�X�ł�������
                        If (strDataKB = DATAKB_HOJIN) Then
                            ' ����������������ꍇ�CMaxLength�𒴂��Ȃ��悤�ɐ؂�l��
                            strKanaShimei = CType(csAtenaRow(ABAtenaEntity.KANAMEISHO1), String) + " " + CType(csAtenaRow(ABAtenaEntity.KANAMEISHO2), String)
                            If (strKanaShimei.RLength > csDataNewRow.Table.Columns(ABDainoDataEntity.KANASHIMEI).MaxLength) Then
                                csDataNewRow(ABDainoDataEntity.KANASHIMEI) = strKanaShimei.RSubstring(0, csDataNewRow.Table.Columns(ABDainoDataEntity.KANASHIMEI).MaxLength)
                            Else
                                csDataNewRow(ABDainoDataEntity.KANASHIMEI) = strKanaShimei
                            End If
                        ElseIf (strDataShu.Chars(0) = DATASHU_FRN) Then
                            '### �O���l�̎��̓J�i���̂P
                            csDataNewRow(ABDainoDataEntity.KANASHIMEI) = csAtenaRow(ABAtenaEntity.KANAMEISHO1)
                        Else
                            csDataNewRow(ABDainoDataEntity.KANASHIMEI) = csAtenaRow(ABAtenaEntity.KANAMEISHO2)
                        End If
                    End If

                    strKanjiShimei = CType(csAtena1Entity.Tables(ABAtena1Entity.TABLE_NAME).Rows(0).Item(ABAtena1Entity.HENSHUKANJISHIMEI), String)
                    If (csDataNewRow.Table.Columns(ABDainoDataEntity.KANJISHIMEI).MaxLength < strKanjiShimei.RLength) Then
                        csDataNewRow(ABDainoDataEntity.KANJISHIMEI) = strKanjiShimei.RSubstring(0, csDataNewRow.Table.Columns(ABDainoDataEntity.KANJISHIMEI).MaxLength)
                    Else
                        csDataNewRow(ABDainoDataEntity.KANJISHIMEI) = strKanjiShimei
                    End If

                    ' �Ǔ��ǊO���̃L�[�Z�b�g
                    strKannaiKangaiCD = CType(csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB), String)
                    ' �Ǔ��ǊO���̎擾���]�b�g���s
                    strKannaiKangaiMeisho = cKannaiKangaiKBB.GetKannaiKangai(strKannaiKangaiCD)
                    ' �Ǔ��ǊO����
                    csDataNewRow(ABDainoDataEntity.KANNAIKANGAIMEISHO) = strKannaiKangaiMeisho
                    ' �X�֔ԍ�
                    csDataNewRow(ABDainoDataEntity.YUBINNO) = csAtenaRow(ABAtenaEntity.YUBINNO)
                    ' �Z���R�[�h
                    csDataNewRow(ABDainoDataEntity.JUSHOCD) = csAtenaRow(ABAtenaEntity.JUSHOCD)
                    ' �Z����
                    csDataNewRow(ABDainoDataEntity.JUSHO) = csAtenaRow(ABAtenaEntity.JUSHO)
                    ' �Ԓn�R�[�h�P
                    csDataNewRow(ABDainoDataEntity.BANCHICD1) = csAtenaRow(ABAtenaEntity.BANCHICD1)
                    ' �Ԓn�R�[�h�Q
                    csDataNewRow(ABDainoDataEntity.BANCHICD2) = csAtenaRow(ABAtenaEntity.BANCHICD2)
                    ' �Ԓn�R�[�h�R
                    csDataNewRow(ABDainoDataEntity.BANCHICD3) = csAtenaRow(ABAtenaEntity.BANCHICD3)
                    ' �Ԓn
                    csDataNewRow(ABDainoDataEntity.BANCHI) = csAtenaRow(ABAtenaEntity.BANCHI)
                    ' �����t���O
                    csDataNewRow(ABDainoDataEntity.KATAGAKIFG) = csAtenaRow(ABAtenaEntity.KATAGAKIFG)
                    ' �����R�[�h
                    csDataNewRow(ABDainoDataEntity.KATAGAKICD) = csAtenaRow(ABAtenaEntity.KATAGAKICD)
                    ' ����
                    csDataNewRow(ABDainoDataEntity.KATAGAKI) = csAtenaRow(ABAtenaEntity.KATAGAKI)
                    ' �A����P
                    csDataNewRow(ABDainoDataEntity.RENRAKUSAKI1) = csAtenaRow(ABAtenaEntity.RENRAKUSAKI1)
                    ' �A����Q
                    csDataNewRow(ABDainoDataEntity.RENRAKUSAKI2) = csAtenaRow(ABAtenaEntity.RENRAKUSAKI2)
                    ' �s����R�[�h
                    csDataNewRow(ABDainoDataEntity.GYOSEIKUCD) = csAtenaRow(ABAtenaEntity.GYOSEIKUCD)
                    ' �s���於
                    csDataNewRow(ABDainoDataEntity.GYOSEIKUMEI) = csAtenaRow(ABAtenaEntity.GYOSEIKUMEI)
                    ' �n��R�[�h�P
                    csDataNewRow(ABDainoDataEntity.CHIKUCD1) = csAtenaRow(ABAtenaEntity.CHIKUCD1)
                    ' �n�於�P
                    csDataNewRow(ABDainoDataEntity.CHIKUMEI1) = csAtenaRow(ABAtenaEntity.CHIKUMEI1)
                    ' �n��R�[�h�Q
                    csDataNewRow(ABDainoDataEntity.CHIKUCD2) = csAtenaRow(ABAtenaEntity.CHIKUCD2)
                    ' �n�於�Q
                    csDataNewRow(ABDainoDataEntity.CHIKUMEI2) = csAtenaRow(ABAtenaEntity.CHIKUMEI2)
                    ' �n��R�[�h�R
                    csDataNewRow(ABDainoDataEntity.CHIKUCD3) = csAtenaRow(ABAtenaEntity.CHIKUCD3)
                    ' �n�於�R
                    csDataNewRow(ABDainoDataEntity.CHIKUMEI3) = csAtenaRow(ABAtenaEntity.CHIKUMEI3)
                    ' �X�֔ԍ�
                    strYubinNO = CType(csAtenaRow(ABAtenaEntity.YUBINNO), String).Trim
                    If (3 < strYubinNO.RLength) Then
                        csDataNewRow(ABDainoDataEntity.DISP_YUBINNO) = strYubinNO.RSubstring(0, 3) + "-" + strYubinNO.RSubstring(3)
                    Else
                        csDataNewRow(ABDainoDataEntity.DISP_YUBINNO) = strYubinNO
                    End If
                    ' �\���p�ҏW�Z��
                    csDataNewRow(ABDainoDataEntity.DISP_HENSHUJUSHO) = csAtena1Entity.Tables(ABAtena1Entity.TABLE_NAME).Rows(0).Item(ABAtena1Entity.HENSHUJUSHO)
                    csDataNewRow(ABDainoDataEntity.KOSHINUSER) = csDataRow(ABAtenaEntity.KOSHINUSER)
                    csDataNewRow(ABDainoDataEntity.MYNUMBER) = csAtenaRow(ABMyNumberEntity.MYNUMBER)
                    csDataNewRow(ABDainoDataEntity.ATENADATAKB) = csAtenaRow(ABAtenaEntity.ATENADATAKB)

                    ' ���l�}�X�^���擾
                    csBikoDataSet = cABBikoB.SelectByKey(
                                            ABBikoEntity.DEFAULT.BIKOKBN.DAINO,
                                            csDataRow(ABDainoSfskRuisekiEntity.JUMINCD).ToString(),
                                            csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD).ToString(),
                                            csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD).ToString(),
                                            csDataRow(ABDainoSfskRuisekiEntity.TOROKURENBAN).ToString(),
                                            csDataRow(ABDainoSfskRuisekiEntity.RRKNO).ToString(),
                                            blnSakujoFG)

                    If (csBikoDataSet IsNot Nothing _
                                AndAlso 0 < csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows.Count) Then
                        csDataNewRow(ABDainoDataEntity.BIKO) = csBikoDataSet.Tables(ABBikoEntity.TABLE_NAME).Rows(0).Item(ABBikoEntity.BIKO)
                    Else
                        csDataNewRow(ABDainoDataEntity.BIKO) = String.Empty
                    End If

                    csDataNewRow(ABDainoDataEntity.CHECK) = False
                    csDataNewRow(ABDainoDataEntity.JOTAI) = ABDainoSfskShoriMode.Empty.GetHashCode.ToString
                    csDataNewRow(ABDainoDataEntity.DISP_JOTAI) = String.Empty
                    csDataNewRow(ABDainoDataEntity.SEIGYOKB) = String.Empty

                    csDataNewRow(ABDainoDataEntity.TOROKURENBAN) = csDataRow(ABDainoSfskRuisekiEntity.TOROKURENBAN)     '�o�^�A��
                    csDataNewRow(ABDainoDataEntity.RRKNO) = csDataRow(ABDainoSfskRuisekiEntity.RRKNO)                   '����ԍ�
                    csDataNewRow(ABDainoDataEntity.SHIKUCHOSONCD) = String.Empty                                        '�s�撬���R�[�g
                    csDataNewRow(ABDainoDataEntity.MACHIAZACD) = String.Empty                                           '�����R�[�h
                    csDataNewRow(ABDainoDataEntity.TODOFUKEN) = String.Empty                                            '�s���{��

                    csDataNewRow(ABDainoDataEntity.SHORINICHIJI) = csDataRow(ABDainoSfskRuisekiEntity.SHORINICHIJI)     '��������
                    csDataNewRow(ABDainoDataEntity.ZENGOKB) = csDataRow(ABDainoSfskRuisekiEntity.ZENGOKB)               '�O��敪

                Catch
                    '���̂܂܃X���[����
                    Throw
                End Try


                ' �폜�t���O
                csDataNewRow(ABDainoDataEntity.SAKUJOFG) = csDataRow(ABDainoSfskRuisekiEntity.SAKUJOFG)

                ' �X�V�J�E���^
                csDataNewRow(ABDainoDataEntity.KOSHINCOUNTER) = csDataRow(ABDainoSfskRuisekiEntity.KOSHINCOUNTER)

                csReturnDataset.Tables(ABDainoDataEntity.TABLE_NAME).Rows.Add(csDataNewRow)

            Next csDataRow
            csReturnDataset.AcceptChanges()

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        End Try

        Return csReturnDataset

    End Function
#End Region

#Region "��[���t��ݐϗ����f�[�^�J�����쐬"

    '************************************************************************************************
    '* ���\�b�h��      �f�[�^�J�����쐬
    '* 
    '* �\��            Private Function CreateColumnsABSfskRirekiData() As DataTable
    '* 
    '* �@�\�@�@        ���t�旚�����Z�b�V�����̃J������`���쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataTable() ��[�������e�[�u��
    '************************************************************************************************
    Private Function CreateColumnsABSfskRirekiData() As DataTable
        Const THIS_METHOD_NAME As String = "CreateColumnsABSfskRirekiData"
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn
        Dim csDataPrimaryKey(8) As DataColumn               '��L�[

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���t����J������`
            csDataTable = New DataTable()
            csDataTable.TableName = ABSfskDataEntity.TABLE_NAME
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.AllowDBNull = False
            csDataColumn.MaxLength = 15
            csDataPrimaryKey(0) = csDataColumn              '��L�[�@�@�Z���R�[�h
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(1) = csDataColumn              '��L�[�A�@�Ɩ��R�[�h
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUMEISHORYAKU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.GYOMUNAISHUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(2) = csDataColumn              '��L�[�B�@�Ɩ�����R�[�h
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.STYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn.AllowDBNull = False
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.EDYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn.AllowDBNull = False
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKDATAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANNAiKANGAIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANNAIKANGAIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANAMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120        '60
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKANJIMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480        '40
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKYUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKZJUSHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKJUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '30
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKBANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '20
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BANCHICD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BANCHICD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BANCHICD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200         '30
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKRENRAKUSAKI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKRENRAKUSAKI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKGYOSEIKUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKGYOSEIKUMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUCD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUMEI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUCD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUMEI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUCD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKCHIKUMEI3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SAKUJOFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.KOSHINCOUNTER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            ' �X�V���[�U�[
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.KOSHINUSER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 32

            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_STYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_EDYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9

            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.BIKO, GetType(String))
            csDataColumn.DefaultValue = String.Empty

            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.CHECK, GetType(String))
            csDataColumn.DefaultValue = Boolean.FalseString
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.JOTAI, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_JOTAI, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DAINOKB, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DISP_DAINOKB, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.DAINOJUMINCD, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataPrimaryKey(3) = csDataColumn              '��L�[�C�@��[�Z���R�[�h
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SEIGYOKB, GetType(String))
            csDataColumn.DefaultValue = String.Empty

            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.TOROKURENBAN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataPrimaryKey(4) = csDataColumn              '��L�[�D�@�o�^�A��
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.RRKNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataPrimaryKey(5) = csDataColumn              '��L�[�E�@����ԍ�
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SFSKKATAGAKICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHIKUCHOSONCD, GetType(String))
            csDataColumn.DefaultValue = 6
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.MACHIAZACD, GetType(String))
            csDataColumn.DefaultValue = 7
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.TODOFUKEN, GetType(String))
            csDataColumn.DefaultValue = 16
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHIKUCHOSON, GetType(String))
            csDataColumn.DefaultValue = 48
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.MACHIAZA, GetType(String))
            csDataColumn.DefaultValue = 480
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.SHORINICHIJI, GetType(String))
            csDataColumn.DefaultValue = 17
            csDataPrimaryKey(6) = csDataColumn              '��L�[�F�@��������
            csDataColumn = csDataTable.Columns.Add(ABSfskDataEntity.ZENGOKB, GetType(String))
            csDataColumn.DefaultValue = 1
            csDataPrimaryKey(7) = csDataColumn              '��L�[�G�@�O��敪

            csDataTable.PrimaryKey = csDataPrimaryKey       '��L�[

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csDataTable

    End Function

    '************************************************************************************************
    '* ���\�b�h��      �f�[�^�J�����쐬
    '* 
    '* �\��            Private Function CreateColumnsABDainoRirekiData() As DataTable
    '* 
    '* �@�\�@�@        ��[�������Z�b�V�����̃J������`���쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataTable() ��[�������e�[�u��
    '************************************************************************************************
    Private Function CreateColumnsABDainoRirekiData() As DataTable
        Const THIS_METHOD_NAME As String = "CreateColumnsABDainoRirekiData"
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn
        Dim csDataPrimaryKey(8) As DataColumn               '��L�[

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ��[���J������`
            csDataTable = New DataTable
            csDataTable.TableName = ABDainoDataEntity.TABLE_NAME
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.AllowDBNull = False
            csDataColumn.MaxLength = 15
            csDataPrimaryKey(0) = csDataColumn              '��L�[�@�@�Z���R�[�h
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(1) = csDataColumn              '��L�[�A�@�Ɩ��R�[�h
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUMEISHORYAKU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOMUNAISHUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(2) = csDataColumn              '��L�[�B�@�Ɩ�����R�[�h
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOJUMINCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn.AllowDBNull = False
            csDataPrimaryKey(3) = csDataColumn              '��L�[�C�@��[�Z���R�[�h
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.STYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.EDYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DAINOKBRYAKUMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUMINSHUMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KANASHIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 240
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KANJISHIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KANNAIKANGAIMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.YUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUSHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHICD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHICD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHICD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KATAGAKIFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KATAGAKICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.RENRAKUSAKI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.RENRAKUSAKI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOSEIKUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.GYOSEIKUMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUCD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUMEI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUCD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUMEI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUCD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHIKUMEI3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SAKUJOFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KOSHINCOUNTER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_STYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_EDYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_YUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_HENSHUJUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 160
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.KOSHINUSER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 32
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.MYNUMBER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.ATENADATAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.BIKO, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.CHECK, GetType(String))
            csDataColumn.DefaultValue = Boolean.FalseString
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.JOTAI, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.DISP_JOTAI, GetType(String))
            csDataColumn.DefaultValue = String.Empty
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SEIGYOKB, GetType(String))
            csDataColumn.DefaultValue = String.Empty

            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SFSKZJUSHOCD, GetType(String))
            csDataColumn.DefaultValue = 13
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.TOROKURENBAN, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataPrimaryKey(4) = csDataColumn              '��L�[�D�@�o�^�A��
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.RRKNO, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataPrimaryKey(5) = csDataColumn              '��L�[�E�@����ԍ�
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SHIKUCHOSONCD, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.MACHIAZACD, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.TODOFUKEN, GetType(String))
            csDataColumn.DefaultValue = 10
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.SHORINICHIJI, GetType(String))
            csDataColumn.DefaultValue = 17
            csDataPrimaryKey(6) = csDataColumn              '��L�[�F�@��������
            csDataColumn = csDataTable.Columns.Add(ABDainoDataEntity.ZENGOKB, GetType(String))
            csDataColumn.DefaultValue = 1
            csDataPrimaryKey(7) = csDataColumn              '��L�[�G�@�O��敪

            csDataTable.PrimaryKey = csDataPrimaryKey       '��L�[

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csDataTable

    End Function
#End Region
    '*����ԍ� 000003 2023/10/25 �ǉ��I��
#End Region

End Class
