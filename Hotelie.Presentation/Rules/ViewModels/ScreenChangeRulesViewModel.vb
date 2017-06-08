Imports System.Collections.Specialized
Imports Hotelie.Application.Leases.Commands
Imports Hotelie.Application.Leases.Factories
Imports Hotelie.Application.Leases.Models
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Parameters.Commands
Imports Hotelie.Application.Parameters.Queries
Imports Hotelie.Application.Rooms.Commands
Imports Hotelie.Application.Rooms.Factories
Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Rules.Models
Imports Hotelie.Presentation.Start.MainWindow.Models
Imports MaterialDesignThemes.Wpf

Namespace Rules.ViewModels
    Public Class ScreenChangeRulesViewModel
        Inherits AppScreenHasSaving
        Implements INeedWindowModals

        ' Dependencies
        Private ReadOnly _getParametersQuery As IGetParametersQuery
        Private ReadOnly _getAllRoomCategoriesQuery As IGetAllRoomCategoriesQuery
        Private ReadOnly _getAllCustomerCategoriesQuery As IGetAllCustomerCategoriesQuery
        Private ReadOnly _updateParametersCommand As IUpdateParametersCommand
        Private ReadOnly _updateRoomCategoryCommand As IUpdateRoomCategoryCommand
        Private ReadOnly _updateCustomerCategoryCommand As IUpdateCustomerCategoryCommand
        Private ReadOnly _removeRoomCategoryCommand As IRemoveRoomCategoryCommand
        Private ReadOnly _removeCustomerCategoryCommand As IRemoveCustomerCategoryCommand
        Private ReadOnly _createRoomCategoryFactory As ICreateRoomCategoryCommand
        Private ReadOnly _createCustomerCategoryFactory As ICreateCustomerCategoryFactory

        ' Backing fields
        Private _isEdited As Boolean
        Private _originalRoomCapacity As Integer
        Private _originalExtraCoefficient As Double
        Private ReadOnly _originalCustomerCategories As List(Of EditableCustomerCategoryModel)
        Private ReadOnly _originalRoomCategories As List(Of EditableRoomCategoryModel)

        ' Bind models
        Public Property Rule As EditableRuleModel

        Public Property Username As String

        Public Sub New(getParametersQuery As IGetParametersQuery,
                       getAllRoomCategoriesQuery As IGetAllRoomCategoriesQuery,
                       getAllCustomerCategoriesQuery As IGetAllCustomerCategoriesQuery,
                       updateParametersCommand As IUpdateParametersCommand,
                       updateRoomCategoryCommand As IUpdateRoomCategoryCommand,
                       updateCustomerCategoryCommand As IUpdateCustomerCategoryCommand,
                       removeRoomCategoryCommand As IRemoveRoomCategoryCommand,
                       removeCustomerCategoryCommand As IRemoveCustomerCategoryCommand,
                       createRoomCategoryFactory As ICreateRoomCategoryCommand,
                       createCustomerCategoryFactory As ICreateCustomerCategoryFactory)
            MyBase.New(ColorZoneMode.Accent)
            _getParametersQuery = getParametersQuery
            _getAllRoomCategoriesQuery = getAllRoomCategoriesQuery
            _getAllCustomerCategoriesQuery = getAllCustomerCategoriesQuery
            _updateParametersCommand = updateParametersCommand
            _updateRoomCategoryCommand = updateRoomCategoryCommand
            _updateCustomerCategoryCommand = updateCustomerCategoryCommand
            _removeRoomCategoryCommand = removeRoomCategoryCommand
            _removeCustomerCategoryCommand = removeCustomerCategoryCommand
            _createRoomCategoryFactory = createRoomCategoryFactory
            _createCustomerCategoryFactory = createCustomerCategoryFactory

            _originalRoomCategories = New List(Of EditableRoomCategoryModel)()
            _originalCustomerCategories = New List(Of EditableCustomerCategoryModel)()

            DisplayName = "Thay đổi quy định"
            Username = "<tên người dùng>"
            Rule = New EditableRuleModel()
            Rule.RoomCapacity = 0
            Rule.ExtraCoefficient = 0
            _originalRoomCapacity = Rule.RoomCapacity
            _originalExtraCoefficient = Rule.ExtraCoefficient
        End Sub

        Private Sub ResetValues()
            Username = "<tên người dùng>"
            Rule.CustomerCategories.Clear()
            Rule.RoomCategories.Clear()
            Rule.RoomCapacity = 0
            Rule.ExtraCoefficient = 0
            _originalCustomerCategories.Clear()
            _originalRoomCategories.Clear()
            _originalRoomCapacity = Rule.RoomCapacity
            _originalExtraCoefficient = Rule.ExtraCoefficient

            RemoveHandler Rule.RoomCategories.CollectionChanged, AddressOf OnRulesChanged
            RemoveHandler Rule.CustomerCategories.CollectionChanged, AddressOf OnRulesChanged
            _isEdited = False
        End Sub

        ' Show
        Public Overrides Sub Show()
            ReloadRules()
        End Sub

        Private Sub ReloadRules()
            Dim model = _getParametersQuery.Execute()
            Rule.ExtraCoefficient = model.ExtraCoefficient
            Rule.RoomCapacity = model.RoomCapacity

            Dim customerCategories = _getAllCustomerCategoriesQuery.Execute()
            Rule.CustomerCategories.Clear()
            Rule.CustomerCategories.AddRange(
                customerCategories.Select(Function(c) New EditableCustomerCategoryModel With {
                                             .Id = c.Id, .Name = c.Name, .Coefficient = c.Coefficient}))

            _originalCustomerCategories.Clear()
            _originalCustomerCategories.AddRange(Rule.CustomerCategories)
            AddHandler Rule.CustomerCategories.CollectionChanged, AddressOf OnRulesChanged

            Dim roomCategories = _getAllRoomCategoriesQuery.Execute()
            Rule.RoomCategories.Clear()
            Rule.RoomCategories.AddRange(roomCategories.Select(Function(r) New EditableRoomCategoryModel With {
                                                                  .Id = r.Id, .Name = r.Name, .UnitPrice = r.UnitPrice}))

            _originalRoomCategories.Clear()
            _originalRoomCategories.AddRange(Rule.RoomCategories)
            AddHandler Rule.RoomCategories.CollectionChanged, AddressOf OnRulesChanged
        End Sub

        Public Overrides Async Function ShowAsync() As Task
            Await ReloadRulesAsync()
        End Function

        Private Async Function ReloadRulesAsync() As Task
            Dim model = Await _getParametersQuery.ExecuteAsync()
            Rule.ExtraCoefficient = model.ExtraCoefficient
            Rule.RoomCapacity = model.RoomCapacity

            Dim customerCategories = Await _getAllCustomerCategoriesQuery.ExecuteAsync()
            Rule.CustomerCategories.Clear()
            Rule.CustomerCategories.AddRange(
                customerCategories.Select(Function(c) New EditableCustomerCategoryModel With {
                                             .Id = c.Id, .Name = c.Name, .Coefficient = c.Coefficient}))
            _originalCustomerCategories.Clear()
            _originalCustomerCategories.AddRange(Rule.CustomerCategories)
            AddHandler Rule.CustomerCategories.CollectionChanged, AddressOf OnRulesChanged

            Dim roomCategories = Await _getAllRoomCategoriesQuery.ExecuteAsync()
            Rule.RoomCategories.Clear()
            Rule.RoomCategories.AddRange(roomCategories.Select(Function(r) New EditableRoomCategoryModel With {
                                                                  .Id = r.Id, .Name = r.Name, .UnitPrice = r.UnitPrice}))
            _originalRoomCategories.Clear()
            _originalRoomCategories.AddRange(Rule.RoomCategories)
            AddHandler Rule.RoomCategories.CollectionChanged, AddressOf OnRulesChanged
        End Function

        Private Sub OnRulesChanged(sender As Object,
                                   e As NotifyCollectionChangedEventArgs)
            _isEdited = True
        End Sub

        ' Domain actions
        Public Sub AddEmptyRoomCategory()
            Rule.RoomCategories.Add(New EditableRoomCategoryModel)
        End Sub

        Public Sub AddEmptyCustomerCategory()
            Rule.CustomerCategories.Add(New EditableCustomerCategoryModel)
        End Sub

        ' Exit
        Public Overrides ReadOnly Property IsEdited As Boolean
            Get
                Return _isEdited OrElse CheckForPendingChanges()
            End Get
        End Property

        Public Function CheckForPendingChanges() As Boolean
            Return (Not Equals(Rule.ExtraCoefficient, _originalExtraCoefficient)) Or
                   (Not Equals(Rule.RoomCapacity, _originalRoomCapacity))
        End Function

        Public Overrides Function ActualExitAsync() As Task
            ResetValues()
            Return MyBase.ActualExitAsync()
        End Function

        ' Save
        Public Overrides Function CanSave() As Task(Of Boolean)
            Return Task.Run(Function() ValidateSaving())
        End Function

        Private Function ValidateSaving() As Boolean
            If Rule.RoomCapacity <= 0
                ShowStaticBottomNotification(StaticNotificationType.Information,
                                             "Số khách tối đa phải lớn hơn hoặc bằng 1")
                Return False
            End If

            Return True
        End Function

        Public Overrides Async Function ActualSaveAsync() As Task
            ' try update parameters
            Dim err = Await _updateParametersCommand.ExecuteAsync(Rule.RoomCapacity, Rule.ExtraCoefficient)

            If Not String.IsNullOrEmpty(err)
                ShowStaticBottomNotification(StaticNotificationType.Error, err)
                Return
            End If
        End Function
    End Class
End Namespace