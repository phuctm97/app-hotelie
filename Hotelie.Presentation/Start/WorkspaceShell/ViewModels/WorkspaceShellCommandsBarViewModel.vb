Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Application.Users.Commands
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellCommandsBarViewModel
		Inherits PropertyChangedBase
		Implements IWindowCommandsBar
		Implements INeedWindowModals

		' Backing fields
		Private _parent As Object

		' Dependencies
		Private ReadOnly _authentication As IAuthentication
		Private ReadOnly _changeUserPasswordCommand As IChangeUserPasswordCommand

		' Parent
		Public Property Parent As Object Implements IChild.Parent
			Get
				Return _parent
			End Get
			Set
				If Equals( Value, _parent ) Then Return
				_parent = value
				NotifyOfPropertyChange( Function() Parent )
			End Set
		End Property

		Public Property ParentShell As WorkspaceShellViewModel
			Get
				Return CType(Parent, WorkspaceShellViewModel)
			End Get
			Set
				If Equals( Parent, Value ) Then Return
				Parent = value
				NotifyOfPropertyChange( Function() ParentShell )
			End Set
		End Property

		' Initializations
		Public Sub New( authentication As IAuthentication,
		                changeUserPasswordCommand As IChangeUserPasswordCommand )
			_authentication = authentication
			_changeUserPasswordCommand = changeUserPasswordCommand
		End Sub

		' User actions
		Public Sub NavigateToScreenChangeRules()
			ParentShell.NavigateToScreenChangeRules()
		End Sub

		Public Sub NavigateToScreenManageUsers()
			ParentShell.NavigateToScreenManageUsers()
		End Sub

		Public Async Sub ChangePassword()
			Dim dialog = New ChangePasswordDialog()

			Dim data = Await ShowDynamicWindowDialog( dialog )

			If data Is Nothing Then Return
			Dim values = TryCast(data, Object())

			Dim oldPassword = CType(values( 0 ), String)
			Dim newPassword = CType(values( 1 ), String)
			Dim confirmPassword = CType(values( 2 ), String)

			If Not String.Equals( newPassword, confirmPassword )
				ShowStaticBottomNotification( MainWindow.Models.StaticNotificationType.Warning,
				                              "Mật khẩu mới và xác nhận mật khẩu không khớp" )
				Return
			End If

			If newPassword.Length = 0
				ShowStaticBottomNotification( MainWindow.Models.StaticNotificationType.Warning,
				                              "Mật khẩu mới không hợp lệ" )
				Return
			End If

			Dim err = Await _changeUserPasswordCommand.ExecuteAsync( _authentication.LoggedAccount.Username,
			                                                         oldPassword,
			                                                         newPassword )
			If Not String.IsNullOrEmpty( err )
				ShowStaticBottomNotification( MainWindow.Models.StaticNotificationType.Error, err )
			Else
				ShowStaticTopNotification( MainWindow.Models.StaticNotificationType.Ok,
				                           "Đổi mật khẩu thành công" )
			End If
		End Sub

		Public Sub Logout()
			IoC.Get(Of IMainWindow).SwitchShell( "login-shell" )
		End Sub
	End Class
End Namespace
