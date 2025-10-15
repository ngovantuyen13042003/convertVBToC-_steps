'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�����ݐ�_�W���}�X�^�c�`(ABAtenaRuiseki_HyojunBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2023/08/14 ����  �Y��
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
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Data
Imports System.Text

'************************************************************************************************
'*
'* �����ݐ�_�W���}�X�^�擾���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABAtenaRuiseki_HyojunBClass
#Region "�����o�ϐ�"
    ' �p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                                              ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                                        ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass                                ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                                              ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                                          ' �G���[�����N���X
    Private m_strInsertSQL As String                                                ' INSERT�pSQL
    Private m_strUpdateSQL As String                                                ' UPDATE�pSQL
    Private m_strDelRonriSQL As String                                              ' �_���폜�pSQL
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      ' SELECT�p�p�����[�^�R���N�V����
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      ' INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      ' UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    ' �_���폜�p�p�����[�^�R���N�V����
    Private m_csDataSchma As DataSet                                                ' �X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_strUpdateDatetime As String                                           ' �X�V����

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaRuiseki_HyojunBClass"         ' �N���X��
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
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing

    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �����ݐ�_�W���}�X�^���o
    '* 
    '* �\��           Public Function GetABAtenaRuisekiHyojunBClassBHoshu(ByVal strJuminCD As String, _
    '*                                                                    ByVal strRirekiNO As String, _
    '*                                                                    ByVal strShoriNichiji As String, _
    '*                                                                    ByVal strZengoKB As String) As DataSet
    '* 
    '* �@�\�@�@    �@ �����ݐ�_�W���}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD         : �Z���R�[�h 
    '*                strRirekiNO        : ����ԍ�
    '*                strShoriNichiji    : ��������
    '*                strZengoKB         : �O��敪
    '* 
    '* �߂�l         DataSet : �擾��������_�W���}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Function GetABAtenaRuisekiHyojunBClassBHoshu(ByVal strJuminCD As String,
                                                        ByVal strRirekiNO As String,
                                                        ByVal strShoriNichiji As String,
                                                        ByVal strZengoKB As String) As DataSet

        Const THIS_METHOD_NAME As String = "GetABAtenaRuisekiHyojunBClassBHoshu"
        Dim cfErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaEntity As DataSet
        Dim csSQL As New StringBuilder()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            ' �Z���R�[�h���w�肳��Ă��Ȃ��Ƃ��G���[
            If (IsNothing(strJuminCD) OrElse (strJuminCD.Trim.RLength = 0)) Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                cfErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                ' ��O�𐶐�
                Throw New UFAppException(cfErrorStruct.m_strErrorMessage + ERR_JUMINCD, cfErrorStruct.m_strErrorCode)
            Else
                '�����Ȃ�
            End If

            ' SELECT��̐���
            csSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            csSQL.AppendFormat(" FROM {0} ", ABAtenaRuisekiHyojunEntity.TABLE_NAME)
            ' �ް����ς̎擾
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(csSQL.ToString(), ABAtenaRuisekiHyojunEntity.TABLE_NAME, False)
            End If

            ' WHERE��̍쐬
            csSQL.Append(Me.CreateWhere(strJuminCD, strRirekiNO, strShoriNichiji, strZengoKB))

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:GetDataSet�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                                csSQL.ToString(), m_cfSelectUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csAtenaEntity = m_csDataSchma.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(csSQL.ToString(), csAtenaEntity,
                                                    ABAtenaRuisekiHyojunEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

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
            csSELECT.AppendFormat("SELECT {0}", ABAtenaRuisekiHyojunEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUMINJUTOGAIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RIREKINO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHORINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.ZENGOKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.EDANO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIMEIKANAKAKUNINFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.UMAREBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOUMAREBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JIJITSUSTAINUSMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KANAKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.BANCHIEDABANSUCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUSHO_KUNIMEICODE)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUSHO_KUNIMEITO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUSHO_KOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_SHIKUGUNCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HON_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CKINIDOWMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CKINIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOCKINIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOROKUIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOTOROKUIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HYOJUNKISAIJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KISAIYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KISAIBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOKISAIBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUTEIIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOJUTEIIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HYOJUNSHOJOJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOKUSEKISOSHITSUBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHOJOIDOWMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHOJOIDOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOSHOJOIDOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_KOKUSEKICD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_KOKUSEKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_YUBINNO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_SHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_MACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_BANCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUTJ_KATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_TODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_SHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_MACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_BANCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAISHUJ_KATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTITODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TENSHUTSUKKTIMACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KAISEIBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOKAISEIBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KAISEISHOJOYMD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KAISEISHOJOBIFUSHOPTN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOKAISEISHOJOBI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD4)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD5)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD6)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD7)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD8)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD9)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.CHIKUCD10)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOKUBETSUYOSHIKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.IDOKB)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.NYURYOKUBASHOCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.NYURYOKUBASHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHKANJIKYUUJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SEARCHKANAKYUUJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KYUUJIKANAKAKUNINFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TDKDSHIMEI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.HYOJUNIDOJIYUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.NICHIJOSEIKATSUKENIKICD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOROKUBUSHO)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TANKITAIZAISHAFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KYOYUNINZU)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHIZEIJIMUSHOCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHUKKOKUKIKAN_ST)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHUKKOKUKIKAN_ED)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.IDOSHURUI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SHOKANKUCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TOGOATENAFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOUMAREBI_DATE)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOCKINIDOBI_DATE)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.FUSHOSHOJOIDOBI_DATE)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKISHIKUCHOSONCD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIMACHIAZACD)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKITODOFUKEN)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKISHIKUCHOSON)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIMACHIAZA)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIKANAKATAGAKI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD4)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD5)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD6)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD7)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD8)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD9)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKICHIKUCD10)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.JUKIBANCHIEDABANSUCHI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE1)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE2)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE3)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE4)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.RESERVE5)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.TANMATSUID)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAKUJOFG)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.SAKUSEIUSER)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI)
            csSELECT.AppendFormat(", {0}", ABAtenaRuisekiHyojunEntity.KOSHINUSER)

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

        Return csSELECT.ToString

    End Function

    '************************************************************************************************
    '* ���\�b�h��     WHERE���̍쐬
    '* 
    '* �\��           Private Function CreateWhere(ByVal strJuminCD As String, _
    '*                                             ByVal strRirekiNO As String, _
    '*                                             ByVal strShoriNichiji As String, _
    '*                                             ByVal strZengoKB As String) As String
    '* 
    '* �@�\�@�@    �@ WHERE�����쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           strJuminCD         : �Z���R�[�h 
    '*                strRirekiNO        : ����ԍ�
    '*                strShoriNichiji    : ��������
    '*                strZengoKB         : �O��敪
    '*
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Function CreateWhere(ByVal strJuminCD As String,
                                 ByVal strRirekiNO As String,
                                 ByVal strShoriNichiji As String,
                                 ByVal strZengoKB As String) As String

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
            csWHERE.AppendFormat("WHERE {0} = {1}", ABAtenaRuisekiHyojunEntity.JUMINCD, ABAtenaRuisekiHyojunEntity.KEY_JUMINCD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ����ԍ�
            If (Not strRirekiNO.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiHyojunEntity.RIREKINO, ABAtenaRuisekiHyojunEntity.KEY_RIREKINO)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_RIREKINO
                cfUFParameterClass.Value = strRirekiNO
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '�����Ȃ�
            End If

            ' ��������
            If (Not strShoriNichiji.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiHyojunEntity.SHORINICHIJI, ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI
                cfUFParameterClass.Value = strShoriNichiji
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
                '�����Ȃ�
            End If

            ' �O��敪
            If (Not strZengoKB.Trim.Equals(String.Empty)) Then
                csWHERE.AppendFormat(" AND {0} = {1}", ABAtenaRuisekiHyojunEntity.ZENGOKB, ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB
                cfUFParameterClass.Value = strZengoKB
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
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

#Region "�����ݐ�_�W���}�X�^�ǉ�"
    '************************************************************************************************
    '* ���\�b�h��     �����ݐ�_�W���}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �����ݐ�_�W���}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaRuisekiHyojunB"
        Dim cfParam As UFParameterClass
        Dim intInsCnt As Integer                            '�ǉ�����

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If ((m_strInsertSQL Is Nothing) OrElse (m_strInsertSQL = String.Empty) _
                OrElse (m_cfInsertUFParameterCollectionClass Is Nothing)) Then
                Call CreateInsertSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId     '�[���h�c
            csDataRow(ABAtenaRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_OFF                        '�폜�t���O
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER) = KOSHINCOUNTER_DEF              '�X�V�J�E���^
            csDataRow(ABAtenaRuisekiHyojunEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId      '�쐬���[�U�[
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId       '�X�V���[�U�[

            '�쐬�����A�X�V�����̐ݒ�
            Me.SetUpdateDatetime(csDataRow(ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI))
            Me.SetUpdateDatetime(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI))

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(
                    ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                            m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")

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
                strParamName = String.Format("{0}{1}", ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER, csDataColumn.ColumnName)

                ' INSERT SQL���̍쐬
                csInsertColumn.AppendFormat("{0},", csDataColumn.ColumnName)
                csInsertParam.AppendFormat("{0},", strParamName)

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = strParamName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '�Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL = String.Format("INSERT INTO {0}({1}) VALUES ({2})",
                                           ABAtenaRuisekiHyojunEntity.TABLE_NAME,
                                           csInsertColumn.ToString.TrimEnd(",".ToCharArray),
                                           csInsertParam.ToString.TrimEnd(",".ToCharArray))

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

    End Sub
#End Region

#Region "�����ݐ�_�W���}�X�^�X�V"
    '************************************************************************************************
    '* ���\�b�h��     �����ݐ�_�W���}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �����ݐ�_�W���}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csDataRow As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaRuisekiHyojunB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        Dim intUpdCnt As Integer                            '�X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If ((m_strUpdateSQL Is Nothing) OrElse (m_strUpdateSQL = String.Empty) _
                OrElse (m_cfUpdateUFParameterCollectionClass Is Nothing)) Then
                Call CreateUpdateSQL(csDataRow)
            Else
                '�����Ȃ�
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId   '�[���h�c
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER) =
                CDec(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)) + 1                  '�X�V�J�E���^
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId     '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI))

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength) =
                        ABAtenaRuisekiHyojunEntity.PREFIX_KEY) Then

                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()

                    '�L�[���ڈȊO�͕ҏW���e�擾
                Else
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                         = csDataRow(cfParam.ParameterName.RSubstring(
                              ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                                m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")

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
            m_strUpdateSQL = "UPDATE " + ABAtenaRuisekiHyojunEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.SHORINICHIJI)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.ZENGOKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER)

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�Z���b�c�E����ԍ��E���������E�O��敪�E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If (Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.JUMINCD) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.RIREKINO) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.SHORINICHIJI) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.ZENGOKB) AndAlso
                     Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.SAKUSEIUSER) AndAlso
                      Not (csDataColumn.ColumnName = ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI)) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL���̍쐬
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(",")

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

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

    End Sub
