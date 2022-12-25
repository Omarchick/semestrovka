using System.Net.Sockets;
using System.Text;

namespace PRS.Files;

public class User
{
    public Guid Id = Guid.NewGuid();
    public byte[] Prs { get; set; }

    public byte[] IsStarted { get; set; } = new byte[] { 1 };
    public byte[] IsStopped { get; set; } = new byte[] { 0 };
    public byte[] IsWon { get; set; } = new byte[] { 111 };
    public byte[] IsLoosed { get; set; } = new byte[] { 222 };
    public byte[] IsDraw { get; set; } = new byte[] { 121 };

    public User enemy;
    public User Enemy
    {
        get => enemy;
        set
        {
            enemy = value;
            enemy.enemy = this;
        }
    }

    public Socket Connection { get; set; }

    public User(Socket connection)
    {
        Connection = connection;
    }
    
    public void SetPaper()
    {
        Prs = new byte[] { 5 };
    }
    public void SetRock()
    {
        Prs = new byte[] { 9 };
    }
    public void SetScissors()
    {
        Prs = new byte[] { 8 };
    }
}