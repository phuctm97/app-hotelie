Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Factories.CreateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Rooms.ViewModels
	Public Class ScreenAddRoomViewModel
		Inherits AppScreenHasSaving
		Implements IChild(Of RoomsWorkspaceViewModel),
		           INeedWindowModals

		' Dependencies
		Private ReadOnly _getRoomCategoriesList As IGetRoomCategoriesListQuery
		Private ReadOnly _createRoomFactory As ICreateRoomFactory
		Private ReadOnly _inventory As IInventory

		' Backing fields
		Private _roomName As String
		Private _roomCategory As RoomCategoriesListItemModel
		Private _roomNote As String

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent

		' Initialization
		Sub New( workspace As RoomsWorkspaceViewModel,
		         getRoomCategoriesList As IGetRoomCategoriesListQuery,
		         createRoomFactory As ICreateRoomFactory,
		         inventory As IInventory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ParentWorkspace = workspace
			_getRoomCategoriesList = getRoomCategoriesList
			_createRoomFactory = createRoomFactory
			_inventory = inventory

			RoomCategories = New BindableCollection(Of RoomCategoriesListItemModel)
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

		' Binding model
		Public Property RoomName As String
			Get
				Return _roomName
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _roomName ) Then Return
				_roomName = value
				NotifyOfPropertyChange( Function() RoomName )
			End Set
		End Property

		Public Property RoomCategory As RoomCategoriesListItemModel
			Get
				Return _roomCategory
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _roomCategory ) Then Return
				_roomCategory = value
				NotifyOfPropertyChange( Function() RoomCategory )
			End Set
		End Property

		Public Property RoomNote As String
			Get
				Return _roomNote
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _roomNote ) Then Return
				_roomNote = value
				NotifyOfPropertyChange( Function() RoomNote )
			End Set
		End Property

		Public ReadOnly Property RoomState As Integer
			Get
				Return 0
			End Get
		End Property

		' Binding data
		' ReSharper disable once CollectionNeverUpdated.Global
		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoriesListItemModel)

		' Exit
		Private Sub ResetValues()
			RoomName = String.Empty
			RoomCategory = RoomCategories.FirstOrDefault()
			RoomNote = String.Empty
		End Sub

		Public Overrides ReadOnly Property IsEdited As Boolean
			Get
				Return CheckForPendingChanges()
			End Get
		End Property

		Private Function CheckForPendingChanges()
			Return (Not String.IsNullOrWhiteSpace( RoomName )) Or
			       (Not String.IsNullOrWhiteSpace( RoomNote ))
		End Function

		Public Overrides Function ActualExitAsync() As Task
			ResetValues()

			ParentWorkspace.NavigateToScreenRoomsList()
			Return MyBase.ActualExitAsync()
		End Function

		' Save
		Public Overrides Function CanSave() As Task(Of Boolean)
			If String.IsNullOrWhiteSpace( RoomName )
				IoC.Get(Of IMainWindow).ShowStaticBottomNotification( StaticNotificationType.Information,
				                                                      "Vui lòng nhập tên phòng!" )
				Return Task.FromResult( False )
			End If

			Return Task.FromResult( True )
		End Function

		Public Overrides Async Function ActualSaveAsync() As Task
			' try create
			ShowStaticWindowLoadingDialog()
			Dim newRoomId = Await _createRoomFactory.ExecuteAsync( RoomName, RoomCategory.Id, RoomNote )

			If String.IsNullOrEmpty( newRoomId )
				OnSaveFail()
			Else
				Await OnSaveSuccessAsync( newRoomId )
			End If

			CloseStaticWindowDialog()
		End Function

		Private Async Function OnSaveSuccessAsync( newRoomId ) As Task
			Await _inventory.OnRoomAddedAsync( newRoomId )
			Await ActualExitAsync()
		End Function

		Private Sub OnSaveFail()
			ShowStaticBottomNotification( StaticNotificationType.Error, "Sự cố ngoài ý muốn. Tạo phòng thất bại!" )
		End Sub
	End Class
End Namespace
