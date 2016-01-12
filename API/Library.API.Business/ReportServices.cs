using System.Collections.Generic;
using System.Linq;
using Library.API.Business.Abstract;
using Library.API.Common.CategoriesObjects;
using Library.API.DAL.Abstract;

namespace Library.API.Business
{
    public class ReportServices : IReportServices
    {
        private ICategoryRepository _categoryRepository;
        private IBookRepository _bookRepository;

        public ReportServices(ICategoryRepository catRepo, IBookRepository bookRepo)
        {
            _categoryRepository = catRepo;
            _bookRepository = bookRepo;

        }
        public Dictionary<string,int> GetReport()
        {
            var report =new Dictionary<string, int>();
            var countCategorizedBook = 0;

            var categories =(List<Category>) _categoryRepository.GetAll();

            for (int i = 0; i < categories.Count(); i++)
            {
                var books = _bookRepository.GetBooksByCategoryId(categories[i].Id);

                if (books == null || !books.Any())
                {
                    report.Add(categories[i].Name, 0);
                }
                else
                {
                    report.Add(categories[i].Name, books.Count());
                    countCategorizedBook+=books.Count();
                }
            }
            report.OrderByDescending(r => report.Values);
            report.Add("[Unknown]", (_bookRepository.GetAll().Count() - countCategorizedBook));
            return report;
        }
    }
}
