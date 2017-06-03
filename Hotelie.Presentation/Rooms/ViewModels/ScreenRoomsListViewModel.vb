Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Rooms.ViewModels
	Public Class ScreenRoomsListViewModel
		Inherits Screen

		Private _rooms As IObservableCollection(Of RoomModel)
		Private _filterRoomNamePrefix As String
		Private _filterRoomCategory As RoomCategoryModel
		Private _filterRoomState As Integer?
		Private _sortingCode As Integer
		Private _isDescendingSort As Boolean

		' Dependencies

		Private ReadOnly _getRoomListsQuery As IGetRoomsListQuery
		Private ReadOnly _getRoomCategoriesListQuery As IGetRoomCategoriesListQuery

		' Data

		Public Property Rooms As IObservableCollection(Of RoomModel)
			Get
				Return _rooms
			End Get
			Set
				If Equals( Value, _rooms ) Then Return
				_rooms = value
				NotifyOfPropertyChange( Function() Rooms )
			End Set
		End Property

		' ReSharper disable once CollectionNeverUpdated.Global
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		' ReSharper disable once CollectionNeverUpdated.Global
		Public ReadOnly Property RoomStates As IObservableCollection(Of Integer)

		' Initialization

		Public Sub New( getRoomListsQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery )
			_getRoomListsQuery = getRoomListsQuery
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery

			Rooms = New BindableCollection(Of RoomModel)()
			RoomCategories = New BindableCollection(Of RoomCategoryModel)()
			RoomStates = New BindableCollection(Of Integer)()

			FilterRoomNamePrefix = String.Empty
			FilterRoomCategory = Nothing
			FilterRoomState = 2

			SortingCode = - 1
			IsDescendingSort = False
		End Sub

		Protected Overrides Sub OnViewLoaded( view As Object )
			MyBase.OnViewLoaded( view )

			Rooms.Clear()
			Rooms.AddRange( _getRoomListsQuery.Execute() )

			RoomCategories.Clear()
			RoomCategories.AddRange( _getRoomCategoriesListQuery.Execute() )
			RoomCategories.Add( New RoomCategoryModel With {.Name="Tất cả"} ) 'filter all

			RoomStates.Clear()
			RoomStates.AddRange( {0, 1} )
			RoomStates.Add( 2 ) 'filter all

			RefreshRoomsListVisibility()
		End Sub

		' Filter values

		Public Property FilterRoomNamePrefix As String
			Get
				Return _filterRoomNamePrefix
			End Get
			Set
				If String.Equals( Value, _filterRoomNamePrefix ) Then Return
				_filterRoomNamePrefix = value
				NotifyOfPropertyChange( Function() FilterRoomNamePrefix )
				RefreshRoomsListVisibility()
			End Set
		End Property

		Public Property FilterRoomCategory As RoomCategoryModel
			Get
				Return _filterRoomCategory
			End Get
			Set
				If Equals( Value, _filterRoomCategory ) Then Return
				_filterRoomCategory = value
				NotifyOfPropertyChange( Function() FilterRoomCategory )
				RefreshRoomsListVisibility()
			End Set
		End Property

		Public Property FilterRoomState As Integer?
			Get
				Return _filterRoomState
			End Get
			Set
				If Equals( Value, _filterRoomState ) Then Return
				_filterRoomState = value
				NotifyOfPropertyChange( Function() FilterRoomState )
				RefreshRoomsListVisibility()
			End Set
		End Property

		' Sort values

		Public Property SortingCode As Integer
			Get
				Return _sortingCode
			End Get
			Set
				If Equals( Value, _sortingCode ) Then Return
				_sortingCode = value
				NotifyOfPropertyChange( Function() SortingCode )
				SortRoomsList()
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
				SortRoomsList()
			End Set
		End Property

		' Filter 

		Public Sub ResetFilters()
			FilterRoomNamePrefix = String.Empty
			FilterRoomCategory = Nothing
			FilterRoomState = Nothing
		End Sub

		Public Sub FilterByRoomCategoryOf( room As RoomModel )
			Dim category = RoomCategories.FirstOrDefault(Function(c) String.Equals(c.Id, room.CategoryId))
			If IsNothing(category) Then Return

			FilterRoomCategory = category
		End Sub

		Public Sub FilterByRoomStateOf(room As RoomModel)
			Dim state = RoomStates.FirstOrDefault(Function(s) Equals(s, room.State))
			If IsNothing(state) Then Return

			FilterRoomState = state
		End Sub

		Public Sub RefreshRoomsListVisibility()
			Dim matchNamePrefix As Boolean
			Dim matchCategory As Boolean
			Dim matchState As Boolean

			For Each room As RoomModel In Rooms
				matchNamePrefix = String.IsNullOrWhiteSpace( FilterRoomNamePrefix ) OrElse
				                  (room.Name.ToLower().Contains( FilterRoomNamePrefix ))

				matchCategory = IsNothing( FilterRoomCategory ) OrElse
				                (String.IsNullOrWhiteSpace( FilterRoomCategory.Id ) OrElse
				                 (Equals( RoomCategories.Count, 0 ) OrElse
				                  (Equals( FilterRoomCategory, RoomCategories.Last() ) OrElse
				                   (String.Equals( room.CategoryId, FilterRoomCategory.Id )))))

				matchState = IsNothing( FilterRoomState ) OrElse
				             (Equals( RoomStates.Count, 0 ) OrElse
				              (Equals( FilterRoomState, RoomStates.Last() ) OrElse
				               (Equals( room.State, FilterRoomState ))))

				If matchNamePrefix And matchCategory And matchState
					room.IsVisible = True
				Else
					room.IsVisible = False
				End If
			Next
		End Sub

		' Sort

		Public Sub SortRoomsList()
			Select Case SortingCode
				Case 0
					If IsDescendingSort
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.Name ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.Name ) )
					End If
				Case 1
					If IsDescendingSort
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.CategoryName ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.CategoryName ) )
					End If
				Case 2
					If IsDescendingSort
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.Price ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.Price ) )
					End If
				Case 3
					If IsDescendingSort
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.State ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.State ) )
					End If
			End Select
		End Sub
	End Class
End Namespace
