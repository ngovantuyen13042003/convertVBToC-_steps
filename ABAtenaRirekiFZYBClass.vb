'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a��������t���}�X�^�c�`
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2011/10/24�@�����@�m��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2023/12/07  000001     �yAB-9000-1�z�Z��X�V�A�g�W�����Ή�(����)
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Data
Imports System.Text

'************************************************************************************************
'*
'* ��������t���}�X�^�擾���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABAtenaRirekiFZYBClass
#Region "�����o�ϐ�"
    ' �p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_strInsertSQL As String                        ' INSERT�pSQL
    Private m_strUpdateSQL As String                        ' UPDATE�pSQL
    Private m_strDelRonriSQL As String                      ' �_���폜�pSQL
    Private m_strDelButuriSQL As String                     ' �����폜�pSQL
    Private m_strDelFromJuminCDSQL As String                ' �����폜�pSQL(�P�Z���R�[�h�w��)
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT�p�p�����[�^�R���N�V����
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    '�_���폜�p�p�����[�^�R���N�V����
    Private m_cfDelButuriUFParameterCollectionClass As UFParameterCollectionClass   '�����폜�p�p�����[�^�R���N�V����
    Private m_cfDelFromJuminCDPrmCollection As UFParameterCollectionClass           '�����폜�pSQL(�P�Z���R�[�h�w��)
    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_strUpdateDatetime As String                   ' �X�V����

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaRirekiFZYBClass"                 ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h

    Private Const SAKUJOFG_OFF As String = "0"
    Private Const SAKUJOFG_ON As String = "1"
    Private Const KOSHINCOUNTER_DEF As Decimal = Decimal.Zero

    Private Const FORMAT_UPDATETIME As String = "yyyyMMddHHmmssfff"

    Private Const ERR_JUMINCD As String = "�Z���R�[�h"
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
        m_strDelFromJuminCDSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing
        m_cfDelFromJuminCDPrmCollection = Nothing
    End Sub
#End Region

#Region "���\�b�h"
#Region "��������t���}�X�^���o�@[GetAtenaFZYRBHoshu]"
    '************************************************************************************************
    '* ���\�b�h��    ��������t���}�X�^���o
    '* 
    '* �\��          Public Function GetAtenaFZYRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�Z�o�O�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����          strJuminCD         : �Z���R�[�h 
    '*               strRrkNo           : ����ԍ�
    '*               strJuminJutogaiKB  : �Z���Z�o�O�敪
    '* 
    '* �߂�l        DataSet : �擾������������t���}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaFZYRBHoshu(ByVal strJuminCD As String, _
                                                 ByVal strRrkNo As String, _
                                                 ByVal strJuminJutogaiKB As String) As DataSet
        Return GetAtenaFZYRBHoshu(strJuminCD, strRrkNo, strJuminJutogaiKB, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��������t���}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String, _
    '*                                                ByVal blnSakujoKB As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@��������t���}�X�^���Y���f�[�^���擾����
    '* 
    '* ����          strJuminCD     : �Z���R�[�h 
    '*               strRrkNo       : ����ԍ�
    '*               strJuminJutogaiKB  : �Z���Z�o�O�敪
    '*               blnSakujoFG    : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾������������t���}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaFZYRBHoshu(ByVal strJuminCD As String, _
                                                 ByVal strRrkNo As String, _
                                                 ByVal strJuminJutogaiKB As String, _
                                                 ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaFZYRBHoshu"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaRirekiEntity As DataSet                  '���������f�[�^�Z�b�g
        Dim strSQL As New StringBuilder()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            ' �Z���R�[�h���w�肳��Ă��Ȃ��Ƃ��G���[
            If IsNothing(strJuminCD) OrElse (strJuminCD.Trim.RLength = 0) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_JUMINCD, objErrorStruct.m_strErrorCode)
            Else
                '�����Ȃ�
            End If

            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiFZYEntity.TABLE_NAME)

            '�ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiFZYEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            strSQL.Append(Me.CreateWhere(strJuminCD, strRrkNo, strJuminJutogaiKB, blnSakujoFG))

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csAtenaRirekiEntity = m_csDataSchma.Clone()
            csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRirekiEntity, ABAtenaRirekiFZYEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csAtenaRirekiEntity

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
            csSELECT.AppendFormat("SELECT {0}", ABAtenaRirekiFZYEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KYUSHICHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RIREKINO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TABLEINSERTKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.LINKNO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUMINHYOJOTAIKBN)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKYOCHITODOKEFLG)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.HONGOKUMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANAHONGOKUMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANJIHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANJITSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KATAKANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.UMAREFUSHOKBN)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TSUSHOMEITOUROKUYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUKIKANCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUKIKANMEISHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUSHACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUSHAMEISHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.ZAIRYUCARDNO)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOFUYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOFUYOTEISTYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOFUYOTEIEDYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYU)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.FRNSTAINUSMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.FRNSTAINUSKANAMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSKANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.STAINUSKANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE6)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE7)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE8)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE9)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.RESERVE10)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiFZYEntity.KOSHINUSER)

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
    '* ���\�b�h��     WHERE���̍쐬
    '* 
    '* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\�@�@    �@�@WHERE�����쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String, _
                                 ByVal strRrkNo As String, _
                                 ByVal strJuminJutogaiKB As String, _
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

            ' �Z���R�[�h
            csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiFZYEntity.JUMINCD, ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '����ԍ�
            If (Not strRrkNo.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYEntity.RIREKINO, ABAtenaRirekiFZYEntity.KEY_RIREKINO)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO
                cfUFParameterClass.Value = strRrkNo
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '�����Ȃ�
            End If

            '�Z���Z�o�O�敪
            If (Not strJuminJutogaiKB.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB, ABAtenaRirekiFZYEntity.KEY_JUMINJUTOGAIKB)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINJUTOGAIKB
                cfUFParameterClass.Value = strJuminJutogaiKB
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '�����Ȃ�
            End If

            ' �폜�t���O
            If blnSakujoFG = False Then
                csWHERE.AppendFormat(" AND {0} <> '{1}'", ABAtenaRirekiFZYEntity.SAKUJOFG, SAKUJOFG_ON)
            Else
                '�����Ȃ�
            End If

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

        Return csWHERE.ToString

    End Function

