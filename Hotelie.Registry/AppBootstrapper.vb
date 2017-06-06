Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Rooms.Factories.CreateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoryData
Imports Hotelie.Application.Rooms.Queries.GetRoomData
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Application.Services.Persistence
Imports Microsoft.Practices.Unity

Public Class AppBootstrapper
	Inherits BootstrapperBase

	Protected ReadOnly _container As IUnityContainer

	Protected Sub New()
		_container = New UnityContainer()

		Initialize()
	End Sub

	Protected Overrides Sub Configure()
		MyBase.Configure()

		ComposeDependencies()
	End Sub

	Protected Overridable Sub ComposeDependencies()
		_container.RegisterType(Of IWindowManager, WindowManager)( New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IEventAggregator, EventAggregator)( New ContainerControlledLifetimeManager() )

		_container.RegisterType(Of IDatabaseService, Tests.Services.Persistence.DatabaseService)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IAuthentication, Tests.Services.Authentication.Authentication)(
			New ContainerControlledLifetimeManager() )

		_container.RegisterType(Of IGetRoomDataQuery, Tests.Rooms.Queries.GetRoomData.GetRoomDataQuery)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType _
			(Of IGetRoomCategoryDataQuery, Tests.Rooms.Queries.GetRoomCategoryData.GetRoomCategoryDataQuery)(
				New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IGetRoomsListQuery, Tests.Rooms.Queries.GetRoomsList.GetRoomsListQuery)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IGetSimpleRoomsListQuery, Tests.Rooms.Queries.GetSimpleRoomsList.GetSimpleRoomsListQuery)(
			New ContainerControlledLifetimeManager() )

		_container.RegisterType _
			(Of IGetRoomCategoriesListQuery, Tests.Rooms.Queries.GetRoomCategoriesList.GetRoomCategoriesListQuery)(
				New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IUpdateRoomCommand, Tests.Rooms.Commands.UpdateRoom.UpdateRoomCommand)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IRemoveRoomCommand, Tests.Rooms.Commands.RemoveRoom.RemoveRoomCommand)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of ICreateRoomFactory, Tests.Rooms.Factories.CreateRoom.CreateRoomFactory)(
			New ContainerControlledLifetimeManager() )
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
End Class
