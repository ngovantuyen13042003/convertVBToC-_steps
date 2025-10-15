'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�����t���}�X�^�e�X�g�p
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2011/10/24�@�����@�m��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
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

Public Class T_ABAtenaFZYBClass
#Region "�����o�ϐ�"
    '�p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X

    '�@�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "T_ABAtenaFZYBClass"                ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h

    Public m_blnSelectAll As ABEnumDefine.AtenaGetKB = ABEnumDefine.AtenaGetKB.SelectAll '�S���ڑI���im_blnAtenaGet��True�̎�����Get�ŕK�v�ȍ��ڑS�Ă���ȊO��SELECT *�j
    Public m_strJukihoKaiseiKB As String = String.Empty

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
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

    End Sub
#End Region

#Region "���\�b�h"
    Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
                                              ByVal cSearchKey As ABAtenaSearchKey, _
                                              ByVal strKikanYMD As String, _
                                              ByVal strJuminJutogaiKB As String, _
                                              ByVal blnSakujoFG As Boolean) As DataSet
        Dim csRetDs As DataSet
        Try
            Dim csAtenaB As New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            csAtenaB.m_blnSelectAll = Me.m_blnSelectAll
            csAtenaB.p_strJukihoKaiseiKB = Me.m_strJukihoKaiseiKB
            csRetDs = csAtenaB.GetAtenaRBHoshu(intGetCount, cSearchKey, strKikanYMD, strJuminJutogaiKB, blnSakujoFG)

        Catch
            Throw
        End Try

        Return csRetDs

    End Function

    Public Function GetAtenaBKobetsu(ByVal intGetCount As Integer, _
                                     ByVal cSearchKey As ABAtenaSearchKey, _
                                     ByVal blnSakujoFG As Boolean, _
                                     ByVal strKobetsuKB As String) As DataSet
        Dim csRetDs As DataSet
        Try
            Dim csAtenaB As New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            csAtenaB.m_blnSelectAll = Me.m_blnSelectAll
            csAtenaB.p_strJukihoKaiseiKB = Me.m_strJukihoKaiseiKB
            csRetDs = csAtenaB.GetAtenaBKobetsu(intGetCount, cSearchKey, blnSakujoFG, strKobetsuKB)

        Catch
            Throw
        End Try

        Return csRetDs

    End Function


    Public Function GetAtenaRBKobetsu(ByVal intGetCount As Integer, _
                                      ByVal cSearchKey As ABAtenaSearchKey, _
                                      ByVal strKikanYMD As String, _
                                      ByVal blnSakujoFG As Boolean, _
                                      ByVal strKobetsuKB As String) As DataSet

        Dim csRetDs As DataSet
        Try
            Dim csAtenaB As New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            csAtenaB.m_blnSelectAll = Me.m_blnSelectAll
            csAtenaB.p_strJukihoKaiseiKB = Me.m_strJukihoKaiseiKB
            csRetDs = csAtenaB.GetAtenaRBKobetsu(intGetCount, cSearchKey, strKikanYMD, blnSakujoFG, strKobetsuKB)

        Catch
            Throw
        End Try

        Return csRetDs

    End Function

#End Region

End Class
