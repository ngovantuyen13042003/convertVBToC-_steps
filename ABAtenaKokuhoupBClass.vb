'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        �������ۃ}�X�^�X�V(ABAtenaKokuhoupBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/11/12�@�g�V�@�s��
'* 
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/02/26  000001     R�V�A�g�i���[�N�t���[�j������ǉ�
'* 2004/03/08  000002     �Z��X�V�����L���̔����ǉ�
'* 2004/03/26  000003     �r�W�l�XID�̕ύX�C��
'* 2005/12/01  000004     �Z��̌ʎ����X�V���ʂ�]�����邩���Ȃ����̏�����ǉ�
'* 2010/04/16  000005      VS2008�Ή��i��Áj
'* 2022/12/16  000006    �yAB-8010�z�Z���R�[�h���уR�[�h15���Ή�(����)
'* 2024/02/19  000007    �yAB-9001_1�z�ʋL�ڎ����Ή�(����)
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
Imports Densan.WorkFlow.UWCommon

Public Class ABAtenaKokuhoupBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfABConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^AB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' �R���t�B�O�f�[�^AA
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_strRsBusiId As String

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaKokuhoupBClass"
    Private Const AA_BUSSINESS_ID As String = "AA"          ' �Ɩ��R�[�h
    '*����ԍ� 000001 2004/02/26 �ǉ��J�n
    Private Const WORK_FLOW_NAME As String = "�������یʎ���"             ' ���[�N�t���[��
    Private Const DATA_NAME As String = "���ی�"                      '�f�[�^��
    '*����ԍ� 000001 2004/02/26 �ǉ��I��

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfControlData As UFControlData,
    '* �@�@                           ByVal cfConfigDataClass As UFConfigDataClass,
    '* �@�@                           ByVal cfRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@            cfConfigDataClass As UFConfigDataClass : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '* �@�@            cfRdbClass As UFRdbClass               : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        Dim cfAAUFConfigData As UFConfigDataClass
        Dim cfAAUFConfigClass As UFConfigClass

        '----------�R���t�B�O�f�[�^��"AA"�̊������擾----------------------
        cfAAUFConfigClass = New UFConfigClass()
        cfAAUFConfigData = cfAAUFConfigClass.GetConfig(AA_BUSSINESS_ID)
        m_cfAAConfigDataClass = cfAAUFConfigData
        '----------�R���t�B�O�f�[�^��"AA"�̊������擾----------------------

        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfABConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(m_cfABConfigDataClass, m_cfControlData.m_strBusinessId)

        '�󂯎�����r�W�l�XID�������o�֕ۑ�
        m_strRsBusiId = m_cfControlData.m_strBusinessId

        '*����ԍ� 000003 2004/03/26 �폜�J�n
        ''�Ɩ�ID������(AB)�ɕύX
        'm_cfControlData.m_strBusinessId = "AB"
        '*����ԍ� 000003 2004/03/26 �폜�I��

    End Sub

#End Region

    '************************************************************************************************
    '* ���\�b�h��     �������ۃ}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaKokuho(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty) As Integer
    '* 
    '* �@�\�@�@    �@  �������ۃ}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           cABKobetsuProperty As ABKobetsuProperty  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaKokuho(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaKokuho"
        Dim intUpdCnt As Integer
        Dim cABAtenaKokuhoBClass As ABAtenaKokuhoBClass
        Dim cAAKOBETSUKOKUHOParamClass(0) As localhost.AAKOBETSUKOKUHOParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaKokuhoEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()
        Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAppExp As UFAppException
        '*����ԍ� 000001 2004/02/26 �ǉ��J�n
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '�����Ǘ����c�`�r�W�l�X�N���X
        Dim csAtenaKanriEntity As DataSet                   '�����Ǘ����f�[�^�Z�b�g
        '*����ԍ� 000001 2004/02/26 �ǉ��I��
        '*����ԍ� 000004 2005/12/01 �ǉ��J�n
        Dim strJukiResult As String                         '�Z��̌��ʂ��`�F�b�N���邩�ǂ���(0:���� 1:���Ȃ�)
        '*����ԍ� 000004 2005/12/01 �ǉ��I��

        Try

            '*����ԍ� 000003 2004/03/26 �ǉ��J�n
            '�Ɩ�ID������(AB)�ɕύX
            m_cfControlData.m_strBusinessId = "AB"
            '*����ԍ� 000003 2004/03/26 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�s�������擾�i�s�����R�[�h)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '�������ۂc�`�N���X�̃C���X�^���X��
            cABAtenaKokuhoBClass = New ABAtenaKokuhoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            '�������ۃ}�X�^���o�Ăяo��
            csABAtenaKokuhoEntity = cABAtenaKokuhoBClass.GetAtenaKokuho(cABKobetsuProperty.p_strJUMINCD)

            '�ǉ��E�X�V�̔���
            If csABAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Count = 0 Then

                cDatRow = csABAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).NewRow()
                '�e���ڂ��v���p�e�B����擾
                cDatRow.Item(ABAtenaKokuhoEntity.JUMINCD) = cABKobetsuProperty.p_strJUMINCD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHONO) = cABKobetsuProperty.p_strKOKUHONO
                cDatRow.Item(ABAtenaKokuhoEntity.HIHOKENSHAGAITOKB) = String.Empty
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKB) = cABKobetsuProperty.p_strKOKUHOGAKUENKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD) = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD) = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKB) = cABKobetsuProperty.p_strKOKUHOTISHKKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO) = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO   '*DB(ABATENAKOKUHO)�ɑ��݂��ĂȂ�
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO) = cABKobetsuProperty.p_strKOKUHOHOKENSHONO

                '�s�����R�[�h
                cDatRow.Item(ABAtenaKokuhoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                '���s�����R�[�h
                cDatRow.Item(ABAtenaKokuhoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                '�f�[�^�̒ǉ�
                'csABAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Add(cDatRow)

                '�������ۃ}�X�^�ǉ����\�b�h�Ăяo��
                intUpdCnt = cABAtenaKokuhoBClass.InsertAtenaKokuho(cDatRow)
            Else

                cDatRow = csABAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows(0)
                '�e���ڂ��v���p�e�B����擾
                cDatRow.Item(ABAtenaKokuhoEntity.JUMINCD) = cABKobetsuProperty.p_strJUMINCD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHONO) = cABKobetsuProperty.p_strKOKUHONO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKB) = cABKobetsuProperty.p_strKOKUHOGAKUENKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD) = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD) = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKB) = cABKobetsuProperty.p_strKOKUHOTISHKKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO) = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO  '*DB(ABATENAKOKUHO)�ɑ��݂��ĂȂ�
                cDatRow.Item(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO) = cABKobetsuProperty.p_strKOKUHOHOKENSHONO

                '�s�����R�[�h
                cDatRow.Item(ABAtenaKokuhoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                '���s�����R�[�h
                cDatRow.Item(ABAtenaKokuhoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                '�������ۃ}�X�^�X�V���\�b�h�Ăяo��
                intUpdCnt = cABAtenaKokuhoBClass.UpdateAtenaKokuho(cDatRow)
            End If

            '�ǉ��E�X�V������0���̎����b�Z�[�W"�����̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
            If intUpdCnt = 0 Then
                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                '�G���[��`���擾
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
                '��O�𐶐�
                csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                Throw csAppExp
            End If


            '*����ԍ� 000002 2004/03/08 �ǉ��J�n
            ' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '  �����Ǘ����̎��04���ʃL�[01�̃f�[�^��S���擾����
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "11")

            '�Ǘ����̏Z��X�V���R�[�h�����݂��A�p�����[�^��"0"�̎������Z��X�V�������s��
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "0" Then

                    'Webservice��URL��WebConfig����擾���Đݒ肷��
                    cAACommonBSClass = New localhost.AACommonBSClass()
                    cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                    'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                    '�ʍ��ۃp�����[�^�̃C���X�^���X��
                    cAAKOBETSUKOKUHOParamClass(0) = New localhost.AAKOBETSUKOKUHOParamClass()

                    '�X�V�E�ǉ��������ڂ��擾
                    cAAKOBETSUKOKUHOParamClass(0).m_strJUMINCD = cABKobetsuProperty.p_strJUMINCD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHONO = cABKobetsuProperty.p_strKOKUHONO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSHIKAKUKB = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSHIKAKUKBMEISHO = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSHIKAKUKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOGAKUENKB = cABKobetsuProperty.p_strKOKUHOGAKUENKB
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOGAKUENKBMEISHO = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOGAKUENKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSHUTOKUYMD = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOSOSHITSUYMD = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKKB = cABKobetsuProperty.p_strKOKUHOTISHKKB
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKKBMEISHO = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKHONHIKB = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKHONHIKBMEISHO = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO '�����ۑސE�{��敪�������̉p�����ږ��ɊԈႢ���聖
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKHONHIKBRYAKUSHO = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKGAITOYMD = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOTISHKHIGAITOYMD = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOHOKENSHOKIGO = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO
                    cAAKOBETSUKOKUHOParamClass(0).m_strKOKUHOHOKENSHONO = cABKobetsuProperty.p_strKOKUHOHOKENSHONO

                    ' �Z��ʍ��ۍX�V���\�b�h�����s����
                    strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                    intUpdCnt = cAACommonBSClass.UpdateKBKOKUHO(strControlData, cAAKOBETSUKOKUHOParamClass)

                    '*����ԍ� 000004 2005/12/01 �ǉ��J�n
                    ' �����Ǘ����̎��04���ʃL�[22�̃f�[�^���擾����(�Z��̍X�V�����̌��ʂ𔻒f���邩�ǂ���)
                    csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "22")
                    ' �Ǘ����Ƀ��R�[�h�����݂��A�p�����[�^��"1"�̎��̓`�F�b�N���Ȃ�
                    If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                        If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                            ' ���Ұ���"1"�̂Ƃ��̓`�F�b�N���Ȃ�
                            strJukiResult = "1"
                        Else
                            ' ���Ұ���"1"�̂Ƃ��̓`�F�b�N����
                            strJukiResult = "0"
                        End If
                    Else
                        ' ���R�[�h���Ȃ��Ƃ��̓`�F�b�N����
                        strJukiResult = "0"
                    End If
                    '*����ԍ� 000004 2005/12/01 �ǉ��I��

                    '*����ԍ� 000004 2005/12/01 �C���J�n
                    '* corresponds to VS2008 Start 2010/04/16 000005
                    ''''�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
                    ''''If intUpdCnt = 0 Then
                    ''''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    ''''    '�G���[��`���擾
                    ''''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    ''''    '��O�𐶐�
                    ''''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    ''''    Throw csAppExp
                    ''''End If
                    '* corresponds to VS2008 End 2010/04/16 000005
                    If strJukiResult = "0" Then
                        ' �Ǘ���񂩂�擾�������e��"0"�̂Ƃ��̓`�F�b�N����
                        '�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
                        If intUpdCnt = 0 Then
                            cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                            '�G���[��`���擾
                            objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                            '��O�𐶐�
                            csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                            Throw csAppExp
                        End If
                    ElseIf strJukiResult = "1" Then
                        ' �`�F�b�N���Ȃ�
                    Else
                        ' �`�F�b�N���Ȃ�
                    End If
                    '*����ԍ� 000004 2005/12/01 �C���I��
                End If
            End If
            '*����ԍ� 000002 2004/03/08 �ǉ��J�n

            '*����ԍ� 000001 2004/02/26 �ǉ��J�n
            '  �����Ǘ����̎��04���ʃL�[01�̃f�[�^��S���擾����
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "02")

            '�Ǘ����̃��[�N�t���[���R�[�h�����݂��A�p�����[�^��"1"�̎��������[�N�t���[�������s��
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                    '���[�N�t���[�������\�b�h���Ă�
                    Me.WorkFlowSet(cABKobetsuProperty)
                End If
            End If
            '*����ԍ� 000001 2004/02/26 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objSoapExp As Web.Services.Protocols.SoapException             ' SoapException���L���b�`
            ' OuterXml�ɃG���[���e���i�[���Ă���B
            Dim objExpTool As UFExceptionTool = New UFExceptionTool(objSoapExp.Detail.OuterXml)
            Dim objErr As UFErrorStruct

            ' �A�v���P�[�V������O���ǂ����̔���
            If (objExpTool.IsAppException = True) Then
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objExpTool.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objExpTool.p_strErrorMessage + "�z")

                ' �t�����b�Z�[�W���쐬����
                Dim strExtMsg As String = "<P>�ΏۏZ���̃��J�o���������s���Ă��������B<BR>"

                ' �A�v���P�[�V������O���쐬����
                Dim objAppExp As UFAppException
                objAppExp = New UFAppException(objExpTool.p_strErrorMessage + strExtMsg, objExpTool.p_strErrorCode)

                ' �g���̈�̃��b�Z�[�W�ɂ��t���i���ۂɂ͂����̃��b�Z�[�W���\�������j
                UFErrorToolClass.ErrorStructSetStr(objErr, objExpTool.p_strExt)
                objErr.m_strErrorMessage += strExtMsg
                objAppExp.p_strExt = UFErrorToolClass.ErrorStructGetStr(objErr)
                ' ���b�Z�[�W��t�����Ȃ��ꍇ�͈ȉ�
                'objAppExp.p_strExt = objExpTool.p_strExt

                Throw objAppExp
            Else
                ' �V�X�e����O�̏ꍇ
                ' �G���[���O�o��
                m_cfLogClass.ErrorWrite(m_cfControlData, _
                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                    "�y�G���[���e:" + objExpTool.p_strErrorMessage + "�z")
                Throw objSoapExp
            End If
        Catch exAppExp As UFAppException                   ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                    "�y���[�j���O�R�[�h:" + exAppExp.p_strErrorCode + "�z" + _
                                    "�y���[�j���O���e:" + exAppExp.Message + "�z")
            Throw exAppExp
        Catch exExp As Exception                           ' Exception���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                    "�y�G���[���e:" + exExp.Message + "�z")
            Throw exExp
        Finally
            '���̃r�W�l�XID������
            m_cfControlData.m_strBusinessId = m_strRsBusiId
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        End Try

        Return intUpdCnt

    End Function


    '*����ԍ� 000001 2004/02/26 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �������ۃ��[�N�t���[
    '* 
    '* �\��           Public Function UpdateAtenaKokuho(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty) As Integer
    '* 
    '* �@�\�@�@    �@  �������ۃf�[�^�����[�N�t���[�֓n���B
    '* 
    '* ����           cABKobetsuProperty As ABKobetsuProperty  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Sub WorkFlowSet(ByVal cABKobetsuProperty As ABKobetsuKokuhoProperty)
        Const THIS_METHOD_NAME As String = "WorkFlowSet"
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim cwMessage As UWMessageClass                     '���[�N�t���[�N���N���X
        'Dim cwStartRetInfo As UWStartRetInfo                '���[�N�t���[�J�n�N���X
        '* corresponds to VS2008 End 2010/04/16 000005
        'Dim cUWSerialGroupId(0) As UWSerialGroupId
        'Dim cUWSerialGroupIdTemp As UWSerialGroupId
        'Dim cwDataInfo As UWStartDataInfo                                              ' ���[�N�t���[�f�[�^
        Dim strMethodName As String = Reflection.MethodBase.GetCurrentMethod.Name       ' ���[�N�t���[�f�[�^
        Dim cUWStartDataInfoForDataSet(0) As UWStartDataInfoForDataSet
        '* corresponds to VS2008 Start 2010/04/16 000005
        'Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000005
        Dim csABKokuhoEntity As New DataSet()               '�ʎ������ۃf�[�^�Z�b�g
        Dim csABKokuhoTable As DataTable                    '�ʎ������ۃf�[�^�e�[�u��
        Dim csABKokuhoRow As DataRow                        '�ʎ������ۃf�[�^���E
        Dim strNen As String                                '�쐬����
        Dim intRecCnt As Integer                            '�A�ԗp�J�E���^�[
        Dim cuCityInfoClass As New USSCityInfoClass()       '�s�����Ǘ����N���X
        Dim strCityCD As String                             '�s�����R�[�h
        Dim cABAtenaCnvBClass As ABAtenaCnvBClass

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�s�����Ǘ����̎擾
            cuCityInfoClass.GetCityInfo(m_cfControlData)
            '�s�����R�[�h�̎擾
            strCityCD = cuCityInfoClass.p_strShichosonCD(0)
            ' �쐬����(14��)�̎擾
            strNen = DateTime.Now.ToString("yyyyMMddHHmmss")
            '�A�ԗp�J�E���^�[�̏����ݒ�
            intRecCnt = 1

            ' �e�[�u���Z�b�g�̎擾
            csABKokuhoTable = Me.CreateColumnsData()
            csABKokuhoTable.TableName = ABKobetsuKokuhoEntity.TABLE_NAME
            ' �f�[�^�Z�b�g�Ƀe�[�u���Z�b�g�̒ǉ�
            csABKokuhoEntity.Tables.Add(csABKokuhoTable)

            '*****
            '*�@�P�s�ڂ̕ҏW
            '*
            '*****
            '�V�K���R�[�h�̍쐬
            csABKokuhoRow = csABKokuhoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME).NewRow
            '�e���ڂɃf�[�^���Z�b�g
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SHICHOSONCD) = strCityCD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SHIKIBETSUID) = "AA60"
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.LASTRECKB) = ""
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SAKUSEIYMD) = strNen
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.JUMINCD) = cABKobetsuProperty.p_strJUMINCD.RSubstring(3, 12)
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHONO) = cABKobetsuProperty.p_strKOKUHONO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKB) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKB
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBMEISHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBMEISHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOSHIKAKUKBRYAKUSHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOGAKUENKB) = cABKobetsuProperty.p_strKOKUHOGAKUENKB
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBMEISHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBMEISHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOGAKUENKBRYAKUSHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSHUTOKUYMD) = cABKobetsuProperty.p_strKOKUHOSHUTOKUYMD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOSOSHITSUYMD) = cABKobetsuProperty.p_strKOKUHOSOSHITSUYMD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKKB) = cABKobetsuProperty.p_strKOKUHOTISHKKB
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBMEISHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKKBRYAKUSHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKB) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKB
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBMEISHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = cABKobetsuProperty.p_strKOKUHOTISHKHONHIKBRYAKUSHO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKGAITOYMD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOTISHKHIGAITOYMD) = cABKobetsuProperty.p_strKOKUHOTISHKHIGAITOYMD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOHOKENSHOKIGO) = cABKobetsuProperty.p_strKOKUHOHOKENSHOKIGO
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.KOKUHOHOKENSHONO) = cABKobetsuProperty.p_strKOKUHOHOKENSHONO
            '�f�[�^�Z�b�g�Ƀ��R�[�h��ǉ�
            csABKokuhoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME).Rows.Add(csABKokuhoRow)

            '*****
            '*�@�ŏI�s�̕ҏW
            '*
            '*****
            '�A�ԗp�J�E���^�ɂP�𑫂�
            intRecCnt += 1
            '�V�K���R�[�h�̍쐬
            csABKokuhoRow = csABKokuhoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME).NewRow
            '�e���ڂɃf�[�^���Z�b�g
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SHICHOSONCD) = strCityCD
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SHIKIBETSUID) = "AA60"
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.LASTRECKB) = "E"
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.SAKUSEIYMD) = strNen
            csABKokuhoRow.Item(ABKobetsuKokuhoEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
            '�f�[�^�Z�b�g�Ƀ��R�[�h��ǉ�
            csABKokuhoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME).Rows.Add(csABKokuhoRow)

            '*****
            '*�@���[�N�t���[���M
            '*
            '*****
            '�f�[�^�Z�b�g�擾�N���X�̃C���X�^���X��
            cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '���[�N�t���[���M�����Ăяo��
            cABAtenaCnvBClass.WorkFlowExec(csABKokuhoEntity, WORK_FLOW_NAME, DATA_NAME)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppExp As UFAppException                   ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                    "�y���[�j���O�R�[�h:" + exAppExp.p_strErrorCode + "�z" + _
                                    "�y���[�j���O���e:" + exAppExp.Message + "�z")
            Throw exAppExp
        Catch exExp As Exception                           ' Exception���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                    "�y�G���[���e:" + exExp.Message + "�z")
            Throw exExp

        End Try

    End Sub


    '************************************************************************************************
    '* ���\�b�h��      �f�[�^�J�����쐬
    '* 
    '* �\��            Private Function CreateColumnsData() As DataTable
    '* 
    '* �@�\�@�@        ���v���J�c�a�̃J������`���쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataTable() ��[���e�[�u��
    '************************************************************************************************
    Private Function CreateColumnsData() As DataTable
        Const THIS_METHOD_NAME As String = "CreateColumnsData"
        Dim csABKokuhoTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ��[���J������`
            csABKokuhoTable = New DataTable()
            csABKokuhoTable.TableName = ABKobetsuKokuhoEntity.TABLE_NAME
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.SHIKIBETSUID, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.SAKUSEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 14
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.LASTRECKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.RENBAN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHONO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 14
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 24
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOGAKUENKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 24
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSHUTOKUYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOSOSHITSUYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 24
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 24
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKGAITOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOTISHKHIGAITOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOHOKENSHOKIGO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 32
            csDataColumn = csABKokuhoTable.Columns.Add(ABKobetsuKokuhoEntity.KOKUHOHOKENSHONO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 32

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csABKokuhoTable

    End Function
    '*����ԍ� 000001 2004/02/26 �ǉ��I��

End Class
