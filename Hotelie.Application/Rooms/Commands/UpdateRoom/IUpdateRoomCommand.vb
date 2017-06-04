Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Commands.UpdateRoom
	Public Interface IUpdateRoomCommand
		Sub Execute( id As String,
		             name As String,
		             categoryId As String,
		             note As String,
                     state As Integer)

		Function ExecuteAsync( id As String,
		             name As String,
		             categoryId As String,
		             note As String,
		             state As Integer) As Task(Of Integer)
	End Interface
End Namespace
