Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries.GetLeasesList

Namespace Leases.ViewModels
	Public Class ScreenLeasesListViewModel
		' Dependencies
		Private ReadOnly _getLeasesListQuery As IGetLeasesListQuery

		' Data
		Public ReadOnly Property Leases As IObservableCollection(Of LeasesListIemModel)

		Public Sub New( getLeasesListQuery As IGetLeasesListQuery )
			_getLeasesListQuery = getLeasesListQuery

			Leases = New BindableCollection(Of LeasesListIemModel)()
		End Sub

		Public Sub Init()
			InitLeases()
		End Sub

		Public Async Function InitAsync() As Task
			Await InitLeasesAsync()
		End Function

		Private Sub InitLeases()
			Leases.Clear()
			Leases.AddRange( _getLeasesListQuery.Execute() )
		End Sub

		Private Async Function InitLeasesAsync() As Task
			Leases.Clear()
			Leases.AddRange( Await _getLeasesListQuery.ExecuteAsync() )
		End Function
	End Class
End Namespace
