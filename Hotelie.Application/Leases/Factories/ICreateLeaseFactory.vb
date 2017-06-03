Namespace Leases.Factories
    Public Interface ICreateLeaseFactory
        Function Execute(roomId As String, beginDate As DateTime, endDate As DateTime) As LeaseModel
    End Interface
End NameSpace