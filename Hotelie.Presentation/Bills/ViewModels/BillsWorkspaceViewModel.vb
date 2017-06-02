Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Bills.ViewModels
	Public Class BillsWorkspaceViewModel
		Inherits PropertyChangedBase
		Implements IWorkspace

		Private _displayName As String

		Public Property DisplayName As String Implements IHaveDisplayName.DisplayName
			Get
				Return _displayName
			End Get
			Set
				If String.Equals( Value, _displayName ) Then Return
				_displayName = value
				NotifyOfPropertyChange( Function() DisplayName )
			End Set
		End Property

		Public Sub New()
			DisplayName = "Thanh toán"
		End Sub
	End Class
End Namespace
