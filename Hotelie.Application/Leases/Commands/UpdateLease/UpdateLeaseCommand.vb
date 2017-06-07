Imports Hotelie.Application.Services.Persistence

Namespace Leases.Commands.UpdateLease
    Public Class UpdateLeaseCommand
        Implements IUpdateLeaseCommand

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork
        Private ReadOnly _parameterRepository As IParameterRepository

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork,
                parameterRepository As IParameterRepository, roomRepository As IRoomRepository)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
            _parameterRepository = parameterRepository
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(id As String, roomId As String, expectedCheckoutDate As Date) As String _
            Implements IUpdateLeaseCommand.Execute
            Try
                Dim lease = _leaseRepository.GetOne(id)
                lease.Room.State = 0
                Dim room = _roomRepository.GetOne(roomId)
                Dim rules = _parameterRepository.GetRules()

                lease.CustomerCoefficient = rules.CustomerCoefficient
                lease.ExtraCoefficient = rules.ExtraCoefficient
                lease.RoomPrice = room.Category.Price
                lease.Room = room
                lease.Room.State = 1
                lease.ExpectedCheckoutDate = expectedCheckoutDate
                _unitOfWork.Commit()
                Return String.Empty
            Catch
                Return "Không thể chỉnh sửa phiếu thuê phòng"
            End try
        End Function

        Public Async Function ExecuteAsync(id As String, roomId As String, expectedCheckoutDate As Date) As Task(Of String) _
            Implements IUpdateLeaseCommand.ExecuteAsync
            Try
                Dim lease = Await _leaseRepository.GetOneAsync(id)
                lease.room.State = 0
                Dim room = Await _roomRepository.GetOneAsync(roomId)
                Dim rules = Await _parameterRepository.GetRulesAsync()

                lease.CustomerCoefficient = rules.CustomerCoefficient
                lease.ExtraCoefficient = rules.ExtraCoefficient
                lease.RoomPrice = room.Category.Price
                lease.Room = room
                room.State = 1

                lease.ExpectedCheckoutDate = expectedCheckoutDate
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không thể chỉnh sửa phiếu thuê phòng"
            End try
        End Function
    End Class
End NameSpace