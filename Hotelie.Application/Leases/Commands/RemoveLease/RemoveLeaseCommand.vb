
Imports Hotelie.Application.Services.Persistence

Namespace Leases.Commands.RemoveLease
    Public Class RemoveLeaseCommand
        Implements IRemoveLeaseCommand

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Sub Execute(id As String) Implements IRemoveLeaseCommand.Execute
            _leaseRepository.Remove(_leaseRepository.GetOne(id))
            _unitOfWork.Commit()
        End Sub

        Public Async Sub ExecuteAsync(id As String) Implements IRemoveLeaseCommand.ExecuteAsync
            _leaseRepository.Remove(Await _leaseRepository.GetOneAsync(id))
            _unitOfWork.CommitAsync()
        End Sub
    End Class
End NameSpace