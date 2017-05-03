
Imports Hotelie.Domain.Rooms

Namespace Rooms
	< TestClass >
	Public Class RoomCategoryTest
		Private _category As RoomCategory

		< TestInitialize >
		Public Sub TestInitialize()
			_category = New RoomCategory()
		End Sub

		< TestCleanup >
		Public Sub TestCleanup()
			_category = Nothing
		End Sub

		< TestMethod >
		Public Sub PropertyIdTest_ShouldAreEqualData()
			' declare
			Dim inputId = "NOR01"
			Dim expectedId = inputId

			' act
			_category.Id = inputId

			' assert
			Assert.AreEqual( expectedId, _category.Id )
		End Sub

		< TestMethod >
		Public Sub PropertyNameTest_ShouldAreEqualData()
			' declare
			Dim inputName = "RoomCategoryName"
			Dim expectedName = inputName

			' act
			_category.Name = inputName

			' assert
			Assert.AreEqual( expectedName, _category.Name )
		End Sub

		< TestMethod >
		Public Sub PropertyPriceTest_ShouldAreEqualData()
			' declare
			Dim inputPrice = 200000D
			Dim expectedPrice = inputPrice

			' act
			_category.Price = inputPrice

			' assert
			Assert.AreEqual( expectedPrice, _category.Price )
		End Sub
	End Class
End Namespace