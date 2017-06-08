Imports Hotelie.Application.Parameters.Models
Imports Hotelie.Application.Services.Persistence

Namespace Parameters.Queries
    Public Class GetParametersQuery
        Implements IGetParametersQuery

        Private ReadOnly _parameterRepository As IParameterRepository

        Sub New(parameterRepository As IParameterRepository)
            _parameterRepository = parameterRepository
        End Sub

        Public Function Execute() As IParameterModel Implements IGetParametersQuery.Execute
          Dim parameter = _parameterRepository.GetRules()
            Return New ParameterModel(parameter)
        End Function

        Public Async Function ExecuteAsync() As Task(Of IParameterModel) Implements IGetParametersQuery.ExecuteAsync
            Dim parameter = Await _parameterRepository.GetRulesAsync()
            Return New ParameterModel(parameter)
        End Function
    End Class
End NameSpace