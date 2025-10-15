'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        ���v���J�c�a�f�[�^�Z�b�g�쐬(ABAtenaCnvBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2004/02/12�@�g�V�@�s��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/03/08  000001      ���[�l�̏��S���擾������ǉ�
'*                      �@ (�قڑS�̂̍\�����C�������̂ŏC���ӏ��͎����Ȃ�)
'* 2004/04/05  000002      �{�X�R�[�h�ǉ��ɔ����C��
'* 2004/11/05  000003      ���x������C�F�@ USSCITYINFO�N���X�C���X�^���X�ʒu��ύX����B
'*                                       �A �Ɩ����̃e�[�u���������o�ɕύX����B
'* 2005/02/06  000004      ���[�N�t���[�Ăяo�������̏C���i���v���J�f�[�^�쐬�������o�b�`�ֈڂ��j
'* 2005/10/13  000005      ��c�s�z�X�g�A�g�i���[�N�t���[�j������ǉ�(�}���S���R)
'* 2008/05/14  000006      ��c�s���ʃz�X�g�A�g�i���[�N�t���[�j������ǉ��i��Áj
'* 2010/04/16  000007      VS2008�Ή��i��Áj
'* 2022/12/16  000008    �yAB-8010�z�Z���R�[�h���уR�[�h15���Ή�(����)
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
Imports Densan.WorkFlow.UWCommon

