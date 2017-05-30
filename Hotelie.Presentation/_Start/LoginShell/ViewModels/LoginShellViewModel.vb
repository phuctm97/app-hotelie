Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Start.LoginShell.ViewModels
	Public Class LoginShellViewModel
		Implements IShell

		Public Property DisplayName As String Implements IHaveDisplayName.DisplayName

		Public Property Parent As Object Implements IChild.Parent

		Public Property IChild_Parent As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return CType(Parent, IMainWindow)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		Public Sub New()
			DisplayName = "Login"
		End Sub
	End Class
End Namespace
