Imports Caliburn.Micro

Namespace Bills.Models
	Public Class EditableBillModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _payerName As String
		Private _payerAddress As String
		Private _totalExpense As Decimal

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

		Public Property PayerName As String
			Get
				Return _payerName
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _payerName ) Then Return
				_payerName = value
				NotifyOfPropertyChange( Function() PayerName )
			End Set
		End Property

		Public Property PayerAddress As String
			Get
				Return _payerAddress
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _payerAddress ) Then Return
				_payerAddress = value
				NotifyOfPropertyChange( Function() PayerAddress )
			End Set
		End Property

		Public Property TotalExpense As Decimal
			Get
				Return _totalExpense
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _totalExpense ) Then Return
				_totalExpense = value
				NotifyOfPropertyChange( Function() TotalExpense )
			End Set
		End Property

		Public Sub New()
			Id = String.Empty
			PayerName = String.Empty
			PayerAddress = String.Empty
			TotalExpense = 0
		End Sub
	End Class
End Namespace
