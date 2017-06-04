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
			' try connection
			IoC.Get(Of IMainWindow).ShowStaticWindowDialog( New LoadingDialog() )
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( dataSource, catalog )
			IoC.Get(Of IMainWindow).CloseStaticWindowDialog()

			' show result
			If result
				IoC.Get(Of IMainWindow).ShowStaticTopNotification( StaticNotificationType.Ok, "Kết nối thành công!" )
			Else
				IoC.Get(Of IMainWindow).ShowStaticTopNotification( StaticNotificationType.Error, "Kết nối thất bại!" )
			End If
		End Sub

		Public Async Sub ApplyConnection( dataSource As String,
		                                  catalog As String )
			IoC.Get(Of IMainWindow).ShowStaticWindowDialog( New LoadingDialog() )
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( dataSource, catalog )
			IoC.Get(Of IMainWindow).CloseStaticWindowDialog()

			If result
				' save settings
				My.Settings.ConnectionDataSource = dataSource
				My.Settings.ConnectionCatalog = catalog
				My.Settings.Save()

				' reload database service
				_databaseService.SetDatabaseConnection( dataSource, catalog )

				' notification
				IoC.Get(Of IMainWindow).ShowStaticTopNotification( StaticNotificationType.Ok, "Đã thiết lập kết nối!" )
			Else
				' report error
				IoC.Get(Of IMainWindow).ShowStaticTopNotification( StaticNotificationType.Error,
				                                                "Kết nối thất bại. Thiết lập không hợp lệ!" )
			End If
		End Sub
	End Class
End Namespace
