Imports System.Text.RegularExpressions

Namespace Rules.Views
	Public Class ScreenChangeRulesView
		Private Sub OnIntegerTextboxPreviewTextInput( sender As Object,
		                                              e As TextCompositionEventArgs )
			e.Handled = Not IsNumericString( e.Text )
		End Sub

		Private Sub OnIntegerTextBoxLostFocus( sender As Object,
		                                       e As RoutedEventArgs )
			Dim textBox = CType(sender, TextBox)
			If String.IsNullOrWhiteSpace( textBox.Text ) Then _
				textBox.Text = "1"
		End Sub

		Private Function IsNumericString( str As String ) As Boolean
			Return Regex.IsMatch( str, "[0-9]+" )
		End Function
	End Class
End Namespace
