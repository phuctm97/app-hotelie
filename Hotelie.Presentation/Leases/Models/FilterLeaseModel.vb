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
			If String.IsNullOrWhiteSpace( AnyText ) Then Return True
			If lease.Id.Contains( AnyText ) Then Return True
			If lease.Room.Name.Contains( AnyText ) Then Return True

			For Each detail As ILeaseDetailModel In lease.Details
				If detail.CustomerName.Contains( AnyText ) Then Return True
			Next

			Return False
		End Function
	End Class
End Namespace