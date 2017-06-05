Imports Hotelie.Application.Leases.Queries

Namespace Tests.Leases.Queries.GetLeasesList
	Public Class GetLeasesListQuery
		Implements IGetLeasesListQuery

		Public Function Execute() As IEnumerable(Of LeaseModel) Implements IGetLeasesListQuery.Execute
			Return LeasesTest.Leases
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of LeaseModel)) Implements IGetLeasesListQuery.ExecuteAsync
			Return Await Task.Run(Function() Execute())
		End Function

	End Class
End Namespace