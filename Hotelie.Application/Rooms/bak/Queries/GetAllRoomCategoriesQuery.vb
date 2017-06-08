Imports System.Data.Entity
Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms

Namespace Rooms.Queries
    Public Class GetAllRoomCategoriesQuery
        Implements IGetAllRoomCategoriesQuery

        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute() As IEnumerable(Of IRoomCategoryModel) Implements IGetAllRoomCategoriesQuery.Execute
            Dim categories = _roomRepository.GetAllRoomCategories().ToList()
            Dim categoryModels = New List(Of RoomCategoryModel)

            For Each roomCategory As RoomCategory In categories
                categoryModels.Add(New RoomCategoryModel(roomCategory))
            Next

            Return categoryModels
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of IRoomCategoryModel)) Implements IGetAllRoomCategoriesQuery.ExecuteAsync
            Dim categories = Await _roomRepository.GetAllRoomCategories().ToListAsync()
            Dim categoryModels = New List(Of RoomCategoryModel)

            For Each roomCategory As RoomCategory In categories
                categoryModels.Add(New RoomCategoryModel(roomCategory))
            Next

            Return categoryModels
        End Function
    End Class
End NameSpace