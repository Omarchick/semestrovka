using System.Data.Common;
using Dapper;
using Microsoft.VisualBasic;
using Npgsql;

namespace MarketPlace;

public class ProductRepositoryWithCount
{
    const string _connString = "Server=localhost;Port=5432;User Id=omr;Password=1234;Database=marketplace";
    
    public static async Task<UserProductWithCount[]> GetProductFromDB(int id = -1)
    {
        await using var db = new NpgsqlConnection(_connString);
        string sqlQuery = "select p.id, p.name, information, " +
                                "Round((select avg(r.rating) from  reviews r where p.id = r.product_id and r.rating != -1), 1) as rating, " +
                                "up.product_count as count from products p INNER JOIN user_products up ON p.id = up.product_id ";
        if (id != -1)
        {
            sqlQuery +=  @$"and up.user_id = {id}";
        }
        
        return (await db.QueryAsync<UserProductWithCount>(sqlQuery)).ToArray();
    }
    public static async Task<UserProductWithCount[]> GetAllProductFromDB(int id = -1)
    {
        await using var db = new NpgsqlConnection(_connString);
        string sqlQuery = "select p.id, p.name, information, " +
                          "Round((select avg(r.rating) from  reviews r where p.id = r.product_id and r.rating != -1), 1) as rating, " +
                          "up.product_count as count from products p LEFT JOIN user_products up ON p.id = up.product_id ";
        if (id != -1)
        {
            sqlQuery +=  @$"and up.user_id = {id}";
        }

        return (await db.QueryAsync<UserProductWithCount>(sqlQuery)).ToArray();
    }

}