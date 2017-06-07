Namespace Bills.Factories.CreateBill
	Public Interface ICreateBillFactory
		Function Execute( payerName As String,
		                  payerAddress As String,
		                  listOfLeaseId As IEnumerable(Of String),
		                  totalExpense As Decimal ) As String

		Function ExecuteAsync( payerName As String,
		                       payerAddress As String,
		                       listOfLeaseId As IEnumerable(Of String),
		                       totalExpense As Decimal ) As Task(Of String)
	End Interface
End Namespace