'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �Ԓn�R�[�h�ҏW�a�N���X(ABBanchiEdabanSuchiBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2023/08/14  ���� �Y��
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

Public Class ABBanchiEdabanSuchiBClass

#Region "�����o�ϐ�"
    '�����o�ϐ��̒�`
    Private m_cfUFLogClass As UFLogClass                            ' ���O�o�̓N���X
    Private m_cfUFControlData As UFControlData                      ' �R���g���[���f�[�^
    Private m_cfUFConfigDataClass As UFConfigDataClass              ' �R���t�B�O�f�[�^

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABBanchiEdabanSuchiBClass"

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass) 
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    <SecuritySafeCritical>
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass)

        ' �����o�ϐ��Z�b�g
        m_cfUFControlData = cfControlData
        m_cfUFConfigDataClass = cfConfigDataClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(m_cfUFConfigDataClass, m_cfUFControlData.m_strBusinessId)

    End Sub
#End Region

#Region "���\�b�h"
    '**********************************************************************************************************************
    '* ���\�b�h��     �Ԓn�R�[�h�ҏW
    '* 
    '* �\��           Public Function GetBanchiEdabanSuchi(ByVal strBanchiCD1 As String, ByVal strBanchiCD2 As String, _
    '*                                                     ByVal strBanchiCD3 As String) As String
    '* 
    '* �@�\           �Ԓn����Ԓn�R�[�h�P�`�R��ҏW����
    '* 
    '* ����           strBanchiCD1 As String : �Ԓn�R�[�h�P
    '*                strBanchiCD2 As String : �Ԓn�R�[�h�Q
    '*                strBanchiCD3 As String : �Ԓn�R�[�h�R
    '*
    '* �߂�l         String      �ҏW�����Ԓn�R�[�h
    '*
    '**********************************************************************************************************************
    <SecuritySafeCritical>
    Public Function GetBanchiEdabanSuchi(ByVal strBanchiCD1 As String, ByVal strBanchiCD2 As String,
                                         ByVal strBanchiCD3 As String) As String
        Dim THIS_METHOD_NAME As String = "GetBanchiEdabanSuchi"
        Dim strAfterBanchiCD1 As String
        Dim strAfterBanchiCD2 As String
        Dim strAfterBanchiCD3 As String

        Try

            strAfterBanchiCD1 = GetBanchiCDChange(strBanchiCD1)
            strAfterBanchiCD2 = GetBanchiCDChange(strBanchiCD2)
            strAfterBanchiCD3 = GetBanchiCDChange(strBanchiCD3)

            '�A�����Ė߂�l�Ƃ���
            GetBanchiEdabanSuchi = strAfterBanchiCD1 & strAfterBanchiCD2 & strAfterBanchiCD3

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

        Return GetBanchiEdabanSuchi

    End Function

    '**********************************************************************************************************************
    '* ���\�b�h��     �Ԓn�R�[�h�ϊ�(5��)
    '* 
    '* �\��           Public Function GetBanchiCDChange(ByVal strBanchiCD As String) As String
    '* 
    '* �@�\           �Ԓn�R�[�h�ɐ��l�ȊO�����݂����ꍇ�A�ȍ~���O���߂���(5��)
    '* 
    '* ����           strBanchiCD As String : �Ԓn�R�[�h
    '*
    '* �߂�l         String      �ҏW�����Ԓn�R�[�h
    '*
    '**********************************************************************************************************************
    Public Function GetBanchiCDChange(ByVal strBanchiCD As String) As String
        Dim THIS_METHOD_NAME As String = "GetBanchiCDChange"
        Dim strBanchiData As String
        Dim strBanchiCDAfter As String = String.Empty

        '�Ԓn�R�[�h���󔒂̏ꍇ
        If (strBanchiCD.Trim IsNot String.Empty) Then
            '�Ԓn�R�[�h�ɐ��l�ȊO���܂܂��ꍇ
            If Not IsNumeric(strBanchiCD) Then
                '�ꕶ���Â`�F�b�N���s���A���l�ȊO�����݂���ꍇ�A�ȍ~0���߂���(5��)
                For Each strBanchiData In strBanchiCD
                    If IsNumeric(strBanchiData) Then
                        strBanchiCDAfter = strBanchiCDAfter & strBanchiData

                    ElseIf strBanchiData = " " Then
                        strBanchiCDAfter = strBanchiCDAfter & "0"

                    Else
                        strBanchiCDAfter = strBanchiCDAfter.PadRight(5, "0"c)
                        Exit For
                    End If
                Next
            ElseIf (strBanchiCD.Trim.Length < 5) Then
                '���l�̂�5���ȉ��̏ꍇ�A�O0��5�����߂�
                strBanchiCDAfter = strBanchiCD.Trim.PadLeft(5, "0"c)
            ElseIf (strBanchiCD.Trim.Length = 5) Then
                '���l�̂�5���̏ꍇ�A���̂܂ܕԂ�
                strBanchiCDAfter = strBanchiCD
            End If
        Else
            strBanchiCDAfter = String.Empty.PadLeft(5, "0"c)
        End If

        Return strBanchiCDAfter

    End Function
#End Region

End Class
