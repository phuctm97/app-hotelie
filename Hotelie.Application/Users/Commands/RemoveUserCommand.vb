Imports Hotelie.Application.Services.Persistence

Namespace Users.Commands
    Public Class RemoveUserCommand
        Implements IRemoveUserCommand

        Private ReadOnly _userRepository As IUserRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(userRepository As IUserRepository, unitOfWork As IUnitOfWork)
            _userRepository = userRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String) As String Implements IRemoveUserCommand.Execute
            Try
                Dim user = _userRepository.GetOne(id)
                If IsNothing(user) Then Return "Không tìm thấy tài khoản cần xóa"
                _userRepository.Remove(user)
                _unitOfWork.Commit()
                Return String.Empty
            Catch
                Return "Không thể xóa tài khoản này"
            End Try
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of String) Implements IRemoveUserCommand.ExecuteAsync
            Try
                Dim user = Await _userRepository.GetOneAsync(id)
                If IsNothing(user) Then Return "Không tìm thấy tài khoản cần xóa"
                _userRepository.Remove(user)
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không thể xóa tài khoản này"
            End Try
        End Function
    End Class
End NameSpace