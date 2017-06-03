Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Start.LoginShell.ViewModels
	Public Class LoginShellCommandsBarViewModel
		Implements IWindowCommandsBar

		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentShell As LoginShellViewModel
			Get
				Return CType(Parent, LoginShellViewModel)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		Public Sub New( shell As LoginShellViewModel )
			ParentShell = shell
		End Sub
	End Class
End Namespace