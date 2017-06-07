Imports Caliburn.Micro
Imports Hotelie.Application.Bills.Factories.CreateBill
Imports Hotelie.Application.Leases.Queries.GetSimpleLeasesList
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls

Namespace Bills.ViewModels
	Public Class BillsWorkspaceViewModel
		Inherits Screen
		Implements INeedWindowModals

		Private _displayCode As Integer

		Public Property ScreenAddBill As ScreenAddBillViewModel

		Public Property DisplayCode As Integer
			Get
				Return _displayCode
			End Get
			Set
				If Equals( Value, _displayCode ) Then Return
				_displayCode = value
				NotifyOfPropertyChange( Function() DisplayCode )
			End Set
		End Property

		Public Sub New( inventory As IInventory,
		                getSimpleRoomsListQuery As IGetSimpleRoomsListQuery,
		                getSimpleLeasesListQuery As IGetSimpleLeasesListQuery,
		                createBillFactory As ICreateBillFactory )
			DisplayName = "Thanh toán"

			ScreenAddBill = New ScreenAddBillViewModel( Me,
			                                            inventory,
			                                            getSimpleRoomsListQuery,
			                                            getSimpleLeasesListQuery,
			                                            createBillFactory )
			InitializeComponents()
		End Sub

		Private Async Sub InitializeComponents()
			ShowStaticWindowLoadingDialog()
			Await InitAsync()
			Await Task.Delay( 100 ) 'allow binding
			CloseStaticWindowDialog()
		End Sub

		Private Sub Init()
			ScreenAddBill.Init()
			DisplayCode = 0
		End Sub

		Private Async Function InitAsync() As Task
			Await ScreenAddBill.InitAsync()
			DisplayCode = 0
		End Function
	End Class
End Namespace
