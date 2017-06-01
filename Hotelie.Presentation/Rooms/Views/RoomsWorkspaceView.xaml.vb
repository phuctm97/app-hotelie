

Namespace Rooms.Views
	Public Class RoomsWorkspaceView
		Private Sub OnButtonResetSearchClicked( sender As Object,
		                                        e As RoutedEventArgs )
			TextBoxSearchName.Text = String.Empty

			ComboBoxSearchCategory.SelectedIndex = - 1

			ComboBoxSearchState.SelectedIndex = - 1
		End Sub

		Private Sub OnComboxBoxSearchDoubleClick( sender As Object,
		                                          e As MouseButtonEventArgs )
			Dim comboxBox = CType(sender, ComboBox)
			If comboxBox Is Nothing Then Return

			comboxBox.SelectedIndex = - 1
		End Sub
	End Class
End Namespace
