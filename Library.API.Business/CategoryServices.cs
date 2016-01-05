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

        public CategoryInfo CreateCategory(CategoryInfo category)
        {
            if (category == null)
            {
                return null;
            }
            var createdBook = _categoryRepository.Create(category);
            return createdBook.Id == 0 ? null : createdBook;
        }

        public CategoryObject GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
            {
                return null;
            }
            var category = _categoryRepository.Get(categoryId);
            if (category.Id <= 0)
            {
                return null;
            }
            category.Books = _categoryRepository.GetCategoriesBooks(categoryId);
            return category;
        }

        public IEnumerable<CategoryObject> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            if (categories.Equals(default(List<CategoryObject>)))
            {
                return null;
            }
            foreach (CategoryObject cat in categories)
            {
                cat.Books = _categoryRepository.GetCategoriesBooks(cat.Id);
            }
            return categories;
        }

        public CategoryObject UpdateCategory(CategoryObject category)
        {
            if (category.Id <= 0)
            {
                return null;
            }
            var updatedCategory = _categoryRepository.Update(category);
            if (updatedCategory.Id <= 0)
            {
                return null;
            }
            category.Books = _categoryRepository.GetCategoriesBooks(category.Id);
            return category;
        }

        public IEnumerable<BookInfo> GetCategoriesBooks(int categoryId)
        {
            if (categoryId <= 0)
            {
                return null;
            }
            var categpriesBooks = _categoryRepository.GetCategoriesBooks(categoryId);
            return categpriesBooks.Equals(default(List<BookInfo>))
                ? null
                : categpriesBooks;
        }

        public bool DeleteCategory(int categoryId)
        {
            return categoryId > 0 && _categoryRepository.Delete(categoryId);
        }

        public bool PutBookToCategory(int categoryId,int bookId)
        {
            return bookId > 0 && _categoryRepository.PutBookToCategory(categoryId, bookId);
        }
    }
}
