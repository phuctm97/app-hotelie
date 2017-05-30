Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Leases.ViewModels
	Public Class LeasesWorkspaceViewModel
		Implements IWorkspace

		Public Property DisplayName As String Implements IHaveDisplayName.DisplayName

		Public Sub New()
			DisplayName = "Thuê phòng"
		End Sub
	End Class
End Namespace
