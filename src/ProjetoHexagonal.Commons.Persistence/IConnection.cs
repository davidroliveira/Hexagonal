namespace ProjetoHexagonal.Commons.Persistence
{
    public interface IConnection : IDisposable
    {
        ICommand Command(string text);
    }
}
