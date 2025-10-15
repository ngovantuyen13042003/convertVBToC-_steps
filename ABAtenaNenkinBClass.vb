'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �����N���c�`(ABAtenaNenkinBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/06�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/17 000001     �ǉ����A���ʍ��ڂ�ݒ肷��
'* 2003/05/21 000002     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/06/20 000003     �G���[�N���X���G���[���ɲݽ�ݽ������
'* 2003/08/28 000004     RDB�A�N�Z�X���O�̏C��
'* 2003/09/11 000005     �N���ԍ��Ŏ擾���郁�\�b�h�̎d�l�ǉ�
'* 2003/09/24 000006     �t�@�C����`�ύX�ɔ����C��
'* 2003/10/03 000007     �擾���R�E�r�����R�̃`�F�b�N��ANK�ɕύX
'* 2004/11/11 000008     �f�[�^�`�F�b�N���s�Ȃ�Ȃ�
'* 2005/02/16 000009     ���X�|���X���P�F�r�p�k���쐬�̏C��     
'* 2010/04/16 000010     VS2008�Ή��i��Áj
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

Public Class ABAtenaNenkinBClass
#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X
    Private m_strInsertSQL As String                                            'INSERT�pSQL
    Private m_strUpdateSQL As String                                            'UPDATE�pSQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE�p�p�����[�^�R���N�V����

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaNenkinBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h
#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfControlData As UFControlData,
    '* �@�@                           ByVal cfConfigDataClass As UFConfigDataClass,
    '* �@�@                           ByVal cfRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '* �@�@            cfConfigDataClass As UFConfigDataClass : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '* �@�@            cfRdbClass As UFRdbClass               : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
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
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �����N���}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaNenkin(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�����N���}�X�^���Y���f�[�^���擾����B�B
    '* 
    '* ����           strJuminCD As String  :�Z���R�[�h
    '* 
    '* �߂�l         �擾���������N���}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsAtenaNenkinEntity    �C���e���Z���X�FABAtenaNenkinEntity
    '************************************************************************************************
    Public Function GetAtenaNenkin(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaNenkin"
        Dim csAtenaNenkinEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaNenkinEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaNenkinEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaNenkinEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

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
            csAtenaNenkinEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaNenkinEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csAtenaNenkinEntity

    End Function

    '*����ԍ� 000005 2003/09/11 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����N���}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaNenkinBango(ByVal strKsNenknNO As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�����N���}�X�^���Y���f�[�^���擾����B�B
    '* 
    '* ����           strKsNenknNO As String  : ��b�N���ԍ�
    '* 
    '* �߂�l         �擾���������N���}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsAtenaNenkinEntity    �C���e���Z���X�FABAtenaNenkinEntity
    '************************************************************************************************
    Public Function GetAtenaNenkinBango(ByVal strKsNenknNO As String) As DataSet
        Dim csAtenaNenkinEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaNenkinEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaNenkinEntity.KSNENKNNO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaNenkinEntity.PARAM_KSNENKNNO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaNenkinEntity.SAKUJOFG)
            strSQL.Append(" <> ")
            strSQL.Append(ABAtenaNenkinEntity.PARAM_SAKUJOFG)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_KSNENKNNO
            cfUFParameterClass.Value = strKsNenknNO
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_SAKUJOFG
            cfUFParameterClass.Value = "1"
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)


            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csAtenaNenkinEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaNenkinEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception ' �V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csAtenaNenkinEntity

    End Function
    '*����ԍ� 000005 2003/09/11 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �����N���}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaNenkin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �����N���}�X�^�Ƀf�[�^��ǉ�����B
    '* 
    '* ����           csDataRow As DataRow  :�ǉ��f�[�^
    '* 
    '* �߂�l         �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertAtenaNenkin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertAtenaNenkin"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000010
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000010
        Dim intInsCnt As Integer
        Dim strUpdateDateTime As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '*����ԍ� 000009 2005/02/16 �C���J�n
                Call CreateInsertSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*����ԍ� 000009 2005/02/16 �C���I��
            End If

            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '�쐬����

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaNenkinEntity.TANMATSUID) = m_cfControlData.m_strClientId               '�[���h�c
            csDataRow(ABAtenaNenkinEntity.SAKUJOFG) = "0"                                           '�폜�t���O
            csDataRow(ABAtenaNenkinEntity.KOSHINCOUNTER) = Decimal.Zero                             '�X�V�J�E���^
            csDataRow(ABAtenaNenkinEntity.SAKUSEINICHIJI) = strUpdateDateTime                       '�쐬����
            csDataRow(ABAtenaNenkinEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                '�쐬���[�U�[
            csDataRow(ABAtenaNenkinEntity.KOSHINNICHIJI) = strUpdateDateTime                        '�X�V����
            csDataRow(ABAtenaNenkinEntity.KOSHINUSER) = m_cfControlData.m_strUserId                 '�X�V���[�U�[

            '*����ԍ� 000008 2004/11/11 �C���J�n
            ' ���N���X�̃f�[�^�������`�F�b�N���s��
            'For Each csDataColumn In csDataRow.Table.Columns
            '    ' �f�[�^�������`�F�b�N
            '    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            'Next csDataColumn
            '*����ԍ� 000008 2004/11/11 �C���I��

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaNenkinEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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
    '* ���\�b�h��     �����N���}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaNenkin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �����N���}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           csDataRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaNenkin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaNenkin"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000010
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000010
        Dim intUpdCnt As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '*����ԍ� 000009 2005/02/16 �C���J�n
                Call CreateUpdateSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*����ԍ� 000009 2005/02/16 �C���I��
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaNenkinEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABAtenaNenkinEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaNenkinEntity.KOSHINCOUNTER)) + 1       '�X�V�J�E���^
            csDataRow(ABAtenaNenkinEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '�X�V����
            csDataRow(ABAtenaNenkinEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaNenkinEntity.PREFIX_KEY.RLength) = ABAtenaNenkinEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaNenkinEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '*����ԍ� 000008 2004/11/11 �C���J�n
                    ' �f�[�^�������`�F�b�N
                    'CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaNenkinEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaNenkinEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString.Trim)
                    '*����ԍ� 000008 2004/11/11 �C���I��
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaNenkinEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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

    '*����ԍ� 000009 2005/02/16 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     SQL���̍쐬
    '* 
    '* �\��           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\�@�@    �@ INSERTSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateInsertSQL"
        Dim csDataColumn As DataColumn
        Dim strInsertColumn As String
        Dim strInsertParam As String
        Dim cfUFParameterClass As UFParameterClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABAtenaNenkinEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            ' INSERT SQL���̃g���~���O
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))

            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            '�f�o�b�O���O�o��
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

    '************************************************************************************************
    '* ���\�b�h��     SQL���̍쐬
    '* 
    '* �\��           Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\�@�@    �@ UPDATESQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateUpdateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateUpdateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass
        Dim strUpdateWhere As String
        Dim strUpdateParam As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABAtenaNenkinEntity.TABLE_NAME + " SET "
            strUpdateParam = ""
            strUpdateWhere = ""

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns

                '�Z���b�c�i��L�[�j�ƍ쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If Not (csDataColumn.ColumnName = ABAtenaInkanEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABAtenaInkanEntity.SAKUSEIUSER) AndAlso _
                     Not (csDataColumn.ColumnName = ABAtenaInkanEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass()

                    ' SQL���̍쐬
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += " WHERE " + ABAtenaNenkinEntity.JUMINCD + " = " + ABAtenaNenkinEntity.KEY_JUMINCD + " AND " + _
                                          ABAtenaNenkinEntity.KOSHINCOUNTER + " = " + ABAtenaNenkinEntity.KEY_KOSHINCOUNTER

            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            '�f�o�b�O���O�o��
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
    '*����ԍ� 000009 2005/02/16 �ǉ��I��

    '*����ԍ� 000009 2005/02/16 �폜�J�n
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
    '    Dim strInsertColumn As String
    '    Dim strInsertParam As String
    '    Dim cfUFParameterClass As UFParameterClass
    '    Dim strUpdateWhere As String
    '    Dim strUpdateParam As String

    '    Try
    '        ' �f�o�b�O���O�o��
    '        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '        ' SELECT SQL���̍쐬
    '        m_strInsertSQL = "INSERT INTO " + ABAtenaNenkinEntity.TABLE_NAME + " "
    '        strInsertColumn = ""
    '        strInsertParam = ""

    '        ' UPDATE SQL���̍쐬
    '        m_strUpdateSQL = "UPDATE " + ABAtenaNenkinEntity.TABLE_NAME + " SET "
    '        strUpdateParam = ""
    '        strUpdateWhere = ""

    '        ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
    '        m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

    '        ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
    '        m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

    '        ' �p�����[�^�R���N�V�����̍쐬
    '        For Each csDataColumn In csDataRow.Table.Columns
    '            cfUFParameterClass = New UFParameterClass()

    '            ' INSERT SQL���̍쐬
    '            strInsertColumn += csDataColumn.ColumnName + ", "
    '            strInsertParam += ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' SQL���̍쐬
    '            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
    '            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    '            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    '            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    '            cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    '            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        Next csDataColumn

    '        ' INSERT SQL���̃g���~���O
    '        strInsertColumn = strInsertColumn.Trim()
    '        strInsertColumn = strInsertColumn.Trim(CType(",", Char))
    '        strInsertParam = strInsertParam.Trim()
    '        strInsertParam = strInsertParam.Trim(CType(",", Char))

    '        m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

    '        ' UPDATE SQL���̃g���~���O
    '        m_strUpdateSQL = m_strUpdateSQL.Trim()
    '        m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

    '        ' UPDATE SQL����WHERE��̒ǉ�
    '        m_strUpdateSQL += " WHERE " + ABAtenaNenkinEntity.JUMINCD + " = " + ABAtenaNenkinEntity.KEY_JUMINCD + " AND " + _
    '                                      ABAtenaNenkinEntity.KOSHINCOUNTER + " = " + ABAtenaNenkinEntity.KEY_KOSHINCOUNTER

    '        ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_JUMINCD
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaNenkinEntity.KEY_KOSHINCOUNTER
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        '�f�o�b�O���O�o��
    '        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    '    Catch exAppException As UFAppException
    '        ' ���[�j���O���O�o��
    '        m_cfLogClass.WarningWrite(m_cfControlData, _
    '                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                    "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
    '                                    "�y���[�j���O���e:" + exAppException.Message + "�z")
    '        ' ���[�j���O���X���[����
    '        Throw exAppException

    '    Catch exException As Exception ' �V�X�e���G���[���L���b�`
    '        ' �G���[���O�o��
    '        m_cfLogClass.ErrorWrite(m_cfControlData, _
    '                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    '                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    '                                    "�y�G���[���e:" + exException.Message + "�z")
    '        ' �V�X�e���G���[���X���[����
    '        Throw exException

    '    End Try
    'End Sub
    '*����ԍ� 000009 2005/02/16 �폜�I��

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* �@�\�@�@       �����N���}�X�^�̃f�[�^�������`�F�b�N���s���܂��B
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

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()
                Case ABAtenaNenkinEntity.JUMINCD                        '�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SHICHOSONCD                    '�s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KYUSHICHOSONCD                 '���s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KSNENKNNO                      '��b�N���ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KSNENKNNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SKAKSHUTKYMD                   '���i�擾�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSHUTKYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaNenkinEntity.SKAKSHUTKSHU                   '���i�擾���
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSHUTKSHU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SKAKSHUTKRIYUCD                '���i�擾���R�R�[�h
                    '* ����ԍ� 000007 2003/10/03 �C���J�n
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000007 2003/10/03 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSHUTKRIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SKAKSSHTSYMD                   '���i�r���N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSSHTSYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaNenkinEntity.SKAKSSHTSRIYUCD                '���i�r�����R�R�[�h
                    '* ����ԍ� 000007 2003/10/03 �C���J�n
                    'If (Not UFStringClass.CheckNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000007 2003/10/03 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SKAKSSHTSRIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKIGO1                  '�󋋔N���L���P
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKIGO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* ����ԍ� 000006 2003/09/24 �C���J�n
                    'Case ABAtenaNenkinEntity.JKYNENKNSHU1                   '�󋋔N���ԍ��P
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU1)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If
                    'Case ABAtenaNenkinEntity.JKYNENKNNO1                    '�󋋔N����ʂP
                    '    If (Not UFStringClass.CheckANK(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO1)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaNenkinEntity.JKYNENKNNO1                    '�󋋔N���ԍ��P
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNSHU1                   '�󋋔N����ʂP
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* ����ԍ� 000006 2003/09/24 �C���I��

                Case ABAtenaNenkinEntity.JKYNENKNEDABAN1                '�󋋔N���}�ԂP
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNEDABAN1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKB1                    '�󋋔N���敪�P
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKB1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKIGO2                  '�󋋔N���L���Q
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKIGO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* ����ԍ� 000006 2003/09/24 �C���J�n
                    'Case ABAtenaNenkinEntity.JKYNENKNSHU2                   '�󋋔N���ԍ��Q
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU2)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If
                    'Case ABAtenaNenkinEntity.JKYNENKNNO2                    '�󋋔N����ʂQ
                    '    If (Not UFStringClass.CheckANK(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO2)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaNenkinEntity.JKYNENKNNO2                    '�󋋔N���ԍ��Q
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNSHU2                   '�󋋔N����ʂQ
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* ����ԍ� 000006 2003/09/24 �C���I��
                Case ABAtenaNenkinEntity.JKYNENKNEDABAN2                '�󋋔N���}�ԂQ
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNEDABAN2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKB2                    '�󋋔N���敪�Q
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKB2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKIGO3                  '�󋋔N���L���R
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKIGO3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* ����ԍ� 000006 2003/09/24 �C���J�n
                    'Case ABAtenaNenkinEntity.JKYNENKNSHU3                   '�󋋔N���ԍ��R
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU3)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If
                    'Case ABAtenaNenkinEntity.JKYNENKNNO3                    '�󋋔N����ʂR
                    '    If (Not UFStringClass.CheckANK(strValue)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO3)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaNenkinEntity.JKYNENKNNO3                    '�󋋔N���ԍ��R
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNSHU3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNSHU3                   '�󋋔N����ʂR
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNNO3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '* ����ԍ� 000006 2003/09/24 �C���I��
                Case ABAtenaNenkinEntity.JKYNENKNEDABAN3                '�󋋔N���}�ԂR
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNEDABAN3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.JKYNENKNKB3                    '�󋋔N���敪�R
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_JKYNENKNKB3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.TANMATSUID                     '�[��ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SAKUJOFG                       '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KOSHINCOUNTER                  '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SAKUSEINICHIJI                 '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.SAKUSEIUSER                    '�쐬���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KOSHINNICHIJI                  '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaNenkinEntity.KOSHINUSER                     '�X�V���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENANENKINB_RDBDATATYPE_KOSHINUSER)
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
