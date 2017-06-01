

Namespace Rooms.Views
	Public Class RoomsWorkspaceView
		Private Sub OnButtonResetSearchClicked( sender As Object,
		                                        e As RoutedEventArgs )
			TextBoxSearchName.Text = String.Empty
			ComboBoxSearchState.SelectedIndex = ComboBoxSearchState.Items.Count - 1
			ComboBoxSearchCategory.SelectedIndex = ComboBoxSearchCategory.Items.Count - 1
		End Sub
	End Class
End Namespace
