Imports Hotelie.Application.Bills.Models

Namespace Bills.Queries
	Public Interface IGetBillQuery
		Function Execute( id As String ) As IBillModel

		Function ExecuteAsync( id As String ) As Task(Of IBillModel)
	End Interface
End Namespace