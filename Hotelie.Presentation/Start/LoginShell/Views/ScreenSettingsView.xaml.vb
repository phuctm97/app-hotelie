Imports System.Windows.Media.Animation

Namespace Start.LoginShell.Views
	Public Class ScreenSettingsView
		' For textbox effects
		Private _previousServerNameLength As Integer
		Private _previousDatabaseNameLength As Integer

		Public Sub New()

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.
			InitEffectParameters()
		End Sub

		' Textbox effects

		Private Sub InitEffectParameters()
			_previousServerNameLength = 0
			_previousDatabaseNameLength = 0
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
	End Class
End Namespace
