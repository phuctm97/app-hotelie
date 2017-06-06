Imports Hotelie.Application.Leases.Factories.CreateLease
Imports Hotelie.Domain.Leases

Namespace Tests.Leases.Factories.CreateLease
	Public Class CreateLeaseFactory
		Implements ICreateLeaseFactory

		Public Function Execute( roomId As String,
		                         checkinDate As DateTime,
		                         expectedCheckoutDate As DateTime,
		                         details As IEnumerable(Of CreateLeaseDetailModel) ) As String _
			Implements ICreateLeaseFactory.Execute
			Dim newId = 0
			While(True)
				Dim newIdStr = newId.ToString()
				If LeaseRepositoryTest.Leases.Any( Function( l )l.Id = newIdStr )
					newId += 1
				Else
					Exit While
				End If
			End While

			Dim room = RoomRepositoryTest.Rooms.FirstOrDefault( Function( r ) r.Id = roomId )
			If IsNothing( room ) Then Return "Không tìm thấy phòng tương ứng"

			Dim newLease = New Lease With {.Id=newId.ToString(),
				    .CheckinDate=checkinDate,
				    .ExpectedCheckoutDate=expectedCheckoutDate,
				    .CustomerCoefficient=0,
				    .ExtraCoefficient=0,
				    .Room=room,
				    .RoomPrice=room.Category.Price}
			LeaseRepositoryTest.Leases.Add( newLease )

			For Each model As CreateLeaseDetailModel In details
				CreateLeaseDetail( newLease, model )
			Next
			Return String.Empty
		End Function

		Private Sub CreateLeaseDetail( leaseToAdd As Lease,
		                               model As CreateLeaseDetailModel )
			Dim newId = 0
			While(True)
				Dim newIdStr = newId.ToString()
				Dim existed = False

				For Each lease As Lease In LeaseRepositoryTest.Leases
					If lease.LeaseDetails.Any( Function( d ) d.Id = newIdStr )
						existed = True
						Exit For
					End If
				Next

				If existed
					newId += 1
				Else
					Exit While
				End If
			End While

			Dim idStr = newId.ToString()

			Dim customerCategory =
				    LeaseRepositoryTest.CustomerCategories.FirstOrDefault( Function( c ) c.Id = model.CustomerCategoryId )
			If IsNothing( customerCategory ) Then Return

			leaseToAdd.LeaseDetails.Add( New LeaseDetail With {.Id=idStr,
				                           .CustomerName=model.CustomerName,
				                           .LicenseId=model.CustomerLicenseId,
				                           .Address=model.CustomerAddress,
				                           .CustomerCategory=customerCategory} )
		End Sub

		Public Async Function ExecuteAsync( roomId As String,
		                                    checkinDate As DateTime,
		                                    expectedCheckoutDate As DateTime,
		                                    details As IEnumerable(Of CreateLeaseDetailModel) ) As Task(Of String) _
			Implements ICreateLeaseFactory.ExecuteAsync
			Return Await Task.Run( Function() Execute( roomId, checkinDate, expectedCheckoutDate, details ) )
		End Function
	End Class
End Namespace