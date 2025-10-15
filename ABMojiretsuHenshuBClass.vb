'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         ������ҏW�a�N���X(ABMojiretsuHenshuBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2008/02/13  ��Á@�v��
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

Public Class ABMojiretsuHenshuBClass

#Region "�����o�ϐ�"
    '�����o�ϐ��̒�`
    Private m_cfControlData As UFControlData                        ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass                ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                              ' �q�c�a�N���X
    Private m_cfLogClass As UFLogClass                              ' ���O�o�̓N���X
    Private m_cfErrorClass As UFErrorClass                          ' �G���[�����N���X

    Private m_cABAtenaKanriJohoB As ABAtenaKanriJohoBClass          ' �Ǘ����a�N���X
    Private m_strShimeiKakkoKB_param As String                      ' �������ʐ���敪�p�����[�^
    Private m_cABMojiRetsuHenshuB As ABMojiretsuHenshuBClass        ' ������ҏW�a�N���X 


    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABMojiretsuHenshuBClass"
    Private Const THIS_BUSINESSID As String = "AB"          ' �Ɩ��R�[�h

    Private Const HIDARI_KAKKO As String = "�i"
    Private Const MIGI_KAKKO As String = "�j"
    Private Const STR_10 As String = "10"
    Private Const STR_20 As String = "20"

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
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass)
        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = New UFRdbClass(m_cfControlData.m_strBusinessId)

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        '�Ǘ����擾���s��
        If (m_strShimeiKakkoKB_param Is Nothing) Then
            '�����o�ɖ����ꍇ�̂݃C���X�^���X�����s��
            If (m_cABAtenaKanriJohoB Is Nothing) Then
                m_cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            Else
            End If
            '�Ǘ������擾
            m_strShimeiKakkoKB_param = m_cABAtenaKanriJohoB.GetShimeiKakkoKB_Param()
        Else
        End If

    End Sub
#End Region

#Region "���\�b�h"
    '**********************************************************************************************************************
    '* ���\�b�h��     �����ȗ������ҏW
    '* 
    '* �\��           Public Overloads Function EditKanryakuMeisho(ByVal strMeisho As String) As String
    '* 
    '* �@�\           �������̂��犇�ʂ̍폜�������s��s
    '* 
    '* ����           strMeisho  ������
    '*
    '* �߂�l         String     �ҏW����������
    '*
    '**********************************************************************************************************************
    Public Overloads Function EditKanryakuMeisho(ByVal strMeisho As String) As String

        Return Me.EditKanryakuMeisho(STR_10, STR_20, strMeisho)
    End Function

#Region "EditKanryakuMeisho:�����ȗ������ҏW"
    '**********************************************************************************************************************
    '* ���\�b�h��     �����ȗ������ҏW
    '* 
    '* �\��           Public Overloads Function EditKanryakuMeisho(ByVal strDataKB As String, _
    '*                                                             ByVal strDataShu As String, _
    '*                                                             ByVal strMeisho As String) As String
    '* 
    '* �@�\           �������̂��犇�ʂ̍폜�������s��
    '* 
    '* ����           strDataKB     �f�[�^�敪
    '*                strDataShu    �f�[�^���               
    '*                strMeisho     ������
    '*
    '* �߂�l         String        �ҏW����������
    '*
    '**********************************************************************************************************************
    Public Overloads Function EditKanryakuMeisho(ByVal strDataKB As String, _
                                                 ByVal strDataShu As String, _
                                                 ByVal strMeisho As String) As String
        Dim THIS_METHOD_NAME As String = "EditKanryakuMeisho"
        Dim intIndexFrom As Integer
        Dim intIndexTo As Integer
        Dim strWkMeisho As String
        Dim strRet As String = String.Empty

        Try
            ' ���[�N�ɖ��̂��Z�b�g
            strWkMeisho = strMeisho

            ' �Ǘ����F�������ʕҏW����[10,15]��"1"�̏ꍇ�A���ʂ���菜��
            If (m_strShimeiKakkoKB_param = "1") Then
            Else
                ' �ҏW�͂��Ȃ�
                strRet = strWkMeisho
                Exit Try
            End If

            ' �f�[�^�敪���l(1%) ���� �f�[�^��ʂ��O���l(2%)�̏ꍇ�A���ʂ���菜��
            If (strDataKB.RSubstring(0, 1) = "1" AndAlso strDataShu.RSubstring(0, 1) = "2") Then
            Else
                ' �ҏW�͂��Ȃ�
                strRet = strWkMeisho
                Exit Try
            End If

            '�󔒂܂��́A���P�����������������̓A���t�@�x�b�g�̏ꍇ�́A�s��Ȃ�
            If (strWkMeisho.TrimEnd <> String.Empty) Then
                If (Not UFStringClass.CheckAlphabetNumber(UFStringClass.ConvertWideToNarrow(strWkMeisho.RSubstring(0, 1)))) Then
                    '�����ʂ�{��
                    intIndexFrom = strWkMeisho.RIndexOf(HIDARI_KAKKO)

                    While intIndexFrom >= 0

                        '�E���ʂ�{��
                        intIndexTo = strWkMeisho.RSubstring(intIndexFrom + 1).RIndexOf(MIGI_KAKKO)

                        If (intIndexTo >= 0) Then
                            '���ʂ��폜����
                            strRet = strRet + strWkMeisho.RSubstring(0, intIndexFrom)                ' �����ʂ̒��O�܂ŏo�͑Ώ�

                            ' ���[�N�������芇�ʏ��������񂪒����ꍇ�͋󔒂��Z�b�g
                            If (strWkMeisho.RLength > intIndexFrom + intIndexTo + 2) Then
                                strWkMeisho = strWkMeisho.RSubstring(intIndexFrom + intIndexTo + 2)      ' �E���ʂ̎����烏�[�N�ɃZ�b�g
                            Else
                                strWkMeisho = String.Empty
                            End If

                            '�����ʂ�{��
                            intIndexFrom = strWkMeisho.RIndexOf(HIDARI_KAKKO)

                        Else
                            Exit While
                        End If

                    End While

                    ' ���[�N�̒l��߂�l�ɒǉ��Z�b�g
                    strRet = strRet + strWkMeisho
                Else
                    ' �ҏW�͂��Ȃ�
                    strRet = strWkMeisho
                End If
            Else
                ' �ҏW�͂��Ȃ�
                strRet = strWkMeisho
            End If

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            Throw

        End Try

        Return strRet
    End Function
#End Region

#End Region

End Class
