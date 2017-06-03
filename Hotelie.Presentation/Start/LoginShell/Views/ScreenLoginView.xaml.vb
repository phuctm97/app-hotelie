Imports System.Windows.Media.Animation

Namespace Start.LoginShell.Views
	Public Class ScreenLoginView
		' For textbox effects
		Private _previousPasswordLength As Integer
		Private _previousAccountLength As Integer

		Public Sub New()

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.
			InitEffectParameters()
		End Sub

		' Textbox effects

		Private Sub InitEffectParameters()
			_previousPasswordLength = 0
			_previousAccountLength = 0
		End Sub

		Private Sub OnPasswordChanged( sender As Object,
		                               e As RoutedEventArgs )
			Dim passwordBox = CType(sender, PasswordBox)
			PasswordStorage.Text = passwordBox.Password

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