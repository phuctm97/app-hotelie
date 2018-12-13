Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Parameters

Namespace Parameters
    Public Class ParameterRepository
        Implements IParameterRepository

        Private _databaseService As IDatabaseService

        Sub New(databaseService As IDatabaseService)
            _databaseService = databaseService
        End Sub

        Public Function GetRules() As Parameter Implements IParameterRepository.GetRules
            Return _databaseService.Context.Parameters.FirstOrDefault()
        End Function

        Public Async Function GetRulesAsync() As Task(Of Parameter) Implements IParameterRepository.GetRulesAsync
            Return Await _databaseService.Context.Parameters.FirstOrDefaultAsync()
        End Function
    End Class
End NameSpace