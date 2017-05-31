Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Start.LoginShell.ViewModels
	Public Class LoginShellViewModel
		Inherits Screen
		Implements IShell

		Private _isLoginFormVisible As Boolean
		Private _isSettingsFormVisible As Boolean

		Public Property ParentWindow As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return CType(Parent, IMainWindow)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		Public Sub New()
			DisplayName = "Login"
			IsLoginFormVisible = True
			IsSettingsFormVisible = False
		End Sub

		' Forms display
		Public Property IsLoginFormVisible As Boolean
			Get
				Return _isLoginFormVisible
			End Get
			Set
				If Equals( Value, _isLoginFormVisible ) Then Return
				_isLoginFormVisible = value
				NotifyOfPropertyChange( Function() IsLoginFormVisible )
			End Set
		End Property

		Public Property IsSettingsFormVisible As Boolean
			Get
				Return _isSettingsFormVisible
			End Get
			Set
				If Equals( Value, _isSettingsFormVisible ) Then Return
				_isSettingsFormVisible = value
				NotifyOfPropertyChange( Function() IsSettingsFormVisible )
			End Set
		End Property

		Public Sub ToggleDisplayForm()
			IsLoginFormVisible = Not IsLoginFormVisible
			IsSettingsFormVisible = Not IsSettingsFormVisible
		End Sub

		' Window commands
		Public Sub ToggleWindowZoomState()
			ParentWindow.ToggleZoomState()
		End Sub

		Public Sub HideWindow()
			ParentWindow.Hide()
		End Sub

		Public Sub CloseWindow()
			ParentWindow.Close()
		End Sub
	End Class
End Namespace
