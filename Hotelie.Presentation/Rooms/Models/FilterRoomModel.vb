Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Models
	Public Class FilterRoomModel
		Inherits PropertyChangedBase

		Private _namePrefix As String
		Private _category As RoomCategoryModel
		Private _state As Integer?
		Private _minUnitPrice As Decimal?
		Private _maxUnitPrice As Decimal?

		Public Property NamePrefix As String
			Get
				Return _namePrefix
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _namePrefix ) Then Return
				_namePrefix = value
				NotifyOfPropertyChange( Function() NamePrefix )
			End Set
		End Property

		Public Property Category As RoomCategoryModel
			Get
				Return _category
			End Get
			Set
				If Equals( Value, _category ) Then Return
				_category = value
				NotifyOfPropertyChange( Function() Category )
			End Set
		End Property

		Public Property State As Integer?
			Get
				Return _state
			End Get
			Set
				If Equals( Value, _state ) Then Return
				_state = value
				NotifyOfPropertyChange( Function() State )
			End Set
		End Property

		Public Property MinUnitPrice As Decimal?
			Get
				Return _minUnitPrice
			End Get
			Set
				If Equals( Value, _minUnitPrice ) Then Return
				_minUnitPrice = value
				NotifyOfPropertyChange( Function() MinUnitPrice )

				If _minUnitPrice IsNot Nothing And
				   _maxUnitPrice IsNot Nothing And
				   _minUnitPrice >= 0 And
				   _maxUnitPrice >= 0 And
				   _minUnitPrice > _maxUnitPrice
					_maxUnitPrice = _minUnitPrice
					NotifyOfPropertyChange( Function() MaxUnitPrice )
				End If
			End Set
		End Property

		Public Property MaxUnitPrice As Decimal?
			Get
				Return _maxUnitPrice
			End Get
			Set
				If Equals( Value, _maxUnitPrice ) Then Return
				_maxUnitPrice = value
				NotifyOfPropertyChange( Function() MaxUnitPrice )

				If _maxUnitPrice IsNot Nothing And
				   _minUnitPrice IsNot Nothing And
				   _maxUnitPrice >= 0 And
				   _minUnitPrice >= 0 And
				   _maxUnitPrice < _minUnitPrice
					_minUnitPrice = _maxUnitPrice
					NotifyOfPropertyChange( Function() MinUnitPrice )
				End If
			End Set
		End Property

		Public Sub New()
			Reset()
		End Sub

		Public Sub Reset()
			_namePrefix = String.Empty
			NotifyOfPropertyChange( Function() NamePrefix )

			_category = Nothing
			NotifyOfPropertyChange( Function() Category )

			_state = Nothing
			NotifyOfPropertyChange( Function() State )

			_minUnitPrice = Nothing
			NotifyOfPropertyChange( Function() MinUnitPrice )

			_maxUnitPrice = Nothing
			NotifyOfPropertyChange( Function() MaxUnitPrice )
		End Sub

		Public Function IsMatch( roomModel As RoomModel,
		                         allCategories As List(Of RoomCategoryModel),
		                         allStates As List(Of Integer) ) As Boolean
			Dim matchNamePrefix As Boolean = String.IsNullOrWhiteSpace( NamePrefix ) OrElse
			                                 (roomModel.Name.ToLower().Contains( NamePrefix ))

			Dim matchCategory As Boolean = IsNothing( Category ) OrElse
			                               (String.IsNullOrWhiteSpace( Category.Id ) OrElse
			                                (Equals( allCategories.Count, 0 ) OrElse
			                                 (Category.UnitPrice < 0 OrElse
			                                  (String.Equals( roomModel.Category.Id, Category.Id )))))

			Dim matchState As Boolean = IsNothing( State ) OrElse
			                            (Equals( allStates.Count, 0 ) OrElse
			                             (Equals( State, 2 ) OrElse
			                              (Equals( roomModel.State, State ))))

			Dim matchMinPrice As Boolean = IsNothing( MinUnitPrice ) OrElse
			                               (MinUnitPrice < 0 OrElse
			                                (MinUnitPrice <= roomModel.Category.UnitPrice))

			Dim matchMaxPrice As Boolean = IsNothing( MaxUnitPrice ) OrElse
			                               (MaxUnitPrice < 0 OrElse
			                                (MaxUnitPrice >= roomModel.Category.UnitPrice))

			If matchNamePrefix And
			   matchCategory And
			   matchState And
			   matchMinPrice And
			   matchMaxPrice
				Return True
			Else
				Return False
			End If
		End Function
	End Class
End Namespace
