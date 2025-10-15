'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�x���[�u�c�`
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2023/10/13�@�����@���]
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2023/10/13             �yAB-0880-1�z�l������ڍ׊Ǘ����ڒǉ�
'*
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

Public Class ABShienSochiBClass
#Region "�����o�ϐ�"
    ' �p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X
    Private m_strInsertSQL As String                        ' INSERT�pSQL
    Private m_strUpdateSQL As String                        ' UPDATE�pSQL
    Private m_strDelRonriSQL As String                      ' �_���폜�pSQL
    Private m_strDelButuriSQL As String                     ' �����폜�pSQL
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT�p�p�����[�^�R���N�V����
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    '�_���폜�p�p�����[�^�R���N�V����
    Private m_cfDelButuriUFParameterCollectionClass As UFParameterCollectionClass   '�����폜�p�p�����[�^�R���N�V����
    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_strUpdateDatetime As String                   ' �X�V����

    Public m_blnBatch As Boolean = False               '�o�b�`�t���O
    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABShienSochiBClass"                   ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                   ' �Ɩ��R�[�h

    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const SAISHINFG_ON As String = "1"
    Private Const SAISHINFG_OFF As String = "0"
    Private Const KARISHIENSOCHI As String = "2"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero

    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

    Private Const ERR_SHIENSOCHI As String = "�x���[�u�Ǘ��ԍ�"

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass, 
    '* �@�@                          ByVal cfRdbClass As UFRdbClass)
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
        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' �p�����[�^�̃����o�ϐ�
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_strDelButuriSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "���\�b�h"
#Region "�x���[�u���o�@[GetShienSochi]"
    '************************************************************************************************
    '* ���\�b�h��    �x���[�u���o
    '* 
    '* �\��          Public Function GetShienSochi As DataSet
    '* 
    '* �@�\�@�@    �@�x���[�u���Y���f�[�^���擾����
    '* 
    '* ����          strShienSochiKanriNo : �x���[�u�Ǘ��ԍ� 
    '* 
    '* �߂�l        DataSet : �擾�����x���[�u�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetShienSochi(ByVal strShienSochiKanriNo As String) As DataSet

        Return Me.GetShienSochi(strShienSochiKanriNo, True, False)

    End Function

    '************************************************************************************************
    '* ���\�b�h��    �x���[�u���o
    '* 
    '* �\��          Public Function GetShienSochi As DataSet
    '* 
    '* �@�\�@�@    �@�x���[�u���Y���f�[�^���擾����
    '* 
    '* ����          strShienSochiKanriNo : �x���[�u�Ǘ��ԍ�  
    '*               blnSaishin           : �ŐV�t���O
    '*               blnSakujoFG          : �폜�t���O
    '* 
    '* �߂�l        DataSet : �擾�����x���[�u�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetShienSochi(ByVal strShienSochiKanriNo As String,
                                                ByVal blnSaishin As Boolean,
                                                ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochi"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csShienSochiEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            ' �x���[�u�Ǘ��ԍ����w�肳��Ă��Ȃ��Ƃ��G���[
            If (IsNothing(strShienSochiKanriNo) OrElse (strShienSochiKanriNo).Trim.RLength = 0) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHI, objErrorStruct.m_strErrorCode)
            Else
                '�����Ȃ�
            End If

            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiEntity.TABLE_NAME)

            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            strSQL.Append(Me.CreateWhere(strShienSochiKanriNo, blnSaishin, blnSakujoFG))

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csShienSochiEntity = m_csDataSchma.Clone()
            csShienSochiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csShienSochiEntity, ABShienSochiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csShienSochiEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��    �x���[�u���o
    '* 
    '* �\��          Public Function GetShienSochi As DataSet
    '* 
    '* �@�\�@�@    �@�x���[�u���Y���f�[�^���擾����
    '* 
    '* ����          strShienSochiKanriNo : �x���[�u�Ǘ��ԍ��̔z��       
    '* 
    '* �߂�l        DataSet : �擾�����x���[�u�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetShienSochi(ByVal strShienSochiKanriNo() As String) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochi"
        Dim csShienSochiEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfParameter As UFParameterClass
        Dim strParameterName As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiEntity.TABLE_NAME)

            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            If (strShienSochiKanriNo.Length = 0) Then
                csShienSochiEntity = m_csDataSchma.Clone()
            Else
                With strSQL
                    .Append(" WHERE ")
                    .Append(ABShienSochiEntity.SHIENSOCHIKANRINO)
                    .Append(" IN (")

                    For i As Integer = 0 To strShienSochiKanriNo.Length - 1
                        ' -----------------------------------------------------------------------------
                        ' �x���[�u�Ǘ��ԍ�
                        strParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO + i.ToString

                        If (i > 0) Then
                            .AppendFormat(", {0}", strParameterName)
                        Else
                            .Append(strParameterName)
                        End If

                        cfParameter = New UFParameterClass
                        cfParameter.ParameterName = strParameterName
                        cfParameter.Value = strShienSochiKanriNo(i)
                        m_cfSelectUFParameterCollectionClass.Add(cfParameter)
                        ' -----------------------------------------------------------------------------
                    Next i

                    .Append(")")
                    .Append(" AND ")
                    .Append(ABShienSochiEntity.SAISHINFG)
                    .Append(" = '1'")

                End With

                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + Me.GetType.Name + "�z" +
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                            "�y���s���\�b�h��:GetDataSet�z" +
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

                ' SQL�̎��s DataSet�̎擾
                csShienSochiEntity = m_csDataSchma.Clone()
                csShienSochiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csShienSochiEntity, ABShienSochiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csShienSochiEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     SELECT��̍쐬
    '* 
    '* �\��           Private Sub CreateSelect() As String
    '* 
    '* �@�\�@�@    �@ SELECT��𐶐�����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         String    :   SELECT��
    '************************************************************************************************
    Private Function CreateSelect() As String
        Const THIS_METHOD_NAME As String = "CreateSelect"
        Dim csSELECT As New StringBuilder

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT��̍쐬
            csSELECT.AppendFormat("SELECT {0}", ABShienSochiEntity.SHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENSOCHIKANRINO)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RIREKINO)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SAISHINFG)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.UKETSUKEKBN)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.UKETSUKEYMD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENSOCHIKBN)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KARISHIENSOCHIUMU)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KARISHIENSOCHISTYMD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KARISHIENSOCHIEDYMD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENSOCHISTYMD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENSOCHIEDYMD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SHIENFUYOKAKUNINRENRAKUYMD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TOSHOUKETSUKESHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TOSHOUKETSUKESHICHOSON)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.GYOMUCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.BIKO)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD1)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON1)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD1)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD2)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON2)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD2)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD3)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON3)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD3)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD4)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON4)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD4)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD5)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON5)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD5)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD6)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON6)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD6)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD7)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON7)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD7)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD8)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON8)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD8)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD9)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON9)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD9)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD10)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON10)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD10)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD11)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON11)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD11)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD12)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON12)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD12)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD13)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON13)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD13)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD14)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON14)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD14)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD15)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON15)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD15)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD16)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON16)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD16)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD17)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON17)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD17)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD18)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON18)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD18)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD19)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON19)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD19)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSONCD20)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOSHICHOSON20)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TENSOYMD20)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KANRIKB)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SETAIYOKUSHIKB)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.NYURYOKUBASHOCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.NYURYOKUBASHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABShienSochiEntity.KOSHINUSER)

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

        Return csSELECT.ToString

    End Function
    '************************************************************************************************
    '* ���\�b�h��   WHERE���̍쐬
    '* 
    '* �\��         Private Sub CreateWhere
    '* 
    '* �@�\         WHERE�����쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����         strShienSochiKanriNo: �x���[�u�Ǘ��ԍ� 
    '*              blnSaishin          : �ŐV�t���O
    '*              blnSakujoFG         : �폜�t���O
    '* 
    '* �߂�l       �Ȃ�
    '************************************************************************************************
    Private Function CreateWhere(ByVal strShienSochiKanriNo As String,
                                 ByVal blnSaishin As Boolean,
                                 ByVal blnSakujoFG As Boolean) As String
        Const THIS_METHOD_NAME As String = "CreateWhere"
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT�p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' WHERE��̍쐬
            csWHERE = New StringBuilder(256)

            ' �x���[�u�Ǘ��ԍ�
            csWHERE.AppendFormat("WHERE {0} = {1}", ABShienSochiEntity.SHIENSOCHIKANRINO, ABShienSochiEntity.KEY_SHIENSOCHIKANRINO)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO
            cfUFParameterClass.Value = strShienSochiKanriNo
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '�ŐV�t���O
            If (blnSaishin) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABShienSochiEntity.SAISHINFG, SAISHINFG_ON)
            Else
                '�����Ȃ�
            End If

            ' �폜�t���O
            If blnSakujoFG = False Then
                csWHERE.AppendFormat(" AND {0} <> '{1}'", ABShienSochiEntity.SAKUJOFG, SAKUJOFG_ON)
            Else
                '�����Ȃ�
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

        Return csWHERE.ToString

    End Function
