
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Rooms.Views
	Public Class RoomsWorkspaceView
		Private Sub OnButtonResetSearchClicked( sender As Object,
		                                        e As RoutedEventArgs )
			TextBoxSearchName.Text = String.Empty
			ComboBoxSearchState.SelectedIndex = ComboBoxSearchState.Items.Count - 1
			ComboBoxSearchCategory.SelectedIndex = ComboBoxSearchCategory.Items.Count - 1
		End Sub

		Private Sub OnButtonRoomCategoryDisplayClicked( sender As Object,
		                                                e As RoutedEventArgs )
			Dim button = CType(sender, Button)
			Dim room = CType(button.DataContext, RoomModel)

			For Each category As RoomCategoryModel In ComboBoxSearchCategory.ItemsSource
				If room.CategoryId = category.Id
					ComboBoxSearchCategory.SelectedItem = category
				End If
			Next
		End Sub

		Private Sub OnButtonRoomStateDisplayClicked( sender As Object,
		                                             e As RoutedEventArgs )
			Dim button = CType(sender, Button)
			Dim room = CType(button.DataContext, RoomModel)

			For Each state As Integer In ComboBoxSearchState.ItemsSource
				If room.State = state
					ComboBoxSearchState.SelectedItem = state
				End If
			Next
		End Sub
	End Class
End Namespace
