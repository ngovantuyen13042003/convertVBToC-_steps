'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        ���t��_�W���}�X�^�c�`(ABSfsk_HyojunBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2023/10/20 ���� �Y��
'*
'* ���쌠          �i���j�d�Z 
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2024/6/11   000001    �yAB-9901-1�z�s��Ή�
'************************************************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Text

Public Class ABSfsk_HyojunBClass
#Region "�����o�ϐ�"

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABSfsk_HyojunBClass"
    Private Const THIS_BUSINESSID As String = "AB"                                  '�Ɩ��R�[�h
    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"
    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Public m_blnBatch As Boolean = False                                            '�o�b�`�t���O
    Private m_csDataSchma As DataSet                                                '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchma_Hyojun As DataSet                                         '�X�L�[�}�ۊǗp�f�[�^�Z�b�g_�W����

    '�����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                                              ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                                        ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass                                ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                                              ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                                          ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                                            ' ���t�N���X
    Private m_strInsertSQL As String                                                ' INSERT�pSQL
    Private m_strUpdateSQL As String                                                ' UPDATE�pSQL
    Private m_strDeleteSQL As String                                                ' DELETE�pSQL�i�����j
    Private m_strDelRonriSQL As String                                              ' DELETE�pSQL�i�_���j
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      ' INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      ' UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass      ' DELETE�p�p�����[�^�R���N�V�����i�����j
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    ' DELETE�p�p�����[�^�R���N�V�����i�_���j

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfControlData As UFControlData,
    '*                                ByVal cfConfigDataClass As UFConfigDataClass,
    '*                                ByVal cfRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                 cfConfigDataClass As UFConfigDataClass : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '*                 cfRdbClass As UFRdbClass               : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass)

        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' �����o�ϐ��̏�����
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@ ���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String  :�Z���R�[�h
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String) As DataSet

        Return GetSfskBHoshu(strJuminCD, String.Empty, String.Empty, String.Empty, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@ ���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String    :�Z���R�[�h
    '*                blnSakujoFG As Boolean  :�폜�t���O
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, ByVal blnSakujoFG As Boolean) As DataSet

        Return GetSfskBHoshu(strJuminCD, String.Empty, String.Empty, String.Empty, blnSakujoFG)

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, _
    '*                                                          ByVal strGyomuCD As String, _
    '*                                                          ByVal strGyomunaiShuCD As String, _
    '*                                                          ByVal strTorokurenban As String) As DataSet
    '* 
    '* �@�\           ���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String        :�Z���R�[�h
    '*                strGyomuCD As String        :�Ɩ��R�[�h
    '*                strGyomunaiShuCD As String  :�Ɩ�����ʃR�[�h
    '*                strTorokurenban As String   :�o�^�A��
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String,
                                              ByVal strGyomuCD As String,
                                              ByVal strGyomunaiShuCD As String,
                                              ByVal strTorokurenban As String) As DataSet

        Return GetSfskBHoshu(strJuminCD, strGyomuCD, strGyomunaiShuCD, strTorokurenban, True)

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, _
    '*                                                          ByVal strGyomuCD As String, _
    '*                                                          ByVal strGyomunaiShuCD As String, _
    '*                                                          ByVal strTorokurenban As String, _
    '*                                                          ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@ ���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String        :�Z���R�[�h
    '*                strGyomuCD As String        :�Ɩ��R�[�h
    '*                strGyomunaiShuCD As String  :�Ɩ�����ʃR�[�h
    '*                strTorokurenban As String   :�o�^�A��
    '*                blnSakujoFG As Boolean      :�폜�t���O
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String,
                                              ByVal strGyomuCD As String,
                                              ByVal strGyomunaiShuCD As String,
                                              ByVal strTorokurenban As String,
                                              ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetSfskBHoshu"            '���̃��\�b�h��
        Dim csSfskEntity As DataSet                                     '���t��}�X�^�f�[�^
        Dim strSQL As String                                            'SQL��������
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim blnSakujo As Boolean                                        '�폜�f�[�^�ǂݍ���

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Dim intWkKensu As Integer
            intWkKensu = m_cfRdbClass.p_intMaxRows()

            'SQL,�p�����[�^�R���N�V�����̍쐬
            blnSakujo = blnSakujoFG
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            strSQL = Me.CreateSql_Param(strJuminCD, strGyomuCD, strGyomunaiShuCD, True, strTorokurenban, blnSakujo, cfUFParameterCollectionClass)

            ' RDB�A�N�Z�X���O�o��
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + Me.GetType.Name + "�z" +
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                            "�y���s���\�b�h��:GetDataSet�z" +
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            End If

            'SQL�̎��s DataSet�̎擾
            csSfskEntity = m_csDataSchma.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, csSfskEntity, ABSfskHyojunEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


            m_cfRdbClass.p_intMaxRows = intWkKensu

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csSfskEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��_�W���f�[�^�쐬
    '* 
    '* �\��           Public Function CreateSfskHyojunData(ByVal csDataRow As DataRow, ByVal csSfskEntity As DataSet) As DataRow
    '*                                      
    '* 
    '* �@�\�@�@    �@ ���t��_�W���f�[�^���쐬����
    '* 
    '* ����           csDataRow As DataRow      : ���t��f�[�^
    '*                csSfskEntity As DataSet   : ���t��G���e�B�e�B
    '* 
    '* �߂�l         DataRow
    '************************************************************************************************
    Public Function CreateSfskHyojunData(ByVal csDataRow As DataRow, ByVal csSfskEntity As DataSet) As DataRow
        Const THIS_METHOD_NAME As String = "CreateSfskHyojunData"
        Dim csSfskHyojunRows() As DataRow
        Dim csSfskHyojunRow As DataRow
        Dim csDataColumn As DataColumn
        Dim csDataHyojunColumn As DataColumn
        Dim strSelect As StringBuilder                                         ' ���oSQL

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���t��_�W����DateRow���쐬
            csSfskHyojunRow = csSfskEntity.Tables(ABSfskHyojunEntity.TABLE_NAME).NewRow

            '���R�[�h�̓���
            strSelect = New StringBuilder()
            strSelect.Append(ABSfskHyojunEntity.GYOMUCD)
            strSelect.Append("='")
            strSelect.Append(CType(csDataRow(ABSfskEntity.GYOMUCD), String))
            strSelect.Append("' AND ")

            strSelect.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strSelect.Append("='")
            strSelect.Append(CType(csDataRow(ABSfskEntity.GYOMUNAISHU_CD), String))
            strSelect.Append("' AND ")

            strSelect.Append(ABSfskHyojunEntity.TOROKURENBAN)
            strSelect.Append("='")
            strSelect.Append(CType(csDataRow(ABSfskEntity.TOROKURENBAN), String))
            strSelect.Append("'")

            csSfskHyojunRows = csSfskEntity.Tables(ABSfskHyojunEntity.TABLE_NAME).Select(strSelect.ToString)
            csSfskHyojunRow = csSfskHyojunRows(0)

            '���t��̃f�[�^�𑗕t��_�W���ɕϊ�
            For Each csDataHyojunColumn In csSfskHyojunRow.Table.Columns
                For Each csDataColumn In csDataRow.Table.Columns
                    If Not (csDataColumn.ColumnName = ABSfskEntity.KOSHINCOUNTER) Then
                        '�J����������v����f�[�^����
                        If (csDataColumn.ColumnName = csDataHyojunColumn.ColumnName) Then

                            csSfskHyojunRow(csDataHyojunColumn.ColumnName) = csDataRow(csDataColumn.ColumnName)

                            Exit For

                        End If
                    End If
                Next csDataColumn
            Next csDataHyojunColumn

            ' �f�o�b�O�I�����O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw
        End Try

        Return csSfskHyojunRow
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ ���t��}�X�^�Ƀf�[�^��ǉ�����B
    '* 
    '* ����           csDataRow As DataRow  :�ǉ��f�[�^
    '* 
    '* �߂�l         �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertSfskB"                '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer                                        '�ǉ�����
        Dim strUpdateDateTime As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing OrElse m_strInsertSQL = String.Empty OrElse
                    m_cfInsertUFParameterCollectionClass Is Nothing) Then

                Call CreateInsertSQL(csDataRow)

            End If

            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)          '�쐬����

            ' �ʍ��ڕҏW���s��
            csDataRow(ABSfskHyojunEntity.SFSKTOROKUYMD) = Left(strUpdateDateTime, 8)         '���t��o�^�N����

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABSfskHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId  '�[���h�c
            csDataRow(ABSfskHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF                     '�폜�t���O
            csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER) = Decimal.Zero                '�X�V�J�E���^
            csDataRow(ABSfskHyojunEntity.SAKUSEINICHIJI) = strUpdateDateTime          '�쐬����
            csDataRow(ABSfskHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   '�쐬���[�U�[
            csDataRow(ABSfskHyojunEntity.KOSHINNICHIJI) = strUpdateDateTime           '�X�V����
            csDataRow(ABSfskHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId    '�X�V���[�U�[

            ' ���N���X�̃f�[�^�������`�F�b�N���s��
            For Each csDataColumn In csDataRow.Table.Columns
                ' �f�[�^�������`�F�b�N
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value =
                    csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:ExecuteSQL�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ ���t��}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           csDataRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateSfskB"                '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        Dim intUpdCnt As Integer                                        '�X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing OrElse m_strUpdateSQL = String.Empty OrElse
                    m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateUpdateSQL(csDataRow)
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABSfskHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  '�[���h�c
            csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER) = CDec(csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER)) + 1       '�X�V�J�E���^
            csDataRow(ABSfskHyojunEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)    '�X�V����
            csDataRow(ABSfskHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABSfskHyojunEntity.PREFIX_KEY.RLength) = ABSfskHyojunEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength),
                                     csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength),
                                               DataRowVersion.Current).ToString.Trim)
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                        csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:ExecuteSQL�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")
            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^�폜�i�_���j
    '* 
    '* �\��           Public Function DeleteSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ ���t��}�X�^�̃f�[�^���폜�i�_���j����B
    '* 
    '* ����           csDataRow As DataRow  :�폜�f�[�^
    '* 
    '* �߂�l         �폜�i�_���j����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteSfskB�i�_���j"        '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        Dim intDelCnt As Integer                                        '�폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDelRonriSQL Is Nothing OrElse m_strDelRonriSQL = String.Empty OrElse
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                Call CreateDeleteRonriSQL(csDataRow)
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABSfskHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  '�[���h�c
            csDataRow(ABSfskHyojunEntity.SAKUJOFG) = SAKUJOFG_ON                                                      '�폜�t���O
            csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER) = CDec(csDataRow(ABSfskHyojunEntity.KOSHINCOUNTER)) + 1       '�X�V�J�E���^
            csDataRow(ABSfskHyojunEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)    '�X�V����
            csDataRow(ABSfskHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABSfskHyojunEntity.PREFIX_KEY.RLength) = ABSfskHyojunEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                        csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:ExecuteSQL�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")


            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^�폜�i�����j
    '* 
    '* �\��           Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow, 
    '*                                                      ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@ ���t��}�X�^�̃f�[�^���폜�i�����j����B
    '* 
    '* ����           csDataRow As DataRow      :�폜�f�[�^
    '*                strSakujoKB As String     :�폜�t���O
    '* 
    '* �߂�l         �폜�i�����j����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow, ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteSfskB�i�����j"
        Const SAKUJOKB_D As String = "D"                    '�폜�敪
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        Dim intDelCnt As Integer                            '�폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �����̍폜�敪���`�F�b�N
            If (strSakujoKB <> SAKUJOKB_D) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_DELETE_SAKUJOKB)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDeleteSQL Is Nothing OrElse m_strDeleteSQL = String.Empty OrElse
                    m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                Call CreateDeleteButsuriSQL(csDataRow)
            End If

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABSfskHyojunEntity.PREFIX_KEY.RLength) = ABSfskHyojunEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '�p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                        csDataRow(cfParam.ParameterName.RSubstring(ABSfskHyojunEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:ExecuteSQL�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     Insert�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           INSERT�p��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateInsertSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                 '�p�����[�^�N���X
        Dim strInsertColumn As StringBuilder                       '�ǉ�SQL�����ڕ�����
        Dim strInsertParam As StringBuilder                        '�ǉ�SQL���p�����[�^������

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'INSERT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABSfskHyojunEntity.TABLE_NAME + " "
            strInsertColumn = New StringBuilder
            strInsertParam = New StringBuilder

            'INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            '�p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                strInsertColumn.Append(csDataColumn.ColumnName)
                strInsertColumn.Append(", ")
                strInsertParam.Append(ABSfskHyojunEntity.PARAM_PLACEHOLDER)
                strInsertParam.Append(csDataColumn.ColumnName)
                strInsertParam.Append(", ")

                'INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            'INSERT SQL���̃g���~���O
            m_strInsertSQL += "(" + strInsertColumn.ToString.Trim().Trim(CType(",", Char)) + ")" _
                    + " VALUES (" + strInsertParam.ToString.Trim().TrimEnd(CType(",", Char)) + ")"

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     Update�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           UPDATE�p�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateUpdateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
        Dim strWhere As New StringBuilder                           '�X�V�폜SQL��Where��������

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�X�V�폜Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskHyojunEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_TOROKURENBAN)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_KOSHINCOUNTER)

            'UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABSfskHyojunEntity.TABLE_NAME + " SET "

            'UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            '�p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�Z���b�c�E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If (Not (csDataColumn.ColumnName = ABSfskHyojunEntity.JUMINCD) AndAlso
                        Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SAKUSEIUSER) AndAlso
                        Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SAKUSEINICHIJI)) Then
                    cfUFParameterClass = New UFParameterClass

                    'SQL���̍쐬
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABSfskHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    'UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            'UPDATE SQL���̃g���~���O
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            'UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += strWhere.ToString

            'UPDATE �R���N�V�����ɃL�[����ǉ�
            '�Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '�o�^�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_TOROKURENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '�X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �_���폜�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           �_��DELETE�p��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateDeleteRonriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateDeleteRonriSQL"
        Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
        Dim strDelRonriSQL As New StringBuilder                     '�_���폜SQL��������
        Dim strWhere As New StringBuilder                           '�X�V�폜SQL��Where��������

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�X�V�폜Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskHyojunEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_TOROKURENBAN)

            'DELETE�i�_���j SQL���̍쐬
            strDelRonriSQL.Append("UPDATE ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.TABLE_NAME)
            strDelRonriSQL.Append(" SET ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.TANMATSUID)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_TANMATSUID)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.SAKUJOFG)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_SAKUJOFG)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.KOSHINCOUNTER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_KOSHINCOUNTER)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.KOSHINNICHIJI)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_KOSHINNICHIJI)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.KOSHINUSER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_KOSHINUSER)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.RRKNO)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskHyojunEntity.PARAM_RRKNO)
            strDelRonriSQL.Append(strWhere.ToString)
            m_strDelRonriSQL = strDelRonriSQL.ToString

            'DELETE�i�_���j �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            'DELETE�i�_���j �R���N�V�����Ƀp�����[�^��ǉ�
            '�[���h�c
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�폜�t���O
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�X�V����
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�X�V���[�U
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '����ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.PARAM_RRKNO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�o�^�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_TOROKURENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �����폜�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           ����DELETE�p��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateDeleteButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
        Dim strDeleteSQL As New StringBuilder                       '�����폜SQL��������
        Dim strWhere As New StringBuilder                           '�X�V�폜SQL��Where��������

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�X�V�폜Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskHyojunEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskHyojunEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskHyojunEntity.KEY_TOROKURENBAN)

            'DELETE�i�����j SQL���̍쐬
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABSfskHyojunEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            'DELETE�i�����j �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            'DELETE(����) �R���N�V�����ɃL�[����ǉ�
            '�Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_JUMINCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_GYOMUNAISHU_CD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '�o�^�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskHyojunEntity.KEY_TOROKURENBAN
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* �@�\�@�@       ���t��_�W���}�X�^�̃f�[�^�������`�F�b�N���s���܂��B
    '* 
    '* ����           strColumnName As String
    '*                strValue As String
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"       '���̃��\�b�h��
        Dim objErrorStruct As UFErrorStruct                         '�G���[��`�\����

        Try

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()
                Case ABSfskHyojunEntity.JUMINCD                               ' �Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.GYOMUCD                               ' �Ɩ��R�[�h
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_GYOMUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.GYOMUNAISHU_CD                        ' �Ɩ�����ʃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_GYOMUNAISHU_CD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.TOROKURENBAN                          ' �o�^�A��
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_TOROKURENBAN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.STYMD                                  ' �J�n�N����
                    If (Not (strValue = String.Empty OrElse strValue = "00000000")) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_STYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABSfskHyojunEntity.EDYMD                                   ' �I���N����
                    If (Not (strValue = String.Empty OrElse strValue = "00000000" OrElse strValue = "99999999")) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_EDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABSfskHyojunEntity.RRKNO                                   ' ����ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_RRKNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKANAKATAGAKI                        ' ���t������t���K�i
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKANAKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKTSUSHO                              ' ���t�掁��_�ʏ�
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKTSUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKANATSUSHO                           ' ���t�掁��_�ʏ�_�t���K�i
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKANATSUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHIMEIYUSENKB                         ' ���t�掁��_�D��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHIMEIYUSENKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKEIJISHIMEI                            ' ���t�掁��_�O���l�p��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKEIJISHIMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKANJISHIMEI                           ' ���t�掁��_�O���l����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKANJISHIMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHINSEISHAMEI                          ' ���t��\���Җ�
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHINSEISHAMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD                     ' ���t��\���Ҋ֌W�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHINSEISHAKANKEICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHIKUCHOSONCD                          ' ���t��_�s�撬���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHIKUCHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKMACHIAZACD                             ' ���t��_�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKMACHIAZACD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKTODOFUKEN                               ' ���t��_�s���{��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKTODOFUKEN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKSHIKUCHOSON                             ' ���t��_�s��S������
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKSHIKUCHOSON)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKMACHIAZA                                ' ���t��_����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKMACHIAZA)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKBANCHICD1                                ' ���t��Ԓn�R�[�h�P
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKBANCHICD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKBANCHICD2                                 ' ���t��Ԓn�R�[�h�Q
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKBANCHICD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKBANCHICD3                                 ' ���t��Ԓn�R�[�h�R
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKBANCHICD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKATAGAKICD                                ' ���t������R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKATAGAKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKRENRAKUSAKIKB                             ' �A����敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKRENRAKUSAKIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKKBN                                       ' ���t��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKKBN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SFSKTOROKUYMD                                  ' ���t��o�^�N����
                    If (Not strValue = String.Empty) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SFSKTOROKUYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABSfskEntity.RESERVE                                               ' ���U�[�u
                    '�������Ȃ�
                Case ABSfskEntity.TANMATSUID                                            ' �[��ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SAKUJOFG                                        ' �폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.KOSHINCOUNTER                                   ' �X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SAKUSEINICHIJI                                  ' �쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.SAKUSEIUSER                                     ' �쐬���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.KOSHINNICHIJI                                   ' �X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskHyojunEntity.KOSHINUSER                                      ' �X�V���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKHB_RDBDATATYPE_KOSHINUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
            End Select

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException
        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException
        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �r�p�k���E�p�����[�^�R���N�V�����쐬
    '* 
    '* �\��           Private Function CreateSql_Param(ByVal strJuminCD As String, 
    '*                                                 ByVal strGyomuCD As String, 
    '*                                                 ByVal strGyomunaiSHUCD As String, 
    '*                                                 ByVal blnGyomunaiSHUCD As Boolean, 
    '*                                                 ByVal strTorokurenban As String, 
    '*                                                 ByVal blnSakujoFG As Boolean,
    '*                                                 ByVal cfUFParameterCollectionClass As UFParameterCollectionClass)
    '                                            As String
    '* 
    '* �@�\�@�@    �@�@�r�p�k���y�уp�����[�^�R���N�V�������쐬�������n���B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                blnGyomunaiSHUCD As Boolean   :�Ɩ�����ʃR�[�h�̗L���iTrue:�L��,False:�����j
    '*                strTorokurenban As String     :�o�^�ԍ�
    '*                blnSakujoFG As Boolean        :�폜�f�[�^�̗L��(True:�L��,False:����)
    '*                cfUFParameterCollectionClass As UFParameterCollectionClass  :�p�����[�^�R���N�V�����N���X
    '* 
    '* �߂�l         �r�p�k��(String)
    '*                �p�����[�^�R���N�V�����N���X(UFParameterCollectionClass)
    '************************************************************************************************
    Private Function CreateSql_Param(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                     ByVal strGyomunaiSHUCD As String, ByVal blnGyomunaiSHUCD As Boolean,
                                     ByVal strTorokurenban As String, ByVal blnSakujoFG As Boolean,
                                     ByVal cfUFParameterCollectionClass As UFParameterCollectionClass) As String
        Const THIS_METHOD_NAME As String = "CreateSql_Param"            '���̃��\�b�h��
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABSfskHyojunEntity.TABLE_NAME)

            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskHyojunEntity.TABLE_NAME, False)
            End If

            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABSfskEntity.JUMINCD)                 '�Z���R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABSfskEntity.KEY_JUMINCD)

            '�Ɩ��R�[�h
            If (Not (strGyomuCD = String.Empty)) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.GYOMUCD)
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUCD)
                strSQL.Append(",'00')")
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
                strSQL.Append(" ,'')")
            End If

            If (Not (strTorokurenban = String.Empty)) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.TOROKURENBAN)
                strSQL.Append(" = ")
                strSQL.Append(ABSfskEntity.KEY_TOROKURENBAN)
            End If

            If (Not (blnSakujoFG)) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.SAKUJOFG)            '�폜�t���O
                strSQL.Append(" <> ")
                strSQL.Append(SAKUJOFG_ON)
            End If

            '�\�[�g
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABSfskEntity.GYOMUCD)
            strSQL.Append(" DESC,")
            strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strSQL.Append(" DESC")

            '���������̃p�����[�^���쐬
            '�Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
            If (blnGyomunaiSHUCD) Then
                cfUFParameterClass.Value = strGyomunaiSHUCD
            Else
                cfUFParameterClass.Value = String.Empty
            End If
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �o�^�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_TOROKURENBAN
            cfUFParameterClass.Value = strTorokurenban

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return strSQL.ToString

    End Function

#End Region

End Class
