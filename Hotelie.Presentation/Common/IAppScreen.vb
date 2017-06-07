Imports Caliburn.Micro
Imports MaterialDesignThemes.Wpf

Namespace Common
	Public Interface IAppScreen
		Inherits IHaveDisplayName

		ReadOnly Property ColorMode As ColorZoneMode

		Event OnExited As EventHandler

		Sub Show()

		Function ShowAsync() As Task

		Sub [Exit]()

		Function ExitAsync() As Task

	End Interface
End Namespace