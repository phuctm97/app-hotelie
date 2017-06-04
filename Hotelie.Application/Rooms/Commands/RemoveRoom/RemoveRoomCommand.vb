Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Commands.RemoveRoom
    Public Class RemoveRoomCommand
        Implements IRemoveRoomCommand

        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(roomRepository As IRoomRepository, unitOfWork As IUnitOfWork)
            _roomRepository = roomRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Sub Execute(id As String) Implements IRemoveRoomCommand.Execute
             _roomRepository.Remove(_roomRepository.GetOne(id))
            _unitOfWork.Commit()
        End Sub

        Public Async Sub ExecuteAsync(id As String) Implements IRemoveRoomCommand.ExecuteAsync
            Dim room = Await _roomRepository.GetOneAsync(id)
            _roomRepository.Remove(room)
            _unitOfWork.CommitAsync()
        End Sub
    End Class
End NameSpace