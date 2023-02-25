using Microsoft.Extensions.Configuration.Json;

namespace ProjetoHexagonal.Commons.Configurations
{
    public class EnvironmentJsonConfigurationProvider : JsonConfigurationProvider
    {
        private readonly IEnvironment environment;

        public EnvironmentJsonConfigurationProvider(JsonConfigurationSource source, IEnvironment environment)
            : base(source)
        {
            this.environment = environment;
        }

        public override void Load()
        {
            base.Load();

            this.Data = this.Data.ToDictionary(
                x => x.Key, x => this.environment.ExpandVariables(x.Value), StringComparer.OrdinalIgnoreCase);
        }
    }
}
