Imports System.Collections.ObjectModel

Public Class RoomsView
	' ReSharper disable once UnassignedGetOnlyAutoProperty
	' ReSharper disable once CollectionNeverUpdated.Global
	Public ReadOnly Property RoomCategories As ObservableCollection(Of RoomCategory)

	' ReSharper disable once UnassignedGetOnlyAutoProperty
	' ReSharper disable once CollectionNeverUpdated.Global
	Public ReadOnly Property Rooms As ObservableCollection(Of RoomModel)

	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		InitRoomCategories( RoomCategories )

		InitRooms( Rooms )

		DataContext = Me
	End Sub

	Private Sub InitRoomCategories( ByRef roomCategoryModels As ObservableCollection(Of RoomCategory) )
		roomCategoryModels = New ObservableCollection(Of RoomCategory)()

		roomCategoryModels.Add( New RoomCategory With {.Id="1", .Name="Phòng thường 1"} )
		roomCategoryModels.Add( New RoomCategory With {.Id="2", .Name="Phòng thường 2"} )
		roomCategoryModels.Add( New RoomCategory With {.Id="3", .Name="Phòng thường 3"} )

		roomCategoryModels.Add( New RoomCategory With {.Id="4", .Name="Phòng trung cấp 1"} )
		roomCategoryModels.Add( New RoomCategory With {.Id="5", .Name="Phòng trung cấp 2"} )
		roomCategoryModels.Add( New RoomCategory With {.Id="6", .Name="Phòng trung cấp 3"} )

		roomCategoryModels.Add( New RoomCategory With {.Id="7", .Name="Phòng Vip 1"} )
		roomCategoryModels.Add( New RoomCategory With {.Id="8", .Name="Phòng Vip 2"} )
		roomCategoryModels.Add( New RoomCategory With {.Id="9", .Name="Phòng Vip 3"} )
	End Sub

	Private Sub InitRooms( ByRef roomModels As ObservableCollection(Of RoomModel) )
		roomModels = New ObservableCollection(Of RoomModel)()

		roomModels.Add( New RoomModel With{.Id="1", .Name="A101", .Category=RoomCategories( 0 )} )
		roomModels.Add( New RoomModel With{.Id="2", .Name="A102", .Category=RoomCategories( 0 )} )
		roomModels.Add( New RoomModel With{.Id="3", .Name="A103", .Category=RoomCategories( 1 )} )
		roomModels.Add( New RoomModel With{.Id="4", .Name="A104", .Category=RoomCategories( 1 )} )

		roomModels.Add( New RoomModel With{.Id="5", .Name="A201", .Category=RoomCategories( 2 )} )
		roomModels.Add( New RoomModel With{.Id="6", .Name="A202", .Category=RoomCategories( 2 )} )
		roomModels.Add( New RoomModel With{.Id="7", .Name="A203", .Category=RoomCategories( 3 )} )
		roomModels.Add( New RoomModel With{.Id="8", .Name="A204", .Category=RoomCategories( 3 )} )

		roomModels.Add( New RoomModel With{.Id="9", .Name="A301", .Category=RoomCategories( 3 )} )
		roomModels.Add( New RoomModel With{.Id="10", .Name="A302", .Category=RoomCategories( 3 )} )
		roomModels.Add( New RoomModel With{.Id="11", .Name="A303", .Category=RoomCategories( 4 )} )
		roomModels.Add( New RoomModel With{.Id="12", .Name="A304", .Category=RoomCategories( 4 )} )

		roomModels.Add( New RoomModel With{.Id="13", .Name="A401", .Category=RoomCategories( 5 )} )
		roomModels.Add( New RoomModel With{.Id="14", .Name="A402", .Category=RoomCategories( 5 )} )
		roomModels.Add( New RoomModel With{.Id="15", .Name="A403", .Category=RoomCategories( 6 )} )
		roomModels.Add( New RoomModel With{.Id="16", .Name="A404", .Category=RoomCategories( 6 )} )

		roomModels.Add( New RoomModel With{.Id="17", .Name="A501", .Category=RoomCategories( 7 )} )
		roomModels.Add( New RoomModel With{.Id="18", .Name="A502", .Category=RoomCategories( 7 )} )
		roomModels.Add( New RoomModel With{.Id="19", .Name="A503", .Category=RoomCategories( 8 )} )
		roomModels.Add( New RoomModel With{.Id="20", .Name="A504", .Category=RoomCategories( 8 )} )
	End Sub
End Class
