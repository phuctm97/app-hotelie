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

		' Initializations
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

		' Infrastructure
		Public Sub OnBillAdded( model As IBillModel ) Implements IBillsListPresenter.OnBillAdded
			If IsNothing( model ) OrElse String.IsNullOrEmpty( model.Id ) Then Return

			Dim bill = Bills.FirstOrDefault( Function( b ) b.Id = model.Id )
			If bill IsNot Nothing
				ShowStaticTopNotification( Start.MainWindow.Models.StaticNotificationType.Warning,
				                           $"Tìm thấy hóa đơn cùng mã {model.IdEx} trong danh sách" )
				Bills( Bills.IndexOf( bill ) ) = model
			Else
				Bills.Add( model )
			End If
		End Sub

		Public Sub OnBillRemoved( id As String ) Implements IBillsListPresenter.OnBillRemoved
			Dim bill = Bills.FirstOrDefault( Function( r ) r.Id = id )
			If bill Is Nothing Then Return

			Bills.Remove( bill )
		End Sub
	End Class
End Namespace
