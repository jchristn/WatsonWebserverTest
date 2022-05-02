using System;
using System.Threading.Tasks;
using WatsonWebserver;

namespace WatsonWebserverTest
{
    class Program
    {
        static Server _Server = null;

        static void Main(string[] args)
        {
            _Server = new Server("127.0.0.1", 8000, true, DefaultRoute);
            _Server.Routes.PreRouting = PreRoutingHandler;
            _Server.Start();

            Console.Write("Server started on:");
            foreach (string prefix in _Server.Settings.Prefixes)
            {
                Console.Write(" " + prefix);
            }
            Console.WriteLine("");
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

        private static async Task<bool> PreRoutingHandler(HttpContext ctx)
        {
            Console.WriteLine(ctx.Request.Method.ToString() + " " + ctx.Request.Url.RawWithQuery);
            await Task.Delay(0);
            return false;
        }

        static async Task DefaultRoute(HttpContext ctx)
        {
            ctx.Response.ContentType = "text/html";
            await ctx.Response.Send(ResponseBody);
        }

        static string ResponseBody =
            "<html>" + Environment.NewLine +
            "  <head>" + Environment.NewLine +
            "    <title>Watson Webserver</title>" + Environment.NewLine +
            "  </head>" + Environment.NewLine +
            "  <body>" + Environment.NewLine +
            "    <p>Hello from Watson Webserver</p>" + Environment.NewLine +
            "  </body>" + Environment.NewLine +
            "</html>";
            
    }
}
