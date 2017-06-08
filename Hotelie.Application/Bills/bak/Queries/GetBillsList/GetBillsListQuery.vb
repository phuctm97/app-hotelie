Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Bills
Imports Hotelie.Domain.Leases

Namespace Bills.Queries.GetBillsList
    Public Class GetBillsListQuery
        Implements IGetBillsListQuery

        Private ReadOnly _billRepository As IBillRepository

        Sub New(billRepository As IBillRepository)
            _billRepository = billRepository
        End Sub

        Public Function Execute() As IEnumerable(Of BillsListItemModel) Implements IGetBillsListQuery.Execute
            Dim bills = _billRepository.GetAll().Select(Function(r) New BillsListItemModel() With _
                                                            {.Id = r.Id,
                                                            .CustomerName = r.CustomerName,
                                                            .TotalPrice = r.TotalExpense}).ToList()

            For Each billModel As BillsListItemModel In bills
               Dim bill = _billRepository.GetOne(billModel.Id)
                For Each billDetail As BillDetail In bill.Details

                    billModel.BillDetails.Add(New BillsListItemDetailModel() With _
                                              {.RoomPrice=billDetail.Lease.RoomPrice,
                                              .CheckinDate = billDetail.Lease.CheckinDate,
                                              .LeaseId = billDetail.Lease.Id,
                                              .NumberOfDays=billDetail.NumberOfDays,
                                              .RoomName = billDetail.Lease.Room.Name})

                    For Each billDetailModel As BillsListItemDetailModel In billModel.BillDetails
                        For Each leaseDetail As LeaseDetail In billDetail.Lease.LeaseDetails
                            billDetailModel.CustomerNames.Add(leaseDetail.CustomerName)
                        Next
                    Next
                Next
            Next

            Return bills
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of BillsListItemModel)) Implements IGetBillsListQuery.ExecuteAsync
            Dim bills = Await _billRepository.GetAll().Select(Function(r) New BillsListItemModel() With _
                                                           {.Id = r.Id,
                                                           .CustomerName = r.CustomerName,
                                                           .TotalPrice = r.TotalExpense}).ToListAsync()

            For Each billModel As BillsListItemModel In bills
                Dim bill = Await _billRepository.GetOneAsync(billModel.Id)
                For Each billDetail As BillDetail In bill.Details

                    billModel.BillDetails.Add(New BillsListItemDetailModel() With _
                                                 {.RoomPrice=billDetail.Lease.RoomPrice,
                                                 .CheckinDate = billDetail.Lease.CheckinDate,
                                                 .LeaseId = billDetail.Lease.Id,
                                                 .NumberOfDays=billDetail.NumberOfDays,
                                                 .RoomName = billDetail.Lease.Room.Name})

                    For Each billDetailModel As BillsListItemDetailModel In billModel.BillDetails
                        For Each leaseDetail As LeaseDetail In billDetail.Lease.LeaseDetails
                            billDetailModel.CustomerNames.Add(leaseDetail.CustomerName)
                        Next
                    Next
                Next
            Next

            Return bills
        End Function
    End Class
End NameSpace