﻿Imports Hotelie.Domain.Leases

Namespace Leases.Models
	Public Class LeaseModel
		Implements ILeaseModel

		Private ReadOnly _entity As Lease

		Sub New( entity As Lease )
			_entity = entity
			Room = new Rooms.Models.RoomModel( _entity.Room )

			Details = New List(Of ILeaseDetailModel)
			For Each leaseDetail As LeaseDetail In _entity.LeaseDetails
				Details.Add( New LeaseDetailModel( leaseDetail ) )
			Next
		End Sub

		Public ReadOnly Property Id As String Implements ILeaseModel.Id
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property IdEx As String Implements ILeaseModel.IdEx
			Get
				Return $"#{Id}"
			End Get
		End Property

		Public ReadOnly Property Room As Rooms.Models.IRoomModel Implements ILeaseModel.Room

		Public ReadOnly Property CheckinDate As Date Implements ILeaseModel.CheckinDate
			Get
				Return _entity.CheckinDate
			End Get
		End Property

		Public ReadOnly Property NumberOfUsedDays As Integer Implements ILeaseModel.NumberOfUsedDays
			Get
                Return CType(Today.Subtract(CheckinDate).TotalDays, Integer) + 1
            End Get
		End Property

		Public ReadOnly Property ExpectedCheckoutDate As Date Implements ILeaseModel.ExpectedCheckoutDate
			Get
				Return _entity.ExpectedCheckoutDate
			End Get
		End Property

		Public ReadOnly Property RoomUnitPrice As Decimal Implements ILeaseModel.RoomUnitPrice
			Get
				Return _entity.RoomPrice
			End Get
		End Property

		Public ReadOnly Property ExtraCoefficient As Double Implements ILeaseModel.ExtraCoefficient
			Get
				Return _entity.ExtraCoefficient
			End Get
		End Property

        Public ReadOnly Property CustomerCoefficient As Double Implements ILeaseModel.CustomerCoefficient
            Get
                Return _entity.CustomerCoefficient
            End Get
        End Property

        Public ReadOnly Property MaximunCustomer As Integer Implements ILeaseModel.MaximunCustomer
            Get
                Return _entity.MaximumCustomer
            End Get
        End Property

        Public ReadOnly Property ExtraCharge As Decimal Implements ILeaseModel.ExtraCharge
			Get
                Dim extra = NumberOfUsedDays * RoomUnitPrice * CustomerCoefficient
                If Details.Count >= MaximunCustomer Then extra += NumberOfUsedDays * RoomUnitPrice * ExtraCoefficient
                Return extra
            End Get
		End Property

		Public ReadOnly Property TotalExpense As Decimal Implements ILeaseModel.TotalExpense
            Get
                Return NumberOfUsedDays * RoomUnitPrice + ExtraCharge
            End Get
        End Property

		Public ReadOnly Property Details As List(Of ILeaseDetailModel) Implements ILeaseModel.Details

		Public ReadOnly Property IsPaid As Boolean Implements ILeaseModel.IsPaid
			Get
				Return _entity.Paid
			End Get
		End Property
	End Class
End Namespace