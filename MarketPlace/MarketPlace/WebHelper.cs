using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using System.Threading.Tasks;
using Azure;

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

        public static async Task SetBalacePage(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/updBalance.html");
        }

        public static async Task ProductsNotRegistered(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/productsNotRegistered.html");
        }

        public static async Task NotFound(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/notFound.html");
        }
        
        public static void IncorrectSession(HttpListenerContext context)
        {
            context.RemoveSession();
            context.Response.StatusCode = 300;
            context.Response.OutputStream.WriteAsync(null);
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
    
        public static async Task addBalance(HttpListenerContext context)
        {
            UserRepository.UpdateUserBalance(context.GetUserId().Result, 1);
            context.Response.StatusCode = 200;
        }
        

        public static async Task DeleteUserProduct(HttpListenerContext context)
        {
            await using var inputStream = context.Request.InputStream;
            using var reader = new StreamReader(inputStream);
            var content = await reader.ReadToEndAsync();
            var userProduct = JsonSerializer.Deserialize<UserProductDTO>(content);
            var userId = await context.GetUserId();
            if (userProduct is not null)
            {
                await UserProductRepository.DeleteUserProduct(userId, userProduct.ProductId);
            }

            context.Response.StatusCode = 418;
            await context.Response.OutputStream.WriteAsync(
                Encoding.UTF8.GetBytes(
                    JsonSerializer.Serialize(
                        new ProductCountUserBalanceDTO
                        {
                            ProductCount = 0,
                            Balance = UserRepository.GetUser(userId).Result.Balance
                        })));
            context.Response.OutputStream.Close();
        }

        public static async Task AddProducts(HttpListenerContext context)
        {
            await using var inputStream = context.Request.InputStream;
            using var reader = new StreamReader(inputStream);
            var content = await reader.ReadToEndAsync();
            Console.WriteLine(content);
            var userId = await context.GetUserId();
            var userProducts = JsonSerializer.Deserialize<UserProductDTO[]>(content);
            if (userProducts != null)
            {
                Dictionary<int, long> products = new();
                    //Enumerable.Repeat(new ProductIDCountDTO {ProductId = -1 ,ProductCount = 0},userProducts.Length).ToArray();
                Console.WriteLine(userProducts.Length);
                foreach (var userProduct in userProducts)
                {
                    if (products.TryGetValue(userProduct.ProductId, out _))
                        {
                            products[userProduct.ProductId] += userProduct.ProductCount;
                        }
                        if (!products.TryGetValue(userProduct.ProductId, out _))
                        {
                            products[userProduct.ProductId] = userProduct.ProductCount;
                        }
                }
                
                foreach (var product in products)
                {
                    AddProductToDB(new ProductIDCountDTO() { ProductId = product.Key, ProductCount = product.Value }, userId);
                }
            }
            
            /*var userBalance = UserRepository.GetUserBalance(userId).Result;
            var productsCount = UserProductRepository.GetAllUserProductsWithBalance(userId).Result;
            context.Response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(new ProductIDCountBalanceDTO(){ Products = productsCount, Balance = userBalance})));
            Console.WriteLine(JsonSerializer.Serialize(new ProductIDCountBalanceDTO(){ Products = productsCount, Balance = userBalance}));*/
            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        public static async Task AddProductToDB(ProductIDCountDTO userProduct, int userId)
        {
            if (userProduct != null)
            {
                var userProductFromDB = UserProductRepository.GetUserProduct(userId, userProduct.ProductId).Result;
                await UserProductRepository.UpdateUserProducts(userId, userProduct.ProductId,
                        userProduct.ProductCount, userProductFromDB);
            }
        }
        
        
        public static async Task AddProductCount(HttpListenerContext context)
        {
            await using var inputStream = context.Request.InputStream;
            using var reader = new StreamReader(inputStream);
            var content = await reader.ReadToEndAsync();
            var userProduct = JsonSerializer.Deserialize<UserProductDTO>(content);
            var userId = await context.GetUserId();
            var userProductFromDB = UserProductRepository.GetUserProduct(userId, userProduct.ProductId).Result;
            var resultOfUpdatingDB =
                UserProductRepository.UpdateUserProductWithStatusCodes(userId, userProduct.ProductId,
                    userProduct.ProductCount, userProductFromDB).Result;
            if (userId is not -1 && userProduct is not null)
            {
                if (resultOfUpdatingDB == -205)
                {
                    context.Response.StatusCode = 201;
                    await context.Response.OutputStream.WriteAsync(
                        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(
                            new ProductCountUserBalanceDTO
                            {
                                ProductCount = 0,
                                Balance = UserRepository.GetUser(userId).Result.Balance
                            })));
                }
                else if (resultOfUpdatingDB != -1)
                {
                    context.Response.StatusCode = 200;
                    await context.Response.OutputStream.WriteAsync(
                        Encoding.UTF8.GetBytes(
                            JsonSerializer.Serialize(
                                new ProductCountUserBalanceDTO
                                {
                                    ProductCount = UserProductRepository.GetUserProduct(userId, userProduct.ProductId)
                                        .Result.ProductCount,
                                    Balance = UserRepository.GetUser(userId).Result.Balance
                                })));
                }
                else
                {
                    context.Response.StatusCode = 418;
                }
            }
            else
            {
                context.Response.StatusCode = 418;
            }

            context.Response.OutputStream.Close();
        }

        public static async Task<int> Register(HttpListenerContext context)
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
                    Console.WriteLine("allDone");
                    return user.Id;
                }
                else
                {
                    var succsessOperation = Encoding.ASCII.GetBytes("Error");
                    context.Response.StatusCode = 208;
                    await context.Response.OutputStream.WriteAsync(succsessOperation);
                    return -1;
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
                JsonSerializer
                    .Serialize(await ProductRepositoryWithCount.GetAllProductFromDB(await context.GetUserId()))
                    .GetBytes());
        }

        public static async Task GetUserProducts(HttpListenerContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;
            await context.Response.OutputStream.WriteAsync(
                JsonSerializer.Serialize(await ProductRepositoryWithCount.GetProductFromDB(await context.GetUserId()))
                    .GetBytes());
        }
    }
}