Imports Caliburn.Micro
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Start.LoginShell.ViewModels
	Public Class ScreenSettingsViewModel
		' Dependencies
		Private ReadOnly _databaseService As IDatabaseService

		Public Sub New( databaseService As IDatabaseService )
			_databaseService = databaseService
		End Sub

		Public Async Sub TestConnection( dataSource As String,
		                           catalog As String )
			IoC.Get(Of IMainWindow).ShowStaticDialog( New LoadingDialog() )

			Dim connectionString =
				    $"data source={dataSource};initial catalog={catalog _
				    };integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"

			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( connectionString )

			IoC.Get(Of IMainWindow).CloseStaticDialog()

			If result
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Ok, "Kết nối thành công!" )
			Else
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Error, "Kết nối thất bại!" )
			End If
		End Sub

		Public Sub ApplyConnection( dataSource As String,
		                            catalog As String )
			Dim connectionString =
				    $"data source={dataSource};initial catalog={catalog _
				    };integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"

			Dim result = _databaseService.CheckDatabaseConnection( connectionString )

			If result
				My.Settings.HotelieDatabaseConnectionString = connectionString
				My.Settings.Save()

				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Ok, "Đã thiết lập kết nối!" )
			Else
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Error, "Kết nối thất bại. Thiết lập không hợp lệ!" )
			End If
		End Sub
	End Class
End Namespace
