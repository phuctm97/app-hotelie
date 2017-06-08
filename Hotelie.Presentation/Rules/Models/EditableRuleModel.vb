Imports Caliburn.Micro

Namespace Rules.Models
	Public Class EditableRuleModel
		Inherits PropertyChangedBase

		Private _roomCapacity As Integer
		Private _extraCoefficient As Double

		Public Property RoomCapacity As Integer
			Get
				Return _roomCapacity
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _roomCapacity ) Then Return
				If Value < 1 Then Value = 1
				_roomCapacity = value
				NotifyOfPropertyChange( Function() RoomCapacity )
			End Set
		End Property

		Public Property ExtraCoefficient As Double
			Get
				Return _extraCoefficient
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _extraCoefficient ) Then Return
				_extraCoefficient = Value
				NotifyOfPropertyChange( Function() ExtraCoefficient )
			End Set
		End Property

		Public ReadOnly Property RoomCategories As IObservableCollection(Of EditableRoomCategoryModel)

		Public ReadOnly Property CustomerCategories As IObservableCollection(Of EditableCustomerCategoryModel)

		Public Sub New()
			_roomCapacity = 0
			_extraCoefficient = 0
			RoomCategories = New BindableCollection(Of EditableRoomCategoryModel)
			CustomerCategories = New BindableCollection(Of EditableCustomerCategoryModel)
		End Sub
	End Class
End Namespace
