Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Bills
Imports Hotelie.Domain.Rooms

Namespace Reports
    Public Class GetRoomUsedDaysReport
        Implements IGetRoomUsedDaysReport

        Private ReadOnly _billRepository As IBillRepository
        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository, billRepository As IBillRepository)
            _roomRepository = roomRepository
            _billRepository = billRepository
        End Sub

        Public Function Execute(fromDays As Date, toDays As Date) As List(Of RoomUsedReportModel) Implements IGetRoomUsedDaysReport.Execute
            Dim reports = New List(Of RoomUsedReportModel)
            Dim rooms = _roomRepository.GetAll().ToList()
            Dim bills = _billRepository.GetAll().ToList()

            For Each room As Room In rooms
                Dim days = 0
                For Each bill As Bill In bills
                    For Each billDetail As BillDetail In bill.Details
                        Dim checker = (billDetail.CheckinDate.Add(TimeSpan.FromDays(billDetail.NumberOfDays))>=fromDays _
                                       And billDetail.CheckinDate.Add(TimeSpan.FromDays(billDetail.NumberOfDays))<=toDays) _
                                       And billDetail.Lease.Room.Id = room.Id
                        If checker Then days + = billDetail.NumberOfDays
                    Next
                Next
                reports.Add(New RoomUsedReportModel() With {.Room = room.Name, .UsedDays = days})
            Next

            Return reports
        End Function

        Public Async Function ExecuteAsync(fromDays As Date, toDays As Date) As Task(Of List(Of RoomUsedReportModel)) Implements IGetRoomUsedDaysReport.ExecuteAsync
            Dim reports = New List(Of RoomUsedReportModel)
            Dim rooms = Await _roomRepository.GetAll().ToListAsync()
            Dim bills = Await _billRepository.GetAll().ToListAsync()

            For Each room As Room In rooms
                Dim days = 0
                For Each bill As Bill In bills
                    For Each billDetail As BillDetail In bill.Details
                        Dim checker = (billDetail.CheckinDate.Add(TimeSpan.FromDays(billDetail.NumberOfDays))>=fromDays _
                                       And billDetail.CheckinDate.Add(TimeSpan.FromDays(billDetail.NumberOfDays))<=toDays) _
                                      And billDetail.Lease.Room.Id = room.Id
                        If checker Then days + = billDetail.NumberOfDays
                    Next
                Next
                reports.Add(New RoomUsedReportModel() With {.Room = room.Name, .UsedDays = days})
            Next

            Return reports
        End Function
    End Class
End NameSpace