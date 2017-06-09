Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellCommandsBarViewModel
		Inherits PropertyChangedBase
		Implements IWindowCommandsBar
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _authentication As IAuthentication

		' Binding properties
		Public ReadOnly Property CanChangeRules As Boolean
			Get
				Return True
			End Get
		End Property

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentShell As WorkspaceShellViewModel
			Get
				Return CType(Parent, WorkspaceShellViewModel)
			End Get
			Set
				Parent = value
			End Set
		End Property

		' Initializations
		Public Sub New( shell As WorkspaceShellViewModel,
		                authentication As IAuthentication )
			ParentShell = shell
			_authentication = authentication
		End Sub

		Public Sub Reload()
			NotifyOfPropertyChange( Function() CanChangeRules )
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

			Dim result = Await ShowDynamicWindowDialog( dialog )

			If result Is Nothing Then Return
			Dim values = TryCast(result, String())

			Dim oldPassword = values( 0 )
			Dim newPassword = values( 1 )
			Dim confirmPassword = values( 2 )

			If Not String.Equals( newPassword, confirmPassword )
				ShowStaticBottomNotification( MainWindow.Models.StaticNotificationType.Warning,
				                              "Mật khẩu mới và xác nhận mật khẩu không giống nhau" )
				Return
			End If
		End Sub

		Public Sub Logout()
			IoC.Get(Of IMainWindow).SwitchShell( "login-shell" )
		End Sub
	End Class
End Namespace
