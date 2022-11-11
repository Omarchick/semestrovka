using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public static class SqlRequests
    {
        static string ConnectionString { get; set; } = @"Data source= LAPTOP-QHM9MDKR;Initial Catalog=CW1; Integrated Security=True";     //DB Name
        //static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static SqlDataReader WorkManyValues(this string cmdStr)
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            SqlCommand command = new(cmdStr, connection);
            var result = command.ExecuteReader();
            connection.Close();
            return result;
        }
        public static object WorkSingleValue(this string cmdStr)
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            SqlCommand command = new(cmdStr, connection);
            var result = command.ExecuteScalar();
            connection.Close();
            return result;
        }
        public static int WorkSmthValue(this string cmdStr)
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            SqlCommand command = new(cmdStr, connection);
            int result = command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }
}
