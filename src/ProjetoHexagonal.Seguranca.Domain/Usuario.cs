using static ProjetoHexagonal.Commons.Contract;

namespace ProjetoHexagonal.Seguranca.Domain
{
    public class Usuario
    {
        private readonly SenhaDigeridor senhaDigeridor;

        private short id;
      
        public Usuario(
            SenhaDigeridor senhaDigeridor,
            short id,
            UsuarioStatus status,
            string nomeDeUsuario,
            string senha,
            string nome,
            Departamento departamento)
        {
            this.senhaDigeridor = senhaDigeridor;
            this.id = id;
            this.Nome = nome;
            this.NomeDeUsuario = nomeDeUsuario;
            this.Senha = senha;
            this.Status = status;
            this.Departamento = departamento;
        }

        public short Id
        {
            get => this.id;
            set => this.id = Ensure(value, this.id == short.MinValue, "O id não pode ser sobrescrito.");
        }

        public UsuarioStatus Status { get; }

        public string NomeDeUsuario { get; }

        public string Senha { get; }

        public string Nome { get; }

        public Departamento Departamento { get; }

        public bool Sistema => this.Id < 0;

        public bool Autentico(string senha)
        {
            if (!this.Status.Equals(UsuarioStatus.LIBERADO))
            {
                return false;
            }

            var senhaDigerida = this.senhaDigeridor.Digerir(senha);

            return this.Senha.Equals(senhaDigerida);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            var otherUsuario = (Usuario)other;

            return this.Id == otherUsuario.Id;
        }

        public override int GetHashCode()
        {
            return new { ID = this.Id }.GetHashCode();
        }
    }
}
