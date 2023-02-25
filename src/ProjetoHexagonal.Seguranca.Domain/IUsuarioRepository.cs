using ProjetoHexagonal.Commons;

namespace ProjetoHexagonal.Seguranca.Domain
{
    public interface IUsuarioRepository
    {
         Result<Usuario> GetByNomeDeUsuario(string nomeDeUsuario);

         Result<Usuario> GetByID(short id);
    }
}
