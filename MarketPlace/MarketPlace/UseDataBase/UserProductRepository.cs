using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Microsoft.VisualBasic;
using Npgsql;

namespace MarketPlace;

public class UserProductRepository
{
    const string _connString = "Server=localhost;Port=5432;User Id=omr;Password=1234;Database=marketplace";

    public static async Task<int> AddUserProduct(UserProduct userProduct)
    {
        if (userProduct is null || await UserRepository.GetUser(userProduct.UserId) is null || await ProductRepository.GetProduct(userProduct.ProductId) is null)
        {
            return -1;
        }

        var product = await GetUserProduct(userProduct.UserId, userProduct.ProductId);
        if (product is null)
        {
            await using var db = new NpgsqlConnection(_connString);
            const string sqlQuery = 
                "Insert Into public.user_products " +
                "(user_id, product_id, product_count) Values " +
                @"(@userId, @productId, @productCount) RETURNING user_id";
            userProduct.UserId = await db.QuerySingleAsync<int>(sqlQuery, userProduct);
            return userProduct.UserId;
        }
        return await UpdateReview(userProduct.UserId, userProduct.ProductId, userProduct.ProductCount);
    }

    public static async Task<UserProduct> GetUserProduct(int userId, int productId) 
    {
        await using var db = new NpgsqlConnection(_connString);
        const string sqlQuery = @"SELECT * FROM public.user_products WHERE user_id = @userId and product_id = @productId";
        return await db.QueryFirstOrDefaultAsync<UserProduct>(sqlQuery, new { userId, productId });
    }

    public static async Task<Product[]> GetProductsByUserId(int userId)
    {
        await using var db = new NpgsqlConnection(_connString);
        const string sqlQuery = "SELECT products.id, products.name, products.information, products.rating " +
                                "FROM user_products, products " +
                                @"WHERE user_products.user_id = @user_id AND user_products.product_id = products.id";
        return (await db.QueryAsync<Product>(sqlQuery, new { userId })).ToArray();
    }

    public static async Task<int> UpdateReview(int userId, int productId, long productCount)
    {
        var product = await GetUserProduct(userId, productId);
        if (product is null)
        {
            return -1;
        }

        if (product.ProductCount <= -productCount)
        {
            await DeleteUserProduct(userId, productId);
            return userId;
        }
        await using var db = new NpgsqlConnection(_connString);
        const string sqlQuery = @"UPDATE public.user_products SET product_count = product_count + @productCount WHERE user_id = @userId and product_id = @productId RETURNING user_id";
        return await db.QueryFirstAsync<int>(sqlQuery, new { @userId, @productId, productCount});
    }

    public static async Task DeleteUserProduct(int userId, int productId)
    {
        await using var db = new NpgsqlConnection(_connString);
        const string sqlQuery = @"DELETE FROM public.user_products WHERE user_id = @userId and product_id = @productId";
        await db.ExecuteAsync(sqlQuery, new { userId, productId });
    }

    public static async Task<UserProduct[]> GetUserProductsFromDB()
    {
        await using var db = new NpgsqlConnection(_connString);
        const string sqlQuery = @"SELECT * FROM public.user_products";
        return (await db.QueryAsync<UserProduct>(sqlQuery)).ToArray();
    }
    
    public static async Task<UserProduct[]> GetUserProductsFromDBWithRating()
    {
        await using var db = new NpgsqlConnection(_connString);
        const string sqlQuery = @"SELECT * FROM public.user_products";
        return (await db.QueryAsync<UserProduct>(sqlQuery)).ToArray();
    }
}