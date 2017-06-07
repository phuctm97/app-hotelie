Imports Hotelie.Domain.Bills

Namespace Bills.Models
	Public Class BillModel
		Private ReadOnly _entity As Bill

		Sub New( entity As Bill )
			_entity = entity

			Details = New List(Of BillDetailModel)
			For Each detail As BillDetail In _entity.Details
				Details.Add( New BillDetailModel( detail ) )
			Next
		End Sub

		Public ReadOnly Property Id As String
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property CreateDate As Date
			Get
				If Details.Count = 0 Then Return Today
				Return Details.FirstOrDefault().CheckoutDate
			End Get
		End Property

		Public ReadOnly Property CustomerName As String
			Get
				Return _entity.CustomerName
			End Get
		End Property

		Public ReadOnly Property CustomerAddress As String
			Get
				Return _entity.CustomerAddress
			End Get
		End Property

		Public ReadOnly Property TotalExpense As Decimal
			Get
				Return _entity.TotalExpense
			End Get
		End Property

		Public ReadOnly Property Details As List(Of BillDetailModel)
	End Class
End Namespace
