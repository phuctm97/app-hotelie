Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Infrastructure
Imports Hotelie.Presentation.Leases.ViewModels
Imports Hotelie.Presentation.Start.LoginShell.ViewModels
Imports Hotelie.Presentation.Start.MainWindow.ViewModels
Imports Hotelie.Presentation.Start.WorkspaceShell.ViewModels
Imports Microsoft.Practices.Unity

Namespace Start
	Public Class AppBootstrapper
		Inherits Registry.AppBootstrapper

		Protected Overrides Sub ComposeDependencies()
			MyBase.ComposeDependencies()

			'' Common
			'_container.RegisterType(Of IInventory, Inventory)( New ContainerControlledLifetimeManager() )

			'' Start
			'_container.RegisterType(Of IMainWindow, MainWindowViewModel)( New ContainerControlledLifetimeManager() )
			'_container.RegisterType(Of IShell, LoginShellViewModel)( "login-shell", New TransientLifetimeManager() )
			'_container.RegisterType(Of IShell, WorkspaceShellViewModel)( "workspace-shell", New TransientLifetimeManager() )
		End Sub

		Protected Overrides Sub OnStartup( sender As Object,
		                                   e As StartupEventArgs )
			MyBase.OnStartup( sender, e )

			DisplayRootViewFor(Of ScreenLeasesListViewModel)()
		End Sub
	End Class
End Namespace
