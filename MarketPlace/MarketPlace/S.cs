using System.Net;
using System.Text;

namespace WebServer
{
    public static class S
    {
        const string _errorMessage = "error";
        public static async Task ShowStatic(HttpListenerContext context)
        {
            var responce = context.Response;
            var requestPath = context.Request.RawUrl;
            var staticPath = Path.Combine(Directory.GetCurrentDirectory(), $"WWW{requestPath}");
            if (File.Exists(staticPath))
            {
                await ShowFile(responce, staticPath);
            }
            else
            {
                await ShowError(responce);
            }
        }

        public static async Task ShowFile(HttpListenerResponse responce, string path)
        {
            responce.ContentType = Path.GetExtension(path) switch
            {
                ".js" => "application/javascript",
                ".html" => "text/html",
                ".css" => "text/css",
                _ => throw new NotImplementedException()
            };
            responce.StatusCode = 200;
            using var stream = responce.OutputStream;
            await stream.WriteAsync(File.ReadAllBytes(path));
        }

        public static async Task ShowError(HttpListenerResponse responce)
        {
            responce.ContentType = "text/plain";
            responce.StatusCode = 404;
            using var stream = responce.OutputStream;
            await stream.WriteAsync(Encoding.UTF8.GetBytes(_errorMessage));
        }
    }
}

