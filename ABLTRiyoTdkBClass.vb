'************************************************************************************************
'* �Ɩ���           �����Ǘ��V�X�e��
'* 
'* �N���X��         �`�a���k�s�`�w���p�̓}�X�^�c�`(ABLTRiyoTdkBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t��           2008/11/10
'*
'* �쐬�ҁ@�@�@     ��Á@�v��
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2008/11/18   000001     �ǉ������A�X�V�������\�b�h��ǉ��i��Áj
'* 2008/11/27   000002     ���p�̓f�[�^�擾�V���\�b�h��ǉ��i��Áj
'* 2009/07/27   000003     ���p�͏o�A�g�@�\�ǉ��ɔ������C�i��Áj
'* 2009/11/16   000004     ��������:�J�i�����������J�i�����ɏC���i��Áj
'* 2010/02/22   000005     �폜�������\�b�h��ǉ��i��Áj
'* 2010/04/16   000006     VS2008�Ή��i��Áj
'* 2014/08/15   000007     �yAB21010�z�l�ԍ����x�Ή� �d�q�\���i�≺�j
'* 2015/03/19   000008     �yAB21010�z�l�ԍ����x�Ή� �d�q�\�� SQL�s��C���i�≺�j
'* 2020/11/06   000009     �yAB00189�z���p�͏o�����[�Ŏ�ID�Ή��i�{�]�j
'* 2024/01/09   000010     �yAB-0770-1�z���p�͏o�f�[�^�Ǘ��Ή��i����j
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
'*����ԍ� 000009 2020/11/06 �ǉ��J�n
Imports System.Collections.Generic
'*����ԍ� 000009 2020/11/06 �ǉ��I��

Public Class ABLTRiyoTdkBClass

