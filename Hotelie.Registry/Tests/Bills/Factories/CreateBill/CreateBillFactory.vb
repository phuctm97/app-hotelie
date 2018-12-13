Imports Hotelie.Application.Bills.Factories.CreateBill

Namespace Tests.Bills.Factories.CreateBill
	Public Class CreateBillFactory
		Implements ICreateBillFactory

		Public Function Execute( payerName As String,
		                         payerAddress As String,
		                         listOfLeaseId As IEnumerable(Of String),
		                         totalExpense As Decimal ) As String Implements ICreateBillFactory.Execute
			Return "1"
		End Function

		Public Async Function ExecuteAsync( payerName As String,
		                                    payerAddress As String,
		                                    listOfLeaseId As IEnumerable(Of String),
		                                    totalExpense As Decimal ) As Task(Of String) _
			Implements ICreateBillFactory.ExecuteAsync
			Return Await Task.Run( Function() Execute( payerName, payerAddress, listOfLeaseId, totalExpense ) )
		End Function
	End Class
End Namespace