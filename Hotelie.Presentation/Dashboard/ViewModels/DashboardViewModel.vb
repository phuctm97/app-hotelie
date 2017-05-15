Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Dashboard.ViewModels
	Public Class DashboardViewModel
		Implements IWorkspace

		Public Property DisplayName As String Implements IHaveDisplayName.DisplayName

		Public Sub New()
			DisplayName = "Dashboard"
		End Sub
	End Class
End Namespace
