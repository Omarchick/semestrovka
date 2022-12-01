using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MarketPlace
{
    public static class WebHelper
    {
        public static byte[] GetBytes(this string convertingString)
        {
            return Encoding.UTF8.GetBytes(convertingString);
        }
        public static async Task Home(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/mainpage.html");
        }
        
        public static async Task Products(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/products.html");
        }
        
        public static async Task GetMyProducts(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/myProducts.html");
        }
        
        public static async Task ProductsNotRegistered(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/productsNotRegistered.html");
        }

        public static async Task NotFound(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/notFound.html");
        }

        public static async Task GetUser(HttpListenerContext context)
        {
            await using var stream = context.Response.OutputStream;
            var user = await UserRepository.GetUser(await context.GetUserId());
            if (user is not null)
            {
                context.Response.StatusCode = 200;
                await stream.WriteAsync((await user.ToJSON()).GetBytes());
            }
            else
            {
                context.Response.StatusCode = 418;
            }
        }

        public static async Task AddProductCount(HttpListenerContext context)
        {
            await using var inputStream = context.Request.InputStream;
            using var reader = new StreamReader(inputStream);
            var content = await reader.ReadToEndAsync();
            var userProduct = JsonSerializer.Deserialize<UserProduct>(content);
            await UserProductRepository.UpdateUserProduct(await context.GetUserId(), userProduct.ProductId, userProduct.ProductCount);
        }
        
        public static async Task Register(HttpListenerContext context)
        {
            await using (var inputStream = context.Request.InputStream)
            {
                using var reader = new StreamReader(inputStream);
                var content = await reader.ReadToEndAsync();
                var user = JsonSerializer.Deserialize<User>(content);
                await using var stream = context.Response.OutputStream;
                if (UserRepository.AddUser(user).Result != -1)
                {
                    var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                    context.Response.StatusCode = 201;
                    await Session.SetSession(user, context);
                    await context.Response.OutputStream.WriteAsync(succsessOperation);
                }
                else
                {
                    var succsessOperation = Encoding.ASCII.GetBytes("Error");
                    context.Response.StatusCode = 208;
                    await context.Response.OutputStream.WriteAsync(succsessOperation);
                }
            }
        }
        public static async Task SignIn(HttpListenerContext context)
        {
            await using (var inputStream = context.Request.InputStream)
            {
                using var reader = new StreamReader(inputStream);
                var content = await reader.ReadToEndAsync();
                var user = JsonSerializer.Deserialize<User>(content);
                await using var stream = context.Response.OutputStream;
                user = UserRepository.GetUser(user?.Name, user?.Password).Result;
                if (user != null)
                {
                    var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                    await Session.SetSession(user, context);
                    context.Response.StatusCode = 201;
                    await context.Response.OutputStream.WriteAsync(succsessOperation);
                }
                else
                {
                    var succsessOperation = Encoding.ASCII.GetBytes("Error");
                    context.Response.StatusCode = 208;
                    await context.Response.OutputStream.WriteAsync(succsessOperation);
                }
            }
        }

        public static async Task GetProductsFromDB(HttpListenerContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;
            await context.Response.OutputStream.WriteAsync(
                JsonSerializer.Serialize(await ProductRepositoryWithCount.GetProductFromDB()).GetBytes());
        }
        
        public static async Task GetUserProducts(HttpListenerContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;
            await context.Response.OutputStream.WriteAsync(
                JsonSerializer.Serialize(await ProductRepositoryWithCount.
                    GetProductFromDB(await context.GetUserId())).GetBytes());
        }
    }
}
