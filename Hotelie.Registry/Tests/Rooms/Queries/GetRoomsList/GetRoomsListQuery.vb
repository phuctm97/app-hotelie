Imports System.Windows.Media
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Tests.Rooms.Queries.GetRoomsList
	Public Class GetRoomsListQuery
		Implements IGetRoomsListQuery

		Public Function Execute() As IEnumerable(Of RoomModel) Implements IGetRoomsListQuery.Execute
			Return RoomsTest.Rooms
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomModel)) Implements IGetRoomsListQuery.ExecuteAsync
			Return Await Task.Run(Function() Execute())
		End Function
	End Class
End Namespace
