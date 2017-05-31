Namespace Services.Persistence
	Public Interface IUnitOfWork
		Inherits IDisposable

		ReadOnly Property RoomRepository As IRoomRepository
		ReadOnly Property UserRepository As IUserRepository

		Sub Commit()

		Sub Rollback()
	End Interface
End Namespace