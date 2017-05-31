Namespace Common
	Public Interface IMainWindow
		Property Title As String

		Property Width As Double

		Property Height As Double

		Property WindowState As WindowState

		ReadOnly Property Shell As IShell

		Sub ShowLoginShell()

		Sub ShowWorkspaceShell()

		Sub ToggleZoomState()

		Sub Hide()

		Sub Close()

	End Interface
End Namespace