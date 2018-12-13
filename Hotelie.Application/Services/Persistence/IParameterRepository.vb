Imports Hotelie.Domain.Parameters

Namespace Services.Persistence
    Public Interface IParameterRepository
        Function GetRules() As Parameter
        Function GetRulesAsync() As Task(Of Parameter)
    End Interface
End NameSpace