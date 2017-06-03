
Imports Caliburn.Micro

Namespace Leases.Queries
    Public Class LeaseModel
        Inherits PropertyChangedBase

        Private _id As String
        Private _roomId As String
        Private _beginDate As Date
        Private _endDate As Date
        Private _numberOfDate As Integer
        Private _price As Decimal
        Private _extraCoefficient As Double
        Private _customerCoefficient As Double
        Private _extraCharge As Decimal
        Private _billId As String

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

        Public Property RoomId As String
            Get
                Return _roomId
            End Get
            Set
                If Equals(Value, _roomId) Then Return
                _roomId = value
                NotifyOfPropertyChange(Function() RoomId)
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

        Public Property NumberOfDate As Integer
            Get
                Return _numberOfDate
            End Get
            Set
                If Equals(Value, _numberOfDate) Then Return
                _numberOfDate = Value
                NotifyOfPropertyChange(Function() NumberOfDate)
            End Set
        End Property

        Public Property ExtraCoefficient As Double
            Get
                Return _extraCoefficient
            End Get
            Set
                If Equals(Value, _extraCoefficient) Then Return
                _extraCoefficient = Value
                NotifyOfPropertyChange(Function() ExtraCoefficient)
            End Set
        End Property

        Public Property CustomerCoefficient As Double
            Get
                Return _customerCoefficient
            End Get
            Set
                If Equals(Value, _customerCoefficient) Then Return
                _customerCoefficient = Value
                NotifyOfPropertyChange(Function() CustomerCoefficient)
            End Set
        End Property

        Public Property ExtraCharge As Decimal
            Get
                Return _extraCharge

            End Get
            Set
                If Equals(Value, _extraCharge) Then Return
                _extraCharge = Value
                NotifyOfPropertyChange(Function() ExtraCharge)
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

        Public Property BillId As String
            Get
                Return _billId

            End Get
            Set
                If Equals(Value, _billId) Then Return
                _billId = Value
                NotifyOfPropertyChange(Function() BillId)
            End Set
        End Property
    End Class
End NameSpace