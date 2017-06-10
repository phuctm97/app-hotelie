Namespace Users.Views
	Public Class DialogAddUser
		Private Sub OnNewPasswordChanged( sender As Object,
		                                  e As RoutedEventArgs )
			NewPasswordStorage.Text = TextBoxNewPassword.Password
		End Sub

		Private Sub OnConfirmPasswordChanged( sender As Object,
		                                      e As RoutedEventArgs )
			ConfirmPasswordStorage.Text = TextBoxConfirmPassword.Password
		End Sub
	End Class
End Namespace
