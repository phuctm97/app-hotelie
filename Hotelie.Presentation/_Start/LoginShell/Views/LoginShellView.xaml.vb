
Imports System.Windows.Media.Animation

Namespace Start.LoginShell.Views
	Public Class LoginShellView
		Private _previousPasswordLength As Integer
		Private _previousAccountLength As Integer

		Public Sub New()

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.
			InitWindowState()

			InitEffectParameters()
		End Sub

		Private Sub InitWindowState()
			If Application.Current.MainWindow.WindowState = WindowState.Minimized Then _
				Application.Current.MainWindow.WindowState = WindowState.Normal
			ButtonZoomDisplay.Text =
				If( Application.Current.MainWindow.WindowState = WindowState.Maximized, "Thu nhỏ", "Phóng to" )
		End Sub

		Private Sub InitEffectParameters()
			_previousPasswordLength = 0
			_previousAccountLength = 0
		End Sub

		' Drag window
		Private Sub OnTitleBarLeftMouseDown( sender As Object,
		                                     e As MouseButtonEventArgs )
			If e.GetPosition( TitleBar ).Y < TitleBar.ActualHeight And
			   e.GetPosition( WindowCommandPopups ).X < 0
				Windows.Application.Current.MainWindow.DragMove()
			End If
		End Sub

		Private Sub OnTitleBarMouseDoubleClick( sender As Object,
		                                        e As MouseButtonEventArgs )
			If e.ChangedButton = MouseButton.Left And
			   e.GetPosition( TitleBar ).Y < TitleBar.ActualHeight And
			   e.GetPosition( WindowCommandPopups ).X < 0
				OnZoomButtonClick( Nothing, Nothing )
			End If
		End Sub

		Private Sub OnZoomButtonClick( sender As Object,
		                               e As RoutedEventArgs )
			Select Case Application.Current.MainWindow.WindowState
				Case WindowState.Normal
					Windows.Application.Current.MainWindow.WindowState = WindowState.Maximized
					ButtonZoomDisplay.Text = "Thu nhỏ"
				Case Else
					Windows.Application.Current.MainWindow.WindowState = WindowState.Normal
					ButtonZoomDisplay.Text = "Phóng to"
			End Select
		End Sub

		Private Sub OnHideButtonClick( sender As Object,
		                               e As RoutedEventArgs )
			Windows.Application.Current.MainWindow.WindowState = WindowState.Minimized
		End Sub

		Private Sub OnCloseButtonClick( sender As Object,
		                                e As RoutedEventArgs )
			Windows.Application.Current.MainWindow.Close()
		End Sub

		Private Sub OnPasswordChanged( sender As Object,
		                               e As RoutedEventArgs )
			Dim passwordBox = CType(sender, PasswordBox)

			If _previousPasswordLength = 0 And passwordBox.Password.Length > 0
				' animate box down
				Dim marginAnimation = New ThicknessAnimation( New Thickness( 40, 16, 40, 32 ),
				                                              New Duration( TimeSpan.FromMilliseconds( 400 ) ) )
				passwordBox.BeginAnimation( MarginProperty, marginAnimation )

			ElseIf _previousPasswordLength > 0 And passwordBox.Password.Length = 0
				' animate box up
				Dim marginAnimation = New ThicknessAnimation( New Thickness( 40, 0, 40, 32 ),
				                                              New Duration( TimeSpan.FromMilliseconds( 400 ) ) )
				passwordBox.BeginAnimation( MarginProperty, marginAnimation )
			End If

			_previousPasswordLength = passwordBox.Password.Length
		End Sub

		Private Sub OnAccountChanged( sender As Object,
		                              e As TextChangedEventArgs )
			Dim accountBox = CType(sender, TextBox)

			If _previousAccountLength = 0 And accountBox.Text.Length > 0
				' animate box down
				Dim marginAnimation = New ThicknessAnimation( New Thickness( 40, 16, 40, 16 ),
				                                              New Duration( TimeSpan.FromMilliseconds( 400 ) ) )
				accountBox.BeginAnimation( MarginProperty, marginAnimation )

			ElseIf _previousAccountLength > 0 And accountBox.Text.Length = 0
				' animate box up
				Dim marginAnimation = New ThicknessAnimation( New Thickness( 40, 0, 40, 16 ),
				                                              New Duration( TimeSpan.FromMilliseconds( 400 ) ) )
				accountBox.BeginAnimation( MarginProperty, marginAnimation )
			End If

			_previousAccountLength = accountBox.Text.Length
		End Sub
	End Class
End Namespace