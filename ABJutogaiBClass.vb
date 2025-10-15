'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�Z�o�O�}�X�^�c�`(ABJutogaiBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2002/12/20�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/12 000001     �L�������̑Ή�
'* 2003/03/25 000002     �X�֔ԍ����ǉ��ɂȂ�܂����B
'* 2003/04/16 000003     ���a��N�����̓��t�`�F�b�N�𐔒l�`�F�b�N�ɕύX
'*                       �����p�J�i�̔��p�J�i�`�F�b�N���`�m�j�`�F�b�N�ɕύX
'* 2003/05/21 000004     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/28 000005     RDB�A�N�Z�X���O�̏C��
'* 2003/09/11 000006     �[���h�c�������`�F�b�N��ANK�ɂ���
'* 2003/10/09 000007     �쐬���[�U�[�E�X�V���[�U�[�`�F�b�N�̕ύX
'* 2003/10/30 000008     �d�l�ύX�A�J�^�J�i�`�F�b�N��ANK�`�F�b�N�ɕύX
'* 2004/05/13 000009     �d�l�ύX�A�ėp�敪��ANK�`�F�b�N�ɕύX
'* 2005/01/15 000010     �d�l�ύX�A�Z���R�[�h��ANK�`�F�b�N�ɕύX
'* 2005/06/16 000011     SQL����Insert,Update,�_��Delete,����Delete�̊e���\�b�h���Ă΂ꂽ���Ɋe���쐬����(�}���S���R)
'* 2005/12/26 000012     �d�l�ύX�F�s����b�c��ANK�`�F�b�N�ɕύX(�}���S���R)
'* 2010/04/16 000013     VS2008�Ή��i��Áj
'* 2011/10/24 000014     �yAB17010�z���Z��@�����Ή��������t���}�X�^�ǉ�   (����)
'* 2023/08/14 000015    �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ�(����)
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
'* �Z�o�O�}�X�^�擾���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABJutogaiBClass
#Region "�����o�ϐ�"
    ' �p�����[�^�̃����o�ϐ�
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X
    Private m_strInsertSQL As String                        ' INSERT�pSQL
    Private m_strUpdateSQL As String                        ' UPDATE�pSQL
    Private m_strDelRonriSQL As String                      ' �_���폜�pSQL
    Private m_strDelButuriSQL As String                     ' �����폜�pSQL
    Private m_cfInsertUFParameterCollection As UFParameterCollectionClass       ' INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollection As UFParameterCollectionClass       ' UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDelRonriUFParameterCollection As UFParameterCollectionClass     ' �_���폜�p�p�����[�^�R���N�V����
    Private m_cfDelButuriUFParameterCollection As UFParameterCollectionClass    ' �����폜�p�p�����[�^�R���N�V����

    '*����ԍ� 000014 2011/10/24 �ǉ��J�n
    Private m_csSekoYMDHanteiB As ABSekoYMDHanteiBClass             '�{�s������B�׽
    Private m_csAtenaFZYB As ABAtenaFZYBClass                       '�����t���}�X�^B�׽
    Private m_strJukihoKaiseiKB As String                           '�Z��@�����敪
    '*����ԍ� 000014 2011/10/24 �ǉ��I��
    '*����ԍ� 000015 2023/08/14 �ǉ��J�n
    Private m_blnJukihoKaiseiFG As Boolean = False
    '*����ԍ� 000015 2023/08/14 �ǉ��I��

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABJutogaiBClass"                 ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h
    Private Const JUKIHOKAISEIKB_ON As String = "1"

#End Region

