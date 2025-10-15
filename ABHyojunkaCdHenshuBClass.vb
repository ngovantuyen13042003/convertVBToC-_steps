'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �W�����R�[�h�ҏW�a�N���X(ABHyojunkaCdHenshuBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2023/03/13  �����@��
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
Imports System.Text

Public Class ABHyojunkaCdHenshuBClass

#Region "�����o�ϐ�"
    '�����o�ϐ��̒�`
    Private m_cfControlData As UFControlData                        ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass                ' �R���t�B�O�f�[�^
    Private m_cfLogClass As UFLogClass                              ' ���O�o�̓N���X

    '�p�����[�^�̃����o�ϐ�
    Private m_strJuminKbn As String                                 '�Z���敪
    Private m_strJuminShubetsu As String                            '�Z�����
    Private m_strJuminJotai As String                               '�Z�����

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABHyojunkaCdHenshuBClass"

    '�e�����o�ϐ��̃v���p�e�B��`
    Public ReadOnly Property p_strJuminKbn() As String
        Get
            Return m_strJuminKbn
        End Get
    End Property
    Public ReadOnly Property p_strJuminShubetsu() As String
        Get
            Return m_strJuminShubetsu
        End Get
    End Property
    Public ReadOnly Property p_strJuminJotai() As String
        Get
            Return m_strJuminJotai
        End Get
    End Property

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
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass)
        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        '�p�����[�^�̃����o�ϐ�
        m_strJuminKbn = String.Empty
        m_strJuminShubetsu = String.Empty
        m_strJuminJotai = String.Empty

    End Sub
#End Region

#Region "���\�b�h"

#Region "HenshuHyojunkaCd:�W�����R�[�h�ҏW"
    '**********************************************************************************************************************
    '* ���\�b�h��     �W�����R�[�h�ҏW
    '* 
    '* �\��           Public Sub HenshuHyojunkaCd(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String)
    '* 
    '* �@�\           �e�R�[�h��W���������ɏ�����̌n�ɕҏW����
    '* 
    '* ����           strAtenaDataKB     �����f�[�^�敪
    '*                strAtenaDataSHU    �����f�[�^���
    '*
    '* �߂�l         �Ȃ�
    '*
    '**********************************************************************************************************************
    Public Sub HenshuHyojunkaCd(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String)
        Dim THIS_METHOD_NAME As String = "HenshuHyojunkaCd"

        Try
            m_strJuminKbn = GetJuminKbn(strAtenaDataKB)
            m_strJuminShubetsu = GetJuminShubetsu(strAtenaDataKB, strAtenaDataSHU)
            m_strJuminJotai = GetJuminJotai(strAtenaDataKB, strAtenaDataSHU)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            Throw

        End Try

    End Sub
#End Region

#Region "GetJuminKbn:�Z���敪�擾"
    '**********************************************************************************************************************
    '* ���\�b�h��     �Z���敪�擾
    '* 
    '* �\��           Private Function GetJuminKbn(ByVal strAtenaDataKB As String) As String
    '* 
    '* �@�\           �W���������̃R�[�h�̌n�ɏ�����Z���敪��ԋp����
    '* 
    '* ����           strAtenaDataKB     �����f�[�^�敪
    '*
    '* �߂�l         String             �Z���敪
    '*
    '**********************************************************************************************************************
    Private Function GetJuminKbn(ByVal strAtenaDataKB As String) As String
        Dim THIS_METHOD_NAME As String = "GetJuminKbn"
        Dim strRet As String = String.Empty

        Try
            Select Case strAtenaDataKB
                Case ABConstClass.ATENADATAKB_JUTONAI_KOJIN
                    '�Z��
                    strRet = "1"
                Case ABConstClass.ATENADATAKB_JUTOGAI_KOJIN
                    '�Z�o�O
                    strRet = "2"
                Case ABConstClass.ATENADATAKB_HOJIN
                    '�@�l
                    strRet = "3"
                Case Else
                    '�ȊO�̏ꍇ�A�󔒂�ݒ�
                    strRet = String.Empty
            End Select

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            Throw

        End Try

        Return strRet
    End Function
