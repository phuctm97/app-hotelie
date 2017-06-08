Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Start.LoginShell.ViewModels
	Public Class LoginShellViewModel
		Inherits Screen
		Implements IShell
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _databaseService As IDatabaseService

		Private _displayCode As Integer

		' Parent window
		Public Property ParentWindow As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return TryCast(Parent, IMainWindow)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		' Initialization
		Public Sub New( authentication As IAuthentication,
		                databaseService As IDatabaseService )
			_databaseService = databaseService

			CommandsBar = New LoginShellCommandsBarViewModel( Me )

			ScreenLogin = New ScreenLoginViewModel( authentication )

			ScreenSettings = New ScreenSettingsViewModel( databaseService )

			DisplayName = "Đăng nhập"

			DisplayCode = - 1
		End Sub

		Public ReadOnly Property ScreenLogin As ScreenLoginViewModel

		Public ReadOnly Property ScreenSettings As ScreenSettingsViewModel

		Public ReadOnly Property CommandsBar As IWindowCommandsBar Implements IShell.CommandsBar

		Public Property DisplayCode As Integer
			Get
				Return _displayCode
			End Get
			Set
				If Equals( value, _displayCode ) Then Return
				_displayCode = value
				NotifyOfPropertyChange( Function() DisplayCode )
			End Set
		End Property

		Protected Overrides Sub OnViewReady( view As Object )
			MyBase.OnViewReady( view )

			SetUpConnectionAsync()
		End Sub

		Private Sub SetUpConnection()
			Dim result = _databaseService.CheckDatabaseConnection( My.Settings.ConnectionDataSource,
			                                                       My.Settings.ConnectionCatalog )
			If result
				' reload database service
				_databaseService.SetDatabaseConnection( My.Settings.ConnectionDataSource,
				                                        My.Settings.ConnectionCatalog )
				' show login screen
				DisplayCode = 0
			Else
				' report error
				ShowStaticTopNotification( StaticNotificationType.Error,
				                           "Sự cố kết nối! Vui lòng kiểm tra lại thiết lập kết nối!" )
				' show settings screen
				DisplayCode = 1
			End If
		End Sub

		Private Async Sub SetUpConnectionAsync()
			' try connection
			ShowStaticWindowDialog( New LoadingDialog( "Đang kiểm tra kết nối..." ) )
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( My.Settings.ConnectionDataSource,
			                                                                  My.Settings.ConnectionCatalog )
			CloseStaticWindowDialog()

			If result = 2
				' reload database service
				Dim canSet = _databaseService.SetDatabaseConnection( My.Settings.ConnectionDataSource,
				                                                     My.Settings.ConnectionCatalog )
				If canSet
					' show login screen
					DisplayCode = 0
				Else
					' report error
					ShowStaticTopNotification( StaticNotificationType.Error,
					                           "Sự cố kết nối! Vui lòng kiểm tra lại thiết lập kết nối!" )
					' show settings screen
					DisplayCode = 1
				End If
			Else
				' report error
				ShowStaticTopNotification( StaticNotificationType.Error,
				                           "Sự cố kết nối! Vui lòng kiểm tra lại thiết lập kết nối!" )
				' show settings screen
				DisplayCode = 1
			End If
		End Sub
	End Class
End Namespace
