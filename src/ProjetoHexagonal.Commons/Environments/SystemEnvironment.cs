namespace ProjetoHexagonal.Commons.Environments
{
    public class SystemEnvironment : IEnvironment
    {
        public string ExpandVariables(string value) => Environment.ExpandEnvironmentVariables(value);
    }
}
