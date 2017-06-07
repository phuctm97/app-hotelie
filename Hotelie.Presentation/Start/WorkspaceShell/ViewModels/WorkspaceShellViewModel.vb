Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Bills.ViewModels
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Leases.ViewModels
Imports Hotelie.Presentation.Rooms.ViewModels
Imports Hotelie.Presentation.Rules.ViewModels
Imports MaterialDesignThemes.Wpf

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellViewModel
		Inherits Screen
		Implements IShell
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _authentication As IAuthentication

		Private _activeWorkspace As IScreen
		Private _displayWorkspaceCode As Integer
		Private _displayCode As Integer

		' Parent window
		Public Property ParentWindow As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return CType(Parent, IMainWindow)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		' Initialization
		Public Sub New( authentication As IAuthentication )
			_authentication = authentication

			CommandsBar = New WorkspaceShellCommandsBarViewModel( Me )

			DisplayName = "Bàn làm việc"
			WorkspaceRooms = IoC.Get(Of RoomsWorkspaceViewModel)
			WorkspaceLeases = IoC.Get(Of LeasesWorkspaceViewModel)
			WorkspaceBills = IoC.Get(Of BillsWorkspaceViewModel)
			Workspaces = New BindableCollection(Of IScreen) From {WorkspaceRooms, WorkspaceLeases, WorkspaceBills}

			ScreenChangeRules = IoC.Get(Of ScreenChangeRulesViewModel)
			DisplayWorkspaceCode = 0
		End Sub

		' Display properties
		Public ReadOnly Property CommandsBar As IWindowCommandsBar Implements IShell.CommandsBar

		Public ReadOnly Property WorkspaceRooms As RoomsWorkspaceViewModel

		Public ReadOnly Property WorkspaceLeases As LeasesWorkspaceViewModel

		Public ReadOnly Property WorkspaceBills As BillsWorkspaceViewModel

		Public ReadOnly Property ScreenChangeRules As ScreenChangeRulesViewModel

		Public ReadOnly Property Workspaces As IObservableCollection(Of IScreen)

		Public Property DisplayWorkspaceCode As Integer
			Get
				Return _displayWorkspaceCode
			End Get
			Set
				If Equals( Value, _displayWorkspaceCode ) Then Return
				_displayWorkspaceCode = value
				DisplayCode = DisplayWorkspaceCode
				NotifyOfPropertyChange( Function() DisplayWorkspaceCode )
			End Set
		End Property

		Public Property DisplayCode As Integer
			Get
				Return _displayCode
			End Get
			Set
				If Equals( Value, _displayCode ) Then Return
				_displayCode = value
				NotifyOfPropertyChange( Function() DisplayCode )

				If DisplayCode = 3
					ParentWindow.TitleMode = ColorZoneMode.Accent
				Else
					ParentWindow.TitleMode = ColorZoneMode.PrimaryDark
				End If
			End Set
		End Property

		Public Sub NavigateToScreenAddLease( roomId As String )
			DisplayWorkspaceCode = 1
			WorkspaceLeases.NavigateToScreenAddLease( roomId )
		End Sub

		Public Sub NavigateToScreenChangeRules()
			DisplayCode = 3
		End Sub

		' Closing
		Public Overrides Async Sub CanClose( callback As Action(Of Boolean) )
			Dim dialog = New TwoButtonDialog( "Thoát khỏi bàn làm việc?", "THOÁT", "HỦY", True, False )

			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "THOÁT" )
				callback( True )
			Else
				callback( False )
			End If
		End Sub

		Protected Overrides Sub OnDeactivate( close As Boolean )
			ParentWindow.TitleMode = ColorZoneMode.PrimaryDark

			MyBase.OnDeactivate( close )

			If close
				_authentication.Logout()
			End If
		End Sub
	End Class
End Namespace
