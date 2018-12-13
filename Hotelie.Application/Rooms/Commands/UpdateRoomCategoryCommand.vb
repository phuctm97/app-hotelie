Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Commands
    Public Class UpdateRoomCategoryCommand
        Implements IUpdateRoomCategoryCommand

        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(roomRepository As IRoomRepository, unitOfWork As IUnitOfWork)
            _roomRepository = roomRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(id As String, name As String, unitPrice As Decimal) As String Implements IUpdateRoomCategoryCommand.Execute
            Dim roomCat = _roomRepository.GetRoomCategory(id)
            If IsNothing(roomCat) Then Return "Không tìm thấy loại phòng cần chỉnh sửa"

            roomCat.Name = name
            roomCat.Price = unitPrice

            _unitOfWork.Commit()
            Return String.Empty
        End Function


        Public Async Function ExecuteAsync(id As String, name As String, unitPrice As Decimal) As Task(Of String) Implements IUpdateRoomCategoryCommand.ExecuteAsync
            Dim roomCat = Await _roomRepository.GetRoomCategoryAsync(id)
            If IsNothing(roomCat) Then Return "Không tìm thấy loại phòng cần chỉnh sửa"

            roomCat.Name = name
            roomCat.Price = unitPrice

            Await _unitOfWork.CommitAsync()
            Return String.Empty
        End Function
    End Class
End NameSpace