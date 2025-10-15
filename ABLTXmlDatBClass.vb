'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �`�a���k�s�`�w��M�w�l�k�}�X�^(ABLTXmlDatBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t             2009/07/15
'*
'* �쐬��           ��Á@�v��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2010/04/16   000001     VS2008�Ή��i��Áj
'* 2011/08/30   000002     eLTAX���p�͏o�A�g�̍폜�@�\�ǉ��ɔ������C�i��Áj
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

Public Class ABLTXmlDatBClass

#Region "�����o�ϐ�"
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X

    Private m_csDataSchma As DataSet                        ' �X�L�[�}�ۊǗp�f�[�^�Z�b�g
    Private m_strInsertSQL As String
    Private m_strUpDateSQL As String
    Private m_strUpDateSQL_ConvertFG As String
    Private m_strUpDateSQL_SakujoFG As String
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfUpdateConvertFGUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfUpdateSakujoFGUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE�p�p�����[�^�R���N�V����
    '*����ԍ� 000002 2011/08/30 �ǉ��J�n
    Private m_strDeleteSQL As String
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass  'DELETE�p�p�����[�^�R���N�V����
    '*����ԍ� 000002 2011/08/30 �ǉ��I��

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABLTXmlDatBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h

#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfControlData As UFControlData, 
    '*                                ByVal cfConfigDataClass As UFConfigDataClass, 
    '*                                ByVal cfRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                 cfConfigData As UFConfigDataClass      : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '*                 cfRdbClass As UFRdbClass               : �q�c�a�f�[�^�I�u�W�F�N�g
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

        ' SQL���̍쐬
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABLTXMLDatEntity.TABLE_NAME, ABLTXMLDatEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "���\�b�h"

#Region "eLTAX��MXML�f�[�^�擾���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�͏o�E�\���f�[�^�擾
    '* 
    '* �\��         Public Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����         csABLTXmlDatParaX As ABLTXmlDatParaXClass   : eLTAX��MXML�p�����[�^�N���X
    '* 
    '* �߂�l       �擾�������k�s�`�w��M�w�l�k�}�X�^�̊Y���f�[�^�iDataSet�j
    '*                 �\���FcsLtXMLDatEntity    
    '************************************************************************************************
    Public Overloads Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTXmlDat"

        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim objErrorStruct As UFErrorStruct                             ' �G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim csLtXMLDatEntity As DataSet                                 ' ���p�͏o��M�}�X�^
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                      ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            strSQL.Append("SELECT * ")
            strSQL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME)

            ' WHERE��
            strSQL.Append(" WHERE ")

            ' �K�{����
            '* SHINKOKUSHINSEIKB = "R0" AND 
            strSQL.Append(ABLTXMLDatEntity.SHINKOKUSHINSEIKB).Append(" = ")
            strSQL.Append(ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ")
            strSQL.Append("'1'")


            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB
            cfUFParameterClass.Value = ABConstClass.ELTAX_RIYOTDKD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �Ŗڋ敪
            If (csABLTXmlDatParaX.p_strTaxKB <> ABEnumDefine.ZeimokuCDType.Empty) Then
                strSQL.Append(" AND ")

                ' �Ŗڋ敪���ݒ肳��Ă���ꍇ�A���o�����ɂ���
                strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(" = ")
                strSQL.Append(ABLTXMLDatEntity.KEY_TAXKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(csABLTXmlDatParaX.p_strTaxKB)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If

            ' �R���o�[�g�t���O
            strSQL.Append(" AND ")
            If (csABLTXmlDatParaX.p_blnConvertFG = True) Then
                ' �R���o�[�g�t���O��True�̏ꍇ�A"1"���擾����
                strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" = ")
                strSQL.Append("'1'")

            Else
                ' �R���o�[�g�t���O��False�̏ꍇ�A"1"�ȊO���擾����
                strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> ")
                strSQL.Append("'1'")

            End If

            ' �ő�擾�����Z�b�g
            If (csABLTXmlDatParaX.p_intMaxCount <> 0) Then
                m_cfRdbClass.p_intMaxRows = csABLTXmlDatParaX.p_intMaxCount
            Else
            End If

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                  "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                  "�y���s���\�b�h��:GetDataSet�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' �͏o�E�\���f�[�^�擾
            csLtXMLDatEntity = m_csDataSchma.Clone()
            csLtXMLDatEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtXMLDatEntity, ABLTXMLDatEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


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

        Return csLtXMLDatEntity

    End Function
#End Region

#Region "eLTAX��MXML�f�[�^�擾���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�͏o�E�\���f�[�^�擾
    '* 
    '* �\��         Public Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass, _
    '*                                          ByRef intAllCount As Integer) As DataSet
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����         csABLTXmlDatParaX As ABLTXmlDatParaXClass   : eLTAX��MXML�p�����[�^�N���X
    '*              intAllCount As Integer                      : �S�f�[�^����
    '* 
    '* �߂�l       �擾�������k�s�`�w��M�w�l�k�}�X�^�̊Y���f�[�^�iDataSet�j
    '*                 �\���FcsLtXMLDatEntity    
    '************************************************************************************************
    Public Overloads Function GetLTXmlDat(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass, ByRef intAllCount As Integer) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTXmlDat"
        Const COL_COUNT As String = "COUNT"
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim objErrorStruct As UFErrorStruct                             ' �G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim csLtXMLDatEntity As DataSet                                 ' ���p�͏o��M�}�X�^
        Dim csLtXmlDat_All As DataSet                                   ' ���p�͏o��M�S���f�[�^
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim strSQL_ALL As New StringBuilder                             ' SQL���S���擾������
        Dim strWhere As New StringBuilder                               ' WHERE��������
        Dim cfUFParameterClass As UFParameterClass                      ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            strSQL.Append("SELECT * ")
            strSQL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME)

            strSQL_ALL.Append("SELECT COUNT(*) AS ").Append(COL_COUNT)
            strSQL_ALL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME)

            ' WHERE��
            strWhere.Append(" WHERE ")

            ' �K�{����
            '* SHINKOKUSHINSEIKB = "R0" AND 
            strWhere.Append(ABLTXMLDatEntity.SHINKOKUSHINSEIKB).Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB)
            strWhere.Append(" AND ")

            '*����ԍ� 000002 2011/08/30 �C���J�n
            If (csABLTXmlDatParaX.p_blnSakuJoFG = False) Then
                ' eLTAX��MXML�p�����[�^�N���X:�폜�t���O="False"�̏ꍇ�A�폜�f�[�^�ȊO�𒊏o
                strWhere.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ")
                strWhere.Append("'1'")
            Else
                ' eLTAX��MXML�p�����[�^�N���X:�폜�t���O="True"�̏ꍇ�A�폜�f�[�^�𒊏o
                strWhere.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" = ")
                strWhere.Append("'1'")
            End If
            'strWhere.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ")
            'strWhere.Append("'1'")
            '*����ԍ� 000002 2011/08/30 �C���I��


            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB
            cfUFParameterClass.Value = ABConstClass.ELTAX_RIYOTDKD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �Ŗڋ敪
            If (csABLTXmlDatParaX.p_strTaxKB <> ABEnumDefine.ZeimokuCDType.Empty) Then
                strWhere.Append(" AND ")

                ' �Ŗڋ敪���ݒ肳��Ă���ꍇ�A���o�����ɂ���
                strWhere.Append(ABLTXMLDatEntity.TAXKB).Append(" = ")
                strWhere.Append(ABLTXMLDatEntity.KEY_TAXKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(csABLTXmlDatParaX.p_strTaxKB)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If

            ' �R���o�[�g�t���O
            strWhere.Append(" AND ")
            If (csABLTXmlDatParaX.p_blnConvertFG = True) Then
                ' �R���o�[�g�t���O��True�̏ꍇ�A"1"���擾����
                strWhere.Append(ABLTXMLDatEntity.CONVERTFG).Append(" = ")
                strWhere.Append("'1'")

            Else
                ' �R���o�[�g�t���O��False�̏ꍇ�A"1"�ȊO���擾����
                strWhere.Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> ")
                strWhere.Append("'1'")

            End If

            ' �ő�擾�����Z�b�g
            If (csABLTXmlDatParaX.p_intMaxCount <> 0) Then
                m_cfRdbClass.p_intMaxRows = csABLTXmlDatParaX.p_intMaxCount
            Else
            End If

            ' SQL������ 
            strSQL.Append(strWhere.ToString)
            strSQL_ALL.Append(strWhere.ToString)

            ' �S���擾����
            csLtXmlDat_All = m_cfRdbClass.GetDataSet(strSQL_ALL.ToString, cfUFParameterCollectionClass)

            intAllCount = CInt(csLtXmlDat_All.Tables(0).Rows(0)(COL_COUNT))


            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                  "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                  "�y���s���\�b�h��:GetDataSet�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' �͏o�E�\���f�[�^�擾
            csLtXMLDatEntity = m_csDataSchma.Clone()
            csLtXMLDatEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtXMLDatEntity, ABLTXMLDatEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


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

        Return csLtXMLDatEntity

    End Function
#End Region

#Region "eLTAX��MXML�͏o�E�\���f�[�^�����擾���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�͏o�E�\���f�[�^�����擾
    '* 
    '* �\��         Public Function GetLTXmlCount(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^���Y���f�[�^�̌������擾����B
    '* 
    '* ����         csABLTXmlDatParaX As ABLTXmlDatParaXClass   : eLTAX��MXML�p�����[�^�N���X
    '* 
    '* �߂�l       �擾����eLTAX��M�f�[�^�����f�[�^�iDataSet�j
    '*                 �\���FcsLtXMLDatCountDS    
    '************************************************************************************************
    Public Function GetLTXmlCount(ByVal csABLTXmlDatParaX As ABLTXmlDatParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTXmlCount"

        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim objErrorStruct As UFErrorStruct                             ' �G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim csLtXMLDatCountDS As DataSet                                ' ABeLTAX��MDAT�����f�[�^�Z�b�g
        Dim csDataSet As DataSet
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                      ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X
        Dim csDataRow As DataRow
        Dim csNewRow As DataRow
        Dim intCount As Integer = 0

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            strSQL.Append("SELECT ")
            strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(", ")
            strSQL.Append(ABLTXMLDatEntity.PROCID).Append(", ")
            strSQL.Append("COUNT(*) AS COUNT")
            strSQL.Append(" FROM ").Append(ABLTXMLDatEntity.TABLE_NAME)

            ' WHERE��
            strSQL.Append(" WHERE ")

            ' �K�{����
            '* SHINKOKUSHINSEIKB = "T0" AND 
            strSQL.Append(ABLTXMLDatEntity.SHINKOKUSHINSEIKB).Append(" = ")
            strSQL.Append(ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB)
            strSQL.Append(" AND ")
            strSQL.Append(ABLTXMLDatEntity.SAKUJOFG).Append(" <> ")
            strSQL.Append("'1'")

            ' ���������̃p�����[�^���쐬
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SHINKOKUSHINSEIKB
            cfUFParameterClass.Value = ABConstClass.ELTAX_RIYOTDKD

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' �Ŗڋ敪
            If (csABLTXmlDatParaX.p_strTaxKB <> ABEnumDefine.ZeimokuCDType.Empty) Then
                strSQL.Append(" AND ")

                ' �Ŗڋ敪���ݒ肳��Ă���ꍇ�A���o�����ɂ���
                strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(" = ")
                strSQL.Append(ABLTXMLDatEntity.KEY_TAXKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(csABLTXmlDatParaX.p_strTaxKB)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If

            ' �R���o�[�g�t���O
            strSQL.Append(" AND ")
            If (csABLTXmlDatParaX.p_blnConvertFG = True) Then
                ' �R���o�[�g�t���O��True�̏ꍇ�A"1"���擾����
                strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" = ")
                strSQL.Append("'1'")

            Else
                ' �R���o�[�g�t���O��False�̏ꍇ�A"1"�ȊO���擾����
                strSQL.Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> ")
                strSQL.Append("'1'")

            End If

            ' GROUP BY��
            strSQL.Append(" GROUP BY ")
            strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(", ")
            strSQL.Append(ABLTXMLDatEntity.PROCID)

            ' ORDER BY��
            strSQL.Append(" ORDER BY ")
            strSQL.Append(ABLTXMLDatEntity.TAXKB).Append(", ")
            strSQL.Append(ABLTXMLDatEntity.PROCID)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                  "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                  "�y���s���\�b�h��:GetDataSet�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' �f�[�^�擾
            csDataSet = m_cfRdbClass.GetDataSet(strSQL.ToString, ABLTXMLDatEntity.TABLE_NAME, cfUFParameterCollectionClass, False)


            ' eLTAX��MDAT�����f�[�^�e�[�u���쐬
            csLtXMLDatCountDS = CreateDataSet()


            ' ���k�s�`�w��M�w�l�k�͏o�E�\���f�[�^�����f�[�^�Z�b�g�ɃZ�b�g
            For Each csDataRow In csDataSet.Tables(ABLTXMLDatEntity.TABLE_NAME).Rows

                csNewRow = csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).NewRow

                csNewRow(ABLTXmlDatCountData.TAXKB) = csDataRow(ABLTXMLDatEntity.TAXKB)
                csNewRow(ABLTXmlDatCountData.PROCID) = csDataRow(ABLTXMLDatEntity.PROCID)
                csNewRow(ABLTXmlDatCountData.PROCRYAKUMEI) = GetProcRyakumei(CStr(csDataRow(ABLTXMLDatEntity.PROCID)))
                csNewRow(ABLTXmlDatCountData.COUNT) = csDataRow("COUNT")

                csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).Rows.Add(csNewRow)

            Next
            '----------------------------------------------------------------------------
            ' ���v�s�ǉ�
            csNewRow = csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).NewRow

            ' �Ŗڋ敪
            If (csABLTXmlDatParaX.p_strTaxKB <> ABEnumDefine.ZeimokuCDType.Empty) Then
                ' �󔒈ȊO
                csNewRow(ABLTXmlDatCountData.TAXKB) = CStr(csABLTXmlDatParaX.p_strTaxKB)
            Else
                ' �󔒂̏ꍇ
                csNewRow(ABLTXmlDatCountData.TAXKB) = String.Empty
            End If

            ' �葱ID
            csNewRow(ABLTXmlDatCountData.PROCID) = String.Empty

            ' �葱��
            csNewRow(ABLTXmlDatCountData.PROCRYAKUMEI) = String.Empty

            ' ����
            For Each csDataRow In csDataSet.Tables(ABLTXMLDatEntity.TABLE_NAME).Rows
                intCount += CInt(csDataRow("COUNT"))
            Next
            csNewRow(ABLTXmlDatCountData.COUNT) = CStr(intCount)

            csLtXMLDatCountDS.Tables(ABLTXmlDatCountData.TABLE_NAME).Rows.Add(csNewRow)
            '----------------------------------------------------------------------------


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

        Return csLtXMLDatCountDS

    End Function
