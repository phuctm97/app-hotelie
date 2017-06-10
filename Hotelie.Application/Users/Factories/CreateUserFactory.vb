
Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users

Namespace Users.Factories
    Public Class CreateUserFactory
        Implements ICreateUserFactory

        Private ReadOnly _userRepository As IUserRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(userRepository As IUserRepository, unitOfWork As IUnitOfWork)
            _userRepository = userRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String, password As String) As String Implements ICreateUserFactory.Execute
            Try
                If id.Length > 20 Then Return String.Empty
                Dim users = _userRepository.GetAll().ToList()
                If users.Exists(Function(p)p.Id = id) Then Return String.Empty
                _userRepository.Add(New User() With {.Id = id, .Password = password})
                _unitOfWork.Commit()
                Return id
            Catch
                Return String.Empty
            End Try
        End Function

        Public Async Function ExecuteAsync(id As String, password As String) As Task(Of String) Implements ICreateUserFactory.ExecuteAsync
            Try
                If id.Length > 20 Then Return String.Empty
                Dim users = Await _userRepository.GetAll().ToListAsync()
                If users.Exists(Function(p)p.Id = id) Then Return String.Empty
                _userRepository.Add(New User() With {.Id = id, .Password = password})
                Await _unitOfWork.CommitAsync()
                Return id
            Catch
                Return String.Empty
            End Try
        End Function
    End Class
End NameSpace