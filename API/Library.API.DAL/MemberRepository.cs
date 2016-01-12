namespace Library.API.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Library.API.DataAccess.Interfaces;
    using Library.API.Model;

    public class MemberRepository : Repository, IMemberRepository
    {
        public Member Get(string email)
        {
            Member member = new Member();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("GetMemberByEmail", conn))
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
                            member.Id = (int) reader["Id"];
                            member.Email = (string) reader["Email"];
                            member.MemberName = (string) reader["MemberName"];
                            member.Password = (string) reader["Password"];
                            member.CreationDate = (DateTime) reader["CreationDate"];
                        }
                    }
                }
            }

            return member.Id == 0 ? null : member;
        }

        public Member Create(Member member)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("CreateMember", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    #region parameters

                    SqlParameter Email = new SqlParameter
                    {
                        ParameterName = "@Email",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = member.Email
                    };
                    cmd.Parameters.Add(Email);

                    SqlParameter MemberName = new SqlParameter
                    {
                        ParameterName = "@MemberName",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = member.MemberName
                    };
                    cmd.Parameters.Add(MemberName);

                    SqlParameter Password = new SqlParameter
                    {
                        ParameterName = "@Password",
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = member.Password
                    };
                    cmd.Parameters.Add(Password);

                    #endregion

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            member.Id = (int) reader["Id"];
                            member.Email = (string) reader["Email"];
                            member.MemberName = (string) reader["MemberName"];
                            member.CreationDate = (DateTime) reader["CreationDate"];
                        }
                    }
                }
            }

            return member.Id == 0 ? null : member;
        }
    }
}