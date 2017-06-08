Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Bills
Imports Hotelie.Domain.Rooms

Namespace Reports
    Public Class GetRoomCategoriesProfit
        Implements IGetRoomCategoriesProfit

        Private ReadOnly _billRepository As IBillRepository
        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(billRepository As IBillRepository, roomRepository As IRoomRepository)
            _billRepository = billRepository
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(fromDays As Date, toDays As Date) As List(Of ProfitReportModel) Implements IGetRoomCategoriesProfit.Execute
            Dim reports = New List(Of ProfitReportModel)
            Dim bills = _billRepository.GetAll()
            
            Dim roomCategories  = _roomRepository.GetAllRoomCategories().ToList()

            For Each roomCategory As RoomCategory In roomCategories
                Dim profits = 0.0
                For Each bill As Bill In bills
                    For Each billDetail As BillDetail In bill.Details
                        Dim checker = (billDetail.CheckinDate.Add(TimeSpan.FromDays(billDetail.NumberOfDays))>=fromDays _
                            And billDetail.CheckinDate.Add(TimeSpan.FromDays(billDetail.NumberOfDays))<=toDays) _
                            And (billDetail.Lease.Room.Category.Id = roomCategory.Id)
                        If checker Then profits + = billDetail.TotalExpense
                    Next
                Next
                reports.Add(New ProfitReportModel() With {.Catetogry = roomCategory.Name, .Profit = profits})
            Next

            Return reports
        End Function

        Public Async Function ExecuteAsync(fromDays As Date, toDays As Date) As Task(Of List(Of ProfitReportModel)) _
            Implements IGetRoomCategoriesProfit.ExecuteAsync
            Dim reports = New List(Of ProfitReportModel)

            Dim bills = Await _billRepository.GetAll().ToListAsync()
            Dim roomCategories  = Await _roomRepository.GetAllRoomCategories().ToListAsync()

            For Each roomCategory As RoomCategory In roomCategories
                Dim profits = 0.0
                For Each bill As Bill In bills
                    For Each billDetail As BillDetail In bill.Details
                        Dim checker = (billDetail.CheckinDate.Add(TimeSpan.FromDays(billDetail.NumberOfDays))>=fromDays _
                                       And billDetail.CheckinDate.Add(TimeSpan.FromDays(billDetail.NumberOfDays))<=toDays) _
                                      And (billDetail.Lease.Room.Category.Id = roomCategory.Id)
                        If checker Then profits + = billDetail.TotalExpense
                    Next
                Next
                reports.Add(New ProfitReportModel() With {.Catetogry = roomCategory.Name, .Profit = profits})
            Next
            Return reports
        End Function
    End Class
End NameSpace