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
            Dim validServerName = $"KHUONG-ASUS\SQLEXPRESS"
            Dim validDatabaseName = $"HotelieDatabase"

            ' invalid database
            Dim validServerName1  ="KHUONG-ASUS\SQLEXPRESS"
            Dim inValidDatabaseName ="HoteileDatabaseTest"

            ' invalid server
            Dim inValidServerName2  ="1"
            Dim validDatabaseName2 ="HoteilieDatabase"

            ' assert
            _databaseService.SetDatabaseConnection(validServerName,validDatabaseName)
            Assert.IsTrue(_databaseService.CheckDatabaseConnection(validServerName,validDatabaseName) = 2)
            'Dim userCategory = New UserCategory() With {.Id = "00001",.Name =  "Test"}
            '_databaseService.Context.UserCategories.Add(userCategory)
            '_databaseService.Context.UserCategories.Remove(userCategory)


            _databaseService.SetDatabaseConnection(validServerName1,inValidDatabaseName)
            Assert.IsTrue(_databaseService.CheckDatabaseConnection(validServerName1,inValidDatabaseName) = 1)

            Assert.IsTrue(_databaseService.CheckDatabaseConnection(inValidServerName2,validDatabaseName2) = 0)
        End Sub
    End Class
End NameSpace