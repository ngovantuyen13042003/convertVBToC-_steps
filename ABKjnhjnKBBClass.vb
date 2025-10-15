'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �@�l�l(ABKjnhjnKBBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2002/12/17�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 
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

Public Class ABKjnhjnKBBClass
    ' �����o�ϐ��̒�`
    Private m_cfUFLogClass As UFLogClass            '���O�o�̓N���X
    Private m_cfUFControlData As UFControlData      '�R���g���[���f�[�^

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABKjnhjnKBBClass"

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��            Public Sub New(ByVal cfControlData AS UFControlData,
    '*         �@�@�@�@               ByVal cfConfigData  AS UFConfigDataClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����            cfUFControlData As UFControlData         : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                 cfUFConfigDataClass As UFConfigDataClass : �R���t�B�O�f�[�^�I�u�W�F�N�g 
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfConfigDataClass As UFConfigDataClass)
        '�����o�ϐ��Z�b�g
        m_cfUFControlData = cfControlData
        '���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �l�@�l�擾
    '* 
    '* �\��            Public Function GetKjnhjn(strKjnhjnKB As String) As String
    '* 
    '* �@�\�@�@        �敪���Ǔ��ǊO���̂��擾
    '* 
    '* ����            strKjnhjnKB As String   :�l�@�l�敪
    '* 
    '* �߂�l          �l�@�l����
    '************************************************************************************************
    Public Function GetKjnhjn(ByVal strKjnhjnKB As String) As String
        Dim strMeisho As String = String.Empty
        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKjnhjn")

            Select Case strKjnhjnKB
                Case "1"
                    strMeisho = "�l"
                Case "2"
                    strMeisho = "�@�l"
            End Select

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKjnhjn")
        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:GetKjnhjn�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return strMeisho
    End Function

    '************************************************************************************************
    '* ���\�b�h��      �l�@�l�ҏW
    '* 
    '* �\��            Public Function HenKangaiKangai() As DataSet
    '* 
    '* �@�\�@�@        �l�@�l�̃R�[�h�Ɩ��̂�ҏW����
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �l�@�l���́iDataSet�j
    '*                   �\���FcsKjnHjnData    �C���e���Z���X�FABKjnHjnData
    '************************************************************************************************
    Public Function HenKangaiKangai() As DataSet
        Dim csKjnHjnData As New DataSet()
        Dim csKjnHjnDataTbl As DataTable
        Dim csKjnHjnDataRow As DataRow

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKangaiKangai")

            '�e�[�u�����쐬����
            csKjnHjnDataTbl = csKjnHjnData.Tables.Add(ABKjnHjnData.TABLE_NAME)

            '�e�[�u���z���ɕK�v�t�B�[���h��p�ӂ���
            csKjnHjnDataTbl.Columns.Add(ABKjnHjnData.KJNHJNKB, System.Type.GetType("System.String"))
            csKjnHjnDataTbl.Columns.Add(ABKjnHjnData.KJNHJNKBMEI, System.Type.GetType("System.String"))

            '�e�t�B�[���h�Ƀf�[�^���i�[����
            '�l�@�l�敪 = 1
            csKjnHjnDataRow = csKjnHjnDataTbl.NewRow()
            csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKB) = "1"
            csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKBMEI) = "�l"
            '�f�[�^�̒ǉ�
            csKjnHjnData.Tables(ABKjnHjnData.TABLE_NAME).Rows.Add(csKjnHjnDataRow)

            '�l�@�l�敪 = 2
            csKjnHjnDataRow = csKjnHjnDataTbl.NewRow()
            csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKB) = "2"
            csKjnHjnDataRow.Item(ABKjnHjnData.KJNHJNKBMEI) = "�@�l"
            '�f�[�^�̒ǉ�
            csKjnHjnData.Tables(ABKjnHjnData.TABLE_NAME).Rows.Add(csKjnHjnDataRow)

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKangaiKangai")
        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:HenKangaiKangai�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csKjnHjnData
    End Function

End Class