#End Region

#Region "eLTAX��MXML�f�[�^�ǉ����\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�f�[�^�ǉ����\�b�h
    '* 
    '* �\��         Public Function InsertLTXMLDat(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^�ɐV�K�f�[�^��ǉ�����
    '* 
    '* ����         csDataRow As DataRow   : �ǉ��f�[�^(ABeLTAXRiyoTdk)
    '* 
    '* �߂�l       �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertLTXMLDat(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertLTXMLDat"
        Dim cfParam As UFParameterClass                                 ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                                  ' �f�[�^�J����
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim intInsCnt As Integer                                        ' �ǉ�����
        Dim strUpdateDateTime As String                                 ' �V�X�e�����t

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")        ' �쐬����

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId              ' �[���h�c
            csDataRow(ABLTXMLDatEntity.SAKUJOFG) = "0"                                          ' �폜�t���O
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = Decimal.Zero                            ' �X�V�J�E���^
            csDataRow(ABLTXMLDatEntity.SAKUSEINICHIJI) = strUpdateDateTime                      ' �쐬����
            csDataRow(ABLTXMLDatEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId               ' �쐬���[�U�[
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = strUpdateDateTime                       ' �X�V����
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                ' �X�V���[�U�[


            For Each cfParam In m_cfInsertUFParameterCollectionClass
                If (cfParam.ParameterName = ABLTXMLDatEntity.KEY_XMLDAT) Then
                    ' ����:XMLDat�̏ꍇ�́Abyte�^�̂܂܃Z�b�g����
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength))
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value =
                                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength)).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")

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

        Return intInsCnt

    End Function
