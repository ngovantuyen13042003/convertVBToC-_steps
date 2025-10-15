'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        ����(ABMeishoBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/07/25�@���@�Ԗ�
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

Public Class ABMeishoBClass
    ' �����o�ϐ��̒�`
    Private m_cfUFLogClass As UFLogClass            '���O�o�̓N���X
    Private m_cfUFControlData As UFControlData      '�R���g���[���f�[�^

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABMeishoBClass"

    '�p�����[�^�̃����o�ϐ�
    Private m_strKeitaiFuyoKB As String                     ' �敪�i1���j
    Private m_strKeitaiSeiRyakuKB As String                 ' �敪�i1���j
    Private m_strKanjiHjnKeitai As String                   ' �`�ԁi�S�p�@Max�P�O�����j
    Private m_strKanjiMeisho1 As String                     ' ���́i�S�p�@Max�S�O�����j
    Private m_strKanjiMeisho2 As String                     ' ���́i�S�p�@Max�S�O�����j
    Private m_strAtenaDataKB As String                      ' �����f�[�^�敪
    Private m_cHojinMeishoBClass As ABHojinMeishoBClass     ' �@�l���̃N���X

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
    Public WriteOnly Property p_strAtenaDataKB() As String
        Set(ByVal Value As String)
            m_strAtenaDataKB = Value
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

        ' �����o�ϐ��Z�b�g
        m_cfUFControlData = cfControlData

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

        ' �@�l���̃N���X�̃C���X�^���X�쐬
        m_cHojinMeishoBClass = New ABHojinMeishoBClass(cfControlData, cfConfigData)

        ' �p�����[�^�̃����o�ϐ�
        m_strKeitaiFuyoKB = String.Empty
        m_strKeitaiSeiRyakuKB = String.Empty
        m_strKanjiHjnKeitai = String.Empty
        m_strKanjiMeisho1 = String.Empty
        m_strKanjiMeisho2 = String.Empty
        m_strAtenaDataKB = String.Empty
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      ���̕ҏW
    '* 
    '* �\��            Public Function GetMeisho() As String
    '* 
    '* �@�\�@�@        �@�l�`�ԕt�^�敪�A�@�l�`�Ԑ������̋敪�A�@�l�`�ԁA���̂P�A���̂Q��薼�̂�ҏW����
    '* 
    '* ����            ����
    '* 
    '* �߂�l          �ҏW���́iString�j
    '************************************************************************************************
    Public Overloads Function GetMeisho() As String
        Const THIS_METHOD_NAME As String = "GetHojinMeisho"
        Dim strKanjiMeisho As String = String.Empty

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case m_strAtenaDataKB
                Case ABConstClass.ATENADATAKB_HOJIN
                    '�@�l�̖��̂̕ҏW
                    m_cHojinMeishoBClass.p_strKeitaiFuyoKB = m_strKeitaiFuyoKB
                    m_cHojinMeishoBClass.p_strKeitaiSeiRyakuKB = m_strKeitaiSeiRyakuKB
                    m_cHojinMeishoBClass.p_strKanjiHjnKeitai = m_strKanjiHjnKeitai
                    m_cHojinMeishoBClass.p_strKanjiMeisho1 = m_strKanjiMeisho1
                    m_cHojinMeishoBClass.p_strKanjiMeisho2 = m_strKanjiMeisho2
                    strKanjiMeisho = m_cHojinMeishoBClass.GetHojinMeisho
                Case Else
                    strKanjiMeisho = m_strKanjiMeisho1
            End Select

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception

            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:" + THIS_METHOD_NAME + "�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return strKanjiMeisho

    End Function

    '************************************************************************************************
    '* ���\�b�h��      ���̕ҏW
    '* 
    '* �\��            Public Function GetHojinMeisho(ByVal cABHojinMeishoParaX() As ABHojinMeishoParaXClass) As String()
    '* 
    '* �@�\�@�@        �@�l�`�ԕt�^�敪�A�@�l�`�Ԑ������̋敪�A�@�l�`�ԁA���̂P�A���̂Q��薼�̂�ҏW����
    '* 
    '* ����            ���̃p�����[�^�N���X   : ABMeishoParaXClass[]
    '* 
    '* �߂�l          �ҏW���́iString[]�j
    '************************************************************************************************
    Public Overloads Function GetMeisho(ByVal cABMeishoParaX() As ABMeishoParaXClass) As String()
        Const THIS_METHOD_NAME As String = "GetHojinMeisho"
        Dim strKanjiMeisho() As String
        Dim intIndex As Integer

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ReDim strKanjiMeisho(UBound(cABMeishoParaX))
            For intIndex = 0 To UBound(cABMeishoParaX)
                With cABMeishoParaX(intIndex)
                    m_strKeitaiFuyoKB = .p_strKeitaiFuyoKB
                    m_strKeitaiSeiRyakuKB = .p_strKeitaiSeiRyakuKB
                    m_strKanjiHjnKeitai = .p_strKanjiHjnKeitai
                    m_strKanjiMeisho1 = .p_strKanjiMeisho1
                    m_strKanjiMeisho2 = .p_strKanjiMeisho2
                    m_strAtenaDataKB = .p_strAtenaDataKB
                End With
                strKanjiMeisho(intIndex) = Me.GetMeisho
            Next intIndex

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:" + THIS_METHOD_NAME + "�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return strKanjiMeisho

    End Function
End Class
