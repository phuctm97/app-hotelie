Imports Caliburn.Micro
Imports Hotelie.Presentation.Common.Controls

Namespace Leases.ViewModels
	Public Class LeasesWorkspaceViewModel
		Inherits Screen
		Implements INeedWindowModals

		' Dependencies
		Private _displayCode As Integer

		Public ReadOnly Property ScreenLeasesList As ScreenLeasesListViewModel

		Public Property ScreenLeaseDetail As ScreenLeaseDetailViewModel

		Public Property ScreenAddLease As ScreenAddLeaseViewModel

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

		Public Sub New( getLeasesListQuery As Hotelie.Application.Leases.Queries.GetLeasesList.IGetLeasesListQuery,
		                getLeaseDataQuery As Hotelie.Application.Leases.Queries.GetLeaseData.IGetLeaseDataQuery,
		                getSimpleRoomsListQuery As _
			              Hotelie.Application.Rooms.Queries.GetSimpleRoomsList.IGetSimpleRoomsListQuery )
			ScreenLeasesList = New ScreenLeasesListViewModel( getLeasesListQuery )

			ScreenLeaseDetail = New ScreenLeaseDetailViewModel( Me,
			                                                    getLeaseDataQuery,
			                                                    getSimpleRoomsListQuery )

			ScreenAddLease = New ScreenAddLeaseViewModel()

			DisplayName = "Thuê phòng"

			DisplayCode = - 1
		End Sub

		Protected Overrides Async Sub OnInitialize()
			MyBase.OnInitialize()

			ShowStaticWindowLoadingDialog()
			Await InitAsync()
			Await Task.Delay( 100 ) 'allow binding
			CloseStaticWindowDialog()
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

		Public Sub NavigateToScreenLeasesList()
			DisplayCode = 0
		End Sub

		Public Sub NavigateToScreenLeaseDetail( leaseListItem As _
			                                      Hotelie.Application.Leases.Queries.GetLeasesList.LeasesListItemModel )
			If IsNothing( leaseListItem ) Then Return

			ScreenLeaseDetail.SetLease( leaseListItem.Id )
			DisplayCode = 1
		End Sub

		Public Sub NavigateToScreenAddLease()
			DisplayCode = 2
		End Sub
	End Class
End Namespace
