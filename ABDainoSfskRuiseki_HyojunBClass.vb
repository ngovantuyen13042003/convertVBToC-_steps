'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �`�a��[���t��ٓ��ݐ�_�W���}�X�^�c�`
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t��           2023/10/25
'*
'* �쐬��           ����@�[�l�Y
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'*  �C�������@ ����ԍ��@�@�C�����e
'* 2024/06/10  000001     �yAB-9902-1�z�s��Ή�
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
Imports System.Data
Imports System.Text

'************************************************************************************************
'*
'* ��[���t��ٓ��ݐ�_�W���}�X�^�擾�A�X�V���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABDainoSfskRuiseki_HyojunBClass

#Region "�����o�ϐ�"

    '�@�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABDainoSfskRuiseki_HyojunBClass"     ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h
    Private Const ZENGOKB_ZEN As String = "1"                                       ' �O��敪�@�O
    Private Const ZENGOKB_GO As String = "2"                                        ' �O��敪�@��
    Private Const SAKUJOFG_SAKUJO As String = "1"                                   ' �폜�t���O�@�폜
    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

    '�p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                                              ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                                        ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass                                ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                                              ' �q�c�a�N���X
    Private m_strInsertSQL As String                                                ' INSERT�pSQL
    Private m_cfErrorClass As UFErrorClass                                          ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                                            ' ���t�N���X
    Private m_csDataSchma As DataSet                                                ' �X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaHyojun As DataSet                                          ' �X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      ' SELECT�p�p�����[�^�R���N�V����
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      ' INSERT�p�p�����[�^�R���N�V����
    Private m_cUSSCityInfoClass As USSCityInfoClass                                 ' �s�������Ǘ��N���X

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '*                               ByVal cfConfigDataClass As UFConfigDataClass, 
    '*                               ByVal cfRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                cfRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
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
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' �p�����[�^�̃����o�ϐ�
        m_strInsertSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing

        ' AB��[���t��ݐσ}�X�^�̃X�L�[�}�擾
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(
            "SELECT * FROM " + ABDainoSfskRuisekiEntity.TABLE_NAME, ABDainoSfskRuisekiEntity.TABLE_NAME, False)

        ' AB��[���t��ݐ�_�W���}�X�^�̃X�L�[�}�擾
        m_csDataSchmaHyojun = m_cfRdbClass.GetTableSchemaNoRestriction(
            "SELECT * FROM " + ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "���\�b�h"

#Region "��[���t��ٓ��ݐσ}�X�^�ǉ�"
    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ٓ��ݐ�_�W���}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\           ��[���t��ٓ��ݐ�_�W���}�X�^�Ƀf�[�^��ǉ�
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertDainoSfskB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertDainoSfskB"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer                            ' �ǉ�����
        Dim strUpdateDateTime As String

        Try

            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing OrElse m_strInsertSQL = String.Empty OrElse
                    m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '�X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)  '�쐬����

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABDainoSfskRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId  ' �[���h�c
            csDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINCOUNTER) = Decimal.Zero                ' �X�V�J�E���^
            csDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEINICHIJI) = strUpdateDateTime          ' �쐬����
            csDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   ' �쐬���[�U�[
            csDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINNICHIJI) = strUpdateDateTime           ' �X�V����
            csDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId    ' �X�V���[�U�[

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoSfskRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:ExecuteSQL�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
                                                m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

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

        Return intInsCnt

    End Function
#End Region

#Region "SQL���쐬"
    '************************************************************************************************
    '* ���\�b�h��     SQL���̍쐬
    '* 
    '* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           INSERT��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)

        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim strInsertColumn As StringBuilder                 'INSERT�p�J������`
        Dim strInsertParam As StringBuilder                  'INSERT�p�p�����[�^��`


        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABDainoSfskRuisekiHyojunEntity.TABLE_NAME + " "
            strInsertColumn = New StringBuilder
            strInsertParam = New StringBuilder

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                strInsertColumn.Append(csDataColumn.ColumnName)
                strInsertColumn.Append(", ")

                strInsertParam.Append(ABDainoSfskRuisekiHyojunEntity.PARAM_PLACEHOLDER)
                strInsertParam.Append(csDataColumn.ColumnName)
                strInsertParam.Append(", ")

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABDainoSfskRuisekiHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)


            Next csDataColumn

            ' �Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL += "(" + strInsertColumn.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + strInsertParam.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")"

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

    End Sub
