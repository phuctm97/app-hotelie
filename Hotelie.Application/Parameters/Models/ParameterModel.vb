Imports Hotelie.Domain.Parameters

Namespace Parameters.Models
	Public Class ParameterModel
		Implements IParameterModel

		Private ReadOnly _entity As Parameter

		Sub New( entity As Parameter )
			_entity = entity
		End Sub

		Public ReadOnly Property ExtraCoefficient As Double Implements IParameterModel.ExtraCoefficient
			Get
				Return _entity.ExtraCoefficient
			End Get
		End Property

		Public ReadOnly Property RoomCapacity As Int32 Implements IParameterModel.RoomCapacity
			Get
				Return _entity.MaximumCustomer
			End Get
		End Property
	End Class
End Namespace