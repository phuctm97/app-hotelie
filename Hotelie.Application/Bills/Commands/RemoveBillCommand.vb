Imports Hotelie.Application.Services.Persistence

Namespace Bills.Commands
    Public Class RemoveBillCommand
        Implements IRemoveBillCommand

        Private ReadOnly _billRepository As IBillRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(unitOfWork As IUnitOfWork, billRepository As IBillRepository)
            _unitOfWork = unitOfWork
            _billRepository = billRepository
        End Sub

        Public Function Execute(id As String) As String Implements IRemoveBillCommand.Execute
            Try
                Dim bill = _billRepository.GetOne(id)
                _billRepository.Remove(bill)
                _unitOfWork.Commit()
                Return String.Empty
            Catch
                Return "Không xóa được hóa đơn"
            End Try
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of String) Implements IRemoveBillCommand.ExecuteAsync
            Try
                Dim bill = Await _billRepository.GetOneAsync(id)
                _billRepository.Remove(bill)
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không xóa được hóa đơn"
            End Try
        End Function
    End Class
End NameSpace