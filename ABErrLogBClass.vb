'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        �����X�V�G���[���O�c�a�Ǘ�(ABErrLogBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2007/02/05�@���R �����Y
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
Imports Densan.Common
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Text
Imports System.Web

Public Class ABErrLogBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfConfigDataClass As UFConfigDataClass                      ' �R���t�B�O�f�[�^
    Private m_cfControlData As UFControlData                              ' �R���g���[���f�[�^
    Private m_cfLogClass As UFLogClass                                    ' ���O�o�̓N���X
    Private m_cfInsParamCollection As UFParameterCollectionClass          ' INSERT�p�p�����[�^�R���N�V����
    Private m_strInsertSQL As String                                      ' INSERT�pSQL
    Private m_strRsBusinId As String                                      ' �r�W�l�X�h�c�ۑ��p

    ' �R���X�^���g��`
    Private Const TAISHOKBN_MIKAKUNIN As String = "0"                     ' ���m�F
    Private Const TAISHOKBN_ZUMI As String = "1"                          ' �m�F��
    Private Const JOKYOKBN_NORMAL As String = "0"                         ' ����I��
    Private Const JOKYOKBN_ERR As String = "9"                            ' �ُ�I��
    Private Const SPACE As String = " "                                   ' SPACE

    Private Const THIS_CLASS_NAME As String = "ABErrLogBClass"            ' �N���X��

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfControlData As UFControlData,
    '* �@�@                           ByVal cfConfigDataClass As UFConfigDataClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@            cfConfigDataClass As UFConfigDataClass : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass)

        ' �����o�ϐ��փZ�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass

        ' ���O�o�̓N���X�C���X�^���X��
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' �󂯎�����r�W�l�XID�������o�֕ۑ�
        m_strRsBusinId = m_cfControlData.m_strBusinessId

        ' �����o�ϐ��̏�����
        m_strInsertSQL = String.Empty
        m_cfInsParamCollection = Nothing

    End Sub

#End Region

