'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        ���������ҏW(ABKensakuShimeiBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2002/12/18�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/11 000001     ��؂蕶���̕ύX
'* 2005/04/04 000002     �S�p�ł̂����܂��������\�ɂ���(�}���S���R)
'* 2007/09/03 000003     �����s�p�ҏW�T�u���[�`���̃I�[�o�[���[�h�i����j
'* 2007/10/10 000004     �W���̎d�l�ł��������A���t�@�x�b�g�̏ꍇ�͑啶���ɕϊ�����i����j
'* 2007/11/06 000005     �����J�i�����ҏW�p�^�[���̏C���A�����J�i���ڃ����o�ϐ����������i����j
'* 2011/09/26 000006     �S�p�A���t�@�x�b�g�������̐��������菈����ǉ��i��Áj
'* 2012/01/20 000007     �yAB17051�z�A���t�@�x�b�g���������@�\�̉��P(�k��)
'* 2020/01/10 000008     �yAB32001�z�A���t�@�x�b�g�����i�΍��j
'* 2023/12/04 000009     �yAB-1600-1�z�����@�\�Ή�(����)
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
Imports System.Security

Public Class ABKensakuShimeiBClass
    ' �����o�ϐ��̒�`
    Private m_cfUFLogClass As UFLogClass            '���O�o�̓N���X
    Private m_cfConfigData As UFConfigDataClass     '�����f�[�^�N���X
    Private m_cfUFControlData As UFControlData      '�R���g���[���f�[�^
    Private m_cRuijiClass As USRuijiClass       ' �ގ������N���X

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABKensakuShimeiBClass"
    Private Const BUBUNITCHI As String = "2"

    '�p�����[�^�̃����o�ϐ�
    Private m_strSearchkanjimei As String           '�����p�������́i�S�p�����@Max�S�O�����j
    Private m_strSearchKanaseimei As String         '�����p�J�i�����i���p�J�i�@Max�S�O�����j
    Private m_strSearchKanasei As String            '�����p�J�i���@�i���p�J�i�@Max�Q�S�����j
    Private m_strSearchKanamei As String            '�����p�J�i���@�i���p�J�i�@Max�P�U�����j

    '�e�����o�ϐ��̃v���p�e�B��`
    Public ReadOnly Property p_strSearchkanjimei() As String
        Get
            Return m_strSearchkanjimei
        End Get
    End Property
    Public ReadOnly Property p_strSearchKanaseimei() As String
        Get
            Return m_strSearchKanaseimei
        End Get
    End Property
    Public ReadOnly Property p_strSearchKanasei() As String
        Get
            Return m_strSearchKanasei
        End Get
    End Property
    Public ReadOnly Property p_strSearchKanamei() As String
        Get
            Return m_strSearchKanamei
        End Get
    End Property

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
        m_cfUFControlData = cfControlData
        m_cfConfigData = cfConfigData

        '���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

        '�p�����[�^�̃����o�ϐ�
        m_strSearchkanjimei = String.Empty
        m_strSearchKanaseimei = String.Empty
        m_strSearchKanasei = String.Empty
        m_strSearchKanamei = String.Empty
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      ���������擾
    '* 
    '* �\��            Public Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String)
    '* 
    '* �@�\�@�@        �����������L�[�Ƃ��ĕҏW����
    '* 
    '* ����            strAimai As String        :�O����v
    '*                 strShimei As String      �F����
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    '*����ԍ� 000003 2007/09/03 �C���J�n
    Public Overloads Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String)
        ''Public Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String)
        'Const THIS_METHOD_NAME As String = "GetKensakuShimei"                   '���\�b�h��
        'Dim cuString As New USStringClass
        'Dim strHenshu As String = String.Empty              '�����̕ҏW���̂��i�[
        'Dim strHenshuSei As String = String.Empty           '�ҏW���̐�
        'Dim strHenshuMei As String = String.Empty           '�ҏW���̖�
        'Dim intIchi As Integer = 0                          '���ʒu
        ''04/02/28 �ǉ��J�n
        'Dim strChkHenshu As String = String.Empty           '�Ђ炪��`�F�b�N
        ''04/02/28 �ǉ��I��

        'Try
        '    '�f�o�b�O�J�n���O�o��
        '    m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        '    '04/02/28 �ǉ��J�n
        '    If cuString.ToHankaku(strShimei, strChkHenshu) Then
        '        strShimei = strChkHenshu
        '    End If
        '    '04/02/28 �ǉ��I��

        '    strHenshu = strShimei

        '    '* ����ԍ� 000002 2005/04/04 �C���J�n
        '    'If (UFStringClass.CheckKanjiCode(strHenshu, m_cfConfigData)) Then
        '    If (UFStringClass.CheckKanjiCode(strHenshu.Trim("%"c).Trim("��"c), m_cfConfigData)) Then
        '        '* ����ԍ� 000002 2005/04/04 �C���I��
        '        '�S�p
        '        '* ����ԍ� 000001 2003/03/11 �C���J�n
        '        'intIchi = InStr(strHenshu, "�F")
        '        intIchi = InStr(strHenshu, "��")
        '        '* ����ԍ� 000001 2003/03/11 �C���I��
        '        If (intIchi > 0) Then
        '            Mid(strHenshu, intIchi, 1) = "�@"
        '        End If
        '        '* ����ԍ� 000002 2005/04/04 �ǉ��J�n
        '        intIchi = InStr(strHenshu, "��")
        '        If (intIchi > 0) Then
        '            Mid(strHenshu, intIchi, 1) = "%"
        '        End If
        '        '* ����ԍ� 000002 2005/04/04 �ǉ��I��
        '        If (strAimai = "1") Then
        '            strHenshu = strHenshu + "%"
        '        End If
        '        m_strSearchkanjimei = strHenshu
        '    Else
        '        '���p
        '        '* ����ԍ� 000002 2005/04/04 �ǉ��J�n
        '        intIchi = InStr(strShimei, "��")
        '        If (intIchi > 0) Then
        '            Mid(strHenshu, intIchi, 1) = "%"
        '        End If
        '        '* ����ԍ� 000002 2005/04/04 �ǉ��I��
        '        '* ����ԍ� 000001 2003/03/11 �C���J�n
        '        'intIchi = InStr(strShimei, ":")
        '        intIchi = InStr(strShimei, "*")
        '        '* ����ԍ� 000001 2003/03/11 �C���I��
        '        If (intIchi = 0) Then
        '            intIchi = InStr(strShimei, " ")
        '        End If
        '        If (intIchi <> 0) Then
        '            '����
        '            '��
        '            strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1))
        '            If (strAimai = "1") Then
        '                strHenshuSei = strHenshuSei + "%"
        '            End If
        '            m_strSearchKanasei = strHenshuSei
        '            '��
        '            strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1))
        '            If (strAimai = "1") Then
        '                strHenshuMei = strHenshuMei + "%"
        '            End If
        '            m_strSearchKanamei = strHenshuMei
        '        Else
        '            '�����Ȃ�
        '            strHenshu = cuString.ToKanaKey(strHenshu)
        '            If (strAimai = "1") Then
        '                strHenshu = strHenshu + "%"
        '            End If
        '            m_strSearchKanaseimei = strHenshu
        '        End If
        '    End If

        '    '�f�o�b�O�I�����O�o��
        '    m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        'Catch objExp As Exception
        '    '�G���[���O�o��
        '    m_cfUFLogClass.ErrorWrite(m_cfUFControlData, _
        '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" _
        '                              + "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" _
        '                              + "�y�G���[���e:" + objExp.Message + "�z")
        '    '�G���[�����̂܂܃X���[����
        '    Throw objExp
        'End Try

        GetKensakuShimei(strAimai, strShimei, 0)
        '*����ԍ� 000003 2007/09/03 �C���I��
    End Sub

    '*����ԍ� 000003 2007/09/03 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��      ���������擾�i�I�[�o�[���[�h�j
    '* 
    '* �\��            Public Sub GetKensakuShimei(ByVal strAimai As String, ByVal strShimei As String, 
    '*                                                                  ByVal intHommyoYusen As Integer)
    '* 
    '* �@�\�@�@        �����������L�[�Ƃ��ĕҏW����
    '* 
    '* ����            strAimai As String        :�O����v
    '*                 strShimei As String      �F����
    '*                 intHommyoYusen As Integer�F�W��(0)�C�{��(1)�C�ʏ̖�(2)
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    <SecuritySafeCritical>
    Public Overloads Sub GetKensakuShimei(ByVal strAimai As String,
                                          ByVal strShimei As String,
                                          ByVal intHommyoYusen As Integer)
        Const THIS_METHOD_NAME As String = "GetKensakuShimei"                   '���\�b�h��
        Dim cuString As New USStringClass
        Dim strHenshu As String = String.Empty              '�����̕ҏW���̂��i�[
        Dim strHenshuSei As String = String.Empty           '�ҏW���̐�
        Dim strHenshuMei As String = String.Empty           '�ҏW���̖�
        Dim intIchi As Integer = 0                          '���ʒu
        Dim strChkHenshu As String = String.Empty           '�Ђ炪�ȃ`�F�b�N
        Dim cfRdb As UFRdbClass                             'RDB�N���X
        Dim crKanriJohoB As URKANRIJOHOCacheBClass          '�Ǘ����a�N���X
        Dim enGaikokujinKensakuKB As FrnHommyoKensakuType   '�O���l�{�������敪
        '*����ԍ� 000006 2011/09/26 �ǉ��J�n
        Dim cABKanriJohoB As ABAtenaKanriJohoBClass         '�����Ǘ����N���X
        Dim csABKanriJohoDS As DataSet
        Dim strZenAlphabetKB As String
        '*����ԍ� 000006 2011/09/26 �ǉ��I��

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDB�N���X�̃C���X�^���X�쐬
            cfRdb = New UFRdbClass(m_cfUFControlData.m_strBusinessId)

            '*����ԍ� 000005 2007/11/06 �ǉ��J�n
            ' �����p�����o�ϐ�������
            m_strSearchkanjimei = String.Empty
            m_strSearchKanaseimei = String.Empty
            m_strSearchKanasei = String.Empty
            m_strSearchKanamei = String.Empty
            '*����ԍ� 000005 2007/11/06 �ǉ��I��

            ' �����擾�r�W�l�X�N���X�̃C���X�^���X�쐬
            crKanriJohoB = New URKANRIJOHOCacheBClass(m_cfUFControlData, m_cfConfigData, cfRdb)
            ' �Ǘ����擾���\�b�h���s
            enGaikokujinKensakuKB = crKanriJohoB.GetFrn_HommyoKensaku_Param()

            '*����ԍ� 000006 2011/09/26 �ǉ��J�n
            ' �����Ǘ����N���X�̃C���X�^���X��
            cABKanriJohoB = New ABAtenaKanriJohoBClass(m_cfUFControlData, m_cfConfigData, cfRdb)
            ' �Ǘ����擾���\�b�h���s(�������(03)�A�S�p�A���t�@�x�b�g��������(14))
            csABKanriJohoDS = cABKanriJohoB.GetKanriJohoHoshu("03", "14")

            ' �Ǘ����`�F�b�N
            If (Not (csABKanriJohoDS Is Nothing) AndAlso csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then
                strZenAlphabetKB = csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0)(ABAtenaKanriJohoEntity.PARAMETER).ToString
            Else
                strZenAlphabetKB = "0"
            End If
            '*����ԍ� 000006 2011/09/26 �ǉ��I��

            If (m_cRuijiClass Is Nothing) Then
                m_cRuijiClass = New USRuijiClass
            End If

            If cuString.ToHankaku(strShimei, strChkHenshu) Then
                '*����ԍ� 000006 2011/09/26 �ǉ��J�n
                If (strZenAlphabetKB = "1") Then
                    ' �Ǘ����F������ʁE�S�p�A���t�@�x�b�g��������(03�E14) = "1" �̏ꍇ
                    If (UFStringClass.CheckAlphabetNumber(strChkHenshu.Replace(" ", "").Trim("%"c).Trim("*"c).Trim("."c).Trim("�"c))) Then
                        If (strShimei = strChkHenshu) Then
                            '���͂����p�A���t�@�x�b�g�Ƃ������ƂɂȂ邽�ߔ��p�Ō���������
                            strShimei = strChkHenshu
                            '*����ԍ� 000007 2012/01/20 �C���J�n
                        ElseIf (strChkHenshu = "*") Then
                            '���p�ϊ���̒l��'*'�̏ꍇ�A'*'�Ō���������
                            strShimei = strChkHenshu
                            '*����ԍ� 000007 2012/01/20 �C���I��
                        Else
                            '*����ԍ� 000008 2020/01/10 �C���J�n
                            ''���͂��S�p�A���t�@�x�b�g�Ƃ������Ƃ�����S�p�Ō���������
                            ' ���͂��S�p�A���t�@�x�b�g�Ƃ������Ƃ�����S�p���p�����Ō���������
                            Call SetSearchKanjiShimei(strShimei, strAimai)
                            strShimei = strChkHenshu
                            '*����ԍ� 000008 2020/01/10 �C���I��
                        End If
                    Else
                        '�A���t�@�x�b�g�ł͂Ȃ��̂Œʏ�ʂ蔼�p�ł̌���
                        strShimei = strChkHenshu
                    End If
                Else
                    strShimei = strChkHenshu
                End If
                'strShimei = strChkHenshu
                '*����ԍ� 000006 2011/09/26 �ǉ��I��
            End If

            strHenshu = strShimei

            If (UFStringClass.CheckKanjiCode(strHenshu.Trim("%"c).Trim("��"c), m_cfConfigData)) Then
                '�S�p
                intIchi = InStr(strHenshu, "��")
                If (intIchi > 0) Then
                    Mid(strHenshu, intIchi, 1) = "�@"
                End If
                strHenshu = m_cRuijiClass.GetRuijiMojiList(strHenshu.Replace("�@", String.Empty)).ToUpper
                intIchi = InStr(strHenshu, "��")
                If (intIchi > 0) Then
                    Mid(strHenshu, intIchi, 1) = "%"
                End If
                If (strAimai = "1") Then
                    strHenshu = strHenshu + "%"
                ElseIf (strAimai = BUBUNITCHI) Then
                    strHenshu = "%" + strHenshu + "%"
                End If
                m_strSearchkanjimei = strHenshu
            Else
                '���p
                intIchi = InStr(strShimei, "��")
                If (intIchi > 0) Then
                    Mid(strHenshu, intIchi, 1) = "%"
                End If
                intIchi = InStr(strShimei, "*")
                If (intIchi = 0) Then
                    intIchi = InStr(strShimei, " ")
                End If

                '�{���D�挟���p�����[�^���P�C�Q�ȊO�̂Ƃ���AtenaGet�̃C���^�[�t�F�[�X�p�Ɍ����J�i�p�ϐ���ݒ�
                '�O���l�{�������@�\�敪���W���̂Ƃ���AtenaGet�̃C���^�[�t�F�[�X�p�Ɍ����J�i�p�ϐ���ݒ�
                '*����ԍ� 000003 2007/09/03�ȑO����GetKensakuShimei���g�p���Ă���Ɩ��ɂ͉e���Ȃ��B
                If (enGaikokujinKensakuKB = FrnHommyoKensakuType.Tsusho OrElse
                                        (intHommyoYusen <> 1 AndAlso intHommyoYusen <> 2)) Then
                    '�W���d�l
                    If (intIchi <> 0) Then
                        '����
                        '��
                        '* ����ԍ� 000004 2007/10/10 �C���J�n
                        strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                        'strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1))
                        '* ����ԍ� 000004 2007/10/10 �C���I��
                        If (strAimai = "1") Then
                            strHenshuSei = strHenshuSei + "%"
                        End If
                        m_strSearchKanasei = strHenshuSei
                        '��
                        '* ����ԍ� 000004 2007/10/10 �C���J�n
                        strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1)).ToUpper()
                        'strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1))
                        '* ����ԍ� 000004 2007/10/10 �C���I��
                        If (strAimai = "1") Then
                            strHenshuMei = strHenshuMei + "%"
                        End If
                        m_strSearchKanamei = strHenshuMei
                        If (strAimai = BUBUNITCHI) Then
                            m_strSearchKanasei = String.Empty
                            m_strSearchKanamei = String.Empty
                            strHenshu = cuString.ToKanaKey(strHenshu.Replace(" ", String.Empty).Replace("*", String.Empty)).ToUpper()
                            strHenshu = "%" + strHenshu + "%"
                            m_strSearchKanaseimei = strHenshu
                        End If
                    Else
                        '�����Ȃ�
                        '* ����ԍ� 000004 2007/10/10 �C���J�n
                        strHenshu = cuString.ToKanaKey(strHenshu).ToUpper()
                        'strHenshu = cuString.ToKanaKey(strHenshu)
                        '* ����ԍ� 000004 2007/10/10 �C���I��
                        If (strAimai = "1") Then
                            strHenshu = strHenshu + "%"
                        ElseIf (strAimai = BUBUNITCHI) Then
                            strHenshu = "%" + strHenshu + "%"
                        End If
                        m_strSearchKanaseimei = strHenshu
                    End If
                Else
                    '�{���ƒʏ̖��̗����Ō����\�Ȃc�a
                    '�A���t�@�x�b�g�͑S�đ啶���ŃZ�b�g����
                    If (intHommyoYusen = 2) Then
                        '�{���D�挟���ȊO
                        '�����J�i�����@�����J�i���Ɍ��������񂪃Z�b�g�����
                        '�J�i�ʏ̖��̏ꍇ
                        If (intIchi <> 0) Then
                            '*����ԍ� 000005 2007/11/06 �C���J�n
                            '�������� �J�i���J�i���𒊏o
                            strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                            strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1)).ToUpper()
                            If (strAimai = "1") Then    '�B�������i�O����v�`�F�b�N��True�j�̂Ƃ�"%"��t��
                                If (strHenshuSei.Trim <> String.Empty) Then
                                    m_strSearchKanaseimei = strHenshuSei + "%"  '�����J�i��
                                End If
                                m_strSearchKanamei = strHenshuMei + "%"     '�����J�i��
                            ElseIf (straimai = BUBUNITCHI) Then
                                strHenshu = cuString.ToKanaKey(strHenshu.Replace(" ", String.Empty)).ToUpper()
                                strHenshu = "%" + strHenshu + "%"
                                m_strSearchKanaseimei = strHenshu
                            Else
                                '���S��v
                                '�����J�i����
                                If (strHenshuSei.Trim <> String.Empty) Then
                                    m_strSearchKanaseimei = cuString.ToKanaKey((strHenshu).Replace(" ", String.Empty)).ToUpper()
                                Else
                                    m_strSearchKanamei = strHenshuMei
                                End If
                            End If
                            ''�������� �J�i���J�i���𒊏o
                            'strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                            'strHenshuMei = cuString.ToKanaKey((Mid(strHenshu, intIchi + 1))).ToUpper()
                            'If (strAimai = "1") Then    '�B�������i�O����v�`�F�b�N��True�j�̂Ƃ�"%"��t��
                            '    strHenshuMei = strHenshuMei + "%"
                            'End If
                            'm_strSearchKanaseimei = strHenshuSei + "%"  '�����J�i���i�B���̗L���ɂ�����炸�����t�������j
                            'm_strSearchKanamei = strHenshuMei           '�����J�i��
                            '*����ԍ� 000005 2007/11/06 �C���I��
                        Else
                            '�����Ȃ�
                            strHenshu = cuString.ToKanaKey(strHenshu).ToUpper()
                            If (strAimai = "1") Then
                                strHenshu = strHenshu + "%"
                            ElseIf (strAimai = BUBUNITCHI) Then
                                strHenshu = "%" + strHenshu + "%"
                            End If
                            m_strSearchKanaseimei = strHenshu           '�����J�i����
                        End If
                    Else
                        '�{���D�挟��
                        '�J�i�{���̏ꍇ�i�����J�i���݂̂Ō����\�ɂ���ϐ��𐶐��j
                        '�����J�i���Ɍ��������񂪃Z�b�g�����
                        If (intIchi <> 0) Then
                            '*����ԍ� 000005 2007/11/06 �C���J�n
                            '��������̏ꍇ��������
                            strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                            strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1)).ToUpper()
                            If (strAimai = "1") Then    '�B�������i�O����v�`�F�b�N��True�j�̂Ƃ�"%"��t��
                                strHenshuSei = strHenshuSei + "%"
                                strHenshuMei = strHenshuMei + "%"
                                '�{���J�i���̂͌����p�J�i�����ŕԂ����i�����J�i���ƌ����J�i���������j
                                m_strSearchKanasei = strHenshuSei + strHenshuMei
                            ElseIf (straimai = BUBUNITCHI) Then
                                strHenshu = cuString.ToKanaKey(strHenshu.Replace(" ", String.Empty)).ToUpper()
                                strHenshu = "%" + strHenshu + "%"
                                m_strSearchKanaseimei = strHenshu
                            Else
                                '���S��v�̏ꍇ
                                If (strHenshuSei.Trim = String.Empty) Then
                                    m_strSearchKanasei = "%" + strHenshuMei
                                Else
                                    m_strSearchKanasei = cuString.ToKanaKey((strHenshu).Replace(" ", String.Empty)).ToUpper()
                                End If
                            End If
                            ''��������̏ꍇ��������
                            'strHenshuSei = cuString.ToKanaKey(Left(strHenshu, intIchi - 1)).ToUpper()
                            'strHenshuMei = cuString.ToKanaKey(Mid(strHenshu, intIchi + 1)).ToUpper()
                            'If (strAimai = "1") Then    '�B�������i�O����v�`�F�b�N��True�j�̂Ƃ�"%"��t��
                            '    strHenshuSei = strHenshuSei + "%"
                            '    strHenshuMei = strHenshuMei + "%"
                            'End If
                            ''�{���J�i���̂͌����p�J�i�����ŕԂ����i�����J�i���ƌ����J�i���������j
                            'm_strSearchKanasei = strHenshuSei + strHenshuMei
                            '*����ԍ� 000005 2007/11/06 �C���I��
                        Else
                            '�����Ȃ��̏ꍇ���̂܂ܞB��������t��
                            strHenshu = cuString.ToKanaKey(strHenshu).ToUpper()
                            If (strAimai = "1") Then
                                strHenshu = strHenshu + "%"
                            ElseIf (strAimai = BUBUNITCHI) Then
                                strHenshu = "%" + strHenshu + "%"
                            End If
                            '�{���J�i���̂͌����p�J�i�����ŕԂ����
                            m_strSearchKanasei = strHenshu
                        End If
                    End If
                End If
            End If

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" _
                                      + "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" _
                                      + "�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try

    End Sub
    '*����ԍ� 000003 2007/09/03 �ǉ��I��

    '*����ԍ� 000008 2020/01/10 �ǉ��J�n
    ''' <summary>
    ''' �����p���������ݒ�
    ''' </summary>
    ''' <param name="strShimei">�Ώە�����</param>
    ''' <param name="strAimai">�����܂�����</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKanjiShimei(ByVal strShimei As String, ByVal strAimai As String)

        Dim strHenshu As String
        Dim intIchi As Integer

        Try

            strHenshu = strShimei
            intIchi = InStr(strHenshu, "��")
            If (intIchi > 0) Then
                Mid(strHenshu, intIchi, 1) = "�@"
            End If
            strHenshu = m_cRuijiClass.GetRuijiMojiList(strHenshu.Replace("�@", String.Empty)).ToUpper
            intIchi = InStr(strHenshu, "��")
            If (intIchi > 0) Then
                Mid(strHenshu, intIchi, 1) = "%"
            End If
            If (strAimai = "1") Then
                strHenshu = strHenshu + "%"
            ElseIf (strAimai = BUBUNITCHI) Then
                strHenshu = "%" + strHenshu + "%"
            End If
            m_strSearchkanjimei = strHenshu

        Catch csExp As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' ����������������
    ''' </summary>
    ''' <param name="cSearchKey">���������L�[</param>
    ''' <param name="strTableName">�e�[�u����</param>
    ''' <param name="csWhere">�쐬������</param>
    ''' <param name="cfParamCollection">�p�����[�^�[�R���N�V����</param>
    ''' <remarks></remarks>
    Public Sub CreateWhereForShimei(
        ByVal cSearchKey As ABAtenaSearchKey,
        ByVal strTableName As String,
        ByRef csWhere As StringBuilder,
        ByRef cfParamCollection As UFParameterCollectionClass)

        Dim csWhereForKanaShimei As StringBuilder
        Dim csWhereForKanjiShimei As StringBuilder
        Dim cfParam As UFParameterClass

        Try

            ' �J�i�������A�����������ɂP�ł��l�����݂���ꍇ�Ɍ���������ǉ�����
            If (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaSei.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaSei2.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaMei.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanjiMeisho.Trim.Trim.RLength > 0 _
                OrElse (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki _
                        AndAlso cSearchKey.p_strKanjiMeisho2.Trim.Trim.RLength > 0)) Then

                If (csWhere.RLength > 0) Then
                    csWhere.Append(" AND ")
                Else
                    ' noop
                End If

                ' ---------------------------------------------------------------------------------
                ' �J�i�������ҏW
                csWhereForKanaShimei = New StringBuilder
                With csWhereForKanaShimei

                    ' -----------------------------------------------------------------------------
                    ' �����p�J�i����
                    If (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0) Then

                        If (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                                .Value = cSearchKey.p_strSearchKanaSeiMei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                                .Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' �����p�J�i��
                    If (cSearchKey.p_strSearchKanaSei.Trim.RLength > 0) Then

                        If (csWhereForKanaShimei.RLength > 0) Then
                            .Append(" AND ")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0) Then
                            .Append("(")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaSei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                                .Value = cSearchKey.p_strSearchKanaSei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                                .Value = cSearchKey.p_strSearchKanaSei.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' �����J�i���Q
                    If (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0) Then

                        .Append(" OR ")

                        If (cSearchKey.p_strSearchKanaSei2.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                                .Value = cSearchKey.p_strSearchKanaSei2
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                                .Value = cSearchKey.p_strSearchKanaSei2.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)

                        .Append(")")

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' �����p�J�i��
                    If (cSearchKey.p_strSearchKanaMei.Trim.RLength > 0) Then

                        If (.RLength > 0) Then
                            .Append(" AND ")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaMei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                                .Value = cSearchKey.p_strSearchKanaMei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                                .Value = cSearchKey.p_strSearchKanaMei.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------

                End With
                ' ---------------------------------------------------------------------------------

                ' ---------------------------------------------------------------------------------
                ' �����������ҏW
                csWhereForKanjiShimei = New StringBuilder
                With csWhereForKanjiShimei

                    ' -----------------------------------------------------------------------------
                    ' �����p��������
                    If (cSearchKey.p_strSearchKanjiMeisho.Trim.RLength > 0) Then

                        If (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                                .Value = cSearchKey.p_strSearchKanjiMeisho
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                                .Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' ���������Q
                    If (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki) Then

                        If (cSearchKey.p_strKanjiMeisho2.Trim.RLength > 0) Then

                            If (cSearchKey.p_strKanjiMeisho2.RIndexOf("%") < 0) Then

                                .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2)

                                ' ���������̃p�����[�^���쐬
                                cfParam = New UFParameterClass
                                With cfParam
                                    .ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                                    .Value = cSearchKey.p_strKanjiMeisho2
                                End With

                            Else

                                .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2)

                                ' ���������̃p�����[�^���쐬
                                cfParam = New UFParameterClass
                                With cfParam
                                    .ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                                    .Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                                End With

                            End If

                            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                            cfParamCollection.Add(cfParam)

                        Else
                            ' noop
                        End If

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------

                End With
                ' ---------------------------------------------------------------------------------

                ' ---------------------------------------------------------------------------------
                ' �J�i�������Ɗ����������������ݒ肳��Ă���ꍇ�A�n�q�����ŘA������
                If (csWhereForKanaShimei.RLength > 0) Then
                    If (csWhereForKanjiShimei.RLength > 0) Then
                        csWhere.AppendFormat("(({0}) OR ({1}))", csWhereForKanaShimei.ToString, csWhereForKanjiShimei.ToString)
                    Else
                        csWhere.AppendFormat("{0}", csWhereForKanaShimei.ToString)
                    End If
                Else
                    csWhere.AppendFormat("{0}", csWhereForKanjiShimei.ToString)
                End If
                ' ---------------------------------------------------------------------------------

            Else
                ' noop
            End If

        Catch csExp As Exception
            Throw
        End Try

    End Sub
    '*����ԍ� 000008 2020/01/10 �ǉ��I��

    ''' <summary>
    ''' ����������������(�I�[�o�[���[�h)
    ''' </summary>
    ''' <param name="cSearchKey">���������L�[</param>
    ''' <param name="strTableName">�e�[�u����</param>
    ''' <param name="csWhere">�쐬������</param>
    ''' <param name="cfParamCollection">�p�����[�^�[�R���N�V����</param>
    ''' <param name="strFZYHyojunTableName">�����t���W���e�[�u����</param>
    ''' <param name="blnFromAtenaRireki">�������𔻒�t���O:Optional-False</param>
    ''' <param name="intHyojunKB">�W�����Ŕ���:Optional�ʏ�</param>
    ''' <remarks></remarks>
    Public Sub CreateWhereForShimei(ByVal cSearchKey As ABAtenaSearchKey,
                                    ByVal strTableName As String,
                                    ByRef csWhere As StringBuilder,
                                    ByRef cfParamCollection As UFParameterCollectionClass,
                                    ByVal strFZYHyojunTableName As String,
                                    Optional ByVal blnFromAtenaRireki As Boolean = False,
                                    Optional ByVal intHyojunKB As ABEnumDefine.HyojunKB = ABEnumDefine.HyojunKB.KB_Tsujo)

        Dim csWhereForKanaShimei As StringBuilder
        Dim csWhereForKanjiShimei As StringBuilder
        Dim cfParam As UFParameterClass
        Dim strWhereFZYHyojunKana As String
        Dim strWhereFzyHyojunKanji As String

        Try

            ' �J�i�������A�����������ɂP�ł��l�����݂���ꍇ�Ɍ���������ǉ�����
            If (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaSei.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaSei2.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanaMei.Trim.Trim.RLength > 0 _
                OrElse cSearchKey.p_strSearchKanjiMeisho.Trim.Trim.RLength > 0 _
                OrElse (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki _
                        AndAlso cSearchKey.p_strKanjiMeisho2.Trim.Trim.RLength > 0)) Then

                If (csWhere.RLength > 0) Then
                    csWhere.Append(" AND ")
                Else
                    ' noop
                End If

                ' ---------------------------------------------------------------------------------
                ' �J�i�������ҏW
                csWhereForKanaShimei = New StringBuilder
                With csWhereForKanaShimei

                    ' -----------------------------------------------------------------------------
                    ' �����p�J�i����
                    If (cSearchKey.p_strSearchKanaSeiMei.Trim.RLength > 0) Then
                        strWhereFZYHyojunKana = CreateWhereFZYHyojunKana(cSearchKey, strFZYHyojunTableName, blnFromAtenaRireki, intHyojunKB)
                        If (strWhereFZYHyojunKana.RLength > 0) Then
                            .Append("(")
                        End If
                        If (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") < 0) Then
                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                                .Value = cSearchKey.p_strSearchKanaSeiMei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEIMEI, ABAtenaEntity.KEY_SEARCHKANASEIMEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
                                .Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)
                        If (strWhereFZYHyojunKana.RLength > 0) Then
                            If (blnFromAtenaRireki) Then
                                .Append(strWhereFZYHyojunKana)
                            Else
                                .Append(" OR ")
                                .AppendFormat("{0}.{1} IN (", strTableName, ABAtenaEntity.JUMINCD)
                                .AppendFormat("SELECT {0}.{1} FROM {0}", strFZYHyojunTableName, ABAtenaFZYHyojunEntity.JUMINCD)
                                .AppendFormat(" WHERE {0}", strWhereFZYHyojunKana)
                                .Append("))")
                            End If
                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI
                            cfParam.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI
                            cfParam.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI
                            cfParam.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
                            cfParamCollection.Add(cfParam)
                        End If
                    Else
                        ' noop
                    End If

                    ' -----------------------------------------------------------------------------
                    ' �����p�J�i��
                    If (cSearchKey.p_strSearchKanaSei.Trim.RLength > 0) Then

                        If (csWhereForKanaShimei.RLength > 0) Then
                            .Append(" AND ")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0) Then
                            .Append("(")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaSei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                                .Value = cSearchKey.p_strSearchKanaSei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
                                .Value = cSearchKey.p_strSearchKanaSei.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' �����J�i���Q
                    If (cSearchKey.p_strSearchKanaSei2.Trim.RLength > 0) Then

                        .Append(" OR ")

                        If (cSearchKey.p_strSearchKanaSei2.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                                .Value = cSearchKey.p_strSearchKanaSei2
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANASEI, ABAtenaEntity.KEY_SEARCHKANASEI2)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
                                .Value = cSearchKey.p_strSearchKanaSei2.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)

                        .Append(")")

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' �����p�J�i��
                    If (cSearchKey.p_strSearchKanaMei.Trim.RLength > 0) Then

                        If (.RLength > 0) Then
                            .Append(" AND ")
                        Else
                            ' noop
                        End If

                        If (cSearchKey.p_strSearchKanaMei.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                                .Value = cSearchKey.p_strSearchKanaMei
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANAMEI, ABAtenaEntity.KEY_SEARCHKANAMEI)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
                                .Value = cSearchKey.p_strSearchKanaMei.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------

                End With
                ' ---------------------------------------------------------------------------------

                ' ---------------------------------------------------------------------------------
                ' �����������ҏW
                csWhereForKanjiShimei = New StringBuilder
                With csWhereForKanjiShimei

                    If (cSearchKey.p_strSearchKanjiMeisho.Trim.RLength > 0) OrElse
                        (cSearchKey.p_enGaikokuHommyoKensaku = 2 And cSearchKey.p_strKanjiMeisho2.Trim.RLength > 0) Then
                        strWhereFzyHyojunKanji = CreateWhereFZYHyojunKanji(cSearchKey, strFZYHyojunTableName, blnFromAtenaRireki, intHyojunKB)
                    Else
                        strWhereFzyHyojunKanji = String.Empty
                    End If
                    If (strWhereFzyHyojunKanji.RLength > 0) Then
                        .Append("(")
                    End If
                    ' -----------------------------------------------------------------------------
                    ' �����p��������
                    If (cSearchKey.p_strSearchKanjiMeisho.Trim.RLength > 0) Then

                        If (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") < 0) Then

                            .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                                .Value = cSearchKey.p_strSearchKanjiMeisho
                            End With

                        Else

                            .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.SEARCHKANJIMEISHO, ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

                            ' ���������̃p�����[�^���쐬
                            cfParam = New UFParameterClass
                            With cfParam
                                .ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
                                .Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            End With

                        End If

                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfParamCollection.Add(cfParam)

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    ' ���������Q
                    If (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki) Then

                        If (cSearchKey.p_strKanjiMeisho2.Trim.RLength > 0) Then

                            If (cSearchKey.p_strKanjiMeisho2.RIndexOf("%") < 0) Then

                                .AppendFormat("{0}.{1} = {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2)

                                ' ���������̃p�����[�^���쐬
                                cfParam = New UFParameterClass
                                With cfParam
                                    .ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                                    .Value = cSearchKey.p_strKanjiMeisho2
                                End With

                            Else

                                .AppendFormat("{0}.{1} LIKE {2}", strTableName, ABAtenaEntity.KANJIMEISHO2, ABAtenaEntity.PARAM_KANJIMEISHO2)

                                ' ���������̃p�����[�^���쐬
                                cfParam = New UFParameterClass
                                With cfParam
                                    .ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
                                    .Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                                End With

                            End If

                            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                            cfParamCollection.Add(cfParam)

                        Else
                            ' noop
                        End If

                    Else
                        ' noop
                    End If
                    ' -----------------------------------------------------------------------------
                    If (strWhereFzyHyojunKanji.RLength > 0) Then
                        If (blnFromAtenaRireki) Then
                            .Append(strWhereFzyHyojunKanji)
                        Else
                            .Append(" OR ")
                            .AppendFormat("{0}.{1} IN (", strTableName, ABAtenaEntity.JUMINCD)
                            .AppendFormat("SELECT {0}.{1} FROM {0}", strFZYHyojunTableName, ABAtenaFZYHyojunEntity.JUMINCD)
                            .AppendFormat(" WHERE {0}", strWhereFzyHyojunKanji)
                            .Append("))")
                        End If
                        If (cSearchKey.p_strSearchKanjiMeisho.RLength > 0) Then
                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI
                            cfParam.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI
                            cfParam.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI
                            cfParam.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd
                            cfParamCollection.Add(cfParam)
                        Else
                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI
                            cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI
                            cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                            cfParamCollection.Add(cfParam)

                            cfParam = New UFParameterClass
                            cfParam.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI
                            cfParam.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd
                            cfParamCollection.Add(cfParam)
                        End If

                    End If
                End With
                ' ---------------------------------------------------------------------------------

                ' ---------------------------------------------------------------------------------
                ' �J�i�������Ɗ����������������ݒ肳��Ă���ꍇ�A�n�q�����ŘA������
                If (csWhereForKanaShimei.RLength > 0) Then
                    If (csWhereForKanjiShimei.RLength > 0) Then
                        csWhere.AppendFormat("(({0}) OR ({1}))", csWhereForKanaShimei.ToString, csWhereForKanjiShimei.ToString)
                    Else
                        csWhere.AppendFormat("{0}", csWhereForKanaShimei.ToString)
                    End If
                Else
                    csWhere.AppendFormat("{0}", csWhereForKanjiShimei.ToString)
                End If
                ' ---------------------------------------------------------------------------------

            Else
                ' noop
            End If

        Catch csExp As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' ���o����������̐����i�����t���W���E�J�i�����p�j
    ''' </summary>
    ''' <param name="cSearchKey">�����L�[</param>
    ''' <param name="strTable">�e�[�u����</param>
    ''' <param name="blnRireki">�����敪</param>
    ''' <param name="intHyojunKB">�W�����敪</param>
    ''' <returns>���o����������</returns>
    ''' <remarks></remarks>
    Public Function CreateWhereFZYHyojunKana(ByVal cSearchKey As ABAtenaSearchKey, ByVal strTable As String,
                                              ByVal blnRireki As Boolean, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder

        Try

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'WHERE��̍쐬
            csWHERE = New StringBuilder(256)

            If (blnRireki) Then
                If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    If (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") = -1) Then
                        csWHERE.Append("(")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI)
                        csWHERE.Append(")")
                    Else
                        csWHERE.Append("(")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI)
                        csWHERE.Append(")")
                    End If
                Else
                    Return String.Empty
                End If
            Else
                If (cSearchKey.p_strSearchKanaSeiMei.RIndexOf("%") = -1) Then
                    csWHERE.Append("(")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI)
                    csWHERE.Append(")")
                Else
                    csWHERE.Append("(")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI)
                    csWHERE.Append(")")
                End If
            End If

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            Throw cfAppExp

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" _
                                      + "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" _
                                      + "�y�G���[���e:" + csExp.Message + "�z")
            Throw csExp

        End Try

        Return csWHERE.ToString

    End Function

    ''' <summary>
    ''' ���o����������̐����i�����t���W���E���������p�j
    ''' </summary>
    ''' <param name="cSearchKey">�����L�[</param>
    ''' <param name="strTable">�e�[�u����</param>
    ''' <param name="blnRireki">�����敪</param>
    ''' <param name="intHyojunKB">�W�����敪</param>
    ''' <returns>���o����������</returns>
    ''' <remarks></remarks>
    Public Function CreateWhereFZYHyojunKanji(ByVal cSearchKey As ABAtenaSearchKey, ByVal strTable As String,
                                               ByVal blnRireki As Boolean, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder

        Try

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'WHERE��̍쐬
            csWHERE = New StringBuilder(256)

            If (blnRireki) Then
                If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    If (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") = -1) Then
                        csWHERE.Append("(")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI)
                        csWHERE.Append(")")
                    Else
                        csWHERE.Append("(")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI)
                        csWHERE.Append(" OR ")
                        csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI)
                        csWHERE.Append(")")
                    End If
                Else
                    Return String.Empty
                End If
            Else
                If (cSearchKey.p_strSearchKanjiMeisho.RIndexOf("%") = -1) Then
                    csWHERE.Append("(")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI)
                    csWHERE.Append(")")
                Else
                    csWHERE.Append("(")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHFRNMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHTSUSHOMEI)
                    csWHERE.Append(" OR ")
                    csWHERE.Append(strTable).Append(".").Append(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYHyojunEntity.PARAM_SEARCHKANJIHEIKIMEI)
                    csWHERE.Append(")")
                End If
            End If

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            Throw cfAppExp

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" _
                                      + "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" _
                                      + "�y�G���[���e:" + csExp.Message + "�z")
            Throw csExp

        End Try

        Return csWHERE.ToString

    End Function
End Class
