Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Start.MainWindow.ViewModels
	Public Class MainWindowViewModel
		Inherits Conductor(Of IShell)
		Implements IMainWindow

		Private _title As String
		Private _width As Double
		Private _height As Double
		Private _windowState As WindowState

		Public Sub New()
			Title = "Hotelie"
		End Sub

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
				If Double.Equals( _width, Value ) Then Return

				_width = value
				NotifyOfPropertyChange( Function() Width )
			End Set
		End Property

		Public Property Height As Double Implements IMainWindow.Height
			Get
				Return _height
			End Get
			Set
				If Double.Equals( _height, Value ) Then Return

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

		Public Sub ShowLoginShell() Implements IMainWindow.ShowLoginShell
			Title = "Hotelie - Login"

			Width = 960

			Height = 540

			WindowState = WindowState.Normal

			ActivateItem( IoC.Get(Of IShell)( "login-shell" ) )
		End Sub

		Public Sub ShowWorkspaceShell() Implements IMainWindow.ShowWorkspaceShell
			Title = "Hotelie - Workspace"

			WindowState = WindowState.Maximized

			ActivateItem( IoC.Get(Of IShell)( "workspace-shell" ) )
		End Sub
	End Class
End Namespace
