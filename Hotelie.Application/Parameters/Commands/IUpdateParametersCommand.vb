Namespace Parameters.Commands
	Public Interface IUpdateParametersCommand
		Function Execute( roomCapacity As Integer,
		                  extraCoefficient As Double ) As String

		Function ExecuteAsync( roomCapacity As Integer,
		                       extraCoefficient As Double ) As Task(Of String)
	End Interface
End Namespace