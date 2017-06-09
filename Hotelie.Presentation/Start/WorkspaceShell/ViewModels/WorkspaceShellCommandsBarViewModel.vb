Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellCommandsBarViewModel
		Inherits PropertyChangedBase
		Implements IWindowCommandsBar

		' Dependencies
		Private ReadOnly _authentication As IAuthentication

		' Binding properties
		Public ReadOnly Property Username As String
			Get
				Return _authentication.LoggedAccount?.Username
			End Get
		End Property

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
			NotifyOfPropertyChange( Function() Username )
			NotifyOfPropertyChange( Function() CanChangeRules )
		End Sub

		' User actions
		Public Sub NavigateToScreenChangeRules()
			ParentShell.NavigateToScreenChangeRules()
		End Sub

		Public Sub Logout()
			IoC.Get(Of IMainWindow).SwitchShell( "login-shell" )
		End Sub
	End Class
End Namespace
