using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PRS.Files;


public static class GameHelper
{
    public static byte[] GetByteId(this User user)
    {
        return user.Id.ToByteArray();
    }
    public static void ConnectUsers(this Queue<User> queue, Dictionary<EndPoint, User> players)
    {
        var firstPlayer = queue.Dequeue();
        var secondPlayer = queue.Dequeue();
        firstPlayer.Enemy = secondPlayer;
        firstPlayer.Connection.SendAsync(firstPlayer.GetByteId(), SocketFlags.None);
        secondPlayer.Connection.SendAsync(secondPlayer.GetByteId(), SocketFlags.None);
        if (firstPlayer.Connection.RemoteEndPoint == null || secondPlayer.Connection.RemoteEndPoint == null) return;
        players.Add(firstPlayer.Connection.RemoteEndPoint, firstPlayer);
        players.Add(secondPlayer.Connection.RemoteEndPoint, secondPlayer);
    }

    public static void SendInfoAboutGame(this User user, int status)
    {
        Console.WriteLine(user.Id + " - " + status);
        switch (status)
        {
            case 121:
                user.Connection.SendAsync(user.IsDraw, SocketFlags.None);
                user.Enemy.Connection.SendAsync(user.IsDraw, SocketFlags.None);
                break;
            case 111:
                user.Connection.SendAsync(user.IsWon, SocketFlags.None);
                user.Enemy.Connection.SendAsync(user.IsLoosed, SocketFlags.None);
                break;
            case 222:
                user.Connection.SendAsync(user.IsLoosed, SocketFlags.None);
                user.Enemy.Connection.SendAsync(user.IsWon, SocketFlags.None);
                break;
                
        }
    }
}