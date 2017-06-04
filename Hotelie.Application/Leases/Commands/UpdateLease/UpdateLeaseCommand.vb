Imports Hotelie.Application.Services.Persistence

Namespace Leases.Commands.UpdateLease
    Public Class UpdateLeaseCommand
        Implements IUpdateLeaseCommand

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork, roomRepository As IRoomRepository)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
            _roomRepository = roomRepository
        End Sub

        Public Sub Execute(id As String, roomId As String, beginDate As Date, endDate As Date) _
            Implements IUpdateLeaseCommand.Execute
            Dim lease = _leaseRepository.GetOne(id)
            Dim room = _roomRepository.GetOne(roomId)

            lease.Room = room
            lease.BeginDate = beginDate
            lease.EndDate = endDate

            _unitOfWork.Commit()
        End Sub

        Public Async Sub ExecuteAsync(id As String, roomId As String, beginDate As Date, endDate As Date) Implements IUpdateLeaseCommand.ExecuteAsync
            Dim lease = Await _leaseRepository.GetOneAsync(id)
            Dim room = Await _roomRepository.GetOneAsync(roomId)

            lease.Room = room
            lease.BeginDate = beginDate
            lease.EndDate = endDate

            _unitOfWork.CommitAsync()
        End Sub
    End Class
End NameSpace