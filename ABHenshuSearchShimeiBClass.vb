'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        �ҏW��������(ABHenshuSearchShimeiBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2007/09/28�@����@��
'* 
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2007/10/10 000001     �W���s�����̌����J�i���ڂ��A���t�@�x�b�g�̏ꍇ�͑啶���ɕϊ��i����j
'* 2023/08/14 000002    �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ�(����)
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports System.Text
Imports Densan.FrameWork.Tools
Imports Densan.Common

Public Class ABHenshuSearchShimeiBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLog As UFLogClass                       '���O�o�̓N���X
    Private m_cfConfigData As UFConfigDataClass         '�����f�[�^�N���X
    Private m_cfControlData As UFControlData            '�R���g���[���f�[�^

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABHenshuSearchShimeiBClass"
    '*����ԍ� 000002 2023/08/14 �ǉ��J�n
    Private Const KANA_SEIMEI As Integer = 120
    Private Const KANA_SEI As Integer = 72
    Private Const KANA_MEI As Integer = 48
    '*����ԍ� 000002 2023/08/14 �ǉ��I��
#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal csUFControlData As UFControlData, 
    '*                               ByVal csUFConfigData As UFConfigDataClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            csUFControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                 csUFConfigData As UFConfigDataClass      : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass)
        '�����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigData = cfConfigData

        '���O�o�̓N���X�̃C���X�^���X��
        m_cfLog = New UFLogClass(m_cfConfigData, m_cfControlData.m_strBusinessId)

    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �����p�J�i�ҏW
    '* 
    '* �\��           Public Function GetSearchKana(ByVal strKanaMeisho As String, _
    '*                                              ByVal strKanaMeisho As String, _
    '*                                              ByVal enHommyKensakuKB As FrnHommyoKensakuType) As String()
    '* 
    '* �@�\�@�@       �����p�J�i���̂�ҏW����
    '* 
    '* ����           strKanaMeisho    As String                   : �J�i���̂P
    '*                strKanaMeisho2   As String                   : �J�i���̂Q
    '*                enHommyKensakuKB As FrnHommyoKensakuType     : �{���D�挟���敪
    '* 
    '* �߂�l         String()          : [0]�����p�J�i����
    '*                                  : [1]�����p�J�i��
    '*                                  : [2]�����p�J�i��
    '*                                  : [3]�J�i��
    '*                                  : [4]�J�i��
    '************************************************************************************************
    Public Function GetSearchKana(ByVal strKanaMeisho As String, _
                                             ByVal strKanaMeisho2 As String, _
                                             ByVal enHommyKensakuKB As FrnHommyoKensakuType) As String()
        Const THIS_METHOD_NAME As String = "GetSearchKana"                      '���\�b�h��
        Dim strSearchKana(4) As String                      '�����p�J�i
        Dim cuString As New USStringClass                   '������ҏW
        Dim intIndex As Integer                             '�擪����̋󔒈ʒu

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�W���iTsusho�F�W���@Tsusho_Seishiki�F�{���ƒʏ̖��Ō����\��DB�j
            If (enHommyKensakuKB = FrnHommyoKensakuType.Tsusho) Then

                ' �J�i���� �󔒂��l�߂Ă��琴��������
                '* ����ԍ� 0000001 2007/10/10 �C���J�n
                strSearchKana(0) = cuString.ToKanaKey((strKanaMeisho).Replace(" ", String.Empty)).ToUpper()
                'strSearchKana(0) = cuString.ToKanaKey((strKanaMeisho).Replace(" ", String.Empty))
                '* ����ԍ� 0000001 2007/10/10 �C���I��

                ' �擪����̋󔒈ʒu�𒲂ׂ�
                intIndex = strKanaMeisho.RIndexOf(" ")

                ' �󔒂����݂��Ȃ��ꍇ
                If (intIndex = -1) Then
                    ' �J�i���E��
                    strSearchKana(1) = strSearchKana(0)
                    strSearchKana(3) = strKanaMeisho
                    strSearchKana(2) = String.Empty
                    strSearchKana(4) = String.Empty
                Else
                    ' �J�i���E��
                    '* ����ԍ� 0000001 2007/10/10 �C���J�n
                    strSearchKana(1) = cuString.ToKanaKey(strKanaMeisho.RSubstring(0, intIndex)).ToUpper()
                    'strSearchKana(1) = cuString.ToKanaKey(strKanaMeisho.Substring(0, intIndex))
                    '* ����ԍ� 0000001 2007/10/10 �C���I��
                    strSearchKana(3) = strKanaMeisho.RSubstring(0, intIndex)

                    ' �擪����̋󔒈ʒu�������񒷂ƈȏ�ꍇ
                    If ((intIndex + 1) >= strKanaMeisho.RLength) Then
                        strSearchKana(2) = String.Empty
                        strSearchKana(4) = String.Empty
                    Else
                        '* ����ԍ� 0000001 2007/10/10 �C���J�n
                        strSearchKana(2) = cuString.ToKanaKey(strKanaMeisho.RSubstring(intIndex + 1)).ToUpper()
                        'strSearchKana(2) = cuString.ToKanaKey(strKanaMeisho.Substring(intIndex + 1))
                        '* ����ԍ� 0000001 2007/10/10 �C���I��
                        strSearchKana(4) = strKanaMeisho.RSubstring(intIndex + 1)
                    End If
                End If
            Else
                '�{���ƒʏ̖��Ō����\��DB

                ' �J�i���� �󔒂��l�߂Ă��琴��������
                strSearchKana(0) = cuString.ToKanaKey((strKanaMeisho).Replace(" ", String.Empty)).ToUpper()

                ' �擪����̋󔒈ʒu�𒲂ׂ�
                intIndex = strKanaMeisho.RIndexOf(" ")

                ' �󔒂����݂��Ȃ��ꍇ�J�i���݂̂��Z�b�g
                If (intIndex = -1) Then
                    ' �J�i��
                    strSearchKana(1) = String.Empty
                    strSearchKana(3) = strKanaMeisho
                    strSearchKana(2) = String.Empty
                    strSearchKana(4) = String.Empty
                Else
                    ' �J�i���i�@�l�̂ݎg�p�j
                    strSearchKana(3) = strKanaMeisho.RSubstring(0, intIndex)

                    ' �擪����̋󔒈ʒu�������񒷈ȏ�̏ꍇ
                    If ((intIndex + 1) >= strKanaMeisho.RLength) Then
                        strSearchKana(2) = String.Empty
                        strSearchKana(4) = String.Empty
                    Else
                        strSearchKana(2) = cuString.ToKanaKey(strKanaMeisho.RSubstring(intIndex + 1)).ToUpper()
                        ' �J�i���i�@�l�̂ݎg�p�j
                        strSearchKana(4) = strKanaMeisho.RSubstring(intIndex + 1)
                    End If
                End If

                '�{���J�i����
                If (strKanaMeisho2.RLength > 0) Then
                    strSearchKana(1) = cuString.ToKanaKey((strKanaMeisho2).Replace(" ", String.Empty)).ToUpper()
                Else
                    strSearchKana(1) = String.Empty
                End If

            End If

            '*����ԍ� 000002 2023/08/14 �C���J�n
            ''�����J�i�����̌��`�F�b�N
            'If strSearchKana(0).RLength > 40 Then
            '    strSearchKana(0) = strSearchKana(0).RSubstring(0, 40)
            'End If
            If strSearchKana(0).RLength > KANA_SEIMEI Then
                strSearchKana(0) = strSearchKana(0).RSubstring(0, KANA_SEIMEI)
            End If
            '*����ԍ� 000002 2023/08/14 �C���I��

            '*����ԍ� 000002 2023/08/14 �C���J�n
            ''�����J�i���̌��`�F�b�N
            'If strSearchKana(1).RLength > 24 Then
            '    strSearchKana(1) = strSearchKana(1).RSubstring(0, 24)
            'End If
            If strSearchKana(1).RLength > KANA_SEI Then
                strSearchKana(1) = strSearchKana(1).RSubstring(0, KANA_SEI)
            End If
            '*����ԍ� 000002 2023/08/14 �C���I��

            '*����ԍ� 000002 2023/08/14 �C���J�n
            ''�����J�i���̌��`�F�b�N
            'If strSearchKana(2).RLength > 16 Then
            '    strSearchKana(2) = strSearchKana(2).RSubstring(0, 16)
            'End If
            If strSearchKana(2).RLength > KANA_MEI Then
                strSearchKana(2) = strSearchKana(2).RSubstring(0, KANA_MEI)
            End If
            '*����ԍ� 000002 2023/08/14 �C���I��

            ' �f�o�b�O�I�����O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

        Return strSearchKana

    End Function
#End Region
End Class