#End Region

#Region "eLTAX��MXML�f�[�^�X�V���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�f�[�^�X�V���\�b�h
    '* 
    '* �\��         Public Function UpdateLTXMLDat(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����         csDataRow As DataRow   : ���p�̓f�[�^(ABeLTAXRiyoTdk)
    '* 
    '* �߂�l       �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateLTXMLDat(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTXmlDat"
        Dim cfParam As UFParameterClass                         ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                          ' �f�[�^�J����
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim intUpdCnt As Integer                                ' �X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpDateSQL Is Nothing Or m_strUpDateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' �[���h�c
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' �X�V�J�E���^
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' �X�V����
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' �X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                ElseIf (cfParam.ParameterName = ABLTXMLDatEntity.KEY_XMLDAT) Then
                    ' ����:XMLDat�̏ꍇ�́Abyte�^�̂܂܃Z�b�g����
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current)
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass)

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

        Return intUpdCnt

    End Function
#End Region

#Region "eLTAX��MXML�f�[�^:�R���o�[�g�t���O�X�V���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�f�[�^:�R���o�[�g�t���O�X�V���\�b�h
    '* 
    '* �\��         Public Function UpdateLTXMLDat_ConvertFG(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����         csDataRow As DataRow   : ���p�̓f�[�^(ABeLTAXRiyoTdk)
    '* 
    '* �߂�l       �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateLTXMLDat_ConvertFG(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTXMLDat_ConvertFG"
        Dim cfParam As UFParameterClass                         ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                          ' �f�[�^�J����
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim intUpdCnt As Integer                                ' �X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpDateSQL_ConvertFG Is Nothing Or m_strUpDateSQL_ConvertFG = String.Empty Or _
                m_cfUpdateConvertFGUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL_UpDateConvertFG()
            Else
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' �[���h�c
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' �X�V�J�E���^
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' �X�V����
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' �X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateConvertFGUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfUpdateConvertFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateConvertFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                          csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current)
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL_ConvertFG, m_cfUpdateConvertFGUFParameterCollectionClass)

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

        Return intUpdCnt

    End Function
