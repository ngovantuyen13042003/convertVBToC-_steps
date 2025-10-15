'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        �`�a�����Q���N�����ҏW
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/24�@�F��@��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/06/27 000001     �ϊ����̒l��Steing.Empty�̏ꍇ�G���[����o�O���C��
'* 2023/03/10 000002     �yAB-0970-1�z����GET�擾���ڕW�����Ή��i�����j
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
Imports System.Text

Public Class ABUmareHenshuBClass
    '************************************************************************************************
    '*
    '* ���N�����ҏW�Ɏg�p����p�����[�^�N���X
    '*
    '************************************************************************************************
    '�p�����[�^�̃����o�ϐ�
    Private m_cfUFLogClass As UFLogClass                '���O�o�̓N���X
    Private m_cfUFControlData As UFControlData          '�R���g���[���f�[�^
    Private m_cfUFConfigDataClass As UFConfigDataClass  '�R���t�B�O�f�[�^

    Private m_strDataKB As String                       '�敪(2��)
    Private m_strJuminSHU As String                     '���(2��)
    Private m_strUmareYMD As String                     '���N����
    Private m_strUmareWMD As String                     '���a��N����
    Private m_strHyojiUmareYMD As String                '�\���p���N����
    Private m_strShomeiUmareYMD As String               '�ؖ��p���N����
    Private m_cfDateClass As UFDateClass                '���t�ҏW 

    '�@�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABUmareHenshuBClass"             '�N���X��

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfUFControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfUFConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfUFControlData As UFControlData, _
                   ByVal cfUFConfigDataClass As UFConfigDataClass)
        '�����o�ϐ��Z�b�g
        m_cfUFControlData = cfUFControlData
        m_cfUFConfigDataClass = cfUFConfigDataClass

        '���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(cfUFConfigDataClass, cfUFControlData.m_strBusinessId)

        '�p�����[�^�̃����o�ϐ�������
        m_strDataKB = String.Empty
        m_strJuminSHU = String.Empty
        m_strUmareYMD = String.Empty
        m_strUmareWMD = String.Empty
        m_strHyojiUmareYMD = String.Empty
        m_strShomeiUmareYMD = String.Empty
        ' ���t�����N���X�C���X�^���X��
        m_cfDateClass = New UFDateClass(m_cfUFConfigDataClass)

    End Sub

    '************************************************************************************************
    '* ���\�b�h��      ���N�����ҏW
    '* 
    '* �\��           Public Sub HenshuUmare()
    '* 
    '* �@�\�@�@       ���N�����E���a��N�������\���p���N�����E�ؖ��p�N������ҏW����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub HenshuUmare()
        Dim strNengo As String = String.Empty
        Dim strUmareYmd As String = String.Empty

        Try


            ' �a��P�����ڂ��擾
            strNengo = m_strUmareWMD.RSubstring(0, 1)
            If ((strNengo = "0") Or (strNengo = "8") Or (strNengo = "9")) Then
                If (m_strUmareYMD.Trim() = "") Then
                    Select Case (strNengo)
                        Case "0"
                            strUmareYmd = "20" + m_strUmareWMD.RSubstring(1)
                        Case "8"
                            strUmareYmd = "18" + m_strUmareWMD.RSubstring(1)
                        Case "9"
                            strUmareYmd = "19" + m_strUmareWMD.RSubstring(1)
                        Case Else
                            strUmareYmd = "20" + m_strUmareWMD.RSubstring(1)
                    End Select
                    m_cfDateClass.p_strDateValue = strUmareYmd
                Else
                    m_cfDateClass.p_strDateValue = m_strUmareYMD
                End If

                If (Not m_cfDateClass.CheckDate()) Then
                    m_strHyojiUmareYMD = String.Empty
                    m_strShomeiUmareYMD = String.Empty
                    Exit Try
                End If

                ' ���N�������\���p���t�̕ҏW���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.Period
                m_cfDateClass.p_blnWideType = False
                m_cfDateClass.p_enDateFillType = UFDateFillType.Zero
                m_strHyojiUmareYMD = m_cfDateClass.p_strSeirekiYMD

                ' ���N�������ؖ��p���t�̕ҏW���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.Japanese
                m_cfDateClass.p_blnWideType = True
                m_cfDateClass.p_enEraType = UFEraType.Kanji
                m_cfDateClass.p_enDateFillType = UFDateFillType.Blank
                m_strShomeiUmareYMD = m_cfDateClass.p_strSeirekiYMD
            Else
                ' ���a��N�������\���p���t�̕ҏW���s��
                m_cfDateClass.p_strDateValue = m_strUmareWMD

                If (Not m_cfDateClass.CheckDate()) Then
                    m_strHyojiUmareYMD = String.Empty
                    m_strShomeiUmareYMD = String.Empty
                    Exit Try
                End If

                m_cfDateClass.p_blnWideType = False
                m_cfDateClass.p_enEraType = UFEraType.KanjiRyaku
                m_cfDateClass.p_enDateFillType = UFDateFillType.Zero
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.Period
                m_strHyojiUmareYMD = m_cfDateClass.p_strWarekiYMD

                ' ���a��N�������ؖ��p���t�̕ҏW���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.Japanese
                m_cfDateClass.p_blnWideType = True
                m_cfDateClass.p_enEraType = UFEraType.Kanji
                m_cfDateClass.p_enDateFillType = UFDateFillType.Blank
                m_strShomeiUmareYMD = m_cfDateClass.p_strWarekiYMD
            End If
        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:HenshuUmare�z�y�G���[���e:" + objExp.Message + "�z")
            '�V�X�e���G���[���X���[����
            Throw objExp
        End Try

    End Sub

    '************************************************************************************************
    '* �e�����o�ϐ��̃v���p�e�B��`
    '************************************************************************************************
    Public WriteOnly Property p_strDataKB() As String
        Set(ByVal Value As String)
            m_strDataKB = Value
        End Set
    End Property
    Public WriteOnly Property p_strJuminSHU() As String
        Set(ByVal Value As String)
            m_strJuminSHU = Value
        End Set
    End Property
    Public WriteOnly Property p_strUmareYMD() As String
        Set(ByVal Value As String)
            '* ����ԍ� 000001 2003/06/27 �C���J�n
            'm_strUmareYMD = Value
            m_strUmareYMD = Value.RPadRight(8)
            '* ����ԍ� 000001 2003/06/27 �C���I��
        End Set
    End Property
    Public WriteOnly Property p_strUmareWMD() As String
        Set(ByVal Value As String)
            '* ����ԍ� 000001 2003/06/27 �C���J�n
            'm_strUmareWMD = Value
            m_strUmareWMD = Value.RPadRight(7)
            '* ����ԍ� 000001 2003/06/27 �C���I��
        End Set
    End Property
    Public ReadOnly Property p_strHyojiUmareYMD() As String
        Get
            Return m_strHyojiUmareYMD
        End Get
    End Property
    Public ReadOnly Property p_strShomeiUmareYMD() As String
        Get
            Return m_strShomeiUmareYMD
        End Get
    End Property

End Class
