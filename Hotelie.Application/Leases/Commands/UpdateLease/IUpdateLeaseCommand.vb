Namespace Leases.Commands.UpdateLease
    Public Interface IUpdateLeaseCommand
        Sub Execute(id As String, roomId As String, beginDate As DateTime, endDate As DateTime) 
        Function ExecuteAsync(id As String, roomId As String, beginDate As DateTime, endDate As DateTime) As Task(Of Integer)

    End Interface
End NameSpace