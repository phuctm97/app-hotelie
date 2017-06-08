Imports Hotelie.Application.Parameters.Models

Namespace Parameters.Queries
	Public Interface IGetParametersQuery
		Function Execute() As IParameterModel

		Function ExecuteAsync() As Task(Of IParameterModel)
	End Interface
End	Namespace