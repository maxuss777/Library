using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Library.API.Common.BooksObjects;
using Library.API.DAL.Abstract;

namespace Library.API.DAL
{
    public class BookRepository : Repository, IBookRepository
    {
        public Book Create(Book book)
        {
            Book createdBook = new Book();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("CreateBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters
                    SqlParameter Name = new SqlParameter
                    {
                        ParameterName = "@Name",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = book.Name
                    };
                    cmd.Parameters.Add(Name);

                    SqlParameter ISBN = new SqlParameter
                    {
                        ParameterName = "@ISBN",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = book.ISBN
                    };
                    cmd.Parameters.Add(ISBN);

                    SqlParameter Author = new SqlParameter
                    {
                        ParameterName = "@Author",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = book.Author
                    };
                    cmd.Parameters.Add(Author);
#endregion

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            createdBook.Id = (int)reader["BookId"];
                            createdBook.Name = (string)reader["Name"];
                            createdBook.ISBN = reader["ISBN"] == DBNull.Value
                                ? null
                                : (string)reader["ISBN"];
                            createdBook.Author = reader["Author"] == DBNull.Value
                                ? null
                                : (string)reader["Author"];
                        }
                    }
                }
            }
            return createdBook;
        }
        public Book Get(int bookId)
        {
            Book bookObject = new Book();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("GetBookById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters
                    SqlParameter Id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = bookId
                    };
                    cmd.Parameters.Add(Id);
#endregion
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bookObject.Id = (int)reader["BookId"];
                            bookObject.Name = (string) reader["Name"];
                            bookObject.ISBN = reader["ISBN"] == DBNull.Value
                                ? null
                                : (string) reader["ISBN"];
                            bookObject.Author = reader["Author"] == DBNull.Value
                                ? null
                                : (string) reader["Author"];
                        }
                    } 
                }
            }
            return bookObject;
        }
        public IEnumerable<Book> GetAll()
        {
            List<Book> books = new List<Book>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("GetBooks", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(
                                new Book
                                {
                                    Id = (int) reader["BookId"],
                                    Name = (string) reader["Name"],
                                    ISBN = reader["ISBN"] == DBNull.Value
                                        ? null
                                        : (string)reader["ISBN"],
                                    Author = reader["Author"] == DBNull.Value
                                        ? null
                                        : (string) reader["Author"]
                                });
                        }
                    }
                }
            }
            return books;
        }
        public IEnumerable<Book> GetBooksByCategoryId(int categoryId)
        {
            List<Book> categoies = new List<Book>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("GetCategoriesBooks", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters
                    SqlParameter Id = new SqlParameter
                    {
                        ParameterName = "@categoryId",
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
                            categoies.Add(new Book
                            {
                                Id = (int) reader["Book_Id"],
                                Name = (string) reader["Name"],
                                ISBN = reader["ISBN"] == DBNull.Value
                                    ? null
                                    : (string) reader["ISBN"],
                                Author = reader["Author"] == DBNull.Value
                                    ? null
                                    : (string) reader["Author"],
                            });
                        }
                    }
                }
            }
            return categoies;
        }
        public Book Update(Book book)
        {
            Book updatedBook = new Book();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters

                    SqlParameter Id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = book.Id
                    };
                    cmd.Parameters.Add(Id);

                    SqlParameter Name = new SqlParameter
                    {
                        ParameterName = "@Name",
                        DbType = DbType.String,
                        Direction = ParameterDirection.InputOutput,
                        Value = book.Name
                    };
                    cmd.Parameters.Add(Name);

                    SqlParameter ISBN = new SqlParameter
                    {
                        ParameterName = "@ISBN",
                        DbType = DbType.String,
                        Direction = ParameterDirection.InputOutput,
                        Value = book.ISBN
                    };
                    cmd.Parameters.Add(ISBN);

                    SqlParameter Author = new SqlParameter
                    {
                        ParameterName = "@Author",
                        DbType = DbType.String,
                        Direction = ParameterDirection.InputOutput,
                        Value = book.Author
                    };
                    cmd.Parameters.Add(Author);

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

                    if ((int) Result.Value == 2 || Result.Value == null)
                    {
                        return null;
                    }
                    updatedBook.Id = book.Id;
                    updatedBook.Name = (string) Name.Value;
                    updatedBook.ISBN = ISBN.Value == DBNull.Value
                        ? null
                        :(string) ISBN.Value;
                    updatedBook.Author = Author.Value == DBNull.Value
                        ? null
                        : (string) Author.Value;
                }
            }
            return updatedBook;
        }
        public bool Delete(int bookId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters

                    SqlParameter Id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = bookId
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

                    return Result.Value != null && (int) Result.Value != 2 && (int) Result.Value != 0;
                }
            }
        }
        public int IfCategoryExist(string categoryName)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("CheckIfCategoryExist", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    #region parameters

                    SqlParameter Name = new SqlParameter
                    {
                        ParameterName = "@Name",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = categoryName
                    };
                    cmd.Parameters.Add(Name);

                    SqlParameter CategoryId = new SqlParameter
                    {
                        ParameterName = "@CategoryId",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(CategoryId);

                    #endregion

                    cmd.ExecuteNonQuery();

                    return CategoryId.Value != null && (int)CategoryId.Value != 0
                        ? (int)CategoryId.Value
                        : 0;
                }
            }
        }
    }
}