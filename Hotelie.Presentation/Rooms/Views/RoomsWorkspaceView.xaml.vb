Imports MaterialDesignThemes.Wpf

Namespace Rooms.Views
	Public Class RoomsWorkspaceView
		Private Sub OnButtonResetSearchClicked( sender As Object,
		                                        e As RoutedEventArgs )
			TextBoxSearchName.Text = String.Empty

			ComboBoxSearchCategory.SelectedIndex = -1

			ComboBoxSearchState.SelectedIndex = -1
		End Sub
	End Class
End Namespace
