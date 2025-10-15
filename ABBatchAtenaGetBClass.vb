'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�o�b�`�p�����擾(ABBatchAtenaGetClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/08/21�@���@�Ԗ�
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

Public Class ABBatchAtenaGetBClass
    Inherits ABAtenaGetBClass           ' �����擾�a�N���X���p��

    ' �p�����[�^�̃����o�ϐ�
    Protected Shadows m_cABAtenaHenshuB As ABBatchAtenaHenshuBClass             ' �����ҏW�N���X(�o�b�`�p)

    ' �R���X�^���g��`
    Protected Shadows Const THIS_CLASS_NAME As String = "ABBatchAtenaGetBClass" ' �N���X��

    '* ����ԍ� 000001 2004/08/27 �ǉ��J�n�i�{��j
    Private m_cfURAtenaKanriJoho As URAtenaKanriJohoBClass    '�����Ǘ����a�N���X
    '* ����ԍ� 000001 2004/08/27 �ǉ��I��


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
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass)
        MyBase.New(cfControlData, cfConfigDataClass)
        m_blnBatch = True
        '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
        If Not (Me.m_cABAtenaBRef Is Nothing) Then
            Me.m_cABAtenaBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABAtenaRirekiBRef Is Nothing) Then
            Me.m_cABAtenaRirekiBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABDainoBRef Is Nothing) Then
            Me.m_cABDainoBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABSfskBRef Is Nothing) Then
            Me.m_cABSfskBRef.m_blnBatch = True
        End If
        '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
    End Sub
    '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* �@�@                          ByVal blnSelectAll as boolean)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* �@�@           ByVal blnSelectAll As Boolean           : True�̏ꍇ�S���ځAFalse�̏ꍇ�ȈՍ��ڂ̂ݎ擾
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal blnSelectAll As Boolean)
        MyBase.New(cfControlData, cfConfigDataClass, blnSelectAll)
        m_blnBatch = True
        '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
        If Not (Me.m_cABAtenaBRef Is Nothing) Then
            Me.m_cABAtenaBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABAtenaRirekiBRef Is Nothing) Then
            Me.m_cABAtenaRirekiBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABDainoBRef Is Nothing) Then
            Me.m_cABDainoBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABSfskBRef Is Nothing) Then
            Me.m_cABSfskBRef.m_blnBatch = True
        End If
        '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
    End Sub
    '* ����ԍ� 000002 2005/01/25 �ǉ��I���i�{��j
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
    '* �@�@           cfRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass)
        MyBase.New(cfControlData, cfConfigDataClass, cfRdbClass)
        m_blnBatch = True
        '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
        If Not (Me.m_cABAtenaBRef Is Nothing) Then
            Me.m_cABAtenaBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABAtenaRirekiBRef Is Nothing) Then
            Me.m_cABAtenaRirekiBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABDainoBRef Is Nothing) Then
            Me.m_cABDainoBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABSfskBRef Is Nothing) Then
            Me.m_cABSfskBRef.m_blnBatch = True
        End If
        '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
    End Sub

    '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* �@�@                          ByVal blnSelectAll as boolean)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* �@�@           cfRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* �@�@           ByVal blnSelectAll As Boolean           : True�̏ꍇ�S���ځAFalse�̏ꍇ�ȈՍ��ڂ̂ݎ擾
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass,
                   ByVal blnSelectAll As Boolean)
        MyBase.New(cfControlData, cfConfigDataClass, cfRdbClass, blnSelectAll)
        m_blnBatch = True
        '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
        If Not (Me.m_cABAtenaBRef Is Nothing) Then
            Me.m_cABAtenaBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABAtenaRirekiBRef Is Nothing) Then
            Me.m_cABAtenaRirekiBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABDainoBRef Is Nothing) Then
            Me.m_cABDainoBRef.m_blnBatch = True
        End If
        If Not (Me.m_cABSfskBRef Is Nothing) Then
            Me.m_cABSfskBRef.m_blnBatch = True
        End If
        '* ����ԍ� 000002 2005/01/25 �ǉ��J�n�i�{��j
    End Sub
    '* ����ԍ� 000002 2005/01/25 �ǉ��I���i�{��j

    '************************************************************************************************
    '* ���\�b�h��     �Ǘ����擾�i���������j
    '* 
    '* �\��           Private Function GetKanriJoho()
    '* 
    '* �@�\�@�@    �@�@�Ǘ������擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    <SecuritySafeCritical>
    Protected Overrides Sub GetKanriJoho()
        Const THIS_METHOD_NAME As String = "GetKanriJoho"
        '* ����ԍ� 000001 2004/08/27 �폜�J�n�i�{��j
        'Dim cfURAtenaKanriJoho As URAtenaKanriJohoBClass    '�����Ǘ����a�N���X
        '* ����ԍ� 000001 2004/08/27 �폜�I��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (m_blnKanriJoho) Then
                Exit Sub
            End If

            '�Ǘ����N���X�̃C���X�^���X�쐬
            '* ����ԍ� 000001 2004/08/27 �X�V�J�n�i�{��j
            'cfURAtenaKanriJoho = New URAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            If (m_cfURAtenaKanriJoho Is Nothing) Then
                m_cfURAtenaKanriJoho = New URAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            End If
            '* ����ԍ� 000001 2004/08/27 �X�V�I��

            m_intHyojiketaJuminCD = m_cfURAtenaKanriJoho.p_intHyojiketaJuminCD                '�Z���R�[�h�\������
            m_intHyojiketaStaiCD = m_cfURAtenaKanriJoho.p_intHyojiketaSetaiCD                 '���уR�[�h�\������
            m_intHyojiketaJushoCD = m_cfURAtenaKanriJoho.p_intHyojiketaJushoCD                '�Z���R�[�h�\�������i�Ǔ��̂݁j
            m_intHyojiketaGyoseikuCD = m_cfURAtenaKanriJoho.p_intHyojiketaGyoseikuCD          '�s����R�[�h�\������
            m_intHyojiketaChikuCD1 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD1              '�n��R�[�h�P�\������
            m_intHyojiketaChikuCD2 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD2              '�n��R�[�h�Q�\������
            m_intHyojiketaChikuCD3 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD3              '�n��R�[�h�R�\������
            m_strChikuCD1HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD1HyojiMeisho          '�n��R�[�h�P�\������
            m_strChikuCD2HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD2HyojiMeisho          '�n��R�[�h�Q�\������
            m_strChikuCD3HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD3HyojiMeisho          '�n��R�[�h�R�\������
            m_strRenrakusaki1HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki1HyojiMeisho  '�A����P�\������
            m_strRenrakusaki2HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki2HyojiMeisho  '�A����Q�\������

            ' �Ǘ����擾�ς݃t���O�ݒ�
            m_blnKanriJoho = True

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

    End Sub

End Class
