'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�����ҏW�N���X(ABAtenaHenshuBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/14�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/02/19 000001     �{�l���t��ҏW�ŁA���t�悪�ҏW����Ȃ��ꍇ������
'* 2003/02/20 000002     �f�[�^���󔒂̏ꍇ�́A����ɕs��
'*                       ���t��f�[�^�}�[�W�����̕ύX
'* 2003/02/21 000003     ���t��f�[�^��ҏW���鎞�A�Ɩ��R�[�h�E�Ɩ�����ʂ́A���t�惌�R�[�h���Z�b�g
'* 2003/02/25 000004     �Z���ҏW3�ŁA3�C4�̏ꍇ�i�j��t������B�A���A�����ꍇ�́A�i�j����
'*                       ������t�����鎞�Ɋ����X�y�[�X���P����ĕt�����Ă��������i�d�l�ύX�j
'* 2003/02/25 000005     ���t�悪���݂��Ȃ��ꍇ�A�Ɩ��R�[�h�E�Ɩ�����ʃR�[�h�� String.Empty �Ƃ���
'* 2003/03/07 000006     �v���W�F�N�g��Imports�͒�`���Ȃ��i�d�l�ύX�j
'* 2003/03/17 000007     �p�����[�^�̃`�F�b�N������
'* 2003/03/17 000008     �Z���ҏW�R�̒l�Ȃ��̍l����ǉ��i�d�l�ύX�j
'* 2003/03/18 000009     �G���[���b�Z�[�W�̕ύX�i�d�l�ύX�j
'* 2003/03/27 000010     �G���[�����N���X�̎Q�Ɛ��"AB"�Œ�ɂ���
'* 2003/04/01 000011     ABAtena1�̃v���C�}���[�L�[���O��
'* 2003/04/18 000012     �������Entity�ɑ����R�[�h�E�����E�J�i���̂Q�E�������̂Q�E�Дԍ���ǉ�
'* 2003/04/18 000013     �N���p�������Entity��ǉ�
'* 2003/04/30 000014     �@�l�̎��A�J�i����2�A�������̂Q�́A�Z�b�g���Ȃ��i�d�l�ύX�j
'* 2003/04/30 000015     �����ҏW���ڂ���������A�ݒ肷��B                      
'* 2003/08/22 000016     �t�q�L���b�V���Ή��^�p���\�N���X�ɕύX
'* 2003/10/09 000017     �A����́A�A����}�X�^�Ƀf�[�^�����݂���ꍇ�́A�����炩��擾����B�A���A�Ɩ��R�[�h���w�肳��Ă��ꍇ�݂̂Ɍ���B
'*                       NenkinAtenaGet��AtenaGet1�Ɠ��l�Ɏw��N�������w�肳�ꂽ��A�����������擾����B�A��������l�B�A���A��[�E���Z�͕s�v�B
'* 2003/10/14 000018     ����ҏW�ŁA�����Q�������ꍇ�A�������ҏW����Ȃ��B
'* 2003/11/19 000019     �����ʏ��ҏW������ǉ�
'* 2003/12/01 000020     �A����Ɩ��R�[�h��ABAtena1����͂����BABNenkinAtena�E�ʈ����ɒǉ�
'* 2003/12/02 000021     �A����擾�E�ҏW�������擾�ֈړ�
'* 2003/12/04 000022     �d�l�ύX�F�N���p�������Entity���ڒǉ��ɔ����ύX
'* 2004/08/27 000023     ���x���P�F�i�{��j
'* 2005/01/25 000024     ���x���P�Q�F�i�{��j
'* 2005/07/14 000025     CheckColumnValue���\�b�h�ł̏Z���ҏW�R�̒l�͈̔͂��C��(�}���S���R)
'* 2005/12/21 000026     �Z���[�\�����̕ҏW�d�l�ύX(����)
'* 2006/07/31 000027     �N�������Q�b�g�U�ǉ�(�g�V)
'* 2007/01/15 000028     �Z���ҏW�p�^�[���ǉ�
'*                       ����ҏW�E�Z��D��ł͂Ȃ��ꍇ�̃R�[�f�B���O�C��
'* 2007/01/25 000029     ���t��ɔԒn�R�[�h��ݒ肷��悤�ɏC��
'* 2007/04/28 000030     ���ň����擾���\�b�h�̒ǉ��ɂ��擾���ڂ̒ǉ� (�g�V)
'* 2007/06/28 000031     DB�������g���Ή��C�������g���ɂƂ��Ȃ��������J������`������єN���p�������J������`��MaxLength�l�C��
'*                       �i�Ή����������ɓn��ׁC����ԍ��t�������j�i����j
'* 2007/07/09 000032     �����񌋍���ɐ؂�l�߂Ă��镶�����̏C���i����j
'* 2007/07/17 000033     �x�X���������ꍇ�́C�@�l���̂Ǝx�X���̌����������s��Ȃ��i����j
'* 2008/01/15 000034     �����ʏ��J�����쐬�Ɍ�������񍀖ڂ�ǉ��i��Áj���l�[�~���O�ύX�i�g�V�j
'* 2008/02/15 000035     �����ȗ������ҏW������ǉ��i��Áj
'* 2008/11/10 000036     �����f�[�^�Z�b�g�̍쐬���ɔ[�Ŏ�ID�E���p��ID��ǉ��i��Áj
'* 2008/11/17 000037     ���t��ҏW���ڂ����������鏈����ǉ��i��Áj
'* 2008/11/18 000038     ����ԍ�:000036�̒ǉ��ɔ������C�i��Áj
'* 2010/04/16 000039     VS2008�Ή��i��Áj
'* 2010/05/14 000040     �{�ЕM���ҋy�я�����~�敪�Ή��i��Áj
'* 2011/05/18 000041     �O���l�ݗ����擾�敪�Ή��i��Áj
'* 2011/05/18 000042     �{���E�ʏ̖��D�搧��Ή��i��Áj
'* 2011/06/23 000043     �{���E�ʏ̖��D�搧��Ή�US�@�\�g�ݍ��݉��C�i��Áj
'* 2011/06/24 000044     ���C�A�E�g�F�N���p�̊O���l�ݗ����̐ݒ�ʒu��ύX�i��Áj
'* 2011/06/27 000045     ���̕ҏW�����Ŗ{���D�揈���̏ꍇ�Ɋ������̂Q�̑��݃`�F�b�N�s���悤�ɉ��C�i��Áj
'* 2011/11/07 000046     �yAB17010�z�Z��@�����ɂ�舶���t���f�[�^���������Ď擾����悤�ɉ��C�i�r�c�j
'* 2012/03/13 000047     �yAB17010-00�z�A�������ɂ��ُ�I������s��C���i�����j
'* 2014/04/28 000048     �yAB21040�z�����ʔԍ��Ή������ʔԍ��ǉ��i�΍��j
'* 2022/12/16 000049     �yAB-8010�z�Z���R�[�h���уR�[�h15���Ή�(����)
'* 2023/03/10 000050     �yAB-0970-1�z����GET�擾���ڕW�����Ή��i�����j
'* 2023/10/19 000051     �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ�_�ǉ��C��(����)
'* 2023/12/22 000020     �yAB-0970-1_2�z����GET���t���ڐݒ�Ή�(����)
'* 2024/06/17 000021     �yAB-9903-1�z�s��Ή�
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
'*����ԍ� 000006  2003/03/07 �폜�J�n
'Imports Densan.Reams.AB.AB001BX
'*����ԍ� 000006  2003/03/07 �폜�I��
Imports System.Data
Imports System.Text
Imports System.Security

Public Class ABAtenaHenshuBClass

#Region " �����o�ϐ� "
    '************************************************************************************************
    '*
    '* �����ҏW�Ɏg�p����p�����[�^�N���X
    '*
    '************************************************************************************************
    '*����ԍ� 000016 2003/08/22 �C���J�n
    ''�p�����[�^�̃����o�ϐ�
    'Private m_cfUFLogClass As UFLogClass                    '���O�o�̓N���X
    'Private m_cfUFControlData As UFControlData              '�R���g���[���f�[�^
    'Private m_cfUFConfigDataClass As UFConfigDataClass      '�R���t�B�O�f�[�^
    'Private m_cfUFRdbClass As UFRdbClass                    '�q�c�a�N���X

    ''�@�R���X�^���g��`
    'Private Const THIS_CLASS_NAME As String = "ABAtenaHenshuBClass"             ' �N���X��
    'Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h
    'Private Const NENKIN As String = "NENKIN"

    '�p�����[�^�̃����o�ϐ�
    Protected m_cfUFLogClass As UFLogClass                                      ' ���O�o�̓N���X
    Protected m_cfUFControlData As UFControlData                                ' �R���g���[���f�[�^
    Protected m_cfUFConfigDataClass As UFConfigDataClass                        ' �R���t�B�O�f�[�^
    Protected m_cfUFRdbClass As UFRdbClass                                      ' �q�c�a�N���X

    '�@�R���X�^���g��`
    Protected Const THIS_CLASS_NAME As String = "ABAtenaHenshuBClass"           ' �N���X��
    Protected Const THIS_BUSINESSID As String = "AB"                            ' �Ɩ��R�[�h
    Protected Const NENKIN As String = "NENKIN"                                 ' �N������
    '*����ԍ� 000027 2006/07/31 �ǉ��J�n
    Protected Const NENKIN_2 As String = "NENKIN_2"                                 ' �N�������p�[�g�U
    '*����ԍ� 000027 2006/07/31 �ǉ��I��
    '*����ԍ� 000016 2003/08/22 �C���I��

    '*����ԍ� 000019 2003/11/19 �ǉ��J�n
    Protected Const KOBETSU As String = "KOBETSU"                               ' �����ʏ�񏈗�
    '*����ԍ� 000019 2003/11/19 �ǉ��I��

    '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
    Private m_cuUSSCityInfo As USSCityInfoClass               '�s�������Ǘ��N���X
    Private m_cABDainoKankeiB As ABDainoKankeiBClass          '��[�֌W�N���X
    Private m_cABJuminShubetsuB As ABJuminShubetsuBClass      '�Z����ʃN���X
    Private m_cABHojinMeishoB As ABHojinMeishoBClass          '�@�l���̃N���X
    Private m_cABKjnhjnKBB As ABKjnhjnKBBClass                '�l�@�l�N���X
    Private m_cABKannaiKangaiKBB As ABKannaiKangaiKBBClass    '�Ǔ��ǊO�N���X
    Private m_cABUmareHenshuB As ABUmareHenshuBClass          '���N�����ҏW�N���X
    Private m_cABCommon As ABCommonClass                      '�������ʃN���X
    Private m_cURKanriJohoB As URKANRIJOHOCacheBClass         '�Ǘ����擾�N���X
    '* ����ԍ� 000023 2004/08/27 �ǉ��I��
    '* �����J�n 000035 2008/02/15 �ǉ��J�n
    Private m_cABMojiHenshuB As ABMojiretsuHenshuBClass       '�����ҏW�a�N���X
    '* �����J�n 000035 2008/02/15 �ǉ��I��
    '*����ԍ� 000042 2011/05/18 �ǉ��J�n
    Private m_cABMeishoSeigyoB As ABMeishoSeigyoBClass        ' ���̐���a�N���X
    '*����ԍ� 000043 2011/06/23 �C���J�n
    Private m_cuUSSUrlParm As USUrlParmClass                  ' USURL�p�����[�^�N���X
    '*����ԍ� 000043 2011/06/23 �C���I��
    '*����ԍ� 000042 2011/05/18 �ǉ��I��
    Private m_cABHyojunkaCdHenshuB As ABHyojunkaCdHenshuBClass    '�W�����R�[�h�ҏW�N���X

    '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
    Protected m_cSofuJushoGyoseikuType As SofuJushoGyoseikuType
    Protected m_bSofuJushoGyoseikuTypeFlg As Boolean = False
    Public m_blnSelectAll As ABEnumDefine.AtenaGetKB = ABEnumDefine.AtenaGetKB.KaniAll
    Private m_strHenshuJusho As StringBuilder = New StringBuilder(200)                        '�ҏW�Z����
    Private m_csOrgAtena1 As DataTable
    Private m_csOrgAtena1Kobetsu As DataTable
    Private m_csOrgAtena1Nenkin As DataTable
    '* ����ԍ� 000024 2005/01/25 �ǉ��I��

    '*����ԍ� 000030 2007/04/28 �ǉ��J�n
    Public m_blnMethodKB As ABEnumDefine.MethodKB  '���\�b�h�敪�i�ʏ�ł��A���ŁA�A�A�j
    '*����ԍ� 000030 2007/04/28 �ǉ��I��

    '*����ԍ� 000034 2008/01/15 �ǉ��J�n
    Private m_strKobetsuShutokuKB As String         ' �����擾�p�����[�^:�ʎ����擾�敪
    '*����ԍ� 000034 2008/01/15 �ǉ��I��

    '*����ԍ� 000036 2008/11/10 �ǉ��J�n
    Private m_strRiyoTdkdKB As String               ' ���p�͏o�擾�敪
    Private m_blnKobetsu As Boolean                 ' �ʎ�������t���O
    '*����ԍ� 000036 2008/11/10 �ǉ��I��

    '*����ԍ� 000040 2010/05/14 �ǉ��J�n
    Private m_strHonsekiHittoshKB_Param As String                   ' �{�ЕM���ҋ敪�p�����[�^
    Private m_strShoriteishiKB_Param As String                      ' ������~�敪�p�����[�^
    Private m_strHonsekiHittoshKB As String = String.Empty          ' �{�ЕM���Ҏ擾�敪(�����Ǘ����)
    Private m_strShoriteishiKB As String = String.Empty             ' ������~�敪�擾�敪(�����Ǘ����)
    Private m_blnNenKin As Boolean = False                          ' �N���Ŕ���t���O
    '*����ԍ� 000040 2010/05/14 �ǉ��I��

    '*����ԍ� 000041 2011/05/18 �ǉ��J�n
    Private m_strFrnZairyuJohoKB_Param As String = String.Empty     ' �O���l�ݗ����擾�敪�p�����[�^
    '*����ԍ� 000041 2011/05/18 �ǉ��I��

    '*����ԍ� 000042 2011/05/18 �ǉ��J�n
    Private m_strHonmyoTsushomeiYusenKB As String = String.Empty    ' �{���ʏ̖��D��ݒ萧��敪(�����Ǘ����)
    '*����ԍ� 000042 2011/05/18 �ǉ��I��
    '*����ԍ� 000046 2011/11/07 �ǉ��J�n
    Private m_strJukiHokaiseiKB_Param As String                     ' �Z��@�����敪
    '*����ԍ� 000046 2011/11/07 �ǉ��I��
    '*����ԍ� 000048 2014/04/28 �ǉ��J�n
    Private m_strMyNumberKB_Param As String = String.Empty          ' ���ʔԍ��擾�敪
    '*����ԍ� 000048 2014/04/28 �ǉ��I��
    '*����ԍ� 000047 2012/03/13 �ǉ��J�n
    Private m_csOrgNenkinKobetsu As DataTable                       ' �N��or�ʂ̎��̕ێ��X�L�[�}
    '*����ԍ� 000047 2012/03/13 �ǉ��I��
    Public m_intHyojunKB As ABEnumDefine.HyojunKB                   ' ����GET�W�����敪
    Private m_csOrgAtena1Hyojun As DataTable
    Private m_csOrgAtena1KobetsuHyojun As DataTable
    Private m_csOrgAtena1NenkinHyojun As DataTable
    Private m_cfDate As UFDateClass
    Private m_strUmareYMDHenkanParam As String
    Private m_strUmareWmdHenkan As String
    Private m_strUmareWmdhenkanSeireki As String
    Private m_strShojoIdobiHenkanParam As String
    Private m_strShojoIdoWmdHenkan As String
    Private m_strCknIdobiHenkanParam As String
    Private m_strCknIdoWmdHenkan As String

#End Region

#Region " �R���X�g���N�^ "
    '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass,
    '*                               ByVal cfUFRdbClass as UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfUFControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfUFConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                cfUFRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfUFControlData As UFControlData,
                   ByVal cfUFConfigDataClass As UFConfigDataClass,
                   ByVal cfUFRdbClass As UFRdbClass)
        Initial(cfUFControlData, cfUFConfigDataClass, cfUFRdbClass, ABEnumDefine.AtenaGetKB.KaniAll)
    End Sub
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass,
    '*                               ByVal cfUFRdbClass as UFRdbClass)
    '* �@�@                          ByVal blnSelectAll as boolean)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfUFControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfUFConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                cfUFRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* �@�@           ByVal blnSelectAll As Boolean           : True�̏ꍇ�S���ځAFalse�̏ꍇ�ȈՍ��ڂ̂ݎ擾
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfUFControlData As UFControlData,
                   ByVal cfUFConfigDataClass As UFConfigDataClass,
                   ByVal cfUFRdbClass As UFRdbClass,
                   ByVal blnSelectAll As ABEnumDefine.AtenaGetKB)
        Initial(cfUFControlData, cfUFConfigDataClass, cfUFRdbClass, blnSelectAll)
    End Sub
    '* ����ԍ� 000024 2005/01/25 �ǉ��I��
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass,
    '*                               ByVal cfUFRdbClass as UFRdbClass)
    '* �@�@                          ByVal blnSelectAll as boolean)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfUFControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfUFConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                cfUFRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* �@�@           ByVal blnSelectAll As Boolean           : True�̏ꍇ�S���ځAFalse�̏ꍇ�ȈՍ��ڂ̂ݎ擾
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
    'Public Sub New(ByVal cfUFControlData As UFControlData, _
    '               ByVal cfUFConfigDataClass As UFConfigDataClass, _
    '               ByVal cfUFRdbClass As UFRdbClass)
    <SecuritySafeCritical>
    Public Sub Initial(ByVal cfUFControlData As UFControlData,
                   ByVal cfUFConfigDataClass As UFConfigDataClass,
                   ByVal cfUFRdbClass As UFRdbClass,
                   ByVal blnSelectAll As ABEnumDefine.AtenaGetKB)
        '* ����ԍ� 000024 2005/01/25 �X�V�I��

        '�����o�ϐ��Z�b�g
        m_cfUFControlData = cfUFControlData
        m_cfUFConfigDataClass = cfUFConfigDataClass
        m_cfUFRdbClass = cfUFRdbClass

        '���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(cfUFConfigDataClass, cfUFControlData.m_strBusinessId)

        '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
        ''�s�������̃C���X�^���X�쐬
        m_cuUSSCityInfo = New USSCityInfoClass()
        m_cuUSSCityInfo.GetCityInfo(m_cfUFControlData)

        ''��[�֌W�̃C���X�^���X�쐬
        m_cABDainoKankeiB = New ABDainoKankeiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)

        ''�Z����ʂ̃C���X�^���X�쐬
        m_cABJuminShubetsuB = New ABJuminShubetsuBClass(m_cfUFControlData, m_cfUFConfigDataClass)

        ''�@�l���̂̃C���X�^���X�쐬
        m_cABHojinMeishoB = New ABHojinMeishoBClass(m_cfUFControlData, m_cfUFConfigDataClass)

        ''�l�@�l�̃C���X�^���X�쐬
        m_cABKjnhjnKBB = New ABKjnhjnKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

        ''�Ǔ��ǊO�̃C���X�^���X�쐬
        m_cABKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

        ''���N�����ҏW�̃C���X�^���X�쐬
        m_cABUmareHenshuB = New ABUmareHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass)

        m_cABCommon = New ABCommonClass()

        '�Ǘ����擾�a�̃C���X�^���X�쐬
        m_cURKanriJohoB = New URKANRIJOHOCacheBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
        '* ����ԍ� 000023 2004/08/27 �ǉ��J�n

        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        m_blnSelectAll = blnSelectAll
        '* ����ԍ� 000024 2005/01/25 �ǉ��I��

        '* ����ԍ� 000035 2008/02/15 �ǉ��J�n
        m_cABMojiHenshuB = New ABMojiretsuHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass)
        '* ����ԍ� 000035 2008/02/15 �ǉ��I��

        '*����ԍ� 000040 2010/05/14 �ǉ��J�n
        '�Ǘ����擾����
        Call GetKanriJoho()
        '*����ԍ� 000040 2010/05/14 �ǉ��I��

        ''�W�����R�[�h�ҏW�̃C���X�^���X�쐬
        m_cABHyojunkaCdHenshuB = New ABHyojunkaCdHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass)

    End Sub


    '*����ԍ� 000040 2010/05/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��       �Ǘ����擾
    '* 
    '* �\��             Private Function GetKanriJoho()
    '* 
    '* �@�\�@�@    �@   �Ǘ������擾����
    '* 
    '* ����             �Ȃ�
    '* 
    '* �߂�l           �Ȃ�
    '************************************************************************************************
    Private Sub GetKanriJoho()
        Const THIS_METHOD_NAME As String = "GetKanriJoho"
        Dim cABAtenaKanriJoho As ABAtenaKanriJohoBClass

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            If (cABAtenaKanriJoho Is Nothing) Then
                cABAtenaKanriJoho = New ABAtenaKanriJohoBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            End If

            ' �{�Ў擾�敪�擾
            m_strHonsekiHittoshKB = cABAtenaKanriJoho.GetHonsekiKB_Param

            ' ������~�敪�擾�敪�擾
            m_strShoriteishiKB = cABAtenaKanriJoho.GetShoriteishiKB_Param

            '*����ԍ� 000042 2011/05/18 �ǉ��J�n
            ' �{���ʏ̖��D��ݒ萧��擾
            m_strHonmyoTsushomeiYusenKB = cABAtenaKanriJoho.GetHonmyoTsushomeiYusenKB_Param
            '*����ԍ� 000042 2011/05/18 �ǉ��I��

            If (IsNothing(m_cfDate)) Then
                m_cfDate = New UFDateClass(m_cfUFConfigDataClass)
                m_cfDate.p_enDateSeparator = UFDateSeparator.None
            End If
            m_strUmareYMDHenkanParam = cABAtenaKanriJoho.GetUmareYMDHenkanHizuke_Param
            m_cfDate.p_strDateValue = m_strUmareYMDHenkanParam
            m_strUmareWmdHenkan = m_cfDate.p_strWarekiYMD
            If (m_strUmareYMDHenkanParam.Trim.RLength >= 8) Then
                m_strUmareWmdhenkanSeireki = m_strUmareYMDHenkanParam.RSubstring(1, 7)
            Else
                m_strUmareWmdhenkanSeireki = String.Empty
            End If

            m_strShojoIdobiHenkanParam = cABAtenaKanriJoho.GetShojoIdobiHenkanHizuke_Param
            m_cfDate.p_strDateValue = m_strShojoIdobiHenkanParam
            m_strShojoIdoWmdHenkan = m_cfDate.p_strWarekiYMD

            m_strCknIdobiHenkanParam = cABAtenaKanriJoho.GetCknIdobiHenkanHizuke_Param
            m_cfDate.p_strDateValue = m_strCknIdobiHenkanParam
            m_strCknIdoWmdHenkan = m_cfDate.p_strWarekiYMD

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

    End Sub
    '*����ԍ� 000040 2010/05/14 �ǉ��I��
#End Region

#Region " �����ҏW(AtenaHenshu) "
    '************************************************************************************************
    '* ���\�b�h��     �����ҏW
    '* 
    '* �\��           Public Function AtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1, 
    '*                                           ByVal csAtenaEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@�ҏW�����f�[�^���쐬����
    '* 
    '* ����           cAtenaGetPara1     : �����擾�p�����[�^
    '*               csAtenaEntity      : �����f�[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Overloads Function AtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                          ByVal csAtenaEntity As DataSet) As DataSet

        '*����ԍ� 000013 2003/04/18 �ǉ��J�n
        'Return Me.AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "")
        Return Me.AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "", "")
        '*����ԍ� 000013 2003/04/18 �ǉ��I��
    End Function

    '*����ԍ� 000013 2003/04/18 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����ҏW
    '* 
    '* �\��           Public Function AtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                           ByVal csAtenaEntity As DataSet, 
    '*                                           ByVal strDainoKB As String,
    '*                                           ByVal strGyomuCD As String,
    '*                                           ByVal strGyomunaiSHU_CD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�ҏW�����f�[�^���쐬����
    '* 
    '* ����         cAtenaGetPara1      : �����擾�p�����[�^
    '* �@�@         csAtenaEntity       : �����f�[�^
    '* �@�@         strDainoKB          : ��[�敪
    '* �@�@         strGyomuCD          : �Ɩ��R�[�h
    '* �@�@         strGyomunaiSHU_CD   : �Ɩ�����ʃR�[�h
    '* 
    '* �߂�l       DataSet(ABAtena1)   : �擾�����������
    '************************************************************************************************
    Public Overloads Function AtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                          ByVal csAtenaEntity As DataSet,
                                          ByVal strDainoKB As String,
                                          ByVal strGyomuCD As String,
                                          ByVal strGyomunaiSHU_CD As String) As DataSet
        Return Me.AtenaHenshu(cAtenaGetPara1, csAtenaEntity, strDainoKB, strGyomuCD, strGyomunaiSHU_CD, "")
    End Function
    '************************************************************************************************
    '* ���\�b�h��     �����ҏW
    '* 
    '* �\��           Public Function AtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                           ByVal csAtenaEntity As DataSet, 
    '*                                           ByVal strDainoKB As String,
    '*                                           ByVal strGyomuCD As String,
    '*                                           ByVal strGyomunaiSHU_CD As String,
    '*                                           ByVal strGyomuMei As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�ҏW�����f�[�^���쐬����
    '* 
    '* ����         cAtenaGetPara1      : �����擾�p�����[�^(ABAtenaGetPara1XClass)
    '*              csAtenaEntity       : �����f�[�^(ABAtenaEntity)
    '*              strDainoKB          : ��[�敪
    '*              strGyomuCD          : �Ɩ��R�[�h
    '*              strGyomunaiSHU_CD   : �Ɩ�����ʃR�[�h
    '*              strGyomuMei         : �Ɩ���
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Private Overloads Function AtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                          ByVal csAtenaEntity As DataSet,
                                          ByVal strDainoKB As String,
                                          ByVal strGyomuCD As String,
                                          ByVal strGyomunaiSHU_CD As String,
                                          ByVal strGyomuMei As String) As DataSet
        '*����ԍ� 000013 2003/04/18 �ǉ��I��
        Const THIS_METHOD_NAME As String = "AtenaHenshu"
        'Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 Start 2010/04/16 000039
        'Dim csDataSet As DataSet
        '* corresponds to VS2008 End 2010/04/16 000039
        Dim csDataTable As DataTable
        Dim csDataRow As DataRow
        Dim csAtena1 As DataSet                             '�������(ABAtena1)
        Dim csDataNewRow As DataRow
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cuUSSCityInfo As USSCityInfoClass               '�s�������Ǘ��N���X
        'Dim cABDainoKankeiB As ABDainoKankeiBClass          '��[�֌W�N���X
        'Dim cABJuminShubetsuB As ABJuminShubetsuBClass      '�Z����ʃN���X
        'Dim cABHojinMeishoB As ABHojinMeishoBClass          '�@�l���̃N���X
        'Dim cABKjnhjnKBB As ABKjnhjnKBBClass                '�l�@�l�N���X
        'Dim cABKannaiKangaiKBB As ABKannaiKangaiKBBClass    '�Ǔ��ǊO�N���X
        'Dim cABUmareHenshuB As ABUmareHenshuBClass          '���N�����ҏW�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        'Dim csDainoKankeiCDMSTEntity As DataSet             '��[�֌WDataSet
        Dim csDainoKankeiCDMSTEntity As DataRow()             '��[�֌WDataRow()
        '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j

        '* ����ԍ� 000024 2005/01/25 �폜�J�n�i�{��j
        'Dim strHenshuJusho As String                        '�ҏW�Z����
        '* ����ԍ� 000024 2005/01/25 �폜�I��

        Dim strHenshuKanaMeisho As String                   '�ҏW�J�i����
        Dim strHenshuKanjiShimei As String                  '�ҏW��������
        '*����ԍ� 000008 2003/03/17 �ǉ��J�n
        '*����ԍ� 000016 2003/08/22 �폜�J�n
        'Dim cURKanriJohoB As URKANRIJOHOBClass              '�Ǘ����擾�N���X
        '*����ԍ� 000016 2003/08/22 �폜�I��
        Dim cSofuJushoGyoseikuType As SofuJushoGyoseikuType
        Dim strJushoHenshu3 As String                       '�Z���ҏW�R
        Dim strJushoHenshu4 As String                       '�Z���ҏW�S
        '*����ԍ� 000008 2003/03/17 �ǉ��I��
        '*����ԍ� 000015 2003/04/30 �ǉ��J�n
        Dim csColumn As DataColumn
        '*����ԍ� 000015 2003/04/30 �ǉ��I��

        '*����ԍ� 000021 2003/12/02 �폜�J�n
        ''*����ԍ� 000017 2003/10/09 �ǉ��J�n
        'Dim cRenrakusakiBClass As ABRenrakusakiBClass       ' �A����a�N���X
        'Dim csRenrakusakiEntity As DataSet                  ' �A����DataSet
        'Dim csRenrakusakiRow As DataRow                     ' �A����Row
        ''*����ԍ� 000017 2003/10/09 �ǉ��I��
        '*����ԍ� 000021 2003/12/02 �폜�I��

        '* ����ԍ� 000026 2005/12/21 �ǉ��J�n
        Dim strWork As String
        '* ����ԍ� 000026 2005/12/21 �ǉ��I��
        '*����ԍ� 000042 2011/05/18 �ǉ��J�n
        Dim strMeisho(1) As String                          ' �{���ʏ̖��D�搧��p
        '*����ԍ� 000042 2011/05/18 �ǉ��I��
        Dim strAtenaDataKB As String
        Dim strAtenaDataSHU As String


        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ''�G���[�����N���X�̃C���X�^���X�쐬
            ''*����ԍ� 000010  2003/03/27 �C���J�n
            ''cfErrorClass = New UFErrorClass(m_cfUFControlData.m_strBusinessId)
            'cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            ''*����ԍ� 000010  2003/03/27 �C���I��

            '*����ԍ� 000013 2003/04/18 �C���J�n
            ''�J�������쐬
            'csDataTable = Me.CreateAtena1Columns()
            'csAtena1 = New DataSet()
            'csAtena1.Tables.Add(csDataTable)

            '*����ԍ� 000019 2003/11/19 �C���J�n
            ''�J�������쐬
            'If (strGyomuMei = NENKIN) Then
            '    csDataTable = Me.CreateNenkinAtenaColumns()
            'Else
            '    csDataTable = Me.CreateAtena1Columns()
            'End If

            '*����ԍ� 000036 2008/11/10 �ǉ��J�n
            ' ���p�͏o�擾�敪��ϐ��ɃZ�b�g()
            m_strRiyoTdkdKB = cAtenaGetPara1.p_strTdkdKB
            '*����ԍ� 000036 2008/11/10 �ǉ��I��

            '*����ԍ� 000040 2010/05/14 �ǉ��J�n
            ' �{�ЕM���ҋ敪�p�����[�^�ɕϐ����Z�b�g
            m_strHonsekiHittoshKB_Param = cAtenaGetPara1.p_strHonsekiHittoshKB

            ' ������~�敪�p�����[�^�ɕϐ����Z�b�g
            m_strShoriteishiKB_Param = cAtenaGetPara1.p_strShoriTeishiKB
            '*����ԍ� 000040 2010/05/14 �ǉ��I��

            '*����ԍ� 000041 2011/05/18 �ǉ��J�n
            '�O���l�ݗ����擾�敪�p�����[�^�ɕϐ����Z�b�g
            m_strFrnZairyuJohoKB_Param = cAtenaGetPara1.p_strFrnZairyuJohoKB
            '*����ԍ� 000041 2011/05/18 �ǉ��I��
            '*����ԍ� 000046 2011/11/07 �ǉ��J�n
            ' �Z��@�����敪��ϐ��ɃZ�b�g
            m_strJukiHokaiseiKB_Param = cAtenaGetPara1.p_strJukiHokaiseiKB
            '*����ԍ� 000046 2011/11/07 �ǉ��I��
            '*����ԍ� 000048 2014/04/28 �ǉ��J�n
            ' ���ʔԍ��擾�敪��ϐ��ɃZ�b�g
            m_strMyNumberKB_Param = cAtenaGetPara1.p_strMyNumberKB
            '*����ԍ� 000048 2014/04/28 �ǉ��I��

            ' �J�������쐬
            Select Case strGyomuMei
                '*����ԍ� 000027 2006/07/31 �C���J�n
                Case NENKIN, NENKIN_2    ' �N���������
                    '*����ԍ� 000040 2010/05/14 �ǉ��J�n
                    m_blnNenKin = True
                    '*����ԍ� 000040 2010/05/14 �ǉ��I��
                    '*����ԍ� 000047 2012/03/13 �ǉ��J�n
                    m_blnKobetsu = False
                    m_strKobetsuShutokuKB = String.Empty
                    '*����ԍ� 000047 2012/03/13 �ǉ��I��
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataTable = Me.CreateNenkinAtenaHyojunColumns(strGyomuMei)
                    Else
                        csDataTable = Me.CreateNenkinAtenaColumns(strGyomuMei)
                    End If
                    'Case NENKIN     ' �N���������
                    '    csDataTable = Me.CreateNenkinAtenaColumns()
                    '*����ԍ� 000027 2006/07/31 �C���I��
                Case KOBETSU    ' �����ʏ��
                    '*����ԍ� 000034 2008/01/15 �ǉ��J�n
                    ' �ʎ����擾�敪�������o�ϐ��ɃZ�b�g
                    m_strKobetsuShutokuKB = cAtenaGetPara1.p_strKobetsuShutokuKB.Trim
                    '*����ԍ� 000034 2008/01/15 �ǉ��I��

                    '*����ԍ� 000036 2008/11/10 �ǉ��J�n
                    m_blnKobetsu = True
                    '*����ԍ� 000036 2008/11/10 �ǉ��I��
                    '*����ԍ� 000047 2012/03/13 �ǉ��J�n
                    m_blnNenKin = False
                    '*����ԍ� 000047 2012/03/13 �ǉ��I��
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataTable = Me.CreateAtena1KobetsuHyojunColumns()
                    Else
                        csDataTable = Me.CreateAtena1KobetsuColumns()
                    End If
                Case Else       ' �������
                    '*����ԍ� 000047 2012/03/13 �ǉ��J�n
                    m_blnKobetsu = False
                    m_blnNenKin = False
                    m_strKobetsuShutokuKB = String.Empty
                    '*����ԍ� 000047 2012/03/13 �ǉ��I��
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataTable = Me.CreateAtena1HyojunColumns()
                    Else
                        csDataTable = Me.CreateAtena1Columns()
                    End If
            End Select
            '*����ԍ� 000019 2003/11/19 �C���I��
            csAtena1 = New DataSet()
            csAtena1.Tables.Add(csDataTable)
            '*����ԍ� 000013 2003/04/18 �C���C��

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            ''�s�������̃C���X�^���X�쐬
            'cuUSSCityInfo = New USSCityInfoClass()

            ''��[�֌W�̃C���X�^���X�쐬
            'cABDainoKankeiB = New ABDainoKankeiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)

            ''�Z����ʂ̃C���X�^���X�쐬
            'cABJuminShubetsuB = New ABJuminShubetsuBClass(m_cfUFControlData, m_cfUFConfigDataClass)

            ''�@�l���̂̃C���X�^���X�쐬
            'cABHojinMeishoB = New ABHojinMeishoBClass(m_cfUFControlData, m_cfUFConfigDataClass)

            ''�l�@�l�̃C���X�^���X�쐬
            'cABKjnhjnKBB = New ABKjnhjnKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

            ''�Ǔ��ǊO�̃C���X�^���X�쐬
            'cABKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

            ''���N�����ҏW�̃C���X�^���X�쐬
            'cABUmareHenshuB = New ABUmareHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass)
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '*����ԍ� 000008 2003/03/17 �ǉ��J�n
            '*����ԍ� 000016 2003/08/22 �폜�J�n
            '�Ǘ����擾�a�̃C���X�^���X�쐬
            'cURKanriJohoB = New Densan.Reams.UR.UR001BB.URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            '*����ԍ� 000016 2003/08/22 �폜�I��
            '*����ԍ� 000008 2003/03/17 �ǉ��I��

            '*����ԍ� 000021 2003/12/02 �폜�J�n
            ''*����ԍ� 000017 2003/10/09 �ǉ��J�n
            '' �A����a�N���X�̃C���X�^���X�쐬
            'cRenrakusakiBClass = New ABRenrakusakiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            ''*����ԍ� 000017 2003/10/09 �ǉ��I��
            '*����ԍ� 000021 2003/12/02 �폜�I��

            '*����ԍ� 000007 2003/03/17 �ǉ��J�n
            '�p�����[�^�̃`�F�b�N
            Me.CheckColumnValue(cAtenaGetPara1)
            '*����ԍ� 000007 2003/03/17 �ǉ��I��

            '�Z���ҏW�P��"1"���Z���ҏW�Q��"1"�̏ꍇ
            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            'If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu2 = "1" Then

            '    '���߂̎s���������擾����
            '    cuUSSCityInfo.GetCityInfo(m_cfUFControlData)
            'End If
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '*����ԍ� 000008 2003/03/17 �ǉ��J�n
            '�Z���ҏW�P��"1"���Z���ҏW�R��""�̏ꍇ
            If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu3 = String.Empty Then
                '*����ԍ� 000016 2003/08/22 �C���J�n
                'cSofuJushoGyoseikuType = cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param

                cSofuJushoGyoseikuType = Me.GetSofuJushoGyoseikuType
                '*����ԍ� 000016 2003/08/22 �C���I��
                Select Case cSofuJushoGyoseikuType
                    Case SofuJushoGyoseikuType.Jusho_Banchi
                        strJushoHenshu3 = "1"
                        strJushoHenshu4 = ""
                    Case SofuJushoGyoseikuType.Jusho_Banchi_SP_Katagaki
                        strJushoHenshu3 = "1"
                        strJushoHenshu4 = "1"
                    Case SofuJushoGyoseikuType.Gyoseiku_SP_Banchi
                        strJushoHenshu3 = "5"
                        strJushoHenshu4 = ""
                    Case SofuJushoGyoseikuType.Gyoseiku_SP_Banchi_SP_Katagaki
                        strJushoHenshu3 = "5"
                        strJushoHenshu4 = "1"
                End Select
            Else
                strJushoHenshu3 = cAtenaGetPara1.p_strJushoHenshu3
                strJushoHenshu4 = cAtenaGetPara1.p_strJushoHenshu4
            End If
            '*����ԍ� 000008 2003/03/17 �ǉ��I��

            '�ҏW�����f�[�^���쐬����
            For Each csDataRow In csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows
                '*����ԍ� 000013 2003/04/18 �C���J�n
                'csDataNewRow = csAtena1.Tables(ABAtena1Entity.TABLE_NAME).NewRow

                csDataNewRow = csDataTable.NewRow
                '*����ԍ� 000013 2003/04/18 �C���I��

                '*����ԍ� 000026 2005/12/21 �ǉ��J�n
                csDataNewRow.BeginEdit()
                '*����ԍ� 000026 2005/12/21 �ǉ��I��

                '*����ԍ� 000015 2003/04/30 �ǉ��J�n
                For Each csColumn In csDataNewRow.Table.Columns
                    csDataNewRow(csColumn) = String.Empty
                Next csColumn
                '*����ԍ� 000015 2003/04/30 �ǉ��I��

                '*����ԍ� 000021 2003/12/02 �폜�J�n
                ''*����ԍ� 000017 2003/10/09 �ǉ��J�n
                '' �Ɩ��R�[�h���w�肳�ꂽ�ꍇ
                'If (strGyomuCD <> String.Empty) Then

                '    ' �A����f�[�^���擾����
                '    csRenrakusakiEntity = cRenrakusakiBClass.GetRenrakusakiBHoshu(CType(csDataRow(ABAtenaEntity.JUMINCD), String), strGyomuCD, strGyomunaiSHU_CD)
                '    If (csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows.Count <> 0) Then
                '        csRenrakusakiRow = csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows(0)
                '    Else
                '        csRenrakusakiRow = Nothing
                '    End If
                'Else
                '    csRenrakusakiRow = Nothing
                'End If
                '*����ԍ� 000017 2003/10/09 �ǉ��I��
                '*����ԍ� 000021 2003/12/02 �폜�I��

                ' �Z���R�[�h
                csDataNewRow(ABAtena1Entity.JUMINCD) = csDataRow(ABAtenaEntity.JUMINCD)

                ' ��[�敪�w��Ȃ��̏ꍇ
                If strDainoKB = String.Empty Then
                    ' ��[�敪
                    csDataNewRow(ABAtena1Entity.DAINOKB) = "00"
                Else
                    ' ��[�敪
                    csDataNewRow(ABAtena1Entity.DAINOKB) = strDainoKB
                End If

                If CType(csDataNewRow(ABAtena1Entity.DAINOKB), String) = "00" Then

                    ' ��[�敪����
                    csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty

                    ' ��[�敪��������
                    csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty
                Else

                    ' ��[�֌W�f�[�^���擾����

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                    'csDainoKankeiCDMSTEntity = m_cABDainoKankeiB.GetDainoKBHoshu(CType(csDataNewRow(ABAtena1Entity.DAINOKB), String))
                    '' �O���̏ꍇ�A
                    'If csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows.Count = 0 Then
                    '    csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty                   '��[�敪����
                    '    csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty              '��[�敪��������
                    'Else
                    '    With csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows(0)

                    '        ' ��[�敪����
                    '        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = CType(.Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO), String)

                    '        ' ��[�敪��������
                    '        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = CType(.Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI), String)
                    '    End With
                    'End If
                    csDainoKankeiCDMSTEntity = m_cABDainoKankeiB.GetDainoKBHoshu2(CType(csDataNewRow(ABAtena1Entity.DAINOKB), String))
                    If csDainoKankeiCDMSTEntity.Length = 0 Then
                        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty                   '��[�敪����
                        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty              '��[�敪��������
                    Else

                        ' ��[�敪����
                        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = CType(csDainoKankeiCDMSTEntity(0).Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO), String)

                        ' ��[�敪��������
                        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = CType(csDainoKankeiCDMSTEntity(0).Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI), String)
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                End If

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                    ' ��[�敪�w��Ȃ��̏ꍇ
                    If strGyomuCD = String.Empty Then

                        '�Ɩ��R�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUCD) = "00"

                        '�Ɩ�����ʃR�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = String.Empty
                    Else
                        '�Ɩ��R�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUCD) = strGyomuCD

                        '�Ɩ�����ʃR�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = strGyomunaiSHU_CD
                    End If

                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '���s�����R�[�h
                csDataNewRow(ABAtena1Entity.KYUSHICHOSONCD) = csDataRow(ABAtenaEntity.KYUSHICHOSONCD)

                '���уR�[�h
                csDataNewRow(ABAtena1Entity.STAICD) = csDataRow(ABAtenaEntity.STAICD)

                '�����f�[�^�敪
                csDataNewRow(ABAtena1Entity.ATENADATAKB) = csDataRow(ABAtenaEntity.ATENADATAKB)

                '�����f�[�^���
                csDataNewRow(ABAtena1Entity.ATENADATASHU) = csDataRow(ABAtenaEntity.ATENADATASHU)

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                    '�ҏW���
                    m_cABJuminShubetsuB.GetJuminshubetsu(CType(csDataRow(ABAtenaEntity.ATENADATAKB), String),
                                                       CType(csDataRow(ABAtenaEntity.ATENADATASHU), String))
                    csDataNewRow(ABAtena1Entity.HENSHUSHUBETSU) = m_cABJuminShubetsuB.p_strHenshuShubetsu

                    '�ҏW��ʗ���
                    csDataNewRow(ABAtena1Entity.HENSHUSHUBETSURYAKU) = m_cABJuminShubetsuB.p_strHenshuShubetsuRyaku
                    '�����p�J�i����
                    csDataNewRow(ABAtena1Entity.SEARCHKANASEIMEI) = csDataRow(ABAtenaEntity.SEARCHKANASEIMEI)

                    '�����p�J�i��
                    csDataNewRow(ABAtena1Entity.SEARCHKANASEI) = csDataRow(ABAtenaEntity.SEARCHKANASEI)

                    '�����p�J�i��
                    csDataNewRow(ABAtena1Entity.SEARCHKANAMEI) = csDataRow(ABAtenaEntity.SEARCHKANAMEI)

                    '�����p��������
                    csDataNewRow(ABAtena1Entity.SEARCHKANJIMEI) = csDataRow(ABAtenaEntity.SEARCHKANJIMEISHO)
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '*����ԍ� 000042 2011/05/18 �ǉ��J�n
                ' �{���ʏ̖��֑ؑΉ� - �J�i���́A�������̎擾
                Select Case CStr(csDataRow(ABAtenaEntity.ATENADATAKB))
                    Case "11", "12"         ' �Z�o���A�Z�o�O

                        If (m_strHonmyoTsushomeiYusenKB.Trim = "1") Then
                            ' �Ǘ����F�{���ʏ̖��D�搧�� = "1" �̏ꍇ
                            strMeisho = MeishoHenshu(csDataRow)
                        Else
                            strMeisho(0) = CStr(csDataRow(ABAtenaEntity.KANAMEISHO1))       ' �J�i���̂P
                            strMeisho(1) = CStr(csDataRow(ABAtenaEntity.KANJIMEISHO1))      ' 
                        End If
                    Case "20"               ' �@�l

                    Case "30"               ' ���L
                        strMeisho(0) = CStr(csDataRow(ABAtenaEntity.KANAMEISHO1))
                        strMeisho(1) = CStr(csDataRow(ABAtenaEntity.KANJIMEISHO1))
                    Case Else
                End Select
                '*����ԍ� 000042 2011/05/18 �ǉ��I��

                '�ҏW�J�i����
                '�����敪="20"(�@�l)�̏ꍇ
                If CType(csDataRow(ABAtenaEntity.ATENADATAKB), String) = "20" Then
                    '* ����ԍ� 000033 2007/07/17 �C���J�n
                    '�J�i���̂Q�i�x�X���j�������ꍇ�̓J�i���̂P�i�@�l���j�ƃJ�i���̂Q�i�x�X���j�̌����͍s��Ȃ�
                    If CType(csDataRow(ABAtenaEntity.KANAMEISHO2), String).Trim <> String.Empty Then
                        strHenshuKanaMeisho = CType(csDataRow(ABAtenaEntity.KANAMEISHO1), String).TrimEnd +
                                " " + CType(csDataRow(ABAtenaEntity.KANAMEISHO2), String).TrimEnd
                    Else
                        strHenshuKanaMeisho = CType(csDataRow(ABAtenaEntity.KANAMEISHO1), String).TrimEnd
                    End If
                    'strHenshuKanaMeisho = CType(csDataRow(ABAtenaEntity.KANAMEISHO1), String).TrimEnd + _
                    '        " " + CType(csDataRow(ABAtenaEntity.KANAMEISHO2), String).TrimEnd
                    '* ����ԍ� 000033 2007/07/17 �C���I��
                    '* ����ԍ� 000032 2007/07/09 �C���J�n
                    If (strHenshuKanaMeisho.RLength > 240) Then
                        csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho.RSubstring(0, 240)
                        'If (strHenshuKanaMeisho.Length > 60) Then
                        '    csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho.Substring(0, 60)
                        '* ����ԍ� 000032 2007/07/09 �C���I��
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho
                    End If
                Else
                    '*����ԍ� 000042 2011/05/18 �C���J�n
                    strHenshuKanaMeisho = strMeisho(0)
                    csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = ABStrXClass.Left(strHenshuKanaMeisho, ABAtenaGetConstClass.KETA_HENSHUKANAMEISHO)
                    'csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = csDataRow(ABAtenaEntity.KANAMEISHO1)
                    '*����ԍ� 000042 2011/05/18 �C���I��
                End If
                '�ҏW�J�i���́i�t���j
                If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    csDataNewRow(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL) = strHenshuKanaMeisho
                Else
                End If

                '�ҏW��������
                '�����敪="20"(�@�l)�̏ꍇ
                If CType(csDataRow(ABAtenaEntity.ATENADATAKB), String) = "20" Then
                    m_cABHojinMeishoB.p_strKeitaiFuyoKB = CType(csDataRow(ABAtenaEntity.HANYOKB1), String)
                    m_cABHojinMeishoB.p_strKeitaiSeiRyakuKB = CType(csDataRow(ABAtenaEntity.HANYOKB2), String)
                    m_cABHojinMeishoB.p_strKanjiHjnKeitai = CType(csDataRow(ABAtenaEntity.KANJIHJNKEITAI), String)
                    m_cABHojinMeishoB.p_strKanjiMeisho1 = CType(csDataRow(ABAtenaEntity.KANJIMEISHO1), String)
                    m_cABHojinMeishoB.p_strKanjiMeisho2 = CType(csDataRow(ABAtenaEntity.KANJIMEISHO2), String)
                    strHenshuKanjiShimei = m_cABHojinMeishoB.GetHojinMeisho()
                    '* ����ԍ� 000032 2007/07/09 �C���J�n
                    If (strHenshuKanjiShimei.RLength > 240) Then
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei.RSubstring(0, 240)
                        'If (strHenshuKanjiShimei.Length > 80) Then
                        '    csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei.Substring(0, 80)
                        '* ����ԍ� 000032 2007/07/09 �C���I��
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei
                    End If
                Else
                    '* �����J�n 000035 2008/02/15 �C���J�n
                    'csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = csDataRow(ABAtenaEntity.KANJIMEISHO1)
                    If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                        '*����ԍ� 000042 2011/05/18 �C���J�n
                        ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                        strHenshuKanjiShimei = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.ATENADATAKB)),
                                                                                   CStr(csDataRow(ABAtenaEntity.ATENADATASHU)),
                                                                                   strMeisho(1))
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strHenshuKanjiShimei, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO)
                        'csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.ATENADATAKB)), _
                        '                                                                                     CStr(csDataRow(ABAtenaEntity.ATENADATASHU)), _
                        '                                                                                     CStr(csDataRow(ABAtenaEntity.KANJIMEISHO1)))
                        '*����ԍ� 000042 2011/05/18 �C���I��
                    Else
                        '*����ԍ� 000042 2011/05/18 �C���J�n
                        ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                        strHenshuKanjiShimei = strMeisho(1)
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strHenshuKanjiShimei, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO)
                        'csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = csDataRow(ABAtenaEntity.KANJIMEISHO1)
                        '*����ԍ� 000042 2011/05/18 �C���I��
                    End If
                    '* �����J�n 000035 2008/02/15 �C���I��
                End If
                '�ҏW�������́i�t���j
                If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    csDataNewRow(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL) = strHenshuKanjiShimei
                Else
                End If

                If (csDataRow(ABAtenaEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTONAI_KOJIN) Then
                    If (csDataRow(ABAtenaEntity.UMAREYMD).ToString.Trim = String.Empty) Then
                        csDataNewRow(ABAtena1Entity.UMAREYMD) = m_strUmareYMDHenkanParam
                        If (csDataRow(ABAtenaEntity.ATENADATASHU).ToString.RSubstring(0, 1) = "1") Then
                            csDataNewRow(ABAtena1Entity.UMAREWMD) = m_strUmareWmdHenkan
                        Else
                            csDataNewRow(ABAtena1Entity.UMAREWMD) = m_strUmareWmdhenkanSeireki
                        End If
                    ElseIf (CheckDate(csDataRow(ABAtenaEntity.UMAREYMD).ToString)) Then
                        csDataNewRow(ABAtena1Entity.UMAREYMD) = csDataRow(ABAtenaEntity.UMAREYMD)
                        csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaEntity.UMAREWMD)
                    Else
                        csDataNewRow(ABAtena1Entity.UMAREYMD) = GetSeirekiLastDay(csDataRow(ABAtenaEntity.UMAREYMD).ToString)
                        csDataNewRow(ABAtena1Entity.UMAREWMD) = GetWarekiLastDay(csDataRow(ABAtenaEntity.UMAREWMD).ToString,
                                                                csDataRow(ABAtenaEntity.UMAREYMD).ToString)
                    End If
                Else
                    '���N����
                    csDataNewRow(ABAtena1Entity.UMAREYMD) = csDataRow(ABAtenaEntity.UMAREYMD)

                    '���N�����ҏW
                    csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaEntity.UMAREWMD)
                End If

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                    m_cABUmareHenshuB.p_strDataKB = CType(csDataRow(ABAtenaEntity.ATENADATAKB), String)
                    m_cABUmareHenshuB.p_strJuminSHU = CType(csDataRow(ABAtenaEntity.ATENADATASHU), String)
                    m_cABUmareHenshuB.p_strUmareYMD = CType(csDataNewRow(ABAtena1Entity.UMAREYMD), String)
                    m_cABUmareHenshuB.p_strUmareWMD = CType(csDataNewRow(ABAtena1Entity.UMAREWMD), String)
                    m_cABUmareHenshuB.HenshuUmare()
                    '���\���N����
                    csDataNewRow(ABAtena1Entity.UMAREHYOJIWMD) = m_cABUmareHenshuB.p_strHyojiUmareYMD

                    '���ؖ��N����
                    csDataNewRow(ABAtena1Entity.UMARESHOMEIWMD) = m_cABUmareHenshuB.p_strShomeiUmareYMD

                    '���ʃR�[�h
                    csDataNewRow(ABAtena1Entity.SEIBETSUCD) = csDataRow(ABAtenaEntity.SEIBETSUCD)

                    '����
                    strWork = CType(csDataRow(ABAtenaEntity.SEIBETSU), String).Trim
                    csDataNewRow(ABAtena1Entity.SEIBETSU) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_SEIBETSU)
                    '���ʁi�t���j
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataNewRow(ABAtena1HyojunEntity.SEIBETSU_FULL) = csDataRow(ABAtenaEntity.SEIBETSU)
                    Else
                    End If

                    '�ҏW�����R�[�h
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(ABAtenaEntity.DAI2ZOKUGARACD, String) = String.Empty Then
                    If CType(csDataRow(ABAtenaEntity.DAI2ZOKUGARACD), String).Trim = String.Empty Then
                        '*����ԍ� 000002 2003/02/20 �C���I��
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = csDataRow(ABAtenaEntity.ZOKUGARACD)
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = csDataRow(ABAtenaEntity.DAI2ZOKUGARACD)
                    End If

                    '�ҏW����
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(ABAtenaEntity.DAI2ZOKUGARA, String) = String.Empty Then
                    If CType(csDataRow(ABAtenaEntity.DAI2ZOKUGARA), String).Trim = String.Empty Then
                        '*����ԍ� 000002 2003/02/20 �C���I��
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = csDataRow(ABAtenaEntity.ZOKUGARA)
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = csDataRow(ABAtenaEntity.DAI2ZOKUGARA)
                    End If

                    '* �����J�n 000035 2008/02/15 �C���J�n
                    '�@�l��\�Җ�
                    'csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = csDataRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
                    If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                        ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                        csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.ATENADATAKB)),
                                                                                                           CStr(csDataRow(ABAtenaEntity.ATENADATASHU)),
                                                                                                           CStr(csDataRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)))
                    Else
                        ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                        csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = csDataRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
                    End If
                    '* �����J�n 000035 2008/02/15 �C���I��
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '�l�@�l�敪
                csDataNewRow(ABAtena1Entity.KJNHJNKB) = csDataRow(ABAtenaEntity.KJNHJNKB)

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                    '�l�@�l�敪����
                    csDataNewRow(ABAtena1Entity.KJNHJNKBMEISHO) = m_cABKjnhjnKBB.GetKjnhjn(CType(csDataRow(ABAtenaEntity.KJNHJNKB), String))

                    '�Ǔ��ǊO�敪����
                    csDataNewRow(ABAtena1Entity.NAIGAIKBMEISHO) = m_cABKannaiKangaiKBB.GetKannaiKangai(CType(csDataRow(ABAtenaEntity.KANNAIKANGAIKB), String))
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '�Ǔ��ǊO�敪
                csDataNewRow(ABAtena1Entity.KANNAIKANGAIKB) = csDataRow(ABAtenaEntity.KANNAIKANGAIKB)

                '�Z��D��̏ꍇ
                If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then

                    '�X�֔ԍ�
                    csDataNewRow(ABAtena1Entity.YUBINNO) = csDataRow(ABAtenaEntity.JUKIYUBINNO)

                    '�Z���R�[�h
                    csDataNewRow(ABAtena1Entity.JUSHOCD) = csDataRow(ABAtenaEntity.JUKIJUSHOCD)

                    '�Z��
                    csDataNewRow(ABAtena1Entity.JUSHO) = csDataRow(ABAtenaEntity.JUKIJUSHO)

                    '�ҏW�Z����
                    If cAtenaGetPara1.p_strJushoHenshu1 = String.Empty Then
                        csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = String.Empty
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = String.Empty
                        Else
                        End If

                    ElseIf cAtenaGetPara1.p_strJushoHenshu1 = "1" Then
                        '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                        'strHenshuJusho = String.Empty
                        m_strHenshuJusho.RRemove(0, m_strHenshuJusho.RLength)
                        '* ����ԍ� 000024 2005/01/25 �X�V�I��

                        If cAtenaGetPara1.p_strJushoHenshu2 = "1" Then

                            '�s�������𓪂ɕt������i�Ǔ��̂݁j
                            If CType(csDataRow(ABAtenaEntity.KANNAIKANGAIKB), String) = "1" Then
                                '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                'strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0)
                                m_strHenshuJusho.Append(m_cuUSSCityInfo.p_strShichosonmei(0))
                                '* ����ԍ� 000024 2005/01/25 �X�V�I��
                            End If


                        End If
                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'Select Case cAtenaGetPara1.p_strJushoHenshu3
                        Select Case strJushoHenshu3
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '* ����ԍ� 000028 2007/01/15 �C���J�n
                            Case "1", "6"   '�Z���{�Ԓn
                                'Case "1"    '�Z���{�Ԓn
                                '* ����ԍ� 000028 2007/01/15 �C���I��
                                '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd)
                                m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                '* ����ԍ� 000024 2005/01/25 �X�V�I��
                            Case "2"    '�s����{�Ԓn
                                '*����ԍ� 000009 2003/03/17 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                '�s���於���Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).Trim = String.Empty) Then
                                    '�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                                Else
                                    '�s����{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                                End If
                                '*����ԍ� 000009 2003/03/17 �C���I��
                            Case "3"    '�Z���{�i�s����j�{�Ԓn
                                '*����ԍ� 000004  2003/02/25 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd

                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                                Else
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + "�i" _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '                + "�j" _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append("�i")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�j")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                                End If
                                '*����ԍ� 000004  2003/02/25 �C���I��
                            Case "4"    '�s����{�i�Z���j�{�Ԓn
                                '*����ԍ� 000004  2003/02/25 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                '               + CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                '               + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd

                                '�Z�������݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '               + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                                    '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                                    '�s���悪���݂��Ȃ��ꍇ
                                ElseIf (CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                                    '*����ԍ� 000009 2003/03/17 �ǉ��I��
                                Else
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '                + "�i" _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + "�j" _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�i")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append("�j")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                                End If
                                '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                            Case "5"    '�s����{���{�Ԓn
                                '�s���於���Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).Trim = String.Empty) Then
                                    '�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                                Else
                                    '�s����{�Ԓn
                                    '
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '                + "�@" _
                                    '                + CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�@")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIBANCHI), String).TrimEnd)
                                End If
                                '*����ԍ� 000009 2003/03/17 �C���I��
                        End Select
                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                        '* ����ԍ� 000028 2007/01/15 �C���J�n
                        If (strJushoHenshu4 = "1") _
                            AndAlso (CType(csDataRow(ABAtenaEntity.JUKIKATAGAKI), String).Trim <> String.Empty) Then
                            'If strJushoHenshu4 = "1" Then
                            '* ����ԍ� 000028 2007/01/15 �C���I��
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '*����ԍ� 000004  2003/02/25 �C���J�n
                            'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUKIKATAGAKI), String).TrimEnd

                            '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                            'strHenshuJusho += "�@" + CType(csDataRow(ABAtenaEntity.JUKIKATAGAKI), String).TrimEnd
                            m_strHenshuJusho.Append("�@")
                            m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIKATAGAKI), String).TrimEnd)
                            '* ����ԍ� 000024 2005/01/25 �X�V�I��
                            '*����ԍ� 000004  2003/02/25 �C���I��
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��J�n
                        ' �Z���ҏW�R�p�����[�^���U�A���s���於������Ƃ��́A�ҏW�Z���Ɂi�s����j��ǉ�����
                        If (strJushoHenshu3 = "6") _
                                AndAlso (CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).Trim <> String.Empty) Then
                            m_strHenshuJusho.Append("�i")
                            m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                            m_strHenshuJusho.Append("�j")
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��I��
                        '* ����ԍ� 000024 2005/01/25 �X�V�J�n
                        'If strHenshuJusho.Length >= 80 Then
                        '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                        'Else
                        '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho
                        'End If
                        '* ����ԍ� 000032 2007/07/09 �C���J�n
                        If m_strHenshuJusho.RLength >= 160 Then
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString.RSubstring(0, 160)
                            'If m_strHenshuJusho.Length >= 80 Then
                            '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString.Substring(0, 80)
                            '* ����ԍ� 000032 2007/07/09 �C���I��
                        Else
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString
                        End If
                        '* ����ԍ� 000024 2005/01/25 �X�V�I��
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = m_strHenshuJusho.ToString()
                        Else
                        End If
                    End If

                    '�Ԓn�R�[�h�P
                    csDataNewRow(ABAtena1Entity.BANCHICD1) = csDataRow(ABAtenaEntity.JUKIBANCHICD1)

                    '�Ԓn�R�[�h�Q
                    csDataNewRow(ABAtena1Entity.BANCHICD2) = csDataRow(ABAtenaEntity.JUKIBANCHICD2)

                    '�Ԓn�R�[�h�R
                    csDataNewRow(ABAtena1Entity.BANCHICD3) = csDataRow(ABAtenaEntity.JUKIBANCHICD3)

                    '�Ԓn
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" Then

                        '�Z���ҏW����̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.BANCHI) = String.Empty
                    Else
                        csDataNewRow(ABAtena1Entity.BANCHI) = csDataRow(ABAtenaEntity.JUKIBANCHI)
                    End If

                    '�����t���O
                    csDataNewRow(ABAtena1Entity.KATAGAKIFG) = csDataRow(ABAtenaEntity.JUKIKATAGAKIFG)

                    '�����R�[�h
                    csDataNewRow(ABAtena1Entity.KATAGAKICD) = csDataRow(ABAtenaEntity.JUKIKATAGAKICD)

                    '����
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                        '�����t������̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = String.Empty
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = String.Empty
                        Else
                        End If
                    Else
                        strWork = CType(csDataRow(ABAtenaEntity.JUKIKATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csDataRow(ABAtenaEntity.JUKIKATAGAKI)
                        Else
                        End If
                    End If

                    '*����ԍ� 000017 2003/10/09 �C���J�n
                    ''�A����P
                    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                    ''�A����Q
                    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)

                    '*����ԍ� 000021 2003/12/02 �C���J�n
                    '' �A����}�X�^�����݂���ꍇ�́A�A����}�X�^�̘A�����ݒ肷��
                    'If (csRenrakusakiRow Is Nothing) Then
                    '    '�A����P
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                    '    '�A����Q
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)
                    'Else
                    '    '�A����P
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                    '    '�A����Q
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                    '    '�A����擾�Ɩ��R�[�h
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                    'End If
                    ''*����ԍ� 000017 2003/10/09 �C���I��

                    '�A����P
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                    '�A����Q
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)
                    '*����ԍ� 000021 2003/12/02 �C���I��

                    '�s����R�[�h
                    csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csDataRow(ABAtenaEntity.JUKIGYOSEIKUCD)

                    '�s���於
                    csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csDataRow(ABAtenaEntity.JUKIGYOSEIKUMEI)

                    '�n��R�[�h�P
                    csDataNewRow(ABAtena1Entity.CHIKUCD1) = csDataRow(ABAtenaEntity.JUKICHIKUCD1)

                    '�n��P
                    csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csDataRow(ABAtenaEntity.JUKICHIKUMEI1)

                    '�n��R�[�h�Q
                    csDataNewRow(ABAtena1Entity.CHIKUCD2) = csDataRow(ABAtenaEntity.JUKICHIKUCD2)

                    '�n��Q
                    csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csDataRow(ABAtenaEntity.JUKICHIKUMEI2)

                    '�n��R�[�h�R
                    csDataNewRow(ABAtena1Entity.CHIKUCD3) = csDataRow(ABAtenaEntity.JUKICHIKUCD3)

                    '�n��R
                    csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csDataRow(ABAtenaEntity.JUKICHIKUMEI3)

                    '�\�����i��Q�Z���[�\����������ꍇ�́A��Q�Z���[�\�����j
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN), String) = String.Empty Then
                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n IF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                        If CType(csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN), String).Trim = "00" Then
                            '*����ԍ� 000002 2003/02/20 �C���I��
                            csDataNewRow(ABAtena1Entity.HYOJIJUN) = csDataRow(ABAtenaEntity.JUMINHYOHYOJIJUN)
                        Else
                            csDataNewRow(ABAtena1Entity.HYOJIJUN) = csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN)
                        End If
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I�� IF���ň͂�
                Else

                    '�X�֔ԍ�
                    csDataNewRow(ABAtena1Entity.YUBINNO) = csDataRow(ABAtenaEntity.YUBINNO)

                    '�Z���R�[�h
                    csDataNewRow(ABAtena1Entity.JUSHOCD) = csDataRow(ABAtenaEntity.JUSHOCD)

                    '�Z��
                    csDataNewRow(ABAtena1Entity.JUSHO) = csDataRow(ABAtenaEntity.JUSHO)

                    '�ҏW�Z����
                    If cAtenaGetPara1.p_strJushoHenshu1 = String.Empty Then
                        csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = String.Empty
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = String.Empty
                        Else
                        End If

                    ElseIf cAtenaGetPara1.p_strJushoHenshu1 = "1" Then
                        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                        'strHenshuJusho = String.Empty
                        m_strHenshuJusho.RRemove(0, m_strHenshuJusho.RLength)
                        '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                        If cAtenaGetPara1.p_strJushoHenshu2 = "1" Then

                            '�Ǔ��̂ݎs��������t������
                            If CType(csDataRow(ABAtenaEntity.KANNAIKANGAIKB), String) = "1" Then
                                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                'strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0)
                                m_strHenshuJusho.Append(m_cuUSSCityInfo.p_strShichosonmei(0))
                                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                            End If
                        End If
                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'Select Case cAtenaGetPara1.p_strJushoHenshu3
                        Select Case strJushoHenshu3
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '* ����ԍ� 000028 2007/01/15 �C���J�n
                            Case "1", "6"   '�Z���{�Ԓn
                                'Case "1"    '�Z���{�Ԓn
                                '* ����ԍ� 000028 2007/01/15 �C���I��
                                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd)
                                m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                            Case "2"    '�s����{�Ԓn
                                '*����ԍ� 000009 2003/03/17 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                Else
                                    '�s����{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000009 2003/03/17 �C���I��
                            Case "3"    '�Z���{�i�s����j�{�Ԓn
                                '*����ԍ� 000004  2003/02/25 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd

                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                Else
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                    '                + "�i" _
                                    '                + CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + "�j" _
                                    '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append("�i")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�j")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000004  2003/02/25 �C���I��

                            Case "4"    '�s����{�i�Z���j�{�Ԓn
                                '*����ԍ� 000004 2003/02/25 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd

                                '�Z�������݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                    '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                                ElseIf (CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                    '*����ԍ� 000009 2003/03/17 �ǉ��I��
                                Else
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + "�i" _
                                    '                + CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                    '                + "�j" _
                                    '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�i")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append("�j")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000004 2003/02/25 �C���I��
                                '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                            Case "5"    '�s����{���{�Ԓn
                                '*����ԍ� 000009 2003/03/17 �C���J�n
                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                Else
                                    '�s����{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + "�@" _
                                    '                + CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�@")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000009 2003/03/17 �ǉ��I��
                        End Select
                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                        '* ����ԍ� 000028 2007/01/15 �C���J�n
                        If (strJushoHenshu4 = "1") _
                            AndAlso (CType(csDataRow(ABAtenaEntity.KATAGAKI), String).Trim <> String.Empty) Then
                            'If strJushoHenshu4 = "1" Then
                            '* ����ԍ� 000028 2007/01/15 �C���I��
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '*����ԍ� 000004  2003/02/25 �C���J�n
                            'strHenshuJusho += CType(csDataRow(ABAtenaEntity.KATAGAKI), String).TrimEnd

                            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                            'strHenshuJusho += "�@" + CType(csDataRow(ABAtenaEntity.KATAGAKI), String).TrimEnd
                            m_strHenshuJusho.Append("�@")
                            m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.KATAGAKI), String).TrimEnd)
                            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                            '*����ԍ� 000004  2003/02/25 �C���I��
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��J�n
                        ' �Z���ҏW�R�p�����[�^���U�A���s���於������Ƃ��́A�ҏW�Z���Ɂi�s����j��ǉ�����
                        If (strJushoHenshu3 = "6") _
                                AndAlso (CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).Trim <> String.Empty) Then
                            m_strHenshuJusho.Append("�i")
                            m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd)
                            m_strHenshuJusho.Append("�j")
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��I��
                        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                        'If strHenshuJusho.Length >= 80 Then
                        '   csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                        'Else
                        '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho
                        'End If
                        '* ����ԍ� 000032 2007/07/09 �C���J�n
                        If m_strHenshuJusho.RLength >= 160 Then
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().RSubstring(0, 160)
                            'If m_strHenshuJusho.Length >= 80 Then
                            '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().Substring(0, 80)
                            '* ����ԍ� 000032 2007/07/09 �C���I��
                        Else
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString()
                        End If
                        '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = m_strHenshuJusho.ToString()
                        Else
                        End If
                    End If

                    '�Ԓn�R�[�h�P
                    csDataNewRow(ABAtena1Entity.BANCHICD1) = csDataRow(ABAtenaEntity.BANCHICD1)

                    '�Ԓn�R�[�h�Q
                    csDataNewRow(ABAtena1Entity.BANCHICD2) = csDataRow(ABAtenaEntity.BANCHICD2)

                    '�Ԓn�R�[�h�R
                    csDataNewRow(ABAtena1Entity.BANCHICD3) = csDataRow(ABAtenaEntity.BANCHICD3)

                    '�Ԓn
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" Then

                        '�Z���ҏW����̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.BANCHI) = ""
                    Else
                        csDataNewRow(ABAtena1Entity.BANCHI) = csDataRow(ABAtenaEntity.BANCHI)
                    End If

                    '�����t���O
                    csDataNewRow(ABAtena1Entity.KATAGAKIFG) = csDataRow(ABAtenaEntity.KATAGAKIFG)

                    '�����R�[�h
                    csDataNewRow(ABAtena1Entity.KATAGAKICD) = csDataRow(ABAtenaEntity.KATAGAKICD)

                    '����
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu4 = "1" Then

                        '�����t������̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = ""
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = String.Empty
                        Else
                        End If
                    Else
                        strWork = CType(csDataRow(ABAtenaEntity.KATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csDataRow(ABAtenaEntity.KATAGAKI)
                        Else
                        End If
                    End If

                    '*����ԍ� 000017 2003/10/09 �C���J�n
                    ''�A����P
                    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                    ''�A����Q
                    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)

                    '*����ԍ� 000021 2003/12/02 �C���J�n
                    '' �A����}�X�^�����݂���ꍇ�́A�A����}�X�^�̘A�����ݒ肷��
                    'If (csRenrakusakiRow Is Nothing) Then
                    '    '�A����P
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                    '    '�A����Q
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)
                    'Else
                    '    '�A����P
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                    '    '�A����Q
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                    '    '�A����擾�Ɩ��R�[�h
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                    'End If
                    ''*����ԍ� 000017 2003/10/09 �C���I��

                    '�A����P
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaEntity.RENRAKUSAKI1)
                    '�A����Q
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaEntity.RENRAKUSAKI2)
                    '*����ԍ� 000021 2003/12/02 �C���I��

                    '�s����R�[�h
                    csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csDataRow(ABAtenaEntity.GYOSEIKUCD)

                    '�s���於
                    csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csDataRow(ABAtenaEntity.GYOSEIKUMEI)

                    '�n��R�[�h�P
                    csDataNewRow(ABAtena1Entity.CHIKUCD1) = csDataRow(ABAtenaEntity.CHIKUCD1)

                    '�n��P
                    csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csDataRow(ABAtenaEntity.CHIKUMEI1)

                    '�n��R�[�h�Q
                    csDataNewRow(ABAtena1Entity.CHIKUCD2) = csDataRow(ABAtenaEntity.CHIKUCD2)

                    '�n��Q
                    csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csDataRow(ABAtenaEntity.CHIKUMEI2)

                    '�n��R�[�h�R
                    csDataNewRow(ABAtena1Entity.CHIKUCD3) = csDataRow(ABAtenaEntity.CHIKUCD3)

                    '�n��R
                    csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csDataRow(ABAtenaEntity.CHIKUMEI3)

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                        '* ����ԍ� 000026 2005/12/21 �C���J�n
                        ''�\����
                        'csDataNewRow(ABAtena1Entity.HYOJIJUN) = String.Empty

                        '�\�����i��Q�Z���[�\����������ꍇ�́A��Q�Z���[�\�����j
                        If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                            strWork = CType(csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN), String).Trim
                            If (strWork = "00") Then
                                strWork = csDataRow(ABAtenaEntity.JUMINHYOHYOJIJUN).ToString().Trim
                            End If
                            If (strWork = String.Empty) Then
                                strWork = "99"
                            End If
                            csDataNewRow(ABAtena1Entity.HYOJIJUN) = strWork
                        End If
                        '* ����ԍ� 000026 2005/12/21 �C���I��
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                    '�o�^�ٓ��N���� 
                    csDataNewRow(ABAtena1Entity.TOROKUIDOYMD) = csDataRow(ABAtenaEntity.TOROKUIDOYMD)

                    '�o�^���R�R�[�h
                    csDataNewRow(ABAtena1Entity.TOROKUJIYUCD) = csDataRow(ABAtenaEntity.TOROKUJIYUCD)

                    '�o�^���R
                    csDataNewRow(ABAtena1Entity.TOROKUJIYU) = csDataRow(ABAtenaEntity.TOROKUJIYU)

                    If ((csDataRow(ABAtenaEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTONAI_KOJIN) OrElse
                        (csDataRow(ABAtenaEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTOGAI_KOJIN)) AndAlso
                       (Not csDataRow(ABAtenaEntity.SHOJOJIYUCD).ToString.Trim = String.Empty) Then
                        If (csDataRow(ABAtenaEntity.SHOJOIDOYMD).ToString.Trim = String.Empty) Then
                            csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = m_strShojoIdobiHenkanParam
                        Else
                            csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csDataRow(ABAtenaEntity.SHOJOIDOYMD)
                        End If
                    Else
                        '�����ٓ��N����
                        csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csDataRow(ABAtenaEntity.SHOJOIDOYMD)
                    End If

                    '�������R�R�[�h
                    csDataNewRow(ABAtena1Entity.SHOJOJIYUCD) = csDataRow(ABAtenaEntity.SHOJOJIYUCD)

                    '�������R����
                    csDataNewRow(ABAtena1Entity.SHOJOJIYU) = csDataRow(ABAtenaEntity.SHOJOJIYU)

                    '�ҏW���ю�Z���R�[�h
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(csDataRow(ABAtenaEntity.DAI2STAINUSJUMINCD), String) = String.Empty Then
                    If CType(csDataRow(ABAtenaEntity.DAI2STAINUSJUMINCD), String).Trim = String.Empty Then
                        '*����ԍ� 000002 2003/02/20 �C���I��
                        csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csDataRow(ABAtenaEntity.STAINUSJUMINCD)
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csDataRow(ABAtenaEntity.DAI2STAINUSJUMINCD)
                    End If
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '�ҏW�J�i���ю喼
                '*����ԍ� 000002 2003/02/20 �C���J�n
                'If CType(csDataRow(ABAtenaEntity.KANADAI2STAINUSMEI), String) = String.Empty Then
                If CType(csDataRow(ABAtenaEntity.KANADAI2STAINUSMEI), String).Trim = String.Empty Then
                    '*����ԍ� 000002 2003/02/20 �C���I��
                    csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csDataRow(ABAtenaEntity.KANASTAINUSMEI)
                Else
                    csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csDataRow(ABAtenaEntity.KANADAI2STAINUSMEI)
                End If

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                    '�ҏW�������ю喼
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(csDataRow(ABAtenaEntity.DAI2STAINUSMEI), String) = String.Empty Then
                    If CType(csDataRow(ABAtenaEntity.DAI2STAINUSMEI), String).Trim = String.Empty Then
                        '*����ԍ� 000002 2003/02/20 �C���I��
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        'csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaEntity.STAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.STAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaEntity.STAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                    Else
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        'csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaEntity.DAI2STAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.DAI2STAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaEntity.DAI2STAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                    End If

                    '*����ԍ� 000012 2003/04/18 �ǉ��J�n
                    ' �����R�[�h
                    csDataNewRow(ABAtena1Entity.ZOKUGARACD) = csDataRow(ABAtenaEntity.ZOKUGARACD)
                    ' ����
                    csDataNewRow(ABAtena1Entity.ZOKUGARA) = csDataRow(ABAtenaEntity.ZOKUGARA)

                    '*����ԍ� 000014 2003/04/30 �C���J�n
                    '' �J�i���̂Q
                    'csDataNewRow(ABAtena1Entity.KANAMEISHO2) = csDataRow(ABAtenaEntity.KANAMEISHO2)
                    '' �������̂Q
                    'csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaEntity.KANJIMEISHO2)

                    '�����敪��"20"(�@�l)�̏ꍇ
                    If Not (CType(csDataRow(ABAtenaEntity.ATENADATAKB), String) = "20") Then
                        ' �J�i���̂Q
                        csDataNewRow(ABAtena1Entity.KANAMEISHO2) = csDataRow(ABAtenaEntity.KANAMEISHO2)
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        ' �������̂Q
                        'csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaEntity.KANJIMEISHO2)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.ATENADATAKB)),
                                                                                                            CStr(csDataRow(ABAtenaEntity.ATENADATASHU)),
                                                                                                            CStr(csDataRow(ABAtenaEntity.KANJIMEISHO2)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaEntity.KANJIMEISHO2)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                    End If
                    '*����ԍ� 000014 2003/04/30 �C���I��

                    ' �Дԍ�
                    csDataNewRow(ABAtena1Entity.SEKINO) = csDataRow(ABAtenaEntity.SEKINO)
                    '*����ԍ� 000012 2003/04/18 �ǉ��I��
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '*����ԍ� 000040 2010/05/14 �ǉ��J�n
                ' �{�ЕM���ҏ��o�͔���
                If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                    ' �p�����[�^:�{�ЕM���Ҏ擾�敪��"1"���A�Ǘ����:�{�Ў擾�敪(10�18)��"1"�̏ꍇ�̂݃Z�b�g
                    ' �{�ЏZ��
                    csDataNewRow(ABAtena1Entity.HON_JUSHO) = csDataRow(ABAtenaEntity.HON_JUSHO)
                    ' �{�ДԒn
                    csDataNewRow(ABAtena1Entity.HONSEKIBANCHI) = csDataRow(ABAtenaEntity.HONSEKIBANCHI)
                    ' �M����
                    csDataNewRow(ABAtena1Entity.HITTOSH) = csDataRow(ABAtenaEntity.HITTOSH)
                Else
                End If

                ' ������~�敪�o�͔���
                If (m_strShoriteishiKB_Param = "1" AndAlso m_strShoriteishiKB = "1") Then
                    ' �p�����[�^:������~�敪�擾�敪��"1"���A�Ǘ����:������~�敪�擾�敪(10�19)��"1"�̏ꍇ�̂݃Z�b�g
                    ' ������~�敪
                    csDataNewRow(ABAtena1Entity.SHORITEISHIKB) = csDataRow(ABAtenaEntity.SHORITEISHIKB)
                Else
                End If
                '*����ԍ� 000040 2010/05/14 �ǉ��I��

                '*����ԍ� 000041 2011/05/18 �ǉ��J�n
                If (m_strFrnZairyuJohoKB_Param = "1") Then
                    ' �p�����[�^�F�O���l�ݗ����i�擾�敪��"1"�̏ꍇ
                    ' ����
                    strWork = CType(csDataRow(ABAtenaEntity.KOKUSEKI), String).Trim
                    csDataNewRow(ABAtena1Entity.KOKUSEKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KOKUSEKI)
                    ' ���Ёi�t���j
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataNewRow(ABAtena1HyojunEntity.KOKUSEKI_FULL) = csDataRow(ABAtenaEntity.KOKUSEKI)
                    Else
                    End If
                    ' �ݗ����i�R�[�h
                    csDataNewRow(ABAtena1Entity.ZAIRYUSKAKCD) = csDataRow(ABAtenaEntity.ZAIRYUSKAKCD)
                    ' �ݗ����i
                    csDataNewRow(ABAtena1Entity.ZAIRYUSKAK) = csDataRow(ABAtenaEntity.ZAIRYUSKAK)
                    ' �ݗ�����
                    csDataNewRow(ABAtena1Entity.ZAIRYUKIKAN) = csDataRow(ABAtenaEntity.ZAIRYUKIKAN)
                    ' �ݗ��J�n�N����
                    csDataNewRow(ABAtena1Entity.ZAIRYU_ST_YMD) = csDataRow(ABAtenaEntity.ZAIRYU_ST_YMD)
                    ' �ݗ��I���N����
                    csDataNewRow(ABAtena1Entity.ZAIRYU_ED_YMD) = csDataRow(ABAtenaEntity.ZAIRYU_ED_YMD)
                Else
                End If
                '*����ԍ� 000041 2011/05/18 �ǉ��I��

                '*����ԍ� 000013 2003/04/18 �C���J�n
                ''�f�[�^���R�[�h�̒ǉ�
                'csAtena1.Tables(ABAtena1Entity.TABLE_NAME).Rows.Add(csDataNewRow)

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                    ' �N���p�f�[�^�쐬
                    '*����ԍ� 000027 2006/07/31 �C���J�n
                    If (strGyomuMei = NENKIN Or strGyomuMei = NENKIN_2) Then
                        'If (strGyomuMei = NENKIN) Then
                        '*����ԍ� 000027 2006/07/31 �C���I��

                        ' ����
                        csDataNewRow(ABNenkinAtenaEntity.KYUSEI) = csDataRow(ABAtenaEntity.KYUSEI)
                        ' �Z��ٓ��N����
                        csDataNewRow(ABNenkinAtenaEntity.JUTEIIDOYMD) = csDataRow(ABAtenaEntity.JUTEIIDOYMD)
                        ' �Z�莖�R
                        csDataNewRow(ABNenkinAtenaEntity.JUTEIJIYU) = csDataRow(ABAtenaEntity.JUTEIJIYU)
                        ' �]���O�Z���X�֔ԍ�
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_YUBINNO) = csDataRow(ABAtenaEntity.TENUMAEJ_YUBINNO)
                        '*����ԍ� 000017 2003/10/09 �ǉ��J�n
                        ' �]���O�Z���S���Z���R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_ZJUSHOCD) = csDataRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD)
                        '*����ԍ� 000017 2003/10/09 �ǉ��I��
                        ' �]���O�Z���Z��
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_JUSHO) = csDataRow(ABAtenaEntity.TENUMAEJ_JUSHO)
                        ' �]���O�Z���Ԓn
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_BANCHI) = csDataRow(ABAtenaEntity.TENUMAEJ_BANCHI)
                        ' �]���O�Z������
                        strWork = CType(csDataRow(ABAtenaEntity.TENUMAEJ_KATAGAKI), String).Trim
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        ' �]�o�\��X�֔ԍ�
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO)
                        ' �]�o�\��S���Z���R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD)
                        ' �]�o�\��ٓ��N����
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD)
                        ' �]�o�\��Z��
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIJUSHO) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO)
                        ' �]�o�\��Ԓn
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIBANCHI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI)
                        ' �]�o�\�����
                        strWork = CType(csDataRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI), String).Trim
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        ' �]�o�m��X�֔ԍ�
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIYUBINNO) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO)
                        '*����ԍ� 000017 2003/10/09 �ǉ��J�n
                        ' �]�o�m��S���Z���R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD)
                        '*����ԍ� 000017 2003/10/09 �ǉ��I��
                        ' �]�o�m��ٓ��N����
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIIDOYMD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD)
                        ' �]�o�m��ʒm�N����
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD)
                        ' �]�o�m��Z��
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIJUSHO) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO)
                        ' �]�o�m��Ԓn
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIBANCHI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI)
                        ' �]�o�m�����
                        strWork = CType(csDataRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI), String).Trim
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            ' �]���O�Z�������i�t���j
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENUMAEJ_KATAGAKI)
                            ' �]�o�\������i�t���j
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI)
                            ' �]�o�m������i�t���j
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI)
                        Else
                        End If

                        '�Z��D��̏ꍇ
                        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                            ' �ҏW�O�Ԓn
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = csDataRow(ABAtenaEntity.JUKIBANCHI)
                            ' �ҏW�O����
                            strWork = CType(csDataRow(ABAtenaEntity.JUKIKATAGAKI), String).Trim
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                ' �ҏW�O�����i�t���j
                                csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaEntity.JUKIKATAGAKI)
                            Else
                            End If
                        Else
                            ' �ҏW�O�Ԓn
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = csDataRow(ABAtenaEntity.BANCHI)
                            ' �ҏW�O����
                            strWork = CType(csDataRow(ABAtenaEntity.KATAGAKI), String).Trim
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                ' �ҏW�O�����i�t���j
                                csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaEntity.KATAGAKI)
                            Else
                            End If
                        End If

                        ' �����͏o�N����
                        csDataNewRow(ABNenkinAtenaEntity.SHOJOTDKDYMD) = csDataRow(ABAtenaEntity.SHOJOTDKDYMD)
                        ' ���ߎ��R�R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.CKINJIYUCD) = csDataRow(ABAtenaEntity.CKINJIYUCD)

                        '*����ԍ� 000022 2003/12/04 �ǉ��J�n
                        ' �{�БS���Z���R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.HON_ZJUSHOCD) = csDataRow(ABAtenaEntity.HON_ZJUSHOCD)
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        ' �]�o�\�萢�ю喼
                        'csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)
                        ' �]�o�m�萢�ю喼
                        'csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            ' �]�o�\�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)))
                            ' �]�o�m�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            ' �]�o�\�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)
                            ' �]�o�m�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                        ' ���ЃR�[�h
                        csDataNewRow(ABNenkinAtenaEntity.KOKUSEKICD) = csDataRow(ABAtenaEntity.KOKUSEKICD)
                        '*����ԍ� 000022 2003/12/04 �ǉ��I��
                        '*����ԍ� 000027 2006/07/31 �ǉ��J�n
                        If strGyomuMei = NENKIN_2 Then
                            '* �����J�n 000035 2008/02/15 �C���J�n
                            '�]���O�Z�����ю喼
                            'csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = csDataRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI)
                            If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                                ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                                csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI)))
                            Else
                                ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                                csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = csDataRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI)
                            End If
                            '*����ԍ� 000022 2003/12/04 �ǉ��I��
                        End If
                        '*����ԍ� 000027 2006/07/31 �ǉ��I��
                    End If

                    '*����ԍ� 000030 2007/04/28 �ǉ��J�n
                    '���p�T�u���[�`���擾����
                    If m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo Then
                        ' ����
                        csDataNewRow(ABAtena1Entity.KYUSEI) = csDataRow(ABAtenaEntity.KYUSEI)
                        ' �Z��ٓ��N����
                        csDataNewRow(ABAtena1Entity.JUTEIIDOYMD) = csDataRow(ABAtenaEntity.JUTEIIDOYMD)
                        ' �Z�莖�R
                        csDataNewRow(ABAtena1Entity.JUTEIJIYU) = csDataRow(ABAtenaEntity.JUTEIJIYU)
                        ' �{�БS���Z���R�[�h
                        csDataNewRow(ABAtena1Entity.HON_ZJUSHOCD) = csDataRow(ABAtenaEntity.HON_ZJUSHOCD)
                        ' �]���O�Z���X�֔ԍ�
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_YUBINNO) = csDataRow(ABAtenaEntity.TENUMAEJ_YUBINNO)
                        ' �]���O�Z���S���Z���R�[�h
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_ZJUSHOCD) = csDataRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD)
                        ' �]���O�Z���Z��
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_JUSHO) = csDataRow(ABAtenaEntity.TENUMAEJ_JUSHO)
                        ' �]���O�Z���Ԓn
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_BANCHI) = csDataRow(ABAtenaEntity.TENUMAEJ_BANCHI)
                        ' �]���O�Z������
                        strWork = CType(csDataRow(ABAtenaEntity.TENUMAEJ_KATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        ' �]�o�\��X�֔ԍ�
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIYUBINNO) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO)
                        ' �]�o�\��S���Z���R�[�h
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIZJUSHOCD) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD)
                        ' �]�o�\��ٓ��N����
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIIDOYMD) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD)
                        ' �]�o�\��Z��
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIJUSHO) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO)
                        ' �]�o�\��Ԓn
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIBANCHI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI)
                        ' �]�o�\�����
                        strWork = CType(csDataRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            ' �]���O�Z�������i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENUMAEJ_KATAGAKI)
                            ' �]�o�\������i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI)
                        Else
                        End If
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        ' �]�o�\�萢�ю喼
                        'csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                        ' �]�o�m��X�֔ԍ�
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIYUBINNO) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO)
                        ' �]�o�m��S���Z���R�[�h
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIZJUSHOCD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD)
                        ' �]�o�m��ٓ��N����
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIIDOYMD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD)
                        ' �]�o�m��ʒm�N����
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTITSUCHIYMD) = csDataRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD)
                        ' �]�o�m��Z��
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIJUSHO) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO)
                        ' �]�o�m��Ԓn
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIBANCHI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI)
                        ' �]�o�m�����
                        strWork = CType(csDataRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            ' �]�o�m������i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = csDataRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI)
                        Else
                        End If
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        ' �]�o�m�萢�ю喼
                        'csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��

                        '�Z��D��̏ꍇ
                        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                            ' �ҏW�O�Ԓn
                            csDataNewRow(ABAtena1Entity.HENSHUMAEBANCHI) = csDataRow(ABAtenaEntity.JUKIBANCHI)
                            ' �ҏW�O����
                            strWork = CType(csDataRow(ABAtenaEntity.JUKIKATAGAKI), String).Trim
                            csDataNewRow(ABAtena1Entity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                ' �ҏW�O�����i�t���j
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaEntity.JUKIKATAGAKI)
                            Else
                            End If
                        Else
                            ' �ҏW�O�Ԓn
                            csDataNewRow(ABAtena1Entity.HENSHUMAEBANCHI) = csDataRow(ABAtenaEntity.BANCHI)
                            ' �ҏW�O����
                            strWork = CType(csDataRow(ABAtenaEntity.KATAGAKI), String).Trim
                            csDataNewRow(ABAtena1Entity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                ' �ҏW�O�����i�t���j
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaEntity.KATAGAKI)
                            Else
                            End If
                        End If

                        ' �����͏o�N����
                        csDataNewRow(ABAtena1Entity.SHOJOTDKDYMD) = csDataRow(ABAtenaEntity.SHOJOTDKDYMD)
                        ' ���ߎ��R�R�[�h
                        csDataNewRow(ABAtena1Entity.CKINJIYUCD) = csDataRow(ABAtenaEntity.CKINJIYUCD)
                        ' ���ЃR�[�h
                        csDataNewRow(ABAtena1Entity.KOKUSEKICD) = csDataRow(ABAtenaEntity.KOKUSEKICD)
                        ' �o�^�͏o�N����
                        csDataNewRow(ABAtena1Entity.TOROKUTDKDYMD) = csDataRow(ABAtenaEntity.TOROKUTDKDYMD)
                        ' �Z��͏o�N����
                        csDataNewRow(ABAtena1Entity.JUTEITDKDYMD) = csDataRow(ABAtenaEntity.JUTEITDKDYMD)
                        ' �]�o�����R
                        csDataNewRow(ABAtena1Entity.TENSHUTSUNYURIYU) = csDataRow(ABAtenaEntity.TENSHUTSUNYURIYU)
                        ' �s�����R�[�h
                        csDataNewRow(ABAtena1Entity.SHICHOSONCD) = csDataRow(ABAtenaEntity.SHICHOSONCD)

                        If (Not csDataRow(ABAtenaEntity.CKINJIYUCD).ToString.Trim = String.Empty) AndAlso
                            (csDataRow(ABAtenaEntity.CKINIDOYMD).ToString.Trim = String.Empty) Then
                            csDataNewRow(ABAtena1Entity.CKINIDOYMD) = m_strCknIdobiHenkanParam
                        Else

                            ' ���߈ٓ��N����
                            csDataNewRow(ABAtena1Entity.CKINIDOYMD) = csDataRow(ABAtenaEntity.CKINIDOYMD)
                        End If
                        ' �X�V����
                        csDataNewRow(ABAtena1Entity.KOSHINNICHIJI) = csDataRow(ABAtenaEntity.KOSHINNICHIJI)
                    End If
                    '*����ԍ� 000030 2007/04/28 �ǉ��I��

                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '*����ԍ� 000019 2003/11/19 �ǉ��J�n
                ' �����ʏ��p�f�[�^�쐬(�{�l���R�[�h�̂ݐݒ�)
                If (strGyomuMei = KOBETSU) And (strDainoKB.Trim = String.Empty) Then
                    ' ��b�N���ԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.KSNENKNNO) = csDataRow(ABAtena1KobetsuEntity.KSNENKNNO)
                    ' �N�����i�擾�N����	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD)
                    ' �N�����i�擾���	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU)
                    ' �N�����i�擾���R�R�[�h	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD)
                    ' �N�����i�r���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD)
                    ' �N�����i�r�����R�R�[�h	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD)
                    ' �󋋔N���L���P	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO1)
                    ' �󋋔N���ԍ��P	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO1)
                    ' �󋋔N����ʂP	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU1)
                    ' �󋋔N���}�ԂP	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN1)
                    ' �󋋔N���敪�P	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB1)
                    ' �󋋔N���L���Q	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO2)
                    ' �󋋔N���ԍ��Q	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO2)
                    ' �󋋔N����ʂQ	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU2)
                    ' �󋋔N���}�ԂQ	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN2)
                    ' �󋋔N���敪�Q	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB2)
                    ' �󋋔N���L���R	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO3)
                    ' �󋋔N���ԍ��R	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO3)
                    ' �󋋔N����ʂR	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU3)
                    ' �󋋔N���}�ԂR	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN3)
                    ' �󋋔N���敪�R	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB3)
                    ' ���۔ԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHONO) = csDataRow(ABAtena1KobetsuEntity.KOKUHONO)
                    ' ���ێ��i�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB)
                    ' ���ێ��i�敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO)
                    ' ���ێ��i�敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO)
                    ' ���ۊw���敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKB)
                    ' ���ۊw���敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO)
                    ' ���ۊw���敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO)
                    ' ���ێ擾�N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD)
                    ' ���ۑr���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD)
                    ' ���ۑސE�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKB)
                    ' ���ۑސE�敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO)
                    ' ���ۑސE�敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO)
                    ' ���ۑސE�{��敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB)
                    ' ���ۑސE�{��敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO)
                    ' ���ۑސE�{��敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
                    ' ���ۑސE�Y���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD)
                    ' ���ۑސE��Y���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD)
                    ' ���ەی��؋L��	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO)
                    ' ���ەی��ؔԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO)
                    ' ��Ӕԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.INKANNO) = csDataRow(ABAtena1KobetsuEntity.INKANNO)
                    ' ��ӓo�^�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.INKANTOROKUKB) = csDataRow(ABAtena1KobetsuEntity.INKANTOROKUKB)
                    ' �I�����i�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB)
                    ' �����p�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB)
                    ' ����J�n�N����	
                    csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATESTYM) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATESTYM)
                    ' ����I���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATEEDYM) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATEEDYM)
                    ' ����ی��Ҕԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGHIHKNSHANO) = csDataRow(ABAtena1KobetsuEntity.KAIGHIHKNSHANO)
                    ' ��쎑�i�擾��	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD)
                    ' ��쎑�i�r����	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD)
                    ' ��쎑�i��ی��ҋ敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB)
                    ' ���Z���n����ҋ敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB) = csDataRow(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB)
                    ' ���󋋎ҋ敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB)
                    ' �v����ԋ敪�R�[�h	
                    csDataNewRow(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD) = csDataRow(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD)
                    ' �v����ԋ敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKKB) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKKB)
                    ' ���F��L���J�n��	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD)
                    ' ���F��L���I����	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD)
                    ' ���󋋔F��N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD)
                    ' ���󋋔F�����N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD)

                    '*����ԍ� 000034 2008/01/15 �ǉ��J�n
                    If (m_strKobetsuShutokuKB = "1") Then
                        ' �ʎ����擾�敪��"1"�̏ꍇ�͌������ڂ�ǉ�����
                        ' ���i�敪
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB)
                        ' ��ی��Ҕԍ�
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO)
                        ' ��ی��Ҏ��i�擾���R�R�[�h
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD)
                        ' ��ی��Ҏ��i�擾���R����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI)
                        ' ��ی��Ҏ��i�擾�N����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD)
                        ' ��ی��Ҏ��i�r�����R�R�[�h
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD)
                        ' ��ی��Ҏ��i�r�����R����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI)
                        ' ��ی��Ҏ��i�r���N����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD)
                        ' �ی��Ҕԍ��K�p�J�n�N����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD)
                        ' �ی��Ҕԍ��K�p�I���N����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD)
                    Else
                        ' �ʎ����擾�敪���l�Ȃ��̏ꍇ�͌������ڂ�ǉ����Ȃ�
                    End If
                    '*����ԍ� 000034 2008/01/15 �ǉ��I��

                End If
                '*����ԍ� 000019 2003/11/19 �ǉ��I��

                '*����ԍ� 000046 2011/11/07 �ǉ��J�n
                '�Z��@��������
                If (m_strJukiHokaiseiKB_Param = "1") Then
                    '�Z���[��ԋ敪
                    csDataNewRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN) = csDataRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN)
                    '�Z���n�͏o�L���t���O
                    csDataNewRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG) = csDataRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG)
                    '�{����
                    csDataNewRow(ABAtenaFZYEntity.HONGOKUMEI) = csDataRow(ABAtenaFZYEntity.HONGOKUMEI)
                    '�J�i�{����
                    csDataNewRow(ABAtenaFZYEntity.KANAHONGOKUMEI) = csDataRow(ABAtenaFZYEntity.KANAHONGOKUMEI)
                    '���L��
                    csDataNewRow(ABAtenaFZYEntity.KANJIHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KANJIHEIKIMEI)
                    '�J�i���L��
                    csDataNewRow(ABAtenaFZYEntity.KANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KANAHEIKIMEI)
                    '�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.KANJITSUSHOMEI) = csDataRow(ABAtenaFZYEntity.KANJITSUSHOMEI)
                    '�J�i�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.KANATSUSHOMEI) = csDataRow(ABAtenaFZYEntity.KANATSUSHOMEI)
                    '�J�^�J�i���L��
                    csDataNewRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI)
                    '���N�����s�ڋ敪
                    csDataNewRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = csDataRow(ABAtenaFZYEntity.UMAREFUSHOKBN)
                    '�ʏ̖��o�^�i�ύX�j�N����
                    csDataNewRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD) = csDataRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD)
                    '�ݗ����ԃR�[�h
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANCD) = csDataRow(ABAtenaFZYEntity.ZAIRYUKIKANCD)
                    '�ݗ����Ԗ���
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO) = csDataRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO)
                    '�������ݗ��҂ł���|���̃R�[�h
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHACD) = csDataRow(ABAtenaFZYEntity.ZAIRYUSHACD)
                    '�������ݗ��҂ł���|��
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO) = csDataRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO)
                    '�ݗ��J�[�h���ԍ�
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUCARDNO) = csDataRow(ABAtenaFZYEntity.ZAIRYUCARDNO)
                    '���ʉi�Z�ҏؖ�����t�N����
                    csDataNewRow(ABAtenaFZYEntity.KOFUYMD) = csDataRow(ABAtenaFZYEntity.KOFUYMD)
                    '���ʉi�Z�ҏؖ�����t�\����ԊJ�n��
                    csDataNewRow(ABAtenaFZYEntity.KOFUYOTEISTYMD) = csDataRow(ABAtenaFZYEntity.KOFUYOTEISTYMD)
                    '����i�Z�ҏؖ�����t�\����ԏI����
                    csDataNewRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD) = csDataRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD)
                    '�Z��Ώێҁi��30��45��Y���j�����ٓ��N����
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
                    '�Z��Ώێҁi��30��45��Y���j�������R�R�[�h
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
                    '�Z��Ώێҁi��30��45��Y���j�������R
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU)
                    '�Z��Ώێҁi��30��45��Y���j�����͏o�N����
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
                    '�Z��Ώێҁi��30��45��Y���j�����͏o�ʒm�敪
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
                    '�O���l���ю喼
                    csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSMEI) = csDataRow(ABAtenaFZYEntity.FRNSTAINUSMEI)
                    '�O���l���ю�J�i��
                    csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI) = csDataRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI)
                    '���ю啹�L��
                    csDataNewRow(ABAtenaFZYEntity.STAINUSHEIKIMEI) = csDataRow(ABAtenaFZYEntity.STAINUSHEIKIMEI)
                    '���ю�J�i���L��
                    csDataNewRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI)
                    '���ю�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI) = csDataRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI)
                    '���ю�J�i�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI) = csDataRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI)
                Else
                    '�����Ȃ�
                End If
                '*����ԍ� 000046 2011/11/07 �ǉ��I��

                '*����ԍ� 000048 2014/04/28 �ǉ��J�n
                ' ���ʔԍ�����
                If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                    ' �󔒏��������l��ݒ肷��B
                    csDataNewRow(ABMyNumberEntity.MYNUMBER) = csDataRow(ABMyNumberEntity.MYNUMBER).ToString.Trim
                Else
                    ' noop
                End If
                '*����ԍ� 000048 2014/04/28 �ǉ��I��

                If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    ' ���ю厁���D��敪
                    csDataNewRow(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB) = csDataRow(ABAtenaFZYHyojunEntity.STAINUSSHIMEIYUSENKB)
                    ' �����D�捀��
                    csDataNewRow(ABAtena1HyojunEntity.SHIMEIYUSENKB) = csDataRow(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB)
                    ' ����
                    csDataNewRow(ABAtena1HyojunEntity.KANJIKYUUJI) = csDataRow(ABAtenaFZYEntity.RESERVE7)
                    ' �J�i����
                    csDataNewRow(ABAtena1HyojunEntity.KANAKYUUJI) = csDataRow(ABAtenaFZYEntity.RESERVE8)
                    ' �����t���K�i�m�F�t���O
                    csDataNewRow(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG) = csDataRow(ABAtenaHyojunEntity.SHIMEIKANAKAKUNINFG)
                    ' �����t���K�i�m�F�t���O
                    csDataNewRow(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG) = csDataRow(ABAtenaHyojunEntity.KYUUJIKANAKAKUNINFG)
                    ' �ʏ̃t���K�i�m�F�t���O
                    csDataNewRow(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG) = csDataRow(ABAtenaFZYHyojunEntity.TSUSHOKANAKAKUNINFG)
                    ' ���N�����s�ڃp�^�[��
                    csDataNewRow(ABAtena1HyojunEntity.UMAREBIFUSHOPTN) = csDataRow(ABAtenaHyojunEntity.UMAREBIFUSHOPTN)
                    ' �s�ڐ��N����
                    csDataNewRow(ABAtena1HyojunEntity.FUSHOUMAREBI) = csDataRow(ABAtenaHyojunEntity.FUSHOUMAREBI)
                    ' �L�ڎ��R
                    csDataNewRow(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD) = csDataRow(ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD)
                    ' �L�ڔN����
                    csDataNewRow(ABAtena1HyojunEntity.KISAIYMD) = csDataRow(ABAtenaHyojunEntity.KISAIYMD)
                    ' �������R
                    csDataNewRow(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD) = csDataRow(ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD)

                    If ((csDataRow(ABAtenaEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTONAI_KOJIN) OrElse
                        (csDataRow(ABAtenaEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTOGAI_KOJIN)) AndAlso
                       (Not csDataRow(ABAtenaEntity.SHOJOJIYUCD).ToString.Trim = String.Empty) Then
                        If (csDataRow(ABAtenaHyojunEntity.SHOJOIDOWMD).ToString.Trim = String.Empty) Then
                            csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = m_strShojoIdoWmdHenkan
                        Else
                            csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csDataRow(ABAtenaHyojunEntity.SHOJOIDOWMD)
                        End If
                    Else
                        ' �����ٓ��a��N����
                        csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csDataRow(ABAtenaHyojunEntity.SHOJOIDOWMD)
                    End If
                    ' �����ٓ����s�ڃp�^�[��
                    csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN) = csDataRow(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN)
                    ' �s�ڏ����ٓ���
                    csDataNewRow(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI) = csDataRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI)

                    If (Not csDataRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI).ToString.Trim = String.Empty) AndAlso
                       (csDataRow(ABAtenaHyojunEntity.CKINIDOWMD).ToString.Trim = String.Empty) Then
                        csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = m_strCknIdoWmdHenkan
                    Else
                        ' ���߈ٓ��a��N����
                        csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = csDataRow(ABAtenaHyojunEntity.CKINIDOWMD)
                    End If
                    ' ���߈ٓ����s�ڃp�^�[��
                    csDataNewRow(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN) = csDataRow(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN)
                    ' �s�ڒ��߈ٓ���
                    csDataNewRow(ABAtena1HyojunEntity.FUSHOCKINIDOBI) = csDataRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI)
                    ' ������̐��ю�
                    csDataNewRow(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI) = csDataRow(ABAtenaHyojunEntity.JIJITSUSTAINUSMEI)
                    If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                        ' �Z��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.JUKISHIKUCHOSONCD)
                        ' �Z��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.JUKIMACHIAZACD)
                        ' �Z��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.JUKITODOFUKEN)
                        ' �Z��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.JUKISHIKUCHOSON)
                        ' �Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csDataRow(ABAtenaHyojunEntity.JUKIMACHIAZA)
                    Else
                        ' �Z��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.SHIKUCHOSONCD)
                        ' �Z��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.MACHIAZACD)
                        ' �Z��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TODOFUKEN)
                        ' �Z��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.SHIKUCHOSON)
                        ' �Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csDataRow(ABAtenaHyojunEntity.MACHIAZA)
                    End If
                    If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                        ' �{��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.HON_SHIKUCHOSONCD)
                        ' �{��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.HON_MACHIAZACD)
                        ' �{��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.HON_TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.HON_TODOFUKEN)
                        ' �{��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON) = csDataRow(ABAtenaHyojunEntity.HON_SHIKUGUNCHOSON)
                        ' �{��_����
                        csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZA) = csDataRow(ABAtenaHyojunEntity.HON_MACHIAZA)
                    End If
                    If (m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo) AndAlso
                       (strGyomuMei <> NENKIN) AndAlso (strGyomuMei <> NENKIN_2) Then
                        ' ���ЃR�[�h
                        csDataNewRow(ABAtena1HyojunEntity.KOKUSEKICD) = csDataRow(ABAtenaEntity.KOKUSEKICD)
                    End If
                    If (strGyomuMei = NENKIN Or strGyomuMei = NENKIN_2) Then
                        ' �]���O�Z��_�s�撬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD)
                        ' �]���O�����R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD)
                        ' �]���O�Z��_�s���{��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN)
                        ' �]���O�Z��_�s��S������
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON)
                        ' �]���O�Z��_����
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA)
                        ' �]���O�Z��_�����R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD)
                        ' �]���O�Z��_����
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKI) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI)
                        ' �]���O�Z��_���O�Z��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO)
                        ' �]�o�m��_�s�撬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD)
                        ' �]�o�m�蒬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD)
                        ' �]�o�m��_�s���{��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN)
                        ' �]�o�m��_�s��S������
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON)
                        ' �]�o�m��_����
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA)
                        ' �]�o�\��_�s�撬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD)
                        ' �]�o�\�蒬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD)
                        ' �]�o�\��_�s���{��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN)
                        ' �]�o�\��_�s��S������
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON)
                        ' �]�o�\��_����
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA)
                        ' �]�o�\��_�����R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD)
                        ' �]�o�\��_������
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI)
                        ' �]�o�\��_���O�Z��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO)
                    End If
                    If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo) Then
                        ' �]���O�Z��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD)
                        ' �]���O�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD)
                        ' �]���O�Z��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_TODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN)
                        ' �]���O�Z��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON)
                        ' �]���O�Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA)
                        ' �]���O�Z��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKICD) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD)
                        ' �]���O�Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKI) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI)
                        ' �]���O�Z��_���O�Z��
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csDataRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO)
                        ' �]�o�m��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD)
                        ' �]�o�m�蒬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD)
                        ' �]�o�m��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTITODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN)
                        ' �]�o�m��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON)
                        ' �]�o�m��_����
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA)
                        ' �]�o�\��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD)
                        ' �]�o�\�蒬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD)
                        ' �]�o�\��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN)
                        ' �]�o�\��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON)
                        ' �]�o�\��_����
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA)
                        ' �]�o�\��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD)
                        ' �]�o�\��_������
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI)
                        ' �]�o�\��_���O�Z��
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csDataRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO)
                    Else
                    End If
                    ' �@��30��46����47�敪
                    csDataNewRow(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB) = csDataRow(ABAtenaFZYHyojunEntity.HODAI30JO46MATAHA47KB)
                    ' �ݗ��J�[�h���ԍ��敪
                    csDataNewRow(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN) = csDataRow(ABAtenaFZYHyojunEntity.ZAIRYUCARDNOKBN)
                    ' �Z���n�␳�R�[�h
                    csDataNewRow(ABAtena1HyojunEntity.JUKYOCHIHOSEICD) = csDataRow(ABAtenaFZYHyojunEntity.JUKYOCHIHOSEICD)
                    ' ���ߓ͏o�ʒm�敪
                    csDataNewRow(ABAtena1HyojunEntity.CKINTDKDTUCIKB) = csDataRow(ABAtenaEntity.CKINTDKDTUCIKB)
                    ' �Ŕԍ�
                    csDataNewRow(ABAtena1HyojunEntity.HANNO) = csDataRow(ABAtenaEntity.HANNO)
                    ' �����N����
                    csDataNewRow(ABAtena1HyojunEntity.KAISEIYMD) = csDataRow(ABAtenaEntity.KAISEIYMD)
                    ' �ٓ��敪
                    csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOKB) = csDataRow(ABAtenaHyojunEntity.HYOJUNIDOKB)
                    ' ���͏ꏊ�R�[�h
                    csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHOCD) = csDataRow(ABAtenaHyojunEntity.NYURYOKUBASHOCD)
                    ' ���͏ꏊ�\�L
                    csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHO) = csDataRow(ABAtenaHyojunEntity.NYURYOKUBASHO)
                    If (strGyomuMei = KOBETSU) And (strDainoKB.Trim = String.Empty) Then
                        ' ���_��ی��ҊY���L��
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB)
                        ' ����_��ی��ҊY���L��
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB)
                        ' �N��_��ی��ҊY���L��
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB)
                        ' �N��_��ʕύX�N����
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD) = csDataRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD)
                        ' �I��_��ԋ敪
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN) = csDataRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN)
                        If (m_strKobetsuShutokuKB = "1") Then
                            ' �������_��ی��ҊY���L��
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB)
                        End If
                    End If
                    ' �A����敪�i�A����j
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIKB) = String.Empty
                    ' �A���於
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIMEI) = String.Empty
                    ' �A����1�i�A����j
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI) = String.Empty
                    ' �A����2�i�A����j
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI) = String.Empty
                    ' �A����3�i�A����j
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI) = String.Empty
                    ' �A������1
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1) = String.Empty
                    ' �A������2
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2) = String.Empty
                    ' �A������3
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3) = String.Empty
                    '* ����ԍ� 000051 2023/10/19 �C���J�n
                    'If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) Then
                    If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) AndAlso
                       (csDataRow.Table.Columns.Contains(ABFugenjuJohoEntity.FUGENJUKB)) Then
                        '* ����ԍ� 000051 2023/10/19 �C���I��
                        ' �s���Z�敪
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUKB) = csDataRow(ABFugenjuJohoEntity.FUGENJUKB)
                        ' �s���Z�������Z��_�X�֔ԍ�
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_YUBINNO)
                        ' �s���Z�������Z��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHICHOSONCD)
                        ' �s���Z�������Z��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZACD)
                        ' �s���Z�������Z��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_TODOFUKEN)
                        ' �s���Z�������Z��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON)
                        ' �s���Z�������Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZA)
                        ' �s���Z�������Z��_�Ԓn���\�L
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)
                        ' �s���Z�������Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)
                        ' �s���Z�������Z��_����_�t���K�i
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI)
                        ' �s���Z���i�Ώێҋ敪�j
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKUBUN)
                        ' �s���Z���i�ΏێҎ����j
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI)
                        ' �s���Z���i���N�����j
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD)
                        ' �s���Z���i���ʁj
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU)
                        ' ���Z�s���N����
                        csDataNewRow(ABAtena1HyojunEntity.KYOJUFUMEI_YMD) = csDataRow(ABFugenjuJohoEntity.KYOJUFUMEI_YMD)
                        ' �s���Z���i���l�j
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_BIKO)
                    Else
                    End If
                    If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                        ' �ԍ��@�X�V�敪
                        csDataNewRow(ABAtena1HyojunEntity.BANGOHOKOSHINKB) = csDataRow(ABMyNumberHyojunEntity.BANGOHOKOSHINKB)
                    End If
                    '* ����ԍ� 000051 2023/10/19 �C���J�n
                    'If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1) AndAlso (strGyomuMei <> NENKIN) AndAlso (strGyomuMei <> NENKIN_2) Then
                    If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1) AndAlso (strGyomuMei <> NENKIN) AndAlso (strGyomuMei <> NENKIN_2) AndAlso
                       (csDataRow.Table.Columns.Contains(ABDENSHISHOMEISHOMSTEntity.SERIALNO)) Then
                        '* ����ԍ� 000051 2023/10/19 �C���I��
                        ' �V���A���ԍ�
                        csDataNewRow(ABAtena1HyojunEntity.SERIALNO) = csDataRow(ABDENSHISHOMEISHOMSTEntity.SERIALNO)
                    End If
                    ' �W�������ٓ����R�R�[�h
                    csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD) = csDataRow(ABAtenaHyojunEntity.HYOJUNIDOJIYUCD)
                    If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) Then
                        ' �A����敪�i���t��j
                        csDataNewRow(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB) = String.Empty
                        ' ���t��敪
                        csDataNewRow(ABAtena1HyojunEntity.SFSKKBN) = String.Empty
                    Else
                    End If

                    strAtenaDataKB = CType(csDataRow(ABAtenaEntity.ATENADATAKB), String).Trim
                    strAtenaDataSHU = CType(csDataRow(ABAtenaEntity.ATENADATASHU), String).Trim
                    m_cABHyojunkaCdHenshuB.HenshuHyojunkaCd(strAtenaDataKB, strAtenaDataSHU)
                    ' �Z���敪
                    csDataNewRow(ABAtena1HyojunEntity.JUMINKBN) = m_cABHyojunkaCdHenshuB.p_strJuminKbn
                    ' �Z�����
                    csDataNewRow(ABAtena1HyojunEntity.JUMINSHUBETSU) = m_cABHyojunkaCdHenshuB.p_strJuminShubetsu
                    ' �Z�����
                    csDataNewRow(ABAtena1HyojunEntity.JUMINJOTAI) = m_cABHyojunkaCdHenshuB.p_strJuminJotai
                    If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                        ' �Ԓn�}�Ԑ��l
                        csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = csDataRow(ABAtenaHyojunEntity.JUKIBANCHIEDABANSUCHI)
                    Else
                        ' �Ԓn�}�Ԑ��l
                        csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = csDataRow(ABAtenaHyojunEntity.BANCHIEDABANSUCHI)
                    End If
                Else
                    ' noop
                End If

                '*����ԍ� 000026 2005/12/21 �ǉ��J�n
                csDataNewRow.EndEdit()
                '*����ԍ� 000026 2005/12/21 �ǉ��I��

                '�f�[�^���R�[�h�̒ǉ�
                csDataTable.Rows.Add(csDataNewRow)
                '*����ԍ� 000013 2003/04/18 �C���I��

            Next csDataRow

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exException As UFAppException

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �X���[����
            Throw exException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csAtena1

    End Function
#End Region

#Region " �N�������ҏW(NenkinAtenaHenshu) "
    '*����ԍ� 000013 2003/04/18 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �N�������ҏW
    '* 
    '* �\��           Public Function NenkinAtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                           ByVal csAtenaEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@�N���ҏW�����f�[�^���쐬����
    '* 
    '* ����         cAtenaGetPara1      : �����擾�p�����[�^
    '*              csAtenaEntity       : �����f�[�^
    '* 
    '* �߂�l       DataSet(ABNenkinAtena)   : �擾�����N���p�������
    '************************************************************************************************
    Public Overloads Function NenkinAtenaHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                                ByVal csAtenaEntity As DataSet) As DataSet
        Return Me.AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "", NENKIN)
    End Function
    '*����ԍ� 000013 2003/04/18 �ǉ��I��
    '*����ԍ� 000017 2003/10/09 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �N������ҏW
    '* 
    '* �\��           Public Function NenkinRirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                                  ByVal csAtenaEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@�N���ҏW�����f�[�^���쐬����
    '* 
    '* ����         cAtenaGetPara1      : �����擾�p�����[�^
    '*              csAtenaEntity       : �����f�[�^
    '* 
    '* �߂�l       DataSet(ABNenkinAtena)   : �擾�����N���p�������
    '************************************************************************************************
    Public Overloads Function NenkinRirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                                 ByVal csAtenaEntity As DataSet) As DataSet
        Return Me.RirekiHenshu(cAtenaGetPara1, csAtenaEntity, String.Empty, String.Empty, String.Empty, NENKIN)
    End Function
    '*����ԍ� 000017 2003/10/09 �ǉ��I��
#End Region

#Region " �N�������ҏW�U(NenkinAtenaHenshu2) "
    '*����ԍ� 000027 2006/07/31 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �N�������ҏW�U
    '* 
    '* �\��           Public Function NenkinAtenaHenshu2(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                           ByVal csAtenaEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@�N���ҏW�����f�[�^���쐬����
    '* 
    '* ����         cAtenaGetPara1      : �����擾�p�����[�^
    '*              csAtenaEntity       : �����f�[�^
    '* 
    '* �߂�l       DataSet(ABNenkinAtena)   : �擾�����N���p�������
    '************************************************************************************************
    Public Overloads Function NenkinAtenaHenshu2(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                                ByVal csAtenaEntity As DataSet) As DataSet
        Return Me.AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "", NENKIN_2)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     �N������ҏW�U
    '* 
    '* �\��           Public Function NenkinRirekiHenshu2(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                                  ByVal csAtenaEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@�N���ҏW�����f�[�^���쐬����
    '* 
    '* ����         cAtenaGetPara1      : �����擾�p�����[�^
    '*              csAtenaEntity       : �����f�[�^
    '* 
    '* �߂�l       DataSet(ABNenkinAtena)   : �擾�����N���p�������
    '************************************************************************************************
    Public Overloads Function NenkinRirekiHenshu2(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                                 ByVal csAtenaEntity As DataSet) As DataSet
        Return Me.RirekiHenshu(cAtenaGetPara1, csAtenaEntity, String.Empty, String.Empty, String.Empty, NENKIN_2)
    End Function
    '*����ԍ� 000027 2006/07/31 �ǉ��I��
#End Region

#Region " �����ʕҏW(AtenaKobetsuHenshu) "
    '*����ԍ� 000019 2003/11/19 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����ʕҏW
    '* 
    '* �\��           Friend Function AtenaKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                           ByVal csAtenaEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@�����ʕҏW�f�[�^���쐬����
    '* 
    '* ����         cAtenaGetPara1      : �����擾�p�����[�^
    '*              csAtenaEntity       : �����f�[�^
    '* 
    '* �߂�l       DataSet(ABAtena1Kobetsu)   : �擾���������ʕҏW
    '************************************************************************************************
    Friend Function AtenaKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                                 ByVal csAtenaEntity As DataSet) As DataSet
        Return Me.AtenaHenshu(cAtenaGetPara1, csAtenaEntity, "", "", "", KOBETSU)
    End Function
    '************************************************************************************************
    '* ���\�b�h��     �����ʕҏW
    '* 
    '* �\��           Friend Function AtenaKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                           ByVal csAtenaEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@�����ʕҏW�f�[�^���쐬����
    '* 
    '* ����         cAtenaGetPara1      : �����擾�p�����[�^
    '* �@�@         csAtenaEntity       : �����f�[�^
    '* �@�@         strDainoKB          : ��[�敪
    '* �@�@         strGyomuCD          : �Ɩ��R�[�h
    '* �@�@         strGyomunaiSHU_CD   : �Ɩ�����ʃR�[�h
    '* 
    '* �߂�l       DataSet(ABAtena1Kobetsu)   : �擾���������ʕҏW
    '************************************************************************************************
    Friend Function AtenaKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                        ByVal csAtenaEntity As DataSet,
                                        ByVal strDainoKB As String,
                                        ByVal strGyomuCD As String,
                                        ByVal strGyomunaiSHU_CD As String) As DataSet
        Return Me.AtenaHenshu(cAtenaGetPara1, csAtenaEntity, strDainoKB, strGyomuCD, strGyomunaiSHU_CD, KOBETSU)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���������ʕҏW
    '* 
    '* �\��           Friend Function RirekiKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                                  ByVal csAtenaEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@���������ʕҏW�f�[�^���쐬����
    '* 
    '* ����         cAtenaGetPara1      : �����擾�p�����[�^
    '*              csAtenaEntity       : �����f�[�^
    '* 
    '* �߂�l       DataSet(ABAtena1Kobetsu)   : �擾�������������ʕҏW
    '************************************************************************************************
    Friend Overloads Function RirekiKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                                  ByVal csAtenaEntity As DataSet) As DataSet
        Return Me.RirekiHenshu(cAtenaGetPara1, csAtenaEntity, String.Empty, String.Empty, String.Empty, KOBETSU)
    End Function
    '************************************************************************************************
    '* ���\�b�h��     ���������ʕҏW
    '* 
    '* �\��           Friend Function RirekiKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1,
    '*                                                  ByVal csAtenaEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@���������ʕҏW�f�[�^���쐬����
    '* 
    '* ����          cAtenaGetPara1         : �����擾�p�����[�^
    '* �@�@          csAtenaRirekiEntity    : ���������f�[�^
    '* �@�@          strDainoKB             : ��[�敪
    '* �@�@          strGyomuMei            : �Ɩ���
    '* 
    '* �߂�l       DataSet(ABAtena1Kobetsu)   : �擾�������������ʕҏW
    '************************************************************************************************
    Friend Overloads Function RirekiKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                                    ByVal csAtenaRirekiEntity As DataSet,
                                                    ByVal strDainoKB As String,
                                                    ByVal strGyomuCD As String,
                                                    ByVal strGyomunaiSHU_CD As String) As DataSet
        Return Me.RirekiHenshu(cAtenaGetPara1, csAtenaRirekiEntity, strDainoKB, strGyomuCD, strGyomunaiSHU_CD, KOBETSU)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��ʕҏW
    '* 
    '* �\��           Friend Function SofusakiKobetsuHenshu(ByVal csAtena1 As DataSet, _
    '*                                                      ByVal csSfskEntity As DataSet, _
    '*                                                      ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@ �ҏW�����f�[�^���쐬����
    '* 
    '* ����           csAtena1              : ���������f�[�^
    '*               csSfskEntity           : ���t��f�[�^
    '*               cAtenaGetPara1         : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena12)    : �擾�����������
    '************************************************************************************************
    Friend Function SofusakiKobetsuHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                          ByVal csAtena1 As DataSet,
                                          ByVal csSfskEntity As DataSet) As DataSet
        Return SofusakiHenshu(cAtenaGetPara1, csAtena1, csSfskEntity, KOBETSU)
    End Function
    '*����ԍ� 000019 2003/11/19 �ǉ��I��
#End Region

#Region " ����ҏW(RirekiHenshu) "
    '************************************************************************************************
    '* ���\�b�h��     ����ҏW
    '* 
    '* �\��           Public Function RirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1, _
    '*                                            ByVal csAtenaRirekiEntity As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@ �ҏW�����f�[�^���쐬����
    '* 
    '* ����           cAtenaGetPara1         : �����擾�p�����[�^
    '*               csAtenaRirekiEntity    : ���������f�[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Overloads Function RirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                           ByVal csAtenaRirekiEntity As DataSet) As DataSet

        '*����ԍ� 000017 2003/10/09 �C���J�n
        'Return RirekiHenshu(cAtenaGetPara1, csAtenaRirekiEntity, "", "", "")
        Return RirekiHenshu(cAtenaGetPara1, csAtenaRirekiEntity, String.Empty, String.Empty, String.Empty)
        '*����ԍ� 000017 2003/10/09 �C���I��
    End Function

    '*����ԍ� 000017 2003/10/09 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ����ҏW
    '* 
    '* �\��           Public Function RirekiHenshu(ByVal csAtenaRirekiEntity As DataSet, 
    '*                                            ByVal cAtenaGetPara1 As ABAtenaGetPara1, 
    '*                                            ByVal strDainoKB As String,
    '*                                            ByVal strGyomuCD As String,
    '*                                            ByVal strGyomunaiSHU_CD As String) As DataSet
    '* 
    '* �@�\�@�@    �@ �ҏW�����f�[�^���쐬����
    '* 
    '* ����           cAtenaGetPara1         : �����擾�p�����[�^
    '*               csAtenaRirekiEntity    : ���������f�[�^
    '*               strDainoKB             : ��[�敪
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Overloads Function RirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                           ByVal csAtenaRirekiEntity As DataSet,
                                           ByVal strDainoKB As String,
                                           ByVal strGyomuCD As String,
                                           ByVal strGyomunaiSHU_CD As String) As DataSet
        Return RirekiHenshu(cAtenaGetPara1, csAtenaRirekiEntity, strDainoKB, strGyomuCD, strGyomunaiSHU_CD, String.Empty)
    End Function
    '*����ԍ� 000017 2003/10/09 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     ����ҏW
    '* 
    '* �\��           Public Function RirekiHenshu(ByVal csAtenaRirekiEntity As DataSet, 
    '*                                            ByVal cAtenaGetPara1 As ABAtenaGetPara1, 
    '*                                            ByVal strDainoKB As String,
    '*                                            ByVal strGyomuCD As String,
    '*                                            ByVal strGyomunaiSHU_CD As String, _
    '*                                            ByVal strGyomuMei As String) As DataSet
    '* 
    '* �@�\�@�@    �@ �ҏW�����f�[�^���쐬����
    '* 
    '* ����           cAtenaGetPara1         : �����擾�p�����[�^
    '*               csAtenaRirekiEntity    : ���������f�[�^
    '*               strDainoKB             : ��[�敪
    '*               strGyomuMei            : �Ɩ���
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    '*����ԍ� 000017 2003/10/09 �C���J�n
    'Public Overloads Function RirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, _
    '                                    ByVal csAtenaRirekiEntity As DataSet, _
    '                                    ByVal strDainoKB As String, _
    '                                    ByVal strGyomuCD As String, _
    '                                    ByVal strGyomunaiSHU_CD As String) As DataSet
    Private Overloads Function RirekiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                             ByVal csAtenaRirekiEntity As DataSet,
                                             ByVal strDainoKB As String,
                                             ByVal strGyomuCD As String,
                                             ByVal strGyomunaiSHU_CD As String,
                                             ByVal strGyomuMei As String) As DataSet
        '*����ԍ� 000017 2003/10/09 �C���I��
        Const THIS_METHOD_NAME As String = "RirekiHenshu"
        'Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 Start 2010/04/16 000039
        'Dim csDataSet As DataSet
        '* corresponds to VS2008 End 2010/04/16 000039
        Dim csDataTable As DataTable
        Dim csDataRow As DataRow
        Dim csAtena1 As DataSet                             '�������(ABAtena1)
        Dim csDataNewRow As DataRow
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cuUSSCityInfo As USSCityInfoClass               '�s�������Ǘ��N���X
        'Dim cABDainoKankeiB As ABDainoKankeiBClass          '��[�֌W�N���X
        'Dim cABJuminShubetsuB As ABJuminShubetsuBClass      '�Z����ʃN���X
        'Dim cABHojinMeishoB As ABHojinMeishoBClass          '�@�l���̃N���X
        'Dim cABKjnhjnKBB As ABKjnhjnKBBClass                '�l�@�l�N���X
        'Dim cABKannaiKangaiKBB As ABKannaiKangaiKBBClass    '�Ǔ��ǊO�N���X
        'Dim cABUmareHenshuB As ABUmareHenshuBClass          '���N�����ҏW�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        'Dim csDainoKankeiCDMSTEntity As DataSet             '��[�֌WDataSet
        Dim csDainoKankeiCDMSTEntity As DataRow()             '��[�֌WDataRow()
        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j

        '* ����ԍ� 000024 2005/01/25 �폜�J�n�i�{��j
        'Dim strHenshuJusho As String                        '�ҏW�Z����
        '* ����ԍ� 000024 2005/01/25 �폜�I��

        Dim strHenshuKanaMeisho As String                   '�ҏW�J�i����
        Dim strHenshuKanjiShimei As String                  '�ҏW��������
        '*����ԍ� 000008 2003/03/17 �ǉ��J�n
        '*����ԍ� 000016 2003/08/22 �폜�J�n
        'Dim cURKanriJohoB As URKANRIJOHOBClass              '�Ǘ����擾�N���X
        '*����ԍ� 000016 2003/08/22 �폜�I��
        Dim cSofuJushoGyoseikuType As SofuJushoGyoseikuType
        Dim strJushoHenshu3 As String                       '�Z���ҏW�R
        Dim strJushoHenshu4 As String                       '�Z���ҏW�S
        '*����ԍ� 000008 2003/03/17 �ǉ��I��
        '*����ԍ� 000015 2003/04/30 �ǉ��J�n
        Dim csColumn As DataColumn
        '*����ԍ� 000015 2003/04/30 �ǉ��I��

        '*����ԍ� 000021 2003/12/02 �폜�J�n
        ''*����ԍ� 000017 2003/10/09 �ǉ��J�n
        'Dim cRenrakusakiBClass As ABRenrakusakiBClass       ' �A����a�N���X
        'Dim csRenrakusakiEntity As DataSet                  ' �A����DataSet
        'Dim csRenrakusakiRow As DataRow                     ' �A����Row
        ''*����ԍ� 000017 2003/10/09 �ǉ��I��
        '*����ԍ� 000021 2003/12/02 �폜�I��
        '* corresponds to VS2008 Start 2010/04/16 000039
        '*����ԍ� 000020 2003/12/01 �ǉ��J�n
        'Dim strRenrakusakiGyomuCD As String                 ' �A����Ɩ��R�[�h
        '*����ԍ� 000020 2003/12/01 �ǉ��I��
        '* corresponds to VS2008 End 2010/04/16 000039

        '* ����ԍ� 000026 2005/12/21 �ǉ��J�n
        Dim strWork As String
        '* ����ԍ� 000026 2005/12/21 �ǉ��I��
        '*����ԍ� 000042 2011/05/18 �ǉ��J�n
        Dim strMeisho(1) As String                          ' �{���ʏ̖��D�搧��p
        '*����ԍ� 000042 2011/05/18 �ǉ��I��
        Dim strAtenaDataKB As String
        Dim strAtenaDataSHU As String


        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ''�G���[�����N���X�̃C���X�^���X�쐬
            ''*����ԍ� 000010  2003/03/27 �C���J�n
            ''cfErrorClass = New UFErrorClass(m_cfUFControlData.m_strBusinessId)
            'cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            ''*����ԍ� 000010  2003/03/27 �C���I��

            '*����ԍ� 000017 2003/10/09 �C���J�n
            ''�J�������쐬
            'csAtena1 = New DataSet()
            'csAtena1.Tables.Add(Me.CreateAtena1Columns())

            '*����ԍ� 000019 2003/11/19 �C���J�n
            ''�J�������쐬
            'If (strGyomuMei = NENKIN) Then
            '    csDataTable = Me.CreateNenkinAtenaColumns()
            'Else
            '    csDataTable = Me.CreateAtena1Columns()
            'End If

            '*����ԍ� 000040 2010/05/14 �ǉ��J�n
            ' �{�ЕM���ҋ敪�p�����[�^�ɕϐ����Z�b�g
            m_strHonsekiHittoshKB_Param = cAtenaGetPara1.p_strHonsekiHittoshKB

            ' ������~�敪�p�����[�^�ɕϐ����Z�b�g
            m_strShoriteishiKB_Param = cAtenaGetPara1.p_strShoriTeishiKB
            '*����ԍ� 000040 2010/05/14 �ǉ��I��

            '*����ԍ� 000041 2011/05/18 �ǉ��J�n
            '�O���l�ݗ����擾�敪�p�����[�^�ɕϐ����Z�b�g
            m_strFrnZairyuJohoKB_Param = cAtenaGetPara1.p_strFrnZairyuJohoKB
            '*����ԍ� 000041 2011/05/18 �ǉ��I��
            '*����ԍ� 000046 2011/11/07 �ǉ��J�n
            ' �Z��@�����敪��ϐ��ɃZ�b�g
            m_strJukiHokaiseiKB_Param = cAtenaGetPara1.p_strJukiHokaiseiKB
            '*����ԍ� 000046 2011/11/07 �ǉ��I��
            '*����ԍ� 000048 2014/04/28 �ǉ��J�n
            ' ���ʔԍ��擾�敪��ϐ��ɃZ�b�g
            m_strMyNumberKB_Param = cAtenaGetPara1.p_strMyNumberKB
            '*����ԍ� 000048 2014/04/28 �ǉ��I��

            ' �J�������쐬
            Select Case strGyomuMei
                '*����ԍ� 000027 2006/07/31 �C���J�n
                Case NENKIN, NENKIN_2    ' �N���������
                    '*����ԍ� 000040 2010/05/14 �ǉ��J�n
                    m_blnNenKin = True
                    '*����ԍ� 000040 2010/05/14 �ǉ��I��

                    '*����ԍ� 000047 2012/03/13 �ǉ��J�n
                    m_blnKobetsu = False
                    m_strKobetsuShutokuKB = String.Empty
                    '*����ԍ� 000047 2012/03/13 �ǉ��I��
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataTable = Me.CreateNenkinAtenaHyojunColumns(strGyomuMei)
                    Else
                        csDataTable = Me.CreateNenkinAtenaColumns(strGyomuMei)
                    End If
                    'Case NENKIN     ' �N���������
                    '    csDataTable = Me.CreateNenkinAtenaColumns()
                    '*����ԍ� 000027 2006/07/31 �C���I��
                Case KOBETSU    ' �����ʏ��
                    '*����ԍ� 000034 2008/01/15 �ǉ��J�n
                    ' �ʎ����擾�敪�������o�ϐ��ɃZ�b�g
                    m_strKobetsuShutokuKB = cAtenaGetPara1.p_strKobetsuShutokuKB.Trim
                    '*����ԍ� 000034 2008/01/15 �ǉ��I��

                    '*����ԍ� 000040 2010/05/14 �ǉ��J�n
                    m_blnKobetsu = True
                    '*����ԍ� 000040 2010/05/14 �ǉ��I��

                    '*����ԍ� 000047 2012/03/13 �ǉ��J�n
                    m_blnNenKin = False
                    '*����ԍ� 000047 2012/03/13 �ǉ��I��
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataTable = Me.CreateAtena1KobetsuHyojunColumns()
                    Else
                        csDataTable = Me.CreateAtena1KobetsuColumns()
                    End If
                Case Else       ' �������
                    '*����ԍ� 000047 2012/03/13 �ǉ��J�n
                    m_blnKobetsu = False
                    m_blnNenKin = False
                    m_strKobetsuShutokuKB = String.Empty
                    '*����ԍ� 000047 2012/03/13 �ǉ��I��
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataTable = Me.CreateAtena1HyojunColumns()
                    Else
                        csDataTable = Me.CreateAtena1Columns()
                    End If
            End Select
            '*����ԍ� 000019 2003/11/19 �C���I��

            csAtena1 = New DataSet()
            csAtena1.Tables.Add(csDataTable)
            '*����ԍ� 000017 2003/10/09 �C���I��

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            ''�s�������̃C���X�^���X�쐬
            ''cuUSSCityInfo = New USSCityInfoClass()

            ''��[�֌W�̃C���X�^���X�쐬
            'cABDainoKankeiB = New ABDainoKankeiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)

            ''�Z����ʂ̃C���X�^���X�쐬
            'cABJuminShubetsuB = New ABJuminShubetsuBClass(m_cfUFControlData, m_cfUFConfigDataClass)

            ''�@�l���̂̃C���X�^���X�쐬
            'cABHojinMeishoB = New ABHojinMeishoBClass(m_cfUFControlData, m_cfUFConfigDataClass)

            ''�l�@�l�̃C���X�^���X�쐬
            'cABKjnhjnKBB = New ABKjnhjnKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

            ''�Ǔ��ǊO�̃C���X�^���X�쐬
            'cABKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)

            ''���N�����ҏW�N���X�̃C���X�^���X��
            'cABUmareHenshuB = New ABUmareHenshuBClass(m_cfUFControlData, m_cfUFConfigDataClass)
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '*����ԍ� 000008 2003/03/17 �ǉ��J�n
            '*����ԍ� 000016 2003/08/22 �폜�J�n
            ''�Ǘ����擾�a�̃C���X�^���X�쐬
            'cURKanriJohoB = New Densan.Reams.UR.UR001BB.URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            '*����ԍ� 000016 2003/08/22 �폜�I��
            '*����ԍ� 000008 2003/03/17 �ǉ��I��

            '*����ԍ� 000021 2003/12/02 �폜�J�n
            ''*����ԍ� 000017 2003/10/09 �ǉ��J�n
            '' �A����a�N���X�̃C���X�^���X�쐬
            'cRenrakusakiBClass = New ABRenrakusakiBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            ''*����ԍ� 000017 2003/10/09 �ǉ��I��
            '*����ԍ� 000021 2003/12/02 �폜�I��

            '*����ԍ� 000007 2003/03/17 �ǉ��J�n
            '�p�����[�^�̃`�F�b�N
            Me.CheckColumnValue(cAtenaGetPara1)
            '*����ԍ� 000007 2003/03/17 �ǉ��I��

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            ''�Z���ҏW�P��"1"���Z���ҏW�Q��"1"�̏ꍇ
            'If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu2 = "1" Then

            '    '���߂̎s���������擾����
            '    'm_cuUSSCityInfo.GetCityInfo(m_cfUFControlData)
            'End If
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '*����ԍ� 000008 2003/03/17 �ǉ��J�n
            '�Z���ҏW�P��"1"���Z���ҏW�R��""�̏ꍇ
            If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu3 = String.Empty Then
                '*����ԍ� 000016 2003/08/22 �C���J�n
                'cSofuJushoGyoseikuType = cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param

                cSofuJushoGyoseikuType = Me.GetSofuJushoGyoseikuType
                '*����ԍ� 000016 2003/08/22 �C���I��
                Select Case cSofuJushoGyoseikuType
                    Case SofuJushoGyoseikuType.Jusho_Banchi
                        strJushoHenshu3 = "1"
                        strJushoHenshu4 = ""
                    Case SofuJushoGyoseikuType.Jusho_Banchi_SP_Katagaki
                        strJushoHenshu3 = "1"
                        strJushoHenshu4 = "1"
                    Case SofuJushoGyoseikuType.Gyoseiku_SP_Banchi
                        strJushoHenshu3 = "5"
                        strJushoHenshu4 = ""
                    Case SofuJushoGyoseikuType.Gyoseiku_SP_Banchi_SP_Katagaki
                        strJushoHenshu3 = "5"
                        strJushoHenshu4 = "1"
                End Select
            Else
                strJushoHenshu3 = cAtenaGetPara1.p_strJushoHenshu3
                strJushoHenshu4 = cAtenaGetPara1.p_strJushoHenshu4
            End If
            '*����ԍ� 000008 2003/03/17 �ǉ��I��

            '�ҏW�����f�[�^���쐬����
            For Each csDataRow In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows
                '*����ԍ� 000017 2003/10/09 �C���J�n
                'csDataNewRow = csAtena1.Tables(ABAtena1Entity.TABLE_NAME).NewRow
                csDataNewRow = csDataTable.NewRow
                '*����ԍ� 000017 2003/10/09 �C���I��

                '*����ԍ� 000015 2003/04/30 �ǉ��J�n
                For Each csColumn In csDataNewRow.Table.Columns
                    csDataNewRow(csColumn) = String.Empty
                Next csColumn
                '*����ԍ� 000015 2003/04/30 �ǉ��I��

                '*����ԍ� 000021 2003/12/02 �폜�J�n
                ''*����ԍ� 000017 2003/10/09 �ǉ��J�n
                '' �Ɩ��R�[�h���w�肳�ꂽ�ꍇ
                'If (strGyomuCD <> String.Empty) Then

                '    ' �A����f�[�^���擾����
                '    csRenrakusakiEntity = cRenrakusakiBClass.GetRenrakusakiBHoshu(CType(csDataRow(ABAtenaEntity.JUMINCD), String), strGyomuCD, strGyomunaiSHU_CD)
                '    If (csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows.Count <> 0) Then
                '        csRenrakusakiRow = csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows(0)
                '    Else
                '        csRenrakusakiRow = Nothing
                '    End If
                'Else
                '    csRenrakusakiRow = Nothing
                'End If
                ''*����ԍ� 000017 2003/10/09 �ǉ��I��
                '*����ԍ� 000021 2003/12/02 �폜�I��

                '�Z���R�[�h
                csDataNewRow(ABAtena1Entity.JUMINCD) = csDataRow(ABAtenaRirekiEntity.JUMINCD)

                '��[�敪
                If strDainoKB = String.Empty Then
                    csDataNewRow(ABAtena1Entity.DAINOKB) = "00"
                Else
                    csDataNewRow(ABAtena1Entity.DAINOKB) = strDainoKB
                End If

                If CType(csDataNewRow(ABAtena1Entity.DAINOKB), String) = "00" Then
                    '��[�敪����
                    csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty
                    '��[�敪��������
                    csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty
                Else
                    '��[�֌W�f�[�^���擾����

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                    'csDainoKankeiCDMSTEntity = m_cABDainoKankeiB.GetDainoKBHoshu(CType(csDataNewRow(ABAtena1Entity.DAINOKB), String))
                    ''�O���̏ꍇ�A
                    'If csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows.Count = 0 Then
                    '    csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty                   '��[�敪����
                    '    csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty              '��[�敪��������
                    'Else
                    '    With csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Rows(0)

                    '        '��[�敪����
                    '        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = CType(.Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO), String)

                    '        '��[�敪��������
                    '        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = CType(.Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI), String)
                    '    End With

                    'End If
                    csDainoKankeiCDMSTEntity = m_cABDainoKankeiB.GetDainoKBHoshu2(CType(csDataNewRow(ABAtena1Entity.DAINOKB), String))
                    If csDainoKankeiCDMSTEntity.Length = 0 Then
                        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty                   '��[�敪����
                        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty              '��[�敪��������
                    Else

                        '��[�敪����
                        csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = CType(csDainoKankeiCDMSTEntity(0).Item(ABDainoKankeiCDMSTEntity.DAINOKBMEISHO), String)

                        '��[�敪��������
                        csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = CType(csDainoKankeiCDMSTEntity(0).Item(ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI), String)

                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                End If

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                    '��[�敪�w��Ȃ��̏ꍇ
                    If strGyomuCD = String.Empty Then

                        '�Ɩ��R�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUCD) = "00"

                        '�Ɩ�����ʃR�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = String.Empty
                    Else
                        '�Ɩ��R�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUCD) = strGyomuCD

                        '�Ɩ�����ʃR�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = strGyomunaiSHU_CD
                    End If

                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '���s�����R�[�h
                csDataNewRow(ABAtena1Entity.KYUSHICHOSONCD) = csDataRow(ABAtenaRirekiEntity.KYUSHICHOSONCD)

                '���уR�[�h
                csDataNewRow(ABAtena1Entity.STAICD) = csDataRow(ABAtenaRirekiEntity.STAICD)

                '�����f�[�^�敪
                csDataNewRow(ABAtena1Entity.ATENADATAKB) = csDataRow(ABAtenaRirekiEntity.ATENADATAKB)

                '�����f�[�^���
                csDataNewRow(ABAtena1Entity.ATENADATASHU) = csDataRow(ABAtenaRirekiEntity.ATENADATASHU)

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                    '�ҏW���
                    Call m_cABJuminShubetsuB.GetJuminshubetsu(CType(csDataRow(ABAtenaRirekiEntity.ATENADATAKB), String),
                                                            CType(csDataRow(ABAtenaRirekiEntity.ATENADATASHU), String))
                    csDataNewRow(ABAtena1Entity.HENSHUSHUBETSU) = m_cABJuminShubetsuB.p_strHenshuShubetsu

                    '�ҏW��ʗ���
                    csDataNewRow(ABAtena1Entity.HENSHUSHUBETSURYAKU) = m_cABJuminShubetsuB.p_strHenshuShubetsuRyaku

                    '�����p�J�i����
                    csDataNewRow(ABAtena1Entity.SEARCHKANASEIMEI) = csDataRow(ABAtenaRirekiEntity.SEARCHKANASEIMEI)

                    '�����p�J�i��
                    csDataNewRow(ABAtena1Entity.SEARCHKANASEI) = csDataRow(ABAtenaRirekiEntity.SEARCHKANASEI)
                    '�����p�J�i��

                    csDataNewRow(ABAtena1Entity.SEARCHKANAMEI) = csDataRow(ABAtenaRirekiEntity.SEARCHKANAMEI)

                    '�����p��������
                    csDataNewRow(ABAtena1Entity.SEARCHKANJIMEI) = csDataRow(ABAtenaRirekiEntity.SEARCHKANJIMEISHO)
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '*����ԍ� 000042 2011/05/18 �ǉ��J�n
                ' �{���ʏ̖��֑ؑΉ� - �J�i���́A�������̎擾
                Select Case CStr(csDataRow(ABAtenaEntity.ATENADATAKB))
                    Case "11", "12"         ' �Z�o���A�Z�o�O

                        If (m_strHonmyoTsushomeiYusenKB.Trim = "1") Then
                            ' �Ǘ����F�{���ʏ̖��D�搧�� = "1" �̏ꍇ
                            strMeisho = MeishoHenshu(csDataRow)
                        Else
                            strMeisho(0) = CStr(csDataRow(ABAtenaEntity.KANAMEISHO1))       ' �J�i���̂P
                            strMeisho(1) = CStr(csDataRow(ABAtenaEntity.KANJIMEISHO1))      ' 
                        End If
                    Case "20"               ' �@�l

                    Case "30"               ' ���L
                        strMeisho(0) = CStr(csDataRow(ABAtenaEntity.KANAMEISHO1))
                        strMeisho(1) = CStr(csDataRow(ABAtenaEntity.KANJIMEISHO1))
                    Case Else
                End Select
                '*����ԍ� 000042 2011/05/18 �ǉ��I��

                '�ҏW�J�i����
                '�����敪="20"(�@�l)�̏ꍇ
                If CType(csDataRow(ABAtenaRirekiEntity.ATENADATAKB), String) = "20" Then
                    '* ����ԍ� 000033 2007/07/17 �C���J�n
                    '�J�i���̂Q�i�x�X���j�������ꍇ�̓J�i���̂P�i�@�l���j�ƃJ�i���̂Q�i�x�X���j�̌����͍s��Ȃ�
                    If CType(csDataRow(ABAtenaRirekiEntity.KANAMEISHO2), String).Trim <> String.Empty Then
                        strHenshuKanaMeisho = CType(csDataRow(ABAtenaRirekiEntity.KANAMEISHO1), String).TrimEnd +
                                " " + CType(csDataRow(ABAtenaRirekiEntity.KANAMEISHO2), String).TrimEnd
                    Else
                        strHenshuKanaMeisho = CType(csDataRow(ABAtenaRirekiEntity.KANAMEISHO1), String).TrimEnd
                    End If
                    'strHenshuKanaMeisho = CType(csDataRow(ABAtenaRirekiEntity.KANAMEISHO1), String).TrimEnd _
                    '        + " " + CType(csDataRow(ABAtenaRirekiEntity.KANAMEISHO2), String).TrimEnd
                    '* ����ԍ� 000033 2007/07/17 �C���I��
                    '* ����ԍ� 000032 2007/07/09 �C���J�n
                    If (strHenshuKanaMeisho.RLength > 240) Then
                        csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho.RSubstring(0, 240)
                        'If (strHenshuKanaMeisho.Length > 60) Then
                        '    csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho.Substring(0, 60)
                        '* ����ԍ� 000032 2007/07/09 �C���I��
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = strHenshuKanaMeisho
                    End If
                Else
                    '*����ԍ� 000042 2011/05/18 �C���J�n
                    strHenshuKanaMeisho = strMeisho(0)
                    csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = ABStrXClass.Left(strHenshuKanaMeisho, ABAtenaGetConstClass.KETA_HENSHUKANAMEISHO)
                    'csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = csDataRow(ABAtenaRirekiEntity.KANAMEISHO1)
                    '*����ԍ� 000042 2011/05/18 �C���I��
                End If
                '�ҏW�J�i���́i�t���j
                If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    csDataNewRow(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL) = strHenshuKanaMeisho
                Else
                End If

                '�ҏW��������
                '�����敪="20"(�@�l)�̏ꍇ
                If CType(csDataRow(ABAtenaRirekiEntity.ATENADATAKB), String) = "20" Then
                    m_cABHojinMeishoB.p_strKeitaiFuyoKB = CType(csDataRow(ABAtenaRirekiEntity.HANYOKB1), String)
                    m_cABHojinMeishoB.p_strKeitaiSeiRyakuKB = CType(csDataRow(ABAtenaRirekiEntity.HANYOKB2), String)
                    m_cABHojinMeishoB.p_strKanjiHjnKeitai = CType(csDataRow(ABAtenaRirekiEntity.KANJIHJNKEITAI), String)
                    m_cABHojinMeishoB.p_strKanjiMeisho1 = CType(csDataRow(ABAtenaRirekiEntity.KANJIMEISHO1), String)
                    m_cABHojinMeishoB.p_strKanjiMeisho2 = CType(csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2), String)
                    strHenshuKanjiShimei = m_cABHojinMeishoB.GetHojinMeisho()
                    '* ����ԍ� 000032 2007/076/09 �C���J�n
                    If (strHenshuKanjiShimei.RLength > 240) Then
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei.RSubstring(0, 240)
                        'If (strHenshuKanjiShimei.Length > 80) Then
                        '    csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei.Substring(0, 80)
                        '* ����ԍ� 000032 2007/07/09 �C���I��
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = strHenshuKanjiShimei
                    End If
                Else
                    '* �����J�n 000035 2008/02/15 �C���J�n
                    'csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO1)
                    If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                        '*����ԍ� 000042 2011/05/18 �C���J�n
                        ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                        strHenshuKanjiShimei = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)),
                                                                                   CStr(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)),
                                                                                   strMeisho(1))
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strHenshuKanjiShimei, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO)
                        'csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)), _
                        '                                                                                     CStr(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)), _
                        '                                                                                     CStr(csDataRow(ABAtenaRirekiEntity.KANJIMEISHO1)))
                        '*����ԍ� 000042 2011/05/18 �C���I��
                    Else
                        '*����ԍ� 000042 2011/05/18 �C���J�n
                        ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                        strHenshuKanjiShimei = strMeisho(1)
                        csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strHenshuKanjiShimei, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO)
                        'csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO1)
                        '*����ԍ� 000042 2011/05/18 �C���I��
                    End If
                    '* �����J�n 000035 2008/02/15 �C���I��
                End If
                '�ҏW�������́i�t���j
                If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    csDataNewRow(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL) = strHenshuKanjiShimei
                Else
                End If

                If (csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTONAI_KOJIN) OrElse
                   (csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTOGAI_KOJIN) Then
                    If (csDataRow(ABAtenaRirekiEntity.UMAREYMD).ToString.Trim = String.Empty) Then
                        csDataNewRow(ABAtena1Entity.UMAREYMD) = m_strUmareYMDHenkanParam
                        csDataNewRow(ABAtena1Entity.UMAREWMD) = m_strUmareWmdHenkan
                    ElseIf (CheckDate(csDataRow(ABAtenaRirekiEntity.UMAREYMD).ToString)) Then
                        csDataNewRow(ABAtena1Entity.UMAREYMD) = csDataRow(ABAtenaRirekiEntity.UMAREYMD)
                        csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaRirekiEntity.UMAREWMD)
                    Else
                        csDataNewRow(ABAtena1Entity.UMAREYMD) = GetSeirekiLastDay(csDataRow(ABAtenaRirekiEntity.UMAREYMD).ToString)
                        csDataNewRow(ABAtena1Entity.UMAREWMD) = GetWarekiLastDay(csDataRow(ABAtenaRirekiEntity.UMAREWMD).ToString,
                                                                csDataRow(ABAtenaRirekiEntity.UMAREYMD).ToString)
                    End If
                Else
                    '���N����
                    csDataNewRow(ABAtena1Entity.UMAREYMD) = csDataRow(ABAtenaRirekiEntity.UMAREYMD)

                '���a��N����
                csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaRirekiEntity.UMAREWMD)
                End If
                '���N�����ҏW
                'csDataNewRow(ABAtena1Entity.UMAREWMD) = csDataRow(ABAtenaRirekiEntity.UMAREWMD)

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                    m_cABUmareHenshuB.p_strDataKB = CType(csDataRow(ABAtenaRirekiEntity.ATENADATAKB), String)
                    m_cABUmareHenshuB.p_strJuminSHU = CType(csDataRow(ABAtenaRirekiEntity.ATENADATASHU), String)
                    m_cABUmareHenshuB.p_strUmareYMD = CType(csDataNewRow(ABAtena1Entity.UMAREYMD), String)
                    m_cABUmareHenshuB.p_strUmareWMD = CType(csDataNewRow(ABAtena1Entity.UMAREWMD), String)
                    m_cABUmareHenshuB.HenshuUmare()
                    '���\���N����
                    csDataNewRow(ABAtena1Entity.UMAREHYOJIWMD) = m_cABUmareHenshuB.p_strHyojiUmareYMD

                    '���ؖ��N����
                    csDataNewRow(ABAtena1Entity.UMARESHOMEIWMD) = m_cABUmareHenshuB.p_strShomeiUmareYMD

                    '���ʃR�[�h
                    csDataNewRow(ABAtena1Entity.SEIBETSUCD) = csDataRow(ABAtenaRirekiEntity.SEIBETSUCD)

                    '����
                    strWork = CType(csDataRow(ABAtenaRirekiEntity.SEIBETSU), String).Trim
                    csDataNewRow(ABAtena1Entity.SEIBETSU) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_SEIBETSU)
                    '���ʁi�t���j
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataNewRow(ABAtena1HyojunEntity.SEIBETSU_FULL) = csDataRow(ABAtenaRirekiEntity.SEIBETSU)
                    Else
                    End If

                    '�ҏW�����R�[�h
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(ABAtenaRirekiEntity.DAI2ZOKUGARACD, String) = String.Empty Then
                    '*����ԍ� 000018 2003/10/14 �C���J�n
                    'If CType(ABAtenaRirekiEntity.DAI2ZOKUGARACD, String).Trim = String.Empty Then
                    If CType(csDataRow(ABAtenaEntity.DAI2ZOKUGARACD), String).Trim = String.Empty Then
                        '*����ԍ� 000018 2003/10/14 �C���I��
                        '*����ԍ� 000002 2003/02/20 �C���I��
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = csDataRow(ABAtenaRirekiEntity.ZOKUGARACD)
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = csDataRow(ABAtenaRirekiEntity.DAI2ZOKUGARACD)
                    End If

                    '�ҏW����
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(ABAtenaRirekiEntity.DAI2ZOKUGARA, String) = String.Empty Then
                    '*����ԍ� 000018 2003/10/14 �C���J�n
                    'If CType(ABAtenaRirekiEntity.DAI2ZOKUGARA, String).Trim = String.Empty Then
                    If CType(csDataRow(ABAtenaEntity.DAI2ZOKUGARA), String).Trim = String.Empty Then
                        '*����ԍ� 000018 2003/10/14 �C���I��
                        '*����ԍ� 000002 2003/02/20 �C���I��
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = csDataRow(ABAtenaRirekiEntity.ZOKUGARA)
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = csDataRow(ABAtenaRirekiEntity.DAI2ZOKUGARA)
                    End If

                    '* �����J�n 000035 2008/02/15 �C���J�n
                    '�@�l��\�Җ�
                    'csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = csDataRow(ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI)
                    If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                        ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                        csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)),
                                                                                                           CStr(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)),
                                                                                                           CStr(csDataRow(ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI)))
                    Else
                        ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                        csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = csDataRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)
                    End If
                    '* �����J�n 000035 2008/02/15 �C���I��
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
                '�l�@�l�敪
                csDataNewRow(ABAtena1Entity.KJNHJNKB) = csDataRow(ABAtenaRirekiEntity.KJNHJNKB)

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                    '�l�@�l�敪����
                    csDataNewRow(ABAtena1Entity.KJNHJNKBMEISHO) = m_cABKjnhjnKBB.GetKjnhjn(CType(csDataRow(ABAtenaRirekiEntity.KJNHJNKB), String))

                    '�Ǔ��ǊO�敪����
                    csDataNewRow(ABAtena1Entity.NAIGAIKBMEISHO) = m_cABKannaiKangaiKBB.GetKannaiKangai(CType(csDataRow(ABAtenaRirekiEntity.KANNAIKANGAIKB), String))
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '�Ǔ��ǊO�敪
                csDataNewRow(ABAtena1Entity.KANNAIKANGAIKB) = csDataRow(ABAtenaRirekiEntity.KANNAIKANGAIKB)

                '�Z��D��̏ꍇ
                If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then

                    '�X�֔ԍ�
                    csDataNewRow(ABAtena1Entity.YUBINNO) = csDataRow(ABAtenaRirekiEntity.JUKIYUBINNO)

                    '�Z���R�[�h
                    csDataNewRow(ABAtena1Entity.JUSHOCD) = csDataRow(ABAtenaRirekiEntity.JUKIJUSHOCD)

                    '�Z��
                    csDataNewRow(ABAtena1Entity.JUSHO) = csDataRow(ABAtenaRirekiEntity.JUKIJUSHO)

                    '�ҏW�Z����
                    If cAtenaGetPara1.p_strJushoHenshu1 = String.Empty Then
                        csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = String.Empty
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = String.Empty
                        Else
                        End If

                    ElseIf cAtenaGetPara1.p_strJushoHenshu1 = "1" Then
                        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                        'strHenshuJusho = String.Empty
                        m_strHenshuJusho.RRemove(0, m_strHenshuJusho.RLength)
                        '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                        If cAtenaGetPara1.p_strJushoHenshu2 = "1" Then

                            '�s�������𓪂ɕt������i�Ǔ��̂݁j
                            If CType(csDataRow(ABAtenaRirekiEntity.KANNAIKANGAIKB), String) = "1" Then
                                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                'strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0)
                                m_strHenshuJusho.Append(m_cuUSSCityInfo.p_strShichosonmei(0))
                                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                            End If
                        End If
                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'Select Case cAtenaGetPara1.p_strJushoHenshu3
                        Select Case strJushoHenshu3
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '* ����ԍ� 000028 2007/01/15 �C���J�n
                            Case "1", "6"   '�Z���{�Ԓn
                                'Case "1"    '�Z���{�Ԓn
                                '* ����ԍ� 000028 2007/01/15 �C���I��
                                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd)
                                m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                            Case "2"    '�s����{�Ԓn
                                '*����ԍ� 000009 2003/03/17 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd

                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                Else
                                    '�s����{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000009 2003/03/17 �C���I��
                            Case "3"    '�Z���{�i�s����j�{�Ԓn
                                '*����ԍ� 000004  2003/02/25 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd

                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                Else
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + "�i" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '                + "�j" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append("�i")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�j")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000004 2003/02/25 �C���I��
                            Case "4"    '�s����{�i�Z���j�{�Ԓn
                                '*����ԍ� 000004 2003/02/25 �C���J�n 
                                'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd

                                '�Z�������݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                    '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                                    '�s���於�����݂��Ȃ��ꍇ
                                ElseIf (CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                    '*����ԍ� 000009 2003/03/17 �ǉ��I��
                                Else
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '                + "�i" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + "�j" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�i")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append("�j")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j

                                End If
                                '*����ԍ� 000004 2003/02/25 �C���I��
                                '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                            Case "5"    '�s����{���{�Ԓn
                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIJUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                Else
                                    '�s����{���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd _
                                    '                + "�@" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�@")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIBANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000009 2003/03/17 �ǉ��I��
                        End Select

                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                        '* ����ԍ� 000028 2007/01/15 �C���J�n
                        If (strJushoHenshu4 = "1") _
                            AndAlso (CType(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI), String).Trim <> String.Empty) Then
                            'If strJushoHenshu4 = "1" Then
                            '* ����ԍ� 000028 2007/01/15 �C���I��
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '*����ԍ� 000004 2003/02/25 �C���J�n
                            'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI), String).TrimEnd

                            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                            'strHenshuJusho += "�@" + CType(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI), String).TrimEnd
                            m_strHenshuJusho.Append("�@")
                            m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI), String).TrimEnd)
                            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                            '*����ԍ� 000004 2003/02/25 �C���I��
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��J�n
                        ' �Z���ҏW�R�p�����[�^���U�A���s���於������Ƃ��́A�ҏW�Z���Ɂi�s����j��ǉ�����
                        If (strJushoHenshu3 = "6") _
                                AndAlso (CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).Trim <> String.Empty) Then
                            m_strHenshuJusho.Append("�i")
                            m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI), String).TrimEnd)
                            m_strHenshuJusho.Append("�j")
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��I��
                        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                        'If strHenshuJusho.Length >= 80 Then
                        '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                        'Else
                        '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho
                        'End If
                        '* ����ԍ� 000032 2007/07/09 �C���J�n
                        If m_strHenshuJusho.RLength >= 160 Then
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().RSubstring(0, 160)
                            'If m_strHenshuJusho.Length >= 80 Then
                            '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().Substring(0, 80)
                            '* ����ԍ� 000032 2007/07/09 �C���I��
                        Else
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString()
                        End If
                        '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = m_strHenshuJusho.ToString()
                        Else
                        End If
                    End If

                    '�Ԓn�R�[�h�P
                    csDataNewRow(ABAtena1Entity.BANCHICD1) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHICD1)

                    '�Ԓn�R�[�h�Q
                    csDataNewRow(ABAtena1Entity.BANCHICD2) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHICD2)

                    '�Ԓn�R�[�h�R
                    csDataNewRow(ABAtena1Entity.BANCHICD3) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHICD3)
                    '�Ԓn
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" Then
                        '�Z���ҏW����̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.BANCHI) = ""
                    Else
                        csDataNewRow(ABAtena1Entity.BANCHI) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)
                    End If

                    '�����t���O
                    csDataNewRow(ABAtena1Entity.KATAGAKIFG) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKIFG)

                    '�����R�[�h
                    csDataNewRow(ABAtena1Entity.KATAGAKICD) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKICD)

                    '����
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                        '�����t������̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = String.Empty
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = String.Empty
                        Else
                        End If
                    Else
                        strWork = CType(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI)
                        Else
                        End If
                    End If

                    '*����ԍ� 000017 2003/10/09 �C���J�n
                    ''�A����P
                    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                    ''�A����Q
                    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)

                    '*����ԍ� 000021 2003/12/02 �C���J�n
                    '' �A����}�X�^�����݂���ꍇ�́A�A����}�X�^�̘A�����ݒ肷��
                    'If (csRenrakusakiRow Is Nothing) Then
                    '    '�A����P
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                    '    '�A����Q
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)
                    '    '*����ԍ� 000020 2003/12/01 �ǉ��J�n
                    '    '�A����擾�Ɩ��R�[�h
                    '    strRenrakusakiGyomuCD = String.Empty
                    '    '*����ԍ� 000020 2003/12/01 �ǉ��I��
                    'Else
                    '    '�A����P
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                    '    '�A����Q
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                    '    '*����ԍ� 000020 2003/12/01 �C���J�n
                    '    ''�A����擾�Ɩ��R�[�h
                    '    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD

                    '    '�A����擾�Ɩ��R�[�h
                    '    strRenrakusakiGyomuCD = strGyomuCD
                    '    '*����ԍ� 000020 2003/12/01 �C���I��
                    'End If
                    ''*����ԍ� 000017 2003/10/09 �C���I��

                    '�A����P
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                    '�A����Q
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)
                    '*����ԍ� 000021 2003/12/02 �C���I��

                    '�s����R�[�h
                    csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUCD)

                    '�s���於
                    csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csDataRow(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)

                    '�n��R�[�h�P
                    csDataNewRow(ABAtena1Entity.CHIKUCD1) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUCD1)

                    '�n��P
                    csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUMEI1)

                    '�n��R�[�h�Q
                    csDataNewRow(ABAtena1Entity.CHIKUCD2) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUCD2)

                    '�n��Q
                    csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUMEI2)

                    '�n��R�[�h�R
                    csDataNewRow(ABAtena1Entity.CHIKUCD3) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUCD3)

                    '�n��R
                    csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csDataRow(ABAtenaRirekiEntity.JUKICHIKUMEI3)

                    '�\�����i��Q�Z���[�\����������ꍇ�́A��Q�Z���[�\�����j
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(csDataRow(ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN), String) = String.Empty Then
                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                        If CType(csDataRow(ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN), String).Trim = "00" Then
                            '*����ԍ� 000002 2003/02/20 �C���I��
                            csDataNewRow(ABAtena1Entity.HYOJIJUN) = csDataRow(ABAtenaRirekiEntity.JUMINHYOHYOJIJUN)
                        Else
                            csDataNewRow(ABAtena1Entity.HYOJIJUN) = csDataRow(ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN)
                        End If
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
                Else
                    '�X�֔ԍ�
                    csDataNewRow(ABAtena1Entity.YUBINNO) = csDataRow(ABAtenaRirekiEntity.YUBINNO)
                    '�Z���R�[�h
                    csDataNewRow(ABAtena1Entity.JUSHOCD) = csDataRow(ABAtenaRirekiEntity.JUSHOCD)
                    '�Z��
                    csDataNewRow(ABAtena1Entity.JUSHO) = csDataRow(ABAtenaRirekiEntity.JUSHO)

                    '�ҏW�Z����
                    If cAtenaGetPara1.p_strJushoHenshu1 = String.Empty Then
                        csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = String.Empty
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = String.Empty
                        Else
                        End If

                    ElseIf cAtenaGetPara1.p_strJushoHenshu1 = "1" Then
                        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                        'strHenshuJusho = String.Empty
                        m_strHenshuJusho.RRemove(0, m_strHenshuJusho.RLength)
                        '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                        If cAtenaGetPara1.p_strJushoHenshu2 = "1" Then

                            '�Ǔ��̂ݎs��������t������
                            If CType(csDataRow(ABAtenaRirekiEntity.KANNAIKANGAIKB), String) = "1" Then
                                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                'strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0)
                                m_strHenshuJusho.Append(m_cuUSSCityInfo.p_strShichosonmei(0))
                                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                            End If
                        End If
                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'Select Case cAtenaGetPara1.p_strJushoHenshu3
                        Select Case strJushoHenshu3
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '* ����ԍ� 000028 2007/01/15 �C���J�n
                            Case "1", "6"   '�Z���{�Ԓn
                                'Case "1"    '�Z���{�Ԓn
                                '* ����ԍ� 000028 2007/01/15 �C���I��
                                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd)
                                m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                            Case "2"    '�s����{�Ԓn
                                '*����ԍ� 000009 2003/03/17 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                Else
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000009 2003/03/17 �C���I��
                            Case "3"    '�Z���{�i�s����j�{�Ԓn
                                '*����ԍ� 000004  2003/02/25 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd

                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                Else
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                    '                + "�i" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + "�j" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append("�i")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�j")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000004  2003/02/25 �C���I��
                            Case "4"    '�s����{�i�Z���j�{�Ԓn
                                '*����ԍ� 000004  2003/02/25 �C���J�n
                                'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd

                                '�Z�������݂��Ȃ��ꍇ�A�s����{�Ԓn
                                If (CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd = String.Empty) Then
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                    '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                                ElseIf (CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�s���於�����݂��Ȃ��ꍇ�A�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                    '                 + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                    '*����ԍ� 000009 2003/03/17 �ǉ��I��
                                Else
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + "�i" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                    '                + "�j" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd)
                                    m_strHenshuJusho.Append("�i")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append("�j")
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000004 2003/02/25 �C���I��
                                '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                            Case "5"    '�s����{���{�Ԓn
                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�Z���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.JUSHO), String).TrimEnd)
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                Else
                                    '�s����{���{�Ԓn
                                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                                    'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd _
                                    '                + "�@" _
                                    '                + CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).TrimEnd)
                                    '* ����ԍ� 000028 2007/01/15 �ǉ��J�n
                                    m_strHenshuJusho.Append("�@")
                                    '* ����ԍ� 000028 2007/01/15 �ǉ��I��
                                    m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.BANCHI), String).TrimEnd)
                                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                                End If
                                '*����ԍ� 000009 2003/03/17 �ǉ��I��
                        End Select
                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                        '* ����ԍ� 000028 2007/01/15 �C���J�n
                        If (strJushoHenshu4 = "1") _
                            AndAlso (CType(csDataRow(ABAtenaRirekiEntity.KATAGAKI), String).Trim <> String.Empty) Then
                            'If strJushoHenshu4 = "1" Then
                            '* ����ԍ� 000028 2007/01/15 �C���I��
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '*����ԍ� 000004  2003/02/25 �C���J�n
                            'strHenshuJusho += CType(csDataRow(ABAtenaRirekiEntity.KATAGAKI), String).TrimEnd

                            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                            'strHenshuJusho += "�@" + CType(csDataRow(ABAtenaRirekiEntity.KATAGAKI), String).TrimEnd
                            m_strHenshuJusho.Append("�@")
                            m_strHenshuJusho.Append(CType(csDataRow(ABAtenaRirekiEntity.KATAGAKI), String).TrimEnd)
                            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                            '*����ԍ� 000004  2003/02/25 �C���I��
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��J�n
                        ' �Z���ҏW�R�p�����[�^���U�A���s���於������Ƃ��́A�ҏW�Z���Ɂi�s����j��ǉ�����
                        If (strJushoHenshu3 = "6") _
                                AndAlso (CType(csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI), String).Trim <> String.Empty) Then
                            m_strHenshuJusho.Append("�i")
                            m_strHenshuJusho.Append(CType(csDataRow(ABAtenaEntity.GYOSEIKUMEI), String).TrimEnd)
                            m_strHenshuJusho.Append("�j")
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��I��
                        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                        'If strHenshuJusho.Length >= 80 Then
                        '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                        'Else
                        '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho
                        'End If
                        '* ����ԍ� 000032 2007/07/09 �C���J�n
                        If m_strHenshuJusho.RLength >= 160 Then
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().RSubstring(0, 160)
                            'If m_strHenshuJusho.Length >= 80 Then
                            '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString().Substring(0, 80)
                            '* ����ԍ� 000032 2007/07/09 �C���I��
                        Else
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = m_strHenshuJusho.ToString()
                        End If
                        '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = m_strHenshuJusho.ToString()
                        Else
                        End If
                    End If

                    '�Ԓn�R�[�h�P
                    csDataNewRow(ABAtena1Entity.BANCHICD1) = csDataRow(ABAtenaRirekiEntity.BANCHICD1)

                    '�Ԓn�R�[�h�Q
                    csDataNewRow(ABAtena1Entity.BANCHICD2) = csDataRow(ABAtenaRirekiEntity.BANCHICD2)

                    '�Ԓn�R�[�h�R
                    csDataNewRow(ABAtena1Entity.BANCHICD3) = csDataRow(ABAtenaRirekiEntity.BANCHICD3)

                    '�Ԓn
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" Then
                        '�Z���ҏW����̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.BANCHI) = ""
                    Else
                        csDataNewRow(ABAtena1Entity.BANCHI) = csDataRow(ABAtenaRirekiEntity.BANCHI)
                    End If

                    '�����t���O
                    csDataNewRow(ABAtena1Entity.KATAGAKIFG) = csDataRow(ABAtenaRirekiEntity.KATAGAKIFG)

                    '�����R�[�h
                    csDataNewRow(ABAtena1Entity.KATAGAKICD) = csDataRow(ABAtenaRirekiEntity.KATAGAKICD)

                    '����
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                        '�����t������̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = ""
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = String.Empty
                        Else
                        End If
                    Else
                        strWork = CType(csDataRow(ABAtenaRirekiEntity.KATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.KATAGAKI)
                        Else
                        End If
                    End If

                    '*����ԍ� 000017 2003/10/09 �C���J�n
                    ''�A����P
                    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                    ''�A����Q
                    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)

                    '*����ԍ� 000021 2003/12/02 �C���J�n
                    '' �A����}�X�^�����݂���ꍇ�́A�A����}�X�^�̘A�����ݒ肷��
                    'If (csRenrakusakiRow Is Nothing) Then
                    '    '�A����P
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                    '    '�A����Q
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)
                    '    '*����ԍ� 000020 2003/12/01 �ǉ��J�n
                    '    '�A����擾�Ɩ��R�[�h
                    '    strRenrakusakiGyomuCD = String.Empty
                    '    '*����ԍ� 000020 2003/12/01 �ǉ��I��
                    'Else
                    '    '�A����P
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                    '    '�A����Q
                    '    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                    '    '*����ԍ� 000020 2003/12/01 �C���J�n
                    '    ''�A����擾�Ɩ��R�[�h
                    '    'csDataNewRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD

                    '    '�A����擾�Ɩ��R�[�h
                    '    strRenrakusakiGyomuCD = strGyomuCD
                    '    '*����ԍ� 000020 2003/12/01 �C���I��
                    'End If
                    ''*����ԍ� 000017 2003/10/09 �C���I��

                    '�A����P
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI1)
                    '�A����Q
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csDataRow(ABAtenaRirekiEntity.RENRAKUSAKI2)
                    '*����ԍ� 000021 2003/12/02 �C���I��

                    '�s����R�[�h
                    csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csDataRow(ABAtenaRirekiEntity.GYOSEIKUCD)

                    '�s���於
                    csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csDataRow(ABAtenaRirekiEntity.GYOSEIKUMEI)

                    '�n��R�[�h�P
                    csDataNewRow(ABAtena1Entity.CHIKUCD1) = csDataRow(ABAtenaRirekiEntity.CHIKUCD1)

                    '�n��P
                    csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csDataRow(ABAtenaRirekiEntity.CHIKUMEI1)

                    '�n��R�[�h�Q
                    csDataNewRow(ABAtena1Entity.CHIKUCD2) = csDataRow(ABAtenaRirekiEntity.CHIKUCD2)

                    '�n��Q
                    csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csDataRow(ABAtenaRirekiEntity.CHIKUMEI2)

                    '�n��R�[�h�R
                    csDataNewRow(ABAtena1Entity.CHIKUCD3) = csDataRow(ABAtenaRirekiEntity.CHIKUCD3)

                    '�n��R
                    csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csDataRow(ABAtenaRirekiEntity.CHIKUMEI3)

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                        '* ����ԍ� 000026 2005/12/21 �C���J�n
                        ''�\����
                        'csDataNewRow(ABAtena1Entity.HYOJIJUN) = String.Empty

                        '�\�����i��Q�Z���[�\����������ꍇ�́A��Q�Z���[�\�����j
                        If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                            strWork = CType(csDataRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN), String).Trim
                            If (strWork = "00") Then
                                strWork = csDataRow(ABAtenaEntity.JUMINHYOHYOJIJUN).ToString().Trim
                            End If
                            If (strWork = String.Empty) Then
                                strWork = "99"
                            End If
                            csDataNewRow(ABAtena1Entity.HYOJIJUN) = strWork
                        End If
                        '* ����ԍ� 000026 2005/12/21 �C���I��
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                    '�o�^�ٓ��N����
                    csDataNewRow(ABAtena1Entity.TOROKUIDOYMD) = csDataRow(ABAtenaRirekiEntity.TOROKUIDOYMD)

                    '�o�^���R�R�[�h
                    csDataNewRow(ABAtena1Entity.TOROKUJIYUCD) = csDataRow(ABAtenaRirekiEntity.TOROKUJIYUCD)

                    '�o�^���R
                    csDataNewRow(ABAtena1Entity.TOROKUJIYU) = csDataRow(ABAtenaRirekiEntity.TOROKUJIYU)

                    If ((csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTONAI_KOJIN) OrElse
                        (csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTOGAI_KOJIN)) AndAlso
                       (Not csDataRow(ABAtenaRirekiEntity.SHOJOJIYUCD).ToString.Trim = String.Empty) Then
                        If (csDataRow(ABAtenaRirekiEntity.SHOJOIDOYMD).ToString.Trim = String.Empty) Then
                            csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = m_strShojoIdobiHenkanParam
                        Else
                            csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csDataRow(ABAtenaRirekiEntity.SHOJOIDOYMD)
                        End If
                    Else
                        '�����ٓ��N����
                        csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csDataRow(ABAtenaRirekiEntity.SHOJOIDOYMD)
                    End If
                    '�������R�R�[�h
                    csDataNewRow(ABAtena1Entity.SHOJOJIYUCD) = csDataRow(ABAtenaRirekiEntity.SHOJOJIYUCD)

                    '�������R����
                    csDataNewRow(ABAtena1Entity.SHOJOJIYU) = csDataRow(ABAtenaRirekiEntity.SHOJOJIYU)

                    '�ҏW���ю�Z���R�[�h
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSJUMINCD), String) = String.Empty Then
                    If CType(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSJUMINCD), String).Trim = String.Empty Then
                        '*����ԍ� 000002 2003/02/20 �C���I��
                        csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csDataRow(ABAtenaRirekiEntity.STAINUSJUMINCD)
                    Else
                        csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csDataRow(ABAtenaRirekiEntity.DAI2STAINUSJUMINCD)
                    End If
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
                '�ҏW�J�i���ю喼
                '*����ԍ� 000002 2003/02/20 �C���J�n
                'If CType(csDataRow(ABAtenaRirekiEntity.KANADAI2STAINUSMEI), String) = String.Empty Then
                If CType(csDataRow(ABAtenaRirekiEntity.KANADAI2STAINUSMEI), String).Trim = String.Empty Then
                    '*����ԍ� 000002 2003/02/20 �C���I��
                    csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csDataRow(ABAtenaRirekiEntity.KANASTAINUSMEI)
                Else
                    csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csDataRow(ABAtenaRirekiEntity.KANADAI2STAINUSMEI)
                End If

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                    '�ҏW�������ю喼
                    '*����ԍ� 000002 2003/02/20 �C���J�n
                    'If CType(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI), String) = String.Empty Then
                    If CType(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI), String).Trim = String.Empty Then
                        '*����ԍ� 000002 2003/02/20 �C���I��
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        'csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaRirekiEntity.STAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.STAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaRirekiEntity.STAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                    Else
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        'csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csDataRow(ABAtenaRirekiEntity.DAI2STAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                    End If

                    '*����ԍ� 000012 2003/04/18 �ǉ��J�n
                    ' �����R�[�h
                    csDataNewRow(ABAtena1Entity.ZOKUGARACD) = csDataRow(ABAtenaRirekiEntity.ZOKUGARACD)
                    ' ����
                    csDataNewRow(ABAtena1Entity.ZOKUGARA) = csDataRow(ABAtenaRirekiEntity.ZOKUGARA)

                    '*����ԍ� 000014 2003/04/30 �C���J�n
                    '' �J�i���̂Q
                    'csDataNewRow(ABAtena1Entity.KANAMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANAMEISHO2)
                    '' �������̂Q
                    'csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2)

                    '�����敪��"20"(�@�l)�̏ꍇ
                    If Not (CType(csDataRow(ABAtenaEntity.ATENADATAKB), String) = "20") Then
                        ' �J�i���̂Q
                        csDataNewRow(ABAtena1Entity.KANAMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANAMEISHO2)
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        ' �������̂Q
                        'csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.ATENADATAKB)),
                                                                                                            CStr(csDataRow(ABAtenaRirekiEntity.ATENADATASHU)),
                                                                                                            CStr(csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = csDataRow(ABAtenaRirekiEntity.KANJIMEISHO2)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                    End If
                    '*����ԍ� 000014 2003/04/30 �C���I��

                    ' �Дԍ�
                    csDataNewRow(ABAtena1Entity.SEKINO) = csDataRow(ABAtenaRirekiEntity.SEKINO)
                    '*����ԍ� 000012 2003/04/18 �ǉ��I��
                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                '*����ԍ� 000040 2010/05/14 �ǉ��J�n
                ' �{�ЕM���ҏ��o�͔���
                If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                    ' �p�����[�^:�{�ЕM���Ҏ擾�敪��"1"���A�Ǘ����:�{�Ў擾�敪(10�18)��"1"�̏ꍇ�̂݃Z�b�g
                    ' �{�ЏZ��
                    csDataNewRow(ABAtena1Entity.HON_JUSHO) = csDataRow(ABAtenaRirekiEntity.HON_JUSHO)
                    ' �{�ДԒn
                    csDataNewRow(ABAtena1Entity.HONSEKIBANCHI) = csDataRow(ABAtenaRirekiEntity.HONSEKIBANCHI)
                    ' �M����
                    csDataNewRow(ABAtena1Entity.HITTOSH) = csDataRow(ABAtenaRirekiEntity.HITTOSH)
                Else
                End If

                ' ������~�敪�o�͔���
                If (m_strShoriteishiKB = "1" AndAlso m_strShoriteishiKB_Param = "1") Then
                    ' �p�����[�^:������~�敪�擾�敪��"1"���A�Ǘ����:������~�敪�擾�敪(10�19)��"1"�̏ꍇ�̂݃Z�b�g
                    ' ������~�敪
                    csDataNewRow(ABAtena1Entity.SHORITEISHIKB) = csDataRow(ABAtenaRirekiEntity.SHORITEISHIKB)
                Else
                End If
                '*����ԍ� 000040 2010/05/14 �ǉ��I��

                '*����ԍ� 000041 2011/05/18 �ǉ��J�n
                If (m_strFrnZairyuJohoKB_Param = "1") Then
                    ' �p�����[�^�F�O���l�ݗ����i�擾�敪��"1"�̏ꍇ
                    ' ����
                    strWork = CType(csDataRow(ABAtenaRirekiEntity.KOKUSEKI), String).Trim
                    csDataNewRow(ABAtena1Entity.KOKUSEKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KOKUSEKI)
                    ' ���Ёi�t���j
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataNewRow(ABAtena1HyojunEntity.KOKUSEKI_FULL) = csDataRow(ABAtenaRirekiEntity.KOKUSEKI)
                    Else
                    End If
                    ' �ݗ����i�R�[�h
                    csDataNewRow(ABAtena1Entity.ZAIRYUSKAKCD) = csDataRow(ABAtenaRirekiEntity.ZAIRYUSKAKCD)
                    ' �ݗ����i
                    csDataNewRow(ABAtena1Entity.ZAIRYUSKAK) = csDataRow(ABAtenaRirekiEntity.ZAIRYUSKAK)
                    ' �ݗ�����
                    csDataNewRow(ABAtena1Entity.ZAIRYUKIKAN) = csDataRow(ABAtenaRirekiEntity.ZAIRYUKIKAN)
                    ' �ݗ��J�n�N����
                    csDataNewRow(ABAtena1Entity.ZAIRYU_ST_YMD) = csDataRow(ABAtenaRirekiEntity.ZAIRYU_ST_YMD)
                    ' �ݗ��I���N����
                    csDataNewRow(ABAtena1Entity.ZAIRYU_ED_YMD) = csDataRow(ABAtenaRirekiEntity.ZAIRYU_ED_YMD)
                Else
                End If
                '*����ԍ� 000041 2011/05/18 �ǉ��I��

                '*����ԍ� 000017 2003/10/09 �C���J�n
                ''���R�[�h�̒ǉ�
                'csAtena1.Tables(ABAtena1Entity.TABLE_NAME).Rows.Add(csDataNewRow)

                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                    ' �N���p�f�[�^�쐬
                    '*����ԍ� 000027 2006/07/31 �C���J�n
                    If (strGyomuMei = NENKIN Or strGyomuMei = NENKIN_2) Then
                        'If (strGyomuMei = NENKIN) Then
                        '*����ԍ� 000027 2006/07/31 �C���I��


                        ' ����
                        csDataNewRow(ABNenkinAtenaEntity.KYUSEI) = csDataRow(ABAtenaRirekiEntity.KYUSEI)
                        ' �Z��ٓ��N����
                        csDataNewRow(ABNenkinAtenaEntity.JUTEIIDOYMD) = csDataRow(ABAtenaRirekiEntity.JUTEIIDOYMD)
                        ' �Z�莖�R
                        csDataNewRow(ABNenkinAtenaEntity.JUTEIJIYU) = csDataRow(ABAtenaRirekiEntity.JUTEIJIYU)
                        ' �]���O�Z���X�֔ԍ�
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_YUBINNO) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_YUBINNO)
                        ' �]���O�Z���S���Z���R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_ZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD)
                        ' �]���O�Z���Z��
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_JUSHO) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_JUSHO)
                        ' �]���O�Z���Ԓn
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_BANCHI) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_BANCHI)
                        ' �]���O�Z������
                        strWork = CType(csDataRow(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI), String).Trim
                        csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        ' �]�o�\��X�֔ԍ�
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO)
                        ' �]�o�\��S���Z���R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD)
                        ' �]�o�\��ٓ��N����
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD)
                        ' �]�o�\��Z��
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIJUSHO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO)
                        ' �]�o�\��Ԓn
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIBANCHI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI)
                        ' �]�o�\�����
                        strWork = CType(csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI), String).Trim
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        ' �]�o�m��X�֔ԍ�
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIYUBINNO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO)
                        ' �]�o�m��S���Z���R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD)
                        ' �]�o�m��ٓ��N����
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIIDOYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD)
                        ' �]�o�m��ʒm�N����
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD)
                        ' �]�o�m��Z��
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIJUSHO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO)
                        ' �]�o�m��Ԓn
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIBANCHI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI)
                        ' �]�o�m�����
                        strWork = CType(csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI), String).Trim
                        csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            ' �]���O�Z�������i�t���j
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI)
                            ' �]�o�\������i�t���j
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI)
                            ' �]�o�m������i�t���j
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI)
                        Else
                        End If

                        '�Z��D��̏ꍇ
                        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                            ' �ҏW�O�Ԓn
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)
                            ' �ҏW�O����
                            strWork = CType(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI), String).Trim
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                ' �ҏW�O�����i�t���j
                                csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI)
                            Else
                            End If
                        Else
                            ' �ҏW�O�Ԓn
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = csDataRow(ABAtenaRirekiEntity.BANCHI)
                            ' �ҏW�O����
                            strWork = CType(csDataRow(ABAtenaRirekiEntity.KATAGAKI), String).Trim
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                ' �ҏW�O�����i�t���j
                                csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.KATAGAKI)
                            Else
                            End If
                        End If

                        ' �����͏o�N����
                        csDataNewRow(ABNenkinAtenaEntity.SHOJOTDKDYMD) = csDataRow(ABAtenaRirekiEntity.SHOJOTDKDYMD)
                        ' ���ߎ��R�R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.CKINJIYUCD) = csDataRow(ABAtenaRirekiEntity.CKINJIYUCD)

                        '*����ԍ� 000022 2003/12/04 �ǉ��J�n
                        ' �{�БS���Z���R�[�h
                        csDataNewRow(ABNenkinAtenaEntity.HON_ZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.HON_ZJUSHOCD)
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        ' �]�o�\�萢�ю喼
                        'csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)
                        ' �]�o�m�萢�ю喼
                        'csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            ' �]�o�\�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)))
                            ' �]�o�m�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            ' �]�o�\�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)
                            ' �]�o�m�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                        ' ���ЃR�[�h
                        csDataNewRow(ABNenkinAtenaEntity.KOKUSEKICD) = csDataRow(ABAtenaRirekiEntity.KOKUSEKICD)
                        '*����ԍ� 000022 2003/12/04 �ǉ��I��
                        '*����ԍ� 000027 2006/07/31 �ǉ��J�n
                        If strGyomuMei = NENKIN_2 Then
                            '* �����J�n 000035 2008/02/15 �C���J�n
                            '�]���O�Z�����ю喼
                            'csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI)
                            If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                                ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                                csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI)))
                            Else
                                ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                                csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI)
                            End If
                            '* �����J�n 000035 2008/02/15 �C���I��
                        End If
                        '*����ԍ� 000027 2006/07/31 �ǉ��I��
                    End If

                    '*����ԍ� 000030 2007/04/28 �ǉ��J�n
                    '���p�T�u���[�`���擾����
                    If m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo Then
                        ' ����
                        csDataNewRow(ABAtena1Entity.KYUSEI) = csDataRow(ABAtenaRirekiEntity.KYUSEI)
                        ' �Z��ٓ��N����
                        csDataNewRow(ABAtena1Entity.JUTEIIDOYMD) = csDataRow(ABAtenaRirekiEntity.JUTEIIDOYMD)
                        ' �Z�莖�R
                        csDataNewRow(ABAtena1Entity.JUTEIJIYU) = csDataRow(ABAtenaRirekiEntity.JUTEIJIYU)
                        ' �{�БS���Z���R�[�h
                        csDataNewRow(ABAtena1Entity.HON_ZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.HON_ZJUSHOCD)
                        ' �]���O�Z���X�֔ԍ�
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_YUBINNO) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_YUBINNO)
                        ' �]���O�Z���S���Z���R�[�h
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_ZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD)
                        ' �]���O�Z���Z��
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_JUSHO) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_JUSHO)
                        ' �]���O�Z���Ԓn
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_BANCHI) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_BANCHI)
                        ' �]���O�Z������
                        strWork = CType(csDataRow(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.TENUMAEJ_KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        ' �]�o�\��X�֔ԍ�
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIYUBINNO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO)
                        ' �]�o�\��S���Z���R�[�h
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD)
                        ' �]�o�\��ٓ��N����
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIIDOYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD)
                        ' �]�o�\��Z��
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIJUSHO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO)
                        ' �]�o�\��Ԓn
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIBANCHI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI)
                        ' �]�o�\�����
                        strWork = CType(csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            ' �]���O�Z�������i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI)
                            ' �]�o�\������i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI)
                        Else
                        End If
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        ' �]�o�\�萢�ю喼
                        'csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��
                        ' �]�o�m��X�֔ԍ�
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIYUBINNO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO)
                        ' �]�o�m��S���Z���R�[�h
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIZJUSHOCD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD)
                        ' �]�o�m��ٓ��N����
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIIDOYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD)
                        ' �]�o�m��ʒm�N����
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTITSUCHIYMD) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD)
                        ' �]�o�m��Z��
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIJUSHO) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO)
                        ' �]�o�m��Ԓn
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIBANCHI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI)
                        ' �]�o�m�����
                        strWork = CType(csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.TENSHUTSUKKTIKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            ' �]�o�m������i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI)
                        Else
                        End If
                        '* �����J�n 000035 2008/02/15 �C���J�n
                        ' �]�o�m�萢�ю喼
                        'csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)
                        If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = m_cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)))
                        Else
                            ' �O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                            csDataNewRow(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)
                        End If
                        '* �����J�n 000035 2008/02/15 �C���I��

                        '�Z��D��̏ꍇ
                        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                            ' �ҏW�O�Ԓn
                            csDataNewRow(ABAtena1Entity.HENSHUMAEBANCHI) = csDataRow(ABAtenaRirekiEntity.JUKIBANCHI)
                            ' �ҏW�O����
                            strWork = CType(csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI), String).Trim
                            csDataNewRow(ABAtena1Entity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                ' �ҏW�O�����i�t���j
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.JUKIKATAGAKI)
                            Else
                            End If
                        Else
                            ' �ҏW�O�Ԓn
                            csDataNewRow(ABAtena1Entity.HENSHUMAEBANCHI) = csDataRow(ABAtenaRirekiEntity.BANCHI)
                            ' �ҏW�O����
                            strWork = CType(csDataRow(ABAtenaRirekiEntity.KATAGAKI), String).Trim
                            csDataNewRow(ABAtena1Entity.HENSHUMAEKATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                ' �ҏW�O�����i�t���j
                                csDataNewRow(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL) = csDataRow(ABAtenaRirekiEntity.KATAGAKI)
                            Else
                            End If
                        End If

                        ' �����͏o�N����
                        csDataNewRow(ABAtena1Entity.SHOJOTDKDYMD) = csDataRow(ABAtenaRirekiEntity.SHOJOTDKDYMD)
                        ' ���ߎ��R�R�[�h
                        csDataNewRow(ABAtena1Entity.CKINJIYUCD) = csDataRow(ABAtenaRirekiEntity.CKINJIYUCD)
                        ' ���ЃR�[�h
                        csDataNewRow(ABAtena1Entity.KOKUSEKICD) = csDataRow(ABAtenaRirekiEntity.KOKUSEKICD)
                        ' �o�^�͏o�N����
                        csDataNewRow(ABAtena1Entity.TOROKUTDKDYMD) = csDataRow(ABAtenaRirekiEntity.TOROKUTDKDYMD)
                        ' �Z��͏o�N����
                        csDataNewRow(ABAtena1Entity.JUTEITDKDYMD) = csDataRow(ABAtenaRirekiEntity.JUTEITDKDYMD)
                        ' �]�o�����R
                        csDataNewRow(ABAtena1Entity.TENSHUTSUNYURIYU) = csDataRow(ABAtenaRirekiEntity.TENSHUTSUNYURIYU)
                        ' �s�����R�[�h
                        csDataNewRow(ABAtena1Entity.SHICHOSONCD) = csDataRow(ABAtenaRirekiEntity.SHICHOSONCD)
                        If (Not csDataRow(ABAtenaRirekiEntity.CKINJIYUCD).ToString.Trim = String.Empty) AndAlso
                            (csDataRow(ABAtenaRirekiEntity.CKINIDOYMD).ToString.Trim = String.Empty) Then
                            csDataNewRow(ABAtena1Entity.CKINIDOYMD) = m_strCknIdobiHenkanParam
                        Else
                            ' ���߈ٓ��N����
                            csDataNewRow(ABAtena1Entity.CKINIDOYMD) = csDataRow(ABAtenaRirekiEntity.CKINIDOYMD)
                        End If
                        ' �X�V����
                        csDataNewRow(ABAtena1Entity.KOSHINNICHIJI) = csDataRow(ABAtenaRirekiEntity.KOSHINNICHIJI)
                    End If
                    '*����ԍ� 000030 2007/04/28 �ǉ��I��

                End If
                '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
                '*����ԍ� 000019 2003/11/19 �ǉ��J�n
                ' �����ʏ��p�f�[�^�쐬(�{�l���R�[�h�̂ݐݒ�)
                If (strGyomuMei = KOBETSU) And (strDainoKB.Trim = String.Empty) Then

                    ' ��b�N���ԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.KSNENKNNO) = csDataRow(ABAtena1KobetsuEntity.KSNENKNNO)
                    ' �N�����i�擾�N����	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD)
                    ' �N�����i�擾���	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU)
                    ' �N�����i�擾���R�R�[�h	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD)
                    ' �N�����i�r���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD)
                    ' �N�����i�r�����R�R�[�h	
                    csDataNewRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD) = csDataRow(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD)
                    ' �󋋔N���L���P	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO1)
                    ' �󋋔N���ԍ��P	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO1)
                    ' �󋋔N����ʂP	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU1)
                    ' �󋋔N���}�ԂP	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN1)
                    ' �󋋔N���敪�P	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB1) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB1)
                    ' �󋋔N���L���Q	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO2)
                    ' �󋋔N���ԍ��Q	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO2)
                    ' �󋋔N����ʂQ	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU2)
                    ' �󋋔N���}�ԂQ	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN2)
                    ' �󋋔N���敪�Q	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB2) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB2)
                    ' �󋋔N���L���R	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKIGO3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKIGO3)
                    ' �󋋔N���ԍ��R	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNNO3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNNO3)
                    ' �󋋔N����ʂR	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNSHU3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNSHU3)
                    ' �󋋔N���}�ԂR	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNEDABAN3)
                    ' �󋋔N���敪�R	
                    csDataNewRow(ABAtena1KobetsuEntity.JKYNENKNKB3) = csDataRow(ABAtena1KobetsuEntity.JKYNENKNKB3)
                    ' ���۔ԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHONO) = csDataRow(ABAtena1KobetsuEntity.KOKUHONO)
                    ' ���ێ��i�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB)
                    ' ���ێ��i�敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO)
                    ' ���ێ��i�敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO)
                    ' ���ۊw���敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKB)
                    ' ���ۊw���敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO)
                    ' ���ۊw���敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO)
                    ' ���ێ擾�N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD)
                    ' ���ۑr���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD)
                    ' ���ۑސE�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKB)
                    ' ���ۑސE�敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO)
                    ' ���ۑސE�敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO)
                    ' ���ۑސE�{��敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB)
                    ' ���ۑސE�{��敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO)
                    ' ���ۑސE�{��敪��������	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
                    ' ���ۑސE�Y���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD)
                    ' ���ۑސE��Y���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD)
                    ' ���ەی��؋L��	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO)
                    ' ���ەی��ؔԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO) = csDataRow(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO)
                    ' ��Ӕԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.INKANNO) = csDataRow(ABAtena1KobetsuEntity.INKANNO)
                    ' ��ӓo�^�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.INKANTOROKUKB) = csDataRow(ABAtena1KobetsuEntity.INKANTOROKUKB)
                    ' �I�����i�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB)
                    ' �����p�敪	
                    csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB)
                    ' ����J�n�N����	
                    csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATESTYM) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATESTYM)
                    ' ����I���N����	
                    csDataNewRow(ABAtena1KobetsuEntity.JIDOTEATEEDYM) = csDataRow(ABAtena1KobetsuEntity.JIDOTEATEEDYM)
                    ' ����ی��Ҕԍ�	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGHIHKNSHANO) = csDataRow(ABAtena1KobetsuEntity.KAIGHIHKNSHANO)
                    ' ��쎑�i�擾��	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD)
                    ' ��쎑�i�r����	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD)
                    ' ��쎑�i��ی��ҋ敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB)
                    ' ���Z���n����ҋ敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB) = csDataRow(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB)
                    ' ���󋋎ҋ敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB)
                    ' �v����ԋ敪�R�[�h	
                    csDataNewRow(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD) = csDataRow(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD)
                    ' �v����ԋ敪	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGSKAKKB) = csDataRow(ABAtena1KobetsuEntity.KAIGSKAKKB)
                    ' ���F��L���J�n��	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD)
                    ' ���F��L���I����	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD)
                    ' ���󋋔F��N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD)
                    ' ���󋋔F�����N����	
                    csDataNewRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD) = csDataRow(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD)

                    '*����ԍ� 000034 2008/01/15 �ǉ��J�n
                    If (m_strKobetsuShutokuKB = "1") Then
                        ' �ʎ����擾�敪��"1"�̏ꍇ�͌������ڂ�ǉ�����
                        ' ���i�敪
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB)
                        ' ��ی��Ҕԍ�
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO)
                        ' ��ی��Ҏ��i�擾���R�R�[�h
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD)
                        ' ��ی��Ҏ��i�擾���R����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI)
                        ' ��ی��Ҏ��i�擾�N����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD)
                        ' ��ی��Ҏ��i�r�����R�R�[�h
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD)
                        ' ��ی��Ҏ��i�r�����R����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI)
                        ' ��ی��Ҏ��i�r���N����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD)
                        ' �ی��Ҕԍ��K�p�J�n�N����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD)
                        ' �ی��Ҕԍ��K�p�I���N����
                        csDataNewRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD) = csDataRow(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD)
                    Else
                        ' �ʎ����擾�敪���l�Ȃ��̏ꍇ�͌������ڂ�ǉ����Ȃ�
                    End If
                    '*����ԍ� 000034 2008/01/15 �ǉ��I��

                End If
                '*����ԍ� 000019 2003/11/19 �ǉ��I��

                '*����ԍ� 000046 2011/11/07 �ǉ��J�n
                '�Z��@��������
                If (m_strJukiHokaiseiKB_Param = "1") Then
                    '�Z���[��ԋ敪
                    csDataNewRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN) = csDataRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN)
                    '�Z���n�͏o�L���t���O
                    csDataNewRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG) = csDataRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG)
                    '�{����
                    csDataNewRow(ABAtenaFZYEntity.HONGOKUMEI) = csDataRow(ABAtenaFZYEntity.HONGOKUMEI)
                    '�J�i�{����
                    csDataNewRow(ABAtenaFZYEntity.KANAHONGOKUMEI) = csDataRow(ABAtenaFZYEntity.KANAHONGOKUMEI)
                    '���L��
                    csDataNewRow(ABAtenaFZYEntity.KANJIHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KANJIHEIKIMEI)
                    '�J�i���L��
                    csDataNewRow(ABAtenaFZYEntity.KANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KANAHEIKIMEI)
                    '�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.KANJITSUSHOMEI) = csDataRow(ABAtenaFZYEntity.KANJITSUSHOMEI)
                    '�J�i�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.KANATSUSHOMEI) = csDataRow(ABAtenaFZYEntity.KANATSUSHOMEI)
                    '�J�^�J�i���L��
                    csDataNewRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI)
                    '���N�����s�ڋ敪
                    csDataNewRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = csDataRow(ABAtenaFZYEntity.UMAREFUSHOKBN)
                    '�ʏ̖��o�^�i�ύX�j�N����
                    csDataNewRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD) = csDataRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD)
                    '�ݗ����ԃR�[�h
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANCD) = csDataRow(ABAtenaFZYEntity.ZAIRYUKIKANCD)
                    '�ݗ����Ԗ���
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO) = csDataRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO)
                    '�������ݗ��҂ł���|���̃R�[�h
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHACD) = csDataRow(ABAtenaFZYEntity.ZAIRYUSHACD)
                    '�������ݗ��҂ł���|��
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO) = csDataRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO)
                    '�ݗ��J�[�h���ԍ�
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUCARDNO) = csDataRow(ABAtenaFZYEntity.ZAIRYUCARDNO)
                    '���ʉi�Z�ҏؖ�����t�N����
                    csDataNewRow(ABAtenaFZYEntity.KOFUYMD) = csDataRow(ABAtenaFZYEntity.KOFUYMD)
                    '���ʉi�Z�ҏؖ�����t�\����ԊJ�n��
                    csDataNewRow(ABAtenaFZYEntity.KOFUYOTEISTYMD) = csDataRow(ABAtenaFZYEntity.KOFUYOTEISTYMD)
                    '����i�Z�ҏؖ�����t�\����ԏI����
                    csDataNewRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD) = csDataRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD)
                    '�Z��Ώێҁi��30��45��Y���j�����ٓ��N����
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
                    '�Z��Ώێҁi��30��45��Y���j�������R�R�[�h
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
                    '�Z��Ώێҁi��30��45��Y���j�������R
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU)
                    '�Z��Ώێҁi��30��45��Y���j�����͏o�N����
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
                    '�Z��Ώێҁi��30��45��Y���j�����͏o�ʒm�敪
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB) = csDataRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
                    '�O���l���ю喼
                    csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSMEI) = csDataRow(ABAtenaFZYEntity.FRNSTAINUSMEI)
                    '�O���l���ю�J�i��
                    csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI) = csDataRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI)
                    '���ю啹�L��
                    csDataNewRow(ABAtenaFZYEntity.STAINUSHEIKIMEI) = csDataRow(ABAtenaFZYEntity.STAINUSHEIKIMEI)
                    '���ю�J�i���L��
                    csDataNewRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI) = csDataRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI)
                    '���ю�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI) = csDataRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI)
                    '���ю�J�i�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI) = csDataRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI)
                Else
                    '�����Ȃ�
                End If
                '*����ԍ� 000046 2011/11/07 �ǉ��I��

                '*����ԍ� 000048 2014/04/28 �ǉ��J�n
                ' ���ʔԍ�����
                If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                    ' �󔒏��������l��ݒ肷��B
                    csDataNewRow(ABMyNumberEntity.MYNUMBER) = csDataRow(ABMyNumberEntity.MYNUMBER).ToString.Trim
                Else
                    ' noop
                End If
                '*����ԍ� 000048 2014/04/28 �ǉ��I��

                If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    ' ���ю厁���D��敪
                    csDataNewRow(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB) = csDataRow(ABAtenaRirekiFZYHyojunEntity.STAINUSSHIMEIYUSENKB)
                    ' �����D�捀��
                    csDataNewRow(ABAtena1HyojunEntity.SHIMEIYUSENKB) = csDataRow(ABAtenaRirekiFZYHyojunEntity.SHIMEIYUSENKB)
                    ' ����
                    csDataNewRow(ABAtena1HyojunEntity.KANJIKYUUJI) = csDataRow(ABAtenaRirekiFZYEntity.RESERVE7)
                    ' �J�i����
                    csDataNewRow(ABAtena1HyojunEntity.KANAKYUUJI) = csDataRow(ABAtenaRirekiFZYEntity.RESERVE8)
                    ' �����t���K�i�m�F�t���O
                    csDataNewRow(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG) = csDataRow(ABAtenaRirekiHyojunEntity.SHIMEIKANAKAKUNINFG)
                    ' �����t���K�i�m�F�t���O
                    csDataNewRow(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG) = csDataRow(ABAtenaRirekiHyojunEntity.KYUUJIKANAKAKUNINFG)
                    ' �ʏ̃t���K�i�m�F�t���O
                    csDataNewRow(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG) = csDataRow(ABAtenaRirekiFZYHyojunEntity.TSUSHOKANAKAKUNINFG)
                    ' ���N�����s�ڃp�^�[��
                    csDataNewRow(ABAtena1HyojunEntity.UMAREBIFUSHOPTN) = csDataRow(ABAtenaRirekiHyojunEntity.UMAREBIFUSHOPTN)
                    ' �s�ڐ��N����
                    csDataNewRow(ABAtena1HyojunEntity.FUSHOUMAREBI) = csDataRow(ABAtenaRirekiHyojunEntity.FUSHOUMAREBI)
                    ' �L�ڎ��R
                    csDataNewRow(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD) = csDataRow(ABAtenaRirekiHyojunEntity.HYOJUNKISAIJIYUCD)
                    ' �L�ڔN����
                    csDataNewRow(ABAtena1HyojunEntity.KISAIYMD) = csDataRow(ABAtenaRirekiHyojunEntity.KISAIYMD)
                    ' �������R
                    csDataNewRow(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD) = csDataRow(ABAtenaRirekiHyojunEntity.HYOJUNSHOJOJIYUCD)

                    If ((csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTONAI_KOJIN) OrElse
                        (csDataRow(ABAtenaRirekiEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_JUTOGAI_KOJIN)) AndAlso
                       (Not csDataRow(ABAtenaRirekiEntity.SHOJOJIYUCD).ToString.Trim = String.Empty) Then
                        If (csDataRow(ABAtenaRirekiHyojunEntity.SHOJOIDOWMD).ToString.Trim = String.Empty) Then
                            csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = m_strShojoIdoWmdHenkan
                        Else
                            csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csDataRow(ABAtenaRirekiHyojunEntity.SHOJOIDOWMD)
                        End If
                    Else
                        ' �����ٓ��a��N����
                        csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csDataRow(ABAtenaRirekiHyojunEntity.SHOJOIDOWMD)
                    End If
                    ' �����ٓ����s�ڃp�^�[��
                    csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN) = csDataRow(ABAtenaRirekiHyojunEntity.SHOJOIDOBIFUSHOPTN)
                    ' �s�ڏ����ٓ���
                    csDataNewRow(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI) = csDataRow(ABAtenaRirekiHyojunEntity.FUSHOSHOJOIDOBI)

                    If (Not csDataRow(ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI).ToString.Trim = String.Empty) AndAlso
                       (csDataRow(ABAtenaRirekiHyojunEntity.CKINIDOWMD).ToString.Trim = String.Empty) Then
                        csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = m_strCknIdoWmdHenkan
                    Else
                        ' ���߈ٓ��a��N����
                        csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = csDataRow(ABAtenaRirekiHyojunEntity.CKINIDOWMD)
                    End If
                    ' ���߈ٓ����s�ڃp�^�[��
                    csDataNewRow(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN) = csDataRow(ABAtenaRirekiHyojunEntity.CKINIDOBIFUSHOPTN)
                    ' �s�ڒ��߈ٓ���
                    csDataNewRow(ABAtena1HyojunEntity.FUSHOCKINIDOBI) = csDataRow(ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI)
                    ' ������̐��ю�
                    csDataNewRow(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI) = csDataRow(ABAtenaRirekiHyojunEntity.JIJITSUSTAINUSMEI)
                    If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                        ' �Z��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSONCD)
                        ' �Z��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.JUKIMACHIAZACD)
                        ' �Z��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.JUKITODOFUKEN)
                        ' �Z��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSON)
                        ' �Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.JUKIMACHIAZA)
                    Else
                        ' �Z��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.SHIKUCHOSONCD)
                        ' �Z��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.MACHIAZACD)
                        ' �Z��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TODOFUKEN)
                        ' �Z��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.SHIKUCHOSON)
                        ' �Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.MACHIAZA)
                    End If
                    If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                        ' �{��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.HON_SHIKUCHOSONCD)
                        ' �{��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.HON_MACHIAZACD)
                        ' �{��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.HON_TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.HON_TODOFUKEN)
                        ' �{��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.HON_SHIKUGUNCHOSON)
                        ' �{��_����
                        csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.HON_MACHIAZA)
                    End If
                    If (m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo) AndAlso
                       (strGyomuMei <> NENKIN) AndAlso (strGyomuMei <> NENKIN_2) Then
                        ' ���ЃR�[�h
                        csDataNewRow(ABAtena1HyojunEntity.KOKUSEKICD) = csDataRow(ABAtenaRirekiEntity.KOKUSEKICD)
                    End If
                    If (strGyomuMei = NENKIN Or strGyomuMei = NENKIN_2) Then
                        ' �]���O�Z��_�s�撬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD)
                        ' �]���O�����R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZACD)
                        ' �]���O�Z��_�s���{��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_TODOFUKEN)
                        ' �]���O�Z��_�s��S������
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSON)
                        ' �]���O�Z��_����
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZA)
                        ' �]���O�Z��_�����R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKICD)
                        ' �]���O�Z��_����
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKI) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKI)
                        ' �]���O�Z��_���O�Z��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO)
                        ' �]�o�m��_�s�撬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD)
                        ' �]�o�m�蒬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD)
                        ' �]�o�m��_�s���{��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTITODOFUKEN)
                        ' �]�o�m��_�s��S������
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON)
                        ' �]�o�m��_����
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZA)
                        ' �]�o�\��_�s�撬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD)
                        ' �]�o�\�蒬���R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD)
                        ' �]�o�\��_�s���{��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN)
                        ' �]�o�\��_�s��S������
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON)
                        ' �]�o�\��_����
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA)
                        ' �]�o�\��_�����R�[�h
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD)
                        ' �]�o�\��_������
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI)
                        ' �]�o�\��_���O�Z��
                        csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO)
                    End If
                    If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo) Then
                        ' �]���O�Z��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD)
                        ' �]���O�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZACD)
                        ' �]���O�Z��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_TODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_TODOFUKEN)
                        ' �]���O�Z��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSON)
                        ' �]���O�Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZA)
                        ' �]���O�Z��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKICD) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKICD)
                        ' �]���O�Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKI) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKI)
                        ' �]���O�Z��_���O�Z��
                        csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csDataRow(ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO)
                        ' �]�o�m��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD)
                        ' �]�o�m�蒬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD)
                        ' �]�o�m��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTITODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTITODOFUKEN)
                        ' �]�o�m��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON)
                        ' �]�o�m��_����
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZA)
                        ' �]�o�\��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD)
                        ' �]�o�\�蒬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD)
                        ' �]�o�\��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN)
                        ' �]�o�\��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON)
                        ' �]�o�\��_����
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA)
                        ' �]�o�\��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD)
                        ' �]�o�\��_������
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI)
                        ' �]�o�\��_���O�Z��
                        csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csDataRow(ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO)
                    Else
                    End If
                    ' �@��30��46����47�敪
                    csDataNewRow(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB) = csDataRow(ABAtenaRirekiFZYHyojunEntity.HODAI30JO46MATAHA47KB)
                    ' �ݗ��J�[�h���ԍ��敪
                    csDataNewRow(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN) = csDataRow(ABAtenaRirekiFZYHyojunEntity.ZAIRYUCARDNOKBN)
                    ' �Z���n�␳�R�[�h
                    csDataNewRow(ABAtena1HyojunEntity.JUKYOCHIHOSEICD) = csDataRow(ABAtenaRirekiFZYHyojunEntity.JUKYOCHIHOSEICD)
                    ' ���ߓ͏o�ʒm�敪
                    csDataNewRow(ABAtena1HyojunEntity.CKINTDKDTUCIKB) = csDataRow(ABAtenaRirekiEntity.CKINTDKDTUCIKB)
                    ' �Ŕԍ�
                    csDataNewRow(ABAtena1HyojunEntity.HANNO) = csDataRow(ABAtenaRirekiEntity.HANNO)
                    ' �����N����
                    csDataNewRow(ABAtena1HyojunEntity.KAISEIYMD) = csDataRow(ABAtenaRirekiEntity.KAISEIYMD)
                    ' �ٓ��敪
                    csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOKB) = csDataRow(ABAtenaRirekiHyojunEntity.HYOJUNIDOKB)
                    ' ���͏ꏊ�R�[�h
                    csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHOCD) = csDataRow(ABAtenaRirekiHyojunEntity.NYURYOKUBASHOCD)
                    ' ���͏ꏊ�\�L
                    csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHO) = csDataRow(ABAtenaRirekiHyojunEntity.NYURYOKUBASHO)
                    If (strGyomuMei = KOBETSU) And (strDainoKB.Trim = String.Empty) Then
                        ' ���_��ی��ҊY���L��
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB)
                        ' ����_��ی��ҊY���L��
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB)
                        ' �N��_��ی��ҊY���L��
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB)
                        ' �N��_��ʕύX�N����
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD) = csDataRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD)
                        ' �I��_��ԋ敪
                        csDataNewRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN) = csDataRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN)
                        If (m_strKobetsuShutokuKB = "1") Then
                            ' �������_��ی��ҊY���L��
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB) = csDataRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB)
                        End If
                    End If
                    ' �A����敪�i�A����j
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIKB) = String.Empty
                    ' �A���於
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIMEI) = String.Empty
                    ' �A����1�i�A����j
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI) = String.Empty
                    ' �A����2�i�A����j
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI) = String.Empty
                    ' �A����3�i�A����j
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI) = String.Empty
                    ' �A������1
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1) = String.Empty
                    ' �A������2
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2) = String.Empty
                    ' �A������3
                    csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3) = String.Empty
                    '* ����ԍ� 000051 2023/10/19 �C���J�n
                    'If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) Then
                    If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) AndAlso
                       (csDataRow.Table.Columns.Contains(ABFugenjuJohoEntity.FUGENJUKB)) Then
                        '* ����ԍ� 000051 2023/10/19 �C���I��
                        ' �s���Z�敪
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUKB) = csDataRow(ABFugenjuJohoEntity.FUGENJUKB)
                        ' �s���Z�������Z��_�X�֔ԍ�
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_YUBINNO)
                        ' �s���Z�������Z��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHICHOSONCD)
                        ' �s���Z�������Z��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZACD)
                        ' �s���Z�������Z��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_TODOFUKEN)
                        ' �s���Z�������Z��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON)
                        ' �s���Z�������Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZA)
                        ' �s���Z�������Z��_�Ԓn���\�L
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)
                        ' �s���Z�������Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)
                        ' �s���Z�������Z��_����_�t���K�i
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI) = csDataRow(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI)
                        ' �s���Z���i�Ώێҋ敪�j
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKUBUN)
                        ' �s���Z���i�ΏێҎ����j
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI)
                        ' �s���Z���i���N�����j
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD)
                        ' �s���Z���i���ʁj
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU)
                        ' ���Z�s���N����
                        csDataNewRow(ABAtena1HyojunEntity.KYOJUFUMEI_YMD) = csDataRow(ABFugenjuJohoEntity.KYOJUFUMEI_YMD)
                        ' �s���Z���i���l�j
                        csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO) = csDataRow(ABFugenjuJohoEntity.FUGENJUJOHO_BIKO)
                    Else
                    End If
                    If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                        ' �ԍ��@�X�V�敪
                        csDataNewRow(ABAtena1HyojunEntity.BANGOHOKOSHINKB) = csDataRow(ABMyNumberHyojunEntity.BANGOHOKOSHINKB)
                    End If
                    '* ����ԍ� 000051 2023/10/19 �C���J�n
                    'If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1) AndAlso (strGyomuMei <> NENKIN) AndAlso (strGyomuMei <> NENKIN_2) Then
                    If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1) AndAlso (strGyomuMei <> NENKIN) AndAlso (strGyomuMei <> NENKIN_2) AndAlso
                       (csDataRow.Table.Columns.Contains(ABDENSHISHOMEISHOMSTEntity.SERIALNO)) Then
                        '* ����ԍ� 000051 2023/10/19 �C���I��
                        ' �V���A���ԍ�
                        csDataNewRow(ABAtena1HyojunEntity.SERIALNO) = csDataRow(ABDENSHISHOMEISHOMSTEntity.SERIALNO)
                    End If
                    ' �W�������ٓ����R�R�[�h
                    csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD) = csDataRow(ABAtenaRirekiHyojunEntity.HYOJUNIDOJIYUCD)
                    If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) Then
                        ' �A����敪�i���t��j
                        csDataNewRow(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB) = String.Empty
                        ' ���t��敪
                        csDataNewRow(ABAtena1HyojunEntity.SFSKKBN) = String.Empty
                    Else
                    End If

                    strAtenaDataKB = CType(csDataRow(ABAtenaRirekiEntity.ATENADATAKB), String).Trim
                    strAtenaDataSHU = CType(csDataRow(ABAtenaRirekiEntity.ATENADATASHU), String).Trim
                    m_cABHyojunkaCdHenshuB.HenshuHyojunkaCd(strAtenaDataKB, strAtenaDataSHU)
                    ' �Z���敪
                    csDataNewRow(ABAtena1HyojunEntity.JUMINKBN) = m_cABHyojunkaCdHenshuB.p_strJuminKbn
                    ' �Z�����
                    csDataNewRow(ABAtena1HyojunEntity.JUMINSHUBETSU) = m_cABHyojunkaCdHenshuB.p_strJuminShubetsu
                    ' �Z�����
                    csDataNewRow(ABAtena1HyojunEntity.JUMINJOTAI) = m_cABHyojunkaCdHenshuB.p_strJuminJotai
                    If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                        ' �Ԓn�}�Ԑ��l
                        csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = csDataRow(ABAtenaRirekiHyojunEntity.JUKIBANCHIEDABANSUCHI)
                    Else
                        ' �Ԓn�}�Ԑ��l
                        csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = csDataRow(ABAtenaRirekiHyojunEntity.BANCHIEDABANSUCHI)
                    End If
                Else
                    ' noop
                End If

                '�f�[�^���R�[�h�̒ǉ�
                csDataTable.Rows.Add(csDataNewRow)

                '*����ԍ� 000017 2003/10/09 �C���I��

            Next csDataRow

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exException As UFAppException

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �X���[����
            Throw exException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csAtena1

    End Function
#End Region

#Region " ���t��ҏW(SofusakiHenshu) "
    '*����ԍ� 000019 2003/11/19 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���t��ҏW
    '* 
    '* �\��           Public Function SofusakiHenshu(ByVal csAtena1 As DataSet, _
    '*                                              ByVal csSfskEntity As DataSet, _
    '*                                              ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@ �ҏW�����f�[�^���쐬����
    '* 
    '* ����           csAtena1              : ���������f�[�^
    '*               csSfskEntity           : ���t��f�[�^
    '*               cAtenaGetPara1         : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena12)    : �擾�����������
    '************************************************************************************************
    Public Overloads Function SofusakiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                             ByVal csAtena1 As DataSet,
                                             ByVal csSfskEntity As DataSet) As DataSet
        Return SofusakiHenshu(cAtenaGetPara1, csAtena1, csSfskEntity, String.Empty)
    End Function
    '*����ԍ� 000019 2003/11/19 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     ���t��ҏW
    '* 
    '* �\��           Public Function SofusakiHenshu(ByVal csAtena1 As DataSet, _
    '*                                              ByVal csSfskEntity As DataSet, _
    '*                                              ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@ �ҏW�����f�[�^���쐬����
    '* 
    '* ����           csAtena1              : �����擾�f�[�^
    '*               csSfskEntity           : ���t��f�[�^
    '*               cAtenaGetPara1         : �����擾�p�����[�^
    '*               strGyomuMei            : �Ɩ���
    '* 
    '* �߂�l         DataSet(ABAtena12)    : �擾�����������
    '************************************************************************************************
    '*����ԍ� 000019 2003/11/19 �C���J�n
    'Public Overloads Function SofusakiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, _
    '                                         ByVal csAtena1 As DataSet, _
    '                                         ByVal csSfskEntity As DataSet) As DataSet
    <SecuritySafeCritical>
    Private Overloads Function SofusakiHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                        ByVal csAtena1 As DataSet,
                                        ByVal csSfskEntity As DataSet,
                                        ByVal strGyomuMei As String) As DataSet
        '*����ԍ� 000019 2003/11/19 �C���I��
        Const THIS_METHOD_NAME As String = "SofusakiHenshu"
        'Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 Start 2010/04/16 000039
        'Dim csDataSet As DataSet
        '* corresponds to VS2008 End 2010/04/16 000039
        Dim csDataTable As DataTable
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cuUSSCityInfo As USSCityInfoClass               '�s�������Ǘ��N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        Dim csAtena1Row As DataRow                          '����������Row
        Dim csAtena12 As DataSet                            '�������(ABAtena1)
        Dim csDataNewRow As DataRow                         '�������o��Row
        Dim csSfskRow As DataRow                            '���t��DataRow
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cABKannaiKangaiKBB As ABKannaiKangaiKBBClass    '�Ǔ��ǊO�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        Dim strHenshuJusho As String                        '�ҏW�Z��
        '*����ԍ� 000008 2003/03/17 �ǉ��J�n
        '*����ԍ� 000016 2003/08/22 �폜�J�n
        'Dim cURKanriJohoB As URKANRIJOHOBClass              '�Ǘ����擾�N���X
        '*����ԍ� 000016 2003/08/22 �폜�I��
        Dim cSofuJushoGyoseikuType As SofuJushoGyoseikuType
        Dim strJushoHenshu3 As String                       '�Z���ҏW�R
        Dim strJushoHenshu4 As String                       '�Z���ҏW�S
        '*����ԍ� 000008 2003/03/17 �ǉ��I��
        '*����ԍ� 000019 2003/11/19 �ǉ��J�n
        Dim dsAtena1Table As DataTable                      ' �����擾�f�[�^Table
        '*����ԍ� 000019 2003/11/19 �ǉ��I��
        '* ����ԍ� 000029 2007/01/25 �ǉ��J�n
        Dim crBanchiCdMstB As URBANCHICDMSTBClass           ' UR�Ԓn�R�[�h�}�X�^�N���X
        Dim strBanchiCD() As String                         ' �Ԓn�R�[�h�擾�p�z��
        Dim strMotoBanchiCD() As String                     ' �ύX�O�Ԓn�R�[�h
        Dim intLoop As Integer                              ' ���[�v�J�E���^
        '* ����ԍ� 000029 2007/01/25 �ǉ��I��
        '*����ԍ� 000037 2008/11/17 �ǉ��J�n
        Dim csColumn As DataColumn
        '*����ԍ� 000037 2008/11/17 �ǉ��I��
        Dim strWork As String

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ''�G���[�����N���X�̃C���X�^���X�쐬
            ''*����ԍ� 000010  2003/03/27 �C���J�n
            ''cfErrorClass = New UFErrorClass(m_cfUFControlData.m_strBusinessId)
            'cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            ''*����ԍ� 000010  2003/03/27 �C���I��

            '*����ԍ� 000019 2003/11/19 �C���J�n
            ''�J�������쐬
            'csAtena12 = New DataSet()
            'csAtena12.Tables.Add(Me.CreateAtena1Columns())

            ' �J�������쐬
            Select Case strGyomuMei
                '*����ԍ� 000027 2006/07/31 �C���J�n
                Case NENKIN, NENKIN_2    ' �N���������
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataTable = Me.CreateNenkinAtenaHyojunColumns(strGyomuMei)
                        dsAtena1Table = csAtena1.Tables(ABNenkinAtenaHyojunEntity.TABLE_NAME)
                    Else
                        csDataTable = Me.CreateNenkinAtenaColumns(strGyomuMei)
                        'Case NENKIN     ' �N���������
                        'csDataTable = Me.CreateNenkinAtenaColumns()
                        '*����ԍ� 000027 2006/07/31 �C���I��
                        dsAtena1Table = csAtena1.Tables(ABNenkinAtenaEntity.TABLE_NAME)
                    End If
                Case KOBETSU    ' �����ʏ��
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataTable = Me.CreateAtena1KobetsuHyojunColumns()
                        dsAtena1Table = csAtena1.Tables(ABAtena1KobetsuHyojunEntity.TABLE_NAME)
                    Else
                        csDataTable = Me.CreateAtena1KobetsuColumns()
                        dsAtena1Table = csAtena1.Tables(ABAtena1KobetsuEntity.TABLE_NAME)
                    End If
                Case Else       ' �������
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataTable = Me.CreateAtena1HyojunColumns()
                        dsAtena1Table = csAtena1.Tables(ABAtena1HyojunEntity.TABLE_NAME)
                    Else
                        csDataTable = Me.CreateAtena1Columns()
                        dsAtena1Table = csAtena1.Tables(ABAtena1Entity.TABLE_NAME)
                    End If
            End Select
            csAtena12 = New DataSet()
            csAtena12.Tables.Add(csDataTable)
            '*����ԍ� 000019 2003/11/19 �C���I��

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            '�s�������̃C���X�^���X�쐬
            'cuUSSCityInfo = New USSCityInfoClass()

            '�Ǔ��ǊO�̃C���X�^���X�쐬
            'cABKannaiKangaiKBB = New ABKannaiKangaiKBBClass(m_cfUFControlData, m_cfUFConfigDataClass)
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '*����ԍ� 000008 2003/03/17 �ǉ��J�n
            '*����ԍ� 000016 2003/08/22 �폜�J�n
            ''�Ǘ����擾�a�̃C���X�^���X�쐬
            'cURKanriJohoB = New Densan.Reams.UR.UR001BB.URKANRIJOHOBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            '*����ԍ� 000016 2003/08/22 �폜�I��
            '*����ԍ� 000008 2003/03/17 �ǉ��I��

            '* ����ԍ� 000029 2007/01/25 �ǉ��J�n
            ' UR�Ԓn�R�[�h�}�X�^�N���X�̃C���X�^���X����
            crBanchiCdMstB = New URBANCHICDMSTBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            '* ����ԍ� 000029 2007/01/25 �ǉ��I��

            '*����ԍ� 000007 2003/03/17 �ǉ��J�n
            '�p�����[�^�̃`�F�b�N
            Me.CheckColumnValue(cAtenaGetPara1)
            '*����ԍ� 000007 2003/03/17 �ǉ��I��

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            '�Z���ҏW�P��"1"���Z���ҏW�Q��"1"�̏ꍇ
            'If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu2 = "1" Then
            '    '���߂̎s���������擾����
            '    'm_cuUSSCityInfo.GetCityInfo(m_cfUFControlData)
            'End If
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '*����ԍ� 000008 2003/03/17 �ǉ��J�n
            '�Z���ҏW�P��"1"���Z���ҏW�R��""�̏ꍇ
            If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu3 = String.Empty Then
                '*����ԍ� 000016 2003/08/22 �C���J�n
                'cSofuJushoGyoseikuType = cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param

                cSofuJushoGyoseikuType = Me.GetSofuJushoGyoseikuType
                '*����ԍ� 000016 2003/08/22 �C���I��
                Select Case cSofuJushoGyoseikuType
                    Case SofuJushoGyoseikuType.Jusho_Banchi
                        strJushoHenshu3 = "1"
                        strJushoHenshu4 = ""
                    Case SofuJushoGyoseikuType.Jusho_Banchi_SP_Katagaki
                        strJushoHenshu3 = "1"
                        strJushoHenshu4 = "1"
                    Case SofuJushoGyoseikuType.Gyoseiku_SP_Banchi
                        strJushoHenshu3 = "5"
                        strJushoHenshu4 = ""
                    Case SofuJushoGyoseikuType.Gyoseiku_SP_Banchi_SP_Katagaki
                        strJushoHenshu3 = "5"
                        strJushoHenshu4 = "1"
                End Select
            Else
                strJushoHenshu3 = cAtenaGetPara1.p_strJushoHenshu3
                strJushoHenshu4 = cAtenaGetPara1.p_strJushoHenshu4
            End If
            '*����ԍ� 000008 2003/03/17 �ǉ��I��

            '�ҏW�����f�[�^���쐬����
            '*����ԍ� 000017 2003/10/09 �C���J�n
            'For Each csAtena1Row In csAtena1.Tables(ABAtena1Entity.TABLE_NAME).Rows
            'csDataNewRow = csAtena12.Tables(ABAtena1Entity.TABLE_NAME).NewRow

            For Each csAtena1Row In dsAtena1Table.Rows
                csDataNewRow = csDataTable.NewRow
                '*����ԍ� 000019 2003/11/19 �C���I��

                '*����ԍ� 000037 2008/11/17 �ǉ��J�n
                For Each csColumn In csDataNewRow.Table.Columns
                    csDataNewRow(csColumn) = String.Empty
                Next csColumn
                '*����ԍ� 000037 2008/11/17 �C���I��

                '���t��f�[�^����
                csSfskRow = Nothing
                '*����ԍ� 000002 2003/02/20 �C���J�n
                'For Each csDataRow In csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows
                '    '*����ԍ� 000001 2003/02/19 �C���J�n
                '    'If CType(csAtena1Row(ABAtena1Entity.JUMINCD), String).Trim = CType(csDataRow(ABSfskEntity.JUMINCD), String).Trim _
                '    '        And CType(csAtena1Row(ABAtena1Entity.GYOMUCD), String).Trim = CType(csDataRow(ABSfskEntity.GYOMUCD), String).Trim _
                '    '        And CType(csAtena1Row(ABAtena1Entity.GYOMUNAISHU_CD), String).Trim = CType(csDataRow(ABSfskEntity.GYOMUNAISHU_CD), String).Trim Then
                '    If CType(csAtena1Row(ABAtena1Entity.JUMINCD), String).Trim = CType(csDataRow(ABSfskEntity.JUMINCD), String).Trim _
                '               And CType(csAtena1Row(ABAtena1Entity.GYOMUCD), String).Trim = CType(csDataRow(ABSfskEntity.GYOMUCD), String).Trim _
                '               And CType(csAtena1Row(ABAtena1Entity.GYOMUNAISHU_CD), String).Trim = CType(csDataRow(ABSfskEntity.GYOMUNAISHU_CD), String).Trim Then
                '        '*����ԍ� 000001 2003/02/19 �C���I��
                '        csSfskRow = csDataRow
                '        Exit For
                '    End If
                'Next csDataRow

                ' ���t��f�[�^��0������1������
                If csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count > 0 Then
                    csSfskRow = csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows(0)
                End If
                '*����ԍ� 000002 2003/02/20 �C���I��

                '���t�悪���݂��Ȃ��ꍇ
                If csSfskRow Is Nothing Then

                    csDataNewRow.ItemArray = csAtena1Row.ItemArray

                    '�Z���R�[�h
                    csDataNewRow(ABAtena1Entity.JUMINCD) = csAtena1Row(ABAtena1Entity.JUMINCD)

                    '��[�敪�i�{�l�}�X�^�̑�[�敪��"00"�̏ꍇ"40"�A����ȊO��"50"�j
                    If CType(csAtena1Row(ABAtena1Entity.DAINOKB), String) = "00" Then
                        '��[�敪
                        csDataNewRow(ABAtena1Entity.DAINOKB) = "40"
                    Else
                        csDataNewRow(ABAtena1Entity.DAINOKB) = "50"
                    End If

                    '��[�敪����
                    csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty

                    '��[�敪��������
                    csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                        '*����ԍ� 000005  2003/02/25 �C���J�n
                        '�Ɩ��R�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUCD) = String.Empty

                        '�Ɩ�����ʃR�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = String.Empty
                        '*����ԍ� 000005  2003/02/25 �C���I��
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
                Else

                    '�Z���R�[�h
                    csDataNewRow(ABAtena1Entity.JUMINCD) = csAtena1Row(ABAtena1Entity.JUMINCD)

                    '��[�敪�i�{�l�}�X�^�̑�[�敪��"00"�̏ꍇ"40"�A����ȊO��"50"�j
                    If CType(csAtena1Row(ABAtena1Entity.DAINOKB), String) = "00" Then
                        '��[�敪
                        csDataNewRow(ABAtena1Entity.DAINOKB) = "40"
                    Else
                        csDataNewRow(ABAtena1Entity.DAINOKB) = "50"
                    End If

                    '��[�敪����
                    csDataNewRow(ABAtena1Entity.DAINOKBMEISHO) = String.Empty

                    '��[�敪��������
                    csDataNewRow(ABAtena1Entity.DAINOKBRYAKUMEISHO) = String.Empty

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                        '*����ԍ� 000003 2003/02/21 �C���J�n
                        ''�Ɩ��R�[�h
                        'csDataNewRow(ABAtena1Entity.GYOMUCD) = csAtena1Row(ABAtena1Entity.GYOMUCD)
                        ''�Ɩ�����ʃR�[�h
                        'csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = csAtena1Row(ABAtena1Entity.GYOMUNAISHU_CD)

                        '�Ɩ��R�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUCD) = csSfskRow(ABSfskEntity.GYOMUCD)

                        '�Ɩ�����ʃR�[�h
                        csDataNewRow(ABAtena1Entity.GYOMUNAISHU_CD) = csSfskRow(ABSfskEntity.GYOMUNAISHU_CD)
                        '*����ԍ� 000003 2003/02/21 �C���I��

                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                    '���s�����R�[�h
                    csDataNewRow(ABAtena1Entity.KYUSHICHOSONCD) = csAtena1Row(ABAtena1Entity.KYUSHICHOSONCD)

                    '���уR�[�h
                    csDataNewRow(ABAtena1Entity.STAICD) = csAtena1Row(ABAtena1Entity.STAICD)

                    '�����f�[�^�敪
                    csDataNewRow(ABAtena1Entity.ATENADATAKB) = csSfskRow(ABSfskEntity.SFSKDATAKB)

                    '�����f�[�^���
                    csDataNewRow(ABAtena1Entity.ATENADATASHU) = String.Empty

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                        '�ҏW���
                        csDataNewRow(ABAtena1Entity.HENSHUSHUBETSU) = String.Empty

                        '�ҏW��ʗ���
                        csDataNewRow(ABAtena1Entity.HENSHUSHUBETSURYAKU) = String.Empty

                        '�����p�J�i����
                        csDataNewRow(ABAtena1Entity.SEARCHKANASEIMEI) = String.Empty

                        '�����p�J�i��
                        csDataNewRow(ABAtena1Entity.SEARCHKANASEI) = String.Empty

                        '�����p�J�i��
                        csDataNewRow(ABAtena1Entity.SEARCHKANAMEI) = String.Empty

                        '�����p��������
                        csDataNewRow(ABAtena1Entity.SEARCHKANJIMEI) = String.Empty
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
                    '�ҏW�J�i����
                    strWork = CType(csSfskRow(ABSfskEntity.SFSKKANAMEISHO), String).Trim
                    csDataNewRow(ABAtena1Entity.HENSHUKANASHIMEI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_HENSHUKANAMEISHO)

                    '�ҏW��������
                    strWork = CType(csSfskRow(ABSfskEntity.SFSKKANJIMEISHO), String).Trim
                    csDataNewRow(ABAtena1Entity.HENSHUKANJISHIMEI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_HENSHUKANJIMEISHO)

                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        '�ҏW�J�i���́i�t���j
                        csDataNewRow(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL) = csSfskRow(ABSfskEntity.SFSKKANAMEISHO)

                        '�ҏW�������́i�t���j
                        csDataNewRow(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL) = csSfskRow(ABSfskEntity.SFSKKANJIMEISHO)
                    Else
                    End If

                    '���N����
                    csDataNewRow(ABAtena1Entity.UMAREYMD) = String.Empty

                    '���a��N����
                    csDataNewRow(ABAtena1Entity.UMAREWMD) = String.Empty

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                        '���\���N����
                        csDataNewRow(ABAtena1Entity.UMAREHYOJIWMD) = String.Empty

                        '���ؖ��N����
                        csDataNewRow(ABAtena1Entity.UMARESHOMEIWMD) = String.Empty

                        '���ʃR�[�h
                        csDataNewRow(ABAtena1Entity.SEIBETSUCD) = String.Empty

                        '����
                        csDataNewRow(ABAtena1Entity.SEIBETSU) = String.Empty

                        '�ҏW�����R�[�h
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARACD) = String.Empty

                        '�ҏW����
                        csDataNewRow(ABAtena1Entity.HENSHUZOKUGARA) = String.Empty

                        '�@�l��\�Җ�
                        csDataNewRow(ABAtena1Entity.HOJINDAIHYOUSHA) = String.Empty
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
                    '�l�@�l�敪
                    csDataNewRow(ABAtena1Entity.KJNHJNKB) = String.Empty
                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                        '�l�@�l�敪����
                        csDataNewRow(ABAtena1Entity.KJNHJNKBMEISHO) = String.Empty

                        '�Ǔ��ǊO�敪����
                        csDataNewRow(ABAtena1Entity.NAIGAIKBMEISHO) = m_cABKannaiKangaiKBB.GetKannaiKangai(CType(csSfskRow(ABSfskEntity.SFSKKANNAIKANGAIKB), String))
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                    '�Ǔ��ǊO�敪
                    csDataNewRow(ABAtena1Entity.KANNAIKANGAIKB) = csSfskRow(ABSfskEntity.SFSKKANNAIKANGAIKB)

                    '�X�֔ԍ�
                    csDataNewRow(ABAtena1Entity.YUBINNO) = csSfskRow(ABSfskEntity.SFSKYUBINNO)

                    '�Z���R�[�h
                    csDataNewRow(ABAtena1Entity.JUSHOCD) = csSfskRow(ABSfskEntity.SFSKZJUSHOCD)

                    '�Z��
                    csDataNewRow(ABAtena1Entity.JUSHO) = csSfskRow(ABSfskEntity.SFSKJUSHO)

                    '�ҏW�Z����
                    If cAtenaGetPara1.p_strJushoHenshu1 = String.Empty Then
                        csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = String.Empty
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = String.Empty
                        Else
                        End If

                    ElseIf cAtenaGetPara1.p_strJushoHenshu1 = "1" Then
                        strHenshuJusho = String.Empty
                        If cAtenaGetPara1.p_strJushoHenshu2 = "1" Then

                            '�Ǔ��̂ݎs��������t������
                            If CType(csSfskRow(ABSfskEntity.SFSKKANNAIKANGAIKB), String) = "1" Then
                                strHenshuJusho += m_cuUSSCityInfo.p_strShichosonmei(0)
                            End If
                        End If
                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'Select Case cAtenaGetPara1.p_strJushoHenshu3
                        Select Case strJushoHenshu3
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '* ����ԍ� 000028 2007/01/15 �C���J�n
                            Case "1", "6"   '�Z���{�Ԓn
                                'Case "1"    '�Z���{�Ԓn
                                '* ����ԍ� 000028 2007/01/15 �C���I��
                                strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                                + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                            Case "2"    '�s����{�Ԓn
                                '*����ԍ� 000009 2003/03/17 �C���J�n
                                'strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�Z���{�Ԓn
                                    strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                Else
                                    '�s����{�Ԓn
                                    strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                End If
                                '*����ԍ� 000009 2003/03/17 �C���I��
                            Case "3"    '�Z���{�i�s����j�{�Ԓn
                                '*����ԍ� 000004  2003/02/25 �C���J�n
                                'strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                '                + CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd

                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                Else
                                    strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                                    + "�i" _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                                    + "�j" _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                End If
                                '*����ԍ� 000004  2003/02/25 �C���I��
                            Case "4"    '�s����{�i�Z���j�{�Ԓn
                                '*����ԍ� 000004  2003/02/25 �C���J�n
                                'strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                '                + CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                '                + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd

                                '�Z�������݂��Ȃ��ꍇ
                                If (CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd = String.Empty) Then
                                    strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                    '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                                    '�s���於�����݂��Ȃ��ꍇ
                                ElseIf (CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                    '*����ԍ� 000009 2003/03/17 �ǉ��I��
                                Else
                                    strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                                    + "�i" _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                                    + "�j" _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                End If
                                '*����ԍ� 000004 2003/02/25 �C���I��
                                '*����ԍ� 000009 2003/03/17 �ǉ��J�n
                            Case "5"    '�s����{���{�Ԓn
                                '�s���於�����݂��Ȃ��ꍇ
                                If (CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd = String.Empty) Then
                                    '�Z���{�Ԓn
                                    strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKJUSHO), String).TrimEnd _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                Else
                                    '�s����{���{�Ԓn
                                    strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd _
                                                    + "�@" _
                                                    + CType(csSfskRow(ABSfskEntity.SFSKBANCHI), String).TrimEnd
                                End If
                                '*����ԍ� 000009 2003/03/17 �ǉ��I��
                        End Select
                        '*����ԍ� 000008 2003/03/17 �C���J�n
                        'If cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                        '* ����ԍ� 000028 2007/01/15 �C���J�n
                        If (strJushoHenshu4 = "1") _
                            AndAlso (CType(csSfskRow(ABSfskEntity.SFSKKATAGAKI), String).Trim <> String.Empty) Then
                            'If strJushoHenshu4 = "1" Then
                            '* ����ԍ� 000028 2007/01/15 �C���I��
                            '*����ԍ� 000008 2003/03/17 �C���I��
                            '*����ԍ� 000004 2003/02/25 �C���J�n
                            'strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKKATAGAKI), String).TrimEnd

                            strHenshuJusho += "�@" + CType(csSfskRow(ABSfskEntity.SFSKKATAGAKI), String).TrimEnd
                            '*����ԍ� 000004 2003/02/25 �C���I��
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��J�n
                        ' �Z���ҏW�R�p�����[�^���U�A���s���於������Ƃ��́A�ҏW�Z���Ɂi�s����j��ǉ�����
                        If (strJushoHenshu3 = "6") _
                                AndAlso (CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).Trim <> String.Empty) Then
                            strHenshuJusho += "�i"
                            strHenshuJusho += CType(csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI), String).TrimEnd
                            strHenshuJusho += "�j"
                        End If
                        '* ����ԍ� 000028 2007/01/15 �ǉ��I��
                        '* ����ԍ� 000032 2007/07/09 �C���J�n
                        If strHenshuJusho.RLength >= 160 Then
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.RSubstring(0, 160)
                            'If strHenshuJusho.Length >= 80 Then
                            '    csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho.Substring(0, 80)
                            '* ����ԍ� 000032 2007/07/09 �C���I��
                        Else
                            csDataNewRow(ABAtena1Entity.HENSHUJUSHO) = strHenshuJusho
                        End If
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�ҏW�Z�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.HENSHUJUSHO_FULL) = strHenshuJusho
                        Else
                        End If
                    End If

                    '* ����ԍ� 000029 2007/01/25 �C���J�n
                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        '�Ԓn�R�[�h�P
                        csDataNewRow(ABAtena1Entity.BANCHICD1) = csSfskRow(ABSfskHyojunEntity.SFSKBANCHICD1)

                        '�Ԓn�R�[�h�Q
                        csDataNewRow(ABAtena1Entity.BANCHICD2) = csSfskRow(ABSfskHyojunEntity.SFSKBANCHICD2)

                        '�Ԓn�R�[�h�R
                        csDataNewRow(ABAtena1Entity.BANCHICD3) = csSfskRow(ABSfskHyojunEntity.SFSKBANCHICD3)
                    ElseIf (IsNothing(csSfskRow(ABSfskEntity.SFSKBANCHI)) = False _
                        AndAlso CStr(csSfskRow(ABSfskEntity.SFSKBANCHI)).Trim <> String.Empty) Then
                        ' �Ԓn��񂪂���ꍇ�́AUR�̃��\�b�h����Ԓn���擾����
                        ' �Ԓn�R�[�h�擾���\�b�h���Ăяo��
                        strBanchiCD = crBanchiCdMstB.GetBanchiCd(CStr(csSfskRow(ABSfskEntity.SFSKBANCHI)), strMotoBanchiCD, True)

                        ' �擾�����Ԓn�R�[�h�z���Nothing�̍��ڂ�����ꍇ��String.Empty���Z�b�g����
                        For intLoop = 0 To strBanchiCD.Length - 1
                            If (IsNothing(strBanchiCD(intLoop))) Then
                                strBanchiCD(intLoop) = String.Empty
                            End If
                        Next

                        '�Ԓn�R�[�h�P
                        csDataNewRow(ABAtena1Entity.BANCHICD1) = strBanchiCD(0)

                        '�Ԓn�R�[�h�Q
                        csDataNewRow(ABAtena1Entity.BANCHICD2) = strBanchiCD(1)

                        '�Ԓn�R�[�h�R
                        csDataNewRow(ABAtena1Entity.BANCHICD3) = strBanchiCD(2)
                    Else
                        '�Ԓn�R�[�h�P
                        csDataNewRow(ABAtena1Entity.BANCHICD1) = String.Empty

                        '�Ԓn�R�[�h�Q
                        csDataNewRow(ABAtena1Entity.BANCHICD2) = String.Empty

                        '�Ԓn�R�[�h�R
                        csDataNewRow(ABAtena1Entity.BANCHICD3) = String.Empty
                    End If
                    '* ����ԍ� 000029 2007/01/25 �C���I��

                    '�Ԓn
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" Then
                        '�Z���ҏW����̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.BANCHI) = String.Empty
                    Else
                        csDataNewRow(ABAtena1Entity.BANCHI) = csSfskRow(ABSfskEntity.SFSKBANCHI)
                    End If

                    '�����t���O
                    csDataNewRow(ABAtena1Entity.KATAGAKIFG) = String.Empty

                    '�����R�[�h
                    csDataNewRow(ABAtena1Entity.KATAGAKICD) = String.Empty

                    '����
                    If cAtenaGetPara1.p_strJushoHenshu1 = "1" And cAtenaGetPara1.p_strJushoHenshu4 = "1" Then
                        '�����t������̏ꍇ�́ANull
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = String.Empty
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = String.Empty
                        Else
                        End If
                    Else
                        strWork = CType(csSfskRow(ABSfskEntity.SFSKKATAGAKI), String).Trim
                        csDataNewRow(ABAtena1Entity.KATAGAKI) = ABStrXClass.Left(strWork, ABAtenaGetConstClass.KETA_KATAGAKI)
                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            '�����i�t���j
                            csDataNewRow(ABAtena1HyojunEntity.KATAGAKI_FULL) = csSfskRow(ABSfskEntity.SFSKKATAGAKI)
                        Else
                        End If
                    End If

                    '�A����P
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI1) = csSfskRow(ABSfskEntity.SFSKRENRAKUSAKI1)

                    '�A����Q
                    csDataNewRow(ABAtena1Entity.RENRAKUSAKI2) = csSfskRow(ABSfskEntity.SFSKRENRAKUSAKI2)

                    '�s����R�[�h
                    csDataNewRow(ABAtena1Entity.GYOSEIKUCD) = csSfskRow(ABSfskEntity.SFSKGYOSEIKUCD)

                    '�s���於
                    csDataNewRow(ABAtena1Entity.GYOSEIKUMEI) = csSfskRow(ABSfskEntity.SFSKGYOSEIKUMEI)

                    '�n��R�[�h�P
                    csDataNewRow(ABAtena1Entity.CHIKUCD1) = csSfskRow(ABSfskEntity.SFSKCHIKUCD1)

                    '�n��P
                    csDataNewRow(ABAtena1Entity.CHIKUMEI1) = csSfskRow(ABSfskEntity.SFSKCHIKUMEI1)

                    '�n��R�[�h�Q
                    csDataNewRow(ABAtena1Entity.CHIKUCD2) = csSfskRow(ABSfskEntity.SFSKCHIKUCD2)

                    '�n��Q
                    csDataNewRow(ABAtena1Entity.CHIKUMEI2) = csSfskRow(ABSfskEntity.SFSKCHIKUMEI2)

                    '�n��R�[�h�R
                    csDataNewRow(ABAtena1Entity.CHIKUCD3) = csSfskRow(ABSfskEntity.SFSKCHIKUCD3)

                    '�n��R
                    csDataNewRow(ABAtena1Entity.CHIKUMEI3) = csSfskRow(ABSfskEntity.SFSKCHIKUMEI3)

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then

                        '�o�^�ٓ��N����
                        csDataNewRow(ABAtena1Entity.TOROKUIDOYMD) = csAtena1Row(ABAtena1Entity.TOROKUIDOYMD)

                        '�o�^���R�R�[�h
                        csDataNewRow(ABAtena1Entity.TOROKUJIYUCD) = csAtena1Row(ABAtena1Entity.TOROKUJIYUCD)

                        '�o�^���R
                        csDataNewRow(ABAtena1Entity.TOROKUJIYU) = csAtena1Row(ABAtena1Entity.TOROKUJIYU)

                        '�����ٓ��N����
                        csDataNewRow(ABAtena1Entity.SHOJOIDOYMD) = csAtena1Row(ABAtena1Entity.SHOJOIDOYMD)

                        '�������R�R�[�h
                        csDataNewRow(ABAtena1Entity.SHOJOJIYUCD) = csAtena1Row(ABAtena1Entity.SHOJOJIYUCD)

                        '�������R����
                        csDataNewRow(ABAtena1Entity.SHOJOJIYU) = csAtena1Row(ABAtena1Entity.SHOJOJIYU)

                        '�ҏW���ю�Z���R�[�h
                        csDataNewRow(ABAtena1Entity.HENSHUNUSHIJUMINCD) = csAtena1Row(ABAtena1Entity.HENSHUNUSHIJUMINCD)
                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                    '�ҏW�J�i���ю喼
                    csDataNewRow(ABAtena1Entity.HENSHUKANANUSHIMEI) = csAtena1Row(ABAtena1Entity.HENSHUKANANUSHIMEI)

                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
                    If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                        '�ҏW�������ю喼
                        csDataNewRow(ABAtena1Entity.HENSHUNUSHIMEI) = csAtena1Row(ABAtena1Entity.HENSHUNUSHIMEI)

                        '�\�����i��Q�Z���[�\����������ꍇ�́A��Q�Z���[�\�����j
                        csDataNewRow(ABAtena1Entity.HYOJIJUN) = csAtena1Row(ABAtena1Entity.HYOJIJUN)

                        '*����ԍ� 000012 2003/04/18 �ǉ��J�n
                        ' �����R�[�h
                        csDataNewRow(ABAtena1Entity.ZOKUGARACD) = String.Empty
                        ' ����
                        csDataNewRow(ABAtena1Entity.ZOKUGARA) = String.Empty

                        ' �J�i���̂Q
                        csDataNewRow(ABAtena1Entity.KANAMEISHO2) = String.Empty
                        ' �������̂Q
                        csDataNewRow(ABAtena1Entity.KANJIMEISHO2) = String.Empty

                        ' �Дԍ�
                        csDataNewRow(ABAtena1Entity.SEKINO) = String.Empty
                        '*����ԍ� 000012 2003/04/18 �ǉ��I��


                        '*����ԍ� 000030 2007/04/28 �ǉ��J�n
                        '���p�T�u���[�`���擾����
                        If m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo Then
                            ' �A����Ɩ��R�[�h
                            csDataNewRow(ABNenkinAtenaEntity.RENRAKUSAKI_GYOMUCD) = String.Empty
                            ' ����
                            csDataNewRow(ABNenkinAtenaEntity.KYUSEI) = String.Empty
                            ' �Z��ٓ��N����
                            csDataNewRow(ABNenkinAtenaEntity.JUTEIIDOYMD) = String.Empty
                            ' �Z�莖�R
                            csDataNewRow(ABNenkinAtenaEntity.JUTEIJIYU) = String.Empty
                            ' �{�БS���Z���R�[�h
                            csDataNewRow(ABNenkinAtenaEntity.HON_ZJUSHOCD) = String.Empty
                            ' �]���O�Z���X�֔ԍ�
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_YUBINNO) = String.Empty
                            ' �]���O�Z���S���Z���R�[�h
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_ZJUSHOCD) = String.Empty
                            ' �]���O�Z���Z��
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_JUSHO) = String.Empty
                            ' �]���O�Z���Ԓn
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_BANCHI) = String.Empty
                            ' �]���O�Z������
                            csDataNewRow(ABNenkinAtenaEntity.TENUMAEJ_KATAGAKI) = String.Empty
                            ' �]�o�\��X�֔ԍ�
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIYUBINNO) = String.Empty
                            ' �]�o�\��S���Z���R�[�h
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = String.Empty
                            ' �]�o�\��ٓ��N����
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIIDOYMD) = String.Empty
                            ' �]�o�\��Z��
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIJUSHO) = String.Empty
                            ' �]�o�\��Ԓn
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIBANCHI) = String.Empty
                            ' �]�o�\�����
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = String.Empty
                            ' �]�o�\�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = String.Empty
                            ' �]�o�m��X�֔ԍ�
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIYUBINNO) = String.Empty
                            ' �]�o�m��S���Z���R�[�h
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = String.Empty
                            ' �]�o�m��ٓ��N����
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIIDOYMD) = String.Empty
                            ' �]�o�m��ʒm�N����
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = String.Empty
                            ' �]�o�m��Z��
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIJUSHO) = String.Empty
                            ' �]�o�m��Ԓn
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIBANCHI) = String.Empty
                            ' �]�o�m�����
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTIKATAGAKI) = String.Empty
                            ' �]�o�m�萢�ю喼
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = String.Empty
                            ' �ҏW�O�Ԓn
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEBANCHI) = String.Empty
                            ' �ҏW�O����
                            csDataNewRow(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI) = String.Empty
                            ' �����͏o�N����
                            csDataNewRow(ABNenkinAtenaEntity.SHOJOTDKDYMD) = String.Empty
                            ' ���ߎ��R�R�[�h
                            csDataNewRow(ABNenkinAtenaEntity.CKINJIYUCD) = String.Empty
                            ' ���ЃR�[�h
                            csDataNewRow(ABNenkinAtenaEntity.KOKUSEKICD) = String.Empty
                            ' �o�^�͏o�N����
                            csDataNewRow(ABNenkinAtenaEntity.TOROKUTDKDYMD) = String.Empty
                            ' �Z��͏o�N����
                            csDataNewRow(ABNenkinAtenaEntity.JUTEITDKDYMD) = String.Empty
                            ' �]�o�����R
                            csDataNewRow(ABNenkinAtenaEntity.TENSHUTSUNYURIYU) = String.Empty
                            ' �s�����R�[�h
                            csDataNewRow(ABNenkinAtenaEntity.SHICHOSONCD) = String.Empty
                            ' ���߈ٓ��N����
                            csDataNewRow(ABNenkinAtenaEntity.CKINIDOYMD) = String.Empty
                            ' �X�V����
                            csDataNewRow(ABNenkinAtenaEntity.KOSHINNICHIJI) = csSfskRow(ABSfskEntity.KOSHINNICHIJI)
                            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                ' �]���O�Z�������i�t���j
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KATAGAKI_FULL) = String.Empty
                                ' �]�o�\������i�t���j
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL) = String.Empty
                                ' �]�o�m������i�t���j
                                csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL) = String.Empty
                                ' �ҏW�O�����i�t���j
                                csDataNewRow(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL) = String.Empty
                            Else
                            End If
                        End If
                        '*����ԍ� 000030 2007/04/28 �ǉ��I��

                    End If
                    '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�

                    If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        ' ���ю厁���D��敪
                        csDataNewRow(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB) = csAtena1Row(ABAtenaFZYHyojunEntity.STAINUSSHIMEIYUSENKB)
                        ' �����D�捀��
                        csDataNewRow(ABAtena1HyojunEntity.SHIMEIYUSENKB) = csAtena1Row(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB)
                        ' ����
                        csDataNewRow(ABAtena1HyojunEntity.KANJIKYUUJI) = String.Empty
                        ' �J�i����
                        csDataNewRow(ABAtena1HyojunEntity.KANAKYUUJI) = String.Empty
                        ' �����t���K�i�m�F�t���O
                        csDataNewRow(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG) = String.Empty
                        ' �����t���K�i�m�F�t���O
                        csDataNewRow(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG) = String.Empty
                        ' �ʏ̃t���K�i�m�F�t���O
                        csDataNewRow(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG) = String.Empty
                        ' ���N�����s�ڃp�^�[��
                        csDataNewRow(ABAtena1HyojunEntity.UMAREBIFUSHOPTN) = String.Empty
                        ' �s�ڐ��N����
                        csDataNewRow(ABAtena1HyojunEntity.FUSHOUMAREBI) = String.Empty
                        ' �L�ڎ��R
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD) = csAtena1Row(ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD)
                        ' �L�ڔN����
                        csDataNewRow(ABAtena1HyojunEntity.KISAIYMD) = csAtena1Row(ABAtenaHyojunEntity.KISAIYMD)
                        ' �������R
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD) = csAtena1Row(ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD)
                        ' �����ٓ��a��N����
                        csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOWMD) = csAtena1Row(ABAtenaHyojunEntity.SHOJOIDOWMD)
                        ' �����ٓ����s�ڃp�^�[��
                        csDataNewRow(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN) = csAtena1Row(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN)
                        ' �s�ڏ����ٓ���
                        csDataNewRow(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI) = csAtena1Row(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI)
                        ' ���߈ٓ��a��N����
                        csDataNewRow(ABAtena1HyojunEntity.CKINIDOWMD) = csAtena1Row(ABAtenaHyojunEntity.CKINIDOWMD)
                        ' ���߈ٓ����s�ڃp�^�[��
                        csDataNewRow(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN) = csAtena1Row(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN)
                        ' �s�ڒ��߈ٓ���
                        csDataNewRow(ABAtena1HyojunEntity.FUSHOCKINIDOBI) = csAtena1Row(ABAtenaHyojunEntity.FUSHOCKINIDOBI)
                        ' ������̐��ю�
                        csDataNewRow(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI) = csAtena1Row(ABAtenaHyojunEntity.JIJITSUSTAINUSMEI)
                        ' �Z��_�s�撬���R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSONCD) = csSfskRow(ABSfskHyojunEntity.SFSKSHIKUCHOSONCD)
                        ' �Z��_�����R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZACD) = csSfskRow(ABSfskHyojunEntity.SFSKMACHIAZACD)
                        ' �Z��_�s���{��
                        csDataNewRow(ABAtena1HyojunEntity.TODOFUKEN) = csSfskRow(ABSfskHyojunEntity.SFSKTODOFUKEN)
                        ' �Z��_�s��S������
                        csDataNewRow(ABAtena1HyojunEntity.SHIKUCHOSON) = csSfskRow(ABSfskHyojunEntity.SFSKSHIKUCHOSON)
                        ' �Z��_����
                        csDataNewRow(ABAtena1HyojunEntity.MACHIAZA) = csSfskRow(ABSfskHyojunEntity.SFSKMACHIAZA)
                        If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                            ' �{��_�s�撬���R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD) = String.Empty
                            ' �{��_�����R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZACD) = String.Empty
                            ' �{��_�s���{��
                            csDataNewRow(ABAtena1HyojunEntity.HON_TODOFUKEN) = String.Empty
                            ' �{��_�s��S������
                            csDataNewRow(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON) = String.Empty
                            ' �{��_����
                            csDataNewRow(ABAtena1HyojunEntity.HON_MACHIAZA) = String.Empty
                        End If
                        If (m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo) AndAlso
                           (strGyomuMei <> NENKIN) AndAlso (strGyomuMei <> NENKIN_2) Then
                            ' ���ЃR�[�h
                            csDataNewRow(ABAtena1HyojunEntity.KOKUSEKICD) = String.Empty
                        End If
                        If (strGyomuMei = NENKIN Or strGyomuMei = NENKIN_2) Then
                            ' �]���O�Z��_�s�撬���R�[�h
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = String.Empty
                            ' �]���O�����R�[�h
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZACD) = String.Empty
                            ' �]���O�Z��_�s���{��
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_TODOFUKEN) = String.Empty
                            ' �]���O�Z��_�s��S������
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON) = String.Empty
                            ' �]���O�Z��_����
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZA) = String.Empty
                            ' �]���O�Z��_�����R�[�h
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD) = String.Empty
                            ' �]���O�Z��_����
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKI) = String.Empty
                            ' �]���O�Z��_���O�Z��
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = String.Empty
                            ' �]�o�m��_�s�撬���R�[�h
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = String.Empty
                            ' �]�o�m�蒬���R�[�h
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD) = String.Empty
                            ' �]�o�m��_�s���{��
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN) = String.Empty
                            ' �]�o�m��_�s��S������
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = String.Empty
                            ' �]�o�m��_����
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA) = String.Empty
                            ' �]�o�\��_�s�撬���R�[�h
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = String.Empty
                            ' �]�o�\�蒬���R�[�h
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = String.Empty
                            ' �]�o�\��_�s���{��
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = String.Empty
                            ' �]�o�\��_�s��S������
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = String.Empty
                            ' �]�o�\��_����
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = String.Empty
                            ' �]�o�\��_�����R�[�h
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = String.Empty
                            ' �]�o�\��_������
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = String.Empty
                            ' �]�o�\��_���O�Z��
                            csDataNewRow(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = String.Empty
                        End If
                        If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo) Then
                            ' �]���O�Z��_�s�撬���R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = String.Empty
                            ' �]���O�����R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZACD) = String.Empty
                            ' �]���O�Z��_�s���{��
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_TODOFUKEN) = String.Empty
                            ' �]���O�Z��_�s��S������
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSON) = String.Empty
                            ' �]���O�Z��_����
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZA) = String.Empty
                            ' �]���O�Z��_�����R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKICD) = String.Empty
                            ' �]���O�Z��_����
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKI) = String.Empty
                            ' �]���O�Z��_���O�Z��
                            csDataNewRow(ABAtena1HyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = String.Empty
                            ' �]�o�m��_�s�撬���R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = String.Empty
                            ' �]�o�m�蒬���R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZACD) = String.Empty
                            ' �]�o�m��_�s���{��
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTITODOFUKEN) = String.Empty
                            ' �]�o�m��_�s��S������
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = String.Empty
                            ' �]�o�m��_����
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZA) = String.Empty
                            ' �]�o�\��_�s�撬���R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = String.Empty
                            ' �]�o�\�蒬���R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = String.Empty
                            ' �]�o�\��_�s���{��
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEITODOFUKEN) = String.Empty
                            ' �]�o�\��_�s��S������
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = String.Empty
                            ' �]�o�\��_����
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZA) = String.Empty
                            ' �]�o�\��_�����R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = String.Empty
                            ' �]�o�\��_������
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = String.Empty
                            ' �]�o�\��_���O�Z��
                            csDataNewRow(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = String.Empty
                        Else
                        End If
                        ' �@��30��46����47�敪
                        csDataNewRow(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB) = csAtena1Row(ABAtenaFZYHyojunEntity.HODAI30JO46MATAHA47KB)
                        ' �ݗ��J�[�h���ԍ��敪
                        csDataNewRow(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN) = csAtena1Row(ABAtenaFZYHyojunEntity.ZAIRYUCARDNOKBN)
                        ' �Z���n�␳�R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.JUKYOCHIHOSEICD) = csAtena1Row(ABAtenaFZYHyojunEntity.JUKYOCHIHOSEICD)
                        ' ���ߓ͏o�ʒm�敪
                        csDataNewRow(ABAtena1HyojunEntity.CKINTDKDTUCIKB) = String.Empty
                        ' �Ŕԍ�
                        csDataNewRow(ABAtena1HyojunEntity.HANNO) = String.Empty
                        ' �����N����
                        csDataNewRow(ABAtena1HyojunEntity.KAISEIYMD) = String.Empty
                        ' �ٓ��敪
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOKB) = String.Empty
                        ' ���͏ꏊ�R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHOCD) = String.Empty
                        ' ���͏ꏊ�\�L
                        csDataNewRow(ABAtena1HyojunEntity.NYURYOKUBASHO) = String.Empty
                        If (strGyomuMei = KOBETSU) Then
                            ' ���_��ی��ҊY���L��
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB) = String.Empty
                            ' ����_��ی��ҊY���L��
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB) = String.Empty
                            ' �N��_��ی��ҊY���L��
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB) = String.Empty
                            ' �N��_��ʕύX�N����
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD) = String.Empty
                            ' �I��_��ԋ敪
                            csDataNewRow(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN) = String.Empty
                            If (m_strKobetsuShutokuKB = "1") Then
                                ' �������_��ی��ҊY���L��
                                csDataNewRow(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB) = String.Empty
                            End If
                        End If
                        ' �A����敪�i�A����j
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIKB) = String.Empty
                        ' �A���於
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKIMEI) = String.Empty
                        ' �A����1�i�A����j
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI) = String.Empty
                        ' �A����2�i�A����j
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI) = String.Empty
                        ' �A����3�i�A����j
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI) = String.Empty
                        ' �A������1
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1) = String.Empty
                        ' �A������2
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2) = String.Empty
                        ' �A������3
                        csDataNewRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3) = String.Empty
                        If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) Then
                            ' �s���Z�敪
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUKB) = String.Empty
                            ' �s���Z�������Z��_�X�֔ԍ�
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO) = String.Empty
                            ' �s���Z�������Z��_�s�撬���R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD) = String.Empty
                            ' �s���Z�������Z��_�����R�[�h
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD) = String.Empty
                            ' �s���Z�������Z��_�s���{��
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN) = String.Empty
                            ' �s���Z�������Z��_�s��S������
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON) = String.Empty
                            ' �s���Z�������Z��_����
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA) = String.Empty
                            ' �s���Z�������Z��_�Ԓn���\�L
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI) = String.Empty
                            ' �s���Z�������Z��_����
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI) = String.Empty
                            ' �s���Z�������Z��_����_�t���K�i
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI) = String.Empty
                            ' �s���Z���i�Ώێҋ敪�j
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN) = String.Empty
                            ' �s���Z���i�ΏێҎ����j
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI) = String.Empty
                            ' �s���Z���i���N�����j
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD) = String.Empty
                            ' �s���Z���i���ʁj
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU) = String.Empty
                            ' ���Z�s���N����
                            csDataNewRow(ABAtena1HyojunEntity.KYOJUFUMEI_YMD) = String.Empty
                            ' �s���Z���i���l�j
                            csDataNewRow(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO) = String.Empty
                        Else
                        End If
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            ' �ԍ��@�X�V�敪
                            csDataNewRow(ABAtena1HyojunEntity.BANGOHOKOSHINKB) = csAtena1Row(ABMyNumberHyojunEntity.BANGOHOKOSHINKB)
                        End If
                        If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1) Then
                            ' �V���A���ԍ�
                            csDataNewRow(ABAtena1HyojunEntity.SERIALNO) = String.Empty
                        End If
                        ' �W�������ٓ����R�R�[�h
                        csDataNewRow(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD) = String.Empty
                        If (strGyomuMei <> NENKIN AndAlso strGyomuMei <> NENKIN_2) Then
                            ' �A����敪�i���t��j
                            csDataNewRow(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB) = csSfskRow(ABSfskHyojunEntity.SFSKRENRAKUSAKIKB)
                            ' ���t��敪
                            csDataNewRow(ABAtena1HyojunEntity.SFSKKBN) = csSfskRow(ABSfskHyojunEntity.SFSKKBN)
                        Else
                        End If
                        ' �Z���敪
                        csDataNewRow(ABAtena1HyojunEntity.JUMINKBN) = String.Empty
                        ' �Z�����
                        csDataNewRow(ABAtena1HyojunEntity.JUMINSHUBETSU) = String.Empty
                        ' �Z�����
                        csDataNewRow(ABAtena1HyojunEntity.JUMINJOTAI) = String.Empty
                        ' �Ԓn�}�Ԑ��l
                        csDataNewRow(ABAtena1HyojunEntity.BANCHIEDABANSUCHI) = String.Empty
                    Else
                        ' noop
                    End If

                End If

                '*����ԍ� 000046 2011/11/07 �ǉ��J�n
                '�Z��@��������
                If (m_strJukiHokaiseiKB_Param = "1") Then
                    '�Z���[��ԋ敪
                    csDataNewRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN) = csAtena1Row(ABAtenaFZYEntity.JUMINHYOJOTAIKBN)
                    '�Z���n�͏o�L���t���O
                    csDataNewRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG) = csAtena1Row(ABAtenaFZYEntity.JUKYOCHITODOKEFLG)
                    '�{����
                    csDataNewRow(ABAtenaFZYEntity.HONGOKUMEI) = csAtena1Row(ABAtenaFZYEntity.HONGOKUMEI)
                    '�J�i�{����
                    csDataNewRow(ABAtenaFZYEntity.KANAHONGOKUMEI) = csAtena1Row(ABAtenaFZYEntity.KANAHONGOKUMEI)
                    '���L��
                    csDataNewRow(ABAtenaFZYEntity.KANJIHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.KANJIHEIKIMEI)
                    '�J�i���L��
                    csDataNewRow(ABAtenaFZYEntity.KANAHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.KANAHEIKIMEI)
                    '�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.KANJITSUSHOMEI) = csAtena1Row(ABAtenaFZYEntity.KANJITSUSHOMEI)
                    '�J�i�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.KANATSUSHOMEI) = csAtena1Row(ABAtenaFZYEntity.KANATSUSHOMEI)
                    '�J�^�J�i���L��
                    csDataNewRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.KATAKANAHEIKIMEI)
                    '���N�����s�ڋ敪
                    csDataNewRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = csAtena1Row(ABAtenaFZYEntity.UMAREFUSHOKBN)
                    '�ʏ̖��o�^�i�ύX�j�N����
                    csDataNewRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD) = csAtena1Row(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD)
                    '�ݗ����ԃR�[�h
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANCD) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUKIKANCD)
                    '�ݗ����Ԗ���
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO)
                    '�������ݗ��҂ł���|���̃R�[�h
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHACD) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUSHACD)
                    '�������ݗ��҂ł���|��
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUSHAMEISHO)
                    '�ݗ��J�[�h���ԍ�
                    csDataNewRow(ABAtenaFZYEntity.ZAIRYUCARDNO) = csAtena1Row(ABAtenaFZYEntity.ZAIRYUCARDNO)
                    '���ʉi�Z�ҏؖ�����t�N����
                    csDataNewRow(ABAtenaFZYEntity.KOFUYMD) = csAtena1Row(ABAtenaFZYEntity.KOFUYMD)
                    '���ʉi�Z�ҏؖ�����t�\����ԊJ�n��
                    csDataNewRow(ABAtenaFZYEntity.KOFUYOTEISTYMD) = csAtena1Row(ABAtenaFZYEntity.KOFUYOTEISTYMD)
                    '����i�Z�ҏؖ�����t�\����ԏI����
                    csDataNewRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD) = csAtena1Row(ABAtenaFZYEntity.KOFUYOTEIEDYMD)
                    '�Z��Ώێҁi��30��45��Y���j�����ٓ��N����
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
                    '�Z��Ώێҁi��30��45��Y���j�������R�R�[�h
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
                    '�Z��Ώێҁi��30��45��Y���j�������R
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU)
                    '�Z��Ώێҁi��30��45��Y���j�����͏o�N����
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
                    '�Z��Ώێҁi��30��45��Y���j�����͏o�ʒm�敪
                    csDataNewRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB) = csAtena1Row(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
                    '�O���l���ю喼
                    csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSMEI) = csAtena1Row(ABAtenaFZYEntity.FRNSTAINUSMEI)
                    '�O���l���ю�J�i��
                    csDataNewRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI) = csAtena1Row(ABAtenaFZYEntity.FRNSTAINUSKANAMEI)
                    '���ю啹�L��
                    csDataNewRow(ABAtenaFZYEntity.STAINUSHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.STAINUSHEIKIMEI)
                    '���ю�J�i���L��
                    csDataNewRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI) = csAtena1Row(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI)
                    '���ю�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI) = csAtena1Row(ABAtenaFZYEntity.STAINUSTSUSHOMEI)
                    '���ю�J�i�ʏ̖�
                    csDataNewRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI) = csAtena1Row(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI)
                Else
                    '�����Ȃ�
                End If
                '*����ԍ� 000046 2011/11/07 �ǉ��I��

                '*����ԍ� 000048 2014/04/28 �ǉ��J�n
                ' ���ʔԍ�����
                If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                    ' �󔒏��������l��ݒ肷��B
                    csDataNewRow(ABMyNumberEntity.MYNUMBER) = csAtena1Row(ABMyNumberEntity.MYNUMBER).ToString.Trim
                Else
                    ' noop
                End If
                '*����ԍ� 000048 2014/04/28 �ǉ��I��

                '*����ԍ� 000019 2003/11/19 �C���J�n
                ''���R�[�h�̒ǉ�
                'csAtena12.Tables(ABAtena1Entity.TABLE_NAME).Rows.Add(csDataNewRow)

                '���R�[�h�̒ǉ�
                csDataTable.Rows.Add(csDataNewRow)
                '*����ԍ� 000019 2003/11/19 �C���I��


            Next csAtena1Row

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exException As UFAppException

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �X���[����
            Throw exException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csAtena12

    End Function
#End Region

#Region " �p�����[�^�[�`�F�b�N(CheckColumnValue) "
    '************************************************************************************************
    '* ���\�b�h��     �p�����[�^�[�`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal cAtenaGetPara1 As ABAtenaGetPara1)
    '* 
    '* �@�\�@�@    �@�@�����擾�p�����[�^�̃`�F�b�N���s�Ȃ�
    '* 
    '* ����           cAtenaGetPara1 As ABAtenaGetPara1 : �����擾�p�����[�^
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass)

        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cABCommon As ABCommonClass                      '�������ʃN���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��

        Try

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ''�G���[�����N���X�̃C���X�^���X�쐬
            ''*����ԍ� 000010  2003/03/27 �C���J�n
            ''cfErrorClass = New UFErrorClass(m_cfUFControlData.m_strBusinessId)
            'cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            ''*����ԍ� 000010  2003/03/27 �C���I��

            '�������ʃN���X�̃C���X�^���X�쐬
            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            'm_cABCommon = New ABCommonClass()
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '*����ԍ� 000007 2003/03/17 �폜�J�n
            ''�Z��E�Z�o�O�敪
            'If Not (cAtenaGetPara1.p_strJukiJutogaiKB = String.Empty) Then
            '    If Not (cAtenaGetPara1.p_strJukiJutogaiKB = "1") Then
            '        '�G���[��`���擾
            '        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUKIJUTOGAIKB)
            '        '��O�𐶐�
            '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '    End If
            'End If
            '*����ԍ� 000007 2003/03/17 �폜�I��

            '�Ɩ��R�[�h
            If Not (cAtenaGetPara1.p_strGyomuCD = String.Empty) Then
                If (Not UFStringClass.CheckAlphabetNumber(cAtenaGetPara1.p_strGyomuCD)) Then
                    '*����ԍ� 000009 2003/03/18 �C���J�n
                    ''�G���[��`���擾
                    'objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_GYOMUCD)
                    ''��O�𐶐�
                    'Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                    '�G���[��`���擾
                    cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Ɩ��R�[�h", objErrorStruct.m_strErrorCode)
                    '*����ԍ� 000009 2003/03/18 �C���I��
                End If
            End If

            '�Ɩ�����ʃR�[�h
            If Not (cAtenaGetPara1.p_strGyomunaiSHU_CD = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strGyomunaiSHU_CD)) Then
                    '*����ԍ� 000009 2003/03/18 �C���J�n
                    ''�G���[��`���擾
                    'objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_GYOMUNAISHU_CD)
                    ''��O�𐶐�
                    'Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                    '�G���[��`���擾
                    cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Ɩ�����ʃR�[�h", objErrorStruct.m_strErrorCode)
                    '*����ԍ� 000009 2003/03/18 �C���I��
                End If
            End If

            '*����ԍ� 000007 2003/03/17 �폜�J�n
            ''���t��f�[�^�敪
            'If Not (cAtenaGetPara1.p_strSfskDataKB = String.Empty) Then
            '    If (Not (cAtenaGetPara1.p_strSfskDataKB = "1")) Then
            '        '�G���[��`���擾
            '        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_SFSKDATAKB)
            '        '��O�𐶐�
            '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '    End If
            'End If

            ''���ш��ҏW
            'If Not (cAtenaGetPara1.p_strStaiinHenshu = String.Empty) Then
            '    If (Not (cAtenaGetPara1.p_strStaiinHenshu = "1")) Then
            '        '�G���[��`���擾
            '        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_STAIINHENSHU)
            '        '��O�𐶐�
            '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '    End If
            'End If

            ''�f�[�^�敪
            'If Not (cAtenaGetPara1.p_strDataKB = String.Empty) Then
            '    If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strDataKB)) Then
            '        '�G���[��`���擾
            '        objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_DATAKB)
            '        '��O�𐶐�
            '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            '    End If
            'End If
            '*����ԍ� 000007 2003/03/17 �폜�I��

            '�Z���ҏW�P
            If Not (cAtenaGetPara1.p_strJushoHenshu1 = String.Empty) Then
                If (Not (cAtenaGetPara1.p_strJushoHenshu1 = "1")) Then
                    '*����ԍ� 000009 2003/03/18 �C���J�n
                    ''�G���[��`���擾
                    'objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUSHOHENSHU1)
                    ''��O�𐶐�
                    'Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                    '�G���[��`���擾
                    cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���ҏW�P", objErrorStruct.m_strErrorCode)
                    '*����ԍ� 000009 2003/03/18 �C���I��
                End If
            End If

            '�Z���ҏW�Q
            If Not (cAtenaGetPara1.p_strJushoHenshu2 = String.Empty) Then
                If (Not (cAtenaGetPara1.p_strJushoHenshu2 = "1")) Then
                    '*����ԍ� 000009 2003/03/18 �C���J�n
                    ''�G���[��`���擾
                    'objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUSHOHENSHU2)
                    ''��O�𐶐�
                    'Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                    '�G���[��`���擾
                    cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���ҏW�Q", objErrorStruct.m_strErrorCode)
                    '*����ԍ� 000009 2003/03/18 �C���I��
                End If
            End If

            '�Z���ҏW�R
            If Not (cAtenaGetPara1.p_strJushoHenshu3 = String.Empty) Then
                '* ����ԍ� 000028 2007/01/15 �C���J�n
                '* ����ԍ� 000025 2005/07/14 �C���J�n
                'If (Not (cAtenaGetPara1.p_strJushoHenshu3 >= "1" And cAtenaGetPara1.p_strJushoHenshu3 <= "4")) Then
                'If (Not (cAtenaGetPara1.p_strJushoHenshu3 >= "1" And cAtenaGetPara1.p_strJushoHenshu3 <= "5")) Then
                If (Not (cAtenaGetPara1.p_strJushoHenshu3 >= "1" And cAtenaGetPara1.p_strJushoHenshu3 <= "6")) Then
                    '* ����ԍ� 000025 2005/07/14 �C���I��
                    '* ����ԍ� 000028 2007/01/15 �C���I��
                    '*����ԍ� 000009 2003/03/18 �C���J�n
                    ''�G���[��`���擾
                    'objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUSHOHENSHU3)
                    ''��O�𐶐�
                    'Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                    '�G���[��`���擾
                    cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���ҏW�R", objErrorStruct.m_strErrorCode)
                    '*����ԍ� 000009 2003/03/18 �C���I��
                End If
            End If

            '�Z���ҏW�S
            If Not (cAtenaGetPara1.p_strJushoHenshu4 = String.Empty) Then
                If (Not (cAtenaGetPara1.p_strJushoHenshu4 = "1")) Then
                    '*����ԍ� 000009 2003/03/18 �C���J�n
                    ''�G���[��`���擾
                    'objErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAGETB_PARA_JUSHOHENSHU4)
                    ''��O�𐶐�
                    'Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

                    '�G���[��`���擾
                    cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���ҏW�S", objErrorStruct.m_strErrorCode)
                    '*����ԍ� 000009 2003/03/18 �C���I��
                End If
            End If

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch ObjAppExp As UFAppException
            '���[�j���O���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + ObjAppExp.Message + "�z")

            ' �G���[���X���[����()
            Throw ObjAppExp

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" _
                                      + "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" _
                                      + "�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try

    End Sub
#End Region

#Region " �������J�����쐬(CreateAtena1Columns) "
    '************************************************************************************************
    '* ���\�b�h��     �������J�����쐬
    '* 
    '* �\��           Private Function CreateAtena1Columns() As DataTable
    '* 
    '* �@�\�@�@    �@�@�������DataSet�̃J�������쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Function CreateAtena1Columns() As DataTable
        Const THIS_METHOD_NAME As String = "CreateAtena1Columns"
        '* corresponds to VS2008 Start 2010/04/16 000039
        'Dim csDataSet As DataSet
        '* corresponds to VS2008 End 2010/04/16 000039
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn
        '*����ԍ� 000011 2003/04/01 �폜�J�n
        'Dim csDataPrimaryKey(4) As DataColumn               '��L�[
        '*����ԍ� 000011 2003/04/01 �폜�I��

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '*����ԍ� 000047 2012/03/13 �C���J�n
            ''* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
            'If Not (m_csOrgAtena1 Is Nothing) Then
            '    Return m_csOrgAtena1.Clone
            'End If
            ''* ����ԍ� 000024 2005/01/25 �ǉ��I���i�{��j

            If ((Not m_blnNenKin) AndAlso (Not m_blnKobetsu)) Then
                '�N���E�ʈȊO�̎��͒ʏ�X�L�[�}������
                If (Not m_csOrgAtena1 Is Nothing) Then
                    Return m_csOrgAtena1.Clone
                Else
                    '�������Ȃ�
                End If
            Else
                '�N��or�ʂ̎��͐�p�̃X�L�[�}������
                If (Not m_csOrgNenkinKobetsu Is Nothing) Then
                    Return m_csOrgNenkinKobetsu.Clone
                Else
                    '�������Ȃ�
                End If
            End If
            '*����ԍ� 000047 2012/03/13 �C���I��

            csDataTable = New DataTable()
            csDataTable.TableName = ABAtena1Entity.TABLE_NAME
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUMINCD, System.Type.GetType("System.String"))
            csDataColumn.AllowDBNull = False
            csDataColumn.MaxLength = 15
            '*����ԍ� 000011 2003/04/01 �폜�J�n
            'csDataPrimaryKey(0) = csDataColumn              '��L�[�@
            '*����ԍ� 000011 2003/04/01 �폜�I��
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.DAINOKB, System.Type.GetType("System.String"))
            csDataColumn.AllowDBNull = False
            csDataColumn.MaxLength = 2
            '*����ԍ� 000011 2003/04/01 �폜�J�n
            'csDataPrimaryKey(1) = csDataColumn              '��L�[�A
            '*����ԍ� 000011 2003/04/01 �폜�I��

            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.DAINOKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.DAINOKBRYAKUMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5

            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.GYOMUCD, System.Type.GetType("System.String"))
                csDataColumn.AllowDBNull = False
                csDataColumn.MaxLength = 2
                '*����ԍ� 000011 2003/04/01 �폜�J�n
                'csDataPrimaryKey(2) = csDataColumn              '��L�[�B
                '*����ԍ� 000011 2003/04/01 �폜�I��
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.GYOMUNAISHU_CD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
                '*����ԍ� 000011 2003/04/01 �폜�J�n
                'csDataPrimaryKey(3) = csDataColumn              '��L�[�C
                '*����ԍ� 000011 2003/04/01 �폜�I��
            End If
            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.STAICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ATENADATAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ATENADATASHU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUSHUBETSU, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUSHUBETSURYAKU, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 3
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEARCHKANASEIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120        '40
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEARCHKANASEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 72         '24
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEARCHKANAMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 48         '16
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEARCHKANJIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480        '40
            End If
            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUKANASHIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 240        '60
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUKANJISHIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 920        '80
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.UMAREYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.UMAREWMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.UMAREHYOJIWMD, System.Type.GetType("System.String"))
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.UMARESHOMEIWMD, System.Type.GetType("System.String"))
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEIBETSUCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEIBETSU, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUZOKUGARACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUZOKUGARA, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 40         '15
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HOJINDAIHYOUSHA, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480        '30
            End If
            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KJNHJNKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KJNHJNKBMEISHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KANNAIKANGAIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.NAIGAIKBMEISHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
            End If
            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.YUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUSHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '30
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUJUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 640        '80
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.BANCHICD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.BANCHICD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.BANCHICD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.BANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '30
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KATAGAKIFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KATAGAKICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 240         '30
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RENRAKUSAKI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RENRAKUSAKI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.GYOSEIKUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.GYOSEIKUMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUCD1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUMEI1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUCD2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUMEI2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUCD3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CHIKUMEI3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TOROKUIDOYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TOROKUJIYUCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TOROKUJIYU, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 10
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHOJOIDOYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHOJOJIYUCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHOJOJIYU, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 10
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUNUSHIJUMINCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 15
            End If
            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
            csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUKANANUSHIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 120        '40
            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUNUSHIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480        '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HYOJIJUN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 4
            End If
            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
            '*����ԍ� 000011 2003/04/01 �폜�J�n
            'csDataTable.PrimaryKey = csDataPrimaryKey       '��L�[
            '*����ԍ� 000011 2003/04/01 �폜�I��
            '*����ԍ� 000012 2003/04/18 �ǉ��J�n
            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZOKUGARACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZOKUGARA, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 40         '15
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KANAMEISHO2, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120        '60
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KANJIMEISHO2, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480        '40
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SEKINO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
            End If
            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��jIF���ň͂�
            '*����ԍ� 000012 2003/04/18 �ǉ��I��
            '*����ԍ� 000017 2003/10/09 �ǉ��J�n
            '*����ԍ� 000020 2003/12/01 �폜�J�n
            'csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RENRAKUSAKI_GYOMUCD, System.Type.GetType("System.String"))
            'csDataColumn.MaxLength = 2
            '*����ԍ� 000020 2003/12/01 �폜�I��
            '*����ԍ� 000017 2003/10/09 �ǉ��I��

            '*����ԍ� 000030 2007/04/28 �ǉ��J�n
            '���p�擾����
            If m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RENRAKUSAKI_GYOMUCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KYUSEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 60         '15
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUTEIIDOYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUTEIJIYU, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 10
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HON_ZJUSHOCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 13
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_YUBINNO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_ZJUSHOCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 13
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_JUSHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200         '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_BANCHI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200         '20
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENUMAEJ_KATAGAKI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 240         '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIYUBINNO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIZJUSHOCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 13
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIIDOYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIJUSHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200         '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIBANCHI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200         '20
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEIKATAGAKI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 240         '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUYOTEISTAINUSMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480        '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIYUBINNO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIZJUSHOCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 13
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIIDOYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTITSUCHIYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIJUSHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200         '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIBANCHI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200         '20
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTIKATAGAKI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 240         '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUKKTISTAINUSMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480        '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUMAEBANCHI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200         '20
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HENSHUMAEKATAGAKI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 240         '30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHOJOTDKDYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CKINJIYUCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKICD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 3
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TOROKUTDKDYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.JUTEITDKDYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.TENSHUTSUNYURIYU, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 30
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHICHOSONCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 6
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.CKINIDOYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOSHINNICHIJI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 17
            End If
            '*����ԍ� 000030 2007/04/28 �ǉ��I��

            '*����ԍ� 000037 2008/11/18 �C���J�n
            '*����ԍ� 000036 2008/11/10 �ǉ��J�n
            'If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso m_blnKobetsu = False AndAlso _
            '    (m_strRiyoTdkdKB = "1" OrElse m_strRiyoTdkdKB = "2")) Then
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso m_blnKobetsu = False AndAlso
                m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo AndAlso (m_strRiyoTdkdKB = "1" OrElse m_strRiyoTdkdKB = "2")) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.NOZEIID, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 11
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.RIYOSHAID, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 11
            Else
            End If
            '*����ԍ� 000036 2008/11/10 �ǉ��I��
            '*����ԍ� 000037 2008/11/18 �C���I��

            '*����ԍ� 000040 2010/05/14 �ǉ��J�n
            If (m_blnNenKin = False AndAlso m_blnKobetsu = False) Then
                ' �ʏ�A�ȈՈ����p�A���p�̂�

                ' �{�ЕM���ҏ��o�͔���
                If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                    ' �p�����[�^:�{�ЕM���Ҏ擾�敪��"1"���A�Ǘ����:�{�Ў擾�敪(10�18)��"1"�̏ꍇ�̂ݏo��
                    ' �{�ЏZ��
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HON_JUSHO, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 200        '30
                    ' �{�ДԒn
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HONSEKIBANCHI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 200        '20
                    ' �M����
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HITTOSH, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 480        '30
                Else
                End If

                ' ������~�敪�o�͔���
                If (m_strShoriteishiKB = "1" AndAlso m_strShoriteishiKB_Param = "1") Then
                    ' �p�����[�^:������~�敪�擾�敪��"1"���A�Ǘ����:������~�敪�擾�敪(10�19)��"1"�̏ꍇ�̂ݏo��
                    ' ������~�敪
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHORITEISHIKB, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 1
                Else
                End If

                '*����ԍ� 000041 2011/05/18 �ǉ��J�n
                ' �O���l�ݗ����o�͔���
                If (m_strFrnZairyuJohoKB_Param = "1") Then
                    ' �p�����[�^:�O���l�ݗ����擾�敪��"1"�̏ꍇ�̂�
                    ' ����
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 20
                    ' �ݗ����i�R�[�h
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAKCD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 3
                    ' �ݗ����i
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAK, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 15
                    ' �ݗ�����
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUKIKAN, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 4
                    ' �ݗ��J�n�N����
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ST_YMD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 8
                    ' �ݗ��I���N����
                    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ED_YMD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 8
                End If
                '*����ԍ� 000041 2011/05/18 �ǉ��I��
                '*����ԍ� 000046 2011/11/07 �ǉ��J�n
                '�Z��@��������
                If (m_strJukiHokaiseiKB_Param = "1") Then
                    '�Z���[��ԋ敪
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUMINHYOJOTAIKBN, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 1
                    '�Z���n�͏o�L���t���O
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKYOCHITODOKEFLG, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 1
                    '�{����
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.HONGOKUMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 480
                    '�J�i�{����
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHONGOKUMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 120
                    '���L��
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJIHEIKIMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 480
                    '�J�i���L��
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHEIKIMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 120
                    '�ʏ̖�
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJITSUSHOMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 480
                    '�J�i�ʏ̖�
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANATSUSHOMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 120
                    '�J�^�J�i���L��
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KATAKANAHEIKIMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 120
                    '���N�����s�ڋ敪
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.UMAREFUSHOKBN, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 2
                    '�ʏ̖��o�^�i�ύX�j�N����
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 8
                    '�ݗ����ԃR�[�h
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANCD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 7
                    '�ݗ����Ԗ���
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 10
                    '�������ݗ��҂ł���|���̃R�[�h
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHACD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 2
                    '�������ݗ��҂ł���|��
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHAMEISHO, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 20
                    '�ݗ��J�[�h���ԍ�
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUCARDNO, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 12
                    '���ʉi�Z�ҏؖ�����t�N����
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYMD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 8
                    '���ʉi�Z�ҏؖ�����t�\����ԊJ�n��
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEISTYMD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 8
                    '����i�Z�ҏؖ�����t�\����ԏI����
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEIEDYMD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 8
                    '�Z��Ώێҁi��30��45��Y���j�����ٓ��N����
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 8
                    '�Z��Ώێҁi��30��45��Y���j�������R�R�[�h
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 2
                    '�Z��Ώێҁi��30��45��Y���j�������R
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 10
                    '�Z��Ώێҁi��30��45��Y���j�����͏o�N����
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 8
                    '�Z��Ώێҁi��30��45��Y���j�����͏o�ʒm�敪
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 1
                    '�O���l���ю喼
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 480
                    '�O���l���ю�J�i��
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSKANAMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 120
                    '���ю啹�L��
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSHEIKIMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 480
                    '���ю�J�i���L��
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 120
                    '���ю�ʏ̖�
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSTSUSHOMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 480
                    '���ю�J�i�ʏ̖�
                    csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 120
                Else
                    '�����Ȃ�
                End If
                '*����ԍ� 000046 2011/11/07 �ǉ��I��

                '*����ԍ� 000048 2014/04/28 �ǉ��J�n
                ' ���ʔԍ�����
                If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                    ' ���ʔԍ�
                    csDataColumn = csDataTable.Columns.Add(ABMyNumberEntity.MYNUMBER, System.Type.GetType("System.String"))
                    csDataColumn.MaxLength = 13
                Else
                    ' noop
                End If
                '*����ԍ� 000048 2014/04/28 �ǉ��I��

                '*����ԍ� 000047 2012/03/13 �ǉ��J�n
                '�ʏ�X�L�[�}�ɕۑ�
                m_csOrgAtena1 = csDataTable.Clone
                '*����ԍ� 000047 2012/03/13 �ǉ��I��
            Else
                '*����ԍ� 000047 2012/03/13 �ǉ��J�n
                '�N���E�ʃX�L�[�}�ɕۑ�
                m_csOrgNenkinKobetsu = csDataTable.Clone
                '*����ԍ� 000047 2012/03/13 �ǉ��I��
            End If
            '*����ԍ� 000040 2010/05/14 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exException As UFAppException

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �X���[����
            Throw exException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        '*����ԍ� 000047 2012/03/13 �C���J�n
        ''* ����ԍ� 000024 2005/01/25 �ύX�J�n�i�{��j
        ''Return csDataTable
        'm_csOrgAtena1 = csDataTable
        'Return m_csOrgAtena1.Clone
        ''* ����ԍ� 000024 2005/01/25 �ύX�I���i�{��j

        Return csDataTable
        '*����ԍ� 000047 2012/03/13 �C���I��
    End Function
#End Region

#Region " �N���p�������J�����쐬(CreateNenkinAtenaColumns) "
    '*����ԍ� 000013 2003/04/18 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �N���p�������J�����쐬
    '* 
    '* �\��           Private Function CreateNenkinAtenaColumns(ByVal strGyomuMei As String) As DataTable
    '* 
    '* �@�\�@�@    �@�@�N���p�������DataSet�̃J�������쐬����
    '* 
    '* ����           ByVal strGyomuMei As String
    '* 
    '* �߂�l         DataSet(ABNenkinAtena) : �쐬�����N���p�������
    '************************************************************************************************
    '*����ԍ� 000027 2006/07/31 �C���J�n
    Private Function CreateNenkinAtenaColumns(ByVal strGyomuMei As String) As DataTable
        'Private Function CreateNenkinAtenaColumns() As DataTable
        '*����ԍ� 000027 2006/07/31 �C���I��
        Const THIS_METHOD_NAME As String = "CreateNenkinAtenaColumns"
        '* corresponds to VS2008 Start 2010/04/16 000039
        'Dim csDataSet As DataSet
        '* corresponds to VS2008 End 2010/04/16 000039
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
            If Not (m_csOrgAtena1Nenkin Is Nothing) Then
                Return m_csOrgAtena1Nenkin.Clone
            End If
            '* ����ԍ� 000024 2005/01/25 �ǉ��I���i�{��j

            ' ���������쐬����
            csDataTable = CreateAtena1Columns()
            csDataTable.TableName = ABNenkinAtenaEntity.TABLE_NAME

            '*����ԍ� 000020 2003/12/01 �ǉ��J�n
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.RENRAKUSAKI_GYOMUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            '*����ԍ� 000020 2003/12/01 �ǉ��I��
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.KYUSEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 60         '15
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.JUTEIIDOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.JUTEIJIYU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            '*����ԍ� 000022 2003/12/04 �ǉ��J�n
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.HON_ZJUSHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            '*����ԍ� 000022 2003/12/04 �ǉ��I��
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_YUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            '*����ԍ� 000017 2003/10/09 �ǉ��J�n
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_ZJUSHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            '*����ԍ� 000017 2003/10/09 �ǉ��I��
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_JUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '30
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_BANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '20
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_KATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 240         '30
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIYUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIZJUSHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIIDOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIJUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '30
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIBANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '20
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEIKATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 240         '30
            '*����ԍ� 000022 2003/12/04 �ǉ��J�n
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUYOTEISTAINUSMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480        '30
            '*����ԍ� 000022 2003/12/04 �ǉ��I��
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIYUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            '*����ԍ� 000017 2003/10/09 �ǉ��J�n
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIZJUSHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            '*����ԍ� 000017 2003/10/09 �ǉ��I��
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIIDOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTITSUCHIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIJUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '30
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIBANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '20
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTIKATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 240         '30
            '*����ԍ� 000022 2003/12/04 �ǉ��J�n
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENSHUTSUKKTISTAINUSMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480        '30
            '*����ԍ� 000022 2003/12/04 �ǉ��I��
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.HENSHUMAEBANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200         '20
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.HENSHUMAEKATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 240         '30
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.SHOJOTDKDYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.CKINJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            '*����ԍ� 000022 2003/12/04 �ǉ��J�n
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.KOKUSEKICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            '*����ԍ� 000022 2003/12/04 �ǉ��I��
            '*����ԍ� 000027 2006/07/31 �C���J�n
            If strGyomuMei = "NENKIN_2" Then
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.TENUMAEJ_STAINUSMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480        '30
            End If
            '*����ԍ� 000027 2006/07/31 �C���I��

            '*����ԍ� 000044 2011/06/24 �ǉ��J�n
            ' �O���l�ݗ����o�͔���
            If (m_strFrnZairyuJohoKB_Param = "1") Then
                ' �p�����[�^:�O���l�ݗ����擾�敪��"1"�̏ꍇ�̂�
                ' ����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 20
                ' �ݗ����i�R�[�h
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAKCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 3
                ' �ݗ����i
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAK, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 15
                ' �ݗ�����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUKIKAN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 4
                ' �ݗ��J�n�N����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ST_YMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                ' �ݗ��I���N����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ED_YMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
            End If
            '*����ԍ� 000044 2011/06/24 �ǉ��I��

            '*����ԍ� 000040 2010/05/14 �ǉ��J�n
            ' �{�ЕM���ҏ��o�͔���
            If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                ' �p�����[�^:�{�ЕM���Ҏ擾�敪��"1"���A�Ǘ����:�{�Ў擾�敪(10�18)��"1"�̏ꍇ�̂ݏo��
                ' �{�ЏZ��
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HON_JUSHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200        '30
                ' �{�ДԒn
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HONSEKIBANCHI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200        '20
                ' �M����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HITTOSH, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480        '30
            Else
            End If

            ' ������~�敪�o�͔���
            If (m_strShoriteishiKB = "1" AndAlso m_strShoriteishiKB_Param = "1") Then
                ' �p�����[�^:������~�敪�擾�敪��"1"���A�Ǘ����:������~�敪�擾�敪(10�19)��"1"�̏ꍇ�̂ݏo��
                ' ������~�敪
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHORITEISHIKB, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
            Else
            End If
            '*����ԍ� 000040 2010/05/14 �ǉ��I��

            '*����ԍ� 000044 2011/06/24 �폜�J�n
            ''*����ԍ� 000041 2011/05/18 �ǉ��J�n
            '' �O���l�ݗ����o�͔���
            'If (m_strFrnZairyuJohoKB_Param = "1") Then
            '    ' �p�����[�^:�O���l�ݗ����擾�敪��"1"�̏ꍇ�̂�
            '    ' ����
            '    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKI, System.Type.GetType("System.String"))
            '    csDataColumn.MaxLength = 20
            '    ' �ݗ����i�R�[�h
            '    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAKCD, System.Type.GetType("System.String"))
            '    csDataColumn.MaxLength = 3
            '    ' �ݗ����i
            '    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAK, System.Type.GetType("System.String"))
            '    csDataColumn.MaxLength = 15
            '    ' �ݗ�����
            '    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUKIKAN, System.Type.GetType("System.String"))
            '    csDataColumn.MaxLength = 4
            '    ' �ݗ��J�n�N����
            '    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ST_YMD, System.Type.GetType("System.String"))
            '    csDataColumn.MaxLength = 8
            '    ' �ݗ��I���N����
            '    csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ED_YMD, System.Type.GetType("System.String"))
            '    csDataColumn.MaxLength = 8
            'End If
            ''*����ԍ� 000041 2011/05/18 �ǉ��I��
            '*����ԍ� 000044 2011/06/24 �폜�I��

            '*����ԍ� 000046 2011/11/07 �ǉ��J�n
            '�Z��@��������
            If (m_strJukiHokaiseiKB_Param = "1") Then
                '�Z���[��ԋ敪
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUMINHYOJOTAIKBN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
                '�Z���n�͏o�L���t���O
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKYOCHITODOKEFLG, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
                '�{����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.HONGOKUMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '�J�i�{����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHONGOKUMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '���L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJIHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '�J�i���L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '�ʏ̖�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJITSUSHOMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '�J�i�ʏ̖�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANATSUSHOMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '�J�^�J�i���L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KATAKANAHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '���N�����s�ڋ敪
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.UMAREFUSHOKBN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                '�ʏ̖��o�^�i�ύX�j�N����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '�ݗ����ԃR�[�h
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                '�ݗ����Ԗ���
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 10
                '�������ݗ��҂ł���|���̃R�[�h
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                '�������ݗ��҂ł���|��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHAMEISHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 20
                '�ݗ��J�[�h���ԍ�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUCARDNO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 12
                '���ʉi�Z�ҏؖ�����t�N����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '���ʉi�Z�ҏؖ�����t�\����ԊJ�n��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEISTYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '����i�Z�ҏؖ�����t�\����ԏI����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEIEDYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '�Z��Ώێҁi��30��45��Y���j�����ٓ��N����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '�Z��Ώێҁi��30��45��Y���j�������R�R�[�h
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                '�Z��Ώێҁi��30��45��Y���j�������R
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 10
                '�Z��Ώێҁi��30��45��Y���j�����͏o�N����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '�Z��Ώێҁi��30��45��Y���j�����͏o�ʒm�敪
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
                '�O���l���ю喼
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '�O���l���ю�J�i��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSKANAMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '���ю啹�L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '���ю�J�i���L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '���ю�ʏ̖�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSTSUSHOMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '���ю�J�i�ʏ̖�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
            Else
                '�����Ȃ�
            End If
            '*����ԍ� 000046 2011/11/07 �ǉ��I��

            '*����ԍ� 000048 2014/04/28 �ǉ��J�n
            ' ���ʔԍ�����
            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                ' ���ʔԍ�
                csDataColumn = csDataTable.Columns.Add(ABMyNumberEntity.MYNUMBER, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 13
            Else
                ' noop
            End If
            '*����ԍ� 000048 2014/04/28 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exException As UFAppException

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �X���[����
            Throw exException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                      "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        '* ����ԍ� 000024 2005/01/25 �ύX�J�n�i�{��j
        'Return csDataTable
        m_csOrgAtena1Nenkin = csDataTable
        Return m_csOrgAtena1Nenkin.Clone
        '* ����ԍ� 000024 2005/01/25 �ύX�I���i�{��j


    End Function
    '*����ԍ� 000013 2003/04/18 �ǉ��I��
#End Region

#Region " �����ʏ��J�����쐬(CreateAtena1KobetsuColumns) "
    '*����ԍ� 000019 2003/11/19 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����ʏ��J�����쐬
    '* 
    '* �\��           Private Function CreateAtena1KobetsuColumns() As DataTable
    '* 
    '* �@�\�@�@    �@�@�����ʏ��DataSet�̃J�������쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataSet(ABAtena1Kobetsu) : �쐬���������ʏ��
    '************************************************************************************************
    Private Function CreateAtena1KobetsuColumns() As DataTable
        '* corresponds to VS2008 Start 2010/04/16 000039
        'Dim csDataSet As DataSet
        '* corresponds to VS2008 End 2010/04/16 000039
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
            If Not (m_csOrgAtena1Kobetsu Is Nothing) Then
                Return m_csOrgAtena1Kobetsu.Clone
            End If
            '* ����ԍ� 000024 2005/01/25 �ǉ��I���i�{��j
            ' ���������쐬����
            csDataTable = CreateAtena1Columns()
            csDataTable.TableName = ABAtena1KobetsuEntity.TABLE_NAME

            '*����ԍ� 000020 2003/12/01 �ǉ��J�n
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaEntity.RENRAKUSAKI_GYOMUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            '*����ԍ� 000020 2003/12/01 �ǉ��I��
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KSNENKNNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKIGO1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNNO1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNSHU1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNEDABAN1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKB1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKIGO2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNNO2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNSHU2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNEDABAN2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKB2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKIGO3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNNO3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNSHU3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNEDABAN3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JKYNENKNKB3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHONO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 14
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOGAKUENKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 16
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 16
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.INKANNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.INKANTOROKUKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 9
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JIDOTEATESTYM, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.JIDOTEATEEDYM, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGHIHKNSHANO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGSKAKKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            '*����ԍ� 000034 2008/01/15 �ǉ��J�n
            If (m_strKobetsuShutokuKB = "1") Then
                ' �ʎ����擾�敪��"1"�̏ꍇ�͌������ڂ�ǉ�����
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB, System.Type.GetType("System.String"))           ' ���i�敪
                csDataColumn.MaxLength = 1
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO, System.Type.GetType("System.String"))          ' ��ی��Ҕԍ�
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD, System.Type.GetType("System.String"))     ' ��ی��Ҏ��i�擾���R�R�[�h
                csDataColumn.MaxLength = 3
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI, System.Type.GetType("System.String"))    ' ��ی��Ҏ��i�擾���R����
                csDataColumn.MaxLength = 10
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD, System.Type.GetType("System.String"))        ' ��ی��Ҏ��i�擾�N����
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD, System.Type.GetType("System.String"))     ' ��ی��Ҏ��i�r�����R�R�[�h
                csDataColumn.MaxLength = 3
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI, System.Type.GetType("System.String"))    ' ��ی��Ҏ��i�r�����R����
                csDataColumn.MaxLength = 10
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD, System.Type.GetType("System.String"))        ' ��ی��Ҏ��i�r���N����
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD, System.Type.GetType("System.String"))     ' �ی��Ҕԍ��K�p�J�n�N����
                csDataColumn.MaxLength = 8
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD, System.Type.GetType("System.String"))     ' �ی��Ҕԍ��K�p�I���N����
                csDataColumn.MaxLength = 8
            Else
                ' �ʎ����擾�敪���l�Ȃ��̏ꍇ�͌������ڂ�ǉ����Ȃ�
            End If

            '*����ԍ� 000034 2008/01/15 �ǉ��I��

            '*����ԍ� 000040 2010/05/14 �ǉ��J�n
            ' �{�ЕM���ҏ��o�͔���
            If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                ' �p�����[�^:�{�ЕM���Ҏ擾�敪��"1"���A�Ǘ����:�{�Ў擾�敪(10�18)��"1"�̏ꍇ�̂ݏo��
                ' �{�ЏZ��
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HON_JUSHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200        '30
                ' �{�ДԒn
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HONSEKIBANCHI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200        '20
                ' �M����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.HITTOSH, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480        '30
            Else
            End If

            ' ������~�敪�o�͔���
            If (m_strShoriteishiKB = "1" AndAlso m_strShoriteishiKB_Param = "1") Then
                ' �p�����[�^:������~�敪�擾�敪��"1"���A�Ǘ����:������~�敪�擾�敪(10�19)��"1"�̏ꍇ�̂ݏo��
                ' ������~�敪
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.SHORITEISHIKB, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
            Else
            End If
            '*����ԍ� 000040 2010/05/14 �ǉ��I��

            '*����ԍ� 000041 2011/05/18 �ǉ��J�n
            ' �O���l�ݗ����o�͔���
            If (m_strFrnZairyuJohoKB_Param = "1") Then
                ' �p�����[�^:�O���l�ݗ����擾�敪��"1"�̏ꍇ�̂�
                ' ����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.KOKUSEKI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 20
                ' �ݗ����i�R�[�h
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAKCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 3
                ' �ݗ����i
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUSKAK, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 15
                ' �ݗ�����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYUKIKAN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 4
                ' �ݗ��J�n�N����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ST_YMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                ' �ݗ��I���N����
                csDataColumn = csDataTable.Columns.Add(ABAtena1Entity.ZAIRYU_ED_YMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
            End If
            '*����ԍ� 000041 2011/05/18 �ǉ��I��
            '*����ԍ� 000046 2011/11/07 �ǉ��J�n
            '�Z��@��������
            If (m_strJukiHokaiseiKB_Param = "1") Then
                '�Z���[��ԋ敪
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUMINHYOJOTAIKBN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
                '�Z���n�͏o�L���t���O
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKYOCHITODOKEFLG, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
                '�{����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.HONGOKUMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '�J�i�{����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHONGOKUMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '���L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJIHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '�J�i���L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANAHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '�ʏ̖�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANJITSUSHOMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '�J�i�ʏ̖�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KANATSUSHOMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '�J�^�J�i���L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KATAKANAHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '���N�����s�ڋ敪
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.UMAREFUSHOKBN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                '�ʏ̖��o�^�i�ύX�j�N����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '�ݗ����ԃR�[�h
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                '�ݗ����Ԗ���
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 10
                '�������ݗ��҂ł���|���̃R�[�h
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                '�������ݗ��҂ł���|��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUSHAMEISHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 20
                '�ݗ��J�[�h���ԍ�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.ZAIRYUCARDNO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 12
                '���ʉi�Z�ҏؖ�����t�N����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '���ʉi�Z�ҏؖ�����t�\����ԊJ�n��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEISTYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '����i�Z�ҏؖ�����t�\����ԏI����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.KOFUYOTEIEDYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '�Z��Ώێҁi��30��45��Y���j�����ٓ��N����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '�Z��Ώێҁi��30��45��Y���j�������R�R�[�h
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 2
                '�Z��Ώێҁi��30��45��Y���j�������R
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 10
                '�Z��Ώێҁi��30��45��Y���j�����͏o�N����
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 8
                '�Z��Ώێҁi��30��45��Y���j�����͏o�ʒm�敪
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
                '�O���l���ю喼
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '�O���l���ю�J�i��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.FRNSTAINUSKANAMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '���ю啹�L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '���ю�J�i���L��
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
                '���ю�ʏ̖�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSTSUSHOMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                '���ю�J�i�ʏ̖�
                csDataColumn = csDataTable.Columns.Add(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 120
            Else
                '�����Ȃ�
            End If
            '*����ԍ� 000046 2011/11/07 �ǉ��I��

            '*����ԍ� 000048 2014/04/28 �ǉ��J�n
            ' ���ʔԍ�����
            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                ' ���ʔԍ�
                csDataColumn = csDataTable.Columns.Add(ABMyNumberEntity.MYNUMBER, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 13
            Else
                ' noop
            End If
            '*����ԍ� 000048 2014/04/28 �ǉ��I��

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch exException As UFAppException

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + Me.GetType.Name + "�z" +
                                      "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �X���[����
            Throw exException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + Me.GetType.Name + "�z" +
                                      "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        '* ����ԍ� 000024 2005/01/25 �ύX�J�n�i�{��j
        'Return csDataTable
        m_csOrgAtena1Kobetsu = csDataTable
        Return m_csOrgAtena1Kobetsu.Clone
        '* ����ԍ� 000024 2005/01/25 �ύX�I���i�{��j

    End Function
    '*����ԍ� 000019 2003/11/19 �ǉ��I��
#End Region

    '*����ԍ� 000050 2023/03/10 �ǉ��J�n
#Region " �������W���ŃJ�����쐬(CreateAtena1HyojunColumns) "
    '************************************************************************************************
    '* ���\�b�h��     �������W���ŃJ�����쐬
    '* 
    '* �\��           Private Function CreateAtena1HyojunColumns() As DataTable
    '* 
    '* �@�\�@�@    �@�@�������W����DataSet�̃J�������쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataSet(ABAtena1Hyojun) : �쐬�����������
    '************************************************************************************************
    Private Function CreateAtena1HyojunColumns() As DataTable
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If Not (m_csOrgAtena1Hyojun Is Nothing) Then
                Return m_csOrgAtena1Hyojun.Clone
            End If
            ' ���������쐬����
            csDataTable = CreateAtena1Columns()
            csDataTable.TableName = ABAtena1HyojunEntity.TABLE_NAME

            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL, System.Type.GetType("System.String"))

            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL, System.Type.GetType("System.String"))

            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SEIBETSU_FULL, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 10
            Else
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUJUSHO_FULL, System.Type.GetType("System.String"))

            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KATAGAKI_FULL, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200
            If m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_KATAGAKI_FULL, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1200
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1200
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1200
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUMAEKATAGAKI_FULL, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1200
            End If
            If (m_strFrnZairyuJohoKB_Param = "1") Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KOKUSEKI_FULL, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 100
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIMEIYUSENKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KANJIKYUUJI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 80
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KANAKYUUJI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.UMAREBIFUSHOPTN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOUMAREBI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 72
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KISAIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHOJOIDOWMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 72
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINIDOWMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOCKINIDOBI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 72
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIKUCHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.MACHIAZACD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TODOFUKEN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 16
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIKUCHOSON, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 48
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.MACHIAZA, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 6
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_MACHIAZACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_TODOFUKEN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 16
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 48
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_MACHIAZA, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
            End If
            If m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KOKUSEKICD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 3
            End If
            If m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSONCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 6
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_TODOFUKEN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 16
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_SHIKUCHOSON, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 48
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_MACHIAZA, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKICD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 3
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_KOKUSEKI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENUMAEJ_KOKUGAIJUSHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 300
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 6
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEITODOFUKEN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 16
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEISHIKUCHOSON, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 48
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIMACHIAZA, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKICD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 3
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUSEKI, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 200
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 300
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 6
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTITODOFUKEN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 16
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTISHIKUCHOSON, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 48
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TENSHUTSUKKTIMACHIAZA, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUKYOCHIHOSEICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINTDKDTUCIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HANNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KAISEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNIDOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.NYURYOKUBASHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.NYURYOKUBASHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 400
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 254
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 254
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 254
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 50
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 300
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 300
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 100
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KYOJUFUMEI_YMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2000
            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.BANGOHOKOSHINKB, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
            End If
            If m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1 Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SERIALNO, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 40
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SFSKKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINSHUBETSU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINJOTAI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.BANCHIEDABANSUCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch exException As UFAppException

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + Me.GetType.Name + "�z" +
                                      "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �X���[����
            Throw exException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + Me.GetType.Name + "�z" +
                                      "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        m_csOrgAtena1Hyojun = csDataTable
        Return m_csOrgAtena1Hyojun.Clone

    End Function
#End Region

#Region " �N���p�������W���ŃJ�����쐬(CreateNenkinAtenaHyojunColumns) "
    '************************************************************************************************
    '* ���\�b�h��     �N���p�������W���ŃJ�����쐬
    '* 
    '* �\��           Private Function CreateNenkinAtenaHyojunColumns(ByVal strGyomuMei As String) As DataTable
    '* 
    '* �@�\�@�@    �@�@�N���p�������W����DataSet�̃J�������쐬����
    '* 
    '* ����           ByVal strGyomuMei As String
    '* 
    '* �߂�l         DataSet(Atena1NenkinHyojun) : �쐬�����N���p�������
    '************************************************************************************************
    Private Function CreateNenkinAtenaHyojunColumns(ByVal strGyomuMei As String) As DataTable
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If Not (m_csOrgAtena1NenkinHyojun Is Nothing) Then
                Return m_csOrgAtena1NenkinHyojun.Clone
            End If

            ' ���������쐬����
            csDataTable = CreateNenkinAtenaColumns(strGyomuMei)
            csDataTable.TableName = ABNenkinAtenaHyojunEntity.TABLE_NAME

            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HENSHUKANASHIMEI_FULL, System.Type.GetType("System.String"))

            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HENSHUKANJISHIMEI_FULL, System.Type.GetType("System.String"))

            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SEIBETSU_FULL, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 10
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HENSHUJUSHO_FULL, System.Type.GetType("System.String"))

            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KATAGAKI_FULL, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_KATAGAKI_FULL, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKATAGAKI_FULL, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIKATAGAKI_FULL, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HENSHUMAEKATAGAKI_FULL, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200
            If (m_strFrnZairyuJohoKB_Param = "1") Then
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KOKUSEKI_FULL, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 100
            End If
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.STAINUSSHIMEIYUSENKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHIMEIYUSENKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KANJIKYUUJI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 80
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KANAKYUUJI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHIMEIKANAKAKUNINFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KYUUJIKANAKAKUNINFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TSUSHOKANAKAKUNINFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.UMAREBIFUSHOPTN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.FUSHOUMAREBI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 72
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HYOJUNKISAIJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KISAIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HYOJUNSHOJOJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHOJOIDOWMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.FUSHOSHOJOIDOBI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 72
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.CKINIDOWMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.CKINIDOBIFUSHOPTN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.FUSHOCKINIDOBI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 72
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JIJITSUSTAINUSMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHIKUCHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.MACHIAZACD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TODOFUKEN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 16
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.SHIKUCHOSON, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 48
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.MACHIAZA, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_SHIKUCHOSONCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 6
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_MACHIAZACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_TODOFUKEN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 16
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_SHIKUGUNCHOSON, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 48
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HON_MACHIAZA, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
            End If
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZACD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_TODOFUKEN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 16
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 48
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_MACHIAZA, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUSEKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 300
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 16
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 48
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 200
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 300
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 16
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 48
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HODAI30JO46MATAHA47KB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.ZAIRYUCARDNOKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JUKYOCHIHOSEICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.CKINTDKDTUCIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HANNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.KAISEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HYOJUNIDOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.NYURYOKUBASHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.NYURYOKUBASHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 400
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKI1_RENRAKUSAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 254
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKI2_RENRAKUSAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 254
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKI3_RENRAKUSAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 254
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKISHUBETSU1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKISHUBETSU2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.RENRAKUSAKISHUBETSU3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.BANGOHOKOSHINKB, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
            End If
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.HYOJUNIDOJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JUMINKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JUMINSHUBETSU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.JUMINJOTAI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABNenkinAtenaHyojunEntity.BANCHIEDABANSUCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch exException As UFAppException

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + Me.GetType.Name + "�z" +
                                      "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �X���[����
            Throw exException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + Me.GetType.Name + "�z" +
                                      "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        m_csOrgAtena1Hyojun = csDataTable
        Return m_csOrgAtena1Hyojun.Clone

    End Function
#End Region

#Region " �����ʏ��W���ŃJ�����쐬(CreateAtena1KobetsuHyojunColumns) "
    '************************************************************************************************
    '* ���\�b�h��     �����ʏ��W���ŃJ�����쐬
    '* 
    '* �\��           Private Function CreateAtena1KobetsuHyojunColumns() As DataTable
    '* 
    '* �@�\�@�@    �@�@�����ʏ��W����DataSet�̃J�������쐬����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataSet(Atena1KobetsuHyojun) : �쐬���������ʏ��
    '************************************************************************************************
    Private Function CreateAtena1KobetsuHyojunColumns() As DataTable
        Dim csDataTable As DataTable
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If Not (m_csOrgAtena1KobetsuHyojun Is Nothing) Then
                Return m_csOrgAtena1KobetsuHyojun.Clone
            End If

            ' ���������쐬����
            csDataTable = CreateAtena1KobetsuColumns()
            csDataTable.TableName = ABAtena1KobetsuHyojunEntity.TABLE_NAME

            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUKANASHIMEI_FULL, System.Type.GetType("System.String"))

            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUKANJISHIMEI_FULL, System.Type.GetType("System.String"))

            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SEIBETSU_FULL, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 10
            Else
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HENSHUJUSHO_FULL, System.Type.GetType("System.String"))

            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KATAGAKI_FULL, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1200
            If (m_strFrnZairyuJohoKB_Param = "1") Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KOKUSEKI_FULL, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 100
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.STAINUSSHIMEIYUSENKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIMEIYUSENKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KANJIKYUUJI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 80
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KANAKYUUJI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIMEIKANAKAKUNINFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KYUUJIKANAKAKUNINFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TSUSHOKANAKAKUNINFG, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.UMAREBIFUSHOPTN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOUMAREBI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 72
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNKISAIJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KISAIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNSHOJOJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHOJOIDOWMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHOJOIDOBIFUSHOPTN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOSHOJOIDOBI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 72
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINIDOWMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINIDOBIFUSHOPTN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUSHOCKINIDOBI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 72
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JIJITSUSTAINUSMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIKUCHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.MACHIAZACD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.TODOFUKEN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 16
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SHIKUCHOSON, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 48
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.MACHIAZA, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            If (m_strHonsekiHittoshKB_Param = "1" AndAlso m_strHonsekiHittoshKB = "1") Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_SHIKUCHOSONCD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 6
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_MACHIAZACD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 7
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_TODOFUKEN, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 16
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_SHIKUGUNCHOSON, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 48
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HON_MACHIAZA, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 480
            End If
            If m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KOKUSEKICD, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 3
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HODAI30JO46MATAHA47KB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.ZAIRYUCARDNOKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUKYOCHIHOSEICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.CKINTDKDTUCIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HANNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 5
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KAISEIYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNIDOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.NYURYOKUBASHOCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.NYURYOKUBASHO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            If (m_strKobetsuShutokuKB = "1") Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 400
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 254
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 254
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 254
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_YUBINNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZACD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 7
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_TODOFUKEN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 4
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 12
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_MACHIAZA, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 480
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_BANCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 50
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 300
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 300
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHAKUBUN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_TAISHOSHASHIMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 100
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_UMAREYMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_SEIBETSU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.KYOJUFUMEI_YMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 8
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.FUGENJUJOHO_BIKO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2000
            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.BANGOHOKOSHINKB, System.Type.GetType("System.String"))
                csDataColumn.MaxLength = 1
            End If
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SERIALNO, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 40
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.HYOJUNIDOJIYUCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SFSKRENRAKUSAKIKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.SFSKKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINKBN, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINSHUBETSU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.JUMINJOTAI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDataColumn = csDataTable.Columns.Add(ABAtena1HyojunEntity.BANCHIEDABANSUCHI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 20

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch exException As UFAppException

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + Me.GetType.Name + "�z" +
                                      "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �X���[����
            Throw exException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`

            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                      "�y�N���X��:" + Me.GetType.Name + "�z" +
                                      "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                      "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        m_csOrgAtena1Hyojun = csDataTable
        Return m_csOrgAtena1Hyojun.Clone

    End Function
#End Region
    '*����ԍ� 000050 2023/03/10 �ǉ��I��

#Region " ���t��Z���s����ҏW�敪�擾(GetSofuJushoGyoseikuType) "
    '*����ԍ� 000016 2003/08/22 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���t��Z���s����ҏW�敪�擾
    '* 
    '* �\��           Private Function GetSofuJushoGyoseikuType() As SofuJushoGyoseikuType
    '* 
    '* �@�\�@�@    �@�@���t��Z���s����ҏW�敪���擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         SofuJushoGyoseikuType
    '************************************************************************************************
    <SecuritySafeCritical>
    Protected Overridable Function GetSofuJushoGyoseikuType() As SofuJushoGyoseikuType
        Const THIS_METHOD_NAME As String = "GetSofuJushoGyoseikuType"
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cURKanriJohoB As URKANRIJOHOCacheBClass         '�Ǘ����擾�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        Dim cSofuJushoGyoseikuType As SofuJushoGyoseikuType

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            '�Ǘ����擾�a�̃C���X�^���X�쐬
            'cURKanriJohoB = New URKANRIJOHOCacheBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
            'cSofuJushoGyoseikuType = m_cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param
            If (m_bSofuJushoGyoseikuTypeFlg = False) Then
                m_cSofuJushoGyoseikuType = m_cURKanriJohoB.GetSofuJushoGyoseiku_SofuJushoGyoseiku_Param
                m_bSofuJushoGyoseikuTypeFlg = True
            End If
            cSofuJushoGyoseikuType = m_cSofuJushoGyoseikuType
            '* ����ԍ� 000024 2005/01/25 �X�V�I���i�{��j

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp
        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

        Return cSofuJushoGyoseikuType

    End Function
    '*����ԍ� 000016 2003/08/22 �ǉ��I��
#End Region

    '*����ԍ� 000042 2011/05/18 �ǉ��J�n
#Region "���̕ҏW����(MeishoHenshu)"
    '************************************************************************************************
    '* ���\�b�h��       ���̕ҏW����
    '* 
    '* �\��             Private Function MeishoHenshu(ByVal csAtenaDataRow As DataRow) As String()
    '* 
    '* �@�\�@�@    �@   �{���ʏ̖��p���̕ҏW�������s��
    '* 
    '* ����             csAtenaDataRow  : DataRow(�����f�[�^)
    '* 
    '* �߂�l           String()        : �z��[�J�i���́A��������]
    '************************************************************************************************
    Private Function MeishoHenshu(ByVal csAtenaDataRow As DataRow) As String()
        Const THIS_METHOD_NAME As String = "MeishoHenshu"
        Dim strMeisho(1) As String                          ' �ԋp�p���̔z��[�J�i���́A��������]
        Dim strGroupID As String                            ' �O���[�vID
        Dim csMeishoSeigyoDS As DataSet                     ' ���̐���f�[�^�p�f�[�^�Z�b�g
        Dim blnMeishoSeigyoFlg As Boolean                   ' ���̐���t���O
        Dim strRiyoFlg As String = String.Empty             ' ���p�t���O
        '*����ԍ� 000043 2011/06/23 �ǉ��J�n
        Dim cuUrlPrmData As USUrlPrmData                    ' URL�p�����[�^�C���^�[�t�F�[�X
        Const DEFAULT_VALUE As String = "01"
        '*����ԍ� 000043 2011/06/23 �ǉ��I��


        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ����������
            strMeisho(0) = String.Empty
            strMeisho(1) = String.Empty

            '*����ԍ� 000043 2011/06/23 �C���J�n
            '**
            '* �ۏ��擾����
            '*
            'URL�p�����[�^�N���X�̃C���X�^���X��
            If (m_cuUSSUrlParm Is Nothing) Then
                m_cuUSSUrlParm = New USUrlParmClass
            End If

            '�ۏ��̎擾
            cuUrlPrmData = m_cuUSSUrlParm.getURLPrm(m_cfUFControlData, USUrlParmClass.PrmType.ToshimaAtenaType, DEFAULT_VALUE)
            strGroupID = cuUrlPrmData.p_strPrm

            'strGroupID = "01"
            '*����ԍ� 000043 2011/06/23 �C���I��

            '**
            '* �D�於�̏��擾����
            '*
            ' �\�����̐���a�N���X�̃C���X�^���X�쐬
            If (m_cABMeishoSeigyoB Is Nothing) Then
                m_cABMeishoSeigyoB = New ABMeishoSeigyoBClass(m_cfUFControlData, m_cfUFConfigDataClass, m_cfUFRdbClass)
            End If

            ' �\�����̐���f�[�^�擾
            csMeishoSeigyoDS = m_cABMeishoSeigyoB.GetMeishoSeigyo(CStr(csAtenaDataRow(ABAtenaEntity.JUMINCD)), strGroupID)

            If (Not (csMeishoSeigyoDS Is Nothing) AndAlso csMeishoSeigyoDS.Tables(ABMeishoSeigyoEntity.TABLE_NAME).Rows.Count > 0) Then
                ' �\�����̐���f�[�^�����݂���ꍇ
                ' ���p�t���O�擾
                strRiyoFlg = csMeishoSeigyoDS.Tables(ABMeishoSeigyoEntity.TABLE_NAME).Rows(0)(ABMeishoSeigyoEntity.RIYOFG).ToString

                blnMeishoSeigyoFlg = True
            Else
                ' �\�����̐���f�[�^�����݂��Ȃ��ꍇ
                strRiyoFlg = String.Empty

                blnMeishoSeigyoFlg = False
            End If

            '**
            '* ���̕ҏW����
            '*
            If (blnMeishoSeigyoFlg = True) Then
                Select Case strRiyoFlg
                    Case "0"        ' �{��
                        '*����ԍ� 000045 2011/06/27 �ǉ��J�n
                        If (csAtenaDataRow(ABAtenaEntity.KANJIMEISHO2).ToString.Trim <> String.Empty) Then
                            ' �������̂Q���󔒈ȊO�̏ꍇ�A�J�i���̂Q�A�������̂Q���Z�b�g
                            strMeisho(0) = csAtenaDataRow(ABAtenaEntity.KANAMEISHO2).ToString
                            strMeisho(1) = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO2).ToString
                        Else
                            ' �������̂Q���󔒂̏ꍇ�A�J�i���̂P�A�������̂P���Z�b�g
                            strMeisho(0) = csAtenaDataRow(ABAtenaEntity.KANAMEISHO1).ToString
                            strMeisho(1) = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO1).ToString
                        End If
                        'strMeisho(0) = csAtenaDataRow(ABAtenaEntity.KANAMEISHO2).ToString
                        'strMeisho(1) = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO2).ToString
                        '*����ԍ� 000045 2011/06/27 �ǉ��I��

                    Case "1"        ' �ʏ̖�
                        strMeisho(0) = csAtenaDataRow(ABAtenaEntity.KANAMEISHO1).ToString
                        strMeisho(1) = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO1).ToString

                    Case Else       ' ����ȊO
                        strMeisho(0) = csAtenaDataRow(ABAtenaEntity.KANAMEISHO1).ToString
                        strMeisho(1) = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO1).ToString

                End Select
            Else
                strMeisho(0) = csAtenaDataRow(ABAtenaEntity.KANAMEISHO1).ToString
                strMeisho(1) = csAtenaDataRow(ABAtenaEntity.KANJIMEISHO1).ToString
            End If

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return strMeisho

    End Function
#End Region
    '*����ԍ� 000042 2011/05/18 �ǉ��I��

#Region "�����`�F�b�N "
    '************************************************************************************************
    '* ���\�b�h��     �����`�F�b�N
    '* 
    '* �\��           Private Function CheckDate(ByVal strDate As String) As Boolean
    '* 
    '* �@�\�@�@    �@�@�����`�F�b�N���s�Ȃ�
    '* 
    '* ����           strDate As String
    '* 
    '* �߂�l         Boolean
    '************************************************************************************************
    <SecuritySafeCritical>
    Private Function CheckDate(ByVal strDate As String) As Boolean
        Const THIS_METHOD_NAME As String = "CheckDate"
        Dim blnResult As Boolean

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            m_cfDate.p_strDateValue = strDate
            blnResult = m_cfDate.CheckDate

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp
        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

        Return blnResult

    End Function

#End Region

#Region "������Z�o "
    '************************************************************************************************
    '* ���\�b�h��     ������Z�o
    '* 
    '* �\��           Private Function GetSeirekiLastDay(ByVal strDate As String) As String
    '* 
    '* �@�\�@�@    �@�@����̖����Z�o���s�Ȃ�
    '* 
    '* ����           strDate As String
    '* 
    '* �߂�l         String
    '************************************************************************************************
    <SecuritySafeCritical>
    Private Function GetSeirekiLastDay(ByVal strDate As String) As String
        Const THIS_METHOD_NAME As String = "GetSeirekiLastDay"
        Dim strResult As String

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            m_cfDate.p_strDateValue = strDate.RSubstring(0, 6) + "01"
            strResult = m_cfDate.GetLastDay()

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp
        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

        Return strResult

    End Function

#End Region

#Region "�a����Z�o "
    '************************************************************************************************
    '* ���\�b�h��     �a����Z�o
    '* 
    '* �\��           Private Function GetWarekiLastDay(ByVal strDate As String) As String
    '* 
    '* �@�\�@�@    �@�@�a��̖����Z�o���s�Ȃ�
    '* 
    '* ����           String
    '* 
    '* �߂�l         Boolean
    '************************************************************************************************
    <SecuritySafeCritical>
    Private Function GetWarekiLastDay(ByVal strDate As String, ByVal strSeireki As String) As String
        Const THIS_METHOD_NAME As String = "GetWarekiLastDay"
        Dim strWork As String
        Dim strResult As String

        Try
            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            strWork = GetSeirekiLastDay(strSeireki)
            strResult = strDate.RSubstring(0, 5) + strWork.RSubstring(6, 2)

            ' �f�o�b�O���O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfUFLogClass.WarningWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp
        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

        Return strResult

    End Function

#End Region

End Class
