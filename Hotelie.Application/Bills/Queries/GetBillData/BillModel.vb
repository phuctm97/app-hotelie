Imports Hotelie.Domain.Bills

Namespace Bills.Queries.GetBillData
    Public Class BillModel
        Private ReadOnly _entity As Bill
        Public Sub New(entity As Bill)
            _entity = entity
        End Sub

        Public ReadOnly Property Id As String
        Get
                Return _entity.Id
        End Get
        End Property

        Public ReadOnly Property CustomerAddress As String
            Get
                Return _entity.CustomerAddress
            End Get
        End Property

        Public ReadOnly Property CustomerName As String
            Get
                Return _entity.CustomerName
            End Get
        End Property

        Public ReadOnly Property TotalExpense As Decimal
            Get
                Return _entity.TotalExpense
            End Get
        End Property

        Public ReadOnly Property Details As List(Of BillDetail)
            Get
                Return _entity.Details
            End Get
        End Property

    End Class
End NameSpace