using System.Net;
using System.Net.Sockets;

namespace PRS.Files;

public static class ClientRequestHandler
{
    public static async Task ProcessRequests(Socket client, Dictionary<EndPoint, User> players,
        Queue<User> usersWantToFind)
    {
        while (true)
        {
            var sentBytes = 0;
            var buffer = new byte[20];
            //do
            //{
                Console.WriteLine("Тут всё отправлено");
                sentBytes = await client.ReceiveAsync(buffer, SocketFlags.None);
                if (sentBytes == 0)
                {
                    break;
                }
                Console.WriteLine(sentBytes + " - sended bytes");
                Console.WriteLine(buffer[0]);
                switch (buffer[0])
                {
                    case 1:
                        Console.WriteLine("User want to find game");
                        var user = new User(client);
                        usersWantToFind.Enqueue(user);
                        if (usersWantToFind.Count >= 2)
                        {
                            Console.WriteLine("Тут 2 игрока");
                            usersWantToFind.ConnectUsers(players);
                            //await client.SendAsync(user.GetByteId(), SocketFlags.None);
                        }

                        break;
                    case 0:
                        Console.WriteLine("User want to END the game");
                        break;
                    case 5:
                        if (client.RemoteEndPoint != null) players[client.RemoteEndPoint].SetPaper();
                        Console.WriteLine(players[client.RemoteEndPoint].Prs[0]);
                        Console.WriteLine("Paper");
                        break;
                    case 9:
                        if (client.RemoteEndPoint != null) players[client.RemoteEndPoint].SetRock();
                        Console.WriteLine("Rock");
                        break;
                    case 8:
                        if (client.RemoteEndPoint != null) players[client.RemoteEndPoint].SetScissors();
                        Console.WriteLine("Scissors");
                        break;
                }

                if (client.RemoteEndPoint != null &&
                    players.TryGetValue(client.RemoteEndPoint, out var usr) &&
                    usr?.Prs is not null &&
                    players[client.RemoteEndPoint].Enemy?.Prs is not null)
                {
                    Console.WriteLine("Здусь");
                    players[client.RemoteEndPoint].SendInfoAboutGame(players[client.RemoteEndPoint].CheckWinner());
                    Console.WriteLine(players[client.RemoteEndPoint].CheckWinner());
                }
            //} while (sentBytes > 0);
        }
        // ReSharper disable once FunctionNeverReturns
    }
}