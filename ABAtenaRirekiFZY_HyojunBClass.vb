'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a��������t��_�W���}�X�^�c�`(ABAtenaRirekiFZY_HyojunBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2023/08/14 ����  �Y��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2023/12/11  000001     �yAB-9000-1�z�Z��X�V�A�g�W�����Ή�(����)
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

'************************************************************************************************
'*
'* ��������t��_�W���}�X�^�擾���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABAtenaRirekiFZY_HyojunBClass
#Region "�����o�ϐ�"
    ' �p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                                              ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                                        ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass                                ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                                              ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                                          ' �G���[�����N���X
    Private m_strInsertSQL As String                                                ' INSERT�pSQL
    Private m_strUpdateSQL As String                                                ' UPDATE�pSQL
    Private m_strDelRonriSQL As String                                              ' �_���폜�pSQL
    Private m_strDelButuriSQL As String                                             ' �����폜�pSQL
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      ' SELECT�p�p�����[�^�R���N�V����
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      ' INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      ' UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    ' �_���폜�p�p�����[�^�R���N�V����
    Private m_cfDelButuriUFParameterCollectionClass As UFParameterCollectionClass   ' �����폜�p�p�����[�^�R���N�V����
    Private m_csDataSchma As DataSet                                                ' �X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_strUpdateDatetime As String                                           ' �X�V����

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaRirekiFZY_HyojunBClass"       ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h
    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero
    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"
    Private Const ERR_JUMINCD As String = "�Z���R�[�h"

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass, 
    '* �@�@                          ByVal cfRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* �@�@           cfRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
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
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' �p�����[�^�̃����o�ϐ�
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing

    End Sub
#End Region

#Region "���\�b�h"
#Region "��������t��_�W���}�X�^���o"
    '************************************************************************************************
    '* ���\�b�h��     ��������t��_�W���}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaRirekiFZYHyojunBHoshu(ByVal strJuminCD As String, _
    '*                                                              ByVal strRirekiNO As String, _
    '*                                                              ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@ ��������t��_�W���}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD         : �Z���R�[�h 
    '*                strRirekiNO        : ����ԍ�
    '*                blnSakujoFG        : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾��������_�W���}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Function GetAtenaRirekiFZYHyojunBHoshu(ByVal strJuminCD As String,
                                                  ByVal strRirekiNO As String,
                                                  ByVal blnSakujoFG As Boolean) As DataSet
        Return GetAtenaRirekiFZYHyojunBHoshu(strJuminCD, strRirekiNO, String.Empty, blnSakujoFG)
    End Function

#End Region

#Region "��������t��_�W���}�X�^���o"
    '************************************************************************************************
    '* ���\�b�h��     ��������t��_�W���}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaRirekiFZYHyojunBHoshu(ByVal strJuminCD As String, _
    '*                                                              ByVal strRirekiNO As String, _
    '*                                                              ByVal strJuminJUtogaiKB As String,
    '*                                                              ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@ ��������t��_�W���}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD              : �Z���R�[�h 
    '*                strRirekiNO             : ����ԍ�
    '*                ByVal strJuminJUtogaiKB : �Z���Z�o�O�敪
    '*                blnSakujoFG             : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾��������_�W���}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Function GetAtenaRirekiFZYHyojunBHoshu(ByVal strJuminCD As String,
                                                  ByVal strRirekiNO As String,
                                                  ByVal strJuminJUtogaiKB As String,
                                                  ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetAtenaRirekiFZYHyojunBHoshu"
        Dim cfErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            ' �Z���R�[�h���w�肳��Ă��Ȃ��Ƃ��G���[
            If (IsNothing(strJuminCD) OrElse (strJuminCD.Trim.RLength = 0)) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' ��O�𐶐�
                Throw New UFAppException(cfErrorStruct.m_strErrorMessage + ERR_JUMINCD, cfErrorStruct.m_strErrorCode)
            Else
                '�����Ȃ�
            End If

            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME)
            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(),
                                                                         ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            strSQL.Append(Me.CreateWhere(strJuminCD, strRirekiNO, strJuminJUtogaiKB, blnSakujoFG))

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:GetDataSet�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                                strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity,
                                                    ABAtenaRirekiFZYHyojunEntity.TABLE_NAME,
                                                    m_cfSelectUFParameterCollectionClass, False)

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

        Return csAtenaEntity

    End Function

