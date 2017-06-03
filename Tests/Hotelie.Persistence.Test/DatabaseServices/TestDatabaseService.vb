Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common

Namespace DatabaseServices
    <TestClass>
    Public Class TestDatabaseService
        Private _databaseService As IDatabaseService

        <TestInitialize>
        Public Sub TestInitialze()
            _databaseService = New DatabaseService()
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _databaseService.Dispose()
        End Sub

        <TestMethod>
        Public Sub TestConnection_ValidConnectionString_Connected()
            ' valid connection string
            Dim valid = $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"

            ' invalid connection string
            Dim inValid  =$"data source=zzzz;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"

            ' assert
            _databaseService.SetDatabaseConnection(valid)
            Assert.IsTrue(_databaseService.CheckDatabaseConnection(valid))
            Dim userCategory = New UserCategory() With {.Id = "00001",.Name =  "Test"}
            _databaseService.Context.UserCategories.Add(userCategory)
            _databaseService.Context.UserCategories.Remove(userCategory)


            _databaseService.SetDatabaseConnection(inValid)
            Assert.IsFalse(_databaseService.CheckDatabaseConnection(inValid))
        End Sub
    End Class
End NameSpace