'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �Z�����(ABJuminShubetsuBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2002/12/13�@�R��@�q��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2011/03/31   000001     �Z����ʎ擾�Q���\�b�h(GetJuminshubetsu2)�̒ǉ��i��Áj
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

Public Class ABJuminShubetsuBClass

    ' �����o�ϐ��̒�`
    Private m_cfUFLogClass As UFLogClass            '���O�o�̓N���X
    Private m_cfUFControlData As UFControlData      '�R���g���[���f�[�^

    '�p�����[�^�̃����o�ϐ�
    Private m_strHenshuShubetsu As String           '��ʁi�S�p�@Max�W�����j
    Private m_strHenshuShubetsuRyaku As String      '���́i�S�p�@Max�R�����j

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABJuminShubetsuBClass"

    '�e�����o�ϐ��̃v���p�e�B��`
    Public ReadOnly Property p_strHenshuShubetsu() As String
        Get
            Return m_strHenshuShubetsu
        End Get
    End Property
    Public ReadOnly Property p_strHenshuShubetsuRyaku() As String
        Get
            Return m_strHenshuShubetsuRyaku
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
        m_strHenshuShubetsu = String.Empty
        m_strHenshuShubetsuRyaku = String.Empty
    End Sub

    '************************************************************************************************
    '* ���\�b�h��      �Z����ʎ擾
    '* 
    '* �\��            Public Sub GetJuminshubetsu(ByVal strAtenaDataKB As String,
    '*                                             ByVal strAtenaDataSHU As String)
    '* 
    '* �@�\�@�@        �����f�[�^�敪�A�����f�[�^��ʂ�薼�̂�ҏW����
    '* 
    '* ����            strAtenaDataKB As String   :�����f�[�^�敪
    '*                 strAtenaDataSHU As String  :�����f�[�^���
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Overloads Sub GetJuminshubetsu(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String)
        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu")

            Select Case strAtenaDataKB
                Case "20"
                    m_strHenshuShubetsu = "�@�@�l"
                    m_strHenshuShubetsuRyaku = "�@�@�l"
                Case "30"
                    m_strHenshuShubetsu = "���@�L"
                    m_strHenshuShubetsuRyaku = "���@�L"
                Case Else
                    Select Case strAtenaDataSHU
                        Case "10"
                            m_strHenshuShubetsu = "���{�l�E�Z��"
                            m_strHenshuShubetsuRyaku = ""
                        Case "13"
                            m_strHenshuShubetsu = "���{�l�i�Z�o�O�j"
                            m_strHenshuShubetsuRyaku = "�Z�o�O"
                        Case "14"
                            m_strHenshuShubetsu = "���̑��l"
                            m_strHenshuShubetsuRyaku = "���̑�"
                        Case "17"
                            m_strHenshuShubetsu = "���{�l�E������"
                            m_strHenshuShubetsuRyaku = "���@��"
                        Case "18"
                            m_strHenshuShubetsu = "���{�l�E�]�o��"
                            m_strHenshuShubetsuRyaku = "�]�@�o"
                        Case "19"
                            m_strHenshuShubetsu = "���{�l�E���S��"
                            m_strHenshuShubetsuRyaku = "���@�S"
                        Case "20"
                            m_strHenshuShubetsu = "�O���l�F�Z��"
                            m_strHenshuShubetsuRyaku = "�O���l"
                        Case "23"
                            m_strHenshuShubetsu = "�O���l�i�Z�o�O�j"
                            m_strHenshuShubetsuRyaku = "�Z�o�O"
                        Case "27"
                            m_strHenshuShubetsu = "�O���l�F������"
                            m_strHenshuShubetsuRyaku = "���@��"
                        Case "28"
                            m_strHenshuShubetsu = "�O���l�F�]�o��"
                            m_strHenshuShubetsuRyaku = "�]�@�o"
                        Case "29"
                            m_strHenshuShubetsu = "�O���l�F���S��"
                            m_strHenshuShubetsuRyaku = "���@�S"
                        Case Else
                            m_strHenshuShubetsu = "����������������"
                            m_strHenshuShubetsuRyaku = "������"
                    End Select
            End Select

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu")
        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:GetJuminshubetsu�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try
    End Sub

    '*����ԍ� 000001 2011/03/31 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��      �Z����ʎ擾�Q
    '* 
    '* �\��            Public Sub GetJuminshubetsu2(ByVal strAtenaDataKB As String,
    '*                                              ByVal strAtenaDataSHU As String)
    '* 
    '* �@�\�@�@        �����f�[�^�敪�A�����f�[�^��ʂ�薼�̂�ҏW����
    '*                 ��GetJuminshubetsu���\�b�h�ƊO���l�̕\�����@���قȂ�
    '* 
    '* ����            strAtenaDataKB As String   :�����f�[�^�敪
    '*                 strAtenaDataSHU As String  :�����f�[�^���
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Overloads Sub GetJuminshubetsu2(ByVal strAtenaDataKB As String, ByVal strAtenaDataSHU As String)
        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu")

            Select Case strAtenaDataKB
                Case "20"
                    m_strHenshuShubetsu = "�@�@�l"
                    m_strHenshuShubetsuRyaku = "�@�@�l"
                Case "30"
                    m_strHenshuShubetsu = "���@�L"
                    m_strHenshuShubetsuRyaku = "���@�L"
                Case Else
                    Select Case strAtenaDataSHU
                        Case "10"
                            m_strHenshuShubetsu = "�Z��"
                            m_strHenshuShubetsuRyaku = ""
                        Case "13"
                            m_strHenshuShubetsu = "�Z�o�O"
                            m_strHenshuShubetsuRyaku = "�Z�o�O"
                        Case "14"
                            m_strHenshuShubetsu = "���̑��l"
                            m_strHenshuShubetsuRyaku = "���̑�"
                        Case "17"
                            m_strHenshuShubetsu = "�E��������"
                            m_strHenshuShubetsuRyaku = "���@��"
                        Case "18"
                            m_strHenshuShubetsu = "�]�o��"
                            m_strHenshuShubetsuRyaku = "�]�@�o"
                        Case "19"
                            m_strHenshuShubetsu = "���S��"
                            m_strHenshuShubetsuRyaku = "���@�S"
                        Case "20"
                            m_strHenshuShubetsu = "�O���l�Z��"
                            m_strHenshuShubetsuRyaku = "�O���l"
                        Case "23"
                            m_strHenshuShubetsu = "�O���l�Z�o�O"
                            m_strHenshuShubetsuRyaku = "�Z�o�O(�O���l)"
                        Case "27"
                            m_strHenshuShubetsu = "�O���l�E��������"
                            m_strHenshuShubetsuRyaku = "����(�O���l)"
                        Case "28"
                            m_strHenshuShubetsu = "�O���l�]�o��"
                            m_strHenshuShubetsuRyaku = "�]�o(�O���l)"
                        Case "29"
                            m_strHenshuShubetsu = "�O���l���S��"
                            m_strHenshuShubetsuRyaku = "���S(�O���l)"
                        Case Else
                            m_strHenshuShubetsu = "����������������"
                            m_strHenshuShubetsuRyaku = "������"
                    End Select
            End Select

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu")
        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:GetJuminshubetsu�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try
    End Sub
    '*����ԍ� 000001 2011/03/31 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��      �Z����ʕҏW
    '* 
    '* �\��            Public Function GetJuminshubetsu() As DataSet
    '* 
    '* �@�\�@�@        �����f�[�^��ʂ̃R�[�h�Ɩ��̂�ҏW����
    '* 
    '* ����            �Ȃ�
    '* 
    '* �߂�l          ��ʃf�[�^�iDataSet�j
    '*                   �\���FcsShubetsuData    �C���e���Z���X�FABShubetsuData
    '************************************************************************************************
    Public Overloads Function GetJuminshubetsu() As DataSet
        Dim csShubetsuData As New DataSet()
        Dim csShubetsuDataTbl As DataTable
        Dim csShubetsuDataRow As DataRow

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfUFLogClass.DebugStartWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu")

            '�e�[�u�����쐬����
            csShubetsuDataTbl = csShubetsuData.Tables.Add(ABShubetsuData.TABLE_NAME)

            '�e�[�u���z���ɕK�v�t�B�[���h��p�ӂ���
            csShubetsuDataTbl.Columns.Add(ABShubetsuData.ATENADATASHU, System.Type.GetType("System.String"))
            csShubetsuDataTbl.Columns.Add(ABShubetsuData.HENSHUSHUBETSU, System.Type.GetType("System.String"))
            csShubetsuDataTbl.Columns.Add(ABShubetsuData.HENSHUSHUBETSURYAKU, System.Type.GetType("System.String"))

            '�e�t�B�[���h�Ƀf�[�^���i�[����
            '�����f�[�^��� = 10
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "10"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "���{�l�E�Z��"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = ""
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 13
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "13"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "���{�l�i�Z�o�O�j"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "�Z�o�O"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 14
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "14"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "���̑��l"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "���̑�"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 17
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "17"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "���{�l�E������"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "���@��"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 18
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "18"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "���{�l�E�]�o��"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "�]�@�o"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 19
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "19"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "���{�l�E���S��"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "���@�S"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 20
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "20"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "�O���l�F�Z��"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "�O���l"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 23
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "23"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "�O���l�i�Z�o�O�j"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "�Z�o�O"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 27
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "27"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "�O���l�F������"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "���@��"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 28
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "28"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "�O���l�F�]�o��"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "�]�@�o"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�����f�[�^��� = 29
            csShubetsuDataRow = csShubetsuDataTbl.NewRow()
            csShubetsuDataRow.Item(ABShubetsuData.ATENADATASHU) = "29"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSU) = "�O���l�F���S��"
            csShubetsuDataRow.Item(ABShubetsuData.HENSHUSHUBETSURYAKU) = "���@�S"
            '�f�[�^�̒ǉ�
            csShubetsuData.Tables(ABShubetsuData.TABLE_NAME).Rows.Add(csShubetsuDataRow)

            '�f�o�b�O�I�����O�o��
            m_cfUFLogClass.DebugEndWrite(m_cfUFControlData, THIS_CLASS_NAME, "GetJuminshubetsu")
        Catch objExp As Exception
            '�G���[���O�o��
            m_cfUFLogClass.ErrorWrite(m_cfUFControlData, "�y�N���X��:" + THIS_CLASS_NAME + "�z�y���\�b�h��:GetJuminshubetsu�z�y�G���[���e:" + objExp.Message + "�z")
            '�G���[�����̂܂܃X���[����
            Throw objExp
        End Try

        Return csShubetsuData
    End Function

End Class
