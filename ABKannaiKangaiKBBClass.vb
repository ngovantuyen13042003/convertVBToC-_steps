'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �Ǔ��ǊO(ABKannaiKangaiKBBClass)
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

Public Class ABKannaiKangaiKBBClass
    ' �����o�ϐ��̒�`
    Private m_cfUFLogClass As UFLogClass                    '���O�o�̓N���X
    Private m_cfUFControlData As UFControlData              '�R���g���[���f�[�^
    Private m_cfUFConfigDataClass As UFConfigDataClass      '�R���t�B�O�f�[�^

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABJuminShubetsuBClass"

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
    Public Sub New(ByVal cfControlData As UFControlData, ByVal cfUFConfigDataClass As UFConfigDataClass)

        '�����o�ϐ��Z�b�g
        m_cfUFControlData = cfControlData
        m_cfUFConfigDataClass = cfUFConfigDataClass

        '���O�o�̓N���X�̃C���X�^���X��
        m_cfUFLogClass = New UFLogClass(cfUFConfigDataClass, cfControlData.m_strBusinessId)
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �Ǔ��ǊO�擾
    '* 
    '* �\��            Public Function GetKannaiKangai(strKannaiKangaiKB As String) As String
    '* 
    '* �@�\�@�@        �敪���Ǔ��ǊO���̂��擾
    '* 
    '* ����            strKannaiKangaiKB As String   :�Ǔ��ǊO�敪
    '* 
    '* �߂�l          �Ǔ��ǊO����
    '************************************************************************************************
    Public Function GetKannaiKangai(ByVal strKannaiKangaiKB As String) As String
        Dim strMeisho As String = String.Empty
        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKannaiKangai")

            Select Case strKannaiKangaiKB
                Case "1"
                    strMeisho = "�Ǔ�"
                Case "2"
                    strMeisho = "�ǊO"
            End Select

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetKannaiKangai")

        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:GetKannaiKangai�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return strMeisho
    End Function

    '************************************************************************************************
    '* ���\�b�h��      �Ǔ��ǊO�ҏW
    '* 
    '* �\��            Public Function HenKannaiKangai() As DataSet
    '* 
    '* �@�\�@�@        �Ǔ��ǊO�̃R�[�h�Ɩ��̂�ҏW����
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          �Ǔ��ǊO���́iDataSet�j
    '*                   �\���FcsKannaiKangaiData    �C���e���Z���X�FABKannaiKangaiData
    '************************************************************************************************
    Public Function HenKannaiKangai() As DataSet
        Dim csKannaiKangaiData As New DataSet()
        Dim csKannaiKangaiDataTbl As DataTable
        Dim csKannaiKangaiDataRow As DataRow

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKannaiKangai")

            '�e�[�u�����쐬����
            csKannaiKangaiDataTbl = csKannaiKangaiData.Tables.Add(ABKannaiKangaiData.TABLE_NAME)

            '�e�[�u���z���ɕK�v�t�B�[���h��p�ӂ���
            csKannaiKangaiDataTbl.Columns.Add(ABKannaiKangaiData.KANNAIKANGAIKB, System.Type.GetType("System.String"))
            csKannaiKangaiDataTbl.Columns.Add(ABKannaiKangaiData.KANNAIKANGAIKBMEI, System.Type.GetType("System.String"))

            '�e�t�B�[���h�Ƀf�[�^���i�[����
            '�Ǔ��ǊO�敪 = 1
            csKannaiKangaiDataRow = csKannaiKangaiDataTbl.NewRow()
            csKannaiKangaiDataRow.Item(ABKannaiKangaiData.KANNAIKANGAIKB) = "1"
            csKannaiKangaiDataRow.Item(ABKannaiKangaiData.KANNAIKANGAIKBMEI) = "�Ǔ�"
            '�f�[�^�̒ǉ�
            csKannaiKangaiData.Tables(ABKannaiKangaiData.TABLE_NAME).Rows.Add(csKannaiKangaiDataRow)

            '�Ǔ��ǊO�敪 = 2
            csKannaiKangaiDataRow = csKannaiKangaiDataTbl.NewRow()
            csKannaiKangaiDataRow.Item(ABKannaiKangaiData.KANNAIKANGAIKB) = "2"
            csKannaiKangaiDataRow.Item(ABKannaiKangaiData.KANNAIKANGAIKBMEI) = "�ǊO"
            '�f�[�^�̒ǉ�
            csKannaiKangaiData.Tables(ABKannaiKangaiData.TABLE_NAME).Rows.Add(csKannaiKangaiDataRow)

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "HenKannaiKangai")
        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:HenKannaiKangai�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csKannaiKangaiData
    End Function

End Class
