using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Library.API.Common.Book;
using Library.API.Common.Category;
using Library.API.DAL.Abstract;

namespace Library.API.DAL
{
    public class CategoryRepository : Repository, ICategoryRepository
    {
        public Category Create(Category category)
        {
            Category createdCategory = new Category();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("CreateCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters
                    SqlParameter Name = new SqlParameter
                    {
                        ParameterName = "@Name",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = category.Name
                    };
                    cmd.Parameters.Add(Name);
#endregion

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            createdCategory.Id = (int)reader["CategoryId"];
                            createdCategory.Name = (string)reader["Name"];
                            createdCategory.CreationDate = reader["CreationDate"] == DBNull.Value
                                ? default(DateTime)
                                : (DateTime)reader["CreationDate"];
                        }
                    }
                }
            }
            return createdCategory;
        }

        public Category Get(int categoryId)
        {
            Category categoryObject = new Category();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("GetCategoryById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters
                    SqlParameter Id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = categoryId
                    };
                    cmd.Parameters.Add(Id);
#endregion
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryObject.Id = (int)reader["CategoryId"];
                            categoryObject.Name = (string)reader["Name"];
                            categoryObject.CreationDate = reader["CreationDate"] == DBNull.Value
                                ? default(DateTime)
                                : (DateTime)reader["CreationDate"];
                        }
                    }
                }
            }
            return categoryObject;
        }

        public IEnumerable<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("GetCategories", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(
                                new Category
                                {
                                    Id = (int)reader["CategoryId"],
                                    Name = (string)reader["Name"],
                                    CreationDate = reader["CreationDate"] == DBNull.Value
                                        ? default(DateTime)
                                        : (DateTime)reader["CreationDate"]
                                });
                        }
                    }
                }
            }
            return categories;
        }

        public IEnumerable<Book> GetCategoriesBooks(int categoryId)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("GetCategoriesBooks", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
#region parameters
                    SqlParameter Id = new SqlParameter
                    {
                        ParameterName = "@CategoryId",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = categoryId
                    };
                    cmd.Parameters.Add(Id);
#endregion
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                Id = (int)reader["Book_Id"],
                                Name = (string)reader["Name"],
                                ISBN = reader["ISBN"] == DBNull.Value
                                    ? null
                                    : (string)reader["ISBN"],
                                Author = reader["Author"] == DBNull.Value
                                    ? null
                                    : (string)reader["Author"]
                            });
                        }
                    }
                }
            }
            return books;
        }

        public Category Update(Category category)
        {
            Category updatedCategory = new Category();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters

                    SqlParameter Id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = category.Id
                    };
                    cmd.Parameters.Add(Id);

                    SqlParameter Name = new SqlParameter
                    {
                        ParameterName = "@Name",
                        DbType = DbType.String,
                        Direction = ParameterDirection.InputOutput,
                        Value = category.Name
                    };
                    cmd.Parameters.Add(Name);

                    SqlParameter CreationDate = new SqlParameter
                    {
                        ParameterName = "@CreationDate",
                        DbType = DbType.DateTime,
                        Direction = ParameterDirection.Output,
                    };
                    cmd.Parameters.Add(CreationDate);

                    SqlParameter Result = new SqlParameter
                    {
                        ParameterName = "@Result",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output,
                        Value = 0
                    };
                    cmd.Parameters.Add(Result);

#endregion

                    cmd.ExecuteNonQuery();

                    if ((int)Result.Value == 2 || Result.Value == null)
                    {
                        return null;
                    }
                    updatedCategory.Id = category.Id;
                    updatedCategory.CreationDate = (DateTime)CreationDate.Value;
                    updatedCategory.Name = (string)Name.Value;
                }
            }
            return updatedCategory;
        }

        public bool Delete(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters

                    SqlParameter Id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = categoryId
                    };
                    cmd.Parameters.Add(Id);

                    SqlParameter Result = new SqlParameter
                    {
                        ParameterName = "@Result",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(Result);

#endregion

                    cmd.ExecuteNonQuery();

                    return Result.Value != null && (int)Result.Value != 2 && (int)Result.Value != 0;
                }
            }
        }

        public bool PutBookToCategory(int categoryId, int bookId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("PutBookToCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters

                    SqlParameter CategoryId = new SqlParameter
                    {
                        ParameterName = "@CategoryId",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = categoryId
                    };
                    cmd.Parameters.Add(CategoryId);

                    SqlParameter BookId = new SqlParameter
                    {
                        ParameterName = "@BookId",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = bookId
                    };
                    cmd.Parameters.Add(BookId);

                    SqlParameter Result = new SqlParameter
                    {
                        ParameterName = "@Result",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(Result);

#endregion

                    cmd.ExecuteNonQuery();

                    return Result.Value != null && (int)Result.Value != 2 && (int)Result.Value != 0;
                }
            }
        }

        public bool RemoveBookFromCategory(int categoryId, int bookId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("RemoveBookFromCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    #region parameters

                    SqlParameter CategoryId = new SqlParameter
                    {
                        ParameterName = "@CategoryId",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = categoryId
                    };
                    cmd.Parameters.Add(CategoryId);

                    SqlParameter BookId = new SqlParameter
                    {
                        ParameterName = "@BookId",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = bookId
                    };
                    cmd.Parameters.Add(BookId);

                    SqlParameter Result = new SqlParameter
                    {
                        ParameterName = "@Result",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(Result);

                    #endregion

                    cmd.ExecuteNonQuery();

                    return Result.Value != null && (int)Result.Value != 2 && (int)Result.Value != 0;
                }
            }
        }
    }
}