#End Region

#Region "SELECT��̍쐬"
    '************************************************************************************************
    '* ���\�b�h��     SELECT��̍쐬
    '* 
    '* �\��           Private Sub CreateSelect() As String
    '* 
    '* �@�\�@�@    �@ SELECT��𐶐�����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         String    :   SELECT��
    '************************************************************************************************
    Private Function CreateSelect() As String
        Const THIS_METHOD_NAME As String = "CreateSelect"
        Dim csSELECT As New StringBuilder

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT��̍쐬
            csSELECT.AppendFormat("SELECT {0}", ABAtenaRirekiFZYHyojunEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.RIREKINO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.JUMINJUTOGAIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SEARCHFRNMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SEARCHKANAFRNMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SEARCHTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SEARCHKANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.TSUSHOKANAKAKUNINFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SHIMEIYUSENKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SEARCHKANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.ZAIRYUCARDNOKBN)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.JUKYOCHIHOSEICD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.HODAI30JO46MATAHA47KB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.STAINUSSHIMEIYUSENKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.TOKUSHOMEI_YUKOKIGEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYHyojunEntity.KOSHINUSER)

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

        Return csSELECT.ToString

    End Function
#End Region

#Region "WHERE���̍쐬"
    '************************************************************************************************
    '* ���\�b�h��     WHERE���̍쐬
    '* 
    '* �\��           Private Function CreateWhere(ByVal strJuminCD As String, _
    '*                                             ByVal strRirekiNO As String, _
    '*                                             ByVal strJuminJutogaiKb As String
    '*                                             ByVal blnSakujoFG As Boolean) As String
    '* 
    '* �@�\�@�@    �@ WHERE�����쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           strJuminCD         : �Z���R�[�h 
    '*                strRirekiNO        : ����ԍ�
    '*                strJuminJutogaiKb  : �Z���Z�o�O�敪
    '*                blnSakujoFG        : �폜�t���O
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String,
                                 ByVal strRirekiNO As String,
                                 ByVal strJuminJutogaiKb As String,
                                 ByVal blnSakujoFG As Boolean) As String

        Const THIS_METHOD_NAME As String = "CreateWhere"
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT�p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' WHERE��̍쐬
            csWHERE = New StringBuilder(256)

            ' �Z���R�[�h
            csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiFZYHyojunEntity.JUMINCD, ABAtenaRirekiFZYHyojunEntity.KEY_JUMINCD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ����ԍ�
            If (Not strRirekiNO.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYHyojunEntity.RIREKINO, ABAtenaRirekiFZYHyojunEntity.KEY_RIREKINO)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_RIREKINO
                cfUFParameterClass.Value = strRirekiNO
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '�����Ȃ�
            End If

            ' �Z���Z�o�O�敪
            If (Not strJuminJutogaiKb.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYHyojunEntity.JUMINJUTOGAIKB, ABAtenaRirekiFZYHyojunEntity.PARAM_JUMINJUTOGAIKB)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.PARAM_JUMINJUTOGAIKB
                cfUFParameterClass.Value = strJuminJutogaiKb
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '�����Ȃ�
            End If

            ' �폜�t���O
            If (blnSakujoFG = False) Then
                csWHERE.AppendFormat(" AND {0} <> '{1}'", ABAtenaRirekiFZYHyojunEntity.SAKUJOFG, SAKUJOFG_ON)
            Else
                '�����Ȃ�
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

        Return csWHERE.ToString

    End Function

#End Region

