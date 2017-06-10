Imports Hotelie.Application.Services.Persistence

Namespace Users.Commands
    Public Class ChangeUserPasswordCommand
        Implements IChangeUserPasswordCommand

        Private ReadOnly _userRepository As IUserRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(userRepository As IUserRepository, unitOfWork As IUnitOfWork)
            _userRepository = userRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String, password As String, repassword As String) As String Implements IChangeUserPasswordCommand.Execute
            Dim user = _userRepository.GetOne(id)
            If IsNothing(user) Then Return "Không tìm thấy tài khoản"
            If password <> user.Password Then Return "Mật khẩu cũ không trùng khớp"
            user.Password = repassword
            _unitOfWork.Commit()
            Return String.Empty
        End Function

        Public Async Function ExecuteAsync(id As String, password As String, repassword As String) As Task(Of String) Implements IChangeUserPasswordCommand.ExecuteAsync
            Dim user = Await _userRepository.GetOneAsync(id)
            If IsNothing(user) Then Return "Không tìm thấy tài khoản"
            If password <> user.Password Then Return "Mật khẩu cũ không trùng khớp"
            user.Password = repassword
            Await _unitOfWork.CommitAsync()
            Return String.Empty
        End Function
    End Class
End NameSpace