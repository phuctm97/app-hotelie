Imports Caliburn.Micro

Namespace Common
	Public Interface IShell
		Inherits IHaveDisplayName,
		         IChild(Of IMainWindow)
	End Interface
End Namespace