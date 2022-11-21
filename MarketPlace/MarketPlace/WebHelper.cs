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
        public static async Task Home(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/mainpage.html");
        }
        
        public static async Task Products(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/products.html");
        }
        public static async Task ProductsNotRegistered(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/productsNotRegistered.html");
        }
        public static async Task SignIn(HttpListenerContext context)
        {
            Console.WriteLine(0);
            await using (var inputStream = context.Request.InputStream)
            {
                Console.WriteLine(1);
                using var reader = new StreamReader(inputStream);
                var content = await reader.ReadToEndAsync();
                Console.WriteLine(2);
                var user = JsonSerializer.Deserialize<User>(content);
                await using var stream = context.Response.OutputStream;
                Console.WriteLine(UserRepository.GetUser(user?.Name, user?.Password).Result);
                if (UserRepository.GetUser(user?.Name, user?.Password).Result != null)
                {
                    var succsessOperation = Encoding.ASCII.GetBytes("All done!");
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
    }
}
