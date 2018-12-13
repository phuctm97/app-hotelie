Imports Hotelie.Domain.Leases

Namespace Leases.Models
	Public Class LeaseDetailModel
		Implements ILeaseDetailModel
		Private ReadOnly _entity As LeaseDetail

		Sub New( entity As LeaseDetail )
			_entity = entity

			CustomerCategory = New CustomerCategoryModel( _entity.CustomerCategory )
		End Sub

		Public ReadOnly Property Id As String Implements ILeaseDetailModel.Id
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property CustomerName As String Implements ILeaseDetailModel.CustomerName
			Get
				Return _entity.CustomerName
			End Get
		End Property

		Public ReadOnly Property CustomerCategory As ICustomerCategoryModel Implements ILeaseDetailModel.CustomerCategory

		Public ReadOnly Property CustomerLicenseId As String Implements ILeaseDetailModel.CustomerLicenseId
			Get
				Return _entity.LicenseId
			End Get
		End Property

		Public ReadOnly Property CustomerAddress As String Implements ILeaseDetailModel.CustomerAddress
			Get
				Return _entity.Address
			End Get
		End Property
	End Class
End Namespace