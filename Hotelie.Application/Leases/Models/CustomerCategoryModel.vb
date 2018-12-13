Imports Hotelie.Domain.Leases

Namespace Leases.Models
	Public Class CustomerCategoryModel
		Implements ICustomerCategoryModel

		Private ReadOnly _entity As CustomerCategory

		Sub New( entity As CustomerCategory )
			_entity = entity
		End Sub

		Public ReadOnly Property Id As String Implements ICustomerCategoryModel.Id
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property Name As String Implements ICustomerCategoryModel.Name
			Get
				Return _entity.Name
			End Get
		End Property

		Public ReadOnly Property Coefficient As Double Implements ICustomerCategoryModel.Coefficient
			Get
				Return _entity.Coefficient
			End Get
		End Property
	End Class
End Namespace