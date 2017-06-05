Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Domain.Leases

Namespace Tests.Leases
	Public Module LeasesTest
		Public Property Leases As List(Of LeaseModel)

		Public Property LeaseDetals As List(Of LeaseDetail)

		Public Property CustomerCategories As List(Of CustomerCategory)

		Sub New()
			CustomerCategories = New List(Of CustomerCategory)
			CustomerCategories.Add(New CustomerCategory With {.Id="0", .Name="Nội địa"})
			CustomerCategories.Add(New CustomerCategory With {.Id="1", .Name="Nước ngoài"})

			LeaseDetals = New List(Of LeaseDetail)

			Leases = New List(Of LeaseModel)
			Leases.Add( New LeaseModel With {.Id="0", .BeginDate=New Date( 2017, 4, 23 ), .EndDate=New Date( 2017, 6, 5 ),
				          .Price=150000, .RoomCategoryName="Phòng thường 1", .RoomName="A101", .TotalPrice=1000000} )
		End Sub
	End Module
End Namespace