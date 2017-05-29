Imports System.Collections.ObjectModel
Imports Caliburn.Micro

Namespace Rooms.Queries.GetRoomsList
    Public Class ExampleRoomsListItem
        Implements IGetRoomsListQuery

        Public Function Execute() As IEnumerable(Of RoomsListItemModel) Implements IGetRoomsListQuery.Execute
                Dim rooms = new BindableCollection(Of RoomsListItemModel)
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 170.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 180.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 190.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 250.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 350.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 450.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 550.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 650.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 750.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 770.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 710.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 720.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 730.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 740.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 750.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 760.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 770.000",.State="Available"})
            rooms.Add(new RoomsListItemModel() With{.Name="101",.CategoryName="VIP",.Id="PH001",.Note="đ 750.000",.State="Available"})
            Return rooms
        End Function
    End Class
End Namespace