#Region "�����o�ϐ�"
    '�����o�ϐ��̒�`
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigDataClass As UFConfigDataClass        ' �R���t�B�O�f�[�^
    Private m_cfRdbClass As UFRdbClass                      ' �q�c�a�N���X
    Private m_cfLogClass As UFLogClass                      ' ���O�o�̓N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_csDataSchma As DataSet                        ' �X�L�[�}�ۊǗp�f�[�^�Z�b�g:�S���ڗp
    Private m_csDataSchma_Select As DataSet                 ' �X�L�[�}�ۊǗp�f�[�^�Z�b�g:�[�Ŏ�ID,���p��ID

    '*����ԍ� 000001 2008/11/17 �ǉ��J�n
    Private m_strInsertSQL As String
    Private m_strUpDateSQL As String
    Private m_cfInsertUFParameterCollectionClass As UFParameterCollectionClass  'INSERT�p�p�����[�^�R���N�V����
    Private m_cfUpdateUFParameterCollectionClass As UFParameterCollectionClass  'UPDATE�p�p�����[�^�R���N�V����
    '*����ԍ� 000001 2008/11/17 �ǉ��I��
    '*����ԍ� 000005 2010/02/22 �ǉ��J�n
    Private m_strDeleteSQL As String
    Private m_cfDeleteUFParameterCollectionClass As UFParameterCollectionClass  'Delete�p�p�����[�^�R���N�V����
    '*����ԍ� 000005 2010/02/22 �ǉ��I��

    '�R���X�^���g��`
    Private Const THIS_CLASS_NAME As String = "ABLTRiyoTdkBClass"
    Private Const THIS_BUSINESSID As String = "AB"                              ' �Ɩ��R�[�h
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
        m_csDataSchma = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT * FROM " + ABLtRiyoTdkEntity.TABLE_NAME, ABLtRiyoTdkEntity.TABLE_NAME, False)
        ' �[�Ŏ�ID�A���p��ID�p�X�L�[�}
        m_csDataSchma_Select = m_cfRdbClass.GetTableSchemaNoRestriction("SELECT NOZEIID,RIYOSHAID FROM " + ABLtRiyoTdkEntity.TABLE_NAME, ABLtRiyoTdkEntity.TABLE_NAME, False)

    End Sub
#End Region

#Region "���\�b�h"

#Region "eLTAX���p�̓f�[�^�擾���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX���p�̓f�[�^�擾���\�b�h
    '* 
    '* �\��         Public Function GetLTRiyoTdkData(ByVal csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass) As DataSet
    '* 
    '* �@�\�@�@     ���p�͏o�}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����         csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass   : ���p�͏o�p�����[�^�N���X
    '* 
    '* �߂�l       �擾�������p�͏o�}�X�^�̊Y���f�[�^�iDataSet�j
    '*                 �\���FcsLtRiyoTdkEntity    
    '************************************************************************************************
    '*����ԍ� 000002 2008/11/27 �C���J�n
    'Public Function GetLTRiyoTdkData(ByVal csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass) As DataSet
    Public Overloads Function GetLTRiyoTdkData(ByVal csABLTRiyoTdkParaX As ABLTRiyoTdkParaXClass) As DataSet
        '*����ԍ� 000002 2008/11/27 �C���I��
        Const THIS_METHOD_NAME As String = "GetLTRiyoTdkData"
        Dim objErrorStruct As UFErrorStruct                             ' �G���[��`�\����
        Dim csLtRiyoTdkEntity As DataSet                                ' ���p�͏o�}�X�^�f�[�^
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                      ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X
        Dim blnAndFg As Boolean = False                                 ' AND����t���O

        '����ԍ� 000009 2020/11/06 �ǉ��J�n
        Dim csRetLtRiyoTdkEntity As DataSet
        Dim csLtRiyoTdkRow As DataRow()
        Dim strFilter As String
        Dim strSort As String
        Dim cABAtenaKanriJohoB As ABAtenaKanriJohoBClass              ' �Ǘ����r�W�l�X�N���X
        Dim strKanriJoho As String
        Dim csHenkyakuFuyoGyomuCDList As List(Of String)              ' �ԋp�s�v�Ɩ�CD���X�g
        Dim strBreakKey As String
        Dim NewDataRow As DataRow
        '����ԍ� 000009 2020/11/06 �ǉ��I��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �p�����[�^�`�F�b�N
            If (csABLTRiyoTdkParaX.p_strJuminCD.Trim = String.Empty AndAlso _
                csABLTRiyoTdkParaX.p_strZeimokuCD = ABEnumDefine.ZeimokuCDType.Empty) Then
                ' �p�����[�^:�Z��CD�A�Ŗ�CD���ݒ肳��Ă��Ȃ��ꍇ�͈����G���[
                ' ���b�Z�[�W�w�K�{���ڂ����͂���Ă��܂���B�F�Z���R�[�h��ŖڃR�[�h�̂����ꂩ��ݒ肵�Ă��������B�x
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z���R�[�h��ŖڃR�[�h�̂����ꂩ��ݒ肵�Ă��������B", objErrorStruct.m_strErrorCode)
            Else
            End If

            '*����ԍ� 000009 2020/11/06 �ǉ��J�n
            If Not (csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "" OrElse _
                csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "1" OrElse _
                csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "2" OrElse _
                csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "3" OrElse _
                csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "4") Then
                ' �p�����[�^:���p�敪�����ݒ�A����"1"�`"4"�̂�����ł��Ȃ��ꍇ�͈����G���[
                ' ���b�Z�[�W�w���p�͏o���p�敪�x
                m_cfErrorClass = New UFErrorClass(THIS_BUSINESSID)
                '�G���[��`���擾
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001002)
                '��O�𐶐�
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "���p�͏o���p�敪", objErrorStruct.m_strErrorCode)
            End If
            '*����ԍ� 000009 2020/11/06 �ǉ��I��

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            ' SELECT��
            '*����ԍ� 000009 2020/11/06 �C���J�n
            'If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
            '    ' �o�͋敪��"1"�̏ꍇ�A�w�[�Ŏ�ID����p��ID�x�𒊏o
            '    strSQL.Append("SELECT ")
            '    strSQL.Append(ABLtRiyoTdkEntity.NOZEIID).Append(", ")
            '    strSQL.Append(ABLtRiyoTdkEntity.RIYOSHAID)
            'Else
            '    ' �o�͋敪��"1"�ȊO�̏ꍇ�A�S���ڒ��o
            '    strSQL.Append("SELECT * ")
            'End If
            ' �o�͋敪��"1"�ȊO�̏ꍇ�A�S���ڒ��o
            strSQL.Append("SELECT * ")
            '*����ԍ� 000009 2020/11/06 �C���I��

            strSQL.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME)

            ' WHERE��
            strSQL.Append(" WHERE ")

            ' �Z���R�[�h
            If (csABLTRiyoTdkParaX.p_strJuminCD.Trim <> String.Empty) Then
                ' �Z���R�[�h���ݒ肳��Ă���ꍇ
                strSQL.Append(ABLtRiyoTdkEntity.JUMINCD).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_JUMINCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_JUMINCD
                cfUFParameterClass.Value = csABLTRiyoTdkParaX.p_strJuminCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
                ' �Z���R�[�h���ݒ肳��Ă��Ȃ��ꍇ�A�����Z�b�g���Ȃ�
            End If

            ' �ŖڃR�[�h
            If (csABLTRiyoTdkParaX.p_strZeimokuCD <> ABEnumDefine.ZeimokuCDType.Empty) Then
                ' �ŖڃR�[�h���ݒ肳��Ă���ꍇ�A���o�����ɂ���
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.TAXKB).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_TAXKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(csABLTRiyoTdkParaX.p_strZeimokuCD)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If

            ' �p�~�t���O
            If (blnAndFg = True) Then
                ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                strSQL.Append(" AND ")
            End If

            If (csABLTRiyoTdkParaX.p_blnHaishiFG = False) Then
                ' �p�~�敪��"False"�̏ꍇ�A�p�~�敪���p�~�łȂ����̂��擾����
                '* AND (HAISHIFG <> '1' OR HAISHIFG <> '2') AND SAKUJOFG <> '1'
                strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '1' AND ")
                strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '2' AND ")
                strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
            Else
                '* AND SAKUJOFG <> '1'
                strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
            End If

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                  "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                  "�y���s���\�b�h��:GetDataSet�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            '*����ԍ� 000009 2020/11/06 �C���J�n
            'If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
            '    csLtRiyoTdkEntity = m_csDataSchma_Select.Clone()
            '    csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            'Else
            '    csLtRiyoTdkEntity = m_csDataSchma.Clone()
            '    csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            'End If
            ' ���̎��_�ł�csLtRiyoTdkEntity�͑S���ڂƂ���
            csLtRiyoTdkEntity = m_csDataSchma.Clone()
            csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)
            '*����ԍ� 000009 2020/11/06 �C���I��

            '*����ԍ� 000009 2020/11/06 �ǉ��J�n

            ' �Ǘ����r�W�l�X�N���X�̃C���X�^���X��
            cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            '�Ǘ����i10-46�j���擾
            strKanriJoho = cABAtenaKanriJohoB.GetHenkyakuFuyoGyomuCD_Param.Trim
            csHenkyakuFuyoGyomuCDList = New List(Of String)(strKanriJoho.Split(","c))

            ' ��U�D�揇�ʂ�t���ă\�[�g�����Ă����̑I�����鎖����N���[��csRetLtRiyoTdkEntity���쐬
            'csRetLtRiyoTdkEntity = csLtRiyoTdkEntity.Clone()

            If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
                ' �o�͋敪'1'�̏ꍇ�͔[�Ŏ�ID�Ɨ��p��ID�̂ݕԋp���邽�߁A2���ڂ݂̂Ƃ���
                csRetLtRiyoTdkEntity = m_csDataSchma_Select.Clone()
            Else
                csRetLtRiyoTdkEntity = m_csDataSchma.Clone()
            End If

            '�Ǘ����i10-46�j�ɊY������Ɩ�CD���ݒ肳��Ă��邩�ۂ��Ő�����s��
            If (csHenkyakuFuyoGyomuCDList.Contains(m_cfControlData.m_strBusinessId) = True) Then
                '�Y������Ɩ�CD���ݒ肳��Ă����ꍇ�i���ʔ[�ł͕ԋp�s�v�ƂȂ�j

                Select Case csABLTRiyoTdkParaX.p_strRiyoKB.Trim

                    Case "", "1"
                        '���ʁ��\�������ʔ[�ł̗D�揇�i�������A���ʔ[�ł͏��O����j
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '�\�������ʂ̗D�揇
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '���ʔ[�Ł����ʂ̗D�揇�i�������A���ʔ[�ł͏��O����j
                        strFilter = String.Format("{0}='{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "1")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '�i�荞�݂Ȃ��i�������A���ʔ[�ł͏��O����j
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Empty

                End Select

            Else
                '�Y������Ɩ�CD���ݒ肳��Ă��Ȃ��ꍇ

                Select Case csABLTRiyoTdkParaX.p_strRiyoKB.Trim

                    Case "", "1"
                        '���ʁ��\�������ʔ[�ł̗D�揇
                        strFilter = String.Empty
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '�\�������ʂ̗D�揇
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '���ʔ[�Ł����ʂ̗D�揇
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "2")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '�i�荞�݂Ȃ�
                        strFilter = String.Empty
                        strSort = String.Empty

                End Select

            End If

            csLtRiyoTdkRow = csLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Select(strFilter, strSort)

            ' csRetLtRiyoTdkEntity�ւ̃Z�b�g
            If (csLtRiyoTdkRow.Length > 0) Then
                '�擾������0���ȏ�̏ꍇ

                If (csABLTRiyoTdkParaX.p_strOutKB = "1") Then
                    ' �o�͋敪'1'�̏ꍇ�͔[�Ŏ�ID�Ɨ��p��ID�̂ݕԋp���邽�߁AcsRetLtRiyoTdkEntity�͂���2���ڂ̂݃Z�b�g����

                    If csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "4" Then
                        '�����F���p�敪��"4"�̏ꍇ�͑S���ԋp����B
                        For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                            NewDataRow = csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).NewRow()                     ' �ǉ�����f�[�^�e�[�u���̐V�K�s�Ƃ���
                            NewDataRow.Item(ABLtRiyoTdkEntity.NOZEIID) = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.NOZEIID)      ' �[�Ŏ�ID
                            NewDataRow.Item(ABLtRiyoTdkEntity.RIYOSHAID) = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.RIYOSHAID)  ' ���p��ID
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows.Add(NewDataRow)                      ' �ԋp�p�f�[�^�e�[�u���ɍs�ǉ�
                        Next
                    Else
                        '�����F���p�敪��"4"�̏ꍇ�́A�Z���R�[�h�A�Ŗڋ敪�A�p�~�t���O�̃u���C�N����1���ԋp����B
                        strBreakKey = ""

                        For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                            If strBreakKey <> csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString Then
                                NewDataRow = csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).NewRow()                     ' �ǉ�����f�[�^�e�[�u���̐V�K�s�Ƃ���
                                NewDataRow.Item(ABLtRiyoTdkEntity.NOZEIID) = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.NOZEIID)      ' �[�Ŏ�ID
                                NewDataRow.Item(ABLtRiyoTdkEntity.RIYOSHAID) = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.RIYOSHAID)  ' ���p��ID
                                csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Rows.Add(NewDataRow)                      ' �ԋp�p�f�[�^�e�[�u���ɍs�ǉ�
                                strBreakKey = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString
                            End If
                        Next

                    End If
                Else
                    ' �o�͋敪'1'�ȊO�̏ꍇ�͂��̂܂�IMPORT����B

                    If csABLTRiyoTdkParaX.p_strRiyoKB.Trim = "4" Then
                        '�����F���p�敪��"4"�̏ꍇ�͑S���ԋp����B
                        For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                        Next
                    Else
                        '�����F���p�敪��"4"�̏ꍇ�́A�Z���R�[�h�A�Ŗڋ敪�A�p�~�t���O�̃u���C�N����1���ԋp����B
                        strBreakKey = ""
                        For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                            If strBreakKey <> csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString Then
                                csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                                strBreakKey = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString
                            End If
                        Next
                    End If
                End If

            End If
            '*����ԍ� 000009 2020/11/06 �ǉ��I��

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

        '*����ԍ� 000009 2020/11/06 �ǉ��J�n
        'Return csLtRiyoTdkEntity
        Return csRetLtRiyoTdkEntity
        '*����ԍ� 000009 2020/11/06 �ǉ��I��

    End Function
