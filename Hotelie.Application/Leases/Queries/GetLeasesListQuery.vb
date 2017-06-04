Imports System.Data.Entity
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
                                                             .EndDate = p.EndDate})
            For Each leaseModel As LeaseModel In leases
                Dim customers = _leaseRepository.GetCustomers(leaseModel.Id)
                leaseModel.NumberOfCustomers = customers.Count()
            Next
            Return leases
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of LeaseModel)) Implements IGetLeasesListQuery.ExecuteAsync
            Dim leases = Await _leaseRepository.GetAll().Select(Function(p) New LeaseModel() With _
                                                             {.Id = p.Id,
                                                             .RoomName = p.Room.Name,
                                                             .BeginDate = p.BeginDate,
                                                             .EndDate = p.EndDate}).ToListAsync()
            For Each leaseModel As LeaseModel In leases
                Dim customers = Await _leaseRepository.GetCustomersAsync(leaseModel.Id)
                leaseModel.NumberOfCustomers = customers.Count()
            Next
            Return leases
        End Function
    End Class
End NameSpace