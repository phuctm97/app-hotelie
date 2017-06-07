Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls

Namespace Leases.ViewModels
	Public Class LeasesWorkspaceViewModel
		Inherits AppScreen
		Implements INeedWindowModals

		' Backing fields
		Private _displayCode As Integer

		' Screens
		Public ReadOnly Property ScreenLeasesList As ScreenLeasesListViewModel

		Public ReadOnly Property ScreenLeaseDetail As ScreenLeaseDetailViewModel

		Public ReadOnly Property ScreenAddLease As ScreenAddLeaseViewModel

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

		' Initializations
		Public Sub New( getLeasesListQuery As Hotelie.Application.Leases.Queries.GetLeasesList.IGetLeasesListQuery,
		                getLeaseDataQuery As Hotelie.Application.Leases.Queries.GetLeaseData.IGetLeaseDataQuery,
		                getSimpleRoomsListQuery As _
			              Hotelie.Application.Rooms.Queries.GetSimpleRoomsList.IGetSimpleRoomsListQuery,
		                getCustomerCategoriesListQuery As _
			              Hotelie.Application.Leases.Queries.GetCustomerCategoriesList.IGetCustomerCategoriesListQuery,
		                updateLeaseCommand As _
			              Hotelie.Application.Leases.Commands.UpdateLease.IUpdateLeaseCommand,
		                removeLeaseCommand As _
			              Hotelie.Application.Leases.Commands.RemoveLease.IRemoveLeaseCommand,
		                updateLeaseDetailCommand As _
			              Hotelie.Application.Leases.Commands.UpdateLeaseDetail.IUpdateLeaseDetailCommand,
		                removeLeaseDetailCommand As _
			              Hotelie.Application.Leases.Commands.RemoveLeaseDetail.IRemoveLeaseDetailCommand,
		                createLeaseDetailFactory As _
			              Hotelie.Application.Leases.Factories.CreateLeaseDetail.ICreateLeaseDetailFactory,
		                inventory As IInventory,
		                createLeaseFactory As _
			              Hotelie.Application.Leases.Factories.CreateLease.ICreateLeaseFactory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ScreenLeasesList = New ScreenLeasesListViewModel( getLeasesListQuery )

			ScreenLeaseDetail = New ScreenLeaseDetailViewModel( Me,
			                                                    getLeaseDataQuery,
			                                                    getSimpleRoomsListQuery,
			                                                    getCustomerCategoriesListQuery,
			                                                    updateLeaseCommand,
			                                                    removeLeaseCommand,
			                                                    updateLeaseDetailCommand,
			                                                    removeLeaseDetailCommand,
			                                                    createLeaseDetailFactory,
			                                                    inventory )
			ScreenAddLease = New ScreenAddLeaseViewModel( Me,
			                                              getSimpleRoomsListQuery,
			                                              getCustomerCategoriesListQuery,
			                                              createLeaseFactory,
			                                              inventory )

			DisplayName = "Thuê phòng"

			DisplayCode = - 1

			InitializeComponents()
		End Sub

		Private Sub InitializeComponents()
			Init()
		End Sub

		Private Sub Init()
			ScreenLeasesList.Init()
			ScreenLeaseDetail.Init()
			ScreenAddLease.Init()
			DisplayCode = 0
		End Sub

		Private Async Function InitAsync() As Task
			Await ScreenLeasesList.InitAsync()
			Await ScreenLeaseDetail.InitAsync()
			Await ScreenAddLease.InitAsync()
			DisplayCode = 0
		End Function

		' Navigations
		Public Sub NavigateToScreenLeasesList()
			DisplayCode = 0
		End Sub

		Public Async Sub NavigateToScreenLeaseDetail( leaseListItem As _
			                                            Hotelie.Application.Leases.Queries.GetLeasesList.LeasesListItemModel )
			If IsNothing( leaseListItem ) Then Return

			Await ScreenLeaseDetail.SetLeaseAsync( leaseListItem.Id )
			DisplayCode = 1
		End Sub

		Public Sub NavigateToScreenAddLease()
			DisplayCode = 2
		End Sub

		Public Sub NavigateToScreenAddLease( roomId As String )
			ScreenAddLease.SetRoomId( roomId )
			DisplayCode = 2
		End Sub
	End Class
End Namespace
