'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         ���k�s�`�w�Ŗڋ敪�}�X�^(ABLTTaxKBBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t             2008/11/25
'*
'* �쐬��           ��Á@�v��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2009/07/16   000001     �Ŗڋ敪�}�X�^�Ɩ��R�[�h�w��擾���\�b�h��ǉ��i��Áj
'* 2010/04/16   000002     VS2008�Ή��i��Áj
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools

Public Class ABLTTaxKBBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X

    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABLTTaxKBBClass"

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfControlData As UFControlData, 
    '*                                ByVal cfConfigDataClass As UFConfigDataClass, 
    '*                                ByVal cfRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                 cfConfigData As UFConfigDataClass      : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '*                 cfRdbClass As UFRdbClass               : �q�c�a�f�[�^�I�u�W�F�N�g
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

        ' SQL���̍쐬
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABLTTaxKBEntity.TABLE_NAME, ABLTTaxKBEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �Ŗڋ敪�}�X�^�擾
    '* 
    '* �\��           Public Overloads Function GetLTTaxKB() As DataSet
    '* 
    '* �@�\�@�@    �@ �Ŗڋ敪�}�X�^���S���f�[�^���擾����B
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �擾�����Ŗڋ敪�}�X�^�̊Y���f�[�^�iDataSet�j
    '************************************************************************************************
    Public Overloads Function GetLTTaxKB() As DataSet
        Const THIS_METHOD_NAME As String = "GetLTTaxKB"
        Dim csLTTaxKBEntity As DataSet                                      ' �Ŗڋ敪�}�X�^�f�[�^
        Dim strSQL As New System.Text.StringBuilder                         ' SQL��������
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim cfUFParameterClass As UFParameterClass                          ' �p�����[�^�N���X
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABLTTaxKBEntity.TABLE_NAME)
            ' ORDER������
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABLTTaxKBEntity.TAXKB)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csLTTaxKBEntity = m_csDataSchma.Clone()
            csLTTaxKBEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLTTaxKBEntity, ABLTTaxKBEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return csLTTaxKBEntity

    End Function

    '*����ԍ� 000001 2009/07/16 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �Ŗڋ敪�}�X�^�擾
    '* 
    '* �\��           Public Overloads Function GetLTTaxKB(ByVal strGyomuCD() As String) As DataSet
    '* 
    '* �@�\�@�@    �@ �Ŗڋ敪�}�X�^���S���f�[�^���擾����B
    '* 
    '* ����           strGyomuCD() As String        :�Ɩ��R�[�h�z��
    '* 
    '* �߂�l         �擾�����Ŗڋ敪�}�X�^�̊Y���f�[�^�iDataSet�j
    '************************************************************************************************
    Public Overloads Function GetLTTaxKB(ByVal strGyomuCD() As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTTaxKB"
        Dim csLTTaxKBEntity As DataSet                                      ' �Ŗڋ敪�}�X�^�f�[�^
        Dim strSQL As New System.Text.StringBuilder                         ' SQL��������
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim cfUFParameterClass As UFParameterClass                          ' �p�����[�^�N���X
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' �p�����[�^�R���N�V�����N���X
        Dim intI As Integer
        Dim strWhere As New System.Text.StringBuilder

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABLTTaxKBEntity.TABLE_NAME)

            ' WHERE��
            If (strGyomuCD.Length > 0) Then
                strSQL.Append(" WHERE ")
                strSQL.Append(ABLTTaxKBEntity.GYOMUCD)
                strSQL.Append(" IN(")

                For intI = 0 To strGyomuCD.Length - 1
                    strSQL.Append("'")
                    strSQL.Append(strGyomuCD(intI))
                    strSQL.Append("',")
                Next
                strSQL.RRemove(strSQL.RLength - 1, 1)
                strSQL.Append(")")

            Else
            End If

            ' ORDER������
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABLTTaxKBEntity.TAXKB)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csLTTaxKBEntity = m_csDataSchma.Clone()
            csLTTaxKBEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLTTaxKBEntity, ABLTTaxKBEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return csLTTaxKBEntity

    End Function
    '*����ԍ� 000001 2009/07/16 �ǉ��I��
#End Region

End Class
