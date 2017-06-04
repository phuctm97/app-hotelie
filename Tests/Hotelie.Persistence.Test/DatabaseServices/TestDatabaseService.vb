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
            ' invalid connection string
            Dim inValidServerName  =""
            Dim inValidDatabaseName ="1"
            ' assert
            _databaseService.SetDatabaseConnection(validServerName,validDatabaseName)
            Assert.IsTrue(_databaseService.CheckDatabaseConnection(validServerName,validDatabaseName))
            Dim userCategory = New UserCategory() With {.Id = "00001",.Name =  "Test"}
            _databaseService.Context.UserCategories.Add(userCategory)
            _databaseService.Context.UserCategories.Remove(userCategory)


            _databaseService.SetDatabaseConnection(inValidServerName,inValidDatabaseName)
            Assert.IsFalse(_databaseService.CheckDatabaseConnection(inValidServerName,inValidDatabaseName))
        End Sub
    End Class
End NameSpace