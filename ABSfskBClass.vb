'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        ���t��}�X�^�c�`(ABSfskBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/08�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/02/25 000001     ���o��������Ɩ�����ʃR�[�h���͂����Ƃ��邪�A�Ɩ�����ʃR�[�h�� String.Empty�Ƃ��Ď擾����
'* 2003/03/10 000002     �Z���b�c���̐������`�F�b�N�Ɍ��
'* 2003/03/17 000003     �ǉ����A���ʍ��ڂ�ݒ肷��
'* 2003/03/27 000004     �G���[�����N���X�̎Q�Ɛ��"AB"�Œ�ɂ���
'* 2003/04/23 000005     �I���N���������`�F�b�N��"999999"������
'* 2003/05/06 000006     �������`�F�b�N�ύX
'* 2003/05/21 000007     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/28 000008     RDB�A�N�Z�X���O�̏C��
'* 2003/10/30 000009     �d�l�ύX�A�J�^�J�i�`�F�b�N��ANK�`�F�b�N�ɕύX
'* 2004/08/27 000010     ���x���P�F�i�{��j
'* 2005/01/25 000011     ���x���P�Q�F�i�{��j
'* 2005/06/05 000012     �f�o�b�N���O�̈ꕔ���͂���
'* 2005/06/16 000013     SQL����Insert,Update,Delete�̊e���\�b�h���Ă΂ꂽ���Ɋe���쐬����(�}���S���R)
'* 2005/12/14 000014     �d�l�ύX�F�s����b�c�̃`�F�b�NANK�ɕύX(�}���S���R)
'* 2007/03/09 000015     ���t����擾SQL�̃\�[�g����ύX(����)
'* 2010/03/04 000016     ���t��}�X�^���o�����̃I�[�o�[���[�h��ǉ��i��Áj
'* 2010/04/16 000017     VS2008�Ή��i��Áj
'* 2020/08/21 000018     �yAB32006�z��[�E���t�惁���e�i���X�i�΍��j
'* 2023/03/10 000019     �yAB-0970-1�z����GET�擾���ڕW�����Ή��i�����j
'* 2023/08/22 000020     �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ��i�V�؁j
'* 2023/10/20 000021     �yAB-0840-1�z���t��Ǘ����ڒǉ�(����)
'* 2023/12/05 000022     �yAB-0840-1�z���t��Ǘ����ڒǉ�_�ǉ��C���i�����j
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

