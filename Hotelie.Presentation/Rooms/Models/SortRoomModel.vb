Imports Caliburn.Micro

Namespace Rooms.Models
	Public Class SortRoomModel
		Inherits PropertyChangedBase

		Private _sortingCode As Integer
		Private _isDescendingSort As Boolean

		Public Property SortingCode As Integer
			Get
				Return _sortingCode
			End Get
			Set
				If Equals( Value, _sortingCode ) Then Return
				_sortingCode = value
				NotifyOfPropertyChange( Function() SortingCode )
			End Set
		End Property

		Public Property IsDescendingSort As Boolean
			Get
				Return _isDescendingSort
			End Get
			Set
				If Equals( Value, _isDescendingSort ) Then Return
				_isDescendingSort = value
				NotifyOfPropertyChange( Function() IsDescendingSort )
			End Set
		End Property

		Public Sub New()
			Reset()
		End Sub

		Public Sub Reset()
			_sortingCode = 0
			NotifyOfPropertyChange( Function() SortingCode )

			_isDescendingSort = False
			NotifyOfPropertyChange( Function() IsDescendingSort )
		End Sub

		End Class
End Namespace