using System;
using System.Data;
using System.Data.SqlClient;
using Library.API.Common.User;
using Library.API.DAL.Abstract;

namespace Library.API.DAL
{
    public class UserRepository : Repository, IUserRepository
    {
        public User Get(string email)
        {
            User user = new User();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.GetUserByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters

                    SqlParameter Email = new SqlParameter
                    {
                        ParameterName = "@Email",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = email
                    };
                    cmd.Parameters.Add(Email);
#endregion

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Id = (int) reader["Id"];
                            user.Email = (string) reader["Email"];
                            user.Password = (string) reader["Password"];
                            user.CreationDate = (DateTime) reader["CreationDate"];
                            user.RoleId = reader["RoleId"] == DBNull.Value
                                ? 0
                                : (int) reader["RoleId"];
                        }
                    }
                }
            }
            return user;
        }
        public User Create(User user)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.CreateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

#region parameters
                    
                    SqlParameter Email = new SqlParameter
                    {
                        ParameterName = "@Email",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = user.Email
                    };
                    cmd.Parameters.Add(Email);

                    SqlParameter Password = new SqlParameter
                    {
                        ParameterName = "@Password",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = user.Password
                    };
                    cmd.Parameters.Add(Password);

                    SqlParameter RoleId = new SqlParameter
                    {
                        ParameterName = "@RoleId",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = user.RoleId
                    };

                    cmd.Parameters.Add(RoleId);

#endregion

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Id = (int)reader["Id"];
                            user.Email = (string)reader["Email"];
                            user.CreationDate = (DateTime)reader["CreationDate"];
                            user.RoleId = reader["RoleId"] == DBNull.Value
                                ? 0
                                : (int)reader["RoleId"];
                        }
                    }
                }
            }
            return user;
        }
    }
}
