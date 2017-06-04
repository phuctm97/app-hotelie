Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Rooms.Factories.CreateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Screen
		Implements INeedWindowModals

		' Dependencies
		Private _displayCode As Integer

		Public ReadOnly Property ScreenRoomsList As ScreenRoomsListViewModel

		Public Property ScreenRoomDetail As ScreenRoomDetailViewModel

		Public Property ScreenAddRoom As ScreenAddRoomViewModel

		Public Property DisplayCode As Integer
			Get
				Return _displayCode
			End Get
			Set
				If Equals( Value, _displayCode ) Then Return
				_displayCode = value
				NotifyOfPropertyChange( Function() DisplayCode )
			End Set
		End Property

		Public Sub New( getRoomsListQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery,
		                createRoomFactory As ICreateRoomFactory,
		                updateRoomCommand As IUpdateRoomCommand,
		                removeRoomCommand As IRemoveRoomCommand,
		                inventory As IInventory )
			ScreenRoomsList = New ScreenRoomsListViewModel( getRoomsListQuery, getRoomCategoriesListQuery )

			ScreenRoomDetail = New ScreenRoomDetailViewModel( Me,
			                                                  getRoomCategoriesListQuery,
			                                                  updateRoomCommand,
			                                                  removeRoomCommand,
			                                                  inventory )
			ScreenAddRoom = New ScreenAddRoomViewModel( Me,
			                                            getRoomCategoriesListQuery,
			                                            createRoomFactory,
			                                            inventory )

			DisplayName = "Danh sách phòng"

			DisplayCode = - 1
		End Sub

		Protected Overrides Async Sub OnInitialize()
			MyBase.OnInitialize()

			ShowStaticWindowLoadingDialog()
			Await InitAsync()
			Await Task.Delay( 100 ) 'allow binding
			CloseStaticWindowDialog()
		End Sub

		Private Sub Init()
			ScreenRoomsList.Init()
			ScreenRoomDetail.Init()
			ScreenAddRoom.Init()
			DisplayCode = 0
		End Sub

		Private Async Function InitAsync() As Task
			Await ScreenRoomsList.InitAsync()
			Await ScreenRoomDetail.InitAsync()
			Await ScreenAddRoom.InitAsync()
			DisplayCode = 0
		End Function

		Public Sub NavigateToScreenRoomsList()
			DisplayCode = 0
		End Sub

		Public Sub NavigateToScreenRoomDetail( room As Hotelie.Application.Rooms.Queries.GetRoomsList.RoomModel )
			If IsNothing( room ) Then Return

			ScreenRoomDetail.SetRoom( room.Id, room.Name, room.CategoryId, room.Note, room.State )
			DisplayCode = 1
		End Sub

		Public Sub NavigateToScreenAddRoom()
			DisplayCode = 2
		End Sub
	End Class
End Namespace
