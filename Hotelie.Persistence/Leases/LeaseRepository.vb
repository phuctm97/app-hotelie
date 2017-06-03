
Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases
Imports Hotelie.Persistence.Common

Namespace Leases
    Public Class LeaseRepository
        Inherits Repository(Of Lease)
        Implements ILeaseRepository

        Private ReadOnly _context As DatabaseContext

        Public Sub New(context As DbContext)
            MyBase.New(context)
            _context = context
        End Sub

        Public Function GetCustomers(id As String) As List(Of LeaseDetail) Implements ILeaseRepository.GetCustomers
            Return _context.LeaseDetails.Where(Function(p)p.Lease.Id = id).ToList()
        End Function

        Public Overrides Function GetOne(id As Object) As Lease
            Dim idString = CType(id, String)
            If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

            Return _context.Leases.FirstOrDefault(Function(p) String.Equals(p.Id, idString))
        End Function
    End Class
End Namespace