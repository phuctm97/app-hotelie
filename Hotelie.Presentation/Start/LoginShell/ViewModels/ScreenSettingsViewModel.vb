Imports Hotelie.Application.Services.Persistence

Namespace Start.LoginShell.ViewModels
	Public Class ScreenSettingsViewModel
		
		' Dependencies
		Private _databaseService As IDatabaseService

		Public Sub New( databaseService As IDatabaseService )
			_databaseService = databaseService
		End Sub

		Public Sub TestConnection(dataSource As String, catalog As String)

		End Sub

		Public Sub ApplyConnection(dataSource As String, catalog As String)

		End Sub

	End Class
End Namespace
