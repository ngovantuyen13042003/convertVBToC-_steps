'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�����}�X�^�c�`(ABAtenaBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2002/12/20�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/02/24 000001     ���N�����̂����܂����������̃o�O
'* 2003/02/25 000002     �f�[�^�敪�����鎞���A�f�[�^��ʂ������Ă���ꍇ�́A�f�[�^��ʂ������Ƃ���
'*                       �Z��D��Ő��уR�[�h���A�w�肳��Ă���ꍇ�ɏZ���[�\�������\�[�g�L�[�ɂ���
'* 2003/03/10 000003     �Z���b�c���̐������`�F�b�N�Ɍ��
'* 2003/03/27 000004     �G���[�����N���X�̎Q�Ɛ��"AB"�Œ�ɂ���
'* 2003/03/31 000005     �������`�F�b�N��Trim�����l�Ń`�F�b�N����
'* 2003/04/16 000006     ���a��N�����̓��t�`�F�b�N�𐔒l�`�F�b�N�ɕύX
'*                       �����p�J�i�̔��p�J�i�`�F�b�N���`�m�j�`�F�b�N�ɕύX
'* 2003/05/20 000007     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/06/12 000008     TOP����O��
'* 2003/08/28 000009     RDB�A�N�Z�X���O�̏C��
'* 2003/09/11 000010     �[���h�c�������`�F�b�N��ANK�ɂ���
'* 2003/10/09 000011     �쐬���[�U�[�E�X�V���[�U�[�`�F�b�N�̕ύX
'* 2003/10/30 000012     �d�l�ύX�F�J�^�J�i�`�F�b�N��ANK�`�F�b�N�ɕύX
'* 2003/11/18 000013     �d�l�ύX�F�f�[�^�敪�Ōl�̂ݎ����Ă���B�i�f�[�^�敪��"1%"�Ǝw�肳�ꂽ�ꍇ�j
'*                       �d�l�ǉ��F�����ʃf�[�^�擾���\�b�h��ǉ�
'* 2004/08/27 000014     ���x���P�F�i�{��j
'* 2004/10/19 000015     �`�S���Z���R�[�h�̃`�F�b�N��CheckNumber --> CheckANK(�}���S���R)
'* 2004/11/12 000016     �f�[�^�`�F�b�N���s�Ȃ�Ȃ�
'* 2005/01/25 000017     ���x���P�Q�F�i�{��j
'* 2005/05/23 000018     SQL����Insert,Update,�_��Delete,����Delete�̊e���\�b�h���Ă΂ꂽ���Ɋe���쐬����(�}���S���R)
'* 2005/07/11 000019     CreateWhereҿ��ނŏZ��CD��Where���쐬���ɏZ��CD���S���Z��CD���̔��������(�}���S���R)
'* 2005/12/26 000020     �d�l�ύX�F�s����b�c��ANK�`�F�b�N�ɕύX(�}���S���R)
'* 2006/07/31 000021     �N�������Q�b�g�U���ڒǉ�(�g�V)
'* 2007/04/28 000022     ���ň����擾���\�b�h�̒ǉ��ɂ��擾���ڂ̒ǉ� (�g�V)
'* 2007/09/03 000023     �O���l�{���D�挟���p�Ɋ������̂Q��ǉ��i����j
'* 2007/10/10 000024     �O���l�{���D�挟���@�\�F�J�i���̐擪��"�"�̂Ƃ���"�"��OR�����Ō�������i����j
'* 2008/01/15 000025     �ʎ����f�[�^�擾�@�\�Ɍ������擾������ǉ��i��Áj���l�[�~���O�ύX�i�g�V�j
'* 2010/04/16 000026     VS2008�Ή��i��Áj
'* 2010/05/12 000027     �{�ЕM���ҋy�я�����~�敪�Ή��i��Áj
'* 2011/05/18 000028     �O���l�ݗ����擾�敪�Ή��i��Áj
'* 2011/10/24 000029     �yAB17010�z���Z��@�����Ή��������t���}�X�^�ǉ�   (����)
'* 2014/04/28 000030     �yAB21040�z�����ʔԍ��Ή������ʔԍ��}�X�^�ǉ��i�΍��j
'* 2018/03/08 000031     �yAB26001�z���������@�\�ǉ��i�΍��j
'* 2020/01/10 000032     �yAB32001�z�A���t�@�x�b�g�����i�΍��j
'* 2023/03/10 000033     �yAB-0970-1�z����GET�擾���ڕW�����Ή��i�����j
'* 2023/08/14 000034     �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ�(����)
'* 2023/10/19 000035     �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ�_�ǉ��C��(����)
'* 2023/12/04 000036     �yAB-1600-1�z�����@�\�Ή�(����)
'* 2023/12/11 000037     �yAB-9000-1�z�Z��X�V�A�g�W�����Ή�(����)
'* 2024/03/07 000038     �yAB-0900-1�z�A�h���X�E�x�[�X�E���W�X�g���Ή�(����)
'* 2024/06/06 000039     �yAB-9901-1�z�s��Ή�
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
'*����ԍ� 000034 2023/08/14 �ǉ��J�n
Imports Densan.Common
'*����ԍ� 000034 2023/08/14 �ǉ��I��

'************************************************************************************************
'*
'* �����}�X�^�擾���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABAtenaBClass
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

    '* ����ԍ� 000014 2004/08/27 �ǉ��J�n�i�{��j
    '* ����ԍ� 000017 2005/01/25 �ύX�J�n�i�{��j
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
    '* ����ԍ� 000017 2005/01/25 �ύX�I��
    '* ����ԍ� 000014 2004/08/27 �ǉ��I��

    '* ����ԍ� 000017 2005/01/25 �ǉ��J�n�i�{��j
    Private m_strAtenaSQLsbAll As StringBuilder = New StringBuilder()
    Private m_strAtenaSQLsbKaniAll As StringBuilder = New StringBuilder()
    Private m_strAtenaSQLsbKaniOnly As StringBuilder = New StringBuilder()
    Private m_strAtenaSQLsbNenkinAll As StringBuilder = New StringBuilder()
    Private m_strKobetuSQLsbAll As StringBuilder = New StringBuilder()
    Private m_strKobetuSQLsbKaniAll As StringBuilder = New StringBuilder()
    Private m_strKobetuSQLsbKaniOnly As StringBuilder = New StringBuilder()
    Private m_strKobetuSQLsbNenkinAll As StringBuilder = New StringBuilder()
    Public m_blnSelectAll As ABEnumDefine.AtenaGetKB = ABEnumDefine.AtenaGetKB.SelectAll '�S���ڑI���im_blnAtenaGet��True�̎�����Get�ŕK�v�ȍ��ڑS�Ă���ȊO��SELECT *�j
    Public m_blnSelectCount As Boolean = False            '�J�E���g���擾���邩�ǂ���
    Public m_blnBatch As Boolean = False               '�o�b�`�t���O
    '* ����ԍ� 000017 2005/01/25 �ǉ��I��

    '*����ԍ� 000022 2007/04/28 �ǉ��J�n
    Public m_blnMethodKB As ABEnumDefine.MethodKB  '���\�b�h�敪�i�ʏ�ł��A���ŁA�A�A�j
    '*����ԍ� 000022 2007/04/28 �ǉ��I��
    '*����ԍ� 000025 2008/01/15 �ǉ��J�n
    Public m_strKobetsuShutokuKB As String                  ' �ʎ����擾�敪
    '*����ԍ� 000025 2008/01/15 �ǉ��I��

    '*����ԍ� 000027 2010/05/12 �ǉ��J�n
    Private m_strHonsekiKB As String = String.Empty                 ' �����Ǘ����:�{�Ў擾
    Private m_strShoriteishiKB As String = String.Empty             ' �����Ǘ����:������~�敪�擾
    Private m_strHonsekiHittoshKB_Param As String = String.Empty    ' �{�ЕM���Ҏ擾�敪�p�����[�^
    Private m_strShoriteishiKB_Param As String = String.Empty       ' ������~�敪�擾�敪�p�����[�^
    '*����ԍ� 000027 2010/05/12 �ǉ��I��

    '*����ԍ� 000028 2011/05/18 �ǉ��J�n
    Private m_strFrnZairyuJohoKB_Param As String = String.Empty     ' �O���l�ݗ����擾�敪�p�����[�^
    '*����ԍ� 000028 2011/05/18 �ǉ��I��

    '*����ԍ� 000029 2011/10/24 �ǉ��J�n
    Private m_csSekoYMDHanteiB As ABSekoYMDHanteiBClass             '�{�s������B�׽
    Private m_csAtenaFZYB As ABAtenaFZYBClass                       '�����t���}�X�^B�׽
    Private m_blnJukihoKaiseiFG As Boolean = False
    Private m_strJukihoKaiseiKB As String                           '�Z��@�����敪
    '*����ԍ� 000029 2011/10/24 �ǉ��I��

    '*����ԍ� 000030 2014/04/28 �ǉ��J�n
    Private m_strMyNumberKB_Param As String                         ' ���ʔԍ��擾�敪
    Private m_strMyNumberChokkinSearchKB_Param As String            ' ���ʔԍ����ߌ����敪
    '*����ԍ� 000030 2014/04/28 �ǉ��I��

    '*����ԍ� 000032 2020/01/10 �ǉ��J�n
    Private m_cKensakuShimeiB As ABKensakuShimeiBClass              ' ���������ҏW�r�W�l�X�N���X
    '*����ԍ� 000032 2020/01/10 �ǉ��I��

    Public m_intHyojunKB As ABEnumDefine.HyojunKB                   '����GET�W�����敪

    '*����ԍ� 000034 2023/08/14 �ǉ��J�n
    Private m_csAtenaHyojunB As ABAtena_HyojunBClass                '����_�W���}�X�^B�׽
    Private m_csAtenaFZYHyojunB As ABAtenaFZY_HyojunBClass          '�����t��_�W���}�X�^B�׽
    '*����ԍ� 000034 2023/08/14 �ǉ��I��

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaBClass"                       ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h

    Private Const JUKIHOKAISEIKB_ON As String = "1"

#End Region

    '*����ԍ� 000027 2010/05/12 �ǉ��J�n
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

    '*����ԍ� 000028 2011/05/18 �ǉ��J�n
    Public WriteOnly Property p_strFrnZairyuJohoKB() As String      ' �O���l�ݗ����擾�敪
        Set(ByVal Value As String)
            m_strFrnZairyuJohoKB_Param = Value
        End Set
    End Property
    '*����ԍ� 000028 2011/05/18 �ǉ��I��

    '*����ԍ� 000029 2011/10/24 �ǉ��J�n
    Public WriteOnly Property p_strJukihoKaiseiKB() As String      ' �Z��@�����敪
        Set(ByVal Value As String)
            m_strJukihoKaiseiKB = Value
        End Set
    End Property
    '*����ԍ� 000029 2011/10/24 �ǉ��I��

    '*����ԍ� 000030 2014/04/28 �ǉ��J�n
    Public Property p_strMyNumberKB() As String                     ' ���ʔԍ��擾�敪
        Get
            Return m_strMyNumberKB_Param
        End Get
        Set(ByVal value As String)
            m_strMyNumberKB_Param = value
        End Set
    End Property
    '*����ԍ� 000030 2014/04/28 �ǉ��I��

