Imports Caliburn.Micro

Namespace Common
	Public Interface IShell
		Inherits IScreen,
		         IChild(Of IMainWindow)

		ReadOnly Property CommandsBar As IWindowCommandsBar
	End Interface
End Namespace