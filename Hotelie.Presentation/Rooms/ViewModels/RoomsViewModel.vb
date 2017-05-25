Imports System.Windows.Automation
Imports Caliburn.Micro
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Rooms.Models
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Rooms.ViewModels
    Public Class RoomsViewModel
        Inherits Screen
        Implements IWorkspace

        Private ReadOnly _getRoomsListQuery As  IGetRoomsListQuery

        Public ReadOnly Property RoomDetail As RoomDetailViewModel

        Private _rooms As IObservableCollection(Of RoomsListItemModel)
        Private _categories As IObservableCollection(Of String)

        Public Property Rooms As IObservableCollection(Of RoomsListItemModel)
            Get
                Return _rooms
            End Get
            Set
                If Equals(_rooms, Value) Then Return

                _rooms = Value

                NotifyOfPropertyChange(Function() Rooms)
            End Set
        End Property
        Public Property Categories As IObservableCollection(Of String)
            Get
                Return _categories
            End Get
            Set
                If Equals(_categories, Value) Then Return
                _categories = Value
                NotifyOfPropertyChange(Function() Categories)
            End Set
        End Property

        Public Sub ShowRoomDetail(id As String)
            RoomDetail.IsOpen = True
            'RoomDetail.Id = id
        End Sub

        Public Sub New(getRoomsListQuery As IGetRoomsListQuery)
            _getRoomsListQuery = getRoomsListQuery
            DisplayName = "Rooms"
            Rooms = New BindableCollection(Of RoomsListItemModel)
            RoomDetail = New RoomDetailViewModel()
        End Sub

        Public Sub RefreshRoomsList()
            Rooms.Clear()
            Rooms = _getRoomsListQuery.Execute()
        End Sub

        Protected Overrides Sub OnViewLoaded(view As Object)
            MyBase.OnViewLoaded(view)
            RefreshRoomsList()
        End Sub

    End Class
End Namespace