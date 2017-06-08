Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Reports.Models

Namespace Reports.ViewModels
	Public Class ReportsWorkspaceViewModel
		Inherits AppScreen

		Public ReadOnly Property Model As ReportsModel

		Public Sub New()
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )
			DisplayName = "Báo cáo - Thống kê"

			Model = new ReportsModel()
			Model.CreateDate = Today

			Model.RevenueModels.Add( New RevenueModel With {.Name="Phòng thường", .Revenue=50000000, .Ratio=20} )
			Model.RevenueModels.Add( New RevenueModel With {.Name="Phòng Vip", .Revenue=80000000, .Ratio=20} )
			Model.RevenueModels.Add( New RevenueModel With {.Name="Phòng thường 2", .Revenue=60000000, .Ratio=30} )
			Model.RevenueModels.Add( New RevenueModel With {.Name="Phòng Vip 2", .Revenue=70000000, .Ratio=30} )

			Model.FigureModels.Add( New FigureModel With {.Name="Phòng thường", .Figure=30, .Ratio=25} )
			Model.FigureModels.Add( New FigureModel With {.Name="Phòng thường", .Figure=30, .Ratio=25} )
			Model.FigureModels.Add( New FigureModel With {.Name="Phòng thường", .Figure=30, .Ratio=25} )
			Model.FigureModels.Add( New FigureModel With {.Name="Phòng thường", .Figure=30, .Ratio=25} )
		End Sub
	End Class
End Namespace