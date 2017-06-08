Namespace Bills.Models
	Public Interface IBillModel
		ReadOnly Property IdEx As String

		ReadOnly Property Id As String

		ReadOnly Property CreateDate As Date

		ReadOnly Property CustomerName As String

		ReadOnly Property CustomerAddress As String

		ReadOnly Property Details As List(Of IBillDetailModel)

		ReadOnly Property TotalExpense As Decimal
	End Interface
End Namespace