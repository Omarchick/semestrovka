using Npgsql;
using Dapper;

namespace WebServer;

public static class PostgreSQL
{
    static string connString = "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";
    public static async Task<int> AddUser(User user)
    {
        await using var db = new NpgsqlConnection(connString);
        var sqlQuery = @"Insert Into TableDB (id, name, email, password) Values (@Id, @Name, @Email, @Password) RETURNING id";
        return await db.QuerySingleAsync<int>(sqlQuery, user);
    }
    


    /*var connString = "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";

    await using var conn = new NpgsqlConnection(connString);
    await conn.OpenAsync();

// Insert some data
    await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
    {
        cmd.Parameters.AddWithValue("p", "Hello world");
        await cmd.ExecuteNonQueryAsync();
    }

// Retrieve all rows
    await using (var cmd = new NpgsqlCommand("SELECT some_field FROM data", conn))
    await using (var reader = await cmd.ExecuteReaderAsync())
    {
        while (await reader.ReadAsync())
            Console.WriteLine(reader.GetString(0));
    }*/
}