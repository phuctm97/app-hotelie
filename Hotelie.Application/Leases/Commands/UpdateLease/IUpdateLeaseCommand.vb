Namespace Leases.Commands.UpdateLease
    Public Interface IUpdateLeaseCommand
        Sub Execute(id As String, roomId As String, beginDate As DateTime, endDate As DateTime) 
        Sub ExecuteAsync(id As String, roomId As String, beginDate As DateTime, endDate As DateTime) 

    End Interface
End NameSpace