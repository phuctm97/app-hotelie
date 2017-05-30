Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Settings.ViewModels
	Public Class SettingsViewModel
		Implements IWorkspace

		Public Property DisplayName As String Implements IHaveDisplayName.DisplayName

		Public Sub New()
			DisplayName = "Settings"
		End Sub
	End Class
End Namespace
