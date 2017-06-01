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

		Public Sub New( getRoomsListQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery )
			_getRoomsListQuery = getRoomsListQuery
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery

			DisplayName = "Danh sách phòng"
			IsTopDrawerOpen = False

			Rooms = New BindableCollection(Of RoomModel)
			RoomCategories = New BindableCollection(Of RoomCategoryModel)
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			Rooms.AddRange( _getRoomsListQuery.Execute() )
			RoomCategories.AddRange( _getRoomCategoriesListQuery.Execute() )
		End Sub

		Protected Overrides Sub OnViewReady( view As Object )
			MyBase.OnViewReady( view )

			FilterRooms( String.Empty )
		End Sub

		' Rooms
		Public ReadOnly Property Rooms As IObservableCollection(Of RoomModel)

		Public Sub FilterRooms( Optional namePrefix As String = "",
		                        Optional category As RoomCategoryModel = Nothing )
			namePrefix = namePrefix.ToLower()

			Dim matchNamePrefix As Boolean
			Dim matchCategory As Boolean

			For Each room As RoomModel In Rooms
				matchNamePrefix = False
				matchCategory = False

				matchNamePrefix = room.Name.ToLower().Contains( namePrefix )
				matchCategory = (category Is Nothing) OrElse String.Equals( room.CategoryId, category.Id )

				If matchNamePrefix And matchCategory
					room.IsVisible = True
				Else
					room.IsVisible = False
				End If
			Next
		End Sub

		' Room categories
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

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
