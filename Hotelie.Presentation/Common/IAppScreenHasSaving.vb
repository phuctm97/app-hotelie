Namespace Common
	Public Interface IAppScreenHasSaving
		Inherits IAppScreen

		ReadOnly Property IsEdited As Boolean

		Function CanExit( Optional message As String = "Thoát mà không lưu các thay đổi?",
		                  Optional buttonExit As String = "THOÁT",
		                  Optional buttonSave As String = "LƯU",
		                  Optional buttonCancel As String = "HỦY" ) As Task(Of Integer)

		Sub ActualExit()

		Function ActualExitAsync() As Task

		Function CanSave() As Task(Of Boolean)

		Sub Save()

		Function SaveAsync() As Task

		Sub ActualSave()

		Function ActualSaveAsync() As Task
	End Interface
End Namespace