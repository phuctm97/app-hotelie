Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Commands
Imports Hotelie.Application.Leases.Factories
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Parameters.Queries
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.WorkspaceShell.ViewModels

Namespace Leases.ViewModels
	Public Class LeasesWorkspaceViewModel
		Inherits AppScreen
		Implements IChild(Of WorkspaceShellViewModel)
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

		' Parent
		Public Property ParentShell As WorkspaceShellViewModel Implements IChild(Of WorkspaceShellViewModel).Parent
			get
				Return TryCast(Parent, WorkspaceShellViewModel)
			End Get
			Set
				Parent = value
				NotifyOfPropertyChange( Function() ParentShell )
			End Set
		End Property

		' Initializations
		Public Sub New( getAllLeasesQuery As IGetAllLeasesQuery,
		                getLeaseQuery As IGetLeaseQuery,
		                getAllRoomsQuery As IGetAllRoomsQuery,
		                getAllCustomerCategoriesQuery As IGetAllCustomerCategoriesQuery,
		                getParametersQuery As IGetParametersQuery,
		                updateLeaseCommand As IUpdateLeaseCommand,
		                updateLeaseDetailCommand As IUpdateLeaseDetailCommand,
		                removeLeaseCommand As IRemoveLeaseCommand,
		                removeLeaseDetailCommand As IRemoveLeaseDetailCommand,
		                createLeaseDetailFactory As ICreateLeaseDetailFactory,
		                createLeaseFactory As ICreateLeaseFactory,
		                inventory As IInventory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ScreenLeasesList = New ScreenLeasesListViewModel( Me,
			                                                  getAllLeasesQuery )

			ScreenLeaseDetail = New ScreenLeaseDetailViewModel( Me,
			                                                    getLeaseQuery,
			                                                    getAllRoomsQuery,
			                                                    getAllCustomerCategoriesQuery,
			                                                    getParametersQuery,
			                                                    updateLeaseCommand,
			                                                    removeLeaseCommand,
			                                                    updateLeaseDetailCommand,
			                                                    removeLeaseDetailCommand,
			                                                    createLeaseDetailFactory,
			                                                    inventory )
			ScreenAddLease = New ScreenAddLeaseViewModel( Me,
			                                              getAllRoomsQuery,
			                                              getAllCustomerCategoriesQuery,
			                                              getParametersQuery,
			                                              createLeaseFactory,
			                                              inventory )

			DisplayName = "Thuê phòng"

			DisplayCode = - 1

			InitializeComponents()
		End Sub

		Private Sub InitializeComponents()
			ScreenLeasesList.Init()
			ScreenLeaseDetail.Init()
			ScreenAddLease.Init()
			DisplayCode = 0
		End Sub

		' Navigations
		Public Sub NavigateToScreenLeasesList()
			DisplayCode = 0
		End Sub

		Public Async Sub NavigateToScreenLeaseDetail( id As String )
			If String.IsNullOrWhiteSpace( id ) Then Return

			Await ScreenLeaseDetail.SetLeaseAsync( id )
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
