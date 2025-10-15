'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �����Ǘ����c�`(ABAtenaKanriJohoBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/14�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/17 000001     �ǉ����A���ʍ��ڂ�ݒ肷��
'* 2003/04/14 000002     ��ʂ��L�[�Ɏ擾���郁�\�b�h��ǉ�
'* 2003/05/21 000003     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/28 000004     RDB�A�N�Z�X���O�̏C��
'* 2005/01/17 000005     �����Ǘ����̎��ʃL�[�̃f�[�^�������`�F�b�N���C��(�������p����)
'* 2007/07/27 000006     ����l��\�Ҏ擾���\�b�h�ǉ�(�g�V)
'* 2007/10/03 000007     �X�V���Ɂu���l�v�͉����`�F�b�N���Ȃ��悤�ɕύX(�g�V)
'* 2008/02/13 000008     �������ʕҏW����擾���\�b�h�ǉ��i��Áj
'* 2010/04/16 000009     VS2008�Ή��i��Áj
'* 2010/05/12 000010     �{�ЕM���Ҏ擾�敪�擾���\�b�h�A�O���t���O�擾�敪�擾���\�b�h�ǉ��i��Áj
'* 2011/05/18 000011     �{���E�ʏ̖��D��ݒ萧��p�����[�^�擾���\�b�h��ǉ��i��Áj
'* 2014/12/18 000012     �yAB21040�z�ԍ����x�@�����擾�@���ߌ����敪�p�����[�^�[�擾���\�b�h��ǉ��i�΍��j
'* 2015/01/05 000013     �yAB21034�z�ԍ����x�@�@�l�ԍ����p�J�n���p�����[�^�[�擾���\�b�h��ǉ��i�΍��j
'* 2015/03/05 000014     �yAB21034�z�ԍ����x�@�@�l�ԍ����p�J�n���̃G���[���b�Z�[�W��ύX�i�΍��j
'* 2018/05/07 000015     �yAB27002�z���l�Ǘ��i�΍��j
'* 2018/05/22 000016     �yAB24011�z�A����Ǘ����ڒǉ��i�΍��j
'* 2020/08/03 000017     �yAB32008�z��[�E���t����l�Ǘ��i�΍��j
'* 2020/08/21 000018     �yAB32006�z��[�E���t�惁���e�i���X�i�΍��j
'* 2020/11/10 000019     �yAB00189�z���p�͏o�����[�Ŏ�ID�Ή��i�{�]�j
'* 2023/12/22 000020     �yAB-0970-1_2�z����GET���t���ڐݒ�Ή�(����)
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports System.Text
Imports Densan.FrameWork.Tools

