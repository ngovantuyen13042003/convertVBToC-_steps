'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �@�l����(ABHojinMeishoBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2002/12/18�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/09/11 000001     �`���[�j���O
'* 2015/04/23 000002     �x�X���̘A�����ɒl�L�������ǉ��i�΍��j
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

Public Class ABHojinMeishoBClass
    ' �����o�ϐ��̒�`
    Private m_cfUFLogClass As UFLogClass            '���O�o�̓N���X
    Private m_cfUFControlData As UFControlData      '�R���g���[���f�[�^

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABHojinMeishoBClass"

    '�p�����[�^�̃����o�ϐ�
    Private m_strKeitaiFuyoKB As String             '�敪�i1���j
    Private m_strKeitaiSeiRyakuKB As String         '�敪�i1���j
    Private m_strKanjiHjnKeitai As String           '�`�ԁi�S�p�@Max�P�O�����j
    Private m_strKanjiMeisho1 As String             '���́i�S�p�@Max�S�O�����j
    Private m_strKanjiMeisho2 As String             '���́i�S�p�@Max�S�O�����j

    '�e�����o�ϐ��̃v���p�e�B��`
    Public WriteOnly Property p_strKeitaiFuyoKB() As String
        Set(ByVal Value As String)
            m_strKeitaiFuyoKB = Value
        End Set
    End Property
    Public WriteOnly Property p_strKeitaiSeiRyakuKB() As String
        Set(ByVal Value As String)
            m_strKeitaiSeiRyakuKB = Value
        End Set
    End Property
    Public WriteOnly Property p_strKanjiHjnKeitai() As String
        Set(ByVal Value As String)
            m_strKanjiHjnKeitai = Value
        End Set
    End Property
    Public WriteOnly Property p_strKanjiMeisho1() As String
        Set(ByVal Value As String)
            m_strKanjiMeisho1 = Value
        End Set
    End Property
    Public WriteOnly Property p_strKanjiMeisho2() As String
        Set(ByVal Value As String)
            m_strKanjiMeisho2 = Value
        End Set
    End Property

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigData As UFConfigDataClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfUFControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                 cfUFConfigData As UFConfigDataClass      : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass)
        '�����o�ϐ��Z�b�g
        m_cfUFControlData = cfControlData
        '���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)
        '�p�����[�^�̃����o�ϐ�
        m_strKeitaiFuyoKB = String.Empty
        m_strKeitaiSeiRyakuKB = String.Empty
        m_strKanjiHjnKeitai = String.Empty
        m_strKanjiMeisho1 = String.Empty
        m_strKanjiMeisho2 = String.Empty
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �@�l���̕ҏW
    '* 
    '* �\��            Public Function GetHojinMeisho() As String
    '* 
    '* �@�\�@�@        �@�l�`�ԕt�^�敪�A�@�l�`�Ԑ������̋敪�A�@�l�`�ԁA���̂P�A���̂Q��薼�̂�ҏW����
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �ҏW���́iString�j
    '************************************************************************************************
    Public Function GetHojinMeisho() As String
        '*����ԍ� 000001 2003/09/11 �C���J�n
        'Dim strKanjiMeisho As String = String.Empty

        'Try
        '    '�f�o�b�O�J�n���O�o��
        '    m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetHojinMeisho")

        '    '�@�l�̖��̂̕ҏW
        '    Select Case m_strKeitaiFuyoKB
        '        Case "1"
        '            Select Case m_strKeitaiSeiRyakuKB
        '                Case "1"
        '                    strKanjiMeisho = m_strKanjiHjnKeitai + m_strKanjiMeisho1 + "�@" + m_strKanjiMeisho2
        '                Case Else
        '                    strKanjiMeisho = m_strKanjiHjnKeitai + "�@" + m_strKanjiMeisho1 + "�@" + m_strKanjiMeisho2
        '            End Select
        '        Case "2"
        '            Select Case m_strKeitaiSeiRyakuKB
        '                Case "1"
        '                    strKanjiMeisho = m_strKanjiMeisho1 + m_strKanjiHjnKeitai + m_strKanjiMeisho2
        '                Case Else
        '                    strKanjiMeisho = m_strKanjiMeisho1 + "�@" + m_strKanjiHjnKeitai + "�@" + m_strKanjiMeisho2
        '            End Select
        '        Case Else
        '            strKanjiMeisho = m_strKanjiMeisho1 + "�@" + m_strKanjiMeisho2
        '    End Select

        '    '�f�o�b�O�I�����O�o��
        '    m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetHojinMeisho")
        'Catch objExp As Exception
        '    '�G���[���O�o��
        '    m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:GetKjnhjn�z�y�G���[���e:" + objExp.Message + "�z")
        '    '�G���[�����̂܂܃X���[����
        '    Throw objExp
        'End Try

        'Return strKanjiMeisho

        Dim strKanjiMeisho As StringBuilder
        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            strKanjiMeisho = New StringBuilder()
            '�@�l�̖��̂̕ҏW
            Select Case m_strKeitaiFuyoKB
                Case "1"
                    Select Case m_strKeitaiSeiRyakuKB
                        Case "1"
                            '*����ԍ� 000002 2015/04/23 �C���J�n
                            'strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append(m_strKanjiMeisho1).Append("�@").Append(m_strKanjiMeisho2)
                            strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append(m_strKanjiMeisho1)
                            strKanjiMeisho = Me.AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2)
                            '*����ԍ� 000002 2015/04/23 �C���I��
                        Case Else
                            '*����ԍ� 000002 2015/04/23 �C���J�n
                            'strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append("�@").Append(m_strKanjiMeisho1).Append("�@").Append(m_strKanjiMeisho2)
                            strKanjiMeisho.Append(m_strKanjiHjnKeitai).Append("�@").Append(m_strKanjiMeisho1)
                            strKanjiMeisho = Me.AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2)
                            '*����ԍ� 000002 2015/04/23 �C���I��
                    End Select
                Case "2"
                    Select Case m_strKeitaiSeiRyakuKB
                        Case "1"
                            strKanjiMeisho.Append(m_strKanjiMeisho1).Append(m_strKanjiHjnKeitai).Append(m_strKanjiMeisho2)
                        Case Else
                            '*����ԍ� 000002 2015/04/23 �C���J�n
                            'strKanjiMeisho.Append(m_strKanjiMeisho1).Append("�@").Append(m_strKanjiHjnKeitai).Append("�@").Append(m_strKanjiMeisho2)
                            strKanjiMeisho.Append(m_strKanjiMeisho1).Append("�@").Append(m_strKanjiHjnKeitai)
                            strKanjiMeisho = Me.AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2)
                            '*����ԍ� 000002 2015/04/23 �C���I��
                    End Select
                Case Else
                    '*����ԍ� 000002 2015/04/23 �C���J�n
                    'strKanjiMeisho.Append(m_strKanjiMeisho1).Append("�@").Append(m_strKanjiMeisho2)
                    strKanjiMeisho.Append(m_strKanjiMeisho1)
                    strKanjiMeisho = Me.AppendShitenmei(strKanjiMeisho, m_strKanjiMeisho2)
                    '*����ԍ� 000002 2015/04/23 �C���I��
            End Select

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)
        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw objExp
        End Try


        Return strKanjiMeisho.ToString
        '*����ԍ� 000001 2003/09/11 �C���I��

    End Function

    '*����ԍ� 000002 2015/04/23 �ǉ��J�n
    ''' <summary>
    ''' �@�l���i����і@�l�`�ԁj�Ɏx�X����A�����ĕԐM���܂��B
    ''' </summary>
    ''' <param name="csHojinmei">�@�l���i����і@�l�`�ԁj</param>
    ''' <param name="strShitenmei">�x�X��</param>
    ''' <returns></returns>
    ''' <remarks>�l�L������A����ѐݒ�l�̑O��󔒂͏������Ȃ��B</remarks>
    Private Function AppendShitenmei( _
        ByVal csHojinmei As StringBuilder, _
        ByVal strShitenmei As String) As StringBuilder

        Try

            With csHojinmei

                ' �x�X�������݂���ꍇ�ɁA�S�p�󔒁{�x�X����A������B
                If (strShitenmei.RLength > 0) Then
                    .Append("�@")
                    .Append(strShitenmei)
                Else
                    ' noop
                End If

            End With

        Catch csExp As Exception
            Throw
        End Try

        Return csHojinmei

    End Function
    '*����ԍ� 000002 2015/04/23 �ǉ��I��

End Class
