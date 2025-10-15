'************************************************************************************************
'* �Ɩ���          �����V�X�e��
'* 
'* �N���X��        ���L�X�V�a(ABKyoyuKoshinBClass)
'* 
'* �o�[�W�������   Ver 1.0
'* 
'* ���t���@�쐬��   2003/06/06�@���@�Ԗ�
'*
'* ���쌠          �i���j�d�Z
'************************************************************************************************
'* �C�������@�@����ԍ��@�@�C�����e
'* 2004/05/17  000001      ���L�X�V�������\�b�h�Ɉٓ������������Ƃ��Ēǉ�
'* 2004/05/17  000002      ���߈ٓ��N�����Ɉٓ��������i�[�ɏC��
'* 2006/03/27  000003      ���[�N�t���[�A�g���\�b�h�ǉ�
'* 2006/05/31  000004      �ݐύX�V���Ɉٓ��O�f�[�^���ǉ�����
'* 2006/09/13  000005      �X�V���@��ύX����
'*                         �����f�[�^�̊J�n�`�I���Ɉ����̈ٓ��N���������Ă͂܂����f�[�^�ȍ~��
'*                         ������cABJutogai�̓��e�ōX�V����B�A���J�n�E�I���E�ٓ��N�����͏���
'*                         ���Ă͂܂����f�[�^�����߂̏ꍇ�͒ʏ�ʂ蕪������(�}���S���R)
'* 2007/10/11  000006      �����ҏW�����̖��g�p�N���X(UR�Ǘ����L���b�V���N���X)���폜����i��Áj
'* 2010/04/16  000007      VS2008�Ή��i��Áj
'* 2014/12/24  000008      �yAB21080�z���ԃT�[�o�[�a�r�A�g�@�\�ǉ��i�΍��j
'* 2015/01/08  000009      �yAB21080�z���ԃT�[�o�[�a�r�A�g�@�\�폜�i�΍��j
'************************************************************************************************
Option Strict On
Option Explicit On 
Option Compare Binary

'**
'* �Q�Ƃ��閼�O���
'* 
Imports Densan.FrameWork
Imports Densan.FrameWork.Tools
Imports Densan.Common

