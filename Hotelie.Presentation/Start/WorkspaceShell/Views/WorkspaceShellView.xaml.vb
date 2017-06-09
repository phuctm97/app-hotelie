Imports Caliburn.Micro
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls

Namespace Start.WorkspaceShell.Views
	Public Class WorkspaceShellView
		Implements INeedWindowModals

		Private Async Sub OnShellLoaded( sender As Object,
		                                 e As RoutedEventArgs )
			ShowStaticWindowLoadingDialog()
			Await Task.Delay( 200 )

			Await IoC.Get(Of IInventory).ReloadAsync()

			Await Task.Delay( 200 )
			CloseStaticWindowDialog()
		End Sub
	End Class
End Namespace
