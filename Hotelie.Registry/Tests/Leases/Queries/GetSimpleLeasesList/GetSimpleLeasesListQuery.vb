Imports Hotelie.Application.Leases.Queries.GetSimpleLeasesList
Imports Hotelie.Domain.Leases

Namespace Tests.Leases.Queries.GetSimpleLeasesList
	Public Class GetSimpleLeasesListQuery
		Implements IGetSimpleLeasesListQuery

		Public Function Execute() As IEnumerable(Of SimpleLeasesListItemModel) Implements IGetSimpleLeasesListQuery.Execute
			Dim list = New List(Of SimpleLeasesListItemModel)
			For Each lease As Lease In LeaseRepositoryTest.Leases
				Dim item = New SimpleLeasesListItemModel With {
					    .Id=lease.Id,
					    .RoomId=lease.Room.Id,
					    .CheckinDate=lease.CheckinDate,
					    .ExtraCharge=10000,
					    .TotalExpense=200000,
							.UnitPrice=lease.RoomPrice}
				list.Add( item )
			Next

			Return list
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of SimpleLeasesListItemModel)) _
			Implements IGetSimpleLeasesListQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute() )
		End Function
	End Class
End Namespace