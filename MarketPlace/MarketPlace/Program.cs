using Microsoft.Data.SqlClient;
using System.Net;
using WebServer;

HttpClient client = new();
HttpListener listener = new();
client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34");
listener.Prefixes.Add("http://localhost:1111/");
listener.Start();
SqlConnection connection = new(@"Data source= LAPTOP-QHM9MDKR;Initial Catalog=MyDataBase; Integrated Security=True");

while(listener.IsListening)
{
    var context = listener.GetContext();
    var request = context.Request;
    var response = context.Response;
    switch (request.Url?.LocalPath)
    {
        case "/":
            await response.ShowFile("WWW/html/mainpage.html");
            break;
        case "/products":
            await response.ShowFile("WWW/html/products.html");
            break;
        default:
            break;
    }
    await context.ShowStatic();
    response.Close();
}
listener.Stop();