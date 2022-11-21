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
    //Console.WriteLine(await UserRepository.AddUser("123asdf", "22we34Ff34f3@@4f43f3f43f"));
    //Console.WriteLine(await UserRepository.AddUser(new(5,"123asdf", "22we34Ff34f3@@4f43f3f43f", 5)));
    //Console.WriteLine(await UserRepository.GetUser("123asdf", "22we34Ff34f3@@4f43f3f43f"));
    //Console.WriteLine(await UserRepository.UpdateUser("123asdf", "22we34Ff34f3@@4f43f3f43f", "5234", "sdafFEF#$F3"));
    //await UserRepository.UpdateUser("12", "22", "33", "34");
    //await UserRepository.DeleteUser(28);
    var context = await listener.GetContextAsync();
    var request = context.Request;
    var response = context.Response;
    _ = Task.Run(async () =>
    {
        Console.WriteLine(request.Url?.LocalPath);//
        switch (request.Url?.LocalPath)
        {
            case "/":
                await WebHelper.Home(context);
                break;
            case "/products":
                await WebHelper.Products(context);
                break;
            case "/productsNotRegistered":
                await WebHelper.ProductsNotRegistered(context);
                break;
            case "/register":
                await WebHelper.Register(context);

                break;
            case "/signIn":
                await WebHelper.SignIn(context);
                break;
            case "/home":
                break;
            default:
                break;
        }
        await context.ShowStatic();
        response.Close();
    });
}
listener.Stop();