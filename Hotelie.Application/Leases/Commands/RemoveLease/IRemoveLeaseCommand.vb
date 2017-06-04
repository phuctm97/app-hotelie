Namespace Leases.Commands.RemoveLease
    Public Interface IRemoveLeaseCommand
        Sub Execute( id As String )
        Sub ExecuteAsync( id As String )
    End Interface
End NameSpace