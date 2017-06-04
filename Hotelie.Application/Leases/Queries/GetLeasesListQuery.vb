Imports System.Data.Entity
Imports Caliburn.Micro
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Queries
    Public Class GetLeasesListQuery
        Implements IGetLeasesListQuery

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute() As IEnumerable(Of LeaseModel) Implements IGetLeasesListQuery.Execute
            Dim leases = _leaseRepository.GetAll().Select(Function(p) New LeaseModel() With _
                                                             {.Id = p.Id,
                                                             .RoomName = p.Room.Name,
                                                             .BeginDate = p.BeginDate,
                                                             .EndDate = p.EndDate,
                                                             .RoomCategoryName = p.Room.Category.Name,
                                                             .Price = p.Room.Category.Price})

            For Each leaseModel As LeaseModel In leases
                Dim customers = _leaseRepository.GetCustomers(leaseModel.Id)
                If (customers.Count > 0)
                    leaseModel.Customers = New BindableCollection(Of LeaseCustomerModel)
                    For Each leaseCustomer As LeaseDetail In customers
                        leaseModel.Customers.Add(New LeaseCustomerModel() With {.Id = leaseCustomer.Id, 
                                                    .Address=leaseCustomer.Address, 
                                                    .Name = leaseCustomer.CustomerName, 
                                                    .LisenceId = leaseCustomer.LicenseId, 
                                                    .CategoryId = leaseCustomer.CustomerCategory.Id, 
                                                    .CategoryName = leaseCustomer.CustomerCategory.Name})
                    Next
                End If
                leaseModel.TotalPrice = leaseModel.Price*(DateTime.Now().Subtract(leaseModel.BeginDate).Days())
            Next
            Return leases
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of LeaseModel)) _
            Implements IGetLeasesListQuery.ExecuteAsync
            Dim leases = Await _leaseRepository.GetAll().Select(Function(p) New LeaseModel() With _
                                                                   {.Id = p.Id,
                                                                   .RoomName = p.Room.Name,
                                                                   .BeginDate = p.BeginDate,
                                                                   .EndDate = p.EndDate,
                                                                   .RoomCategoryName = p.Room.Category.Name}).
                    ToListAsync()
            For Each leaseModel As LeaseModel In leases
                Dim customers = Await _leaseRepository.GetCustomersAsync(leaseModel.Id)
                If (customers.Count > 0)
                    leaseModel.Customers = New BindableCollection(Of LeaseCustomerModel)
                    For Each leaseCustomer As LeaseDetail In customers
                        leaseModel.Customers.Add(New LeaseCustomerModel() With {.Id = leaseCustomer.Id, 
                                                    .Address=leaseCustomer.Address, 
                                                    .Name = leaseCustomer.CustomerName, 
                                                    .LisenceId = leaseCustomer.LicenseId, 
                                                    .CategoryId = leaseCustomer.CustomerCategory.Id, 
                                                    .CategoryName = leaseCustomer.CustomerCategory.Name})
                    Next
                End If
                leaseModel.TotalPrice = leaseModel.Price*(DateTime.Now().Subtract(leaseModel.BeginDate).Days())
            Next
            Return leases
        End Function
    End Class
End NameSpace