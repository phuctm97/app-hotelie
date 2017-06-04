Namespace Leases.Commands.RemoveLease
    Public Interface IRemoveLeaseCommand
        Sub Execute( id As String )
        Function ExecuteAsync( id As String ) As Task(Of Integer)
    End Interface
End NameSpace