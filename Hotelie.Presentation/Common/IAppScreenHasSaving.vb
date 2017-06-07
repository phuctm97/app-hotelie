Namespace Common
	Public Interface IAppScreenHasSaving
		Inherits IAppScreen

		Property IsEdited As Boolean

		Function CanExit( Optional message As String = "Thoát mà không lưu các thay đổi?",
		                  Optional buttonExit As String = "THOÁT",
		                  Optional buttonSave As String = "LƯU",
		                  Optional buttonCancel As String = "HỦY" ) As Task(Of Integer)

		Sub ActualExit()

		Function ActualExitAsync() As Task

		Function Save() As Object

		Function SaveAsync() As Task(Of Object)
	End Interface
End Namespace