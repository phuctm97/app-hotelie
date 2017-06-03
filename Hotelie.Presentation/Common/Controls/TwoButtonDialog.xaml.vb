Namespace Common.Controls
	Public Class TwoButtonDialog
		Public Sub New( question As String,
		                button1 As String,
		                button2 As String )

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.		

			Me.QuestionTextBlock.Text = question
			Me.Button1.Content = button1
			Me.Button2.Content = button2
		End Sub
	End Class
End Namespace
