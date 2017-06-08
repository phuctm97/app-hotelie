Imports Hotelie.Application.Bills.Models

Namespace Bills.Queries
	Public Interface IGetAllBillsQuery
		Function Execute() As IEnumerable(Of IBillModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of IBillModel))
	End Interface
End Namespace