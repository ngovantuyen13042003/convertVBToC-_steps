'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �ٓ����R(ABIdoJiyuBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/04/01�@���@�Ԗ�
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

Public Class ABIdoJiyuBClass

    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABIdoJiyuBClass"

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfControlData As UFControlData, 
    '*                                  ByVal cfConfigData As UFConfigDataClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                   cfConfigData As UFConfigDataClass      : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass)

        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �ٓ����R�擾
    '* 
    '* �\��            Public Sub GetIdoJiyu(ByVal strAtenaDataKB As String,
    '*                                         ByVal strAtenaDataSHU As String)
    '* 
    '* �@�\�@�@        �����f�[�^�敪�A�����f�[�^��ʂ�薼�̂�ҏW����
    '* 
    '* ����            strIdoJiyuCD As String   : �ٓ����R�R�[�h
    '* 
    '* �߂�l          �ٓ����R(String)
    '************************************************************************************************
    Public Function GetIdoJiyu(ByVal strIdoJiyuCD As String) As String
        Const THIS_METHOD_NAME As String = "GetIdoJiyu"
        Dim strIdoJiyu As String

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strIdoJiyuCD
                Case "001", "01"
                    strIdoJiyu = "�폜"
                Case "002", "02"
                    strIdoJiyu = "�ǉ�"
                Case "010", "10"
                    strIdoJiyu = "�]��"
                Case "011", "11"
                    strIdoJiyu = "�o��"
                Case "012", "12"
                    strIdoJiyu = "�E���L��"
                Case "013", "13"
                    strIdoJiyu = "�A��"
                Case "014", "14"
                    strIdoJiyu = "���Ў擾"
                Case "015", "15"
                    strIdoJiyu = "��"
                Case "020", "20"
                    strIdoJiyu = "�]�o"
                Case "021", "21"
                    strIdoJiyu = "���S"
                Case "022", "22"
                    strIdoJiyu = "�E������"
                Case "023", "23"
                    strIdoJiyu = "���Бr��"
                Case "024", "24"
                    strIdoJiyu = "���H"
                Case Else
                    strIdoJiyu = ""
            End Select

            ' �f�o�b�O�I�����O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return strIdoJiyu

    End Function

End Class
