Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Presentation.Common.Controls

Namespace Leases.ViewModels
	Public Class LeasesWorkspaceViewModel
		Inherits Screen
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _getLeasesListQuery As IGetLeasesListQuery

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

		Public Sub New( getLeasesListQuery As IGetLeasesListQuery )
			_getLeasesListQuery = getLeasesListQuery

			ScreenLeasesList = New ScreenLeasesListViewModel()

			ScreenLeaseDetail = New ScreenLeaseDetailViewModel()

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

		Public Sub NavigateToScreenLeaseDetail( lease As LeaseModel )
			If IsNothing( lease ) Then Return

			ScreenLeaseDetail.SetLease( lease.Id )
			DisplayCode = 1
		End Sub

		Public Sub NavigateToScreenAddLease()
			DisplayCode = 2
		End Sub
	End Class
End Namespace
