'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�����擾(ABAtenaGetClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/06�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/02/19 000001     �ȈՈ����擾�P�ŁA�Ǘ���񂪈����n����Ȃ��P�[�X������B
'*                       �ȈՈ����擾�P�ŁA�Ɩ��R�[�h���w�肳��Ă��āA�擾�������P���̏ꍇ�́A���t��f�[�^���Ȃ��Ă��A���t�惌�R�[�h��߂�
'* 2003/02/25 000002     �ȈՈ����擾�P���b�\�b�h�ŁA16�E17�Ńf�[�^�擾�O���̏ꍇ�́A�G���[�ɂ�����csAtenaH��csAtenaHS ���}�[�W���Ė߂��B
'* 2003/02/26 000003     �s�����R�[�h�̒��o������ǉ�
'* 2003/03/07 000004     �v���W�F�N�g��Imports�͒�`���Ȃ��i�d�l�ύX�j
'* 2003/03/07 000005     �L�������Ή��i�d�l�ύX�j
'* 2003/03/17 000006     �p�����[�^�̃`�F�b�N���͂����i�d�l�ύX�j
'* 2003/03/17 000007     �Ɩ�"AB"�Œ��RDB���A�N�Z�X����i�d�l�ύX�j
'* 2003/03/18 000008     �G���[���b�Z�[�W�̕ύX�i�d�l�ύX�j
'* 2003/03/27 000009     �G���[�����N���X�̎Q�Ɛ��"AB"�Œ�ɂ���
'* 2003/04/18 000010     �N�������擾���\�b�h�E���ۈ��������擾���\�b�h��ǉ�
'* 2003/04/22 000011     �f�[�^���擾�o���Ȃ��Ă���O�𔭐������Ȃ�
'* 2003/04/30 000012     �f�[�^���擾�ł��Ȃ������ꍇ���A0���ŕҏW�f�[�^��Ԃ��B
'* 2003/05/22 000013     RDB��Connect��ҿ��ނ̐擪�ɕύX(�d�l�ύX)
'* 2003/06/17 000014     �`���[�j���O(�Ǘ����擾���ŏ����ɂ���)
'* 2003/08/21 000015     �t�q�L���b�V���Ή��^�p���\�N���X�ɕύX
'* 2003/09/08 000016     ���ۈ��������擾�̎d�l�ύX
'* 2003/10/09 000017     �A����́A�A����}�X�^�Ƀf�[�^�����݂���ꍇ�́A�����炩��擾����B�A���A�Ɩ��R�[�h���w�肳��Ă��ꍇ�݂̂Ɍ���B
'*                       NenkinAtenaGet��AtenaGet1�Ɠ��l�Ɏw��N�������w�肳�ꂽ��A�����������擾����B�A��������l�B�A���A��[�E���Z�͕s�v�B
'* 2003/10/30 000018     p_strJukiJushoCD��8��
'* 2003/10/30 000019     �d�l�ύX�F�J�^�J�i�`�F�b�N��ANK�`�F�b�N�ɕύX
'* 2003/11/19 000020     �d�l�ǉ��F�ȈՈ����擾1(�I�[�o�[���[�h)���\�b�h�̒ǉ�
'* 2003/12/01 000021     �d�l�ύX�F�f�[�^�敪'1%'�̏ꍇ�A�l�݂̂��擾����
'* 2003/12/02 000022     �d�l�ύX�F�A����擾�����������ҏW���父���擾�ֈړ�
'* 2004/08/27 000023     ���x���P�F�i�{��j
'* 2005/01/25 000024     ���x���P�Q�F�i�{��j
'* 2005/04/04 000025     �S�p�ł̂����܂��������\�ɂ���(�}���S���R)
'* 2005/04/21 000026     ��[�E���t��̊��Ԏw������V�X�e�����t�ɂ���
'* 2005/05/06 000027     �p�����[�^�`�F�b�N��TRIM���Ă���s�Ȃ��B���ʒP�Ƃ͋����Ȃ��B
'* 2005/12/06 000028     CheckColumnValue���\�b�h�ōs����b�c�͂`�m�j�`�F�b�N���s���B(�}���S���R)
'* 2006/07/31 000029     �N�������Q�b�g�U�ǉ��ɔ����C�� (�g�V)
'* 2007/04/21 000030     ���ň����擾���\�b�h�̒ǉ� (�g�V)
'* 2007/07/28 000031     ����l��\�Ҏ擾�@�\�̒ǉ� (�g�V)
'* 2007/09/04 000032     �O���l�{�������@�\�̒ǉ��F�����J�i���ҏW�p���\�b�h�ǉ��i����j
'* 2007/09/13 000033     �����擾�p�����[�^�̏Z���R�[�h���g��������up_strJuminCD�v (�g�V)
'* 2007/10/10 000034     �����p�J�i���ڂɃA���t�@�x�b�g�������Ă����ꍇ�͑啶���ɕϊ��i����j
'* 2007/10/10 000035     �O���l�{�������Ŗ��O�̐擪���u�E�v�̏ꍇ�̌����R��Ή��i����j
'* 2007/11/06 000036     �����J�i�ҏW���\�b�h�A�d�l�ʂ�ҏW����Ȃ��������C���i����j
'* 2008/01/17 000037     ����l��\�Ҏ擾�ɂ��Z���R�[�h���̕s��Ή��i�g�V�j
'* 2008/01/17 000038     �����ʏ����擾���鎞�A�ʎ����擾�敪�������ɐݒ肷��悤�C���i��Áj
'* 2008/02/17 000039     �����ȗ������ҏW������ǉ��i��Áj
'* 2008/11/10 000040     ���p�͏o�擾������ǉ��i��Áj
'* 2008/11/17 000041     ���p�͊Y���f�[�^�i���ݏ����̏C���i��Áj
'* 2008/11/18 000042     ���p�͏o�擾�����̒ǉ��ɔ����A�A����f�[�^�擾�����̉��C�i��Áj
'* 2009/04/08 000043     �����L�[������AtnaGet2���g�p����ƃI�u�W�F�N�g�Q�ƃG���[����������s����C�i����j
'* 2010/04/16 000044     VS2008�Ή��i��Áj
'* 2010/05/17 000045     �{�ЕM���ҋy�я�����~�敪�Ή��i��Áj
'* 2011/05/18 000046     �O���l�ݗ����擾�敪�Ή��i��Áj
'* 2011/11/07 000047     �yAB17010�z�Z��@�����敪�ǉ��Ή��i�r�c�j
'* 2014/04/28 000048     �yAB21040�z�����ʔԍ��Ή������ʔԍ��擾�敪�ǉ��i�΍��j
'* 2018/03/08 000049     �yAB26001�z���������@�\�ǉ��i�΍��j
'* 2020/01/31 000050     �yAB00185�zAtenaGet1�ȊO�̗��������@�\�ǉ��i�΍��j
'* 2020/11/04 000051     �yAB00189�z���p�͏o�����[�Ŏ�ID�Ή��i�{�]�j
'* 2023/03/10 000052     �yAB-0970-1�z����GET�擾���ڕW�����Ή��i�����j
'* 2023/12/04 000053     �yAB-1600-1�z�����@�\�Ή�(����)
'* 2024/03/07 000054     �yAB-0900-1�z�A�h���X�E�x�[�X�E���W�X�g���Ή�(����)
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
Imports System.Data
Imports System.Text
Imports System.Security

'************************************************************************************************
'*
'* �����擾�Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABAtenaGetBClass

