using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;

namespace Starterkit.Helper
{
    public class Context
    {
        public static ISession Session { get; set; }

        public static HttpRequest Request { get; set; }

        public static ClaimsPrincipal User { get; set; }

        public static HttpResponse Response { get; set; }

        public static string TraceIdentifier { get; set; }

        public static ConnectionInfo Connection { get; set; }

        public static IFeatureCollection Features { get; set; }

        public static WebSocketManager WebSockets { get; set; }

        public static IServiceProvider RequestServices { get; set; }
    }
}