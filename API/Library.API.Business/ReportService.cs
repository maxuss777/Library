namespace Library.API.Business
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.API.Business.Interfaces;
    using Library.API.DataAccess.Interfaces;
    using Library.API.Model;

    public class ReportService : IReportService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;

        public ReportService(ICategoryRepository catRepository, IBookRepository bookRepository)
        {
            _categoryRepository = catRepository;
            _bookRepository = bookRepository;
        }

        public List<KeyValuePair<string, int>> GetReport()
        {
            List<KeyValuePair<string, int>> report = new List<KeyValuePair<string, int>>();
            List<int> categorisedBooksIds = new List<int>();

            List<Category> categories = _categoryRepository.GetAll();

            for (int i = 0; i < categories.Count; i++)
            {
                Category category = categories[i];

                List<Book> books = _bookRepository.GetBooksByCategoryId(category.Id);

                if (books.Count == 0)
                {
                    report.Add(new KeyValuePair<string, int>(category.Name, 0));
                }
                else
                {
                    report.Add(new KeyValuePair<string, int>(category.Name, books.Count));
                    categorisedBooksIds.AddRange(books.Select(book => book.Id));
                }
            }

            List<KeyValuePair<string, int>> orderedReport = report.OrderByDescending((r => r.Value)).ToList();
            List<Book> allExistedBooks = _bookRepository.GetAll();
            int booksWithoutCategory = allExistedBooks.Count(book => !categorisedBooksIds.Contains(book.Id));
            orderedReport.Add(new KeyValuePair<string, int>("[Unknown]", booksWithoutCategory));

            return orderedReport;
        }
    }
}