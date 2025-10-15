'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        ������Ԏ擾(ABAkibanShutokuBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/20�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/04/19  000001      �Z���R�[�h�擾(�����@�l�p)�E(�������L�p)�����ǉ� 
'* 2007/02/05  000002      �����X�V�G���[���O�ԍ��擾�����ǉ��i���R(��)�j
'* 2007/04/02  000003      �R�[�h�擾���̑��݃`�F�b�N�������C���i��Áj
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.Common
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports System.Text

Public Class ABAkibanShutokuBClass

    ' �����o�ϐ��̒�`
    Private m_cfUFLogClass As UFLogClass            '���O�o�̓N���X
    Private m_cfUFControlData As UFControlData      '�R���g���[���f�[�^

    '�p�����[�^�̃����o�ϐ�
    Private m_strBango As String                    '�擾�ԍ�

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAkibanShutokuBClass"

    '�e�����o�ϐ��̃v���p�e�B��`
    Public ReadOnly Property p_strBango() As String
        Get
            Return m_strBango
        End Get
    End Property

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigData As UFConfigDataClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfUFControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                 cfUFConfigData As UFConfigDataClass      : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigData As UFConfigDataClass)

        '�����o�ϐ��Z�b�g
        m_cfUFControlData = cfControlData

        '���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(cfConfigData, cfControlData.m_strBusinessId)

        '�p�����[�^�̃����o�ϐ�
        m_strBango = String.Empty
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �Z���R�[�h�擾
    '* 
    '* �\��            Public Sub GetJuminCD()
    '* 
    '* �@�\�@�@        ��Ԃ��擾����B
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub GetJuminCD()
        Const THIS_METHOD_NAME As String = "GetJuminCD"             '���̃��\�b�h��

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�ԍ��擾�N���X�R���X�g���N�^�Z�b�g
            Dim cuGetNum As New USSnumgetClass("AB", "0001", "0000")

            '*����ԍ� 000003 2007/04/02 �C���J�n
            ' �R�[�h���݃`�F�b�N
            AtenaDBChecKCD(cuGetNum, "0")

            ''�Z���R�[�h���P���擾
            'cuGetNum.GetNum(m_cfUFControlData)

            ''�擾�ԍ����v���p�e�B�ɃZ�b�g
            'm_strBango = cuGetNum.p_strBango(0)
            '*����ԍ� 000003 2007/04/02 �C���I��

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:THIS_METHOD_NAME�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �Z���R�[�h�擾�i�����p�j
    '* 
    '* �\��            Public Sub GetAtenaJuminCD()
    '* 
    '* �@�\�@�@        ��Ԃ��擾����B
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub GetAtenaJuminCD()
        Const THIS_METHOD_NAME As String = "GetAtenaJuminCD"            '���̃��\�b�h��

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�ԍ��擾�N���X�R���X�g���N�^�Z�b�g
            Dim cuGetNum As New USSnumgetClass("AB", "0002", "0000")

            '*����ԍ� 000003 2007/04/02 �C���J�n
            AtenaDBChecKCD(cuGetNum, "0")

            ''�Z���R�[�h�i�����p�j���P���擾
            'cuGetNum.GetNum(m_cfUFControlData)

            ''�擾�ԍ����v���p�e�B�ɃZ�b�g
            'm_strBango = cuGetNum.p_strBango(0)
            '*����ԍ� 000003 2007/04/02 �C���I��

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:THIS_METHOD_NAME�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      ���уR�[�h�擾
    '* 
    '* �\��            Public Sub GetSetaiCD()
    '* 
    '* �@�\�@�@        ��Ԃ��擾����B
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub GetSetaiCD()
        Const THIS_METHOD_NAME As String = "GetSetaiCD"             '���̃��\�b�h��

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�ԍ��擾�N���X�R���X�g���N�^�Z�b�g
            Dim cuGetNum As New USSnumgetClass("AB", "0003", "0000")

            '*����ԍ� 000003 2007/04/02 �C���J�n
            AtenaDBChecKCD(cuGetNum, "1")

            ''���уR�[�h���P���擾
            'cuGetNum.GetNum(m_cfUFControlData)

            ''�擾�ԍ����v���p�e�B�ɃZ�b�g
            'm_strBango = cuGetNum.p_strBango(0)
            '*����ԍ� 000003 2007/04/02 �C���I��

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:THIS_METHOD_NAME�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      ���уR�[�h�擾�i�����p�j
    '* 
    '* �\��            Public Sub GetAtenaSetaiCD()
    '* 
    '* �@�\�@�@        ��Ԃ��擾����B
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub GetAtenaSetaiCD()
        Const THIS_METHOD_NAME As String = "GetAtenaSetaiCD"        '���̃��\�b�h��

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�ԍ��擾�N���X�R���X�g���N�^�Z�b�g
            Dim cuGetNum As New USSnumgetClass("AB", "0004", "0000")

            '*����ԍ� 000003 2007/04/02 �C���J�n
            AtenaDBChecKCD(cuGetNum, "1")

            ''���уR�[�h�i�����p�j���P���擾
            'cuGetNum.GetNum(m_cfUFControlData)

            ''�擾�ԍ����v���p�e�B�ɃZ�b�g
            'm_strBango = cuGetNum.p_strBango(0)
            '*����ԍ� 000003 2007/04/02 �C���I��

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:THIS_METHOD_NAME�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      ���L�҃R�[�h�擾
    '* 
    '* �\��            Public Sub GetKyoyuCD()
    '* 
    '* �@�\�@�@        ��Ԃ��擾����B
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub GetKyoyuCD()
        Const THIS_METHOD_NAME As String = "GetKyoyuCD"             '���̃��\�b�h��

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�ԍ��擾�N���X�R���X�g���N�^�Z�b�g
            Dim cuGetNum As New USSnumgetClass("AB", "0005", "0000")

            '*����ԍ� 000003 2007/04/02 �C���J�n
            AtenaDBChecKCD(cuGetNum, "0")

            ''���L�҃R�[�h���P���擾
            'cuGetNum.GetNum(m_cfUFControlData)

            ''�擾�ԍ����v���p�e�B�ɃZ�b�g
            'm_strBango = cuGetNum.p_strBango(0)
            '*����ԍ� 000003 2007/04/02 �C���I��

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:THIS_METHOD_NAME�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp

        End Try
    End Sub

    '*����ԍ� 000001 2004/04/19 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��      �Z���R�[�h�擾�i�����@�l�p�j
    '* 
    '* �\��            Public Sub GetAtenaHojinCD()
    '* 
    '* �@�\�@�@        ��Ԃ��擾����B
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub GetAtenaHojinCD()
        Const THIS_METHOD_NAME As String = "GetAtenaHojinCD"            '���̃��\�b�h��

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�ԍ��擾�N���X�R���X�g���N�^�Z�b�g
            Dim cuGetNum As New USSnumgetClass("AB", "0006", "0000")

            '*����ԍ� 000003 2007/04/02 �C���J�n
            AtenaDBChecKCD(cuGetNum, "0")

            ''�Z���R�[�h�i�����p�j���P���擾
            'cuGetNum.GetNum(m_cfUFControlData)

            ''�擾�ԍ����v���p�e�B�ɃZ�b�g
            'm_strBango = cuGetNum.p_strBango(0)
            '*����ԍ� 000003 2007/04/02 �C���I��

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:THIS_METHOD_NAME�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp

        End Try
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �Z���R�[�h�擾�i�������L�p�j
    '* 
    '* �\��            Public Sub GetAtenaKyoyuCD()
    '* 
    '* �@�\�@�@        ��Ԃ��擾����B
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub GetAtenaKyoyuCD()
        Const THIS_METHOD_NAME As String = "GetAtenaKyoyuCD"            '���̃��\�b�h��

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�ԍ��擾�N���X�R���X�g���N�^�Z�b�g
            Dim cuGetNum As New USSnumgetClass("AB", "0007", "0000")

            '*����ԍ� 000003 2007/04/02 �C���J�n
            AtenaDBChecKCD(cuGetNum, "0")

            ''�Z���R�[�h�i�����p�j���P���擾
            'cuGetNum.GetNum(m_cfUFControlData)

            ''�擾�ԍ����v���p�e�B�ɃZ�b�g
            'm_strBango = cuGetNum.p_strBango(0)
            '*����ԍ� 000003 2007/04/02 �C���I��

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:THIS_METHOD_NAME�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp

        End Try
    End Sub
    '*����ԍ� 000001 2004/04/19 �ǉ��I��

    '*����ԍ� 000003 2007/04/02 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��      �R�[�h�擾���̑��݃`�F�b�N
    '* 
    '* �\��            Public Sub AtenaDBChecKCD(ByVal cuGetNum As USSnumgetClass, ByVal strChkCD As String)
    '* 
    '* �@�\�@�@        �擾�����R�[�h�������c�a��ɑ��݂��Ȃ����`�F�b�N���s���B
    '* 
    '* ����            cuGetNum As USSnumgetClass   :�ԍ��擾�N���X 
    '*                 strChkCD As String           :�R�[�h�擾����t���O
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub AtenaDBChecKCD(ByVal cuGetNum As USSnumgetClass, ByVal strChkCD As String)
        Const THIS_METHOD_NAME As String = "AtenaDBChecKCD"     ' ���\�b�h��
        Dim cfRdb As UFRdbClass                                 ' RDB�N���X
        Dim blnChkCD As Boolean = True                          ' �R�[�h���݃`�F�b�N�t���O
        Dim csSB As StringBuilder
        Dim cfParamCollection As UFParameterCollectionClass     ' �p�����[�^�R���N�V�����N���X
        Dim cfDataReder As UFDataReaderClass                    ' �f�[�^���[�_�[�N���X

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �C���X�^���X��
            csSB = New StringBuilder
            cfParamCollection = New UFParameterCollectionClass

            ' SQL�쐬
            '* SELECT JUMINCD FROM ABATENA WHERE JUMINCD = @JUMINCD
            '* SELECT JUMINCD FROM ABATENA WHERE STAICD = @STAICD
            csSB.Append("SELECT ").Append(ABAtenaEntity.JUMINCD)
            csSB.Append(" FROM ").Append(ABAtenaEntity.TABLE_NAME)
            If (strChkCD = "0") Then
                ' �Z���R�[�h�̑��ݒl�`�F�b�N
                csSB.Append(" WHERE ").Append(ABAtenaEntity.JUMINCD)
                csSB.Append(" = ").Append(ABAtenaEntity.PARAM_JUMINCD)
            Else
                ' ���уR�[�h�̑��ݒl�`�F�b�N
                csSB.Append(" WHERE ").Append(ABAtenaEntity.STAICD)
                csSB.Append(" = ").Append(ABAtenaEntity.PARAM_STAICD)
            End If

            ' RDB�N���X�̃C���X�^���X�쐬
            cfRdb = New UFRdbClass(m_cfUFControlData.m_strBusinessId)
            ' RDB�ڑ�
            cfRdb.Connect()

            Try
                ' �󂫃R�[�h��������܂ŌJ��Ԃ�
                While blnChkCD
                    ' ��Ԏ擾
                    cuGetNum.GetNum(m_cfUFControlData)

                    cfParamCollection.Clear()
                    ' �Z���R�[�h�����уR�[�h�����f
                    If (strChkCD = "0") Then
                        ' �Z���R�[�h�̏ꍇ
                        cfParamCollection.Add(ABAtenaEntity.PARAM_JUMINCD, cuGetNum.p_strBango(0))
                    Else
                        ' ���уR�[�h�̏ꍇ
                        cfParamCollection.Add(ABAtenaEntity.PARAM_STAICD, cuGetNum.p_strBango(0))
                    End If

                    cfDataReder = cfRdb.GetDataReader(csSB.ToString, cfParamCollection)
                    If (cfDataReder.Read = False) Then
                        ' �R�[�h�����݂��Ȃ��ꍇ
                        ' �`�F�b�N�t���O��False�ɂ���
                        blnChkCD = False
                    End If
                    cfDataReder.Close()

                End While
            Catch
                ' �G���[�����̂܂܃X���[
                Throw
            Finally
                ' RDB�A�N�Z�X���O�o��
                m_cfUFLogClass.RdbWrite(m_cfUFControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���s���\�b�h��:Disconnect�z")
                ' RDB�ؒf
                cfRdb.Disconnect()
            End Try

            ' �擾�ԍ����v���p�e�B�ɃZ�b�g
            m_strBango = cuGetNum.p_strBango(0)

            ' �f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:THIS_METHOD_NAME�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw

        End Try
    End Sub
    '*����ԍ� 000003 2007/04/02 �ǉ��I��

    '*����ԍ� 000002 2007/02/05 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��      �����X�V�G���[���O�ԍ��擾
    '* 
    '* �\��            Public Sub GetErrLogNo()
    '* 
    '* �@�\�@�@        ��Ԃ��擾����B
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub GetErrLogNo()

        Const THIS_METHOD_NAME As String = "GetErrLogNo"          ' ���\�b�h��

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �ԍ��擾�N���X�R���X�g���N�^�Z�b�g
            Dim cuGetNum As New USSnumgetClass("AB", "2001", "0000")

            ' �����X�V�G���[���O�ԍ����P���擾
            cuGetNum.GetNum(m_cfUFControlData)

            ' �擾�ԍ����v���p�e�B�ɃZ�b�g
            m_strBango = cuGetNum.p_strBango(0)

            ' �f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:THIS_METHOD_NAME�z�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp

        End Try

    End Sub
    '*����ԍ� 000002 2007/02/05 �ǉ��I��

End Class
