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
                Return False
            End Get
        End Property


        Public ReadOnly Property CouldAddLease As Boolean Implements IUserModel.CouldAddLease
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property CouldEditLease As Boolean Implements IUserModel.CouldEditLease
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property CouldRemoveLease As Boolean Implements IUserModel.CouldRemoveLease
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property CouldManageUser As Boolean Implements IUserModel.CouldManageUser
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property CouldEditRule As Boolean Implements IUserModel.CouldEditRule
            Get
                Return False
            End Get
        End Property
    End Class
End NameSpace