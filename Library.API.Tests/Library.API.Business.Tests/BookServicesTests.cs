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
        private readonly BookServices _bookServices = new BookServices(new BookRepository());
        private BookInfo actualBook = new BookInfo();
        private BookInfo expectedBook = new BookInfo();
    }
}
