namespace ProjetoHexagonal.Commons.Environments
{
    public class CompositeEnvironment : IEnvironment
    {
        private readonly IEnvironment[] environments;

        public CompositeEnvironment(params IEnvironment[] environments)
        {
            this.environments = environments;
        }

        public string ExpandVariables(string value)
        {
            var result = value;

            foreach (var environment in this.environments)
            {
                result = environment.ExpandVariables(result);
            }

            return result;
        }
    }
}