#End Region

#Region "eLTAX��MXML�f�[�^:�폜�t���O�X�V���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�f�[�^:�폜�t���O�X�V���\�b�h
    '* 
    '* �\��         Public Overloads Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����         csDataRow As DataRow   : ���p�̓f�[�^(ABeLTAXRiyoTdk)
    '* 
    '* �߂�l       �X�V����(Integer)
    '************************************************************************************************
    Public Overloads Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTXMLDat_SakujoFG"
        Dim cfParam As UFParameterClass                         ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                          ' �f�[�^�J����
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim intUpdCnt As Integer                                ' �X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpDateSQL_SakujoFG Is Nothing Or m_strUpDateSQL_SakujoFG = String.Empty Or _
                m_cfUpdateSakujoFGUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL_UpDateSakujoFG()
            Else
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' �[���h�c
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' �X�V�J�E���^
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' �X�V����
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' �X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateSakujoFGUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                                csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL_SakujoFG, m_cfUpdateSakujoFGUFParameterCollectionClass)

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

        Return intUpdCnt

    End Function
#End Region

#Region "eLTAX��MXML�f�[�^:�폜�t���O�X�V���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�f�[�^:�폜�t���O�X�V���\�b�h
    '* 
    '* �\��         Public Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow, _
    '*                                                      ByVal blnKoshinCounter As Boolean) As Integer
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����         csDataRow As DataRow    : ���p�̓f�[�^(ABeLTAXRiyoTdk)
    '*              blnKoshinCounter        : �X�V�J�E���^(True:�����Ɋ܂ށAFalse:�܂܂Ȃ�)
    '* 
    '* �߂�l       �X�V����(Integer)
    '************************************************************************************************
    Public Overloads Function UpdateLTXMLDat_SakujoFG(ByVal csDataRow As DataRow, _
                                                      ByVal blnKoshinCounter As Boolean) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTXMLDat_SakujoFG"
        Dim cfParam As UFParameterClass                         ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000001
        'Dim csDataColumn As DataColumn                          ' �f�[�^�J����
        '* corresponds to VS2008 End 2010/04/16 000001
        Dim intUpdCnt As Integer                                ' �X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpDateSQL_SakujoFG Is Nothing Or m_strUpDateSQL_SakujoFG = String.Empty Or _
                m_cfUpdateSakujoFGUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL_UpDateSakujoFG(blnKoshinCounter)
            Else
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' �[���h�c
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' �X�V�J�E���^
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' �X�V����
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' �X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateSakujoFGUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateSakujoFGUFParameterCollectionClass(cfParam.ParameterName).Value =
                                csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strUpDateSQL, m_cfUpdateUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strUpDateSQL_SakujoFG, m_cfUpdateSakujoFGUFParameterCollectionClass)

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

        Return intUpdCnt

    End Function
#End Region

    '*����ԍ� 000002 2011/08/30 �ǉ��J�n
#Region "eLTAX��MXML�f�[�^:�폜(����)���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�f�[�^:�폜(����)���\�b�h
    '* 
    '* �\��         Public Overloads Function DeleteLTXMLDat() As Integer
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^�̊Y���f�[�^�𕨗��폜����
    '* 
    '* ����         �Ȃ�
    '* 
    '* �߂�l       �X�V����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteLTXMLDat(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteLTXMLDat"
        Dim cfParam As UFParameterClass                         ' �p�����[�^�N���X
        Dim intUpdCnt As Integer                                ' �X�V����
        Dim blnKoshinCounter As Boolean = False                 ' �X�V�J�E���^�[

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpDateSQL_SakujoFG Is Nothing OrElse m_strUpDateSQL_SakujoFG = String.Empty OrElse _
                m_cfUpdateSakujoFGUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL_UpDateSakujoFG(blnKoshinCounter)
            Else
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABLTXMLDatEntity.TANMATSUID) = m_cfControlData.m_strClientId                                  ' �[���h�c
            csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLTXMLDatEntity.KOSHINCOUNTER)) + 1         ' �X�V�J�E���^
            csDataRow(ABLTXMLDatEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")    ' �X�V����
            csDataRow(ABLTXMLDatEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                    ' �X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfDeleteUFParameterCollectionClass
                If (cfParam.ParameterName.RSubstring(0, ABLTXMLDatEntity.PREFIX_KEY.RLength) = ABLTXMLDatEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                                csDataRow(cfParam.ParameterName.RSubstring(ABLTXMLDatEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "�z")

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass)

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

        Return intUpdCnt

    End Function
#End Region

#Region "eLTAX��MXML�f�[�^:�폜�f�[�^�ꊇ�폜(����)���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�f�[�^:�폜�f�[�^�ꊇ�폜(����)���\�b�h
    '* 
    '* �\��         Public Overloads Function DeleteLTXMLDat_Sakujo() As Integer
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^�̃f�[�^�̍폜�t���O="1"�̃f�[�^���ꊇ�폜����
    '* 
    '* ����         �Ȃ�
    '* 
    '* �߂�l       �X�V����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteLTXMLDat_Sakujo() As Integer
        Const THIS_METHOD_NAME As String = "DeleteLTXMLDat_Sakujo"
        Dim csSQL As New StringBuilder
        Dim intUpdCnt As Integer                                ' �X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬
            csSQL.Append("DELETE ").Append(ABLTXMLDatEntity.TABLE_NAME)
            csSQL.Append(" WHERE ").Append(ABLTXMLDatEntity.CONVERTFG).Append(" <> '1' ")
            csSQL.Append("AND ").Append(ABLTXMLDatEntity.SAKUJOFG).Append(" = '1'")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(csSQL.ToString) + "�z")

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(csSQL.ToString)

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

        Return intUpdCnt

    End Function
#End Region

#Region "eLTAX��MXML�f�[�^:�R���o�[�g�ς݈ꊇ�폜(����)���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX��MXML�f�[�^:�R���o�[�g�ς݈ꊇ�폜(����)���\�b�h
    '* 
    '* �\��         Public Overloads Function DeleteLTXMLDat_Sakujo() As Integer
    '* 
    '* �@�\�@�@     eLTAX��MXML�}�X�^�̃f�[�^�̃R���o�[�g�t���O="1"�̃f�[�^���ꊇ�폜����
    '* 
    '* ����         �Ȃ�
    '* 
    '* �߂�l       �X�V����(Integer)
    '************************************************************************************************
    Public Overloads Function DeleteLTXMLDat_Convert() As Integer
        Const THIS_METHOD_NAME As String = "DeleteLTXMLDat_Convert"
        Dim csSQL As New StringBuilder
        Dim intUpdCnt As Integer                    ' �X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬
            csSQL.Append("DELETE ").Append(ABLTXMLDatEntity.TABLE_NAME)
            csSQL.Append(" WHERE ").Append(ABLTXMLDatEntity.CONVERTFG).Append(" = '1'")

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                        "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(csSQL.ToString) + "�z")

            ' SQL�̎��s
            intUpdCnt = m_cfRdbClass.ExecuteSQL(csSQL.ToString)

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

        Return intUpdCnt

    End Function
#End Region
    '*����ԍ� 000002 2011/08/30 �ǉ��I��

#Region "SQL���̍쐬"
    '************************************************************************************************
    '* ���\�b�h��   SQL���̍쐬
    '* 
    '* �\��         Private Sub CreateSQL(ByVal csDataRow As DataRow)
    '* 
    '* �@�\�@�@     INSERT, UPDATE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����         csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l       �Ȃ�
    '************************************************************************************************
    Private Sub CreateSQL(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass                  ' �p�����[�^�N���X
        Dim strInsertColumn As String                               ' �ǉ�SQL�����ڕ�����
        Dim strInsertParam As String                                ' �ǉ�SQL���p�����[�^������
        Dim strWhere As New StringBuilder                           ' �X�V�폜SQL��Where��������

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABLTXMLDatEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' UPDATE SQL���̍쐬
            m_strUpDateSQL = "UPDATE " + ABLTXMLDatEntity.TABLE_NAME + " SET "

            ' UPDATE Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER)

            ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' UPDATE SQL���̍쐬
                m_strUpDateSQL += csDataColumn.ColumnName + " = " + ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)

            Next csDataColumn

            ' INSERT SQL���̃g���~���O
            strInsertColumn = strInsertColumn.Trim()
            strInsertColumn = strInsertColumn.Trim(CType(",", Char))
            strInsertParam = strInsertParam.Trim()
            strInsertParam = strInsertParam.Trim(CType(",", Char))
            m_strInsertSQL += "(" + strInsertColumn + ")" + " VALUES (" + strInsertParam + ")"

            ' UPDATE SQL���̃g���~���O
            m_strUpDateSQL = m_strUpDateSQL.Trim()
            m_strUpDateSQL = m_strUpDateSQL.Trim(CType(",", Char))

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpDateSQL += strWhere.ToString

            ' UPDATE �R���N�V�����ɃL�[����ǉ�
            ' ��M����
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��t�ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �w�l�k�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �\����t�ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER
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
    End Sub
#End Region

#Region "SQL���̍쐬(�R���o�[�g�t���O�p)"
    '************************************************************************************************
    '* ���\�b�h��   SQL���̍쐬(�R���o�[�g�t���O�p)
    '* 
    '* �\��         Private Sub CreateSQL_UpDateConvertFG()
    '* 
    '* �@�\�@�@     UPDATE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����         csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l       �Ȃ�
    '************************************************************************************************
    Private Sub CreateSQL_UpDateConvertFG()
        Const THIS_METHOD_NAME As String = "CreateSQL_UpDateConvertFG"
        Dim cfUFParameterClass As UFParameterClass                  ' �p�����[�^�N���X
        Dim strWhere As New StringBuilder                           ' �X�VSQL��Where��������
        Dim strSet As New StringBuilder                             ' �X�VSQL��Set��������

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL���̍쐬
            m_strUpDateSQL_ConvertFG = "UPDATE " + ABLTXMLDatEntity.TABLE_NAME + " SET "

            ' UPDATE Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER)

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateConvertFGUFParameterCollectionClass = New UFParameterCollectionClass

            ' �R���o�[�g�t���O�pUPDATE SQL���̍쐬
            m_strUpDateSQL_ConvertFG += ABLTXMLDatEntity.CONVERTFG + " = " + ABLTXMLDatEntity.KEY_CONVERTFG + ","

            ' ����Set��
            strSet.Append(ABLTXMLDatEntity.TANMATSUID).Append(" = ").Append(ABLTXMLDatEntity.KEY_TANMATSUID).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINCOUNTER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINCOUNTER).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINNICHIJI).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINNICHIJI).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINUSER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINUSER)

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpDateSQL_ConvertFG += strSet.ToString + strWhere.ToString

            '*-------------------------------------------------------------------------*
            ' �R���o�[�g�t���O�pUPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            ' �R���o�[�g�t���O
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_CONVERTFG
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            '*-------------------------------------------------------------------------*
            ' UPDATE �R���N�V�����ɃL�[����ǉ�
            ' �[���h�c
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TANMATSUID
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINCOUNTER
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V����
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINNICHIJI
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V���[�U
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINUSER
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��M����
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��t�ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �w�l�k�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �\����t�ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER
            m_cfUpdateConvertFGUFParameterCollectionClass.Add(cfUFParameterClass)
            '*-------------------------------------------------------------------------*

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
    End Sub
#End Region

#Region "SQL���̍쐬(�폜�t���O�p)"
    '************************************************************************************************
    '* ���\�b�h��   SQL���̍쐬(�폜�t���O�p)
    '* 
    '* �\��         Private Sub CreateSQL_UpDateSakujoFG()
    '* 
    '* �@�\�@�@     UPDATE�̊eSQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����         csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l       �Ȃ�
    '************************************************************************************************
    Private Sub CreateSQL_UpDateSakujoFG()

        Call CreateSQL_UpDateSakujoFG(True)

    End Sub
    Private Sub CreateSQL_UpDateSakujoFG(ByVal blnKoshinCounter As Boolean)
        Const THIS_METHOD_NAME As String = "CreateSQL_UpDateSakujoFG"
        Dim cfUFParameterClass As UFParameterClass                  ' �p�����[�^�N���X
        Dim strWhere As New StringBuilder                           ' �X�VSQL��Where��������
        Dim strSet As New StringBuilder                             ' �X�VSQL��Set��������

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' UPDATE SQL���̍쐬
            m_strUpDateSQL_SakujoFG = "UPDATE " + ABLTXMLDatEntity.TABLE_NAME + " SET "

            '*����ԍ� 000002 2011/08/30 �ǉ��J�n
            m_strDeleteSQL = "DELETE " + ABLTXMLDatEntity.TABLE_NAME
            '*����ԍ� 000002 2011/08/30 �ǉ��I��

            ' UPDATE Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN)
            strWhere.Append(" AND ")
            strWhere.Append(ABLTXMLDatEntity.RCPTYMD)
            strWhere.Append(" = ")
            strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD)

            If (blnKoshinCounter = True) Then
                strWhere.Append(" AND ")
                strWhere.Append(ABLTXMLDatEntity.KOSHINCOUNTER)
                strWhere.Append(" = ")
                strWhere.Append(ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER)
            Else
            End If

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateSakujoFGUFParameterCollectionClass = New UFParameterCollectionClass

            ' �폜�t���O�pUPDATE SQL���̍쐬
            m_strUpDateSQL_SakujoFG += ABLTXMLDatEntity.SAKUJOFG + " = " + ABLTXMLDatEntity.KEY_SAKUJOFG + ","

            ' ����Set��
            strSet.Append(ABLTXMLDatEntity.TANMATSUID).Append(" = ").Append(ABLTXMLDatEntity.KEY_TANMATSUID).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINCOUNTER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINCOUNTER).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINNICHIJI).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINNICHIJI).Append(",")
            strSet.Append(ABLTXMLDatEntity.KOSHINUSER).Append(" = ").Append(ABLTXMLDatEntity.KEY_KOSHINUSER)

            ' UPDATE SQL����WHERE��̒ǉ�
            m_strUpDateSQL_SakujoFG += strSet.ToString + strWhere.ToString

            '*-------------------------------------------------------------------------*
            ' �폜�t���O�pUPDATE �R���N�V�����Ƀp�����[�^��ǉ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_SAKUJOFG
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            '*-------------------------------------------------------------------------*
            ' UPDATE �R���N�V�����ɃL�[����ǉ�
            ' �[���h�c
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_TANMATSUID
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINCOUNTER
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V����
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINNICHIJI
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V���[�U
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.KEY_KOSHINUSER
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��M����
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��t�ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �w�l�k�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �\����t�ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD
            m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            If (blnKoshinCounter = True) Then
                ' �X�V�J�E���^
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER
                m_cfUpdateSakujoFGUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If
            '*-------------------------------------------------------------------------*

            '*����ԍ� 000002 2011/08/30 �ǉ��J�n
            ' DELETE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' DELETE SQL����WHERE��̒ǉ�
            m_strDeleteSQL += strWhere.ToString

            '*-------------------------------------------------------------------------*
            ' DELETE �R���N�V�����ɃL�[����ǉ�
            ' ��M����
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.JUSHINYMD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��t�ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTNO
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �w�l�k�A��
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.XMLRENBAN
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �\����t�ԍ�
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.RCPTYMD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            If (blnKoshinCounter = True) Then
                ' �X�V�J�E���^
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLTXMLDatEntity.PREFIX_KEY + ABLTXMLDatEntity.KOSHINCOUNTER
                m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If
            '*-------------------------------------------------------------------------*
            '*����ԍ� 000002 2011/08/30 �ǉ��I��

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
    End Sub
#End Region

#Region "�f�[�^�Z�b�g�쐬"
    '************************************************************************************************
    '* ���\�b�h��   ���k�s�`�w��M�c�`�s�����f�[�^�Z�b�g�쐬
    '* 
    '* �\��         Private Function CreateDataSet() As DataSet
    '* 
    '* �@�\�@�@     ���k�s�`�w��M�c�`�s�����f�[�^�Z�b�g���쐬����
    '* 
    '* ����         �Ȃ�
    '* 
    '* �߂�l       �쐬�������k�s�`�w��M�c�`�s�f�[�^�Z�b�g(DataSet)
    '************************************************************************************************
    Private Function CreateDataSet() As DataSet
        Const THIS_METHOD_NAME As String = "CreateDataSet"
        Dim csDataSet As DataSet                        ' �f�[�^�Z�b�g
        Dim csDataTable As DataTable                    ' �e�[�u��
        Dim csDataColumn As DataColumn                  ' �J����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' DataSet�̃C���X�^���X�쐬
            csDataSet = New DataSet

            ' �f�[�^�e�[�u���쐬
            csDataTable = csDataSet.Tables.Add(ABLTXmlDatCountData.TABLE_NAME)

            ' �J������`�̍쐬
            ' �Ŗڋ敪
            csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.TAXKB, System.Type.GetType("System.String"))
            ' �葱ID
            csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.PROCID, System.Type.GetType("System.String"))
            ' �葱��(��)
            csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.PROCRYAKUMEI, System.Type.GetType("System.String"))
            ' ����
            csDataColumn = csDataTable.Columns.Add(ABLTXmlDatCountData.COUNT, System.Type.GetType("System.String"))

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

        Return csDataSet

    End Function
