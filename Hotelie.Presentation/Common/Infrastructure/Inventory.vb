

Namespace Infrastructure
	Public Class Inventory
		Implements IInventory

		Private ReadOnly _roomCollectionPresenters As List(Of IRoomCollectionPresenter)

		Public Sub New()
			_roomCollectionPresenters = New List(Of IRoomCollectionPresenter)()
		End Sub

		Public Sub OnRoomAdded( id As String,
		                        name As String,
		                        categoryId As String,
		                        note As String ) Implements IInventory.OnRoomAdded
			For Each roomCollectionPresenter As IRoomCollectionPresenter In _roomCollectionPresenters
				roomCollectionPresenter.OnRoomAdded( id, name, categoryId, note )
			Next
		End Sub

		Public Sub OnRoomUpdated( id As String,
		                          name As String,
		                          categoryId As String,
		                          note As String,
		                          state As Int32 ) Implements IInventory.OnRoomUpdated
			For Each roomCollectionPresenter As IRoomCollectionPresenter In _roomCollectionPresenters
				roomCollectionPresenter.OnRoomUpdated( id, name, categoryId, note, state )
			Next
		End Sub

		Public Sub Track( roomCollectionPresenter As IRoomCollectionPresenter ) Implements IInventory.Track
			_roomCollectionPresenters.Add( roomCollectionPresenter )
		End Sub
	End Class
End Namespace
