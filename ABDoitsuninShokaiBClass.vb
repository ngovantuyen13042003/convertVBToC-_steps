'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        ����l�Ɖ�c�`(ABDoitsuninShokaiBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/05/01�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/08/28 000001     RDB�A�N�Z�X���O�̏C��
'* 2004/01/19 000002     ���s�����R�[�h�̒ǉ��ɔ����C��     
'* 2007/05/22 000003     �����f�[�^��ʂ̒ǉ��ɔ����C��(���N�������ꉻ�̔���p�ɒǉ�)
'* 2007/07/10 000004     DB�������g���Ή��C���������g������DB�ɑΉ����邽�߂ɃJ�����쐬����MaxLength�l�C���i����j
'* 2014/09/01 000005     �yAB21010�z�l�ԍ����x�Ή��i�≺�j
'* 2022/12/16 000006    �yAB-8010�z�Z���R�[�h���уR�[�h15���Ή�(����)
'* 2023/12/18 000007    �yAB-7010-1�z����l�ݒ���擾�Ή�(����)
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
Imports Densan.Common

Public Class ABDoitsuninShokaiBClass
#Region "�����o�ϐ�"
    '�����o�ϐ��̒�`
    Private m_cfLog As UFLogClass                           ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdb As UFRdbClass                           ' �q�c�a�N���X

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABDoitsuninShokaiBClass"
#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfUFControlData As UFControlData,
    '*                                ByVal cfUFConfigDataClass As UFConfigDataClass,
    '*                                ByVal cfUFRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfUFControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                 cfUFConfigDataClass As UFConfigDataClass : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '*                 cfUFRdbClass As UFRdbClass               : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass, ByVal cfRdb As UFRdbClass)

        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigData
        m_cfRdb = cfRdb

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLog = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     ����l�O���[�v�������o
    '* 
    '* �\��           Public Function GetDoitsuninAtena(ByVal strDoitsuninShikibetsuCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��������l���Y���f�[�^��S���擾����B
    '* 
    '* ����           strDoitsuninShikibetsuCD As String      :����l���ʃR�[�h
    '* 
    '* �߂�l         �擾������������l�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsGappeiDoitsuninEntity    �C���e���Z���X�FABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsuninAtena(ByVal strDoitsuninShikibetsuCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninAtena"          ' ���̃��\�b�h��
        Dim csGappeiDoitsuninEntity As DataSet                          ' ��������l�f�[�^
        Dim strSQL As New StringBuilder()                               ' SQL��������
        Dim cfParameter As UFParameterClass                             ' �p�����[�^�N���X
        Dim cfParameterCollection As UFParameterCollectionClass         ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(".*,")
            strSQL.Append(ABAtenaEntity.TABLE_NAME)
            strSQL.Append(".* FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(" LEFT OUTER JOIN ")
            strSQL.Append(ABAtenaEntity.TABLE_NAME)
            strSQL.Append(" ON ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append("=")
            strSQL.Append(ABAtenaEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABAtenaEntity.JUMINCD)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            strSQL.Append("=")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABAtenaEntity.JUTOGAIYUSENKB)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaEntity.KEY_JUTOGAIYUSENKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(".")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfParameterCollection = New UFParameterCollectionClass()
            ' ���������̃p�����[�^���쐬

            ' ����l���ʃR�[�h
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
            cfParameter.Value = strDoitsuninShikibetsuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfParameterCollection.Add(cfParameter)

            ' �Z�o�O�D��敪
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABAtenaEntity.KEY_JUTOGAIYUSENKB
            cfParameter.Value = "1"
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfParameterCollection.Add(cfParameter)

            '*����ԍ� 000001 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLog.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLog.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfParameterCollection) + "�z")
            '*����ԍ� 000001 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            csGappeiDoitsuninEntity = m_cfRdb.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)


            ' �f�o�b�O���O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csGappeiDoitsuninEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ����l�f�[�^�X�L�[�}�쐬
    '* 
    '* �\��           Public Function GetSchemaDoitsuninData() As DataSet
    '* 
    '* �@�\�@�@       ����l�f�[�^�̃X�L�[�}���쐬����B
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         ABDoitsuninDataEntity(DataSet) : ����l�f�[�^
    '************************************************************************************************
    Public Function GetSchemaDoitsuninData() As DataSet
        Const THIS_METHOD_NAME As String = "GetSchemaDoitsuninData"
        Dim csDoitsuninDataEntity As DataSet                ' ����l�f�[�^�Z�b�g
        Dim csDoitsuninDataTable As DataTable               ' ����l�f�[�^�e�[�u��
        Dim csDataColumn As DataColumn                      ' �f�[�^�J����

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ��������l�̃e�[�u���X�L�[�}���擾����
            csDoitsuninDataEntity = m_cfRdb.GetTableSchema(ABGappeiDoitsuninEntity.TABLE_NAME)

            ' �e�[�u������ύX����
            csDoitsuninDataTable = csDoitsuninDataEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME)
            csDoitsuninDataTable.TableName = ABDoitsuninDataEntity.TABLE_NAME

            '**
            '* �\���p�J������ǉ�����
            '*
            ' �\���p���(�Z�����)
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_HENSHUSHUBETSURYOKU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' �\���p���N����
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_UMAREHYOJIWMD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 11
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' �\���p����
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_SEIBETSU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 1
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' �\���p�����i���́j
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_HENSHUKANJISHIMEI, System.Type.GetType("System.String"))
            '* ����ԍ� 000004 2007/07/10 �C���J�n
            csDataColumn.MaxLength = 240
            'csDataColumn.MaxLength = 40
            '* ����ԍ� 000004 2007/07/10 �C���I��
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' �\���p�Z��
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_HENSHUJUSHO, System.Type.GetType("System.String"))
            '* ����ԍ� 000004 2007/07/10 �C���J�n
            csDataColumn.MaxLength = 160
            'csDataColumn.MaxLength = 60
            '* ����ԍ� 000004 2007/07/10 �C���I��
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' �\���p�s����
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_GYOSEIKUMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 30
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' �\���p���уR�[�h
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_STAICD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 15
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            '*����ԍ� 000002 2003/08/28 �C���J�n
            ' �\���p���уR�[�h
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_KYUSHICHOSONCD, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 6
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            '*����ԍ� 000002 2003/08/28 �C���I��
            ' �\���p�{�l�敪
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.DISP_HONNINKBMEI, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 3
            csDoitsuninDataTable.Columns.Add(csDataColumn)

            ' ����ԍ� 000003 2007/05/22 �ǉ��J�n
            ' �����f�[�^���
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.ATENADATASHU, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' ����ԍ� 000003 2007/05/22 �ǉ��I��

            ' ����ԍ� 000005 2014/09/01 �ǉ��J�n
            ' �l�ԍ�
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.MYNUMBER, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 13
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' �����f�[�^�敪
            csDataColumn = New DataColumn(ABDoitsuninDataEntity.ATENADATAKB, System.Type.GetType("System.String"))
            csDataColumn.MaxLength = 2
            csDoitsuninDataTable.Columns.Add(csDataColumn)
            ' ����ԍ� 000005 2014/09/01 �ǉ��I��

            ' �f�o�b�O�I�����O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
            ' UFAppException���X���[����
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

        Return csDoitsuninDataEntity

    End Function

#Region "����l�擾"
    '************************************************************************************************
    '* ���\�b�h��     ����l�擾
    '* 
    '* �\��           Public Function GetDoitsuninData_JuminCD(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�Z���R�[�h�w��œ���l���擾����B
    '* 
    '* ����           strJuminCD As String      :�Z���R�[�h
    '* 
    '* �߂�l         �擾������������l�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsGappeiDoitsuninEntity    �C���e���Z���X�FABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsuninData_JuminCD(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninData_JuminCD"          ' ���̃��\�b�h��
        Dim csGappeiDoitsuninEntity As DataSet                          ' ��������l�f�[�^
        Dim strSQL As New StringBuilder()                               ' SQL��������
        Dim cfParameter As UFParameterClass                             ' �p�����[�^�N���X
        Dim cfParameterCollection As UFParameterCollectionClass         ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            With strSQL
                .Append("SELECT * FROM ")
                .Append(ABGappeiDoitsuninEntity.TABLE_NAME)

                ' WHERE������
                .Append(" WHERE ")
                .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                .Append(" = (SELECT ")
                .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                .Append(" FROM ")
                .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABGappeiDoitsuninEntity.JUMINCD)
                .Append(" = ")
                .Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
                .Append(" AND ")
                .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                .Append(" <> '1')")
                .Append(" AND ")
                .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                .Append(" <> '1'")
                .Append(" ORDER BY ")
                .Append(ABGappeiDoitsuninEntity.JUMINCD)
            End With

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfParameterCollection = New UFParameterCollectionClass()
            ' ���������̃p�����[�^���쐬

            ' ����l���ʃR�[�h
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfParameter.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfParameterCollection.Add(cfParameter)

            ' RDB�A�N�Z�X���O�o��
            m_cfLog.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfParameterCollection) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csGappeiDoitsuninEntity = m_cfRdb.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)


            ' �f�o�b�O���O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csGappeiDoitsuninEntity

    End Function
