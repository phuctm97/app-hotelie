Namespace Infrastructure
	Public Interface IRoomCollectionPresenter
		Sub OnRoomAdded( id As String,
		                 name As String,
		                 categoryId As String,
		                 note As String )

		Sub OnRoomUpdated( id As String,
		                   name As String,
		                   categoryId As String,
		                   note As String,
		                   state As Integer )

		Sub OnRoomRemoved( id As String )
	End Interface
End Namespace