Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Rules.Models
Imports MaterialDesignThemes.Wpf

Namespace Rules.ViewModels
	Public Class ScreenChangeRulesViewModel
		Inherits AppScreenHasSaving

		Private _isEdited As Boolean

		Public Property Rule As RuleModel

		Public Property Username As String

		Public Sub New()
			MyBase.New( ColorZoneMode.Accent )

			DisplayName = "Thay đổi quy định"
			Username = "<tên người dùng>"
			Rule = New RuleModel()
		End Sub

		Private Sub ResetValues()
			Username = "<tên người dùng>"
			Rule.CustomerCategories.Clear()
			Rule.RoomCategories.Clear()
			Rule.RoomCapacity = 0
			Rule.ExtraCoefficient = 0
		End Sub

		' Show
		Public Overrides Sub Show()
			ReloadRules()
		End Sub

		Private Sub ReloadRules()
		End Sub

		Public Overrides Async Function ShowAsync() As Task
			Await ReloadRulesAsync()
		End Function

		Private Async Function ReloadRulesAsync() As Task
			Await Task.Delay( 0 )
		End Function

		' Domain actions
		Public Sub AddEmptyRoomCategory()
		End Sub

		Public Sub AddEmptyCustomerCategory()
		End Sub
	End Class
End Namespace