Public Class ABKyoyuKoshinBClass

    '**
    '* �N���XID��`
    '* 
    Private Const THIS_CLASS_NAME As String = "ABKyoyuKoshinBClass"

    '**
    '* �����o�ϐ�
    '*  
    Private m_cfControlData As UFControlData                ' �R���g���[���f�[�^
    Private m_cfConfigData As UFConfigDataClass             ' �����f�[�^�N���X
    Private m_cfLog As UFLogClass                           ' ���O�o�̓N���X
    Private m_cfRdb As UFRdbClass                           ' RDB�N���X
    Private m_cfErrorClass As UFErrorClass                  ' �G���[�����N���X
    Private m_cfDateClass As UFDateClass                    ' ���t�N���X
    Private m_cNyuryokuParaX As ABNyuryokuParaXClass        ' ���͉�ʃp�����[�^
    Private m_cCommonClass As New ABCommonClass()           ' Common�N���X

    '************************************************************************************************
    '* ���\�b�h��      �R���X�g���N�^
    '* 
    '* �\��           Public Sub New(ByVal cfControlData As UFControlData, 
    '* �@�@                          ByVal cfConfigDataClass As UFConfigDataClass
    '* �@�@                          ByVal csUFRdbClass As UFRdbClass)
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
        m_cfConfigData = cfConfigDataClass
        m_cfRdb = cfRdbClass

        ' ���O�o�̓N���X�̃C���X�^���X��
        m_cfLog = New UFLogClass(cfConfigDataClass, cfControlData.m_strBusinessId)

        ' ���t�N���X�̃C���X�^���X��
        m_cfDateClass = New UFDateClass(m_cfConfigData)
    End Sub


    '************************************************************************************************
    '* ���\�b�h��     ���L�X�V����
    '* 
    '* �\��           Public Function UpdateKyoyu(ByVal StrJuminCD As String, _
    '*                        �@                  ByVal IntKoshinKB As Integer, _
    '*                                            ByVal cABJutogai As DataSet) As Integer
    '* 
    '* �@�\�@�@       ���L�f�[�^�̒ǉ����s�Ȃ��B
    '* 
    '* ����           StrJuminCD As String           : �Z���R�[�h
    '*                IntKoshinKB As Intege          : �X�V�敪
    '* �@�@�@         cABJutogai As DataSet          : �Z�o�OEntity
    '* 
    '* �߂�l         ����
    '************************************************************************************************
    Public Function UpdateKyoyu(ByVal StrJuminCD As String, _
                                ByVal IntKoshinKB As Integer, _
                                ByVal StrIdoYMD As String, _
                                ByVal cABJutogai As DataSet) As Integer

        Const THIS_METHOD_NAME As String = "UpdateKyoyu"    '���\�b�h��
        Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        Dim cJutogaiB As ABJutogaiBClass                    ' �Z�o�O�c�`
        Dim csJutogaiEntity As DataSet                      ' �Z�o�ODataSet
        Dim csJutogaiRow As DataRow                         ' �Z�o�ORow
        Dim csJutogaiRowN As DataRow
        Dim cAtenaB As ABAtenaBClass                        ' �����c�`
        Dim csAtenaEntity As DataSet                        ' ����Entity
        Dim cAtenaSearchKey As New ABAtenaSearchKey()       ' ���������L�[
        Dim cAtenaRirekiB As ABAtenaRirekiBClass            ' ���������c�`
        Dim csAtenaRirekiEntity As DataSet                  ' ��������Entity
        Dim cAtenaRuisekiB As ABAtenaRuisekiBClass          ' �����ݐςc�`
        Dim csAtenaRuisekiEntity As DataSet                 ' �����ݐ�Entity
        Dim intUpdataCount As Integer                       ' �X�V����
        Dim strSystemDate As String                         ' �V�X�e�����t
        Dim csDataRow As DataRow
        '* corresponds to VS2008 Start 2010/04/16 000007
        Dim cABEnumDefine As New ABEnumDefine
        'Dim csColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000007
        '*����ԍ� 000003 2006/03/27 �ǉ��J�n
        Dim strKoshinKB As String                           '�X�V�敪
        '*����ԍ� 000003 2006/03/27 �ǉ��I��
        '*����ԍ� 000005 2006/09/13 �ǉ��J�n
        Dim csRirekiRows() As DataRow
        Dim csRirekiCkinRow As DataRow
        '*����ԍ� 000005 2006/09/13 �ǉ��I��
        '* ����ԍ� 000009 2015/01/08 �폜�J�n
        ''*����ԍ� 000008 2014/12/24 �ǉ��J�n
        'Dim cABBSRenkeiB As ABBSRenkeiBClass                ' ���ԃT�[�o�[�a�r�A�g�r�W�l�X�N���X
        ''*����ԍ� 000008 2014/12/24 �ǉ��I��
        '* ����ԍ� 000009 2015/01/08 �폜�I��

        Try

            ' �f�o�b�O�J�n���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �Z�o�O�c�`�N���X�̃C���X�^���X��
            cJutogaiB = New ABJutogaiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            ' �����c�`�N���X�̃C���X�^���X��
            cAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            ' ���������c�`�N���X�̃C���X�^���X��
            cAtenaRirekiB = New ABAtenaRirekiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            ' �����ݐςc�`�N���X�̃C���X�^���X��
            cAtenaRuisekiB = New ABAtenaRuisekiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)

            ' �V�X�e�����t�̎擾
            strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMdd")


            ' �Z�o�O�}�X�^�̒ǉ����s�Ȃ�
            If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                intUpdataCount = cJutogaiB.InsertJutogaiB(cABJutogai.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0))
            Else
                csJutogaiEntity = cJutogaiB.GetJutogaiBHoshu(StrJuminCD)

                ' �Z�o�O�f�[�^�����݂��Ȃ��ꍇ�A�G���[�𔭐�����
                If (csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows.Count = 0) Then
                    m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    ' �G���[��`���擾�i�X�V�Ώۂ̃f�[�^�����݂��܂���B�F�Z�o�O�j
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001040)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z�o�O", objErrorStruct.m_strErrorCode)
                End If

                csJutogaiRow = csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0)
                csJutogaiRowN = cABJutogai.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0)

                csJutogaiRow.BeginEdit()

                'For Each csColumn In csJutogaiRow.Table.Columns
                '    csJutogaiRow(csColumn.ColumnName) = csJutogaiRowN(csColumn)
                'Next csColumn
                'csJutogaiRow = csJutogaiRowN
                ' �Z�o�O�ҏW����
                Me.EditJutogai(csJutogaiRow, csJutogaiRowN)

                csJutogaiRow.EndEdit()

                intUpdataCount = cJutogaiB.UpdateJutogaiB(csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0))
                'intUpdataCount = cJutogaiB.UpdateJutogaiB(csJutogaiRow)
            End If

            ' �X�V�������P���ȊO�̏ꍇ�A�G���[�𔭐�������
            If Not (intUpdataCount = 1) Then
                m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F�Z�o�O�j
                objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�Z�o�O", objErrorStruct.m_strErrorCode)
            End If

            '**
            '* �����}�X�^�X�V����
            '*
            cAtenaSearchKey.p_strJuminCD = StrJuminCD

            ' �����ҏW����
            ' �V�K�쐬�̏ꍇ
            If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                csAtenaEntity = m_cfRdb.GetTableSchema(ABAtenaEntity.TABLE_NAME)
            Else
                ' �����}�X�^���擾����
                ' �����c�`�N���X�̃C���X�^���X��
                cAtenaB = New ABAtenaBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
                csAtenaEntity = cAtenaB.GetAtenaBHoshu(1, cAtenaSearchKey)
            End If

            Me.EditAtenaJutogai(IntKoshinKB, StrIdoYMD, cABJutogai, csAtenaEntity)

            For Each csDataRow In csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows

                If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                    ' �����}�X�^�̒ǉ����s�Ȃ�
                    intUpdataCount = cAtenaB.InsertAtenaB(csDataRow)
                Else
                    ' �����}�X�^�̍X�V���s�Ȃ�
                    intUpdataCount = cAtenaB.UpdateAtenaB(csDataRow)
                End If

                ' �X�V�������P���ȊO�̏ꍇ�A�G���[�𔭐�������
                If Not (intUpdataCount = 1) Then
                    m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                    ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F�����j
                    objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                    Throw New UFAppException(objErrorStruct.m_strErrorMessage + "����", objErrorStruct.m_strErrorCode)
                End If
            Next csDataRow

            '**
            '* ���������}�X�^�X�V����
            '*

            ' ���������}�X�^���擾����
            '*����ԍ� 000005 2006/09/13 �C���J�n
            ' ���߂����łȂ��S���擾����
            ''csAtenaRirekiEntity = cAtenaRirekiB.GetAtenaRBHoshu(999, cAtenaSearchKey, "99999999", True)
            csAtenaRirekiEntity = cAtenaRirekiB.GetAtenaRBHoshu(999, cAtenaSearchKey, "", True)

            ' ���߃��E��ޔ����Ă���
            csRirekiRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2' AND " + ABAtenaRirekiEntity.RRKED_YMD + " = '99999999'")
            If csRirekiRows.Length > 0 Then
                csRirekiCkinRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow()
                csRirekiCkinRow.ItemArray = csRirekiRows(0).ItemArray
            Else
                csRirekiCkinRow = Nothing
            End If
            '*����ԍ� 000005 2006/09/13 �C���I��

            ' ��������ҏW����
            Me.EditAtenaRireki(StrIdoYMD, csAtenaEntity, csAtenaRirekiEntity)

            ' ���������}�X�^�̒ǉ����s�Ȃ�
            For Each csDataRow In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows
                Select Case csDataRow.RowState
                    Case DataRowState.Added
                        intUpdataCount = cAtenaRirekiB.InsertAtenaRB(csDataRow)

                        ' �X�V�������P���ȊO�̏ꍇ�A�G���[�𔭐�������
                        If Not (intUpdataCount = 1) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F���������j
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                        End If
                    Case DataRowState.Modified
                        intUpdataCount = cAtenaRirekiB.UpdateAtenaRB(csDataRow)
                        ' �X�V�������P���ȊO�̏ꍇ�A�G���[�𔭐�������
                        If Not (intUpdataCount = 1) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            ' �G���[��`���擾�i�X�V�Ώۂ̃f�[�^�����݂��܂���B�F���������j
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001040)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "��������", objErrorStruct.m_strErrorCode)
                        End If
                End Select
            Next csDataRow

            '**
            '* �����ݐσ}�X�^�X�V����
            '*

            ' �����ݐσ}�X�^���擾����
            csAtenaRuisekiEntity = m_cfRdb.GetTableSchema(ABAtenaRuisekiEntity.TABLE_NAME)

            ' �����ݐϕҏW����
            '*����ԍ� 000005 2006/09/13 �C���J�n
            ' �ޔ����Ă���������O�̒��߃��R�[�h�������ɉ�����
            ''Me.EditAtenaRuiseki(csAtenaRirekiEntity, csAtenaRuisekiEntity)
            Me.EditAtenaRuiseki(csAtenaRirekiEntity, csAtenaRuisekiEntity, csRirekiCkinRow)
            '*����ԍ� 000005 2006/09/13 �C���I��

            ' �����ݐσ}�X�^�̒ǉ����s�Ȃ�
            For Each csDataRow In csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows
                Select Case csDataRow.RowState
                    Case DataRowState.Added
                        intUpdataCount = cAtenaRuisekiB.InsertAtenaRB(csDataRow)

                        ' �X�V�������P���ȊO�̏ꍇ�A�G���[�𔭐�������
                        If Not (intUpdataCount = 1) Then
                            m_cfErrorClass = New UFErrorClass(ABConstClass.THIS_BUSINESSID)
                            ' �G���[��`���擾�i���ɓ���f�[�^�����݂��܂��B�F�����ݐρj
                            objErrorStruct = m_cfErrorClass.GetErrorStruct(URErrorClass.URE001044)
                            Throw New UFAppException(objErrorStruct.m_strErrorMessage + "�����ݐ�", objErrorStruct.m_strErrorCode)
                        End If
                End Select

            Next csDataRow

            '*����ԍ� 000003 2006/03/27 �ǉ��J�n
            '�����敪�����Y�ōX�V�p���烏�[�N�t���[�A�g�p�ɏC������
            Select Case IntKoshinKB
                Case cABEnumDefine.KoshinKB.Insert
                    strKoshinKB = "1"
                Case cABEnumDefine.KoshinKB.Update
                    strKoshinKB = "2"
            End Select
            '���[�N�t���[�A�g�����̌Ăяo��
            AtenaDataReplicaKoshin(CStr(cABJutogai.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0).Item(ABJutogaiEntity.JUMINCD)), _
                                      CStr(cABJutogai.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0).Item(ABJutogaiEntity.STAICD)), CStr(IntKoshinKB))
            '*����ԍ� 000003 2006/03/27 �ǉ��I��

            '* ����ԍ� 000009 2015/01/08 �폜�J�n
            ''*����ԍ� 000008 2014/12/24 �ǉ��J�n
            '' ���ԃT�[�o�[�a�r�A�g�r�W�l�X�N���X�̃C���X�^���X��
            'cABBSRenkeiB = New ABBSRenkeiBClass(m_cfControlData, m_cfConfigData, m_cfRdb)

            '' ���ԃT�[�o�[�a�r�A�g�̎��s
            'cABBSRenkeiB.ExecRenkei(StrJuminCD)
            ''*����ԍ� 000008 2014/12/24 �ǉ��I��
            '* ����ԍ� 000009 2015/01/08 �폜�I��

            ' �f�o�b�O�I�����O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objRdbDeadLockExp As UFRdbDeadLockException   ' �f�b�h���b�N���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objRdbDeadLockExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objRdbDeadLockExp.Message + "�z")
            ' UFAppException���X���[����
            Throw New UFAppException(objRdbDeadLockExp.Message, objRdbDeadLockExp.p_intErrorCode, objRdbDeadLockExp)

        Catch objUFRdbUniqueExp As UFRdbUniqueException     ' ��Ӑ���ᔽ���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objUFRdbUniqueExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objUFRdbUniqueExp.Message + "�z")
            ' UFAppException���X���[����
            Throw New UFAppException(objUFRdbUniqueExp.Message, objUFRdbUniqueExp.p_intErrorCode, objUFRdbUniqueExp)

        Catch objRdbTimeOutExp As UFRdbTimeOutException    ' UFRdbTimeOutException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objRdbTimeOutExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objRdbTimeOutExp.Message + "�z")
            ' UFAppException���X���[����
            Throw New UFAppException(objRdbTimeOutExp.Message, objRdbTimeOutExp.p_intErrorCode, objRdbTimeOutExp)

        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

        Return intUpdataCount

    End Function

    '************************************************************************************************
    '* ���\�b�h��     �Z�o�O�ҏW����
    '* 
    '* �\��           Public Sub EditJutogai(ByVal cfControlData As UFControlData,
    '* �@�@                               ByVal cNyuryokuParaX As ABNyuryokuParaXClass) As DataSet
    '* 
    '* �@�\�@�@       ���͉�ʃf�[�^���Z�o�OEntity��ǉ��E�ҏW����
    '* 
    '* ����           csJutogaiEntity As DataSet              : �Z�o�OEntity
    '* �@�@           cNyuryokuParaX As ABNyuryokuParaXClass  : �l���̓f�[�^
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub EditJutogai(ByRef csJutogaiRow As DataRow, _
                            ByVal csJutogaiRowN As DataRow)
        Const THIS_METHOD_NAME As String = "EditJutogai"    '���\�b�h��
        Dim cABJutogaiIF As New ABJutogaiEntity()                   '�Z�o�O�}�X�^�R���X�g�N���X

        Try
            '**
            '* �ҏW����
            '*
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            With cABJutogaiIF
                '�s�����R�[�h
                csJutogaiRow(.SHICHOSONCD) = csJutogaiRowN(.SHICHOSONCD)
                '���s�����R�[�h
                csJutogaiRow(.KYUSHICHOSONCD) = csJutogaiRowN(.KYUSHICHOSONCD)
                '���уR�[�h
                csJutogaiRow(.STAICD) = csJutogaiRowN(.STAICD)
                '�����f�[�^�敪
                csJutogaiRow(.ATENADATAKB) = csJutogaiRowN(.ATENADATAKB)
                '�����f�[�^���
                csJutogaiRow(.ATENADATASHU) = csJutogaiRowN(.ATENADATASHU)
                '�����p�J�i����
                csJutogaiRow(.SEARCHKANASEIMEI) = csJutogaiRowN(.SEARCHKANASEIMEI)
                '�����p�J�i��
                csJutogaiRow(.SEARCHKANASEI) = csJutogaiRowN(.SEARCHKANASEI)
                '�����p�J�i��
                csJutogaiRow(.SEARCHKANAMEI) = csJutogaiRowN(.SEARCHKANAMEI)
                '�J�i����1
                csJutogaiRow(.KANAMEISHO1) = csJutogaiRowN(.KANAMEISHO1)
                '��������1
                csJutogaiRow(.KANJIMEISHO1) = csJutogaiRowN(.KANJIMEISHO1)
                '�J�i����2
                csJutogaiRow(.KANAMEISHO2) = csJutogaiRowN(.KANAMEISHO2)
                '��������2
                csJutogaiRow(.KANJIMEISHO2) = csJutogaiRowN(.KANJIMEISHO2)
                '���N����
                csJutogaiRow(.UMAREYMD) = csJutogaiRowN(.UMAREYMD)
                '���a��N����
                csJutogaiRow(.UMAREWMD) = csJutogaiRowN(.UMAREWMD)
                '���ʃR�[�h
                csJutogaiRow(.SEIBETSUCD) = csJutogaiRowN(.SEIBETSUCD)
                '����
                csJutogaiRow(.SEIBETSU) = csJutogaiRowN(.SEIBETSU)
                '�����R�[�h
                csJutogaiRow(.ZOKUGARACD) = csJutogaiRowN(.ZOKUGARACD)
                '����
                csJutogaiRow(.ZOKUGARA) = csJutogaiRowN(.ZOKUGARA)
                '��2�����R�[�h
                csJutogaiRow(.DAI2ZOKUGARACD) = csJutogaiRowN(.DAI2ZOKUGARACD)
                '��2����
                csJutogaiRow(.DAI2ZOKUGARA) = csJutogaiRowN(.DAI2ZOKUGARA)
                '�����@�l��\�Ҏ���
                csJutogaiRow(.KANJIHJNDAIHYOSHSHIMEI) = csJutogaiRowN(.KANJIHJNDAIHYOSHSHIMEI)
                '�ėp�敪1
                csJutogaiRow(.HANYOKB1) = csJutogaiRowN(.HANYOKB1)
                '�����@�l�`��
                csJutogaiRow(.KANJIHJNKEITAI) = csJutogaiRowN(.KANJIHJNKEITAI)
                '�l�@�l�敪
                csJutogaiRow(.KJNHJNKB) = csJutogaiRowN(.KJNHJNKB)
                '�ėp�敪2
                csJutogaiRow(.HANYOKB2) = csJutogaiRowN(.HANYOKB2)
                '�Ǔ��ǊO�敪
                csJutogaiRow(.KANNAIKANGAIKB) = csJutogaiRowN(.KANNAIKANGAIKB)
                '�Ɖ��~�敪
                csJutogaiRow(.KAOKUSHIKIKB) = csJutogaiRowN(.KAOKUSHIKIKB)
                '���l�Ŗ�
                csJutogaiRow(.BIKOZEIMOKU) = csJutogaiRowN(.BIKOZEIMOKU)
                '�X�֔ԍ�
                csJutogaiRow(.YUBINNO) = csJutogaiRowN(.YUBINNO)
                '�Z���R�[�h
                csJutogaiRow(.JUSHOCD) = csJutogaiRowN(.JUSHOCD)
                '�Z��
                csJutogaiRow(.JUSHO) = csJutogaiRowN(.JUSHO)
                '�Ԓn�R�[�h1
                csJutogaiRow(.BANCHICD1) = csJutogaiRowN(.BANCHICD1)
                '�Ԓn�R�[�h2
                csJutogaiRow(.BANCHICD2) = csJutogaiRowN(.BANCHICD2)
                '�Ԓn�R�[�h3
                csJutogaiRow(.BANCHICD3) = csJutogaiRowN(.BANCHICD3)
                '�Ԓn
                csJutogaiRow(.BANCHI) = csJutogaiRowN(.BANCHI)
                '�����t���O
                csJutogaiRow(.KATAGAKIFG) = csJutogaiRowN(.KATAGAKIFG)
                '�����R�[�h
                csJutogaiRow(.KATAGAKICD) = csJutogaiRowN(.KATAGAKICD)
                '����
                csJutogaiRow(.KATAGAKI) = csJutogaiRowN(.KATAGAKI)
                '�A����1
                csJutogaiRow(.RENRAKUSAKI1) = csJutogaiRowN(.RENRAKUSAKI1)
                '�A����2
                csJutogaiRow(.RENRAKUSAKI2) = csJutogaiRowN(.RENRAKUSAKI2)
                '�s����R�[�h
                csJutogaiRow(.GYOSEIKUCD) = csJutogaiRowN(.GYOSEIKUCD)
                '�s���於
                csJutogaiRow(.GYOSEIKUMEI) = csJutogaiRowN(.GYOSEIKUMEI)
                '�n��R�[�h1
                csJutogaiRow(.CHIKUCD1) = csJutogaiRowN(.CHIKUCD1)
                '�n�於1
                csJutogaiRow(.CHIKUMEI1) = csJutogaiRowN(.CHIKUMEI1)
                '�n��R�[�h2
                csJutogaiRow(.CHIKUCD2) = csJutogaiRowN(.CHIKUCD2)
                '�n�於2
                csJutogaiRow(.CHIKUMEI2) = csJutogaiRowN(.CHIKUMEI2)
                '�n��R�[�h3
                csJutogaiRow(.CHIKUCD3) = csJutogaiRowN(.CHIKUCD3)
                '�n�於3
                csJutogaiRow(.CHIKUMEI3) = csJutogaiRowN(.CHIKUMEI3)
                '�o�^�ٓ��N����
                csJutogaiRow(.TOROKUIDOYMD) = csJutogaiRowN(.TOROKUIDOYMD)
                '�o�^���R�R�[�h
                csJutogaiRow(.TOROKUJIYUCD) = csJutogaiRowN(.TOROKUJIYUCD)
                '�����ٓ��N����
                csJutogaiRow(.SHOJOIDOYMD) = csJutogaiRowN(.SHOJOIDOYMD)
                '�������R�R�[�h
                csJutogaiRow(.SHOJOJIYUCD) = csJutogaiRowN(.SHOJOJIYUCD)
                '���U�[�u
                csJutogaiRow(.RESERVE) = csJutogaiRowN(.RESERVE)
            End With

            ' �f�o�b�O�I�����O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �����ҏW����
    '* 
    '* �\��           Public Sub EditAtenaJutogai(ByVal csJutogaiEntity As DataSet, _
    '* �@�@                             ByVal csAtenaEntity As DataSet)
    '* 
    '* �@�\�@�@       �Z�o�OEntity��舶��Entity��ǉ��E�ҏW����
    '* 
    '* ����           csJutogaiEntity As DataSet  : �Z�o�O(ABJutogaiEntity)
    '* �@�@           csAtenaEntity   As DataSet  : ����(ABAtenaEntity)
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Sub EditAtenaJutogai(ByVal IntKoshinKB As Integer, _
                                 ByVal StrIdoYMD As String, _
                                 ByVal csJutogaiEntity As DataSet, _
                                 ByRef csAtenaEntity As DataSet)
        Const THIS_METHOD_NAME As String = "EditAtenaJutogai"
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                 ' �G���[��`�\����
        'Dim cuCityInfo As USSCityInfoClass                  ' �s�������Ǘ��N���X
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim csRow As DataRow
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim csDataSet As DataSet
        'Dim csColumn As DataColumn
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim strSystemDate As String                         ' �V�X�e�����t
        Dim csJutogaiRow As DataRow                         ' �Z�o�ODataRow
        Dim cIdoJiyuB As ABIdoJiyuBClass                    ' �ٓ����R�a�N���X
        '* ����ԍ� 000006 2007/10/11 �폜�J�n
        '* ����ԍ� 000002 2003/08/22 �C���J�n
        'Dim cuKanriJohoB As URKANRIJOHOBClass               ' �Ǘ����a�N���X
        'Dim cuKanriJohoB As URKANRIJOHOCacheBClass          ' �Ǘ����a�N���X(�L���b�V���Ή���)
        '* ����ԍ� 000002 2003/08/22 �C���I��
        'Dim emKensakShimei As FrnKensakuShimeiType          ' �O���l�����p����
        '* ����ԍ� 000006 2007/10/11 �폜�I��
        '* corresponds to VS2008 Start 2010/04/16 000007
        Dim cABEnumDefine As New ABEnumDefine
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim cAtenaSearchKey As New ABAtenaSearchKey()       '���������L�[
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim cAtenaB As ABAtenaBClass                        '�����c�`
        '* corresponds to VS2008 End 2010/04/16 000007


        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �ٓ����R�a�N���X�̃C���X�^���X��
            cIdoJiyuB = New ABIdoJiyuBClass(m_cfControlData, m_cfConfigData)

            ' �t�q�Ǘ����a�N���X�̃C���X�^���X��
            '* ����ԍ� 000006 2007/10/11 �폜�J�n
            '* ����ԍ� 000002 2003/08/22 �C���J�n
            'cuKanriJohoB = New URKANRIJOHOBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            'cuKanriJohoB = New URKANRIJOHOCacheBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            '* ����ԍ� 000002 2003/08/22 �C���I��
            '* ����ԍ� 000006 2007/10/11 �폜�J�n

            ' ���t�N���X�̕K�v�Ȑݒ���s��
            m_cfDateClass.p_enDateSeparator = UFDateSeparator.None

            '* ����ԍ� 000006 2007/10/11 �폜�J�n
            '' �t�q�O���l�����p�������擾����   �ۗ�
            'emKensakShimei = cuKanriJohoB.GetFrn_KensakuShimei_Param
            '* ����ԍ� 000006 2007/10/11 �폜�J�n

            ' �Z�o�O�f�[�^Row
            csJutogaiRow = csJutogaiEntity.Tables(ABJutogaiEntity.TABLE_NAME).Rows(0)

            If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                csRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).NewRow
                ' DataRow�̏�����
                m_cCommonClass.InitColumnValue(csRow)
            Else
                ' �����}�X�^���擾����
                ' �����c�`�N���X�̃C���X�^���X��
                csRow = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows(0)
            End If

            '**
            '* �ҏW����
            '*
            strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMdd")                                        '�V�X�e�����t

            csRow(ABAtenaEntity.JUMINCD) = csJutogaiRow(ABJutogaiEntity.JUMINCD)                                ' �Z���R�[�h
            csRow(ABAtenaEntity.SHICHOSONCD) = csJutogaiRow(ABJutogaiEntity.SHICHOSONCD)                        ' �s�����R�[�h
            csRow(ABAtenaEntity.KYUSHICHOSONCD) = csJutogaiRow(ABJutogaiEntity.KYUSHICHOSONCD)                  ' ���s�����R�[�h
            csRow(ABAtenaEntity.JUMINJUTOGAIKB) = "2"                                                           ' �Z���Z�o�O�敪
            csRow(ABAtenaEntity.JUMINYUSENIKB) = "0"                                                            ' �Z���D��敪
            csRow(ABAtenaEntity.JUTOGAIYUSENKB) = "1"                                                           ' �Z�o�O�D��敪
            csRow(ABAtenaEntity.ATENADATAKB) = csJutogaiRow(ABJutogaiEntity.ATENADATAKB)                        ' �����f�[�^�敪
            csRow(ABAtenaEntity.STAICD) = csJutogaiRow(ABJutogaiEntity.STAICD)                                  ' ���уR�[�h
            csRow(ABAtenaEntity.ATENADATASHU) = csJutogaiRow(ABJutogaiEntity.ATENADATASHU)                      ' �����f�[�^���
            csRow(ABAtenaEntity.HANYOKB1) = csJutogaiRow(ABJutogaiEntity.HANYOKB1)                              ' �ėp�敪1
            csRow(ABAtenaEntity.KJNHJNKB) = csJutogaiRow(ABJutogaiEntity.KJNHJNKB)                              ' �l�@�l�敪
            csRow(ABAtenaEntity.HANYOKB2) = csJutogaiRow(ABJutogaiEntity.HANYOKB2)                              ' �ėp�敪2
            csRow(ABAtenaEntity.KANNAIKANGAIKB) = csJutogaiRow(ABJutogaiEntity.KANNAIKANGAIKB)                  ' �Ǔ��ǊO�敪
            csRow(ABAtenaEntity.KANAMEISHO1) = csJutogaiRow(ABJutogaiEntity.KANAMEISHO1)                        ' �J�i����1
            csRow(ABAtenaEntity.KANJIMEISHO1) = csJutogaiRow(ABJutogaiEntity.KANJIMEISHO1)                      ' ��������1
            csRow(ABAtenaEntity.KANAMEISHO2) = csJutogaiRow(ABJutogaiEntity.KANAMEISHO2)                        ' �J�i����2
            csRow(ABAtenaEntity.KANJIMEISHO2) = csJutogaiRow(ABJutogaiEntity.KANJIMEISHO2)                      ' ��������2
            csRow(ABAtenaEntity.KANJIHJNKEITAI) = csJutogaiRow(ABJutogaiEntity.KANJIHJNKEITAI)                  ' �����@�l�`��
            csRow(ABAtenaEntity.KANJIHJNDAIHYOSHSHIMEI) = csJutogaiRow(ABJutogaiEntity.KANJIHJNDAIHYOSHSHIMEI)  ' �����@�l��\�Ҏ���
            csRow(ABAtenaEntity.SEARCHKANJIMEISHO) = csJutogaiRow(ABJutogaiEntity.KANJIMEISHO1)             ' �����p��������
            csRow(ABAtenaEntity.SEARCHKANASEIMEI) = csJutogaiRow(ABJutogaiEntity.SEARCHKANASEIMEI)              ' �����p�J�i����
            csRow(ABAtenaEntity.SEARCHKANASEI) = csJutogaiRow(ABJutogaiEntity.SEARCHKANASEI)                    ' �����p�J�i��
            csRow(ABAtenaEntity.SEARCHKANAMEI) = csJutogaiRow(ABJutogaiEntity.SEARCHKANAMEI)                    ' �����p�J�i��
            csRow(ABAtenaEntity.RRKST_YMD) = StrIdoYMD                                                      ' �����J�n�N����
            csRow(ABAtenaEntity.RRKED_YMD) = "99999999"                                                         ' �����I���N����
            csRow(ABAtenaEntity.UMAREYMD) = csJutogaiRow(ABJutogaiEntity.UMAREYMD)                              ' ���N����
            csRow(ABAtenaEntity.UMAREWMD) = csJutogaiRow(ABJutogaiEntity.UMAREWMD)                              ' ���a��N����
            csRow(ABAtenaEntity.SEIBETSUCD) = csJutogaiRow(ABJutogaiEntity.SEIBETSUCD)                          ' ���ʃR�[�h
            csRow(ABAtenaEntity.SEIBETSU) = csJutogaiRow(ABJutogaiEntity.SEIBETSU)                              ' ����
            csRow(ABAtenaEntity.ZOKUGARACD) = csJutogaiRow(ABJutogaiEntity.ZOKUGARACD)                          ' �����R�[�h
            csRow(ABAtenaEntity.ZOKUGARA) = csJutogaiRow(ABJutogaiEntity.ZOKUGARA)                              ' ����
            csRow(ABAtenaEntity.DAI2ZOKUGARACD) = csJutogaiRow(ABJutogaiEntity.DAI2ZOKUGARACD)                  ' ��2�����R�[�h
            csRow(ABAtenaEntity.DAI2ZOKUGARA) = csJutogaiRow(ABJutogaiEntity.DAI2ZOKUGARA)                      ' ��2����
            csRow(ABAtenaEntity.YUBINNO) = csJutogaiRow(ABJutogaiEntity.YUBINNO)                                ' �X�֔ԍ�
            csRow(ABAtenaEntity.JUSHOCD) = csJutogaiRow(ABJutogaiEntity.JUSHOCD)                                ' �Z���R�[�h
            csRow(ABAtenaEntity.JUSHO) = csJutogaiRow(ABJutogaiEntity.JUSHO)                                    ' �Z��
            csRow(ABAtenaEntity.BANCHICD1) = csJutogaiRow(ABJutogaiEntity.BANCHICD1)                            ' �Ԓn�R�[�h1
            csRow(ABAtenaEntity.BANCHICD2) = csJutogaiRow(ABJutogaiEntity.BANCHICD2)                            ' �Ԓn�R�[�h2
            csRow(ABAtenaEntity.BANCHICD3) = csJutogaiRow(ABJutogaiEntity.BANCHICD3)                            ' �Ԓn�R�[�h3
            csRow(ABAtenaEntity.BANCHI) = csJutogaiRow(ABJutogaiEntity.BANCHI)                                  ' �Ԓn
            csRow(ABAtenaEntity.KATAGAKIFG) = csJutogaiRow(ABJutogaiEntity.KATAGAKIFG)                          ' �����t���O
            csRow(ABAtenaEntity.KATAGAKICD) = csJutogaiRow(ABJutogaiEntity.KATAGAKICD)                          ' �����R�[�h
            csRow(ABAtenaEntity.KATAGAKI) = csJutogaiRow(ABJutogaiEntity.KATAGAKI)                              ' ����
            csRow(ABAtenaEntity.RENRAKUSAKI1) = csJutogaiRow(ABJutogaiEntity.RENRAKUSAKI1)                      ' �A����1
            csRow(ABAtenaEntity.RENRAKUSAKI2) = csJutogaiRow(ABJutogaiEntity.RENRAKUSAKI2)                      ' �A����2
            ' ���߈ٓ��N����
            'm_cfDateClass.p_strDateValue = m_cNyuryokuParaX.p_strCkinIdoYMD
            '*����ԍ� 000002 2004/05/17 �C���J�n
            'csRow(ABAtenaEntity.CKINIDOYMD) = strSystemDate                                                      ' �����J�n�N����
            csRow(ABAtenaEntity.CKINIDOYMD) = StrIdoYMD
            '*����ԍ� 000002 2004/05/17 �C���I��
            ' �o�^�ٓ��N����
            csRow(ABAtenaEntity.TOROKUIDOYMD) = csJutogaiRow(ABJutogaiEntity.TOROKUIDOYMD)
            ' �o�^�ٓ��a��N����
            m_cfDateClass.p_strDateValue = CType(csJutogaiRow(ABJutogaiEntity.TOROKUIDOYMD), String)
            csRow(ABAtenaEntity.TOROKUIDOWMD) = m_cfDateClass.p_strWarekiYMD
            csRow(ABAtenaEntity.TOROKUJIYUCD) = csJutogaiRow(ABJutogaiEntity.TOROKUJIYUCD)                      ' �o�^���R�R�[�h
            csRow(ABAtenaEntity.TOROKUJIYU) = cIdoJiyuB.GetIdoJiyu(csJutogaiRow(ABJutogaiEntity.TOROKUJIYUCD).ToString)     ' �o�^���R
            csRow(ABAtenaEntity.SHOJOIDOYMD) = csJutogaiRow(ABJutogaiEntity.SHOJOIDOYMD)                        ' �����ٓ��N����
            csRow(ABAtenaEntity.SHOJOJIYUCD) = csJutogaiRow(ABJutogaiEntity.SHOJOJIYUCD)                        ' �������R�R�[�h
            csRow(ABAtenaEntity.SHOJOJIYU) = cIdoJiyuB.GetIdoJiyu(csJutogaiRow(ABJutogaiEntity.SHOJOJIYUCD).ToString)       ' �������R
            csRow(ABAtenaEntity.GYOSEIKUCD) = csJutogaiRow(ABJutogaiEntity.GYOSEIKUCD)                          ' �s����R�[�h
            csRow(ABAtenaEntity.GYOSEIKUMEI) = csJutogaiRow(ABJutogaiEntity.GYOSEIKUMEI)                        ' �s���於
            csRow(ABAtenaEntity.CHIKUCD1) = csJutogaiRow(ABJutogaiEntity.CHIKUCD1)                              ' �n��R�[�h1
            csRow(ABAtenaEntity.CHIKUMEI1) = csJutogaiRow(ABJutogaiEntity.CHIKUMEI1)                            ' �n�於1
            csRow(ABAtenaEntity.CHIKUCD2) = csJutogaiRow(ABJutogaiEntity.CHIKUCD2)                              ' �n��R�[�h2
            csRow(ABAtenaEntity.CHIKUMEI2) = csJutogaiRow(ABJutogaiEntity.CHIKUMEI2)                            ' �n�於2
            csRow(ABAtenaEntity.CHIKUCD3) = csJutogaiRow(ABJutogaiEntity.CHIKUCD3)                              ' �n��R�[�h3
            csRow(ABAtenaEntity.CHIKUMEI3) = csJutogaiRow(ABJutogaiEntity.CHIKUMEI3)                            ' �n�於3
            csRow(ABAtenaEntity.KAOKUSHIKIKB) = csJutogaiRow(ABJutogaiEntity.KAOKUSHIKIKB)                      ' �Ɖ��~�敪
            csRow(ABAtenaEntity.BIKOZEIMOKU) = csJutogaiRow(ABJutogaiEntity.BIKOZEIMOKU)                        ' ���l�Ŗ�

            ' �V�K�쐬�̏ꍇ
            If IntKoshinKB = cABEnumDefine.KoshinKB.Insert Then
                csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Rows.Add(csRow)
            End If

            ' �f�o�b�O�I�����O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

    End Sub

    '************************************************************************************************
    '* ���\�b�h��     ��������ҏW����
    '* 
    '* �\��           Public Sub EditAtenaRireki(ByVal csAtenaEntity As DataSet, _
    '* �@�@                                  ByVal csAtenaRirekiEntity As DataSet)
    '* 
    '* �@�\�@�@       ���������̕ҏW���s�Ȃ��B
    '* 
    '* ����           csAtenaEntity        As DataSet  : ����(ABAtenaEntity)
    '* �@�@           csAtenaRirekiEntity  As DataSet  : ��������(ABAtenaRirekiEntity)
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Private Overloads Sub EditAtenaRireki(ByVal StrIdoYMD As String, _
                                          ByVal csAtenaEntity As DataSet, _
                                          ByRef csAtenaRirekiEntity As DataSet)
        Const THIS_METHOD_NAME As String = "EditAtenaRireki"                    '���\�b�h��
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                                     '�G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim csRow As DataRow
        Dim csRows As DataRow()
        Dim csColumn As DataColumn
        Dim strSearchKana(4) As String                                          '�����p�J�i
        Dim csAtenaRow As DataRow                                               '����DataRow
        Dim csAtenaRows As DataRow()
        Dim strSystemDate As String                                             '�V�X�e�����t
        '*����ԍ� 000005 2006/09/13 �ǉ��J�n
        Dim csRirekiRow As DataRow                ' �i���݁E�\�[�g���{�������R�[�h����
        Dim strST_YMD As String                   ' �J�n�N����
        Dim strED_YMD As String                   ' �I���N����
        Dim blnHit As Boolean = False             ' ���Ă͂܂������ǂ���
        Dim strRirekiNO As String
        '*����ԍ� 000005 2006/09/13 �ǉ��I��

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �K�v�Ȑݒ���s��
            m_cfDateClass.p_enDateSeparator = UFDateSeparator.None
            m_cfDateClass.p_enEraType = UFEraType.Number

            strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMdd")        '�V�X�e�����t

            ' ����Row���擾����
            csAtenaRows = csAtenaEntity.Tables(ABAtenaEntity.TABLE_NAME).Select(ABAtenaEntity.JUMINJUTOGAIKB + "='2'")
            csAtenaRow = csAtenaRows(0)

            ' �����������V����Row���擾����
            csRow = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).NewRow
            ' DataRow�̏�����
            m_cCommonClass.InitColumnValue(csRow)

            '**
            '* �ҏW����
            '*

            If (csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Count = 0) Then
                ' ����ԍ�
                csRow(ABAtenaRirekiEntity.RIREKINO) = "0001"

                '*����ԍ� 000005 2006/09/13 �ǉ��J�n
                ' �����}�X�^�����������ւ��̂܂ܕҏW����
                For Each csColumn In csAtenaRow.Table.Columns
                    csRow(csColumn.ColumnName) = csAtenaRow(csColumn)
                Next csColumn

                ' ���������֒ǉ�����
                csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csRow)
                '*����ԍ� 000005 2006/09/13 �ǉ��I��
            Else
                '*����ԍ� 000005 2006/09/13 �C���J�n
                '* corresponds to VS2008 Start 2010/04/16 000007
                ''csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
                '''' �����I�����ɃV�X�e�����t�̑O����ݒ肷��
                ''m_cfDateClass.p_strDateValue = StrIdoYMD
                ''csRows(0).BeginEdit()
                ''csRows(0).Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                ''csRows(0).EndEdit()

                '''' ����ԍ�
                ''csRow(ABAtenaRirekiEntity.RIREKINO) = CType((CType(csRows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).PadLeft(4, "0"c)
                '* corresponds to VS2008 End 2010/04/16 000007

                ' �ǉ����郌�R�[�h�p�ɗ���ԍ����擾����
                csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select("", ABAtenaRirekiEntity.RIREKINO + " DESC")
                strRirekiNO = CType((CType(csRows(0).Item(ABAtenaRirekiEntity.RIREKINO), Integer) + 1), String).RPadLeft(4, "0"c)

                ' �Z���Z�o�O�敪="2"�Œ��o���A�����J�n�N���������E����ԍ������Ƀ\�[�g����
                csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2'", ABAtenaRirekiEntity.RRKST_YMD + " ASC , " + ABAtenaRirekiEntity.RIREKINO + " ASC")

                ' �ǂ̃��R�[�h�̊J�n�`�I���ɓ��Ă͂܂邩�𒲂ׂ�
                For Each csRirekiRow In csRows
                    ' �J�n�E�I���N�������擾
                    strST_YMD = CStr(csRirekiRow.Item(ABAtenaRirekiEntity.RRKST_YMD))
                    strED_YMD = CStr(csRirekiRow.Item(ABAtenaRirekiEntity.RRKED_YMD))

                    If blnHit = False Then
                        ' �܂����Ă͂܂郌�R�[�h���������Ă��Ȃ�
                        If strST_YMD > StrIdoYMD Then
                            ' �J�n�N������StrIdoYMD

                            blnHit = True   ' �t���O��True�ɂ��āA����ȍ~�̃��R�[�h�̍X�V���s��

                        ElseIf (strST_YMD <= StrIdoYMD AndAlso StrIdoYMD <= strED_YMD) AndAlso _
                                strED_YMD <> "99999999" Then
                            ' �J�n�N������StrIdoYMD���I���N����
                            ' ����
                            ' �I���N������"99999999"�łȂ�

                            blnHit = True   ' �t���O��True�ɂ��āA����ȍ~�̃��R�[�h�̍X�V���s��

                        End If
                    End If

                    ' ���Ă͂܂郌�R�[�h�����������ꍇ
                    If blnHit = True Then
                        ' �����}�X�^�����������ւ��̂܂ܕҏW����
                        For Each csColumn In csAtenaRow.Table.Columns
                            If csColumn.ColumnName <> ABAtenaRirekiEntity.JUMINCD AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.RIREKINO AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.RRKST_YMD AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.RRKED_YMD AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.CKINIDOYMD AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.SAKUSEINICHIJI AndAlso _
                               csColumn.ColumnName <> ABAtenaRirekiEntity.SAKUSEIUSER Then
                                ' �Z��CD�E����ԍ��E�J�n�E�I���E���߈ٓ��N�����E�쐬�����E�쐬���[�U�ȊO���㏑������

                                csRirekiRow(csColumn.ColumnName) = csAtenaRow(csColumn)
                            End If
                        Next csColumn
                    End If

                Next csRirekiRow

                ' ���Ă͂܂郌�R�[�h��������Ȃ������ꍇ�A���߂ŕ�������
                If blnHit = False Then
                    ' �Z���Z�o�O�敪="2"�A�����I���N����="99999999"�Œ��o
                    csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2' AND " + ABAtenaRirekiEntity.RRKED_YMD + " = '99999999'")
                    If csRows.Length > 0 Then
                        m_cfDateClass.p_strDateValue = StrIdoYMD
                        ' ���߃��R�[�h�̏I���N������StrIdoYMD�̈���O�̒l�ōX�V����
                        csRows(0).BeginEdit()
                        csRows(0).Item(ABAtenaRirekiEntity.RRKED_YMD) = m_cfDateClass.AddDay(-1)
                        csRows(0).EndEdit()
                    End If

                    ' �����}�X�^�����������ւ��̂܂ܕҏW����
                    For Each csColumn In csAtenaRow.Table.Columns
                        csRow(csColumn.ColumnName) = csAtenaRow(csColumn)
                    Next csColumn

                    ' ����ԍ���ݒ肷��
                    csRow(ABAtenaRirekiEntity.RIREKINO) = strRirekiNO

                    ' ���������֒ǉ�����
                    csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csRow)
                End If
                '*����ԍ� 000005 2006/09/13 �C���I��
            End If

            '*����ԍ� 000005 2006/09/13 �폜�J�n
            '* corresponds to VS2008 Start 2010/04/16 000007
            '''' �����}�X�^�����������ւ��̂܂ܕҏW����
            ''For Each csColumn In csAtenaRow.Table.Columns
            ''    csRow(csColumn.ColumnName) = csAtenaRow(csColumn)
            ''Next csColumn

            '''' ���������֒ǉ�����
            ''csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows.Add(csRow)
            '*����ԍ� 000005 2006/09/13 �폜�I��
            '* corresponds to VS2008 End 2010/04/16 000007

            ' �f�o�b�O�I�����O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

    End Sub

    '************************************************************************************************
    '* ���\�b�h��     �����ݐϏ���
    '* 
    '* �\��           Private Sub EditAtenaRuiseki(ByVal csAtenaRirekiEntity As DataSet, _
    '*                                             ByRef csAtenaRuisekiEntity As DataSet, _
    '*                                             ByVal csRirekiCkinRow As DataRow)
    '* 
    '* �@�\�@�@       ���������̕ҏW���s�Ȃ��B
    '* 
    '* ����           csAtenaRirekiEntity   As DataSet  : ��������(ABAtenaRirekiEntity)
    '* �@�@           csAtenaRuisekiEntity  As DataSet  : �����ݐ�(ABAtenaRuisekiEntity)
    '* �@�@           csRirekiCkinRow       As DataRow  : ���������O�̗��𒼋߃��E
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    '*����ԍ� 000005 2006/09/13 �C���J�n
    ''Private Sub EditAtenaRuiseki(ByVal csAtenaRirekiEntity As DataSet, _
    ''ByRef csAtenaRuisekiEntity As DataSet)
    Private Sub EditAtenaRuiseki(ByVal csAtenaRirekiEntity As DataSet, _
                                 ByRef csAtenaRuisekiEntity As DataSet, _
                                 ByVal csRirekiCkinRow As DataRow)
        '*����ԍ� 000005 2006/09/13 �C���I��
        Const THIS_METHOD_NAME As String = "EditAtenaRuiseki"                   '���\�b�h��
        '* corresponds to VS2008 Start 2010/04/16 000007
        'Dim objErrorStruct As UFErrorStruct                                     ' �G���[��`�\����
        '* corresponds to VS2008 End 2010/04/16 000007
        Dim csRow As DataRow
        Dim csRows As DataRow()
        Dim csColumn As DataColumn
        Dim strSearchKana(4) As String                                          '�����p�J�i
        Dim csAtenaRirekiRow As DataRow                                         '��������DataRow
        Dim strSystemDate As String                                             '�V�X�e�����t
        '*����ԍ� 000005 2006/09/13 �ǉ��J�n
        Dim blnAtoAdd As Boolean = False                                        '��̃��R�[�h��ǉ��������ǂ���
        '*����ԍ� 000005 2006/09/13 �ǉ��I��

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            '*����ԍ� 000004 2006/05/31 �ǉ��J�n
            strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff")                   '�V�X�e�����t
            '*����ԍ� 000004 2006/05/31 �ǉ��I��

            '*����ԍ� 000005 2006/09/13 �ǉ��J�n
            ' �ݐ�(�O)�𐶐����ǉ�����
            ' �ݐ�(�O)�͑���O�̗��𒼋߃��R�[�h�Ƃ���
            If Not (csRirekiCkinRow Is Nothing) Then
                ' �����ݐς��V����Row���擾����
                csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                ' DataRow�̏�����
                m_cCommonClass.InitColumnValue(csRow)

                ' ��������
                csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate
                ' �O��敪
                csRow(ABAtenaRuisekiEntity.ZENGOKB) = "1"

                ' �������ݐ�
                For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                    csRow(csColumn.ColumnName) = csRirekiCkinRow(csColumn)
                Next csColumn

                ' �����ݐς֒ǉ�����
                csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)
            End If
            '*����ԍ� 000005 2006/09/13 �ǉ��I��

            For Each csAtenaRirekiRow In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Rows

                If csAtenaRirekiRow.RowState = DataRowState.Added Then
                    ' �����ݐς��V����Row���擾����
                    csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                    ' DataRow�̏�����
                    m_cCommonClass.InitColumnValue(csRow)

                    ' ��������
                    csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate
                    ' �O��敪
                    csRow(ABAtenaRuisekiEntity.ZENGOKB) = "2"

                    ' ���������}�X�^�������ݐςւ��̂܂ܕҏW����
                    For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                        csRow(csColumn.ColumnName) = csAtenaRirekiRow(csColumn)
                    Next csColumn

                    ' �����ݐς֒ǉ�����
                    csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)

                    blnAtoAdd = True
                    Exit For
                End If
                '* corresponds to VS2008 Start 2010/04/16 000007
                ''*����ԍ� 000005 2006/09/13 �폜�J�n
                ''''Select Case csAtenaRirekiRow.RowState
                ''''    Case DataRowState.Added

                ''''        ' �����ݐς��V����Row���擾����
                ''''        csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                ''''        ' DataRow�̏�����
                ''''        m_cCommonClass.InitColumnValue(csRow)

                ''''        '**
                ''''        '* �ҏW����
                ''''        '*

                ''''        ' ��������
                ''''        '*����ԍ� 000004 2006/05/31 �폜�J�n
                ''''        'strSystemDate = m_cfRdb.GetSystemDate().ToString("yyyyMMddHHmmssfff")                   '�V�X�e�����t
                ''''        '*����ԍ� 000004 2006/05/31 �폜�I��
                ''''        csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate

                ''''        ' �O��敪
                ''''        csRow(ABAtenaRuisekiEntity.ZENGOKB) = "2"

                ''''        ' �����}�X�^�����������ւ��̂܂ܕҏW����
                ''''        For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                ''''            csRow(csColumn.ColumnName) = csAtenaRirekiRow(csColumn)
                ''''        Next csColumn

                ''''        ' �����ݐς֒ǉ�����
                ''''        csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)

                ''''        '*����ԍ� 000004 2006/05/31 �ǉ��J�n
                ''''    Case DataRowState.Modified
                ''''        ' �����ݐς��V����Row���擾����
                ''''        csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                ''''        ' DataRow�̏�����
                ''''        m_cCommonClass.InitColumnValue(csRow)

                ''''        '**
                ''''        '* �ҏW����
                ''''        '*

                ''''        ' ��������
                ''''        csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate

                ''''        ' �O��敪
                ''''        csRow(ABAtenaRuisekiEntity.ZENGOKB) = "1"

                ''''        ' ���������f�[�^�������ݐςւ��̂܂ܕҏW����
                ''''        For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                ''''            csRow(csColumn.ColumnName) = csAtenaRirekiRow(csColumn)
                ''''        Next csColumn

                ''''        ' �����ݐς֒ǉ�����
                ''''        csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)
                ''''        '*����ԍ� 000004 2006/05/31 �ǉ��I��
                ''''End Select
                ''*����ԍ� 000005 2006/09/13 �폜�I��
                '* corresponds to VS2008 End 2010/04/16 000007
            Next csAtenaRirekiRow

            ' �����ŗݐ�(��)���܂��ǉ�����Ă��Ȃ��ꍇ(�ǉ��Ȃ��ōX�V���������̏ꍇ)
            If blnAtoAdd = False Then
                ' �����̗��𒼋߃��R�[�h���擾����
                csRows = csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Select(ABAtenaRirekiEntity.JUMINJUTOGAIKB + " = '2' AND " + ABAtenaRirekiEntity.RRKED_YMD + " = '99999999'")

                ' �����ݐς��V����Row���擾����
                csRow = csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).NewRow
                ' DataRow�̏�����
                m_cCommonClass.InitColumnValue(csRow)

                ' ��������
                csRow(ABAtenaRuisekiEntity.SHORINICHIJI) = strSystemDate
                ' �O��敪
                csRow(ABAtenaRuisekiEntity.ZENGOKB) = "2"

                ' ���������}�X�^�������ݐςւ��̂܂ܕҏW����
                For Each csColumn In csAtenaRirekiEntity.Tables(ABAtenaRirekiEntity.TABLE_NAME).Columns
                    csRow(csColumn.ColumnName) = csRows(0)(csColumn)
                Next csColumn

                ' �����ݐς֒ǉ�����
                csAtenaRuisekiEntity.Tables(ABAtenaRuisekiEntity.TABLE_NAME).Rows.Add(csRow)

            End If

            ' �f�o�b�O�I�����O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

    End Sub


    '************************************************************************************************
    '* ���\�b�h��     �����p�J�i�擾
    '* 
    '* �\��           Public Function GetSearchKana(ByVal strKanaMeisho As String) As String()
    '* 
    '* �@�\�@�@       �����p�J�i���̂�ҏW����
    '* 
    '* ����           strKanaMeisho As String     : �J�i����
    '* 
    '* �߂�l         String()        : [0]�����p�J�i����
    '*                                  : [1]�����p�J�i��
    '*                                  : [2]�����p�J�i��
    '*                                  : [3]�J�i��
    '*                                  : [4]�J�i��
    '************************************************************************************************
    Private Function GetSearchKana(ByVal strKanaMeisho As String) As String()
        Const THIS_METHOD_NAME As String = "GetSearchKana"                      '���\�b�h��
        Dim strSearchKana(4) As String                      '�����p�J�i
        Dim cuString As New USStringClass()                 '������ҏW
        Dim intIndex As Integer                             '�擪����̋󔒈ʒu

        Try
            ' �f�o�b�O�J�n���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �J�i�����i�󔒂��l�߂�j
            strSearchKana(0) = cuString.ToKanaKey(strKanaMeisho).Replace(" ", String.Empty)

            ' �擪����̋󔒈ʒu�𒲂ׂ�
            intIndex = strKanaMeisho.RIndexOf(" ")

            ' �󔒂����݂��Ȃ��ꍇ
            If (intIndex = -1) Then
                ' �J�i���E��
                strSearchKana(1) = strSearchKana(0)
                strSearchKana(3) = strKanaMeisho
                strSearchKana(2) = String.Empty
                strSearchKana(4) = String.Empty
            Else
                ' �J�i���E��
                strSearchKana(1) = cuString.ToKanaKey(strKanaMeisho.RSubstring(0, intIndex))
                strSearchKana(3) = strKanaMeisho.RSubstring(0, intIndex)

                ' �擪����̋󔒈ʒu�������񒷂ƈȏ�ꍇ
                If ((intIndex + 1) >= strKanaMeisho.RLength) Then
                    strSearchKana(2) = String.Empty
                    strSearchKana(4) = String.Empty
                Else
                    strSearchKana(2) = cuString.ToKanaKey(strKanaMeisho.RSubstring(intIndex + 1))
                    strSearchKana(4) = strKanaMeisho.RSubstring(intIndex + 1)
                End If
            End If

            ' �f�o�b�O�I�����O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)


        Catch objAppExp As UFAppException    ' UFAppException���L���b�`
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �V�X�e���G���[���X���[����
            Throw objExp
        End Try

        Return strSearchKana

    End Function

    '*����ԍ� 000003 2006/03/27 �ǉ��J�n
    '************************************************************************************************
    '* ���\�b�h��      �������v���J�f�[�^�X�V
    '* 
    '* �\��            Public Sub AtenaDataReplicaKoshin(ByVal strJuminCD As String, _
    '*                                      ByVal strStaiCD As String, ByVal strKoshinKB As String)
    '* 
    '* �@�\�@�@        �������v���J�f�[�^�̍X�V�������s�Ȃ�
    '* 
    '* ����           strJuminCD�F�Z���R�[�h
    '*                  strStaiCD�F���уR�[�h
    '*                  strKoshinKB�F�X�V�敪�i�ǉ��F1�@�C���F2�j
    '* 
    '* �߂�l         �Ȃ�
    '************************************************************************************************
    Public Sub AtenaDataReplicaKoshin(ByVal strJuminCD As String, ByVal strStaiCD As String, ByVal strKoshinKB As String)
        Const THIS_METHOD_NAME As String = "AtenaDataReplicaKoshin"
        Const WORK_FLOW_NAME As String = "�����ٓ�"             ' ���[�N�t���[��
        Const DATA_NAME As String = "����"                      '�f�[�^��
        Dim cAtenaKanriJohoB As ABAtenaKanriJohoBClass      '�����Ǘ����c�`�r�W�l�X�N���X
        Dim csAtenaKanriEntity As DataSet                   '�����Ǘ����f�[�^�Z�b�g
        Dim csABToshoPrmEntity As New DataSet()             '���v���J�쐬�p�p�����[�^�f�[�^�Z�b�g
        Dim csABToshoPrmTable As DataTable                  '���v���J�쐬�p�p�����[�^�f�[�^�e�[�u��
        Dim csABToshoPrmRow As DataRow                      '���v���J�쐬�p�p�����[�^�f�[�^�e�[�u��
        Dim cABAtenaCnvBClass As ABAtenaCnvBClass


        Try
            ' �f�o�b�O���O�o��
            m_cfLog.DebugStartWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

            ' �����Ǘ����a�N���X�̃C���X�^���X�쐬
            cAtenaKanriJohoB = New ABAtenaKanriJohoBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
            '  �����Ǘ����̎��04���ʃL�[01�̃f�[�^��S���擾����
            csAtenaKanriEntity = cAtenaKanriJohoB.GetKanriJohoHoshu("04", "01")

            '�Ǘ����̃��[�N�t���[���R�[�h�����݂��A�p�����[�^��"1"��"2"�̎��������[�N�t���[�������s��
            If Not (csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows.Count = 0) AndAlso _
                    (CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "1" OrElse _
                        CStr(csAtenaKanriEntity.Tables(ABAtenaKanriJohoEntity.TABLE_NAME).Rows(0).Item(ABAtenaKanriJohoEntity.PARAMETER)) = "2") Then

                '�f�[�^�Z�b�g�擾�N���X�̃C���X�^���X��
                cABAtenaCnvBClass = New ABAtenaCnvBClass(m_cfControlData, m_cfConfigData, m_cfRdb)
                ' �e�[�u���Z�b�g�̎擾
                csABToshoPrmTable = cABAtenaCnvBClass.CreateColumnsToshoPrmData()
                csABToshoPrmTable.TableName = ABToshoPrmEntity.TABLE_NAME
                ' �f�[�^�Z�b�g�Ƀe�[�u���Z�b�g�̒ǉ�
                csABToshoPrmEntity.Tables.Add(csABToshoPrmTable)

                '�V�K���E�̍쐬
                csABToshoPrmRow = csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).NewRow()
                '���v���J�f�[�^�쐬�p�p�����[�^�ɃZ�b�g
                csABToshoPrmRow.Item(ABToshoPrmEntity.JUMINCD) = strJuminCD                 '�Z���R�[�h
                csABToshoPrmRow.Item(ABToshoPrmEntity.STAICD) = strStaiCD                   '���уR�[�h
                csABToshoPrmRow.Item(ABToshoPrmEntity.KOSHINKB) = strKoshinKB               '�X�V�敪�i�ǉ�:1 �C��:2 �_���폜:9 �폜�f�[�^��:2 �����폜:D�j
                '�f�[�^�Z�b�g�Ƀ��E��ǉ�����
                csABToshoPrmEntity.Tables(ABToshoPrmEntity.TABLE_NAME).Rows.Add(csABToshoPrmRow)

                '���[�N�t���[���M�����Ăяo��
                cABAtenaCnvBClass.WorkFlowExec(csABToshoPrmEntity, WORK_FLOW_NAME, DATA_NAME)

            End If

            ' �f�o�b�O���O�o��
            m_cfLog.DebugEndWrite(m_cfControlData, THIS_CLASS_NAME, THIS_METHOD_NAME)

        Catch objAppExp As UFAppException
            ' ���[�j���O���O�o��
            m_cfLog.WarningWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y���[�j���O�R�[�h:" + objAppExp.p_strErrorCode + "�z" + _
                                        "�y���[�j���O���e:" + objAppExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objAppExp

        Catch objExp As Exception
            ' �G���[���O�o��
            m_cfLog.ErrorWrite(m_cfControlData, _
                                        "�y�N���X��:" + THIS_CLASS_NAME + "�z" + _
                                        "�y���\�b�h��:" + THIS_METHOD_NAME + "�z" + _
                                        "�y�G���[���e:" + objExp.Message + "�z")
            ' �G���[�����̂܂܃X���[����
            Throw objExp
        End Try

    End Sub
    '*����ԍ� 000003 2006/03/27 �ǉ��I��

End Class
