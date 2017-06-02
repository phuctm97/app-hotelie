Imports Hotelie.Presentation.Common

Namespace Start.MainWindow.Views
	Public Class MainWindowView
		' Window command
		Private Sub OnTitleBarLeftMouseDown( sender As Object,
		                                     e As MouseButtonEventArgs )
			If e.GetPosition( TitleBar ).Y > TitleBar.ActualHeight Then Return
			If e.GetPosition( CommandsBar ).X > 0 Then Return

			CType(DataContext, IMainWindow).DragMove()
		End Sub

		Private Sub OnTitleBarMouseDoubleClick( sender As Object,
		                                        e As MouseButtonEventArgs )
			If Not e.ChangedButton = MouseButton.Left Then Return
			If e.GetPosition( TitleBar ).Y > TitleBar.ActualHeight Then Return
			If e.GetPosition( CommandsBar ).X > 0 Then Return

			CType(DataContext, IMainWindow).ToggleZoomState()
		End Sub
	End Class
End Namespace
