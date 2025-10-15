'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        ������Ӄ}�X�^�X�V(ABAtenaInkanupBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/11/11�@�g�V�@�s��
'* 
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/03/26 000001     �r�W�l�XID�̕ύX�C�� 
'* 2007/03/16 000002     �G���[���擾������̕ύX��ABLOG�֏������ޏ����̒ǉ�(����)
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


Public Class ABAtenaInkanupBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfABConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^AB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' �R���t�B�O�f�[�^AA
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_strRsBusiId As String
 
    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaInkanupBClass"
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
       
        '�R���t�B�O�f�[�^��"AA"�̊������擾
        cfAAUFConfigClass = New UFConfigClass()
        cfAAUFConfigData = cfAAUFConfigClass.GetConfig(AA_BUSSINESS_ID)
        m_cfAAConfigDataClass = cfAAUFConfigData

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
    '* ���\�b�h��     ������Ӄ}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty As ABKobetsuInkanProperty) As Integer
    '* 
    '* �@�\�@�@    �@  ������Ӄ}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           cABKobetsuProperty As ABKobetsuProperty  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty() As ABKobetsuInkanProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaInkan"
        Dim intUpdCnt As Integer
        Dim cABAtenaInkanBClass As ABAtenaInkanBClass
        Dim cAAKOBETSUINKANParamClass() As localhost.AAKOBETSUINKANParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaInkanEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass
        Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAppExp As UFAppException
        Dim intcnt As Integer
       
        Try

            '*����ԍ� 000001 2004/03/26 �ǉ��J�n
            '�Ɩ�ID������(AB)�ɕύX
            m_cfControlData.m_strBusinessId = "AB"
            '*����ԍ� 000001 2004/03/26 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�s�������擾�i�s�����R�[�h)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '������ӂc�`�N���X�̃C���X�^���X��
            cABAtenaInkanBClass = New ABAtenaInkanBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            Try
                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '������Ӄ}�X�^���o�Ăяo��
                    csABAtenaInkanEntity = cABAtenaInkanBClass.GetAtenaInkan(CStr(cABKobetsuProperty(intcnt).p_strJUMINCD))

                    '�ǉ��E�X�V�̔���
                    If csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows.Count = 0 Then

                        cDatRow = csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).NewRow()
                        '�e���ڂ��v���p�e�B����擾
                        cDatRow.Item(ABAtenaInkanEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                        cDatRow.Item(ABAtenaInkanEntity.INKANNO) = cABKobetsuProperty(intcnt).p_strINKANNO
                        cDatRow.Item(ABAtenaInkanEntity.INKANTOROKUKB) = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

                        '�s�����R�[�h
                        cDatRow.Item(ABAtenaInkanEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                        '���s�����R�[�h
                        cDatRow.Item(ABAtenaInkanEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                        '�f�[�^�̒ǉ�
                        'csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows.Add(cDatRow)

                        '������Ӄ}�X�^�ǉ����\�b�h�Ăяo��
                        intUpdCnt = cABAtenaInkanBClass.InsertAtenaInkan(cDatRow)
                    Else

                        cDatRow = csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows(0)
                        '�e���ڂ��v���p�e�B����擾
                        cDatRow.Item(ABAtenaInkanEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                        cDatRow.Item(ABAtenaInkanEntity.INKANNO) = cABKobetsuProperty(intcnt).p_strINKANNO
                        cDatRow.Item(ABAtenaInkanEntity.INKANTOROKUKB) = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

                        '�s�����R�[�h
                        cDatRow.Item(ABAtenaInkanEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                        '���s�����R�[�h
                        cDatRow.Item(ABAtenaInkanEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                        '������Ӄ}�X�^�X�V���\�b�h�Ăяo��
                        intUpdCnt = cABAtenaInkanBClass.UpdateAtenaInkan(cDatRow)
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

                '*����ԍ� 000002 2007/03/16 �ǉ��J�n
            Catch exAppExp As UFAppException                   ' UFAppException���L���b�`
                ' ���ʏ�̃G���[�����O�t�@�C���ɏ�������
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppExp.Message + "�z")

                ' �����O�t�@�C���������݌�A�A�g�G���[�p���b�Z�[�W���쐬
                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                '�G���[��`���擾
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
                ' ��ABLOG�֏�������
                SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "�ʋL�ڍX�V�i��Ӂj", _
                         cABKobetsuProperty(intcnt).p_strJUMINCD, objErrorStruct.m_strErrorMessage)

                Throw exAppExp
            Catch exExp As Exception                           ' Exception���L���b�`
                ' ���ʏ�̃G���[�����O�t�@�C���ɏ�������
                ' �G���[���O�o��
                m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exExp.Message + "�z")
                ' �����O�t�@�C���������݌�A�A�g�G���[�p���b�Z�[�W���쐬
                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                '�G���[��`���擾
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
                ' ��ABLOG�֏�������
                SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "�ʋL�ڍX�V�i��Ӂj", _
                         cABKobetsuProperty(intcnt).p_strJUMINCD, objErrorStruct.m_strErrorMessage)

                Throw exExp
            End Try
            '*����ԍ� 000002 2007/03/16 �ǉ��I��


            Try
                'Webservice��URL��WebConfig����擾���Đݒ肷��
                cAACommonBSClass = New localhost.AACommonBSClass
                cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                ReDim cAAKOBETSUINKANParamClass(cABKobetsuProperty.Length - 1)

                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '�ʈ�Ӄp�����[�^�̃C���X�^���X��
                    cAAKOBETSUINKANParamClass(intcnt) = New localhost.AAKOBETSUINKANParamClass

                    '�X�V�E�ǉ��������ڂ��擾
                    cAAKOBETSUINKANParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                    cAAKOBETSUINKANParamClass(intcnt).m_strINKANNO = CStr(cABKobetsuProperty(intcnt).p_strINKANNO)
                    cAAKOBETSUINKANParamClass(intcnt).m_strINKANTOROKUKB = CStr(cABKobetsuProperty(intcnt).p_strINKANTOROKUKB)
                Next

                ' �Z��ʈ�ӍX�V���\�b�h�����s����
                strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                intUpdCnt = cAACommonBSClass.UpdateKBINKAN(strControlData, cAAKOBETSUINKANParamClass)

                '�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
                If Not (intUpdCnt = cABKobetsuProperty.Length) Then

                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    '�G���[��`���擾
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    '��O�𐶐�
                    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    Throw csAppExp

                End If

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
                    '*����ԍ� 000002 2007/03/16 �ǉ��J�n
                    ' �����O�t�@�C���������݌�A�A�g�G���[�p���b�Z�[�W���쐬
                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    '�G���[��`���擾
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    ' ��ABLOG�֏�������
                    ' ���@���@���@�����ŏZ���R�[�h��n�����ł����AcABKobetsuProperty�������ł����Ă�
                    ' �@�@�@�@�@�@�`�`����߂��Ă����G���[�ł͉��Ԗڂŗ����������f�ł��Ȃ��̂ŁA�ȉ��Œ��Index(0)��n���܂��B
                    SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "�ʋL�ڍX�V�i��Ӂj", _
                             cABKobetsuProperty(0).p_strJUMINCD, objErrorStruct.m_strErrorMessage)
                    '*����ԍ� 000002 2007/03/16 �ǉ��I��

                    Throw objAppExp
                Else
                    ' �V�X�e����O�̏ꍇ
                    ' �G���[���O�o��
                    m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExpTool.p_strErrorMessage + "�z")

                    '*����ԍ� 000002 2007/03/16 �ǉ��J�n
                    ' �����O�t�@�C���������݌�A�A�g�G���[�p���b�Z�[�W���쐬
                    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                    '�G���[��`���擾
                    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                    ' ��ABLOG�֏�������
                    SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "�ʋL�ڍX�V�i��Ӂj", _
                             cABKobetsuProperty(0).p_strJUMINCD, objErrorStruct.m_strErrorMessage)
                    '*����ԍ� 000002 2007/03/16 �ǉ��I��
                    Throw objSoapExp
                End If
            Catch exAppExp As UFAppException                   ' UFAppException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppExp.Message + "�z")

                '*����ԍ� 000002 2007/03/16 �ǉ��J�n
                ' �����O�t�@�C���������݌�A�A�g�G���[�p���b�Z�[�W���쐬
                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                '�G���[��`���擾
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                ' ��ABLOG�֏�������
                SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "�ʋL�ڍX�V�i��Ӂj", _
                         cABKobetsuProperty(0).p_strJUMINCD, objErrorStruct.m_strErrorMessage)
                '*����ԍ� 000002 2007/03/16 �ǉ��I��

                Throw exAppExp
            Catch exExp As Exception                           ' Exception���L���b�`
                ' �G���[���O�o��
                m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exExp.Message + "�z")

                '*����ԍ� 000002 2007/03/16 �ǉ��J�n
                ' �����O�t�@�C���������݌�A�A�g�G���[�p���b�Z�[�W���쐬
                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                '�G���[��`���擾
                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
                ' ��ABLOG�֏�������
                SetABLOG(cUSSCItyInfo.p_strShichosonCD(0), "ABKOB", "AC", "�ʋL�ڍX�V�i��Ӂj", _
                         cABKobetsuProperty(0).p_strJUMINCD, objErrorStruct.m_strErrorMessage)
                '*����ԍ� 000002 2007/03/16 �ǉ��I��

                Throw exExp
            End Try
        Catch
            Throw
        Finally
            '���̃r�W�l�XID������
            m_cfControlData.m_strBusinessId = m_strRsBusiId
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        End Try

        Return intUpdCnt

    End Function

    '*����ԍ� 000002 2007/03/16 �ǉ��J�n
