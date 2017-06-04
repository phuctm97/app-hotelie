Imports Caliburn.Micro
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Start.MainWindow.ViewModels
	Public Class MainWindowViewModel
		Inherits Conductor(Of IShell)
		Implements IMainWindow

		' Window property backing fields

		Private _title As String
		Private _width As Double
		Private _height As Double
		Private _windowState As WindowState

		Public Sub New()
			DisplayName = "Hotelie"

			Title = "Hotelie"

			Width = 1024

			Height = 700

			WindowState = WindowState.Normal

			StaticNotification = New StaticNotificationModel()

			StaticWindowDialog = New StaticDialogModel()

			StaticShellDialog = New StaticDialogModel()
		End Sub

		' Window properties

		Public Property Title As String Implements IMainWindow.Title
			Get
				Return _title
			End Get
			Set
				If String.Equals( _title, Value ) Then Return

				_title = value
				NotifyOfPropertyChange( Function() Title )
			End Set
		End Property

		Public Property Width As Double Implements IMainWindow.Width
			Get
				Return _width
			End Get
			Set
				If Equals( _width, Value ) Then Return

				_width = value
				NotifyOfPropertyChange( Function() Width )
			End Set
		End Property

		Public Property Height As Double Implements IMainWindow.Height
			Get
				Return _height
			End Get
			Set
				If Equals( _height, Value ) Then Return

				_height = value
				NotifyOfPropertyChange( Function() Height )
			End Set
		End Property

		Public Property WindowState As WindowState Implements IMainWindow.WindowState
			Get
				Return _windowState
			End Get
			Set
				If Equals( _windowState, Value ) Then Return

				_windowState = value
				NotifyOfPropertyChange( Function() WindowState )
			End Set
		End Property

		Public ReadOnly Property StaticNotification As StaticNotificationModel

		Public ReadOnly Property StaticWindowDialog As StaticDialogModel

		Public ReadOnly Property StaticShellDialog As StaticDialogModel

		' Shell

		Public ReadOnly Property Shell As IShell Implements IMainWindow.Shell
			Get
				Return ActiveItem
			End Get
		End Property

		Public Sub SwitchShell( shellName As String ) Implements IMainWindow.SwitchShell
			ActivateItem( IoC.Get(Of IShell)( shellName ) )
		End Sub

		Protected Overrides Sub ChangeActiveItem( newItem As IShell,
		                                          closePrevious As Boolean )
			MyBase.ChangeActiveItem( newItem, closePrevious )

			' update view
			NotifyOfPropertyChange( Function() Shell )

			' update window display name
			If Shell Is Nothing
				DisplayName = "Hotelie"
			Else
				DisplayName = $"Hotelie - {Shell.DisplayName}"
			End If
		End Sub

		' Window actions

		Public Sub DragMove() Implements IMainWindow.DragMove
			Windows.Application.Current.MainWindow.DragMove()
		End Sub

		Public Sub ToggleZoomState() Implements IMainWindow.ToggleZoomState
			WindowState = 2 - WindowState
		End Sub

		Public Sub Hide() Implements IMainWindow.Hide
			WindowState = WindowState.Minimized
		End Sub

		Public Sub Close() Implements IMainWindow.Close
			Windows.Application.Current.MainWindow.Close()
		End Sub

		' Notification

		Public Sub ShowStaticNotification( type As Integer,
		                                   text As String ) Implements IMainWindow.ShowStaticNotification
			StaticNotification.Type = type
			StaticNotification.Text = text
		End Sub

		Public Sub CloseStaticNotification() Implements IMainWindow.CloseStaticNotification
			StaticNotification.Type = StaticNotificationType.None
			StaticNotification.Text = String.Empty
		End Sub

		' Dialog

		Public Sub ShowStaticWindowDialog( content As Object ) Implements IMainWindow.ShowStaticWindowDialog
			StaticWindowDialog.Content = content
			StaticWindowDialog.IsVisible = True
		End Sub

		Public Sub CloseStaticWindowDialog() Implements IMainWindow.CloseStaticWindowDialog
			StaticWindowDialog.Content = Nothing
			StaticWindowDialog.IsVisible = False
		End Sub

		Public Sub ShowStaticShellDialog( content As Object ) Implements IMainWindow.ShowStaticShellDialog
			StaticShellDialog.Content = content
			StaticShellDialog.IsVisible = True
		End Sub

		Public Sub CloseStaticShellDialog() Implements IMainWindow.CloseStaticShellDialog
			StaticShellDialog.Content = Nothing
			StaticShellDialog.IsVisible = False
		End Sub
	End Class
End Namespace
