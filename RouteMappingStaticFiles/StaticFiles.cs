using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RouteMappingStaticFiles
{
    public class StaticFiles
    {
        public static async Task ReturnStaticFile(HttpContext context)
        {
            FileInfo file;

            file = new FileInfo($"wwwroot/index.html");

            byte[] buffer;
            if (file.Exists)
            {
                buffer = File.ReadAllBytes(file.FullName);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "text/html";
                context.Response.ContentLength = buffer.Length;
                context.Response.Headers.Add("FileName", file.Name);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "text/plain";

                buffer = Encoding.UTF8.GetBytes("Not Found, 404");
            }

            using (var stream = context.Response.Body)
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
            }
        }
    }
}
