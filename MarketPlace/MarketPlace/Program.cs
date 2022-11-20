using Microsoft.Data.SqlClient;
using System.Net;
using System.Text;
using System.Text.Json;
using MarketPlace;

HttpClient client = new();
HttpListener listener = new();
client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34");
listener.Prefixes.Add("http://localhost:1111/");
listener.Start();
SqlConnection connection = new(@"Data source= LAPTOP-QHM9MDKR;Initial Catalog=MyDataBase; Integrated Security=True");

while (listener.IsListening)
{
    //await UserRepository.AddUser("12", "22");
    Console.WriteLine((await UserRepository.GetUser("12", "22")).Name);
    //await UserRepository.UpdateUser("12", "22", "33", "34");
    //await UserRepository.DeleteUser(28);
    var context = await listener.GetContextAsync();
    var request = context.Request;
    var response = context.Response;
    _ = Task.Run(async () =>
    {
        switch (request.Url?.LocalPath)
        {
            case "/":
                await WebHelper.Home(context);
                break;
            case "/products":
                await WebHelper.Products(context);
                break;
            case "/registration":
                break;
            case "/signIn":
                break;
            case "/home":
                using (var stream = response.OutputStream)
                {
                    var c = new { mydata = 51 };
                    var a = JsonSerializer.Serialize(c);
                    response.StatusCode = 200;
                    response.Headers.Add("Access-Control-Allow-Origin", "*");
                    response.ContentType = "application/json";
                    Console.WriteLine("Пришло");
                    await stream.WriteAsync(Encoding.ASCII.GetBytes("56sdf"));
                }
                break;
            default:
                break;
        }
        await context.ShowStatic();
        response.Close();
    });
}
listener.Stop();