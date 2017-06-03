Imports Caliburn.Micro
Imports Hotelie.Domain.Leases

Namespace Leases.Queries
    Public Class LeaseCustomerModel
        Inherits PropertyChangedBase

        Private _id As String
        Private _name As String
        Private _address As String
        Private _lisenceId As String
        Private _categoryId As String
        Private _categoryName As String

        Public Property Id As String
            Get
                Return _id
            End Get
            Set
                If Equals(Value, _id) Then Return
                _id = Value
                NotifyOfPropertyChange(Function() Id)
            End Set
        End Property

        Public Property Name As String
            Get
                Return _name
            End Get
            Set
                If Equals(Value, _name) Then Return
                _name = Value
                NotifyOfPropertyChange(Function() Name)
            End Set
        End Property

        Public Property LisenceId As String
            Get
                Return _lisenceId
            End Get
            Set
                If Equals(Value, _lisenceId) Then Return
                _lisenceId = Value
                NotifyOfPropertyChange(Function() LisenceId)
            End Set
        End Property

        Public Property Address As String
            Get
                Return _address
            End Get
            Set
                If Equals(Value, _address) Then Return
                _address = Value
                NotifyOfPropertyChange(Function() Address)
            End Set
        End Property

        Public Property CategoryId As String
            Get
                Return _categoryId
            End Get
            Set
                If Equals(Value, _categoryId) Then Return
                _categoryId = Value
                NotifyOfPropertyChange(Function() CategoryId)
            End Set
        End Property

        Public Property CategoryName As String
            Get
                Return _categoryName
            End Get
            Set
                If Equals(Value, _categoryName) Then Return
                _categoryName = Value
                NotifyOfPropertyChange(Function() CategoryName)
            End Set
        End Property
    End Class
End NameSpace