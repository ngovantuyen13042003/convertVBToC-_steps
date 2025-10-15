'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �`�a�����ݐσ}�X�^�c�`
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/01/15�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2003/03/10 000001     �Z���b�c���̐������`�F�b�N�Ɍ��
'* 2003/03/31 000002     �������`�F�b�N��Trim�����l�Ń`�F�b�N����
'* 2003/04/16 000003     ���a��N�����̓��t�`�F�b�N�𐔒l�`�F�b�N�ɕύX
'*                       �����p�J�i�̔��p�J�i�`�F�b�N���`�m�j�`�F�b�N�ɕύX
'* 2003/05/20 000004     �G���[�A���t�N���X�̲ݽ�ݽ��ݽ�׸��ɕύX
'* 2003/08/28 000005     RDB�A�N�Z�X���O�̏C��
'* 2003/09/11 000006     �[���h�c�������`�F�b�N��ANK�ɂ���
'* 2003/10/09 000007     �쐬���[�U�[�E�X�V���[�U�[�`�F�b�N�̕ύX
'* 2003/10/30 000008     �d�l�ύX�A�J�^�J�i�`�F�b�N��ANK�`�F�b�N�ɕύX
'* 2003/11/18 000009     �d�l�ύX�F���ڒǉ�
'* 2003/12/01 000010     �d�l�ύX�F���ږ��̕ύX(SYORINICHIJI->SHORINICHIJI)
'*                       �d�l�ύX�F���ږ��̕ύX(KOKUHOTIAHKHONHIKBMEISHO->KOKUHOTISHKHONHIKBMEISHO)
'* 2004/03/06 000011     �d�l�ύX�F���ەی��ؔԍ��̃`�F�b�N�Ȃ��ɕύX
'* 2004/08/13 000012     �d�l�ύX�A�n��R�[�h�`�F�b�N��ANK�`�F�b�N�ɕύX
'* 2004/11/12 000013     �f�[�^�`�F�b�N���s�Ȃ�Ȃ�
'* 2005/12/26 000014     �d�l�ύX�F�s����b�c��ANK�`�F�b�N�ɕύX(�}���S���R)
'* 2010/04/16 000015     VS2008�Ή��i��Áj
'* 2011/10/24 000016     �yAB17010�z���Z��@�����Ή��������ݐϕt���}�X�^�ǉ�   (����)
'* 2023/08/14 000017    �yAB-0820-1�z�Z�o�O�Ǘ����ڒǉ�(����)
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
'* �����ݐσ}�X�^�擾���Ɏg�p����p�����[�^�N���X
'*
'************************************************************************************************
Public Class ABAtenaRuisekiBClass
#Region "�����o�ϐ�"
    '�p�����[�^�̃����o�ϐ�
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_strInsertSQL As String                        ' INSERT�pSQL
    Private m_cfSelectUFParameterCollectionClass As UFParameterCollectionClass      'SELECT�p�p�����[�^�R���N�V����
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass      'INSERT�p�p�����[�^�R���N�V����
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X

    '*����ԍ� 000016 2011/10/24 �ǉ��J�n
    Private m_csSekoYMDHanteiB As ABSekoYMDHanteiBClass             '�{�s������B�׽
    Private m_csAtenaRuisekiFZYB As ABAtenaRuisekiFZYBClass         '�����ݐϕt���}�X�^B�׽
    Private m_blnJukihoKaiseiFG As Boolean = False
    Private m_strJukihoKaiseiKB As String                           '�Z��@�����敪
    '*����ԍ� 000016 2011/10/24 �ǉ��I��

    '*����ԍ� 000017 2023/08/14 �ǉ��J�n
    Private m_csAtenaRuisekiHyojunB As ABAtenaRuiseki_HyojunBClass            '�����ݐ�_�W���}�X�^B�׽
    Private m_csAtenaRuisekiFZYHyojunB As ABAtenaRuisekiFZY_HyojunBClass      '�����ݐϕt��_�W���}�X�^B�׽
    '*����ԍ� 000017 2023/08/14 �ǉ��I��

    '�@�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABAtenaRuisekiBClass"                ' �N���X��
    Private Const THIS_BUSINESSID As String = "AB"                                  ' �Ɩ��R�[�h

    Private Const JUKIHOKAISEIKB_ON As String = "1"

#End Region

#Region "�v���p�e�B"
    '*����ԍ� 000016 2011/10/24 �ǉ��J�n
    Public WriteOnly Property p_strJukihoKaiseiKB() As String      ' �Z��@�����敪
        Set(ByVal Value As String)
            m_strJukihoKaiseiKB = Value
        End Set
    End Property
    '*����ԍ� 000016 2011/10/24 �ǉ��I��
#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfUFControlData As UFControlData, 
    '*                               ByVal cfUFConfigDataClass As UFConfigDataClass, 
    '*                               ByVal cfUFRdbClass As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfUFControlData As UFControlData          : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfUFConfigDataClass as UFConfigDataClass  : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                cfUFRdbClass as UFRdbClass                : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
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

        ' �p�����[�^�̃����o�ϐ�
        m_strInsertSQL = String.Empty
        m_cfSelectUFParameterCollectionClass = Nothing
        m_cfInsertUFParameterCollectionClass = Nothing

        '*����ԍ� 000016 2011/10/24 �ǉ��J�n
        m_strJukihoKaiseiKB = String.Empty

        '�Z��@�����׸ގ擾
        Call GetJukihoKaiseiFG()
        '*����ԍ� 000016 2011/10/24 �ǉ��I��
    End Sub
#End Region

