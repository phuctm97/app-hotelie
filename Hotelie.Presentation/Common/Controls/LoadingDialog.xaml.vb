Namespace Common.Controls
	Public Class LoadingDialog
		Public Sub New( Optional loadingText As String = "" )

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.		

			If String.IsNullOrWhiteSpace( loadingText )
				Me.LoadingTextBlock.Visibility = Visibility.Collapsed
			Else
				Me.LoadingTextBlock.Visibility = Visibility.Visible
				Me.LoadingTextBlock.Text = loadingText
			End If
		End Sub
	End Class
End Namespace