#Region "�v���p�e�B"
    '*����ԍ� 000014 2011/10/24 �ǉ��J�n
    Public WriteOnly Property p_strJukihoKaiseiKB() As String      ' �Z��@�����敪
        Set(ByVal Value As String)
            m_strJukihoKaiseiKB = Value
        End Set
    End Property
    '*����ԍ� 000014 2011/10/24 �ǉ��I��
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
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
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
        m_cfInsertUFParameterCollection = Nothing
        m_cfUpdateUFParameterCollection = Nothing
        m_cfDelRonriUFParameterCollection = Nothing
        m_cfDelButuriUFParameterCollection = Nothing

        '*����ԍ� 000014 2011/10/24 �ǉ��J�n
        m_strJukihoKaiseiKB = String.Empty
        '*����ԍ� 000014 2011/10/24 �ǉ��I��
    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �Z�o�O�}�X�^���o
    '* 
    '* �\��           Public Function GetJutogaiBHoshu() As DataSet
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataSet : �擾�����Z�o�O�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetJutogaiBHoshu() As DataSet

        Return Me.GetJutogaiBHoshu(False)

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Z�o�O�}�X�^���o
    '* 
    '* �\��           Public Function GetJutogaiBHoshu(ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\           �Z�o�O�}�X�^���S���f�[�^���擾����
    '* 
    '* ����           blnSakujoFG   : �폜�t���O�i�ȗ��j
    '* 
    '* �߂�l         DataSet : �擾�����Z�o�O�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetJutogaiBHoshu(ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetJutogaiBHoshu"
        Dim csJutogaiEntity As DataSet
        Dim strSQL As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            If blnSakujoFG = True Then
                strSQL = "SELECT * FROM " + ABJutogaiEntity.TABLE_NAME
            Else
                strSQL = "SELECT * FROM " + ABJutogaiEntity.TABLE_NAME _
                        + " WHERE " + ABJutogaiEntity.SAKUJOFG + " <> '1';"
            End If

            '*����ԍ� 000015 2023/08/14 �ǉ��J�n
            '�{�s���ȍ~�t���O���擾����
            m_csSekoYMDHanteiB = New ABSekoYMDHanteiBClass(Me.m_cfControlData, Me.m_cfConfigDataClass, Me.m_cfRdbClass)
            m_blnJukihoKaiseiFG = m_csSekoYMDHanteiB.CheckAfterSekoYMD

            '�Z��@�����ȍ~�̂Ƃ��A�͈���_�W���A�����t��_�W����LEFT OUTER JOIN���Ď擾����
            If (m_blnJukihoKaiseiFG) Then
                strSQL = "SELECT A.* FROM (" + strSQL + ") A"
                strSQL = strSQL + " LEFT OUTER JOIN " + ABAtenaHyojunEntity.TABLE_NAME + " B ON A." + ABJutogaiEntity.JUMINCD +
                    "  = B." + ABAtenaHyojunEntity.JUMINCD
                strSQL = strSQL + " LEFT OUTER JOIN " + ABAtenaFZYHyojunEntity.TABLE_NAME + " C ON A." + ABJutogaiEntity.JUMINCD +
                    " = C." + ABAtenaFZYHyojunEntity.JUMINCD
            End If
            '*����ԍ� 000015 2023/08/14 �ǉ��I��

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
            '                            "�y���s���\�b�h��:GetDataSet�z" +
            '                            "�ySQL���e:" + strSQL + "�z")

            ' SQL�̎��s DataSet�̎擾
            csJutogaiEntity = m_cfRdbClass.GetDataSet(strSQL, ABJutogaiEntity.TABLE_NAME)


            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

        Return csJutogaiEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Z�o�O�}�X�^���o
    '* 
    '* �\��           Public Function GetJutogaiBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD    : �Z���R�[�h�i�ȗ��j
    '* 
    '* �߂�l         DataSet : �擾�����Z�o�O�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetJutogaiBHoshu(ByVal strJuminCD As String) As DataSet

        Return Me.GetJutogaiBHoshu(strJuminCD, False)

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Z�o�O�}�X�^���o
    '* 
    '* �\��           Public Function GetJutogaiBHoshu(Optional ByVal strJuminCD As String = "", _
    '*                                Optional ByVal blnSakujoFG As Boolean = False) As DataSet
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD    : �Z���R�[�h
    '*                blnSakujoFG   : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾�����Z�o�O�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetJutogaiBHoshu(ByVal strJuminCD As String,
                                               ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetJutogaiBHoshu"
        Dim csJutogaiEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            '*����ԍ� 000014 2011/10/24 �C���J�n
            '�Z��@�����ȍ~�͈����t���}�X�^��t��
            If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                strSQL.AppendFormat("SELECT {0}.* ", ABJutogaiEntity.TABLE_NAME)
                Me.SetFZYEntity(strSQL)
                strSQL.AppendFormat(" FROM {0} ", ABJutogaiEntity.TABLE_NAME)
                Me.SetFZYJoin(strSQL)
                strSQL.AppendFormat(" WHERE {0}.{1}={2} ", ABJutogaiEntity.TABLE_NAME, ABJutogaiEntity.JUMINCD, ABJutogaiEntity.KEY_JUMINCD)
                If blnSakujoFG = False Then
                    strSQL.AppendFormat(" AND {0}.{1} <> '1' ", ABJutogaiEntity.TABLE_NAME, ABJutogaiEntity.SAKUJOFG)
                End If
            Else
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABJutogaiEntity.TABLE_NAME)
                strSQL.Append(" WHERE ")
                strSQL.Append(ABJutogaiEntity.JUMINCD)
                strSQL.Append(" = ")
                strSQL.Append(ABJutogaiEntity.KEY_JUMINCD)
                If blnSakujoFG = False Then
                    strSQL.Append(" AND ")
                    strSQL.Append(ABJutogaiEntity.SAKUJOFG)
                    strSQL.Append(" <> '1';")
                End If
            End If
            '*����ԍ� 000014 2011/10/24 �C���I��

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaJiteEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000005 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:GetDataSet�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '*����ԍ� 000005 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            csJutogaiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABJutogaiEntity.TABLE_NAME, cfUFParameterCollectionClass)


            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

        Return csJutogaiEntity

    End Function

    '*����ԍ� 000014 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����t���f�[�^���ڕҏW
    '* 
    '* �\��           Private SetFZYEntity()
    '* 
    '* �@�\           �����t���f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TABLEINSERTKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.LINKNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINHYOJOTAIKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKYOCHITODOKEFLG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.HONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJIHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJITSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KATAKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.UMAREFUSHOKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUKIKANCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUKIKANMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUSHACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUSHAMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.ZAIRYUCARDNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYOTEISTYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KOFUYOTEIEDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.FRNSTAINUSMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.FRNSTAINUSKANAMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.STAINUSKANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE5)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE6)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE7)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE8)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE9)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.RESERVE10)
    End Sub
    '************************************************************************************************
    '* ���\�b�h��     �����t���e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetFZYJoin()
    '* 
    '* �@�\           �����t���e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaFZYEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABJutogaiEntity.TABLE_NAME, ABJutogaiEntity.JUMINCD,
                                    ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD)
    End Sub
    '*����ԍ� 000014 2011/10/24 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �Z�o�O�}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertJutogaiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertJutogaiB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertJutogaiB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000013
        'Dim csInstRow As DataRow
        '* corresponds to VS2008 End 2010/04/16 000013
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer        '�ǉ�����
        Dim strUpdateDateTime As String


        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or
                m_cfInsertUFParameterCollection Is Nothing) Then
                '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateInsertSQL(csDataRow)
                '* ����ԍ� 000011 2005/06/16 �ǉ��I��
            End If

            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '�쐬����

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABJutogaiEntity.TANMATSUID) = m_cfControlData.m_strClientId   '�[���h�c
            csDataRow(ABJutogaiEntity.SAKUJOFG) = "0"                               '�폜�t���O
            csDataRow(ABJutogaiEntity.KOSHINCOUNTER) = Decimal.Zero                 '�X�V�J�E���^
            csDataRow(ABJutogaiEntity.SAKUSEINICHIJI) = strUpdateDateTime           '�쐬����
            csDataRow(ABJutogaiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId    '�쐬���[�U�[
            csDataRow(ABJutogaiEntity.KOSHINNICHIJI) = strUpdateDateTime            '�X�V����
            csDataRow(ABJutogaiEntity.KOSHINUSER) = m_cfControlData.m_strUserId     '�X�V���[�U�[

            ' ���N���X�̃f�[�^�������`�F�b�N���s��
            For Each csDataColumn In csDataRow.Table.Columns
                ' �f�[�^�������`�F�b�N
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString().Trim)
            Next csDataColumn

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollection
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*����ԍ� 000005 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strInsertSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollection) + "�z")
            '*����ԍ� 000005 2003/08/28 �C���I��

            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollection)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Z�o�O�}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateJutogaiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csDataRow As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateJutogaiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateJutogaiB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000013
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000013
        Dim intUpdCnt As Integer                            '�X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or
                m_cfUpdateUFParameterCollection Is Nothing) Then
                '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateUpdateSQL(csDataRow)
                '* ����ԍ� 000011 2005/06/16 �ǉ��I��
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABJutogaiEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   ' �[���h�c
            csDataRow(ABJutogaiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABJutogaiEntity.KOSHINCOUNTER)) + 1           ' �X�V�J�E���^
            csDataRow(ABJutogaiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   ' �X�V����
            csDataRow(ABJutogaiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     ' �X�V���[�U�[


            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollection
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABJutogaiEntity.PREFIX_KEY.RLength) = ABJutogaiEntity.PREFIX_KEY) Then
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollection(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    m_cfUpdateUFParameterCollection(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000005 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strUpdateSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollection) + "�z")
            '*����ԍ� 000005 2003/08/28 �C���I��

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollection)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "UpdateKinyuKikan")

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Z�o�O�}�X�^�폜
    '* 
    '* �\��           Public Function DeleteJutogaiB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csDataRow As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteJutogaiB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateJutogaiB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000013
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000013
        Dim intDelCnt As Integer                            '�폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or
                    m_cfDelRonriUFParameterCollection Is Nothing) Then
                '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateDeleteRonriSQL(csDataRow)
                '* ����ԍ� 000011 2005/06/16 �ǉ��I��
            End If


            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABJutogaiEntity.TANMATSUID) = m_cfControlData.m_strClientId                                     '�[���h�c
            csDataRow(ABJutogaiEntity.SAKUJOFG) = "1"                                                                   '�폜�t���O
            csDataRow(ABJutogaiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABJutogaiEntity.KOSHINCOUNTER)) + 1               '�X�V�J�E���^
            csDataRow(ABJutogaiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")     '�X�V����
            csDataRow(ABJutogaiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                       '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollection
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABJutogaiEntity.PREFIX_KEY.RLength) = ABJutogaiEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollection(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PREFIX_KEY.RLength),
                                    DataRowVersion.Original).ToString()
                Else
                    ' �f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    m_cfDelRonriUFParameterCollection(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000005 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strUpdateSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollection) + "�z")
            '*����ԍ� 000005 2003/08/28 �C���I��

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollection)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Z�o�O�}�X�^�����폜
    '* 
    '* �\��           Public Function DeleteJutogaiB(ByVal csDataRow As DataRow, _
    '*                                               ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csDataRow As DataRow : �폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteJutogaiB(ByVal csDataRow As DataRow,
                                             ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteJutogaiB"
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        Dim cfParam As UFParameterClass                     ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000013
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000013
        Dim intDelCnt As Integer                            ' �폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �폜�敪�̃`�F�b�N���s��
            If Not (strSakujoKB = "D") Then
                ' �G���[��`���擾
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_DELETE_SAKUJOKB)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' �폜�p�̃p�����[�^�tDELETE��������ƃp�����[�^�R���N�V�������쐬����
            If (m_strDelButuriSQL Is Nothing Or m_strDelButuriSQL = "" Or
                    IsNothing(m_cfDelButuriUFParameterCollection)) Then
                '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateDeleteButsuriSQL(csDataRow)
                '* ����ԍ� 000011 2005/06/16 �ǉ��I��
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            For Each cfParam In m_cfDelButuriUFParameterCollection

                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABJutogaiEntity.PREFIX_KEY.RLength) = ABJutogaiEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollection(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABJutogaiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                End If
            Next cfParam


            '*����ԍ� 000005 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strUpdateSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection) + "�z")
            '*����ԍ� 000005 2003/08/28 �C���I��

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollection)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

        Return intDelCnt

    End Function

    '* corresponds to VS2008 Start 2010/04/16 000013
    ''* ����ԍ� 000011 2005/06/16 �폜�J�n
    '''''************************************************************************************************
    '''''* ���\�b�h��     SQL���̍쐬
    '''''* 
    '''''* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '''''* 
    '''''* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '''''* 
    '''''* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '''''* 
    '''''* �߂�l         �Ȃ�
    '''''************************************************************************************************
    ''''Private Sub CreateSQL(ByVal csDataRow As DataRow)

    ''''    Const THIS_METHOD_NAME As String = "CreateSQL"
    ''''    Dim csDataColumn As DataColumn
    ''''    Dim strInsertColumn As String                       'INSERT�p�J����
    ''''    Dim strInsertParam As String
    ''''    Dim cfUFParameterClass As UFParameterClass
    ''''    Dim strUpdateWhere As String
    ''''    Dim strUpdateParam As String
    ''''    Dim csDelRonriSQL As New StringBuilder()            '�_���폜�pSQL

    ''''    Try
    ''''        ' �f�o�b�O���O�o��
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''        ' SELECT SQL���̍쐬
    ''''        m_strInsertSQL = "INSERT INTO " + ABJutogaiEntity.TABLE_NAME + " "
    ''''        strInsertColumn = ""
    ''''        strInsertParam = ""

    ''''        ' UPDATE SQL���̍쐬
    ''''        m_strUpdateSQL = "UPDATE " + ABJutogaiEntity.TABLE_NAME + " SET "
    ''''        strUpdateParam = ""
    ''''        strUpdateWhere = ""

    ''''        ' �_��DELETE SQL���̍쐬
    ''''        csDelRonriSQL.Append("UPDATE ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.TABLE_NAME)
    ''''        csDelRonriSQL.Append(" SET ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.TANMATSUID)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_TANMATSUID)
    ''''        csDelRonriSQL.Append(", ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.SAKUJOFG)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_SAKUJOFG)
    ''''        csDelRonriSQL.Append(", ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINCOUNTER)
    ''''        csDelRonriSQL.Append(", ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINNICHIJI)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINNICHIJI)
    ''''        csDelRonriSQL.Append(", ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINUSER)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINUSER)
    ''''        csDelRonriSQL.Append(" WHERE ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.JUMINCD)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KEY_JUMINCD)
    ''''        csDelRonriSQL.Append(" AND ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
    ''''        csDelRonriSQL.Append(" = ")
    ''''        csDelRonriSQL.Append(ABJutogaiEntity.KEY_KOSHINCOUNTER)


    ''''        ' ����DELETE SQL���̍쐬
    ''''        m_strDelButuriSQL = "DELETE FROM " + ABJutogaiEntity.TABLE_NAME + " WHERE " + _
    ''''                         ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " + _
    ''''                         ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER

    ''''        ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
    ''''        m_cfInsertUFParameterCollection = New UFParameterCollectionClass()

    ''''        ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfUpdateUFParameterCollection = New UFParameterCollectionClass()

    ''''        ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfDelRonriUFParameterCollection = New UFParameterCollectionClass()

    ''''        ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfDelButuriUFParameterCollection = New UFParameterCollectionClass()

    ''''        ' �f�o�b�O���O�o��
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, "UFParameterCollectionClass End")


    ''''        ' �p�����[�^�R���N�V�����̍쐬
    ''''        For Each csDataColumn In csDataRow.Table.Columns
    ''''            cfUFParameterClass = New UFParameterClass()

    ''''            ' INSERT SQL���̍쐬
    ''''            strInsertColumn += csDataColumn.ColumnName + ", "
    ''''            strInsertParam += ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    ''''            ' UPDATE SQL���̍쐬
    ''''            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    ''''            ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
    ''''            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfInsertUFParameterCollection.Add(cfUFParameterClass)

    ''''            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    ''''            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

    ''''        Next csDataColumn

    ''''        ' INSERT SQL���̃g���~���O
    ''''        strInsertColumn = strInsertColumn.Trim()
    ''''        strInsertColumn = strInsertColumn.Trim(CType(",", Char))
    ''''        strInsertParam = strInsertParam.Trim()
    ''''        strInsertParam = strInsertParam.Trim(CType(",", Char))

    ''''        m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

    ''''        ' UPDATE SQL���̃g���~���O
    ''''        m_strUpdateSQL = m_strUpdateSQL.Trim()
    ''''        m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

    ''''        ' UPDATE SQL����WHERE��̒ǉ�
    ''''        m_strUpdateSQL += " WHERE " + ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " + _
    ''''                                      ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER


    ''''        ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
    ''''        m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
    ''''        m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

    ''''        ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_TANMATSUID
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_SAKUJOFG
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINNICHIJI
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINUSER
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

    ''''        ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
    ''''        m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)


    ''''        '�p�����[�^�ϐ��֊i�[
    ''''        m_strDelRonriSQL = csDelRonriSQL.ToString

    ''''        ' �f�o�b�O���O�o��
    ''''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''    Catch objAppExp As UFAppException    ' UFAppException���L���b�`
    ''''        ' ���[�j���O���O�o��
    ''''        m_cfLogClass.WarningWrite(m_cfControlData, _
    ''''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''''                                    "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
    ''''                                    "�y���[�j���O���e:" + objAppExp.Message + "�z")
    ''''        ' �G���[�����̂܂܃X���[����
    ''''        Throw objAppExp

    ''''    Catch objExp As Exception
    ''''        ' �G���[���O�o��
    ''''        m_cfLogClass.ErrorWrite(m_cfControlData, _
    ''''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''''                                    "�y�G���[���e:" + objExp.Message + "�z")
    ''''        ' �V�X�e���G���[���X���[����
    ''''        Throw objExp

    ''''    End Try

    ''''End Sub
    ''* ����ԍ� 000011 2005/06/16 �폜�I��
    '* corresponds to VS2008 End 2010/04/16 000013
    '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
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

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABJutogaiEntity.TABLE_NAME + " "
            csInsertColumn = New StringBuilder()
            csInsertParam = New StringBuilder()

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollection = New UFParameterCollectionClass()

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL���̍쐬
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")
                csInsertParam.Append(ABJutogaiEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollection.Add(cfUFParameterClass)

            Next csDataColumn

            '�Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL += "(" + csInsertColumn.ToString.Trim().Trim(CType(",", Char)) + ")" _
                    + " VALUES (" + csInsertParam.ToString.Trim().TrimEnd(CType(",", Char)) + ")"

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

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
        Dim cfUFParameterClass As UFParameterClass
        Dim csUpdateParam As StringBuilder                  'UPDATE�pSQL��`

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABJutogaiEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder()

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollection = New UFParameterCollectionClass()

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�Z���b�c�E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If Not (csDataColumn.ColumnName = ABJutogaiEntity.JUMINCD) AndAlso
                    Not (csDataColumn.ColumnName = ABJutogaiEntity.SAKUSEIUSER) AndAlso
                     Not (csDataColumn.ColumnName = ABJutogaiEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass()

                    ' UPDATE SQL���̍쐬
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL = m_strUpdateSQL.ToString.Trim()
            m_strUpdateSQL = m_strUpdateSQL.ToString.Trim(CType(",", Char))

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += " WHERE " + ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " +
                                          ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER

            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollection.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

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
        Dim cfUFParameterClass As UFParameterClass
        Dim csDelRonriSQL As New StringBuilder()            '�_���폜�pSQL

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �_��DELETE SQL���̍쐬
            csDelRonriSQL.Append("UPDATE ")
            csDelRonriSQL.Append(ABJutogaiEntity.TABLE_NAME)
            csDelRonriSQL.Append(" SET ")
            csDelRonriSQL.Append(ABJutogaiEntity.TANMATSUID)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_TANMATSUID)
            csDelRonriSQL.Append(", ")
            csDelRonriSQL.Append(ABJutogaiEntity.SAKUJOFG)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_SAKUJOFG)
            csDelRonriSQL.Append(", ")
            csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINCOUNTER)
            csDelRonriSQL.Append(", ")
            csDelRonriSQL.Append(ABJutogaiEntity.KOSHINNICHIJI)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINNICHIJI)
            csDelRonriSQL.Append(", ")
            csDelRonriSQL.Append(ABJutogaiEntity.KOSHINUSER)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.PARAM_KOSHINUSER)
            csDelRonriSQL.Append(" WHERE ")
            csDelRonriSQL.Append(ABJutogaiEntity.JUMINCD)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.KEY_JUMINCD)
            csDelRonriSQL.Append(" AND ")
            csDelRonriSQL.Append(ABJutogaiEntity.KOSHINCOUNTER)
            csDelRonriSQL.Append(" = ")
            csDelRonriSQL.Append(ABJutogaiEntity.KEY_KOSHINCOUNTER)

            ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollection = New UFParameterCollectionClass()

            ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollection.Add(cfUFParameterClass)

            '�p�����[�^�ϐ��֊i�[
            m_strDelRonriSQL = csDelRonriSQL.ToString

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
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
        Const THIS_METHOD_NAME As String = "CreateDeleteButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ����DELETE SQL���̍쐬
            m_strDelButuriSQL = "DELETE FROM " + ABJutogaiEntity.TABLE_NAME + " WHERE " +
                             ABJutogaiEntity.JUMINCD + " = " + ABJutogaiEntity.KEY_JUMINCD + " AND " +
                             ABJutogaiEntity.KOSHINCOUNTER + " = " + ABJutogaiEntity.KEY_KOSHINCOUNTER

            ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelButuriUFParameterCollection = New UFParameterCollectionClass()

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABJutogaiEntity.KEY_KOSHINCOUNTER
            m_cfDelButuriUFParameterCollection.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

    End Sub
    '* ����ԍ� 000011 2005/06/16 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
    '* 
    '* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           strColumnName As String : �Z�o�O�}�X�^�f�[�^�Z�b�g�̍��ږ�
    '*                strValue As String     : ���ڂɑΉ�����l
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Const TABLENAME As String = "�Z�o�O�D"
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����


        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'")

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()

                Case ABJutogaiEntity.JUMINCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KYUSHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.STAICD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_STAICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.ATENADATAKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ATENADATAKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.ATENADATASHU
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ATENADATASHU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEARCHKANASEIMEI
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i����", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEARCHKANASEI
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i��", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEARCHKANAMEI
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i��", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANAMEISHO1
                    '*����ԍ� 000008 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000008 2003/10/30 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANAMEISHO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANJIMEISHO1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIMEISHO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANAMEISHO2
                    '*����ԍ� 000008 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000008 2003/10/30 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANAMEISHO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANJIMEISHO2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIMEISHO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.UMAREYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_UMAREYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABJutogaiEntity.UMAREWMD               '���a��N����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾(�������ړ��͂̌��ł��B�F)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "���a��N����", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEIBETSUCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SEIBETSUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SEIBETSU
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SEIBETSU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.ZOKUGARACD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ZOKUGARACD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.ZOKUGARA
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_ZOKUGARA)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.DAI2ZOKUGARACD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_DAI2ZOKUGARACD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.DAI2ZOKUGARA
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_DAI2ZOKUGARA)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANJIHJNDAIHYOSHSHIMEI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.HANYOKB1
                    '*����ԍ� 000009 2004/05/13 �C���J�n
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'If (Not UFStringClass.CheckNumber(strValue)) Then
                        '*����ԍ� 000009 2004/05/13 �C���J�n
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_HANYOKB1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANJIHJNKEITAI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANJIHJNKEITAI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KJNHJNKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KJNHJNKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.HANYOKB2
                    '*����ԍ� 000009 2004/05/13 �C���J�n
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        'If (Not UFStringClass.CheckNumber(strValue)) Then
                        '*����ԍ� 000009 2004/05/13 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_HANYOKB2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KANNAIKANGAIKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KANNAIKANGAIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KAOKUSHIKIKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KAOKUSHIKIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BIKOZEIMOKU
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BIKOZEIMOKU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.YUBINNO                '�X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�X�֔ԍ�", objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.JUSHOCD
                    '*����ԍ� 000010 2005/01/15 �C���J�n
                    'If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*����ԍ� 000010 2005/01/15 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_JUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.JUSHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BANCHICD1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHICD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BANCHICD2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHICD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BANCHICD3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHICD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.BANCHI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_BANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KATAGAKIFG
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KATAGAKIFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KATAGAKICD
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KATAGAKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KATAGAKI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.RENRAKUSAKI1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_RENRAKUSAKI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.RENRAKUSAKI2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_RENRAKUSAKI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.GYOSEIKUCD
                    '* ����ԍ� 000012 2005/12/26 �C���J�n
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* ����ԍ� 000012 2005/12/26 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_GYOSEIKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.GYOSEIKUMEI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_GYOSEIKUMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUCD1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUCD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUMEI1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUMEI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUCD2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUCD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUMEI2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUMEI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUCD3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUCD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.CHIKUMEI3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_CHIKUMEI3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.TOROKUIDOYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_TOROKUIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABJutogaiEntity.TOROKUJIYUCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_TOROKUJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SHOJOIDOYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SHOJOIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABJutogaiEntity.SHOJOJIYUCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SHOJOJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.RESERVE
                        '�`�F�b�N�Ȃ�

                Case ABJutogaiEntity.TANMATSUID
                    '* ����ԍ� 000006 2003/09/11 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000006 2003/09/11 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SAKUJOFG
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KOSHINCOUNTER
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SAKUSEINICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.SAKUSEIUSER
                    '* ����ԍ� 000007 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000007 2003/10/09 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KOSHINNICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABJutogaiEntity.KOSHINUSER
                    '* ����ԍ� 000007 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000007 2003/10/09 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABJUTOGAIB_RDBDATATYPE_KOSHINUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

            End Select

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

    End Sub

#End Region

End Class