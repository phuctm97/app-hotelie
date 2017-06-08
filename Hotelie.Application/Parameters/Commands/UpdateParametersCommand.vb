Imports Hotelie.Application.Services.Persistence

Namespace Parameters.Commands
    Public Class UpdateParametersCommand
        Implements IUpdateParametersCommand

        Private ReadOnly _parameterRepository As IParameterRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(parameterRepository As IParameterRepository, unitOfWork As IUnitOfWork)
            _parameterRepository = parameterRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(roomCapacity As Integer, extraCoefficient As Double) As String _
            Implements IUpdateParametersCommand.Execute
            Try
                Dim rules = _parameterRepository.GetRules()
                rules.MaximumCustomer = roomCapacity
                rules.ExtraCoefficient = extraCoefficient
                _unitOfWork.Commit()
                Return String.Empty
            Catch
                Return "Không thể chỉnh sửa qui định"
            End Try
        End Function

        Public Async Function ExecuteAsync(roomCapacity As Integer, extraCoefficient As Double) As Task(Of String) _
            Implements IUpdateParametersCommand.ExecuteAsync
            Try
                Dim rules = Await _parameterRepository.GetRulesAsync()
                rules.MaximumCustomer = roomCapacity
                rules.ExtraCoefficient = extraCoefficient
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không thể chỉnh sửa qui định"
            End Try
        End Function
    End Class
End NameSpace