using System.Data.SqlClient;
using System.Net;
using System.Text;
using WebServer;

var userRepository = new UserRepository();
HttpListener httpListener = new();
HttpClient client = new();
client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34");
httpListener.Prefixes.Add("http://localhost:5555/");
httpListener.Start();
SqlConnection connection = new(@"Data source= LAPTOP-QHM9MDKR;Initial Catalog=MyDataBase; Integrated Security=True");
connection.Open();
SqlCommand command = new("Select Id From TableDB", connection);
var executeReader = command.ExecuteReader();
int idCount = 0;
var localPathsForAll = new string[] { "/authorization", "/reg", "/signIn", "/newsForAll" };

while (executeReader.Read())
{
    idCount++;
}
connection.Close();


while (httpListener.IsListening)
{
    HttpListenerContext context = httpListener.GetContext();
    try
    {
        switch (context.Request.Url?.LocalPath)
        {
            case "/authorization":
                /*            await Store.StringSetAsync("1", "2");
                            await Store.StringGetAsync(new RedisKey("1"));
                            Console.WriteLine(Store.StringGet(new RedisKey("1")));
                            //await Store.SetAddAsync(new RedisKey("1"), new RedisValue("3"));
                            //Console.WriteLine(Store.StringGet("1"));*/
                await StaticFile.ShowFile(context.Response, "WWW/authorization.html");
                break;
            case "/":
                await StaticFile.ShowFile(context.Response, "WWW/Site.html");
                break;
            case "/news":
                using (var stream = context.Response.OutputStream)
                {
                    var response =
                        await client.GetAsync(
                            "https://newsapi.org/v2/everythzing?q=tesla&apiKey=8cb87580d0744fd3aedc6efa2720d9c0");
                    context.Response.StatusCode = 200;
                    await context.Response.OutputStream.WriteAsync(await response.Content.ReadAsByteArrayAsync());
                }

                break;
            case "/newsForAll":
                using (var stream = context.Response.OutputStream)
                {
                    var response =
                        await client.GetAsync(
                            "https://newsapi.org/v2/everythzing?q=tesla&apiKey=8cb87580d0744fd3aedc6efa2720d9c0");
                    context.Response.StatusCode = 200;
                    await context.Response.OutputStream.WriteAsync(await response.Content.ReadAsByteArrayAsync());
                }

                break;
            case "/reg":
                using (var inputStream = context.Request.InputStream)
                {
                    using var reader = new StreamReader(inputStream);
                    var content = await reader.ReadToEndAsync();
                    var enterData = content.Split(":");
                    enterData[0] = @$"{enterData[0]}";
                    enterData[1] = @$"{enterData[1].CreateMD5()}";
                    var resultOfSelect = $"SELECT count(UserName) FROM TableDB Where UserName = '{enterData[0]}'"
                        .WorkSingleValue();
                    int countOfSelect = (string.IsNullOrEmpty(enterData[0])) ? 1 : (int)resultOfSelect;
                    $"INSERT INTO TableDB (Id, UserName, UserPassword, ToDoList) VALUES ({idCount}, '{enterData[0]}', '{enterData[1]}', '')"
                            .WorkSmthValue();
                        var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                        using (var stream = context.Response.OutputStream)
                        {
                            context.Response.StatusCode = 201;
                            await context.Response.OutputStream.WriteAsync(succsessOperation);
                        }
                        
                }
                break;

            case "/signIn":
                using (var inputStream = context.Request.InputStream)
                {
                    using var reader = new StreamReader(inputStream);
                    var content = await reader.ReadToEndAsync();
                    var enterData = content.Split(":");
                    enterData[0] = @$"{enterData[0]}";
                    enterData[1] = @$"{enterData[1]}";
                    var user = userRepository.GetUser(enterData[0], enterData[1]);
                    if (user != null)
                    {
                        var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                        using var stream = context.Response.OutputStream;
                        context.Response.StatusCode = 200;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                    else
                    {
                        //userRepository.CreateUser(new User {Id = idCount,UserName = enterData[0],UserPassword = enterData[1]});
                        var succsessOperation = Encoding.ASCII.GetBytes("Not registered!");
                        using var stream = context.Response.OutputStream;
                        context.Response.StatusCode = 400;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                }
                break;

            case "/deleteUser":
                using (var inputStream = context.Request.InputStream)
                {
                    using var reader = new StreamReader(inputStream);
                    var content = await reader.ReadToEndAsync();
                    var enterData = content.Split(":");
                    enterData[0] = @$"{enterData[0]}";
                    enterData[1] = @$"{enterData[1]}";
                    var user = userRepository.GetUser(enterData[0], enterData[1]);
                    if (user != null)
                    {
                        userRepository.DeleteUser(user.Id);
                        var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                        using var stream = context.Response.OutputStream;
                        context.Response.StatusCode = 200;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                    else
                    {
                        var succsessOperation = Encoding.ASCII.GetBytes("Not registered!");
                        using var stream = context.Response.OutputStream;
                        context.Response.StatusCode = 400;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                }

                break;
        }

        await StaticFile.ShowStatic(context);
        context.Response.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка");
        Console.WriteLine(ex.Message);
    }
}

httpListener.Stop();
