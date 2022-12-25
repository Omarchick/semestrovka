namespace PRS.Files;
public static class Judje
{
    public static byte CheckWinner(this User user)
    {
        if (user.Prs[0] == user.Enemy.Prs[0])
            return 121;
        return Convert.ToByte(user.Prs[0] switch
        {
            5 => user.Enemy.Prs[0] == 9 ? 111 : 222,
            9 => user.Enemy.Prs[0] == 8 ? 111 : 222,
            _ => user.Enemy.Prs[0] == 5 ? 111 : 222
        });
    }
}