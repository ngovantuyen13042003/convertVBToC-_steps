'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �A����}�X�^�c�`(ABRenrakusaki2BClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t��           2007/07/25
'*
'* �쐬�ҁ@�@�@     ��Á@�v��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2007/08/27  000001      �`�F�b�N���@�̌����C��
'* 2010/04/16  000002      VS2008�Ή��i��Áj
'* 2024/01/11  000003     �yAB-0860-1�z�A����Ǘ����ڒǉ�
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Text

Public Class ABRenrakusaki2BClass

#Region "�����o�ϐ�"
    '�����o�ϐ��̒�`
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_strInsertSQL As String                                                'INSERT�pSQL
    Private m_strUpdateSQL As String                                                'UPDATE�pSQL
    Private m_strDeleteSQL As String                                                'DELETE�pSQL�i�����j
    Private m_strDelRonriSQL As String                                              'DELETE�pSQL�i�_���j
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass      'DELETE�p�p�����[�^�R���N�V�����i�����j
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    'DELETE�p�p�����[�^�R���N�V�����i�_���j
    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABRenrakusaki2BClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h
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
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        '�����o�ϐ��̏�����
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing

        ' SQL���̍쐬
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�A����}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String  :�Z���R�[�h
    '* 
    '* �߂�l         �擾�����A����}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsRenrakusakiEntity    �C���e���Z���X�FABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       '���̃��\�b�h��
        Dim csRenrakusakiEntity As DataSet                              '�A����}�X�^�f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            ' ORDER������
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)
            strSQL.Append(" ASC")

            strSQL.Append(" , ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)
            strSQL.Append(" ASC")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@�A����}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String    :�Z���R�[�h
    '*                blnSakujoFG As Boolean  :�폜�t���O
    '* 
    '* �߂�l         �擾�����A����}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsRenrakusakiEntity    �C���e���Z���X�FABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       '���̃��\�b�h��
        Dim csRenrakusakiEntity As DataSet                              '�A����}�X�^�f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABRenrakusakiEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            ' ORDER������
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)
            strSQL.Append(" ASC")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�A����}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '* 
    '* �߂�l         �擾�����A����}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsRenrakusakiEntity    �C���e���Z���X�FABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String, ByVal strGyomunaiSHUCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       '���̃��\�b�h��
        Dim csRenrakusakiEntity As DataSet                              '�A����}�X�^�f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim blnSakujo As Boolean = True                                 '�폜�f�[�^�ǂݍ���

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)          '�Z���R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)          '�Ɩ��R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)   '�Ɩ�����ʃR�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            
            ' SQL�̎��s DataSet�̎擾
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function

    '*����ԍ� 000003 2024/01/11 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD, 
    '*                                                        ByVal intTorokuRenban As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�A����}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                strTorokuRenban As String     :�o�^�A��
    '* 
    '* �߂�l         �擾�����A����}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsRenrakusakiEntity    �C���e���Z���X�FABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String, ByVal strGyomunaiSHUCD As String, ByVal strTorokuRenban As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       '���̃��\�b�h��
        Dim csRenrakusakiEntity As DataSet                              '�A����}�X�^�f�[�^
        Dim strSQL As New StringBuilder()                               'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim blnSakujo As Boolean = True                                 '�폜�f�[�^�ǂݍ���

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)          '�Z���R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)          '�Ɩ��R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)   '�Ɩ�����ʃR�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.TOROKURENBAN)     '�o�^�A��
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_TOROKURENBAN)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �o�^�A��
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_TOROKURENBAN
            cfUFParameterClass.Value = strTorokuRenban
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function
    '*����ԍ� 000003 2024/01/11 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@�A����}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                blnSakujoFG As Boolean        :�폜�t���O
    '* 
    '* �߂�l         �擾�����A����}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsRenrakusakiEntity    �C���e���Z���X�FABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String, ByVal strGyomunaiSHUCD As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       '���̃��\�b�h��
        Dim csRenrakusakiEntity As DataSet                              '�A����}�X�^�f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim blnSakujo As Boolean = True                                 '�폜�f�[�^�ǂݍ���

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)          '�Z���R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)          '�Ɩ��R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)   '�Ɩ�����ʃR�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABRenrakusakiEntity.SAKUJOFG)     '�폜�t���O
                strSQL.Append(" <> 1")
            End If

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            
            ' SQL�̎��s DataSet�̎擾
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function

    '*����ԍ� 000003 2024/01/11 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal strTorokuRenban As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@�A����}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                strTorokuRenban As String     :�o�^�A��
    '*                blnSakujoFG As Boolean        :�폜�t���O
    '* 
    '* �߂�l         �擾�����A����}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsRenrakusakiEntity    �C���e���Z���X�FABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String, ByVal strGyomunaiSHUCD As String, ByVal strTorokuRenban As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu"       '���̃��\�b�h��
        Dim csRenrakusakiEntity As DataSet                              '�A����}�X�^�f�[�^
        Dim strSQL As New StringBuilder()                               'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim blnSakujo As Boolean = True                                 '�폜�f�[�^�ǂݍ���

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiEntity.JUMINCD)          '�Z���R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUCD)          '�Ɩ��R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.GYOMUNAISHU_CD)   '�Ɩ�����ʃR�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD)
            strSQL.Append(" AND ")
            strSQL.Append(ABRenrakusakiEntity.TOROKURENBAN)     '�o�^�A��
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiEntity.KEY_TOROKURENBAN)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABRenrakusakiEntity.SAKUJOFG)     '�폜�t���O
                strSQL.Append(" <> 1")
            End If

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �o�^�A��
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_TOROKURENBAN
            cfUFParameterClass.Value = strTorokuRenban
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csRenrakusakiEntity

    End Function
    '*����ԍ� 000003 2024/01/11 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertRenrakusakiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �A����}�X�^�Ƀf�[�^��ǉ�����B
    '* 
    '* ����           csDataRow As DataRow  :�ǉ��f�[�^
    '* 
    '* �߂�l         �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertRenrakusakiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertRenrakusakiB"         '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer                                        '�ǉ�����
        Dim strUpdateDateTime As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '�쐬����

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABRenrakusakiEntity.TANMATSUID) = m_cfControlData.m_strClientId               '�[���h�c
            csDataRow(ABRenrakusakiEntity.SAKUJOFG) = "0"                                           '�폜�t���O
            csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) = Decimal.Zero                             '�X�V�J�E���^
            csDataRow(ABRenrakusakiEntity.SAKUSEINICHIJI) = strUpdateDateTime                       '�쐬����
            csDataRow(ABRenrakusakiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                '�쐬���[�U�[
            csDataRow(ABRenrakusakiEntity.KOSHINNICHIJI) = strUpdateDateTime                        '�X�V����
            csDataRow(ABRenrakusakiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                 '�X�V���[�U�[

            ' ���N���X�̃f�[�^�������`�F�b�N���s��
            For Each csDataColumn In csDataRow.Table.Columns
                ' �f�[�^�������`�F�b�N
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")
            
            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateRenrakusakiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �A����}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           csDataRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateRenrakusakiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateRenrakusakiB"         '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim intUpdCnt As Integer                                        '�X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABRenrakusakiEntity.TANMATSUID) = m_cfControlData.m_strClientId '�[���h�c
            csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER)) + 1   '�X�V�J�E���^
            csDataRow(ABRenrakusakiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '�X�V����
            csDataRow(ABRenrakusakiEntity.KOSHINUSER) = m_cfControlData.m_strUserId   '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABRenrakusakiEntity.PREFIX_KEY.RLength) = ABRenrakusakiEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")
            
            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^�폜�i�_���j
    '* 
    '* �\��           Public Function DeleteRenrakusakiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �A����}�X�^�̃f�[�^���폜�i�_���j����B
    '* 
    '* ����           csDataRow As DataRow  :�폜�f�[�^
    '* 
    '* �߂�l         �폜�i�_���j����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteRenrakusakiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteRenrakusakiB�i�_���j"  '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim intDelCnt As Integer                                        '�폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or _
                m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABRenrakusakiEntity.TANMATSUID) = m_cfControlData.m_strClientId '�[���h�c
            csDataRow(ABRenrakusakiEntity.SAKUJOFG) = 1                                 '�폜�t���O
            csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABRenrakusakiEntity.KOSHINCOUNTER)) + 1   '�X�V�J�E���^
            csDataRow(ABRenrakusakiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '�X�V����
            csDataRow(ABRenrakusakiEntity.KOSHINUSER) = m_cfControlData.m_strUserId   '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABRenrakusakiEntity.PREFIX_KEY.RLength) = ABRenrakusakiEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")
            
            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^�폜�i�����j
    '* 
    '* �\��           Public Overloads Function DeleteRenrakusakiB(ByVal csDataRow As DataRow, 
    '*                                                      ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@  �A����}�X�^�̃f�[�^���폜�i�����j����B
    '* 
    '* ����           csDataRow As DataRow      :�폜�f�[�^
    '*                strSakujoKB As String     :�폜�t���O
    '* 
    '* �߂�l         �폜�i�����j����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteRenrakusakiB(ByVal csDataRow As DataRow, ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteRenrakusakiB�i�����j"  '���̃��\�b�h��
        Dim objErrorStruct As UFErrorStruct                             '�G���[��`�\����
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim intDelCnt As Integer                                        '�폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �����̍폜�敪���`�F�b�N
            If (strSakujoKB <> "D") Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_DELETE_SAKUJOKB)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDeleteSQL Is Nothing Or m_strDeleteSQL = String.Empty Or _
                m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABRenrakusakiEntity.PREFIX_KEY.RLength) = ABRenrakusakiEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABRenrakusakiEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     SQL���̍쐬
    '* 
    '* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\�@�@    �@ INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL"              '���̃��\�b�h��
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
        Dim strInsertColumn As String                               '�ǉ�SQL�����ڕ�����
        Dim strInsertParam As String                                '�ǉ�SQL���p�����[�^������
        Dim strDelRonriSQL As New StringBuilder                     '�_���폜SQL��������
        Dim strDeleteSQL As New StringBuilder                       '�����폜SQL��������
        Dim strWhere As New StringBuilder                           '�X�V�폜SQL��Where��������

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABRenrakusakiEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' �X�V�폜Where���쐬
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
            '*����ԍ� 000003 2024/01/11 �ǉ��J�n
            strWhere.Append(" AND ")
            strWhere.Append(ABRenrakusakiEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABRenrakusakiEntity.KEY_TOROKURENBAN)
            '*����ԍ� 000003 2024/01/11 �ǉ��I��

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABRenrakusakiEntity.TABLE_NAME + " SET "

            ' DELETE�i�_���j SQL���̍쐬
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

            ' DELETE�i�����j SQL���̍쐬
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABRenrakusakiEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE�i�_���j �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE�i�����j �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL���̍쐬
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            ' INSERT SQL���̃g���~���O
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))
            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += strWhere.ToString

            ' UPDATE,DELETE(����) �R���N�V�����ɃL�[����ǉ�
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000003 2024/01/11 �ǉ��J�n
            ' �o�^�A��
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_TOROKURENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000003 2024/01/11 �ǉ��I��

            ' DELETE�i�_���j �R���N�V�����Ƀp�����[�^��ǉ�
            ' �[���h�c
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �폜�t���O
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V����
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V���[�U
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000003 2024/01/11 �ǉ��J�n
            ' �o�^�A��
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_TOROKURENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000003 2024/01/11 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* �@�\�@�@       �A����}�X�^�̃f�[�^�������`�F�b�N���s���܂��B
    '* 
    '* ����           strColumnName As String
    '*                strValue As String
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"       '���̃��\�b�h��
        Dim objErrorStruct As UFErrorStruct                         '�G���[��`�\����

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strColumnName.ToUpper()
                Case ABRenrakusakiEntity.JUMINCD                        '�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.SHICHOSONCD                    '�s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.KYUSHICHOSONCD                 '���s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.GYOMUCD                        '�Ɩ��R�[�h
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_GYOMUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.GYOMUNAISHU_CD                 '�Ɩ�����ʃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_GYOMUNAISHU_CD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKIKB                  '�A����敪
                    '* ����ԍ� 000001 2007/08/27 �C���J�n
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        'If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000001 2007/08/27 �C���I��
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKIMEI                 '�A���於
                    '* ����ԍ� 000001 2007/08/27 �C���J�n
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        'If (Not UFStringClass.CheckNumber(strValue, m_cfConfigDataClass)) Then
                        '* ����ԍ� 000001 2007/08/27 �C���I��
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKIMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKI1                   '�A����1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKI2                   '�A����2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RENRAKUSAKI3                   '�A����3
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_RENRAKUSAKI3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.RESERVE                        '���U�[�u
                    '�������Ȃ�
                Case ABRenrakusakiEntity.TANMATSUID                     '�[��ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.SAKUJOFG                       '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.KOSHINCOUNTER                  '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.SAKUSEINICHIJI                 '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.SAKUSEIUSER                    '�쐬���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.KOSHINNICHIJI                  '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABRenrakusakiEntity.KOSHINUSER                     '�X�V���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABRENRAKUSAKIB_RDBDATATYPE_KOSHINUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
            End Select

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException
        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException
        End Try
    End Sub
#End Region

End Class
