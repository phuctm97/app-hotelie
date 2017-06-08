Imports Hotelie.Domain.Bills
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Rooms

Namespace Bills.Queries.GetBillDetailData
    Public Class BillDetailModel
        Private ReadOnly _entity As BillDetail

        Sub New(entity As BillDetail)
            _entity = entity
        End Sub

        Public ReadOnly Property Id As String
        Get
                Return _entity.Id
        End Get
        End Property

        Public ReadOnly Property Lease As Lease
            Get
                Return _entity.Lease
            End Get
        End Property

        Public ReadOnly Property ExtraCharge As Decimal
            Get
                Return _entity.ExtraCharge
            End Get
        End Property

        Public ReadOnly Property NumberOfDays As Integer
            Get
                Return _entity.NumberOfDays
            End Get
        End Property

        Public ReadOnly Property TotalExpense As Decimal
            Get
                Return _entity.TotalExpense
            End Get
        End Property

        Public ReadOnly Property CheckinDate As Date
            Get
                Return _entity.CheckinDate
            End Get
        End Property

    End Class
End NameSpace