#End Region
    '*����ԍ� 000027 2010/05/12 �ǉ��I��

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
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing

        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
        '�Z��@�����敪������
        m_strJukihoKaiseiKB = String.Empty
        '�Z��@�����׸ގ擾
        Call GetJukihoKaiseiFG()
        '*����ԍ� 000029 2011/10/24 �ǉ��I��

        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
        ' ���ʔԍ��擾�敪������
        m_strMyNumberKB_Param = String.Empty
        ' ���ʔԍ��@�����擾�@���ߌ����敪�擾
        Me.GetMyNumberChokkinSearchKB()
        '*����ԍ� 000030 2014/04/28 �ǉ��I��

        '*����ԍ� 000032 2020/01/10 �ǉ��J�n
        ' ���������ҏW�r�W�l�X�N���X�̃C���X�^���X��
        m_cKensakuShimeiB = New ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass)
        '*����ԍ� 000032 2020/01/10 �ǉ��I��

    End Sub
    '* ����ԍ� 000017 2005/01/25 �ǉ��J�n�i�{��j
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
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass,
                   ByVal blnSelectAll As ABEnumDefine.AtenaGetKB,
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
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing

        m_blnSelectAll = blnSelectAll
        m_blnSelectCount = blnSelectCount

        '*����ԍ� 000027 2010/05/12 �ǉ��J�n
        '�Ǘ����擾����
        Call GetKanriJoho()
        '*����ԍ� 000027 2010/05/12 �ǉ��I��

        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
        '�Z��@�����敪������
        m_strJukihoKaiseiKB = String.Empty
        '�Z��@�����׸ގ擾
        Call GetJukihoKaiseiFG()
        '*����ԍ� 000029 2011/10/24 �ǉ��I��

        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
        ' ���ʔԍ��擾�敪������
        m_strMyNumberKB_Param = String.Empty
        ' ���ʔԍ��@�����擾�@���ߌ����敪�擾
        Me.GetMyNumberChokkinSearchKB()
        '*����ԍ� 000030 2014/04/28 �ǉ��I��

        '*����ԍ� 000032 2020/01/10 �ǉ��J�n
        ' ���������ҏW�r�W�l�X�N���X�̃C���X�^���X��
        m_cKensakuShimeiB = New ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass)
        '*����ԍ� 000032 2020/01/10 �ǉ��I��

    End Sub
    '* ����ԍ� 000017 2005/01/25 �ǉ��I��
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaBHoshu(ByVal intGetCount As Integer, _
    '*                                               ByVal cSearchKey As ABAtenaSearchKey) As DataSet
    '* 
    '* �@�\�@�@    �@�@�����}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           intGetCount   : �擾����
    '*                cSearchKey    : �����}�X�^�����L�[
    '* 
    '* �߂�l         DataSet : �擾���������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaBHoshu(ByVal intGetCount As Integer,
                                             ByVal cSearchKey As ABAtenaSearchKey) As DataSet

        Return Me.GetAtenaBHoshu(intGetCount, cSearchKey, False)

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaBHoshu(ByVal intGetCount As Integer, 
    '*                                               ByVal cSearchKey As ABAtenaSearchKey, 
    '*                                               ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@�����}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           intGetCount   : �擾����
    '*                cSearchKey    : �����}�X�^�����L�[
    '*                blnSakujoFG   : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾���������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaBHoshu(ByVal intGetCount As Integer, ByVal cSearchKey As ABAtenaSearchKey,
                                             ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetAtenaBHoshu"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaEntity As DataSet
        '* corresponds to VS2008 Start 2010/04/16 000026
        'Dim csDataTable As DataTable
        '* corresponds to VS2008 End 2010/04/16 000026
        '* ����ԍ� 000017 2005/01/25 �X�V�J�n�i�{��j
        'Dim strSQL As String
        Dim strSQL As New StringBuilder()
        Dim strSQLExec As String
        '* ����ԍ� 000017 2005/01/25 �X�V�I��

        Dim strWHERE As StringBuilder
        '* ����ԍ� 000017 2005/01/25 �X�V�J�n�i�{��j
        'Dim strORDER As String
        Dim strORDER As New StringBuilder()
        '* ����ԍ� 000017 2005/01/25 �X�V�I��

        Dim intMaxRows As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            If intGetCount < 0 Or intGetCount > 999 Then    '�擾�����̌��
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_PARA_GETCOUNT)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' ���������L�[�̃`�F�b�N
            ' �Ȃ�

            ' SQL���̍쐬
            '* ����ԍ� 000008 2003/06/12 �C���J�n
            'If intGetCount = 0 Then
            '    strSQL = "SELECT TOP 100 * FROM " + ABAtenaEntity.TABLE_NAME
            'Else
            '    strSQL = "SELECT TOP " + intGetCount.ToString + " * FROM " + ABAtenaEntity.TABLE_NAME
            'End If

            ' p_intMaxRows��ޔ�����
            intMaxRows = m_cfRdbClass.p_intMaxRows
            If intGetCount = 0 Then
                m_cfRdbClass.p_intMaxRows = 100
            Else
                m_cfRdbClass.p_intMaxRows = intGetCount
            End If
            '* ����ԍ� 000017 2005/01/25 �X�V�J�n�i�{��j
            'strSQL = "SELECT * FROM " + ABAtenaEntity.TABLE_NAME
            Select Case (Me.m_blnSelectAll)
                Case ABEnumDefine.AtenaGetKB.KaniAll
                    If (m_strAtenaSQLsbKaniAll.RLength = 0) Then
                        m_strAtenaSQLsbKaniAll.Append("SELECT ")
                        Call SetAtenaEntity(m_strAtenaSQLsbKaniAll)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbKaniAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strAtenaSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_strAtenaSQLsbKaniAll.Append(ABAtenaEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbKaniAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strAtenaSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_csDataSchmaKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaKaniAll
                Case ABEnumDefine.AtenaGetKB.KaniOnly
                    If (m_strAtenaSQLsbKaniOnly.RLength = 0) Then
                        m_strAtenaSQLsbKaniOnly.Append("SELECT ")
                        Call SetAtenaEntity(m_strAtenaSQLsbKaniOnly)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbKaniOnly)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strAtenaSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_strAtenaSQLsbKaniOnly.Append(ABAtenaEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbKaniOnly)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strAtenaSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_csDataSchmaKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaKaniOnly
                Case ABEnumDefine.AtenaGetKB.NenkinAll
                    If (m_strAtenaSQLsbNenkinAll.RLength = 0) Then
                        m_strAtenaSQLsbNenkinAll.Append("SELECT ")
                        Call SetAtenaEntity(m_strAtenaSQLsbNenkinAll)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbNenkinAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strAtenaSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_strAtenaSQLsbNenkinAll.Append(ABAtenaEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbNenkinAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strAtenaSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_csDataSchmaNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaNenkinAll
                Case Else
                    If (m_strAtenaSQLsbAll.RLength = 0) Then
                        m_strAtenaSQLsbAll.Append("SELECT ")
                        '���s
                        m_strAtenaSQLsbAll.Append(ABAtenaEntity.TABLE_NAME).Append(".*")

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strAtenaSQLsbAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strAtenaSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strAtenaSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_strAtenaSQLsbAll.Append(ABAtenaEntity.TABLE_NAME)

                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strAtenaSQLsbAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strAtenaSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strAtenaSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_csDataSchmaAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchma = m_csDataSchmaAll
            End Select
            'If (m_strAtenaSQLsb.Length = 0) Then
            '    m_strAtenaSQLsb.Append("SELECT ")
            '    Select Case (Me.m_blnSelectAll)
            '        Case ABEnumDefine.AtenaGetKB.SelectAll
            '            '���s
            '            m_strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".*")
            '        Case ABEnumDefine.AtenaGetKB.KaniAll
            '            Call SetAtenaEntity(m_strAtenaSQLsb)
            '        Case ABEnumDefine.AtenaGetKB.KaniOnly
            '            Call SetAtenaEntity(m_strAtenaSQLsb)
            '        Case Else
            '            '���s
            '            m_strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".*")
            '    End Select

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaCountEntity(m_strAtenaSQLsb)

            '    m_strAtenaSQLsb.Append(" FROM ")
            '    m_strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME)

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaJoin(m_strAtenaSQLsb)
            'End If
            'strSQL.Append(m_strAtenaSQLsb)
            '* ����ԍ� 000017 2005/01/25 �X�V�I��

            '* ����ԍ� 000008 2003/06/12 �C���I��

            '* ����ԍ� 000014 2004/08/27 �ǉ��J�n�i�{��j
            'If (m_csDataSchma Is Nothing) Then
            '    m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
            'End If
            '* ����ԍ� 000014 2004/08/27 �ǉ��I��
            ' WHERE��̍쐬
            '*����ԍ� 000031 2018/03/08 �C���J�n
            'strWHERE = New StringBuilder(Me.CreateWhere(cSearchKey))

            '' �폜�t���O
            'If blnSakujoFG = False Then
            '    If Not (strWHERE.Length = 0) Then
            '        strWHERE.Append(" AND ")
            '    End If
            '    strWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SAKUJOFG)
            '    strWHERE.Append(" <> '1'")
            'End If
            strWHERE = New StringBuilder(CreateWhereMain(cSearchKey, blnSakujoFG))
            '*����ԍ� 000031 2018/03/08 �C���I��

            'ORDER�������

            '�Z���D��敪���h1�h�ł����уR�[�h���w��ς̏ꍇ�F�Z���[�\����
            '* ����ԍ� 000017 2005/01/25 �X�V�J�n�i�{��j
            'If ((cSearchKey.p_strJuminYuseniKB = "1") And (cSearchKey.p_strStaiCD.Trim <> String.Empty)) Then
            '    strORDER = " ORDER BY " + ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.JUMINHYOHYOJIJUN + " ASC,"
            '    strORDER += ABAtenaEntity.JUMINCD + " ASC;"
            'ElseIf Not (cSearchKey.p_strUmareYMD.Trim = String.Empty) Then
            '    strORDER = " ORDER BY " + ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.UMAREYMD + " ASC,"
            '    strORDER += ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.JUMINCD + " ASC;"
            'Else
            '    strORDER = " ORDER BY " + ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.SEARCHKANASEIMEI + " ASC,"
            '    strORDER += ABAtenaEntity.TABLE_NAME + "." + ABAtenaEntity.JUMINCD + " ASC;"
            'End If
            'If strWHERE.Length = 0 Then
            '    strSQL += strORDER
            'Else
            '    strSQL += " WHERE " + strWHERE.ToString + strORDER
            'End If
            If ((cSearchKey.p_strJuminYuseniKB = "1") And (cSearchKey.p_strStaiCD.Trim <> String.Empty)) Then
                strORDER.Append(" ORDER BY ").Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINHYOHYOJIJUN).Append(" ASC,")
                strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;")
            ElseIf Not (cSearchKey.p_strUmareYMD.Trim = String.Empty) Then
                strORDER.Append(" ORDER BY ").Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD).Append(" ASC,")
                strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;")
            Else
                strORDER.Append(" ORDER BY ").Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI).Append(" ASC,")
                strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;")
            End If

            If strWHERE.RLength = 0 Then
                strSQL.Append(strORDER)
            Else
                strSQL.Append(" WHERE ").Append(strWHERE).Append(strORDER)
            End If
            strSQLExec = strSQL.ToString()
            '* ����ԍ� 000017 2005/01/25 �X�V�I��

            '*����ԍ� 000009 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:GetDataSet�z" + _
            '                        "�ySQL���e:" + strSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            ''* ����ԍ� 000017 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            'If (m_blnBatch = False) Then
            '    m_cfLogClass.RdbWrite(m_cfControlData,
            '                                "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                                "�y���s���\�b�h��:GetDataSet�z" +
            '                                "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQLExec, m_cfSelectUFParameterCollectionClass) + "�z")
            'End If
            '* ����ԍ� 000017 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
            '*����ԍ� 000009 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾

            '* ����ԍ� 000014 2004/08/27 �ύX�J�n�i�{��j
            'csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL, ABAtenaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
            csAtenaEntity = m_csDataSchma.Clone()
            'm_csDataSchma.Clear()
            'csAtenaEntity = m_csDataSchma
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQLExec, csAtenaEntity, ABAtenaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '* ����ԍ� 000014 2004/08/27 �ύX�I��

            ' MaxRows�l��߂�
            m_cfRdbClass.p_intMaxRows = intMaxRows

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
    '* ���\�b�h��     �����ʃf�[�^���o
    '* 
    '* �\��           Friend Function GetAtenaBKobetsu(ByVal intGetCount As Integer, 
    '*                                                ByVal cSearchKey As ABAtenaSearchKey, 
    '*                                                ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@�����}�X�^���Y���f�[�^�ƌʃf�[�^���擾����
    '* 
    '* ����           intGetCount   : �擾����
    '*                cSearchKey    : �����}�X�^�����L�[
    '*                blnSakujoFG   : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾���������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    '*����ԍ� 000025 2008/01/15 �C���J�n
    'Friend Function GetAtenaBKobetsu(ByVal intGetCount As Integer, _
    '                                 ByVal cSearchKey As ABAtenaSearchKey, _
    '                                 ByVal blnSakujoFG As Boolean) As DataSet
    Friend Function GetAtenaBKobetsu(ByVal intGetCount As Integer,
                                     ByVal cSearchKey As ABAtenaSearchKey,
                                     ByVal blnSakujoFG As Boolean,
                                     ByVal strKobetsuKB As String) As DataSet
        '*����ԍ� 000025 2008/01/15 �C���I��
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim csAtenaEntity As DataSet
        '* corresponds to VS2008 Start 2010/04/16 000026
        'Dim csDataTable As DataTable
        '* corresponds to VS2008 End 2010/04/16 000026
        Dim strSQL As New StringBuilder
        '* ����ԍ� 000017 2005/01/25 �ǉ��J�n�i�{��j
        Dim strSQLExec As String
        '* ����ԍ� 000017 2005/01/25 �ǉ��I��

        Dim strWHERE As StringBuilder
        Dim strORDER As New StringBuilder
        Dim intMaxRows As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' �p�����[�^�`�F�b�N
            If intGetCount < 0 Or intGetCount > 999 Then    '�擾�����̌��
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_PARA_GETCOUNT)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' ���������L�[�̃`�F�b�N
            ' �Ȃ�

            '*����ԍ� 000025 2008/01/15 �ǉ��J�n
            ' �ʎ����擾�敪�������o�ϐ��ɃZ�b�g
            m_strKobetsuShutokuKB = strKobetsuKB.Trim
            '*����ԍ� 000025 2008/01/15 �ǉ��I��

            ' p_intMaxRows��ޔ�����
            intMaxRows = m_cfRdbClass.p_intMaxRows
            If intGetCount = 0 Then
                m_cfRdbClass.p_intMaxRows = 100
            Else
                m_cfRdbClass.p_intMaxRows = intGetCount
            End If

            '* ����ԍ� 000017 2005/01/25 �X�V�J�n�i�{��jIF���ň͂�
            '' SELECT ABATENA.*
            'strSQL.Append("SELECT ").Append(ABAtenaEntity.TABLE_NAME).Append(".*")
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
            Select Case (Me.m_blnSelectAll)
                Case ABEnumDefine.AtenaGetKB.KaniAll
                    If (m_strKobetuSQLsbKaniAll.RLength = 0) Then
                        m_strKobetuSQLsbKaniAll.Append("SELECT ")
                        Call SetAtenaEntity(m_strKobetuSQLsbKaniAll)
                        '�ʎ����̍��ڃZ�b�g
                        Call SetKobetsuEntity(m_strKobetuSQLsbKaniAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strKobetuSQLsbKaniAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strKobetuSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strKobetuSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_strKobetuSQLsbKaniAll.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME)
                        '�ʎ�����JOIN����쐬
                        Call SetKobetsuJoin(m_strKobetuSQLsbKaniAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strKobetuSQLsbKaniAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strKobetuSQLsbKaniAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strKobetuSQLsbKaniAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_csDataSchmaKobetuKaniAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
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

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strKobetuSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strKobetuSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_strKobetuSQLsbKaniOnly.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME)
                        '�ʎ�����JOIN����쐬
                        Call SetKobetsuJoin(m_strKobetuSQLsbKaniOnly)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strKobetuSQLsbKaniOnly)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strKobetuSQLsbKaniOnly)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strKobetuSQLsbKaniOnly)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_csDataSchmaKobetuKaniOnly = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
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

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strKobetuSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strKobetuSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_strKobetuSQLsbNenkinAll.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME)
                        '�ʎ�����JOIN����쐬
                        Call SetKobetsuJoin(m_strKobetuSQLsbNenkinAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strKobetuSQLsbNenkinAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strKobetuSQLsbNenkinAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strKobetuSQLsbNenkinAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_csDataSchmaKobetuNenkinAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchmaKobetu = m_csDataSchmaKobetuNenkinAll
                Case Else
                    If (m_strKobetuSQLsbAll.RLength = 0) Then
                        m_strKobetuSQLsbAll.Append("SELECT ")
                        '���s
                        m_strKobetuSQLsbAll.Append(ABAtenaEntity.TABLE_NAME).Append(".*")
                        '�ʎ����̍��ڃZ�b�g
                        Call SetKobetsuEntity(m_strKobetuSQLsbAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaCountEntity(m_strKobetuSQLsbAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYEntity(m_strKobetuSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�̏ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON) Then
                            Call SetMyNumberEntity(m_strKobetuSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_strKobetuSQLsbAll.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME)
                        '�ʎ�����JOIN����쐬
                        Call SetKobetsuJoin(m_strKobetuSQLsbAll)
                        '�㗝�l���̃J�E���g���擾
                        Call SetAtenaJoin(m_strKobetuSQLsbAll)

                        '*����ԍ� 000029 2011/10/24 �ǉ��J�n
                        '�Z��@�����ȍ~�͈����t���}�X�^��t��
                        If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) OrElse (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                            Call SetFZYJoin(m_strKobetuSQLsbAll)
                        Else
                            '�����Ȃ�
                        End If
                        '*����ԍ� 000029 2011/10/24 �ǉ��I��

                        '*����ԍ� 000030 2014/04/28 �ǉ��J�n
                        ' ���ʔԍ��擾�敪��"1"�i�擾����j�A�܂��͋��ʔԍ����w�肳��Ă���ꍇ�A���ʔԍ��}�X�^��t��
                        If (m_strMyNumberKB_Param = ABConstClass.MYNUMBER.MYNUMBERKB.ON _
                            OrElse cSearchKey.p_strMyNumber.Trim.RLength > 0) Then
                            Call SetMyNumberJoin(m_strKobetuSQLsbAll)
                        Else
                            ' noop
                        End If
                        '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
                        m_csDataSchmaKobetuAll = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaEntity.TABLE_NAME, False)
                    End If
                    m_csDataSchmaKobetu = m_csDataSchmaKobetuAll
            End Select
            'If (m_strKobetuSQLsb.Length = 0) Then
            '    m_strKobetuSQLsb.Append("SELECT ")
            '    Select Case (Me.m_blnSelectAll)
            '        Case ABEnumDefine.AtenaGetKB.SelectAll
            '            '���s
            '            m_strKobetuSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".*")
            '        Case ABEnumDefine.AtenaGetKB.KaniAll
            '            Call SetAtenaEntity(m_strKobetuSQLsb)
            '        Case ABEnumDefine.AtenaGetKB.KaniOnly
            '            Call SetAtenaEntity(m_strKobetuSQLsb)
            '        Case Else
            '            '���s
            '            m_strKobetuSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".*")
            '    End Select

            '    '�ʎ����̍��ڃZ�b�g
            '    Call SetKobetsuEntity(m_strKobetuSQLsb)

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaCountEntity(m_strKobetuSQLsb)

            '    '  FROM ABATENA 
            '    m_strKobetuSQLsb.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME)

            '    '�ʎ�����JOIN����쐬
            '    Call SetKobetsuJoin(m_strKobetuSQLsb)

            '    '�㗝�l���̃J�E���g���擾
            '    Call SetAtenaJoin(m_strKobetuSQLsb)
            'End If
            'strSQL.Append(m_strKobetuSQLsb)
            ''* ����ԍ� 000017 2005/01/25 �X�V�I���i�{��jIF���ň͂�

            ''* ����ԍ� 000014 2004/08/27 �ǉ��J�n�i�{��j
            'If (m_csDataSchmaKobetu Is Nothing) Then
            '    m_csDataSchmaKobetu = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABAtenaEntity.TABLE_NAME, False)
            'End If
            ''* ����ԍ� 000014 2004/08/27 �ǉ��I��

            ' WHERE��̍쐬
            '*����ԍ� 000031 2018/03/08 �C���J�n
            'strWHERE = New StringBuilder(Me.CreateWhere(cSearchKey))

            '' �폜�t���O
            'If blnSakujoFG = False Then
            '    If Not (strWHERE.Length = 0) Then
            '        strWHERE.Append(" AND ")
            '    End If
            '    strWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SAKUJOFG)
            '    strWHERE.Append(" <> '1'")
            'End If
            strWHERE = New StringBuilder(CreateWhereMain(cSearchKey, blnSakujoFG))
            '*����ԍ� 000031 2018/03/08 �C���I��

            'ORDER�������

            '�Z���D��敪���h1�h�ł����уR�[�h���w��ς̏ꍇ�F�Z���[�\����
            If ((cSearchKey.p_strJuminYuseniKB = "1") And (cSearchKey.p_strStaiCD.Trim <> String.Empty)) Then
                strORDER.Append(" ORDER BY ")
                strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINHYOHYOJIJUN).Append(" ASC,")
                strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;")
            ElseIf Not (cSearchKey.p_strUmareYMD.Trim = String.Empty) Then
                strORDER.Append(" ORDER BY ")
                strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD).Append(" ASC,")
                strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;")
            Else
                strORDER.Append(" ORDER BY ")
                strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI).Append(" ASC,")
                strORDER.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(" ASC;")
            End If

            If strWHERE.RLength = 0 Then
                strSQL.Append(strORDER)
            Else
                strSQL.Append(" WHERE ").Append(strWHERE).Append(strORDER)
            End If

            '* ����ԍ� 000017 2005/01/25 �ǉ��J�n�i�{��j
            strSQLExec = strSQL.ToString()
            '* ����ԍ� 000017 2005/01/25 �ǉ��I��

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            ''* ����ԍ� 000017 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            'If (m_blnBatch = False) Then
            '    m_cfLogClass.RdbWrite(m_cfControlData,
            '                                "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                                "�y���s���\�b�h��:GetDataSet�z" +
            '                                "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQLExec, m_cfSelectUFParameterCollectionClass) + "�z")
            'End If
            '* ����ԍ� 000017 2005/01/25 �X�V�I���i�{��jIf ���ň͂�

            '* ����ԍ� 000014 2004/08/27 �ύX�J�n�i�{��j
            ' SQL�̎��s DataSet�̎擾
            'csAtenaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
            csAtenaEntity = m_csDataSchmaKobetu.Clone()
            csAtenaEntity = m_cfRdbClass.GetDataSet(strSQLExec, csAtenaEntity, ABAtenaEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '* ����ԍ� 000014 2004/08/27 �ύX�I��

            ' MaxRows�l��߂�
            m_cfRdbClass.p_intMaxRows = intMaxRows

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

        Return csAtenaEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�����}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000026
        'Dim csInstRow As DataRow
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000026
        Dim intInsCnt As Integer                            '�ǉ�����
        Dim strUpdateDateTime As String
        '*����ԍ� 000034 2023/08/14 �ǉ��J�n
        Dim m_cRuijiClass As New USRuijiClass                   ' �ގ������N���X
        '*����ԍ� 000034 2023/08/14 �ǉ��I��

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000018 2005/05/23 �C���J�n
                'Call CreateSQL(csDataRow)
                Call CreateInsertSQL(csDataRow)
                '* ����ԍ� 000018 2005/05/23 �C���I��
            End If

            '*����ԍ� 000034 2023/08/14 �ǉ��J�n
            '�����p�������̂ɗގ����Z�b�g����
            '*����ԍ� 000035 2023/10/19 �C���J�n
            'csDataRow(ABAtenaEntity.SEARCHKANJIMEISHO) =
            '    m_cRuijiClass.GetRuijiMojiList(csDataRow(ABAtenaEntity.SEARCHKANJIMEISHO).ToString)
            csDataRow(ABAtenaEntity.SEARCHKANJIMEISHO) =
                m_cRuijiClass.GetRuijiMojiList(CStr(csDataRow(ABAtenaEntity.SEARCHKANJIMEISHO)).Replace("�@", String.Empty)).ToUpper
            '*����ԍ� 000035 2023/10/19 �C���I��
            '*����ԍ� 000034 2023/08/14 �ǉ��I��

            '�X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '�쐬����

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaEntity.TANMATSUID) = m_cfControlData.m_strClientId '�[���h�c
            csDataRow(ABAtenaEntity.SAKUJOFG) = "0"                               '�폜�t���O
            csDataRow(ABAtenaEntity.KOSHINCOUNTER) = Decimal.Zero                 '�X�V�J�E���^
            csDataRow(ABAtenaEntity.SAKUSEINICHIJI) = strUpdateDateTime           '�쐬����
            csDataRow(ABAtenaEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId  '�쐬���[�U�[
            csDataRow(ABAtenaEntity.KOSHINNICHIJI) = strUpdateDateTime            '�X�V����
            csDataRow(ABAtenaEntity.KOSHINUSER) = m_cfControlData.m_strUserId   '�X�V���[�U�[


            ''���N���X�̃f�[�^�������`�F�b�N���s��
            'For Each csDataColumn In csDataRow.Table.Columns
            '    '�f�[�^�������`�F�b�N
            '    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString().Trim)
            'Next csDataColumn


            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam


            '*����ԍ� 000009 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                        "�ySQL���e:" + m_strInsertSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")
            '*����ԍ� 000009 2003/08/28 �C���I��

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
    '*����ԍ� 000029 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaB() As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csAtenaDr As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����j
    '* �@�@           csAtenaFZYDr As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t���j
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow) As Integer
        Dim intInsCnt As Integer = 0
        Dim intInsCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "InsertAtenaB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����}�X�^�ǉ������s
            intInsCnt = Me.InsertAtenaB(csAtenaDr)

            '�Z��@�����ȍ~�̂Ƃ�
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '�����t���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaFZYB)) Then
                    m_csAtenaFZYB = New ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�쐬�����A�X�V�����̓���
                csAtenaFZYDr(ABAtenaFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaEntity.SAKUSEINICHIJI)
                csAtenaFZYDr(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                '�����t���}�X�^�ǉ������s
                intInsCnt2 = m_csAtenaFZYB.InsertAtenaFZYB(csAtenaFZYDr)
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

        Return intInsCnt

    End Function
    '*����ԍ� 000029 2011/10/24 �ǉ��I��

    '*����ԍ� 000034 2023/08/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow,
    '                                              ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csAtenaDr As DataRow          : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����j
    '* �@�@           csAtenaHyojunDr As DataRow    : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i����_�W���j
    '* �@�@           csAtenaFZYDr As DataRow       : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t���j
    '* �@�@           csAtenaFZYHyojunDr As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t��_�W���j
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow,
                                 ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        Dim intInsCnt As Integer = 0
        Dim intInsCnt2 As Integer = 0
        Dim intInsCnt3 As Integer = 0
        Dim intInsCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "InsertAtenaB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����}�X�^�ǉ������s
            intInsCnt = Me.InsertAtenaB(csAtenaDr)

            ''����_�W���}�X�^�����݂��Ă���ꍇ
            If (Not IsNothing(csAtenaHyojunDr)) Then
                '����_�W���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaHyojunB)) Then
                    m_csAtenaHyojunB = New ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�쐬�����A�X�V�����̓���
                csAtenaHyojunDr(ABAtenaFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaEntity.SAKUSEINICHIJI)
                csAtenaHyojunDr(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                '����_�W���}�X�^�ǉ������s
                intInsCnt2 = m_csAtenaHyojunB.InsertAtenaHyojunB(csAtenaHyojunDr)

            End If

            '�Z��@�����ȍ~�̂Ƃ�
            If (m_blnJukihoKaiseiFG) Then

                '�����t���}�X�^�����݂���ꍇ
                If (Not IsNothing(csAtenaFZYDr)) Then

                    '�����t���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaFZYB)) Then
                        m_csAtenaFZYB = New ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�쐬�����A�X�V�����̓���
                    csAtenaFZYDr(ABAtenaFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaEntity.SAKUSEINICHIJI)
                    csAtenaFZYDr(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                    '�����t���}�X�^�ǉ������s
                    intInsCnt3 = m_csAtenaFZYB.InsertAtenaFZYB(csAtenaFZYDr)

                End If

                '�����t��_�W���}�X�^�����݂���ꍇ
                If (Not IsNothing(csAtenaFZYHyojunDr)) Then

                    '�����t��_�W���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaFZYHyojunB)) Then
                        m_csAtenaFZYHyojunB = New ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�쐬�����A�X�V�����̓���
                    csAtenaFZYHyojunDr(ABAtenaFZYHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaEntity.SAKUSEINICHIJI)
                    csAtenaFZYHyojunDr(ABAtenaFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                    '�����t��_�W���}�X�^�ǉ������s
                    intInsCnt4 = m_csAtenaFZYHyojunB.InsertAtenaFZYHyojunB(csAtenaFZYHyojunDr)

                End If

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

        Return intInsCnt

    End Function
    '*����ԍ� 000034 2023/08/14 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�����}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csDataRow As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateAtenaB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateAtenaB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000026
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000026
        Dim intUpdCnt As Integer                            '�X�V����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000018 2005/05/23 �C���J�n
                'Call CreateSQL(csDataRow)
                Call CreateUpdateSQL(csDataRow)
                '* ����ԍ� 000018 2005/05/23 �C���I��
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABAtenaEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaEntity.KOSHINCOUNTER)) + 1               '�X�V�J�E���^
            csDataRow(ABAtenaEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '�X�V����
            csDataRow(ABAtenaEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[


            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaEntity.PREFIX_KEY.RLength) = ABAtenaEntity.PREFIX_KEY) Then
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '�f�[�^�������`�F�b�N
                    '*����ԍ� 000008 2004/11/12 �C���J�n
                    'CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString().Trim)
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                    '*����ԍ� 000008 2004/11/12 �C���I��
                End If
            Next cfParam

            '*����ԍ� 000009 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                        "�ySQL���e:" + m_strUpdateSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")
            '*����ԍ� 000009 2003/08/28 �C���I��

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
    '*����ԍ� 000029 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaB() As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csAtenaDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����j
    '* �@�@           csAtenaFZYDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t���j
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "UpdateAtenaB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����}�X�^�X�V�����s
            intCnt = Me.UpdateAtenaB(csAtenaDr)

            '�Z��@�����ȍ~�̂Ƃ�
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '�����t���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaFZYB)) Then
                    m_csAtenaFZYB = New ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�X�V�����̓���
                csAtenaFZYDr(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                '�����t���}�X�^�X�V�����s
                intCnt2 = m_csAtenaFZYB.UpdateAtenaFZYB(csAtenaFZYDr)
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

        Return intCnt

    End Function
    '*����ԍ� 000029 2011/10/24 �ǉ��I��

    '*����ԍ� 000034 2023/08/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
    '*                                             ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csAtenaDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����j
    '* �@�@           csAtenaHyojunDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i����_�W���j
    '* �@�@           csAtenaFZYDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t���j
    '* �@�@           csAtenaFZYHyojunDr As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t��_�W���j
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow,
                                 ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow,
                                 Optional ByVal blnJutogai As Boolean = True) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0
        Dim intCnt3 As Integer = 0
        Dim intCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "UpdateAtenaB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����}�X�^�X�V�����s
            intCnt = Me.UpdateAtenaB(csAtenaDr)

            '����_�W���}�X�^�����݂���ꍇ�A�X�V������
            If (Not IsNothing(csAtenaHyojunDr)) Then
                '����_�W���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaHyojunB)) Then
                    m_csAtenaHyojunB = New ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�X�V�����̓���
                csAtenaHyojunDr(ABAtenaHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                '����_�W���}�X�^�X�V�����s
                If (blnJutogai) Then
                    intCnt2 = m_csAtenaHyojunB.UpdateAtenaHyojunB(csAtenaHyojunDr, csAtenaDr(ABAtenaEntity.ATENADATAKB).ToString)
                Else
                    intCnt2 = m_csAtenaHyojunB.UpdateAtenaHyojunB(csAtenaHyojunDr)
                End If
            End If

            '�Z��@�����ȍ~�̂Ƃ�
            If (m_blnJukihoKaiseiFG) Then

                '�����t���}�X�^�����݂���ꍇ�A�X�V������
                If (Not IsNothing(csAtenaFZYDr)) Then
                    '�����t���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaFZYB)) Then
                        m_csAtenaFZYB = New ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYDr(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                    '�����t���}�X�^�X�V�����s
                    intCnt3 = m_csAtenaFZYB.UpdateAtenaFZYB(csAtenaFZYDr)
                End If

                '�����t��_�W���}�X�^�����݂���ꍇ�A�X�V������
                If (Not IsNothing(csAtenaFZYHyojunDr)) Then
                    '�����t���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaFZYHyojunB)) Then
                        m_csAtenaFZYHyojunB = New ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYHyojunDr(ABAtenaFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                    '�����t���}�X�^�X�V�����s
                    intCnt4 = m_csAtenaFZYHyojunB.UpdateAtenaFZYHyojunB(csAtenaFZYHyojunDr)
                End If
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

        Return intCnt

    End Function
    '*����ԍ� 000034 2023/08/14 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�폜
    '* 
    '* �\��           Public Function DeleteAtenaB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@�����}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csDataRow As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaB"
        Dim cfParam As UFParameterClass  '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000026
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000026
        Dim intDelCnt As Integer        '�폜����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000018 2005/05/23 �C���J�n
                'CreateSQL(csDataRow)
                Call CreateDeleteRonriSQL(csDataRow)
                '* ����ԍ� 000018 2005/05/23 �C���I��
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABAtenaEntity.SAKUJOFG) = "1"                                                                 '�폜�t���O
            csDataRow(ABAtenaEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaEntity.KOSHINCOUNTER)) + 1               '�X�V�J�E���^
            csDataRow(ABAtenaEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '�X�V����
            csDataRow(ABAtenaEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[

            '* ����ԍ� 000018 2005/05/23 �C���J�n
            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            'For Each cfParam In m_cfUpdateUFParameterCollectionClass
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaEntity.PREFIX_KEY.RLength) = ABAtenaEntity.PREFIX_KEY) Then
                    '    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = _
                    '            csDataRow(cfParam.ParameterName.Substring(ABAtenaEntity.PREFIX_KEY.Length), _
                    '                      DataRowVersion.Original).ToString()
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                                 csDataRow(cfParam.ParameterName.RSubstring(ABAtenaEntity.PREFIX_KEY.RLength),
                                           DataRowVersion.Original).ToString()
                Else
                    '*����ԍ� 000008 2004/11/12 �C���J�n
                    '�f�[�^�������`�F�b�N
                    'CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString().Trim)
                    '*����ԍ� 000008 2004/11/12 �C���I��
                    'm_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.Substring(ABAtenaEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString()
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam
            '* ����ԍ� 000018 2005/05/23 �C���I��


            '*����ԍ� 000009 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                        "�ySQL���e:" + m_strUpdateSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")
            '*����ԍ� 000009 2003/08/28 �C���I��

            ' SQL�̎��s
            'intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfUpdateUFParameterCollectionClass)
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
    '*����ԍ� 000029 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�폜
    '* 
    '* �\��           Public Function UpdateAtenaB() As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csAtenaDr As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����j
    '* �@�@           csAtenaFZYDr As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t���j
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "DeleteAtenaB"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����}�X�^�X�V�����s
            intCnt = Me.DeleteAtenaB(csAtenaDr)

            '�Z��@�����ȍ~�̂Ƃ�
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '�����t���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaFZYB)) Then
                    m_csAtenaFZYB = New ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�X�V�����̓���
                csAtenaFZYDr(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                '�����t���}�X�^�X�V�����s
                intCnt2 = m_csAtenaFZYB.DeleteAtenaFZYB(csAtenaFZYDr)
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

        Return intCnt

    End Function
    '*����ԍ� 000029 2011/10/24 �ǉ��I��

    '*����ԍ� 000034 2023/08/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�폜
    '* 
    '* �\��           Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, _
    '*                                                       ByVal csAtenaFZYDr As DataRow, _
    '*                                                       ByVal csAtenaHyojunDr As DataRow, _
    '*                                                       ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�A�����t���}�X�^�A����_�W���}�X�^�A�����t��_�W���}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csAtenaDr As DataRow           : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����j
    '* �@�@           csAtenaHyojunDr As DataRow     : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i����_�W���j
    '* �@�@           csAtenaFZYDr As DataRow        : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t���j
    '* �@�@           csAtenaFZYHyojunDr As DataRow  : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t��_�W���j
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    '*����ԍ� 000035 2023/10/19 �C���J�n
    'Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow,
    '                                       ByVal csAtenaHyojunDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow,
                                           ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        '*����ԍ� 000035 2023/10/19 �C���I��

        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0
        Dim intCnt3 As Integer = 0
        Dim intCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "DeleteAtenaB"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����}�X�^�X�V�����s
            intCnt = Me.DeleteAtenaB(csAtenaDr)

            '�Z��@�����ȍ~�̂Ƃ�
            If (m_blnJukihoKaiseiFG) Then

                '����_�W���}�X�^�̃f�[�^�����݂���ꍇ�A�������s��
                If (Not IsNothing(csAtenaHyojunDr)) Then

                    '����_�W���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaHyojunB)) Then
                        m_csAtenaHyojunB = New ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaHyojunDr(ABAtenaHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                    '����_�W���}�X�^�X�V�����s
                    intCnt2 = m_csAtenaHyojunB.DeleteAtenaHyojunB(csAtenaHyojunDr)

                End If

                '�����t���}�X�^�̃f�[�^�����݂���ꍇ�A�������s��
                If (Not IsNothing(csAtenaFZYDr)) Then

                    '�����t���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaFZYB)) Then
                        m_csAtenaFZYB = New ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYDr(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                    '�����t���}�X�^�X�V�����s
                    intCnt3 = m_csAtenaFZYB.DeleteAtenaFZYB(csAtenaFZYDr)

                End If

                '�����t��_�W���}�X�^�̃f�[�^�����݂���ꍇ�A�������s��
                If (Not IsNothing(csAtenaFZYHyojunDr)) Then

                    '�����t��_�W���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaFZYHyojunB)) Then
                        m_csAtenaFZYHyojunB = New ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYHyojunDr(ABAtenaFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                    '�����t��_�W���}�X�^�X�V�����s
                    intCnt4 = m_csAtenaFZYHyojunB.DeleteAtenaFZYHyojun(csAtenaFZYHyojunDr)

                End If

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

        Return intCnt

    End Function
    '*����ԍ� 000034 2023/08/14 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�����폜
    '* 
    '* �\��           Public Function DeleteAtenaB(ByVal csDataRow As DataRow, _
    '*                                               ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@�@�����}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csDataRow As DataRow : �폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaB(ByVal csDataRow As DataRow,
                                             ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteAtenaB"
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

            End If

            ' �폜�p�̃p�����[�^�tDELETE��������ƃp�����[�^�R���N�V�������쐬����
            If (m_strDelButuriSQL Is Nothing Or m_strDelButuriSQL = String.Empty Or
                    IsNothing(m_cfDelButuriUFParameterCollectionClass)) Then
                '* ����ԍ� 000018 2005/05/23 �C���J�n
                'CreateSQL(csDataRow)
                Call CreateDeleteButsuriSQL(csDataRow)
                '* ����ԍ� 000018 2005/05/23 �C���I��
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass

                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaEntity.PREFIX_KEY.RLength) = ABAtenaEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                End If
            Next cfParam


            '*����ԍ� 000009 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                        "�ySQL���e:" + m_strUpdateSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData,
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" +
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
            '                            "�y���s���\�b�h��:ExecuteSQL�z" +
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "�z")
            '*����ԍ� 000003 2003/08/28 �C���I��

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
    '*����ԍ� 000029 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�����폜
    '* 
    '* �\��           Public Function UpdateAtenaB() As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csAtenaDr As DataRow : �����폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����j
    '* �@�@           csAtenaFZYDr As DataRow : �����폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t���j
    '*                strSakujoKB As String �F �폜�敪  
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow, ByVal strSakujoKB As String) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "DeleteAtenaB"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����}�X�^�X�V�����s
            intCnt = Me.DeleteAtenaB(csAtenaDr, strSakujoKB)

            '�Z��@�����ȍ~�̂Ƃ�
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '�����t���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaFZYB)) Then
                    m_csAtenaFZYB = New ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�����t���}�X�^�X�V�����s
                intCnt2 = m_csAtenaFZYB.DeleteAtenaFZYB(csAtenaFZYDr, strSakujoKB)
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

        Return intCnt

    End Function
    '*����ԍ� 000029 2011/10/24 �ǉ��I��

    '*����ԍ� 000034 2023/08/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����}�X�^�����폜
    '* 
    '* �\��           Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, _
    '*                                                       ByVal csAtenaFZYDr As DataRow, _
    '*                                                       ByVal csAtenaHyojunDr As DataRow, _
    '*                                                       ByVal csAtenaFZYHyojunDr As DataRow, _
    '*                                                       ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@ �����}�X�^�A�����t���}�X�^�A����_�W���}�X�^�A�����t��_�W���}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csAtenaDr As DataRow           : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����j
    '* �@�@           csAtenaHyojunDr As DataRow     : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i����_�W���j
    '* �@�@           csAtenaFZYDr As DataRow        : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t���j
    '* �@�@           csAtenaFZYHyojunDr As DataRow  : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����t��_�W���j
    '*                strSakujoKB As String          �F �폜�敪  
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    '*����ԍ� 000035 2023/10/19 �C���J�n
    'Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow,
    '                                       ByVal csAtenaHyojunDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow,
    '                                       ByVal strSakujoKB As String) As Integer
    Public Overloads Function DeleteAtenaB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow,
                                           ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow,
                                           ByVal strSakujoKB As String) As Integer
        '*����ԍ� 000035 2023/10/19 �C���I��

        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0
        Dim intCnt3 As Integer = 0
        Dim intCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "DeleteAtenaB"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����}�X�^�X�V�����s
            intCnt = Me.DeleteAtenaB(csAtenaDr, strSakujoKB)

            '����_�W���}�X�^�̃f�[�^�����݂���ꍇ�A�������s��
            If (Not IsNothing(csAtenaHyojunDr)) Then

                '����_�W���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaHyojunB)) Then
                    m_csAtenaHyojunB = New ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�X�V�����̓���
                csAtenaHyojunDr(ABAtenaHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                '����_�W���}�X�^�X�V�����s
                intCnt2 = m_csAtenaHyojunB.DeleteAtenaHyojunB(csAtenaHyojunDr, strSakujoKB)

            End If

            '�Z��@�����ȍ~�̂Ƃ�
            If (m_blnJukihoKaiseiFG) Then

                '�����t���}�X�^�̃f�[�^�����݂���ꍇ�A�������s��
                If (Not IsNothing(csAtenaFZYDr)) Then

                    '�����t���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaFZYB)) Then
                        m_csAtenaFZYB = New ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYDr(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                    '�����t���}�X�^�X�V�����s
                    intCnt3 = m_csAtenaFZYB.DeleteAtenaFZYB(csAtenaFZYDr, strSakujoKB)

                End If

                '�����t��_�W���}�X�^�̃f�[�^�����݂���ꍇ�A�������s��
                If (Not IsNothing(csAtenaFZYHyojunDr)) Then

                    '�����t��_�W���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaFZYHyojunB)) Then
                        m_csAtenaFZYHyojunB = New ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�X�V�����̓���
                    csAtenaFZYHyojunDr(ABAtenaFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaEntity.KOSHINNICHIJI)

                    '�����t��_�W���}�X�^�X�V�����s
                    intCnt4 = m_csAtenaFZYHyojunB.DeleteAtenaFZYHyojunB(csAtenaFZYHyojunDr, strSakujoKB)

                End If

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

        Return intCnt

    End Function
    '*����ԍ� 000034 2023/08/14 �ǉ��I��

    '* ����ԍ� 000018 2005/05/23 �폜�J�n
    ''************************************************************************************************
    ''* ���\�b�h��     SQL���̍쐬
    ''* 
    ''* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    ''* 
    ''* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    ''* 
    ''* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    ''* 
    ''* �߂�l         �Ȃ�
    ''************************************************************************************************
    'Private Sub CreateSQL(ByVal csDataRow As DataRow)

    '    Const THIS_METHOD_NAME As String = "CreateSQL"
    '    Dim csDataColumn As DataColumn
    '    Dim csInsertColumn As StringBuilder                 'INSERT�p�J������`
    '    Dim csInsertParam As StringBuilder                  'INSERT�p�p�����[�^��`
    '    Dim cfUFParameterClass As UFParameterClass
    '    Dim csWhere As StringBuilder                        'WHERE��`
    '    Dim csUpdateParam As StringBuilder                  'UPDATE�pSQL��`
    '    Dim csDelRonriParam As StringBuilder                '�_���폜�p�����[�^��`


    '    Try
    '        ' �f�o�b�O�J�n���O�o��
    '        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        ' SELECT SQL���̍쐬
    '        m_strInsertSQL = "INSERT INTO " + ABAtenaEntity.TABLE_NAME + " "
    '        csInsertColumn = New StringBuilder()
    '        csInsertParam = New StringBuilder()


    '        ' UPDATE SQL���̍쐬
    '        m_strUpdateSQL = "UPDATE " + ABAtenaEntity.TABLE_NAME + " SET "
    '        csUpdateParam = New StringBuilder()


    '        ' WHERE���̍쐬
    '        csWhere = New StringBuilder()
    '        csWhere.Append(" WHERE ")
    '        csWhere.Append(ABAtenaEntity.JUMINCD)
    '        csWhere.Append(" = ")
    '        csWhere.Append(ABAtenaEntity.KEY_JUMINCD)
    '        csWhere.Append(" AND ")
    '        csWhere.Append(ABAtenaEntity.JUMINJUTOGAIKB)
    '        csWhere.Append(" = ")
    '        csWhere.Append(ABAtenaEntity.KEY_JUMINJUTOGAIKB)
    '        csWhere.Append(" AND ")
    '        csWhere.Append(ABAtenaEntity.KOSHINCOUNTER)
    '        csWhere.Append(" = ")
    '        csWhere.Append(ABAtenaEntity.KEY_KOSHINCOUNTER)


    '        ' �_��DELETE SQL���̍쐬
    '        csDelRonriParam = New StringBuilder()
    '        csDelRonriParam.Append("UPDATE ")
    '        csDelRonriParam.Append(ABAtenaEntity.TABLE_NAME)
    '        csDelRonriParam.Append(" SET ")
    '        csDelRonriParam.Append(ABAtenaEntity.TANMATSUID)
    '        csDelRonriParam.Append(" = ")
    '        csDelRonriParam.Append(ABAtenaEntity.PARAM_TANMATSUID)
    '        csDelRonriParam.Append(", ")
    '        csDelRonriParam.Append(ABAtenaEntity.SAKUJOFG)
    '        csDelRonriParam.Append(" = ")
    '        csDelRonriParam.Append(ABAtenaEntity.PARAM_SAKUJOFG)
    '        csDelRonriParam.Append(", ")
    '        csDelRonriParam.Append(ABAtenaEntity.KOSHINCOUNTER)
    '        csDelRonriParam.Append(" = ")
    '        csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINCOUNTER)
    '        csDelRonriParam.Append(", ")
    '        csDelRonriParam.Append(ABAtenaEntity.KOSHINNICHIJI)
    '        csDelRonriParam.Append(" = ")
    '        csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINNICHIJI)
    '        csDelRonriParam.Append(", ")
    '        csDelRonriParam.Append(ABAtenaEntity.KOSHINUSER)
    '        csDelRonriParam.Append(" = ")
    '        csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINUSER)
    '        csDelRonriParam.Append(csWhere)
    '        m_strDelRonriSQL = csDelRonriParam.ToString

    '        ' ����DELETE SQL���̍쐬
    '        m_strDelButuriSQL = "DELETE FROM " + ABAtenaEntity.TABLE_NAME + csWhere.ToString

    '        ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
    '        m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

    '        ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
    '        m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

    '        ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
    '        m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

    '        ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
    '        m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass()


    '        ' �p�����[�^�R���N�V�����̍쐬
    '        For Each csDataColumn In csDataRow.Table.Columns
    '            cfUFParameterClass = New UFParameterClass()

    '            ' INSERT SQL���̍쐬
    '            csInsertColumn.Append(csDataColumn.ColumnName)
    '            csInsertColumn.Append(", ")

    '            csInsertParam.Append(ABAtenaEntity.PARAM_PLACEHOLDER)
    '            csInsertParam.Append(csDataColumn.ColumnName)
    '            csInsertParam.Append(", ")


    '            ' UPDATE SQL���̍쐬
    '            csUpdateParam.Append(csDataColumn.ColumnName)
    '            csUpdateParam.Append(" = ")
    '            csUpdateParam.Append(ABAtenaEntity.PARAM_PLACEHOLDER)
    '            csUpdateParam.Append(csDataColumn.ColumnName)
    '            csUpdateParam.Append(", ")

    '            ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
    '            cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    '            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    '            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    '            cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    '            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        Next csDataColumn

    '        '�Ō�̃J���}����菜����INSERT�����쐬
    '        m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
    '                + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"


    '        ' UPDATE SQL���̃g���~���O
    '        m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray())

    '        ' UPDATE SQL����WHERE��̒ǉ�
    '        m_strUpdateSQL += csWhere.ToString


    '        ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_TANMATSUID
    '        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SAKUJOFG
    '        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINCOUNTER
    '        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINNICHIJI
    '        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINUSER
    '        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
    '        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB
    '        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER
    '        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
    '        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB
    '        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER
    '        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    '        ' �f�o�b�O�I�����O�o��
    '        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '    Catch objAppExp As UFAppException
    '        ' ���[�j���O���O�o��
    '        m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                    "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
    '                                    "�y���[�j���O���e:" + objAppExp.Message + "�z")
    '        ' �G���[�����̂܂܃X���[����
    '        Throw objAppExp

    '    Catch objExp As Exception
    '        ' �G���[���O�o��
    '        m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                    "�y�G���[���e:" + objExp.Message + "�z")
    '        ' �G���[�����̂܂܃X���[����
    '        Throw objExp
    '    End Try

    'End Sub
    '* ����ԍ� 000018 2005/05/23 �폜�I��

    '* ����ԍ� 000018 2005/05/23 �ǉ��J�n
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
            m_strInsertSQL = "INSERT INTO " + ABAtenaEntity.TABLE_NAME + " "
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

                csInsertParam.Append(ABAtenaEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '�Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"

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
            m_strUpdateSQL = "UPDATE " + ABAtenaEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE���̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABAtenaEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaEntity.KEY_JUMINJUTOGAIKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaEntity.KEY_KOSHINCOUNTER)

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�Z���b�c�E�Z���Z�o�O�敪�E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If Not (csDataColumn.ColumnName = ABAtenaEntity.JUMINCD) AndAlso
                    Not (csDataColumn.ColumnName = ABAtenaEntity.JUMINJUTOGAIKB) AndAlso
                     Not (csDataColumn.ColumnName = ABAtenaEntity.SAKUSEIUSER) AndAlso
                      Not (csDataColumn.ColumnName = ABAtenaEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL���̍쐬
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABAtenaEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(", ")

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray())

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += csWhere.ToString

            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER
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
            csWhere.Append(ABAtenaEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaEntity.KEY_JUMINJUTOGAIKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaEntity.KEY_KOSHINCOUNTER)


            ' �_��DELETE SQL���̍쐬
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABAtenaEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABAtenaEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINCOUNTER)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABAtenaEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABAtenaEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            ' Where���̒ǉ�
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER
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
            csWhere.Append(ABAtenaEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaEntity.JUMINJUTOGAIKB)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaEntity.KEY_JUMINJUTOGAIKB)
            csWhere.Append(" AND ")
            csWhere.Append(ABAtenaEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABAtenaEntity.KEY_KOSHINCOUNTER)

            ' ����DELETE SQL���̍쐬
            m_strDelButuriSQL = "DELETE FROM " + ABAtenaEntity.TABLE_NAME + csWhere.ToString

            ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINJUTOGAIKB
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_KOSHINCOUNTER
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

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
    '* ����ԍ� 000018 2005/05/23 �ǉ��I��

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
    Private Function CreateWhere(ByVal cSearchKey As ABAtenaSearchKey) As String
        Const THIS_METHOD_NAME As String = "CreateWhere"
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass
        Dim strWhereHyojun As String
        Dim strWhereFzy As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT�p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

            ' WHERE��̍쐬
            '* ����ԍ� 000017 2005/01/25 �X�V�J�n�i�{��j
            'csWHERE = New StringBuilder()
            csWHERE = New StringBuilder(256)
            '* ����ԍ� 000017 2005/01/25 �X�V�I��

            ' �Z���R�[�h
            If Not (cSearchKey.p_strJuminCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                '*����ԍ� 000013 2003/11/18 �C���J�n
                'csWHERE.Append(ABAtenaEntity.JUMINCD)
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD)
                '*����ԍ� 000013 2003/11/18 �C���I��
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUMINCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
                cfUFParameterClass.Value = cSearchKey.p_strJuminCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z���D��敪
            If Not (cSearchKey.p_strJuminYuseniKB.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINYUSENIKB)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUMINYUSENIKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINYUSENIKB
                cfUFParameterClass.Value = cSearchKey.p_strJuminYuseniKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z�o�O�D��敪
            If Not (cSearchKey.p_strJutogaiYusenKB.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTOGAIYUSENKB)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUTOGAIYUSENKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUTOGAIYUSENKB
                cfUFParameterClass.Value = cSearchKey.p_strJutogaiYusenKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' ���уR�[�h
            If Not (cSearchKey.p_strStaiCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAICD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_STAICD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_STAICD
                cfUFParameterClass.Value = cSearchKey.p_strStaiCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '*����ԍ� 000032 2020/01/10 �C���J�n
            '' �����p�J�i����
            'If Not (cSearchKey.p_strSearchKanaSeiMei.Trim = String.Empty) Then
            '    If Not (csWHERE.Length = 0) Then
            '        csWHERE.Append(" AND ")
            '    End If

            '    If cSearchKey.p_strSearchKanaSeiMei.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEIMEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSeiMei
            '    Else
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEIMEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEIMEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSeiMei.TrimEnd
            '    End If
            '    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            'End If

            '' �����p�J�i��
            'If Not (cSearchKey.p_strSearchKanaSei.Trim = String.Empty) Then
            '    If Not (csWHERE.Length = 0) Then
            '        csWHERE.Append(" AND ")
            '    End If
            '    '* ����ԍ� 000024 2007/10/10 �ǉ��J�n
            '    ' �O���l�{���D�挟�� OR�����������邽�߂Ɋ��ʂł�����
            '    If (cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty) Then
            '        csWHERE.Append(" ( ")
            '    End If
            '    '* ����ԍ� 000024 2007/10/10 �ǉ��I��
            '    If cSearchKey.p_strSearchKanaSei.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    Else
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei.TrimEnd

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    End If
            'End If

            ''* ����ԍ� 000024 2007/10/10 �ǉ��J�n
            '' �����p�J�i���Q��OR�����Œǉ�
            '' �����J�i���Q�Ɍ����L�[���i�[����Ă���ꍇ�͌��������Ƃ��Ēǉ�
            'If ((cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty)) Then
            '    csWHERE.Append(" OR ")
            '    If cSearchKey.p_strSearchKanaSei2.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEI2)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei2

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    Else
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANASEI2)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANASEI2
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaSei2.TrimEnd

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    End If
            '    ' OR�����͌����p�J�i���݂̂ł̏����Ȃ̂Ŋ��ʂŊ���
            '    csWHERE.Append(" ) ")
            'End If
            ''* ����ԍ� 000024 2007/10/10 �ǉ��I��

            '' �����p�J�i��
            'If Not (cSearchKey.p_strSearchKanaMei.Trim = String.Empty) Then
            '    If Not (csWHERE.Length = 0) Then
            '        csWHERE.Append(" AND ")
            '    End If
            '    If cSearchKey.p_strSearchKanaMei.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANAMEI)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANAMEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaMei

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    Else
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANAMEI)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaEntity.KEY_SEARCHKANAMEI)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEARCHKANAMEI
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanaMei.TrimEnd

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    End If
            'End If

            '' �����p��������
            'If Not (cSearchKey.p_strSearchKanjiMeisho.Trim = String.Empty) Then
            '    If Not (csWHERE.Length = 0) Then
            '        csWHERE.Append(" AND ")
            '    End If
            '    If cSearchKey.p_strSearchKanjiMeisho.IndexOf("%") = -1 Then
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANJIMEISHO)
            '        csWHERE.Append(" = ")
            '        csWHERE.Append(ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanjiMeisho

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    Else
            '        csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANJIMEISHO)
            '        csWHERE.Append(" LIKE ")
            '        csWHERE.Append(ABAtenaEntity.PARAM_SEARCHKANJIMEISHO)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass
            '        cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SEARCHKANJIMEISHO
            '        cfUFParameterClass.Value = cSearchKey.p_strSearchKanjiMeisho.TrimEnd

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '    End If
            'End If

            ''* ����ԍ� 000023 2007/09/03 �ǉ��J�n
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
            ''* ����ԍ� 000023 2007/09/03 �ǉ��I��

            ' �������������𐶐�
            m_cKensakuShimeiB.CreateWhereForShimei(cSearchKey, ABAtenaEntity.TABLE_NAME, csWHERE, m_cfSelectUFParameterCollectionClass,
                                                   ABAtenaFZYHyojunEntity.TABLE_NAME)
            '*����ԍ� 000032 2020/01/10 �C���I��

            ' ���N����
            If Not (cSearchKey.p_strUmareYMD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                If cSearchKey.p_strUmareYMD.RIndexOf("%") = -1 Then
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaEntity.KEY_UMAREYMD)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_UMAREYMD
                    cfUFParameterClass.Value = cSearchKey.p_strUmareYMD

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                Else
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaEntity.KEY_UMAREYMD)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_UMAREYMD
                    cfUFParameterClass.Value = cSearchKey.p_strUmareYMD.TrimEnd

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            End If

            ' ����
            If Not (cSearchKey.p_strSeibetsuCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEIBETSUCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_SEIBETSUCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_SEIBETSUCD
                cfUFParameterClass.Value = cSearchKey.p_strSeibetsuCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z���R�[�h
            If Not (cSearchKey.p_strJushoCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHOCD)
                '* ����ԍ� 000019 2005/07/11 �C���J�n
                '*********************************************************
                '*** �Z��CD���S���Z��CD���̔��肵�āAWhere�����쐬���� ***
                '*********************************************************
                'csWHERE.Append(" = ")
                'csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUSHOCD

                If (cSearchKey.p_strJushoCD.Trim.RLength = 11 AndAlso
                    cSearchKey.p_strJushoCD.RRemove(0, 2) = "000000000") Then
                    ' 11���� ���� ��9����"0"�̂Ƃ��A��2���ł����܂�����
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD)
                    cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RSubstring(0, 2) + "%"
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

                ElseIf (cSearchKey.p_strJushoCD.Trim.RLength = 11 AndAlso
                        cSearchKey.p_strJushoCD.RRemove(0, 5) = "000000") Then
                    ' 11���� ���� ��6����"0"�̂Ƃ��A��5���ł����܂�����
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD)
                    cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RSubstring(0, 5) + "%"
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                ElseIf (cSearchKey.p_strJushoCD.Trim.RLength = 11 AndAlso
                        cSearchKey.p_strJushoCD.RRemove(0, 8) = "000") Then
                    ' 11���� ���� ��3����"0"�̂Ƃ��A��8���ł����܂�����
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD)
                    cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RSubstring(0, 8) + "%"
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                Else
                    ' 13���Ō���
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaEntity.KEY_JUSHOCD)
                    If (cSearchKey.p_strJushoCD.Trim.RLength = 11) Then
                        cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RPadRight(13)
                    Else
                        cfUFParameterClass.Value = cSearchKey.p_strJushoCD.RPadLeft(13)
                    End If

                    '' ���������̃p�����[�^���쐬
                    'cfUFParameterClass = New UFParameterClass()
                    'cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUSHOCD
                    'cfUFParameterClass.Value = cSearchKey.p_strJushoCD
                    '* ����ԍ� 000019 2005/07/11 �C���I��

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                End If
            End If

            ' �s����R�[�h
            If Not (cSearchKey.p_strGyoseikuCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_GYOSEIKUCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_GYOSEIKUCD
                cfUFParameterClass.Value = cSearchKey.p_strGyoseikuCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �n��R�[�h�P
            If Not (cSearchKey.p_strChikuCD1.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_CHIKUCD1)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_CHIKUCD1
                cfUFParameterClass.Value = cSearchKey.p_strChikuCD1

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �n��R�[�h�Q
            If Not (cSearchKey.p_strChikuCD2.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_CHIKUCD2)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_CHIKUCD2
                cfUFParameterClass.Value = cSearchKey.p_strChikuCD2

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �n��R�[�h�R
            If Not (cSearchKey.p_strChikuCD3.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD3)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_CHIKUCD3)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_CHIKUCD3
                cfUFParameterClass.Value = cSearchKey.p_strChikuCD3

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Ԓn�R�[�h�P
            If Not (cSearchKey.p_strBanchiCD1.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_BANCHICD1)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_BANCHICD1
                cfUFParameterClass.Value = cSearchKey.p_strBanchiCD1

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Ԓn�R�[�h�Q
            If Not (cSearchKey.p_strBanchiCD2.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_BANCHICD2)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_BANCHICD2
                cfUFParameterClass.Value = cSearchKey.p_strBanchiCD2

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Ԓn�R�[�h�R
            If Not (cSearchKey.p_strBanchiCD3.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD3)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_BANCHICD3)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_BANCHICD3
                cfUFParameterClass.Value = cSearchKey.p_strBanchiCD3

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z��Z���R�[�h
            If Not (cSearchKey.p_strJukiJushoCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHOCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUKIJUSHOCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIJUSHOCD
                cfUFParameterClass.Value = cSearchKey.p_strJukiJushoCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z��s����R�[�h
            If Not (cSearchKey.p_strJukiGyoseikuCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUKIGYOSEIKUCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIGYOSEIKUCD
                cfUFParameterClass.Value = cSearchKey.p_strJukiGyoseikuCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z��n��R�[�h�P
            If Not (cSearchKey.p_strJukiChikuCD1.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_JUKICHIKUCD1)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_JUKICHIKUCD1
                cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD1

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z��n��R�[�h�Q
            If Not (cSearchKey.p_strJukiChikuCD2.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_JUKICHIKUCD2)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_JUKICHIKUCD2
                cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD2

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z��n��R�[�h�R
            If Not (cSearchKey.p_strJukiChikuCD3.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD3)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_JUKICHIKUCD3)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_JUKICHIKUCD3
                cfUFParameterClass.Value = cSearchKey.p_strJukiChikuCD3

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z��Ԓn�R�[�h�P
            If Not (cSearchKey.p_strJukiBanchiCD1.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUKIBANCHICD1)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIBANCHICD1
                cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD1

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z��Ԓn�R�[�h�Q
            If Not (cSearchKey.p_strJukiBanchiCD2.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUKIBANCHICD2)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIBANCHICD2
                cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD2

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z��Ԓn�R�[�h�R
            If Not (cSearchKey.p_strJukiBanchiCD3.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD3)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUKIBANCHICD3)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUKIBANCHICD3
                cfUFParameterClass.Value = cSearchKey.p_strJukiBanchiCD3

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �f�[�^�敪
            If Not (cSearchKey.p_strDataKB.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                '*����ԍ� 000013 2003/11/18 �C���J�n
                'csWHERE.Append(ABAtenaEntity.ATENADATAKB)
                'csWHERE.Append(" = ")
                'csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB)

                If cSearchKey.p_strDataKB.RIndexOf("%") = -1 Then
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB)
                Else
                    csWHERE.Append(ABAtenaEntity.ATENADATAKB)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB)

                End If
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_ATENADATAKB
                cfUFParameterClass.Value = cSearchKey.p_strDataKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

                '*����ԍ� 000013 2003/11/18 �C���I��

                ' ���������̃p�����[�^���쐬
            End If

            If Not ((cSearchKey.p_strJuminShubetu1 = String.Empty) And (cSearchKey.p_strJuminShubetu2 = String.Empty)) Then
                If (cSearchKey.p_strDataKB.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    csWHERE.Append("((")
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB)
                    csWHERE.Append(" = '11')")
                    csWHERE.Append(" OR (")
                    csWHERE.Append(ABAtenaEntity.ATENADATAKB)
                    csWHERE.Append(" = '12'))")
                End If

                '�Z����ʂP
                If Not (cSearchKey.p_strJuminShubetu1.Trim = String.Empty) Then
                    If Not (csWHERE.RLength = 0) Then
                        csWHERE.Append(" AND ")
                    End If
                    csWHERE.Append(" {fn SUBSTRING(")
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU)
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
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU)
                    csWHERE.Append(",2,1)} = '")
                    csWHERE.Append(cSearchKey.p_strJuminShubetu2)
                    csWHERE.Append("'")
                End If
            End If

            ''���ԔN����
            'If Not (strKikanYMD.Trim = String.Empty) Then
            '    If Not (csWHERE.Length = 0) Then
            '        csWHERE.Append(" AND ")
            '    End If
            '    csWHERE.Append(ABAtenaEntity.RRKST_YMD)
            '    csWHERE.Append(" <= ")
            '    csWHERE.Append(ABAtenaEntity.KEY_RRKST_YMD)
            '    csWHERE.Append(" AND ")
            '    csWHERE.Append(ABAtenaEntity.RRKED_YMD)
            '    csWHERE.Append(" >= ")
            '    csWHERE.Append(ABAtenaEntity.KEY_RRKED_YMD)

            '    ' ���������̃p�����[�^���쐬
            '    cfUFParameterClass = New UFParameterClass()
            '    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_RRKST_YMD
            '    cfUFParameterClass.Value = strKikanYMD
            '    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)

            '    ' ���������̃p�����[�^���쐬
            '    cfUFParameterClass = New UFParameterClass()
            '    cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_RRKED_YMD
            '    cfUFParameterClass.Value = strKikanYMD
            '    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '    m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            'End If

            ' �s�����R�[�h
            If Not (cSearchKey.p_strShichosonCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                '*����ԍ� 000013 2003/11/18 �C���J�n
                'csWHERE.Append(ABAtenaEntity.SHICHOSONCD)
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHICHOSONCD)
                '*����ԍ� 000013 2003/11/18 �C���I��
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_SHICHOSONCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SHICHOSONCD
                cfUFParameterClass.Value = cSearchKey.p_strShichosonCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '*����ԍ� 000030 2014/04/28 �ǉ��J�n
            ' ---------------------------------------------------------------------------------------------------------
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
                    csWHERE.AppendFormat("{0}.{1} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
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
                                         ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KJNHJNKB,
                                         ABAtenaEntity.PARAM_KJNHJNKB)

                    ' ���������̃p�����[�^�[���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KJNHJNKB
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
            ' ---------------------------------------------------------------------------------------------------------
            '*����ԍ� 000030 2014/04/28 �ǉ��I��

            ' �d�b�ԍ�
            If Not (cSearchKey.p_strRenrakusaki.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append("((")
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI1)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_RENRAKUSAKI1)
                csWHERE.Append(") OR (")
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI2)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_RENRAKUSAKI2)
                csWHERE.Append("))")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_RENRAKUSAKI1
                cfUFParameterClass.Value = cSearchKey.p_strRenrakusaki

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_RENRAKUSAKI2
                cfUFParameterClass.Value = cSearchKey.p_strRenrakusaki

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�����W��
            strWhereHyojun = Me.CreateWhereHyojun(cSearchKey)
            If (strWhereHyojun.RLength > 0) Then

                If (csWHERE.RLength > 0) Then
                    csWHERE.Append(" AND ")
                Else
                    ' noop
                End If

                csWHERE.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                csWHERE.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUMINCD)
                csWHERE.AppendFormat(" WHERE {0}", strWhereHyojun)
                csWHERE.Append(")")
            Else
                ' noop
            End If

            '�����t��
            strWhereFzy = Me.CreateWhereFZY(cSearchKey)
            If (strWhereFzy.RLength > 0) Then

                If (csWHERE.RLength > 0) Then
                    csWHERE.Append(" AND ")
                Else
                    ' noop
                End If

                csWHERE.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                csWHERE.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD)
                csWHERE.AppendFormat(" WHERE {0}", strWhereFzy)
                csWHERE.Append(")")
            Else
                ' noop
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

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
    '* 
    '* �@�\           �X�V�f�[�^�̐��������`�F�b�N����B
    '* 
    '* ����           strColumnName As String   : �����}�X�^�f�[�^�Z�b�g�̍��ږ�
    '* �@�@           strValue As String        : ���ڂɑΉ�����l
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Const TABLENAME As String = "�����D"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����

        Try
            ' �f�o�b�O�J�n���O�o��
            'm_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'")

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If


            Select Case strColumnName.ToUpper()

                Case ABAtenaEntity.JUMINCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KYUSHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUMINJUTOGAIKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUMINJUTOGAIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUMINYUSENIKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUMINYUSENIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUTOGAIYUSENKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTOGAIYUSENKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.ATENADATAKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ATENADATAKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.STAICD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_STAICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUMINHYOCD               '�Z���[�R�[�h
                    '�`�F�b�N�Ȃ�

                Case ABAtenaEntity.SEIRINO                  '�����ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEIRINO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.ATENADATASHU             '�����f�[�^���
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ATENADATASHU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.HANYOKB1                 '�ėp�敪1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HANYOKB1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KJNHJNKB                 '�l�@�l�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KJNHJNKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.HANYOKB2                 '�ėp�敪2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HANYOKB2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANNAIKANGAIKB           '�Ǔ��ǊO�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANNAIKANGAIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANAMEISHO1              '�J�i����1
                    '*����ԍ� 000012 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000012 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANAMEISHO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANJIMEISHO1             '��������1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIMEISHO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANAMEISHO2              '�J�i����2
                    '*����ԍ� 000012 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000012 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANAMEISHO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANJIMEISHO2             '��������2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIMEISHO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANJIHJNKEITAI           '�����@�l�`��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIHJNKEITAI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI   '�����@�l��\�Ҏ���
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SEARCHKANJIMEISHO        '�����p��������
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEARCHKANJIMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KYUSEI                   '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KYUSEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SEARCHKANASEIMEI         '�����p�J�i����
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i����", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SEARCHKANASEI            '�����p�J�i��
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i��", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SEARCHKANAMEI            '�����p�J�i��
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i��", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIRRKNO                '�Z���ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIRRKNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.RRKST_YMD                '�����J�n�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_RRKST_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.RRKED_YMD                '�����I���N����
                    If Not (strValue = String.Empty Or strValue = "00000000" Or strValue = "99999999") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_RRKED_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                    'Case ABAtenaEntity.UMAREYMD                 '���N����
                    '    If Not (strValue = String.Empty Or strValue = "00000000") Then
                    '        m_cfDateClass.p_strDateValue = strValue
                    '        If (Not m_cfDateClass.CheckDate()) Then
                    '            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '            '�G���[��`���擾
                    '            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_UMAREYMD)
                    '            '��O�𐶐�
                    '            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    '        End If
                    '    End If

                    'Case ABAtenaEntity.UMAREWMD                 '���a��N����
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾(�������ړ��͂̌��ł��B�F)
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "���a��N����", objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaEntity.SEIBETSUCD               '���ʃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEIBETSUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SEIBETSU                 '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEIBETSU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SEKINO                   '�Дԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SEKINO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUMINHYOHYOJIJUN         '�Z���[�\����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUMINHYOHYOJIJUN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.ZOKUGARACD               '�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimEnd)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZOKUGARACD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.ZOKUGARA                 '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZOKUGARA)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.DAI2JUMINHYOHYOJIJUN     '��Q�Z���[�\����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2JUMINHYOHYOJIJUN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.DAI2ZOKUGARACD           '��Q�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimEnd)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2ZOKUGARACD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.DAI2ZOKUGARA             '��Q����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2ZOKUGARA)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.STAINUSJUMINCD           '���ю�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_STAINUSJUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.STAINUSMEI               '���ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANASTAINUSMEI           '�J�i���ю喼
                    '*����ԍ� 000012 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000012 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANASTAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.DAI2STAINUSJUMINCD       '��Q���ю�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2STAINUSJUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.DAI2STAINUSMEI           '��Q���ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_DAI2STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANADAI2STAINUSMEI       '��Q�J�i���ю喼
                    '*����ԍ� 000012 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000012 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANADAI2STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.YUBINNO                  '�X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_YUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUSHOCD                  '�Z���R�[�h
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUSHO                    '�Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.BANCHICD1                '�Ԓn�R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BANCHICD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.BANCHICD2                '�Ԓn�R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BANCHICD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.BANCHICD3                '�Ԓn�R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BANCHICD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.BANCHI                   '�Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KATAGAKIFG               '�����t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KATAGAKIFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KATAGAKICD               '�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KATAGAKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KATAGAKI                 '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.RENRAKUSAKI1             '�A����1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_RENRAKUSAKI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.RENRAKUSAKI2             '�A����2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_RENRAKUSAKI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.HON_ZJUSHOCD             '�{�БS���Z���R�[�h
                    '* ����ԍ� 000015 2004/10/19 �C���J�n�i�}���S���R�j
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000015 2004/10/19 �C���I���i�}���S���R�j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HON_ZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.HON_JUSHO                '�{�ЏZ��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HON_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.HONSEKIBANCHI            '�{�ДԒn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HONSEKIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.HITTOSH                  '�M����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HITTOSH)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CKINIDOYMD               '���߈ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.CKINJIYUCD               '���ߎ��R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CKINJIYU                 '���ߎ��R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CKINTDKDYMD              '���ߓ͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINTDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.CKINTDKDTUCIKB           '���ߓ͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CKINTDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TOROKUIDOYMD             '�o�^�ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.TOROKUIDOWMD             '�o�^�ٓ��a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUIDOWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.TOROKUJIYUCD             '�o�^���R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TOROKUJIYU               '�o�^���R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TOROKUTDKDYMD            '�o�^�͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUTDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.TOROKUTDKDWMD            '�o�^�͏o�a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUTDKDWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.TOROKUTDKDTUCIKB         '�o�^�͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOROKUTDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUTEIIDOYMD              '�Z��ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEIIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.JUTEIIDOWMD              '�Z��ٓ��a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEIIDOWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.JUTEIJIYUCD              '�Z�莖�R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEIJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUTEIJIYU                '�Z�莖�R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEIJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUTEITDKDYMD             '�Z��͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEITDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.JUTEITDKDWMD             '�Z��͏o�a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEITDKDWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.JUTEITDKDTUCIKB          '�Z��͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUTEITDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SHOJOIDOYMD              '�����ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.SHOJOJIYUCD              '�������R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SHOJOJIYU                '�������R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SHOJOTDKDYMD             '�����͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOTDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.SHOJOTDKDTUCIKB          '�����͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOJOTDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUYOTEIIDOYMD     '�]�o�\��͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.TENSHUTSUKKTIIDOYMD      '�]�o�m��͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD   '�]�o�m��ʒm�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTITSUCHIYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.TENSHUTSUNYURIYUCD       '�]�o�����R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUNYURIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUNYURIYU         '�]�o�����R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUNYURIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENUMAEJ_YUBINNO         '�]���O�X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_YUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENUMAEJ_ZJUSHOCD        '�]���O�Z���S���Z���R�[�h
                    '* ����ԍ� 000015 2004/10/19 �C���J�n�i�}���S���R�j
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000015 2004/10/19 �C���I���i�}���S���R�j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_ZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENUMAEJ_JUSHO           '�]���O�Z���Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENUMAEJ_BANCHI          '�]���O�Z���Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_BANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENUMAEJ_KATAGAKI        '�]���O�Z������
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_KATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENUMAEJ_STAINUSMEI      '�]���O�Z�����ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENUMAEJ_STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUYOTEIYUBINNO    '�]�o�\��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD   '�]�o�\��S���Z���R�[�h
                    '* ����ԍ� 000015 2004/10/19 �C���J�n�i�}���S���R�j
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000015 2004/10/19 �C���I���i�}���S���R�j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUYOTEIJUSHO      '�]�o�\��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUYOTEIBANCHI     '�]�o�\��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI   '�]�o�\�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEIKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI '�]�o�\�萢�ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUYOTEISTAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUKKTIYUBINNO     '�]�o�m��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD    '�]�o�m��S���Z���R�[�h
                    '* ����ԍ� 000015 2004/10/19 �C���J�n�i�}���S���R�j
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000015 2004/10/19 �C���I���i�}���S���R�j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUKKTIJUSHO    '�]�o�m��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUKKTIBANCHI      '�]�o�m��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUKKTIKATAGAKI    '�]�o�m�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI  '�]�o�m�萢�ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTISTAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TENSHUTSUKKTIMITDKFG     '�]�o�m�茩�̓t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TENSHUTSUKKTIMITDKFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.BIKOYMD                  '���l�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BIKOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.BIKO                     '���l
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BIKO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.BIKOTENSHUTSUKKTIJUSHOFG '���l�]�o�m��Z���t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BIKOTENSHUTSUKKTIJUSHOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.HANNO                    '�Ŕԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HANNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KAISEIATOFG              '������t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KAISEIATOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KAISEIMAEFG             '�����O�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KAISEIMAEFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KAISEIYMD                '�����N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KAISEIYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.GYOSEIKUCD               '�s����R�[�h
                    '* ����ԍ� 000020 2005/12/26 �C���J�n
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* ����ԍ� 000020 2005/12/26 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_GYOSEIKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.GYOSEIKUMEI              '�s���於
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_GYOSEIKUMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CHIKUCD1                 '�n��R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUCD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CHIKUMEI1                '�n�於1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUMEI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CHIKUCD2                 '�n��R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUCD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CHIKUMEI2                '�n�於2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUMEI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CHIKUCD3                 '�n��R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUCD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CHIKUMEI3                '�n�於3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHIKUMEI3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.TOHYOKUCD                '���[��R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TOHYOKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SHOGAKKOKUCD             '���w�Z��R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHOGAKKOKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.CHUGAKKOKUCD             '���w�Z��R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_CHUGAKKOKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.HOGOSHAJUMINCD           '�ی�ҏZ���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_HOGOSHAJUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANJIHOGOSHAMEI          '�����ی�Җ�
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANJIHOGOSHAMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KANAHOGOSHAMEI           '�J�i�ی�Җ�
                    '*����ԍ� 000012 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000012 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KANAHOGOSHAMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KIKAYMD                  '�A���N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KIKAYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.KARIIDOKB                '���ٓ��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KARIIDOKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SHORITEISHIKB            '������~�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHORITEISHIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SHORIYOKUSHIKB           '�����}�~�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SHORIYOKUSHIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIYUBINNO              '�Z��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIJUSHOCD              '�Z��Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIJUSHO                '�Z��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIBANCHICD1            '�Z��Ԓn�R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIBANCHICD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIBANCHICD2            '�Z��Ԓn�R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIBANCHICD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIBANCHICD3            '�Z��Ԓn�R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIBANCHICD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIBANCHI               '�Z��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIKATAGAKIFG           '�Z������t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIKATAGAKIFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIKATAGAKICD           '�Z������R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIKATAGAKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIKATAGAKI             '�Z�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIGYOSEIKUCD           '�Z��s����R�[�h
                    '* ����ԍ� 000020 2005/12/26 �C���J�n
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* ����ԍ� 000020 2005/12/26 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIGYOSEIKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKIGYOSEIKUMEI          '�Z��s���於
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKIGYOSEIKUMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKICHIKUCD1             '�Z��n��R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUCD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKICHIKUMEI1            '�Z��n�於1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUMEI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKICHIKUCD2             '�Z��n��R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUCD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKICHIKUMEI2            '�Z��n�於2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUMEI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKICHIKUCD3             '�Z��n��R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUCD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.JUKICHIKUMEI3            '�Z��n�於3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_JUKICHIKUMEI3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KAOKUSHIKIKB             '�Ɖ��~�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KAOKUSHIKIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.BIKOZEIMOKU              '���l�Ŗ�
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_BIKOZEIMOKU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KOKUSEKICD               '���ЃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOKUSEKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KOKUSEKI                 '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOKUSEKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.ZAIRYUSKAKCD             '�ݗ����i�R�[�h
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYUSKAKCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.ZAIRYUSKAK               '�ݗ����i
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYUSKAK)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.ZAIRYUKIKAN              '�ݗ�����
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYUKIKAN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.ZAIRYU_ST_YMD            '�ݗ��J�n�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYU_ST_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.ZAIRYU_ED_YMD            '�ݗ��I���N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_ZAIRYU_ED_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaEntity.RESERCE                  '���U�[�u
                    '�`�F�b�N�Ȃ�

                Case ABAtenaEntity.TANMATSUID               '�[���h�c
                    '* ����ԍ� 000010 2003/09/11 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000010 2003/09/11 �C���C��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SAKUJOFG                 '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KOSHINCOUNTER            '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SAKUSEINICHIJI           '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.SAKUSEIUSER              '�쐬���[�U
                    '* ����ԍ� 000011 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000011 2003/10/09 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KOSHINNICHIJI            '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaEntity.KOSHINUSER               '�X�V���[�U
                    '* ����ԍ� 000011 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000011 2003/10/09 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAB_RDBDATATYPE_KOSHINUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + ":" + strValue, objErrorStruct.m_strErrorCode)
                    End If

            End Select

            ' �f�o�b�O�I�����O�o��
            'm_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

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
            '�G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
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
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KYUSHICHOSONCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANYOKB1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KJNHJNKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANYOKB2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANNAIKANGAIKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANAMEISHO1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANAMEISHO2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIHJNKEITAI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANJIMEISHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEIMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANASEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEARCHKANAMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREWMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEIBETSUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEIBETSU).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SEKINO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINHYOHYOJIJUN).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZOKUGARACD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZOKUGARA).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2ZOKUGARACD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2ZOKUGARA).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAINUSJUMINCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANASTAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2STAINUSJUMINCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.DAI2STAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANADAI2STAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.YUBINNO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHOCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKIFG).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TOROKUIDOYMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TOROKUJIYUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TOROKUJIYU).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOIDOYMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOJIYUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOJIYU).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIYUBINNO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHOCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKIFG).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI3)

            '*����ԍ� 000027 2010/05/12 �ǉ��J�n
            ' �{�ЕM���ҏ�񒊏o����
            If (m_strHonsekiKB = "1" AndAlso m_strHonsekiHittoshKB_Param = "1") Then
                ' �{�ЏZ���A�{�ДԒn�A�M���҂𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HON_JUSHO).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HONSEKIBANCHI).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HITTOSH)
            Else
            End If

            ' ������~�敪���o����
            If (m_strShoriteishiKB = "1" AndAlso m_strShoriteishiKB_Param = "1") Then
                ' ������~�敪�𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHORITEISHIKB)
            Else
            End If
            '*����ԍ� 000027 2010/05/12 �ǉ��I��

            '*����ԍ� 000028 2011/05/18 �ǉ��J�n
            If (m_strFrnZairyuJohoKB_Param = "1") Then
                ' �O���l�ݗ����(���ЁA�ݗ����i�R�[�h�A�ݗ����i�A�ݗ����ԁA�ݗ��J�n�N�����A�ݗ��I���N����)�𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKI).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUSKAKCD).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUSKAK).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUKIKAN).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYU_ST_YMD).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYU_ED_YMD)
            Else
            End If
            '*����ԍ� 000028 2011/05/18 �ǉ��I��
        Else
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KYUSHICHOSONCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.STAICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANYOKB1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KJNHJNKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANYOKB2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANNAIKANGAIKB).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANAMEISHO1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANAMEISHO2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIMEISHO2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANJIHJNKEITAI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREYMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.UMAREWMD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANASTAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KANADAI2STAINUSMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.YUBINNO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHOCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUSHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHICD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.BANCHI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKIFG).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KATAGAKI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.RENRAKUSAKI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.GYOSEIKUMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUCD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CHIKUMEI3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIYUBINNO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHOCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIJUSHO).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHICD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIBANCHI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKIFG).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKICD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIKATAGAKI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUCD).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKIGYOSEIKUMEI).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI1).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI2).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUCD3).Append(",")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUKICHIKUMEI3)

            '*����ԍ� 000027 2010/05/12 �ǉ��J�n
            ' �{�ЕM���ҏ�񒊏o����
            If (m_strHonsekiKB = "1" AndAlso m_strHonsekiHittoshKB_Param = "1") Then
                ' �{�ЏZ���A�{�ДԒn�A�M���҂𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HON_JUSHO).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HONSEKIBANCHI).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HITTOSH)
            Else
            End If

            ' ������~�敪���o����
            If (m_strShoriteishiKB = "1" AndAlso m_strShoriteishiKB_Param = "1") Then
                ' ������~�敪�𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHORITEISHIKB)
            Else
            End If
            '*����ԍ� 000027 2010/05/12 �ǉ��I��

            '*����ԍ� 000028 2011/05/18 �ǉ��J�n
            If (m_strFrnZairyuJohoKB_Param = "1") Then
                ' �O���l�ݗ����(���ЁA�ݗ����i�R�[�h�A�ݗ����i�A�ݗ����ԁA�ݗ��J�n�N�����A�ݗ��I���N����)�𒊏o���ڂɃZ�b�g����
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKI).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUSKAKCD).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUSKAK).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYUKIKAN).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYU_ST_YMD).Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ZAIRYU_ED_YMD)
            Else
            End If
            '*����ԍ� 000028 2011/05/18 �ǉ��I��
        End If
        If (Me.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll) Then
            strAtenaSQLsb.Append(",")
            ' ����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KYUSEI).Append(",")
            ' �Z��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEIIDOYMD).Append(",")
            ' �Z�莖�R
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEIJIYU).Append(",")
            ' �]���O�Z���X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_YUBINNO).Append(",")
            ' �]���O�Z���S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_ZJUSHOCD).Append(",")
            ' �]���O�Z���Z��
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_JUSHO).Append(",")
            ' �]���O�Z���Ԓn
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_BANCHI).Append(",")
            ' �]���O�Z������
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_KATAGAKI).Append(",")
            ' �]�o�\��X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO).Append(",")
            ' �]�o�\��S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD).Append(",")
            ' �]�o�\��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD).Append(",")
            ' �]�o�\��Z��
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIJUSHO).Append(",")
            ' �]�o�\��Ԓn
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIBANCHI).Append(",")
            ' �]�o�\�����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI).Append(",")
            ' �]�o�m��X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIYUBINNO).Append(",")
            ' �]�o�m��S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD).Append(",")
            ' �]�o�m��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIIDOYMD).Append(",")
            ' �]�o�m��ʒm�N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD).Append(",")
            ' �]�o�m��Z��
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIJUSHO).Append(",")
            ' �]�o�m��Ԓn
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIBANCHI).Append(",")
            ' �]�o�m�����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI).Append(",")

            ' �����͏o�N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOTDKDYMD).Append(",")
            ' ���ߎ��R�R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CKINJIYUCD).Append(",")

            ' �{�БS���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HON_ZJUSHOCD).Append(",")
            ' �]�o�\�萢�ю喼
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI).Append(",")
            ' �]�o�m�萢�ю喼
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI).Append(",")
            '*����ԍ� 000021 2006/07/31 �ǉ��J�n
            ' ���ЃR�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKICD).Append(",")
            ' �]���O�Z�����ю喼
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_STAINUSMEI)
            'strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKICD)
            '*����ԍ� 000021 2006/07/31 �ǉ��I��

        End If

        '*����ԍ� 000022 2007/04/28 �ǉ��J�n
        If m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo Then
            strAtenaSQLsb.Append(",")
            ' ����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KYUSEI).Append(",")
            ' �Z��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEIIDOYMD).Append(",")
            ' �Z�莖�R
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEIJIYU).Append(",")
            ' �]���O�Z���X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_YUBINNO).Append(",")
            ' �]���O�Z���S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_ZJUSHOCD).Append(",")
            ' �]���O�Z���Z��
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_JUSHO).Append(",")
            ' �]���O�Z���Ԓn
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_BANCHI).Append(",")
            ' �]���O�Z������
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENUMAEJ_KATAGAKI).Append(",")
            ' �]�o�\��X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO).Append(",")
            ' �]�o�\��S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD).Append(",")
            ' �]�o�\��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD).Append(",")
            ' �]�o�\��Z��
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIJUSHO).Append(",")
            ' �]�o�\��Ԓn
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIBANCHI).Append(",")
            ' �]�o�\�����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI).Append(",")
            ' �]�o�m��X�֔ԍ�
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIYUBINNO).Append(",")
            ' �]�o�m��S���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD).Append(",")
            ' �]�o�m��ٓ��N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIIDOYMD).Append(",")
            ' �]�o�m��ʒm�N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD).Append(",")
            ' �]�o�m��Z��
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIJUSHO).Append(",")
            ' �]�o�m��Ԓn
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIBANCHI).Append(",")
            ' �]�o�m�����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI).Append(",")
            ' �����͏o�N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHOJOTDKDYMD).Append(",")
            ' ���ߎ��R�R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CKINJIYUCD).Append(",")
            ' �{�БS���Z���R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HON_ZJUSHOCD).Append(",")
            ' �]�o�\�萢�ю喼
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI).Append(",")
            ' �]�o�m�萢�ю喼
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI).Append(",")
            ' ���ЃR�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKICD).Append(",")
            ' �o�^�͏o�N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TOROKUTDKDYMD).Append(",")
            ' �Z��͏o�N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTEITDKDYMD).Append(",")
            ' �]�o�����R
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.TENSHUTSUNYURIYU).Append(",")
            ' �s�����R�[�h
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHICHOSONCD).Append(",")
            ' ���߈ٓ��N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CKINIDOYMD).Append(",")
            ' �X�V����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOSHINNICHIJI)
        End If
        '*����ԍ� 000022 2007/04/28 �ǉ��I��
        If (m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
            strAtenaSQLsb.Append(",")
            ' ���ߓ͏o�ʒm�敪
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.CKINTDKDTUCIKB).Append(",")
            ' �Ŕԍ�
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.HANNO).Append(",")
            ' �����N����
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KAISEIYMD)
            If (m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo) AndAlso
               (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.NenkinAll) Then
                ' ���ЃR�[�h
                strAtenaSQLsb.Append(",")
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.KOKUSEKICD)
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

        '*����ԍ� 000025 2008/01/15 �ǉ��J�n
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
        '*����ԍ� 000025 2008/01/15 �ǉ��I��
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
    '*����ԍ� 000029 2011/10/24 �ǉ��J�n
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
    '*����ԍ� 000029 2011/10/24 �ǉ��I��

    '*����ԍ� 000030 2014/04/28 �ǉ��J�n
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
    '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
        'Dim cfUFParameterClass As UFParameterClass

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
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME)
                strAtenaSQLsb.Append(".")
                strAtenaSQLsb.Append(ABAtenaEntity.JUMINCD)
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
                strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME)
                strAtenaSQLsb.Append(".")
                strAtenaSQLsb.Append(ABAtenaEntity.JUMINCD)
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
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME)
            strAtenaSQLsb.Append(".")
            strAtenaSQLsb.Append(ABAtenaEntity.JUMINCD)
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
        strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaNenkinEntity.TABLE_NAME).Append(".").Append(ABAtenaNenkinEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENAKOKUHO ON ABATENA.JUMINCD=ABATENAKOKUHO.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaKokuhoEntity.TABLE_NAME).Append(".").Append(ABAtenaKokuhoEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENAINKAN ON ABATENA.JUMINCD=ABATENAINKAN.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaInkanEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaInkanEntity.TABLE_NAME).Append(".").Append(ABAtenaInkanEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENASENKYO ON ABATENA.JUMINCD=ABATENASENKYO.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaSenkyoEntity.TABLE_NAME).Append(".").Append(ABAtenaSenkyoEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENAJITE ON ABATENA.JUMINCD=ABATENAJIDOUTE.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaJiteEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaJiteEntity.TABLE_NAME).Append(".").Append(ABAtenaJiteEntity.JUMINCD)

        ' LEFT OUTER JOIN ABATENAKAIGO ON ABATENA.JUMINCD=ABATENAKAIGO.JUMINCD
        strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKaigoEntity.TABLE_NAME).Append(" ON ")
        strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD)
        strAtenaSQLsb.Append("=")
        strAtenaSQLsb.Append(ABAtenaKaigoEntity.TABLE_NAME).Append(".").Append(ABAtenaKaigoEntity.JUMINCD)

        '*����ԍ� 000025 2008/01/15 �ǉ��J�n
        If (m_strKobetsuShutokuKB = "1") Then
            ' �ʎ����擾�敪��"1"�̏ꍇ�A�������҃}�X�^��JOIN����
            strAtenaSQLsb.Append(" LEFT OUTER JOIN ").Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(" ON ")
            strAtenaSQLsb.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD)
            strAtenaSQLsb.Append("=")
            strAtenaSQLsb.Append(ABAtenaKokiKoreiEntity.TABLE_NAME).Append(".").Append(ABAtenaKokiKoreiEntity.JUMINCD)
        Else
            ' �ʎ����擾�敪���l�����̏ꍇ�A�������s��Ȃ�
        End If
        '*����ԍ� 000025 2008/01/15 �ǉ��I��
    End Sub
    '*����ԍ� 000029 2011/10/24 �ǉ��J�n
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
                                    ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD,
                                    ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB,
                                    ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINJUTOGAIKB)
    End Sub
    '*����ԍ� 000029 2011/10/24 �ǉ��I��

    '*����ԍ� 000030 2014/04/28 �ǉ��J�n
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
        strAtenaSQLsb.AppendFormat("(SELECT * FROM {0} WHERE {1} = '{2}') AS {0} ",
                                    ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.CKINKB, ABMyNumberEntity.DEFAULT.CKINKB.CKIN)
        strAtenaSQLsb.AppendFormat("ON {0}.{1} = {2}.{3} ",
                                    ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD,
                                    ABMyNumberEntity.TABLE_NAME, ABMyNumberEntity.JUMINCD)
    End Sub
    '*����ԍ� 000030 2014/04/28 �ǉ��I��

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
            m_strHonsekiKB = cABAtenaKanriJoho.GetHonsekiKB_Param

            ' ������~�敪�擾�敪�擾
            m_strShoriteishiKB = cABAtenaKanriJoho.GetShoriteishiKB_Param

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
    '*����ԍ� 000027 2010/05/12 �ǉ��I��

    '*����ԍ� 000029 2011/10/24 �ǉ��J�n
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
            Else
                '�����Ȃ�
            End If

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
    '*����ԍ� 000029 2011/10/24 �ǉ��I��

    '*����ԍ� 000030 2014/04/28 �ǉ��J�n
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
    '*����ԍ� 000030 2014/04/28 �ǉ��I��

    '*����ԍ� 000031 2018/03/08 �ǉ��J�n
    ''' <summary>
    ''' ���o����������̐���
    ''' </summary>
    ''' <param name="cSearchKey">�����L�[</param>
    ''' <param name="blnSakujoFG">�폜�t���O</param>
    ''' <returns>���o����������</returns>
    ''' <remarks></remarks>
    Private Function CreateWhereMain(ByVal cSearchKey As ABAtenaSearchKey, ByVal blnSakujoFG As Boolean) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWhere As StringBuilder
        Dim csWhereForRireki As StringBuilder
        Dim strWhereRirekiHyojun As String
        Dim strWhereRirekiFZY As String

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�@������������
            If (cSearchKey.p_blnIsRirekiSearch = True) Then

                ' [��������]

                ' �p�����[�^�[�R���N�V�����N���X�̃C���X�^���X��
                m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass

                ' ���߂ɑ΂��钊�o�����𐶐�
                csWhere = New StringBuilder(Me.CreateWhereForChokkin(cSearchKey, blnSakujoFG))

                ' �����ɑ΂��钊�o�����𐶐�
                csWhereForRireki = New StringBuilder(Me.CreateWhereForRireki(cSearchKey))

                ' �����ɑ΂��钊�o�������w�肳��Ă���ꍇ�A
                ' �Y���҂̏Z���R�[�h�Œ��߂��i�荞��
                If (csWhereForRireki.RLength > 0) Then

                    If (csWhere.RLength > 0) Then
                        csWhere.Append(" AND ")
                    Else
                        ' noop
                    End If

                    csWhere.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                    csWhere.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaRirekiEntity.TABLE_NAME, ABAtenaRirekiEntity.JUMINCD)
                    csWhere.AppendFormat(" WHERE {0}", csWhereForRireki)
                    csWhere.Append(")")

                Else
                    ' noop
                End If

                '����W��
                strWhereRirekiHyojun = Me.CreateWhereRirekiHyojun(cSearchKey)
                If (strWhereRirekiHyojun.RLength > 0) Then

                    If (csWhere.RLength > 0) Then
                        csWhere.Append(" AND ")
                    Else
                        ' noop
                    End If

                    csWhere.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                    csWhere.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaRirekiHyojunEntity.TABLE_NAME, ABAtenaRirekiHyojunEntity.JUMINCD)
                    csWhere.AppendFormat(" WHERE {0}", strWhereRirekiHyojun)
                    csWhere.Append(")")

                Else
                    ' noop
                End If

                '����t��
                strWhereRirekiFZY = Me.CreateWhereRirekiFZY(cSearchKey)
                If (strWhereRirekiFZY.RLength > 0) Then

                    If (csWhere.RLength > 0) Then
                        csWhere.Append(" AND ")
                    Else
                        ' noop
                    End If

                    csWhere.AppendFormat("{0}.{1} IN (", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                    csWhere.AppendFormat("SELECT {0}.{1} FROM {0}", ABAtenaRirekiFZYEntity.TABLE_NAME, ABAtenaRirekiFZYEntity.JUMINCD)
                    csWhere.AppendFormat(" WHERE {0}", strWhereRirekiFZY)
                    csWhere.Append(")")

                Else
                    ' noop
                End If
            Else

                ' [���ߌ���]

                ' �����̏��������̂܂܎��s����
                csWhere = New StringBuilder(Me.CreateWhere(cSearchKey))

                ' �폜�t���O
                If blnSakujoFG = False Then
                    If Not (csWhere.RLength = 0) Then
                        csWhere.Append(" AND ")
                    End If
                    csWhere.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SAKUJOFG)
                    csWhere.Append(" <> '1'")
                End If

            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")
            Throw

        End Try

        Return csWhere.ToString

    End Function

    ''' <summary>
    ''' ���o����������̐����i���ߗp�j
    ''' </summary>
    ''' <param name="cSearchKey">�����L�[</param>
    ''' <param name="blnSakujoFG">�폜�t���O</param>
    ''' <returns>���o����������</returns>
    ''' <remarks></remarks>
    Private Function CreateWhereForChokkin(ByVal cSearchKey As ABAtenaSearchKey, ByVal blnSakujoFG As Boolean) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE��̍쐬
            csWHERE = New StringBuilder(256)

            ' �Z���R�[�h
            If Not (cSearchKey.p_strJuminCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUMINCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINCD
                cfUFParameterClass.Value = cSearchKey.p_strJuminCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z���D��敪
            If Not (cSearchKey.p_strJuminYuseniKB.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUMINYUSENIKB)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUMINYUSENIKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUMINYUSENIKB
                cfUFParameterClass.Value = cSearchKey.p_strJuminYuseniKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �Z�o�O�D��敪
            If Not (cSearchKey.p_strJutogaiYusenKB.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.JUTOGAIYUSENKB)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.KEY_JUTOGAIYUSENKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.KEY_JUTOGAIYUSENKB
                cfUFParameterClass.Value = cSearchKey.p_strJutogaiYusenKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �f�[�^�敪
            If Not (cSearchKey.p_strDataKB.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If

                If cSearchKey.p_strDataKB.RIndexOf("%") = -1 Then
                    csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB)
                Else
                    csWHERE.Append(ABAtenaEntity.ATENADATAKB)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaEntity.PARAM_ATENADATAKB)

                End If

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_ATENADATAKB
                cfUFParameterClass.Value = cSearchKey.p_strDataKB

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �����f�[�^���
            Select Case cSearchKey.p_strDataKB.Trim

                Case ABConstClass.ATENADATAKB_HOJIN
                    ' noop
                Case Else

                    ' [�Z�o���l][�Z�o�O�l][���L][�w��Ȃ�]�̏ꍇ

                    If Not ((cSearchKey.p_strJuminShubetu1 = String.Empty) And (cSearchKey.p_strJuminShubetu2 = String.Empty)) Then
                        If (cSearchKey.p_strDataKB.Trim = String.Empty) Then
                            If Not (csWHERE.RLength = 0) Then
                                csWHERE.Append(" AND ")
                            End If
                            csWHERE.Append("((")
                            csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATAKB)
                            csWHERE.Append(" = '11')")
                            csWHERE.Append(" OR (")
                            csWHERE.Append(ABAtenaEntity.ATENADATAKB)
                            csWHERE.Append(" = '12'))")
                        End If

                        '�Z����ʂP
                        If Not (cSearchKey.p_strJuminShubetu1.Trim = String.Empty) Then
                            If Not (csWHERE.RLength = 0) Then
                                csWHERE.Append(" AND ")
                            End If
                            csWHERE.Append(" {fn SUBSTRING(")
                            csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU)
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
                            csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.ATENADATASHU)
                            csWHERE.Append(",2,1)} = '")
                            csWHERE.Append(cSearchKey.p_strJuminShubetu2)
                            csWHERE.Append("'")
                        End If
                    End If

            End Select

            ' �s�����R�[�h
            If Not (cSearchKey.p_strShichosonCD.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SHICHOSONCD)
                csWHERE.Append(" = ")
                csWHERE.Append(ABAtenaEntity.PARAM_SHICHOSONCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_SHICHOSONCD
                cfUFParameterClass.Value = cSearchKey.p_strShichosonCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' ---------------------------------------------------------------------------------------------------------
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
                    csWHERE.AppendFormat("{0}.{1} ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
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
                                         ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KJNHJNKB,
                                         ABAtenaEntity.PARAM_KJNHJNKB)

                    ' ���������̃p�����[�^�[���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABAtenaEntity.PARAM_KJNHJNKB
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
            ' ---------------------------------------------------------------------------------------------------------

            ' �폜�t���O
            If blnSakujoFG = False Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                csWHERE.Append(ABAtenaEntity.TABLE_NAME).Append(".").Append(ABAtenaEntity.SAKUJOFG)
                csWHERE.Append(" <> '1'")
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")
            Throw

        End Try

        Return csWHERE.ToString

    End Function

    ''' <summary>
    ''' ���o����������̐����i����p�j
    ''' </summary>
    ''' <param name="cSearchKey">�����L�[</param>
    ''' <returns>���o����������</returns>
    ''' <remarks></remarks>
    Private Function CreateWhereForRireki(ByVal cSearchKey As ABAtenaSearchKey) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'WHERE��̍쐬
            csWHERE = New StringBuilder(256)

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

            '*����ԍ� 000032 2020/01/10 �C���J�n
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
            '    ' �����p�J�i���Q�Ɍ����L�[���i�[����Ă���ꍇ�͌��������Ƃ��Ēǉ�
            '    If (cSearchKey.p_strSearchKanaSei2.Trim() <> String.Empty) Then
            '        csWHERE.Append(" ( ")
            '    End If
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

            '' �{���������� �{������="2(Tsusho_Seishiki)"�̂Ƃ��̂݊��������Q�͌������ڂƂȂ�
            'If (cSearchKey.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki) Then
            '    If Not (cSearchKey.p_strKanjiMeisho2.Trim = String.Empty) Then
            '        If Not (csWHERE.Length = 0) Then
            '            csWHERE.Append(" AND ")
            '        End If
            '        If cSearchKey.p_strKanjiMeisho2.IndexOf("%") = -1 Then
            '            csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO2)
            '            csWHERE.Append(" = ")
            '            csWHERE.Append(ABAtenaRirekiEntity.PARAM_KANJIMEISHO2)

            '            ' ���������̃p�����[�^���쐬
            '            cfUFParameterClass = New UFParameterClass
            '            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KANJIMEISHO2
            '            cfUFParameterClass.Value = cSearchKey.p_strKanjiMeisho2

            '            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '        Else
            '            csWHERE.Append(ABAtenaRirekiEntity.TABLE_NAME).Append(".").Append(ABAtenaRirekiEntity.KANJIMEISHO2)
            '            csWHERE.Append(" LIKE ")
            '            csWHERE.Append(ABAtenaRirekiEntity.PARAM_KANJIMEISHO2)

            '            ' ���������̃p�����[�^���쐬
            '            cfUFParameterClass = New UFParameterClass
            '            cfUFParameterClass.ParameterName = ABAtenaRirekiEntity.PARAM_KANJIMEISHO2
            '            cfUFParameterClass.Value = cSearchKey.p_strKanjiMeisho2.TrimEnd

            '            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '            m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            '        End If
            '    End If
            'End If

            ' �������������𐶐�
            m_cKensakuShimeiB.CreateWhereForShimei(cSearchKey, ABAtenaRirekiEntity.TABLE_NAME, csWHERE, m_cfSelectUFParameterCollectionClass,
                                                   ABAtenaRirekiFZYHyojunEntity.TABLE_NAME)
            '*����ԍ� 000032 2020/01/10 �C���I��

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

            ' �@�l�`��
            Select Case cSearchKey.p_strDataKB.Trim

                Case ABConstClass.ATENADATAKB_HOJIN

                    ' [�@�l]�̏ꍇ

                    If Not ((cSearchKey.p_strJuminShubetu1 = String.Empty) And (cSearchKey.p_strJuminShubetu2 = String.Empty)) Then
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

                Case Else
                    ' noop
            End Select

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
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")
            Throw

        End Try

        Return csWHERE.ToString

    End Function
    '*����ԍ� 000031 2018/03/08 �ǉ��I��

    ''' <summary>
    ''' ���o����������̐����i�����W���p�j
    ''' </summary>
    ''' <param name="cSearchKey">�����L�[</param>
    ''' <returns>���o����������</returns>
    ''' <remarks></remarks>
    Private Function CreateWhereHyojun(ByVal cSearchKey As ABAtenaSearchKey) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'WHERE��̍쐬
            csWHERE = New StringBuilder(256)

            '�Z��
            If Not (cSearchKey.p_strJusho.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                If (cSearchKey.p_strJusho.RIndexOf("%") = -1) Then
                    csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHJUSHO)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHJUSHO)
                Else
                    csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHJUSHO)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHJUSHO)
                End If
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SEARCHJUSHO
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
                    csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKATAGAKI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKATAGAKI)
                Else
                    csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKATAGAKI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKATAGAKI)
                End If
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SEARCHKATAGAKI
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
                    csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKANJIKYUUJI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKANJIKYUUJI)
                Else
                    csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKANJIKYUUJI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKANJIKYUUJI)
                End If
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SEARCHKANJIKYUUJI
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
                    csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKANAKYUUJI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKANAKYUUJI)
                Else
                    csWHERE.Append(ABAtenaHyojunEntity.TABLE_NAME).Append(".").Append(ABAtenaHyojunEntity.SEARCHKANAKYUUJI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaHyojunEntity.PARAM_SEARCHKANAKYUUJI)
                End If
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaHyojunEntity.PARAM_SEARCHKANAKYUUJI
                cfUFParameterClass.Value = cSearchKey.p_strKanaKyuuji

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")
            Throw

        End Try

        Return csWHERE.ToString

    End Function

    ''' <summary>
    ''' ���o����������̐����i�����t���p�j
    ''' </summary>
    ''' <param name="cSearchKey">�����L�[</param>
    ''' <returns>���o����������</returns>
    ''' <remarks></remarks>
    Private Function CreateWhereFZY(ByVal cSearchKey As ABAtenaSearchKey) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'WHERE��̍쐬
            csWHERE = New StringBuilder(256)

            '�J�^�J�i���L��
            If Not (cSearchKey.p_strKatakanaHeikimei.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                If (cSearchKey.p_strKatakanaHeikimei.RIndexOf("%") = -1) Then
                    csWHERE.Append(ABAtenaFZYEntity.TABLE_NAME).Append(".").Append(ABAtenaFZYEntity.KATAKANAHEIKIMEI)
                    csWHERE.Append(" = ")
                    csWHERE.Append(ABAtenaFZYEntity.PARAM_KATAKANAHEIKIMEI)
                Else
                    csWHERE.Append(ABAtenaFZYEntity.TABLE_NAME).Append(".").Append(ABAtenaFZYEntity.KATAKANAHEIKIMEI)
                    csWHERE.Append(" LIKE ")
                    csWHERE.Append(ABAtenaFZYEntity.PARAM_KATAKANAHEIKIMEI)
                End If
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABAtenaFZYEntity.PARAM_KATAKANAHEIKIMEI
                cfUFParameterClass.Value = cSearchKey.p_strKatakanaHeikimei

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")
            Throw

        End Try

        Return csWHERE.ToString

    End Function

    ''' <summary>
    ''' ���o����������̐����i��������W���p�j
    ''' </summary>
    ''' <param name="cSearchKey">�����L�[</param>
    ''' <returns>���o����������</returns>
    ''' <remarks></remarks>
    Private Function CreateWhereRirekiHyojun(ByVal cSearchKey As ABAtenaSearchKey) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'WHERE��̍쐬
            csWHERE = New StringBuilder(256)

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

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")
            Throw

        End Try

        Return csWHERE.ToString

    End Function

    ''' <summary>
    ''' ���o����������̐����i�����t���p�j
    ''' </summary>
    ''' <param name="cSearchKey">�����L�[</param>
    ''' <returns>���o����������</returns>
    ''' <remarks></remarks>
    Private Function CreateWhereRirekiFZY(ByVal cSearchKey As ABAtenaSearchKey) As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csWHERE As StringBuilder
        Dim cfUFParameterClass As UFParameterClass

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'WHERE��̍쐬
            csWHERE = New StringBuilder(256)

            '�J�^�J�i���L��
            If Not (cSearchKey.p_strKatakanaHeikimei.Trim = String.Empty) Then
                If Not (csWHERE.RLength = 0) Then
                    csWHERE.Append(" AND ")
                End If
                If (cSearchKey.p_strKatakanaHeikimei.RIndexOf("%") = -1) Then
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

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")
            Throw

        End Try

        Return csWHERE.ToString

    End Function
    '*����ԍ� 000033 2023/03/10 �ǉ��J�n
#Region "�����W���f�[�^���ڕҏW"
    '************************************************************************************************
    '* ���\�b�h��     �����W���f�[�^���ڕҏW
    '* 
    '* �\��           Private SetHyojunEntity()
    '* 
    '* �@�\           �����W���f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetHyojunEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RRKNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.EDANO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHIMEIKANAKAKUNINFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.UMAREBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOUMAREBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JIJITSUSTAINUSMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.MACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SEARCHJUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KANAKATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SEARCHKATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.BANCHIEDABANSUCHI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUSHO_KUNIMEICODE)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUSHO_KUNIMEITO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUSHO_KOKUGAIJUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_SHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_MACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_SHIKUGUNCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HON_MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CKINIDOWMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOCKINIDOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TOROKUIDOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOTOROKUIDOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KISAIYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KISAIBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOKISAIBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUTEIIDOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOJUTEIIDOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KOKUSEKISOSHITSUBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHOJOIDOWMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOSHOJOIDOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_YUBINNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_MACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_BANCHI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUTJ_KATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_TODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_SHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_MACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_BANCHI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SAISHUJ_KATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KAISEIBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOKAISEIBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KAISEISHOJOYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KAISEISHOJOBIFUSHOPTN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOKAISEISHOJOBI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD4)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD5)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD6)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD7)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD8)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD9)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.CHIKUCD10)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TOKUBETSUYOSHIKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HYOJUNIDOKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.NYURYOKUBASHOCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.NYURYOKUBASHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SEARCHKANJIKYUUJI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SEARCHKANAKYUUJI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KYUUJIKANAKAKUNINFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TDKDSHIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.HYOJUNIDOJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.NICHIJOSEIKATSUKENIKICD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TOROKUBUSHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TANKITAIZAISHAFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.KYOYUNINZU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHIZEIJIMUSHOCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHUKKOKUKIKAN_ST)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHUKKOKUKIKAN_ED)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.IDOSHURUI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.SHOKANKUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.TOGOATENAFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOUMAREBI_DATE)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKISHIKUCHOSONCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKIMACHIAZACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKITODOFUKEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKISHIKUCHOSON)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKIMACHIAZA)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKIKANAKATAGAKI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD4)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD5)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD6)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD7)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD8)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD9)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKICHIKUCD10)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUKIBANCHIEDABANSUCHI)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_H", ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.RESERVE5)
    End Sub
#End Region

#Region "�����t���W���f�[�^���ڕҏW"
    '************************************************************************************************
    '* ���\�b�h��     �����t���W���f�[�^���ڕҏW
    '* 
    '* �\��           Private SetFZYHyojunEntity()
    '* 
    '* �@�\           �����t���W���f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYHyojunEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHFRNMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.TSUSHOKANAKAKUNINFG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SHIMEIYUSENKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.ZAIRYUCARDNOKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.JUKYOCHIHOSEICD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.HODAI30JO46MATAHA47KB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.STAINUSSHIMEIYUSENKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.TOKUSHOMEI_YUKOKIGEN)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1} AS {1}_FH", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.RESERVE5)
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
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.FUGENJUGYOSEIKUCD)
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

#Region "�����W���e�[�u��JOIN��쐬"
    '************************************************************************************************
    '* ���\�b�h��     �����W���e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetHyojunJoin()
    '* 
    '* �@�\           �����W���e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetHyojunJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaHyojunEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD,
                                    ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUMINCD)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB,
                                    ABAtenaHyojunEntity.TABLE_NAME, ABAtenaHyojunEntity.JUMINJUTOGAIKB)
    End Sub
#End Region

#Region "�����t���W���e�[�u��JOIN��쐬"
    '************************************************************************************************
    '* ���\�b�h��     �����t���W���e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetFZYHyojunJoin()
    '* 
    '* �@�\           �����t���W���e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYHyojunJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaFZYHyojunEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD,
                                    ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.JUMINCD)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB,
                                    ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB)
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
                                    ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD,
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
                                    ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD,
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
                                        ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD,
                                        ABDENSHISHOMEISHOMSTEntity.JUMINCD)
            strAtenaSQLsb.AppendFormat(" AND {0}.{1} = DS3.{2} ",
                                        ABAtenaEntity.TABLE_NAME, ABAtenaEntity.STAICD,
                                        ABDENSHISHOMEISHOMSTEntity.STAICD)
        End If
    End Sub
#End Region
    '*����ԍ� 000033 2023/03/10 �ǉ��I��
#End Region

End Class
