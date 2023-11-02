using System;
using System.Threading.Tasks;
using WatsonWebserver;

namespace WatsonWebserverTest
{
    class Program
    {
        static string _Hostname = "localhost";
        static int _Port = 8000;
        static bool _Ssl = false;

        static Server _Server = null;
        
        static void Main(string[] args)
        {
            _Server = new Server(_Hostname, _Port, _Ssl, DefaultRoute);
            _Server.Routes.PreRouting = PreRoutingHandler;
            _Server.Settings.IO.EnableKeepAlive = false;
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

            await Task.Delay(10000);

            Console.WriteLine("Done");

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
