Namespace Start.WorkspaceShell.Views
	Public Class WorkspaceShellView
		Private Sub OnTitleBarLeftMouseDown( sender As Object,
		                                     e As MouseButtonEventArgs )
			If e.GetPosition( TitleBar ).Y > TitleBar.ActualHeight Then Return
			If e.GetPosition( TabItems ).X > 0 Then Return

			Windows.Application.Current.MainWindow.DragMove()
		End Sub

		Private Sub OnTitleBarMouseDoubleClick( sender As Object,
		                                        e As MouseButtonEventArgs )
			If Not e.ChangedButton = MouseButton.Left Then Return
			If e.GetPosition( TitleBar ).Y > TitleBar.ActualHeight Then Return
			If e.GetPosition( TabItems ).X > 0 Then Return

			ButtonToggleWindowZoomState.RaiseEvent( New RoutedEventArgs( Primitives.ButtonBase.ClickEvent ) )
		End Sub
	End Class
End Namespace
