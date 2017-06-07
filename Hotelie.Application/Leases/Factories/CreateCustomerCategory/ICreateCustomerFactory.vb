Namespace Leases.Factories.CreateCustomerCategory
    Public Interface ICreateCustomerFactory
        Function Execute(name As String, coefficient As Double) As String
        Function ExecuteAsync(name As String, coefficient As Double) As Task(Of String)
    End Interface
End NameSpace