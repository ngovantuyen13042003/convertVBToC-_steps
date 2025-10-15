'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �������ۂc�`(ABAtenaKokuhoBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2002/12/26�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/17 000001     �ǉ����A���ʍ��ڂ�ݒ肷��
'* 2003/05/21 000002     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/28 000003     RDB�A�N�Z�X���O�̏C��
'* 2003/09/11 000004     ���۔ԍ��Ŏ擾���郁�\�b�h�̎d�l�ǉ�
'* 2003/10/31 000005     �t�@�C�����C�A�E�g�ύX�ɔ����C��
'* 2003/11/18 000006     �d�l�ύX�F���ږ��̕ύX
'* 2004/11/11 000007     �f�[�^�`�F�b�N���s�Ȃ�Ȃ�
'* 2005/02/16 000008     ���X�|���X���P�F�r�p�k���쐬�̏C��     
'* 2010/04/16 000009     VS2008�Ή��i��Áj
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

Public Class ABAtenaKokuhoBClass
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

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaKokuhoBClass"
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
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �������ۃ}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaKokuho(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������ۃ}�X�^���Y���f�[�^���擾����B�B
    '* 
    '* ����           strJuminCD As String  :�Z���R�[�h
    '* 
    '* �߂�l         �擾�����������ۃ}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsAtenaKokuhoEntity    �C���e���Z���X�FABAtenaKokuhoEntity
    '************************************************************************************************
    Public Function GetAtenaKokuho(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaKokuho"
        Dim csAtenaKokuhoEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKokuhoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKokuhoEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKokuhoEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000003 2003/08/28 �C���J�n
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
            '*����ԍ� 000003 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            csAtenaKokuhoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKokuhoEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csAtenaKokuhoEntity

    End Function

    '*����ԍ� 000004 2003/09/11 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �������ۃ}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaKokuhoBango(ByVal strKokuhoNO As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������ۃ}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strKokuhoNO As String  : ���۔ԍ�
    '* 
    '* �߂�l         �擾�����������ۃ}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsAtenaKokuhoEntity    �C���e���Z���X�FABAtenaKokuhoEntity
    '************************************************************************************************
    Public Function GetAtenaKokuhoBango(ByVal strKokuhoNO As String) As DataSet
        Dim csAtenaKokuhoEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKokuhoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKokuhoEntity.KOKUHONO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKokuhoEntity.PARAM_KOKUHONO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKokuhoEntity.SAKUJOFG)
            strSQL.Append(" <> ")
            strSQL.Append(ABAtenaKokuhoEntity.PARAM_SAKUJOFG)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_KOKUHONO
            cfUFParameterClass.Value = strKokuhoNO
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_SAKUJOFG
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
            csAtenaKokuhoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKokuhoEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csAtenaKokuhoEntity

    End Function
    '*����ԍ� 000004 2003/09/11 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �������ۃ}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaKokuho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �������ۃ}�X�^�Ƀf�[�^��ǉ�����B
    '* 
    '* ����           csDataRow As DataRow  :�ǉ��f�[�^
    '* 
    '* �߂�l         �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertAtenaKokuho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertAtenaKokuho"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000009
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000009
        Dim intInsCnt As Integer
        Dim strUpdateDateTime As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '*����ԍ� 000008 2005/02/16 �C���J�n
                Call CreateInsertSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*����ԍ� 000008 2005/02/16 �C���I��
            End If

            '�X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '�쐬����

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaKokuhoEntity.TANMATSUID) = m_cfControlData.m_strClientId               '�[���h�c
            csDataRow(ABAtenaKokuhoEntity.SAKUJOFG) = "0"                                           '�폜�t���O
            csDataRow(ABAtenaKokuhoEntity.KOSHINCOUNTER) = Decimal.Zero                             '�X�V�J�E���^
            csDataRow(ABAtenaKokuhoEntity.SAKUSEINICHIJI) = strUpdateDateTime                       '�쐬����
            csDataRow(ABAtenaKokuhoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                '�쐬���[�U�[
            csDataRow(ABAtenaKokuhoEntity.KOSHINNICHIJI) = strUpdateDateTime                        '�X�V����
            csDataRow(ABAtenaKokuhoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                 '�X�V���[�U�[

            '*����ԍ� 000007 2004/11/11 �C���J�n
            '���N���X�̃f�[�^�������`�F�b�N���s��
            'For Each csDataColumn In csDataRow.Table.Columns
            '    '�f�[�^�������`�F�b�N
            '    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            'Next csDataColumn
            '*����ԍ� 000007 2004/11/11 �C���I��

            '�p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKokuhoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*����ԍ� 000003 2003/08/28 �C���J�n
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
            '*����ԍ� 000003 2003/08/28 �C���I��

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
    '* ���\�b�h��     �������ۃ}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaKokuho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �������ۃ}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           csDataRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaKokuho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaKokuho"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000009
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000009
        Dim intUpdCnt As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '*����ԍ� 000008 2005/02/16 �C���J�n
                Call CreateUpdateSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*����ԍ� 000008 2005/02/16 �C���I��
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaKokuhoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABAtenaKokuhoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaKokuhoEntity.KOSHINCOUNTER)) + 1       '�X�V�J�E���^
            csDataRow(ABAtenaKokuhoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '�X�V����
            csDataRow(ABAtenaKokuhoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaKokuhoEntity.PREFIX_KEY.RLength) = ABAtenaKokuhoEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKokuhoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '*����ԍ� 000007 2004/11/11 �C���J�n
                    ' �f�[�^�������`�F�b�N
                    'CheckColumnValue(cfParam.ParameterName.Substring(ABAtenaKokuhoEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABAtenaKokuhoEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString.Trim)
                    '*����ԍ� 000007 2004/11/11 �C���I��
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKokuhoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000003 2003/08/28 �C���J�n
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
            '*����ԍ� 000003 2003/08/28 �C���I��

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "UpdateAtenaKokuho")

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

    '*����ԍ� 000008 2005/02/16 �ǉ��J�n
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
            m_strInsertSQL = "INSERT INTO " + ABAtenaKokuhoEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            m_strUpdateSQL = "UPDATE " + ABAtenaKokuhoEntity.TABLE_NAME + " SET "
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
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += " WHERE " + ABAtenaKokuhoEntity.JUMINCD + " = " + ABAtenaKokuhoEntity.KEY_JUMINCD + " AND " + _
                                          ABAtenaKokuhoEntity.KOSHINCOUNTER + " = " + ABAtenaKokuhoEntity.KEY_KOSHINCOUNTER

            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_KOSHINCOUNTER
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
    '*����ԍ� 000008 2005/02/16 �ǉ��I��

    '*����ԍ� 000008 2005/02/16 �폜�J�n
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
    '        m_strInsertSQL = "INSERT INTO " + ABAtenaKokuhoEntity.TABLE_NAME + " "
    '        strInsertColumn = ""
    '        strInsertParam = ""

    '        ' UPDATE SQL���̍쐬
    '        m_strUpdateSQL = "UPDATE " + ABAtenaKokuhoEntity.TABLE_NAME + " SET "
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
    '            strInsertParam += ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' SQL���̍쐬
    '            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
    '            cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    '            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    '            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    '            cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
    '        m_strUpdateSQL += " WHERE " + ABAtenaKokuhoEntity.JUMINCD + " = " + ABAtenaKokuhoEntity.KEY_JUMINCD + " AND " + _
    '                                      ABAtenaKokuhoEntity.KOSHINCOUNTER + " = " + ABAtenaKokuhoEntity.KEY_KOSHINCOUNTER

    '        ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_JUMINCD
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaKokuhoEntity.KEY_KOSHINCOUNTER
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
    '*����ԍ� 000008 2005/02/16 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* �@�\�@�@       �������ۃ}�X�^�̃f�[�^�������`�F�b�N���s���܂��B
    '* 
    '* ����           strColumnName As String
    '*                strValue As String
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 Start 2010/04/16 000009
        ''*����ԍ� 000001 2003/10/31 �ǉ��J�n
        'Const THIS_DBTABLE_NAME As String = "�`�a��������."
        ''*����ԍ� 000001 2003/10/31 �ǉ��I��
        '* corresponds to VS2008 End 2010/04/16 000009

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
                Case ABAtenaKokuhoEntity.JUMINCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.SHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KYUSHICHOSONCD
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHONO
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHONO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOSHIKAKUKB
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSHIKAKUKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBMEISHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSHIKAKUKBMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOSHIKAKUKBRYAKUSHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSHIKAKUKBRYAKUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOGAKUENKB
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOGAKUENKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOGAKUENKBMEISHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOGAKUENKBMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOGAKUENKBRYAKUSHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOGAKUENKBRYAKUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOSHUTOKUYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSHUTOKUYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOSOSHITSUYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOSOSHITSUYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOTISHKKB
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOTISHKKBMEISHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKKBMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOTISHKKBRYAKUSHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKKBRYAKUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKB
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKHONHIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                    '*����ԍ� 000006 2003/11/18 �C���J�n
                    'Case ABAtenaKokuhoEntity.KOKUHOTIAHKHONHIKBMEISHO
                Case ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBMEISHO       ' ���ۑސE�{��敪��������
                    '*����ԍ� 000006 2003/11/18 �C���I��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTIAHKHONHIKBMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOTISHKHONHIKBRYAKUSHO
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKHONHIKBRYAKUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOTISHKGAITOYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKGAITOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKokuhoEntity.KOKUHOTISHKHIGAITOYMD
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOTISHKHIGAITOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                    '*����ԍ� 000005 2003/10/31 �ǉ��J�n
                Case ABAtenaKokuhoEntity.KOKUHOHOKENSHOKIGO
                    '    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        '�G���[��`���擾
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002011)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage + THIS_DBTABLE_NAME + strColumnName, objErrorStruct.m_strErrorCode)
                    '    End If
                    '*����ԍ� 000005 2003/10/31 �ǉ��I��
                Case ABAtenaKokuhoEntity.KOKUHOHOKENSHONO
                    '*����ԍ� 000005 2003/10/31 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '    '�G���[��`���擾
                    '    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOKUHOHOKENSHONO)
                    '    '��O�𐶐�
                    '    Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    'End If
                    'If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                    '    m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '    '�G���[��`���擾
                    '    objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002011)
                    '    '��O�𐶐�
                    '    Throw New UFAppException(objErrorStruct.m_strErrorMessage + THIS_DBTABLE_NAME + strColumnName, objErrorStruct.m_strErrorCode)
                    'End If
                    '*����ԍ� 000005 2003/10/31 �C���I��
                Case ABAtenaKokuhoEntity.TANMATSUID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.SAKUJOFG
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOSHINCOUNTER
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.SAKUSEINICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.SAKUSEIUSER
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOSHINNICHIJI
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKokuhoEntity.KOSHINUSER
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKOKUHOB_RDBDATATYPE_KOSHINUSER)
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
