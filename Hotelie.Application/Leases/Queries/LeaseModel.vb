
Imports Caliburn.Micro

Namespace Leases.Queries
    Public Class LeaseModel
        Inherits PropertyChangedBase

        Private _id As String
        Private _roomName As String
        Private _roomCategoryName As String
        Private _beginDate As Date
        Private _endDate As Date
        Private _customers As IObservableCollection(Of LeaseCustomerModel)
        Private _price As Decimal
        Private _totalPrice As Decimal

        Public Property Id As String
            Get
                Return _id
            End Get
            Set
                If Equals(Value, _id) Then Return
                _id = value
                NotifyOfPropertyChange(Function() Id)
            End Set
        End Property

        Public Property RoomName As String
            Get
                Return _roomName
            End Get
            Set
                If Equals(Value, _roomName) Then Return
                _roomName = value
                NotifyOfPropertyChange(Function() RoomName)
            End Set
        End Property
        Public Property RoomCategoryName As String
            Get
                Return _roomCategoryName
            End Get
            Set
                If Equals(Value, _roomCategoryName) Then Return
                _roomCategoryName = value
                NotifyOfPropertyChange(Function() RoomName)
            End Set
        End Property


        Public Property BeginDate As DateTime
            Get
                Return _beginDate
            End Get
            Set
                If Equals(Value, _beginDate) Then Return
                _beginDate = value
                NotifyOfPropertyChange(Function() BeginDate)
            End Set
        End Property

        Public Property EndDate As DateTime
            Get
                Return _endDate
            End Get
            Set
                If Equals(Value, _endDate) Then Return
                _endDate = value
                NotifyOfPropertyChange(Function() EndDate)
            End Set
        End Property

        Public Property Customers As IObservableCollection(Of LeaseCustomerModel)
            Get
                Return _customers
            End Get
            Set
                If Equals(Value, _customers) Then Return
                _customers = Value
                NotifyOfPropertyChange(Function() Customers)
            End Set
        End Property

        Public Property Price As Decimal
            Get
                Return _price
            End Get
            Set
                If Equals(Value, _price) Then Return
                _price = Value
                NotifyOfPropertyChange(Function() Price)
            End Set
        End Property

        Public Property TotalPrice As Decimal
            Get
                Return _totalPrice
            End Get
            Set
                If Equals(Value, _totalPrice) Then Return
                _totalPrice = Value
                NotifyOfPropertyChange(Function() TotalPrice)
            End Set
        End Property
        
    End Class
End NameSpace