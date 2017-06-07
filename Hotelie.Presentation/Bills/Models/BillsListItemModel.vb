Imports Caliburn.Micro

Namespace Bills.Models
	Public Class BillsListItemModel
		Inherits PropertyChangedBase

		Private _id As String

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing(Value) OrElse Equals(Value, _id) Then Return
				_id = value
				NotifyOfPropertyChange(Function() Id)
			End Set
		End Property

	End Class
End Namespace