#End Region

#Region "�����ݐ�_�W���}�X�^�폜"
    '************************************************************************************************
    '* ���\�b�h��     �����ݐ�_�W���}�X�^�폜
    '* 
    '* �\��           Public Function DeleteAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �����ݐ�_�W���}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csDataRow As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Function DeleteAtenaRuisekiHyojunB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaRuisekiHyojunB"
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
            csDataRow(ABAtenaRuisekiHyojunEntity.TANMATSUID) = m_cfControlData.m_strClientId      '�[���h�c
            csDataRow(ABAtenaRuisekiHyojunEntity.SAKUJOFG) = SAKUJOFG_ON                          '�폜�t���O
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER) =
                CDec(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)) + 1                     '�X�V�J�E���^
            csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINUSER) = m_cfControlData.m_strUserId        '�X�V���[�U�[

            '�X�V�����̐ݒ�
            m_strUpdateDatetime = m_cfRdbClass.GetSystemDate().ToString(FORMAT_UPDATETIME)
            Me.SetUpdateDatetime(csDataRow(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI))

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength) =
                        ABAtenaRuisekiHyojunEntity.PREFIX_KEY) Then

                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                                 csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiHyojunEntity.PREFIX_KEY.RLength),
                                           DataRowVersion.Original).ToString()
                    '�L�[���ڈȊO�͕ҏW���e��ݒ�
                Else
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(
                            ABAtenaRuisekiHyojunEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(
            '                                m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")
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
            csWhere.Append(ABAtenaRuisekiHyojunEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.SHORINICHIJI)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_SHORINICHIJI)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.ZENGOKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_ZENGOKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER)

            ' �_��DELETE SQL���̍쐬
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRuisekiHyojunEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where���̒ǉ�
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_JUMINJUTOGAIKB
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRuisekiHyojunEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

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

    End Sub
#End Region

#Region "�X�V�����ݒ�"
    '************************************************************************************************
    '* ���\�b�h��     �X�V�����ݒ�
    '* 
    '* �\��           Private Sub SetUpdateDatetime(ByRef csDate As Object)
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
            If ((IsDBNull(csDate)) OrElse (CType(csDate, String).Trim.Equals(String.Empty))) Then
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
