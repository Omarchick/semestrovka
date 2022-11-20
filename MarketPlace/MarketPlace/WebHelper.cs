using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace
{
    public static class WebHelper
    {
        public static async Task Home(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/mainpage.html");
        }
        public static async Task Products(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/products.html");
        }
        public static async Task Register(HttpListenerContext context)
        {
            await context.Response.ShowFile("WWW/html/mainpage.html");
        }
    }
}
