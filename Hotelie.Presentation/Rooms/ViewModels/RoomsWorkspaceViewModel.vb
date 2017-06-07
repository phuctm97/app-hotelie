Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Rooms.Factories.CreateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomData
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.WorkspaceShell.ViewModels

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits AppScreen
		Implements INeedWindowModals
		Implements IChild(Of WorkspaceShellViewModel)

		' Dependencies
		Private _displayCode As Integer

		' Screens
		Public ReadOnly Property ScreenRoomsList As ScreenRoomsListViewModel

		Public ReadOnly Property ScreenRoomDetail As ScreenRoomDetailViewModel

		Public ReadOnly Property ScreenAddRoom As ScreenAddRoomViewModel

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

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Private Property ParentShell As WorkspaceShellViewModel Implements IChild(Of WorkspaceShellViewModel).Parent
			Get
				Return CType(Parent, WorkspaceShellViewModel)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		' Initializations
		Public Sub New( getRoomsListQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery,
		                getRoomDataQuery As IGetRoomDataQuery,
		                createRoomFactory As ICreateRoomFactory,
		                updateRoomCommand As IUpdateRoomCommand,
		                removeRoomCommand As IRemoveRoomCommand,
		                inventory As IInventory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ScreenRoomsList = New ScreenRoomsListViewModel( Me,
			                                                getRoomsListQuery,
			                                                getRoomCategoriesListQuery,
			                                                removeRoomCommand,
			                                                inventory )
			ScreenRoomDetail = New ScreenRoomDetailViewModel( Me,
			                                                  getRoomDataQuery,
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

			InitializeComponents()
		End Sub

		Private Sub InitializeComponents()
			Init()
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

		' Navigations
		Public Sub NavigateToScreenRoomsList()
			DisplayCode = 0
		End Sub

		Public Sub NavigateToScreenRoomDetail( roomsListItem As _
			                                     Hotelie.Application.Rooms.Queries.GetRoomsList.RoomsListItemModel )
			If IsNothing( roomsListItem ) Then Return

			ScreenRoomDetail.SetRoom( roomsListItem.Id )
			DisplayCode = 1
		End Sub

		Public Async Sub NavigateToScreenRoomDetailAsync( roomsListItem As _
			                                                Hotelie.Application.Rooms.Queries.GetRoomsList.RoomsListItemModel )
			If IsNothing( roomsListItem ) Then Return

			Await ScreenRoomDetail.SetRoomAsync( roomsListItem.Id )
			DisplayCode = 1
		End Sub

		Public Sub NavigateToScreenAddRoom()
			DisplayCode = 2
		End Sub
	End Class
End Namespace
