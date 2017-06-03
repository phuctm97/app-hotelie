Imports Hotelie.Application.Services.Persistence

Namespace Services.Authentication
    Public Class Authentication
        Implements IAuthentication

        Public Const AlreadyLoggedInError As String = "Đăng nhập lỗi, Hotelie đang được đăng nhập."
        Public Const UsernameInvalidError As String = "Tên đăng nhập không tồn tại."
        Public Const PasswordInvalidError As String = "Mật khẩu không chính xác."

        Private ReadOnly _userRepository As IUserRepository

        Public Property LoggedAccount As Account Implements IAuthentication.LoggedAccount

        Public ReadOnly Property LoggedIn As Boolean Implements IAuthentication.LoggedIn
            Get
                Return Not(IsNothing(LoggedAccount))
            End Get
        End Property

        ''' <summary>
        '''     Login to Hotelie
        ''' </summary>
        Public Function TryLogin(username As String, password As String) As IEnumerable(Of String) Implements IAuthentication.TryLogin
            Dim errorLog = New List(Of String)

            ' Currently logged in
            If (LoggedIn)
                errorLog.Add(AlreadyLoggedInError)
                Return errorLog
            End If

            ' Username is invalid
            Dim user = _userRepository.Find(Function(p)(p.Id = username)).FirstOrDefault()
            If (IsNothing(user))
                errorLog.Add(UsernameInvalidError)
                Return errorLog
            End If

            ' Password is invalid
            If (user.Password <> password)
                errorLog.Add(PasswordInvalidError)
                Return errorLog
            End If

            ' Logging in
            LoggedAccount = New Account() With {.Username=username}

            Return errorLog
        End Function

        ''' <summary>
        '''     Logout current user
        ''' </summary>
        Public Sub Logout() Implements IAuthentication.Logout
            LoggedAccount = Nothing
        End Sub

        Public Sub New(userRepository As IUserRepository)
            Logout()
            _userRepository = userRepository
        End Sub
    End Class
End Namespace