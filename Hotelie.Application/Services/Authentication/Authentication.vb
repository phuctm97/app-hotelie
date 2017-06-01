Imports Hotelie.Application.Services.Persistence

Namespace Services.Authentication
    Public Class Authentication
        Implements IAuthentication


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
        Public Function TryLogin(account As Account) As IEnumerable(Of String) Implements IAuthentication.TryLogin
            Dim errorLog = New List(Of String)

            ' Currently logged in
            If (LoggedIn)
                errorLog.Add("Đăng nhập lỗi, Hotelie đang được đăng nhập với tài khoản: " + LoggedAccount.Username)
                Return errorLog
            End If

            ' Username is invalid
            Dim user = _userRepository.Find(Function(p)(p.Id = account.Username)).FirstOrDefault()
            If (IsNothing(user))
                errorLog.Add("Tên tài khoản không tồn tại.")
                Return errorLog
            End If

            ' Password is invalid
            If (user.Password <> account.Password)
                errorLog.Add("Sai mật khẩu")
                Return errorLog
            End If

            ' Logging in
            LoggedAccount = New Account() With {.Username=account.Username,.Password=account.Password}

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