#Region "���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��     �����ݐσ}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
    '*                                                          ByVal strYusenKB As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD    : �Z���R�[�h
    '*                strYusenKB    : �D��敪
    '* 
    '* �߂�l         DataSet : �擾�������������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
                                              ByVal strYusenKB As String) As DataSet
        Return Me.GetAtenaRuiseki(strJuminCD, "", "", strYusenKB)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����ݐσ}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetAtenaRuiseki(ByVal strKaishiNichiji As String, _
    '*                                                          ByVal strSyuryoNichiji As String, _
    '*                                                          ByVal strYusenKB As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strKaishiNichiji  : �J�n����
    '*                strSyuryoNichiji  : �I������
    '*                strYusenKB        : �D��敪
    '* 
    '* �߂�l         DataSet : �擾�������������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaRuiseki(ByVal strKaishiNichiji As String, _
                                              ByVal strSyuryoNichiji As String, _
                                              ByVal strYusenKB As String) As DataSet
        Return Me.GetAtenaRuiseki("", strKaishiNichiji, strSyuryoNichiji, strYusenKB)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     �����ݐσ}�X�^���o
    '* 
    '* �\��           Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
    '*                                                          ByVal strKaishiNichiji As String, _
    '*                                                          ByVal strSyuryoNichiji As String, _
    '*                                                          ByVal strYusenKB As String) As DataSet
    '* 
    '* �@�\�@�@    �@�@�Z�o�O�}�X�^���Y���f�[�^���擾����
    '* 
    '* ����           strJuminCD        : �Z���R�[�h
    '*                strKaishiNichiji  : �J�n����
    '*                strSyuryoNichiji  : �I������
    '*                strYusenKB        : �D��敪
    '* 
    '* �߂�l         DataSet : �擾�������������}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetAtenaRuiseki(ByVal strJuminCD As String, _
                                              ByVal strKaishiNichiji As String, _
                                              ByVal strSyuryoNichiji As String, _
                                              ByVal strYusenKB As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetAtenaRuiseki"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����
        Dim cfUFParameterClass As UFParameterClass          '�p�����[�^�N���X
        Dim csAtenaRuisekiEntity As DataSet                 '�����ݐ�DataSet
        Dim strKaishiNichiji2 As String                     '�J�n����
        Dim strSyuryoNichiji2 As String                     '�I������
        Dim strSQL As StringBuilder
        Dim strWHERE As StringBuilder
        Dim csDataSchema As DataSet

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfSelectUFParameterCollectionClass = New UFParameterCollectionClass()

            ' �p�����[�^�`�F�b�N
            ' �J�n�����`�F�b�N
            If strKaishiNichiji.RLength = 17 Then
                strKaishiNichiji2 = strKaishiNichiji

            ElseIf strKaishiNichiji.RLength = 8 Then
                strKaishiNichiji2 = strKaishiNichiji + "000000000"

            ElseIf (strKaishiNichiji = String.Empty) And (strSyuryoNichiji = String.Empty) Then
                strKaishiNichiji2 = String.Empty
            Else
                '�G���[��`���擾
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_KAISHINICHIJI)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            '�I�������`�F�b�N
            If strSyuryoNichiji.RLength = 17 Then
                strSyuryoNichiji2 = strSyuryoNichiji

            ElseIf strSyuryoNichiji.RLength = 8 Then
                strSyuryoNichiji2 = strSyuryoNichiji + "000000000"

            ElseIf strSyuryoNichiji = String.Empty Then
                strSyuryoNichiji2 = String.Empty
            Else
                '�G���[��`���擾
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_SYURYONICHIJI)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If

            '�D��敪
            If Not (strYusenKB = "1" Or strYusenKB = "2") Then
                '�G���[��`���擾
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARIREKIB_PARA_YUSENKB)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
            End If


            ' SQL���̍쐬
            strSQL = New StringBuilder()
            '*����ԍ� 000016 2011/10/24 �C���J�n
            'strSQL.Append("SELECT * FROM ")
            'strSQL.Append(ABAtenaRuisekiEntity.TABLE_NAME)
            '�Z��@�����ȍ~�͈����ݐϕt���}�X�^��t��
            If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                strSQL.AppendFormat("SELECT {0}.*", ABAtenaRuisekiEntity.TABLE_NAME)
                Call SetFZYEntity(strSQL)
                strSQL.AppendFormat(" FROM {0}", ABAtenaRuisekiEntity.TABLE_NAME)
                Call SetFZYJoin(strSQL)
            Else
                strSQL.Append("SELECT * FROM ")
                strSQL.Append(ABAtenaRuisekiEntity.TABLE_NAME)
            End If
            '*����ԍ� 000016 2011/10/24 �C���I��

            '*����ԍ� 000016 2011/10/24 �ǉ��J�n
            csDataSchema = m_cfRdbClass.GetTableSchemaNoRestriction(strSQL.ToString(), ABAtenaRuisekiEntity.TABLE_NAME, False)
            '*����ԍ� 000016 2011/10/24 �ǉ��I��


            strSQL.Append(" WHERE ")

            'WHERE��̍쐬
            strWHERE = New StringBuilder()
            '�Z���R�[�h
            If Not (strJuminCD = String.Empty) Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                '*����ԍ� 000016 2011/10/24 �ǉ��J�n
                '�Z��@�����ȍ~�͈����ݐϕt���}�X�^��t��
                If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                    strWHERE.AppendFormat("{0}.", ABAtenaRuisekiEntity.TABLE_NAME)
                Else
                    '�����Ȃ�
                End If
                '*����ԍ� 000016 2011/10/24 �ǉ��I��
                strWHERE.Append(ABAtenaRuisekiEntity.JUMINCD)
                strWHERE.Append(" = ")
                strWHERE.Append(ABAtenaRuisekiEntity.KEY_JUMINCD)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.KEY_JUMINCD
                cfUFParameterClass.Value = strJuminCD
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '�J�n����
            If Not (strKaishiNichiji2 = String.Empty) Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                '*����ԍ� 000016 2011/10/24 �ǉ��J�n
                '�Z��@�����ȍ~�͈����ݐϕt���}�X�^��t��
                If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                    strWHERE.AppendFormat("{0}.", ABAtenaRuisekiEntity.TABLE_NAME)
                Else
                    '�����Ȃ�
                End If
                '*����ԍ� 000016 2011/10/24 �ǉ��I��
                '*����ԍ� 000010 2003/12/01 �C���J�n
                'strWHERE.Append(ABAtenaRuisekiEntity.SYORINICHIJI)
                strWHERE.Append(ABAtenaRuisekiEntity.SHORINICHIJI)
                '*����ԍ� 000010 2003/12/01 �C���I��
                strWHERE.Append(" >= ")
                strWHERE.Append(ABAtenaRuisekiEntity.KEY_SYORINICHIJI)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.KEY_SYORINICHIJI
                cfUFParameterClass.Value = strKaishiNichiji2
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '�I������
            If Not (strSyuryoNichiji2 = String.Empty) Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                '*����ԍ� 000016 2011/10/24 �ǉ��J�n
                '�Z��@�����ȍ~�͈����ݐϕt���}�X�^��t��
                If (m_strJukihoKaiseiKB = JUKIHOKAISEIKB_ON) Then
                    strWHERE.AppendFormat("{0}.", ABAtenaRuisekiEntity.TABLE_NAME)
                Else
                    '�����Ȃ�
                End If
                '*����ԍ� 000016 2011/10/24 �ǉ��I��
                '*����ԍ� 000010 2003/12/01 �C���J�n
                'strWHERE.Append(ABAtenaRuisekiEntity.SYORINICHIJI)
                strWHERE.Append(ABAtenaRuisekiEntity.SHORINICHIJI)
                '*����ԍ� 000010 2003/12/01 �C���I��
                strWHERE.Append(" <= ")
                strWHERE.Append(ABAtenaRuisekiEntity.PARAM_SYORINICHIJI)
                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass()
                cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.PARAM_SYORINICHIJI
                cfUFParameterClass.Value = strSyuryoNichiji2
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                m_cfSelectUFParameterCollectionClass.Add(cfUFParameterClass)
            End If
            '�D��敪
            If (strYusenKB = "1") Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                strWHERE.Append(ABAtenaRuisekiEntity.JUTOGAIYUSENKB)
                strWHERE.Append(" = '1'")
            End If
            If (strYusenKB = "2") Then
                If Not (strWHERE.RLength = 0) Then
                    strWHERE.Append(" AND ")
                End If
                strWHERE.Append(ABAtenaRuisekiEntity.JUMINYUSENIKB)
                strWHERE.Append(" = '1'")
            End If


            'ORDER�������
            If strWHERE.RLength <> 0 Then
                strSQL.Append(strWHERE)
            End If


            '*����ԍ� 000005 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:GetDataSet�z" + _
            '                        "�ySQL���e:" + strSQL.ToString + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:GetDataSet�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, m_cfSelectUFParameterCollectionClass) + "�z")
            '*����ԍ� 000005 2003/08/28 �C���I��

            ' SQL�̎��s DataSet�̎擾
            '*����ԍ� 000016 2011/10/24 �C���J�n
            'csAtenaRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, ABAtenaRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass)
            csAtenaRuisekiEntity = csDataSchema.Clone()
            csAtenaRuisekiEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csAtenaRuisekiEntity, ABAtenaRuisekiEntity.TABLE_NAME, m_cfSelectUFParameterCollectionClass, False)
            '*����ԍ� 000016 2011/10/24 �C���I��


            ' �f�o�b�O�I�����O�o��
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

        Return csAtenaRuisekiEntity

    End Function


    '************************************************************************************************
    '* ���\�b�h��     ���������}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaRB(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@�@���������}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csDataRow As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaRB(ByVal csDataRow As DataRow) As Integer

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"
        Dim cfParam As UFParameterClass
        '* corresponds to VS2008 Start 2010/04/16 000015
        'Dim csInstRow As DataRow
        'Dim csDataColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000015
        Dim intInsCnt As Integer                            ' �ǉ�����
        Dim strUpdateDateTime As String

        Try

            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If (m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty Or _
                m_cfInsertUFParameterCollectionClass Is Nothing) Then
                Call CreateSQL(csDataRow)
            End If

            '�X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate().ToString("yyyyMMddHHmmssfff")  '�쐬����

            '���ʍ��ڂ̕ҏW���s��
            csDataRow(ABAtenaRuisekiEntity.TANMATSUID) = m_cfControlData.m_strClientId  ' �[���h�c
            csDataRow(ABAtenaRuisekiEntity.SAKUJOFG) = "0"                              ' �폜�t���O
            csDataRow(ABAtenaRuisekiEntity.KOSHINCOUNTER) = Decimal.Zero                ' �X�V�J�E���^
            csDataRow(ABAtenaRuisekiEntity.SAKUSEINICHIJI) = strUpdateDateTime          ' �쐬����
            csDataRow(ABAtenaRuisekiEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId   ' �쐬���[�U�[
            csDataRow(ABAtenaRuisekiEntity.KOSHINNICHIJI) = strUpdateDateTime           ' �X�V����
            csDataRow(ABAtenaRuisekiEntity.KOSHINUSER) = m_cfControlData.m_strUserId    ' �X�V���[�U�[

            '*����ԍ� 000013 2004/11/12 �C���J�n
            '���N���X�̃f�[�^�������`�F�b�N���s��
            'For Each csDataColumn In csDataRow.Table.Columns
            '    CheckColumnValue(csDataColumn.ColumnName, csDataRow(csDataColumn.ColumnName).ToString().Trim)
            'Next csDataColumn
            '*����ԍ� 000016 2004/11/12 �C���I��

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                cfParam.Value = csDataRow(cfParam.ParameterName.RSubstring(ABAtenaRuisekiEntity.PARAM_PLACEHOLDER.RLength)).ToString()
            Next cfParam

            '*����ԍ� 000005 2003/08/28 �C���J�n
            '' RDB�A�N�Z�X���O�o��
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
            '                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
            '                        "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                        "�ySQL���e:" + m_strInsertSQL + "�z")

            '' RDB�A�N�Z�X���O�o�́i2024/04/18 DB�A�N�Z�X���x���P�̂��߃R�����g�A�E�g�j
            'm_cfLogClass.RdbWrite(m_cfControlData, _
            '                            "�y�N���X��:" + Me.GetType.Name + "�z" + _
            '                            "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
            '                            "�y���s���\�b�h��:ExecuteSQL�z" + _
            '                            "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strInsertSQL, m_cfInsertUFParameterCollectionClass) + "�z")
            '*����ԍ� 000005 2003/08/28 �C���I��

            ' SQL�̎��s
            intInsCnt = m_cfRdbClass.ExecuteSQL(m_strInsertSQL, m_cfInsertUFParameterCollectionClass)

            ' �f�o�b�O�I�����O�o��
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
    '*����ԍ� 000016 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����ݐσ}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaB() As Integer
    '* 
    '* �@�\�@�@    �@ �����ݐσ}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csAtenaDr As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����ݐρj
    '* �@�@           csAtenaFZYDr As DataRow : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����ݐϕt���j
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaFZYDr As DataRow) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����ݐσ}�X�^�ǉ������s
            intCnt = Me.InsertAtenaRB(csAtenaDr)

            '�Z��@�����ȍ~�̂Ƃ�
            If (Not IsNothing(csAtenaFZYDr)) AndAlso (m_blnJukihoKaiseiFG) Then
                '�����ݐϕt���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRuisekiFZYB)) Then
                    m_csAtenaRuisekiFZYB = New ABAtenaRuisekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�쐬�����A�X�V�����̓���
                csAtenaFZYDr(ABAtenaRuisekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI)
                csAtenaFZYDr(ABAtenaRuisekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI)

                '�����ݐϕt���}�X�^�ǉ������s
                intCnt2 = m_csAtenaRuisekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr)
            Else
                '�����Ȃ�
            End If

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

        Return intCnt

    End Function

    '*����ԍ� 000017 2023/08/14 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����ݐσ}�X�^�ǉ�
    '* 
    '* �\��           Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
    '*                                              ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
    '* 
    '* �@�\�@�@    �@ �����ݐσ}�X�^�Ƀf�[�^��ǉ�����
    '* 
    '* ����           csAtenaDr As DataRow           : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����ݐρj
    '*                csAtenaHyojunDr As DataRow     : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����ݐ�_�W���j
    '* �@�@           csAtenaFZYDr As DataRow        : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����ݐϕt���j
    '*                csAtenaFZYHyojunDr As DataRow  : �ǉ�����f�[�^�̊܂܂��DataRow�I�u�W�F�N�g�i�����ݐϕt��_�W���j
    '* 
    '* �߂�l         Integer : �ǉ������f�[�^�̌���
    '************************************************************************************************
    Public Function InsertAtenaRB(ByVal csAtenaDr As DataRow, ByVal csAtenaHyojunDr As DataRow, _
                                  ByVal csAtenaFZYDr As DataRow, ByVal csAtenaFZYHyojunDr As DataRow) As Integer
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0
        Dim intCnt3 As Integer = 0
        Dim intCnt4 As Integer = 0

        Const THIS_METHOD_NAME As String = "InsertAtenaRB"

        Try

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '�����ݐσ}�X�^�ǉ������s
            intCnt = Me.InsertAtenaRB(csAtenaDr)

            If (Not IsNothing(csAtenaHyojunDr)) Then

                '�����ݐ�_�W���}�X�^B�׽�̲ݽ�ݽ��
                If (IsNothing(m_csAtenaRuisekiHyojunB)) Then
                    m_csAtenaRuisekiHyojunB = New ABAtenaRuiseki_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                Else
                    '�����Ȃ�
                End If

                '�����ݐϕW���̍쐬�����ƍX�V�����Ɉ����ݐ�Row�̍쐬�����ƍX�V�������Z�b�g����
                csAtenaHyojunDr(ABAtenaRuisekiHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI)
                csAtenaHyojunDr(ABAtenaRuisekiHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI)

                '�����ݐ�_�W���}�X�^�ǉ������s
                intCnt2 = m_csAtenaRuisekiHyojunB.InsertAtenaRuisekiHyojunB(csAtenaHyojunDr)

            End If
            '�Z��@�����ȍ~�̂Ƃ�
            If (m_blnJukihoKaiseiFG) Then

                '�����ݐϕt��Row�����݂���ꍇ
                If (csAtenaFZYDr IsNot Nothing) Then

                    '�����ݐϕt���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRuisekiFZYB)) Then
                        m_csAtenaRuisekiFZYB = New ABAtenaRuisekiFZYBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�쐬�����A�X�V�����̓���
                    csAtenaFZYDr(ABAtenaRuisekiFZYEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI)
                    csAtenaFZYDr(ABAtenaRuisekiFZYEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI)

                    '�����ݐϕt���}�X�^�ǉ������s
                    intCnt3 = m_csAtenaRuisekiFZYB.InsertAtenaFZYRB(csAtenaFZYDr)

                End If

                '�����ݐϕt��_�W��Row�����݂���ꍇ
                If (csAtenaFZYHyojunDr IsNot Nothing) Then

                    '�����ݐϕt��_�W���}�X�^B�׽�̲ݽ�ݽ��
                    If (IsNothing(m_csAtenaRuisekiFZYHyojunB)) Then
                        m_csAtenaRuisekiFZYHyojunB = New ABAtenaRuisekiFZY_HyojunBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                    Else
                        '�����Ȃ�
                    End If

                    '�쐬�����A�X�V�����̓���
                    csAtenaFZYHyojunDr(ABAtenaRuisekiFZYHyojunEntity.SAKUSEINICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.SAKUSEINICHIJI)
                    csAtenaFZYHyojunDr(ABAtenaRuisekiFZYHyojunEntity.KOSHINNICHIJI) = csAtenaDr(ABAtenaRuisekiEntity.KOSHINNICHIJI)

                    '�����ݐϕt��_�W���}�X�^�ǉ������s
                    intCnt4 = m_csAtenaRuisekiFZYHyojunB.InsertAtenaRuisekiFZYHyojunB(csAtenaFZYHyojunDr)

                End If

            Else
                '�����Ȃ�
            End If

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

        Return intCnt

    End Function
    '*����ԍ� 000017 2023/08/14 �ǉ��I��

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
        Dim cfUFParameterClass As UFParameterClass
        Dim csInsertColumn As StringBuilder                 'INSERT�p�J������`
        Dim csInsertParam As StringBuilder                  'INSERT�p�p�����[�^��`


        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' INSERT SQL���̍쐬
            m_strInsertSQL = "INSERT INTO " + ABAtenaRuisekiEntity.TABLE_NAME + " "
            csInsertColumn = New StringBuilder()
            csInsertParam = New StringBuilder()

            ' INSERT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass()



            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass()

                ' INSERT SQL���̍쐬
                csInsertColumn.Append(csDataColumn.ColumnName)
                csInsertColumn.Append(", ")

                csInsertParam.Append(ABAtenaRuisekiEntity.PARAM_PLACEHOLDER)
                csInsertParam.Append(csDataColumn.ColumnName)
                csInsertParam.Append(", ")

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABAtenaRuisekiEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)


            Next csDataColumn

            ' �Ō�̃J���}����菜����INSERT�����쐬
            m_strInsertSQL += "(" + csInsertColumn.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")" _
                    + " VALUES (" + csInsertParam.ToString.TrimEnd.TrimEnd(",".ToCharArray()) + ")"

            ' �f�o�b�O�I�����O�o��
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

    '*����ԍ� 000016 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��     �����t���f�[�^���ڕҏW
    '* 
    '* �\��           Private SetFZYEntity()
    '* 
    '* �@�\           �����t���f�[�^�̍��ڕҏW�����܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYEntity(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TABLEINSERTKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.LINKNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUMINHYOJOTAIKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKYOCHITODOKEFLG)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.HONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANAHONGOKUMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANJIHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANJITSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KATAKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.UMAREFUSHOKBN)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TSUSHOMEITOUROKUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUKIKANCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUKIKANMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUSHACD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUSHAMEISHO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZAIRYUCARDNO)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KOFUYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KOFUYOTEISTYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.KOFUYOTEIEDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOIDOYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYUCD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOJIYU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDYMD)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUKITAISHOSHASHOJOTDKDTUCIKB)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.FRNSTAINUSMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.FRNSTAINUSKANAMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSKANAHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.STAINUSKANATSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENUMAEJ_STAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUYOTEISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSMEI_KYOTSU)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSHEIKIMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.TENSHUTSUKKTISTAINUSTSUSHOMEI)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE1)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE2)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE3)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE4)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE5)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE6)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE7)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE8)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE9)
        strAtenaSQLsb.AppendFormat(", {0}.{1}", ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RESERVE10)

    End Sub
   '************************************************************************************************
    '* ���\�b�h��     �����t���e�[�u��JOIN��쐬
    '* 
    '* �\��           Private SetFZYJoin()
    '* 
    '* �@�\           �����t���e�[�u����JOIN����쐬���܂��B
    '* 
    '* ����           strAtenaSQLsb�@�F�@�����擾�pSQL  
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub SetFZYJoin(ByRef strAtenaSQLsb As StringBuilder)
        strAtenaSQLsb.AppendFormat(" LEFT OUTER JOIN {0} ", ABAtenaRuisekiFZYEntity.TABLE_NAME)
        strAtenaSQLsb.AppendFormat(" ON {0}.{1} = {2}.{3} ", _
                                    ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.JUMINCD, _
                                    ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.JUMINCD)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", _
                                    ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.RIREKINO, _
                                    ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.RIREKINO)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", _
                                    ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.SHORINICHIJI, _
                                    ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.SHORINICHIJI)
        strAtenaSQLsb.AppendFormat(" AND {0}.{1} = {2}.{3} ", _
                                    ABAtenaRuisekiEntity.TABLE_NAME, ABAtenaRuisekiEntity.ZENGOKB, _
                                    ABAtenaRuisekiFZYEntity.TABLE_NAME, ABAtenaRuisekiFZYEntity.ZENGOKB)
    End Sub
    '*����ԍ� 000016 2011/10/24 �ǉ��I��

    '************************************************************************************************
    '* ���\�b�h��     �f�[�^�������`�F�b�N
    '* 
    '* �\��           Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue as String)
    '* 
    '* �@�\           �X�V�f�[�^�̐��������`�F�b�N����B
    '* 
    '* ����           strColumnName As String : ���������}�X�^�f�[�^�Z�b�g�̍��ږ�
    '*                strValue As String     : ���ڂɑΉ�����l
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CheckColumnValue(ByVal strColumnName As String, ByVal strValue As String)

        Const THIS_METHOD_NAME As String = "CheckColumnValue"
        Const TABLENAME As String = "�����ݐρD"
        Dim objErrorStruct As UFErrorStruct                 '�G���[��`�\����

        Try
            ' �f�o�b�O�J�n���O�o��
            'm_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME, strColumnName + "'" + strValue + "'")

            ' ���t�N���X�̃C���X�^���X��
            If (IsNothing(m_cfDateClass)) Then
                m_cfDateClass = New UFDateClass(m_cfConfigDataClass)
                ' ���t�N���X�̕K�v�Ȑݒ���s��
                m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            End If

            Select Case strColumnName.ToUpper()

                Case ABAtenaRuisekiEntity.JUMINCD            '�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHICHOSONCD        '�s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KYUSHICHOSONCD     '���s�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KYUSHICHOSONCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.RIREKINO           '����ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RIREKINO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                    '*����ԍ� 000010 2003/12/01 �C���J�n
                    'Case ABAtenaRuisekiEntity.SYORINICHIJI      '��������
                Case ABAtenaRuisekiEntity.SHORINICHIJI      '��������
                    '*����ԍ� 000010 2003/12/01 �C���I��
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SYORINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If
                Case ABAtenaRuisekiEntity.ZENGOKB           '�O��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZENGOKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.RRKST_YMD          '�����J�n�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RRKST_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.RRKED_YMD          '�����I���N����
                    If Not (strValue = String.Empty Or strValue = "00000000" Or strValue = "99999999") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RRKED_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUMINJUTOGAIKB     '�Z���Z�o�O�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINJUTOGAIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUMINYUSENIKB      '�Z���D��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINYUSENIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUTOGAIYUSENKB     '�Z�o�O�D��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTOGAIYUSENKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ATENADATAKB        '�����f�[�^�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ATENADATAKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.STAICD             '���уR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_STAICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUMINHYOCD         '�Z���[�R�[�h
                    '�`�F�b�N�Ȃ�

                Case ABAtenaRuisekiEntity.SEIRINO            '�����ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEIRINO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ATENADATASHU       '�����f�[�^���
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ATENADATASHU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HANYOKB1           '�ėp�敪1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HANYOKB1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KJNHJNKB           '�l�@�l�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KJNHJNKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HANYOKB2           '�ėp�敪2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HANYOKB2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANNAIKANGAIKB     '�Ǔ��ǊO�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANNAIKANGAIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANAMEISHO1        '�J�i����1
                    '*����ԍ� 000008 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000008 2003/10/30 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANAMEISHO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIMEISHO1       '��������1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIMEISHO1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANAMEISHO2        '�J�i����2
                    '*����ԍ� 000008 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000008 2003/10/30 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANAMEISHO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIMEISHO2       '��������2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIMEISHO2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIHJNKEITAI     '�����@�l�`��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIHJNKEITAI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIHJNDAIHYOSHSHIMEI   '�����@�l��\�Ҏ���
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIHJNDAIHYOSHSHIMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEARCHKANJIMEISHO  '�����p��������
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEARCHKANJIMEISHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KYUSEI             '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KYUSEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEARCHKANASEIMEI   '�����p�J�i����
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i����", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEARCHKANASEI      '�����p�J�i��
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i��", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEARCHKANAMEI      '�����p�J�i��
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾(�p�����E���p�J�i���ړ��͂̌��ł��B�F)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "�����p�J�i��", objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIRRKNO          '�Z���ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIRRKNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                    'Case ABAtenaRuisekiEntity.UMAREYMD           '���N����
                    '    If Not (strValue = String.Empty Or strValue = "00000000") Then
                    '        m_cfDateClass.p_strDateValue = strValue
                    '        If (Not m_cfDateClass.CheckDate()) Then
                    '            '�G���[��`���擾
                    '            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_UMAREYMD)
                    '            '��O�𐶐�
                    '            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    '        End If
                    '    End If

                    'Case ABAtenaRuisekiEntity.UMAREWMD           '���a��N����
                    '    If (Not UFStringClass.CheckNumber(strValue)) Then
                    '        '�G���[��`���擾(�������ړ��͂̌��ł��B�F)
                    '        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                    '        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                    '        '��O�𐶐�
                    '        Throw New UFAppException(objErrorStruct.m_strErrorMessage + TABLENAME + "���a��N����", objErrorStruct.m_strErrorCode)
                    '    End If

                Case ABAtenaRuisekiEntity.SEIBETSUCD         '���ʃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEIBETSUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEIBETSU           '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEIBETSU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SEKINO             '�Дԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SEKINO)
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUMINHYOHYOJIJUN   '�Z���[�\����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUMINHYOHYOJIJUN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZOKUGARACD         '�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimEnd)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZOKUGARACD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZOKUGARA           '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZOKUGARA)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2JUMINHYOHYOJIJUN     '��Q�Z���[�\����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2JUMINHYOHYOJIJUN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2ZOKUGARACD           '��Q�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimEnd)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2ZOKUGARACD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2ZOKUGARA             '��Q����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2ZOKUGARA)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.STAINUSJUMINCD     '���ю�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_STAINUSJUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.STAINUSMEI         '���ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANASTAINUSMEI     '�J�i���ю喼
                    '*����ԍ� 000008 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000008 2003/10/30 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANASTAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2STAINUSJUMINCD       '��Q���ю�Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2STAINUSJUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.DAI2STAINUSMEI           '��Q���ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_DAI2STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANADAI2STAINUSMEI       '��Q�J�i���ю喼
                    '*����ԍ� 000008 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000008 2003/10/30 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANADAI2STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.YUBINNO            '�X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_YUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUSHOCD            '�Z���R�[�h
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUSHO              '�Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BANCHICD1          '�Ԓn�R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHICD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BANCHICD2          '�Ԓn�R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHICD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BANCHICD3          '�Ԓn�R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHICD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BANCHI             '�Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KATAGAKIFG         '�����t���O
                    If (Not strValue.Trim = String.Empty) Then
                        If (Not UFStringClass.CheckNumber(strValue)) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KATAGAKIFG)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.KATAGAKICD         '�����R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KATAGAKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KATAGAKI           '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.RENRAKUSAKI1       '�A����1
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RENRAKUSAKI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.RENRAKUSAKI2       '�A����2
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_RENRAKUSAKI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HON_ZJUSHOCD       '�{�БS���Z���R�[�h
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HON_ZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HON_JUSHO          '�{�ЏZ��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HON_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HONSEKIBANCHI      '�{�ДԒn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HONSEKIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HITTOSH            '�M����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HITTOSH)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CKINIDOYMD         '���߈ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.CKINJIYUCD         '���ߎ��R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CKINJIYU           '���ߎ��R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CKINTDKDYMD        '���ߓ͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINTDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.CKINTDKDTUCIKB     '���ߓ͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CKINTDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TOROKUIDOYMD       '�o�^�ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TOROKUIDOWMD       '�o�^�ٓ��a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUIDOWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TOROKUJIYUCD       '�o�^���R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TOROKUJIYU         '�o�^���R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TOROKUTDKDYMD      '�o�^�͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUTDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TOROKUTDKDWMD      '�o�^�͏o�a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUTDKDWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TOROKUTDKDTUCIKB   '�o�^�͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOROKUTDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUTEIIDOYMD        '�Z��ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUTEIIDOWMD        '�Z��ٓ��a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIIDOWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUTEIJIYUCD        '�Z�莖�R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUTEIJIYU          '�Z�莖�R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEIJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUTEITDKDYMD       '�Z��͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEITDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUTEITDKDWMD       '�Z��͏o�a��N����
                    If Not (strValue = String.Empty Or strValue = "0000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEITDKDWMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.JUTEITDKDTUCIKB    '�Z��͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUTEITDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHOJOIDOYMD        '�����ٓ��N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.SHOJOJIYUCD        '�������R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOJIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHOJOJIYU          '�������R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOJIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHOJOTDKDYMD       '�����͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOTDKDYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.SHOJOTDKDTUCIKB    '�����͏o�ʒm�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOJOTDKDTUCIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIIDOYMD     '�]�o�\��͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIIDOYMD      '�]�o�m��͏o�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIIDOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTITSUCHIYMD   '�]�o�m��ʒm�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTITSUCHIYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUNYURIYUCD       '�]�o�����R�R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUNYURIYUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUNYURIYU         '�]�o�����R
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUNYURIYU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_YUBINNO         '�]���O�Z���X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_YUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_ZJUSHOCD        '�]���O�Z���S���Z���R�[�h
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_ZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_JUSHO           '�]���O�Z���Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_JUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_BANCHI          '�]���O�Z���Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_BANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_KATAGAKI        '�]���O�Z������
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_KATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENUMAEJ_STAINUSMEI      '�]���O�Z�����ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENUMAEJ_STAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIYUBINNO    '�]�o�\��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIZJUSHOCD   '�]�o�\��S���Z���R�[�h
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIJUSHO      '�]�o�\��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIBANCHI     '�]�o�\��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEIKATAGAKI   '�]�o�\�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEIKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUYOTEISTAINUSMEI '�]�o�\�萢�ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUYOTEISTAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIYUBINNO     '�]�o�m��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIZJUSHOCD    '�]�o�m��S���Z���R�[�h
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIZJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIJUSHO     '�]�o�m��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIBANCHI      '�]�o�m��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIKATAGAKI    '�]�o�m�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTISTAINUSMEI  '�]�o�m�萢�ю喼
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTISTAINUSMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TENSHUTSUKKTIMITDKFG     '�]�o�m�薢�̓t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TENSHUTSUKKTIMITDKFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BIKOYMD                  '���l�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKOYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.BIKO                     '���l
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BIKOTENSHUTSUKKTIJUSHOFG '���l�]�o�m��Z���t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKOTENSHUTSUKKTIJUSHOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HANNO                    '�Ŕԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HANNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KAISEIATOFG              '������t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAISEIATOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KAISEIMAEFG             '�����O�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAISEIMAEFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KAISEIYMD                '�����N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAISEIYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.GYOSEIKUCD               '�s����R�[�h
                    '* ����ԍ� 000014 2005/12/26 �C���J�n
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* ����ԍ� 000014 2005/12/26 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_GYOSEIKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.GYOSEIKUMEI              '�s���於
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_GYOSEIKUMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUCD1                 '�n��R�[�h1
                    '*����ԍ� 00012 2004/08/13 �C���J�n
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*����ԍ� 00012 2004/08/13 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUCD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUMEI1                '�n�於1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUMEI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUCD2                 '�n��R�[�h2
                    '*����ԍ� 00012 2004/08/13 �C���J�n
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*����ԍ� 00012 2004/08/13 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUCD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUMEI2                '�n�於2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUMEI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUCD3                 '�n��R�[�h3
                    '*����ԍ� 00012 2004/08/13 �C���J�n
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*����ԍ� 00012 2004/08/13 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUCD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHIKUMEI3                '�n�於3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHIKUMEI3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.TOHYOKUCD                '���[��R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TOHYOKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHOGAKKOKUCD             '���w�Z��R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHOGAKKOKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.CHUGAKKOKUCD             '���w�Z��R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_CHUGAKKOKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.HOGOSHAJUMINCD           '�ی�ҏZ���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_HOGOSHAJUMINCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANJIHOGOSHAMEI          '�����ی�Җ�
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANJIHOGOSHAMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KANAHOGOSHAMEI           '�J�i�ی�Җ�
                    '*����ԍ� 000008 2003/10/30 �C���J�n
                    'If (Not UFStringClass.CheckKataKana(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '*����ԍ� 000008 2003/10/30 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KANAHOGOSHAMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KIKAYMD                  '�A���N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KIKAYMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.KARIIDOKB                '���ٓ��敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KARIIDOKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHORITEISHIKB            '������~�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHORITEISHIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIYUBINNO              '�Z��X�֔ԍ�
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIYUBINNO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SHORIYOKUSHIKB           '�����}�~�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SHORIYOKUSHIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIJUSHOCD              '�Z��Z���R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIJUSHOCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIJUSHO                '�Z��Z��
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIJUSHO)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIBANCHICD1            '�Z��Ԓn�R�[�h1
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHICD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIBANCHICD2            '�Z��Ԓn�R�[�h2
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHICD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIBANCHICD3            '�Z��Ԓn�R�[�h3
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHICD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIBANCHI               '�Z��Ԓn
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIBANCHI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIKATAGAKIFG           '�Z������t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIKATAGAKIFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIKATAGAKICD           '�Z������R�[�h
                    If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIKATAGAKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIKATAGAKI             '�Z�����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIKATAGAKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIGYOSEIKUCD           '�Z��s����R�[�h
                    '* ����ԍ� 000014 2005/12/26 �C���J�n
                    ''If (Not UFStringClass.CheckNumber(strValue.TrimStart)) Then
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '* ����ԍ� 000014 2005/12/26 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIGYOSEIKUCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKIGYOSEIKUMEI          '�Z��s���於
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKIGYOSEIKUMEI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUCD1                    '�Z��n��R�[�h1
                    '*����ԍ� 00012 2004/08/13 �C���J�n
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*����ԍ� 00012 2004/08/13 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUCD1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUMEI1            '�Z��n�於1
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUMEI1)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUCD2             '�Z��n��R�[�h2
                    '*����ԍ� 00012 2004/08/13 �C���J�n
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*����ԍ� 00012 2004/08/13 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUCD2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUMEI2            '�Z��n�於2
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUMEI2)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUCD3             '�Z��n��R�[�h3
                    '*����ԍ� 00012 2004/08/13 �C���J�n
                    If (Not UFStringClass.CheckANK(strValue.TrimStart)) Then
                        '*����ԍ� 00012 2004/08/13 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUCD3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.JUKICHIKUMEI3            '�Z��n�於3
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_JUKICHIKUMEI3)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KAOKUSHIKIKB             '�Ɖ��~�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KAOKUSHIKIKB)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.BIKOZEIMOKU              '���l�Ŗ�
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_BIKOZEIMOKU)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOKUSEKICD               '���ЃR�[�h
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOKUSEKICD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOKUSEKI                 '����
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOKUSEKI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYUSKAKCD             '�ݗ����i�R�[�h
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYUSKAKCD)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYUSKAK               '�ݗ����i
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYUSKAK)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYUKIKAN              '�ݗ�����
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYUKIKAN)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYU_ST_YMD            '�ݗ��J�n�N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYU_ST_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.ZAIRYU_ED_YMD            '�ݗ��I���N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_ZAIRYU_ED_YMD)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                    '*����ԍ� 000009 2003/11/18 �ǉ��J�n
                Case ABAtenaRuisekiEntity.KSNENKNNO, _
                        ABAtenaRuisekiEntity.JKYNENKNKIGO1, _
                        ABAtenaRuisekiEntity.JKYNENKNNO1, _
                        ABAtenaRuisekiEntity.JKYNENKNEDABAN1, _
                        ABAtenaRuisekiEntity.JKYNENKNKB1, _
                        ABAtenaRuisekiEntity.JKYNENKNKIGO2, _
                        ABAtenaRuisekiEntity.JKYNENKNNO2, _
                        ABAtenaRuisekiEntity.JKYNENKNEDABAN2, _
                        ABAtenaRuisekiEntity.JKYNENKNKB2, _
                        ABAtenaRuisekiEntity.JKYNENKNKIGO3, _
                        ABAtenaRuisekiEntity.JKYNENKNNO3, _
                        ABAtenaRuisekiEntity.JKYNENKNEDABAN3, _
                        ABAtenaRuisekiEntity.JKYNENKNKB3, _
                        ABAtenaRuisekiEntity.KOKUHOSHIKAKUKB
                    ' ��b�N���ԍ�
                    ' �󋋔N���L���P
                    ' �󋋔N���ԍ��P
                    ' �󋋔N���}�ԂP
                    ' �󋋔N���敪�P
                    ' �󋋔N���L���Q
                    ' �󋋔N���ԍ��Q
                    ' �󋋔N���}�ԂQ
                    ' �󋋔N���敪�Q
                    ' �󋋔N���L���R
                    ' �󋋔N���ԍ��R
                    ' �󋋔N���}�ԂR
                    ' �󋋔N���敪�R
                    ' ���ێ��i�敪
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002013)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.NENKNSKAKSHUTKYMD, _
                        ABAtenaRuisekiEntity.NENKNSKAKSSHTSYMD, _
                        ABAtenaRuisekiEntity.KOKUHOSHUTOKUYMD, _
                        ABAtenaRuisekiEntity.KOKUHOSOSHITSUYMD, _
                        ABAtenaRuisekiEntity.KOKUHOTISHKGAITOYMD, _
                        ABAtenaRuisekiEntity.KOKUHOTISHKHIGAITOYMD
                    ' �N�����i�擾�N����
                    ' �N�����i�r���N����
                    ' ���ێ擾�N����
                    ' ���ۑr���N����
                    ' ���ۑސE�Y���N����
                    ' ���ۑސE��Y���N����
                    If Not (strValue = String.Empty Or strValue = "00000000") Then
                        m_cfDateClass.p_strDateValue = strValue
                        If (Not m_cfDateClass.CheckDate()) Then
                            '�G���[��`���擾
                            m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002019)
                            '��O�𐶐�
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode)
                        End If
                    End If

                Case ABAtenaRuisekiEntity.NENKNSKAKSHUTKSHU, _
                        ABAtenaRuisekiEntity.NENKNSKAKSHUTKRIYUCD, _
                        ABAtenaRuisekiEntity.NENKNSKAKSSHTSRIYUCD, _
                        ABAtenaRuisekiEntity.JKYNENKNSHU1, _
                        ABAtenaRuisekiEntity.JKYNENKNSHU2, _
                        ABAtenaRuisekiEntity.JKYNENKNSHU3, _
                        ABAtenaRuisekiEntity.KOKUHONO, _
                        ABAtenaRuisekiEntity.KOKUHOGAKUENKB, _
                        ABAtenaRuisekiEntity.KOKUHOTISHKKB, _
                        ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKB
                    ' �N�����i�擾���
                    ' �N�����i�擾���R�R�[�h
                    ' �N�����i�r�����R�R�[�h
                    ' �󋋔N����ʂP
                    ' �󋋔N����ʂQ
                    ' �󋋔N����ʂR
                    ' ���۔ԍ�
                    ' ���ۊw���敪
                    ' ���ۑސE�敪
                    ' ���ۑސE�{��敪
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002017)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode)
                    End If

                    '*����ԍ� 000010 2003/12/01 �C���J�n
                    'Case ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBMEISHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBRYAKUSHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOGAKUENKBMEISHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOGAKUENKBRYAKUSHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOTISHKKBMEISHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOTISHKKBRYAKUSHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOTIAHKHONHIKBMEISHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBRYAKUSHO, _
                    '        ABAtenaRuisekiEntity.KOKUHOHOKENSHOKIGO, _
                    '        ABAtenaRuisekiEntity.KOKUHOHOKENSHONO
                Case ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBMEISHO, _
                  ABAtenaRuisekiEntity.KOKUHOSHIKAKUKBRYAKUSHO, _
                  ABAtenaRuisekiEntity.KOKUHOGAKUENKBMEISHO, _
                  ABAtenaRuisekiEntity.KOKUHOGAKUENKBRYAKUSHO, _
                  ABAtenaRuisekiEntity.KOKUHOTISHKKBMEISHO, _
                  ABAtenaRuisekiEntity.KOKUHOTISHKKBRYAKUSHO, _
                  ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBMEISHO, _
                  ABAtenaRuisekiEntity.KOKUHOTISHKHONHIKBRYAKUSHO, _
                  ABAtenaRuisekiEntity.KOKUHOHOKENSHOKIGO ', _
                    '*����ԍ� 000011 2004/03/06 �C���J�n
                    'ABAtenaRuisekiEntity.KOKUHOHOKENSHONO
                    '*����ԍ� 000011 2004/03/06 �C���J�n
                    '*����ԍ� 000010 2003/12/01 �C���I��
                    ' ���ێ��i�敪��������
                    ' ���ێ��i�敪��������
                    ' ���ۊw���敪��������
                    ' ���ۊw���敪��������
                    ' ���ۑސE�敪��������
                    ' ���ۑސE�敪��������
                    ' ���ۑސE�{��敪��������
                    ' ���ۑސE�{��敪��������
                    ' ���ەی��؋L��
                    ' ���ەی��ؔԍ�
                    If (Not UFStringClass.CheckKanjiCode(strValue, m_cfConfigDataClass)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABE002011)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage + strColumnName, objErrorStruct.m_strErrorCode)
                    End If

                    '*����ԍ� 000009 2003/11/18 �ǉ��I��

                Case ABAtenaRuisekiEntity.RESERCE                  '���U�[�u
                    '�`�F�b�N�Ȃ�

                Case ABAtenaRuisekiEntity.TANMATSUID               '�[���h�c
                    '* ����ԍ� 000006 2003/09/11 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000006 2003/09/11 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_TANMATSUID)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SAKUJOFG                 '�폜�t���O
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SAKUJOFG)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOSHINCOUNTER            '�X�V�J�E���^
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOSHINCOUNTER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SAKUSEINICHIJI           '�쐬����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SAKUSEINICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.SAKUSEIUSER              '�쐬���[�U
                    '* ����ԍ� 000007 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000007 2003/10/09 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_SAKUSEIUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOSHINNICHIJI            '�X�V����
                    If (Not UFStringClass.CheckNumber(strValue)) Then
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOSHINNICHIJI)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

                Case ABAtenaRuisekiEntity.KOSHINUSER               '�X�V���[�U
                    '* ����ԍ� 000007 2003/10/09 �C���J�n
                    'If (Not UFStringClass.CheckAlphabetNumber(strValue)) Then
                    If (Not UFStringClass.CheckANK(strValue)) Then
                        '* ����ԍ� 000007 2003/10/09 �C���I��
                        '�G���[��`���擾
                        m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                        objErrorStruct = m_cfErrorClass.GetErrorStruct(ABErrorClass.ABATENARUISEKIB_RDBDATATYPE_KOSHINUSER)
                        '��O�𐶐�
                        Throw New UFAppException(objErrorStruct.m_strErrorMessage, objErrorStruct.m_strErrorCode)
                    End If

            End Select

            ' �f�o�b�O�I�����O�o��
            'm_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLogClass.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[���X���[����
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

    '*����ԍ� 000016 2011/10/24 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��       �Z��@�����׸ގ擾
    '* 
    '* �\��             Private Function GetJukihoKaiseiFG()
    '* 
    '* �@�\�@�@    �@   �Ǘ������擾����
    '* 
    '* ����             �Ȃ�
    '* 
    '* �߂�l           �Ȃ�
    '************************************************************************************************
    Private Sub GetJukihoKaiseiFG()
        Const THIS_METHOD_NAME As String = "GetJukihoKaiseiFG"
        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            If (IsNothing(m_csSekoYMDHanteiB)) Then
                '�{�s������a�׽�̲ݽ�ݽ��
                m_csSekoYMDHanteiB = New ABSekoYMDHanteiBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)
                '�Z��@�����׸ށ��{�s�����茋��
                m_blnJukihoKaiseiFG = m_csSekoYMDHanteiB.CheckAfterSekoYMD
            Else
                '�����Ȃ�
            End If

            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
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
            ' �V�X�e���G���[���X���[����
            Throw objExp

        End Try
    End Sub
    '*����ԍ� 000016 2011/10/24 �ǉ��I��

#End Region

End Class