#End Region

    '*����ԍ� 000002 2008/11/27 �ǉ��J�n
#Region "eLTAX���p�̓f�[�^�擾���\�b�h�Q"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX���p�̓f�[�^�擾���\�b�h�Q
    '* 
    '* �\��         Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass) As DataSet
    '* 
    '* �@�\�@�@     ���p�͏o�}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����         cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass   : ���p�͏o�p�����[�^�Q�N���X
    '* 
    '* �߂�l       �擾�������p�͏o�}�X�^�̊Y���f�[�^�iDataSet�j
    '*                 �\���FcsLtRiyoTdkEntity    
    '************************************************************************************************
    Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTRiyoTdkData"
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim objErrorStruct As UFErrorStruct                             ' �G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim csLtRiyoTdkEntity As DataSet                                ' ���p�͏o�}�X�^�f�[�^
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim cfUFParameterClass As UFParameterClass                      ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X
        Dim blnAndFg As Boolean = False                                 ' AND����t���O

        '����ԍ� 000009 2020/11/06 �ǉ��J�n
        Dim csRetLtRiyoTdkEntity As DataSet
        Dim csLtRiyoTdkRow As DataRow()
        Dim strFilter As String
        Dim strSort As String
        Dim cABAtenaKanriJohoB As ABAtenaKanriJohoBClass              ' �Ǘ����r�W�l�X�N���X
        Dim strKanriJoho As String
        Dim csHenkyakuFuyoGyomuCDList As List(Of String)              ' �ԋp�s�v�Ɩ�CD���X�g
        Dim strBreakKey As String
        '����ԍ� 000009 2020/11/06 �ǉ��I��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            ' SELECT��
            '*����ԍ� 000010 2024/01/09 �C���J�n
            'strSQL.Append("SELECT * ")
            strSQL.Append("SELECT ")
            strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".*")
            '*����ԍ� 000010 2024/01/09 �C���I��

            strSQL.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME)

            '*����ԍ� 000010 2024/01/09 �ǉ��J�n
            If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
                strSQL.Append(" INNER JOIN ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME)
                strSQL.Append(" ON ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.JUMINCD)
                strSQL.Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.JUMINCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.MYNUMBER)
                strSQL.Append(" = ")
                strSQL.Append(ABMyNumberEntity.PARAM_MYNUMBER)
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.CKINKB)
                strSQL.Append(" = ")
                strSQL.Append("'").Append(ABMyNumberEntity.DEFAULT.CKINKB.CKIN).Append("'")
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.SAKUJOFG)
                strSQL.Append(" <> '1'")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strMyNumber)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If
            '*����ԍ� 000010 2024/01/09 �ǉ��I��

            ' WHERE��
            strSQL.Append(" WHERE ")
            '---------------------------------------------------------------------------------
            ' �Ŗڋ敪
            If (cABLTRiyoTdkPara2X.p_strTaxKB.Trim <> String.Empty) Then
                ' �Ŗڋ敪���ݒ肳��Ă���ꍇ

                strSQL.Append(ABLtRiyoTdkEntity.TAXKB).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_TAXKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strTaxKB)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �[�Ŏ�ID
            If (cABLTRiyoTdkPara2X.p_strNozeiID.Trim <> String.Empty) Then
                ' �[�Ŏ�ID���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.NOZEIID).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_NOZEIID)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_NOZEIID
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strNozeiID

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' ���p��ID
            If (cABLTRiyoTdkPara2X.p_strRiyoshaID.Trim <> String.Empty) Then
                ' ���p��ID���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.RIYOSHAID).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_RIYOSHAID)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RIYOSHAID
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strRiyoshaID

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '*����ԍ� 000010 2024/01/09 �폜�J�n
            ''*����ԍ� 000007 2014/08/15 �ǉ��J�n
            ''---------------------------------------------------------------------------------
            '' �l�ԍ�
            ''If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
            ''    �Z���R�[�h���ݒ肳��Ă���ꍇ
            ''    If (blnAndFg = True) Then
            ''        AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
            ''        strSQL.Append(" AND ")
            ''    End If

            ''    strSQL.Append(ABLtRiyoTdkEntity.RESERVE1).Append(" = ")
            ''    strSQL.Append(ABLtRiyoTdkEntity.KEY_RESERVE1)

            ''    ���������̃p�����[�^���쐬
            ''    cfUFParameterClass = New UFParameterClass
            ''    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RESERVE1
            ''    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strMyNumber

            ''    ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            ''    cfUFParameterCollectionClass.Add(cfUFParameterClass)

            ''    AND����t���O���Z�b�g
            ''    blnAndFg = True
            ''Else
            ''End If
            ''*����ԍ� 000007 2014/08/15 �ǉ��I��
            '*����ԍ� 000010 2024/01/09 �폜�I��
            '---------------------------------------------------------------------------------
            ' �Z���R�[�h
            If (cABLTRiyoTdkPara2X.p_strJuminCD.Trim <> String.Empty) Then
                ' �Z���R�[�h���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.JUMINCD).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_JUMINCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_JUMINCD
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strJuminCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' ��t�ԍ�
            If (cABLTRiyoTdkPara2X.p_strRcptNO.Trim <> String.Empty) Then
                ' ��t�ԍ����ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.RCPTNO).Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_RCPTNO)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTNO
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptNO)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' ��t��
            If (cABLTRiyoTdkPara2X.p_strRcptYMD_From.Trim <> String.Empty AndAlso _
                cABLTRiyoTdkPara2X.p_strRcptYMD_To.Trim <> String.Empty) Then
                ' ��t�����ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" >= ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "1")

                strSQL.Append(" AND ")

                strSQL.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" <= ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "2")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "1"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptYMD_From).RPadRight(17, "0"c)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "2"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptYMD_To).RPadRight(17, "9"c)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            '*����ԍ� 000003 2009/07/27 �ǉ��J�n
            ' ������
            If (cABLTRiyoTdkPara2X.p_strShoriYMD_From.Trim <> String.Empty AndAlso _
                cABLTRiyoTdkPara2X.p_strShoriYMD_To.Trim <> String.Empty) Then
                ' ���������ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                strSQL.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" >= ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1")

                strSQL.Append(" AND ")

                strSQL.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" <= ")
                strSQL.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strShoriYMD_From).RPadRight(17, "0"c)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strShoriYMD_To).RPadRight(17, "9"c)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �J�i�E��������
            ' �J�i����
            If Not (cABLTRiyoTdkPara2X.p_strKanaMeisho.Trim = String.Empty) Then
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                If (cABLTRiyoTdkPara2X.p_strKanaMeisho.RIndexOf("%") = -1) Then
                    '*����ԍ� 000004 2009/11/16 �C���J�n
                    strSQL.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO)
                    'strSQL.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                    '*����ԍ� 000004 2009/11/16 �C���I��
                    strSQL.Append(" = ")
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho
                Else
                    '*����ԍ� 000004 2009/11/16 �C���J�n
                    strSQL.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO)
                    'strSQL.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                    '*����ԍ� 000004 2009/11/16 �C���I��
                    strSQL.Append(" LIKE ")
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho.TrimEnd
                End If
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            End If

            '�����p��������
            If Not (cABLTRiyoTdkPara2X.p_strKanjiMeisho.Trim = String.Empty) Then
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                If (cABLTRiyoTdkPara2X.p_strKanjiMeisho.RIndexOf("%") = -1) Then
                    strSQL.Append(ABLtRiyoTdkEntity.KANJIMEISHO)
                    strSQL.Append(" = ")
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho
                Else
                    strSQL.Append(ABLtRiyoTdkEntity.KANJIMEISHO)
                    strSQL.Append(" LIKE ")
                    strSQL.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho.TrimEnd

                End If
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            End If
            '*����ԍ� 000003 2009/07/27 �ǉ��I��
            '---------------------------------------------------------------------------------
            ' �p�~�t���O
            If (cABLTRiyoTdkPara2X.p_strHaishiFG.Trim <> String.Empty) Then
                ' �p�~�t���O���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strSQL.Append(" AND ")
                End If

                Select Case cABLTRiyoTdkPara2X.p_strHaishiFG
                    Case "0"    ' �L���̂�
                        strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '1' AND ")
                        strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '2'")

                    Case "1"    ' �p�~�̂�
                        strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '1'")

                    Case "2"    ' �Ŗڍ폜�̂�
                        strSQL.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '2'")
                    Case Else
                End Select

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �폜�t���O
            If (blnAndFg = True) Then
                ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                strSQL.Append(" AND ")
                '*����ԍ� 000010 2024/01/09 �C���J�n
                'strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                '*����ԍ� 000010 2024/01/09 �C���I��

            Else
                '*����ԍ� 000010 2024/01/09 �C���J�n
                'strSQL.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                '*����ԍ� 000010 2024/01/09 �C���I��
            End If
            '---------------------------------------------------------------------------------
            ' �ő�擾����
            If (cABLTRiyoTdkPara2X.p_intGetCountMax <> 0) Then
                m_cfRdbClass.p_intMaxRows = cABLTRiyoTdkPara2X.p_intGetCountMax
            Else
            End If

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                  "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                  "�y���s���\�b�h��:GetDataSet�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csLtRiyoTdkEntity = m_csDataSchma.Clone()
            csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            '*����ԍ� 000009 2020/11/06 �ǉ��J�n

            ' �Ǘ����r�W�l�X�N���X�̃C���X�^���X��
            cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            '�Ǘ����i10-46�j���擾
            strKanriJoho = cABAtenaKanriJohoB.GetHenkyakuFuyoGyomuCD_Param.Trim
            csHenkyakuFuyoGyomuCDList = New List(Of String)(strKanriJoho.Split(","c))

            csRetLtRiyoTdkEntity = csLtRiyoTdkEntity.Clone()

            If (csHenkyakuFuyoGyomuCDList.Contains(m_cfControlData.m_strBusinessId) = True) Then
                '�Ǘ����i10-46�j�ɊY������Ɩ�CD���ݒ肳��Ă����ꍇ�͋��ʔ[�ł͕s�v

                Select Case cABLTRiyoTdkPara2X.p_strRiyoKB.Trim

                    Case "", "1"
                        '���ʁ��\�������ʔ[�ł̗D�揇�i�������A���ʔ[�ł͏��O����j
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '�\�������ʂ̗D�揇
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '���ʔ[�Ł����ʂ̗D�揇�i�������A���ʔ[�ł͏��O����j
                        strFilter = String.Format("{0}='{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "1")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '�i�荞�݂Ȃ��i�������A���ʔ[�ł͏��O����j
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Empty

                End Select

            Else
                '�Ǘ����i10-46�j�ɊY������Ɩ�CD���ݒ肳��Ă��Ȃ��ꍇ

                Select Case cABLTRiyoTdkPara2X.p_strRiyoKB.Trim

                    Case "", "1"
                        '���ʁ��\�������ʔ[�ł̗D�揇
                        strFilter = String.Empty
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '�\�������ʂ̗D�揇
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '���ʔ[�Ł����ʂ̗D�揇
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "2")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '�i�荞�݂Ȃ�
                        strFilter = String.Empty
                        strSort = String.Empty

                End Select

            End If

            csLtRiyoTdkRow = csLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Select(strFilter, strSort)

            If (csLtRiyoTdkRow.Length > 0) Then
                '�擾������0���ȏ�̏ꍇ
                If cABLTRiyoTdkPara2X.p_strRiyoKB.Trim = "4" Then
                    '�����F���p�敪��"4"�̏ꍇ�͑S���ԋp����B
                    For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                        csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                    Next
                Else
                    'csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(0))
                    '�����F���p�敪��"4"�̏ꍇ�́A�Z���R�[�h�A�Ŗڋ敪�A�p�~�t���O�̃u���C�N����1���ԋp����B
                    strBreakKey = ""
                    For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                        If strBreakKey <> csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString Then
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                            strBreakKey = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString
                        End If
                    Next
                End If
            End If
            '*����ԍ� 000009 2020/11/06 �ǉ��I��

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

        '*����ԍ� 000009 2020/11/06 �ǉ��J�n
        'Return csLtRiyoTdkEntity
        Return csRetLtRiyoTdkEntity
        '*����ԍ� 000009 2020/11/06 �ǉ��I��

    End Function
