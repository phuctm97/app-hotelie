Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence

Namespace Leases.Queries.GetSimpleLeasesList
    Public Class GetSimpleLeasesListQuery
        Implements IGetSimpleLeasesListQuery

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute() As IEnumerable(Of SimpleLeasesListItemModel) Implements IGetSimpleLeasesListQuery.Execute
            Dim leases = _leaseRepository.GetAll().Select(Function(r) New SimpleLeasesListItemModel() With _
                                                              {.Id = r.Id,
                                                              .CheckinDate = r.CheckinDate,
                                                              .UnitPrice = r.RoomPrice,
                                                              .RoomId = r.Room.Id})
            For Each simpleLease As SimpleLeasesListItemModel In leases
                Dim lease = _leaseRepository.GetOne(simpleLease.Id)
                Dim numberOfDays = Today.Subtract(lease.CheckinDate).TotalDays()
                Dim extraCharge = lease.RoomPrice*lease.ExtraCoefficient*numberOfDays
                Dim totalExpense = lease.RoomPrice*(1+lease.CustomerCoefficient)*numberOfDays

                simpleLease.ExtraCharge = extraCharge
                simpleLease.TotalExpense = extraCharge + totalExpense
            Next
            Return leases
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of SimpleLeasesListItemModel)) Implements IGetSimpleLeasesListQuery.ExecuteAsync
            Dim leases = Await _leaseRepository.GetAll().Select(Function(r) New SimpleLeasesListItemModel() With _
                                                             {.Id = r.Id,
                                                             .CheckinDate = r.CheckinDate,
                                                             .UnitPrice = r.RoomPrice,
                                                             .RoomId = r.Room.Id}).ToListAsync()

            For Each simpleLease As SimpleLeasesListItemModel In leases
                Dim lease = Await _leaseRepository.GetOneAsync(simpleLease.Id)
                Dim numberOfDays = Today.Subtract(lease.CheckinDate).TotalDays()
                Dim extraCharge = lease.RoomPrice*lease.ExtraCoefficient*numberOfDays
                Dim totalExpense = lease.RoomPrice*(1+lease.CustomerCoefficient)*numberOfDays

                simpleLease.ExtraCharge = extraCharge
                simpleLease.TotalExpense = extraCharge + totalExpense
            Next
            Return leases
        End Function
    End Class
End NameSpace