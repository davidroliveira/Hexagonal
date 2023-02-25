using ProjetoHexagonal.Commons.Environments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ProjetoHexagonal.Commons.Configurations
{
    public static class EnvironmentJsonConfigurationExtensions
    {
        public static IConfigurationBuilder ReplaceDefaultJsonByEnvironmentJson(this IConfigurationBuilder builder, IEnvironment environment = null)
        {
            environment ??= new SystemEnvironment();

            /*
             * Provê uma oportunidade para os testes automatizados substituirem o 'environment' por uma instância controlada.
             */
            ReplaceConfigurationSource<EnvironmentJsonConfigurationSource>(builder, environment);

            ReplaceConfigurationSource<JsonConfigurationSource>(builder, environment);

            return builder;
        }

        private static void ReplaceConfigurationSource<TSource>(IConfigurationBuilder builder, IEnvironment environment)
            where TSource : JsonConfigurationSource
        {
            var sources = builder.Sources.OfType<TSource>().ToList();

            foreach (var source in sources)
            {
                var index = builder.Sources.IndexOf(source);

                builder.Sources.RemoveAt(index);

                builder.Sources.Insert(index, new EnvironmentJsonConfigurationSource(environment)
                {
                    FileProvider   = source.FileProvider,
                    Path           = source.Path,
                    Optional       = source.Optional,
                    ReloadOnChange = source.ReloadOnChange
                });
            }
        }
    }
}
