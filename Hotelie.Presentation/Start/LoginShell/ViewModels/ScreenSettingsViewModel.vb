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

		' Display
		Public ReadOnly Property InitialDataSource As String

		Public ReadOnly Property InitialCatalog As String

		' Initialization
		Public Sub New( databaseService As IDatabaseService )
			_databaseService = databaseService
			InitialDataSource = My.Settings.ConnectionDataSource
			InitialCatalog = My.Settings.ConnectionCatalog
		End Sub

		' Test connection
		Public Sub PreviewTestConnection( dataSource As String,
		                                  catalog As String )
			TestConnection( dataSource, catalog )
		End Sub

		Public Sub PreviewTestConnectionAsync( dataSource As String,
		                                       catalog As String )
			TestConnectionAsync( dataSource, catalog )
		End Sub

		Private Sub TestConnection( dataSource As String,
		                            catalog As String )
			' try connection
			Dim result = _databaseService.CheckDatabaseConnection( dataSource, catalog )

			' show result
			If result
				ShowStaticTopNotification( StaticNotificationType.Ok, "Kết nối thành công!" )
			Else
				ShowStaticTopNotification( StaticNotificationType.Error, "Kết nối thất bại!" )
			End If
		End Sub

		Private Async Sub TestConnectionAsync( dataSource As String,
		                                       catalog As String )
			' try connection
			ShowStaticWindowLoadingDialog()
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( dataSource, catalog )
			CloseStaticWindowDialog()

			' show result
			If result
				ShowStaticTopNotification( StaticNotificationType.Ok, "Kết nối thành công!" )
			Else
				ShowStaticTopNotification( StaticNotificationType.Error, "Kết nối thất bại!" )
			End If
		End Sub

		' Apply connection
		Public Sub PreviewApplyConnection( dataSource As String,
		                                   catalog As String )
			ApplyConnection( dataSource, catalog )
		End Sub

		Public Sub PreviewApplyConnectionAsync( dataSource As String,
		                                        catalog As String )
			ApplyConnectionAsync( dataSource, catalog )
		End Sub

		Private Sub ApplyConnection( dataSource As String,
		                             catalog As String )
			Dim result = _databaseService.CheckDatabaseConnection( dataSource, catalog )

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

		Private Async Sub ApplyConnectionAsync( dataSource As String,
		                                        catalog As String )
			ShowStaticWindowLoadingDialog()
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( dataSource, catalog )
			CloseStaticWindowDialog()

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
