Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Infrastructure
Imports Hotelie.Presentation.Start.MainWindow.Models
Imports MaterialDesignThemes.Wpf

Namespace Rooms.ViewModels
	Public Class ScreenRoomDetailViewModel
		Inherits PropertyChangedBase
		Implements IChild(Of RoomsWorkspaceViewModel)

		' Dependencies
		Private ReadOnly _getRoomCategoriesListQuery As IGetRoomCategoriesListQuery

		' Backing fields
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
			ResetValues()
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

		' Exit
		Private Sub ResetValues()
			_roomId = String.Empty
			_roomName = "Chưa có tên"
			_roomCategory = RoomCategories.FirstOrDefault()
			_roomNote = String.Empty
			_roomState = 0

			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Sub

		Private Sub [Exit]()
			ResetValues()
			ParentWorkspace.NavigateToScreenRoomsList()
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

		Private Function CheckForPendingChanges() As Boolean
			Return (Not String.IsNullOrEmpty( _roomId )) And
			       ((Not String.Equals( _roomName, _originalRoomName )) Or
			        (Not String.Equals( _roomCategory.Id, _originalRoomCategoryId )) Or
			        (Not String.Equals( _roomNote, _originalRoomNote )))
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
			Dim result = Await DialogHost.Show( dialog, "window" )

			If String.Equals( result, "THOÁT" ) Then Return 0
			If String.Equals( result, "HỦY" ) Then Return 2
			Return 1
		End Function

		' Save
		Public Sub PreviewSave()
			If Not ValidateData() Then Return
			Save()
		End Sub

		Private Sub Save()
			' try update
			IoC.Get(Of IMainWindow).ShowStaticWindowDialog( New LoadingDialog() )
			' TODO: call update here
			IoC.Get(Of IMainWindow).CloseStaticWindowDialog()

			[Exit]()
		End Sub

		Private Function ValidateData() As Boolean
			If String.IsNullOrWhiteSpace( RoomName )
				IoC.Get(Of IMainWindow).ShowStaticBottomNotification( StaticNotificationType.Information,
				                                                      "Vui lòng nhập tên phòng!" )
				Return False
			End If

			Return True
		End Function

		' Delete
		Public Async Sub PreviewDelete()
			Dim result = Await ConfirmDelete()

			If Equals( result, 0 )
				Delete()
			End If
		End Sub

		Private Async Function ConfirmDelete() As Task(Of Integer)
			' show dialog
			Dim dialog = New TwoButtonDialog( "Xóa phòng. Tiếp tục?",
			                                  "XÓA",
			                                  "HỦY",
			                                  True,
			                                  False )
			Dim result = Await DialogHost.Show( dialog, "window" )

			If String.Equals( result, "XÓA" ) Then Return 0
			Return 1
		End Function

		Private Sub Delete()
			' try update
			IoC.Get(Of IMainWindow).ShowStaticWindowDialog( New LoadingDialog() )
			' TODO: call delete here
			IoC.Get(Of IMainWindow).CloseStaticWindowDialog()

			[Exit]()
		End Sub
	End Class
End Namespace