Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Presentation.Common

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Conductor(Of IScreen)
		Implements IWorkspace

		' Dialog
		Private _isTopDrawerOpen As Boolean

		' Dependencies
		Private ReadOnly _getRoomsListQuery As IGetRoomsListQuery
		Private ReadOnly _getRoomCategoriesListQuery As IGetRoomCategoriesListQuery

		' Data
		Private _rooms As IObservableCollection(Of RoomModel)

		' Filter
		Private _filterRoomName As String
		Private _filterRoomCategory As RoomCategoryModel
		Private _filterRoomState As Integer
		Private _isSortDescending As Boolean
		Private _sortFieldCode As Integer

		Public Sub New( getRoomsListQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery )
			' Dependencies
			_getRoomsListQuery = getRoomsListQuery
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery

			' Display
			DisplayName = "Danh sách phòng"
			IsTopDrawerOpen = False

			' Data
			Rooms = New BindableCollection(Of RoomModel)
			RoomCategories = New BindableCollection(Of RoomCategoryModel)
			RoomStates = New BindableCollection(Of Integer)

			' Filter
			FilterRoomName = String.Empty
			FilterRoomCategory = Nothing
			FilterRoomState = - 1
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			Rooms.AddRange( _getRoomsListQuery.Execute() )
			RoomCategories.AddRange( _getRoomCategoriesListQuery.Execute() )
			RoomStates.AddRange( {0, 1, 2} )
		End Sub

		Protected Overrides Sub OnViewReady( view As Object )
			MyBase.OnViewReady( view )

			RefreshRoomsList()
		End Sub

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

		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		Public ReadOnly Property RoomStates As IObservableCollection(Of Integer)

		' Filter

		Public Property FilterRoomName As String
			Get
				Return _filterRoomName
			End Get
			Set
				If String.Equals( Value, _filterRoomName ) Then Return
				_filterRoomName = value

				NotifyOfPropertyChange( Function() FilterRoomName )
				RefreshRoomsList()
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
				RefreshRoomsList()
			End Set
		End Property

		Public Property FilterRoomState As Integer
			Get
				Return _filterRoomState
			End Get
			Set
				If Equals( Value, _filterRoomState ) Then Return
				_filterRoomState = value

				NotifyOfPropertyChange( Function() FilterRoomState )
				RefreshRoomsList()
			End Set
		End Property

		Public Property IsSortDescending As Boolean
			Get
				Return _isSortDescending
			End Get
			Set
				If Equals( Value, _isSortDescending ) Then Return
				_isSortDescending = value
				NotifyOfPropertyChange( Function() IsSortDescending )
				SortRoomsList()
			End Set
		End Property

		Public Property SortFieldCode As Integer
			Get
				Return _sortFieldCode
			End Get
			Set
				If Equals( Value, _sortFieldCode ) Then Return
				_sortFieldCode = value
				NotifyOfPropertyChange( Function() SortFieldCode )
				SortRoomsList()
			End Set
		End Property

		Public Sub FilterRoomCategoryBy( room As RoomModel )
			FilterRoomCategory = RoomCategories.FirstOrDefault( Function( category ) category.Id = room.CategoryId )
		End Sub

		Public Sub FilterRoomStateBy( room As RoomModel )
			FilterRoomState = room.State
		End Sub

		Public Sub RefreshRoomsList()
			Dim matchNamePrefix As Boolean
			Dim matchCategory As Boolean
			Dim matchState As Boolean

			For Each room As RoomModel In Rooms
				matchNamePrefix = IsNothing( FilterRoomName ) OrElse
				                  room.Name.ToLower().Contains( FilterRoomName )
				matchCategory = IsNothing( FilterRoomCategory ) OrElse
				                (IsNothing( RoomCategories.LastOrDefault() ) OrElse
				                 (IsNothing( FilterRoomCategory.Id ) Or
				                  String.Equals( FilterRoomCategory.Id, "" ) Or
				                  String.Equals( FilterRoomCategory.Id, RoomCategories.LastOrDefault().Id ) Or
				                  String.Equals( room.CategoryId, FilterRoomCategory.Id )))
				matchState = IsNothing( FilterRoomState ) OrElse
				             (FilterRoomState < 0 Or
				              FilterRoomState > 1 Or
				              Equals( room.State, FilterRoomState ))

				If matchNamePrefix And matchCategory And matchState
					room.IsVisible = True
				Else
					room.IsVisible = False
				End If
			Next
		End Sub

		Public Sub SortRoomsList()
			Select Case SortFieldCode
				Case 0
					If IsSortDescending
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.Name ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.Name ) )
					End If
				Case 1
					If IsSortDescending
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.CategoryName ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.CategoryName ) )
					End If
				Case 2
					If IsSortDescending
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.Price ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.Price ) )
					End If
				Case 3
					If IsSortDescending
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.State ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.State ) )
					End If
			End Select
		End Sub

		' Display

		Public Property IsTopDrawerOpen As Boolean
			Get
				Return _isTopDrawerOpen
			End Get
			Set
				If Equals( Value, _isTopDrawerOpen ) Then Return
				_isTopDrawerOpen = value
				NotifyOfPropertyChange( Function() IsTopDrawerOpen )
			End Set
		End Property

		Public Sub ShowRoomDetailDialog()
			ActivateItem( New RoomDetailViewModel() )
		End Sub

		Public Sub ShowAddRoomDialog()
			ActivateItem( New AddRoomViewModel() )
		End Sub
	End Class
End Namespace