#Region "��������t��_�W���}�X�^�ǉ�"
    '************************************************************************************************
    '* ���\�b�h��     ��������t��_�W���}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaRirekiFZYHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ ��������t��_�W���}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaRirekiFZYHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaRirekiFZYHyojunB"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer                            '�ǉ�����

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If ((m_strInsertSQL Is Nothing) OrElse (m_strInsertSQL = String.Empty) _
                OrElse (m_cfInsertUFParameterCollectionClass Is Nothing)) Then
                Call CreateInsertSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRirekiFZYHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId     '�[���h�c
            csDataRow(ABAtenaRirekiFZYHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF                        '�폜�t���O
            csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              '�X�V�J�E���^
            csDataRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      '�쐬���[�U�[
            csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId       '�X�V���[�U�[

            '�쐬�����A�X�V�����̐ݒ�
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI))

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(
                    ABAtenaRirekiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                            m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

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

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     Insert�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           INSERT�p��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateInsertSQL"
        Dim csDataColumn As DataColumn
        Dim csInsertColumn As StringBuilder                 'INSERT�p�J������`
        Dim csInsertParam As StringBuilder                  'INSERT�p�p�����[�^��`
        Dim cfUFParameterClass As UFParameterClass
        Dim strParamName As String

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL���̍쐬
            csInsertColumn = New StringBuilder
            csInsertParam = New StringBuilder

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass
                strParamName = String.Format("{0}{1}", ABAtenaRirekiFZYHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL���̍쐬
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '�Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})",
                                           ABAtenaRirekiFZYHyojunEntity.TABLE_NAME,
                                           csInsertColumn.ToString.TrimEnd(",".ToCharArray),
                                           csInsertParam.ToString.TrimEnd(",".ToCharArray))

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

    End Sub
#End Region

#Region "��������t��_�W���}�X�^�X�V"
    '************************************************************************************************
    '* ���\�b�h��     ��������t��_�W���}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaRirekiFZYHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ ��������t��_�W���}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csDataRow As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateAtenaRirekiFZYHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaRirekiFZYHyojunB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        Dim intUpdCnt As Integer                            '�X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If ((m_strUpdateSQL Is Nothing) OrElse (m_strUpdateSQL = String.Empty) _
                OrElse (m_cfUpdateUFParameterCollectionClass Is Nothing)) Then
                Call CreateUpdateSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRirekiFZYHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId   '�[���h�c
            csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER) =
                CDec(csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER)) + 1                  '�X�V�J�E���^
            csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId     '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI))

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYHyojunEntity.PREFIX_KEY.RLength) =
                    ABAtenaRirekiFZYHyojunEntity.PREFIX_KEY) Then

                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYHyojunEntity.PREFIX_KEY.RLength)).ToString()

                    '�L�[���ڈȊO�͕ҏW���e�擾
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                         = csDataRow(cfParam.ParameterName.RSubstring(
                              ABAtenaRirekiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                                m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

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

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     Update�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           UPDATE�p�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateUpdateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE��`
        Dim csUpdateParam As StringBuilder                  'UPDATE�pSQL��`

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABAtenaRirekiFZYHyojunEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KEY_RIREKINO)

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�Z���b�c�E����ԍ��E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If (Not (csDataColumn.ColumnName = ABAtenaRirekiFZYHyojunEntity.JUMINCD) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaRirekiFZYHyojunEntity.RIREKINO) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaRirekiFZYHyojunEntity.SAKUSEIUSER) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI)) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL���̍쐬
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaRirekiFZYHyojunEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                Else
                    '�����Ȃ�
                End If

            Next csDataColumn

            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(",".ToCharArray())

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += csWhere.ToString

            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_RIREKINO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

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

    End Sub
#End Region