Public Class ABAtenaKanriJohoBClass
#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_strInsertSQL As String                                            'INSERT�pSQL
    Private m_strUpdateSQL As String                                            'UPDATE�pSQL
    Private m_strDeleteSQL As String                                            'DELETE�pSQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass  'DELETE�p�p�����[�^�R���N�V����

    '*����ԍ� 000006 2007/07/27 �ǉ��J�n
    Private m_strDoitsuHantei_Param As String() = {"10", "07"}             '����l��\�҂̎擾����
    '*����ԍ� 000006 2007/07/27 �ǉ��I��
    '*����ԍ� 000008 2008/02/13 �ǉ��J�n
    Private m_strShimeiKakkoKB_Param As String() = {"10", "15"}            '�������ʕҏW����
    '*����ԍ� 000008 2008/02/13 �ǉ��I��
    '*����ԍ� 000010 2010/05/12 �ǉ��J�n
    Private m_strHonsekiKB_Param As String() = {"10", "18"}                '�{�Ў擾�敪
    Private m_strShoriTeishiKB_Param As String() = {"10", "19"}            '������~�敪�擾�敪
    '*����ԍ� 000010 2010/05/12 �ǉ��I��
    '*����ԍ� 000011 2011/05/18 �ǉ��J�n
    Private m_strHonmyoTsushomeiYusenKB_Param As String() = {"10", "23"}   '�{���ʏ̖��D��敪�擾�敪
    '*����ԍ� 000011 2011/05/18 �ǉ��I��
    '*����ԍ� 000019 2020/11/10 �ǉ��J�n
    Private m_strHenkyakuFuyoGyomuCD_Param As String() = {"10", "46"}      ' �Ǝ������@���p�͏o���ʔ[�ŕԋp�s�v�Ɩ�
    '*����ԍ� 000019 2020/11/10 �ǉ��I��
    '*����ԍ� 000012 2014/12/18 �ǉ��J�n
    Private m_strMyNumberChokkinSearchKB_Param() As String = {"35", "29"}   ' �ԍ����x�@�����擾�@���ߌ����敪
    '*����ԍ� 000012 2014/12/18 �ǉ��I��
    '*����ԍ� 000013 2015/01/05 �ǉ��J�n
    Private m_strHojinBangoRiyoKaishiYMD_Param() As String = {"35", "30"}   ' �ԍ����x�@�@�l�ԍ����p�J�n��
    '*����ԍ� 000013 2015/01/05 �ǉ��I��
    '*����ԍ� 000015 2018/05/07 �ǉ��J�n
    Private m_strJutogaiBikoUmu_Param() As String = {"40", "07"}            ' �����q���������@�Z�o�O���l�L��
    '*����ԍ� 000015 2018/05/07 �ǉ��I��
    '*����ԍ� 000016 2018/05/22 �ǉ��J�n
    Private m_strRenrakusakiKakuchoUmu_Param() As String = {"40", "08"}     ' �����q���������@�A����g���L��
    '*����ԍ� 000016 2018/05/22 �ǉ��I��
    '*����ԍ� 000017 2020/08/03 �ǉ��J�n
    Private m_strDainoSfskBikoUmu_Param() As String = {"40", "15"}          ' ��[�E���t����l�L��
    '*����ԍ� 000017 2020/08/03 �ǉ��I��
    '*����ԍ� 000018 2020/08/21 �ǉ��J�n
    Private m_strZeimokuCDConvertTable_Param() As String = {"10", "40"}     ' �ŖڃR�[�h�ϊ��e�[�u��
    Private m_strDainoSfskMainteShiyoUmu_Param() As String = {"12", "25"}   ' ��[�E���t�惁���e�i���X�g�p�L��
    '*����ԍ� 000018 2020/08/21 �ǉ��I��
    Private m_strUmareYMDHenkan_Param() As String = {"51", "01"}            ' �W�������Ή�����GET ������ϊ����t�i���N�����j
    Private m_strShojoIdobiHenkan_Param() As String = {"51", "02"}          ' �W�������Ή�����GET ������ϊ����t�i�����ٓ����j
    Private m_strCknIdobiHenkan_Param() As String = {"51", "03"}            ' �W�������Ή�����GET ������ϊ����t�i���߈ٓ����j

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaKanriJohoBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h
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

        ' �����o�ϐ��̏�����
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDeleteSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �����Ǘ���񒊏o
    '* 
    '* �\��           Public Overloads Function GetKanriJohoHoshu() As DataSet
    '* 
    '* �@�\�@�@    �@�@�����Ǘ������Y���f�[�^��S���擾����B
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �擾���������Ǘ����̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsAtenaKanriJohoEntity    �C���e���Z���X�FABAtenaKanriJohoEntity
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu() As DataSet
        Const THIS_METHOD_NAME As String = "GetKanriJohoHoshu"          '���̃��\�b�h��
        Dim csAtenaKanriJohoEntity As DataSet                           '�����Ǘ����f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKanriJohoEntity.KANRINENDO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            ' �Ǘ��N�x
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO
            cfUFParameterClass.Value = "0000"
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = "AB"
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000004 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '*����ԍ� 000004 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            csAtenaKanriJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKanriJohoEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csAtenaKanriJohoEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����Ǘ���񒊏o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String, 
    '*                                                            ByVal strShikibetsuKey As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�����Ǘ������Y���f�[�^��S���擾����B
    '* 
    '* ����           strSHUKey As String           :��ʃL�[
    '*                strShikibetsuKey As String    :���ʃL�[
    '* 
    '* �߂�l         �擾���������Ǘ����̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsAtenaKanriJohoEntity    �C���e���Z���X�FABAtenaKanriJohoEntity
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String, ByVal strShikibetsuKey As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetKanriJohoHoshu(Overloads)"          '���̃��\�b�h��
        Dim csAtenaKanriJohoEntity As DataSet                           '�����Ǘ����f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKanriJohoEntity.KANRINENDO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SHUKEY)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_SHUKEY)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SHIKIBETSUKEY)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            ' �Ǘ��N�x
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO
            cfUFParameterClass.Value = "0000"
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = "AB"
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��ʃL�[
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHUKEY
            cfUFParameterClass.Value = strSHUKey
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ���ʃL�[
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY
            cfUFParameterClass.Value = strShikibetsuKey
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000004 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '*����ԍ� 000004 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            csAtenaKanriJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKanriJohoEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csAtenaKanriJohoEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����Ǘ���񒊏o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�����Ǘ������Y���f�[�^��S���擾����B
    '* 
    '* ����           strSHUKey As String           :��ʃL�[
    '* 
    '* �߂�l         �擾���������Ǘ����̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsAtenaKanriJohoEntity    �C���e���Z���X�FABAtenaKanriJohoEntity
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu(ByVal strSHUKey As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetKanriJohoHoshu(Overloads)"          '���̃��\�b�h��
        Dim csAtenaKanriJohoEntity As DataSet                           '�����Ǘ����f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKanriJohoEntity.KANRINENDO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SHUKEY)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKanriJohoEntity.KEY_SHUKEY)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKanriJohoEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            ' �Ǘ��N�x
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO
            cfUFParameterClass.Value = "0000"
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = "AB"
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��ʃL�[
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHUKEY
            cfUFParameterClass.Value = strSHUKey
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000004 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '*����ԍ� 000004 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            csAtenaKanriJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKanriJohoEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csAtenaKanriJohoEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����Ǘ����ǉ�
    '* 
    '* �\��           Public Function InsertKanriJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �����Ǘ����Ƀf�[�^��ǉ�����B
    '* 
    '* ����           csDataRow As DataRow  :�ǉ��f�[�^
    '* 
    '* �߂�l         �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertKanriJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertKanriJoho"            '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer                                        '�ǉ�����
        Dim strUpdateDateTime As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '�쐬����

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaKanriJohoEntity.TANMATSUID) = m_cfControlData.m_strClientId            '�[���h�c
            csDataRow(ABAtenaKanriJohoEntity.SAKUJOFG) = "0"                                        '�폜�t���O
            csDataRow(ABAtenaKanriJohoEntity.KOSHINCOUNTER) = Decimal.Zero                          '�X�V�J�E���^
            csDataRow(ABAtenaKanriJohoEntity.SAKUSEINICHIJI) = strUpdateDateTime                    '�쐬����
            csDataRow(ABAtenaKanriJohoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId             '�쐬���[�U�[
            csDataRow(ABAtenaKanriJohoEntity.KOSHINNICHIJI) = strUpdateDateTime                     '�X�V����
            csDataRow(ABAtenaKanriJohoEntity.KOSHINUSER) = m_cfControlData.m_strUserId              '�X�V���[�U�[

            ' ���N���X�̃f�[�^�������`�F�b�N���s��
            For Each csDataColumn In csDataRow.Table.Columns
                ' �f�[�^�������`�F�b�N
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*����ԍ� 000004 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strInsertSQL + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")
            '*����ԍ� 000004 2003/08/28 �C���I��

            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����Ǘ����X�V
    '* 
    '* �\��           Public Function UpdateKanriJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �����Ǘ����̃f�[�^���X�V����B
    '* 
    '* ����           csDataRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateKanriJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateKanriJoho"         '���̃��\�b�h��
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000009
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000009
        Dim intUpdCnt As Integer                            '�X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaKanriJohoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '�[���h�c
            csDataRow(ABAtenaKanriJohoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaKanriJohoEntity.KOSHINCOUNTER)) + 1     '�X�V�J�E���^
            csDataRow(ABAtenaKanriJohoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")    '�X�V����
            csDataRow(ABAtenaKanriJohoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaKanriJohoEntity.PREFIX_KEY.RLength) = ABAtenaKanriJohoEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000004 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strUpdateSQL + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")
            '*����ԍ� 000004 2003/08/28 �C���I��

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����Ǘ����폜�i�����j
    '* 
    '* �\��           Public Overloads Function DeleteKanriJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �����Ǘ����̃f�[�^���폜�i�����j����B
    '* 
    '* ����           csDataRow As DataRow      :�폜�f�[�^
    '* 
    '* �߂�l         �폜�i�����j����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteKanriJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteKanriJoho�i�����j"
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000009
        'Dim csDataColumn As DataColumn
        'Dim objErrorStruct As UFErrorStruct                             '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000009
        Dim intDelCnt As Integer                                        '�폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDeleteSQL Is Nothing Or m_strDeleteSQL = String.Empty Or _
                m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaKanriJohoEntity.PREFIX_KEY.RLength) = ABAtenaKanriJohoEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKanriJohoEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000004 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strDeleteSQL + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "�z")
            '*����ԍ� 000004 2003/08/28 �C���I��

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     SQL���̍쐬
    '* 
    '* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL"              '���̃��\�b�h��
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
        Dim strInsertColumn As String                               '�ǉ�SQL�����ڕ�����
        Dim strInsertParam As String                                '�ǉ�SQL���p�����[�^������
        Dim strDeleteSQL As New StringBuilder                       '�폜SQL��������
        Dim strWhere As New StringBuilder                           '�X�V�폜SQL��Where��������

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABAtenaKanriJohoEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' �X�V�폜Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABAtenaKanriJohoEntity.KANRINENDO)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_KANRINENDO)
            strWhere.Append(" AND ")
            strWhere.Append(ABAtenaKanriJohoEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABAtenaKanriJohoEntity.SHUKEY)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_SHUKEY)
            strWhere.Append(" AND ")
            strWhere.Append(ABAtenaKanriJohoEntity.SHIKIBETSUKEY)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY)
            strWhere.Append(" AND ")
            strWhere.Append(ABAtenaKanriJohoEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABAtenaKanriJohoEntity.KEY_KOSHINCOUNTER)

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABAtenaKanriJohoEntity.TABLE_NAME + " SET "

            ' DELETE�i�����j SQL���̍쐬
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABAtenaKanriJohoEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE�i�����j �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL���̍쐬
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            ' INSERT SQL���̃g���~���O
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))
            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += strWhere.ToString

            ' UPDATE,DELETE(����) �R���N�V�����ɃL�[����ǉ�
            ' �Ǘ��N�x
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KANRINENDO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��ʃL�[
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHUKEY
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ���ʃL�[
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_SHIKIBETSUKEY
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABAtenaKanriJohoEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* �@�\�@�@       �����Ǘ����̃f�[�^�������`�F�b�N���s���܂��B
    '* 
    '* ����           strColumnName As String
    '*                strValue As String
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"       '���̃��\�b�h��
        Dim objErrorStruct As UFErrorStruct                         '�G���[��`�\����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strColumnName.ToUpper()
                Case ABAtenaKanriJohoEntity.SHICHOSONCD                 '�s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KYUSHICHOSONCD              '���s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KANRINENDO                  '�Ǘ��N�x
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KANRINENDO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.GYOMUCD                     '�Ɩ��R�[�h
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_GYOMUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SHUKEY                      '��ʃL�[
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHUKEY)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SHIKIBETSUKEY               '���ʃL�[
                    '*����ԍ� 000005 2006/01/17 �C���J�n
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        'If (Not UFStringClass.CheckNumber(strValue)) Then
                        '*����ԍ� 000005 2006/01/17 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHIKIBETSUKEY)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SHUKEYMEISHO                '��ʃL�[����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHUKEYMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SHIKIBETSUKEYMEISHO         '���ʃL�[����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SHIKIBETSUKEYMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.PARAMETER                   '�p�����[�^
                    '�������Ȃ�
                Case ABAtenaKanriJohoEntity.BIKO                        '���l
                    '*����ԍ� 000007 2007/10/01 �폜�J�n
                    'If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                    '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '    '�G���[��`���擾
                    '    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_BIKO)
                    '    '��O�𐶐�
                    '    Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    'End If
                    '*����ԍ� 000007 2007/10/01 �폜�I��
                Case ABAtenaKanriJohoEntity.RESERVE                     '���U�[�u
                    '�������Ȃ�
                Case ABAtenaKanriJohoEntity.TANMATSUID                  '�[��ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SAKUJOFG                    '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KOSHINCOUNTER               '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SAKUSEINICHIJI              '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.SAKUSEIUSER                 '�쐬���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KOSHINNICHIJI               '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKanriJohoEntity.KOSHINUSER                  '�X�V���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKANRIJOHOB_RDBDATATYPE_KOSHINUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
            End Select

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException
        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException
        End Try
    End Sub

    '*����ԍ� 000006 2007/07/27 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ����l��\�Ҏ擾�̔���p�����[�^�擾
    '* 
    '* �\��           Public Function GetDoitsuHantei_Param() As DataSet
    '* 
    '* �@�\           ����l��\�Ҏ擾�̔���p�����[�^���擾����
    '* 
    '* ����           strShichosonCD As String : �s�����R�[�h
    '* 
    '* �߂�l         String : 
    '************************************************************************************************
    Public Function GetDoitsuHantei_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetDoitsuHantei_Param"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDS = GetKanriJohoHoshu(m_strDoitsuHantei_Param(0), m_strDoitsuHantei_Param(1))

            '�擾�f�[�^�̃`�F�b�N
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                '���R�[�h�����݂��Ȃ��ꍇ�́A�{�l���̎擾�Ƃ���
                strRet = ABConstClass.PRM_HONNIN
            ElseIf CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = ABConstClass.PRM_DAIHYO Then
                '�p�����[�^������l��\�Ҏ擾�̏ꍇ�́A����l��\�҂̎擾�Ƃ���
                strRet = ABConstClass.PRM_DAIHYO
            Else
                '��L�ȊO�́A�{�l���̎擾�Ƃ���
                strRet = ABConstClass.PRM_HONNIN
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Function
    '*����ԍ� 000006 2007/07/27 �ǉ��I��
    '*����ԍ� 000008 2008/02/13 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �������ʕҏW����p�����[�^�擾
    '* 
    '* �\��           Public Function GetShimeiKakkoKB_Param() As DataSet
    '* 
    '* �@�\           �������ʕҏW����̔���p�����[�^���擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         String : 
    '************************************************************************************************
    Public Function GetShimeiKakkoKB_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetShimeiKakkoKB_Param"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDS = GetKanriJohoHoshu(m_strShimeiKakkoKB_Param(0), m_strShimeiKakkoKB_Param(1))

            '�擾�f�[�^�̃`�F�b�N
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                '���R�[�h�����݂��Ȃ��ꍇ�́A�W���Ƃ���
                strRet = "0"
            Else
                '���R�[�h�����݂���ꍇ�́A�Ǘ������Z�b�g����
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try
    End Function
    '*����ԍ� 000008 2008/02/13 �ǉ��I��
    '*����ԍ� 000010 2010/05/12 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �{�Ў擾�敪�p�����[�^�擾
    '* 
    '* �\��           Public Function GetHonsekiKB_Param() As DataSet
    '* 
    '* �@�\           �{�Ў擾�敪�p�����[�^���擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         String 
    '************************************************************************************************
    Public Function GetHonsekiKB_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetHonsekiKB_Param"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDS = GetKanriJohoHoshu(m_strHonsekiKB_Param(0), m_strHonsekiKB_Param(1))

            '�擾�f�[�^�̃`�F�b�N
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                '���R�[�h�����݂��Ȃ��ꍇ�́A�󔒂Ƃ���
                strRet = "0"
            Else
                '���R�[�h�����݂���ꍇ�́A�Ǘ������Z�b�g����
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try
    End Function
    '************************************************************************************************
    '* ���\�b�h��     ������~�敪�擾�敪�p�����[�^�擾
    '* 
    '* �\��           Public Function GetShoriteishiKB_Param() As DataSet
    '* 
    '* �@�\           ������~�敪�擾�敪�p�����[�^���擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         String 
    '************************************************************************************************
    Public Function GetShoriteishiKB_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetShoriteishiKB_Param"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDS = GetKanriJohoHoshu(m_strShoriTeishiKB_Param(0), m_strShoriTeishiKB_Param(1))

            '�擾�f�[�^�̃`�F�b�N
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                '���R�[�h�����݂��Ȃ��ꍇ�́A�󔒂Ƃ���
                strRet = "0"
            Else
                '���R�[�h�����݂���ꍇ�́A�Ǘ������Z�b�g����
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try
    End Function
    '*����ԍ� 000010 2010/05/12 �ǉ��I��

    '*����ԍ� 000011 2011/05/18 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �{���E�ʏ̖��D�搧��敪�p�����[�^�擾
    '* 
    '* �\��           Public Function GetHonmyoTsushomeiYusenKB_Param() As String
    '* 
    '* �@�\           �{���E�ʏ̖��D�搧��敪�p�����[�^���擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         String 
    '************************************************************************************************
    Public Function GetHonmyoTsushomeiYusenKB_Param() As String
        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetHonmyoTsushomeiYusenKB_Param"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDS = GetKanriJohoHoshu(m_strHonmyoTsushomeiYusenKB_Param(0), m_strHonmyoTsushomeiYusenKB_Param(1))

            '�擾�f�[�^�̃`�F�b�N
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                '���R�[�h�����݂��Ȃ��ꍇ�́A�󔒂Ƃ���
                strRet = "0"
            Else
                '���R�[�h�����݂���ꍇ�́A�Ǘ������Z�b�g����
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try
    End Function
    '*����ԍ� 000011 2011/05/18 �ǉ��I��

    '*����ԍ� 000012 2014/12/18 �ǉ��J�n
#Region "�ԍ����x�@�����擾�@���ߌ����敪�@�p�����[�^�[�擾"

    ''' <summary>
    ''' �ԍ����x�@�����擾�@���ߌ����敪�@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>�ԍ����x�@�����擾�@���ߌ����敪</returns>
    ''' <remarks></remarks>
    Public Function GetMyNumberChokkinSearchKB_Param() As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csDataSet As DataSet
        Dim strResult As String

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDataSet = GetKanriJohoHoshu(m_strMyNumberChokkinSearchKB_Param(0), m_strMyNumberChokkinSearchKB_Param(1))

            ' �擾�f�[�^�̃`�F�b�N
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                strResult = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                Select Case strResult

                    Case ABEnumDefine.MyNumberChokkinSearchKB.CKIN.GetHashCode.ToString, _
                         ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode.ToString
                        ' noop
                    Case Else

                        ' �K��l�ȊO�i�l�Ȃ����܂ށj�̏ꍇ�́A"2"�i�������܂߂Č����j��ݒ肷��B
                        strResult = ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode.ToString

                End Select

            Else

                ' ���R�[�h�����݂��Ȃ��ꍇ�́A"2"�i�������܂߂Č����j��ݒ肷��B
                strResult = ABEnumDefine.MyNumberChokkinSearchKB.RRK.GetHashCode.ToString

            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + csExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return strResult

    End Function

#End Region
    '*����ԍ� 000012 2014/12/18 �ǉ��I��

    '*����ԍ� 000013 2015/01/05 �ǉ��J�n
#Region "�ԍ����x�@�@�l�ԍ����p�J�n���@�p�����[�^�[�擾"

    ''' <summary>
    ''' �ԍ����x�@�@�l�ԍ����p�J�n���@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>�ԍ����x�@�@�l�ԍ����p�J�n��</returns>
    ''' <remarks></remarks>
    Public Function GetHojinBangoRiyoKaishiYMD_Param() As String

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csDataSet As DataSet
        Dim strResult As String
        Dim cfDate As UFDateClass                           ' ���t�N���X
        Dim cfErrorClass As UFErrorClass                    ' �G���[�N���X
        Dim cfErrorStruct As UFErrorStruct                  ' �G���[��`�\����

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDataSet = GetKanriJohoHoshu(m_strHojinBangoRiyoKaishiYMD_Param(0), m_strHojinBangoRiyoKaishiYMD_Param(1))

            ' �p�����[�^�[�l�̎��o��
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then
                strResult = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString
            Else
                strResult = String.Empty
            End If

            ' �擾�f�[�^�̃`�F�b�N
            cfDate = New UFDateClass(m_cfConfigDataClass, UFDateSeparator.None, UFDateFillType.Zero, UFEraType.Number, False, False)
            cfDate.p_strDateValue = strResult
            If (cfDate.CheckDate = True) Then
                strResult = cfDate.p_strSeirekiYMD
            Else

                ' ���ݓ��ȊO�̏ꍇ�́A�G���[�Ƃ���B�i�Ƌ��̓����ɏ���������B�j
                '*����ԍ� 000014 2015/03/05 �C���J�n
                'cfErrorClass = New UFErrorClass
                'cfErrorStruct = cfErrorClass.GetErrorStruct(URErrorClass.URE001053)
                'Throw New Exception(cfErrorStruct.m_strErrorMessage)
                cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                cfErrorStruct = cfErrorClass.GetErrorStruct(ABErrorClass.ABE003144)
                Throw New Exception(String.Format("{0} �����Ǘ���� �F ��ʃL�[�y{1}�z�A���ʃL�[�y{2}�z", _
                                                  cfErrorStruct.m_strErrorMessage, _
                                                  m_strHojinBangoRiyoKaishiYMD_Param(0), _
                                                  m_strHojinBangoRiyoKaishiYMD_Param(1)))
                '*����ԍ� 000014 2015/03/05 �C���I��

            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + csExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return strResult

    End Function

#End Region
    '*����ԍ� 000013 2015/01/05 �ǉ��I��

    '*����ԍ� 000015 2018/05/07 �ǉ��J�n
#Region "�����q���������@�Z�o�O���l�L���@�p�����[�^�[�擾"

    ''' <summary>
    ''' �����q���������@�Z�o�O���l�L���@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>�����q���������@�Z�o�O���l�L��</returns>
    ''' <remarks></remarks>
    Public Function GetJutogaiBikoUmu_Param() As Boolean

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim blnResult As Boolean = False
        Dim csDataSet As DataSet
        Dim strParameter As String

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDataSet = GetKanriJohoHoshu(m_strJutogaiBikoUmu_Param(0), m_strJutogaiBikoUmu_Param(1))

            ' �擾�f�[�^�̃`�F�b�N
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                If (strParameter.Trim = "1") Then
                    blnResult = True
                Else
                    blnResult = False
                End If

            Else
                blnResult = False
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + csExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return blnResult

    End Function

#End Region
    '*����ԍ� 000015 2018/05/07 �ǉ��I��

    '*����ԍ� 000016 2018/05/22 �ǉ��J�n
#Region "�����q���������@�A����g���L���@�p�����[�^�[�擾"

    ''' <summary>
    ''' �����q���������@�A����g���L���@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>�����q���������@�A����g���L��</returns>
    ''' <remarks></remarks>
    Public Function GetRenrakusakiKakuchoUmu_Param() As Boolean

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim blnResult As Boolean = False
        Dim csDataSet As DataSet
        Dim strParameter As String

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDataSet = GetKanriJohoHoshu(m_strRenrakusakiKakuchoUmu_Param(0), m_strRenrakusakiKakuchoUmu_Param(1))

            ' �擾�f�[�^�̃`�F�b�N
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                If (strParameter.Trim = "1") Then
                    blnResult = True
                Else
                    blnResult = False
                End If

            Else
                blnResult = False
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + csExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return blnResult

    End Function

#End Region
    '*����ԍ� 000016 2018/05/22 �ǉ��I��

    '*����ԍ� 000017 2020/08/03 �ǉ��J�n
#Region "��[�E���t����l�L���@�p�����[�^�[�擾"

    ''' <summary>
    ''' ��[�E���t����l�L���@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>��[�E���t����l�L��</returns>
    ''' <remarks></remarks>
    Public Function GetDainoSfskBikoUmu_Param() As Boolean

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim blnResult As Boolean = False
        Dim csDataSet As DataSet
        Dim strParameter As String

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDataSet = GetKanriJohoHoshu(m_strDainoSfskBikoUmu_Param(0), m_strDainoSfskBikoUmu_Param(1))

            ' �擾�f�[�^�̃`�F�b�N
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                If (strParameter.Trim = "1") Then
                    blnResult = True
                Else
                    blnResult = False
                End If

            Else
                blnResult = False
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + csExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return blnResult

    End Function

#End Region
    '*����ԍ� 000017 2020/08/03 �ǉ��I��

    '*����ԍ� 000018 2020/08/21 �ǉ��J�n
#Region "�ŖڃR�[�h�ϊ��e�[�u���@�p�����[�^�[�擾"

    ''' <summary>
    ''' �ŖڃR�[�h�ϊ��e�[�u���@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>�ŖڃR�[�h�ϊ��e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetZeikokuCDConvertTable_Param() As Hashtable

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csResult As Hashtable
        Dim csDataSet As DataSet
        Dim strParameter As String
        Dim a_strParameter() As String
        Dim a_strValue() As String

        Const SEPARATOR_SLASH As Char = "/"c
        Const SEPARATOR_COMMA As Char = ","c

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �ԐM�I�u�W�F�N�g�̃C���X�^���X��
            csResult = New Hashtable

            ' �Ǘ���񂩂�f�[�^���擾
            csDataSet = GetKanriJohoHoshu(m_strZeimokuCDConvertTable_Param(0), m_strZeimokuCDConvertTable_Param(1))

            ' �擾�f�[�^�̃`�F�b�N
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                ' �p�����[�^�[���擾
                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                ' �X���b�V���ŋ�؂�
                a_strParameter = strParameter.Split(SEPARATOR_SLASH)

                ' �Ɩ��������[�v
                For Each strValue As String In a_strParameter

                    ' �J���}�ŋ�؂�
                    a_strValue = strValue.Split(SEPARATOR_COMMA)

                    ' ���ڐ������[�v
                    If (a_strValue.Count > 1) Then

                        ' �d���`�F�b�N���s���Ȃ���A�n�b�V���֒ǉ�����
                        If (csResult.ContainsKey(a_strValue(0)) = True) Then
                            ' noop
                        Else
                            csResult.Add(a_strValue(0), a_strValue(1))
                        End If

                    Else
                        ' noop
                    End If

                Next strValue

            Else
                ' noop
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")

            ' ���[�j���O���X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + csExp.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return csResult

    End Function

#End Region

#Region "��[�E���t�惁���e�i���X�g�p�L���@�p�����[�^�[�擾"

    ''' <summary>
    ''' ��[�E���t�惁���e�i���X�g�p�L���@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>��[�E���t�惁���e�i���X�g�p�L��</returns>
    ''' <remarks></remarks>
    Public Function GetDainoSfskMainteShiyoUmu_Param() As Boolean

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim blnResult As Boolean
        Dim csDataSet As DataSet
        Dim strParameter As String

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �ԐM�I�u�W�F�N�g�̏�����
            blnResult = False

            ' �Ǘ���񂩂�f�[�^���擾
            csDataSet = GetKanriJohoHoshu(m_strDainoSfskMainteShiyoUmu_Param(0), m_strDainoSfskMainteShiyoUmu_Param(1))

            ' �擾�f�[�^�̃`�F�b�N
            If (csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count > 0) Then

                ' �p�����[�^�[���擾
                strParameter = csDataSet.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER).ToString

                ' �擾���ʂ𔻒�
                If (strParameter.Trim = "1") Then
                    blnResult = True
                Else
                    blnResult = False
                End If

            Else
                ' noop
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")

            ' ���[�j���O���X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + csExp.Message + "�z")

            ' �V�X�e���G���[���X���[����
            Throw

        End Try

        Return blnResult

    End Function

#End Region
    '*����ԍ� 000018 2020/08/21 �ǉ��I��

    '*����ԍ� 000019 2020/11/10 �ǉ��J�n
#Region "�Ǝ������@���p�͏o���ʔ[�ŕԋp�s�v�Ɩ��@�p�����[�^�[�擾"

    ''' <summary>
    ''' �Ǝ������@���p�͏o���ʔ[�ŕԋp�s�v�Ɩ��@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>�Ǝ������@���p�͏o���ʔ[�ŕԋp�s�v�Ɩ�</returns>
    ''' <remarks></remarks>
    Public Function GetHenkyakuFuyoGyomuCD_Param() As String

        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetHenkyakuFuyoGyomuCD_Param"


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDS = GetKanriJohoHoshu(m_strHenkyakuFuyoGyomuCD_Param(0), m_strHenkyakuFuyoGyomuCD_Param(1))

            '�擾�f�[�^�̃`�F�b�N
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                '���R�[�h�����݂��Ȃ��ꍇ�́A�󔒂Ƃ���
                strRet = ""
            Else
                '���R�[�h�����݂���ꍇ�́A�Ǘ������Z�b�g����
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw

        End Try
    End Function

#End Region
    '*����ԍ� 000019 2020/11/10 �ǉ��I��

#Region "�W�������Ή�����GET�@������ϊ����t�i���N�����j�@�p�����[�^�[�擾"

    ''' <summary>
    ''' �W�������Ή�����GET�@������ϊ����t�i���N�����j�@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>�W�������Ή�����GET�@������ϊ����t�i���N�����j</returns>
    ''' <remarks></remarks>
    Public Function GetUmareYMDHenkanHizuke_Param() As String

        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetUmareYMDHenkanHizuke_Param"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDS = GetKanriJohoHoshu(m_strUmareYMDHenkan_Param(0), m_strUmareYMDHenkan_Param(1))

            '�擾�f�[�^�̃`�F�b�N
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                '���R�[�h�����݂��Ȃ��ꍇ�́A�󔒂Ƃ���
                strRet = String.Empty
            Else
                '���R�[�h�����݂���ꍇ�́A�Ǘ������Z�b�g����
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

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
    End Function

#End Region

#Region "�W�������Ή�����GET�@������ϊ����t�i�����ٓ����j�@�p�����[�^�[�擾"

    ''' <summary>
    ''' �W�������Ή�����GET�@������ϊ����t�i�����ٓ����j�@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>�W�������Ή�����GET�@������ϊ����t�i�����ٓ����j</returns>
    ''' <remarks></remarks>
    Public Function GetShojoIdobiHenkanHizuke_Param() As String

        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetShojoIdobiHenkanHizuke_Param"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDS = GetKanriJohoHoshu(m_strShojoIdobiHenkan_Param(0), m_strShojoIdobiHenkan_Param(1))

            '�擾�f�[�^�̃`�F�b�N
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                '���R�[�h�����݂��Ȃ��ꍇ�́A�󔒂Ƃ���
                strRet = String.Empty
            Else
                '���R�[�h�����݂���ꍇ�́A�Ǘ������Z�b�g����
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

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
    End Function

#End Region

#Region "�W�������Ή�����GET�@������ϊ����t�i���߈ٓ����j�@�p�����[�^�[�擾"

    ''' <summary>
    ''' �W�������Ή�����GET�@������ϊ����t�i���߈ٓ����j�@�p�����[�^�[�擾
    ''' </summary>
    ''' <returns>�W�������Ή�����GET�@������ϊ����t�i���߈ٓ����j</returns>
    ''' <remarks></remarks>
    Public Function GetCknIdobiHenkanHizuke_Param() As String

        Dim csDS As DataSet
        Dim strRet As String
        Const THIS_METHOD_NAME As String = "GetCknIdobiHenkanHizuke_Param"

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Ǘ���񂩂�f�[�^���擾
            csDS = GetKanriJohoHoshu(m_strCknIdobiHenkan_Param(0), m_strCknIdobiHenkan_Param(1))

            '�擾�f�[�^�̃`�F�b�N
            If (csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count) = 0 Then
                '���R�[�h�����݂��Ȃ��ꍇ�́A�󔒂Ƃ���
                strRet = String.Empty
            Else
                '���R�[�h�����݂���ꍇ�́A�Ǘ������Z�b�g����
                strRet = CStr(csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER))
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Return strRet

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
    End Function

#End Region
#End Region

End Class
