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
			If lease.Id.ToLower().Contains( text ) Then Return True
			If lease.Room.Name.ToLower().Contains( text ) Then Return True

			Dim expense As Decimal = - 1
			If Decimal.TryParse( text, expense )
				Return lease.ToString >= expense
			End If

			Dim checkinDate As Date = Today
			If Date.TryParse( text, checkinDate )
				Return lease.CheckinDate > checkinDate
			End If

			For Each detail As ILeaseDetailModel In lease.Details
				If detail.CustomerName.ToLower().Contains( text ) Then Return True
			Next

			Return False
		End Function
	End Class
End Namespace