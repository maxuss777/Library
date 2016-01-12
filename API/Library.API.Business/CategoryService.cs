namespace Library.API.Business
{
    using System.Collections.Generic;
    using Library.API.Business.Interfaces;
    using Library.API.DataAccess.Interfaces;
    using Library.API.Model;

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category CreateCategory(Category category)
        {
            if (category == null)
            {
                return null;
            }

            Category createdBook = _categoryRepository.Create(category);
            
            return createdBook.Id == 0 ? null : createdBook;
        }

        public Category GetCategoryById(int categoryId)
        {
            return categoryId <= 0 ? null : _categoryRepository.GetById(categoryId);
        }

        public Category GetCategoryByName(string categoryName)
        {
            return string.IsNullOrEmpty(categoryName) ? null : _categoryRepository.GetByName(categoryName.ToLower());
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category UpdateCategory(Category category)
        {
            if (category.Id <= 0)
            {
                return null;
            }

            Category updatedCategory = _categoryRepository.Update(category);
            
            return updatedCategory.Id <= 0 ? null : category;
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