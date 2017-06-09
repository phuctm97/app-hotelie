Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands
Imports Hotelie.Application.Rooms.Factories
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.WorkspaceShell.ViewModels
Imports MaterialDesignThemes.Wpf

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits AppScreen
		Implements INeedWindowModals
		Implements IChild(Of WorkspaceShellViewModel)

		' Backing fields
		Private _displayCode As Integer

		' Bind models
		Public ReadOnly Property ScreenRoomsList As ScreenRoomsListViewModel

		Public ReadOnly Property ScreenRoomDetail As ScreenRoomDetailViewModel

		Public ReadOnly Property ScreenAddRoom As ScreenAddRoomViewModel

		' Bind properties
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
		Public Property ParentShell As WorkspaceShellViewModel Implements IChild(Of WorkspaceShellViewModel).Parent
			Get
				Return TryCast(Parent, WorkspaceShellViewModel)
			End Get
			Set
				Parent = Value
				NotifyOfPropertyChange(Function() ParentShell)
			End Set
		End Property

		' Initializations
		Public Sub New( getAllRoomsQuery As IGetAllRoomsQuery,
		                getAllRoomCategoriesQuery As IGetAllRoomCategoriesQuery,
		                getRoomQuery As IGetRoomQuery,
		                createRoomFactory As ICreateRoomFactory,
		                updateRoomCommand As IUpdateRoomCommand,
		                removeRoomCommand As IRemoveRoomCommand,
		                inventory As IInventory )
			MyBase.New( ColorZoneMode.PrimaryDark )

			DisplayName = "Danh sách phòng"

			'load screens
			ScreenRoomsList = New ScreenRoomsListViewModel( Me,
			                                                getAllRoomsQuery,
			                                                getAllRoomCategoriesQuery )
			ScreenRoomDetail = New ScreenRoomDetailViewModel( Me,
			                                                  getAllRoomCategoriesQuery,
			                                                  getRoomQuery,
			                                                  updateRoomCommand,
			                                                  removeRoomCommand,
			                                                  inventory )
			ScreenAddRoom = New ScreenAddRoomViewModel( Me,
			                                            getAllRoomCategoriesQuery,
			                                            createRoomFactory,
			                                            inventory )
			DisplayCode = - 1

			InitializeComponents()
		End Sub

		Private Sub InitializeComponents()
			ScreenRoomsList.Init()
			ScreenRoomDetail.Init()
			ScreenAddRoom.Init()
			DisplayCode = 0
		End Sub

		' Navigations
		Public Sub NavigateToScreenRoomsList()
			DisplayCode = 0
		End Sub

		Public Async Sub NavigateToScreenRoomDetail( roomId As String )
			If String.IsNullOrEmpty( roomId ) Then Return

			Await ScreenRoomDetail.SetRoomAsync( roomId )
			DisplayCode = 1
		End Sub

		Public Sub NavigateToScreenAddRoom()
			DisplayCode = 2
		End Sub
	End Class
End Namespace
