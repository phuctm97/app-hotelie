Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms

Namespace Rooms.Factories.CreateRoomCategory
    Public Class CreateRoomCategoryFactory
        Implements ICreateRoomCategoryFactory

        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(roomRepository As IRoomRepository, unitOfWork As IUnitOfWork)
            _roomRepository = roomRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(name As String, price As Double) As String Implements ICreateRoomCategoryFactory.Execute
            Try
            ' create new room category id
                Dim rooms = _roomRepository.GetAllRoomCategories().ToList()
                Dim defaultId = 0
                Dim newId = String.Empty

                If rooms.Count = 0 Then
                    newId = "RC" + 1.ToString("000")
                Else
                    Do While newId = Nothing
                        defaultId += 1
                        Dim newIdCheck = "RC" + defaultId.ToString("000")
                        Dim q = True
                        For Each unit As RoomCategory In rooms
                            If (unit.Id = newIdCheck) Then
                                q = False
                                Exit For
                            End If
                        Next
                        If q Then newId = newIdCheck
                    Loop
                End If

            ' initialize room category
                Dim roomCategory = New RoomCategory() With {.Id = newId, .Name = name, .Price= price}
            
                ' add new room category to database
                _roomRepository.AddRoomCategory(roomCategory)
                _unitOfWork.Commit()

                Return String.Empty
            Catch
                Return "Không thể thêm loại phòng mới"
            End Try
        End Function

        Public Async Function ExecuteAsync(name As String, price As Double) As Task(Of String) Implements ICreateRoomCategoryFactory.ExecuteAsync
            Try
                ' create new room category id
                Dim rooms = Await _roomRepository.GetAllRoomCategories().ToListAsync()
                Dim defaultId = 0
                Dim newId = String.Empty

                If rooms.Count = 0 Then
                    newId = "RC" + 1.ToString("000")
                Else
                    Do While newId = Nothing
                        defaultId += 1
                        Dim newIdCheck = "RC" + defaultId.ToString("000")
                        Dim q = True
                        For Each unit As RoomCategory In rooms
                            If (unit.Id = newIdCheck) Then
                                q = False
                                Exit For
                            End If
                        Next
                        If q Then newId = newIdCheck
                    Loop
                End If

                ' initialize room category
                Dim roomCategory = New RoomCategory() With {.Id = newId, .Name = name, .Price= price}
                
                ' add new room category to database
                _roomRepository.AddRoomCategory(roomCategory)
                Await _unitOfWork.CommitAsync()

                Return String.Empty
            Catch
                Return "Không thể thêm loại phòng mới"
            End Try
        End Function
    End Class
End NameSpace