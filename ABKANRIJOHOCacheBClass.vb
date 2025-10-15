'************************************************************************************************
'* �Ɩ���          �����Ǘ��V�X�e��
'* 
'* �N���X��        �����Ǘ����L���b�V���c�`(ABKANRIJOHOCacheBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2014/04/28�@�≺ ���
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2014/04/28  000000      �V�K�쐬
'* 2014/06/11  000001      �o�b�`�������R�[�����ꂽ�ۂ̃G���[�C���i�c���j
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports System.Text
Imports Densan.FrameWork.Tools
Imports System.Web

Public Class ABKANRIJOHOCacheBClass
    Inherits ABAtenaKanriJohoBClass

#Region "�����o�ϐ�"
    '**
    '* �N���XID��`
    '* 
    Private Const THIS_CLASS_NAME As String = "ABKANRIJOHOCacheBClass"

    ' �����o�ϐ��̒�`
    Private m_cfLog As URLogXClass                                     ' ���O�o�̓N���X

    ' �L���b�V���N���X
    Private Const ABKANRIJOHO As String = "ABKANRIJOHO"
    Private Class CacheDataClass
        Public m_strUpdate As String
        Public m_csDS As DataSet
    End Class

    ' �����Ǘ����@��ʃL�[�E���ʃL�[
    Private Const SHUBETSUKEY_KOJINJOHOSEIGYO As String = "20"         ' ��ʃL�[:20�F�l��񐧌�@�\
#End Region

#Region "�R���X�g���N�^"
    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '*                               ByVal cfConfigData As UFConfigDataClass, 
    '*                               ByVal cfRdb As UFRdbClass)
    '* 
    '* �@�\�@�@        ����������
    '* 
    '* ����           cfControlData As UFControlData    : �R���g���[���f�[�^�I�u�W�F�N�g
    '*                cfConfigData As UFConfigDataClass : �R���t�B�O�f�[�^�I�u�W�F�N�g
    '*                cfRdb As UFRdbClass               : �f�[�^�x�[�X�A�N�Z�X�p�I�u�W�F�N�g
    '* 
    '* �߂�l          �Ȃ�
    '************************************************************************************************
    Public Sub New(ByVal cfControlData As UFControlData, _
                   ByVal cfConfigData As UFConfigDataClass, _
                   ByVal cfRdb As UFRdbClass)

        MyBase.New(cfControlData, cfConfigData, cfRdb)

        ' ���O�o�̓N���X�C���X�^���X��
        m_cfLog = New URLogXClass(cfControlData, cfConfigData, Me.GetType.Name)

    End Sub
#End Region

