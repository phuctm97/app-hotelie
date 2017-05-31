Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Start.WorkspaceShell.ViewModels
	Public Class WorkspaceShellViewModel
		Inherits Conductor(Of IWorkspace).Collection.OneActive
		Implements IShell

		Private _activeWorkspace As IWorkspace

		Public Property ParentWindow As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return CType(Parent, IMainWindow)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		Public Property ActiveWorkspace As IWorkspace
			Get
				Return _activeWorkspace
			End Get
			Set
				If Equals(Value, _activeWorkspace) Then Return

				_activeWorkspace = value
				NotifyOfPropertyChange(Function() ActiveWorkspace)
			End Set
		End Property

		Public ReadOnly Property Workspaces As IObservableCollection(Of IWorkspace)
			Get
				Return Items
			End Get
		End Property

		Public Sub New()
			DisplayName = "Workspace"
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			Items.Add( IoC.Get(Of IWorkspace)( "rooms-workspace" ) )
			Items.Add( IoC.Get(Of IWorkspace)( "leases-workspace" ) )
		End Sub

		Protected Overrides Sub OnViewLoaded( view As Object )
			MyBase.OnViewLoaded( view )

			ActivateItem( Items.FirstOrDefault() )
		End Sub

		Protected Overrides Sub ChangeActiveItem( newItem As IWorkspace,
		                                          closePrevious As Boolean )
			MyBase.ChangeActiveItem( newItem, closePrevious )

			ActiveWorkspace = newItem
		End Sub
	End Class
End Namespace
