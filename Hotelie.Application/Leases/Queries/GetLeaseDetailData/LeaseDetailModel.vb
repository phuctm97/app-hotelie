Imports Hotelie.Domain.Leases

Namespace Leases.Queries.GetLeaseDetailData
	Public Class LeaseDetailModel
		Private ReadOnly _entity As LeaseDetail

		Sub New( entity As LeaseDetail )
			_entity = entity

			CustomerCategory = New GetCustomerCategoryData.CustomerCategoryModel( _entity.CustomerCategory )
		End Sub

		Public ReadOnly Property Id As String
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property CustomerName As String
			Get
				Return _entity.CustomerName
			End Get
		End Property

		Public ReadOnly Property CustomerCategory As GetCustomerCategoryData.CustomerCategoryModel

		Public ReadOnly Property LicenseId As String
			Get
				Return _entity.LicenseId
			End Get
		End Property

		Public ReadOnly Property Address As String
			Get
				Return _entity.Address
			End Get
		End Property
	End Class
End Namespace