Imports Caliburn.Micro
Imports MaterialDesignThemes.Wpf

Namespace Common
	Public MustInherit Class AppScreen
		Inherits PropertyChangedBase
		Implements IAppScreen

		Private _displayName As String

		Sub New( colorMode As ColorZoneMode )
			Me.ColorMode = colorMode
		End Sub

		Public ReadOnly Property ColorMode As ColorZoneMode Implements IAppScreen.ColorMode

		Public Property DisplayName As String Implements IHaveDisplayName.DisplayName
			Get
				Return _displayName
			End Get
			Set
				If String.IsNullOrEmpty( Value ) OrElse String.Equals( Value, _displayName ) Then Return
				_displayName = value
				NotifyOfPropertyChange( Function() DisplayName )
			End Set
		End Property

		Public Event OnExited As EventHandler Implements IAppScreen.OnExited

		Protected Sub RaiseEventOnExited( Optional e As EventArgs = Nothing )
			RaiseEvent OnExited( Me, e )
		End Sub

		Public Overridable Sub Show() Implements IAppScreen.Show
		End Sub

		Public Overridable Function ShowAsync() As Task Implements IAppScreen.ShowAsync
			Return Task.FromResult( True )
		End Function

		Public Function CanHide() As Task(Of Boolean) Implements IAppScreen.CanHide
			Return Task.FromResult( True )
		End Function

		Public Overridable Sub [Exit]() Implements IAppScreen.[Exit]
			RaiseEventOnExited()
		End Sub

		Public Overridable Function ExitAsync() As Task Implements IAppScreen.ExitAsync
			RaiseEventOnExited()
			Return Nothing
		End Function
	End Class
End Namespace
