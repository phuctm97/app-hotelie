Imports Hotelie.Domain.Leases

Namespace Tests.Leases
	Module LeaseRepositoryTest
		Public Property Leases As List(Of Lease)

		Public Property CustomerCategories As List(Of CustomerCategory)

		Sub New()
			CustomerCategories = New List(Of CustomerCategory) From {
				New CustomerCategory With {.Id="0", .Name="Nội địa", .Coefficient=0},
				New CustomerCategory With {.Id="1", .Name="Nước ngoài", .Coefficient=0.5}}

			Leases = New List(Of Lease) From {
				New Lease With {.Id = "0", 
					.CheckinDate=New Date( 2017, 1, 1 ),
					.ExpectedCheckoutDate=New Date( 2017, 1, 1 ),
					.ExtraCoefficient=0,
					.CustomerCoefficient=0,
					.RoomPrice=200000,
					.Room=RoomRepositoryTest.Rooms( 0 ),
					.LeaseDetails = New List(Of LeaseDetail) From {
						New LeaseDetail With {.Id="0",
							.CustomerName="Trần Minh Phúc",
							.CustomerCategory=CustomerCategories( 0 ),
							.Address="Thủ Đức",
							.LicenseId="3XX-XXX-XXX"},
						New LeaseDetail With {.Id="1",
							.CustomerName="Nguyễn Bảo Khương",
							.CustomerCategory=CustomerCategories( 0 ),
							.Address="Thủ Đức",
							.LicenseId="3XX-XXX-XXX"},
						New LeaseDetail With {.Id="2",
							.CustomerName="Trần Đức Nhật",
							.CustomerCategory=CustomerCategories( 0 ),
							.Address="Thủ Đức",
							.LicenseId="3XX-XXX-XXX"}}},
							_
				New Lease With {.Id = "1", 
					.CheckinDate=New Date( 2017, 1, 1 ),
					.ExpectedCheckoutDate=New Date( 2017, 1, 1 ),
					.ExtraCoefficient=0,
					.CustomerCoefficient=0,
					.RoomPrice=400000,
					.Room=RoomRepositoryTest.Rooms( 1 ),
					.LeaseDetails = New List(Of LeaseDetail) From {
						New LeaseDetail With {.Id="3",
							.CustomerName="Cristiano Ronaldo",
							.CustomerCategory=CustomerCategories( 1 ),
							.Address="Bồ Đào Nha",
							.LicenseId="3XX-XXX-XXX"},
						New LeaseDetail With {.Id="4",
							.CustomerName="Ngọc Trinh",
							.CustomerCategory=CustomerCategories( 0 ),
							.Address="Tp. Hồ Chí Minh",
							.LicenseId="3XX-XXX-XXX"}}},
							_
				New Lease With {.Id = "2", 
					.CheckinDate=New Date( 2017, 1, 1 ),
					.ExpectedCheckoutDate=New Date( 2017, 1, 1 ),
					.ExtraCoefficient=0,
					.CustomerCoefficient=0,
					.RoomPrice=400000,
					.Room=RoomRepositoryTest.Rooms( 2 ),
					.LeaseDetails = New List(Of LeaseDetail) From {
						New LeaseDetail With {.Id="5",
							.CustomerName="Lionel Messi",
							.CustomerCategory=CustomerCategories( 1 ),
							.Address="Ác-hen-ti-na",
							.LicenseId="3XX-XXX-XXX"},
						New LeaseDetail With {.Id="6",
							.CustomerName="Angle Phương Trinh",
							.CustomerCategory=CustomerCategories( 0 ),
							.Address="Tp. Hồ Chí Minh",
							.LicenseId="3XX-XXX-XXX"}}}}
		End Sub
	End Module
End Namespace
