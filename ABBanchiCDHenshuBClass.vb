'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �Ԓn�R�[�h�ҏW�a�N���X(ABBanchiCDHenshuBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2009/04/07  �H���@�����R
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
Imports System.Security

Public Class ABBanchiCDHenshuBClass

#Region "�����o�ϐ�"
    '�����o�ϐ��̒�`
    Private m_cfUFLogClass As UFLogClass                            ' ���O�o�̓N���X
    Private m_cfUFControlData As UFControlData                      ' �R���g���[���f�[�^
    Private m_cfUFConfigDataClass As UFConfigDataClass              ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                              ' �q�c�a�N���X
    Private m_crBanchiCdMstB As URBANCHICDMSTBClass                 ' �t�q�Ԓn�R�[�h�}�X�^�N���X
    Private m_cfErrorClass As UFErrorClass                          ' �G���[�����N���X

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABBanchiCDHenshuBClass"
    Private Const THIS_BUSINESSID As String = "AB"


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
    <SecuritySafeCritical>
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass)
        ' �����o�ϐ��Z�b�g
        m_cfUFControlData = cfControlData
        m_cfUFConfigDataClass = cfConfigDataClass
        m_cfRdbClass = New UFRdbClass(m_cfUFControlData.m_strBusinessId)

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(m_cfUFConfigDataClass, m_cfUFControlData.m_strBusinessId)

        ' �t�q�Ԓn�R�[�h�}�X�^�N���X�̃C���X�^���X��
        If (m_crBanchiCdMstB Is Nothing) Then
            m_crBanchiCdMstB = New URBANCHICDMSTBClass(cfControlData, cfConfigDataClass, m_cfRdbClass)
        End If

    End Sub
#End Region

#Region "���\�b�h"

#Region "CreateBanchiCD:�Ԓn�R�[�h�ҏW"
    '**********************************************************************************************************************
    '* ���\�b�h��     �Ԓn�R�[�h�ҏW
    '* 
    '* �\��           Public Function CreateBanchiCD(ByVal strBanchi As String) As String()
    '* 
    '* �@�\           �Ԓn����Ԓn�R�[�h�P�`�R��ҏW����
    '* 
    '* ����           strBanchi     �Ԓn
    '*
    '* �߂�l         String()      �ҏW�����Ԓn�R�[�h�z��
    '*
    '**********************************************************************************************************************
    <SecuritySafeCritical>
    Public Function CreateBanchiCD(ByVal strBanchi As String) As String()
        Dim THIS_METHOD_NAME As String = "CreateBanchiCD"
        Dim strBanchiCD(2) As String                        ' �Ԓn�R�[�h�z��i�擾�p�j
        Dim strRetBanchiCD(2) As String                     ' �Ԓn�R�[�h�z��i�߂�l�p�j
        Dim strMotoBanchiCD() As String                     ' �ύX�O�Ԓn�R�[�h
        Dim intLoop As Integer                              ' ���[�v�J�E���^

        Try

            ' �Ԓn�R�[�h�擾
            strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(strBanchi, strMotoBanchiCD, True)

            For intLoop = 0 To strBanchiCD.Length - 1
                If (IsNothing(strBanchiCD(intLoop))) Then
                    ' �擾�����Ԓn�R�[�h�z���Nothing������ꍇ��String.Empty���Z�b�g
                    strBanchiCD(intLoop) = String.Empty
                End If

                '�Ԓn�R�[�h���E�l����i5���ɖ����Ȃ��ꍇ�͔��p�X�y�[�X�����l�j
                strRetBanchiCD(intLoop) = strBanchiCD(intLoop).Trim.RPadLeft(5, " "c)
            Next

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            Throw
        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            Throw
        End Try

        Return strRetBanchiCD

    End Function
#End Region

#End Region

End Class
