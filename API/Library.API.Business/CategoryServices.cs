using System.Collections.Generic;
using Library.API.Business.Abstract;
using Library.API.Common.Book;
using Library.API.Common.Category;
using Library.API.DAL.Abstract;

namespace Library.API.Business
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepo)
        {
            _categoryRepository = categoryRepo;
        }
        public Category CreateCategory(Category category)
        {
            if (category == null)
            {
                return null;
            }
            var createdBook = _categoryRepository.Create(category);
            return createdBook.Id == 0 ? null : createdBook;
        }
        public Category GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
            {
                return null;
            }
            var category = _categoryRepository.GetById(categoryId);
            return category.Id <= 0 ? null : category;
        }
        public Category GetCategoryByName(string categoryName)
        {
            if (categoryName == null)
            {
                return null;
            }
            var category = _categoryRepository.GetByName(categoryName.ToLower());
            return category.Id <= 0 ? null : category;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            return categories.Equals(default(List<Category>)) ? null : categories;
        }
        public Category UpdateCategory(Category category)
        {
            if (category.Id <= 0)
            {
                return null;
            }
            var updatedCategory = _categoryRepository.Update(category);
            return updatedCategory.Id <= 0 ? null : category;
        }
        public IEnumerable<Book> GetCategoriesBooks(int categoryId)
        {
            if (categoryId <= 0)
            {
                return null;
            }
            var categpriesBooks = _categoryRepository.GetCategoriesBooks(categoryId);
            return categpriesBooks.Equals(default(List<Book>))
                ? null
                : categpriesBooks;
        }
        public bool DeleteCategory(int categoryId)
        {
            return categoryId > 0 && _categoryRepository.Delete(categoryId);
        }
        public bool PutBookToCategory(int categoryId, int bookId)
        {
            return bookId > 0 && _categoryRepository.PutBookToCategory(categoryId, bookId);
        }
        public bool RemoveBookFromCategory(int categoryId, int bookId)
        {
            return bookId > 0 && _categoryRepository.RemoveBookFromCategory(categoryId, bookId);
        }
    }
}
