Namespace Start.WorkspaceShell.Views
	Public Class WorkspaceShellView
		Public Sub New()

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.
			InitWindowState()
		End Sub

		Private Sub InitWindowState()
			If Application.Current.MainWindow.WindowState = WindowState.Minimized Then _
				Application.Current.MainWindow.WindowState = WindowState.Normal
			ButtonZoomDisplay.Text =
				If( Application.Current.MainWindow.WindowState = WindowState.Maximized, "Thu nhỏ", "Phóng to" )
		End Sub

		Private Sub OnTitleBarLeftMouseDown( sender As Object,
		                                     e As MouseButtonEventArgs )
			If e.GetPosition( TitleBar ).Y > TitleBar.ActualHeight Then Return
			If e.GetPosition( UserCommandsPopupBox ).X > 0 Then Return

			Windows.Application.Current.MainWindow.DragMove()
		End Sub

		Private Sub OnTitleBarMouseDoubleClick( sender As Object,
		                                        e As MouseButtonEventArgs )
			If Not e.ChangedButton = MouseButton.Left Then Return
			If e.GetPosition( TitleBar ).Y > TitleBar.ActualHeight Then Return
			If e.GetPosition( UserCommandsPopupBox ).X > 0 Then Return

			OnZoomButtonClick( Nothing, Nothing )
		End Sub

		Private Sub OnZoomButtonClick( sender As Object,
		                               e As RoutedEventArgs )
			Select Case Application.Current.MainWindow.WindowState
				Case WindowState.Normal
					Windows.Application.Current.MainWindow.WindowState = WindowState.Maximized
					ButtonZoomDisplay.Text = "Thu nhỏ"
				Case Else
					Windows.Application.Current.MainWindow.WindowState = WindowState.Normal
					ButtonZoomDisplay.Text = "Phóng to"
			End Select
		End Sub

		Private Sub OnHideButtonClick( sender As Object,
		                               e As RoutedEventArgs )
			Windows.Application.Current.MainWindow.WindowState = WindowState.Minimized
		End Sub

		Private Sub OnCloseButtonClick( sender As Object,
		                                e As RoutedEventArgs )
			Windows.Application.Current.MainWindow.Close()
		End Sub
	End Class
End Namespace
