'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a���������}�X�^�c�`
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/10�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/02/25 000001     �f�[�^�敪�����鎞���A�f�[�^��ʂ������Ă���ꍇ�́A�f�[�^��ʂ������Ƃ���
'* 2003/03/10 000002     �Z���b�c���̐������`�F�b�N�Ɍ��
'* 2003/03/17 000003     �G���[���b�Z�[�W�̌��
'* 2003/03/27 000004     �G���[�����N���X�̎Q�Ɛ��"AB"�Œ�ɂ���
'* 2003/03/31 000005     �������`�F�b�N��Trim�����l�Ń`�F�b�N����
'* 2003/04/11 000006     ���������擾�ŁA�����N����=99999999������
'* 2003/04/16 000007     ���a��N�����̓��t�`�F�b�N�𐔒l�`�F�b�N�ɕύX
'*                       �����p�J�i�̔��p�J�i�`�F�b�N���`�m�j�`�F�b�N�ɕύX
'* 2003/04/30 000008     �w����������Ă��G���[�ɂ��Ȃ��B
'* 2003/05/20 000009     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/06/12 000010     TOP����O��
'* 2003/08/28 000011     RDB�A�N�Z�X���O�̏C��
'* 2003/09/11 000012     �[���h�c�������`�F�b�N��ANK�ɂ���
'* 2003/10/09 000013     �쐬���[�U�[�E�X�V���[�U�[�`�F�b�N�̕ύX
'* 2003/10/30 000014     �d�l�ύX�A�J�^�J�i�`�F�b�N��ANK�`�F�b�N�ɕύX
'* 2003/11/18 000015     �d�l�ύX�F�f�[�^�敪�Ōl�̂ݎ����Ă���B�i�f�[�^�敪��"1%"�Ǝw�肳�ꂽ�ꍇ�j
'*                       �d�l�ǉ��F�����ʃf�[�^�擾���\�b�h��ǉ�
'* 2004/04/12 000016     �d�l�ύX�F���ߎ��R�`�F�b�N���R�����g�A�E�g�ɏC��
'*          �@�@         �n��R�[�h��ANK�`�F�b�N�ɕύX
'* 2004/10/19 000017     �`�S���Z���R�[�h�̃`�F�b�N��CheckNumber --> CheckANK(�}���S���R)
'* 2004/11/12 000018     �f�[�^�`�F�b�N���s�Ȃ�Ȃ�
'* 2005/01/25 000019     ���x���P�Q�F�i�{��j
'* 2005/06/15 000020     SQL����Insert,Update,�_��Delete,����Delete�̊e���\�b�h���Ă΂ꂽ���Ɋe���쐬����
'* 2005/06/17 000021     ����ԍ��݂̂��擾���郁�\�b�h�ǉ�
'* 2005/11/18 000022     �Z���b�c�w��(�P�Z���b�c�j�ŊY���Z���b�c�̑S�����f�[�^���폜���鏈����ǉ�(�}���S���R)
'* 2005/12/26 000023     �d�l�ύX�F�s����b�c��ANK�`�F�b�N�ɕύX(�}���S���R)
'* 2006/07/31 000024     �N�������Q�b�g�U���ڒǉ�(�g�V)
'* 2007/04/28 000025     ���ň����擾���\�b�h�̒ǉ��ɂ��擾���ڂ̒ǉ� (�g�V)
'* 2007/09/04 000026     �O���l�{���D�挟���p�Ɋ������̂Q��ǉ��i����j
'* 2007/10/10 000027     �O���l�{���D�挟�����\�Ȏs�����́A�J�i���̐擪��"�"�̂Ƃ���"�"��OR�����Ō�������i����j
'* 2008/01/17 000028     �ʎ����f�[�^�擾�@�\�Ɍ������擾������ǉ��i��Áj���l�[�~���O�ύX�i�g�V�j
'* 2010/04/16 000029     VS2008�Ή��i��Áj
'* 2010/05/14 000030     �{�ЕM���ҋy�я�����~�敪�Ή��i��Áj
'* 2011/05/18 000031     �O���l�ݗ����擾�敪�Ή��i��Áj
'* 2011/10/24 000032     �yAB17010�z���Z��@�����Ή�����������t���}�X�^�ǉ�   (����)
'* 2014/04/28 000033     �yAB21040�z�����ʔԍ��Ή������ʔԍ��}�X�^�ǉ��i�΍��j
'* 2014/06/05 000034     �yAB21040-00�z�����ʔԍ��Ή����ʎ擾���\�b�h�̑Ή��R����C�i�΍��j
'* 2015/05/08 000035     �yAB21052�z�����ʔԍ��Ή����l�ԍ���Ď擾�����擾���\�b�h�ǉ��i�≺�j
'* 2020/01/10 000036     �yAB32001�z�A���t�@�x�b�g�����i�΍��j
'* 2023/03/10 000037     �yAB-0970-1�z����GET�擾���ڕW�����Ή��i�����j
'* 2023/08/14 000038     �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ�(����)
'* 2023/10/19 000039     �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ�_�ǉ��C��(����)
'* 2023/12/04 000040     �yAB-1600-1�z�����@�\�Ή�(����)
'* 2023/12/07 000041     �yAB-9000-1�z�Z��X�V�A�g�W�����Ή�(����)
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
'* ���������}�X�^�擾���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABAtenaRirekiBClass
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
    '* ����ԍ� 000022 2005/11/18 �ǉ��J�n
    Private m_strDelFromJuminCDSQL As String                ' �����폜�pSQL(�P�Z���R�[�h�w��)
    '* ����ԍ� 000022 2005/11/18 �ǉ��I��
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT�p�p�����[�^�R���N�V����
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    '�_���폜�p�p�����[�^�R���N�V����
    Private m_cfDelButuriUFParameterCollectionClass As UFParameterCollectionClass   '�����폜�p�p�����[�^�R���N�V����
    '* ����ԍ� 000022 2005/11/18 �ǉ��J�n
    Private m_cfDelFromJuminCDPrmCollection As UFParameterCollectionClass           '�����폜�pSQL(�P�Z���R�[�h�w��)
    '* ����ԍ� 000022 2005/11/18 �ǉ��I��

    '* ����ԍ� 000019 2005/01/25 �ǉ��J�n�i�{��j
    Private m_strAtenaSQLsbAll As StringBuilder = New StringBuilder()
    Private m_strAtenaSQLsbKaniAll As StringBuilder = New StringBuilder()
    Private m_strAtenaSQLsbKaniOnly As StringBuilder = New StringBuilder()
    Private m_strAtenaSQLsbNenkinAll As StringBuilder = New StringBuilder()
    Private m_strKobetuSQLsbAll As StringBuilder = New StringBuilder()
    Private m_strKobetuSQLsbKaniAll As StringBuilder = New StringBuilder()
    Private m_strKobetuSQLsbKaniOnly As StringBuilder = New StringBuilder()
    Private m_strKobetuSQLsbNenkinAll As StringBuilder = New StringBuilder()
    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaKobetu As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaAll As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaKaniAll As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaKaniOnly As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaNenkinAll As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaKobetuAll As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaKobetuKaniAll As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaKobetuKaniOnly As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_csDataSchmaKobetuNenkinAll As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Public m_blnSelectAll As ABEnumDefine.AtenaGetKB = ABEnumDefine.AtenaGetKB.SelectAll '�S���ڑI���im_blnAtenaGet��True�̎�����Get�ŕK�v�ȍ��ڑS�Ă���ȊO��SELECT *�j
    Public m_blnSelectCount As Boolean = False            '�J�E���g���擾���邩�ǂ���
    Public m_blnBatch As Boolean = False               '�o�b�`�t���O
    '* ����ԍ� 000019 2005/01/25 �ǉ��I���i�{��j

    '*����ԍ� 000025 2007/04/28 �ǉ��J�n
    Public m_blnMethodKB As ABEnumDefine.MethodKB  '���\�b�h�敪�i�ʏ�ł��A���ŁA�A�A�j
    '*����ԍ� 000025 2007/04/28 �ǉ��I��

    '*����ԍ� 000028 2008/01/17 �ǉ��J�n
    Public m_strKobetsuShutokuKB As String                  ' �ʎ����擾�敪
    '*����ԍ� 000028 2008/01/17 �ǉ��I��

    '*����ԍ� 000030 2010/05/14 �ǉ��J�n
    Private m_strHonsekiHittoshKB As String = String.Empty          ' �{�ЕM���Ҏ擾�敪(�����Ǘ����)
    Private m_strShoriteishiKB As String = String.Empty             ' ������~�敪�擾�敪(�����Ǘ����)
    Private m_strHonsekiHittoshKB_Param As String = String.Empty    ' �{�ЕM���Ҏ擾�敪�p�����[�^
    Private m_strShoriTeishiKB_Param As String = String.Empty       ' ������~�敪�擾�敪�p�����[�^
    '*����ԍ� 000030 2010/05/14 �ǉ��I��

    '*����ԍ� 000031 2011/05/18 �ǉ��J�n
    Private m_strFrnZairyuJohoKB_Param As String = String.Empty     ' �O���l�ݗ����擾�敪�p�����[�^
    '*����ԍ� 000031 2011/05/18 �ǉ��I��

    '*����ԍ� 000032 2011/10/24 �ǉ��J�n
    Private m_csSekoYMDHanteiB As ABSekoYMDHanteiBClass             '�{�s������B�׽
    Private m_csAtenaRirekiFZYB As ABAtenaRirekiFZYBClass                '�����t���}�X�^B�׽
    Private m_blnJukihoKaiseiFG As Boolean = False
    Private m_strJukihoKaiseiKB As String                           '�Z��@�����敪
    '*����ԍ� 000032 2011/10/24 �ǉ��I��

    '*����ԍ� 000033 2014/04/28 �ǉ��J�n
    Private m_strMyNumberKB_Param As String                         ' ���ʔԍ��擾�敪
    Private m_strMyNumberChokkinSearchKB_Param As String            ' ���ʔԍ����ߌ����敪
    '*����ԍ� 000033 2014/04/28 �ǉ��I��

    '*����ԍ� 000036 2020/01/10 �ǉ��J�n
    Private m_cKensakuShimeiB As ABKensakuShimeiBClass              ' ���������ҏW�r�W�l�X�N���X
    '*����ԍ� 000036 2020/01/10 �ǉ��I��

    '*����ԍ� 000038 2023/08/14 �ǉ��J�n
    Private m_csAtenaRirekiHyojunB As ABAtenaRireki_HyojunBClass            '��������_�W���}�X�^B�׽
    Private m_csAtenaRirekiFZYHyojunB As ABAtenaRirekiFZY_HyojunBClass      '��������t��_�W���}�X�^B�׽
    '*����ԍ� 000038 2023/08/14 �ǉ��I��

    Public m_intHyojunKB As ABEnumDefine.HyojunKB                   '����GET�W�����敪

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaRirekiBClass"                 ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h

    Private Const JUKIHOKAISEIKB_ON As String = "1"

#End Region

    '*����ԍ� 000030 2010/05/14 �ǉ��J�n
#Region "�v���p�e�B"
    Public WriteOnly Property p_strHonsekiHittoshKB() As String     ' �{�ЕM���Ҏ擾�敪
        Set(ByVal Value As String)
            m_strHonsekiHittoshKB_Param = Value
        End Set
    End Property
    Public WriteOnly Property p_strShoriteishiKB() As String        ' ������~�敪�擾�敪
        Set(ByVal Value As String)
            m_strShoriTeishiKB_Param = Value
        End Set
    End Property

    '*����ԍ� 000031 2011/05/18 �ǉ��J�n
    Public WriteOnly Property p_strFrnZairyuJohoKB() As String      ' �O���l�ݗ����i���擾�敪
        Set(ByVal Value As String)
            m_strFrnZairyuJohoKB_Param = Value
        End Set
    End Property
    '*����ԍ� 000031 2011/05/18 �ǉ��I��

    '*����ԍ� 000032 2011/10/24 �ǉ��J�n
    Public WriteOnly Property p_strJukihoKaiseiKB() As String      ' �Z��@�����敪
        Set(ByVal Value As String)
            m_strJukihoKaiseiKB = Value
        End Set
    End Property
    '*����ԍ� 000032 2011/10/24 �ǉ��I��

    '*����ԍ� 000033 2014/04/28 �ǉ��J�n
    Public Property p_strMyNumberKB() As String                     ' ���ʔԍ��擾�敪
        Get
            Return m_strMyNumberKB_Param
        End Get
        Set(ByVal value As String)
            m_strMyNumberKB_Param = value
        End Set
    End Property
    '*����ԍ� 000033 2014/04/28 �ǉ��I��

#End Region
    '*����ԍ� 000030 2010/05/14 �ǉ��I��

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
        '* ����ԍ� 000022 2005/11/18 �ǉ��J�n
        m_strDelFromJuminCDSQL = String.Empty
        '* ����ԍ� 000022 2005/11/18 �ǉ��I��
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing
        '* ����ԍ� 000022 2005/11/18 �ǉ��J�n
        m_cfDelFromJuminCDPrmCollection = Nothing
        '* ����ԍ� 000022 2005/11/18 �ǉ��I��

        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
        '�Z��@�����敪������
        m_strJukihoKaiseiKB = String.Empty
        '�Z��@�����׸ގ擾
        Call GetJukihoKaiseiFG()
        '*����ԍ� 000032 2011/10/24 �ǉ��I��

        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
        ' ���ʔԍ��擾�敪������
        m_strMyNumberKB_Param = String.Empty
        ' ���ʔԍ��@�����擾�@���ߌ����敪�擾
        Me.GetMyNumberChokkinSearchKB()
        '*����ԍ� 000033 2014/04/28 �ǉ��I��

        '*����ԍ� 000036 2020/01/10 �ǉ��J�n
        ' ���������ҏW�r�W�l�X�N���X�̃C���X�^���X��
        m_cKensakuShimeiB = New ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass)
        '*����ԍ� 000036 2020/01/10 �ǉ��I��

    End Sub
    '* ����ԍ� 000019 2005/01/25 �ǉ��J�n�i�{��j
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass, 
    '* �@�@                          ByVal cfRdbClass As UFRdbClass)
    '* �@�@                          ByVal blnSelectAll As Boolean, _
    '* �@�@                          ByVal blnAtenaGet As Boolean)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* �@�@           cfRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* �@�@           blnSelectAll As Boolean                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* �@�@           blnAtenaGet As Boolean                 : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '*                �t���O�̑g�ݍ��킹����
    '*                            blnSelectAll binAtenaGet
    '*                              True         True       ����Get��p�̍��ڂ�S�Ď擾�i��[�l�A���t��A�A���挏�����܂ށj
    '*                              True         False      �������ڂ�S�ēǂݍ��ށi���s�̓ǂݕ��j�i��[�l�A���t��A�A���挏�����܂܂Ȃ��j�i�f�t�H���g�ݒ�j
    '*                              False        True       ����Get��p�̍��ڂŊȈՓI�ȍ��ڂ̂݁i��[�l�A���t��A�A���挏�����܂ށj
    '*                              False        False      �ȈՓI�ȍ��ڂ̂݁i��[�l�A���t��A�A���挏�����܂܂Ȃ��j
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass, _
                   ByVal blnSelectAll As ABEnumDefine.AtenaGetKB, _
                   ByVal blnSelectCount As Boolean)
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
        '* ����ԍ� 000022 2005/11/18 �ǉ��J�n
        m_strDelFromJuminCDSQL = String.Empty
        '* ����ԍ� 000022 2005/11/18 �ǉ��I��
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing
        '* ����ԍ� 000022 2005/11/18 �ǉ��J�n
        m_cfDelFromJuminCDPrmCollection = Nothing
        '* ����ԍ� 000022 2005/11/18 �ǉ��I��
        m_blnSelectAll = blnSelectAll
        m_blnSelectCount = blnSelectCount

        '*����ԍ� 000030 2010/05/14 �ǉ��J�n
        '�Ǘ����擾����
        Call GetKanriJoho()
        '*����ԍ� 000030 2010/05/14 �ǉ��I��

        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
        '�Z��@�����敪������
        m_strJukihoKaiseiKB = String.Empty

        '�Z��@�����׸ގ擾
        Call GetJukihoKaiseiFG()
        '*����ԍ� 000032 2011/10/24 �ǉ��I��

        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
        ' ���ʔԍ��擾�敪������
        m_strMyNumberKB_Param = String.Empty
        ' ���ʔԍ��@�����擾�@���ߌ����敪�擾
        Me.GetMyNumberChokkinSearchKB()
        '*����ԍ� 000033 2014/04/28 �ǉ��I��

        '*����ԍ� 000036 2020/01/10 �ǉ��J�n
        ' ���������ҏW�r�W�l�X�N���X�̃C���X�^���X��
        m_cKensakuShimeiB = New ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass)
        '*����ԍ� 000036 2020/01/10 �ǉ��I��

    End Sub
    '* ����ԍ� 000019 2005/01/25 �ǉ��I���i�{��j
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           intGetCount   : �擾����
    '*                  cSearchKey    : ���������}�X�^�����L�[
    '*                  strKikanYMD   : ���ԔN����
    '* 
    '* �߂�l         DataSet : �擾�������������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
                                              ByVal cSearchKey As ABAtenaSearchKey, _
                                              ByVal strKikanYMD As String) As DataSet
        Return GetAtenaRBHoshu(intGetCount, cSearchKey, strKikanYMD, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String, _
    '*                                                ByVal blnSakujoKB As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@���������}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           intGetCount   : �擾����
    '* �@�@           cSearchKey    : ���������}�X�^�����L�[
    '* �@�@           strKikanYMD   : ���ԔN����
    '* �@�@           blnSakujoKB   : �폜�敪
    '* 
    '* �߂�l         DataSet : �擾�������������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
                                              ByVal cSearchKey As ABAtenaSearchKey, _
                                              ByVal strKikanYMD As String, _
                                              ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaRBHoshu"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaRirekiEntity As DataSet                  '���������f�[�^�Z�b�g
        Dim strSQL As New StringBuilder()
        Dim strWHERE As String
        Dim strORDER As StringBuilder
        Dim intMaxRows As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            ' �����̃`�F�b�N���s�Ȃ�

            ' �擾�����̃`�F�b�N
            If intGetCount < 0 Or intGetCount > 999 Then                '�擾�����̌��
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002001)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If


            ' �����N�����̃`�F�b�N
            If Not ((strKikanYMD = "99999999") Or (strKikanYMD = String.Empty)) Then
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                m_cfDateClass.p_strDateValue = strKikanYMD
                If (Not m_cfDateClass.CheckDate()) Then
                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_KIKANYMD)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                End If
            End If

            '���������L�[�̃`�F�b�N
            '�Ȃ�

            ' SQL���̍쐬
            '* �C���ԍ� 000010 2003/06/12 �C���J�n
            'If intGetCount = 0 Then
            '    strSQL = "SELECT TOP 100 * FROM " + ABAtenaRirekiEntity.TABLE_NAME
            'Else
            '    strSQL = "SELECT TOP " + intGetCount.ToString + " * FROM " + ABAtenaRirekiEntity.TABLE_NAME
            'End If

            ' p_intMaxRows��ޔ�����
            intMaxRows = m_cfRdbClass.p_intMaxRows
            If intGetCount = 0 Then
                m_cfRdbClass.p_intMaxRows = 100
            Else
                m_cfRdbClass.p_intMaxRows = intGetCount
            End If
            '*����ԍ� 000011 2003/08/28 �C���J�n
            'strSQL = "SELECT * FROM " + ABAtenaRirekiEntity.TABLE_NAME

            '* ����ԍ� 000019 2005/01/25 �X�V�J�n�i�{��j
            'strSQL.Append("SELECT * FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)
            Select Case (Me.m_blnSelectAll)
                Case ABEnumDefine.AtenaGetKB.KaniAll
                    If (m_strAtenaSQLsbKaniAll.RLength = 0) Then
                        m_strAtenaSQLsbKaniAll.Append("SELECT ")
                        Call SetAtenaEntity(m_strAtenaSQLsbKaniAll)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbKaniAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strAtenaSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunEntity(m_strAtenaSQLsbKaniAll)
                            Call SetFZYHyojunEntity(m_strAtenaSQLsbKaniAll)
                            Call SetFugenjuEntity(m_strAtenaSQLsbKaniAll)
                            Call SetDenshiShomeishoMSTEntity(m_strAtenaSQLsbKaniAll)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                                Call SetMyNumberHyojunEntity(m_strAtenaSQLsbKaniAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                        m_strAtenaSQLsbKaniAll.Append(" FROM ")
                        m_strAtenaSQLsbKaniAll.Append(ABAtenaRirekiEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbKaniAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strAtenaSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunJoin(m_strAtenaSQLsbKaniAll)
                            Call SetFZYHyojunJoin(m_strAtenaSQLsbKaniAll)
                            Call SetFugenjuJoin(m_strAtenaSQLsbKaniAll)
                            Call SetDenshiShomeishoMSTJoin(m_strAtenaSQLsbKaniAll)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                                OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                                Call SetMyNumberHyojunJoin(m_strAtenaSQLsbKaniAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                    End If
                    strSQL.Append(m_strAtenaSQLsbKaniAll)
                    If (m_csDataSchmaKaniAll Is Nothing) Then
                        m_csDataSchmaKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaKaniAll
                Case ABEnumDefine.AtenaGetKB.KaniOnly
                    If (m_strAtenaSQLsbKaniOnly.RLength = 0) Then
                        m_strAtenaSQLsbKaniOnly.Append("SELECT ")
                        Call SetAtenaEntity(m_strAtenaSQLsbKaniOnly)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbKaniOnly)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strAtenaSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunEntity(m_strAtenaSQLsbKaniOnly)
                            Call SetFZYHyojunEntity(m_strAtenaSQLsbKaniOnly)
                            Call SetFugenjuEntity(m_strAtenaSQLsbKaniOnly)
                            Call SetDenshiShomeishoMSTEntity(m_strAtenaSQLsbKaniOnly)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                                Call SetMyNumberHyojunEntity(m_strAtenaSQLsbKaniOnly)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                        m_strAtenaSQLsbKaniOnly.Append(" FROM ")
                        m_strAtenaSQLsbKaniOnly.Append(ABAtenaRirekiEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbKaniOnly)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strAtenaSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunJoin(m_strAtenaSQLsbKaniOnly)
                            Call SetFZYHyojunJoin(m_strAtenaSQLsbKaniOnly)
                            Call SetFugenjuJoin(m_strAtenaSQLsbKaniOnly)
                            Call SetDenshiShomeishoMSTJoin(m_strAtenaSQLsbKaniOnly)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                                OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                                Call SetMyNumberHyojunJoin(m_strAtenaSQLsbKaniOnly)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                    End If
                    strSQL.Append(m_strAtenaSQLsbKaniOnly)
                    If (m_csDataSchmaKaniOnly Is Nothing) Then
                        m_csDataSchmaKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaKaniOnly
                Case ABEnumDefine.AtenaGetKB.NenkinAll
                    If (m_strAtenaSQLsbNenkinAll.RLength = 0) Then
                        m_strAtenaSQLsbNenkinAll.Append("SELECT ")
                        Call SetAtenaEntity(m_strAtenaSQLsbNenkinAll)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbNenkinAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strAtenaSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunEntity(m_strAtenaSQLsbNenkinAll)
                            Call SetFZYHyojunEntity(m_strAtenaSQLsbNenkinAll)
                            Call SetFugenjuEntity(m_strAtenaSQLsbNenkinAll)
                            Call SetDenshiShomeishoMSTEntity(m_strAtenaSQLsbNenkinAll)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                                Call SetMyNumberHyojunEntity(m_strAtenaSQLsbNenkinAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                        m_strAtenaSQLsbNenkinAll.Append(" FROM ")
                        m_strAtenaSQLsbNenkinAll.Append(ABAtenaRirekiEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbNenkinAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strAtenaSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunJoin(m_strAtenaSQLsbNenkinAll)
                            Call SetFZYHyojunJoin(m_strAtenaSQLsbNenkinAll)
                            Call SetFugenjuJoin(m_strAtenaSQLsbNenkinAll)
                            Call SetDenshiShomeishoMSTJoin(m_strAtenaSQLsbNenkinAll)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                                OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                                Call SetMyNumberHyojunJoin(m_strAtenaSQLsbNenkinAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                    End If
                    strSQL.Append(m_strAtenaSQLsbNenkinAll)
                    If (m_csDataSchmaNenkinAll Is Nothing) Then
                        m_csDataSchmaNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaNenkinAll
                Case Else
                    If (m_strAtenaSQLsbAll.RLength = 0) Then
                        m_strAtenaSQLsbAll.Append("SELECT ")
                        '���s
                        m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strAtenaSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunEntity(m_strAtenaSQLsbAll)
                            Call SetFZYHyojunEntity(m_strAtenaSQLsbAll)
                            If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo) Then
                                Call SetFugenjuEntity(m_strAtenaSQLsbAll)
                                Call SetDenshiShomeishoMSTEntity(m_strAtenaSQLsbAll)
                            Else
                            End If
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                                Call SetMyNumberHyojunEntity(m_strAtenaSQLsbAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                        m_strAtenaSQLsbAll.Append(" FROM ")
                        m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strAtenaSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunJoin(m_strAtenaSQLsbAll)
                            Call SetFZYHyojunJoin(m_strAtenaSQLsbAll)
                            If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo) Then
                                Call SetFugenjuJoin(m_strAtenaSQLsbAll)
                                Call SetDenshiShomeishoMSTJoin(m_strAtenaSQLsbAll)
                            Else
                            End If
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                                OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                                Call SetMyNumberHyojunJoin(m_strAtenaSQLsbAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                    End If
                    strSQL.Append(m_strAtenaSQLsbAll)
                    If (m_csDataSchmaAll Is Nothing) Then
                        m_csDataSchmaAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaAll
            End Select
            'If (m_strAtenaSQLsb.Length = 0) Then
            '    m_strAtenaSQLsb.Append("SELECT ")
            '    Select Case (Me.m_blnSelectAll)
            '        Case ABEnumDefine.AtenaGetKB.SelectAll
            '            '���s
            '            m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
            '        Case ABEnumDefine.AtenaGetKB.KaniAll
            '            Call SetAtenaEntity(m_strAtenaSQLsb)
            '        Case ABEnumDefine.AtenaGetKB.KaniOnly
            '            Call SetAtenaEntity(m_strAtenaSQLsb)
            '        Case Else
            '            '���s
            '            m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
            '    End Select

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaCountEntity(m_strAtenaSQLsb)

            '    m_strAtenaSQLsb.Append(" FROM ")
            '    m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME)

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaJoin(m_strAtenaSQLsb)
            'End If
            'strSQL.Append(m_strAtenaSQLsb)
            ''* ����ԍ� 000019 2005/01/25 �X�V�I���i�{��j

            ''* ����ԍ� 000014 2004/08/27 �ǉ��J�n�i�{��j
            'If (m_csDataSchma Is Nothing) Then
            '    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, False)
            'End If
            '* ����ԍ� 000014 2004/08/27 �ǉ��I��

            '*����ԍ� 000011 2003/08/28 �C���I��
            '* �C���ԍ� 000010 2003/06/12 �C���I��

            'WHERE��̍쐬
            strWHERE = CreateWhere(cSearchKey, strKikanYMD)

            '�폜�t���O
            If blnSakujoFG = False Then
                If Not (strWHERE = String.Empty) Then
                    strWHERE += " AND "
                End If
                strWHERE += ABAtenaRirekiEntity.TABLE_NAME + "." + ABAtenaRirekiEntity.SAKUJOFG + " <> '1'"
            End If

            'ORDER�������
            strORDER = New StringBuilder()
            If (cSearchKey.p_strJuminYuseniKB = "1") And (Not (cSearchKey.p_strStaiCD = String.Empty)) Then
                strORDER.Append(" ORDER BY ")
                strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINHYOHYOJIJUN)
                strORDER.Append(" ASC;")
            Else
                If Not (cSearchKey.p_strUmareYMD = String.Empty) Then
                    strORDER.Append(" ORDER BY ")
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD)
                    strORDER.Append(" ASC,")
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                    strORDER.Append(" ASC;")
                Else
                    strORDER.Append(" ORDER BY ")
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI)
                    strORDER.Append(" ASC,")
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                    strORDER.Append(" ASC;")
                End If
            End If

            '*����ԍ� 000011 2003/08/28 �C���J�n
            'If strWHERE = String.Empty Then
            '    strSQL += strORDER.ToString
            'Else
            '    strSQL += " WHERE " + strWHERE + strORDER.ToString
            'End If

            If Not (strWHERE = String.Empty) Then
                strSQL.Append(" WHERE ").Append(strWHERE)
            End If
            strSQL.Append(strORDER)
            '*����ԍ� 000011 2003/08/28 �C���I��

            '*����ԍ� 000011 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:GetDataSet�z" + _
            '                        "�ySQL���e:" + strSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            ''* ����ԍ� 000019 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            'If (m_blnBatch = False) Then
            '    m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                                "�y���s���\�b�h��:GetDataSet�z" + _
            '                                "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "�z")
            'End If
            ''* ����ԍ� 000019 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
            '*����ԍ� 000011 2003/08/28 �C���I��

            '*����ԍ� 000011 2003/08/28 �C���J�n
            '' SQL�̎��s DataSet�̎擾
            'csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)

            ' SQL�̎��s DataSet�̎擾

            '* ����ԍ� 000019 2005/01/25 �ǉ��J�n�i�{��j
            'csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
            csAtenaRirekiEntity = m_csDataSchma.Clone()
            csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '* ����ԍ� 000019 2005/01/25 �ǉ��I���i�{��j


            '*����ԍ� 000011 2003/08/28 �C���I��

            ' MaxRows�l��߂�
            m_cfRdbClass.p_intMaxRows = intMaxRows

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

    '*����ԍ� 000015 2003/11/18 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                ByVal strKikanYMD As String, _
    '*                                                ByVal strJuminJutogaiKB As String, _
    '*                                                ByVal blnSakujoKB As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@���������}�X�^���Y���f�[�^���擾����i�Z��f�[�^�X�V�p�j
    '* 
    '* ����           intGetCount   : �擾����
    '* �@�@           cSearchKey    : ���������}�X�^�����L�[
    '* �@�@           strKikanYMD   : ���ԔN����
    '* �@�@           strJuminJutogaiKB : �Z���Z�o�O�敪
    '* �@�@           blnSakujoKB   : �폜�敪
    '* 
    '* �߂�l         DataSet : �擾�������������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Friend Overloads Function GetAtenaRBHoshu(ByVal intGetCount As Integer, _
                                              ByVal cSearchKey As ABAtenaSearchKey, _
                                              ByVal strKikanYMD As String, _
                                              ByVal strJuminJutogaiKB As String, _
                                              ByVal blnSakujoFG As Boolean) As DataSet
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaRirekiEntity As DataSet                  '���������f�[�^�Z�b�g
        Dim strSQL As New StringBuilder()
        Dim strWHERE As String
        Dim strORDER As StringBuilder
        Dim intMaxRows As Integer
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            ' �����̃`�F�b�N���s�Ȃ�

            ' �擾�����̃`�F�b�N
            If intGetCount < 0 Or intGetCount > 999 Then                '�擾�����̌��
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002001)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If


            ' �����N�����̃`�F�b�N
            If Not ((strKikanYMD = "99999999") Or (strKikanYMD = String.Empty)) Then
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                m_cfDateClass.p_strDateValue = strKikanYMD
                If (Not m_cfDateClass.CheckDate()) Then
                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_KIKANYMD)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                End If
            End If

            '���������L�[�̃`�F�b�N
            '�Ȃ�

            ' SQL���̍쐬

            ' p_intMaxRows��ޔ�����
            intMaxRows = m_cfRdbClass.p_intMaxRows
            If intGetCount = 0 Then
                m_cfRdbClass.p_intMaxRows = 100
            Else
                m_cfRdbClass.p_intMaxRows = intGetCount
            End If
            '* ����ԍ� 000019 2005/01/25 �X�V�J�n�i�{��j
            'strSQL.Append("SELECT * FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)
            Select Case (Me.m_blnSelectAll)
                Case ABEnumDefine.AtenaGetKB.KaniAll
                    If (m_strAtenaSQLsbKaniAll.RLength = 0) Then
                        m_strAtenaSQLsbKaniAll.Append("SELECT ")
                        Call SetAtenaEntity(m_strAtenaSQLsbKaniAll)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbKaniAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                            Call SetFZYEntity(m_strAtenaSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        m_strAtenaSQLsbKaniAll.Append(" FROM ")
                        m_strAtenaSQLsbKaniAll.Append(ABAtenaRirekiEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbKaniAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                            Call SetFZYJoin(m_strAtenaSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                    End If
                    strSQL.Append(m_strAtenaSQLsbKaniAll)
                    If (m_csDataSchmaKaniAll Is Nothing) Then
                        m_csDataSchmaKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaKaniAll
                Case ABEnumDefine.AtenaGetKB.KaniOnly
                    If (m_strAtenaSQLsbKaniOnly.RLength = 0) Then
                        m_strAtenaSQLsbKaniOnly.Append("SELECT ")
                        Call SetAtenaEntity(m_strAtenaSQLsbKaniOnly)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbKaniOnly)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                            Call SetFZYEntity(m_strAtenaSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        m_strAtenaSQLsbKaniOnly.Append(" FROM ")
                        m_strAtenaSQLsbKaniOnly.Append(ABAtenaRirekiEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbKaniOnly)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                            Call SetFZYJoin(m_strAtenaSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                    End If
                    strSQL.Append(m_strAtenaSQLsbKaniOnly)
                    If (m_csDataSchmaKaniOnly Is Nothing) Then
                        m_csDataSchmaKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaKaniOnly
                Case ABEnumDefine.AtenaGetKB.NenkinAll
                    If (m_strAtenaSQLsbNenkinAll.RLength = 0) Then
                        m_strAtenaSQLsbNenkinAll.Append("SELECT ")
                        Call SetAtenaEntity(m_strAtenaSQLsbNenkinAll)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbNenkinAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                            Call SetFZYEntity(m_strAtenaSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        m_strAtenaSQLsbNenkinAll.Append(" FROM ")
                        m_strAtenaSQLsbNenkinAll.Append(ABAtenaRirekiEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbNenkinAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                            Call SetFZYJoin(m_strAtenaSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                    End If
                    strSQL.Append(m_strAtenaSQLsbNenkinAll)
                    If (m_csDataSchmaNenkinAll Is Nothing) Then
                        m_csDataSchmaNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaNenkinAll
                Case Else
                    If (m_strAtenaSQLsbAll.RLength = 0) Then
                        m_strAtenaSQLsbAll.Append("SELECT ")
                        '���s
                        m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                            Call SetFZYEntity(m_strAtenaSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                        m_strAtenaSQLsbAll.Append(" FROM ")
                        m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                            Call SetFZYJoin(m_strAtenaSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000033 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000033 2014/04/28 �ǉ��I��

                    End If
                    strSQL.Append(m_strAtenaSQLsbAll)
                    If (m_csDataSchmaAll Is Nothing) Then
                        m_csDataSchmaAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaAll
            End Select
            'If (m_strAtenaSQLsb.Length = 0) Then
            '    m_strAtenaSQLsb.Append("SELECT ")
            '    Select Case (Me.m_blnSelectAll)
            '        Case ABEnumDefine.AtenaGetKB.SelectAll
            '            '���s
            '            m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
            '        Case ABEnumDefine.AtenaGetKB.KaniAll
            '            Call SetAtenaEntity(m_strAtenaSQLsb)
            '        Case ABEnumDefine.AtenaGetKB.KaniOnly
            '            Call SetAtenaEntity(m_strAtenaSQLsb)
            '        Case Else
            '            '���s
            '            m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
            '    End Select

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaCountEntity(m_strAtenaSQLsb)

            '    m_strAtenaSQLsb.Append(" FROM ")
            '    m_strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME)

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaJoin(m_strAtenaSQLsb)
            'End If
            'strSQL.Append(m_strAtenaSQLsb)
            'If (m_csDataSchma Is Nothing) Then
            '    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, False)
            'End If
            '* ����ԍ� 000019 2005/01/25 �X�V�I���i�{��j

            'WHERE��̍쐬
            strWHERE = CreateWhere(cSearchKey, strKikanYMD)

            ' �Z���Z�o�O�敪
            If (strJuminJutogaiKB.Trim <> String.Empty) Then
                If Not (strWHERE = String.Empty) Then
                    strWHERE += " AND "
                End If
                strWHERE += ABAtenaRirekiEntity.TABLE_NAME + "." + ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = "
                strWHERE += ABAtenaRirekiEntity.PARAM_JUMINJUTOGAIKB

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUMINJUTOGAIKB
                cfUFParameterClass.Value = strJuminJutogaiKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�폜�t���O
            If blnSakujoFG = False Then
                If Not (strWHERE = String.Empty) Then
                    strWHERE += " AND "
                End If
                strWHERE += ABAtenaRirekiEntity.TABLE_NAME + "." + ABAtenaRirekiEntity.SAKUJOFG + " <> '1'"
            End If

            'ORDER�������
            strORDER = New StringBuilder()
            strORDER.Append(" ORDER BY ")
            strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RIREKINO)
            strORDER.Append(" DESC;")

            If Not (strWHERE = String.Empty) Then
                strSQL.Append(" WHERE ").Append(strWHERE)
            End If
            strSQL.Append(strORDER)

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            ''* ����ԍ� 000019 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            'If (m_blnBatch = False) Then
            '    m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                                "�y���s���\�b�h��:GetDataSet�z" + _
            '                                "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "�z")
            'End If
            ''* ����ԍ� 000019 2005/01/25 �X�V�I���i�{��jIf ���ň͂�

            ' SQL�̎��s DataSet�̎擾
            '* ����ԍ� 000019 2005/01/25 �X�V�J�n�i�{��j
            'csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
            csAtenaRirekiEntity = m_csDataSchma.Clone()
            csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '* ����ԍ� 000019 2005/01/25 �X�V�I���i�{��j
            ' MaxRows�l��߂�
            m_cfRdbClass.p_intMaxRows = intMaxRows

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csAtenaRirekiEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����ʗ����f�[�^���o
    '* 
    '* �\��           Friend Function GetAtenaRBKobetsu(ByVal intGetCount As Integer, _
    '*                                                  ByVal cSearchKey As ABAtenaSearchKey, _
    '*                                                  ByVal strKikanYMD As String, _
    '*                                                  ByVal blnSakujoKB As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@���������}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           intGetCount   : �擾����
    '* �@�@           cSearchKey    : ���������}�X�^�����L�[
    '* �@�@           strKikanYMD   : ���ԔN����
    '* �@�@           blnSakujoKB   : �폜�敪
    '* 
    '* �߂�l         DataSet : �擾�������������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    '*����ԍ� 000028 2008/01/17 �C���J�n
    'Friend Function GetAtenaRBKobetsu(ByVal intGetCount As Integer, _
    '                                  ByVal cSearchKey As ABAtenaSearchKey, _
    '                                  ByVal strKikanYMD As String, _
    '                                  ByVal blnSakujoFG As Boolean) As DataSet
    Friend Function GetAtenaRBKobetsu(ByVal intGetCount As Integer, _
                                      ByVal cSearchKey As ABAtenaSearchKey, _
                                      ByVal strKikanYMD As String, _
                                      ByVal blnSakujoFG As Boolean, _
                                      ByVal strKobetsuKB As String) As DataSet
        '*����ԍ� 000028 2008/01/17 �C���I��
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaRirekiEntity As DataSet                  '���������f�[�^�Z�b�g
        Dim strSQL As New StringBuilder
        Dim strWHERE As StringBuilder
        Dim strORDER As StringBuilder
        Dim intMaxRows As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            ' �����̃`�F�b�N���s�Ȃ�

            ' �擾�����̃`�F�b�N
            If intGetCount < 0 Or intGetCount > 999 Then                '�擾�����̌��
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002001)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If


            ' �����N�����̃`�F�b�N
            If Not ((strKikanYMD = "99999999") Or (strKikanYMD = String.Empty)) Then
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                m_cfDateClass.p_strDateValue = strKikanYMD
                If (Not m_cfDateClass.CheckDate()) Then
                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_KIKANYMD)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                End If
            End If

            '*����ԍ� 000028 2008/01/17 �ǉ��J�n
            ' �ʎ����擾�敪�������o�ϐ��ɃZ�b�g
            m_strKobetsuShutokuKB = strKobetsuKB.Trim
            '*����ԍ� 000028 2008/01/17 �ǉ��I��

            '���������L�[�̃`�F�b�N
            '�Ȃ�

            ' SQL���̍쐬

            ' p_intMaxRows��ޔ�����
            intMaxRows = m_cfRdbClass.p_intMaxRows
            If intGetCount = 0 Then
                m_cfRdbClass.p_intMaxRows = 100
            Else
                m_cfRdbClass.p_intMaxRows = intGetCount
            End If
            ' SELECT ABATENA.*
            '* ����ԍ� 000019 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
            'strSQL.Append("SELECT ").Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
            '' , ABATENANENKIN.KSNENKNNO AS KSNENKNNO
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.KSNENKNNO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KSNENKNNO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKSHU)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO1)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO1)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO1)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO1)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU1)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU1)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN1)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN1)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB1)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB1)

            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO2)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO2)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO2)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO2)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU2)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU2)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN2)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN2)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB2)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB2)

            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO3)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO3)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO3)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO3)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU3)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU3)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN3)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN3)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB3)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB3)

            '' ����
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHONO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHONO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKB)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKB)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO)

            '' ���
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANNO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANNO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANTOROKUKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANTOROKUKB)

            '' �I��
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB)

            '' �����蓖
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEHIYOKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATESTYM)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATESTYM)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEEDYM)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEEDYM)

            '' ���
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.HIHKNSHANO)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGHIHKNSHANO)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSHUTKYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSSHTSYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUSHOCHITKRIKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUSHAKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.KAIGSKAKKB)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKKB)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEIKAISHIYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEISHURYOYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEIYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD)
            'strSQL.Append(", ")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD)
            'strSQL.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD)
            ''  FROM ABATENA 
            'strSQL.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)

            '' LEFT OUTER JOIN ABATENANENKIN ON ABATENA.JUMINCD=ABATENANENKIN.JUMINCD
            'strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaNenkinEntity.TABLE_NAME).Append(" ON ")
            'strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            'strSQL.Append("=")
            'strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JUMINCD)

            '' LEFT OUTER JOIN ABATENAKOKUHO ON ABATENA.JUMINCD=ABATENAKOKUHO.JUMINCD
            'strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(" ON ")
            'strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            'strSQL.Append("=")
            'strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.JUMINCD)

            '' LEFT OUTER JOIN ABATENAINKAN ON ABATENA.JUMINCD=ABATENAINKAN.JUMINCD
            'strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaInkanEntity.TABLE_NAME).Append(" ON ")
            'strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            'strSQL.Append("=")
            'strSQL.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.JUMINCD)

            '' LEFT OUTER JOIN ABATENASENKYO ON ABATENA.JUMINCD=ABATENASENKYO.JUMINCD
            'strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(" ON ")
            'strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            'strSQL.Append("=")
            'strSQL.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.JUMINCD)

            '' LEFT OUTER JOIN ABATENAJITE ON ABATENA.JUMINCD=ABATENAJIDOUTE.JUMINCD
            'strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaJiteEntity.TABLE_NAME).Append(" ON ")
            'strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            'strSQL.Append("=")
            'strSQL.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JUMINCD)

            '' LEFT OUTER JOIN ABATENAKAIGO ON ABATENA.JUMINCD=ABATENAKAIGO.JUMINCD
            'strSQL.Append(" LEFT OUTER JOIN ").Append(ABAtenaKaigoEntity.TABLE_NAME).Append(" ON ")
            'strSQL.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            'strSQL.Append("=")
            'strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUMINCD)
            Select Case (Me.m_blnSelectAll)
                Case ABEnumDefine.AtenaGetKB.KaniAll
                    If (m_strKobetuSQLsbKaniAll.RLength = 0) Then
                        m_strKobetuSQLsbKaniAll.Append("SELECT ")
                        Call SetAtenaEntity(m_strKobetuSQLsbKaniAll)
                        '�ʎ����̍��ڃZ�b�g
                        Call SetKobetsuEntity(m_strKobetuSQLsbKaniAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strKobetuSQLsbKaniAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strKobetuSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000034 2014/06/05 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strKobetuSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000034 2014/06/05 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunEntity(m_strKobetuSQLsbKaniAll)
                            Call SetFZYHyojunEntity(m_strKobetuSQLsbKaniAll)
                            Call SetFugenjuEntity(m_strKobetuSQLsbKaniAll)
                            Call SetDenshiShomeishoMSTEntity(m_strKobetuSQLsbKaniAll)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                                Call SetMyNumberHyojunEntity(m_strKobetuSQLsbKaniAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                        '  FROM ABATENA 
                        m_strKobetuSQLsbKaniAll.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)
                        '�ʎ�����JOIN����쐬
                        Call SetKobetsuJoin(m_strKobetuSQLsbKaniAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strKobetuSQLsbKaniAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strKobetuSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000034 2014/06/05 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strKobetuSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000034 2014/06/05 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunJoin(m_strKobetuSQLsbKaniAll)
                            Call SetFZYHyojunJoin(m_strKobetuSQLsbKaniAll)
                            Call SetFugenjuJoin(m_strKobetuSQLsbKaniAll)
                            Call SetDenshiShomeishoMSTJoin(m_strKobetuSQLsbKaniAll)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                                OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                                Call SetMyNumberHyojunJoin(m_strKobetuSQLsbKaniAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                    End If
                    strSQL.Append(m_strKobetuSQLsbKaniAll)
                    If (m_csDataSchmaKobetuKaniAll Is Nothing) Then
                        m_csDataSchmaKobetuKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchmaKobetu = m_csDataSchmaKobetuKaniAll
                Case ABEnumDefine.AtenaGetKB.KaniOnly
                    If (m_strKobetuSQLsbKaniOnly.RLength = 0) Then
                        m_strKobetuSQLsbKaniOnly.Append("SELECT ")
                        Call SetAtenaEntity(m_strKobetuSQLsbKaniOnly)
                        '�ʎ����̍��ڃZ�b�g
                        Call SetKobetsuEntity(m_strKobetuSQLsbKaniOnly)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strKobetuSQLsbKaniOnly)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strKobetuSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000034 2014/06/05 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strKobetuSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000034 2014/06/05 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunEntity(m_strKobetuSQLsbKaniOnly)
                            Call SetFZYHyojunEntity(m_strKobetuSQLsbKaniOnly)
                            Call SetFugenjuEntity(m_strKobetuSQLsbKaniOnly)
                            Call SetDenshiShomeishoMSTEntity(m_strKobetuSQLsbKaniOnly)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                                Call SetMyNumberHyojunEntity(m_strKobetuSQLsbKaniOnly)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                        '  FROM ABATENA 
                        m_strKobetuSQLsbKaniOnly.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)
                        '�ʎ�����JOIN����쐬
                        Call SetKobetsuJoin(m_strKobetuSQLsbKaniOnly)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strKobetuSQLsbKaniOnly)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strKobetuSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000034 2014/06/05 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strKobetuSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000034 2014/06/05 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunJoin(m_strKobetuSQLsbKaniOnly)
                            Call SetFZYHyojunJoin(m_strKobetuSQLsbKaniOnly)
                            Call SetFugenjuJoin(m_strKobetuSQLsbKaniOnly)
                            Call SetDenshiShomeishoMSTJoin(m_strKobetuSQLsbKaniOnly)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                                OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                                Call SetMyNumberHyojunJoin(m_strKobetuSQLsbKaniOnly)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                    End If
                    strSQL.Append(m_strKobetuSQLsbKaniOnly)
                    If (m_csDataSchmaKobetuKaniOnly Is Nothing) Then
                        m_csDataSchmaKobetuKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchmaKobetu = m_csDataSchmaKobetuKaniOnly
                Case ABEnumDefine.AtenaGetKB.NenkinAll
                    If (m_strKobetuSQLsbNenkinAll.RLength = 0) Then
                        m_strKobetuSQLsbNenkinAll.Append("SELECT ")
                        Call SetAtenaEntity(m_strKobetuSQLsbNenkinAll)
                        '�ʎ����̍��ڃZ�b�g
                        Call SetKobetsuEntity(m_strKobetuSQLsbNenkinAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strKobetuSQLsbNenkinAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strKobetuSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000034 2014/06/05 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strKobetuSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000034 2014/06/05 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunEntity(m_strKobetuSQLsbNenkinAll)
                            Call SetFZYHyojunEntity(m_strKobetuSQLsbNenkinAll)
                            Call SetFugenjuEntity(m_strKobetuSQLsbNenkinAll)
                            Call SetDenshiShomeishoMSTEntity(m_strKobetuSQLsbNenkinAll)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                                Call SetMyNumberHyojunEntity(m_strKobetuSQLsbNenkinAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                        '  FROM ABATENA 
                        m_strKobetuSQLsbNenkinAll.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)
                        '�ʎ�����JOIN����쐬
                        Call SetKobetsuJoin(m_strKobetuSQLsbNenkinAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strKobetuSQLsbNenkinAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strKobetuSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000034 2014/06/05 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strKobetuSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000034 2014/06/05 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunJoin(m_strKobetuSQLsbNenkinAll)
                            Call SetFZYHyojunJoin(m_strKobetuSQLsbNenkinAll)
                            Call SetFugenjuJoin(m_strKobetuSQLsbNenkinAll)
                            Call SetDenshiShomeishoMSTJoin(m_strKobetuSQLsbNenkinAll)
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                                OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                                Call SetMyNumberHyojunJoin(m_strKobetuSQLsbNenkinAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                    End If
                    strSQL.Append(m_strKobetuSQLsbNenkinAll)
                    If (m_csDataSchmaKobetuNenkinAll Is Nothing) Then
                        m_csDataSchmaKobetuNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchmaKobetu = m_csDataSchmaKobetuNenkinAll
                Case Else
                    If (m_strKobetuSQLsbAll.RLength = 0) Then
                        m_strKobetuSQLsbAll.Append("SELECT ")
                        '���s
                        m_strKobetuSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
                        '�ʎ����̍��ڃZ�b�g
                        Call SetKobetsuEntity(m_strKobetuSQLsbAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strKobetuSQLsbAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strKobetuSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000034 2014/06/05 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strKobetuSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000034 2014/06/05 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunEntity(m_strKobetuSQLsbAll)
                            Call SetFZYHyojunEntity(m_strKobetuSQLsbAll)
                            If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo) Then
                                Call SetFugenjuEntity(m_strKobetuSQLsbAll)
                                Call SetDenshiShomeishoMSTEntity(m_strKobetuSQLsbAll)
                            Else
                            End If
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                                Call SetMyNumberHyojunEntity(m_strKobetuSQLsbAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                        '  FROM ABATENA 
                        m_strKobetuSQLsbAll.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)
                        '�ʎ�����JOIN����쐬
                        Call SetKobetsuJoin(m_strKobetuSQLsbAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strKobetuSQLsbAll)

                        '*����ԍ� 000032 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈�������t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strKobetuSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000032 2011/10/24 �ǉ��I��

                        '*����ԍ� 000034 2014/06/05 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strKobetuSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000034 2014/06/05 �ǉ��I��

                        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetHyojunJoin(m_strKobetuSQLsbAll)
                            Call SetFZYHyojunJoin(m_strKobetuSQLsbAll)
                            If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo) Then
                                Call SetFugenjuJoin(m_strKobetuSQLsbAll)
                                Call SetDenshiShomeishoMSTJoin(m_strKobetuSQLsbAll)
                            Else
                            End If
                            If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                                OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                                Call SetMyNumberHyojunJoin(m_strKobetuSQLsbAll)
                            Else
                            End If
                        Else
                            '�����Ȃ�
                        End If

                    End If
                    strSQL.Append(m_strKobetuSQLsbAll)
                    If (m_csDataSchmaKobetuAll Is Nothing) Then
                        m_csDataSchmaKobetuAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchmaKobetu = m_csDataSchmaKobetuAll
            End Select
            'If (m_strKobetuSQLsb.Length = 0) Then
            '    m_strKobetuSQLsb.Append("SELECT ")
            '    Select Case (Me.m_blnSelectAll)
            '        Case ABEnumDefine.AtenaGetKB.SelectAll
            '            '���s
            '            m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
            '        Case ABEnumDefine.AtenaGetKB.KaniAll
            '            Call SetAtenaEntity(m_strKobetuSQLsb)
            '        Case ABEnumDefine.AtenaGetKB.KaniOnly
            '            Call SetAtenaEntity(m_strKobetuSQLsb)
            '        Case Else
            '            '���s
            '            m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".*")
            '    End Select
            '    ' , ABATENANENKIN.KSNENKNNO AS KSNENKNNO
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.KSNENKNNO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KSNENKNNO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKSHU)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO1)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO1)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO1)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO1)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU1)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU1)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN1)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN1)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB1)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB1)

            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO2)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO2)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO2)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO2)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU2)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU2)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN2)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN2)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB2)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB2)

            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO3)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO3)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO3)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO3)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU3)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU3)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN3)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN3)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB3)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB3)

            '    ' ����
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHONO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHONO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKB)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKB)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO)

            '    ' ���
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANNO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANNO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANTOROKUKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANTOROKUKB)

            '    ' �I��
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB)

            '    ' �����蓖
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEHIYOKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATESTYM)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATESTYM)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEEDYM)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEEDYM)

            '    ' ���
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.HIHKNSHANO)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGHIHKNSHANO)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSHUTKYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSSHTSYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUSHOCHITKRIKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUSHAKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.KAIGSKAKKB)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKKB)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEIKAISHIYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEISHURYOYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEIYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD)
            '    m_strKobetuSQLsb.Append(", ")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD)
            '    m_strKobetuSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD)

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaCountEntity(m_strKobetuSQLsb)

            '    '  FROM ABATENA 
            '    m_strKobetuSQLsb.Append(" FROM ").Append(ABAtenaRirekiEntity.TABLE_NAME)

            '    ' LEFT OUTER JOIN ABATENANENKIN ON ABATENA.JUMINCD=ABATENANENKIN.JUMINCD
            '    m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaNenkinEntity.TABLE_NAME).Append(" ON ")
            '    m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            '    m_strKobetuSQLsb.Append("=")
            '    m_strKobetuSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JUMINCD)

            '    ' LEFT OUTER JOIN ABATENAKOKUHO ON ABATENA.JUMINCD=ABATENAKOKUHO.JUMINCD
            '    m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(" ON ")
            '    m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            '    m_strKobetuSQLsb.Append("=")
            '    m_strKobetuSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.JUMINCD)

            '    ' LEFT OUTER JOIN ABATENAINKAN ON ABATENA.JUMINCD=ABATENAINKAN.JUMINCD
            '    m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaInkanEntity.TABLE_NAME).Append(" ON ")
            '    m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            '    m_strKobetuSQLsb.Append("=")
            '    m_strKobetuSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.JUMINCD)

            '    ' LEFT OUTER JOIN ABATENASENKYO ON ABATENA.JUMINCD=ABATENASENKYO.JUMINCD
            '    m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(" ON ")
            '    m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            '    m_strKobetuSQLsb.Append("=")
            '    m_strKobetuSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.JUMINCD)

            '    ' LEFT OUTER JOIN ABATENAJITE ON ABATENA.JUMINCD=ABATENAJIDOUTE.JUMINCD
            '    m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaJiteEntity.TABLE_NAME).Append(" ON ")
            '    m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            '    m_strKobetuSQLsb.Append("=")
            '    m_strKobetuSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JUMINCD)

            '    ' LEFT OUTER JOIN ABATENAKAIGO ON ABATENA.JUMINCD=ABATENAKAIGO.JUMINCD
            '    m_strKobetuSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKaigoEntity.TABLE_NAME).Append(" ON ")
            '    m_strKobetuSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            '    m_strKobetuSQLsb.Append("=")
            '    m_strKobetuSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUMINCD)

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaJoin(m_strKobetuSQLsb)
            'End If
            'strSQL.Append(m_strKobetuSQLsb)
            'If (m_csDataSchmaKobetu Is Nothing) Then
            '    m_csDataSchmaKobetu = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, False)
            'End If
            '* ����ԍ� 000019 2005/01/25 �X�V�I���i�{��jIF���ň͂�

            ' WHERE��̍쐬
            strWHERE = New StringBuilder(CreateWhere(cSearchKey, strKikanYMD))

            ' �폜�t���O
            If blnSakujoFG = False Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                strWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SAKUJOFG)
                strWHERE.Append(" <> '1'")
            End If

            'ORDER�������
            strORDER = New StringBuilder
            If (cSearchKey.p_strJuminYuseniKB = "1") And (Not (cSearchKey.p_strStaiCD = String.Empty)) Then
                strORDER.Append(" ORDER BY ")
                strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINHYOHYOJIJUN)
                strORDER.Append(" ASC;")
            Else
                If Not (cSearchKey.p_strUmareYMD = String.Empty) Then
                    strORDER.Append(" ORDER BY ")
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD)
                    strORDER.Append(" ASC,")
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                    strORDER.Append(" ASC;")
                Else
                    strORDER.Append(" ORDER BY ")
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI)
                    strORDER.Append(" ASC,")
                    strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                    strORDER.Append(" ASC;")
                End If
            End If

            If Not (strWHERE.ToString = String.Empty) Then
                strSQL.Append(" WHERE ").Append(strWHERE)
            End If
            strSQL.Append(strORDER)

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            ''* ����ԍ� 000019 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            'If (m_blnBatch = False) Then
            '    m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                                "�y���s���\�b�h��:GetDataSet�z" + _
            '                                "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "�z")
            'End If
            ''* ����ԍ� 000019 2005/01/25 �X�V�I���i�{��jIf ���ň͂�

            ' SQL�̎��s DataSet�̎擾
            '* ����ԍ� 000019 2005/01/25 �X�V�J�n�i�{��j
            'csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
            csAtenaRirekiEntity = m_csDataSchmaKobetu.Clone()
            csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '* ����ԍ� 000019 2005/01/25 �X�V�I���i�{��j

            ' MaxRows�l��߂�
            m_cfRdbClass.p_intMaxRows = intMaxRows

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csAtenaRirekiEntity

    End Function

    '*����ԍ� 000015 2003/11/18 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@���������}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000029
        'Dim csInstRow As DataRow
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000029
        Dim intInsCnt As Integer        '�ǉ�����
        Dim strUpdateDateTime As String

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000020 2005/06/15 �C���J�n
                'Call CreateSQL(csDataRow)
                Call CreateInsertSQL(csDataRow)
                '* ����ԍ� 000020 2005/06/15 �C���I��
            End If

            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '�쐬����

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRirekiEntity.TANMATSUID) = m_cfControlData.m_strClientId   ' �[���h�c
            csDataRow(ABAtenaRirekiEntity.SAKUJOFG) = "0"                               ' �폜�t���O
            csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER) = Decimal.Zero                 ' �X�V�J�E���^
            csDataRow(ABAtenaRirekiEntity.SAKUSEINICHIJI) = strUpdateDateTime           ' �쐬����
            csDataRow(ABAtenaRirekiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId    ' �쐬���[�U�[
            csDataRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = strUpdateDateTime            ' �X�V����
            csDataRow(ABAtenaRirekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId     ' �X�V���[�U�[


            '' ���N���X�̃f�[�^�������`�F�b�N���s��
            'For Each csDataColumn In csDataRow.Table.Columns
            '    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString().Trim)
            'Next csDataColumn


            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*����ԍ� 000011 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                        "�ySQL���e:" + m_strInsertSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")
            '*����ԍ� 000011 2003/08/28 �C���I��

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
    '*����ԍ� 000032 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaRB() As Integer
    '* 
    '* �@�\�@�@    �@ ���������}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csAtenaDr As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i���������j
    '* �@�@           csAtenaFZYDr As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t���j
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������}�X�^�ǉ������s
            intCnt = Me.InsertAtenaRB(csAtenaDr)

            '�Z��@�����ȍ~�̂Ƃ�
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '��������t���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRirekiFZYB)) Then
                    m_csAtenaRirekiFZYB = New ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�쐬�����A�X�V�����̓���
                csAtenaFZYDr(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRirekiEntity.SAKUSEINICHIJI)
                csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                '�����t���}�X�^�ǉ������s
                intCnt2 = m_csAtenaRirekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr)
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

        Return intCnt

    End Function
    '*����ԍ� 000032 2011/10/24 �ǉ��I��

    '*����ԍ� 000038 2023/08/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
    '*                                              ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ ���������}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csAtenaDr As DataRow          : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i���������j
    '* �@�@           csAtenaHyojunDr As DataRow    : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������_�W���j
    '* �@�@           csAtenaFZYDr As DataRow       : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t���j
    '* �@�@           csAtenaFZYHyojunDr As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t��_�W���j
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
                                  ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0
        Dim intCnt3 As Integer = 0
        Dim intCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������}�X�^�ǉ������s
            intCnt = Me.InsertAtenaRB(csAtenaDr)

            '��������_�W�������݂���ꍇ
            If (Not IsNothing(csAtenaHyojunDr))Then
                '��������_�W���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRirekiHyojunB)) Then
                    m_csAtenaRirekiHyojunB = New ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�쐬�����A�X�V�����̓���
                csAtenaHyojunDr(ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRirekiEntity.SAKUSEINICHIJI)
                csAtenaHyojunDr(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                '��������_�W���}�X�^�ǉ������s
                intCnt2 = m_csAtenaRirekiHyojunB.InsertAtenaRirekiHyojunB(csAtenaHyojunDr)

            End If

            '�Z��@�����ȍ~�̂Ƃ�
            If (m_blnJukihoKaiseiFG) Then

                '��������t�������݂���ꍇ
                If (Not IsNothing(csAtenaFZYDr)) Then
                    '��������t���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRirekiFZYB)) Then
                        m_csAtenaRirekiFZYB = New ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�쐬�����A�X�V�����̓���
                    csAtenaFZYDr(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRirekiEntity.SAKUSEINICHIJI)
                    csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                    '�����t���}�X�^�ǉ������s
                    intCnt3 = m_csAtenaRirekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr)
                End If

                '��������t��_�W�������݂���ꍇ
                If (Not IsNothing(csAtenaFZYHyojunDr)) Then
                    '��������t��_�W���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRirekiFZYHyojunB)) Then
                        m_csAtenaRirekiFZYHyojunB = New ABAtenaRirekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�쐬�����A�X�V�����̓���
                    csAtenaFZYHyojunDr(ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRirekiEntity.SAKUSEINICHIJI)
                    csAtenaFZYHyojunDr(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                    '�����t��_�W���}�X�^�ǉ������s
                    intCnt4 = m_csAtenaRirekiFZYHyojunB.InsertAtenaRirekiFZYHyojunB(csAtenaFZYHyojunDr)
                End If

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

        Return intCnt

    End Function
    '*����ԍ� 000038 2023/08/14 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@���������}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csDataRow As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateAtenaRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaRB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000029
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000029
        Dim intUpdCnt As Integer                            '�X�V����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000020 2005/06/15 �C���J�n
                'Call CreateSQL(csDataRow)
                Call CreateUpdateSQL(csDataRow)
                '* ����ԍ� 000020 2005/06/15 �C���I��
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRirekiEntity.TANMATSUID) = m_cfControlData.m_strClientId                                 '�[���h�c
            csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER)) + 1       '�X�V�J�E���^
            csDataRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff") '�X�V����
            csDataRow(ABAtenaRirekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                   '�X�V���[�U�[

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiEntity.PREFIX_KEY.RLength) = ABAtenaRirekiEntity.PREFIX_KEY) Then
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '*����ԍ� 000016 2004/11/12 �C���J�n
                    '�f�[�^�������`�F�b�N
                    'CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString().Trim)
                    '*����ԍ� 000016 2004/11/12 �C���J�n
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000011 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                        "�ySQL���e:" + m_strUpdateSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")
            '*����ԍ� 000011 2003/08/28 �C���I��

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
    '*����ԍ� 000032 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaRB() As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csAtenaDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i���������j
    '* �@�@           csAtenaFZYDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t���j
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow) As Integer
        Dim intInsCnt As Integer = 0
        Dim intInsCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "UpdateAtenaRB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������}�X�^�X�V�����s
            intInsCnt = Me.UpdateAtenaRB(csAtenaDr)

            '�Z��@�����ȍ~�̂Ƃ�
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '��������t���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRirekiFZYB)) Then
                    m_csAtenaRirekiFZYB = New ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�X�V�����̓���
                csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                '��������t���}�X�^�X�V�����s
                intInsCnt2 = m_csAtenaRirekiFZYB.UpdateAtenaFZYRB(csAtenaFZYDr)
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

        Return intInsCnt

    End Function
    '*����ԍ� 000032 2011/10/24 �ǉ��I��

    '*����ԍ� 000038 2023/08/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaRB(ByVal csAtenaDr As DataRow, _
    '*                                              ByVal csAtenaHyojunDr As DataRow, _
    '*                                              ByVal csAtenaFZYDr As DataRow, _
    '*                                              ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csAtenaDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i���������j
    '* �@�@           csAtenaHyojunDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������_�W���j
    '* �@�@           csAtenaFZYDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t���j
    '* �@�@           csAtenaFZYHyojunDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t��_�W���j
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateAtenaRB(ByVal csAtenaDr As DataRow, _
                                  ByVal csAtenaHyojunDr As DataRow, _
                                  ByVal csAtenaFZYDr As DataRow, _
                                  ByVal csAtenaFZYHyojunDr As DataRow) As Integer

        Dim intInsCnt As Integer = 0
        Dim intInsCnt2 As Integer = 0
        Dim intInsCnt3 As Integer = 0
        Dim intInsCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "UpdateAtenaRB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������}�X�^�X�V�����s
            intInsCnt = Me.UpdateAtenaRB(csAtenaDr)

            '��������_�W���}�X�^�����݂���ꍇ
            If (Not IsNothing(csAtenaHyojunDr)) Then
                '��������_�W���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRirekiHyojunB)) Then
                    m_csAtenaRirekiHyojunB = New ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�X�V�����̓���
                csAtenaHyojunDr(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                '��������_�W���}�X�^�X�V�����s
                intInsCnt2 = m_csAtenaRirekiHyojunB.UpdateAtenaRirekiHyojunB(csAtenaHyojunDr)

            End If

            '�Z��@�����ȍ~�̂Ƃ�
            If (m_blnJukihoKaiseiFG) Then
                '��������t���}�X�^�����݂���ꍇ
                If (Not IsNothing(csAtenaFZYDr)) Then
                    '��������t���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRirekiFZYB)) Then
                        m_csAtenaRirekiFZYB = New ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                    '��������t���}�X�^�X�V�����s
                    intInsCnt3 = m_csAtenaRirekiFZYB.UpdateAtenaFZYRB(csAtenaFZYDr)

                End If

                '��������t��_�W���}�X�^�����݂���ꍇ
                If (Not IsNothing(csAtenaFZYHyojunDr)) Then
                    '��������t��_�W���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRirekiFZYHyojunB)) Then
                        m_csAtenaRirekiFZYHyojunB = New ABAtenaRirekiFZY_HYojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYHyojunDr(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                    '��������t���}�X�^�X�V�����s
                    intInsCnt4 = m_csAtenaRirekiFZYHyojunB.UpdateAtenaRirekiFZYHyojunB(csAtenaFZYHyojunDr)

                End If
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

        Return intInsCnt

    End Function
    '*����ԍ� 000038 2023/08/14 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�폜
    '* 
    '* �\��           Public Function DeleteAtenaRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@���������}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csDataRow As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaRB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000029
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000029
        Dim intDelCnt As Integer                            '�폜����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or _
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000020 2005/06/15 �C���J�n
                'CreateSQL(csDataRow)
                Call CreateDeleteRonriSQL(csDataRow)
                '* ����ԍ� 000020 2005/06/15 �C���I��
            End If


            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRirekiEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABAtenaRirekiEntity.SAKUJOFG) = "1"                                                                 '�폜�t���O
            csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaRirekiEntity.KOSHINCOUNTER)) + 1               '�X�V�J�E���^
            csDataRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '�X�V����
            csDataRow(ABAtenaRirekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[

            '*����ԍ� 000011 2003/08/28 �C���J�n
            ''�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            'For Each cfParam In m_cfUpdateUFParameterCollectionClass
            '    '�L�[���ڂ͍X�V�O�̒l�Őݒ�
            '    If (cfParam.ParameterName.Substring(0, ABAtenaRirekiEntity.PREFIX_KEY.Length) = ABAtenaRirekiEntity.PREFIX_KEY) Then
            '        m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = _
            '                csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PREFIX_KEY.Length), _
            '                          DataRowVersion.Original).ToString()
            '    Else
            '        '�f�[�^�������`�F�b�N
            '        CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString().Trim)
            '        m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString()
            '    End If
            'Next cfParam

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiEntity.PREFIX_KEY.RLength) = ABAtenaRirekiEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '*����ԍ� 000016 2004/11/12 �C���J�n
                    '�f�[�^�������`�F�b�N
                    'CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString().Trim)
                    '*����ԍ� 000016 2004/11/12 �C���I��
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam
            '*����ԍ� 000011 2003/08/28 �C���I��

            '*����ԍ� 000011 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                        "�ySQL���e:" + m_strUpdateSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")
            '*����ԍ� 000011 2003/08/28 �C���I��

            '*����ԍ� 000011 2003/08/28 �C���J�n
            '' SQL�̎��s
            'intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfUpdateUFParameterCollectionClass)

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)
            '*����ԍ� 000011 2003/08/28 �C���I��

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
    '*����ԍ� 000032 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�폜
    '* 
    '* �\��           Public Function UpdateAtenaB() As Integer
    '* 
    '* �@�\�@�@    �@ ���������}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csAtenaDr As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i���������j
    '* �@�@           csAtenaFZYDr As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t���j
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "DeleteAtenaRB"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������}�X�^�X�V�����s
            intCnt = Me.DeleteAtenaRB(csAtenaDr)

            '�Z��@�����ȍ~�̂Ƃ�
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '��������t���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRirekiFZYB)) Then
                    m_csAtenaRirekiFZYB = New ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�X�V�����̓���
                csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                '��������t���}�X�^�폜�����s
                intCnt2 = m_csAtenaRirekiFZYB.DeleteAtenaFZYRB(csAtenaFZYDr)
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

        Return intCnt

    End Function

    '*����ԍ� 000038 2023/08/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�폜
    '* 
    '* �\��           Public Function UpdateAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow, _
    '*                                             ByVal csAtenaHyojunDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    '* 
    '* ����           csAtenaDr As DataRow          : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i���������j
    '* �@�@           csAtenaHyojunDr As DataRow    : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������_�W���j
    '* �@�@           csAtenaFZYDr As DataRow       : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t���j
    '* �@�@           csAtenaFZYHyojunDr As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t��_�W���j
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    '*����ԍ� 000039 2023/10/19 �C���J�n
    'Public Overloads Function DeleteAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow,
    '                                        ByVal csAtenaHyojunDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    Public Overloads Function DeleteAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow,
                                            ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        '*����ԍ� 000039 2023/10/19 �C���I��
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0
        Dim intCnt3 As Integer = 0
        Dim intCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "DeleteAtenaRB"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������}�X�^�X�V�����s
            intCnt = Me.DeleteAtenaRB(csAtenaDr)

            '��������_�W���}�X�^�̃f�[�^�����݂���ꍇ�A�������s��
            If (Not IsNothing(csAtenaHyojunDr)) Then

                '��������_�W���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRirekiHyojunB)) Then
                    m_csAtenaRirekiHyojunB = New ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�X�V�����̓���
                csAtenaHyojunDr(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                '��������_�W���}�X�^�폜�����s
                intCnt2 = m_csAtenaRirekiHyojunB.DeleteAtenaRirekiHyojunB(csAtenaHyojunDr)

            End If

            '�Z��@�����ȍ~�̂Ƃ�
            If (m_blnJukihoKaiseiFG) Then

                '��������t���}�X�^�̃f�[�^�����݂���ꍇ�A�������s��
                If (Not IsNothing(csAtenaFZYDr)) Then

                    '��������t���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRirekiFZYB)) Then
                        m_csAtenaRirekiFZYB = New ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYDr(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                    '��������t���}�X�^�폜�����s
                    intCnt3 = m_csAtenaRirekiFZYB.DeleteAtenaFZYRB(csAtenaFZYDr)

                End If

                '��������t��_�W���}�X�^�̃f�[�^�����݂���ꍇ�A�������s��
                If (Not IsNothing(csAtenaFZYHyojunDr)) Then

                    '��������t��_�W���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRirekiFZYHyojunB)) Then
                        m_csAtenaRirekiFZYHyojunB = New ABAtenaRirekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYHyojunDr(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRirekiEntity.KOSHINNICHIJI)

                    '��������t��_�W���}�X�^�폜�����s
                    intCnt4 = m_csAtenaRirekiFZYHyojunB.DeleteAtenaRirekiFZYHyojunB(csAtenaFZYHyojunDr)

                End If

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

        Return intCnt

    End Function
    '*����ԍ� 000038 2023/08/14 �ǉ��I��
    '*����ԍ� 000032 2011/10/24 �ǉ��I��
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�����폜
    '* 
    '* �\��           Public Function DeleteAtenaRB(ByVal csDataRow As DataRow, _
    '*                                              ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@�@���������}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csDataRow As DataRow : �폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaRB(ByVal csDataRow As DataRow, _
                                            ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaRB"
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        Dim cfParam As UFParameterClass                     ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000029
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000029
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

            End If

            ' �폜�p�̃p�����[�^�tDELETE��������ƃp�����[�^�R���N�V�������쐬����
            If (m_strDelButuriSQL Is Nothing Or m_strDelButuriSQL = String.Empty Or _
                    IsNothing(m_cfDelButuriUFParameterCollectionClass)) Then
                '* ����ԍ� 000020 2005/06/15 �C���J�n
                'CreateSQL(csDataRow)
                Call CreateDeleteButsuriSQL(csDataRow)
                '* ����ԍ� 000020 2005/06/15 �C���I��
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass

                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaRirekiEntity.PREFIX_KEY.RLength) = ABAtenaRirekiEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRirekiEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                End If
            Next cfParam


            '*����ԍ� 000011 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                        "�ySQL���e:" + m_strUpdateSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "�z")
            '*����ԍ� 000011 2003/08/28 �C���I��

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
    '*����ԍ� 000032 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�����폜
    '* 
    '* �\��           Public Function DeleteAtenaRB() As Integer
    '* 
    '* �@�\�@�@    �@ ���������}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csAtenaDr As DataRow : �����폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i���������j
    '* �@�@           csAtenaFZYDr As DataRow : �����폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t���j
    '*                strSakujoKB As String �F �폜�敪  
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow, ByVal strSakujoKB As String) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "DeleteAtenaB"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������}�X�^�X�V�����s
            intCnt = Me.DeleteAtenaRB(csAtenaDr, strSakujoKB)

            '�Z��@�����ȍ~�̂Ƃ�
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '��������t���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRirekiFZYB)) Then
                    m_csAtenaRirekiFZYB = New ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '��������t���}�X�^�����폜���s
                intCnt2 = m_csAtenaRirekiFZYB.DeleteAtenaFZYRB(csAtenaFZYDr, strSakujoKB)
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

        Return intCnt

    End Function
    '*����ԍ� 000032 2011/10/24 �ǉ��I��

    '*����ԍ� 000038 2023/08/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�����폜
    '* 
    '* �\��           Public Overloads Function DeleteAtenaRB(ByVal csAtenaDr As DataRow, _
    '*                                                        ByVal csAtenaHyojunDr As DataRow, _
    '*                                                        ByVal csAtenaFZYDr As DataRow, _
    '*                                                        ByVal csAtenaFZYHyojunDr As DataRow, _
    '*                                                        ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@ ���������}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csAtenaDr As DataRow          : �����폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i���������j
    '*                csAtenaHyojunDr As DataRow    : �����폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������_�W���j
    '*                csAtenaFZYDr As DataRow       : �����폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t���j
    '*                csAtenaFZYHyojunDr As DataRow : �����폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i��������t��_�W���j
    '*                strSakujoKB As String         : �폜�敪  
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaRB(ByVal csAtenaDr As DataRow, _
                                            ByVal csAtenaHyojunDr As DataRow, _
                                            ByVal csAtenaFZYDr As DataRow, _                                    
                                            ByVal csAtenaFZYHyojunDr As DataRow, _
                                            ByVal strSakujoKB As String) As Integer

        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0
        Dim intCnt3 As Integer = 0
        Dim intCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "DeleteAtenaRB"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������}�X�^�X�V�����s
            intCnt = Me.DeleteAtenaRB(csAtenaDr, strSakujoKB)

            '��������_�W���}�X�^�����݂���΍X�V�����s
            If (Not IsNothing(csAtenaHyojunDr)) Then
                '��������_�W���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRirekiHyojunB)) Then
                    m_csAtenaRirekiHyojunB = New ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If
            
                '��������_�W���}�X�^�����폜���s
                intCnt2 = m_csAtenaRirekiHyojunB.DeleteAtenaHyojunRB(csAtenaHyojunDr, strSakujoKB)
            End If

            '�Z��@�����ȍ~�̂Ƃ�
            If (m_blnJukihoKaiseiFG) Then

                '��������t���}�X�^�����݂���ꍇ�A�X�V����
                If (Not IsNothing(csAtenaFZYDr)) Then
                    '��������t���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRirekiFZYB)) Then
                        m_csAtenaRirekiFZYB = New ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '��������t���}�X�^�����폜���s
                    intCnt3 = m_csAtenaRirekiFZYB.DeleteAtenaFZYRB(csAtenaFZYDr, strSakujoKB)
                End If

                '��������t���}�X�^�����݂���ꍇ�A�X�V����
                If (Not IsNothing(csAtenaFZYHyojunDr)) Then
                    '��������t��_�W���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRirekiFZYHyojunB)) Then
                        m_csAtenaRirekiFZYHyojunB = New ABAtenaRirekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '��������t��_�W���}�X�^�����폜���s
                    intCnt4 = m_csAtenaRirekiFZYHyojunB.DeleteAtenaFZYHyojunRB(csAtenaFZYHyojunDr, strSakujoKB)
                End If
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

        Return intCnt

    End Function
    '*����ԍ� 000038 2023/08/14 �ǉ��I��

    '* ����ԍ� 000022 2005/11/18 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�����폜(�P�Z���R�[�h�w��)
    '* 
    '* �\��           Public Overloads Function DeleteAtenaRB(ByVal strJuminCD As String) As Integer
    '* 
    '* �@�\�@�@    �@�@���������}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           strJuminCD As String : �폜����ΏۂƂȂ�Z���R�[�h
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaRB(ByVal strJuminCD As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteAtenaRB"
        Dim intDelCnt As Integer                            ' �폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            ' �폜�p�̃p�����[�^�tDELETE��������ƃp�����[�^�R���N�V�������쐬����
            If (m_strDelFromJuminCDSQL Is Nothing OrElse m_strDelFromJuminCDSQL = String.Empty OrElse _
                    IsNothing(m_cfDelFromJuminCDPrmCollection)) Then
                Call CreateDelFromJuminCDSQL()
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            m_cfDelFromJuminCDPrmCollection(ABAtenaRirekiEntity.KEY_JUMINCD).Value = strJuminCD

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

    '* ����ԍ� 000022 2005/11/18 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     WHERE���̍쐬
    '* 
    '* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '*                strKikanYMD As String : ���ԔN����
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Function CreateWhere(ByVal cSearchKey As ABAtenaSearchKey, ByVal strKikanYMD As String) As String
        Const THIS_METHOD_NAME As String = "CreateWhere"
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            'WHERE��̍쐬
            '* ����ԍ� 000019 2005/01/25 �X�V�J�n�i�{��j
            'csWHERE = New StringBuilder()
            csWHERE = New StringBuilder(256)
            '* ����ԍ� 000019 2005/01/25 �X�V�I���i�{��j

            '�Z���R�[�h
            If Not (cSearchKey.p_strJuminCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                '*����ԍ� 000015 2003/11/18 �C���J�n
                'csWHERE.Append(ABAtenaRirekiEntity.JUMINCD)
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
                '*����ԍ� 000015 2003/11/18 �C���I��
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_JUMINCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
                cfUFParameterClass.Value = cSearchKey.p_strJuminCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z���D��敪
            If Not (cSearchKey.p_strJuminYuseniKB.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINYUSENIKB)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_JUMINYUSENIKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINYUSENIKB
                cfUFParameterClass.Value = cSearchKey.p_strJuminYuseniKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z�o�O�D��敪
            If Not (cSearchKey.p_strJutogaiYusenKB.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTOGAIYUSENKB)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_JUTOGAIYUSENKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUTOGAIYUSENKB
                cfUFParameterClass.Value = cSearchKey.p_strJutogaiYusenKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '���уR�[�h
            If Not (cSearchKey.p_strStaiCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAICD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_STAICD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_STAICD
                cfUFParameterClass.Value = cSearchKey.p_strStaiCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '*����ԍ� 000036 2020/01/10 �C���J�n
            ''�����p�J�i����
            'If Not (cSearchKey.p_strSearchKanaSeiMei.Trim = String.Empty) Then
            '    If Not (csWHERE.Length = 0) Then
            '        csWHERE.Append(" AND ")
            '    End If

            '    If cSearchKey.p_strSearchKanaSeiMei.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEIMEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEIMEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSeiMei
            '    Else
            '        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEIMEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEIMEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
            '    End If
            '    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            'End If

            ''�����p�J�i��
            'If Not (cSearchKey.p_strSearchKanaSei.Trim = String.Empty) Then
            '    If Not (csWHERE.Length = 0) Then
            '        csWHERE.Append(" AND ")
            '    End If
            '    '* ����ԍ� 000027 2007/10/10 �ǉ��J�n
            '    ' �����p�J�i���Q�Ɍ����L�[���i�[����Ă���ꍇ�͌��������Ƃ��Ēǉ�
            '    If (cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty) Then
            '        csWHERE.Append(" ( ")
            '    End If
            '    '* ����ԍ� 000027 2007/10/10 �ǉ��I��
            '    If cSearchKey.p_strSearchKanaSei.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    Else
            '        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei.TrimEnd

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    End If
            'End If

            ''* ����ԍ� 000027 2007/10/10 �ǉ��J�n
            '' �����p�J�i���QOR����
            '' �����p�J�i���Q�Ɍ����L�[���i�[����Ă���ꍇ�͌��������Ƃ��Ēǉ�
            'If (cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty) Then
            '    csWHERE.Append(" OR ")
            '    If cSearchKey.p_strSearchKanaSei2.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEI2)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEI2
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei2

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    Else
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANASEI2)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANASEI2
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei2.TrimEnd

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    End If
            '    ' OR�����͌����p�J�i���݂̂ł̏����Ȃ̂Ŋ��ʂŊ���
            '    csWHERE.Append(" ) ")
            'End If
            ''* ����ԍ� 000027 2007/10/10 �ǉ��I��

            ''�����p�J�i��
            'If Not (cSearchKey.p_strSearchKanaMei.Trim = String.Empty) Then
            '    If Not (csWHERE.Length = 0) Then
            '        csWHERE.Append(" AND ")
            '    End If
            '    If cSearchKey.p_strSearchKanaMei.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANAMEI)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANAMEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANAMEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaMei

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    Else
            '        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANAMEI)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaRirekiEntity.KEY_SEARCHKANAMEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEARCHKANAMEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaMei.TrimEnd

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    End If
            'End If

            ''�����p��������
            'If Not (cSearchKey.p_strSearchKanjiMeisho.Trim = String.Empty) Then
            '    If Not (csWHERE.Length = 0) Then
            '        csWHERE.Append(" AND ")
            '    End If
            '    If cSearchKey.p_strSearchKanjiMeisho.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANJIMEISHO)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaRirekiEntity.PARAM_SEARCHKANJIMEISHO)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SEARCHKANJIMEISHO
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanjiMeisho

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    Else
            '        csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANJIMEISHO)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaRirekiEntity.PARAM_SEARCHKANJIMEISHO)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SEARCHKANJIMEISHO
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    End If
            'End If

            ''* ����ԍ� 000026 2007/09/04 �ǉ��J�n
            '' �{���������� �{������="2(Tsusho_Seishiki)"�̂Ƃ��̂݊��������Q�͌������ڂƂȂ�
            'If (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki) Then
            '    If Not (cSearchKey.p_strKanjiMeisho2.Trim = String.Empty) Then
            '        If Not (csWHERE.Length = 0) Then
            '            csWHERE.Append(" AND ")
            '        End If
            '        If cSearchKey.p_strKanjiMeisho2.IndexOf("%") = -1 Then
            '            csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO2)
            '            csWHERE.Append(" = ")
            '            csWHERE.Append(ABAtenaEntity.PARAM_KANJIMEISHO2)

            '            ' ���������̃p�����[�^���쐬
            '            cfUFParameterClass = New UFParameterClass
            '            cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
            '            cfUFParameterClass.Value = cSearchKey.p_strKanjiMeisho2

            '            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '        Else
            '            csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO2)
            '            csWHERE.Append(" LIKE ")
            '            csWHERE.Append(ABAtenaEntity.PARAM_KANJIMEISHO2)

            '            ' ���������̃p�����[�^���쐬
            '            cfUFParameterClass = New UFParameterClass
            '            cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KANJIMEISHO2
            '            cfUFParameterClass.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd

            '            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '        End If
            '    End If
            'End If
            ''* ����ԍ� 000026 2007/09/04 �ǉ��I��

            ' �������������𐶐�
            m_cKensakuShimeiB.CreateWhereForShimei(cSearchKey, ABAtenaRirekiEntity.TABLE_NAME, csWHERE, m_cfSelectUFParameterCollectionClass,
                                                   ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, True, m_intHyojunKB)
            '*����ԍ� 000036 2020/01/10 �C���I��

            '���N����
            If Not (cSearchKey.p_strUmareYMD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                If cSearchKey.p_strUmareYMD.RIndexOf("%") = -1 Then
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_UMAREYMD)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_UMAREYMD
                    cfUFParameterClass.Value = cSearchKey.p_strUmareYMD

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                Else
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaRirekiEntity.KEY_UMAREYMD)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_UMAREYMD
                    cfUFParameterClass.Value = cSearchKey.p_strUmareYMD.TrimEnd

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            End If

            '����
            If Not (cSearchKey.p_strSeibetsuCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEIBETSUCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_SEIBETSUCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_SEIBETSUCD
                cfUFParameterClass.Value = cSearchKey.p_strSeibetsuCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z���R�[�h
            If Not (cSearchKey.p_strJushoCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHOCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_JUSHOCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUSHOCD
                cfUFParameterClass.Value = cSearchKey.p_strJushoCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�s����R�[�h
            If Not (cSearchKey.p_strGyoseikuCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_GYOSEIKUCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_GYOSEIKUCD
                cfUFParameterClass.Value = cSearchKey.p_strGyoseikuCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�n��R�[�h�P
            If Not (cSearchKey.p_strChikuCD1.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.PARAM_CHIKUCD1)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_CHIKUCD1
                cfUFParameterClass.Value = cSearchKey.p_strChikuCD1

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�n��R�[�h�Q
            If Not (cSearchKey.p_strChikuCD2.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.PARAM_CHIKUCD2)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_CHIKUCD2
                cfUFParameterClass.Value = cSearchKey.p_strChikuCD2

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�n��R�[�h�R
            If Not (cSearchKey.p_strChikuCD3.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD3)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.PARAM_CHIKUCD3)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_CHIKUCD3
                cfUFParameterClass.Value = cSearchKey.p_strChikuCD3

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Ԓn�R�[�h�P
            If Not (cSearchKey.p_strBanchiCD1.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_BANCHICD1)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_BANCHICD1
                cfUFParameterClass.Value = cSearchKey.p_strBanchiCD1

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Ԓn�R�[�h�Q
            If Not (cSearchKey.p_strBanchiCD2.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_BANCHICD2)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_BANCHICD2
                cfUFParameterClass.Value = cSearchKey.p_strBanchiCD2

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Ԓn�R�[�h�R
            If Not (cSearchKey.p_strBanchiCD3.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD3)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_BANCHICD3)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_BANCHICD3
                cfUFParameterClass.Value = cSearchKey.p_strBanchiCD3

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z��Z���R�[�h
            If Not (cSearchKey.p_strJukiJushoCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHOCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIJUSHOCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIJUSHOCD
                cfUFParameterClass.Value = cSearchKey.p_strJukiJushoCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z��s����R�[�h
            If Not (cSearchKey.p_strJukiGyoseikuCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIGYOSEIKUCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIGYOSEIKUCD
                cfUFParameterClass.Value = cSearchKey.p_strJukiGyoseikuCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z��n��R�[�h�P
            If Not (cSearchKey.p_strJukiChikuCD1.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.PARAM_JUKICHIKUCD1)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUKICHIKUCD1
                cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD1

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z��n��R�[�h�Q
            If Not (cSearchKey.p_strJukiChikuCD2.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.PARAM_JUKICHIKUCD2)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUKICHIKUCD2
                cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD2

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z��n��R�[�h�R
            If Not (cSearchKey.p_strJukiChikuCD3.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD3)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.PARAM_JUKICHIKUCD3)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUKICHIKUCD3
                cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD3

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z��Ԓn�R�[�h�P
            If Not (cSearchKey.p_strJukiBanchiCD1.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIBANCHICD1)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIBANCHICD1
                cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD1

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z��Ԓn�R�[�h�Q
            If Not (cSearchKey.p_strJukiBanchiCD2.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIBANCHICD2)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIBANCHICD2
                cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD2

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z��Ԓn�R�[�h�R
            If Not (cSearchKey.p_strJukiBanchiCD3.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD3)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_JUKIBANCHICD3)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUKIBANCHICD3
                cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD3

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�f�[�^�敪
            If Not (cSearchKey.p_strDataKB.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                '*����ԍ� 000015 2003/11/18 �C���J�n
                'csWHERE.Append(ABAtenaRirekiEntity.ATENADATAKB)
                'csWHERE.Append(" = ")
                'csWHERE.Append(ABAtenaRirekiEntity.PARAM_ATENADATAKB)

                If cSearchKey.p_strDataKB.RIndexOf("%") = -1 Then
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_ATENADATAKB)
                    ' ���������̃p�����[�^���쐬
                Else
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaRirekiEntity.PARAM_ATENADATAKB)
                    ' ���������̃p�����[�^���쐬
                End If
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_ATENADATAKB
                cfUFParameterClass.Value = cSearchKey.p_strDataKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                '*����ԍ� 000015 2003/11/18 �C���I��


            End If

            If Not ((cSearchKey.p_strJuminShubetu1 = String.Empty) And (cSearchKey.p_strJuminShubetu2 = String.Empty)) Then
                If (cSearchKey.p_strDataKB.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    csWHERE.Append("((")
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB)
                    csWHERE.Append(" = '11')")
                    csWHERE.Append(" OR (")
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB)
                    csWHERE.Append(" = '12'))")
                End If

                '�Z����ʂP
                If Not (cSearchKey.p_strJuminShubetu1.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    csWHERE.Append(" {fn SUBSTRING(")
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATASHU)
                    csWHERE.Append(",1,1)} = '")
                    csWHERE.Append(cSearchKey.p_strJuminShubetu1)
                    csWHERE.Append("'")
                End If

                '�Z����ʂQ
                If Not (cSearchKey.p_strJuminShubetu2.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    csWHERE.Append(" {fn SUBSTRING(")
                    csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATASHU)
                    csWHERE.Append(",2,1)} = '")
                    csWHERE.Append(cSearchKey.p_strJuminShubetu2)
                    csWHERE.Append("'")
                End If
            End If

            '���ԔN����

            If Not (strKikanYMD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If

                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RRKST_YMD)
                csWHERE.Append(" <= ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_RRKST_YMD)
                csWHERE.Append(" AND ")
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RRKED_YMD)
                csWHERE.Append(" >= ")
                csWHERE.Append(ABAtenaRirekiEntity.KEY_RRKED_YMD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RRKST_YMD
                cfUFParameterClass.Value = strKikanYMD
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RRKED_YMD
                cfUFParameterClass.Value = strKikanYMD
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�s�����R�[�h
            If Not (cSearchKey.p_strShichosonCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                '*����ԍ� 000015 2003/11/18 �C���J�n
                'csWHERE.Append(ABAtenaRirekiEntity.SHICHOSONCD)
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHICHOSONCD)
                '*����ԍ� 000015 2003/11/18 �C���I��
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.PARAM_SHICHOSONCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SHICHOSONCD
                cfUFParameterClass.Value = cSearchKey.p_strShichosonCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '*����ԍ� 000033 2014/04/28 �ǉ��J�n
            ' --------------------------------------------------------------------------------------------------------
            ' ���ʔԍ����w�肳��Ă���ꍇ
            If (cSearchKey.p_strMyNumber.Trim.RLength > 0) Then

                ' -----------------------------------------------------------------------------------------------------
                ' �y�P�D���ߌ����敪�ɂ�鐧��z
                ' ���ߌ����敪�̐���
                Select Case cSearchKey.p_strMyNumberChokkinSearchKB
                    Case ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode.ToString,
                         ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode.ToString
                        ' noop
                    Case Else
                        ' �K��l�ȊO�i�l�Ȃ����܂ށj�̏ꍇ�A�Ǘ����o�^�l�ɂĐ��䂷��B
                        cSearchKey.p_strMyNumberChokkinSearchKB = m_strMyNumberChokkinSearchKB_Param
                End Select

                ' ���ߌ����敪��"1"�i���߂̂݁j�̏ꍇ
                If (cSearchKey.p_strMyNumberChokkinSearchKB = ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode.ToString) Then

                    ' ���ʔԍ��J�����ɋ��ʔԍ����w�肷��B
                    If (csWHERE.RLength > 0) Then
                        csWHERE.Append(" AND ")
                    Else
                        ' noop
                    End If
                    csWHERE.AppendFormat("{0}.{1} = {2}",
                                         ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.MYNUMBER,
                                         ABMyNumberEntity.PARAM_MYNUMBER)

                    ' ���������̃p�����[�^�[���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER
                    cfUFParameterClass.Value = cSearchKey.p_strMyNumber

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

                Else

                    ' ���ʔԍ��}�X�^�ւ̃T�u�N�G���ɋ��ʔԍ����w�肷��B
                    If (csWHERE.RLength > 0) Then
                        csWHERE.Append(" AND ")
                    Else
                        ' noop
                    End If
                    csWHERE.AppendFormat("{0}.{1} ", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD)
                    csWHERE.Append("IN ( ")
                    csWHERE.AppendFormat("SELECT {0} FROM {1} ", ABMyNumberEntity.JUMINCD, ABMyNumberEntity.TABLE_NAME)
                    csWHERE.AppendFormat("WHERE {0} = {1} ", ABMyNumberEntity.MYNUMBER, ABMyNumberEntity.PARAM_MYNUMBER)
                    csWHERE.Append(")")

                    ' ���������̃p�����[�^�[���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER
                    cfUFParameterClass.Value = cSearchKey.p_strMyNumber

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

                End If
                ' -----------------------------------------------------------------------------------------------------
                ' �y�Q�D�l�@�l�敪�ɂ�鐧��z
                ' �l�@�l�敪��"1"�i�l�j�A�܂���"2"�i�@�l�j�̏ꍇ
                If (cSearchKey.p_strMyNumberKojinHojinKB = "1" _
                    OrElse cSearchKey.p_strMyNumberKojinHojinKB = "2") Then

                    ' �l�@�l�敪�J�����Ɍl�@�l�敪���w�肷��B
                    If (csWHERE.RLength > 0) Then
                        csWHERE.Append(" AND ")
                    Else
                        ' noop
                    End If
                    csWHERE.AppendFormat("{0}.{1} = {2}",
                                         ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KJNHJNKB,
                                         ABAtenaRirekiEntity.PARAM_KJNHJNKB)

                    ' ���������̃p�����[�^�[���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KJNHJNKB
                    cfUFParameterClass.Value = cSearchKey.p_strMyNumberKojinHojinKB

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

                Else
                    ' noop
                End If
                ' -----------------------------------------------------------------------------------------------------

            Else
                ' noop
            End If
            ' --------------------------------------------------------------------------------------------------------
            '*����ԍ� 000033 2014/04/28 �ǉ��I��            

            '�d�b�ԍ�
            If Not (cSearchKey.p_strRenrakusaki.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append("((")
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.PARAM_RENRAKUSAKI1)
                csWHERE.Append(") OR (")
                csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaRirekiEntity.PARAM_RENRAKUSAKI2)
                csWHERE.Append("))")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_RENRAKUSAKI1
                cfUFParameterClass.Value = cSearchKey.p_strRenrakusaki

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_RENRAKUSAKI2
                cfUFParameterClass.Value = cSearchKey.p_strRenrakusaki

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                '�Z��
                If Not (cSearchKey.p_strJusho.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    If (cSearchKey.p_strJusho.RIndexOf("%") = -1) Then
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHJUSHO)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHJUSHO)
                    Else
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHJUSHO)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHJUSHO)
                    End If
                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_SEARCHJUSHO
                    cfUFParameterClass.Value = cSearchKey.p_strJusho

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

                '����
                If Not (cSearchKey.p_strKatagaki.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    If cSearchKey.p_strKatagaki.RIndexOf("%") = -1 Then
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKATAGAKI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKATAGAKI)
                    Else
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKATAGAKI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKATAGAKI)
                    End If
                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_SEARCHKATAGAKI
                    cfUFParameterClass.Value = cSearchKey.p_strKatagaki

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

                '����
                If Not (cSearchKey.p_strKyuuji.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    If cSearchKey.p_strKyuuji.RIndexOf("%") = -1 Then
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKANJIKYUUJI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANJIKYUUJI)
                    Else
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKANJIKYUUJI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANJIKYUUJI)
                    End If
                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANJIKYUUJI
                    cfUFParameterClass.Value = cSearchKey.p_strKyuuji

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

                '�J�i����
                If Not (cSearchKey.p_strKanaKyuuji.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    If cSearchKey.p_strKanaKyuuji.RIndexOf("%") = -1 Then
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKANAKYUUJI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANAKYUUJI)
                    Else
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiHyojunEntity.SEARCHKANAKYUUJI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANAKYUUJI)
                    End If
                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaRirekiHyojunEntity.PARAM_SEARCHKANAKYUUJI
                    cfUFParameterClass.Value = cSearchKey.p_strKanaKyuuji

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

                '�J�^�J�i���L��
                If Not (cSearchKey.p_strKatakanaHeikimei.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    If cSearchKey.p_strKatakanaHeikimei.RIndexOf("%") = -1 Then
                        csWHERE.Append(ABAtenaRirekiFZYEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiFZYEntity.KATAKANAHEIKIMEI)
                        csWHERE.Append(" = ")
                        csWHERE.Append(ABAtenaRirekiFZYEntity.PARAM_KATAKANAHEIKIMEI)
                    Else
                        csWHERE.Append(ABAtenaRirekiFZYEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiFZYEntity.KATAKANAHEIKIMEI)
                        csWHERE.Append(" LIKE ")
                        csWHERE.Append(ABAtenaRirekiFZYEntity.PARAM_KATAKANAHEIKIMEI)
                    End If
                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaRirekiFZYEntity.PARAM_KATAKANAHEIKIMEI
                    cfUFParameterClass.Value = cSearchKey.p_strKatakanaHeikimei

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                End If
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

    '* corresponds to VS2008 Start 2010/04/16 000029
    '* ����ԍ� 000020 2005/06/15 �폜�J�n
    ''''************************************************************************************************
    ''''* ���\�b�h��     SQL���̍쐬
    ''''* 
    ''''* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    ''''* 
    ''''* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    ''''* 
    ''''* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    ''''* 
    ''''* �߂�l         �Ȃ�
    ''''************************************************************************************************
    ''Private Sub CreateSQL(ByVal csDataRow As DataRow)

    ''    Const THIS_METHOD_NAME As String = "CreateSQL"
    ''    Dim csDataColumn As DataColumn
    ''    Dim cfUFParameterClass As UFParameterClass
    ''    Dim csInsertColumn As StringBuilder                 'INSERT�p�J������`
    ''    Dim csInsertParam As StringBuilder                  'INSERT�p�p�����[�^��`
    ''    Dim csWhere As StringBuilder                        'WHERE��`
    ''    Dim csUpdateParam As StringBuilder                  'UPDATE�pSQL��`
    ''    Dim csDelRonriParam As StringBuilder                '�_���폜�p�����[�^��`


    ''    Try
    '''' �f�o�b�O���O�o��
    ''m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '''' SELECT SQL���̍쐬
    ''m_strInsertSQL = "INSERT INTO " + ABAtenaRirekiEntity.TABLE_NAME + " "
    ''csInsertColumn = New StringBuilder()
    ''csInsertParam = New StringBuilder()


    '''' UPDATE SQL���̍쐬
    ''m_strUpdateSQL = "UPDATE " + ABAtenaRirekiEntity.TABLE_NAME + " SET "
    ''csUpdateParam = New StringBuilder()


    '''' WHERE���̍쐬
    ''csWhere = New StringBuilder()
    ''csWhere.Append(" WHERE ")
    ''csWhere.Append(ABAtenaRirekiEntity.JUMINCD)
    ''csWhere.Append(" = ")
    ''csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD)
    ''csWhere.Append(" AND ")
    ''csWhere.Append(ABAtenaRirekiEntity.RIREKINO)
    ''csWhere.Append(" = ")
    ''csWhere.Append(ABAtenaRirekiEntity.KEY_RIREKINO)
    ''csWhere.Append(" AND ")
    ''csWhere.Append(ABAtenaRirekiEntity.KOSHINCOUNTER)
    ''csWhere.Append(" = ")
    ''csWhere.Append(ABAtenaRirekiEntity.KEY_KOSHINCOUNTER)


    '''' �_��DELETE SQL���̍쐬
    ''csDelRonriParam = New StringBuilder()
    ''csDelRonriParam.Append("UPDATE ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.TABLE_NAME)
    ''csDelRonriParam.Append(" SET ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.TANMATSUID)
    ''csDelRonriParam.Append(" = ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_TANMATSUID)
    ''csDelRonriParam.Append(", ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.SAKUJOFG)
    ''csDelRonriParam.Append(" = ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_SAKUJOFG)
    ''csDelRonriParam.Append(", ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINCOUNTER)
    ''csDelRonriParam.Append(" = ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINCOUNTER)
    ''csDelRonriParam.Append(", ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINNICHIJI)
    ''csDelRonriParam.Append(" = ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINNICHIJI)
    ''csDelRonriParam.Append(", ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINUSER)
    ''csDelRonriParam.Append(" = ")
    ''csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINUSER)
    ''csDelRonriParam.Append(csWhere)
    ''m_strDelRonriSQL = csDelRonriParam.ToString

    '''' ����DELETE SQL���̍쐬
    ''m_strDelButuriSQL = "DELETE FROM " + ABAtenaRirekiEntity.TABLE_NAME + csWhere.ToString

    '''' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
    ''m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

    '''' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
    ''m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

    '''' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
    ''m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

    '''' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
    ''m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass()


    '''' �p�����[�^�R���N�V�����̍쐬
    ''For Each csDataColumn In csDataRow.Table.Columns
    ''    cfUFParameterClass = New UFParameterClass()

    '''' INSERT SQL���̍쐬
    ''csInsertColumn.Append(csDataColumn.ColumnName)
    ''csInsertColumn.Append(", ")

    ''csInsertParam.Append(ABAtenaRirekiEntity.PARAM_PLACEHOLDER)
    ''csInsertParam.Append(csDataColumn.ColumnName)
    ''csInsertParam.Append(", ")


    '''' UPDATE SQL���̍쐬
    ''csUpdateParam.Append(csDataColumn.ColumnName)
    ''csUpdateParam.Append(" = ")
    ''csUpdateParam.Append(ABAtenaRirekiEntity.PARAM_PLACEHOLDER)
    ''csUpdateParam.Append(csDataColumn.ColumnName)
    ''csUpdateParam.Append(", ")

    '''' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    '''' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''Next csDataColumn

    ''''�Ō�̃J���}����菜����INSERT�����쐬
    ''m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
    ''        + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"


    '''' UPDATE SQL���̃g���~���O
    ''m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray())

    '''' UPDATE SQL����WHERE��̒ǉ�
    ''m_strUpdateSQL += csWhere.ToString


    '''' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
    ''m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
    ''m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER
    ''m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)


    '''' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_TANMATSUID
    ''m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SAKUJOFG
    ''m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINCOUNTER
    ''m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINNICHIJI
    ''m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINUSER
    ''m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
    ''m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
    ''m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER
    ''m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)


    '''' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
    ''m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
    ''m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''cfUFParameterClass = New UFParameterClass()
    ''cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER
    ''m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    '''' �f�o�b�O���O�o��
    ''m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''    Catch objAppExp As UFAppException
    ''        ' ���[�j���O���O�o��
    ''        m_cfLogClass.WarningWrite(m_cfControlData, _
    ''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''                                    "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
    ''                                    "�y���[�j���O���e:" + objAppExp.Message + "�z")
    ''        ' �G���[�����̂܂܃X���[����
    ''        Throw objAppExp

    ''    Catch objExp As Exception
    ''        ' �G���[���O�o��
    ''        m_cfLogClass.ErrorWrite(m_cfControlData, _
    ''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''                                    "�y�G���[���e:" + objExp.Message + "�z")
    ''        ' �G���[�����̂܂܃X���[����
    ''        Throw objExp
    ''    End Try

    ''End Sub
    '* ����ԍ� 000020 2005/06/15 �폜�I��
    '* corresponds to VS2008 End 2010/04/16 000029


    '* ����ԍ� 000020 2005/06/15 �ǉ��J�n
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
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABAtenaRirekiEntity.TABLE_NAME + " "
            csInsertColumn = New StringBuilder
            csInsertParam = New StringBuilder

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")

                csInsertParam.Append(ABAtenaRirekiEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '�Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"

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
            m_strUpdateSQL = "UPDATE " + ABAtenaRirekiEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_KOSHINCOUNTER)

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�Z���b�c�E����ԍ��E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If Not (csDataColumn.ColumnName = ABAtenaRirekiEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABAtenaRirekiEntity.RIREKINO) AndAlso _
                     Not (csDataColumn.ColumnName = ABAtenaRirekiEntity.SAKUSEIUSER) AndAlso _
                      Not (csDataColumn.ColumnName = ABAtenaRirekiEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL���̍쐬
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaRirekiEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(", ")

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn


            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray())

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += csWhere.ToString


            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER
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
            csWhere.Append(ABAtenaRirekiEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_KOSHINCOUNTER)

            ' �_��DELETE SQL���̍쐬
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaRirekiEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where���̒ǉ�
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass


            ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER
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
            csWhere.Append(ABAtenaRirekiEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiEntity.RIREKINO)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_RIREKINO)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaRirekiEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_KOSHINCOUNTER)

            ' ����DELETE SQL���̍쐬
            m_strDelButuriSQL = "DELETE FROM " + ABAtenaRirekiEntity.TABLE_NAME + csWhere.ToString

            ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_KOSHINCOUNTER
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
    '* ����ԍ� 000020 2005/06/15 �ǉ��I��

    '* ����ԍ� 000022 2005/11/18 �ǉ��J�n
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
        '* corresponds to VS2008 Start 2010/04/16 000029
        'Dim cfUFParameterClass As UFParameterClass
        '* corresponds to VS2008 End 2010/04/16 000029
        Dim csWhere As StringBuilder                        'WHERE��`

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaRirekiEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD)

            ' ����DELETE(�P�Z���b�c�w��) SQL���̍쐬
            m_strDelFromJuminCDSQL = "DELETE FROM " + ABAtenaRirekiEntity.TABLE_NAME + csWhere.ToString

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            m_cfDelFromJuminCDPrmCollection = New UFParameterCollectionClass
            m_cfDelFromJuminCDPrmCollection.Add(ABAtenaRirekiEntity.KEY_JUMINCD, DbType.String)

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
    '* ����ԍ� 000022 2005/11/18 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
    '* 
    '* �@�\           �X�V�f�[�^�̐��������`�F�b�N����B
    '* 
    '* ����           strColumnName As String : ���������}�X�^�f�[�^�Z�b�g�̍��ږ�
    '*                strValue As String     : ���ڂɑΉ�����l
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)

        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Const TABLENAME As String = "���������D"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����

        Try
            ' �f�o�b�O���O�o��
            'm_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'")

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()

                Case ABAtenaRirekiEntity.JUMINCD            '�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SHICHOSONCD        '�s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KYUSHICHOSONCD     '���s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.RIREKINO           '����ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RIREKINO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.RRKST_YMD          '�����J�n�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RRKST_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.RRKED_YMD          '�����I���N����
                    If Not (strValue = String.Empty Or strValue = "00000000" Or strValue = "99999999") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RRKED_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.JUMINJUTOGAIKB     '�Z���Z�o�O�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUMINJUTOGAIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUMINYUSENIKB      '�Z���D��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUMINYUSENIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUTOGAIYUSENKB     '�Z�o�O�D��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTOGAIYUSENKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.ATENADATAKB        '�����f�[�^�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ATENADATAKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.STAICD             '���уR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_STAICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUMINHYOCD         '�Z���[�R�[�h
                    '�`�F�b�N�Ȃ�

                Case ABAtenaRirekiEntity.SEIRINO            '�����ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEIRINO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.ATENADATASHU       '�����f�[�^���
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ATENADATASHU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.HANYOKB1           '�ėp�敪1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HANYOKB1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KJNHJNKB           '�l�@�l�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KJNHJNKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.HANYOKB2           '�ėp�敪2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HANYOKB2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANNAIKANGAIKB     '�Ǔ��ǊO�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANNAIKANGAIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANAMEISHO1        '�J�i����1
                    '*����ԍ� 000014 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000014 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANAMEISHO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANJIMEISHO1       '��������1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIMEISHO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANAMEISHO2        '�J�i����2
                    '*����ԍ� 000014 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000014 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANAMEISHO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANJIMEISHO2       '��������2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIMEISHO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANJIHJNKEITAI     '�����@�l�`��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIHJNKEITAI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI   '�����@�l��\�Ҏ���
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SEARCHKANJIMEISHO  '�����p��������
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEARCHKANJIMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KYUSEI             '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KYUSEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SEARCHKANASEIMEI   '�����p�J�i����
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i����", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SEARCHKANASEI      '�����p�J�i��
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i��", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SEARCHKANAMEI      '�����p�J�i��
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i��", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIRRKNO          '�Z���ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIRRKNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                    'Case ABAtenaRirekiEntity.UMAREYMD           '���N����
                    '    If Not (strValue = String.Empty Or strValue = "00000000") Then
                    '        m_cfDateClass.p_strDateValue = strValue
                    '        If (Not m_cfDateClass.CheckDate()) Then
                    '            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '            '�G���[��`���擾
                    '            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_UMAREYMD)
                    '            '��O�𐶐�
                    '            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '        End If
                    '    End If

                    'Case ABAtenaRirekiEntity.UMAREWMD           '���a��N����
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾(�������ړ��͂̌��ł��B�F)
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "���a��N����", objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaRirekiEntity.SEIBETSUCD         '���ʃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEIBETSUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SEIBETSU           '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEIBETSU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SEKINO             '�Дԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SEKINO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUMINHYOHYOJIJUN   '�Z���[�\����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUMINHYOHYOJIJUN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.ZOKUGARACD         '�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimEnd)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZOKUGARACD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.ZOKUGARA           '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZOKUGARA)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN     '��Q�Z���[�\����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2JUMINHYOHYOJIJUN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.DAI2ZOKUGARACD           '��Q�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimEnd)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2ZOKUGARACD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.DAI2ZOKUGARA             '��Q����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2ZOKUGARA)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.STAINUSJUMINCD     '���ю�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_STAINUSJUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.STAINUSMEI         '���ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANASTAINUSMEI     '�J�i���ю喼
                    '*����ԍ� 000014 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000014 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANASTAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.DAI2STAINUSJUMINCD       '��Q���ю�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2STAINUSJUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.DAI2STAINUSMEI           '��Q���ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_DAI2STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANADAI2STAINUSMEI       '��Q�J�i���ю喼
                    '*����ԍ� 000014 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000014 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANADAI2STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.YUBINNO            '�X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_YUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUSHOCD            '�Z���R�[�h
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUSHO              '�Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.BANCHICD1          '�Ԓn�R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BANCHICD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.BANCHICD2          '�Ԓn�R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BANCHICD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.BANCHICD3          '�Ԓn�R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BANCHICD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.BANCHI             '�Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KATAGAKIFG         '�����t���O
                    If (Not strValue.Trim = String.Empty) Then
                        If (Not UFStringClass.CheckNumber(strValue)) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KATAGAKIFG)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.KATAGAKICD         '�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KATAGAKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KATAGAKI           '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.RENRAKUSAKI1       '�A����1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RENRAKUSAKI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.RENRAKUSAKI2       '�A����2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_RENRAKUSAKI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.HON_ZJUSHOCD       '�{�БS���Z���R�[�h
                    '* ����ԍ� 000017 2004/10/19 �C���J�n�i�}���S���R�j
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000017 2004/10/19 �C���I���i�}���S���R�j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HON_ZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.HON_JUSHO          '�{�ЏZ��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HON_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.HONSEKIBANCHI      '�{�ДԒn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HONSEKIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.HITTOSH            '�M����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HITTOSH)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.CKINIDOYMD         '���߈ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.CKINJIYUCD         '���ߎ��R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                    'Case ABAtenaRirekiEntity.CKINJIYU           '���ߎ��R
                    '    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINJIYU)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaRirekiEntity.CKINTDKDYMD        '���ߓ͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINTDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.CKINTDKDTUCIKB     '���ߓ͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CKINTDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TOROKUIDOYMD       '�o�^�ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.TOROKUIDOWMD       '�o�^�ٓ��a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUIDOWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.TOROKUJIYUCD       '�o�^���R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TOROKUJIYU         '�o�^���R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TOROKUTDKDYMD      '�o�^�͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUTDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.TOROKUTDKDWMD      '�o�^�͏o�a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUTDKDWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.TOROKUTDKDTUCIKB   '�o�^�͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOROKUTDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUTEIIDOYMD        '�Z��ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEIIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.JUTEIIDOWMD        '�Z��ٓ��a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEIIDOWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.JUTEIJIYUCD        '�Z�莖�R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEIJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUTEIJIYU          '�Z�莖�R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEIJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUTEITDKDYMD       '�Z��͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEITDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.JUTEITDKDWMD       '�Z��͏o�a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEITDKDWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.JUTEITDKDTUCIKB    '�Z��͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUTEITDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SHOJOIDOYMD        '�����ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.SHOJOJIYUCD        '�������R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SHOJOJIYU          '�������R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SHOJOTDKDYMD       '�����͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOTDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.SHOJOTDKDTUCIKB    '�����͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOJOTDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD     '�]�o�\��͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD      '�]�o�m��͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD   '�]�o�m��ʒm�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTITSUCHIYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUNYURIYUCD       '�]�o�����R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUNYURIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUNYURIYU         '�]�o�����R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUNYURIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENUMAEJ_YUBINNO         '�]���O�Z���X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_YUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD        '�]���O�Z���S���Z���R�[�h
                    '* ����ԍ� 000017 2004/10/19 �C���J�n�i�}���S���R�j
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000017 2004/10/19 �C���I���i�}���S���R�j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_ZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENUMAEJ_JUSHO           '�]���O�Z���Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENUMAEJ_BANCHI          '�]���O�Z���Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_BANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI        '�]���O�Z������
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_KATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI      '�]���O�Z�����ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENUMAEJ_STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO    '�]�o�\��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD   '�]�o�\��S���Z���R�[�h
                    '* ����ԍ� 000017 2004/10/19 �C���J�n�i�}���S���R�j
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000017 2004/10/19 �C���I���i�}���S���R�j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO      '�]�o�\��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI     '�]�o�\��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI   '�]�o�\�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEIKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI '�]�o�\�萢�ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUYOTEISTAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO     '�]�o�m��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD    '�]�o�m��S���Z���R�[�h
                    '* ����ԍ� 000017 2004/10/19 �C���J�n�i�}���S���R�j
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000017 2004/10/19 �C���I���i�}���S���R�j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO     '�]�o�m��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI      '�]�o�m��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI    '�]�o�m�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI  '�]�o�m�萢�ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTISTAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TENSHUTSUKKTIMITDKFG     '�]�o�m�薢�̓t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TENSHUTSUKKTIMITDKFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.BIKOYMD                  '���l�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BIKOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.BIKO                     '���l
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BIKO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.BIKOTENSHUTSUKKTIJUSHOFG '���l�]�o�m��Z���t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BIKOTENSHUTSUKKTIJUSHOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.HANNO                    '�Ŕԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HANNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KAISEIATOFG              '������t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KAISEIATOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KAISEIMAEFG             '�����O�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KAISEIMAEFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KAISEIYMD                '�����N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KAISEIYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.GYOSEIKUCD               '�s����R�[�h
                    '* ����ԍ� 000023 2005/12/26 �C���J�n
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* ����ԍ� 000023 2005/12/26 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_GYOSEIKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.GYOSEIKUMEI              '�s���於
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_GYOSEIKUMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.CHIKUCD1                 '�n��R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUCD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.CHIKUMEI1                '�n�於1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUMEI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.CHIKUCD2                 '�n��R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUCD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.CHIKUMEI2                '�n�於2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUMEI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.CHIKUCD3                 '�n��R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUCD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.CHIKUMEI3                '�n�於3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHIKUMEI3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.TOHYOKUCD                '���[��R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TOHYOKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SHOGAKKOKUCD             '���w�Z��R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHOGAKKOKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.CHUGAKKOKUCD             '���w�Z��R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_CHUGAKKOKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.HOGOSHAJUMINCD           '�ی�ҏZ���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_HOGOSHAJUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANJIHOGOSHAMEI          '�����ی�Җ�
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANJIHOGOSHAMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KANAHOGOSHAMEI           '�J�i�ی�Җ�
                    '*����ԍ� 000014 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000014 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KANAHOGOSHAMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KIKAYMD                  '�A���N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KIKAYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.KARIIDOKB                '���ٓ��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KARIIDOKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SHORITEISHIKB            '������~�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHORITEISHIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIYUBINNO              '�Z��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SHORIYOKUSHIKB           '�����}�~�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SHORIYOKUSHIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIJUSHOCD              '�Z��Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIJUSHO                '�Z��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIBANCHICD1            '�Z��Ԓn�R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIBANCHICD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIBANCHICD2            '�Z��Ԓn�R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIBANCHICD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIBANCHICD3            '�Z��Ԓn�R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIBANCHICD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIBANCHI               '�Z��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIKATAGAKIFG           '�Z������t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIKATAGAKIFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIKATAGAKICD           '�Z������R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIKATAGAKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIKATAGAKI             '�Z�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIGYOSEIKUCD           '�Z��s����R�[�h
                    '* ����ԍ� 000023 2005/12/26 �C���J�n
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* ����ԍ� 000023 2005/12/26 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIGYOSEIKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKIGYOSEIKUMEI          '�Z��s���於
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKIGYOSEIKUMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKICHIKUCD1             '�Z��n��R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUCD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKICHIKUMEI1            '�Z��n�於1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUMEI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKICHIKUCD2             '�Z��n��R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUCD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKICHIKUMEI2            '�Z��n�於2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUMEI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKICHIKUCD3             '�Z��n��R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUCD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.JUKICHIKUMEI3            '�Z��n�於3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_JUKICHIKUMEI3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KAOKUSHIKIKB             '�Ɖ��~�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KAOKUSHIKIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.BIKOZEIMOKU              '���l�Ŗ�
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_BIKOZEIMOKU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KOKUSEKICD               '���ЃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOKUSEKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KOKUSEKI                 '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOKUSEKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.ZAIRYUSKAKCD             '�ݗ����i�R�[�h
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYUSKAKCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.ZAIRYUSKAK               '�ݗ����i
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYUSKAK)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.ZAIRYUKIKAN              '�ݗ�����
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYUKIKAN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.ZAIRYU_ST_YMD            '�ݗ��J�n�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYU_ST_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.ZAIRYU_ED_YMD            '�ݗ��I���N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_ZAIRYU_ED_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRirekiEntity.RESERCE                  '���U�[�u
                    '�`�F�b�N�Ȃ�

                Case ABAtenaRirekiEntity.TANMATSUID               '�[���h�c
                    '* ����ԍ� 000012 2003/09/11 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000012 2003/09/11 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SAKUJOFG                 '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KOSHINCOUNTER            '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SAKUSEINICHIJI           '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.SAKUSEIUSER              '�쐬���[�U
                    '* ����ԍ� 000013 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000013 2003/10/09 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KOSHINNICHIJI            '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRirekiEntity.KOSHINUSER               '�X�V���[�U
                    '* ����ԍ� 000013 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000013 2003/10/09 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_RDBDATATYPE_KOSHINUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

            End Select

            ' �f�o�b�O���O�o��
            'm_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

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
    '* ���\�b�h��     ����Get�p�̍��ڂ�ҏW
    '* 
    '* �\��           Private SetAtenaEntity(ByRef strSql As StringBuilder)
    '* 
    '* �@�\           ����Get�p�̍��ڂ�ҏW����B
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetAtenaEntity(ByRef strAtenaSQLsb As StringBuilder)
        If (Me.m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly) Then
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KYUSHICHOSONCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATASHU).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANYOKB1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KJNHJNKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANYOKB2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANNAIKANGAIKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANAMEISHO1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANAMEISHO2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIHJNKEITAI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANJIMEISHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEIMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANASEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEARCHKANAMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREWMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEIBETSUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEIBETSU).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SEKINO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINHYOHYOJIJUN).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZOKUGARACD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZOKUGARA).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2ZOKUGARACD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2ZOKUGARA).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAINUSJUMINCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANASTAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2STAINUSJUMINCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.DAI2STAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANADAI2STAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.YUBINNO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHOCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKIFG).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TOROKUIDOYMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TOROKUJIYUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TOROKUJIYU).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOIDOYMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOJIYUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOJIYU).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIYUBINNO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHOCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKIFG).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI3)

            '*����ԍ� 000030 2010/05/14 �ǉ��J�n
            ' �{�ЕM���ҏ�񒊏o����
            If (m_strHonsekiHittoshKB = "1" AndAlso m_strHonsekiHittoshKB_Param = "1") Then
                ' �{�ЏZ���A�{�ДԒn�A�M���҂𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HON_JUSHO).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HONSEKIBANCHI).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HITTOSH)
            Else
            End If

            ' ������~�敪���o����
            If (m_strShoriteishiKB = "1" AndAlso m_strShoriTeishiKB_Param = "1") Then
                ' ������~�敪�𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHORITEISHIKB)
            Else
            End If
            '*����ԍ� 000030 2010/05/14 �ǉ��I��

            '*����ԍ� 000031 2011/05/18 �ǉ��J�n
            If (m_strFrnZairyuJohoKB_Param = "1") Then
                ' �O���l�ݗ����(���ЁA�ݗ����i�R�[�h�A�ݗ����i�A�ݗ����ԁA�ݗ��J�n�N�����A�ݗ��I���N����)�𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKI).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUSKAKCD).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUSKAK).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUKIKAN).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYU_ST_YMD).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYU_ED_YMD)
            Else
            End If
            '*����ԍ� 000031 2011/05/18 �ǉ��I��
        Else
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KYUSHICHOSONCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATAKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.STAICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ATENADATASHU).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANYOKB1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KJNHJNKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANYOKB2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANNAIKANGAIKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANAMEISHO1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANAMEISHO2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIHJNKEITAI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREYMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.UMAREWMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANASTAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANADAI2STAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.YUBINNO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHOCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUSHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHICD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.BANCHI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKIFG).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KATAGAKI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RENRAKUSAKI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.GYOSEIKUMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUCD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CHIKUMEI3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIYUBINNO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHOCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIJUSHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHICD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIBANCHI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKIFG).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIKATAGAKI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKIGYOSEIKUMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUCD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUKICHIKUMEI3)

            '*����ԍ� 000030 2010/05/14 �ǉ��J�n
            ' �{�ЕM���ҏ�񒊏o����
            If (m_strHonsekiHittoshKB = "1" AndAlso m_strHonsekiHittoshKB_Param = "1") Then
                ' �{�ЏZ���A�{�ДԒn�A�M���҂𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HON_JUSHO).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HONSEKIBANCHI).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HITTOSH)
            Else
            End If

            ' ������~�敪���o����
            If (m_strShoriteishiKB = "1" AndAlso m_strShoriTeishiKB_Param = "1") Then
                ' ������~�敪�𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHORITEISHIKB)
            Else
            End If
            '*����ԍ� 000030 2010/05/14 �ǉ��I��

            '*����ԍ� 000031 2011/05/18 �ǉ��J�n
            If (m_strFrnZairyuJohoKB_Param = "1") Then
                ' �O���l�ݗ����(���ЁA�ݗ����i�R�[�h�A�ݗ����i�A�ݗ����ԁA�ݗ��J�n�N�����A�ݗ��I���N����)�𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKI).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUSKAKCD).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUSKAK).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYUKIKAN).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYU_ST_YMD).Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.ZAIRYU_ED_YMD)
            Else
            End If
            '*����ԍ� 000031 2011/05/18 �ǉ��I��

        End If
        If (Me.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll) Then
            strAtenaSQLsb.Append(",")
            ' ����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KYUSEI).Append(",")
            ' �Z��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEIIDOYMD).Append(",")
            ' �Z�莖�R
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEIJIYU).Append(",")
            ' �]���O�Z���X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_YUBINNO).Append(",")
            ' �]���O�Z���S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD).Append(",")
            ' �]���O�Z���Z��
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_JUSHO).Append(",")
            ' �]���O�Z���Ԓn
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_BANCHI).Append(",")
            ' �]���O�Z������
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI).Append(",")
            ' �]�o�\��X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO).Append(",")
            ' �]�o�\��S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD).Append(",")
            ' �]�o�\��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD).Append(",")
            ' �]�o�\��Z��
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO).Append(",")
            ' �]�o�\��Ԓn
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI).Append(",")
            ' �]�o�\�����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI).Append(",")
            ' �]�o�m��X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO).Append(",")
            ' �]�o�m��S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD).Append(",")
            ' �]�o�m��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD).Append(",")
            ' �]�o�m��ʒm�N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD).Append(",")
            ' �]�o�m��Z��
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO).Append(",")
            ' �]�o�m��Ԓn
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI).Append(",")
            ' �]�o�m�����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI).Append(",")

            ' �����͏o�N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOTDKDYMD).Append(",")
            ' ���ߎ��R�R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CKINJIYUCD).Append(",")

            ' �{�БS���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HON_ZJUSHOCD).Append(",")
            ' �]�o�\�萢�ю喼
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI).Append(",")
            ' �]�o�m�萢�ю喼
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI).Append(",")
            '*����ԍ� 000024 2006/07/31 �ǉ��J�n
            ' ���ЃR�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKICD).Append(",")
            ' �]���O�Z�����ю喼
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI)
            'strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKICD)
            '*����ԍ� 000024 2006/07/31 �ǉ��I��

        End If

        '*����ԍ� 000025 2007/04/28 �ǉ��J�n
        If m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo Then
            strAtenaSQLsb.Append(",")
            ' ����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KYUSEI).Append(",")
            ' �Z��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEIIDOYMD).Append(",")
            ' �Z�莖�R
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEIJIYU).Append(",")
            ' �]���O�Z���X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_YUBINNO).Append(",")
            ' �]���O�Z���S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD).Append(",")
            ' �]���O�Z���Z��
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_JUSHO).Append(",")
            ' �]���O�Z���Ԓn
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_BANCHI).Append(",")
            ' �]���O�Z������
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI).Append(",")
            ' �]�o�\��X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO).Append(",")
            ' �]�o�\��S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD).Append(",")
            ' �]�o�\��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD).Append(",")
            ' �]�o�\��Z��
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO).Append(",")
            ' �]�o�\��Ԓn
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI).Append(",")
            ' �]�o�\�����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI).Append(",")
            ' �]�o�m��X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO).Append(",")
            ' �]�o�m��S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD).Append(",")
            ' �]�o�m��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD).Append(",")
            ' �]�o�m��ʒm�N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD).Append(",")
            ' �]�o�m��Z��
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO).Append(",")
            ' �]�o�m��Ԓn
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI).Append(",")
            ' �]�o�m�����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI).Append(",")
            ' �����͏o�N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHOJOTDKDYMD).Append(",")
            ' ���ߎ��R�R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CKINJIYUCD).Append(",")
            ' �{�БS���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HON_ZJUSHOCD).Append(",")
            ' �]�o�\�萢�ю喼
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI).Append(",")
            ' �]�o�m�萢�ю喼
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI).Append(",")
            ' ���ЃR�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKICD).Append(",")
            ' �o�^�͏o�N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TOROKUTDKDYMD).Append(",")
            ' �Z��͏o�N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTEITDKDYMD).Append(",")
            ' �]�o�����R
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.TENSHUTSUNYURIYU).Append(",")
            ' �s�����R�[�h
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SHICHOSONCD).Append(",")
            ' ���߈ٓ��N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CKINIDOYMD).Append(",")
            ' �X�V����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOSHINNICHIJI)
        End If
        '*����ԍ� 000025 2007/04/28 �ǉ��I��
        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
            strAtenaSQLsb.Append(",")
            ' ���ߓ͏o�ʒm�敪
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.CKINTDKDTUCIKB).Append(",")
            ' �Ŕԍ�
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.HANNO).Append(",")
            ' �����N����
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KAISEIYMD)
            If (m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo) AndAlso
               (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.NenkinAll) Then
                ' ���ЃR�[�h
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KOKUSEKICD)
            End If
        End If
    End Sub
    '************************************************************************************************
    '* ���\�b�h��     ����Get�p�̌ʎ������ڂ�ҏW
    '* 
    '* �\��           Private SetKobetsuaEntity(ByRef strSql As StringBuilder)
    '* 
    '* �@�\           ����Get�p�̍��ڂ�ҏW����B
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetKobetsuEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.KSNENKNNO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KSNENKNNO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKSHU)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKSHU)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSHUTKRIYUCD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.NENKNSKAKSSHTSRIYUCD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO1)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO1)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO1)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO1)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU1)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU1)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN1)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN1)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB1)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB1)

        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO2)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO2)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO2)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO2)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU2)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU2)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN2)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN2)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB2)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB2)

        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKIGO3)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKIGO3)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNNO3)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNNO3)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNSHU3)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNSHU3)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNEDABAN3)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNEDABAN3)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JKYNENKNKB3)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JKYNENKNKB3)
        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.HIHOKENSHAGAITOKB)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.NENKINHIHOKENSHAGAITOKB)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.SHUBETSUHENKOYMD)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.NENKINSHUBETSUHENKOYMD)
        End If

        ' ����
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHONO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHONO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKB)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBMEISHO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHIKAKUKBRYAKUSHO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKB)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBMEISHO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOGAKUENKBRYAKUSHO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSHUTOKUYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOSOSHITSUYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKB)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBMEISHO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKKBRYAKUSHO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKB)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBMEISHO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKGAITOYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOTISHKHIGAITOYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHOKIGO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKUHOHOKENSHONO)
        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.HIHOKENSHAGAITOKB)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.KOKUHOHIHOKENSHAGAITOKB)
        End If

        ' ���
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANNO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANNO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.INKANTOROKUKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.INKANTOROKUKB)

        ' �I��
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.SENKYOSHIKAKUKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.SENKYOSHIKAKUKB)
        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.TOROKUJOTAIKBN)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.SENKYOTOROKUJOTAIKBN)
        End If

        ' �����蓖
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEHIYOKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEHIYOKB)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATESTYM)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATESTYM)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JIDOTEATEEDYM)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.JIDOTEATEEDYM)

        ' ���
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.HIHKNSHANO)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGHIHKNSHANO)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSHUTKYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSHUTKYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKSSHTSYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKSSHTSYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.SKAKHIHOKENSHAKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKHIHOKENSHAKB)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUSHOCHITKRIKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUSHOCHITKRIKB)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUSHAKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUSHAKB)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.YOKAIGJOTAIKBCD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.YOKAIGJOTAIKBCD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.KAIGSKAKKB)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGSKAKKB)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEIKAISHIYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEIKAISHIYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.NINTEISHURYOYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGNINTEISHURYOYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEIYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEIYMD)
        strAtenaSQLsb.Append(", ")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD)
        strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KAIGJUKYUNINTEITORIKESHIYMD)
        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.HIHOKENSHAGAITOKB)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.KAIGOHIHOKENSHAGAITOKB)
        End If

        '*����ԍ� 000028 2008/01/17 �ǉ��J�n
        ' �������
        If (m_strKobetsuShutokuKB = "1") Then
            ' �ʎ����擾�敪��"1"�̏ꍇ�A�������҃}�X�^���ڂ��擾����
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SHIKAKUKB)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISHIKAKUKB)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.HIHKNSHANO)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREIHIHKNSHANO)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSHUTKJIYUCD)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUCD)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSHUTKJIYUMEI)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKJIYUMEI)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSHUTKYMD)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSHUTKYMD)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSSHTSJIYUCD)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUCD)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSSHTSJIYUMEI)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSJIYUMEI)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.SKAKSSHTSYMD)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREISKAKSSHTSYMD)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.TEKIYOKAISHIYMD)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREITEKIYOKAISHIYMD)
            strAtenaSQLsb.Append(", ")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.TEKIYOSHURYOYMD)
            strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuEntity.KOKIKOREITEKIYOSHURYOYMD)
            If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                strAtenaSQLsb.Append(", ")
                strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.HIHOKENSHAGAITOKB)
                strAtenaSQLsb.Append(" AS ").Append(ABAtena1KobetsuHyojunEntity.KOKIKOREIHIHOKENSHAGAITOKB)
            End If
        Else
            ' �ʎ����擾�敪���l�����̏ꍇ�A�������s��Ȃ�
        End If
        '*����ԍ� 000028 2008/01/17 �ǉ��I��
    End Sub
    '************************************************************************************************
    '* ���\�b�h��     ����Get�p��COUNTEntity��ҏW
    '* 
    '* �\��           Private SetAtenaCountEntity()
    '* 
    '* �@�\           ����Get�p�̍��ڂ�ҏW����B
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetAtenaCountEntity(ByRef strAtenaSQLsb As StringBuilder)
        If (Me.m_blnSelectCount = True) Then
            If (Me.m_blnSelectAll <> ABEnumDefine.AtenaGetKB.NenkinAll) Then
                strAtenaSQLsb.Append(",B.")
                strAtenaSQLsb.Append(ABAtenaCountEntity.DAINOCOUNT)
                strAtenaSQLsb.Append(",C.")
                strAtenaSQLsb.Append(ABAtenaCountEntity.SFSKCOUNT)
            End If
            strAtenaSQLsb.Append(",D.")
            strAtenaSQLsb.Append(ABAtenaCountEntity.RENERAKUSAKICOUNT)
        End If
    End Sub
    '*����ԍ� 000032 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ��������t���f�[�^���ڕҏW
    '* 
    '* �\��           Private SetFZYEntity()
    '* 
    '* �@�\           ��������t���f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TABLEINSERTKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.LINKNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUMINHYOJOTAIKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKYOCHITODOKEFLG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.HONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANAHONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANJIHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANJITSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KATAKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.UMAREFUSHOKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TSUSHOMEITOUROKUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUKIKANCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUKIKANMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUSHACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUSHAMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.ZAIRYUCARDNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KOFUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KOFUYOTEISTYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.KOFUYOTEIEDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOJIYU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.FRNSTAINUSMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.FRNSTAINUSKANAMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.STAINUSKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.STAINUSKANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE5)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE6)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE7)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE8)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE9)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RESERVE10)
    End Sub

    '*����ԍ� 000033 2014/04/28 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���ʔԍ��f�[�^���ڕҏW
    '* 
    '* �\��           Private SetMyNumberEntity()
    '* 
    '* �@�\           ���ʔԍ��f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetMyNumberEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.MYNUMBER)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.CKINKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.IDOKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.IDOYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.IDOSHA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.RESERVE)
    End Sub
    '*����ԍ� 000033 2014/04/28 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     ����Get�p��JOIN���ҏW
    '* 
    '* �\��           Private SetAtenaJoin()
    '* 
    '* �@�\           ����Get�p�̍��ڂ�ҏW����B
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetAtenaJoin(ByRef strAtenaSQLsb As StringBuilder)
        If (Me.m_blnSelectCount = True) Then
            If (Me.m_blnSelectAll <> ABEnumDefine.AtenaGetKB.NenkinAll) Then
                strAtenaSQLsb.Append(" LEFT OUTER JOIN (SELECT ")
                strAtenaSQLsb.Append(ABDainoEntity.JUMINCD)
                strAtenaSQLsb.Append(",COUNT(*) AS ")
                strAtenaSQLsb.Append(ABAtenaCountEntity.DAINOCOUNT)
                strAtenaSQLsb.Append(" FROM ")
                strAtenaSQLsb.Append(ABDainoEntity.TABLE_NAME)
                strAtenaSQLsb.Append(" GROUP BY ")
                strAtenaSQLsb.Append(ABDainoEntity.JUMINCD)
                strAtenaSQLsb.Append(") B ON ")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME)
                strAtenaSQLsb.Append(".")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.JUMINCD)
                strAtenaSQLsb.Append(" = B.")
                strAtenaSQLsb.Append(ABDainoEntity.JUMINCD)
                strAtenaSQLsb.Append(" LEFT OUTER JOIN (SELECT ")
                strAtenaSQLsb.Append(ABSfskEntity.JUMINCD)
                strAtenaSQLsb.Append(",COUNT(*) AS ")
                strAtenaSQLsb.Append(ABAtenaCountEntity.SFSKCOUNT)
                strAtenaSQLsb.Append(" FROM ")
                strAtenaSQLsb.Append(ABSfskEntity.TABLE_NAME)
                strAtenaSQLsb.Append(" GROUP BY ")
                strAtenaSQLsb.Append(ABSfskEntity.JUMINCD)
                strAtenaSQLsb.Append(") C ON ")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME)
                strAtenaSQLsb.Append(".")
                strAtenaSQLsb.Append(ABAtenaRirekiEntity.JUMINCD)
                strAtenaSQLsb.Append(" = C.")
                strAtenaSQLsb.Append(ABSfskEntity.JUMINCD)
            End If
            strAtenaSQLsb.Append(" LEFT OUTER JOIN (SELECT ")
            strAtenaSQLsb.Append(ABRenrakusakiEntity.JUMINCD)
            strAtenaSQLsb.Append(",COUNT(*) AS ")
            strAtenaSQLsb.Append(ABAtenaCountEntity.RENERAKUSAKICOUNT)
            strAtenaSQLsb.Append(" FROM ")
            strAtenaSQLsb.Append(ABRenrakusakiEntity.TABLE_NAME)
            strAtenaSQLsb.Append(" GROUP BY ")
            strAtenaSQLsb.Append(ABRenrakusakiEntity.JUMINCD)
            strAtenaSQLsb.Append(") D ON ")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME)
            strAtenaSQLsb.Append(".")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.JUMINCD)
            strAtenaSQLsb.Append(" = D.")
            strAtenaSQLsb.Append(ABRenrakusakiEntity.JUMINCD)
        End If
    End Sub
    '************************************************************************************************
    '* ���\�b�h��     ����Get�p�̌ʎ���JOIN���ҏW
    '* 
    '* �\��           Private SetKobetsuJoin()
    '* 
    '* �@�\           ����Get�p�̍��ڂ�ҏW����B
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetKobetsuJoin(ByRef strAtenaSQLsb As StringBuilder)

        ' LEFT OUTER JOIN ABATENANENKIN ON ABATENA.JUMINCD=ABATENANENKIN.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaNenkinEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENAKOKUHO ON ABATENA.JUMINCD=ABATENAKOKUHO.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENAINKAN ON ABATENA.JUMINCD=ABATENAINKAN.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaInkanEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENASENKYO ON ABATENA.JUMINCD=ABATENASENKYO.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENAJITE ON ABATENA.JUMINCD=ABATENAJIDOUTE.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaJiteEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENAKAIGO ON ABATENA.JUMINCD=ABATENAKAIGO.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKaigoEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUMINCD)

        '*����ԍ� 000025 2008/01/15 �ǉ��J�n
        If (m_strKobetsuShutokuKB = "1") Then
            ' �ʎ����擾�敪��"1"�̏ꍇ�A�������҃}�X�^��JOIN����
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(" ON ")
            strAtenaSQLsb.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            strAtenaSQLsb.Append("=")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.JUMINCD)
        Else
            ' �ʎ����擾�敪���l�����̏ꍇ�A�������s��Ȃ�
        End If
        '*����ԍ� 000025 2008/01/15 �ǉ��I��
    End Sub
    '*����ԍ� 000032 2011/10/24 �ǉ��J�n
   '************************************************************************************************
    '* ���\�b�h��     ��������t���e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetFZYJoin()
    '* 
    '* �@�\           ��������t���e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaRirekiFZYEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", _
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, _
                                    ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUMINCD)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", _
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RIREKINO, _
                                    ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.RIREKINO)
    End Sub
    '*����ԍ� 000032 2011/10/24 �ǉ��I��

    '*����ԍ� 000033 2014/04/28 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���ʔԍ��e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetMyNumberJoin()
    '* 
    '* �@�\           ���ʔԍ��e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetMyNumberJoin(ByRef strAtenaSQLsb As StringBuilder)
        ' ���ʔԍ��e�[�u���͒��߃��R�[�h�݂̂���������B
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ")
        strAtenaSQLsb.AppendFormat("(SELECT * FROM {0} WHERE {1} = '{2}') AS {0} ", _
                                    ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.CKINKB, ABMyNumberEntity.DEFAULT.CKINKB.CKIN)
        strAtenaSQLsb.AppendFormat("ON {0}.{1} = {2}.{3} ", _
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, _
                                    ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.JUMINCD)
    End Sub
    '*����ԍ� 000033 2014/04/28 �ǉ��I��

    '* ����ԍ� 000021 2005/06/17 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ����ԍ��̎擾
    '* 
    '* �\��           Private Function GetRirekiNo(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\           ����ԍ��̎擾���s��
    '* 
    '* ����           strJuminCD As string : �ΏۂƂȂ�Z���b�c
    '* 
    '* �߂�l         csRirekiNoDataEntity as DataSet:����ԍ�
    '************************************************************************************************
    Public Function GetRirekiNo(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetRirekiNo"
        Dim csRirekiNoDataEntity As DataSet                '����ԍ��f�[�^�Z�b�g
        Dim strGetRirekiNoSQL As StringBuilder        '�r�p�k��
        Dim cfUFParameterClass As UFParameterClass      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass    '�p�����[�^�R���N�V�����N���X

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            '�r�p�k���̍쐬
            strGetRirekiNoSQL = New StringBuilder
            strGetRirekiNoSQL.Append("SELECT ")
            strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.RIREKINO)
            strGetRirekiNoSQL.Append(" FROM ")
            strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.TABLE_NAME)
            strGetRirekiNoSQL.Append(" WHERE ")
            strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.JUMINCD)
            strGetRirekiNoSQL.Append(" = ")
            strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.PARAM_JUMINCD)
            strGetRirekiNoSQL.Append(" ORDER BY ")
            strGetRirekiNoSQL.Append(ABAtenaRirekiEntity.RIREKINO)
            strGetRirekiNoSQL.Append(" DESC ")

            '�p�����[�^�N���X�̃C���X�^���X��
            cfUFParameterClass = New UFParameterClass
            '�p�����[�^�̃Z�b�g
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            '�p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            cfUFParameterCollectionClass = New UFParameterCollectionClass
            '�p�����[�^�R���N�V�����ɃZ�b�g
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '����ԍ��̎擾
            csRirekiNoDataEntity = m_cfRdbClass.GetDataSet(strGetRirekiNoSQL.ToString, ABAtenaRirekiEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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

        Return csRirekiNoDataEntity

    End Function
    '* ����ԍ� 000021 2005/06/17 �ǉ��I��

    '*����ԍ� 000030 2010/05/14 �ǉ��J�n
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
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            If (cABAtenaKanriJoho Is Nothing) Then
                cABAtenaKanriJoho = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            End If

            ' �{�Ў擾�敪�擾
            m_strHonsekiHittoshKB = cABAtenaKanriJoho.GetHonsekiKB_Param

            ' ������~�敪�擾�敪�擾
            m_strShoriteishiKB = cABAtenaKanriJoho.GetShoriteishiKB_Param

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try

    End Sub
    '*����ԍ� 000030 2010/05/14 �ǉ��I��

    '*����ԍ� 000032 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��       �Z��@�����׸ގ擾
    '* 
    '* �\��             Private Function GetJukihoKaiseiFG()
    '* 
    '* �@�\�@�@    �@   �Ǘ������擾����
    '* 
    '* ����             �Ȃ�
    '* 
    '* �߂�l           �Ȃ�
    '************************************************************************************************
    Private Sub GetJukihoKaiseiFG()
        Const THIS_METHOD_NAME As String = "GetJukihoKaiseiFG"
        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (IsNothing(m_csSekoYMDHanteiB)) Then
                '�{�s������a�׽�̲ݽ�ݽ��
                m_csSekoYMDHanteiB = New ABSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '�Z��@�����׸ށ��{�s�����茋��
                m_blnJukihoKaiseiFG = m_csSekoYMDHanteiB.CheckAfterSekoYMD
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try
    End Sub
    '*����ԍ� 000032 2011/10/24 �ǉ��I��

    '*����ԍ� 000033 2014/04/28 �ǉ��J�n
    ''' <summary>
    ''' ���ʔԍ��@�����擾�@���ߌ����敪�擾
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetMyNumberChokkinSearchKB()

        Dim cABAtenaKanriJoho As ABAtenaKanriJohoBClass

        Try

            ' �����Ǘ����r�W�l�X�N���X�̃C���X�^���X��
            cABAtenaKanriJoho = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' ���ʔԍ��@�����擾�@���ߌ����敪�̎擾
            m_strMyNumberChokkinSearchKB_Param = cABAtenaKanriJoho.GetMyNumberChokkinSearchKB_Param()

        Catch csExp As Exception
            Throw
        End Try

    End Sub
    '*����ԍ� 000033 2014/04/28 �ǉ��I��

#End Region

    '*����ԍ� 000035 2015/05/08 �ǉ��J�n
#Region "���������}�X�^���o(�ԍ��ꊇ�擾�o�b�`����ďo)"
    ''' <summary>
    ''' ���������}�X�^���o
    ''' </summary>
    ''' <param name="cSearchKey">���������}�X�^�����L�[</param>
    ''' <returns>�擾�������������}�X�^�̒��߃f�[�^</returns>
    Public Function CreateRuisekiData(ByVal cSearchKey As ABAtenaSearchKey) As DataSet
        Const THIS_METHOD_NAME As String = "CreateRuisekiData"
        Dim csAtenaRirekiEntity As DataSet                  '���������f�[�^�Z�b�g
        Dim strSQL As New StringBuilder()
        Dim strAtenaSQLsbWhere As StringBuilder
        Dim strORDER As StringBuilder
        Dim cfUFParameterClass As UFParameterClass
        Dim cfSelectUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (m_strAtenaSQLsbAll.ToString = String.Empty) Then
                '����SQL�쐬
                Call GetRirekiSQLString()
            End If
            strSQL.Append(m_strAtenaSQLsbAll)

            If (m_csDataSchmaAll Is Nothing) Then
                m_csDataSchmaAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)
            End If
            m_csDataSchma = m_csDataSchmaAll

            'Where��쐬(�Z���R�[�h/�Z�o�O�D��敪)
            strAtenaSQLsbWhere = New StringBuilder
            strAtenaSQLsbWhere.Append(" WHERE ")
            strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            strAtenaSQLsbWhere.Append(" = ")
            strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.KEY_JUMINCD)
            strAtenaSQLsbWhere.Append(" AND ")
            strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUTOGAIYUSENKB)
            strAtenaSQLsbWhere.Append(" = ")
            strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.KEY_JUTOGAIYUSENKB)
            strAtenaSQLsbWhere.Append(" AND ")
            strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.RRKED_YMD)
            strAtenaSQLsbWhere.Append(" = ")
            strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.KEY_RRKED_YMD)
            strAtenaSQLsbWhere.Append(" AND ")
            strAtenaSQLsbWhere.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.SAKUJOFG)
            strAtenaSQLsbWhere.Append(" <> '1' ")

            'ORDER BY��쐬(�Z���R�[�h)
            strORDER = New StringBuilder()
            strORDER.Append(" ORDER BY ")
            strORDER.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.JUMINCD)
            strORDER.Append(" ASC;")

            strSQL.Append(strAtenaSQLsbWhere)
            strSQL.Append(strORDER)

            ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            cfSelectUFParameterCollectionClass = New UFParameterCollectionClass
            '�p�����[�^(�Z���R�[�h)
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = cSearchKey.p_strJuminCD
            cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '�p�����[�^(�Z�o�O�D��敪)
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUTOGAIYUSENKB
            cfUFParameterClass.Value = cSearchKey.p_strJutogaiYusenKB
            cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '�p�����[�^(�����I���N����)
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RRKED_YMD
            cfUFParameterClass.Value = "99999999"
            cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' SQL�̎��s DataSet�̎擾
            csAtenaRirekiEntity = m_csDataSchma.Clone()
            csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, cfSelectUFParameterCollectionClass, False)

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

    ''' <summary>
    ''' SQL��������擾����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRirekiSQLString()
        Const THIS_METHOD_NAME As String = "GetRirekiSQLString"

        Try
            m_strAtenaSQLsbAll.Append("SELECT ")

            '��������t��
            Call SetRirekiEntity(m_strAtenaSQLsbAll)

            '�����N���t��
            Call SetNenkinEntity(m_strAtenaSQLsbAll)

            '�������ەt��
            Call SetKokuhoEntity(m_strAtenaSQLsbAll)

            'FROM��
            m_strAtenaSQLsbAll.Append(" FROM ")
            m_strAtenaSQLsbAll.Append(ABAtenaRirekiEntity.TABLE_NAME)

            '�����N���}�X�^��t��
            Call SetNENKINJoin(m_strAtenaSQLsbAll)

            '�������ۃ}�X�^��t��
            Call SetKOKUHOJoin(m_strAtenaSQLsbAll)

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

#Region "���������f�[�^���ڕҏW"
    ''' <summary>
    ''' ���������f�[�^���ڕҏW
    ''' </summary>
    ''' <param name="strAtenaSQLsb">�����擾�pSQL</param>
    ''' <remarks></remarks>
    Private Sub SetRirekiEntity(ByRef strAtenaSQLsb As StringBuilder)
        Const THIS_METHOD_NAME As String = "SetRirekiEntity"
        Try
            With strAtenaSQLsb
                .AppendFormat("{0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHICHOSONCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KYUSHICHOSONCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RIREKINO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RRKST_YMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RRKED_YMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINJUTOGAIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINYUSENIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTOGAIYUSENKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ATENADATAKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.STAICD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINHYOCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEIRINO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ATENADATASHU)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HANYOKB1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KJNHJNKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HANYOKB2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANNAIKANGAIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANAMEISHO1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIMEISHO1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANAMEISHO2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIMEISHO2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIHJNKEITAI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEARCHKANJIMEISHO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KYUSEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEARCHKANASEIMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEARCHKANASEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEARCHKANAMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIRRKNO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.UMAREYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.UMAREWMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEIBETSUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEIBETSU)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SEKINO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINHYOHYOJIJUN)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZOKUGARACD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZOKUGARA)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2ZOKUGARACD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2ZOKUGARA)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.STAINUSJUMINCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.STAINUSMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANASTAINUSMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2STAINUSJUMINCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.DAI2STAINUSMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANADAI2STAINUSMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.YUBINNO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUSHOCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BANCHICD1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BANCHICD2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BANCHICD3)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BANCHI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KATAGAKIFG)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KATAGAKICD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KATAGAKI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RENRAKUSAKI1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RENRAKUSAKI2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HON_ZJUSHOCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HON_JUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HONSEKIBANCHI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HITTOSH)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINIDOYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINJIYUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINJIYU)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINTDKDYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CKINTDKDTUCIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUIDOYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUIDOWMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUJIYUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUJIYU)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUTDKDYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUTDKDWMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOROKUTDKDTUCIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEIIDOYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEIIDOWMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEIJIYUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEIJIYU)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEITDKDYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEITDKDWMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUTEITDKDTUCIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOIDOYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOJIYUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOJIYU)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOTDKDYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOJOTDKDTUCIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUNYURIYUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUNYURIYU)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_YUBINNO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_JUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_BANCHI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TENSHUTSUKKTIMITDKFG)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BIKOYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BIKO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BIKOTENSHUTSUKKTIJUSHOFG)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HANNO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KAISEIATOFG)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KAISEIMAEFG)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KAISEIYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.GYOSEIKUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.GYOSEIKUMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUCD1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUMEI1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUCD2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUMEI2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUCD3)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHIKUMEI3)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TOHYOKUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHOGAKKOKUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.CHUGAKKOKUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.HOGOSHAJUMINCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANJIHOGOSHAMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KANAHOGOSHAMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KIKAYMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KARIIDOKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHORITEISHIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIYUBINNO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SHORIYOKUSHIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIJUSHOCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIJUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIBANCHICD1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIBANCHICD2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIBANCHICD3)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIBANCHI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIKATAGAKIFG)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIKATAGAKICD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIKATAGAKI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIGYOSEIKUCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUCD1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUMEI1)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUCD2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUMEI2)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUCD3)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUKICHIKUMEI3)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KAOKUSHIKIKB)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.BIKOZEIMOKU)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOKUSEKICD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOKUSEKI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYUSKAKCD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYUSKAK)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYUKIKAN)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYU_ST_YMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.ZAIRYU_ED_YMD)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RESERCE)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.TANMATSUID)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SAKUJOFG)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOSHINCOUNTER)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SAKUSEINICHIJI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.SAKUSEIUSER)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOSHINNICHIJI)
                .AppendFormat(", {0}.{1}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.KOSHINUSER)
            End With
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

#Region "�����N���f�[�^���ڕҏW"
    ''' <summary>
    ''' �N���f�[�^���ڕҏW
    ''' </summary>
    ''' <param name="strAtenaSQLsb">�����擾�pSQL</param>
    ''' <remarks></remarks>
    Private Sub SetNenkinEntity(ByRef strAtenaSQLsb As StringBuilder)
        Const THIS_METHOD_NAME As String = "SetNenkinEntity"
        Try
            With strAtenaSQLsb
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.KSNENKNNO)
                .AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSHUTKYMD, ABAtenaRuisekiEntity.NENKNSKAKSHUTKYMD)
                .AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSHUTKSHU, ABAtenaRuisekiEntity.NENKNSKAKSHUTKSHU)
                .AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSHUTKRIYUCD, ABAtenaRuisekiEntity.NENKNSKAKSHUTKRIYUCD)
                .AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSSHTSYMD, ABAtenaRuisekiEntity.NENKNSKAKSSHTSYMD)
                .AppendFormat(", {0}.{1} AS {2}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.SKAKSSHTSRIYUCD, ABAtenaRuisekiEntity.NENKNSKAKSSHTSRIYUCD)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKIGO1)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNNO1)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNSHU1)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNEDABAN1)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKB1)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKIGO2)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNNO2)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNSHU2)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNEDABAN2)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKB2)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKIGO3)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNNO3)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNSHU3)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNEDABAN3)
                .AppendFormat(", {0}.{1}", ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JKYNENKNKB3)
            End With
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

#Region "���ۃf�[�^���ڕҏW"
    ''' <summary>
    ''' ���ۃf�[�^���ڕҏW
    ''' </summary>
    ''' <param name="strAtenaSQLsb">�����擾�pSQL</param>
    ''' <remarks></remarks>
    Private Sub SetKokuhoEntity(ByRef strAtenaSQLsb As StringBuilder)
        Const THIS_METHOD_NAME As String = "SetKokuhoEntity"
        Try
            With strAtenaSQLsb
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHONO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOGAKUENKB)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKKB)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO)
                .AppendFormat(", {0}.{1}", ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.KOKUHOHOKENSHONO)
            End With
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

#Region "�����N��JOIN��쐬"
    ''' <summary>
    ''' �����N���e�[�u����JOIN����쐬
    ''' </summary>
    ''' <param name="strAtenaSQLsb">�����擾�pSQL</param>
    ''' <remarks></remarks>
    Private Sub SetNENKINJoin(ByRef strAtenaSQLsb As StringBuilder)
        Const THIS_METHOD_NAME As String = "SetNENKINJoin"
        Try
            With strAtenaSQLsb
                .AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaNenkinEntity.TABLE_NAME)
                .AppendFormat(" ON {0}.{1} = {2}.{3} ", _
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, _
                                    ABAtenaNenkinEntity.TABLE_NAME, ABAtenaNenkinEntity.JUMINCD)
            End With
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

#Region "��������JOIN��쐬"
    ''' <summary>
    ''' �������ۃe�[�u����JOIN����쐬
    ''' </summary>
    ''' <param name="strAtenaSQLsb">�����擾�pSQL</param>
    ''' <remarks></remarks>
    Private Sub SetKOKUHOJoin(ByRef strAtenaSQLsb As StringBuilder)
        Const THIS_METHOD_NAME As String = "SetKOKUHOJoin"
        Try
            strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaKokuhoEntity.TABLE_NAME)
            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", _
                                        ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD, _
                                        ABAtenaKokuhoEntity.TABLE_NAME, ABAtenaKokuhoEntity.JUMINCD)
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

#End Region
    '*����ԍ� 000035 2015/05/08 �ǉ��I��

    '*����ԍ� 000037 2023/03/10 �ǉ��J�n
#Region "��������W���f�[�^���ڕҏW"
    '************************************************************************************************
    '* ���\�b�h��     ��������W���f�[�^���ڕҏW
    '* 
    '* �\��           Private SetHyojunEntity()
    '* 
    '* �@�\           ��������W���f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetHyojunEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.EDANO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHIMEIKANAKAKUNINFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.UMAREBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOUMAREBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JIJITSUSTAINUSMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.MACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SEARCHJUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KANAKATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SEARCHKATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.BANCHIEDABANSUCHI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUSHO_KUNIMEICODE)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUSHO_KUNIMEITO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUSHO_KOKUGAIJUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_SHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_MACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_SHIKUGUNCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HON_MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CKINIDOWMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CKINIDOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TOROKUIDOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOTOROKUIDOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HYOJUNKISAIJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KISAIYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KISAIBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOKISAIBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUTEIIDOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOJUTEIIDOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HYOJUNSHOJOJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KOKUSEKISOSHITSUBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHOJOIDOWMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHOJOIDOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOSHOJOIDOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_SHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKICD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUSEKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENUMAEJ_KOKUGAIJUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_YUBINNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_SHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_MACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_SHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_BANCHI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUTJ_KATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_SHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_BANCHI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SAISHUJ_KATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEITODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIMACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUSEKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTITODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTISHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TENSHUTSUKKTIMACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KAISEIBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOKAISEIBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KAISEISHOJOYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KAISEISHOJOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOKAISEISHOJOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD4)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD5)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD6)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD7)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD8)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD9)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.CHIKUCD10)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TOKUBETSUYOSHIKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HYOJUNIDOKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.NYURYOKUBASHOCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.NYURYOKUBASHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SEARCHKANJIKYUUJI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SEARCHKANAKYUUJI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KYUUJIKANAKAKUNINFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TDKDSHIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.HYOJUNIDOJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.NICHIJOSEIKATSUKENIKICD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TOROKUBUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TANKITAIZAISHAFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.KYOYUNINZU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHIZEIJIMUSHOCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHUKKOKUKIKAN_ST)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHUKKOKUKIKAN_ED)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.IDOSHURUI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.SHOKANKUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.TOGOATENAFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOUMAREBI_DATE)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOCKINIDOBI_DATE)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.FUSHOSHOJOIDOBI_DATE)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKIMACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKITODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKISHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKIMACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKIKANAKATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD4)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD5)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD6)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD7)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD8)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD9)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKICHIKUCD10)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUKIBANCHIEDABANSUCHI)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RESERVE5)
    End Sub
#End Region

#Region "��������t���W���f�[�^���ڕҏW"
    '************************************************************************************************
    '* ���\�b�h��     ��������t���W���f�[�^���ڕҏW
    '* 
    '* �\��           Private SetFZYHyojunEntity()
    '* 
    '* �@�\           ��������t���W���f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYHyojunEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHFRNMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHKANAFRNMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHKANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.TSUSHOKANAKAKUNINFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SHIMEIYUSENKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.SEARCHKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.ZAIRYUCARDNOKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.JUKYOCHIHOSEICD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.HODAI30JO46MATAHA47KB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.STAINUSSHIMEIYUSENKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.TOKUSHOMEI_YUKOKIGEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RESERVE5)
    End Sub
#End Region

#Region "�s���Z���f�[�^���ڕҏW"
    '************************************************************************************************
    '* ���\�b�h��     �s���Z���f�[�^���ڕҏW
    '* 
    '* �\��           Private SetFugenjuEntity()
    '* 
    '* �@�\           �s���Z���f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFugenjuEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_YUBINNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHICHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKUBUN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI_SEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI_MEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.KYOJUFUMEI_YMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUTOROKUYMD)
        '*����ԍ� 000038 2023/08/14 �C���J�n
        'strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.GYOSEIKUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUGYOSEIKUCD)
        '*����ԍ� 000038 2023/08/14 �C���I��
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUJOHO_BIKO)
    End Sub
#End Region

#Region "���ʔԍ��W���f�[�^���ڕҏW"
    '************************************************************************************************
    '* ���\�b�h��     ���ʔԍ��W���f�[�^���ڕҏW
    '* 
    '* �\��           Private SetMyNumberHyojunEntity()
    '* 
    '* �@�\           ���ʔԍ��W���f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetMyNumberHyojunEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.BANGOHOKOSHINKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_MH", ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.RESERVE5)
    End Sub
#End Region

#Region "�d�q�ؖ������f�[�^���ڕҏW"
    '************************************************************************************************
    '* ���\�b�h��     �d�q�ؖ������f�[�^���ڕҏW
    '* 
    '* �\��           Private SetDenshiShomeishoMSTEntity()
    '* 
    '* �@�\           �d�q�ؖ������f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetDenshiShomeishoMSTEntity(ByRef strAtenaSQLsb As StringBuilder)
        If (Me.m_blnSelectAll <> ABEnumDefine.AtenaGetKB.NenkinAll) AndAlso
           (Me.m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1) Then
            strAtenaSQLsb.AppendFormat(", DS3.{0}", ABDENSHISHOMEISHOMSTEntity.SERIALNO)
        End If
    End Sub
#End Region

#Region "��������W���e�[�u��JOIN��쐬"
    '************************************************************************************************
    '* ���\�b�h��     ��������W���e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetHyojunJoin()
    '* 
    '* �@�\           ��������W���e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetHyojunJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaRirekiHyojunEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD,
                                    ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUMINCD)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RIREKINO,
                                    ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.RIREKINO)
    End Sub
#End Region

#Region "��������t���W���e�[�u��JOIN��쐬"
    '************************************************************************************************
    '* ���\�b�h��     ��������t���W���e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetFZYHyojunJoin()
    '* 
    '* �@�\           ��������t���W���e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYHyojunJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaRirekiFZYHyojunEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD,
                                    ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.JUMINCD)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.RIREKINO,
                                    ABAtenaRirekiFZYHyojunEntity.TABLE_NAME, ABAtenaRirekiFZYHyojunEntity.RIREKINO)
    End Sub
#End Region

#Region "�s���Z���e�[�u��JOIN��쐬"
    '************************************************************************************************
    '* ���\�b�h��     �s���Z���e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetFugenjuJoin()
    '* 
    '* �@�\           �s���Z���e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFugenjuJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABFugenjuJohoEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD,
                                    ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.JUMINCD)
    End Sub
#End Region

#Region "���ʔԍ��W���e�[�u��JOIN��쐬"
    '************************************************************************************************
    '* ���\�b�h��     ���ʔԍ��W���e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetMyNumberHyojunJoin()
    '* 
    '* �@�\           ���ʔԍ��W���e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetMyNumberHyojunJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABMyNumberHyojunEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD,
                                    ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.JUMINCD)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.MYNUMBER,
                                    ABMyNumberHyojunEntity.TABLE_NAME, ABMyNumberHyojunEntity.MYNUMBER)
    End Sub
#End Region

#Region "�d�q�ؖ������e�[�u��JOIN��쐬"
    '************************************************************************************************
    '* ���\�b�h��     �d�q�ؖ������e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetDenshiShomeishoMST()
    '* 
    '* �@�\           �d�q�ؖ������e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetDenshiShomeishoMSTJoin(ByRef strAtenaSQLsb As StringBuilder)
        If (Me.m_blnSelectAll <> ABEnumDefine.AtenaGetKB.NenkinAll) AndAlso
           (Me.m_blnMethodKB = ABEnumDefine.MethodKB.KB_AtenaGet1) Then
            ' �d�q�ؖ������e�[�u���͏����������ŐV�̃��R�[�h�݂̂���������B
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ")
            strAtenaSQLsb.AppendFormat("(SELECT DS1.* FROM {0} AS DS1 INNER JOIN (SELECT {1}, {2}, MAX({3}) AS {3} FROM {0} GROUP BY {1}, {2}) AS DS2 ON DS1.{1} = DS2.{1} AND DS1.{2} = DS2.{2} AND DS1.{3} = DS2.{3}) AS DS3 ",
                                       ABDENSHISHOMEISHOMSTEntity.TABLE_NAME,
                                       ABDENSHISHOMEISHOMSTEntity.JUMINCD,
                                       ABDENSHISHOMEISHOMSTEntity.STAICD,
                                       ABDENSHISHOMEISHOMSTEntity.SHORINICHIJI)

            strAtenaSQLsb.AppendFormat(" ON {0}.{1} = DS3.{2} ",
                                        ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD,
                                        ABDENSHISHOMEISHOMSTEntity.JUMINCD)
            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = DS3.{2} ",
                                        ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.STAICD,
                                        ABDENSHISHOMEISHOMSTEntity.STAICD)
        End If
    End Sub
#End Region
    '*����ԍ� 000037 2023/03/10 �ǉ��I��

#Region "���������擾"
    '************************************************************************************************
    '* ���\�b�h��     ���������f�[�^���o
    '* 
    '* �\��           Public Function GetAtenaRirekiByRirekiNO(ByVal strJuminCD As String, ByVal strRirekiNO As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@���������}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD   : �Z���R�[�h
    '* �@�@           strRirekiNO  : ����ԍ�
    '* 
    '* �߂�l         DataSet : �擾�������������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Function GetAtenaRirekiByRirekiNO(ByVal strJuminCD As String, ByVal strRirekiNO As String) As DataSet
        Dim cfUFParameterClass As UFParameterClass
        Dim csAtenaRirekiEntity As DataSet                  '���������f�[�^�Z�b�g
        Dim strSQL As New StringBuilder
        Dim csDataSchma As DataSet

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' SQL���̍쐬
            ' SELECT��̐���
            strSQL.Append(Me.CreateSelect)
            ' FROM��̐���
            strSQL.AppendFormat(" FROM {0} ", ABAtenaRirekiEntity.TABLE_NAME)

            csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRirekiEntity.TABLE_NAME, False)

            ' WHERE��̍쐬
            ' SELECT�p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' �Z���R�[�h
            strSQL.AppendFormat("WHERE {0} = {1}", ABAtenaRirekiEntity.JUMINCD, ABAtenaRirekiEntity.KEY_JUMINCD)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ����ԍ�
            strSQL.AppendFormat(" AND {0} = {1}", ABAtenaRirekiEntity.RIREKINO, ABAtenaRirekiEntity.KEY_RIREKINO)
            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.KEY_RIREKINO
            cfUFParameterClass.Value = strRirekiNO
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '�Z���Z�o�O�敪
            strSQL.AppendFormat(" AND {0} = '1'", ABAtenaRirekiEntity.JUMINJUTOGAIKB)
            '�폜�t���O
            strSQL.AppendFormat(" AND {0} <> '1'", ABAtenaRirekiEntity.SAKUJOFG)

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                                "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                                "�y���s���\�b�h��:GetDataSet�z" +
            '                                "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "�z")

            csAtenaRirekiEntity = csDataSchma.Clone
            csAtenaRirekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRirekiEntity, ABAtenaRirekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
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
            csSELECT.AppendFormat("SELECT {0}", ABAtenaRirekiEntity.JUMINCD)               ' �Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHICHOSONCD)                ' �s�����R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KYUSHICHOSONCD)             ' ���s�����R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RIREKINO)                   ' ����ԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RRKST_YMD)                  ' �����J�n�N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RRKED_YMD)                  ' �����I���N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUMINJUTOGAIKB)             ' �Z���Z�o�O�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUMINYUSENIKB)              ' �Z���D��敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTOGAIYUSENKB)             ' �Z�o�O�D��敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ATENADATAKB)                ' �����f�[�^�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.STAICD)                     ' ���уR�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUMINHYOCD)                 ' �Z���[�R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEIRINO)                    ' �����ԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ATENADATASHU)               ' �����f�[�^���
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HANYOKB1)                   ' �ėp�敪1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KJNHJNKB)                   ' �l�@�l�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HANYOKB2)                   ' �ėp�敪2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANNAIKANGAIKB)             ' �Ǔ��ǊO�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANAMEISHO1)                ' �J�i����1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIMEISHO1)               ' ��������1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANAMEISHO2)                ' �J�i����2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIMEISHO2)               ' ��������2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIHJNKEITAI)             ' �����@�l�`��
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIHJNDAIHYOSHSHIMEI)     ' �����@�l��\�Ҏ���
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEARCHKANJIMEISHO)          ' �����p��������
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KYUSEI)                     ' ����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEARCHKANASEIMEI)           ' �����p�J�i����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEARCHKANASEI)              ' �����p�J�i��
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEARCHKANAMEI)              ' �����p�J�i��
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIRRKNO)                  ' �Z���ԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.UMAREYMD)                   ' ���N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.UMAREWMD)                   ' ���a��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEIBETSUCD)                 ' ���ʃR�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEIBETSU)                   ' ����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SEKINO)                     ' �Дԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUMINHYOHYOJIJUN)           ' �Z���[�\����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZOKUGARACD)                 ' �����R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZOKUGARA)                   ' ����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2JUMINHYOHYOJIJUN)       ' ��2�Z���[�\����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2ZOKUGARACD)             ' ��2�����R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2ZOKUGARA)               ' ��2����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.STAINUSJUMINCD)             ' ���ю�Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.STAINUSMEI)                 ' ���ю喼
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANASTAINUSMEI)             ' �J�i���ю喼
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2STAINUSJUMINCD)         ' ��2���ю�Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.DAI2STAINUSMEI)             ' ��2���ю喼
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANADAI2STAINUSMEI)         ' �J�i��2���ю喼
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.YUBINNO)                    ' �X�֔ԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUSHOCD)                    ' �Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUSHO)                      ' �Z��
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BANCHICD1)                  ' �Ԓn�R�[�h1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BANCHICD2)                  ' �Ԓn�R�[�h2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BANCHICD3)                  ' �Ԓn�R�[�h3
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BANCHI)                     ' �Ԓn
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KATAGAKIFG)                 ' �����t���O
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KATAGAKICD)                 ' �����R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KATAGAKI)                   ' ����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RENRAKUSAKI1)               ' �A����1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RENRAKUSAKI2)               ' �A����2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HON_ZJUSHOCD)               ' �{�БS���Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HON_JUSHO)                  ' �{�ЏZ��
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HONSEKIBANCHI)              ' �{�ДԒn
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HITTOSH)                    ' �M����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINIDOYMD)                 ' ���߈ٓ��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINJIYUCD)                 ' ���ߎ��R�R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINJIYU)                   ' ���ߎ��R
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINTDKDYMD)                ' ���ߓ͏o�N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CKINTDKDTUCIKB)             ' ���ߓ͏o�ʒm�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUIDOYMD)               ' �o�^�ٓ��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUIDOWMD)               ' �o�^�ٓ��a��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUJIYUCD)               ' �o�^���R�R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUJIYU)                 ' �o�^���R
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUTDKDYMD)              ' �o�^�͏o�N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUTDKDWMD)              ' �o�^�͏o�a��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOROKUTDKDTUCIKB)           ' �o�^�͏o�ʒm�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEIIDOYMD)                ' �Z��ٓ��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEIIDOWMD)                ' �Z��ٓ��a��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEIJIYUCD)                ' �Z�莖�R�R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEIJIYU)                  ' �Z�莖�R
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEITDKDYMD)               ' �Z��͏o�N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEITDKDWMD)               ' �Z��͏o�a��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUTEITDKDTUCIKB)            ' �Z��͏o�ʒm�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOIDOYMD)                ' �����ٓ��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOJIYUCD)                ' �������R�R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOJIYU)                  ' �������R
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOTDKDYMD)               ' �����͏o�N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOJOTDKDTUCIKB)            ' �����͏o�ʒm�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIIDOYMD)       ' �]�o�\��ٓ��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIIDOYMD)        ' �]�o�m��ٓ��N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTITSUCHIYMD)     ' �]�o�m��ʒm�N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUNYURIYUCD)         ' �]�o�����R�R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUNYURIYU)           ' �]�o�����R
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_YUBINNO)           ' �]���O�Z���X�֔ԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_ZJUSHOCD)          ' �]���O�Z���S���Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_JUSHO)             ' �]���O�Z���Z��
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_BANCHI)            ' �]���O�Z���Ԓn
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_KATAGAKI)          ' �O�Z������
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENUMAEJ_STAINUSMEI)        ' �]���O�Z�����ю喼
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIYUBINNO)      ' �]�o�\��X�֔ԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIZJUSHOCD)     ' �]�o�\��S���Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIJUSHO)        ' �]�o�\��Z��
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIBANCHI)       ' �]�o�\��Ԓn
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEIKATAGAKI)     ' �]�o�\�����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUYOTEISTAINUSMEI)   ' �]�o�\�萢�ю喼
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIYUBINNO)       ' �]�o�m��X�֔ԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIZJUSHOCD)      ' �]�o�m��S���Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIJUSHO)         ' �]�o�m��Z��
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIBANCHI)        ' �]�o�m��Ԓn
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIKATAGAKI)      ' �]�o�m�����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTISTAINUSMEI)    ' �]�o�m�萢�ю喼
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TENSHUTSUKKTIMITDKFG)       ' �]�o�m�薢�̓t���O
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BIKOYMD)                    ' ���l�N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BIKO)                       ' ���l
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BIKOTENSHUTSUKKTIJUSHOFG)   ' ���l�]�o�m��Z���t���O
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HANNO)                      ' �Ŕԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KAISEIATOFG)                ' ������t���O
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KAISEIMAEFG)                ' �����O�t���O
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KAISEIYMD)                  ' �����N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.GYOSEIKUCD)                 ' �s����R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.GYOSEIKUMEI)                ' �s���於
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUCD1)                   ' �n��R�[�h1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUMEI1)                  ' �n�於1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUCD2)                   ' �n��R�[�h2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUMEI2)                  ' �n�於2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUCD3)                   ' �n��R�[�h3
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHIKUMEI3)                  ' �n�於3
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TOHYOKUCD)                  ' ���[��R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHOGAKKOKUCD)               ' ���w�Z��R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.CHUGAKKOKUCD)               ' ���w�Z��R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.HOGOSHAJUMINCD)             ' �ی�ҏZ���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANJIHOGOSHAMEI)            ' �����ی�Җ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KANAHOGOSHAMEI)             ' �J�i�ی�Җ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KIKAYMD)                    ' �A���N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KARIIDOKB)                  ' ���ٓ��敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHORITEISHIKB)              ' ������~�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIYUBINNO)                ' �Z��X�֔ԍ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SHORIYOKUSHIKB)             ' �����}�~�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIJUSHOCD)                ' �Z��Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIJUSHO)                  ' �Z��Z��
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIBANCHICD1)              ' �Z��Ԓn�R�[�h1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIBANCHICD2)              ' �Z��Ԓn�R�[�h2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIBANCHICD3)              ' �Z��Ԓn�R�[�h3
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIBANCHI)                 ' �Z��Ԓn
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIKATAGAKIFG)             ' �Z������t���O
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIKATAGAKICD)             ' �Z������R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIKATAGAKI)               ' �Z�����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIGYOSEIKUCD)             ' �Z��s����R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKIGYOSEIKUMEI)            ' �Z��s���於
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUCD1)               ' �Z��n��R�[�h1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUMEI1)              ' �Z��n�於1
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUCD2)               ' �Z��n��R�[�h2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUMEI2)              ' �Z��n�於2
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUCD3)               ' �Z��n��R�[�h3
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.JUKICHIKUMEI3)              ' �Z��n�於3
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KAOKUSHIKIKB)               ' �Ɖ��~�敪
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.BIKOZEIMOKU)                ' ���l�Ŗ�
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOKUSEKICD)                 ' ���ЃR�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOKUSEKI)                   ' ����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYUSKAKCD)               ' �ݗ����i�R�[�h
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYUSKAK)                 ' �ݗ����i
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYUKIKAN)                ' �ݗ�����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYU_ST_YMD)              ' �ݗ��J�n�N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.ZAIRYU_ED_YMD)              ' �ݗ��I���N����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.RESERCE)                    ' ���U�[�u
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.TANMATSUID)                 ' �[��ID
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SAKUJOFG)                   ' �폜�t���O
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOSHINCOUNTER)              ' �X�V�J�E���^
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SAKUSEINICHIJI)             ' �쐬����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.SAKUSEIUSER)                ' �쐬���[�U
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOSHINNICHIJI)              ' �X�V����
            csSELECT.AppendFormat(", {0}", ABAtenaRirekiEntity.KOSHINUSER)                 ' �X�V���[�U
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
#End Region
End Class
