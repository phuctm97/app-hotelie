Imports Hotelie.Presentation.Common.Controls
Imports MaterialDesignThemes.Wpf

Namespace Common
	Public Class AppScreenHasSaving
		Inherits AppScreen
		Implements IAppScreenHasSaving
		Implements INeedWindowModals

		Public Property IsEdited As Boolean Implements IAppScreenHasSaving.IsEdited

		Public Sub New( colorMode As ColorZoneMode )
			MyBase.New( colorMode )

			IsEdited = False
		End Sub

		Public Overrides Sub Show()
			IsEdited = False
			MyBase.Show()
		End Sub

		Public Overrides Function ShowAsync() As Task
			IsEdited = False
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
			Return Nothing
		End Function

		Public Function Save() As Object Implements IAppScreenHasSaving.Save
			Return True
		End Function

		Public Function SaveAsync() As Task(Of Object) Implements IAppScreenHasSaving.SaveAsync
			Return Nothing
		End Function
	End Class
End Namespace