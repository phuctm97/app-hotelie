Imports System.Runtime.CompilerServices
Imports Caliburn.Micro
Imports Hotelie.Presentation.Start.MainWindow.Models
Imports MaterialDesignThemes.Wpf

Namespace Common.Controls
	Public Interface INeedWindowModals
	End Interface

	Public Module WindowExtensions
		Private ReadOnly LoadingDialog As LoadingDialog = New LoadingDialog()

		< Extension >
		Public Sub ShowStaticWindowDialog( component As INeedWindowModals,
		                                   dialog As Object )
			IoC.Get(Of IMainWindow).ShowStaticWindowDialog( dialog )
		End Sub

		< Extension >
		Public Sub ShowStaticWindowLoadingDialog( component As INeedWindowModals )
			IoC.Get(Of IMainWindow).ShowStaticWindowDialog( LoadingDialog )
		End Sub

		< Extension >
		Public Sub CloseStaticWindowDialog( component As INeedWindowModals )
			IoC.Get(Of IMainWindow).CloseStaticWindowDialog()
		End Sub

		< Extension >
		Public Async Function ShowDynamicWindowDialog( component As INeedWindowModals,
		                                               dialog As Object ) As Task(Of Object)
			Return Await DialogHost.Show( dialog, "window" )
		End Function

		< Extension >
		Public Sub ShowStaticTopNotification( component As INeedWindowModals,
		                                      type As StaticNotificationType,
		                                      text As String )
			IoC.Get(Of IMainWindow).ShowStaticTopNotification( type, text )
		End Sub

		< Extension >
		Public Sub CloseStaticTopNotification( component As INeedWindowModals )
			IoC.Get(Of IMainWindow).CloseStaticTopNotification()
		End Sub

		< Extension >
		Public Sub ShowStaticBottomNotification( component As INeedWindowModals,
		                                         type As StaticNotificationType,
		                                         text As String )
			IoC.Get(Of IMainWindow).ShowStaticBottomNotification( type, text )
		End Sub

		< Extension >
		Public Sub CloseStaticBottomNotification( component As INeedWindowModals )
			IoC.Get(Of IMainWindow).CloseStaticBottomNotification()
		End Sub
	End Module
End Namespace
