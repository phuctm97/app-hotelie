Imports Hotelie.Application.Services.Persistence

Namespace Leases.Commands.RemoveLeaseDetail
    Public Class RemoveLeaseDetailCommand
        Implements IRemoveLeaseDetailCommand

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String) As String Implements IRemoveLeaseDetailCommand.Execute
            Try
                Dim leaseDetail = _leaseRepository.GetLeaseDetail(id)
                _leaseRepository.RemoveLeaseDetail(leaseDetail)
                _unitOfWork.Commit()
                Return String.Empty
            Catch
                Return "Không xóa được chi tiết hóa đơn"
            End try
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of String) _
            Implements IRemoveLeaseDetailCommand.ExecuteAsync
            Try
                Dim leaseDetail = Await _leaseRepository.GetLeaseDetailAsync(id)
                _leaseRepository.RemoveLeaseDetail(leaseDetail)
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không xóa được chi tiết hóa đơn"
            End try
        End Function
    End Class
End NameSpace