#Region " �����o�ϐ� "
    '�p�����[�^�̃����o�ϐ�
    '* ����ԍ� 000015 2003/08/21 �C���J�n
    'Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    'Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    'Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    'Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    'Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X

    'Private m_intHyojiketaJuminCD As Integer                ' �Z���R�[�h�\������
    'Private m_intHyojiketaStaiCD As Integer                 ' ���уR�[�h�\������
    'Private m_intHyojiketaJushoCD As Integer                ' �Z���R�[�h�\�������i�Ǔ��̂݁j
    'Private m_intHyojiketaGyoseikuCD As Integer             ' �s����R�[�h�\������
    'Private m_intHyojiketaChikuCD1 As Integer               ' �n��R�[�h�P�\������
    'Private m_intHyojiketaChikuCD2 As Integer               ' �n��R�[�h�Q�\������
    'Private m_intHyojiketaChikuCD3 As Integer               ' �n��R�[�h�R�\������
    'Private m_strChikuCD1HyojiMeisho As String              ' �n��R�[�h�P�\������
    'Private m_strChikuCD2HyojiMeisho As String              ' �n��R�[�h�Q�\������
    'Private m_strChikuCD3HyojiMeisho As String              ' �n��R�[�h�R�\������
    'Private m_strRenrakusaki1HyojiMeisho As String          ' �A����P�\������
    'Private m_strRenrakusaki2HyojiMeisho As String          ' �A����Q�\������
    ''* ����ԍ� 000014 2003/06/17 �ǉ��J�n
    'Private m_blnKanriJoho As Boolean                       ' �Ǘ����擾
    ''* ����ԍ� 000014 2003/06/17 �ǉ��I��

    ''�@�R���X�^���g��`
    'Private Const THIS_CLASS_NAME As String = "ABAtenaGetBClass"                ' �N���X��
    'Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h

    Protected m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Protected m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Protected m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Protected m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Protected m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X

    Protected m_intHyojiketaJuminCD As Integer                ' �Z���R�[�h�\������
    Protected m_intHyojiketaStaiCD As Integer                 ' ���уR�[�h�\������
    Protected m_intHyojiketaJushoCD As Integer                ' �Z���R�[�h�\�������i�Ǔ��̂݁j
    Protected m_intHyojiketaGyoseikuCD As Integer             ' �s����R�[�h�\������
    Protected m_intHyojiketaChikuCD1 As Integer               ' �n��R�[�h�P�\������
    Protected m_intHyojiketaChikuCD2 As Integer               ' �n��R�[�h�Q�\������
    Protected m_intHyojiketaChikuCD3 As Integer               ' �n��R�[�h�R�\������
    Protected m_strChikuCD1HyojiMeisho As String              ' �n��R�[�h�P�\������
    Protected m_strChikuCD2HyojiMeisho As String              ' �n��R�[�h�Q�\������
    Protected m_strChikuCD3HyojiMeisho As String              ' �n��R�[�h�R�\������
    Protected m_strRenrakusaki1HyojiMeisho As String          ' �A����P�\������
    Protected m_strRenrakusaki2HyojiMeisho As String          ' �A����Q�\������
    Protected m_blnKanriJoho As Boolean                       ' �Ǘ����擾
    Protected m_blnBatch As Boolean                           ' �o�b�`�敪(True:�o�b�`�n, False:���A���n)
    Protected m_blnBatchRdb As Boolean
    Protected m_cABAtenaHenshuB As ABAtenaHenshuBClass                          ' �����ҏW�N���X
    Protected m_cABBatchAtenaHenshuB As ABBatchAtenaHenshuBClass                ' �����ҏW�N���X(�o�b�`�n)
    '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
    Private m_cABAtenaRirekiB As ABAtenaRirekiBClass          '���������}�X�^�c�`�N���X
    Private m_cABAtenaB As ABAtenaBClass                      '�����}�X�^�c�`�N���X
    Private m_cABSfskB As ABSfskBClass                        '���t��}�X�^�c�`�N���X
    Private m_cABDainoB As ABDainoBClass                      '��[�}�X�^�c�`�N���X

    Private m_cUSSCityInfoClass As USSCityInfoClass           '�s�������Ǘ��N���X
    Private m_cRenrakusakiBClass As ABRenrakusakiBClass       ' �A����a�N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X
    Private m_cfURAtenaKanriJoho As URAtenaKanriJohoCacheBClass   '�����Ǘ����L���b�V���a�N���X
    '* ����ԍ� 000023 2004/08/27 �ǉ��I��
    '*����ԍ� 000032 2007/09/04 �ǉ��J�n
    Private m_cURKanriJohoB As URKANRIJOHOBClass         '�Ǘ����擾�N���X
    '�o�b�`����Ă΂ꂽ�ꍇ�G���[���������邽�߁C�L���b�V���N���X�̓R�����g�A�E�g
    'Private m_cURKanriJohoB As URKANRIJOHOCacheBClass         '�Ǘ����擾�N���X
    '*����ԍ� 000032 2007/09/04 �ǉ��I��

    '�@�R���X�^���g��`
    Protected Const THIS_CLASS_NAME As String = "ABAtenaGetBClass"              ' �N���X��
    Protected Const THIS_BUSINESSID As String = "AB"                            ' �Ɩ��R�[�h
    '* ����ԍ� 000015 2003/08/21 �C���I��

    '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
    Protected m_blnSelectAll As ABEnumDefine.AtenaGetKB = ABEnumDefine.AtenaGetKB.KaniAll
    Protected m_cABAtenaRirekiBRef As ABAtenaRirekiBClass          '���������}�X�^�c�`�N���X
    Protected m_cABAtenaBRef As ABAtenaBClass                      '�����}�X�^�c�`�N���X
    Protected m_cABSfskBRef As ABSfskBClass                        '���t��}�X�^�c�`�N���X
    Protected m_cABDainoBRef As ABDainoBClass                      '��[�}�X�^�c�`�N���X
    '* ����ԍ� 000024 2005/01/25 �ǉ��I��
    '* ����ԍ� 000026 2005/04/21 �ǉ��J�n
    Private m_strSystemDateTime As String                          '��������
    '* ����ԍ� 000026 2005/04/21 �ǉ��I��

    '*����ԍ� 000022 2007/04/28 �ǉ��J�n
    Private m_blnSelectKaigo As ABEnumDefine.MethodKB  '���\�b�h�敪�i�ʏ�ł��A���ŁA�A�A�j
    '*����ԍ� 000022 2007/04/28 �ǉ��I��

    '*����ԍ� 000031 2007/07/28 �ǉ��J�n
    Dim m_cABAtenaKanriJohoB As ABAtenaKanriJohoBClass              '�Ǘ����a�N���X
    Dim m_cABGappeiDoitsuninB As ABGappeiDoitsuninBClass            '����l�a�N���X
    Dim m_strDoitsu_Param As String                    '����l����p�����[�^
    Dim m_strHonninJuminCD As String                    '�{�l�Z���R�[�h
    '*����ԍ� 000031 2007/07/28 �ǉ��I��

    '*����ԍ� 000042 2008/11/18 �ǉ��J�n
    Dim m_blnMethodKB As ABEnumDefine.MethodKB
    '*����ԍ� 000042 2008/11/18 �ǉ��I��

#End Region

#Region "�v���p�e�B "
    '************************************************************************************************
    '* �e�����o�ϐ��̃v���p�e�B��`
    '************************************************************************************************
    Public ReadOnly Property p_intHyojiketaJuminCD() As Integer
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_intHyojiketaJuminCD
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaStaiCD() As Integer
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_intHyojiketaStaiCD
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaJushoCD() As Integer
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_intHyojiketaJushoCD
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaGyoseikuCD() As Integer
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_intHyojiketaGyoseikuCD
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaChikuCD1() As Integer
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_intHyojiketaChikuCD1
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaChikuCD2() As Integer
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_intHyojiketaChikuCD2
        End Get
    End Property
    Public ReadOnly Property p_intHyojiketaChikuCD3() As Integer
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_intHyojiketaChikuCD3
        End Get
    End Property
    Public ReadOnly Property p_strChikuCD1HyojiMeisho() As String
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_strChikuCD1HyojiMeisho
        End Get
    End Property
    Public ReadOnly Property p_strChikuCD2HyojiMeisho() As String
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_strChikuCD2HyojiMeisho
        End Get
    End Property
    Public ReadOnly Property p_strChikuCD3HyojiMeisho() As String
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_strChikuCD3HyojiMeisho
        End Get
    End Property
    Public ReadOnly Property p_strRenrakusaki1HyojiMeisho() As String
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_strRenrakusaki1HyojiMeisho
        End Get
    End Property
    Public ReadOnly Property p_strRenrakusaki2HyojiMeisho() As String
        Get
            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If Not (m_blnKanriJoho) Then
                Me.KanriJohoGet()
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��
            Return m_strRenrakusaki2HyojiMeisho
        End Get
    End Property
#End Region

#Region " �R���X�g���N�^ "
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass)
        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        m_blnBatchRdb = False
        ' �q�c�a�N���X�̃C���X�^���X��
        m_cfRdbClass = New UFRdbClass(THIS_BUSINESSID)
        Initial(cfControlData, cfConfigDataClass, m_cfRdbClass, True)
        '* ����ԍ� 000024 2005/01/25 �ǉ��I��

        '* ����ԍ� 000024 2005/01/25 �폜�J�n�i�{��j
        '' �����o�ϐ��Z�b�g
        'm_cfControlData = cfControlData
        'm_cfConfigDataClass = cfConfigDataClass

        '' �q�c�a�N���X�̃C���X�^���X��
        'm_cfRdbClass = New UFRdbClass(THIS_BUSINESSID)

        '' ���O�o�̓N���X�̃C���X�^���X��
        'm_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        '' �p�����[�^�̃����o�ϐ�������
        'm_intHyojiketaJuminCD = 0                           '�Z���R�[�h�\������
        'm_intHyojiketaStaiCD = 0                            '���уR�[�h�\������
        'm_intHyojiketaJushoCD = 0                           '�Z���R�[�h�\�������i�Ǔ��̂݁j
        'm_intHyojiketaGyoseikuCD = 0                        '�s����R�[�h�\������
        'm_intHyojiketaChikuCD1 = 0                          '�n��R�[�h�P�\������
        'm_intHyojiketaChikuCD2 = 0                          '�n��R�[�h�Q�\������
        'm_intHyojiketaChikuCD3 = 0                          '�n��R�[�h�R�\������
        'm_strChikuCD1HyojiMeisho = String.Empty             '�n��R�[�h�P�\������
        'm_strChikuCD2HyojiMeisho = String.Empty             '�n��R�[�h�Q�\������
        'm_strChikuCD3HyojiMeisho = String.Empty             '�n��R�[�h�R�\������
        'm_strRenrakusaki1HyojiMeisho = String.Empty         '�A����P�\������
        'm_strRenrakusaki2HyojiMeisho = String.Empty         '�A����Q�\������
        ''* ����ԍ� 000014 2003/06/17 �ǉ��J�n
        '' �Ǘ����擾�ς݃t���O�̏�����
        'm_blnKanriJoho = False
        ''* ����ԍ� 000014 2003/06/17 �ǉ��I��
        ''* ����ԍ� 000015 2003/08/21 �ǉ��J�n
        'm_blnBatch = False                                  ' �o�b�`�敪
        ''* ����ԍ� 000015 2003/08/21 �ǉ��I��
        'm_blnBatchRdb = False

        ''* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
        ''���������}�X�^�c�`�N���X�̃C���X�^���X�쐬
        'm_cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        ''�����}�X�^�c�`�N���X�̃C���X�^���X�쐬
        'm_cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        ''���t��}�X�^�c�`�N���X�̃C���X�^���X�쐬
        'm_cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        ''��[�}�X�^�c�`�N���X�̃C���X�^���X�쐬
        'm_cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        'm_cUSSCityInfoClass = New USSCityInfoClass()
        'm_cUSSCityInfoClass.GetCityInfo(m_cfControlData)
        'm_cfDateClass = New UFDateClass(m_cfConfigDataClass)
        ''* ����ԍ� 000023 2004/08/27 �ǉ��I��
        '* ����ԍ� 000024 2005/01/25 �폜�I��
    End Sub

    '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* �@�@                          ByVal blnSelectAll As Boolean)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* �@�@           ByVal blnSelectAll As Boolean           : True�̏ꍇ�S���ځAFalse�̏ꍇ�ȈՍ��ڂ̂ݎ擾
    '*
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal blnSelectAll As Boolean)
        m_blnBatchRdb = False
        ' �q�c�a�N���X�̃C���X�^���X��
        m_cfRdbClass = New UFRdbClass(THIS_BUSINESSID)
        Initial(cfControlData, cfConfigDataClass, m_cfRdbClass, blnSelectAll)
    End Sub
    '* ����ԍ� 000024 2005/01/25 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass)
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
        '* ����ԍ� 000015 2003/08/21 �ǉ��J�n
        m_blnBatchRdb = True                                  ' �o�b�`�敪
        '* ����ԍ� 000015 2003/08/21 �ǉ��I��
        Initial(cfControlData, cfConfigDataClass, cfRdbClass, True)
    End Sub
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass)
    '* �@�@                          ByVal cfRdbClass As UFRdbClass, _
    '* �@�@                          ByVal blnSelectAll As Boolean)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* �@�@           cfRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* �@�@           ByVal blnSelectAll As Boolean           : True�̏ꍇ�S���ځAFalse�̏ꍇ�ȈՍ��ڂ̂ݎ擾
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass,
                   ByVal blnSelectAll As Boolean)
        '* ����ԍ� 000015 2003/08/21 �ǉ��J�n
        m_blnBatchRdb = True                                  ' �o�b�`�敪
        '* ����ԍ� 000015 2003/08/21 �ǉ��I��
        Initial(cfControlData, cfConfigDataClass, cfRdbClass, blnSelectAll)
    End Sub
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           'Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@           '               ByVal cfConfigDataClass As UFConfigDataClass)
    '* �\��           Public Sub Initial(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass,
    '* �@�@                          ByVal blnSelectAll as boolean)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@           cfConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '* �@�@           cfRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* �@�@           ByVal blnSelectAll As Boolean           : True�̏ꍇ�S���ځAFalse�̏ꍇ�ȈՍ��ڂ̂ݎ擾
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
    'Public Sub New(ByVal cfControlData As UFControlData, _
    '               ByVal cfConfigDataClass As UFConfigDataClass, _
    '               ByVal cfRdbClass As UFRdbClass)
    <SecuritySafeCritical>
    Private Sub Initial(ByVal cfControlData As UFControlData,
                   ByVal cfConfigDataClass As UFConfigDataClass,
                   ByVal cfRdbClass As UFRdbClass,
                   ByVal blnSelectAll As Boolean)
        '* ����ԍ� 000024 2005/01/25 �X�V�I��
        m_cfRdbClass = cfRdbClass

        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' �p�����[�^�̃����o�ϐ�������
        m_intHyojiketaJuminCD = 0                           '�Z���R�[�h�\������
        m_intHyojiketaStaiCD = 0                            '���уR�[�h�\������
        m_intHyojiketaJushoCD = 0                           '�Z���R�[�h�\�������i�Ǔ��̂݁j
        m_intHyojiketaGyoseikuCD = 0                        '�s����R�[�h�\������
        m_intHyojiketaChikuCD1 = 0                          '�n��R�[�h�P�\������
        m_intHyojiketaChikuCD2 = 0                          '�n��R�[�h�Q�\������
        m_intHyojiketaChikuCD3 = 0                          '�n��R�[�h�R�\������
        m_strChikuCD1HyojiMeisho = String.Empty             '�n��R�[�h�P�\������
        m_strChikuCD2HyojiMeisho = String.Empty             '�n��R�[�h�Q�\������
        m_strChikuCD3HyojiMeisho = String.Empty             '�n��R�[�h�R�\������
        m_strRenrakusaki1HyojiMeisho = String.Empty         '�A����P�\������
        m_strRenrakusaki2HyojiMeisho = String.Empty         '�A����Q�\������
        '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
        ' �Ǘ����擾�ς݃t���O�̏�����
        m_blnKanriJoho = False
        '* ����ԍ� 000014 2003/06/17 �ǉ��I��
        '* ����ԍ� 000015 2003/08/21 �ǉ��J�n
        m_blnBatch = False                                  ' �o�b�`�敪
        '* ����ԍ� 000015 2003/08/21 �ǉ��I��

        '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
        '���������}�X�^�c�`�N���X�̃C���X�^���X�쐬

        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        'm_cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        If (blnSelectAll = True) Then
            m_blnSelectAll = ABEnumDefine.AtenaGetKB.KaniAll
        Else
            m_blnSelectAll = ABEnumDefine.AtenaGetKB.KaniOnly
        End If
        m_cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll, True)
        m_cABAtenaRirekiBRef = m_cABAtenaRirekiB
        '* ����ԍ� 000024 2005/01/25 �X�V�I��

        '�����}�X�^�c�`�N���X�̃C���X�^���X�쐬
        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        'm_cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        m_cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll, True)
        m_cABAtenaBRef = m_cABAtenaB
        '* ����ԍ� 000024 2005/01/25 �X�V�I��

        '���t��}�X�^�c�`�N���X�̃C���X�^���X�쐬
        m_cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        m_cABSfskBRef = m_cABSfskB
        '* ����ԍ� 000024 2005/01/25 �ǉ��I��(�{��)
        '��[�}�X�^�c�`�N���X�̃C���X�^���X�쐬
        m_cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        m_cABDainoBRef = m_cABDainoB
        '* ����ԍ� 000024 2005/01/25 �ǉ��I��(�{��)

        m_cUSSCityInfoClass = New USSCityInfoClass
        m_cUSSCityInfoClass.GetCityInfo(m_cfControlData)
        m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
        '* ����ԍ� 000023 2004/08/27 �ǉ��I��

        '* ����ԍ� 000026 2005/04/21 �ǉ��J�n
        m_strSystemDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMdd")    '��������
        '* ����ԍ� 000026 2005/04/21 �ǉ��I��

        '*����ԍ� 000032 2007/09/04 �ǉ��J�n
        'UR�Ǘ������擾
        If (m_cURKanriJohoB Is Nothing) Then
            m_cURKanriJohoB = New URKANRIJOHOBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        End If
        '�o�b�`����Ă΂ꂽ�ꍇ�G���[���������邽�߁C�R�����g�A�E�g
        'm_cURKanriJohoB = New URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '*����ԍ� 000032 2007/09/04 �ǉ��I��

    End Sub
#End Region

#Region " �ȈՈ����擾�P(AtenaGet1) "
    '************************************************************************************************
    '* ���\�b�h��     �ȈՈ����擾�P
    '* 
    '* �\��           Public Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    '*����ԍ� 000020 2003/11/19 �C���J�n
    'Public Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    Public Overloads Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        '*����ԍ� 000020 2003/11/19 �C���J�n

        ''*����ԍ� 000020 2003/11/19 �C���I��
        'Const THIS_METHOD_NAME As String = "AtenaGet1"
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        'Dim cSearchKey As ABAtenaSearchKey                  '���������L�[
        'Dim csDataTable As DataTable
        'Dim csDataSet As DataSet
        'Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '���������}�X�^�c�`�N���X
        'Dim cABAtenaB As ABAtenaBClass                      '�����}�X�^�c�`�N���X
        'Dim cABSfskB As ABSfskBClass                        '���t��}�X�^�c�`�N���X
        'Dim cABDainoB As ABDainoBClass                      '��[�}�X�^�c�`�N���X
        ''*����ԍ� 000015 2003/08/21 �폜�J�n
        ''Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '�����ҏW�N���X
        ''*����ԍ� 000015 2003/08/21 �폜�I��
        'Dim csAtena1 As DataSet                             '�������(ABAtena1)
        'Dim csAtenaH As DataSet                             '�������(ABAtena1)
        'Dim csAtenaHS As DataSet                            '�������(ABAtena1)
        'Dim csAtenaD As DataSet                             '�������(ABAtena1)
        'Dim csAtenaDS As DataSet                            '�������(ABAtena1)
        'Dim strStaiCD As String                             '���уR�[�h
        'Dim intHyojiKensu As Integer                        '�ő�擾����
        'Dim intGetCount As Integer                          '�擾����
        'Dim strKikanYM As String                            '���ԔN��
        'Dim strDainoKB As String                            '��[�敪
        'Dim strGyomuCD As String                            '�Ɩ��R�[�h
        'Dim strGyomunaiSHU_CD As String                     '�Ɩ�����ʃR�[�h
        'Dim cUSSCityInfoClass As New USSCityInfoClass()     '�s�������Ǘ��N���X
        'Dim strShichosonCD As String                        '�s�����R�[�h

        'Try
        '    ' �f�o�b�O�J�n���O�o��
        '    m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        '    ' RDB�A�N�Z�X���O�o��
        '    m_cfLogClass.RdbWrite(m_cfControlData, _
        '                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
        '                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
        '                                    "�y���s���\�b�h��:Connect�z")
        '    '�q�c�a�ڑ�
        '    m_cfRdbClass.Connect()

        '    Try
        '        '* ����ԍ� 000014 2003/06/17 �폜�J�n
        '        '' �Ǘ����擾(��������)���\�b�h�����s����B
        '        'Me.GetKanriJoho()
        '        '* ����ԍ� 000014 2003/06/17 �폜�I��

        '        '�p�����[�^�`�F�b�N
        '        Me.CheckColumnValue(cAtenaGetPara1)

        '        '���������}�X�^�c�`�N���X�̃C���X�^���X�쐬
        '        cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        '�����}�X�^�c�`�N���X�̃C���X�^���X�쐬
        '        cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        '���t��}�X�^�c�`�N���X�̃C���X�^���X�쐬
        '        cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        '��[�}�X�^�c�`�N���X�̃C���X�^���X�쐬
        '        cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        '*����ԍ� 000015 2003/08/21 �C���J�n
        '        ''�����ҏW�N���X�̃C���X�^���X�쐬
        '        'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        If (m_blnBatch) Then
        '            '�����ҏW�o�b�`�N���X�̃C���X�^���X�쐬
        '            m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '        Else
        '            '�����ҏW�N���X�̃C���X�^���X�쐬
        '            m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '        End If
        '        '*����ԍ� 000015 2003/08/21 �C���I��

        '        '*����ǉ� 000003 2003/02/26 �ǉ��J�n
        '        'USSCityInfoClass.GetCityInfo()���g�p���āA���ߎs�������擾���擾����B
        '        cUSSCityInfoClass.GetCityInfo(m_cfControlData)

        '        '�s�����R�[�h�̓��e��ݒ肷��B
        '        If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
        '            strShichosonCD = cUSSCityInfoClass.p_strShichosonCD(0)
        '        Else
        '            strShichosonCD = cAtenaGetPara1.p_strShichosonCD
        '        End If
        '        '*����ǉ� 000003 2003/02/26 �ǉ��I��

        '        '���уR�[�h�̎w�肪�Ȃ��A���ш��ҏW�̎w��������ꍇ
        '        If cAtenaGetPara1.p_strStaiCD = "" And cAtenaGetPara1.p_strStaiinHenshu = "1" Then

        '            '���������L�[�̃C���X�^���X��
        '            cSearchKey = New ABAtenaSearchKey()

        '            '�Z���R�[�h�̐ݒ�
        '            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

        '            '�Z��E�Z�o�O�敪��<>"1"�̏ꍇ
        '            If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '                cSearchKey.p_strJutogaiYusenKB = "1"
        '            End If

        '            '�Z��E�Z�o�O�敪��="1"�̏ꍇ
        '            If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '                cSearchKey.p_strJuminYuseniKB = "1"
        '            End If

        '            '�w��N�������w�肳��Ă���ꍇ
        '            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '                '�u���������}�X�^���o�v���]�b�g�����s����
        '                csDataSet = cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                            cSearchKey, _
        '                                                            cAtenaGetPara1.p_strShiteiYMD, _
        '                                                            cAtenaGetPara1.p_blnSakujoFG)

        '                '�擾�������P���łȂ��ꍇ�A�G���[
        '                If (csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 1) Then
        '                    '�G���[��`���擾
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
        '                End If

        '                strStaiCD = CType(csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.STAICD), String)
        '            End If

        '            '�w��N�������w�肳��Ă��Ȃ��ꍇ
        '            If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '                '�u�����}�X�^���o�v���]�b�g�����s����
        '                csDataSet = cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                     cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                '�擾�������P���łȂ��ꍇ�A�G���[
        '                If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count <> 1) Then
        '                    '�G���[��`���擾
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
        '                End If

        '                '���уR�[�h��NULL�̏ꍇ�A�G���[
        '                If CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String).Trim = String.Empty Then
        '                    '�G���[��`���擾
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
        '                End If

        '                strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String)
        '            End If
        '            cAtenaGetPara1.p_strStaiCD = strStaiCD
        '            cAtenaGetPara1.p_strJuminCD = String.Empty
        '        End If

        '        cSearchKey = Nothing
        '        cSearchKey = New ABAtenaSearchKey()

        '        '���ш��ҏW��"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strStaiinHenshu = "1" Then
        '            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
        '        Else
        '            '�����擾�p�����[�^���父�������L�[�ɃZ�b�g����
        '            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
        '            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
        '            cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
        '            cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
        '            cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
        '            cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
        '            cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
        '            cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
        '            cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
        '            cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
        '            cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
        '            cSearchKey.p_strShichosonCD = strShichosonCD
        '        End If

        '        '�Z��E�Z�o�O�敪��<>"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJutogaiYusenKB = "1"
        '        End If

        '        '�Z��E�Z�o�O�敪��="1"�̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJuminYuseniKB = "1"
        '        End If

        '        '�Z���`�Ԓn�R�[�h3�̃Z�b�g
        '        '�Z�o�O�D��̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
        '            cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
        '            cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
        '            cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
        '            cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
        '            cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
        '            cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
        '            cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
        '        End If

        '        '�Z��D��̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            '*����ԍ� 000018 2003/10/30 �C���J�n
        '            'cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
        '            cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(8)
        '            '*����ԍ� 000018 2003/10/30 �C���I��
        '            cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
        '            cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
        '            cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
        '            cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
        '            cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
        '            cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
        '            cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
        '        End If

        '        '�ő�擾�������Z�b�g����
        '        If cAtenaGetPara1.p_intHyojiKensu = 0 Then
        '            intHyojiKensu = 100
        '        Else
        '            intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
        '        End If

        '        '�w��N�������w�肳��Ă���ꍇ
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '            '�u���������}�X�^���o�v���]�b�g�����s����
        '            csDataSet = cABAtenaRirekiB.GetAtenaRBHoshu(intHyojiKensu, _
        '                                                        cSearchKey, _
        '                                                        cAtenaGetPara1.p_strShiteiYMD, _
        '                                                        cAtenaGetPara1.p_blnSakujoFG)

        '            intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

        '            '*����ԍ� 000015 2003/08/21 �C���J�n
        '            ''�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
        '            'csAtenaH = cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)

        '            If (m_blnBatch) Then
        '                '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
        '                csAtenaH = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
        '            Else
        '                '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
        '                csAtenaH = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
        '            End If
        '            '*����ԍ� 000015 2003/08/21 �C���I��
        '        End If

        '        '�w��N�������w�肳��Ă��Ȃ��ꍇ
        '        If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '            '�u�����}�X�^���o�v���]�b�g�����s����
        '            csDataSet = cABAtenaB.GetAtenaBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '            intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

        '            '*����ԍ� 000015 2003/08/21 �C���J�n
        '            ''�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
        '            'csAtenaH = cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)

        '            If (m_blnBatch) Then
        '                '�u�����ҏW�o�b�`�v�́u�����ҏW�v���\�b�h�����s����
        '                csAtenaH = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
        '            Else
        '                '�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
        '                csAtenaH = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
        '            End If
        '            '*����ԍ� 000015 2003/08/21 �C���I��

        '        End If

        '        '�擾�p�����[�^�̋Ɩ��R�[�h���w�肳��Ă��Ȃ����A�擾������1���łȂ��ꍇ�́A�l��Ԃ�
        '        If cAtenaGetPara1.p_strGyomuCD = "" Or intGetCount <> 1 Then

        '            csAtena1 = csAtenaH

        '            Exit Try
        '        End If

        '        '�w��N�������w�肵�Ă��芎�擾�p�����[�^�̑��t��f�[�^�敪��"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            strKikanYM = "999999"
        '        End If

        '        '�u���t��}�X�^�c�`�v�́u���t��}�X�^���o�v���\�b�h�����s����
        '        csDataSet = cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '                                           cAtenaGetPara1.p_strGyomuCD, _
        '                                           cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                           strKikanYM, _
        '                                           cAtenaGetPara1.p_blnSakujoFG)


        '        '*����ԍ� 000015 2003/08/21 �C���J�n
        '        ''�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
        '        'csAtenaHS = cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)

        '        If (m_blnBatch) Then
        '            '�u�����ҏW�o�b�`�v�́u���t��ҏW�v���\�b�h�����s����
        '            csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '        Else
        '            '�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
        '            csAtenaHS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '        End If
        '        '*����ԍ� 000015 2003/08/21 �C���I��

        '        '�w��N�������w�肵�Ă���ꍇ
        '        If (cAtenaGetPara1.p_strShiteiYMD <> "") Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            strKikanYM = "999999"
        '        End If

        '        '�u��[�}�X�^�c�`�v�́u��[�}�X�^���o�v���\�b�h�����s����
        '        csDataSet = cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '                                             cAtenaGetPara1.p_strGyomuCD, _
        '                                             cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                             strKikanYM, _
        '                                             cAtenaGetPara1.p_blnSakujoFG)

        '        '�擾������1���łȂ��ꍇ
        '        If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count <> 1) Then

        '            'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)
        '            Exit Try
        '        End If

        '        With csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows(0)

        '            '��[�敪��ޔ�����
        '            strDainoKB = CType(.Item(ABDainoEntity.DAINOKB), String)

        '            '�Ɩ��R�[�h��ޔ�����
        '            strGyomuCD = CType(.Item(ABDainoEntity.GYOMUCD), String)

        '            '�Ɩ�����ʃR�[�h��ޔ�����
        '            strGyomunaiSHU_CD = CType(.Item(ABDainoEntity.GYOMUNAISHU_CD), String)

        '            '���������L�[�ɃZ�b�g����
        '            cSearchKey = Nothing
        '            cSearchKey = New ABAtenaSearchKey()

        '            cSearchKey.p_strJuminCD = CType(.Item(ABDainoEntity.DAINOJUMINCD), String)

        '        End With

        '        '�Z��E�Z�o�O�敪��<>"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJutogaiYusenKB = "1"
        '        End If

        '        '�Z��E�Z�o�O�敪��="1"�̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJuminYuseniKB = "1"
        '        End If

        '        '�O�w��N�������w�肳��Ă���ꍇ
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then
        '            '�u���������}�X�^�c�`�v�́u���������}�X�^���o�v���\�b�h�����s����
        '            csDataSet = cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                        cSearchKey, _
        '                                                        cAtenaGetPara1.p_strShiteiYMD, _
        '                                                        cAtenaGetPara1.p_blnSakujoFG)

        '            '�擾����
        '            intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
        '            '�擾�������O���̏ꍇ�A
        '            If (intGetCount = 0) Then

        '                'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '                csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)
        '                Exit Try
        '            End If

        '            '*����ԍ� 000015 2003/08/21 �C���J�n
        '            ''�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
        '            'csAtenaD = cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '            '                                        strGyomuCD, strGyomunaiSHU_CD)

        '            If (m_blnBatch) Then
        '                '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
        '                csAtenaD = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                        strGyomuCD, strGyomunaiSHU_CD)
        '            Else
        '                '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
        '                csAtenaD = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                        strGyomuCD, strGyomunaiSHU_CD)
        '            End If
        '            '*����ԍ� 000015 2003/08/21 �C���I��

        '        Else
        '            '�P�w��N�������w�肳��Ă��Ȃ��ꍇ

        '            '�u�����}�X�^���o�v���]�b�g�����s����
        '            csDataSet = cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '            '�擾����
        '            intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
        '            '�擾�������O���̏ꍇ�A
        '            If (intGetCount = 0) Then

        '                'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '                csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)
        '                Exit Try
        '            End If

        '            '*����ԍ� 000015 2003/08/21 �C���J�n
        '            ''�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
        '            'csAtenaD = cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '            '                                       strGyomuCD, strGyomunaiSHU_CD)

        '            If (m_blnBatch) Then
        '                '�u�����ҏW�o�b�`�v�́u�����ҏW�v���\�b�h�����s����
        '                csAtenaD = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                       strGyomuCD, strGyomunaiSHU_CD)
        '            Else
        '                '�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
        '                csAtenaD = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                       strGyomuCD, strGyomunaiSHU_CD)
        '            End If
        '            '*����ԍ� 000015 2003/08/21 �C���I��

        '        End If

        '        '�w��N�������w�肵�Ă��芎�擾�p�����[�^�̑��t��f�[�^�敪��"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            strKikanYM = "999999"
        '        End If

        '        '�u���t��}�X�^�c�`�v�́u���t��}�X�^���o�v���\�b�h�����s����
        '        csDataSet = cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, _
        '                                           cAtenaGetPara1.p_strGyomuCD, _
        '                                           cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                           strKikanYM, _
        '                                           cAtenaGetPara1.p_blnSakujoFG)

        '        '*����ԍ� 000015 2003/08/21 �C���J�n
        '        ''�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
        '        'csAtenaDS = cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)

        '        If (m_blnBatch) Then
        '            '�u�����ҏW�o�b�`�v�́u���t��ҏW�v���\�b�h�����s����
        '            csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '        Else
        '            '�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
        '            csAtenaDS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '        End If
        '        '*����ԍ� 000015 2003/08/21 �C���I��

        '        'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '        csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS)

        '    Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
        '        ' ���[�j���O���O�o��
        '        m_cfLogClass.WarningWrite(m_cfControlData, _
        '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
        '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
        '                                "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" + _
        '                                "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
        '        ' UFAppException���X���[����
        '        Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        '    Catch
        '        ' �G���[�����̂܂܃X���[
        '        Throw

        '    Finally
        '        ' RDB�A�N�Z�X���O�o��
        '        m_cfLogClass.RdbWrite(m_cfControlData, _
        '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
        '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
        '                                "�y���s���\�b�h��:Disconnect�z")
        '        ' RDB�ؒf
        '        m_cfRdbClass.Disconnect()
        '    End Try

        '    ' �f�o�b�O�I�����O�o��
        '    m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        'Catch objAppExp As UFAppException
        '    ' ���[�j���O���O�o��
        '    m_cfLogClass.WarningWrite(m_cfControlData, _
        '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
        '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
        '                                "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
        '                                "�y���[�j���O���e:" + objAppExp.Message + "�z")
        '    ' �G���[�����̂܂܃X���[����
        '    Throw objAppExp

        'Catch objExp As Exception
        '    ' �G���[���O�o��
        '    m_cfLogClass.ErrorWrite(m_cfControlData, _
        '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
        '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
        '                                "�y�G���[���e:" + objExp.Message + "�z")
        '    Throw objExp
        'End Try

        'Return csAtena1

        Return AtenaGet1(cAtenaGetPara1, False)
        '*����ԍ� 000020 2003/11/19 �C���I��

    End Function
#End Region

#Region " �ȈՈ����擾�P(AtenaGet1) "
    '*����ԍ� 000020 2003/11/19 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �ȈՈ����擾�P
    '* 
    '* �\��           Public Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* �@�@           blnKobetsu       : �ʎ擾(True:�e�ʃ}�X�^���f�[�^���擾����)
    '* 
    '* �߂�l         DataSet(ABAtena1Kobetsu) : �擾�����������
    '************************************************************************************************
    Public Overloads Function AtenaGet1(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                        ByVal blnKobetsu As Boolean) As DataSet
        '*����ԍ� 000030 2007/04/21 �C���J�n
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        'Dim cSearchKey As ABAtenaSearchKey                  '���������L�[
        'Dim csDataTable As DataTable
        'Dim csDataSet As DataSet
        ''* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        ''Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '���������}�X�^�c�`�N���X
        ''Dim cABAtenaB As ABAtenaBClass                      '�����}�X�^�c�`�N���X
        ''Dim cABSfskB As ABSfskBClass                        '���t��}�X�^�c�`�N���X
        ''Dim cABDainoB As ABDainoBClass                      '��[�}�X�^�c�`�N���X
        ''* ����ԍ� 000023 2004/08/27 �폜�I��
        'Dim csAtena1 As DataSet                             '�������(ABAtena1)
        'Dim csAtenaH As DataSet                             '�������(ABAtena1)
        'Dim csAtenaHS As DataSet                            '�������(ABAtena1)
        'Dim csAtenaD As DataSet                             '�������(ABAtena1)
        'Dim csAtenaDS As DataSet                            '�������(ABAtena1)
        'Dim strStaiCD As String                             '���уR�[�h
        'Dim intHyojiKensu As Integer                        '�ő�擾����
        'Dim intGetCount As Integer                          '�擾����
        'Dim strKikanYM As String                            '���ԔN��
        'Dim strDainoKB As String                            '��[�敪
        'Dim strGyomuCD As String                            '�Ɩ��R�[�h
        'Dim strGyomunaiSHU_CD As String                     '�Ɩ�����ʃR�[�h
        ''* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        ''Dim cUSSCityInfoClass As New USSCityInfoClass()     '�s�������Ǘ��N���X
        ''* ����ԍ� 000023 2004/08/27 �폜�I��
        'Dim strShichosonCD As String                        '�s�����R�[�h

        ''* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        'Dim csWkAtena As DataSet                             '�������(ABAtena1)
        ''* ����ԍ� 000024 2005/01/25 �ǉ��I��

        'Try
        '    ' �f�o�b�O�J�n���O�o��
        '    m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '    '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        '    ' RDB�A�N�Z�X���O�o��
        '    'm_cfLogClass.RdbWrite(m_cfControlData, _
        '    '                                "�y�N���X��:" + Me.GetType.Name + "�z" + _
        '    '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
        '    '                                "�y���s���\�b�h��:Connect�z")
        '    '* ����ԍ� 000023 2004/08/27 �폜�I��
        '    '�q�c�a�ڑ�
        '    If m_blnBatchRdb = False Then
        '        '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
        '        ' RDB�A�N�Z�X���O�o��
        '        m_cfLogClass.RdbWrite(m_cfControlData, _
        '                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
        '                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
        '                                        "�y���s���\�b�h��:Connect�z")
        '        '* ����ԍ� 000023 2004/08/27 �ǉ��I��
        '        m_cfRdbClass.Connect()
        '    End If
        '    Try
        '        '�p�����[�^�`�F�b�N
        '        Me.CheckColumnValue(cAtenaGetPara1)
        '        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        '        ''���������}�X�^�c�`�N���X�̃C���X�^���X�쐬
        '        'cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        ''�����}�X�^�c�`�N���X�̃C���X�^���X�쐬
        '        'cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        ''���t��}�X�^�c�`�N���X�̃C���X�^���X�쐬
        '        'cABSfskB = New ABSfskBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

        '        ''��[�}�X�^�c�`�N���X�̃C���X�^���X�쐬
        '        'cABDainoB = New ABDainoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '        '* ����ԍ� 000023 2004/08/27 �폜�J�n

        '        If (m_blnBatch) Then
        '            If (m_cABBatchAtenaHenshuB Is Nothing) Then
        '                '�����ҏW�o�b�`�N���X�̃C���X�^���X�쐬
        '                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        '                'm_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '                m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
        '                '* ����ԍ� 000024 2005/01/25 �X�V�I��
        '            End If
        '        Else
        '            If (m_cABAtenaHenshuB Is Nothing) Then
        '                '�����ҏW�N���X�̃C���X�^���X�쐬
        '                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        '                'm_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '                m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
        '                '* ����ԍ� 000024 2005/01/25 �X�V�I��
        '            End If
        '        End If

        '        'USSCityInfoClass.GetCityInfo()���g�p���āA���ߎs�������擾���擾����B
        '        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        '        'cUSSCityInfoClass.GetCityInfo(m_cfControlData)
        '        '* ����ԍ� 000023 2004/08/27 �폜�I��

        '        '�s�����R�[�h�̓��e��ݒ肷��B
        '        If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
        '            strShichosonCD = m_cUSSCityInfoClass.p_strShichosonCD(0)
        '        Else
        '            strShichosonCD = cAtenaGetPara1.p_strShichosonCD
        '        End If

        '        '���уR�[�h�̎w�肪�Ȃ��A���ш��ҏW�̎w��������ꍇ
        '        If cAtenaGetPara1.p_strStaiCD = "" And cAtenaGetPara1.p_strStaiinHenshu = "1" Then

        '            '���������L�[�̃C���X�^���X��
        '            cSearchKey = New ABAtenaSearchKey

        '            '�Z���R�[�h�̐ݒ�
        '            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

        '            '�Z��E�Z�o�O�敪��<>"1"�̏ꍇ
        '            If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '                cSearchKey.p_strJutogaiYusenKB = "1"
        '            End If

        '            '�Z��E�Z�o�O�敪��="1"�̏ꍇ
        '            If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '                cSearchKey.p_strJuminYuseniKB = "1"
        '            End If

        '            '�w��N�������w�肳��Ă���ꍇ
        '            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '                '�u���������}�X�^���o�v���]�b�g�����s����
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                            cSearchKey, _
        '                                                            cAtenaGetPara1.p_strShiteiYMD, _
        '                                                            cAtenaGetPara1.p_blnSakujoFG)

        '                '�擾�������P���łȂ��ꍇ�A�G���[
        '                If (csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 1) Then
        '                    '�G���[��`���擾
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
        '                End If

        '                strStaiCD = CType(csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.STAICD), String)
        '            End If

        '            '�w��N�������w�肳��Ă��Ȃ��ꍇ
        '            If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '                '�u�����}�X�^���o�v���]�b�g�����s����
        '                csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                     cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                '�擾�������P���łȂ��ꍇ�A�G���[
        '                If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count <> 1) Then
        '                    '�G���[��`���擾
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
        '                End If

        '                '���уR�[�h��NULL�̏ꍇ�A�G���[
        '                If CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String).Trim = String.Empty Then
        '                    '�G���[��`���擾
        '                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
        '                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
        '                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
        '                End If

        '                strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String)
        '            End If
        '            cAtenaGetPara1.p_strStaiCD = strStaiCD
        '            cAtenaGetPara1.p_strJuminCD = String.Empty
        '        End If

        '        cSearchKey = Nothing
        '        cSearchKey = New ABAtenaSearchKey

        '        '���ш��ҏW��"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strStaiinHenshu = "1" Then
        '            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
        '        Else
        '            '�����擾�p�����[�^���父�������L�[�ɃZ�b�g����
        '            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
        '            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
        '            cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
        '            cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
        '            cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
        '            cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
        '            cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
        '            cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
        '            cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
        '            cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
        '            cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
        '            cSearchKey.p_strShichosonCD = strShichosonCD
        '        End If

        '        '�Z��E�Z�o�O�敪��<>"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJutogaiYusenKB = "1"
        '        End If

        '        '�Z��E�Z�o�O�敪��="1"�̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJuminYuseniKB = "1"
        '        End If

        '        '�Z���`�Ԓn�R�[�h3�̃Z�b�g
        '        '�Z�o�O�D��̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
        '            cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
        '            cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
        '            cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
        '            cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
        '            cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
        '            cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
        '            cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
        '        End If

        '        '�Z��D��̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(8)
        '            cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.PadLeft(9)
        '            cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.PadLeft(8)
        '            cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.PadLeft(8)
        '            cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.PadLeft(8)
        '            cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.PadLeft(5)
        '            cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.PadLeft(5)
        '            cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.PadLeft(5)
        '        End If

        '        '�ő�擾�������Z�b�g����
        '        If cAtenaGetPara1.p_intHyojiKensu = 0 Then
        '            intHyojiKensu = 100
        '        Else
        '            intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
        '        End If

        '        '�w��N�������w�肳��Ă���ꍇ
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

        '            ' �����ʏ��̏ꍇ
        '            If (blnKobetsu) Then
        '                '�u�����ʗ����f�[�^���o�v���]�b�g�����s����
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(intHyojiKensu, _
        '                                                              cSearchKey, _
        '                                                              cAtenaGetPara1.p_strShiteiYMD, _
        '                                                              cAtenaGetPara1.p_blnSakujoFG)

        '                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

        '                If (m_blnBatch) Then
        '                    '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
        '                    csAtenaH = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
        '                Else
        '                    '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
        '                    csAtenaH = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
        '                End If
        '            Else
        '                '�u���������}�X�^���o�v���]�b�g�����s����
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(intHyojiKensu, _
        '                                                            cSearchKey, _
        '                                                            cAtenaGetPara1.p_strShiteiYMD, _
        '                                                            cAtenaGetPara1.p_blnSakujoFG)

        '                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

        '                If (m_blnBatch) Then
        '                    '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
        '                    csAtenaH = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
        '                Else
        '                    '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
        '                    csAtenaH = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
        '                End If
        '            End If
        '        Else
        '            '�w��N�������w�肳��Ă��Ȃ��ꍇ

        '            ' �����ʏ��̏ꍇ
        '            If (blnKobetsu) Then
        '                '�u�����ʏ�񒊏o�v���]�b�g�����s����
        '                csDataSet = m_cABAtenaB.GetAtenaBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

        '                If (m_blnBatch) Then
        '                    '�u�����ҏW�o�b�`�v�́u�����ʕҏW�v���\�b�h�����s����
        '                    csAtenaH = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
        '                Else
        '                    '�u�����ҏW�v�́u�����ʕҏW�v���\�b�h�����s����
        '                    csAtenaH = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
        '                End If
        '            Else
        '                '�u�����}�X�^���o�v���]�b�g�����s����
        '                csDataSet = m_cABAtenaB.GetAtenaBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

        '                If (m_blnBatch) Then
        '                    '�u�����ҏW�o�b�`�v�́u�����ҏW�v���\�b�h�����s����
        '                    csAtenaH = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
        '                Else
        '                    '�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
        '                    csAtenaH = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
        '                End If

        '            End If

        '        End If

        '        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        '        csWkAtena = csDataSet
        '        '* ����ԍ� 000024 2005/01/25 �ǉ��I��

        '        '*����ԍ� 000022 2003/12/02 �ǉ��J�n
        '        ' �A����ҏW����

        '        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        '        'Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtenaH)
        '        Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtenaH, csWkAtena)
        '        '* ����ԍ� 000024 2005/01/25 �X�V�I��
        '        '*����ԍ� 000022 2003/12/02 �ǉ��I��

        '        '�擾�p�����[�^�̋Ɩ��R�[�h���w�肳��Ă��Ȃ����A�擾������1���łȂ��ꍇ�́A�l��Ԃ�
        '        If cAtenaGetPara1.p_strGyomuCD = "" Or intGetCount <> 1 Then

        '            csAtena1 = csAtenaH

        '            Exit Try
        '        End If

        '        '�w��N�������w�肵�Ă��芎�擾�p�����[�^�̑��t��f�[�^�敪��"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            '* ����ԍ� 000026 2005/04/21 �C���J�n
        '            strKikanYM = m_strSystemDateTime
        '            ''''strKikanYM = "999999"
        '            '* ����ԍ� 000026 2005/04/21 �C���I��
        '        End If

        '        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        '        ''�u���t��}�X�^�c�`�v�́u���t��}�X�^���o�v���\�b�h�����s����
        '        'csDataSet = m_cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '        '                                   cAtenaGetPara1.p_strGyomuCD, _
        '        '                                   cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '        '                                   strKikanYM, _
        '        '                                   cAtenaGetPara1.p_blnSakujoFG)
        '        '�u���t��}�X�^�c�`�v�́u���t��}�X�^���o�v���\�b�h�����s����
        '        If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
        '            '���t�悪����̂œǂݍ���
        '            csDataSet = m_cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '                                               cAtenaGetPara1.p_strGyomuCD, _
        '                                               cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                               strKikanYM, _
        '                                               cAtenaGetPara1.p_blnSakujoFG)
        '        Else
        '            '���t�悪�����̂ŁA��̃e�[�u���쐬
        '            csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
        '        End If
        '        '* ����ԍ� 000024 2005/01/25 �X�V�I��

        '        ' �����ʏ��̏ꍇ
        '        If (blnKobetsu) Then
        '            If (m_blnBatch) Then
        '                '�u�����ҏW�o�b�`�v�́u���t��ʕҏW�v���\�b�h�����s����
        '                csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '            Else
        '                '�u�����ҏW�v�́u���t��ʕҏW�v���\�b�h�����s����
        '                csAtenaHS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '            End If
        '        Else
        '            If (m_blnBatch) Then
        '                '�u�����ҏW�o�b�`�v�́u���t��ҏW�v���\�b�h�����s����
        '                csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '            Else
        '                '�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
        '                csAtenaHS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
        '            End If
        '        End If

        '        '�w��N�������w�肵�Ă���ꍇ
        '        If (cAtenaGetPara1.p_strShiteiYMD <> "") Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            '* ����ԍ� 000026 2005/04/21 �C���J�n
        '            strKikanYM = m_strSystemDateTime
        '            ''''strKikanYM = "999999"
        '            '* ����ԍ� 000026 2005/04/21 �C���I��
        '        End If

        '        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        '        ''�u��[�}�X�^�c�`�v�́u��[�}�X�^���o�v���\�b�h�����s����
        '        'csDataSet = m_cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '        '                                     cAtenaGetPara1.p_strGyomuCD, _
        '        '                                     cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '        '                                     strKikanYM, _
        '        '                                     cAtenaGetPara1.p_blnSakujoFG)
        '        '�u��[�}�X�^�c�`�v�́u��[�}�X�^���o�v���\�b�h�����s����
        '        If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.DAINOCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.DAINOCOUNT + " > 0").Length > 0) Then
        '            '��[������̂œǂݍ���
        '            csDataSet = m_cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD, _
        '                                                cAtenaGetPara1.p_strGyomuCD, _
        '                                                 cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                                 strKikanYM, _
        '                                                 cAtenaGetPara1.p_blnSakujoFG)
        '        Else
        '            '��[�������̂ŁA��̃e�[�u���쐬
        '            csDataSet = m_cABDainoB.GetDainoSchemaBHoshu()
        '        End If
        '        '* ����ԍ� 000024 2005/01/25 �X�V�I��

        '        '�擾������1���łȂ��ꍇ
        '        If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count <> 1) Then

        '            'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)

        '            Exit Try
        '        End If

        '        With csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows(0)

        '            '��[�敪��ޔ�����
        '            strDainoKB = CType(.Item(ABDainoEntity.DAINOKB), String)

        '            '�Ɩ��R�[�h��ޔ�����
        '            strGyomuCD = CType(.Item(ABDainoEntity.GYOMUCD), String)

        '            '�Ɩ�����ʃR�[�h��ޔ�����
        '            strGyomunaiSHU_CD = CType(.Item(ABDainoEntity.GYOMUNAISHU_CD), String)

        '            '���������L�[�ɃZ�b�g����
        '            cSearchKey = Nothing
        '            cSearchKey = New ABAtenaSearchKey

        '            cSearchKey.p_strJuminCD = CType(.Item(ABDainoEntity.DAINOJUMINCD), String)

        '        End With

        '        '�Z��E�Z�o�O�敪��<>"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
        '            cSearchKey.p_strJutogaiYusenKB = "1"
        '        End If

        '        '�Z��E�Z�o�O�敪��="1"�̏ꍇ
        '        If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
        '            cSearchKey.p_strJuminYuseniKB = "1"
        '        End If

        '        '�O�w��N�������w�肳��Ă���ꍇ
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then

        '            ' �����ʏ��̏ꍇ
        '            If (blnKobetsu) Then

        '                '�u���������}�X�^�c�`�v�́u���������}�X�^���o�v���\�b�h�����s����
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                              cSearchKey, _
        '                                                              cAtenaGetPara1.p_strShiteiYMD, _
        '                                                              cAtenaGetPara1.p_blnSakujoFG)

        '                '�擾����
        '                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
        '                '�擾�������O���̏ꍇ�A
        '                If (intGetCount = 0) Then

        '                    'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
        '                    Exit Try
        '                End If

        '                If (m_blnBatch) Then
        '                    '�u�����ҏW�o�b�`�v�́u�����ʕҏW�v���\�b�h�����s����
        '                    csAtenaD = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                            strGyomuCD, strGyomunaiSHU_CD)
        '                Else
        '                    '�u�����ҏW�v�́u�����ʕҏW�v���\�b�h�����s����
        '                    csAtenaD = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                            strGyomuCD, strGyomunaiSHU_CD)
        '                End If
        '            Else
        '                '�u���������}�X�^�c�`�v�́u���������}�X�^���o�v���\�b�h�����s����
        '                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                            cSearchKey, _
        '                                                            cAtenaGetPara1.p_strShiteiYMD, _
        '                                                            cAtenaGetPara1.p_blnSakujoFG)

        '                '�擾����
        '                intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
        '                '�擾�������O���̏ꍇ�A
        '                If (intGetCount = 0) Then

        '                    'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
        '                    Exit Try
        '                End If

        '                If (m_blnBatch) Then
        '                    '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
        '                    csAtenaD = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                            strGyomuCD, strGyomunaiSHU_CD)
        '                Else
        '                    '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
        '                    csAtenaD = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                            strGyomuCD, strGyomunaiSHU_CD)
        '                End If
        '            End If
        '        Else

        '            '�P�w��N�������w�肳��Ă��Ȃ��ꍇ
        '            ' �����ʏ��̏ꍇ
        '            If (blnKobetsu) Then

        '                '�u�����ʃf�[�^���o�v���]�b�g�����s����
        '                csDataSet = m_cABAtenaB.GetAtenaBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                    cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                '�擾����
        '                intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
        '                '�擾�������O���̏ꍇ�A
        '                If (intGetCount = 0) Then

        '                    'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
        '                    Exit Try
        '                End If

        '                If (m_blnBatch) Then
        '                    '�u�����ҏW�o�b�`�v�́u�����ҏW�v���\�b�h�����s����
        '                    csAtenaD = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                           strGyomuCD, strGyomunaiSHU_CD)
        '                Else
        '                    '�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
        '                    csAtenaD = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                           strGyomuCD, strGyomunaiSHU_CD)
        '                End If

        '            Else

        '                '�u�����}�X�^���o�v���]�b�g�����s����
        '                csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
        '                                                    cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

        '                '�擾����
        '                intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
        '                '�擾�������O���̏ꍇ�A
        '                If (intGetCount = 0) Then

        '                    'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)
        '                    Exit Try
        '                End If

        '                If (m_blnBatch) Then
        '                    '�u�����ҏW�o�b�`�v�́u�����ҏW�v���\�b�h�����s����
        '                    csAtenaD = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                           strGyomuCD, strGyomunaiSHU_CD)
        '                Else
        '                    '�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
        '                    csAtenaD = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB, _
        '                                                           strGyomuCD, strGyomunaiSHU_CD)
        '                End If
        '            End If
        '        End If

        '        '�w��N�������w�肵�Ă��芎�擾�p�����[�^�̑��t��f�[�^�敪��"1"�̏ꍇ
        '        If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
        '            strKikanYM = cAtenaGetPara1.p_strShiteiYMD.Substring(0, 6)
        '        Else
        '            '* ����ԍ� 000026 2005/04/21 �C���J�n
        '            strKikanYM = m_strSystemDateTime
        '            ''''strKikanYM = "999999"
        '            '* ����ԍ� 000026 2005/04/21 �C���I��
        '        End If

        '        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        '        '�u���t��}�X�^�c�`�v�́u���t��}�X�^���o�v���\�b�h�����s����
        '        'csDataSet = m_cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, _
        '        '                                   cAtenaGetPara1.p_strGyomuCD, _
        '        '                                   cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '        '                                   strKikanYM, _
        '        '                                   cAtenaGetPara1.p_blnSakujoFG)
        '        If (csDataSet.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
        '            '���t�悪����̂œǂݍ���
        '            csDataSet = m_cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD, _
        '                                               cAtenaGetPara1.p_strGyomuCD, _
        '                                               cAtenaGetPara1.p_strGyomunaiSHU_CD, _
        '                                               strKikanYM, _
        '                                               cAtenaGetPara1.p_blnSakujoFG)
        '        Else
        '            '���t�悪�����̂ŁA��̃e�[�u���쐬
        '            csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
        '        End If
        '        '* ����ԍ� 000024 2005/01/25 �X�V�I��

        '        ' �����ʏ��̏ꍇ
        '        If (blnKobetsu) Then
        '            If (m_blnBatch) Then
        '                '�u�����ҏW�o�b�`�v�́u���t��ҏW�v���\�b�h�����s����
        '                csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '            Else
        '                '�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
        '                csAtenaDS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '            End If
        '        Else
        '            If (m_blnBatch) Then
        '                '�u�����ҏW�o�b�`�v�́u���t��ҏW�v���\�b�h�����s����
        '                csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '            Else
        '                '�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
        '                csAtenaDS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
        '            End If
        '        End If

        '        'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
        '        csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu)



        '    Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
        '        ' ���[�j���O���O�o��
        '        m_cfLogClass.WarningWrite(m_cfControlData, _
        '                                "�y�N���X��:" + Me.GetType.Name + "�z" + _
        '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
        '                                "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" + _
        '                                "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
        '        ' UFAppException���X���[����
        '        Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        '    Catch
        '        ' �G���[�����̂܂܃X���[
        '        Throw

        '    Finally
        '        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        '        ' RDB�A�N�Z�X���O�o��
        '        'm_cfLogClass.RdbWrite(m_cfControlData, _
        '        '                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
        '        '                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
        '        '                        "�y���s���\�b�h��:Disconnect�z")
        '        '* ����ԍ� 000023 2004/08/27 �폜�I��
        '        ' RDB�ؒf
        '        If m_blnBatchRdb = False Then
        '            '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
        '            ' RDB�A�N�Z�X���O�o��
        '            m_cfLogClass.RdbWrite(m_cfControlData, _
        '                                    "�y�N���X��:" + Me.GetType.Name + "�z" + _
        '                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
        '                                    "�y���s���\�b�h��:Disconnect�z")
        '            '* ����ԍ� 000023 2004/08/27 �ǉ��I��
        '            m_cfRdbClass.Disconnect()
        '        End If
        '    End Try

        '    ' �f�o�b�O�I�����O�o��
        '    m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'Catch objAppExp As UFAppException
        '    ' ���[�j���O���O�o��
        '    m_cfLogClass.WarningWrite(m_cfControlData, _
        '                                "�y�N���X��:" + Me.GetType.Name + "�z" + _
        '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
        '                                "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
        '                                "�y���[�j���O���e:" + objAppExp.Message + "�z")
        '    ' �G���[�����̂܂܃X���[����
        '    Throw objAppExp

        'Catch objExp As Exception
        '    ' �G���[���O�o��
        '    m_cfLogClass.ErrorWrite(m_cfControlData, _
        '                                "�y�N���X��:" + Me.GetType.Name + "�z" + _
        '                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
        '                                "�y�G���[���e:" + objExp.Message + "�z")
        '    Throw objExp
        'End Try

        'Return csAtena1

        Return AtenaGetMain(cAtenaGetPara1, blnKobetsu, ABEnumDefine.MethodKB.KB_AtenaGet1, ABEnumDefine.HyojunKB.KB_Tsujo)
        '*����ԍ� 000030 2007/04/21 �C���I��

    End Function
    '*����ԍ� 000020 2003/11/19 �ǉ��I��
#End Region

    '*����ԍ� 000030 2007/04/21 �ǉ��J�n
#Region " �����擾���C���i�ȈՈ����擾�P�A���p�����擾�j "
    '************************************************************************************************
    '* ���\�b�h��     �����擾���C���i�ȈՈ����擾�P�A���p�����擾�j
    '* 
    '* �\��           Public Function AtenaGetMain(ByVal cAtenaGetPara1 As ABAtenaGetPara1, _
    '*                    ByVal blnKobetsu As Boolean, ByVal MethodKB As ABEnumDefine.MethodKB) As DataSet
    '*
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* �@�@           blnKobetsu       : �ʎ擾(True:�e�ʃ}�X�^���f�[�^���擾����)
    '* �@�@           MethodKB         : call���ꂽ���\�b�h�̎�ނ�\��
    '* 
    '* �߂�l         DataSet(ABAtena1Kobetsu) : �擾�����������
    '************************************************************************************************
    Private Function AtenaGetMain(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                  ByVal blnKobetsu As Boolean, ByVal blnMethodKB As ABEnumDefine.MethodKB,
                                  ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim cSearchKey As ABAtenaSearchKey                  '���������L�[
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim csDataTable As DataTable
        '* corresponds to VS2008 End 2010/04/16 000044
        Dim csDataSet As DataSet
        Dim csAtena1 As DataSet                             '�������(ABAtena1)
        Dim csAtenaH As DataSet                             '�������(ABAtena1)
        Dim csAtenaHS As DataSet                            '�������(ABAtena1)
        Dim csAtenaD As DataSet                             '�������(ABAtena1)
        Dim csAtenaDS As DataSet                            '�������(ABAtena1)
        Dim strStaiCD As String                             '���уR�[�h
        Dim intHyojiKensu As Integer                        '�ő�擾����
        Dim intGetCount As Integer                          '�擾����
        Dim strKikanYMD As String                           '���ԔN����
        Dim strDainoKB As String                            '��[�敪
        Dim strGyomuCD As String                            '�Ɩ��R�[�h
        Dim strGyomunaiSHU_CD As String                     '�Ɩ�����ʃR�[�h
        Dim strShichosonCD As String                        '�s�����R�[�h
        Dim csWkAtena As DataSet                             '�������(ABAtena1)

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            '=====================================================================================================================
            '== �P�D�q�c�a�ڑ�
            '==�@�@�@�@
            '==�@�@�@�@<����>�@�o�b�`�v���O��������Ăяo���ꂽ�ꍇ�ȂǁA����q�c�a�ڑ����s��Ȃ�������s���B
            '==�@�@�@�@
            '=====================================================================================================================
            If m_blnBatchRdb = False Then
                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "�y�N���X��:" + Me.GetType.Name + "�z" +
                                                "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                                "�y���s���\�b�h��:Connect�z")
                m_cfRdbClass.Connect()
            End If

            Try
                '=====================================================================================================================
                '== �Q�D�����擾�p�����[�^�`�F�b�N
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�p�����[�^�N���X�Ɏw�肳�ꂽ���e���`�F�b�N����B
                '==�@�@�@�@
                '=====================================================================================================================
                Me.CheckColumnValue(cAtenaGetPara1, intHyojunKB)

                '=====================================================================================================================
                '== �R�D�e��N���X�̃C���X�^���X��
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�o�b�`�t���O�̏ꍇ�����ɂ��A���A���p�E�o�b�`�p�N���X���C���X�^���X������B
                '==�@�@�@�@
                '=====================================================================================================================
                If (m_blnBatch) Then
                    If (m_cABBatchAtenaHenshuB Is Nothing) Then
                        '�����ҏW�o�b�`�N���X�̃C���X�^���X�쐬
                        m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                        m_cABBatchAtenaHenshuB.m_blnMethodKB = blnMethodKB               '�����ҏW�a�N���X
                    End If
                    m_cABBatchAtenaHenshuB.m_intHyojunKB = intHyojunKB
                Else
                    If (m_cABAtenaHenshuB Is Nothing) Then
                        '�����ҏW�N���X�̃C���X�^���X�쐬
                        m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                        '���s���\�b�h�ɂ��o�̓��C�A�E�g��ύX����
                        m_cABAtenaHenshuB.m_blnMethodKB = blnMethodKB               '�����ҏW�a�N���X
                    End If
                    m_cABAtenaHenshuB.m_intHyojunKB = intHyojunKB
                End If
                '���s���\�b�h�ɂ��o�̓��C�A�E�g��ύX����
                m_cABAtenaB.m_blnMethodKB = blnMethodKB                             '�����a�N���X
                m_cABAtenaRirekiB.m_blnMethodKB = blnMethodKB                      '���������a�N���X
                m_cABAtenaB.m_intHyojunKB = intHyojunKB
                m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB

                '*����ԍ� 000042 2008/11/18 �ǉ��J�n
                m_blnMethodKB = blnMethodKB
                '*����ԍ� 000042 2008/11/18 �ǉ��I��

                '*����ԍ� 000045 2010/05/17 �ǉ��J�n
                ' �����a�N���X�e��v���p�e�B���Z�b�g
                m_cABAtenaB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
                m_cABAtenaB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
                '*����ԍ� 000046 2011/05/18 �ǉ��J�n
                m_cABAtenaB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
                '*����ԍ� 000046 2011/05/18 �ǉ��I��
                '*����ԍ� 000047 2011/11/07 �ǉ��J�n
                m_cABAtenaB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
                '*����ԍ� 000047 2011/11/07 �ǉ��I��
                '*����ԍ� 000048 2014/04/28 �ǉ��J�n
                m_cABAtenaB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
                '*����ԍ� 000048 2014/04/28 �ǉ��I��

                ' ���������a�N���X�e��v���p�e�B���Z�b�g
                m_cABAtenaRirekiB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
                m_cABAtenaRirekiB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
                '*����ԍ� 000046 2011/05/18 �ǉ��J�n
                m_cABAtenaRirekiB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
                '*����ԍ� 000046 2011/05/18 �ǉ��I��
                '*����ԍ� 000047 2011/11/07 �ǉ��J�n
                m_cABAtenaRirekiB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
                '*����ԍ� 000047 2011/11/07 �ǉ��I��
                '*����ԍ� 000045 2010/05/17 �ǉ��I��
                '*����ԍ� 000048 2014/04/28 �ǉ��J�n
                m_cABAtenaRirekiB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
                '*����ԍ� 000048 2014/04/28 �ǉ��I��

                '=====================================================================================================================
                '== �S�D�s�����R�[�h�ݒ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@���s�����R�[�h�̎w�肪�Ȃ��ꍇ�́A����(����)�̎s�����R�[�h��ݒ肷��B
                '==�@�@�@�@
                '=====================================================================================================================
                If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
                    strShichosonCD = m_cUSSCityInfoClass.p_strShichosonCD(0)
                Else
                    strShichosonCD = cAtenaGetPara1.p_strShichosonCD
                End If


                '=====================================================================================================================
                '== �T�D���ш��ҏW���̐��уR�[�h���擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�����ш��ҏW�̎w�肪����ꍇ�́A�����уR�[�h���g�p�����ш����擾����B
                '==�@�@�@�@�@�@�@�@�����уR�[�h���w�肳��Ă��Ȃ������ꍇ�́��Z���R�[�h�ɂ�萢�уR�[�h�̎擾���s���B
                '==�@�@�@�@
                '=====================================================================================================================
                '���уR�[�h�̎w�肪�Ȃ��A���ш��ҏW�̎w��������ꍇ
                If cAtenaGetPara1.p_strStaiCD = "" And cAtenaGetPara1.p_strStaiinHenshu = "1" Then

                    '���������L�[�̃C���X�^���X��
                    cSearchKey = New ABAtenaSearchKey

                    '�Z���R�[�h�̐ݒ�
                    cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

                    '�Z��E�Z�o�O�敪��<>"1"�̏ꍇ
                    If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
                        cSearchKey.p_strJutogaiYusenKB = "1"
                    End If

                    '�Z��E�Z�o�O�敪��="1"�̏ꍇ
                    If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                        cSearchKey.p_strJuminYuseniKB = "1"
                    End If

                    '�w��N�������w�肳��Ă���ꍇ
                    If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

                        '�u���������}�X�^���o�v���]�b�g�����s����
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                                    cSearchKey,
                                                                    cAtenaGetPara1.p_strShiteiYMD,
                                                                    cAtenaGetPara1.p_blnSakujoFG)

                        '�擾�������P���łȂ��ꍇ�A�G���[
                        If (csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count <> 1) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
                        End If

                        strStaiCD = CType(csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows(0).Item(ABAtenaRirekiEntity.STAICD), String)
                    End If

                    '�w��N�������w�肳��Ă��Ȃ��ꍇ
                    If (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

                        '�u�����}�X�^���o�v���]�b�g�����s����
                        csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                             cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

                        '�擾�������P���łȂ��ꍇ�A�G���[
                        If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count <> 1) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
                        End If

                        '���уR�[�h��NULL�̏ꍇ�A�G���[
                        If CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String).Trim = String.Empty Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
                        End If

                        strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0).Item(ABAtenaEntity.STAICD), String)
                    End If
                    cAtenaGetPara1.p_strStaiCD = strStaiCD
                    cAtenaGetPara1.p_strJuminCD = String.Empty
                End If



                '*����ԍ� 000031 2007/07/28 �ǉ��J�n
                '=====================================================================================================================
                '== �U�D����l��\�Ҏ擾����
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�Z���R�[�h�E�Z�o�O�D��E����l����FG�L���̌��������̏ꍇ�̂݁A����l��\�Ҏ擾���s���B
                '==�@�@�@�@�@�@�@�@�Ǘ����ɂ��A���[�U���Ƃ̎擾����L��B
                '==�@�@�@�@
                '=====================================================================================================================
                '����l��\�ҏZ���R�[�h�������p�����[�^�ɏ㏑������
                GetDaihyoJuminCD(cAtenaGetPara1)
                '*����ԍ� 000031 2007/07/28 �ǉ��I��



                '=====================================================================================================================
                '== �V�D�{�l�����擾�����L�[�̐ݒ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�{�l�̈��������擾���邽�߂̌����L�[���w�肳�ꂽ�p�����[�^�N���X���ݒ肷��B
                '==�@�@�@�@�@�@�@�@�ő�擾�������擾����B
                '==�@�@�@�@
                '=====================================================================================================================
                '�����L�[�N���X�̏������ƃC���X�^���X��
                cSearchKey = Nothing
                cSearchKey = New ABAtenaSearchKey

                '���ш��ҏW��"1"�̏ꍇ
                If cAtenaGetPara1.p_strStaiinHenshu = "1" Then
                    cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
                Else
                    '�����擾�p�����[�^���父�������L�[�ɃZ�b�g����
                    cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
                    cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
                    cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
                    cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
                    cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
                    cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
                    cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
                    cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
                    cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
                    cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
                    cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
                    cSearchKey.p_strShichosonCD = strShichosonCD

                    '*����ԍ� 000032 2007/09/04 �ǉ��J�n
                    '�����p�J�i�����E�����p�J�i���E�����p�J�i���̕ҏW
                    cSearchKey = HenshuSearchKana(cSearchKey, cAtenaGetPara1.p_blnGaikokuHommyoYusen)
                    '*����ԍ� 000032 2007/09/04 �ǉ��I��

                    '*����ԍ� 000048 2014/04/28 �ǉ��J�n
                    cSearchKey.p_strMyNumber = cAtenaGetPara1.p_strMyNumber.RPadRight(13)
                    cSearchKey.p_strMyNumberKojinHojinKB = cAtenaGetPara1.p_strMyNumberKojinHojinKB
                    cSearchKey.p_strMyNumberChokkinSearchKB = cAtenaGetPara1.p_strMyNumberChokkinSearchKB
                    '*����ԍ� 000048 2014/04/28 �ǉ��I��
                    cSearchKey.p_strKyuuji = cAtenaGetPara1.p_strKyuuji
                    cSearchKey.p_strKanaKyuuji = cAtenaGetPara1.p_strKanaKyuuji
                    cSearchKey.p_strKatakanaHeikimei = cAtenaGetPara1.p_strKatakanaHeikimei
                    cSearchKey.p_strJusho = cAtenaGetPara1.p_strJusho
                    cSearchKey.p_strKatagaki = cAtenaGetPara1.p_strKatagaki
                    cSearchKey.p_strRenrakusaki = cAtenaGetPara1.p_strRenrakusaki
                End If

                '�Z��E�Z�o�O�敪��<>"1"�̏ꍇ
                If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
                    cSearchKey.p_strJutogaiYusenKB = "1"
                End If

                '�Z��E�Z�o�O�敪��="1"�̏ꍇ
                If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                    cSearchKey.p_strJuminYuseniKB = "1"
                End If

                '�Z���`�Ԓn�R�[�h3�̃Z�b�g
                '�Z�o�O�D��̏ꍇ
                If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
                    cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD
                    cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9)
                    cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8)
                    cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8)
                    cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8)
                    cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5)
                    cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5)
                    cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5)
                End If

                '�Z��D��̏ꍇ
                If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                    cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.Trim.RPadLeft(8)
                    cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9)
                    cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8)
                    cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8)
                    cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8)
                    cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5)
                    cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5)
                    cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5)
                End If

                '*����ԍ� 000049 2018/03/08 �ǉ��J�n
                ' ���������t���O
                cSearchKey.p_blnIsRirekiSearch = cAtenaGetPara1.p_blnIsRirekiSearch
                '*����ԍ� 000049 2018/03/08 �ǉ��I��

                '�ő�擾�������Z�b�g����
                If cAtenaGetPara1.p_intHyojiKensu = 0 Then
                    intHyojiKensu = 100
                Else
                    intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
                End If


                '=====================================================================================================================
                '== �W�D�{�l�����f�[�^�̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�{�l�̈��������擾����B
                '==�@�@�@�@�@�@�@�@�@. �w��N����������ꍇ�́u���������}�X�^�FABATENARIREKI�v�ɂ��擾����
                '==�@�@�@�@�@�@�@�@�A. �w��N�������Ȃ��ꍇ�́u�����}�X�^�FABATENA�v�ɂ��擾����
                '==�@�@�@�@�@�@�@�@�B. �ʎ����e�f�̎w�肪����ꍇ�͌ʎ����f�[�^���擾����
                '==�@�@�@�@�@�@�@�@�C. �o�b�`�ł̎w�肪����ꍇ�̓o�b�`�ł̃N���X�ɂ��擾����
                '==�@�@�@�@
                '=====================================================================================================================
                '�w��N�������w�肳��Ă���ꍇ
                If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then

                    ' �����ʏ��̏ꍇ
                    If (blnKobetsu) Then
                        '*����ԍ� 000038 2008/01/17 �C���J�n
                        '�u�����ʗ����f�[�^���o�v���]�b�g�����s����
                        'csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(intHyojiKensu, _
                        '                                              cSearchKey, _
                        '                                              cAtenaGetPara1.p_strShiteiYMD, _
                        '                                              cAtenaGetPara1.p_blnSakujoFG)
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(intHyojiKensu,
                                                                     cSearchKey,
                                                                     cAtenaGetPara1.p_strShiteiYMD,
                                                                     cAtenaGetPara1.p_blnSakujoFG,
                                                                     cAtenaGetPara1.p_strKobetsuShutokuKB)
                        '*����ԍ� 000038 2008/01/17 �C���I��

                        intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

                        If (m_blnBatch) Then
                            '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
                            csAtenaH = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
                        Else
                            '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
                            csAtenaH = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet)
                        End If
                    Else
                        '�u���������}�X�^���o�v���]�b�g�����s����
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(intHyojiKensu,
                                                                    cSearchKey,
                                                                    cAtenaGetPara1.p_strShiteiYMD,
                                                                    cAtenaGetPara1.p_blnSakujoFG)

                        intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count

                        If (m_blnBatch) Then
                            '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
                            csAtenaH = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
                        Else
                            '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
                            csAtenaH = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
                        End If
                    End If
                Else
                    '�w��N�������w�肳��Ă��Ȃ��ꍇ

                    ' �����ʏ��̏ꍇ
                    If (blnKobetsu) Then
                        '*����ԍ� 000038 2008/01/17 �C���J�n
                        '�u�����ʏ�񒊏o�v���\�b�h�����s����
                        'csDataSet = m_cABAtenaB.GetAtenaBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)
                        csDataSet = m_cABAtenaB.GetAtenaBKobetsu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG, cAtenaGetPara1.p_strKobetsuShutokuKB)
                        '*����ԍ� 000038 2008/01/17 �C���I��

                        intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

                        If (m_blnBatch) Then
                            '�u�����ҏW�o�b�`�v�́u�����ʕҏW�v���\�b�h�����s����
                            csAtenaH = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
                        Else
                            '�u�����ҏW�v�́u�����ʕҏW�v���\�b�h�����s����
                            csAtenaH = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet)
                        End If
                    Else
                        '�u�����}�X�^���o�v���]�b�g�����s����
                        csDataSet = m_cABAtenaB.GetAtenaBHoshu(intHyojiKensu, cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

                        intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count

                        If (m_blnBatch) Then
                            '�u�����ҏW�o�b�`�v�́u�����ҏW�v���\�b�h�����s����
                            csAtenaH = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
                        Else
                            '�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
                            csAtenaH = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet)
                        End If

                    End If

                End If

                csWkAtena = csDataSet

                '*����ԍ� 000040 2008/11/10 �ǉ��J�n
                '=====================================================================================================================
                '== �X�D���p�̓f�[�^�̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@���p�̓f�[�^�̎擾
                '==�@�@�@�@�@�@�@�@�@. �W�����C�A�E�g�̏ꍇ���A�����ʏ��ȊO�̏ꍇ�ɏ������s��
                '==�@�@�@�@�@�@�@�@�A. ���p�͏o�擾�敪��"1,2"�̏ꍇ�ɏ������s��
                '==�@�@�@�@�@�@�@�@�B. �Z���R�[�h�A�Ŗڋ敪�Ȃǂ��痘�p�̓f�[�^���擾���A�[�Ŏ�ID�A���p��ID�ɃZ�b�g����
                '==�@�@�@�@
                '=====================================================================================================================
                Me.RiyoTdkHenshu(cAtenaGetPara1, blnKobetsu, csAtenaH)

                '*����ԍ� 000041 2008/11/17 �ǉ��J�n
                ' ���p�͋敪��"2"�̏ꍇ�A�Y���f�[�^�ȊO���폜�����̂ŐV�K�������Z�b�g����
                If (cAtenaGetPara1.p_strTdkdKB = "2") Then
                    intGetCount = csAtenaH.Tables(0).Rows.Count
                Else
                End If
                '*����ԍ� 000041 2008/11/17 �ǉ��I��
                '*����ԍ� 000040 2008/11/10 �ǉ��I��

                '=====================================================================================================================
                '== �P�O�D�A����f�[�^�̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�A��������擾����B
                '==�@�@�@�@�@�@�@�@�@. �Ɩ��R�[�h�����݂��Ȃ��ꍇ�́A�������Ȃ�
                '==�@�@�@�@�@�@�@�@�A. �w�肵���Ɩ��R�[�h�E�Ɩ�����ʃR�[�h�������Ɂu�A����}�X�^�FABRENRAKUSAKI�v����擾����
                '==�@�@�@�@�@�@�@�@�B. �A.�Ńf�[�^���擾�����ꍇ�A�������ɘA����P�A�A����Q��ԋp����
                '==�@�@�@�@�@�@�@�@�C. �N�������Q�b�g�E�ʃQ�b�g�̃��C�A�E�g�̏ꍇ�̂݁u�A����Ɩ��R�[�h�v�ɒ��o�����̋Ɩ��R�[�h���Z�b�g����
                '==�@�@�@�@
                '=====================================================================================================================
                '�w��N�������w�肵�Ă��芎�擾�p�����[�^�̑��t��f�[�^�敪��"1"�̏ꍇ
                If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If
                Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtenaH, csWkAtena, intHyojunKB, strKikanYMD)


                '=====================================================================================================================
                '== �P�P�D��[�E���t��f�[�^�擾�̔���
                '==�@�@�@�@
                '==�@�@�@�@<����>�@���Ɩ��R�[�h�̎w�肪�Ȃ��ꍇ�́A�����������I�ɏI������B
                '==�@�@�@�@�@�@�@�@�{�l�f�[�^�̎擾�������P���łȂ��ꍇ�������������I�ɏI������B
                '==�@�@�@�@
                '=====================================================================================================================
                '�擾�p�����[�^�̋Ɩ��R�[�h���w�肳��Ă��Ȃ����A�擾������1���łȂ��ꍇ�́A�l��Ԃ�
                If cAtenaGetPara1.p_strGyomuCD = "" Or intGetCount <> 1 Then

                    csAtena1 = csAtenaH

                    '�������I������
                    Exit Try
                End If


                '=====================================================================================================================
                '== �P�Q�D���t��f�[�^�̒��o����ݒ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@���t��f�[�^�̒��o�ɂ����āA���w����̎w�肪����A�������t��f�[�^�敪�� "1" �̏ꍇ��
                '==�@�@�@�@�@�@�@�@�w�肳�ꂽ���t���L�����ԂɊ܂܂�Ă��邱�Ƃ������Ƃ���B
                '==�@�@�@�@�@�@�@�@��L�ȊO�́A�V�X�e�����t���L�����ԂɊ܂܂��Ă��邱�Ƃ������Ƃ���B
                '==�@�@�@�@
                '=====================================================================================================================
                '�w��N�������w�肵�Ă��芎�擾�p�����[�^�̑��t��f�[�^�敪��"1"�̏ꍇ
                If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If


                '=====================================================================================================================
                '== �P�R�D���t��f�[�^�̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@���t��f�[�^�̌����ɂ��A���݂��Ă���ꍇ�̂ݑ��t��f�[�^�̎擾���s���B
                '==�@�@�@�@�@�@�@�@�擾���s��Ȃ������ꍇ�́A��̃e�[�u�����쐬����B
                '==�@�@�@�@
                '=====================================================================================================================
                '�u���t��}�X�^�c�`�v�́u���t��}�X�^���o�v���\�b�h�����s����
                If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
                    '���t�悪����̂œǂݍ���
                    If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataSet = m_cABSfskB.GetSfskBHoshu_Hyojun(cAtenaGetPara1.p_strJuminCD,
                                                       cAtenaGetPara1.p_strGyomuCD,
                                                       cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                       strKikanYMD,
                                                       cAtenaGetPara1.p_blnSakujoFG)
                    Else
                        csDataSet = m_cABSfskB.GetSfskBHoshu(cAtenaGetPara1.p_strJuminCD,
                                                       cAtenaGetPara1.p_strGyomuCD,
                                                       cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                       strKikanYMD,
                                                       cAtenaGetPara1.p_blnSakujoFG)
                    End If
                Else
                    '���t�悪�����̂ŁA��̃e�[�u���쐬
                    If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataSet = m_cABSfskB.GetSfskSchemaBHoshu_Hyojun()
                    Else
                        csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
                    End If
                End If


                '=====================================================================================================================
                '== �P�S�D���t��f�[�^�̃��C�A�E�g�ҏW
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�ʎ����e�f�̎w�肪����ꍇ�́A���t��f�[�^���ʎ������ڂ��t�����ꂽ���C�A�E�g�ɕҏW����B
                '==�@�@�@�@�@�@�@�@�܂��A�o�b�`�ŁE���A���łɂ��g�p����N���X�𕪂���B
                '==�@�@�@�@
                '=====================================================================================================================
                ' �����ʏ��̏ꍇ
                If (blnKobetsu) Then
                    If (m_blnBatch) Then
                        '�u�����ҏW�o�b�`�v�́u���t��ʕҏW�v���\�b�h�����s����
                        csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
                    Else
                        '�u�����ҏW�v�́u���t��ʕҏW�v���\�b�h�����s����
                        csAtenaHS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
                    End If
                Else
                    If (m_blnBatch) Then
                        '�u�����ҏW�o�b�`�v�́u���t��ҏW�v���\�b�h�����s����
                        csAtenaHS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
                    Else
                        '�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
                        csAtenaHS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaH, csDataSet)
                    End If
                End If


                '=====================================================================================================================
                '== �P�T�D��[�f�[�^�̒��o����ݒ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@��[�f�[�^�̒��o�ɂ����āA���w����̎w�肪����ꍇ�́A�w�肳�ꂽ���t���L�����ԂɊ܂܂�Ă���
                '==�@�@�@�@�@�@�@�@���Ƃ������Ƃ���B
                '==�@�@�@�@�@�@�@�@��L�ȊO�́A�V�X�e�����t���L�����ԂɊ܂܂��Ă��邱�Ƃ������Ƃ���B
                '==�@�@�@�@
                '=====================================================================================================================
                '�w��N�������w�肵�Ă���ꍇ
                If (cAtenaGetPara1.p_strShiteiYMD <> "") Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If


                '=====================================================================================================================
                '== �P�U�D��[�f�[�^�̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@��[�f�[�^�̌����ɂ��A���݂��Ă���ꍇ�̂ݑ�[�f�[�^�̎擾���s���B
                '==�@�@�@�@�@�@�@�@�擾���s��Ȃ������ꍇ�́A��̃e�[�u�����쐬����B
                '==�@�@�@�@
                '=====================================================================================================================
                '�u��[�}�X�^�c�`�v�́u��[�}�X�^���o�v���\�b�h�����s����
                If (csWkAtena.Tables(0).Select(ABAtenaCountEntity.DAINOCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.DAINOCOUNT + " > 0").Length > 0) Then
                    '��[������̂œǂݍ���
                    csDataSet = m_cABDainoB.GetDainoBHoshu(cAtenaGetPara1.p_strJuminCD,
                                                        cAtenaGetPara1.p_strGyomuCD,
                                                         cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                         strKikanYMD,
                                                         cAtenaGetPara1.p_blnSakujoFG)
                Else
                    '��[�������̂ŁA��̃e�[�u���쐬
                    csDataSet = m_cABDainoB.GetDainoSchemaBHoshu()
                End If


                '=====================================================================================================================
                '== �P�V�D�擾�f�[�^�̃}�[�W
                '==�@�@�@�@
                '==�@�@�@�@<����>�@��[�f�[�^�̎擾�������P���łȂ��ꍇ�́A�u�{�l�v�u���t��v�u��[�l�v�u��[���t��v�f�[�^��
                '==�@�@�@�@�@�@�@�@�P�̃f�[�^�Z�b�g�Ƀ}�[�W���A�����������I�ɏI������B
                '==�@�@�@�@�@�@�@�@���̎��_�ł́A�u��[�l�v�u��[���t��v�f�[�^�͋�ł���B
                '==�@�@�@�@
                '=====================================================================================================================
                '�擾������1���łȂ��ꍇ
                If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count <> 1) Then

                    'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
                    csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                    '�������I������
                    Exit Try
                End If


                '=====================================================================================================================
                '== �P�W�D��[�l�����擾�����L�[�̐ݒ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@��[�l�̈��������擾���邽�߂̌����L�[���w�肳�ꂽ�p�����[�^�N���X���ݒ肷��B
                '==�@�@�@�@�@�@�@�@���̎��A��[�敪�E�Ɩ��R�[�h�E�Ɩ�����ʃR�[�h��ޔ�����B
                '==�@�@�@�@
                '=====================================================================================================================
                With csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows(0)

                    '��[�敪��ޔ�����
                    strDainoKB = CType(.Item(ABDainoEntity.DAINOKB), String)

                    '�Ɩ��R�[�h��ޔ�����
                    strGyomuCD = CType(.Item(ABDainoEntity.GYOMUCD), String)

                    '�Ɩ�����ʃR�[�h��ޔ�����
                    strGyomunaiSHU_CD = CType(.Item(ABDainoEntity.GYOMUNAISHU_CD), String)

                    '���������L�[�ɃZ�b�g����
                    cSearchKey = Nothing
                    cSearchKey = New ABAtenaSearchKey

                    cSearchKey.p_strJuminCD = CType(.Item(ABDainoEntity.DAINOJUMINCD), String)

                End With

                '�Z��E�Z�o�O�敪��<>"1"�̏ꍇ
                If cAtenaGetPara1.p_strJukiJutogaiKB <> "1" Then
                    cSearchKey.p_strJutogaiYusenKB = "1"
                End If

                '�Z��E�Z�o�O�敪��="1"�̏ꍇ
                If cAtenaGetPara1.p_strJukiJutogaiKB = "1" Then
                    cSearchKey.p_strJuminYuseniKB = "1"
                End If


                '=====================================================================================================================
                '== �P�X�D��[�l�����f�[�^�̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@��[�l�̈��������擾����B
                '==�@�@�@�@�@�@�@�@�@. �w��N����������ꍇ�́u���������}�X�^�FABATENARIREKI�v�ɂ��擾����
                '==�@�@�@�@�@�@�@�@�A. �w��N�������Ȃ��ꍇ�́u�����}�X�^�FABATENA�v�ɂ��擾����
                '==�@�@�@�@�@�@�@�@�B. �ʎ����e�f�̎w�肪����ꍇ�͌ʎ����f�[�^���擾����
                '==�@�@�@�@�@�@�@�@�C. �o�b�`�ł̎w�肪����ꍇ�̓o�b�`�ł̃N���X�ɂ��擾����
                '==�@�@�@�@
                '=====================================================================================================================
                '�w��N�������w�肳��Ă���ꍇ
                If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then

                    ' �����ʏ��̏ꍇ
                    If (blnKobetsu) Then

                        '*����ԍ� 000038 2008/01/17 �C���J�n
                        '�u���������}�X�^�c�`�v�́u���������}�X�^���o�v���\�b�h�����s����
                        'csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
                        '                                              cSearchKey, _
                        '                                              cAtenaGetPara1.p_strShiteiYMD, _
                        '                                              cAtenaGetPara1.p_blnSakujoFG)
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBKobetsu(cAtenaGetPara1.p_intHyojiKensu,
                                                                        cSearchKey,
                                                                        cAtenaGetPara1.p_strShiteiYMD,
                                                                        cAtenaGetPara1.p_blnSakujoFG,
                                                                        cAtenaGetPara1.p_strKobetsuShutokuKB)
                        '*����ԍ� 000038 2008/01/17 �C���I��

                        '�擾����
                        intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
                        '�擾�������O���̏ꍇ�A
                        If (intGetCount = 0) Then

                            'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
                            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                            '�������I������
                            Exit Try
                        End If

                        If (m_blnBatch) Then
                            '�u�����ҏW�o�b�`�v�́u�����ʕҏW�v���\�b�h�����s����
                            csAtenaD = m_cABBatchAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                    strGyomuCD, strGyomunaiSHU_CD)
                        Else
                            '�u�����ҏW�v�́u�����ʕҏW�v���\�b�h�����s����
                            csAtenaD = m_cABAtenaHenshuB.RirekiKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                    strGyomuCD, strGyomunaiSHU_CD)
                        End If
                    Else
                        '�u���������}�X�^�c�`�v�́u���������}�X�^���o�v���\�b�h�����s����
                        csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                                    cSearchKey,
                                                                    cAtenaGetPara1.p_strShiteiYMD,
                                                                    cAtenaGetPara1.p_blnSakujoFG)
                        '�擾����
                        intGetCount = csDataSet.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count
                        '�擾�������O���̏ꍇ�A
                        If (intGetCount = 0) Then

                            'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
                            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                            '�������I������
                            Exit Try
                        End If

                        If (m_blnBatch) Then
                            '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
                            csAtenaD = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                    strGyomuCD, strGyomunaiSHU_CD)
                        Else
                            '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
                            csAtenaD = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                    strGyomuCD, strGyomunaiSHU_CD)
                        End If
                    End If
                Else

                    '�P�w��N�������w�肳��Ă��Ȃ��ꍇ
                    ' �����ʏ��̏ꍇ
                    If (blnKobetsu) Then

                        '*����ԍ� 000038 2008/01/17 �C���J�n
                        '�u�����ʃf�[�^���o�v���]�b�g�����s����
                        'csDataSet = m_cABAtenaB.GetAtenaBKobetsu(cAtenaGetPara1.p_intHyojiKensu, _
                        '                                    cSearchKey, cAtenaGetPara1.p_blnSakujoFG)
                        csDataSet = m_cABAtenaB.GetAtenaBKobetsu(cAtenaGetPara1.p_intHyojiKensu,
                                                                 cSearchKey, cAtenaGetPara1.p_blnSakujoFG,
                                                                 cAtenaGetPara1.p_strKobetsuShutokuKB)
                        '*����ԍ� 000038 2008/01/17 �C���I��

                        '�擾����
                        intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
                        '�擾�������O���̏ꍇ�A
                        If (intGetCount = 0) Then

                            'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
                            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                            '�������I������
                            Exit Try
                        End If

                        If (m_blnBatch) Then
                            '�u�����ҏW�o�b�`�v�́u�����ҏW�v���\�b�h�����s����
                            csAtenaD = m_cABBatchAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                   strGyomuCD, strGyomunaiSHU_CD)
                        Else
                            '�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
                            csAtenaD = m_cABAtenaHenshuB.AtenaKobetsuHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                   strGyomuCD, strGyomunaiSHU_CD)
                        End If

                    Else

                        '�u�����}�X�^���o�v���]�b�g�����s����
                        csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                            cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

                        '�擾����
                        intGetCount = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count
                        '�擾�������O���̏ꍇ�A
                        If (intGetCount = 0) Then

                            'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
                            csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)

                            '�������I������
                            Exit Try
                        End If

                        If (m_blnBatch) Then
                            '�u�����ҏW�o�b�`�v�́u�����ҏW�v���\�b�h�����s����
                            csAtenaD = m_cABBatchAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                   strGyomuCD, strGyomunaiSHU_CD)
                        Else
                            '�u�����ҏW�v�́u�����ҏW�v���\�b�h�����s����
                            csAtenaD = m_cABAtenaHenshuB.AtenaHenshu(cAtenaGetPara1, csDataSet, strDainoKB,
                                                                   strGyomuCD, strGyomunaiSHU_CD)
                        End If
                    End If
                End If


                '=====================================================================================================================
                '== �Q�O�D��[�l���t��f�[�^�̒��o����ݒ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@��[�l�̑��t��f�[�^�̒��o�ɂ����āA���w����̎w�肪����A�������t��f�[�^�敪�� "1" �̏ꍇ��
                '==�@�@�@�@�@�@�@�@�w�肳�ꂽ���t���L�����ԂɊ܂܂�Ă��邱�Ƃ������Ƃ���B
                '==�@�@�@�@�@�@�@�@��L�ȊO�́A�V�X�e�����t���L�����ԂɊ܂܂��Ă��邱�Ƃ������Ƃ���B
                '==�@�@�@�@
                '=====================================================================================================================
                '�w��N�������w�肵�Ă��芎�擾�p�����[�^�̑��t��f�[�^�敪��"1"�̏ꍇ
                If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If


                '=====================================================================================================================
                '== �Q�P�D��[�l���t��f�[�^�̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@��[�l�̑��t��f�[�^�̌����ɂ��A���݂��Ă���ꍇ�̂ݑ��t��f�[�^�̎擾���s���B
                '==�@�@�@�@�@�@�@�@�擾���s��Ȃ������ꍇ�́A��̃e�[�u�����쐬����B
                '==�@�@�@�@
                '=====================================================================================================================
                If (csDataSet.Tables(0).Select(ABAtenaCountEntity.SFSKCOUNT + " IS NOT NULL AND " + ABAtenaCountEntity.SFSKCOUNT + " > 0").Length > 0) Then
                    '���t�悪����̂œǂݍ���
                    If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataSet = m_cABSfskB.GetSfskBHoshu_Hyojun(cSearchKey.p_strJuminCD,
                                                       cAtenaGetPara1.p_strGyomuCD,
                                                       cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                       strKikanYMD,
                                                       cAtenaGetPara1.p_blnSakujoFG)
                    Else
                        csDataSet = m_cABSfskB.GetSfskBHoshu(cSearchKey.p_strJuminCD,
                                                       cAtenaGetPara1.p_strGyomuCD,
                                                       cAtenaGetPara1.p_strGyomunaiSHU_CD,
                                                       strKikanYMD,
                                                       cAtenaGetPara1.p_blnSakujoFG)
                    End If
                Else
                    '���t�悪�����̂ŁA��̃e�[�u���쐬
                    If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                        csDataSet = m_cABSfskB.GetSfskSchemaBHoshu_Hyojun()
                    Else
                        csDataSet = m_cABSfskB.GetSfskSchemaBHoshu()
                    End If
                End If


                '=====================================================================================================================
                '== �Q�Q�D��[���t��f�[�^�̃��C�A�E�g�ҏW
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�ʎ����e�f�̎w�肪����ꍇ�́A���t��f�[�^���ʎ������ڂ��t�����ꂽ���C�A�E�g�ɕҏW����B
                '==�@�@�@�@�@�@�@�@�܂��A�o�b�`�ŁE���A���łɂ��g�p����N���X�𕪂���B
                '==�@�@�@�@
                '=====================================================================================================================
                ' �����ʏ��̏ꍇ
                If (blnKobetsu) Then
                    If (m_blnBatch) Then
                        '�u�����ҏW�o�b�`�v�́u���t��ҏW�v���\�b�h�����s����
                        csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
                    Else
                        '�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
                        csAtenaDS = m_cABAtenaHenshuB.SofusakiKobetsuHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
                    End If
                Else
                    If (m_blnBatch) Then
                        '�u�����ҏW�o�b�`�v�́u���t��ҏW�v���\�b�h�����s����
                        csAtenaDS = m_cABBatchAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
                    Else
                        '�u�����ҏW�v�́u���t��ҏW�v���\�b�h�����s����
                        csAtenaDS = m_cABAtenaHenshuB.SofusakiHenshu(cAtenaGetPara1, csAtenaD, csDataSet)
                    End If
                End If


                '=====================================================================================================================
                '== �Q�R�D�擾�f�[�^�̃}�[�W
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�u�{�l�v�u���t��v�u��[�l�v�u��[���t��v�f�[�^���P�̃f�[�^�Z�b�g�Ƀ}�[�W�������������I�ɏI������B
                '==�@�@�@�@
                '=====================================================================================================================
                'csAtenaH �� csAtenaHS ���}�[�W���āAcaAtena1 �ɃZ�b�g����
                csAtena1 = Me.CreateAtenaDataSet(csAtenaH, csAtenaHS, csAtenaD, csAtenaDS, blnKobetsu, intHyojunKB)



            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
                ' UFAppException���X���[����
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' �G���[�����̂܂܃X���[
                Throw

            Finally

                '=====================================================================================================================
                '== �Q�S�D�q�c�a�ؒf
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�o�b�`�v���O��������Ăяo���ꂽ�ꍇ�ȂǁA����q�c�a�ؒf���s��Ȃ�������s���B
                '==�@�@�@�@
                '=====================================================================================================================
                ' RDB�ؒf
                If m_blnBatchRdb = False Then
                    ' RDB�A�N�Z�X���O�o��
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + Me.GetType.Name + "�z" +
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                            "�y���s���\�b�h��:Disconnect�z")
                    m_cfRdbClass.Disconnect()
                End If


                '*����ԍ� 000031 2007/07/30 �C���J�n
                '=====================================================================================================================
                '== �Q�T�D�ԋp����Z���R�[�h���w�肳�ꂽ�Z���R�[�h�ŏ㏑������
                '==�@�@�@�@
                '==�@�@�@�@<����>�@����l��\�Ҏ擾���ꂽ�ꍇ�́A�w�肳�ꂽ�Z���R�[�h��Ԃ�
                '==�@�@�@�@
                '=====================================================================================================================
                '�ޔ������Z���R�[�h�����݂���ꍇ�́A�㏑������
                SetJuminCD(csAtena1)
                '*����ԍ� 000031 2007/07/30 �C���I��

                '*����ԍ� 000041 2008/11/17 �폜�J�n
                ''*����ԍ� 000040 2008/11/10 �ǉ��J�n
                ''=====================================================================================================================
                ''== �Q�U�D���p�͏o�f�[�^�̍i����
                ''==�@�@�@�@
                ''==�@�@�@�@<����>�@���p�͏o�擾�敪 = "2" �̏ꍇ�A�ԋp�f�[�^�̔[�Ŏ�ID�����݂��Ȃ����R�[�h�͕ԋp���Ȃ�
                ''==�@�@�@�@
                ''=====================================================================================================================
                ''�ޔ������Z���R�[�h�����݂���ꍇ�́A�㏑������
                'RiyoTdkHenshu_Select(cAtenaGetPara1, blnKobetsu, csAtena1)
                ''*����ԍ� 000040 2008/11/10 �ǉ��I��
                '*����ԍ� 000041 2008/11/17 �폜�V���E������

            End Try

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

        Return csAtena1

    End Function
#End Region
    '*����ԍ� 000030 2007/04/21 �ǉ��I��

    '*����ԍ� 000030 2007/04/21 �ǉ��J�n
#Region " ���p�����擾 "
    '************************************************************************************************
    '* ���\�b�h��     ���p�����擾
    '* 
    '* �\��           Public Function GetKaigoAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet : �擾�����������
    '************************************************************************************************
    Public Function GetKaigoAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim blnAtenaKani As Boolean
        'Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        'Dim blnRirekiKani As Boolean
        '* corresponds to VS2008 End 2010/04/16 000044
        Dim csAtenaEntity As DataSet                        '���p����Entity

        Try
            '�R���X�g���N�^�̐ݒ��ۑ�
            blnAtenaSelectAll = m_blnSelectAll
            m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            If Not (Me.m_cABAtenaB Is Nothing) Then
                Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            End If
            If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            End If

            '�����擾���C�����\�b�h�̌ďo���i�����F�擾�p�����[�^�N���X�A�ʎ����f�[�^�擾�t���O�A�Ăяo�����\�b�h�敪�j
            csAtenaEntity = AtenaGetMain(cAtenaGetPara1, False, ABEnumDefine.MethodKB.KB_Kaigo, ABEnumDefine.HyojunKB.KB_Tsujo)

            '�R���X�g���N�^�̐ݒ�����ɂ��ǂ�
            m_blnSelectAll = blnAtenaSelectAll
            If Not (Me.m_cABAtenaB Is Nothing) Then
                Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
            End If
            If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                Me.m_cABAtenaRirekiB.m_blnSelectAll = m_blnSelectAll
            End If

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

        Return csAtenaEntity

    End Function
#End Region
    '*����ԍ� 000030 2007/04/21 �ǉ��I��

#Region " �ȈՈ����擾�Q(AtenaGet2) "
    '************************************************************************************************
    '* ���\�b�h��     �ȈՈ����擾�Q
    '* 
    '* �\��           Public Function AtenaGet2(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Function AtenaGet2(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Const THIS_METHOD_NAME As String = "AtenaGet2"
        Dim csAtenaEntity As DataSet                        '����Entity
        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnAtenaKani As Boolean
        Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnRirekiKani As Boolean
        '* ����ԍ� 000024 2005/01/25 �ǉ��I��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            ' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                                "�y���s���\�b�h��:Connect�z")
            '* ����ԍ� 000023 2004/08/27 �폜�I��
            '�q�c�a�ڑ�
            If m_blnBatchRdb = False Then
                '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                                "�y���s���\�b�h��:Connect�z")
                '* ����ԍ� 000023 2004/08/27 �ǉ��I��
                m_cfRdbClass.Connect()
            End If

            Try
                '* ����ԍ� 000014 2003/06/17 �폜�J�n
                '' �Ǘ����擾(��������)���\�b�h�����s����B
                'Me.GetKanriJoho()
                '* ����ԍ� 000014 2003/06/17 �폜�I��

                '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j�ȈՓǂݍ��݉\�ɂ������ߔN���Ή��i�S�ēǂނ悤�Ɂj
                '�R���X�g���N�^�̐ݒ��ۑ�
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    blnAtenaSelectAll = Me.m_cABAtenaB.m_blnSelectAll
                    blnAtenaKani = Me.m_cABAtenaB.m_blnSelectCount
                    Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = False
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    blnRirekiSelectAll = Me.m_cABAtenaRirekiB.m_blnSelectAll
                    blnRirekiKani = Me.m_cABAtenaRirekiB.m_blnSelectCount
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = False

                End If
                '* ����ԍ� 000024 2005/01/25 �ǉ��I��

                ' �ȈՈ����擾�Q(��������)���\�b�h�����s����B
                csAtenaEntity = Me.GetAtena2(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Tsujo)

                '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
                '�R���X�g���N�^�̐ݒ�����ɂ��ǂ�
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = blnAtenaKani
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani
                End If
                '* ����ԍ� 000024 2005/01/25 �ǉ��I��

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
                ' UFAppException���X���[����
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' �G���[�����̂܂܃X���[
                Throw

            Finally
                '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
                ' RDB�A�N�Z�X���O�o��
                'm_cfLogClass.RdbWrite(m_cfControlData, _
                '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                '                        "�y���s���\�b�h��:Disconnect�z")
                '* ����ԍ� 000023 2004/08/27 �폜�I��
                ' RDB�ؒf
                If m_blnBatchRdb = False Then
                    '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
                    ' RDB�A�N�Z�X���O�o��
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                            "�y���s���\�b�h��:Disconnect�z")
                    '* ����ԍ� 000023 2004/08/27 �ǉ��I��
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtenaEntity

    End Function
#End Region

#Region " �Ǘ����擾(KanriJohoGet) "
    '************************************************************************************************
    '* ���\�b�h��     �Ǘ����擾
    '* 
    '* �\��           Public Function KanriJohoGet()
    '* 
    '* �@�\�@�@    �@�@�Ǘ������擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub KanriJohoGet()
        Const THIS_METHOD_NAME As String = "KanriJohoGet"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If (m_blnKanriJoho) Then
                Exit Sub
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            ' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:Connect�z")
            '* ����ԍ� 000023 2004/08/27 �폜�I��
            ' �q�c�a�ڑ�
            If m_blnBatchRdb = False Then
                '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                            "�y���s���\�b�h��:Connect�z")
                '* ����ԍ� 000023 2004/08/27 �ǉ��I��
                m_cfRdbClass.Connect()
            End If

            Try

                ' �Ǘ����擾(��������)���\�b�h�����s����B
                Me.GetKanriJoho()

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
                ' UFAppException���X���[����
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' �G���[�����̂܂܃X���[
                Throw

            Finally
                '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
                ' RDB�A�N�Z�X���O�o��
                'm_cfLogClass.RdbWrite(m_cfControlData, _
                '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                '                        "�y���s���\�b�h��:Disconnect�z")
                '* ����ԍ� 000023 2004/08/27 �폜�I��
                ' RDB�ؒf
                If m_blnBatchRdb = False Then
                    '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
                    ' RDB�A�N�Z�X���O�o��
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                            "�y���s���\�b�h��:Disconnect�z")
                    '* ����ԍ� 000023 2004/08/27 �ǉ��I��
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

#Region " �N�������擾(NenkinAtenaGet) "
    '*����ԍ� 000029 2006/07/31 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �N�������擾
    '* 
    '* �\��           Public Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* �@�\�@�@       �N�����������擾����
    '* 
    '* ����           cAtenaGetPara1    : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Overloads Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Const THIS_METHOD_NAME As String = "NenkinAtenaGet"
        '* corresponds to VS2008 End 2010/04/16 000044
        '�N�������Q�b�g���N�����������擾����
        Return NenkinAtenaGet(cAtenaGetPara1, ABEnumDefine.NenkinAtenaGetKB.Version01)
    End Function
    '*����ԍ� 000029 2006/07/31 �ǉ��I��
#End Region

#Region " �N�������擾(NenkinAtenaGet) "
    '************************************************************************************************
    '* ���\�b�h��     �N�������擾
    '* 
    '* �\��           Public Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* �@�\�@�@       �N�����������擾����
    '* 
    '* ����           cAtenaGetPara1    : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    '*����ԍ� 000029 2006/07/31 �C���J�n
    Public Overloads Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intNenkinAtenaGetKB As Integer) As DataSet
        'Const THIS_METHOD_NAME As String = "NenkinAtenaGet"
        ''Public Function NenkinAtenaGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        ''    Const THIS_METHOD_NAME As String = "KanriJohoGet"
        ''*����ԍ� 000029 2006/07/31 �C���I��
        ''*����ԍ� 000015 2003/08/21 �폜�J�n
        ''Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '�����ҏW�N���X
        ''*����ԍ� 000015 2003/08/21 �폜�I��
        'Dim csAtenaEntity As DataSet                        '����Entity
        'Dim csAtena1Entity As DataSet                       '����1Entity
        ''*����ԍ� 000022 2003/12/02 �ǉ��J�n
        'Dim cAtenaGetPara1Save As New ABAtenaGetPara1XClass     ' �ޔ�p
        ''*����ԍ� 000022 2003/12/02 �ǉ��I��

        ''* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        'Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        'Dim blnAtenaKani As Boolean
        'Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        'Dim blnRirekiKani As Boolean
        ''* ����ԍ� 000024 2005/01/25 �ǉ��I��

        'Try
        '    ' �f�o�b�O���O�o��
        '    m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        '    '=====================================================================================================================
        '    '== �P�D�q�c�a�ڑ�
        '    '==�@�@�@�@
        '    '==�@�@�@�@<����>�@�o�b�`�v���O��������Ăяo���ꂽ�ꍇ�ȂǁA����q�c�a�ڑ����s��Ȃ�������s���B
        '    '==�@�@�@�@
        '    '=====================================================================================================================
        '    '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        '    ' RDB�A�N�Z�X���O�o��
        '    'm_cfLogClass.RdbWrite(m_cfControlData, _
        '    '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
        '    '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
        '    '                                "�y���s���\�b�h��:Connect�z")
        '    '* ����ԍ� 000023 2004/08/27 �폜�I��
        '    '�q�c�a�ڑ�
        '    If m_blnBatchRdb = False Then
        '        '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
        '        ' RDB�A�N�Z�X���O�o��
        '        m_cfLogClass.RdbWrite(m_cfControlData,
        '                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
        '                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
        '                                        "�y���s���\�b�h��:Connect�z")
        '        '* ����ԍ� 000023 2004/08/27 �ǉ��I��
        '        m_cfRdbClass.Connect()
        '    End If

        '    Try
        '        '=====================================================================================================================
        '        '== �Q�D�e��N���X�̃C���X�^���X��
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�o�b�`�t���O�̏ꍇ�����ɂ��A���A���p�E�o�b�`�p�N���X���C���X�^���X������B
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        '*����ԍ� 000015 2003/08/21 �C���J�n
        '        ''�����ҏW�N���X�̃C���X�^���X�쐬
        '        'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '        If (m_blnBatch) Then
        '            If (m_cABBatchAtenaHenshuB Is Nothing) Then
        '                '�����ҏW�o�b�`�N���X�̃C���X�^���X�쐬
        '                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        '                'm_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '                m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
        '                '* ����ԍ� 000024 2005/01/25 �X�V�I��
        '            End If
        '        Else
        '            If (m_cABAtenaHenshuB Is Nothing) Then
        '                '�����ҏW�N���X�̃C���X�^���X�쐬
        '                '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        '                'm_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
        '                m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
        '                '* ����ԍ� 000024 2005/01/25 �X�V�I��
        '            End If
        '        End If
        '        '*����ԍ� 000015 2003/08/21 �C���I��

        '        '*����ԍ� 000045 2010/05/17 �ǉ��J�n
        '        ' �����a�N���X�e��v���p�e�B���Z�b�g
        '        m_cABAtenaB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
        '        m_cABAtenaB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
        '        '*����ԍ� 000046 2011/05/18 �ǉ��J�n
        '        m_cABAtenaB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
        '        '*����ԍ� 000046 2011/05/18 �ǉ��I��

        '        ' ���������a�N���X�e��v���p�e�B���Z�b�g
        '        m_cABAtenaRirekiB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
        '        m_cABAtenaRirekiB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
        '        '*����ԍ� 000046 2011/05/18 �ǉ��J�n
        '        m_cABAtenaRirekiB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB
        '        '*����ԍ� 000046 2011/05/18 �ǉ��I��
        '        '*����ԍ� 000045 2010/05/17 �ǉ��I��


        '        '=====================================================================================================================
        '        '== �R�D�R���X�g���N�^�̐ݒ��ۑ�
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�ȈՔŁE�ʏ�ł̏���ۑ�����B
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j�ȈՓǂݍ��݉\�ɂ������ߔN���Ή��i�S�ēǂނ悤�Ɂj
        '        '�R���X�g���N�^�̐ݒ��ۑ�
        '        If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
        '            Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
        '        End If
        '        If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
        '            Me.m_cABAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
        '        End If
        '        '* ����ԍ� 000024 2005/01/25 �ǉ��I���i�{��j



        '        '=====================================================================================================================
        '        '== �S�D�Ǘ����̎擾
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�e��Ǘ����̎擾���s���B
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        ' �Ǘ����擾(��������)���\�b�h�����s����B
        '        Me.GetKanriJoho()



        '        '=====================================================================================================================
        '        '== �T�D�Ɩ��R�[�h�̑ޔ�
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�Ɩ��R�[�h�E�Ɩ�����ʃR�[�h��ޔ�����B
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        '*����ԍ� 000022 2003/12/02 �ǉ��J�n
        '        ' �Ɩ��R�[�h�E�Ɩ�����ʃR�[�h��ޔ�����
        '        cAtenaGetPara1Save.p_strGyomuCD = cAtenaGetPara1.p_strGyomuCD
        '        cAtenaGetPara1Save.p_strGyomunaiSHU_CD = cAtenaGetPara1.p_strGyomunaiSHU_CD
        '        cAtenaGetPara1.p_strGyomuCD = String.Empty
        '        cAtenaGetPara1.p_strGyomunaiSHU_CD = String.Empty
        '        '*����ԍ� 000022 2003/12/02 �ǉ��I��



        '        '=====================================================================================================================
        '        '== �U�D�R���X�g���N�^�̐ݒ��ۑ�
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�ȈՔŁE�ʏ�ŁA���ߔŁE����ł̏���ۑ�����B
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j�ȈՓǂݍ��݉\�ɂ������ߔN���Ή��i�S�ēǂނ悤�Ɂj
        '        '�R���X�g���N�^�̐ݒ��ۑ�
        '        If Not (Me.m_cABAtenaB Is Nothing) Then
        '            blnAtenaSelectAll = Me.m_cABAtenaB.m_blnSelectAll
        '            blnAtenaKani = Me.m_cABAtenaB.m_blnSelectCount
        '            Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
        '            Me.m_cABAtenaB.m_blnSelectCount = True
        '        End If
        '        If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
        '            blnRirekiSelectAll = Me.m_cABAtenaRirekiB.m_blnSelectAll
        '            blnRirekiKani = Me.m_cABAtenaRirekiB.m_blnSelectCount
        '            Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
        '            Me.m_cABAtenaRirekiB.m_blnSelectCount = True

        '        End If
        '        '* ����ԍ� 000024 2005/01/25 �ǉ��I��



        '        '=====================================================================================================================
        '        '== �U�D�������̎擾
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�������̎擾���s���B
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        ' �ȈՈ����擾(��������)�Q���\�b�h�����s����B
        '        csAtenaEntity = Me.GetAtena2(cAtenaGetPara1)



        '        '=====================================================================================================================
        '        '== �V�D�R���X�g���N�^�̐ݒ��߂�
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�ȈՔŁE�ʏ�ŁA���ߔŁE����ł̏���߂��B
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        '        '�R���X�g���N�^�̐ݒ�����ɂ��ǂ�
        '        If Not (Me.m_cABAtenaB Is Nothing) Then
        '            Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
        '            Me.m_cABAtenaB.m_blnSelectCount = blnAtenaKani
        '        End If
        '        If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
        '            Me.m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll
        '            Me.m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani
        '        End If
        '        '* ����ԍ� 000024 2005/01/25 �ǉ��I��



        '        '=====================================================================================================================
        '        '== �W�D�������̕ҏW
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�������̕ҏW���s���B
        '        '==�@�@�@�@�@�@�@�@�@. �w��N����������ꍇ�́u���������}�X�^�FABATENARIREKI�v�ɂ��擾����
        '        '==�@�@�@�@�@�@�@�@�A. �w��N�������Ȃ��ꍇ�́u�����}�X�^�FABATENA�v�ɂ��擾����
        '        '==�@�@�@�@�@�@�@�@�B. �o�b�`�ł̎w�肪����ꍇ�̓o�b�`�ł̃N���X�ɂ��擾����
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        '*����ԍ� 000015 2003/08/21 �C���J�n
        '        '' �����ҏW�N���X�̔N�������ҏW���\�b�h�����s����B
        '        'csAtena1Entity = cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '        '*����ԍ� 000016 2003/10/09 �C���J�n
        '        'If (m_blnBatch) Then
        '        '    ' �����ҏW�o�b�`�N���X�̔N�������ҏW���\�b�h�����s����B
        '        '    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '        'Else
        '        '    ' �����ҏW�N���X�̔N�������ҏW���\�b�h�����s����B
        '        '    csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '        'End If
        '        ' �w��N�������w�肳��Ă���ꍇ
        '        If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then
        '            If (m_blnBatch) Then
        '                '*����ԍ� 000029 2006/07/31 �C���J�n
        '                '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
        '                If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
        '                    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
        '                Else
        '                    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
        '                End If
        '                'csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
        '                '*����ԍ� 000029 2006/07/31 �C���I��

        '            Else
        '                '*����ԍ� 000029 2006/07/31 �C���J�n
        '                '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
        '                If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
        '                    csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
        '                Else
        '                    csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
        '                End If
        '                'csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
        '                '*����ԍ� 000029 2006/07/31 �C���I��
        '            End If
        '        Else
        '            If (m_blnBatch) Then
        '                '*����ԍ� 000029 2006/07/31 �C���J�n
        '                ' �����ҏW�o�b�`�N���X�̔N�������ҏW���\�b�h�����s����B
        '                If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
        '                    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '                Else
        '                    csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
        '                End If
        '                'csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '                '*����ԍ� 000029 2006/07/31 �C���I��
        '            Else
        '                '*����ԍ� 000029 2006/07/31 �C���J�n
        '                ' �����ҏW�N���X�̔N�������ҏW���\�b�h�����s����B
        '                If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
        '                    csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '                Else
        '                    csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
        '                End If
        '                'csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
        '                '*����ԍ� 000029 2006/07/31 �C���I��
        '            End If
        '        End If
        '        '*����ԍ� 000016 2003/10/09 �C���I��
        '        '*����ԍ� 000015 2003/08/21 �C���I��



        '        '=====================================================================================================================
        '        '== �X�D�Ɩ��R�[�h�̑ޔ�
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�Ɩ��R�[�h�E�Ɩ�����ʃR�[�h��ޔ�����B
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        '*����ԍ� 000022 2003/12/02 �ǉ��J�n
        '        ' �Ɩ��R�[�h�E�Ɩ�����ʃR�[�h�𕜌�����
        '        cAtenaGetPara1.p_strGyomuCD = cAtenaGetPara1Save.p_strGyomuCD
        '        cAtenaGetPara1.p_strGyomunaiSHU_CD = cAtenaGetPara1Save.p_strGyomunaiSHU_CD




        '        '=====================================================================================================================
        '        '== �P�O�D�A����f�[�^�̎擾
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�A��������擾����B
        '        '==�@�@�@�@�@�@�@�@�@. �Ɩ��R�[�h�����݂��Ȃ��ꍇ�́A�������Ȃ�
        '        '==�@�@�@�@�@�@�@�@�A. �w�肵���Ɩ��R�[�h�E�Ɩ�����ʃR�[�h�������Ɂu�A����}�X�^�FABRENRAKUSAKI�v����擾����
        '        '==�@�@�@�@�@�@�@�@�B. �A.�Ńf�[�^���擾�����ꍇ�A�������ɘA����P�A�A����Q��ԋp����
        '        '==�@�@�@�@�@�@�@�@�C. �N�������Q�b�g�E�ʃQ�b�g�̃��C�A�E�g�̏ꍇ�̂݁u�A����Ɩ��R�[�h�v�ɒ��o�����̋Ɩ��R�[�h���Z�b�g����
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        ' �A����ҏW����
        '        '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
        '        'Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtena1Entity)
        '        Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtena1Entity, csAtenaEntity)
        '        '* ����ԍ� 000024 2005/01/25 �X�V�I��
        '        '*����ԍ� 000022 2003/12/02 �ǉ��I��



        '        '=====================================================================================================================
        '        '== �P�P�D�R���X�g���N�^�̐ݒ��߂�
        '        '==�@�@�@�@
        '        '==�@�@�@�@<����>�@�ȈՔŁE�ʏ�ł̏���߂��B
        '        '==�@�@�@�@
        '        '=====================================================================================================================
        '        '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
        '        '�R���X�g���N�^�̐ݒ�����ɂ��ǂ�
        '        If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
        '            Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
        '        End If
        '        If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
        '            Me.m_cABAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
        '        End If
        '        '* ����ԍ� 000024 2005/01/25 �ǉ��I��

        '    Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
        '        ' ���[�j���O���O�o��
        '        m_cfLogClass.WarningWrite(m_cfControlData,
        '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
        '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
        '                                "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
        '                                "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
        '        ' UFAppException���X���[����
        '        Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        '    Catch
        '        ' �G���[�����̂܂܃X���[
        '        Throw

        '    Finally
        '        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        '        ' RDB�A�N�Z�X���O�o��
        '        'm_cfLogClass.RdbWrite(m_cfControlData, _
        '        '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
        '        '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
        '        '                        "�y���s���\�b�h��:Disconnect�z")
        '        '* ����ԍ� 000023 2004/08/27 �폜�I��
        '        ' RDB�ؒf
        '        If m_blnBatchRdb = False Then
        '            '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
        '            ' RDB�A�N�Z�X���O�o��
        '            m_cfLogClass.RdbWrite(m_cfControlData,
        '                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
        '                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
        '                                    "�y���s���\�b�h��:Disconnect�z")
        '            '* ����ԍ� 000023 2004/08/27 �ǉ��I��
        '            m_cfRdbClass.Disconnect()
        '        End If

        '    End Try

        '    ' �f�o�b�O���O�o��
        '    m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        'Catch objAppExp As UFAppException    ' UFAppException���L���b�`
        '    ' ���[�j���O���O�o��
        '    m_cfLogClass.WarningWrite(m_cfControlData,
        '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
        '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
        '                                "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
        '                                "�y���[�j���O���e:" + objAppExp.Message + "�z")
        '    ' �G���[�����̂܂܃X���[����
        '    Throw objAppExp

        'Catch objExp As Exception
        '    ' �G���[���O�o��
        '    m_cfLogClass.ErrorWrite(m_cfControlData,
        '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
        '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
        '                                "�y�G���[���e:" + objExp.Message + "�z")
        '    ' �V�X�e���G���[���X���[����
        '    Throw objExp

        'End Try

        'Return csAtena1Entity

        Return GetNenkinAtena(cAtenaGetPara1, intNenkinAtenaGetKB, ABEnumDefine.HyojunKB.KB_Tsujo)

    End Function
#End Region

#Region " �N�������擾(GetNenkinAtena) "
    '************************************************************************************************
    '* ���\�b�h��     �N�������擾�i���������j
    '* 
    '* �\��           Private Function GetNenkinAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* �@�\�@�@       �N�����������擾����
    '* 
    '* ����           cAtenaGetPara1    : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Private Function GetNenkinAtena(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intNenkinAtenaGetKB As Integer,
                                    ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        Const THIS_METHOD_NAME As String = "GetNenkinAtena"
        Dim csAtenaEntity As DataSet                        '����Entity
        Dim csAtena1Entity As DataSet                       '����1Entity
        Dim cAtenaGetPara1Save As New ABAtenaGetPara1XClass     ' �ޔ�p
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnAtenaKani As Boolean
        Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnRirekiKani As Boolean
        Dim strKikanYMD As String                           '���ԔN����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            '=====================================================================================================================
            '== �P�D�q�c�a�ڑ�
            '==�@�@�@�@
            '==�@�@�@�@<����>�@�o�b�`�v���O��������Ăяo���ꂽ�ꍇ�ȂǁA����q�c�a�ڑ����s��Ȃ�������s���B
            '==�@�@�@�@
            '=====================================================================================================================
            '�q�c�a�ڑ�
            If m_blnBatchRdb = False Then
                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                                "�y���s���\�b�h��:Connect�z")
                m_cfRdbClass.Connect()
            End If

            Try
                '=====================================================================================================================
                '== �Q�D�e��N���X�̃C���X�^���X��
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�o�b�`�t���O�̏ꍇ�����ɂ��A���A���p�E�o�b�`�p�N���X���C���X�^���X������B
                '==�@�@�@�@
                '=====================================================================================================================
                If (m_blnBatch) Then
                    If (m_cABBatchAtenaHenshuB Is Nothing) Then
                        '�����ҏW�o�b�`�N���X�̃C���X�^���X�쐬
                        m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                    End If
                    m_cABBatchAtenaHenshuB.m_intHyojunKB = intHyojunKB
                Else
                    If (m_cABAtenaHenshuB Is Nothing) Then
                        '�����ҏW�N���X�̃C���X�^���X�쐬
                        m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                    End If
                    m_cABAtenaHenshuB.m_intHyojunKB = intHyojunKB
                End If

                m_cABAtenaB.m_intHyojunKB = intHyojunKB
                m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB

                ' �����a�N���X�e��v���p�e�B���Z�b�g
                m_cABAtenaB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
                m_cABAtenaB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
                m_cABAtenaB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB

                ' ���������a�N���X�e��v���p�e�B���Z�b�g
                m_cABAtenaRirekiB.p_strHonsekiHittoshKB = cAtenaGetPara1.p_strHonsekiHittoshKB
                m_cABAtenaRirekiB.p_strShoriteishiKB = cAtenaGetPara1.p_strShoriTeishiKB
                m_cABAtenaRirekiB.p_strFrnZairyuJohoKB = cAtenaGetPara1.p_strFrnZairyuJohoKB


                '=====================================================================================================================
                '== �R�D�R���X�g���N�^�̐ݒ��ۑ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�ȈՔŁE�ʏ�ł̏���ۑ�����B
                '==�@�@�@�@
                '=====================================================================================================================
                '�R���X�g���N�^�̐ݒ��ۑ�
                If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
                    Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                End If
                If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
                    Me.m_cABAtenaHenshuB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                End If



                '=====================================================================================================================
                '== �S�D�Ǘ����̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�e��Ǘ����̎擾���s���B
                '==�@�@�@�@
                '=====================================================================================================================
                ' �Ǘ����擾(��������)���\�b�h�����s����B
                Me.GetKanriJoho()



                '=====================================================================================================================
                '== �T�D�Ɩ��R�[�h�̑ޔ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�Ɩ��R�[�h�E�Ɩ�����ʃR�[�h��ޔ�����B
                '==�@�@�@�@
                '=====================================================================================================================
                ' �Ɩ��R�[�h�E�Ɩ�����ʃR�[�h��ޔ�����
                cAtenaGetPara1Save.p_strGyomuCD = cAtenaGetPara1.p_strGyomuCD
                cAtenaGetPara1Save.p_strGyomunaiSHU_CD = cAtenaGetPara1.p_strGyomunaiSHU_CD
                cAtenaGetPara1.p_strGyomuCD = String.Empty
                cAtenaGetPara1.p_strGyomunaiSHU_CD = String.Empty



                '=====================================================================================================================
                '== �U�D�R���X�g���N�^�̐ݒ��ۑ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�ȈՔŁE�ʏ�ŁA���ߔŁE����ł̏���ۑ�����B
                '==�@�@�@�@
                '=====================================================================================================================
                '�R���X�g���N�^�̐ݒ��ۑ�
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    blnAtenaSelectAll = Me.m_cABAtenaB.m_blnSelectAll
                    blnAtenaKani = Me.m_cABAtenaB.m_blnSelectCount
                    Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
                    Me.m_cABAtenaB.m_blnSelectCount = True
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    blnRirekiSelectAll = Me.m_cABAtenaRirekiB.m_blnSelectAll
                    blnRirekiKani = Me.m_cABAtenaRirekiB.m_blnSelectCount
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.NenkinAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = True

                End If



                '=====================================================================================================================
                '== �U�D�������̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�������̎擾���s���B
                '==�@�@�@�@
                '=====================================================================================================================
                ' �ȈՈ����擾(��������)�Q���\�b�h�����s����B
                csAtenaEntity = Me.GetAtena2(cAtenaGetPara1, intHyojunKB)



                '=====================================================================================================================
                '== �V�D�R���X�g���N�^�̐ݒ��߂�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�ȈՔŁE�ʏ�ŁA���ߔŁE����ł̏���߂��B
                '==�@�@�@�@
                '=====================================================================================================================
                '�R���X�g���N�^�̐ݒ�����ɂ��ǂ�
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = blnAtenaKani
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani
                End If



                '=====================================================================================================================
                '== �W�D�������̕ҏW
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�������̕ҏW���s���B
                '==�@�@�@�@�@�@�@�@�@. �w��N����������ꍇ�́u���������}�X�^�FABATENARIREKI�v�ɂ��擾����
                '==�@�@�@�@�@�@�@�@�A. �w��N�������Ȃ��ꍇ�́u�����}�X�^�FABATENA�v�ɂ��擾����
                '==�@�@�@�@�@�@�@�@�B. �o�b�`�ł̎w�肪����ꍇ�̓o�b�`�ł̃N���X�ɂ��擾����
                '==�@�@�@�@
                '=====================================================================================================================
                ' �w��N�������w�肳��Ă���ꍇ
                If Not (cAtenaGetPara1.p_strShiteiYMD = "") Then
                    If (m_blnBatch) Then
                        '�u�����ҏW�o�b�`�v�́u����ҏW�v���\�b�h�����s����
                        If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
                        Else
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
                        End If

                    Else
                        '�u�����ҏW�v�́u����ҏW�v���\�b�h�����s����
                        If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu(cAtenaGetPara1, csAtenaEntity)
                        Else
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinRirekiHenshu2(cAtenaGetPara1, csAtenaEntity)
                        End If
                    End If
                Else
                    If (m_blnBatch) Then
                        ' �����ҏW�o�b�`�N���X�̔N�������ҏW���\�b�h�����s����B
                        If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
                        Else
                            csAtena1Entity = m_cABBatchAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
                        End If
                    Else
                        ' �����ҏW�N���X�̔N�������ҏW���\�b�h�����s����B
                        If intNenkinAtenaGetKB = ABEnumDefine.NenkinAtenaGetKB.Version01 Then
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu(cAtenaGetPara1, csAtenaEntity)
                        Else
                            csAtena1Entity = m_cABAtenaHenshuB.NenkinAtenaHenshu2(cAtenaGetPara1, csAtenaEntity)
                        End If
                    End If
                End If



                '=====================================================================================================================
                '== �X�D�Ɩ��R�[�h�̑ޔ�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�Ɩ��R�[�h�E�Ɩ�����ʃR�[�h��ޔ�����B
                '==�@�@�@�@
                '=====================================================================================================================
                ' �Ɩ��R�[�h�E�Ɩ�����ʃR�[�h�𕜌�����
                cAtenaGetPara1.p_strGyomuCD = cAtenaGetPara1Save.p_strGyomuCD
                cAtenaGetPara1.p_strGyomunaiSHU_CD = cAtenaGetPara1Save.p_strGyomunaiSHU_CD




                '=====================================================================================================================
                '== �P�O�D�A����f�[�^�̎擾
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�A��������擾����B
                '==�@�@�@�@�@�@�@�@�@. �Ɩ��R�[�h�����݂��Ȃ��ꍇ�́A�������Ȃ�
                '==�@�@�@�@�@�@�@�@�A. �w�肵���Ɩ��R�[�h�E�Ɩ�����ʃR�[�h�������Ɂu�A����}�X�^�FABRENRAKUSAKI�v����擾����
                '==�@�@�@�@�@�@�@�@�B. �A.�Ńf�[�^���擾�����ꍇ�A�������ɘA����P�A�A����Q��ԋp����
                '==�@�@�@�@�@�@�@�@�C. �N�������Q�b�g�E�ʃQ�b�g�̃��C�A�E�g�̏ꍇ�̂݁u�A����Ɩ��R�[�h�v�ɒ��o�����̋Ɩ��R�[�h���Z�b�g����
                '==�@�@�@�@
                '=====================================================================================================================
                '�w��N�������w�肵�Ă��芎�擾�p�����[�^�̑��t��f�[�^�敪��"1"�̏ꍇ
                If cAtenaGetPara1.p_strShiteiYMD <> "" And cAtenaGetPara1.p_strSfskDataKB = "1" Then
                    strKikanYMD = cAtenaGetPara1.p_strShiteiYMD.RSubstring(0, 8)
                Else
                    strKikanYMD = m_strSystemDateTime
                End If
                ' �A����ҏW����
                Me.RenrakusakiHenshu(cAtenaGetPara1.p_strGyomuCD, cAtenaGetPara1.p_strGyomunaiSHU_CD, csAtena1Entity, csAtenaEntity, intHyojunKB, strKikanYMD)



                '=====================================================================================================================
                '== �P�P�D�R���X�g���N�^�̐ݒ��߂�
                '==�@�@�@�@
                '==�@�@�@�@<����>�@�ȈՔŁE�ʏ�ł̏���߂��B
                '==�@�@�@�@
                '=====================================================================================================================
                '�R���X�g���N�^�̐ݒ�����ɂ��ǂ�
                If Not (Me.m_cABBatchAtenaHenshuB Is Nothing) Then
                    Me.m_cABBatchAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
                End If
                If Not (Me.m_cABAtenaHenshuB Is Nothing) Then
                    Me.m_cABAtenaHenshuB.m_blnSelectAll = Me.m_blnSelectAll
                End If

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
                ' UFAppException���X���[����
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' �G���[�����̂܂܃X���[
                Throw

            Finally
                ' RDB�ؒf
                If m_blnBatchRdb = False Then
                    ' RDB�A�N�Z�X���O�o��
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                            "�y���s���\�b�h��:Disconnect�z")
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtena1Entity

    End Function
#End Region

#Region " ���ۈ��������擾(KokuhoAtenaRirekiGet) "
    '************************************************************************************************
    '* ���\�b�h��     ���ۈ��������擾
    '* 
    '* �\��           Public Function KokuhoAtenaRirekiGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* �@�\�@�@       ���ۈ��������f�[�^���擾����
    '* 
    '* ����           cAtenaGetPara1    : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Function KokuhoAtenaRirekiGet(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Const THIS_METHOD_NAME As String = "KokuhoAtenaRirekiGet"
        '*����ԍ� 000015 2003/08/21 �폜�J�n
        'Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '�����ҏW�N���X
        '*����ԍ� 000015 2003/08/21 �폜�I��
        Dim csAtena1Entity As DataSet                       '����1Entity

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            ' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                                "�y���s���\�b�h��:Connect�z")
            '* ����ԍ� 000023 2004/08/27 �폜�I��
            '�q�c�a�ڑ�
            If m_blnBatchRdb = False Then
                '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                                "�y���s���\�b�h��:Connect�z")
                '* ����ԍ� 000023 2004/08/27 �ǉ��I��
                m_cfRdbClass.Connect()
            End If

            Try
                ' �Ǘ����擾(��������)���\�b�h�����s����B
                Me.GetKanriJoho()

                ' ���ۈ��������擾(��������)���\�b�h�����s����B
                csAtena1Entity = Me.GetKokuhoAtenaRireki(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Tsujo)

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
                ' UFAppException���X���[����
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' �G���[�����̂܂܃X���[
                Throw

            Finally
                '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
                ' RDB�A�N�Z�X���O�o��
                'm_cfLogClass.RdbWrite(m_cfControlData, _
                '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                '                        "�y���s���\�b�h��:Disconnect�z")
                '* ����ԍ� 000023 2004/08/27 �폜�I��
                ' RDB�ؒf
                If m_blnBatchRdb = False Then
                    '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
                    ' RDB�A�N�Z�X���O�o��
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                            "�y���s���\�b�h��:Disconnect�z")
                    '* ����ԍ� 000023 2004/08/27 �ǉ��I��
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtena1Entity

    End Function
#End Region

#Region " �ȈՈ����擾�Q(GetAtena2) "
    '************************************************************************************************
    '* ���\�b�h��     �ȈՈ����擾�Q�i���������j
    '* 
    '* �\��           Private Function GetAtena2(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Private Function GetAtena2(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtena2"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim cSearchKey As ABAtenaSearchKey                  '���������L�[
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim csDataTable As DataTable
        '* corresponds to VS2008 End 2010/04/16 000044
        Dim csDataSet As DataSet
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '���������}�X�^�c�`�N���X
        'Dim cABAtenaB As ABAtenaBClass                      '�����}�X�^�c�`�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        '*����ԍ� 000015 2003/08/21 �폜�J�n
        'Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '�����ҏW�N���X
        '*����ԍ� 000015 2003/08/21 �폜�I��
        Dim intHyojiKensu As Integer                        '�ő�擾����
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim intGetCount As Integer                          '�擾����
        '* corresponds to VS2008 End 2010/04/16 000044
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cUSSCityInfoClass As New USSCityInfoClass()     '�s�������Ǘ��N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        Dim strShichosonCD As String                        '�s�����R�[�h
        '* ����ԍ� 000039 2008/02/17 �ǉ��J�n
        Dim intIdx As Integer
        Dim cABMojiHenshuB As ABMojiretsuHenshuBClass       '�����ҏW�a�N���X
        '* ����ԍ� 000039 2008/02/17 �ǉ��I��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


            '=====================================================================================================================
            '== �P�D�����擾�p�����[�^�`�F�b�N
            '==�@�@�@�@
            '==�@�@�@�@<����>�@�p�����[�^�N���X�Ɏw�肳�ꂽ���e���`�F�b�N����B
            '==�@�@�@�@
            '=====================================================================================================================
            ' �p�����[�^�`�F�b�N
            Me.CheckColumnValue(cAtenaGetPara1, intHyojunKB)


            '=====================================================================================================================
            '== �Q�D�Ɩ��R�[�h���݃`�F�b�N
            '==�@�@�@�@
            '==�@�@�@�@<����>�@�Ɩ��R�[�h�������L�[�ɂ��Ă���Ă����ꍇ�́A�G���[��Ԃ��B
            '==�@�@�@�@
            '=====================================================================================================================
            ' �Ɩ��R�[�h���w�肳��Ă���ꍇ�́A�G���[
            If Not (cAtenaGetPara1.p_strGyomuCD = String.Empty) Then
                ' �G���[��`���擾
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002002)
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Ɩ��R�[�h", objErrorStruct.m_strErrorCode)
            End If

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            ' ���������}�X�^�c�`�N���X�̃C���X�^���X�쐬
            'cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' �����}�X�^�c�`�N���X�̃C���X�^���X�쐬
            'cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            '* ����ԍ� 000023 2004/08/27 �폜�I��


            '*����ԍ� 000015 2003/08/21 �C���J�n
            '' �����ҏW�N���X�̃C���X�^���X�쐬
            'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            '*����ԍ� 000015 2003/08/21 �C���I��

            ' ���ߎs�������擾���擾����B
            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            'cUSSCityInfoClass.GetCityInfo(m_cfControlData)
            '* ����ԍ� 000023 2004/08/27 �폜�I��


            '=====================================================================================================================
            '== �R�D�s�����R�[�h�̎擾
            '==�@�@�@�@
            '==�@�@�@�@<����>�@���߂̎s�����R�[�h���擾����B
            '==�@�@�@�@
            '=====================================================================================================================
            ' �s�����R�[�h�̓��e��ݒ肷��B
            If (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
                strShichosonCD = m_cUSSCityInfoClass.p_strShichosonCD(0)
            Else
                strShichosonCD = cAtenaGetPara1.p_strShichosonCD
            End If



            '*����ԍ� 000031 2007/07/31 �ǉ��J�n
            '=====================================================================================================================
            '== �S�D����l��\�Ҏ擾����
            '==�@�@�@�@
            '==�@�@�@�@<����>�@�Z���R�[�h�E�Z�o�O�D��E����l����FG�L���̌��������̏ꍇ�̂݁A����l��\�Ҏ擾���s���B
            '==�@�@�@�@�@�@�@�@�Ǘ����ɂ��A���[�U���Ƃ̎擾����L��B
            '==�@�@�@�@
            '=====================================================================================================================
            '����l��\�ҏZ���R�[�h�������p�����[�^�ɏ㏑������
            GetDaihyoJuminCD(cAtenaGetPara1)
            '*����ԍ� 000031 2007/07/31 �ǉ��I��



            '=====================================================================================================================
            '== �T�D�{�l�����擾�����L�[�̐ݒ�
            '==�@�@�@�@
            '==�@�@�@�@<����>�@�{�l�̈��������擾���邽�߂̌����L�[���w�肳�ꂽ�p�����[�^�N���X���ݒ肷��B
            '==�@�@�@�@�@�@�@�@�ő�擾�������擾����B
            '==�@�@�@�@
            '=====================================================================================================================
            ' ���������L�[�̃C���X�^���X��
            cSearchKey = New ABAtenaSearchKey

            ' �����擾�p�����[�^���父�������L�[�ɃZ�b�g����
            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD
            cSearchKey.p_strStaiCD = cAtenaGetPara1.p_strStaiCD
            cSearchKey.p_strSearchKanaSeiMei = cAtenaGetPara1.p_strKanaSeiMei
            cSearchKey.p_strSearchKanaSei = cAtenaGetPara1.p_strKanaSei
            cSearchKey.p_strSearchKanaMei = cAtenaGetPara1.p_strKanaMei
            cSearchKey.p_strSearchKanjiMeisho = cAtenaGetPara1.p_strKanjiShimei
            cSearchKey.p_strUmareYMD = cAtenaGetPara1.p_strUmareYMD
            cSearchKey.p_strSeibetsuCD = cAtenaGetPara1.p_strSeibetsu
            cSearchKey.p_strDataKB = cAtenaGetPara1.p_strDataKB
            cSearchKey.p_strJuminShubetu1 = cAtenaGetPara1.p_strJuminSHU1
            cSearchKey.p_strJuminShubetu2 = cAtenaGetPara1.p_strJuminSHU2
            cSearchKey.p_strShichosonCD = strShichosonCD
            '*����ԍ� 000032 2007/09/04 �ǉ��J�n
            '�����p�J�i�����E�����p�J�i���E�����p�J�i���̕ҏW
            cSearchKey = HenshuSearchKana(cSearchKey, cAtenaGetPara1.p_blnGaikokuHommyoYusen)
            '*����ԍ� 000032 2007/09/04 �ǉ��I��

            ' �Z���`�Ԓn�R�[�h3�̃Z�b�g
            If Not (cAtenaGetPara1.p_strJukiJutogaiKB = "1") Then
                ' �Z�o�O�D��̏ꍇ
                cSearchKey.p_strJutogaiYusenKB = "1"
                cSearchKey.p_strJushoCD = cAtenaGetPara1.p_strJushoCD
                cSearchKey.p_strGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9)
                cSearchKey.p_strChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8)
                cSearchKey.p_strChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8)
                cSearchKey.p_strChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8)
                cSearchKey.p_strBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5)
                cSearchKey.p_strBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5)
                cSearchKey.p_strBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5)
            Else
                ' �Z��D��̏ꍇ
                cSearchKey.p_strJuminYuseniKB = "1"
                '*����ԍ� 000018 2003/10/30 �C���J�n
                'cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.PadLeft(11)
                cSearchKey.p_strJukiJushoCD = cAtenaGetPara1.p_strJushoCD.Trim.RPadLeft(8)
                '*����ԍ� 000018 2003/10/30 �C���I��
                cSearchKey.p_strJukiGyoseikuCD = cAtenaGetPara1.p_strGyoseikuCD.RPadLeft(9)
                cSearchKey.p_strJukiChikuCD1 = cAtenaGetPara1.p_strChikuCD1.RPadLeft(8)
                cSearchKey.p_strJukiChikuCD2 = cAtenaGetPara1.p_strChikuCD2.RPadLeft(8)
                cSearchKey.p_strJukiChikuCD3 = cAtenaGetPara1.p_strChikuCD3.RPadLeft(8)
                cSearchKey.p_strJukiBanchiCD1 = cAtenaGetPara1.p_strBanchiCD1.RPadLeft(5)
                cSearchKey.p_strJukiBanchiCD2 = cAtenaGetPara1.p_strBanchiCD2.RPadLeft(5)
                cSearchKey.p_strJukiBanchiCD3 = cAtenaGetPara1.p_strBanchiCD3.RPadLeft(5)
            End If
            '*����ԍ� 000048 2014/04/28 �ǉ��J�n
            cSearchKey.p_strMyNumber = cAtenaGetPara1.p_strMyNumber.RPadRight(13)
            cSearchKey.p_strMyNumberKojinHojinKB = cAtenaGetPara1.p_strMyNumberKojinHojinKB
            cSearchKey.p_strMyNumberChokkinSearchKB = cAtenaGetPara1.p_strMyNumberChokkinSearchKB
            '*����ԍ� 000048 2014/04/28 �ǉ��I��
            ' �ő�擾�������Z�b�g����
            If cAtenaGetPara1.p_intHyojiKensu = 0 Then
                intHyojiKensu = 100
            Else
                intHyojiKensu = cAtenaGetPara1.p_intHyojiKensu
            End If
            '*����ԍ� 000047 2011/11/07 �ǉ��J�n
            m_cABAtenaB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
            m_cABAtenaRirekiB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
            '*����ԍ� 000047 2011/11/07 �ǉ��I��
            '*����ԍ� 000048 2014/04/28 �ǉ��J�n
            m_cABAtenaB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
            m_cABAtenaRirekiB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
            '*����ԍ� 000048 2014/04/28 �ǉ��I��

            '*����ԍ� 000050 2020/01/31 �ǉ��J�n
            ' ���������t���O
            cSearchKey.p_blnIsRirekiSearch = cAtenaGetPara1.p_blnIsRirekiSearch
            '*����ԍ� 000050 2020/01/31 �ǉ��I��
            cSearchKey.p_strKyuuji = cAtenaGetPara1.p_strKyuuji
            cSearchKey.p_strKanaKyuuji = cAtenaGetPara1.p_strKanaKyuuji
            cSearchKey.p_strKatakanaHeikimei = cAtenaGetPara1.p_strKatakanaHeikimei
            cSearchKey.p_strJusho = cAtenaGetPara1.p_strJusho
            cSearchKey.p_strKatagaki = cAtenaGetPara1.p_strKatagaki
            cSearchKey.p_strRenrakusaki = cAtenaGetPara1.p_strRenrakusaki

            m_cABAtenaB.m_intHyojunKB = intHyojunKB
            m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB

            '=====================================================================================================================
            '== �U�D�{�l�����f�[�^�̎擾
            '==�@�@�@�@
            '==�@�@�@�@<����>�@�{�l�̈��������擾����B
            '==�@�@�@�@�@�@�@�@�@. �w��N����������ꍇ�́u���������}�X�^�FABATENARIREKI�v�ɂ��擾����
            '==�@�@�@�@�@�@�@�@�A. �w��N�������Ȃ��ꍇ�́u�����}�X�^�FABATENA�v�ɂ��擾����
            '==�@�@�@�@
            '=====================================================================================================================
            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then
                ' �w��N�������w�肳��Ă���ꍇ
                '�u���������}�X�^���o�v���]�b�g�����s����
                csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                            cSearchKey,
                                                            cAtenaGetPara1.p_strShiteiYMD,
                                                            cAtenaGetPara1.p_blnSakujoFG)

            Else
                ' �w��N�������w�肳��Ă��Ȃ��ꍇ
                '�u�����}�X�^���o�v���]�b�g�����s����
                csDataSet = m_cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                     cSearchKey, cAtenaGetPara1.p_blnSakujoFG)
            End If

            '* ����ԍ� 000024 2005/01/25 �ǉ��I��
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
            ' UFAppException���X���[����
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

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

            '*����ԍ� 000031 2007/07/31 �ǉ��J�n
        Finally
            '=====================================================================================================================
            '== �Q�S�D�ԋp����Z���R�[�h���w�肳�ꂽ�Z���R�[�h�ŏ㏑������
            '==�@�@�@�@
            '==�@�@�@�@<����>�@����l��\�Ҏ擾���ꂽ�ꍇ�́A�w�肳�ꂽ�Z���R�[�h��Ԃ�
            '==�@�@�@�@
            '=====================================================================================================================
            '�ޔ������Z���R�[�h�����݂���ꍇ�́A�㏑������
            SetJuminCD(csDataSet)
            '*����ԍ� 000031 2007/07/31 �ǉ��I��

            '*����ԍ� 000039 2008/02/17 �ǉ��J�n
            '=====================================================================================================================
            '== �W�D�O���l�̊��������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��
            '==�@�@�@�@
            '==�@�@�@�@<����>�@�O���l�f�[�^:���������P�A�Q�A�܂��͊������ю喼(�]�o�m��A�]�o�\��A�]���O�܂�)�̊��ʂŊ���ꂽ������̏������s��
            '==�@�@�@�@�@�@�@�@
            '=====================================================================================================================
            '*����ԍ� 000043 2009/04/08 �C���J�n
            If Not (csDataSet Is Nothing) Then
                If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
                    '���������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��

                    cABMojiHenshuB = New ABMojiretsuHenshuBClass(m_cfControlData, m_cfConfigDataClass)

                    ' �S�擾�f�[�^���s��
                    '* �����}�X�^�A���������}�X�^�Ƃ��ɓ������C�A�E�g�̂��߁A�e�[�u���w��F"0"�A���ږ��͈���Entity���g�p�B
                    For intIdx = 0 To csDataSet.Tables(0).Rows.Count - 1
                        ' �������̂P
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)),
                                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)),
                                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1)))
                        ' �������̂Q
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)),
                                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)),
                                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2)))
                        ' ���ю喼
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI)))
                        ' ��Q���ю喼
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI)))
                        ' �����@�l��\�Җ�
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)),
                                                                                                                                   CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)),
                                                                                                                                   CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)))
                        ' �]���O���ю喼
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI)))
                        ' �]�o�\�萢�ю喼
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)))
                        ' �]�o�m�萢�ю喼
                        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)))
                    Next
                Else
                    ' ���������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
                End If
            End If

            'If (cAtenaGetPara1.p_strFrnMeishoHenshuKB <> "1") Then
            '    '���������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��

            '    cABMojiHenshuB = New ABMojiretsuHenshuBClass(m_cfControlData, m_cfConfigDataClass)

            '    ' �S�擾�f�[�^���s��
            '    '* �����}�X�^�A���������}�X�^�Ƃ��ɓ������C�A�E�g�̂��߁A�e�[�u���w��F"0"�A���ږ��͈���Entity���g�p�B
            '    For intIdx = 0 To csDataSet.Tables(0).Rows.Count - 1
            '        ' �������̂P
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)), _
            '                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)), _
            '                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO1)))
            '        ' �������̂Q
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)), _
            '                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)), _
            '                                                                                                         CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIMEISHO2)))
            '        ' ���ю喼
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.STAINUSMEI)))
            '        ' ��Q���ю喼
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.DAI2STAINUSMEI)))
            '        ' �����@�l��\�Җ�
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATAKB)), _
            '                                                                                                                   CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.ATENADATASHU)), _
            '                                                                                                                   CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI)))
            '        ' �]���O���ю喼
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENUMAEJ_STAINUSMEI)))
            '        ' �]�o�\�萢�ю喼
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUYOTEISTAINUSMEI)))
            '        ' �]�o�m�萢�ю喼
            '        csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI) = cABMojiHenshuB.EditKanryakuMeisho(CStr(csDataSet.Tables(0).Rows(intIdx)(ABAtenaEntity.TENSHUTSUKKTISTAINUSMEI)))
            '    Next
            'Else
            '    ' ���������Ɋ܂܂�銇�ʂŊ���ꂽ������̏������s��Ȃ�
            'End If
            ''*����ԍ� 000039 2008/02/17 �ǉ��I��
            '*����ԍ� 000043 2009/04/08 �C���I��

        End Try

        Return csDataSet

    End Function
#End Region

#Region " ���ۈ��������擾(GetKokuhoAtenaRireki) "
    '************************************************************************************************
    '* ���\�b�h��     ���ۈ��������擾�i���������j
    '* 
    '* �\��           Private Function GetKokuhoAtenaRireki(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@�@�擾�p�����[�^��舶�������f�[�^��Ԃ��B
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Private Function GetKokuhoAtenaRireki(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        Const THIS_METHOD_NAME As String = "GetKokuhoAtenaRireki"
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000044
        Dim cSearchKey As ABAtenaSearchKey                  '���������L�[
        Dim csDataSet As DataSet
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cABAtenaRirekiB As ABAtenaRirekiBClass          '���������}�X�^�c�`�N���X
        'Dim cABAtenaB As ABAtenaBClass                      '�����}�X�^�c�`�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        '*����ԍ� 000015 2003/08/21 �폜�J�n
        'Dim cABAtenaHenshuB As ABAtenaHenshuBClass          '�����ҏW�N���X
        '*����ԍ� 000015 2003/08/21 �폜�I��
        Dim csAtena1Entity As DataSet                       '����1Entity
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim strShiteiYMD As String                          ' �w���
        '* corresponds to VS2008 End 2010/04/16 000044

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            ' ���������}�X�^�c�`�N���X�̃C���X�^���X�쐬
            'cABAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            '
            ' �����}�X�^�c�`�N���X�̃C���X�^���X�쐬
            'cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '*����ԍ� 000015 2003/08/21 �C���J�n
            '' �����ҏW�N���X�̃C���X�^���X�쐬
            'cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            If (m_blnBatch) Then
                If (m_cABBatchAtenaHenshuB Is Nothing) Then
                    '�����ҏW�o�b�`�N���X�̃C���X�^���X�쐬
                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                    'm_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    m_cABBatchAtenaHenshuB = New ABBatchAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                End If
                m_cABBatchAtenaHenshuB.m_intHyojunKB = intHyojunKB
            Else
                If (m_cABAtenaHenshuB Is Nothing) Then
                    '�����ҏW�N���X�̃C���X�^���X�쐬
                    '* ����ԍ� 000024 2005/01/25 �X�V�J�n�i�{��j
                    'm_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    m_cABAtenaHenshuB = New ABAtenaHenshuBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass, m_blnSelectAll)
                    '* ����ԍ� 000024 2005/01/25 �X�V�I��
                End If
                m_cABAtenaHenshuB.m_intHyojunKB = intHyojunKB
            End If
            '*����ԍ� 000015 2003/08/21 �C���I��

            ' �@�p�����[�^�`�F�b�N
            Me.CheckColumnValue(cAtenaGetPara1, intHyojunKB)

            ' ���������L�[�̃C���X�^���X��
            cSearchKey = New ABAtenaSearchKey

            ' �B�����擾�p�����[�^���父�������L�[�ɃZ�b�g����
            cSearchKey.p_strJuminCD = cAtenaGetPara1.p_strJuminCD

            '*����ԍ� 000016 2003/09/08 �C���J�n
            ''�u�����}�X�^���o�v���]�b�g�����s����
            'csDataSet = cABAtenaB.GetAtenaBHoshu(cAtenaGetPara1.p_intHyojiKensu, _
            '                                     cSearchKey, cAtenaGetPara1.p_blnSakujoFG)

            '' �擾�������P���łȂ��ꍇ�A�G���[
            'If Not (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count = 1) Then
            '    '�G���[��`���擾(�����L�[�̌��ł��B�F�Z���R�[�h)
            '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            '    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            '    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
            'End If

            '' ���уR�[�h��Null�ꍇ�A�G���[
            'If (CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.STAICD), String).Trim = String.Empty) Then
            '    '�G���[��`���擾(�����L�[�̌��ł��B�F�Z���R�[�h)
            '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
            '    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
            '    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
            'End If

            '' ���������L�[�̃C���X�^���X��
            'cSearchKey = New ABAtenaSearchKey()

            '' �C	ABAtenaSearchKey�ɐ��уR�[�h���Z�b�g
            'cSearchKey.p_strStaiCD = CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.STAICD), String)

            'If (CType(csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)(ABAtenaEntity.JUMINJUTOGAIKB), String) = "1") Then
            '    ' �Z��E�Z�o�O�敪���h1�h�̎��A�h1�h���Z���D��敪�ɃZ�b�g
            '    cSearchKey.p_strJuminYuseniKB = "1"
            'Else
            '    ' �Z��E�Z�o�O�敪��<>�h1�h�̎��A�h1�h���Z�o�O�D��敪�ɃZ�b�g
            '    cSearchKey.p_strJutogaiYusenKB = "1"
            'End If

            ' �Z��E�Z�o�O�敪��<>�h1�h�̎��A�h1�h���Z�o�O�D��敪�ɃZ�b�g
            If (cAtenaGetPara1.p_strJukiJutogaiKB <> "1") Then
                cSearchKey.p_strJutogaiYusenKB = "1"
            Else
                cSearchKey.p_strJuminYuseniKB = "1"
            End If
            '*����ԍ� 000016 2003/09/08 �C���I��
            '*����ԍ� 000047 2011/11/07 �ǉ��J�n
            m_cABAtenaRirekiB.p_strJukihoKaiseiKB = cAtenaGetPara1.p_strJukiHokaiseiKB
            '*����ԍ� 000047 2011/11/07 �ǉ��I��
            '*����ԍ� 000048 2014/04/28 �ǉ��J�n
            m_cABAtenaRirekiB.p_strMyNumberKB = cAtenaGetPara1.p_strMyNumberKB
            '*����ԍ� 000048 2014/04/28 �ǉ��I��
            m_cABAtenaRirekiB.m_intHyojunKB = intHyojunKB

            ' �D	���������}�X�^�c�`�v�N���X�́u���������}�X�^���o�v���\�b�h�����s����
            csDataSet = m_cABAtenaRirekiB.GetAtenaRBHoshu(cAtenaGetPara1.p_intHyojiKensu,
                                                        cSearchKey, cAtenaGetPara1.p_strShiteiYMD)

            '*����ԍ� 000015 2003/08/21 �C���J�n
            '' �u�����ҏW�v�N���X�́u����ҏW�v���\�b�h�����s����B
            'csAtena1Entity = cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)

            If (m_blnBatch) Then
                ' �u�����ҏW�v�N���X�́u����ҏW�v���\�b�h�����s����B
                csAtena1Entity = m_cABBatchAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
            Else
                ' �u�����ҏW�v�N���X�́u����ҏW�v���\�b�h�����s����B
                csAtena1Entity = m_cABAtenaHenshuB.RirekiHenshu(cAtenaGetPara1, csDataSet)
            End If
            '*����ԍ� 000015 2003/08/21 �C���I��

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
            ' UFAppException���X���[����
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

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

        Return csAtena1Entity

    End Function
#End Region

#Region " �Ǘ����擾(GetKanriJoho) "
    '************************************************************************************************
    '* ���\�b�h��     �Ǘ����擾�i���������j
    '* 
    '* �\��           Private Function GetKanriJoho()
    '* 
    '* �@�\�@�@    �@�@�Ǘ������擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    '* ����ԍ� 000015 2003/08/21 �C���J�n
    'Private Sub GetKanriJoho()
    <SecuritySafeCritical>
    Protected Overridable Sub GetKanriJoho()
        '* ����ԍ� 000015 2003/08/21 �C���I��
        Const THIS_METHOD_NAME As String = "GetKanriJoho"
        '* ����ԍ� 000015 2003/08/21 �폜�J�n
        'Dim cfURAtenaKanriJoho As URAtenaKanriJohoBClass    '�����Ǘ����a�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cfURAtenaKanriJoho As URAtenaKanriJohoCacheBClass   '�����Ǘ����L���b�V���a�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        '* ����ԍ� 000015 2003/08/21 �폜�I��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            If (m_blnKanriJoho) Then
                Exit Sub
            End If
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��

            '* ����ԍ� 000015 2003/08/21 �C���J�n
            '�Ǘ����N���X�̃C���X�^���X�쐬
            'cfURAtenaKanriJoho = New URAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            ' �����Ǘ����L���b�V���a�N���X�̃C���X�^���X�쐬
            '* ����ԍ� 000023 2004/08/27 �X�V�J�n�i�{��j
            'cfURAtenaKanriJoho = New URAtenaKanriJohoCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            If (m_cfURAtenaKanriJoho Is Nothing) Then
                m_cfURAtenaKanriJoho = New URAtenaKanriJohoCacheBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            End If
            '* ����ԍ� 000023 2004/08/27 �X�V�I��
            '* ����ԍ� 000015 2003/08/21 �C���I��

            m_intHyojiketaJuminCD = m_cfURAtenaKanriJoho.p_intHyojiketaJuminCD                '�Z���R�[�h�\������
            m_intHyojiketaStaiCD = m_cfURAtenaKanriJoho.p_intHyojiketaSetaiCD                 '���уR�[�h�\������
            m_intHyojiketaJushoCD = m_cfURAtenaKanriJoho.p_intHyojiketaJushoCD                '�Z���R�[�h�\�������i�Ǔ��̂݁j
            m_intHyojiketaGyoseikuCD = m_cfURAtenaKanriJoho.p_intHyojiketaGyoseikuCD          '�s����R�[�h�\������
            m_intHyojiketaChikuCD1 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD1              '�n��R�[�h�P�\������
            m_intHyojiketaChikuCD2 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD2              '�n��R�[�h�Q�\������
            m_intHyojiketaChikuCD3 = m_cfURAtenaKanriJoho.p_intHyojiketaChikuCD3              '�n��R�[�h�R�\������
            m_strChikuCD1HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD1HyojiMeisho          '�n��R�[�h�P�\������
            m_strChikuCD2HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD2HyojiMeisho          '�n��R�[�h�Q�\������
            m_strChikuCD3HyojiMeisho = m_cfURAtenaKanriJoho.p_strChikuCD3HyojiMeisho          '�n��R�[�h�R�\������
            m_strRenrakusaki1HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki1HyojiMeisho  '�A����P�\������
            m_strRenrakusaki2HyojiMeisho = m_cfURAtenaKanriJoho.p_strRenrakusaki2HyojiMeisho  '�A����Q�\������

            '* ����ԍ� 000014 2003/06/17 �ǉ��J�n
            ' �Ǘ����擾�ς݃t���O�ݒ�
            m_blnKanriJoho = True
            '* ����ԍ� 000014 2003/06/17 �ǉ��I��

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

#Region " �p�����[�^�[�`�F�b�N(CheckColumnValue) "
    '************************************************************************************************
    '* ���\�b�h��     �p�����[�^�[�`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal cAtenaGetPara1 As ABAtenaGetPara1)
    '* 
    '* �@�\�@�@    �@�@�����擾�p�����[�^�̃`�F�b�N���s�Ȃ�
    '* 
    '* ����           cAtenaGetPara1 As ABAtenaGetPara1 : �����擾�p�����[�^
    '*                intHyojunKB                       : �W�����敪
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intHyojunKB As ABEnumDefine.HyojunKB)

        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim m_cfDateClass As UFDateClass                    ' ���t�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���t�N���X�̃C���X�^���X��
            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            'm_cfDateClass = New UFDateClass(m_cfConfigDataClass)
            '* ����ԍ� 000023 2004/08/27 �폜�I��
            '�K�v�Ȑݒ���s��
            m_cfDateClass.p_enDateSeparator = UFDateSeparator.None


            '�Z��E�Z�o�O�敪
            If Not (cAtenaGetPara1.p_strJukiJutogaiKB.Trim = String.Empty) Then
                If (Not (cAtenaGetPara1.p_strJukiJutogaiKB = "1")) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z��E�Z�o�O�敪", objErrorStruct.m_strErrorCode)
                End If
            End If


            '���t��f�[�^�敪
            If Not (cAtenaGetPara1.p_strSfskDataKB = String.Empty) Then
                If (Not (cAtenaGetPara1.p_strSfskDataKB = "1")) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "���t��f�[�^�敪", objErrorStruct.m_strErrorCode)
                End If
            End If

            '���ш��ҏW
            If Not (cAtenaGetPara1.p_strStaiinHenshu = String.Empty) Then
                If (Not (cAtenaGetPara1.p_strStaiinHenshu = "1")) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "���ш��ҏW", objErrorStruct.m_strErrorCode)
                End If
            End If


            '�Z���R�[�h
            If Not (cAtenaGetPara1.p_strJuminCD.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strJuminCD.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
                End If
            End If

            '���уR�[�h
            If Not (cAtenaGetPara1.p_strStaiCD.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strStaiCD.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "���уR�[�h", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�J�i����
            If Not (cAtenaGetPara1.p_strKanaSeiMei = String.Empty) Then
                '*����ԍ� 000019 2003/10/30 �C���J�n
                'If (Not UFStringClass.CheckKataKana(cAtenaGetPara1.p_strKanaSeiMei.TrimEnd("%"c))) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaSeiMei.TrimEnd("%"c))) Then
                    '*����ԍ� 000019 2003/10/30 �C���I��

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�J�i����", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�J�i��
            If Not (cAtenaGetPara1.p_strKanaSei = String.Empty) Then
                '*����ԍ� 000019 2003/10/30 �C���J�n
                'If (Not UFStringClass.CheckKataKana(cAtenaGetPara1.p_strKanaSei.TrimEnd("%"c))) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaSei.TrimEnd("%"c))) Then
                    '*����ԍ� 000019 2003/10/30 �C���I��

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�J�i��", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�J�i��
            If Not (cAtenaGetPara1.p_strKanaMei = String.Empty) Then
                '*����ԍ� 000019 2003/10/30 �C���J�n
                'If (Not UFStringClass.CheckKataKana(cAtenaGetPara1.p_strKanaMei.TrimEnd("%"c))) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaMei.TrimEnd("%"c))) Then
                    '*����ԍ� 000019 2003/10/30 �C���I��

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�J�i��", objErrorStruct.m_strErrorCode)
                End If
            End If

            '��������
            If Not (cAtenaGetPara1.p_strKanjiShimei = String.Empty) Then
                '* ����ԍ� 000025 2005/04/04 �C���J�n
                'If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKanjiShimei.TrimEnd("%"c), m_cfConfigDataClass)) Then
                If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKanjiShimei.Replace("%"c, String.Empty), m_cfConfigDataClass)) Then
                    '* ����ԍ� 000025 2005/04/04 �C���I��

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                End If
            End If

            '���N����
            If Not (cAtenaGetPara1.p_strUmareYMD = String.Empty Or cAtenaGetPara1.p_strUmareYMD = "00000000") Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strUmareYMD.TrimEnd("%"c))) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "���N����", objErrorStruct.m_strErrorCode)
                End If
            End If

            '���ʃR�[�h
            If Not (cAtenaGetPara1.p_strSeibetsu = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strSeibetsu)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "���ʃR�[�h", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�Z���R�[�h
            If Not (cAtenaGetPara1.p_strJushoCD.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strJushoCD.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�s����R�[�h
            If Not (cAtenaGetPara1.p_strGyoseikuCD.Trim = String.Empty) Then
                '*����ԍ� 000028 2005/12/06 �C���J�n
                ''If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strGyoseikuCD.Trim)) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strGyoseikuCD.Trim)) Then
                    '*����ԍ� 000028 2005/12/06 �C���I��

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�s����R�[�h", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�n��R�[�h�P
            If Not (cAtenaGetPara1.p_strChikuCD1.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strChikuCD1.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�n��R�[�h�P", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�n��R�[�h�Q
            If Not (cAtenaGetPara1.p_strChikuCD2.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strChikuCD2.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�n��R�[�h�Q", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�n��R�[�h�R
            If Not (cAtenaGetPara1.p_strChikuCD3.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strChikuCD3)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�n��R�[�h�R", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�Ԓn�R�[�h�P
            If Not (cAtenaGetPara1.p_strBanchiCD1.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strBanchiCD1.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Ԓn�R�[�h�P", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�Ԓn�R�[�h�Q
            If Not (cAtenaGetPara1.p_strBanchiCD2.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strBanchiCD2.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Ԓn�R�[�h�Q", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�Ԓn�R�[�h�R
            If Not (cAtenaGetPara1.p_strBanchiCD3.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strBanchiCD3.Trim)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Ԓn�R�[�h�R", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�f�[�^�敪
            '*����ԍ� 000021 2003/12/01 �C���J�n
            'If Not (cAtenaGetPara1.p_strDataKB = String.Empty) Then
            If Not ((cAtenaGetPara1.p_strDataKB = String.Empty) Or (cAtenaGetPara1.p_strDataKB = "1%")) Then
                '*����ԍ� 000021 2003/12/01 �C���I��
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strDataKB)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�f�[�^�敪", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�Z����ʂP
            If Not (cAtenaGetPara1.p_strJuminSHU1 = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strJuminSHU1)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z����ʂP", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�Z����ʂQ
            If Not (cAtenaGetPara1.p_strJuminSHU2 = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strJuminSHU2)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z����ʂQ", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�w��N����
            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty Or cAtenaGetPara1.p_strShiteiYMD = "00000000") Then
                m_cfDateClass.p_strDateValue = cAtenaGetPara1.p_strShiteiYMD
                If (Not m_cfDateClass.CheckDate()) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�w��N����", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�s�����R�[�h
            If Not (cAtenaGetPara1.p_strShichosonCD = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strShichosonCD)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�s�����R�[�h", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�\������
            If (cAtenaGetPara1.p_intHyojiKensu < 0) Or (cAtenaGetPara1.p_intHyojiKensu > 999) Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�\������", objErrorStruct.m_strErrorCode)
            End If

            '�Z���R�[�h�Ɛ��уR�[�h��NULL�ŁA���ш��ҏW��"1"�̎��A��O�G���[
            If (cAtenaGetPara1.p_strJuminCD = String.Empty) _
                    And (cAtenaGetPara1.p_strStaiCD = String.Empty) _
                    And (cAtenaGetPara1.p_strStaiinHenshu = "1") Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "���ш��ҏW", objErrorStruct.m_strErrorCode)
            End If

            '����
            If Not (cAtenaGetPara1.p_strKyuuji.Trim = String.Empty) Then
                If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKyuuji.Replace("%"c, String.Empty), m_cfConfigDataClass)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "����", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�J�i����
            If Not (cAtenaGetPara1.p_strKanaKyuuji.Trim = String.Empty) Then
                If (Not UFStringClass.CheckANK(cAtenaGetPara1.p_strKanaKyuuji.Replace("%"c, String.Empty))) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�J�i����", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�J�^�J�i���L��
            If Not (cAtenaGetPara1.p_strKatakanaHeikimei.Trim = String.Empty) Then
                If (Not UFStringClass.CheckKataKanaWide(cAtenaGetPara1.p_strKatakanaHeikimei.Replace("%"c, String.Empty))) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�J�^�J�i���L��", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�Z��
            If Not (cAtenaGetPara1.p_strJusho.Trim = String.Empty) Then
                If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strJusho.Replace("%"c, String.Empty), m_cfConfigDataClass)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z��", objErrorStruct.m_strErrorCode)
                End If
            End If

            '����
            If Not (cAtenaGetPara1.p_strKatagaki.Trim = String.Empty) Then
                If (Not UFStringClass.CheckKanjiCode(cAtenaGetPara1.p_strKatagaki.Replace("%"c, String.Empty), m_cfConfigDataClass)) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "����", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�d�b�ԍ�
            If Not (cAtenaGetPara1.p_strRenrakusaki.Trim = String.Empty) Then
                If (Not UFStringClass.CheckNumber(cAtenaGetPara1.p_strRenrakusaki.Replace("-", String.Empty))) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�d�b�ԍ�", objErrorStruct.m_strErrorCode)
                End If
            End If

            '�Z���R�[�h�`�Ԓn�R�[�h�R���ׂĂ�NULL�̎��A��O�G���[
            '*����ԍ� 000027 2005/05/06 �C���J�n
            '*����ԍ� 000048 2014/04/28 �C���J�n
            ' ���ʔԍ��̒P�Ǝw����\�Ƃ��邽�߁A���荀�ڂɒǉ�����B
            'If (cAtenaGetPara1.p_strJuminCD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strStaiCD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strKanaSeiMei.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strKanaSei.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strKanaMei.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strKanjiShimei.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strUmareYMD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strJushoCD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strGyoseikuCD.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strChikuCD1.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strChikuCD2.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strChikuCD3.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strBanchiCD1.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strBanchiCD2.Trim = String.Empty) _
            '        And (cAtenaGetPara1.p_strBanchiCD3.Trim = String.Empty) Then

            If (Not cAtenaGetPara1.p_strShiteiYMD.Trim = String.Empty) AndAlso
               (intHyojunKB = ABEnumDefine.HyojunKB.KB_Tsujo) Then
                If (cAtenaGetPara1.p_strJuminCD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strStaiCD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strKanaSeiMei.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strKanaSei.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strKanaMei.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strKanjiShimei.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strUmareYMD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strJushoCD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strGyoseikuCD.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strChikuCD1.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strChikuCD2.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strChikuCD3.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strBanchiCD1.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strBanchiCD2.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strBanchiCD3.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strMyNumber.Trim = String.Empty) _
                    And (cAtenaGetPara1.p_strRenrakusaki.Trim = String.Empty) Then
                    '*����ԍ� 000048 2014/04/28 �C���I��
                    '*����ԍ� 000027 2005/05/06 �C���I��

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�����L�[�Ȃ�", objErrorStruct.m_strErrorCode)
                End If
            Else
                If (cAtenaGetPara1.p_strJuminCD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strStaiCD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanaSeiMei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanaSei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanaMei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanjiShimei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strUmareYMD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strJushoCD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strGyoseikuCD.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strChikuCD1.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strChikuCD2.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strChikuCD3.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strBanchiCD1.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strBanchiCD2.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strBanchiCD3.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strMyNumber.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKyuuji.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKanaKyuuji.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKatakanaHeikimei.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strJusho.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strKatagaki.Trim = String.Empty) _
                    AndAlso (cAtenaGetPara1.p_strRenrakusaki.Trim = String.Empty) Then

                    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '�G���[��`���擾
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                    '��O�𐶐�
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�����L�[�Ȃ�", objErrorStruct.m_strErrorCode)
                End If
            End If

            '*����ԍ� 000040 2008/11/10 �ǉ��J�n
            If ((cAtenaGetPara1.p_strTdkdKB = "1" OrElse cAtenaGetPara1.p_strTdkdKB = "2") AndAlso
                cAtenaGetPara1.p_strTdkdZeimokuCD = ABEnumDefine.ZeimokuCDType.Empty) Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "���p�͏o�擾�敪���g�p����ꍇ�́A���p�͏o�擾�p�ŖڃR�[�h���w�肵�Ă��������B",
                                         objErrorStruct.m_strErrorCode)
            End If
            '*����ԍ� 000040 2008/11/10 �ǉ��I��

            '*����ԍ� 000051 2020/11/02 �ǉ��J�n
            '���p�͏o���p�敪
            If ((cAtenaGetPara1.p_strTdkdKB = "1" OrElse cAtenaGetPara1.p_strTdkdKB = "2") AndAlso
                Not (cAtenaGetPara1.p_strTdkdRiyoKB = String.Empty OrElse cAtenaGetPara1.p_strTdkdRiyoKB = "1" OrElse cAtenaGetPara1.p_strTdkdRiyoKB = "2" OrElse cAtenaGetPara1.p_strTdkdRiyoKB = "3")) Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001036)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "���p�͏o���p�敪", objErrorStruct.m_strErrorCode)
            End If
            '*����ԍ� 000051 2020/11/02 �ǉ��I��

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
            Throw objExp
        End Try

    End Sub
#End Region

#Region " �������̃}�[�W(CreateAtenaDataSet) "
    '************************************************************************************************
    '* ���\�b�h��     �������̃}�[�W
    '* 
    '* �\��           Private Function CreateAtenaDataSet(ByVal csAtenaH As DataSet, _
    '*                                                  ByVal csAtenaHS As DataSet, _
    '*                                                  ByVal csAtenaD As DataSet, _
    '*                                                  ByVal csAtenaDS As DataSet) As DataSet
    '* 
    '* �@�\�@�@    �@�@�e�������f�[�^�Z�b�g���}�[�W����
    '* 
    '* ����           csAtenaH As DataSet   : �����f�[�^
    '*                csAtenaHS As DataSet  : ���t��f�[�^
    '*                csAtenaD  As DataSet  : ��[�f�[�^
    '*                csAtenaDS As DataSet  : ��[���t��f�[�^
    '* �@�@           blnKobetsu       : �ʎ擾(True:�e�ʃ}�X�^���f�[�^���擾����)
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    '*����ԍ� 000020 2003/11/19 �C���J�n
    'Private Function CreateAtenaDataSet(ByVal csAtenaH As DataSet, ByVal csAtenaHS As DataSet, _
    '                                    ByVal csAtenaD As DataSet, ByVal csAtenaDS As DataSet) As DataSet
    Private Function CreateAtenaDataSet(ByVal csAtenaH As DataSet, ByVal csAtenaHS As DataSet,
                                        ByVal csAtenaD As DataSet, ByVal csAtenaDS As DataSet,
                                        ByVal blnKobetsu As Boolean, ByVal intHyojunKB As ABEnumDefine.HyojunKB) As DataSet
        '*����ԍ� 000020 2003/11/19 �C���I��
        Const THIS_METHOD_NAME As String = "CreateAtenaDataSet"
        Dim csAtena1 As DataSet                             '�������(ABAtena1)
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim csRow As DataRow
        'Dim csNewRow As DataRow
        '* corresponds to VS2008 End 2010/04/16 000044
        'Dim cABCommon As ABCommonClass                      '�����Ɩ����ʃN���X
        Dim strTableName As String

        Try

            '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
            '���O�o�͗p�N���X�C���X�^���X��
            'm_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)
            '* ����ԍ� 000023 2004/08/27 �폜�I��

            '�f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����Ɩ����ʃN���X�̃C���X�^���X��
            'cABCommon = New ABCommonClass()

            '�������̃C���X�^���X��
            csAtena1 = New DataSet

            If (blnKobetsu) Then
                If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    strTableName = ABAtena1KobetsuHyojunEntity.TABLE_NAME
                Else
                    strTableName = ABAtena1KobetsuEntity.TABLE_NAME
                End If
            Else
                If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                    strTableName = ABAtena1HyojunEntity.TABLE_NAME
                Else
                    strTableName = ABAtena1Entity.TABLE_NAME
                End If
            End If

            '�����f�[�^���݃`�F�b�N
            If Not (csAtenaH Is Nothing) Then
                ''*����ԍ� 000020 2003/11/19 �C���J�n
                '''�������Ɉ����f�[�^��ǉ�����
                ''csAtena1.Merge(csAtenaH.Tables(ABAtena1Entity.TABLE_NAME))

                'If (blnKobetsu) Then
                '    '�������Ɉ����f�[�^��ǉ�����
                '    csAtena1.Merge(csAtenaH.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                'Else
                '    '�������Ɉ����f�[�^��ǉ�����
                '    csAtena1.Merge(csAtenaH.Tables(ABAtena1Entity.TABLE_NAME))
                'End If
                ''*����ԍ� 000020 2003/11/19 �C���I��
                '�������Ɉ����f�[�^��ǉ�����
                csAtena1.Merge(csAtenaH.Tables(strTableName))
            End If

            '��[�f�[�^���݃`�F�b�N
            If Not (csAtenaD Is Nothing) Then
                ''*����ԍ� 000020 2003/11/19 �C���J�n
                '''��[�f�[�^��ǉ�����
                ''csAtena1.Merge(csAtenaD.Tables(ABAtena1Entity.TABLE_NAME))

                'If (blnKobetsu) Then
                '    '�������Ɉ����f�[�^��ǉ�����
                '    csAtena1.Merge(csAtenaD.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                'Else
                '    '�������Ɉ����f�[�^��ǉ�����
                '    csAtena1.Merge(csAtenaD.Tables(ABAtena1Entity.TABLE_NAME))
                'End If
                ''*����ԍ� 000020 2003/11/19 �C���I��
                '�������ɑ�[�f�[�^��ǉ�����
                csAtena1.Merge(csAtenaD.Tables(strTableName))
            End If

            '���t��f�[�^���݃`�F�b�N
            If Not (csAtenaHS Is Nothing) Then
                ''*����ԍ� 000020 2003/11/19 �C���J�n
                '''���t��f�[�^��ǉ�����
                ''csAtena1.Merge(csAtenaHS.Tables(ABAtena1Entity.TABLE_NAME))

                'If (blnKobetsu) Then
                '    '�������Ɉ����f�[�^��ǉ�����
                '    csAtena1.Merge(csAtenaHS.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                'Else
                '    '�������Ɉ����f�[�^��ǉ�����
                '    csAtena1.Merge(csAtenaHS.Tables(ABAtena1Entity.TABLE_NAME))
                'End If
                ''*����ԍ� 000020 2003/11/19 �C���I��
                '�������ɑ��t��f�[�^��ǉ�����
                csAtena1.Merge(csAtenaHS.Tables(strTableName))
            End If

            '��[���t��f�[�^���݃`�F�b�N
            If Not (csAtenaDS Is Nothing) Then
                ''*����ԍ� 000020 2003/11/19 �C���J�n
                '''��[���t��f�[�^��ǉ�����
                ''csAtena1.Merge(csAtenaDS.Tables(ABAtena1Entity.TABLE_NAME))

                'If (blnKobetsu) Then
                '    '�������Ɉ����f�[�^��ǉ�����
                '    csAtena1.Merge(csAtenaDS.Tables(ABAtena1KobetsuEntity.TABLE_NAME))
                'Else
                '    '�������Ɉ����f�[�^��ǉ�����
                '    csAtena1.Merge(csAtenaDS.Tables(ABAtena1Entity.TABLE_NAME))
                'End If
                ''*����ԍ� 000020 2003/11/19 �C���I��
                '�������ɑ�[���t��f�[�^��ǉ�����
                csAtena1.Merge(csAtenaDS.Tables(strTableName))
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

        Return csAtena1

    End Function
#End Region

#Region " �A����ҏW����(RenrakusakiHenshu) "
    '*����ԍ� 000022 2003/12/02 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �A����ҏW����
    '* 
    '* �\��           Private Sub RenrakusakiHenshu(ByVal strGyomuCD As String, 
    '* �@�@                                         ByVal strGyomunaiSHU_CD As String, 
    '* �@�@                                         ByRef csAtenaH As DataSet,
    '* �@�@                                         ByRef csOrgAtena As DataSet)
    '* 
    '* �@�\�@�@    �@�@�A������擾���āA�ҏW����
    '* 
    '* ����           strGyomuCD As String          : �Ɩ��R�[�h
    '* �@�@           strGyomunaiSHU_CD As String   : �Ɩ�����ʃR�[�h
    '*                csAtenaH  As DataSet          : �{�l�f�[�^
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    'Private Sub RenrakusakiHenshu(ByVal strGyomuCD As String, ByVal strGyomunaiSHU_CD As String, ByRef csAtenaH As DataSet)
    Private Sub RenrakusakiHenshu(ByVal strGyomuCD As String, ByVal strGyomunaiSHU_CD As String, ByRef csAtenaH As DataSet,
                                  ByRef csOrgAtena As DataSet, ByVal intHyojunKB As ABEnumDefine.HyojunKB, ByVal strKikanYMD As String)
        '* ����ԍ� 000023 2004/08/27 �폜�J�n�i�{��j
        'Dim cRenrakusakiBClass As ABRenrakusakiBClass       ' �A����a�N���X
        '* ����ԍ� 000023 2004/08/27 �폜�I��
        Dim csRenrakusakiEntity As DataSet                  ' �A����DataSet
        Dim csRenrakusakiRow As DataRow                     ' �A����Row
        Dim csRow As DataRow
        Dim csAtena1Table As DataTable                      ' AtenaTable

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' �Ɩ��R�[�h�����݂��Ȃ��ꍇ�́A�������Ȃ�
            If (strGyomuCD.Trim = String.Empty) Then
                Exit Sub
            End If

            ' �A����a�N���X�̃C���X�^���X�쐬
            'cRenrakusakiBClass = New ABRenrakusakiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            If (m_cRenrakusakiBClass Is Nothing) Then
                m_cRenrakusakiBClass = New ABRenrakusakiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
            End If

            If (csAtenaH.Tables.Contains(ABAtena1Entity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABAtena1Entity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABNenkinAtenaEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABNenkinAtenaEntity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABAtena1KobetsuEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABAtena1KobetsuEntity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABAtena1HyojunEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABAtena1HyojunEntity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABNenkinAtenaHyojunEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABNenkinAtenaHyojunEntity.TABLE_NAME)
            ElseIf (csAtenaH.Tables.Contains(ABAtena1KobetsuHyojunEntity.TABLE_NAME)) Then
                csAtena1Table = csAtenaH.Tables(ABAtena1KobetsuHyojunEntity.TABLE_NAME)
            Else
                ' �V�X�e���G���[
            End If

            '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��j
            Dim intCount As Integer = 0
            Dim csAtenaRow As DataRow
            '* ����ԍ� 000024 2005/01/25 �ǉ��I��

            For Each csRow In csAtena1Table.Rows
                '* ����ԍ� 000024 2005/01/25 �ǉ��J�n�i�{��jIF����ǉ�
                csAtenaRow = csOrgAtena.Tables(0).Rows(intCount)
                If (Not (csAtenaRow.Item(ABAtenaCountEntity.RENERAKUSAKICOUNT) Is System.DBNull.Value)) Then
                    If (CType(csAtenaRow.Item(ABAtenaCountEntity.RENERAKUSAKICOUNT), Integer) > 0) Then
                        '* ����ԍ� 000024 2005/01/25 �ǉ��I���i�{��jIF����ǉ�
                        ' �A����f�[�^���擾����
                        csRenrakusakiEntity = m_cRenrakusakiBClass.GetRenrakusakiBHoshu_Hyojun(CType(csRow(ABAtena1Entity.JUMINCD), String), strGyomuCD, strGyomunaiSHU_CD, strKikanYMD)
                        If (csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows.Count <> 0) Then
                            csRenrakusakiRow = csRenrakusakiEntity.Tables(ABRenrakusakiEntity.TABLE_NAME).Rows(0)
                            '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j
                            csRenrakusakiRow.BeginEdit()
                            '* ����ԍ� 000023 2004/08/27 �ǉ��I��
                            '�A����P
                            If (CType(csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1), String).Trim <> "03") AndAlso
                               (CType(csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1), String).RLength <= 15) Then
                                csRow(ABAtena1Entity.RENRAKUSAKI1) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                            End If
                            '�A����Q
                            If (CType(csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2), String).Trim <> "03") AndAlso
                               (CType(csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2), String).RLength <= 15) Then
                                csRow(ABAtena1Entity.RENRAKUSAKI2) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                            End If
                            Select Case csAtena1Table.TableName
                                Case ABNenkinAtenaEntity.TABLE_NAME, ABNenkinAtenaHyojunEntity.TABLE_NAME
                                    '�A����擾�Ɩ��R�[�h
                                    csRow(ABNenkinAtenaEntity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                                Case ABAtena1KobetsuEntity.TABLE_NAME, ABAtena1KobetsuHyojunEntity.TABLE_NAME
                                    '�A����擾�Ɩ��R�[�h
                                    csRow(ABAtena1KobetsuEntity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                                    '*����ԍ� 000030 2007/04/21 �C���J�n
                                Case ABAtena1Entity.TABLE_NAME, ABAtena1HyojunEntity.TABLE_NAME
                                    '*����ԍ� 000042 2008/11/18 �C���J�n
                                    ' ���\�b�h�敪�����̏ꍇ�̂݃Z�b�g����
                                    '�A����擾�Ɩ��R�[�h (���p�e�[�u���̏ꍇ�̂݃Z�b�g����B���ڐ�68�ȏ�͉��p�e�[�u���Ƃ݂Ȃ��B)
                                    'If csRow.ItemArray.Length > 67 Then
                                    If (m_blnMethodKB = ABEnumDefine.MethodKB.KB_Kaigo) Then
                                        csRow(ABAtena1Entity.RENRAKUSAKI_GYOMUCD) = strGyomuCD
                                    End If
                                    '*����ԍ� 000042 2008/11/18 �C���I��
                                    '*����ԍ� 000030 2007/04/21 �C���I��
                            End Select
                            '* ����ԍ� 000023 2004/08/27 �ǉ��J�n�i�{��j

                            If (intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun) Then
                                '�A����敪
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKIKB) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKIKB)
                                '�A���於
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKIMEI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKIMEI)
                                '�A����P
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKI1_RENRAKUSAKI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI1)
                                '�A����Q
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKI2_RENRAKUSAKI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI2)
                                '�A����R
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKI3_RENRAKUSAKI) = csRenrakusakiRow(ABRenrakusakiEntity.RENRAKUSAKI3)
                                '�A�����ʂP
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU1) = csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU1)
                                '�A�����ʂQ
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU2) = csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU2)
                                '�A�����ʂR
                                csRow(ABAtena1HyojunEntity.RENRAKUSAKISHUBETSU3) = csRenrakusakiRow(ABRenrakusakiFZYHyojunEntity.RENRAKUSAKISHUBETSU3)
                            Else
                            End If

                            csRenrakusakiRow.EndEdit()
                            '* ����ԍ� 000023 2004/08/27 �ǉ��I��
                        End If
                    End If
                End If
                intCount = intCount + 1
            Next csRow

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
    '*����ԍ� 000022 2003/12/02 �ǉ��I��
#End Region

    '*����ԍ� 000031 2007/07/28 �ǉ��J�n
#Region " ����l��\�ҏZ���R�[�h�擾(GetDaihyoJuminCD)"
    '************************************************************************************************
    '* ���\�b�h��     ����l��\�ҏZ���R�[�h�擾
    '* 
    '* �\��           Private Sub GetDaihyoJuminCD(ByRef cAtenaGetPara1 As ABAtenaGetPara1XClass)
    '* 
    '* �@�\�@�@    �@�@�Z���R�[�h�Z�b�g
    '* 
    '* ����           cAtenaGetPara1�@�F�@�����p���߁[��
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub GetDaihyoJuminCD(ByRef cAtenaGetPara1 As ABAtenaGetPara1XClass)
        Const THIS_METHOD_NAME As String = "GetDaihyoJuminCD"
        '* corresponds to VS2008 Start 2010/04/16 000044
        'Dim strDaihyoJuminCD As String                  '��\�ҏZ���R�[�h
        '* corresponds to VS2008 End 2010/04/16 000044

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������ɂ��A��\�Ҏ擾�̏������s��
            If cAtenaGetPara1.p_strJuminCD <> String.Empty AndAlso cAtenaGetPara1.p_strJukiJutogaiKB = "" AndAlso cAtenaGetPara1.p_strDaihyoShaKB = "" Then

                '�Ǘ����擾���s��
                If m_strDoitsu_Param = String.Empty Then
                    '�����o�ɖ����ꍇ�̂݃C���X�^���X�����s��
                    If (m_cABAtenaKanriJohoB Is Nothing) Then
                        m_cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    End If
                    '�Ǘ������擾
                    m_strDoitsu_Param = m_cABAtenaKanriJohoB.GetDoitsuHantei_Param()
                End If

                '�Ǘ����ɂ��A����l��\�Ҏ擾���s�������肷��
                If m_strDoitsu_Param = ABConstClass.PRM_DAIHYO Then
                    '�Z���R�[�h��ޔ�������
                    m_strHonninJuminCD = cAtenaGetPara1.p_strJuminCD.Trim
                    '�����o�ɖ����ꍇ�̂݃C���X�^���X�����s��
                    If (m_cABGappeiDoitsuninB Is Nothing) Then
                        m_cABGappeiDoitsuninB = New ABGappeiDoitsuninBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    End If

                    '����l��\�҂̏����擾���A�����p�����[�^�փZ�b�g����
                    cAtenaGetPara1.p_strJuminCD = m_cABGappeiDoitsuninB.GetDoitsuninDaihyoJuminCD(m_strHonninJuminCD)
                Else
                    '�ޔ�p�Z���R�[�h���N���A����
                    m_strHonninJuminCD = String.Empty
                End If
            Else
                '*����ԍ� 000037 2008/01/17 �ǉ��J�n
                '�ޔ�p�Z���R�[�h���N���A����
                m_strHonninJuminCD = String.Empty
                '*����ԍ� 000037 2008/01/17 �ǉ��I��
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
#End Region

#Region " �Z���R�[�h�Z�b�g(SetJuminCD) "
    '************************************************************************************************
    '* ���\�b�h��     �Z���R�[�h�Z�b�g�i���������j
    '* 
    '* �\��           Private Sub SetJuminCD(ByRef csDataSet As DataSet)
    '* 
    '* �@�\�@�@    �@�@�Z���R�[�h�Z�b�g
    '* 
    '* ����           csDataSet�@�F�@�����f�[�^�Z�b�g
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetJuminCD(ByRef csDataSet As DataSet)
        Const THIS_METHOD_NAME As String = "SetJuminCD"
        Dim intCnt As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�ޔ������Z���R�[�h�����݂���ꍇ�́A�㏑������
            If (m_strHonninJuminCD <> String.Empty) Then

                '�e�[�u�����ɂ���ďꍇ�������s��(�e�[�u���͕K���P�����Ȃ�)
                Select Case csDataSet.Tables(0).TableName
                    Case ABAtena1Entity.TABLE_NAME, ABAtena1KobetsuEntity.TABLE_NAME, ABAtena1HyojunEntity.TABLE_NAME, ABAtena1KobetsuHyojunEntity.TABLE_NAME
                        '����l��\�Ҏ擾���s�����ꍇ�́A�ޔ������Z���R�[�h(�{�l)�ŏ㏑������
                        For intCnt = 0 To csDataSet.Tables(0).Rows.Count - 1
                            '�{�l�E���t��i�{�l�j���R�[�h�̂ݏ㏑������
                            If (CStr(csDataSet.Tables(0).Rows(intCnt).Item(ABAtena1Entity.DAINOKB)) = ABConstClass.DAINOKB_HONNIN) OrElse
                                (CStr(csDataSet.Tables(0).Rows(intCnt).Item(ABAtena1Entity.DAINOKB)) = ABConstClass.DAINOKB_H_SFSK) Then
                                csDataSet.Tables(0).Rows(intCnt).Item(ABAtena1Entity.JUMINCD) = m_strHonninJuminCD
                            End If
                        Next

                    Case Else
                        '����l��\�Ҏ擾���s�����ꍇ�́A�ޔ������Z���R�[�h(�{�l)�ŏ㏑������
                        For intCnt = 0 To csDataSet.Tables(0).Rows.Count - 1
                            csDataSet.Tables(0).Rows(intCnt).Item(ABAtenaEntity.JUMINCD) = m_strHonninJuminCD
                        Next

                End Select
            Else
                '�������Ȃ�
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
#End Region
    '*����ԍ� 000031 2007/07/28 �ǉ��I��

    '*����ԍ� 000032 2007/09/04 �ǉ��J�n
#Region " �����J�i�����E�����J�i���E�����J�i���ҏW(HenshuSearchKana)"
    '************************************************************************************************
    '* ���\�b�h��     �����J�i�����E�����J�i���E�����J�i���ҏW
    '* 
    '* �\��           Private Function HenshuSearchKana(ByRef cSearchKey As ABAtenaSearchKey,
    '*                                                  ByRef blnHommyoYusen As Boolean) As ABAtenaSearchKey 
    '* 
    '* �@�\�@�@    �@ ���������̃J�i������W���d�l�ƊO���l�{�������@�\�p�ɕҏW����
    '* 
    '* ����           ABAtenaSearchKey�@�F�@���������L�[�p�����[�^
    '* 
    '* �߂�l         ABAtenaSearchKey�@�F�@���������L�[�p�����[�^
    '************************************************************************************************
    <SecuritySafeCritical>
    Private Function HenshuSearchKana(ByVal cSearchKey As ABAtenaSearchKey,
                                        ByVal blnHommyoYusen As Boolean) As ABAtenaSearchKey
        Const THIS_METHOD_NAME As String = "HenshuSearchKana"

        Dim cSearch_Param As ABAtenaSearchKey '���������L�[�p�����[�^
        Dim HenshuKanaSeiMei As String = String.Empty  '�ҏW�����p�J�i����(�p�����͑啶���Ŋi�[���邱��)
        Dim HenshuKanaSei As String = String.Empty     '�ҏW�����p�J�i��(�p�����͑啶���Ŋi�[���邱��)
        Dim HenshuKanaMei As String = String.Empty     '�ҏW�����p�J�i��(�p�����͑啶���Ŋi�[���邱��)
        '* ����ԍ� 000034 2007/10/10 �ǉ��J�n
        Dim HenshuKanaSei2 As String = String.Empty    '�ҏW�����p�J�i���Q(�p�����͑啶���Ŋi�[���邱��)
        Dim cuString As New USStringClass              '�~�h���l�[����������
        '* ����ԍ� 000034 2007/10/10 �ǉ��I��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '���������L�[�p�����[�^���R�s�[
            cSearch_Param = cSearchKey

            '�O���l�{�������@�\�����ݒ�����������L�[�p�����[�^�ɐݒ�
            cSearch_Param.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho

            '�W���d�l�̏ꍇ�͉����ҏW�����ɂ��̂܂ܕԂ�
            '�O���l�{���D�挟���@�\���������ꂽ�s������
            '�c�a���ڂ���p�Ȃ̂�(�����p�J�i�����E�����p�J�i���E�����p�J�i���E�����p�������̂����ꂼ��ăZ�b�g)
            If (m_cURKanriJohoB.GetFrn_HommyoKensaku_Param() = 2) Then
                '�O���l�{�������@�\�����������L�[�p�����[�^�ɐݒ�
                cSearch_Param.p_enGaikokuHommyoKensaku = FrnHommyoKensakuType.Tsusho_Seishiki
                If (blnHommyoYusen = True) Then
                    '�����p�����[�^�̕ҏW
                    '*����ԍ� 000036 2007/11/06 �ǉ��J�n
                    ' �����J�i�������L��A�����J�i���������̏ꍇ�A�����J�i�����͌����J�i���Ɠ��l�̈���������
                    If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty AndAlso
                        cSearchKey.p_strSearchKanaSei = String.Empty) Then
                        cSearchKey.p_strSearchKanaSei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                    End If
                    ''�����J�i�����������J�i���̌����L�[�p�����[�^�Ƃ��ăZ�b�g
                    'If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty) Then
                    '    HenshuKanaSei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                    'End If
                    '*����ԍ� 000036 2007/11/06 �ǉ��I��
                    '�����J�i���������J�i���̌����L�[�p�����[�^�Ƃ��ăZ�b�g
                    If (cSearchKey.p_strSearchKanaSei <> String.Empty) Then
                        '*����ԍ� 000036 2007/11/06 �C���J�n
                        ' �����p�J�i���̃A���t�@�x�b�g��啶���ɕϊ�����
                        HenshuKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper()
                        ''�����J�i���̕����̍Ō��"%"��K���t������
                        'If (InStr(cSearchKey.p_strSearchKanaSei, "%") = cSearchKey.p_strSearchKanaSei.Length) Then
                        '    HenshuKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper()
                        'Else
                        '    HenshuKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper() + "%"
                        'End If
                        '*����ԍ� 000036 2007/11/06 �C���I��
                    End If
                    '�J�i���ƃJ�i��������ꍇ�C�������Č����J�i���̌����L�[�p�����[�^�Ƃ��ăZ�b�g
                    '�S�Ă̌����J�i���ڂŌ�����������ꂽ�ꍇ�͂��̌����L�[���Z�b�g�����
                    If (cSearchKey.p_strSearchKanaSei <> String.Empty AndAlso cSearchKey.p_strSearchKanaMei <> String.Empty) Then
                        '* ����ԍ� 000034 2007/10/10 �ǉ��J�n
                        ' �J�i���̐擪������"�"�̏ꍇ�̂�"�"�ɒu�����Č����p�J�i���Q�𐶐�����
                        If (cSearchKey.p_strSearchKanaMei.StartsWith("�")) Then
                            ' �J�i���Ɋ܂܂��~�h���l�[�����ł������q�b�g����悤�ɃX�y�[�X������ꍇ�̓X�y�[�X���������������s��
                            If (InStr(cSearchKey.p_strSearchKanaMei, " ") <> 0) Then
                                HenshuKanaSei2 = HenshuKanaSei + cuString.ToKanaKey(Replace(cSearchKey.p_strSearchKanaMei, "�", "�", 1, 1).Replace(" ", String.Empty)).ToUpper()
                            Else
                                HenshuKanaSei2 = HenshuKanaSei + Replace(cSearchKey.p_strSearchKanaMei, "�", "�", 1, 1).ToUpper()
                            End If
                        End If
                        ' �J�i���Ɋ܂܂��~�h���l�[�����ł������q�b�g����悤�ɃX�y�[�X������ꍇ�̓X�y�[�X���������������s��
                        If (InStr(cSearchKey.p_strSearchKanaMei, " ") <> 0) Then
                            HenshuKanaSei = HenshuKanaSei + cuString.ToKanaKey(cSearchKey.p_strSearchKanaMei.Replace(" ", String.Empty)).ToUpper()
                        Else
                            HenshuKanaSei = HenshuKanaSei + cSearchKey.p_strSearchKanaMei.ToUpper()
                        End If
                        'HenshuKanaSei = HenshuKanaSei + cSearchKey.p_strSearchKanaMei.ToUpper()
                        '* ����ԍ� 000034 2007/10/10 �ǉ��I��
                    End If
                    '�J�i���݂̂̏ꍇ�C�擪�Ɂ������������J�i���̌����L�[�p�����[�^�Ƃ��ăZ�b�g
                    If (cSearchKey.p_strSearchKanaSei = String.Empty AndAlso cSearchKey.p_strSearchKanaMei <> String.Empty) Then
                        '* ����ԍ� 000034 2007/10/10 �ǉ��J�n
                        ' �J�i���̐擪������"�"�̏ꍇ�̂�"�"�ɒu�����Č����p�J�i���Q�𐶐�����
                        If (cSearchKey.p_strSearchKanaMei.StartsWith("�")) Then
                            ' �J�i���Ɋ܂܂��~�h���l�[�����ł������q�b�g����悤�ɃX�y�[�X������ꍇ�̓X�y�[�X���������������s��
                            If (InStr(cSearchKey.p_strSearchKanaMei, " ") <> 0) Then
                                HenshuKanaSei2 = "%" + cuString.ToKanaKey(Replace(cSearchKey.p_strSearchKanaMei, "�", "�", 1, 1).Replace(" ", String.Empty)).ToUpper()
                            Else
                                HenshuKanaSei2 = "%" + Replace(cSearchKey.p_strSearchKanaMei, "�", "�", 1, 1).ToUpper()
                            End If
                        End If
                        ' �J�i���Ɋ܂܂��~�h���l�[�����ł������q�b�g����悤�ɃX�y�[�X������ꍇ�̓X�y�[�X���������������s��
                        If (InStr(cSearchKey.p_strSearchKanaMei, " ") <> 0) Then
                            HenshuKanaSei = "%" + cuString.ToKanaKey(cSearchKey.p_strSearchKanaMei.Replace(" ", String.Empty)).ToUpper()
                        Else
                            HenshuKanaSei = "%" + cSearchKey.p_strSearchKanaMei.ToUpper()
                        End If
                        'HenshuKanaSei = "%" + cSearchKey.p_strSearchKanaMei.ToUpper()
                        '* ����ԍ� 000034 2007/10/10 �ǉ��I��
                    End If
                    '�����p�J�i���Q�ɕҏW���������L�[�������L�[�p�����[�^�ɃZ�b�g
                    '�{���̌����p�����[�^���Z�b�g
                    cSearch_Param.p_strSearchKanaSeiMei = String.Empty
                    cSearch_Param.p_strSearchKanaSei = HenshuKanaSei                            '�J�i�͌����J�i���̍��ڂ݂̂Ō���
                    cSearch_Param.p_strSearchKanaMei = String.Empty
                    cSearch_Param.p_strSearchKanaSei2 = HenshuKanaSei2                    '�����p�J�i���Q
                    '������������
                    cSearch_Param.p_strKanjiMeisho2 = cSearchKey.p_strSearchKanjiMeisho         '�������̂Q�Ɍ����p�������̂��Z�b�g
                    cSearch_Param.p_strSearchKanjiMeisho = String.Empty
                Else
                    '�����p�����[�^�̕ҏW
                    '*����ԍ� 000036 2007/11/06 �ǉ��J�n
                    ' �����J�i�������L��A�����J�i���������̏ꍇ�A�����J�i�����͌����J�i���Ɠ��l�̈���������
                    If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty AndAlso
                        cSearchKey.p_strSearchKanaSei = String.Empty) Then
                        cSearchKey.p_strSearchKanaSei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                    End If
                    ''�����J�i�����������J�i�����̌����L�[�p�����[�^�Ƃ��ăZ�b�g
                    'If (cSearchKey.p_strSearchKanaSeiMei <> String.Empty) Then
                    '    HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSeiMei.ToUpper()
                    'End If
                    '*����ԍ� 000036 2007/11/06 �ǉ��I��
                    '�����J�i��������ꍇ�͌����J�i�����Ƀp�����[�^���Z�b�g
                    If (cSearchKey.p_strSearchKanaSei <> String.Empty) Then
                        '*����ԍ� 000036 2007/11/06 �C���J�n
                        ' �����J�i���ƌ����J�i���̗�����"%"�������ꍇ�͊��S��v
                        If (InStr(cSearchKey.p_strSearchKanaSei, "%") = 0 AndAlso
                            InStr(cSearchKey.p_strSearchKanaMei, "%") = 0) Then
                            ' ���S��v���̂݌����J�i�����Ƃ��Č�������̂ŁA���������s��
                            HenshuKanaSeiMei = cuString.ToKanaKey(cSearchKey.p_strSearchKanaSei + cSearchKey.p_strSearchKanaMei).ToUpper()
                        Else
                            ' "%"������ꍇ�͂��̂܂܌����J�i�����ɑ啶�������ăZ�b�g
                            ' ������"%"�݂̂̏ꍇ�͉����Z�b�g���Ȃ�
                            If (cSearchKey.p_strSearchKanaSei <> "%") Then
                                HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSei.ToUpper()
                            End If
                            '�����J�i�����A���t�@�x�b�g�啶�������ăZ�b�g
                            If (cSearchKey.p_strSearchKanaMei <> String.Empty) Then
                                HenshuKanaMei = cSearchKey.p_strSearchKanaMei.ToUpper()
                            End If
                        End If
                        ''�����J�i���̕����̍Ō��"%"��K���t�����C�����J�i�����̌����L�[�p�����[�^�Ƃ��ăZ�b�g
                        'If (InStr(cSearchKey.p_strSearchKanaSei, "%") = cSearchKey.p_strSearchKanaSei.Length) Then
                        '    HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSei.ToUpper()
                        'Else
                        '    HenshuKanaSeiMei = cSearchKey.p_strSearchKanaSei.ToUpper() + "%"
                        'End If
                        ''�����J�i�����A���t�@�x�b�g�啶�������ăZ�b�g
                        'If (cSearchKey.p_strSearchKanaMei <> String.Empty) Then
                        '    HenshuKanaMei = cSearchKey.p_strSearchKanaMei.ToUpper()
                        'End If
                        '*����ԍ� 000036 2007/11/06 �C���I��
                    Else
                        '�����J�i��
                        HenshuKanaMei = cSearch_Param.p_strSearchKanaMei.ToUpper()
                    End If
                    '�����p�J�i���Q�ɕҏW���������L�[�������L�[�p�����[�^�ɃZ�b�g
                    '�ʏ̖��̌����p�����[�^���Z�b�g
                    cSearch_Param.p_strSearchKanaSeiMei = HenshuKanaSeiMei                      '�J�i�����C�J�i��
                    cSearch_Param.p_strSearchKanaSei = String.Empty
                    cSearch_Param.p_strSearchKanaMei = HenshuKanaMei                            '�J�i��
                    cSearch_Param.p_strSearchKanaSei2 = String.Empty                         '�����p�J�i���Q�i��ɂ���j
                    '������������
                    cSearch_Param.p_strSearchKanjiMeisho = cSearchKey.p_strSearchKanjiMeisho    '�����p�������̂Ɍ����p�������̂��Z�b�g
                    cSearch_Param.p_strKanjiMeisho2 = String.Empty
                End If
                '* ����ԍ� 000034 2007/10/10 �ǉ��J�n
            Else
                ' �W���d�l�̎s�����ɂ����Ă������J�i���ڂ̃A���t�@�x�b�g�͑啶���ň���
                cSearch_Param.p_strSearchKanaSeiMei = cSearchKey.p_strSearchKanaSeiMei.ToUpper() '�J�i����
                cSearch_Param.p_strSearchKanaSei = cSearchKey.p_strSearchKanaSei.ToUpper()       '�J�i��
                cSearch_Param.p_strSearchKanaMei = cSearchKey.p_strSearchKanaMei.ToUpper()       '�J�i��
                cSearch_Param.p_strSearchKanaSei2 = String.Empty                              '�����p�J�i���Q�i��ɂ���j
                '* ����ԍ� 000034 2007/10/10 �ǉ��I��
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

        Return cSearch_Param

    End Function
#End Region
    '*����ԍ� 000032 2007/09/04 �ǉ��I��

    '*����ԍ� 000040 2008/11/10 �ǉ��J�n
#Region " ���p�͕ҏW����(RiyoTdkHenshu) "
    '************************************************************************************************
    '* ���\�b�h��     ���p�͕ҏW����
    '* 
    '* �\��           Private Sub RiyoTdkHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
    '* �@�@                                     ByVal blnKobetsu As Boolean, 
    '* �@�@                                     ByRef csAtenaH As DataSet)
    '* 
    '* �@�\�@�@    �@ ���p�̓f�[�^���擾���A�����f�[�^�Z�b�g�փZ�b�g����
    '* 
    '* ����           cAtenaGetPara1 As ABAtenaGetPara1XClass   : �����擾�p�����[�^
    '* �@�@           blnKobetsu As Boolean                     : �ʎ�������t���O
    '*                csAtenaH As DataSet                       : �{�l�f�[�^
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub RiyoTdkHenshu(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal blnKobetsu As Boolean, ByRef csAtenaH As DataSet)
        Dim cABLTRiyoTdkB As ABLTRiyoTdkBClass                      ' ABeLTAX���p�̓}�X�^�c�`
        Dim cABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass              ' ABeLTAX���p�̓p�����[�^�N���X
        Dim csRiyoTdkEntity As DataSet                              ' ���p�̓f�[�^�Z�b�g
        Dim csRiyoTdkRow As DataRow                                 ' ���p�̓f�[�^�Z�b�g
        Dim csRow As DataRow
        '*����ԍ� 000041 2008/11/17 �ǉ��J�n
        Dim csNotRiyouTdkdRows As DataRow()
        '*����ԍ� 000041 2008/11/17 �ǉ��I��

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            '*����ԍ� 000041 2008/11/17 �ǉ��J�n
            If Not (cAtenaGetPara1.p_strShiteiYMD = String.Empty) Then
                Exit Try
            Else
            End If
            '*����ԍ� 000041 2008/11/17 �ǉ��I��

            '*����ԍ� 000042 2008/11/18 �C���J�n
            'If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso _
            '    blnKobetsu = False AndAlso (cAtenaGetPara1.p_strTdkdKB = "1" OrElse cAtenaGetPara1.p_strTdkdKB = "2")) Then
            If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso
                blnKobetsu = False AndAlso m_blnMethodKB <> ABEnumDefine.MethodKB.KB_Kaigo AndAlso
                (cAtenaGetPara1.p_strTdkdKB = "1" OrElse cAtenaGetPara1.p_strTdkdKB = "2")) Then
                '*����ԍ� 000042 2008/11/18 �C���I��
                ' �ȈՔłł͂Ȃ��ꍇ���ʎ����擾���Ȃ��ꍇ�����p�͏o�擾�敪��"1,2"�̏ꍇ�A�[�Ŏ�ID�Ɨ��p��ID���Z�b�g

                ' ABeLTAX���p�̓}�X�^�c�`�N���X�̃C���X�^���X�쐬
                cABLTRiyoTdkB = New ABLTRiyoTdkBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                ' ABeLTAX���p�̓p�����[�^�N���X�̃C���X�^���X��
                cABLTRiyoTdkParaX = New ABLTRiyoTdkParaXClass

                ' �擾�f�[�^�Z�b�g����
                For Each csRow In csAtenaH.Tables(0).Rows

                    ' ���p�͏o�p�����[�^�Z�b�g
                    ' �Z���R�[�h
                    If (m_strHonninJuminCD.Trim = String.Empty) Then
                        ' �Z���R�[�h���Z�b�g
                        cABLTRiyoTdkParaX.p_strJuminCD = CStr(csRow(ABAtena1Entity.JUMINCD))
                    Else
                        ' ����l��\�҃f�[�^�̂��߁A�{�l�Z���R�[�h���Z�b�g
                        cABLTRiyoTdkParaX.p_strJuminCD = m_strHonninJuminCD
                    End If

                    ' �ŖڃR�[�h:�Ɩ��R�[�h���Z�b�g
                    cABLTRiyoTdkParaX.p_strZeimokuCD = cAtenaGetPara1.p_strTdkdZeimokuCD

                    ' �p�~�t���O:�p�~�f�[�^�ȊO���擾
                    cABLTRiyoTdkParaX.p_blnHaishiFG = False

                    ' �o�͋敪:�[�Ŏ�ID�A���p��ID�̂Q���ڂ��擾
                    cABLTRiyoTdkParaX.p_strOutKB = "1"

                    '*����ԍ� 000051 2020/11/02 �ǉ��J�n
                    ' ���p�敪�F���p�͏o���p�敪���Z�b�g
                    cABLTRiyoTdkParaX.p_strRiyoKB = cAtenaGetPara1.p_strTdkdRiyoKB
                    '*����ԍ� 000051 2020/11/02 �ǉ��I��

                    ' ���p�͏o�f�[�^���擾
                    csRiyoTdkEntity = cABLTRiyoTdkB.GetLTRiyoTdkData(cABLTRiyoTdkParaX)

                    ' ���p�͏o�f�[�^��{�l�f�[�^�ɃZ�b�g
                    csRow.BeginEdit()
                    If (csRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows.Count <> 0) Then
                        csRiyoTdkRow = csRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows(0)

                        csRow(ABAtena1Entity.NOZEIID) = csRiyoTdkRow(ABLtRiyoTdkEntity.NOZEIID)         ' �[�Ŏ�ID
                        csRow(ABAtena1Entity.RIYOSHAID) = csRiyoTdkRow(ABLtRiyoTdkEntity.RIYOSHAID)     ' ���p��ID
                    Else
                        csRow(ABAtena1Entity.NOZEIID) = String.Empty                                    ' �[�Ŏ�ID
                        csRow(ABAtena1Entity.RIYOSHAID) = String.Empty                                  ' ���p��ID

                    End If
                    csRow.EndEdit()
                Next csRow

                '*����ԍ� 000041 2008/11/17 �ǉ��J�n
                If (cAtenaGetPara1.p_strTdkdKB = "2") Then
                    ' �{�l�f�[�^����[�Ŏ�ID���󔒂̃f�[�^���擾����
                    csNotRiyouTdkdRows = csAtenaH.Tables(0).Select(ABAtena1Entity.NOZEIID + " = ''")

                    ' �[�Ŏ�ID���󔒂̃f�[�^���폜����
                    For Each csRow In csNotRiyouTdkdRows
                        csRow.Delete()
                    Next
                Else
                End If
                '*����ԍ� 000041 2008/11/17 �ǉ��I��
            Else
            End If

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
            Throw

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw
        End Try

    End Sub
#End Region

    '*����ԍ� 000041 2008/11/17 �폜�J�n
#Region " ���p�̓f�[�^�i����(RiyoTdkHenshu_Select) "
    ''************************************************************************************************
    ''* ���\�b�h��     ���p�͕ҏW����
    ''* 
    ''* �\��           Private Sub RiyoTdkHenshu_Select(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
    ''* �@�@                                            ByVal blnKobetsu As Boolean, 
    ''* �@�@                                            ByRef csAtenaH As DataSet)
    ''* 
    ''* �@�\�@�@    �@ �{�l�f�[�^����[�Ŏ�ID�����݂��Ȃ����R�[�h���폜����
    ''* 
    ''* ����           cAtenaGetPara1 As ABAtenaGetPara1XClass   : �����擾�p�����[�^
    ''* �@�@           blnKobetsu As Boolean                     : �ʎ�������t���O
    ''*                csAtenaH As DataSet                       : �{�l�f�[�^
    ''* 
    ''* �߂�l         �Ȃ�
    ''************************************************************************************************
    'Private Sub RiyoTdkHenshu_Select(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal blnKobetsu As Boolean, ByRef csAtena1 As DataSet)
    '    Dim csRow As DataRow
    '    Dim csNotRiyouTdkdRows As DataRow()

    '    Try
    '        '�f�o�b�O�J�n���O�o��
    '        m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    '        If (m_blnSelectAll <> ABEnumDefine.AtenaGetKB.KaniOnly AndAlso _
    '            blnKobetsu = False AndAlso cAtenaGetPara1.p_strTdkdKB = "2") Then
    '            ' �ȈՔłł͂Ȃ��ꍇ���ʎ����擾���Ȃ��ꍇ�����p�͏o�擾�敪��"2"�̏ꍇ�A�[�Ŏ�ID�����݂��Ȃ��f�[�^���폜����

    '            ' �{�l�f�[�^����[�Ŏ�ID���󔒂̃f�[�^���擾����
    '            csNotRiyouTdkdRows = csAtena1.Tables(0).Select(ABAtena1Entity.NOZEIID + " = ''")

    '            ' �[�Ŏ�ID���󔒂̃f�[�^���폜����
    '            For Each csRow In csNotRiyouTdkdRows
    '                csRow.Delete()
    '            Next
    '        Else
    '        End If

    '        ' �f�o�b�O�I�����O�o��
    '        m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    '    Catch objAppExp As UFAppException
    '        ' ���[�j���O���O�o��
    '        m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                    "�y�N���X��:" + Me.GetType.Name + "�z" + _
    '                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
    '                                    "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
    '                                    "�y���[�j���O���e:" + objAppExp.Message + "�z")
    '        ' �G���[�����̂܂܃X���[����
    '        Throw

    '    Catch objExp As Exception
    '        ' �G���[���O�o��
    '        m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                    "�y�N���X��:" + Me.GetType.Name + "�z" + _
    '                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
    '                                    "�y�G���[���e:" + objExp.Message + "�z")
    '        Throw
    '    End Try

    'End Sub
#End Region
    '*����ԍ� 000041 2008/11/17 �폜�I��
    '*����ԍ� 000040 2008/11/10 �ǉ��I��

    '*����ԍ� 000052 2023/03/10 �ǉ��J�n
#Region " �ȈՈ����擾�P_�W����(AtenaGet1_Hyojun) "
    '************************************************************************************************
    '* ���\�b�h��     �ȈՈ����擾�P_�W����
    '* 
    '* �\��           Public Function AtenaGet1_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Overloads Function AtenaGet1_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet

        Return AtenaGet1_Hyojun(cAtenaGetPara1, False)

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �ȈՈ����擾�P_�W����
    '* 
    '* �\��           Public Function AtenaGet1_Hyoujn(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* �@�@           blnKobetsu       : �ʎ擾(True:�e�ʃ}�X�^���f�[�^���擾����)
    '* 
    '* �߂�l         DataSet(ABAtena1Kobetsu) : �擾�����������
    '************************************************************************************************
    Public Overloads Function AtenaGet1_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass,
                                                ByVal blnKobetsu As Boolean) As DataSet

        Return AtenaGetMain(cAtenaGetPara1, blnKobetsu, ABEnumDefine.MethodKB.KB_AtenaGet1, ABEnumDefine.HyojunKB.KB_Hyojun)

    End Function
#End Region

#Region " �ȈՈ����擾�Q_�W����(AtenaGet2_Hyojun) "
    '************************************************************************************************
    '* ���\�b�h��     �ȈՈ����擾�Q_�W����
    '* 
    '* �\��           Public Function AtenaGet2_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Function AtenaGet2_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Const THIS_METHOD_NAME As String = "AtenaGet2_Hyojun"
        Dim csAtenaEntity As DataSet                        '����Entity
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnAtenaKani As Boolean
        Dim blnRirekiSelectAll As ABEnumDefine.AtenaGetKB
        Dim blnRirekiKani As Boolean

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�q�c�a�ڑ�
            If m_blnBatchRdb = False Then
                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                                "�y���s���\�b�h��:Connect�z")
                m_cfRdbClass.Connect()
            End If

            Try
                '�R���X�g���N�^�̐ݒ��ۑ�
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    blnAtenaSelectAll = Me.m_cABAtenaB.m_blnSelectAll
                    blnAtenaKani = Me.m_cABAtenaB.m_blnSelectCount
                    Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = False
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    blnRirekiSelectAll = Me.m_cABAtenaRirekiB.m_blnSelectAll
                    blnRirekiKani = Me.m_cABAtenaRirekiB.m_blnSelectCount
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = False

                End If

                ' �ȈՈ����擾�Q(��������)���\�b�h�����s����B
                csAtenaEntity = Me.GetAtena2(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Hyojun)

                '�R���X�g���N�^�̐ݒ�����ɂ��ǂ�
                If Not (Me.m_cABAtenaB Is Nothing) Then
                    Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
                    Me.m_cABAtenaB.m_blnSelectCount = blnAtenaKani
                End If
                If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                    Me.m_cABAtenaRirekiB.m_blnSelectAll = blnRirekiSelectAll
                    Me.m_cABAtenaRirekiB.m_blnSelectCount = blnRirekiKani
                End If

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
                ' UFAppException���X���[����
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' �G���[�����̂܂܃X���[
                Throw

            Finally
                ' RDB�ؒf
                If m_blnBatchRdb = False Then
                    ' RDB�A�N�Z�X���O�o��
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                            "�y���s���\�b�h��:Disconnect�z")
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtenaEntity

    End Function
#End Region

#Region " ���p�����擾_�W����(GetKaigoAtena_Hyojun) "
    '************************************************************************************************
    '* ���\�b�h��     ���p�����擾_�W����
    '* 
    '* �\��           Public Function GetKaigoAtena_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������擾����
    '* 
    '* ����           cAtenaGetPara1   : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet : �擾�����������
    '************************************************************************************************
    Public Function GetKaigoAtena_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Dim blnAtenaSelectAll As ABEnumDefine.AtenaGetKB
        Dim csAtenaEntity As DataSet                        '���p����Entity

        Try
            '�R���X�g���N�^�̐ݒ��ۑ�
            blnAtenaSelectAll = m_blnSelectAll
            m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            If Not (Me.m_cABAtenaB Is Nothing) Then
                Me.m_cABAtenaB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            End If
            If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                Me.m_cABAtenaRirekiB.m_blnSelectAll = ABEnumDefine.AtenaGetKB.SelectAll
            End If

            '�����擾���C�����\�b�h�̌ďo���i�����F�擾�p�����[�^�N���X�A�ʎ����f�[�^�擾�t���O�A�Ăяo�����\�b�h�敪�j
            csAtenaEntity = AtenaGetMain(cAtenaGetPara1, False, ABEnumDefine.MethodKB.KB_Kaigo, ABEnumDefine.HyojunKB.KB_Hyojun)

            '�R���X�g���N�^�̐ݒ�����ɂ��ǂ�
            m_blnSelectAll = blnAtenaSelectAll
            If Not (Me.m_cABAtenaB Is Nothing) Then
                Me.m_cABAtenaB.m_blnSelectAll = blnAtenaSelectAll
            End If
            If Not (Me.m_cABAtenaRirekiB Is Nothing) Then
                Me.m_cABAtenaRirekiB.m_blnSelectAll = m_blnSelectAll
            End If

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

        Return csAtenaEntity

    End Function
#End Region

#Region " �N�������擾_�W����(NenkinAtenaGet_Hyojun) "
    '************************************************************************************************
    '* ���\�b�h��     �N�������擾_�W����
    '* 
    '* �\��           Public Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* �@�\�@�@       �N�����������擾����
    '* 
    '* ����           cAtenaGetPara1    : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Overloads Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet

        '�N�������Q�b�g���N�����������擾����
        Return NenkinAtenaGet_Hyojun(cAtenaGetPara1, ABEnumDefine.NenkinAtenaGetKB.Version01)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     �N�������擾_�W����
    '* 
    '* �\��           Public Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* �@�\�@�@       �N�����������擾����
    '* 
    '* ����           cAtenaGetPara1    : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Overloads Function NenkinAtenaGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass, ByVal intNenkinAtenaGetKB As Integer) As DataSet

        Return GetNenkinAtena(cAtenaGetPara1, intNenkinAtenaGetKB, ABEnumDefine.HyojunKB.KB_Hyojun)

    End Function
#End Region

#Region " ���ۈ��������擾_�W����(KokuhoAtenaRirekiGet_Hyojun) "
    '************************************************************************************************
    '* ���\�b�h��     ���ۈ��������擾_�W����
    '* 
    '* �\��           Public Function KokuhoAtenaRirekiGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
    '* 
    '* �@�\�@�@       ���ۈ��������f�[�^���擾����
    '* 
    '* ����           cAtenaGetPara1    : �����擾�p�����[�^
    '* 
    '* �߂�l         DataSet(ABAtena1) : �擾�����������
    '************************************************************************************************
    Public Function KokuhoAtenaRirekiGet_Hyojun(ByVal cAtenaGetPara1 As ABAtenaGetPara1XClass) As DataSet
        Const THIS_METHOD_NAME As String = "KokuhoAtenaRirekiGet_Hyojun"
        Dim csAtena1Entity As DataSet                       '����1Entity

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�q�c�a�ڑ�
            If m_blnBatchRdb = False Then
                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData,
                                                "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                                "�y���s���\�b�h��:Connect�z")
                m_cfRdbClass.Connect()
            End If

            Try
                ' �Ǘ����擾(��������)���\�b�h�����s����B
                Me.GetKanriJoho()

                ' ���ۈ��������擾(��������)���\�b�h�����s����B
                csAtena1Entity = Me.GetKokuhoAtenaRireki(cAtenaGetPara1, ABEnumDefine.HyojunKB.KB_Hyojun)

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
                ' UFAppException���X���[����
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' �G���[�����̂܂܃X���[
                Throw

            Finally
                ' RDB�ؒf
                If m_blnBatchRdb = False Then
                    ' RDB�A�N�Z�X���O�o��
                    m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                            "�y���s���\�b�h��:Disconnect�z")
                    m_cfRdbClass.Disconnect()
                End If

            End Try

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

        Return csAtena1Entity

    End Function
#End Region
    '*����ԍ� 000052 2023/03/10 �ǉ��I��

    Public Sub New()

    End Sub
End Class
