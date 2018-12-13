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

        Public Sub AddCustomerCategory(category As CustomerCategory) Implements ILeaseRepository.AddCustomerCategory
            _databaseService.Context.CustomerCategories.Add(category)
        End Sub

        Public Sub RemoveCustomerCategory(category As CustomerCategory) Implements ILeaseRepository.RemoveCustomerCategory
            _databaseService.Context.CustomerCategories.Remove(category)
        End Sub

        Public Sub RemoveLeaseDetail(leaseDetail As LeaseDetail) Implements ILeaseRepository.RemoveLeaseDetail
            _databaseService.Context.LeaseDetails.Remove(leaseDetail)
        End Sub


        Public Function GetCustomerCategories() As List(Of CustomerCategory) Implements ILeaseRepository.GetCustomerCategories
            Return _databaseService.Context.CustomerCategories.ToList()
        End Function

        Public Async Function GetCustomerCategoriesAsync() As Task(Of List(Of CustomerCategory)) Implements ILeaseRepository.GetCustomerCategoriesAsync
            Return Await _databaseService.Context.CustomerCategories.ToListAsync()
        End Function

        Public Function GetLeaseDetail(id As String) As LeaseDetail Implements ILeaseRepository.GetLeaseDetail
            Return _databaseService.Context.LeaseDetails.FirstOrDefault(Function(p)p.Id = id)
        End Function

        Public Async Function GetLeaseDetailAsync(id As String) As Task(Of LeaseDetail) Implements ILeaseRepository.GetLeaseDetailAsync
            Return Await _databaseService.Context.LeaseDetails.FirstOrDefaultAsync(Function(p)p.Id = id)
        End Function

        Public Function GetLeaseDetails() As List(Of LeaseDetail) Implements ILeaseRepository.GetLeaseDetails
            Return _databaseService.Context.LeaseDetails.ToList()
        End Function

        Public Async Function GetLeaseDetailsAsync() As Task(Of List(Of LeaseDetail)) Implements ILeaseRepository.GetLeaseDetailsAsync
            Return Await _databaseService.Context.LeaseDetails.ToListAsync()
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