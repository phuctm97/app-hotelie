Public Class ChangePasswordDialog
	Private Sub OnOldPasswordChanged( sender As Object,
	                                  e As RoutedEventArgs )
		OldPasswordStorage.Text = TextBoxOldPassword.Password
	End Sub

	Private Sub OnNewPasswordChanged( sender As Object,
	                                  e As RoutedEventArgs )
		NewPasswordStorage.Text = TextBoxNewPassword.Password
	End Sub

	Private Sub OnConfirmPasswordChanged( sender As Object,
	                                      e As RoutedEventArgs )
		ConfirmPasswordStorage.Text = TextBoxConfirmPassword.Password
	End Sub
End Class
