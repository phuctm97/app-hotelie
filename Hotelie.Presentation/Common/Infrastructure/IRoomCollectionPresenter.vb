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
	End Interface
End Namespace