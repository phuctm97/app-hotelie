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
                                                             .Price = p.Room.Category.Price}).ToList()
            For Each leaseModel As LeaseModel In leases

                Dim maxCoefficient As Double
                ' Get all customer of each LeaseModel
                Dim customers = _leaseRepository.GetCustomers(leaseModel.Id)

                If (customers.Count > 0)
                    maxCoefficient = customers(0).CustomerCategory.Coefficient
                    leaseModel.Customers = New BindableCollection(Of LeaseCustomerModel)

                    For Each leaseCustomer As LeaseDetail In customers
                        ' Add customer to lease's customer collection
                        leaseModel.Customers.Add(New LeaseCustomerModel() With {.Id = leaseCustomer.Id, 
                                                    .Address=leaseCustomer.Address, 
                                                    .Name = leaseCustomer.CustomerName, 
                                                    .LisenceId = leaseCustomer.LicenseId, 
                                                    .CategoryId = leaseCustomer.CustomerCategory.Id, 
                                                    .CategoryName = leaseCustomer.CustomerCategory.Name})
                        ' Get the highest coefficent
                        If maxCoefficient < leaseCustomer.CustomerCategory.Coefficient Then _
                            maxCoefficient = leaseCustomer.CustomerCategory.Coefficient
                    Next

                End If

                ' calculate time
                Dim days = DateTime.Now().Subtract(leaseModel.BeginDate).Days()

                ' calculate extra charge
                If (customers.Count>3)
                    leaseModel.ExtraCharge = leaseModel.Price*leaseModel.ExtraCoefficient*days
                End If

                ' calculate total price
                leaseModel.TotalPrice = leaseModel.Price*maxCoefficient*days + leaseModel.ExtraCharge

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
                                                                   .RoomCategoryName = p.Room.Category.Name,
                                                                   .ExtraCoefficient = p.ExtraCoefficient}).
                    ToListAsync()
            For Each leaseModel As LeaseModel In leases
                Dim maxCoefficient As Double
                ' Get all customer of each LeaseModel
                Dim customers = Await _leaseRepository.GetCustomersAsync(leaseModel.Id)

                If (customers.Count > 0)
                    maxCoefficient = customers(0).CustomerCategory.Coefficient
                    leaseModel.Customers = New BindableCollection(Of LeaseCustomerModel)

                    For Each leaseCustomer As LeaseDetail In customers
                        ' Add customer to lease's customer collection
                        leaseModel.Customers.Add(New LeaseCustomerModel() With {.Id = leaseCustomer.Id, 
                                                    .Address=leaseCustomer.Address, 
                                                    .Name = leaseCustomer.CustomerName, 
                                                    .LisenceId = leaseCustomer.LicenseId, 
                                                    .CategoryId = leaseCustomer.CustomerCategory.Id, 
                                                    .CategoryName = leaseCustomer.CustomerCategory.Name})
                        ' Get the highest coefficent
                        If maxCoefficient < leaseCustomer.CustomerCategory.Coefficient Then _
                            maxCoefficient = leaseCustomer.CustomerCategory.Coefficient
                    Next

                End If

                ' calculate time
                Dim days = DateTime.Now().Subtract(leaseModel.BeginDate).Days()

                ' calculate extra charge
                If (customers.Count>3)
                    leaseModel.ExtraCharge = leaseModel.Price*leaseModel.ExtraCoefficient*days
                End If

                ' calculate total price
                leaseModel.TotalPrice = leaseModel.Price*maxCoefficient*days + leaseModel.ExtraCharge
            Next
            Return leases
        End Function
    End Class
End NameSpace