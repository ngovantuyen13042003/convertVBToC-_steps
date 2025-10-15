'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a��[�}�X�^�c�`(ABDainoBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/06�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/02/25 000001     ���o��������Ɩ�����ʃR�[�h���͂����Ƃ��邪�A�Ɩ�����ʃR�[�h�� String.Empty�Ƃ��Ď擾����
'* 2003/03/27 000002     �G���[�����N���X�̎Q�Ɛ��"AB"�Œ�ɂ���
'* 2003/04/21 000003     �������`�F�b�N�ύX(�Ɩ�����ʁE�J�n�N���E�I���N��)
'* 2003/05/06 000004     �������`�F�b�N�ύX
'* 2003/05/20 000005     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/28 000006     RDB�A�N�Z�X���O�̏C��
'* 2003/09/11 000007     �[���h�c�������`�F�b�N��ANK�ɂ���
'* 2003/10/09 000008     �쐬���[�U�[�E�X�V���[�U�[�`�F�b�N�̕ύX
'* 2004/08/27 000009     ���x���P�F�i�{��j
'* 2005/01/25 000010     ���x���P�Q�F�i�{��j
'* 2005/06/16 000011     SQL����Insert,Update,Delete�̊e���\�b�h���Ă΂ꂽ���Ɋe���쐬����(�}���S���R)
'* 2006/12/22 000012     �{�X���擾���\�b�h��ǉ��B
'* 2007/03/09 000013     ��[���擾SQL�̃\�[�g����ύX(����)
'* 2010/03/05 000014     ��[�}�X�^���o�����̃I�[�o�[���[�h��ǉ��i��Áj
'* 2010/04/16 000015     VS2008�Ή��i��Áj
'* 2023/03/10 000016     �yAB-0970-1�z����GET�擾���ڕW�����Ή��i�����j
'* 2023/04/20 000017     �yAB-0970-1�z����GET�擾���ڕW�����Ή�_�b��Ή��i�����j
'* 2023/10/19 000018     �yAB-0840-1�z���t��Ǘ����ڒǉ��Ή��i����j
'* 2023/12/05 000019     �yAB-0840-1�z���t��Ǘ����ڒǉ��Ή�_�ǉ��C���i�����j
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

