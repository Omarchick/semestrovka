using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;

namespace WebServer
{

    public class CompanyRepository
    {
        readonly string Company = @"Company";
        readonly string CW1 = @"CW1";
        readonly string connectionString = @"Data source= LAPTOP-QHM9MDKR;Initial Catalog=CW1; Integrated Security=True; TrustServerCertificate=True";             //BD Name
        public List<Company> GetCompany()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Company>(@$"Select * From {Company}").ToList();
        }

        public Company GetCompany(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Company>(@"Select * From Company Where Id = @id", new { id }).FirstOrDefault()!;
        }
        public Company GetCompany(string name)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Company>(@"Select * From Company Where Name = @name", new { name }).FirstOrDefault()!;
        }

        public void CreateCompany(Company company)
        {
            company.Name = @$"{company.Name}";
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = (@"Insert Into Company (Id, UserName) Values (@Id, @Name)");
            db.Execute(sqlQuery, company);
        }

        public void UpdateCompany(Company company)
        {
            company.Name = @$"{company.Name}";
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = (@"UPDATE Company SET Name = @Name Where Id = @Id");
            db.Execute(sqlQuery, company);
        }

        public void DeleteCompany(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = (@"Delete From TableDB Where Id = @id");
            db.Execute(sqlQuery, new { id });
        }
    }

    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
    }

}
