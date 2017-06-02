Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellViewModel
		Inherits Conductor(Of IWorkspace).Collection.OneActive
		Implements IShell

		' Parent window

		Public Property ParentWindow As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return CType(Parent, IMainWindow)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		' Initialization
		Public Sub New()
			CommandsBar = New WorkspaceShellCommandsBarViewModel( Me )
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			DisplayName = "Workspace"
		End Sub

		' Display properties

		Public ReadOnly Property CommandsBar As IWindowCommandsBar Implements IShell.CommandsBar
	End Class
End Namespace
