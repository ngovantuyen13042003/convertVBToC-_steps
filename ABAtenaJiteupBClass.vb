'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        ��������}�X�^�X�V(ABAtenaJiteupBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/11/11�@�g�V�@�s��
'* 
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/03/26 000001     �r�W�l�XID�̕ύX�C�� 
'* 2005/10/13 000002     ��c�s�z�X�g�A�g�i���[�N�t���[�j������ǉ�(�}���S���R)
'* 2005/10/25 000003     ��c�s�z�X�g�A�g�i���[�N�t���[�j�������C��(�}���S���R)
'* 2005/12/01 000004     �Z��̌ʎ����X�V���ʂ�]�����邩���Ȃ����̏�����ǉ�
'* 2010/04/09 000005     �Ǘ����ɂ��Z��ʎ����̍X�V�𐧌䂷��i��Áj
'* 2010/04/16 000006     VS2008�Ή��i��Áj
'* 2022/12/16 000007    �yAB-8010�z�Z���R�[�h���уR�[�h15���Ή�(����)
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
'*����ԍ� 000002 2005/10/13 �ǉ��J�n
Imports Densan.WorkFlow.UWCommon
'*����ԍ� 000002 2005/10/13 �ǉ��I��

Public Class ABAtenaJiteupBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfABConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^AB
    Private m_cfAAConfigDataClass As UFConfigDataClass       ' �R���t�B�O�f�[�^AA
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_strRsBusiId As String

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaJiteupBClass"
    Private Const AA_BUSSINESS_ID As String = "AA"                              ' �Ɩ��R�[�h
    '*����ԍ� 000002 2005/10/13 �ǉ��J�n
    Private Const WORK_FLOW_NAME As String = "��������ʎ���"         ' ���[�N�t���[��
    Private Const DATA_NAME As String = "�����"                      ' �f�[�^��
    '*����ԍ� 000002 2005/10/13 �ǉ��I��
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
    '* ���\�b�h��     ��������}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaJite(ByVal cABKobetsuProperty As ABKobetsuJiteProperty) As Integer
    '* 
    '* �@�\�@�@    �@  ��������}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           cABKobetsuProperty As ABKobetsuProperty  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaJite(ByVal cABKobetsuProperty() As ABKobetsuJiteProperty) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaJite"
        Dim intUpdCnt As Integer
        Dim cABAtenaJiteBClass As ABAtenaJiteBClass
        Dim cAAKOBETSUJITEParamClass(0) As localhost.AAKOBETSUJITEParamClass
        Dim cAACommonBSClass As localhost.AACommonBSClass
        Dim csABAtenaJiteEntity As DataSet
        Dim cDatRow As DataRow
        Dim strControlData As String
        Dim cUSSCItyInfo As New USSCityInfoClass()
        Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAppExp As UFAppException
        '*����ԍ� 000002 2005/10/13 �ǉ��J�n
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '�����Ǘ����c�`�r�W�l�X�N���X
        Dim csAtenaKanriEntity As DataSet                   '�����Ǘ����f�[�^�Z�b�g
        '*����ԍ� 000002 2005/10/13 �ǉ��I��
        '*����ԍ� 000004 2005/12/01 �ǉ��J�n
        Dim strJukiResult As String                         '�Z��̌��ʂ��`�F�b�N���邩�ǂ���(0:���� 1:���Ȃ�)
        '*����ԍ� 000004 2005/12/01 �ǉ��I��

        Try

            '*����ԍ� 000001 2004/03/26 �ǉ��J�n
            '�Ɩ�ID������(AB)�ɕύX
            m_cfControlData.m_strBusinessId = "AB"
            '*����ԍ� 000001 2004/03/26 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�s�������擾�i�s�����R�[�h)
            cUSSCItyInfo.GetCityInfo(m_cfControlData)

            '��������c�`�N���X�̃C���X�^���X��
            cABAtenaJiteBClass = New ABAtenaJiteBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)

            Dim intcnt As Integer
            For intcnt = 0 To cABKobetsuProperty.Length - 1

                '��������}�X�^���o�Ăяo��
                csABAtenaJiteEntity = cABAtenaJiteBClass.GetAtenaJite(cABKobetsuProperty(intcnt).p_strJUMINCD)

                '�ǉ��E�X�V�̔���
                If csABAtenaJiteEntity.Tables(ABAtenaJiteEntity.TABLE_NAME).Rows.Count = 0 Then

                    cDatRow = csABAtenaJiteEntity.Tables(ABAtenaJiteEntity.TABLE_NAME).NewRow()
                    '�e���ڂ��v���p�e�B����擾
                    cDatRow.Item(ABAtenaJiteEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATEHIYOKB) = cABKobetsuProperty(intcnt).p_strHIYOKB
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATESTYM) = cABKobetsuProperty(intcnt).p_strJIDOTEATESTYM
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATEEDYM) = cABKobetsuProperty(intcnt).p_strJIDOTEATEEDYM

                    '�s�����R�[�h
                    cDatRow.Item(ABAtenaJiteEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '���s�����R�[�h
                    cDatRow.Item(ABAtenaJiteEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '�f�[�^�̒ǉ�
                    'csABAtenaJiteEntity.Tables(ABAtenaJiteEntity.TABLE_NAME).Rows.Add(cDatRow)

                    '��������}�X�^�ǉ����\�b�h�Ăяo��
                    intUpdCnt = cABAtenaJiteBClass.InsertAtenaJite(cDatRow)
                Else

                    cDatRow = csABAtenaJiteEntity.Tables(ABAtenaJiteEntity.TABLE_NAME).Rows(0)
                    '�e���ڂ��v���p�e�B����擾
                    cDatRow.Item(ABAtenaJiteEntity.JUMINCD) = cABKobetsuProperty(intcnt).p_strJUMINCD
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATEHIYOKB) = cABKobetsuProperty(intcnt).p_strHIYOKB
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATESTYM) = cABKobetsuProperty(intcnt).p_strJIDOTEATESTYM
                    cDatRow.Item(ABAtenaJiteEntity.JIDOTEATEEDYM) = cABKobetsuProperty(intcnt).p_strJIDOTEATEEDYM
                    '�s�����R�[�h
                    cDatRow.Item(ABAtenaJiteEntity.SHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)
                    '���s�����R�[�h
                    cDatRow.Item(ABAtenaJiteEntity.KYUSHICHOSONCD) = cUSSCItyInfo.p_strShichosonCD(0)

                    '��������}�X�^�X�V���\�b�h�Ăяo��
                    intUpdCnt = cABAtenaJiteBClass.UpdateAtenaJite(cDatRow)
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

            '*����ԍ� 000005 2010/04/09 �C���J�n
            ' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '  �����Ǘ����̎�ʃL�[:04,���ʃL�[:16�̃f�[�^���擾����
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "16")

            '�Ǘ����̏Z��X�V���R�[�h�����݂��Ȃ��A�܂��́A�p�����[�^��"0"�̎������Z��X�V�������s��
            If (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) OrElse _
               (CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "0") Then

                ' �����Ǘ����̎��04���ʃL�[24�̃f�[�^���擾����(�Z��̍X�V�����̌��ʂ𔻒f���邩�ǂ���)
                csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "24")
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

                'Webservice��URL��WebConfig����擾���Đݒ肷��
                cAACommonBSClass = New localhost.AACommonBSClass
                cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
                'cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

                ReDim cAAKOBETSUJITEParamClass(cABKobetsuProperty.Length - 1)

                For intcnt = 0 To cABKobetsuProperty.Length - 1

                    '�ʎ���p�����[�^�̃C���X�^���X��
                    cAAKOBETSUJITEParamClass(intcnt) = New localhost.AAKOBETSUJITEParamClass

                    '�X�V�E�ǉ��������ڂ��擾
                    cAAKOBETSUJITEParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
                    cAAKOBETSUJITEParamClass(intcnt).m_strHIYOKB = CStr(cABKobetsuProperty(intcnt).p_strHIYOKB)
                    cAAKOBETSUJITEParamClass(intcnt).m_strJIDOTEATESTYM = CStr(cABKobetsuProperty(intcnt).p_strJIDOTEATESTYM)
                    cAAKOBETSUJITEParamClass(intcnt).m_strJIDOTEATEEDYM = CStr(cABKobetsuProperty(intcnt).p_strJIDOTEATEEDYM)

                Next

                ' �Z��ʎ���X�V���\�b�h�����s����
                strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
                intUpdCnt = cAACommonBSClass.UpdateKBJITE(strControlData, cAAKOBETSUJITEParamClass)

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
            Else
            End If

            ''*����ԍ� 000004 2005/12/01 �ǉ��J�n
            '' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            'cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '' �����Ǘ����̎��04���ʃL�[24�̃f�[�^���擾����(�Z��̍X�V�����̌��ʂ𔻒f���邩�ǂ���)
            'csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "24")
            '' �Ǘ����Ƀ��R�[�h�����݂��A�p�����[�^��"1"�̎��̓`�F�b�N���Ȃ�
            'If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
            '    If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
            '        ' ���Ұ���"1"�̂Ƃ��̓`�F�b�N���Ȃ�
            '        strJukiResult = "1"
            '    Else
            '        ' ���Ұ���"1"�̂Ƃ��̓`�F�b�N����
            '        strJukiResult = "0"
            '    End If
            'Else
            '    ' ���R�[�h���Ȃ��Ƃ��̓`�F�b�N����
            '    strJukiResult = "0"
            'End If
            ''*����ԍ� 000004 2005/12/01 �ǉ��I��

            ''Webservice��URL��WebConfig����擾���Đݒ肷��
            'cAACommonBSClass = New localhost.AACommonBSClass
            'cAACommonBSClass.Url = m_cfAAConfigDataClass.p_strWebServiceDomain + m_cfAAConfigDataClass.p_strWWWroot + "AA001BS/AACommonBSClass.asmx"
            ''cAACommonBSClass.Url = "http://localhost/Densan/Reams/AA/AA001BS/AACommonBSClass.asmx"

            'ReDim cAAKOBETSUJITEParamClass(cABKobetsuProperty.Length - 1)

            'For intcnt = 0 To cABKobetsuProperty.Length - 1

            '    '�ʎ���p�����[�^�̃C���X�^���X��
            '    cAAKOBETSUJITEParamClass(intcnt) = New localhost.AAKOBETSUJITEParamClass

            '    '�X�V�E�ǉ��������ڂ��擾
            '    cAAKOBETSUJITEParamClass(intcnt).m_strJUMINCD = CStr(cABKobetsuProperty(intcnt).p_strJUMINCD)
            '    cAAKOBETSUJITEParamClass(intcnt).m_strHIYOKB = CStr(cABKobetsuProperty(intcnt).p_strHIYOKB)
            '    cAAKOBETSUJITEParamClass(intcnt).m_strJIDOTEATESTYM = CStr(cABKobetsuProperty(intcnt).p_strJIDOTEATESTYM)
            '    cAAKOBETSUJITEParamClass(intcnt).m_strJIDOTEATEEDYM = CStr(cABKobetsuProperty(intcnt).p_strJIDOTEATEEDYM)

            'Next

            '' �Z��ʎ���X�V���\�b�h�����s����
            'strControlData = UFControlToolClass.ControlGetStr(m_cfControlData)
            'intUpdCnt = cAACommonBSClass.UpdateKBJITE(strControlData, cAAKOBETSUJITEParamClass)

            ''*����ԍ� 000004 2005/12/01 �C���J�n
            '''''�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
            ''''If Not (intUpdCnt = cABKobetsuProperty.Length) Then

            ''''    cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
            ''''    '�G���[��`���擾
            ''''    objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
            ''''    '��O�𐶐�
            ''''    csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            ''''    Throw csAppExp

            ''''End If

            'If strJukiResult = "0" Then
            '    ' �Ǘ���񂩂�擾�������e��"0"�̂Ƃ��̓`�F�b�N����
            '    '�ǉ��E�X�V������0���̎����b�Z�[�W"�Z��̌ʎ����̍X�V�͐���ɍs���܂���ł���"��Ԃ�
            '    If Not (intUpdCnt = cABKobetsuProperty.Length) Then

            '        cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
            '        '�G���[��`���擾
            '        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003002)
            '        '��O�𐶐�
            '        csAppExp = New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '        Throw csAppExp

            '    End If
            'ElseIf strJukiResult = "1" Then
            '    ' �`�F�b�N���Ȃ�
            'Else
            '    ' �`�F�b�N���Ȃ�
            'End If
            ''*����ԍ� 000004 2005/12/01 �C���I��
            '*����ԍ� 000005 2010/04/09 �C���I��

            '*����ԍ� 000002 2005/10/13 �ǉ��J�n
            '*����ԍ� 000004 2005/12/01 �폜�J�n
            ' ��̂ق��ň����Ǘ������擾����̂ŁA�����ŃC���X�^���X�쐬����
            '''' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            '* corresponds to VS2008 Start 2010/04/16 000006
            ''''cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            '* corresponds to VS2008 End 2010/04/16 000006
            '*����ԍ� 000004 2005/12/01 �폜�I��

            ' �����Ǘ����̎��04���ʃL�[21�̃f�[�^���擾����(��c�sνĂƂ̘A�g�����邩�ǂ����̔���)
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "21")

            ' �Ǘ����̃��[�N�t���[���R�[�h�����݂��A�p�����[�^��"1"�̎��������[�N�t���[�������s��
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
                If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then
                    ' ���[�N�t���[�������\�b�h���Ă�
                    Me.WorkFlowSet(cABKobetsuProperty)
                End If
            End If
            '*����ԍ� 000002 2005/10/13 �ǉ��I��

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

    '*����ԍ� 000002 2005/10/13 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������蓖���[�N�t���[
    '* 
    '* �\��           Public Sub WorkFlowSet(ByVal cABKobetsuProperty() As ABKobetsuJiteProperty)
    '* 
    '* �@�\�@�@    �@ ���������蓖�f�[�^�����[�N�t���[�֓n���B
    '* 
    '* ����           ByVal cDatRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub WorkFlowSet(ByVal cABKobetsuProperty() As ABKobetsuJiteProperty)
        Const THIS_METHOD_NAME As String = "WorkFlowSet"
        Dim csABJiteEntity As New DataSet()                 ' �ʎ�������f�[�^�Z�b�g
        Dim csABJiteTable As DataTable                      ' �ʎ�������f�[�^�e�[�u��
        Dim csABJiteRow As DataRow                          ' �ʎ�������f�[�^���E
        Dim strNen As String                                ' �쐬����
        Dim intRecCnt As Integer                            ' �A�ԗp�J�E���^�[
        Dim cuCityInfoClass As New USSCityInfoClass()       ' �s�����Ǘ����N���X
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
            csABJiteTable = Me.CreateColumnsData()
            csABJiteTable.TableName = ABKobetsuJiteEntity.TABLE_NAME
            ' �f�[�^�Z�b�g�Ƀe�[�u���Z�b�g�̒ǉ�
            csABJiteEntity.Tables.Add(csABJiteTable)

            '*****
            '*�@�P�s�ځ`�̕ҏW
            '*
            '*****
            For intIdx = 0 To cABKobetsuProperty.Length - 1
                ' �V�K���R�[�h�̍쐬
                csABJiteRow = csABJiteEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME).NewRow
                ' �e���ڂɃf�[�^���Z�b�g
                csABJiteRow.Item(ABKobetsuJiteEntity.SHICHOSONCD) = strCityCD
                csABJiteRow.Item(ABKobetsuJiteEntity.SHIKIBETSUID) = "AA64"
                csABJiteRow.Item(ABKobetsuJiteEntity.LASTRECKB) = ""
                csABJiteRow.Item(ABKobetsuJiteEntity.SAKUSEIYMD) = strNen
                csABJiteRow.Item(ABKobetsuJiteEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
                csABJiteRow.Item(ABKobetsuJiteEntity.JUMINCD) = cABKobetsuProperty(intIdx).p_strJUMINCD
                '*����ԍ� 000003 2005/10/25 �ǉ��J�n
                csABJiteRow.Item(ABKobetsuJiteEntity.CITYCD) = strCityCD
                csABJiteRow.Item(ABKobetsuJiteEntity.KYUCITYCD) = String.Empty
                '*����ԍ� 000003 2005/10/25 �ǉ��I��
                csABJiteRow.Item(ABKobetsuJiteEntity.HIYOKB) = cABKobetsuProperty(intIdx).p_strHIYOKB
                csABJiteRow.Item(ABKobetsuJiteEntity.JIDOTEATESTYM) = cABKobetsuProperty(intIdx).p_strJIDOTEATESTYM
                csABJiteRow.Item(ABKobetsuJiteEntity.JIDOTEATEEDYM) = cABKobetsuProperty(intIdx).p_strJIDOTEATEEDYM

                '�f�[�^�Z�b�g�Ƀ��R�[�h��ǉ�
                csABJiteEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME).Rows.Add(csABJiteRow)
                ' �A�ԗp�J�E���g�A�b�v
                intRecCnt += 1
            Next intIdx

            '*****
            '*�@�ŏI�s�̕ҏW
            '*
            '*****
            ' �V�K���R�[�h�̍쐬
            csABJiteRow = csABJiteEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME).NewRow
            ' �e���ڂɃf�[�^���Z�b�g
            csABJiteRow.Item(ABKobetsuJiteEntity.SHICHOSONCD) = strCityCD
            csABJiteRow.Item(ABKobetsuJiteEntity.SHIKIBETSUID) = "AA64"
            csABJiteRow.Item(ABKobetsuJiteEntity.LASTRECKB) = "E"
            csABJiteRow.Item(ABKobetsuJiteEntity.SAKUSEIYMD) = strNen
            csABJiteRow.Item(ABKobetsuJiteEntity.RENBAN) = CType(intRecCnt, String).RPadLeft(7, "0"c)
            csABJiteRow.Item(ABKobetsuJiteEntity.JUMINCD) = String.Empty
            '*����ԍ� 000003 2005/10/25 �ǉ��J�n
            csABJiteRow.Item(ABKobetsuJiteEntity.CITYCD) = String.Empty
            csABJiteRow.Item(ABKobetsuJiteEntity.KYUCITYCD) = String.Empty
            csABJiteRow.Item(ABKobetsuJiteEntity.HIYOKB) = String.Empty
            csABJiteRow.Item(ABKobetsuJiteEntity.JIDOTEATESTYM) = String.Empty
            csABJiteRow.Item(ABKobetsuJiteEntity.JIDOTEATEEDYM) = String.Empty
            '*����ԍ� 000003 2005/10/25 �ǉ��I��
            ' �f�[�^�Z�b�g�Ƀ��R�[�h��ǉ�
            csABJiteEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME).Rows.Add(csABJiteRow)

            '*****
            '*�@���[�N�t���[���M
            '*
            '*****
            ' �f�[�^�Z�b�g�擾�N���X�̃C���X�^���X��
            cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfABConfigDataClass, m_cfRdbClass)
            ' ���[�N�t���[���M�����Ăяo��
            cABAtenaCnvBClass.WorkFlowExec(csABJiteEntity, WORK_FLOW_NAME, DATA_NAME)

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
        Dim csABJiteTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �ʎ�����J������`
            csABJiteTable = New DataTable()
            csABJiteTable.TableName = ABKobetsuJiteEntity.TABLE_NAME
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.SHIKIBETSUID, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.SAKUSEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 14
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.LASTRECKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.RENBAN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            '*����ԍ� 000003 2005/10/25 �ǉ��J�n
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.CITYCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.KYUCITYCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            '*����ԍ� 000003 2005/10/25 �ǉ��I��
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.HIYOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.JIDOTEATESTYM, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csABJiteTable.Columns.Add(ABKobetsuJiteEntity.JIDOTEATEEDYM, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6

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

        Return csABJiteTable

    End Function
    '*����ԍ� 000002 2005/10/13 �ǉ��I��

End Class
