Namespace Start.LoginShell.Views
	Public Class LoginShellView
		Public Sub New()

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.
			InitWindowState()
		End Sub

		Private Sub InitWindowState()
			If Application.Current.MainWindow.WindowState = WindowState.Minimized Then _
				Application.Current.MainWindow.WindowState = WindowState.Normal
			ButtonZoom.ToolTip = If( Application.Current.MainWindow.WindowState = WindowState.Maximized, "Thu nhỏ", "Phóng to" )
		End Sub

		Private Sub OnZoomButtonClick( sender As Object,
		                               e As RoutedEventArgs )
			If Application.Current.MainWindow.WindowState = WindowState.Normal
				Application.Current.MainWindow.WindowState = WindowState.Maximized
				ButtonZoom.ToolTip = "Thu nhỏ"
			Else
				Application.Current.MainWindow.WindowState = WindowState.Normal
				ButtonZoom.ToolTip = "Phóng to"
			End If
		End Sub

		Private Sub OnHideButtonClick( sender As Object,
		                               e As RoutedEventArgs )
			Application.Current.MainWindow.WindowState = WindowState.Minimized
		End Sub

		Private Sub OnCloseButtonClick( sender As Object,
		                                e As RoutedEventArgs )
			Application.Current.MainWindow.Close()
		End Sub

		Private Sub OnTitleBarLeftMouseDown( sender As Object,
		                                     e As MouseButtonEventArgs )
			Application.Current.MainWindow.DragMove()
		End Sub

		Private Sub OnTitleBarMouseDoubleClick( sender As Object,
		                                        e As MouseButtonEventArgs )
			If e.ChangedButton = MouseButton.Left And
			   e.GetPosition( TitleBar ).Y < TitleBar.ActualHeight
				OnZoomButtonClick( Nothing, Nothing )
			End If
		End Sub
	End Class
End Namespace