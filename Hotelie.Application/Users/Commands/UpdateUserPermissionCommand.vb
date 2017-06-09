Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users

Namespace Users.Commands
    Public Class UpdateUserPermissionCommand
        Implements IUpdateUserPermissionCommand

        Private ReadOnly _userRepository As IUserRepository
        Private ReadOnly _unitOfWork As IUnitOfWork
        Private ReadOnly _permissionRepository As IPermissionRepository

        Sub New(userRepository As IUserRepository, unitOfWork As IUnitOfWork,
                permissionRepository As IPermissionRepository)
            _userRepository = userRepository
            _unitOfWork = unitOfWork
            _permissionRepository = permissionRepository
        End Sub

        Public Function Execute(id As String, canConfigRoom As Boolean, canAddLease As Boolean, canEditLease As Boolean,
                                canRemoveLease As Boolean, canManageUser As Boolean, canEditRules As Boolean) As String _
            Implements IUpdateUserPermissionCommand.Execute
            Try
                Dim user = _userRepository.GetOne(id)
                If IsNothing(user) Then Return "Không tìm thấy user"

                ' remove all user permission

                _permissionRepository.RemovePermissionsOfUser(user)
                _unitOfWork.Commit()

                ' get 6 new user permission id

                Dim defaultDetailId = 1
                Dim qu = True
                Dim userPermissionList = _permissionRepository.GetAll().ToList()
                Dim idList = New List(Of String)
                For i = 0 To 5
                    Dim newUserPermissionId = String.Empty
                    If userPermissionList.Count = 0 And qu Then
                        idList.Add(1.ToString("00000"))
                        qu = False
                    Else
                        Do While newUserPermissionId = Nothing
                            defaultDetailId += 1
                            Dim newIdCheck = defaultDetailId.ToString("00000")
                            Dim q = True
                            For Each unit As UserPermission In userPermissionList
                                If (unit.Id = newIdCheck) Then
                                    q = False
                                    Exit For
                                End If
                            Next
                            If q Then newUserPermissionId = newIdCheck
                        Loop
                        idList.Add(newUserPermissionId)
                    End If
                Next

                ' check and insert permission of user

                Dim j = 0
                If canConfigRoom Then
                    Dim permission = _permissionRepository.GetPermissionType("PM001")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canAddLease Then
                    Dim permission = _permissionRepository.GetPermissionType("PM002")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canEditLease Then
                    Dim permission = _permissionRepository.GetPermissionType("PM003")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canRemoveLease Then
                    Dim permission = _permissionRepository.GetPermissionType("PM004")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canManageUser Then
                    Dim permission = _permissionRepository.GetPermissionType("PM005")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canEditRules Then
                    Dim permission = _permissionRepository.GetPermissionType("PM006")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                End If

                _unitOfWork.Commit()

                Return String.Empty
            Catch
                Return "Hiện tại không thể cập nhật quyền cho tài khoản"
            End Try
        End Function

        Public Async Function ExecuteAsync(id As String, canConfigRoom As Boolean, canAddLease As Boolean,
                                           canEditLease As Boolean, canRemoveLease As Boolean, canManageUser As Boolean,
                                           canEditRules As Boolean) As Task(Of String) _
            Implements IUpdateUserPermissionCommand.ExecuteAsync
            Try
                Dim user = Await _userRepository.GetOneAsync(id)
                If IsNothing(user) Then Return "Không tìm thấy tài khoản"
                ' remove all current user permission of user

                _permissionRepository.RemovePermissionsOfUser(user)
                Await _unitOfWork.CommitAsync()

                ' get 6 new user permission id
                Dim defaultDetailId = 1
                Dim qu = True
                Dim userPermissionList = Await _permissionRepository.GetAll().ToListAsync()
                Dim idList = New List(Of String)
                For i = 0 To 5
                    Dim newUserPermissionId = String.Empty
                    If userPermissionList.Count = 0 And qu Then
                        idList.Add(1.ToString("00000"))
                        qu = False
                    Else
                        Do While newUserPermissionId = Nothing
                            defaultDetailId += 1
                            Dim newIdCheck = defaultDetailId.ToString("00000")
                            Dim q = True
                            For Each unit As UserPermission In userPermissionList
                                If (unit.Id = newIdCheck) Then
                                    q = False
                                    Exit For
                                End If
                            Next
                            If q Then newUserPermissionId = newIdCheck
                        Loop
                        idList.Add(newUserPermissionId)
                    End If
                Next

                ' check and insert permission of user

                Dim j = 0
                If canConfigRoom Then
                    Dim permission = _permissionRepository.GetPermissionType("PM001")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canAddLease Then
                    Dim permission = _permissionRepository.GetPermissionType("PM002")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canEditLease Then
                    Dim permission = _permissionRepository.GetPermissionType("PM003")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canRemoveLease Then
                    Dim permission = _permissionRepository.GetPermissionType("PM004")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canManageUser Then
                    Dim permission = _permissionRepository.GetPermissionType("PM005")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                    j += 1
                End If
                If canEditRules Then
                    Dim permission = _permissionRepository.GetPermissionType("PM006")
                    _permissionRepository.Add(New UserPermission() With {.Id = idList(j), .Permission=permission})
                End If

                Await _unitOfWork.CommitAsync()

                Return String.Empty
            Catch
                Return "Hiện tại không thể cập nhật tài khoản"
            End try
        End Function
    End Class
End NameSpace