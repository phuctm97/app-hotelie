Namespace Users.Factories
    Public Interface ICreateUserFactory
        Function Execute(id As String, password As String) As String
        Function ExecuteAsync(id As String, password As String) As Task(Of String)
    End Interface
End NameSpace