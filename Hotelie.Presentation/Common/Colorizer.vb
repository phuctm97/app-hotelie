Namespace Common
	Public Class Colorizer

#Region "Presets"

		Public Shared ReadOnly B0 As Color = Color.FromRgb( 255, 0, 0 )
		Public Shared ReadOnly F0 As Color = Colors.White

		Public Shared ReadOnly B1 As Color = Color.FromRgb( 255, 0, 128 )
		Public Shared ReadOnly F1 As Color = Colors.White

		Public Shared ReadOnly B2 As Color = Color.FromRgb( 255, 128, 0 )
		Public Shared ReadOnly F2 As Color = Colors.White

		Public Shared ReadOnly B3 As Color = Color.FromRgb( 255, 255, 0 )
		Public Shared ReadOnly F3 As Color = Colors.Black

		Public Shared ReadOnly B4 As Color = Color.FromRgb( 222, 224, 123 )
		Public Shared ReadOnly F4 As Color = Colors.Black

		Public Shared ReadOnly B5 As Color = Color.FromRgb( 191, 113, 35 )
		Public Shared ReadOnly F5 As Color = Colors.White

		Public Shared ReadOnly B6 As Color = Color.FromRgb( 204, 204, 204 )
		Public Shared ReadOnly F6 As Color = Colors.Black

		Public Shared ReadOnly B7 As Color = Color.FromRgb( 51, 51, 51 )
		Public Shared ReadOnly F7 As Color = Colors.White

		Public Shared ReadOnly B8 As Color = Color.FromRgb( 128, 0, 255 )
		Public Shared ReadOnly F8 As Color = Colors.White

		Public Shared ReadOnly B9 As Color = Color.FromRgb( 68, 68, 255 )
		Public Shared ReadOnly F9 As Color = Colors.White

		Public Shared ReadOnly B10 As Color = Color.FromRgb( 0, 218, 220 )
		Public Shared ReadOnly F10 As Color = Colors.White

		Public Shared ReadOnly B11 As Color = Color.FromRgb( 0, 255, 0 )
		Public Shared ReadOnly F11 As Color = Colors.White

		Public Shared ReadOnly PresetColors As CategoryColor()

		Shared Sub New()
			PresetColors = {New CategoryColor With {.Name="C0", .BackColor=B0, .ForeColor=F0},
			                New CategoryColor With {.Name="C1", .BackColor=B1, .ForeColor=F1},
			                New CategoryColor With {.Name="C2", .BackColor=B2, .ForeColor=F2},
			                New CategoryColor With {.Name="C3", .BackColor=B3, .ForeColor=F3},
			                New CategoryColor With {.Name="C4", .BackColor=B4, .ForeColor=F4},
			                New CategoryColor With {.Name="C5", .BackColor=B5, .ForeColor=F5},
			                New CategoryColor With {.Name="C6", .BackColor=B6, .ForeColor=F6},
			                New CategoryColor With {.Name="C7", .BackColor=B7, .ForeColor=F7},
			                New CategoryColor With {.Name="C8", .BackColor=B8, .ForeColor=F8},
			                New CategoryColor With {.Name="C9", .BackColor=B9, .ForeColor=F9},
			                New CategoryColor With {.Name="C10", .BackColor=B10, .ForeColor=F10},
			                New CategoryColor With {.Name="C11", .BackColor=B11, .ForeColor=F11}}
		End Sub

#End Region

		Dim ReadOnly _groups As Dictionary(Of String, Dictionary(Of String, CategoryColor))
		Dim ReadOnly _groupColorIterators As Dictionary(Of String, Integer)

		Public Sub New()
			_groups = New Dictionary(Of String, Dictionary(Of String, CategoryColor))()
			_groupColorIterators = New Dictionary(Of String, Integer)()
		End Sub

		Public Function GetGroup( id As String ) As Dictionary(Of String, CategoryColor)
			If Not _groups.ContainsKey( id )
				_groups.Add( id, New Dictionary(Of String, CategoryColor)() )
				_groupColorIterators.Add( id, 0 )
			End If

			Return _groups( id )
		End Function

		Public Function GetColor( groupId As String,
		                          elementId As String ) As CategoryColor
			Dim group = GetGroup( groupId )

			If Not group.ContainsKey( elementId )
				group.Add( elementId, PresetColors( _groupColorIterators( groupId ) ) )

				_groupColorIterators( groupId ) = (_groupColorIterators( groupId ) + 1) Mod PresetColors.Length
			End If

			Return group( elementId )
		End Function
	End Class

	Public Structure CategoryColor
		Public Property Name As String

		Public Property ForeColor As Color

		Public Property BackColor As Color
	End Structure
End Namespace