#End Region
    '*����ԍ� 000002 2008/11/27 �ǉ��I��

    '*����ԍ� 000003 2009/07/27 �ǉ��J�n
#Region "eLTAX���p�̓f�[�^�擾���\�b�h�R"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX���p�̓f�[�^�擾���\�b�h�R
    '* 
    '* �\��         Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass, _
    '*                                                         ByRef intAllCount As Integer) As DataSet
    '* 
    '* �@�\�@�@     ���p�͏o�}�X�^���Y���f�[�^���擾����B
    '* 
    '* ����         cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass    : ���p�͏o�p�����[�^�Q�N���X
    '*              intAllCount As Integer                          : �S�f�[�^����
    '* 
    '* �߂�l       �擾�������p�͏o�}�X�^�̊Y���f�[�^�iDataSet�j
    '*                 �\���FcsLtRiyoTdkEntity    
    '************************************************************************************************
    Public Overloads Function GetLTRiyoTdkData(ByVal cABLTRiyoTdkPara2X As ABLTRiyoTdkPara2XClass, _
                                               ByRef intAllCount As Integer) As DataSet
        Const THIS_METHOD_NAME As String = "GetLTRiyoTdkData"
        Const COL_COUNT As String = "COUNT"
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim objErrorStruct As UFErrorStruct                             ' �G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim csLtRiyoTdkEntity As DataSet                                ' ���p�͏o�}�X�^�f�[�^
        Dim csLtRiyoTdk_AllCount As DataSet                             ' ���p�͏o�}�X�^�S���擾�f�[�^
        Dim strSQL As New StringBuilder                                 ' SQL��������
        Dim strSQL_Conut As New StringBuilder                           ' �S�����o
        Dim strWhere As New StringBuilder                               ' WHERE��������
        Dim cfUFParameterClass As UFParameterClass                      ' �p�����[�^�N���X
        Dim cfUFParameterCollectionClass As UFParameterCollectionClass  ' �p�����[�^�R���N�V�����N���X
        Dim blnAndFg As Boolean = False                                 ' AND����t���O
        '*����ԍ� 000007 2014/08/15 �ǉ��J�n
        Dim strSQLMyNumber As New StringBuilder                         ' ���ʔԍ�SQL
        '*����ԍ� 000007 2014/08/15 �ǉ��I��

        '����ԍ� 000009 2020/11/06 �ǉ��J�n
        Dim csRetLtRiyoTdkEntity As DataSet
        Dim csLtRiyoTdkRow As DataRow()
        Dim strFilter As String
        Dim strSort As String
        Dim cABAtenaKanriJohoB As ABAtenaKanriJohoBClass              ' �Ǘ����r�W�l�X�N���X
        Dim strKanriJoho As String
        Dim csHenkyakuFuyoGyomuCDList As List(Of String)              ' �ԋp�s�v�Ɩ�CD���X�g
        Dim strBreakKey As String
        '����ԍ� 000009 2020/11/06 �ǉ��I��

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g���쐬
            cfUFParameterCollectionClass = New UFParameterCollectionClass

            ' SQL���̍쐬
            ' SELECT��
            '*����ԍ� 000010 2024/01/09 �C���J�n
            'strSQL.Append("SELECT * ")
            strSQL.Append("SELECT ")
            strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".*")
            '*����ԍ� 000010 2024/01/09 �C���I��
            strSQL.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME)

            strSQL_Conut.Append("SELECT COUNT(*) AS ").Append(COL_COUNT)
            strSQL_Conut.Append(" FROM ").Append(ABLtRiyoTdkEntity.TABLE_NAME)

            '*����ԍ� 000010 2024/01/09 �ǉ��J�n
            If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
                strSQL.Append(" INNER JOIN ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME)
                strSQL.Append(" ON ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.JUMINCD)
                strSQL.Append(" = ")
                strSQL.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.JUMINCD)
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.MYNUMBER)
                strSQL.Append(" = ")
                strSQL.Append(ABMyNumberEntity.PARAM_MYNUMBER)
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.CKINKB)
                strSQL.Append(" = ")
                strSQL.Append("'").Append(ABMyNumberEntity.DEFAULT.CKINKB.CKIN).Append("'")
                strSQL.Append(" AND ")
                strSQL.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.SAKUJOFG)
                strSQL.Append(" <> '1' ")

                strSQL_Conut.Append(" INNER JOIN ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME)
                strSQL_Conut.Append(" ON ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.JUMINCD)
                strSQL_Conut.Append(" = ")
                strSQL_Conut.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.JUMINCD)
                strSQL_Conut.Append(" AND ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.MYNUMBER)
                strSQL_Conut.Append(" = ")
                strSQL_Conut.Append(ABMyNumberEntity.PARAM_MYNUMBER)
                strSQL_Conut.Append(" AND ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.CKINKB)
                strSQL_Conut.Append(" = ")
                strSQL_Conut.Append("'").Append(ABMyNumberEntity.DEFAULT.CKINKB.CKIN).Append("'")
                strSQL_Conut.Append(" AND ")
                strSQL_Conut.Append(ABMyNumberEntity.TABLE_NAME).Append(".").Append(ABMyNumberEntity.SAKUJOFG)
                strSQL_Conut.Append(" <> '1' ")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABMyNumberEntity.PARAM_MYNUMBER
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strMyNumber)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)
            Else
            End If
            '*����ԍ� 000010 2024/01/09 �ǉ��I��

            ' WHERE��
            strWhere.Append(" WHERE ")
            '---------------------------------------------------------------------------------
            ' �Ŗڋ敪
            If (cABLTRiyoTdkPara2X.p_strTaxKB.Trim <> String.Empty) Then
                ' �Ŗڋ敪���ݒ肳��Ă���ꍇ

                strWhere.Append(ABLtRiyoTdkEntity.TAXKB).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_TAXKB)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_TAXKB
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strTaxKB)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �[�Ŏ�ID
            If (cABLTRiyoTdkPara2X.p_strNozeiID.Trim <> String.Empty) Then
                ' �[�Ŏ�ID���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.NOZEIID).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_NOZEIID)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_NOZEIID
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strNozeiID

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' ���p��ID
            If (cABLTRiyoTdkPara2X.p_strRiyoshaID.Trim <> String.Empty) Then
                ' ���p��ID���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.RIYOSHAID).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_RIYOSHAID)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RIYOSHAID
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strRiyoshaID

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '*����ԍ� 000010 2024/01/09 �폜�J�n
            ''*����ԍ� 000007 2014/08/15 �ǉ��J�n
            ''---------------------------------------------------------------------------------
            '' �l�ԍ�
            'If (cABLTRiyoTdkPara2X.p_strMyNumber.Trim <> String.Empty) Then
            '    '*����ԍ� 000007 2014/08/15 �C���J�n
            '    '' �Z���R�[�h���ݒ肳��Ă���ꍇ
            '    'If (blnAndFg = True) Then
            '    '    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
            '    '    strSQL.Append(" AND ")
            '    'End If

            '    'strSQL.Append(ABLtRiyoTdkEntity.RESERVE1).Append(" = ")
            '    'strSQL.Append(ABLtRiyoTdkEntity.KEY_RESERVE1)
            '    ' �l�ԍ����ݒ肳��Ă���ꍇ
            '    If (blnAndFg = True) Then
            '        ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
            '        strWhere.Append(" AND ")
            '    End If

            '    strWhere.Append(ABLtRiyoTdkEntity.RESERVE1).Append(" = ")
            '    strWhere.Append(ABLtRiyoTdkEntity.KEY_RESERVE1)
            '    '*����ԍ� 000007 2014/08/15 �C���I��

            '    ' ���������̃p�����[�^���쐬
            '    cfUFParameterClass = New UFParameterClass
            '    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RESERVE1
            '    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strMyNumber

            '    ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
            '    cfUFParameterCollectionClass.Add(cfUFParameterClass)

            '    ' AND����t���O���Z�b�g
            '    blnAndFg = True
            'Else
            'End If
            ''*����ԍ� 000007 2014/08/15 �ǉ��I��
            '*����ԍ� 000010 2024/01/09 �폜�I��
            '---------------------------------------------------------------------------------
            ' �Z���R�[�h
            If (cABLTRiyoTdkPara2X.p_strJuminCD.Trim <> String.Empty) Then
                ' �Z���R�[�h���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.JUMINCD).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_JUMINCD)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_JUMINCD
                cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strJuminCD

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' ��t�ԍ�
            If (cABLTRiyoTdkPara2X.p_strRcptNO.Trim <> String.Empty) Then
                ' ��t�ԍ����ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.RCPTNO).Append(" = ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_RCPTNO)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTNO
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptNO)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' ��t��
            If (cABLTRiyoTdkPara2X.p_strRcptYMD_From.Trim <> String.Empty AndAlso _
                cABLTRiyoTdkPara2X.p_strRcptYMD_To.Trim <> String.Empty) Then
                ' ��t�����ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" >= ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "1")

                strWhere.Append(" AND ")

                strWhere.Append(ABLtRiyoTdkEntity.RCPTYMD).Append(" <= ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_RCPTYMD + "2")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "1"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptYMD_From).RPadRight(17, "0"c)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_RCPTYMD + "2"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strRcptYMD_To).RPadRight(17, "9"c)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            '*����ԍ� 000003 2009/07/27 �ǉ��J�n
            ' ������
            If (cABLTRiyoTdkPara2X.p_strShoriYMD_From.Trim <> String.Empty AndAlso _
                cABLTRiyoTdkPara2X.p_strShoriYMD_To.Trim <> String.Empty) Then
                ' ���������ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strWhere.Append(" AND ")
                End If

                strWhere.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" >= ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1")

                strWhere.Append(" AND ")

                strWhere.Append(ABLtRiyoTdkEntity.KOSHINNICHIJI).Append(" <= ")
                strWhere.Append(ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2")

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "1"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strShoriYMD_From).RPadRight(17, "0"c)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' ���������̃p�����[�^���쐬
                cfUFParameterClass = New UFParameterClass
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KOSHINNICHIJI + "2"
                cfUFParameterClass.Value = CStr(cABLTRiyoTdkPara2X.p_strShoriYMD_To).RPadRight(17, "9"c)

                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �J�i�E��������
            ' �J�i����
            If Not (cABLTRiyoTdkPara2X.p_strKanaMeisho.Trim = String.Empty) Then
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strWhere.Append(" AND ")
                End If

                If (cABLTRiyoTdkPara2X.p_strKanaMeisho.RIndexOf("%") = -1) Then
                    '*����ԍ� 000004 2009/11/16 �C���J�n
                    strWhere.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO)
                    'strWhere.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                    '*����ԍ� 000004 2009/11/16 �C���I��
                    strWhere.Append(" = ")
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho
                Else
                    '*����ԍ� 000004 2009/11/16 �C���J�n
                    strWhere.Append(ABLtRiyoTdkEntity.SEARCHKANAMEISHO)
                    'strWhere.Append(ABLtRiyoTdkEntity.KANAMEISHO)
                    '*����ԍ� 000004 2009/11/16 �C���I��
                    strWhere.Append(" LIKE ")
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KANAMEISHO)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANAMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanaMeisho.TrimEnd
                End If
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            End If

            '�����p��������
            If Not (cABLTRiyoTdkPara2X.p_strKanjiMeisho.Trim = String.Empty) Then
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strWhere.Append(" AND ")
                End If

                If (cABLTRiyoTdkPara2X.p_strKanjiMeisho.RIndexOf("%") = -1) Then
                    strWhere.Append(ABLtRiyoTdkEntity.KANJIMEISHO)
                    strWhere.Append(" = ")
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho
                Else
                    strWhere.Append(ABLtRiyoTdkEntity.KANJIMEISHO)
                    strWhere.Append(" LIKE ")
                    strWhere.Append(ABLtRiyoTdkEntity.KEY_KANJIMEISHO)

                    ' ���������̃p�����[�^���쐬
                    cfUFParameterClass = New UFParameterClass
                    cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.KEY_KANJIMEISHO
                    cfUFParameterClass.Value = cABLTRiyoTdkPara2X.p_strKanjiMeisho.TrimEnd

                End If
                ' ���������̃p�����[�^�R���N�V�����I�u�W�F�N�g�Ƀp�����[�^�I�u�W�F�N�g�̒ǉ�
                cfUFParameterCollectionClass.Add(cfUFParameterClass)

                ' AND����t���O���Z�b�g
                blnAndFg = True
            End If
            '*����ԍ� 000003 2009/07/27 �ǉ��I��
            '---------------------------------------------------------------------------------
            ' �p�~�t���O
            If (cABLTRiyoTdkPara2X.p_strHaishiFG.Trim <> String.Empty) Then
                ' �p�~�t���O���ݒ肳��Ă���ꍇ
                If (blnAndFg = True) Then
                    ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                    strWhere.Append(" AND ")
                End If

                Select Case cABLTRiyoTdkPara2X.p_strHaishiFG
                    Case "0"    ' �L���̂�
                        strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '1' AND ")
                        strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" <> '2'")

                    Case "1"    ' �p�~�̂�
                        strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '1'")

                    Case "2"    ' �Ŗڍ폜�̂�
                        strWhere.Append(ABLtRiyoTdkEntity.HAISHIFG).Append(" = '2'")
                    Case Else
                End Select

                ' AND����t���O���Z�b�g
                blnAndFg = True
            Else
            End If
            '---------------------------------------------------------------------------------
            ' �폜�t���O
            If (blnAndFg = True) Then
                ' AND����t���O��"True"�̏ꍇ�AAND����Z�b�g
                strWhere.Append(" AND ")
                '*����ԍ� 000010 2024/01/09 �C���J�n
                'strWhere.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                strWhere.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                '*����ԍ� 000010 2024/01/09 �C���I��
            Else
                '*����ԍ� 000010 2024/01/09 �C���J�n
                'strWhere.Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                strWhere.Append(ABLtRiyoTdkEntity.TABLE_NAME).Append(".").Append(ABLtRiyoTdkEntity.SAKUJOFG).Append(" <> '1'")
                '*����ԍ� 000010 2024/01/09 �C���I��
            End If
            '---------------------------------------------------------------------------------
            ' �ő�擾����
            If (cABLTRiyoTdkPara2X.p_intGetCountMax <> 0) Then
                m_cfRdbClass.p_intMaxRows = cABLTRiyoTdkPara2X.p_intGetCountMax
            Else
            End If

            ' SQL����������
            strSQL.Append(strWhere.ToString)
            strSQL_Conut.Append(strWhere.ToString)

            ' �S���擾����
            csLtRiyoTdk_AllCount = m_cfRdbClass.GetDataSet(strSQL_Conut.ToString, cfUFParameterCollectionClass)

            intAllCount = CInt(csLtRiyoTdk_AllCount.Tables(0).Rows(0)(COL_COUNT))

            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                 "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                  "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                  "�y���s���\�b�h��:GetDataSet�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(strSQL.ToString, cfUFParameterCollectionClass) + "�z")

            ' SQL�̎��s DataSet�̎擾
            csLtRiyoTdkEntity = m_csDataSchma.Clone()
            csLtRiyoTdkEntity = m_cfRdbClass.GetDataSet(strSQL.ToString, csLtRiyoTdkEntity, ABLtRiyoTdkEntity.TABLE_NAME, cfUFParameterCollectionClass, False)

            '*����ԍ� 000009 2020/11/06 �ǉ��J�n

            ' �Ǘ����r�W�l�X�N���X�̃C���X�^���X��
            cABAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigDataClass, m_cfRdbClass)

            '�Ǘ����i10-46�j���擾
            strKanriJoho = cABAtenaKanriJohoB.GetHenkyakuFuyoGyomuCD_Param.Trim
            csHenkyakuFuyoGyomuCDList = New List(Of String)(strKanriJoho.Split(","c))

            csRetLtRiyoTdkEntity = csLtRiyoTdkEntity.Clone()

            If (csHenkyakuFuyoGyomuCDList.Contains(m_cfControlData.m_strBusinessId) = True) Then
                '�Ǘ����i10-46�j�ɊY������Ɩ�CD���ݒ肳��Ă����ꍇ�͋��ʔ[�ł͕s�v

                Select Case cABLTRiyoTdkPara2X.p_strRiyoKB.Trim

                    Case "", "1"
                        '���ʁ��\�������ʔ[�ł̗D�揇�i�������A���ʔ[�ł͏��O����j
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '�\�������ʂ̗D�揇
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '���ʔ[�Ł����ʂ̗D�揇�i�������A���ʔ[�ł͏��O����j
                        strFilter = String.Format("{0}='{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "1")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '�i�荞�݂Ȃ��i�������A���ʔ[�ł͏��O����j
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Empty

                End Select

            Else
                '�Ǘ����i10-46�j�ɊY������Ɩ�CD���ݒ肳��Ă��Ȃ��ꍇ

                Select Case cABLTRiyoTdkPara2X.p_strRiyoKB.Trim

                    Case "", "1"
                        '���ʁ��\�������ʔ[�ł̗D�揇
                        strFilter = String.Empty
                        strSort = String.Format("{0},{1},{2},{3}", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "2"
                        '�\�������ʂ̗D�揇
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "3")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "3"
                        '���ʔ[�Ł����ʂ̗D�揇
                        strFilter = String.Format("{0}<>'{1}'", ABLtRiyoTdkEntity.RESERVE2.Trim, "2")
                        strSort = String.Format("{0},{1},{2},{3} DESC", ABLtRiyoTdkEntity.JUMINCD, ABLtRiyoTdkEntity.TAXKB, ABLtRiyoTdkEntity.HAISHIFG, ABLtRiyoTdkEntity.RESERVE2.Trim)

                    Case "4"
                        '�i�荞�݂Ȃ�
                        strFilter = String.Empty
                        strSort = String.Empty

                End Select

            End If

            csLtRiyoTdkRow = csLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).Select(strFilter, strSort)

            If (csLtRiyoTdkRow.Length > 0) Then
                '�擾������0���ȏ�̏ꍇ
                If cABLTRiyoTdkPara2X.p_strRiyoKB.Trim = "4" Then
                    '�����F���p�敪��"4"�̏ꍇ�͑S���ԋp����B
                    For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                        csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                    Next
                Else
                    'csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(0))
                    '�����F���p�敪��"4"�̏ꍇ�́A�Z���R�[�h�A�Ŗڋ敪�A�p�~�t���O�̃u���C�N����1���ԋp����B
                    strBreakKey = ""
                    For i As Integer = 0 To csLtRiyoTdkRow.Length - 1
                        If strBreakKey <> csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString Then
                            csRetLtRiyoTdkEntity.Tables(ABLtRiyoTdkEntity.TABLE_NAME).ImportRow(csLtRiyoTdkRow(i))
                            strBreakKey = csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.JUMINCD).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.TAXKB).ToString & csLtRiyoTdkRow(i).Item(ABLtRiyoTdkEntity.HAISHIFG).ToString
                        End If
                    Next
                End If
            End If
            '*����ԍ� 000009 2020/11/06 �ǉ��I��

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

        '*����ԍ� 000009 2020/11/06 �ǉ��J�n
        'Return csLtRiyoTdkEntity
        Return csRetLtRiyoTdkEntity
        '*����ԍ� 000009 2020/11/06 �ǉ��I��

    End Function
