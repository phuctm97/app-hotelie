Imports Caliburn.Micro

Namespace Bills.Queries.GetBillsList
    Public Class BillsListItemModel
        Inherits PropertyChangedBase

        Private _id As String
        Private _customerName As String
        Private _totalPrice As Decimal
        Private _billDetails As IObservableCollection(Of BillsListItemDetailModel)

        Public Sub New()
            BillDetails = New BindableCollection(Of BillsListItemDetailModel)
        End Sub

        Property Id As String
            Get
                Return _id
            End Get
            Set
                If Equals(_id, Value) Then Return
                _id = value
                NotifyOfPropertyChange( Function() Id )
            End Set
        End Property

        Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set
                If Equals(_customerName, Value) Then Return
                _customerName = value
                NotifyOfPropertyChange( Function() CustomerName )
            End Set
        End Property

        Property TotalPrice As Decimal
            Get
                Return _totalPrice
            End Get
            Set
                If Equals(_totalPrice, Value) Then Return
                _totalPrice = value
                NotifyOfPropertyChange( Function() TotalPrice )
            End Set
        End Property

        Property BillDetails As IObservableCollection(Of BillsListItemDetailModel)
            Get
                Return _billDetails
            End Get
            Set
                If Equals(_billDetails, Value) Then Return
                _billDetails = value
                NotifyOfPropertyChange( Function() BillDetails )
            End Set
        End Property
    End Class
End NameSpace