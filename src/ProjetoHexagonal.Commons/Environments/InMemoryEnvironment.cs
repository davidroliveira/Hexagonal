namespace ProjetoHexagonal.Commons.Environments
{
    public class InMemoryEnvironment : IEnvironment
    {
        private readonly Dictionary<string, string> variables;

        public InMemoryEnvironment()
        {
            this.variables = new Dictionary<string, string>();
        }

        public string this[string name]
        {
            get => this.variables[name];
            set => this.variables[name] = value;
        }

        public string ExpandVariables(string value)
        {
            var result = value;

            foreach (var name in this.variables.Keys)
            {
                result = result.Replace($"%{name}%", this.variables[name]);
            }

            return result;
        }
    }
}
