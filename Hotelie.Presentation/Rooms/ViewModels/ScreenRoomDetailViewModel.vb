Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Presentation.Common.Controls
Imports MaterialDesignThemes.Wpf

Namespace Rooms.ViewModels
	Public Class ScreenRoomDetailViewModel
		Inherits PropertyChangedBase
		Implements IChild(Of RoomsWorkspaceViewModel)

		' Dependencies
		Private ReadOnly _getRoomCategoriesList As IGetRoomCategoriesListQuery

		Private _roomId As String
		Private _roomName As String
		Private _roomCategory As RoomCategoryModel
		Private _roomNote As String
		Private _roomState As Integer
		Private _isEdited As Boolean

		' Parent

		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent

		' Initialization

		Sub New( workspace As RoomsWorkspaceViewModel,
		         getRoomCategoriesList As IGetRoomCategoriesListQuery )
			ParentWorkspace = workspace

			_getRoomCategoriesList = getRoomCategoriesList

			InitRoomCategories( RoomCategories )

			InitValues()
		End Sub

		Private Sub InitValues()
			_roomId = String.Empty
			_roomName = "Chưa có tên"
			_roomCategory = RoomCategories.FirstOrDefault()
			_roomNote = String.Empty
			_roomState = 0
			_isEdited = False
		End Sub

		Private Sub InitRoomCategories( ByRef roomCategoryCollection As IObservableCollection(Of RoomCategoryModel) )
			roomCategoryCollection = New BindableCollection(Of RoomCategoryModel)
			roomCategoryCollection.AddRange( _getRoomCategoriesList.Execute() )
		End Sub

		' Data

		Public Property RoomName As String
			Get
				Return _roomName
			End Get
			Set
				If String.Equals( Value, _roomName ) Then Return
				_roomName = value
				_isEdited = True
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
				_isEdited = True
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
				_isEdited = True
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
				_isEdited = True
				NotifyOfPropertyChange( Function() RoomState )
			End Set
		End Property

		' ReSharper disable once CollectionNeverUpdated.Global
		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		Public Sub SetRoom( id As String,
		                    name As String,
		                    categoryId As String,
		                    state As Integer,
		                    note As String )
			_roomId = id
			RoomName = name
			RoomCategory = RoomCategories.FirstOrDefault( Function( c ) Equals( c.Id, categoryId ) )
			RoomState = state
			RoomNote = note
			_isEdited = False
		End Sub

		Public Async Sub PreviewNavigateToScreenRoomsList()
			If Not _isEdited
				ParentWorkspace.NavigateToScreenRoomsList()
				Return
			End If


			Dim dialog = New ThreeButtonDialog( "Thoát mà không lưu các thay đổi?",
			                                    "THOÁT",
			                                    "LƯU & THOÁT",
			                                    "HỦY",
			                                    False,
			                                    True,
			                                    False )

			Dim result = Await DialogHost.Show( dialog, "screen-room-detail" )

			If String.Equals( result, "THOÁT" )
				ParentWorkspace.NavigateToScreenRoomsList()
			ElseIf String.Equals( result, "LƯU & THOÁT" )

			End If
		End Sub
	End Class
End Namespace