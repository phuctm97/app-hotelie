Namespace Reports
    Public Interface IGetRoomCategoriesProfit
        Function Execute(fromDays As Date, toDays As Date) As List(Of ProfitReportModel)
        Function ExecuteAsync(month As Date, toDays As Date) As Task(Of List(Of ProfitReportModel))
    End Interface
End NameSpace