#End Region

#Region "��������t���}�X�^���o"
    '************************************************************************************************
    '* ���\�b�h��     ��������t���}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String, _
    '*                                                ByVal blnSakujoKB As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@��������t���}�X�^���Y���f�[�^���擾����
    '* 
    '* ����          strJuminCD     : �Z���R�[�h 
    '*               strRrkNo       : ����ԍ�
    '*               strJuminJutogaiKB  : �Z���Z�o�O�敪
    '*               blnSakujoFG    : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾������������t���}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaRirekiFZYByLinkNo(ByVal strJuminCD As String,
                                                 ByVal strLinkNo As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaRirekiFZYByLinkNo"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaRirekiFZYEntity As DataSet               '��������t���f�[�^�Z�b�g
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            ' �Z���R�[�h���w�肳��Ă��Ȃ��Ƃ��G���[
            If IsNothing(strJuminCD) OrElse (strJuminCD.Trim.RLength = 0) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_JUMINCD, objErrorStruct.m_strErrorCode)
            Else
                '�����Ȃ�
            End If

            ' SELECT�p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiFZYEntity.TABLE_NAME)

            '�ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiFZYEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            ' �Z���R�[�h
            strSQL.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiFZYEntity.JUMINCD, ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '�����N�ԍ�
            strSQL.AppendFormat(" AND {0} = {1}", ABAtenaRirekiFZYEntity.LINKNO, ABAtenaRirekiFZYEntity.PARAM_LINKNO)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_LINKNO
            cfUFParameterClass.Value = strLinkNo
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '�Z���Z�o�O�敪
            strSQL.AppendFormat(" AND {0} = '1'", ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB)

            ' �폜�t���O
            strSQL.AppendFormat(" AND {0} <> '{1}'", ABAtenaRirekiFZYEntity.SAKUJOFG, SAKUJOFG_ON)

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:GetDataSet�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csAtenaRirekiFZYEntity = m_csDataSchma.Clone()
            csAtenaRirekiFZYEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRirekiFZYEntity, ABAtenaRirekiFZYEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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

        Return csAtenaRirekiFZYEntity

    End Function
#End Region

#Region "��������t���}�X�^�ǉ��@[InsertAtenaFZYRB]"
    '************************************************************************************************
    '* ���\�b�h��     ��������t���}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@��������t���}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaFZYRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer        '�ǉ�����

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing OrElse m_strInsertSQL = String.Empty OrElse _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateInsertSQL(csDataRow)
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRirekiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId   ' �[���h�c
            csDataRow(ABAtenaRirekiFZYEntity.SAKUJOFG) = SAKUJOFG_OFF                      ' �폜�t���O
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF            ' �X�V�J�E���^
            csDataRow(ABAtenaRirekiFZYEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId    ' �쐬���[�U�[
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId     ' �X�V���[�U�[

            '�쐬�����A�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI))

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

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
                strParamName = String.Format("{0}{1}", ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL���̍쐬
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '�Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})", _
                                           ABAtenaRirekiFZYEntity.TABLE_NAME, _
                                           csInsertColumn.ToString.TrimEnd(",".ToCharArray), _
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

#Region "��������t���}�X�^�X�V�@[UpdateAtenaFZYRB]"
    '************************************************************************************************
    '* ���\�b�h��     ��������t���}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@��������t���}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csDataRow As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateAtenaFZYRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaRB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        Dim intUpdCnt As Integer                            '�X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing OrElse m_strUpdateSQL = String.Empty OrElse _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateUpdateSQL(csDataRow)
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRirekiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '�[���h�c
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)) + 1     '�X�V�J�E���^
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI))

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength) = ABAtenaRirekiFZYEntity.PREFIX_KEY) Then
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

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
            m_strUpdateSQL = "UPDATE " + ABAtenaRirekiFZYEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER)

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�Z���b�c�E����ԍ��E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If Not (csDataColumn.ColumnName = ABAtenaRirekiFZYEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABAtenaRirekiFZYEntity.RIREKINO) AndAlso _
                     Not (csDataColumn.ColumnName = ABAtenaRirekiFZYEntity.SAKUSEIUSER) AndAlso _
                      Not (csDataColumn.ColumnName = ABAtenaRirekiFZYEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL���̍쐬
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER
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

#Region "��������t���}�X�^�폜�@[DeleteAtenaFZYRB]"
    '************************************************************************************************
    '* ���\�b�h��     ��������t���}�X�^�폜
    '* 
    '* �\��           Public Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@��������t���}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csDataRow As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaFZYRB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        Dim intDelCnt As Integer                            '�폜����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDelRonriSQL Is Nothing OrElse m_strDelRonriSQL = String.Empty OrElse _
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                Call CreateDeleteRonriSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRirekiFZYEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '�[���h�c
            csDataRow(ABAtenaRirekiFZYEntity.SAKUJOFG) = SAKUJOFG_ON                                                        '�폜�t���O
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)) + 1     '�X�V�J�E���^
            csDataRow(ABAtenaRirekiFZYEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI))

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength) = ABAtenaRirekiFZYEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

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

        Return intDelCnt

    End Function
    '************************************************************************************************
    '* ���\�b�h��     ��������t���}�X�^�����폜
    '* 
    '* �\��           Public Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow, _
    '*                                              ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@�@��������t���}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csDataRow As DataRow : �폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaFZYRB(ByVal csDataRow As DataRow, _
                                            ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaFZYRB"
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        Dim cfParam As UFParameterClass                     ' �p�����[�^�N���X
        Dim intDelCnt As Integer                            ' �폜����


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
            If (m_strDelButuriSQL Is Nothing Or m_strDelButuriSQL = String.Empty Or _
                    IsNothing(m_cfDelButuriUFParameterCollectionClass)) Then
                Call CreateDeleteButsuriSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength) = ABAtenaRirekiFZYEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiFZYEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    '�����Ȃ�
                End If
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass)

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

        Return intDelCnt

    End Function
    '* ����ԍ� 000022 2005/11/18 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ��������t���}�X�^�����폜(�P�Z���R�[�h�w��)
    '* 
    '* �\��           Public Overloads Function DeleteAtenaFZYRB(ByVal strJuminCD As String) As Integer
    '* 
    '* �@�\�@�@    �@�@��������t���}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           strJuminCD As String : �폜����ΏۂƂȂ�Z���R�[�h
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaFZYRB(ByVal strJuminCD As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteAtenaFZYRB"
        Dim intDelCnt As Integer                            ' �폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �폜�p�̃p�����[�^�tDELETE��������ƃp�����[�^�R���N�V�������쐬����
            If (m_strDelFromJuminCDSQL Is Nothing OrElse m_strDelFromJuminCDSQL = String.Empty OrElse _
                    IsNothing(m_cfDelFromJuminCDPrmCollection)) Then
                Call CreateDelFromJuminCDSQL()
            Else
                '�����Ȃ�
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            m_cfDelFromJuminCDPrmCollection(ABAtenaRirekiFZYEntity.KEY_JUMINCD).Value = strJuminCD

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelFromJuminCDSQL, m_cfDelFromJuminCDPrmCollection) + "�z")

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelFromJuminCDSQL, m_cfDelFromJuminCDPrmCollection)

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
            csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER)

            ' �_��DELETE SQL���̍쐬
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiFZYEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where���̒ǉ�
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass


            ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER
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
            csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER)

            ' ����DELETE SQL���̍쐬
            m_strDelButuriSQL = "DELETE FROM " + ABAtenaRirekiFZYEntity.TABLE_NAME + csWhere.ToString

            ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_RIREKINO
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.KEY_KOSHINCOUNTER
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
    '************************************************************************************************
    '* ���\�b�h��     �����폜�p(�P�Z���b�c�w��)SQL���̍쐬
    '* 
    '* �\��           Private Sub CreateDelFromJuminCDSQL()
    '* 
    '* �@�\           �Z���b�c�ŊY���S�����f�[�^�𕨗��폜����SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateDelFromJuminCDSQL()
        Const THIS_METHOD_NAME As String = "CreateDelFromJuminCDSQL"
        Dim csWhere As StringBuilder                        'WHERE��`

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiFZYEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiFZYEntity.KEY_JUMINCD)

            ' ����DELETE(�P�Z���b�c�w��) SQL���̍쐬
            m_strDelFromJuminCDSQL = "DELETE FROM " + ABAtenaRirekiFZYEntity.TABLE_NAME + csWhere.ToString

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            m_cfDelFromJuminCDPrmCollection = New UFParameterCollectionClass
            m_cfDelFromJuminCDPrmCollection.Add(ABAtenaRirekiFZYEntity.KEY_JUMINCD, DbType.String)

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
