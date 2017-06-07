Imports Hotelie.Presentation.Common.Controls
Imports MaterialDesignThemes.Wpf

Namespace Common
	Public MustInherit Class AppScreenHasSavingAndDeleting
		Inherits AppScreenHasSaving
		Implements IAppScreenHasDeleting

		Public Sub New( colorMode As ColorZoneMode )
			MyBase.New( colorMode )
		End Sub

		Public Overridable Async Function CanDelete() As Task(Of Boolean) _
			Implements IAppScreenHasDeleting.CanDelete
			' show dialog
			Dim dialog = New TwoButtonDialog( "Bạn có chắc muốn xóa?",
			                                  "XÓA",
			                                  "HỦY",
			                                  True,
			                                  False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "XÓA" ) Then Return True
			Return False
		End Function

		Public Overridable Async Sub Delete() Implements IAppScreenHasDeleting.Delete
			If Not Await CanDelete() Then Return
			ActualDelete()
		End Sub

		Public Overridable Sub ActualDelete() Implements IAppScreenHasDeleting.ActualDelete
		End Sub

		Public Overridable Function ActualDeleteAsync() As Task Implements IAppScreenHasDeleting.ActualDeleteAsync
			Return Task.FromResult( True )
		End Function

		Public Overridable Async Function DeleteAsync() As Task Implements IAppScreenHasDeleting.DeleteAsync
			If Not Await CanDelete() Then Return
			Await ActualDeleteAsync()
		End Function
	End Class
End Namespace
