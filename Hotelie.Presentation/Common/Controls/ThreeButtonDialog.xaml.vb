Namespace Common.Controls
	Public Class ThreeButtonDialog
		Public Sub New( question As String,
		                button1 As String,
		                button2 As String,
		                button3 As String,
		                Optional button1Accent As Boolean = False,
		                Optional button2Accent As Boolean = False,
		                Optional button3Accent As Boolean = False )

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.		

			Me.QuestionTextBlock.Text = question
			Me.Button1.Content = button1
			Me.Button2.Content = button2
			Me.Button3.Content = button3

			If button1Accent Then Me.Button1.Style = FindResource("MaterialDesignFlatAccentButton")
			If button2Accent Then Me.Button2.Style = FindResource("MaterialDesignFlatAccentButton")
			If button3Accent Then Me.Button3.Style = FindResource("MaterialDesignFlatAccentButton")
		End Sub
	End Class
End Namespace
