'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �`�a�����Z��o�b�`�X�V(ABBatchJukiKoshinBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t��           2009/05/12�@
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
Imports Densan.Common
Imports System.Data
Imports System.Text

Public Class ABBatchJukiKoshinBClass
    Inherits ABJukiKoshinBClass           ' �Z��X�V�a�N���X���p��

    '************************************************************************************************
    '* ���\�b�h��     �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass,
    '*                               ByVal cfRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@       ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* �@�@           cfRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        MyBase.New(cfControlData, cfConfigDataClass, cfRdbClass)
        m_blnBatch = True

    End Sub
End Class