#End Region
    '*����ԍ� 000003 2009/07/27 �ǉ��I��

    '*����ԍ� 000001 2008/11/18 �ǉ��J�n
#Region "eLTAX���p�̓f�[�^�ǉ����\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX���p�̓f�[�^�ǉ����\�b�h
    '* 
    '* �\��         Public Function InsertLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     ���p�͏o�}�X�^�ɐV�K�f�[�^��ǉ�����B
    '* 
    '* ����         csDataRow As DataRow   : ���p�̓f�[�^(ABeLTAXRiyoTdk)
    '* 
    '* �߂�l       �ǉ�����(Integer)
    '************************************************************************************************
    Public Function InsertLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "InsertLTRiyoTdk"
        Dim cfParam As UFParameterClass                                 ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn                                  ' �f�[�^�J����
        '* corresponds to VS2008 End 2010/04/16 000006
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
            csDataRow(ABLtRiyoTdkEntity.TANMATSUID) = m_cfControlData.m_strClientId             ' �[���h�c
            csDataRow(ABLtRiyoTdkEntity.SAKUJOFG) = "0"                                         ' �폜�t���O
            csDataRow(ABLtRiyoTdkEntity.KOSHINCOUNTER) = Decimal.Zero                           ' �X�V�J�E���^
            csDataRow(ABLtRiyoTdkEntity.SAKUSEINICHIJI) = strUpdateDateTime                     ' �쐬����
            csDataRow(ABLtRiyoTdkEntity.SAKUSEIUSER) = m_cfControlData.m_strUserId              ' �쐬���[�U�[
            csDataRow(ABLtRiyoTdkEntity.KOSHINNICHIJI) = strUpdateDateTime                      ' �X�V����
            csDataRow(ABLtRiyoTdkEntity.KOSHINUSER) = m_cfControlData.m_strUserId               ' �X�V���[�U�[


            ' �p�����[�^�R���N�V�����֒l�̐ݒ�
            For Each cfParam In m_cfInsertUFParameterCollectionClass
                m_cfInsertUFParameterCollectionClass(cfParam.ParameterName).Value _
                        = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PARAM_PLACEHOLDER.RLength)).ToString()
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

