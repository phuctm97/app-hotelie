Namespace Users.Commands
    Public Interface IRemoveUserCommand
        Function Execute(id As String) As String
        Function ExecuteAsync(id As String) As Task(Of String)
    End Interface
End NameSpace