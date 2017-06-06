Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Factories.CreateLease
Imports Hotelie.Application.Leases.Queries.GetCustomerCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomData
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Infrastructure
Imports Hotelie.Presentation.Leases.Models
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Leases.ViewModels
	Public Class ScreenAddLeaseViewModel
		Inherits PropertyChangedBase
		Implements IChild(Of LeasesWorkspaceViewModel)
		Implements INeedWindowModals
		Implements IRoomsListPresenter

		' Dependencies
		Private ReadOnly _getSimpleRoomsListQuery As IGetSimpleRoomsListQuery
		Private ReadOnly _getCustomerCategoriesListQuery As IGetCustomerCategoriesListQuery
		Private ReadOnly _createLeaseFactory As ICreateLeaseFactory
		Private ReadOnly _inventory As IInventory

		Private _room As SimpleRoomsListItemModel
		Private _expectedCheckoutDate As Date
		Private _details As IObservableCollection(Of EditableLeaseDetailModel)

		Private _maxNumberOfUsers As Integer

		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As LeasesWorkspaceViewModel Implements IChild(Of LeasesWorkspaceViewModel).Parent
			Get
				Return CType(Parent, LeasesWorkspaceViewModel)
			End Get
			Set
				Parent = value
			End Set
		End Property

		Public ReadOnly Property CheckinDate As Date
			Get
				Return Date.Now
			End Get
		End Property

		Public Property Room As SimpleRoomsListItemModel
			Get
				Return _room
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _room ) Then Return
				_room = value
				NotifyOfPropertyChange( Function() Room )
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

		Public Property Details As IObservableCollection(Of EditableLeaseDetailModel)
			Get
				Return _details
			End Get
			Set
				If IsNothing( Value ) Or Equals( Value, _details ) Then Return
				_details = value
				NotifyOfPropertyChange( Function() Details )
			End Set
		End Property

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

		Public ReadOnly Property Rooms As IObservableCollection(Of SimpleRoomsListItemModel)

		Public Shared ReadOnly Property CustomerCategories As IObservableCollection(Of CustomerCategoriesListItemModel)

		' Initialization
		Shared Sub New()
			CustomerCategories = New BindableCollection(Of CustomerCategoriesListItemModel)
		End Sub

		Public Sub New( workspace As LeasesWorkspaceViewModel,
		                getSimpleRoomsListQuery As IGetSimpleRoomsListQuery,
		                getCustomerCategoriesListQuery As IGetCustomerCategoriesListQuery,
		                createLeaseFactory As ICreateLeaseFactory,
		                inventory As IInventory )
			ParentWorkspace = workspace
			_getSimpleRoomsListQuery = getSimpleRoomsListQuery
			_getCustomerCategoriesListQuery = getCustomerCategoriesListQuery
			_createLeaseFactory = createLeaseFactory
			_inventory = inventory
			RegisterInventory()

			Rooms = New BindableCollection(Of SimpleRoomsListItemModel)
		End Sub

		Public Sub Init()
			Rooms.Clear()
			Rooms.AddRange( _getSimpleRoomsListQuery.Execute().Where( Function( r ) r.State = 0 ) )

			CustomerCategories.Clear()
			CustomerCategories.AddRange( _getCustomerCategoriesListQuery.Execute() )
			InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Rooms.Clear()
			Rooms.AddRange( (Await _getSimpleRoomsListQuery.ExecuteAsync()).Where( Function( r ) r.State = 0 ) )

			CustomerCategories.Clear()
			CustomerCategories.AddRange( Await _getCustomerCategoriesListQuery.ExecuteAsync() )
			InitValues()
		End Function

		Private Sub InitValues()
			_maxNumberOfUsers = 4
			Room = Rooms.FirstOrDefault()
			NotifyOfPropertyChange( Function() CheckinDate )
			ExpectedCheckoutDate = Date.Now
			Details = New BindableCollection(Of EditableLeaseDetailModel)
		End Sub

		Private Sub ResetValues()
			Room = Rooms.FirstOrDefault()
			NotifyOfPropertyChange( Function() CheckinDate )
			ExpectedCheckoutDate = Date.Now
			Details.Clear()
		End Sub

		Public Sub CheckNumberOfDetails()
			NotifyOfPropertyChange( Function() CanAddDetail )
			NotifyOfPropertyChange( Function() CanDeleteDetail )
		End Sub

		' Exit
		Public Async Sub PreviewExit()
			If CheckForPendingChanges()
				Dim result = Await ConfirmExit()

				If Equals( result, 1 )
					PreviewSave()
					Return
				ElseIf Equals( result, 2 )
					Return
				End If
			End If

			[Exit]()
		End Sub

		Public Async Sub PreviewExitAsync()
			If CheckForPendingChanges()
				Dim result = Await ConfirmExit()

				If Equals( result, 1 )
					PreviewSaveAsync()
					Return
				ElseIf Equals( result, 2 )
					Return
				End If
			End If

			[Exit]()
		End Sub

		Private Sub [Exit]()
			ParentWorkspace.NavigateToScreenLeasesList()
			ResetValues()
		End Sub

		Private Function CheckForPendingChanges() As Boolean
			If Details.Count > 0 Then Return True
			Return False
		End Function

		Private Async Function ConfirmExit() As Task(Of Integer)
			' show dialog
			Dim dialog = New ThreeButtonDialog( "Thoát mà không lưu các thay đổi?",
			                                    "THOÁT",
			                                    "LƯU & THOÁT",
			                                    "HỦY",
			                                    False,
			                                    True,
			                                    False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "THOÁT" ) Then Return 0
			If String.Equals( result, "HỦY" ) Then Return 2
			Return 1
		End Function

		' Save
		Public Sub PreviewSave()
			If ValidateData()
				Save()
			End If
		End Sub

		Public Sub PreviewSaveAsync()
			If ValidateData()
				SaveAsync()
			End If
		End Sub

		Private Sub Save()
			' try create lease
			Dim detailModels = New List(Of CreateLeaseDetailModel)
			For Each detail As EditableLeaseDetailModel In Details
				detailModels.Add( New CreateLeaseDetailModel With {
					                .CustomerName=detail.CustomerName,
					                .CustomerLicenseId=detail.CustomerLicenseId,
					                .CustomerAddress=detail.CustomerAddress,
					                .CustomerCategoryId=detail.CustomerCategory.Id} )
			Next

			Dim newId = _createLeaseFactory.Execute( Room.Id, CheckinDate, ExpectedCheckoutDate, detailModels )

			If String.IsNullOrEmpty( newId )
				OnSaveFail()
			Else
				OnSaveSuccess( newId )
			End If
		End Sub

		Private Sub OnSaveSuccess( newId As String )
			_inventory.OnLeaseAdded( newId )
			[Exit]()
		End Sub

		Private Async Sub SaveAsync()
			' try create lease
			Dim detailModels = New List(Of CreateLeaseDetailModel)
			For Each detail As EditableLeaseDetailModel In Details
				detailModels.Add( New CreateLeaseDetailModel With {
					                .CustomerName=detail.CustomerName,
					                .CustomerLicenseId=detail.CustomerLicenseId,
					                .CustomerAddress=detail.CustomerAddress,
					                .CustomerCategoryId=detail.CustomerCategory.Id} )
			Next

			Dim newId = Await _createLeaseFactory.ExecuteAsync( Room.Id, CheckinDate, ExpectedCheckoutDate, detailModels )

			If String.IsNullOrEmpty( newId )
				OnSaveFail()
			Else
				Await OnSaveSuccessAsync( newId )
			End If
		End Sub

		Private Async Function OnSaveSuccessAsync( newId As String ) As Task
			Await _inventory.OnLeaseAddedAsync( newId )
			[Exit]()
		End Function

		Private Sub OnSaveFail()
			ShowStaticBottomNotification( StaticNotificationType.Error, "Gặp sự cố trong lúc tạo phiếu thuê phòng" )
		End Sub

		Private Function ValidateData() As Boolean
			If ExpectedCheckoutDate < CheckinDate
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Ngày dự kiến trả phải sau ngày đăng ký thuê phòng" )
				Return False
			End If

			If IsNothing( Room ) OrElse String.IsNullOrWhiteSpace( Room.Id )
				ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng chọn phòng thuê" )
				Return False
			End If

			If Room.State = 1
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Phòng {Room.Name} đang thuê bởi khách hàng khác. Vui lòng chọn phòng khác" )
				Return False
			End If

			If Details.Count > _maxNumberOfUsers
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Vượt quá số khách quy định. Mỗi phòng chỉ có tối đa {_maxNumberOfUsers} người" )
				Return False
			End If

			If Details.Count <= 0
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Phải có ít nhất 1 khách thuê phòng" )
				Return False
			End If

			For Each detail As EditableLeaseDetailModel In Details
				If String.IsNullOrWhiteSpace( detail.CustomerName )
					ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng nhập đầy đủ tên khách hàng" )
					Return False
				End If

				If IsNothing( detail.CustomerCategory ) OrElse String.IsNullOrWhiteSpace( detail.CustomerCategory.Id )
					ShowStaticBottomNotification( StaticNotificationType.Information, "Vui chọn đầy đủ loại khách hàng" )
					Return False
				End If
			Next

			Return True
		End Function

		Public Sub OnRoomAdded( model As RoomModel ) Implements IRoomsListPresenter.OnRoomAdded
			If model.State <> 0 Then Return

			If Rooms.Any( Function( r ) r.Id = model.Id )
				Throw New DuplicateWaitObjectException()
			End If

			Rooms.Add( New SimpleRoomsListItemModel With {
				         .Id=model.Id,
				         .CategoryName=model.Category.Name,
				         .Name=model.Name,
				         .State=0,
				         .UnitPrice=model.Category.UnitPrice} )
		End Sub

		Public Sub OnRoomUpdated( model As RoomModel ) Implements IRoomsListPresenter.OnRoomUpdated
			Dim roomToUpdate = Rooms.FirstOrDefault( Function( r ) r.Id = model.Id )
			If IsNothing( roomToUpdate )
				If model.State = 0
					Rooms.Add( New SimpleRoomsListItemModel With {
						         .Id=model.Id,
						         .Name=model.Name,
						         .CategoryName=model.Category.Name,
						         .State=0,
						         .UnitPrice=model.Category.UnitPrice} )
				End If
			Else
				If model.State <> 0
					Rooms.Remove( roomToUpdate )
					If Equals( Room, roomToUpdate )
						Room = Rooms.FirstOrDefault()
					End If
				Else
					roomToUpdate.Name = model.Name
					roomToUpdate.CategoryName = model.Category.Name
					roomToUpdate.UnitPrice = model.Category.UnitPrice
				End If
			End If
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
