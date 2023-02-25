using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ProjetoHexagonal.Commons.Configurations
{
    public class  EnvironmentJsonConfigurationSource : JsonConfigurationSource
    {
        private readonly IEnvironment environment;

        public EnvironmentJsonConfigurationSource(IEnvironment environment)
        {
            this.environment = environment;
        }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);

            return new EnvironmentJsonConfigurationProvider(this, this.environment);
        }
    }
}
