Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Factories
    Public Class CreateLeaseFactory
        Implements ICreateLeaseFactory

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _unitOfWork As IUnitOfWork
        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork, roomRepository As IRoomRepository)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(roomId As String, beginDate As Date, endDate As Date) As LeaseModel _
            Implements ICreateLeaseFactory.Execute
            Dim room = _roomRepository.GetOne(roomId)

            ' create new lease id
            Dim defaultId = 0
            Dim newId As String = Nothing
            Dim leases = _leaseRepository.GetAll().ToList()

            If (leases.Count = 0) Then
                newId = "LS"+1.ToString("000")
            Else
                Do While newId = Nothing
                    defaultId += 1
                    Dim newIdCheck = "LS" + defaultId.ToString("000")
                    Dim q = True
                    For Each unit As Lease In leases
                        If (unit.Id = newIdCheck) Then 
                            q = False
                            Exit For
                        End If
                    Next
                    If q Then newId = newIdCheck
                Loop
            End If

            ' new lease initialize
            Dim lease = New LeaseModel() With { _
                    .Id = newId,
                    .RoomId = roomId,
                    .BeginDate = beginDate,
                    .EndDate = endDate,
                    .Price = room.Category.Price
                    }

            ' add new lease to database
            _leaseRepository.Add(
                New Lease() _
                                    With {.Id = lease.Id, .Room=room, .BeginDate = lease.BeginDate,
                                    .EndDate = lease.EndDate, .Price = lease.Price})
            _unitOfWork.Commit()

            Return lease
        End Function

        Public Async Function ExecuteAsync(roomId As String, beginDate As Date, endDate As Date) As Task(Of LeaseModel) Implements ICreateLeaseFactory.ExecuteAsync
            Dim room = Await _roomRepository.GetOneAsync(roomId)

            ' create new lease id
            Dim defaultId = 0
            Dim newId As String = Nothing
            Dim leases = _leaseRepository.GetAll().ToList()

            If (leases.Count = 0) Then
                newId = "LS"+1.ToString("000")
            Else
                Do While newId = Nothing
                    defaultId += 1
                    Dim newIdCheck = "LS" + defaultId.ToString("000")
                    Dim q = True
                    For Each unit As Lease In leases
                        If (unit.Id = newIdCheck) Then 
                            q = False
                            Exit For
                        End If
                    Next
                    If q Then newId = newIdCheck
                Loop
            End If

            ' new lease initialize
            Dim lease = New LeaseModel() With { _
                    .Id = newId,
                    .RoomId = roomId,
                    .BeginDate = beginDate,
                    .EndDate = endDate,
                    .Price = room.Category.Price
                    }

            ' add new lease to database
            _leaseRepository.Add(
                New Lease() _
                                    With {.Id = lease.Id, .Room=room, .BeginDate = lease.BeginDate,
                                    .EndDate = lease.EndDate, .Price = lease.Price})
            Await _unitOfWork.CommitAsync()

            Return lease
        End Function
    End Class
End NameSpace