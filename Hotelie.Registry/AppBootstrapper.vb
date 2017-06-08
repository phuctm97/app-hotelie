Imports Caliburn.Micro
Imports Hotelie.Application.Bills.Commands.RemoveBill
Imports Hotelie.Application.Bills.Factories
Imports Hotelie.Application.Bills.Queries
Imports Hotelie.Application.Leases.Commands
Imports Hotelie.Application.Leases.Factories
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Rooms.Commands
Imports Hotelie.Application.Rooms.Factories
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Persistence.Bills
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

	    _container.RegisterType(Of IUnitOfWork, UnitOfWork)(
	        New ContainerControlledLifetimeManager() )

	    _container.RegisterType(Of IUserRepository, UserRepository)(
	        New ContainerControlledLifetimeManager() )
	    _container.RegisterType(Of IRoomRepository, RoomRepository)(
	        New ContainerControlledLifetimeManager() )
	    _container.RegisterType(Of ILeaseRepository, LeaseRepository)(
	        New ContainerControlledLifetimeManager() )
	    _container.RegisterType(Of IBillRepository, BillRepository)(
            New ContainerControlledLifetimeManager)
	    _container.RegisterType(Of IParameterRepository, ParameterRepository)(
	        New ContainerControlledLifetimeManager() )

        _container.RegisterType(Of IGetAllRoomsQuery, GetAllRoomsQuery)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IGetRoomQuery, GetRooomQuery)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IGetAllRoomCategoriesQuery, GetAllRoomCategoriesQuery)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IGetRoomCategoryQuery, GetRoomCategoryQuery)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IGetAllLeasesQuery, GetAllLeasesQuery)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IGetLeaseQuery, GetLeaseQuery)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IGetAllCustomerCategoriesQuery, GetAllCustomerCategoriesQuery)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IGetAllBillsQuery, GetAllBillsQuery)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IGetBillQuery, GetBillQuery)(
            New ContainerControlledLifetimeManager())

        _container.RegisterType(Of IRemoveRoomCategoryCommand, RemoveRoomCategoryCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IRemoveRoomCommand, RemoveRoomCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IUpdateRoomCategoryCommand, UpdateRoomCategoryCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IUpdateRoomCommand, UpdateRoomCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IRemoveCustomerCategoryCommand, RemoveCustomerCategoryCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IRemoveLeaseCommand, RemoveLeaseCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IRemoveLeaseDetailCommand, RemoveLeaseDetailCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IUpdateCustomerCategoryCommand, UpdateCustomerCategoryCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IUpdateLeaseCommand, UpdateLeaseCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IUpdateLeaseDetailCommand, UpdateLeaseDetailCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of IRemoveBillCommand, RemoveBillCommand)(
            New ContainerControlledLifetimeManager())

        _container.RegisterType(Of ICreateRoomCategoryCommand, CreateRoomCategoryCommand)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of ICreateRoomFactory, CreateRoomFactory)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of ICreateCustomerCategoryFactory, CreateCustomerCategoryFactory)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of ICreateLeaseDetailFactory, CreateLeaseDetailFactory)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of ICreateLeaseFactory, CreateLeaseFactory)(
            New ContainerControlledLifetimeManager())
        _container.RegisterType(Of ICreateBillFactory, CreateBillFactory)(
            New ContainerControlledLifetimeManager())
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