#Region "eLTAX���p�̓f�[�^�X�V���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX���p�̓f�[�^�X�V���\�b�h
    '* 
    '* �\��         Public Function UpdateLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     ���p�͏o�}�X�^�̃f�[�^���X�V����B
    '* 
    '* ����         csDataRow As DataRow   : ���p�̓f�[�^(ABeLTAXRiyoTdk)
    '* 
    '* �߂�l       �X�V����(Integer)
    '************************************************************************************************
    Public Function UpdateLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "UpdateLTRiyoTdk"
        Dim cfParam As UFParameterClass                         ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn                          ' �f�[�^�J����
        '* corresponds to VS2008 End 2010/04/16 000006
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
            csDataRow(ABLtRiyoTdkEntity.TANMATSUID) = m_cfControlData.m_strClientId                                 ' �[���h�c
            csDataRow(ABLtRiyoTdkEntity.KOSHINCOUNTER) = CDec(csDataRow(ABLtRiyoTdkEntity.KOSHINCOUNTER)) + 1       ' �X�V�J�E���^
            csDataRow(ABLtRiyoTdkEntity.KOSHINNICHIJI) = m_cfRdbClass.GetSystemDate.ToString("yyyyMMddHHmmssfff")   ' �X�V����
            csDataRow(ABLtRiyoTdkEntity.KOSHINUSER) = m_cfControlData.m_strUserId                                   ' �X�V���[�U�[

            ' �쐬�ς݂̃p�����[�^�֍X�V�s����l��ݒ肷��B
            For Each cfParam In m_cfUpdateUFParameterCollectionClass
                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABLtRiyoTdkEntity.PREFIX_KEY.RLength) = ABLtRiyoTdkEntity.PREFIX_KEY) Then
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value =
                            csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PREFIX_KEY.RLength),
                                      DataRowVersion.Original).ToString()
                Else
                    ' �p�����[�^�R���N�V�����֒l�̐ݒ�
                    m_cfUpdateUFParameterCollectionClass(cfParam.ParameterName).Value = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PARAM_PLACEHOLDER.RLength), DataRowVersion.Current).ToString()
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
            m_strInsertSQL = "INSERT INTO " + ABLtRiyoTdkEntity.TABLE_NAME + " "
            strInsertColumn = ""
            strInsertParam = ""

            ' UPDATE SQL���̍쐬
            m_strUpDateSQL = "UPDATE " + ABLtRiyoTdkEntity.TABLE_NAME + " SET "

            ' UPDATE Where���쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLtRiyoTdkEntity.NOZEIID)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.RCPTSHICHOSONCD)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.TAXKB)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER)

            ' SELECT �p�����[�^�R���N�V�����N���X�̃C���X�^���X��
            m_cfInsertUFParameterCollectionClass = New UFParameterCollectionClass

            ' UPDATE �p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfUpdateUFParameterCollectionClass = New UFParameterCollectionClass

            ' �p�����[�^�R���N�V�����̍쐬
            For Each csDataColumn In csDataRow.Table.Columns
                cfUFParameterClass = New UFParameterClass

                ' INSERT SQL���̍쐬
                strInsertColumn += csDataColumn.ColumnName + ", "
                strInsertParam += ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' UPDATE SQL���̍쐬
                m_strUpDateSQL += csDataColumn.ColumnName + " = " + ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName + ", "

                ' INSERT �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
                m_cfInsertUFParameterCollectionClass.Add(cfUFParameterClass)

                ' UPDATE �R���N�V�����Ƀp�����[�^��ǉ�
                cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PARAM_PLACEHOLDER + csDataColumn.ColumnName
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
            ' �[�Ŏ�ID
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��t�s��������
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ŗڋ敪
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB
            m_cfUpdateUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER
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
    '*����ԍ� 000001 2008/11/18 �ǉ��I��

    '*����ԍ� 000005 2010/02/22 �ǉ��J�n
