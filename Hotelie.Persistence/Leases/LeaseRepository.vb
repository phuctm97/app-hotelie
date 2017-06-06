Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases
Imports Hotelie.Persistence.Common

Namespace Leases
    Public Class LeaseRepository
        Inherits Repository(Of Lease)
        Implements ILeaseRepository

        Private _databaseService As DatabaseService
        Public Sub New(databaseService As DatabaseService)
            MyBase.New(databaseService)
            _databaseService = databaseService
        End Sub

        Public Function GetLeaseDetails() As List(Of LeaseDetail) Implements ILeaseRepository.GetLeaseDetails
            Return _databaseService.Context.LeaseDetails().ToList()
        End Function

        Public Async Function GetLeaseDetailsAsync() As Task(Of List(Of LeaseDetail)) Implements ILeaseRepository.GetLeaseDetailsAsync
            Return Await _databaseService.Context.LeaseDetails().ToListAsync()
        End Function

        Public Overrides Function GetOne(id As Object) As Lease
            Dim  idString = CType(id, String)
            If idString Is Nothing Then Throw new Exception("Id must be string")

            Return _databaseService.Context.Leases.FirstOrDefault(Function(p) String.Equals(p.Id,idString))
        End Function

        Public Overrides Async Function GetOneAsync(id As Object) As Task(Of Lease)
            Dim  idString = CType(id, String)
            If idString Is Nothing Then Throw new Exception("Id must be string")

            Dim r = Await _databaseService.Context.Leases.FirstOrDefaultAsync(Function(p) String.Equals(p.Id,idString))
            Return r
        End Function
    End Class
End NameSpace