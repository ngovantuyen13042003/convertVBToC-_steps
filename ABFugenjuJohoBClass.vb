'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �W�����@�����Ǘ��@�s���Z�Ǘ��@�\
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t��           2024/01/15
'*
'* �쐬�ҁ@�@�@     ��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2024/01/15           �yAB-0830-1�z�s���Z�Ǘ��@�\�ǉ�(��)
'* 2024/03/07  000001   �yAB-0900-1�z�A�h���X�E�x�[�X�E���W�X�g���Ή�(����)
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
Imports Densan.Common

Public Class ABFugenjuJohoBClass

#Region "�����o�ϐ�"
    '�����o�ϐ��̒�`
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_csDataSchma As DataSet                        ' �X�L�[�}�ۊǗp�f�[�^�Z�b�g:�S���ڗp

    Private m_strInsertSQL As String
    Private m_strUpDateSQL As String
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE�p�p�����[�^�R���N�V����

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABFugenjuJohoBClass"

    '�萔
    Private Const MAX_ROWS As Integer = 100                       ' �ő�擾����
#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass, 
    '* �@�@                          ByVal cfRdbClass As UFRdbClass)
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
        m_cfLogClass = New UFLogClass(m_cfConfigDataClass, m_cfControlData.m_strBusinessId)

        ' SQL���̍쐬
        ' �S���ڒ��o�p�X�L�[�}
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABFugenjuJohoEntity.TABLE_NAME, ABFugenjuJohoEntity.TABLE_NAME, False)
    End Sub
#End Region

#Region "���\�b�h"

