Imports MaterialDesignThemes.Wpf
Imports MaterialDesignThemes.Wpf.Transitions

Namespace Start.WorkspaceShell.Views

Public Class WorkspaceShellView

    Private _storedSelectedIndex As Integer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitWindowState()

        InitViews()
    End Sub

    Private Sub InitViews()
     

        _storedSelectedIndex = 0
        TabItems.SelectedIndex = 0
    End Sub

    Private Sub InitWindowState()
        If Application.Current.MainWindow.WindowState = WindowState.Minimized Then _
            Application.Current.MainWindow.WindowState = WindowState.Normal
        ButtonZoom.ToolTip = If( Application.Current.MainWindow.WindowState = WindowState.Maximized, "Thu nhỏ", "Phóng to" )
    End Sub

    Private Sub OnZoomButtonClick( sender As Object,
                                   e As RoutedEventArgs )
        If Application.Current.MainWindow.WindowState = WindowState.Normal
            Application.Current.MainWindow.WindowState = WindowState.Maximized
            ButtonZoom.ToolTip = "Thu nhỏ"
        Else
            Application.Current.MainWindow.WindowState = WindowState.Normal
            ButtonZoom.ToolTip = "Phóng to"
        End If
    End Sub

    Private Sub OnHideButtonClick( sender As Object,
                                   e As RoutedEventArgs )
        Application.Current.MainWindow.WindowState = WindowState.Minimized
    End Sub

    Private Sub OnCloseButtonClick( sender As Object,
                                    e As RoutedEventArgs )
        Application.Current.MainWindow.Close()
    End Sub

    Private Sub OnTitleBarLeftMouseDown( sender As Object,
                                         e As MouseButtonEventArgs )
        Application.Current.MainWindow.DragMove()
    End Sub

    Private Sub OnTabItemsSelectionChanged( sender As Object,
                                            e As SelectionChangedEventArgs )
        If TabItems.SelectedIndex < 0 Then Return

        ' remove slide effects
        Dim slideEffects = TabView.OpeningEffects.Where(
            Function( p ) TypeOf p Is TransitionEffect And
                          (CType(p, TransitionEffect).Kind = TransitionEffectKind.SlideInFromLeft Or
                           CType(p, TransitionEffect).Kind = TransitionEffectKind.SlideInFromRight) ).ToList()
        For Each slideEffect As TransitionEffectBase In slideEffects
            TabView.OpeningEffects.Remove( slideEffect )
        Next

        ' add corresponding effect
        If TabItems.SelectedIndex < _storedSelectedIndex
            TabView.OpeningEffects.Add( New TransitionEffect( TransitionEffectKind.SlideInFromLeft ) )
        ElseIf TabItems.SelectedIndex > _storedSelectedIndex
            TabView.OpeningEffects.Add( New TransitionEffect( TransitionEffectKind.SlideInFromRight ) )
        End If
        _storedSelectedIndex = TabItems.SelectedIndex

        ' run effect
        TabView.Visibility = Visibility.Hidden
        TabView.Visibility = Visibility.Visible
    End Sub

    Private Sub OnTitleBarMouseDoubleClick( sender As Object,
                                            e As MouseButtonEventArgs )
        If e.ChangedButton = MouseButton.Left And e.GetPosition(Me).Y < 76
            OnZoomButtonClick( Nothing, Nothing )
        End If
    End Sub

    Private Sub OnPopupBoxLoaded( sender As Object,
                                  e As RoutedEventArgs )
        ' bug: fix bug of MaterialToolkit, PopupBox "silent open" on loaded
        Dim popupBox = CType(sender, PopupBox)

        popupBox.IsPopupOpen = True
        popupBox.IsPopupOpen = False
    End Sub
End Class

End Namespace
