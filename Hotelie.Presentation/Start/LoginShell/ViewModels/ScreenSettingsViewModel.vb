Imports Caliburn.Micro
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Start.LoginShell.ViewModels
	Public Class ScreenSettingsViewModel
		' Dependencies
		Private ReadOnly _databaseService As IDatabaseService

		' Display
		Public ReadOnly Property InitialDataSource As String

		Public ReadOnly Property InitialCatalog As String

		Public Sub New( databaseService As IDatabaseService )
			_databaseService = databaseService

			InitialDataSource = My.Settings.ConnectionDataSource
			InitialCatalog = My.Settings.ConnectionCatalog
		End Sub

		Public Async Sub TestConnection( dataSource As String,
		                                 catalog As String )
			' show loading dialog
			IoC.Get(Of IMainWindow).ShowStaticDialog( New LoadingDialog() )

			' try connection
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( dataSource, catalog )

			' finish, close dialog
			IoC.Get(Of IMainWindow).CloseStaticDialog()

			' show result
			If result
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Ok, "Kết nối thành công!" )
			Else
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Error, "Kết nối thất bại!" )
			End If
		End Sub

		Public Async Sub ApplyConnection( dataSource As String,
		                                  catalog As String )
			' show loading dialog
			IoC.Get(Of IMainWindow).ShowStaticDialog( New LoadingDialog() )

			' try connection
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( dataSource, catalog )

			' finish, close dialog
			IoC.Get(Of IMainWindow).CloseStaticDialog()

			If result
				' save settings
				My.Settings.ConnectionDataSource = dataSource
				My.Settings.ConnectionCatalog = catalog
				My.Settings.Save()

				' reload database service
				_databaseService.SetDatabaseConnection( dataSource, catalog )

				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Ok, "Đã thiết lập kết nối!" )
			Else
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Error,
				                                                "Kết nối thất bại. Thiết lập không hợp lệ!" )
			End If
		End Sub
	End Class
End Namespace
