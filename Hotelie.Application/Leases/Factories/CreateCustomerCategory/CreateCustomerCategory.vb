Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Factories.CreateCustomerCategory
    Public Class CreateCustomerCategory
        Implements ICreateCustomerFactory

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(name As String, coefficient As Double) As String _
            Implements ICreateCustomerFactory.Execute
            Try
            ' create new customer category
            Dim customerCategories = _leaseRepository.GetCustomerCategories()
            Dim defaultId = 0
            Dim newId = Nothing
            If (customerCategories.Count = 0)
                newId = "CC" + 1.ToString("000")
            Else
                While newId Is Nothing
                    defaultId + = 1
                    Dim checkNewId = "CC"+defaultId.ToString("000")
                    Dim q = True
                    For Each customerCategory As CustomerCategory In customerCategories
                        If customerCategory.Id = checkNewId
                            q = False
                            Exit For
                        End If
                    Next
                    If q Then newId = checkNewId
                End While
            End If

            ' new customer category
            Dim category = New CustomerCategory() With {.Id = newId,.Name=name,.Coefficient=coefficient}
            _leaseRepository.AddCustomerCategory(category)
            _unitOfWork.Commit()
            Return String.Empty
            Catch
                Return "Không thể thêm loại khách mới"
            End Try
        End Function

        Public Async Function ExecuteAsync(name As String, coefficient As Double) As Task(Of String) _
            Implements ICreateCustomerFactory.ExecuteAsync
            Try
                ' create new customer category
                Dim customerCategories = Await _leaseRepository.GetCustomerCategoriesAsync()
                Dim defaultId = 0
                Dim newId = Nothing
                If (customerCategories.Count = 0)
                    newId = "CC" + 1.ToString("000")
                Else
                    While newId Is Nothing
                        defaultId + = 1
                        Dim checkNewId = "CC"+defaultId.ToString("000")
                        Dim q = True
                        For Each customerCategory As CustomerCategory In customerCategories
                            If customerCategory.Id = checkNewId
                                q = False
                                Exit For
                            End If
                        Next
                        If q Then newId = checkNewId
                    End While
                End If

                ' new customer category
                Dim category = New CustomerCategory() With {.Id = newId,.Name=name,.Coefficient=coefficient}
                _leaseRepository.AddCustomerCategory(category)
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không thể thêm loại khách mới"
            End Try
        End Function
    End Class
End NameSpace