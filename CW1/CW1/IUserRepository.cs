using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;

namespace WebServer
{
    public interface IUserRepository
    {
        //User GetUser(int Id);
        void UpdateUser(User user);
        void DeleteUser(int id);
        List<User> GetUsers();
    }

    public class UserRepository : IUserRepository
    {
        readonly string connectionString = @"Data source= LAPTOP-QHM9MDKR;Initial Catalog=MyDataBase; Integrated Security=True; TrustServerCertificate=True";              //BD Name
        public List<User> GetUsers()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<User>("Select * From TableDB").ToList();
        }

        public User GetUser(string name, string password)
        {
            password = password.CreateMD5();
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<User>("Select * From TableDB Where UserName = @name and UserPassword = @password", new { name, password }).FirstOrDefault()!;
        }

        public User GetUser(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<User>("Select * From TableDB Where Id = @id", new { id }).FirstOrDefault()!;
        }

        public void CreateUser(User user)
        {
            user.UserName = @$"{user.UserName}";
            user.UserPassword = @$"{user.UserPassword?.CreateMD5()}";
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = ("Insert Into TableDB (Id, UserName, UserPassword) Values (@Id, @UserName, @UserPassword)");
            db.Execute(sqlQuery, user);
        }

        public void UpdateUser(User user)
        {
            user.UserName = @$"{user.UserName}";
            user.UserPassword = @$"{user.UserPassword?.CreateMD5()}";
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = ("UPDATE TableDB SET UserName = @UserName, UserPassword = @UserPassword Where Id = @Id");
            db.Execute(sqlQuery, user);
        }

        public void DeleteUser(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = ("Delete From TableDB Where Id = @id");
            db.Execute(sqlQuery, new { id });
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
    }
    public static class UserHelper
    {
        public static string CreateMD5(this string input)
        {
            // Use input string to calculate MD5 hash
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes); // .NET 5 +

            //Convert the byte array to hexadecimal string prior to .NET 5
            /*             StringBuilder sb = new System.Text.StringBuilder();
                         for (int i = 0; i < hashBytes.Length; i++)
                         {
                             sb.Append(hashBytes[i].ToString("X2"));
                         }
                         return sb.ToString();*/
        }
    }
}
