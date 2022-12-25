using System.Net;
using System.Net.Sockets;
using System.Text;
using PRS.Files;

var usersWantToFind = new Queue<User>();
var players = new Dictionary<EndPoint, User>();
var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, 5000));
serverSocket.Listen();
//var stringBuilder = new StringBuilder();
//var a = client.RemoteEndPoint;
//Console.WriteLine(a + " -RemEP");
//await client.ConnectAsync(a);
while (true)
{
    try
    {
        var client = await serverSocket.AcceptAsync(); //ждет
        ClientRequestHandler.ProcessRequests(client, players, usersWantToFind);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception!!!");
        Console.WriteLine();
        Console.WriteLine(ex.Message);
    }

}