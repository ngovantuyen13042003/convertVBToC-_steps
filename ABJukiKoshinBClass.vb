'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�����Z��X�V(ABJukiKoshinBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/06/02�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/09/11 000001     �Z���R�[�h������Ή�
'* 2003/09/18 000002     �C��
'* 2003/11/21 000003     �d�l�ύX�F�N���E���ۂ̌ʏ���t���āA�O����o�́i�V�K�̏ꍇ�͌�̂݁j
'* 2004/02/16 000004     �ǉ��E�X�V����R�V�A�g�i���[�N�t���[�j������ǉ��B���v���JDB�ւ̓����o�^
'* 2004/03/09 000005     �Œ莑�Y�łւ̓o�^
'* 2004/08/27 000006     �Œ莑�Y�Ńf�[�^�A�g���䏈���ǉ�
'* 2004/10/20 000007     �Œ莑�Y�ŘA���l�@�l�敪���C��
'* 2005/02/15 000008     �Œ莑�Y�ŘA���ǉ������ȊO�̍X�V��ǉ�
'* 2005/02/28 000009     ���v���J�A�����\�b�h��ǉ��i���v���J�A���N���ӏ����C�j
'* 2005/04/04 000010     �Œ莑�Y�ŘA���ٓ��N�������C��(�}���S���R)
'* 2005/06/05 000011     �����J�n�N�����𓖓��ɂ���
'* 2005/06/07 000012     �O�����I���N�����𒼋ߗ����J�n�N�����̑O���ɂ���
'* 2005/06/17 000013     �����X�V�̏C��
'* 2005/08/17 000014     �����ݐϒǉ����A�ėp�b�c��ABATENARUISEKI��RESERCE�ɃZ�b�g����C��(�O��敪�Q�̎�����)(�}���S���R)
'* 2005/08/17 000015     �����ݐϒǉ����A�ėp�b�c��ABATENARUISEKI��RESERCE�ɃZ�b�g����C��(�O��敪�P�̎���)(�}���S���R)
'* 2005/11/01 000016     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/11/22 000017     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/11/27 000018     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/12/02 000019     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/12/07 000020     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/12/12 000021     �s����b�c���s���於�̂̃J�X�^�}�C�Y(�}���S���R)
'* 2005/12/15 000022     �d�l�ύX�F�s����b�c���s���於�̂̃J�X�^�}�C�Y�@���̂̓Z�b�g���Ȃ�
'* 2005/12/16 000023     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/12/17 000024     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/12/18 000025     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/12/18 000026     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/12/20 000027     JukiDataKoshin���\�b�h�̏C��(�}���S���R)
'* 2005/12/27 000028     CKINJIYUCD��SHORIJIYUCD���Z�b�g���Ȃ��i�d�l�ύX�j
'* 2006/04/19 000029     ABATENARUISEKI��RESERCE�ɃZ�b�g������e��ėp�b�c���珈���b�c�ɕύX����
'* 2006/08/10 000030     �������c���Ȃ��C��(SHORIJIYUCD="03"or"04")�̏ꍇ�A�����J�n�N�����͍X�V���Ȃ�
'*                       �Z��ٓ��Ғǉ���������ɏZ���"03"or"04"�C������ƊJ�n�N���������������Ȃ��Ă��܂�(�}���S���R)
'* 2007/01/30 000031     �]�o�m��Z������A�]�o�\��Z������̏ꍇ���Ԓn�R�[�h��ݒ肷��悤�ɏC��
'* 2007/02/15 000032     �����ݐσ}�X�^�̍X�V���@��ύX
'* 2007/07/13 000033     DB�g���Ή��C�J�����쐬����Maxlength�l���g�����DB�̃T�C�Y�ɑΉ�������
'*                       �i�K�p�͈͂����U���Ă��邽�ߗ���ԍ��̕t�������C�O���l�̂݃R�����g�A�E�g�j�i����j
'* 2007/08/31 000034     UR�Ǘ����F�O���l�{���������䂪"2"�̂Ƃ��͊O���l�{���D�挟���p�ɖ{���J�i�������Z�b�g�i����j
'* 2007/09/05 000035     UR�Ԓn�R�[�h�}�X�^�N���X�̃C���X�^���X���������C���i����j
'* 2007/09/28 000036     �������p�敪�̂��P�̂Ƃ��͒ʏ̖��D��A�Q�̂Ƃ��͖{���D��i����j
'* 2008/05/12 000037     �Ǔ��ǊO�敪�̕ҏW�d�l�̕ύX�ɔ����C���i��Áj
'* 2009/04/07 000038     �ԒnCD�����l�ɂȂ�s��Ή��F�]�o�m�襗\��Ԓn����̔ԒnCD������ԒnCD�ҏW�a�׽�ōs���i�H���j
'* 2009/05/12 000039     �o�b�`�t���O��ǉ��A�y��UR�Ǘ����擾���@���ꕔ�ύX�i��Áj
'* 2009/05/22 000040     �Z�o�O����ē]�����A����ɓ]�o�����ꍇ�̏Z�o�O�D��敪��"0"�ɂȂ�s��̑Ή��i�g�V�j
'*                       ����ɕs�v�ȃ��W�b�N���폜�i�g�V�j
'* 2009/06/18 000041     �����C���ŗ����C���f�[�^��1���̂�(���߃f�[�^�̂�)�̏ꍇ�ɗ����f�[�^���ǉ������s��̑Ή��i��Áj
'* 2009/08/10 000042     ����ԍ�000041�̉��C�R��ɂ��s��Ή��i��Áj
'* 2010/04/16 000043     VS2008�Ή��i��Áj
'* 2011/11/09 000044     �yAB17020�z�Z��@�����Ή��i�����j
'* 2011/11/28 000045     �yAB17020�z�Z��@�����Ή��F���N�����s�ڋ敪�ҏW�d�l�ύX�i���V�j
'* 2011/12/05 000046     �yAB17020�z�Z��@�����Ή��F�����NNo��^�ύX�i���V�j
'* 2011/12/26 000047     �yAB17020�z�Z��@�����Ή��F�]���̎��ɕt�����X�V����Ȃ��s��̑Ή��i�����j
'* 2012/01/05 000048     �yAB17020�z�Z��@�����Ή��F�����C�����A�L�[�d���ƂȂ�G���[�C���i�����j
'* 2012/04/06 000049     �yAB17020�z�Z��@�����Ή��F�����C���i�Z�o�O���Z�o���̊Ԃɓ����j���A�ُ�I������s��C���i�����j
'* 2014/06/25 000050     �yAB21051�z�����ʔԍ��Ή������ʔԍ��X�V�����ǉ��i�΍��j
'* 2014/07/08 000051     �yAB21051�z�����ʔԍ��Ή������ʔԍ��X�V�������R�ǉ��i�΍��j
'* 2014/09/10 000052     �yAB21051�z�����ʔԍ��Ή������ʔԍ��X�V�������R�ǉ��Q�i�΍��j
'* 2014/09/10 000053     �yAB21080�z�����ʔԍ��Ή������ԃT�[�o�[�a�r�A�g�@�\�ǉ��i�΍��j
'* 2014/12/26 000054     �yAB21051�z�����ʔԍ��Ή������ʔԍ��X�V�������R�ǉ��R�i�΍��j
'* 2015/01/08 000055     �yAB21080�z�����ʔԍ��Ή������ԃT�[�o�[�a�r�A�g�@�\�폜�i�΍��j
'* 2015/01/28 000056     �yAB21051�z�����ʔԍ��Ή������ʔԍ��X�V�����C���i�΍��j
'* 2015/02/17 000057     �yAB21051�z�����ʔԍ��Ή������ʔԍ��X�V�����C���i�΍��j
'* 2015/10/14 000058     �yAB21051�z�����ʔԍ��Ή����{�t�ԏ������R�[�h�ɑ΂�����ꏈ���ւ̍l���ǉ��i�΍��j
'* 2018/01/04 000059     �yAB25001�z�������L�Ή��i�΍��j
'* 2022/12/16 000060     �yAB-8010�z�Z���R�[�h���уR�[�h15���Ή�(����)
'* 2023/08/14 000061     �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ�(����)
'* 2023/12/07 000062     �yAB-9000-1�z�Z��X�V�A�g�W�����Ή�(����)
'* 2024/02/06 000063     �yAB-1580-1�z�]�o�Z�������X�V�Ή�(�|��)
'* 2024/03/07 000064     �yAB-0900-1�z�A�h���X�E�x�[�X�E���W�X�g���Ή�(����)
'* 2024/04/02 000065     �yAB-6047-1�z�Z����̈ٓ��ɔ������Ɩ��ւ̊e����񋟂̂��߂̘A�g(��)
'* 2024/06/10 000066     �yAB-9902-1�z�s��Ή�
'* 2024/06/18 000067     �yAB-9903-1�z�s��Ή�
'* 2024/07/05 000068     �yAB-9907-1�z�����D��敪�̑Ή�
'* 2024/07/09 000069     �yAB-9907-1�z�s��Ή��@�s�ڐ��N����DATE�̕ҏW
'************************************************************************************************
'* ���������ݐς̎擾�́A�X�L�[�}�[���擾�o����悤�ɂȂ�΁A�X�L�[�}�[�擾�ɕύX����B(2003/06/05)

Option Strict On
Option Explicit On
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports Densan.Common
Imports System.Data
Imports System.Text
Imports Densan.WorkFlow.UWCommon
'* ����ԍ� 000058 2015/10/14 �ǉ��J�n
Imports Densan.Reams.UR.UR010BB
'* ����ԍ� 000058 2015/10/14 �ǉ��I��
Imports System.Security
Imports Microsoft.SqlServer.Server

Public Class ABJukiKoshinBClass

    ' �p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X
    Private m_strGyosekuInit As String
    Private m_strChiku1Init As String
    Private m_strChiku2Init As String
    Private m_strChiku3Init As String
    Private m_strZokugara1Init As String
    Private m_strZokugara2Init As String
    Private m_cJutogaiB As ABJutogaiBClass                  ' �Z�o�O�c�`�N���X
    Private m_cAtenaB As ABAtenaBClass                      ' �����}�X�^�c�`�N���X
    Private m_cAtenaRirekiB As ABAtenaRirekiBClass          ' ���������c�`�N���X
    Private m_cAtenaRuisekiB As ABAtenaRuisekiBClass        ' �����ݐςc�`�N���X
    Private m_csAtenaEntity As DataSet                      ' ����Entity
    Private m_csAtenaRuisekiEntity As DataSet               ' �����ݐ�Entity
    '*����ԍ� 000003 2003/11/21 �ǉ��J�n
    Private m_cAtenaNenkinB As ABAtenaNenkinBClass          ' �����N���c�`�N���X
    Private m_cAtenaKokuhoB As ABAtenaKokuhoBClass          ' �������ۂc�`�N���X
    '*����ԍ� 000003 2003/11/21 �ǉ��I��
    '*����ԍ� 000004 2004/02/13 �ǉ��J�n   000009 2005/02/28 �폜�J�n
    ''''''Dim m_ABToshoProperty() As ABToshoProperty
    ''''''Dim m_intCnt As Integer
    '*����ԍ� 000004 2004/02/13 �ǉ��J�n   000009 2005/02/28 �폜�I��
    '*����ԍ� 000009 2005/03/18 �ǉ��J�n
    Private m_csAtenaKanriEntity As DataSet                      '�����Ǘ����f�[�^�Z�b�g
    Private m_strR3RenkeiFG As String                            'R3���v���J�A�g�t���O
    Private m_strKoteiRenkeiFG As String                         '�Œ�A�g�t���O
    Private m_strGapeiDate As String = String.Empty              '������
    '*����ԍ� 000027 2005/12/20 �ǉ��J�n
    Private m_strBefGapeiDate As String = String.Empty           ' ����������O
    Private m_strSystemDate As String = String.Empty             ' �V�X�e�����t
    '*����ԍ� 000027 2005/12/20 �ǉ��I��
    '*����ԍ� 000016 2005/11/01 �폜�J�n
    '* corresponds to VS2008 Start 2010/04/16 000043
    ''''Private m_blnGappei As Boolean = False                         '��������t���O
    '* corresponds to VS2008 End 2010/04/16 000043
    '*����ԍ� 000016 2005/11/01 �폜�I��
    Private m_cBAAtenaLinkageBClass As BAAtenaLinkageBClass      ' �Œ莑�Y�ň����N���X
    Private m_cBAAtenaLinkageIFXClass As BAAtenaLinkageIFXClass
    '*����ԍ� 000009 2005/03/18 �ǉ��I��
    '*����ԍ� 000016 2005/11/01 �ǉ��J�n
    Private m_csReRirekiEntity As DataSet                        ' �S�����f�[�^�ޔ�p
    Private m_blnJutogaiAriFG As Boolean = False                 ' �����̒��ɏZ�o�O�����邩�ǂ����̃t���O
    Private m_csJutogaiRows() As DataRow                         ' ���̂c�a�̏Z�o�O�̂q�n�v�r
    Private m_csFirstJutogaiRow As DataRow                       ' ���̂c�a�̍ŏ��̏Z�o�O�q�n�v
    Private m_intRenbanCnt As Integer = 0                        ' ����ҏW�ŗp����A�ԗp�̃J�E���g
    Private m_intJutogaiRowCnt As Integer = 0                    ' ���̂c�a�Ɋ܂܂��Z�o�O�q�n�v�̌���
    Private m_intJutogaiInCnt As Integer = 0                     ' �Z�o�O��ǉ���������
    Private m_intJutogaiST_YMD As Integer                        ' �Z�o�O�q�n�v�̊J�n�N������������
    Private m_blnHenkanFG As Boolean = False                     ' �Z�o�O���N���������ǂ����̃t���O
    '*����ԍ� 000018 2005/11/27 �폜�J�n
    'Private m_blnSaiTenyuFG As Boolean = False                   ' �ē]���������ǂ����̃t���O
    '*����ԍ� 000018 2005/11/27 �폜�I��
    '*����ԍ� 000016 2005/11/01 �ǉ��I��
    '*����ԍ� 000021 2005/12/12 �ǉ��J�n
    Private m_strTenshutsuGyoseikuCD As String                   ' �]�o�҂̍s����b�c
    '*����ԍ� 000022 2005/12/15 �폜�J�n
    'Private m_strTenshutsuGyoseikuMei As String                  ' �]�o�҂̍s���於��
    'Private m_cuGyoseikuCDCashB As URGYOSEIKUCDMSTCacheBClass    ' �s����R�[�h�}�X�^�L���b�V���a
    '*����ԍ� 000022 2005/12/15 �폜�I��
    '*����ԍ� 000021 2005/12/12 �ǉ��I��
    '*����ԍ� 000038 2009/04/07 �폜�J�n
    ''*����ԍ� 000031 2007/01/30 �ǉ��J�n
    'Private m_crBanchiCdMstB As URBANCHICDMSTBClass              ' UR�Ԓn�R�[�h�}�X�^�N���X
    ''*����ԍ� 000031 2007/01/30 �ǉ��I��
    '*����ԍ� 000038 2009/04/07 �폜�I��
    '*����ԍ� 000034 2007/08/31 �ǉ��J�n
    Private cuKanriJohoB As URKANRIJOHOCacheBClass               ' �Ǘ����a�N���X(�L���b�V���Ή���)
    '*����ԍ� 000034 2007/08/31 �ǉ��I��
    '*����ԍ� 000038 2009/04/07 �ǉ��J�n
    Private m_cBanchiCDHenshuB As ABBanchiCDHenshuBClass         ' �Ԓn�R�[�h�ҏW�a�N���X
    '*����ԍ� 000038 2009/04/07 �ǉ��I��
    '*����ԍ� 000039 2009/05/12 �ǉ��J�n
    Protected m_blnBatch As Boolean = False                      ' �o�b�`�敪(True:�o�b�`�n,False:���A���n)
    Private m_cuKanriJohoB_Batch As URKANRIJOHOBClass            ' �Ǘ����a�N���X ���o�b�`�p
    Private m_cFrnHommyoKensakuType As FrnHommyoKensakuType
    '*����ԍ� 000039 2009/05/12 �ǉ��I��
    '*����ԍ� 000041 2009/06/18 �ǉ��J�n
    Private m_blnRirekiShusei As Boolean = False                 ' �����C���f�[�^�폜����t���O
    '*����ԍ� 000041 2009/06/18 �ǉ��I��
    '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
    Private m_cAtenaRirekiFzyB As ABAtenaRirekiFZYBClass         ' ��������t��B�N���X
    Private m_cAtenaFzyB As ABAtenaFZYBClass                     ' �����t��B�N���X
    Private m_csReRirekiFzyEntity As DataSet                     ' ��������t���e�[�u���X�L�[�}
    Private m_csAtenaRuisekiFzyEntity As DataSet                 ' �����ݐϕt���e�[�u���X�L�[�}
    '* ����ԍ� 000044 2011/11/09 �ǉ��I��
    '* ����ԍ� 000050 2014/06/25 �ǉ��J�n
    Private m_cABMyNumberB As ABMyNumberBClass                  ' ���ʔԍ��r�W�l�X�N���X
    Private m_cABMyNumberRuisekiB As ABMyNumberRuisekiBClass    ' ���ʔԍ��ݐσr�W�l�X�N���X
    '* ����ԍ� 000050 2014/06/25 �ǉ��I��
    Private m_cABAtenaHyojunB As ABAtena_HyojunBClass                      ' �����W��B
    Private m_cABAtenaFZYHyojunB As ABAtenaFZY_HyojunBClass                ' �����t���W��B
    Private m_cABAtenaRirekiHyojunB As ABAtenaRireki_HyojunBClass          ' ��������W��B
    Private m_cABAtenaRirekiFZYHyojunB As ABAtenaRirekiFZY_HyojunBClass    ' ��������t���W��B 
    Private m_cABAtenaRuisekiHyojunB As ABAtenaRuiseki_HyojunBClass        ' �����ݐϕW��B
    Private m_cABatenaRuisekiFZYHyojunB As ABAtenaRuisekiFZY_HyojunBClass  ' �����ݐϕt���W��B
    Private m_cuUsRuiji As USRuijiClass                                    ' �ގ��ϊ�
    Private m_csAtenaRuisekiHyojunEntity As DataSet                        ' �����ݐ�_�W��Entity
    Private m_csAtenaRuisekiFZYHyojunEntity As DataSet                     ' �����ݐϕt��_�W��Entity
    Private m_cABBanchiEdabanSuchiB As ABBanchiEdabanSuchiBClass           ' �Ԓn�R�[�h�ҏW�a�N���X
    Private m_csABMyNumberHyojunB As ABMyNumberHyojunBClass                ' ���ʔԍ��W��
    Private m_csAbMyNumberRuisekiHyojunB As ABMyNumberRuisekiHyojunBClass  ' ���ʔԍ��ݐϕW��
    Private m_csReRirekiHyojunEntity As DataSet
    Private m_csRERirekiFZYHyojunEntity As DataSet
    '*����ԍ� 000065 2024/04/02 �ǉ��J�n
    Private m_cABKojinSeigyoB As ABKojinSeigyoBClass                       ' �����l��񐧌�a
    Private m_cABKojinseigyoRirekiB As ABKojinseigyoRirekiBClass           ' �����l��񐧌䗚���a
    Private m_strSeinenKoKenShokiMsg As String                             ' ���N�㌩�l���b�Z�[�W
    '*����ԍ� 000065 2024/04/02 �ǉ��I��

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABJukiKoshinBClass"              ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h
    '*����ԍ� 000004 2004/02/14 �ǉ��J�n   000009 2005/02/28 �폜�J�n
    ''''''Private Const WORK_FLOW_NAME As String = "�����ٓ�"             ' ���[�N�t���[��
    ''''''Private Const DATA_NAME As String = "����"                      '�f�[�^��
    '*����ԍ� 000004 2004/02/14 �ǉ��I��   000009 2005/02/28 �폜�I��
    Private Const FUSHOPTN_FUSHO As String = "1"
    Private Const FUSHOPTN_NASHI As String = "0"
    '*����ԍ� 000065 2024/04/02 �ǉ��J�n
    Private Const ERR_MSG_KOJINSEIGYO As String = "�l������"           ' �G���[���b�Z�[�W_�l������
    Private Const ERR_MSG_KOJINSEIGYORIREKI As String = "�l���䗚�����" ' �G���[���b�Z�[�W_�l���䗚�����
    '*����ԍ� 000065 2024/04/02 �ǉ��I��
    Private Const CNS_KURAN As String = "��"

    '* ����ԍ� 000050 2014/06/25 �ǉ��J�n
    Private Enum ABMyNumberType
        [New] = 0                   ' ���ʔԍ�
        Old                         ' �����ʔԍ�
    End Enum
    '* ����ԍ� 000050 2014/06/25 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass
    '* �@�@                          ByVal csUFRdbClass As UFRdbClass)
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
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' �p�����[�^�̃����o�ϐ�������
        m_strGyosekuInit = String.Empty
        m_strChiku1Init = String.Empty
        m_strChiku2Init = String.Empty
        m_strChiku3Init = String.Empty
        m_strZokugara1Init = String.Empty
        m_strZokugara2Init = String.Empty
        '*����ԍ� 000021 2005/12/12 �ǉ��J�n
        m_strTenshutsuGyoseikuCD = String.Empty
        '*����ԍ� 000022 2005/12/15 �폜�J�n
        'm_strTenshutsuGyoseikuMei = String.Empty
        '*����ԍ� 000022 2005/12/15 �폜�I��
        '*����ԍ� 000021 2005/12/12 �ǉ��I��

    End Sub

#Region "�f�[�^�Z�b�g�쐬"
    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�Z�b�g�쐬
    '* 
    '* �\��           Public Function DataSetSakusei() As DataSet
    '* 
    '* �@�\ �@    �@�@�Z��f�[�^�Z�b�g���쐬����
    '* 
    '* ����           ����
    '* 
    '* �߂�l         DataSet(ABJukiDataEntity) : �Z��f�[�^�Z�b�g
    '************************************************************************************************
    Public Function DataSetSakusei() As DataSet
        Const THIS_METHOD_NAME As String = "DataSetSakusei"
        Dim csJukiDataEntity As DataSet                     ' �f�[�^�Z�b�g
        Dim csJukiDataTable As DataTable                    ' �e�[�u��
        Dim csJukiDataColumn As DataColumn                  ' �J����
        Dim csJukiPrimaryKey(1) As DataColumn               ' ��L�[

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Z��f�[�^Entity�̃C���X�^���X�쐬
            csJukiDataEntity = New DataSet()

            ' �Z��f�[�^�e�[�u���̍쐬
            csJukiDataTable = csJukiDataEntity.Tables.Add(ABJukiData.TABLE_NAME)

            ' �J������`�̍쐬
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 15
            csJukiDataColumn.AllowDBNull = False
            csJukiPrimaryKey(0) = csJukiDataColumn
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHICHOSONCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 6
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 6
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINHYOSHICHOSONCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 6
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RIREKINO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 6
            csJukiDataColumn.AllowDBNull = False
            csJukiPrimaryKey(1) = csJukiDataColumn
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RRKST_YMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RRKED_YMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAICD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 15
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEIRINO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 12
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINSHU, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAMEISHO1, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120        '80
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJIMEISHO1, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120        '40
            csJukiDataColumn.MaxLength = 480        '40
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAMEISHO2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120        '80
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJIMEISHO2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480        '40
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KYUSEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 60         '15
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANASEIMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120        '60
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANJIMEISHO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480        '40
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANASEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 72         '24
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANAMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 48         '16
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.UMAREYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.UMAREWMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEIBETSUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEIBETSU, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 1
            csJukiDataColumn.MaxLength = 10
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEIKINO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINHYOHYOJIJUN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZOKUGARACD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZOKUGARA, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 40         '15
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HYOJIJUN2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZOKUGARACD2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZOKUGARA2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 40         '15
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSJUMINCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 15
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJISTAINUSMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480        '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANASTAINUSMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120        '40
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSJUMINCD2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 15
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJISTAINUSMEI2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480        '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANASTAINUSMEI2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120        '40
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIYUBINNO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIJUSHOCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIJUSHO, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '30
            csJukiDataColumn.MaxLength = 200         '30
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIBANCHICD1, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 5
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIBANCHICD2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 5
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIBANCHICD3, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 5
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIBANCHI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '20
            csJukiDataColumn.MaxLength = 200         '20
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIKATAGAKIFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIKATAGAKICD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 20
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIKATAGAKI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1200        '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RENRAKUSAKI1, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 14
            csJukiDataColumn.MaxLength = 15
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.RENRAKUSAKI2, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 14
            csJukiDataColumn.MaxLength = 15
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KJNRENRAKUSAKI1, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 14
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KJNRENRAKUSAKI2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 14
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_ZJUSHOCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 13
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_JUSHO, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '30
            csJukiDataColumn.MaxLength = 200         '30
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_BANCHI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '20
            csJukiDataColumn.MaxLength = 200         '20
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HITTOSHA, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480        '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINIDOYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINJIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINJIYU, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 10
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINTDKDYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINTDKDTUCIKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUIDOYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUIDOWMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUJIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUJIYU, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 10
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUTDKDYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUTDKDWMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOROKUTDKDTUCIKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEIIDOYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEIIDOWMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEIJIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEIJIYU, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 10
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEITDKDYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEITDKDWMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUTEITDKDTUCIKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOIDOYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOJIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOJIYU, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 10
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOTDKDYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOTDKDTUCIKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIIDOYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIIDOYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTITUCIYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUNYURIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUNYURIYU, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_YUBINNO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_ZJUSHOCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 13
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_JUSHO, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '30
            csJukiDataColumn.MaxLength = 200         '30
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_BANCHI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '20
            csJukiDataColumn.MaxLength = 200         '20
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_KATAGAKI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1200        '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_STAINUSMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480        '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_MITDKFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIYUBINNO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIZJUSHOCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 13
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIJUSHO, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '30
            csJukiDataColumn.MaxLength = 200         '30
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIBANCHI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '20
            csJukiDataColumn.MaxLength = 200         '20
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIKATAGAKI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1200         '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISTAINUSMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480        '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIYUBINNO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIZJUSHOCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 13
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIJUSHO, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '30
            csJukiDataColumn.MaxLength = 200         '30
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIBANCHI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50         '20
            csJukiDataColumn.MaxLength = 200         '20
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIKATAGAKI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1200         '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISTAINUSMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480        '30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIMITDKFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.BIKOYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.BIKO, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 50
            csJukiDataColumn.MaxLength = 200
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.BIKOTENSHUTSUKKTIJUSHOFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.UTSUSHIKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HANNO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 5       '2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KAISEIATOFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KAISEIMAEFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KAISEIYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIGYOSEIKUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 9
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIGYOSEIKUMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUCD1, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUMEI1, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUCD2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUMEI2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUCD3, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKICHIKUMEI3, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOHYOKUCD, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 4
            csJukiDataColumn.MaxLength = 5
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOGAKKOKUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 4
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CHUGAKKOKUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 4
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HOGOSHAJUMINCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 15
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJIHOGOSHAMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120        '30
            csJukiDataColumn.MaxLength = 480        '30
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAHOGOSHAMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120        '40
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KIKAYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KARIIDOKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHORITEISHIKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHORIYOKUSHIKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOKUSEKICD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 3
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOKUSEKI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 20
            csJukiDataColumn.MaxLength = 100
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUSKAKCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 3
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUSKAK, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 15
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUKIKAN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 4
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYU_ST_YMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYU_ED_YMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HANYOCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            '*����ԍ� 000016 2005/11/01 �ǉ��J�n
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHORIJIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            '*����ԍ� 000016 2005/11/01 �ǉ��I��
            '*����ԍ� 000036 2007/09/28 �ǉ��J�n
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIMEIRIYOKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            '*����ԍ� 000036 2007/09/28 �ǉ��I��
            '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TABLEINSERTKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            '* ����ԍ� 000045 2011/12/05 �ǉ��J�n
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.LINKNO, System.Type.GetType("System.Decimal"))
            'csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.LINKNO, System.Type.GetType("System.String"))
            'csJukiDataColumn.MaxLength = 6
            '* ����ԍ� 000045 2011/12/05 �ǉ��I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUMINHYOJOTAIKBN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKYOCHITODOKEFLG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HONGOKUMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAHONGOKUMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJIHEIKIMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANAHEIKIMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANJITSUSHOMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KANATSUSHOMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KATAKANAHEIKIMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.UMAREFUSHOKBN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TSUSHOMEITOUROKUYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUKIKANCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUKIKANMEISHO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 10
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUSHACD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUSHAMEISHO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 20
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUCARDNO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 12
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOFUYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOFUYOTEISTYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KOFUYOTEIEDYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOIDOYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOJIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOJIYU, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 10
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOTDKDYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKITAISHOSHASHOJOTDKDTUCIKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNSTAINUSMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNSTAINUSKANAMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSHEIKIMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSKANAHEIKIMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSTSUSHOMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.STAINUSKANATSUSHOMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_STAINUSMEI_KYOTSU, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_STAINUSHEIKIMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_STAINUSTSUSHOMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISTAINUSMEI_KYOTSU, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISTAINUSHEIKIMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISTAINUSTSUSHOMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISTAINUSMEI_KYOTSU, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISTAINUSHEIKIMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISTAINUSTSUSHOMEI, System.Type.GetType("System.String"))
            '*����ԍ� 000061 2023/08/14 �C���J�n
            'csJukiDataColumn.MaxLength = 120
            csJukiDataColumn.MaxLength = 480
            '*����ԍ� 000061 2023/08/14 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE1, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 50
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE2, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 50
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE3, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 50
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE4, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 50
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FRNRESERVE5, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 50
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE1, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 50
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE2, System.Type.GetType("System.String"))
            '* ����ԍ� 000059 2018/01/04 �C���J�n
            'csJukiDataColumn.MaxLength = 50
            csJukiDataColumn.MaxLength = 80
            '* ����ԍ� 000059 2018/01/04 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE3, System.Type.GetType("System.String"))
            '* ����ԍ� 000059 2018/01/04 �C���J�n
            'csJukiDataColumn.MaxLength = 50
            csJukiDataColumn.MaxLength = 20
            '* ����ԍ� 000059 2018/01/04 �C���I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE4, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 50
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKIRESERVE5, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 50
            '* ����ԍ� 000044 2011/11/09 �ǉ��I��
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.EDANO, System.Type.GetType("System.Decimal"))
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIMEIKANAKAKUNINFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOUMAREBI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 72
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JIJITSUSTAINUSMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIKUCHOSONCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 6
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.MACHIAZACD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TODOFUKEN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 16
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIKUGUNCHOSON, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 48
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.MACHIAZA, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHJUSHO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 200
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKATAGAKI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1200
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.BANCHIEDABANSUCHI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 20
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_SHIKUCHOSONCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 6
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_MACHIAZACD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_TODOFUKEN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 16
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_SHIKUGUNCHOSON, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 48
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HON_MACHIAZA, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.CKINIDOWMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOCKINIDOBI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 72
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HYOJUNKISAIJIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KISAIYMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 8
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HYOJUNSHOJOJIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHOJOIDOWMD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOSHOJOIDOBI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 72
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_SHIKUCHOSONCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 6
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_MACHIAZACD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_TODOFUKEN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 16
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_SHIKUCHOSON, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 48
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_MACHIAZA, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_KOKUSEKICD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 3
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_KOKUSEKI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 200
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENUMAEJ_KOKUGAIJUSHO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 300
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISHIKUCHOSONCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 6
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIMACHIAZACD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEITODOFUKEN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 16
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEISHIKUCHOSON, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 48
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIMACHIAZA, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIKOKUSEKICD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 3
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIKOKUSEKI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 200
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUYOTEIKOKUGAIJUSHO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 300
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISHIKUCHOSONCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 6
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIMACHIAZACD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 7
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTITODOFUKEN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 16
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTISHIKUCHOSON, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 48
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TENSHUTSUKKTIMACHIAZA, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOKUBETSUYOSHIKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.IDOKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.NYURYOKUBASHOCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 4
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.NYURYOKUBASHO, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 30
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANJIKYUUJI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 80
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANAKYUUJI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 20
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.KYUUJIKANAKAKUNINFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TDKDSHIMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HYOJUNIDOJIYUCD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 2
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOUMAREBIDATE, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 10
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOCKINIDOBIDATE, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 10
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.FUSHOSHOJOIDOBIDATE, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 10
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHFRNMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANAFRNMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHTSUSHOMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANATSUSHOMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TSUSHOKANAKAKUNINFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SHIMEIYUSENKB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANJIHEIKIMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 480
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.SEARCHKANAHEIKIMEI, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 120
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.ZAIRYUCARDNOKBN, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.JUKYOCHIHOSEICD, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.HODAI30JO46MATAHA47KB, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1
            csJukiDataColumn = csJukiDataTable.Columns.Add(ABJukiData.TOGOATENAFG, System.Type.GetType("System.String"))
            csJukiDataColumn.MaxLength = 1


            csJukiDataTable.PrimaryKey = csJukiPrimaryKey   ' ��L�[

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
            Throw objExp
        End Try

        Return csJukiDataEntity

    End Function

#End Region

    '************************************************************************************************
    '* ���\�b�h��     �Z��f�[�^�X�V
    '* 
    '* �\��           Public Sub JukiDataKoshin(ByVal csJukiDataEntity As DataSet)
    '* 
    '* �@�\ �@    �@�@�Z��f�[�^�̍X�V�������s�Ȃ�
    '* 
    '* ����           DataSet(csJukiDataEntity) : �Z��f�[�^�Z�b�g
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    <SecuritySafeCritical>
    Public Sub JukiDataKoshin(ByVal csJukiDataEntity As DataSet)
        Const THIS_METHOD_NAME As String = "JukiDataKoshin"
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '�����Ǘ����c�`�r�W�l�X�N���X
        '*����ԍ� 000009 2005/03/18 �폜�J�n
        ''''''Dim csAtenaKanriEntity As DataSet                   '�����Ǘ����f�[�^�Z�b�g
        '*����ԍ� 000009 2005/03/18 �폜�I��
        'Dim csAtenaRirekiEntity As DataSet                  '���������f�[�^�Z�b�g
        Dim csAtenaKanriRow As DataRow                      '�����Ǘ����f�[�^Row
        Dim csJukiDataRow As DataRow                        '�Z��f�[�^Row
        '*����ԍ� 000004 2004/02/13 �ǉ��J�n
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csDataRow As DataRow                            ' �c�������q����
        '* corresponds to VS2008 End 2010/04/16 000043
        '''''Dim cABAtenaCnvBClass As ABAtenaCnvBClass
        'Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        '* corresponds to VS2008 End 2010/04/16 000043
        'Dim cSearchKey As ABAtenaSearchKey                  ' ���������L�[
        ''''''Dim blnGappei As Boolean = False
        '*����ԍ� 000016 2005/11/01 �폜�J�n
        '* corresponds to VS2008 Start 2010/04/16 000043
        ''''Dim strsvjumincd As String
        ''''Dim strSystemDate As String                         '�V�X�e�����t
        '* corresponds to VS2008 End 2010/04/16 000043
        '*����ԍ� 000016 2005/11/01 �폜�J�n
        '*����ԍ� 000004 2004/02/13 �ǉ��I��
        '*����ԍ� 000016 2005/11/18 �ǉ��J�n
        'Dim intDelCnt As Integer
        'Dim intAllCnt As Integer
        '*����ԍ� 000016 2005/11/18 �ǉ��I��
        '*����ԍ� 000017 2005/11/22 �ǉ��J�n
        Dim strBreakJuminCD() As String = {String.Empty, String.Empty}
        '*����ԍ� 000017 2005/11/22 �ǉ��I��
        '*����ԍ� 000019 2005/12/02 �ǉ��J�n
        Dim csJukiDataRows() As DataRow
        '*����ԍ� 000019 2005/12/02 �ǉ��I��
        '*����ԍ� 000021 2005/12/12 �ǉ��J�n 000022 2005/12/15 �폜�J�n
        'Dim csGyoseikuCDMstEntity As DataSet
        '*����ԍ� 000021 2005/12/12 �ǉ��I�� 000022 2005/12/15 �폜�I��
        '*����ԍ� 000027 2005/12/20 �ǉ��J�n
        'Dim csJukiCkinDataRows() As DataRow                 ' �Z��f�[�^�̒��߃��E
        'Dim strJukiCkinST_YMD As String                     ' �Z��f�[�^�̒��߃��E�̊J�n�N����
        '*����ԍ� 000027 2005/12/20 �ǉ��I��
        '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
        'Dim csAtenaRirekiFzyEntity As DataSet               ' ��������t��
        '* ����ԍ� 000044 2011/11/09 �ǉ��I��
        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            '---------------------------------------------------------------------------------------
            ' 1. �Ǘ����̎擾
            '---------------------------------------------------------------------------------------

            '*����ԍ� 000009 2005/03/18 �C���J�n
            '�Ǘ�����ް���Ă������ꍇ�͎擾����
            If m_csAtenaKanriEntity Is Nothing Then

                ' ���t�N���X�̃C���X�^���X��
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                '*����ԍ� 000027 2005/12/20 �ǉ��J�n
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                m_cfDateClass.p_enEraType = UFEraType.Number
                '*����ԍ� 000027 2005/12/20 �ǉ��I��
                ' �Z�o�O�c�`�N���X�̃C���X�^���X�쐬
                m_cJutogaiB = New ABJutogaiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' �����}�X�^�c�`�N���X�̃C���X�^���X�쐬
                m_cAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' ���������c�`�N���X�̃C���X�^���X�쐬
                m_cAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' �����ݐςc�`�N���X�̃C���X�^���X�쐬
                m_cAtenaRuisekiB = New ABAtenaRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                ' ��������t���c�`�N���X�̃C���X�^���X�쐬
                m_cAtenaRirekiFzyB = New ABAtenaRirekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' �����t���c�`�N���X�̃C���X�^���X�쐬
                m_cAtenaFzyB = New ABAtenaFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '* ����ԍ� 000044 2011/11/09 �ǉ��I��
                '* ����ԍ� 000050 2014/06/25 �ǉ��J�n
                ' ���ʔԍ��r�W�l�X�N���X�̃C���X�^���X��
                m_cABMyNumberB = New ABMyNumberBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' ���ʔԍ��ݐσr�W�l�X�N���X�̃C���X�^���X��
                m_cABMyNumberRuisekiB = New ABMyNumberRuisekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '* ����ԍ� 000050 2014/06/25 �ǉ��I��

                '*����ԍ� 000003 2003/11/21 �ǉ��J�n
                ' �����N���c�`�N���X�̃C���X�^���X�쐬
                If (m_cAtenaNenkinB Is Nothing) Then
                    m_cAtenaNenkinB = New ABAtenaNenkinBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                End If

                ' �������ۂc�`�N���X�̃C���X�^���X�쐬
                If (m_cAtenaKokuhoB Is Nothing) Then
                    m_cAtenaKokuhoB = New ABAtenaKokuhoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                End If
                '*����ԍ� 000003 2003/11/21 �ǉ��I��

                '*����ԍ� 000039 2009/05/12 �C���J�n
                If (m_blnBatch = True) Then
                    ' �t�q�Ǘ����a�N���X���C���X�^���X��
                    If (m_cuKanriJohoB_Batch Is Nothing) Then
                        m_cuKanriJohoB_Batch = New URKANRIJOHOBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                    End If
                    ' �O���l�{�������p�����[�^
                    m_cFrnHommyoKensakuType = m_cuKanriJohoB_Batch.GetFrn_HommyoKensaku_Param
                Else
                    ' �t�q�Ǘ����a�L���b�V���N���X���C���X�^���X��
                    If (cuKanriJohoB Is Nothing) Then
                        cuKanriJohoB = New URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                    End If
                    ' �O���l�{�������p�����[�^
                    m_cFrnHommyoKensakuType = cuKanriJohoB.GetFrn_HommyoKensaku_Param
                End If

                ''*����ԍ� 000034 2007/08/31 �ǉ��J�n
                '' �t�q�Ǘ����a�N���X�̃C���X�^���X��
                'If (cuKanriJohoB Is Nothing) Then
                '    cuKanriJohoB = New URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                'End If
                ''*����ԍ� 000034 2007/08/31 �ǉ��I��
                '*����ԍ� 000039 2009/05/12 �C���I��

                '**
                '* �Ǘ����̎擾
                '*
                ' �����Ǘ����c�`�r�W�l�X�N���X�̃C���X�^���X�쐬
                cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                ' �����Ǘ���񒊏o�i�S���j���\�b�h���s
                m_csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu()

                For Each csAtenaKanriRow In m_csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows
                    '��ʃL�[
                    Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHUKEY).ToString
                        Case "01"   '�ٓ�����
                            '���ʃL�[
                            Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                Case "06"   '�s���揉����
                                    m_strGyosekuInit = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                Case "07"   '�n��P
                                    m_strChiku1Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                Case "08"   '�n��Q
                                    m_strChiku2Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                Case "09"   '�n��R
                                    m_strChiku3Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                Case "10"   '�����P������
                                    m_strZokugara1Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                Case "11"   '�����Q������
                                    m_strZokugara2Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                            End Select
                        Case "04"   '�f�[�^�A������
                            '���ʃL�[
                            Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                Case "01"   '�������v���J�A�g���[�N�t���[
                                    m_strR3RenkeiFG = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                Case "12"   '�Œ�A��
                                    m_strKoteiRenkeiFG = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                            End Select
                        Case "05"   '�����֘A
                            '���ʃL�[
                            Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                Case "01"   '������
                                    m_strGapeiDate = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
                                    '*����ԍ� 000027 2005/12/20 �ǉ��J�n
                                    ' �������̈���O���擾
                                    If m_strGapeiDate <> String.Empty Then
                                        m_cfDateClass.p_strDateValue = m_strGapeiDate
                                        m_strBefGapeiDate = m_cfDateClass.AddDay(-1)
                                    End If
                                    ' �V�X�e�����t���擾����
                                    m_strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")
                                    '*����ԍ� 000027 2005/12/20 �ǉ��I��
                            End Select
                            '*����ԍ� 000021 2005/12/12 �ǉ��J�n
                        Case "10"   '�Ǝ�����
                            Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                Case "03"   '�]�o�ҍs����b�c
                                    m_strTenshutsuGyoseikuCD = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String)
                                    '*����ԍ� 000022 2005/12/15 �폜�J�n
                                    'If m_strTenshutsuGyoseikuCD.Trim <> String.Empty Then
                                    '    ' �s����R�[�h�}�X�^�L���b�V���a�N���X�̃C���X�^���X�쐬
                                    '    m_cuGyoseikuCDCashB = New URGYOSEIKUCDMSTCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                                    '    ' �L���b�V���̓��e���ŐV���`�F�b�N
                                    '    m_cuGyoseikuCDCashB.NewestCacheCheck()
                                    '    ' �]�o�җp�̍s����b�c�ōs���於�̂��擾����
                                    '    csGyoseikuCDMstEntity = m_cuGyoseikuCDCashB.GetGYOSEIKUCDMST(m_strTenshutsuGyoseikuCD.PadLeft(9, " "c))
                                    '    m_strTenshutsuGyoseikuMei = CType(csGyoseikuCDMstEntity.Tables(URGYOSEIKUCDMSTData.TABLE_NAME).Rows(0)(URGYOSEIKUCDMSTData.GYOSEIKUMEI), String)
                                    'End If
                                    '*����ԍ� 000022 2005/12/15 �폜�J�n
                            End Select
                            '*����ԍ� 000021 2005/12/12 �ǉ��I��

                        '*����ԍ� 000065 2024/04/02 �ǉ��J�n
                        Case "20"   '�l��񐧌�@�\
                            Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
                                Case "08"   '���N�㌩�l�������b�Z�[�W
                                    m_strSeinenKoKenShokiMsg = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim

                            End Select
                        '*����ԍ� 000065 2024/04/02 �ǉ��I��
                    End Select
                Next csAtenaKanriRow

                ' ���������ݐς̃X�L�[�}�[���擾����B(GetTableSchema���g�����U�N�V�������擾�ł��Ȃ�)
                m_csAtenaRuisekiEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiEntity.TABLE_NAME)

                '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                '�����ݐϕt���e�[�u���̃X�L�[�}��ێ�
                m_csAtenaRuisekiFzyEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiFZYEntity.TABLE_NAME)
                '* ����ԍ� 000044 2011/11/09 �ǉ��I��

                '* corresponds to VS2008 Start 2010/04/16 000043
                '*����ԍ� 000016 2005/11/01 �폜�J�n
                '���ݓ������擾����
                ''''strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")

                ''''�����N�������Ǘ����ɑ��݂��A�����N����������A�������N�����ȑO�̍X�V�̏ꍇ�͍����N�������i�[����
                ''''If Not m_strGapeiDate Is Nothing AndAlso m_strGapeiDate > strSystemDate Then
                ''''    m_blnGappei = True
                ''''End If
                '*����ԍ� 000016 2005/11/01 �폜�I��
                '* corresponds to VS2008 End 2010/04/16 000043

                ' �����W���c�`�N���X�̃C���X�^���X�쐬
                m_cABAtenaHyojunB = New ABAtena_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' �����t���W���c�`�N���X�̃C���X�^���X�쐬
                m_cABAtenaFZYHyojunB = New ABAtenaFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' ��������W���c�`�N���X�̃C���X�^���X�쐬
                m_cABAtenaRirekiHyojunB = New ABAtenaRireki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' ��������t���W���c�`�N���X�̃C���X�^���X�쐬
                m_cABAtenaRirekiFZYHyojunB = New ABAtenaRirekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' �����ݐϕW���c�`�N���X�̃C���X�^���X�쐬
                m_cABAtenaRuisekiHyojunB = New ABAtenaRuiseki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' �����ݐϕt���W���c�`�N���X�̃C���X�^���X�쐬
                m_cABatenaRuisekiFZYHyojunB = New ABAtenaRuisekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '�ގ��N���X
                m_cuUsRuiji = New USRuijiClass
                '�Ԓn�R�[�h�ҏW�a�N���X
                m_cABBanchiEdabanSuchiB = New ABBanchiEdabanSuchiBClass(m_cfControlData, m_cfConfigDataClass)
                '�����ݐϕW���e�[�u���̃X�L�[�}��ێ�
                m_csAtenaRuisekiHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiHyojunEntity.TABLE_NAME)
                '�����ݐϕt���W���e�[�u���̃X�L�[�}��ێ�
                m_csAtenaRuisekiFZYHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME)
                '���ʔԍ��W��
                m_csABMyNumberHyojunB = New ABMyNumberHyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '���ʔԍ��ݐϕW��
                m_csAbMyNumberRuisekiHyojunB = New ABMyNumberRuisekiHyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '*����ԍ� 000065 2024/04/02 �ǉ��J�n
                '�����l��񐧌�a
                m_cABKojinSeigyoB = New ABKojinSeigyoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '�����l��񐧌䗚���a
                m_cABKojinseigyoRirekiB = New ABKojinseigyoRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '*����ԍ� 000065 2024/04/02 �ǉ��I��
            End If
            ''''''''' �����Ǘ����c�`�r�W�l�X�N���X�̃C���X�^���X�쐬
            ''''''''cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ''''''''' �����Ǘ���񒊏o�i�S���j���\�b�h���s
            ''''''''csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu()

            ''''''''For Each csAtenaKanriRow In csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows
            ''''''''    '��ʃL�[
            ''''''''    Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHUKEY).ToString
            ''''''''        Case "01"   '�ٓ�����
            ''''''''            '���ʃL�[
            ''''''''            Select Case csAtenaKanriRow(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).ToString
            ''''''''                Case "06"   '�s���揉����
            ''''''''                    m_strGyosekuInit = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
            ''''''''                Case "07"   '�n��P
            ''''''''                    m_strChiku1Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
            ''''''''                Case "08"   '�n��Q
            ''''''''                    m_strChiku2Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
            ''''''''                Case "09"   '�n��R
            ''''''''                    m_strChiku3Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
            ''''''''                Case "10"   '�����P������
            ''''''''                    m_strZokugara1Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
            ''''''''                Case "11"   '�����Q������
            ''''''''                    m_strZokugara2Init = CType(csAtenaKanriRow(ABAtenaKanriJohoEntity.PARAMETER), String).Trim
            ''''''''            End Select
            ''''''''    End Select
            ''''''''Next csAtenaKanriRow

            ''''''' ���������ݐς̃X�L�[�}�[���擾����B(GetTableSchema���g�����U�N�V�������擾�ł��Ȃ�)
            ''''''m_csAtenaRuisekiEntity = m_cfRdbClass.GetTableSchema(ABAtenaRuisekiEntity.TABLE_NAME)
            ' ���������ݐσ}�X�^���擾����(��L�̑�֑΍�)
            'm_csAtenaRuisekiEntity = m_cAtenaRuisekiB.GetAtenaRuiseki("000000000000", "1")
            '*����ԍ� 000009 2005/03/18 �C���I��

            '*����ԍ� 000004 2004/02/13 �ǉ��J�n   000009 2005/02/28 �폜�J�n
            ''''''''m_ABToshoProperty�̃J�E���^�̏����l��"0"�ɐݒ�
            '''''''m_intCnt = 0
            ''''''''m_ABToshoProperty�̔z�񐔂��`
            '''''''ReDim m_ABToshoProperty(csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows.Count - 1)
            '*����ԍ� 000004 2004/02/13 �ǉ��I��   000009 2005/02/28 �폜�I��

            '*����ԍ� 000006 2004/08/27 �C���J�n
            '''''''csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("05", "01")
            '''''''strSystemDate = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")
            ''''''''�����N�������Ǘ����ɑ��݂��A�����N����������A�������N�����ȑO�̍X�V�̏ꍇ�͍����N�������i�[����
            '''''''If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) AndAlso _
            '''''''   CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) > strSystemDate Then
            '''''''    blnGappei = True
            '''''''End If

            '*����ԍ� 000016 2005/11/01 �C���J�n
            '* �R�����g***********************************************************************
            '* �y�ǂ��������ԁE�P�\���ԁE�ʏ���ԁz��y�ٓ����R�z�ɂ���Ĕ��f����̂ł͂Ȃ��A*
            '* �y���߂݂̘̂A�g�z���y����S���̘A�g�z�Ȃ̂������𔻒f���Ĉ������ɔ��f����B  *
            '*********************************************************************************
            '* corresponds to VS2008 Start 2010/04/16 000043
            ''''If m_blnGappei Then
            ''''    strsvjumincd = String.Empty
            ''''    For Each csJukiDataRow In csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows
            ''''        If CType(csJukiDataRow(ABJukiData.JUMINCD), String) <> strsvjumincd And _
            ''''           (Not ((CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "03") Or _
            ''''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "04") Or _
            ''''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "08") Or _
            ''''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "45") Or _
            ''''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "63"))) And _
            ''''             (CType(csJukiDataRow(ABJukiData.RIREKINO), Integer) = 1) Then
            ''''            strsvjumincd = CType(csJukiDataRow(ABJukiData.JUMINCD), String)
            ''''            cSearchKey = New ABAtenaSearchKey()
            ''''            cSearchKey.p_strJuminCD = strsvjumincd
            ''''            cSearchKey.p_strJuminYuseniKB = "1"
            ''''            'cSearchKey.p_strStaiCD = CType(csJukiDataRow(ABJukiData.STAICD), String)
            ''''            csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)
            ''''            If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then
            ''''                For Each csDataRow In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows
            ''''                    m_cAtenaRirekiB.DeleteAtenaRB(csDataRow, "D")
            ''''                Next csDataRow
            ''''            End If
            ''''        End If
            ''''    Next csJukiDataRow
            ''''End If
            ''''' �f�[�^���J��Ԃ�
            ''''For Each csJukiDataRow In csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows
            ''''    'If Not (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "08") And _
            ''''    If CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then

            ''''        Me.JukiDataKoshin01(csJukiDataRow)

            ''''    ElseIf m_blnGappei And _
            ''''    Not ((CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "08") Or _
            ''''           (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "03") Or _
            ''''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "04") Or _
            ''''             (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "45") Or _
            ''''           (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "63")) Then

            ''''        Me.JukiDataKoshin08(csJukiDataRow)

            ''''    End If
            ''''Next csJukiDataRow
            '* corresponds to VS2008 End 2010/04/16 000043

            '---------------------------------------------------------------------------------------
            ' 2. �Z��f�[�^���Z���R�[�h�A����ԍ��̏����ɕ��ёւ���
            '---------------------------------------------------------------------------------------

            '*����ԍ� 000019 2005/12/02 �ǉ��J�n
            csJukiDataRows = csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Select("", ABJukiData.JUMINCD + " ASC , " + ABJukiData.RIREKINO + " ASC")
            '*����ԍ� 000019 2005/12/02 �ǉ��I��

            '*����ԍ� 000038 2009/04/07 �폜�J�n
            ''*����ԍ� 000031 2007/01/30 �ǉ��J�n
            '' UR�Ԓn�R�[�h�}�X�^�N���X�̃C���X�^���X����
            ''*����ԍ� 000035 2007/09/05 �C���J�n
            'If (m_crBanchiCdMstB Is Nothing) Then
            '    m_crBanchiCdMstB = New URBANCHICDMSTBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            'End If
            ''m_crBanchiCdMstB = New URBANCHICDMSTBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            ''*����ԍ� 000035 2007/09/05 �C���I��
            ''*����ԍ� 000031 2007/01/30 �ǉ��I��
            '*����ԍ� 000038 2009/04/07 �폜�I��

            '*����ԍ� 000038 2009/04/07 �ǉ��J�n
            ' �Ԓn�R�[�h�ҏW�N���X�̃C���X�^���X����
            If (m_cBanchiCDHenshuB Is Nothing) Then
                m_cBanchiCDHenshuB = New ABBanchiCDHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            End If
            '*����ԍ� 000038 2009/04/07 �ǉ��I��

            '---------------------------------------------------------------------------------------
            ' 3. �Z��f�[�^�������Ȃ�܂ōX�V����
            '---------------------------------------------------------------------------------------

            '*����ԍ� 000017 2005/11/22 �C���J�n
            '*����ԍ� 000019 2005/12/02 �C���J�n
            '* corresponds to VS2008 Start 2010/04/16 000043
            ''''For Each csJukiDataRow In csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows
            '* corresponds to VS2008 End 2010/04/16 000043
            For Each csJukiDataRow In csJukiDataRows
                '*����ԍ� 000019 2005/12/02 �C���I��

                strBreakJuminCD(0) = strBreakJuminCD(1)
                strBreakJuminCD(1) = CType(csJukiDataRow(ABJukiData.JUMINCD), String)

                ' �Z���R�[�h���u���C�N������e�퍀�ڂ�����������
                If strBreakJuminCD(0) <> strBreakJuminCD(1) Then
                    m_intRenbanCnt = 0
                    m_intJutogaiInCnt = 0
                    m_intJutogaiRowCnt = 0
                    m_blnHenkanFG = False
                    '*����ԍ� 000018 2005/11/27 �폜�J�n
                    'm_blnSaiTenyuFG = False
                    '*����ԍ� 000018 2005/11/27 �폜�I��
                    '*����ԍ� 000041 2009/06/18 �ǉ��J�n
                    m_blnRirekiShusei = False
                    '*����ԍ� 000041 2009/06/18 �ǉ��I��
                    '*����ԍ� 000042 2009/08/10 �C���J�n
                    m_csReRirekiEntity = Nothing
                    '*����ԍ� 000042 2009/08/10 �C���I��
                    m_csReRirekiHyojunEntity = Nothing
                    m_csRERirekiFZYHyojunEntity = Nothing
                End If

                ' ���߂̃f�[�^�������f�[�^�Ȃ̂��𔻒�
                ' �����I���N�������I�[���X�̏ꍇ�́A���߃f�[�^�̏ꍇ�ł���
                If CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then

                    '---------------------------------------------------------------------------------------
                    ' 3-1. ���߃��R�[�h��ҏW���X�V����
                    '---------------------------------------------------------------------------------------

                    ' �Z��X�V���\�b�h���Ă�
                    Me.JukiDataKoshin01(csJukiDataRow)

                Else
                    '* ����ԍ�000062 2023/12/07 �폜�J�n
                    ''---------------------------------------------------------------------------------------
                    '' 3-2-1. �c�a����Ώۃf�[�^�̑S������ޔ����A�c�a���폜����
                    ''---------------------------------------------------------------------------------------

                    '' �Z���R�[�h���u���C�N������DB���̊Y�����R�[�h��S���폜����
                    'If strBreakJuminCD(0) <> strBreakJuminCD(1) Then
                    '    ' ����S���f�[�^�̎�
                    '    ' ���������L�[�̃C���X�^���X��
                    '    cSearchKey = New ABAtenaSearchKey
                    '    ' �����L�[�ɏZ���R�[�h��ݒ肷��
                    '    cSearchKey.p_strJuminCD = CType(csJukiDataRow(ABJukiData.JUMINCD), String)

                    '    ' �Y���̗����f�[�^���擾����(�Z��E�Z�o�O�S��)
                    '    csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)

                    '    '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                    '    '����t�����擾
                    '    csAtenaRirekiFzyEntity = m_cAtenaRirekiFzyB.GetAtenaFZYRBHoshu(cSearchKey.p_strJuminCD, String.Empty, String.Empty, True)
                    '    '* ����ԍ� 000044 2011/11/09 �ǉ��I��

                    '    ' �S�����f�[�^�̌������擾
                    '    intAllCnt = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

                    '    ' �S�����f�[�^��ޔ�����
                    '    m_csReRirekiEntity = csAtenaRirekiEntity

                    '    '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                    '    '����t���̑ޔ�������
                    '    m_csReRirekiFzyEntity = csAtenaRirekiFzyEntity
                    '    '* ����ԍ� 000044 2011/11/09 �ǉ��I��

                    '    ' �ޔ����������d����������������Z�o�O�̂q�n�v���������o��
                    '    m_csJutogaiRows = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUMINJUTOGAIKB ='2'", ABAtenaRirekiEntity.RIREKINO)
                    '    m_intJutogaiRowCnt = m_csJutogaiRows.Length

                    '    ' �Z�o�O���R�[�h�����݂���ꍇ�̓t���O�𗧂Ă�B
                    '    If m_intJutogaiRowCnt >= 1 Then
                    '        ' �Z�o�O����t���O���s������
                    '        m_blnJutogaiAriFG = True

                    '        '*����ԍ� 000027 2005/12/20 �ǉ��J�n
                    '        ' �����ǂ��������Ԓ��ŏZ��f�[�^�̒��߂��Z���ł���ꍇ�A�ޔ������Z�o�O���E��ҏW����B
                    '        If m_strGapeiDate <> String.Empty AndAlso m_strSystemDate < m_strGapeiDate Then
                    '            ' �����ǂ��������Ԃł���
                    '            ' �Z��f�[�^�ΏۏZ���b�c�̒��߃��R�[�h���擾����
                    '            csJukiCkinDataRows = csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Select("JUMINCD = '" + CType(csJukiDataRow(ABJukiData.JUMINCD), String) + "' AND RRKED_YMD = '99999999'")
                    '            ' �Z��f�[�^���߂��Z���̏ꍇ
                    '            If CType(csJukiCkinDataRows(0)(ABJukiData.JUMINSHU), String).RPadLeft(2, " "c).RRemove(0, 1) = "0" Then
                    '                ' �Z��f�[�^���߃��R�[�h�̊J�n�N�������擾����
                    '                strJukiCkinST_YMD = CType(csJukiCkinDataRows(0)(ABJukiData.RRKST_YMD), String)
                    '                ' �Z�o�O���E��ҏW����
                    '                m_csJutogaiRows = EditJutogaiRows(m_csJutogaiRows, strJukiCkinST_YMD)
                    '                ' ���߂ďZ�o�O���E�̌������擾����
                    '                m_intJutogaiRowCnt = m_csJutogaiRows.Length
                    '            End If
                    '        End If
                    '        '*����ԍ� 000027 2005/12/20 �ǉ��I��

                    '        ' �ŏ��̏Z�o�O�q�n�v���擾����
                    '        m_csFirstJutogaiRow = m_csJutogaiRows(0)

                    '        ' �����J�n�N�������擾����
                    '        m_intJutogaiST_YMD = CType(m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                    '    Else
                    '        m_blnJutogaiAriFG = False
                    '    End If

                    '    ' �Y���̗����f�[�^��S���폜����
                    '    intDelCnt = m_cAtenaRirekiB.DeleteAtenaRB(CType(csJukiDataRow(ABJukiData.JUMINCD), String))

                    '    '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                    '    '* ����ԍ� 000062 2023/12/07 �폜�J�n
                    '    ''����t���̍폜
                    '    'Me.m_cAtenaRirekiFzyB.DeleteAtenaFZYRB(csJukiDataRow(ABJukiData.JUMINCD).ToString)
                    '    ''* ����ԍ� 000044 2011/11/09 �ǉ��I��

                    '    '' �S�����f�[�^�̌����ƍ폜������������v���Ȃ��ꍇ�̓G���[
                    '    'If intAllCnt <> intDelCnt Then
                    '    '    ' �G���[��`���擾�i�Y���f�[�^�͑��ōX�V����Ă��܂��܂����B�ēx����F���������j
                    '    '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '    '    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                    '    '    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    '    'End If

                    '    ''*����ԍ� 000041 2009/06/18 �ǉ��J�n
                    '    'm_blnRirekiShusei = True
                    '    '* ����ԍ� 000062 2023/12/07 �폜�I��
                    '    '*����ԍ� 000041 2009/06/18 �ǉ��I��

                    'End If
                    '*����ԍ�000062 2023/12/07 �폜�I��

                    '---------------------------------------------------------------------------------------
                    ' 3-2-2. �������R�[�h��ҏW���X�V����
                    '---------------------------------------------------------------------------------------

                    ' �����f�[�^���ăZ�b�g����
                    Me.JukiDataKoshin08N(csJukiDataRow)
                    '*����ԍ� 000017 2005/11/22 �C���I��
                End If

            Next

            '*����ԍ� 000016 2005/11/01 �C���I��

            '*����ԍ� 000004 2004/02/13 �ǉ��J�n   000009 2005/02/28 �폜�J�n
            '**
            '* ���[�N�t���[����
            '*
            '�J�E���g��"0"�̎��̓��[�N�t���[�������s��Ȃ�
            ''''''''If Not (m_intCnt = 0) Then
            ''''''''    '  �����Ǘ����̎��04���ʃL�[01�̃f�[�^��S���擾����
            ''''''''    csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "01")

            ''''''''    '�Ǘ����̃��[�N�t���[���R�[�h�����݂��A�p�����[�^��"1"�̎��������[�N�t���[�������s��
            ''''''''    If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
            ''''''''        If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then

            ''''''''            'm_ABToshoProperty�̔z�񐔂��Ē�`
            ''''''''            ReDim Preserve m_ABToshoProperty(m_intCnt - 1)
            ''''''''            '�f�[�^�Z�b�g�擾�N���X�̃C���X�^���X��
            ''''''''            cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            ''''''''            '���[�N�t���[���M�����Ăяo��
            ''''''''            cABAtenaCnvBClass.AtenaCnv(m_ABToshoProperty, WORK_FLOW_NAME, DATA_NAME)

            ''''''''        End If
            ''''''''    End If
            ''''''''End If
            '*����ԍ� 000004 2004/02/13 �ǉ��I��   000009 2005/02/28 �폜�I��


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
            Throw objExp
        End Try

    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �Z��f�[�^�X�V�i�ʏ�j
    '* 
    '* �\��           Public Sub JukiDataKoshin1(ByVal csJukiDataRow As DataRow) 
    '* 
    '* �@�\ �@    �@�@�Z��f�[�^���X�V����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    <SecuritySafeCritical>
    Private Sub JukiDataKoshin01(ByVal csJukiDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "JukiDataKoshin01"
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        Dim blnJutogaiUmu As Boolean                        ' �Z�o�O�L��FLG
        Dim blnJukiUmu As Boolean                           ' �Z��L��FLG
        Dim strJuminCD As String                            ' �Z���R�[�h
        Dim csJutogaiEntity As DataSet                      ' �Z�o�ODataSet
        Dim cSearchKey As ABAtenaSearchKey                  ' ���������L�[
        Dim csAtenaEntity As DataSet                        ' �����}�X�^Entity
        Dim csAtenaRow As DataRow                           ' �����}�X�^Row
        Dim csDataRow As DataRow                            ' �c�������q����
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csDataSet As DataSet                            ' �c�������r����
        '* corresponds to VS2008 End 2010/04/16 000043
        Dim csDataColumn As DataColumn                      ' �c�������b����������
        Dim csAtenaRirekiEntity As DataSet                  ' ��������DataSet
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csAtenaRirekiRows() As DataRow                  ' ��������Rows
        '* corresponds to VS2008 End 2010/04/16 000043
        Dim csAtenaRirekiRow As DataRow                     ' ��������Row
        Dim intCount As Integer                             ' �X�V����
        Dim csAtenaRuisekiEntity As DataSet                 ' �����ݐ�DataSet
        Dim csAtenaRuisekiRow As DataRow                    ' �����ݐ�Row
        '*����ԍ� 000003 2003/11/21 �ǉ��J�n
        Dim csAtenaNenkinEntity As DataSet                  ' �����N��DataSet
        Dim csAtenaKokuhoEntity As DataSet                  ' ��������DataSet
        '*����ԍ� 000003 2003/11/21 �ǉ��I��
        Dim StrShoriNichiji As String
        ''*����ԍ� 000004 2004/02/13 �ǉ��J�n  000009 2005/03/18 �폜�J�n
        '''''Dim cABToshoProperty As ABToshoProperty
        '''''Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '�����Ǘ����c�`�r�W�l�X�N���X
        '''''Dim csAtenaKanriEntity As DataSet                   '�����Ǘ����f�[�^�Z�b�g
        '*����ԍ� 000004 2004/02/13 �ǉ��I��  000009 2005/03/18 �폜�J�n
        '*����ԍ� 000005 2004/03/08 �ǉ��J�n   000009 2005/03/18 �폜
        '''''''Dim cBAAtenaLinkageBClass As BAAtenaLinkageBClass   ' �Œ莑�Y�ň����N���X
        '''''''Dim cBAAtenaLinkageIFXClass As BAAtenaLinkageIFXClass
        Dim BlnRcd As Boolean
        '*����ԍ� 000005 2004/03/08 �ǉ��I��
        '*����ԍ� 000013 2005/06/19 �ǉ��J�n
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csRirekiNoEntity As DataSet         '����ԍ��f�[�^�Z�b�g
        '* corresponds to VS2008 End 2010/04/16 000043
        Dim strMaxRirekino As String            '�ő嗚��ԍ�
        Dim blnTokushuFG As Boolean             '���ꏈ���t���O
        '*����ԍ� 000016 2005/11/01 �폜�J�n
        '* corresponds to VS2008 Start 2010/04/16 000043
        ''''Dim csSortRirekiDataRow() As DataRow      '����ԍ��f�[�^���E
        '* corresponds to VS2008 End 2010/04/16 000043
        '*����ԍ� 000016 2005/11/01 �폜�I��
        '*����ԍ� 000013 2005/06/19 �ǉ��I��
        '*����ԍ� 000016 2005/11/01 �ǉ��J�n
        Dim csUpRirekiRows() As DataRow           ' �S�������Z���N�g�������R�[�h�Q���i�[����
        Dim csUpRirekiRow As DataRow              ' ���ꏈ���C�����̏C���ς݂̍X�V���R�[�h
        Dim intIdx As Integer                     ' For���Ŏg�p����C���f�b�N�X
        Dim intJukiInCnt As Integer = 0           ' �Z��f�[�^���C���T�[�g��������
        '*����ԍ� 000016 2005/11/01 �ǉ��I��
        '*����ԍ� 000017 2005/11/22 �ǉ��J�n
        Dim intForCnt As Integer = 0
        '*����ԍ� 000017 2005/11/22 �ǉ��I��
        '*����ԍ� 000023 2005/12/16 �ǉ��J�n
        Dim csRirekiNORows() As DataRow
        Dim intMaxRirekiNO As Integer
        '*����ԍ� 000023 2005/12/16 �ǉ��I��
        '*����ԍ� 000031 2007/01/30 �ǉ��J�n
        Dim strBanchiCD() As String                         ' �Ԓn�R�[�h�擾�p�z��
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim strMotoBanchiCD() As String                     ' �ύX�O�Ԓn�R�[�h
        'Dim intLoop As Integer                              ' ���[�v�J�E���^
        '* corresponds to VS2008 End 2010/04/16 000043
        '*����ԍ� 000031 2007/01/30 �ǉ��I��
        '*����ԍ� 000032 2007/02/15 �ǉ��J�n
        Dim csBeforeRirekiRows As DataRow()                 ' �X�V�O�������R�[�h�擾�pDataRows
        '*����ԍ� 000032 2007/02/15 �ǉ��I��
        '*����ԍ� 000036 2007/09/28 �ǉ��J�n
        Dim cHenshuSearchKana As ABHenshuSearchShimeiBClass ' �����p�J�i�����N���X
        Dim strSearchKana(4) As String                      ' �����p�J�i���̗p
        '*����ԍ� 000036 2007/09/28 �ǉ��I��
        '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
        Dim csSelectedRows As DataRow()                     '�������ʔz��
        Dim csCkinRirekiFzyRows As DataRow                  '���߈�������t���s
        Dim csAtenaFzyEntity As DataSet                     '�����t��
        Dim csAtenaFzyRow As DataRow                        '�����t���s
        Dim csAtenaRirekiFzyEntity As DataSet               '��������t��
        Dim csAtenaRirekiFzyRow As DataRow                  '��������t���s
        Dim csAtenaRirekiFzyTokushuRow As DataRow           '��������t������s
        Dim csAtenaRuisekiFzyEntity As DataSet              '�����ݐϕt��
        Dim csAtenaRuisekiFzyRow As DataRow                 '�����ݐϕt���s
        Dim cSekoYMDHanteiB As ABSekoYMDHanteiBClass        '�{�s������B
        Dim blnAfterSekobi As Boolean = False               '�{�s���ȍ~���ǂ���
        '* ����ԍ� 000044 2011/11/09 �ǉ��I��
        '* ����ԍ� 000050 2014/06/25 �ǉ��J�n
        Dim a_strMyNumber() As String                       ' ���ʔԍ��E�����ʔԍ������p
        Dim cABMyNumberPrm As ABMyNumberPrmXClass           ' ���ʔԍ��p�����[�^�[�N���X
        '* ����ԍ� 000050 2014/06/25 �ǉ��I��
        '* ����ԍ� 000058 2015/10/14 �ǉ��J�n
        Dim crBangoSekoYMDHanteiB As URSekoYMDHanteiBClass  ' ���ʔԍ��{�s������N���X
        Dim strBangoSekoYMD As String                       ' ���ʔԍ��{�s��
        Dim blnIsCreateAtenaRireki As Boolean               ' �����������쐬���邩�ǂ����i����C���̏ꍇ�ɓ���Ƃ��āj
        '* ����ԍ� 000058 2015/10/14 �ǉ��I��
        Dim csAtenaHyojunEntity As DataSet                  '�����W��
        Dim csAtenaHyojunRow As DataRow                     '�����W��Row
        Dim csAtenaFzyHyojunEntity As DataSet               '�����t���W��
        Dim csAtenaFzyHyojunRow As DataRow                  '�����t���W��Row
        Dim csAtenaRirekiHyojunEntity As DataSet            '��������W��
        Dim csAtenaRirekiHyojunRow As DataRow               '��������W��Row
        Dim csAtenaRirekiFZYHyojunEntity As DataSet         '��������t���W��
        Dim csAtenaRirekiFZYHyojunRow As DataRow            '��������t���W��Row
        Dim csAtenaRuisekiHyojunEntity As DataSet           '�����ݐϕW��
        Dim csAtenaRuisekiHyojunRow As DataRow              '�����ݐϕW��Row
        Dim csAtenaRuisekiFZYHyojunEntity As DataSet        '�����ݐϕt���W��
        Dim csAtenaRuisekiFZYHyojunRow As DataRow           '�����ݐϕt���W��Row
        Dim csAtenaRirekiHyojunTokushuRow As DataRow        '��������W������s
        Dim csAtenaRirekiFzyHyojunTokushuRow As DataRow     '��������t���W������s
        Dim csCkinRirekiHyojunRows As DataRow               '���߈�������W���s
        Dim csCkinRirekiFzyHyojunRows As DataRow            '���߈�������t���W���s

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '*����ԍ� 000016 2005/11/18 �ǉ��J�n
            ' �g�p����Ƃ��ɂ��������Z�b�g���Ă��̂ōŏ��ɍs���B(���܂œ_�݂��Ă������͍폜)
            ' ���t�N���X�̕K�v�Ȑݒ���s��
            m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            m_cfDateClass.p_enEraType = UFEraType.Number
            '*����ԍ� 000016 2005/11/18 �ǉ��I��

            '*����ԍ� 000036 2007/09/28 �ǉ��J�n
            ' �����p�J�i�����N���X�C���X�^���X��
            cHenshuSearchKana = New ABHenshuSearchShimeiBClass(m_cfControlData, m_cfConfigDataClass)
            '*����ԍ� 000036 2007/09/28 �ǉ��I��

            '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
            '�{�s���ȍ~�t���O���擾���Ă���
            cSekoYMDHanteiB = New ABSekoYMDHanteiBClass(Me.m_cfControlData, Me.m_cfConfigDataClass, Me.m_cfRdbClass)
            blnAfterSekobi = cSekoYMDHanteiB.CheckAfterSekoYMD
            '* ����ԍ� 000044 2011/11/09 �ǉ��I��

            '* ����ԍ� 000058 2015/10/14 �ǉ��J�n
            ' �{�t�ԏ����̍ۂɁA�Z��ƈ����ŗ��𐔂��قȂ��Ă���B
            ' �{�t�ԏ����ɂč쐬���ꂽ�Z��݂̂ɑ��݂��闚���ɑ΂��ē���C�������������ꍇ�́A
            ' �����ɊY�����������݂��Ȃ����߁A�������������ޓ����Ƃ���B
            ' �������A�����C�����ŗ��𐔂���v�����ȍ~�̓���C���͍��܂Œʂ�㏑�������ƂȂ�B
            blnIsCreateAtenaRireki = False
            Select Case csJukiDataRow.Item(ABJukiData.SHORIJIYUCD).ToString

                Case ABEnumDefine.ABJukiShoriJiyuType.TokushuShusei.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.TokushuCodeShusei.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00")

                    ' �u03�F����C���v�u04�F�Z���[�R�[�h�C���v�u05�F�l�ԍ��C���v�̏ꍇ

                    ' ���������̒��߃��R�[�h���擾����
                    cSearchKey = New ABAtenaSearchKey
                    cSearchKey.p_strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString
                    csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "", "1", True)

                    If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then

                        csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)

                        ' ��������t���̒��߃��R�[�h���擾����
                        csAtenaRirekiFzyEntity = m_cAtenaRirekiFzyB.GetAtenaFZYRBHoshu(
                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RIREKINO).ToString,
                                                        "1",
                                                        True)

                        If (csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows.Count > 0) Then

                            csAtenaRirekiFzyRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows(0)

                            ' �Z���ɑ΂�����ꏈ�����ǂ������肷��i�{�t�ԏ����ΏۂƂȂ������R�[�h�ɑ΂��鏈�����̔���j
                            ' ���u10�F���{�l�Z���v�u20-0�F�O���l�Z���v���ǂ������肷��
                            If (csAtenaRirekiRow.Item(ABAtenaRirekiEntity.ATENADATASHU).ToString = ABConstClass.JUMINSHU_NIHONJIN_JUMIN _
                                OrElse (csAtenaRirekiRow.Item(ABAtenaRirekiEntity.ATENADATASHU).ToString = ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN _
                                        AndAlso csAtenaRirekiFzyRow.Item(ABAtenaRirekiFZYEntity.JUMINHYOJOTAIKBN).ToString = ABConstClass.JUMINHYOJOTAIKB_TAISHO)) Then

                                ' �ԍ����x�{�s�����擾����
                                crBangoSekoYMDHanteiB = New URSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, ABConstClass.THIS_BUSINESSID)
                                strBangoSekoYMD = crBangoSekoYMDHanteiB.GetBangoSeidoSekoYMD

                                ' �����J�n�����ԍ����x�{�s�������A���쐬�N�������ԍ����x�{�s�������̏ꍇ�A�������R�R�[�h�ɒ��߈ٓ����R�R�[�h��ݒ肷��
                                ' ��L�����𖞂����ꍇ�A�{�t�ԏ����ȍ~�Ɉٓ����������Ă��炸�A���𐔕s��v�̏�ԂƂȂ��Ă��邽�߁A
                                ' �������������݁A���𐔁i���߂̈ٓ���ԁj����v������
                                ' ���ԍ��{�s���ȍ~�ɒʏ�ٓ������������ꍇ�A�����J�n���E�쐬�����Ƃ��ɔԍ��{�s���ȍ~�ƂȂ�i���ߗ�������v���Ă����Ԃ̂��ߏ㏑�������Ƃ���j
                                ' ���ԍ��{�s���ȍ~�ɗ����C�������������ꍇ�A�쐬�����݂̂��ԍ��{�s���ȍ~�ƂȂ�i�����C���ɂĒ��ߗ�������v�A���𐔂���v���Ă����Ԃ̂��ߏ㏑�������Ƃ���j
                                ' ���ԍ��{�s���ȍ~�ɓ���C�����P��ł����������ꍇ�A�����J�n���E�쐬�����Ƃ��ɔԍ��{�s���ȍ~�ƂȂ�i���ꏈ���i���l���j�ɂĒ��ߗ�������v���Ă����Ԃ̂��ߏ㏑�������Ƃ���j
                                ' �����㔭������Ǝv����ڍs�����ŗ������쐬����ꍇ�A�쐬�����݂̂��ԍ��{�s���ȍ~�ƂȂ�i�ڍs���ɗ��𐔂���v����Ă��邱�Ƃ��O��Ƃ��㏑�������Ƃ���j
                                ' ���Z�o���C���i���ЏZ��j�̏ꍇ�A������ɂė��������܂��\�������邪���Ȃ��Ƃ���
                                If (csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKST_YMD).ToString < strBangoSekoYMD _
                                    AndAlso csAtenaRirekiRow.Item(ABAtenaRirekiEntity.SAKUSEINICHIJI).ToString.RPadRight(8).RSubstring(0, 8) < strBangoSekoYMD) Then

                                    ' �����������쐬����i���ꏈ���̏ꍇ�ɓ���Ƃ��āj
                                    blnIsCreateAtenaRireki = True

                                Else
                                    ' noop
                                End If

                            Else
                                ' noop
                            End If

                        Else
                            ' noop
                        End If

                        ' ��������W���̒��߃��R�[�h���擾����
                        csAtenaRirekiHyojunEntity = m_cABAtenaRirekiHyojunB.GetAtenaRirekiHyojunBHoshu(
                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RIREKINO).ToString,
                                                        "1",
                                                        True)

                        If (csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                            csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows(0)
                        End If

                        ' ��������t���W���̒��߃��R�[�h���擾����
                        csAtenaRirekiFZYHyojunEntity = m_cABAtenaRirekiFZYHyojunB.GetAtenaRirekiFZYHyojunBHoshu(
                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                        csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RIREKINO).ToString,
                                                        "1",
                                                        True)

                        If (csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                            csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows(0)
                        End If
                    Else
                        ' noop
                    End If

                Case Else
                    ' noop
            End Select
            '* ����ԍ� 000058 2015/10/14 �ǉ��I��

            '---------------------------------------------------------------------------------------
            ' 1. �ϐ��̏�����
            '
            '---------------------------------------------------------------------------------------
            blnJutogaiUmu = False           '�Z�o�O�f�[�^�����݂��Ă���ꍇ��True
            blnJukiUmu = False              '�Z��f�[�^�����݂��Ă���ꍇ��True
            strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString    '�Ώۃf�[�^�̏Z���R�[�h���擾


            '---------------------------------------------------------------------------------------
            ' 2. �Z�o�O�f�[�^�̑��݃`�F�b�N
            '�@�@�@�@�@���߂̏Z�o�O�f�[�^�����݂��Ă��邩�Z�o�O�}�X�^����擾����B
            '---------------------------------------------------------------------------------------
            ' �Z���R�[�h�ŏZ�o�O�}�X�^���擾����i���݂���ꍇ�́A�Z�o�O�L��e�k�f�Ɂh1�h���Z�b�g�j
            csJutogaiEntity = m_cJutogaiB.GetJutogaiBHoshu(strJuminCD, True)
            If (csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows.Count > 0) Then
                blnJutogaiUmu = True
            End If


            '---------------------------------------------------------------------------------------
            ' 3. �ē]���̏���
            '�@�@�@�@�@���߂̏Z�o�O�f�[�^�����݂��Ă���ꍇ�͍폜����B
            '---------------------------------------------------------------------------------------
            ' �Z����ʂ̉��P�����h0�h�i�Z���j�ł��Z�o�O�L��e�k�f���h1�h�̎�
            ' �E�Z�o�O�f�[�^���폜����
            ' �E�Z�o�O�D��Ŏw��N�����h99999999�h�ň����}�X�^���擾���A���̃f�[�^���폜����
            If (((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) = "0") _
                    And blnJutogaiUmu) Then
                For Each csDataRow In csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows
                    m_cJutogaiB.DeleteJutogaiB(csDataRow, "D")
                Next csDataRow
                cSearchKey = New ABAtenaSearchKey
                cSearchKey.p_strJuminCD = strJuminCD
                cSearchKey.p_strJutogaiYusenKB = "1"
                csAtenaEntity = m_cAtenaB.GetAtenaBHoshu(1, cSearchKey, True)
                For Each csDataRow In csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows
                    m_cAtenaB.DeleteAtenaB(csDataRow, "D")
                    '�����W��
                    csAtenaHyojunEntity = m_cABAtenaHyojunB.GetAtenaHyojunBHoshu(cSearchKey.p_strJuminCD,
                                                                      csDataRow(ABAtenaEntity.JUMINJUTOGAIKB).ToString,
                                                                      True)
                    If (csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                        '�������ʂ����݂�����O�Ԗڃf�[�^��Delete���s���i�P���O�����Ȃ��͂��j
                        m_cABAtenaHyojunB.DeleteAtenaHyojunB(csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows(0), "D")
                    Else
                        '�������Ȃ�
                    End If

                    '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                    '�����t���f�[�^�擾
                    csAtenaFzyEntity = m_cAtenaFzyB.GetAtenaFZYBHoshu(cSearchKey.p_strJuminCD,
                                                                     csDataRow(ABAtenaEntity.JUMINJUTOGAIKB).ToString,
                                                                     True)
                    If (csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0) Then
                        '�������ʂ����݂�����O�Ԗڃf�[�^��Delete���s���i�P���O�����Ȃ��͂��j
                        m_cAtenaFzyB.DeleteAtenaFZYB(csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows(0), "D")
                    Else
                        '�������Ȃ�
                    End If
                    '* ����ԍ� 000044 2011/11/09 �ǉ��I��

                    '�����t���W��
                    csAtenaFzyHyojunEntity = m_cABAtenaFZYHyojunB.GetAtenaFZYHyojunBHoshu(cSearchKey.p_strJuminCD,
                                                                      csDataRow(ABAtenaEntity.JUMINJUTOGAIKB).ToString,
                                                                      True)
                    If (csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                        '�������ʂ����݂�����O�Ԗڃf�[�^��Delete���s���i�P���O�����Ȃ��͂��j
                        m_cABAtenaFZYHyojunB.DeleteAtenaFZYHyojunB(csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows(0), "D")
                    Else
                        '�������Ȃ�
                    End If
                Next csDataRow
            End If


            '---------------------------------------------------------------------------------------
            ' 4. �Z��f�[�^�̑��݃`�F�b�N
            '�@�@�@�@�@���߂̏Z��f�[�^�����݂��Ă��邩�����}�X�^����擾����B
            '---------------------------------------------------------------------------------------
            ' �Z��D��ň����}�X�^���擾����i���݂���ꍇ�́A�Z��L��e�k�f�Ɂh1�h���Z�b�g�j
            ' ���������L�[�̃C���X�^���X��
            cSearchKey = New ABAtenaSearchKey
            cSearchKey.p_strJuminCD = strJuminCD
            cSearchKey.p_strJuminYuseniKB = "1"
            csAtenaEntity = m_cAtenaB.GetAtenaBHoshu(1, cSearchKey, True)
            If (csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count > 0) Then
                blnJukiUmu = True
                '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                '�����t�������Z���R�[�h�w��Ŏ擾
                csAtenaFzyEntity = m_cAtenaFzyB.GetAtenaFZYBHoshu(strJuminCD,
                                                                  csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME) _
                                                                    .Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB).ToString,
                                                                    True)
                '�����W��
                csAtenaHyojunEntity = m_cABAtenaHyojunB.GetAtenaHyojunBHoshu(strJuminCD,
                                      csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB).ToString, True)
                '�����t���W��
                csAtenaFzyHyojunEntity = m_cABAtenaFZYHyojunB.GetAtenaFZYHyojunBHoshu(strJuminCD,
                                     csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB).ToString, True)
            Else
                '�����łȂ��Ƃ��A�Z���Z�o�O�敪����Ō���
                csAtenaFzyEntity = m_cAtenaFzyB.GetAtenaFZYBHoshu(strJuminCD, String.Empty)
                '* ����ԍ� 000044 2011/11/09 �ǉ��I��
                '�����W��
                csAtenaHyojunEntity = m_cABAtenaHyojunB.GetAtenaHyojunBHoshu(strJuminCD, String.Empty, False)
                '�����t���W��
                csAtenaFzyHyojunEntity = m_cABAtenaFZYHyojunB.GetAtenaFZYHyojunBHoshu(strJuminCD, String.Empty, False)
            End If


            '---------------------------------------------------------------------------------------
            ' 5. �f�[�^�̕ҏW
            '�@�@�@�@�@���߂̏Z��f�[�^�����݂��Ă���ꍇ�͏C���A���Ă��Ȃ���Βǉ��ƂȂ�B
            '�@�@�@�@�@ 
            '---------------------------------------------------------------------------------------
            ' �����}�X�^
            ' �����}�X�^�̗���擾���A����������B�i�X�V�J�E�^�[�́A0�A����ȊO�́AString Empty�j�i���ʁj
            If (blnJukiUmu) Then
                csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)
            Else
                csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).NewRow
                Me.ClearAtena(csAtenaRow)
            End If

            ' �Z��f�[�^��舶���}�X�^�̕ҏW���s���i�`�k�k�D�m�t�k�k���́A�`�k�k�X�y�[�X�̎��́AString.Empty�ɂ��āj
            For Each csDataColumn In csJukiDataRow.Table.Columns
                If (IsDBNull(csJukiDataRow(csDataColumn))) _
                        OrElse (CType(csJukiDataRow(csDataColumn), String).Trim = String.Empty) Then
                    csJukiDataRow(csDataColumn) = String.Empty
                End If
            Next csDataColumn

            ' �Z��f�[�^�̓��ꍀ�ڂ������}�X�^�̍��ڂɃZ�b�g����
            ' �E�Z���R�[�h
            csAtenaRow(ABAtenaEntity.JUMINCD) = csJukiDataRow(ABJukiData.JUMINCD)
            ' �E�s�����R�[�h
            csAtenaRow(ABAtenaEntity.SHICHOSONCD) = csJukiDataRow(ABJukiData.SHICHOSONCD)
            ' �E���s�����R�[�h
            csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD) = csJukiDataRow(ABJukiData.KYUSHICHOSONCD)

            ' �����Z�b�g���Ȃ�����
            ' �E�Z���[�R�[�h
            ' �E�ėp�敪�Q
            ' �E�����@�l�`��
            ' �E�����@�l��\�Ҏ���
            ' �E�Ɖ��~�敪
            ' �E���l�Ŗ�

            ' �ҏW���ăZ�b�g���鍀��
            ' �E�Z���Z�o�O�敪   1
            csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB) = "1"
            ' �E�Z���D��敪     1
            csAtenaRow(ABAtenaEntity.JUMINYUSENIKB) = "1"
            ' �E�Z�o�O�D��敪
            ' �@�@�Z����ʂ̉��P�����h0�h�i�Z���j�łȂ��A���Z�o�O�L��e�k�f���h1�h�̎��A�@0
            If (((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) <> "0") _
                    And blnJutogaiUmu) Then
                csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "0"
            Else
                '   �@��L�ȊO       1
                csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1"
            End If
            ' �E�����f�[�^�敪=(11)
            csAtenaRow(ABAtenaEntity.ATENADATAKB) = "11"
            ' �E���уR�[�h�`�����ԍ�
            csAtenaRow(ABAtenaEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD)
            'csAtenaRow(ABAtenaEntity.JUMINHYOCD) = String.Empty
            csAtenaRow(ABAtenaEntity.SEIRINO) = csJukiDataRow(ABJukiData.SEIRINO)
            ' �E�����f�[�^���=(�Z�����)
            csAtenaRow(ABAtenaEntity.ATENADATASHU) = csJukiDataRow(ABJukiData.JUMINSHU)
            ' �E�ėp�敪�P=(�ʂ��敪)
            csAtenaRow(ABAtenaEntity.HANYOKB1) = csJukiDataRow(ABJukiData.UTSUSHIKB)
            ' �E�l�@�l�敪=(1)
            csAtenaRow(ABAtenaEntity.KJNHJNKB) = "1"
            ' �E�ėp�敪�Q
            'csAtenaRow(ABAtenaEntity.HANYOKB2) = String.Empty
            '*����ԍ� 000037 2008/05/12 �폜�J�n
            '* corresponds to VS2008 Start 2010/04/16 000043
            '''' �E�Ǔ��ǊO�敪
            '''' �@�@�Z����ʂ̉��P�����h8�h�i�]�o�ҁj�̏ꍇ�A�@�@2
            '* corresponds to VS2008 End 2010/04/16 000043
            ''If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").Substring(1, 1) = "8") Then
            ''    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"
            ''Else
            ''    ' �Z����ʂ̉��P�����h8�h�i�]�o�ҁj�łȂ��ꍇ�A1			
            ''    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1"
            ''End If
            '*����ԍ� 000037 2008/05/12 �폜�I��

            '*����ԍ� 000068 2024/07/05 �ǉ��J�n
            If (CStr(csJukiDataRow(ABJukiData.HONGOKUMEI)).Trim <> String.Empty) AndAlso
               (CStr(csJukiDataRow(ABJukiData.KANJIHEIKIMEI)).Trim <> String.Empty) AndAlso
               (CStr(csJukiDataRow(ABJukiData.KANJITSUSHOMEI)).Trim = String.Empty) Then
                ' �{�������� ���� ���L������ ���� �ʏ̖����󔒂̏ꍇ
                ' �������̂Q�E�J�i���̂Q�ɋ󔒂�ݒ�
                csJukiDataRow(ABJukiData.KANJIMEISHO2) = String.Empty
                csJukiDataRow(ABJukiData.KANAMEISHO2) = String.Empty
            Else
            End If
            '*����ԍ� 000068 2024/07/05 �ǉ��I��

            '*����ԍ� 000036 2007/09/28 �C���J�n
            ' �E�J�i���̂P�`�����p�J�i��
            If ((CStr(csJukiDataRow(ABJukiData.SHIMEIRIYOKB)).Trim = "2") AndAlso
                    (CStr(csJukiDataRow(ABJukiData.KANJIMEISHO2)).Trim <> String.Empty)) Then
                ' �{���D��(�{���ƒʏ̖������O���l���������p�敪��"2")
                csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                csAtenaRow(ABAtenaEntity.KANAMEISHO2) = String.Empty
                csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = String.Empty
                csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = GetSearchMoji(csJukiDataRow(ABJukiData.KANJIMEISHO2).ToString)

                '*����ԍ� 000039 2009/05/12 �C���J�n
                ' �����p�J�i�����A�����p�J�i���A�����p�J�i���𐶐����i�[
                strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)),
                                                               String.Empty, m_cFrnHommyoKensakuType)
                'strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)), _
                '                                               String.Empty, cuKanriJohoB.GetFrn_HommyoKensaku_Param)
                '*����ԍ� 000039 2009/05/12 �C���I��

                ' �ʏ̖��������@�l��\�Ҏ����Ɋi�[
                csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                ' �ėp�敪�Q�Ɏ������p�敪�̃p�����[�^���i�[
                csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB)
                ' �擾���������p�J�i�����A�����p�J�i���A�����p�J�i�����i�[
                csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana(0)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana(1)
                csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana(2)

                '*����ԍ� 000039 2009/05/12 �C���J�n
            ElseIf (m_cFrnHommyoKensakuType = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                'ElseIf (cuKanriJohoB.GetFrn_HommyoKensaku_Param = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                '*����ԍ� 000039 2009/05/12 �C���I��

                ' �ʏ̖��D��(�{���D��̏����ȊO�̏ꍇ)
                csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)

                '*����ԍ� 000039 2009/05/12 �C���J�n
                ' �����p�J�i�����A�����p�J�i���A�����p�J�i���𐶐����i�[
                strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO1)),
                                                               CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)),
                                                               m_cFrnHommyoKensakuType)
                'strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO1)), _
                '                                               CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)), _
                '                                               cuKanriJohoB.GetFrn_HommyoKensaku_Param)
                '*����ԍ� 000039 2009/05/12 �C���I��

                ' �ʏ̖��������@�l��\�Ҏ�������ɂ���
                csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
                ' �ėp�敪�Q�Ɏ������p�敪�̃p�����[�^���i�[
                csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB)
                ' �擾���������p�J�i�����A�����p�J�i���A�����p�J�i�����i�[
                csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana(0)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana(1)
                csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana(2)
            Else
                '�ʏ̖��D��i�������[�U�j
                csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                ' �ʏ̖��������@�l��\�Ҏ�������ɂ���
                csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
                ' �ėp�敪�Q�Ɏ������p�敪�̃p�����[�^���i�[
                csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB)
                csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
                csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
            End If
            '' �E�J�i���̂P�`�����p�J�i��
            'csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
            'csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
            'csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
            'csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
            ''csAtenaRow(ABAtenaEntity.KANJIHJNKEITAI) = String.Empty
            ''csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
            'csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)
            ''*����ԍ� 000034 2007/08/31 �C���J�n
            'If (cuKanriJohoB.GetFrn_HommyoKensaku_Param = FrnHommyoKensakuType.Tsusho_Seishiki) Then
            '    '�O���l�{�������@�\��"2(Tsusho_Seishiki)"�̂Ƃ��p���͑啶���ɂ���
            '    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = CType(csJukiDataRow(ABJukiData.SEARCHKANASEIMEI), String).ToUpper()
            '    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = GetSearchKana(CType(csJukiDataRow(ABJukiData.KANAMEISHO2), String))
            '    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = CType(csJukiDataRow(ABJukiData.SEARCHKANAMEI), String).ToUpper()
            'Else
            '    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
            '    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
            '    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
            'End If
            ''csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
            ''csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
            ''csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
            ''*����ԍ� 000034 2007/08/31 �C���I��
            '*����ԍ� 000036 2007/09/28 �C���I��
            csAtenaRow(ABAtenaEntity.KYUSEI) = csJukiDataRow(ABJukiData.KYUSEI)

            ' �E�Z���ԍ�=(����ԍ�)
            csAtenaRow(ABAtenaEntity.JUKIRRKNO) = CStr(csJukiDataRow(ABJukiData.RIREKINO)).RSubstring(2, 4)
            ' �E�����J�n�N�����`�Z���[�\����
            csAtenaRow(ABAtenaEntity.RRKST_YMD) = csJukiDataRow(ABJukiData.RRKST_YMD)
            csAtenaRow(ABAtenaEntity.RRKED_YMD) = csJukiDataRow(ABJukiData.RRKED_YMD)
            csAtenaRow(ABAtenaEntity.UMAREYMD) = csJukiDataRow(ABJukiData.UMAREYMD)
            csAtenaRow(ABAtenaEntity.UMAREWMD) = csJukiDataRow(ABJukiData.UMAREWMD)
            csAtenaRow(ABAtenaEntity.SEIBETSUCD) = csJukiDataRow(ABJukiData.SEIBETSUCD)
            csAtenaRow(ABAtenaEntity.SEIBETSU) = csJukiDataRow(ABJukiData.SEIBETSU)
            csAtenaRow(ABAtenaEntity.SEKINO) = csJukiDataRow(ABJukiData.SEIKINO)
            csAtenaRow(ABAtenaEntity.JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.JUMINHYOHYOJIJUN)
            ' �E��Q�Z���[�\����
            csAtenaRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.HYOJIJUN2)
            ' �E�����R�[�h�E�����E��2�����R�[�h�E��2����
            ' �@�Z����ʂ̉��P�����h8�h�i�]�o�ҁj�̏ꍇ�ő������h01�h�i���ю�j�̏ꍇ�A�Ǘ����̃R�[�h�ɕύX���A			
            '   ���̂̓N���A����
            If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) = "8") Then
                If (csJukiDataRow(ABJukiData.ZOKUGARACD).ToString.TrimEnd = "02") Then
                    If (m_strZokugara1Init = "00") Then
                        csAtenaRow(ABAtenaEntity.ZOKUGARACD) = String.Empty
                        csAtenaRow(ABAtenaEntity.ZOKUGARA) = String.Empty
                    Else
                        csAtenaRow(ABAtenaEntity.ZOKUGARACD) = m_strZokugara1Init
                        csAtenaRow(ABAtenaEntity.ZOKUGARA) = CNS_KURAN
                    End If

                Else
                    csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD)
                    csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA)
                End If
                If (csJukiDataRow(ABJukiData.ZOKUGARACD2).ToString.TrimEnd = "02") Then
                    If (m_strZokugara2Init = "00") Then
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = String.Empty
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = String.Empty
                    Else
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = m_strZokugara2Init
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = CNS_KURAN
                    End If
                Else
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2)
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2)
                End If
            Else
                ' �Z����ʂ̉��P�����h8�h�i�]�o�ҁj�łȂ��ꍇ�́A���̂܂܃Z�b�g			
                csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD)
                csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA)
                csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2)
                csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2)
            End If
            ' �E���ю�Z���R�[�h�`�J�i��Q���ю喼
            csAtenaRow(ABAtenaEntity.STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD)
            csAtenaRow(ABAtenaEntity.STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI)
            csAtenaRow(ABAtenaEntity.KANASTAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI)
            csAtenaRow(ABAtenaEntity.DAI2STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD2)
            csAtenaRow(ABAtenaEntity.DAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI2)
            csAtenaRow(ABAtenaEntity.KANADAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI2)

            ' �E�X�֔ԍ��`����
            ' �E�]�o�m��Z��������ꍇ�́A�]�o�m�藓����Z�b�g�i�Ȃ����ڂ̓Z�b�g�Ȃ��j
            If (csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString.TrimEnd <> String.Empty) Then
                csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO)
                '*����ԍ� 000001 2003/09/11 �C���J�n
                'csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD)
                csAtenaRow(ABAtenaEntity.JUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD), String)
                '*����ԍ� 000001 2003/09/11 �C���I��
                csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO)
                '*����ԍ� 000031 2007/01/30 �C���J�n
                ' �Ԓn��񂩂�Ԓn�R�[�h���擾

                '*����ԍ� 000038 2009/04/07 �C���J�n
                strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(CStr(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)))
                'strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(CStr(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)), strMotoBanchiCD, True)
                '' �擾�����Ԓn�R�[�h�z���Nothing�̍��ڂ�����ꍇ��String.Empty���Z�b�g����
                'For intLoop = 0 To strBanchiCD.Length - 1
                '    If (IsNothing(strBanchiCD(intLoop))) Then
                '        strBanchiCD(intLoop) = String.Empty
                '    End If
                'Next
                '*����ԍ� 000038 2009/04/07 �C���I��

                csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD(0)
                csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD(1)
                csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD(2)
                'csAtenaRow(ABAtenaEntity.BANCHICD1) = String.Empty
                'csAtenaRow(ABAtenaEntity.BANCHICD2) = String.Empty
                'csAtenaRow(ABAtenaEntity.BANCHICD3) = String.Empty
                '*����ԍ� 000031 2007/01/30 �C���I��
                csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)
                csAtenaRow(ABAtenaEntity.KATAGAKIFG) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKICD) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI)

                '*����ԍ� 000037 2008/05/12 �ǉ��J�n
                ' �Ǔ��ǊO�敪�F�ǊO�ɃZ�b�g    ���R�����g:�]�o�m��Z�������݂���ꍇ�͊ǊO�ɐݒ肷��B
                csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"
                '*����ԍ� 000037 2008/05/12 �ǉ��I��

            ElseIf (csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString.TrimEnd <> String.Empty) Then
                ' �E�]�o�m��Z���������A�]�o�\��Z��������ꍇ�́A�]�o�\�藓����Z�b�g�i�Ȃ����ڂ̓Z�b�g�Ȃ��j
                csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
                '*����ԍ� 000001 2003/09/11 �C���J�n
                'csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD)
                csAtenaRow(ABAtenaEntity.JUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String)
                '*����ԍ� 000001 2003/09/11 �C���I��
                csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
                '*����ԍ� 000031 2007/01/30 �C���J�n
                ' �Ԓn��񂩂�Ԓn�R�[�h���擾
                '*����ԍ� 000038 2009/04/07 �C���J�n
                strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(CStr(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)))
                'strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(CStr(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)), strMotoBanchiCD, True)
                '' �擾�����Ԓn�R�[�h�z���Nothing�̍��ڂ�����ꍇ��String.Empty���Z�b�g����
                'For intLoop = 0 To strBanchiCD.Length - 1
                '    If (IsNothing(strBanchiCD(intLoop))) Then
                '        strBanchiCD(intLoop) = String.Empty
                '    End If
                'Next
                '*����ԍ� 000038 2009/04/07 �C���I��
                csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD(0)
                csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD(1)
                csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD(2)
                'csAtenaRow(ABAtenaEntity.BANCHICD1) = String.Empty
                'csAtenaRow(ABAtenaEntity.BANCHICD2) = String.Empty
                'csAtenaRow(ABAtenaEntity.BANCHICD3) = String.Empty
                '*����ԍ� 000031 2007/01/30 �C���I��
                csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
                csAtenaRow(ABAtenaEntity.KATAGAKIFG) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKICD) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)

                '*����ԍ� 000037 2008/05/12 �ǉ��J�n
                ' �Ǔ��ǊO�敪�F�ǊO�ɃZ�b�g    ���R�����g:�]�o�\��Z�������݂���ꍇ�͊ǊO�ɐݒ肷��B
                csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"
                '*����ԍ� 000037 2008/05/12 �ǉ��I��

            Else
                ' �E�����������ꍇ�́A�Z��Z��������Z�b�g
                csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO)
                '*����ԍ� 000001 2003/09/11 �C���J�n
                'csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD)
                csAtenaRow(ABAtenaEntity.JUSHOCD) = CType(csJukiDataRow(ABJukiData.JUKIJUSHOCD), String).RPadLeft(13)
                '*����ԍ� 000001 2003/09/11 �C���I��
                csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO)
                csAtenaRow(ABAtenaEntity.BANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1)
                csAtenaRow(ABAtenaEntity.BANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2)
                csAtenaRow(ABAtenaEntity.BANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3)
                csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI)
                csAtenaRow(ABAtenaEntity.KATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG)
                csAtenaRow(ABAtenaEntity.KATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20)
                csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI)

                '*����ԍ� 000037 2008/05/12 �ǉ��J�n
                ' �Ǔ��ǊO�敪�F�Ǔ��ɃZ�b�g    ���R�����g:�]�o�m��Z���A�]�o�\��Z�������݂��Ȃ��ꍇ�͊Ǔ��ɐݒ肷��B
                csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1"
                '*����ԍ� 000037 2008/05/12 �ǉ��I��

            End If
            ' �E�A����P�`�����N����
            csAtenaRow(ABAtenaEntity.RENRAKUSAKI1) = csJukiDataRow(ABJukiData.RENRAKUSAKI1)
            csAtenaRow(ABAtenaEntity.RENRAKUSAKI2) = csJukiDataRow(ABJukiData.RENRAKUSAKI2)
            '*����ԍ� 000001 2003/09/11 �C���J�n
            'csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = csJukiDataRow(ABJukiData.HON_ZJUSHOCD)
            csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = CType(csJukiDataRow(ABJukiData.HON_ZJUSHOCD), String)
            '*����ԍ� 000001 2003/09/11 �C���I��
            csAtenaRow(ABAtenaEntity.HON_JUSHO) = csJukiDataRow(ABJukiData.HON_JUSHO)
            csAtenaRow(ABAtenaEntity.HONSEKIBANCHI) = csJukiDataRow(ABJukiData.HON_BANCHI)
            csAtenaRow(ABAtenaEntity.HITTOSH) = csJukiDataRow(ABJukiData.HITTOSHA)
            csAtenaRow(ABAtenaEntity.CKINIDOYMD) = csJukiDataRow(ABJukiData.CKINIDOYMD)
            csAtenaRow(ABAtenaEntity.CKINJIYUCD) = csJukiDataRow(ABJukiData.CKINJIYUCD)
            csAtenaRow(ABAtenaEntity.CKINJIYU) = csJukiDataRow(ABJukiData.CKINJIYU)
            csAtenaRow(ABAtenaEntity.CKINTDKDYMD) = csJukiDataRow(ABJukiData.CKINTDKDYMD)
            csAtenaRow(ABAtenaEntity.CKINTDKDTUCIKB) = csJukiDataRow(ABJukiData.CKINTDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.TOROKUIDOYMD) = csJukiDataRow(ABJukiData.TOROKUIDOYMD)
            csAtenaRow(ABAtenaEntity.TOROKUIDOWMD) = csJukiDataRow(ABJukiData.TOROKUIDOWMD)
            csAtenaRow(ABAtenaEntity.TOROKUJIYUCD) = csJukiDataRow(ABJukiData.TOROKUJIYUCD)
            csAtenaRow(ABAtenaEntity.TOROKUJIYU) = csJukiDataRow(ABJukiData.TOROKUJIYU)
            csAtenaRow(ABAtenaEntity.TOROKUTDKDYMD) = csJukiDataRow(ABJukiData.TOROKUTDKDYMD)
            csAtenaRow(ABAtenaEntity.TOROKUTDKDWMD) = csJukiDataRow(ABJukiData.TOROKUTDKDWMD)
            csAtenaRow(ABAtenaEntity.TOROKUTDKDTUCIKB) = csJukiDataRow(ABJukiData.TOROKUTDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.JUTEIIDOYMD) = csJukiDataRow(ABJukiData.JUTEIIDOYMD)
            csAtenaRow(ABAtenaEntity.JUTEIIDOWMD) = csJukiDataRow(ABJukiData.JUTEIIDOWMD)
            csAtenaRow(ABAtenaEntity.JUTEIJIYUCD) = csJukiDataRow(ABJukiData.JUTEIJIYUCD)
            csAtenaRow(ABAtenaEntity.JUTEIJIYU) = csJukiDataRow(ABJukiData.JUTEIJIYU)
            csAtenaRow(ABAtenaEntity.JUTEITDKDYMD) = csJukiDataRow(ABJukiData.JUTEITDKDYMD)
            csAtenaRow(ABAtenaEntity.JUTEITDKDWMD) = csJukiDataRow(ABJukiData.JUTEITDKDWMD)
            csAtenaRow(ABAtenaEntity.JUTEITDKDTUCIKB) = csJukiDataRow(ABJukiData.JUTEITDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.SHOJOIDOYMD) = csJukiDataRow(ABJukiData.SHOJOIDOYMD)
            csAtenaRow(ABAtenaEntity.SHOJOJIYUCD) = csJukiDataRow(ABJukiData.SHOJOJIYUCD)
            csAtenaRow(ABAtenaEntity.SHOJOJIYU) = csJukiDataRow(ABJukiData.SHOJOJIYU)
            csAtenaRow(ABAtenaEntity.SHOJOTDKDYMD) = csJukiDataRow(ABJukiData.SHOJOTDKDYMD)
            csAtenaRow(ABAtenaEntity.SHOJOTDKDTUCIKB) = csJukiDataRow(ABJukiData.SHOJOTDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIIDOYMD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIIDOYMD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITUCIYMD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYUCD) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYUCD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYU) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYU)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_YUBINNO) = csJukiDataRow(ABJukiData.TENUMAEJ_YUBINNO)
            '*����ԍ� 000001 2003/09/11 �C���J�n
            'csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD), String)
            '*����ԍ� 000001 2003/09/11 �C���I��
            csAtenaRow(ABAtenaEntity.TENUMAEJ_JUSHO) = csJukiDataRow(ABJukiData.TENUMAEJ_JUSHO)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_BANCHI) = csJukiDataRow(ABJukiData.TENUMAEJ_BANCHI)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_KATAGAKI) = csJukiDataRow(ABJukiData.TENUMAEJ_KATAGAKI)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSMEI)
            '* ����ԍ� 000063 2024/02/06 �C���J�n
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
            ''*����ԍ� 000001 2003/09/11 �C���J�n
            ''csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String).RPadLeft(13)
            ''*����ԍ� 000001 2003/09/11 �C���I��
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)

            '�Z��f�[�^.�������R�R�[�h��45�i�]���ʒm�󗝁j�̏ꍇ
            If (csJukiDataRow(ABJukiData.SHORIJIYUCD).ToString() = ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00")) Then
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD), String)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI)
            Else
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)
            End If
            '* ����ԍ� 000063 2024/02/06 �C���I��
            csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSMEI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO)
            '*����ԍ� 000001 2003/09/11 �C���J�n
            'csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD), String)
            '*����ԍ� 000001 2003/09/11 �C���I��
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSMEI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIMITDKFG) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMITDKFG)
            csAtenaRow(ABAtenaEntity.BIKOYMD) = csJukiDataRow(ABJukiData.BIKOYMD)
            csAtenaRow(ABAtenaEntity.BIKO) = csJukiDataRow(ABJukiData.BIKO)
            csAtenaRow(ABAtenaEntity.BIKOTENSHUTSUKKTIJUSHOFG) = csJukiDataRow(ABJukiData.BIKOTENSHUTSUKKTIJUSHOFG)
            csAtenaRow(ABAtenaEntity.HANNO) = csJukiDataRow(ABJukiData.HANNO)
            csAtenaRow(ABAtenaEntity.KAISEIATOFG) = csJukiDataRow(ABJukiData.KAISEIATOFG)
            csAtenaRow(ABAtenaEntity.KAISEIMAEFG) = csJukiDataRow(ABJukiData.KAISEIMAEFG)
            csAtenaRow(ABAtenaEntity.KAISEIYMD) = csJukiDataRow(ABJukiData.KAISEIYMD)

            ' �E�s����R�[�h�`�n�於�R
            ' �@�Z����ʂ̉��P�����h8�h�i�]�o�ҁj�łȂ��ꍇ�A�Z��s����`�Z��n�於�R���Z�b�g			
            If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) <> "8") Then
                csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1)
                csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1)
                '*����ԍ� 000002 2003/09/18 �C���J�n
                'csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                'csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2)
                csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2)
                '*����ԍ� 000002 2003/09/18 �C���I��
                csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
            Else
                ' �Z����ʂ̉��P�����h8�h�i�]�o�ҁj�̏ꍇ�A�Ǘ����i�s���揉�����`�n��R�j�����āA
                ' �N���A�ɂȂ��Ă���ꍇ�́A�Z�b�g���Ȃ�
                If (m_strGyosekuInit.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = String.Empty
                    csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = String.Empty
                Else
                    '*����ԍ� 000021 2005/12/12 �C���J�n
                    ''csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                    ''csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                    If m_strTenshutsuGyoseikuCD.Trim = String.Empty Then
                        ' �N���A���Ȃ��ꍇ�œ]�o�җp�̍s����b�c���ݒ肳��Ă��Ȃ��ꍇ��
                        ' ���̂܂܏Z��̃f�[�^��ݒ肷��B
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                    Else
                        ' �N���A���Ȃ��ꍇ�œ]�o�җp�̍s����b�c���ݒ肳��Ă���ꍇ��
                        ' �s����b�c�}�X�^���s���於�̂��擾���A�ݒ肷��B
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = m_strTenshutsuGyoseikuCD.RPadLeft(9, " "c)
                        '*����ԍ� 000022 2005/12/15 �C���J�n
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = String.Empty
                        'csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = m_strTenshutsuGyoseikuMei
                        '*����ԍ� 000022 2005/12/15 �C���I��
                    End If
                    '*����ԍ� 000021 2005/12/12 �C���I��
                End If
                If (m_strChiku1Init.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.CHIKUCD1) = String.Empty
                    csAtenaRow(ABAtenaEntity.CHIKUMEI1) = String.Empty
                Else
                    csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1)
                    csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1)
                End If
                If (m_strChiku2Init.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.CHIKUCD2) = String.Empty
                    csAtenaRow(ABAtenaEntity.CHIKUMEI2) = String.Empty
                Else
                    '*����ԍ� 000002 2003/09/18 �C���J�n
                    'csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                    'csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                    csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2)
                    csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2)
                    '*����ԍ� 000002 2003/09/18 �C���I��
                End If
                If (m_strChiku3Init.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.CHIKUCD3) = String.Empty
                    csAtenaRow(ABAtenaEntity.CHIKUMEI3) = String.Empty
                Else
                    csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                    csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                End If
            End If

            ' �E���[��R�[�h�`�ݗ��I���N����
            csAtenaRow(ABAtenaEntity.TOHYOKUCD) = csJukiDataRow(ABJukiData.TOHYOKUCD).ToString.RPadLeft(5)
            csAtenaRow(ABAtenaEntity.SHOGAKKOKUCD) = csJukiDataRow(ABJukiData.SHOGAKKOKUCD)
            csAtenaRow(ABAtenaEntity.CHUGAKKOKUCD) = csJukiDataRow(ABJukiData.CHUGAKKOKUCD)
            csAtenaRow(ABAtenaEntity.HOGOSHAJUMINCD) = csJukiDataRow(ABJukiData.HOGOSHAJUMINCD)
            csAtenaRow(ABAtenaEntity.KANJIHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANJIHOGOSHAMEI)
            csAtenaRow(ABAtenaEntity.KANAHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANAHOGOSHAMEI)
            csAtenaRow(ABAtenaEntity.KIKAYMD) = csJukiDataRow(ABJukiData.KIKAYMD)
            csAtenaRow(ABAtenaEntity.KARIIDOKB) = csJukiDataRow(ABJukiData.KARIIDOKB)
            csAtenaRow(ABAtenaEntity.SHORITEISHIKB) = csJukiDataRow(ABJukiData.SHORITEISHIKB)
            csAtenaRow(ABAtenaEntity.SHORIYOKUSHIKB) = csJukiDataRow(ABJukiData.SHORIYOKUSHIKB)
            csAtenaRow(ABAtenaEntity.JUKIYUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO)
            '*����ԍ� 000001 2003/09/11 �C���J�n
            csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD)
            'csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = CType(csJukiDataRow(ABJukiData.JUKIJUSHOCD), String).PadLeft(11)
            '*����ԍ� 000001 2003/09/11 �C���I��
            csAtenaRow(ABAtenaEntity.JUKIJUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO)
            csAtenaRow(ABAtenaEntity.JUKIBANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1)
            csAtenaRow(ABAtenaEntity.JUKIBANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2)
            csAtenaRow(ABAtenaEntity.JUKIBANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3)
            csAtenaRow(ABAtenaEntity.JUKIBANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI)
            csAtenaRow(ABAtenaEntity.JUKIKATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG)
            csAtenaRow(ABAtenaEntity.JUKIKATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20)
            csAtenaRow(ABAtenaEntity.JUKIKATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI)
            csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
            csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
            csAtenaRow(ABAtenaEntity.JUKICHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1)
            csAtenaRow(ABAtenaEntity.JUKICHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1)
            csAtenaRow(ABAtenaEntity.JUKICHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2)
            csAtenaRow(ABAtenaEntity.JUKICHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2)
            csAtenaRow(ABAtenaEntity.JUKICHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
            csAtenaRow(ABAtenaEntity.JUKICHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
            'csAtenaRow(ABAtenaEntity.KAOKUSHIKIKB) = String.Empty
            'csAtenaRow(ABAtenaEntity.BIKOZEIMOKU) = String.Empty
            csAtenaRow(ABAtenaEntity.KOKUSEKICD) = csJukiDataRow(ABJukiData.KOKUSEKICD)
            csAtenaRow(ABAtenaEntity.KOKUSEKI) = csJukiDataRow(ABJukiData.KOKUSEKI)
            csAtenaRow(ABAtenaEntity.ZAIRYUSKAKCD) = csJukiDataRow(ABJukiData.ZAIRYUSKAKCD)
            csAtenaRow(ABAtenaEntity.ZAIRYUSKAK) = csJukiDataRow(ABJukiData.ZAIRYUSKAK)
            csAtenaRow(ABAtenaEntity.ZAIRYUKIKAN) = csJukiDataRow(ABJukiData.ZAIRYUKIKAN)
            csAtenaRow(ABAtenaEntity.ZAIRYU_ST_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ST_YMD)
            csAtenaRow(ABAtenaEntity.ZAIRYU_ED_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ED_YMD)

            '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
            If ((blnJukiUmu) AndAlso (csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0)) Then
                '�Z����݂��銎�Z��t����񂪑��݂��鎞�A�O�Ԗڂ��擾
                csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows(0)
            Else
                '���݂��Ȃ����A��s�擾
                csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).NewRow
                Me.ClearAtenaFZY(csAtenaFzyRow)
            End If

            '�����E�Z����f�[�^�ݒ�
            csAtenaFzyRow = Me.SetAtenaFzy(csAtenaFzyRow, csAtenaRow, csJukiDataRow)
            '* ����ԍ� 000044 2011/11/09 �ǉ��I��

            '�����W��
            If ((blnJukiUmu) AndAlso (csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows.Count > 0)) Then
                '�Z����݂��銎�Z��W����񂪑��݂��鎞�A�O�Ԗڂ��擾
                csAtenaHyojunRow = csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows(0)
            Else
                '���݂��Ȃ����A��s�擾
                csAtenaHyojunRow = csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).NewRow
                Me.ClearAtenaHyojun(csAtenaHyojunRow)
            End If

            '�����E�Z����f�[�^�ݒ�
            csAtenaHyojunRow = Me.SetAtenaHyojun(csAtenaHyojunRow, csAtenaRow, csJukiDataRow)

            '�����t���W��
            If ((blnJukiUmu) AndAlso (csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)) Then
                '�Z����݂��銎�Z��W����񂪑��݂��鎞�A�O�Ԗڂ��擾
                csAtenaFzyHyojunRow = csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows(0)
            Else
                '���݂��Ȃ����A��s�擾
                csAtenaFzyHyojunRow = csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).NewRow
                Me.ClearAtenafzyHyojun(csAtenaFzyHyojunRow)
            End If

            '�����E�Z����f�[�^�ݒ�
            csAtenaFzyHyojunRow = Me.SetAtenaFzyHyojun(csAtenaFzyHyojunRow, csAtenaRow, csJukiDataRow)

            '---------------------------------------------------------------------------------------
            ' 6. �����}�X�^�̍X�V
            '�@�@�@�@�@���߂̏Z��f�[�^�����݂��Ă���ꍇ�͏C���A���Ă��Ȃ���Βǉ��ƂȂ�B
            '---------------------------------------------------------------------------------------

            ' �Z��L��e�k�f���h1�h�̎��́A�����}�X�^�̍X�V���s�Ȃ�
            If (blnJukiUmu) Then
                '* ����ԍ� 000044 2011/11/09 �C���J�n
                'intCount = m_cAtenaB.UpdateAtenaB(csAtenaRow)
                'If (intCount <> 1) Then
                '    ' �G���[��`���擾�i�Y���f�[�^�͑��ōX�V����Ă��܂��܂����B�ēx����F�����j
                '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                '    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "����", objErrorStruct.m_strErrorCode)
                'End If

                If (csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0) AndAlso
                    (csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).Rows.Count > 0) AndAlso
                    (csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                    intCount = m_cAtenaB.UpdateAtenaB(csAtenaRow, csAtenaHyojunRow, csAtenaFzyRow, csAtenaFzyHyojunRow, False)
                    If (intCount <> 1) Then
                        '* ����ԍ� 000047 2011/12/26 �ǉ��J�n
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '* ����ԍ� 000047 2011/12/26 �ǉ��I��
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "����", objErrorStruct.m_strErrorCode)
                    Else
                        '�������Ȃ�
                    End If
                Else
                    '����
                    intCount = m_cAtenaB.UpdateAtenaB(csAtenaRow)
                    If (intCount <> 1) Then
                        '* ����ԍ� 000047 2011/12/26 �ǉ��J�n
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '* ����ԍ� 000047 2011/12/26 �ǉ��I��
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "����", objErrorStruct.m_strErrorCode)
                    Else
                        '�������Ȃ�
                    End If

                    '�����W��
                    csAtenaHyojunRow(ABAtenaHyojunEntity.KOSHINNICHIJI) = csAtenaRow(ABAtenaEntity.KOSHINNICHIJI)
                    If (csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                        m_cABAtenaHyojunB.UpdateAtenaHyojunB(csAtenaHyojunRow)
                    Else
                        m_cABAtenaHyojunB.InsertAtenaHyojunB(csAtenaHyojunRow)
                    End If

                    If (blnAfterSekobi) Then
                        '�����t��
                        csAtenaFzyRow(ABAtenaFZYEntity.KOSHINNICHIJI) = csAtenaRow(ABAtenaEntity.KOSHINNICHIJI)
                        If (csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0) Then
                            m_cAtenaFzyB.UpdateAtenaFZYB(csAtenaFzyRow)
                        Else
                            m_cAtenaFzyB.InsertAtenaFZYB(csAtenaFzyRow)
                        End If
                        '�����t���W��
                        csAtenaFzyHyojunRow(ABAtenaFZYHyojunEntity.KOSHINNICHIJI) = csAtenaRow(ABAtenaEntity.KOSHINNICHIJI)
                        If (csAtenaFzyHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                            m_cABAtenaFZYHyojunB.UpdateAtenaFZYHyojunB(csAtenaFzyHyojunRow)
                        Else
                            m_cABAtenaFZYHyojunB.InsertAtenaFZYHyojunB(csAtenaFzyHyojunRow)
                        End If
                    End If
                End If
                '* ����ԍ� 000044 2011/11/09 �C���I��
            Else
                ' ��L�ȊO�́A�����}�X�^�̒ǉ����s�Ȃ�
                csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Add(csAtenaRow)
                '* ����ԍ� 000044 2011/11/09 �C���J�n
                'intCount = m_cAtenaB.InsertAtenaB(csAtenaRow)
                intCount = m_cAtenaB.InsertAtenaB(csAtenaRow, csAtenaHyojunRow, csAtenaFzyRow, csAtenaFzyHyojunRow)
                '* ����ԍ� 000044 2011/11/09 �C���I��
                If (intCount <> 1) Then
                    ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F�����j
                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "����", objErrorStruct.m_strErrorCode)
                End If
            End If



            '---------------------------------------------------------------------------------------
            ' 7. �����ݐσ}�X�^�̍X�V�@�i�O�j
            '�@�@�@�@�@�����C���̏ꍇ�́A�ޔ�����Ă����X�V�O�f�[�^���璼�߃��R�[�h���擾���A
            '�@�@�@�@�@�X�V�O�f�[�^�Ƃ���B
            '---------------------------------------------------------------------------------------

            '*����ԍ� 000016 2005/11/01 �ǉ��J�n
            '**
            '* �����ݐρi�O�j
            '*
            '*����ԍ� 000016 2005/11/01 �ǉ��I��
            '*����ԍ� 000003 2003/11/21 �ǉ��J�n
            '*����ԍ� 000032 2007/02/15 �ǉ��J�n
            If (Not IsNothing(m_csReRirekiEntity)) AndAlso
                (m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 0) Then
                ' �Z��痚�����S���n���Ă��鏈���̏ꍇ
                ' �X�V�O�̈���������񂩂�Z�o�O�D��敪���P�̒��߃��R�[�h���擾
                csBeforeRirekiRows = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUTOGAIYUSENKB='1' AND RRKED_YMD='99999999'")
                ' �����������擾
                StrShoriNichiji = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")

                ' �Ώۃ��R�[�h�����݂���ꍇ
                If (csBeforeRirekiRows.Length >= 1) Then
                    ' �����ݐς̐V�K���R�[�h���擾
                    csAtenaRuisekiEntity = m_csAtenaRuisekiEntity.Clone
                    csAtenaRuisekiRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                    Me.ClearAtenaRuiseki(csAtenaRuisekiRow)

                    ' �����������Z�b�g
                    csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI) = StrShoriNichiji

                    ' �O��敪 = 1
                    csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB) = "1"

                    ' �����}�X�^�̒��߃��R�[�h�����̂܂ܕҏW����
                    For Each csDataColumn In csBeforeRirekiRows(0).Table.Columns
                        csAtenaRuisekiRow(csDataColumn.ColumnName) = csBeforeRirekiRows(0)(csDataColumn.ColumnName)
                    Next csDataColumn

                    ' �������R�b�c�������ݐς�RESERCE�ɃZ�b�g����
                    '* ����ԍ� 000058 2015/10/14 �C���J�n
                    ' �����������쐬����i���ꏈ���̏ꍇ�ɓ���Ƃ��āj�́A�u41�F�E���C���v���Œ�Ń��U�[�u��o�^����
                    'csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
                    If (blnIsCreateAtenaRireki = True) Then
                        csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00")
                    Else
                        csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
                    End If
                    '* ����ԍ� 000058 2015/10/14 �C���I��

                    ' �����N�����擾����
                    csAtenaNenkinEntity = m_cAtenaNenkinB.GetAtenaNenkin(strJuminCD)
                    If (csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Count > 0) Then
                        ' �����ݐϐݒ�(�����N��)
                        Me.SetNenkinToRuiseki(csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows(0), csAtenaRuisekiRow)
                    End If
                    ' �������ۂ��擾����
                    csAtenaKokuhoEntity = m_cAtenaKokuhoB.GetAtenaKokuho(strJuminCD)
                    If (csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Count > 0) Then
                        ' �����ݐϐݒ�(��������)
                        Me.SetKokuhoToRuiseki(csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows(0), csAtenaRuisekiRow)
                    End If

                    ' �����ݐς֒ǉ�����
                    csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csAtenaRuisekiRow)

                    ' �����ݐσ}�X�^�̒ǉ����s��
                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow)
                    '�����t��
                    If ((Me.m_csReRirekiFzyEntity IsNot Nothing) _
                            AndAlso (Me.m_csReRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows.Count > 0)) Then
                        '�ޔ�������������t���Ƀf�[�^�����݂���ꍇ
                        csSelectedRows = m_csReRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME) _
                                            .Select(String.Format("{0}='{1}' AND {2}='{3}'",
                                                                  ABAtenaRirekiFZYEntity.JUMINCD,
                                                                  csAtenaRuisekiRow(ABAtenaRuisekiEntity.JUMINCD).ToString,
                                                                  ABAtenaRirekiFZYEntity.RIREKINO,
                                                                  csAtenaRuisekiRow(ABAtenaRuisekiEntity.RIREKINO).ToString))
                        If (csSelectedRows.Count > 0) Then
                            '���ߍs�����݂��鎞�A�ݐϕt���̐V�K�s���쐬
                            csAtenaRuisekiFzyEntity = m_csAtenaRuisekiFzyEntity.Clone
                            csAtenaRuisekiFzyRow = csAtenaRuisekiFzyEntity.Tables(ABAtenaRuisekiFZYEntity.TABLE_NAME).NewRow
                            Me.ClearAtenaFZY(csAtenaRuisekiFzyRow)
                            '���ߗ����s��ޔ����Ă���
                            csAtenaRirekiFzyRow = csSelectedRows(0)
                            csAtenaRuisekiFzyRow = Me.SetAtenaRuisekiFzy(csAtenaRuisekiFzyRow, csAtenaRirekiFzyRow, csAtenaRuisekiRow)
                        Else
                            '��L�ȊO�̎��ANothing
                            csAtenaRuisekiFzyRow = Nothing
                        End If
                    Else
                        '��L�ȊO�̎��ANothing
                        csAtenaRuisekiFzyRow = Nothing
                    End If

                    '������W��
                    If ((Me.m_csReRirekiHyojunEntity IsNot Nothing) _
                            AndAlso (Me.m_csReRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows.Count > 0)) Then
                        '�ޔ�������������W���Ƀf�[�^�����݂���ꍇ
                        csSelectedRows = m_csReRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME) _
                                            .Select(String.Format("{0}='{1}' AND {2}='{3}'",
                                                                  ABAtenaRirekiHyojunEntity.JUMINCD,
                                                                  csAtenaRuisekiRow(ABAtenaRuisekiEntity.JUMINCD).ToString,
                                                                  ABAtenaRirekiHyojunEntity.RIREKINO,
                                                                  csAtenaRuisekiRow(ABAtenaRuisekiEntity.RIREKINO).ToString))
                        If (csSelectedRows.Count > 0) Then
                            '���ߍs�����݂��鎞�A�ݐϕW���̐V�K�s���쐬
                            csAtenaRuisekiHyojunEntity = m_csAtenaRuisekiHyojunEntity.Clone
                            csAtenaRuisekiHyojunRow = csAtenaRuisekiHyojunEntity.Tables(ABAtenaRuisekiHyojunEntity.TABLE_NAME).NewRow
                            Me.ClearAtenaHyojun(csAtenaRuisekiHyojunRow)
                            '���ߗ����s��ޔ����Ă���
                            csAtenaRirekiHyojunRow = csSelectedRows(0)
                            csAtenaRuisekiHyojunRow = Me.SetAtenaRuisekiHyojun(csAtenaRuisekiHyojunRow, csAtenaRirekiHyojunRow, csAtenaRuisekiRow)
                        Else
                            '��L�ȊO�̎��ANothing
                            csAtenaRuisekiHyojunRow = Nothing
                        End If
                    Else
                        '��L�ȊO�̎��ANothing
                        csAtenaRuisekiHyojunRow = Nothing
                    End If

                    '��������t���W��
                    If ((Me.m_csRERirekiFZYHyojunEntity IsNot Nothing) _
                            AndAlso (Me.m_csRERirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0)) Then
                        '�ޔ�������������t���W���Ƀf�[�^�����݂���ꍇ
                        csSelectedRows = m_csRERirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME) _
                                            .Select(String.Format("{0}='{1}' AND {2}='{3}'",
                                                                  ABAtenaRirekiFZYHyojunEntity.JUMINCD,
                                                                  csAtenaRuisekiRow(ABAtenaRuisekiEntity.JUMINCD).ToString,
                                                                  ABAtenaRirekiFZYHyojunEntity.RIREKINO,
                                                                  csAtenaRuisekiRow(ABAtenaRuisekiEntity.RIREKINO).ToString))
                        If (csSelectedRows.Count > 0) Then
                            '���ߍs�����݂��鎞�A�ݐϕt���W���̐V�K�s���쐬
                            csAtenaRuisekiFZYHyojunEntity = m_csAtenaRuisekiFZYHyojunEntity.Clone
                            csAtenaRuisekiFZYHyojunRow = csAtenaRuisekiFZYHyojunEntity.Tables(ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME).NewRow
                            Me.ClearAtenaFZYHyojun(csAtenaRuisekiFZYHyojunRow)
                            '���ߗ����s��ޔ����Ă���
                            csAtenaRirekiFZYHyojunRow = csSelectedRows(0)
                            csAtenaRuisekiFZYHyojunRow = Me.SetAtenaRuisekiFZYHyojun(csAtenaRuisekiFZYHyojunRow, csAtenaRirekiFZYHyojunRow, csAtenaRuisekiRow)
                        Else
                            '��L�ȊO�̎��ANothing
                            csAtenaRuisekiFZYHyojunRow = Nothing
                        End If
                    Else
                        '��L�ȊO�̎��ANothing
                        csAtenaRuisekiFZYHyojunRow = Nothing
                    End If

                    intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow, csAtenaRuisekiHyojunRow, csAtenaRuisekiFzyRow, csAtenaRuisekiFZYHyojunRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��
                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F�����ݐρj
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�����ݐ�", objErrorStruct.m_strErrorCode)
                    End If

                End If
            Else
                '*����ԍ� 000032 2007/02/15 �ǉ��I��
                ' ���������}�X�^�̏Z���Z�o�O�敪���P�i�Z���j�ŗ���ԍ�����ԑ傫�����̂��擾
                cSearchKey = New ABAtenaSearchKey
                cSearchKey.p_strJuminCD = strJuminCD
                csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "", "1", True)
                StrShoriNichiji = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")
                ' �f�[�^�����݂���ꍇ�́A
                If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then
                    csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)

                    ' �����ݐς̗���擾���A����������B�i�X�V�J�E�^�[�́A0�A����ȊO�́AString Empty�j�i���ʁj�@			
                    ' �����ݐς��V����Row���擾����
                    csAtenaRuisekiEntity = m_csAtenaRuisekiEntity.Clone
                    csAtenaRuisekiRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                    ' ��������������������
                    Me.ClearAtenaRuiseki(csAtenaRuisekiRow)

                    ' ���������}�X�^��舶���ݐσ}�X�^�̕ҏW���s��(����)
                    ' ��������=�V�X�e������
                    csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI) = StrShoriNichiji

                    ' �O��敪 = 1
                    csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB) = "1"

                    ' ����ȊO�̍��ڂɂ��ẮA�����}�X�^�����̂܂ܕҏW����
                    ' �������������������ւ��̂܂ܕҏW����
                    For Each csDataColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                        csAtenaRuisekiRow(csDataColumn.ColumnName) = csAtenaRirekiRow(csDataColumn)
                    Next csDataColumn

                    '*����ԍ� 000015 2005/08/17 �ǉ��J�n 000029 2006/04/19 �C���J�n
                    ' �������R�b�c�������ݐς�RESERCE�ɃZ�b�g����
                    '* ����ԍ� 000058 2015/10/14 �C���J�n
                    ' �����������쐬����i���ꏈ���̏ꍇ�ɓ���Ƃ��āj�́A�u41�F�E���C���v���Œ�Ń��U�[�u��o�^����
                    'csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
                    If (blnIsCreateAtenaRireki = True) Then
                        csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00")
                    Else
                        csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
                    End If
                    '* ����ԍ� 000058 2015/10/14 �C���I��
                    '' �ėp�b�c�������ݐς�RESERCE�ɃZ�b�g����
                    'csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.HANYOCD)
                    '*����ԍ� 000015 2005/08/17 �ǉ��I�� 000029 2006/04/19 �C���I��

                    '*����ԍ� 000003 2003/11/21 �ǉ��J�n
                    ' �����N�����擾����
                    csAtenaNenkinEntity = m_cAtenaNenkinB.GetAtenaNenkin(strJuminCD)
                    If (csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Count > 0) Then
                        ' �����ݐϐݒ�(�����N��)
                        Me.SetNenkinToRuiseki(csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows(0), csAtenaRuisekiRow)
                    End If
                    ' �������ۂ��擾����
                    csAtenaKokuhoEntity = m_cAtenaKokuhoB.GetAtenaKokuho(strJuminCD)
                    If (csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Count > 0) Then
                        ' �����ݐϐݒ�(��������)
                        Me.SetKokuhoToRuiseki(csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows(0), csAtenaRuisekiRow)
                    End If
                    '*����ԍ� 000003 2003/11/21 �ǉ��I��

                    ' �����ݐς֒ǉ�����
                    csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csAtenaRuisekiRow)

                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    '' �����ݐσ}�X�^�̒ǉ����s��
                    'intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow)

                    '��������t���f�[�^�擾
                    csAtenaRuisekiFzyEntity = m_cAtenaRirekiFzyB.GetAtenaFZYRBHoshu(csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                                  csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString,
                                                                                  String.Empty, True)
                    If (csAtenaRuisekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows.Count > 0) Then
                        '���߂̈�������t�������݂������A�����ݐϕt�����쐬
                        csAtenaRuisekiFzyRow = m_csAtenaRuisekiFzyEntity.Tables(ABAtenaRuisekiFZYEntity.TABLE_NAME).NewRow
                        Me.ClearAtenaFZY(csAtenaRuisekiFzyRow)
                        '���ߗ����s��ޔ����Ă���
                        csAtenaRirekiFzyRow = csAtenaRuisekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows(0)
                        csAtenaRuisekiFzyRow = Me.SetAtenaRuisekiFzy(csAtenaRuisekiFzyRow, csAtenaRirekiFzyRow, csAtenaRuisekiRow)
                    Else
                        '��L�ȊO�̎��ANothing
                        csAtenaRuisekiFzyRow = Nothing
                    End If

                    '��������W��
                    csAtenaRirekiHyojunEntity = m_cABAtenaRirekiHyojunB.GetAtenaRirekiHyojunBHoshu(
                                                                                  csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                                  csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString,
                                                                                  String.Empty, True)
                    If (csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                        '���߂̈�������W�������݂������A�����ݐϕW�����쐬
                        csAtenaRuisekiHyojunRow = m_csAtenaRuisekiHyojunEntity.Tables(ABAtenaRuisekiHyojunEntity.TABLE_NAME).NewRow
                        Me.ClearAtenaHyojun(csAtenaRuisekiHyojunRow)
                        '���ߗ����s��ޔ����Ă���
                        csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows(0)
                        csAtenaRuisekiHyojunRow = Me.SetAtenaRuisekiHyojun(csAtenaRuisekiHyojunRow, csAtenaRirekiHyojunRow, csAtenaRuisekiRow)
                    Else
                        '��L�ȊO�̎��ANothing
                        csAtenaRuisekiHyojunRow = Nothing
                    End If

                    '��������t���W��
                    csAtenaRirekiFZYHyojunEntity = m_cABAtenaRirekiFZYHyojunB.GetAtenaRirekiFZYHyojunBHoshu(
                                                                                  csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                                  csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString,
                                                                                  String.Empty, True)
                    If (csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                        '���߂̈�������t���W�������݂������A�����ݐϕt���W�����쐬
                        csAtenaRuisekiFZYHyojunRow = m_csAtenaRuisekiFZYHyojunEntity.Tables(ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME).NewRow
                        Me.ClearAtenaFZYHyojun(csAtenaRuisekiFZYHyojunRow)
                        '���ߗ����s��ޔ����Ă���
                        csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows(0)
                        csAtenaRuisekiFZYHyojunRow = Me.SetAtenaRuisekiFZYHyojun(csAtenaRuisekiFZYHyojunRow, csAtenaRirekiFZYHyojunRow, csAtenaRuisekiRow)
                    Else
                        '��L�ȊO�̎��ANothing
                        csAtenaRuisekiFZYHyojunRow = Nothing
                    End If

                    ' �����ݐσ}�X�^�̒ǉ����s��
                    intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow, csAtenaRuisekiHyojunRow, csAtenaRuisekiFzyRow, csAtenaRuisekiFZYHyojunRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��

                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F�����ݐρj
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�����ݐ�", objErrorStruct.m_strErrorCode)
                    End If
                End If
                '*����ԍ� 000032 2007/02/15 �ǉ��J�n
            End If
            '*����ԍ� 000032 2007/02/15 �ǉ��I��
            '*����ԍ� 000003 2003/11/21 �ǉ��I��



            '---------------------------------------------------------------------------------------
            ' 8. ���������}�X�^�̍X�V
            '---------------------------------------------------------------------------------------

            '**
            '* ��������
            '*
            '*����ԍ� 000016 2005/11/18 �C���J�n
            '* corresponds to VS2008 Start 2010/04/16 000043
            ''''*����ԍ� 000013 2005/06/19 �ǉ��J�n
            '''''����ԍ��̎擾
            ''''csRirekiNoEntity = m_cAtenaRirekiB.GetRirekiNo(strJuminCD)

            ''''' �����}�X�^��舶�������}�X�^�̕ҏW���s��(����)
            ''''' ����ԍ��@�@�@�V�K�̂΂����́A0001�@�@�C���̏ꍇ�́A���������}�X�^�̍ŏI�ԍ��ɂ`�c�c�@�P����
            ''''' ����ȊO�̍��ڂɂ��ẮA�����}�X�^�����̂܂ܕҏW����			
            ''''If (csRirekiNoEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
            ''''    ' ����ԍ�
            ''''    strMaxRirekino = "0001"
            ''''Else
            ''''    ' ����ԍ�(�擪�s�̗���ԍ�+1)
            ''''    strMaxRirekino = CType((CType(csRirekiNoEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).PadLeft(4, "0"c)
            ''''End If
            ''''*����ԍ� 000013 2005/06/19 �ǉ��C��
            '* corresponds to VS2008 End 2010/04/16 000043


            '---------------------------------------------------------------------------------------
            ' 8-1. �Y���̗����f�[�^��S���擾����
            '---------------------------------------------------------------------------------------

            cSearchKey = New ABAtenaSearchKey
            cSearchKey.p_strJuminCD = strJuminCD
            csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)

            '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
            '��������t���̑S���R�[�h���擾
            csAtenaRirekiFzyEntity = m_cAtenaRirekiFzyB.GetAtenaFZYRBHoshu(strJuminCD, String.Empty, String.Empty, True)
            '* ����ԍ� 000044 2011/11/09 �ǉ��I��

            '��������W��
            csAtenaRirekiHyojunEntity = m_cABAtenaRirekiHyojunB.GetAtenaRirekiHyojunBHoshu(strJuminCD, String.Empty, String.Empty, True)

            '��������t���W��
            csAtenaRirekiFZYHyojunEntity = m_cABAtenaRirekiFZYHyojunB.GetAtenaRirekiFZYHyojunBHoshu(strJuminCD, String.Empty, String.Empty, True)

            ' ����ԍ��̎擾
            If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
                ' ����ԍ�
                strMaxRirekino = "0001"
            Else
                ' ����ԍ�
                '*����ԍ� 000023 2005/12/16 �C���J�n
                ' ����ԍ��~���ŕ��בւ��čő嗚��ԍ��{�P���擾����
                ''strMaxRirekino = CType(csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count + 1, String).PadLeft(4, "0"c)
                csRirekiNORows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
                intMaxRirekiNO = CType(csRirekiNORows(0)(ABAtenaRirekiEntity.RIREKINO), Integer) + 1
                strMaxRirekino = CType(intMaxRirekiNO, String).RPadLeft(4, "0"c)
                '*����ԍ� 000023 2005/12/16 �C���I��
            End If
            '*����ԍ� 000016 2005/11/18 �C���I��


            '---------------------------------------------------------------------------------------
            ' 8-2. ���O�̗����f�[�^���X�V����
            '�@�@�@�@�@�Z��f�[�^�����݂��Ă���ꍇ�̂ݍs���B
            '---------------------------------------------------------------------------------------

            ' �E�Z��L��e�k�f���h1�h�̎��́A�Z��D��Ŏw��N������99999999�ň��������}�X�^����ݗ����I���N�������V�X�e�A
            ' �@�����t�̑O�����Z�b�g���A���������}�X�^�X�V�����s����
            If (blnJukiUmu) Then

                '*����ԍ� 000016 2005/11/01 �C���J�n
                '* �R�����g**********************************************************************************************
                '* �����������}�X�^�X�V���@��                                                                           *
                '* �Z��Ƃ̘A�g���@�𒼋߃f�[�^������S�����̂Q�p�^�[���ł����s��Ȃ��悤�ɂ����̂ŁA�ȉ����C�����܂��B *
                '* �Z����y�������R�b�c�z���ڒǉ����Ă�������̂ŁA��������ē��ꏈ���C��(03)�̎��́A���������}�X�^�� *
                '* ���߃f�[�^���X�V����B����ȊO�̂Ƃ��͒��߃��R�[�h�̏I���N�������X�V���V�K���R�[�h�ǉ��ƂȂ�܂��B   *
                '********************************************************************************************************
                '* corresponds to VS2008 Start 2010/04/16 000043
                ''''*����ԍ� 000013 2005/06/19 �ǉ��J�n
                ''''' ���t�N���X�̕K�v�Ȑݒ���s��
                ''''m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                ''''m_cfDateClass.p_enEraType = UFEraType.Number

                '''''�����f�[�^�S���擾
                ''''cSearchKey = New ABAtenaSearchKey()
                ''''cSearchKey.p_strJuminCD = strJuminCD
                ''''cSearchKey.p_strJuminYuseniKB = "1"
                ''''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)

                '''''����ԍ��������ɕ��ёւ�
                ''''csSortRirekiDataRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO)

                '''''�����f�[�^���Ȃ��Ȃ�܂ŌJ�Ԃ�
                ''''' �f�[�^���J��Ԃ�
                ''''For Each csDataRow In csSortRirekiDataRow
                ''''    '�c�a�ɂ���J�n�N�����Ɠ���������ȏ�̂��́@���@�c�a�̊J�n�N�������I���N�������ߋ��̂���
                ''''    If (CType(csJukiDataRow(ABAtenaRirekiEntity.RRKST_YMD), String) <= CType(csDataRow(ABAtenaRirekiEntity.RRKST_YMD), String)) AndAlso _
                ''''        (CType(csDataRow(ABAtenaRirekiEntity.RRKST_YMD), String) < CType(csDataRow(ABAtenaRirekiEntity.RRKED_YMD), String)) Then

                ''''        ' �����}�X�^�����������ւ��̂܂ܕҏW����
                ''''        For Each csDataColumn In csAtenaRow.Table.Columns
                ''''            csAtenaRirekiRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
                ''''        Next csDataColumn

                ''''        '�ǉ��p���R�[�h�̕ҏW���s��
                ''''        csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = strMaxRirekino                                         '����ԍ�
                ''''        csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB) = csDataRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB)   '�Z���Z�o�O�敪
                ''''        csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINYUSENIKB) = csDataRow(ABAtenaRirekiEntity.JUMINYUSENIKB)     '�Z���D��敪
                ''''        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = csDataRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB)   '�Z�o�O�D��敪
                ''''        csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = csDataRow(ABAtenaRirekiEntity.RRKST_YMD)             '�J�n�N����
                ''''        csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = csDataRow(ABAtenaRirekiEntity.RRKED_YMD)             '�I���N����

                ''''        '����ԍ��Ɂ{�P
                ''''        strMaxRirekino = CType(CType(strMaxRirekino, Integer) + 1, String).PadLeft(4, "0"c)

                ''''        ' ���������}�X�^�̒ǉ����s��
                ''''        'csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csAtenaRirekiRow)
                ''''        intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)
                ''''        If (intCount <> 1) Then
                ''''            ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                ''''            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ''''            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                ''''            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                ''''        End If

                ''''        '�I���N�����ɊJ�n�N�����̑O�����Z�b�g����
                ''''        m_cfDateClass.p_strDateValue = CType(csDataRow(ABAtenaRirekiEntity.RRKST_YMD), String)
                ''''        csDataRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)

                ''''        '���������}�X�^�̏C�����s��
                ''''        intCount = m_cAtenaRirekiB.UpdateAtenaRB(csDataRow)
                ''''        If (intCount <> 1) Then
                ''''            ' �G���[��`���擾�i�Y���f�[�^�͑��ōX�V����Ă��܂��܂����B�ēx����F���������j
                ''''            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ''''            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                ''''            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                ''''        End If

                ''''        '���ꏈ���t���O��True�ɂ���
                ''''        blnTokushuFG = True

                ''''    End If
                ''''Next
                '''''*����ԍ� 000013 2005/06/19 �ǉ��I��
                '*����ԍ� 000016 2005/11/18 �폜�J�n
                '''' ���t�N���X�̕K�v�Ȑݒ���s��
                ''''m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                ''''m_cfDateClass.p_enEraType = UFEraType.Number
                '*����ԍ� 000016 2005/11/18 �폜�I��
                '* corresponds to VS2008 End 2010/04/16 000043

                '---------------------------------------------------------------------------------------
                ' 8-2-1. ���ꏈ���̏ꍇ�́A�����钼�߃��R�[�h���㏑������B
                '---------------------------------------------------------------------------------------

                '*����ԍ� 000041 2009/06/18 �C���J�n
                '*����ԍ� 000018 2005/11/27 �C���J�n
                '* corresponds to VS2008 Start 2010/04/16 000043
                '''' �������R�R�[�h��"03"(���ꏈ���C��)�̏ꍇ�͒��߃��R�[�h�̏C�����X�V�݂̂��s��
                '* corresponds to VS2008 End 2010/04/16 000043
                ''If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" Then
                '' �������R�R�[�h��"03"(���ꏈ���C��)�@�܂��́@"04"(�Z���[�b�c�C��)�̏ꍇ��
                '' ���߃��R�[�h�̏C�����X�V�݂̂��s��(�ǉ������ɍX�V����)
                ' �������R�R�[�h��"03"(���ꏈ���C��)�@�܂��́@"04"(�Z���[�b�c�C��)�̏ꍇ �܂��� 
                ' �����f�[�^�S���폜���s��ꂸ ���� �������R�R�[�h��"08"(�����C��)�̏ꍇ��
                ' ���߃��R�[�h�̏C�����X�V�݂̂��s��(�ǉ������ɍX�V����)
                'If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
                '   CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" Then
                '* ����ԍ� 000050 2014/06/25 �C���J�n
                'If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
                '   CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse _
                '   (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08") Then
                '* ����ԍ� 000058 2015/10/14 �C���J�n
                'If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
                '   CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse _
                '   CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "05" OrElse _
                '   (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08") Then
                If (blnIsCreateAtenaRireki = False _
                    AndAlso (CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse
                             CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse
                             CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "05" OrElse
                             (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08"))) Then
                    '* ����ԍ� 000058 2015/10/14 �C���I��
                    '* ����ԍ� 000050 2014/06/25 �C���I��
                    '*����ԍ� 000018 2005/11/27 �C���I��
                    '*����ԍ� 000041 2009/06/18 �C���I��
                    ' ���������f�[�^���o(�Z���Z�o�O�敪��"1"�ŗ���ԍ��ő�~���ŕ��ёւ�)
                    csUpRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUMINJUTOGAIKB = '1'", ABAtenaRirekiEntity.RIREKINO + " DESC")

                    ' ���߃��R�[�h�̎擾
                    ' ���ꏈ���C���̏ꍇ�͕K�������}�X�^�ɂ���͂��Ȃ̂Ŗ����ꍇ�͍l�����Ȃ�
                    csUpRirekiRow = csUpRirekiRows(0)

                    ' ���߃��R�[�h���C�����čX�V����
                    ' �����}�X�^�����������ւ��̂܂ܕҏW����
                    For Each csDataColumn In csAtenaRow.Table.Columns
                        '*����ԍ� 000030 2006/08/10 �C���J�n
                        ' �����J�n�N�����͍X�V���Ȃ�
                        ''csUpRirekiRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
                        If csDataColumn.ColumnName <> ABAtenaEntity.RRKST_YMD Then
                            csUpRirekiRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
                        End If
                        '*����ԍ� 000030 2006/08/10 �C���I��
                    Next csDataColumn

                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    '' ���������}�X�^���X�V����
                    'intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow)

                    '���������̒��ߍs���父������t���̒��ߍs����
                    csSelectedRows = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Select(
                                                String.Format("{0}='{1}' AND {2}='{3}'",
                                                              ABAtenaRirekiFZYEntity.JUMINCD,
                                                              csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                              ABAtenaRirekiFZYEntity.RIREKINO,
                                                              csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString))
                    If (csSelectedRows.Count > 0) Then
                        '�������ʂ����݂��鎞�A�����t�����父������t���Ƀf�[�^���ʂ�
                        csAtenaRirekiFzyTokushuRow = csSelectedRows(0)
                        csAtenaRirekiFzyTokushuRow = Me.SetAtenaRirekiFzy(csAtenaRirekiFzyTokushuRow, csAtenaFzyRow)
                    Else

                        '��L�ȊO�̎��ANothing
                        csAtenaRirekiFzyTokushuRow = Nothing
                    End If

                    '��������W��
                    '���������̒��ߍs���父������W���̒��ߍs����
                    csSelectedRows = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Select(
                                                String.Format("{0}='{1}' AND {2}='{3}'",
                                                              ABAtenaRirekiHyojunEntity.JUMINCD,
                                                              csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                              ABAtenaRirekiHyojunEntity.RIREKINO,
                                                              csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString))
                    If (csSelectedRows.Count > 0) Then
                        '�������ʂ����݂��鎞�A�����t�����父������t���Ƀf�[�^���ʂ�
                        csAtenaRirekiHyojunTokushuRow = csSelectedRows(0)
                        csAtenaRirekiHyojunTokushuRow = Me.SetAtenaRirekiHyojun(csAtenaRirekiHyojunTokushuRow, csAtenaHyojunRow, csUpRirekiRow)
                    Else

                        '��L�ȊO�̎��ANothing
                        csAtenaRirekiHyojunTokushuRow = Nothing
                    End If

                    '��������t���W��
                    '���������̒��ߍs���父������t���W���̒��ߍs����
                    csSelectedRows = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Select(
                                                String.Format("{0}='{1}' AND {2}='{3}'",
                                                              ABAtenaRirekiFZYHyojunEntity.JUMINCD,
                                                              csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                              ABAtenaRirekiFZYHyojunEntity.RIREKINO,
                                                              csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString))
                    If (csSelectedRows.Count > 0) Then
                        '�������ʂ����݂��鎞�A�����t�����父������t���Ƀf�[�^���ʂ�
                        csAtenaRirekiFzyHyojunTokushuRow = csSelectedRows(0)
                        csAtenaRirekiFzyHyojunTokushuRow = Me.SetAtenaRirekiFZYHyojun(csAtenaRirekiFzyHyojunTokushuRow, csAtenaFzyHyojunRow)
                    Else

                        '��L�ȊO�̎��ANothing
                        csAtenaRirekiFzyHyojunTokushuRow = Nothing
                    End If

                    ' ���������}�X�^���X�V����
                    intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow, csAtenaRirekiHyojunTokushuRow, csAtenaRirekiFzyTokushuRow, csAtenaRirekiFzyHyojunTokushuRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��

                    ' �X�V�������P���łȂ��ƃG���[
                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i�Y���f�[�^�͑��ōX�V����Ă��܂��܂����B�ēx����F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If

                    blnTokushuFG = True
                Else
                    blnTokushuFG = False
                End If
                '*����ԍ� 000016 2005/11/01 �C���I��

                '---------------------------------------------------------------------------------------
                ' 8-2-2. ���ꏈ���ȊO�̏ꍇ�A�����钼�߃��R�[�h�̏I���N���������B
                '---------------------------------------------------------------------------------------

                '*����ԍ� 000013 2005/06/19 �C���J�n
                '* corresponds to VS2008 Start 2010/04/16 000043
                '''' ���t�N���X�̕K�v�Ȑݒ���s��
                '* corresponds to VS2008 End 2010/04/16 000043
                ''m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                ''m_cfDateClass.p_enEraType = UFEraType.Number
                '���ꏈ���̔���
                If Not blnTokushuFG Then
                    '�����������}�X�^�̒��߃��R�[�h�̏I���N�������C�����čX�V��
                    '*����ԍ� 000013 2005/06/19 �C���I��
                    '*����ԍ� 000016 2005/11/18 �C���J�n
                    '* corresponds to VS2008 Start 2010/04/16 000043
                    ''''cSearchKey = New ABAtenaSearchKey()
                    ''''cSearchKey.p_strJuminCD = strJuminCD
                    ''''cSearchKey.p_strJuminYuseniKB = "1"
                    ''''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "99999999", True)
                    ''''If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then
                    ''''    csDataRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)
                    ''''    '*����ԍ� 000012 2005/06/07 �C���J�n
                    ''''    'm_cfDateClass.p_strDateValue = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd") '�V�X�e�����t
                    ''''    m_cfDateClass.p_strDateValue = CType(csAtenaRow(ABAtenaRirekiEntity.RRKST_YMD), String)
                    ''''    '*����ԍ� 000012 2005/06/07 �C���I��
                    ''''    csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                    ''''    intCount = m_cAtenaRirekiB.UpdateAtenaRB(csDataRow)
                    ''''    If (intCount <> 1) Then
                    ''''        ' �G���[��`���擾�i�Y���f�[�^�͑��ōX�V����Ă��܂��܂����B�ēx����F���������j
                    ''''        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    ''''        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                    ''''        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    ''''    End If
                    ''''End If
                    '* corresponds to VS2008 End 2010/04/16 000043
                    ' ���������f�[�^���o(�Z���D��敪��"1"�ŗ����I���N������'99999999')
                    csUpRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUMINYUSENIKB = '1' AND RRKED_YMD = '99999999'")

                    ' ���߃��R�[�h���擾���A�A�b�v�f�[�g
                    If csUpRirekiRows.Length > 0 Then
                        csUpRirekiRow = csUpRirekiRows(0)
                        m_cfDateClass.p_strDateValue = CType(csAtenaRow(ABAtenaRirekiEntity.RRKST_YMD), String)
                        csUpRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                        '* ����ԍ� 000044 2011/11/09 �C���J�n
                        'intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow)

                        '���߈��������f�[�^��ޔ��f�[�^����擾
                        '* ����ԍ� 000047 2011/12/26 �C���J�n
                        'csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, _
                        '                                              csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, _
                        '                                              csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                        csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(csAtenaRirekiFzyEntity,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                        '* ����ԍ� 000047 2011/12/26 �C���I��
                        '��������W��
                        csCkinRirekiHyojunRows = Me.GetChokkin_RirekiHyojun(csAtenaRirekiHyojunEntity,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)

                        '��������t���W��
                        csCkinRirekiFzyHyojunRows = Me.GetChokkin_RirekiFZYHyojun(csAtenaRirekiFZYHyojunEntity,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)

                        intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow, csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows)
                        '* ����ԍ� 000044 2011/11/09 �C���I��
                        If (intCount <> 1) Then
                            ' �G���[��`���擾�i�Y���f�[�^�͑��ōX�V����Ă��܂��܂����B�ēx����F���������j
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                        End If
                    Else

                    End If
                    '*����ԍ� 000016 2005/11/18 �C���I��

                    '*����ԍ� 000013 2005/06/19 �C���J�n
                End If
                '*����ԍ� 000013 2005/06/19 �C���I��
            End If

            '---------------------------------------------------------------------------------------
            ' 8-3. ���߃��R�[�h���X�V����
            '�@�@�@�@�@���ꏈ���ȊO�̏ꍇ�̂ݍs���B
            '---------------------------------------------------------------------------------------

            '*����ԍ� 000013 2005/06/19 �C���J�n
            '���ꏈ���̔���
            If Not blnTokushuFG Then

                '---------------------------------------------------------------------------------------
                ' 8-3-1. ���O�̗������Z�o�O�A���ē]���̏ꍇ�͒��O�̗����̏Z�o�O�f�[�^�̏I���N���������B
                '---------------------------------------------------------------------------------------

                ' �E�Z����ʂ̉��P�����h0�h�i�Z���j�ł��Z�o�O�L��e�k�f���h1�h�̎��A�Z�o�O�D��Ŏw��N������99999999�ň���
                ' �@�����}�X�^����ݗ����I���N�������V�X�e�����t�̑O�����Z�b�g���A���������}�X�^�X�V�����s����B
                If (((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) = "0") _
                        And blnJutogaiUmu) Then
                    '*����ԍ� 000016 2005/11/18 �C���J�n
                    ' ���t�N���X�̕K�v�Ȑݒ���s��
                    '* corresponds to VS2008 Start 2010/04/16 000043
                    ''''m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
                    ''''m_cfDateClass.p_enEraType = UFEraType.Number
                    ''''cSearchKey = New ABAtenaSearchKey()
                    ''''cSearchKey.p_strJuminCD = strJuminCD
                    ''''cSearchKey.p_strJutogaiYusenKB = "1"
                    ''''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "99999999", True)
                    ''''If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then
                    ''''    csDataRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)
                    ''''    m_cfDateClass.p_strDateValue = CType(csAtenaRow(ABAtenaEntity.RRKST_YMD), String)
                    ''''    csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                    ''''    intCount = m_cAtenaRirekiB.UpdateAtenaRB(csDataRow)
                    ''''    If (intCount <> 1) Then
                    ''''        ' �G���[��`���擾�i�Y���f�[�^�͑��ōX�V����Ă��܂��܂����B�ēx����F���������j
                    ''''        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    ''''        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                    ''''        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    ''''    End If
                    ''''End If
                    '* corresponds to VS2008 End 2010/04/16 000043
                    ' ���������f�[�^���o(�Z�o�O�D��敪��"1"�ŗ����I���N������'99999999')
                    csUpRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("JUTOGAIYUSENKB = '1' AND RRKED_YMD = '99999999'")

                    ' ���߃��R�[�h���擾���A�A�b�v�f�[�g
                    If csUpRirekiRows.Length > 0 Then
                        csUpRirekiRow = csUpRirekiRows(0)
                        m_cfDateClass.p_strDateValue = CType(csAtenaRow(ABAtenaEntity.RRKST_YMD), String)
                        csUpRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                        '* ����ԍ� 000044 2011/11/09 �C���J�n
                        'intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow)

                        '���߈��������f�[�^��ޔ��f�[�^����擾
                        '* ����ԍ� 000047 2011/12/26 �C���J�n
                        'csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, _
                        '                                              csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString, _
                        '                                              csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                        csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(csAtenaRirekiFzyEntity,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                        '* ����ԍ� 000047 2011/12/26 �C���I��

                        '��������W��
                        csCkinRirekiHyojunRows = Me.GetChokkin_RirekiHyojun(csAtenaRirekiHyojunEntity,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                        '��������t���W��
                        csCkinRirekiFzyHyojunRows = Me.GetChokkin_RirekiFZYHyojun(csAtenaRirekiFZYHyojunEntity,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                        csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)

                        intCount = m_cAtenaRirekiB.UpdateAtenaRB(csUpRirekiRow, csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows)
                        '* ����ԍ� 000044 2011/11/09 �C���I��
                        If (intCount <> 1) Then
                            ' �G���[��`���擾�i�Y���f�[�^�͑��ōX�V����Ă��܂��܂����B�ēx����F���������j
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                        End If
                    Else

                    End If

                    '*����ԍ� 000016 2005/11/18 �C���I��
                End If

                '*����ԍ� 000013 2005/06/19 �C���J�n
                '''' ���������}�X�^���Y���҂̑S�������擾����
                '* corresponds to VS2008 Start 2010/04/16 000043
                ''''cSearchKey = New ABAtenaSearchKey()
                ''''cSearchKey.p_strJuminCD = strJuminCD
                ''''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)

                ''''' ���������̗���擾���A����������B�i�X�V�J�E�^�[�́A0�A����ȊO�́AString Empty�j�i���ʁj
                ''''csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow
                ''''Me.ClearAtenaRireki(csAtenaRirekiRow)

                ''''' �����}�X�^��舶�������}�X�^�̕ҏW���s��(����)
                ''''' ����ԍ��@�@�@�V�K�̂΂����́A0001�@�@�C���̏ꍇ�́A���������}�X�^�̍ŏI�ԍ��ɂ`�c�c�@�P����
                ''''' ����ȊO�̍��ڂɂ��ẮA�����}�X�^�����̂܂ܕҏW����			
                ''''If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
                ''''    ' ����ԍ�
                ''''    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = "0001"
                ''''Else
                ''''    ' ����ԍ��ō~���ɕ��ёւ�
                ''''    csAtenaRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
                ''''    ' ����ԍ�(�擪�s�̗���ԍ�+1)
                ''''    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType((CType(csAtenaRirekiRows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).PadLeft(4, "0"c)
                ''''End If
                '* corresponds to VS2008 End 2010/04/16 000043

                '---------------------------------------------------------------------------------------
                ' 8-3-2. �X�V�p�̒��߃��R�[�h���쐬����B
                '---------------------------------------------------------------------------------------

                '�����������E��nothing�̏ꍇ�̓X�L�[�}���擾����
                If csAtenaRirekiRow Is Nothing Then
                    '���������}�X�^�̃X�L�[�}���擾����
                    csAtenaRirekiEntity = m_cfRdbClass.GetTableSchema(ABAtenaRirekiEntity.TABLE_NAME)
                    '�����������E��V�K�쐬����
                    csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow()
                End If '�ő嗚��ԍ����Z�b�g����
                '*����ԍ� 000016 2005/11/01 �폜�J�n
                '* corresponds to VS2008 Start 2010/04/16 000043
                ''''csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = strMaxRirekino
                '* corresponds to VS2008 End 2010/04/16 000043
                '*����ԍ� 000016 2005/11/01 �폜�I��
                '*����ԍ� 000013 2005/06/19 �C���I��

                ' �����}�X�^�����������ւ��̂܂ܕҏW����
                For Each csDataColumn In csAtenaRow.Table.Columns
                    csAtenaRirekiRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
                Next csDataColumn
                '* ����ԍ� 000044 2011/11/09 �ǉ��J�n

                If (csAtenaRirekiFzyRow Is Nothing) Then
                    '��������t���̐V�K�s�쐬
                    csAtenaRirekiFzyEntity = m_cfRdbClass.GetTableSchema(ABAtenaRirekiFZYEntity.TABLE_NAME)
                    csAtenaRirekiFzyRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).NewRow
                Else
                    '�������Ȃ�
                End If

                '�����t������������t���ɃR�s�[
                For Each csColumn As DataColumn In csAtenaFzyRow.Table.Columns
                    csAtenaRirekiFzyRow(csColumn.ColumnName) = csAtenaFzyRow(csColumn.ColumnName)
                Next
                '* ����ԍ� 000044 2011/11/09 �ǉ��I��

                '��������W��
                If (csAtenaRirekiHyojunRow Is Nothing) Then
                    '��������W���̐V�K�s�쐬
                    csAtenaRirekiHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaRirekiHyojunEntity.TABLE_NAME)
                    csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).NewRow
                Else
                    '�������Ȃ�
                End If

                '�����W������������W���ɃR�s�[
                For Each csColumn As DataColumn In csAtenaHyojunRow.Table.Columns
                    If (csAtenaRirekiHyojunRow.Table.Columns.Contains(csColumn.ColumnName)) Then
                        csAtenaRirekiHyojunRow(csColumn.ColumnName) = csAtenaHyojunRow(csColumn.ColumnName)
                    End If
                Next

                '��������t���W��
                If (csAtenaRirekiFZYHyojunRow Is Nothing) Then
                    '��������t���W���̐V�K�s�쐬
                    csAtenaRirekiFZYHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME)
                    csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).NewRow
                Else
                    '�������Ȃ�
                End If

                '�����t���W������������t���W���ɃR�s�[
                For Each csColumn As DataColumn In csAtenaFzyHyojunRow.Table.Columns
                    csAtenaRirekiFZYHyojunRow(csColumn.ColumnName) = csAtenaFzyHyojunRow(csColumn.ColumnName)
                Next

                '*����ԍ� 000012 2005/06/07 �폜�J�n
                '*����ԍ� 000011 2005/06/05 �ǉ��J�n
                ''�����}�X�^�̊J�n���𓖓��ɂ���
                'm_cfDateClass.p_strDateValue = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd") '�V�X�e�����t
                'csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = m_cfDateClass.p_strDay
                '*����ԍ� 000011 2005/06/05 �ǉ��I��
                '*����ԍ� 000012 2005/06/07 �ǉ��I��

                ' ���������}�X�^�̒ǉ����s��
                '*����ԍ� 000013 2005/06/21 �폜�J�n
                'csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csAtenaRirekiRow)
                '*����ԍ� 000013 2005/06/21 �폜�I��
                '*����ԍ� 000016 2005/11/01 �C���J�n
                '* corresponds to VS2008 Start 2010/04/16 000043
                ''''intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)
                ''''If (intCount <> 1) Then
                ''''    ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                ''''    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ''''    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                ''''    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                ''''End If
                '* corresponds to VS2008 End 2010/04/16 000043


                '---------------------------------------------------------------------------------------
                ' 8-3-3. �Z�o�O�f�[�^�A�Z��f�[�^���X�V����B
                '---------------------------------------------------------------------------------------

                ' �Z�o�O���N���Ă���f�[�^�Ł@���@���̏Z�o�O���R�[�h��S�čăZ�b�g���I����ĂȂ��ꍇ
                If m_blnJutogaiAriFG = True AndAlso
                   m_intJutogaiRowCnt > m_intJutogaiInCnt Then

                    '---------------------------------------------------------------------------------------
                    ' 8-3-3-1. �����f�[�^�̍X�V�����őޔ����Ă����Z�o�O�f�[�^���c���Ă��鎞�B
                    ' �@�@�@   �c���Ă���Z�o�O�f�[�^��S�čX�V����B
                    '---------------------------------------------------------------------------------------

                    ' �c��̏Z�o�O���R�[�h���ăZ�b�g���Ă���
                    For intIdx = m_intJutogaiInCnt To m_intJutogaiRowCnt - 1 Step 1
                        intForCnt += 1

                        ' �Z�o�O���R�[�h���c���Ă����Ԃ̂Ƃ�JukiDataKoshin08ҿ��ނŊ��Ɏ擾���Ă���̂�
                        ' ���ڂ̃��[�v�ł͎擾���Ȃ��B
                        If intForCnt > 1 Then
                            m_intJutogaiST_YMD = CType(m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                        End If

                        If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) > m_intJutogaiST_YMD Then

                            ' �A�ԗp�J�E���g���{�P
                            m_intRenbanCnt += 1
                            ' ����ԍ����Z�b�g
                            m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                            '*����ԍ� 000023 2005/12/16 �ǉ��J�n
                            ' �Z��̃��R�[�h���ē]�����R�[�h�̎��ł��Z�o�O�̃��R�[�h�����߃��R�[�h�̏ꍇ
                            ' �I���N�������Z��R�[�h�̊J�n�N�����̈���O�ɃZ�b�g����
                            If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RRemove(0, 1) = "0" AndAlso
                               CType(m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKED_YMD), String) = "99999999" Then

                                m_cfDateClass.p_strDateValue = CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), String)
                                m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)

                            End If
                            '*����ԍ� 000023 2005/12/16 �ǉ��I��
                            '* ����ԍ� 000044 2011/11/09 �C���J�n
                            '' �Z�o�O���E���C���T�[�g
                            'intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(intIdx))

                            '���߈��������f�[�^��ޔ��f�[�^����擾
                            csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                                                                           m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                           m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RIREKINO).ToString)

                            '��������W��
                            csCkinRirekiHyojunRows = Me.GetChokkin_RirekiHyojun(m_csReRirekiHyojunEntity,
                                                                           m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                           m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RIREKINO).ToString)
                            '��������t���W��
                            csCkinRirekiFzyHyojunRows = Me.GetChokkin_RirekiFZYHyojun(m_csRERirekiFZYHyojunEntity,
                                                                           m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                           m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RIREKINO).ToString)

                            intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(intIdx), csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows)
                            '* ����ԍ� 000044 2011/11/09 �C���I��
                            If (intCount <> 1) Then
                                ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                            End If
                        Else
                            If intJukiInCnt = 0 Then
                                ' �Z��f�[�^�̒��߂��C���T�[�g
                                ' �A�ԗp�J�E���g���{�P
                                m_intRenbanCnt += 1
                                ' ����ԍ����Z�b�g
                                csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                                '* corresponds to VS2008 Start 2010/04/16 000043
                                ''*����ԍ� 000020 2005/12/07 �C���J�n
                                '''''*����ԍ� 000018 2005/11/27 �C���J�n
                                '''''If m_blnSaiTenyuFG = True Then
                                ''''If m_blnHenkanFG = False Then
                                ''''    '*����ԍ� 000018 2005/11/27 �C���I��
                                ''''    ' �Z�o�O�D��敪��"1"
                                ''''    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                                ''''    ' �����I���N�������Z�o�O���E�̗����J�n�N�����̈���O�ɃZ�b�g����
                                ''''    m_cfDateClass.p_strDateValue = CType(m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKST_YMD), String)
                                ''''    csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                                ''''End If
                                '* corresponds to VS2008 End 2010/04/16 000043

                                If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RRemove(0, 1) = "0" Then
                                    ' �Z���̎�
                                    ' �Z�o�O�D��敪��"1"
                                    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                                Else
                                    ' �Z���łȂ���
                                    If m_blnHenkanFG = False Then
                                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                                        '*����ԍ� 000023 2005/12/16 �ǉ��J�n
                                        ' �����I���N�������Z�o�O���E�̗����J�n�N�����̈���O�ɃZ�b�g����
                                        m_cfDateClass.p_strDateValue = CType(m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKST_YMD), String)
                                        csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                                        '*����ԍ� 000023 2005/12/16 �ǉ��I��
                                    Else
                                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                                    End If
                                End If
                                '* corresponds to VS2008 Start 2010/04/16 000043
                                ''*����ԍ� 000023 2005/12/16 �폜�J�n
                                ''''' �����I���N�������Z�o�O���E�̗����J�n�N�����̈���O�ɃZ�b�g����
                                ''''m_cfDateClass.p_strDateValue = CType(m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKST_YMD), String)
                                ''''csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                                ''*����ԍ� 000023 2005/12/16 �폜�I��
                                '* corresponds to VS2008 End 2010/04/16 000043
                                '*����ԍ� 000020 2005/12/07 �C���I��

                                '* ����ԍ� 000044 2011/11/09 �C���J�n
                                'intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                                '���߈��������f�[�^��ޔ��f�[�^����擾
                                csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)

                                '��������W��
                                csCkinRirekiHyojunRows = Me.GetChokkin_RirekiHyojun(m_csReRirekiHyojunEntity,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                                '��������t���W��
                                csCkinRirekiFzyHyojunRows = Me.GetChokkin_RirekiFZYHyojun(m_csRERirekiFZYHyojunEntity,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)

                                intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows)
                                '* ����ԍ� 000044 2011/11/09 �C���I��

                                If (intJukiInCnt <> 1) Then
                                    ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                                End If
                            End If

                            ' �Z��f�[�^�̒��߂��C���T�[�g����Ă���
                            ' �ē]���t���O��True�̂Ƃ��Z�o�O���N������Ƃ��s��
                            '*����ԍ� 000018 2005/11/27 �C���J�n
                            'If intJukiInCnt <> 0 AndAlso m_blnSaiTenyuFG = True Then
                            If intJukiInCnt <> 0 AndAlso m_blnHenkanFG = False AndAlso
                                CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RRemove(0, 1) <> "0" Then
                                '*����ԍ� 000018 2005/11/27 �C���I��
                                ' �A�ԗp�J�E���g���{�P
                                m_intRenbanCnt += 1
                                ' ����ԍ����Z�b�g
                                csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)
                                ' �Z�o�O�D��敪��"0"�ɐݒ�
                                csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                                ' �����J�n�N�������Z�o�O���E�̗����J�n�Ɠ���̂��̂ɂ���
                                csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = CType(m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKST_YMD), String)
                                ' �����I���N�������I�[���X�ɐݒ�
                                csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = "99999999"

                                '* ����ԍ� 000044 2011/11/09 �C���J�n
                                'intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                                '���߈��������f�[�^��ޔ��f�[�^����擾
                                csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)

                                '��������W��
                                csCkinRirekiHyojunRows = Me.GetChokkin_RirekiHyojun(m_csReRirekiHyojunEntity,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                                '��������t���W��
                                csCkinRirekiFzyHyojunRows = Me.GetChokkin_RirekiFZYHyojun(m_csRERirekiFZYHyojunEntity,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)

                                intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows)
                                '* ����ԍ� 000044 2011/11/09 �C���I��

                                If (intCount <> 1) Then
                                    ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                                End If

                                '*����ԍ� 000018 2005/11/27 �ǉ��J�n
                                m_blnHenkanFG = True
                                '*����ԍ� 000018 2005/11/27 �ǉ��I��
                            End If

                            ' �Z�o�O���E���C���T�[�g
                            ' �A�ԗp�J�E���g���{�P
                            m_intRenbanCnt += 1
                            ' ����ԍ����Z�b�g
                            m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                            '* ����ԍ� 000044 2011/11/09 �C���J�n
                            'intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(intIdx))

                            '���߈��������f�[�^��ޔ��f�[�^����擾
                            csCkinRirekiFzyRows = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                                                                           m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                           m_csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RIREKINO).ToString)
                            '��������W��
                            csCkinRirekiHyojunRows = Me.GetChokkin_RirekiHyojun(m_csReRirekiHyojunEntity,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                            '��������t���W��
                            csCkinRirekiFzyHyojunRows = Me.GetChokkin_RirekiFZYHyojun(m_csRERirekiFZYHyojunEntity,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                               csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString)

                            intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(intIdx), csCkinRirekiHyojunRows, csCkinRirekiFzyRows, csCkinRirekiFzyHyojunRows)
                            '* ����ԍ� 000044 2011/11/09 �C���I��

                            If (intCount <> 1) Then
                                ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                            End If

                        End If
                    Next intIdx

                    ' �Z��̒��߃��R�[�h���܂��C���T�[�g����Ă��Ȃ���΃C���T�[�g
                    If intJukiInCnt = 0 Then

                        ' �A�ԗp�J�E���g���{�P
                        m_intRenbanCnt += 1
                        ' ����ԍ����Z�b�g
                        csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                        If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RSubstring(1, 1) = "0" Then
                            '*����ԍ� 000020 2005/12/07 �C���J�n
                            ' �f�[�^��ʂ��Z���̎��͏Z�o�O�D��敪��"1"
                            '* corresponds to VS2008 Start 2010/04/16 000043
                            ''''csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                            '* corresponds to VS2008 End 2010/04/16 000043
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                        Else
                            If m_blnHenkanFG = False Then
                                csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                            Else
                                csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                            End If
                            '*����ԍ� 000020 2005/12/07 �C���I��
                        End If

                        '* ����ԍ� 000044 2011/11/09 �C���J�n
                        'intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                        '�����قǎ擾������������t���s�̗���ԍ������������s�̗���ԍ��ŏ㏑��
                        csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                        '��������W��
                        csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                        '��������t���W��
                        csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)

                        '�C���T�[�g
                        intJukiInCnt = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiHyojunRow, csAtenaRirekiFzyRow, csAtenaRirekiFZYHyojunRow)
                        '* ����ԍ� 000044 2011/11/09 �C���I��
                        If (intCount <> 1) Then
                            ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Else

                    '---------------------------------------------------------------------------------------
                    ' 8-3-3-2. ���߂̏Z�o�O�f�[�^�����݂��Ȃ��A�܂��́A
                    ' �@�@�@   �����f�[�^�̍X�V�����őޔ����Ă����Z�o�O�f�[�^���S�čX�V����Ă��鎞�A
                    ' �@�@�@   �Z��f�[�^���X�V����B
                    '---------------------------------------------------------------------------------------

                    ' �Z�o�O���N���Ă��Ȃ��f�[�^�A�Z�o�O�f�[�^���ăZ�b�g���I����Ă���f�[�^�͂��̂܂܃C���T�[�g
                    ' ����ԍ���ݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = strMaxRirekino

                    '*����ԍ� 000020 2005/12/07 �C���J�n
                    '* corresponds to VS2008 Start 2010/04/16 000043
                    ''''*����ԍ� 000018 2005/11/27 �C���J�n
                    ''''If m_blnSaiTenyuFG = True Then
                    ''If m_blnHenkanFG = False Then
                    ''    ' �ē]�����N���Ă���ꍇ�ɂ͏Z�o�O�D��敪��"1"
                    ''    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                    ''Else
                    ''    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                    ''End If
                    ''''*����ԍ� 000018 2005/11/27 �C���I��
                    '* corresponds to VS2008 End 2010/04/16 000043

                    If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RRemove(0, 1) = "0" Then
                        ' ��ʂ�"*0"�̏ꍇ�͖������ŏZ�o�O�D��敪��"1"
                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                        m_blnHenkanFG = False
                    Else
                        ' ��ʂ�"*0"�łȂ��Ƃ�
                        '*����ԍ� 000023 2005/12/16 �C���J�n
                        ''If m_blnHenkanFG = False Then
                        ''    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                        ''Else
                        ''    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                        ''End If
                        If m_blnHenkanFG = True Then
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                        Else
                            If blnJutogaiUmu = True Then
                                csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                            Else
                                csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                            End If
                        End If
                        '*����ԍ� 000023 2005/12/16 �C���I��
                    End If
                    '*����ԍ� 000020 2005/12/07 �C���I��

                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                    '�����قǎ擾������������t���s�̗���ԍ������������s�̗���ԍ��ŏ㏑��
                    csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                    '��������W��
                    csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                    '��������t���W��
                    csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)

                    '�C���T�[�g
                    intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiHyojunRow, csAtenaRirekiFzyRow, csAtenaRirekiFZYHyojunRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��

                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If
                End If
                '*����ԍ� 000016 2005/11/01 �C���J�n

                '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
            Else
                If ((csAtenaRirekiFzyTokushuRow Is Nothing) AndAlso (blnAfterSekobi)) Then
                    '��������t�����ꂪ���݂��Ȃ����{�s���ȍ~�̎��A�����t������쐬
                    csAtenaRirekiFzyTokushuRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).NewRow
                    csAtenaRirekiFzyTokushuRow = Me.SetAtenaRirekiFzy(csAtenaRirekiFzyTokushuRow, csAtenaFzyRow)
                    '����ԍ��E�X�V�����𒼋߈�������t�����擾
                    csAtenaRirekiFzyTokushuRow(ABAtenaRirekiEntity.RIREKINO) = csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                    csAtenaRirekiFzyTokushuRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = csUpRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI)
                    '�C���T�[�g
                    m_cAtenaRirekiFzyB.InsertAtenaFZYRB(csAtenaRirekiFzyTokushuRow)
                Else
                    '�������Ȃ�
                End If
                If ((csAtenaRirekiFzyHyojunTokushuRow Is Nothing) AndAlso (blnAfterSekobi)) Then
                    '��������t���W�����ꂪ���݂��Ȃ����{�s���ȍ~�̎��A�����t���W������쐬
                    csAtenaRirekiFzyHyojunTokushuRow = csAtenaRirekiFZYHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).NewRow
                    csAtenaRirekiFzyHyojunTokushuRow = Me.SetAtenaRirekiFZYHyojun(csAtenaRirekiFzyHyojunTokushuRow, csAtenaFzyHyojunRow)
                    '����ԍ��E�X�V�����𒼋߈�������t�����擾
                    csAtenaRirekiFzyHyojunTokushuRow(ABAtenaRirekiFZYHyojunEntity.RIREKINO) = csUpRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                    csAtenaRirekiFzyHyojunTokushuRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csUpRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI)
                    '�C���T�[�g
                    m_cABAtenaRirekiFZYHyojunB.InsertAtenaRirekiFZYHyojunB(csAtenaRirekiFzyHyojunTokushuRow)
                Else
                    '�������Ȃ�
                End If
                '* ����ԍ� 000044 2011/11/09 �ǉ��I��
            End If
            '*����ԍ� 000013 2005/06/19 �C���I��



            '---------------------------------------------------------------------------------------
            ' 9. �����ݐσ}�X�^�̍X�V�@�i��j
            '�@�@�@�@�@����C���i03�A04�j�̏ꍇ�͍X�V�f�[�^���قȂ�B
            '---------------------------------------------------------------------------------------
            '**
            '* �����ݐρi��j
            '*
            ' �����ݐς̗���擾���A����������B�i�X�V�J�E�^�[�́A0�A����ȊO�́AString Empty�j�i���ʁj�@			
            ' �����ݐς��V����Row���擾����
            csAtenaRuisekiEntity = m_csAtenaRuisekiEntity.Clone
            csAtenaRuisekiRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
            ' ��������������������
            Me.ClearAtenaRuiseki(csAtenaRuisekiRow)

            ' ���������}�X�^��舶���ݐσ}�X�^�̕ҏW���s��(����)
            ' ��������=�V�X�e������
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI) = StrShoriNichiji

            ' �O��敪=2
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB) = "2"

            '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
            '�����ݐϕt���s���쐬
            csAtenaRuisekiFzyRow = Me.m_csAtenaRuisekiFzyEntity.Tables(ABAtenaRuisekiFZYEntity.TABLE_NAME).NewRow
            Me.ClearAtenaFZY(csAtenaRuisekiFzyRow)
            '���������ƑO��敪�͈����ݐς���擾
            csAtenaRuisekiFzyRow(ABAtenaRuisekiFZYEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI)
            csAtenaRuisekiFzyRow(ABAtenaRuisekiFZYEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB)
            '* ����ԍ� 000044 2011/11/09 �ǉ��I��

            '�����ݐϕW��
            csAtenaRuisekiHyojunRow = Me.m_csAtenaRuisekiHyojunEntity.Tables(ABAtenaRuisekiHyojunEntity.TABLE_NAME).NewRow
            Me.ClearAtenaHyojun(csAtenaRuisekiHyojunRow)
            '���������ƑO��敪�͈����ݐς���擾
            csAtenaRuisekiHyojunRow(ABAtenaRuisekiHyojunEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI)
            csAtenaRuisekiHyojunRow(ABAtenaRuisekiHyojunEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB)

            '�����ݐϕt���W��
            csAtenaRuisekiFZYHyojunRow = Me.m_csAtenaRuisekiFZYHyojunEntity.Tables(ABAtenaRuisekiFZYHyojunEntity.TABLE_NAME).NewRow
            Me.ClearAtenaFZYHyojun(csAtenaRuisekiFZYHyojunRow)
            '���������ƑO��敪�͈����ݐς���擾
            csAtenaRuisekiFZYHyojunRow(ABAtenaRuisekiFZYHyojunEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI)
            csAtenaRuisekiFZYHyojunRow(ABAtenaRuisekiFZYHyojunEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB)

            '*����ԍ� 000013 2005/06/19 �C���J�n
            ' ����ȊO�̍��ڂɂ��ẮA�����}�X�^�����̂܂ܕҏW����			
            ' �������������������ւ��̂܂ܕҏW����
            '*����ԍ� 000026 2005/12/18 �C���J�n
            ' �������R�R�[�h��"03"(���ꏈ���C��)�@�܂��́@"04"(�Z���[�b�c�C��)�̏ꍇ��
            ' �ʂ̃��E��ݐ�(��)�ɔ��f������
            ''For Each csDataColumn In csAtenaRirekiRow.Table.Columns
            ''    csAtenaRuisekiRow(csDataColumn.ColumnName) = csAtenaRirekiRow(csDataColumn)
            ''Next csDataColumn
            '*����ԍ� 000042 2009/08/10 �C���J�n
            'If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
            '   CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" Then
            '* ����ԍ� 000050 2014/06/25 �C���J�n
            'If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
            '   CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse _
            '   (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08") Then
            '* ����ԍ� 000058 2015/10/14 �C���J�n
            'If CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse _
            '   CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse _
            '   CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "05" OrElse _
            '   (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08") Then
            If (blnIsCreateAtenaRireki = False _
                AndAlso (CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "03" OrElse
                         CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "04" OrElse
                         CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "05" OrElse
                         (m_blnRirekiShusei = False AndAlso CType(csJukiDataRow(ABJukiData.SHORIJIYUCD), String) = "08"))) Then
                '* ����ԍ� 000058 2015/10/14 �C���I��
                '* ����ԍ� 000050 2014/06/25 �C���I��

                For Each csDataColumn In csUpRirekiRow.Table.Columns
                    csAtenaRuisekiRow(csDataColumn.ColumnName) = csUpRirekiRow(csDataColumn)
                Next csDataColumn

                '�����ݐϕW��
                csAtenaRuisekiHyojunRow = SetAtenaRuisekiHyojun(csAtenaRuisekiHyojunRow, csAtenaRirekiHyojunTokushuRow, csAtenaRuisekiRow)

                '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                If (blnAfterSekobi) Then
                    '�{�s���ȍ~�̎��A�����ݐϕt�����ꂩ��S���ڃR�s�[
                    csAtenaRuisekiFzyRow = Me.SetAtenaRuisekiFzy(csAtenaRuisekiFzyRow, csAtenaRirekiFzyTokushuRow, csAtenaRuisekiRow)

                    '�����ݐϕt���W��
                    csAtenaRuisekiFZYHyojunRow = SetAtenaRuisekiFZYHyojun(csAtenaRuisekiFZYHyojunRow, csAtenaRirekiFzyHyojunTokushuRow, csAtenaRuisekiRow)
                Else
                    '�{�s���ȑO�͕t����Nothing�ɂ��Ēǉ����Ȃ�
                    csAtenaRuisekiFzyRow = Nothing
                    csAtenaRuisekiFZYHyojunRow = Nothing
                End If
                '* ����ԍ� 000044 2011/11/09 �ǉ��I��
            Else
                For Each csDataColumn In csAtenaRirekiRow.Table.Columns
                    csAtenaRuisekiRow(csDataColumn.ColumnName) = csAtenaRirekiRow(csDataColumn)
                Next csDataColumn

                '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                '��������t������R�s�[
                csAtenaRuisekiFzyRow = Me.SetAtenaRuisekiFzy(csAtenaRuisekiFzyRow, csAtenaRirekiFzyRow, csAtenaRuisekiRow)
                '* ����ԍ� 000044 2011/11/09 �ǉ��I��
                '�����ݐϕW��
                csAtenaRuisekiHyojunRow = SetAtenaRuisekiHyojun(csAtenaRuisekiHyojunRow, csAtenaRirekiHyojunRow, csAtenaRuisekiRow)
                '�����ݐϕt���W��
                csAtenaRuisekiFZYHyojunRow = SetAtenaRuisekiFZYHyojun(csAtenaRuisekiFZYHyojunRow, csAtenaRirekiFZYHyojunRow, csAtenaRuisekiRow)
            End If
            '*����ԍ� 000042 2009/08/10 �C���I��
            '*����ԍ� 000026 2005/12/18 �C���I��
            'For Each csDataColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
            '    csAtenaRuisekiRow(csDataColumn.ColumnName) = csAtenaRirekiRow(csDataColumn)
            'Next csDataColumn
            '*����ԍ� 000013 2005/06/19 �C���I��

            '*����ԍ� 000014 2005/08/17 �ǉ��J�n 000029 2006/04/19 �C���J�n
            ' �������R�b�c�������ݐς�RESERCE�ɃZ�b�g����
            '* ����ԍ� 000058 2015/10/14 �C���J�n
            ' �����������쐬����i���ꏈ���̏ꍇ�ɓ���Ƃ��āj�́A�u41�F�E���C���v���Œ�Ń��U�[�u��o�^����
            'csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
            If (blnIsCreateAtenaRireki = True) Then
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00")
            Else
                csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
            End If
            '* ����ԍ� 000058 2015/10/14 �C���I��
            '' �ėp�b�c�������ݐς�RESERCE�ɃZ�b�g����
            'csAtenaRuisekiRow(ABAtenaRuisekiEntity.RESERCE) = csJukiDataRow(ABJukiData.HANYOCD)
            '*����ԍ� 000014 2005/08/17 �ǉ��I�� 000029 2006/04/19 �C���I��

            '*����ԍ� 000016 2005/11/01 �ǉ��J�n   000028 2005/12/27 �폜�J�n
            ' �������R�R�[�h�������ݐς�CKINJIYUCD�ɃZ�b�g����
            ''csAtenaRuisekiRow(ABAtenaRuisekiEntity.CKINJIYUCD) = csJukiDataRow(ABJukiData.SHORIJIYUCD)
            '*����ԍ� 000016 2005/11/01 �ǉ��I��   000028 2005/12/27 �폜�I��

            '*����ԍ� 000003 2003/11/21 �ǉ��J�n
            ' �����N�����擾����
            csAtenaNenkinEntity = m_cAtenaNenkinB.GetAtenaNenkin(strJuminCD)
            If (csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows.Count > 0) Then
                ' �����ݐϐݒ�(�����N��)
                Me.SetNenkinToRuiseki(csAtenaNenkinEntity.Tables(ABAtenaNenkinEntity.TABLE_NAME).Rows(0), csAtenaRuisekiRow)
            End If
            ' �������ۂ��擾����
            csAtenaKokuhoEntity = m_cAtenaKokuhoB.GetAtenaKokuho(strJuminCD)
            If (csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows.Count > 0) Then
                ' �����ݐϐݒ�(��������)
                Me.SetKokuhoToRuiseki(csAtenaKokuhoEntity.Tables(ABAtenaKokuhoEntity.TABLE_NAME).Rows(0), csAtenaRuisekiRow)
            End If
            '*����ԍ� 000003 2003/11/21 �ǉ��I��

            ' �����ݐς֒ǉ�����
            csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csAtenaRuisekiRow)

            ' �����ݐσ}�X�^�̒ǉ����s��
            '* ����ԍ� 000044 2011/11/09 �C���J�n
            'intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow)

            intCount = m_cAtenaRuisekiB.InsertAtenaRB(csAtenaRuisekiRow, csAtenaRuisekiHyojunRow, csAtenaRuisekiFzyRow, csAtenaRuisekiFZYHyojunRow)
            '* ����ԍ� 000044 2011/11/09 �C���I��
            If (intCount <> 1) Then
                ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F�����ݐρj
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�����ݐ�", objErrorStruct.m_strErrorCode)
            End If

            '* ����ԍ� 000050 2014/06/25 �ǉ��J�n
            '---------------------------------------------------------------------------------------
            ' x. ���ʔԍ��}�X�^�̍X�V
            '---------------------------------------------------------------------------------------
            ' ���ʔԍ��}�X�^�X�V����
            If (Me.IsUpdateMyNumber(csJukiDataRow) = True) Then

                ' ���ʔԍ��E�����ʔԍ��̎擾�i�����j
                a_strMyNumber = GetMyNumber(csJukiDataRow)

                ' ���ʔԍ��p�����[�^�[�̐ݒ�
                cABMyNumberPrm = Me.SetMyNumber(csJukiDataRow, a_strMyNumber(ABMyNumberType.New))

                ' ���ʔԍ��}�X�^�X�V
                'Select Case csJukiDataRow.Item(ABJukiData.SHORIJIYUCD).ToString
                '    Case ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00")
                '        ' �y���ꏈ���z
                '        '* ����ԍ� 000057 2015/02/17 �C���J�n
                '        'Me.UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji, a_strMyNumber(ABMyNumberType.Old), IsJumin(csJukiDataRow))
                '        Me.UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji, a_strMyNumber(ABMyNumberType.Old))
                '        '* ����ԍ� 000057 2015/02/17 �C���I��
                '    Case Else
                ' �y�ʏ폈���z
                '* ����ԍ� 000054 2014/12/26 �C���J�n
                'Me.UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji)
                '* ����ԍ� 000056 2015/01/28 �C���J�n
                'Me.UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji, IsJumin(csJukiDataRow))
                Me.UpdateMyNumber(cABMyNumberPrm, StrShoriNichiji)
                '* ����ԍ� 000056 2015/01/28 �C���I��
                '* ����ԍ� 000054 2014/12/26 �C���I��
                '    End Select

            Else
                ' noop
            End If
            '* ����ԍ� 000050 2014/06/25 �ǉ��I��

            '---------------------------------------------------------------------------------------
            ' 10. �Œ莑�Y�ŃV�X�e���ւ̘A�g
            '�@�@�@�@�@�Ǘ����ɂ��A�g�𐧌䂷��B�i04.12�j
            '---------------------------------------------------------------------------------------
            '*����ԍ� 000006 2004/08/27 �C���J�n
            '*����ԍ� 000009 2005/03/18 �C���J�n
            '�Ǘ����̌Œ�A�g���R�[�h�����݂��Ȃ����ƁA�p�����[�^���g0�h�̎��ɌŒ�A�g�������s��
            If m_strKoteiRenkeiFG Is Nothing OrElse m_strKoteiRenkeiFG = "0" Then
                '�Œ�A���N���X��nothing�Ȃ�C���X�^���X�����s��
                If m_cBAAtenaLinkageBClass Is Nothing Then
                    '�Œ�A���N���X�̃C���X�^���X�����s��
                    m_cBAAtenaLinkageBClass = New BAAtenaLinkageBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    m_cBAAtenaLinkageIFXClass = New BAAtenaLinkageIFXClass
                End If
                '''''''''' �����Ǘ����a�N���X�̃C���X�^���X�쐬
                '''''''''cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ''''''''''  �����Ǘ����̎��04���ʃL�[01�̃f�[�^��S���擾����
                '''''''''csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "12")

                ''''''''''�Ǘ����̌Œ�A�g���R�[�h�����݂��A�p�����[�^���g�P�h�̎��ɂ͌Œ�A�g�������s�Ȃ�Ȃ�
                '''''''''If (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) OrElse _
                '''''''''     CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "0" Then
                '*����ԍ� 000009 2005/03/18 �C���I��

                '*����ԍ� 000005 2004/03/09 �ǉ��J�n
                '�@�Œ莑�Y�Ńf�[�^�n�����s�Ȃ�
                If Not (blnJukiUmu) Then
                    m_cBAAtenaLinkageIFXClass.ShichosonCD = CType(csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD), String)
                    m_cBAAtenaLinkageIFXClass.JuminCD = CType(csAtenaRow(ABAtenaEntity.JUMINCD), String)
                    '*����ԍ� 000010 2005/04/04 �C���J�n
                    'm_cBAAtenaLinkageIFXClass.IdoYMD = CType(csAtenaRow(ABAtenaEntity.CKINIDOYMD), String)
                    If CType(csAtenaRow(ABAtenaEntity.CKINIDOYMD), String).Trim = String.Empty Then
                        m_cBAAtenaLinkageIFXClass.IdoYMD = "00000000"
                    Else
                        m_cBAAtenaLinkageIFXClass.IdoYMD = CType(csAtenaRow(ABAtenaEntity.CKINIDOYMD), String)
                    End If
                    '*����ԍ� 000010 2005/04/04 �C���I��
                    '*����ԍ� 000007 2004/10/20 �C���J�n
                    m_cBAAtenaLinkageIFXClass.KjnHjnKB = CType(csAtenaRow(ABAtenaEntity.KJNHJNKB), String)
                    ''''cBAAtenaLinkageIFXClass.KjnHjnKB = CType(csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB), String)
                    '*����ԍ� 000007 2004/10/20 �C���I��
                    BlnRcd = m_cBAAtenaLinkageBClass.BAAtenaLinkage(m_cBAAtenaLinkageIFXClass)
                    If BlnRcd = False Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾�i�Y���f�[�^�͏����ł��܂���B�F�Œ莑�Y�Łj
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001046)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Œ莑�Y��", objErrorStruct.m_strErrorCode)
                    End If
                Else
                    '*����ԍ� 000008 2005/02/15     �ǉ��J�n
                    m_cBAAtenaLinkageIFXClass.ShichosonCD = CType(csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD), String)
                    m_cBAAtenaLinkageIFXClass.JuminCD = CType(csAtenaRow(ABAtenaEntity.JUMINCD), String)
                    '*����ԍ� 000010 2005/04/04 �C���J�n
                    'm_cBAAtenaLinkageIFXClass.IdoYMD = CType(csAtenaRow(ABAtenaEntity.CKINIDOYMD), String)
                    If CType(csAtenaRow(ABAtenaEntity.CKINIDOYMD), String).Trim = String.Empty Then
                        m_cBAAtenaLinkageIFXClass.IdoYMD = "00000000"
                    Else
                        m_cBAAtenaLinkageIFXClass.IdoYMD = CType(csAtenaRow(ABAtenaEntity.CKINIDOYMD), String)
                    End If
                    '*����ԍ� 000010 2005/04/04 �C���I��
                    m_cBAAtenaLinkageIFXClass.KjnHjnKB = CType(csAtenaRow(ABAtenaEntity.KJNHJNKB), String)
                    BlnRcd = m_cBAAtenaLinkageBClass.BAAtenaLinkage_IR(m_cBAAtenaLinkageIFXClass)
                    If BlnRcd = False Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾�i�Y���f�[�^�͏����ł��܂���B�F�Œ莑�Y�Łj
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001046)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Œ莑�Y��", objErrorStruct.m_strErrorCode)
                    End If
                    '*����ԍ� 000008 2005/02/15     �ǉ��I��
                End If
                '*����ԍ� 000005 2004/03/09 �ǉ��I��

            End If
            '*����ԍ� 000006 2004/08/27 �C���I��


            '*����ԍ� 000004 2004/02/16 �ǉ��J�n   000009 2005/02/28 �폜�J�n
            '**
            '* ���[�N�t���[����(�p�����[�^�i�[)
            '*
            ''''''''''' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            '''''''''cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            ''''''''  �����Ǘ����̎��04���ʃL�[01�̃f�[�^��S���擾����
            '''''''csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "01")

            ''''''''�Ǘ����̃��[�N�t���[���R�[�h�����݂��A�p�����[�^��"1"�̎��������[�N�t���[�������s��
            '''''''If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) Then
            '''''''    If CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" Then

            '''''''        '�Z�o�OFLG��"1"�łȂ��ėp�敪��"02","10","11","12","14","15"�ŗ����I���N������"99999999"�i���߃f�[�^�j�̏ꍇ
            '''''''        If Not (blnJutogaiUmu) And _
            '''''''            (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "02" Or _
            '''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "10" Or _
            '''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "11" Or _
            '''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "12" Or _
            '''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "14" Or _
            '''''''            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "15") And _
            '''''''            CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then
            '''''''            '�C���X�^���X��
            '''''''            m_ABToshoProperty(m_intCnt) = New ABToshoProperty()
            '''''''            '�Z���R�[�h���v���p�e�B�ɃZ�b�g
            '''''''            m_ABToshoProperty(m_intCnt).p_strJuminCD = strJuminCD
            '''''''            '�X�V�敪���v���p�e�B�ɃZ�b�g�i�ǉ�:1 �C��:2 �폜:D�j
            '''''''            m_ABToshoProperty(m_intCnt).p_strKoshinKB = "1"
            '''''''            '�J�E���^�[��1�v���X
            '''''''            m_intCnt += 1

            '''''''        ElseIf CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then
            '''''''            '�C���X�^���X��
            '''''''            m_ABToshoProperty(m_intCnt) = New ABToshoProperty()
            '''''''            '�Z���R�[�h���v���p�e�B�ɃZ�b�g
            '''''''            m_ABToshoProperty(m_intCnt).p_strJuminCD = strJuminCD
            '''''''            '�X�V�敪���v���p�e�B�ɃZ�b�g�i�ǉ�:1 �C��:2 �폜:D�j
            '''''''            m_ABToshoProperty(m_intCnt).p_strKoshinKB = "2"
            '''''''            '�J�E���^�[��1�v���X
            '''''''            m_intCnt += 1

            '''''''        End If

            '''''''    End If
            '''''''End If
            '*����ԍ� 000004 2004/02/16 �ǉ��I��   000009 2005/02/28 �폜�I��
            '*����ԍ� 000065 2024/04/02 �ǉ��J�n
            ' �l����̍X�V
            UpdateKojinSeigyo(csJukiDataRow)
            '*����ԍ� 000065 2024/04/02 �ǉ��I��

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
            Throw objExp
        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �Z��f�[�^�X�V�i�����j
    '* 
    '* �\��           Public Sub JukiDataKoshin08() 
    '* 
    '* �@�\ �@    �@�@�Z����f�[�^���X�V����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub JukiDataKoshin08(ByVal csJukiDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "JukiDataKoshin08"
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        '*����ԍ� 000040 2009/05/22 �폜�J�n
        'Dim blnJutogaiUmu As Boolean                        ' �Z�o�O�L��FLG
        '*����ԍ� 000040 2009/05/22 �폜�I��
        Dim blnJukiUmu As Boolean                           ' �Z��L��FLG
        Dim strJuminCD As String                            ' �Z���R�[�h
        '*����ԍ� 000040 2009/05/22 �폜�J�n
        'Dim csJutogaiEntity As DataSet                      ' �Z�o�ODataSet
        '*����ԍ� 000040 2009/05/22 �폜�I��
        Dim cSearchKey As ABAtenaSearchKey                  ' ���������L�[
        Dim csAtenaEntity As DataSet                        ' �����}�X�^Entity
        Dim csAtenaRow As DataRow                           ' �����}�X�^Row
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csDataRow As DataRow                            ' �c�������q����
        'Dim csDataSet As DataSet                            ' �c�������r����
        '* corresponds to VS2008 End 2010/04/16 000043
        Dim csDataColumn As DataColumn                      ' �c�������b����������
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csAtenaRirekiEntity As DataSet                  ' ��������DataSet
        'Dim csAtenaRirekiRows() As DataRow                  ' ��������Rows
        '* corresponds to VS2008 End 2010/04/16 000043
        Dim csAtenaRirekiRow As DataRow                     ' ��������Row
        Dim intCount As Integer                             ' �X�V����
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csAtenaRuisekiEntity As DataSet                 ' �����ݐ�DataSet
        'Dim csAtenaRuisekiRow As DataRow                    ' �����ݐ�Row
        '* corresponds to VS2008 End 2010/04/16 000043
        '*����ԍ� 000003 2003/11/21 �ǉ��J�n
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csAtenaNenkinEntity As DataSet                  ' �����N��DataSet
        'Dim csAtenaKokuhoEntity As DataSet                  ' ��������DataSet
        '* corresponds to VS2008 End 2010/04/16 000043
        '*����ԍ� 000003 2003/11/21 �ǉ��I��
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim StrShoriNichiji As String
        '* corresponds to VS2008 End 2010/04/16 000043
        '*����ԍ� 000016 2005/11/01 �ǉ��J�n
        Dim intYMD As Integer
        Dim intIdx As Integer
        '*����ԍ� 000016 2005/11/01 �ǉ��I��
        '*����ԍ� 000031 2007/01/30 �ǉ��J�n
        Dim strBanchiCD() As String                         ' �Ԓn�R�[�h�擾�p�z��
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim strMotoBanchiCD() As String                     ' �ύX�O�Ԓn�R�[�h
        'Dim intLoop As Integer                              ' ���[�v�J�E���^
        '* corresponds to VS2008 End 2010/04/16 000043
        '*����ԍ� 000031 2007/01/30 �ǉ��I��
        '*����ԍ� 000036 2007/09/28 �ǉ��J�n
        Dim cHenshuSearchKana As ABHenshuSearchShimeiBClass ' �����p�J�i�����N���X
        Dim strSearchKana(4) As String                      ' �����p�J�i���̗p
        '*����ԍ� 000036 2007/09/28 �ǉ��I��
        '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
        Dim csAtenaFzyEntity As DataSet                     ' �����t���f�[�^
        Dim csAtenaFzyRow As DataRow                        ' �����t���s
        Dim csAtenaRirekiFzyRow As DataRow                  ' ��������t���s
        Dim csAtenaRirekiFzyJugaiRow As DataRow             ' ��������t���s�i�Z�o�O�j
        '* ����ԍ� 000044 2011/11/09 �ǉ��I��
        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            '---------------------------------------------------------------------------------------
            ' 1. �ϐ��̏�����
            '
            '---------------------------------------------------------------------------------------
            ' �ϐ��̏�����
            '*����ԍ� 000040 2009/05/22 �폜�J�n
            'blnJutogaiUmu = False           '�Z�o�O�f�[�^�����݂��Ă���ꍇ��True
            '*����ԍ� 000040 2009/05/22 �폜�I��
            blnJukiUmu = False              '�Z��f�[�^�����݂��Ă���ꍇ��True
            strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString    '�Ώۃf�[�^�̏Z���R�[�h���擾

            '*����ԍ� 000036 2007/09/28 �ǉ��J�n
            ' �����p�J�i�����N���X�C���X�^���X��
            cHenshuSearchKana = New ABHenshuSearchShimeiBClass(m_cfControlData, m_cfConfigDataClass)
            '*����ԍ� 000036 2007/09/28 �ǉ��I��



            '---------------------------------------------------------------------------------------
            ' 2. �Z�o�O�f�[�^�̑��݃`�F�b�N
            '�@�@�@�@�@���߂̏Z�o�O�f�[�^�����݂��Ă��邩�Z�o�O�}�X�^����擾����B
            '---------------------------------------------------------------------------------------
            '*����ԍ� 000040 2009/05/22 �폜�J�n
            '' �Z���R�[�h�ŏZ�o�O�}�X�^���擾����i���݂���ꍇ�́A�Z�o�O�L��e�k�f�Ɂh1�h���Z�b�g�j
            'csJutogaiEntity = m_cJutogaiB.GetJutogaiBHoshu(strJuminCD, True)
            'If (csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows.Count > 0) Then
            '    blnJutogaiUmu = True
            'End If
            '*����ԍ� 000040 2009/05/22 �폜�I��

            ' �Z����ʂ̉��P�����h0�h�i�Z���j�ł��Z�o�O�L��e�k�f���h1�h�̎�
            ' �E�Z�o�O�f�[�^���폜����
            ' �E�Z�o�O�D��Ŏw��N�����h99999999�h�ň����}�X�^���擾���A���̃f�[�^���폜����
            'If (((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").Substring(1, 1) = "0") _
            '        And blnJutogaiUmu) Then
            '    For Each csDataRow In csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows
            '        m_cJutogaiB.DeleteJutogaiB(csDataRow, "D")
            '    Next csDataRow
            '    cSearchKey = New ABAtenaSearchKey()
            '    cSearchKey.p_strJuminCD = strJuminCD
            '    cSearchKey.p_strJutogaiYusenKB = "1"
            '    csAtenaEntity = m_cAtenaB.GetAtenaBHoshu(1, cSearchKey, True)
            '    For Each csDataRow In csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows
            '        m_cAtenaB.DeleteAtenaB(csDataRow, "D")
            '    Next csDataRow
            'End If



            '---------------------------------------------------------------------------------------
            ' 3. �Z��f�[�^�̑��݃`�F�b�N
            '�@�@�@�@�@���߂̏Z��f�[�^�����݂��Ă��邩�����}�X�^����擾����B
            '---------------------------------------------------------------------------------------
            ' �Z��D��ň����}�X�^���擾����i���݂���ꍇ�́A�Z��L��e�k�f�Ɂh1�h���Z�b�g�j
            ' ���������L�[�̃C���X�^���X��
            cSearchKey = New ABAtenaSearchKey
            cSearchKey.p_strJuminCD = strJuminCD
            cSearchKey.p_strJuminYuseniKB = "1"
            csAtenaEntity = m_cAtenaB.GetAtenaBHoshu(1, cSearchKey, True)
            If (csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count > 0) Then
                blnJukiUmu = True
                '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                '�����t���f�[�^�擾�i�Z���Z�o�O�敪�͈�������擾�j
                csAtenaFzyEntity = Me.m_cAtenaFzyB.GetAtenaFZYBHoshu(strJuminCD,
                                                                     csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB).ToString,
                                                                     True)
            Else
                '�����t���f�[�^�擾�i�Z���Z�o�O�敪��String.Empty�j
                csAtenaFzyEntity = Me.m_cAtenaFzyB.GetAtenaFZYBHoshu(strJuminCD, String.Empty, True)
                '* ����ԍ� 000044 2011/11/09 �ǉ��I��
            End If



            '---------------------------------------------------------------------------------------
            ' 4. �f�[�^�̕ҏW
            '�@�@�@�@�@���߂̏Z��f�[�^�����݂��Ă���ꍇ�͏C���A���Ă��Ȃ���Βǉ��ƂȂ�B
            '�@�@�@�@�@�Z��C�A�E�g���父�����C�A�E�g�ɂ���B
            '---------------------------------------------------------------------------------------
            ' �����}�X�^

            ' �����}�X�^�̗���擾���A����������B�i�X�V�J�E�^�[�́A0�A����ȊO�́AString Empty�j�i���ʁj
            If (blnJukiUmu) Then
                csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)
            Else
                csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).NewRow
                Me.ClearAtena(csAtenaRow)
            End If

            ' �Z��f�[�^��舶���}�X�^�̕ҏW���s���i�`�k�k�D�m�t�k�k���́A�`�k�k�X�y�[�X�̎��́AString.Empty�ɂ��āj
            For Each csDataColumn In csJukiDataRow.Table.Columns
                If (IsDBNull(csJukiDataRow(csDataColumn))) _
                        OrElse (CType(csJukiDataRow(csDataColumn), String).Trim = String.Empty) Then
                    csJukiDataRow(csDataColumn) = String.Empty
                End If
            Next csDataColumn

            ' �Z��f�[�^�̓��ꍀ�ڂ������}�X�^�̍��ڂɃZ�b�g����
            ' �E�Z���R�[�h
            csAtenaRow(ABAtenaEntity.JUMINCD) = csJukiDataRow(ABJukiData.JUMINCD)
            ' �E�s�����R�[�h
            csAtenaRow(ABAtenaEntity.SHICHOSONCD) = csJukiDataRow(ABJukiData.SHICHOSONCD)
            ' �E���s�����R�[�h
            csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD) = csJukiDataRow(ABJukiData.KYUSHICHOSONCD)

            ' �����Z�b�g���Ȃ�����
            ' �E�Z���[�R�[�h
            ' �E�ėp�敪�Q
            ' �E�����@�l�`��
            ' �E�����@�l��\�Ҏ���
            ' �E�Ɖ��~�敪
            ' �E���l�Ŗ�

            ' �ҏW���ăZ�b�g���鍀��
            ' �E�Z���Z�o�O�敪   1
            csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB) = "1"
            ' �E�Z���D��敪     1
            csAtenaRow(ABAtenaEntity.JUMINYUSENIKB) = "1"
            ' �E�Z�o�O�D��敪
            ' �@�@�Z����ʂ̉��P�����h0�h�i�Z���j�łȂ��A���Z�o�O�L��e�k�f���h1�h�̎��A�@0
            '*����ԍ� 000040 2009/05/22 �C���J�n
            '�Ƃ肠������������ "1" �Ƃ��ăZ�b�g����
            csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1"
            'If (((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").Substring(1, 1) <> "0") _
            '        And blnJutogaiUmu) Then
            '    csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "0"
            'Else
            '    '   �@��L�ȊO       1
            '    csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1"
            'End If
            '*����ԍ� 000040 2009/05/22 �C���I��
            ' �E�����f�[�^�敪=(11)
            csAtenaRow(ABAtenaEntity.ATENADATAKB) = "11"
            ' �E���уR�[�h�`�����ԍ�
            csAtenaRow(ABAtenaEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD)
            'csAtenaRow(ABAtenaEntity.JUMINHYOCD) = String.Empty
            csAtenaRow(ABAtenaEntity.SEIRINO) = csJukiDataRow(ABJukiData.SEIRINO)
            ' �E�����f�[�^���=(�Z�����)
            csAtenaRow(ABAtenaEntity.ATENADATASHU) = csJukiDataRow(ABJukiData.JUMINSHU)
            ' �E�ėp�敪�P=(�ʂ��敪)
            csAtenaRow(ABAtenaEntity.HANYOKB1) = csJukiDataRow(ABJukiData.UTSUSHIKB)
            ' �E�l�@�l�敪=(1)
            csAtenaRow(ABAtenaEntity.KJNHJNKB) = "1"
            ' �E�ėp�敪�Q
            'csAtenaRow(ABAtenaEntity.HANYOKB2) = String.Empty

            '*����ԍ� 000037 2008/05/12 �폜�J�n
            '* corresponds to VS2008 Start 2010/04/16 000043
            '''' �E�Ǔ��ǊO�敪
            '''' �@�@�Z����ʂ̉��P�����h8�h�i�]�o�ҁj�̏ꍇ�A�@�@2
            '* corresponds to VS2008 End 2010/04/16 000043
            ''If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").Substring(1, 1) = "8") Then
            ''    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"
            ''Else
            ''    ' �Z����ʂ̉��P�����h8�h�i�]�o�ҁj�łȂ��ꍇ�A1			
            ''    csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1"
            ''End If
            '*����ԍ� 000037 2008/05/12 �폜�I��

            '*����ԍ� 000068 2024/07/05 �ǉ��J�n
            If (CStr(csJukiDataRow(ABJukiData.HONGOKUMEI)).Trim <> String.Empty) AndAlso
               (CStr(csJukiDataRow(ABJukiData.KANJIHEIKIMEI)).Trim <> String.Empty) AndAlso
               (CStr(csJukiDataRow(ABJukiData.KANJITSUSHOMEI)).Trim = String.Empty) Then
                ' �{�������� ���� ���L������ ���� �ʏ̖����󔒂̏ꍇ
                ' �������̂Q�E�J�i���̂Q�ɋ󔒂�ݒ�
                csJukiDataRow(ABJukiData.KANJIMEISHO2) = String.Empty
                csJukiDataRow(ABJukiData.KANAMEISHO2) = String.Empty
            Else
            End If
            '*����ԍ� 000068 2024/07/05 �ǉ��I��

            '*����ԍ� 000036 2007/09/28 �C���J�n
            ' �E�J�i���̂P�`�����p�J�i��
            If ((CStr(csJukiDataRow(ABJukiData.SHIMEIRIYOKB)).Trim = "2") AndAlso
                    (CStr(csJukiDataRow(ABJukiData.KANJIMEISHO2)).Trim <> String.Empty)) Then
                ' �{���D��(�{���ƒʏ̖������O���l���������p�敪��"2")
                csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                csAtenaRow(ABAtenaEntity.KANAMEISHO2) = String.Empty
                csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = String.Empty
                csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.KANJIMEISHO2)

                '����ԍ� 000039 2009/05/12 �C���J�n
                ' �����p�J�i�����A�����p�J�i���A�����p�J�i���𐶐����i�[
                strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)),
                                                               String.Empty, m_cFrnHommyoKensakuType)
                'strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)), _
                '                                               String.Empty, cuKanriJohoB.GetFrn_HommyoKensaku_Param)
                '����ԍ� 000039 2009/05/12 �C���I��

                ' �ʏ̖��������@�l��\�Ҏ����Ɋi�[
                csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                ' �ėp�敪�Q�Ɏ������p�敪�̃p�����[�^���i�[
                csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB)
                ' �擾���������p�J�i�����A�����p�J�i���A�����p�J�i�����i�[
                csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana(0)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana(1)
                csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana(2)

                '*����ԍ� 000039 2009/05/12 �C���J�n
            ElseIf (m_cFrnHommyoKensakuType = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                'ElseIf (cuKanriJohoB.GetFrn_HommyoKensaku_Param = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                '*����ԍ� 000039 2009/05/12 �C���I��

                ' �ʏ̖��D��(�{���D��̏����ȊO�̏ꍇ)
                csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)

                '*����ԍ� 000039 2009/05/12 �C���J�n
                ' �����p�J�i�����A�����p�J�i���A�����p�J�i���𐶐����i�[
                strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO1)),
                                                                CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)),
                                                                m_cFrnHommyoKensakuType)
                'strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO1)), _
                '                                               CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)), _
                '                                               cuKanriJohoB.GetFrn_HommyoKensaku_Param)
                '*����ԍ� 000039 2009/05/12 �C���I��

                ' �ʏ̖��������@�l��\�Ҏ�������ɂ���
                csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
                ' �ėp�敪�Q�Ɏ������p�敪�̃p�����[�^���i�[
                csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB)
                ' �擾���������p�J�i�����A�����p�J�i���A�����p�J�i�����i�[
                csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana(0)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana(1)
                csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana(2)
            Else
                '�ʏ̖��D��i�������[�U�j
                csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                ' �ʏ̖��������@�l��\�Ҏ�������ɂ���
                csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
                ' �ėp�敪�Q�Ɏ������p�敪�̃p�����[�^���i�[
                csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB)
                csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
                csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
            End If
            '' �E�J�i���̂P�`�����p�J�i��
            'csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
            'csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
            'csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
            'csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
            ''csAtenaRow(ABAtenaEntity.KANJIHJNKEITAI) = String.Empty
            ''csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
            'csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)
            ''*����ԍ� 000034 2007/08/31 �C���J�n
            'If (cuKanriJohoB.GetFrn_HommyoKensaku_Param = FrnHommyoKensakuType.Tsusho_Seishiki) Then
            '    '�O���l�{�������@�\��"2(Tsusho_Seishiki)"�̂Ƃ��p���͑啶���ɂ���
            '    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = CType(csJukiDataRow(ABJukiData.SEARCHKANASEIMEI), String).ToUpper()
            '    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = GetSearchKana(CType(csJukiDataRow(ABJukiData.KANAMEISHO2), String))
            '    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = CType(csJukiDataRow(ABJukiData.SEARCHKANAMEI), String).ToUpper()
            'Else
            '    csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
            '    csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
            '    csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
            'End If
            ''csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
            ''csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
            ''csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
            ''*����ԍ� 000034 2007/08/31 �C���I��
            '*����ԍ� 000036 2007/09/28 �C���I��
            csAtenaRow(ABAtenaEntity.KYUSEI) = csJukiDataRow(ABJukiData.KYUSEI)

            ' �E�Z���ԍ�=(����ԍ�)
            csAtenaRow(ABAtenaEntity.JUKIRRKNO) = CStr(csJukiDataRow(ABJukiData.RIREKINO)).RSubstring(2, 4)
            ' �E�����J�n�N�����`�Z���[�\����
            csAtenaRow(ABAtenaEntity.RRKST_YMD) = csJukiDataRow(ABJukiData.RRKST_YMD)
            csAtenaRow(ABAtenaEntity.RRKED_YMD) = csJukiDataRow(ABJukiData.RRKED_YMD)
            csAtenaRow(ABAtenaEntity.UMAREYMD) = csJukiDataRow(ABJukiData.UMAREYMD)
            csAtenaRow(ABAtenaEntity.UMAREWMD) = csJukiDataRow(ABJukiData.UMAREWMD)
            csAtenaRow(ABAtenaEntity.SEIBETSUCD) = csJukiDataRow(ABJukiData.SEIBETSUCD)
            csAtenaRow(ABAtenaEntity.SEIBETSU) = csJukiDataRow(ABJukiData.SEIBETSU)
            csAtenaRow(ABAtenaEntity.SEKINO) = csJukiDataRow(ABJukiData.SEIKINO)
            csAtenaRow(ABAtenaEntity.JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.JUMINHYOHYOJIJUN)
            ' �E��Q�Z���[�\����
            csAtenaRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.HYOJIJUN2)
            ' �E�����R�[�h�E�����E��2�����R�[�h�E��2����
            ' �@�Z����ʂ̉��P�����h8�h�i�]�o�ҁj�̏ꍇ�ő������h01�h�i���ю�j�̏ꍇ�A�Ǘ����̃R�[�h�ɕύX���A			
            '   ���̂̓N���A����
            If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) = "8") Then
                If (csJukiDataRow(ABJukiData.ZOKUGARACD).ToString.TrimEnd = "01") Then
                    If (m_strZokugara1Init = "00") Then
                        csAtenaRow(ABAtenaEntity.ZOKUGARACD) = String.Empty
                    Else
                        csAtenaRow(ABAtenaEntity.ZOKUGARACD) = m_strZokugara1Init
                    End If
                    csAtenaRow(ABAtenaEntity.ZOKUGARA) = String.Empty
                Else
                    csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD)
                    csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA)
                End If
                If (csJukiDataRow(ABJukiData.ZOKUGARACD2).ToString.TrimEnd = "01") Then
                    If (m_strZokugara2Init = "00") Then
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = String.Empty
                    Else
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = m_strZokugara2Init
                    End If
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = String.Empty
                Else
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2)
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2)
                End If
            Else
                ' �Z����ʂ̉��P�����h8�h�i�]�o�ҁj�łȂ��ꍇ�́A���̂܂܃Z�b�g			
                csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD)
                csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA)
                csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2)
                csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2)
            End If
            ' �E���ю�Z���R�[�h�`�J�i��Q���ю喼
            csAtenaRow(ABAtenaEntity.STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD)
            csAtenaRow(ABAtenaEntity.STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI)
            csAtenaRow(ABAtenaEntity.KANASTAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI)
            csAtenaRow(ABAtenaEntity.DAI2STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD2)
            csAtenaRow(ABAtenaEntity.DAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI2)
            csAtenaRow(ABAtenaEntity.KANADAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI2)

            ' �E�X�֔ԍ��`����
            ' �E�]�o�m��Z��������ꍇ�́A�]�o�m�藓����Z�b�g�i�Ȃ����ڂ̓Z�b�g�Ȃ��j
            If (csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString.TrimEnd <> String.Empty) Then
                csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO)
                '*����ԍ� 000001 2003/09/11 �C���J�n
                'csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD)
                csAtenaRow(ABAtenaEntity.JUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD), String)
                '*����ԍ� 000001 2003/09/11 �C���I��
                csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO)
                '*����ԍ� 000031 2007/01/30 �C���J�n
                ' �Ԓn��񂩂�Ԓn�R�[�h���擾
                '*����ԍ� 000038 2009/04/07 �C���J�n
                strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(CStr(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)))
                'strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(CStr(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)), strMotoBanchiCD, True)
                '' �擾�����Ԓn�R�[�h�z���Nothing�̍��ڂ�����ꍇ��String.Empty���Z�b�g����
                'For intLoop = 0 To strBanchiCD.Length - 1
                '    If (IsNothing(strBanchiCD(intLoop))) Then
                '        strBanchiCD(intLoop) = String.Empty
                '    End If
                'Next
                '*����ԍ� 000038 2009/04/07 �C���I��
                csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD(0)
                csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD(1)
                csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD(2)
                'csAtenaRow(ABAtenaEntity.BANCHICD1) = String.Empty
                'csAtenaRow(ABAtenaEntity.BANCHICD2) = String.Empty
                'csAtenaRow(ABAtenaEntity.BANCHICD3) = String.Empty
                '*����ԍ� 000031 2007/01/30 �C���I��
                csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)
                csAtenaRow(ABAtenaEntity.KATAGAKIFG) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKICD) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI)

                '*����ԍ� 000037 2008/05/12 �ǉ��J�n
                ' �Ǔ��ǊO�敪�F�ǊO�ɃZ�b�g    ���R�����g:�]�o�m��Z�������݂���ꍇ�͊ǊO�ɐݒ肷��B
                csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"
                '*����ԍ� 000037 2008/05/12 �ǉ��I��

            ElseIf (csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString.TrimEnd <> String.Empty) Then
                ' �E�]�o�m��Z���������A�]�o�\��Z��������ꍇ�́A�]�o�\�藓����Z�b�g�i�Ȃ����ڂ̓Z�b�g�Ȃ��j
                csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
                '*����ԍ� 000001 2003/09/11 �C���J�n
                'csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD)
                csAtenaRow(ABAtenaEntity.JUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String)
                '*����ԍ� 000001 2003/09/11 �C���I��
                csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
                ' �Ԓn��񂩂�Ԓn�R�[�h���擾
                '*����ԍ� 000038 2009/04/07 �C���J�n
                strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(CStr(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)))
                'strBanchiCD = m_crBanchiCdMstB.GetBanchiCd(CStr(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)), strMotoBanchiCD, True)
                '' �擾�����Ԓn�R�[�h�z���Nothing�̍��ڂ�����ꍇ��String.Empty���Z�b�g����
                'For intLoop = 0 To strBanchiCD.Length - 1
                '    If (IsNothing(strBanchiCD(intLoop))) Then
                '        strBanchiCD(intLoop) = String.Empty
                '    End If
                'Next
                '*����ԍ� 000038 2009/04/07 �C���I��
                csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD(0)
                csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD(1)
                csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD(2)
                'csAtenaRow(ABAtenaEntity.BANCHICD1) = String.Empty
                'csAtenaRow(ABAtenaEntity.BANCHICD2) = String.Empty
                'csAtenaRow(ABAtenaEntity.BANCHICD3) = String.Empty
                '*����ԍ� 000031 2007/01/30 �C���I��
                csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
                csAtenaRow(ABAtenaEntity.KATAGAKIFG) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKICD) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)

                '*����ԍ� 000037 2008/05/12 �ǉ��J�n
                ' �Ǔ��ǊO�敪�F�ǊO�ɃZ�b�g    ���R�����g:�]�o�\��Z�������݂���ꍇ�͊ǊO�ɐݒ肷��B
                csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"
                '*����ԍ� 000037 2008/05/12 �ǉ��I��

            Else
                ' �E�����������ꍇ�́A�Z��Z��������Z�b�g
                csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO)
                '*����ԍ� 000001 2003/09/11 �C���J�n
                'csAtenaRow(ABAtenaEntity.JUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD)
                csAtenaRow(ABAtenaEntity.JUSHOCD) = CType(csJukiDataRow(ABJukiData.JUKIJUSHOCD), String).RPadLeft(13)
                '*����ԍ� 000001 2003/09/11 �C���I��
                csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO)
                csAtenaRow(ABAtenaEntity.BANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1)
                csAtenaRow(ABAtenaEntity.BANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2)
                csAtenaRow(ABAtenaEntity.BANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3)
                csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI)
                csAtenaRow(ABAtenaEntity.KATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG)
                csAtenaRow(ABAtenaEntity.KATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20)
                csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI)

                '*����ԍ� 000037 2008/05/12 �ǉ��J�n
                ' �Ǔ��ǊO�敪�F�Ǔ��ɃZ�b�g    ���R�����g:�]�o�m��Z���A�]�o�\��Z�������݂��Ȃ��ꍇ�͊Ǔ��ɐݒ肷��B
                csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1"
                '*����ԍ� 000037 2008/05/12 �ǉ��I��

            End If
            ' �E�A����P�`�����N����
            csAtenaRow(ABAtenaEntity.RENRAKUSAKI1) = csJukiDataRow(ABJukiData.RENRAKUSAKI1)
            csAtenaRow(ABAtenaEntity.RENRAKUSAKI2) = csJukiDataRow(ABJukiData.RENRAKUSAKI2)
            '*����ԍ� 000001 2003/09/11 �C���J�n
            'csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = csJukiDataRow(ABJukiData.HON_ZJUSHOCD)
            csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = CType(csJukiDataRow(ABJukiData.HON_ZJUSHOCD), String)
            '*����ԍ� 000001 2003/09/11 �C���I��
            csAtenaRow(ABAtenaEntity.HON_JUSHO) = csJukiDataRow(ABJukiData.HON_JUSHO)
            csAtenaRow(ABAtenaEntity.HONSEKIBANCHI) = csJukiDataRow(ABJukiData.HON_BANCHI)
            csAtenaRow(ABAtenaEntity.HITTOSH) = csJukiDataRow(ABJukiData.HITTOSHA)
            csAtenaRow(ABAtenaEntity.CKINIDOYMD) = csJukiDataRow(ABJukiData.CKINIDOYMD)
            csAtenaRow(ABAtenaEntity.CKINJIYUCD) = csJukiDataRow(ABJukiData.CKINJIYUCD)
            csAtenaRow(ABAtenaEntity.CKINJIYU) = csJukiDataRow(ABJukiData.CKINJIYU)
            csAtenaRow(ABAtenaEntity.CKINTDKDYMD) = csJukiDataRow(ABJukiData.CKINTDKDYMD)
            csAtenaRow(ABAtenaEntity.CKINTDKDTUCIKB) = csJukiDataRow(ABJukiData.CKINTDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.TOROKUIDOYMD) = csJukiDataRow(ABJukiData.TOROKUIDOYMD)
            csAtenaRow(ABAtenaEntity.TOROKUIDOWMD) = csJukiDataRow(ABJukiData.TOROKUIDOWMD)
            csAtenaRow(ABAtenaEntity.TOROKUJIYUCD) = csJukiDataRow(ABJukiData.TOROKUJIYUCD)
            csAtenaRow(ABAtenaEntity.TOROKUJIYU) = csJukiDataRow(ABJukiData.TOROKUJIYU)
            csAtenaRow(ABAtenaEntity.TOROKUTDKDYMD) = csJukiDataRow(ABJukiData.TOROKUTDKDYMD)
            csAtenaRow(ABAtenaEntity.TOROKUTDKDWMD) = csJukiDataRow(ABJukiData.TOROKUTDKDWMD)
            csAtenaRow(ABAtenaEntity.TOROKUTDKDTUCIKB) = csJukiDataRow(ABJukiData.TOROKUTDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.JUTEIIDOYMD) = csJukiDataRow(ABJukiData.JUTEIIDOYMD)
            csAtenaRow(ABAtenaEntity.JUTEIIDOWMD) = csJukiDataRow(ABJukiData.JUTEIIDOWMD)
            csAtenaRow(ABAtenaEntity.JUTEIJIYUCD) = csJukiDataRow(ABJukiData.JUTEIJIYUCD)
            csAtenaRow(ABAtenaEntity.JUTEIJIYU) = csJukiDataRow(ABJukiData.JUTEIJIYU)
            csAtenaRow(ABAtenaEntity.JUTEITDKDYMD) = csJukiDataRow(ABJukiData.JUTEITDKDYMD)
            csAtenaRow(ABAtenaEntity.JUTEITDKDWMD) = csJukiDataRow(ABJukiData.JUTEITDKDWMD)
            csAtenaRow(ABAtenaEntity.JUTEITDKDTUCIKB) = csJukiDataRow(ABJukiData.JUTEITDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.SHOJOIDOYMD) = csJukiDataRow(ABJukiData.SHOJOIDOYMD)
            csAtenaRow(ABAtenaEntity.SHOJOJIYUCD) = csJukiDataRow(ABJukiData.SHOJOJIYUCD)
            csAtenaRow(ABAtenaEntity.SHOJOJIYU) = csJukiDataRow(ABJukiData.SHOJOJIYU)
            csAtenaRow(ABAtenaEntity.SHOJOTDKDYMD) = csJukiDataRow(ABJukiData.SHOJOTDKDYMD)
            csAtenaRow(ABAtenaEntity.SHOJOTDKDTUCIKB) = csJukiDataRow(ABJukiData.SHOJOTDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIIDOYMD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIIDOYMD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITUCIYMD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYUCD) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYUCD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYU) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYU)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_YUBINNO) = csJukiDataRow(ABJukiData.TENUMAEJ_YUBINNO)
            '*����ԍ� 000001 2003/09/11 �C���J�n
            'csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD), String)
            '*����ԍ� 000001 2003/09/11 �C���I��
            csAtenaRow(ABAtenaEntity.TENUMAEJ_JUSHO) = csJukiDataRow(ABJukiData.TENUMAEJ_JUSHO)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_BANCHI) = csJukiDataRow(ABJukiData.TENUMAEJ_BANCHI)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_KATAGAKI) = csJukiDataRow(ABJukiData.TENUMAEJ_KATAGAKI)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSMEI)
            '* ����ԍ� 000063 2024/02/06 �C���J�n
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
            ''*����ԍ� 000001 2003/09/11 �C���J�n
            ''csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String).RPadLeft(13)
            ''*����ԍ� 000001 2003/09/11 �C���I��
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)

            '�Z��f�[�^.�������R�R�[�h��45�i�]���ʒm�󗝁j�̏ꍇ
            If (csJukiDataRow(ABJukiData.SHORIJIYUCD).ToString() = ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00")) Then
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD), String)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI)
            Else
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)
            End If
            '* ����ԍ� 000063 2024/02/06 �C���I��
            csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSMEI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO)
            '*����ԍ� 000001 2003/09/11 �C���J�n
            'csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD), String)
            '*����ԍ� 000001 2003/09/11 �C���I��
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSMEI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIMITDKFG) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMITDKFG)
            csAtenaRow(ABAtenaEntity.BIKOYMD) = csJukiDataRow(ABJukiData.BIKOYMD)
            csAtenaRow(ABAtenaEntity.BIKO) = csJukiDataRow(ABJukiData.BIKO)
            csAtenaRow(ABAtenaEntity.BIKOTENSHUTSUKKTIJUSHOFG) = csJukiDataRow(ABJukiData.BIKOTENSHUTSUKKTIJUSHOFG)
            csAtenaRow(ABAtenaEntity.HANNO) = csJukiDataRow(ABJukiData.HANNO)
            csAtenaRow(ABAtenaEntity.KAISEIATOFG) = csJukiDataRow(ABJukiData.KAISEIATOFG)
            csAtenaRow(ABAtenaEntity.KAISEIMAEFG) = csJukiDataRow(ABJukiData.KAISEIMAEFG)
            csAtenaRow(ABAtenaEntity.KAISEIYMD) = csJukiDataRow(ABJukiData.KAISEIYMD)

            ' �E�s����R�[�h�`�n�於�R
            ' �@�Z����ʂ̉��P�����h8�h�i�]�o�ҁj�łȂ��ꍇ�A�Z��s����`�Z��n�於�R���Z�b�g			
            If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) <> "8") Then
                csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1)
                csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1)
                '*����ԍ� 000002 2003/09/18 �C���J�n
                'csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                'csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2)
                csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2)
                '*����ԍ� 000002 2003/09/18 �C���I��
                csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
            Else
                ' �Z����ʂ̉��P�����h8�h�i�]�o�ҁj�̏ꍇ�A�Ǘ����i�s���揉�����`�n��R�j�����āA
                ' �N���A�ɂȂ��Ă���ꍇ�́A�Z�b�g���Ȃ�
                If (m_strGyosekuInit.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = String.Empty
                    csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = String.Empty
                Else
                    '*����ԍ� 000021 2005/12/12 �C���J�n
                    ''csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                    ''csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                    If m_strTenshutsuGyoseikuCD.Trim = String.Empty Then
                        ' �N���A���Ȃ��ꍇ�œ]�o�җp�̍s����b�c���ݒ肳��Ă��Ȃ��ꍇ��
                        ' ���̂܂܏Z��̃f�[�^��ݒ肷��B
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                    Else
                        ' �N���A���Ȃ��ꍇ�œ]�o�җp�̍s����b�c���ݒ肳��Ă���ꍇ��
                        ' �s����b�c�}�X�^���s���於�̂��擾���A�ݒ肷��B
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = m_strTenshutsuGyoseikuCD.RPadLeft(9, " "c)
                        '*����ԍ� 000022 2005/12/15 �C���J�n
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = String.Empty
                        'csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = m_strTenshutsuGyoseikuMei
                        '*����ԍ� 000022 2005/12/15 �C���I��
                    End If
                    '*����ԍ� 000021 2005/12/12 �C���I��
                End If
                If (m_strChiku1Init.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.CHIKUCD1) = String.Empty
                    csAtenaRow(ABAtenaEntity.CHIKUMEI1) = String.Empty
                Else
                    csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1)
                    csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1)
                End If
                If (m_strChiku2Init.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.CHIKUCD2) = String.Empty
                    csAtenaRow(ABAtenaEntity.CHIKUMEI2) = String.Empty
                Else
                    '*����ԍ� 000002 2003/09/18 �C���J�n
                    'csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                    'csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                    csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2)
                    csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2)
                    '*����ԍ� 000002 2003/09/18 �C���I��
                End If
                If (m_strChiku3Init.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.CHIKUCD3) = String.Empty
                    csAtenaRow(ABAtenaEntity.CHIKUMEI3) = String.Empty
                Else
                    csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                    csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                End If
            End If

            ' �E���[��R�[�h�`�ݗ��I���N����
            csAtenaRow(ABAtenaEntity.TOHYOKUCD) = csJukiDataRow(ABJukiData.TOHYOKUCD)
            csAtenaRow(ABAtenaEntity.SHOGAKKOKUCD) = csJukiDataRow(ABJukiData.SHOGAKKOKUCD)
            csAtenaRow(ABAtenaEntity.CHUGAKKOKUCD) = csJukiDataRow(ABJukiData.CHUGAKKOKUCD)
            csAtenaRow(ABAtenaEntity.HOGOSHAJUMINCD) = csJukiDataRow(ABJukiData.HOGOSHAJUMINCD)
            csAtenaRow(ABAtenaEntity.KANJIHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANJIHOGOSHAMEI)
            csAtenaRow(ABAtenaEntity.KANAHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANAHOGOSHAMEI)
            csAtenaRow(ABAtenaEntity.KIKAYMD) = csJukiDataRow(ABJukiData.KIKAYMD)
            csAtenaRow(ABAtenaEntity.KARIIDOKB) = csJukiDataRow(ABJukiData.KARIIDOKB)
            csAtenaRow(ABAtenaEntity.SHORITEISHIKB) = csJukiDataRow(ABJukiData.SHORITEISHIKB)
            csAtenaRow(ABAtenaEntity.SHORIYOKUSHIKB) = csJukiDataRow(ABJukiData.SHORIYOKUSHIKB)
            csAtenaRow(ABAtenaEntity.JUKIYUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO)
            '*����ԍ� 000001 2003/09/11 �C���J�n
            csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD)
            'csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = CType(csJukiDataRow(ABJukiData.JUKIJUSHOCD), String).PadLeft(11)
            '*����ԍ� 000001 2003/09/11 �C���I��
            csAtenaRow(ABAtenaEntity.JUKIJUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO)
            csAtenaRow(ABAtenaEntity.JUKIBANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1)
            csAtenaRow(ABAtenaEntity.JUKIBANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2)
            csAtenaRow(ABAtenaEntity.JUKIBANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3)
            csAtenaRow(ABAtenaEntity.JUKIBANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI)
            csAtenaRow(ABAtenaEntity.JUKIKATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG)
            csAtenaRow(ABAtenaEntity.JUKIKATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20)
            csAtenaRow(ABAtenaEntity.JUKIKATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI)
            csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
            csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
            csAtenaRow(ABAtenaEntity.JUKICHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1)
            csAtenaRow(ABAtenaEntity.JUKICHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1)
            csAtenaRow(ABAtenaEntity.JUKICHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2)
            csAtenaRow(ABAtenaEntity.JUKICHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2)
            csAtenaRow(ABAtenaEntity.JUKICHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
            csAtenaRow(ABAtenaEntity.JUKICHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
            'csAtenaRow(ABAtenaEntity.KAOKUSHIKIKB) = String.Empty
            'csAtenaRow(ABAtenaEntity.BIKOZEIMOKU) = String.Empty
            csAtenaRow(ABAtenaEntity.KOKUSEKICD) = csJukiDataRow(ABJukiData.KOKUSEKICD)
            csAtenaRow(ABAtenaEntity.KOKUSEKI) = csJukiDataRow(ABJukiData.KOKUSEKI)
            csAtenaRow(ABAtenaEntity.ZAIRYUSKAKCD) = csJukiDataRow(ABJukiData.ZAIRYUSKAKCD)
            csAtenaRow(ABAtenaEntity.ZAIRYUSKAK) = csJukiDataRow(ABJukiData.ZAIRYUSKAK)
            csAtenaRow(ABAtenaEntity.ZAIRYUKIKAN) = csJukiDataRow(ABJukiData.ZAIRYUKIKAN)
            csAtenaRow(ABAtenaEntity.ZAIRYU_ST_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ST_YMD)
            csAtenaRow(ABAtenaEntity.ZAIRYU_ED_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ED_YMD)

            '*����ԍ� 000003 2003/11/21 �ǉ��J�n
            ' ���������}�X�^�̏Z���Z�o�O�敪���P�i�Z���j�ŗ���ԍ�����ԑ傫�����̂��擾
            'cSearchKey = New ABAtenaSearchKey()
            'cSearchKey.p_strJuminCD = strJuminCD
            'csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "", "1", True)
            'StrShoriNichiji = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")
            '' �f�[�^�����݂���ꍇ�́A

            '*����ԍ� 000003 2003/11/21 �ǉ��I��

            '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
            If ((blnJukiUmu) AndAlso csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows.Count > 0) Then
                '�����t���̃f�[�^�����݂���ꍇ�A0�s�ڂ��擾
                csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).Rows(0)
            Else
                '��L�ȊO�̎��A��̍s���쐬
                csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).NewRow
                Me.ClearAtenaFZY(csAtenaFzyRow)
            End If

            '�����t���s�̕ҏW
            csAtenaFzyRow = Me.SetAtenaFzy(csAtenaFzyRow, csAtenaRow, csJukiDataRow)
            '* ����ԍ� 000044 2011/11/09 �ǉ��I��

            '---------------------------------------------------------------------------------------
            ' 5. ���������}�X�^�̍X�V
            '�@�@�@�@�@�Z�o�O�f�[�^�����݂��Ă���ꍇ�͊J�n�E�I���N�����ƏZ�o�O�D��敪��ҏW����B
            '---------------------------------------------------------------------------------------
            '**
            '* ��������
            '*
            ' �E�Z��L��e�k�f���h1�h�̎��́A�Z��D��Ŏw��N������99999999�ň��������}�X�^����ݗ����I���N�������V�X�e�A
            ' �@�����t�̑O�����Z�b�g���A���������}�X�^�X�V�����s����
            'If (blnJukiUmu) Then
            '    ' ���t�N���X�̕K�v�Ȑݒ���s��
            '    m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            '    m_cfDateClass.p_enEraType = UFEraType.Number
            '    cSearchKey = New ABAtenaSearchKey()
            '    cSearchKey.p_strJuminCD = strJuminCD
            '    cSearchKey.p_strJuminYuseniKB = "1"
            '    csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(1, cSearchKey, "99999999", True)
            '    If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count > 0) Then
            '        csDataRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)
            '        'm_cfDateClass.p_strDateValue = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd") '�V�X�e�����t
            '        'csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
            '        ' �����}�X�^�����������ւ��̂܂ܕҏW����
            '        For Each csDataColumn In csAtenaRow.Table.Columns
            '            csDataRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
            '        Next csDataColumn
            '        m_cfDateClass.p_strDateValue = CType(csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD), String)
            '        csDataRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
            '        intCount = m_cAtenaRirekiB.UpdateAtenaRB(csDataRow)
            '        If (intCount <> 1) Then
            '            ' �G���[��`���擾�i�Y���f�[�^�͑��ōX�V����Ă��܂��܂����B�ēx����F���������j
            '            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            '            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
            '            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
            '        End If
            '    End If
            'Else

            '*����ԍ� 000016 2005/11/01 �C���J�n
            '*�R�����g**********************************************************
            ' �Z�o�O���N���Ă���f�[�^�Ɋւ��Ă͂�����l�����Ă��Ȃ��ƁA     *
            ' �����������}�X�^�͍���Ȃ��B�C���O�͈�؍l������Ă��Ȃ��̂ŁA *
            ' �Z�o�O���N���Ă���ꍇ�͐V���ɍ�肱��ł��K�v������B         *
            '*******************************************************************
            '* corresponds to VS2008 Start 2010/04/16 000043
            ''''' ���������}�X�^���Y���҂̑S�������擾����
            ''''cSearchKey = New ABAtenaSearchKey()
            ''''cSearchKey.p_strJuminCD = strJuminCD
            ''''csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRBHoshu(999, cSearchKey, "", True)

            ''''' ���������̗���擾���A����������B�i�X�V�J�E�^�[�́A0�A����ȊO�́AString Empty�j�i���ʁj
            ''''csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow
            ''''Me.ClearAtenaRireki(csAtenaRirekiRow)

            ''''' �����}�X�^��舶�������}�X�^�̕ҏW���s��(����)
            ''''' ����ԍ��@�@�@�V�K�̂΂����́A0001�@�@�C���̏ꍇ�́A���������}�X�^�̍ŏI�ԍ��ɂ`�c�c�@�P����
            ''''' ����ȊO�̍��ڂɂ��ẮA�����}�X�^�����̂܂ܕҏW����			
            ''''If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
            ''''    ' ����ԍ�
            ''''    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = "0001"
            ''''Else
            ''''    ' ����ԍ��ō~���ɕ��ёւ�
            ''''    csAtenaRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
            ''''    ' ����ԍ�(�擪�s�̗���ԍ�+1)
            ''''    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType((CType(csAtenaRirekiRows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).PadLeft(4, "0"c)
            ''''End If
            ''''' �����}�X�^�����������ւ��̂܂ܕҏW����
            ''''For Each csDataColumn In csAtenaRow.Table.Columns
            ''''    csAtenaRirekiRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
            ''''Next csDataColumn

            ''''m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            ''''m_cfDateClass.p_enEraType = UFEraType.Number

            ''''m_cfDateClass.p_strDateValue = CType(csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD), String)
            ''''csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)

            ''''' ���������}�X�^�̒ǉ����s��
            ''''csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csAtenaRirekiRow)
            ''''intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)
            ''''If (intCount <> 1) Then
            ''''    ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
            ''''    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            ''''    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
            ''''    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
            ''''End If
            '* corresponds to VS2008 End 2010/04/16 000043

            '---------------------------------------------------------------------------------------
            ' 5-1. �X�V�p�̗������R�[�h���쐬����B
            '---------------------------------------------------------------------------------------

            ' ���������̍s���擾���A����������B�i�X�V�J�E�^�[�́A0�A����ȊO�́AString Empty�j�i���ʁj
            csAtenaRirekiRow = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow
            Me.ClearAtenaRireki(csAtenaRirekiRow)

            ' �����}�X�^�����������ւ��̂܂ܕҏW����
            For Each csDataColumn In csAtenaRow.Table.Columns
                csAtenaRirekiRow(csDataColumn.ColumnName) = csAtenaRow(csDataColumn)
            Next csDataColumn

            '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
            '�ޔ�������������t�����V�K�s�쐬
            csAtenaRirekiFzyRow = Me.m_csReRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).NewRow
            Me.ClearAtenaFZY(csAtenaRirekiFzyRow)
            '�����t������f�[�^�R�s�[
            csAtenaRirekiFzyRow = Me.SetAtenaRirekiFzy(csAtenaRirekiFzyRow, csAtenaFzyRow)
            '* ����ԍ� 000044 2011/11/09 �ǉ��I��
            '---------------------------------------------------------------------------------------
            ' 5-2. �J�n�E�I���N�����̕ҏW�����A�I���N����������}�C�i�X�ɂ���
            '---------------------------------------------------------------------------------------

            ' ���t�N���X�̕K�v�Ȑݒ������
            m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            m_cfDateClass.p_enEraType = UFEraType.Number

            ' �I���N�������Z�����̃f�[�^�̈���O��ݒ肷��B
            ' (�Z��͗���N�������P���R�[�h�ڂ̏I���ƂQ���R�[�h�ڂ̊J�n��������B�����͈�������)
            m_cfDateClass.p_strDateValue = CType(csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD), String)
            csAtenaRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)


            '---------------------------------------------------------------------------------------
            ' 5-3. �Z��E�Z�o�O�̗����f�[�^���X�V����
            '---------------------------------------------------------------------------------------

            ' �Z�o�O���N���Ă��Ȃ��f�[�^�Ɋւ��Ă͂��̂܂ܒǉ�
            If m_blnJutogaiAriFG = False Then

                '---------------------------------------------------------------------------------------
                ' 5-3-1. �Z�o�O�f�[�^�����݂��Ȃ��̂ŁA�Z��f�[�^�����̂܂܍X�V����
                '---------------------------------------------------------------------------------------

                m_intRenbanCnt += 1
                ' ����ԍ���ݒ肷��
                csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                ' �Z�o�O�D��敪��"1"�ɐݒ肷��
                csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"

                ' ���������}�X�^�̒ǉ����s��
                '* ����ԍ� 000044 2011/11/09 �C���J�n
                'intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                If (Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                           csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString) Is Nothing) Then
                    'Insert���鈶�������ƈ�v���鈶������t�������݂��Ȃ���΁ANothing�ɂ���
                    csAtenaRirekiFzyRow = Nothing
                Else
                    '����ԍ��̐ݒ�
                    csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                End If

                intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow)
                '* ����ԍ� 000044 2011/11/09 �C���I��
                If (intCount <> 1) Then
                    ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                End If
            Else

                '---------------------------------------------------------------------------------------
                ' 5-3-2. �Z�o�O�f�[�^�����݂��Ă���̂ŁA�Z��E�Z�o�O�f�[�^�̕ҏW���s���X�V����
                '---------------------------------------------------------------------------------------

                ' �Z�o�O���N���Ă���f�[�^�Ɋւ��Ă͍l������
                ' �A�ԗp�J�E���g���{�P
                m_intRenbanCnt += 1

                ' �ǉ�����Z��R�[�h���Z�o�O���N�����ׂ����R�[�h���ǂ����𔻒肷��
                '*����ԍ� 000020 2005/12/07 �ǉ��J�n
                ' �Z��R�[�h�ƏZ�o�O���R�[�h�̊J�n�N�����������ꍇ�̏�����ǉ�
                '*����ԍ� 000024 2005/12/17 �C���J�n
                ''If m_blnHenkanFG = False AndAlso _
                ''   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) = m_intJutogaiST_YMD AndAlso _
                ''   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" Then
                '*����ԍ� 000025 2005/12/18 �C���J�n
                ''If m_blnHenkanFG = False AndAlso _
                ''   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) = m_intJutogaiST_YMD AndAlso _
                ''   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" AndAlso _
                ''   m_intJutogaiRowCnt > m_intJutogaiInCnt Then

                If m_blnHenkanFG = False AndAlso
                   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) >= m_intJutogaiST_YMD AndAlso
                   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RRemove(0, 1) <> "0" AndAlso
                   m_intJutogaiRowCnt > m_intJutogaiInCnt Then
                    '*����ԍ� 000025 2005/12/18 �C���I��
                    '*����ԍ� 000024 2005/12/17 �C���I��

                    '---------------------------------------------------------------------------------------
                    ' 5-3-2-1. �Z�o�O�����݂��Ă�����ԂŏZ��f�[�^�𕪊����Ȃ��P�[�X
                    '
                    '          �Z�o�O�f�[�^�쐬���܂��Ȃ��A�@����
                    '�@�@�@�@�@�ޔ������Z�o�O�f�[�^�̊J�n�N�����ƏZ��f�[�^�̊J�n�N�������������A
                    '          �Z��f�[�^�̕����������ł���A ����
                    '�@�@�@�@�@�Z���ȊO�A  ����
                    '�@�@�@�@�@�ޔ������Z�o�O�f�[�^���܂��c���Ă���@�ꍇ��
                    '
                    '          �Z��f�[�^�i1���j�ƏZ�o�O�f�[�^�i1���j�̌v2�����X�V����B
                    '---------------------------------------------------------------------------------------

                    ' �J�n�N�������������āA�Z��R�[�h�����[�҂ł���Ȃ�A���̃��R�[�h����Z�o�O���N�����B
                    ' �������ŕʓr���R�[�h���쐬���āA�ǉ����邱�Ƃ͂��Ȃ��B
                    ' ����ԍ���ݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)
                    ' �Z�o�O�D��敪��"0"�ɐݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                    ' ���������}�X�^�̒ǉ����s��
                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)
                    '����ԍ��������������R�s�[
                    csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                    intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��
                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If

                    ' �A�ԗp�J�E���g���{�P
                    m_intRenbanCnt += 1

                    '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                    '�ޔ���������t���f�[�^���珉��Z�o�O���R�[�h�Ɉ�v����f�[�^���擾
                    csAtenaRirekiFzyJugaiRow = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                                                                  m_csFirstJutogaiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                  m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                    '* ����ԍ� 000049 2012/04/06 �폜�J�n
                    'If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                    '    '��łȂ����͗���ԍ����㏑��
                    '    csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                    'Else
                    '    '�������Ȃ�
                    'End If
                    '* ����ԍ� 000049 2012/04/06 �폜�I��
                    '* ����ԍ� 000044 2011/11/09 �ǉ��I��

                    ' ����ԍ���ݒ肷��
                    m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)
                    ' ���������}�X�^�̒ǉ����s��
                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csFirstJutogaiRow)
                    '* ����ԍ� 000049 2012/04/06 �C���J�n
                    'csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                    If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                        '��łȂ����͗���ԍ����㏑��
                        csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                    Else
                        '�������Ȃ�
                    End If
                    '* ����ԍ� 000049 2012/04/06 �C���I��
                    intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csFirstJutogaiRow, csAtenaRirekiFzyJugaiRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��
                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If
                    ' �Z�o�O�f�[�^�J�E���g���{�P
                    m_intJutogaiInCnt += 1
                    ' ���̏Z�o�O�q�n�v���擾����
                    If m_intJutogaiInCnt <= m_intJutogaiRowCnt - 1 Then
                        m_csFirstJutogaiRow = m_csJutogaiRows(m_intJutogaiInCnt)
                        m_intJutogaiST_YMD = CType(m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                    End If
                    ' �Z�o�O���N���������ǂ����̃t���O���s�������ɂ���
                    m_blnHenkanFG = True
                    '*����ԍ� 000020 2005/12/07 �ǉ��I��
                    '*����ԍ� 000024 2005/12/17 �C���J�n
                    ''ElseIf m_blnHenkanFG = False AndAlso _
                    ''   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) <= m_intJutogaiST_YMD AndAlso _
                    ''   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD), Integer) > m_intJutogaiST_YMD AndAlso _
                    ''   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" Then
                    '*����ԍ� 000040 2009/05/22 �C���J�n
                ElseIf m_blnHenkanFG = False AndAlso
                        CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) < m_intJutogaiST_YMD AndAlso
                            CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD), Integer) >= m_intJutogaiST_YMD AndAlso
                                CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RRemove(0, 1) <> "0" AndAlso
                                    m_intJutogaiRowCnt > m_intJutogaiInCnt Then
                    'ElseIf m_blnHenkanFG = False AndAlso _
                    '       CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) <= m_intJutogaiST_YMD AndAlso _
                    '       CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD), Integer) > m_intJutogaiST_YMD AndAlso _
                    '       CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" AndAlso _
                    '       m_intJutogaiRowCnt > m_intJutogaiInCnt Then
                    '*����ԍ� 000040 2009/05/22 �C���I��

                    '---------------------------------------------------------------------------------------
                    ' 5-3-2-2. �Z�o�O�����݂��Ă�����ԂŏZ��f�[�^�𕪊�����P�[�X
                    '
                    '          �Z�o�O�f�[�^�쐬���܂��Ȃ��A�@����
                    '�@�@�@�@�@�ޔ������Z�o�O�f�[�^�̊J�n�N�����ƏZ��f�[�^�̊J�n�N�������������A
                    '          �Z��f�[�^�̕����ߋ����ł���A ����
                    '�@�@�@�@�@�ޔ������Z�o�O�f�[�^�̊J�n�N�������Z��f�[�^�̏I���N�����̕����������ł���A ����
                    '�@�@�@�@�@�Z���ȊO�A  ���@�ޔ������Z�o�O�f�[�^���܂��c���Ă���@�ꍇ��
                    '
                    '          �Z��f�[�^�i2���j�ƏZ�o�O�f�[�^�i1���j�̌v3�����X�V����B
                    '---------------------------------------------------------------------------------------

                    '*����ԍ� 000024 2005/12/17 �C���I��
                    ' ����ԍ���ݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                    ' �Z�o�O�D��敪��"1"�ɐݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"

                    ' �I���N�������ŏ��̏Z�o�O�q�n�v�̊J�n�N�����̈���O�ɐݒ肷��
                    intYMD = CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD), Integer)   ' �ޔ�����
                    m_cfDateClass.p_strDateValue = CType(m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD), String)
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)

                    ' ���������}�X�^�̒ǉ����s��
                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                    '���������̗���ԍ�����������t���ɐݒ�
                    csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                    intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��
                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If

                    ' �A�ԗp�J�E���g���{�P
                    m_intRenbanCnt += 1

                    ' ����ԍ���ݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                    ' �Z�o�O�D��敪��"0"�ɐݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"

                    ' �J�n�N�������ŏ��̏Z�o�O�q�n�v�̊J�n�N�����ɐݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD)

                    ' �I���N�������Z�o�O���N�����O�̃��R�[�h�̏I���N�����ɐݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = CType(intYMD, String)

                    ' ���������}�X�^�̒ǉ����s��
                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                    '���������̗���ԍ�����������t���ɐݒ�
                    csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                    intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��
                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If

                    ' �A�ԗp�J�E���g���{�P
                    m_intRenbanCnt += 1

                    '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                    '�ޔ���������t���f�[�^���珉��Z�o�O���R�[�h�Ɉ�v����f�[�^���擾
                    csAtenaRirekiFzyJugaiRow = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                                                                  m_csFirstJutogaiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                  m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                    '* ����ԍ� 000049 2012/04/06 �폜�J�n
                    'If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                    '    '��łȂ����͗���ԍ����㏑��
                    '    csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                    'Else
                    '    '�������Ȃ�
                    'End If
                    '* ����ԍ� 000049 2012/04/06 �폜�I��
                    '* ����ԍ� 000044 2011/11/09 �ǉ��I��


                    ' ����ԍ���ݒ肷��
                    m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                    ' ���������}�X�^�̒ǉ����s��
                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csFirstJutogaiRow)
                    '* ����ԍ� 000049 2012/04/06 �C���J�n
                    'csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                    If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                        '��łȂ����͗���ԍ����㏑��
                        csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                    Else
                        '�������Ȃ�
                    End If
                    '* ����ԍ� 000049 2012/04/06 �C���I��
                    intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csFirstJutogaiRow, csAtenaRirekiFzyJugaiRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��

                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If

                    ' �Z�o�O�f�[�^�J�E���g���{�P
                    m_intJutogaiInCnt += 1

                    ' ���̏Z�o�O�q�n�v���擾����
                    If m_intJutogaiInCnt <= m_intJutogaiRowCnt - 1 Then
                        m_csFirstJutogaiRow = m_csJutogaiRows(m_intJutogaiInCnt)
                        m_intJutogaiST_YMD = CType(m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                    End If

                    ' �Z�o�O���N���������ǂ����̃t���O���s�������ɂ���
                    m_blnHenkanFG = True
                    '*����ԍ� 000018 2005/11/27 �폜�J�n
                    '' �ē]���t���O���e���������ɂ���
                    ''m_blnSaiTenyuFG = False
                    '*����ԍ� 000018 2005/11/27 �폜�I��

                    '*����ԍ� 000040 2009/05/22 �C���J�n
                    '*����ԍ� 000024 2005/12/17 �C���J�n
                    ''ElseIf m_intJutogaiRowCnt > m_intJutogaiInCnt AndAlso _
                    ''   CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) > m_intJutogaiST_YMD Then
                    'ElseIf m_intJutogaiRowCnt > m_intJutogaiInCnt AndAlso _
                    '       CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) >= m_intJutogaiST_YMD AndAlso _
                    '       CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).PadLeft(2, " "c).Remove(0, 1) <> "0" Then
                    '*����ԍ� 000024 2005/12/17 �C���I��
                ElseIf m_intJutogaiRowCnt > m_intJutogaiInCnt AndAlso
                            CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) >= m_intJutogaiST_YMD Then
                    '*����ԍ� 000040 2009/05/22 �C���I��

                    '---------------------------------------------------------------------------------------
                    ' 5-3-2-3. �Z�o�O�����݂��Ă�����ԂŏZ��f�[�^�𕪊����Ȃ��P�[�X
                    '          �i�Q���R�[�h�ڈȍ~�̏Z�o�O�f�[�^�Z�b�g���j
                    '
                    '          �ޔ������Z�o�O�f�[�^���܂��c���Ă���A�@����
                    '�@�@�@�@�@�ޔ������Z�o�O�f�[�^�̊J�n�N�����ƏZ��f�[�^�̊J�n�N�������������A
                    '          �Z��f�[�^�̕����������ł���A ����
                    '
                    '�@�@�@�@�@�Z���ȊO�@�̏ꍇ
                    '---------------------------------------------------------------------------------------
                    '** �R�����g ***************************************************************************
                    ' �Z��f�[�^�̑S�������S�ďZ���A���Z�o�O�̗��������݂���P�[�X�i�ʏ킠�肦�Ȃ����j����������\��������B
                    ' �f�O���[�g�̊댯���傫�����ƂƁA�����p�x�����Ȃ菭�Ȃ��̂ł��̍l���͍s��Ȃ����ƂƂ���B
                    '***************************************************************************************

                    '* ����ԍ� 000048 2012/01/05 �C���J�n
                    ''* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                    ''�ޔ���������t���f�[�^���珉��Z�o�O���R�[�h�Ɉ�v����f�[�^���擾
                    'csAtenaRirekiFzyJugaiRow = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, _
                    '                                              m_csFirstJutogaiRow(ABAtenaRirekiEntity.JUMINCD).ToString, _
                    '                                              m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                    'If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                    '    '��łȂ����͗���ԍ����㏑��
                    '    csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                    'Else
                    '    '�������Ȃ�
                    'End If
                    ''* ����ԍ� 000044 2011/11/09 �ǉ��I��
                    '�ޔ���������t���f�[�^���珉��Z�o�O���R�[�h�Ɉ�v����f�[�^���擾
                    csAtenaRirekiFzyJugaiRow = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                                                                  m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                  m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RIREKINO).ToString)
                    '* ����ԍ� 000048 2012/01/05 �C���I��

                    ' ����ԍ���ݒ肷��
                    m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                    '*����ԍ� 000023 2005/12/16 �ǉ��J�n
                    ' �Z��̃��R�[�h���ē]�����R�[�h�̎��ł��Z�o�O�̃��R�[�h�����߃��R�[�h�̏ꍇ
                    ' �I���N�������Z��R�[�h�̊J�n�N�����̈���O�ɃZ�b�g����
                    If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RRemove(0, 1) = "0" AndAlso
                       CType(m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RRKED_YMD), String) = "99999999" Then
                        m_cfDateClass.p_strDateValue = CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), String)
                        m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                    End If

                    '* ����ԍ� 000048 2012/01/05 �ǉ��J�n
                    If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                        '��łȂ����͗���ԍ����㏑��
                        csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RIREKINO)
                    Else
                        '�������Ȃ�
                    End If
                    '* ����ԍ� 000048 2012/01/05 �ǉ��I��

                    '*����ԍ� 000023 2005/12/16 �ǉ��I��
                    ' ���������}�X�^�̒ǉ����s��(�Z�o�O�q�n�v)
                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(m_intJutogaiInCnt))

                    intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(m_intJutogaiInCnt), csAtenaRirekiFzyJugaiRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��

                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If

                    ' �Z�o�O�f�[�^�J�E���g���{�P
                    m_intJutogaiInCnt += 1

                    '�Z�o�O���R�[�h���Ō�̂ЂƂɂȂ�܂ŌJ��Ԃ��Ǝv���邪�A���̂��߂ɂ���Ă��邩������Ȃ��i�悵����j
                    For intIdx = m_intJutogaiInCnt To m_intJutogaiRowCnt - 1

                        '*����ԍ� 000040 2009/05/22 �폜�J�n
                        '�������ŃZ�b�g�������Ă��邯�ǁA������ĈӖ�����́H�������Ƃɂ���i�悵����j
                        'm_intJutogaiST_YMD = CType(m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                        '*����ԍ� 000040 2009/05/22 �폜�I��

                        ' ���̏Z�o�O�q�n�v���擾
                        m_csFirstJutogaiRow = m_csJutogaiRows(m_intJutogaiInCnt)
                        m_intJutogaiST_YMD = CType(m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer)

                        '*����ԍ� 000024 2005/12/17 �C���J�n
                        ''If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) > m_intJutogaiST_YMD Then
                        '�Z�o�O�f�[�^�̊J�n�N�����ƏZ��f�[�^�̊J�n�N�������������A�������̏ꍇ�͏Z�o�O���X�V����
                        If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer) >= m_intJutogaiST_YMD Then
                            '*����ԍ� 000024 2005/12/17 �C���I��
                            ' �A�ԗp�J�E���g���{�P
                            m_intRenbanCnt += 1
                            '* ����ԍ� 000048 2012/01/05 �C���J�n
                            ''* ����ԍ� 000044 2011/11/09 �ǉ��J�n
                            ''�ޔ���������t���f�[�^���珉��Z�o�O���R�[�h�Ɉ�v����f�[�^���擾
                            'csAtenaRirekiFzyJugaiRow = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity, _
                            '                                              m_csFirstJutogaiRow(ABAtenaRirekiEntity.JUMINCD).ToString, _
                            '                                              m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO).ToString)
                            'If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                            '    '��łȂ����͗���ԍ����㏑��
                            '    csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csFirstJutogaiRow(ABAtenaRirekiEntity.RIREKINO)
                            'Else
                            '    '�������Ȃ�
                            'End If
                            ''* ����ԍ� 000044 2011/11/09 �ǉ��I��
                            '�ޔ���������t���f�[�^���珉��Z�o�O���R�[�h�Ɉ�v����f�[�^���擾
                            csAtenaRirekiFzyJugaiRow = Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                                                                          m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.JUMINCD).ToString,
                                                                          m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RIREKINO).ToString)

                            '* ����ԍ� 000048 2012/01/05 �C���I��

                            ' ����ԍ���ݒ肷��
                            m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                            '*����ԍ� 000023 2005/12/16 �ǉ��J�n
                            ' �Z��̃��R�[�h���ē]�����R�[�h�̎��ł��Z�o�O�̃��R�[�h�����߃��R�[�h�̏ꍇ
                            ' �I���N�������Z��R�[�h�̊J�n�N�����̈���O�ɃZ�b�g����
                            If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RRemove(0, 1) = "0" AndAlso
                               CType(m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RRKED_YMD), String) = "99999999" Then

                                m_cfDateClass.p_strDateValue = CType(csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD), String)
                                m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)

                            End If
                            '*����ԍ� 000023 2005/12/16 �ǉ��I��

                            '* ����ԍ� 000048 2012/01/05 �ǉ��J�n
                            If (csAtenaRirekiFzyJugaiRow IsNot Nothing) Then
                                '��łȂ����͗���ԍ����㏑��
                                csAtenaRirekiFzyJugaiRow(ABAtenaRirekiFZYEntity.RIREKINO) = m_csJutogaiRows(m_intJutogaiInCnt)(ABAtenaRirekiEntity.RIREKINO)
                            Else
                                '�������Ȃ�
                            End If
                            '* ����ԍ� 000048 2012/01/05 �ǉ��I��

                            ' ���������}�X�^�̒ǉ����s��(�Z�o�O�q�n�v)
                            '* ����ԍ� 000044 2011/11/09 �C���J�n
                            'intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(m_intJutogaiInCnt))

                            intCount = m_cAtenaRirekiB.InsertAtenaRB(m_csJutogaiRows(m_intJutogaiInCnt), csAtenaRirekiFzyJugaiRow)
                            '* ����ԍ� 000044 2011/11/09 �C���I��

                            If (intCount <> 1) Then
                                ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                            End If

                            ' �Z�o�O�f�[�^�J�E���g���{�P
                            m_intJutogaiInCnt += 1
                        Else
                            '*����ԍ� 000040 2009/05/22 �폜�J�n
                            ' �O�̏Z�o�O�q�n�v���擾
                            '�O���R�[�h�̊J�n�N�������擾���Ă��g�p����ĂȂ����ǁA������ĈӖ�����́H�������Ƃɂ���i�悵����j
                            'm_csFirstJutogaiRow = m_csJutogaiRows(m_intJutogaiInCnt - 1)
                            'm_intJutogaiST_YMD = CType(m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                            '*����ԍ� 000040 2009/05/22 �폜�I��
                            Exit For
                        End If
                    Next intIdx

                    ' �A�ԗp�J�E���g���{�P
                    m_intRenbanCnt += 1

                    ' ����ԍ���ݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                    ' �Z����ʂ��Z���Ȃ�Z�o�O���N���������ǂ����̃t���O��False�ɂ���
                    If CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RSubstring(1, 1) = "0" Then
                        m_blnHenkanFG = False
                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                        '*����ԍ� 000018 2005/11/27 �폜�J�n
                        ' �ē]���t���O��True�ɂ���
                        'm_blnSaiTenyuFG = True
                        '*����ԍ� 000018 2005/11/27 �폜�I��
                    Else
                        If m_blnHenkanFG = False Then
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                        Else
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                        End If
                    End If

                    ' ���������}�X�^�̒ǉ����s��
                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                    csAtenaRirekiFzyRow(ABAtenaRirekiEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                    intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��

                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If

                    ' ���̏Z�o�O�q�n�v���擾����
                    If m_intJutogaiInCnt <= m_intJutogaiRowCnt - 1 Then
                        m_csFirstJutogaiRow = m_csJutogaiRows(m_intJutogaiInCnt)
                        m_intJutogaiST_YMD = CType(m_csFirstJutogaiRow(ABAtenaRirekiEntity.RRKST_YMD), Integer)
                    End If
                Else

                    '---------------------------------------------------------------------------------------
                    ' 5-3-2-4. �ǂ�ɂ����Ă͂܂�Ȃ��ꍇ
                    '---------------------------------------------------------------------------------------

                    ' ����ԍ���ݒ肷��
                    csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = CType(m_intRenbanCnt, String).RPadLeft(4, "0"c)

                    ' �Z�o�O���N���Ă��ā@���@��ʂ��Z���łȂ���ΏZ�o�O�D��敪��"0"
                    If m_blnHenkanFG = True AndAlso
                       CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RSubstring(1, 1) <> "0" Then
                        ' �Z�o�O�D��敪��"0"
                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "0"
                    ElseIf m_blnHenkanFG = True AndAlso
                           CType(csAtenaRirekiRow(ABAtenaRirekiEntity.ATENADATASHU), String).RPadLeft(2, " "c).RSubstring(1, 1) = "0" Then
                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                        '*����ԍ� 000018 2005/11/27 �폜�J�n
                        '' �ē]���t���O��True�ɂ���
                        ''m_blnSaiTenyuFG = True
                        '*����ԍ� 000018 2005/11/27 �폜�I��
                        m_blnHenkanFG = False
                    Else
                        ' �Z�o�O�D��敪��"1"
                        csAtenaRirekiRow(ABAtenaRirekiEntity.JUTOGAIYUSENKB) = "1"
                    End If

                    ' ���������}�X�^�̒ǉ����s��
                    '* ����ԍ� 000044 2011/11/09 �C���J�n
                    'intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow)

                    If (Me.GetChokkin_RirekiFzy(m_csReRirekiFzyEntity,
                            csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINCD).ToString,
                           csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO).ToString) Is Nothing) Then
                        'Insert���鈶�������ƈ�v���鈶������t�������݂��Ȃ���΁ANothing�ɂ���
                        csAtenaRirekiFzyRow = Nothing
                    Else
                        '����ԍ��������������擾
                        csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
                    End If
                    intCount = m_cAtenaRirekiB.InsertAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow)
                    '* ����ԍ� 000044 2011/11/09 �C���I��

                    If (intCount <> 1) Then
                        ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                    End If
                End If

            End If
            '*����ԍ� 000016 2005/11/01 �C���I��

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
            Throw objExp
        End Try

    End Sub
    '************************************************************************************************
    '* ���\�b�h��     ����Row�̏�����
    '* 
    '* �\��           Public Sub ClearAtena(ByRef csAtenaRow As DataRow)
    '* 
    '* �@�\ �@    �@�@����Row������������
    '* 
    '* ����           DataRow : AtenaEntity
    '* 
    '* �߂�l         DataRow : AtenaEntity
    '************************************************************************************************
    Private Sub ClearAtena(ByRef csAtenaDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "ClearAtena"
        Dim csDataColumn As DataColumn                      ' �c�������b����������

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���ڂ̏�����
            For Each csDataColumn In csAtenaDataRow.Table.Columns
                Select Case csDataColumn.ColumnName
                    Case ABAtenaEntity.KOSHINCOUNTER
                        csAtenaDataRow(csDataColumn) = Decimal.Zero
                    Case Else
                        csAtenaDataRow(csDataColumn) = String.Empty
                End Select
            Next csDataColumn

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
            Throw objExp
        End Try
    End Sub
    '************************************************************************************************
    '* ���\�b�h��     ��������Row�̏�����
    '* 
    '* �\��           Public Sub ClearAtenaRireki(ByRef csAtenaRirekiRow As DataRow)
    '* 
    '* �@�\ �@    �@�@��������Row�̏�����
    '* 
    '* ����           DataRow : AtenaRirekiEntity
    '* 
    '* �߂�l         DataRow : AtenaRirekiEntity
    '************************************************************************************************
    Private Sub ClearAtenaRireki(ByRef csAtenaRirekiRow As DataRow)
        Const THIS_METHOD_NAME As String = "ClearAtenaRireki"
        Dim csDataColumn As DataColumn                      ' �c�������b����������

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���ڂ̏�����
            For Each csDataColumn In csAtenaRirekiRow.Table.Columns
                Select Case csDataColumn.ColumnName
                    Case ABAtenaRirekiEntity.KOSHINCOUNTER
                        csAtenaRirekiRow(csDataColumn) = Decimal.Zero
                    Case Else
                        csAtenaRirekiRow(csDataColumn) = String.Empty
                End Select
            Next csDataColumn

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
            Throw objExp
        End Try
    End Sub
    '************************************************************************************************
    '* ���\�b�h��     �����ݐ�Row�̏�����
    '* 
    '* �\��           Public Sub ClearAtenaRuiseki(ByRef csAtenaRuisekiRow As DataRow)
    '* 
    '* �@�\ �@    �@�@�����ݐ�Row������������
    '* 
    '* ����           DataRow : AtenaRuisekiEntity
    '* 
    '* �߂�l         DataRow : AtenaRuisekiEntity
    '************************************************************************************************
    Private Sub ClearAtenaRuiseki(ByRef csAtenaRuisekiRow As DataRow)
        Const THIS_METHOD_NAME As String = "ClearAtenaRuiseki"
        Dim csDataColumn As DataColumn                      ' �c�������b����������

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���ڂ̏�����
            For Each csDataColumn In csAtenaRuisekiRow.Table.Columns
                Select Case csDataColumn.ColumnName
                    Case ABAtenaRuisekiEntity.KOSHINCOUNTER
                        csAtenaRuisekiRow(csDataColumn) = Decimal.Zero
                    Case Else
                        csAtenaRuisekiRow(csDataColumn) = String.Empty
                End Select
            Next csDataColumn

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
            Throw objExp
        End Try
    End Sub

    '*����ԍ� 000003 2003/11/21 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����ݐ�Row�ֈ����N����ݒ�
    '* 
    '* �\��           Public Sub SetNenkinToRuiseki(ByVal csAtenaNenkinRow As DataRow, ByRef csAtenaRuisekiRow As DataRow)
    '* 
    '* �@�\ �@    �@�@�����ݐ�Row������������
    '* 
    '* ����           DataRow : AtenaNenkinEntity
    '* �@�@           DataRow : AtenaRuisekiEntity
    '* 
    '* �߂�l         DataRow : AtenaRuisekiEntity
    '************************************************************************************************
    Private Sub SetNenkinToRuiseki(ByVal csAtenaNenkinRow As DataRow, ByRef csAtenaRuisekiRow As DataRow)
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csDataColumn As DataColumn                      ' �c�������b����������
        '* corresponds to VS2008 End 2010/04/16 000043

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KSNENKNNO) = csAtenaNenkinRow(ABAtenaNenkinEntity.KSNENKNNO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSHUTKYMD) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSHUTKYMD)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSHUTKSHU) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSHUTKSHU)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSHUTKRIYUCD) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSHUTKRIYUCD)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSSHTSYMD) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSSHTSYMD)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKNSKAKSSHTSRIYUCD) = csAtenaNenkinRow(ABAtenaNenkinEntity.SKAKSSHTSRIYUCD)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKIGO1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKIGO1)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNNO1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNNO1)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNSHU1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNSHU1)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNEDABAN1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNEDABAN1)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKB1) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKB1)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKIGO2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKIGO2)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNNO2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNNO2)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNSHU2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNSHU2)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNEDABAN2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNEDABAN2)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKB2) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKB2)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKIGO3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKIGO3)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNNO3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNNO3)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNSHU3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNSHU3)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNEDABAN3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNEDABAN3)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.JKYNENKNKB3) = csAtenaNenkinRow(ABAtenaNenkinEntity.JKYNENKNKB3)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.NENKINHIHOKENSHAGAITOKB) = String.Empty
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHUBETSUHENKOYMD) = String.Empty

            ' �f�o�b�O�I�����O�o��
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
            Throw objExp
        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �����ݐ�Row�ֈ������ۂ�ݒ�
    '* 
    '* �\��           Public Sub SetKokuhoToRuiseki(ByVal csAtenaKokuhoRow As DataRow, ByRef csAtenaRuisekiRow As DataRow)
    '* 
    '* �@�\ �@    �@�@�����ݐ�Row������������
    '* 
    '* ����           DataRow : csAtenaKokuhoEntity
    '* �@�@           DataRow : AtenaRuisekiEntity
    '* 
    '* �߂�l         DataRow : AtenaRuisekiEntity
    '************************************************************************************************
    Private Sub SetKokuhoToRuiseki(ByVal csAtenaKokuhoRow As DataRow, ByRef csAtenaRuisekiRow As DataRow)
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Dim csDataColumn As DataColumn                      ' �c�������b����������
        '* corresponds to VS2008 End 2010/04/16 000043

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHONO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHONO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSHIKAKUKB) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBMEISHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBRYAKUSHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOGAKUENKB) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOGAKUENKB)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOGAKUENKBMEISHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOGAKUENKBRYAKUSHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSHUTOKUYMD) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOSOSHITSUYMD) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKKB) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKKB)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKKBMEISHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKKBRYAKUSHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKB) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBMEISHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBRYAKUSHO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKGAITOYMD) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOTISHKHIGAITOYMD) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOHOKENSHOKIGO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKUHOHOKENSHONO) = csAtenaKokuhoRow(ABAtenaKokuhoEntity.KOKUHOHOKENSHONO)
            csAtenaRuisekiRow(ABAtenaRuisekiEntity.KOKHOHIHOKENSHAGAITOKB) = String.Empty

            ' �f�o�b�O�I�����O�o��
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
            Throw objExp
        End Try
    End Sub
    '*����ԍ� 000003 2003/11/21 �ǉ��I��

    '*����ԍ� 000009 2005/02/28     �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �Z��v���J�f�[�^�X�V
    '* 
    '* �\��           Public Sub JukiDataReplicaKoshin(ByVal csJukiDataEntity As DataSet)
    '* 
    '* �@�\ �@    �@�@�Z��v���J�f�[�^�̍X�V�������s�Ȃ�
    '* 
    '* ����           DataSet(csJukiDataEntity) : �Z��f�[�^�Z�b�g
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub JukiDataReplicaKoshin(ByVal csJukiDataEntity As DataSet)
        Const THIS_METHOD_NAME As String = "JukiDataReplicaKoshin"
        '*����ԍ� 000009 2005/03/18 �폜�J�n
        ''''''Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '�����Ǘ����c�`�r�W�l�X�N���X
        ''''''Dim csAtenaKanriEntity As DataSet                   '�����Ǘ����f�[�^�Z�b�g
        '*����ԍ� 000009 2005/03/18 �폜�I��
        Dim csABToshoPrmEntity As New DataSet               '���v���J�쐬�p�p�����[�^�f�[�^�Z�b�g
        Dim csABToshoPrmTable As DataTable                  '���v���J�쐬�p�p�����[�^�f�[�^�e�[�u��
        Dim csABToshoPrmRow As DataRow                      '���v���J�쐬�p�p�����[�^�f�[�^�e�[�u��
        Dim csJukiDataRow As DataRow                        '�Z��f�[�^Row
        Dim blnJutogaiUmu As Boolean = False                ' �Z�o�O�L��FLG
        Dim csJutogaiEntity As DataSet                      ' �Z�o�ODataSet
        Dim strJuminCD As String                            ' �Z���R�[�h
        Dim cABAtenaCnvBClass As ABAtenaCnvBClass
        Const WORK_FLOW_NAME As String = "�����ٓ�"             ' ���[�N�t���[��
        Const DATA_NAME As String = "����"                      '�f�[�^��

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            '�Ǘ����̃��[�N�t���[���R�[�h�����݂��A�p�����[�^��"1"�̎��������[�N�t���[�������s��
            If Not m_strR3RenkeiFG Is Nothing AndAlso m_strR3RenkeiFG = "1" Then

                '�f�[�^�Z�b�g�擾�N���X�̃C���X�^���X��
                cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                ' �e�[�u���Z�b�g�̎擾
                csABToshoPrmTable = cABAtenaCnvBClass.CreateColumnsToshoPrmData()
                csABToshoPrmTable.TableName = ABToshoPrmEntity.TABLE_NAME
                ' �f�[�^�Z�b�g�Ƀe�[�u���Z�b�g�̒ǉ�
                csABToshoPrmEntity.Tables.Add(csABToshoPrmTable)

                ' �f�[�^���J��Ԃ�
                For Each csJukiDataRow In csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows
                    If CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then

                        '�Z���b�c�̎擾
                        strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString

                        ' �Z���R�[�h�ŏZ�o�O�}�X�^���擾����i���݂���ꍇ�́A�Z�o�O�L��e�k�f�Ɂh1�h���Z�b�g�j
                        csJutogaiEntity = m_cJutogaiB.GetJutogaiBHoshu(strJuminCD, True)
                        If (csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows.Count > 0) Then
                            blnJutogaiUmu = True
                        End If

                        '�Z�o�OFLG��"1"�łȂ��ėp�敪��"02","10","11","12","14","15"�ŗ����I���N������"99999999"�i���߃f�[�^�j�̏ꍇ
                        If Not (blnJutogaiUmu) And
                            (CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "02" Or
                            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "10" Or
                            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "11" Or
                            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "12" Or
                            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "14" Or
                            CType(csJukiDataRow(ABJukiData.HANYOCD), String) = "15") And
                            CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then

                            '�V�K���E�̍쐬
                            csABToshoPrmRow = csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).NewRow()
                            '�v���p�e�B�ɃZ�b�g
                            csABToshoPrmRow.Item(ABToshoPrmEntity.JUMINCD) = strJuminCD                                 '�Z���R�[�h
                            csABToshoPrmRow.Item(ABToshoPrmEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD).ToString   '���уR�[�h
                            csABToshoPrmRow.Item(ABToshoPrmEntity.KOSHINKB) = ABConstClass.WF_INSERT_KOSHINKB           '�X�V�敪�i�ǉ�:1 �C��:2 �폜:D�j
                            '�f�[�^�Z�b�g�Ƀ��E��ǉ�����
                            csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows.Add(csABToshoPrmRow)

                        ElseIf CType(csJukiDataRow(ABJukiData.RRKED_YMD), String) = "99999999" Then

                            '�V�K���E�̍쐬
                            csABToshoPrmRow = csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).NewRow()
                            '�v���p�e�B�ɃZ�b�g
                            csABToshoPrmRow.Item(ABToshoPrmEntity.JUMINCD) = strJuminCD                                 '�Z���R�[�h
                            csABToshoPrmRow.Item(ABToshoPrmEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD).ToString   '���уR�[�h
                            csABToshoPrmRow.Item(ABToshoPrmEntity.KOSHINKB) = ABConstClass.WF_UPDATE_KOSHINKB           '�X�V�敪�i�ǉ�:1 �C��:2 �폜:D�j
                            '�f�[�^�Z�b�g�Ƀ��E��ǉ�����
                            csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows.Add(csABToshoPrmRow)

                        End If
                    End If
                Next csJukiDataRow

                '���R�[�h������"0"�o�Ȃ����̓��[�N�t���[�������s��
                If Not (csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows.Count = 0) Then
                    '���[�N�t���[���M�����Ăяo��
                    cABAtenaCnvBClass.WorkFlowExec(csABToshoPrmEntity, WORK_FLOW_NAME, DATA_NAME)
                End If
            End If

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
            Throw objExp
        End Try
    End Sub
    '*����ԍ� 000009 2005/02/28     �ǉ��I��

    '* ����ԍ� 000055 2015/01/08 �폜�J�n
    ''* ����ԍ� 000053 2014/09/10 �ǉ��J�n
    '''' <summary>
    '''' ���ԃT�[�o�[�a�r�f�[�^�X�V
    '''' </summary>
    '''' <param name="csJukiDataEntity">�Z��f�[�^</param>
    '''' <remarks></remarks>
    'Public Sub JukiDataBSKoshin(ByVal csJukiDataEntity As DataSet)

    '    Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
    '    Dim cfErrorClass As UFErrorClass
    '    Dim cfErrorStruct As UFErrorStruct
    '    Dim csJuminCD As ArrayList
    '    Dim cABBSRenkeiB As ABBSRenkeiBClass

    '    Try

    '        ' �f�o�b�O�J�n���O�o��
    '        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        ' �����`�F�b�N
    '        If (csJukiDataEntity Is Nothing OrElse _
    '            csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Rows.Count = 0) Then
    '            cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
    '            cfErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003304)
    '            Throw New UFAppException(cfErrorStruct.m_strErrorMessage, cfErrorStruct.m_strErrorCode)
    '        Else
    '            ' noop
    '        End If

    '        ' �Z��f�[�^��蒼�߃f�[�^�i�����I������"99999999"�j�̏Z���R�[�h���擾
    '        csJuminCD = New ArrayList
    '        For Each csDataRow As DataRow In csJukiDataEntity.Tables(ABJukiData.TABLE_NAME).Select( _
    '                                            String.Format("{0} = '99999999'", ABJukiData.RRKED_YMD), _
    '                                            ABJukiData.JUMINCD)
    '            csJuminCD.Add(csDataRow.Item(ABJukiData.JUMINCD).ToString)
    '        Next csDataRow

    '        ' ���ԃT�[�o�[�a�r�A�g�r�W�l�X�N���X�̃C���X�^���X��
    '        cABBSRenkeiB = New ABBSRenkeiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

    '        ' ���ԃT�[�o�[�a�r�A�g�̎��s
    '        cABBSRenkeiB.ExecRenkei(csJuminCD)

    '        ' �f�o�b�O�I�����O�o��
    '        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '    Catch cfAppExp As UFAppException

    '        ' ���[�j���O���O�o��
    '        m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                    "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" + _
    '                                    "�y���[�j���O���e:" + cfAppExp.Message + "�z")
    '        Throw

    '    Catch csExp As Exception

    '        ' �G���[���O�o��
    '        m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                    "�y�G���[���e:" + csExp.Message + "�z")
    '        Throw

    '    End Try

    'End Sub
    '* ����ԍ� 000053 2014/09/10 �ǉ��I��
    '* ����ԍ� 000055 2015/01/08 �폜�I��

    '*����ԍ� 000027 2005/12/20 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �C���T�[�g����Z�o�O���R�[�h��ҏW����
    '* 
    '* �\��           Public Function EditJutogaiRows(ByVal csJutogaiRows() As DataRow) As DataRow()
    '* 
    '* �@�\ �@    �@�@�C���T�[�g����Z�o�O���R�[�h��ҏW����
    '* 
    '* ����           DataRow(csJutogaiRows()) : �Z�o�O�f�[�^���E(����)
    '* 
    '* �߂�l         DataRow()�F�ҏW�����Z�o�O�f�[�^���E(����)
    '************************************************************************************************
    Public Function EditJutogaiRows(ByVal csJutogaiRows() As DataRow, ByVal strJukiCkinST_YMD As String) As DataRow()
        '* corresponds to VS2008 Start 2010/04/16 000043
        'Const THIS_METHOD_NAME As String = "EditJutogaiRows"
        '* corresponds to VS2008 End 2010/04/16 000043
        Dim intIdx As Integer = 0
        Dim intNewIdx As Integer = 0
        Dim csNewJutogaiRow(0) As DataRow

        For intIdx = 0 To csJutogaiRows.Length - 1

            If CType(csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKST_YMD), String) = m_strGapeiDate AndAlso
                (CType(csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKED_YMD), String) = m_strBefGapeiDate OrElse
                 CType(csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKED_YMD), String) = "99999999") Then
                ' �Z�o�O���R�[�h�̊J�n�N�������������@���@(�I���N����������������O�@�܂��́@"99999999")�̏ꍇ�A
                ' ���̏Z�o�O���R�[�h�͕K�v�Ȃ��Ȃ�̂ŉ������Ȃ��B

            ElseIf CType(csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKED_YMD), String) = m_strBefGapeiDate Then
                ' �Z�o�O���R�[�h�̏I���N����������������O�̏ꍇ�A
                ' ���̏Z�o�O���R�[�h�̏I���N�������Z��f�[�^���߃��R�[�h�̊J�n�N�����̈���O��ݒ肷��B
                ' �����G���e�B�e�B�̐V�K���E���擾����
                ReDim Preserve csNewJutogaiRow(intNewIdx)
                csNewJutogaiRow(intNewIdx) = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow

                m_cfDateClass.p_strDateValue = strJukiCkinST_YMD
                csJutogaiRows(intIdx)(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                csNewJutogaiRow(intNewIdx) = csJutogaiRows(intIdx)

                intNewIdx += 1
            Else
                ' ����ȊO�͂��̂܂܃Z�b�g
                ' �����G���e�B�e�B�̐V�K���E���擾����
                ReDim Preserve csNewJutogaiRow(intNewIdx)
                csNewJutogaiRow(intNewIdx) = m_csReRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow

                csNewJutogaiRow(intNewIdx) = csJutogaiRows(intIdx)

                intNewIdx += 1
            End If

        Next intIdx

        Return csNewJutogaiRow

    End Function
    '*����ԍ� 000027 2005/12/20 �ǉ��I��

    '*����ԍ� 000036 2007/09/28 �폜�J�n
    ''*����ԍ� 000034 2007/08/31 �ǉ��J�n
    ''************************************************************************************************
    ''* ���\�b�h��     �����p�J�i�擾�F�O���l�{�������@�\
    ''* 
    ''* �\��           Public Function GetSearchKana(ByVal strKanaMeisho As String,) As String
    ''* 
    ''* �@�\           �����p�J�i���̂�ҏW����
    ''* 
    ''* ����           strKanaMeisho As String     : �J�i����
    ''* 
    ''* �߂�l         String                      : �J�i�����i�������C������24�����ȓ��j
    ''************************************************************************************************
    'Private Function GetSearchKana(ByVal strKanaMeisho As String) As String
    '    Const THIS_METHOD_NAME As String = "GetSearchKana"                      '���\�b�h��
    '    Dim strSearchKana As String                         '�����p�J�i
    '    Dim cuString As New USStringClass                   '������ҏW
    '    Dim intIndex As Integer                             '�擪����̋󔒈ʒu

    '    Try
    '        ' �f�o�b�O�J�n���O�o��
    '        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        '�{���J�i����
    '        If (strKanaMeisho.Length > 0) Then
    '            strSearchKana = cuString.ToKanaKey((strKanaMeisho).Replace(" ", String.Empty)).ToUpper()
    '        Else
    '            strSearchKana = String.Empty
    '        End If

    '        '�����J�i���̌��`�F�b�N
    '        If strSearchKana.Length > 24 Then
    '            strSearchKana = strSearchKana.Substring(0, 24)
    '        End If

    '        ' �f�o�b�O�I�����O�o��
    '        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


    '    Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
    '        ' �V�X�e���G���[���X���[����
    '        Throw objExp
    '    End Try

    '    Return strSearchKana

    'End Function
    ''*����ԍ� 000034 2007/08/31 �ǉ��I��
    '*����ԍ� 000036 2007/09/28 �폜�I��

    '* ����ԍ� 000044 2011/11/09 �ǉ��J�n
#Region "�����t��������"
    '************************************************************************************************
    '* ���\�b�h��     �����t���nDataRwo����������
    '* 
    '* �\��           Private Sub ClearAtenaFZY(ByVal csFzyRow As DataRow)
    '* 
    '* �@�\           �����t���nDataRow�̏��������s��
    '* 
    '* ����           csFzyRow As DataRow     : �t���s
    '************************************************************************************************
    Private Sub ClearAtenaFZY(ByVal csFzyRow As DataRow)
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���ڂ̏�����
            For Each csDataColumn As DataColumn In csFzyRow.Table.Columns
                Select Case csDataColumn.ColumnName
                    Case ABAtenaFZYEntity.KOSHINCOUNTER, ABAtenaFZYEntity.LINKNO
                        csFzyRow(csDataColumn) = Decimal.Zero
                    Case Else
                        csFzyRow(csDataColumn) = String.Empty
                End Select
            Next csDataColumn

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
    End Sub
#End Region
#Region "�����t���f�[�^�ݒ�"
    '************************************************************************************************
    '* ���\�b�h��     �����t���f�[�^�ݒ菈��
    '* 
    '* �\��           Private Function SetAtenaFzy(ByVal csAtenaFzyRow As DataRow, ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
    '* 
    '* �@�\           �����t���nDataRow�̏��������s��
    '* 
    '* ����           csAtenaFzyRow As DataRow     : �����t���f�[�^
    '*                csAtenaRow As DataRow        �F�����f�[�^
    '*                csJukiDataRow As DataRow     �F�Z��f�[�^
    '*
    '* �߂�l         �����t���̃f�[�^�ݒ���s��
    '************************************************************************************************
    Private Function SetAtenaFzy(ByVal csAtenaFzyRow As DataRow, ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�f�[�^�ҏW
            csAtenaFzyRow(ABAtenaFZYEntity.JUMINCD) = csAtenaRow(ABAtenaEntity.JUMINCD)
            csAtenaFzyRow(ABAtenaFZYEntity.SHICHOSONCD) = csAtenaRow(ABAtenaEntity.SHICHOSONCD)
            csAtenaFzyRow(ABAtenaFZYEntity.KYUSHICHOSONCD) = csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD)
            csAtenaFzyRow(ABAtenaFZYEntity.JUMINJUTOGAIKB) = csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB)
            csAtenaFzyRow(ABAtenaFZYEntity.TABLEINSERTKB) = csJukiDataRow(ABJukiData.TABLEINSERTKB)
            csAtenaFzyRow(ABAtenaFZYEntity.LINKNO) = csJukiDataRow(ABJukiData.LINKNO)
            csAtenaFzyRow(ABAtenaFZYEntity.JUMINHYOJOTAIKBN) = csJukiDataRow(ABJukiData.JUMINHYOJOTAIKBN)
            csAtenaFzyRow(ABAtenaFZYEntity.JUKYOCHITODOKEFLG) = csJukiDataRow(ABJukiData.JUKYOCHITODOKEFLG)
            csAtenaFzyRow(ABAtenaFZYEntity.HONGOKUMEI) = csJukiDataRow(ABJukiData.HONGOKUMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.KANAHONGOKUMEI) = csJukiDataRow(ABJukiData.KANAHONGOKUMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.KANJIHEIKIMEI) = csJukiDataRow(ABJukiData.KANJIHEIKIMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.KANAHEIKIMEI) = csJukiDataRow(ABJukiData.KANAHEIKIMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.KANJITSUSHOMEI) = csJukiDataRow(ABJukiData.KANJITSUSHOMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.KANATSUSHOMEI) = csJukiDataRow(ABJukiData.KANATSUSHOMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.KATAKANAHEIKIMEI) = csJukiDataRow(ABJukiData.KATAKANAHEIKIMEI)
            '* ����ԍ� 000045 2011/11/28 �ǉ��J�n
            If csJukiDataRow(ABJukiData.FUSHOUMAREBI).ToString.Trim.RLength > 0 Then
                csAtenaFzyRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = ABConstClass.UMAREFUSHOKBN_FUSHO_YMD
            Else
                csAtenaFzyRow(ABAtenaFZYEntity.UMAREFUSHOKBN) = ABConstClass.UMAREFUSHOKBN_FUSHONASHI
            End If
            '* ����ԍ� 000045 2011/11/28 �ǉ��I��
            csAtenaFzyRow(ABAtenaFZYEntity.TSUSHOMEITOUROKUYMD) = csJukiDataRow(ABJukiData.TSUSHOMEITOUROKUYMD)
            csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUKIKANCD) = csJukiDataRow(ABJukiData.ZAIRYUKIKANCD)
            csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUKIKANMEISHO) = csJukiDataRow(ABJukiData.ZAIRYUKIKANMEISHO)
            csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUSHACD) = csJukiDataRow(ABJukiData.ZAIRYUSHACD)
            csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUSHAMEISHO) = csJukiDataRow(ABJukiData.ZAIRYUSHAMEISHO)
            csAtenaFzyRow(ABAtenaFZYEntity.ZAIRYUCARDNO) = csJukiDataRow(ABJukiData.ZAIRYUCARDNO)
            csAtenaFzyRow(ABAtenaFZYEntity.KOFUYMD) = csJukiDataRow(ABJukiData.KOFUYMD)
            csAtenaFzyRow(ABAtenaFZYEntity.KOFUYOTEISTYMD) = csJukiDataRow(ABJukiData.KOFUYOTEISTYMD)
            csAtenaFzyRow(ABAtenaFZYEntity.KOFUYOTEIEDYMD) = csJukiDataRow(ABJukiData.KOFUYOTEIEDYMD)
            csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOIDOYMD) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOIDOYMD)
            csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYUCD) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOJIYUCD)
            csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOJIYU) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOJIYU)
            csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDYMD) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOTDKDYMD)
            csAtenaFzyRow(ABAtenaFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB) = csJukiDataRow(ABJukiData.JUKITAISHOSHASHOJOTDKDTUCIKB)
            csAtenaFzyRow(ABAtenaFZYEntity.FRNSTAINUSMEI) = csJukiDataRow(ABJukiData.FRNSTAINUSMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.FRNSTAINUSKANAMEI) = csJukiDataRow(ABJukiData.FRNSTAINUSKANAMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.STAINUSHEIKIMEI) = csJukiDataRow(ABJukiData.STAINUSHEIKIMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.STAINUSKANAHEIKIMEI) = csJukiDataRow(ABJukiData.STAINUSKANAHEIKIMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.STAINUSTSUSHOMEI) = csJukiDataRow(ABJukiData.STAINUSTSUSHOMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.STAINUSKANATSUSHOMEI) = csJukiDataRow(ABJukiData.STAINUSKANATSUSHOMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSMEI_KYOTSU)
            csAtenaFzyRow(ABAtenaFZYEntity.TENUMAEJ_STAINUSHEIKIMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSHEIKIMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSTSUSHOMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSMEI_KYOTSU)
            csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSHEIKIMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSTSUSHOMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSMEI_KYOTSU)
            csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSHEIKIMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSTSUSHOMEI)
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE1) = csJukiDataRow(ABJukiData.FRNRESERVE1)
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE2) = csJukiDataRow(ABJukiData.FRNRESERVE2)
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE3) = csJukiDataRow(ABJukiData.FRNRESERVE3)
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE4) = csJukiDataRow(ABJukiData.FRNRESERVE4)
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE5) = csJukiDataRow(ABJukiData.FRNRESERVE5)
            '* ����ԍ� 000050 2014/06/25 �C���J�n
            'csAtenaFzyRow(ABAtenaFZYEntity.RESERVE6) = csJukiDataRow(ABJukiData.JUKIRESERVE1)
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE6) = String.Empty
            '* ����ԍ� 000050 2014/06/25 �C���I��
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE7) = csJukiDataRow(ABJukiData.JUKIRESERVE2)
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE8) = csJukiDataRow(ABJukiData.JUKIRESERVE3)
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE9) = csJukiDataRow(ABJukiData.JUKIRESERVE4)
            csAtenaFzyRow(ABAtenaFZYEntity.RESERVE10) = csJukiDataRow(ABJukiData.JUKIRESERVE5)

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csAtenaFzyRow
    End Function
#End Region

#Region "��������t���f�[�^�ݒ�"
    '************************************************************************************************
    '* ���\�b�h��     ��������t���f�[�^�ݒ菈��
    '* 
    '* �\��           Private Function SetAtenaRirekiFzy(ByVal csAtenaRirekiFzy As DataRow, ByVal csAtenaFzyRow As DataRow) As DataRow
    '* 
    '* �@�\           �����t���nDataRow�̏��������s��
    '* 
    '* ����           csAtenaRirekiFzy As DataRow     : ��������t���f�[�^
    '*                csAtenaFzyRow As DataRow        �F�����t���f�[�^
    '*
    '* �߂�l         ��������t���̃f�[�^�ݒ���s��
    '************************************************************************************************
    Private Function SetAtenaRirekiFzy(ByVal csAtenaRirekiFzy As DataRow, ByVal csAtenaFzyRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�t�����痚��t���ɃZ�b�g
            For Each csColumn As DataColumn In csAtenaFzyRow.Table.Columns
                If (csAtenaRirekiFzy(csColumn.ColumnName) IsNot Nothing) Then
                    '�񂪂������������ݒ�
                    csAtenaRirekiFzy(csColumn.ColumnName) = csAtenaFzyRow(csColumn.ColumnName)
                Else
                    '�������Ȃ�
                End If
            Next

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csAtenaRirekiFzy
    End Function
#End Region
#Region "�����ݐϕt���f�[�^�ݒ�"
    '************************************************************************************************
    '* ���\�b�h��     �����ݐϕt���f�[�^�ݒ菈��
    '* 
    '* �\��           Private Function SetAtenaRirekiFzy(ByVal csAtenaRirekiFzy As DataRow, ByVal csAtenaFzyRow As DataRow) As DataRow
    '* 
    '* �@�\           �����t���nDataRow�̏��������s��
    '* 
    '* ����           csAtenaRuisekiFzyRow As DataRow     : �����ݐϕt���f�[�^
    '*                csAtenaRirekiRow As DataRow        �F���������f�[�^
    '*                csAtenaRuisekiRow As DataRow       �F�����ݐσf�[�^
    '*
    '* �߂�l         ��������t�����父���ݐϕt�������
    '************************************************************************************************
    Private Function SetAtenaRuisekiFzy(ByVal csAtenaRuisekiFzyRow As DataRow, ByVal csAtenaRirekiRow As DataRow, ByVal csAtenaRuisekiRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '����or�t����������t���ݐςɃZ�b�g
            For Each csColumn As DataColumn In csAtenaRirekiRow.Table.Columns
                If (csAtenaRuisekiFzyRow.Table.Columns.Contains(csColumn.ColumnName)) Then
                    '�񂪂������������Z�b�g
                    csAtenaRuisekiFzyRow(csColumn.ColumnName) = csAtenaRirekiRow(csColumn.ColumnName)
                Else
                    '�������Ȃ�
                End If
            Next

            '���������ƑO��敪�͗ݐς���Z�b�g
            csAtenaRuisekiFzyRow(ABAtenaRuisekiFZYEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI)
            csAtenaRuisekiFzyRow(ABAtenaRuisekiFZYEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB)

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csAtenaRuisekiFzyRow
    End Function
#End Region
#Region "��������t�����߃f�[�^�擾"
    '************************************************************************************************
    '* ���\�b�h��     ��������t�����߃f�[�^�擾
    '* 
    '* �\��           Private Function GetChokkin_RirekiFzy(ByVal csAtenaRirekiFzy As DataSet, ByVal strJuminCD As String, ByVal strRirekiNo As String) As DataRow
    '* 
    '* �@�\           �����t���nDataRow�̏��������s��
    '* 
    '* ����           csAtenaRirekiFzy As DataSet     : ��������t���f�[�^
    '*                strJuminCD As String            �F�Z���R�[�h
    '*                strRirekiNo As String           �F����ԍ�
    '*
    '* �߂�l         ��������t���������̏����Ō������A���ʂ̂O�Ԗڂ�Ԃ��B��������Nothing��Ԃ�
    '************************************************************************************************
    Private Function GetChokkin_RirekiFzy(ByVal csAtenaRirekiFzy As DataSet, ByVal strJuminCD As String, ByVal strRirekiNo As String) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csSelectedRows As DataRow() '�������ʔz��
        Dim csCkinRow As DataRow        '���ߍs
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (csAtenaRirekiFzy IsNot Nothing) Then
                '������������t����Nothing�łȂ���
                csSelectedRows = csAtenaRirekiFzy.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Select(
                    String.Format("{0}='{1}' AND {2}='{3}'",
                          ABAtenaRirekiFZYEntity.JUMINCD, strJuminCD,
                          ABAtenaRirekiFZYEntity.RIREKINO, strRirekiNo))
                If (csSelectedRows.Count > 0) Then
                    '���߃f�[�^�����݂������A�O�s�ڂ�����Ă���
                    csCkinRow = csSelectedRows(0)
                Else
                    '����ȊO�̎��ANothing�ŕԂ�
                    csCkinRow = Nothing
                End If
            Else
                'Nothing�̎���Nothing�ŕԂ�
                csCkinRow = Nothing
            End If


            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csCkinRow
    End Function
#End Region
    '* ����ԍ� 000044 2011/11/09 �ǉ��I��

    '* ����ԍ� 000050 2014/06/25 �ǉ��J�n
#Region "���ʔԍ��}�X�^�̍X�V����"

    ''' <summary>
    ''' ���ʔԍ��}�X�^�̍X�V����
    ''' </summary>
    ''' <param name="csDataRow">�Z��f�[�^</param>
    ''' <returns>�X�V���茋�ʁiTrue�F�X�V����AFalse�F�X�V���Ȃ��j</returns>
    ''' <remarks></remarks>
    Private Function IsUpdateMyNumber(
        ByVal csDataRow As DataRow) As Boolean

        Dim blnResult As Boolean = False

        Try

            ' �������R����
            Select Case csDataRow.Item(ABJukiData.SHORIJIYUCD).ToString
                '* ����ԍ� 000054 2014/12/26 �C���J�n
                '* ����ԍ� 000052 2014/09/10 �C���J�n
                '* ����ԍ� 000051 2014/07/08 �C���J�n
                'Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00")
                '    ' "02"�i����ǉ��j�A"10"�i�]���j�A"11"�i�o���j�A"12"�i�E���L�ځj
                '    ' "05"�i�l�ԍ��C���j�A"48"�i�l�ԍ��ύX�����j�A"49"�i�l�ԍ��E���L�ځj
                'Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00")
                '    ' "02"�i����ǉ��j�A"10"�i�]���j�A"11"�i�o���j�A"12"�i�E���L�ځj
                '    ' "43"�i�]�o����j�A"44"�i�񕜁j
                '    ' "05"�i�l�ԍ��C���j�A"06"�i�l�ԍ��E���L�ځj�A"48"�i�l�ԍ��ύX�����j�A"49"�i�l�ԍ��E���C���j
                'Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"), _
                '     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00")
                '    ' "02"�i����ǉ��j�A"10"�i�]���j�A"11"�i�o���j�A"12"�i�E���L�ځj
                '    ' "41"�i�E���C���j�A"43"�i�]�o����j�A"44"�i�񕜁j
                '    ' "05"�i�l�ԍ��C���j�A"06"�i�l�ԍ��E���L�ځj�A"48"�i�l�ԍ��ύX�����j�A"49"�i�l�ԍ��E���C���j
                Case ABEnumDefine.ABJukiShoriJiyuType.TokushuTsuika.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.Tennyu.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.Shussei.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.ShokkenKisai.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.JushoSettei.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.ShokkenShusei.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.TenshutsuTorikeshi.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.Kaifuku.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShusei.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoKisai.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoHenko.GetHashCode.ToString("00"),
                     ABEnumDefine.ABJukiShoriJiyuType.KojinNoShokkenShusei.GetHashCode.ToString("00")
                    ' "02"�i����ǉ��j�A"10"�i�]���j�A"11"�i�o���j�A"12"�i�E���L�ځj�A"15"�i�Z���ݒ�j
                    ' "41"�i�E���C���j�A"43"�i�]�o����j�A"44"�i�񕜁j�A"45"�i�]���ʒm�󗝁j
                    ' "05"�i�l�ԍ��C���j�A"06"�i�l�ԍ��E���L�ځj�A"48"�i�l�ԍ��ύX�����j�A"49"�i�l�ԍ��E���C���j
                    '* ����ԍ� 000051 2014/07/08 �C���I��
                    '* ����ԍ� 000052 2014/09/10 �C���I��
                    '* ����ԍ� 000054 2014/12/26 �C���I��
                    blnResult = True
                Case Else
                    blnResult = False
            End Select

        Catch csExp As Exception
            Throw
        End Try

        Return blnResult

    End Function

#End Region

#Region "�Z������"

    '* ����ԍ� 000057 2015/02/17 �폜�J�n
    '''' <summary>
    '''' �Z������
    '''' </summary>
    '''' <param name="csDataRow">�Z��f�[�^</param>
    '''' <returns>�Z�����茋�ʁiTrue�F�Z���AFalse�F�Z���ȊO�j</returns>
    '''' <remarks></remarks>
    'Private Function IsJumin( _
    '    ByVal csDataRow As DataRow) As Boolean

    '    Dim blnResult As Boolean = False

    '    Try

    '        ' �Z������
    '        Select Case csDataRow.Item(ABJukiData.JUMINSHU).ToString
    '            Case ABConstClass.JUMINSHU_NIHONJIN_JUMIN, _
    '                 ABConstClass.JUMINSHU_GAIKOKUJIN_JUMIN
    '                ' "10"�i���{�l�Z���j�A"20"�i�O���l�Z���j
    '                blnResult = True
    '            Case Else
    '                blnResult = False
    '        End Select

    '    Catch csExp As Exception
    '        Throw
    '    End Try

    '    Return blnResult

    'End Function
    '* ����ԍ� 000057 2015/02/17 �폜�I��

#End Region

#Region "���ʔԍ��̎擾"

    ''' <summary>
    ''' ���ʔԍ��̎擾
    ''' </summary>
    ''' <param name="csDataRow">�Z��f�[�^</param>
    ''' <returns>���ʔԍ��i�v�f0�F���ʔԍ��A�v�f1�F�����ʔԍ��j</returns>
    ''' <remarks></remarks>
    Private Function GetMyNumber(
        ByVal csDataRow As DataRow) As String()

        Dim a_strResult() As String = {String.Empty, String.Empty}
        Dim a_strMyNumber() As String
        Const SEPARATOR As String = ","c

        Try

            ' �Z��U�[�u�P���J���}�ŕ�������
            a_strMyNumber = csDataRow.Item(ABJukiData.JUKIRESERVE1).ToString.Split(SEPARATOR.ToCharArray)

            ' ���ʔԍ�
            a_strResult(ABMyNumberType.New) = a_strMyNumber(ABMyNumberType.New)

            ' �����ʔԍ�
            If (a_strMyNumber.Length > 1) Then
                a_strResult(ABMyNumberType.Old) = a_strMyNumber(ABMyNumberType.Old)
            Else
                a_strResult(ABMyNumberType.Old) = String.Empty
            End If

        Catch csExp As Exception
            Throw
        End Try

        Return a_strResult

    End Function

#End Region

#Region "���ʔԍ��p�����[�^�[�N���X�̐ݒ�"

    ''' <summary>
    ''' ���ʔԍ��p�����[�^�[�N���X�̐ݒ�
    ''' </summary>
    ''' <param name="csDataRow">�Z��f�[�^</param>
    ''' <param name="strMyNumber">���ʔԍ�</param>
    ''' <returns>���ʔԍ��p�����[�^�[�N���X</returns>
    ''' <remarks></remarks>
    Private Function SetMyNumber(
        ByVal csDataRow As DataRow,
        ByVal strMyNumber As String) As ABMyNumberPrmXClass

        Dim csResult As ABMyNumberPrmXClass = Nothing

        Try

            csResult = New ABMyNumberPrmXClass
            With csResult
                .p_strJuminCD = csDataRow.Item(ABJukiData.JUMINCD).ToString
                .p_strShichosonCD = csDataRow.Item(ABJukiData.SHICHOSONCD).ToString
                .p_strKyuShichosonCD = csDataRow.Item(ABJukiData.KYUSHICHOSONCD).ToString
                .p_strMyNumber = strMyNumber
                .p_strCkinKB = ABMyNumberEntity.DEFAULT.CKINKB.CKIN
                .p_strIdoKB = ABMyNumberEntity.DEFAULT.IDOKB.JUKIIDO
                .p_strIdoYMD = m_cfRdbClass.GetSystemDate.ToString("yyyyMMdd")
                .p_strIdoSha = m_cfControlData.m_strUserName
                .p_strReserve = String.Empty
            End With

        Catch csExp As Exception
            Throw
        End Try

        Return csResult

    End Function

#End Region

#Region "���ʔԍ��}�X�^�̍X�V����"

    '* ����ԍ� 000054 2014/12/26 �C���J�n
    '''' <summary>
    '''' ���ʔԍ��}�X�^�̍X�V����
    '''' </summary>
    '''' <param name="cABMyNumberPrm">���ʔԍ��p�����[�^�[�N���X</param>
    '''' <param name="strShoriNichiji">��������</param>
    '''' <returns>�X�V����</returns>
    '''' <remarks>�ʏ폈���Ɏg�p���܂��B</remarks>
    'Public Overloads Function UpdateMyNumber( _
    '    ByVal cABMyNumberPrm As ABMyNumberPrmXClass, _
    '    ByVal strShoriNichiji As String) As Integer
    '* ����ԍ� 000056 2015/01/28 �C���J�n
    '''' <summary>
    '''' ���ʔԍ��}�X�^�̍X�V����
    '''' </summary>
    '''' <param name="cABMyNumberPrm">���ʔԍ��p�����[�^�[�N���X</param>
    '''' <param name="strShoriNichiji">��������</param>
    '''' <param name="blnIsJuminFG">�Z���t���O</param>
    '''' <returns>�X�V����</returns>
    '''' <remarks>�ʏ폈���Ɏg�p���܂��B</remarks>
    'Public Overloads Function UpdateMyNumber( _
    '    ByVal cABMyNumberPrm As ABMyNumberPrmXClass, _
    '    ByVal strShoriNichiji As String, _
    '    ByVal blnIsJuminFG As Boolean) As Integer
    ''' <summary>
    ''' ���ʔԍ��}�X�^�̍X�V����
    ''' </summary>
    ''' <param name="cABMyNumberPrm">���ʔԍ��p�����[�^�[�N���X</param>
    ''' <param name="strShoriNichiji">��������</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks>�ʏ폈���Ɏg�p���܂��B</remarks>
    Public Overloads Function UpdateMyNumber(
        ByVal cABMyNumberPrm As ABMyNumberPrmXClass,
        ByVal strShoriNichiji As String) As Integer
        '* ����ԍ� 000056 2015/01/28 �C���I��
        '* ����ԍ� 000054 2014/12/26 �C���I��

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim intKoshinCount As Integer
        Dim intCount As Integer
        Dim cfErrorClass As UFErrorClass
        Dim objErrorStruct As UFErrorStruct
        Dim csABMyNumberEntity As DataSet
        Dim csABMyNumberRuisekiEntity As DataSet
        Dim csDataSet As DataSet
        Dim csNewRow As DataRow
        Dim csRrkDataSet As DataSet
        Dim strShoriKB As String
        Dim csABMyNumberHyojunEntity As DataSet
        Dim csABMyNumberRuisekiHyojunEntity As DataSet
        Dim csMyNumberDS As DataSet
        Dim csHyojunNewRow As DataRow
        Dim csRuisekiNewRow As DataRow

        Try

            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ---------------------------------------------------------------------------------------------------------

            ' ���ʔԍ��̃X�L�[�}���擾
            csABMyNumberEntity = m_cfRdbClass.GetTableSchema(ABMyNumberEntity.TABLE_NAME)

            ' ���ʔԍ��ݐς̃X�L�[�}���擾
            csABMyNumberRuisekiEntity = m_cfRdbClass.GetTableSchema(ABMyNumberRuisekiEntity.TABLE_NAME)

            '���ʔԍ��W��
            csABMyNumberHyojunEntity = m_cfRdbClass.GetTableSchema(ABMyNumberHyojunEntity.TABLE_NAME)
            '���ʔԍ��ݐϕW��
            csABMyNumberRuisekiHyojunEntity = m_cfRdbClass.GetTableSchema(ABMyNumberRuisekiHyojunEntity.TABLE_NAME)

            ' ---------------------------------------------------------------------------------------------------------
            ' �y���ʔԍ����ݗL���𔻒�z

            If (cABMyNumberPrm.p_strMyNumber.Trim.RLength > 0) Then
                ' noop
            Else
                ' ���ʔԍ��ɒl�����݂��Ȃ����߁A�X�V����0�ɂď����𗣒E����B�i�ʏ폈���ł́A�l�Ȃ��ł̍X�V�͍s��Ȃ��B�j
                Return 0
            End If

            '* ����ԍ� 000056 2015/01/28 �폜�J�n
            ''* ����ԍ� 000054 2014/12/26 �ǉ��J�n
            '' ---------------------------------------------------------------------------------------------------------
            '' �y���ʔԍ��}�X�^�̃��R�[�h�L���𔻒�z�@�����[�҂ɑ΂���X�V�̍l��

            '' �Z���t���O�𔻒�
            'If (blnIsJuminFG = True) Then
            '    ' noop
            'Else

            '    ' �������R�[�h�̎擾
            '    csDataSet = m_cABMyNumberB.SelectByJuminCd(cABMyNumberPrm.p_strJuminCD, String.Empty)

            '    If (csDataSet IsNot Nothing _
            '        AndAlso csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then
            '        ' �������R�[�h�����݂��邽�߁A�X�V����0�ɂď����𗣒E����B�i���[�҂ɑ΂��ẮA�V�K�t�Ԃ̂ݍs���B�j
            '        Return 0
            '    Else
            '        ' noop
            '    End If

            'End If

            '' ---------------------------------------------------------------------------------------------------------
            ''* ����ԍ� 000054 2014/12/26 �ǉ��I��
            '* ����ԍ� 000056 2015/01/28 �폜�I��

            ' ---------------------------------------------------------------------------------------------------------
            ' �y���߂̋��ʔԍ��ύX�L���𔻒�z

            ' ���߃��R�[�h�̎擾
            csDataSet = m_cABMyNumberB.SelectByJuminCd(cABMyNumberPrm.p_strJuminCD)

            If (csDataSet IsNot Nothing _
                 AndAlso csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then

                ' ���ʔԍ��̕ύX�L���𔻒�
                If (cABMyNumberPrm.p_strMyNumber.Trim =
                    csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0).Item(ABMyNumberEntity.MYNUMBER).ToString.Trim) Then
                    ' ���߂̋��ʔԍ��ɕύX���Ȃ����߁A�X�V����0�ɂď����𗣒E����B
                    Return 0
                Else
                    ' noop
                End If

            Else
                ' noop
            End If

            ' ---------------------------------------------------------------------------------------------------------
            ' �y���ʔԍ��̍X�V�z

            ' �X�V�㓯��L�[���R�[�h�̎擾
            csDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber)

            If (csDataSet IsNot Nothing _
                 AndAlso csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then

                ' -----------------------------------------------------------------------------------------------------

                ' �����敪��ݒ�
                strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.UPD

                ' -----------------------------------------------------------------------------------------------------
                ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ��O�j�z

                ' ���ʔԍ��ݐ�DataRow�̐���
                csNewRow = CreateMyNumberRuiseki(
                                csABMyNumberRuisekiEntity,
                                csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0),
                                strShoriNichiji,
                                strShoriKB,
                                ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE)

                ' ���ʔԍ��ݐϒǉ�����
                m_cABMyNumberRuisekiB.Insert(csNewRow)

                ' -----------------------------------------------------------------------------------------------------
                ' �y���ʔԍ��̍X�V�z

                With csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)
                    .BeginEdit()
                    .Item(ABMyNumberEntity.CKINKB) = cABMyNumberPrm.p_strCkinKB
                    .EndEdit()
                End With
                intCount = m_cABMyNumberB.Update(csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0))
                If (intCount <> 1) Then
                    cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                    Throw New UFAppException(String.Concat(objErrorStruct.m_strErrorMessage, "���ʔԍ�"), objErrorStruct.m_strErrorCode)
                Else
                    ' noop
                End If

                ' �ԐM�l�֐ݒ�
                intKoshinCount += intCount

                ' -----------------------------------------------------------------------------------------------------

            Else

                ' -----------------------------------------------------------------------------------------------------

                ' �����敪��ݒ�
                strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.INS

                ' -----------------------------------------------------------------------------------------------------
                ' �y���ʔԍ��̒ǉ��z

                csNewRow = csABMyNumberEntity.Tables(ABMyNumberEntity.TABLE_NAME).NewRow
                With csNewRow
                    .BeginEdit()
                    .Item(ABMyNumberEntity.JUMINCD) = cABMyNumberPrm.p_strJuminCD
                    .Item(ABMyNumberEntity.SHICHOSONCD) = cABMyNumberPrm.p_strShichosonCD
                    .Item(ABMyNumberEntity.KYUSHICHOSONCD) = cABMyNumberPrm.p_strKyuShichosonCD
                    .Item(ABMyNumberEntity.MYNUMBER) = cABMyNumberPrm.p_strMyNumber
                    .Item(ABMyNumberEntity.CKINKB) = cABMyNumberPrm.p_strCkinKB
                    .Item(ABMyNumberEntity.IDOKB) = cABMyNumberPrm.p_strIdoKB
                    .Item(ABMyNumberEntity.IDOYMD) = cABMyNumberPrm.p_strIdoYMD
                    .Item(ABMyNumberEntity.IDOSHA) = cABMyNumberPrm.p_strIdoSha
                    .Item(ABMyNumberEntity.RESERVE) = cABMyNumberPrm.p_strReserve
                    .EndEdit()
                End With
                csABMyNumberEntity.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Add(csNewRow)
                intKoshinCount += m_cABMyNumberB.Insert(csNewRow)

                ' -----------------------------------------------------------------------------------------------------
                '���ʔԍ��W��
                csMyNumberDS = m_csABMyNumberHyojunB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber)
                If (csMyNumberDS IsNot Nothing _
                    AndAlso csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                Else
                    csHyojunNewRow = CreateMyNumberHyojun(csABMyNumberHyojunEntity, cABMyNumberPrm)
                    m_csABMyNumberHyojunB.Insert(csHyojunNewRow)
                    '���ʔԍ��ݐϕW��
                    csRuisekiNewRow = CreateMyNumberRuisekiHyojun(csABMyNumberRuisekiHyojunEntity, csHyojunNewRow,
                                                                  cABMyNumberPrm, strShoriNichiji, ABMyNumberRuisekiHyojunEntity.DEFAULT.ZENGOKB.ATO)
                    m_csAbMyNumberRuisekiHyojunB.Insert(csRuisekiNewRow)
                End If
            End If

            ' ---------------------------------------------------------------------------------------------------------
            ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ���j�z

            ' �X�V�㓯��L�[���R�[�h�̎擾
            csDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber)

            If (csDataSet IsNot Nothing _
                 AndAlso csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then

                ' ���ʔԍ��ݐ�DataRow�̐���
                csNewRow = CreateMyNumberRuiseki(
                                csABMyNumberRuisekiEntity,
                                csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0),
                                strShoriNichiji,
                                strShoriKB,
                                ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO)

                ' ���ʔԍ��ݐϒǉ�����
                m_cABMyNumberRuisekiB.Insert(csNewRow)

            Else
                ' noop
            End If

            ' ---------------------------------------------------------------------------------------------------------
            ' �y���������̃C���N�������g�z

            strShoriNichiji = (CType(strShoriNichiji, Long) + 1000).ToString

            ' ---------------------------------------------------------------------------------------------------------
            ' �y�X�V�㓯��L�[���R�[�h�ȊO�𗚗����z

            ' �S�������R�[�h�̎擾
            csDataSet = m_cABMyNumberB.SelectByJuminCd(cABMyNumberPrm.p_strJuminCD, String.Empty)

            If (csDataSet IsNot Nothing _
                 AndAlso csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then

                For Each csDataRow As DataRow In csDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows

                    ' ���ʔԍ��l�L������
                    If (csDataRow.Item(ABMyNumberEntity.MYNUMBER).ToString.Trim.RLength > 0) Then

                        ' �X�V�㓯��L�[���R�[�h����
                        If (cABMyNumberPrm.p_strMyNumber.Trim =
                            csDataRow.Item(ABMyNumberEntity.MYNUMBER).ToString.Trim) Then
                            ' noop
                        Else

                            ' ���ߔ���
                            If (csDataRow.Item(ABMyNumberEntity.CKINKB).ToString = ABMyNumberEntity.DEFAULT.CKINKB.CKIN) Then

                                ' -------------------------------------------------------------------------------------

                                ' �����敪��ݒ�
                                strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.UPD

                                ' -------------------------------------------------------------------------------------
                                ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ��O�j�z

                                ' ���ʔԍ��ݐ�DataRow�̐���
                                csNewRow = CreateMyNumberRuiseki(
                                                csABMyNumberRuisekiEntity,
                                                csDataRow,
                                                strShoriNichiji,
                                                strShoriKB,
                                                ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE)

                                ' ���ʔԍ��ݐϒǉ�����
                                m_cABMyNumberRuisekiB.Insert(csNewRow)

                                ' -------------------------------------------------------------------------------------
                                ' �y�������R�[�h�̗������z

                                csDataRow.BeginEdit()
                                csDataRow.Item(ABMyNumberEntity.CKINKB) = ABMyNumberEntity.DEFAULT.CKINKB.RRK
                                csDataRow.EndEdit()
                                intCount = m_cABMyNumberB.Update(csDataRow)
                                If (intCount <> 1) Then
                                    cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                                    Throw New UFAppException(String.Concat(objErrorStruct.m_strErrorMessage, "���ʔԍ�"), objErrorStruct.m_strErrorCode)
                                Else
                                    ' noop
                                End If

                                ' �ԐM�l�֐ݒ�
                                intKoshinCount += intCount

                                ' -------------------------------------------------------------------------------------
                                ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ���j�z

                                ' �������������R�[�h�̎擾
                                csRrkDataSet = m_cABMyNumberB.SelectByKey(
                                                csDataRow.Item(ABMyNumberEntity.JUMINCD).ToString,
                                                csDataRow.Item(ABMyNumberEntity.MYNUMBER).ToString)

                                If (csRrkDataSet IsNot Nothing _
                                     AndAlso csRrkDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then

                                    ' ���ʔԍ��ݐ�DataRow�̐���
                                    csNewRow = CreateMyNumberRuiseki(
                                                    csABMyNumberRuisekiEntity,
                                                    csRrkDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0),
                                                    strShoriNichiji,
                                                    strShoriKB,
                                                    ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO)

                                    ' ���ʔԍ��ݐϒǉ�����
                                    m_cABMyNumberRuisekiB.Insert(csNewRow)

                                Else
                                    ' noop
                                End If

                                ' -------------------------------------------------------------------------------------
                                ' �y���������̃C���N�������g�z

                                strShoriNichiji = (CType(strShoriNichiji, Long) + 1000).ToString

                                ' -------------------------------------------------------------------------------------

                            Else
                                ' noop
                            End If

                            ' -----------------------------------------------------------------------------------------

                        End If

                        ' ---------------------------------------------------------------------------------------------

                    Else

                        ' ---------------------------------------------------------------------------------------------

                        ' �����敪��ݒ�
                        strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.DEL

                        ' ---------------------------------------------------------------------------------------------
                        ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ��O�j�z

                        ' ���ʔԍ��ݐ�DataRow�̐���
                        csNewRow = CreateMyNumberRuiseki(
                                        csABMyNumberRuisekiEntity,
                                        csDataRow,
                                        strShoriNichiji,
                                        strShoriKB,
                                        ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE)

                        ' ���ʔԍ��ݐϒǉ�����
                        m_cABMyNumberRuisekiB.Insert(csNewRow)

                        ' ---------------------------------------------------------------------------------------------
                        ' �y���ʔԍ��Ȃ����R�[�h�̍폜�z

                        intCount = m_cABMyNumberB.Delete(csDataRow)
                        If (intCount <> 1) Then
                            cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                            Throw New UFAppException(String.Concat(objErrorStruct.m_strErrorMessage, "���ʔԍ�"), objErrorStruct.m_strErrorCode)
                        Else
                            ' noop
                        End If

                        ' �ԐM�l�֐ݒ�
                        intKoshinCount += intCount

                        ' ---------------------------------------------------------------------------------------------
                        ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ���j�z

                        ' ���ʔԍ��ݐ�DataRow�̐���
                        csNewRow.BeginEdit()
                        csNewRow.Item(ABMyNumberRuisekiEntity.ZENGOKB) = ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO
                        csNewRow.EndEdit()

                        ' ���ʔԍ��ݐϒǉ�����
                        m_cABMyNumberRuisekiB.Insert(csNewRow)

                        ' ---------------------------------------------------------------------------------------------
                        ' �y���������̃C���N�������g�z

                        strShoriNichiji = (CType(strShoriNichiji, Long) + 1000).ToString

                        ' ---------------------------------------------------------------------------------------------

                    End If

                    ' -------------------------------------------------------------------------------------------------

                Next csDataRow

            Else
                ' noop
            End If

            ' ---------------------------------------------------------------------------------------------------------

            ' �f�o�b�O�I�����O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        End Try

        Return intKoshinCount

    End Function

    '* ����ԍ� 000057 2015/02/17 �C���J�n
    '''' <summary>
    '''' ���ʔԍ��}�X�^�̍X�V����
    '''' </summary>
    '''' <param name="cABMyNumberPrm">���ʔԍ��p�����[�^�[�N���X</param>
    '''' <param name="strShoriNichiji">��������</param>
    '''' <param name="strOldMyNumber">�����ʔԍ�</param>
    '''' <param name="blnIsJuminFG">�Z���t���O</param>
    '''' <returns>�X�V����</returns>
    '''' <remarks>���ꏈ���Ɏg�p���܂��B</remarks>
    'Public Overloads Function UpdateMyNumber( _
    '    ByVal cABMyNumberPrm As ABMyNumberPrmXClass, _
    '    ByVal strShoriNichiji As String, _
    '    ByVal strOldMyNumber As String, _
    '    ByVal blnIsJuminFG As Boolean) As Integer
    ''' <summary>
    ''' ���ʔԍ��}�X�^�̍X�V����
    ''' </summary>
    ''' <param name="cABMyNumberPrm">���ʔԍ��p�����[�^�[�N���X</param>
    ''' <param name="strShoriNichiji">��������</param>
    ''' <param name="strOldMyNumber">�����ʔԍ�</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks>���ꏈ���Ɏg�p���܂��B</remarks>
    Public Overloads Function UpdateMyNumber(
        ByVal cABMyNumberPrm As ABMyNumberPrmXClass,
        ByVal strShoriNichiji As String,
        ByVal strOldMyNumber As String) As Integer
        '* ����ԍ� 000057 2015/02/17 �C���I��

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim intKoshinCount As Integer
        Dim intCount As Integer
        Dim cfErrorClass As UFErrorClass
        Dim objErrorStruct As UFErrorStruct
        Dim csABMyNumberEntity As DataSet
        Dim csABMyNumberRuisekiEntity As DataSet
        Dim csNewRow As DataRow
        Dim csMaeDataSet As DataSet
        Dim csAtoDataSet As DataSet
        Dim strShoriKB As String
        Dim csABMyNumberHyojunEntity As DataSet
        Dim csABMyNumberRuisekiHyojunEntity As DataSet
        Dim csMyNumberDS As DataSet
        Dim csHyojunNewRow As DataRow
        Dim csRuisekiNewRow As DataRow

        Try

            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ---------------------------------------------------------------------------------------------------------

            ' ���ʔԍ��̃X�L�[�}���擾
            csABMyNumberEntity = m_cfRdbClass.GetTableSchema(ABMyNumberEntity.TABLE_NAME)

            ' ���ʔԍ��ݐς̃X�L�[�}���擾
            csABMyNumberRuisekiEntity = m_cfRdbClass.GetTableSchema(ABMyNumberRuisekiEntity.TABLE_NAME)

            '���ʔԍ��W��
            csABMyNumberHyojunEntity = m_cfRdbClass.GetTableSchema(ABMyNumberHyojunEntity.TABLE_NAME)
            '���ʔԍ��ݐϕW��
            csABMyNumberRuisekiHyojunEntity = m_cfRdbClass.GetTableSchema(ABMyNumberRuisekiHyojunEntity.TABLE_NAME)
            ' ---------------------------------------------------------------------------------------------------------
            ' �y���ʔԍ��̕ύX�L���𔻒�z

            If (cABMyNumberPrm.p_strMyNumber.Trim = strOldMyNumber.Trim) Then
                ' ���ʔԍ��ɕύX���Ȃ����߁A�X�V����0�ɂď����𗣒E����B
                Return 0
            Else
                ' noop
            End If

            ' ---------------------------------------------------------------------------------------------------------
            ' �y�X�V�O����L�[���R�[�h�̑��ݗL���𔻒�z

            ' �X�V�O����L�[���R�[�h���擾
            csMaeDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, strOldMyNumber)

            If (csMaeDataSet IsNot Nothing _
                 AndAlso csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then

                ' -----------------------------------------------------------------------------------------------------

                ' �����敪��ݒ�
                strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.DEL

                ' -----------------------------------------------------------------------------------------------------
                ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ��O�j�z

                ' ���ʔԍ��ݐ�DataRow�̐���
                csNewRow = CreateMyNumberRuiseki(
                                csABMyNumberRuisekiEntity,
                                csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0),
                                strShoriNichiji,
                                strShoriKB,
                                ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE)

                ' ���ʔԍ��ݐϒǉ�����
                m_cABMyNumberRuisekiB.Insert(csNewRow)

                ' -----------------------------------------------------------------------------------------------------
                ' �y�X�V�O����L�[���R�[�h�̍폜�z

                intCount = m_cABMyNumberB.Delete(csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0))
                If (intCount <> 1) Then
                    cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                    Throw New UFAppException(String.Concat(objErrorStruct.m_strErrorMessage, "���ʔԍ�"), objErrorStruct.m_strErrorCode)
                Else
                    ' noop
                End If

                ' �ԐM�l�֐ݒ�
                intKoshinCount += intCount

                ' -----------------------------------------------------------------------------------------------------
                ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ���j�z

                ' ���ʔԍ��ݐ�DataRow�̐���
                csNewRow.BeginEdit()
                csNewRow.Item(ABMyNumberRuisekiEntity.ZENGOKB) = ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO
                csNewRow.EndEdit()

                ' ���ʔԍ��ݐϒǉ�����
                m_cABMyNumberRuisekiB.Insert(csNewRow)

                '���ʔԍ��W���폜
                csMyNumberDS = m_csABMyNumberHyojunB.SelectByKey(cABMyNumberPrm.p_strJuminCD, strOldMyNumber)
                If (csMyNumberDS IsNot Nothing _
                    AndAlso csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                    '���ʔԍ��ݐϕW��-�O�ǉ�
                    csRuisekiNewRow = CreateMyNumberRuisekiHyojun(csABMyNumberRuisekiEntity,
                                      csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows(0),
                                      cABMyNumberPrm, strShoriNichiji, ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE)
                    m_csAbMyNumberRuisekiHyojunB.Insert(csRuisekiNewRow)
                    '���ʔԍ��W���폜
                    m_csABMyNumberHyojunB.Delete(csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows(0))
                    '���ʔԍ��ݐϕW��-��ǉ�
                    csRuisekiNewRow.BeginEdit()
                    csRuisekiNewRow.Item(ABMyNumberRuisekiHyojunEntity.ZENGOKB) = ABMyNumberRuisekiHyojunEntity.DEFAULT.ZENGOKB.ATO
                    csRuisekiNewRow.EndEdit()
                    m_csAbMyNumberRuisekiHyojunB.Insert(csRuisekiNewRow)
                Else
                End If

                ' -----------------------------------------------------------------------------------------------------
                ' �y���������̃C���N�������g�z

                strShoriNichiji = (CType(strShoriNichiji, Long) + 1000).ToString

                ' -----------------------------------------------------------------------------------------------------

                ' �X�V�㓯��L�[���R�[�h���擾
                csAtoDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber)

                If (csAtoDataSet IsNot Nothing _
                     AndAlso csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then

                    ' -------------------------------------------------------------------------------------------------
                    ' �y�X�V�O����L�[���R�[�h�̒��ߋ敪�ƍX�V�㓯��L�[���R�[�h�̒��ߋ敪�𔻒�z

                    If (csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0).Item(ABMyNumberEntity.CKINKB).ToString =
                        ABMyNumberEntity.DEFAULT.CKINKB.CKIN _
                        AndAlso
                        csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0).Item(ABMyNumberEntity.CKINKB).ToString =
                        ABMyNumberEntity.DEFAULT.CKINKB.RRK) Then
                        ' �X�V�O����L�[���R�[�h�̒��ߋ敪��"1"�A���X�V�㓯��L�[���R�[�h�̒��ߋ敪��"0"�̏ꍇ

                        ' ---------------------------------------------------------------------------------------------

                        ' �����敪��ݒ�
                        strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.UPD

                        ' ---------------------------------------------------------------------------------------------
                        ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ��O�j�z

                        ' ���ʔԍ��ݐ�DataRow�̐���
                        csNewRow = CreateMyNumberRuiseki(
                                        csABMyNumberRuisekiEntity,
                                        csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0),
                                        strShoriNichiji,
                                        strShoriKB,
                                        ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.MAE)

                        ' ���ʔԍ��ݐϒǉ�����
                        m_cABMyNumberRuisekiB.Insert(csNewRow)

                        ' ---------------------------------------------------------------------------------------------
                        ' �y���ʔԍ��̍X�V�z

                        With csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)
                            .BeginEdit()
                            .Item(ABMyNumberEntity.CKINKB) = ABMyNumberEntity.DEFAULT.CKINKB.CKIN
                            .EndEdit()
                        End With
                        intCount = m_cABMyNumberB.Update(csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0))
                        If (intCount <> 1) Then
                            cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                            Throw New UFAppException(String.Concat(objErrorStruct.m_strErrorMessage, "���ʔԍ�"), objErrorStruct.m_strErrorCode)
                        Else
                            ' noop
                        End If

                        ' �ԐM�l�֐ݒ�
                        intKoshinCount += intCount

                        ' ---------------------------------------------------------------------------------------------
                        ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ���j�z

                        ' �X�V�㓯��L�[���R�[�h�̎擾
                        csAtoDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber)

                        If (csAtoDataSet IsNot Nothing _
                             AndAlso csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then

                            ' ���ʔԍ��ݐ�DataRow�̐���
                            csNewRow = CreateMyNumberRuiseki(
                                            csABMyNumberRuisekiEntity,
                                            csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0),
                                            strShoriNichiji,
                                            strShoriKB,
                                            ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO)

                            ' ���ʔԍ��ݐϒǉ�����
                            m_cABMyNumberRuisekiB.Insert(csNewRow)

                        Else
                            ' noop
                        End If

                        ' ---------------------------------------------------------------------------------------------
                        ' �y���������̃C���N�������g�z

                        strShoriNichiji = (CType(strShoriNichiji, Long) + 1000).ToString

                        ' ---------------------------------------------------------------------------------------------

                    Else
                        ' noop
                    End If

                    ' -------------------------------------------------------------------------------------------------

                Else

                    ' -------------------------------------------------------------------------------------------------

                    ' �����敪��ݒ�
                    strShoriKB = ABMyNumberRuisekiEntity.DEFAULT.SHORIKB.INS

                    ' -------------------------------------------------------------------------------------------------
                    ' �y���ʔԍ��̒ǉ��z

                    ' �X�V���������ADELETE/INSERT�ŏ���������̂ŕK�v���ڂ̂ݏ㏑���Ƃ���B
                    ' ����L�[�̍X�V�𔺂��AUPDATE���s��Ȃ��悤�ɂ��邽�߁B�i�ٓ��ݐςւ̔z�����܂ށB�j
                    With csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0)
                        .BeginEdit()
                        .Item(ABMyNumberEntity.MYNUMBER) = cABMyNumberPrm.p_strMyNumber
                        .Item(ABMyNumberEntity.IDOKB) = cABMyNumberPrm.p_strIdoKB
                        .Item(ABMyNumberEntity.IDOYMD) = cABMyNumberPrm.p_strIdoYMD
                        .Item(ABMyNumberEntity.IDOSHA) = cABMyNumberPrm.p_strIdoSha
                        .EndEdit()
                    End With
                    intKoshinCount += m_cABMyNumberB.Insert(csMaeDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0))

                    ' -------------------------------------------------------------------------------------------------
                    ' �y���ʔԍ��ݐσ}�X�^�̍X�V�i�ٓ���j�z

                    ' �X�V�㓯��L�[���R�[�h�̎擾
                    csAtoDataSet = m_cABMyNumberB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber)

                    If (csAtoDataSet IsNot Nothing _
                         AndAlso csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows.Count > 0) Then

                        ' ���ʔԍ��ݐ�DataRow�̐���
                        csNewRow = CreateMyNumberRuiseki(
                                        csABMyNumberRuisekiEntity,
                                        csAtoDataSet.Tables(ABMyNumberEntity.TABLE_NAME).Rows(0),
                                        strShoriNichiji,
                                        strShoriKB,
                                        ABMyNumberRuisekiEntity.DEFAULT.ZENGOKB.ATO)

                        ' ���ʔԍ��ݐϒǉ�����
                        m_cABMyNumberRuisekiB.Insert(csNewRow)

                    Else
                        ' noop
                    End If

                    '���ʔԍ��W��
                    csMyNumberDS = m_csABMyNumberHyojunB.SelectByKey(cABMyNumberPrm.p_strJuminCD, cABMyNumberPrm.p_strMyNumber)
                    If (csMyNumberDS IsNot Nothing _
                    AndAlso csMyNumberDS.Tables(ABMyNumberHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                    Else
                        csHyojunNewRow = CreateMyNumberHyojun(csABMyNumberHyojunEntity, cABMyNumberPrm)
                        m_csABMyNumberHyojunB.Insert(csHyojunNewRow)
                        '���ʔԍ��ݐϕW��
                        csRuisekiNewRow = CreateMyNumberRuisekiHyojun(csABMyNumberRuisekiEntity, csHyojunNewRow,
                                                                  cABMyNumberPrm, strShoriNichiji, ABMyNumberRuisekiHyojunEntity.DEFAULT.ZENGOKB.ATO)
                        m_csAbMyNumberRuisekiHyojunB.Insert(csRuisekiNewRow)
                    End If
                    ' -------------------------------------------------------------------------------------------------
                    ' �y���������̃C���N�������g�z

                    strShoriNichiji = (CType(strShoriNichiji, Long) + 1000).ToString

                    ' -------------------------------------------------------------------------------------------------

                End If
                ' -----------------------------------------------------------------------------------------------------

            Else

                ' -----------------------------------------------------------------------------------------------------

                '* ����ԍ� 000057 2015/02/17 �C���J�n
                ' �Z��������s��Ȃ����ƂƂ���B
                ' ���[�҂ɑ΂���C���̏ꍇ�ɁA
                ' �{�����߂Ƃ��ׂ��łȂ��ԍ������߂ƂȂ�\�������邪�A
                ' �������X�V�ł��������m�F���Ă��������^�p��O�ꂷ�邱�ƂƂ���B
                '' �Z���t���O�𔻒�
                'If (blnIsJuminFG = True) Then
                '    ' �Z���̏ꍇ�́A�ʏ폈���Ƃ��ď���������B
                '    '* ����ԍ� 000054 2014/12/26 �C���J�n
                '    'Return Me.UpdateMyNumber(cABMyNumberPrm, strShoriNichiji)
                '    '* ����ԍ� 000056 2015/01/28 �C���J�n
                '    'Return Me.UpdateMyNumber(cABMyNumberPrm, strShoriNichiji, blnIsJuminFG)
                '    Return Me.UpdateMyNumber(cABMyNumberPrm, strShoriNichiji)
                '    '* ����ԍ� 000056 2015/01/28 �C���I��
                '    '* ����ԍ� 000054 2014/12/26 �C���I��
                'Else
                '    ' �X�V�Ώۃ��R�[�h�����݂��Ȃ����߁A�X�V����0�ɂď����𗣒E����B
                '    Return 0
                'End If
                Return Me.UpdateMyNumber(cABMyNumberPrm, strShoriNichiji)
                '* ����ԍ� 000057 2015/02/17 �C���I��

                ' -----------------------------------------------------------------------------------------------------

            End If

            ' ---------------------------------------------------------------------------------------------------------

            ' �f�o�b�O�I�����O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + csExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        End Try

        Return intKoshinCount

    End Function

#End Region

#Region "���ʔԍ��ݐ�DataRow�̐���"

    ''' <summary>
    ''' ���ʔԍ��ݐ�DataRow�̐���
    ''' </summary>
    ''' <param name="csMyNumberRuisekiEntity">���ʔԍ��ݐσ}�X�^</param>
    ''' <param name="csDataRow">�Ώ�DataRow</param>
    ''' <param name="strShoriNichiji">��������</param>
    ''' <param name="strShoriKB">�����敪</param>
    ''' <param name="strZengoKB">�O��敪</param>
    ''' <returns>���ʔԍ��ݐ�DataRow</returns>
    ''' <remarks></remarks>
    Private Function CreateMyNumberRuiseki(
        ByVal csMyNumberRuisekiEntity As DataSet,
        ByVal csDataRow As DataRow,
        ByVal strShoriNichiji As String,
        ByVal strShoriKB As String,
        ByVal strZengoKB As String) As DataRow

        Dim csNewRow As DataRow

        Try

            csNewRow = csMyNumberRuisekiEntity.Tables(ABMyNumberRuisekiEntity.TABLE_NAME).NewRow
            With csNewRow
                .BeginEdit()
                .Item(ABMyNumberRuisekiEntity.JUMINCD) = csDataRow.Item(ABMyNumberEntity.JUMINCD)
                .Item(ABMyNumberRuisekiEntity.SHICHOSONCD) = csDataRow.Item(ABMyNumberEntity.SHICHOSONCD)
                .Item(ABMyNumberRuisekiEntity.KYUSHICHOSONCD) = csDataRow.Item(ABMyNumberEntity.KYUSHICHOSONCD)
                .Item(ABMyNumberRuisekiEntity.MYNUMBER) = csDataRow.Item(ABMyNumberEntity.MYNUMBER)
                .Item(ABMyNumberRuisekiEntity.SHORINICHIJI) = strShoriNichiji
                .Item(ABMyNumberRuisekiEntity.SHORIKB) = strShoriKB
                .Item(ABMyNumberRuisekiEntity.ZENGOKB) = strZengoKB
                .Item(ABMyNumberRuisekiEntity.CKINKB) = csDataRow.Item(ABMyNumberEntity.CKINKB)
                .Item(ABMyNumberRuisekiEntity.IDOKB) = csDataRow.Item(ABMyNumberEntity.IDOKB)
                .Item(ABMyNumberRuisekiEntity.IDOYMD) = csDataRow.Item(ABMyNumberEntity.IDOYMD)
                .Item(ABMyNumberRuisekiEntity.IDOSHA) = csDataRow.Item(ABMyNumberEntity.IDOSHA)
                .Item(ABMyNumberRuisekiEntity.RESERVE) = csDataRow.Item(ABMyNumberEntity.RESERVE)
                .EndEdit()
            End With
            csMyNumberRuisekiEntity.Tables(ABMyNumberRuisekiEntity.TABLE_NAME).Rows.Add(csNewRow)

        Catch csExp As Exception
            Throw
        End Try

        Return csNewRow

    End Function

#End Region
    '* ����ԍ� 000050 2014/06/25 �ǉ��I��

#Region "�����W��������"
    '************************************************************************************************
    '* ���\�b�h��     �����W���nDataRwo����������
    '* 
    '* �\��           Private Sub ClearAtenaHyojun(ByVal csRow As DataRow)
    '* 
    '* �@�\           �����W���nDataRow�̏��������s��
    '* 
    '* ����           csRow As DataRow     : �����W��Row
    '************************************************************************************************
    Private Sub ClearAtenaHyojun(ByVal csRow As DataRow)
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���ڂ̏�����
            For Each csDataColumn As DataColumn In csRow.Table.Columns
                Select Case csDataColumn.ColumnName
                    Case ABAtenaHyojunEntity.KOSHINCOUNTER, ABAtenaHyojunEntity.RRKNO,
                         ABAtenaHyojunEntity.EDANO, ABAtenaHyojunEntity.KYOYUNINZU
                        csRow(csDataColumn) = Decimal.Zero
                    Case Else
                        csRow(csDataColumn) = String.Empty
                End Select
            Next csDataColumn

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
    End Sub
#End Region

#Region "�����W���f�[�^�ݒ�"
    '************************************************************************************************
    '* ���\�b�h��     �����W���f�[�^�ݒ菈��
    '* 
    '* �\��           Private Function SetAtenaHyojun(ByVal csRow As DataRow, ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
    '* 
    '* �@�\           �����W���̕ҏW���s��
    '* 
    '* ����           csRow As DataRow             : �����W���f�[�^
    '*                csAtenaRow As DataRow        �F�����f�[�^
    '*                csJukiDataRow As DataRow     �F�Z��f�[�^
    '*
    '* �߂�l         �����W���f�[�^
    '************************************************************************************************
    Private Function SetAtenaHyojun(ByVal csRow As DataRow, ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        '*����ԍ� 000069 2024/07/09 �ǉ��J�n
        Dim cfDate As UFDateClass
        '*����ԍ� 000069 2024/07/09 �ǉ��I��
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '*����ԍ� 000069 2024/07/09 �ǉ��J�n
            ' ���t�N���X
            cfDate = New UFDateClass(m_cfConfigDataClass)
            cfDate.p_enDateFillType = UFDateFillType.Zero
            cfDate.p_enDateSeparator = UFDateSeparator.Hyphen
            cfDate.p_enEraType = UFEraType.Number
            '*����ԍ� 000069 2024/07/09 �ǉ��I��

            '�f�[�^�ҏW
            csRow(ABAtenaHyojunEntity.JUMINCD) = csAtenaRow(ABAtenaEntity.JUMINCD)                            ' �Z���R�[�h
            csRow(ABAtenaHyojunEntity.JUMINJUTOGAIKB) = csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB)              ' �Z���Z�o�O�敪
            csRow(ABAtenaHyojunEntity.RRKNO) = csJukiDataRow(ABJukiData.RIREKINO)                             ' ����ԍ�
            csRow(ABAtenaHyojunEntity.EDANO) = csJukiDataRow(ABJukiData.EDANO)                                ' �}�ԍ�
            csRow(ABAtenaHyojunEntity.SHIMEIKANAKAKUNINFG) = csJukiDataRow(ABJukiData.SHIMEIKANAKAKUNINFG)    ' �����t���K�i�m�F�t���O
            If (csJukiDataRow(ABJukiData.FUSHOUMAREBI).ToString.Trim = String.Empty) Then
                csRow(ABAtenaHyojunEntity.UMAREBIFUSHOPTN) = FUSHOPTN_NASHI                                   ' ���N�����s�ڃp�^�[��
            Else
                csRow(ABAtenaHyojunEntity.UMAREBIFUSHOPTN) = FUSHOPTN_FUSHO
            End If
            csRow(ABAtenaHyojunEntity.FUSHOUMAREBI) = csJukiDataRow(ABJukiData.FUSHOUMAREBI)                  ' �s�ڐ��N����
            csRow(ABAtenaHyojunEntity.JIJITSUSTAINUSMEI) = csJukiDataRow(ABJukiData.JIJITSUSTAINUSMEI)        ' ������̐��ю�

            If (csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString.TrimEnd <> String.Empty) Then
                '�]�o�m��
                csRow(ABAtenaHyojunEntity.SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSONCD)                 ' �Z��_�s�撬���R�[�h
                csRow(ABAtenaHyojunEntity.MACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZACD)                       ' �Z��_�����R�[�h
                csRow(ABAtenaHyojunEntity.TODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITODOFUKEN)                         ' �Z��_�s���{��
                csRow(ABAtenaHyojunEntity.SHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSON)                     ' �Z��_�s��S������
                csRow(ABAtenaHyojunEntity.MACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZA)                           ' �Z��_����
                csRow(ABAtenaHyojunEntity.SEARCHJUSHO) = GetSearchMoji(csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString)   ' �����p�Z��
                csRow(ABAtenaHyojunEntity.SEARCHKATAGAKI) = GetSearchMoji(csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI).ToString)  ' �����p����
                csRow(ABAtenaHyojunEntity.BANCHIEDABANSUCHI) = m_cABBanchiEdabanSuchiB.GetBanchiEdabanSuchi(
                                                               csAtenaRow(ABAtenaEntity.BANCHICD1).ToString,
                                                               csAtenaRow(ABAtenaEntity.BANCHICD2).ToString,
                                                               csAtenaRow(ABAtenaEntity.BANCHICD3).ToString)                    ' �Ԓn�}�Ԑ��l
            ElseIf (csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString.TrimEnd <> String.Empty) Then
                '�]�o�\��
                csRow(ABAtenaHyojunEntity.SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSONCD)                ' �Z��_�s�撬���R�[�h
                csRow(ABAtenaHyojunEntity.MACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZACD)                      ' �Z��_�����R�[�h
                csRow(ABAtenaHyojunEntity.TODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEITODOFUKEN)                        ' �Z��_�s���{��
                csRow(ABAtenaHyojunEntity.SHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSON)                    ' �Z��_�s��S������
                csRow(ABAtenaHyojunEntity.MACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZA)                          ' �Z��_����
                csRow(ABAtenaHyojunEntity.SEARCHJUSHO) = GetSearchMoji(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString)  ' �����p�Z��
                csRow(ABAtenaHyojunEntity.SEARCHKATAGAKI) = GetSearchMoji(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI).ToString) ' �����p����
                csRow(ABAtenaHyojunEntity.BANCHIEDABANSUCHI) = m_cABBanchiEdabanSuchiB.GetBanchiEdabanSuchi(
                                                               csAtenaRow(ABAtenaEntity.BANCHICD1).ToString,
                                                               csAtenaRow(ABAtenaEntity.BANCHICD2).ToString,
                                                               csAtenaRow(ABAtenaEntity.BANCHICD3).ToString)                    ' �Ԓn�}�Ԑ��l
            Else
                csRow(ABAtenaHyojunEntity.SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.SHIKUCHOSONCD)                              ' �Z��_�s�撬���R�[�h
                csRow(ABAtenaHyojunEntity.MACHIAZACD) = csJukiDataRow(ABJukiData.MACHIAZACD)                                    ' �Z��_�����R�[�h
                csRow(ABAtenaHyojunEntity.TODOFUKEN) = csJukiDataRow(ABJukiData.TODOFUKEN)                                      ' �Z��_�s���{��
                csRow(ABAtenaHyojunEntity.SHIKUCHOSON) = csJukiDataRow(ABJukiData.SHIKUGUNCHOSON)                               ' �Z��_�s��S������
                csRow(ABAtenaHyojunEntity.MACHIAZA) = csJukiDataRow(ABJukiData.MACHIAZA)                                        ' �Z��_����
                csRow(ABAtenaHyojunEntity.SEARCHJUSHO) = csJukiDataRow(ABJukiData.SEARCHJUSHO)                                  ' �����p�Z��
                csRow(ABAtenaHyojunEntity.SEARCHKATAGAKI) = csJukiDataRow(ABJukiData.SEARCHKATAGAKI)                            ' �����p����
                csRow(ABAtenaHyojunEntity.BANCHIEDABANSUCHI) = csJukiDataRow(ABJukiData.BANCHIEDABANSUCHI)                      ' �Ԓn�}�Ԑ��l
            End If
            csRow(ABAtenaHyojunEntity.KANAKATAGAKI) = String.Empty                                         ' �����t���K�i
            csRow(ABAtenaHyojunEntity.JUSHO_KUNIMEICODE) = String.Empty                                    ' �Z��_�����R�[�h
            csRow(ABAtenaHyojunEntity.JUSHO_KUNIMEITO) = String.Empty                                      ' �Z��_������
            csRow(ABAtenaHyojunEntity.JUSHO_KOKUGAIJUSHO) = String.Empty                                   ' �Z��_���O�Z��
            csRow(ABAtenaHyojunEntity.HON_SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.HON_SHIKUCHOSONCD)     ' �{��_�s�撬���R�[�h
            csRow(ABAtenaHyojunEntity.HON_MACHIAZACD) = csJukiDataRow(ABJukiData.HON_MACHIAZACD)           ' �{��_�����R�[�h
            csRow(ABAtenaHyojunEntity.HON_TODOFUKEN) = csJukiDataRow(ABJukiData.HON_TODOFUKEN)             ' �{��_�s���{��
            csRow(ABAtenaHyojunEntity.HON_SHIKUGUNCHOSON) = csJukiDataRow(ABJukiData.HON_SHIKUGUNCHOSON)   ' �{��_�s��S������
            csRow(ABAtenaHyojunEntity.HON_MACHIAZA) = csJukiDataRow(ABJukiData.HON_MACHIAZA)               ' �{��_����
            csRow(ABAtenaHyojunEntity.CKINIDOWMD) = csJukiDataRow(ABJukiData.CKINIDOWMD)                   ' ���߈ٓ��a��N����
            If (csJukiDataRow(ABJukiData.FUSHOCKINIDOBI).ToString.Trim = String.Empty) Then
                csRow(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN) = FUSHOPTN_NASHI                              ' ���߈ٓ����s�ڃp�^�[��
            Else
                csRow(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN) = FUSHOPTN_FUSHO
            End If
            csRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI) = csJukiDataRow(ABJukiData.FUSHOCKINIDOBI)           ' �s�ڒ��߈ٓ���
            csRow(ABAtenaHyojunEntity.TOROKUIDOBIFUSHOPTN) = FUSHOPTN_NASHI                                ' �o�^�ٓ����s�ڃp�^�[��
            csRow(ABAtenaHyojunEntity.FUSHOTOROKUIDOBI) = String.Empty                                     ' �s�ړo�^�ٓ���
            csRow(ABAtenaHyojunEntity.HYOJUNKISAIJIYUCD) = csJukiDataRow(ABJukiData.HYOJUNKISAIJIYUCD)     ' �L�ڎ��R
            csRow(ABAtenaHyojunEntity.KISAIYMD) = csJukiDataRow(ABJukiData.KISAIYMD)                       ' �L�ڔN����
            csRow(ABAtenaHyojunEntity.KISAIBIFUSHOPTN) = FUSHOPTN_NASHI                                    ' �L�ڔN�����s�ڃp�^�[��
            csRow(ABAtenaHyojunEntity.FUSHOKISAIBI) = String.Empty                                         ' �s�ڋL�ڔN����
            csRow(ABAtenaHyojunEntity.JUTEIIDOBIFUSHOPTN) = FUSHOPTN_NASHI                                 ' �Z��ٓ����s�ڃp�^�[��
            csRow(ABAtenaHyojunEntity.FUSHOJUTEIIDOBI) = String.Empty                                      ' �s�ڏZ��ٓ���
            csRow(ABAtenaHyojunEntity.HYOJUNSHOJOJIYUCD) = csJukiDataRow(ABJukiData.HYOJUNSHOJOJIYUCD)     ' �������R
            csRow(ABAtenaHyojunEntity.KOKUSEKISOSHITSUBI) = String.Empty                                   ' ���Бr����
            csRow(ABAtenaHyojunEntity.SHOJOIDOWMD) = csJukiDataRow(ABJukiData.SHOJOIDOWMD)                 ' �����ٓ��a��N����
            If (csJukiDataRow(ABJukiData.FUSHOSHOJOIDOBI).ToString.Trim = String.Empty) Then
                csRow(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN) = FUSHOPTN_NASHI                             ' �����ٓ����s�ڃp�^�[��
            Else
                csRow(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN) = FUSHOPTN_FUSHO
            End If
            csRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI) = csJukiDataRow(ABJukiData.FUSHOSHOJOIDOBI)                ' �s�ڏ����ٓ���
            csRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENUMAEJ_SHIKUCHOSONCD)  ' �]���O�Z��_�s�撬���R�[�h
            csRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZACD) = csJukiDataRow(ABJukiData.TENUMAEJ_MACHIAZACD)        ' �]���O�Z��_�����R�[�h
            csRow(ABAtenaHyojunEntity.TENUMAEJ_TODOFUKEN) = csJukiDataRow(ABJukiData.TENUMAEJ_TODOFUKEN)          ' �]���O�Z��_�s���{��
            csRow(ABAtenaHyojunEntity.TENUMAEJ_SHIKUCHOSON) = csJukiDataRow(ABJukiData.TENUMAEJ_SHIKUCHOSON)      ' �]���O�Z��_�s��S������
            csRow(ABAtenaHyojunEntity.TENUMAEJ_MACHIAZA) = csJukiDataRow(ABJukiData.TENUMAEJ_MACHIAZA)            ' �]���O�Z��_����
            csRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKICD) = csJukiDataRow(ABJukiData.TENUMAEJ_KOKUSEKICD)        ' �]���O�Z��_�����R�[�h
            csRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUSEKI) = csJukiDataRow(ABJukiData.TENUMAEJ_KOKUSEKI)            ' �]���O�Z��_����
            csRow(ABAtenaHyojunEntity.TENUMAEJ_KOKUGAIJUSHO) = csJukiDataRow(ABJukiData.TENUMAEJ_KOKUGAIJUSHO)    ' �]���O�Z��_���O�Z��
            csRow(ABAtenaHyojunEntity.SAISHUTJ_YUBINNO) = String.Empty                                     ' �ŏI�o�^�Z��_�X�֔ԍ�
            csRow(ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSONCD) = String.Empty                               ' �ŏI�o�^�Z��_�s�撬���R�[�h
            csRow(ABAtenaHyojunEntity.SAISHUTJ_MACHIAZACD) = String.Empty                                  ' �ŏI�o�^�Z��_�����R�[�h
            csRow(ABAtenaHyojunEntity.SAISHUTJ_TODOFUKEN) = String.Empty                                   ' �ŏI�o�^�Z��_�s���{��
            csRow(ABAtenaHyojunEntity.SAISHUTJ_SHIKUCHOSON) = String.Empty                                 ' �ŏI�o�^�Z��_�s��S������
            csRow(ABAtenaHyojunEntity.SAISHUTJ_MACHIAZA) = String.Empty                                    ' �ŏI�o�^�Z��_����
            csRow(ABAtenaHyojunEntity.SAISHUTJ_BANCHI) = String.Empty                                      ' �ŏI�o�^�Z��_�Ԓn���\�L
            csRow(ABAtenaHyojunEntity.SAISHUTJ_KATAGAKI) = String.Empty                                    ' �ŏI�o�^�Z��_����
            csRow(ABAtenaHyojunEntity.SAISHUJ_TODOFUKEN) = String.Empty                                    ' �ŏI�Z��_�s���{��
            csRow(ABAtenaHyojunEntity.SAISHUJ_SHIKUCHOSON) = String.Empty                                  ' �ŏI�Z��_�s��S������
            csRow(ABAtenaHyojunEntity.SAISHUJ_MACHIAZA) = String.Empty                                     ' �ŏI�Z��_����
            csRow(ABAtenaHyojunEntity.SAISHUJ_BANCHI) = String.Empty                                       ' �ŏI�Z��_�Ԓn���\�L
            csRow(ABAtenaHyojunEntity.SAISHUJ_KATAGAKI) = String.Empty                                     ' �ŏI�Z��_����
            '* ����ԍ� 000063 2024/02/06 �C���J�n
            'csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSONCD) ' �]�o�\��_�s�撬���R�[�h
            'csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZACD)       ' �]�o�\��_�����R�[�h
            'csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEITODOFUKEN)         ' �]�o�\��_�s���{��
            'csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSON)     ' �]�o�\��_�s��S������
            'csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZA)           ' �]�o�\��_����

            '�Z��f�[�^.�������R�R�[�h��45�i�]���ʒm�󗝁j�̏ꍇ
            If (csJukiDataRow(ABJukiData.SHORIJIYUCD).ToString() = ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00")) Then
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSONCD)  ' �]�o�\��_�s�撬���R�[�h
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZACD)        ' �]�o�\��_�����R�[�h
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITODOFUKEN)          ' �]�o�\��_�s���{��
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSON)      ' �]�o�\��_�s��S������
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZA)            ' �]�o�\��_����
            Else
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSONCD) ' �]�o�\��_�s�撬���R�[�h
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZACD)       ' �]�o�\��_�����R�[�h
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEITODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEITODOFUKEN)         ' �]�o�\��_�s���{��
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEISHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISHIKUCHOSON)     ' �]�o�\��_�s��S������
                csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIMACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIMACHIAZA)           ' �]�o�\��_����
            End If
            '* ����ԍ� 000063 2024/02/06 �C���I��
            csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKICD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKOKUSEKICD)       ' �]�o�\��_�����R�[�h
            csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUSEKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKOKUSEKI)           ' �]�o�\��_������
            csRow(ABAtenaHyojunEntity.TENSHUTSUYOTEIKOKUGAIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKOKUGAIJUSHO)   ' �]�o�\��_���O�Z��
            csRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSONCD)   ' �]�o�m��_�s�撬���R�[�h
            csRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZACD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZACD)         ' �]�o�m��_�����R�[�h
            csRow(ABAtenaHyojunEntity.TENSHUTSUKKTITODOFUKEN) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITODOFUKEN)           ' �]�o�m��_�s���{��
            csRow(ABAtenaHyojunEntity.TENSHUTSUKKTISHIKUCHOSON) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISHIKUCHOSON)       ' �]�o�m��_�s��S������
            csRow(ABAtenaHyojunEntity.TENSHUTSUKKTIMACHIAZA) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMACHIAZA)             ' �]�o�m��_����
            csRow(ABAtenaHyojunEntity.KAISEIBIFUSHOPTN) = FUSHOPTN_NASHI                                   ' �����N�����s�ڃp�^�[��
            csRow(ABAtenaHyojunEntity.FUSHOKAISEIBI) = String.Empty                                        ' �s�ډ����N����
            csRow(ABAtenaHyojunEntity.KAISEISHOJOYMD) = String.Empty                                       ' ���������N����
            csRow(ABAtenaHyojunEntity.KAISEISHOJOBIFUSHOPTN) = FUSHOPTN_NASHI                              ' ���������N�����s�ڃp�^�[��
            csRow(ABAtenaHyojunEntity.FUSHOKAISEISHOJOBI) = String.Empty                                   ' �s�ډ��������N����
            csRow(ABAtenaHyojunEntity.CHIKUCD4) = String.Empty                                             ' �n��R�[�h�S
            csRow(ABAtenaHyojunEntity.CHIKUCD5) = String.Empty                                             ' �n��R�[�h�T
            csRow(ABAtenaHyojunEntity.CHIKUCD6) = String.Empty                                             ' �n��R�[�h�U
            csRow(ABAtenaHyojunEntity.CHIKUCD7) = String.Empty                                             ' �n��R�[�h�V
            csRow(ABAtenaHyojunEntity.CHIKUCD8) = String.Empty                                             ' �n��R�[�h�W
            csRow(ABAtenaHyojunEntity.CHIKUCD9) = String.Empty                                             ' �n��R�[�h�X
            csRow(ABAtenaHyojunEntity.CHIKUCD10) = String.Empty                                            ' �n��R�[�h�P�O
            csRow(ABAtenaHyojunEntity.TOKUBETSUYOSHIKB) = csJukiDataRow(ABJukiData.TOKUBETSUYOSHIKB)       ' ���ʗ{�q�敪
            csRow(ABAtenaHyojunEntity.HYOJUNIDOKB) = csJukiDataRow(ABJukiData.IDOKB)                       ' �ٓ��敪
            csRow(ABAtenaHyojunEntity.NYURYOKUBASHOCD) = csJukiDataRow(ABJukiData.NYURYOKUBASHOCD)         ' ���͏ꏊ�R�[�h
            csRow(ABAtenaHyojunEntity.NYURYOKUBASHO) = csJukiDataRow(ABJukiData.NYURYOKUBASHO)             ' ���͏ꏊ�\�L
            csRow(ABAtenaHyojunEntity.SEARCHKANJIKYUUJI) = csJukiDataRow(ABJukiData.SEARCHKANJIKYUUJI)     ' �����p��������
            csRow(ABAtenaHyojunEntity.SEARCHKANAKYUUJI) = csJukiDataRow(ABJukiData.SEARCHKANAKYUUJI)       ' �����p�J�i����
            csRow(ABAtenaHyojunEntity.KYUUJIKANAKAKUNINFG) = csJukiDataRow(ABJukiData.KYUUJIKANAKAKUNINFG) ' �����t���K�i�m�F�t���O
            csRow(ABAtenaHyojunEntity.TDKDSHIMEI) = csJukiDataRow(ABJukiData.TDKDSHIMEI)                   ' �͏o�l����
            csRow(ABAtenaHyojunEntity.HYOJUNIDOJIYUCD) = csJukiDataRow(ABJukiData.HYOJUNIDOJIYUCD)         ' �W�������ٓ����R�R�[�h
            csRow(ABAtenaHyojunEntity.NICHIJOSEIKATSUKENIKICD) = String.Empty                              ' ���퐶������R�[�h
            csRow(ABAtenaHyojunEntity.KOBOJONOJUSHO_SHOZAICHI_YOMIGANA) = String.Empty                     ' �����̏Z���i���ݒn�j_�ǂ݉���
            csRow(ABAtenaHyojunEntity.TOROKUBUSHO) = String.Empty                                          ' �o�^����
            csRow(ABAtenaHyojunEntity.TANKITAIZAISHAFG) = String.Empty                                     ' �Z���؍ݎ҃t���O
            csRow(ABAtenaHyojunEntity.KYOYUNINZU) = Decimal.Zero                                           ' ���L�Ґl��
            csRow(ABAtenaHyojunEntity.SHIZEIJIMUSHOCD) = String.Empty                                      ' �s�Ŏ������R�[�h
            csRow(ABAtenaHyojunEntity.SHUKKOKUKIKAN_ST) = String.Empty                                     ' �o������_�J�n�N����
            csRow(ABAtenaHyojunEntity.SHUKKOKUKIKAN_ED) = String.Empty                                     ' �o������_�I���N����
            csRow(ABAtenaHyojunEntity.IDOSHURUI) = String.Empty                                            ' �ٓ��̎��
            csRow(ABAtenaHyojunEntity.SHOKANKUCD) = "000000"                                               ' ���ǋ�R�[�h
            csRow(ABAtenaHyojunEntity.TOGOATENAFG) = csJukiDataRow(ABJukiData.TOGOATENAFG)                 ' ���������t���O
            '*����ԍ� 000069 2024/07/09 �C���J�n
            'csRow(ABAtenaHyojunEntity.FUSHOUMAREBI_DATE) = csJukiDataRow(ABJukiData.FUSHOUMAREBIDATE)      ' �s�ڐ��N����DATE
            'csRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE) = csJukiDataRow(ABJukiData.FUSHOCKINIDOBIDATE)  ' �s�ڒ��߈ٓ���DATE
            'csRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE) = csJukiDataRow(ABJukiData.FUSHOSHOJOIDOBIDATE) ' �s�ڏ����ٓ���DATE
            ' �s�ڐ��N����DATE
            If (csRow(ABAtenaHyojunEntity.UMAREBIFUSHOPTN).ToString = FUSHOPTN_FUSHO) Then
                csRow(ABAtenaHyojunEntity.FUSHOUMAREBI_DATE) = csJukiDataRow(ABJukiData.FUSHOUMAREBIDATE)
            Else
                cfDate.p_strDateValue = csJukiDataRow(ABJukiData.UMAREYMD).ToString
                csRow(ABAtenaHyojunEntity.FUSHOUMAREBI_DATE) = cfDate.p_strSeirekiYMD
            End If
            ' �s�ڒ��߈ٓ���DATE
            If (csRow(ABAtenaHyojunEntity.CKINIDOBIFUSHOPTN).ToString = FUSHOPTN_FUSHO) Then
                csRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE) = csJukiDataRow(ABJukiData.FUSHOCKINIDOBIDATE)
            Else
                cfDate.p_strDateValue = csJukiDataRow(ABJukiData.CKINIDOYMD).ToString
                csRow(ABAtenaHyojunEntity.FUSHOCKINIDOBI_DATE) = cfDate.p_strSeirekiYMD
            End If
            ' �s�ڏ����ٓ���DATE
            If (csRow(ABAtenaHyojunEntity.SHOJOIDOBIFUSHOPTN).ToString = FUSHOPTN_FUSHO) Then
                csRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE) = csJukiDataRow(ABJukiData.FUSHOSHOJOIDOBIDATE)
            Else
                cfDate.p_strDateValue = csJukiDataRow(ABJukiData.SHOJOIDOYMD).ToString
                csRow(ABAtenaHyojunEntity.FUSHOSHOJOIDOBI_DATE) = cfDate.p_strSeirekiYMD
            End If
            '*����ԍ� 000069 2024/07/09 �C���I��
            csRow(ABAtenaHyojunEntity.JUKISHIKUCHOSONCD) = csJukiDataRow(ABJukiData.SHIKUCHOSONCD)         ' �Z��Z��_�s�撬���R�[�h
            csRow(ABAtenaHyojunEntity.JUKIMACHIAZACD) = csJukiDataRow(ABJukiData.MACHIAZACD)               ' �Z��Z��_�����R�[�h
            csRow(ABAtenaHyojunEntity.JUKITODOFUKEN) = csJukiDataRow(ABJukiData.TODOFUKEN)                 ' �Z��Z��_�s���{��
            csRow(ABAtenaHyojunEntity.JUKISHIKUCHOSON) = csJukiDataRow(ABJukiData.SHIKUGUNCHOSON)          ' �Z��Z��_�s��S������
            csRow(ABAtenaHyojunEntity.JUKIMACHIAZA) = csJukiDataRow(ABJukiData.MACHIAZA)                   ' �Z��Z��_����
            csRow(ABAtenaHyojunEntity.JUKIKANAKATAGAKI) = String.Empty                                     ' �Z��Z��_�����t���K�i
            csRow(ABAtenaHyojunEntity.JUKICHIKUCD4) = String.Empty                                         ' �Z��n��R�[�h4
            csRow(ABAtenaHyojunEntity.JUKICHIKUCD5) = String.Empty                                         ' �Z��n��R�[�h5
            csRow(ABAtenaHyojunEntity.JUKICHIKUCD6) = String.Empty                                         ' �Z��n��R�[�h6
            csRow(ABAtenaHyojunEntity.JUKICHIKUCD7) = String.Empty                                         ' �Z��n��R�[�h7
            csRow(ABAtenaHyojunEntity.JUKICHIKUCD8) = String.Empty                                         ' �Z��n��R�[�h8
            csRow(ABAtenaHyojunEntity.JUKICHIKUCD9) = String.Empty                                         ' �Z��n��R�[�h9
            csRow(ABAtenaHyojunEntity.JUKICHIKUCD10) = String.Empty                                        ' �Z��n��R�[�h10
            csRow(ABAtenaHyojunEntity.JUKIBANCHIEDABANSUCHI) = csJukiDataRow(ABJukiData.BANCHIEDABANSUCHI) ' �Z��Ԓn�}�Ԑ��l
            csRow(ABAtenaHyojunEntity.RESERVE1) = String.Empty                              ' ���U�[�u�P
            csRow(ABAtenaHyojunEntity.RESERVE2) = String.Empty                              ' ���U�[�u�Q
            csRow(ABAtenaHyojunEntity.RESERVE3) = String.Empty                              ' ���U�[�u�R
            csRow(ABAtenaHyojunEntity.RESERVE4) = String.Empty                              ' ���U�[�u�S
            csRow(ABAtenaHyojunEntity.RESERVE5) = String.Empty                              ' ���U�[�u�T


            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csRow
    End Function
#End Region

#Region "���������쐬"
    '************************************************************************************************
    '* ���\�b�h��     ���������쐬
    '* 
    '* �\��           Private Function GetSearchMoji(ByVal strData As String) As String
    '* 
    '* �@�\           �ގ����E�啶�������s�Ȃ�
    '* 
    '* ����           strData As String     :�Ώۃf�[�^
    '*
    '* �߂�l         �ގ����f�[�^
    '************************************************************************************************
    Private Function GetSearchMoji(ByVal strData As String) As String
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim strResult As String
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�f�[�^�ҏW
            If (strData.Trim = String.Empty) Then
                strResult = String.Empty
            Else
                strResult = m_cuUsRuiji.GetRuijiMojiList(strData.Replace("�@", String.Empty)).ToUpper
            End If

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return strResult
    End Function
#End Region

#Region "�����쐬"
    '************************************************************************************************
    '* ���\�b�h��     ����Row�쐬
    '* 
    '* �\��           Private Function SetAtena(ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
    '* 
    '* �@�\           ����Row���쐬����
    '* 
    '* ����           csAtenaRow As DataRow     :����Rowt
    '*                csJukiDataRow As DataRow  :�Z��f�[�^Row
    '*
    '* �߂�l         ����Row
    '************************************************************************************************
    Private Function SetAtena(ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim strBanchiCD() As String                         ' �Ԓn�R�[�h�擾�p�z��
        Dim cHenshuSearchKana As ABHenshuSearchShimeiBClass ' �����p�J�i�����N���X
        Dim strSearchKana(4) As String                      ' �����p�J�i���̗p

        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            cHenshuSearchKana = New ABHenshuSearchShimeiBClass(m_cfControlData, m_cfConfigDataClass)
            '�f�[�^�ҏW

            ' �Z��f�[�^�̓��ꍀ�ڂ������}�X�^�̍��ڂɃZ�b�g����
            ' �E�Z���R�[�h
            csAtenaRow(ABAtenaEntity.JUMINCD) = csJukiDataRow(ABJukiData.JUMINCD)
            ' �E�s�����R�[�h
            csAtenaRow(ABAtenaEntity.SHICHOSONCD) = csJukiDataRow(ABJukiData.SHICHOSONCD)
            ' �E���s�����R�[�h
            csAtenaRow(ABAtenaEntity.KYUSHICHOSONCD) = csJukiDataRow(ABJukiData.KYUSHICHOSONCD)

            ' �����Z�b�g���Ȃ�����
            ' �E�Z���[�R�[�h
            ' �E�ėp�敪�Q
            ' �E�����@�l�`��
            ' �E�����@�l��\�Ҏ���
            ' �E�Ɖ��~�敪
            ' �E���l�Ŗ�

            ' �ҏW���ăZ�b�g���鍀��
            ' �E�Z���Z�o�O�敪   1
            csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB) = "1"
            ' �E�Z���D��敪     1
            csAtenaRow(ABAtenaEntity.JUMINYUSENIKB) = "1"
            ' �E�Z�o�O�D��敪
            ' �@�@�Z����ʂ̉��P�����h0�h�i�Z���j�łȂ��A���Z�o�O�L��e�k�f���h1�h�̎��A�@0
            '*����ԍ� 000040 2009/05/22 �C���J�n
            '�Ƃ肠������������ "1" �Ƃ��ăZ�b�g����
            csAtenaRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1"
            ' �E�����f�[�^�敪=(11)
            csAtenaRow(ABAtenaEntity.ATENADATAKB) = "11"
            ' �E���уR�[�h�`�����ԍ�
            csAtenaRow(ABAtenaEntity.STAICD) = csJukiDataRow(ABJukiData.STAICD)
            'csAtenaRow(ABAtenaEntity.JUMINHYOCD) = String.Empty
            csAtenaRow(ABAtenaEntity.SEIRINO) = csJukiDataRow(ABJukiData.SEIRINO)
            ' �E�����f�[�^���=(�Z�����)
            csAtenaRow(ABAtenaEntity.ATENADATASHU) = csJukiDataRow(ABJukiData.JUMINSHU)
            ' �E�ėp�敪�P=(�ʂ��敪)
            csAtenaRow(ABAtenaEntity.HANYOKB1) = csJukiDataRow(ABJukiData.UTSUSHIKB)
            ' �E�l�@�l�敪=(1)
            csAtenaRow(ABAtenaEntity.KJNHJNKB) = "1"

            ' �E�J�i���̂P�`�����p�J�i��
            If ((CStr(csJukiDataRow(ABJukiData.SHIMEIRIYOKB)).Trim = "2") AndAlso
                    (CStr(csJukiDataRow(ABJukiData.KANJIMEISHO2)).Trim <> String.Empty)) Then
                ' �{���D��(�{���ƒʏ̖������O���l���������p�敪��"2")
                csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                csAtenaRow(ABAtenaEntity.KANAMEISHO2) = String.Empty
                csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = String.Empty
                csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = GetSearchMoji(csJukiDataRow(ABJukiData.KANJIMEISHO2).ToString)

                ' �����p�J�i�����A�����p�J�i���A�����p�J�i���𐶐����i�[
                strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)),
                                                               String.Empty, m_cFrnHommyoKensakuType)
                ' �ʏ̖��������@�l��\�Ҏ����Ɋi�[
                csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                ' �ėp�敪�Q�Ɏ������p�敪�̃p�����[�^���i�[
                csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB)
                ' �擾���������p�J�i�����A�����p�J�i���A�����p�J�i�����i�[
                csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana(0)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana(1)
                csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana(2)

            ElseIf (m_cFrnHommyoKensakuType = FrnHommyoKensakuType.Tsusho_Seishiki) Then
                ' �ʏ̖��D��(�{���D��̏����ȊO�̏ꍇ)
                csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)
                ' �����p�J�i�����A�����p�J�i���A�����p�J�i���𐶐����i�[
                strSearchKana = cHenshuSearchKana.GetSearchKana(CStr(csJukiDataRow(ABJukiData.KANAMEISHO1)),
                                                                CStr(csJukiDataRow(ABJukiData.KANAMEISHO2)),
                                                                m_cFrnHommyoKensakuType)
                ' �ʏ̖��������@�l��\�Ҏ�������ɂ���
                csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
                ' �ėp�敪�Q�Ɏ������p�敪�̃p�����[�^���i�[
                csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB)
                ' �擾���������p�J�i�����A�����p�J�i���A�����p�J�i�����i�[
                csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = strSearchKana(0)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = strSearchKana(1)
                csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = strSearchKana(2)
            Else
                '�ʏ̖��D��i�������[�U�j
                csAtenaRow(ABAtenaEntity.KANAMEISHO1) = csJukiDataRow(ABJukiData.KANAMEISHO1)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO1) = csJukiDataRow(ABJukiData.KANJIMEISHO1)
                csAtenaRow(ABAtenaEntity.KANAMEISHO2) = csJukiDataRow(ABJukiData.KANAMEISHO2)
                csAtenaRow(ABAtenaEntity.KANJIMEISHO2) = csJukiDataRow(ABJukiData.KANJIMEISHO2)
                ' �ʏ̖��������@�l��\�Ҏ�������ɂ���
                csAtenaRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = String.Empty
                ' �ėp�敪�Q�Ɏ������p�敪�̃p�����[�^���i�[
                csAtenaRow(ABAtenaEntity.HANYOKB2) = csJukiDataRow(ABJukiData.SHIMEIRIYOKB)
                csAtenaRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJukiDataRow(ABJukiData.SEARCHKANJIMEISHO)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJukiDataRow(ABJukiData.SEARCHKANASEIMEI)
                csAtenaRow(ABAtenaEntity.SEARCHKANASEI) = csJukiDataRow(ABJukiData.SEARCHKANASEI)
                csAtenaRow(ABAtenaEntity.SEARCHKANAMEI) = csJukiDataRow(ABJukiData.SEARCHKANAMEI)
            End If
            csAtenaRow(ABAtenaEntity.KYUSEI) = csJukiDataRow(ABJukiData.KYUSEI)

            ' �E�Z���ԍ�=(����ԍ�)
            csAtenaRow(ABAtenaEntity.JUKIRRKNO) = CStr(csJukiDataRow(ABJukiData.RIREKINO)).RSubstring(2, 4)
            ' �E�����J�n�N�����`�Z���[�\����
            csAtenaRow(ABAtenaEntity.RRKST_YMD) = csJukiDataRow(ABJukiData.RRKST_YMD)
            csAtenaRow(ABAtenaEntity.RRKED_YMD) = csJukiDataRow(ABJukiData.RRKED_YMD)
            csAtenaRow(ABAtenaEntity.UMAREYMD) = csJukiDataRow(ABJukiData.UMAREYMD)
            csAtenaRow(ABAtenaEntity.UMAREWMD) = csJukiDataRow(ABJukiData.UMAREWMD)
            csAtenaRow(ABAtenaEntity.SEIBETSUCD) = csJukiDataRow(ABJukiData.SEIBETSUCD)
            csAtenaRow(ABAtenaEntity.SEIBETSU) = csJukiDataRow(ABJukiData.SEIBETSU)
            csAtenaRow(ABAtenaEntity.SEKINO) = csJukiDataRow(ABJukiData.SEIKINO)
            csAtenaRow(ABAtenaEntity.JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.JUMINHYOHYOJIJUN)
            ' �E��Q�Z���[�\����
            csAtenaRow(ABAtenaEntity.DAI2JUMINHYOHYOJIJUN) = csJukiDataRow(ABJukiData.HYOJIJUN2)
            ' �E�����R�[�h�E�����E��2�����R�[�h�E��2����
            ' �@�Z����ʂ̉��P�����h8�h�i�]�o�ҁj�̏ꍇ�ő������h01�h�i���ю�j�̏ꍇ�A�Ǘ����̃R�[�h�ɕύX���A			
            '   ���̂̓N���A����
            If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) = "8") Then
                If (csJukiDataRow(ABJukiData.ZOKUGARACD).ToString.TrimEnd = "02") Then
                    If (m_strZokugara1Init = "00") Then
                        csAtenaRow(ABAtenaEntity.ZOKUGARACD) = String.Empty
                        csAtenaRow(ABAtenaEntity.ZOKUGARA) = String.Empty
                    Else
                        csAtenaRow(ABAtenaEntity.ZOKUGARACD) = m_strZokugara1Init
                        csAtenaRow(ABAtenaEntity.ZOKUGARA) = CNS_KURAN
                    End If
                Else
                    csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD)
                    csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA)
                End If
                If (csJukiDataRow(ABJukiData.ZOKUGARACD2).ToString.TrimEnd = "02") Then
                    If (m_strZokugara2Init = "00") Then
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = String.Empty
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = String.Empty
                    Else
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = m_strZokugara2Init
                        csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = CNS_KURAN
                    End If
                Else
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2)
                    csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2)
                End If
            Else
                ' �Z����ʂ̉��P�����h8�h�i�]�o�ҁj�łȂ��ꍇ�́A���̂܂܃Z�b�g			
                csAtenaRow(ABAtenaEntity.ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD)
                csAtenaRow(ABAtenaEntity.ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA)
                csAtenaRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJukiDataRow(ABJukiData.ZOKUGARACD2)
                csAtenaRow(ABAtenaEntity.DAI2ZOKUGARA) = csJukiDataRow(ABJukiData.ZOKUGARA2)
            End If
            ' �E���ю�Z���R�[�h�`�J�i��Q���ю喼
            csAtenaRow(ABAtenaEntity.STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD)
            csAtenaRow(ABAtenaEntity.STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI)
            csAtenaRow(ABAtenaEntity.KANASTAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI)
            csAtenaRow(ABAtenaEntity.DAI2STAINUSJUMINCD) = csJukiDataRow(ABJukiData.STAINUSJUMINCD2)
            csAtenaRow(ABAtenaEntity.DAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANJISTAINUSMEI2)
            csAtenaRow(ABAtenaEntity.KANADAI2STAINUSMEI) = csJukiDataRow(ABJukiData.KANASTAINUSMEI2)

            ' �E�X�֔ԍ��`����
            ' �E�]�o�m��Z��������ꍇ�́A�]�o�m�藓����Z�b�g�i�Ȃ����ڂ̓Z�b�g�Ȃ��j
            If (csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO).ToString.TrimEnd <> String.Empty) Then
                csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO)
                csAtenaRow(ABAtenaEntity.JUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD), String)
                csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO)
                ' �Ԓn��񂩂�Ԓn�R�[�h���擾
                strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(CStr(csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)))
                csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD(0)
                csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD(1)
                csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD(2)
                csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)
                csAtenaRow(ABAtenaEntity.KATAGAKIFG) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKICD) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI)
                ' �Ǔ��ǊO�敪�F�ǊO�ɃZ�b�g    ���R�����g:�]�o�m��Z�������݂���ꍇ�͊ǊO�ɐݒ肷��B
                csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"

            ElseIf (csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO).ToString.TrimEnd <> String.Empty) Then
                ' �E�]�o�m��Z���������A�]�o�\��Z��������ꍇ�́A�]�o�\�藓����Z�b�g�i�Ȃ����ڂ̓Z�b�g�Ȃ��j
                csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
                csAtenaRow(ABAtenaEntity.JUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String)
                csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
                ' �Ԓn��񂩂�Ԓn�R�[�h���擾
                strBanchiCD = m_cBanchiCDHenshuB.CreateBanchiCD(CStr(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)))
                csAtenaRow(ABAtenaEntity.BANCHICD1) = strBanchiCD(0)
                csAtenaRow(ABAtenaEntity.BANCHICD2) = strBanchiCD(1)
                csAtenaRow(ABAtenaEntity.BANCHICD3) = strBanchiCD(2)
                csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
                csAtenaRow(ABAtenaEntity.KATAGAKIFG) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKICD) = String.Empty
                csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)
                ' �Ǔ��ǊO�敪�F�ǊO�ɃZ�b�g    ���R�����g:�]�o�\��Z�������݂���ꍇ�͊ǊO�ɐݒ肷��B
                csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "2"

            Else
                ' �E�����������ꍇ�́A�Z��Z��������Z�b�g
                csAtenaRow(ABAtenaEntity.YUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO)
                csAtenaRow(ABAtenaEntity.JUSHOCD) = CType(csJukiDataRow(ABJukiData.JUKIJUSHOCD), String).RPadLeft(13)
                csAtenaRow(ABAtenaEntity.JUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO)
                csAtenaRow(ABAtenaEntity.BANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1)
                csAtenaRow(ABAtenaEntity.BANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2)
                csAtenaRow(ABAtenaEntity.BANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3)
                csAtenaRow(ABAtenaEntity.BANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI)
                csAtenaRow(ABAtenaEntity.KATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG)
                csAtenaRow(ABAtenaEntity.KATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20)
                csAtenaRow(ABAtenaEntity.KATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI)
                ' �Ǔ��ǊO�敪�F�Ǔ��ɃZ�b�g    ���R�����g:�]�o�m��Z���A�]�o�\��Z�������݂��Ȃ��ꍇ�͊Ǔ��ɐݒ肷��B
                csAtenaRow(ABAtenaEntity.KANNAIKANGAIKB) = "1"

            End If
            ' �E�A����P�`�����N����
            csAtenaRow(ABAtenaEntity.RENRAKUSAKI1) = csJukiDataRow(ABJukiData.RENRAKUSAKI1)
            csAtenaRow(ABAtenaEntity.RENRAKUSAKI2) = csJukiDataRow(ABJukiData.RENRAKUSAKI2)
            csAtenaRow(ABAtenaEntity.HON_ZJUSHOCD) = CType(csJukiDataRow(ABJukiData.HON_ZJUSHOCD), String)
            csAtenaRow(ABAtenaEntity.HON_JUSHO) = csJukiDataRow(ABJukiData.HON_JUSHO)
            csAtenaRow(ABAtenaEntity.HONSEKIBANCHI) = csJukiDataRow(ABJukiData.HON_BANCHI)
            csAtenaRow(ABAtenaEntity.HITTOSH) = csJukiDataRow(ABJukiData.HITTOSHA)
            csAtenaRow(ABAtenaEntity.CKINIDOYMD) = csJukiDataRow(ABJukiData.CKINIDOYMD)
            csAtenaRow(ABAtenaEntity.CKINJIYUCD) = csJukiDataRow(ABJukiData.CKINJIYUCD)
            csAtenaRow(ABAtenaEntity.CKINJIYU) = csJukiDataRow(ABJukiData.CKINJIYU)
            csAtenaRow(ABAtenaEntity.CKINTDKDYMD) = csJukiDataRow(ABJukiData.CKINTDKDYMD)
            csAtenaRow(ABAtenaEntity.CKINTDKDTUCIKB) = csJukiDataRow(ABJukiData.CKINTDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.TOROKUIDOYMD) = csJukiDataRow(ABJukiData.TOROKUIDOYMD)
            csAtenaRow(ABAtenaEntity.TOROKUIDOWMD) = csJukiDataRow(ABJukiData.TOROKUIDOWMD)
            csAtenaRow(ABAtenaEntity.TOROKUJIYUCD) = csJukiDataRow(ABJukiData.TOROKUJIYUCD)
            csAtenaRow(ABAtenaEntity.TOROKUJIYU) = csJukiDataRow(ABJukiData.TOROKUJIYU)
            csAtenaRow(ABAtenaEntity.TOROKUTDKDYMD) = csJukiDataRow(ABJukiData.TOROKUTDKDYMD)
            csAtenaRow(ABAtenaEntity.TOROKUTDKDWMD) = csJukiDataRow(ABJukiData.TOROKUTDKDWMD)
            csAtenaRow(ABAtenaEntity.TOROKUTDKDTUCIKB) = csJukiDataRow(ABJukiData.TOROKUTDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.JUTEIIDOYMD) = csJukiDataRow(ABJukiData.JUTEIIDOYMD)
            csAtenaRow(ABAtenaEntity.JUTEIIDOWMD) = csJukiDataRow(ABJukiData.JUTEIIDOWMD)
            csAtenaRow(ABAtenaEntity.JUTEIJIYUCD) = csJukiDataRow(ABJukiData.JUTEIJIYUCD)
            csAtenaRow(ABAtenaEntity.JUTEIJIYU) = csJukiDataRow(ABJukiData.JUTEIJIYU)
            csAtenaRow(ABAtenaEntity.JUTEITDKDYMD) = csJukiDataRow(ABJukiData.JUTEITDKDYMD)
            csAtenaRow(ABAtenaEntity.JUTEITDKDWMD) = csJukiDataRow(ABJukiData.JUTEITDKDWMD)
            csAtenaRow(ABAtenaEntity.JUTEITDKDTUCIKB) = csJukiDataRow(ABJukiData.JUTEITDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.SHOJOIDOYMD) = csJukiDataRow(ABJukiData.SHOJOIDOYMD)
            csAtenaRow(ABAtenaEntity.SHOJOJIYUCD) = csJukiDataRow(ABJukiData.SHOJOJIYUCD)
            csAtenaRow(ABAtenaEntity.SHOJOJIYU) = csJukiDataRow(ABJukiData.SHOJOJIYU)
            csAtenaRow(ABAtenaEntity.SHOJOTDKDYMD) = csJukiDataRow(ABJukiData.SHOJOTDKDYMD)
            csAtenaRow(ABAtenaEntity.SHOJOTDKDTUCIKB) = csJukiDataRow(ABJukiData.SHOJOTDKDTUCIKB)
            csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIIDOYMD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIIDOYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIIDOYMD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTITSUCHIYMD) = csJukiDataRow(ABJukiData.TENSHUTSUKKTITUCIYMD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYUCD) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYUCD)
            csAtenaRow(ABAtenaEntity.TENSHUTSUNYURIYU) = csJukiDataRow(ABJukiData.TENSHUTSUNYURIYU)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_YUBINNO) = csJukiDataRow(ABJukiData.TENUMAEJ_YUBINNO)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_ZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENUMAEJ_ZJUSHOCD), String)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_JUSHO) = csJukiDataRow(ABJukiData.TENUMAEJ_JUSHO)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_BANCHI) = csJukiDataRow(ABJukiData.TENUMAEJ_BANCHI)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_KATAGAKI) = csJukiDataRow(ABJukiData.TENUMAEJ_KATAGAKI)
            csAtenaRow(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = csJukiDataRow(ABJukiData.TENUMAEJ_STAINUSMEI)
            '* ����ԍ� 000063 2024/02/06 �C���J�n
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String).RPadLeft(13)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
            'csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)

            '�Z��f�[�^.�������R�R�[�h��45�i�]���ʒm�󗝁j�̏ꍇ
            If (csJukiDataRow(ABJukiData.SHORIJIYUCD).ToString() = ABEnumDefine.ABJukiShoriJiyuType.TennyuTsuchiJuri.GetHashCode.ToString("00")) Then
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD), String)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI)
            Else
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIYUBINNO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUYOTEIZJUSHOCD), String)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIJUSHO)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIBANCHI)
                csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEIKATAGAKI)
            End If
            '* ����ԍ� 000063 2024/02/06 �C���I��

            csAtenaRow(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUYOTEISTAINUSMEI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIYUBINNO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIYUBINNO)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIZJUSHOCD) = CType(csJukiDataRow(ABJukiData.TENSHUTSUKKTIZJUSHOCD), String)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIJUSHO) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIJUSHO)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIBANCHI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIBANCHI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIKATAGAKI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIKATAGAKI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = csJukiDataRow(ABJukiData.TENSHUTSUKKTISTAINUSMEI)
            csAtenaRow(ABAtenaEntity.TENSHUTSUKKTIMITDKFG) = csJukiDataRow(ABJukiData.TENSHUTSUKKTIMITDKFG)
            csAtenaRow(ABAtenaEntity.BIKOYMD) = csJukiDataRow(ABJukiData.BIKOYMD)
            csAtenaRow(ABAtenaEntity.BIKO) = csJukiDataRow(ABJukiData.BIKO)
            csAtenaRow(ABAtenaEntity.BIKOTENSHUTSUKKTIJUSHOFG) = csJukiDataRow(ABJukiData.BIKOTENSHUTSUKKTIJUSHOFG)
            csAtenaRow(ABAtenaEntity.HANNO) = csJukiDataRow(ABJukiData.HANNO)
            csAtenaRow(ABAtenaEntity.KAISEIATOFG) = csJukiDataRow(ABJukiData.KAISEIATOFG)
            csAtenaRow(ABAtenaEntity.KAISEIMAEFG) = csJukiDataRow(ABJukiData.KAISEIMAEFG)
            csAtenaRow(ABAtenaEntity.KAISEIYMD) = csJukiDataRow(ABJukiData.KAISEIYMD)

            ' �E�s����R�[�h�`�n�於�R
            ' �@�Z����ʂ̉��P�����h8�h�i�]�o�ҁj�łȂ��ꍇ�A�Z��s����`�Z��n�於�R���Z�b�g			
            If ((csJukiDataRow(ABJukiData.JUMINSHU).ToString + "  ").RSubstring(1, 1) <> "8") Then
                csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1)
                csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1)
                csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2)
                csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2)
                csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
            Else
                ' �Z����ʂ̉��P�����h8�h�i�]�o�ҁj�̏ꍇ�A�Ǘ����i�s���揉�����`�n��R�j�����āA
                ' �N���A�ɂȂ��Ă���ꍇ�́A�Z�b�g���Ȃ�
                If (m_strGyosekuInit.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = String.Empty
                    csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = String.Empty
                Else
                    If m_strTenshutsuGyoseikuCD.Trim = String.Empty Then
                        ' �N���A���Ȃ��ꍇ�œ]�o�җp�̍s����b�c���ݒ肳��Ă��Ȃ��ꍇ��
                        ' ���̂܂܏Z��̃f�[�^��ݒ肷��B
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
                    Else
                        ' �N���A���Ȃ��ꍇ�œ]�o�җp�̍s����b�c���ݒ肳��Ă���ꍇ��
                        ' �s����b�c�}�X�^���s���於�̂��擾���A�ݒ肷��B
                        csAtenaRow(ABAtenaEntity.GYOSEIKUCD) = m_strTenshutsuGyoseikuCD.RPadLeft(9, " "c)
                        csAtenaRow(ABAtenaEntity.GYOSEIKUMEI) = String.Empty
                    End If
                End If
                If (m_strChiku1Init.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.CHIKUCD1) = String.Empty
                    csAtenaRow(ABAtenaEntity.CHIKUMEI1) = String.Empty
                Else
                    csAtenaRow(ABAtenaEntity.CHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1)
                    csAtenaRow(ABAtenaEntity.CHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1)
                End If
                If (m_strChiku2Init.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.CHIKUCD2) = String.Empty
                    csAtenaRow(ABAtenaEntity.CHIKUMEI2) = String.Empty
                Else
                    csAtenaRow(ABAtenaEntity.CHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2)
                    csAtenaRow(ABAtenaEntity.CHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2)
                End If
                If (m_strChiku3Init.TrimEnd = "1") Then
                    csAtenaRow(ABAtenaEntity.CHIKUCD3) = String.Empty
                    csAtenaRow(ABAtenaEntity.CHIKUMEI3) = String.Empty
                Else
                    csAtenaRow(ABAtenaEntity.CHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
                    csAtenaRow(ABAtenaEntity.CHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
                End If
            End If

            ' �E���[��R�[�h�`�ݗ��I���N����
            csAtenaRow(ABAtenaEntity.TOHYOKUCD) = csJukiDataRow(ABJukiData.TOHYOKUCD).ToString.RPadLeft(5)
            csAtenaRow(ABAtenaEntity.SHOGAKKOKUCD) = csJukiDataRow(ABJukiData.SHOGAKKOKUCD)
            csAtenaRow(ABAtenaEntity.CHUGAKKOKUCD) = csJukiDataRow(ABJukiData.CHUGAKKOKUCD)
            csAtenaRow(ABAtenaEntity.HOGOSHAJUMINCD) = csJukiDataRow(ABJukiData.HOGOSHAJUMINCD)
            csAtenaRow(ABAtenaEntity.KANJIHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANJIHOGOSHAMEI)
            csAtenaRow(ABAtenaEntity.KANAHOGOSHAMEI) = csJukiDataRow(ABJukiData.KANAHOGOSHAMEI)
            csAtenaRow(ABAtenaEntity.KIKAYMD) = csJukiDataRow(ABJukiData.KIKAYMD)
            csAtenaRow(ABAtenaEntity.KARIIDOKB) = csJukiDataRow(ABJukiData.KARIIDOKB)
            csAtenaRow(ABAtenaEntity.SHORITEISHIKB) = csJukiDataRow(ABJukiData.SHORITEISHIKB)
            csAtenaRow(ABAtenaEntity.SHORIYOKUSHIKB) = csJukiDataRow(ABJukiData.SHORIYOKUSHIKB)
            csAtenaRow(ABAtenaEntity.JUKIYUBINNO) = csJukiDataRow(ABJukiData.JUKIYUBINNO)
            csAtenaRow(ABAtenaEntity.JUKIJUSHOCD) = csJukiDataRow(ABJukiData.JUKIJUSHOCD)
            csAtenaRow(ABAtenaEntity.JUKIJUSHO) = csJukiDataRow(ABJukiData.JUKIJUSHO)
            csAtenaRow(ABAtenaEntity.JUKIBANCHICD1) = csJukiDataRow(ABJukiData.JUKIBANCHICD1)
            csAtenaRow(ABAtenaEntity.JUKIBANCHICD2) = csJukiDataRow(ABJukiData.JUKIBANCHICD2)
            csAtenaRow(ABAtenaEntity.JUKIBANCHICD3) = csJukiDataRow(ABJukiData.JUKIBANCHICD3)
            csAtenaRow(ABAtenaEntity.JUKIBANCHI) = csJukiDataRow(ABJukiData.JUKIBANCHI)
            csAtenaRow(ABAtenaEntity.JUKIKATAGAKIFG) = csJukiDataRow(ABJukiData.JUKIKATAGAKIFG)
            csAtenaRow(ABAtenaEntity.JUKIKATAGAKICD) = csJukiDataRow(ABJukiData.JUKIKATAGAKICD).ToString.Trim.RPadLeft(20)
            csAtenaRow(ABAtenaEntity.JUKIKATAGAKI) = csJukiDataRow(ABJukiData.JUKIKATAGAKI)
            csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUCD) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUCD)
            csAtenaRow(ABAtenaEntity.JUKIGYOSEIKUMEI) = csJukiDataRow(ABJukiData.JUKIGYOSEIKUMEI)
            csAtenaRow(ABAtenaEntity.JUKICHIKUCD1) = csJukiDataRow(ABJukiData.JUKICHIKUCD1)
            csAtenaRow(ABAtenaEntity.JUKICHIKUMEI1) = csJukiDataRow(ABJukiData.JUKICHIKUMEI1)
            csAtenaRow(ABAtenaEntity.JUKICHIKUCD2) = csJukiDataRow(ABJukiData.JUKICHIKUCD2)
            csAtenaRow(ABAtenaEntity.JUKICHIKUMEI2) = csJukiDataRow(ABJukiData.JUKICHIKUMEI2)
            csAtenaRow(ABAtenaEntity.JUKICHIKUCD3) = csJukiDataRow(ABJukiData.JUKICHIKUCD3)
            csAtenaRow(ABAtenaEntity.JUKICHIKUMEI3) = csJukiDataRow(ABJukiData.JUKICHIKUMEI3)
            csAtenaRow(ABAtenaEntity.KOKUSEKICD) = csJukiDataRow(ABJukiData.KOKUSEKICD)
            csAtenaRow(ABAtenaEntity.KOKUSEKI) = csJukiDataRow(ABJukiData.KOKUSEKI)
            csAtenaRow(ABAtenaEntity.ZAIRYUSKAKCD) = csJukiDataRow(ABJukiData.ZAIRYUSKAKCD)
            csAtenaRow(ABAtenaEntity.ZAIRYUSKAK) = csJukiDataRow(ABJukiData.ZAIRYUSKAK)
            csAtenaRow(ABAtenaEntity.ZAIRYUKIKAN) = csJukiDataRow(ABJukiData.ZAIRYUKIKAN)
            csAtenaRow(ABAtenaEntity.ZAIRYU_ST_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ST_YMD)
            csAtenaRow(ABAtenaEntity.ZAIRYU_ED_YMD) = csJukiDataRow(ABJukiData.ZAIRYU_ED_YMD)


            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csAtenaRow
    End Function
#End Region

#Region "���������쐬"
    '************************************************************************************************
    '* ���\�b�h��     ���������쐬
    '* 
    '* �\��           Private Function SetAtenaRireki(ByVal csAtenaRirekiRow As DataRow, ByVal csAtenaRow As DataRow) As DataRow
    '* 
    '* �@�\           ����Row���父������Row���쐬����
    '* 
    '* ����           csAtenaRirekiRow As DataRow     :��������Row
    '*                csAtenaRow As DataRow           :����Row
    '*
    '* �߂�l         ��������Row
    '************************************************************************************************
    Private Function SetAtenaRireki(ByVal csAtenaRirekiRow As DataRow, ByVal csAtenaRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim strRirekiSTYMD As String
        Dim strRirekiEDYMD As String
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            strRirekiSTYMD = csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD).ToString
            strRirekiEDYMD = csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD).ToString

            '�f�[�^�ҏW
            For Each csColumn As DataColumn In csAtenaRow.Table.Columns
                If (csAtenaRirekiRow.Table.Columns.Contains(csColumn.ColumnName)) Then
                    '�񂪂������������Z�b�g
                    csAtenaRirekiRow(csColumn.ColumnName) = csAtenaRow(csColumn.ColumnName)
                Else
                    '�������Ȃ�
                End If
            Next
            csAtenaRirekiRow(ABAtenaRirekiEntity.RRKST_YMD) = strRirekiSTYMD
            csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = strRirekiEDYMD

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csAtenaRirekiRow
    End Function
#End Region

#Region "��������W���쐬"
    '************************************************************************************************
    '* ���\�b�h��     ��������W���쐬
    '* 
    '* �\��           Private Function SetAtenaRirekiHyojun(ByVal csAtenaRirekiHyojunRow As DataRow, ByVal csAtenaHyojunRow As DataRow,
    '*                                      ByVal csAtenaRirekiRow As DataRow) As DataRow
    '* 
    '* �@�\           �����W��Row���父������W��Row���쐬����
    '* 
    '* ����           csAtenaRirekiHyojunRow As DataRow     :��������W��Row
    '*                csAtenaHyojunRow As DataRow           :�����W��Row
    '*                csAtenaRirekiRow As DataRow           :��������Row
    '*
    '* �߂�l         ��������W��Row
    '************************************************************************************************
    Private Function SetAtenaRirekiHyojun(ByVal csAtenaRirekiHyojunRow As DataRow, ByVal csAtenaHyojunRow As DataRow,
                                          ByVal csAtenaRirekiRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�f�[�^�ҏW
            For Each csColumn As DataColumn In csAtenaHyojunRow.Table.Columns
                If (csAtenaRirekiHyojunRow.Table.Columns.Contains(csColumn.ColumnName)) Then
                    '�񂪂������������Z�b�g
                    csAtenaRirekiHyojunRow(csColumn.ColumnName) = csAtenaHyojunRow(csColumn.ColumnName)
                Else
                    '�������Ȃ�
                End If
            Next
            csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csAtenaRirekiHyojunRow
    End Function
#End Region

#Region "�����ݐϕW���쐬"
    '************************************************************************************************
    '* ���\�b�h��     �����ݐϕW���쐬
    '* 
    '* �\��           Private Function SetAtenaRuisekiHyojun(ByVal csAtenaRuisekiHyojunRow As DataRow,
    '*                                 ByVal csAtenaRirekiHyojunRow As DataRow, ByVal csAtenaRuisekiRow As DataRow) As DataRow
    '* 
    '* �@�\           ��������W��Row���父���ݐϕW��Row���쐬����
    '* 
    '* ����           csAtenaRuisekiHyojunRow As DataRow :�����ݐϕW��Row
    '*                csAtenaRirekiHyojunRowAs DataRow   :��������W��Row
    '*                csAtenaRuisekiRow As DataRow       :�����ݐ�Row
    '*
    '* �߂�l         �����ݐϕW��Row
    '************************************************************************************************
    Private Function SetAtenaRuisekiHyojun(ByVal csAtenaRuisekiHyojunRow As DataRow,
                                           ByVal csAtenaRirekiHyojunRow As DataRow, ByVal csAtenaRuisekiRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            '�f�[�^�ҏW
            For Each csColumn As DataColumn In csAtenaRirekiHyojunRow.Table.Columns
                If (csAtenaRuisekiHyojunRow.Table.Columns.Contains(csColumn.ColumnName)) Then
                    '�񂪂������������Z�b�g
                    csAtenaRuisekiHyojunRow(csColumn.ColumnName) = csAtenaRirekiHyojunRow(csColumn.ColumnName)
                Else
                    '�������Ȃ�
                End If
            Next
            csAtenaRuisekiHyojunRow(ABAtenaRuisekiHyojunEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI)
            csAtenaRuisekiHyojunRow(ABAtenaRuisekiHyojunEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB)

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csAtenaRuisekiHyojunRow
    End Function
#End Region

#Region "��������W�����߃f�[�^�擾"
    '************************************************************************************************
    '* ���\�b�h��     ��������W�����߃f�[�^�擾
    '* 
    '* �\��           Private Function GetChokkin_RirekiHyojun(ByVal csAtenaRirekiHyojun As DataSet, ByVal strJuminCD As String, ByVal strRirekiNo As String) As DataRow
    '* 
    '* �@�\           ��������W���̒��߃f�[�^���擾����
    '* 
    '* ����           csAtenaRirekiHyojun As DataSet   : ��������W���f�[�^
    '*                strJuminCD As String             : �Z���R�[�h
    '*                strRirekiNo As String            : ����ԍ�
    '*
    '* �߂�l         ��������W���������̏����Ō������A���ʂ̂O�Ԗڂ�Ԃ��B��������Nothing��Ԃ�
    '************************************************************************************************
    Private Function GetChokkin_RirekiHyojun(ByVal csAtenaRirekiHyojun As DataSet, ByVal strJuminCD As String, ByVal strRirekiNo As String) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csSelectedRows As DataRow() '�������ʔz��
        Dim csCkinRow As DataRow        '���ߍs
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (csAtenaRirekiHyojun IsNot Nothing) Then
                '������������W����Nothing�łȂ���
                csSelectedRows = csAtenaRirekiHyojun.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Select(
                    String.Format("{0}='{1}' AND {2}='{3}'",
                          ABAtenaRirekiHyojunEntity.JUMINCD, strJuminCD,
                          ABAtenaRirekiHyojunEntity.RIREKINO, strRirekiNo))
                If (csSelectedRows.Count > 0) Then
                    '���߃f�[�^�����݂������A�O�s�ڂ�����Ă���
                    csCkinRow = csSelectedRows(0)
                Else
                    '����ȊO�̎��ANothing�ŕԂ�
                    csCkinRow = Nothing
                End If
            Else
                'Nothing�̎���Nothing�ŕԂ�
                csCkinRow = Nothing
            End If

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csCkinRow
    End Function
#End Region

#Region "�����t���W��������"
    '************************************************************************************************
    '* ���\�b�h��     �����t���W���nDataRwo����������
    '* 
    '* �\��           Private Sub ClearAtenaFZYHyojun(ByVal csRow As DataRow)
    '* 
    '* �@�\           �����t���W���nDataRow�̏��������s��
    '* 
    '* ����           csRow As DataRow     : �����t���W��Row
    '************************************************************************************************
    Private Sub ClearAtenaFZYHyojun(ByVal csRow As DataRow)
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���ڂ̏�����
            For Each csDataColumn As DataColumn In csRow.Table.Columns
                Select Case csDataColumn.ColumnName
                    Case ABAtenaFZYHyojunEntity.KOSHINCOUNTER
                        csRow(csDataColumn) = Decimal.Zero
                    Case Else
                        csRow(csDataColumn) = String.Empty
                End Select
            Next csDataColumn

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
    End Sub
#End Region

#Region "�����t���W���f�[�^�ݒ�"
    '************************************************************************************************
    '* ���\�b�h��     �����t���W���f�[�^�ݒ菈��
    '* 
    '* �\��           Private Function SetAtenaHyojun(ByVal csRow As DataRow, ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
    '* 
    '* �@�\           �����t���W���̕ҏW���s��
    '* 
    '* ����           csRow As DataRow             : �����t���W���f�[�^
    '*                csAtenaRow As DataRow        �F�����f�[�^
    '*                csJukiDataRow As DataRow     �F�Z��f�[�^
    '*
    '* �߂�l         �����t���W���f�[�^
    '************************************************************************************************
    Private Function SetAtenaFZYHyojun(ByVal csRow As DataRow, ByVal csAtenaRow As DataRow, ByVal csJukiDataRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�f�[�^�ҏW
            csRow(ABAtenaFZYHyojunEntity.JUMINCD) = csAtenaRow(ABAtenaEntity.JUMINCD)                         ' �Z���R�[�h
            csRow(ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB) = csAtenaRow(ABAtenaEntity.JUMINJUTOGAIKB)           ' �Z���Z�o�O�敪
            csRow(ABAtenaFZYHyojunEntity.SEARCHFRNMEI) = csJukiDataRow(ABJukiData.SEARCHFRNMEI)               ' �����p�O���l��
            csRow(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI) = csJukiDataRow(ABJukiData.SEARCHKANAFRNMEI)       ' �����p�J�i�O���l��
            csRow(ABAtenaFZYHyojunEntity.SEARCHTSUSHOMEI) = csJukiDataRow(ABJukiData.SEARCHTSUSHOMEI)         ' �����p�ʏ̖�
            csRow(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI) = csJukiDataRow(ABJukiData.SEARCHKANATSUSHOMEI) ' �����p�J�i�ʏ̖�
            csRow(ABAtenaFZYHyojunEntity.TSUSHOKANAKAKUNINFG) = csJukiDataRow(ABJukiData.TSUSHOKANAKAKUNINFG) ' �ʏ̃t���K�i�m�F�t���O
            '*����ԍ� 000068 2024/07/05 �C���J�n
            'csRow(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB) = csJukiDataRow(ABJukiData.SHIMEIYUSENKB)             ' �����D��敪
            ' �����D��敪
            If (CStr(csJukiDataRow(ABJukiData.SHIMEIYUSENKB)).Trim = "2") AndAlso
               (CStr(csJukiDataRow(ABJukiData.KANJIHEIKIMEI)).Trim <> String.Empty) Then
                ' �����D��敪��2�i�{���D��j ���� ���L������ �̏ꍇ
                ' �����D��敪��3�i���L���D��j��ݒ�
                csRow(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB) = "3"
            Else
                csRow(ABAtenaFZYHyojunEntity.SHIMEIYUSENKB) = csJukiDataRow(ABJukiData.SHIMEIYUSENKB)
            End If
            '*����ԍ� 000068 2024/07/05 �C���I��
            csRow(ABAtenaFZYHyojunEntity.SEARCHKANJIHEIKIMEI) = csJukiDataRow(ABJukiData.SEARCHKANJIHEIKIMEI) ' �����p�������L��
            csRow(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI) = csJukiDataRow(ABJukiData.SEARCHKANAHEIKIMEI)   ' �����p�J�i���L��
            csRow(ABAtenaFZYHyojunEntity.ZAIRYUCARDNOKBN) = csJukiDataRow(ABJukiData.ZAIRYUCARDNOKBN)         ' �ݗ��J�[�h���ԍ��敪
            csRow(ABAtenaFZYHyojunEntity.JUKYOCHIHOSEICD) = csJukiDataRow(ABJukiData.JUKYOCHIHOSEICD)         ' �Z���n�␳�R�[�h
            csRow(ABAtenaFZYHyojunEntity.HODAI30JO46MATAHA47KB) = csJukiDataRow(ABJukiData.HODAI30JO46MATAHA47KB) ' �@��30��46����47�敪
            csRow(ABAtenaFZYHyojunEntity.STAINUSSHIMEIYUSENKB) = String.Empty                                   ' ���ю厁���D��敪
            csRow(ABAtenaFZYHyojunEntity.TOKUSHOMEI_YUKOKIGEN) = String.Empty                                   ' ���ʉi�Z�ҏؖ����L������
            csRow(ABAtenaFZYHyojunEntity.RESERVE1) = String.Empty                                               ' ���U�[�u�P
            csRow(ABAtenaFZYHyojunEntity.RESERVE2) = String.Empty                                               ' ���U�[�u�Q
            csRow(ABAtenaFZYHyojunEntity.RESERVE3) = String.Empty                                               ' ���U�[�u�R
            csRow(ABAtenaFZYHyojunEntity.RESERVE4) = String.Empty                                               ' ���U�[�u�S
            csRow(ABAtenaFZYHyojunEntity.RESERVE5) = String.Empty                                               ' ���U�[�u�T

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csRow
    End Function
#End Region

#Region "��������t���W���쐬"
    '************************************************************************************************
    '* ���\�b�h��     ��������t���W���쐬
    '* 
    '* �\��           Private Function SetAtenaRirekiFZYHyojun(ByVal csAtenaRirekiFZYHyojunRow As DataRow,
    '*                                                         ByVal csAtenaFZYHyojunRow As DataRow) As DataRow
    '* 
    '* �@�\           �����t���W��Row���父������t���W��Row���쐬����
    '* 
    '* ����           csAtenaRirekiFZYRow As DataRow     :��������t���W��Row
    '*                csAtenaFZYHyojunRow As DataRow     :�����t���W��Row
    '*
    '* �߂�l         ��������t���W��Row
    '************************************************************************************************
    Private Function SetAtenaRirekiFZYHyojun(ByVal csAtenaRirekiFZYHyojunRow As DataRow,
                                             ByVal csAtenaFZYHyojunRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�f�[�^�ҏW
            For Each csColumn As DataColumn In csAtenaFZYHyojunRow.Table.Columns
                If (csAtenaRirekiFZYHyojunRow.Table.Columns.Contains(csColumn.ColumnName)) Then
                    '�񂪂������������Z�b�g
                    csAtenaRirekiFZYHyojunRow(csColumn.ColumnName) = csAtenaFZYHyojunRow(csColumn.ColumnName)
                Else
                    '�������Ȃ�
                End If
            Next

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csAtenaRirekiFZYHyojunRow
    End Function
#End Region

#Region "�����ݐϕt���W���쐬"
    '************************************************************************************************
    '* ���\�b�h��     �����ݐϕt���W���쐬
    '* 
    '* �\��           Private Function SetAtenaRuisekiHyojun(ByVal csAtenaRuisekiFZYHyojunRow As DataRow,
    '*                                 ByVal csAtenaRirekiFZYHyojunRow As DataRow, ByVal csAtenaRuisekiRow As DataRow) As DataRow
    '* 
    '* �@�\           ��������t���W��Row���父���ݐϕt���W��Row���쐬����
    '* 
    '* ����           csAtenaRuisekiFZYHyojunRow As DataRow :�����ݐϕt���W��Row
    '*                csAtenaRirekiFZYHyojunRowAs DataRow   :��������t���W��Row
    '*                csAtenaRuisekiRow As DataRow       :�����ݐ�Row
    '*
    '* �߂�l         �����ݐϕt���W��Row
    '************************************************************************************************
    Private Function SetAtenaRuisekiFZYHyojun(ByVal csAtenaRuisekiFZYHyojunRow As DataRow,
                                           ByVal csAtenaRirekiFZYHyojunRow As DataRow, ByVal csAtenaRuisekiRow As DataRow) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            '�f�[�^�ҏW
            For Each csColumn As DataColumn In csAtenaRirekiFZYHyojunRow.Table.Columns
                If (csAtenaRuisekiFZYHyojunRow.Table.Columns.Contains(csColumn.ColumnName)) Then
                    '�񂪂������������Z�b�g
                    csAtenaRuisekiFZYHyojunRow(csColumn.ColumnName) = csAtenaRirekiFZYHyojunRow(csColumn.ColumnName)
                Else
                    '�������Ȃ�
                End If
            Next
            csAtenaRuisekiFZYHyojunRow(ABAtenaRuisekiHyojunEntity.SHORINICHIJI) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.SHORINICHIJI)
            csAtenaRuisekiFZYHyojunRow(ABAtenaRuisekiHyojunEntity.ZENGOKB) = csAtenaRuisekiRow(ABAtenaRuisekiEntity.ZENGOKB)

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csAtenaRuisekiFZYHyojunRow
    End Function
#End Region

#Region "��������t���W�����߃f�[�^�擾"
    '************************************************************************************************
    '* ���\�b�h��     ��������t���W�����߃f�[�^�擾
    '* 
    '* �\��           Private Function GetChokkin_RirekiFZYHyojun(ByVal csAtenaRirekiFZYHyojun As DataSet, ByVal strJuminCD As String, ByVal strRirekiNo As String) As DataRow
    '* 
    '* �@�\           ��������t���W���̒��߃f�[�^���擾����
    '* 
    '* ����           csAtenaRirekiFZYHyojun As DataSet: ��������t���W���f�[�^
    '*                strJuminCD As String             : �Z���R�[�h
    '*                strRirekiNo As String            : ����ԍ�
    '*
    '* �߂�l         ��������t���W���������̏����Ō������A���ʂ̂O�Ԗڂ�Ԃ��B��������Nothing��Ԃ�
    '************************************************************************************************
    Private Function GetChokkin_RirekiFZYHyojun(ByVal csAtenaRirekiFZYHyojun As DataSet,
                                                ByVal strJuminCD As String, ByVal strRirekiNo As String) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csSelectedRows As DataRow() '�������ʔz��
        Dim csCkinRow As DataRow        '���ߍs
        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (csAtenaRirekiFZYHyojun IsNot Nothing) Then
                '������������t���W����Nothing�łȂ���
                csSelectedRows = csAtenaRirekiFZYHyojun.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Select(
                    String.Format("{0}='{1}' AND {2}='{3}'",
                          ABAtenaRirekiFZYHyojunEntity.JUMINCD, strJuminCD,
                          ABAtenaRirekiFZYHyojunEntity.RIREKINO, strRirekiNo))
                If (csSelectedRows.Count > 0) Then
                    '���߃f�[�^�����݂������A�O�s�ڂ�����Ă���
                    csCkinRow = csSelectedRows(0)
                Else
                    '����ȊO�̎��ANothing�ŕԂ�
                    csCkinRow = Nothing
                End If
            Else
                'Nothing�̎���Nothing�ŕԂ�
                csCkinRow = Nothing
            End If

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csCkinRow
    End Function
#End Region

#Region "���ʔԍ��ݐϕW���쐬"
    '************************************************************************************************
    '* ���\�b�h��     ���ʔԍ��ݐϕW���쐬
    '* 
    '* �\��            Private Function CreateMyNumberRuisekiHyojun(ByVal csMyNumberRuisekiHyojunEntity As DataSet,
    '*                                                              ByVal csMyNumberHyojunRow As DataRow, ByVal csMyNumberPrm As ABMyNumberPrmXClass,
    '*                                                              ByVal strShoriNichiji As String, ByVal strZengoKbn As String) As DataRow
    '* 
    '* �@�\           ���ʔԍ��W��Row���狤�ʔԍ��ݐϕW��Row���쐬����
    '* 
    '* ����           csMyNumberRuisekiHyojunEntity As DataSet:���ʔԍ��ݐϕW��DataSet
    '*                csMyNumberHyojunRow As DataRow          :���ʔԍ��W��Row
    '*                csMyNumberPrm                           :���ʔԍ��p�����[�^
    '*                strShoriNichiji As String               :��������
    '*                ByVal strZengoKbn As String             :�O��敪
    '*
    '* �߂�l         ���ʔԍ��ݐϕW��Row
    '************************************************************************************************
    Private Function CreateMyNumberRuisekiHyojun(ByVal csMyNumberRuisekiHyojunEntity As DataSet,
                                                 ByVal csMyNumberHyojunRow As DataRow, ByVal csMyNumberPrm As ABMyNumberPrmXClass,
                                                 ByVal strShoriNichiji As String, ByVal strZengoKbn As String) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csNewRow As DataRow

        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            csNewRow = csMyNumberRuisekiHyojunEntity.Tables(ABMyNumberRuisekiHyojunEntity.TABLE_NAME).NewRow
            '�f�[�^�ҏW
            csNewRow(ABMyNumberRuisekiHyojunEntity.JUMINCD) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.JUMINCD)                 ' �Z���R�[�h
            csNewRow(ABMyNumberRuisekiHyojunEntity.SHICHOSONCD) = csMyNumberPrm.p_strShichosonCD                                  ' �s�����R�[�h
            csNewRow(ABMyNumberRuisekiHyojunEntity.KYUSHICHOSONCD) = csMyNumberPrm.p_strKyuShichosonCD                            ' ���s�����R�[�h
            csNewRow(ABMyNumberRuisekiHyojunEntity.MYNUMBER) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.MYNUMBER)               ' �l�@�l�ԍ�
            csNewRow(ABMyNumberRuisekiHyojunEntity.SHORINICHIJI) = strShoriNichiji                                                ' ��������
            csNewRow(ABMyNumberRuisekiHyojunEntity.ZENGOKB) = strZengoKbn                                                         ' �O��敪
            csNewRow(ABMyNumberRuisekiHyojunEntity.BANGOHOKOSHINKB) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.BANGOHOKOSHINKB) ' �ԍ��@�X�V�敪
            csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE1) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE1)               ' ���U�[�u�P
            csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE2) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE2)               ' ���U�[�u�Q
            csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE3) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE3)               ' ���U�[�u�R
            csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE4) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE4)               ' ���U�[�u�S
            csNewRow(ABMyNumberRuisekiHyojunEntity.RESERVE5) = csMyNumberHyojunRow(ABMyNumberHyojunEntity.RESERVE5)               ' ���U�[�u�T

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csNewRow
    End Function
#End Region

#Region "���ʔԍ��W���쐬"
    '************************************************************************************************
    '* ���\�b�h��     ���ʔԍ��W���쐬
    '* 
    '* �\��           Private Function CreateMyNumberHyojun(ByVal csMyNumberHyojunEntity As DataSet,
    '*                                                      ByVal csMyNumberPrm As ABMyNumberPrmXClass) As DataRow
    '* 
    '* �@�\           ���ʔԍ��W��Row���쐬����
    '* 
    '* ����           csMyNumberHyojunEntity As DataSet:���ʔԍ��ݐϕW��DataSet
    '*                csMyNumberPrm                           :���ʔԍ��p�����[�^
    '*
    '* �߂�l         ���ʔԍ��W��Row
    '************************************************************************************************
    Private Function CreateMyNumberHyojun(ByVal csMyNumberHyojunEntity As DataSet,
                                         ByVal csMyNumberPrm As ABMyNumberPrmXClass) As DataRow
        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csNewRow As DataRow

        Try
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            csNewRow = csMyNumberHyojunEntity.Tables(ABMyNumberHyojunEntity.TABLE_NAME).NewRow
            '�f�[�^�ҏW
            csNewRow(ABMyNumberHyojunEntity.JUMINCD) = csMyNumberPrm.p_strJuminCD    ' �Z���R�[�h
            csNewRow(ABMyNumberHyojunEntity.MYNUMBER) = csMyNumberPrm.p_strMyNumber  ' �l�@�l�ԍ�
            csNewRow(ABMyNumberHyojunEntity.BANGOHOKOSHINKB) = String.Empty          ' �ԍ��@�X�V�敪
            csNewRow(ABMyNumberHyojunEntity.RESERVE1) = String.Empty                 ' ���U�[�u�P
            csNewRow(ABMyNumberHyojunEntity.RESERVE2) = String.Empty                 ' ���U�[�u�Q
            csNewRow(ABMyNumberHyojunEntity.RESERVE3) = String.Empty                 ' ���U�[�u�R
            csNewRow(ABMyNumberHyojunEntity.RESERVE4) = String.Empty                 ' ���U�[�u�S
            csNewRow(ABMyNumberHyojunEntity.RESERVE5) = String.Empty                 ' ���U�[�u�T

            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        Catch objAppExp As UFAppException
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw
        Catch objExp As Exception
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try
        Return csNewRow
    End Function
#End Region

#Region "�Z��f�[�^�X�V�i�����j"
    '************************************************************************************************
    '* ���\�b�h��     �Z��f�[�^�X�V�i�����j
    '* 
    '* �\��           Public Sub JukiDataKoshin08N() 
    '* 
    '* �@�\ �@    �@�@�Z����f�[�^���X�V����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub JukiDataKoshin08N(ByVal csJukiDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "JukiDataKoshin08N"
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        Dim strJuminCD As String                            ' �Z���R�[�h
        Dim strLinkNo As String                             ' �����N�p�A��
        Dim strRirekiNO As String                           ' ����ԍ�
        Dim csAtenaEntity As DataSet                        ' �����}�X�^Entity
        Dim csAtenaRow As DataRow                           ' �����}�X�^Row
        Dim csAtenaRirekiRow As DataRow                     ' ��������Row
        Dim intCount As Integer                             ' �X�V����
        Dim csAtenaRirekiEntity As DataSet                  ' ��������
        Dim csBkAtenaRirekiRow As DataRow
        Dim csAtenaFzyEntity As DataSet                     ' �����t���f�[�^
        Dim csAtenaFzyRow As DataRow                        ' �����t���s
        Dim csAtenaRirekiFzyEntity As DataSet               ' ��������t��
        Dim csAtenaRirekiFzyRow As DataRow                  ' ��������t���s
        Dim csBkAtenaRirekiFzyRow As DataRow                ' ��������t���s
        Dim csAtenaHyojunEntity As DataSet                  ' �����W��
        Dim csAtenaHyojunRow As DataRow                     ' �����W��Row
        Dim csAtenaRirekiHyojunEntity As DataSet            ' ��������W��
        Dim csAtenaRirekiHyojunRow As DataRow               ' ��������W��Row
        Dim csBkAtenaRirekiHyojunRow As DataRow
        Dim csAtenaFZYHyojunEntity As DataSet               ' �����t���W��
        Dim csAtenaFZYHyojunRow As DataRow                  ' �����t���W��Row
        Dim csAtenaRirekiFZyHyojunEntity As DataSet         ' ��������t���W��
        Dim csAtenaRirekiFZYHyojunRow As DataRow            ' ��������t���W��Row
        Dim csBkAtenaRirekiFZYHyojunRow As DataRow
        Dim blnRirekiHyojunUpdate As Boolean
        Dim blnRirekiFZYHyojunUpdate As Boolean
        Dim csDataColumn As DataColumn

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '---------------------------------------------------------------------------------------
            ' 1. �ϐ��̏�����
            '
            '---------------------------------------------------------------------------------------
            strJuminCD = csJukiDataRow(ABJukiData.JUMINCD).ToString    '�Ώۃf�[�^�̏Z���R�[�h���擾
            strLinkNo = csJukiDataRow(ABJukiData.LINKNO).ToString.Trim

            '---------------------------------------------------------------------------------------
            ' 2. �f�[�^�ҏW
            '---------------------------------------------------------------------------------------
            '����
            csAtenaEntity = m_cfRdbClass.GetTableSchema(ABAtenaEntity.TABLE_NAME)
            csAtenaRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).NewRow
            Me.ClearAtena(csAtenaRow)
            csAtenaRow = SetAtena(csAtenaRow, csJukiDataRow)

            '�����t��
            csAtenaFzyEntity = m_cfRdbClass.GetTableSchema(ABAtenaFZYEntity.TABLE_NAME)
            csAtenaFzyRow = csAtenaFzyEntity.Tables(ABAtenaFZYEntity.TABLE_NAME).NewRow
            Me.ClearAtenaFZY(csAtenaFzyRow)
            csAtenaFzyRow = Me.SetAtenaFzy(csAtenaFzyRow, csAtenaRow, csJukiDataRow)

            '�����W��
            csAtenaHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaHyojunEntity.TABLE_NAME)
            csAtenaHyojunRow = csAtenaHyojunEntity.Tables(ABAtenaHyojunEntity.TABLE_NAME).NewRow
            Me.ClearAtenaHyojun(csAtenaHyojunRow)
            csAtenaHyojunRow = Me.SetAtenaHyojun(csAtenaHyojunRow, csAtenaRow, csJukiDataRow)

            '�����t���W��
            csAtenaFZYHyojunEntity = m_cfRdbClass.GetTableSchema(ABAtenaFZYHyojunEntity.TABLE_NAME)
            csAtenaFZYHyojunRow = csAtenaFZYHyojunEntity.Tables(ABAtenaFZYHyojunEntity.TABLE_NAME).NewRow
            Me.ClearAtenaFZYHyojun(csAtenaFZYHyojunRow)
            csAtenaFZYHyojunRow = Me.SetAtenaFZYHyojun(csAtenaFZYHyojunRow, csAtenaRow, csJukiDataRow)

            '---------------------------------------------------------------------------------------
            ' 3. �����f�[�^�擾
            '---------------------------------------------------------------------------------------
            '��������t��
            csAtenaRirekiFzyEntity = m_cAtenaRirekiFzyB.GetAtenaRirekiFZYByLinkNo(strJuminCD, strLinkNo)
            If (csAtenaRirekiFzyEntity Is Nothing) OrElse
                   (csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows.Count <> 1) Then
                ' �G���[��`���擾�i���������̍X�V�ŃG���[���܂����B�j
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003459)
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + strJuminCD + "-" + strLinkNo, objErrorStruct.m_strErrorCode)
            Else
                csAtenaRirekiFzyRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).Rows(0)
                strRirekiNO = csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO).ToString
            End If

            '��������
            csAtenaRirekiEntity = m_cAtenaRirekiB.GetAtenaRirekiByRirekiNO(strJuminCD, strRirekiNO)
            If (csAtenaRirekiEntity Is Nothing) OrElse
                   (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 1) Then
                ' �G���[��`���擾�i���������̍X�V�ŃG���[���܂����B�j
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE003459)
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + strJuminCD + "-" + strRirekiNO, objErrorStruct.m_strErrorCode)
            Else
                csAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0)
            End If

            '��������W��
            csAtenaRirekiHyojunEntity = m_cABAtenaRirekiHyojunB.GetAtenaRirekiHyojunBHoshu(strJuminCD, strRirekiNO, True)
            If (csAtenaRirekiHyojunEntity IsNot Nothing) AndAlso
               (csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).Rows(0)
                blnRirekiHyojunUpdate = True
            Else
                csAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).NewRow
                ClearAtenaHyojun(csAtenaRirekiHyojunRow)
                blnRirekiHyojunUpdate = False
            End If

            '��������t���W��
            csAtenaRirekifzyHyojunEntity = m_cABAtenaRirekiFZYHyojunB.GetAtenaRirekiFZYHyojunBHoshu(strJuminCD, strRirekiNO, True)
            If (csAtenaRirekiFZyHyojunEntity IsNot Nothing) AndAlso
               (csAtenaRirekiFZyHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows.Count > 0) Then
                csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZyHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).Rows(0)
                blnRirekiFZYHyojunUpdate = True
            Else
                csAtenaRirekiFZYHyojunRow = csAtenaRirekiFZyHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).NewRow
                ClearAtenaFZYHyojun(csAtenaRirekiFZYHyojunRow)
                blnRirekiFZYHyojunUpdate = False
            End If

            '---------------------------------------------------------------------------------------
            ' 4. �����f�[�^�ҏW
            '---------------------------------------------------------------------------------------
            '��������
            csBkAtenaRirekiRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow
            For Each csDataColumn In csAtenaRirekiRow.Table.Columns
                csBkAtenaRirekiRow(csDataColumn.ColumnName) = csAtenaRirekiRow(csDataColumn.ColumnName)
            Next csDataColumn
            csAtenaRirekiRow = SetAtenaRireki(csAtenaRirekiRow, csAtenaRow)
            csAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
            csAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB)
            csAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RRKED_YMD)
            csAtenaRirekiRow(ABAtenaRirekiEntity.TANMATSUID) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.TANMATSUID)
            csAtenaRirekiRow(ABAtenaRirekiEntity.SAKUJOFG) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUJOFG)
            csAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINCOUNTER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINCOUNTER)
            csAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEINICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEINICHIJI)
            csAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEIUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEIUSER)
            csAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI)
            csAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINUSER)

            '��������W��
            csBkAtenaRirekiHyojunRow = csAtenaRirekiHyojunEntity.Tables(ABAtenaRirekiHyojunEntity.TABLE_NAME).NewRow
            For Each csDataColumn In csAtenaRirekiHyojunRow.Table.Columns
                csBkAtenaRirekiHyojunRow(csDataColumn.ColumnName) = csAtenaRirekiHyojunRow(csDataColumn.ColumnName)
            Next csDataColumn
            csAtenaRirekiHyojunRow = SetAtenaRirekiHyojun(csAtenaRirekiHyojunRow, csAtenaHyojunRow, csAtenaRirekiRow)
            csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
            csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.JUMINJUTOGAIKB) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB)
            If (blnRirekiHyojunUpdate) Then
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.TANMATSUID) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.TANMATSUID)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUJOFG) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUJOFG)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINCOUNTER) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINCOUNTER)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEIUSER) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEIUSER)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINUSER) = csBkAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINUSER)
            Else
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.TANMATSUID) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.TANMATSUID)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUJOFG) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUJOFG)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINCOUNTER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINCOUNTER)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEINICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEINICHIJI)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.SAKUSEIUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEIUSER)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINNICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI)
                csAtenaRirekiHyojunRow(ABAtenaRirekiHyojunEntity.KOSHINUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINUSER)
            End If

            '��������t��
            csBkAtenaRirekiFzyRow = csAtenaRirekiFzyEntity.Tables(ABAtenaRirekiFZYEntity.TABLE_NAME).NewRow
            For Each csDataColumn In csAtenaRirekiFzyRow.Table.Columns
                csBkAtenaRirekiFzyRow(csDataColumn.ColumnName) = csAtenaRirekiFzyRow(csDataColumn.ColumnName)
            Next csDataColumn
            csAtenaRirekiFzyRow = SetAtenaRirekiFzy(csAtenaRirekiFzyRow, csAtenaFzyRow)
            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.RIREKINO) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.JUMINJUTOGAIKB) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB)
            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.TANMATSUID) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.TANMATSUID)
            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUJOFG) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUJOFG)
            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINCOUNTER)
            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUSEINICHIJI)
            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUSEIUSER) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.SAKUSEIUSER)
            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINNICHIJI)
            csAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINUSER) = csBkAtenaRirekiFzyRow(ABAtenaRirekiFZYEntity.KOSHINUSER)

            '��������t���W��
            csBkAtenaRirekiFZYHyojunRow = csAtenaRirekiFZyHyojunEntity.Tables(ABAtenaRirekiFZYHyojunEntity.TABLE_NAME).NewRow
            For Each csDataColumn In csAtenaRirekiFZYHyojunRow.Table.Columns
                csBkAtenaRirekiFZYHyojunRow(csDataColumn.ColumnName) = csAtenaRirekiFZYHyojunRow(csDataColumn.ColumnName)
            Next csDataColumn
            csAtenaRirekiFZYHyojunRow = SetAtenaRirekiFZYHyojun(csAtenaRirekiFZYHyojunRow, csAtenaFZYHyojunRow)
            csAtenaRirekiFZYHyojunRow(ABAtenaRirekiHyojunEntity.RIREKINO) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.RIREKINO)
            csAtenaRirekiFZYHyojunRow(ABAtenaRirekiHyojunEntity.JUMINJUTOGAIKB) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.JUMINJUTOGAIKB)
            If (blnRirekiFZYHyojunUpdate) Then
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.TANMATSUID) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.TANMATSUID)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUJOFG) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUJOFG)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEIUSER) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEIUSER)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER) = csBkAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER)
            Else
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.TANMATSUID) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.TANMATSUID)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUJOFG) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUJOFG)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINCOUNTER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINCOUNTER)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEINICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEINICHIJI)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.SAKUSEIUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.SAKUSEIUSER)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINNICHIJI) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINNICHIJI)
                csAtenaRirekiFZYHyojunRow(ABAtenaRirekiFZYHyojunEntity.KOSHINUSER) = csBkAtenaRirekiRow(ABAtenaRirekiEntity.KOSHINUSER)
            End If

            '---------------------------------------------------------------------------------------
            ' 5. �����f�[�^�X�V
            '---------------------------------------------------------------------------------------
            If (blnRirekiHyojunUpdate) AndAlso (blnRirekiFZYHyojunUpdate) Then
                intCount = m_cAtenaRirekiB.UpdateAtenaRB(csAtenaRirekiRow, csAtenaRirekiHyojunRow, csAtenaRirekiFzyRow, csAtenaRirekiFZYHyojunRow)
                If (intCount <> 1) Then
                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                Else
                End If
            Else
                '���������E��������t��
                intCount = m_cAtenaRirekiB.UpdateAtenaRB(csAtenaRirekiRow, csAtenaRirekiFzyRow)
                If (intCount <> 1) Then
                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001047)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                Else
                End If
                '��������W��
                If (blnRirekiHyojunUpdate) Then
                    intCount = m_cABAtenaRirekiHyojunB.UpdateAtenaRirekiHyojunB(csAtenaRirekiHyojunRow)
                Else
                    intCount = m_cABAtenaRirekiHyojunB.InsertAtenaRirekiHyojunB(csAtenaRirekiHyojunRow)
                End If
                '��������t���W��
                If (blnRirekiFZYHyojunUpdate) Then
                    intCount = m_cABAtenaRirekiFZYHyojunB.UpdateAtenaRirekiFZYHyojunB(csAtenaRirekiFZYHyojunRow)
                Else
                    intCount = m_cABAtenaRirekiFZYHyojunB.InsertAtenaRirekiFZYHyojunB(csAtenaRirekiFZYHyojunRow)
                End If
            End If

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
            Throw objExp
        End Try

    End Sub
#End Region

    '*����ԍ� 000065 2024/04/02 �ǉ��J�n
#Region "�l����}�X�^�̍X�V"
    '************************************************************************************************
    '* ���\�b�h��     �l����}�X�^�̍X�V
    '* 
    '* �\��           Public Function UpdateKojinSeigyo(ByVal cABKariTorokuPrm As ABKariTorokuParamXClass) As Integer
    '* 
    '* �@�\�@�@    �@ �l����}�X�^�̍X�V���s��
    '* 
    '* ����           cdrJukiData�F�Z��f�[�^
    '* 
    '* �߂�l         �X�V�����FInteger
    '************************************************************************************************
    Public Function UpdateKojinSeigyo(ByVal cdrJukiData As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateKojinSeigyo"          ' ���\�b�h��
        Dim cfErrorClass As UFErrorClass                    '�G���[�����N���X
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim strSeinenHikokenninGaitoUmu As String           '���N��㌩�l_�Y���L��
        Dim strSeinenHikokenninShinpanKakuteiYMD As String  '���N��㌩�l_�R���m���
        Dim strSeinenHikokenninTokiYMD As String            '���N��㌩�l�̓o�L��
        Dim strSeinenHikokenninShittaYMD As String          '���N��㌩�l�ł���|��m������
        Dim strJukiReserve4 As String()                     '�Z��f�[�^_���U�[�u4���Z�p���[�^�ŕ������ĕێ�
        Dim cdsKojinseigyo As DataSet                       '�l����}�X�^DataSet
        Dim cdsKojinseigyoRrk As DataSet                    '�l���䗚��DataSet
        Dim cdrKojinseigyoRow As DataRow                    '�l����}�X�^DataRow
        Dim cdrKojinseigyoRrkRow As DataRow                 '�l���䗚��DataRow
        Dim csColumn As DataColumn                          '�J�������
        Dim csSortDataRow As DataRow()                      '����ԍ��擾�pDataRow
        Dim intKoshinCnt As Integer = 0                     '�l����}�X�^�X�V����
        Dim intRrkKoshinCnt As Integer = 0                  '�l���䗚���X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            With cdrJukiData

                ' ����.�Z��f�[�^����K�v���ڂ��擾
                strJukiReserve4 = .Item(ABJukiData.JUKIRESERVE4).ToString.Split("^"c)
                If (strJukiReserve4.Count = 4) Then
                    strSeinenHikokenninGaitoUmu = strJukiReserve4(0)
                    strSeinenHikokenninShinpanKakuteiYMD = strJukiReserve4(1)
                    strSeinenHikokenninTokiYMD = strJukiReserve4(2)
                    strSeinenHikokenninShittaYMD = strJukiReserve4(3)
                Else
                    strSeinenHikokenninGaitoUmu = "0"
                    strSeinenHikokenninShinpanKakuteiYMD = String.Empty
                    strSeinenHikokenninTokiYMD = String.Empty
                    strSeinenHikokenninShittaYMD = String.Empty
                End If


                ' �l����f�[�^���擾
                cdsKojinseigyo = m_cABKojinSeigyoB.GetABKojinSeigyo(.Item(ABJukiData.JUMINCD).ToString)

                ' �l���䗚���f�[�^���擾
                cdsKojinseigyoRrk = m_cABKojinseigyoRirekiB.GetKojinseigyoRireki(.Item(ABJukiData.JUMINCD).ToString)

                ' �l������̍X�V
                If (cdsKojinseigyo.Tables(ABKojinseigyomstEntity.TABLE_NAME).Rows.Count = 0) Then
                    ' �擾�����l����}�X�^�̃f�[�^��0���̏ꍇ

                    If (strSeinenHikokenninGaitoUmu = "1") Then
                        ' ���N��㌩�l_�Y���L���̒l��"1"�i�L�j�̏ꍇ

                        cdrKojinseigyoRow = cdsKojinseigyo.Tables(ABKojinseigyomstEntity.TABLE_NAME).NewRow

                        cdrKojinseigyoRow.BeginEdit()

                        For Each csColumn In cdrKojinseigyoRow.Table.Columns
                            If (csColumn.DataType.Name = GetType(Decimal).Name) Then

                                cdrKojinseigyoRow(csColumn) = 0

                            Else

                                cdrKojinseigyoRow(csColumn) = String.Empty

                            End If

                        Next csColumn

                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JUMINCD) = .Item(ABJukiData.JUMINCD).ToString                         ' �Z���R�[�h
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHICHOSONCD) = .Item(ABJukiData.SHICHOSONCD).ToString                 ' �s�����R�[�h
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KYUSHICHOSONCD) = .Item(ABJukiData.KYUSHICHOSONCD).ToString           ' ���s�����R�[�h
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB) = "1"                                                  ' ���N�㌩�敪
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENMSG) = m_strSeinenKoKenShokiMsg                            ' ���N�㌩���b�Z�[�W
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD) = .Item(ABJukiData.CKINIDOYMD).ToString         ' ���N�㌩�J�n��
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD) = "99999999"                                    ' ���N�㌩�I����
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = strSeinenHikokenninShinpanKakuteiYMD  ' ���N��㌩�l�̐R���m���
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD) = strSeinenHikokenninTokiYMD                 ' ���N��㌩�l�̓o�L��
                        cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD) = strSeinenHikokenninShittaYMD             ' ���N��㌩�l�ł���|��m������

                        cdrKojinseigyoRow.EndEdit()

                        intKoshinCnt = m_cABKojinSeigyoB.InsertKojinSeigyo(cdrKojinseigyoRow)

                        If (intKoshinCnt = 0) Then
                            '�X�V������0���̏ꍇ   
                            '�G���[��`���擾
                            cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYO, objErrorStruct.m_strErrorCode)
                        End If
                    Else
                        Return intKoshinCnt
                    End If
                Else
                    cdrKojinseigyoRow = cdsKojinseigyo.Tables(ABKojinseigyomstEntity.TABLE_NAME).Rows(0)
                    If (((cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB).ToString = "1") AndAlso (strSeinenHikokenninGaitoUmu = "0")) OrElse
                        ((cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB).ToString.Trim = String.Empty) AndAlso (strSeinenHikokenninGaitoUmu = "1")) OrElse
                         (cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD).ToString <> strSeinenHikokenninShinpanKakuteiYMD) OrElse
                         (cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD).ToString <> strSeinenHikokenninTokiYMD) OrElse
                         (cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD).ToString <> strSeinenHikokenninShittaYMD)) Then

                        If (strSeinenHikokenninGaitoUmu = "1") Then
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB) = "1"                                                  ' ���N��㌩�敪
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENMSG) = m_strSeinenKoKenShokiMsg                            ' ���N�㌩���b�Z�[�W
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD) = .Item(ABJukiData.CKINIDOYMD).ToString         ' ���N�㌩�J�n��
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD) = "99999999"                                    ' ���N�㌩�I����
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = strSeinenHikokenninShinpanKakuteiYMD  ' ���N��㌩�l�̐R���m���
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD) = strSeinenHikokenninTokiYMD                 ' ���N��㌩�l�̓o�L��
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD) = strSeinenHikokenninShittaYMD             ' ���N��㌩�l�ł���|��m������

                        Else
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB) = String.Empty                                         ' ���N��㌩�敪
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENMSG) = String.Empty                                        ' ���N�㌩���b�Z�[�W
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD) = String.Empty                                  ' ���N�㌩�J�n��
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD) = String.Empty                                  ' ���N�㌩�I����
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = String.Empty                          ' ���N��㌩�l�̐R���m���
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD) = String.Empty                               ' ���N��㌩�l�̓o�L��
                            cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD) = String.Empty                             ' ���N��㌩�l�ł���|��m������
                        End If

                        intKoshinCnt = m_cABKojinSeigyoB.UpdateKojinSeigyo(cdrKojinseigyoRow)

                        If (intKoshinCnt = 0) Then
                            '�X�V������0���̏ꍇ   
                            '�G���[��`���擾
                            cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001048)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYO, objErrorStruct.m_strErrorCode)
                        End If
                    Else
                        Return intKoshinCnt
                    End If
                End If
            End With

            ' �l���䗚���̍X�V
            cdrKojinseigyoRrkRow = cdsKojinseigyoRrk.Tables(ABKojinseigyoRirekiEntity.TABLE_NAME).NewRow

            cdrKojinseigyoRrkRow.BeginEdit()

            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JUMINCD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JUMINCD).ToString                                              '�Z���R�[�h
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHICHOSONCD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHICHOSONCD).ToString                                      '�s�����R�[�h
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KYUSHICHOSONCD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KYUSHICHOSONCD).ToString                                '���s�����R�[�h
            If (cdsKojinseigyoRrk.Tables(ABKojinseigyoRirekiEntity.TABLE_NAME).Rows.Count = 0) Then                                                                                     '����ԍ�
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.RIREKINO) = 1
            Else
                csSortDataRow = cdsKojinseigyoRrk.Tables(ABKojinseigyoRirekiEntity.TABLE_NAME).Select(String.Empty,
                                                             ABKojinseigyoRirekiEntity.RIREKINO + " DESC, " _
                                                             + ABKojinseigyoRirekiEntity.RIREKIEDABAN + " DESC ")
                cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.RIREKINO) = CInt(csSortDataRow(0).Item(ABKojinseigyoRirekiEntity.RIREKINO).ToString()) + 1
            End If
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.RIREKIEDABAN) = 0                                                                                                       '�����}��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOKB).ToString                                        '�c�u�Ώۋ敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOMSG).ToString                                      '�c�u�Ώۃ��b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOSHINSEIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOSHINSEIYMD).ToString                        '�c�u�Ώې\����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOKAISHIYMD).ToString                          '�c�u�ΏۊJ�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.DVTAISHOSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.DVTAISHOSHURYOYMD).ToString                          '�c�u�ΏۏI����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.HAKKOTEISHIKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.HAKKOTEISHIKB).ToString                                  '���s��~�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.HAKKOTEISHIMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.HAKKOTEISHIMSG).ToString                                '���s��~���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.HAKKOTEISHIKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.HAKKOTEISHIKAISHIYMD).ToString                    '���s��~�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.HAKKOTEISHISHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.HAKKOTEISHISHURYOYMD).ToString                    '���s��~�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JITTAICHOSAKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JITTAICHOSAKB).ToString                                  '���Ԓ����敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JITTAICHOSAMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JITTAICHOSAMSG).ToString                                '���Ԓ������b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JITTAICHOSAKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JITTAICHOSAKAISHIYMD).ToString                    '���Ԓ����J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.JITTAICHOSASHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.JITTAICHOSASHURYOYMD).ToString                    '���Ԓ����I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKB).ToString                                  '���N�㌩�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENMSG).ToString                                '���N�㌩���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENKAISHIYMD).ToString                    '���N�㌩�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHURYOYMD).ToString                    '���N�㌩�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENKOKENSHIMPANKAKUTEIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENKOKENSHIMPANKAKUTEIYMD).ToString    '���N��㌩�l�̐R���m���
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENHIKOKENNINTOKIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINTOKIYMD).ToString              '���N��㌩�l�̓o�L��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SEINENHIKOKENNINSHITTAYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SEINENHIKOKENNINSHITTAYMD).ToString          '���N��㌩�l�ł���|��m������
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KARITOROKUKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KARITOROKUKB).ToString                                    '���o�^���敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KARITOROKUMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KARITOROKUMSG).ToString                                  '���o�^�����b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KARITOROKUKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KARITOROKUKAISHIYMD).ToString                      '���o�^���J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KARITOROKUSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KARITOROKUSHURYOYMD).ToString                      '���o�^���I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUYOSHIKB).ToString                            '���ʗ{�q�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUYOSHIMSG).ToString                          '���ʗ{�q���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHIKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUYOSHIKAISHIYMD).ToString              '���ʗ{�q�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUYOSHISHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUYOSHISHURYOYMD).ToString              '���ʗ{�q�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUJIJOKB).ToString                              '���ʎ���敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUJIJOMSG).ToString                            '���ʎ���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUJIJOKAISHIYMD).ToString                '���ʎ���J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.TOKUBETSUJIJOSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.TOKUBETSUJIJOSHURYOYMD).ToString                '���ʎ���I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI1KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI1KB).ToString                                    '��������1�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI1MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI1MSG).ToString                                  '��������1���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI1KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI1KAISHIYMD).ToString                      '��������1�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI1SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI1SHURYOYMD).ToString                      '��������1�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI2KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI2KB).ToString                                    '��������2�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI2MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI2MSG).ToString                                  '��������2���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI2KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI2KAISHIYMD).ToString                      '��������2�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI2SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI2SHURYOYMD).ToString                      '��������2�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.GYOMUCD_CHUI) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.GYOMUCD_CHUI).ToString                                    '�Ɩ��R�[�h����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.GYOMUSHOSAICD_CHUI) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.GYOMUSHOSAICD_CHUI).ToString                        '�Ɩ��ڍׁi�Ŗځj�R�[�h����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI3KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI3KB).ToString                                    '��������3�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI3MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI3MSG).ToString                                  '��������3���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI3KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI3KAISHIYMD).ToString                      '��������3�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORICHUI3SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORICHUI3SHURYOYMD).ToString                      '��������3�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORIHORYUKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORIHORYUKB).ToString                                    '�����ۗ��敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORIHORYUMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORIHORYUMSG).ToString                                  '�����ۗ����b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORIHORYUKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORIHORYUKAISHIYMD).ToString                      '�����ۗ��J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SHORIHORYUSHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SHORIHORYUSHURYOYMD).ToString                      '�����ۗ��I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.GYOMUCD_HORYU) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.GYOMUCD_HORYU).ToString                                  '�Ɩ��R�[�h�ۗ�
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.GYOMUSHOSAICD_HORYU) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.GYOMUSHOSAICD_HORYU).ToString                      '�Ɩ��ڍׁi�Ŗځj�R�[�h�ۗ�
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKAKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKAKB).ToString                                    '���Ɩ��s�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKAMSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKAMSG).ToString                                  '���Ɩ��s���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKAKAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKAKAISHIYMD).ToString                      '���Ɩ��s�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKASHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKASHURYOYMD).ToString                      '���Ɩ��s�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SANSHOFUKATOROKUGYOMUCD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SANSHOFUKATOROKUGYOMUCD).ToString              '�o�^�Ɩ��R�[�h
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA1KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA1KB).ToString                                          '���̑��P�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA1MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA1MSG).ToString                                        '���̑��P���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA1KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA1KAISHIYMD).ToString                            '���̑��P�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA1SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA1SHURYOYMD).ToString                            '���̑��P�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA2KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA2KB).ToString                                          '���̑��Q�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA2MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA2MSG).ToString                                        '���̑��Q���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA2KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA2KAISHIYMD).ToString                            '���̑��Q�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA2SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA2SHURYOYMD).ToString                            '���̑��Q�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA3KB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA3KB).ToString                                          '���̑��R�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA3MSG) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA3MSG).ToString                                        '���̑��R���b�Z�[�W
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA3KAISHIYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA3KAISHIYMD).ToString                            '���̑��R�J�n��
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SONOTA3SHURYOYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SONOTA3SHURYOYMD).ToString                            '���̑��R�I����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KINSHIKAIJOKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KINSHIKAIJOKB).ToString                                  '�֎~�����敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.SETAIYOKUSHIKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.SETAIYOKUSHIKB).ToString                                '���ї}�~�敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOSTYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOSTYMD).ToString                            '�ꎞ�����J�n�N����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOSTTIME) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOSTTIME).ToString                          '�ꎞ�����J�n����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOEDYMD) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOEDYMD).ToString                            '�ꎞ�����I���N����
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOEDTIME) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOEDTIME).ToString                          '�ꎞ�����I������
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.ICHIJIKAIJOUSER) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.ICHIJIKAIJOUSER).ToString                              '�ꎞ�����ݒ葀���ID
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.KANRIKB) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.KANRIKB).ToString                                              '�Ǘ��敪
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.BIKO) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.BIKO).ToString                                                    '���l
            cdrKojinseigyoRrkRow.Item(ABKojinseigyoRirekiEntity.RESERVE) = cdrKojinseigyoRow.Item(ABKojinseigyomstEntity.RESERVE).ToString                                              '���U�[�u

            cdrKojinseigyoRrkRow.EndEdit()

            intRrkKoshinCnt = m_cABKojinseigyoRirekiB.InsertKojinseigyoRireki(cdrKojinseigyoRrkRow)

            If (intRrkKoshinCnt = 0) Then
                '�X�V������0���̏ꍇ   
                '�G���[��`���擾
                cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                objErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + ERR_MSG_KOJINSEIGYORIREKI, objErrorStruct.m_strErrorCode)
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, objRdbTimeOutExp.Message)
            ' UFAppException���X���[����
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, exAppException.Message)
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, exException.Message)
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intKoshinCnt

    End Function
#End Region
    '*����ԍ� 000065 2024/04/02 �ǉ��I��

End Class
