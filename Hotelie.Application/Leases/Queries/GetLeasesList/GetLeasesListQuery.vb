Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Queries.GetLeasesList
    Public Class GetLeasesListQuery
        Implements IGetLeasesListQuery

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute() As IEnumerable(Of LeasesListItemModel) Implements IGetLeasesListQuery.Execute
            Dim leases = _leaseRepository.GetAll().Select(Function(p) New LeasesListItemModel() With _
                                                             {.Id = p.Id,
                                                             .CheckinDate = p.CheckinDate,
                                                             .ExpectedCheckoutDate = p.ExpectedCheckoutDate,
                                                             .RoomName = p.Room.Name,
                                                             .RoomCategoryName = p.Room.Category.Name,
                                                             .TotalExpense = p.CalculateExpense}).ToList()
            For Each leasesListItemModel As LeasesListItemModel In leases
                Dim lease = _leaseRepository.GetOne(leasesListItemModel.Id)
                For Each leaseDetail As LeaseDetail In lease.LeaseDetails
                    leasesListItemModel.Details.Add(New LeasesListItemDetailModel() With {.CustomerName = leaseDetail.CustomerName})
                Next
            Next
            Return leases
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of LeasesListItemModel)) Implements IGetLeasesListQuery.ExecuteAsync
            Dim leases = Await _leaseRepository.GetAll().Select(Function(p) New LeasesListItemModel() With _
                                                             {.Id = p.Id,
                                                             .CheckinDate = p.CheckinDate,
                                                             .ExpectedCheckoutDate = p.ExpectedCheckoutDate,
                                                             .RoomName = p.Room.Name,
                                                             .RoomCategoryName = p.Room.Category.Name,
                                                             .TotalExpense = p.CalculateExpense}).ToListAsync()
            For Each leasesListItemModel As LeasesListItemModel In leases
                Dim lease = Await _leaseRepository.GetOneAsync(leasesListItemModel.Id)
                For Each leaseDetail As LeaseDetail In lease.LeaseDetails
                    leasesListItemModel.Details.Add(New LeasesListItemDetailModel() With {.CustomerName = leaseDetail.CustomerName})
                Next
            Next
            Return leases
        End Function
    End Class
End NameSpace