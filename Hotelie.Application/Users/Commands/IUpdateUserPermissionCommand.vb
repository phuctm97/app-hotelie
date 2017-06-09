Namespace Users.Commands
    Public Interface IUpdateUserPermissionCommand
        Function Execute(id As String, canConfigRoom As Boolean, canAddLease As Boolean, canEditLease As Boolean, canRemoveLease As Boolean, canManageUser As Boolean, canEditRules As Boolean) As String
        Function ExecuteAsync(id As String, canConfigRoom As Boolean, canAddLease As Boolean, canEditLease As Boolean, canRemoveLease As Boolean, canManageUser As Boolean, canEditRules As Boolean) As Task(Of String)
    End Interface
End NameSpace