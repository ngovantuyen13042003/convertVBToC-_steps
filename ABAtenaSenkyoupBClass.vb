'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        �����I���}�X�^�X�V(ABAtenaSenkyoupBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/11/11�@�g�V�@�s��
'* 
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/03/26 000001     �r�W�l�XID�̕ύX�C��
'* 2005/02/17 000002     ���X�|���X���P�FUpdateAtenaSenkyo��Atena�}�X�^�X�V�C��
'* 2006/03/17 000003     ���[��R�[�h�̍X�V������C��
'* 2010/02/09 000004     �Ǘ����ɂ��Z��ʎ����̍X�V�𐧌䂷��
'* 2024/02/19 000005    �yAB-9001_1�z�ʋL�ڎ����Ή�(����)
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

Public Class ABAtenaSenkyoupBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfABConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^AB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' �R���t�B�O�f�[�^AA
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_strRsBusiId As String

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaSenkyoupBClass"
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
    '* ���\�b�h��     �����I���}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaSenkyo(ByVal cABKobetsuProperty As ABKobetsuSenkyoProperty) As Integer
    '* 
    '* �@�\�@�@    �@  �����I���}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           cABKobetsuProperty As ABKobetsuProperty  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaSenkyo(ByVal cABKobetsuProperty() As ABKobetsuSenkyoProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaSenkyo"
        Dim intUpdCnt As Integer
        Dim cABAtenaSenkyoBClass As ABAtenaSenkyoBClass
        Dim cAAKOBETSUSENKYOParamClass(0) As localhost.AAKOBETSUSENKYOParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaSenkyoEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()

        Dim cABAtenaBClass As ABAtenaBClass
        Dim csABAtenaEntity As DataSet
        Dim cDatRowt As DataRow
        Dim cSearchKey As New ABAtenaSearchKey()            ' ���������L�[
        Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAppExp As UFAppException
        Dim intcnt As Integer

        '*����ԍ� 000004 2010/02/09 �ǉ��J�n
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '�����Ǘ����c�`�r�W�l�X�N���X
        Dim csAtenaKanriEntity As DataSet                   '�����Ǘ����f�[�^�Z�b�g
        '*����ԍ� 000004 2010/02/09 �ǉ��I��

        Try

            '*����ԍ� 000001 2004/03/26 �ǉ��J�n
            '�Ɩ�ID������(AB)�ɕύX
            m_cfControlData.m_strBusinessId = "AB"
            '*����ԍ� 000001 2004/03/26 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�s�������擾�i�s�����R�[�h)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '�����I���c�`�N���X�̃C���X�^���X��
            cABAtenaSenkyoBClass = New ABAtenaSenkyoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            '�����c�`�N���X�̃C���X�^���X��
            cABAtenaBClass = New ABAtenaBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            cSearchKey.p_strJuminYuseniKB = "1"

            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '�����I���}�X�^���o�Ăяo��
                csABAtenaSenkyoEntity = cABAtenaSenkyoBClass.GetAtenaSenkyo(cABKobetsuProperty(intcnt).p_strJUMINCD)

                '�ǉ��E�X�V�̔���
                If csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).Rows.Count = 0 Then

                    cDatRow = csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).NewRow()
                    '�e���ڂ��v���p�e�B����擾
                    cDatRow.Item(ABAtenaSenkyoEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB) = cABKobetsuProperty(intcnt).p_strSenkyoShikakuKB
                    cDatRow.Item(ABAtenaSenkyoEntity.TOROKUJOTAIKBN) = String.Empty

                    '�s�����R�[�h
                    cDatRow.Item(ABAtenaSenkyoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '���s�����R�[�h
                    cDatRow.Item(ABAtenaSenkyoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '�f�[�^�̒ǉ�
                    'csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).Rows.Add(cDatRow)

                    '�����I���}�X�^�ǉ����\�b�h�Ăяo��
                    intUpdCnt = cABAtenaSenkyoBClass.InsertAtenaSenkyo(cDatRow)

                Else

                    cDatRow = csABAtenaSenkyoEntity.Tables(ABAtenaSenkyoEntity.TABLE_NAME).Rows(0)
                    '�e���ڂ��v���p�e�B����擾
                    cDatRow.Item(ABAtenaSenkyoEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB) = cABKobetsuProperty(intcnt).p_strSenkyoShikakuKB

                    '�s�����R�[�h
                    cDatRow.Item(ABAtenaSenkyoEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '���s�����R�[�h
                    cDatRow.Item(ABAtenaSenkyoEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '�����I���}�X�^�X�V���\�b�h�Ăяo��
                    intUpdCnt = cABAtenaSenkyoBClass.UpdateAtenaSenkyo(cDatRow)
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

                ' ���������L�[�̐ݒ������
                cSearchKey.p_strJuminCD = cABKobetsuProperty(intcnt).p_strJUMINCD

                ' �����f�[�^���擾����
                csABAtenaEntity = cABAtenaBClass.GetAtenaBHoshu(1, cSearchKey)

                '�ǉ��E�X�V�̔���
                If csABAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count = 0 Then
                    intUpdCnt = 0
                Else
                    '*����ԍ� 000002 2005/02/17 �C���J�n�@000003 2006/03/17 �C���J�n
                    'Row���擾
                    cDatRowt = csABAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)

                    ' �����}�X�^�̓��[��R�[�h�ƌʃv���p�e�B�̓��[��R�[�h��������������X�V���Ȃ�
                    If Not (CType(cDatRowt.Item(ABAtenaEntity.TOHYOKUCD), String) = cABKobetsuProperty(intcnt).p_strTohyokuCD) Then
                        '���[��CD���v���p�e�B����擾
                        cDatRowt.Item(ABAtenaEntity.TOHYOKUCD) = cABKobetsuProperty(intcnt).p_strTohyokuCD

                        '�����}�X�^�ǉ����\�b�h�Ăяo��
                        intUpdCnt = cABAtenaBClass.UpdateAtenaB(cDatRowt)
                    End If

                    'cDatRowt = csABAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)
                    ''���[��CD���v���p�e�B����擾
                    'cDatRowt.Item(ABAtenaEntity.TOHYOKUCD) = cABKobetsuProperty(intcnt).p_strTohyokuCD

                    ''�����}�X�^�ǉ����\�b�h�Ăяo��
                    'intUpdCnt = cABAtenaBClass.UpdateAtenaB(cDatRowt)
                    '*����ԍ� 000002 2004/02/17 �C���I���@000003 2006/03/17 �C���J�n
                End If

                '�ǉ��E�X�V������0���̎�0��Ԃ�
                If intUpdCnt = 0 Then

                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    '�G���[��`���擾
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
                    '��O�𐶐�
                    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    Throw csAppExp
                End If

            Next

            '*����ԍ� 000004 2010/02/09 �C���J�n
            ' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '  �����Ǘ����̎��04���ʃL�[01�̃f�[�^��S���擾����
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "15")

            '�Ǘ����̏Z��X�V���R�[�h�����݂��Ȃ��A�܂��́A�p�����[�^��"0"�̎������Z��X�V�������s��
            If (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) OrElse _
                CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "0" Then

                'Webservice��URL��WebConfig����擾���Đݒ肷��
                cAACommonBSClass = New localhost.AACommonBSClass
                cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                ReDim cAAKOBETSUSENKYOParamClass(cABKobetsuProperty.Length - 1)

                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '�ʑI���p�����[�^�̃C���X�^���X��
                    cAAKOBETSUSENKYOParamClass(intcnt) = New localhost.AAKOBETSUSENKYOParamClass

                    '�X�V�E�ǉ��������ڂ��擾
                    cAAKOBETSUSENKYOParamClass(intcnt).m_strJuminCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                    cAAKOBETSUSENKYOParamClass(intcnt).m_strSenkyoShikakuKB = CStr(cABKobetsuProperty(intcnt).p_strSenkyoShikakuKB)
                    cAAKOBETSUSENKYOParamClass(intcnt).m_strTohyokuCD = CStr(cABKobetsuProperty(intcnt).p_strTohyokuCD)

                Next

                ' �Z��ʑI���X�V���\�b�h�����s����
                strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                intUpdCnt = cAACommonBSClass.UpdateKBSENKYO(strControlData, cAAKOBETSUSENKYOParamClass)

                '�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
                If Not (intUpdCnt = cABKobetsuProperty.Length) Then

                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    '�G���[��`���擾
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    '��O�𐶐�
                    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    Throw csAppExp

                End If

            End If
            ''Webservice��URL��WebConfig����擾���Đݒ肷��
            'cAACommonBSClass = New localhost.AACommonBSClass
            'cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
            ''cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

            'ReDim cAAKOBETSUSENKYOParamClass(cABKobetsuProperty.Length - 1)

            'For intcnt = 0 To cABKobetsuProperty.Length - 1

            '    '�ʑI���p�����[�^�̃C���X�^���X��
            '    cAAKOBETSUSENKYOParamClass(intcnt) = New localhost.AAKOBETSUSENKYOParamClass

            '    '�X�V�E�ǉ��������ڂ��擾
            '    cAAKOBETSUSENKYOParamClass(intcnt).m_strJuminCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
            '    cAAKOBETSUSENKYOParamClass(intcnt).m_strSenkyoShikakuKB = CStr(cABKobetsuProperty(intcnt).p_strSenkyoShikakuKB)
            '    cAAKOBETSUSENKYOParamClass(intcnt).m_strTohyokuCD = CStr(cABKobetsuProperty(intcnt).p_strTohyokuCD)

            'Next

            '' �Z��ʑI���X�V���\�b�h�����s����
            'strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
            'intUpdCnt = cAACommonBSClass.UpdateKBSENKYO(strControlData, cAAKOBETSUSENKYOParamClass)

            ''�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
            'If Not (intUpdCnt = cABKobetsuProperty.Length) Then

            '    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
            '    '�G���[��`���擾
            '    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
            '    '��O�𐶐�
            '    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '    Throw csAppExp

            'End If
            '*����ԍ� 000004 2010/02/09 �C���I��

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
