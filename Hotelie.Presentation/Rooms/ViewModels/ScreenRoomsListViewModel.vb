Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomData
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Infrastructure

Namespace Rooms.ViewModels
	Public Class ScreenRoomsListViewModel
		Inherits PropertyChangedBase
		Implements IRoomsListPresenter,
		           INeedWindowModals,
		           IChild(Of RoomsWorkspaceViewModel)

		Private _rooms As IObservableCollection(Of RoomsListItemModel)
		Private _filterRoomNamePrefix As String
		Private _filterRoomCategory As RoomCategoriesListItemModel
		Private _filterRoomState As Integer?
		Private _filterRoomMinPrice As Decimal?
		Private _filterRoomMaxPrice As Decimal?
		Private _sortingCode As Integer
		Private _isDescendingSort As Boolean

		' Dependencies
		Private ReadOnly _getRoomListsQuery As IGetRoomsListQuery
		Private ReadOnly _getRoomCategoriesListQuery As IGetRoomCategoriesListQuery
		Private ReadOnly _removeRoomCommand As IRemoveRoomCommand
		Private ReadOnly _inventory As IInventory

		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent
			Get
				Return CType(Parent, RoomsWorkspaceViewModel)
			End Get
			Set
				Parent = value
			End Set
		End Property

		' Data
		Public Property Rooms As IObservableCollection(Of RoomsListItemModel)
			Get
				Return _rooms
			End Get
			Set
				If Equals( Value, _rooms ) Then Return
				_rooms = value
				NotifyOfPropertyChange( Function() Rooms )
			End Set
		End Property

		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoriesListItemModel)

		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomStates As IObservableCollection(Of Integer)

		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomMinPrices As IObservableCollection(Of Decimal)

		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomMaxPrices As IObservableCollection(Of Decimal)

		' Initialization
		Public Sub New( workspace As RoomsWorkspaceViewModel,
										getRoomListsQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery,
		                removeRoomCommand As IRemoveRoomCommand,
		                inventory As IInventory )
			ParentWorkspace = workspace
			_getRoomListsQuery = getRoomListsQuery
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery
			_removeRoomCommand = removeRoomCommand
			_inventory = inventory
			RegisterInventory()

			Rooms = New BindableCollection(Of RoomsListItemModel)
			RoomCategories = New BindableCollection(Of RoomCategoriesListItemModel)
			RoomStates = New BindableCollection(Of Integer)
			RoomMinPrices = New BindableCollection(Of Decimal)
			RoomMaxPrices = New BindableCollection(Of Decimal)
		End Sub

		Public Sub Init()
			InitRooms()

			InitRoomCategories()

			InitRoomStates()

			InitRoomPrices()

			InitFilteringValues()

			InitSortingValues()

			RefreshRoomsListVisibility()
		End Sub

		Public Async Function InitAsync() As Task
			Await InitRoomsAsync()

			Await InitRoomCategoriesAsync()

			InitRoomStates()

			InitRoomPrices()

			InitFilteringValues()

			InitSortingValues()

			RefreshRoomsListVisibility()
		End Function

		Private Sub InitRooms()
			Rooms.Clear()
			Rooms.AddRange( _getRoomListsQuery.Execute() )
		End Sub

		Private Async Function InitRoomsAsync() As Task
			Rooms.Clear()
			Rooms.AddRange( Await _getRoomListsQuery.ExecuteAsync() )
		End Function

		Private Sub InitRoomCategories()
			RoomCategories.Clear()
			RoomCategories.AddRange( _getRoomCategoriesListQuery.Execute() )
			RoomCategories.Add( New RoomCategoriesListItemModel With {.Name = "Tất cả", .UnitPrice = - 1} ) 'filter all
		End Sub

		Private Async Function InitRoomCategoriesAsync() As Task
			RoomCategories.Clear()
			RoomCategories.AddRange( Await _getRoomCategoriesListQuery.ExecuteAsync() )
			RoomCategories.Add( New RoomCategoriesListItemModel With {.Name = "Tất cả", .UnitPrice = - 1} ) 'filter all
		End Function

		Private Sub InitRoomPrices()
			Dim minPrices = New List(Of Decimal)
			Dim maxPrices = New List(Of Decimal)

			For i = 0 To RoomCategories.Count - 2
				Dim price = RoomCategories( i ).UnitPrice

				If Not minPrices.Contains( price ) Then _
					minPrices.Add( price )

				If Not maxPrices.Contains( price ) Then _
					maxPrices.Add( price )
			Next

			minPrices.Sort( Function( a,
				              b ) a < b )
			minPrices.Add( - 1 )

			maxPrices.Sort( Function( a,
				              b ) a > b )
			maxPrices.Add( - 1 )

			RoomMinPrices.Clear()
			RoomMinPrices.AddRange( minPrices )

			RoomMaxPrices.Clear()
			RoomMaxPrices.AddRange( maxPrices )
		End Sub

		Private Sub InitRoomStates()
			RoomStates.Clear()
			RoomStates.AddRange( {0, 1} )
			RoomStates.Add( 2 ) 'filter all
		End Sub

		Private Sub InitFilteringValues()
			_filterRoomNamePrefix = String.Empty
			_filterRoomCategory = Nothing
			_filterRoomState = Nothing
			_filterRoomMinPrice = Nothing
			_filterRoomMaxPrice = Nothing
		End Sub

		Private Sub InitSortingValues()
			SortingCode = - 1
			IsDescendingSort = False
		End Sub

		' Filter values
		Public Property FilterRoomNamePrefix As String
			Get
				Return _filterRoomNamePrefix
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _filterRoomNamePrefix ) Then Return
				_filterRoomNamePrefix = value
				NotifyOfPropertyChange( Function() FilterRoomNamePrefix )
				RefreshRoomsListVisibility()
			End Set
		End Property

		Public Property FilterRoomCategory As RoomCategoriesListItemModel
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

		Public Property FilterRoomMinPrice As Decimal?
			Get
				Return _filterRoomMinPrice
			End Get
			Set
				If Equals( Value, _filterRoomMinPrice ) Then Return
				_filterRoomMinPrice = value
				NotifyOfPropertyChange( Function() FilterRoomMinPrice )

				If FilterRoomMinPrice IsNot Nothing And
				   FilterRoomMaxPrice IsNot Nothing And
				   FilterRoomMinPrice >= 0 And
				   FilterRoomMaxPrice >= 0 And
				   FilterRoomMinPrice > FilterRoomMaxPrice
					FilterRoomMaxPrice = FilterRoomMinPrice
				Else
					RefreshRoomsListVisibility()
				End If
			End Set
		End Property

		Public Property FilterRoomMaxPrice As Decimal?
			Get
				Return _filterRoomMaxPrice
			End Get
			Set
				If Equals( Value, _filterRoomMaxPrice ) Then Return
				_filterRoomMaxPrice = value
				NotifyOfPropertyChange( Function() FilterRoomMaxPrice )

				If FilterRoomMaxPrice IsNot Nothing And
				   FilterRoomMinPrice IsNot Nothing And
				   FilterRoomMaxPrice >= 0 And
				   FilterRoomMinPrice >= 0 And
				   FilterRoomMaxPrice < FilterRoomMinPrice
					FilterRoomMinPrice = FilterRoomMaxPrice
				Else
					RefreshRoomsListVisibility()
				End If
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
			_filterRoomNamePrefix = String.Empty
			NotifyOfPropertyChange( Function() FilterRoomNamePrefix )

			_filterRoomCategory = Nothing
			NotifyOfPropertyChange( Function() FilterRoomCategory )

			_filterRoomState = Nothing
			NotifyOfPropertyChange( Function() FilterRoomState )

			_filterRoomMinPrice = Nothing
			NotifyOfPropertyChange( Function() FilterRoomMinPrice )

			_filterRoomMaxPrice = Nothing
			NotifyOfPropertyChange( Function() FilterRoomMaxPrice )

			RefreshRoomsListVisibility()
		End Sub

		Public Sub FilterByRoomCategoryOf( roomsListItem As RoomsListItemModel )
			Dim category = RoomCategories.FirstOrDefault( Function( c ) String.Equals( c.Id, roomsListItem.CategoryId ) )
			If IsNothing( category ) Then Return

			FilterRoomCategory = category
		End Sub

		Public Sub FilterByRoomStateOf( roomsListItem As RoomsListItemModel )
			Dim state = RoomStates.FirstOrDefault( Function( s ) Equals( s, roomsListItem.State ) )
			If IsNothing( state ) Then Return

			FilterRoomState = state
		End Sub

		Public Sub RefreshRoomsListVisibility()
			Dim matchNamePrefix As Boolean
			Dim matchCategory As Boolean
			Dim matchState As Boolean
			Dim matchMinPrice As Boolean
			Dim matchMaxPrice As Boolean

			For Each room As RoomsListItemModel In Rooms
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

				matchMinPrice = IsNothing( FilterRoomMinPrice ) OrElse
				                (FilterRoomMinPrice < 0 OrElse
				                 (FilterRoomMinPrice <= room.UnitPrice))

				matchMaxPrice = IsNothing( FilterRoomMaxPrice ) OrElse
				                (FilterRoomMaxPrice < 0 OrElse
				                 (FilterRoomMaxPrice >= room.UnitPrice))

				If matchNamePrefix And
				   matchCategory And
				   matchState And
				   matchMinPrice And
				   matchMaxPrice
					room.IsFiltersMatched = True
				Else
					room.IsFiltersMatched = False
				End If
			Next
		End Sub

		' Sort
		Public Sub SortRoomsList()
			Select Case SortingCode
				Case 0
					If IsDescendingSort
						Rooms = New BindableCollection(Of RoomsListItemModel)( Rooms.OrderByDescending( Function( p ) p.Name ) )
					Else
						Rooms = New BindableCollection(Of RoomsListItemModel)( Rooms.OrderBy( Function( p ) p.Name ) )
					End If
				Case 1
					If IsDescendingSort
						Rooms = New BindableCollection(Of RoomsListItemModel)( Rooms.OrderByDescending( Function( p ) p.CategoryName ) )
					Else
						Rooms = New BindableCollection(Of RoomsListItemModel)( Rooms.OrderBy( Function( p ) p.CategoryName ) )
					End If
				Case 2
					If IsDescendingSort
						Rooms = New BindableCollection(Of RoomsListItemModel)( Rooms.OrderByDescending( Function( p ) p.UnitPrice ) )
					Else
						Rooms = New BindableCollection(Of RoomsListItemModel)( Rooms.OrderBy( Function( p ) p.UnitPrice ) )
					End If
				Case 3
					If IsDescendingSort
						Rooms = New BindableCollection(Of RoomsListItemModel)( Rooms.OrderByDescending( Function( p ) p.State ) )
					Else
						Rooms = New BindableCollection(Of RoomsListItemModel)( Rooms.OrderBy( Function( p ) p.State ) )
					End If
			End Select
		End Sub

		Public Sub PreviewHireAsync( roomModel As RoomsListItemModel )
		End Sub

		' Infrastructure
		Public Sub OnRoomAdded( model As RoomModel ) Implements IRoomsListPresenter.OnRoomAdded
			' find category
			Dim category = RoomCategories.FirstOrDefault( Function( c ) c.Id = model.Category.Id )
			If category Is Nothing Then Throw New EntryPointNotFoundException()

			Rooms.Add( New RoomsListItemModel With {.Id=model.Id, 
				         .Name=model.Name, 
				         .CategoryId=category.Id,
				         .CategoryName=category.Name, 
				         .CategoryDisplayColor=category.DisplayColor, 
				         .UnitPrice=category.UnitPrice, 
				         .State=0, 
				         .IsFiltersMatched=False} )

			RefreshRoomsListVisibility()
			SortRoomsList()
		End Sub

		Public Sub OnRoomUpdated( model As RoomModel ) Implements IRoomsListPresenter.OnRoomUpdated
			' find room
			Dim room = Rooms.FirstOrDefault( Function( r ) r.Id = model.Id )
			If room Is Nothing Then Throw New DuplicateWaitObjectException()

			' find category
			Dim category = RoomCategories.FirstOrDefault( Function( c ) c.Id = model.Category.Id )
			If category Is Nothing Then Throw New EntryPointNotFoundException()

			' update
			room.Name = model.Name
			room.CategoryId = category.id
			room.CategoryName = category.Name
			room.CategoryDisplayColor = category.DisplayColor
			room.UnitPrice = category.UnitPrice
			room.State = model.State

			RefreshRoomsListVisibility()
			SortRoomsList()
		End Sub

		Public Sub OnRoomRemoved( id As String ) Implements IRoomsListPresenter.OnRoomRemoved
			' find room
			Dim room = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If room Is Nothing Then Return

			Rooms.Remove( room )
		End Sub
	End Class
End Namespace
