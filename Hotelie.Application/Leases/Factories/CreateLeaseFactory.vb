Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Factories
    Public Class CreateLeaseFactory
        Implements ICreateLeaseFactory

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _unitOfWork As IUnitOfWork
        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork, roomRepository As IRoomRepository)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(id As String, roomId As String, beginDate As Date, endDate As Date) As LeaseModel _
            Implements ICreateLeaseFactory.Execute
            Dim room = _roomRepository.GetOne(roomId)

            Dim lease = New LeaseModel() With { _
                    .Id = id,
                    .RoomId = roomId,
                    .BeginDate = beginDate,
                    .EndDate = endDate,
                    .Price = room.Category.Price
                    }

            _leaseRepository.Add(
                New Lease() _
                                    With {.Id = lease.Id, .Room=room, .BeginDate = lease.BeginDate,
                                    .EndDate = lease.EndDate, .Price = lease.Price})
            _unitOfWork.Commit()

            Return lease
        End Function
    End Class
End NameSpace