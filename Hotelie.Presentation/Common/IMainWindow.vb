﻿Imports Caliburn.Micro

Namespace Common
	Public Interface IMainWindow
		Inherits IHaveDisplayName

		Property Title As String

		Property Width As Double

		Property Height As Double

		Property WindowState As WindowState

		ReadOnly Property Shell As IShell

		Sub SwitchShell( shellName As String )

		Sub ShowStaticTopNotification( type As Integer,
		                               text As String )

		Sub CloseStaticTopNotification()

		Sub ShowStaticBottomNotification( type As Integer,
		                                  text As String )

		Sub CloseStaticBottomNotification()

		Sub ShowStaticShellDialog( content As Object )

		Sub CloseStaticShellDialog()

		Sub ShowStaticWindowDialog( content As Object )

		Sub CloseStaticWindowDialog()

		Sub DragMove()

		Sub ToggleZoomState()

		Sub Hide()

		Sub Close()
	End Interface
End Namespace