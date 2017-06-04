Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Factories.CreateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Rooms.ViewModels
	Public Class ScreenAddRoomViewModel
		Inherits PropertyChangedBase
		Implements IChild(Of RoomsWorkspaceViewModel),
		           INeedWindowModals

		' Dependencies
		Private ReadOnly _getRoomCategoriesList As IGetRoomCategoriesListQuery
		Private ReadOnly _createRoomFactory As ICreateRoomFactory
		Private ReadOnly _inventory As IInventory

		' Backing fields
		Private _roomName As String
		Private _roomCategory As RoomCategoryModel
		Private _roomNote As String

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent

		' Initialization
		Sub New( workspace As RoomsWorkspaceViewModel,
		         getRoomCategoriesList As IGetRoomCategoriesListQuery,
		         createRoomFactory As ICreateRoomFactory,
		         inventory As IInventory )
			ParentWorkspace = workspace
			_getRoomCategoriesList = getRoomCategoriesList
			_createRoomFactory = createRoomFactory
			_inventory = inventory

			RoomCategories = New BindableCollection(Of RoomCategoryModel)
		End Sub

		Public Sub Init()
			InitRoomCategories()
			InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Await InitRoomCategoriesAsync()
			InitValues()
		End Function

		Private Sub InitRoomCategories()
			RoomCategories.Clear()
			RoomCategories.AddRange( _getRoomCategoriesList.Execute() )
		End Sub

		Private Async Function InitRoomCategoriesAsync() As Task
			RoomCategories.Clear()
			RoomCategories.AddRange( Await _getRoomCategoriesList.ExecuteAsync() )
		End Function

		Private Sub InitValues()
			RoomName = String.Empty
			RoomCategory = RoomCategories.FirstOrDefault()
			RoomNote = String.Empty
		End Sub

		' Data
		Public Property RoomName As String
			Get
				Return _roomName
			End Get
			Set
				If String.Equals( Value, _roomName ) Then Return
				_roomName = value
				NotifyOfPropertyChange( Function() RoomName )
			End Set
		End Property

		Public Property RoomCategory As RoomCategoryModel
			Get
				Return _roomCategory
			End Get
			Set
				If Equals( Value, _roomCategory ) Then Return
				_roomCategory = value
				NotifyOfPropertyChange( Function() RoomCategory )
			End Set
		End Property

		Public Property RoomNote As String
			Get
				Return _roomNote
			End Get
			Set
				If String.Equals( Value, _roomNote ) Then Return
				_roomNote = value
				NotifyOfPropertyChange( Function() RoomNote )
			End Set
		End Property

		Public ReadOnly Property RoomState As Integer
			Get
				Return 0
			End Get
		End Property

		' ReSharper disable once CollectionNeverUpdated.Global
		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		' Exit
		Private Sub ResetValues()
			RoomName = String.Empty
			RoomCategory = RoomCategories.FirstOrDefault()
			RoomNote = String.Empty
		End Sub

		Public Async Sub PreviewExit()
			If CheckForPendingChanges()
				Dim result = Await ConfirmExit()

				If Equals( result, 1 )
					PreviewSave()
					Return
				ElseIf Equals( result, 2 )
					Return
				End If
			End If

			[Exit]()
		End Sub

		Private Sub [Exit]()
			ResetValues()
			ParentWorkspace.NavigateToScreenRoomsList()
		End Sub

		Private Function CheckForPendingChanges()
			Return (Not String.IsNullOrWhiteSpace( RoomName )) Or
			       (Not String.IsNullOrWhiteSpace( RoomNote ))
		End Function

		Private Async Function ConfirmExit() As Task(Of Integer)
			' show dialog
			Dim dialog = New ThreeButtonDialog( "Thoát mà không lưu các thay đổi?",
			                                    "THOÁT",
			                                    "LƯU & THOÁT",
			                                    "HỦY",
			                                    False,
			                                    True,
			                                    False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "THOÁT" ) Then Return 0
			If String.Equals( result, "HỦY" ) Then Return 2
			Return 1
		End Function

		' Save
		Public Sub PreviewSave()
			If Not ValidateData() Then Return
			SaveAsync()
		End Sub

		Private Sub Save()
			' try update
			Dim roomModel = _createRoomFactory.Execute( RoomName, RoomCategory.Id, RoomNote )
			If IsNothing( roomModel )
				ShowStaticBottomNotification(StaticNotificationType.Error, "Sự cố ngoài ý muốn. Tạo phòng thất bại!")
			Else
				_inventory.OnRoomAdded( roomModel.Id, roomModel.Name, roomModel.CategoryId, roomModel.Note )
				[Exit]()
			End If
		End Sub

		Private Async Sub SaveAsync()
			' try update
			ShowStaticWindowLoadingDialog()
			Dim roomModel = Await _createRoomFactory.ExecuteAsync( RoomName, RoomCategory.Id, RoomNote )
			CloseStaticWindowDialog()

			If IsNothing( roomModel )
				ShowStaticBottomNotification(StaticNotificationType.Error, "Sự cố ngoài ý muốn. Tạo phòng thất bại!")
			Else
				_inventory.OnRoomAdded( roomModel.Id, roomModel.Name, roomModel.CategoryId, roomModel.Note )
				[Exit]()
			End If
		End Sub

		Private Function ValidateData() As Boolean
			If String.IsNullOrWhiteSpace( RoomName )
				IoC.Get(Of IMainWindow).ShowStaticBottomNotification( StaticNotificationType.Information,
				                                                      "Vui lòng nhập tên phòng!" )
				Return False
			End If

			Return True
		End Function
	End Class
End Namespace
