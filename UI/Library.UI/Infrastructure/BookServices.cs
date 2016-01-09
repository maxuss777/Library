using System.Collections.Generic;
using System.IO;
using System.Net;
using Library.UI.Abstract;
using Library.UI.Helpers;
using Library.UI.Models;
using Newtonsoft.Json;

namespace Library.UI.Infrastructure
{
    public class BookServices : IBookServices
    {
        public IEnumerable<Book> GetAll()
        {
            List<Book> booksList = new List<Book>();
            WebRequest request = WebRequest.Create(UrlResolver.Books_GetAll);

            using (WebResponse response = request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                if (dataStream == null) return booksList;
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    booksList = JsonConvert.DeserializeObject<List<Book>>(reader.ReadToEnd());
                }
            }
            return booksList;
        }
    }
}