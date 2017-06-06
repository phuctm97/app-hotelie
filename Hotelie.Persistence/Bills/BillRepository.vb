Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Bills
Imports Hotelie.Persistence.Common

Namespace Bills
    Public Class BillRepository
        Inherits Repository(Of Bill)
        Implements IBillRepository

        Private _databaseService As DatabaseService
        Public Sub New(databaseService As DatabaseService)
            MyBase.New(databaseService)
            _databaseService = databaseService
        End Sub

        Public Overrides Function GetOne(id As Object) As Bill
            Dim idString = CType(id, String)
            If IsNothing(idString) Then Throw New Exception("id phải là string")

            Return _databaseService.Context.Bills.FirstOrDefault(Function(p)p.Id = idString)           
        End Function


        Public Overrides Async Function GetOneAsync(id As Object) As Task(Of Bill)
            Dim idString = CType(id, String)
            If IsNothing(idString) Then Throw New Exception("id phải là string")

            Return Await _databaseService.Context.Bills.FirstOrDefaultAsync(Function(p)p.Id = idString) 
        End Function
    End Class
End NameSpace