'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �������c�`(ABAtenaKaigoBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/08�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/17 000001     �ǉ����A���ʍ��ڂ�ݒ肷��
'* 2003/05/21 000002     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/28 000003     RDB�A�N�Z�X���O�̏C��
'* 2003/09/11 000004     ��ی��Ҕԍ��Ŏ擾���郁�\�b�h�̎d�l�ǉ�
'* 2003/11/18 000005     �d�l�ύX�F���ږ��̕ύX NINTEISYURYOYMD->NinteiShuryoYMD
'* 2005/02/16 000006     ���X�|���X���P�F�r�p�k���쐬�̏C��     
'* 2010/04/16 000007     VS2008�Ή��i��Áj
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

Public Class ABAtenaKaigoBClass
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
    Private Const THIS_CLASS_NAME As String = "ABAtenaKaigoBClass"
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
    '* ���\�b�h��     �������}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaKaigo(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������}�X�^���Y���f�[�^���擾����B�B
    '* 
    '* ����           strJuminCD As String  :�Z���R�[�h
    '* 
    '* �߂�l         �擾�����������}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsAtenaKaigoEntity    �C���e���Z���X�FABAtenaKaigoEntity
    '************************************************************************************************
    Public Function GetAtenaKaigo(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaKaigo"
        Dim csAtenaKaigoEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKaigoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKaigoEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKaigoEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_JUMINCD
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
            csAtenaKaigoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKaigoEntity.TABLE_NAME, cfUFParameterCollectionClass)

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

        Return csAtenaKaigoEntity

    End Function

    '*����ԍ� 000004 2003/09/11 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �������}�X�^���o
    '* 
    '* �\��           Public Function GetAtenaKaigoBango(ByVal strHihknshaNO As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�������}�X�^���Y���f�[�^���擾����B�B
    '* 
    '* ����           strJuminCD As String  :��ی��Ҕԍ�
    '* 
    '* �߂�l         �擾�����������}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsAtenaKaigoEntity    �C���e���Z���X�FABAtenaKaigoEntity
    '************************************************************************************************
    Public Function GetAtenaKaigoBango(ByVal strHihknshaNO As String) As DataSet
        Dim csAtenaKaigoEntity As DataSet
        Dim strSQL As New StringBuilder()
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABAtenaKaigoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABAtenaKaigoEntity.HIHKNSHANO)
            strSQL.Append(" = ")
            strSQL.Append(ABAtenaKaigoEntity.PARAM_HIHKNSHANO)
            strSQL.Append(" AND ")
            strSQL.Append(ABAtenaKaigoEntity.SAKUJOFG)
            strSQL.Append(" <> ")
            strSQL.Append(ABAtenaKaigoEntity.PARAM_SAKUJOFG)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_HIHKNSHANO
            cfUFParameterClass.Value = strHihknshaNO
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_SAKUJOFG
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
            csAtenaKaigoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaKaigoEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �f�o�b�O�I�����O�o��
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

        Return csAtenaKaigoEntity

    End Function
    '*����ԍ� 000004 2003/09/11 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �������}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaKaigo(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �������}�X�^�Ƀf�[�^��ǉ�����B
    '* 
    '* ����           csDataRow As DataRow  :�ǉ��f�[�^
    '* 
    '* �߂�l         �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertAtenaKaigo(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertAtenaKaigo"
        Dim cfParam As UFParameterClass
        Dim csDataColumn As DataColumn
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim intIndex As Integer
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim intInsCnt As Integer
        Dim strUpdateDateTime As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '*����ԍ� 000006 2005/02/16 �C���J�n
                Call CreateInsertSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*����ԍ� 000006 2005/02/16 �C���I��
            End If

            '�X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '�쐬����

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaKaigoEntity.TANMATSUID) = m_cfControlData.m_strClientId                '�[���h�c
            csDataRow(ABAtenaKaigoEntity.SAKUJOFG) = "0"                                            '�폜�t���O
            csDataRow(ABAtenaKaigoEntity.KOSHINCOUNTER) = Decimal.Zero                              '�X�V�J�E���^
            csDataRow(ABAtenaKaigoEntity.SAKUSEINICHIJI) = strUpdateDateTime                        '�쐬����
            csDataRow(ABAtenaKaigoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                 '�쐬���[�U�[
            csDataRow(ABAtenaKaigoEntity.KOSHINNICHIJI) = strUpdateDateTime                         '�X�V����
            csDataRow(ABAtenaKaigoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                  '�X�V���[�U�[

            '���N���X�̃f�[�^�������`�F�b�N���s��
            For Each csDataColumn In csDataRow.Table.Columns
                '�f�[�^�������`�F�b�N
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            '�p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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
    '* ���\�b�h��     �������}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateAtenaKaigo(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  �������}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           csDataRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateAtenaKaigo(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateAtenaKaigo"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim csDataColumn As DataColumn
        'Dim intIndex As Integer
        '* corresponds to VS2008 Ends 2010/04/16 000007
        Dim intUpdCnt As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '*����ԍ� 000006 2005/02/16 �C���J�n
                Call CreateUpdateSQL(csDataRow)
                'Call CreateSQL(csDataRow)
                '*����ԍ� 000006 2005/02/16 �C���I��
            End If

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaKaigoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                    '�[���h�c
            csDataRow(ABAtenaKaigoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABAtenaKaigoEntity.KOSHINCOUNTER)) + 1         '�X�V�J�E���^
            csDataRow(ABAtenaKaigoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")    '�X�V����
            csDataRow(ABAtenaKaigoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                      '�X�V���[�U�[

            '�쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABAtenaKaigoEntity.PREFIX_KEY.RLength) = ABAtenaKaigoEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '�f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    '�p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaKaigoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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

    '*����ԍ� 000006 2005/02/16 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     SQL���̍쐬
    '* 
    '* �\��           Private Sub CreateInsertSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\�@�@    �@ INSERTSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �ǉ��Ώۂ̍s
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
            m_strInsertSQL = "INSERT INTO " + ABAtenaKaigoEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim strInsertColumn As String
        'Dim strInsertParam As String
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim cfUFParameterClass As UFParameterClass
        Dim strUpdateWhere As String
        Dim strUpdateParam As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABAtenaKaigoEntity.TABLE_NAME + " SET "
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
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            ' UPDATE SQL���̃g���~���O
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += " WHERE " + ABAtenaKaigoEntity.JUMINCD + " = " + ABAtenaKaigoEntity.KEY_JUMINCD + " AND " + _
                                          ABAtenaKaigoEntity.KOSHINCOUNTER + " = " + ABAtenaKaigoEntity.KEY_KOSHINCOUNTER

            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_KOSHINCOUNTER
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
    '*����ԍ� 000006 2005/02/16 �ǉ��J�n

    '*����ԍ� 000006 2005/02/16 �폜�J�n
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
    '        m_strInsertSQL = "INSERT INTO " + ABAtenaKaigoEntity.TABLE_NAME + " "
    '        strInsertColumn = ""
    '        strInsertParam = ""

    '        ' UPDATE SQL���̍쐬
    '        m_strUpdateSQL = "UPDATE " + ABAtenaKaigoEntity.TABLE_NAME + " SET "
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
    '            strInsertParam += ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' SQL���̍쐬
    '            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    '            ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
    '            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    '            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    '            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    '            cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
    '        m_strUpdateSQL += " WHERE " + ABAtenaKaigoEntity.JUMINCD + " = " + ABAtenaKaigoEntity.KEY_JUMINCD + " AND " + _
    '                                      ABAtenaKaigoEntity.KOSHINCOUNTER + " = " + ABAtenaKaigoEntity.KEY_KOSHINCOUNTER

    '        ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_JUMINCD
    '        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    '        cfUFParameterClass = New UFParameterClass()
    '        cfUFParameterClass.ParameterName = ABAtenaKaigoEntity.KEY_KOSHINCOUNTER
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
    '*����ԍ� 000006 2005/02/16 �폜�J�n

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* �@�\�@�@       �������}�X�^�̃f�[�^�������`�F�b�N���s���܂��B
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
                Case ABAtenaKaigoEntity.JUMINCD                         '�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SHICHOSONCD                     '�s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KYUSHICHOSONCD                  '���s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.HIHKNSHANO                      '��ی��Ҕԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_HIHKNSHANO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SKAKSHUTKYMD                    '���i�擾��
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SKAKSHUTKYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.SKAKSSHTSYMD                    '���i�r����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SKAKSSHTSYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.SKAKHIHOKENSHAKB                '���i��ی��ҋ敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SKAKHIHOKENSHAKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.JUSHOCHITKRIKB                  '�Z���n����ҋ敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUSHOCHITKRIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.JUKYUSHAKB                      '�󋋎ҋ敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUKYUSHAKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.YOKAIGJOTAIKBCD                 '�v����ԋ敪�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_YOKAIGJOTAIKBCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KAIGSKAKKB                      '�v����ԋ敪
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KAIGSKAKKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.NINTEIKAISHIYMD                 '�F��L���J�n��
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_NINTEIKAISHIYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                    '* ����ԍ� 000005 2003/11/18 �C���J�n
                    'Case ABAtenaKaigoEntity.NINTEISYURYOYMD                 '�F��L���I����
                Case ABAtenaKaigoEntity.NINTEISHURYOYMD                 '�F��L���I����
                    '* ����ԍ� 000005 2003/11/18 �C���I��
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_NINTEISYURYOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.JUKYUNINTEIYMD                  '�󋋔F��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUKYUNINTEIYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.JUKYUNINTEITORIKESHIYMD         '�󋋔F�����N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_JUKYUNINTEITORIKESHIYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABAtenaKaigoEntity.TANMATSUID                      '�[��ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SAKUJOFG                        '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KOSHINCOUNTER                   '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SAKUSEINICHIJI                  '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.SAKUSEIUSER                     '�쐬���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KOSHINNICHIJI                   '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaKaigoEntity.KOSHINUSER                      '�X�V���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENAKAIGOB_RDBDATATYPE_KOSHINUSER)
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
