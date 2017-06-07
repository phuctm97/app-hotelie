Imports Caliburn.Micro
Imports Hotelie.Application.Bills.Models

Namespace Bills.ViewModels
	Public Class ScreenBillsListViewModel
		' Binding models
		Public ReadOnly Property Bills As IObservableCollection(Of BillModel)

		Public Sub New()
			Bills = New BindableCollection(Of BillModel)
		End Sub

		Public Sub Init()

		End Sub

	End Class
End Namespace
