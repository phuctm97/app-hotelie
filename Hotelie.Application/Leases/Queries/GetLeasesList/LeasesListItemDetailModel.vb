Imports Caliburn.Micro

Namespace Leases.Queries.GetLeasesList
	Public Class LeasesListItemDetailModel
		Inherits PropertyChangedBase

		Private _customerName As String

		Public Property CustomerName As String
			Get
				Return _customerName
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _customerName ) Then Return
				_customerName = Value
				NotifyOfPropertyChange( Function() CustomerName )
			End Set
		End Property

		Public Sub New()
			CustomerName = String.Empty
		End Sub
	End Class
End NameSpace