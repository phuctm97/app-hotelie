Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Presentation.Common.Controls
Imports MaterialDesignThemes.Wpf

Namespace Rooms.ViewModels
	Public Class ScreenAddRoomViewModel
		Inherits PropertyChangedBase
		Implements IChild(Of RoomsWorkspaceViewModel)

		' Dependencies
		Private ReadOnly _getRoomCategoriesList As IGetRoomCategoriesListQuery

		Private _roomName As String
		Private _roomCategory As RoomCategoryModel
		Private _roomNote As String

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
			_roomName = String.Empty
			_roomCategory = RoomCategories.FirstOrDefault()
			_roomNote = String.Empty
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

		Public Sub Reset()
			RoomName = String.Empty
			RoomCategory = RoomCategories.FirstOrDefault()
			RoomNote = String.Empty
		End Sub

		Public Async Sub PreviewNavigateToScreenRoomsList()
			Dim dialog = New ThreeButtonDialog( "Thoát mà không lưu các thay đổi?",
			                                    "THOÁT",
			                                    "LƯU & THOÁT",
			                                    "HỦY",
			                                    False,
			                                    True,
			                                    False )

			Dim result = Await DialogHost.Show( dialog, "shell" )

			If String.Equals( result, "THOÁT" )
				ParentWorkspace.NavigateToScreenRoomsList()
			ElseIf String.Equals( result, "LƯU & THOÁT" )

			End If
		End Sub
	End Class
End Namespace