#End Region

#Region "����l���Ҏ擾"
    '************************************************************************************************
    '* ���\�b�h��     ����l���Ҏ擾
    '* 
    '* �\��           Public Function GetDoitsuninKohoshaData(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�Z���R�[�h�w��œ���l���҂��擾����B
    '* 
    '* ����           strJuminCD As String      :�Z���R�[�h
    '* 
    '* �߂�l         �擾��������l���҂̃f�[�^�iDataSet�j
    '*                   �\���FcsResultDS    �C���e���Z���X�FABDoitsuninKohoshaEntity
    '************************************************************************************************
    Public Function GetDoitsuninKohoshaData(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninKohoshaData"          ' ���̃��\�b�h��
        Dim csResultDS As DataSet                                       ' ����l���҃f�[�^
        Dim strSQL As New StringBuilder()                               ' SQL��������
        Dim cfParameter As UFParameterClass                             ' �p�����[�^�N���X
        Dim cfParameterCollection As UFParameterCollectionClass         ' �p�����[�^�R���N�V�����N���X
        Dim cSearchKey As ABAtenaSearchKey
        Dim cABAtenaB As ABAtenaBClass
        Dim csDataSet As DataSet
        Dim csRow As DataRow
        Dim strUmareYMD As String
        Dim strSearchKanaShimei1 As String
        Dim strSearchKanaShimei2 As String
        Dim strSearchKanaShimei3 As String
        Dim strSearchKanaShimei4 As String
        Dim strSearchKanaShimei5 As String
        Dim strSeibetsuCd As String
        Dim intI As Integer = 0

        Try
            ' �f�o�b�O���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�Ώێ҂̌���
            cSearchKey = New ABAtenaSearchKey
            cSearchKey.p_strJuminCD = strJuminCD
            cSearchKey.p_strJutogaiYusenKB = "1"                                '�Z�o�O�D��
            cABAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdb, ABEnumDefine.AtenaGetKB.SelectAll, True)
            cABAtenaB.m_intHyojunKB = ABEnumDefine.HyojunKB.KB_Hyojun           '�W�����Ή�
            csDataSet = cABAtenaB.GetAtenaBHoshu(1, cSearchKey)

            If (csDataSet Is Nothing) Then
                Return csResultDS
            Else
                If (csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count > 0) Then
                    csRow = csDataSet.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)
                    strUmareYMD = csRow.Item(ABAtenaEntity.UMAREYMD).ToString
                    strSearchKanaShimei1 = csRow.Item(ABAtenaEntity.SEARCHKANASEIMEI).ToString
                    If (csRow.Item(ABAtenaEntity.ATENADATAKB).ToString = ABConstClass.ATENADATAKB_HOJIN) Then
                        strSearchKanaShimei2 = csRow.Item(ABAtenaEntity.SEARCHKANASEI).ToString
                    Else
                        strSearchKanaShimei2 = String.Empty
                    End If
                    strSearchKanaShimei3 = csRow.Item(ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI).ToString
                    strSearchKanaShimei4 = csRow.Item(ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI).ToString
                    strSearchKanaShimei5 = csRow.Item(ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI).ToString
                    strSeibetsuCd = csRow.Item(ABAtenaEntity.SEIBETSUCD).ToString
                Else
                    Return csResultDS
                End If
            End If

            ' SQL���̍쐬
            With strSQL
                .Append(CreateSelect)
                .Append(" FROM ")
                .Append(ABAtenaEntity.TABLE_NAME)
                .Append(" LEFT JOIN ")
                .Append(ABAtenaFZYEntity.TABLE_NAME)
                .AppendFormat(" ON {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                .AppendFormat(" = {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD)
                .AppendFormat(" AND {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB)
                .AppendFormat(" = {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINJUTOGAIKB)
                .Append(" LEFT JOIN ")
                .Append(ABAtenaFZYHyojunEntity.TABLE_NAME)
                .AppendFormat(" ON {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                .AppendFormat(" = {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYEntity.JUMINCD)
                .AppendFormat(" AND {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINJUTOGAIKB)
                .AppendFormat(" = {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.JUMINJUTOGAIKB)

                ' WHERE������
                .Append(" WHERE ")
                .AppendFormat("{0}.{1} = '1'", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUTOGAIYUSENKB)
                .AppendFormat(" AND {0}.{1} <> '1'", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SAKUJOFG)
                .AppendFormat(" AND {0}.{1} <> ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
                .Append(ABAtenaEntity.KEY_JUMINCD)
                .AppendFormat(" AND {0}.{1} = ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.UMAREYMD)
                .Append(ABAtenaEntity.PARAM_UMAREYMD)
                .AppendFormat(" AND {0}.{1} = ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEIBETSUCD)
                .Append(ABAtenaEntity.PARAM_SEIBETSUCD)
                '�����J�i����
                .AppendFormat(" AND (( {0}.{1} <> '' AND ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEIMEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEIMEI)
                intI = 1
                .AppendFormat("{0},", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})", ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString)
                '�����J�i��
                .AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEARCHKANASEI)
                intI = 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})", ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString)
                '�����J�i�O���l��
                .AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAFRNMEI)
                intI = 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString)
                '�����J�i�ʏ̖�
                .AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANATSUSHOMEI)
                intI = 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString)
                '�����J�i���L��
                .AppendFormat(") OR ({0}.{1} <> '' AND ", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                .AppendFormat("{0}.{1} IN(", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SEARCHKANAHEIKIMEI)
                intI = 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0} ,", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
                intI += 1
                .AppendFormat("{0})))", ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString)
            End With

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfParameterCollection = New UFParameterCollectionClass()
            ' ���������̃p�����[�^���쐬

            ' �Z���R�[�h
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABAtenaEntity.KEY_JUMINCD
            cfParameter.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfParameterCollection.Add(cfParameter)

            ' ���N����
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABAtenaEntity.PARAM_UMAREYMD
            cfParameter.Value = strUmareYMD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfParameterCollection.Add(cfParameter)

            ' ���ʃR�[�h
            cfParameter = New UFParameterClass()
            cfParameter.ParameterName = ABAtenaEntity.PARAM_SEIBETSUCD
            cfParameter.Value = strSeibetsuCd
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfParameterCollection.Add(cfParameter)

            ' �����J�i����
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaEntity.PARAM_SEARCHKANASEIMEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfParameterCollection.Add(cfParameter)
            Next

            ' �����J�i��
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaEntity.PARAM_SEARCHKANASEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfParameterCollection.Add(cfParameter)
            Next

            ' �����J�i�O���l��
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAFRNMEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfParameterCollection.Add(cfParameter)
            Next

            ' �����J�i�ʏ̖�
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANATSUSHOMEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfParameterCollection.Add(cfParameter)
            Next

            ' �����J�i���L��
            For intI = 1 To 5
                cfParameter = New UFParameterClass()
                cfParameter.ParameterName = ABAtenaFZYHyojunEntity.PARAM_SEARCHKANAHEIKIMEI + intI.ToString
                Select Case intI
                    Case 1
                        cfParameter.Value = strSearchKanaShimei1
                    Case 2
                        cfParameter.Value = strSearchKanaShimei2
                    Case 3
                        cfParameter.Value = strSearchKanaShimei3
                    Case 4
                        cfParameter.Value = strSearchKanaShimei4
                    Case 5
                        cfParameter.Value = strSearchKanaShimei5
                End Select
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfParameterCollection.Add(cfParameter)
            Next

            ' RDB�A�N�Z�X���O�o��
            m_cfLog.RdbWrite(m_cfControlData,
                                        "�y�N���X��:" + Me.GetType.Name + "�z" +
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                        "�y���s���\�b�h��:GetDataSet�z" +
                                        "�ySQL���e:" + m_cfRdb.GetDevelopmentSQLString(strSQL.ToString, cfParameterCollection) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csResultDS = m_cfRdb.GetDataSet(strSQL.ToString, ABDoitsuninKohoshaEntity.TABLE_NAME, cfParameterCollection)

            ' �f�o�b�O���O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csResultDS

    End Function

#End Region

#Region "SELECT��쐬"
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
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT��̍쐬
            csSELECT.AppendFormat("SELECT {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUMINCD)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.ATENADATAKB)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.ATENADATASHU)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANAMEISHO1)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANJIMEISHO1)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANAMEISHO2)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KANJIMEISHO2)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANATSUSHOMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJITSUSHOMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHONGOKUMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.HONGOKUMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANAHEIKIMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYEntity.TABLE_NAME, ABAtenaFZYEntity.KANJIHEIKIMEI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaFZYHyojunEntity.TABLE_NAME, ABAtenaFZYHyojunEntity.SHIMEIYUSENKB)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.UMAREYMD)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEIBETSUCD)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.SEIBETSU)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUSHO)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KATAGAKI)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.YUBINNO)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.JUSHOCD)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHICD1)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHICD2)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.BANCHICD3)
            csSELECT.AppendFormat(", {0}.{1}", ABAtenaEntity.TABLE_NAME, ABAtenaEntity.KATAGAKICD)

            ' �f�o�b�O���O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csSELECT.ToString

    End Function
#End Region

#End Region

End Class
