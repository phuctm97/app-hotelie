Imports Hotelie.Domain.Leases

Namespace Leases.Queries.GetLeaseData
	Public Class LeaseModel
		Private ReadOnly _entity As Lease

		Sub New( entity As Lease )
			_entity = entity
			Room = new Rooms.Queries.GetRoomData.RoomModel( _entity.Room )

			Details = New List(Of GetLeaseDetailData.LeaseDetailModel)()
			For Each leaseDetail As LeaseDetail In _entity.LeaseDetails
				Details.Add( New GetLeaseDetailData.LeaseDetailModel( leaseDetail ) )
			Next
		End Sub

		Public ReadOnly Property Id As String
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property Room As Rooms.Queries.GetRoomData.RoomModel

		Public ReadOnly Property CheckinDate As Date
			Get
				Return _entity.CheckinDate
			End Get
		End Property

		Public ReadOnly Property ExpectedCheckoutDate As Date
			Get
				Return _entity.ExpectedCheckoutDate
			End Get
		End Property

		Public ReadOnly Property RoomPrice As Decimal
			Get
				Return _entity.RoomPrice
			End Get
		End Property

		Public ReadOnly Property ExtraCoefficient As Double
			Get
				Return _entity.ExtraCoefficient
			End Get
		End Property

		Public ReadOnly Property CustomerCoefficient As Double
			Get
				Return _entity.CustomerCoefficient
			End Get
		End Property

		Public ReadOnly Property Details As List(Of GetLeaseDetailData.LeaseDetailModel)
	End Class
End Namespace