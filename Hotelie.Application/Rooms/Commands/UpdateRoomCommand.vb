Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Commands
    Public Class UpdateRoomCommand
        Implements IUpdateRoomCommand

        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Public Sub New(roomRepository As IRoomRepository, unitOfWork As IUnitOfWork)
            _roomRepository = roomRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String, name As String, categoryId As String, note As String, state As Integer) _
            As String _
            Implements IUpdateRoomCommand.Execute
            Dim room = _roomRepository.GetOne(id)
            If IsNothing(room) Then Return "Không tìm thấy phòng"

            room.Name = name
            room.Note = note
            room.State = state

            Try
                Dim category = _roomRepository.GetRoomCategory(categoryId)
                room.Category = category

                _unitOfWork.Commit()
            Catch
                Return "Lỗi không xác định"
            End Try

            Return String.Empty
        End Function

        Public Async Function ExecuteAsync(id As String, name As String, categoryId As String, note As String,
                                           state As Integer) As Task(Of String) _
            Implements IUpdateRoomCommand.ExecuteAsync
            Dim room = Await _roomRepository.GetOneAsync(id)
            If IsNothing(room) Then Return "Không tìm thấy phòng"

            room.Name = name
            room.Note = note
            room.State = state

            
                Dim category = Await _roomRepository.GetRoomCategoryAsync(categoryId)
                If IsNothing(category) Then Return "Không tìm thấy loại phòng"

                room.Category = category
            Try
                Await _unitOfWork.CommitAsync()
            Catch
                Return "Lỗi không xác định"
            End Try

            Return String.Empty
        End Function
    End Class
End NameSpace