#Region "�G���[���O�擾"
    '************************************************************************************************
    '* ���\�b�h��      �G���[���O�擾
    '* 
    '* �\��            Public Function GetABErrLog() As String()
    '* 
    '* �@�\            �G���[���O�̎擾���s�Ȃ�
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          String()�F�G���[�����ꏊ�ƃG���[���b�Z�[�W
    '************************************************************************************************
    Public Function GetABErrLog() As String()

        Const THIS_METHOD_NAME As String = "GetABErrLog"
        Dim cfRdb As UFRdbClass
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csABErrLogEntity As DataSet
        Dim csDataRow As DataRow
        Dim intCnt As Integer
        Dim strGyomuMei As String
        Dim strErrMSG As String
        Dim strReturn() As String
        Dim strSQL As New StringBuilder

        Try
            ' �Ɩ��h�c������(AB)�ɕύX
            m_cfControlData.m_strBusinessId = "AB"

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                 "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                 "�y���s���\�b�h��:Connect�z")

            ' RDB�N���X�̃C���X�^���X�쐬
            cfRdb = New UFRdbClass(m_cfControlData.m_strBusinessId)

            ' RDB�ڑ�
            cfRdb.Connect()

            ' SelectSQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABErrLogEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABErrLogEntity.TAISHOKB)
            strSQL.Append(" = ")
            strSQL.Append(ABErrLogEntity.KEY_TAISHOKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABErrLogEntity.JOKYOKB)
            strSQL.Append(" = ")
            strSQL.Append(ABErrLogEntity.KEY_JOKYOKB)
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABErrLogEntity.LOGNO)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TAISHOKB          ' �Ώ��敪
            cfUFParameterClass.Value = TAISHOKBN_MIKAKUNIN
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_JOKYOKB           ' �󋵋敪
            cfUFParameterClass.Value = JOKYOKBN_ERR
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                 "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                 "�y���s���\�b�h��:GetDataSet�z" + _
                                 "�ySQL���e:" + cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL���s DataSet�擾
            csABErrLogEntity = cfRdb.GetDataSet(strSQL.ToString, ABErrLogEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �߂�l�ҏW�p�z�񏉊���
            Dim strRet(csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows.Count - 1) As String

            ' �߂�l�ҏW
            'For intCnt = 0 To csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows.Count - 1
            '    csDataRow = csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows(intCnt)
            '    strGyomuMei = CType(csDataRow(ABErrLogEntity.MSG5), String).Trim
            '    strErrMSG = CType(csDataRow(ABErrLogEntity.MSG7), String).Trim
            '    strRet(intCnt) = strGyomuMei + "," + strErrMSG
            'Next intCnt

            intCnt = 0
            For Each csDataRow In csABErrLogEntity.Tables(ABErrLogEntity.TABLE_NAME).Rows
                strGyomuMei = CType(csDataRow(ABErrLogEntity.MSG5), String).Trim          ' �G���[�����ꏊ
                strErrMSG = CType(csDataRow(ABErrLogEntity.MSG7), String).Trim            ' �G���[���b�Z�[�W
                strRet(intCnt) = strGyomuMei + "," + strErrMSG
                intCnt += 1
            Next csDataRow

            ' �߂�l�Z�b�g
            strReturn = strRet

        Catch objRdbExp As UFRdbException                          ' RdbException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O���e:" + objRdbExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw objRdbExp

        Catch objRdbDeadLockExp As UFRdbDeadLockException          ' �f�b�h���b�N���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O�R�[�h:" + objRdbDeadLockExp.p_strErrorCode + "�z" + _
                                     "�y���[�j���O���e:" + objRdbDeadLockExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw objRdbDeadLockExp

        Catch objUFRdbUniqueExp As UFRdbUniqueException            ' ��Ӑ���ᔽ���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O�R�[�h:" + objUFRdbUniqueExp.p_strErrorCode + "�z" + _
                                     "�y���[�j���O���e:" + objUFRdbUniqueExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw objUFRdbUniqueExp

        Catch objRdbTimeOutExp As UFRdbTimeOutException            ' UFRdbTimeOutException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" + _
                                     "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw objRdbTimeOutExp

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                     "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception                             ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                   "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                   "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                   "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        Finally
            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                 "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                 "�y���s���\�b�h��:Disconnect�z")

            ' RDB�ؒf
            cfRdb.Disconnect()

            ' ���̃r�W�l�XID������
            m_cfControlData.m_strBusinessId = m_strRsBusinId

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        End Try

        ' �߂�l�ݒ�
        Return strReturn

    End Function

#End Region

#Region "�G���[���O�ǉ�"
    '************************************************************************************************
    '* ���\�b�h��      �G���[���O�ǉ�
    '* 
    '* �\��            Public Function InsertABErrLog(ByVal cABErrLogXClass As ABErrLogXClass) As Integer
    '* 
    '* �@�\            �G���[���O�̒ǉ����s�Ȃ�
    '* 
    '* ����            cABErrLogXClass As ABErrLogXClass : �ǉ��f�[�^
    '* 
    '* �߂�l          Integer �F �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertABErrLog(ByVal cABErrLogXClass As ABErrLogXClass) As Integer

        Const THIS_METHOD_NAME As String = "InsertABErrLog"
        Dim cABAkibanShutokuBClass As ABAkibanShutokuBClass          ' �G���[���O�ԍ���Ԏ擾
        Dim cfErrorClass As UFErrorClass                             ' �G���[�N���X
        Dim cfErrorStruct As UFErrorStruct                           ' �G���[��`�\����
        Dim cfRdb As UFRdbClass
        Dim cfUFParameterClass As UFParameterClass
        Dim intCheckCnt As Integer
        Dim intInsCnt As Integer
        Dim strErrLogNo As String
        Dim strSystemDateTime As String
        Dim strSystemDate As String
        Dim strSystemTime As String

        Try
            ' �Ɩ��h�c������(AB)�ɕύX
            m_cfControlData.m_strBusinessId = "AB"

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                 "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                 "�y���s���\�b�h��:Connect�z")

            ' RDB�N���X�̃C���X�^���X�쐬
            cfRdb = New UFRdbClass(m_cfControlData.m_strBusinessId)

            ' RDB�ڑ�
            cfRdb.Connect()

            ' �����`�F�b�N
            ' �󔒃`�F�b�N
            If (cABErrLogXClass.p_strShichosonCD.Trim = String.Empty) Then          ' �s�����R�[�h
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' ��O�𐶐�
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "�s�����R�[�h�z", cfErrorStruct.m_strErrorCode)
            End If

            ' �������`�F�b�N
            If (cABErrLogXClass.p_strShichosonCD.RLength > 6) Then                   ' �s�����R�[�h
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001030)
                ' ��O�𐶐�
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "�s�����R�[�h�z", cfErrorStruct.m_strErrorCode)
            End If

            ' ���l�`�F�b�N
            For intCheckCnt = 1 To Len(cABErrLogXClass.p_strShichosonCD)            ' �s�����R�[�h
                If Not Mid(cABErrLogXClass.p_strShichosonCD, intCheckCnt, 1) Like "[0-9]" Then
                    cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001014)
                    ' ��O�𐶐�
                    Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "�s�����R�[�h�z", cfErrorStruct.m_strErrorCode)
                End If
            Next intCheckCnt

            ' �󔒃`�F�b�N
            If (cABErrLogXClass.p_strShoriID.Trim = String.Empty) Then              ' �����h�c
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' ��O�𐶐�
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "�����h�c�z", cfErrorStruct.m_strErrorCode)
            End If

            ' �������`�F�b�N
            If (cABErrLogXClass.p_strShoriID.RLength > 5) Then                       ' �����h�c
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001030)
                ' ��O�𐶐�
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "�����h�c�z", cfErrorStruct.m_strErrorCode)
            End If

            ' ���p�`�F�b�N
            For intCheckCnt = 1 To Len(cABErrLogXClass.p_strShoriID)                ' �����h�c
                If Not Mid(cABErrLogXClass.p_strShoriID, intCheckCnt, 1) Like "[0-9a-zA-Z]" Then
                    cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001080)
                    ' ��O�𐶐�
                    Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "�����h�c�z", cfErrorStruct.m_strErrorCode)
                End If
            Next intCheckCnt

            ' �󔒃`�F�b�N
            If (cABErrLogXClass.p_strShoriShu.Trim = String.Empty) Then             ' �������
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' ��O�𐶐�
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "������ʁz", cfErrorStruct.m_strErrorCode)
            End If

            ' �������`�F�b�N
            If (cABErrLogXClass.p_strShoriShu.RLength > 4) Then                      ' �������
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001030)
                ' ��O�𐶐�
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "������ʁz", cfErrorStruct.m_strErrorCode)
            End If

            ' ���p�`�F�b�N
            For intCheckCnt = 1 To Len(cABErrLogXClass.p_strShoriShu)               ' �������
                If Not Mid(cABErrLogXClass.p_strShoriShu, intCheckCnt, 1) Like "[0-9a-zA-Z]" Then
                    cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                    cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001080)
                    ' ��O�𐶐�
                    Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "������ʁz", cfErrorStruct.m_strErrorCode)
                End If
            Next intCheckCnt

            ' �󔒃`�F�b�N
            If (cABErrLogXClass.p_strMsg5.Trim = String.Empty) Then                 ' ���b�Z�[�W�T�i�G���[�����ꏊ�j
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' ��O�𐶐�
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "�G���[�����ꏊ�z", cfErrorStruct.m_strErrorCode)
            End If

            ' �󔒃`�F�b�N
            If (cABErrLogXClass.p_strMsg6.Trim = String.Empty) Then                 ' ���b�Z�[�W�U�i�Z���R�[�h�j
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' ��O�𐶐�
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "�Z���R�[�h�z", cfErrorStruct.m_strErrorCode)
            End If

            ' �󔒃`�F�b�N
            If (cABErrLogXClass.p_strMsg7.Trim = String.Empty) Then                 ' ���b�Z�[�W�V�i�G���[���b�Z�[�W�j
                cfErrorClass = New UFErrorClass(URCommonXClass.GYOMUCD_REAMS)
                cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                ' ��O�𐶐�
                Throw New UFAppException(cABErrLogXClass.p_strMsg7.Trim + "\n�y" + cfErrorStruct.m_strErrorMessage + "�G���[���b�Z�[�W�z", cfErrorStruct.m_strErrorCode)
            End If

            ' InsertSQL���̐��`���쐬
            Call CreateInsertSQL()

            ' ��Ԏ擾�N���X�̃C���X�^���X��
            cABAkibanShutokuBClass = New ABAkibanShutokuBClass(m_cfControlData, m_cfConfigDataClass)
            cABAkibanShutokuBClass.GetErrLogNo()

            ' �G���[���O�ԍ����擾
            strErrLogNo = cABAkibanShutokuBClass.p_strBango

            ' �c�a�����̎擾
            strSystemDateTime = cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff")          ' �c�a����
            strSystemDate = cfRdb.GetSystemDate.ToString("yyyyMMdd")                         ' �c�a���t
            strSystemTime = cfRdb.GetSystemDate.ToString("HHmmss")                           ' �c�a����

            ' �p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            m_cfInsParamCollection = New UFParameterCollectionClass

            ' ���ڂ̕ҏW
            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGNO                   ' ���O�ԍ�
            cfUFParameterClass.Value = strErrLogNo
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_YMD                  ' �J�n�N����
            cfUFParameterClass.Value = strSystemDate
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_TIME                 ' �J�n����
            cfUFParameterClass.Value = strSystemTime
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORIID                 ' �����h�c
            cfUFParameterClass.Value = cABErrLogXClass.p_strShoriID.Trim
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORISHU                ' �������
            cfUFParameterClass.Value = cABErrLogXClass.p_strShoriShu.Trim
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TAISHOKB                ' �Ώ��敪
            cfUFParameterClass.Value = TAISHOKBN_MIKAKUNIN
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_JOKYOKB                 ' �󋵋敪
            cfUFParameterClass.Value = JOKYOKBN_ERR
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHINCHOKURITSU          ' �i����
            cfUFParameterClass.Value = String.Empty
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS1                    ' �X�e�[�^�X�P
            cfUFParameterClass.Value = String.Empty
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS2                    ' �X�e�[�^�X�Q
            cfUFParameterClass.Value = String.Empty
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_YMD                  ' �I���N����
            cfUFParameterClass.Value = String.Empty
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_TIME                 ' �I������
            cfUFParameterClass.Value = String.Empty
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG1                    ' ���b�Z�[�W�P
            ' �������`�F�b�N
            If (cABErrLogXClass.p_strMsg1.RLength > 8) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg1.RSubstring(0, 8).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg1.Trim
            End If
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG2                    ' ���b�Z�[�W�Q
            If (cABErrLogXClass.p_strMsg2.RLength > 8) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg2.RSubstring(0, 8).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg2.Trim
            End If
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG3                    ' ���b�Z�[�W�R
            If (cABErrLogXClass.p_strMsg3.RLength > 8) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg3.RSubstring(0, 8).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg3.Trim
            End If
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG4                    ' ���b�Z�[�W�S
            If (cABErrLogXClass.p_strMsg4.RLength > 8) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg4.RSubstring(0, 8).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg4.Trim
            End If
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG5                    ' ���b�Z�[�W�T
            If (cABErrLogXClass.p_strMsg5.RLength > 15) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg5.RSubstring(0, 15).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg5.Trim
            End If
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG6                    ' ���b�Z�[�W�U
            If (cABErrLogXClass.p_strMsg6.RLength > 40) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg6.RSubstring(0, 40).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg6.Trim
            End If
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG7                    ' ���b�Z�[�W�V
            If (cABErrLogXClass.p_strMsg7.RLength > 100) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg7.RSubstring(0, 100).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg7.Trim
            End If
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG8                    ' ���b�Z�[�W�W
            If (cABErrLogXClass.p_strMsg8.RLength > 120) Then
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg8.RSubstring(0, 120).Trim
            Else
                cfUFParameterClass.Value = cABErrLogXClass.p_strMsg8.Trim
            End If
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGFILEMEI              ' ���O�t�@�C����
            cfUFParameterClass.Value = String.Empty
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHICHOSONCD             ' �s�����R�[�h
            cfUFParameterClass.Value = cABErrLogXClass.p_strShichosonCD.Trim
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KYUSHICHOSONCD          ' ���s�����R�[�h
            cfUFParameterClass.Value = cABErrLogXClass.p_strShichosonCD.Trim
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_RESERVE30BYTE           ' ���U�[�u
            cfUFParameterClass.Value = String.Empty
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TANMATSUID              ' �[���h�c
            cfUFParameterClass.Value = m_cfControlData.m_strClientId
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUJOFG                ' �폜�t���O
            cfUFParameterClass.Value = "0"
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINCOUNTER           ' �X�V�J�E���^
            cfUFParameterClass.Value = Decimal.Zero
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEINICHIJI          ' �쐬����
            cfUFParameterClass.Value = strSystemDateTime
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEIUSER             ' �쐬���[�U�[
            cfUFParameterClass.Value = m_cfControlData.m_strUserId
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINNICHIJI           ' �X�V����
            cfUFParameterClass.Value = strSystemDateTime
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' �p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINUSER              ' �X�V���[�U�[
            cfUFParameterClass.Value = m_cfControlData.m_strUserId
            ' �p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                 "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                 "�y���s���\�b�h��:INSERT�z" + _
                                 "�ySQL���e:" + cfRdb.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsParamCollection) + "�z")

            ' SQL���s
            intInsCnt = cfRdb.ExecuteSQL(m_strInsertSQL, m_cfInsParamCollection)

        Catch objRdbExp As UFRdbException                          ' RdbException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O���e:" + objRdbExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw objRdbExp

        Catch objRdbDeadLockExp As UFRdbDeadLockException          ' �f�b�h���b�N���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O�R�[�h:" + objRdbDeadLockExp.p_strErrorCode + "�z" + _
                                     "�y���[�j���O���e:" + objRdbDeadLockExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw objRdbDeadLockExp

        Catch objUFRdbUniqueExp As UFRdbUniqueException            ' ��Ӑ���ᔽ���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O�R�[�h:" + objUFRdbUniqueExp.p_strErrorCode + "�z" + _
                                     "�y���[�j���O���e:" + objUFRdbUniqueExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw objUFRdbUniqueExp

        Catch objRdbTimeOutExp As UFRdbTimeOutException            ' UFRdbTimeOutException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" + _
                                     "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw objRdbTimeOutExp

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                     "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception                             ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                   "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                   "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                   "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        Finally
            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                 "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                 "�y���s���\�b�h��:Disconnect�z")

            ' RDB�ؒf
            cfRdb.Disconnect()

            ' ���̃r�W�l�XID������
            m_cfControlData.m_strBusinessId = m_strRsBusinId

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        End Try

        ' �߂�l�ݒ�
        Return intInsCnt

    End Function

