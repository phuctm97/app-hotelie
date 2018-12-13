Imports System.Data.Entity
Imports Hotelie.Application.Leases.Models
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Queries
    Public Class GetAllLeasesQuery
        Implements IGetAllLeasesQuery

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute() As IEnumerable(Of ILeaseModel) Implements IGetAllLeasesQuery.Execute
            Dim leases = _leaseRepository.GetAll().ToList()
            Dim leaseModels = New List(Of LeaseModel)
            For Each lease As Lease In leases
                leaseModels.Add(New LeaseModel(lease))
            Next
            Return leaseModels
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of ILeaseModel)) Implements IGetAllLeasesQuery.ExecuteAsync
            Dim leases = Await _leaseRepository.GetAll().ToListAsync()
            Dim leaseModels = New List(Of LeaseModel)
            For Each lease As Lease In leases
                leaseModels.Add(New LeaseModel(lease))
            Next
            Return leaseModels
        End Function
    End Class
End NameSpace