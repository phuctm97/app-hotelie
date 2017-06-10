Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
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
		Public Sub New( authentication As IAuthentication )
			_authentication = authentication
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
