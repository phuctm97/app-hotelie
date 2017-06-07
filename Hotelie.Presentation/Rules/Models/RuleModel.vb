Imports Caliburn.Micro

Namespace Rules.Models
	Public Class RuleModel
		Inherits PropertyChangedBase

		Private _roomCapacity As Integer
		Private _extraCoefficient As Integer

		Public Property RoomCapacity As Integer
			Get
				Return _roomCapacity
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _roomCapacity ) Then Return
				_roomCapacity = value
				NotifyOfPropertyChange( Function() RoomCapacity )
			End Set
		End Property

		Public Property ExtraCoefficient As Integer
			Get
				Return _extraCoefficient
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _extraCoefficient ) Then Return
				_extraCoefficient = Value
				NotifyOfPropertyChange( Function() ExtraCoefficient )
			End Set
		End Property

		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		Public ReadOnly Property CustomerCategories As IObservableCollection(Of CustomerCategoryModel)

		Public Sub New()
			_roomCapacity = 0
			_extraCoefficient = 0
			RoomCategories = New BindableCollection(Of RoomCategoryModel)
			CustomerCategories = New BindableCollection(Of CustomerCategoryModel)
		End Sub
	End Class
End Namespace
