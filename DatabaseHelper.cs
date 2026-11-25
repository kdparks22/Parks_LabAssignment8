using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Parks_LabAssignment8
{
    public static class DatabaseHelper
    {
        // Change this server name if your SQL Server Express instance has a different name.
        private static readonly string ConnectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=UserLoginDB;Integrated Security=True;";

        private static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static bool UsernameExists(string username)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.Users WHERE Username = @Username;", connection))
            {
                command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public static void CreateUser(string username, byte[] passwordHash, byte[] passwordSalt)
        {
            const string sql = @"INSERT INTO dbo.Users (Username, PasswordHash, PasswordSalt)
                                 VALUES (@Username, @PasswordHash, @PasswordSalt);";

            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;
                command.Parameters.Add("@PasswordHash", SqlDbType.VarBinary, 32).Value = passwordHash;
                command.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary, 16).Value = passwordSalt;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static bool AuthenticateUser(string username, string password)
        {
            const string sql = @"SELECT PasswordHash, PasswordSalt
                                 FROM dbo.Users WHERE Username = @Username;";

            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return false;

                    byte[] storedHash = (byte[])reader["PasswordHash"];
                    byte[] storedSalt = (byte[])reader["PasswordSalt"];
                    return PasswordHelper.VerifyPassword(password, storedHash, storedSalt);
                }
            }
        }
    }
}
