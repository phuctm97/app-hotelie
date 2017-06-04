Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models
Imports MaterialDesignThemes.Wpf

Namespace Rooms.ViewModels
	Public Class ScreenRoomDetailViewModel
		Inherits PropertyChangedBase
		Implements IChild(Of RoomsWorkspaceViewModel)

		' Dependencies
		Private ReadOnly _getRoomCategoriesListQuery As IGetRoomCategoriesListQuery

		Private _roomId As String
		Private _roomName As String
		Private _roomCategory As RoomCategoryModel
		Private _roomNote As String
		Private _roomState As Integer

		Private _originalRoomName As String
		Private _originalRoomCategoryId As String
		Private _originalRoomNote As String

		' Parent

		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent

		' Initialization

		Sub New( workspace As RoomsWorkspaceViewModel,
		         getRoomCategoriesListQuery As IGetRoomCategoriesListQuery )

			ParentWorkspace = workspace
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery

			InitRoomCategories( RoomCategories )

			InitValues()
		End Sub

		Private Sub InitValues()
			_roomId = String.Empty
			_roomName = "Chưa có tên"
			_roomCategory = RoomCategories.FirstOrDefault()
			_roomNote = String.Empty
			_roomState = 0

			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Sub

		Private Sub InitRoomCategories( ByRef roomCategoryCollection As IObservableCollection(Of RoomCategoryModel) )
			roomCategoryCollection = New BindableCollection(Of RoomCategoryModel)
			roomCategoryCollection.AddRange( _getRoomCategoriesListQuery.Execute() )
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

		Public Property RoomState As Integer
			Get
				Return _roomState
			End Get
			Set
				If Equals( Value, _roomState ) Then Return
				_roomState = value
				NotifyOfPropertyChange( Function() RoomState )
			End Set
		End Property

		' ReSharper disable once CollectionNeverUpdated.Global
		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		Public Sub SetRoom( id As String,
		                    name As String,
		                    categoryId As String,
		                    note As String,
		                    state As Integer )
			' Bind values
			_roomId = id
			RoomName = name
			RoomCategory = RoomCategories.FirstOrDefault( Function( c ) Equals( c.Id, categoryId ) )
			RoomState = state
			RoomNote = note

			' Backup old values
			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Sub

		Public Async Sub PreviewNavigateToScreenRoomsList()
			' nothing changes
			If String.Equals( _roomName, _originalRoomName ) And
			   String.Equals( _roomCategory.Id, _originalRoomCategoryId ) And
			   String.Equals( _roomNote, _originalRoomNote )
				ParentWorkspace.NavigateToScreenRoomsList()
				Return
			End If

			' show dialog
			Dim dialog = New ThreeButtonDialog( "Thoát mà không lưu các thay đổi?",
			                                    "THOÁT",
			                                    "LƯU & THOÁT",
			                                    "HỦY",
			                                    False,
			                                    True,
			                                    False )
			Dim result = Await DialogHost.Show( dialog, "window" )

			If String.Equals( result, "THOÁT" )
				ParentWorkspace.NavigateToScreenRoomsList()
			ElseIf String.Equals( result, "LƯU & THOÁT" )
				SaveChanges()
				ParentWorkspace.NavigateToScreenRoomsList()
			End If
		End Sub

		Public Sub SaveChanges()
			If String.IsNullOrWhiteSpace( RoomName )
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Information, "Vui lòng nhập tên phòng!" )
				Return
			End If

			' try update
			IoC.Get(Of IMainWindow).ShowStaticShellDialog( New LoadingDialog() )
			' TODO: call update here
			IoC.Get(Of IMainWindow).CloseStaticShellDialog()
		End Sub
	End Class
End Namespace