#Region "�����X�V�G���[���OSET"
    '************************************************************************************************
    '* ���\�b�h��     �����X�V�G���[���OSET����
    '* 
    '* �\��           SetABLOG(ByVal strShichosonCD As String, _
    '* �@�@                    ByVal strShoriID As String, _
    '* �@�@                    ByVal strShoriShu As String, _
    '* �@�@                    ByVal strBasho As String, _
    '* �@�@                    ByVal strJuminCD As String, _
    '* �@�@                    ByVal strErrMsg As String)
    '* 
    '* �@�\           ABLOG�p�G���[���b�Z�[�W��SET����
    '* 
    '* ����           ByVal strShichosonCD As String : �s�����R�[�h
    '* �@�@           ByVal strShoriID as string     : �����h�c
    '* �@�@           ByVal strShoriShu As String    : �������
    '* �@�@           ByVal strBasho As String       : �G���[�����ꏊ
    '* �@�@           ByVal strJuminCD As String     : �Y���Z���R�[�h
    '* �@�@           ByVal strErrMsg As String      : �G���[���b�Z�[�W
    '* 
    '* �߂�l         Dim intCnt As Integer          : �G���[�ǉ�����
    '************************************************************************************************
    Private Function SetABLOG(ByVal strShichosonCD As String, _
                              ByVal strShoriID As String, _
                              ByVal strShoriShu As String, _
                              ByVal strBasho As String, _
                              ByVal strJuminCD As String, _
                              ByVal strErrMsg As String) As Integer
        Dim cABErrLog As ABErrLogBClass
        Dim cABErrLogPrm As ABErrLogXClass
        Dim intCnt As Integer

        cABErrLog = New ABErrLogBClass(m_cfControlData, m_cfABConfigDataClass)
        cABErrLogPrm = New ABErrLogXClass

        ' �e�퍀�ڂ��p�����[�^�ɃZ�b�g
        cABErrLogPrm.p_strShichosonCD = strShichosonCD
        cABErrLogPrm.p_strShoriID = strShoriID
        cABErrLogPrm.p_strShoriShu = strShoriShu
        cABErrLogPrm.p_strMsg5 = strBasho
        cABErrLogPrm.p_strMsg6 = strJuminCD
        cABErrLogPrm.p_strMsg7 = strErrMsg

        intCnt = cABErrLog.InsertABErrLog(cABErrLogPrm)

        Return intCnt

    End Function

