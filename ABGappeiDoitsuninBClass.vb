'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        ��������l�c�`(ABGappeiDoitsuninBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/15�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/17 000001     �ǉ����A���ʍ��ڂ�ݒ肷��
'* 2003/04/25 000002     ��������l�O���[�v���o���\�b�h��ǉ�
'* 2003/05/13 000003     �c�a���X�L�[�}���擾����SQL���쐬
'* 2003/05/21 000004     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/21 000005     ��������l���o(GetDoitsunin)���\�b�h��ǉ�
'* 2007/07/27 000006     ����l��\�Ҏ擾�@�\�̒ǉ� (�g�V)
'* 2010/04/16 000007     VS2008�Ή��i��Áj
'* 2016/01/07 000008     �yAB00163�z�l����̓���l�Ή��i�΍��j
'* 2018/05/01 000009     �yAB27001�z�Y���҈ꗗ�ւ̓���l�敪�\���i�΍��j
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
'*����ԍ� 000009 2018/05/01 �ǉ��J�n
Imports System.Collections.Generic
'*����ԍ� 000009 2018/05/01 �ǉ��I��

Public Class ABGappeiDoitsuninBClass
    ' �����o�ϐ��̒�`
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_strInsertSQL As String                        ' INSERT�pSQL
    Private m_strUpdateSQL As String                        ' UPDATE�pSQL
    Private m_strDeleteSQL As String                        ' DELETE�pSQL
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass      'UPDATE�p�p�����[�^�R���N�V����
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass      'DELETE�p�p�����[�^�R���N�V����

    ' �R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABGappeiDoitsuninBClass"

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
        m_cfInsertUFParameterCollectionClass = Nothing
        m_cfUpdateUFParameterCollectionClass = Nothing
        m_cfDeleteUFParameterCollectionClass = Nothing
    End Sub

    '************************************************************************************************
    '* ���\�b�h��     ��������l�S�����o
    '* 
    '* �\��           Public Function GetDoitsuninAll(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��������l���Y���f�[�^��S���擾����B
    '* 
    '* ����           strJuminCD As String      :�Z���R�[�h
    '* 
    '* �߂�l         �擾������������l�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsGappeiDoitsuninEntity    �C���e���Z���X�FABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsuninAll(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninAll"            '���̃��\�b�h��
        Dim csGappeiDoitsuninEntity As DataSet                          '��������l�f�[�^
        Dim strSQL As New StringBuilder()                               'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim objErrorStruct As UFErrorStruct                             '�G���[��`�\����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �擾�������O���̎�
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_DATA_NOTFOUND)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' �擾�������P���̎�
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 1) Then
                ' SQL���̍쐬
                strSQL = New StringBuilder()
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                ' WHERE������
                strSQL.Append(" WHERE ")
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" = ")
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                strSQL.Append(" <> 1")

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
                cfUFParameterCollectionClass = New UFParameterCollectionClass()
                ' ���������̃p�����[�^���쐬
                ' ����l���ʃR�[�h
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
                cfUFParameterClass.Value = CType(csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString, String)
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

                ' SQL�̎��s DataSet�̎擾
                csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)
            End If

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

        Return csGappeiDoitsuninEntity

    End Function

    '* ����ԍ� 000005 2003/08/21 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ��������l���o
    '* 
    '* �\��           Public Function GetDoitsunin(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��������l���Y���f�[�^��S���擾����B
    '* 
    '* ����           strJuminCD As String      :�Z���R�[�h
    '* 
    '* �߂�l         �擾������������l�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsGappeiDoitsuninEntity    �C���e���Z���X�FABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsunin(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsunin"               '���̃��\�b�h��
        Dim csGappeiDoitsuninEntity As DataSet                          '��������l�f�[�^
        Dim strSQL As New StringBuilder()                               'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                             '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000007

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass()
            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass()
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �擾�������O���̎�
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                Exit Try
            End If

            ' �擾�������P���̎�
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 1) Then
                ' SQL���̍쐬
                strSQL = New StringBuilder()
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                ' WHERE������
                strSQL.Append(" WHERE ")
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" = ")
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                strSQL.Append(" <> 1")

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
                cfUFParameterCollectionClass = New UFParameterCollectionClass()
                ' ���������̃p�����[�^���쐬
                ' ����l���ʃR�[�h
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
                cfUFParameterClass.Value = CType(csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString, String)
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

                ' SQL�̎��s DataSet�̎擾
                csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)
            End If

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
        Finally
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)
        End Try

        Return csGappeiDoitsuninEntity

    End Function
    '* ����ԍ� 000005 2003/08/21 �ǉ��I��

    '*����ԍ� 000008 2016/01/07 �ǉ��J�n
    ''' <summary>
    ''' ����l�f�[�^�擾
    ''' </summary>
    ''' <param name="a_strJuminCD">�Z���R�[�h������z��</param>
    ''' <returns>����l�f�[�^</returns>
    ''' <remarks></remarks>
    Public Function GetDoitsunin(ByVal a_strJuminCD() As String) As DataSet

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csDataSet As DataSet
        Dim csSQL As StringBuilder
        Dim cfParameter As UFParameterClass
        Dim cfParameterCollection As UFParameterCollectionClass
        Dim strParameterName As String

        Try

            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            csSQL = New StringBuilder
            cfParameterCollection = New UFParameterCollectionClass

            With csSQL

                .Append("SELECT * FROM ")
                .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABGappeiDoitsuninEntity.JUMINCD)
                .Append(" IN (")

                For i As Integer = 0 To a_strJuminCD.Length - 1

                    ' -----------------------------------------------------------------------------
                    ' �Z���R�[�h
                    strParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD + i.ToString

                    If (i > 0) Then
                        .AppendFormat(", {0}", strParameterName)
                    Else
                        .Append(strParameterName)
                    End If

                    cfParameter = New UFParameterClass
                    cfParameter.ParameterName = strParameterName
                    cfParameter.Value = a_strJuminCD(i)
                    cfParameterCollection.Add(cfParameter)
                    ' -----------------------------------------------------------------------------

                Next i

                .Append(")")
                .Append(" AND ")
                .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                .Append(" <> '1'")

            End With

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + csSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)

            ' �擾�������P���ȏ�̎�
            If (csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count > 0) Then

                ' SQL���̍쐬
                csSQL = New StringBuilder
                cfParameterCollection = New UFParameterCollectionClass

                With csSQL

                    .Append("SELECT * FROM ")
                    .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                    .Append(" WHERE ")
                    .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                    .Append(" IN (")

                    For i As Integer = 0 To csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count - 1

                        ' -----------------------------------------------------------------------------
                        ' ����l���ʃR�[�h
                        strParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD + i.ToString

                        If (i > 0) Then
                            .AppendFormat(", {0}", strParameterName)
                        Else
                            .Append(strParameterName)
                        End If

                        cfParameter = New UFParameterClass
                        cfParameter.ParameterName = strParameterName
                        cfParameter.Value = _
                            csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(i).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString
                        cfParameterCollection.Add(cfParameter)
                        ' -----------------------------------------------------------------------------

                    Next i

                    .Append(")")
                    .Append(" AND ")
                    .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                    .Append(" <> '1'")
                    .Append(" ORDER BY ")
                    .Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                    .Append(", ")
                    .Append(ABGappeiDoitsuninEntity.HONNINKB)
                    .Append(", ")
                    .Append(ABGappeiDoitsuninEntity.JUMINCD)

                End With

                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + csSQL.ToString + "�z")

                ' SQL�̎��s DataSet�̎擾
                csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)

            Else
                ' noop
            End If

            ' �f�o�b�O�I�����O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch csAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + csAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + csAppExp.Message + "�z")
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

        Return csDataSet

    End Function
    '*����ԍ� 000008 2016/01/07 �ǉ��I��

    '*����ԍ� 000009 2018/05/01 �ǉ��J�n
    ''' <summary>
    ''' ����l�敪���̎擾
    ''' </summary>
    ''' <param name="csJuminCDList">�Z���R�[�h���X�g</param>
    ''' <returns>����l�敪����</returns>
    ''' <remarks></remarks>
    Public Function GetDoitsuninMeisho(ByVal csJuminCDList As List(Of String)) As Hashtable

        Dim THIS_METHOD_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim csResult As Hashtable
        Dim csDataSet As DataSet
        Dim csSQL As StringBuilder
        Dim cfParameter As UFParameterClass
        Dim cfParameterCollection As UFParameterCollectionClass
        Dim strParameterName As String

        Try

            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �ԐM�I�u�W�F�N�g�̃C���X�^���X��
            csResult = New Hashtable

            ' SQL���̍쐬
            csSQL = New StringBuilder
            cfParameterCollection = New UFParameterCollectionClass

            With csSQL

                .Append("SELECT * FROM ")
                .Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                .Append(" WHERE ")
                .Append(ABGappeiDoitsuninEntity.JUMINCD)
                .Append(" IN (")

                For i As Integer = 0 To csJuminCDList.Count - 1

                    ' -----------------------------------------------------------------------------
                    ' �Z���R�[�h
                    strParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD + i.ToString

                    If (i > 0) Then
                        .AppendFormat(", {0}", strParameterName)
                    Else
                        .Append(strParameterName)
                    End If

                    cfParameter = New UFParameterClass
                    cfParameter.ParameterName = strParameterName
                    cfParameter.Value = csJuminCDList(i)
                    cfParameterCollection.Add(cfParameter)
                    ' -----------------------------------------------------------------------------

                Next i

                .Append(")")
                .Append(" AND ")
                .Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                .Append(" <> '1'")

            End With

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + csSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDataSet = m_cfRdbClass.GetDataSet(csSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfParameterCollection)

            ' �擾�������P���ȏ�̎�
            If (csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count > 0) Then

                For Each csDataRow As DataRow In csDataSet.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows

                    ' -----------------------------------------------------------------------------
                    ' ����l�敪���̕ҏW
                    Select Case csDataRow.Item(ABGappeiDoitsuninEntity.HONNINKB).ToString.Trim
                        Case ABConstClass.HONNINKB.CODE.DAIHYO
                            csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, ABConstClass.HONNINKB.RYAKUSHO.DAIHYO)
                        Case ABConstClass.HONNINKB.CODE.DOITSUNIN
                            csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, ABConstClass.HONNINKB.RYAKUSHO.DOITSUNIN)
                        Case ABConstClass.HONNINKB.CODE.HAISHI
                            csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, ABConstClass.HONNINKB.RYAKUSHO.HAISHI)
                        Case Else
                            csResult.Add(csDataRow.Item(ABGappeiDoitsuninEntity.JUMINCD).ToString, String.Empty)
                    End Select
                    ' -----------------------------------------------------------------------------

                Next csDataRow

            Else
                ' noop
            End If

            ' �f�o�b�O�I�����O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch csAppExp As UFAppException

            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + csAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + csAppExp.Message + "�z")
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
    '*����ԍ� 000009 2018/05/01 �ǉ��I��

    '*����ԍ� 000006 2007/07/27 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     ��������l��\�Z���R�[�h�擾
    '* 
    '* �\��           Public Function GetDoitsuninDaihyoJuminCD(ByVal strJuminCD As String) As String
    '* 
    '* �@�\�@�@    �@ ��������l��\�̏Z���R�[�h���擾����
    '* 
    '* ����           strJuminCD As String      :�Z���R�[�h
    '* 
    '* �߂�l         �擾������������l�̊Y���f�[�^�iString�j
    '************************************************************************************************
    Public Function GetDoitsuninDaihyoJuminCD(ByVal strJuminCD As String) As String
        Const THIS_METHOD_NAME As String = "GetDoitsuninDaihyoJuminCD"         '���̃��\�b�h��
        Dim strDaihyoJuminCD As String                      '�Z���R�[�h�i��\�ҁj
        Dim csDaihyosyaEntity As DataSet              '����l��\�҃f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X


        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '��\�ҏ��̎擾���s��
            ' SQL���̍쐬
            strSQL.Append("SELECT A.")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(" A ")
            ' JOIN������
            strSQL.Append("JOIN (SELECT ")
            strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            strSQL.Append(" FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.HONNINKB)
            strSQL.Append(" IN ('0','1')")
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1) B ON A.")
            strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            strSQL.Append(" = B.")
            strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            ' WHERE������
            strSQL.Append(" WHERE A.")
            strSQL.Append(ABGappeiDoitsuninEntity.HONNINKB)
            strSQL.Append(" = '0'")


            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass
            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csDaihyosyaEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �擾�������O���̎�
            If (csDaihyosyaEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                '����l�Ǘ�����Ă��Ȃ��ꍇ�́A�w�肳�ꂽ�Z���R�[�h��ԋp����
                strDaihyoJuminCD = strJuminCD
            Else
                '����l�Ǘ�����Ă���ꍇ�́A����l��\�҂̏Z���R�[�h��ԋp����
                strDaihyoJuminCD = CStr(csDaihyosyaEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.JUMINCD))
            End If

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

        Return strDaihyoJuminCD

    End Function
    '*����ԍ� 000006 2007/07/27 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     ��������l�{�l���o
    '* 
    '* �\��           Public Function GetDoitsuninHonnin(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��������l���Y���f�[�^���擾����B
    '* 
    '* ����           strJuminCD As String      :�Z���R�[�h
    '* 
    '* �߂�l         �擾������������l�̊Y���f�[�^�iDataSet�j
    '*                   �\���FcsGappeiDoitsuninEntity    �C���e���Z���X�FABGappeiDoitsuninEntity
    '************************************************************************************************
    Public Function GetDoitsuninHonnin(ByVal strJuminCD As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetDoitsuninHonnin"         '���̃��\�b�h��
        Dim objErrorStruct As UFErrorStruct                             '�G���[��`�\����
        Dim csGappeiDoitsuninEntity As DataSet                          '��������l�f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> 1")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass
            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �擾�������O���̎�
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_DATA_NOTFOUND)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            ' �擾�������P���̎�
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 1) Then
                ' SQL���̍쐬
                strSQL = New StringBuilder
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
                ' WHERE������
                strSQL.Append(" WHERE ")
                strSQL.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" = ")
                strSQL.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABGappeiDoitsuninEntity.HONNINKB)
                strSQL.Append(" = '0'")
                strSQL.Append(" AND ")
                strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
                strSQL.Append(" <> '1'")

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
                cfUFParameterCollectionClass = New UFParameterCollectionClass
                ' ���������̃p�����[�^���쐬
                ' ����l���ʃR�[�h
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
                cfUFParameterClass.Value = CType(csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD).ToString, String)
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

                ' SQL�̎��s DataSet�̎擾
                csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)
            End If

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

        Return csGappeiDoitsuninEntity

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��������l�O���[�v���o
    '* 
    '* �\��           Public Function GetDoitsuninGroup(ByVal strJuminCD As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@��������l���Y���f�[�^��S���擾����B
    '* 
    '* ����           strJuminCD As String      :�Z���R�[�h
    '* 
    '* �߂�l         ���ʃR�[�h(String)         
    '************************************************************************************************
    Public Function GetDoitsuninGroup(ByVal strJuminCD As String) As String
        Const THIS_METHOD_NAME As String = "GetDoitsuninGroup"          '���̃��\�b�h��
        Dim objErrorStruct As UFErrorStruct                             '�G���[��`�\����
        Dim csGappeiDoitsuninEntity As DataSet                          '��������l�f�[�^
        Dim strSQL As New StringBuilder                                 'SQL��������
        Dim cfUFParameterClass As UFParameterClass                      '�p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  '�p�����[�^�R���N�V�����N���X
        Dim strShikibetsuCD As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���̍쐬
            strSQL.Append("SELECT * FROM ")
            strSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
            ' WHERE������
            strSQL.Append(" WHERE ")
            strSQL.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strSQL.Append(" = ")
            strSQL.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strSQL.Append(" AND ")
            strSQL.Append(ABGappeiDoitsuninEntity.SAKUJOFG)
            strSQL.Append(" <> '1'")

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass
            ' ���������̃p�����[�^���쐬
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            cfUFParameterClass.Value = strJuminCD
            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:GetDataSet�z" + _
                                        "�ySQL���e:" + strSQL.ToString + "�z")

            ' SQL�̎��s DataSet�̎擾
            csGappeiDoitsuninEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABGappeiDoitsuninEntity.TABLE_NAME, cfUFParameterCollectionClass)

            ' �擾�������O���̎�
            If (csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows.Count() = 0) Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' �G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_DATA_NOTFOUND)
                ' ��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            strShikibetsuCD = CType(csGappeiDoitsuninEntity.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Rows(0).Item(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD), String)


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

        Return strShikibetsuCD

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��������l�ǉ�
    '* 
    '* �\��           Public Function InsertDoitsunin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  ��������l�Ƀf�[�^��ǉ�����B
    '* 
    '* ����           csDataRow As DataRow  :�ǉ��f�[�^
    '* 
    '* �߂�l         �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertDoitsunin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertDoitsunin"            '���̃��\�b�h��
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        Dim csDataColumn As DataColumn
        Dim intInsCnt As Integer                            '�ǉ�����
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
            csDataRow(ABGappeiDoitsuninEntity.TANMATSUID) = m_cfControlData.m_strClientId           '�[���h�c
            csDataRow(ABGappeiDoitsuninEntity.SAKUJOFG) = "0"                                       '�폜�t���O
            csDataRow(ABGappeiDoitsuninEntity.KOSHINCOUNTER) = Decimal.Zero                         '�X�V�J�E���^
            csDataRow(ABGappeiDoitsuninEntity.SAKUSEINICHIJI) = strUpdateDateTime                   '�쐬����
            csDataRow(ABGappeiDoitsuninEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId            '�쐬���[�U�[
            csDataRow(ABGappeiDoitsuninEntity.KOSHINNICHIJI) = strUpdateDateTime                    '�X�V����
            csDataRow(ABGappeiDoitsuninEntity.KOSHINUSER) = m_cfControlData.m_strUserId             '�X�V���[�U�[

            ' ���N���X�̃f�[�^�������`�F�b�N���s��
            For Each csDataColumn In csDataRow.Table.Columns
                ' �f�[�^�������`�F�b�N
                CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString.Trim)
            Next csDataColumn

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_strInsertSQL + "�z")

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
    '* ���\�b�h��     ��������l�X�V
    '* 
    '* �\��           Public Function UpdateDoitsunin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  ��������l�̃f�[�^���X�V����B
    '* 
    '* ����           csDataRow As DataRow  :�X�V�f�[�^
    '* 
    '* �߂�l         �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateDoitsunin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateDoitsunin"            '���̃��\�b�h��
        Dim cfParam As UFParameterClass                                 '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim intUpdCnt As Integer                                        '�X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strUpdateSQL Is Nothing Or m_strUpdateSQL = String.Empty Or _
                m_cfUpdateUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABGappeiDoitsuninEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   '�[���h�c
            csDataRow(ABGappeiDoitsuninEntity.KOSHINCOUNTER) = CDec(csDataRow(ABGappeiDoitsuninEntity.KOSHINCOUNTER)) + 1   '�X�V�J�E���^
            csDataRow(ABGappeiDoitsuninEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")   '�X�V����
            csDataRow(ABGappeiDoitsuninEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     '�X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABGappeiDoitsuninEntity.PREFIX_KEY.RLength) = ABGappeiDoitsuninEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �f�[�^�������`�F�b�N
                    CheckColumnValue(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength), csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString.Trim)
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_strUpdateSQL + "�z")

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

            '�V�X�e���G���[���X���[����
            Throw exException

        End Try

        Return intUpdCnt

    End Function

    '************************************************************************************************
    '* ���\�b�h��     ��������l�폜�i�����j
    '* 
    '* �\��           Public Function DeleteDoitsunin(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@  ��������l�̃f�[�^���폜�i�����j����B
    '* 
    '* ����           csDataRow As DataRow      :�폜�f�[�^
    '* 
    '* �߂�l         �폜�i�����j����(Integer)
    '************************************************************************************************
    Public Function DeleteDoitsunin(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteDoitsunin�i�����j"
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim cfParam As UFParameterClass                     '�p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim intDelCnt As Integer                            '�폜����

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
                If (cfParam.ParameterName.RSubstring(0, ABGappeiDoitsuninEntity.PREFIX_KEY.RLength) = ABGappeiDoitsuninEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABGappeiDoitsuninEntity.PREFIX_KEY.RLength), DataRowVersion.Current).ToString()
                End If
            Next cfParam

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:ExecuteSQL�z" + _
                                        "�ySQL���e:" + m_strDeleteSQL + "�z")

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
        Const THIS_METHOD_NAME As String = "CreateSQL"
        Dim csDataColumn As DataColumn
        Dim cfUFParameterClass As UFParameterClass          '�p�����[�^�N���X
        Dim strInsertColumn As String                       '�ǉ�SQL�����ڕ�����
        Dim strInsertParam As String                        '�ǉ�SQL���p�����[�^������
        Dim strDeleteSQL As New StringBuilder               '�폜SQL��������
        Dim strWhere As New StringBuilder                   '�X�V�폜SQL��Where��������

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABGappeiDoitsuninEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' �X�V�폜Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABGappeiDoitsuninEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABGappeiDoitsuninEntity.KEY_JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD)
            strWhere.Append(" = ")
            strWhere.Append(ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABGappeiDoitsuninEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABGappeiDoitsuninEntity.KEY_KOSHINCOUNTER)

            ' UPDATE SQL���̍쐬
            m_strUpdateSQL = "UPDATE " + ABGappeiDoitsuninEntity.TABLE_NAME + " SET "

            ' DELETE�i�����j SQL���̍쐬
            strDeleteSQL.Append("DELETE FROM ")
            strDeleteSQL.Append(ABGappeiDoitsuninEntity.TABLE_NAME)
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
                '' �J���������݂���ꍇ
                'If (m_csSchema.Tables(ABGappeiDoitsuninEntity.TABLE_NAME).Columns.Contains(csDataColumn.ColumnName)) Then

                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' SQL���̍쐬
                m_strUpdateSQL += csDataColumn.ColumnName + " = " + ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
                'End If

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
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ����l���ʃR�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_DOITSUNINSHIKIBETSUCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABGappeiDoitsuninEntity.KEY_KOSHINCOUNTER
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
    '* �@�\�@�@       ��������̃f�[�^�������`�F�b�N���s���܂��B
    '* 
    '* ����           strColumnName As String
    '*                strValue As String
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)
        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            Select Case strColumnName.ToUpper()
                Case ABGappeiDoitsuninEntity.JUMINCD                    '�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_JUMINCD)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.SHICHOSONCD                '�s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SHICHOSONCD)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.KYUSHICHOSONCD             '���s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KYUSHICHOSONCD)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.DOITSUNINSHIKIBETSUCD      '����l���ʃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_DOITSUNINSHIKIBETSUCD)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.HONNINKB                   '�{�l�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_HONNINKB)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.HANYOKB1                   '�ėp�敪1
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_HANYOKB1)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.HANYOKB2                   '�ėp�敪2
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_HANYOKB2)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.BIKO                       '���l
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_BIKO)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.RESERVE                    '���U�[�u
                    ' �������Ȃ�
                Case ABGappeiDoitsuninEntity.TANMATSUID                 '�[��ID
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_TANMATSUID)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.SAKUJOFG                   '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SAKUJOFG)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.KOSHINCOUNTER              '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KOSHINCOUNTER)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.SAKUSEINICHIJI             '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SAKUSEINICHIJI)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.SAKUSEIUSER                '�쐬���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_SAKUSEIUSER)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.KOSHINNICHIJI              '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KOSHINNICHIJI)
                        ' ��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABGappeiDoitsuninEntity.KOSHINUSER                 '�X�V���[�U
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                        ' �G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABGAPPEIDOITSUNINB_RDBDATATYPE_KOSHINUSER)
                        ' ��O�𐶐�
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

End Class
