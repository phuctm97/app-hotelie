Namespace Services.Persistence
	Public Interface IUnitOfWork
		Inherits IDisposable

		Sub Commit()

	End Interface
End Namespace