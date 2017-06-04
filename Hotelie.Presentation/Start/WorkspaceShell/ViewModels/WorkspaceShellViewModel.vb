Imports Caliburn.Micro
Imports Hotelie.Presentation.Bills.ViewModels
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Leases.ViewModels
Imports Hotelie.Presentation.Rooms.ViewModels

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellViewModel
		Inherits Conductor(Of IScreen).Collection.OneActive
		Implements IShell
		Implements INeedWindowModals

		Private _activeWorkspace As IScreen

		' Parent window
		Public Property ParentWindow As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return CType(Parent, IMainWindow)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		' Workspaces
		Public Property ActiveWorkspace As IScreen
			Get
				Return _activeWorkspace
			End Get
			Set
				If Equals( Value, _activeWorkspace ) Then Return
				_activeWorkspace = value
				NotifyOfPropertyChange( Function() ActiveWorkspace )
			End Set
		End Property

		Protected Overrides Sub ChangeActiveItem( newItem As IScreen,
		                                          closePrevious As Boolean )
			MyBase.ChangeActiveItem( newItem, closePrevious )

			ActiveWorkspace = ActiveItem
		End Sub

		' Initialization
		Public Sub New()
			CommandsBar = New WorkspaceShellCommandsBarViewModel( Me )

			DisplayName = "Bàn làm việc"

			' Add all screens
			Items.Add( IoC.Get(Of RoomsWorkspaceViewModel)() )
			Items.Add( IoC.Get(Of LeasesWorkspaceViewModel)() )
			Items.Add( IoC.Get(Of BillsWorkspaceViewModel)() )
		End Sub

		Protected Overrides Sub OnViewReady( view As Object )
			MyBase.OnViewReady( view )

			ActivateItem( Items.FirstOrDefault() )
		End Sub

		' Display properties
		Public ReadOnly Property CommandsBar As IWindowCommandsBar Implements IShell.CommandsBar

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
	End Class
End Namespace
