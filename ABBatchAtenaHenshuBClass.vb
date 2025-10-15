'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�o�b�`�p�����ҏW�N���X(ABBatchAtenaHenshuBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/08/22�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/08/27 000001     ���x���P�F�i�{��j
'* 2005/01/25 000002     ���x���P�Q�F�i�{��j
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
Imports Densan.Common
Imports System.Data
Imports System.Text
Imports System.Security

Public Class ABBatchAtenaHenshuBClass
    Inherits ABAtenaHenshuBClass        ' �����ҏW�a�N���X���p��

    '�p�����[�^�̃����o�ϐ�

    '�@�R���X�^���g��`
    Protected Shadows Const THIS_CLASS_NAME As String = "ABBatchAtenaHenshuBClass"      ' �N���X��

    '* ����ԍ� 000001 2004/08/27 �ǉ��J�n�i�{��j
    Private m_cURKanriJohoB As URKANRIJOHOBClass              '�Ǘ����擾�N���X
    '* ����ԍ� 000001 2004/08/27 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass,
    '*                               ByVal cfUFRdbClass as UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfUFControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfUFConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                cfUFRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfUFControlData As UFControlData,
                   ByVal cfUFConfigDataClass As UFConfigDataClass,
                   ByVal cfUFRdbClass As UFRdbClass)
        MyBase.New(cfUFControlData, cfUFConfigDataClass, cfUFRdbClass)
    End Sub
    '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass,
    '*                               ByVal cfUFRdbClass as UFRdbClass)
    '* �@�@                          ByVal blnSelectAll as boolean)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfUFControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfUFConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                cfUFRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* �@�@           ByVal blnSelectAll As Boolean           : True�̏ꍇ�S���ځAFalse�̏ꍇ�ȈՍ��ڂ̂ݎ擾
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfUFControlData As UFControlData,
                   ByVal cfUFConfigDataClass As UFConfigDataClass,
                   ByVal cfUFRdbClass As UFRdbClass,
                   ByVal blnSelectAll As ABEnumDefine.AtenaGetKB)
        MyBase.New(cfUFControlData, cfUFConfigDataClass, cfUFRdbClass, blnSelectAll)
    End Sub
    '* ����ԍ� 000002 2005/01/25 �ǉ��I���i�{��j

    '************************************************************************************************
    '* ���\�b�h��     ���t��Z���s����ҏW�敪�擾
    '* 
    '* �\��           Private Function GetSofuJushoGyoseikuType() As SofuJushoGyoseikuType
    '* 
    '* �@�\�@�@    �@�@���t��Z���s����ҏW�敪���擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         SofuJushoGyoseikuType
    '************************************************************************************************
    <SecuritySafeCritical>
    Protected Overrides Function GetSofuJushoGyoseikuType() As SofuJushoGyoseikuType
        Const THIS_METHOD_NAME As String = "GetSofuJushoGyoseikuType"
        '* ����ԍ� 000001 2004/08/27 �폜�J�n�i�{��j
        'Dim cURKanriJohoB As URKANRIJOHOBClass              '�Ǘ����擾�N���X
        '* ����ԍ� 000001 2004/08/27 �폜�I��
        Dim cSofuJushoGyoseikuType As SofuJushoGyoseikuType

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�Ǘ����擾�a�̃C���X�^���X�쐬
            '* ����ԍ� 000001 2004/08/27 �X�V�J�n�i�{��j
            'cURKanriJohoB = New URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            If (m_cURKanriJohoB Is Nothing) Then
                m_cURKanriJohoB = New URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            End If
            '* ����ԍ� 000001 2004/08/27 �X�V�I��

            '* ����ԍ� 000002 2005/01/25 �X�V�J�n�i�{��j
            'cSofuJushoGyoseikuType = m_cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param
            If (m_bSofuJushoGyoseikuTypeFlg = False) Then
                m_cSofuJushoGyoseikuType = m_cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param
                m_bSofuJushoGyoseikuTypeFlg = True
            End If
            cSofuJushoGyoseikuType = m_cSofuJushoGyoseikuType
            '* ����ԍ� 000002 2005/01/25 �X�V�I���i�{��j

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp
        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

        Return cSofuJushoGyoseikuType

    End Function

End Class
