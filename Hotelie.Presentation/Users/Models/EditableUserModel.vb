Imports Caliburn.Micro

Namespace Users.ViewModels
	Public Class EditableUserModel
		Inherits PropertyChangedBase

		Private _username As String
		Private _password As String
		Private _couldConfigRoom As Boolean
		Private _couldAddLease As Boolean
		Private _couldEditLease As Boolean
		Private _couldRemoveLease As Boolean
		Private _couldManageUser As Boolean
		Private _couldEditRule As Boolean

		Public Property Username As String
			Get
				Return _username
			End Get
			Set
				If Equals( Value, _username ) Then Return
				_username = value
				NotifyOfPropertyChange( Function() Username )
			End Set
		End Property

		Public Property Password As String
			Get
				Return _password
			End Get
			Set
				If Equals( Value, _password ) Then Return
				_password = value
				NotifyOfPropertyChange( Function() Password )
			End Set
		End Property

		Public Property CouldConfigRoom As Boolean
			Get
				Return _couldConfigRoom
			End Get
			Set
				If Equals( Value, _couldConfigRoom ) Then Return
				_couldConfigRoom = value
				NotifyOfPropertyChange( Function() CouldConfigRoom )
			End Set
		End Property

		Public Property CouldAddLease As Boolean
			Get
				Return _couldAddLease
			End Get
			Set
				If Equals( Value, _couldAddLease ) Then Return
				_couldAddLease = value
				NotifyOfPropertyChange( Function() CouldAddLease )
			End Set
		End Property

		Public Property CouldEditLease As Boolean
			Get
				Return _couldEditLease
			End Get
			Set
				If Equals( Value, _couldEditLease ) Then Return
				_couldEditLease = value
				NotifyOfPropertyChange( Function() CouldEditLease )
			End Set
		End Property

		Public Property CouldRemoveLease As Boolean
			Get
				Return _couldRemoveLease
			End Get
			Set
				If Equals( Value, _couldRemoveLease ) Then Return
				_couldRemoveLease = value
				NotifyOfPropertyChange( Function() CouldRemoveLease )
			End Set
		End Property

		Public Property CouldManageUser As Boolean
			Get
				Return _couldManageUser
			End Get
			Set
				If Equals( Value, _couldManageUser ) Then Return
				_couldManageUser = value
				NotifyOfPropertyChange( Function() CouldManageUser )
			End Set
		End Property

		Public Property CouldEditRule As Boolean
			Get
				Return _couldEditRule
			End Get
			Set
				If Equals( Value, _couldEditRule ) Then Return
				_couldEditRule = value
				NotifyOfPropertyChange( Function() CouldEditRule )
			End Set
		End Property

		Public Sub New()
			Username = String.Empty
			Password = String.Empty
			CouldConfigRoom = False
			CouldAddLease = False
			CouldEditLease = False
			CouldRemoveLease = False
			CouldManageUser = False
			CouldEditRule = False
		End Sub
	End Class
End Namespace