Imports Caliburn.Micro

Namespace Common
	Public Interface IMainWindow
		Inherits IHaveDisplayName

		Property Title As String

		Property Width As Double

		Property Height As Double

		Property WindowState As WindowState

		ReadOnly Property Shell As IShell

		Sub SwitchShell( shellName As String )

		Sub ShowStaticNotification( type As Integer,
		                            text As String )

		Sub CloseStaticNotification()

		Sub ShowStaticDialog( content As Object )

		Sub CloseStaticDialog()

		Sub DragMove()

		Sub ToggleZoomState()

		Sub Hide()

		Sub Close()
	End Interface
End Namespace