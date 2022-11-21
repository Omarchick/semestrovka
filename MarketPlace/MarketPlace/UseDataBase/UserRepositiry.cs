using System.Data.Common;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks.Dataflow;
using Npgsql;
using Dapper;

namespace MarketPlace;

public static class UserRepository
{
    const string _connString = "Server=localhost;Port=5432;User Id=omr;Password=1234;Database=marketplace";

    public static async Task<int> AddUser(User user)
    {
        Console.WriteLine("1!");
        Console.WriteLine((await new UserValidator().ValidateAsync(user)).IsValid);
        Console.WriteLine(
                           await GetUser(user.Name, user.Password));
        if (!(user is not null &&
              await GetUser(user.Name, user.Password) is null &&
              (await new UserValidator().ValidateAsync(user)).IsValid))
        {
            return -1;
        }

        Console.WriteLine("2!");
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        await using var db = new NpgsqlConnection(_connString);
        const string sqlQuery = @"Insert Into users (name, password) Values (@name, @password) RETURNING id";
        return await db.QuerySingleAsync<int>(sqlQuery, user);
    }

    public static async Task<int> AddUser(string name, string password)
    {
        if (!(name is not null &&
              password is not null &&
              await GetUser(name, password) is null &&
              (await new NameValidator().ValidateAsync(name)).IsValid &&
              (await new PasswordValidator().ValidateAsync(password)).IsValid))
        {
            return -1;
        }
        await using var db = new NpgsqlConnection(_connString);
        await db.OpenAsync();
        await using var cmd = new NpgsqlCommand(@"INSERT INTO users (name, password) VALUES (@name, @password)", db);
        cmd.Parameters.AddWithValue("name", $@"{name}");
        cmd.Parameters.AddWithValue("password", $@"{BCrypt.Net.BCrypt.HashPassword(password)}");
        await cmd.ExecuteNonQueryAsync();
        return (await GetUser(name, password)).Id;
        //await db.CloseAsync();
    }

    public static async Task<User> GetUser(int id)
    {
        await using var db = new NpgsqlConnection(_connString);
        await db.OpenAsync();
        await using var cmd = new NpgsqlCommand($@"SELECT * FROM users WHERE id = @id", db);
        cmd.Parameters.AddWithValue("id", id);
        await using var reader = await cmd.ExecuteReaderAsync();
        return await GetUserFromReader(reader);
    }

    public static async Task<User> GetUser(string name, string password)
    {
        Console.WriteLine("REallu");
        await using var db = new NpgsqlConnection(_connString);
        Console.WriteLine(db.State);
        Console.WriteLine("KAK");
        await db.OpenAsync();
        Console.WriteLine(db.State);
        Console.WriteLine("TOPCHIck7");
        await using var cmd = new NpgsqlCommand($@"SELECT * FROM users WHERE name = @name", db);
        Console.WriteLine("TOPCHIc6");
        cmd.Parameters.AddWithValue("name", $@"{name}");
        await using var reader = await cmd.ExecuteReaderAsync();
        Console.WriteLine("TOPCHIck4");
        var user = await GetUserFromReader(reader);
        Console.WriteLine("TOPCHIck");
        Console.WriteLine(user);
        await db.CloseAsync();
        return user is not null && BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
    }

    public static async Task<int> UpdateUser(string name, string password, string newname, string newpassword)
    {
        if (!(name is not null &&
              password is not null &&
              (await new NameValidator().ValidateAsync(name)).IsValid &&
              (await new PasswordValidator().ValidateAsync(password)).IsValid))
        {
            return -1;
        }
        newpassword = BCrypt.Net.BCrypt.HashPassword(newpassword);
        await using var db = new NpgsqlConnection(_connString);
        await db.OpenAsync();
        var user = await GetUser(name, password);
        if (user is not null)
        {
            await using var cmd =
                new NpgsqlCommand(@"UPDATE users SET name = @name, password = @password Where id = @id", db);
            cmd.Parameters.AddWithValue("name", $@"{newname}");
            cmd.Parameters.AddWithValue("password", $@"{newpassword}");
            cmd.Parameters.AddWithValue("id", user.Id);
            await using var reader = await cmd.ExecuteReaderAsync();
        }
        return user?.Id ?? -1 ;
        //await db.CloseAsync();
    }

    public static async Task DeleteUser(int id)
    {
        await using var db = new NpgsqlConnection(_connString);
        const string sqlQuery = @"DELETE FROM users WHERE id = @id";
        await db.ExecuteAsync(sqlQuery, new { id });
    }

    private static async Task<User> GetUserFromReader(DbDataReader reader)
    {
        while (await reader.ReadAsync())
        {
            User user = new(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetInt32(3));
            return user;
        }

        return null!;
    }

    //const string _connString = "Host=localhost;User Id=omr;Password=1234;Database=marketplace";
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