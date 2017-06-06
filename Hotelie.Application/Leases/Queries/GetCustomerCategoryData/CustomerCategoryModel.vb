Imports Hotelie.Domain.Leases

Namespace Leases.Queries.GetCustomerCategoryData
	Public Class CustomerCategoryModel
		Private ReadOnly _entity As CustomerCategory

		Sub New( entity As CustomerCategory )
			_entity = entity
		End Sub

		Public ReadOnly Property Id As String
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property Name As String
			Get
				Return _entity.Name
			End Get
		End Property

		Public ReadOnly Property Coefficient As Double
			Get
				Return _entity.Coefficient
			End Get
		End Property
	End Class
End Namespace