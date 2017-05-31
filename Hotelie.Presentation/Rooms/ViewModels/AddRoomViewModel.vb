Imports Caliburn.Micro

Namespace Rooms.ViewModels
	Public Class AddRoomViewModel
		Inherits Screen
		Implements IChild(Of RoomsWorkspaceViewModel)

		Private Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent
			Get
				Return CType(Parent, RoomsWorkspaceViewModel)
			End Get
			Set
				Parent = value
			End Set
		End Property

		Public Sub Close()
			ParentWorkspace.IsDialogOpen = False
		End Sub

	End Class	
End Namespace
