using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;

namespace ProjetoHexagonal.Commons.Hangfire.Dashboard.Filters
{
    public sealed class BasicAuthAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string user;
        private readonly string password;

        public BasicAuthAuthorizationFilter(string user, string password)
        {
            this.user = user;
            this.password = password;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            var header = httpContext.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(header))
            {
                return Challenge(httpContext);
            }

            var authValues = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(header);

            if (!"Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase))
            {
                return Challenge(httpContext);
            }

            var parameter = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter!));
            var parts = parameter.Split(':');

            if (parts.Length < 2)
            {
                return Challenge(httpContext);
            }

            var username = parts[0];
            var password = parts[1];

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Challenge(httpContext);
            }

            if (username.Equals(user) && password.Equals(this.password))
            {
                return true;
            }

            return Challenge(httpContext);
        }

        private static bool Challenge(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
            httpContext.Response.WriteAsync("Authentication is required.");
            return false;
        }
    }
}
