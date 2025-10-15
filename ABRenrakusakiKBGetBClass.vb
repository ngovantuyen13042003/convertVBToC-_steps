'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �A����敪�R�[�h�}�X�^�擾(ABRenrakusakiKBGetBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t             2007/07/26
'*
'* �쐬��           ��Á@�v��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools

Public Class ABRenrakusakiKBGetBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
 
    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABRenrakusakiKBGetBClass"

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
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABRenrakusakiCDMstEntity.TABLE_NAME, ABRenrakusakiCDMstEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �A����敪�R�[�h�}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiCD() As DataSet
    '* 
    '* �@�\�@�@    �@ �A����敪�R�[�h�}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �A����敪�R�[�h�}�X�^�f�[�^(�S��)�iDataSet�j
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiCD() As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiCD"             ' ���̃��\�b�h��
        Dim csRenrakusakiCDEntity As DataSet                                ' �ٓ����R�}�X�^�f�[�^
        Dim strSQL As New System.Text.StringBuilder                         ' SQL��������

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiCDMstEntity.TABLE_NAME)
            ' ORDER������
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csRenrakusakiCDEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABRenrakusakiCDMstEntity.TABLE_NAME, False)

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

        Return csRenrakusakiCDEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �A����敪�R�[�h�}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@ �A����敪�R�[�h���Y���f�[�^���擾����B
    '* 
    '* ����           strRenrakusakiCD As String     :�A����敪
    '* 
    '* �߂�l         �擾�����A����敪�R�[�h�}�X�^�̊Y���f�[�^�iDataSet�j
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiCD"             ' ���̃��\�b�h��
        Dim csRenrakusakiCDEntity As DataSet                                ' �A����敪�R�[�h�}�X�^�f�[�^
        Dim strSQL As New System.Text.StringBuilder                         ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                          ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiCDMstEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiCDMstEntity.KEY_RENRAKUSAKIKB)
            ' ORDER������
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABRenrakusakiCDMstEntity.KEY_RENRAKUSAKIKB
            cfUFParameterClass.Value = strRenrakusakiCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)


            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csRenrakusakiCDEntity = m_csDataSchma.Clone()
            csRenrakusakiCDEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiCDEntity, ABRenrakusakiCDMstEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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

        Return csRenrakusakiCDEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �A����敪�R�[�h�}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String, 
    '*                                                             ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@ �A����敪�R�[�h���Y���f�[�^���擾����B
    '* 
    '* ����           strRenrakusakiCD As String     :�A����敪
    '*                blnSakujoFG As Boolean         :�폜�t���O
    '* 
    '* �߂�l         �擾�����A����敪�R�[�h�}�X�^�̊Y���f�[�^�iDataSet�j
    '************************************************************************************************
    Public Overloads Function GetRenrakusakiCD(ByVal strRenrakusakiCD As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetRenrakusakiCD"             ' ���̃��\�b�h��
        Dim csRenrakusakiCDEntity As DataSet                                ' �A����敪�R�[�h�}�X�^�f�[�^
        Dim strSQL As New System.Text.StringBuilder                         ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                          ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABRenrakusakiCDMstEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)
            strSQL.Append(" = ")
            strSQL.Append(ABRenrakusakiCDMstEntity.KEY_RENRAKUSAKIKB)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABRenrakusakiCDMstEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            ' ORDER������
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABRenrakusakiCDMstEntity.RENRAKUSAKIKB)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABIdoRiyuEntity.KEY_RIYUCD
            cfUFParameterClass.Value = strRenrakusakiCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)


            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csRenrakusakiCDEntity = m_csDataSchma.Clone()
            csRenrakusakiCDEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csRenrakusakiCDEntity, ABRenrakusakiCDMstEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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

        Return csRenrakusakiCDEntity

    End Function
#End Region

End Class