#Region "�s���Z���f�[�^�擾���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   �s���Z���f�[�^�擾���\�b�h
    '* 
    '* �\��         Public Function GetFugenjuJohoData(ByVal csABFugenjuJohoParaX As ABFugenjuJohoParaXClass) As DataSet
    '* 
    '* �@�\         �s���Z�����Y���f�[�^���擾����B
    '* 
    '* ����         csABFugenjuJohoParaX As ABFugenjuJohoParaXClass   : �s���Z���p�����[�^�N���X
    '* 
    '* �߂�l       �擾�����s���Z���̊Y���f�[�^�iDataSet�j
    '*                 �\���FcsFugenjuJohoEntity    
    '************************************************************************************************
    Public Overloads Function GetFugenjuJohoData(ByVal csABFugenjuJohoParaX As ABFugenjuJohoParaXClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetFugenjuJohoData"
        Dim csFugenjuJohoEntity As DataSet                              ' �s���Z���f�[�^
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                      ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X
        Dim cABKensakuShimeiB As ABKensakuShimeiBClass                  ' ���������ҏWB�N���X
        Dim intAimaiKanji As Integer = 0                                ' ���p�����܂܂�鐔(�����j
        Dim intAimaiKana As Integer = 0                                 ' ���p�����܂܂�鐔(�J�i�j
        Dim strJushoCD As String = String.Empty                         ' �Z���R�[�h
        Dim strJusho As String = String.Empty                           ' �Z��
        Dim strBanchi As String = String.Empty                          ' �Ԓn
        Dim strKatagaki As String = String.Empty                        ' ����
        Dim strShimei As String = String.Empty                          ' ����
        Const CHAR_PERCENT As String = "%"                              ' %
        Dim cRuijiClass As New USRuijiClass                             ' �ގ������N���X
        Dim strRuijiJusho As String

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            ' SELECT��
            strSQL.Append(Me.CreateSelect)
            strSQL.Append(" FROM ").Append(ABFugenjuJohoEntity.TABLE_NAME)

            ' WHERE��
            strSQL.Append(" WHERE ")

            '�K�{��������
            '�폜�f�[�^�͒��o���Ȃ��悤�Ɉȉ��̏�����ǉ�����B
            strSQL.Append(ABFugenjuJohoEntity.SAKUJOFG).Append(" <> '1'")

            '����
            If (csABFugenjuJohoParaX.p_strShimei.Trim.RLength > 0) Then
                'AB000BB.ABKensakuShimeiBClass��GetKensakuShimei���\�b�h�𗘗p���A�����p������ҏW����B
                '�������̏ꍇ�͗ގ����A�J�i�̏ꍇ�͔��p�������A�A���t�@�x�b�g�̏ꍇ�͑啶�������s���A�����̑O����v�̒l�ɉ����ĕ�����̑O��ɔ��p���̕t�^���s���B

                strShimei = csABFugenjuJohoParaX.p_strShimei.Replace("��", String.Empty).Replace("*", String.Empty).Replace("�@", String.Empty).Replace(" ", String.Empty)
                '�C���X�^���X�쐬
                cABKensakuShimeiB = New ABKensakuShimeiBClass(m_cfControlData, m_cfConfigDataClass)
                cABKensakuShimeiB.GetKensakuShimei(csABFugenjuJohoParaX.p_strShimeiZenpoIcchi, strShimei)
                intAimaiKanji = InStr(cABKensakuShimeiB.p_strSearchkanjimei, CHAR_PERCENT)
                intAimaiKana = InStr(cABKensakuShimeiB.p_strSearchKanaseimei, CHAR_PERCENT)

                If (cABKensakuShimeiB.p_strSearchkanjimei.Trim.RLength > 0) Then
                    '�����p�����N���X.�����p�������́��󔒂̏ꍇ
                    If (intAimaiKanji > 0) Then
                        '�����p�����N���X.�����p�������̂ɔ��p�����܂܂�Ă���ꍇ
                        'AB�s���Z���.�s���Z���i�����p���������j�@LIKE�@'�����p�����N���X.�����p��������'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANJISHIMEI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANJISHIMEI)
                    Else
                        'AB�s���Z���.�s���Z���i�����p���������j�@���@'�����p�����N���X.�����p��������'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANJISHIMEI)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANJISHIMEI)
                    End If

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANJISHIMEI
                    cfUFParameterClass.Value = cABKensakuShimeiB.p_strSearchkanjimei
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ElseIf (cABKensakuShimeiB.p_strSearchKanaseimei.Trim.RLength > 0) Then
                    '�����p�����N���X.�����p�J�i�������󔒂̏ꍇ
                    If (intAimaiKana > 0) Then
                        '�����p�����N���X.�����p�J�i�����ɔ��p�����܂܂�Ă���ꍇ
                        'AB�s���Z���.�s���Z���i�����p�J�i�����j�@LIKE�@'�����p�����N���X.�����p�J�i����'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANASHIMEI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANASHIMEI)
                    Else
                        'AB�s���Z���.�s���Z���i�����p�J�i�����j�@���@'�����p�����N���X.�����p�J�i����'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANASHIMEI)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANASHIMEI)
                    End If

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEARCHKANASHIMEI
                    cfUFParameterClass.Value = cABKensakuShimeiB.p_strSearchKanaseimei
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)

                End If
            End If

            '���N����
            If (csABFugenjuJohoParaX.p_strUmareymd.Trim.RLength > 0) Then
                'AB�s���Z���.�s���Z���i���N�����j�@���@'AB�s���Z���.���N����'
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD)
                strSQL.Append(" = ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_UMAREYMD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_UMAREYMD
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strUmareymd.Trim.ToString
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '����
            If (csABFugenjuJohoParaX.p_strSeibetuCD.Trim.RLength > 0) Then
                'AB�s���Z���.�s���Z���i���ʁj�@���@'AB�s���Z���.����'
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU)
                strSQL.Append(" = ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEIBETSU)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUJOHO_SEIBETSU
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strSeibetuCD.Trim.ToString
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z���R�[�h
            If ((csABFugenjuJohoParaX.p_strJushoSearchShitei.Trim.ToString = "1") _
                OrElse (csABFugenjuJohoParaX.p_strJushoSearchShitei.Trim.ToString = "3")) Then
                '�s���Z�����p�����[�^.�Z�������w�聁1�i�Z���R�[�h�Ō����j or 3�i�Z���R�[�h�ƏZ���Ō����j�̏ꍇ
                If (csABFugenjuJohoParaX.p_strJushoCD.Trim.RLength > 0) Then
                    If (csABFugenjuJohoParaX.p_strKangaiJushoKB.Trim.ToString = "1") Then
                        '�s���Z�����p�����[�^.�ǊO�Z���敪��1�i�ǊO�Z���j 
                        If ((RegularExpressions.Regex.IsMatch(csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(2), "0+?")) AndAlso
                            (csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(2).Distinct().Count() = 1)) Then
                            '�s���Z�����p�����[�^.�s�撬���R�[�h�̏�2���ȍ~���S��"0"�̏ꍇ�i�s���{���R�[�h�Ō����j
                            'LTRIM�iAB�s���Z���.�s���Z�������Z��_�Z���R�[�h�j�@LIKE�@'�s���Z�����p�����[�^.�Z���R�[�h�̏�2�� + ���p��'
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                            strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(0, 2) + CHAR_PERCENT
                        ElseIf ((RegularExpressions.Regex.IsMatch(csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(5), "0+?")) AndAlso
                            (csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(5).Distinct().Count() = 1)) Then
                            '�s���Z�����p�����[�^.�Z���R�[�h�̏�5���ȍ~���S��"0"�̏ꍇ�i�s�撬���R�[�h�Ō����j
                            'LTRIM�iAB�s���Z���.�s���Z�������Z��_�Z���R�[�h�j�@LIKE�@'�s���Z�����p�����[�^.�Z���R�[�h�̏�5�� + ���p��'
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                            strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(0, 5) + CHAR_PERCENT
                        ElseIf ((RegularExpressions.Regex.IsMatch(csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(8), "0+?")) AndAlso
                            (csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(8).Distinct().Count() = 1)) Then
                            '�s���Z�����p�����[�^.�Z���R�[�h�̏�8���ȍ~���S��"0"�̏ꍇ�i�s�撬���R�[�h�Ō����j
                            'LTRIM�iAB�s���Z���.�s���Z�������Z��_�Z���R�[�h�j�@LIKE�@'�s���Z�����p�����[�^.�Z���R�[�h�̏�8�� + ���p��'
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                            strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.Trim.RSubstring(0, 8) + CHAR_PERCENT
                        Else
                            '�i�S���Z���R�[�h�Ō����j
                            'LTRIM�iAB�s���Z���.�s���Z�������Z��_�s�撬���R�[�h�j +�@LTRIM�iAB�s���Z���.�s���Z�������Z��_�����R�[�h�j ���@'�s���Z�����p�����[�^.�Z���R�[�h'
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                            strSQL.Append(" = ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                            strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.RPadRight(13)
                        End If
                    Else
                        'LTRIM�iAB�s���Z���.�s���Z�������Z��_�s�撬���R�[�h�j +�@LTRIM�iAB�s���Z���.�s���Z�������Z��_�����R�[�h�j ���@'�s���Z�����p�����[�^.�Z���R�[�h'
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD)
                        strJushoCD = csABFugenjuJohoParaX.p_strJushoCD.Trim.RPadLeft(13)
                    End If

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_JUSHOCD
                    cfUFParameterClass.Value = strJushoCD
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)
                End If
            End If

            '�Z��
            If ((csABFugenjuJohoParaX.p_strJushoSearchShitei.Trim.ToString = "2") _
                OrElse (csABFugenjuJohoParaX.p_strJushoSearchShitei.Trim.ToString = "3")) Then
                '�s���Z�����p�����[�^.�Z�������w�聁2�i�Z���Ō����j or 3�i�Z���R�[�h�ƏZ���Ō����j�̏ꍇ
                If (csABFugenjuJohoParaX.p_strJusho.Trim.RLength > 0) Then
                    strRuijiJusho = cRuijiClass.GetRuijiMojiList((csABFugenjuJohoParaX.p_strJusho).Replace("�@", String.Empty)).ToUpper
                    Select Case csABFugenjuJohoParaX.p_strJushoZenpoIcchi.Trim.ToString
                        Case "1"
                            '�s���Z�����p�����[�^.�Z���O����v��1�i�O����v�j�̏ꍇ
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strJusho = strRuijiJusho + CHAR_PERCENT
                        Case "2"
                            '�s���Z�����p�����[�^.�Z���O����v��2�i������v�j�̏ꍇ
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strSQL.Append(" LIKE ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strJusho = CHAR_PERCENT + strRuijiJusho + CHAR_PERCENT
                        Case Else
                            '�i���S��v�j
                            strSQL.Append(" AND ")
                            strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strSQL.Append(" = ")
                            strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_SEARCHJUSHO)
                            strJusho = strRuijiJusho
                    End Select

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_SEARCHJUSHO
                    cfUFParameterClass.Value = strJusho
                    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                    cfUFParameterCollectionClass.Add(cfUFParameterClass)
                End If
            End If

            '�Ԓn
            If (csABFugenjuJohoParaX.p_strBanchi.Trim.RLength > 0) Then
                Select csABFugenjuJohoParaX.p_strBanchiZenpoIcchi.Trim.ToString
                    Case "1"
                        '�s���Z�����p�����[�^.�Ԓn�O����v��1�i�O����v�j�̏ꍇ
                        'AB�s���Z���.�s���Z�������Z��_�Ԓn���\�L�@LIKE�@�f�s���Z�����p�����[�^.�Ԓn + ���p���f
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_BANCHI)
                        strBanchi = csABFugenjuJohoParaX.p_strBanchi.Trim.ToString + CHAR_PERCENT
                    Case "2"
                        '���Z�����p�����[�^.�Ԓn�O����v��2�i������v�j�̏ꍇ
                        'AB�s���Z���.�s���Z�������Z��_�Ԓn���\�L�@LIKE�@�f���p�� + �s���Z�����p�����[�^.�Ԓn + ���p���f
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_BANCHI)
                        strBanchi = CHAR_PERCENT + csABFugenjuJohoParaX.p_strBanchi.Trim.ToString + CHAR_PERCENT
                    Case Else
                        '�i���S��v�j
                        'AB�s���Z���s���Z�������Z��_�Ԓn���\�L�@���@�f�s���Z�����p�����[�^.�Ԓn�f
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_BANCHI)
                        strBanchi = csABFugenjuJohoParaX.p_strBanchi.Trim.ToString
                End Select

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_BANCHI
                cfUFParameterClass.Value = strBanchi
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '����
            If (csABFugenjuJohoParaX.p_strKatagaki.Trim.RLength > 0) Then
                Select csABFugenjuJohoParaX.p_strKatagakiZenpoIcchi.Trim.ToString
                    Case "1"
                        '�s���Z�����p�����[�^.�����O����v��1�i�O����v�j�̏ꍇ
                        'AB�s���Z���.�s���Z�������Z��_�����@LIKE�@�f�s���Z�����p�����[�^.���� + ���p���f
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_KATAGAKI)
                        strKatagaki = csABFugenjuJohoParaX.p_strKatagaki.Trim.ToString + CHAR_PERCENT
                    Case "2"
                        '�s���Z�����p�����[�^.�����O����v��2�i������v�j�̏ꍇ
                        'AB�s���Z���.�s���Z�������Z��_�����@LIKE�@�f���p�� + �s���Z�����p�����[�^.���� + ���p���f
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)
                        strSQL.Append(" LIKE ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_KATAGAKI)
                        strKatagaki = CHAR_PERCENT + csABFugenjuJohoParaX.p_strKatagaki.Trim.ToString + CHAR_PERCENT
                    Case Else
                        '�i���S��v�j
                        'AB�s���Z���s���Z�������Z��_�����@���@�f�s���Z�����p�����[�^.�����f
                        strSQL.Append(" AND ")
                        strSQL.Append(ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)
                        strSQL.Append(" = ")
                        strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_KATAGAKI)
                        strKatagaki = csABFugenjuJohoParaX.p_strKatagaki.Trim.ToString
                End Select

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUDATTAJUSHO_KATAGAKI
                cfUFParameterClass.Value = strKatagaki
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�Z���R�[�h
            If (csABFugenjuJohoParaX.p_strJuminCD.Trim.RLength > 0) Then
                'AB�s���Z���.�Z���R�[�h�@���@�f�s���Z�����p�����[�^.�Z���R�[�h�f
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.JUMINCD)
                strSQL.Append(" = ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_JUMINCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_JUMINCD
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strJuminCD.Trim.ToString
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�o�^�N����
            If ((csABFugenjuJohoParaX.p_strTorokuSTYMD.Trim.RLength > 0) _
                AndAlso (csABFugenjuJohoParaX.p_strTorokuEDYMD.Trim.RLength > 0)) Then
                'AB�s���Z���.�s���Z�o�^�N�����@���@�f�s���Z�����p�����[�^.�J�n�o�^�N�����f
                'AND�@AB�s���Z���.�s���Z�o�^�N�����@���@�f�s���Z�����p�����[�^.�I���o�^�N�����f
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUTOROKUYMD).Append(" >= ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUTOROKUYMD + "_ST")
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUTOROKUYMD).Append(" <= ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUTOROKUYMD + "_ED")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUTOROKUYMD + "_ST"
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strTorokuSTYMD.Trim.ToString
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUTOROKUYMD + "_ED"
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strTorokuEDYMD.Trim.ToString
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '���Z�s���N����
            If ((csABFugenjuJohoParaX.p_strKyojuFumeiSTYMD.Trim.RLength > 0) _
                AndAlso (csABFugenjuJohoParaX.p_strKyojuFumeiEDYMD.Trim.RLength > 0)) Then
                'AB�s���Z���.�s���Z���Z�s���N�����@���@�f�s���Z�����p�����[�^.�J�n���Z�s���N�����f
                'AND�@AB�s���Z���.�s���Z���Z�s���N�����@���@�f�s���Z�����p�����[�^.�I�����Z�s���N�����f

                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.KYOJUFUMEI_YMD).Append(" >= ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_KYOJUFUMEI_YMD + "_ST")
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.KYOJUFUMEI_YMD).Append(" <= ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_KYOJUFUMEI_YMD + "_ED")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_KYOJUFUMEI_YMD + "_ST"
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strKyojuFumeiSTYMD.Trim.ToString
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_KYOJUFUMEI_YMD + "_ED"
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strKyojuFumeiEDYMD.Trim.ToString
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�s���Z�敪
            If (csABFugenjuJohoParaX.p_strFugenjuKB.Trim.RLength > 0) Then
                strSQL.Append(" AND ")
                strSQL.Append(ABFugenjuJohoEntity.FUGENJUKB)
                strSQL.Append(" = ")
                strSQL.Append(ABFugenjuJohoEntity.PARAM_FUGENJUKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_FUGENJUKB
                cfUFParameterClass.Value = csABFugenjuJohoParaX.p_strFugenjuKB.Trim.ToString
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            End If

            '�ő�擾����
            If (csABFugenjuJohoParaX.p_intHyojiKensu = 0) Then
                '���oSQL�̎��s���s���q�c�a�N���X�iUFRdbClass�j�̍ő�擾�����v���p�e�B�ip_intMaxRows�j��100��ݒ肷��
                m_cfRdbClass.p_intMaxRows = MAX_ROWS
            Else
                '���oSQL�̎��s���s���q�c�a�N���X�iUFRdbClass�j�̍ő�擾�����v���p�e�B�ip_intMaxRows�j�ɕs���Z�����p�����[�^.�ő�擾�����̒l��ݒ肷��
                m_cfRdbClass.p_intMaxRows = csABFugenjuJohoParaX.p_intHyojiKensu
            End If

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                  "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                  "�y���s���\�b�h��:GetDataSet�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csFugenjuJohoEntity = m_csDataSchma.Clone()
            csFugenjuJohoEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csFugenjuJohoEntity, ABFugenjuJohoEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

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

        Return csFugenjuJohoEntity
    End Function
#End Region

#Region "�s���Z���f�[�^�ǉ����\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   �s���Z���f�[�^�ǉ����\�b�h
    '* 
    '* �\��         Public Function InsertFugenjuJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     �s���Z���ɐV�K�f�[�^��ǉ�����B
    '* 
    '* ����         csDataRow As DataRow   : �s���Z�ҏ��(ABFUGENJUJOHO)
    '* 
    '* �߂�l       �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertFugenjuJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertFugenjuJoho"
        Dim cfParam As UFParameterClass                                 ' �p�����[�^�N���X
        Dim intInsCnt As Integer                                        ' �ǉ�����
        Dim strUpdateDateTime As String                                 ' �V�X�e�����t

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If ((m_strInsertSQL Is Nothing Or m_strInsertSQL = String.Empty) _
                OrElse (m_cfInsertUFParameterCollectionClass Is Nothing)) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' �X�V�����̎擾
            strUpdateDateTime = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")            ' �쐬����

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABFugenjuJohoEntity.TANMATSUID) = m_cfControlData.m_strClientId               ' �[���h�c
            csDataRow(ABFugenjuJohoEntity.SAKUJOFG) = "0"                                           ' �폜�t���O
            csDataRow(ABFugenjuJohoEntity.KOSHINCOUNTER) = Decimal.Zero                             ' �X�V�J�E���^
            csDataRow(ABFugenjuJohoEntity.SAKUSEINICHIJI) = strUpdateDateTime                       ' �쐬����
            csDataRow(ABFugenjuJohoEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId                ' �쐬���[�U�[
            csDataRow(ABFugenjuJohoEntity.KOSHINNICHIJI) = strUpdateDateTime                        ' �X�V����
            csDataRow(ABFugenjuJohoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                 ' �X�V���[�U�[

            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABFugenjuJohoEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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

#Region "�s���Z���f�[�^�X�V���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   �s���Z���f�[�^�X�V���\�b�h
    '* 
    '* �\��         Public Function UpdateFugenjuJoho(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     �s���Z���̃f�[�^���X�V����B
    '* 
    '* ����         csDataRow As DataRow   : �s���Z�ҏ��(ABFUGENJUJOHO)
    '* 
    '* �߂�l       �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateFugenjuJoho(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateFugenjuJoho"
        Dim cfParam As UFParameterClass                         ' �p�����[�^�N���X
        Dim intUpdCnt As Integer                                ' �X�V����

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SQL���쐬����Ă��Ȃ���΍쐬
            If ((m_strUpDateSQL Is Nothing Or m_strUpDateSQL = String.Empty) _
                OrElse (m_cfUpdateUFParameterCollectionClass Is Nothing)) Then
                Call CreateSQL(csDataRow)
            Else
            End If

            ' ���ʍ��ڂ̕ҏW���s��
            csDataRow(ABFugenjuJohoEntity.TANMATSUID) = m_cfControlData.m_strClientId                                   ' �[���h�c
            csDataRow(ABFugenjuJohoEntity.KOSHINCOUNTER) = CDec(csDataRow(ABFugenjuJohoEntity.KOSHINCOUNTER)) + 1       ' �X�V�J�E���^
            csDataRow(ABFugenjuJohoEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")     ' �X�V����
            csDataRow(ABFugenjuJohoEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                     ' �X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABFugenjuJohoEntity.PREFIX_KEY.RLength) = ABFugenjuJohoEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                        csDataRow(cfParam.ParameterName.RSubstring(ABFugenjuJohoEntity.PREFIX_KEY.RLength),
                        DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABFugenjuJohoEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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

#Region " SQL���̍쐬"
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
            m_strInsertSQL = "INSERT INTO " + ABFugenjuJohoEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' UPDATE SQL���̍쐬
            m_strUpDateSQL = "UPDATE " + ABFugenjuJohoEntity.TABLE_NAME + " SET "

            ' UPDATE Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABFugenjuJohoEntity.JUMINCD)
            strWhere.Append(" = ")
            strWhere.Append(ABFugenjuJohoEntity.PREFIX_KEY + ABFugenjuJohoEntity.JUMINCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABFugenjuJohoEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABFugenjuJohoEntity.PREFIX_KEY + ABFugenjuJohoEntity.KOSHINCOUNTER)

            ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABFugenjuJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' UPDATE SQL���̍쐬
                m_strUpDateSQL += csDataColumn.ColumnName + " = " + ABFugenjuJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            ' �Z���R�[�h
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PREFIX_KEY + ABFugenjuJohoEntity.JUMINCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABFugenjuJohoEntity.PREFIX_KEY + ABFugenjuJohoEntity.KOSHINCOUNTER
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

#Region "SELECT��̍쐬"
    '************************************************************************************************
    '* ���\�b�h��     SELECT��̍쐬
    '* 
    '* �\��           Private Sub CreateSelect() As String
    '* 
    '* �@�\�@�@    �@ SELECT��𐶐�����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         String    :   SELECT��
    '************************************************************************************************
    Private Function CreateSelect() As String
        Const THIS_METHOD_NAME As String = "CreateSelect"
        Dim csSELECT As New StringBuilder

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' SELECT��̍쐬
            csSELECT.AppendFormat("SELECT {0}", ABFugenjuJohoEntity.SHICHOSONCD)                      ' �s�����R�[�h
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.JUMINCD)                               ' �Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUKB)                             ' �s���Z�敪
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_YUBINNO)             ' �s���Z�������Z��_�X�֔ԍ�
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANNAIKANGAIKB)      ' �s���Z�������Z��_�Ǔ��ǊO�敪
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHOCD)             ' �s���Z�������Z��_�Z���R�[�h
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_JUSHO)               ' �s���Z�������Z��_�Z��
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHICHOSONCD)         ' �s���Z�������Z��_�s�撬���R�[�h
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZACD)          ' �s���Z�������Z��_�����R�[�h
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_TODOFUKEN)           ' �s���Z�������Z��_�s���{��
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SHIKUGUNCHOSON)      ' �s���Z�������Z��_�s��S������
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_MACHIAZA)            ' �s���Z�������Z��_����
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_BANCHI)              ' �s���Z�������Z��_�Ԓn���\�L
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KATAGAKI)            ' �s���Z�������Z��_����
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_KANAKATAGAKI)        ' �s���Z�������Z��_����_�t���K�i
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKUBUN)            ' �s���Z���i�Ώێҋ敪�j
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI)           ' �s���Z���i�ΏێҎ����j
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHAKANASHIMEI)       ' �s���Z���i�Ώێ҃J�i�����j
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANJISHIMEI)         ' �s���Z���i�����p���������j
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_SEARCHKANASHIMEI)          ' �s���Z���i�����p�J�i�����j
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUDATTAJUSHO_SEARCHJUSHO)         ' �s���Z�������Z��_�����p�Z��
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI_SEI)       ' �s���Z���i�ΏێҎ���_���j
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_TAISHOSHASHIMEI_MEI)       ' �s���Z���i�ΏێҎ���_���j
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_UMAREYMD)                  ' �s���Z���i���N�����j
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_SEIBETSU)                  ' �s���Z���i���ʁj
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.KYOJUFUMEI_YMD)                        ' ���Z�s���N����
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUTOROKUYMD)                      ' �s���Z�o�^�N����
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUGYOSEIKUCD)                     ' �w��s�s_�s���擙�R�[�h
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.FUGENJUJOHO_BIKO)                      ' �s���Z���i���l�j
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.RESERVE)                               ' ���U�[�u
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.TANMATSUID)                            ' �[��ID
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.SAKUJOFG)                              ' �폜�t���O
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.KOSHINCOUNTER)                         ' �X�V�J�E���^
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.SAKUSEINICHIJI)                        ' �쐬����
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.SAKUSEIUSER)                           ' �쐬���[�U
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.KOSHINNICHIJI)                         ' �X�V����
            csSELECT.AppendFormat(", {0}", ABFugenjuJohoEntity.KOSHINUSER)                            ' �X�V���[�U

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

        Return csSELECT.ToString

    End Function

#End Region
#End Region

End Class
