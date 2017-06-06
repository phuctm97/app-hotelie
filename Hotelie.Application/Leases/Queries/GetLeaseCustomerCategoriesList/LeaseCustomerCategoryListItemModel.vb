﻿Imports Caliburn.Micro

Namespace Leases.Queries.GetLeaseCustomerCategoriesList
	Public Class LeaseCustomerCategoryListItemModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _name As String

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing(Value) OrElse String.Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Public Property Name As String
			Get
				Return _name
			End Get
			Set
				If IsNothing(Value) OrElse String.Equals( Value, _name ) Then Return
				_name = value
				NotifyOfPropertyChange( Function() Name )
			End Set
		End Property

		Public Sub New()
			Id = String.Empty
			Name = String.Empty
		End Sub
	End Class
End Namespace
