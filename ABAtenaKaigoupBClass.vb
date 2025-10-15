'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        �������}�X�^�X�V(ABAtenaNenkinupBClas)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/11/12�@�g�V�@�s��
'* 
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/03/26 000001     �r�W�l�XID�̕ύX�C��
'* 2005/12/01 000002     �Z��̌ʎ����X�V���ʂ�]�����邩���Ȃ����̏�����ǉ�
'* 2008/05/13 000003     �z�X�g�A�g�������N�����郏�[�N�t���[�N��������ǉ��i��Áj
'* 2008/09/30 000004     �Z��̌ʎ����}�X�^�X�V�̐���@�\��ǉ��i�g�V�j
'* 2022/12/16 000005    �yAB-8010�z�Z���R�[�h���уR�[�h15���Ή�(����)
'* 2024/02/19 000006    �yAB-9001_1�z�ʋL�ڎ����Ή�(����)
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

Public Class ABAtenaKaigoupBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfABConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^AB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' �R���t�B�O�f�[�^AA
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_strRsBusiId As String

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaKaigoupBClass"
    Private Const AA_BUSSINESS_ID As String = "AA"                            ' �Ɩ��R�[�h
    '*����ԍ� 000003 2008/05/13 �ǉ��J�n
    Private Const WORK_FLOW_NAME As String = "�������ʎ���"         ' ���[�N�t���[��
    Private Const DATA_NAME As String = "����"                      ' �f�[�^��
    '*����ԍ� 000003 2008/05/13 �ǉ��I��
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
    '* ���\�b�h��     �������}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaKaigo(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty) As Integer
    '* 
    '* �@�\�@�@    �@  �������}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           cABKobetsuProperty As ABKobetsuProperty  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaKaigo(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaKaigo"
        Dim intUpdCnt As Integer
        Dim cABAtenaKaigoBClass As ABAtenaKaigoBClass
        Dim cAAKOBETSUKAIGOParamClass() As localhost.AAKOBETSUKAIGOParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaKaigoEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()
        Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAppExp As UFAppException
        Dim intcnt As Integer
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

            '�������c�`�N���X�̃C���X�^���X��
            cABAtenaKaigoBClass = New ABAtenaKaigoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '�������}�X�^���o�Ăяo��
                csABAtenaKaigoEntity = cABAtenaKaigoBClass.GetAtenaKaigo(cABKobetsuProperty(intcnt).p_strJUMINCD)

                '�ǉ��E�X�V�̔���
                If csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).Rows.Count = 0 Then

                    cDatRow = csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).NewRow()
                    '�e���ڂ��v���p�e�B����擾
                    cDatRow.Item(ABAtenaKaigoEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaKaigoEntity.HIHOKENSHAGAITOKB) = String.Empty
                    cDatRow.Item(ABAtenaKaigoEntity.HIHKNSHANO) = cABKobetsuProperty(intcnt).p_strHIHKNSHANO
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB) = cABKobetsuProperty(intcnt).p_strSKAKHIHOKENSHAKB
                    cDatRow.Item(ABAtenaKaigoEntity.JUSHOCHITKRIKB) = cABKobetsuProperty(intcnt).p_strJUSHOCHITKRIKB
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUSHAKB) = cABKobetsuProperty(intcnt).p_strJUKYUSHAKB
                    cDatRow.Item(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD) = cABKobetsuProperty(intcnt).p_strYOKAIGJOTAIKBCD
                    cDatRow.Item(ABAtenaKaigoEntity.KAIGSKAKKB) = cABKobetsuProperty(intcnt).p_strKAIGSKAKKB
                    cDatRow.Item(ABAtenaKaigoEntity.NINTEIKAISHIYMD) = cABKobetsuProperty(intcnt).p_strNINTEIKAISHIYMD
                    cDatRow.Item(ABAtenaKaigoEntity.NINTEISHURYOYMD) = cABKobetsuProperty(intcnt).p_strNINTEISHURYOYMD
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEIYMD) = cABKobetsuProperty(intcnt).p_strJUKYUNINTEIYMD
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD) = cABKobetsuProperty(intcnt).p_strJUKYUNINTEITORIKESHIYMD

                    '�s�����R�[�h
                    cDatRow.Item(ABAtenaKaigoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '���s�����R�[�h
                    cDatRow.Item(ABAtenaKaigoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '�f�[�^�̒ǉ�
                    'csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).Rows.Add(cDatRow)

                    '�������}�X�^�ǉ����\�b�h�Ăяo��
                    intUpdCnt = cABAtenaKaigoBClass.InsertAtenaKaigo(cDatRow)
                Else

                    cDatRow = csABAtenaKaigoEntity.Tables(ABAtenaKaigoEntity.TABLE_NAME).Rows(0)
                    '�e���ڂ��v���p�e�B����擾
                    cDatRow.Item(ABAtenaKaigoEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaKaigoEntity.HIHKNSHANO) = cABKobetsuProperty(intcnt).p_strHIHKNSHANO
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD
                    cDatRow.Item(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB) = cABKobetsuProperty(intcnt).p_strSKAKHIHOKENSHAKB
                    cDatRow.Item(ABAtenaKaigoEntity.JUSHOCHITKRIKB) = cABKobetsuProperty(intcnt).p_strJUSHOCHITKRIKB
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUSHAKB) = cABKobetsuProperty(intcnt).p_strJUKYUSHAKB
                    cDatRow.Item(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD) = cABKobetsuProperty(intcnt).p_strYOKAIGJOTAIKBCD
                    cDatRow.Item(ABAtenaKaigoEntity.KAIGSKAKKB) = cABKobetsuProperty(intcnt).p_strKAIGSKAKKB
                    cDatRow.Item(ABAtenaKaigoEntity.NINTEIKAISHIYMD) = cABKobetsuProperty(intcnt).p_strNINTEIKAISHIYMD
                    cDatRow.Item(ABAtenaKaigoEntity.NINTEISHURYOYMD) = cABKobetsuProperty(intcnt).p_strNINTEISHURYOYMD
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEIYMD) = cABKobetsuProperty(intcnt).p_strJUKYUNINTEIYMD
                    cDatRow.Item(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD) = cABKobetsuProperty(intcnt).p_strJUKYUNINTEITORIKESHIYMD
                    '�s�����R�[�h
                    cDatRow.Item(ABAtenaNenkinEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '���s�����R�[�h
                    cDatRow.Item(ABAtenaNenkinEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '�������}�X�^�X�V���\�b�h�Ăяo��
                    intUpdCnt = cABAtenaKaigoBClass.UpdateAtenaKaigo(cDatRow)
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

            '*����ԍ� 000004 2008/09/30 �C���J�n
            ' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            ' �Z��ʎ����}�X�^�X�V������̎擾
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "17")

            '�Ǘ����̃��R�[�h���݂��A�p�����[�^�� "1" �̏ꍇ�̂ݍX�V���s�Ȃ�Ȃ��B
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) AndAlso _
                    CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                '�Z��ʎ����}�X�^�̍X�V�͍s��Ȃ��B
            Else

                '*����ԍ� 000002 2005/12/01 �ǉ��J�n
                '*����ԍ� 000004 2008/09/30 �폜�J�n
                '' �����Ǘ����a�N���X�̃C���X�^���X�쐬
                'cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
                '*����ԍ� 000004 2008/09/30 �폜�I��

                ' �����Ǘ����̎��04���ʃL�[25�̃f�[�^���擾����(�Z��̍X�V�����̌��ʂ𔻒f���邩�ǂ���)
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "25")
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
                cAACommonBSClass = New localhost.AACommonBSClass
                cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                ReDim cAAKOBETSUKAIGOParamClass(cABKobetsuProperty.Length - 1)

                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '�ʉ��p�����[�^�̃C���X�^���X��
                    cAAKOBETSUKAIGOParamClass(intcnt) = New localhost.AAKOBETSUKAIGOParamClass

                    '�X�V�E�ǉ��������ڂ��擾
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strHIHKNSHANO = CStr(cABKobetsuProperty(intcnt).p_strHIHKNSHANO)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strSKAKSHUTKYMD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSHUTKYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strSKAKSSHTSYMD = CStr(cABKobetsuProperty(intcnt).p_strSKAKSSHTSYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strSKAKHIHOKENSHAKB = CStr(cABKobetsuProperty(intcnt).p_strSKAKHIHOKENSHAKB)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUSHOCHITKRIKB = CStr(cABKobetsuProperty(intcnt).p_strJUSHOCHITKRIKB)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUKYUSHAKB = CStr(cABKobetsuProperty(intcnt).p_strJUKYUSHAKB)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strYOKAIGJOTAIKBCD = CStr(cABKobetsuProperty(intcnt).p_strYOKAIGJOTAIKBCD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strKAIGSKAKKB = CStr(cABKobetsuProperty(intcnt).p_strKAIGSKAKKB)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strNINTEIKAISHIYMD = CStr(cABKobetsuProperty(intcnt).p_strNINTEIKAISHIYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strNINTEISHURYOYMD = CStr(cABKobetsuProperty(intcnt).p_strNINTEISHURYOYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUKYUNINTEIYMD = CStr(cABKobetsuProperty(intcnt).p_strJUKYUNINTEIYMD)
                    cAAKOBETSUKAIGOParamClass(intcnt).m_strJUKYUNINTEITORIKESHIYMD = CStr(cABKobetsuProperty(intcnt).p_strJUKYUNINTEITORIKESHIYMD)
                Next

                ' �Z��ʉ��X�V���\�b�h�����s����
                strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                intUpdCnt = cAACommonBSClass.UpdateKBKAIGO(strControlData, cAAKOBETSUKAIGOParamClass)

                '*����ԍ� 000002 2005/12/01 �C���J�n
                '''''�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
                ''''If Not (intUpdCnt = cABKobetsuProperty.Length) Then

                ''''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                ''''    '�G���[��`���擾
                ''''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                ''''    '��O�𐶐�
                ''''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                ''''    Throw csAppExp

                ''''End If
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

            End If
            '*����ԍ� 000004 2008/09/30 �C���I��



            '*����ԍ� 000003 2008/05/13 �ǉ��J�n
            ' �����Ǘ����̎��04���ʃL�[26�̃f�[�^���擾����(��c�sνĂƂ̘A�g�����邩�ǂ����̔���)
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "26")

            ' �Ǘ����̃��[�N�t���[���R�[�h�����݂��A�p�����[�^��"1"�̎��������[�N�t���[�������s��
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                    ' ���[�N�t���[�������\�b�h���Ă�
                    Me.WorkFlowSet(cABKobetsuProperty)
                End If
            End If
            '*����ԍ� 000003 2008/05/13 �ǉ��I��

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

    '*����ԍ� 000003 2008/05/13 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ������샏�[�N�t���[
    '* 
    '* �\��           Public Sub WorkFlowSet(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty)
    '* 
    '* �@�\�@�@    �@ �������f�[�^�����[�N�t���[�֓n���B
    '* 
    '* ����           ByVal cDatRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub WorkFlowSet(ByVal cABKobetsuProperty() As ABKobetsuKaigoProperty)
        Const THIS_METHOD_NAME As String = "WorkFlowSet"
        Dim csABKaigoEntity As New DataSet                  ' �ʎ������f�[�^�Z�b�g
        Dim csABKaigoTable As DataTable                     ' �ʎ������f�[�^�e�[�u��
        Dim csABKaigoRow As DataRow                         ' �ʎ������f�[�^���E
        Dim strNen As String                                ' �쐬����
        Dim intRecCnt As Integer                            ' �A�ԗp�J�E���^�[
        Dim cuCityInfoClass As New USSCityInfoClass         ' �s�����Ǘ����N���X
        Dim strCityCD As String                             ' �s�����R�[�h
        Dim cABAtenaCnvBClass As ABAtenaCnvBClass
        Dim intIdx As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �s�����Ǘ����̎擾
            cuCityInfoClass.GetCityInfo(m_cfControlData)
            ' �s�����R�[�h�̎擾
            strCityCD = cuCityInfoClass.p_strShichosonCD(0)
            ' �쐬����(14��)�̎擾
            strNen = DateTime.Now.ToString("yyyyMMddHHmmss")
            ' �A�ԗp�J�E���^�[�̏����ݒ�
            intRecCnt = 1

            ' �e�[�u���Z�b�g�̎擾
            csABKaigoTable = Me.CreateColumnsData()
            csABKaigoTable.TableName = ABKobetsuKaigoEntity.TABLE_NAME
            ' �f�[�^�Z�b�g�Ƀe�[�u���Z�b�g�̒ǉ�
            csABKaigoEntity.Tables.Add(csABKaigoTable)

            '*****
            '*�@�P�s�ځ`�̕ҏW
            '*
            '*****
            For intIdx = 0 To cABKobetsuProperty.Length - 1
                ' �V�K���R�[�h�̍쐬
                csABKaigoRow = csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).NewRow
                ' �e���ڂɃf�[�^���Z�b�g
                csABKaigoRow.Item(ABKobetsuKaigoEntity.CITYCD) = strCityCD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SHIKIBETSUID) = "AA65"
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SAKUSEIYMD) = strNen
                csABKaigoRow.Item(ABKobetsuKaigoEntity.LASTRECKB) = ""
                csABKaigoRow.Item(ABKobetsuKaigoEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUMINCD) = cABKobetsuProperty(intIdx).p_strJUMINCD.RSubstring(3, 12)
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SHICHOSONCD) = strCityCD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.KYUSHICHOSONCD) = String.Empty
                csABKaigoRow.Item(ABKobetsuKaigoEntity.HIHKNSHANO) = cABKobetsuProperty(intIdx).p_strHIHKNSHANO
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSHUTKYMD) = cABKobetsuProperty(intIdx).p_strSKAKSHUTKYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSSHTSYMD) = cABKobetsuProperty(intIdx).p_strSKAKSSHTSYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKHIHOKENSHAKB) = cABKobetsuProperty(intIdx).p_strSKAKHIHOKENSHAKB
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUSHOCHITKRIKB) = cABKobetsuProperty(intIdx).p_strJUSHOCHITKRIKB
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUSHAKB) = cABKobetsuProperty(intIdx).p_strJUKYUSHAKB
                csABKaigoRow.Item(ABKobetsuKaigoEntity.YOKAIGJOTAIKBCD) = cABKobetsuProperty(intIdx).p_strYOKAIGJOTAIKBCD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.KAIGSKAKKB) = cABKobetsuProperty(intIdx).p_strKAIGSKAKKB
                csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEIKAISHIYMD) = cABKobetsuProperty(intIdx).p_strNINTEIKAISHIYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEISHURYOYMD) = cABKobetsuProperty(intIdx).p_strNINTEISHURYOYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEIYMD) = cABKobetsuProperty(intIdx).p_strJUKYUNINTEIYMD
                csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEITORIKESHIYMD) = cABKobetsuProperty(intIdx).p_strJUKYUNINTEITORIKESHIYMD

                '�f�[�^�Z�b�g�Ƀ��R�[�h��ǉ�
                csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).Rows.Add(csABKaigoRow)
                ' �A�ԗp�J�E���g�A�b�v
                intRecCnt += 1
            Next intIdx

            '*****
            '*�@�ŏI�s�̕ҏW
            '*
            '*****
            ' �V�K���R�[�h�̍쐬
            csABKaigoRow = csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).NewRow
            ' �e���ڂɃf�[�^���Z�b�g
            csABKaigoRow.Item(ABKobetsuKaigoEntity.CITYCD) = strCityCD
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SHIKIBETSUID) = "AA65"
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SAKUSEIYMD) = strNen
            csABKaigoRow.Item(ABKobetsuKaigoEntity.LASTRECKB) = "E"
            csABKaigoRow.Item(ABKobetsuKaigoEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUMINCD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SHICHOSONCD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.KYUSHICHOSONCD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.HIHKNSHANO) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSHUTKYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKSSHTSYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.SKAKHIHOKENSHAKB) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUSHOCHITKRIKB) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUSHAKB) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.YOKAIGJOTAIKBCD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.KAIGSKAKKB) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEIKAISHIYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.NINTEISHURYOYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEIYMD) = String.Empty
            csABKaigoRow.Item(ABKobetsuKaigoEntity.JUKYUNINTEITORIKESHIYMD) = String.Empty
            ' �f�[�^�Z�b�g�Ƀ��R�[�h��ǉ�
            csABKaigoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME).Rows.Add(csABKaigoRow)

            '*****
            '*�@���[�N�t���[���M
            '*
            '*****
            ' �f�[�^�Z�b�g�擾�N���X�̃C���X�^���X��
            cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            ' ���[�N�t���[���M�����Ăяo��
            cABAtenaCnvBClass.WorkFlowExec(csABKaigoEntity, WORK_FLOW_NAME, DATA_NAME)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppExp As UFAppException                   ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                    "�y���[�j���O�R�[�h:" + exAppExp.p_strErrorCode + "�z" + _
                                    "�y���[�j���O���e:" + exAppExp.Message + "�z")
            Throw
        Catch exExp As Exception                           ' Exception���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                    "�y�G���[���e:" + exExp.Message + "�z")
            Throw

        End Try

    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �f�[�^�J�����쐬
    '* 
    '* �\��            Private Function CreateColumnsData() As DataTable
    '* 
    '* �@�\�@�@        ���v���J�c�a�̃J������`���쐬����
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          DataTable() ��[���e�[�u��
    '************************************************************************************************
    Private Function CreateColumnsData() As DataTable
        Const THIS_METHOD_NAME As String = "CreateColumnsData"
        Dim csABKaigoTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �ʉ����J������`
            csABKaigoTable = New DataTable
            csABKaigoTable.TableName = ABKobetsuKaigoEntity.TABLE_NAME
            ' �s�����R�[�h
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.CITYCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            ' ����ID
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SHIKIBETSUID, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            ' ��������
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SAKUSEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 14
            ' �ŏI�s�敪
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.LASTRECKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            ' �A��
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.RENBAN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            ' �Z���R�[�h
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            ' �s�����R�[�h
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            ' ���s�����R�[�h
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            ' ��ی��Ҕԍ�
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.HIHKNSHANO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            ' ���i�擾��
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SKAKSHUTKYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' ���i�r����
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SKAKSSHTSYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' ���i��ی��ҋ敪
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.SKAKHIHOKENSHAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            ' �Z���n����ҋ敪
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUSHOCHITKRIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            ' �󋋎ҋ敪
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUKYUSHAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            ' �v����ԋ敪�R�[�h
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.YOKAIGJOTAIKBCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            ' �v����ԋ敪
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.KAIGSKAKKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            ' �F��L���J�n�N����
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.NINTEIKAISHIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' �F��L���I���N����
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.NINTEISHURYOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' �󋋔F��N����
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUKYUNINTEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            ' �󋋔F�����N����
            csDataColumn = csABKaigoTable.Columns.Add(ABKobetsuKaigoEntity.JUKYUNINTEITORIKESHIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8

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
            Throw

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw
        End Try

        Return csABKaigoTable

    End Function
    '*����ԍ� 000003 2008/05/13 �ǉ��I��

End Class
