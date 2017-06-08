Namespace Reports
    Public Interface IGetRoomUsedDaysReport
        Function Execute(fromDays As Date, toDays As Date) As List(Of RoomUsedReportModel)
        Function ExecuteAsync(fromDays As Date, toDays As Date) As Task(Of List(Of RoomUsedReportModel))
    End Interface
End NameSpace