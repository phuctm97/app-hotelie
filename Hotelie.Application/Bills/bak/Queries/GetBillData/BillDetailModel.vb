Imports Hotelie.Domain.Bills

Namespace Bills.Queries.GetBillData
    Public Class BillDetailModel
        Private ReadOnly _entity As BillDetail

        Public Sub New(entity As BillDetail)
            _entity = entity
        End Sub

        Public ReadOnly Property Id As String
        Get
                Return _entity.Id
        End Get
        End Property

        Public ReadOnly Property Lease As String
    End Class
End NameSpace