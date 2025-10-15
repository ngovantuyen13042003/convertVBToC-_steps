'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �A����}�X�^�c�`(ABRenrakusakiBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/14�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/17 000001     �ǉ����A���ʍ��ڂ�ݒ肷��
'* 2003/05/21 000002     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/28 000003     RDB�A�N�Z�X���O�̏C��
'* 2004/08/27 000004     ���x���P�F�i�{��j
'* 2010/04/16 000005     VS2008�Ή��i��Áj
'* 2023/07/13 000006     �yAB-0970-1�z����GET�擾���ڕW�����Ή��i�����j
'* 2024/02/06 000007     �yAB-0860-1�z�A����Ǘ����ڒǉ�(����)
'* 2024/03/07 000008     �yAB-0900-1�z�A�h���X�E�x�[�X�E���W�X�g���Ή�(����)
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

Public Class ABRenrakusakiBClass
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
    '* ����ԍ� 000004 2004/08/27 �ǉ��J�n�i�{��j
    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    '* ����ԍ� 000004 2004/08/27 �ǉ��I��
    '*����ԍ� 000006 2023/07/13 �ǉ��J�n
    Private m_csDataSchma_Hyojun As DataSet   '�W�����ŃX�L�[�}�ۊǗp�f�[�^�Z�b�g
    '*����ԍ� 000006 2023/07/13 �ǉ��I��

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABRenrakusakiBClass"
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
        '* ����ԍ� 000004 2004/08/27 �ǉ��J�n�i�{��j
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TABLE_NAME, False)
        '* ����ԍ� 000004 2004/08/27 �ǉ��I��
        '*����ԍ� 000006 2023/07/13 �ǉ��J�n
        m_csDataSchma_Hyojun = Me.GetRenrakusakiSchemaBHoshu_Hyojun()
        '*����ԍ� 000006 2023/07/13 �ǉ��I��

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
        Dim strSQL As New StringBuilder()                               'SQL��������
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
            strSQL.Append(" ASC, ")
            strSQL.Append(ABRenrakusakiEntity.TOROKURENBAN)
            strSQL.Append(" ASC")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
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
            '* ����ԍ� 000004 2004/08/27 �X�V�J�n�i�{��j
            'csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '* ����ԍ� 000004 2004/08/27 �X�V�I��
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
        Dim strSQL As New StringBuilder()                               'SQL��������
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
            strSQL.Append(" ASC, ")
            strSQL.Append(ABRenrakusakiEntity.TOROKURENBAN)
            strSQL.Append(" ASC")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000003 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '*����ԍ� 000003 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾

            '* ����ԍ� 000004 2004/08/27 �X�V�J�n�i�{��j
            'csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '* ����ԍ� 000004 2004/08/27 �X�V�I��

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

            '*����ԍ� 000003 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '*����ԍ� 000003 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            '* ����ԍ� 000004 2004/08/27 �X�V�J�n�i�{��j
            'csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '* ����ԍ� 000004 2004/08/27 �X�V�I��

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

            '*����ԍ� 000003 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '*����ԍ� 000003 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            '* ����ԍ� 000004 2004/08/27 �X�V�J�n�i�{��j
            'csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csRenrakusakiEntity = m_csDataSchma.Clone()
            csRenrakusakiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiEntity, ABRenrakusakiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '* ����ԍ� 000004 2004/08/27 �X�V�I��

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

            '*����ԍ� 000003 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strInsertSQL + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")
            '*����ԍ� 000003 2003/08/28 �C���I��

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
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000005
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

            '*����ԍ� 000003 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strUpdateSQL + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")
            '*����ԍ� 000003 2003/08/28 �C���I��

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
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000005
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

            '*����ԍ� 000003 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strDelRonriSQL + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")
            '*����ԍ� 000003 2003/08/28 �C���I��

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
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000005
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

            '*����ԍ� 000003 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strDeleteSQL + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "�z")
            '*����ԍ� 000003 2003/08/28 �C���I��

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
    '* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
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
        Dim strDelRonriSQL As New StringBuilder()                   '�_���폜SQL��������
        Dim strDeleteSQL As New StringBuilder()                     '�����폜SQL��������
        Dim strWhere As New StringBuilder()                         '�X�V�폜SQL��Where��������

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
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

            ' DELETE�i�_���j �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

            ' DELETE�i�����j �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass()

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

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
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            ' DELETE�i�_���j �R���N�V�����Ƀp�����[�^��ǉ�
            ' �[���h�c
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �폜�t���O
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V����
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V���[�U
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_GYOMUNAISHU_CD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

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
    End Sub

    '*����ԍ� 000006 2023/07/13 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^���o_�W����
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiBHoshu_Hyojun(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String,
    '*                                                        ByVal strKikanYMD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�A����}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                strKikanYMD As String         :���ԔN����
    '* 
    '* �߂�l         �擾�����A����}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsRenrakusakiEntity    �C���e���Z���X�FABRenrakusakiEntity
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiBHoshu_Hyojun(ByVal strJuminCD As String, ByVal strGyomuCD As String, ByVal strGyomunaiSHUCD As String, ByVal strKikanYMD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiBHoshu_Hyojun" '���̃��\�b�h��
        Dim csRenrakusakiEntity As DataSet                              '�A����}�X�^�f�[�^
        Dim strSQL As New StringBuilder()                               'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim blnSakujo As Boolean = True                                 '�폜�f�[�^�ǂݍ���

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.AppendFormat("SELECT {0}.* ", ABRenrakusakiEntity.TABLE_NAME)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKUYMD)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN)
            strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.BIKO)
            strSQL.AppendFormat(" FROM {0}", ABRenrakusakiEntity.TABLE_NAME)

            ' JOIN������
            strSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME)
            strSQL.AppendFormat(" ON {0}.{1} = {2}.{3}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD,
                                ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.JUMINCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD,
                                ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD,
                                ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN,
                                ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKURENBAN)
            strSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYEntity.TABLE_NAME)
            strSQL.AppendFormat(" ON {0}.{1} = {2}.{3}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD,
                                ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.JUMINCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD,
                                ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD,
                                ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUNAISHU_CD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN,
                                ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TOROKURENBAN)

            ' WHERE������
            strSQL.AppendFormat(" WHERE {0}.{1} = {2}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD, ABRenrakusakiFZYHyojunEntity.KEY_JUMINCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}",
                                ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD, ABRenrakusakiFZYHyojunEntity.KEY_GYOMUNAISHU_CD)
            strSQL.AppendFormat(" AND {0}.{1} <= {2}",
                                ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD, ABRenrakusakiFZYHyojunEntity.KEY_RENRAKUSAKI_STYMD)
            strSQL.AppendFormat(" AND {0}.{1} >= {2}",
                                ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD, ABRenrakusakiFZYHyojunEntity.KEY_RENRAKUSAKI_EDYMD)

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

            ' �A����J�n��
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_RENRAKUSAKI_STYMD
            cfUFParameterClass.Value = strKikanYMD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �A����I����
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABRenrakusakiFZYHyojunEntity.KEY_RENRAKUSAKI_EDYMD
            cfUFParameterClass.Value = strKikanYMD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            csRenrakusakiEntity = m_csDataSchma_Hyojun.Clone()
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

    '************************************************************************************************
    '* ���\�b�h��     �A����}�X�^�X�L�[�}�擾_�W����
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiSchemaBHoshu_Hyojun() As DataSet
    '* 
    '* �@�\�@�@    �@ �A����}�X�^���X�L�[�}���擾����B
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataSet : �擾�������t��}�X�^�̃X�L�[�}
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiSchemaBHoshu_Hyojun() As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiSchemaBHoshu_Hyojun" '���̃��\�b�h��
        Dim csRenrakusakiEntity As DataSet                              '�A����}�X�^�f�[�^
        Dim strSQL As New StringBuilder()                               'SQL��������

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (m_csDataSchma_Hyojun Is Nothing) Then
                ' SQL���̍쐬
                strSQL.AppendFormat("SELECT {0}.* ", ABRenrakusakiEntity.TABLE_NAME)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI1BIKO)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI2BIKO)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI3BIKO)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI4BIKO)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI5BIKO)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.RENRAKUSAKI6BIKO)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_STYMD)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKI_EDYMD)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU4)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU5)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU6)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIYUBINNO)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHOCD)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIJUSHO)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHIKUCHOSONCD)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZACD)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKTODOFUKEN)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHICHOSON)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIMACHIAZA)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIBANCHI)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIKATAGAKI)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKUYMD)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOSEIKUCD)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.RENRAKUSAKIEDABAN)
                strSQL.AppendFormat(", {0}.{1}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.BIKO)
                strSQL.AppendFormat(" FROM {0}", ABRenrakusakiEntity.TABLE_NAME)

                ' JOIN������
                strSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYHyojunEntity.TABLE_NAME)
                strSQL.AppendFormat(" ON {0}.{1} = {2}.{3}",
                                    ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD,
                                    ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.JUMINCD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                    ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD,
                                    ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUCD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                    ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD,
                                    ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.GYOMUNAISHU_CD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                    ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN,
                                    ABRenrakusakiFZYHyojunEntity.TABLE_NAME, ABRenrakusakiFZYHyojunEntity.TOROKURENBAN)
                strSQL.AppendFormat(" LEFT JOIN {0}", ABRenrakusakiFZYEntity.TABLE_NAME)
                strSQL.AppendFormat(" ON {0}.{1} = {2}.{3}",
                                    ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.JUMINCD,
                                    ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.JUMINCD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                    ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUCD,
                                    ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUCD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                    ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.GYOMUNAISHU_CD,
                                    ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.GYOMUNAISHU_CD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3}",
                                    ABRenrakusakiEntity.TABLE_NAME, ABRenrakusakiEntity.TOROKURENBAN,
                                    ABRenrakusakiFZYEntity.TABLE_NAME, ABRenrakusakiFZYEntity.TOROKURENBAN)

                csRenrakusakiEntity = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABRenrakusakiEntity.TABLE_NAME, False)
            Else
                csRenrakusakiEntity = m_csDataSchma_Hyojun.Clone
            End If

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
    '*����ԍ� 000006 2023/07/13 �ǉ��I��
#End Region

End Class