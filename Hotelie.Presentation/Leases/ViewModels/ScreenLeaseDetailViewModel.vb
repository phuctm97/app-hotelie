Imports System.Collections.Specialized
Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Commands.RemoveLease
Imports Hotelie.Application.Leases.Commands.RemoveLeaseDetail
Imports Hotelie.Application.Leases.Commands.UpdateLease
Imports Hotelie.Application.Leases.Commands.UpdateLeaseDetail
Imports Hotelie.Application.Leases.Factories.CreateLeaseDetail
Imports Hotelie.Application.Leases.Queries.GetCustomerCategoriesList
Imports Hotelie.Application.Leases.Queries.GetLeaseData
Imports Hotelie.Application.Leases.Queries.GetLeaseDetailData
Imports Hotelie.Application.Rooms.Queries.GetRoomData
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Infrastructure
Imports Hotelie.Presentation.Leases.Models
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Leases.ViewModels
	Public Class ScreenLeaseDetailViewModel
		Inherits AppScreenHasSavingAndDeleting
		Implements IChild(Of LeasesWorkspaceViewModel)
		Implements INeedWindowModals
		Implements IRoomsListPresenter

		' Dependencies
		Private ReadOnly _getLeaseDataQuery As IGetLeaseDataQuery
		Private ReadOnly _getSimpleRoomsListQuery As IGetSimpleRoomsListQuery
		Private ReadOnly _getCustomerCategoriesListQuery As IGetCustomerCategoriesListQuery
		Private ReadOnly _updateLeaseCommand As IUpdateLeaseCommand
		Private ReadOnly _removeLeaseCommmand As IRemoveLeaseCommand
		Private ReadOnly _updateLeaseDetailCommand As IUpdateLeaseDetailCommand
		Private ReadOnly _removeLeaseDetailCommand As IRemoveLeaseDetailCommand
		Private ReadOnly _createLeaseDetailFactory As ICreateLeaseDetailFactory
		Private ReadOnly _inventory As IInventory

		' Backing fields
		Private _leaseId As String
		Private _roomUnitPrice As Decimal
		Private _checkinDate As Date
		Private _room As SimpleRoomsListItemModel
		Private _expectedCheckoutDate As Date

		Private _originalroomUnitPrice As Decimal
		Private _originalcheckinDate As Date
		Private _originalroomId As String
		Private _originalexpectedCheckoutDate As Date
		Private _originalDetails As IObservableCollection(Of EditableLeaseDetailModel)
		Private _maxNumberOfUsers As Integer

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As LeasesWorkspaceViewModel Implements IChild(Of LeasesWorkspaceViewModel).Parent
			Get
				Return CType(Parent, LeasesWorkspaceViewModel)
			End Get
			Set
				Parent = value
			End Set
		End Property

		' Binding models
		Public Property LeaseId As String
			Get
				Return _leaseId
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _leaseId ) Then Return
				_leaseId = value
				NotifyOfPropertyChange( Function() LeaseId )
			End Set
		End Property

		Public Property RoomUnitPrice As Decimal
			Get
				Return _roomUnitPrice
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _roomUnitPrice ) Then Return
				_roomUnitPrice = value
				NotifyOfPropertyChange( Function() RoomUnitPrice )
			End Set
		End Property

		Public Property CheckinDate As Date
			Get
				Return _checkinDate
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _checkinDate ) Then Return
				_checkinDate = value
				NotifyOfPropertyChange( Function() CheckinDate )
				NotifyOfPropertyChange( Function() NumberOfUsedDays )
			End Set
		End Property

		Public ReadOnly Property NumberOfUsedDays As Integer
			Get
				Return (Today - CheckinDate).TotalDays
			End Get
		End Property

		Public Property Room As SimpleRoomsListItemModel
			Get
				Return _room
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _room ) Then Return
				_room = value
				_roomUnitPrice = _room.UnitPrice
				NotifyOfPropertyChange( Function() Room )
				NotifyOfPropertyChange( Function() RoomUnitPrice )
			End Set
		End Property

		Public Property ExpectedCheckoutDate As Date
			Get
				Return _expectedCheckoutDate
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _expectedCheckoutDate ) Then Return
				_expectedCheckoutDate = value
				NotifyOfPropertyChange( Function() ExpectedCheckoutDate )
			End Set
		End Property

		Public ReadOnly Property Details As IObservableCollection(Of EditableLeaseDetailModel)

		Public ReadOnly Property CanAddDetail As Boolean
			Get
				Return IsNothing( _details ) OrElse _details.Count < _maxNumberOfUsers
			End Get
		End Property

		Public ReadOnly Property CanDeleteDetail As Boolean
			Get
				Return IsNothing( _details ) OrElse _details.Count > 1
			End Get
		End Property

		' Binding data
		Public ReadOnly Property Rooms As IObservableCollection(Of SimpleRoomsListItemModel)

		Public Shared ReadOnly Property CustomerCategories As IObservableCollection(Of CustomerCategoriesListItemModel)

		' Initialization

		Shared Sub New()
			CustomerCategories = New BindableCollection(Of CustomerCategoriesListItemModel)
		End Sub

		Public Sub New( workspace As LeasesWorkspaceViewModel,
		                getLeaseDataQuery As IGetLeaseDataQuery,
		                getSimpleRoomsListQuery As IGetSimpleRoomsListQuery,
		                getCustomerCategoriesListQuery As IGetCustomerCategoriesListQuery,
		                updateLeaseCommand As IUpdateLeaseCommand,
		                removeLeaseCommmand As IRemoveLeaseCommand,
		                updateLeaseDetailCommand As IUpdateLeaseDetailCommand,
		                removeLeaseDetailCommand As IRemoveLeaseDetailCommand,
		                createLeaseDetailFactory As ICreateLeaseDetailFactory,
		                inventory As IInventory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ParentWorkspace = workspace
			_getLeaseDataQuery = getLeaseDataQuery
			_getSimpleRoomsListQuery = getSimpleRoomsListQuery
			_getCustomerCategoriesListQuery = getCustomerCategoriesListQuery
			_updateLeaseCommand = updateLeaseCommand
			_removeLeaseCommmand = removeLeaseCommmand
			_updateLeaseDetailCommand = updateLeaseDetailCommand
			_removeLeaseDetailCommand = removeLeaseDetailCommand
			_createLeaseDetailFactory = createLeaseDetailFactory
			_inventory = inventory
			RegisterInventory()

			Rooms = New BindableCollection(Of SimpleRoomsListItemModel)
			Details = New BindableCollection(Of EditableLeaseDetailModel)
			AddHandler Details.CollectionChanged, AddressOf OnDetailsUpdated
		End Sub

		Private Sub OnDetailsUpdated( sender As Object,
		                              e As NotifyCollectionChangedEventArgs )
			NotifyOfPropertyChange( Function() CanAddDetail )
			NotifyOfPropertyChange( Function() CanDeleteDetail )
		End Sub

		Public Sub Init()
			Rooms.Clear()
			Rooms.AddRange( _getSimpleRoomsListQuery.Execute() )

			CustomerCategories.Clear()
			CustomerCategories.AddRange( _getCustomerCategoriesListQuery.Execute() )
			InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Rooms.Clear()
			Rooms.AddRange( (Await _getSimpleRoomsListQuery.ExecuteAsync()) )

			CustomerCategories.Clear()
			CustomerCategories.AddRange( Await _getCustomerCategoriesListQuery.ExecuteAsync() )
			InitValues()
		End Function

		Private Sub InitValues()
			_maxNumberOfUsers = 4
			LeaseId = String.Empty
			Room = Rooms.FirstOrDefault()
			RoomUnitPrice = 0
			CheckinDate = Today
			ExpectedCheckoutDate = Today

			_originalcheckinDate = CheckinDate
			_originalexpectedCheckoutDate = ExpectedCheckoutDate
			_originalroomUnitPrice = RoomUnitPrice
			_originalroomId = String.Empty
			_originalDetails = New BindableCollection(Of EditableLeaseDetailModel)
		End Sub

		Private Sub ResetValues()
			LeaseId = String.Empty
			RoomUnitPrice = 0
			Room = Rooms.FirstOrDefault()
			CheckinDate = Today
			ExpectedCheckoutDate = Today
			Details.Clear()

			_originalcheckinDate = CheckinDate
			_originalexpectedCheckoutDate = ExpectedCheckoutDate
			_originalroomUnitPrice = RoomUnitPrice
			_originalroomId = Room.Id
			_originalDetails.Clear()
		End Sub

		' Domain actions
		Public Sub SetLease( id As String )
			Dim model = _getLeaseDataQuery.Execute( id )
			If IsNothing( model ) Then Return

			' add selected room to list
			Dim roomItem = Rooms.FirstOrDefault( Function( r ) r.Id = model.Room.Id )
			If IsNothing( roomItem ) Then Throw New EntryPointNotFoundException()

			LeaseId = id
			Room = roomItem
			RoomUnitPrice = model.RoomPrice
			CheckinDate = model.CheckinDate
			ExpectedCheckoutDate = model.ExpectedCheckoutDate

			_originalcheckinDate = CheckinDate
			_originalexpectedCheckoutDate = ExpectedCheckoutDate
			_originalroomUnitPrice = RoomUnitPrice
			_originalroomId = Room.Id

			Details.Clear()
			_originalDetails.Clear()
			For Each leaseDetailModel As LeaseDetailModel In model.Details
				Dim editableModel = New EditableLeaseDetailModel()
				editableModel.Id = leaseDetailModel.Id
				editableModel.CustomerName = leaseDetailModel.CustomerName
				editableModel.CustomerLicenseId = leaseDetailModel.CustomerLicenseId
				editableModel.CustomerAddress = leaseDetailModel.CustomerAddress

				Dim customerCategory = CustomerCategories.FirstOrDefault( Function( c ) c.Id = leaseDetailModel.CustomerCategory.Id )
				If IsNothing( customerCategory ) Then Throw New EntryPointNotFoundException()

				editableModel.CustomerCategory = customerCategory
				Details.Add( editableModel )
				editableModel.IsEdited = False

				' backup details for saving
				Dim originalModel = New EditableLeaseDetailModel()
				originalModel.Id = leaseDetailModel.Id
				originalModel.CustomerName = leaseDetailModel.CustomerName
				originalModel.CustomerLicenseId = leaseDetailModel.CustomerLicenseId
				originalModel.CustomerAddress = leaseDetailModel.CustomerAddress
				originalModel.CustomerCategory = customerCategory
				_originalDetails.Add( originalModel )
			Next

		End Sub

		Public Async Function SetLeaseAsync( id As String ) As Task
			ShowStaticWindowLoadingDialog()
			Dim model = Await _getLeaseDataQuery.ExecuteAsync( id )
			CloseStaticWindowDialog()

			If IsNothing( model ) Then Return

			' add selected room to list
			Dim roomItem = Rooms.FirstOrDefault( Function( r ) r.Id = model.Room.Id )
			If IsNothing( roomItem ) Then Throw New EntryPointNotFoundException()

			LeaseId = id
			Room = roomItem
			RoomUnitPrice = model.RoomPrice
			CheckinDate = model.CheckinDate
			ExpectedCheckoutDate = model.ExpectedCheckoutDate

			_originalcheckinDate = CheckinDate
			_originalexpectedCheckoutDate = ExpectedCheckoutDate
			_originalroomUnitPrice = RoomUnitPrice
			_originalroomId = Room.Id

			Details.Clear()
			_originalDetails.Clear()
			For Each leaseDetailModel As LeaseDetailModel In model.Details
				Dim editableModel = New EditableLeaseDetailModel()
				editableModel.Id = leaseDetailModel.Id
				editableModel.CustomerName = leaseDetailModel.CustomerName
				editableModel.CustomerLicenseId = leaseDetailModel.CustomerLicenseId
				editableModel.CustomerAddress = leaseDetailModel.CustomerAddress

				Dim customerCategory = CustomerCategories.FirstOrDefault( Function( c ) c.Id = leaseDetailModel.CustomerCategory.Id )
				If IsNothing( customerCategory ) Then Throw New EntryPointNotFoundException()

				editableModel.CustomerCategory = customerCategory
				Details.Add( editableModel )
				editableModel.IsEdited = False

				' backup details for saving
				Dim originalModel = New EditableLeaseDetailModel()
				originalModel.Id = leaseDetailModel.Id
				originalModel.CustomerName = leaseDetailModel.CustomerName
				originalModel.CustomerLicenseId = leaseDetailModel.CustomerLicenseId
				originalModel.CustomerAddress = leaseDetailModel.CustomerAddress
				originalModel.CustomerCategory = customerCategory
				_originalDetails.Add( originalModel )
			Next
		End Function

		Public Sub AddEmptyDetail()
			If CanAddDetail Then _
				Details.Add( New EditableLeaseDetailModel )
		End Sub

		' Exit
		Public Overrides ReadOnly Property IsEdited As Boolean
			Get
				Return CheckForPendingChanges()
			End Get
		End Property

		Private Function CheckForPendingChanges() As Boolean
			If Not Equals( _originalcheckinDate.Date, CheckinDate.Date ) Then Return True
			If Not Equals( _originalexpectedCheckoutDate.Date, ExpectedCheckoutDate.Date ) Then Return True
			If Not Equals( _originalroomUnitPrice, RoomUnitPrice ) Then Return True
			If Not Equals( _originalroomId, Room.Id ) Then Return True
			If Not Equals( _originalDetails.Count, Details.Count ) Then Return True
			If Details.Any( Function( d ) d.IsEdited ) Then Return True
			Return False
		End Function

		Public Overrides Function ActualExitAsync() As Task
			ResetValues()

			ParentWorkspace.NavigateToScreenLeasesList()
			Return MyBase.ActualExitAsync()
		End Function

		' Save
		Public Overrides Function CanSave() As Task(Of Boolean)
			If String.IsNullOrWhiteSpace( LeaseId ) Then Return Task.FromResult( False )

			If ExpectedCheckoutDate.Date < CheckinDate.Date
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Ngày dự kiến trả phải sau ngày đăng ký thuê phòng" )
				Return Task.FromResult( False )
			End If

			If IsNothing( Room ) OrElse String.IsNullOrWhiteSpace( Room.Id )
				ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng chọn phòng thuê" )
				Return Task.FromResult( False )
			End If

			If (Not String.Equals( Room.Id, _originalroomId )) And Room.State = 1
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Phòng {Room.Name} đang thuê bởi khách hàng khác. Vui lòng chọn phòng khác" )
				Return Task.FromResult( False )
			End If

			If Details.Count > _maxNumberOfUsers
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Vượt quá số khách quy định. Mỗi phòng chỉ có tối đa {_maxNumberOfUsers} người" )
				Return Task.FromResult( False )
			End If

			If Details.Count <= 0
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Phải có ít nhất 1 khách thuê phòng" )
				Return Task.FromResult( False )
			End If

			For Each detail As EditableLeaseDetailModel In Details
				If String.IsNullOrWhiteSpace( detail.CustomerName )
					ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng nhập đầy đủ tên khách hàng" )
					Return Task.FromResult( False )
				End If

				If IsNothing( detail.CustomerCategory ) OrElse String.IsNullOrWhiteSpace( detail.CustomerCategory.Id )
					ShowStaticBottomNotification( StaticNotificationType.Information, "Vui chọn đầy đủ loại khách hàng" )
					Return Task.FromResult( False )
				End If
			Next

			Return Task.FromResult( True )
		End Function

		Public Overrides Async Function ActualSaveAsync() As Task
			' try update
			ShowStaticWindowLoadingDialog()

			' try update lease details
			Dim hasError = False

			For Each detail As EditableLeaseDetailModel In Details
				If Not String.IsNullOrWhiteSpace( detail.Id )
					' update lease detail
					If Not Await UpdateLeaseDetailAsync( detail )
						hasError = True
						Exit For
					End If
				Else
					' create lease detail
					If Not Await CreateLeaseDetailAsync( detail )
						hasError = True
						Exit For
					End If
				End If
			Next

			' remove lease detail
			For Each originalDetail As EditableLeaseDetailModel In _originalDetails
				If Not Details.Any( Function( d ) d.Id = originalDetail.Id )
					If Not Await RemoveLeaseDetailAsync( originalDetail )
						hasError = True
						Exit For
					End If
				End If
			Next

			' try update lease
			If hasError
				CloseStaticWindowDialog()
				Return
			End If

			Dim err = Await _updateLeaseCommand.ExecuteAsync( LeaseId, Room.Id, ExpectedCheckoutDate )

			If String.IsNullOrEmpty( err )
				Await OnSaveSuccessAsync()
			Else
				OnSaveFail( err )
			End If
			CloseStaticWindowDialog()
		End Function

		Private Async Function UpdateLeaseDetailAsync( detail As EditableLeaseDetailModel ) As Task(Of Boolean)
			Dim err = Await _updateLeaseDetailCommand.ExecuteAsync( detail.Id,
			                                                        detail.CustomerName,
			                                                        detail.CustomerLicenseId,
			                                                        detail.CustomerAddress,
			                                                        detail.CustomerCategory.Id )
			' error
			If Not String.IsNullOrEmpty( err )
				OnSaveFail( err )
				Return False
			End If

			Return True
		End Function

		Private Async Function CreateLeaseDetailAsync( detail As EditableLeaseDetailModel ) As Task(Of Boolean)
			' create lease detail
			Dim newId = Await _createLeaseDetailFactory.ExecuteAsync( LeaseId,
			                                                          detail.CustomerName,
			                                                          detail.CustomerLicenseId,
			                                                          detail.CustomerAddress,
			                                                          detail.CustomerCategory.Id )
			If String.IsNullOrEmpty( newId )
				OnSaveFail( $"Gặp sự cố trong lúc tạo chi tiết thuê phòng cho khách hàng {detail.CustomerName}" )
				Return False
			End If

			Return True
		End Function

		Private Async Function RemoveLeaseDetailAsync( detail As EditableLeaseDetailModel ) As Task(Of Boolean)
			Dim err = Await _removeLeaseDetailCommand.ExecuteAsync( detail.Id )
			' error
			If Not String.IsNullOrEmpty( err )
				OnSaveFail( err )
				Return False
			End If

			Return True
		End Function

		Private Async Function OnSaveSuccessAsync() As Task
			Await _inventory.OnLeaseUpdatedAsync( LeaseId )
			Await ActualExitAsync()
		End Function

		Private Sub OnSaveFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		' Delete
		Public Overrides Async Function ActualDeleteAsync() As Task
			' try update
			ShowStaticWindowLoadingDialog()
			Dim err = Await _removeLeaseCommmand.ExecuteAsync( LeaseId )

			If String.IsNullOrEmpty( err )
				Await OnDeleteSuccessAsync()
			Else
				OnDeleteFail( err )
			End If

			CloseStaticWindowDialog()
		End Function

		Private Async Function OnDeleteSuccessAsync() As Task
			Await _inventory.OnLeaseRemovedAsync( LeaseId )
			Await ActualExitAsync()
		End Function

		Private Sub OnDeleteFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		' Infrastructure
		Public Sub OnRoomAdded( model As RoomModel ) Implements IRoomsListPresenter.OnRoomAdded
			If Rooms.Any( Function( r ) r.Id = model.Id )
				Throw New DuplicateWaitObjectException()
			End If

			Rooms.Add( New SimpleRoomsListItemModel With {
				         .Id=model.Id,
				         .CategoryName=model.Category.Name,
				         .Name=model.Name,
				         .State=model.State,
				         .UnitPrice=model.Category.UnitPrice} )
		End Sub

		Public Sub OnRoomUpdated( model As RoomModel ) Implements IRoomsListPresenter.OnRoomUpdated
			Dim roomToUpdate = Rooms.FirstOrDefault( Function( r ) r.Id = model.Id )
			If IsNothing( roomToUpdate ) Then Throw New EntryPointNotFoundException()

			roomToUpdate.Name = model.Name
			roomToUpdate.CategoryName = model.Category.Name
			roomToUpdate.UnitPrice = model.Category.UnitPrice
			roomToUpdate.State = model.State
		End Sub

		Public Sub OnRoomRemoved( id As String ) Implements IRoomsListPresenter.OnRoomRemoved
			Dim roomToRemove = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( roomToRemove ) Then Return

			Rooms.Remove( roomToRemove )
			If Equals( Room, roomToRemove )
				Room = Rooms.FirstOrDefault()
			End If
		End Sub
	End Class
End Namespace
