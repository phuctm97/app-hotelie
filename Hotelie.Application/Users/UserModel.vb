Imports Hotelie.Domain.Users

Namespace Users
    Public Class UserModel
        Implements  IUserModel

        Private ReadOnly _entity As User

        Sub New(entity As User)
            _entity = entity
        End Sub

        Public ReadOnly Property CouldConfigRoom As Boolean Implements IUserModel.CouldConfigRoom
            Get
                If _entity.Permissions.Exists(Function(p)p.Id = "PM001") Then Return True
                Return False
            End Get
        End Property


        Public ReadOnly Property CouldAddLease As Boolean Implements IUserModel.CouldAddLease
            Get
                If _entity.Permissions.Exists(Function(p)p.Id = "PM002") Then Return True
                Return False
            End Get
        End Property

        Public ReadOnly Property CouldEditLease As Boolean Implements IUserModel.CouldEditLease
            Get
                If _entity.Permissions.Exists(Function(p)p.Id = "PM003") Then Return True
                Return False
            End Get
        End Property

        Public ReadOnly Property CouldRemoveLease As Boolean Implements IUserModel.CouldRemoveLease
            Get
                If _entity.Permissions.Exists(Function(p)p.Id = "PM004") Then Return True
                Return False
            End Get
        End Property

        Public ReadOnly Property CouldManageUser As Boolean Implements IUserModel.CouldManageUser
            Get
                If _entity.Permissions.Exists(Function(p)p.Id = "PM005") Then Return True
                Return False
            End Get
        End Property

        Public ReadOnly Property CouldEditRule As Boolean Implements IUserModel.CouldEditRule
            Get
                If _entity.Permissions.Exists(Function(p)p.Id = "PM006") Then Return True
                Return False
            End Get
        End Property
    End Class
End NameSpace