#Region "eLTAX���p�̓f�[�^�폜(����)���\�b�h"
    '************************************************************************************************
    '* ���\�b�h��   eLTAX���p�̓f�[�^�폜(����)���\�b�h
    '* 
    '* �\��         Public Function DeleteLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
    '* 
    '* �@�\�@�@     ���p�͏o�}�X�^����Y���f�[�^�𕨗��폜����B
    '* 
    '* ����         csDataRow As DataRow   : ���p�̓f�[�^(ABeLTAXRiyoTdk)
    '* 
    '* �߂�l       �폜����(Integer)
    '************************************************************************************************
    Public Function DeleteLTRiyoTdk(ByVal csDataRow As DataRow) As Integer
        Const THIS_METHOD_NAME As String = "DeleteLTRiyoTdk"
        Dim cfParam As UFParameterClass                                 ' �p�����[�^�N���X
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim csDataColumn As DataColumn                                  ' �f�[�^�J����
        '* corresponds to VS2008 End 2010/04/16 000006
        Dim intDelCnt As Integer                                        ' �폜����
        '* corresponds to VS2008 Start 2010/04/16 000006
        'Dim strUpdateDateTime As String                                 ' �V�X�e�����t
        '* corresponds to VS2008 End 2010/04/16 000006

        Try
            ' �f�o�b�O���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �폜�p�̃p�����[�^�tDELETE��������ƃp�����[�^�R���N�V�������쐬����
            If ((m_strDeleteSQL Is Nothing) OrElse (m_strDeleteSQL = String.Empty) OrElse _
                (IsNothing(m_cfDeleteUFParameterCollectionClass))) Then
                Call CreateSQL_Delete(csDataRow)
            Else
            End If

            ' �쐬�ς݂̃p�����[�^�֍폜�s����l��ݒ肷��B
            For Each cfParam In m_cfDeleteUFParameterCollectionClass

                ' �L�[���ڂ͍X�V�O�̒l�Őݒ�
                If (cfParam.ParameterName.RSubstring(0, ABLtRiyoTdkEntity.PREFIX_KEY.RLength) = ABLtRiyoTdkEntity.PREFIX_KEY) Then
                    m_cfDeleteUFParameterCollectionClass(cfParam.ParameterName).Value _
                            = csDataRow(cfParam.ParameterName.RSubstring(ABLtRiyoTdkEntity.PREFIX_KEY.RLength), DataRowVersion.Original).ToString()
                End If
            Next cfParam


            ' RDB�A�N�Z�X���O�o��
            m_cfLogClass.RdbWrite(m_cfControlData, _
                                  "�y�N���X��:" + Me.GetType.Name + "�z" + _
                                  "�y���\�b�h��:" + System.Reflection.MethodBase.GetCurrentMethod.Name + "�z" + _
                                  "�y���s���\�b�h��:ExecuteSQL�z" + _
                                  "�ySQL���e:" + m_cfRdbClass.GetDevelopmentSQLString(m_strDeleteSQL, m_cfDeleteUFParameterCollectionClass) + "�z")

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

        Return intDelCnt

    End Function
