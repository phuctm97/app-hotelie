Namespace Rooms.Factories.CreateRoomCategory
    Public Interface ICreateRoomCategoryFactory
        Function Execute (name As String, price As Double) As String
        Function ExecuteAsync (name As String, price As Double) As Task(Of String)
    End Interface
End NameSpace