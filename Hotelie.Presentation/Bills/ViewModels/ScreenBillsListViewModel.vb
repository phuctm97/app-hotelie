Imports Caliburn.Micro
Imports Hotelie.Application.Bills.Models
Imports Hotelie.Application.Bills.Queries
Imports Hotelie.Presentation.Common.Controls

Namespace Bills.ViewModels
	Public Class ScreenBillsListViewModel
		Implements INeedWindowModals

		Public Const MaxDisplayCapacity As Integer = 30

		' Dependencies
		Private ReadOnly _getAllBillsQuery As IGetAllBillsQuery

		' Binding models
		Public ReadOnly Property Bills As IObservableCollection(Of IBillModel)

		Public Sub New( workspace As BillsWorkspaceViewModel,
		                getAllBillsQuery As IGetAllBillsQuery )
			_getAllBillsQuery = getAllBillsQuery

			Bills = New BindableCollection(Of IBillModel)
		End Sub

		Public Sub Init()
			Bills.Clear()
			Bills.AddRange(
				_getAllBillsQuery.Execute().
				              OrderByDescending( Function( b ) b.CreateDate ).
				              Take( MaxDisplayCapacity ) )
		End Sub

		Public Async Function InitAsync() As Task
			Bills.Clear()
			Bills.AddRange(
				(Await _getAllBillsQuery.ExecuteAsync()).
				              OrderByDescending( Function( b ) b.CreateDate ).
				              Take( MaxDisplayCapacity ) )
		End Function
	End Class
End Namespace
