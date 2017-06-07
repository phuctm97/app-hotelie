Imports Hotelie.Presentation.Common.Controls
Imports MaterialDesignThemes.Wpf

Namespace Common
	Public MustInherit Class AppScreenHasSaving
		Inherits AppScreen
		Implements IAppScreenHasSaving

		Implements INeedWindowModals

		Public Overridable ReadOnly Property IsEdited As Boolean Implements IAppScreenHasSaving.IsEdited

		Public Sub New( colorMode As ColorZoneMode )
			MyBase.New( colorMode )

			IsEdited = False
		End Sub

		Public Overrides Sub Show()
			MyBase.Show()
		End Sub

		Public Overrides Function ShowAsync() As Task
			Return MyBase.ShowAsync()
		End Function

		Public Overrides Async Sub [Exit]()
			If Not IsEdited
				ActualExit()
			Else
				Dim rc = Await CanExit()

				Select Case rc
					Case 0
						ActualExit()
					Case 1
						Save()
					Case 2
						Return
				End Select
			End If
		End Sub

		Public Overrides Async Function ExitAsync() As Task
			If Not IsEdited
				Await ActualExitAsync()
			Else
				Dim rc = Await CanExit()

				Select Case rc
					Case 0
						Await ActualExitAsync()
					Case 1
						Await SaveAsync()
					Case 2
						Return
				End Select
			End If
		End Function

		Public Overridable Async Function CanExit( Optional message As String = "Thoát mà không lưu các thay đổi?",
		                                           Optional buttonExit As String = "THOÁT",
		                                           Optional buttonSave As String = "LƯU",
		                                           Optional buttonCancel As String = "HỦY" ) As Task(Of Int32) _
			Implements IAppScreenHasSaving.CanExit
			' show dialog
			Dim dialog = New ThreeButtonDialog( message,
			                                    buttonExit,
			                                    buttonSave,
			                                    buttonCancel,
			                                    False,
			                                    True,
			                                    False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, buttonExit ) Then Return 0
			If String.Equals( result, buttonCancel ) Then Return 2
			Return 1
		End Function

		Public Overridable Sub ActualExit() Implements IAppScreenHasSaving.ActualExit
			RaiseEventOnExited()
		End Sub

		Public Overridable Function ActualExitAsync() As Task Implements IAppScreenHasSaving.ActualExitAsync
			RaiseEventOnExited()
			Return Task.FromResult( True )
		End Function

		Public Overridable Function CanSave() As Task(Of Boolean) Implements IAppScreenHasSaving.CanSave
			Return Task.FromResult( True )
		End Function

		Public Overridable Async Sub Save() Implements IAppScreenHasSaving.Save
			If Not Await CanSave() Then Return
			ActualSave()
		End Sub

		Public Overridable Async Function SaveAsync() As Task Implements IAppScreenHasSaving.SaveAsync
			If Not Await CanSave() Then Return
			Await ActualSaveAsync()
		End Function

		Public Overridable Sub ActualSave() Implements IAppScreenHasSaving.ActualSave
		End Sub

		Public Overridable Function ActualSaveAsync() As Task Implements IAppScreenHasSaving.ActualSaveAsync
			Return Task.FromResult( True )
		End Function
	End Class
End Namespace