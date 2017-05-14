Namespace Services.Persistence
	Public Interface IUnitOfWork
		Inherits IDisposable

		ReadOnly Property RoomRepository As IRoomRepository

		Sub Commit()

		Sub Rollback()
	End Interface
End Namespace