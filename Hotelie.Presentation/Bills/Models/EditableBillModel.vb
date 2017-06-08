Imports Caliburn.Micro

Namespace Bills.Models
	Public Class EditableBillModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _customerName As String
		Private _customerAddress As String
		Private _totalExpense As Decimal
		Private _createDate As Date

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, Id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Public Property CustomerName As String
			Get
				Return _customerName
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _customerName ) Then Return
				_customerName = value
				NotifyOfPropertyChange( Function() CustomerName )
			End Set
		End Property

		Public Property CustomerAddress As String
			Get
				Return _customerAddress
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _customerAddress ) Then Return
				_customerAddress = value
				NotifyOfPropertyChange( Function() CustomerAddress )
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

		Public Property CreateDate As Date
			Get
				Return _createDate
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _createDate ) Then Return
				_createDate = value
				NotifyOfPropertyChange(Function() CreateDate)
			End Set
		End Property

		Public ReadOnly Property Details As IObservableCollection(Of EditableBillDetailModel)

		Public Sub New()
			Id = String.Empty
			CustomerName = String.Empty
			CustomerAddress = String.Empty
			CreateDate = Today
			TotalExpense = 0
			Details = New BindableCollection(Of EditableBillDetailModel)
		End Sub
	End Class
End Namespace