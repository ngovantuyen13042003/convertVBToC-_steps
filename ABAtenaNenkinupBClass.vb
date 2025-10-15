'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        �����N���}�X�^�X�V(ABAtenaNenkinupBClas)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/11/11�@�g�V�@�s��
'* 
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/03/26 000001     �r�W�l�XID�̕ύX�C��
'* 2005/12/01 000002     �Z��̌ʎ����X�V���ʂ�]�����邩���Ȃ����̏�����ǉ�
'* 2024/02/19 000003    �yAB-9001_1�z�ʋL�ڎ����Ή�(����)
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

Public Class ABAtenaNenkinupBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfABConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^AB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' �R���t�B�O�f�[�^AA
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_strRsBusiId As String

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaNenkinupBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h
    Private Const AA_BUSSINESS_ID As String = "AA"            ' �Ɩ��R�[�h
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

        '*����ԍ� 000001 2004/03/26 �폜�J�n
        ''�Ɩ�ID������(AB)�ɕύX
        'm_cfControlData.m_strBusinessId = "AB"
        '*����ԍ� 000001 2004/03/26 �폜�I��

    End Sub

#End Region

    '************************************************************************************************
    '* ���\�b�h��     �����N���}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaNenkin(ByVal cABKobetsuProperty As ABKobetsuNenkinProperty) As Integer
    '* 
    '* �@�\�@�@    �@  �����N���}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           cABKobetsuProperty As ABKobetsuProperty  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaNenkin(ByVal cABKobetsuProperty() As ABKobetsuNenkinProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaNenkin"
        Dim intUpdCnt As Integer
        Dim cABAtenaNenkinBClass As ABAtenaNenkinBClass
        Dim cAAKOBETSUNENKINParamClass(0) As localhost.AAKOBETSUNENKINParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csAtenaNenkinEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()
        Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAppExp As UFAppException
        '*����ԍ� 000002 2005/12/01 �ǉ��J�n
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '�����Ǘ����c�`�r�W�l�X�N���X
        Dim csAtenaKanriEntity As DataSet                   '�����Ǘ����f�[�^�Z�b�g
        Dim strJukiResult As String                         '�Z��̌��ʂ��`�F�b�N���邩�ǂ���(0:���� 1:���Ȃ�)
        '*����ԍ� 000002 2005/12/01 �ǉ��I��

        Try

            '*����ԍ� 000001 2004/03/26 �ǉ��J�n
            '�Ɩ�ID������(AB)�ɕύX
            m_cfControlData.m_strBusinessId = "AB"
            '*����ԍ� 000001 2004/03/26 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�s�������擾�i�s�����R�[�h)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '�����N���c�`�N���X�̃C���X�^���X��
            cABAtenaNenkinBClass = New ABAtenaNenkinBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            Dim intcnt As Integer
            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '�����N���}�X�^���o�Ăяo��
                csAtenaNenkinEntity = cABAtenaNenkinBClass.GetAtenaNenkin(CStr(cABKobetsuProperty(intcnt).p_strJUMINCD))

                '�ǉ��E�X�V�̔���
                If csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Count = 0 Then

                    cDatRow = csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).NewRow()
                    '�e���ڂ��v���p�e�B����擾
                    cDatRow.Item(ABAtenaNenkinEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaNenkinEntity.HIHOKENSHAGAITOKB) = String.Empty
                    cDatRow.Item(ABAtenaNenkinEntity.KSNENKNNO) = cABKobetsuProperty(intcnt).p_strKSNENKNNO
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKSHU) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKSHU
                    cDatRow.Item(ABAtenaNenkinEntity.SHUBETSUHENKOYMD) = String.Empty
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKRIYUCD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSRIYUCD
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO1) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO1) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU1) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN1) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB1) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO2) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO2) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU2) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN2) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB2) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO3) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO3) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU3) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN3) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB3) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB3
                    '�s�����R�[�h
                    cDatRow.Item(ABAtenaNenkinEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '���s�����R�[�h
                    cDatRow.Item(ABAtenaNenkinEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '�f�[�^�̒ǉ�
                    'csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Add(cDatRow)

                    '�����N���}�X�^�ǉ����\�b�h�Ăяo��
                    intUpdCnt = cABAtenaNenkinBClass.InsertAtenaNenkin(cDatRow)
                Else

                    cDatRow = csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows(0)
                    '�e���ڂ��v���p�e�B����擾
                    cDatRow.Item(ABAtenaNenkinEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaNenkinEntity.KSNENKNNO) = cABKobetsuProperty(intcnt).p_strKSNENKNNO
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKSHU) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKSHU
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKRIYUCD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD
                    cDatRow.Item(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSRIYUCD
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO1) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO1) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU1) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN1) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB1) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB1
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO2) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO2) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU2) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN2) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB2) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB2
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKIGO3) = cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNNO3) = cABKobetsuProperty(intcnt).p_strJKYNENKNNO3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNSHU3) = cABKobetsuProperty(intcnt).p_strJKYNENKNSHU3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNEDABAN3) = cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN3
                    cDatRow.Item(ABAtenaNenkinEntity.JKYNENKNKB3) = cABKobetsuProperty(intcnt).p_strJKYNENKNKB3

                    '�s�����R�[�h
                    cDatRow.Item(ABAtenaNenkinEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '���s�����R�[�h
                    cDatRow.Item(ABAtenaNenkinEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '�����N���}�X�^�X�V���\�b�h�Ăяo��
                    intUpdCnt = cABAtenaNenkinBClass.UpdateAtenaNenkin(cDatRow)
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

            Next

            '*����ԍ� 000002 2005/12/01 �ǉ��J�n
            ' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            ' �����Ǘ����̎��04���ʃL�[23�̃f�[�^���擾����(�Z��̍X�V�����̌��ʂ𔻒f���邩�ǂ���)
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "23")
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
            '*����ԍ� 000002 2005/12/01 �ǉ��I��

            'Webservice��URL��WebConfig����擾���Đݒ肷��
            cAACommonBSClass = New localhost.AACommonBSClass()
            'm_cfLogClass.WarningWrite(m_cfControlData, m_cfABConfigDataClass.p_strWebServerDomain + "Densan/Reams/AA/AA001BS/AACommonBSClass.asmx")
            cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"

            'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

            ReDim cAAKOBETSUNENKINParamClass(cABKobetsuProperty.Length - 1)

            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '�ʔN���p�����[�^�̃C���X�^���X��
                cAAKOBETSUNENKINParamClass(intcnt) = New localhost.AAKOBETSUNENKINParamClass()

                '�X�V�E�ǉ��������ڂ��擾
                cAAKOBETSUNENKINParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strKSNENKNNO = CStr(cABKobetsuProperty(intcnt).p_strKSNENKNNO)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSHUTKYMD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSHUTKSHU = CStr(cABKobetsuProperty(intcnt).p_strSKAKSHUTKSHU)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSHUTKRIYUCD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSHUTKRIYUCD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSSHTSYMD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strSKAKSSHTSRIYUCD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSSHTSRIYUCD)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKIGO1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNNO1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNNO1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNSHU1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNSHU1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNEDABAN1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKB1 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKB1)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKIGO2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNNO2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNNO2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNSHU2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNSHU2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNEDABAN2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKB2 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKB2)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKIGO3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKIGO3)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNNO3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNNO3)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNSHU3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNSHU3)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNEDABAN3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNEDABAN3)
                cAAKOBETSUNENKINParamClass(intcnt).m_strJKYNENKNKB3 = CStr(cABKobetsuProperty(intcnt).p_strJKYNENKNKB3)

            Next

            ' �Z��ʔN���X�V���\�b�h�����s����
            strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
            intUpdCnt = cAACommonBSClass.UpdateKBNENKIN(strControlData, cAAKOBETSUNENKINParamClass)

            '*����ԍ� 000002 2005/12/01 �C���J�n
            ''''''�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
            '''''If Not (intUpdCnt = cABKobetsuProperty.Length) Then

            '''''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
            '''''    '�G���[��`���擾
            '''''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
            '''''    '��O�𐶐�
            '''''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '''''    Throw csAppExp

            '''''End If

            If strJukiResult = "0" Then
                ' �Ǘ���񂩂�擾�������e��"0"�̂Ƃ��̓`�F�b�N����
                '�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
                If Not (intUpdCnt = cABKobetsuProperty.Length) Then

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
            '*����ԍ� 000002 2005/12/01 �C���I��

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

End Class
