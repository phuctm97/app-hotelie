Imports Hotelie.Presentation.Dashboard.Design
Imports Hotelie.Presentation.Settings.Design

Namespace Start.WorkspaceShell.Design
	Public Class WorkspaceShellViewModel
		Inherits WorkspaceShell.ViewModels.WorkspaceShellViewModel

		Public Sub New()
			MyBase.New( Nothing )

			DisplayName = "Hotelie - Home"
			Items.Add( New DashboardViewModel )
			Items.Add( New SettingsViewModel )
		End Sub
	End Class
End Namespace
