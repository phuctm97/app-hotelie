Namespace Services.Persistence
	Public Interface IUnitOfWork
		Inherits IDisposable
		Sub Commit()
        Sub CommitAsync()
	End Interface
End Namespace