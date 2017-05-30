Imports System.Globalization

Namespace Rooms.Converters
    Public Class StateConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim state As Integer = value 

            If (state=-1) Then Return "Busy"
            If (state=0) Then Return "Pending"
            If (state=1) Then Return "Free"

            Return "Unknow"

        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim state As String = value

            if (state="Free") Then Return 1
            If (state="Pending") Then Return 0
            If (state="Busy") Then Return -1

            Return -2
        End Function
    End Class
End Namespace
