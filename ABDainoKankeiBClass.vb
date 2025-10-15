'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        ��[�֌W�c�`(ABDainoKankeiBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2002/12/19�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/14 000001     �������`�F�b�N�́A�󔒂��Ƃ��ď�������
'* 2003/03/27 000002     �G���[�����N���X�̎Q�Ɛ��"AB"�Œ�ɂ���
'* 2003/05/21 000003     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/28 000004     RDB�A�N�Z�X���O�̏C��
'* 2005/01/25 000005     ���x���P�Q�F�i�{��j
'* 2010/04/16  000006      VS2008�Ή��i��Áj
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

Public Class ABDainoKankeiBClass
#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_strInsertSQL As String                        ' INSERT�pSQL
    Private m_strUpdateSQL As String                        ' UPDATE�pSQL
    Private m_strDeleteSQL As String                        ' DELETE�pSQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  ' INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  ' UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass  ' DELETE�p�p�����[�^�R���N�V����

    '* ����ԍ� 000005 2005/01/25 �ǉ��J�n�i�{��j
    Private m_csDainoKankeiCDMSTEntity As DataSet
    '* ����ԍ� 000005 2005/01/25 �ǉ��I���i�{��j

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABDainoKankeiBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h
#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfUFControlData As UFControlData,
    '* �@�@                           ByVal cfUFConfigDataClass As UFConfigDataClass,
    '* �@�@                           ByVal cfUFRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfUFControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@            cfUFConfigDataClass As UFConfigDataClass : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '* �@�@            cfUFRdbClass As UFRdbClass               : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigDataClass As UFConfigDataClass, _
                   ByVal cfRdbClass As UFRdbClass)
        '�����o�ϐ��Z�b�g
        m_cfControlData = cfControlData
        m_cfConfigDataClass = cfConfigDataClass
        m_cfRdbClass = cfRdbClass

        '���O�o�̓N���X�̃C���X�^���X��
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
    '* ���\�b�h��     ��[�֌W�R�[�h�}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetDainoKBHoshu() As DataSet
    '* 
    '* �@�\       �@�@��[�֌W�R�[�h�}�X�^���Y���f�[�^��S���擾����B
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         �擾������[�֌W�R�[�h�}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsDainoKankeiCDMSTEntity    �C���e���Z���X�FABDainoKankeiCDMSTEntity
    '************************************************************************************************
    Public Overloads Function GetDainoKBHoshu() As DataSet
        Const THIS_METHOD_NAME As String = "GetDainoKBHoshu"
        Dim csDainoKankeiCDMSTEntity As DataSet
        Dim strSQL As New StringBuilder()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDainoKankeiCDMSTEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoKankeiCDMSTEntity.TABLE_NAME)

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

        Return csDainoKankeiCDMSTEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[�֌W�R�[�h�}�X�^���o�i�I�[�o�[���[�h�j
    '* 
    '* �\��           Public Overloads Function GetDainoKBHoshu(ByVal strDainoKB As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�w�肳�ꂽ��[�敪�������ɑ�[�֌W�R�[�h�}�X�^�̊Y���f�[�^���擾����B
    '* 
    '* ����           strDainoKB As String  :��[�敪
    '* 
    '* �߂�l         �擾������[�֌W�R�[�h�}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsDainoKankeiCDMSTEntity    �C���e���Z���X�FABDainoKankeiCDMSTEntity
    '************************************************************************************************
    Public Overloads Function GetDainoKBHoshu(ByVal strDainoKB As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetJutogaiBHoshu(String)"
        Dim csDainoKankeiCDMSTEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.DAINOKB)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.KEY_DAINOKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoKankeiCDMSTEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.KEY_DAINOKB
            cfUFParameterClass.Value = strDainoKB

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
            csDainoKankeiCDMSTEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoKankeiCDMSTEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csDainoKankeiCDMSTEntity

    End Function

    '* ����ԍ� 000005 2005/01/25 �ǉ��J�n�i�{��j
    '************************************************************************************************
    '* ���\�b�h��     ��[�֌W�R�[�h�}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetDainoKBHoshu2(ByVal strDainoKB As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�w�肳�ꂽ��[�敪�������ɑ�[�֌W�R�[�h�}�X�^�̊Y���f�[�^���擾����B
    '* 
    '* ����           strDainoKB As String  :��[�敪
    '* 
    '* �߂�l         �擾������[�֌W�R�[�h�}�X�^�̊Y���f�[�^�iDataRow�j
    '*                   �\���FcsDainoKankeiCDMSTEntity    �C���e���Z���X�FABDainoKankeiCDMSTEntity
    '************************************************************************************************
    Public Overloads Function GetDainoKBHoshu2(ByVal strDainoKB As String) As DataRow()
        Const THIS_METHOD_NAME As String = "GetJutogaiBHoshu2(String)"
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDainoKankeiCDMSTEntity As DataSet
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim strSQL As New StringBuilder()
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim cfUFParameterClass As UFParameterClass
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDainoKankeiCDMSTDataRows As DataRow()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (m_csDainoKankeiCDMSTEntity Is Nothing) Then
                ' SQL���̍쐬
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME)
                strSQL.Append(" WHERE ")
                strSQL.Append(ABDainoKankeiCDMSTEntity.SAKUJOFG)
                strSQL.Append(" <> '1'")

                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                            "�y���s���\�b�h��:GetDataSet�z" + _
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
                '*����ԍ� 000004 2003/08/28 �C���I��

                ' SQL�̎��s DataSet�̎擾

                m_csDainoKankeiCDMSTEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoKankeiCDMSTEntity.TABLE_NAME, cfUFParameterCollectionClass)
            End If

            strSQL.RRemove(0, strSQL.RLength)
            strSQL.Append(ABDainoKankeiCDMSTEntity.DAINOKB)
            strSQL.Append(" = '")
            strSQL.Append(strDainoKB)
            strSQL.Append("'")
            csDainoKankeiCDMSTDataRows = m_csDainoKankeiCDMSTEntity.Tables(ABDainoKankeiCDMSTEntity.TABLE_NAME).Select(strSQL.ToString())

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

        Return csDainoKankeiCDMSTDataRows

    End Function
    '* ����ԍ� 000005 2005/01/25 �ǉ��I���i�{��j

    '************************************************************************************************
    '* ���\�b�h��     ��[�֌W�R�[�h�}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertDainoKB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  ��[�֌W�R�[�h�}�X�^�Ƀf�[�^��ǉ�����B
    '* 
    '* ����           csDataRow As DataRow  :�ǉ��f�[�^
    '* 
    '* �߂�l         �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertDainoKB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertDainoKB"
        Dim cfParam As UFParameterClass
        Dim csDataColumn As DataColumn
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intInsCnt As Integer
        Dim strUpdateDateTime As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '�X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          ' �쐬����

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABDainoKankeiCDMSTEntity.TANMATSUID) = m_cfControlData.m_strClientId          ' �[���h�c
            csDataRow(ABDainoKankeiCDMSTEntity.SAKUJOFG) = "0"                                      ' �폜�t���O
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINCOUNTER) = Decimal.Zero                        ' �X�V�J�E���^
            csDataRow(ABDainoKankeiCDMSTEntity.SAKUSEINICHIJI) = strUpdateDateTime                  ' �쐬����
            csDataRow(ABDainoKankeiCDMSTEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId           ' �쐬���[�U�[
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINNICHIJI) = strUpdateDateTime                   ' �X�V����
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINUSER) = m_cfControlData.m_strUserId            ' �X�V���[�U�[

            '���N���X�̃f�[�^�������`�F�b�N���s��
            For Each csDataColumn In csDataRow.Table.Columns
                '�f�[�^�������`�F�b�N
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            '�p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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

        Catch exException As Exception ' �V�X�e���G���[���L���b�`
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
    '* ���\�b�h��     ��[�֌W�R�[�h�}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateDainoKB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  ��[�֌W�R�[�h�}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           csDataRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateDainoKB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateDainoKB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intUpdCnt As Integer
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim strUpdateDateTime As String
        '* corresponds to VS2008 End 2010/04/16 000006

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABDainoKankeiCDMSTEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '�[���h�c
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINCOUNTER) = CDec(csDataRow(ABDainoKankeiCDMSTEntity.KOSHINCOUNTER)) + 1     '�X�V�J�E���^
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")    '�X�V����
            csDataRow(ABDainoKankeiCDMSTEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '�X�V���[�U�[

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength) = ABDainoKankeiCDMSTEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '�f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    '�p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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

        Catch exException As Exception ' �V�X�e���G���[���L���b�`
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
    '* ���\�b�h��     ��[�֌W�R�[�h�}�X�^�폜
    '* 
    '* �\��           Public Function DeleteDainoKB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  ��[�֌W�R�[�h�}�X�^�̃f�[�^���폜����B
    '* 
    '* ����           csDataRow As DataRow  :�폜�f�[�^
    '* 
    '* �߂�l         �폜����(Integer)
    '************************************************************************************************
    Public Function DeleteDainoKB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteDainoKB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intDelCnt As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDeleteSQL Is Nothing Or m_strDeleteSQL = String.Empty Or _
                m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength) = ABDainoKankeiCDMSTEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '�p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "DeleteDainoKB")

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
        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim strInsertColumn As String
        Dim strInsertParam As String
        Dim cfUFParameterClass As UFParameterClass
        Dim strDeleteSQL As New StringBuilder()

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABDainoKankeiCDMSTEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABDainoKankeiCDMSTEntity.TABLE_NAME + " SET "

            ' DELETE SQL���̍쐬
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.TABLE_NAME)
            strDeleteSQL.Append(" WHERE ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.DAINOKB)
            strDeleteSQL.Append(" = ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.KEY_DAINOKB)
            strDeleteSQL.Append(" AND ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.KOSHINCOUNTER)
            strDeleteSQL.Append(" = ")
            strDeleteSQL.Append(ABDainoKankeiCDMSTEntity.KEY_KOSHINCOUNTER)
            m_strDeleteSQL = strDeleteSQL.ToString

            ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

            ' DELETE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass()

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL���̍쐬
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            m_strUpdateSQL += " WHERE " + ABDainoKankeiCDMSTEntity.DAINOKB + " = " + ABDainoKankeiCDMSTEntity.KEY_DAINOKB + " AND " + _
                                          ABDainoKankeiCDMSTEntity.KOSHINCOUNTER + " = " + ABDainoKankeiCDMSTEntity.KEY_KOSHINCOUNTER

            ' UPDATE,DELETE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.KEY_DAINOKB
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoKankeiCDMSTEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

            '�f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "CreateSQL")

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
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* �@�\�@�@       ��[�֌W�R�[�h�}�X�^�̃f�[�^�������`�F�b�N���s���܂��B
    '* 
    '* ����           strColumnName As String
    '*                strValue As String
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strColumnName.ToUpper()
                Case ABDainoKankeiCDMSTEntity.SHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.KYUSHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.DAINOKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_DAINOKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.DAINOKBMEISHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_DAINOKBMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.DAINOKBRYAKUMEI
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_DAINOKBRYAKUMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.TANMATSUID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.SAKUJOFG
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.KOSHINCOUNTER
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.SAKUSEINICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.SAKUSEIUSER
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.KOSHINNICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABDainoKankeiCDMSTEntity.KOSHINUSER
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOKANKEICDMSTB_RDBDATATYPE_KOSHINUSER)
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
        Catch exException As Exception ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException
        End Try
    End Sub
#End Region

End Class