#End Region
    '*����ԍ� 000002 2007/03/16 �ǉ��I��

    '*����ԍ� 000002 2007/03/16 �폜�J�n
    ' ��Try-Catch�̍���啝�ɕς���̂ŋ��\�[�X�����̂܂܎c���Ă����܂��B
#Region "���\�[�X UpdateAtenaInkan"
    ''************************************************************************************************
    ''* ���\�b�h��     ������Ӄ}�X�^�X�V
    ''* 
    ''* �\��           Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty As ABKobetsuInkanProperty) As Integer
    ''* 
    ''* �@�\�@�@    �@  ������Ӄ}�X�^�̃f�[�^���X�V����B
    ''* 
    ''* ����           cABKobetsuProperty As ABKobetsuProperty  :�X�V�f�[�^
    ''* 
    ''* �߂�l         �X�V����(Integer)
    ''************************************************************************************************
    'Public Function UpdateAtenaInkan(ByVal cABKobetsuProperty() As ABKobetsuInkanProperty) As Integer
    '    Const THIS_METHOD_NAME As String = "UpdateAtenaInkan"
    '    Dim intUpdCnt As Integer
    '    Dim cABAtenaInkanBClass As ABAtenaInkanBClass
    '    Dim cAAKOBETSUINKANParamClass() As localhost.AAKOBETSUINKANParamClass
    '    Dim cAACommonBSClass As localhost.AACommonBSClass
    '    Dim csABAtenaInkanEntity As DataSet
    '    Dim cDatRow As DataRow
    '    Dim strControlData As String
    '    Dim cUSSCItyInfo As New USSCityInfoClass()
    '    Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
    '    Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
    '    Dim csAppExp As UFAppException
    '    Dim intcnt As Integer

    '    Try

    '        '*����ԍ� 000001 2004/03/26 �ǉ��J�n
    '        '�Ɩ�ID������(AB)�ɕύX
    '        m_cfControlData.m_strBusinessId = "AB"
    '        '*����ԍ� 000001 2004/03/26 �ǉ��I��

    '        ' �f�o�b�O���O�o��
    '        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        '�s�������擾�i�s�����R�[�h)
    '        cUSSCItyInfo.GetCityInfo(m_cfControlData)

    '        '������ӂc�`�N���X�̃C���X�^���X��
    '        cABAtenaInkanBClass = New ABAtenaInkanBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

    '        For intcnt = 0 To cABKobetsuProperty.Length - 1

    '            '������Ӄ}�X�^���o�Ăяo��
    '            csABAtenaInkanEntity = cABAtenaInkanBClass.GetAtenaInkan(CStr(cABKobetsuProperty(intcnt).p_strJUMINCD))

    '            '�ǉ��E�X�V�̔���
    '            If csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows.Count = 0 Then

    '                cDatRow = csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).NewRow()
    '                '�e���ڂ��v���p�e�B����擾
    '                cDatRow.Item(ABAtenaInkanEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
    '                cDatRow.Item(ABAtenaInkanEntity.INKANNO) = cABKobetsuProperty(intcnt).p_strINKANNO
    '                cDatRow.Item(ABAtenaInkanEntity.INKANTOROKUKB) = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

    '                '�s�����R�[�h
    '                cDatRow.Item(ABAtenaInkanEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
    '                '���s�����R�[�h
    '                cDatRow.Item(ABAtenaInkanEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

    '                '�f�[�^�̒ǉ�
    '                'csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows.Add(cDatRow)

    '                '������Ӄ}�X�^�ǉ����\�b�h�Ăяo��
    '                intUpdCnt = cABAtenaInkanBClass.InsertAtenaInkan(cDatRow)
    '            Else

    '                cDatRow = csABAtenaInkanEntity.Tables(ABAtenaInkanEntity.TABLE_NAME).Rows(0)
    '                '�e���ڂ��v���p�e�B����擾
    '                cDatRow.Item(ABAtenaInkanEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
    '                cDatRow.Item(ABAtenaInkanEntity.INKANNO) = cABKobetsuProperty(intcnt).p_strINKANNO
    '                cDatRow.Item(ABAtenaInkanEntity.INKANTOROKUKB) = cABKobetsuProperty(intcnt).p_strINKANTOROKUKB

    '                '�s�����R�[�h
    '                cDatRow.Item(ABAtenaInkanEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
    '                '���s�����R�[�h
    '                cDatRow.Item(ABAtenaInkanEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

    '                '������Ӄ}�X�^�X�V���\�b�h�Ăяo��
    '                intUpdCnt = cABAtenaInkanBClass.UpdateAtenaInkan(cDatRow)
    '            End If

    '            '�ǉ��E�X�V������0���̎����b�Z�[�W"�����̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
    '            If intUpdCnt = 0 Then

    '                cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
    '                '�G���[��`���擾
    '                objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003004)
    '                '��O�𐶐�
    '                csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
    '                Throw csAppExp
    '            End If

    '        Next

    '        'Webservice��URL��WebConfig����擾���Đݒ肷��
    '        cAACommonBSClass = New localhost.AACommonBSClass()
    '        cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
    '        'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

    '        ReDim cAAKOBETSUINKANParamClass(cABKobetsuProperty.Length - 1)

    '        For intcnt = 0 To cABKobetsuProperty.Length - 1

    '            '�ʈ�Ӄp�����[�^�̃C���X�^���X��
    '            cAAKOBETSUINKANParamClass(intcnt) = New localhost.AAKOBETSUINKANParamClass()

    '            '�X�V�E�ǉ��������ڂ��擾
    '            cAAKOBETSUINKANParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
    '            cAAKOBETSUINKANParamClass(intcnt).m_strINKANNO = CStr(cABKobetsuProperty(intcnt).p_strINKANNO)
    '            cAAKOBETSUINKANParamClass(intcnt).m_strINKANTOROKUKB = CStr(cABKobetsuProperty(intcnt).p_strINKANTOROKUKB)
    '        Next

    '        ' �Z��ʈ�ӍX�V���\�b�h�����s����
    '        strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
    '        intUpdCnt = cAACommonBSClass.UpdateKBINKAN(strControlData, cAAKOBETSUINKANParamClass)

    '        '�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
    '        If Not (intUpdCnt = cABKobetsuProperty.Length) Then

    '            cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
    '            '�G���[��`���擾
    '            objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
    '            '��O�𐶐�
    '            csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
    '            Throw csAppExp

    '        End If

    '    Catch objSoapExp As Web.Services.Protocols.SoapException             ' SoapException���L���b�`
    '        ' OuterXml�ɃG���[���e���i�[���Ă���B
    '        Dim objExpTool As UFExceptionTool = New UFExceptionTool(objSoapExp.Detail.OuterXml)
    '        Dim objErr As UFErrorStruct

    '        ' �A�v���P�[�V������O���ǂ����̔���
    '        If (objExpTool.IsAppException = True) Then
    '            ' ���[�j���O���O�o��
    '            m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                    "�y���[�j���O�R�[�h:" + objExpTool.p_strErrorCode + "�z" + _
    '                                    "�y���[�j���O���e:" + objExpTool.p_strErrorMessage + "�z")

    '            ' �t�����b�Z�[�W���쐬����
    '            Dim strExtMsg As String = "<P>�ΏۏZ���̃��J�o���������s���Ă��������B<BR>"

    '            ' �A�v���P�[�V������O���쐬����
    '            Dim objAppExp As UFAppException
    '            objAppExp = New UFAppException(objExpTool.p_strErrorMessage + strExtMsg, objExpTool.p_strErrorCode)

    '            ' �g���̈�̃��b�Z�[�W�ɂ��t���i���ۂɂ͂����̃��b�Z�[�W���\�������j
    '            UFErrorToolClass.ErrorStructSetStr(objErr, objExpTool.p_strExt)
    '            objErr.m_strErrorMessage += strExtMsg
    '            objAppExp.p_strExt = UFErrorToolClass.ErrorStructGetStr(objErr)
    '            ' ���b�Z�[�W��t�����Ȃ��ꍇ�͈ȉ�
    '            'objAppExp.p_strExt = objExpTool.p_strExt

    '            Throw objAppExp
    '        Else
    '            ' �V�X�e����O�̏ꍇ
    '            ' �G���[���O�o��
    '            m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                "�y�G���[���e:" + objExpTool.p_strErrorMessage + "�z")
    '            Throw objSoapExp
    '        End If
    '    Catch exAppExp As UFAppException                   ' UFAppException���L���b�`
    '        ' ���[�j���O���O�o��
    '        m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                "�y���[�j���O�R�[�h:" + exAppExp.p_strErrorCode + "�z" + _
    '                                "�y���[�j���O���e:" + exAppExp.Message + "�z")
    '        Throw exAppExp
    '    Catch exExp As Exception                           ' Exception���L���b�`
    '        ' �G���[���O�o��
    '        m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                "�y�G���[���e:" + exExp.Message + "�z")
    '        Throw exExp
    '    Finally
    '        '���̃r�W�l�XID������
    '        m_cfControlData.m_strBusinessId = m_strRsBusiId
    '        ' �f�o�b�O���O�o��
    '        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '    End Try

    '    Return intUpdCnt

    'End Function
#End Region
    '*����ԍ� 000002 2007/03/16 �폜�I��
End Class
