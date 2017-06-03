Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases
Imports Hotelie.Persistence.Common

Namespace Leases
    Public Class LeaseRepository
        Inherits Repository(Of Lease)
        Implements ILeaseRepository

        Private ReadOnly _databaseService As DatabaseService

        Public Sub New(databaseService As DatabaseService)
            MyBase.New(databaseService)
            _databaseService = databaseService
        End Sub

        Public Function GetCustomers(id As String) As List(Of LeaseDetail) Implements ILeaseRepository.GetCustomers
            Try
                Return _databaseService.Context.LeaseDetails.Where(Function(p)p.Lease.Id = id).ToList()
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Function

        Public Overrides Function GetOne(id As Object) As Lease
            Try
                Dim idString = CType(id, String)
                If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

                Return _databaseService.Context.Leases.FirstOrDefault(Function(p) String.Equals(p.Id, idString))
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Function
    End Class
End Namespace