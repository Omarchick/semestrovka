namespace PRSClient;
using System.Net.Sockets;

public class User
{
    public byte[] Prs { get; set; }


    private User _enemy;

    public User Enemy
    {
        get => _enemy;
        set
        {
            _enemy = value;
            _enemy.Enemy = this;
        }
    }


    public Guid Id { get; set; }

    public User(Guid id)
    {
        Id = id;
    }

}
public static class UserHelper
{
    public static async Task<int> SetPaper(this Socket socket)
    {
        return await socket.SendAsync(new byte[] { 5 }, SocketFlags.None);
        //return new byte[] { 5 };
    }
    public static async Task<int> SetRock(this Socket socket)
    {
        return await socket.SendAsync(new byte[] { 9 }, SocketFlags.None);
        //return new byte[] { 9 };
    }
    public static async Task<int> SetScissors(this Socket socket)
    {
        return await socket.SendAsync(new byte[] { 8 }, SocketFlags.None);
        //return new byte[] { 8 };
    }

    public static async Task<int> StartGame(this Socket socket)
    {
        return await socket.SendAsync(new byte[] { 1 }, SocketFlags.None);
        //return new byte[] { 1 };
    }
    public static async Task<int> StopGame(this Socket socket)
    {
        return await socket.SendAsync(new byte[] { 0 }, SocketFlags.None);
        //return new byte[] { 0 };
    }
}