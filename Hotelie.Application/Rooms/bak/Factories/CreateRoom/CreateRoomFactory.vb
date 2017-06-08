Imports System.Windows.Media
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms

Namespace Rooms.Factories.CreateRoom
    Public Class CreateRoomFactory
        Implements ICreateRoomFactory

        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(roomRepository As IRoomRepository, unitOfWork As IUnitOfWork)
            _unitOfWork = unitOfWork
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(name As String, categoryId As String, note As String) As String _
            Implements ICreateRoomFactory.Execute

            ' get category has Id = categoryId
            Dim category = _roomRepository.GetRoomCategory(categoryId)

            ' create new room id
            Dim defaultId = 0
            Dim newId As String = Nothing
            Dim rooms = _roomRepository.GetAll().ToList()

            If (rooms.Count = 0) Then
                newId = "PH" + 1.ToString("000")
            Else
                Do While newId = Nothing
                    defaultId += 1
                    Dim newIdCheck = "PH" + defaultId.ToString("000")
                    Dim q = True
                    For Each unit As Room In rooms
                        If (unit.Id = newIdCheck) Then
                            q = False
                            Exit For
                        End If
                    Next
                    If q Then newId = newIdCheck
                Loop
            End If

            ' add new room to database
            _roomRepository.Add(
                New Room() _
                                   With {.Id =newId,.Category=category,.Name=name,
                                   .Note=note, .State=0})
            _unitOfWork.Commit()

            Return newId
        End Function

        Public Async Function ExecuteAsync(name As String, categoryId As String, note As String) As Task(Of String) _
            Implements ICreateRoomFactory.ExecuteAsync
            ' get category has Id = categoryId
            Dim category = Await _roomRepository.GetRoomCategoryAsync(categoryId)

            ' create new room id
            Dim defaultId = 0
            Dim newId As String = Nothing
            Dim rooms = _roomRepository.GetAll().ToList()

            If (rooms.Count = 0) Then
                newId = "PH" + 1.ToString("000")
            Else
                Do While newId = Nothing
                    defaultId += 1
                    Dim newIdCheck = "PH" + defaultId.ToString("000")
                    Dim q = True
                    For Each unit As Room In rooms
                        If (unit.Id = newIdCheck) Then
                            q = False
                            Exit For
                        End If
                    Next
                    If q Then newId = newIdCheck
                Loop
            End If

        

            ' add new room to database
            _roomRepository.Add(
                New Room() _
                                   With {.Id =newId,.State=0,.Category=category,.Name=name,
                                   .Note=note})
            Await _unitOfWork.CommitAsync()

            Return newId
        End Function
    End Class
End NameSpace