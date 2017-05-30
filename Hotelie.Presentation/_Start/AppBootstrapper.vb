Imports Caliburn.Micro
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Leases.ViewModels
Imports Hotelie.Presentation.Rooms.ViewModels
Imports Hotelie.Presentation.Start.LoginShell.ViewModels
Imports Hotelie.Presentation.Start.MainWindow.ViewModels
Imports Hotelie.Presentation.Start.WorkspaceShell.ViewModels
Imports Microsoft.Practices.Unity

Namespace Start
	Public Class AppBootstrapper
		Inherits BootstrapperBase

		Private _container As IUnityContainer

		Public Sub New()
			Initialize()
		End Sub

		Protected Overrides Sub Configure()
			MyBase.Configure()

			' Dependencies resolution
			_container = New UnityContainer()
			_container.RegisterType(Of IWindowManager, WindowManager)( New ContainerControlledLifetimeManager() )
			_container.RegisterType(Of IEventAggregator, EventAggregator)( New ContainerControlledLifetimeManager() )

			' Start
			_container.RegisterType(Of IMainWindow, MainWindowViewModel)( New ContainerControlledLifetimeManager() )
			_container.RegisterType(Of IShell, LoginShellViewModel)( "login-shell", New TransientLifetimeManager() )
			_container.RegisterType(Of IShell, WorkspaceShellViewModel)( "workspace-shell", New TransientLifetimeManager() )

			' Workspaces
			_container.RegisterType(Of IWorkspace, RoomsWorkspaceViewModel)( "rooms-workspace", New TransientLifetimeManager() )
			_container.RegisterType(Of IWorkspace, LeasesWorkspaceViewModel)( "leases-workspace",
			                                                                  New TransientLifetimeManager() )

			'_container.RegisterType(Of IAuthentication, Authentication)( New ContainerControlledLifetimeManager() )

			'_container.RegisterType(Of IGetRoomsListQuery, ExampleRoomsListItem)( new ContainerControlledLifetimeManager() )
		End Sub

		Protected Overrides Function GetInstance( service As Type,
		                                          key As String ) As Object
			Return _container.Resolve( service, key )
		End Function

		Protected Overrides Function GetAllInstances( service As Type ) As IEnumerable(Of Object)
			Return _container.ResolveAll( service )
		End Function

		Protected Overrides Sub BuildUp( instance As Object )
			_container.BuildUp( instance )
		End Sub

		Protected Overrides Sub OnExit( sender As Object,
		                                e As EventArgs )
			MyBase.OnExit( sender, e )

			_container.Dispose()
		End Sub

		Protected Overrides Sub OnStartup( sender As Object,
		                                   e As StartupEventArgs )
			MyBase.OnStartup( sender, e )

			DisplayRootViewFor(Of IMainWindow)()
		End Sub
	End Class
End Namespace
