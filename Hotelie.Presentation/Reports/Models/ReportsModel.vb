Imports Caliburn.Micro

Namespace Reports.Models
	Public Class ReportsModel
		Inherits PropertyChangedBase

		Private _createDate As Date
		Private _beginDate As Date
		Private _endDate As Date

		Public Property CreateDate As Date
			Get
				Return _createDate
			End Get
			Set
				If Equals( Value, _createDate ) Then Return
				_createDate = value
				NotifyOfPropertyChange( Function() CreateDate )
			End Set
		End Property

		Public Property BeginDate As Date
			Get
				Return _beginDate
			End Get
			Set
				If Equals( Value, _beginDate ) Then Return
				_beginDate = value
				NotifyOfPropertyChange( Function() BeginDate )
			End Set
		End Property

		Public Property EndDate As Date
			Get
				Return _endDate
			End Get
			Set
				If Equals( Value, _endDate ) Then Return
				_endDate = value
				NotifyOfPropertyChange( Function() EndDate )
			End Set
		End Property

		Public Property RevenueModels As IObservableCollection(Of RevenueModel)

		Public Property FigureModels As IObservableCollection(Of FigureModel)

		Public Sub New()
			CreateDate = Today
			BeginDate = Today
			EndDate = Today
			RevenueModels = new BindableCollection(Of RevenueModel)
			FigureModels = new BindableCollection(Of FigureModel)
		End Sub
	End Class
End Namespace