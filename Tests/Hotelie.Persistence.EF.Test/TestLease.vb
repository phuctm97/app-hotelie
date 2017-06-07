
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Rooms

<TestClass>
Public Class TestLeaseRepository
    Private _databaseContext As IDatabaseContext

    <TestInitialize>
    Public Sub TestInitialize()
        _databaseContext = New DatabaseContext($"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
    End Sub

    <TestCleanup>
    Public Sub TestCleanup()
        _databaseContext.Dispose()
    End Sub

    <TestMethod>
    Public Sub GetLease()
        Dim ls = _databaseContext.Leases.ToList()

        Dim i = 0
        For Each lease As Lease In ls
            i + = 1
        Next

        Dim detail = _databaseContext.LeaseDetails.ToList()

        For Each details As LeaseDetail In detail
            i + = 1
        Next
    End Sub

End Class
