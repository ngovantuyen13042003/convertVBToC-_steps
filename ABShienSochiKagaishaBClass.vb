'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�x���[�u���Q�҃}�X�^�c�`(ABShienSochiKagaishaBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2023/10/13�@�����@���]
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2023/10/13             �yAB-0880-1�z�l������ڍ׊Ǘ����ڒǉ�
'* 2024/03/07  000001     �yAB-0900-1�z�A�h���X�E�x�[�X�E���W�X�g���Ή�(����)
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

Public Class ABShienSochiKagaishaBClass
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
    Private Const THIS_CLASS_NAME As String = "ABShienSochiKagaisha"                 ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                   ' �Ɩ��R�[�h

    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero

    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

    Private Const ERR_SHIENSOCHIKANRINO As String = "�x���[�u�Ǘ��ԍ�"

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
#Region "�x���[�u���Q�Ғ��o�@[GetShienSochiKagaisha]"
    '************************************************************************************************
    '* ���\�b�h��    �x���[�u���Q�҃}�X�^���o
    '* 
    '* �\��          Public Function GetShienSochiKagaisha As DataSet
    '* 
    '* �@�\�@�@    �@�x���[�u���Q�҃}�X�^���Y���f�[�^���擾����
    '* 
    '* ����          strShienSochiKanriNo: �x���[�u�Ǘ��ԍ� 
    '* 
    '* �߂�l        DataSet : �擾�����x���[�u���Q�҃}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetShienSochiKagaisha(ByVal strShienSochiKanriNo As String) As DataSet

        Return Me.GetShienSochiKagaisha(strShienSochiKanriNo, False)

    End Function
    '************************************************************************************************
    '* ���\�b�h��    �x���[�u���Q�҃}�X�^���o
    '* 
    '* �\��          Public Function GetShienSochiKagaisha As DataSet
    '* 
    '* �@�\�@�@    �@�x���[�u���Q�҃}�X�^���Y���f�[�^���擾����
    '* 
    '* ����          strShienSochiKanriNo: �x���[�u�Ǘ��ԍ� 
    '*               blnSakujoFG        : �폜�t���O
    '* 
    '* �߂�l        DataSet : �擾�����x���[�u���Q�҃}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetShienSochiKagaisha(ByVal strShienSochiKanriNo As String,
                                                    ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochiKagaisha"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            ' �x���[�u�Ǘ��ԍ����w�肳��Ă��Ȃ��Ƃ��G���[
            If IsNothing(strShienSochiKanriNo) OrElse (strShienSochiKanriNo.Trim.RLength = 0) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHIKANRINO, objErrorStruct.m_strErrorCode)
            Else
                '�����Ȃ�
            End If

            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiKagaishaEntity.TABLE_NAME)

            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiKagaishaEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            strSQL.Append(Me.CreateWhere(strShienSochiKanriNo, 0, blnSakujoFG))
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO)
            strSQL.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RENBAN)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiKagaishaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csAtenaEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��    �x���[�u���Q�҃}�X�^���o
    '* 
    '* �\��          Public Function GetShienSochiKagaisha As DataSet
    '* 
    '* �@�\�@�@    �@�x���[�u���Q�҃}�X�^���Y���f�[�^���擾����
    '* 
    '* ����          strShienSochiKanriNo: �x���[�u�Ǘ��ԍ� 
    '*               intRenban           : �A��
    '* 
    '* �߂�l        DataSet : �擾�����x���[�u���Q�҃}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetShienSochiKagaisha(ByVal strShienSochiKanriNo As String,
                                                    ByVal intRenban As Integer) As DataSet

        Return Me.GetShienSochiKagaisha(strShienSochiKanriNo, intRenban, False)

    End Function
    '************************************************************************************************
    '* ���\�b�h��    �x���[�u���Q�҃}�X�^���o
    '* 
    '* �\��          Public Function GetShienSochiKagaisha As DataSet
    '* 
    '* �@�\�@�@    �@�x���[�u���Q�҃}�X�^���Y���f�[�^���擾����
    '* 
    '* ����          strShienSochiKanriNo: �x���[�u�Ǘ��ԍ� 
    '*               intRenban           : �A��
    '*               blnSakujoFG         : �폜�t���O
    '* 
    '* �߂�l        DataSet : �擾�����x���[�u���Q�҃}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetShienSochiKagaisha(ByVal strShienSochiKanriNo As String,
                                                    ByVal intrenban As Integer,
                                                    ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochiKagaisha"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            ' �x���[�u�Ǘ��ԍ����w�肳��Ă��Ȃ��Ƃ��G���[
            If IsNothing(strShienSochiKanriNo) OrElse (strShienSochiKanriNo.Trim.RLength = 0) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_SHIENSOCHIKANRINO, objErrorStruct.m_strErrorCode)
            Else
                '�����Ȃ�
            End If

            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiKagaishaEntity.TABLE_NAME)

            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiKagaishaEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            strSQL.Append(Me.CreateWhere(strShienSochiKanriNo, intrenban, blnSakujoFG))

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiKagaishaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csAtenaEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��    �x���[�u���Q�҃}�X�^���o
    '* 
    '* �\��          Public Function GetShienSochiKagaisha As DataSet
    '* 
    '* �@�\�@�@    �@�x���[�u���Q�҃}�X�^���Y���f�[�^���擾����
    '* 
    '* ����          strShienSochiKanriNo: �x���[�u�Ǘ��ԍ��̔z�� 
    '* 
    '* �߂�l        DataSet : �擾�����x���[�u���Q�҃}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetShienSochiKagaisha(ByVal strShienSochiKanriNo() As String) As DataSet

        Const THIS_METHOD_NAME As String = "GetShienSochiKagaisha"
        Dim csAtenaEntity As DataSet
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
            strSQL.AppendFormat(" FROM {0} ", ABShienSochiKagaishaEntity.TABLE_NAME)

            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABShienSochiKagaishaEntity.TABLE_NAME, False)
            End If

            If strShienSochiKanriNo.Length = 0 Then
                csAtenaEntity = m_csDataSchma.Clone()
            Else
                ' WHERE��̍쐬
                With strSQL
                    .Append(" WHERE ")
                    .Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO)
                    .Append(" IN (")

                    For i As Integer = 0 To strShienSochiKanriNo.Length - 1
                        '�x���[�u�Ǘ��ԍ�
                        strParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO + i.ToString

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

                End With

                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + Me.GetType.Name + "�z" +
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                            "�y���s���\�b�h��:GetDataSet�z" +
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

                ' SQL�̎��s DataSet�̎擾
                csAtenaEntity = m_csDataSchma.Clone()
                csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString(), csAtenaEntity, ABShienSochiKagaishaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
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

        Return csAtenaEntity

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
            csSELECT.AppendFormat("SELECT {0}", ABShienSochiKagaishaEntity.SHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RENBAN)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_JUMINCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SHIMEI)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_UMAREYMD)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SEIBETSU)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_YUBINNO)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KANNAIKANGAIKB)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_JUSHOCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_JUSHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SHICHOSON)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_BANCHI)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KATAGAKI)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KOKUSEKICD)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KOKUSEKI)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_KOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KAGAISHA_SONOTA)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABShienSochiKagaishaEntity.KOSHINUSER)

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
    '* �\��         Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\         WHERE�����쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����         strShienSochiKanriNo : �x���[�u�Ǘ��ԍ� 
    '*              intRenban            : �A��
    '*              blnSakujoFG          : �폜�t���O
    '* 
    '* �߂�l       �Ȃ�
    '************************************************************************************************
    Private Function CreateWhere(ByVal strShienSochiKanriNo As String,
                                 ByVal intRenban As Integer,
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
            csWHERE.AppendFormat("WHERE {0} = {1}", ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO, ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO
            cfUFParameterClass.Value = strShienSochiKanriNo
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �A��
            If (Not intRenban = 0) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABShienSochiKagaishaEntity.RENBAN, ABShienSochiKagaishaEntity.KEY_RENBAN)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_RENBAN
                cfUFParameterClass.Value = intRenban.ToString
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '�����Ȃ�
            End If

            ' �폜�t���O
            If blnSakujoFG = False Then
                csWHERE.AppendFormat(" AND {0} <> '{1}'", ABShienSochiKagaishaEntity.SAKUJOFG, SAKUJOFG_ON)
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

#Region "�x���[�u���Q�҃}�X�^�ǉ��@[InsertShienSochiKagaisha]"
    '************************************************************************************************
    '* ���\�b�h��     �x���[�u���Q�҃}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertShienSochiKagaisha(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�x���[�u���Q�҃}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertShienSochiKagaisha(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertShienSochiKagaisha"
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
            csDataRow(ABShienSochiKagaishaEntity.TANMATSUID) = m_cfControlData.m_strClientId     '�[���h�c
            csDataRow(ABShienSochiKagaishaEntity.SAKUJOFG) = SAKUJOFG_OFF                        '�폜�t���O
            csDataRow(ABShienSochiKagaishaEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              '�X�V�J�E���^
            csDataRow(ABShienSochiKagaishaEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      '�쐬���[�U�[
            csDataRow(ABShienSochiKagaishaEntity.KOSHINUSER) = m_cfControlData.m_strUserId       '�X�V���[�U�[

            '�쐬�����A�X�V�����̐ݒ�
            Me.SetUpdateDatetime(csDataRow(ABShienSochiKagaishaEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABShienSochiKagaishaEntity.KOSHINNICHIJI))

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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
                strParamName = String.Format("{0}{1}", ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL���̍쐬
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '�Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})",
                                           ABShienSochiKagaishaEntity.TABLE_NAME,
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

#Region "�x���[�u���Q�҃}�X�^�X�V�@[UpdateShienSochiKagaisha]"
    '************************************************************************************************
    '* ���\�b�h��     �x���[�u���Q�҃}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateShienSochiKagaisha(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �x���[�u���Q�҃}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csDataRow As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateShienSochiKagaisha(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateShienSochiKagaisha"
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
            csDataRow(ABShienSochiKagaishaEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABShienSochiKagaishaEntity.KOSHINCOUNTER) = CDec(csDataRow(ABShienSochiKagaishaEntity.KOSHINCOUNTER)) + 1 '�X�V�J�E���^
            csDataRow(ABShienSochiKagaishaEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            csDataRow(ABShienSochiKagaishaEntity.KOSHINNICHIJI) = m_strUpdateDatetime

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABShienSochiKagaishaEntity.PREFIX_KEY.RLength) = ABShienSochiKagaishaEntity.PREFIX_KEY) Then
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()

                    '�L�[���ڈȊO�͕ҏW���e�擾
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                         = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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
            m_strUpdateSQL = "UPDATE " + ABShienSochiKagaishaEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiKagaishaEntity.RENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiKagaishaEntity.KEY_RENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiKagaishaEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER)

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�x���[�u�Ǘ��ԍ��E�A�ԁE�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If Not (csDataColumn.ColumnName = ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO) AndAlso
                    Not (csDataColumn.ColumnName = ABShienSochiKagaishaEntity.RENBAN) AndAlso
                     Not (csDataColumn.ColumnName = ABShienSochiKagaishaEntity.SAKUSEIUSER) AndAlso
                      Not (csDataColumn.ColumnName = ABShienSochiKagaishaEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL���̍쐬
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_RENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER
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

#Region "�x���[�u���Q�҃}�X�^�폜�@[DeleteShienSochiKagaisha]"
    '************************************************************************************************
    '* ���\�b�h��     �x���[�u���Q�҃}�X�^�폜
    '* 
    '* �\��           Public Function DeleteShienSochiKagaisha(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�x���[�u���Q�҃}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csDataRow As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteShienSochiKagaisha(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteShienSochiKagaisha"
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
            csDataRow(ABShienSochiKagaishaEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABShienSochiKagaishaEntity.SAKUJOFG) = SAKUJOFG_ON                                                       '�폜�t���O
            csDataRow(ABShienSochiKagaishaEntity.KOSHINCOUNTER) = CDec(csDataRow(ABShienSochiKagaishaEntity.KOSHINCOUNTER)) + 1 '�X�V�J�E���^
            csDataRow(ABShienSochiKagaishaEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABShienSochiKagaishaEntity.KOSHINNICHIJI))

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABShienSochiKagaishaEntity.PREFIX_KEY.RLength) = ABShienSochiKagaishaEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                                 csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PREFIX_KEY.RLength),
                                           DataRowVersion.Original).ToString()
                    '�L�[���ڈȊO�͕ҏW���e��ݒ�
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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
    '* ���\�b�h��     �x���[�u���Q�҃}�X�^�����폜
    '* 
    '* �\��           Public Function DeleteShienSochiKagaisha(ByVal csDataRow As DataRow, _
    '*                                               ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@�@�x���[�u���Q�҃}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csDataRow As DataRow : �폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteShienSochiKagaisha(ByVal csDataRow As DataRow,
                                             ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteShienSochiKagaisha"
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
                If (cfParam.ParameterName.RSubstring(0, ABShienSochiKagaishaEntity.PREFIX_KEY.RLength) = ABShienSochiKagaishaEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABShienSochiKagaishaEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()

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
            csWhere.Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiKagaishaEntity.RENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiKagaishaEntity.KEY_RENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiKagaishaEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER)


            ' �_��DELETE SQL���̍쐬
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABShienSochiKagaishaEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where���̒ǉ�
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_RENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER
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
            csWhere.Append(ABShienSochiKagaishaEntity.SHIENSOCHIKANRINO)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiKagaishaEntity.RENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiKagaishaEntity.KEY_RENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABShienSochiKagaishaEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER)

            ' ����DELETE SQL���̍쐬
            m_strDelButuriSQL = "DELETE FROM " + ABShienSochiKagaishaEntity.TABLE_NAME + csWhere.ToString

            ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_SHIENSOCHIKANRINO
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_RENBAN
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABShienSochiKagaishaEntity.KEY_KOSHINCOUNTER
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