#End Region

#Region "��[���t��ݐ�_�W���f�[�^�쐬"
    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐ�_�W���f�[�^�쐬
    '* 
    '* �\��           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String, _
    '*                                                    ByVal strShoriNichiji As String) As Integer
    '* 
    '* �@�\           ��[���t��ݐσf�[�^���쐬����
    '* 
    '* ����           csDataRow As DataRow      : ��[���t��f�[�^
    '*                strShoriKB As String      : �����敪
    '*                strShoriNichiji As String : ��������
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String, ByVal strShoriNichiji As String) As Integer
        Dim intInsCnt As Integer
        Dim cSfskHyojunB As ABSfsk_HyojunBClass               '���t��c�`�N���X
        Dim csSfskHyojun As DataSet                           '���t��c�`�N���X

        Const THIS_METHOD_NAME As String = "CreateDainoSfskData"

        Try

            ' ���t��_�W���c�`�N���X�̃C���X�^���X��
            cSfskHyojunB = New ABSfsk_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            '���t��_�W���̎擾
            csSfskHyojun = cSfskHyojunB.GetSfskBHoshu(
                               csDataRow(ABSfskEntity.JUMINCD).ToString(),
                               csDataRow(ABSfskEntity.GYOMUCD).ToString(),
                               csDataRow(ABSfskEntity.GYOMUNAISHU_CD).ToString(),
                               csDataRow(ABSfskEntity.TOROKURENBAN).ToString())

            intInsCnt = CreateDainoSfskData(csDataRow, strShoriKB, csSfskHyojun.Tables(ABSfskHyojunEntity.TABLE_NAME).Rows(0), strShoriNichiji)

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

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐ�_�W���f�[�^�쐬
    '* 
    '* �\��           Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, _
    '*                                                    ByVal strShoriKB As String, _
    '*                                                    ByVal csABSfskHyojunDataRow As DataRow, _
    '*                                                    ByVal strShoriNichiji As String) As Integer
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐ�_�W���f�[�^���쐬����
    '* 
    '* ����           csDataRow As DataRow              : ��[���t��f�[�^
    '*                strShoriKB As String              : �����敪
    '*                csABSfskHyojunDataRow As DataRow  : AB���t��_�W���f�[�^�iDataRow�`���j
    '*                strShoriNichiji As String         : ��������
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function CreateDainoSfskData(ByVal csDataRow As DataRow, ByVal strShoriKB As String,
                                        ByVal csABSfskHyojunDataRow As DataRow, ByVal strShoriNichiji As String) As Integer
        Const THIS_METHOD_NAME As String = "CreateDainoSfskData"
        Dim csDataSet As DataSet
        Dim csDataSetHyojun As DataSet
        Dim csRuisekiDR As DataRow
        Dim csDataColumn As DataColumn
        'Dim strSystemDate As String                 ' �V�X�e�����t
        Dim intInsCnt As Integer
        Dim csOriginalDR As DataRow
        Dim csOriginalHyojunDR As DataRow
        Dim csDainoSfskRuisekiHyojunDR As DataRow
        Dim intUpdataCount_zen As Integer
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        Dim cuCityInfo As New USSCityInfoClass()            '�s�������Ǘ��N���X

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'strSystemDate = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            ' �X�L�[�}���擾
            csDataSet = m_csDataSchma.Clone
            csDataSetHyojun = m_csDataSchmaHyojun.Clone

            ' �X�V�p�f�[�^��DataRow���쐬
            csDainoSfskRuisekiHyojunDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow

            '***
            '* ��[���t��ݐ�_�W��(�O)�ҏW����
            '*

            If (strShoriKB <> ABConstClass.SFSK_ADD) Then

                ' ��[���t��ݐσf�[�^���쐬
                csOriginalDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow
                ' ��[���t��ݐ�_�W���f�[�^���쐬
                csOriginalHyojunDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow

                ' �����敪���ǉ��ȊO�̏ꍇ
                If (csDataRow.HasVersion(DataRowVersion.Original)) Then

                    ' �C���O��񂪎c���Ă���ꍇ�A��[���t��ݐσf�[�^���쐬
                    csOriginalDR = csDataSet.Tables(ABDainoSfskRuisekiEntity.TABLE_NAME).NewRow

                    For Each csDataColumn In csDataRow.Table.Columns
                        If (Not (csDataColumn.ColumnName = ABDainoEntity.RESERVE) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskDataEntity.SFSKDATAKB)) Then
                            csOriginalDR(csDataColumn.ColumnName) = csDataRow(csDataColumn.ColumnName, DataRowVersion.Original)
                        End If
                    Next

                    ' ��[���t��ݐ�_�W���f�[�^���쐬
                    csOriginalHyojunDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow

                    For Each csDataColumn In csABSfskHyojunDataRow.Table.Columns
                        If (Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SFSKBANCHICD1) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SFSKBANCHICD2) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SFSKBANCHICD3) AndAlso
                            Not (csDataColumn.ColumnName = ABSfskHyojunEntity.SFSKKATAGAKICD)) Then

                            csOriginalHyojunDR(csDataColumn.ColumnName) = csABSfskHyojunDataRow(csDataColumn.ColumnName, DataRowVersion.Original)
                        End If
                    Next

                    '(�O)�f�[�^�̃Z�b�g
                    csOriginalHyojunDR = SetDainoSfskRuisekiHyojunData(csOriginalDR, csOriginalHyojunDR, csDainoSfskRuisekiHyojunDR)

                    '���ʍ��ڂ̃Z�b�g
                    csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI) = strShoriNichiji                 '��������
                    csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.SHORIKB) = strShoriKB                           '�����敪
                    csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.ZENGOKB) = ZENGOKB_ZEN                          '�O��敪

                    '�폜�t���O�̐ݒ�
                    csOriginalHyojunDR(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = csDataRow(ABSfskEntity.SAKUJOFG, DataRowVersion.Original)

                    ' �f�[�^�Z�b�g�ɏC���O����ǉ�
                    csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows.Add(csOriginalHyojunDR)

                    ' ��[���t��ݐ�(�O)�}�X�^�ǉ�����
                    intUpdataCount_zen = Me.InsertDainoSfskB(csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows(0))

                    ' �X�V�������P���ȊO�̏ꍇ�A�G���[�𔭐�������
                    If (Not (intUpdataCount_zen = 1)) Then
                        m_cfErrorClass = New UFErrorClass(m_cfControlData.m_strBusinessId)
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F��[���t��ݐρj
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��[���t��ݐ�_�W��", objErrorStruct.m_strErrorCode)
                    End If

                    ' �f�[�^�Z�b�g�̃N���A
                    csDataSetHyojun.Clear()
                Else

                End If
            Else

            End If

            '***
            '* ��[���t��ݐ�_�W��(��)�ҏW�����@�ǉ��̏ꍇ��������
            '*
            ' ��[���t��ݐ�_�W���f�[�^���쐬
            csRuisekiDR = csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).NewRow

            '���ʍ��ڂ̃Z�b�g
            csRuisekiDR = SetDainoSfskRuisekiHyojunData(csDataRow, csABSfskHyojunDataRow, csDainoSfskRuisekiHyojunDR)

            ' �f�[�^�Z�b�g�@�@
            csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI) = strShoriNichiji            ' ��������
            csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SHORIKB) = strShoriKB                      ' �����敪
            csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.ZENGOKB) = ZENGOKB_GO                      ' �O��敪
            '�폜�t���O
            If (strShoriKB = ABConstClass.SFSK_DELETE) Then
                '�폜�̏ꍇ��"1"���Z�b�g
                csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_SAKUJO
            Else
                '����ȊO�̏ꍇ�͑��t��̒l�����̂܂܃Z�b�g
                csRuisekiDR(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = csDataRow(ABSfskEntity.SAKUJOFG)
            End If

            csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows.Add(csRuisekiDR)

            '***
            '* ��[���t��ݐ�_�W��(��)�}�X�^�ǉ�����
            '*
            intInsCnt = InsertDainoSfskB(csDataSetHyojun.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME).Rows(0))

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

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐ�_�W���f�[�^�ҏW����
    '* 
    '* �\��           Private Function SetDainoSfskRuisekiHyojunData(ByVal csSfskDataRow As DataRow,
    '*                                                               ByVal csSfskHyojunDataRow As DataRow,
    '*                                                               ByVal csReturnDataRow As DataRow) As DataRow
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐ�_�W���f�[�^��ҏW����
    '* 
    '* ����           csSfskDataRow As DataRow            : ���t��f�[�^
    '*                csSfskHyojunDataRow As DataRow      : ���t��_�W���f�[�^
    '*                csReturnDataRow                     : �߂�l
    '* 
    '* �߂�l         DataRow : �ҏW�����f�[�^
    '************************************************************************************************
    Private Function SetDainoSfskRuisekiHyojunData(ByVal csSfskDataRow As DataRow,
                                                   ByVal csSfskHyojunDataRow As DataRow,
                                                   ByVal csReturnDataRow As DataRow) As DataRow
        Const THIS_METHOD_NAME As String = "SetDainoSfskRuisekiHyojunData"

        '�s�������Ǘ��N���X�̐ݒ�
        m_cUSSCityInfoClass = New USSCityInfoClass
        m_cUSSCityInfoClass.GetCityInfo(m_cfControlData)

        Try
            '���ʍ��ځ@�����������A�����敪�A�O��敪�A�폜�t���O�͌ďo�����ŃZ�b�g����
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.JUMINCD) = csSfskDataRow(ABSfskEntity.JUMINCD)                                           '�Z���R�[�h
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SHICHOSONCD) = m_cUSSCityInfoClass.p_strShichosonCD(0)                                   '�s�����R�[�h
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KYUSHICHOSONCD) = m_cUSSCityInfoClass.p_strShichosonCD(0)                                '���s�����R�[�h

            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.GYOMUCD) = csSfskDataRow(ABSfskEntity.GYOMUCD)                                           '�Ɩ��R�[�h
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.GYOMUNAISHU_CD) = csSfskDataRow(ABSfskEntity.GYOMUNAISHU_CD)                             '�Ɩ�����ʃR�[�h
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.TOROKURENBAN) = csSfskDataRow(ABSfskEntity.TOROKURENBAN)                                 '�o�^�A��
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.STYMD) = csSfskDataRow(ABSfskEntity.STYMD)                                               '�J�n�N����
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.EDYMD) = csSfskDataRow(ABSfskEntity.EDYMD)                                               '�I���N����
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RRKNO) = csSfskDataRow(ABSfskEntity.RRKNO)                                               '����ԍ�

            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKANAKATAGAKI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKANAKATAGAKI)             '���t������t���K�i
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKTSUSHO) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKTSUSHO)                         '���t�掁��_�ʏ�
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKANATSUSHO) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKANATSUSHO)                 '���t�掁��_�ʏ�_�t���K�i
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHIMEIYUSENKB) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHIMEIYUSENKB)           '���t�掁��_�D��敪
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKEIJISHIMEI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKEIJISHIMEI)                 '���t�掁��_�O���l�p��
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKANJISHIMEI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKANJISHIMEI)               '���t�掁��_�O���l����
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHINSEISHAMEI) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHINSEISHAMEI)           '���t��\���Җ�
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHINSEISHAKANKEICD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD) '���t��\���Ҋ֌W�R�[�h
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHIKUCHOSONCD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHIKUCHOSONCD)           '���t��_�s�撬���R�[�h
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKMACHIAZACD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKMACHIAZACD)                 '���t��_�����R�[�h
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKTODOFUKEN) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKTODOFUKEN)                   '���t��_�s���{��
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKSHIKUCHOSON) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKSHIKUCHOSON)               '���t��_�s��S������
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKMACHIAZA) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKMACHIAZA)                     '���t��_����
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKRENRAKUSAKIKB) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKRENRAKUSAKIKB)           '�A����敪
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKKBN) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKKBN)                               '���t��敪
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SFSKTOROKUYMD) = csSfskHyojunDataRow(ABSfskHyojunEntity.SFSKTOROKUYMD)                   '���t��o�^�N����
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE1) = String.Empty                                                                 '���U�[�u�P
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE2) = String.Empty                                                                 '���U�[�u�Q
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE3) = String.Empty                                                                 '���U�[�u�R
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE4) = String.Empty                                                                 '���U�[�u�S
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.RESERVE5) = String.Empty                                                                 '���U�[�u�T
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId                                              ' �[���h�c
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUJOFG) = csSfskHyojunDataRow(ABSfskHyojunEntity.SAKUJOFG)                             ' �폜�t���O
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINCOUNTER) = Decimal.Zero                                                            ' �X�V�J�E���^
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEINICHIJI) = csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI)           ' �쐬����
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                                               ' �쐬���[�U�[
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINNICHIJI) = csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI)            ' �X�V����
            csReturnDataRow(ABDainoSfskRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                                ' �X�V���[�U�[

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

        Return csReturnDataRow

    End Function

#End Region

#Region "��[���t��ݐ�_�W���f�[�^���o"
    '************************************************************************************************
    '* ���\�b�h��     ��[���t��ݐ�_�W���f�[�^���o
    '* 
    '* �\��           PPublic Function GetABDainoSfskRuisekiData(ByVal strJuminCD As String,
    '*                                                           ByVal strGyomuCD As String,
    '*                                                           ByVal strGyomuNaiShubetsuCD As String,
    '*                                                           ByVal intTorokuRenban As Integer,
    '*                                                           ByVal strShoriKB As String) As DataRow()
    '* 
    '* �@�\�@�@    �@ ��[���t��ݐσ}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD             : �Z���R�[�h 
    '*                strGyomuCD             : �Ɩ��R�[�h
    '*                strGyomuNaiShubetsuCD  : �Ɩ�����ʃR�[�h
    '*                intTorokuRenban        : �o�^�ԍ�
    '*                strShoriKB             : �����敪�@"D"�F��[�A"S"�F���t
    '* 
    '* �߂�l         DataSet : �擾������[���t��ݐσ}�X�^�̊Y���f�[�^(DataRow())
    '************************************************************************************************
    Public Function GetABDainoSfskRuisekiData(ByVal strJuminCD As String,
                                              ByVal strGyomuCD As String,
                                              ByVal strGyomuNaiShubetsuCD As String,
                                              ByVal intTorokuRenban As Integer,
                                              ByVal strShoriKB As String) As DataTable

        Const THIS_METHOD_NAME As String = "GetABDainoSfskRuisekiData"
        Dim csDainoSfskRuisekiHyojunEntity As DataSet
        Dim csReturnDataRows As DataRow()
        Dim csReturnDatatable As DataTable
        Dim strSQL As New StringBuilder

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABDainoSfskRuisekiHyojunEntity.TABLE_NAME)
            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            strSQL.Append(Me.CreateWhere(strJuminCD, strGyomuCD, strGyomuNaiShubetsuCD, intTorokuRenban, strShoriKB, THIS_METHOD_NAME))

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
                                            strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDainoSfskRuisekiHyojunEntity = m_csDataSchma.Clone()
            csDainoSfskRuisekiHyojunEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csDainoSfskRuisekiHyojunEntity,
                                                    ABDainoSfskRuisekiHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '�߂�l�p�Ƀf�[�^���i�[
            csReturnDatatable = csDainoSfskRuisekiHyojunEntity.Tables(ABDainoSfskRuisekiHyojunEntity.TABLE_NAME)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csReturnDatatable

    End Function

    '************************************************************************************************
    '* ���\�b�h��     SELECT��̍쐬
    '* 
    '* �\��           Private Sub CreateSelect() As String
    '* 
    '* �@�\           SELECT��𐶐�����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         String    :   SELECT��
    '************************************************************************************************
    Private Function CreateSelect() As String
        Const THIS_METHOD_NAME As String = "CreateSelect"
        Dim strSELECT As New StringBuilder

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT��̍쐬
            strSELECT.AppendFormat("SELECT {0}", ABDainoSfskRuisekiHyojunEntity.JUMINCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SHICHOSONCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KYUSHICHOSONCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SHORINICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SHORIKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.ZENGOKB)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.GYOMUCD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.GYOMUNAISHU_CD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.TOROKURENBAN)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.STYMD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.EDYMD)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.RRKNO)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SFSKKBN)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.RESERVE1)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.RESERVE2)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.TANMATSUID)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SAKUJOFG)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KOSHINCOUNTER)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SAKUSEINICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.SAKUSEIUSER)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KOSHINNICHIJI)
            strSELECT.AppendFormat(", {0}", ABDainoSfskRuisekiHyojunEntity.KOSHINUSER)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return strSELECT.ToString

    End Function

    '************************************************************************************************
    '* ���\�b�h��     WHERE���̍쐬
    '* 
    '* �\��           Private Function CreateWhere(ByVal strJuminCD As String,
    '*                                             ByVal strGyomuCD As String,
    '*                                             ByVal strGyomuNaiShubetsuCD As String,
    '*                                             ByVal intTorokuRenban As Integer,
    '*                                             ByVal strShoriKB As String,
    '*                                             ByVal strMethodName As String) As String
    '* 
    '* �@�\�@�@    �@ WHERE�����쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           strJuminCD             : �Z���R�[�h 
    '*                strGyomuCD             : �Ɩ��R�[�h
    '*                strGyomuNaiShubetsuCD  : �Ɩ�����ʃR�[�h
    '*                strShoriKB             : �����敪�@"D"�F��[�A"S"�F���t
    '*                strMethodName          : �ďo�����֐���
    '*
    '* �߂�l         String    :   WHERE��
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String,
                                 ByVal strGyomuCD As String,
                                 ByVal strGyomuNaiShubetsuCD As String,
                                 ByVal intTorokuRenban As Integer,
                                 ByVal strShoriKB As String,
                                 ByVal strMethodName As String) As String

        Const THIS_METHOD_NAME As String = "CreateWhere"
        Const GET_MAX_TOROKURENBAN As String = "GetMaxTorokuRenban"

        Dim strWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT�p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' WHERE��̍쐬
            strWHERE = New StringBuilder(256)

            ' �Z���R�[�h
            strWHERE.AppendFormat("WHERE {0} = {1}", ABDainoSfskRuisekiEntity.JUMINCD, ABDainoSfskRuisekiEntity.KEY_JUMINCD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �Ɩ��R�[�h
            strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.GYOMUCD, ABDainoSfskRuisekiEntity.KEY_GYOMUCD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �Ɩ�����ʃR�[�h
            strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD, ABDainoSfskRuisekiEntity.KEY_GYOMUNAISHU_CD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomuNaiShubetsuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '�o�^�A��
            strWHERE.AppendFormat(" AND {0} = {1}", ABDainoSfskRuisekiEntity.TOROKURENBAN, ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoSfskRuisekiEntity.KEY_TOROKURENBAN
            cfUFParameterClass.Value = intTorokuRenban
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '�����敪
            '���t
            strWHERE.AppendFormat(" AND {0} IN ('{1}','{2}','{3}')", ABDainoSfskRuisekiEntity.SHORIKB,
                                         ABConstClass.SFSK_ADD, ABConstClass.SFSK_SHUSEI, ABConstClass.SFSK_DELETE)

            '�O��敪
            strWHERE.AppendFormat(" AND {0} = '{1}'", ABDainoSfskRuisekiEntity.ZENGOKB, ZENGOKB_GO)

            '����ԍ��@�~�ԂŃ\�[�g�@
            If (strMethodName <> GET_MAX_TOROKURENBAN) Then
                strWHERE.AppendFormat(" ORDER BY {0} DESC", ABDainoSfskRuisekiEntity.RRKNO)
            End If


            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return strWHERE.ToString

    End Function
#End Region

#End Region

End Class
