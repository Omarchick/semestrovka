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
            using (var inputStream = context.Request.InputStream)
            {
                using var reader = new StreamReader(inputStream);
                var content = await reader.ReadToEndAsync();
                var user = JsonSerializer.Deserialize<User>(content);
                Console.WriteLine(user.Name);
                Console.WriteLine(user.Password);
                if (await UserRepository.AddUser(user) != -1)
                {
                    Console.WriteLine(1121);
                    var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                    using var stream = context.Response.OutputStream;
                    context.Response.StatusCode = 200;
                    await context.Response.OutputStream.WriteAsync(succsessOperation);
                }
                else
                {
                    Console.WriteLine(2133);
                    var succsessOperation = Encoding.ASCII.GetBytes("Error");
                    using var stream = context.Response.OutputStream;
                    context.Response.StatusCode = 400;
                    await context.Response.OutputStream.WriteAsync(succsessOperation);
                }
            }
        }

        public static async Task Register(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/mainpage.html");
        }
    }
}
