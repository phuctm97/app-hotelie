Imports Hotelie.Application.Bills.Factories.CreateBill
Imports Hotelie.Application.Leases.Queries.GetSimpleLeasesList
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls

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
				If Equals( Value, _displayCode ) Then Return
				_displayCode = value
				NotifyOfPropertyChange( Function() DisplayCode )
			End Set
		End Property

		Public Sub New( inventory As IInventory,
		                getSimpleRoomsListQuery As IGetSimpleRoomsListQuery,
		                getSimpleLeasesListQuery As IGetSimpleLeasesListQuery,
		                createBillFactory As ICreateBillFactory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			DisplayName = "Thanh toán"

			ScreenBillsList = New ScreenBillsListVIewModel()

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

		Public Sub NavigateToScreenAddBill()
			DisplayCode = 1
		End Sub

		Public Sub NavigateToScreenBillsList()
			DisplayCode = 0
		End Sub
	End Class
End Namespace
