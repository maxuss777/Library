﻿using System;
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
        public CategoryObject Create(CategoryObject category)
        {
            CategoryObject createdCategory = new CategoryObject();

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

        public CategoryObject Get(int categoryId)
        {
            CategoryObject categoryObject = new CategoryObject();

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

        public IEnumerable<CategoryObject> GetAll()
        {
            List<CategoryObject> categories = new List<CategoryObject>();

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
                                new CategoryObject
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

        public IEnumerable<BookObject> GetCategoriesBooks(int categoryId)
        {
            List<BookObject> books = new List<BookObject>();

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
                            books.Add(new BookObject
                            {
                                Id = (int)reader["Book_Id"],
                                Name = (string)reader["Name"],
                                ISBN = reader["ISBN"] == DBNull.Value
                                    ? 0
                                    : (long)reader["ISBN"],
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

        public CategoryObject Update(CategoryObject category)
        {
            CategoryObject updatedCategory = new CategoryObject();

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
                        Value = 0
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
                    updatedCategory.CreationDate = category.CreationDate;
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
    }
}
