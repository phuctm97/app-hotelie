Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries

Namespace Leases.ViewModels
	Public Class ScreenLeasesListViewModel

		Public ReadOnly Property Leases As IObservableCollection(Of LeaseModel)

		Public Sub New()
			Leases = New BindableCollection(Of LeaseModel)()
		End Sub

		Public Sub Init()
		End Sub

		Public Async Function InitAsync() As Task
		End Function

		Private Sub InitLeases()

		End Sub
	End Class
End Namespace
