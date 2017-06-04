Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Infrastructure

Namespace Rooms.ViewModels
	Public Class ScreenRoomsListViewModel
		Inherits PropertyChangedBase
		Implements IRoomCollectionPresenter,
		           INeedWindowModals

		Private _rooms As IObservableCollection(Of RoomModel)
		Private _filterRoomNamePrefix As String
		Private _filterRoomCategory As RoomCategoryModel
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

		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomStates As IObservableCollection(Of Integer)

		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomMinPrices As IObservableCollection(Of Decimal)

		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomMaxPrices As IObservableCollection(Of Decimal)

		' Initialization
		Public Sub New( getRoomListsQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery,
		                removeRoomCommand As IRemoveRoomCommand,
		                inventory As IInventory )
			_getRoomListsQuery = getRoomListsQuery
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery
			_removeRoomCommand = removeRoomCommand
			_inventory = inventory
			RegisterInventory()

			Rooms = New BindableCollection(Of RoomModel)
			RoomCategories = New BindableCollection(Of RoomCategoryModel)
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
			RoomCategories.Add( New RoomCategoryModel With {.Name = "Tất cả", .Price = - 1} ) 'filter all
		End Sub

		Private Async Function InitRoomCategoriesAsync() As Task
			RoomCategories.Clear()
			RoomCategories.AddRange( Await _getRoomCategoriesListQuery.ExecuteAsync() )
			RoomCategories.Add( New RoomCategoryModel With {.Name = "Tất cả", .Price = - 1} ) 'filter all
		End Function

		Private Sub InitRoomPrices()
			Dim minPrices = New List(Of Decimal)
			Dim maxPrices = New List(Of Decimal)

			For i = 0 To RoomCategories.Count - 2
				Dim price = RoomCategories( i ).Price

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

		Public Sub FilterByRoomCategoryOf( room As RoomModel )
			Dim category = RoomCategories.FirstOrDefault( Function( c ) String.Equals( c.Id, room.CategoryId ) )
			If IsNothing( category ) Then Return

			FilterRoomCategory = category
		End Sub

		Public Sub FilterByRoomStateOf( room As RoomModel )
			Dim state = RoomStates.FirstOrDefault( Function( s ) Equals( s, room.State ) )
			If IsNothing( state ) Then Return

			FilterRoomState = state
		End Sub

		Public Sub RefreshRoomsListVisibility()
			Dim matchNamePrefix As Boolean
			Dim matchCategory As Boolean
			Dim matchState As Boolean
			Dim matchMinPrice As Boolean
			Dim matchMaxPrice As Boolean

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

				matchMinPrice = IsNothing( FilterRoomMinPrice ) OrElse
				                (FilterRoomMinPrice < 0 OrElse
				                 (FilterRoomMinPrice <= room.Price))

				matchMaxPrice = IsNothing( FilterRoomMaxPrice ) OrElse
				                (FilterRoomMaxPrice < 0 OrElse
				                 (FilterRoomMaxPrice >= room.Price))

				If matchNamePrefix And
				   matchCategory And
				   matchState And
				   matchMinPrice And
				   matchMaxPrice
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

		' Delete room
		Public Async Sub PreviewDelete( roomModel As RoomModel )
			Dim result = Await ConfirmDelete( roomModel.Name )

			If Equals( result, 0 )
				DeleteAsync( roomModel.Id )
			End If
		End Sub

		Private Async Function ConfirmDelete( roomName As String ) As Task(Of Integer)
			' show dialog
			Dim dialog = New TwoButtonDialog( $"Xóa phòng {roomName}. Tiếp tục?",
			                                  "XÓA",
			                                  "HỦY",
			                                  True,
			                                  False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "XÓA" ) Then Return 0
			Return 1
		End Function

		Private Sub Delete( roomId As String )
			' try update
			_removeRoomCommand.Execute( roomId )
			_inventory.OnRoomRemoved( roomId )
		End Sub

		Private Async Sub DeleteAsync( roomId As String )
			' try update
			ShowStaticWindowLoadingDialog()
			Await _removeRoomCommand.ExecuteAsync( roomId )
			_inventory.OnRoomRemoved( roomId )
			CloseStaticWindowDialog()
		End Sub

		' Infrastructure
		Public Sub OnRoomAdded( id As String,
		                        name As String,
		                        categoryId As String,
		                        note As String ) Implements IRoomCollectionPresenter.OnRoomAdded
			' check for duplicate
			Dim rc = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If rc IsNot Nothing Then Throw New DuplicateWaitObjectException()

			' find category
			Dim category = RoomCategories.FirstOrDefault( Function( c ) c.Id = categoryId )
			If categoryId Is Nothing Then Throw New EntryPointNotFoundException()

			Rooms.Add( New RoomModel With {.Id=id, .Name=name, 
				         .CategoryId=categoryId, .CategoryName=category.Name, 
				         .CategoryDisplayColor=category.DisplayColor, 
				         .Price=category.Price, .State=0,.Note=note, .IsVisible=False} )

			RefreshRoomsListVisibility()
			SortRoomsList()
		End Sub

		Public Sub OnRoomUpdated( id As String,
		                          name As String,
		                          categoryId As String,
		                          note As String,
		                          state As Int32 ) Implements IRoomCollectionPresenter.OnRoomUpdated
			' find room
			Dim room = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If room Is Nothing Then Throw New DuplicateWaitObjectException()

			' find category
			Dim category = RoomCategories.FirstOrDefault( Function( c ) c.Id = categoryId )
			If categoryId Is Nothing Then Throw New EntryPointNotFoundException()

			' update
			room.Name = name
			room.CategoryId = categoryId
			room.CategoryName = category.Name
			room.CategoryDisplayColor = category.DisplayColor
			room.Price = category.Price
			room.Note = note
			room.State = state

			RefreshRoomsListVisibility()
			SortRoomsList()
		End Sub

		Public Sub OnRoomRemoved( id As String ) Implements IRoomCollectionPresenter.OnRoomRemoved
			' find room
			Dim room = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If room Is Nothing Then Throw New DuplicateWaitObjectException()

			Rooms.Remove( room )
		End Sub
	End Class
End Namespace
