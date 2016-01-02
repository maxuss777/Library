using System.Linq;
using Library.API.Business;
using Library.API.Common.Book;
using Library.API.DAL;
using NUnit.Framework;
using ServiceStack;

namespace Library.API.Tests.Library.API.Business.Tests
{
    [TestFixture]
    public class BookServicesTests
    {
        private readonly BookServices _bookServices = new BookServices();
        private BookObject actualBook = new BookObject();
        private BookObject expectedBook = new BookObject();

        [Test]
        public void Test_01_Positive_Create_Book_Valid_Data()
        {
            //Arrange
            expectedBook = new BookObject
            {
                Name = "TestBook",
                ISBN = 9999999999999,
                Author = "Pushkin"
            };

            //Act
            actualBook = _bookServices.CreateBook(expectedBook);
            if (actualBook == null || actualBook.Id == 0)
            {
                Assert.Fail("book object has not been created");
            }
            expectedBook.Id = actualBook.Id;

            //Assert
            Assert.AreEqual(expectedBook.ToJson(), actualBook.ToJson());
        }

        [Test]
        public void Test_02_Positive_Get_Book_By_Id_Valid_Id()
        {
            //Arrange
            expectedBook = new BookObject
            {
                Name = "TestBook",
                ISBN = 9999999999999,
                Author = "Pushkin"
            };
            var idOfCreatedBook = _bookServices.CreateBook(expectedBook).Id;
            if (idOfCreatedBook == 0)
            {
                Assert.Fail("book object has not been created");
            }

            //Act
            actualBook = _bookServices.GetBookById(idOfCreatedBook);
            if (actualBook == null || actualBook.Id == 0)
            {
                Assert.Fail("the book has not been taken");
            }
            expectedBook.Id = actualBook.Id;

            //Assert
            Assert.AreEqual(expectedBook.ToJson(), actualBook.ToJson());
        }

        [Test]
        public void Test_03_Positive_Get_All_Existing_Books()
        {
            //Arrange
            expectedBook = new BookObject
            {
                Name = "TestBook",
                ISBN = 9999999999999,
                Author = "Pushkin"
            };
            expectedBook.Id = _bookServices.CreateBook(expectedBook).Id;
            if (expectedBook.Id == 0)
            {
                Assert.Fail("book object hasn't been created");
            }

            //Act
            var booksList = _bookServices.GetAllBooks().ToList();

            if (!booksList.Any())
            {
                Assert.Fail("the books have not been taken");
            }

            //Assert
            Assert.AreEqual(
                expectedBook.ToJson(), booksList[booksList.Count() - 1].ToJson());
        }

        [Test]
        public void Test_04_Positive_Update_Book_Valid_Data()
        {
            //Arrange
            var bookToCreate = new BookObject
            {
                Name = "TestBook",
                ISBN = 9999999999999,
                Author = "Pushkin"
            };

            expectedBook = _bookServices.CreateBook(bookToCreate);
            if (expectedBook == null)
            {
                Assert.Fail("book object has not been created");
            }
            expectedBook.Name = "New Name";
            expectedBook.ISBN = 123456;
            expectedBook.Author = "New Author";

            //Act
            actualBook = _bookServices.UpdateBook(expectedBook);
            if (actualBook == null)
            {
                Assert.Fail("Actual book is null");
            }

            //Assert
            Assert.AreEqual(expectedBook.ToJson(), actualBook.ToJson());
        }

        [Test]
        public void Test_05_Positive_Delete_Book()
        {
            //Arrange
            var bookToCreate = new BookObject
            {
                Name = "TestBook",
                ISBN = 9999999999999,
                Author = "Pushkin"
            };

            expectedBook = _bookServices.CreateBook(bookToCreate);
            if (expectedBook == null)
            {
                Assert.Fail("book object hasn't been created");
            }
            
            //Act

            //Assert
            Assert.IsTrue(_bookServices.DeleteBook(expectedBook.Id));
        }

        [Ignore("Not Realised")]
        [Test]
        public void Test_06_Positive_Put_Book_To_Existing_Category()
        {
            //Arrange

            //Act

            //Assert
        }

        [Ignore("Not Realised")]
        [Test]
        public void Test_07_Positive_Get_Books_Category()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
