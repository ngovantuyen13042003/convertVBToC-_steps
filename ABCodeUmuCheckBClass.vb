'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        �R�[�h���݃`�F�b�N�a(ABCodeUmuCheckBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/04/21�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/05/22 000001     RDB��Connect��ҿ��ނ̐擪�ɕύX(�d�l�ύX)
'* 2010/04/16  000002      VS2008�Ή��i��Áj
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools

Public Class ABCodeUmuCheckBClass

    ' �p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_blnCodeUmu As Boolean                         ' �R�[�h�L��

    '�@�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABCodeUmuCheckBClass"            '�N���X��
    Private Const THIS_BUSINESSID As String = "AB"                              '�Ɩ��R�[�h

    '************************************************************************************************
    '* �e�����o�ϐ��̃v���p�e�B��`
    '************************************************************************************************

    Public Property p_blnCodeUmu() As Boolean
        Get
            Return m_blnCodeUmu
        End Get
        Set(ByVal Value As Boolean)
            m_blnCodeUmu = Value
        End Set
    End Property

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControldata As UFControlData, 
    '*                                 ByVal cfConfigData As UFConfigDataClass,
    '*                                 ByVal cfRdb As UFRdbClass)
    '* 
    '* �@�\           ����������
    '* 
    '* ����           cfControlData As UFControlData        : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                  cfConfigData As UFConfigDataClass     : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                  cfRdb As UFRdbClass                   : �q�c�a�I�u�W�F�N�g
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControldata As UFControlData, _
                   ByVal cfConfigData As UFConfigDataClass)
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Const THIS_METHOD_NAME As String = "New"            '���\�b�h��
        '* corresponds to VS2008 End 2010/04/16 000002

        ' �����o�ϐ��Z�b�g
        m_cfControlData = cfControldata
        m_cfConfigDataClass = cfConfigData

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' RDB�N���X�̃C���X�^���X�쐬
        m_cfRdbClass = New UFRdbClass(THIS_BUSINESSID)

        ' �����o�ϐ��̏�����
        m_blnCodeUmu = False
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �Z���R�[�h�L���`�F�b�N
    '* 
    '* �\��           Public Sub JuminCDUmuCheck(ByVal strJuminCD As String)
    '* 
    '* �@�\�@�@        �Z���R�[�h�����݂��邩�`�F�b�N����B
    '* 
    '* ����           strJuminCD As String          : �Z���R�[�h
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub JuminCDUmuCheck(ByVal strJuminCD As String)
        Const THIS_METHOD_NAME As String = "JuminCDUmuCheck"
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim cAtenaB As ABAtenaBClass                        '�����c�`�N���X
        Dim cAtenaSearchKey As New ABAtenaSearchKey()       '���������L�[
        Dim csAtenaEntity As DataSet                        '����Entity
        Dim intDataCount As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:Connect�z")
            ' RDB�ڑ�
            m_cfRdbClass.Connect()

            Try
                ' �����擾�C���X�^���X��
                cAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                cAtenaSearchKey.p_strJuminCD = strJuminCD

                ' �����c�`�N���X�̈����擾���]�b�g�����s
                csAtenaEntity = cAtenaB.GetAtenaBHoshu(1, cAtenaSearchKey, True)

                intDataCount = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count()

                ' �f�[�^���O���̂Ƃ���
                If (intDataCount = 0) Then
                    m_blnCodeUmu = False
                Else
                    m_blnCodeUmu = True
                End If

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
                ' UFAppException���X���[����
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' �G���[�����̂܂܃X���[
                Throw

            Finally
                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:Disconnect�z")
                ' RDB�ؒf
                m_cfRdbClass.Disconnect()
            End Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            '���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp

        End Try

    End Sub

    '************************************************************************************************
    '* ���\�b�h��      ���уR�[�h�L���`�F�b�N
    '* 
    '* �\��           Public Sub StaiCDUmuCheck(ByVal strStaiCD As String)
    '* 
    '* �@�\�@�@        ���уR�[�h�����݂��邩�`�F�b�N����B
    '* 
    '* ����           strStaiCD As String          : ���уR�[�h
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub StaiCDUmuCheck(ByVal strStaiCD As String)
        Const THIS_METHOD_NAME As String = "StaiCDUmuCheck"
        '* corresponds to VS2008 Start 2010/04/16 000002
        'Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000002
        Dim cAtenaB As ABAtenaBClass                        '�����c�`�N���X
        Dim cAtenaSearchKey As New ABAtenaSearchKey()       '���������L�[
        Dim csAtenaEntity As DataSet                        '����Entity
        Dim intDataCount As Integer

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:Connect�z")
            ' RDB�ڑ�
            m_cfRdbClass.Connect()

            Try
                ' �����擾�C���X�^���X��
                cAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

                cAtenaSearchKey.p_strStaiCD = strStaiCD

                ' �����c�`�N���X�̈����擾���]�b�g�����s
                csAtenaEntity = cAtenaB.GetAtenaBHoshu(1, cAtenaSearchKey, True)

                intDataCount = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Count()

                ' �f�[�^���O���̂Ƃ���
                If (intDataCount = 0) Then
                    m_blnCodeUmu = False
                Else
                    m_blnCodeUmu = True
                End If

            Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
                ' ���[�j���O���O�o��
                m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
                ' UFAppException���X���[����
                Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

            Catch
                ' �G���[�����̂܂܃X���[
                Throw

            Finally
                ' RDB�A�N�Z�X���O�o��
                m_cfLogClass.RdbWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:Disconnect�z")
                ' RDB�ؒf
                m_cfRdbClass.Disconnect()
            End Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            '���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp

        End Try

    End Sub

End Class