Public Class ABAtenaCnvBClass

    '**
    '* �N���XID��`
    '* 
    Private Const THIS_CLASS_NAME As String = "ABAtenaCnvBClass"

    '**
    '* �����o�ϐ�
    '*  
    Private m_cfControlData As UFControlData                        ' �R���g���[���f�[�^
    Private m_cfConfigData As UFConfigDataClass                     ' �����f�[�^�N���X
    Private m_cfLog As UFLogClass                                   ' ���O�o�̓N���X
    Private m_cfRdbClass As UFRdbClass                              ' RDB�N���X
    Private m_cfSFSKRdbClass As UFRdbClass                          ' RDB�N���X
    Private m_cfDainoRdbClass As UFRdbClass                         ' RDB�N���X
    Private m_cfErrorClass As UFErrorClass                          ' �G���[�����N���X
    Private m_cReader As UFDataReaderClass                          ' �f�[�^���[�_�N���X

    Private m_aryABAtena As ArrayList                               '�������o���X�g�z��
    Private m_aryABSfsk As ArrayList                                '���t�撊�o���X�g�z��
    Private m_aryABDaino As ArrayList                               '��[���o���X�g�z��
    Private m_strSQL As String                                      '�����{�l�r�p�k��
    Private m_strSFSKSQL As String                                  '���t��r�p�k��
    Private m_strDAINOSQL As String                                 '��[�r�p�k��
    '*����ԍ� 000001 2004/03/08 �ǉ��J�n
    Private m_strHIDAINOSQL As String                               '���[�r�p�k��
    '(�b�菈���̂���"50"�̐����ɈӖ��͂Ȃ�)
    Private m_strHidainoJuminCD(50) As String
    Private m_intHiDaiCnt As Integer = 0                            '���[�l�J�E���^
    '*����ԍ� 000001 2004/03/08 �ǉ��I��
    '*����ԍ� 000003 2004/11/05 �ǉ��J�n
    Private m_strCityCD As String                                   '�s����CD
    Private m_csGyomuTable As DataTable                           '�Ɩ����e�[�u��
    '*����ԍ� 000003 2004/11/05 �ǉ��I��

    Private m_JuminCDA As String                                    '�����{�l�p�Z���R�[�h
    Private m_JuminCDS As String                                    '�������t��p�Z���R�[�h
    Private m_JuminCDD As String                                    '������[�p�Z���R�[�h
    Private m_intRecCnt As Integer                                  '�A�Ԃ̃J�E�^
    Private m_strNen As String                                        '�쐬����

    Public Const STR_A As String = "A"
    Public Const STR_B As String = "B"
    Public Const STR_C As String = "C"
    Public Const STR_D As String = "D"
    '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    Public Const STR_E As String = "E"
    Public Const STR_E_ As String = "E_"
    '*����ԍ� 000002 2004/04/05 �ǉ��I��
    Public Const STR_A_ As String = "A_"
    Public Const STR_B_ As String = "B_"
    Public Const STR_C_ As String = "C_"
    Public Const STR_D_ As String = "D_"
    Private Const SEPARATOR As String = ","                         '�Z�p���[�^
    Public Const ATENA As String = "����"                           '���[�N�t���[��(����)
    Public Const KOKUHO As String = "���ی�"                      '���[�N�t���[��(����)
    '*����ԍ� 000005 2005/10/17 �ǉ��J�n
    Public Const JITE As String = "�����"                        '���[�N�t���[��(����)
    '*����ԍ� 000005 2005/10/17 �ǉ��I��
    '*����ԍ� 000006 2008/05/14 �ǉ��J�n
    Public Const KAIGO As String = "����"                       '���[�N�t���[��(���)
    '*����ԍ� 000006 2008/05/14 �ǉ��I��

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass
    '* �@�@                          ByVal csUFRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* �@�@           cfRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        Dim cuCityInfo As USSCityInfoClass                      '�s�������N���X
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim strCityCD As String                                 '�s�����R�[�h
        '* corresponds to VS2008 End 2010/04/16 000007

        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigData = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        '*����ԍ� 000003 2004/11/05 �ǉ��J�n
        ''''�C���X�^���X��
        cuCityInfo = New USSCityInfoClass()
        '�s�������̎擾
        cuCityInfo.GetCityInfo(m_cfControlData)
        '�s�������ނ̎擾
        m_strCityCD = cuCityInfo.p_strShichosonCD(0)
        '*����ԍ� 000003 2004/11/05 �ǉ��I��

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLog = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

    End Sub
#End Region

    '*����ԍ� 000004 2005/03/22 �폜�J�n
#Region "�����ǉ�����"
    '    '************************************************************************************************
    '    '* ���\�b�h��     �����ǉ�����
    '    '* 
    '    '* �\��           Public Function AtenaCnv(ByVal cABToshoProperty() As ABToshoProperty,  
    '    '* �@�@                                      ByVal WORK_FLOW_NAME As String,
    '    '*                                           ByVal DATA_NAME As String) As DataSet
    '    '* 
    '    '* �@�\�@�@       �l�f�[�^�̒ǉ����s�Ȃ��B
    '    '* 
    '    '* ����           cABToshoProperty()
    '    '*                WORK_FLOW_NAME
    '    '*                DATA_NAME
    '    '* 
    '    '* �߂�l         �Ȃ�
    '    '************************************************************************************************
    '    Public Function AtenaCnv(ByVal cABToshoProperty() As ABToshoProperty, ByVal WORK_FLOW_NAME As String, ByVal DATA_NAME As String) As DataSet
    '        Const THIS_METHOD_NAME As String = "AtenaCnv"
    '        Dim csToshoEntity As New DataSet()                      '�����p�f�[�^�Z�b�g
    '        Dim csToshoRow As DataRow                               '�����f�[�^���E
    '        Dim csToshoTable As DataTable                           '�����f�[�^�e�[�u��
    '        Dim intCnt As Integer
    '        '*����ԍ� 000003 2004/11/05 �폜�J�n
    '        '''Dim cuCityInfo As USSCityInfoClass                      '�s�������N���X
    '        '''Dim strCityCD As String                                 '�s�����R�[�h
    '        '*����ԍ� 000003 2004/11/05 �폜�I��
    '        '*����ԍ� 000001 2004/03/08 �ǉ��J�n
    '        Dim csHiDainoEntity As DataSet                          '���[�f�[�^�Z�b�g
    '        Dim csHiDainoRow As DataRow                             '���[�f�[�^���E
    '        Dim intHiDainoCnt As Integer
    '        '*����ԍ� 000001 2004/03/08 �ǉ��I��

    '        Try
    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '            ' �쐬����(14��)
    '            m_strNen = DateTime.Now.ToString("yyyyMMddHHmmss")

    '            ' �e�[�u���Z�b�g�̎擾
    '            csToshoTable = Me.CreateColumnsData()
    '            csToshoTable.TableName = ABToshoTable.TABLE_NAME
    '            ' �f�[�^�Z�b�g�Ƀe�[�u���Z�b�g�̒ǉ�
    '            csToshoEntity.Tables.Add(csToshoTable)

    '            '�����폜�Ƃ���ȊO�̏ꍇ����
    '            If Not (cABToshoProperty(0).p_strKoshinKB = "D") Then

    '                '���[�l�J�E���^��"0"�ɂ���
    '                m_intHiDaiCnt = 0

    '                '�v���o�e�B���Ȃ��Ȃ�܂ŌJ��Ԃ�
    '                For intCnt = 0 To cABToshoProperty.Length - 1

    '                    '**
    '                    '*�{�l���̑S���擾����
    '                    '*
    '                    csToshoEntity = Me.AtenaHenshu(cABToshoProperty(intCnt).p_strJuminCD, cABToshoProperty(intCnt).p_strRonSakuFG, cABToshoProperty(intCnt).p_strKoshinKB, csToshoEntity)


    '                    '**
    '                    '*���[�l�Z���R�[�h�ҏW
    '                    '*
    '                    ' ���[SQL���s
    '                    csHiDainoEntity = m_cfRdbClass.GetDataSet(m_strHIDAINOSQL, ABDainoEntity.TABLE_NAME)
    '                    '��[�f�[�^�̎擾
    '                    For Each csHiDainoRow In csHiDainoEntity.Tables(ABDainoEntity.TABLE_NAME).Rows
    '                        m_strHidainoJuminCD(m_intHiDaiCnt) = CType(csHiDainoRow.Item(ABDainoEntity.JUMINCD), String)
    '                        '���[�l�̐����J�E���g
    '                        m_intHiDaiCnt += 1
    '                    Next

    '                Next

    '                '**
    '                '*���[�l�̑S���擾����
    '                '*
    '                For intHiDainoCnt = 0 To m_intHiDaiCnt - 1
    '                    '�S���擾����
    '                    csToshoEntity = Me.AtenaHenshu(m_strHidainoJuminCD(intHiDainoCnt), cABToshoProperty(0).p_strRonSakuFG, cABToshoProperty(0).p_strKoshinKB, csToshoEntity)
    '                Next

    '            Else

    '                '**
    '                '*�����폜�̕ҏW����
    '                '*
    '                '*����ԍ� 000003 2004/11/05 �폜�J�n
    '                ''''�C���X�^���X��
    '                '''cuCityInfo = New USSCityInfoClass()
    '                ''''�s�������̎擾
    '                '''cuCityInfo.GetCityInfo(m_cfControlData)
    '                ''''�s�������ނ̎擾
    '                '''strCityCD = cuCityInfo.p_strShichosonCD(0)
    '                '*����ԍ� 000003 2004/11/05 �폜�I��

    '                '�A�Ԃ̃J�E���g���Ƃ�
    '                m_intRecCnt += 1
    '                '�V����Row��ǉ�
    '                csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow

    '                ' �s��������(6��)
    '                '*����ԍ� 000003 2004/11/05 �C���J�n
    '                '''csToshoRow.Item(ABToshoTable.SHICHOSONCD) = strCityCD
    '                csToshoRow.Item(ABToshoTable.SHICHOSONCD) = m_strCityCD
    '                '*����ԍ� 000003 2004/11/05 �C���I��
    '                ' ����ID(4��)
    '                csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
    '                ' �쐬����(14��)
    '                csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
    '                ' �ŏI�s�敪(1��)
    '                csToshoRow.Item(ABToshoTable.LASTRECKB) = ""
    '                ' �A��(7��)
    '                csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)
    '                ' �Z���R�[�h(8��)(.NET12��)
    '                csToshoRow.Item(ABToshoTable.JUMIN_CD) = cABToshoProperty(intCnt).p_strJuminCD.Substring(4, 8)
    '                ' �X�V�敪(1��)
    '                csToshoRow.Item(ABToshoTable.UPDATE_KBN) = cABToshoProperty(intCnt).p_strKoshinKB

    '                '�ҏW����Row���f�[�^�Z�b�g�ɒǉ�
    '                csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)

    '            End If

    '            '**
    '            '*�ŏI�s�̕ҏW����
    '            '*
    '            '�A�Ԃ̃J�E���g���Ƃ�
    '            m_intRecCnt += 1
    '            '�ŏI�s�̎擾
    '            csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow
    '            csToshoRow = Me.ReflectLastData(csToshoRow)
    '            '�ҏW����Row���f�[�^�Z�b�g�ɒǉ�
    '            csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)

    '            '**
    '            '*���[�N�t���[���M�����Ăяo��
    '            '*
    '            Me.WorkFlowExec(csToshoEntity, WORK_FLOW_NAME, DATA_NAME)

    '            ' RDB�A�N�Z�X���O�o��
    '            m_cfLog.RdbWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
    '                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
    '                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
    '                                        "�ySQL���e:" + m_strSQL + m_strSFSKSQL + m_strDAINOSQL + "�z")

    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        Catch exAppException As UFAppException
    '            ' ���[�j���O���O�o��
    '            m_cfLog.WarningWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
    '                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
    '            ' ���[�j���O���X���[����
    '            Throw exAppException

    '        Catch exException As Exception ' �V�X�e���G���[���L���b�`
    '            ' �G���[���O�o��
    '            m_cfLog.ErrorWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y�G���[���e:" + exException.Message + "�z")

    '            ' �V�X�e���G���[���X���[����
    '            Throw exException

    '        End Try

    '        Return csToshoEntity

    '    End Function
#End Region

#Region "�����ǉ�����(MAIN)"
    '    '************************************************************************************************
    '    '* ���\�b�h��     �����ǉ�����(�J�Ԃ���)
    '    '* 
    '    '* �\��           Public Function AtenaHenshu(ByVal strJuminCD As String,   
    '    '* �@�@                                      ByVal strRonSakuFG As String,
    '    '*                                           ByVal strUpdataKB As String,
    '    '*                                           ByVal csToshoEntity As DataSet) As DataSet
    '    '* 
    '    '* �@�\�@�@       �S���̃f�[�^���擾����B
    '    '* 
    '    '* ����           strJuminCD        
    '    '*                strRonSakuFG
    '    '*                strUpdataKB
    '    '*              �@csToshoEntity
    '    '* 
    '    '* �߂�l         DataSet
    '    '************************************************************************************************
    '    Public Function AtenaHenshu(ByVal strJuminCD As String, ByVal strRonSakuFG As String, ByVal strUpdataKB As String, ByVal csToshoEntity As DataSet) As DataSet
    '        Const THIS_METHOD_NAME As String = "AtenaHenshu"
    '        Dim csToshoRow As DataRow                               '�����f�[�^���E
    '        Dim csAtenaEntity As DataSet                            '�{�l�������p�f�[�^�Z�b�g
    '        Dim csDainoEntity As DataSet                            '�{�l����+��[�l���p�f�[�^�Z�b�g
    '        Dim csSfskEntity As New DataSet()                       '�{�l����+��f����p�f�[�^�Z�b�g
    '        Dim csAtenaRow As DataRow                               '�{�l�������p�f�[�^���E
    '        Dim csSfskRow As DataRow                                '�{�l����+���t����p�f�[�^���E
    '        Dim csDainoRow As DataRow                               '�{�l����+��[�l���p�f�[�^���E
    '        Dim strKey(1) As String                                 '�L�[
    '        Dim intED As Integer = 1                                '�}�ԃJ�E���^
    '        '*����ԍ� 000003 2004/11/29 �폜�J�n
    '        '''''Dim csGyomuTable As DataTable
    '        '*����ԍ� 000003 2004/11/29 �폜�I��
    '        Dim csGyomuRow As DataRow                               '�Ɩ��f�[�^���E
    '        Dim csGERows As DataRow()                               '�Ɩ��E�}�ŗp�f�[�^���E

    '        Try
    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '            ' �쐬����(14��)
    '            m_strNen = DateTime.Now.ToString("yyyyMMddHHmmss")

    '            'SQL�쐬
    '            Me.CreateSQL(strJuminCD, strRonSakuFG)

    '            '**
    '            '*�{�l�������ҏW
    '            '*
    '            '�{�lSQL���s
    '            csAtenaEntity = m_cfRdbClass.GetDataSet(m_strSQL, ABAtenaEntity.TABLE_NAME)
    '            ' �{�l�����f�[�^�̎擾
    '            For Each csAtenaRow In csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows
    '                '�A�Ԃ̃J�E���g���Ƃ�
    '                m_intRecCnt += 1
    '                '�V����Row��ǉ�
    '                csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow()

    '                ' �����{�l�̃f�[�^����s�ǂݍ��݃Z�b�g����
    '                csToshoRow = Me.ReflectAtenaData(csAtenaRow, csToshoRow, strUpdataKB)
    '                '�ҏW����Row���f�[�^�Z�b�g�ɒǉ�
    '                csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)
    '            Next

    '            '�}�Ԃ̃J�E���^��������
    '            intED = 0
    '            '�Ɩ��R�[�h�̃L�[��������
    '            strKey(0) = String.Empty
    '            strKey(1) = String.Empty
    '            '�Ɩ��R�[�h�E�}�Ԃ̃e�[�u���쐬
    '            '*����ԍ� 000003 2004/11/29 �C���J�n
    '            '�Ɩ��b�c�E�}�Ńe�[�u���̍쐬
    '            If m_csGyomuTable Is Nothing Then
    '                m_csGyomuTable = Me.CreateClmGyomuData
    '            End If
    '            ''''''csGyomuTable = Me.CreateClmGyomuData
    '            '*����ԍ� 000003 2004/11/29 �C���I��

    '            '**
    '            '*�{�l�����E���t����ҏW
    '            '*
    '            ' ���t��SQL���s
    '            csSfskEntity = m_cfRdbClass.GetDataSet(m_strSFSKSQL, ABSfskEntity.TABLE_NAME)
    '            '���t��f�[�^�̎擾
    '            ' �f�[�^�ҏW & �o��
    '            For Each csSfskRow In csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows
    '                '�A�Ԃ̃J�E���g���Ƃ�
    '                m_intRecCnt += 1
    '                ''�V����Row��ǉ�
    '                csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow
    '                ' �����{�l�E���t�����s�ǂݍ��݃Z�b�g����
    '                csToshoRow = Me.ReflectSofusakiData(csSfskRow, csToshoRow, strUpdataKB)
    '                '�}�Ԃ̕ҏW
    '                If Not (CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String) = String.Empty) Then
    '                    '�u���C�N�L�[�̐ݒ�(��L�[)
    '                    strKey(0) = CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String)
    '                    '�O�L�[�ƌ�L�[��������������}�ԃJ�E���^��+1���Ď}�ԂɃf�[�^��ǉ�
    '                    If (strKey(0) = strKey(1)) Then
    '                        intED += 1
    '                        csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
    '                    Else
    '                        '�Ɩ��R�[�h�E�}�ԃe�[�u���ɐV�K���E���쐬
    '                        csGyomuRow = m_csGyomuTable.NewRow()
    '                        csGyomuRow.Item(ABToshoTable.GYOMU_CD) = strKey(1)
    '                        csGyomuRow.Item(ABToshoTable.EDABAN) = CStr(intED)
    '                        '�Ɩ��R�[�h�E�}�ԃe�[�u���Ƀ��E��ǉ�
    '                        m_csGyomuTable.Rows.Add(csGyomuRow)

    '                        intED = 1
    '                        '�}�ԂɈ�Ԗڂ̃f�[�^(001)
    '                        csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)

    '                    End If
    '                Else
    '                    intED = 1
    '                    '�}�ԂɈ�Ԗڂ̃f�[�^(001)
    '                    ' �}��(3��)
    '                    csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
    '                End If
    '                '�u���C�N�L�[�̐ݒ�(�O�L�[)
    '                strKey(1) = CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String)
    '                '�ҏW����Row���f�[�^�Z�b�g�ɒǉ�
    '                csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)
    '            Next

    '            '�Ɩ��R�[�h�E�}�ԃe�[�u���ɐV�K���E���쐬
    '            csGyomuRow = m_csGyomuTable.NewRow()
    '            csGyomuRow.Item(ABToshoTable.GYOMU_CD) = strKey(1)
    '            csGyomuRow.Item(ABToshoTable.EDABAN) = CStr(intED)
    '            '�Ɩ��R�[�h�E�}�ԃe�[�u���Ƀ��E��ǉ�
    '            m_csGyomuTable.Rows.Add(csGyomuRow)

    '            '�}�Ԃ̃J�E���^��������
    '            intED = 0
    '            '�Ɩ��R�[�h�̃L�[��������
    '            strKey(0) = String.Empty
    '            strKey(1) = String.Empty


    '            '**
    '            '*�{�l�����E��[�l�������ҏW
    '            '*
    '            ' ��[SQL���s
    '            csDainoEntity = m_cfRdbClass.GetDataSet(m_strDAINOSQL, ABDainoEntity.TABLE_NAME)

    '            '��[�f�[�^�̎擾
    '            For Each csDainoRow In csDainoEntity.Tables(ABDainoEntity.TABLE_NAME).Rows
    '                '�A�Ԃ̃J�E���g���Ƃ�
    '                m_intRecCnt += 1
    '                '�V����Row��ǉ�
    '                csToshoRow = csToshoEntity.Tables(ABToshoTable.TABLE_NAME).NewRow

    '                ' �����{�l�E��[�̃f�[�^����s�ǂݍ��݃Z�b�g����
    '                csToshoRow = Me.ReflectDainoData(csDainoRow, csToshoRow, strUpdataKB)

    '                '�}�Ԃ̕ҏW
    '                If Not (CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String) = String.Empty) Then
    '                    '�u���C�N�L�[�̐ݒ�(��L�[)
    '                    strKey(0) = CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String)
    '                    '�O�L�[�ƌ�L�[��������������}�ԃJ�E���^��+1���Ď}�ԂɃf�[�^��ǉ�
    '                    If (strKey(0) = strKey(1)) Then
    '                        intED += 1
    '                        csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
    '                    Else
    '                        If Not (m_csGyomuTable.Rows.Count = 0) Then

    '                            '�Ɩ��R�[�h���L�[�����������Ƃ��đ��݂��郍�E���擾
    '                            csGERows = m_csGyomuTable.Select(ABToshoTable.GYOMU_CD + " = " + "'" + strKey(0) + "'")

    '                            '�Ɩ��b�c�E�}�ԃe�[�u���Ƀf�[�^�����݂��邩�ǂ���
    '                            If Not (csGERows.Length = 0) Then
    '                                intED = CType(csGERows(0).Item(ABToshoTable.EDABAN), Integer) + 1
    '                                csToshoRow.Item(ABToshoTable.EDABAN) = CType(intED, String).PadLeft(3, "0"c)
    '                            Else
    '                                intED = 1
    '                                '�}�ԂɈ�Ԗڂ̃f�[�^(001)
    '                                csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
    '                            End If
    '                        Else
    '                            intED = 1
    '                            '�}�ԂɈ�Ԗڂ̃f�[�^(001)
    '                            csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
    '                        End If
    '                    End If
    '                Else
    '                    intED = 1
    '                    '�}�ԂɈ�Ԗڂ̃f�[�^(001)
    '                    ' �}��(3��)
    '                    csToshoRow.Item(ABToshoTable.EDABAN) = CStr(intED).PadLeft(3, "0"c)
    '                End If
    '                '�u���C�N�L�[�̐ݒ�(�O�L�[)
    '                strKey(1) = CType(csToshoRow.Item(ABToshoTable.GYOMU_CD), String)

    '                '�ҏW����Row���f�[�^�Z�b�g�ɒǉ�
    '                csToshoEntity.Tables(ABToshoTable.TABLE_NAME).Rows.Add(csToshoRow)
    '            Next


    '            ' RDB�A�N�Z�X���O�o��
    '            m_cfLog.RdbWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
    '                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
    '                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
    '                                        "�ySQL���e:" + m_strSQL + m_strSFSKSQL + m_strDAINOSQL + "�z")

    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        Catch exAppException As UFAppException
    '            ' ���[�j���O���O�o��
    '            m_cfLog.WarningWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
    '                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
    '            ' ���[�j���O���X���[����
    '            Throw exAppException

    '        Catch exException As Exception ' �V�X�e���G���[���L���b�`
    '            ' �G���[���O�o��
    '            m_cfLog.ErrorWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y�G���[���e:" + exException.Message + "�z")

    '            ' �V�X�e���G���[���X���[����
    '            Throw exException

    '        End Try

    '        Return csToshoEntity

    '    End Function
#End Region

#Region "�����f�[�^�ҏW"
    '    '**
    '    '*	���\�b�h��	ReflectAtenaData
    '    '*	�T�v			�����f�[�^�̔��f (�{�l�������)
    '    '*	����			csRow		�@�@: �f�[�^�擾
    '    '*			    	csToshoRow		: �f�[�^�i�[
    '    '*				    strUpDateKB		: �X�V�敪
    '    '*	�߂�l		�Ȃ�
    '    '*
    '    Private Function ReflectAtenaData(ByVal csRow As DataRow, ByVal csToshoRow As DataRow, ByVal strUpDateKB As String) As DataRow
    '        Const THIS_METHOD_NAME As String = "ReflectAtenaData"
    '        Dim strPrefixA As String = CType((STR_A_), String)
    '        '*����ԍ� 000002 2004/04/058 �ǉ��J�n
    '        Dim strPrefixE As String = CType((STR_E_), String)
    '        '*����ԍ� 000002 2004/04/058 �ǉ��I��

    '        Try
    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '            ' �s��������(6��)
    '            csToshoRow.Item(ABToshoTable.SHICHOSONCD) = csRow.Item(strPrefixA + ABAtenaEntity.SHICHOSONCD)
    '            ' ����ID(4��)
    '            csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
    '            ' �쐬����(14��)
    '            csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
    '            ' �ŏI�s�敪(1��)
    '            csToshoRow.Item(ABToshoTable.LASTRECKB) = ""
    '            ' �A��(7��)
    '            csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)
    '            ' �Z���R�[�h(8��)(.NET12��)
    '            csToshoRow.Item(ABToshoTable.JUMIN_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.JUMINCD), String).Substring(4, 8)
    '            ' �}��(3��)
    '            csToshoRow.Item(ABToshoTable.EDABAN) = "001"
    '            ' ���уR�[�h(8��)(.NET12��)
    '            If CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String) = String.Empty Then
    '                csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.SETAI_CD) = "        "
    '            Else
    '                csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Substring(4, 8)
    '            End If
    '            ' �f�[�^�敪(2��)
    '            csToshoRow.Item(ABToshoTable.DATA_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATAKB)
    '            Dim strDataKB As String = CType(csToshoRow.Item(ABToshoTable.DATA_KBN), String)
    '            ' �Z����{�䒠�ԍ�(14��)
    '            csToshoRow.Item(ABToshoTable.DAICHO_NO) = ""
    '            ' �f�[�^���(2��)
    '            csToshoRow.Item(ABToshoTable.DATA_SHU) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATASHU)
    '            Dim strDataSB As String = CType(csToshoRow.Item(ABToshoTable.DATA_SHU), String)
    '            ' �����p�J�i�i���j(24��)
    '            csToshoRow.Item(ABToshoTable.KANASEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANASEI)
    '            ' �����p�J�i�i���j(16��)
    '            csToshoRow.Item(ABToshoTable.KANAMEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANAMEI)
    '            ' �J�i���̂P(60��)
    '            csToshoRow.Item(ABToshoTable.KANAMEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO1)
    '            ' �������̂P(80��)
    '            csToshoRow.Item(ABToshoTable.MEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO1)
    '            ' �J�i���̂Q(60��)
    '            csToshoRow.Item(ABToshoTable.KANAMEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO2)
    '            ' �������̂Q(80��)
    '            csToshoRow.Item(ABToshoTable.MEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO2)
    '            '���N����(8��)
    '            csToshoRow.Item(ABToshoTable.UMARE_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREYMD)
    '            ' ���a��N����(7��)
    '            csToshoRow.Item(ABToshoTable.UMARE_WYMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREWMD)
    '            '���ʃR�[�h(1��)
    '            csToshoRow.Item(ABToshoTable.SEIBETSU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSUCD)
    '            ' ����(2��)
    '            csToshoRow.Item(ABToshoTable.SEIBETSU) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSU)
    '            ' �����R�[�h(8��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA_CD) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARACD)
    '            ' ����(30��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARA)
    '            ' ��Q�����R�[�h(8��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARACD)
    '            ' ��Q����(30��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARA)
    '            ' ���L��\�ҏZ���R�[�h(8��)
    '            csToshoRow.Item(ABToshoTable.K_DAIHYOJUMIN_CD) = ""
    '            ' �@�l��\�Җ��i�����j(60��)
    '            csToshoRow.Item(ABToshoTable.H_DAIHYOMEI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
    '            ' �Y�ƕ��ރR�[�h(4��)
    '            csToshoRow.Item(ABToshoTable.SANGYO_CD) = ""
    '            '*����ԍ� 000002 2004/04/058 �C���J�n
    '            If CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Trim = String.Empty Then
    '                ' �{�X�R�[�h(8��)
    '                csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
    '            Else
    '                ' �{�X�R�[�h(8��)
    '                csToshoRow.Item(ABToshoTable.HONTEN_CD) = CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Substring(4, 8)
    '            End If
    '            'csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
    '            '*����ԍ� 000002 2004/04/058 �C���I��
    '            ' �ėp�敪�P(1��)
    '            '(�f�[�^�敪��"11""12"�̎��A�J�i���̂Q�����鎞�̔���)
    '            If (strDataKB = "11" Or strDataKB = "12") Then
    '                If Not (csToshoRow.Item(ABToshoTable.KANAMEISHO2) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "T"
    '                Else
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "S"
    '                End If
    '            Else
    '                csToshoRow.Item(ABToshoTable.HANYO_KBN1) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB1)
    '            End If
    '            ' �@�l�`��(20��)
    '            csToshoRow.Item(ABToshoTable.HOJINKEITAI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNKEITAI)
    '            ' �l�@�l�敪(1��)
    '            csToshoRow.Item(ABToshoTable.KOJINHOJIN_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KJNHJNKB)
    '            ' ���l��(4��)
    '            csToshoRow.Item(ABToshoTable.HOKA_NINZU) = ""
    '            ' �ėp�敪�Q(1��)
    '            '(�f�[�^�敪��"18""28"�̎��A�]�o�m��Z���E�]�o�\��Z�������鎞�̔���)
    '            If strDataSB = "18" Or strDataSB = "28" Then
    '                If Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUKKTIJUSHO) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "K"
    '                ElseIf Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUYOTEIJUSHO) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "Y"
    '                Else
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
    '                End If
    '            Else
    '                csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
    '            End If
    '            ' �Ǔ��ǊO�敪(1��)
    '            csToshoRow.Item(ABToshoTable.NAIGAI_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KANNAIKANGAIKB)
    '            ' �X�֔ԍ�(7��)
    '            csToshoRow.Item(ABToshoTable.YUBIN_NO) = csRow.Item(strPrefixA + ABAtenaEntity.YUBINNO)
    '            ' �Z���R�[�h(11��)
    '            csToshoRow.Item(ABToshoTable.JUSHO_CD) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHOCD)
    '            ' �Z����(60��)
    '            csToshoRow.Item(ABToshoTable.JUSHO) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHO)
    '            ' �Ԓn�R�[�h�P(5��)
    '            csToshoRow.Item(ABToshoTable.BANCHI_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD1)
    '            ' �Ԓn�R�[�h�Q(5��)
    '            csToshoRow.Item(ABToshoTable.BANCHI_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD2)
    '            ' �Ԓn�R�[�h�R(5��)
    '            csToshoRow.Item(ABToshoTable.BANCHI_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD3)
    '            ' �Ԓn(40��)
    '            csToshoRow.Item(ABToshoTable.BANCHI) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHI)
    '            ' �����t���O(1��)
    '            csToshoRow.Item(ABToshoTable.KATAGAKI_FLG) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKIFG)
    '            ' �����R�[�h(4��)
    '            csToshoRow.Item(ABToshoTable.KATAGAKI_CD) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKICD)
    '            ' ����(60��)
    '            csToshoRow.Item(ABToshoTable.KATAGAKI) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKI)
    '            ' �A����P(14��)
    '            csToshoRow.Item(ABToshoTable.RENRAKUSAKI1) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI1)
    '            ' �A����Q(14��)
    '            csToshoRow.Item(ABToshoTable.RENRAKUSAKI2) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI2)
    '            ' �s����R�[�h(7��)(.NET9��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Length <= 7 Then
    '                csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = "       "
    '            Else
    '                csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Substring(2, 7)
    '            End If
    '            ' �s���於(60��)
    '            csToshoRow.Item(ABToshoTable.GYOSEIKU) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUMEI)
    '            ' �n��R�[�h�P(6��)(.NET8��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD1) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD1) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
    '            End If
    '            ' �n�於�P(60��)
    '            csToshoRow.Item(ABToshoTable.CHIKU1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI1)
    '            ' �n��R�[�h�Q(6��)(.NET8��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2) Is String.Empty Or _
    '               CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD2) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD2) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Substring(2, 6)
    '            End If
    '            ' �n�於�Q(60��)
    '            csToshoRow.Item(ABToshoTable.CHIKU2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI2)
    '            ' �n��R�[�h�R(6��)(.NET8��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3) Is String.Empty Or _
    '              CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD3) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD3) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Substring(2, 6)
    '            End If
    '            ' �n�於�R(60��)
    '            csToshoRow.Item(ABToshoTable.CHIKU3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI3)
    '            ' �o�^�ٓ��N����(8��)
    '            csToshoRow.Item(ABToshoTable.TRK_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUIDOYMD)
    '            ' �o�^���R�R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.TRK_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUJIYUCD)
    '            ' �폜�ٓ��N����(8��)
    '            csToshoRow.Item(ABToshoTable.SJO_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOIDOYMD)
    '            ' �폜���R�R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.SJO_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOJIYUCD)
    '            ' �ŏI����ԍ�(4��)
    '            csToshoRow.Item(ABToshoTable.LAST_RIREKI_NO) = ""
    '            ' �ٓ��N����(8��)
    '            csToshoRow.Item(ABToshoTable.IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINIDOYMD)
    '            ' �ٓ����R�R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINJIYUCD)
    '            ' �o�^�N����(8��)
    '            csToshoRow.Item(ABToshoTable.TRK_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINTDKDYMD)
    '            ' �X�V�敪(1��)
    '            csToshoRow.Item(ABToshoTable.UPDATE_KBN) = strUpDateKB
    '            ' ���[�UID(8��)(.NET32��)
    '            If CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Length >= 8 Then
    '                csToshoRow.Item(ABToshoTable.USER_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Substring(0, 8)
    '            Else
    '                csToshoRow.Item(ABToshoTable.USER_ID) = csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER)
    '            End If
    '            ' �[��ID(8��)(.NET32��)
    '            If CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Length >= 8 Then
    '                csToshoRow.Item(ABToshoTable.WS_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Substring(0, 8)
    '            Else
    '                csToshoRow.Item(ABToshoTable.WS_ID) = csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID)
    '            End If
    '            ' �^�C���X�^���v(14��)
    '            csToshoRow.Item(ABToshoTable.UP_DATE) = ""
    '            ' �_�����b�N�L�[(6��)
    '            csToshoRow.Item(ABToshoTable.LOCK_KEY) = ""

    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        Catch exAppException As UFAppException
    '            ' ���[�j���O���O�o��
    '            m_cfLog.WarningWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
    '                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
    '            ' ���[�j���O���X���[����
    '            Throw exAppException

    '        Catch exException As Exception ' �V�X�e���G���[���L���b�`
    '            ' �G���[���O�o��
    '            m_cfLog.ErrorWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y�G���[���e:" + exException.Message + "�z")

    '            ' �V�X�e���G���[���X���[����
    '            Throw exException

    '        End Try

    '        Return csToshoRow

    '    End Function
#End Region

#Region "���t��f�[�^�ҏW"
    '    '**
    '    '*	���\�b�h��	ReflectSofusakiData
    '    '*	�T�v			���t��f�[�^�̔��f
    '    '*	����			csRow		�@�@: �f�[�^�擾
    '    '*				    csToshoRow		: �f�[�^�i�[
    '    '*				    strUpDateKB		: �X�V�敪
    '    '*	�߂�l		�Ȃ�
    '    '*
    '    Private Function ReflectSofusakiData(ByVal csRow As DataRow, ByVal csToshoRow As DataRow, ByVal strUpDateKB As String) As DataRow
    '        Const THIS_METHOD_NAME As String = "ReflectSofusakiData"
    '        Dim strPrefixA As String = CType((STR_A_), String)
    '        Dim strPrefixB As String = CType((STR_B_), String)
    '        '*����ԍ� 000002 2004/04/058 �ǉ��J�n
    '        Dim strPrefixE As String = CType((STR_E_), String)
    '        '*����ԍ� 000002 2004/04/058 �ǉ��I��

    '        Try
    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '            ' �s��������(6��)
    '            csToshoRow.Item(ABToshoTable.SHICHOSONCD) = csRow.Item(strPrefixA + ABAtenaEntity.SHICHOSONCD)
    '            ' ����ID(4��)
    '            csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
    '            ' �쐬����(14��)
    '            csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
    '            ' �ŏI�s�敪(1��)
    '            csToshoRow.Item(ABToshoTable.LASTRECKB) = ""
    '            ' �A��(7��)
    '            csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)
    '            ' �Z���R�[�h(8��)(.NET12��)
    '            csToshoRow.Item(ABToshoTable.JUMIN_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.JUMINCD), String).Substring(4, 8)
    '            ' �}��(3��)
    '            'csToshoRow.Item(ABToshoTable.EDABAN) = ""
    '            ' ���уR�[�h(8��)(.NET12��)
    '            If CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String) = String.Empty Then
    '                csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.SETAI_CD) = "        "
    '            Else
    '                csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Substring(4, 8)
    '            End If
    '            ' �f�[�^�敪(2��)
    '            csToshoRow.Item(ABToshoTable.DATA_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATAKB)
    '            Dim strDataKB As String = CType(csToshoRow.Item(ABToshoTable.DATA_KBN), String)
    '            ' �Z����{�䒠�ԍ�(14��)
    '            csToshoRow.Item(ABToshoTable.DAICHO_NO) = ""
    '            ' �f�[�^���(2��)
    '            csToshoRow.Item(ABToshoTable.DATA_SHU) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATASHU)
    '            Dim strDataSB As String = CType(csToshoRow.Item(ABToshoTable.DATA_SHU), String)
    '            ' �����p�J�i�i���j(24��)
    '            csToshoRow.Item(ABToshoTable.KANASEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANASEI)
    '            ' �����p�J�i�i���j(16��)
    '            csToshoRow.Item(ABToshoTable.KANAMEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANAMEI)
    '            ' �J�i���̂P(60��)
    '            csToshoRow.Item(ABToshoTable.KANAMEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO1)
    '            ' �������̂P(80��)
    '            csToshoRow.Item(ABToshoTable.MEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO1)
    '            ' �J�i���̂Q(60��)
    '            csToshoRow.Item(ABToshoTable.KANAMEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO2)
    '            ' �������̂Q(80��)
    '            csToshoRow.Item(ABToshoTable.MEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO2)
    '            '���N����(8��)
    '            csToshoRow.Item(ABToshoTable.UMARE_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREYMD)
    '            ' ���a��N����(7��)
    '            csToshoRow.Item(ABToshoTable.UMARE_WYMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREWMD)
    '            '���ʃR�[�h(1��)
    '            csToshoRow.Item(ABToshoTable.SEIBETSU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSUCD)
    '            ' ����(2��)
    '            csToshoRow.Item(ABToshoTable.SEIBETSU) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSU)
    '            ' �����R�[�h(8��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA_CD) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARACD)
    '            ' ����(30��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARA)
    '            ' ��Q�����R�[�h(8��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARACD)
    '            ' ��Q����(30��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARA)
    '            ' ���L��\�ҏZ���R�[�h(8��)
    '            csToshoRow.Item(ABToshoTable.K_DAIHYOJUMIN_CD) = ""
    '            ' �@�l��\�Җ��i�����j(60��)
    '            csToshoRow.Item(ABToshoTable.H_DAIHYOMEI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
    '            ' �Y�ƕ��ރR�[�h(4��)
    '            csToshoRow.Item(ABToshoTable.SANGYO_CD) = ""
    '            '*����ԍ� 000002 2004/04/058 �C���J�n
    '            If CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Trim = String.Empty Then
    '                ' �{�X�R�[�h(8��)
    '                csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
    '            Else
    '                ' �{�X�R�[�h(8��)
    '                csToshoRow.Item(ABToshoTable.HONTEN_CD) = CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Substring(4, 8)
    '            End If
    '            'csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
    '            '*����ԍ� 000002 2004/04/058 �C���I��
    '            ' �ėp�敪�P(1��)
    '            '(�f�[�^�敪��"11""12"�̎��A�J�i���̂Q�����鎞�̔���)
    '            If (strDataKB = "11" Or strDataKB = "12") Then
    '                If Not (csToshoRow.Item(ABToshoTable.KANAMEISHO2) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "T"
    '                Else
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "S"
    '                End If
    '            Else
    '                csToshoRow.Item(ABToshoTable.HANYO_KBN1) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB1)
    '            End If
    '            ' �@�l�`��(20��)
    '            csToshoRow.Item(ABToshoTable.HOJINKEITAI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNKEITAI)
    '            ' �l�@�l�敪(1��)
    '            csToshoRow.Item(ABToshoTable.KOJINHOJIN_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KJNHJNKB)
    '            ' ���l��(4��)
    '            csToshoRow.Item(ABToshoTable.HOKA_NINZU) = ""
    '            ' �ėp�敪�Q(1��)
    '            '(�f�[�^�敪��"18""28"�̎��A�]�o�m��Z���E�]�o�\��Z�������鎞�̔���)
    '            If strDataSB = "18" Or strDataSB = "28" Then
    '                If Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUKKTIJUSHO) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "K"
    '                ElseIf Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUYOTEIJUSHO) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "Y"
    '                Else
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
    '                End If
    '            Else
    '                csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
    '            End If
    '            ' �Ǔ��ǊO�敪(1��)
    '            csToshoRow.Item(ABToshoTable.NAIGAI_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KANNAIKANGAIKB)
    '            ' �X�֔ԍ�(7��)
    '            csToshoRow.Item(ABToshoTable.YUBIN_NO) = csRow.Item(strPrefixA + ABAtenaEntity.YUBINNO)
    '            ' �Z���R�[�h(11��)
    '            csToshoRow.Item(ABToshoTable.JUSHO_CD) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHOCD)
    '            ' �Z����(60��)
    '            csToshoRow.Item(ABToshoTable.JUSHO) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHO)
    '            ' �Ԓn�R�[�h�P(5��)
    '            csToshoRow.Item(ABToshoTable.BANCHI_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD1)
    '            ' �Ԓn�R�[�h�Q(5��)
    '            csToshoRow.Item(ABToshoTable.BANCHI_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD2)
    '            ' �Ԓn�R�[�h�R(5��)
    '            csToshoRow.Item(ABToshoTable.BANCHI_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD3)
    '            ' �Ԓn(40��)
    '            csToshoRow.Item(ABToshoTable.BANCHI) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHI)
    '            ' �����t���O(1��)
    '            csToshoRow.Item(ABToshoTable.KATAGAKI_FLG) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKIFG)
    '            ' �����R�[�h(4��)
    '            csToshoRow.Item(ABToshoTable.KATAGAKI_CD) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKICD)
    '            ' ����(60��)
    '            csToshoRow.Item(ABToshoTable.KATAGAKI) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKI)
    '            ' �A����P(14��)
    '            csToshoRow.Item(ABToshoTable.RENRAKUSAKI1) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI1)
    '            ' �A����Q(14��)
    '            csToshoRow.Item(ABToshoTable.RENRAKUSAKI2) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI2)
    '            ' �s����R�[�h(7��)(.NET9��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Length <= 7 Then
    '                csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = "       "
    '            Else
    '                csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Substring(2, 7)
    '            End If
    '            ' �s���於(60��)
    '            csToshoRow.Item(ABToshoTable.GYOSEIKU) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUMEI)
    '            ' �n��R�[�h�P(6��)(.NET8��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD1) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD1) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
    '            End If
    '            ' �n�於�P(60��)
    '            csToshoRow.Item(ABToshoTable.CHIKU1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI1)
    '            ' �n��R�[�h�Q(6��)(.NET8��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2) Is String.Empty Or _
    '               CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD2) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD2) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Substring(2, 6)
    '            End If
    '            ' �n�於�Q(60��)
    '            csToshoRow.Item(ABToshoTable.CHIKU2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI2)
    '            ' �n��R�[�h�R(6��)(.NET8��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3) Is String.Empty Or _
    '              CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD3) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD3) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Substring(2, 6)
    '            End If
    '            ' �n�於�R(60��)
    '            csToshoRow.Item(ABToshoTable.CHIKU3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI3)
    '            ' �o�^�ٓ��N����(8��)
    '            csToshoRow.Item(ABToshoTable.TRK_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUIDOYMD)
    '            ' �o�^���R�R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.TRK_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUJIYUCD)
    '            ' �폜�ٓ��N����(8��)
    '            csToshoRow.Item(ABToshoTable.SJO_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOIDOYMD)
    '            ' �폜���R�R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.SJO_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOJIYUCD)
    '            ' �ŏI����ԍ�(4��)
    '            csToshoRow.Item(ABToshoTable.LAST_RIREKI_NO) = ""
    '            ' �ٓ��N����(8��)
    '            csToshoRow.Item(ABToshoTable.IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINIDOYMD)
    '            ' �ٓ����R�R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINJIYUCD)
    '            ' �o�^�N����(8��)
    '            csToshoRow.Item(ABToshoTable.TRK_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINTDKDYMD)
    '            ' �X�V�敪(1��)
    '            csToshoRow.Item(ABToshoTable.UPDATE_KBN) = strUpDateKB
    '            ' ���[�UID(8��)(.NET32��)
    '            If CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Length >= 8 Then
    '                csToshoRow.Item(ABToshoTable.USER_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Substring(0, 8)
    '            Else
    '                csToshoRow.Item(ABToshoTable.USER_ID) = csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER)
    '            End If
    '            ' �[��ID(8��)(.NET32��)
    '            If CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Length >= 8 Then
    '                csToshoRow.Item(ABToshoTable.WS_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Substring(0, 8)
    '            Else
    '                csToshoRow.Item(ABToshoTable.WS_ID) = csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID)
    '            End If
    '            ' �^�C���X�^���v(14��)
    '            csToshoRow.Item(ABToshoTable.UP_DATE) = ""
    '            ' �_�����b�N�L�[(6��)
    '            csToshoRow.Item(ABToshoTable.LOCK_KEY) = ""


    '            '�Z���R�[�h(8��)(.NET12��)
    '            csToshoRow.Item(ABToshoTable.D_JUMIN_CD) = CType(csRow.Item(strPrefixB + ABSfskEntity.JUMINCD), String).Substring(4, 8)
    '            ' �Ɩ��R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.GYOMU_CD) = csRow.Item(strPrefixB + ABSfskEntity.GYOMUCD)
    '            ' �J�n�N����(6��)
    '            csToshoRow.Item(ABToshoTable.ST_YM) = csRow.Item(strPrefixB + ABSfskEntity.STYM)
    '            ' �I���N����(6��)
    '            csToshoRow.Item(ABToshoTable.ED_YM) = csRow.Item(strPrefixB + ABSfskEntity.EDYM)
    '            ' ��[�敪(2��)
    '            csToshoRow.Item(ABToshoTable.D_DAINO_KBN) = "40"
    '            ' �J�i���̂P(60��)
    '            csToshoRow.Item(ABToshoTable.D_KANAMEISHO1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKKANAMEISHO)
    '            ' �������̂P(80��)
    '            csToshoRow.Item(ABToshoTable.D_MEISHO1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKKANJIMEISHO)
    '            '�Ǔ��ǊO�敪(1��)
    '            csToshoRow.Item(ABToshoTable.D_NAIGAI_KBN) = csRow.Item(strPrefixB + ABSfskEntity.SFSKKANNAIKANGAIKB)
    '            ' �X�֔ԍ�(7��)
    '            csToshoRow.Item(ABToshoTable.D_YUBIN_NO) = csRow.Item(strPrefixB + ABSfskEntity.SFSKYUBINNO)
    '            ' �Z���R�[�h(11��)
    '            csToshoRow.Item(ABToshoTable.D_JUSHO_CD) = csRow.Item(strPrefixB + ABSfskEntity.SFSKZJUSHOCD)
    '            '�Z��(60��)
    '            csToshoRow.Item(ABToshoTable.D_JUSHO) = csRow.Item(strPrefixB + ABSfskEntity.SFSKJUSHO)
    '            '�Ԓn(40��)
    '            csToshoRow.Item(ABToshoTable.D_BANCHI) = csRow.Item(strPrefixB + ABSfskEntity.SFSKBANCHI)
    '            ' ����(60��)
    '            csToshoRow.Item(ABToshoTable.D_KATAGAKI) = csRow.Item(strPrefixB + ABSfskEntity.SFSKKATAGAKI)
    '            ' �A����1(14��)
    '            csToshoRow.Item(ABToshoTable.D_RENRAKUSAKI1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKRENRAKUSAKI1)
    '            ' �A����2(14��)
    '            csToshoRow.Item(ABToshoTable.D_RENRAKUSAKI2) = csRow.Item(strPrefixB + ABSfskEntity.SFSKRENRAKUSAKI2)
    '            ' �s����R�[�h(7��)(.NET9��)
    '            If csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD), String).Length <= 7 Then
    '                csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD)
    '            ElseIf CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = "       "
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUCD), String).Substring(2, 7)
    '            End If
    '            ' �s���於(60��)
    '            csToshoRow.Item(ABToshoTable.D_GYOSEIKU) = csRow.Item(strPrefixB + ABSfskEntity.SFSKGYOSEIKUMEI)
    '            ' �n��R�[�h�P(6��)(.NET8��)
    '            If csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1)
    '            ElseIf CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD1), String).Substring(2, 6)
    '            End If
    '            ' �n��P(60��)
    '            csToshoRow.Item(ABToshoTable.D_CHIKU1) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUMEI1)
    '            ' �n��R�[�h�Q(6��)(.NET8��)
    '            If csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2)
    '            ElseIf CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD2), String).Substring(2, 6)
    '            End If
    '            ' �n��Q(60��)
    '            csToshoRow.Item(ABToshoTable.D_CHIKU2) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUMEI2)
    '            ' �n��R�[�h�R(6��)(.NET8��)
    '            If csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3)
    '            ElseIf CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = CType(csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUCD3), String).Substring(2, 6)
    '            End If
    '            ' �n��R(60��)
    '            csToshoRow.Item(ABToshoTable.D_CHIKU3) = csRow.Item(strPrefixB + ABSfskEntity.SFSKCHIKUMEI3)

    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        Catch exAppException As UFAppException
    '            ' ���[�j���O���O�o��
    '            m_cfLog.WarningWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
    '                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
    '            ' ���[�j���O���X���[����
    '            Throw exAppException

    '        Catch exException As Exception ' �V�X�e���G���[���L���b�`
    '            ' �G���[���O�o��
    '            m_cfLog.ErrorWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y�G���[���e:" + exException.Message + "�z")

    '            ' �V�X�e���G���[���X���[����
    '            Throw exException

    '        End Try

    '        Return csToshoRow

    '    End Function
#End Region

#Region "��[�f�[�^�ҏW"
    '    '**
    '    '*	���\�b�h��	ReflectDainoData
    '    '*	�T�v			�����f�[�^�̔��f (��[�������)
    '    '*	����			csRow		�@�@: �f�[�^�擾
    '    '*				    csToshoRow		: �f�[�^�i�[
    '    '*			    	strUpDateKB		: �X�V�敪
    '    '*	�߂�l		�Ȃ�
    '    '*
    '    Private Function ReflectDainoData(ByVal csRow As DataRow, ByVal csToshoRow As DataRow, ByVal strUpDateKB As String) As DataRow
    '        Const THIS_METHOD_NAME As String = "ReflectDainoData"
    '        Dim strPrefixA As String = CType((STR_A_), String)
    '        Dim strPrefixC As String = CType((STR_C_), String)
    '        Dim strPrefixD As String = CType((STR_D_), String)
    '        '*����ԍ� 000002 2004/04/058 �ǉ��J�n
    '        Dim strPrefixE As String = CType((STR_E_), String)
    '        '*����ԍ� 000002 2004/04/058 �ǉ��I��

    '        Try
    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '            ' �s��������(6��)
    '            csToshoRow.Item(ABToshoTable.SHICHOSONCD) = csRow.Item(strPrefixA + ABAtenaEntity.SHICHOSONCD)
    '            ' ����ID(4��)
    '            csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
    '            ' �쐬����(14��)
    '            csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
    '            ' �ŏI�s�敪(1��)
    '            csToshoRow.Item(ABToshoTable.LASTRECKB) = ""
    '            ' �A��(7��)
    '            csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)
    '            ' �Z���R�[�h(8��)(.NET12��)
    '            csToshoRow.Item(ABToshoTable.JUMIN_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.JUMINCD), String).Substring(4, 8)
    '            ' �}��(3��)
    '            'csToshoRow.Item(ABToshoTable.EDABAN) = ""
    '            ' ���уR�[�h(8��)(.NET12��)
    '            If CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String) = String.Empty Then
    '                csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.SETAI_CD) = "        "
    '            Else
    '                csToshoRow.Item(ABToshoTable.SETAI_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.STAICD), String).Substring(4, 8)
    '            End If
    '            ' �f�[�^�敪(2��)
    '            csToshoRow.Item(ABToshoTable.DATA_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATAKB)
    '            Dim strDataKB As String = CType(csToshoRow.Item(ABToshoTable.DATA_KBN), String)
    '            ' �Z����{�䒠�ԍ�(14��)
    '            csToshoRow.Item(ABToshoTable.DAICHO_NO) = ""
    '            ' �f�[�^���(2��)
    '            csToshoRow.Item(ABToshoTable.DATA_SHU) = csRow.Item(strPrefixA + ABAtenaEntity.ATENADATASHU)
    '            Dim strDataSB As String = CType(csToshoRow.Item(ABToshoTable.DATA_SHU), String)
    '            ' �����p�J�i�i���j(24��)
    '            csToshoRow.Item(ABToshoTable.KANASEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANASEI)
    '            ' �����p�J�i�i���j(16��)
    '            csToshoRow.Item(ABToshoTable.KANAMEI) = csRow.Item(strPrefixA + ABAtenaEntity.SEARCHKANAMEI)
    '            ' �J�i���̂P(60��)
    '            csToshoRow.Item(ABToshoTable.KANAMEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO1)
    '            ' �������̂P(80��)
    '            csToshoRow.Item(ABToshoTable.MEISHO1) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO1)
    '            ' �J�i���̂Q(60��)
    '            csToshoRow.Item(ABToshoTable.KANAMEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANAMEISHO2)
    '            ' �������̂Q(80��)
    '            csToshoRow.Item(ABToshoTable.MEISHO2) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIMEISHO2)
    '            '���N����(8��)
    '            csToshoRow.Item(ABToshoTable.UMARE_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREYMD)
    '            ' ���a��N����(7��)
    '            csToshoRow.Item(ABToshoTable.UMARE_WYMD) = csRow.Item(strPrefixA + ABAtenaEntity.UMAREWMD)
    '            '���ʃR�[�h(1��)
    '            csToshoRow.Item(ABToshoTable.SEIBETSU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSUCD)
    '            ' ����(2��)
    '            csToshoRow.Item(ABToshoTable.SEIBETSU) = csRow.Item(strPrefixA + ABAtenaEntity.SEIBETSU)
    '            ' �����R�[�h(8��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA_CD) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARACD)
    '            ' ����(30��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA) = csRow.Item(strPrefixA + ABAtenaEntity.ZOKUGARA)
    '            ' ��Q�����R�[�h(8��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARACD)
    '            ' ��Q����(30��)
    '            csToshoRow.Item(ABToshoTable.ZOKUGARA2) = csRow.Item(strPrefixA + ABAtenaEntity.DAI2ZOKUGARA)
    '            ' ���L��\�ҏZ���R�[�h(8��)
    '            csToshoRow.Item(ABToshoTable.K_DAIHYOJUMIN_CD) = ""
    '            ' �@�l��\�Җ��i�����j(60��)
    '            csToshoRow.Item(ABToshoTable.H_DAIHYOMEI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
    '            ' �Y�ƕ��ރR�[�h(4��)
    '            csToshoRow.Item(ABToshoTable.SANGYO_CD) = ""
    '            '*����ԍ� 000002 2004/04/058 �C���J�n
    '            If CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Trim = String.Empty Then
    '                ' �{�X�R�[�h(8��)
    '                csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
    '            Else
    '                ' �{�X�R�[�h(8��)
    '                csToshoRow.Item(ABToshoTable.HONTEN_CD) = CType(csRow.Item(strPrefixE + ABDainoEntity.DAINOJUMINCD), String).Substring(4, 8)
    '            End If
    '            'csToshoRow.Item(ABToshoTable.HONTEN_CD) = ""
    '            '*����ԍ� 000002 2004/04/058 �C���I��
    '            ' �ėp�敪�P(1��)
    '            '(�f�[�^�敪��"11""12"�̎��A�J�i���̂Q�����鎞�̔���)
    '            If (strDataKB = "11" Or strDataKB = "12") Then
    '                If Not (csToshoRow.Item(ABToshoTable.KANAMEISHO2) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "T"
    '                Else
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN1) = "S"
    '                End If
    '            Else
    '                csToshoRow.Item(ABToshoTable.HANYO_KBN1) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB1)
    '            End If
    '            ' �@�l�`��(20��)
    '            csToshoRow.Item(ABToshoTable.HOJINKEITAI) = csRow.Item(strPrefixA + ABAtenaEntity.KANJIHJNKEITAI)
    '            ' �l�@�l�敪(1��)
    '            csToshoRow.Item(ABToshoTable.KOJINHOJIN_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KJNHJNKB)
    '            ' ���l��(4��)
    '            csToshoRow.Item(ABToshoTable.HOKA_NINZU) = ""
    '            ' �ėp�敪�Q(1��)
    '            '(�f�[�^�敪��"18""28"�̎��A�]�o�m��Z���E�]�o�\��Z�������鎞�̔���)
    '            If strDataSB = "18" Or strDataSB = "28" Then
    '                If Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUKKTIJUSHO) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "K"
    '                ElseIf Not (csRow.Item(strPrefixA + ABAtenaEntity.TENSHUTSUYOTEIJUSHO) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN2) = "Y"
    '                Else
    '                    csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
    '                End If
    '            Else
    '                csToshoRow.Item(ABToshoTable.HANYO_KBN2) = csRow.Item(strPrefixA + ABAtenaEntity.HANYOKB2)
    '            End If
    '            ' �Ǔ��ǊO�敪(1��)
    '            csToshoRow.Item(ABToshoTable.NAIGAI_KBN) = csRow.Item(strPrefixA + ABAtenaEntity.KANNAIKANGAIKB)
    '            ' �X�֔ԍ�(7��)
    '            csToshoRow.Item(ABToshoTable.YUBIN_NO) = csRow.Item(strPrefixA + ABAtenaEntity.YUBINNO)
    '            ' �Z���R�[�h(11��)
    '            csToshoRow.Item(ABToshoTable.JUSHO_CD) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHOCD)
    '            ' �Z����(60��)
    '            csToshoRow.Item(ABToshoTable.JUSHO) = csRow.Item(strPrefixA + ABAtenaEntity.JUSHO)
    '            ' �Ԓn�R�[�h�P(5��)
    '            csToshoRow.Item(ABToshoTable.BANCHI_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD1)
    '            ' �Ԓn�R�[�h�Q(5��)
    '            csToshoRow.Item(ABToshoTable.BANCHI_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD2)
    '            ' �Ԓn�R�[�h�R(5��)
    '            csToshoRow.Item(ABToshoTable.BANCHI_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHICD3)
    '            ' �Ԓn(40��)
    '            csToshoRow.Item(ABToshoTable.BANCHI) = csRow.Item(strPrefixA + ABAtenaEntity.BANCHI)
    '            ' �����t���O(1��)
    '            csToshoRow.Item(ABToshoTable.KATAGAKI_FLG) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKIFG)
    '            ' �����R�[�h(4��)
    '            csToshoRow.Item(ABToshoTable.KATAGAKI_CD) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKICD)
    '            ' ����(60��)
    '            csToshoRow.Item(ABToshoTable.KATAGAKI) = csRow.Item(strPrefixA + ABAtenaEntity.KATAGAKI)
    '            ' �A����P(14��)
    '            csToshoRow.Item(ABToshoTable.RENRAKUSAKI1) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI1)
    '            ' �A����Q(14��)
    '            csToshoRow.Item(ABToshoTable.RENRAKUSAKI2) = csRow.Item(strPrefixA + ABAtenaEntity.RENRAKUSAKI2)
    '            ' �s����R�[�h(7��)(.NET9��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Length <= 7 Then
    '                csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = "       "
    '            Else
    '                csToshoRow.Item(ABToshoTable.GYOSEIKU_CD) = CType(csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUCD), String).Substring(2, 7)
    '            End If
    '            ' �s���於(60��)
    '            csToshoRow.Item(ABToshoTable.GYOSEIKU) = csRow.Item(strPrefixA + ABAtenaEntity.GYOSEIKUMEI)
    '            ' �n��R�[�h�P(6��)(.NET8��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD1) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD1) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
    '            End If
    '            ' �n�於�P(60��)
    '            csToshoRow.Item(ABToshoTable.CHIKU1) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI1)
    '            ' �n��R�[�h�Q(6��)(.NET8��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2) Is String.Empty Or _
    '               CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD2) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD2) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD2), String).Substring(2, 6)
    '            End If
    '            ' �n�於�Q(60��)
    '            csToshoRow.Item(ABToshoTable.CHIKU2) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI2)
    '            ' �n��R�[�h�R(6��)(.NET8��)
    '            If csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3) Is String.Empty Or _
    '              CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3)
    '            ElseIf CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD3) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.CHIKU_CD3) = CType(csRow.Item(strPrefixA + ABAtenaEntity.CHIKUCD3), String).Substring(2, 6)
    '            End If
    '            ' �n�於�R(60��)
    '            csToshoRow.Item(ABToshoTable.CHIKU3) = csRow.Item(strPrefixA + ABAtenaEntity.CHIKUMEI3)
    '            ' �o�^�ٓ��N����(8��)
    '            csToshoRow.Item(ABToshoTable.TRK_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUIDOYMD)
    '            ' �o�^���R�R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.TRK_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.TOROKUJIYUCD)
    '            ' �폜�ٓ��N����(8��)
    '            csToshoRow.Item(ABToshoTable.SJO_IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOIDOYMD)
    '            ' �폜���R�R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.SJO_JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.SHOJOJIYUCD)
    '            ' �ŏI����ԍ�(4��)
    '            csToshoRow.Item(ABToshoTable.LAST_RIREKI_NO) = ""
    '            ' �ٓ��N����(8��)
    '            csToshoRow.Item(ABToshoTable.IDO_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINIDOYMD)
    '            ' �ٓ����R�R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.JIYU_CD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINJIYUCD)
    '            ' �o�^�N����(8��)
    '            csToshoRow.Item(ABToshoTable.TRK_YMD) = csRow.Item(strPrefixA + ABAtenaEntity.CKINTDKDYMD)
    '            ' �X�V�敪(1��)
    '            csToshoRow.Item(ABToshoTable.UPDATE_KBN) = strUpDateKB
    '            ' ���[�UID(8��)(.NET32��)
    '            If CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Length >= 8 Then
    '                csToshoRow.Item(ABToshoTable.USER_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER), String).Substring(0, 8)
    '            Else
    '                csToshoRow.Item(ABToshoTable.USER_ID) = csRow.Item(strPrefixA + ABAtenaEntity.SAKUSEIUSER)
    '            End If
    '            ' �[��ID(8��)(.NET32��)
    '            If CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Length >= 8 Then
    '                csToshoRow.Item(ABToshoTable.WS_ID) = CType(csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID), String).Substring(0, 8)
    '            Else
    '                csToshoRow.Item(ABToshoTable.WS_ID) = csRow.Item(strPrefixA + ABAtenaEntity.TANMATSUID)
    '            End If
    '            ' �^�C���X�^���v(14��)
    '            csToshoRow.Item(ABToshoTable.UP_DATE) = ""
    '            ' �_�����b�N�L�[(6��)
    '            csToshoRow.Item(ABToshoTable.LOCK_KEY) = ""

    '            ' ��[�Z���R�[�h(8��)(.NET12��)
    '            csToshoRow.Item(ABToshoTable.D_JUMIN_CD) = CType(csRow.Item(strPrefixD + ABDainoEntity.DAINOJUMINCD), String).Substring(4, 8)
    '            ' �Ɩ��R�[�h(2��)
    '            csToshoRow.Item(ABToshoTable.GYOMU_CD) = csRow.Item(strPrefixD + ABDainoEntity.GYOMUCD)
    '            ' �J�n�N����(6��)
    '            csToshoRow.Item(ABToshoTable.ST_YM) = csRow.Item(strPrefixD + ABDainoEntity.STYM)
    '            ' �I���N����(6��)
    '            csToshoRow.Item(ABToshoTable.ED_YM) = csRow.Item(strPrefixD + ABDainoEntity.EDYM)
    '            ' ��[�敪(2��)
    '            csToshoRow.Item(ABToshoTable.D_DAINO_KBN) = csRow.Item(strPrefixD + ABDainoEntity.DAINOKB)
    '            ' ���уR�[�h(8��)(.NET12��)
    '            If CType(csRow.Item(strPrefixC + ABAtenaEntity.STAICD), String) = String.Empty Then
    '                csToshoRow.Item(ABToshoTable.D_SETAI_CD) = CType(csRow.Item(strPrefixC + ABAtenaEntity.STAICD), String)
    '            ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.STAICD), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.D_SETAI_CD) = "        "
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_SETAI_CD) = CType(csRow.Item(strPrefixC + ABAtenaEntity.STAICD), String).Substring(4, 8)
    '            End If
    '            '�f�[�^�敪(2��)
    '            csToshoRow.Item(ABToshoTable.D_DATA_KBN) = csRow.Item(strPrefixC + ABAtenaEntity.ATENADATAKB)
    '            Dim strDataDKB As String = CStr(csToshoRow.Item(ABToshoTable.D_DATA_KBN))
    '            '�Z����{�䒠�ԍ�(14��)
    '            csToshoRow.Item(ABToshoTable.D_DAICHO_NO) = ""
    '            '�l�@�l�敪(1��)
    '            csToshoRow.Item(ABToshoTable.D_KOJINHOJIN_KBN) = csRow.Item(strPrefixC + ABAtenaEntity.KJNHJNKB)
    '            ' �f�[�^���(2��)
    '            csToshoRow.Item(ABToshoTable.D_DATA_SHU) = csRow.Item(strPrefixC + ABAtenaEntity.ATENADATASHU)
    '            Dim strDataDSB As String = CStr(csToshoRow.Item(ABToshoTable.D_DATA_SHU))
    '            ' �J�i���̂P(60��)
    '            csToshoRow.Item(ABToshoTable.D_KANAMEISHO1) = csRow.Item(strPrefixC + ABAtenaEntity.KANAMEISHO1)
    '            ' �������̂P(80��)
    '            csToshoRow.Item(ABToshoTable.D_MEISHO1) = csRow.Item(strPrefixC + ABAtenaEntity.KANJIMEISHO1)
    '            ' �J�i���̂Q(60��)
    '            csToshoRow.Item(ABToshoTable.D_KANAMEISHO2) = csRow.Item(strPrefixC + ABAtenaEntity.KANAMEISHO2)
    '            ' �������̂Q(80��)
    '            csToshoRow.Item(ABToshoTable.D_MEISHO2) = csRow.Item(strPrefixC + ABAtenaEntity.KANJIMEISHO2)
    '            ' �ėp�敪�P(1��)
    '            '(�f�[�^�敪��"11""12"�̎��A�J�i���̂Q�����鎞�̔���)
    '            If (strDataKB = "11" Or strDataKB = "12") Then
    '                If Not (csToshoRow.Item(ABToshoTable.D_KANAMEISHO2) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.D_HANYO_KBN1) = "T"
    '                Else
    '                    csToshoRow.Item(ABToshoTable.D_HANYO_KBN1) = "S"
    '                End If
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_HANYO_KBN1) = csRow.Item(strPrefixC + ABAtenaEntity.HANYOKB1)
    '            End If
    '            ' �@�l�`��(20��)
    '            csToshoRow.Item(ABToshoTable.D_HOJINKEITAI) = csRow.Item(strPrefixC + ABAtenaEntity.KANJIHJNKEITAI)
    '            ' �ėp�敪�Q(1��)
    '            '(�f�[�^�敪��"18""28"�̎��A�]�o�m��Z���E�]�o�\��Z�������鎞�̔���)
    '            If strDataSB = "18" Or strDataSB = "28" Then
    '                If Not (csRow.Item(strPrefixC + ABAtenaEntity.TENSHUTSUKKTIJUSHO) Is String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.D_HANYO_KBN2) = "K"
    '                ElseIf Not (CType(csRow.Item(strPrefixC + ABAtenaEntity.TENSHUTSUYOTEIJUSHO), String) = String.Empty) Then
    '                    csToshoRow.Item(ABToshoTable.D_HANYO_KBN2) = "Y"
    '                Else
    '                    csToshoRow.Item(ABToshoTable.D_HANYO_KBN2) = csRow.Item(strPrefixC + ABAtenaEntity.HANYOKB2)
    '                End If
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_HANYO_KBN2) = csRow.Item(strPrefixC + ABAtenaEntity.HANYOKB2)
    '            End If
    '            '�Ǔ��ǊO�敪(1��)
    '            csToshoRow.Item(ABToshoTable.D_NAIGAI_KBN) = csRow.Item(strPrefixC + ABAtenaEntity.KANNAIKANGAIKB)
    '            ' �X�֔ԍ�(7��)
    '            csToshoRow.Item(ABToshoTable.D_YUBIN_NO) = csRow.Item(strPrefixC + ABAtenaEntity.YUBINNO)
    '            ' �Z���R�[�h(11��)
    '            csToshoRow.Item(ABToshoTable.D_JUSHO_CD) = csRow.Item(strPrefixC + ABAtenaEntity.JUSHOCD)
    '            '�Z��(60��)
    '            csToshoRow.Item(ABToshoTable.D_JUSHO) = csRow.Item(strPrefixC + ABAtenaEntity.JUSHO)
    '            '�Ԓn�R�[�h�P(5��)
    '            csToshoRow.Item(ABToshoTable.D_BANCHI_CD1) = csRow.Item(strPrefixC + ABAtenaEntity.BANCHICD1)
    '            '�Ԓn�R�[�h�Q(5��)
    '            csToshoRow.Item(ABToshoTable.D_BANCHI_CD2) = csRow.Item(strPrefixC + ABAtenaEntity.BANCHICD2)
    '            '�Ԓn�R�[�h�R(5��)
    '            csToshoRow.Item(ABToshoTable.D_BANCHI_CD3) = csRow.Item(strPrefixC + ABAtenaEntity.BANCHICD3)
    '            '�Ԓn(40��)
    '            csToshoRow.Item(ABToshoTable.D_BANCHI) = csRow.Item(strPrefixC + ABAtenaEntity.BANCHI)
    '            ' �����t���O(1��)
    '            csToshoRow.Item(ABToshoTable.D_KATAGAKI_FLG) = csRow.Item(strPrefixC + ABAtenaEntity.KATAGAKIFG)
    '            ' �����R�[�h(4��)
    '            csToshoRow.Item(ABToshoTable.D_KATAGAKI_CD) = csRow.Item(strPrefixC + ABAtenaEntity.KATAGAKICD)
    '            ' ����(60��)
    '            csToshoRow.Item(ABToshoTable.D_KATAGAKI) = csRow.Item(strPrefixC + ABAtenaEntity.KATAGAKI)
    '            ' �A����1(14��)
    '            csToshoRow.Item(ABToshoTable.D_RENRAKUSAKI1) = csRow.Item(strPrefixC + ABAtenaEntity.RENRAKUSAKI1)
    '            ' �A����2(14��)
    '            csToshoRow.Item(ABToshoTable.D_RENRAKUSAKI2) = csRow.Item(strPrefixC + ABAtenaEntity.RENRAKUSAKI2)
    '            ' �s����R�[�h(7��)(.NET9��)
    '            If csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD) Is String.Empty Or _
    '               CType(csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD), String).Length <= 7 Then
    '                csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD)
    '            ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = "       "
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_GYOSEIKU_CD) = CType(csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUCD), String).Substring(2, 7)
    '            End If
    '            ' �s���於(60��)
    '            csToshoRow.Item(ABToshoTable.D_GYOSEIKU) = csRow.Item(strPrefixC + ABAtenaEntity.GYOSEIKUMEI)
    '            ' �n��R�[�h�P(6��)(.NET8��)
    '            If csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1) Is String.Empty Or _
    '                CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1)
    '            ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD1) = CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
    '            End If
    '            ' �n�於�P(60��)
    '            csToshoRow.Item(ABToshoTable.D_CHIKU1) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUMEI1)
    '            ' �n��R�[�h�Q(6��)(.NET8��)
    '            If csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD2) Is String.Empty Or _
    '               CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD2), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD2)
    '            ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD2), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD2) = CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
    '            End If
    '            ' �n�於�Q(60��)
    '            csToshoRow.Item(ABToshoTable.D_CHIKU2) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUMEI2)
    '            ' �n��R�[�h�R(6��)(.NET8��)
    '            If csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD3) Is String.Empty Or _
    '              CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD3), String).Length <= 6 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD3)
    '            ElseIf CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD3), String).Trim.Length = 0 Then
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = "      "
    '            Else
    '                csToshoRow.Item(ABToshoTable.D_CHIKU_CD3) = CType(csRow.Item(strPrefixC + ABAtenaEntity.CHIKUCD1), String).Substring(2, 6)
    '            End If
    '            ' �n��R(60��)
    '            csToshoRow.Item(ABToshoTable.D_CHIKU3) = csRow.Item(strPrefixC + ABAtenaEntity.CHIKUMEI3)
    '            ' �ʈ�����(3��)
    '            csToshoRow.Item(ABToshoTable.D_BETSUATENA) = "000"


    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        Catch exAppException As UFAppException
    '            ' ���[�j���O���O�o��
    '            m_cfLog.WarningWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
    '                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
    '            ' ���[�j���O���X���[����
    '            Throw exAppException

    '        Catch exException As Exception ' �V�X�e���G���[���L���b�`
    '            ' �G���[���O�o��
    '            m_cfLog.ErrorWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y�G���[���e:" + exException.Message + "�z")

    '            ' �V�X�e���G���[���X���[����
    '            Throw exException

    '        End Try

    '        Return csToshoRow

    '    End Function
#End Region

#Region "�ŏI�f�[�^�ҏW"
    '    '**
    '    '*	���\�b�h��	ReflectLastData
    '    '*	�T�v			�ŏI�f�[�^�̔��f
    '    '*	����			csRow		: �擾�f�[�^
    '    '*				    csToshoRow	: �i�[�f�[�^
    '    '*	�߂�l		DataRow
    '    '*
    '    Public Function ReflectLastData(ByVal csToshoRow As DataRow) As DataRow
    '        Const THIS_METHOD_NAME As String = "ReflectLastData"
    '        '*����ԍ� 000003 2004/11/05 �폜�J�n
    '        ''''Dim cuCityInfo As USSCityInfoClass
    '        ''''Dim strCityCD As String
    '        '*����ԍ� 000003 2004/11/05 �폜�I��

    '        Try
    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '            '*����ԍ� 000003 2004/11/05 �C���J�n
    '            ''''�C���X�^���X��
    '            '''cuCityInfo = New USSCityInfoClass()
    '            ''''�s�������̎擾
    '            '''cuCityInfo.GetCityInfo(m_cfControlData)
    '            ''''�s�������ނ̎擾
    '            '''strCityCD = cuCityInfo.p_strShichosonCD(0)
    '            ' �s��������(6��)
    '            ''''csToshoRow.Item(ABToshoTable.SHICHOSONCD) = strCityCD
    '            csToshoRow.Item(ABToshoTable.SHICHOSONCD) = m_strCityCD
    '            '*����ԍ� 000003 2004/11/05 �C���I��
    '            ' ����ID(4��)
    '            csToshoRow.Item(ABToshoTable.SHIKIBETSUID) = "AB21"
    '            ' �쐬����(14��)
    '            csToshoRow.Item(ABToshoTable.SAKUSEIYMD) = m_strNen
    '            ' �ŏI�s�敪(1��)
    '            csToshoRow.Item(ABToshoTable.LASTRECKB) = "E"
    '            ' �A��(7��)
    '            csToshoRow.Item(ABToshoTable.RENBAN) = CType(m_intRecCnt, String).PadLeft(7, "0"c)

    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        Catch exAppException As UFAppException
    '            ' ���[�j���O���O�o��
    '            m_cfLog.WarningWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
    '                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
    '            ' ���[�j���O���X���[����
    '            Throw exAppException

    '        Catch exException As Exception ' �V�X�e���G���[���L���b�`
    '            ' �G���[���O�o��
    '            m_cfLog.ErrorWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y�G���[���e:" + exException.Message + "�z")

    '            ' �V�X�e���G���[���X���[����
    '            Throw exException

    '        End Try

    '        Return csToshoRow
    '    End Function
#End Region

#Region "SQL���̍쐬"
    '    '************************************************************************************************
    '    '* ���\�b�h��     SQL���̍쐬
    '    '* 
    '    '* �\��           Private Sub CreateSQL(ByVal strJuminCD As String)
    '    '* 
    '    '* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '    '* 
    '    '* ����           strJuminCD As String : �擾�f�[�^�̏Z���R�[�h
    '    '* 
    '    '* �߂�l         �Ȃ�
    '    '************************************************************************************************
    '    Private Sub CreateSQL(ByVal strJuminCD As String, ByVal strRonSakuFG As String)
    '        Const THIS_METHOD_NAME As String = "CreateSQL"
    '        Dim strSQL As New Text.StringBuilder()
    '        Dim strSFSKSQL As New Text.StringBuilder()
    '        Dim strDAINOSQL As New Text.StringBuilder()
    '        '*����ԍ� 000001 2004/03/08 �ǉ��J�n
    '        Dim strHIDAINOSQL As New Text.StringBuilder()
    '        '*����ԍ� 000001 2004/03/08 �ǉ��I��


    '        '**
    '        '*�{�l����
    '        '*
    '        strSQL.Append(" SELECT	")
    '        strSQL.Append(getColumnList(True))
    '        strSQL.Append(" FROM	ABATENA A")
    '        '�_���폜�̔���
    '        If strRonSakuFG = "1" Then
    '            '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '            strSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '0' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON A.JUMINCD = E.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��I��
    '            strSQL.Append(" WHERE	A.SAKUJOFG<>'0' AND A.JUTOGAIYUSENKB ='1' AND A.JUMINCD = '")
    '        Else
    '            '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '            strSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '1' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON A.JUMINCD = E.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��I��
    '            strSQL.Append(" WHERE	A.SAKUJOFG<>'1' AND A.JUTOGAIYUSENKB ='1' AND A.JUMINCD = '")
    '        End If
    '        strSQL.Append(strJuminCD)
    '        strSQL.Append("'")

    '        '**
    '        '*�{�l�����{�{�l���t��
    '        '*
    '        strSFSKSQL.Append(" SELECT	")
    '        strSFSKSQL.Append(getSFSKColumnList(True))
    '        strSFSKSQL.Append(" FROM	ABSFSK B")
    '        '�_���폜�̔���
    '        If strRonSakuFG = "1" Then
    '            strSFSKSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '0' AND JUTOGAIYUSENKB ='1') A ON B.JUMINCD = A.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '            strSFSKSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '0' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON B.JUMINCD = E.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��I��
    '            strSFSKSQL.Append(" WHERE	B.SAKUJOFG<>'0' AND B.JUMINCD = '")
    '        Else
    '            strSFSKSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '1' AND JUTOGAIYUSENKB ='1') A ON B.JUMINCD = A.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '            strSFSKSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '1' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON B.JUMINCD = E.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��I��
    '            strSFSKSQL.Append(" WHERE	B.SAKUJOFG<>'1' AND B.JUMINCD = '")
    '        End If
    '        strSFSKSQL.Append(strJuminCD)
    '        strSFSKSQL.Append("'")
    '        strSFSKSQL.Append(" ORDER BY B.GYOMUCD ")

    '        '**
    '        '*�{�l�����{��[�l�����{�{�l��[
    '        '*
    '        strDAINOSQL.Append(" SELECT	")
    '        strDAINOSQL.Append(getDAINOColumnList(True))
    '        strDAINOSQL.Append(" FROM	ABDAINO D")
    '        '�_���폜�̔���
    '        If strRonSakuFG = "1" Then
    '            strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '0' AND JUTOGAIYUSENKB ='1') C ON D.DAINOJUMINCD = C.JUMINCD")
    '            strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '0' AND JUTOGAIYUSENKB ='1') A ON D.JUMINCD = A.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '            strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '0' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON D.JUMINCD = E.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��I��
    '            strDAINOSQL.Append(" WHERE	D.SAKUJOFG<>'0' AND D.GYOMUCD<>'05' AND D.GYOMUNAISHU_CD<>'9' AND D.JUMINCD = '")
    '        Else
    '            strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '1' AND JUTOGAIYUSENKB ='1') C ON D.DAINOJUMINCD = C.JUMINCD")
    '            strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABATENA WHERE SAKUJOFG <> '1' AND JUTOGAIYUSENKB ='1') A ON D.JUMINCD = A.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '            strDAINOSQL.Append("			LEFT OUTER JOIN (SELECT * FROM ABDAINO WHERE SAKUJOFG <> '1' AND GYOMUCD='05' AND GYOMUNAISHU_CD='9') E ON D.JUMINCD = E.JUMINCD")
    '            '*����ԍ� 000002 2004/04/05 �ǉ��I��
    '            strDAINOSQL.Append(" WHERE	D.SAKUJOFG<>'1' AND D.GYOMUCD<>'05' AND D.GYOMUNAISHU_CD<>'9' AND D.JUMINCD = '")
    '        End If
    '        strDAINOSQL.Append(strJuminCD)
    '        strDAINOSQL.Append("'")
    '        strDAINOSQL.Append(" ORDER BY D.GYOMUCD ")

    '        '*����ԍ� 000001 2004/03/08 �ǉ��J�n
    '        '**
    '        '*���[�l�Z���R�[�h
    '        '*
    '        strHIDAINOSQL.Append(" SELECT  JUMINCD ")
    '        strHIDAINOSQL.Append(" FROM    ABDAINO ")
    '        strHIDAINOSQL.Append(" WHERE	SAKUJOFG<>'1' AND GYOMUCD<>'05' AND GYOMUNAISHU_CD<>'9' AND DAINOJUMINCD = '")
    '        strHIDAINOSQL.Append(strJuminCD)
    '        strHIDAINOSQL.Append("'")
    '        '*����ԍ� 000001 2004/03/08 �ǉ��I��

    '        m_strSQL = strSQL.ToString()
    '        m_strSFSKSQL = strSFSKSQL.ToString()
    '        m_strDAINOSQL = strDAINOSQL.ToString()
    '        '*����ԍ� 000001 2004/03/08 �ǉ��J�n
    '        m_strHIDAINOSQL = strHIDAINOSQL.ToString()
    '        '*����ԍ� 000001 2004/03/08 �ǉ��I��
    '    End Sub
#End Region

#Region "SQL�p�����[�^�ҏW"
    '    '**
    '    '* ���\�b�h��
    '    '*	GetColumnList_ABAtena
    '    '* 
    '    '* �T�v
    '    '*	ABAtena�ŏ����ɕK�v�ȗ�̃��X�g��Ԃ��B
    '    '* 
    '    '* ����
    '    '*	�Ȃ�
    '    '* 
    '    '* �߂�l
    '    '*	�񃊃X�g
    '    Private Function GetColumnList_ABAtena() As ArrayList

    '        If (m_aryABAtena Is Nothing) Then
    '            m_aryABAtena = New ArrayList(56)
    '            m_aryABAtena.Add(ABAtenaEntity.SHICHOSONCD)
    '            m_aryABAtena.Add(ABAtenaEntity.JUMINCD)
    '            m_aryABAtena.Add(ABAtenaEntity.STAICD)
    '            m_aryABAtena.Add(ABAtenaEntity.ATENADATAKB)
    '            m_aryABAtena.Add(ABAtenaEntity.ATENADATASHU)
    '            m_aryABAtena.Add(ABAtenaEntity.SEARCHKANASEI)
    '            m_aryABAtena.Add(ABAtenaEntity.SEARCHKANAMEI)
    '            m_aryABAtena.Add(ABAtenaEntity.KANAMEISHO1)
    '            m_aryABAtena.Add(ABAtenaEntity.KANJIMEISHO1)
    '            m_aryABAtena.Add(ABAtenaEntity.KANAMEISHO2)
    '            m_aryABAtena.Add(ABAtenaEntity.KANJIMEISHO2)
    '            m_aryABAtena.Add(ABAtenaEntity.UMAREYMD)
    '            m_aryABAtena.Add(ABAtenaEntity.UMAREWMD)
    '            m_aryABAtena.Add(ABAtenaEntity.SEIBETSUCD)
    '            m_aryABAtena.Add(ABAtenaEntity.SEIBETSU)
    '            m_aryABAtena.Add(ABAtenaEntity.ZOKUGARACD)
    '            m_aryABAtena.Add(ABAtenaEntity.ZOKUGARA)
    '            m_aryABAtena.Add(ABAtenaEntity.DAI2ZOKUGARACD)
    '            m_aryABAtena.Add(ABAtenaEntity.DAI2ZOKUGARA)
    '            m_aryABAtena.Add(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
    '            m_aryABAtena.Add(ABAtenaEntity.HANYOKB1)
    '            m_aryABAtena.Add(ABAtenaEntity.KANJIHJNKEITAI)
    '            m_aryABAtena.Add(ABAtenaEntity.KJNHJNKB)
    '            m_aryABAtena.Add(ABAtenaEntity.HANYOKB2)
    '            m_aryABAtena.Add(ABAtenaEntity.KANNAIKANGAIKB)
    '            m_aryABAtena.Add(ABAtenaEntity.YUBINNO)
    '            m_aryABAtena.Add(ABAtenaEntity.JUSHOCD)
    '            m_aryABAtena.Add(ABAtenaEntity.JUSHO)
    '            m_aryABAtena.Add(ABAtenaEntity.BANCHICD1)
    '            m_aryABAtena.Add(ABAtenaEntity.BANCHICD2)
    '            m_aryABAtena.Add(ABAtenaEntity.BANCHICD3)
    '            m_aryABAtena.Add(ABAtenaEntity.BANCHI)
    '            m_aryABAtena.Add(ABAtenaEntity.KATAGAKIFG)
    '            m_aryABAtena.Add(ABAtenaEntity.KATAGAKICD)
    '            m_aryABAtena.Add(ABAtenaEntity.KATAGAKI)
    '            m_aryABAtena.Add(ABAtenaEntity.RENRAKUSAKI1)
    '            m_aryABAtena.Add(ABAtenaEntity.RENRAKUSAKI2)
    '            m_aryABAtena.Add(ABAtenaEntity.GYOSEIKUCD)
    '            m_aryABAtena.Add(ABAtenaEntity.GYOSEIKUMEI)
    '            m_aryABAtena.Add(ABAtenaEntity.CHIKUCD1)
    '            m_aryABAtena.Add(ABAtenaEntity.CHIKUMEI1)
    '            m_aryABAtena.Add(ABAtenaEntity.CHIKUCD2)
    '            m_aryABAtena.Add(ABAtenaEntity.CHIKUMEI2)
    '            m_aryABAtena.Add(ABAtenaEntity.CHIKUCD3)
    '            m_aryABAtena.Add(ABAtenaEntity.CHIKUMEI3)
    '            m_aryABAtena.Add(ABAtenaEntity.TOROKUIDOYMD)
    '            m_aryABAtena.Add(ABAtenaEntity.TOROKUJIYUCD)
    '            m_aryABAtena.Add(ABAtenaEntity.SHOJOIDOYMD)
    '            m_aryABAtena.Add(ABAtenaEntity.SHOJOJIYUCD)
    '            m_aryABAtena.Add(ABAtenaEntity.CKINIDOYMD)
    '            m_aryABAtena.Add(ABAtenaEntity.CKINJIYUCD)
    '            m_aryABAtena.Add(ABAtenaEntity.CKINTDKDYMD)
    '            m_aryABAtena.Add(ABAtenaEntity.SAKUSEIUSER)
    '            m_aryABAtena.Add(ABAtenaEntity.TANMATSUID)
    '            m_aryABAtena.Add(ABAtenaEntity.TENSHUTSUKKTIJUSHO)
    '            m_aryABAtena.Add(ABAtenaEntity.TENSHUTSUYOTEIJUSHO)
    '            m_aryABAtena.TrimToSize()
    '        End If

    '        Return m_aryABAtena
    '    End Function

    '    '**
    '    '* ���\�b�h��
    '    '*	GetColumnList_ABSfsk
    '    '* 
    '    '* �T�v
    '    '*	ABSfsk�ŏ����ɕK�v�ȗ�̃��X�g��Ԃ��B
    '    '* 
    '    '* ����
    '    '*	�Ȃ�
    '    '* 
    '    '* �߂�l
    '    '*	�񃊃X�g
    '    Private Function GetColumnList_ABSfsk() As ArrayList

    '        If (m_aryABSfsk Is Nothing) Then
    '            m_aryABSfsk = New ArrayList(23)
    '            m_aryABSfsk.Add(ABSfskEntity.STYM)
    '            m_aryABSfsk.Add(ABSfskEntity.EDYM)
    '            m_aryABSfsk.Add(ABSfskEntity.JUMINCD)
    '            m_aryABSfsk.Add(ABSfskEntity.GYOMUCD)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKDATAKB)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKKANAMEISHO)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKKANJIMEISHO)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKKANNAIKANGAIKB)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKYUBINNO)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKZJUSHOCD)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKJUSHO)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKBANCHI)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKKATAGAKI)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKRENRAKUSAKI1)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKRENRAKUSAKI2)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKGYOSEIKUCD)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKGYOSEIKUMEI)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUCD1)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUMEI1)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUCD2)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUMEI2)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUCD3)
    '            m_aryABSfsk.Add(ABSfskEntity.SFSKCHIKUMEI3)
    '            m_aryABSfsk.TrimToSize()
    '        End If

    '        Return m_aryABSfsk
    '    End Function
    '    '**
    '    '* ���\�b�h��
    '    '*	GetColumnList_ABDaino
    '    '* 
    '    '* �T�v
    '    '*	ABDaino�ŏ����ɕK�v�ȗ�̃��X�g��Ԃ��B
    '    '* 
    '    '* ����
    '    '*	�Ȃ�
    '    '* 
    '    '* �߂�l
    '    '*	�񃊃X�g
    '    Private Function GetColumnList_ABDaino() As ArrayList
    '        If (m_aryABDaino Is Nothing) Then
    '            m_aryABDaino = New ArrayList(5)
    '            m_aryABDaino.Add(ABDainoEntity.STYM)
    '            m_aryABDaino.Add(ABDainoEntity.EDYM)
    '            m_aryABDaino.Add(ABDainoEntity.DAINOKB)
    '            m_aryABDaino.Add(ABDainoEntity.DAINOJUMINCD)
    '            m_aryABDaino.Add(ABDainoEntity.GYOMUCD)
    '            m_aryABDaino.TrimToSize()
    '        End If

    '        Return m_aryABDaino
    '    End Function

    '    '**
    '    '* ���\�b�h��
    '    '*	getColumnList
    '    '* 
    '    '* �T�v
    '    '*	SQL��Select�߂̕�����𐶐�����B
    '    '* 
    '    '* ����
    '    '*	blnNeedAll		: �Ɩ��R�[�h���w�肳��A�S�Ẵe�[�u������
    '    '*					  ���ꂼ��f�[�^���擾����K�v�����邩�H
    '    '* 
    '    '* �߂�l
    '    '*	Select�ߕ�����(�A���A"Select" ������)
    '    Private Function getColumnList(ByVal blnNeedAll As Boolean) As String
    '        Dim ary As ArrayList
    '        Dim iEnum As IEnumerator
    '        Dim strTmp As String
    '        Dim strClmn As String
    '        Dim strBldr1 As New Text.StringBuilder()
    '        '*����ԍ� 000002 2004/04/058 �ǉ��J�n
    '        Dim strBldr2 As New Text.StringBuilder()
    '        '*����ԍ� 000002 2004/04/058 �ǉ��J�n

    '        Const CLMDEF_1 As String = " {0}.{1} AS {0}_{1}"
    '        Const CLMDEF_2 As String = " '' AS {0}_{1}"

    '        Dim strFormat As String = CType(IIf(blnNeedAll, CLMDEF_1, CLMDEF_2), String)

    '        ' �{�l����
    '        ary = GetColumnList_ABAtena()
    '        iEnum = ary.GetEnumerator()
    '        While (iEnum.MoveNext())
    '            If (strBldr1.Length > 0) Then
    '                strBldr1.Append(SEPARATOR)
    '            End If
    '            ' �{�l����
    '            strTmp = String.Format(CLMDEF_1, STR_A, iEnum.Current)
    '            strBldr1.Append(strTmp)

    '        End While

    '        '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '        ' ��[
    '        ary = GetColumnList_ABDaino()
    '        iEnum = ary.GetEnumerator()
    '        While (iEnum.MoveNext())
    '            If (strBldr2.Length > 0) Then
    '                strBldr2.Append(SEPARATOR)
    '            End If
    '            strTmp = String.Format(strFormat, STR_E, iEnum.Current)
    '            strBldr2.Append(strTmp)
    '        End While
    '        '*����ԍ� 000002 2004/04/05 �ǉ��I��

    '        '*����ԍ� 000002 2004/04/05 �C���J�n
    '        Return strBldr1.ToString() + SEPARATOR + strBldr2.ToString()
    '        'Return strBldr1.ToString()
    '        '*����ԍ� 000002 2004/04/05 �C���J�n
    '    End Function

    '    '**
    '    '* ���\�b�h��
    '    '*	getSFSKColumnList
    '    '* 
    '    '* �T�v
    '    '*	SQL��Select�߂̕�����𐶐�����B
    '    '* 
    '    '* ����
    '    '*	blnNeedAll		: �Ɩ��R�[�h���w�肳��A�S�Ẵe�[�u������
    '    '*					  ���ꂼ��f�[�^���擾����K�v�����邩�H
    '    '* 
    '    '* �߂�l
    '    '*	Select�ߕ�����(�A���A"Select" ������)
    '    Private Function getSFSKColumnList(ByVal blnNeedAll As Boolean) As String
    '        Dim ary As ArrayList

    '        Dim iEnum As IEnumerator
    '        Dim strSFSKTmp As String
    '        Dim strSFSKClmn As String
    '        Dim strSFSKBldr1 As New Text.StringBuilder()
    '        Dim strSFSKBldr2 As New Text.StringBuilder()
    '        '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '        Dim strSFSKBldr3 As New Text.StringBuilder()
    '        '*����ԍ� 000002 2004/04/05 �ǉ��J�n

    '        Const CLMDEF_1 As String = " {0}.{1} AS {0}_{1}"
    '        Const CLMDEF_2 As String = " '' AS {0}_{1}"

    '        Dim strFormat As String = CType(IIf(blnNeedAll, CLMDEF_1, CLMDEF_2), String)

    '        ' �{�l����
    '        ary = GetColumnList_ABAtena()
    '        iEnum = ary.GetEnumerator()
    '        While (iEnum.MoveNext())
    '            If (strSFSKBldr1.Length > 0) Then
    '                strSFSKBldr1.Append(SEPARATOR)
    '            End If
    '            ' �{�l����
    '            strSFSKTmp = String.Format(CLMDEF_1, STR_A, iEnum.Current)
    '            strSFSKBldr1.Append(strSFSKTmp)

    '        End While
    '        '�{�l���t��()
    '        ary = GetColumnList_ABSfsk()
    '        iEnum = ary.GetEnumerator()
    '        While (iEnum.MoveNext())
    '            If (strSFSKBldr2.Length > 0) Then
    '                strSFSKBldr2.Append(SEPARATOR)
    '            End If
    '            ' �{�l���t��
    '            strSFSKTmp = String.Format(strFormat, STR_B, iEnum.Current)
    '            strSFSKBldr2.Append(strSFSKTmp)
    '        End While


    '        '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '        ' ��[
    '        ary = GetColumnList_ABDaino()
    '        iEnum = ary.GetEnumerator()
    '        While (iEnum.MoveNext())
    '            If (strSFSKBldr3.Length > 0) Then
    '                strSFSKBldr3.Append(SEPARATOR)
    '            End If
    '            strSFSKTmp = String.Format(strFormat, STR_E, iEnum.Current)
    '            strSFSKBldr3.Append(strSFSKTmp)
    '        End While
    '        '*����ԍ� 000002 2004/04/05 �ǉ��I��

    '        '*����ԍ� 000002 2004/04/05 �C���J�n
    '        Return strSFSKBldr1.ToString() + SEPARATOR + strSFSKBldr2.ToString() + SEPARATOR + strSFSKBldr3.ToString()
    '        'Return strSFSKBldr1.ToString() + SEPARATOR + strSFSKBldr2.ToString()
    '        '*����ԍ� 000002 2004/04/05 �C���I��
    '    End Function

    '    '**
    '    '* ���\�b�h��
    '    '*	getDAINOColumnList
    '    '* 
    '    '* �T�v
    '    '*	SQL��Select�߂̕�����𐶐�����B
    '    '* 
    '    '* ����
    '    '*	blnNeedAll		: �Ɩ��R�[�h���w�肳��A�S�Ẵe�[�u������
    '    '*					  ���ꂼ��f�[�^���擾����K�v�����邩�H
    '    '* 
    '    '* �߂�l
    '    '*	Select�ߕ�����(�A���A"Select" ������)
    '    Private Function getDAINOColumnList(ByVal blnNeedAll As Boolean) As String
    '        Dim ary As ArrayList

    '        Dim iEnum As IEnumerator
    '        Dim strDAINOTmp As String
    '        Dim strDAINOClmn As String
    '        Dim strDAINOBldr1 As New Text.StringBuilder()
    '        Dim strDAINOBldr2 As New Text.StringBuilder()
    '        Dim strDAINOBldr3 As New Text.StringBuilder()
    '        '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '        Dim strDAINOBldr4 As New Text.StringBuilder()
    '        '*����ԍ� 000002 2004/04/05 �ǉ��I��

    '        Const CLMDEF_1 As String = " {0}.{1} AS {0}_{1}"
    '        Const CLMDEF_2 As String = " '' AS {0}_{1}"

    '        Dim strFormat As String = CType(IIf(blnNeedAll, CLMDEF_1, CLMDEF_2), String)

    '        ' �{�l����
    '        ary = GetColumnList_ABAtena()
    '        iEnum = ary.GetEnumerator()
    '        While (iEnum.MoveNext())
    '            If (strDAINOBldr1.Length > 0) Then
    '                strDAINOBldr1.Append(SEPARATOR)
    '                strDAINOBldr2.Append(SEPARATOR)
    '            End If
    '            ' �{�l����
    '            strDAINOTmp = String.Format(CLMDEF_1, STR_A, iEnum.Current)
    '            strDAINOBldr1.Append(strDAINOTmp)

    '            ' ��[�l����
    '            strDAINOTmp = String.Format(strFormat, STR_C, iEnum.Current)
    '            strDAINOBldr2.Append(strDAINOTmp)
    '        End While

    '        ' ��[
    '        ary = GetColumnList_ABDaino()
    '        iEnum = ary.GetEnumerator()
    '        While (iEnum.MoveNext())
    '            If (strDAINOBldr3.Length > 0) Then
    '                strDAINOBldr3.Append(SEPARATOR)
    '            End If
    '            strDAINOTmp = String.Format(strFormat, STR_D, iEnum.Current)
    '            strDAINOBldr3.Append(strDAINOTmp)
    '        End While

    '        '*����ԍ� 000002 2004/04/05 �ǉ��J�n
    '        ' ��[
    '        ary = GetColumnList_ABDaino()
    '        iEnum = ary.GetEnumerator()
    '        While (iEnum.MoveNext())
    '            If (strDAINOBldr4.Length > 0) Then
    '                strDAINOBldr4.Append(SEPARATOR)
    '            End If
    '            strDAINOTmp = String.Format(strFormat, STR_E, iEnum.Current)
    '            strDAINOBldr4.Append(strDAINOTmp)
    '        End While
    '        '*����ԍ� 000002 2004/04/05 �ǉ��I��

    '        '*����ԍ� 000002 2004/04/05 �C���J�n
    '        Return strDAINOBldr1.ToString() + SEPARATOR + strDAINOBldr2.ToString() + SEPARATOR + strDAINOBldr3.ToString() + SEPARATOR + strDAINOBldr4.ToString()
    '        'Return strDAINOBldr1.ToString() + SEPARATOR + strDAINOBldr2.ToString() + SEPARATOR + strDAINOBldr3.ToString()
    '        '*����ԍ� 000002 2004/04/05 �C���I��
    '    End Function
#End Region

#Region "�f�[�^�J�����쐬"
    '    '************************************************************************************************
    '    '* ���\�b�h��      �f�[�^�J�����쐬
    '    '* 
    '    '* �\��            Private Function CreateColumnsData() As DataTable
    '    '* 
    '    '* �@�\�@�@        ���v���J�c�a�̃J������`���쐬����
    '    '* 
    '    '* ����           �Ȃ�
    '    '* 
    '    '* �߂�l         DataTable() ��[���e�[�u��
    '    '************************************************************************************************
    '    Private Function CreateColumnsData() As DataTable
    '        Const THIS_METHOD_NAME As String = "CreateColumnsData"
    '        Dim csToshoTable As DataTable
    '        Dim csDataColumn As DataColumn

    '        Try
    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '            ' ��[���J������`
    '            csToshoTable = New DataTable()
    '            csToshoTable.TableName = ABToshoTable.TABLE_NAME
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SHICHOSONCD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SHIKIBETSUID, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 4
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SAKUSEIYMD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 14
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.LASTRECKB, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.RENBAN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 7
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.JUMIN_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.GYOMU_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.EDABAN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 3
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ST_YM, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ED_YM, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SETAI_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.DATA_KBN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.DAICHO_NO, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 14
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.DATA_SHU, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KANASEI, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 24
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KANAMEI, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 16
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KANAMEISHO1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.MEISHO1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 80
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KANAMEISHO2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.MEISHO2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 80
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.UMARE_YMD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.UMARE_WYMD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 7
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SEIBETSU_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SEIBETSU, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ZOKUGARA_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ZOKUGARA, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 30
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ZOKUGARA_CD2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.ZOKUGARA2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 30
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.K_DAIHYOJUMIN_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.H_DAIHYOMEI, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SANGYO_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 4
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HONTEN_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HANYO_KBN1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HOJINKEITAI, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 20
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KOJINHOJIN_KBN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HOKA_NINZU, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 4
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.HANYO_KBN2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.NAIGAI_KBN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.YUBIN_NO, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 7
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.JUSHO_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 11
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.JUSHO, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.BANCHI_CD1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 5
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.BANCHI_CD2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 5
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.BANCHI_CD3, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 5
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.BANCHI, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 40
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KATAGAKI_FLG, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KATAGAKI_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 4
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.KATAGAKI, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.RENRAKUSAKI1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 14
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.RENRAKUSAKI2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 14
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.GYOSEIKU_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 7
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.GYOSEIKU, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU_CD1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU_CD2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU_CD3, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.CHIKU3, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.TRK_IDO_YMD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.TRK_JIYU_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SJO_IDO_YMD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.SJO_JIYU_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.LAST_RIREKI_NO, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 4
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_DAINO_KBN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_JUMIN_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_SETAI_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_DATA_KBN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_DAICHO_NO, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 14
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KOJINHOJIN_KBN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_DATA_SHU, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KANAMEISHO1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_MEISHO1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 80
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KANAMEISHO2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_MEISHO2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 80
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_HANYO_KBN1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_HOJINKEITAI, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 20
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_HANYO_KBN2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_NAIGAI_KBN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_YUBIN_NO, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 7
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_JUSHO_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 11
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_JUSHO, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BANCHI_CD1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 5
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BANCHI_CD2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 5
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BANCHI_CD3, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 5
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BANCHI, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 40
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KATAGAKI_FLG, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KATAGAKI_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 4
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_KATAGAKI, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_RENRAKUSAKI1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 14
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_RENRAKUSAKI2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 14
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_GYOSEIKU_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 7
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_GYOSEIKU, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU_CD1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU1, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU_CD2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU2, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU_CD3, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_CHIKU3, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 60
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.D_BETSUATENA, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 3
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.IDO_YMD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.JIYU_CD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 2
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.TRK_YMD, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.UPDATE_KBN, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 1
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.RSV, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 23
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.USER_ID, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.WS_ID, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 8
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.UP_DATE, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 14
    '            csDataColumn = csToshoTable.Columns.Add(ABToshoTable.LOCK_KEY, System.Type.GetType("System.String"))
    '            csDataColumn.MaxLength = 6


    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


    '        Catch objAppExp As UFAppException
    '            ' ���[�j���O���O�o��
    '            m_cfLog.WarningWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
    '                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
    '            ' �G���[�����̂܂܃X���[����
    '            Throw objAppExp

    '        Catch objExp As Exception
    '            ' �G���[���O�o��
    '            m_cfLog.ErrorWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y�G���[���e:" + objExp.Message + "�z")
    '            ' �G���[�����̂܂܃X���[����
    '            Throw objExp
    '        End Try

    '        Return csToshoTable

    '    End Function
#End Region

#Region "�f�[�^�J�����쐬(�Ɩ��E�}��)"
    '    '************************************************************************************************
    '    '* ���\�b�h��      �f�[�^�J�����쐬
    '    '* 
    '    '* �\��            Private Function CreateClmGyomuData() As DataTable
    '    '* 
    '    '* �@�\�@�@        ���v���J�c�a�̃J������`���쐬����
    '    '* 
    '    '* ����           �Ȃ�
    '    '* 
    '    '* �߂�l         DataTable() ��[���e�[�u��
    '    '************************************************************************************************
    '    Private Function CreateClmGyomuData() As DataTable
    '        Const THIS_METHOD_NAME As String = "CreateClmGyomuData"
    '        Dim csGyomuTable As DataTable
    '        Dim csGyomuColumn As DataColumn

    '        Try
    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '            ' ��[���J������`
    '            csGyomuTable = New DataTable()
    '            csGyomuTable.TableName = ABToshoTable.TABLE_NAME
    '            csGyomuColumn = csGyomuTable.Columns.Add(ABToshoTable.GYOMU_CD, System.Type.GetType("System.String"))
    '            csGyomuColumn.MaxLength = 2
    '            csGyomuColumn = csGyomuTable.Columns.Add(ABToshoTable.EDABAN, System.Type.GetType("System.String"))
    '            csGyomuColumn.MaxLength = 3

    '            ' �f�o�b�O���O�o��
    '            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        Catch objAppExp As UFAppException
    '            ' ���[�j���O���O�o��
    '            m_cfLog.WarningWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
    '                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
    '            ' �G���[�����̂܂܃X���[����
    '            Throw objAppExp

    '        Catch objExp As Exception
    '            ' �G���[���O�o��
    '            m_cfLog.ErrorWrite(m_cfControlData, _
    '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                        "�y�G���[���e:" + objExp.Message + "�z")
    '            ' �G���[�����̂܂܃X���[����
    '            Throw objExp
    '        End Try

    '        Return csGyomuTable

    '    End Function
#End Region
    '*����ԍ� 000004 2005/03/22 �폜�I��

#Region "���[�N�t���[���M"
    '************************************************************************************************
    '* ���\�b�h��      ���[�N�t���[���M
    '* 
    '* �\��            Private Sub WorkFlowExec()
    '* 
    '* �@�\�@�@        ���v���J�c�a�̃J������`���쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub WorkFlowExec(ByVal csToshoEntity As DataSet, ByVal WORK_FLOW_NAME As String, ByVal DATA_NAME As String)
        Const THIS_METHOD_NAME As String = "WorkFlowExec"
        Dim cwMessage As UWMessageClass
        Dim cwStartRetInfo As UWStartRetInfo
        Dim cwStartDataInfoForDataSet(0) As UWStartDataInfoForDataSet
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        Dim cwSerialGroupId(0) As UWSerialGroupId            '�V���A���O���[�v
        Dim cwStartDataInfo(0) As UWStartDataInfo
        Dim strHanteiFile(0) As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���[�N�t���[�o�͐ݒ�
            cwStartDataInfoForDataSet(0) = New UWStartDataInfoForDataSet()
            cwStartDataInfoForDataSet(0).p_blnColumnOn = False                                          '�J�������t���O
            cwStartDataInfoForDataSet(0).p_strSep = ","                                                 '��؂蕶��
            cwStartDataInfoForDataSet(0).p_strDataName = DATA_NAME                                      '�f�[�^��
            cwStartDataInfoForDataSet(0).p_strDataKbn = UWStartDataInfo.DATAKBN_DATA                    '�f�[�^�敪
            cwStartDataInfoForDataSet(0).p_strCompressionType = UWStartDataInfo.COMPRESSIONTYPE_NONE    '���k�`��

            '���[�N�t���[�N���p�N���X�̃v���p�e�B�ݒ�
            cwMessage = New UWMessageClass(WORK_FLOW_NAME, m_cfControlData.m_strBusinessId)
            cwMessage.p_strWorkFlowName = WORK_FLOW_NAME
            cwMessage.p_strBusinessCd = ABConstClass.THIS_BUSINESSID
            cwMessage.p_strApplicationId = m_cfControlData.m_strMenuId
            cwMessage.p_strUserId = m_cfControlData.m_strUserId
            cwMessage.p_strClientId = m_cfControlData.m_strClientId
            '�f�[�^�l�[���ɂ���ăe�[�u�����̏ꍇ����������
            Select Case DATA_NAME
                Case ATENA
                    '*����ԍ� 000004 2005/02/28 �C���J�n
                    cwStartDataInfoForDataSet(0).p_csData = csToshoEntity.Tables(ABToshoPrmEntity.TABLE_NAME)
                    cwSerialGroupId(0) = New UWSerialGroupId()
                    cwSerialGroupId(0).p_strValue = CType(csToshoEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows(0).Item(ABToshoPrmEntity.STAICD), String)
                    cwMessage.p_arySerialGroupId = cwSerialGroupId
                    '���[�N�t���[�o�͐ݒ�_�Q
                    cwStartDataInfo(0) = New UWStartDataInfo()
                    cwStartDataInfo(0).p_strDataName = DATA_NAME + "��������"                         '�f�[�^��
                    cwStartDataInfo(0).p_strDataKbn = UWStartDataInfo.DATAKBN_PARAM                    '�f�[�^�敪
                    cwStartDataInfo(0).p_strCompressionType = UWStartDataInfo.COMPRESSIONTYPE_NONE    '���k�`��
                    cwStartDataInfo(0).p_strEncryptionType = UWStartDataInfo.ENCRYPTIONTYPE_NONE
                    cwStartDataInfo(0).p_strDataType = UWStartDataInfo.DATATYPE_TXT
                    cwStartDataInfo(0).p_strCharCode = UWStartDataInfo.CHARCODE_SJIS + UWStartDataInfo.CHAR_RENKETSU + UWStartDataInfo.GAIJI_DENSANUSER
                    strHanteiFile(0) = "SET PRM_FG=0"
                    cwStartDataInfo(0).p_strData = strHanteiFile
                    cwMessage.p_aryDataInfo = cwStartDataInfo
                    '-----------------------------------
                    ''''''cwStartDataInfoForDataSet(1) = New UWStartDataInfoForDataSet()
                    ''''''cwStartDataInfoForDataSet(1).p_blnColumnOn = False                                          '�J�������t���O
                    ''''''cwStartDataInfoForDataSet(1).p_strSep = ","                                                 '��؂蕶��
                    ''''''cwStartDataInfoForDataSet(1).p_strDataName = DATA_NAME + "��������"                         '�f�[�^��
                    ''''''cwStartDataInfoForDataSet(1).p_strDataKbn = UWStartDataInfo.DATAKBN_DATA                    '�f�[�^�敪
                    ''''''cwStartDataInfoForDataSet(1).p_strCompressionType = UWStartDataInfo.COMPRESSIONTYPE_NONE    '���k�`��
                    ''''''cwStartDataInfoForDataSet(1).p_strEncryptionType = UWStartDataInfo.ENCRYPTIONTYPE_NONE
                    ''''''cwStartDataInfoForDataSet(1).p_strCharCode = UWStartDataInfo.CHARCODE_SJIS + UWStartDataInfo.CHAR_RENKETSU + UWStartDataInfo.GAIJI_DENSANUSER
                    ''''''cwStartDataInfoForDataSet(1).p_strDataType = UWStartDataInfo.DATATYPE_TXT
                    ''''''cwStartDataInfoForDataSet(1).p_csData = csToshoEntity.Tables(ABToshoPrmEntity.TABLE_NAME)
                    '-------------------------------------
                    ''''''cwStartDataInfoForDataSet(0).p_csData = csToshoEntity.Tables(ABToshoTable.TABLE_NAME)
                    '*����ԍ� 000004 2005/02/28 �C���I��
                Case KOKUHO
                    cwStartDataInfoForDataSet(0).p_csData = csToshoEntity.Tables(ABKobetsuKokuhoEntity.TABLE_NAME)
                    '*����ԍ� 000005 2005/10/17 �ǉ��J�n
                Case JITE
                    cwStartDataInfoForDataSet(0).p_csData = csToshoEntity.Tables(ABKobetsuJiteEntity.TABLE_NAME)
                    '*����ԍ� 000005 2005/10/17 �ǉ��I��
                    '*����ԍ� 000006 2008/05/14 �ǉ��J�n
                Case KAIGO
                    cwStartDataInfoForDataSet(0).p_csData = csToshoEntity.Tables(ABKobetsuKaigoEntity.TABLE_NAME)
                    '*����ԍ� 000006 2008/05/14 �ǉ��I��
            End Select
            cwMessage.SetAryDataInfoFromDataSet(cwStartDataInfoForDataSet)

            Try
                cwStartRetInfo = New UWStartRetInfo()
                '*����ԍ� 000005 2005/10/17 �C���J�n
                ''''cwStartRetInfo = cwMessage.SendPreStartMsg()
                Try
                    cwStartRetInfo = cwMessage.SendPreStartMsg()
                Catch
                    cwStartRetInfo = cwMessage.SendPreStartCancel()
                    Throw
                End Try
                '*����ԍ� 000005 2005/10/17 �C���I��

                If (cwStartRetInfo.p_enStatus = UWReturnCodeTyep.SUCCESS) Then
                    Try
                        ' ���[�N�t���[�N���n�j
                        '�{���͂����ŃR�~�b�g�����Ȃ���΂Ȃ�Ȃ�
                    Catch objExp As Exception
                        m_cfLog.DebugWrite(m_cfControlData, "���[�N�t���[�N���E�X�e�b�v�Q�^" + objExp.ToString)
                        cwStartRetInfo = cwMessage.SendPreStartCancel()
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End Try
                    Try
                        cwStartRetInfo = cwMessage.SendStartMsg()
                        If (cwStartRetInfo.p_enStatus = UWReturnCodeTyep.ERROR) Then
                            m_cfLog.DebugWrite(m_cfControlData, "���[�N�t���[�N���E�X�e�b�v�R�^���s")
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    Catch objExp As Exception
                        m_cfLog.DebugWrite(m_cfControlData, "���[�N�t���[�N���E�X�e�b�v�R�^" + objExp.ToString)
                        'System.Diagnostics.Debug.WriteLine(ex.Message)
                        cwStartRetInfo = cwMessage.SendPreStartCancel()
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End Try
                    '*����ԍ� 000005 2005/10/17 �ǉ��J�n
                Else
                    m_cfLog.DebugWrite(m_cfControlData, "���[�N�t���[�N���E�X�e�b�v�P�^���s")
                    cwStartRetInfo = cwMessage.SendPreStartCancel()
                    m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '*����ԍ� 000005 2005/10/17 �ǉ��I��
                End If
            Catch objExp As Exception
                m_cfLog.DebugWrite(m_cfControlData, "���[�N�t���[�N���E�X�e�b�v�P�^" + objExp.ToString)
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003070)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End Try

            ' �f�o�b�O���O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

    End Sub
#End Region

    '*����ԍ� 000004 2005/02/28 �ǉ��J�n
#Region "���v���J�f�[�^�쐬�p�p�����[�^�f�[�^�J�����쐬"
    '************************************************************************************************
    '* ���\�b�h��      ���v���J�f�[�^�쐬�p�p�����[�^�f�[�^�J�����쐬
    '* 
    '* �\��            Private Function CreateColumnsData() As DataTable
    '* 
    '* �@�\�@�@        ���v���J�c�a�̃J������`���쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataTable() ��[���e�[�u��
    '************************************************************************************************
    Public Function CreateColumnsToshoPrmData() As DataTable
        Const THIS_METHOD_NAME As String = "CreateColumnsToshoPrmData"
        Dim csABToshoPrmTable As DataTable                       '���v���J�쐬�p�p�����[�^�f�[�^�e�[�u��
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���v���J�f�[�^�쐬�p�p�����[�^�J������`
            csABToshoPrmTable = New DataTable()
            csABToshoPrmTable.TableName = ABToshoPrmEntity.TABLE_NAME
            csDataColumn = csABToshoPrmTable.Columns.Add(ABToshoPrmEntity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csABToshoPrmTable.Columns.Add(ABToshoPrmEntity.STAICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csABToshoPrmTable.Columns.Add(ABToshoPrmEntity.KOSHINKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1

            ' �f�o�b�O���O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try
        Return csABToshoPrmTable

    End Function
#End Region
    '*����ԍ� 000004 2005/02/28 �ǉ��I��

End Class