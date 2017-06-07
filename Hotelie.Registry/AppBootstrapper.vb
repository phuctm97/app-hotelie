Imports Caliburn.Micro
Imports Hotelie.Application.Bills.Factories
Imports Hotelie.Application.Bills.Factories.CreateBill
Imports Hotelie.Application.Leases.Commands.RemoveLease
Imports Hotelie.Application.Leases.Commands.RemoveLeaseDetail
Imports Hotelie.Application.Leases.Commands.UpdateLease
Imports Hotelie.Application.Leases.Commands.UpdateLeaseDetail
Imports Hotelie.Application.Leases.Factories.CreateLease
Imports Hotelie.Application.Leases.Factories.CreateLeaseDetail
Imports Hotelie.Application.Leases.Queries.GetCustomerCategoriesList
Imports Hotelie.Application.Leases.Queries.GetLeaseData
Imports Hotelie.Application.Leases.Queries.GetLeasesList
Imports Hotelie.Application.Leases.Queries.GetSimpleLeasesList
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Rooms.Factories.CreateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomData
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Parameters
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

		_container.RegisterType(Of IAuthentication, Authentication)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IDatabaseService, DatabaseService)(
			New ContainerControlledLifetimeManager() )

		_container.RegisterType(Of IGetRoomDataQuery, GetRoomDataQuery)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IGetRoomsListQuery, GetRoomsListQuery)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IGetSimpleRoomsListQuery, GetSimpleRoomsListQuery)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType _
			(Of IGetRoomCategoriesListQuery, GetRoomCategoriesListQuery)(
				New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IUpdateRoomCommand, UpdateRoomCommand)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IRemoveRoomCommand, RemoveRoomCommand)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of ICreateRoomFactory, CreateRoomFactory)(
			New ContainerControlledLifetimeManager() )

		_container.RegisterType(Of IGetLeasesListQuery, GetLeasesListQuery)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IGetLeaseDataQuery, GetLeaseDataQuery)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType _
			(Of IGetCustomerCategoriesListQuery, GetCustomerCategoriesListQuery)(
				New ContainerControlledLifetimeManager() )
		_container.RegisterType _
			(Of IUpdateLeaseDetailCommand, UpdateLeaseDetailCommand)(
				New ContainerControlledLifetimeManager() )
		_container.RegisterType _
			(Of IRemoveLeaseDetailCommand, RemoveLeaseDetailCommand)(
				New ContainerControlledLifetimeManager() )
		_container.RegisterType _
			(Of ICreateLeaseDetailFactory, CreateLeaseDetailFactory)(
				New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IUpdateLeaseCommand,UpdateLeaseCommand)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of IRemoveLeaseCommand, RemoveLeaseCommand)(
			New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of ICreateLeaseFactory, CreateLeaseFactory)(
			New ContainerControlledLifetimeManager() )
	    _container.RegisterType(Of ILeaseRepository, LeaseRepository)(
	        New ContainerControlledLifetimeManager() )
	    _container.RegisterType(Of IRoomRepository, RoomRepository)(
	        New ContainerControlledLifetimeManager() )
	    _container.RegisterType(Of IUserRepository, UserRepository)(
	        New ContainerControlledLifetimeManager() )
	    _container.RegisterType(Of IUnitOfWork, UnitOfWork)(
	        New ContainerControlledLifetimeManager() )
	    _container.RegisterType(Of IParameterRepository, ParameterRepository)(
	        New ContainerControlledLifetimeManager() )
		_container.RegisterType _
			(Of IGetSimpleLeasesListQuery, GetSimpleLeasesListQuery)(
				New ContainerControlledLifetimeManager() )
		_container.RegisterType(Of ICreateBillFactory, CreateBillFactory)(
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
