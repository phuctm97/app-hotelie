Imports Hotelie.Application.Leases.Queries.GetLeasesList
Imports Hotelie.Domain.Leases

Namespace Tests.Leases.Queries.GetLeasesList
	Public Class GetLeasesListQuery
		Implements IGetLeasesListQuery

		Public Function Execute() As IEnumerable(Of LeasesListItemModel) Implements IGetLeasesListQuery.Execute
			Dim list = New List(Of LeasesListItemModel)

			For Each lease As Lease In LeaseRepositoryTest.Leases
				Dim model = New LeasesListItemModel With {.Id=lease.Id,
					    .CheckinDate=lease.CheckinDate,
					    .ExpectedCheckoutDate=lease.ExpectedCheckoutDate,
					    .RoomName=lease.Room.Name,
					    .RoomCategoryName=lease.Room.Category.Name}
				For Each leaseDetail As LeaseDetail In lease.LeaseDetails
					Dim detailModel = New LeasesListItemDetailModel With {.CustomerName=leaseDetail.CustomerName}
					model.Details.Add( detailModel )
				Next

				list.Add( model )
			Next

			Return list
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of LeasesListItemModel)) _
			Implements IGetLeasesListQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute() )
		End Function
	End Class
End Namespace
