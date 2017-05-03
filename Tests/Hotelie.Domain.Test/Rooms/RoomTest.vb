Imports Hotelie.Domain.Rooms

Namespace Rooms
	< TestClass >
	Public Class RoomTest
		Private _room As Room

		< TestInitialize >
		Public Sub TestInitialize()
			_room = New Room()
		End Sub

		< TestCleanup >
		Public Sub TestCleanup()
			_room = Nothing
		End Sub

		< TestMethod >
		Public Sub PropertyIdTest_ShouldAreEqualData()
			' declare
			Dim inputId = "PH101"
			Dim expectedId = inputId

			' act
			_room.Id = inputId

			' assert
			Assert.AreEqual( expectedId, _room.Id )
		End Sub

		< TestMethod >
		Public Sub PropertyNameTest_ShouldAreEqualData()
			' declare
			Dim inputName = "RoomName"
			Dim expectedName = inputName

			' act
			_room.Name = inputName

			' assert
			Assert.AreEqual( expectedName, _room.Name )
		End Sub

		< TestMethod >
		Public Sub PropertyNoteTest_ShouldAreEqualData()
			' declare
			Dim inputNote = "RoomNote"
			Dim expectedNote = inputNote

			' act
			_room.Note = inputNote

			' assert
			Assert.AreEqual( expectedNote, _room.Note )
		End Sub

		< TestMethod >
		Public Sub PropertyCategoryTest_ShouldSameReference()
			' declare
			Dim inputCategory = New RoomCategory With {.Id="NOR01", .Name="RoomCategory Test"}
			Dim expectedCategory = inputCategory

			' act
			_room.Category = inputCategory

			' assert
			Assert.AreSame( expectedCategory, _room.Category )
		End Sub
	End Class
End Namespace