Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Presentation.Common

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Conductor(Of IScreen)
		Implements IWorkspace

		Private _isTopDrawerOpen As Boolean
		Private ReadOnly _getRoomsListQuery As IGetRoomsListQuery
		Private ReadOnly _getRoomCategoriesListQuery As IGetRoomCategoriesListQuery
		Private _rooms As IObservableCollection(Of RoomModel)

		Public Sub New( getRoomsListQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery )
			_getRoomsListQuery = getRoomsListQuery
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery

			DisplayName = "Danh sách phòng"
			IsTopDrawerOpen = False

			Rooms = New BindableCollection(Of RoomModel)
			RoomCategories = New BindableCollection(Of RoomCategoryModel)
			RoomStates = New BindableCollection(Of Integer)
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			Rooms.AddRange( _getRoomsListQuery.Execute() )
			RoomCategories.AddRange( _getRoomCategoriesListQuery.Execute() )
			RoomStates.AddRange( {0, 1, 2} )
		End Sub

		Protected Overrides Sub OnViewReady( view As Object )
			MyBase.OnViewReady( view )

			FilterRooms()
		End Sub

		' Rooms
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

		Public Sub FilterRooms( Optional namePrefix As String = "",
		                        Optional categoryId As String = "",
		                        Optional state As Integer = - 1 )
			namePrefix = namePrefix.ToLower()

			Dim matchNamePrefix As Boolean
			Dim matchCategory As Boolean
			Dim matchState As Boolean

			For Each room As RoomModel In Rooms
				matchNamePrefix = room.Name.ToLower().Contains( namePrefix )
				matchCategory = String.Equals( categoryId, "" ) Or
				                String.Equals( categoryId, "##all##" ) Or
				                String.Equals( room.CategoryId, categoryId )
				matchState = state < 0 Or state > 1 Or Equals( room.State, state )

				If matchNamePrefix And matchCategory And matchState
					room.IsVisible = True
				Else
					room.IsVisible = False
				End If
			Next
		End Sub

		Public Sub SortRooms( value As Integer,
		                      Optional descending As Boolean = False )
			Select Case value
				Case 0
					If descending
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.Name ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.Name ) )
					End If
				Case 1
					If descending
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.CategoryName ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.CategoryName ) )
					End If
				Case 2
					If descending
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.Price ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.Price ) )
					End If
				Case 3
					If descending
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderByDescending( Function( p ) p.State ) )
					Else
						Rooms = New BindableCollection(Of RoomModel)( Rooms.OrderBy( Function( p ) p.State ) )
					End If
			End Select
		End Sub

		' Room categories
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		Public ReadOnly Property RoomStates As IObservableCollection(Of Integer)
		' Dialog

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
