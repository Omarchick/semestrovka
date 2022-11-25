using System.Net.Sockets;
using System.Text;

using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var url = "www.google.com";
socket.Connect("www.google.com", 80);
if (socket.Connected)
{
    Console.WriteLine("Connected");
}

var message = $"GET / HTTP/1.1\r\nHost: {url}\r\nConnection: close\r\n\r\n";
var messageBytes = Encoding.UTF8.GetBytes(message);
var sendedBytes = await socket.SendAsync(messageBytes, SocketFlags.None);
Console.WriteLine(sendedBytes + " Байтов отправлено.");
Console.WriteLine(messageBytes.Length + " Хотел отправить.");

var stringBuilder = new StringBuilder();

var buffer = new byte[500];
do
{
    sendedBytes = await socket.ReceiveAsync(buffer, SocketFlags.None);
    stringBuilder.Append(Encoding.UTF8.GetString(buffer));
} 
while (sendedBytes > 0);

Console.WriteLine(stringBuilder);



