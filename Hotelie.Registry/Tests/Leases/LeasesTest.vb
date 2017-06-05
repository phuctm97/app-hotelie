Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Domain.Leases

Namespace Tests.Leases
	Public Module LeasesTest
		Public Property Leases As List(Of LeaseModel)

		Public Property CustomerCategories As List(Of CustomerCategory)

		Sub New()
			CustomerCategories = New List(Of CustomerCategory)
			CustomerCategories.Add( New CustomerCategory With {.Id="0", .Name="Nội địa"} )
			CustomerCategories.Add( New CustomerCategory With {.Id="1", .Name="Nước ngoài"} )

			Leases = New List(Of LeaseModel)
			Leases.Add( New LeaseModel With {.Id="0", .BeginDate=New Date( 2017, 4, 23 ), .EndDate=New Date( 2017, 6, 5 ), 
				          .Price=150000, .RoomCategoryName="Phòng thường 1", .RoomName="A101", .TotalPrice=1000000} )
			Leases( 0 ).Customers = New BindableCollection(Of LeaseCustomerModel) From { 
				New LeaseCustomerModel With {.Id="0", .CategoryName="Nội địa", .CategoryId="0",  
					.Address="Thủ Đức", .LisenceId="3XX-XXX-XXX", .Name="Trần Minh Phúc"}, 
				New LeaseCustomerModel With {.Id="1", .CategoryName="Nội địa", .CategoryId="0",  
					.Address="Tp. Hồ Chí Minh", .LisenceId="3XX-XXX-XXX", .Name="Lê Phương Khanh"}, 
				New LeaseCustomerModel With {.Id="2", .CategoryName="Nội địa", .CategoryId="0",  
					.Address="Tp. Hồ Chí Minh", .LisenceId="3XX-XXX-XXX", .Name="Phạm Thi Vương"}}

			Leases.Add( New LeaseModel With {.Id="1", .BeginDate=New Date( 2017, 4, 23 ), .EndDate=New Date( 2017, 6, 5 ), 
				          .Price=150000, .RoomCategoryName="Phòng Vip 2", .RoomName="A102", .TotalPrice=1000000} )
			Leases( 1 ).Customers = New BindableCollection(Of LeaseCustomerModel) From { 
				New LeaseCustomerModel With {.Id="3", .CategoryName="Nước ngoài", .CategoryId="1",  
					.Address="Bồ Đào Nha", .LisenceId="3XX-XXX-XXX", .Name="Cristiano Ronaldo"}, 
				New LeaseCustomerModel With {.Id="4", .CategoryName="Nội địa", .CategoryId="0",  
					.Address="Tp. Hồ Chí Minh", .LisenceId="3XX-XXX-XXX", .Name="Ngọc Trinh"}}

			Leases.Add( New LeaseModel With {.Id="2", .BeginDate=New Date( 2017, 4, 23 ), .EndDate=New Date( 2017, 6, 5 ), 
				          .Price=150000, .RoomCategoryName="Phòng Vip 1", .RoomName="A103", .TotalPrice=1000000} )
			Leases( 2 ).Customers = New BindableCollection(Of LeaseCustomerModel) From { 
				New LeaseCustomerModel With {.Id="5", .CategoryName="Nước ngoài", .CategoryId="1",  
					.Address="Ác-hen-ti-na", .LisenceId="3XX-XXX-XXX", .Name="Lionel Messi"}, 
				New LeaseCustomerModel With {.Id="6", .CategoryName="Nội địa", .CategoryId="0",  
					.Address="Tp. Hồ Chí Minh", .LisenceId="3XX-XXX-XXX", .Name="Angle Phương Trinh"}}
		End Sub
	End Module
End Namespace