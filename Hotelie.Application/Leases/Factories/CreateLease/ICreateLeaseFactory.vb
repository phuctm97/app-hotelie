Namespace Leases.Factories.CreateLease
	Public Interface ICreateLeaseFactory
		Function Execute( roomId As String,
		                  checkinDate As Date,
		                  expectedCheckoutDate As Date,
		                  details As IEnumerable(Of CreateLeaseDetailModel) ) As String

		Function ExecuteAsync( roomId As String,
		                       checkinDate As Date,
		                       expectedCheckoutDate As Date,
		                       details As IEnumerable(Of CreateLeaseDetailModel) ) As Task(Of String)
	End Interface
End Namespace