#End Region

#Region "InsertSQL���̐��`���쐬"
    '************************************************************************************************
    '* ���\�b�h��      InsertSQL���̐��`���쐬
    '* 
    '* �\��            Private Sub CreateInsertSQL()
    '* 
    '* �@�\�@�@    �@�@InsertSQL�̐��^�ƃp�����[�^�R���N�V�������쐬����
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Private Sub CreateInsertSQL()

        Const THIS_METHOD_NAME As String = "CreateInsertSQL"
        Dim cfUFParameterClass As UFParameterClass
        Dim strInsertColumn As New StringBuilder
        Dim strInsertParam As New StringBuilder
        Dim strInsertSQL As New StringBuilder

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' InsertSQL���̍쐬
            strInsertSQL.Append("INSERT INTO ")
            strInsertSQL.Append(ABErrLogEntity.TABLE_NAME)
            strInsertSQL.Append(" ")

            ' INSERT�p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsParamCollection = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            cfUFParameterClass = New UFParameterClass

            ' InsertSQL���̍쐬
            strInsertColumn.Append(ABErrLogEntity.LOGNO)                   ' ���O�ԍ�
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.ST_YMD)                  ' �J�n�N����
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.ST_TIME)                 ' �J�n����
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SHORIID)                 ' �����h�c
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SHORISHU)                ' �������
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.TAISHOKB)                ' �Ώ��敪
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.JOKYOKB)                 ' �󋵋敪
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SHINCHOKURITSU)          ' �i����
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.STS1)                    ' �X�e�[�^�X�P
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.STS2)                    ' �X�e�[�^�X�Q
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.ED_YMD)                  ' �I���N����
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.ED_TIME)                 ' �I������
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG1)                    ' ���b�Z�[�W�P
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG2)                    ' ���b�Z�[�W�Q
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG3)                    ' ���b�Z�[�W�R
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG4)                    ' ���b�Z�[�W�S
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG5)                    ' ���b�Z�[�W�T
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG6)                    ' ���b�Z�[�W�U
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG7)                    ' ���b�Z�[�W�V
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.MSG8)                    ' ���b�Z�[�W�W
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.LOGFILEMEI)              ' ���O�t�@�C����
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SHICHOSONCD)             ' �s�����R�[�h
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.KYUSHICHOSONCD)          ' ���s�����R�[�h
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.RESERVE30BYTE)           ' ���U�[�u
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.TANMATSUID)              ' �[���h�c
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SAKUJOFG)                ' �폜�t���O
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.KOSHINCOUNTER)           ' �X�V�J�E���^
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SAKUSEINICHIJI)          ' �쐬����
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.SAKUSEIUSER)             ' �쐬���[�U�[
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.KOSHINNICHIJI)           ' �X�V����
            strInsertColumn.Append(", ")
            strInsertColumn.Append(ABErrLogEntity.KOSHINUSER)              ' �X�V���[�U�[

            strInsertParam.Append(ABErrLogEntity.KEY_LOGNO)                   ' ���O�ԍ�
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_ST_YMD)                  ' �J�n�N����
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_ST_TIME)                 ' �J�n����
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SHORIID)                 ' �����h�c
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SHORISHU)                ' �������
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_TAISHOKB)                ' �Ώ��敪
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_JOKYOKB)                 ' �󋵋敪
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SHINCHOKURITSU)          ' �i����
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_STS1)                    ' �X�e�[�^�X�P
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_STS2)                    ' �X�e�[�^�X�Q
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_ED_YMD)                  ' �I���N����
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_ED_TIME)                 ' �I������
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG1)                    ' ���b�Z�[�W�P
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG2)                    ' ���b�Z�[�W�Q
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG3)                    ' ���b�Z�[�W�R
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG4)                    ' ���b�Z�[�W�S
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG5)                    ' ���b�Z�[�W�T
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG6)                    ' ���b�Z�[�W�U
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG7)                    ' ���b�Z�[�W�V
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_MSG8)                    ' ���b�Z�[�W�W
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_LOGFILEMEI)              ' ���O�t�@�C����
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SHICHOSONCD)             ' �s�����R�[�h
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_KYUSHICHOSONCD)          ' ���s�����R�[�h
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_RESERVE30BYTE)           ' ���U�[�u
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_TANMATSUID)              ' �[���h�c
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SAKUJOFG)                ' �폜�t���O
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_KOSHINCOUNTER)           ' �X�V�J�E���^
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SAKUSEINICHIJI)          ' �쐬����
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_SAKUSEIUSER)             ' �쐬���[�U�[
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_KOSHINNICHIJI)           ' �X�V����
            strInsertParam.Append(", ")
            strInsertParam.Append(ABErrLogEntity.KEY_KOSHINUSER)              ' �X�V���[�U�[

            ' INSERT�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGNO                   ' ���O�ԍ�
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_YMD                  ' �J�n�N����
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ST_TIME                 ' �J�n����
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORIID                 ' �����h�c
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHORISHU                ' �������
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TAISHOKB                ' �Ώ��敪
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_JOKYOKB                 ' �󋵋敪
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHINCHOKURITSU          ' �i����
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS1                    ' �X�e�[�^�X�P
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_STS2                    ' �X�e�[�^�X�Q
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_YMD                  ' �I���N����
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_ED_TIME                 ' �I������
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG1                    ' ���b�Z�[�W�P
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG2                    ' ���b�Z�[�W�Q
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG3                    ' ���b�Z�[�W�R
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG4                    ' ���b�Z�[�W�S
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG5                    ' ���b�Z�[�W�T
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG6                    ' ���b�Z�[�W�U
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG7                    ' ���b�Z�[�W�V
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_MSG8                    ' ���b�Z�[�W�W
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_LOGFILEMEI              ' ���O�t�@�C����
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SHICHOSONCD             ' �s�����R�[�h
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KYUSHICHOSONCD          ' ���s�����R�[�h
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_RESERVE30BYTE           ' ���U�[�u
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_TANMATSUID              ' �[���h�c
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUJOFG                ' �폜�t���O
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINCOUNTER           ' �X�V�J�E���^
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEINICHIJI          ' �쐬����
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_SAKUSEIUSER             ' �쐬���[�U�[
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINNICHIJI           ' �X�V����
            m_cfInsParamCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABErrLogEntity.KEY_KOSHINUSER              ' �X�V���[�U�[
            m_cfInsParamCollection.Add(cfUFParameterClass)

            ' InsertSQL���̌���
            strInsertSQL.Append("(")
            strInsertSQL.Append(strInsertColumn)
            strInsertSQL.Append(")")
            strInsertSQL.Append(" VALUES (")
            strInsertSQL.Append(strInsertParam)
            strInsertSQL.Append(")")

            ' String�^�ɕϊ�
            m_strInsertSQL = strInsertSQL.ToString

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                     "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                     "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                     "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                     "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw exAppException

        Catch exException As Exception          ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                   "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                   "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                   "�y�G���[���e:" + exException.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw exException

        Finally
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        End Try

    End Sub

#End Region

End Class
