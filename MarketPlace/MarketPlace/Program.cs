using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using MarketPlace;
using StackExchange.Redis;

HttpClient client = new();
HttpListener listener = new();
client.DefaultRequestHeaders.UserAgent.ParseAdd(
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34");
listener.Prefixes.Add("http://localhost:1111/");
listener.Start();
SqlConnection connection = new(@"Data source= LAPTOP-QHM9MDKR;Initial Catalog=MyDataBase; Integrated Security=True");
while (listener.IsListening)
{
    try
    {
        //Console.WriteLine(await UserRepository.AddUser("123asdf", "22we34Ff34f3@@4f43f3f43f"));
        //Console.WriteLine(await UserRepository.AddUser(new(5,"123asdf", "22we34Ff34f3@@4f43f3f43f", 5)));
        //Console.WriteLine(await UserRepository.GetUser("123asdf", "22we34Ff34f3@@4f43f3f43f"));
        //Console.WriteLine(await UserRepository.UpdateUser("123asdf", "22we34Ff34f3@@4f43f3f43f", "5234", "sdafFEF#$F3"));
        //await UserRepository.UpdateUser("12", "22", "33", "34");
        //await UserRepository.DeleteUser(28);
        
        /*Console.WriteLine(await ProductRepository.AddProduct("Арбузf", "Тёплый"));
        Console.WriteLine((await ProductRepository.GetProduct(6)).Name);
        Console.WriteLine(await ProductRepository.UpdateProduct(3, "ПФФ", "Работает"));
        Console.WriteLine((await ProductRepository.GetProduct(6)).Name);
        await ProductRepository.DeleteProduct(3);*/
        
        //Console.WriteLine(await ReviewRepository.AddReview(new Review(90, 1, 1, 1, "Nice")));
        //Console.WriteLine(ReviewRepository.UpdateReview(1, 4, "Хороший").Result);
        //Console.WriteLine(ReviewRepository.GetReview(111).Result.Id);
        //Console.WriteLine(ReviewRepository.DeleteReview(551));
        //Console.WriteLine(ReviewRepository.GetReviewsFromDB());

        //Console.WriteLine(await UserProductRepository.AddUserProduct(new UserProduct(1,1, 57)));
        Console.WriteLine(UserProductRepository.GetProductsByUserId(1).Result[0].Information);
        
        
        var context = await listener.GetContextAsync();
        var request = context.Request;
        var isUsingShowStatic = true;
        _ = Task.Run(async () =>
        {
            Console.WriteLine(request.Url?.LocalPath); //
            switch (request.Url?.LocalPath)
            {
                //Pages
                case "/":
                    await WebHelper.Home(context);
                    break;
                case "/productsNotRegistered":
                    await WebHelper.ProductsNotRegistered(context);
                    break;
                //Actions
                case "/register":
                    await WebHelper.Register(context);
                    break;
                case "/signIn":
                    await WebHelper.SignIn(context);
                    break;
                case "/getProductsFromDB":
                    await WebHelper.GetProductsFromDB(context);
                    isUsingShowStatic = false;
                    break;
                default:
                    break;
            }
            
            if (context.Request.Cookies["sessionId"] is not null)
            {
                var enteredUserId = await context.GetCookieInformation();
                var enteredUser = await UserRepository.GetUser(Convert.ToInt32(enteredUserId));
                if (enteredUser != null)
                {
                    await Session.SetSession(enteredUser, context);
                }
                
                switch (request.Url?.LocalPath)
                {
                    //Pages
                    case "/products":
                        await WebHelper.Products(context);
                        break;
                    case "/myProducts":
                        await WebHelper.GetMyProducts(context);
                        break;;
                    //Actions
                    case "/getUserProducts":
                        await WebHelper.GetUserProducts(context);
                        isUsingShowStatic = false;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (request.Url?.LocalPath)
                {
                    //Pages
                    case "/products":
                    case "/myProducts":
                        //case"/notFound": //Addings
                        await WebHelper.NotFound(context);
                        break;
                    //Actions

                    default:
                        break;
                }
            }

            if (isUsingShowStatic)
            {
                await context.ShowStatic();
            }
            else
            {
                context.Response.Close();
            }
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка!");
        Console.WriteLine(ex.Message);
    }
}

listener.Stop();