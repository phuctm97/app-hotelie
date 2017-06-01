Imports Caliburn.Micro

Namespace Rooms.ViewModels
	Public Class RoomDetailViewModel
		Inherits Screen
		Implements IChild(Of RoomsWorkspaceViewModel)

		Public Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent
			Get
				Return CType(Parent, RoomsWorkspaceViewModel)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		Protected Overrides Sub OnViewReady(view As Object)
			MyBase.OnViewReady(view)

			ParentWorkspace.IsTopDrawerOpen = True
		End Sub

	End Class
End Namespace
