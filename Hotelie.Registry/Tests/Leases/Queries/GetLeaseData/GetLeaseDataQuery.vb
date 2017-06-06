Imports Hotelie.Application.Leases.Queries.GetLeaseData

Namespace Tests.Leases.Queries.GetLeaseData
	Public Class GetLeaseDataQuery
		Implements IGetLeaseDataQuery

		Public Function Execute( id As String ) As LeaseModel Implements IGetLeaseDataQuery.Execute
			Dim entity = LeaseRepositoryTest.Leases.FirstOrDefault( Function( l ) l.Id = id )
			If IsNothing( entity ) Then Return Nothing

			Return New LeaseModel( entity )
		End Function

		Public Async Function ExecuteAsync( id As String ) As Task(Of LeaseModel) Implements IGetLeaseDataQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute( id ) )
		End Function
	End Class
End Namespace