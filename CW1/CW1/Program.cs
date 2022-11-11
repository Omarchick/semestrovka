using System.Data.SqlClient;
using System.Net;
using System.Text;
using WebServer;

var companyRepository = new CompanyRepository();
HttpListener httpListener = new();
HttpClient client = new();
client.DefaultRequestHeaders.UserAgent.ParseAdd(@"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34");
httpListener.Prefixes.Add("http://localhost:5555/");
httpListener.Start();
SqlConnection connection = new(@"Data source= LAPTOP-QHM9MDKR;Initial Catalog=CW1; Integrated Security=True");
connection.Open();
var DBName = @"Company";
SqlCommand command = new(@$"Select Id From {DBName}", connection);
var executeReader = command.ExecuteReader();
int idCount = 0;

while (executeReader.Read())
{
    idCount++;
}
connection.Close();
Console.WriteLine(idCount);


while (httpListener.IsListening)
{
    HttpListenerContext context = httpListener.GetContext();
    try
    {
        switch (context.Request.Url?.LocalPath)
        {
            case "/":
                await StaticFile.ShowFile(context.Response, "WWW/index.html");
                break;

            case "/createCompany":
                Console.WriteLine(123123);
                using (var inputStream = context.Request.InputStream)
                {
                    using var reader = new StreamReader(inputStream);
                    var content = await reader.ReadToEndAsync();
                    var enterData = content.Split(":");
                    enterData[0] = @$"{enterData[0]}";
                    enterData[1] = @$"{enterData[1]}";
                    var company = companyRepository.GetCompany(enterData[0]);
                    Console.WriteLine(enterData[0]);
                    if (company == null)
                    {
                        var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                        using var stream = context.Response.OutputStream;
                        companyRepository.CreateCompany(new Company() { Id = idCount, Name = enterData[0], Info = enterData[1]});
                        context.Response.StatusCode = 200;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                    else
                    {
                        //userRepository.CreateUser(new User {Id = idCount,UserName = enterData[0],UserPassword = enterData[1]});
                        var succsessOperation = Encoding.ASCII.GetBytes("Fail!");
                        using var stream = context.Response.OutputStream;
                        context.Response.StatusCode = 400;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                }
                break;
            case "/createOtziv":
                Console.WriteLine(123123);
                using (var inputStream = context.Request.InputStream)
                {
                    using var reader = new StreamReader(inputStream);
                    var content = await reader.ReadToEndAsync();
                    var enterData = content.Split(":");
                    enterData[0] = @$"{enterData[0]}";
                    enterData[1] = @$"{enterData[1]}";
                    var company = companyRepository.GetCompany(enterData[0]);
                    Console.WriteLine(enterData[0]);
                    if (company == null)
                    {
                        var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                        using var stream = context.Response.OutputStream;
                        companyRepository.CreateCompany(new Company() { Id = idCount, Name = enterData[0], Info = enterData[1] });
                        context.Response.StatusCode = 200;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                    else
                    {
                        //userRepository.CreateUser(new User {Id = idCount,UserName = enterData[0],UserPassword = enterData[1]});
                        var succsessOperation = Encoding.ASCII.GetBytes("Fail!");
                        using var stream = context.Response.OutputStream;
                        context.Response.StatusCode = 400;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                }
                break;
            case "/Find":
                using (var inputStream = context.Request.InputStream)
                {
                    using var reader = new StreamReader(inputStream);
                    var content = await reader.ReadToEndAsync();
                    var enterData = content.Split(":");
                    enterData[0] = @$"{enterData[0]}";
                    enterData[1] = @$"{enterData[1]}";
                    var company = companyRepository.GetCompany(enterData[0]);
                    Console.WriteLine(enterData[0]);
                    if (company == null)
                    {
                        var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                        using var stream = context.Response.OutputStream;
                        context.Response.StatusCode = 200;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                    else
                    {
                        var succsessOperation = Encoding.ASCII.GetBytes("Fail!");
                        using var stream = context.Response.OutputStream;
                        context.Response.StatusCode = 400;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                }
                break;

            case "/deleteCompany":
                using (var inputStream = context.Request.InputStream)
                {
                    using var reader = new StreamReader(inputStream);
                    var content = await reader.ReadToEndAsync();
                    var enterData = @$"{content}";
                    var user = companyRepository.GetCompany(Convert.ToInt32(enterData));
                    if (user != null)
                    {
                        companyRepository.DeleteCompany(user.Id);
                        var succsessOperation = Encoding.ASCII.GetBytes("All done!");
                        using var stream = context.Response.OutputStream;
                        context.Response.StatusCode = 200;
                        await context.Response.OutputStream.WriteAsync(succsessOperation);
                    }
                    else
                    {
                        var succsessOperation = Encoding.ASCII.GetBytes("Fail!");
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
