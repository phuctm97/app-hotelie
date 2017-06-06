Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries.GetLeaseData
Imports Hotelie.Application.Leases.Queries.GetLeaseDetailData
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Leases.ViewModels
	Public Class ScreenLeaseDetailViewModel
		Inherits PropertyChangedBase
		Implements IChild(Of LeasesWorkspaceViewModel)
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _getLeaseDataQuery As IGetLeaseDataQuery
		Private ReadOnly _getSimpleRoomsListQuery As IGetSimpleRoomsListQuery

		Private _leaseId As String
		Private _roomUnitPrice As Decimal
		Private _checkinDate As Date
		Private _room As SimpleRoomsListItemModel
		Private _expectedCheckoutDate As Date
		Private _details As IObservableCollection(Of LeaseDetailModel)

		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As LeasesWorkspaceViewModel Implements IChild(Of LeasesWorkspaceViewModel).Parent
			Get
				Return CType(Parent, LeasesWorkspaceViewModel)
			End Get
			Set
				Parent = value
			End Set
		End Property

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
				Return (Date.Now - CheckinDate).TotalDays
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

		Public Property Details As IObservableCollection(Of LeaseDetailModel)
			Get
				Return _details
			End Get
			Set
				If IsNothing( _details ) Or Equals( Value, _details ) Then Return
				_details = value
				NotifyOfPropertyChange( Function() Details )
			End Set
		End Property

		Public ReadOnly Property Rooms As IObservableCollection(Of SimpleRoomsListItemModel)

		' Initialization

		Public Sub New( workspace As LeasesWorkspaceViewModel,
		                getLeaseDataQuery As IGetLeaseDataQuery,
		                getSimpleRoomsListQuery As IGetSimpleRoomsListQuery )
			ParentWorkspace = workspace
			_getLeaseDataQuery = getLeaseDataQuery
			_getSimpleRoomsListQuery = getSimpleRoomsListQuery

			Rooms = New BindableCollection(Of SimpleRoomsListItemModel)
		End Sub

		Public Sub Init()
			Rooms.Clear()
			Rooms.AddRange( _getSimpleRoomsListQuery.Execute() )

			InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Rooms.Clear()
			Rooms.AddRange( Await _getSimpleRoomsListQuery.ExecuteAsync() )

			InitValues()
		End Function

		Private Sub InitValues()
			LeaseId = String.Empty
			Room = New SimpleRoomsListItemModel()
			RoomUnitPrice = 0
			CheckinDate = Date.Now
			ExpectedCheckoutDate = Date.Now
		End Sub

		Private Sub ResetValues()
			LeaseId = String.Empty
			RoomUnitPrice = 0
			CheckinDate = Date.Now
			ExpectedCheckoutDate = Date.Now
		End Sub

		Public Sub SetLease( id As String )
			Dim model = _getLeaseDataQuery.Execute( id )
			If IsNothing( model ) Then Return

			Dim roomItem = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( roomItem ) Then Throw New EntryPointNotFoundException()

			LeaseId = id
			Room = roomItem
			RoomUnitPrice = model.RoomPrice
			CheckinDate = model.CheckinDate
			ExpectedCheckoutDate = model.ExpectedCheckoutDate
			Details = New BindableCollection(Of LeaseDetailModel)( model.Details )
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
			' try update
			Dim err = String.Empty

			If String.IsNullOrEmpty( err )
				OnSaveSuccess()
			Else
				OnSaveFail( err )
			End If
		End Sub

		Private Sub OnSaveSuccess()
			[Exit]()
		End Sub

		Private Async Sub SaveAsync()
			' try update
			ShowStaticWindowLoadingDialog()

			Dim err = Await Task.Run( Function() String.Empty )

			If String.IsNullOrEmpty( err )
				Await OnSaveSuccessAsync()
			Else
				OnSaveFail( err )
			End If

			CloseStaticWindowDialog()
		End Sub

		Private Async Function OnSaveSuccessAsync() As Task
			[Exit]()
		End Function

		Private Sub OnSaveFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		Private Function ValidateData() As Boolean
			Return True
		End Function

		' Delete
		Public Async Sub PreviewDelete()
			Dim result = Await ConfirmDelete()

			If Equals( result, 0 )
				Delete()
			End If
		End Sub

		Public Async Sub PreviewDeleteAsync()
			Dim result = Await ConfirmDelete()

			If Equals( result, 0 )
				DeleteAsync()
			End If
		End Sub

		Private Async Function ConfirmDelete() As Task(Of Integer)
			' show dialog
			Dim dialog = New TwoButtonDialog( "Xóa phòng. Tiếp tục?",
			                                  "XÓA",
			                                  "HỦY",
			                                  True,
			                                  False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "XÓA" ) Then Return 0
			Return 1
		End Function

		Private Sub Delete()
			' try update
			Dim err = String.Empty

			If String.IsNullOrEmpty( err )
				OnDeleteSuccess()
			Else
				OnDeleteFail( err )
			End If
		End Sub

		Private Sub OnDeleteSuccess()
			[Exit]()
		End Sub

		Private Async Sub DeleteAsync()
			' try update
			ShowStaticWindowLoadingDialog()
			Dim err = Await Task.Run( Function() String.Empty )

			If String.IsNullOrEmpty( err )
				Await OnDeleteSuccessAsync()
			Else
				OnDeleteFail( err )
			End If

			CloseStaticWindowDialog()
		End Sub

		Private Async Function OnDeleteSuccessAsync() As Task
			[Exit]()
		End Function

		Private Sub OnDeleteFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub
	End Class
End Namespace
