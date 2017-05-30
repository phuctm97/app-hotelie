
Imports System.Windows.Media.Animation

Namespace Start.LoginShell.Views
	Public Class LoginShellView
		Private _previousPasswordLength As Integer
		Private _previousAccountLength As Integer
		Private _previousServerNameLength As Integer
		Private _previousDatabaseNameLength As Integer

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

		' Window command

		Private Sub OnTitleBarLeftMouseDown( sender As Object,
		                                     e As MouseButtonEventArgs )
			If e.GetPosition( TitleBar ).Y < TitleBar.ActualHeight And
			   e.GetPosition( WindowCommandPopups ).X < 0
				Windows.Application.Current.MainWindow.DragMove()
			End If
		End Sub

		Private Sub OnTitleBarMouseDoubleClick( sender As Object,
		                                        e As MouseButtonEventArgs )
			If Not e.ChangedButton = MouseButton.Left Then Return
			If e.GetPosition( TitleBar ).Y > TitleBar.ActualHeight Then Return
			If ButtonSettings.Visibility = Visibility.Visible And
			   e.GetPosition( ButtonSettings ).X > 0 Then Return
			If ButtonLogin.Visibility = Visibility.Visible And
			   e.GetPosition( ButtonLogin ).X > 0 Then Return

			OnZoomButtonClick( Nothing, Nothing )
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

		' Textbox effects

		Private Sub InitEffectParameters()
			_previousPasswordLength = 0
			_previousAccountLength = 0
			_previousServerNameLength = 0
			_previousDatabaseNameLength = 0
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

		Private Sub OnServerChanged( sender As Object,
		                             e As TextChangedEventArgs )
			Dim serverBox = CType(sender, TextBox)

			If _previousServerNameLength = 0 And serverBox.Text.Length > 0
				' animate box down
				Dim marginAnimation = New ThicknessAnimation( New Thickness( 40, 16, 40, 16 ),
				                                              New Duration( TimeSpan.FromMilliseconds( 400 ) ) )
				serverBox.BeginAnimation( MarginProperty, marginAnimation )

			ElseIf _previousServerNameLength > 0 And serverBox.Text.Length = 0
				' animate box up
				Dim marginAnimation = New ThicknessAnimation( New Thickness( 40, 0, 40, 16 ),
				                                              New Duration( TimeSpan.FromMilliseconds( 400 ) ) )
				serverBox.BeginAnimation( MarginProperty, marginAnimation )
			End If

			_previousServerNameLength = serverBox.Text.Length
		End Sub

		Private Sub OnDatabaseChanged( sender As Object,
		                               e As TextChangedEventArgs )
			Dim databaseBox = CType(sender, TextBox)

			If _previousDatabaseNameLength = 0 And databaseBox.Text.Length > 0
				' animate box down
				Dim marginAnimation = New ThicknessAnimation( New Thickness( 40, 16, 40, 32 ),
				                                              New Duration( TimeSpan.FromMilliseconds( 400 ) ) )
				databaseBox.BeginAnimation( MarginProperty, marginAnimation )

			ElseIf _previousDatabaseNameLength > 0 And databaseBox.Text.Length = 0
				' animate box up
				Dim marginAnimation = New ThicknessAnimation( New Thickness( 40, 0, 40, 32 ),
				                                              New Duration( TimeSpan.FromMilliseconds( 400 ) ) )
				databaseBox.BeginAnimation( MarginProperty, marginAnimation )
			End If

			_previousDatabaseNameLength = databaseBox.Text.Length
		End Sub

		' Form switch

		Private Sub OnButtonSettingsClick( sender As Object,
		                                   e As RoutedEventArgs )
			ButtonSettings.Visibility = Visibility.Collapsed
			DescriptionSettings.Visibility = Visibility.Collapsed

			ButtonLogin.Visibility = Visibility.Visible
			DescriptionLogin.Visibility = Visibility.Visible

			FormLogin.Visibility = Visibility.Collapsed
			FormSettings.Visibility = Visibility.Visible
		End Sub

		Private Sub OnButtonLoginClick( sender As Object,
		                                e As RoutedEventArgs )
			ButtonLogin.Visibility = Visibility.Collapsed
			DescriptionLogin.Visibility = Visibility.Collapsed

			ButtonSettings.Visibility = Visibility.Visible
			DescriptionSettings.Visibility = Visibility.Visible

			FormLogin.Visibility = Visibility.Visible
			FormSettings.Visibility = Visibility.Collapsed
		End Sub
	End Class
End Namespace