'************************************************************************************************
'*
'* ��[�}�X�^�擾���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABDainoBClass
#Region "�����o�ϐ�"
    ' �p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_strInsertSQL As String                        ' INSERT�pSQL
    Private m_strUpdateSQL As String                        ' UPDATE�pSQL
    Private m_strDelRonriSQL As String                      ' �_���폜�pSQL
    Private m_strDelButuriSQL As String                     ' �����폜�pSQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDelRonriUFParameterCollectionClass As UFParameterCollectionClass    '�_���폜�p�p�����[�^�R���N�V����
    Private m_cfDelButuriUFParameterCollectionClass As UFParameterCollectionClass   '�����폜�p�p�����[�^�R���N�V����
    Private m_cfParameterCollectionClass As UFParameterCollectionClass            '�Ǎ��p�p�����[�^�R���N�V����
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABDainoBClass"                       ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h

    '* ����ԍ� 000009 2004/08/27 �ǉ��J�n�i�{��j
    Public m_blnBatch As Boolean = False               '�o�b�`�t���O
    Private m_csDataSchma As DataSet   '�X�L�[�}�ۊǗp�f�[�^�Z�b�g
    '* ����ԍ� 000009 2004/08/27 �ǉ��I��
    '* ����ԍ� 000018 2023/10/19 �C���J�n
    Private Const ALL0_YMD As String = "00000000"            ' �N�����I�[���O
    Private Const ALL9_YMD As String = "99999999"            ' �N�����I�[���X
    '* ����ԍ� 000018 2023/10/19 �C���I��

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '*                               ByVal cfConfigDataClass As UFConfigDataClass, 
    '*                               ByVal cfRdbClass As UFRdbClass)
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
        m_cfLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' �p�����[�^�̃����o�ϐ�������
        m_strInsertSQL = String.Empty
        m_strUpdateSQL = String.Empty
        m_strDelRonriSQL = String.Empty
        m_strDelButuriSQL = String.Empty
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDelRonriUFParameterCollectionClass = Nothing
        m_cfDelButuriUFParameterCollectionClass = Nothing
        m_cfParameterCollectionClass = Nothing
        '* ����ԍ� 000009 2004/08/27 �ǉ��J�n�i�{��j
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABDainoEntity.TABLE_NAME, ABDainoEntity.TABLE_NAME, False)
        '* ����ԍ� 000009 2004/08/27 �ǉ��I��
    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^���o
    '* 
    '* �\��           Public Function GetDainoBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD    : �Z���R�[�h
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal strJuminCD As String) As DataSet
        Return GetDainoBHoshu(strJuminCD, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^���o
    '* 
    '* �\��           Public Function GetDainoBHoshu(ByVal strJuminCD As String,
    '*                                               ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD    : �Z���R�[�h
    '*                blnSakujoFG  : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal strJuminCD As String, _
                                             ByVal blnSakujoFG As Boolean) As DataSet
        Const THIS_METHOD_NAME As String = "GetDainoBHoshu"
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                            '�f�[�^�Z�b�g
        Dim strSQL As StringBuilder = New StringBuilder("")

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            ' �Ȃ�

            ' ���������L�[�̃`�F�b�N
            ' �Ȃ�

            ' SQL���̍쐬    
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            If Not blnSakujoFG Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABDainoEntity.GYOMUCD)
            strSQL.Append(" ASC, ")
            strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            strSQL.Append(" ASC")
            '*����ԍ� 000013 2007/03/09 �ǉ��J�n
            strSQL.Append(", ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" ASC")
            '*����ԍ� 000013 2007/03/09 �ǉ��I��
            strSQL.Append(";")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000006 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            '* ����ԍ� 000010 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                            "�y���s���\�b�h��:GetDataSet�z" + _
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            End If
            '* ����ԍ� 000010 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
            '*����ԍ� 000006 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            '* ����ԍ� 000009 2004/08/27 �X�V�J�n�i�{��j
            'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '* ����ԍ� 000009 2004/08/27 �X�V�I��


            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp

        End Try

        Return csDataSet

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^���o
    '* 
    '* �\��           Public Function GetDainoBHoshu(ByVal strJuminCD As String,
    '*                                               ByVal strGyomuCD As String,
    '*                                               ByVal strGyomunaiSHUCD As String,
    '*                                               ByVal strKikanYMD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD        : �Z���R�[�h
    '*                strGyomuCD        : �Ɩ��R�[�h
    '*                strGyomunaiSHUCD  : �Ɩ�����ʃR�[�h
    '*                strKikanYM        : ���ԔN����
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                             ByVal strGyomunaiSHUCD As String, ByVal strKikanYMD As String) As DataSet
        Return GetDainoBHoshu(strJuminCD, strGyomuCD, strGyomunaiSHUCD, strKikanYMD, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^���o
    '* 
    '* �\��           Public Function GetDainoBHoshu(ByVal strJuminCD As String,
    '*                                               ByVal strGyomuCD As String,
    '*                                               ByVal strGyomunaiSHUCD As String,
    '*                                               ByVal strKikanYMD As String,
    '*                                               ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD        : �Z���R�[�h
    '*                strGyomuCD        : �Ɩ��R�[�h
    '*                strGyomunaiSHUCD  : �Ɩ�����ʃR�[�h
    '*                strKikanYMD       : ���ԔN����
    '*                blnSakujoFG       : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                             ByVal strGyomunaiSHUCD As String, ByVal strKikanYMD As String,
                                             ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetDainoBHoshu"
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                            '�f�[�^�Z�b�g
        Dim strSQL As StringBuilder
        Dim cfDateClass As UFDateClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            ' �Ȃ�

            '* ����ԍ� 000010 2005/01/25 �ǉ��J�n�i�{��j�P�������ǂݍ��ޗl�ɂ���
            Dim intWkKensu As Integer
            intWkKensu = m_cfRdbClass.p_intMaxRows()
            '* ����ԍ� 000010 2005/01/25 �ǉ��I���i�{��j�P�������ǂݍ��ޗl�ɂ���

            ' SQL���̍쐬    
            strSQL = New StringBuilder()
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            If Not (strGyomuCD = "*1") Then
                '* ����ԍ� 000010 2005/01/25 �X�V�J�n�i�{��j���ʑ�[����x�ɓǂ�
                'strSQL.Append(" AND ")
                'strSQL.Append(ABDainoEntity.GYOMUCD)
                'strSQL.Append(" = ")
                'strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.GYOMUCD)
                strSQL.Append(" IN(")
                strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                strSQL.Append(",'00')")
                '* ����ԍ� 000010 2005/01/25 �X�V�I���i�{��j���ʑ�[����x�ɓǂ�

                '* ����ԍ� 000010 2005/01/25 �ǉ��J�n�i�{��j�P�������ǂݍ��ޗl�ɂ���
                m_cfRdbClass.p_intMaxRows = 1
                '* ����ԍ� 000010 2005/01/25 �ǉ��I���i�{��j�P�������ǂݍ��ޗl�ɂ���
            End If
            strSQL.Append(" AND ")

            '* ����ԍ� 000010 2005/01/25 �X�V�J�n�i�{��j��ʖ�������x�ɓǂ�
            'strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            'strSQL.Append(" = ")
            'strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                strSQL.Append(" IN(")
                strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
                strSQL.Append(" ,'')")
            Else
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                strSQL.Append(" = ")
                strSQL.Append("''")
            End If
            '* ����ԍ� 000010 2005/01/25 �X�V�I���i�{��j��ʖ�������x�ɓǂ�

            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" <= ")
            strSQL.Append(ABDainoEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.EDYMD)
            strSQL.Append(" >= ")
            strSQL.Append(ABDainoEntity.KEY_EDYMD)
            If Not blnSakujoFG Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If

            '* ����ԍ� 000010 2005/01/25 �ǉ��J�n�i�{��j��x�œǂ񂾂��̂��\�[�g���Đ擪�̂P����Ώۂɂ���
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" ORDER BY ")
                strSQL.Append(ABDainoEntity.GYOMUCD)
                strSQL.Append(" DESC,")
                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                strSQL.Append(" DESC")
            End If
            '* ����ԍ� 000010 2005/01/25 �ǉ��I���i�{��j��x�œǂ񂾂��̂��\�[�g���Đ擪�̂P����Ώۂɂ���

            strSQL.Append(";")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '* ����ԍ� 000010 2005/01/25 �X�V�J�n�i�{��jIf���ň͂�
            If Not (strGyomuCD = "*1") Then
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                cfUFParameterClass.Value = strGyomuCD
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '* ����ԍ� 000010 2005/01/25 �X�V�I���i�{��jIf���ň͂�

            ' ���������̃p�����[�^���쐬
            If Not (strGyomuCD = "*1") Then
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                cfUFParameterClass.Value = strGyomunaiSHUCD
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
            If (strKikanYMD.Trim.Length = 6) Then
                If (strKikanYMD.Trim = "000000") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                ElseIf (strKikanYMD.Trim = "999999") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                Else
                    cfDateClass = New UFDateClass(m_cfConfigDataClass)
                    cfDateClass.p_enDateSeparator = UFDateSeparator.None
                    '* ����ԍ� 000018 2023/10/19 �C���J�n
                    'cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                    cfDateClass.p_strDateValue = strKikanYMD.Trim + "00"
                    '* ����ԍ� 000018 2023/10/19 �C���I��
                    cfUFParameterClass.Value = cfDateClass.GetLastDay()
                End If
            Else
                cfUFParameterClass.Value = strKikanYMD
            End If
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
            If (strKikanYMD.Trim.Length = 6) Then
                If (strKikanYMD.Trim = "000000") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                ElseIf (strKikanYMD.Trim = "999999") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                Else
                    '* ����ԍ� 000018 2023/10/19 �C���J�n
                    'cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                    '* ����ԍ� 000018 2023/10/19 �C���I��
                End If
            Else
                cfUFParameterClass.Value = strKikanYMD
            End If
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000006 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            '* ����ԍ� 000010 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + Me.GetType.Name + "�z" +
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                            "�y���s���\�b�h��:GetDataSet�z" +
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            End If
            '* ����ԍ� 000010 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
            '*����ԍ� 000006 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            '* ����ԍ� 000009 2004/08/27 �X�V�J�n�i�{��j
            'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '* ����ԍ� 000009 2004/08/27 �X�V�I��

            '* ����ԍ� 000010 2005/01/25 �ǉ��J�n�i�{��j�������Ԃ��ꍇ�́A�擪�Ɠ����Ɩ�����ʈȊO�̂��͍̂폜����
            '��̔ԍ��ň�x�쐬�������A�K�v�Ȃ��Ȃ����̂ō폜
            'If (strGyomuCD = "*1") Then
            '    If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count > 1) Then
            '        Dim csDataRow As DataRow
            '        Dim csDataTable As DataTable
            '        Dim intRowCount As Integer
            '        csDataTable = csDataSet.Tables(ABDainoEntity.TABLE_NAME)
            '        csDataRow = csDataTable.Rows(0)
            '        For intRowCount = csDataTable.Rows.Count - 1 To 1 Step -1
            '            If (CType(csDataRow.Item(ABDainoEntity.GYOMUNAISHU_CD), String) <> CType(csDataTable.Rows(intRowCount).Item(ABDainoEntity.GYOMUNAISHU_CD), String)) Then
            '                csDataTable.Rows(intRowCount).Delete()
            '            End If
            '        Next
            '        csDataTable.AcceptChanges()
            '    End If
            'End If
            '* ����ԍ� 000010 2005/01/25 �ǉ��I���i�{��j�������Ԃ��ꍇ�́A�擪�Ɠ����Ɩ�����ʈȊO�̂��͍̂폜����

            '* ����ԍ� 000010 2005/01/25 �ǉ��J�n�i�{��j�P�������ǂݍ��ޗl�ɂ������̂����ɖ߂�
            m_cfRdbClass.p_intMaxRows = intWkKensu
            '* ����ԍ� 000010 2005/01/25 �ǉ��I���i�{��j�P�������ǂݍ��ޗl�ɂ������̂����ɖ߂�

            '* ����ԍ� 000010 2005/01/25 �폜�J�n�i�{��j
            '' �f�[�^�����`�F�b�N
            'If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

            '    ' �Ɩ�����ʂ��w�肳��Ă����ꍇ
            '    If Not (strGyomunaiSHUCD = String.Empty) Then

            '        ' SQL���̍쐬
            '        strSQL = Nothing
            '        strSQL = New StringBuilder()
            '        strSQL.Append("SELECT * FROM ")
            '        strSQL.Append(ABDainoEntity.TABLE_NAME)
            '        strSQL.Append(" WHERE ")
            '        strSQL.Append(ABDainoEntity.JUMINCD)
            '        strSQL.Append(" = ")
            '        strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            '        If Not (strGyomuCD = "*1") Then
            '            strSQL.Append(" AND ")
            '            strSQL.Append(ABDainoEntity.GYOMUCD)
            '            strSQL.Append(" = ")
            '            strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
            '        End If
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            '        strSQL.Append(" = ")
            '        strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.STYM)
            '        strSQL.Append(" <= ")
            '        strSQL.Append(ABDainoEntity.KEY_STYM)
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.EDYM)
            '        strSQL.Append(" >= ")
            '        strSQL.Append(ABDainoEntity.KEY_EDYM)
            '        If Not blnSakujoFG Then
            '            strSQL.Append(" AND ")
            '            strSQL.Append(ABDainoEntity.SAKUJOFG)
            '            strSQL.Append(" <> 1")
            '        End If
            '        strSQL.Append(";")

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            '        cfUFParameterCollectionClass = New UFParameterCollectionClass()

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            '        cfUFParameterClass.Value = strJuminCD
            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        If Not (strGyomuCD = "*1") Then
            '            ' ���������̃p�����[�^���쐬
            '            cfUFParameterClass = New UFParameterClass()
            '            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            '            cfUFParameterClass.Value = strGyomuCD
            '            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            '        End If

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            '        cfUFParameterClass.Value = ""
            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            '        cfUFParameterClass.Value = strKikanYM
            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            '        cfUFParameterClass.Value = strKikanYM
            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        '*����ԍ� 000006 2003/08/28 �C���J�n
            '        '' RDB�A�N�Z�X���O�o��
            '        'm_cfLogClass.RdbWrite(m_cfControlData, _
            '        '                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '        '                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '        '                    "�y���s���\�b�h��:GetDataSet�z" + _
            '        '                    "�ySQL���e:" + strSQL.ToString + "�z")

            '        ' RDB�A�N�Z�X���O�o��
            '        m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                    "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                                    "�y���s���\�b�h��:GetDataSet�z" + _
            '                                    "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '        '*����ԍ� 000006 2003/08/28 �C���I��

            '        ' SQL�̎��s DataSet�̎擾
            '        '* ����ԍ� 000009 2004/08/27 �X�V�J�n�i�{��j
            '        'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '        csDataSet = m_csDataSchma.Clone()
            '        csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '        '* ����ԍ� 000009 2004/08/27 �X�V�I��


            '    End If

            'End If

            '' �f�[�^�����`�F�b�N
            'If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

            '    ' �Ɩ��R�[�h�i�h00�h�ȊO�j���w�肳��Ă����ꍇ
            '    If Not (strGyomuCD = "00") Then

            '        ' SQL���̍쐬
            '        strSQL = Nothing
            '        strSQL = New StringBuilder()
            '        strSQL.Append("SELECT * FROM ")
            '        strSQL.Append(ABDainoEntity.TABLE_NAME)
            '        strSQL.Append(" WHERE ")
            '        strSQL.Append(ABDainoEntity.JUMINCD)
            '        strSQL.Append(" = ")
            '        strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            '        If Not (strGyomuCD = "*1") Then
            '            strSQL.Append(" AND ")
            '            strSQL.Append(ABDainoEntity.GYOMUCD)
            '            strSQL.Append(" = ")
            '            strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
            '        End If
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            '        strSQL.Append(" = ")
            '        strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.STYM)
            '        strSQL.Append(" <= ")
            '        strSQL.Append(ABDainoEntity.KEY_STYM)
            '        strSQL.Append(" AND ")
            '        strSQL.Append(ABDainoEntity.EDYM)
            '        strSQL.Append(" >= ")
            '        strSQL.Append(ABDainoEntity.KEY_EDYM)
            '        If Not blnSakujoFG Then
            '            strSQL.Append(" AND ")
            '            strSQL.Append(ABDainoEntity.SAKUJOFG)
            '            strSQL.Append(" <> 1")
            '        End If
            '        strSQL.Append(";")

            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            '        cfUFParameterCollectionClass = New UFParameterCollectionClass()

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            '        cfUFParameterClass.Value = strJuminCD
            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        If Not (strGyomuCD = "*1") Then
            '            ' ���������̃p�����[�^���쐬
            '            cfUFParameterClass = New UFParameterClass()
            '            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            '            cfUFParameterClass.Value = "00"
            '            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '            cfUFParameterCollectionClass.Add(cfUFParameterClass)
            '        End If

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            '        cfUFParameterClass.Value = ""
            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            '        cfUFParameterClass.Value = strKikanYM
            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        ' ���������̃p�����[�^���쐬
            '        cfUFParameterClass = New UFParameterClass()
            '        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            '        cfUFParameterClass.Value = strKikanYM
            '        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '        cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '        '*����ԍ� 000006 2003/08/28 �C���J�n
            '        '' RDB�A�N�Z�X���O�o��
            '        'm_cfLogClass.RdbWrite(m_cfControlData, _
            '        '                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '        '                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '        '                    "�y���s���\�b�h��:GetDataSet�z" + _
            '        '                    "�ySQL���e:" + strSQL.ToString + "�z")

            '        ' RDB�A�N�Z�X���O�o��
            '        m_cfLogClass.RdbWrite(m_cfControlData, _
            '                                    "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                                    "�y���s���\�b�h��:GetDataSet�z" + _
            '                                    "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            '        '*����ԍ� 000006 2003/08/28 �C���I��

            '        ' SQL�̎��s DataSet�̎擾
            '        '* ����ԍ� 000009 2004/08/27 �X�V�J�n�i�{��j
            '        'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            '        csDataSet = m_csDataSchma.Clone()
            '        csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '        '* ����ԍ� 000009 2004/08/27 �X�V�I��

            '    End If

            'End If
            '* ����ԍ� 000010 2005/01/25 �폜�I���i�{��j

            ' �N���X�̉��
            strSQL = Nothing

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

        Return csDataSet

    End Function


    '*����ԍ� 000014 2010/03/05 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^���o(���ް۰��)
    '* 
    '* �\��           Public Overloads Function GetDainoBHoshu(ByVal cABDainoGetParaX As ABDainoGetParaXClass) As DataSet
    '* 
    '* 
    '* �@�\�@�@    �@ ��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           cABDainoGetParaX      :   ��[���p�����[�^�N���X
    '*  
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetDainoBHoshu(ByVal cABDainoGetParaX As ABDainoGetParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetDainoBHoshu"             ' ���\�b�h��
        Dim csDainoEntity As DataSet                                    ' ��[�}�X�^�f�[�^
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                      ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X
        Dim blnAndFg As Boolean = False                                 ' AND����t���O
        Dim strWork As String
        Dim cfDateClass As UFDateClass

        Try
            '�f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �C���X�^���X��
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' �X�L�[�}�擾����
            If (m_csDataSchma Is Nothing) Then
                m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString, ABDainoEntity.TABLE_NAME, False)
            Else
            End If

            ' SQL���̍쐬
            ' SELECT��
            strSQL.Append("SELECT * ")

            strSQL.Append(" FROM ").Append(ABDainoEntity.TABLE_NAME)

            ' WHERE��
            strSQL.Append(" WHERE ")
            '---------------------------------------------------------------------------------
            ' �Z���R�[�h
            If (cABDainoGetParaX.p_strJuminCD.Trim <> String.Empty) Then
                ' �Z���R�[�h���ݒ肳��Ă���ꍇ

                strSQL.Append(ABDainoEntity.JUMINCD).Append(" = ")
                strSQL.Append(ABDainoEntity.KEY_JUMINCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
                cfUFParameterClass.Value = CStr(cABDainoGetParaX.p_strJuminCD)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �Ɩ��R�[�h
            If (cABDainoGetParaX.p_strGyomuCD.Trim <> String.Empty) Then
                ' �Ɩ��R�[�h���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABDainoEntity.GYOMUCD).Append(" = ")
                strSQL.Append(ABDainoEntity.KEY_GYOMUCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                cfUFParameterClass.Value = cABDainoGetParaX.p_strGyomuCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �Ɩ�����ʃR�[�h
            If (cABDainoGetParaX.p_strGyomuneiSHU_CD.Trim <> String.Empty) Then
                ' �Ɩ�����ʃR�[�h���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD).Append(" = ")
                strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                cfUFParameterClass.Value = cABDainoGetParaX.p_strGyomuneiSHU_CD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If

            '---------------------------------------------------------------------------------
            ' ����
            '* ����ԍ� 000018 2023/10/19 �C���J�n
            'If (cABDainoGetParaX.p_strKikanYM.Trim <> String.Empty) Then
            If (cABDainoGetParaX.p_strKikanYMD.Trim <> String.Empty) Then
            '* ����ԍ� 000018 2023/10/19 �C���I��
                ' ���Ԃ��ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append("(")
                strSQL.Append(ABDainoEntity.STYMD)                    '�J�n�N����
                strSQL.Append(" <= ")
                strSQL.Append(ABDainoEntity.KEY_STYMD)
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.EDYMD)                    '�I���N����
                strSQL.Append(" >= ")
                strSQL.Append(ABDainoEntity.KEY_EDYMD)
                strSQL.Append(")")

                ' �J�n�N����
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
                '* ����ԍ� 000018 2023/10/19 �C���J�n
                'If (cABDainoGetParaX.p_strKikanYM.Trim.Length = 6) Then
                '    If (cABDainoGetParaX.p_strKikanYM.Trim = "000000") Then
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                '    ElseIf (cABDainoGetParaX.p_strKikanYM.Trim = "999999") Then
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "99"
                '    Else
                '        cfDateClass = New UFDateClass(m_cfConfigDataClass)
                '        cfDateClass.p_enDateSeparator = UFDateSeparator.None
                '        cfDateClass.p_strDateValue = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                '        cfUFParameterClass.Value = cfDateClass.GetLastDay()
                '    End If
                'Else
                '    cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM
                'End If

                If (cABDainoGetParaX.p_strKikanYMD.Trim.Length = 6) Then
                    If (cABDainoGetParaX.p_strKikanYMD.Trim = ALL0_YMD) Then
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "00"
                    ElseIf (cABDainoGetParaX.p_strKikanYMD.Trim = ALL9_YMD) Then
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "99"
                    Else
                        cfDateClass = New UFDateClass(m_cfConfigDataClass)
                        cfDateClass.p_enDateSeparator = UFDateSeparator.None
                        cfDateClass.p_strDateValue = cABDainoGetParaX.p_strKikanYMD.Trim + "00"
                        cfUFParameterClass.Value = cfDateClass.GetLastDay()
                    End If
                Else
                    cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD
                End If
                '* ����ԍ� 000018 2023/10/19 �C���I��

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' �I���N����
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
                '* ����ԍ� 000018 2023/10/19 �C���J�n
                'If (cABDainoGetParaX.p_strKikanYM.Trim.Length = 6) Then
                '    If (cABDainoGetParaX.p_strKikanYM.Trim = "000000") Then
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                '    ElseIf (cABDainoGetParaX.p_strKikanYM.Trim = "999999") Then
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "99"
                '    Else
                '        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM.Trim + "01"
                '    End If
                'Else
                '    cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYM
                'End If
                If (cABDainoGetParaX.p_strKikanYMD.Trim.Length = 6) Then
                    If (cABDainoGetParaX.p_strKikanYMD.Trim = ALL0_YMD) Then
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "00"
                    ElseIf (cABDainoGetParaX.p_strKikanYMD.Trim = ALL9_YMD) Then
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "99"
                    Else
                        cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD.Trim + "00"
                    End If
                Else
                    cfUFParameterClass.Value = cABDainoGetParaX.p_strKikanYMD
                End If
                '* ����ԍ� 000018 2023/10/19 �C���I��

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �폜�t���O
            If (cABDainoGetParaX.p_strSakujoFG.Trim = String.Empty) Then
                ' �폜�t���O�w�肪�Ȃ��ꍇ�A�폜�f�[�^�͒��o���Ȃ�
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If
                strSQL.Append(ABDainoEntity.SAKUJOFG).Append(" <> '1'")

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
            csDainoEntity = m_csDataSchma.Clone()
            csDainoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csDainoEntity, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


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

        Return csDainoEntity

    End Function
    '*����ԍ� 000014 2010/03/05 �ǉ��I��


    '************************************************************************************************
    '* ���\�b�h��     ���[�}�X�^���o
    '* 
    '* �\��           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD    : �Z���R�[�h
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetHiDainoBHoshu(ByVal strJuminCD As String) As DataSet
        Return GetHiDainoBHoshu(strJuminCD, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���[�}�X�^���o
    '* 
    '* �\��           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String,
    '*                                                 ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD    : �Z���R�[�h
    '*                blnSakujoFG   : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetHiDainoBHoshu(ByVal strJuminCD As String, _
                                               ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetHiDainoBHoshu"
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                            '�f�[�^�Z�b�g
        Dim strSQL As StringBuilder = New StringBuilder("")

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�p�����[�^�`�F�b�N
            '�Ȃ�

            '���������L�[�̃`�F�b�N
            '�Ȃ�

            ' SQL���̍쐬    
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.DAINOJUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            If Not blnSakujoFG Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABDainoEntity.GYOMUCD)
            strSQL.Append(" ASC, ")
            strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            strSQL.Append(" ASC")
            '*����ԍ� 000013 2007/03/09 �ǉ��J�n
            strSQL.Append(", ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" ASC")
            '*����ԍ� 000013 2007/03/09 �ǉ��I��
            strSQL.Append(";")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000006 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            '* ����ԍ� 000010 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                            "�y���s���\�b�h��:GetDataSet�z" + _
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            End If
            '* ����ԍ� 000010 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
            '*����ԍ� 000006 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            '* ����ԍ� 000009 2004/08/27 �X�V�J�n�i�{��j
            'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '* ����ԍ� 000009 2004/08/27 �X�V�I��


            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp

        End Try

        Return csDataSet

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���[�}�X�^���o
    '* 
    '* �\��           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String,
    '*                                                 ByVal strGyomuCD As String,
    '*                                                 ByVal strGyomunaiSHUCD As String,
    '*                                                 ByVal strKikanYMD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD        : �Z���R�[�h
    '*                strGyomuCD        : �Ɩ��R�[�h
    '*                strGyomunaiSHUCD  : �Ɩ�����ʃR�[�h
    '*                strKikanYM        : ���ԔN����
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetHiDainoBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                               ByVal strGyomunaiSHUCD As String, ByVal strKikanYMD As String) As DataSet
        Return GetHiDainoBHoshu(strJuminCD, strGyomuCD, strGyomunaiSHUCD, strKikanYMD, False)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     ���[�}�X�^���o
    '* 
    '* �\��           Public Function GetHiDainoBHoshu(ByVal strJuminCD As String,
    '*                                                 ByVal strGyomuCD As String,
    '*                                                 ByVal strGyomunaiSHUCD As String,
    '*                                                 ByVal strKikanYMD As String,
    '*                                                 ByVal blnSakujoFG As Boolean) As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD        : �Z���R�[�h
    '*                strGyomuCD        : �Ɩ��R�[�h
    '*                strGyomunaiSHUCD  : �Ɩ�����ʃR�[�h
    '*                strKikanYM        : ���ԔN����
    '*                blnSakujoFG       : �폜�t���O
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetHiDainoBHoshu(ByVal strJuminCD As String, ByVal strGyomuCD As String,
                                               ByVal strGyomunaiSHUCD As String, ByVal strKikanYMD As String,
                                               ByVal blnSakujoFG As Boolean) As DataSet

        Const THIS_METHOD_NAME As String = "GetHiDainoBHoshu"
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                            '�f�[�^�Z�b�g
        Dim strSQL As StringBuilder
        Dim cfDateClass As UFDateClass

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�p�����[�^�`�F�b�N
            '�Ȃ�

            '���������L�[�̃`�F�b�N
            '�Ȃ�

            ' SQL���̍쐬    
            strSQL = New StringBuilder
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.DAINOJUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            If Not (strGyomuCD = "*1") Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.GYOMUCD)
                strSQL.Append(" = ")
                strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
            End If
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" <= ")
            strSQL.Append(ABDainoEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.EDYMD)
            strSQL.Append(" >= ")
            strSQL.Append(ABDainoEntity.KEY_EDYMD)
            If Not blnSakujoFG Then
                strSQL.Append(" AND ")
                strSQL.Append(ABDainoEntity.SAKUJOFG)
                strSQL.Append(" <> 1")
            End If
            strSQL.Append(";")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            If Not (strGyomuCD = "*1") Then
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                cfUFParameterClass.Value = strGyomuCD
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = strGyomunaiSHUCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
            If (strKikanYMD.Trim.Length = 6) Then
                If (strKikanYMD.Trim = "000000") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                ElseIf (strKikanYMD.Trim = "999999") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                Else
                    cfDateClass = New UFDateClass(m_cfConfigDataClass)
                    cfDateClass.p_enDateSeparator = UFDateSeparator.None
                    '* ����ԍ� 000018 2023/10/19 �C���J�n
                    'cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                    cfDateClass.p_strDateValue = strKikanYMD.Trim + "00"
                    '* ����ԍ� 000018 2023/10/19 �C���I��
                    cfUFParameterClass.Value = cfDateClass.GetLastDay()
                End If
            Else
                cfUFParameterClass.Value = strKikanYMD
            End If
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
            If (strKikanYMD.Trim.Length = 6) Then
                If (strKikanYMD.Trim = "000000") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                ElseIf (strKikanYMD.Trim = "999999") Then
                    cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                Else
                    '* ����ԍ� 000018 2023/10/19 �C���J�n
                    'cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                    cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                    '* ����ԍ� 000018 2023/10/19 �C���I��
                End If
            Else
                cfUFParameterClass.Value = strKikanYMD
            End If
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '*����ԍ� 000006 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                            "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + strSQL.ToString + "�z")

            ' RDB�A�N�Z�X���O�o��
            '* ����ԍ� 000010 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
            If (m_blnBatch = False) Then
                m_cfLogClass.RdbWrite(m_cfControlData,
                                            "�y�N���X��:" + Me.GetType.Name + "�z" +
                                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                            "�y���s���\�b�h��:GetDataSet�z" +
                                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
            End If
            '* ����ԍ� 000010 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
            '*����ԍ� 000006 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            '* ����ԍ� 000009 2004/08/27 �X�V�J�n�i�{��j
            'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
            '* ����ԍ� 000009 2004/08/27 �X�V�I��

            '�f�[�^�����`�F�b�N
            If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

                '�Ɩ�����ʂ��w�肳��Ă����ꍇ
                If Not (strGyomunaiSHUCD = String.Empty) Then

                    'SQL���̍쐬
                    strSQL = Nothing
                    strSQL = New StringBuilder
                    strSQL.Append("SELECT * FROM ")
                    strSQL.Append(ABDainoEntity.TABLE_NAME)
                    strSQL.Append(" WHERE ")
                    strSQL.Append(ABDainoEntity.DAINOJUMINCD)
                    strSQL.Append(" = ")
                    strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD)
                    If Not (strGyomuCD = "*1") Then
                        strSQL.Append(" AND ")
                        strSQL.Append(ABDainoEntity.GYOMUCD)
                        strSQL.Append(" = ")
                        strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                    End If
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                    strSQL.Append(" = ")
                    strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.STYMD)
                    strSQL.Append(" <= ")
                    strSQL.Append(ABDainoEntity.KEY_STYMD)
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.EDYMD)
                    strSQL.Append(" >= ")
                    strSQL.Append(ABDainoEntity.KEY_EDYMD)
                    If Not blnSakujoFG Then
                        strSQL.Append(" AND ")
                        strSQL.Append(ABDainoEntity.SAKUJOFG)
                        strSQL.Append(" <> 1")
                    End If
                    strSQL.Append(";")

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
                    cfUFParameterCollectionClass = New UFParameterCollectionClass

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
                    cfUFParameterClass.Value = strJuminCD
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    If Not (strGyomuCD = "*1") Then
                        ' ���������̃p�����[�^���쐬
                        cfUFParameterClass = New UFParameterClass
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                        cfUFParameterClass.Value = strGyomuCD
                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfUFParameterCollectionClass.Add(cfUFParameterClass)
                    End If

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                    cfUFParameterClass.Value = ""
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
                    If (strKikanYMD.Trim.Length = 6) Then
                        If (strKikanYMD.Trim = "000000") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                        ElseIf (strKikanYMD.Trim = "999999") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                        Else
                            cfDateClass = New UFDateClass(m_cfConfigDataClass)
                            cfDateClass.p_enDateSeparator = UFDateSeparator.None
                            '* ����ԍ� 000018 2023/10/19 �C���J�n
                            'cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                            cfDateClass.p_strDateValue = strKikanYMD.Trim + "00"
                            '* ����ԍ� 000018 2023/10/19 �C���I��
                            cfUFParameterClass.Value = cfDateClass.GetLastDay()
                        End If
                    Else
                        cfUFParameterClass.Value = strKikanYMD
                    End If
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
                    If (strKikanYMD.Trim.Length = 6) Then
                        If (strKikanYMD.Trim = "000000") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                        ElseIf (strKikanYMD.Trim = "999999") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                        Else
                            '* ����ԍ� 000018 2023/10/19 �C���J�n
                            'cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                            '* ����ԍ� 000018 2023/10/19 �C���I��
                        End If
                    Else
                        cfUFParameterClass.Value = strKikanYMD
                    End If
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    '*����ԍ� 000006 2003/08/28 �C���J�n
                    '' RDB�A�N�Z�X���O�o��
                    'm_cfLogClass.RdbWrite(m_cfControlData, _
                    '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                    '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                    '                        "�y���s���\�b�h��:GetDataSet�z" + _
                    '                        "�ySQL���e:" + strSQL.ToString + "�z")

                    ' RDB�A�N�Z�X���O�o��
                    '* ����ԍ� 000010 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
                    If (m_blnBatch = False) Then
                        m_cfLogClass.RdbWrite(m_cfControlData,
                                                    "�y�N���X��:" + Me.GetType.Name + "�z" +
                                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                                    "�y���s���\�b�h��:GetDataSet�z" +
                                                    "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
                    End If
                    '* ����ԍ� 000010 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
                    '*����ԍ� 000006 2003/08/28 �C���I��

                    ' SQL�̎��s DataSet�̎擾
                    '* ����ԍ� 000009 2004/08/27 �X�V�J�n�i�{��j
                    'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                    csDataSet = m_csDataSchma.Clone()
                    csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
                    '* ����ԍ� 000009 2004/08/27 �X�V�I��


                End If

            End If

            '�f�[�^�����`�F�b�N
            If (csDataSet.Tables(ABDainoEntity.TABLE_NAME).Rows.Count = 0) Then

                '�Ɩ��R�[�h�i�h00�h�ȊO�j���w�肳��Ă����ꍇ
                If Not (strGyomuCD = "00") Then

                    ' SQL���̍쐬
                    strSQL = Nothing
                    strSQL = New StringBuilder
                    strSQL.Append("SELECT * FROM ")
                    strSQL.Append(ABDainoEntity.TABLE_NAME)
                    strSQL.Append(" WHERE ")
                    strSQL.Append(ABDainoEntity.DAINOJUMINCD)
                    strSQL.Append(" = ")
                    strSQL.Append(ABDainoEntity.KEY_DAINOJUMINCD)
                    If Not (strGyomuCD = "*1") Then
                        strSQL.Append(" AND ")
                        strSQL.Append(ABDainoEntity.GYOMUCD)
                        strSQL.Append(" = ")
                        strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
                    End If
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
                    strSQL.Append(" = ")
                    strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.STYMD)
                    strSQL.Append(" <= ")
                    strSQL.Append(ABDainoEntity.KEY_STYMD)
                    strSQL.Append(" AND ")
                    strSQL.Append(ABDainoEntity.EDYMD)
                    strSQL.Append(" >= ")
                    strSQL.Append(ABDainoEntity.KEY_EDYMD)
                    If Not blnSakujoFG Then
                        strSQL.Append(" AND ")
                        strSQL.Append(ABDainoEntity.SAKUJOFG)
                        strSQL.Append(" <> 1")
                    End If
                    strSQL.Append(";")

                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
                    cfUFParameterCollectionClass = New UFParameterCollectionClass

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
                    cfUFParameterClass.Value = strJuminCD
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    If Not (strGyomuCD = "*1") Then
                        ' ���������̃p�����[�^���쐬
                        cfUFParameterClass = New UFParameterClass
                        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
                        cfUFParameterClass.Value = "00"
                        ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                        cfUFParameterCollectionClass.Add(cfUFParameterClass)
                    End If

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
                    cfUFParameterClass.Value = ""
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
                    If (strKikanYMD.Trim.Length = 6) Then
                        If (strKikanYMD.Trim = "000000") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                        ElseIf (strKikanYMD.Trim = "999999") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                        Else
                            cfDateClass = New UFDateClass(m_cfConfigDataClass)
                            cfDateClass.p_enDateSeparator = UFDateSeparator.None
                            '* ����ԍ� 000018 2023/10/19 �C���J�n
                            'cfDateClass.p_strDateValue = strKikanYMD.Trim + "01"
                            cfDateClass.p_strDateValue = strKikanYMD.Trim + "00"
                            '* ����ԍ� 000018 2023/10/19 �C���I��
                            cfUFParameterClass.Value = cfDateClass.GetLastDay()
                        End If
                    Else
                        cfUFParameterClass.Value = strKikanYMD
                    End If
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
                    If (strKikanYMD.Trim.Length = 6) Then
                        If (strKikanYMD.Trim = "000000") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                        ElseIf (strKikanYMD.Trim = "999999") Then
                            cfUFParameterClass.Value = strKikanYMD.Trim + "99"
                        Else
                            '* ����ԍ� 000018 2023/10/19 �C���J�n
                            'cfUFParameterClass.Value = strKikanYMD.Trim + "01"
                            cfUFParameterClass.Value = strKikanYMD.Trim + "00"
                            '* ����ԍ� 000018 2023/10/19 �C���I��
                        End If
                    Else
                        cfUFParameterClass.Value = strKikanYMD
                    End If
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                    '*����ԍ� 000006 2003/08/28 �C���J�n
                    '' RDB�A�N�Z�X���O�o��
                    'm_cfLogClass.RdbWrite(m_cfControlData, _
                    '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                    '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                    '                        "�y���s���\�b�h��:GetDataSet�z" + _
                    '                        "�ySQL���e:" + strSQL.ToString + "�z")

                    ' RDB�A�N�Z�X���O�o��
                    '* ����ԍ� 000010 2005/01/25 �X�V�J�n�i�{��jIf ���ň͂�
                    If (m_blnBatch = False) Then
                        m_cfLogClass.RdbWrite(m_cfControlData,
                                                    "�y�N���X��:" + Me.GetType.Name + "�z" +
                                                    "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" +
                                                    "�y���s���\�b�h��:GetDataSet�z" +
                                                    "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")
                    End If
                    '* ����ԍ� 000010 2005/01/25 �X�V�I���i�{��jIf ���ň͂�
                    '*����ԍ� 000006 2003/08/28 �C���I��

                    ' SQL�̎��s DataSet�̎擾
                    '* ����ԍ� 000009 2004/08/27 �X�V�J�n�i�{��j
                    'csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass)
                    csDataSet = m_csDataSchma.Clone()
                    csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)
                    '* ����ԍ� 000009 2004/08/27 �X�V�I��

                End If

            End If

            '�N���X�̉��
            strSQL = Nothing

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

        Return csDataSet

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertDainoB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertDainoB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertDainoB"
        Dim cfParam As UFParameterClass     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csInstRow As DataRow
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer            '�ǉ�����
        Dim strUpdateDateTime As String

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateInsertSQL(csDataRow)
                '* ����ԍ� 000011 2005/06/16 �ǉ��I��
            End If


            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '�쐬����

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABDainoEntity.TANMATSUID) = m_cfControlData.m_strClientId ' �[���h�c
            csDataRow(ABDainoEntity.SAKUJOFG) = "0"                             ' �폜�t���O
            csDataRow(ABDainoEntity.KOSHINCOUNTER) = Decimal.Zero               ' �X�V�J�E���^
            csDataRow(ABDainoEntity.SAKUSEINICHIJI) = strUpdateDateTime         ' �쐬����
            csDataRow(ABDainoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId  ' �쐬���[�U�[
            csDataRow(ABDainoEntity.KOSHINNICHIJI) = strUpdateDateTime          ' �X�V����
            csDataRow(ABDainoEntity.KOSHINUSER) = m_cfControlData.m_strUserId   ' �X�V���[�U�[


            ' ���N���X�̃f�[�^�������`�F�b�N���s��
            For Each csDataColumn In csDataRow.Table.Columns
                ' �f�[�^�������`�F�b�N
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn


            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam


            '*����ԍ� 000006 2003/08/28 �C���J�n
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
            '*����ԍ� 000006 2003/08/28 �C���I��

            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp

        End Try

        Return intInsCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^�X�V
    '* 
    '* �\��           Public Function UpdateDainoB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^�̃f�[�^���X�V����
    '* 
    '* ����           csDataRow As DataRow : �X�V����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �X�V�����f�[�^�̌���
    '************************************************************************************************
    Public Function UpdateDainoB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "UpdateDainoB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim intUpdCnt As Integer                            '�X�V����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateUpdateSQL(csDataRow)
                '* ����ԍ� 000011 2005/06/16 �ǉ��I��
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABDainoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABDainoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABDainoEntity.KOSHINCOUNTER)) + 1               '�X�V�J�E���^
            csDataRow(ABDainoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '�X�V����
            csDataRow(ABDainoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[
            '* ����ԍ� 000019 2023/12/05 �폜�J�n
            ''* ����ԍ� 000018 2023/10/19 �ǉ��J�n
            'csDataRow(ABDainoEntity.RRKNO) = CDec(csDataRow(ABDainoEntity.RRKNO)) + 1                             '����ԍ�
            ''* ����ԍ� 000018 2023/10/19 �ǉ��I��
            '* ����ԍ� 000019 2023/12/05 �폜�I��

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                '�L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABDainoEntity.PREFIX_KEY.RLength) = ABDainoEntity.PREFIX_KEY) Then
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '�f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            '*����ԍ� 000006 2003/08/28 �C���J�n
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
            '*����ԍ� 000006 2003/08/28 �C���I��

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpdateSQL, m_cfUpdateUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^�_���폜
    '* 
    '* �\��           Public Function DeleteDainoB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^�̃f�[�^��_���폜����
    '* 
    '* ����           csDataRow As DataRow : �_���폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �_���폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteDainoB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "DeleteDainoB"
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim intDelCnt As Integer                            '�폜����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strDelRonriSQL Is Nothing Or m_strDelRonriSQL = String.Empty Or _
                    m_cfDelRonriUFParameterCollectionClass Is Nothing) Then
                '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateDeleteRonriSQL(csDataRow)
                '* ����ԍ� 000011 2005/06/16 �ǉ��I��
            End If


            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABDainoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   ' �[���h�c
            csDataRow(ABDainoEntity.SAKUJOFG) = "1"                                                               ' �폜�t���O
            csDataRow(ABDainoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABDainoEntity.KOSHINCOUNTER)) + 1             ' �X�V�J�E���^
            csDataRow(ABDainoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   ' �X�V����
            csDataRow(ABDainoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     ' �X�V���[�U�[
            '* ����ԍ� 000019 2023/12/05 �폜�J�n
            ''* ����ԍ� 000018 2023/10/19 �ǉ��J�n
            'csDataRow(ABDainoEntity.RRKNO) = CDec(csDataRow(ABDainoEntity.RRKNO)) + 1                             ' ����ԍ�
            ''* ����ԍ� 000018 2023/10/19 �ǉ��I��
            '* ����ԍ� 000019 2023/12/05 �폜�I��

            '*����ԍ� 000006 2003/08/28 �C���J�n
            '' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            'For Each cfParam In m_cfUpdateUFParameterCollectionClass
            '    ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
            '    If (cfParam.ParameterName.Substring(0, ABDainoEntity.PREFIX_KEY.Length) = ABDainoEntity.PREFIX_KEY) Then
            '        m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = _
            '                csDataRow(cfParam.ParameterName.Substring(ABDainoEntity.PREFIX_KEY.Length), _
            '                          DataRowVersion.Original).ToString()
            '    Else
            '        '�f�[�^�������`�F�b�N
            '        CheckColumnValue(cfParam.ParameterName.Substring(ABDainoEntity.PARAM_PLACEHOLDER.Length), csDataRow(cfParam.ParameterName.Substring(ABDainoEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString.Trim)
            '        m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.Substring(ABDainoEntity.PARAM_PLACEHOLDER.Length), DataRowVersion.Current).ToString()
            '    End If
            'Next cfParam

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDelRonriUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABDainoEntity.PREFIX_KEY.RLength) = ABDainoEntity.PREFIX_KEY) Then
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    '�f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    m_cfDelRonriUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam
            '*����ԍ� 000006 2003/08/28 �C���I��


            '*����ԍ� 000006 2003/08/28 �C���J�n
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
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass) + "�z")
            '*����ԍ� 000006 2003/08/28 �C���I��

            '*����ԍ� 000006 2003/08/28 �C���J�n
            '' SQL�̎��s
            'intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfUpdateUFParameterCollectionClass)

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelRonriSQL, m_cfDelRonriUFParameterCollectionClass)
            '*����ԍ� 000006 2003/08/28 �C���I��

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, "UpdateKinyuKikan")

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp

        End Try

        Return intDelCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^�����폜
    '* 
    '* �\��           Public Function DeleteDainoB(ByVal csDataRow As DataRow, _
    '*                                             ByVal strSakujoKB As String) As Integer
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^�̃f�[�^�𕨗��폜����
    '* 
    '* ����           csDataRow As DataRow  : �폜����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '*                strSakujoKB As String : �폜�t���O
    '* 
    '* �߂�l         Integer : �폜�����f�[�^�̌���
    '************************************************************************************************
    Public Overloads Function DeleteDainoB(ByVal csDataRow As DataRow, _
                                           ByVal strSakujoKB As String) As Integer

        Const THIS_METHOD_NAME As String = "DeleteDainoB"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim intDelCnt As Integer                            '�폜����


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �폜�敪�̃`�F�b�N���s��
            If Not (strSakujoKB = "D") Then

                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_DELETE_SAKUJOKB)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)

            End If

            ' �폜�p�̃p�����[�^�tDELETE��������ƃp�����[�^�R���N�V�������쐬����
            If (m_strDelButuriSQL Is Nothing Or m_strDelButuriSQL = String.Empty Or _
                    IsNothing(m_cfDelButuriUFParameterCollectionClass)) Then
                '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
                'Call CreateSQL(csDataRow)
                Call CreateDeleteButsuriSQL(csDataRow)
                '* ����ԍ� 000011 2005/06/16 �ǉ��I��
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            For Each cfParam In m_cfDelButuriUFParameterCollectionClass

                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABDainoEntity.PREFIX_KEY.RLength) = ABDainoEntity.PREFIX_KEY) Then
                    m_cfDelButuriUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABDainoEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                End If
            Next cfParam


            '*����ԍ� 000006 2003/08/28 �C���J�n
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
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass) + "�z")
            '*����ԍ� 000006 2003/08/28 �C���I��

            ' SQL�̎��s
            intDelCnt = m_cfRdbClass.ExecuteSQL(m_strDelButuriSQL, m_cfDelButuriUFParameterCollectionClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp

        End Try

        Return intDelCnt

    End Function

    '* corresponds to VS2008 Start 2010/04/16 000015
    '* ����ԍ� 000011 2005/06/16 �폜�J�n
    ''''************************************************************************************************
    ''''* ���\�b�h��     SQL���̍쐬
    ''''* 
    ''''* �\��           Private Sub CreateSQL(ByVal csDataRow As DataRow)
    ''''* 
    ''''* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    ''''* 
    ''''* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    ''''* 
    ''''* �߂�l         �Ȃ�
    ''''************************************************************************************************
    ''''Private Sub CreateSQL(ByVal csDataRow As DataRow)

    ''''    Const THIS_METHOD_NAME As String = "CreateSQL"
    ''''    Dim cfUFParameterClass As UFParameterClass
    ''''    Dim csDataColumn As DataColumn
    ''''    Dim csInsertColumn As StringBuilder                 'INSERT�J������`
    ''''    Dim csInsertParam As StringBuilder                  'INSERT�p�����[�^��`
    ''''    Dim csUpdateParam As StringBuilder                  'UPDATE�p�p�����[�^
    ''''    Dim csWhere As StringBuilder                        'WHERE��
    ''''    Dim csDelRonriParam As StringBuilder                '�_���폜�p�����[�^��`

    ''''    Try
    ''''        ' �f�o�b�O���O�o��
    ''''        m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''        ' INSERT SQL���̍쐬
    ''''        m_strInsertSQL = "INSERT INTO " + ABDainoEntity.TABLE_NAME + " "
    ''''        csInsertColumn = New StringBuilder()
    ''''        csInsertParam = New StringBuilder()

    ''''        ' UPDATE SQL���̍쐬
    ''''        m_strUpdateSQL = "UPDATE " + ABDainoEntity.TABLE_NAME + " SET "
    ''''        csUpdateParam = New StringBuilder()

    ''''        ' WHERE��̍쐬
    ''''        csWhere = New StringBuilder()
    ''''        csWhere.Append(" WHERE ")
    ''''        csWhere.Append(ABDainoEntity.JUMINCD)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_JUMINCD)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.GYOMUCD)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_GYOMUCD)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.DAINOJUMINCD)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.STYM)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_STYM)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.EDYM)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_EDYM)
    ''''        csWhere.Append(" AND ")
    ''''        csWhere.Append(ABDainoEntity.KOSHINCOUNTER)
    ''''        csWhere.Append(" = ")
    ''''        csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER)

    ''''        ' �_��DELETE SQL���̍쐬
    ''''        csDelRonriParam = New StringBuilder()
    ''''        csDelRonriParam.Append("UPDATE ")
    ''''        csDelRonriParam.Append(ABDainoEntity.TABLE_NAME)
    ''''        csDelRonriParam.Append(" SET ")
    ''''        csDelRonriParam.Append(ABDainoEntity.TANMATSUID)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_TANMATSUID)
    ''''        csDelRonriParam.Append(", ")
    ''''        csDelRonriParam.Append(ABDainoEntity.SAKUJOFG)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_SAKUJOFG)
    ''''        csDelRonriParam.Append(", ")
    ''''        csDelRonriParam.Append(ABDainoEntity.KOSHINCOUNTER)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINCOUNTER)
    ''''        csDelRonriParam.Append(", ")
    ''''        csDelRonriParam.Append(ABDainoEntity.KOSHINNICHIJI)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINNICHIJI)
    ''''        csDelRonriParam.Append(", ")
    ''''        csDelRonriParam.Append(ABDainoEntity.KOSHINUSER)
    ''''        csDelRonriParam.Append(" = ")
    ''''        csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINUSER)
    ''''        csDelRonriParam.Append(csWhere)
    ''''        m_strDelRonriSQL = csDelRonriParam.ToString

    ''''        ' ����DELETE SQL���̍쐬
    ''''        m_strDelButuriSQL = "DELETE FROM " + ABDainoEntity.TABLE_NAME _
    ''''                + csWhere.ToString

    ''''        ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
    ''''        m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass()

    ''''        ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
    ''''        m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass()



    ''''        ' �p�����[�^�R���N�V�����̍쐬
    ''''        For Each csDataColumn In csDataRow.Table.Columns
    ''''            cfUFParameterClass = New UFParameterClass()

    ''''            ' INSERT SQL���̍쐬
    ''''            csInsertColumn.Append(csDataColumn.ColumnName)
    ''''            csInsertColumn.Append(", ")

    ''''            csInsertParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
    ''''            csInsertParam.Append(csDataColumn.ColumnName)
    ''''            csInsertParam.Append(", ")


    ''''            ' UPDATE SQL���̍쐬
    ''''            csUpdateParam.Append(csDataColumn.ColumnName)
    ''''            csUpdateParam.Append(" = ")
    ''''            csUpdateParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
    ''''            csUpdateParam.Append(csDataColumn.ColumnName)
    ''''            csUpdateParam.Append(", ")

    ''''            ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
    ''''            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    ''''            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
    ''''            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        Next csDataColumn


    ''''        '�Ō�̃J���}����菜����INSERT�����쐬
    ''''        m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
    ''''                + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"



    ''''        '�Ō�̃J���}����菜����UPDATE�����쐬
    ''''        m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + csWhere.ToString


    ''''        ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
    ''''        m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)


    ''''        ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_TANMATSUID
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_SAKUJOFG
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINNICHIJI
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINUSER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)


    ''''        ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        cfUFParameterClass = New UFParameterClass()
    ''''        cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
    ''''        m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

    ''''        ' �f�o�b�O���O�o��
    ''''        m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

    ''''    Catch objAppExp As UFAppException
    ''''        ' ���[�j���O���O�o��
    ''''        m_cfLogClass.WarningWrite(m_cfControlData, _
    ''''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''''                                    "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
    ''''                                    "�y���[�j���O���e:" + objAppExp.Message + "�z")
    ''''        ' �G���[�����̂܂܃X���[����
    ''''        Throw objAppExp

    ''''    Catch objExp As Exception
    ''''        ' �G���[���O�o��
    ''''        m_cfLogClass.ErrorWrite(m_cfControlData, _
    ''''                                    "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
    ''''                                    "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
    ''''                                    "�y�G���[���e:" + objExp.Message + "�z")
    ''''        ' �G���[�����̂܂܃X���[����
    ''''        Throw objExp

    ''''    End Try

    ''''End Sub
    '* ����ԍ� 000011 2005/06/16 �폜�I��
    '* corresponds to VS2008 End 2010/04/16 000015

    '* ����ԍ� 000011 2005/06/16 �ǉ��J�n
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
        Dim cfUFParameterClass As UFParameterClass
        Dim csDataColumn As DataColumn
        Dim csInsertColumn As StringBuilder                 'INSERT�J������`
        Dim csInsertParam As StringBuilder                  'INSERT�p�����[�^��`

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABDainoEntity.TABLE_NAME + " "
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

                csInsertParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            '�Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + csInsertParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + ")"

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
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
        Dim cfUFParameterClass As UFParameterClass
        Dim csDataColumn As DataColumn
        Dim csUpdateParam As StringBuilder                  'UPDATE�p�p�����[�^
        Dim csWhere As StringBuilder                        'WHERE��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABDainoEntity.TABLE_NAME + " SET "
            csUpdateParam = New StringBuilder

            ' WHERE��̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABDainoEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.DAINOJUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.STYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_STYM)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.EDYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_EDYM)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.TOROKURENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_TOROKURENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER)

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                '�Z���b�c�E�쐬�����E�쐬���[�U�͍X�V���Ȃ�
                If Not (csDataColumn.ColumnName = ABDainoEntity.JUMINCD) AndAlso _
                    Not (csDataColumn.ColumnName = ABDainoEntity.SAKUSEIUSER) AndAlso _
                     Not (csDataColumn.ColumnName = ABDainoEntity.SAKUSEINICHIJI) Then

                    cfUFParameterClass = New UFParameterClass

                    ' UPDATE SQL���̍쐬
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(" = ")
                    csUpdateParam.Append(ABDainoEntity.PARAM_PLACEHOLDER)
                    csUpdateParam.Append(csDataColumn.ColumnName)
                    csUpdateParam.Append(", ")

                    ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                    cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                    m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                End If

            Next csDataColumn

            '�Ō�̃J���}����菜����UPDATE�����쐬
            m_strUpdateSQL += csUpdateParam.ToString.TrimEnd(" ".ToCharArray()).TrimEnd(",".ToCharArray()) + csWhere.ToString

            ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            'm_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_TOROKURENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
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
        Dim csWhere As StringBuilder                        'WHERE��
        Dim csDelRonriParam As StringBuilder                '�_���폜�p�����[�^��`

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE��̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABDainoEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.DAINOJUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.STYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_STYM)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.EDYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_EDYM)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.TOROKURENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_TOROKURENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER)

            ' �_��DELETE SQL���̍쐬
            csDelRonriParam = New StringBuilder
            csDelRonriParam.Append("UPDATE ")
            csDelRonriParam.Append(ABDainoEntity.TABLE_NAME)
            csDelRonriParam.Append(" SET ")
            csDelRonriParam.Append(ABDainoEntity.TANMATSUID)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_TANMATSUID)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.SAKUJOFG)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_SAKUJOFG)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.KOSHINCOUNTER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINCOUNTER)
            '* ����ԍ� 000018 2023/10/19 �ǉ��J�n
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.RRKNO)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_RRKNO)
            '* ����ԍ� 000018 2023/10/19 �ǉ��I��
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.KOSHINNICHIJI)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINNICHIJI)
            csDelRonriParam.Append(", ")
            csDelRonriParam.Append(ABDainoEntity.KOSHINUSER)
            csDelRonriParam.Append(" = ")
            csDelRonriParam.Append(ABDainoEntity.PARAM_KOSHINUSER)
            csDelRonriParam.Append(csWhere)
            m_strDelRonriSQL = csDelRonriParam.ToString

            ' �_���폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelRonriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �_���폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_TANMATSUID
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_SAKUJOFG
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            '* ����ԍ� 000018 2023/10/19 �ǉ��J�n
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_RRKNO
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)
            '* ����ԍ� 000018 2023/10/19 �ǉ��I��

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINNICHIJI
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.PARAM_KOSHINUSER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            'm_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_TOROKURENBAN
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
            m_cfDelRonriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
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
        Const THIS_METHOD_NAME As String = "CreateDeleteButsuriSQL"
        Dim cfUFParameterClass As UFParameterClass
        Dim csWhere As StringBuilder                        'WHERE��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE��̍쐬
            csWhere = New StringBuilder
            csWhere.Append(" WHERE ")
            csWhere.Append(ABDainoEntity.JUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_JUMINCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUCD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.GYOMUNAISHU_CD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.DAINOJUMINCD)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_DAINOJUMINCD)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.STYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_STYM)
            'csWhere.Append(" AND ")
            'csWhere.Append(ABDainoEntity.EDYM)
            'csWhere.Append(" = ")
            'csWhere.Append(ABDainoEntity.KEY_EDYM)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.TOROKURENBAN)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_TOROKURENBAN)
            csWhere.Append(" AND ")
            csWhere.Append(ABDainoEntity.KOSHINCOUNTER)
            csWhere.Append(" = ")
            csWhere.Append(ABDainoEntity.KEY_KOSHINCOUNTER)

            ' ����DELETE SQL���̍쐬
            m_strDelButuriSQL = "DELETE FROM " + ABDainoEntity.TABLE_NAME _
                    + csWhere.ToString

            ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDelButuriUFParameterCollectionClass = New UFParameterCollectionClass

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_DAINOJUMINCD
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYM
            'm_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            'cfUFParameterClass = New UFParameterClass
            'cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYM
            'm_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_TOROKURENBAN
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_KOSHINCOUNTER
            m_cfDelButuriUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp

        End Try

    End Sub
    '* ����ԍ� 000011 2005/06/16 �폜�I��

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String,
    '*                                             ByVal strValue as String)
    '* 
    '* �@�\�@�@    �@�@INSERT, UPDATE, DELETE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           strColumnName As String : �Z�o�O�}�X�^�f�[�^�Z�b�g�̍��ږ�
    '*                strValue As String     : ���ڂɑΉ�����l
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)

        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Const TABLENAME As String = "��[�D"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'")

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()

                Case ABDainoEntity.JUMINCD                  '�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.SHICHOSONCD              '�s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.KYUSHICHOSONCD           '���s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.GYOMUCD                  '�Ɩ��R�[�h
                    If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_GYOMUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.GYOMUNAISHU_CD           '�Ɩ�����ʃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue.Trim)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_GYOMUNAISHU_CD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.DAINOJUMINCD             '��[�Z���R�[�h
                    If Not (strValue.Trim = String.Empty) Then
                        If (Not UFStringClass.CheckNumber(strValue)) Then
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_DAINOJUMINCD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABDainoEntity.STYMD                     '�J�n�N����
                    Select Case strValue.Trim
                        Case "00000000", String.Empty
                            ' �n�j
                        Case Else
                            m_cfDateClass.p_strDateValue = strValue
                            If (Not m_cfDateClass.CheckDate()) Then
                                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                                '�G���[��`���擾(���t���ړ��͂̌��ł��B�F)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002019)
                                '��O�𐶐�
                                Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�J�n�N����", objErrorStruct.m_strErrorCode)
                            End If
                    End Select

                Case ABDainoEntity.EDYMD                     '�I���N����
                    Select Case strValue.Trim
                        Case "00000000", "99999999", String.Empty
                            ' �n�j
                        Case Else
                            m_cfDateClass.p_strDateValue = strValue
                            If (Not m_cfDateClass.CheckDate()) Then
                                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                                '�G���[��`���擾(���t���ړ��͂̌��ł��B�F)
                                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002019)
                                '��O�𐶐�
                                Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�I���N����", objErrorStruct.m_strErrorCode)
                            End If
                    End Select

                '* ����ԍ� 000018 2023/10/19 �ǉ��J�n
                Case ABDainoEntity.TOROKURENBAN             '�o�^�A��
                    If (Not (strValue.Trim = String.Empty)) Then
                        If (Not UFStringClass.CheckNumber(strValue)) Then
                            '* ����ԍ� 000019 2023/12/05 �C���J�n
                            ''��O�𐶐�
                            'Throw New UFAppException("�������ړ��̓G���[�F�`�a��[�@�o�^�A��", UFAppException.ERR_EXCEPTION)
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_TOROKURENBAN)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                            '* ����ԍ� 000019 2023/12/05 �C���I��
                        End If
                    End If

                Case ABDainoEntity.RRKNO                     '����ԍ�
                    If (Not (strValue.Trim = String.Empty)) Then
                        If (Not UFStringClass.CheckNumber(strValue)) Then
                            '* ����ԍ� 000019 2023/12/05 �C���J�n
                            ''��O�𐶐�
                            'Throw New UFAppException("�������ړ��̓G���[�F�`�a��[�@����ԍ�", UFAppException.ERR_EXCEPTION)
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            '�G���[��`���擾
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_RRKNO)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                            '* ����ԍ� 000019 2023/12/05 �C���I��
                        End If
                    End If
                '* ����ԍ� 000018 2023/10/19 �ǉ��I��

                Case ABDainoEntity.DAINOKB                  '��[�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_DAINOKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.RESERVE                  '���U�[�u
                    '�`�F�b�N�Ȃ�

                Case ABDainoEntity.TANMATSUID               '�[���h�c
                    '* ����ԍ� 000007 2003/09/11 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000007 2003/09/11 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.SAKUJOFG                 '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.KOSHINCOUNTER            '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.SAKUSEINICHIJI           '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.SAKUSEIUSER              '�쐬���[�U
                    '* ����ԍ� 000008 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000008 2003/10/09 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.KOSHINNICHIJI            '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABDainoEntity.KOSHINUSER               '�X�V���[�U
                    '* ����ԍ� 000008 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000008 2003/10/09 �C���I��
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABDAINOB_RDBDATATYPE_KOSHINUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

            End Select

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

    End Sub
    '* ����ԍ� 000010 2005/01/25 �ǉ��J�n�i�{��j
    '************************************************************************************************
    '* ���\�b�h��     ��[�}�X�^�X�L�[�}�擾
    '* 
    '* �\��           Public Function GetDainoSchemaBHoshu() As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���X�L�[�}�擾
    '* 
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̃X�L�[�}
    '************************************************************************************************
    Public Overloads Function GetDainoSchemaBHoshu() As DataSet
        Const THIS_METHOD_NAME As String = "GetDainoSchemaBHoshu"              '���̃��\�b�h��

        Try
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
    '* ����ԍ� 000010 2005/01/25 �ǉ��I���i�{��j

    '* ����ԍ� 000012 2006/12/22 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �{�X��񒊏o
    '* 
    '* �\��           Public Function GetHontenBHoshu(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��[�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD    : �Z���R�[�h
    '* 
    '* �߂�l         DataSet : �擾������[�}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetHontenBHoshu(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetHontenBHoshu"    ' ���\�b�h��
        Const HONTEN_GYOMUCD As String = "05"                   ' �{�X��񃌃R�[�h�Ɩ��R�[�h
        Const HONTEN_GYOMUNAISHU_CD As String = "9"             ' �{�X��񃌃R�[�h�Ɩ�����R�[�h
        Const HONTEN_STYMD As String = "00000000"                  ' �{�X��񃌃R�[�h�J�n�N����
        Const HONTEN_EDYMD As String = "99999999"                  ' �{�X��񃌃R�[�h�I���N����
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim objErrorStruct As UFErrorStruct                     ' �G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim cfUFParameterClass As UFParameterClass
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass
        Dim csDataSet As DataSet                                '�f�[�^�Z�b�g
        Dim strSQL As StringBuilder = New StringBuilder("")

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬    
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABDainoEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABDainoEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.GYOMUCD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_GYOMUCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.GYOMUNAISHU_CD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_GYOMUNAISHU_CD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.STYMD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_STYMD)
            strSQL.Append(" AND ")
            strSQL.Append(ABDainoEntity.EDYMD)
            strSQL.Append(" = ")
            strSQL.Append(ABDainoEntity.KEY_EDYMD)

            strSQL.Append(";")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' ���������̃p�����[�^���쐬�i�Z���R�[�h�j
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬�i�Ɩ��R�[�h�j
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUCD
            cfUFParameterClass.Value = HONTEN_GYOMUCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬�i�Ɩ�����R�[�h�j
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_GYOMUNAISHU_CD
            cfUFParameterClass.Value = HONTEN_GYOMUNAISHU_CD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬�i�J�n�N�����j
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_STYMD
            cfUFParameterClass.Value = HONTEN_STYMD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' ���������̃p�����[�^���쐬�i�I���N�����j
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABDainoEntity.KEY_EDYMD
            cfUFParameterClass.Value = HONTEN_EDYMD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDataSet = m_csDataSchma.Clone()
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, csDataSet, ABDainoEntity.TABLE_NAME, cfUFParameterCollectionClass, True)

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp

        End Try

        Return csDataSet

    End Function
    '* ����ԍ� 000012 2006/12/22 �ǉ��I��
#End Region

End Class
