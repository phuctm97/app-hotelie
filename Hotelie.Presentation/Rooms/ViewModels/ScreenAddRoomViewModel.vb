Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Factories
Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Rooms.Models
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Rooms.ViewModels
	Public Class ScreenAddRoomViewModel
		Inherits AppScreenHasSaving
		Implements IChild(Of RoomsWorkspaceViewModel),
		           INeedWindowModals

		' Dependencies
		Private ReadOnly _getAllRoomCategoriesQuery As IGetAllRoomCategoriesQuery
		Private ReadOnly _createRoomFactory As ICreateRoomFactory
		Private ReadOnly _inventory As IInventory

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent

		' Initializations
		Sub New( workspace As RoomsWorkspaceViewModel,
		         getAllRoomCategoriesQuery As IGetAllRoomCategoriesQuery,
		         createRoomFactory As ICreateRoomFactory,
		         inventory As IInventory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ParentWorkspace = workspace
			_getAllRoomCategoriesQuery = getAllRoomCategoriesQuery
			_createRoomFactory = createRoomFactory
			_inventory = inventory

			Categories = New BindableCollection(Of RoomCategoryModel)
			Room = New EditableRoomModel()
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
			Categories.Clear()
			Categories.AddRange( _getAllRoomCategoriesQuery.Execute() )
		End Sub

		Private Async Function InitRoomCategoriesAsync() As Task
			Categories.Clear()
			Categories.AddRange( Await _getAllRoomCategoriesQuery.ExecuteAsync() )
		End Function

		Private Sub InitValues()
			Room.Id = String.Empty
			Room.Name = String.Empty
			Room.Category = Categories.FirstOrDefault()
			Room.Note = String.Empty
			Room.State = 0
		End Sub

		' Binding model
		Public ReadOnly Property Room As EditableRoomModel

		' Binding data
		' ReSharper disable once CollectionNeverUpdated.Global
		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property Categories As IObservableCollection(Of RoomCategoryModel)

		' Exit
		Private Sub ResetValues()
			Room.Id = String.Empty
			Room.Name = String.Empty
			Room.Category = Categories.FirstOrDefault()
			Room.Note = String.Empty
			Room.State = 0
		End Sub

		Public Overrides ReadOnly Property IsEdited As Boolean
			Get
				Return CheckForPendingChanges()
			End Get
		End Property

		Private Function CheckForPendingChanges()
			Return (Not String.IsNullOrWhiteSpace( Room.Name )) Or
			       (Not String.IsNullOrWhiteSpace( Room.Note ))
		End Function

		Public Overrides Function ActualExitAsync() As Task
			ResetValues()

			ParentWorkspace.NavigateToScreenRoomsList()
			Return MyBase.ActualExitAsync()
		End Function

		' Save
		Public Overrides Function CanSave() As Task(Of Boolean)
			If String.IsNullOrWhiteSpace( Room.Name )
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Vui lòng nhập tên phòng" )
				Return Task.FromResult( False )
			End If

			If IsNothing( Room.Category ) OrElse String.IsNullOrEmpty( Room.Category.Id )
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Vui lòng chọn loại phòng" )
				Return Task.FromResult( False )
			End If

			Return Task.FromResult( True )
		End Function

		Public Overrides Async Function ActualSaveAsync() As Task
			' try create
			ShowStaticWindowLoadingDialog()
			Dim newRoomId = Await _createRoomFactory.ExecuteAsync( Room.Name, Room.Category.Id, Room.Note )

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
			ShowStaticBottomNotification( StaticNotificationType.Error, "Sự cố ngoài ý muốn. Tạo phòng thất bại." )
		End Sub
	End Class
End Namespace
