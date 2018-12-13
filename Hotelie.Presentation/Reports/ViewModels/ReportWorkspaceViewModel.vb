Imports System.ComponentModel
Imports Hotelie.Application.Reports
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Reports.Models

Namespace Reports.ViewModels
	Public Class ReportsWorkspaceViewModel
		Inherits AppScreen
		Implements INeedWindowModals

		Private ReadOnly _getRoomUsedDaysReport As IGetRoomUsedDaysReport
		Private ReadOnly _getRoomCategoriesProfit As IGetRoomCategoriesProfit
		Private _updating = False

		Public ReadOnly Property Model As ReportsModel


		Public Sub New( getRoomUsedDaysReport As IGetRoomUsedDaysReport,
		                getRoomCategoriesProfit As IGetRoomCategoriesProfit )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )
			_getRoomUsedDaysReport = getRoomUsedDaysReport
			_getRoomCategoriesProfit = getRoomCategoriesProfit

			DisplayName = "Báo cáo - Thống kê"
			Model = new ReportsModel()

			Init()
		End Sub

		Public Sub Init()
			Model.CreateDate = Today
			AddHandler Model.PropertyChanged, AddressOf OnModelUpdated
			_updating = False
		End Sub

		Private Async Sub OnModelUpdated( sender As Object,
		                                  e As PropertyChangedEventArgs )
			If _updating Then Return

			If String.Equals( e.PropertyName, "BeginDate" ) Or String.Equals( e.PropertyName, "EndDate" )
				_updating = True
				ShowStaticWindowLoadingDialog()

				Model.RevenueModels.Clear()
				Model.RevenueModels.AddRange(
					(Await _getRoomCategoriesProfit.ExecuteAsync( Model.BeginDate, Model.EndDate )).
					                            Select( Function( p ) New RevenueModel With {
						                                  .Name=p.Catetogry, .Revenue=p.Profit} ) )
				Dim sum As Decimal = 0
				For Each revenueModel As RevenueModel In Model.RevenueModels
					sum += revenueModel.Revenue
				Next
				For Each revenueModel As RevenueModel In Model.RevenueModels
					if sum = 0
						revenueModel.Ratio = 0
						Continue For
					End If
					revenueModel.Ratio = revenueModel.Revenue / sum
				Next

				Model.FigureModels.Clear()
				Model.FigureModels.AddRange(
					(Await _getRoomUsedDaysReport.ExecuteAsync( Model.BeginDate, Model.EndDate )).
					                           Select( Function( p ) New FigureModel With {
						                                 .Name=p.Room, .Figure=p.UsedDays} ) )

				Dim sum2 As Long = 0
				For Each figureModel As FigureModel In Model.FigureModels
					sum2 += figureModel.Figure
				Next
				For Each figureModel As FigureModel In Model.FigureModels
					if sum2 = 0
						figureModel.Ratio = 0
						Continue For
					End If
					figureModel.Ratio = figureModel.Figure / sum2
				Next

				CloseStaticWindowDialog()
			_updating = False
			End If
		End Sub
	End Class
End Namespace