#End Region

#Region "�葱����(��)�擾����"
    '************************************************************************************************
    '* ���\�b�h��   �葱����(��)�擾����
    '* 
    '* �\��         Private Function GetProcRyakumei(ByVal strProcId As String) As String
    '* 
    '* �@�\�@�@     �葱����(��)���擾����
    '* 
    '* ����         ByVal strProcId As String   �F�葱�h�c
    '* 
    '* �߂�l       
    '************************************************************************************************
    Private Function GetProcRyakumei(ByVal strProcId As String) As String
        Const THIS_METHOD_NAME As String = "GetProcRyakumei"
        Dim strProcRyakumei As String = String.Empty

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strProcId
                Case ABConstClass.ELTAX_PROCID_SHINKI
                    ' �葱�h�c:T0999910�C�葱����:�͏o�V�K
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_SHINKI

                Case ABConstClass.ELTAX_PROCID_HENKO_RIYOSHAJOHO
                    ' �葱�h�c:T0999920�C�葱����:�ύX(��)
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_HENKO_RIYOSHAJOHO

                Case ABConstClass.ELTAX_PROCID_HENKO_SHINKOKUSAKITAXKB
                    ' �葱�h�c:T0999910�C�葱����:�ύX(�\)
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_HENKO_SHINKOKUSAKITAXKB

                Case ABConstClass.ELTAX_PROCID_HAISHI
                    ' �葱�h�c:T0999910�C�葱����:�p�~
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_HAISHI

                Case ABConstClass.ELTAX_PROCID_SHOMEISHOSASIKAE
                    ' �葱�h�c:T0999910�C�葱����:�ؖ�����
                    strProcRyakumei = ABConstClass.ELTAX_PROCRYAKU_SHOMEISHOSASIKAE

                Case Else

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

        Return strProcRyakumei

    End Function
#End Region

#End Region

End Class
