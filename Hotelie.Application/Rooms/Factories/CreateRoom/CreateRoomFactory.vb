Imports System.Windows.Media
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms

Namespace Rooms.Factories.CreateRoom
    Public Class CreateRoomFactory
        Implements ICreateRoomFactory

        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(roomRepository As IRoomRepository, unitOfWork As IUnitOfWork)
            _unitOfWork = unitOfWork
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(id As String, name As String, categoryId As String, note As String) As RoomModel _
            Implements ICreateRoomFactory.Execute

            Dim category = _roomRepository.GetRoomCategory(categoryId)

            Dim room = New RoomModel() With { _
                    .Id = id,
                    .CategoryId = category.Id,
                    .CategoryName = category.Name,
                    .Name = name,
                    .Note = note,
                    .Price = category.Price}
            room.State = 0
            room.CategoryDisplayColor = Colors.Black

            _roomRepository.Add(New Room() _
                                   With {.Id =room.Id,.State=room.State,.Category=category,.Name=room.Name,
                                   .Note=room.Note})
            _unitOfWork.Commit()

            Return room
        End Function
    End Class
End NameSpace