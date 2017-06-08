Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Models

Namespace Leases.Models
	Public Class FilterLeaseModel
		Inherits PropertyChangedBase

		Private _anyText As String

		Public Property AnyText As String
			Get
				Return _anyText
			End Get
			Set
				If Equals( Value, _anyText ) Then Return
				_anyText = value
				NotifyOfPropertyChange( Function() AnyText )
			End Set
		End Property

		Public Sub New()
			AnyText = String.Empty
		End Sub

		Public Function IsMatch( lease As ILeaseModel ) As Boolean
			Dim text = AnyText.ToLower()

			If String.IsNullOrWhiteSpace( text ) Then Return True
			If lease.IdEx.ToLower().Contains( text ) Then Return True
			If lease.Room.Name.ToLower().Contains( text ) Then Return True

			Dim expense As Decimal = - 1
			If Decimal.TryParse( text, expense )
				Return lease.TotalExpense >= expense
			End If

			For Each s As String In {"/", "\", "-"}
				If text.Contains( s )
					Dim x1, x2 As Integer
					If Integer.TryParse( text.Split( s )( 0 ), x1 ) And
					   Integer.TryParse( text.Split( s )( 1 ), x2 )
						If x2 < 2000 Or x1 <= 0 Then Continue For

						Try
							Return lease.CheckinDate >= New Date( x2, x1, 1 )
						Catch
						End Try
					End If
				End If
			Next

			For Each detail As ILeaseDetailModel In lease.Details
				If detail.CustomerName.ToLower().Contains( text ) Then Return True
			Next

			Return False
		End Function
	End Class
End Namespace