#End Region

#Region "SQL���쐬(�����폜)"
    '************************************************************************************************
    '* ���\�b�h��     �����폜�pSQL���̍쐬
    '* 
    '* �\��           Private Sub CreateSQL_Delete(ByVal csDataRow As DataRow)
    '* 
    '* �@�\           ����DELETE�p��SQL���쐬�A�p�����[�^�R���N�V�������쐬����
    '* 
    '* ����           csDataRow As DataRow : �X�V�Ώۂ̍s
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub CreateSQL_Delete(ByVal csDataRow As DataRow)
        Const THIS_METHOD_NAME As String = "CreateSQL_Delete"
        Dim cfUFParameterClass As UFParameterClass              ' �p�����[�^�N���X
        Dim strWhere As New StringBuilder                       ' WHERE��`

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLogClass.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' WHERE���̍쐬
            strWhere.Append(" WHERE ")
            strWhere.Append(ABLtRiyoTdkEntity.NOZEIID)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.RCPTSHICHOSONCD)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.TAXKB)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB)
            strWhere.Append(" AND ")
            strWhere.Append(ABLtRiyoTdkEntity.KOSHINCOUNTER)
            strWhere.Append(" = ")
            strWhere.Append(ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER)

            ' ����DELETE SQL���̍쐬
            m_strDeleteSQL = "DELETE FROM " + ABLtRiyoTdkEntity.TABLE_NAME + strWhere.ToString

            ' �����폜�p�p�����[�^�R���N�V�����̃C���X�^���X��
            m_cfDeleteUFParameterCollectionClass = New UFParameterCollectionClass

            ' �����폜�p�R���N�V�����Ƀp�����[�^��ǉ�
            ' �[�Ŏ�ID
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.NOZEIID
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' ��t�s��������
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.RCPTSHICHOSONCD
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �Ŗڋ敪
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.TAXKB
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)
            ' �X�V�J�E���^
            cfUFParameterClass = New UFParameterClass
            cfUFParameterClass.ParameterName = ABLtRiyoTdkEntity.PREFIX_KEY + ABLtRiyoTdkEntity.KOSHINCOUNTER
            m_cfDeleteUFParameterCollectionClass.Add(cfUFParameterClass)

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
            Throw

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLogClass.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw
        End Try

    End Sub
#End Region
    '*����ԍ� 000005 2010/02/22 �ǉ��I��

#End Region

End Class
