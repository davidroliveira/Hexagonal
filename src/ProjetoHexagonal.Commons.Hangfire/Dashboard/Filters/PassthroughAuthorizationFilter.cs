using Hangfire.Dashboard;

namespace ProjetoHexagonal.Commons.Hangfire.Dashboard.Filters
{
    public sealed class PassthroughAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
