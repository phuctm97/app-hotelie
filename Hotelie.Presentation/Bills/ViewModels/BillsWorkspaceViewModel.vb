Imports Hotelie.Application.Bills.Factories
Imports Hotelie.Application.Bills.Queries
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports MaterialDesignThemes.Wpf

Namespace Bills.ViewModels
    Public Class BillsWorkspaceViewModel
        Inherits AppScreen
        Implements INeedWindowModals

        Private _displayCode As Integer

        Public ReadOnly Property ScreenBillsList As ScreenBillsListVIewModel

        Public ReadOnly Property ScreenAddBill As ScreenAddBillViewModel

        Public Property DisplayCode As Integer
            Get
                Return _displayCode
            End Get
            Set
                If Equals(Value, _displayCode) Then Return
                _displayCode = value
                NotifyOfPropertyChange(Function() DisplayCode)
            End Set
        End Property

        Public Sub New(getAllBillsQuery As IGetAllBillsQuery,
                       getAllRoomsQuery As IGetAllRoomsQuery,
                       getAllLeasesQuery As IGetAllLeasesQuery,
                       createBillFactory As ICreateBillFactory,
                       inventory As IInventory)
            MyBase.New(ColorZoneMode.PrimaryDark)

            DisplayName = "Thanh toán"

            ScreenBillsList = New ScreenBillsListVIewModel(Me,
                                                           getAllBillsQuery)

            ScreenAddBill = New ScreenAddBillViewModel(Me,
                                                       getAllRoomsQuery,
                                                       getAllLeasesQuery,
                                                       createBillFactory)
            InitializeComponents()
        End Sub

        Private Async Sub InitializeComponents()
            Init()
        End Sub

        Private Sub Init()
            ScreenBillsList.Init()
            ScreenAddBill.Init()
            DisplayCode = 0
        End Sub

        Private Async Function InitAsync() As Task
            Await ScreenAddBill.InitAsync()
            Await ScreenBillsList.InitAsync()
            DisplayCode = 0
        End Function

        Public Sub NavigateToScreenAddBill()
            DisplayCode = 1
        End Sub

        Public Sub NavigateToScreenAddBillWithRoom(id As String)
            DisplayCode = 1
            ScreenAddBill.InsertRoomId(id)
        End Sub

        Public Sub NavigateToScreenAddBillWithLease(id As String)
            DisplayCode = 1
            ScreenAddBill.InsertLeaseId(id)
        End Sub

        Public Sub NavigateToScreenBillsList()
            DisplayCode = 0
        End Sub
    End Class
End Namespace
