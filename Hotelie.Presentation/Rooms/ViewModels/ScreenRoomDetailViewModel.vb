Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList

Namespace Rooms.ViewModels
	Public Class ScreenRoomDetailViewModel
		Inherits PropertyChangedBase

		' Dependencies
		Private ReadOnly _getRoomCategoriesList As IGetRoomCategoriesListQuery

		Private _roomName As String
		Private _roomCategory As RoomCategoryModel
		Private _roomNote As String
		Private _roomState As Integer

		Sub New( getRoomCategoriesList As IGetRoomCategoriesListQuery )
			_getRoomCategoriesList = getRoomCategoriesList

			RoomCategories = New BindableCollection(Of RoomCategoryModel)
			RoomCategories.AddRange( _getRoomCategoriesList.Execute() )
		End Sub

		Private Property RoomId As String

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
				If Equals(Value, _roomCategory) Then Return
				_roomCategory = value
				NotifyOfPropertyChange(Function() RoomCategory)
			End Set
		End Property

		Public Property RoomNote As String
			Get
				Return _roomNote
			End Get
			Set
				If String.Equals(Value, _roomNote) Then Return
				_roomNote = value
				NotifyOfPropertyChange(Function() RoomNote)
			End Set
		End Property

		Public Property RoomState As Integer
			Get
				Return _roomState
			End Get
			Set
				If Equals(Value, _roomState) Then Return
				_roomState = value
				NotifyOfPropertyChange(Function() RoomState)
			End Set
		End Property

		' ReSharper disable once CollectionNeverUpdated.Global
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		Public Sub SetRoom( id As String,
		                    name As String,
		                    categoryId As String,
		                    state As Integer,
		                    note As String )
			RoomId = id
			RoomName = name
			RoomCategory = RoomCategories.FirstOrDefault( Function( c ) Equals( c.Id, categoryId ) )
			RoomState = state
			RoomNote = note
		End Sub
	End Class
End Namespace