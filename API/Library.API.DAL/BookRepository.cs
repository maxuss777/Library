namespace Library.API.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Library.API.DataAccess.Interfaces;
    using Library.API.Model;

    public class BookRepository : Repository, IBookRepository
    {
        public Book Create(Book book)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("CreateBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter nameParameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = book.Name
                    };
                    cmd.Parameters.Add(nameParameter);

                    SqlParameter isbnParameter = new SqlParameter
                    {
                        ParameterName = "@ISBN",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = book.ISBN
                    };
                    cmd.Parameters.Add(isbnParameter);

                    SqlParameter authorParameter = new SqlParameter
                    {
                        ParameterName = "@Author",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = book.Author
                    };
                    cmd.Parameters.Add(authorParameter);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            book.Id = (int) reader["BookId"];
                        }
                    }
                }
            }

            return book;
        }

        public Book GetById(int id)
        {
            Book book = new Book();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("GetBookById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParameter = new SqlParameter
                    {
                        ParameterName = "@Id",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = id
                    };
                    cmd.Parameters.Add(idParameter);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            book.Id = (int) reader["BookId"];
                            book.Name = (string) reader["Name"];
                            book.ISBN = reader["ISBN"] == DBNull.Value
                                ? null
                                : (string) reader["ISBN"];
                            book.Author = reader["Author"] == DBNull.Value
                                ? null
                                : (string) reader["Author"];
                        }
                    }
                }
            }

            return book;
        }

        public List<Book> GetAll()
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
                                        : (string) reader["ISBN"],
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

        public List<Book> GetBooksByCategoryId(int categoryId)
        {
            List<Book> categories = new List<Book>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("GetCategoriesBooks", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParameter = new SqlParameter
                    {
                        ParameterName = "@categoryId",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = categoryId
                    };
                    cmd.Parameters.Add(idParameter);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Book
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
            return categories;
        }

        public Book Update(Book book)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParameter = new SqlParameter
                    {
                        ParameterName = "@Id",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = book.Id
                    };
                    cmd.Parameters.Add(idParameter);

                    SqlParameter nameParameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        DbType = DbType.String,
                        Direction = ParameterDirection.InputOutput,
                        Value = book.Name
                    };
                    cmd.Parameters.Add(nameParameter);

                    SqlParameter isbnParameter = new SqlParameter
                    {
                        ParameterName = "@ISBN",
                        DbType = DbType.String,
                        Direction = ParameterDirection.InputOutput,
                        Value = book.ISBN
                    };
                    cmd.Parameters.Add(isbnParameter);

                    SqlParameter authorParameter = new SqlParameter
                    {
                        ParameterName = "@Author",
                        DbType = DbType.String,
                        Direction = ParameterDirection.InputOutput,
                        Value = book.Author
                    };
                    cmd.Parameters.Add(authorParameter);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            book.Id = (int)reader["BookId"];
                        }
                    }
                }
            }

            return book.Id == 0 ? null : book;
        }

        public bool Delete(int bookId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParameter = new SqlParameter
                    {
                        ParameterName = "@Id",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = bookId
                    };
                    cmd.Parameters.Add(idParameter);

                    int result = cmd.ExecuteNonQuery();

                    return result != 0;
                }
            }
        }

        public int IfCategoryExists(string categoryName)
        {
            int categoryId = 0;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("CheckIfCategoryExist", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter nameParameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = categoryName
                    };
                    cmd.Parameters.Add(nameParameter);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryId = (int) reader["CategoryId"];
                        }
                    }

                    return categoryId;
                }
            }
        }
    }
}