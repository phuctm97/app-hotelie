Namespace Leases.Factories
    Public Interface ICreateLeaseFactory
        Function Execute(id As String, roomId As String, beginDate As DateTime, endDate As DateTime) As LeaseModel
    End Interface
End NameSpace