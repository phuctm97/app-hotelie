Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Commands.RemoveRoomCategory
    Public Class RemoveRoomCategoryCommand
        Implements IRemoveRoomCategoryCommand

        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(roomRepository As IRoomRepository, unitOfWork As IUnitOfWork)
            _roomRepository = roomRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String) As String Implements IRemoveRoomCategoryCommand.Execute
            Try
                Dim roomCat = _roomRepository.GetRoomCategory(id)
                If roomCat Is Nothing Then Return "Không tìm thấy loại phòng"
                _roomRepository.RemoveRoomCategory(roomCat)
                _unitOfWork.Commit()
                Return String.Empty
            Catch 
                Return "Không thể xóa loại phòng"
            End Try
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of String) Implements IRemoveRoomCategoryCommand.ExecuteAsync
            Try
                Dim roomCat = Await _roomRepository.GetRoomCategoryAsync(id)
                If roomCat Is Nothing Then Return "Không tìm thấy loại phòng"
                _roomRepository.RemoveRoomCategory(roomCat)
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không thể xóa loại phòng"
            End Try
        End Function
    End Class
End NameSpace