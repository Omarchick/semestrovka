using Dapper;
using Npgsql;

namespace MarketPlace;

public static class UserProductRepository
{
    const string connString = "Host=pg;Username=omr;Password=1234;Database=marketplace";
    public static async Task<int> AddUser(User user)
    {
        await using var db = new NpgsqlConnection(connString);
        var sqlQuery = @"Insert Into TableDB (name, password) Values (@Name, @Password) RETURNING id";
        return await db.QuerySingleAsync<int>(sqlQuery, user);
    }
}