#Region "���\�b�h"
#Region "�Ǘ����}�X�^���o"
    '************************************************************************************************
    '* ���\�b�h��     �Ǘ����}�X�^���o
    '* 
    '* �\��           Private Function GetKanriJohoHoshu() As DataSet
    '* 
    '* �@�\           �w�肳�ꂽ�Ǘ����}�X�^�������ɂ��Y���f�[�^���擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataSet : �擾�����Ǘ����}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu() As DataSet
        Return MyClass.GetKanriJohoHoshu(String.Empty, String.Empty)
    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Ǘ����}�X�^���o
    '* 
    '* �\��           Private Function GetKanriJohoHoshu(ByVal strShuKEY As String) As DataSet
    '* 
    '* �@�\           �w�肳�ꂽ�Ǘ����}�X�^�������ɂ��Y���f�[�^���擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataSet : �擾�����Ǘ����}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu(ByVal strShuKEY As String) As DataSet
        Const THIS_METHOD_NAME As String = "GetKanriJohoHoshu"     '���\�b�h��
        Dim csRet As DataSet
        Dim strMethodName As String = Reflection.MethodBase.GetCurrentMethod.Name

        Try
            m_cfLog.DebugStartWrite(strMethodName)

            ' �L���b�V������f�[�^���擾
            csRet = GetKanriJohoHoshu(strShuKEY, String.Empty)

            m_cfLog.DebugEndWrite(strMethodName)

            Return csRet

        Catch objAppExp As UFAppException
            '���[�j���O���O�o��
            m_cfLog.WarningWrite("�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z", _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z", _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw objAppExp
        Catch objExp As Exception
            '�G���[���O�o��
            m_cfLog.ErrorWrite("�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z", _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw objExp
        End Try
    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Ǘ����}�X�^���o
    '* 
    '* �\��           Private Function GetKanriJohoHoshu(ByVal strShuKEY As String, _
    '*                                                      ByVal strShikibetsuKEY As String) As DataSet
    '* 
    '* �@�\           �w�肳�ꂽ�Ǘ����}�X�^�������ɂ��Y���f�[�^���擾����
    '* 
    '* ����           strShuKEY As String        : ��ʃL�[�i�Ǘ����}�X�^�擾���̃L�[�j
    '*                strShikibetsuKEY As String : ���ʃL�[�i�Ǘ����}�X�^�擾���̃L�[�j
    '* 
    '* �߂�l         DataSet : �擾�����Ǘ����}�X�^�̊Y���f�[�^
    '************************************************************************************************
    Public Overloads Function GetKanriJohoHoshu(ByVal strShuKEY As String, _
                                                     ByVal strShikibetsuKEY As String) As DataSet
        Dim csDS As DataSet
        Dim csRetDS As DataSet
        Dim csDRs As DataRow()
        Dim intI As Integer
        Dim csRetDT As DataTable
        Dim csSB As StringBuilder = New StringBuilder()

        '�L���b�V������Ǘ����̎擾
        csDS = GetDataFromCache()

        'Filter�����̍쐬
        If (strShuKEY <> String.Empty) Then
            csSB.Append(ABAtenaKanriJohoEntity.SHUKEY).Append(" = '").Append(strShuKEY).Append("'")
            If (strShikibetsuKEY <> String.Empty) Then
                csSB.Append(" AND ")
            End If
        End If
        If (strShikibetsuKEY <> String.Empty) Then
            csSB.Append(ABAtenaKanriJohoEntity.SHIKIBETSUKEY).Append(" = '").Append(strShikibetsuKEY).Append("'")
        End If
        If (csSB.RLength > 0) Then
            csDRs = csDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Select(csSB.ToString)
        End If

        csRetDS = csDS.Clone
        csRetDT = csRetDS.Tables(ABAtenaKanriJohoEntity.TABLE_NAME)
        For intI = 0 To csDRs.Length - 1
            csRetDT.ImportRow(csDRs(intI))
        Next
        Return csRetDS
    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Ǘ����}�X�^�擾
    '* 
    '* �\��           Private Function GetDataFromCache() As DataSet
    '* 
    '* �@�\           �Ǘ����}�X�^���L���b�V������擾����
    '* 
    '* ����           �Ȃ�
    '* 
    '* �߂�l         DataSet
    '************************************************************************************************
    Private Function GetDataFromCache() As DataSet
        Const THIS_METHOD_NAME As String = "GetDataFromCache"     '���\�b�h��
        Dim cCacheData As CacheDataClass
        Dim csRet As DataSet

        Try
            '�f�o�b�O�J�n���O�o��
            m_cfLog.DebugStartWrite(THIS_METHOD_NAME)

            SyncLock GetType(HttpContext)
                Try
                    cCacheData = DirectCast(HttpContext.Current.Cache(ABKANRIJOHO), CacheDataClass)
                Catch
                End Try
                If cCacheData Is Nothing Then
                    '*����ԍ� 000001 2014/06/11 �C���J�n
                    'm_cfLog.DebugWrite("�L���b�V���쐬(ABKANRIJOHO)")
                    'cCacheData = New CacheDataClass()
                    'cCacheData.m_csDS = MyBase.GetKanriJohoHoshu(SHUBETSUKEY_KOJINJOHOSEIGYO)
                    'cCacheData.m_strUpdate = String.Empty
                    'HttpContext.Current.Cache(ABKANRIJOHO) = cCacheData

                    csRet = MyBase.GetKanriJohoHoshu(SHUBETSUKEY_KOJINJOHOSEIGYO)

                    If Not (HttpContext.Current Is Nothing) Then
                        'HttpContext.Current��Nothing�łȂ��ꍇ
                        m_cfLog.DebugWrite("�L���b�V���쐬(ABKANRIJOHO)")
                        cCacheData = New CacheDataClass()
                        cCacheData.m_csDS = csRet
                        cCacheData.m_strUpdate = String.Empty
                        HttpContext.Current.Cache(ABKANRIJOHO) = cCacheData
                    Else
                        '����ȊO�̏ꍇ�A�����Ȃ�
                    End If
                    '*����ԍ� 000001 2014/06/11 �C���I��
                Else
                    m_cfLog.DebugWrite("�L���b�V�����Ƀf�[�^�L")
                    '*����ԍ� 000001 2014/06/11 �ǉ��J�n
                    csRet = cCacheData.m_csDS
                    '*����ԍ� 000001 2014/06/11 �ǉ��I��
                End If
                '*����ԍ� 000001 2014/06/11 �폜�J�n
                'csRet = cCacheData.m_csDS
                '*����ԍ� 000001 2014/06/11 �폜�I��

            End SyncLock

            m_cfLog.DebugEndWrite(THIS_METHOD_NAME)

            Return csRet

        Catch objAppExp As UFAppException
            '���[�j���O���O�o��
            m_cfLog.WarningWrite("�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z", _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z", _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            Throw objAppExp
        Catch objExp As Exception
            '�G���[���O�o��
            m_cfLog.ErrorWrite("�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z", _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            Throw objExp
        End Try
    End Function
#End Region
#End Region

End Class
