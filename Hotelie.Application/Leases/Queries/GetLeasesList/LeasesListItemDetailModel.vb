Imports Caliburn.Micro

Namespace Leases.Queries.GetLeasesList
	Public Class LeasesListItemDetailModel
		Inherits PropertyChangedBase

		Private _customerName As String
		Private _id As String

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Public Property CustomerName As String
			Get
				Return _customerName
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _customerName ) Then Return
				_customerName = value
				NotifyOfPropertyChange( Function() CustomerName )
			End Set
		End Property

		Public Sub New()
			Id = String.Empty
			CustomerName = String.Empty
		End Sub
	End Class
End Namespace
