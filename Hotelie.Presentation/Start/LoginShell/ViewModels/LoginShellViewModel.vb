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

		' Dependencies

		Private ReadOnly _databaseService As IDatabaseService

		' Display property backing fields

		Private _displayCode As Integer

		' Parent window

		Public Property ParentWindow As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return CType(Parent, IMainWindow)
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

			SetUpConnection()
		End Sub

		Private Async Sub SetUpConnection()
			' show loading dialog
			IoC.Get(Of IMainWindow).ShowStaticDialog( New LoadingDialog( "Đang kiểm tra kết nối..." ) )

			' try connection
			Dim connectionString = My.Settings.ConnectionDataSource
			Dim result = Await _databaseService.CheckDatabaseConnectionAsync( connectionString )

			' finish, close dialog
			IoC.Get(Of IMainWindow).CloseStaticDialog()

			If result
				' reload database service
				_databaseService.SetDatabaseConnection( connectionString )
				DisplayCode = 0
			Else
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Error,
				                                                "Sự cố kết nối! Vui lòng kiểm tra lại thiết lập kết nối!" )
				DisplayCode = 1
			End If
		End Sub
	End Class
End Namespace
