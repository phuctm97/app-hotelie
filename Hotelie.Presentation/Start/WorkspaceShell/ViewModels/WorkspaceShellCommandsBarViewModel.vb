Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellCommandsBarViewModel
		Implements IWindowCommandsBar

		' Dependencies
		Private ReadOnly _authentication As IAuthentication

		' Binding models
		Public ReadOnly Property Username As String
			Get
				Return _authentication.LoggedAccount?.Username
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

		Public Sub New( shell As WorkspaceShellViewModel,
		                authentication As IAuthentication )
			ParentShell = shell
			_authentication = authentication
		End Sub

		Public Sub Logout()
			IoC.Get(Of IMainWindow).SwitchShell( "login-shell" )
		End Sub

		Public Sub NavigateToScreenChangeRules()
			ParentShell.NavigateToScreenChangeRules()
		End Sub
	End Class
End Namespace
