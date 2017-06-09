Imports Caliburn.Micro
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Start.LoginShell.ViewModels
	Public Class ScreenSettingsViewModel
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _databaseService As IDatabaseService

		' Bind models
		Public ReadOnly Property InitialDataSource As String

		Public ReadOnly Property InitialCatalog As String

		' Initialization
		Public Sub New( databaseService As IDatabaseService )
			_databaseService = databaseService
			InitialDataSource = My.Settings.ConnectionDataSource
			InitialCatalog = My.Settings.ConnectionCatalog
		End Sub

		' Test connection
		Public Async Sub TestConnection( dataSource As String,
		                                  catalog As String )
			'test connection
			ShowStaticWindowLoadingDialog()
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( dataSource, catalog )
			CloseStaticWindowDialog()

			'show result
			If result
				ShowStaticTopNotification( StaticNotificationType.Ok, "Kết nối thành công!" )
			Else
				ShowStaticTopNotification( StaticNotificationType.Error, "Kết nối thất bại!" )
			End If
		End Sub

		' Apply connection
		Private Async Sub ApplyConnection( dataSource As String,
		                                   catalog As String )
			'test connection before apply
			ShowStaticWindowLoadingDialog()
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( dataSource, catalog )
			CloseStaticWindowDialog()

			If result
				'save settings
				My.Settings.ConnectionDataSource = dataSource
				My.Settings.ConnectionCatalog = catalog
				My.Settings.Save()

				'setup database service
				_databaseService.SetDatabaseConnection( dataSource, catalog )

				'notification
				IoC.Get(Of IMainWindow).ShowStaticTopNotification( StaticNotificationType.Ok, "Đã thiết lập kết nối!" )
			Else
				'report error
				IoC.Get(Of IMainWindow).ShowStaticTopNotification( StaticNotificationType.Error,
				                                                   "Kết nối thất bại. Thiết lập không hợp lệ!" )
			End If
		End Sub
	End Class
End Namespace