#End Region

#Region "�x���[�u�ǉ��@[InsertShienSochi]"
    '************************************************************************************************
    '* ���\�b�h��     �x���[�u�ǉ�
    '* 
    '* �\��           Public Function InsertShienSochi((ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�x���[�u�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertShienSochi(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertShienSochi"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer                            '�ǉ�����

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing OrElse m_strInsertSQL = String.Empty OrElse
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateInsertSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABShienSochiEntity.TANMATSUID) = m_cfControlData.m_strClientId     '�[���h�c
            csDataRow(ABShienSochiEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              '�X�V�J�E���^
            csDataRow(ABShienSochiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      '�쐬���[�U�[
            csDataRow(ABShienSochiEntity.SAKUSEINICHIJI) = m_strUpdateDatetime           '�쐬����
            csDataRow(ABShienSochiEntity.KOSHINUSER) = m_cfControlData.m_strUserId       '�X�V���[�U�[
            csDataRow(ABShienSochiEntity.KOSHINNICHIJI) = m_strUpdateDatetime             '�X�V����

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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

        Return intInsCnt

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
        Dim csInsertColumn As StringBuilder                 'INSERT�p�J������`
        Dim csInsertParam As StringBuilder                  'INSERT�p�p�����[�^��`
        Dim cfUFParameterClass As UFParameterClass
        Dim strParamName As String


        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL���̍쐬
            csInsertColumn = New StringBuilder
            csInsertParam = New StringBuilder

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass
                strParamName = String.Format("{0}{1}", ABShienSochiEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL���̍쐬
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '�Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})",
                                           ABShienSochiEntity.TABLE_NAME,
                                           csInsertColumn.ToString.TrimEnd(",".ToCharArray),
                                           csInsertParam.ToString.TrimEnd(",".ToCharArray))

            ' �f�o�b�O�I�����O�o��
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

    End Sub

#End Region

#Region "�x���[�u�X�V�@[UpdateShienSochi]"
    '************************************************************************************************
    '* ���\�b�h��     �x���[�u�X�V
    '* 
    '* �\��           Public Function UpdateShienSochi(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �x���[�u�̃f�[�^���X�V����
    '* 
    '* ����           csDataRow As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateShienSochi(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateShienSochi"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        Dim intUpdCnt As Integer                            '�X�V����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing OrElse m_strUpdateSQL = String.Empty OrElse
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateUpdateSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABShienSochiEntity.SAISHINFG) = SAISHINFG_OFF                                                    '�ŐV�t���O
            csDataRow(ABShienSochiEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABShienSochiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABShienSochiEntity.KOSHINCOUNTER)) + 1        '�X�V�J�E���^
            csDataRow(ABShienSochiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            csDataRow(ABShienSochiEntity.KOSHINNICHIJI) = m_strUpdateDatetime

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABShienSochiEntity.PREFIX_KEY.RLength) = ABShienSochiEntity.PREFIX_KEY) Then
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()

                    '�L�[���ڈȊO�͕ҏW���e�擾
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                         = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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

        Return intUpdCnt

    End Function

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
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE��`
        Dim csUpdateParam As StringBuilder                  'UPDATE�pSQL��`


        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABShienSochiEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABShienSochiEntity.SHIENSOCHIKANRINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiEntity.KEY_SHIENSOCHIKANRINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiEntity.KEY_KOSHINCOUNTER)

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�x���[�u�Ǘ��ԍ��E����ԍ��E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If Not (csDataColumn.ColumnName = ABShienSochiEntity.SHIENSOCHIKANRINO) AndAlso
                   Not (csDataColumn.ColumnName = ABShienSochiEntity.RIREKINO) AndAlso
                   Not (csDataColumn.ColumnName = ABShienSochiEntity.SAKUSEIUSER) AndAlso
                   Not (csDataColumn.ColumnName = ABShienSochiEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL���̍쐬
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABShienSochiEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                Else
                    '�����Ȃ�
                End If

            Next csDataColumn

            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(",".ToCharArray())

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += csWhere.ToString

            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_RIREKINO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O�I�����O�o��
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

    End Sub

#End Region

#Region "�x���[�u�폜�@[DeleteShienSochi]"
    '************************************************************************************************
    '* ���\�b�h��     �x���[�u�폜
    '* 
    '* �\��           Public Function DeleteShienSochi(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�x���[�u�̃f�[�^��_���폜����
    '* 
    '* ����           csDataRow As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteShienSochi(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteShienSochi"
        Dim cfParam As UFParameterClass  '�p�����[�^�N���X
        Dim intDelCnt As Integer        '�폜����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDelRonriSQL Is Nothing OrElse m_strDelRonriSQL = String.Empty OrElse
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                Call CreateDeleteRonriSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABShienSochiEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABShienSochiEntity.SAKUJOFG) = SAKUJOFG_ON                                                       '�폜�t���O
            csDataRow(ABShienSochiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABShienSochiEntity.KOSHINCOUNTER)) + 1        '�X�V�J�E���^
            csDataRow(ABShienSochiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABShienSochiEntity.KOSHINNICHIJI))

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABShienSochiEntity.PREFIX_KEY.RLength) = ABShienSochiEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                                 csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PREFIX_KEY.RLength),
                                           DataRowVersion.Original).ToString()
                    '�L�[���ڈȊO�͕ҏW���e��ݒ�
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �x���[�u�����폜
    '* 
    '* �\��           Public Function DeleteShienSochi(ByVal csDataRow As DataRow, _
    '*                                               ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@�@�x���[�u�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csDataRow As DataRow : �폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteShienSochi(ByVal csDataRow As DataRow,
                                             ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteShienSochi"
        Dim objErrorStruct As UFErrorStruct '�G���[��`�\����
        Dim cfParam As UFParameterClass     '�p�����[�^�N���X
        Dim intDelCnt As Integer            '�폜����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �폜�敪�̃`�F�b�N���s��
            If Not (strSakujoKB = "D") Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            Else
                '�����Ȃ�
            End If

            ' �폜�p�̃p�����[�^�tDELETE��������ƃp�����[�^�R���N�V�������쐬����
            If (m_strDelButuriSQL Is Nothing OrElse m_strDelButuriSQL = String.Empty OrElse
                    IsNothing(m_cfDelButuriUFParameterCollectionClass)) Then
                Call CreateDeleteButsuriSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABShienSochiEntity.PREFIX_KEY.RLength) = ABShienSochiEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

                    '�L�[���ڈȊO�̎擾�Ȃ�
                Else
                    '�����Ȃ�
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:ExecuteSQL�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "�z")
            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass)

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

        Return intDelCnt

    End Function


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
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE��`
        Dim csDelRonriParam As StringBuilder                '�_���폜�p�����[�^��`

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABShienSochiEntity.SHIENSOCHIKANRINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiEntity.KEY_SHIENSOCHIKANRINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiEntity.KEY_KOSHINCOUNTER)


            ' �_��DELETE SQL���̍쐬
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABShienSochiEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABShienSochiEntity.NYURYOKUBASHOCD)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiEntity.PARAM_NYURYOKUBASHOCD)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiEntity.NYURYOKUBASHO)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiEntity.PARAM_NYURYOKUBASHO)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where���̒ǉ�
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_NYURYOKUBASHOCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_NYURYOKUBASHO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_RIREKINO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O�I�����O�o��
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

    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �����폜�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateButsuriSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           ����DELETE�p��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateDeleteButsuriSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE��`

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABShienSochiEntity.SHIENSOCHIKANRINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiEntity.KEY_SHIENSOCHIKANRINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiEntity.KEY_KOSHINCOUNTER)

            ' ����DELETE SQL���̍쐬
            m_strDelButuriSQL = "DELETE FROM " + ABShienSochiEntity.TABLE_NAME + csWhere.ToString

            ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_RIREKINO
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_KOSHINCOUNTER
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O�I�����O�o��
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

    End Sub
#End Region

#Region "�x���[�u���擾"
    '************************************************************************************************
    '* ���\�b�h��     �x���[�u���擾
    '* 
    '* �\��           Public Function GetShienSochiJoho(ByVal strJumincd As String) As DataSet
    '* 
    '* �@�\�@�@    �@ �x���[�u�Ǝx���[�u�Ώۂ���f�[�^���擾
    '* 
    '* ����           strJumincd�F�Z���R�[�h
    '* 
    '* �߂�l         �擾�����f�[�^�FDataSet
    '************************************************************************************************
    Public Function GetShienSochiJoho(ByVal strJumincd As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetShienSochiJoho"
        Dim csShienSochiDS As DataSet                                        ' �x���[�u�f�[�^
        Dim strSQL As StringBuilder                                         ' SQL��SELECT��
        Dim cfUFParameterClass As UFParameterClass                          ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            strSQL = New StringBuilder

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            strSQL.Append("SELECT ")
            strSQL.Append(ABShienSochiEntity.TABLE_NAME)
            strSQL.Append(".* ")
            strSQL.Append(" FROM ")
            strSQL.Append(ABShienSochiEntity.TABLE_NAME)
            strSQL.Append(" INNER JOIN ")
            strSQL.Append(ABShienSochiTaishoEntity.TABLE_NAME)
            strSQL.Append(" ON ")
            strSQL.AppendFormat("{0}.{1}", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SHIENSOCHIKANRINO)
            strSQL.Append(" = ")
            strSQL.AppendFormat("{0}.{1}", ABShienSochiTaishoEntity.TABLE_NAME, ABShienSochiTaishoEntity.SHIENSOCHIKANRINO)
            strSQL.Append(" AND ")
            strSQL.Append(ABShienSochiTaishoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABShienSochiTaishoEntity.PARAM_JUMINCD)

            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiTaishoEntity.PARAM_JUMINCD
            cfUFParameterClass.Value = strJumincd
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' WHERE��̍쐬
            strSQL.Append(" WHERE ")
            strSQL.AppendFormat("{0} = '{1}'", ABShienSochiEntity.SAISHINFG, SAISHINFG_ON)
            strSQL.AppendFormat(" AND {0}.{1} <> '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SAKUJOFG, SAKUJOFG_ON)
            strSQL.Append(" ORDER BY ")
            strSQL.AppendFormat("{0}.{1}", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SHIENSOCHIKANRINO)
            strSQL.Append(" DESC")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csShiensochids = m_cfRdbClass.GetDataSet(strSQL.ToString, ABShienSochiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return csShienSochiDS

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���x���[�u�`�F�b�N
    '* 
    '* �\��           Public Function CheckKariShienSochi(ByVal strSystemYMD As String) As DataSet
    '* 
    '* �@�\�@�@    �@ �x���[�u�Ǝx���[�u�Ώۂ���f�[�^���擾
    '* 
    '* ����           strSystemYMD�F�V�X�e�����t
    '* 
    '* �߂�l         �擾�����f�[�^�FDataSet
    '************************************************************************************************
    Public Function CheckKariShienSochi(ByVal strSystemYMD As String) As DataSet
        Const THIS_METHOD_NAME As String = "CheckKariShienSochi"
        Dim cABKanriJohoB As ABAtenaKanriJohoBClass         '�����Ǘ����N���X
        Dim csABKanriJohoDS As DataSet
        Dim intNisu As Integer
        Dim csShienSochiDS As DataSet                                        ' �x���[�u�f�[�^
        Dim strSQL As StringBuilder                                         ' SQL��SELECT��
        Dim cfUFParameterClass As UFParameterClass                          ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' �p�����[�^�R���N�V�����N���X
        Dim cfDate As UFDateClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �����Ǘ����N���X�̃C���X�^���X��
            cABKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            ' �Ǘ����擾���\�b�h���s(�l����@�\(20)�A���x���x������(84))
            csABKanriJohoDS = cABKanriJohoB.GetKanriJohoHoshu("20", "84")

            ' �Ǘ����`�F�b�N
            If (Not (csABKanriJohoDS Is Nothing) AndAlso csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) AndAlso
                (UFStringClass.CheckNumber(csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0)(ABAtenaKanriJohoEntity.PARAMETER).ToString.Trim)) Then
                intNisu = CType(csABKanriJohoDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0)(ABAtenaKanriJohoEntity.PARAMETER).ToString, Integer)
            Else
                intNisu = 30
            End If
            cfDate = New UFDateClass(m_cfConfigDataClass, UFDateSeparator.None, UFDateFillType.Zero)
            intNisu = intNisu * -1
            cfDate.p_strDateValue = strSystemYMD
            cfDate.p_strDateValue = cfDate.AddDay(intNisu)

            strSQL = New StringBuilder

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            strSQL.Append("SELECT DISTINCT ")
            strSQL.AppendFormat("{0}.{1}", ABShienSochiTaishoEntity.TABLE_NAME, ABShienSochiTaishoEntity.JUMINCD)
            strSQL.Append(" FROM ")
            strSQL.Append(ABShienSochiEntity.TABLE_NAME)
            strSQL.Append(" INNER JOIN ")
            strSQL.Append(ABShienSochiTaishoEntity.TABLE_NAME)
            strSQL.Append(" ON ")
            strSQL.AppendFormat("{0}.{1}", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SHIENSOCHIKANRINO)
            strSQL.Append(" = ")
            strSQL.AppendFormat("{0}.{1}", ABShienSochiTaishoEntity.TABLE_NAME, ABShienSochiTaishoEntity.SHIENSOCHIKANRINO)

            ' WHERE��̍쐬
            strSQL.Append(" WHERE ")
            strSQL.AppendFormat("{0}.{1} = '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SHIENSOCHIKBN, KARISHIENSOCHI)
            strSQL.AppendFormat(" AND {0}.{1} = '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SAISHINFG, SAISHINFG_ON)
            strSQL.AppendFormat(" AND {0}.{1} = '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.KARISHIENSOCHIEDYMD, "99999999")
            strSQL.AppendFormat(" AND {0}.{1} < {2}", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.KARISHIENSOCHISTYMD, ABShienSochiEntity.PARAM_KARISHIENSOCHISTYMD)
            strSQL.AppendFormat(" AND {0}.{1} <> '{2}'", ABShienSochiEntity.TABLE_NAME, ABShienSochiEntity.SAKUJOFG, SAKUJOFG_ON)

            ' ���������̃p�����[�^���쐬
            ' �V�X�e�����t
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.PARAM_KARISHIENSOCHISTYMD
            cfUFParameterClass.Value = cfDate.p_strSeirekiYMD
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csShienSochiDS = m_cfRdbClass.GetDataSet(strSQL.ToString, ABShienSochiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return csShienSochiDS

    End Function
#End Region

#Region "�x���[�u���擾"
    '************************************************************************************************
    '* ���\�b�h��     �x���[�u���擾
    '* 
    '* �\��           Public Function GetShienSochi(ByVal strShienSochiNo As String, ByVal strRirekiNo As String) As DataSet
    '* 
    '* �@�\�@�@    �@ �x���[�u�ԍ��Ɨ���ԍ�����f�[�^���擾
    '* 
    '* ����           strShienSochiNo�F�Z���R�[�h
    '*                strRirekiNO    :����ԍ�
    '* 
    '* �߂�l         �擾�����f�[�^�FDataSet
    '************************************************************************************************
    Public Overloads Function GetShienSochi(ByVal strShienSochiNo As String, ByVal strRirekiNo As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetShienSochi"
        Dim csShienSochiDS As DataSet                                        ' �x���[�u�f�[�^
        Dim strSQL As StringBuilder                                         ' SQL��SELECT��
        Dim cfUFParameterClass As UFParameterClass                          ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass      ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            strSQL = New StringBuilder

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiEntity.TABLE_NAME)

            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiEntity.TABLE_NAME, False)
            End If

            strSQL.AppendFormat("WHERE {0} = {1}", ABShienSochiEntity.SHIENSOCHIKANRINO, ABShienSochiEntity.KEY_SHIENSOCHIKANRINO)
            strSQL.AppendFormat(" AND {0} = {1}", ABShienSochiEntity.RIREKINO, ABShienSochiEntity.KEY_RIREKINO)

            ' ���������̃p�����[�^���쐬
            ' �x���[�u�Ǘ��ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_SHIENSOCHIKANRINO
            cfUFParameterClass.Value = strShienSochiNo
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ����ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiEntity.KEY_RIREKINO
            cfUFParameterClass.Value = strRirekiNo
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csShienSochiDS = m_cfRdbClass.GetDataSet(strSQL.ToString, ABShienSochiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return csShienSochiDS

    End Function

#End Region

#Region "���̑�"
    '************************************************************************************************
    '* ���\�b�h��     �X�V�����ݒ�
    '* 
    '* �\��           Private Sub SetUpdateDatetime()
    '* 
    '* �@�\           ���ݒ�̂Ƃ��X�V������ݒ肷��
    '* 
    '* ����           csDate As Object : �X�V�����̍���
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetUpdateDatetime(ByRef csDate As Object)
        Try
            '���ݒ�̂Ƃ�
            If (IsDBNull(csDate)) OrElse (CType(csDate, String).Trim.Equals(String.Empty)) Then
                csDate = m_strUpdateDatetime
            Else
                '�����Ȃ�
            End If
        Catch
            Throw
        End Try
    End Sub
#End Region

#End Region

End Class
