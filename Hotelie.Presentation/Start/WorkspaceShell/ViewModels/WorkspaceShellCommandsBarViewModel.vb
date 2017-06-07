Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellCommandsBarViewModel
		Implements IWindowCommandsBar

		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentShell As WorkspaceShellViewModel
			Get
				Return CType(Parent, WorkspaceShellViewModel)
			End Get
			Set
				Parent = value
			End Set
		End Property

		Public Sub New( shell As WorkspaceShellViewModel )
			ParentShell = shell
		End Sub

		Public Sub PreviewLogout()
			IoC.Get(Of IMainWindow).SwitchShell("login-shell")
		End Sub

		Public Sub PreviewNavigateToScreenChangeRules()
			ParentShell.NavigateToScreenChangeRules()
		End Sub

	End Class
End Namespace
