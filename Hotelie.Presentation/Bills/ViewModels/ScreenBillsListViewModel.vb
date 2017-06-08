Imports Caliburn.Micro
Imports Hotelie.Application.Bills.Models
Imports Hotelie.Application.Bills.Queries
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Common.Infrastructure

Namespace Bills.ViewModels
	Public Class ScreenBillsListViewModel
		Implements IBillsListPresenter,
		           INeedWindowModals

		Public Const MaxDisplayCapacity As Integer = 30

		' Dependencies
		Private ReadOnly _getAllBillsQuery As IGetAllBillsQuery

		' Binding models
		Public ReadOnly Property Bills As IObservableCollection(Of IBillModel)

		Public Sub New( workspace As BillsWorkspaceViewModel,
		                getAllBillsQuery As IGetAllBillsQuery )
			_getAllBillsQuery = getAllBillsQuery
			RegisterInventory()

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

		Public Sub OnBillAdded( model As IBillModel ) Implements IBillsListPresenter.OnBillAdded
			Dim bill = Bills.FirstOrDefault( Function( r ) r.Id = model.Id )
			If bill IsNot Nothing
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Warning,
				                              "Tìm thấy hóa đơn cùng id trong danh sách" )
				Bills.Remove( bill )
			End If

			Bills.Add( model )
		End Sub

		Public Sub OnBillRemoved( id As String ) Implements IBillsListPresenter.OnBillRemoved
			Dim bill = Bills.FirstOrDefault( Function( r ) r.Id = id )
			If bill Is Nothing Then Return

			Bills.Remove( bill )
		End Sub
	End Class
End Namespace
