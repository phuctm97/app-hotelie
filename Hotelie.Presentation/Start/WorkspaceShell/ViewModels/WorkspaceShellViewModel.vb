Imports Caliburn.Micro
Imports Hotelie.Presentation.Bills.ViewModels
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Leases.ViewModels
Imports Hotelie.Presentation.Rooms.ViewModels
Imports MaterialDesignThemes.Wpf

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellViewModel
		Inherits Conductor(Of IWorkspace).Collection.OneActive
		Implements IShell

		' Workspaces backing fields

		Private _activeWorkspace As IWorkspace

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

		Public Property ActiveWorkspace As IWorkspace
			Get
				Return _activeWorkspace
			End Get
			Set
				If Equals( Value, _activeWorkspace ) Then Return
				_activeWorkspace = value
				NotifyOfPropertyChange( Function() ActiveWorkspace )
			End Set
		End Property

		Protected Overrides Sub ChangeActiveItem( newItem As IWorkspace,
		                                          closePrevious As Boolean )
			MyBase.ChangeActiveItem( newItem, closePrevious )

			ActiveWorkspace = ActiveItem
		End Sub

		' Initialization
		Public Sub New()
			CommandsBar = New WorkspaceShellCommandsBarViewModel( Me )

			DisplayName = "Bàn làm việc"

			Items.Add( IoC.Get(Of RoomsWorkspaceViewModel)() )
			Items.Add( IoC.Get(Of LeasesWorkspaceViewModel)() )
			Items.Add( IoC.Get(Of BillsWorkspaceViewModel)() )
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			ActivateItem( Items.FirstOrDefault() )
		End Sub

		' Display properties

		Public ReadOnly Property CommandsBar As IWindowCommandsBar Implements IShell.CommandsBar

		' Closing
		Public Overrides Async Sub CanClose( callback As Action(Of Boolean) )
			Dim dialog = New TwoButtonDialog( "Thoát khỏi bàn làm việc?", "THOÁT", "HỦY" )

			Dim result = Await DialogHost.Show( dialog, "workspace-shell" )

			If String.Equals( result, "THOÁT" )
				callback( True )
			Else
				callback( False )
			End If
		End Sub
	End Class
End Namespace