Public Class ABSfskBClass
#Region "�����o�ϐ�"
    '�����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X
    Private m_strInsertSQL As String                        ' INSERT�pSQL
    Private m_strUpdateSQL As String                        ' UPDATE�pSQL
    Private m_strDeleteSQL As String                        ' DELETE�pSQL�i�����j
    Private m_strDelRonriSQL As String                      ' DELETE�pSQL�i�_���j
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass      'DELETE�p�p�����[�^�R���N�V�����i�����j
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    'DELETE�p�p�����[�^�R���N�V�����i�_���j

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABSfskBClass"
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h
    '*����ԍ� 000021 2023/10/20 �ǉ��J�n
    Private Const THIS_ONE As Integer = 1
    Private Const ALL0_YMD As String = "00000000"                                   ' �N�����I�[���O
    Private Const ALL9_YMD As String = "99999999"                                   ' �N�����I�[���X
    '*����ԍ� 000021 2023/10/20 �ǉ��I��
    '* ����ԍ� 000010 2004/08/27 �ǉ��J�n�i�{��j
    Public m_blnBatch As Boolean = False               '�o�b�`�t���O
    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    '* ����ԍ� 000010 2004/08/27 �ǉ��I��
    Private m_csDataSchma_Hyojun As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g_�W����
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
        m_strDeleteSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String  :�Z���R�[�h
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsSfskEntity    �C���e���Z���X�FABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String) As DataSet
        Return GetSfskBHoshu(strJuminCD, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String    :�Z���R�[�h
    '*                blnSakujoFG As Boolean  :�폜�t���O
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsSfskEntity    �C���e���Z���X�FABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskBHoshu"              '���̃��\�b�h��
        Dim csSfskEntity As DataSet                                     '���t��}�X�^�f�[�^
        Dim strSQL As New StringBuilder()                               'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABSfskEntity.TABLE_NAME)

            '* ����ԍ� 000010 2004/08/27 �ǉ��J�n�i�{��j
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If
            '* ����ԍ� 000010 2004/08/27 �ǉ��I��

            'WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABSfskEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABSfskEntity.KEY_JUMINCD)
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            'ORDER������
            '*����ԍ� 000015 2007/03/09 �C���J�n
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABSfskEntity.GYOMUCD)
            strSQL.Append(" ASC, ")
            strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strSQL.Append(" ASC, ")
            '*����ԍ� 000020 2023/08/22 �C���J�n
            strSQL.Append(ABSfskEntity.STYMD)
            strSQL.Append(" ASC;")
            'strSQL.Append(ABSfskEntity.STYM)
            'strSQL.Append(" ASC;")
            '*����ԍ� 000020 2023/08/22 �C���I��
            'strSQL.Append(" ORDER BY ")
            'strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
            'strSQL.Append(" ASC")
            '*����ԍ� 000015 2007/03/09 �C���I��

            '���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            '���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD

            '���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000008 2003/08/28 �C���J�n
            ''RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            '* ����ԍ� 000011 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                           "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                           "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                           "�y���s���\�b�h��:GetDataSet�z" + _
                                           "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            End If
            '* ����ԍ� 000011 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
            '*����ԍ� 000008 2003/08/28 �C���I��

            'SQL�̎��s DataSet�̎擾

            '* ����ԍ� 000010 2004/08/27 �X�V�J�n�i�{��j
            'csSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csSfskEntity = m_csDataSchma.Clone()
            'm_csDataSchma.Clear()
            'csSfskEntity = m_csDataSchma
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '* ����ԍ� 000010 2004/08/27 �X�V�I��

            '�f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            '���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            '���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            '�G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            '�V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csSfskEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal strKikanYMD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                strKikanYMD As String         :���ԔN����
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsSfskEntity    �C���e���Z���X�FABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String,
                                            ByVal strGyomuCD As String,
                                            ByVal strGyomunaiSHUCD As String,
                                            ByVal strKikanYMD As String) As DataSet
        Return GetSfskBHoshu(strJuminCD, strGyomuCD, strGyomunaiSHUCD, strKikanYMD, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal strKikanYMD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                strKikanYMD As String         :���ԔN����
    '*                blnSakujoFG As Boolean        :�폜�t���O
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsSfskEntity    �C���e���Z���X�FABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal strJuminCD As String,
                                            ByVal strGyomuCD As String,
                                            ByVal strGyomunaiSHUCD As String,
                                            ByVal strKikanYMD As String,
                                            ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskBHoshu"              '���̃��\�b�h��
        Dim csSfskEntity As DataSet                                     '���t��}�X�^�f�[�^
        Dim strSQL As String                                            'SQL��������
        '* corresponds to VS2008 Start 2010/04/16 000017
        'Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        '* corresponds to VS2008 End 2010/04/16 000017
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim blnSakujo As Boolean                                        '�폜�f�[�^�ǂݍ���

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '* ����ԍ� 000011 2005/01/25 �ǉ��J�n�i�{��j�P�������ǂݍ��ޗl�ɂ���
            Dim intWkKensu As Integer
            intWkKensu = m_cfRdbClass.p_intMaxRows()
            '* ����ԍ� 000011 2005/01/25 �ǉ��I���i�{��j�P�������ǂݍ��ޗl�ɂ���

            'SQL,�p�����[�^�R���N�V�����̍쐬
            blnSakujo = blnSakujoFG
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            strSQL = Me.CreateSql_Param(strJuminCD, strGyomuCD, strGyomunaiSHUCD, True, strKikanYMD, blnSakujo, cfUFParameterCollectionClass)

            '*����ԍ� 000008 2003/08/28 �C���J�n
            ''RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL + "�z")

            ' RDB�A�N�Z�X���O�o��
            '* ����ԍ� 000011 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + Me.GetType.Name + "�z" +
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                            "�y���s���\�b�h��:GetDataSet�z" +
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            End If
            '* ����ԍ� 000011 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
            '*����ԍ� 000008 2003/08/28 �C���I��

            'SQL�̎��s DataSet�̎擾
            '* ����ԍ� 000010 2004/08/27 �X�V�J�n�i�{��j
            'csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csSfskEntity = m_csDataSchma.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '* ����ԍ� 000010 2004/08/27 �X�V�I��

            '* ����ԍ� 000011 2005/01/25 �ǉ��J�n�i�{��j�������Ԃ��ꍇ�́A�擪�Ɠ����Ɩ�����ʈȊO�̂��͍̂폜����
            '��̔ԍ��ň�x�쐬�������A�K�v�Ȃ��Ȃ����̂ō폜
            'If (strGyomuCD = "*1") Then
            '    If (csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count > 1) Then
            '        Dim csDataRow As DataRow
            '        Dim csDataTable As DataTable
            '        Dim intRowCount As Integer
            '        csDataTable = csSfskEntity.Tables(ABSfskEntity.TABLE_NAME)
            '        csDataRow = csDataTable.Rows(0)
            '        For intRowCount = csDataTable.Rows.Count - 1 To 1 Step -1
            '            If (CType(csDataRow.Item(ABSfskEntity.GYOMUNAISHU_CD), String) <> CType(csDataTable.Rows(intRowCount).Item(ABSfskEntity.GYOMUNAISHU_CD), String)) Then
            '                csDataTable.Rows(intRowCount).Delete()
            '            End If
            '        Next
            '        csDataTable.AcceptChanges()
            '    End If
            'End If
            '* ����ԍ� 000011 2005/01/25 �ǉ��I���i�{��j�������Ԃ��ꍇ�́A�擪�Ɠ����Ɩ�����ʈȊO�̂��͍̂폜����

            '* ����ԍ� 000011 2005/01/25 �ǉ��I���i�{��j�P�������ǂݍ��ޗl�ɂ������̂����ɖ߂�
            m_cfRdbClass.p_intMaxRows = intWkKensu
            '* ����ԍ� 000011 2005/01/25 �ǉ��I���i�{��j�P�������ǂݍ��ޗl�ɂ������̂����ɖ߂�

            '* ����ԍ� 000011 2005/01/25 �폜�J�n�i�{��j��őS���ǂݍ��ޗl�ɂ����̂ō폜
            ''�擾����
            'If (csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count() = 0) Then
            '    '�擾�������O���̎�
            '    If (strGyomunaiSHUCD <> "") Then
            '        'SQL,�p�����[�^�R���N�V�����̍쐬
            '        cfUFParameterCollectionClass = New UFParameterCollectionClass()
            '        strSQL = Me.CreateSql_Param(strJuminCD, strGyomuCD, strGyomunaiSHUCD, False, strKikanYM, blnSakujo, cfUFParameterCollectionClass)
            '        '*����ԍ� 000008 2003/08/28 �C���J�n
            '        ''RDB�A�N�Z�X���O�o��
            '        'm_cfLogClass.RdbWrite(m_cfControlData, _
            '        '                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '        '                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '        '                    "�y���s���\�b�h��:GetDataSet�z" + _
            '        '                    "�ySQL���e:" + strSQL + "�z")

            '        ' RDB�A�N�Z�X���O�o��
            '        m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                    "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                                    "�y���s���\�b�h��:GetDataSet�z" + _
            '                                    "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '        '*����ԍ� 000008 2003/08/28 �C���I��
            '        'SQL�̎��s DataSet�̎擾
            '        csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '        '�擾����
            '        If (csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count() = 0) Then
            '            '�擾�������O���̎�
            '            'SQL,�p�����[�^�R���N�V�����̍쐬
            '            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            '            strSQL = Me.CreateSql_Param(strJuminCD, "00", strGyomunaiSHUCD, False, strKikanYM, blnSakujo, cfUFParameterCollectionClass)
            '            '*����ԍ� 000008 2003/08/28 �C���J�n
            '            ''RDB�A�N�Z�X���O�o��
            '            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '            '                "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '            '                "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '            '                "�y���s���\�b�h��:GetDataSet�z" + _
            '            '                "�ySQL���e:" + strSQL + "�z")

            '            ' RDB�A�N�Z�X���O�o��
            '            m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                                        "�y���s���\�b�h��:GetDataSet�z" + _
            '                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '            '*����ԍ� 000008 2003/08/28 �C���I��
            '            'SQL�̎��s DataSet�̎擾
            '            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '        End If
            '    ElseIf (strGyomuCD <> "00") Then
            '        'SQL,�p�����[�^�R���N�V�����̍쐬
            '        cfUFParameterCollectionClass = New UFParameterCollectionClass()
            '        strSQL = Me.CreateSql_Param(strJuminCD, "00", strGyomunaiSHUCD, False, strKikanYM, blnSakujo, cfUFParameterCollectionClass)
            '        '*����ԍ� 000008 2003/08/28 �C���J�n
            '        ''RDB�A�N�Z�X���O�o��
            '        'm_cfLogClass.RdbWrite(m_cfControlData, _
            '        '                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '        '                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '        '                    "�y���s���\�b�h��:GetDataSet�z" + _
            '        '                    "�ySQL���e:" + strSQL + "�z")

            '        ' RDB�A�N�Z�X���O�o��
            '        m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                    "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                                    "�y���s���\�b�h��:GetDataSet�z" + _
            '                                    "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '        '*����ԍ� 000008 2003/08/28 �C���I��
            '        'SQL�̎��s DataSet�̎擾
            '        csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '    End If
            'End If
            '* ����ԍ� 000011 2005/01/25 �폜�I���i�{��j��őS���ǂݍ��ޗl�ɂ����̂ō폜

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csSfskEntity

    End Function

    '*����ԍ� 000016 2010/03/04 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu(ByVal cABSfskGetParaX As ABSFSKGetParaXClass) As DataSet
    '* 
    '* �@�\�@�@    �@ ���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           cABSfskGetParaX   :   ���t����p�����[�^�N���X
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsSfskEntity    �C���e���Z���X�FABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu(ByVal cABSfskGetParaX As ABSFSKGetParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskBHoshu"              ' ���\�b�h��
        Dim csSfskEntity As DataSet                                     ' ���t��}�X�^�f�[�^
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                      ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X
        Dim blnAndFg As Boolean = False                                 ' AND����t���O
        Dim strWork As String

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �C���X�^���X��
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' �X�L�[�}�擾����
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            Else
            End If

            ' SQL���̍쐬
            ' SELECT��
            strSQL.Append("SELECT * ")

            strSQL.Append(" FROM ").Append(ABSfskEntity.TABLE_NAME)

            ' WHERE��
            strSQL.Append(" WHERE ")
            '---------------------------------------------------------------------------------
            ' �Z���R�[�h
            If (cABSfskGetParaX.p_strJuminCD.Trim <> String.Empty) Then
                ' �Z���R�[�h���ݒ肳��Ă���ꍇ

                strSQL.Append(ABSfskEntity.JUMINCD).Append(" = ")
                strSQL.Append(ABSfskEntity.KEY_JUMINCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
                cfUFParameterClass.Value = CStr(cABSfskGetParaX.p_strJuminCD)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �Ɩ��R�[�h
            If (cABSfskGetParaX.p_strGyomuCD.Trim <> String.Empty) Then
                ' �Ɩ��R�[�h���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABSfskEntity.GYOMUCD).Append(" = ")
                strSQL.Append(ABSfskEntity.KEY_GYOMUCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
                cfUFParameterClass.Value = cABSfskGetParaX.p_strGyomuCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �Ɩ�����ʃR�[�h
            If (cABSfskGetParaX.p_strGyomuneiSHU_CD.Trim <> String.Empty) Then
                ' �Ɩ�����ʃR�[�h���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD).Append(" = ")
                strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
                cfUFParameterClass.Value = cABSfskGetParaX.p_strGyomuneiSHU_CD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If

            '---------------------------------------------------------------------------------
            ' ����
            If (cABSfskGetParaX.p_strKikanYM.Trim <> String.Empty) Then
                ' ���Ԃ��ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append("(")
                '*����ԍ� 000021 2023/10/20 �C���J�n
                'strSQL.Append(ABSfskEntity.STYM)                    '�J�n�N��
                'strSQL.Append(" <= ")
                'strSQL.Append(ABSfskEntity.KEY_STYM)
                'strSQL.Append(" AND ")
                'strSQL.Append(ABSfskEntity.EDYM)                    '�I���N��
                'strSQL.Append(" >= ")
                'strSQL.Append(ABSfskEntity.KEY_EDYM)
                strSQL.Append(ABSfskEntity.STYMD)                    '�J�n�N��
                strSQL.Append(" <= ")
                strSQL.Append(ABSfskEntity.KEY_STYMD)
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.EDYMD)                    '�I���N��
                strSQL.Append(" >= ")
                strSQL.Append(ABSfskEntity.KEY_EDYMD)
                '*����ԍ� 000021 2023/10/20 �C���I��
                strSQL.Append(")")

                ' �J�n�N��
                cfUFParameterClass = New UFParameterClass
                '*����ԍ� 000021 2023/10/20 �C���J�n
                'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYMD
                '*����ԍ� 000021 2023/10/20 �C���I��
                cfUFParameterClass.Value = cABSfskGetParaX.p_strKikanYM
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' �I���N��
                cfUFParameterClass = New UFParameterClass
                '*����ԍ� 000021 2023/10/20 �C���J�n
                'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYMD
                '*����ԍ� 000021 2023/10/20 �C���I��
                cfUFParameterClass.Value = cABSfskGetParaX.p_strKikanYM
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �폜�t���O
            If (cABSfskGetParaX.p_strSakujoFG.Trim = String.Empty) Then
                ' �폜�t���O�w�肪�Ȃ��ꍇ�A�폜�f�[�^�͒��o���Ȃ�
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If
                strSQL.Append(ABSfskEntity.SAKUJOFG).Append(" <> '1'")

            Else
                ' �폜�t���O�w�肪����ꍇ�A�폜�f�[�^�����o����
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�ASQL�������������I��
                Else
                    ' AND����t���O��"False"�̏ꍇ�ASQL������WHERE����폜
                    ' �폜����SQL���ꎞ�ޔ�
                    strWork = strSQL.ToString.Replace("WHERE", String.Empty)

                    ' strSQL���N���A���A�ޔ�����SQL���Z�b�g
                    strSQL.Length = 0
                    strSQL.Append(strWork)
                End If
            End If
            '---------------------------------------------------------------------------------

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                  "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                  "�y���s���\�b�h��:GetDataSet�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csSfskEntity = m_csDataSchma.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


            '�f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            '���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            '���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            '�G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            '�V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csSfskEntity

    End Function
    '*����ԍ� 000016 2010/03/04 �ǉ��I��

    '*����ԍ� 000018 2020/08/21 �ǉ��J�n
#Region "�푗�t��}�X�^���o"

    ''' <summary>
    ''' �푗�t��}�X�^���o
    ''' </summary>
    ''' <param name="strJuminCD">�Z���R�[�h</param>
    ''' <returns>�푗�t��}�X�^</returns>
    ''' <remarks></remarks>
    Public Overloads Function GetHiSfskBHoshu(ByVal strJuminCD As String) As DataSet
        Return GetHiSfskBHoshu(strJuminCD, False)
    End Function

    ''' <summary>
    ''' �푗�t��}�X�^���o
    ''' </summary>
    ''' <param name="strJuminCD">�Z���R�[�h</param>
    ''' <param name="blnSakujoFG">�폜�t���O</param>
    ''' <returns>�푗�t��}�X�^</returns>
    ''' <remarks></remarks>
    Public Overloads Function GetHiSfskBHoshu( _
        ByVal strJuminCD As String, _
        ByVal blnSakujoFG As Boolean) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim cfParameterClass As UFParameterClass
        Dim cfParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet
        Dim csSQL As StringBuilder

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �X�L�[�}�擾����
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(String.Empty, ABSfskEntity.TABLE_NAME, False)
            Else
                ' noop
            End If

            ' SQL���̍쐬    
            csSQL = New StringBuilder
            With csSQL

                .AppendFormat("SELECT A.* FROM {0} AS A", ABSfskEntity.TABLE_NAME)
                .AppendFormat(" LEFT JOIN {0} AS B", ABBikoEntity.TABLE_NAME)
                .AppendFormat(" ON A.{0} = B.{1}", ABSfskEntity.JUMINCD, ABBikoEntity.DATAKEY1)
                .AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.GYOMUCD, ABBikoEntity.DATAKEY2)
                .AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.GYOMUNAISHU_CD, ABBikoEntity.DATAKEY3)
                '*����ԍ� 000021 2023/10/20 �C���J�n
                '* ����ԍ� 000020 2023/08/22 �C���J�n
                '.AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.STYMD, ABBikoEntity.DATAKEY4)
                '.AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.EDYMD, ABBikoEntity.DATAKEY5)
                '.AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.STYM, ABBikoEntity.DATAKEY4)
                '.AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.EDYM, ABBikoEntity.DATAKEY5)
                .AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.TOROKURENBAN, ABBikoEntity.DATAKEY4)
                .AppendFormat(" AND A.{0} = B.{1}", ABSfskEntity.RRKNO, ABBikoEntity.DATAKEY5)
                '* ����ԍ� 000020 2023/08/22 �C���I��
                '*����ԍ� 000021 2023/10/20 �C���I��
                .Append(" WHERE")
                .AppendFormat(" B.{0} = '{1}'", ABBikoEntity.BIKOKBN, ABBikoEntity.DEFAULT.BIKOKBN.SFSK)
                .AppendFormat(" AND B.{0} = {1} AND B.{0} IS NOT NULL AND RTRIM(LTRIM(B.{0})) <> ''", ABBikoEntity.RESERVE, ABBikoEntity.PARAM_RESERVE)
                If (blnSakujoFG = True) Then
                    ' noop
                Else
                    .AppendFormat(" AND A.{0} <> '1'", ABSfskEntity.SAKUJOFG)
                End If
                .Append(" ORDER BY")
                .AppendFormat(" A.{0} ASC,", ABSfskEntity.GYOMUCD)
                .AppendFormat(" A.{0} ASC,", ABSfskEntity.GYOMUNAISHU_CD)
                '* ����ԍ� 000020 2023/08/22 �C���J�n
                .AppendFormat(" A.{0} DESC", ABSfskEntity.STYMD)
                '.AppendFormat(" A.{0} DESC", ABSfskEntity.STYM)
                '* ����ԍ� 000020 2023/08/22 �C���I��
                .Append(";")

            End With

            ' ���������̃p�����[�^�[�R���N�V�����N���X�̃C���X�^���X��
            cfParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^�[���쐬
            cfParameterClass = New UFParameterClass
            cfParameterClass.ParameterName = ABBikoEntity.PARAM_RESERVE
            cfParameterClass.Value = strJuminCD

            ' ���������̃p�����[�^�[�R���N�V�����N���X�Ƀp�����[�^�[�N���X��ǉ�
            cfParameterCollectionClass.Add(cfParameterClass)

            ' �o�b�`����
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                            "�y���s���\�b�h��:GetDataSet�z" + _
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(csSQL.ToString, cfParameterCollectionClass) + "�z")
            Else
                ' noop
            End If

            ' SQL�̎��s DataSet�̎擾
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString, csDataSet, ABSfskEntity.TABLE_NAME, cfParameterCollectionClass, True)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch cfAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + cfAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + cfAppExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        Catch csExp As Exception

            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + csExp.Message + "�z")

            ' �G���[�����̂܂܃X���[����
            Throw

        End Try

        Return csDataSet

    End Function

#End Region
    '*����ԍ� 000018 2020/08/21 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  ���t��}�X�^�Ƀf�[�^��ǉ�����B
    '* 
    '* ����           csDataRow As DataRow  :�ǉ��f�[�^
    '* 
    '* �߂�l         �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertSfskB"                '���̃��\�b�h��
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
                '* ����ԍ� 000013 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateInsertSQL(csDataRow)
                '* ����ԍ� 000013 2005/06/16 �ǉ��I��
            End If

            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")          '�쐬����

            '*����ԍ� 000021 2023/10/20 �ǉ��J�n
            ' �ʍ��ڕҏW���s��
            csDataRow(ABSfskEntity.RRKNO) = THIS_ONE.ToString()                 '����ԍ�
            '*����ԍ� 000021 2023/10/20 �ǉ��I��

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABSfskEntity.TANMATSUID) = m_cfControlData.m_strClientId  '�[���h�c
            csDataRow(ABSfskEntity.SAKUJOFG) = "0"                              '�폜�t���O
            csDataRow(ABSfskEntity.KOSHINCOUNTER) = Decimal.Zero                '�X�V�J�E���^
            csDataRow(ABSfskEntity.SAKUSEINICHIJI) = strUpdateDateTime          '�쐬����
            csDataRow(ABSfskEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   '�쐬���[�U�[
            csDataRow(ABSfskEntity.KOSHINNICHIJI) = strUpdateDateTime           '�X�V����
            csDataRow(ABSfskEntity.KOSHINUSER) = m_cfControlData.m_strUserId    '�X�V���[�U�[

            ' ���N���X�̃f�[�^�������`�F�b�N���s��
            For Each csDataColumn In csDataRow.Table.Columns
                ' �f�[�^�������`�F�b�N
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*����ԍ� 000008 2003/08/28 �C���J�n
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
            '*����ԍ� 000008 2003/08/28 �C���I��

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
    '* ���\�b�h��     ���t��}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  ���t��}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����           csDataRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateSfskB"                '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000017
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000017
        Dim intUpdCnt As Integer                                        '�X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000013 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateUpdateSQL(csDataRow)
                '* ����ԍ� 000013 2005/06/16 �ǉ��I��
            End If

            '*����ԍ� 000022 2023/12/05 �폜�J�n
            ''*����ԍ� 000021 2023/10/20 �ǉ��J�n
            ''����ԍ��̃J�E���g�A�b�v
            'csDataRow(ABSfskEntity.RRKNO) = CDec(csDataRow(ABSfskEntity.RRKNO)) + 1                             '����ԍ�
            ''*����ԍ� 000021 2023/10/20 �ǉ��I��
            '*����ԍ� 000022 2023/12/05 �폜�I��

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABSfskEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  '�[���h�c
            csDataRow(ABSfskEntity.KOSHINCOUNTER) = CDec(csDataRow(ABSfskEntity.KOSHINCOUNTER)) + 1             '�X�V�J�E���^
            csDataRow(ABSfskEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '�X�V����
            csDataRow(ABSfskEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABSfskEntity.PREFIX_KEY.RLength) = ABSfskEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000008 2003/08/28 �C���J�n
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
            '*����ԍ� 000008 2003/08/28 �C���I��

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
    '* ���\�b�h��     ���t��}�X�^�폜�i�_���j
    '* 
    '* �\��           Public Function DeleteSfskB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  ���t��}�X�^�̃f�[�^���폜�i�_���j����B
    '* 
    '* ����           csDataRow As DataRow  :�폜�f�[�^
    '* 
    '* �߂�l         �폜�i�_���j����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteSfskB�i�_���j"                '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000017
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000017
        Dim intDelCnt As Integer                                        '�폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or _
                m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000013 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateDeleteRonriSQL(csDataRow)
                '* ����ԍ� 000013 2005/06/16 �ǉ��I��
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABSfskEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  '�[���h�c
            csDataRow(ABSfskEntity.SAKUJOFG) = 1                                                                '�폜�t���O
            csDataRow(ABSfskEntity.KOSHINCOUNTER) = CDec(csDataRow(ABSfskEntity.KOSHINCOUNTER)) + 1             '�X�V�J�E���^
            csDataRow(ABSfskEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '�X�V����
            csDataRow(ABSfskEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    '�X�V���[�U�[

            '*����ԍ� 000022 2023/12/05 �폜�J�n
            ''* ����ԍ� 000021 2023/10/20 �ǉ��J�n
            'csDataRow(ABSfskEntity.RRKNO) = CDec(csDataRow(ABSfskEntity.RRKNO)) + 1                             '����ԍ�
            ''* ����ԍ� 000021 2023/10/20 �ǉ��I��
            '*����ԍ� 000022 2023/12/05 �폜�I��

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABSfskEntity.PREFIX_KEY.RLength) = ABSfskEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000008 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_strDelRonriSQL + "�z")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")
            '*����ԍ� 000008 2003/08/28 �C���I��

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)

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
    '* ���\�b�h��     ���t��}�X�^�폜�i�����j
    '* 
    '* �\��           Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow, 
    '*                                                      ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@  ���t��}�X�^�̃f�[�^���폜�i�����j����B
    '* 
    '* ����           csDataRow As DataRow      :�폜�f�[�^
    '*                strSakujoKB As String     :�폜�t���O
    '* 
    '* �߂�l         �폜�i�����j����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteSfskB(ByVal csDataRow As DataRow, ByVal strSakujoKB As String) As Integer
        Const THIS_METHOD_NAME As String = "DeleteSfskB�i�����j"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000017
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000017
        Dim intDelCnt As Integer                            '�폜����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �����̍폜�敪���`�F�b�N
            If (strSakujoKB <> "D") Then
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_DELETE_SAKUJOKB)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDeleteSQL Is Nothing Or m_strDeleteSQL = String.Empty Or _
                m_cfDeleteUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000013 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateDeleteButsuriSQL(csDataRow)
                '* ����ԍ� 000013 2005/06/16 �ǉ��I��
            End If

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABSfskEntity.PREFIX_KEY.RLength) = ABSfskEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '�p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABSfskEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000008 2003/08/28 �C���J�n
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
            '*����ԍ� 000008 2003/08/28 �C���I��

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

    '* corresponds to VS2008 Start 2010/04/16 000017
    ''* ����ԍ� 000013 2005/06/16 �폜�J�n
    '''''************************************************************************************************
    '''''* ���\�b�h��     SQL���̍쐬
    '''''* 
    '''''* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '''''* 
    '''''* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '''''* 
    '''''* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '''''* 
    '''''* �߂�l         �Ȃ�
    '''''************************************************************************************************
    ''''Private Sub CreateSQL(ByVal csDataRow As DataRow)
    ''''    Const THIS_METHOD_NAME As String = "CreateSQL"              '���̃��\�b�h��
    ''''    Dim csDataColumn As DataColumn
    ''''    Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
    ''''    Dim strInsertColumn As String                               '�ǉ�SQL�����ڕ�����
    ''''    Dim strInsertParam As String                                '�ǉ�SQL���p�����[�^������
    ''''    Dim strDelRonriSQL As New StringBuilder()                   '�_���폜SQL��������
    ''''    Dim strDeleteSQL As New StringBuilder()                     '�����폜SQL��������
    ''''    Dim strWhere As New StringBuilder()                         '�X�V�폜SQL��Where��������

    ''''    Try
    ''''        '�f�o�b�O���O�o��
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''        'SELECT SQL���̍쐬
    ''''        m_strInsertSQL = "INSERT INTO " + ABSfskEntity.TABLE_NAME + " "
    ''''        strInsertColumn = ""
    ''''        strInsertParam = ""

    ''''        '�X�V�폜Where���쐬
    ''''        strWhere.Append(" WHERE ")
    ''''        strWhere.Append(ABSfskEntity.JUMINCD)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_JUMINCD)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.GYOMUCD)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_GYOMUCD)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.GYOMUNAISHU_CD)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.STYM)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_STYM)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.EDYM)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_EDYM)
    ''''        strWhere.Append(" AND ")
    ''''        strWhere.Append(ABSfskEntity.KOSHINCOUNTER)
    ''''        strWhere.Append(" = ")
    ''''        strWhere.Append(ABSfskEntity.KEY_KOSHINCOUNTER)

    ''''        'UPDATE SQL���̍쐬
    ''''        m_strUpdateSQL = "UPDATE " + ABSfskEntity.TABLE_NAME + " SET "

    ''''        'DELETE�i�_���j SQL���̍쐬
    ''''        strDelRonriSQL.Append("UPDATE ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.TABLE_NAME)
    ''''        strDelRonriSQL.Append(" SET ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.TANMATSUID)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_TANMATSUID)
    ''''        strDelRonriSQL.Append(", ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.SAKUJOFG)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_SAKUJOFG)
    ''''        strDelRonriSQL.Append(", ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.KOSHINCOUNTER)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINCOUNTER)
    ''''        strDelRonriSQL.Append(", ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.KOSHINNICHIJI)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINNICHIJI)
    ''''        strDelRonriSQL.Append(", ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.KOSHINUSER)
    ''''        strDelRonriSQL.Append(" = ")
    ''''        strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINUSER)
    ''''        strDelRonriSQL.Append(strWhere.ToString)
    ''''        m_strDelRonriSQL = strDelRonriSQL.ToString

    ''''        'DELETE�i�����j SQL���̍쐬
    ''''        strDeleteSQL.Append("DELETE FROM ")
    ''''        strDeleteSQL.Append(ABSfskEntity.TABLE_NAME)
    ''''        strDeleteSQL.Append(strWhere.ToString)
    ''''        m_strDeleteSQL = strDeleteSQL.ToString

    ''''        'SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
    ''''        m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        'UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        'DELETE�i�_���j �p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        'DELETE�i�����j �p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        '�p�����[�^�R���N�V�����̍쐬
    ''''        For Each csDataColumn In csDataRow.Table.Columns
    ''''            cfUFParameterClass = New UFParameterClass()

    ''''            'INSERT SQL���̍쐬
    ''''            strInsertColumn += csDataColumn.ColumnName + ", "
    ''''            strInsertParam += ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    ''''            'SQL���̍쐬
    ''''            m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

    ''''            'INSERT �R���N�V�����Ƀp�����[�^��ǉ�
    ''''            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''            'UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    ''''            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        Next csDataColumn

    ''''        'INSERT SQL���̃g���~���O
    ''''        strInsertColumn = strInsertColumn.Trim()
    ''''        strInsertColumn = strInsertColumn.Trim(CType(",", Char))
    ''''        strInsertParam = strInsertParam.Trim()
    ''''        strInsertParam = strInsertParam.Trim(CType(",", Char))
    ''''        m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

    ''''        'UPDATE SQL���̃g���~���O
    ''''        m_strUpdateSQL = m_strUpdateSQL.Trim()
    ''''        m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

    ''''        'UPDATE SQL����WHERE��̒ǉ�
    ''''        m_strUpdateSQL += strWhere.ToString

    ''''        'UPDATE,DELETE(����) �R���N�V�����ɃL�[����ǉ�
    ''''        '�Z���R�[�h
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�Ɩ��R�[�h
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�Ɩ�����ʃR�[�h
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�J�n�N��
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�I���N��
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�X�V�J�E���^
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        'DELETE�i�_���j �R���N�V�����Ƀp�����[�^��ǉ�
    ''''        '�[���h�c
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_TANMATSUID
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�폜�t���O
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_SAKUJOFG
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�X�V�J�E���^
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�X�V����
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINNICHIJI
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�X�V���[�U
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINUSER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�Z���R�[�h
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�Ɩ��R�[�h
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�Ɩ�����ʃR�[�h
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�J�n�N��
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�I���N��
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
    ''''        '�X�V�J�E���^
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        ' �f�o�b�O���O�o��
    ''''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''    Catch exAppException As UFAppException
    ''''        ' ���[�j���O���O�o��
    ''''        m_cfLogClass.WarningWrite(m_cfControlData, _
    ''''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''''                                    "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
    ''''                                    "�y���[�j���O���e:" + exAppException.Message + "�z")
    ''''        ' ���[�j���O���X���[����
    ''''        Throw exAppException

    ''''    Catch exException As Exception '�V�X�e���G���[���L���b�`
    ''''        ' �G���[���O�o��
    ''''        m_cfLogClass.ErrorWrite(m_cfControlData, _
    ''''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''''                                    "�y�G���[���e:" + exException.Message + "�z")
    ''''        ' �V�X�e���G���[���X���[����
    ''''        Throw exException

    ''''    End Try
    ''''End Sub
    ''* ����ԍ� 000013 2005/06/16 �폜�I��
    '* corresponds to VS2008 Start 2010/04/16 000017

    '* ����ԍ� 000013 2005/06/16 �ǉ��J�n
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
        Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
        Dim csInsertColumn As StringBuilder                        '�ǉ�SQL�����ڕ�����
        Dim csInsertParam As StringBuilder                         '�ǉ�SQL���p�����[�^������

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            'INSERT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABSfskEntity.TABLE_NAME + " "
            csInsertColumn = New StringBuilder
            csInsertParam = New StringBuilder

            'INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            '�p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")

                csInsertParam.Append(ABSfskEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                'INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            'INSERT SQL���̃g���~���O
            m_strInsertSQL += "(" + csInsertColumn.ToString.Trim().Trim(CType(",", Char)) + ")" _
                    + " VALUES (" + csInsertParam.ToString.Trim().TrimEnd(CType(",", Char)) + ")"

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
        Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
        Dim strWhere As New StringBuilder                           '�X�V�폜SQL��Where��������

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�X�V�폜Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            '*����ԍ� 000021 2023/10/20 �C���J�n
            'strWhere.Append(ABSfskEntity.STYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_STYM)
            'strWhere.Append(" AND ")
            'strWhere.Append(ABSfskEntity.EDYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_EDYM)
            strWhere.Append(ABSfskEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_TOROKURENBAN)
            '*����ԍ� 000021 2023/10/20 �C���I��
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_KOSHINCOUNTER)

            'UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABSfskEntity.TABLE_NAME + " SET "

            'UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            '�p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�Z���b�c�E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If Not (csDataColumn.ColumnName = ABSfskEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABSfskEntity.SAKUSEIUSER) AndAlso _
                     Not (csDataColumn.ColumnName = ABSfskEntity.SAKUSEINICHIJI) Then
                    cfUFParameterClass = New UFParameterClass

                    'SQL���̍쐬
                    m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                    'UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            'UPDATE SQL���̃g���~���O
            m_strUpdateSQL = m_strUpdateSQL.Trim()
            m_strUpdateSQL = m_strUpdateSQL.Trim(CType(",", Char))

            'UPDATE SQL����WHERE��̒ǉ�
            m_strUpdateSQL += strWhere.ToString

            'UPDATE �R���N�V�����ɃL�[����ǉ�
            '�Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000021 2023/10/20 �C���J�n
            ''�J�n�N��
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
            'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ''�I���N��
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
            'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '�o�^�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_TOROKURENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000021 2023/10/20 �C���I��
            '�X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

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
        Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
        Dim strDelRonriSQL As New StringBuilder                     '�_���폜SQL��������
        Dim strWhere As New StringBuilder                           '�X�V�폜SQL��Where��������

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�X�V�폜Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            '*����ԍ� 000021 2023/10/20 �C���J�n
            'strWhere.Append(ABSfskEntity.STYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_STYM)
            'strWhere.Append(" AND ")
            'strWhere.Append(ABSfskEntity.EDYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_EDYM)
            strWhere.Append(ABSfskEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_TOROKURENBAN)
            '*����ԍ� 000021 2023/10/20 �C���I��
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_KOSHINCOUNTER)

            'DELETE�i�_���j SQL���̍쐬
            strDelRonriSQL.Append("UPDATE ")
            strDelRonriSQL.Append(ABSfskEntity.TABLE_NAME)
            strDelRonriSQL.Append(" SET ")
            strDelRonriSQL.Append(ABSfskEntity.TANMATSUID)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_TANMATSUID)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.SAKUJOFG)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_SAKUJOFG)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.KOSHINCOUNTER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINCOUNTER)
            '*����ԍ� 000021 2023/10/20 �ǉ��J�n
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.RRKNO)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_RRKNO)
            '*����ԍ� 000021 2023/10/20 �ǉ��I��
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.KOSHINNICHIJI)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINNICHIJI)
            strDelRonriSQL.Append(", ")
            strDelRonriSQL.Append(ABSfskEntity.KOSHINUSER)
            strDelRonriSQL.Append(" = ")
            strDelRonriSQL.Append(ABSfskEntity.PARAM_KOSHINUSER)
            strDelRonriSQL.Append(strWhere.ToString)
            m_strDelRonriSQL = strDelRonriSQL.ToString

            'DELETE�i�_���j �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            'DELETE�i�_���j �R���N�V�����Ƀp�����[�^��ǉ�
            '�[���h�c
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�폜�t���O
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000021 2023/10/20 �ǉ��J�n
            '����ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_RRKNO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000021 2023/10/20 �ǉ��I��
            '�X�V����
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�X�V���[�U
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000021 2023/10/20 �C���J�n
            ''�J�n�N��
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
            'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            ''�I���N��
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
            'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '�o�^�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_TOROKURENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000021 2023/10/20 �C���I��
            '�X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

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
        Const THIS_METHOD_NAME As String = "CreateDeleteButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass                  '�p�����[�^�N���X
        Dim strDeleteSQL As New StringBuilder                       '�����폜SQL��������
        Dim strWhere As New StringBuilder                           '�X�V�폜SQL��Where��������

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�X�V�폜Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABSfskEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.GYOMUNAISHU_CD)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
            strWhere.Append(" AND ")
            '*����ԍ� 000021 2023/10/20 �C���J�n
            'strWhere.Append(ABSfskEntity.STYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_STYM)
            'strWhere.Append(" AND ")
            'strWhere.Append(ABSfskEntity.EDYM)
            'strWhere.Append(" = ")
            'strWhere.Append(ABSfskEntity.KEY_EDYM)
            strWhere.Append(ABSfskEntity.TOROKURENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_TOROKURENBAN)
            '*����ԍ� 000021 2023/10/20 �C���I��
            strWhere.Append(" AND ")
            strWhere.Append(ABSfskEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABSfskEntity.KEY_KOSHINCOUNTER)

            'DELETE�i�����j SQL���̍쐬
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABSfskEntity.TABLE_NAME)
            strDeleteSQL.Append(strWhere.ToString)
            m_strDeleteSQL = strDeleteSQL.ToString

            'DELETE�i�����j �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            'DELETE(����) �R���N�V�����ɃL�[����ǉ�
            '�Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '�Ɩ�����ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000021 2023/10/20 �C���J�n
            ''�J�n�N��
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYM
            'm_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ''�I���N��
            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYM
            'm_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '�o�^�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_TOROKURENBAN
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            '*����ԍ� 000021 2023/10/20 �C���I��
            '�X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_KOSHINCOUNTER
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
    '* ����ԍ� 000013 2005/06/16 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue As String)
    '* 
    '* �@�\�@�@       ���t��}�X�^�̃f�[�^�������`�F�b�N���s���܂��B
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
            'm_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()
                Case ABSfskEntity.JUMINCD                               '�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SHICHOSONCD                           '�s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.KYUSHICHOSONCD                        '���s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.GYOMUCD                               '�Ɩ��R�[�h
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_GYOMUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.GYOMUNAISHU_CD                        '�Ɩ�����ʃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_GYOMUNAISHU_CD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                '*����ԍ� 000021 2023/10/20 �C���J�n
                'Case ABSfskEntity.STYM                                  '�J�n�N��
                '    If Not (strValue = String.Empty Or strValue = "000000") Then
                '        m_cfDateClass.p_strDateValue = strValue + "01"
                '        If (Not m_cfDateClass.CheckDate()) Then
                '            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '            '�G���[��`���擾
                '            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_STYM)
                '            '��O�𐶐�
                '            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                '        End If
                '    End If
                'Case ABSfskEntity.EDYM                                  '�I���N��
                '    If Not (strValue = String.Empty Or strValue = "000000" Or strValue = "999999") Then
                '        m_cfDateClass.p_strDateValue = strValue + "01"
                '        If (Not m_cfDateClass.CheckDate()) Then
                '            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '            '�G���[��`���擾
                '            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_EDYM)
                '            '��O�𐶐�
                '            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                '        End If
                '    End If
                Case ABSfskEntity.STYMD                                  '�J�n�N����
                    If (Not (strValue = String.Empty Or strValue = ALL0_YMD)) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_STYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                Case ABSfskEntity.EDYMD                                  '�I���N����
                    If (Not (strValue = String.Empty Or strValue = ALL0_YMD Or strValue = ALL9_YMD)) Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_EDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If
                '*����ԍ� 000021 2023/10/20 �C���I��
                Case ABSfskEntity.SFSKDATAKB                            '���t��f�[�^�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKDATAKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKKANNAIKANGAIKB                    '���t��Ǔ��ǊO�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKKANNAIKANGAIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKKANAMEISHO                        '���t��J�i����
                    '*����ԍ� 000009 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000009 2003/10/30 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKKANAMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKKANJIMEISHO                       '���t�抿������
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKKANJIMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKYUBINNO                           '���t��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKZJUSHOCD                          '���t��Z���R�[�h
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKJUSHO                             '���t��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKBANCHI                            '���t��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKKATAGAKI                          '���t�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKRENRAKUSAKI1                      '���t��A����1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKRENRAKUSAKI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKRENRAKUSAKI2                      '���t��A����2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKRENRAKUSAKI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKGYOSEIKUCD                        '���t��s����R�[�h
                    '* ����ԍ� 000014 2005/12/14 �C���J�n
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* ����ԍ� 000014 2005/12/14 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKGYOSEIKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKGYOSEIKUMEI                       '���t��s���於
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKGYOSEIKUMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUCD1                          '���t��n��R�[�h1
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUCD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUMEI1                         '���t��n�於1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUMEI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUCD2                          '���t��n��R�[�h2
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUCD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUMEI2                         '���t��n�於2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUMEI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUCD3                          '���t��n��R�[�h3
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUCD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SFSKCHIKUMEI3                         '���t��n�於3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SFSKCHIKUMEI3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.RESERVE                               '���U�[�u
                    '�������Ȃ�
                Case ABSfskEntity.TANMATSUID                            '�[��ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SAKUJOFG                              '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.KOSHINCOUNTER                         '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SAKUSEINICHIJI                        '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.SAKUSEIUSER                           '�쐬���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.KOSHINNICHIJI                         '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABSfskEntity.KOSHINUSER                            '�X�V���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_KOSHINUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
            '*����ԍ� 000021 2023/10/20 �ǉ��J�n
                Case ABSfskEntity.TOROKURENBAN                          '�o�^�A��
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '*����ԍ� 000022 2023/12/05 �C���J�n
                        ''��O�𐶐�
                        'Throw New UFAppException("�������ړ��̓G���[�F�`�a���t��@�o�^�A��", UFAppException.ERR_EXCEPTION)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_TOROKURENBAN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        '*����ԍ� 000022 2023/12/05 �C���I��
                    End If

                Case ABSfskEntity.RRKNO                                 '����ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '*����ԍ� 000022 2023/12/05 �C���J�n
                        ''��O�𐶐�
                        'Throw New UFAppException("�������ړ��̓G���[�F�`�a���t��@����ԍ�", UFAppException.ERR_EXCEPTION)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABSFSKB_RDBDATATYPE_RRKNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        '*����ԍ� 000022 2023/12/05 �C���I��
                    End If
                    '*����ԍ� 000021 2023/10/20 �ǉ��I��
            End Select

            ' �f�o�b�O���O�o��
            'm_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

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
    '* ���\�b�h��     �r�p�k���E�p�����[�^�R���N�V�����쐬
    '* 
    '* �\��           Private Function CreateSql_Param(ByVal strJuminCD As String, 
    '*                                                 ByVal strGyomuCD As String, 
    '*                                                 ByVal strGyomunaiSHUCD As String, 
    '*                                                 ByVal blnGyomunaiSHUCD As Boolean, 
    '*                                                 ByVal strKikanYMD As String, 
    '*                                                 ByVal blnSakujoFG As Boolean,
    '*                                                 ByVal cfUFParameterCollectionClass As UFParameterCollectionClass)
    '                                            As String
    '* 
    '* �@�\�@�@    �@�@�r�p�k���y�уp�����[�^�R���N�V�������쐬�������n���B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                blnGyomunaiSHUCD As Boolean   :�Ɩ�����ʃR�[�h�̗L���iTrue:�L��,False:�����j
    '*                strKikanYMD As String         :���ԔN����
    '*                blnSakujo As Boolean          :�폜�f�[�^�̗L��(True:�L��,False:����)
    '*                cfUFParameterCollectionClass As UFParameterCollectionClass  :�p�����[�^�R���N�V�����N���X
    '* 
    '* �߂�l         �r�p�k��(String)
    '*                �p�����[�^�R���N�V�����N���X(UFParameterCollectionClass)
    '************************************************************************************************
    Private Function CreateSql_Param(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                     ByVal strGyomunaiSHUCD As String, ByVal blnGyomunaiSHUCD As Boolean,
                                     ByVal strKikanYMD As String, ByVal blnSakujoFG As Boolean,
                                     ByVal cfUFParameterCollectionClass As UFParameterCollectionClass) As String
        Const THIS_METHOD_NAME As String = "CreateSql_Param"            '���̃��\�b�h��
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABSfskEntity.TABLE_NAME)

            '* ����ԍ� 000010 2004/08/27 �ǉ��J�n�i�{��j
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If
            '* ����ԍ� 000010 2004/08/27 �ǉ��I��

            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABSfskEntity.JUMINCD)                 '�Z���R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABSfskEntity.KEY_JUMINCD)
            If Not (strGyomuCD = "*1") Then
                '* ����ԍ� 000011 2005/01/25 �X�V�J�n�i�{��j���ʂ���x�ɓǂݍ���
                'strSQL.Append(" AND ")
                'strSQL.Append(ABSfskEntity.GYOMUCD)             '�Ɩ��R�[�h
                'strSQL.Append(" = ")
                'strSQL.Append(ABSfskEntity.KEY_GYOMUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.GYOMUCD)             '�Ɩ��R�[�h
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUCD)
                strSQL.Append(",'00')")
                '* ����ԍ� 000011 2005/01/25 �X�V�I���i�{��j

                '* ����ԍ� 000011 2005/01/25 �ǉ��J�n�i�{��j�P�������ǂݍ��ޗl�ɂ���
                m_cfRdbClass.p_intMaxRows = 1
                '* ����ԍ� 000011 2005/01/25 �ǉ��I���i�{��j�P�������ǂݍ��ޗl�ɂ���
            End If
            strSQL.Append(" AND ")
            '* ����ԍ� 000011 2005/01/25 �X�V�J�n�i�{��j���ʎ�ʂ���x�ɓǂݍ���
            'strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)      '�Ɩ�����ʃR�[�h
            'strSQL.Append(" = ")
            'strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
                strSQL.Append(" ,'')")
            Else
                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" = ")
                strSQL.Append("''")
            End If
            '* ����ԍ� 000011 2005/01/25 �X�V�I���i�{��j���ʎ�ʂ���x�ɓǂݍ���

            strSQL.Append(" AND (")
            strSQL.Append(ABSfskEntity.STYMD)                    '�J�n�N����
            strSQL.Append(" <= ")
            strSQL.Append(ABSfskEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.Append(ABSfskEntity.EDYMD)                    '�I���N����
            strSQL.Append(" >= ")
            strSQL.Append(ABSfskEntity.KEY_EDYMD)
            strSQL.Append(")")
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABSfskEntity.SAKUJOFG)            '�폜�t���O
                strSQL.Append(" <> 1")
            End If

            '* ����ԍ� 000011 2005/01/25 �ǉ��J�n�i�{��j��x�œǂ񂾂��̂��\�[�g���Đ擪�̂P����Ώۂɂ���
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" ORDER BY ")
                strSQL.Append(ABSfskEntity.GYOMUCD)
                strSQL.Append(" DESC,")
                strSQL.Append(ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" DESC")
            End If
            '* ����ԍ� 000011 2005/01/25 �ǉ��I���i�{��j��x�œǂ񂾂��̂��\�[�g���Đ擪�̂P����Ώۂɂ���

            '���������̃p�����[�^���쐬
            '�Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '* ����ԍ� 000011 2005/01/25 �ǉ��J�n�i�{��j
            ' �Ɩ�����ʃR�[�h
            If Not (strGyomuCD = "*1") Then
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
                If (blnGyomunaiSHUCD) Then
                    cfUFParameterClass.Value = strGyomunaiSHUCD
                Else
                    cfUFParameterClass.Value = String.Empty
                End If
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '* ����ԍ� 000011 2005/01/25 �ǉ��J�n�i�{��j

            ' �J�n�N��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYMD
            cfUFParameterClass.Value = strKikanYMD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �I���N��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYMD
            cfUFParameterClass.Value = strKikanYMD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return strSQL.ToString

    End Function
    '* ����ԍ� 000011 2005/01/25 �ǉ��J�n�i�{��j
    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^�X�L�[�}�擾
    '* 
    '* �\��           Public Function GetSfskSchemaBHoshu() As DataSet
    '* 
    '* �@�\�@�@    �@�@���t��}�X�^���X�L�[�}�擾
    '* 
    '* 
    '* �߂�l         DataSet : �擾�������t��}�X�^�̃X�L�[�}
    '************************************************************************************************
    Public Overloads Function GetSfskSchemaBHoshu() As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskSchemaBHoshu"              '���̃��\�b�h��

        Try
            If (m_csDataSchma Is Nothing) Then
                Dim strSQL As New StringBuilder                                 'SQL��������
                '�f�o�b�O���O�o��
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

                'SQL���̍쐬
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABSfskEntity.TABLE_NAME)

                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If
            Return (m_csDataSchma.Clone)
        Catch exAppException As UFAppException
            '���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            '���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            '�G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            '�V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Function
    '* ����ԍ� 000011 2005/01/25 �ǉ��I���i�{��j

    '*����ԍ� 000019 2023/03/10 �ǉ��J�n
#Region "���t��}�X�^���o_�W����"
    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^���o_�W����
    '* 
    '* �\��           Public Overloads Function GetSfskBHoshu_Hyojun(ByVal strJuminCD As String, 
    '*                                                        ByVal strGyomuCD As String, 
    '*                                                        ByVal strGyomunaiSHUCD As String, 
    '*                                                        ByVal strKikanYMD As String, 
    '*                                                        ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@���t��}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                strKikanYMD As String         :���ԔN����
    '*                blnSakujoFG As Boolean        :�폜�t���O
    '* 
    '* �߂�l         �擾�������t��}�X�^�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsSfskEntity    �C���e���Z���X�FABSfskEntity
    '************************************************************************************************
    Public Overloads Function GetSfskBHoshu_Hyojun(ByVal strJuminCD As String,
                                            ByVal strGyomuCD As String,
                                            ByVal strGyomunaiSHUCD As String,
                                            ByVal strKikanYMD As String,
                                            ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskBHoshu_Hyojun"       '���̃��\�b�h��
        Dim csSfskEntity As DataSet                                     '���t��}�X�^�f�[�^
        Dim strSQL As String                                            'SQL��������
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim blnSakujo As Boolean                                        '�폜�f�[�^�ǂݍ���

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Dim intWkKensu As Integer
            intWkKensu = m_cfRdbClass.p_intMaxRows()

            'SQL,�p�����[�^�R���N�V�����̍쐬
            blnSakujo = blnSakujoFG
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            strSQL = Me.CreateSql_Param_Hyojun(strJuminCD, strGyomuCD, strGyomunaiSHUCD, True, strKikanYMD, blnSakujo, cfUFParameterCollectionClass)

            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + Me.GetType.Name + "�z" +
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                            "�y���s���\�b�h��:GetDataSet�z" +
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            End If

            'SQL�̎��s DataSet�̎擾
            csSfskEntity = m_csDataSchma_Hyojun.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            m_cfRdbClass.p_intMaxRows = intWkKensu

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return csSfskEntity

    End Function
#End Region

#Region "�r�p�k���E�p�����[�^�R���N�V�����쐬_�W����"
    '************************************************************************************************
    '* ���\�b�h��     �r�p�k���E�p�����[�^�R���N�V�����쐬_�W����
    '* 
    '* �\��           Private Function CreateSql_Param_Hyojun(ByVal strJuminCD As String, 
    '*                                                 ByVal strGyomuCD As String, 
    '*                                                 ByVal strGyomunaiSHUCD As String, 
    '*                                                 ByVal blnGyomunaiSHUCD As Boolean, 
    '*                                                 ByVal strKikanYMD As String, 
    '*                                                 ByVal blnSakujoFG As Boolean,
    '*                                                 ByVal cfUFParameterCollectionClass As UFParameterCollectionClass)
    '                                            As String
    '* 
    '* �@�\�@�@    �@�@�r�p�k���y�уp�����[�^�R���N�V�������쐬�������n���B
    '* 
    '* ����           strJuminCD As String          :�Z���R�[�h
    '*                strGyomuCD As String          :�Ɩ��R�[�h
    '*                strGyomunaiSHUCD As String    :�Ɩ�����ʃR�[�h
    '*                blnGyomunaiSHUCD As Boolean   :�Ɩ�����ʃR�[�h�̗L���iTrue:�L��,False:�����j
    '*                strKikanYMD As String         :���ԔN����
    '*                blnSakujo As Boolean          :�폜�f�[�^�̗L��(True:�L��,False:����)
    '*                cfUFParameterCollectionClass As UFParameterCollectionClass  :�p�����[�^�R���N�V�����N���X
    '* 
    '* �߂�l         �r�p�k��(String)
    '*                �p�����[�^�R���N�V�����N���X(UFParameterCollectionClass)
    '************************************************************************************************
    Private Function CreateSql_Param_Hyojun(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                     ByVal strGyomunaiSHUCD As String, ByVal blnGyomunaiSHUCD As Boolean,
                                     ByVal strKikanYMD As String, ByVal blnSakujoFG As Boolean,
                                     ByVal cfUFParameterCollectionClass As UFParameterCollectionClass) As String
        Const THIS_METHOD_NAME As String = "CreateSql_Param_Hyojun"     '���̃��\�b�h��
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT ")
            ' ���t��}�X�^�̑S���ڃZ�b�g
            strSQL.AppendFormat(" {0}.*", ABSfskEntity.TABLE_NAME)
            ' ���t��}�X�^_�W���̍��ڃZ�b�g
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANAKATAGAKI)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTSUSHO)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANATSUSHO)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIMEIYUSENKB)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKEIJISHIMEI)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANJISHIMEI)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHINSEISHAMEI)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIKUCHOSONCD)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKMACHIAZACD)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTODOFUKEN)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIKUCHOSON)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKMACHIAZA)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD1)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD2)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD3)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKATAGAKICD)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKRENRAKUSAKIKB)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKBN)
            strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTOROKUYMD)

            strSQL.Append(" FROM ")
            strSQL.Append(ABSfskEntity.TABLE_NAME)

            ' ���t��}�X�^_�W����t��
            strSQL.AppendFormat(" LEFT OUTER JOIN {0} ", ABSfskHyojunEntity.TABLE_NAME)
            strSQL.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.JUMINCD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.JUMINCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUCD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.GYOMUCD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.GYOMUNAISHU_CD)
            strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.TOROKURENBAN,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.TOROKURENBAN)

            If (m_csDataSchma_Hyojun Is Nothing) Then
                m_csDataSchma_Hyojun = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If

            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.JUMINCD)               '�Z���R�[�h
            strSQL.Append(" = ")
            strSQL.Append(ABSfskEntity.KEY_JUMINCD)
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" AND ")
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUCD)           '�Ɩ��R�[�h
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUCD)
                strSQL.Append(",'00')")

                m_cfRdbClass.p_intMaxRows = 1
            End If
            strSQL.Append(" AND ")
            If Not (strGyomuCD = "*1") Then
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" IN(")
                strSQL.Append(ABSfskEntity.KEY_GYOMUNAISHU_CD)
                strSQL.Append(" ,'')")
            Else
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" = ")
                strSQL.Append("''")
            End If

            strSQL.Append(" AND (")
            strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.STYMD)                 '�J�n�N����
            strSQL.Append(" <= ")
            strSQL.Append(ABSfskEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.EDYMD)                 '�I���N����
            strSQL.Append(" >= ")
            strSQL.Append(ABSfskEntity.KEY_EDYMD)
            strSQL.Append(")")
            If Not (blnSakujoFG) Then
                strSQL.Append(" AND ")
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.SAKUJOFG)            '�폜�t���O
                strSQL.Append(" <> 1")
            End If

            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" ORDER BY ")
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUCD)
                strSQL.Append(" DESC,")
                strSQL.AppendFormat("{0}.{1}", ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD)
                strSQL.Append(" DESC")
            End If

            '���������̃p�����[�^���쐬
            '�Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ɩ��R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = strGyomuCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �Ɩ�����ʃR�[�h
            If Not (strGyomuCD = "*1") Then
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABSfskEntity.KEY_GYOMUNAISHU_CD
                If (blnGyomunaiSHUCD) Then
                    cfUFParameterClass.Value = strGyomunaiSHUCD
                Else
                    cfUFParameterClass.Value = String.Empty
                End If
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' �J�n�N��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_STYMD
            cfUFParameterClass.Value = strKikanYMD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �I���N��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABSfskEntity.KEY_EDYMD
            cfUFParameterClass.Value = strKikanYMD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch exAppException As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            ' ���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return strSQL.ToString

    End Function
#End Region

#Region "���t��}�X�^�X�L�[�}�擾_�W����"
    '************************************************************************************************
    '* ���\�b�h��     ���t��}�X�^�X�L�[�}�擾_�W����
    '* 
    '* �\��           Public Function GetSfskSchemaBHoshu_Hyojun() As DataSet
    '* 
    '* �@�\�@�@    �@�@���t��}�X�^���X�L�[�}�擾
    '* 
    '* 
    '* �߂�l         DataSet : �擾�������t��}�X�^�̃X�L�[�}
    '************************************************************************************************
    Public Overloads Function GetSfskSchemaBHoshu_Hyojun() As DataSet
        Const THIS_METHOD_NAME As String = "GetSfskSchemaBHoshu_Hyojun"         '���̃��\�b�h��

        Try
            If (m_csDataSchma_Hyojun Is Nothing) Then
                Dim strSQL As New StringBuilder                                 'SQL��������
                '�f�o�b�O���O�o��
                m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

                ' SQL���̍쐬
                strSQL.Append("SELECT ")
                ' ���t��}�X�^�̑S���ڃZ�b�g
                strSQL.AppendFormat(" {0}.*", ABSfskEntity.TABLE_NAME)
                ' ���t��}�X�^_�W���̍��ڃZ�b�g
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANAKATAGAKI)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTSUSHO)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANATSUSHO)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIMEIYUSENKB)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKEIJISHIMEI)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKANJISHIMEI)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHINSEISHAMEI)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHINSEISHAKANKEICD)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIKUCHOSONCD)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKMACHIAZACD)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTODOFUKEN)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKSHIKUCHOSON)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKMACHIAZA)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD1)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD2)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKBANCHICD3)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKATAGAKICD)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKRENRAKUSAKIKB)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKKBN)
                strSQL.AppendFormat(", {0}.{1}", ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.SFSKTOROKUYMD)

                strSQL.Append(" FROM ")
                strSQL.Append(ABSfskEntity.TABLE_NAME)

                ' ���t��}�X�^_�W����t��
                strSQL.AppendFormat(" LEFT OUTER JOIN {0} ", ABSfskHyojunEntity.TABLE_NAME)
                strSQL.AppendFormat(" ON {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.JUMINCD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.JUMINCD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUCD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.GYOMUCD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.GYOMUNAISHU_CD,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.GYOMUNAISHU_CD)
                strSQL.AppendFormat(" AND {0}.{1} = {2}.{3} ",
                                    ABSfskEntity.TABLE_NAME, ABSfskEntity.TOROKURENBAN,
                                    ABSfskHyojunEntity.TABLE_NAME, ABSfskHyojunEntity.TOROKURENBAN)

                m_csDataSchma_Hyojun = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            End If
            Return (m_csDataSchma_Hyojun.Clone)
        Catch exAppException As UFAppException
            '���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            '���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            '�G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y�G���[���e:" + exException.Message + "�z")
            '�V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Function
#End Region
    '*����ԍ� 000019 2023/03/10 �ǉ��I��

    '*����ԍ� 000021 2023/10/20 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     AB��[���t��ݐώ擾
    '* 
    '* �\��           Public Overloads Function GetABdainosfskruiseki(ByVal csDataRow As DataRow) As String
    '* 
    '* �@�\           AB��[���t��ݐς��o�^�A�Ԃ��擾
    '* 
    '* ����           csDataRow As DataRow          :�s�f�[�^
    '* 
    '* �߂�l         �o�^�A��
    '************************************************************************************************
    Public Overloads Function GetABdainosfskruiseki(ByVal csDataRow As DataRow) As String
        Const THIS_METHOD_NAME As String = "GetABdainosfskruiseki"      ' ���̃��\�b�h��
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim csSfskEntity As DataSet                                     ' ���t��}�X�^�f�[�^
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X
        Dim strTorokurenban As String                                   ' �o�^�A��

        Try

            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �X�L�[�}�擾����
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABSfskEntity.TABLE_NAME, False)
            Else
            End If

            'SQL���̍쐬
            strSQL.Append("SELECT ")
            strSQL.Append("MAX( ")
            strSQL.Append(ABDainoSfskRuisekiEntity.TOROKURENBAN)
            strSQL.Append(") ")
            strSQL.Append(" FROM ")
            strSQL.Append(ABDainoSfskRuisekiEntity.TABLE_NAME)

            strSQL.Append(" WHERE ")
            '�Z���R�[�h++
            strSQL.Append(ABDainoSfskRuisekiEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(csDataRow(ABDainoSfskRuisekiEntity.JUMINCD))
            '�Ɩ��R�[�h
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoSfskRuisekiEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(csDataRow(ABDainoSfskRuisekiEntity.GYOMUCD))
            '�Ɩ�����ʃR�[�h
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD)
            strSQL.Append(" = ")
            If (csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD).ToString = String.Empty) Then
                strSQL.Append("''")
            Else
                strSQL.Append(csDataRow(ABDainoSfskRuisekiEntity.GYOMUNAISHU_CD))
            End If
            '�����敪
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoSfskRuisekiEntity.SHORIKB)
            strSQL.Append(" IN ('")
            strSQL.Append(ABConstClass.SFSK_ADD)            ' �ǉ��i���t��j
            strSQL.Append("','")
            strSQL.Append(ABConstClass.SFSK_SHUSEI)         ' �C���i���t��j
            strSQL.Append("','")
            strSQL.Append(ABConstClass.SFSK_DELETE)         ' �폜�i���t��j
            strSQL.Append("')")

           ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                  "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                  "�y���s���\�b�h��:GetDataSet�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csSfskEntity = m_csDataSchma.Clone()
            csSfskEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csSfskEntity, ABSfskEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            '�f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�o�^�A�Ԃ��擾����B
            If (csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows.Count > 0) Then
                If (Not (IsDBNull(csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows(0).Item(0)))) Then
                    strTorokurenban = csSfskEntity.Tables(ABSfskEntity.TABLE_NAME).Rows(0).Item(0).ToString()
                End If
            End If

            '�o�^�A�Ԃ��擾�ł��Ȃ��ꍇ0���Z�b�g����
            If (strTorokurenban = String.Empty) Then
                strTorokurenban = "0"
            End If

            Return strTorokurenban

        Catch exAppException As UFAppException
            '���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData,
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" +
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" +
                                        "�y���[�j���O�R�[�h:" + exAppException.p_strErrorCode + "�z" +
                                        "�y���[�j���O���e:" + exAppException.Message + "�z")
            '���[�j���O���X���[����
            Throw exAppException

        Catch exException As Exception '�V�X�e���G���[���L���b�`
            '�G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + exException.Message + "�z")
            '�V�X�e���G���[���X���[����
            Throw exException

        End Try
    End Function
    '*����ԍ� 000021 2023/10/20 �ǉ��I��
#End Region

End Class
