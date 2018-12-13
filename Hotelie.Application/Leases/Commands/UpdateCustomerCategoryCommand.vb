Imports Hotelie.Application.Services.Persistence

Namespace Leases.Commands
    Public Class UpdateCustomerCategoryCommand
        Implements IUpdateCustomerCategoryCommand

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String, name As String, coefficient As Double) As String _
            Implements IUpdateCustomerCategoryCommand.Execute
            Try
                Dim categories = _leaseRepository.GetCustomerCategories()
                Dim category = categories.FirstOrDefault(Function(p)p.Id = id)
                If Not (IsNothing(category))
                    category.Name = name
                    category.Coefficient = coefficient
                    _unitOfWork.Commit()
                Else
                    Return "Không tìm thấy loại khách hàng cần chỉnh sửa"
                End If
                Return String.Empty
            Catch
                Return "Không thể chỉnh sửa loại khách hàng này"
            End Try
        End Function

        Public Async Function ExecuteAsync(id As String, name As String, coefficient As Double) As Task(Of String) _
            Implements IUpdateCustomerCategoryCommand.ExecuteAsync
            Try
                Dim categories = Await _leaseRepository.GetCustomerCategoriesAsync()
                Dim category = categories.FirstOrDefault(Function(p)p.Id = id)
                If Not (IsNothing(category))
                    category.Name = name
                    category.Coefficient = coefficient
                    Await _unitOfWork.CommitAsync()
                Else
                    Return "Không tìm thấy loại khách hàng cần chỉnh sửa"
                End If
                Return String.Empty
            Catch
                Return "Không thể chỉnh sửa loại khách hàng này"
            End Try
        End Function
    End Class
End NameSpace