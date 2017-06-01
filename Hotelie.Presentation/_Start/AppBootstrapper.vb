Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Leases.ViewModels
Imports Hotelie.Presentation.Rooms.ViewModels
Imports Hotelie.Presentation.Start.LoginShell.ViewModels
Imports Hotelie.Presentation.Start.MainWindow.ViewModels
Imports Hotelie.Presentation.Start.WorkspaceShell.ViewModels
Imports Microsoft.Practices.Unity

Namespace Start
	Public Class AppBootstrapper
		Inherits Registry.AppBootstrapper

		Protected Overrides Sub ComposeDependencies()
			MyBase.ComposeDependencies()

			' Start
			_container.RegisterType(Of IMainWindow, MainWindowViewModel)( New ContainerControlledLifetimeManager() )
			_container.RegisterType(Of IShell, LoginShellViewModel)( "login-shell", New TransientLifetimeManager() )
			_container.RegisterType(Of IShell, WorkspaceShellViewModel)( "workspace-shell", New TransientLifetimeManager() )

			' Workspaces
			_container.RegisterType(Of IWorkspace, RoomsWorkspaceViewModel)( "rooms-workspace", New TransientLifetimeManager() )
			_container.RegisterType(Of IWorkspace, LeasesWorkspaceViewModel)( "leases-workspace",
			                                                                  New TransientLifetimeManager() )
		End Sub

		Protected Overrides Sub OnStartup( sender As Object,
		                                   e As StartupEventArgs )
			MyBase.OnStartup( sender, e )

			DisplayRootViewFor(Of IMainWindow)()
		End Sub
	End Class
End Namespace
