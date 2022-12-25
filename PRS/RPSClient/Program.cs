using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using PRSClient;

var user = new User(new Guid());

using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var url = "localhost";
socket.Connect(url, 5000);
if (socket.Connected)
{
    Console.WriteLine("Connected");
}


var sendedBytes =await  socket.StartGame();

var buffer = new byte[20];
/*do
{*/
Console.WriteLine("Тут");
sendedBytes = await socket.ReceiveAsync(buffer, SocketFlags.None);
user.Id = new Guid(buffer[..16]);
/*} 
while (sendedBytes > buffer.Length);*/


await socket.SetScissors();
buffer = new byte[20];
Console.WriteLine("Результат");
await socket.ReceiveAsync(buffer, SocketFlags.None);
Console.WriteLine(buffer[0]);