Imports Hotelie.Application.Services.Persistence

Namespace Leases.Commands.RemoveLease
    Public Class RemoveLeaseCommand
        Implements IRemoveLeaseCommand

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private _unitOfWork As IUnitOfWork

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String) As String Implements IRemoveLeaseCommand.Execute
            Try
                Dim lease = _leaseRepository.GetOne(id)
                lease.Room.State = 0
                _leaseRepository.Remove(lease)
                _unitOfWork.Commit()
                Return String.Empty
            Catch
                Return "Không xóa được phiếu thuê phòng"
            End Try
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of String) Implements IRemoveLeaseCommand.ExecuteAsync
            Try
                Dim lease = Await _leaseRepository.GetOneAsync(id)
                lease.Room.State = 0
                _leaseRepository.Remove(lease)
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không xóa được phiếu thuê phòng"
            End Try
        End Function
    End Class
End NameSpace