#Region "��������t��_�W���}�X�^�폜"
    '************************************************************************************************
    '* ���\�b�h��     ��������t��_�W���}�X�^�폜
    '* 
    '* �\��           Public Function DeleteAtenaRirekiFZYHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ ��������t��_�W���}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csDataRow As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Function DeleteAtenaRirekiFZYHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaRirekiFZYHyojunB"
        Dim cfParam As UFParameterClass  '�p�����[�^�N���X
        Dim intDelCnt As Integer        '�폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If ((m_strDelRonriSQL Is Nothing) OrElse (m_strDelRonriSQL = String.Empty) _
                    OrElse (m_cfDelRonriUFParameterCollectionClass Is Nothing)) Then
                Call CreateDeleteRonriSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRirekiFZYHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId   '�[���h�c
            csDataRow(ABAtenaRirekiFZYHyojunEntity.SAKUJOFG) = SAKUJOFG_ON                       '�폜�t���O
            csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER) =
                CDec(csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER)) + 1                  '�X�V�J�E���^
            csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId     '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI))

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYHyojunEntity.PREFIX_KEY.RLength) =
                        ABAtenaRirekiFZYHyojunEntity.PREFIX_KEY) Then

                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                                 csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYHyojunEntity.PREFIX_KEY.RLength),
                                           DataRowVersion.Original).ToString()
                    '�L�[���ڈȊO�͕ҏW���e��ݒ�
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(
                            ABAtenaRirekiFZYHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                                m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")
            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

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

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��������t��_�W���}�X�^�����폜
    '* 
    '* �\��           Public Function DeleteAtenaFZYHyojunRB(ByVal csDataRow As DataRow, _
    '*                                                       ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@ ��������t��_�W���}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csDataRow As DataRow   : �폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '*                strSakujoKB As String  : �폜�t���O
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaFZYHyojunRB(ByVal csDataRow As DataRow,
                                                     ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaFZYHyojunRB"
        Dim cfErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        Dim cfParam As UFParameterClass                     ' �p�����[�^�N���X
        Dim intDelCnt As Integer                            ' �폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �폜�敪�̃`�F�b�N���s��
            If (Not (strSakujoKB = "D")) Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                cfErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB)
                '��O�𐶐�
                Throw New UFAppException(cfErrorStruct.m_strErrorMessage, cfErrorStruct.m_strErrorCode)
            Else
                '�����Ȃ�
            End If

            ' �폜�p�̃p�����[�^�tDELETE��������ƃp�����[�^�R���N�V�������쐬����
            If ((m_strDelButuriSQL Is Nothing) OrElse (m_strDelButuriSQL = String.Empty) _
                    OrElse IsNothing(m_cfDelButuriUFParameterCollectionClass)) Then
                Call CreateDeleteButsuriSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYHyojunEntity.PREFIX_KEY.RLength) =
                        ABAtenaRirekiFZYHyojunEntity.PREFIX_KEY) Then

                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYHyojunEntity.PREFIX_KEY.RLength),
                                        DataRowVersion.Original).ToString()
                Else
                    '�����Ȃ�
                End If
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL,
            '                                                                             m_cfDelButuriUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass)

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

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �_���폜�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           �_��DELETE�p��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateDeleteRonriSQL"
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE��`
        Dim csDelRonriParam As StringBuilder                '�_���폜�p�����[�^��`

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KEY_KOSHINCOUNTER)


            ' �_��DELETE SQL���̍쐬
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYHyojunEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where���̒ǉ�
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_RIREKINO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

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

    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �����폜�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           ����DELETE�p��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateDeleteButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE��`

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYHyojunEntity.KEY_KOSHINCOUNTER)

            ' ����DELETE SQL���̍쐬
            m_strDelButuriSQL = "DELETE FROM " + ABAtenaRirekiFZYHyojunEntity.TABLE_NAME + csWhere.ToString

            ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_RIREKINO
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYHyojunEntity.KEY_KOSHINCOUNTER
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

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

    End Sub
#End Region

#Region "�X�V�����ݒ�"
    '************************************************************************************************
    '* ���\�b�h��     �X�V�����ݒ�
    '* 
    '* �\��           Private Sub SetUpdateDatetime(ByRef csDate As Object)
    '* 
    '* �@�\           ���ݒ�̂Ƃ��X�V������ݒ肷��
    '* 
    '* ����           csDate As Object : �X�V�����̍���
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetUpdateDatetime(ByRef csDate As Object)
        Try
            '���ݒ�̂Ƃ�
            If ((IsDBNull(csDate)) OrElse (CType(csDate, String).Trim.Equals(String.Empty))) Then
                csDate = m_strUpdateDatetime
            Else
                '�����Ȃ�
            End If
        Catch
            Throw
        End Try
    End Sub
#End Region

#End Region

End Class
