Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Models

Namespace Leases.Models
	Public Class EditableLeaseDetailModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _customerName As String
		Private _customerCategory As ICustomerCategoryModel
		Private _customerLicenseId As String
		Private _customerAddress As String

		Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Property CustomerName As String
			Get
				Return _customerName
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _customerName ) Then Return
				_customerName = value
				NotifyOfPropertyChange( Function() CustomerName )
			End Set
		End Property

		Property CustomerCategory As ICustomerCategoryModel
			Get
				Return _customerCategory
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _customerCategory ) Then Return
				_customerCategory = value
				NotifyOfPropertyChange( Function() CustomerCategory )
			End Set
		End Property

		Property CustomerLicenseId As String
			Get
				Return _customerLicenseId
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _customerLicenseId ) Then Return
				_customerLicenseId = value
				NotifyOfPropertyChange( Function() CustomerLicenseId )
			End Set
		End Property

		Property CustomerAddress As String
			Get
				Return _customerAddress
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _customerAddress ) Then Return
				_customerAddress = value
				NotifyOfPropertyChange( Function() CustomerAddress )
			End Set
		End Property

		Public Sub New()
			_id = String.Empty
			_customerName = String.Empty
			_customerCategory = Nothing
			_customerLicenseId = String.Empty
			_customerAddress = String.Empty
		End Sub
	End Class
End Namespace