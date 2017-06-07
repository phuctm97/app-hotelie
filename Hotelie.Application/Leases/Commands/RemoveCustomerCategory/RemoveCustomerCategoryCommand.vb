Imports Hotelie.Application.Services.Persistence

Namespace Leases.Commands.RemoveCustomerCategory
    Public Class RemoveCustomerCategoryCommand
        Implements IRemoveCustomerCategoryCommand

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String) As String Implements IRemoveCustomerCategoryCommand.Execute
            Try

                Dim category = _leaseRepository.GetCustomerCategories().FirstOrDefault(Function(p)p.Id = id)
                If IsNothing(category) Then Return "Không tìm thấy loại khách hàng cần xóa"
                _leaseRepository.RemoveCustomerCategory(category)
                _unitOfWork.Commit()
                Return String.Empty
            Catch
                Return "Không thể xóa loại khách"
            End Try
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of String) _
            Implements IRemoveCustomerCategoryCommand.ExecuteAsync
            Try
                Dim categories = Await _leaseRepository.GetCustomerCategoriesAsync()
                Dim category = categories.FirstOrDefault(Function(p)p.Id = id)
                If IsNothing(category) Then Return "Không tìm thấy loại khách hàng cần xóa"
                _leaseRepository.RemoveCustomerCategory(category)
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không thể xóa loại khách"
            End Try
        End Function
    End Class
End NameSpace