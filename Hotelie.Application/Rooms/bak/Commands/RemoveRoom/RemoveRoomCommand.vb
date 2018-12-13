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

        Public Function Execute(id As String) As String Implements IRemoveRoomCommand.Execute
            Dim room = _roomRepository.GetOne(id)
            If room Is Nothing Then Return "Không tìm thấy phòng có"
            Try
                _roomRepository.Remove(_roomRepository.GetOne(id))
                _unitOfWork.Commit()
            Catch
                Return "Lỗi không thể xác định"
            End Try
            Return String.Empty
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of String) Implements IRemoveRoomCommand.ExecuteAsync
            Dim room = Await _roomRepository.GetOneAsync(id)
            If room Is Nothing Then Return "Không tìm thấy phòng"
            Try
                _roomRepository.Remove(_roomRepository.GetOne(id))
                Await _unitOfWork.CommitAsync()
            Catch
                Return "Lỗi không thể xác định"
            End Try
            Return String.Empty
        End Function
    End Class
End NameSpace