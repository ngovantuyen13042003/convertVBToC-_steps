'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �R�[�h�}�X�^�擾(ABCodeMSTBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t             2023/10/31
'*
'* �쐬��           �����@���]
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2023/10/31             �yAB-0880-1�z�l������ڍ׊Ǘ����ڒǉ�
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools

Public Class ABCodeMSTBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X

    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABCodeMSTBClass"

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfControlData As UFControlData, 
    '*                              �@ByVal cfConfigData As UFConfigDataClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                 cfConfigData As UFConfigDataClass      : �R���t�B�O�f�[�^�I�u�W�F�N�g 
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

        ' SQL���̍쐬
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABCodeMSTEntity.TABLE_NAME, ABCodeMSTEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �R�[�h�}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetCodeMst() As DataSet
    '* 
    '* �@�\�@�@    �@ �R�[�h�}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �R�[�h�}�X�^�f�[�^(�S��)�iDataSet�j
    '************************************************************************************************
    Public Overloads Function GetCodeMst() As DataSet
        Const THIS_METHOD_NAME As String = "GetCodeMst"             ' ���̃��\�b�h��
        Dim csDataSet As DataSet                                    ' �R�[�h�}�X�^
        Dim strSQL As New System.Text.StringBuilder                 ' SQL��������

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABCodeMSTEntity.TABLE_NAME)
            ' ORDER������
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABCodeMSTEntity.SHUBETSU)
            strSQL.Append(",")
            strSQL.Append(ABCodeMSTEntity.CODE)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABCodeMSTEntity.TABLE_NAME, False)

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
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw
        End Try

        Return csDataSet

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �R�[�h�}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetCodeMst(ByVal strShubetsu As String) As DataSet
    '* 
    '* �@�\�@�@    �@ ��ʃR�[�h���Y���f�[�^���擾����B
    '* 
    '* ����          strShubetsu As String     :���
    '* 
    '* �߂�l         �擾�����R�[�h�}�X�^�iDataSet�j
    '************************************************************************************************
    Public Overloads Function GetCodeMst(ByVal strShubetsu As String) As DataSet
        Return Me.GetCodeMst(strShubetsu, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     �R�[�h�}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetCodeMst(ByVal strShubetsu As String, 
    '*                                                     ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@ ��ʃR�[�h���Y���f�[�^���擾����
    '* 
    '* ����           strShubetsu As String     :��ʃR�[�h
    '*                blnSakujoFG As Boolean    :�폜�t���O
    '* 
    '* �߂�l         �擾�����R�[�h�}�X�^�iDataSet�j
    '************************************************************************************************
    Public Overloads Function GetCodeMst(ByVal strShubetsu As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetCodeMst"             ' ���̃��\�b�h��
        Dim csDataset As DataSet                                    ' �R�[�h�}�X�^�f�[�^
        Dim strSQL As New System.Text.StringBuilder                         ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                          ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABCodeMSTEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABCodeMSTEntity.SHUBETSU)
            strSQL.Append(" = ")
            strSQL.Append(ABCodeMSTEntity.KEY_SHUBETSU)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABCodeMSTEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            ' ORDER������
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABCodeMSTEntity.HYOJIJUN)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABCodeMSTEntity.KEY_SHUBETSU
            cfUFParameterClass.Value = strShubetsu

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)


            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDataset = m_csDataSchma.Clone()
            csDataset = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataset, ABCodeMSTEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw
        End Try

        Return csDataset

    End Function
#End Region

End Class
