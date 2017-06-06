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
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Rooms
Imports Hotelie.Persistence.Users
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
        _container.RegisterType(Of IUserRepository, UserRepository)(New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IAuthentication, Authentication)( New ContainerControlledLifetimeManager() )
        _container.RegisterType(Of IDatabaseService, DatabaseService)(New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IRoomRepository, RoomRepository)(New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IGetRoomsListQuery, GetRoomsListQuery)(New ContainerControlledLifetimeManager())
        _container.RegisterType _
            (Of IGetRoomCategoriesListQuery, GetRoomCategoriesListQuery)(New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IUpdateRoomCommand, UpdateRoomCommand)(New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IRemoveRoomCommand, RemoveRoomCommand)(New ContainerControlledLifetimeManager())
        _container.RegisterType(Of ICreateRoomFactory, CreateRoomFactory)(New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IUnitOfWork, UnitOfWork)(New ContainerControlledLifetimeManager)

        _container.RegisterType(Of IGetLeasesListQuery, GetLeasesListQuery)(New ContainerControlledLifetimeManager())
        _container.RegisterType(Of ILeaseRepository, LeaseRepository)(New ContainerControlledLifetimeManager())
    End Sub

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
