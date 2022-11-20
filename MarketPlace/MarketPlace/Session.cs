using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MarketPlace
{
    public class Session
    {
        /*const double _sessionTime = 20;
        public readonly string? UserName;
        public readonly string? UserPassword;
        public readonly int UserId;
        public string? SessionId;

        public Session(User EnteredUser)
        {
            if (EnteredUser is not null)
            {
                UserName = EnteredUser.UserName!;
                UserPassword = EnteredUser.UserPassword!;
                UserId = EnteredUser.Id;
                SessionId = EnteredUser.Id.ToString().CreateMD5() + "mysite";
            }
        }
        public override string ToString()
        {
            return $"{UserName}:{UserPassword}:{UserId}";
        }
        public string this[int key]
        {
            get
            {
                if (key < 0 && key > 4)
                    return null!;
                return ToString().Split(":")[key];
            }
        }
        public static async Task<bool> IsInSession(HttpListenerContext context, User EnteredUser)
        {
            var request = context.Request;
            if (request.Cookies["sessionId"] != null)
            {
                if ((await RedisStore.RedisCashe.StringGetAsync(request.Cookies[EnteredUser.Id.ToString().CreateMD5() + "mysite"]!.Value)).HasValue)
                {
                    request.Cookies["sessionId"]!.Expires = DateTime.UtcNow.AddMinutes(_sessionTime);
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public static async Task SetSession(User EnteredUser, HttpListenerContext context)
        {
            if (EnteredUser != null)
            {
                await RedisStore.RedisCashe.StringSetAsync(EnteredUser.Id.ToString().CreateMD5() + "mysite",new Session(EnteredUser).ToString());
                context.Response.Cookies.Add(new Cookie("sessionId", EnteredUser.Id.ToString().CreateMD5() + "mysite")
                { Expires = DateTime.UtcNow.AddMinutes(_sessionTime) });
            }
        }*/
    }
}
