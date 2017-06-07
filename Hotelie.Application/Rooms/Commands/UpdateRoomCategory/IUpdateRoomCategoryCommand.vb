Namespace Rooms.Commands.UpdateRoomCategory
    Public Interface IUpdateRoomCategoryCommand
        Function Execute(id As String, name As String, price As String) As String
        Function ExecuteAsync(id As String, name As String, price As String) As Task(Of String)
    End Interface
End NameSpace