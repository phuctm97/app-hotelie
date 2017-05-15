Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Dashboard.ViewModels
Imports Hotelie.Presentation.Settings.ViewModels

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellViewModel
		Inherits Conductor(Of IWorkspace).Collection.OneActive
		Implements IShell

		Private ReadOnly _authentication As IAuthentication

		Public Sub New( authentication As IAuthentication )
			_authentication = authentication
			' TODO: uncomment this for authentication check
			' If Not _authentication.LoggedIn Then Throw New ApplicationException( "Authentication has not been passed yet" )

			DisplayName = "Hotelie - Workspace"
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()
			InitializeWorkspaces()
		End Sub

		Private Sub InitializeWorkspaces()
			Items.Add( New DashboardViewModel )
			Items.Add( New SettingsViewModel )
		End Sub
	End Class
End Namespace
