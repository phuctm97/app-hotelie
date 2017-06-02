Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Commands.UpdateRoom
    Public Class UpdateRoomCommand
        Implements IUpdateRoomCommand

        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Public Sub New(roomRepository As IRoomRepository, unitOfWork As IUnitOfWork)
            _roomRepository = roomRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Sub Execute(id As String, name As String, categoryId As String, note As String, state As Integer) _
            Implements IUpdateRoomCommand.Execute
            Dim room = _roomRepository.GetOne(id)
            room.Name = name
            room.Note = note
            room.State = state

            Dim category = _roomRepository.GetRoomCategory(categoryId)
            room.Category = category

            _unitOfWork.Commit()
        End Sub

       
    End Class
End NameSpace