#End Region

#Region "GetJuminShubetsu:�Z����ʎ擾"
    '**********************************************************************************************************************
    '* ���\�b�h��     �Z����ʎ擾
    '* 
    '* �\��           Private Function GetJuminShubetsu(ByVal strAtenaDataKB As String) As String
    '* 
    '* �@�\           �W���������̃R�[�h�̌n�ɏ�����Z����ʂ�ԋp����
    '* 
    '* ����           strAtenaDataKB     �����f�[�^�敪
    '*                strAtenaDataSHU    �����f�[�^���
    '*
    '* �߂�l         String             �Z�����
    '*
    '**********************************************************************************************************************
    Private Function GetJuminShubetsu(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String) As String
        Dim THIS_METHOD_NAME As String = "GetJuminShubetsu"
        Dim strRet As String = String.Empty

        Try
            Select Case strAtenaDataKB
                Case ABConstClass.ATENADATAKB_JUTONAI_KOJIN,
                     ABConstClass.ATENADATAKB_JUTOGAI_KOJIN
                    If (strAtenaDataSHU.Trim.RLength > 0) AndAlso (strAtenaDataSHU.Trim.RSubstring(0, 1) = "1") Then
                        '���{�l
                        strRet = "1"
                    ElseIf (strAtenaDataSHU.Trim.RLength > 0) AndAlso (strAtenaDataSHU.Trim.RSubstring(0, 1) = "2") Then
                        '�O���l
                        strRet = "2"
                    Else
                        '�ȊO�̏ꍇ�A�󔒂�ݒ�
                        strRet = String.Empty
                    End If
                Case Else
                    '�ȊO�̏ꍇ�A�󔒂�ݒ�
                    strRet = String.Empty
            End Select

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            Throw

        End Try

        Return strRet
    End Function
#End Region

#Region "GetJuminJotai:�Z����Ԏ擾"
    '**********************************************************************************************************************
    '* ���\�b�h��     �Z����Ԏ擾
    '* 
    '* �\��           Private Function GetJuminJotai(ByVal strAtenaDataKB As String) As String
    '* 
    '* �@�\           �W���������̃R�[�h�̌n�ɏ�����Z����Ԃ�ԋp����
    '* 
    '* ����           strAtenaDataKB     �����f�[�^�敪
    '*                strAtenaDataSHU    �����f�[�^���
    '*
    '* �߂�l         String             �Z�����
    '*
    '**********************************************************************************************************************
    Private Function GetJuminJotai(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String) As String
        Dim THIS_METHOD_NAME As String = "GetJuminJotai"
        Dim strRet As String = String.Empty

        Try
            Select Case strAtenaDataKB
                Case ABConstClass.ATENADATAKB_JUTONAI_KOJIN
                    Select Case strAtenaDataSHU
                        Case ABConstClass.JUMINSHU_NIHONJIN_JUMIN,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN
                            '�Z�o��
                            strRet = "1"
                        Case ABConstClass.JUMINSHU_NIHONJIN_TENSHUTU,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_TENSHUTU
                            '�]�o��
                            strRet = "2"
                        Case ABConstClass.JUMINSHU_NIHONJIN_SHIBOU,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU
                            '���S��
                            strRet = "3"
                        Case Else
                            '���̑�������
                            strRet = "9"
                    End Select

                Case ABConstClass.ATENADATAKB_JUTOGAI_KOJIN
                    Select Case strAtenaDataSHU
                        Case ABConstClass.JUMINSHU_NIHONJIN_JUMIN,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN
                            '�Z�o�O��
                            strRet = "1"
                        Case ABConstClass.JUMINSHU_NIHONJIN_SHIBOU,
                             ABConstClass.JUMINSHU_GAIKOKUJIN_SHIBOU
                            '���S��
                            strRet = "2"
                        Case Else
                            '���̑�������
                            strRet = "9"
                    End Select

                Case Else
                    '�ȊO�̏ꍇ�A�󔒂�ݒ�
                    strRet = String.Empty
            End Select

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            Throw

        End Try

        Return strRet
    End Function
#End Region

#End Region

End Class
