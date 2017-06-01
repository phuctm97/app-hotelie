Namespace Common
	Public Module CategoryColors
		Public ReadOnly B0 As Color = Color.FromRgb( 255, 0, 0 )
		Public ReadOnly F0 As Color = Colors.White
		Public ReadOnly CategoryColor0 As CategoryColor = New CategoryColor With {.Name="C0", .BackColor=B0, .ForeColor=F0}

		Public ReadOnly B1 As Color = Color.FromRgb( 255, 0, 128 )
		Public ReadOnly F1 As Color = Colors.White
		Public ReadOnly CategoryColor1 As CategoryColor = New CategoryColor With {.Name="C1", .BackColor=B1, .ForeColor=F1}

		Public ReadOnly B2 As Color = Color.FromRgb( 255, 128, 0 )
		Public ReadOnly F2 As Color = Colors.White
		Public ReadOnly CategoryColor2 As CategoryColor = New CategoryColor With {.Name="C2", .BackColor=B2, .ForeColor=F2}

		Public ReadOnly B3 As Color = Color.FromRgb( 255, 255, 0 )
		Public ReadOnly F3 As Color = Colors.Black
		Public ReadOnly CategoryColor3 As CategoryColor = New CategoryColor With {.Name="C3", .BackColor=B3, .ForeColor=F3}

		Public ReadOnly B4 As Color = Color.FromRgb( 222, 224, 123 )
		Public ReadOnly F4 As Color = Colors.Black
		Public ReadOnly CategoryColor4 As CategoryColor = New CategoryColor With {.Name="C4", .BackColor=B4, .ForeColor=F4}

		Public ReadOnly B5 As Color = Color.FromRgb( 191, 113, 35 )
		Public ReadOnly F5 As Color = Colors.White
		Public ReadOnly CategoryColor5 As CategoryColor = New CategoryColor With {.Name="C5", .BackColor=B5, .ForeColor=F5}

		Public ReadOnly B6 As Color = Color.FromRgb( 204, 204, 204 )
		Public ReadOnly F6 As Color = Colors.Black
		Public ReadOnly CategoryColor6 As CategoryColor = New CategoryColor With {.Name="C6", .BackColor=B6, .ForeColor=F6}

		Public ReadOnly B7 As Color = Color.FromRgb( 51, 51, 51 )
		Public ReadOnly F7 As Color = Colors.White
		Public ReadOnly CategoryColor7 As CategoryColor = New CategoryColor With {.Name="C7", .BackColor=B7, .ForeColor=F7}

		Public ReadOnly B8 As Color = Color.FromRgb( 128, 0, 255 )
		Public ReadOnly F8 As Color = Colors.White
		Public ReadOnly CategoryColor8 As CategoryColor = New CategoryColor With {.Name="C8", .BackColor=B8, .ForeColor=F8}

		Public ReadOnly B9 As Color = Color.FromRgb( 68, 68, 255 )
		Public ReadOnly F9 As Color = Colors.White
		Public ReadOnly CategoryColor9 As CategoryColor = New CategoryColor With {.Name="C9", .BackColor=B9, .ForeColor=F9}

		Public ReadOnly B10 As Color = Color.FromRgb( 0, 218, 220 )
		Public ReadOnly F10 As Color = Colors.White

		Public ReadOnly _
			CategoryColor10 As CategoryColor = New CategoryColor With {.Name="C10", .BackColor=B10, .ForeColor=F10}

		Public ReadOnly B11 As Color = Color.FromRgb( 0, 255, 0 )
		Public ReadOnly F11 As Color = Colors.White

		Public ReadOnly _
			CategoryColor11 As CategoryColor = New CategoryColor With {.Name="C11", .BackColor=B11, .ForeColor=F11}
	End Module

	Public Structure CategoryColor
		Public Property Name As String

		Public Property ForeColor As Color

		Public Property BackColor As Color
	End Structure
End Namespace