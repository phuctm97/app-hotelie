
Imports Caliburn.Micro

Namespace Leases.Queries
    Public Class LeaseModel
        Inherits PropertyChangedBase

        Private _id As String
        Private _roomName As String
        Private _beginDate As Date
        Private _endDate As Date
        Private _numberOfCustomers As Integer

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

        Public Property NumberOfCustomers As Integer
            Get
                Return _numberOfCustomers
            End Get
            Set
                If Equals(Value, _numberOfCustomers) Then Return
                _numberOfCustomers = Value
                NotifyOfPropertyChange(Function() NumberOfCustomers)
            End Set
        End Property

    End Class
End NameSpace