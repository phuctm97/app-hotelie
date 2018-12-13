Namespace Users.Commands
    Public Interface IChangeUserPasswordCommand
        Function Execute(id As String, password As String, repassword As String) As String
        Function ExecuteAsync(id As String, password As String, repassword As String) As Task(Of String)
    End Interface
End NameSpace