Namespace Common
	Public Interface IAppScreenHasDeleting
		Inherits IAppScreen

		Function CanDelete() As Task(Of Boolean)

		Sub Delete()

		Function DeleteAsync() As Task

		Sub ActualDelete()

		Function ActualDeleteAsync() As Task

	End Interface
End Namespace