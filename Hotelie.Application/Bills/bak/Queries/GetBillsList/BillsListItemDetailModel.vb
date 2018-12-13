Imports Caliburn.Micro

Namespace Bills.Queries.GetBillsList
    Public Class BillsListItemDetailModel
        Inherits PropertyChangedBase

        Private _leaseId As String
        Private _roomName As String
        Private _customerNames As IObservableCollection(Of String)
        Private _roomPrice As Decimal
        Private _checkinDate As Date
        Private _numberOfDays As Integer

        Public Sub New()
            CustomerNames = New BindableCollection(Of String)
        End Sub
        Property LeaseId As String
            Get
                Return _leaseId
            End Get
            Set
                If Equals(_leaseId, Value) Then Return
                _leaseId = value
                NotifyOfPropertyChange(Function() LeaseId)
            End Set
        End Property

        Property RoomName As String
            Get
                Return _roomName
            End Get
            Set
                If Equals(_roomName, Value) Then Return
                _roomName = value
                NotifyOfPropertyChange(Function() RoomName)
            End Set
        End Property

        Property CustomerNames As IObservableCollection(Of String)
            Get
                Return _customerNames
            End Get
            Set
                If Equals(_customerNames, Value) Then Return
                _customerNames = value
                NotifyOfPropertyChange(Function() CustomerNames)
            End Set
        End Property

        Property RoomPrice As Decimal
            Get
                Return _roomPrice
            End Get
            Set
                If Equals(_roomPrice, Value) Then Return
                _roomPrice = value
                NotifyOfPropertyChange(Function() RoomPrice)
            End Set
        End Property

        Property CheckinDate As Date
            Get
                Return _checkinDate
            End Get
            Set
                If Equals(_checkinDate, Value) Then Return
                _checkinDate = Value
                NotifyOfPropertyChange(Function() CheckinDate)
            End Set
        End Property

        Property NumberOfDays As Integer
            Get
                Return _numberOfDays
            End Get
            Set
                If Equals(_numberOfDays, Value) Then Return
                _numberOfDays = Value
                NotifyOfPropertyChange(Function() NumberOfDays)
            End Set
        End Property
    End Class
End NameSpace