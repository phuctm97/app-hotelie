Imports Hotelie.Application.Services.Persistence

Namespace Leases.Queries
    Public Class GetLeasesListQuery
        Implements IGetLeasesListQuery

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute() As IEnumerable(Of LeaseModel) Implements IGetLeasesListQuery.Execute
            Dim leases = _leaseRepository.GetAll().Select(Function(p) New LeaseModel() With _
                                                        {.Id = p.Id,
                                                        .RoomId = p.Room.Id,
                                                        .BeginDate = p.BeginDate,
                                                        .EndDate = p.EndDate,
                                                        .ExtraCoefficient = p.ExtraCoefficient,
                                                        .CustomerCoefficient = p.CustomerCoefficient,
                                                        .ExtraCharge = p.ExtraCharge,
                                                        .NumberOfDate = p.NumberOfDate,
                                                        .BillId = p.Bill.Id})
            For Each leaseModel As LeaseModel In leases

            Next
            Return leases
        End Function
    End Class
End NameSpace