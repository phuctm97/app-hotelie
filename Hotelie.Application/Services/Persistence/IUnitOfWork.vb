Namespace Services.Persistence
	Public Interface IUnitOfWork
		Inherits IDisposable
		Sub Commit()
        Function CommitAsync() As Task(Of Integer)
	End Interface
End Namespace